Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik

Public Class frmMilkShiftUploaderRaj
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ReportID As String = "MSUShiRws"

    Dim isInsideLoadData As Boolean = False
    Dim MilkWeight_Setting As Decimal = 0
    Dim CreateNewDocumentOnUploader As Boolean = False
    Dim strFolderPath As String = ""
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim settMaxReceiveSNFPer As Decimal = 0
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim settLastMilkReceiptQtyTollerance As Decimal = 0
    Dim settAlwaysVSPDefaulter As Boolean = False
    Dim settSelectMilkRejectDefaulterManually As Boolean = False
    Dim settMilkProcurementBatchPosting As Boolean = False

    Dim SampleNo As Integer = -1
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.SetUserMgmt(clsUserMgtCode.MilkShiftUploader)
        settMilkProcurementBatchPosting = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcurementBatchPosting, clsFixedParameterCode.MilkProcurementBatchPosting, Nothing)) = 1)
        MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        CreateNewDocumentOnUploader = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateNewDocumentOnUploader, clsFixedParameterCode.CreateNewDocumentOnUploader, Nothing)) = 1
        isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))
        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        settLastMilkReceiptQtyTollerance = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LastMilkReceiptQtyTollerance, clsFixedParameterCode.LastMilkReceiptQtyTollerance, Nothing))

        settAlwaysVSPDefaulter = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, Nothing)) = 1)
        settSelectMilkRejectDefaulterManually = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectMilkRejectDefaulterManually, clsFixedParameterCode.SelectMilkRejectDefaulterManually, Nothing)) = 1)
        LoadShift()
        LoadReject()
        LoadLate()
        AddNew()
    End Sub
    Public Sub LoadLate()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = "No"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        cboLate.DataSource = dt
        cboLate.ValueMember = "Code"
        cboLate.DisplayMember = "Name"
    End Sub
    Public Sub LoadReject()
        cboRejectType.DataSource = clsMilkReceiptMCC.GetReject(False)
        cboRejectType.ValueMember = "Code"
        cboRejectType.DisplayMember = "Name"
    End Sub
    Public Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        isNewEntry = True
        txtMCC.Value = ""
        LblMccName.Text = ""
        cboShift.SelectedValue = "M"
        UsLock1.Status = ERPTransactionStatus.Pending
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        txRoute.Value = ""
        lblRoute.Text = ""
        txtTruckNo.Text = ""
        cboLate.SelectedValue = 0

        txtMCC.Value = ""
        LblMccName.Text = ""

        txtTotEnteredQty.Text = ""
        txtTotEnteredFAT.Text = ""
        txtTotEnteredSNF.Text = ""

        txtTotReceivedQty.Text = ""
        txtTotReceivedFAT.Text = ""
        txtTotReceivedSNF.Text = ""

        txtTotPendingQty.Text = ""
        txtTotPendingFAT.Text = ""
        txtTotPendingSNF.Text = ""
        txtTotPendingFATPer.Text = ""
        txtTotPendingSNFPer.Text = ""

        txtVLC.Value = ""
        txtVLC.Tag = ""
        lblVLC.Text = ""
        cboRejectType.SelectedValue = ""
        txtQty.Text = ""
        txtFAT.Text = ""
        txtSNF.Text = ""
        lblTotEntry.Text = ""
        txtMCC.Value = ""
        LblMccName.Text = ""
        SampleNo = -1
        lblTotEntry.Text = ""
    End Sub

    Function GetRejectDefaulter() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Company"
        dr("Name") = "Company"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Transporter"
        dr("Name") = "Transporter"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "VSP"
        dr("Name") = "VSP"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Function FillYesNoValue() As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        'Prevent future date transaction
        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            txtDate.Focus()
            Throw New Exception("Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
        End If
        If txtTotEnteredQty.Value <= 0 Then
            txtTotEnteredQty.Focus()
            Throw New Exception("Please fill Total Entered Qty")
        End If
        If txtTotEnteredFAT.Value <= 0 Then
            txtTotEnteredFAT.Focus()
            Throw New Exception("Please fill Total FAT KG")
        End If
        If txtTotEnteredSNF.Value <= 0 Then
            txtTotEnteredSNF.Focus()
            Throw New Exception("Please fill Total FAT SNF")
        End If

        If clsCommon.myLen(txRoute.Value) <= 0 Then
            txRoute.Focus()
            Throw New Exception("Please select Route")
        End If
        If clsCommon.myLen(txtMCC.Value) <= 0 Then
            txtMCC.Focus()
            Throw New Exception("Please select MCC")
        End If
        If clsCommon.myLen(txtVLC.Value) <= 0 Then
            txtVLC.Focus()
            Throw New Exception("Please select VLC")
        End If

        If txtQty.Value <= 0 Then
            txtQty.Focus()
            Throw New Exception("Please enter Qty")
        End If

        If txtFAT.Value <= 0 Then
            txtFAT.Focus()
            Throw New Exception("Please enter FAT")
        End If
        If txtSNF.Value <= 0 Then
            txtSNF.Focus()
            Throw New Exception("Please enter SNF")
        End If
        Return True
    End Function

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub

    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No,convert (varchar,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) as Shift_Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift,TSPL_MILK_SHIFT_UPLOADER_HEAD.Description,case when TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 then 'Posted' else 'Pending' end as Status" &
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code as [MCC Code]  ,tspl_mcc_master.MCC_NAME as [Mcc Name] " &
        ",tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name]" &
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.DOCK_CODE as [Dock Code]" &
        ",TSPL_DOCK_MASTER.Description as [Dock Name]" &
        " from TSPL_MILK_SHIFT_UPLOADER_HEAD" &
        " left join  tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MILK_SHIFT_UPLOADER_HEAD.mcc_code" &
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" &
        " left join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.code=TSPL_MILK_SHIFT_UPLOADER_HEAD.dock_code"
        LoadData(clsCommon.ShowSelectForm("SMPUFINOC", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
    End Sub

    Public Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkShiftUploaderHead()
                obj.Document_No = txtDocNo.Value
                obj.Shift_Date = txtDate.Value
                obj.Shift = clsCommon.myCstr(cboShift.SelectedValue)
                'obj.Description = txtDesc.Text
                obj.MCC_Code = txtMCC.Value
                'obj.Dock_Code = txtDockCode.Value
                'obj.Mix_Milk = chkMixMilk.Checked

                obj.Raj_Bulk_Route_Code = txRoute.Value
                obj.Raj_Truck_no = txtTruckNo.Text
                obj.Raj_Late = clsCommon.myCdbl(cboLate.SelectedValue)
                obj.Raj_Entered_Qty = txtTotEnteredQty.Value
                obj.Raj_Entered_FATKg = txtTotEnteredFAT.Value
                obj.Raj_Entered_SNFKg = txtTotEnteredSNF.Value
                obj.Arr = New List(Of clsMilkShiftUploaderDetail)

                Dim objTr As New clsMilkShiftUploaderDetail()
                If SampleNo > 0 Then
                    objTr.SNo = SampleNo
                ElseIf gv1 Is Nothing OrElse gv1.Rows.Count <= 0 Then
                    objTr.SNo = 1
                Else
                    objTr.SNo = gv1.Rows.Count + 1
                End If
                'objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value)
                objTr.VLC_Code = txtVLC.Tag
                'objTr.No_Of_Cans = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNoOfCan).Value)
                objTr.Milk_Weight = txtQty.Value
                If objTr.No_Of_Cans = 0 Then
                    objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                End If
                objTr.FAT = Math.Round(txtFAT.Value, 1, MidpointRounding.ToEven)
                objTr.SNF = Math.Round(txtSNF.Value, IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.ToEven)
                objTr.Reject_Type = clsCommon.myCstr(cboRejectType.SelectedValue)

                If clsCommon.myLen(cboRejectType.SelectedValue) > 0 Then
                    If clsCommon.myCdbl(clsCommon.myCdbl(cboLate.SelectedValue)) = 1 Then
                        objTr.Reject_Defaulter = "Transporter"
                    Else
                        objTr.Reject_Defaulter = "VSP"
                    End If
                End If
                obj.Arr.Add(objTr)
                obj.SaveData(obj, isNewEntry, True)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
                txtVLC.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            isInsideLoadData = True
            Dim obj As New clsMilkShiftUploaderHead()
            obj = clsMilkShiftUploaderHead.GetData(strCode, NavTyep, Nothing, True, False)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Shift_Date
                cboShift.SelectedValue = obj.Shift
                'txtDesc.Text = obj.Description
                UsLock1.Status = obj.Status
                txtMCC.Value = obj.MCC_Code
                LblMccName.Text = obj.MCC_Name
                'txtDockCode.Value = obj.Dock_Code
                'lblDockName.Text = obj.Dock_Name

                txRoute.Value = obj.Raj_Bulk_Route_Code
                lblRoute.Text = obj.Raj_Bulk_Route_Name
                txtTruckNo.Text = obj.Raj_Truck_no

                cboLate.SelectedValue = obj.Raj_Late
                txtTotEnteredQty.Value = obj.Raj_Entered_Qty
                txtTotEnteredFAT.Value = obj.Raj_Entered_FATKg
                txtTotEnteredSNF.Value = obj.Raj_Entered_SNFKg

                txtTotReceivedQty.Text = obj.Raj_Received_Qty
                txtTotReceivedFAT.Text = obj.Raj_Received_FATKg
                txtTotReceivedSNF.Text = obj.Raj_Received_SNFKg

                txtTotPendingQty.Text = (obj.Raj_Entered_Qty - obj.Raj_Received_Qty)
                txtTotPendingFAT.Text = (obj.Raj_Entered_FATKg - obj.Raj_Received_FATKg)
                txtTotPendingSNF.Text = (obj.Raj_Entered_SNFKg - obj.Raj_Received_SNFKg)
                If clsCommon.myCdbl(txtTotPendingQty.Text) <> 0 Then
                    txtTotPendingFATPer.Text = Math.Round(clsCommon.myCdbl(txtTotPendingFAT.Text) * 100 / clsCommon.myCdbl(txtTotPendingQty.Text), 2)
                    txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCdbl(txtTotPendingSNF.Text) * 100 / clsCommon.myCdbl(txtTotPendingQty.Text), 2)
                End If
                If obj.dtRaj IsNot Nothing AndAlso obj.dtRaj.Rows.Count > 0 Then
                    gv1.DataSource = obj.dtRaj
                    gv1.ShowGroupPanel = False
                    gv1.ShowFilteringRow = True
                    gv1.AllowAddNewRow = False
                    gv1.AllowDeleteRow = False
                    gv1.SummaryRowsBottom.Clear()

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                        gv1.Columns(ii).IsVisible = True
                        gv1.Columns(ii).BestFit()

                        Try


                            If gv1.Columns(ii).Name.Contains(" Qty") OrElse gv1.Columns(ii).Name.Contains(" FATKg") OrElse gv1.Columns(ii).Name.Contains(" SNFKG") Then
                                Dim item1 As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F3}", GridAggregateFunction.Sum)
                                summaryRowItem.Add(item1)
                            ElseIf gv1.Columns(ii).Name.Contains(" FAT %") Then ''OrElse gv1.Columns(ii).Name.Contains(" SNF %")
                                Dim strPrefix As String = gv1.Columns(ii).Name.Replace(" FAT %", "")
                                Dim summaryItem5 As New GridViewSummaryItem()
                                summaryItem5.FormatString = "{0:F2}"
                                summaryItem5.Name = gv1.Columns(ii).Name
                                summaryItem5.AggregateExpression = "sum([" + strPrefix + " FATKg])*100/sum([" + strPrefix + " Qty])"
                                summaryRowItem.Add(summaryItem5)
                            ElseIf gv1.Columns(ii).Name.Contains(" SNF %") Then
                                Dim strPrefix As String = gv1.Columns(ii).Name.Replace(" SNF %", "")
                                Dim summaryItem5 As New GridViewSummaryItem()
                                summaryItem5.FormatString = "{0:F2}"
                                summaryItem5.Name = gv1.Columns(ii).Name
                                summaryItem5.AggregateExpression = "sum([" + strPrefix + " SNFKg])*100/sum([" + strPrefix + " Qty])"
                                summaryRowItem.Add(summaryItem5)
                            End If
                        Catch ex As Exception
                        End Try
                    Next

                    Dim item11 As New GridViewSummaryItem("VLC Code", "Total", GridAggregateFunction.Max)
                    summaryRowItem.Add(item11)

                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    gv1.Columns("TR_No").IsVisible = False
                    lblTotEntry.Text = gv1.Rows.Count
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkShiftUploaderHead.DeleteData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If settMilkProcurementBatchPosting Then
                    clsMilkShiftUploaderHead.PostDataForBatch(txtDocNo.Value)
                Else
                    clsMilkShiftUploaderHead.PostData(txtDocNo.Value)
                End If
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        If clsCommon.myLen(txRoute.Value) <= 0 Then
            txRoute.Focus()
            Throw New Exception("Please provide Route")
        End If

        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master inner join TSPL_BULK_ROUTE_MASTER_MCC on TSPL_BULK_ROUTE_MASTER_MCC.MCC_code=TSPL_MCC_MASTER.MCC_Code and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txRoute.Value + "' LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("frmCorrection@M", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + txtMCC.Value + "' "))
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        Dim qry As String = "select 1 as 'S NO',null as 'VLC Uploader Code',null as 'Qty (Ltr)',null as 'FAT%',null as 'SNF%','' as [Milk Type(M/C/B)],'' as  [Reject Type],'' as [Reject Defaulter]"
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub txRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            txRoute.Value = clsCommon.ShowSelectForm("dd12ShUp", qry, "Code", whrCls, txRoute.Value, "Code", isButtonClicked)
            lblRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + txRoute.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVLC._MYValidating
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txRoute.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],Route_name as [Route Name]," _
                & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                + " ,TSPL_VLC_MASTER_HEAD.Village_Code as VillageCode,TSPL_VILLAGE_MASTER.Village_Name as VillageName " _
                & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code "
            txtVLC.Value = clsCommon.ShowSelectForm("VLCFNDpt@R", qry, "Uploader_Code", " TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 and TSPL_VLC_MASTER_HEAD.Active=1 and tspl_mcc_master.mcc_Code='" & txtMCC.Value & "'", txtVLC.Value, "Uploader_Code", isButtonClicked)
            If clsCommon.myLen(txtVLC.Value) <= 0 Then
                Exit Sub
            End If

            qry &= " where  tspl_mcc_master.mcc_Code='" & txtMCC.Value & "' And vlc_code_vlc_uploader ='" & clsCommon.myCstr(txtVLC.Value) & "'"
            Dim dt_vlc As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dr As DataRow = dt_vlc.Rows(0)
            If Not IsNothing(dr) Then
                txtVLC.Value = clsCommon.myCstr(dr("Uploader_Code"))
                txtVLC.Tag = clsCommon.myCstr(dr("Vlc Code"))
                lblVLC.Text = clsCommon.myCstr(dr("VLC Name"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboRejectType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboRejectType.SelectedIndexChanged
        Try
            If cboRejectType.SelectedIndex = 0 Then
                RadGroupBox2.BackColor = System.Drawing.Color.MediumSeaGreen
            ElseIf cboRejectType.SelectedIndex = 1 Then
                RadGroupBox2.BackColor = System.Drawing.Color.Khaki
            ElseIf cboRejectType.SelectedIndex = 2 Then
                RadGroupBox2.BackColor = System.Drawing.Color.PaleGoldenrod
            ElseIf cboRejectType.SelectedIndex = 3 Then
                RadGroupBox2.BackColor = System.Drawing.Color.Tan
            ElseIf cboRejectType.SelectedIndex = 4 Then
                RadGroupBox2.BackColor = System.Drawing.Color.Coral
            ElseIf cboRejectType.SelectedIndex = 5 Then
                RadGroupBox2.BackColor = System.Drawing.Color.MistyRose
            ElseIf cboRejectType.SelectedIndex = 6 Then
                RadGroupBox2.BackColor = System.Drawing.Color.IndianRed
            ElseIf cboRejectType.SelectedIndex = 7 Then
                RadGroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
            ElseIf cboRejectType.SelectedIndex = 8 Then
                RadGroupBox2.BackColor = System.Drawing.Color.NavajoWhite
            Else
                RadGroupBox2.BackColor = System.Drawing.Color.Transparent
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSNF_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSNF.Validating
        SaveData()
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells("TR_No").Value) > 0 Then
                Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight,TSPL_MILK_SHIFT_UPLOADER_DETAIL.FAT,TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNF,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type 
                from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                where  TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No='" + clsCommon.myCstr(gv1.CurrentRow.Cells("TR_No").Value) + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    SampleNo = clsCommon.myCdbl(dt.Rows(0)("SNo"))
                    txtVLC.Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader"))
                    txtVLC.Tag = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                    lblVLC.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                    cboRejectType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Reject_Type"))
                    txtQty.Text = clsCommon.myCdbl(dt.Rows(0)("Milk_Weight"))
                    txtFAT.Text = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                    txtSNF.Text = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                    txtVLC.Focus()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        clsMilkShiftUploaderHead.MultipleDateSingleExport(Me)

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        clsMilkShiftUploaderHead.MultipleDateSingleImport(Me)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_MILK_SHIFT_UPLOADER_HEAD", "TSPL_MILK_SHIFT_UPLOADER_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
