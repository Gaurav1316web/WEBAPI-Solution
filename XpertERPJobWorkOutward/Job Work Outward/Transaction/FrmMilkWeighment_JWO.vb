
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Public Class FrmMilkWeighment_JWO
    Inherits FrmMainTranScreen
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colDipValue As String = "colDipValue"
    Public strDocCode As String = ""
    Dim docType As String = String.Empty
    Dim obj As clsMilkWeighment_JOW = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub reset()
        chkBulkMilkProc.IsChecked = True
        fndTankerNo.Value = ""
        fndDocNO.Value = ""
        fndDocNO.MyReadOnly = False
        fndGateEntryNoBulk.Value = ""
        lblGateEntryDateAndTimeValueBulk.Text = ""
        lblLocationCodeBulk.Text = ""
        lblLocationNameBulk.Text = ""
        lblVendorCodeBulk.Text = ""
        lblVendorNameBulk.Text = ""
        lblChallanNoBulk.Text = ""
        lblChallanDateBulk.Text = ""
        lblTankerNoBulk.Text = ""
        lblStatusBulk.Text = ""
        txtjobworklocation.Text = ""
        lblJobworkLocation.Text = ""
        txtDipValue.Text = ""
        txtDipValue.ReadOnly = False
        dtpWeighmentDateBulk.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
        dtpWeighmentDateBulk.Enabled = True
        'dtpTareWeight.Enabled = False
        dtpTareWeight.Value = dtpWeighmentDateBulk.Value
        loadBlankGrid()
        If chkBulkMilkProc.IsChecked Then
            lblVendorBulk.Text = "Vendor"
        Else
            lblVendorBulk.Text = "Vendor"
        End If
        txtWeighmentSlipNo.Text = ""
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        btnReverse.Visible = False
    End Sub
    Sub loadBlankGrid()
        If chkBulkMilkProc.IsChecked Then
            loadBlankGvItemBulk()
        Else
            loadBlankGvItemMcc()
        End If
        ReStoreGridLayout()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMilkWeighment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub
    Sub loadBlankGvItemBulk()

        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = True

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Qty ")
        gvItemBulk.Columns(colQty).Width = 120
        gvItemBulk.Columns(colQty).IsVisible = False
        gvItemBulk.Columns(colQty).ReadOnly = True

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 75
        gvItemBulk.Columns(colFat).ReadOnly = True
        gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 75
        gvItemBulk.Columns(colSNF).ReadOnly = True
        gvItemBulk.Columns(colSNF).IsVisible = False


        gvItemBulk.Columns.Add(colFatKG, "FAT KG")
        gvItemBulk.Columns(colFatKG).Width = 75
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF KG")
        gvItemBulk.Columns(colSNFKG).Width = 75
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False

        gvItemBulk.Columns.Add(colGrossWeight, "Gross Weight")
        gvItemBulk.Columns(colGrossWeight).Width = 75
        gvItemBulk.Columns(colGrossWeight).ReadOnly = False



        gvItemBulk.Columns.Add(colTareWeight, "Tare Weight")
        gvItemBulk.Columns(colTareWeight).Width = 75
        gvItemBulk.Columns(colTareWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colNetWeight, "Net Weight")
        gvItemBulk.Columns(colNetWeight).Width = 75
        gvItemBulk.Columns(colNetWeight).ReadOnly = True


        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        gvItemBulk.AllowAddNewRow = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                errorControl.SetError(fndTankerNo, "Please Select Tanker No First ")
                Throw New Exception("Please Select Tanker No    ")
            Else
                errorControl.SetError(fndTankerNo, "")
            End If
            If clsCommon.myLen(fndGateEntryNoBulk.Value) <= 0 Then
                errorControl.SetError(fndGateEntryNoBulk, "Please Select Gate Entry No First ")
                Throw New Exception("Please Select Gate Entry No    ")
            Else
                errorControl.SetError(fndGateEntryNoBulk, "")
            End If
            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) < 0 Then
                Throw New Exception("Gross Weight is mandatory, And it must not be negative in Grid at Row No 1")
            End If
            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) = 0 Then
                Throw New Exception("Gross Weight is mandatory, And it must not be zero or blank in Grid at Row No 1")
            End If
            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value) < 0 Then
                Throw New Exception("Net Weight must not be negative in Grid at Row No 1")
            End If
            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value) < 0 Then
                Throw New Exception("Tare Weight must not be negative in Grid at Row No 1")
            End If
            If dtpWeighmentDateBulk.Value < clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text) Then
                Throw New Exception("Weighment Date can not be less than Gate Entry Date")
            End If
            'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_Milk_quality_check where isposted=1 and gate_entry_no='" & fndGateEntryNoBulk.Value & "' ")) > 0 Then
            '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_Milk_quality_check where isposted=1 and gate_entry_no='" & fndGateEntryNoBulk.Value & "' ")) <= 0 AndAlso clsCommon.myCdbl(txtDipValue.Text) <= 0 Then
            '        txtDipValue.Focus()
            '        Throw New Exception("Please fill Dip Value")
            '    End If
            'End If
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_Weighment where gate_entry_no='" & fndGateEntryNoBulk.Value & "' and weighment_no <>'" & fndDocNO.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            'If dtpTareWeight.Enabled = True Then
            If dtpTareWeight.Value < dtpWeighmentDateBulk.Value Then
                Throw New Exception("Tare Weighment date time (" + clsCommon.GetPrintDate(dtpTareWeight.Value, "dd/MM/yyyy hh:mm tt") + ") must not smaller or equal to gross weight date time (" + clsCommon.GetPrintDate(dtpWeighmentDateBulk.Value, "dd/MM/yyyy hh:mm tt") + ")")
            End If
            Dim QCDatetime As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select QC_In_Date_Time from TSPL_JWO_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNoBulk.Value & "'"), "dd/MM/yyyy hh:mm tt")
            If dtpTareWeight.Value < QCDatetime Then
                Throw New Exception("Tare Weighment date time (" + clsCommon.GetPrintDate(dtpTareWeight.Value, "dd/MM/yyyy hh:mm tt") + ") must not smaller or equal to unloading date time (" + clsCommon.GetPrintDate(QCDatetime, "dd/MM/yyyy hh:mm tt") + ")")
            End If
            'End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowWeighmentDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpWeighmentDateBulk.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpWeighmentDateBulk.Value = dt
                    Throw New Exception("Gross weighment Date should not be Larger than current date")
                End If
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowWeighmentDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpTareWeight.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpTareWeight.Value = dt
                    Throw New Exception("Tare weighment Date should not be Larger than current date")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Sub SaveData(ByVal isPostbtnClick As Boolean)
        Try
            'Dim trans As SqlTransaction = Nothing
            obj = New clsMilkWeighment_JOW()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            'trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
            'If obj.isNewEntry Then
            '    obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpWeighmentDateBulk.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkWeighment, "", lblLocationCodeBulk.Text)
            '    If clsCommon.myLen(obj.Weighment_No) <= 0 Then
            '        clsCommon.MyMessageBoxShow("Error In Weighment No Genertion")
            '        Exit Sub
            '    End If
            'Else
            '    obj.Weighment_No = clsCommon.myCstr(fndDocNO.Value)
            'End If
            obj.Weighment_No = clsCommon.myCstr(fndDocNO.Value)
            'If dtpTareWeight.Enabled = True Then
            obj.Tare_Weight_date = dtpTareWeight.Value
            'End If
            If chkBulkMilkProc.IsChecked Then
                obj.Weighment_Date = clsCommon.GetPrintDate(dtpWeighmentDateBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNoBulk.Value)
                obj.Doc_Type = "Tanker"
                obj.Date_And_Time = clsCommon.GetPrintDate(clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Challan_No = clsCommon.myCstr(lblChallanNoBulk.Text)
                obj.Challan_Date = clsCommon.GetPrintDate(clsCommon.myCDate(lblChallanDateBulk.Text), "dd/MMM/yyyy")
                obj.Tanker_No = clsCommon.myCstr(lblTankerNoBulk.Text)
                obj.location_Code = clsCommon.myCstr(lblLocationCodeBulk.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationNameBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(lblVendorCodeBulk.Text)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.Gross_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
                obj.Tare_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
                obj.Net_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
                obj.Weighment_Slip_No = clsCommon.myCstr(txtWeighmentSlipNo.Text)
                obj.JobWorkLocation = txtjobworklocation.Text
            ElseIf chkMccProc.IsChecked Then
                obj.Weighment_Date = clsCommon.GetPrintDate(dtpWeighmentDateBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNoBulk.Value)
                obj.Doc_Type = "Sku_Receipt"
                obj.Date_And_Time = clsCommon.GetPrintDate(clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Challan_No = clsCommon.myCstr(lblChallanNoBulk.Text)
                obj.Challan_Date = clsCommon.GetPrintDate(clsCommon.myCDate(lblChallanDateBulk.Text), "dd/MMM/yyyy")
                obj.Tanker_No = clsCommon.myCstr(lblTankerNoBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(lblVendorCodeBulk.Text)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
                obj.location_Code = clsCommon.myCstr(lblLocationCodeBulk.Text)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationNameBulk.Text)
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)

                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.Gross_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
                obj.Tare_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
                obj.Net_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value)
                obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
                obj.Weighment_Slip_No = clsCommon.myCstr(txtWeighmentSlipNo.Text)
                obj.JobWorkLocation = txtjobworklocation.Text
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            If clsMilkWeighment_JOW.saveData(obj, obj.isNewEntry) Then
                'trans.Commit()
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                btnSave.Text = "Update"
                fndDocNO.MyReadOnly = True
                loadData(obj.Weighment_No, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavigatorType.Current)

                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPrint.Enabled = False
            btnPost.Enabled = False
            fndDocNO.MyReadOnly = False
            'trans.Rollback()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub deleteData()
        Dim arr As List(Of String) = New List(Of String)
        If clsCommon.myLen(fndDocNO.Value) > 0 Then
            If deleteConfirm() Then
                arr.Add(fndDocNO.Value)
                If clsERPFuncationality.AddToHistory(arr, clsUserMgtCode.FrmMilkWeighment, Nothing) Then
                    If clsMilkWeighment_JOW.deleteData(fndDocNO.Value) Then
                        reset()
                        myMessages.delete()
                    Else
                        clsCommon.MyMessageBoxShow("Could not deleted")
                    End If
                End If
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select a weighment no to delete")
        End If
    End Sub
    Sub postData()
        Try
            Dim strDocType As String = String.Empty
            If chkBulkMilkProc.IsChecked Then
                strDocType = "Tanker"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "Sku_Receipt"
            End If
            Dim trWeight As Double = 0
            Dim GrsWeight As Double = 0
            Dim netWeight As Double = 0
            trWeight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
            GrsWeight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
            If trWeight < 0 Then
                Throw New Exception("Tare Weight can not be negative")
            End If
            If trWeight = 0 Then
                Throw New Exception("Tare Weight can not be Zero. Please Fill Tare Weight")
            End If
            If trWeight > GrsWeight Then
                Throw New Exception("value of Tare Weight can not be more than Gross Weight")
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsMilkWeighment_JOW.postData(fndDocNO.Value, strDocType, Me.Form_ID, Nothing)) Then
                    msg = "Successfully Posted"
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
                common.clsCommon.MyMessageBoxShow(msg)
                loadData(fndDocNO.Value, strDocType, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Sub sendForQC()
    '    If clsCommon.myLen(fndDocNO.Value) > 0 Then
    '        Dim strqry As String = "Update TSPL_JWO_Weighment set status=1, sent_to_qc_by='" & objCommonVar.CurrentUserCode & "',sent_to_qc_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' where Weighment_No='" & fndDocNO.Value & "'"
    '        clsDBFuncationality.ExecuteNonQuery(strqry)
    '        clsCommon.MyMessageBoxShow("SuccessFully Sent For QC")
    '        loadData(fndDocNO.Value, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavigatorType.Current)
    '    Else
    '        clsCommon.MyMessageBoxShow("Please Select a Weighment No")
    '    End If
    'End Sub
    Sub printData()
        clsCommon.MyMessageBoxShow("No Print Format Found")
    End Sub
    Sub loadData(ByVal strWeighmentNo As String, ByVal docType As String, ByVal navType As NavigatorType)
        Try
            obj = New clsMilkWeighment_JOW()
            If clsCommon.myLen(docType) > 0 Then
                obj = clsMilkWeighment_JOW.getData(strWeighmentNo, docType, navType, False)
            Else
                obj = clsMilkWeighment_JOW.getData(strWeighmentNo, navType, False)
            End If

            If obj IsNot Nothing Then
                If clsCommon.CompairString(obj.Doc_Type, "Tanker") = CompairStringResult.Equal Then
                    chkBulkMilkProc.IsChecked = True
                    fndDocNO.Value = obj.Weighment_No
                    fndGateEntryNoBulk.Value = obj.Gate_Entry_No
                    dtpWeighmentDateBulk.Value = clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MM/yyyy hh:mm:ss tt")
                    lblGateEntryDateAndTimeValueBulk.Text = obj.Date_And_Time
                    lblLocationCodeBulk.Text = obj.location_Code
                    lblLocationNameBulk.Text = obj.Location_Desc
                    lblVendorCodeBulk.Text = obj.Vendor_Code
                    lblVendorNameBulk.Text = obj.Vendor_Desc
                    lblChallanNoBulk.Text = obj.Challan_No
                    lblChallanDateBulk.Text = obj.Challan_Date
                    lblTankerNoBulk.Text = obj.Tanker_No
                    fndTankerNo.Value = obj.Tanker_No
                    txtDipValue.Text = obj.Dip_Value
                    txtWeighmentSlipNo.Text = obj.Weighment_Slip_No
                    txtjobworklocation.Text = obj.JobWorkLocation
                    lblJobworkLocation.Text = clsLocation.GetName(txtjobworklocation.Text, Nothing)
                    If obj.Tare_Weight_date IsNot Nothing Then
                        dtpTareWeight.Value = obj.Tare_Weight_date
                    End If
                    If clsCommon.myCdbl(obj.Dip_Value) = 0 Then
                        txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_JWO_QUALITY_CHECK where  isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'"))
                    End If
                    If obj.status = 0 Then
                        lblStatusBulk.Text = "Not Done"
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                    ElseIf obj.status = 1 Then
                        lblStatusBulk.Text = "Done"
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                    End If
                    btnPrint.Enabled = True
                    gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                    gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                    gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                    gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                    gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                    gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                    gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                    gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
                    gvItemBulk.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                    gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                    gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                    If obj.isPosted = 1 Then
                        lblPending.Status = ERPTransactionStatus.Approved
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                    Else
                        lblPending.Status = ERPTransactionStatus.Pending
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Sku_Receipt") = CompairStringResult.Equal Then
                    chkMccProc.IsChecked = True
                    fndDocNO.Value = obj.Weighment_No
                    fndGateEntryNoBulk.Value = obj.Gate_Entry_No
                    dtpWeighmentDateBulk.Value = clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MM/yyyy hh:mm:ss tt")
                    lblGateEntryDateAndTimeValueBulk.Text = obj.Date_And_Time
                    lblLocationCodeBulk.Text = obj.location_Code
                    lblLocationNameBulk.Text = clsLocation.GetName(obj.location_Code, Nothing)
                    lblVendorCodeBulk.Text = obj.Vendor_Code
                    lblVendorNameBulk.Text = obj.Vendor_Desc
                    lblChallanNoBulk.Text = obj.Challan_No
                    lblChallanDateBulk.Text = obj.Challan_Date
                    lblTankerNoBulk.Text = obj.Tanker_No
                    fndTankerNo.Value = obj.Tanker_No
                    txtDipValue.Text = obj.Dip_Value
                    txtWeighmentSlipNo.Text = obj.Weighment_Slip_No
                    txtjobworklocation.Text = obj.JobWorkLocation
                    lblJobworkLocation.Text = clsLocation.GetName(txtjobworklocation.Text, Nothing)
                    If obj.Tare_Weight_date IsNot Nothing Then
                        dtpTareWeight.Value = obj.Tare_Weight_date
                    End If
                    If clsCommon.myCdbl(obj.Dip_Value) = 0 Then
                        obj.Dip_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_JWO_QUALITY_CHECK where isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'"))
                    End If
                    If obj.status = 0 Then
                        lblStatusBulk.Text = "Not Done"
                    ElseIf obj.status = 1 Then
                        lblStatusBulk.Text = "Done"
                    End If
                    btnPrint.Enabled = True
                    gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                    gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                    gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                    gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                    gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                    gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                    gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                    gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
                    gvItemBulk.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                    gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                    gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                    If obj.isPosted = 1 Then
                        lblPending.Status = ERPTransactionStatus.Approved
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                    Else
                        lblPending.Status = ERPTransactionStatus.Pending
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                    End If
                End If

                If obj.isPosted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnPrint.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    If (clsMilkUnloading_JOW.isUnloadingDone(obj.Gate_Entry_No, Nothing) And chkBulkMilkProc.IsChecked) Then
                        btnPost.Enabled = True
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                        gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = True
                        dtpTareWeight.Text = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
                        'dtpTareWeight.Enabled = True
                        dtpWeighmentDateBulk.Enabled = False
                    Else
                        btnPost.Enabled = False
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                        gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = False
                        'dtpTareWeight.Enabled = False
                        dtpWeighmentDateBulk.Enabled = True
                    End If
                    btnPrint.Enabled = True
                End If

                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
                'If obj.isPosted = 0 Then
                '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & obj.Challan_No & "' ")) = 1 Then
                '        Dim isReachedAtFinal As Integer = 0
                '        isReachedAtFinal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(reachedAtFinal,0) from TSPL_MCC_Dispatch_Challan where  Chalan_NO ='" & obj.Challan_No & "'  "))
                '        'gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')")))
                '        If isReachedAtFinal = 1 Then
                '            gvItemBulk.Columns(colTareWeight).ReadOnly = False
                '            gvItemBulk.Rows(0).Cells(colTareWeight).Value = IIf(clsCommon.myCdbl(obj.Tare_Weight) = 0, clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')"))), clsCommon.myFormat(obj.Tare_Weight))
                '        Else
                '            gvItemBulk.Columns(colTareWeight).ReadOnly = True
                '            gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')")))
                '        End If
                '        gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = False
                '        gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) - clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value))
                '        btnPost.Enabled = True
                '    End If
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub loadBlankGvItemMcc()

        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = True

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Qty")
        gvItemBulk.Columns(colQty).Width = 120
        gvItemBulk.Columns(colQty).IsVisible = False
        gvItemBulk.Columns(colQty).ReadOnly = True


        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 75
        gvItemBulk.Columns(colFat).IsVisible = False
        gvItemBulk.Columns(colFat).ReadOnly = True

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 75
        gvItemBulk.Columns(colSNF).IsVisible = False
        gvItemBulk.Columns(colSNF).ReadOnly = True

        gvItemBulk.Columns.Add(colFatKG, "FAT KG")
        gvItemBulk.Columns(colFatKG).Width = 75
        gvItemBulk.Columns(colFatKG).IsVisible = False
        gvItemBulk.Columns(colFatKG).ReadOnly = True

        gvItemBulk.Columns.Add(colSNFKG, "SNF KG")
        gvItemBulk.Columns(colSNFKG).Width = 75
        gvItemBulk.Columns(colSNFKG).IsVisible = False
        gvItemBulk.Columns(colSNFKG).ReadOnly = True

        gvItemBulk.Columns.Add(colGrossWeight, "Gross Weight")
        gvItemBulk.Columns(colGrossWeight).Width = 75
        gvItemBulk.Columns(colGrossWeight).ReadOnly = False

        'gvItemBulk.Columns.Add(colDipValue, "DIP Value")
        'gvItemBulk.Columns(colDipValue).Width = 75
        'gvItemBulk.Columns(colDipValue).ReadOnly = False



        gvItemBulk.Columns.Add(colTareWeight, "Tare Weight")
        gvItemBulk.Columns(colTareWeight).Width = 75
        gvItemBulk.Columns(colTareWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colNetWeight, "Net Weight")
        gvItemBulk.Columns(colNetWeight).Width = 75
        gvItemBulk.Columns(colNetWeight).ReadOnly = True


        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub

    Private Sub FrmMilkWeighment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub FrmMilkWeighment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If clsERPFuncationality.isCurrentUserMCC() Then
        '    chkBulkMilkProc.Enabled = False
        '    chkMccProc.IsChecked = True
        'Else
        '    chkBulkMilkProc.Enabled = True
        '    chkBulkMilkProc.IsChecked = True
        'End If
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        dtpTareWeight.Enabled = True
        If clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, "", NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), "", NavigatorType.Current)
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBulkMilkProc.ToggleStateChanged
        reset()
    End Sub

    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccProc.ToggleStateChanged
        reset()
    End Sub

    Private Sub fndGateEntryNoBulk__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNoBulk._MYValidating

        'Dim whrCls As String = String.Empty
        'whrCls = whrCls & " isPosted=1  " & IIf(chkBulkMilkProc.IsChecked, " and doc_type='BulkProc'", " and doc_type='MccProc'") & "  and gate_entry_no not in (select gate_entry_no from  TSPL_JWO_Weighment where gate_entry_no<>'" & fndGateEntryNoBulk.Value & "') "
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrCls = whrCls & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
        '    Else
        '        whrCls = whrCls & ""
        '    End If
        'End If
        'fndGateEntryNoBulk.Value = clsMilkGateEntry_JOW.getFinder(whrCls, fndGateEntryNoBulk.Value, isButtonClicked)
        'reset()
        'If clsCommon.myLen(fndGateEntryNoBulk.Value) > 0 Then
        '    If chkBulkMilkProc.IsChecked Then
        '        loadGateEntryDataBulk(fndGateEntryNoBulk.Value)
        '    Else
        '        loadGateEntryDataMcc(fndGateEntryNoBulk.Value)
        '    End If
        '    btnSave.Enabled = True
        'Else
        '    reset()
        'End If

    End Sub
    Sub loadGateEntryDataBulk(ByVal strGateEntryNo As String)

        '' checking that weighemnt has done against this gate entry no, if yes then show its already shown details
        Dim strWeighmentNo As String = String.Empty
        strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_JWO_Weighment where gate_entry_no='" & strGateEntryNo & "'"))
        If clsCommon.myLen(strWeighmentNo) > 0 Then
            loadData(strWeighmentNo, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
            Exit Sub
        End If

        Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Tanker", NavigatorType.Current)
        If obj IsNot Nothing Then
            fndGateEntryNoBulk.Value = obj.Gate_Entry_No
            lblGateEntryDateAndTimeValueBulk.Text = clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MM/yyyy hh:mm:ss tt")
            lblLocationCodeBulk.Text = obj.location_Code
            lblLocationNameBulk.Text = obj.Location_Desc
            lblVendorCodeBulk.Text = obj.Vendor_Code
            lblVendorNameBulk.Text = obj.Vendor_Desc
            lblTankerNoBulk.Text = obj.Tanker_No
            fndTankerNo.Value = obj.Tanker_No
            lblChallanNoBulk.Text = obj.Challan_No
            lblChallanDateBulk.Text = clsCommon.GetPrintDate(obj.Challan_Date, "dd/MM/yyyy")
            txtjobworklocation.Text = obj.JobWorkLocation
            lblJobworkLocation.Text = clsLocation.GetName(txtjobworklocation.Text, Nothing)
            gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
            gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
            gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
            gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
            gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
            gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
            gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
            gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'") > 0 Then
                lblStatusBulk.Text = "Done"
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_JWO_QUALITY_CHECK where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            Else
                lblStatusBulk.Text = "Not Done"
            End If
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnPrint.Enabled = False
            btnDelete.Enabled = False
            btnPost.Enabled = False
        End If
    End Sub
    Sub loadGateEntryDataMcc(ByVal strGateEntryNo As String)

        '' checking that weighemnt has done against this gate entry no, if yes then show its already shown details
        Dim strWeighmentNo As String = String.Empty
        strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_JWO_Weighment where gate_entry_no='" & strGateEntryNo & "'"))
        If clsCommon.myLen(strWeighmentNo) > 0 Then
            loadData(strWeighmentNo, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
            Exit Sub
        End If


        Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Sku_Receipt", NavigatorType.Current, False)
        If obj IsNot Nothing Then
            fndGateEntryNoBulk.Value = obj.Gate_Entry_No
            lblGateEntryDateAndTimeValueBulk.Text = clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MM/yyyy hh:mm:ss tt")
            lblLocationCodeBulk.Text = obj.location_Code
            lblLocationNameBulk.Text = obj.Location_Desc
            lblVendorCodeBulk.Text = obj.Vendor_Code
            lblVendorNameBulk.Text = obj.Vendor_Desc
            fndTankerNo.Value = obj.Tanker_No
            lblTankerNoBulk.Text = obj.Tanker_No
            lblChallanNoBulk.Text = obj.Challan_No
            lblChallanDateBulk.Text = clsCommon.GetPrintDate(obj.Challan_Date, "dd/MM/yyyy")
            gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
            gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
            gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
            gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
            gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
            gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
            gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
            gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'") > 0 Then
                lblStatusBulk.Text = "Done"
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_JWO_QUALITY_CHECK where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            Else
                lblStatusBulk.Text = "Not Done"
            End If
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnPrint.Enabled = False
            btnDelete.Enabled = False
            btnPost.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
        '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_Milk_quality_check where isposted=1 and weighment_no='" & fndDocNO.Value & "' ")) > 0 Then
        '        clsCommon.MyMessageBoxShow("This document is in use, you can not update it.")
        '    End If
        'End If
        If allowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_JWO_QUALITY_CHECK where isposted=1 and weighment_no='" & fndDocNO.Value & "' ")) > 0 Then
            clsCommon.MyMessageBoxShow("This document is in use, you can not delete it.")
        End If
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub

    Private Sub fndDocNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNO._MYNavigator
        If chkBothDoc.Checked Then
            loadData(fndDocNO.Value, "", NavType)
        Else
            loadData(fndDocNO.Value, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavType)
        End If

    End Sub

    Private Sub fndDocNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNO._MYValidating
        Dim strDocType As String = ""

        If Not chkBothDoc.Checked Then

            If chkBulkMilkProc.IsChecked Then
                strDocType = "Tanker"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "Sku_Receipt"
            Else
                fndDocNO.Value = ""
                clsCommon.MyMessageBoxShow("Please Select a Weighment Type i.e Bulk Procurement or Mcc Procurement")
                Exit Sub
            End If

        End If

        Dim whrcls As String = "  1=1  "

        If clsCommon.myLen(strDocType) > 0 Then
            whrcls = " doc_type='" & strDocType & "'"
        End If
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = whrcls & " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        'If chkPendingTare.Checked Then
        '    whrcls = whrcls & "  and isPosted=0 and gate_entry_no not in(select gate_entry_no from tspl_gate_out) "
        'End If
        If chkPendingTare.Checked Then
            whrcls = whrcls & " and isPosted=0 "
        End If
        fndDocNO.Value = clsMilkWeighment_JOW.getFinder(whrcls, fndDocNO.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNO.Value) > 0 Then
            loadData(fndDocNO.Value, strDocType, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Net Qty can not be 0")
            Exit Sub
        End If
        postData()
    End Sub

    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True

                If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
                    Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value))
                    If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) - intPart) > 0 Then
                        gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = intPart
                        Throw New Exception("Gross Weight must be integer")
                    End If
                    gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value, 0))
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                End If

                If e.Column Is gvItemBulk.Columns(colTareWeight) Then
                    Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value))
                    If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) - intPart) > 0 Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = intPart
                        Throw New Exception("Tare Weight must be integer")
                    End If
                    If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) < clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
                        Throw New Exception("Tare Weight Can not be more than Gross weight")
                    End If
                    If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) < 0 Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
                        Throw New Exception("Tare Weight Can not be negative or Zero")
                    End If
                    gvItemBulk.CurrentRow.Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 0))
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                End If
            End If
            isCellValueChangedOpen = False
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub SplitContainer3_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer3.Panel1.Paint

    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItemBulk.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItemBulk.Columns.Count - 1 Step ii + 1
                        gvItemBulk.Columns(ii).IsVisible = False
                        gvItemBulk.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItemBulk.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItemBulk.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItemBulk.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItemBulk.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkWeighment_JOW.ReverseAndUnpost(fndDocNO.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    loadData(fndDocNO.Value, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating

        Try


            Dim whrCls As String = String.Empty
            whrCls = whrCls & " isPosted=1  " & IIf(chkBulkMilkProc.IsChecked, " and doc_type='Tanker'", IIf(chkMccProc.IsChecked, " and doc_type='Sku_Receipt'", "")) & "  and gate_entry_no not in (select gate_entry_no from  TSPL_JWO_Weighment where gate_entry_no<>'" & fndGateEntryNoBulk.Value & "' " & IIf(chkPendingTare.Checked, " and  isPosted=1 and Tare_Weight>0 ", "") & " union all select gate_entry_no from tspl_gate_out ) "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = whrCls & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                Else
                    whrCls = whrCls & ""
                End If
            End If
            fndGateEntryNoBulk.Value = clsMilkGateEntry_JOW.getTankerFinder(whrCls, fndTankerNo.Value)
            'reset()
            If clsCommon.myLen(fndGateEntryNoBulk.Value) > 0 Then
                If chkBulkMilkProc.IsChecked Then
                    loadGateEntryDataBulk(fndGateEntryNoBulk.Value)
                Else
                    loadGateEntryDataMcc(fndGateEntryNoBulk.Value)
                End If
                'btnSave.Enabled = True
            Else
                reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub funPrint(ByVal strDocNo As String)

        'Try

        '    Dim Qry As String = "  select TSPL_LOCATION_MASTER_for_MCC.CST_No as Mcc_CstNo,TSPL_VENDOR_MASTER.CST as Ven_CSTNo,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.CST_No as Loc_Cst,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,"
        '    Qry += " Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER.Phone1 end  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Loc_Phone,TSPL_JWO_Weighment.Weighment_No as Doc_no,TSPL_LOCATION_MASTER.Location_Desc ,convert(varchar,TSPL_JWO_Weighment.Weighment_date,103) as Weighment_date ,TSPL_JWO_Weighment.Gate_Entry_No ,TSPL_JWO_Weighment.Doc_Type ,TSPL_JWO_Weighment.Date_And_Time ,TSPL_JWO_Weighment.Challan_No ,convert(varchar,TSPL_JWO_Weighment.Challan_Date,103) as Challan_Date ,TSPL_JWO_Weighment.Tanker_No ,TSPL_JWO_Weighment.isPosted ,convert(varchar,TSPL_JWO_Weighment.Posting_Date,103) as Posting_Date,TSPL_JWO_Weighment.Dispatched_From_Mcc,TSPL_LOCATION_MASTER_for_MCC.Location_Desc as Mcc_Name ,TSPL_JWO_Weighment.location_Code ,TSPL_JWO_Weighment.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_JWO_Weighment.Item_Code ,TSPL_ITEM_MASTER .Item_Desc ,TSPL_JWO_Weighment.Qty_In_Kg ,TSPL_JWO_Weighment.Created_By ,TSPL_JWO_Weighment.Created_Date ,TSPL_JWO_Weighment.Modify_By ,TSPL_JWO_Weighment.Modify_Date ,TSPL_JWO_Weighment.comp_code ,TSPL_JWO_Weighment.Gross_Weight ,TSPL_JWO_Weighment.Dip_Value ,TSPL_JWO_Weighment.Tare_Weight ,TSPL_JWO_Weighment.Net_Weight ,case when TSPL_JWO_Weighment.status=1 then 'Not Done' else 'Done' end as status ,TSPL_JWO_Weighment.Weighment_Slip_No ,TSPL_JWO_Weighment.UOM ,TSPL_JWO_Weighment.Sent_to_QC_By ,convert(varchar,TSPL_JWO_Weighment.Sent_To_QC_Date,103) as Sent_To_QC_Date ,TSPL_JWO_Weighment.QC_Done_By ,TSPL_JWO_Weighment.QC_Done_Date,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2  as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as comp_add3,TSPL_COMPANY_MASTER.Tin_No as Comp_tinNo,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Pincode as Comp_PinCode,TSPL_COMPANY_MASTER.Email as Comp_Email,Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_COMPANY_MASTER.Phone1 end "
        '    Qry += " +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Comp_Phone ,TSPL_LOCATION_MASTER_for_MCC.Location_Desc  as MCC_Desc ,TSPL_LOCATION_MASTER_for_MCC.Add1 as Mcc_Add1,TSPL_LOCATION_MASTER_for_MCC.Add2 as mcc_Add2,TSPL_LOCATION_MASTER_for_MCC.Add3 as Mcc_Add3,TSPL_LOCATION_MASTER_for_MCC.TIN_No as MCC_TinNo,Case when len(ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone1,''))>0 and TSPL_LOCATION_MASTER_for_MCC.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER_for_MCC.Phone1 end "
        '    Qry += "   +  Case When   ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER_for_MCC.Phone2 Else'' End as MCC_Phone,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.Add1 as Vend_Add1,TSPL_VENDOR_MASTER.Add2 as Ven_Add2,TSPL_VENDOR_MASTER.Add3 as Ven_Add3,TSPL_VENDOR_MASTER.Tin_No as ven_tinno,Case when len(ISNULL(TSPL_VENDOR_MASTER.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_VENDOR_MASTER.Phone1 end "
        '    Qry += "  +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phone"


        '    Qry += "  from TSPL_JWO_Weighment"
        '    Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_JWO_Weighment.comp_code "
        '    Qry += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_for_MCC on TSPL_LOCATION_MASTER_for_MCC.Location_Code =TSPL_JWO_Weighment.Dispatched_From_Mcc "
        '    Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_JWO_Weighment.Vendor_Code "
        '    Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_JWO_Weighment.Item_Code"
        '    Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_JWO_Weighment.location_Code  where 2=2"

        '    Qry += " and TSPL_JWO_Weighment.Weighment_No ='" + strDocNo + "'"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        '    If dt.Rows.Count > 0 Then

        '        frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptWeighmetBulk", "Weighment", "rptCompanyAddress.rpt")
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndDocNO.Value) <= 0 Then
            myMessages.blankValue("Weighment not found to Print")
        Else
            funPrint(fndDocNO.Value)
        End If
    End Sub

    Private Sub gvItemBulk_Click(sender As Object, e As EventArgs) Handles gvItemBulk.Click

    End Sub
End Class
