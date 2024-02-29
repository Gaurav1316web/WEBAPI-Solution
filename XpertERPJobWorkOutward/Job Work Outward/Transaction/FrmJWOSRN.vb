Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmJWOSRN
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim isCellValueChangedOpen As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isNewEntery As Boolean = True
    Public Const colSNo As String = "SLNO"
    Public Const colICode As String = "ItemCode"
    Public Const colIName As String = "ItemDesc"
    Public Const colHSNCode As String = "colHSNCode"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colEstimateQty As String = "colEstimateQty"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colJobRate As String = "colJobRate"
    Public Const colJobAmt As String = "colJobAmt"
    Public Const colRate As String = "colRate"
    Public Const colAmt As String = "colAmt"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsJWOSRNHead = Nothing
    Dim settJobWorkOutwardComsumeItemAccordingToBOM As Boolean = False
    Dim allowMilkJobworkOutowordWithAvgFatSNFRate As Boolean = False
#End Region

    Private Sub FrmJobMilkSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim coll = New Dictionary(Of String, String)()
        coll.Add("TransferFATKG", "decimal(18,3) null")
        coll.Add("TransferFATRate", "decimal(18,10) null")
        coll.Add("TransferFATAmt", "decimal(18,2) null")
        coll.Add("TransferSNFKG", "decimal(18,3) null")
        coll.Add("TransferSNFRate", "decimal(18,10) null")
        coll.Add("TransferSNFAmt", "decimal(18,2) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_JWO_SRN_HEAD", coll, Nothing, True, False)



        SetUserMgmtNew()
        settJobWorkOutwardComsumeItemAccordingToBOM = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.JobWorkOutwardComsumeItemAccordingToBOM, clsFixedParameterCode.JobWorkOutwardComsumeItemAccordingToBOM, Nothing)) = 1)
        allowMilkJobworkOutowordWithAvgFatSNFRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.allowMilkJWOutowordWithAvgFatSNFRate, clsFixedParameterCode.allowMilkJWOutowordWithAvgFatSNFRate, Nothing)) = 1, True, False)
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        Reset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        lblVendor.Visible = True
        RadPageView1.SelectedPage = pvpCustomFields
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If

    End Sub

    Sub Reset()
        BlankAllControl()
        isNewEntery = True
        btnSave.Enabled = True
        btnPrint.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        grpGateEntryType.Enabled = True
        txtLocation.Enabled = True
        isCellValueChangedOpen = False
    End Sub

    Sub BlankAllControl()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtGateEntryNo.Text = ""
        txtLocation.Value = clsGateEntry.getUsersDefaultLocation()
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        txtJobLocation.Value = ""
        lblJobLocation.Text = ""
        txtVendor.Value = ""
        lblVendorName.Text = ""
        txtVehicleNo.Value = ""
        lblUnloadingNo.Text = ""
        txtGateEntryNo.Text = ""
        txtGateEntryDate.Value = txtDate.Value
        txtChallanNo.Text = ""
        txtChallanDate.Value = txtDate.Value
        lblTotalAmt.Text = ""
        lblJobAmt.Text = ""
        txtJWOEstimate.Value = ""
        loadBlankItemGrid()
        UcAttachment1.BlankAllControls()
    End Sub

    Sub loadBlankItemGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        gvItem.Columns.Add(colSNo, "SNo")
        gvItem.Columns(colSNo).Width = 50
        gvItem.Columns(colSNo).ReadOnly = True

        gvItem.Columns.Add(colICode, "Item Code")
        gvItem.Columns(colICode).Width = 100
        gvItem.Columns(colICode).ReadOnly = False

        gvItem.Columns.Add(colIName, "Item Desc")
        gvItem.Columns(colIName).Width = 320
        gvItem.Columns(colIName).ReadOnly = True

        gvItem.Columns.Add(colHSNCode, "HSN Code")
        gvItem.Columns(colHSNCode).Width = 100
        gvItem.Columns(colHSNCode).ReadOnly = True

        gvItem.Columns.Add(colPriceCode, "Price Code")
        gvItem.Columns(colPriceCode).IsVisible = False
        gvItem.Columns(colPriceCode).ReadOnly = True

        gvItem.Columns.Add(colGrossWeight, "Gross Weight")
        gvItem.Columns(colGrossWeight).Width = 100
        gvItem.Columns(colGrossWeight).ReadOnly = True
        gvItem.Columns(colGrossWeight).TextAlignment = ContentAlignment.MiddleRight
        gvItem.Columns(colGrossWeight).IsVisible = rbtnTanker.IsChecked

        gvItem.Columns.Add(colTareWeight, "Tare Weight")
        gvItem.Columns(colTareWeight).Width = 100
        gvItem.Columns(colTareWeight).ReadOnly = True
        gvItem.Columns(colTareWeight).TextAlignment = ContentAlignment.MiddleRight
        gvItem.Columns(colTareWeight).IsVisible = rbtnTanker.IsChecked

        gvItem.Columns.Add(colNetWeight, "Net Weight")
        gvItem.Columns(colNetWeight).Width = 100
        gvItem.Columns(colNetWeight).ReadOnly = True
        gvItem.Columns(colNetWeight).TextAlignment = ContentAlignment.MiddleRight
        gvItem.Columns(colNetWeight).IsVisible = rbtnTanker.IsChecked

        gvItem.Columns.Add(colEstimateQty, "Estimate Qty")
        gvItem.Columns(colEstimateQty).Width = 100
        gvItem.Columns(colEstimateQty).ReadOnly = True
        gvItem.Columns(colEstimateQty).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 100
        gvItem.Columns(colQty).ReadOnly = (rbtnTanker.IsChecked OrElse chkSKU.IsChecked)
        gvItem.Columns(colQty).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 100
        gvItem.Columns(colUOM).ReadOnly = False

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        gvItem.Columns(colFat).ReadOnly = rbtnTanker.IsChecked
        gvItem.Columns(colFat).IsVisible = True
        gvItem.Columns(colFat).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        gvItem.Columns(colSNF).ReadOnly = rbtnTanker.IsChecked
        gvItem.Columns(colSNF).IsVisible = True
        gvItem.Columns(colSNF).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colRate, "Rate")
        gvItem.Columns(colRate).Width = 100
        gvItem.Columns(colRate).ReadOnly = allowMilkJobworkOutowordWithAvgFatSNFRate
        gvItem.Columns(colRate).IsVisible = Not allowMilkJobworkOutowordWithAvgFatSNFRate
        gvItem.Columns(colRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colAmt, "Amount")
        gvItem.Columns(colAmt).Width = 200
        gvItem.Columns(colAmt).ReadOnly = True
        gvItem.Columns(colAmt).IsVisible = Not allowMilkJobworkOutowordWithAvgFatSNFRate
        gvItem.Columns(colAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colJobRate, "Job Work Rate")
        gvItem.Columns(colJobRate).Width = 100
        gvItem.Columns(colJobRate).ReadOnly = True
        gvItem.Columns(colJobRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colJobAmt, "Job Work Amount")
        gvItem.Columns(colJobAmt).Width = 200
        gvItem.Columns(colJobAmt).ReadOnly = True
        gvItem.Columns(colJobAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSNo).Value = "1"

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = True
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True
        gvItem.AllowColumnReorder = True
        ReStoreGridLayout()
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            AllowToSave()
            obj = New clsJWOSRNHead()
            obj.Document_No = txtDocNo.Value
            obj.Document_Date = txtDate.Value
            If rbtnTanker.IsChecked Then
                obj.Document_Type = clsDocTransactionType.Tanker
            ElseIf rbtnManual.IsChecked Then
                obj.Document_Type = clsDocTransactionType.Manual
            Else
                obj.Document_Type = clsDocTransactionType.Sku_Receipt
            End If
            obj.Loc_Code = txtLocation.Value
            obj.Job_Loc_Code = txtJobLocation.Value
            obj.Vendor_Code = txtVendor.Value
            obj.Tanker_No = txtVehicleNo.Value
            obj.Gate_Entry_No = txtGateEntryNo.Text
            If chkSKU.IsChecked Then
                obj.Against_Gate_Entry_No = txtGateEntryNo.Text
            End If
            obj.Against_Estimate = txtJWOEstimate.Value
            obj.Gate_Entry_Date = txtGateEntryDate.Value
            obj.Challan_No = txtChallanNo.Text
            obj.Challan_Date = txtChallanDate.Value
            obj.Document_Amt = clsCommon.myCdbl(lblTotalAmt.Text)
            obj.Total_Job_Amt = clsCommon.myCdbl(lblJobAmt.Text)
            obj.Unloading_No = lblUnloadingNo.Text
            obj.Arr = New List(Of clsJWOSRNDetail)
            For ii As Integer = 0 To gvItem.RowCount - 1
                Dim objtr As New clsJWOSRNDetail
                objtr.SNo = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNo).Value)
                objtr.Item_Code = clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value)
                objtr.UOM = clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value)
                objtr.Gross_Weight = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colGrossWeight).Value)
                objtr.Tare_Weight = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTareWeight).Value)
                objtr.Net_Weight = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetWeight).Value)
                objtr.Estimate_Qty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colEstimateQty).Value)
                objtr.Qty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colQty).Value)
                objtr.FAT_Per = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFat).Value)
                objtr.SNF_Per = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNF).Value)
                objtr.Job_Price_code = clsCommon.myCstr(gvItem.Rows(ii).Cells(colPriceCode).Value)
                objtr.Rate = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colRate).Value)
                objtr.Amount = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colAmt).Value)
                objtr.Job_Rate = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colJobRate).Value)
                objtr.Job_Amount = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colJobAmt).Value)
                objtr.arrBatchItem = TryCast(gvItem.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next
            If clsJWOSRNHead.SaveData(obj, isNewEntery) Then
                UcAttachment1.SaveData(obj.Document_No)
                clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Document_No, NavigatorType.Current)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please Select location")
            End If
            If clsCommon.myLen(txtJobLocation.Value) <= 0 Then
                txtJobLocation.Focus()
                Throw New Exception("Please Select Job Work location")
            End If
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                txtVendor.Focus()
                Throw New Exception("Please Select Vendor")
            End If
            If clsCommon.myLen(txtVehicleNo.Value) <= 0 Then
                txtVehicleNo.Focus()
                Throw New Exception("Please Select vehicel No /Tanker No")
            End If
            If clsCommon.myLen(txtGateEntryNo.Text) <= 0 Then
                txtGateEntryNo.Focus()
                Throw New Exception("Please Enter Gate entry No")
            End If
            If clsCommon.myLen(txtChallanNo.Text) <= 0 Then
                txtChallanNo.Focus()
                Throw New Exception("Please enter Challan no")
            End If
            If clsCommon.myLen(txtJWOEstimate.Value) <= 0 Then
                txtJWOEstimate.Focus()
                Throw New Exception("Please select JWO Estimate")
            End If
            For ii As Integer = 0 To gvItem.RowCount - 1
                UpdateCurrentRow(ii)
                If clsCommon.myLen(gvItem.Rows(ii).Cells(colICode).Value) > 0 Then
                    If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(txtDate.Value)) Then
                        'Dim HSNCode As String = clsItemMaster.GetItemHSNCode(gvItem.Rows(ii).Cells(colICode).Value, Nothing)
                        'gvItem.Rows(ii).Cells(colHSNCode).Value = HSNCode
                        If clsCommon.myLen(gvItem.Rows(ii).Cells(colHSNCode).Value) <= 0 Then
                            Throw New Exception("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If
                    If clsCommon.myLen(gvItem.Rows(ii).Cells(colPriceCode).Value) <= 0 Then
                        Throw New Exception("Error at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " job work charges not found")
                    End If
                    Dim dblQty As Decimal = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colQty).Value)
                    If dblQty <= 0 Then
                        Throw New Exception("Error at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " Quantity should be greater than zero")
                    End If
                    If Not allowMilkJobworkOutowordWithAvgFatSNFRate Then
                        If clsCommon.myCdbl(gvItem.Rows(ii).Cells(colRate).Value) <= 0 Then
                            Throw New Exception(" Please enter Rate at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "")
                        End If
                    End If

                    If dblQty > 0 AndAlso clsCommon.myCBool(clsItemMaster.IsBatchItem(gvItem.Rows(ii).Cells(colICode).Value)) Then
                        Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gvItem.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventory In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If tQty <> dblQty Then
                                Throw New Exception("Item : " + clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value) + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    End If
                End If
            Next
            UpdateAllTotals()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItem.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gvItem"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItem.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItem.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gvItem.MasterTemplate.FilterDescriptors.Clear()
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ''richa Against Ticket no .BM00000003725 on 05/08/2014
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvRange", objCommonVar.CurrentUserCode)
        ''=====================================================
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
                obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub FrmJobMilkSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter SRN No To delete ")
        Else
            If myMessages.deleteConfirm() Then
                If clsJWOSRNHead.deleteData(txtDocNo.Value) Then
                    UcAttachment1.funDelete(txtDocNo.Value)
                    Reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub

    Sub printData(ByVal SRNNo As String)
        Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        Dim strCompany_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
        Dim strCompanyLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  'Registered Office : '+ tspl_company_master.Comp_Name + Case when len(tspl_company_master.Add1) >0 then ','+tspl_company_master.Add1 else '' end + case when len( tspl_company_master.Add2) >0 then ',' + tspl_company_master.Add2 else '' end + case when len(tspl_company_master.Add3) >0  then ','+tspl_company_master.Add3 else '' end + case when len(tspl_company_master.Fax) >0 then ', FAX :'+ tspl_company_master.Fax else '' end + case when len(tspl_company_master.Email) >0 then ', Email : '+tspl_company_master.Email else '' end + case when len( tspl_company_master.CINNo) >0 then  'CIN NO : '+tspl_company_master.CINNo else '' end + case when len(tspl_company_master.Pan_No) > 0 then ', PAN No : '+ tspl_company_master.Pan_No else '' end as Company_Address from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
        If clsCommon.myLen(SRNNo) > 0 Then
            'Dim strQuery As String = " select    case when isnull(JOB_WORK_VENDOR_MASTER.Add1,'')<>'' then JOB_WORK_VENDOR_MASTER.Add1  else '' end + case when  isnull(JOB_WORK_VENDOR_MASTER.Add2,'')<>'' then  ','+JOB_WORK_VENDOR_MASTER.Add2 else '' end + case when isnull(JOB_WORK_VENDOR_MASTER.Add3,'')<>'' then ',' + JOB_WORK_VENDOR_MASTER.Add3   else '' end as JobWork_Vendor_Add" & _
            '                         ",JOB_WORK_VENDOR_MASTER.GSTFinalNo as JobWork_Vendor_GSTIN,Job_Work_VendorState_Master.GST_STATE_Code as JobWork_Vendor_Gst_State," & _
            '                        " TSPL_LOCATION_MASTER_From_loc.GSTNO as From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code as From_Loc_GST_STATE_Code,    TSPL_LOCATION_MASTER_To_loc.GSTNO as To_Location_GSTIN,TSPL_STATE_MASTER_To_loc.GST_STATE_Code as     To_Loc_GST_State_Code,'" + strCompany_Name + "' as Comp_Name, '" + strCompanyLocation + "' as Company_Address ,TSPL_JWO_SRN_DETAIL.Document_No ,convert(varchar,TSPL_JWO_SRN_HEAD.Document_Date,103) as  Document_Date,TSPL_JWO_SRN_DETAIL.SNO,TSPL_JWO_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_JWO_SRN_DETAIL.UOM,TSPL_JWO_SRN_DETAIL.Gross_Weight,TSPL_JWO_SRN_DETAIL.Tare_Weight,TSPL_JWO_SRN_DETAIL.Net_Weight,TSPL_JWO_SRN_DETAIL.Qty,TSPL_JWO_SRN_DETAIL.Rate,TSPL_JWO_SRN_DETAIL.Amount,TSPL_JWO_SRN_HEAD.Document_Type,TSPL_JWO_SRN_HEAD.Loc_Code,TSPL_JWO_SRN_HEAD.Job_Loc_Code,TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_JWO_SRN_HEAD.Challan_No,convert(varchar,TSPL_JWO_SRN_HEAD.Challan_Date,103) asChallan_Date, TSPL_JWO_SRN_HEAD.Tanker_No,TSPL_JWO_SRN_HEAD.Gate_Entry_No, convert (varchar,TSPL_JWO_SRN_HEAD.Gate_Entry_Date ,103) as Gate_Entry_Date,TSPL_JWO_SRN_HEAD.Posted , TSPL_JWO_SRN_HEAD.Posted_Date,TSPL_JWO_SRN_HEAD.Created_By,TSPL_JWO_SRN_HEAD.Modified_By, tspl_user_master_Modified_By.User_Name as Modified_Name,tspl_user_master_Created_By.User_Name as Created_Name ,TSPL_JWO_SRN_HEAD.Document_Amt,TSPL_JWO_SRN_HEAD.Unloading_No, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Add1 + case when len(TSPL_VENDOR_MASTER.Add2) >0 then ','+ TSPL_VENDOR_MASTER.Add2 else '' end + case when len(TSPL_VENDOR_MASTER.Add3)>0 then ','+ TSPL_VENDOR_MASTER.Add3 else '' end as Vendor_Address,TSPL_CITY_MASTER_Vendor.City_Code as Vendor_City_Code,TSPL_CITY_MASTER_Vendor.City_Name as Vendor_City_Name,TSPL_VENDOR_MASTER.GSTFinalNo , TSPL_STATE_MASTER_Vendor.STATE_NAME as Vendor_State_Name, TSPL_STATE_MASTER_Vendor.STATE_CODE as Vendor_State_Code, TSPL_STATE_MASTER_Vendor.GST_STATE_Code " & _
            '                         "  ,    TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '     end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len    (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len     (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address , TSPL_LOCATION_MASTER_To_loc.Add1 + case when len(TSPL_LOCATION_MASTER_To_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_To_loc. Add2 else ' ' end + Case when len( TSPL_LOCATION_MASTER_To_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_To_loc. Add3 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_To_loc.Add4 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_To_loc.City_Code else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.state) > 0 then ','+ TSPL_STATE_MASTER_To_loc.STATE_NAME else ''  end + Case when len(TSPL_LOCATION_MASTER_To_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_To_loc.Pin_Code,103) else '' end  as To_Location_Address from TSPL_JWO_SRN_DETAIL left outer join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No =TSPL_JWO_SRN_HEAD.Document_No left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_JWO_SRN_HEAD.Vendor_Code left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_Vendor on TSPL_CITY_MASTER_Vendor.city_code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.State_Code " & _
            '                         " left outer join tspl_user_master as tspl_user_master_Modified_By on tspl_user_master_Modified_By.User_Code =TSPL_JWO_SRN_HEAD.Modified_By " & _
            '                         " left outer join tspl_user_master as tspl_user_master_Created_By on tspl_user_master_Created_By.User_Code =TSPL_JWO_SRN_HEAD.Created_By " & _
            '                         " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_JWO_SRN_HEAD.loc_Code   left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_To_loc on TSPL_LOCATION_MASTER_To_loc.Location_Code =TSPL_JWO_SRN_HEAD.Job_Loc_Code    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_loc on  TSPL_STATE_MASTER_To_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State   left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_To_Location on TSPL_CITY_MASTER_To_Location.City_Code =TSPL_LOCATION_MASTER_To_loc.City_Code " & _
            '                         " left outer join TSPL_LOCATION_MASTER AS JOB_WORK_LOCATION_MASTER ON TSPL_JWO_SRN_HEAD.Job_Loc_Code =JOB_WORK_LOCATION_MASTER.Location_Code " & _
            '                        " LEFT OUTER JOIN TSPL_VENDOR_MASTER AS JOB_WORK_VENDOR_MASTER ON JOB_WORK_LOCATION_MASTER.Jobwork_Vendor=JOB_WORK_VENDOR_MASTER.Vendor_Code" & _
            '                            " LEFT OUTER JOIN TSPL_STATE_MASTER AS Job_Work_VendorState_Master on JOB_WORK_VENDOR_MASTER.State_Code=Job_Work_VendorState_Master.STATE_CODE " & _
            '                         "  where  TSPL_JWO_SRN_HEAD.Document_No = '" & SRNNo & "' "

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            'Dim dtDocdate As Date?
            'dtDocdate = Nothing
            'dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
            'frmCrystalReportViewer.funreport(CrystalReportFolder.ServiceReport, dt, "rptJobWorkSRN", "Job Work SRN", dtDocdate)
            Dim strQuery As String = " select TSPL_COMPANY_MASTER.Logo_Img,TSPL_LOCATION_MASTER_From_loc.Location_Desc as FromLoaction,case when isnull(JOB_WORK_VENDOR_MASTER.Add1,'')<>'' then JOB_WORK_VENDOR_MASTER.Add1  else '' end + case when  isnull(JOB_WORK_VENDOR_MASTER.Add2,'')<>'' then  ','+JOB_WORK_VENDOR_MASTER.Add2 else '' end + case when isnull(JOB_WORK_VENDOR_MASTER.Add3,'')<>'' then ',' + JOB_WORK_VENDOR_MASTER.Add3   else '' end as JobWork_Vendor_Add" &
                            ",JOB_WORK_VENDOR_MASTER.GSTFinalNo as JobWork_Vendor_GSTIN,Job_Work_VendorState_Master.GST_STATE_Code as JobWork_Vendor_Gst_State," &
                           " TSPL_LOCATION_MASTER_From_loc.GSTNO as From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code as From_Loc_GST_STATE_Code,    TSPL_LOCATION_MASTER_To_loc.GSTNO as To_Location_GSTIN,TSPL_STATE_MASTER_To_loc.GST_STATE_Code as     To_Loc_GST_State_Code,'" + strCompany_Name + "' as Comp_Name, '" + strCompanyLocation + "' as Company_Address ,TSPL_JWO_SRN_DETAIL.Document_No,convert(varchar,TSPL_JWO_SRN_HEAD.Document_Date,103) as  Document_Date,TSPL_JWO_SRN_DETAIL.SNO,TSPL_JWO_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_JWO_SRN_DETAIL.Gross_Weight,TSPL_JWO_SRN_DETAIL.Tare_Weight,TSPL_JWO_SRN_DETAIL.Net_Weight" &
                           ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.UOM ELSE TSPL_JWO_SRN_DETAIL.UOM END AS UOM,case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_JWO_SRN_DETAIL.Qty end as Qty,TSPL_JWO_SRN_DETAIL.Rate,case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then COALESCE(BI.QTY,0)* (COALESCE(TSPL_JWO_SRN_DETAIL.Amount,0)/TSPL_JWO_SRN_DETAIL.Qty) else COALESCE(TSPL_JWO_SRN_DETAIL.Amount,0) end  AS Amount,case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END AS Item_BatchNo,TSPL_ITEM_MASTER.Is_Batch_Item" &
                           ",TSPL_JWO_SRN_HEAD.Document_Type,TSPL_JWO_SRN_HEAD.Loc_Code,TSPL_JWO_SRN_HEAD.Job_Loc_Code,TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_JWO_SRN_HEAD.Challan_No,convert(varchar,TSPL_JWO_SRN_HEAD.Challan_Date,103) asChallan_Date, TSPL_JWO_SRN_HEAD.Tanker_No,TSPL_JWO_SRN_HEAD.Gate_Entry_No, convert (varchar,TSPL_JWO_SRN_HEAD.Gate_Entry_Date ,103) as Gate_Entry_Date,TSPL_JWO_SRN_HEAD.Posted , TSPL_JWO_SRN_HEAD.Posted_Date,TSPL_JWO_SRN_HEAD.Created_By,TSPL_JWO_SRN_HEAD.Modified_By, case when TSPL_JWO_SRN_HEAD.Posted=1 then tspl_user_master_Modified_By.User_Name else '' end as PostedBy,tspl_user_master_Created_By.User_Name as Created_Name ,TSPL_JWO_SRN_HEAD.Document_Amt,TSPL_JWO_SRN_HEAD.Unloading_No, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Add1 + case when len(TSPL_VENDOR_MASTER.Add2) >0 then ','+ TSPL_VENDOR_MASTER.Add2 else '' end + case when len(TSPL_VENDOR_MASTER.Add3)>0 then ','+ TSPL_VENDOR_MASTER.Add3 else '' end as Vendor_Address,TSPL_CITY_MASTER_Vendor.City_Code as Vendor_City_Code,TSPL_CITY_MASTER_Vendor.City_Name as Vendor_City_Name,TSPL_VENDOR_MASTER.GSTFinalNo , TSPL_STATE_MASTER_Vendor.STATE_NAME as Vendor_State_Name, TSPL_STATE_MASTER_Vendor.STATE_CODE as Vendor_State_Code, TSPL_STATE_MASTER_Vendor.GST_STATE_Code " &
                            "  ,    TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '     end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len    (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len     (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address , TSPL_LOCATION_MASTER_To_loc.Add1 + case when len(TSPL_LOCATION_MASTER_To_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_To_loc. Add2 else ' ' end + Case when len( TSPL_LOCATION_MASTER_To_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_To_loc. Add3 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_To_loc.Add4 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_To_loc.City_Code else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.state) > 0 then ','+ TSPL_STATE_MASTER_To_loc.STATE_NAME else ''  end + Case when len(TSPL_LOCATION_MASTER_To_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_To_loc.Pin_Code,103) else '' end  as To_Location_Address ,QCPOSTUSER.User_Name as QC_Post_User,convert(varchar,TSPL_JWO_GATE_ENTRY.QC_Post_Date,103) as QC_Post_Date from TSPL_JWO_SRN_DETAIL left outer join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No =TSPL_JWO_SRN_HEAD.Document_No left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_JWO_SRN_HEAD.Vendor_Code left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_Vendor on TSPL_CITY_MASTER_Vendor.city_code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.State_Code " &
                            " left outer join tspl_user_master as tspl_user_master_Modified_By on tspl_user_master_Modified_By.User_Code =TSPL_JWO_SRN_HEAD.Modified_By " &
                            " left outer join tspl_user_master as tspl_user_master_Created_By on tspl_user_master_Created_By.User_Code =TSPL_JWO_SRN_HEAD.Created_By " &
                            " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_JWO_SRN_HEAD.loc_Code   left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_To_loc on TSPL_LOCATION_MASTER_To_loc.Location_Code =TSPL_JWO_SRN_HEAD.Job_Loc_Code    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_loc on  TSPL_STATE_MASTER_To_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State   left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_To_Location on TSPL_CITY_MASTER_To_Location.City_Code =TSPL_LOCATION_MASTER_To_loc.City_Code " &
                            " left outer join TSPL_LOCATION_MASTER AS JOB_WORK_LOCATION_MASTER ON TSPL_JWO_SRN_HEAD.Job_Loc_Code =JOB_WORK_LOCATION_MASTER.Location_Code " &
                            " LEFT OUTER JOIN TSPL_VENDOR_MASTER AS JOB_WORK_VENDOR_MASTER ON JOB_WORK_LOCATION_MASTER.Jobwork_Vendor=JOB_WORK_VENDOR_MASTER.Vendor_Code" &
                            " LEFT OUTER JOIN TSPL_STATE_MASTER AS Job_Work_VendorState_Master on JOB_WORK_VENDOR_MASTER.State_Code=Job_Work_VendorState_Master.STATE_CODE " &
                            " Left Join TSPL_BATCH_ITEM AS BI ON TSPL_JWO_SRN_DETAIL.Document_No=BI.Document_Code AND TSPL_JWO_SRN_DETAIL.SNo=BI.Parent_Line_No AND  BI.Document_Type='JWO-SRN' " &
            " LEFT JOIN TSPL_JWO_GATE_ENTRY ON TSPL_JWO_GATE_ENTRY.Gate_Entry_No=TSPL_JWO_SRN_HEAD.Against_Gate_Entry_No LEFT join TSPL_USER_MASTER as QCPOSTUSER ON QCPOSTUSER.USER_CODE= TSPL_JWO_GATE_ENTRY.QC_POST_BY" &
            " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_JWO_GATE_ENTRY.Comp_Code " &
            "  where  TSPL_JWO_SRN_HEAD.Document_No = '" & SRNNo & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "RptJWOSRN", "JOB WORK SRN", dtDocdate)
                frmCRV = Nothing
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select an SRN to print")
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData(txtDocNo.Value)
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                clsJWOSRNHead.postData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strSrnNo As String, ByVal nav As NavigatorType)
        Try
            Reset()
            loadBlankItemGrid()
            isInsideLoadData = True
            obj = clsJWOSRNHead.GetData(strSrnNo, nav)
            If obj IsNot Nothing Then
                isNewEntery = False
                btnSave.Text = "Update"
                grpGateEntryType.Enabled = False
                txtLocation.Enabled = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                If clsCommon.CompairString(obj.Document_Type, clsDocTransactionType.Tanker) = CompairStringResult.Equal Then
                    rbtnTanker.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Document_Type, clsDocTransactionType.Manual) = CompairStringResult.Equal Then
                    rbtnManual.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Document_Type, clsDocTransactionType.Sku_Receipt) = CompairStringResult.Equal Then
                    chkSKU.IsChecked = True
                End If
                txtLocation.Value = obj.Loc_Code
                lblLocationDesc.Text = obj.Loc_Name
                txtJobLocation.Value = obj.Job_Loc_Code
                lblJobLocation.Text = obj.Job_Loc_Name
                txtVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtVehicleNo.Value = obj.Tanker_No
                txtGateEntryNo.Text = obj.Gate_Entry_No
                txtGateEntryDate.Value = obj.Gate_Entry_Date
                txtChallanNo.Text = obj.Challan_No
                txtChallanDate.Value = obj.Challan_Date
                lblTotalAmt.Text = clsCommon.myCstr(obj.Document_Amt)
                lblJobAmt.Text = clsCommon.myCstr(obj.Total_Job_Amt)
                lblUnloadingNo.Text = obj.Unloading_No
                txtJWOEstimate.Value = obj.Against_Estimate
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsJWOSRNDetail In obj.Arr
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colSNo).Value = objtr.SNo
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colICode).Value = objtr.Item_Code
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colIName).Value = objtr.Item_Desc
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colHSNCode).Value = objtr.HSN_Code
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colUOM).Value = objtr.UOM
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colGrossWeight).Value = objtr.Gross_Weight
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colTareWeight).Value = objtr.Tare_Weight
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colNetWeight).Value = objtr.Net_Weight
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colEstimateQty).Value = objtr.Estimate_Qty
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colQty).Value = objtr.Qty
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colFat).Value = objtr.FAT_Per
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colSNF).Value = objtr.SNF_Per
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colPriceCode).Value = objtr.Job_Price_code
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colRate).Value = objtr.Rate
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colAmt).Value = objtr.Amount
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colJobRate).Value = objtr.Job_Rate
                        gvItem.Rows(gvItem.RowCount - 1).Cells(colJobAmt).Value = objtr.Job_Amount
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colICode).Tag = objtr.arrBatchItem
                        gvItem.Rows.AddNew()
                    Next
                End If

                lblPending.Status = obj.Posted
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                btnPrint.Enabled = True
                UcAttachment1.LoadData(obj.Document_No)
            Else
                Reset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub fndSRNNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "  loc_code in  (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            txtDocNo.Value = clsJWOSRNHead.getFinder(whrCls, txtDocNo.Value, isButtonClicked)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicleNo._MYValidating
        Try
            Dim strDocNo As String = txtDocNo.Value
            If rbtnTanker.IsChecked Then
                Dim qry As String = "select TSPL_JWO_GATE_ENTRY.Tanker_No as TankerNo,TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [GateEntryNo] ,TSPL_JWO_GATE_ENTRY.Doc_Type as [Doc Type] ,TSPL_JWO_GATE_ENTRY.Date_And_Time as [Gate Entry Date And Time],  TSPL_JWO_Weighment.Weighment_No as [Weighment No],TSPL_JWO_Weighment.Weighment_Date as [Weighment Date],TSPL_JWO_QUALITY_CHECK.QC_No as [QC No],TSPL_JWO_QUALITY_CHECK.Qc_In_Date_Time as [QC Date Time]  ,TSPL_JWO_GATE_ENTRY.Challan_No as [Challan No] ,TSPL_JWO_GATE_ENTRY.Challan_Date as [Challan Date]  , case when isnull (TSPL_JWO_GATE_ENTRY.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_JWO_GATE_ENTRY.Posting_Date as [Posting Date] ,TSPL_JWO_GATE_ENTRY.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_JWO_GATE_ENTRY.location_Code as [Location Code] ,TSPL_JWO_GATE_ENTRY.Location_Desc as [Location Desc] ,TSPL_JWO_GATE_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_JWO_GATE_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_GATE_ENTRY.Item_Code as [Item Code],TSPL_JWO_GATE_ENTRY.UOM ,TSPL_JWO_GATE_ENTRY.Item_Desc as [Item Desc] ,TSPL_JWO_GATE_ENTRY.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_GATE_ENTRY.snf_Per as [SNF %] ,TSPL_JWO_GATE_ENTRY.fat_per as [FAT %] ,TSPL_JWO_GATE_ENTRY.Created_By as [Created By] ,TSPL_JWO_GATE_ENTRY.Created_Date as [Created Date] ,TSPL_JWO_GATE_ENTRY.Modify_By as [Modify By] ,TSPL_JWO_GATE_ENTRY.Modify_Date as [Modify Date] ,TSPL_JWO_GATE_ENTRY.comp_code as [Company Code] , case when ISNULL(TSPL_JWO_GATE_ENTRY.Doc_Type,'')='Sku_Receipt' then 1 when ISNULL(TSPL_JWO_QUALITY_CHECK.is_param_accepted,0)>0 then 1 else 0 end as Accepted " + Environment.NewLine +
            " ,TSPL_JWO_UNLOADING.Unloading_No,TSPL_JWO_UNLOADING.Unloading_Date_Time,TSPL_JWO_GATE_ENTRY.JobWorkLocation,TabJOBLocation.Location_Desc as JobWorkLocationDescription ,TSPL_JWO_WEIGHMENT.Gross_Weight,TSPL_JWO_WEIGHMENT.Tare_Weight,TSPL_JWO_WEIGHMENT.Net_Weight" + Environment.NewLine +
            " From TSPL_JWO_GATE_ENTRY" + Environment.NewLine +
            " left outer join TSPL_JWO_Weighment on TSPL_JWO_Weighment.Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No  " + Environment.NewLine +
            " left outer join TSPL_JWO_QUALITY_CHECK on TSPL_JWO_QUALITY_CHECK.gate_entry_no=TSPL_JWO_GATE_ENTRY.Gate_Entry_No " + Environment.NewLine +
            " left outer join TSPL_JWO_UNLOADING on TSPL_JWO_UNLOADING.gate_entry_no=TSPL_JWO_GATE_ENTRY.Gate_Entry_No " + Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER as TabJOBLocation on TabJOBLocation.Location_Code=TSPL_JWO_GATE_ENTRY.JobWorkLocation " + Environment.NewLine +
            " where TSPL_JWO_UNLOADING.isPosted=1 and TSPL_JWO_Weighment.isPosted=1  and not exists (select 1 from TSPL_JWO_SRN_HEAD where TSPL_JWO_SRN_HEAD.Unloading_No=TSPL_JWO_UNLOADING.Unloading_No and TSPL_JWO_SRN_HEAD.Document_No not in ('" + strDocNo + "') and not exists(select 1 from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No) )"
                If settJobWorkOutwardComsumeItemAccordingToBOM Then
                    qry += " and TSPL_JWO_GATE_ENTRY.QC_Status=1 "
                End If
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("Ojowsankerf", qry, "TankerNo", txtVehicleNo.Value)
                If dr Is Nothing Then
                    Exit Sub
                End If
                BlankAllControl()
                txtVehicleNo.Value = clsCommon.myCstr(dr("TankerNo"))
                txtGateEntryNo.Text = clsCommon.myCstr(dr("GateEntryNo"))
                txtGateEntryDate.Value = clsCommon.myCDate(dr("Gate Entry Date And Time"))
                txtChallanNo.Text = clsCommon.myCstr(dr("Challan No"))
                txtChallanDate.Value = clsCommon.myCDate(dr("Challan Date"))
                txtVendor.Value = clsCommon.myCstr(dr("Vendor Code"))
                lblVendorName.Text = clsCommon.myCstr(dr("Vendor Desc"))
                txtLocation.Value = clsCommon.myCstr(dr("Location Code"))
                lblLocationDesc.Text = clsCommon.myCstr(dr("Location Desc"))
                txtJobLocation.Value = clsCommon.myCstr(dr("JobWorkLocation"))
                lblJobLocation.Text = clsCommon.myCstr(dr("JobWorkLocationDescription"))
                lblUnloadingNo.Text = clsCommon.myCstr(dr("Unloading_No"))
                isInsideLoadData = True
                gvItem.Rows(gvItem.RowCount - 1).Cells(colSNo).Value = 1
                gvItem.Rows(gvItem.RowCount - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item Code"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item Desc"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item Code")), Nothing)
                gvItem.Rows(gvItem.RowCount - 1).Cells(colUOM).Value = clsCommon.myCstr(dr("UOM"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(dr("Gross_Weight"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colTareWeight).Value = clsCommon.myCdbl(dr("Tare_Weight"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colNetWeight).Value = clsCommon.myCdbl(dr("Net_Weight"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Net_Weight"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colFat).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_JWO_QC_PARAMETER_DETAIL where QC_No='" + clsCommon.myCstr(dr("QC No")) + "' and Param_Type in ('FAT')"))
                gvItem.Rows(gvItem.RowCount - 1).Cells(colSNF).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_JWO_QC_PARAMETER_DETAIL where QC_No='" + clsCommon.myCstr(dr("QC No")) + "' and Param_Type in ('SNF')"))
                isInsideLoadData = False
                UpdateCurrentRow(gvItem.RowCount - 1)
                UpdateAllTotals()
                txtDocNo.Value = strDocNo
            ElseIf chkSKU.IsChecked Then
                Dim qry As String = "select Tanker_No, Gate_Entry_No as GateEntryNo,convert(varchar, Date_And_Time,103) as GateEntryDate ,Challan_No,Challan_Date ,location_Code,Location_Desc,JobWorkLocation,Vendor_Code,Vendor_Desc" + Environment.NewLine +
                " from TSPL_JWO_GATE_ENTRY" + Environment.NewLine

                Dim Whrclas As String = " Doc_Type='Sku_Receipt' " + Environment.NewLine +
                " and isPosted=1 " + Environment.NewLine +
                " and not exists (select 1 from TSPL_JWO_SRN_HEAD where TSPL_JWO_SRN_HEAD.Against_Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No and TSPL_JWO_SRN_HEAD.Document_No not in ('" + strDocNo + "') and not exists(select 1 from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No) )"
                If Not settJobWorkOutwardComsumeItemAccordingToBOM Then
                    Whrclas += " and TSPL_JWO_GATE_ENTRY.QC_Status=1 "
                End If
                Dim strGateEntryNo As String = clsCommon.ShowSelectForm("jowdgenkerfi", qry, "GateEntryNo", Whrclas, txtVehicleNo.Value, "", isButtonClicked)
                BlankAllControl()
                If clsCommon.myLen(strGateEntryNo) > 0 Then
                    Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Sku_Receipt", NavigatorType.Current)
                    txtVehicleNo.Value = obj.Tanker_No
                    txtGateEntryNo.Text = obj.Gate_Entry_No
                    txtGateEntryDate.Value = obj.Date_And_Time
                    txtChallanNo.Text = obj.Challan_No
                    txtChallanDate.Value = obj.Challan_Date
                    txtVendor.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Desc
                    txtLocation.Value = obj.location_Code
                    lblLocationDesc.Text = obj.Location_Desc
                    txtJobLocation.Value = obj.JobWorkLocation
                    lblJobLocation.Text = clsLocation.GetName(obj.JobWorkLocation, Nothing)
                    lblUnloadingNo.Text = ""
                    isInsideLoadData = True
                    If obj.arrJOWGateEntryDetail IsNot Nothing AndAlso obj.arrJOWGateEntryDetail.Count > 0 Then
                        For Each objTr As clsMilkGateEntryDetail_JOW In obj.arrJOWGateEntryDetail
                            If Not settJobWorkOutwardComsumeItemAccordingToBOM Then
                                If Not objTr.QC_Status Then
                                    Continue For
                                End If
                            End If
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNo).Value = gvItem.Rows.Count
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
                            gvItem.Rows(gvItem.RowCount - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Qty_In_Kg
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                            UpdateCurrentRow(gvItem.RowCount - 1)
                            gvItem.Rows.AddNew()
                        Next
                    End If
                    UcAttachment1.Form_ID = clsUserMgtCode.frmGateEntry_JWO
                    UcAttachment1.LoadData(txtGateEntryNo.Text)

                    UcAttachment1.Form_ID = MyBase.Form_ID
                    isInsideLoadData = False
                    UpdateAllTotals()
                End If
                txtDocNo.Value = strDocNo
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Dim WhrCls As String = " Location_Type='Physical' and  isnull(Is_Jobwork,0)=0"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsLocation.getFinder(WhrCls, txtLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            txtLocation.Text = txtLocation.Value
        Else
            lblLocationDesc.Text = ""
            txtLocation.Text = ""
        End If
        loadBlankItemGrid()
    End Sub

    Private Sub txtJobLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtJobLocation._MYValidating
        Dim WhrCls As String = " isnull(Is_Jobwork,0)=1"
        txtJobLocation.Value = clsLocation.getFinder(WhrCls, txtJobLocation.Value, isButtonClicked)
        setVendorLocation(True)
        loadBlankItemGrid()
    End Sub

    Sub setVendorLocation(ByVal isLocation)
        Dim qry As String = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Jobwork_Vendor,TSPL_VENDOR_MASTER.Vendor_Name from " + Environment.NewLine +
        "TSPL_LOCATION_MASTER" + Environment.NewLine +
        "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor" + Environment.NewLine +
        "where 2=2"
        If isLocation Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code='" + txtJobLocation.Value + "'"
        Else
            qry += " and TSPL_LOCATION_MASTER.Jobwork_Vendor='" + txtVendor.Value + "'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtJobLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            lblJobLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            txtVendor.Value = clsCommon.myCstr(dt.Rows(0)("Jobwork_Vendor"))
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
        Else
            txtJobLocation.Value = ""
            lblJobLocation.Text = ""
            txtVendor.Value = ""
            lblVendorName.Text = ""
        End If
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendor._MYValidating
        Dim qry As String = "select TSPL_LOCATION_MASTER.Jobwork_Vendor as VendorCode,TSPL_VENDOR_MASTER.Vendor_Name as VendorName from " + Environment.NewLine +
        " TSPL_LOCATION_MASTER" + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor"
        Dim wrhclas As String = "len(isnull( TSPL_LOCATION_MASTER.Jobwork_Vendor,''))>0"
        txtVendor.Value = clsCommon.ShowSelectForm("vendorFromloc", qry, "VendorCode", wrhclas, txtVendor.Value, "", isButtonClicked)
        setVendorLocation(False)
    End Sub

    Private Sub gvItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colICode) OrElse e.Column Is gvItem.Columns(colUOM) OrElse e.Column Is gvItem.Columns(colQty) OrElse e.Column Is gvItem.Columns(colRate) Then
                        If e.Column Is gvItem.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gvItem.Columns(colUOM) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gvItem.Columns(colQty) Then
                            OpenBatchItem()
                        End If
                        UpdateCurrentRow(gvItem.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""
        Dim obj As clsItemMaster
        obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gvItem.CurrentRow.Cells(colICode).Value), "", isButtonClick, Nothing, whrcls)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gvItem.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gvItem.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gvItem.CurrentRow.Cells(colUOM).Value = obj.Unit_Code
            gvItem.CurrentRow.Cells(colFat).Value = clsBOM.GetFAT_PERS(obj.Item_Code)
            gvItem.CurrentRow.Cells(colSNF).Value = clsBOM.GetSNF_PERS(obj.Item_Code)
            gvItem.CurrentRow.Cells(colHSNCode).Value = obj.HSNCode
        Else
            gvItem.CurrentRow.Cells(colICode).Value = ""
            gvItem.CurrentRow.Cells(colIName).Value = ""
            gvItem.CurrentRow.Cells(colUOM).Value = ""
            gvItem.CurrentRow.Cells(colFat).Value = 0.0
            gvItem.CurrentRow.Cells(colSNF).Value = 0.0
            gvItem.CurrentRow.Cells(colSNF).Value = 0.0
            gvItem.CurrentRow.Cells(colHSNCode).Value = ""
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvItem.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRdsefndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
        End If
    End Sub

    Sub UpdateCurrentRow(ByVal ii As Integer)
        Dim strRMItem As String = ""
        If clsCommon.myLen(txtJWOEstimate.Value) > 0 Then
            strRMItem = "select Raw_Item_Code from TSPL_JWO_ESTIMATION_RAW_ITEM
             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_JWO_ESTIMATION_RAW_ITEM.Raw_Item_Code
             where TSPL_JWO_ESTIMATION_RAW_ITEM.Main_Item_Code='" + clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value) + "' and TSPL_ITEM_MASTER.Product_Type='MI' and Document_NO='" + txtJWOEstimate.Value + "'"
            strRMItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strRMItem))
            If clsCommon.myLen(strRMItem) > 0 Then
                Dim obj As clsVendorItemChargeDetail = clsVendorItemChargeDetail.GetJobPrice(txtVendor.Value, strRMItem, clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value), txtDate.Value, Nothing)
                Dim dblQty As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colQty).Value)
                Dim dblRate As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colRate).Value)
                Dim dblJobRate As Double = obj.ItemCharge
                Dim dblJobAmt As Double = dblQty * dblJobRate
                Dim dblAmt As Double = dblQty * dblRate
                gvItem.Rows(ii).Cells(colPriceCode).Value = obj.Price_Code
                gvItem.Rows(ii).Cells(colJobRate).Value = obj.ItemCharge
                gvItem.Rows(ii).Cells(colJobAmt).Value = dblJobAmt
                gvItem.Rows(ii).Cells(colAmt).Value = dblAmt
            End If
        End If
    End Sub

    Sub UpdateAllTotals()
        Dim dblAmt As Decimal = 0
        Dim dblJobAmt As Decimal = 0
        For ii As Integer = 0 To gvItem.RowCount - 1
            dblAmt += clsCommon.myCdbl(gvItem.Rows(ii).Cells(colAmt).Value)
            dblJobAmt += clsCommon.myCdbl(gvItem.Rows(ii).Cells(colJobAmt).Value)
        Next
        lblTotalAmt.Text = clsCommon.myCstr(dblAmt)
        lblJobAmt.Text = clsCommon.myCstr(dblJobAmt)
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
        If gvItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvItem.CurrentRow.Index
            gvItem.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvItem.Rows.Count - 1 Then
                gvItem.Rows.AddNew()
                gvItem.CurrentRow = gvItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub rbtnManual_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnManual.ToggleStateChanged, rbtnTanker.ToggleStateChanged, chkSKU.ToggleStateChanged
        If rbtnManual.IsChecked Then
            txtLocation.Enabled = True
            txtJobLocation.Enabled = True
            txtVendor.Enabled = True
            txtGateEntryNo.Enabled = True
            txtGateEntryDate.Enabled = True
            txtChallanNo.Enabled = True
            txtChallanDate.Enabled = True
        ElseIf rbtnTanker.IsChecked Then
            txtLocation.Enabled = False
            txtJobLocation.Enabled = False
            txtVendor.Enabled = False
            txtGateEntryNo.Enabled = False
            txtGateEntryDate.Enabled = False
            txtChallanNo.Enabled = False
            txtChallanDate.Enabled = False
        ElseIf chkSKU.IsChecked Then
            txtLocation.Enabled = False
            txtJobLocation.Enabled = False
            txtVendor.Enabled = False
            txtGateEntryNo.Enabled = False
            txtGateEntryDate.Enabled = False
            txtChallanNo.Enabled = False
            txtChallanDate.Enabled = False
        End If
    End Sub


    Private Sub gvItem_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvItem.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gvItem.Columns(colICode) Then
                    gvItem.CurrentRow.Cells(colICode).ReadOnly = Not rbtnManual.IsChecked
                ElseIf e.Column Is gvItem.Columns(colUOM) Then
                    gvItem.CurrentRow.Cells(colUOM).ReadOnly = Not rbtnManual.IsChecked
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvItem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvItem.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gvItem.Rows.Count
            gvItem.Rows(ii - 1).Cells(colSNo).Value = ii
        Next

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Reverese and unpost current document " + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsJWOSRNHead.ReverseAndUnpostData(txtDocNo.Value)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gvItem.KeyDown
        If e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub

    Sub OpenBatchItem()
        Dim blnBatchqty As Boolean = False
        If clsCommon.myCBool(clsItemMaster.IsBatchItem(gvItem.CurrentRow.Cells(colICode).Value)) Then
            Dim frm As frmBatchItemIn = New frmBatchItemIn()
            frm.strItemCode = clsCommon.myCstr(gvItem.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gvItem.CurrentRow.Cells(colIName).Value)
            frm.dblqty = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value)
            frm.strUOM = clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value)
            'frm.dblMRP = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colMRP).Value)
            frm.arr = TryCast(gvItem.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gvItem.CurrentRow.Cells(colICode).Tag = frm.arr
            End If
        End If
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code not found to show history")
                txtDocNo.Focus()
            End If
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(txtDocNo.Value), "DOCUMENT_NO", "TSPL_JWO_SRN_HEAD", "TSPL_JWO_SRN_DETAIL")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub txtJWOEstimate__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtJWOEstimate._MYValidating
        Try
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                Throw New Exception("Please select Vendor")
            End If
            Dim qry As String = "select Document_NO,Document_Date,Location_Code,Vendor_Code,Item_Structure_FAT,Item_Structure_SNF from TSPL_JWO_ESTIMATION_HEAD  "
            Dim Whrclas As String = "status=1 and vendor_code='" + txtVendor.Value + "' and not exists(select 1 from TSPL_JWO_SRN_HEAD where TSPL_JWO_SRN_HEAD.Against_Estimate=TSPL_JWO_ESTIMATION_HEAD.Document_NO)"
            txtJWOEstimate.Value = clsCommon.ShowSelectForm("Ojowenkerfi", qry, "Document_NO", Whrclas, txtJWOEstimate.Value, "", isButtonClicked)

            If clsCommon.myLen(txtJWOEstimate.Value) > 0 Then
                qry = "select Item_code,qty from TSPL_JWO_ESTIMATION_FAT_PRODUCTION where Document_NO='" + txtJWOEstimate.Value + "'" + Environment.NewLine +
                "union all" + Environment.NewLine +
                "select Item_code,qty from TSPL_JWO_ESTIMATION_SNF_PRODUCTION where Document_NO='" + txtJWOEstimate.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        For ii As Integer = 0 To gvItem.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(dr("Item_code"))) = CompairStringResult.Equal Then
                                gvItem.Rows(ii).Cells(colEstimateQty).Value = clsCommon.myCdbl(dr("qty"))
                                Exit For
                            End If
                        Next
                    Next
                Else
                    For ii As Integer = 0 To gvItem.Rows.Count - 1
                        gvItem.Rows(ii).Cells(colEstimateQty).Value = 0
                    Next
                End If
            Else
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    gvItem.Rows(ii).Cells(colEstimateQty).Value = 0
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class
