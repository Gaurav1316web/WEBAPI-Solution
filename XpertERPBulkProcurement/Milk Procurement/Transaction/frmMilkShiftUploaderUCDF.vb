Imports System.Data.SqlClient
Imports common
Imports Telerik

Public Class frmMilkShiftUploaderUCDF
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
    Dim SettShowAllDCS As Boolean
    Dim settAllowZeroFATSNF As Boolean = False
    Dim SampleNo As Integer = -1
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '        Dim coll As New Dictionary(Of String, String)()

        '        Try
        '            coll = New Dictionary(Of String, String)()
        '            coll.Add("ACC_Qty_LTR", "DECIMAL(18,3) NOT NULL DEFAULT 0")
        '            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_SRN_DETAIL", coll, "Primary Key (DOC_CODE,PK_Id)", True, False, "TSPL_MILK_SRN_HEAD", "DOC_CODE", "")


        '            Dim qry As String = "select 1 from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='TSPL_MILK_SRN_HEAD' and COLUMN_NAME='Against_Uploader_TR_No'"
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
        '                Try
        '                    qry = "alter table TSPL_MILK_SRN_HEAD drop PK_MilkSampleCodeSample_NoReject"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "alter table TSPL_MILK_SRN_HEAD add Against_Uploader_TR_No varchar(30) NULL References TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL(TR_No)"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "alter table TSPL_MILK_SRN_HEAD add Against_Shift_Uploader_TR_No varchar(30) NULL References TSPL_MILK_SHIFT_UPLOADER_DETAIL(TR_No)"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "CREATE UNIQUE INDEX Unique_Against_Uploader_TR_No ON TSPL_MILK_SRN_HEAD (Against_Uploader_TR_No) WHERE Against_Uploader_TR_No IS NOT NULL;"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "CREATE UNIQUE INDEX Unique_Against_Shift_Uploader_TR_No ON TSPL_MILK_SRN_HEAD (Against_Shift_Uploader_TR_No) WHERE Against_Shift_Uploader_TR_No IS NOT NULL;"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "update TSPL_MILK_SRN_HEAD set Against_Uploader_TR_No=x.Against_Uploader_TR_No,Against_Shift_Uploader_TR_No=x.Against_Shift_Uploader_TR_No from (
        'select TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No,TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No,TSPL_MILK_SRN_HEAD.DOC_CODE from TSPL_MILK_SRN_HEAD
        'inner join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE
        'inner join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
        ')x inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=x.DOC_CODE"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "update TSPL_MILK_SRN_DETAIL set ACC_Qty_LTR=x.ACC_WEIGHT_LTR from (
        'select TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR 
        'from TSPL_MILK_SRN_DETAIL
        'inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
        'inner join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE
        'inner join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
        ')x inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=x.DOC_CODE"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    ''Now delete Procurement table
        '                    qry = clsGetKeys.GetForeignKeyName("TSPL_MILK_SRN_HEAD", "MILK_SAMPLE_CODE", tran)
        '                    If clsCommon.myLen(qry) > 0 Then
        '                        qry = "alter table TSPL_MILK_SRN_HEAD drop " & qry & ""
        '                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    End If

        '                    qry = "alter table TSPL_MILK_SRN_HEAD drop column MILK_SAMPLE_CODE"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "drop table TSPL_MILK_Shift_End_Route_DETAIL  "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_Shift_End_DETAIL"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_Shift_End_HEAD"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_SAMPLE_DETAIL"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table  TSPL_MCC_SAMPLE_QC_DETAIL "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_SAMPLE_DETAIL_History "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_SAMPLE_READING_LOG "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_SAMPLE_HEAD"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "drop table TSPL_MILK_RECEIPT_DETAIL"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MCC_SAMPLE_QC_PARAMETER_DETAIL"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MCC_SAMPLE_QC_HEAD "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG "
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '                    qry = "drop table TSPL_MILK_RECEIPT_HEAD"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    qry = "drop table TSPL_OPEN_MCC_SHIFT"
        '                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

        '                    tran.Commit()
        '                Catch ex As Exception
        '                    tran.Rollback()
        '                    Throw New Exception("Error in Milk Procument Structure change" + Environment.NewLine + ex.Message)
        '                End Try
        '            End If
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(ex.Message)
        '        End Try

        '        coll = New Dictionary(Of String, String)()
        '        coll.Add("Code", "Varchar(30) not null PRIMARY KEY")
        '        coll.Add("Description", "varchar(200) NOT NULL")
        '        coll.Add("Start_Date", "date not null")
        '        coll.Add("End_Date", "date null")
        '        coll.Add("End_Date_Created_By", "varchar(12)  NULL")
        '        coll.Add("End_Date_Created_Date", "datetime NULL")
        '        coll.Add("Created_By", "varchar(12) NOT NULL")
        '        coll.Add("Created_Date", "datetime NOT NULL")
        '        coll.Add("Modified_By", "varchar(12)  NULL")
        '        coll.Add("Modified_Date", "datetime NOT NULL")
        '        coll.Add("Posted", "integer  NOT NULL DEFAULT 0")
        '        coll.Add("Posted_By", "varchar(12)  NULL")
        '        coll.Add("Posted_Date", "datetime NULL")
        '        coll.Add("Inactive", "integer  NOT NULL DEFAULT 0")
        '        coll.Add("Inactive_By", "varchar(12)  NULL")
        '        coll.Add("Inactive_Date", "datetime NULL")
        '        coll.Add("Applicable_On", "integer not null")
        '        coll.Add("Payment_Mehod", "integer not null")
        '        coll.Add("Calculation_Mehod", "integer not null")
        '        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_CHILLING_CHARGES", coll, Nothing, True)


        '        coll = New Dictionary(Of String, String)()
        '        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        '        coll.Add("Code", "Varchar(30) not null references TSPL_CHILLING_CHARGES(Code)")
        '        coll.Add("Capacity", "integer not null")
        '        coll.Add("Rate", "decimal(18,2) null")
        '        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_CHILLING_CHARGES_SLAB", coll, Nothing, True)

        '        coll = New Dictionary(Of String, String)()
        '        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        '        coll.Add("InvoiceNo", "Varchar(30) not null references TSPL_MILK_PURCHASE_INVOICE_HEAD(DOC_CODE)")
        '        coll.Add("Against_Chilling_Slab_PK_ID", "integer not NULL references TSPL_CHILLING_CHARGES_SLAB(PK_ID)")
        '        coll.Add("Qty", "DECIMAL(18,2) NULL")
        '        coll.Add("Apply_Date", "datetime not NULL")
        '        coll.Add("Rate", "DECIMAL(18,2) NULL")
        '        coll.Add("Amt", "DECIMAL(18,2) NULL")
        '        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES", coll, Nothing, False, False, "TSPL_MILK_PURCHASE_INVOICE_HEAD", "InvoiceNo", "")


        MyBase.SetUserMgmt(clsUserMgtCode.MilkShiftUploader)
        SettShowAllDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllDCS, clsFixedParameterCode.ShowAllDCS, Nothing))
        settMilkProcurementBatchPosting = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcurementBatchPosting, clsFixedParameterCode.MilkProcurementBatchPosting, Nothing)) = 1)
        settAllowZeroFATSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowZeroFATSNF, clsFixedParameterCode.AllowZeroFATSNF, Nothing)) = 1)
        MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        CreateNewDocumentOnUploader = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateNewDocumentOnUploader, clsFixedParameterCode.CreateNewDocumentOnUploader, Nothing)) = 1
        isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        MyLabel28.Text = If(isPickCLRInsteadOfSNF, "CLR %", "SNF %")
        txtSNF.DecimalPlaces = IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1)
        settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))
        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        settLastMilkReceiptQtyTollerance = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LastMilkReceiptQtyTollerance, clsFixedParameterCode.LastMilkReceiptQtyTollerance, Nothing))

        settAlwaysVSPDefaulter = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, Nothing)) = 1)
        settSelectMilkRejectDefaulterManually = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectMilkRejectDefaulterManually, clsFixedParameterCode.SelectMilkRejectDefaulterManually, Nothing)) = 1)
        LoadDockCollection()
        LoadShift()
        LoadShiftFrom()
        LoadReject()
        LoadLate()
        AddNew()


    End Sub

    Private Sub LoadDockCollection()
        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False, True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"

        cboDockCollectionMilkType.SelectedValue = "M"

        cboDockCollectionMilkType.Enabled = objCommonVar.DisplayTypeInMilkReceipt
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
        Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = ""
            dr("Name") = "Good"
            dt.Rows.InsertAt(dr, 0)
        End If
        cboRejectType.DataSource = dt
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
        GroupBox76.Visible = False
        txtMCCFromDate.Value = DateTime.Now
        txtMCCToDate.Value = DateTime.Now
        txtFromShift.SelectedValue = "M"
        txtNoOfCan.Value = Nothing
        TxtMultiSelectFinder8.arrValueMember = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gvP.DataSource = Nothing
        gvP.Rows.Clear()
        gv1.Columns.Clear()

        'txRoute.Value = ""
        'lblRoute.Text = ""
        'txtTruckNo.Text = ""
        cboLate.SelectedValue = 0

        txtMCC.Value = ""
        LblMccName.Text = ""

        'txtTotEnteredQty.Text = ""
        'txtTotEnteredFAT.Text = ""
        'txtTotEnteredSNF.Text = ""

        'txtTotReceivedQty.Text = ""
        'txtTotReceivedFAT.Text = ""
        'txtTotReceivedSNF.Text = ""

        'txtTotPendingQty.Text = ""
        'txtTotPendingFAT.Text = ""
        'txtTotPendingSNF.Text = ""
        'txtTotPendingFATPer.Text = ""
        'txtTotPendingSNFPer.Text = ""

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
        txtPaymentCycleNo.Text = ""
        txtFiscalYear.Text = ""
        'txtPageNo.Value = 0
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
        Dim Qry As String = "Select * from TSPL_MILK_SHIFT_UPLOADER_HEAD Where TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code='" + txtMCC.Value + "' And 
                                    TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "' And 
                                    convert (Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)=Convert(Date,'" + txtDate.Value + "',103)"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document Already Created !", Me.Text)
        Else
            SaveData()
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        'Prevent future date transaction
        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            txtDate.Focus()
            Throw New Exception("Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
        End If

        If clsCommon.myLen(txtMCC.Value) <= 0 Then
            txtMCC.Focus()
            Throw New Exception("Please select MCC")
        End If
        If clsCommon.myLen(txtVLC.Value) <= 0 Then
            txtVLC.Focus()
            Throw New Exception("Please select VLC")
        End If

        If clsCommon.myCdbl(txtPageNo.Value) <= 0 Then
            txtPageNo.Focus()
            Throw New Exception("Please enter Page No")
        End If

        If txtQty.Value <= 0 Then
            txtQty.Focus()
            Throw New Exception("Please enter Qty")
        End If
        Dim intRejectApplicableOn As Integer = clsMilkRejectType.GetApplicableOn(clsCommon.myCstr(cboRejectType.SelectedValue), Nothing)
        If intRejectApplicableOn <> 1 Then '' ''1-Reject Type is Rate 
            If clsCommon.myLen(txtFAT.Text) <= 0 Then
                txtFAT.Focus()
                Throw New Exception("Please enter FAT")
            End If
            If clsCommon.myLen(txtSNF.Value) <= 0 Then
                txtSNF.Focus()
                Throw New Exception("Please enter SNF")
            End If
            If Not settAllowZeroFATSNF Then
                If txtFAT.Value <= 0 Then
                    txtFAT.Focus()
                    Throw New Exception("Please enter FAT")
                End If
                If txtSNF.Value <= 0 Then
                    txtSNF.Focus()
                    Throw New Exception("Please enter SNF")
                End If
            End If
        End If
        Return True
    End Function

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        ElseIf e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.DeleteMccMilkShiftPassword
            pwd.strType = clsFixedParameterType.DeleteMccMilkShiftPassword
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                GroupBox76.Visible = True
            Else
                GroupBox76.Visible = False
            End If
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
        Dim arrLoc As String = ""
        Dim whrcls As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 1 Then
            arrLoc = "'" + obj.Default_LocCode + "'"
        Else
            arrLoc = obj.arrLocCodes
        End If
        If arrLoc IsNot Nothing AndAlso clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")"
        End If

        Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No,convert (varchar,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) as Shift_Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift,TSPL_MILK_SHIFT_UPLOADER_HEAD.Description,case when TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 then 'Posted' else 'Pending' end as Status" &
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code as [MCC Code]  ,tspl_mcc_master.MCC_NAME as [Mcc Name] " &
        ",tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name]" &
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.DOCK_CODE as [Dock Code]" &
        ",TSPL_DOCK_MASTER.Description as [Dock Name]" &
        " from TSPL_MILK_SHIFT_UPLOADER_HEAD" &
        " left join  tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MILK_SHIFT_UPLOADER_HEAD.mcc_code" &
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" &
        " left join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.code=TSPL_MILK_SHIFT_UPLOADER_HEAD.dock_code"
        LoadData(clsCommon.ShowSelectForm("SMPUFINOC", qry, "Document_No", whrcls, txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
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

                'obj.Raj_Bulk_Route_Code = txRoute.Value
                'obj.Raj_Truck_no = txtTruckNo.Text
                obj.Raj_Late = clsCommon.myCdbl(cboLate.SelectedValue)
                'obj.Raj_Entered_Qty = txtTotEnteredQty.Value
                'obj.Raj_Entered_FATKg = txtTotEnteredFAT.Value
                'obj.Raj_Entered_SNFKg = txtTotEnteredSNF.Value
                obj.Arr = New List(Of clsMilkShiftUploaderDetail)

                Dim objTr As New clsMilkShiftUploaderDetail()
                If SampleNo > 0 Then
                    objTr.SNo = SampleNo
                ElseIf gv1 Is Nothing OrElse gv1.Rows.Count <= 0 Then
                    objTr.SNo = 1
                Else
                    objTr.SNo = gv1.Rows.Count + 1
                End If
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                objTr.VLC_Code = txtVLC.Tag
                'objTr.No_Of_Cans = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNoOfCan).Value)
                objTr.Milk_Weight = txtQty.Value

                objTr.No_Of_Cans = txtNoOfCan.Value
                objTr.BULK_ROUTE_NO = txtBulkRoute.Value
                'If objTr.No_Of_Cans = 0 Then
                '    objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                'End If
                objTr.FAT = Math.Round(txtFAT.Value, 1, MidpointRounding.ToEven)
                objTr.SNF = Math.Round(txtSNF.Value, IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.ToEven)
                objTr.Reject_Type = clsCommon.myCstr(cboRejectType.SelectedValue)
                objTr.PageNo = txtPageNo.Text
                If clsCommon.myLen(cboRejectType.SelectedValue) > 0 Then
                    If clsCommon.myCdbl(clsCommon.myCdbl(cboLate.SelectedValue)) = 1 Then
                        objTr.Reject_Defaulter = "Transporter"
                    Else
                        objTr.Reject_Defaulter = "VSP"
                    End If
                End If
                obj.Arr.Add(objTr)
                obj.SaveData(obj, isNewEntry, True)
                'clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
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
            obj = clsMilkShiftUploaderHead.GetData(strCode, NavTyep, Nothing, True)

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
                cboLate.SelectedValue = obj.Raj_Late
                'SNo.Value = obj.S_No

                'txtDockCode.Value = obj.Dock_Code
                'lblDockName.Text = obj.Dock_Name

                'txRoute.Value = obj.Raj_Bulk_Route_Code
                'lblRoute.Text = obj.Raj_Bulk_Route_Name
                'txtTruckNo.Text = obj.Raj_Truck_no


                'txtTotEnteredQty.Value = obj.Raj_Entered_Qty
                'txtTotEnteredFAT.Value = obj.Raj_Entered_FATKg
                'txtTotEnteredSNF.Value = obj.Raj_Entered_SNFKg

                'txtTotReceivedQty.Text = obj.Raj_Received_Qty
                'txtTotReceivedFAT.Text = obj.Raj_Received_FATKg
                'txtTotReceivedSNF.Text = obj.Raj_Received_SNFKg

                'txtTotPendingQty.Text = (obj.Raj_Entered_Qty - obj.Raj_Received_Qty)
                'txtTotPendingFAT.Text = (obj.Raj_Entered_FATKg - obj.Raj_Received_FATKg)
                'txtTotPendingSNF.Text = (obj.Raj_Entered_SNFKg - obj.Raj_Received_SNFKg)
                'If clsCommon.myCdbl(txtTotPendingQty.Text) <> 0 Then
                '    txtTotPendingFATPer.Text = Math.Round(clsCommon.myCdbl(txtTotPendingFAT.Text) * 100 / clsCommon.myCdbl(txtTotPendingQty.Text), 2)
                '    txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCdbl(txtTotPendingSNF.Text) * 100 / clsCommon.myCdbl(txtTotPendingQty.Text), 2)
                'End If
                If obj.dtRaj IsNot Nothing AndAlso obj.dtRaj.Rows.Count > 0 Then
                    gv1.DataSource = obj.dtRaj
                    gv1.ShowGroupPanel = False
                    gv1.ShowFilteringRow = True
                    gv1.AllowAddNewRow = False
                    'gv1.AllowDeleteRow = False
                    gv1.SummaryRowsBottom.Clear()

                    '=================
                    gvP.DataSource = obj.dtRajPage
                    gvP.ShowGroupPanel = False
                    gvP.ShowFilteringRow = True
                    gvP.AllowAddNewRow = False
                    ' gvP.AllowDeleteRow = False
                    gvP.SummaryRowsBottom.Clear()
                    gvP.ReadOnly = True
                    gvP.BestFitColumns()
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
                            If isPickCLRInsteadOfSNF Then
                                gv1.Columns(ii).HeaderText = gv1.Columns(ii).HeaderText.Replace("SNF", "CLR")
                            End If

                        Catch ex As Exception
                        End Try
                    Next

                    Dim item11 As New GridViewSummaryItem("VLC Code", "Total", GridAggregateFunction.Max)
                    summaryRowItem.Add(item11)

                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    gv1.Columns("TR_No").IsVisible = False
                    gv1.Columns("Route Code").IsVisible = False
                    lblTotEntry.Text = gv1.Rows.Count
                    '===================Page wise Grid Summary total ======================
                    Dim summaryRowItem2 As New GridViewSummaryRowItem()
                    For ii As Integer = 0 To gvP.Columns.Count - 1
                        gvP.Columns(ii).ReadOnly = True
                        gvP.Columns(ii).IsVisible = True
                        gvP.Columns(ii).BestFit()
                        Try
                            If gvP.Columns(ii).Name.Contains(" Qty") OrElse gvP.Columns(ii).Name.Contains(" FATKg") OrElse gvP.Columns(ii).Name.Contains(" SNFKG") Then
                                Dim item1 As New GridViewSummaryItem(gvP.Columns(ii).Name, "{0:F3}", GridAggregateFunction.Sum)
                                summaryRowItem2.Add(item1)
                            ElseIf gvP.Columns(ii).Name.Contains(" FAT %") Then ''OrElse gv1.Columns(ii).Name.Contains(" SNF %")
                                Dim strPrefix As String = gvP.Columns(ii).Name.Replace(" FAT %", "")
                                Dim summaryItem5 As New GridViewSummaryItem()
                                summaryItem5.FormatString = "{0:F2}"
                                summaryItem5.Name = gvP.Columns(ii).Name
                                summaryItem5.AggregateExpression = "sum([" + strPrefix + " FATKg])*100/sum([" + strPrefix + " Qty])"
                                summaryRowItem2.Add(summaryItem5)
                            ElseIf gvP.Columns(ii).Name.Contains(" SNF %") Then
                                Dim strPrefix As String = gvP.Columns(ii).Name.Replace(" SNF %", "")
                                Dim summaryItem5 As New GridViewSummaryItem()
                                summaryItem5.FormatString = "{0:F2}"
                                summaryItem5.Name = gvP.Columns(ii).Name
                                summaryItem5.AggregateExpression = "sum([" + strPrefix + " SNFKg])*100/sum([" + strPrefix + " Qty])"
                                summaryRowItem2.Add(summaryItem5)
                            End If
                            If isPickCLRInsteadOfSNF Then
                                gvP.Columns(ii).HeaderText = gvP.Columns(ii).HeaderText.Replace("SNF", "CLR")
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    gvP.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem2)

                    '======================================================================

                End If
                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtDate.Value)
                For i As Integer = 0 To gv1.Rows.Count - 1
                    gv1.Rows(i).Cells("SNo").Value = (i + 1)
                Next
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

    Private Sub txtMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        'If clsCommon.myLen(txRoute.Value) <= 0 Then
        '    txRoute.Focus()
        '    Throw New Exception("Please provide Route")
        'End If

        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 1 Then
            arrLoc = "'" + obj.Default_LocCode + "'"
        Else
            arrLoc = obj.arrLocCodes
        End If

        'qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master inner join TSPL_BULK_ROUTE_MASTER_MCC on TSPL_BULK_ROUTE_MASTER_MCC.MCC_code=TSPL_MCC_MASTER.MCC_Code and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txRoute.Value + "' LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        '& " and (tspl_location_master.lo   c_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("MiSftUp@M", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + txtMCC.Value + "' "))
        txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtDate.Value)
        txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtDate.Value)
        FetchData()
    End Sub

    Private Sub FetchData()
        Dim qry As String = "Select * from TSPL_MILK_SHIFT_UPLOADER_HEAD Where TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code='" + txtMCC.Value + "' And 
               TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "' And 
               convert (Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)=Convert(Date,'" + txtDate.Value + "',103)"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count = 1 Then
            LoadData(clsCommon.myCstr(dt.Rows(0)("Document_No")), Nothing)
        Else
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            txtDocNo.Value = ""
            isNewEntry = True
            GroupBox76.Visible = False
            txtMCCFromDate.Value = DateTime.Now
            txtMCCToDate.Value = DateTime.Now
            txtFromShift.SelectedValue = "M"
            txtNoOfCan.Value = Nothing
            TxtMultiSelectFinder8.arrValueMember = Nothing
            UsLock1.Status = ERPTransactionStatus.Pending
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gvP.DataSource = Nothing
            gvP.Rows.Clear()
            gv1.Columns.Clear()
            cboLate.SelectedValue = 0
            txtVLC.Value = ""
            txtVLC.Tag = ""
            lblVLC.Text = ""
            cboRejectType.SelectedValue = ""
            txtQty.Text = ""
            txtFAT.Text = ""
            txtSNF.Text = ""
            lblTotEntry.Text = ""
            SampleNo = -1
            lblTotEntry.Text = ""
            txtPaymentCycleNo.Text = ""
            txtFiscalYear.Text = ""
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        Dim qry As String = "select 1 as 'S NO',null as 'VLC Uploader Code',null as 'Qty (Ltr)',null as 'FAT%',null as 'SNF%','' as [Milk Type(M/C/B)],'' as  [Reject Type],'' as [Reject Defaulter]"
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub txtVLC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVLC._MYValidating
        Try
            'If clsCommon.myLen(txtMCC.Value) <= 0 Then
            '    txtMCC.Focus()
            '    Throw New Exception("Please select MCC")
            'End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],Route_name as [Route Name]," _
                & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                + " ,TSPL_VLC_MASTER_HEAD.Village_Code as VillageCode,TSPL_VILLAGE_MASTER.Village_Name as VillageName " _
                & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code "
            Dim whr As String = ""
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                whr = " TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 and TSPL_VLC_MASTER_HEAD.Active=1"
                If Not SettShowAllDCS Then
                    whr += " and tspl_mcc_master.mcc_Code='" & txtMCC.Value & "' "
                End If
            Else
                whr = " TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 and TSPL_VLC_MASTER_HEAD.Active=1 "
            End If
            txtVLC.Value = clsCommon.ShowSelectForm("VLCFNDpt@R", qry, "Uploader_Code", whr, txtVLC.Value, "Uploader_Code", isButtonClicked)
            If clsCommon.myLen(txtVLC.Value) <= 0 Then
                Exit Sub
            End If
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qry &= " where   vlc_code_vlc_uploader ='" & clsCommon.myCstr(txtVLC.Value) & "'"
                If Not SettShowAllDCS Then
                    whr += " And tspl_mcc_master.mcc_Code='" & txtMCC.Value & "'  "
                End If
            Else
                qry &= " where   vlc_code_vlc_uploader ='" & clsCommon.myCstr(txtVLC.Value) & "'"
            End If

            Dim dt_vlc As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dr As DataRow = dt_vlc.Rows(0)
            If Not IsNothing(dr) Then
                txtVLC.Value = clsCommon.myCstr(dr("Uploader_Code"))
                txtVLC.Tag = clsCommon.myCstr(dr("Vlc Code"))
                lblVLC.Text = clsCommon.myCstr(dr("VLC Name"))
                If clsCommon.myLen(txtMCC.Value) <= 0 Then
                    txtMCC.Value = clsCommon.myCstr(dr("MCC Code"))
                    LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + txtMCC.Value + "' "))
                    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtDate.Value)
                    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtDate.Value)
                End If
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
                Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Dock_Collection_Milk_Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight,TSPL_MILK_SHIFT_UPLOADER_DETAIL.FAT,TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNF,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans,TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO as ROUTE_NO,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME 
                from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO
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
                    txtNoOfCan.Value = clsCommon.myCDecimal(dt.Rows(0)("No_Of_Cans"))
                    txtBulkRoute.Value = clsCommon.myCstr(dt.Rows(0)("ROUTE_NO"))
                    lblBulkRoute.Text = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME"))
                    cboDockCollectionMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
                    txtVLC.Focus()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        Try
            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Value, txtDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Value, txtDate.Value)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtMultiSelectFinder8__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder8._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        TxtMultiSelectFinder8.arrValueMember = clsCommon.ShowMultipleSelectForm("BulkMCC@Uti2", qry, "MCC_Code", "MCC_NAME", TxtMultiSelectFinder8.arrValueMember, TxtMultiSelectFinder8.arrDispalyMember)
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

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"

        txtMCCFromDate.Value = DateTime.Now
        txtMCCToDate.Value = txtMCCFromDate.Value
    End Sub

    Private Sub BulkDelete_Click(sender As Object, e As EventArgs) Handles BulkDelete.Click
        If TxtMultiSelectFinder8.arrValueMember Is Nothing OrElse TxtMultiSelectFinder8.arrValueMember.Count < 0 Then
            TxtMultiSelectFinder8.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please First select MCC", Me.Text)
        End If


        If clsCommon.MyMessageBoxShow(Me, "Delete the collection data", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select  * from ExplodeDates('" + clsCommon.GetPrintDate(txtMCCFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtMCCToDate.Value, "dd/MMM/yyyy") + "')"
            Dim dtDate As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dtDate Is Nothing OrElse dtDate.Rows.Count <= 0 Then
                Throw New Exception("Not Date found between from and To Date")
            End If
            For Each drDate As DataRow In dtDate.Rows
                Dim TransDate As String = "'" + clsCommon.GetPrintDate(clsCommon.myCDate(drDate(0)), "dd/MMM/yyyy") + "'"
                For Each strMCCcode As String In TxtMultiSelectFinder8.arrValueMember
                    Dim strShiftCon As String = ""
                    If Not clsCommon.CompairString(clsCommon.myCstr(txtFromShift.SelectedValue), "B") = CompairStringResult.Equal Then
                        strShiftCon = " and SHIFT='" + clsCommon.myCstr(txtFromShift.SelectedValue) + "'"
                    End If

                    qry = "delete from TSPL_MILK_SRN_detail_SYNC where doc_code in (select doc_code from TSPL_MILK_SRN_HEAD_SYNC where mcc_code='" + strMCCcode + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    qry = "delete from TSPL_MILK_SRN_HEAD_SYNC where mcc_code='" + strMCCcode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)


                    qry = "delete from TSPL_PROVISION_ENTRY where Ref_Doc_No in (select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + TransDate + " " + strShiftCon + ") "
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_Shift_End_Route_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + TransDate + " " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_Shift_End_DETAIL where DOC_CODE in( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + TransDate + " " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + TransDate + " " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    '----Milk Sample
                    qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SAMPLE_DETAIL_History where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SAMPLE_READING_LOG where Sample_Code in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    '----End of Milk Sample


                    '----Milk Rejection
                    qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_REJECT_Detail where DOC_CODE in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    '----End of Milk Rejection


                    qry = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_OPEN_MCC_SHIFT where MCC_CODE='" + strMCCcode + "' and convert(date, MCC_SHIFT_DATE,103)=" + TransDate + " " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    'qry = "update TSPL_OPEN_MCC_SHIFT set Status='C'  where MCC_CODE='" + strMCCcode + "'    and Status='O'"
                    'clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    'Milk Shift Uploader - Delete data
                    qry = "delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where DOCUMENT_NO in ( select DOCUMENT_NO from TSPL_MILK_SHIFT_UPLOADER_HEAD where convert(date, SHIFT_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where DOCUMENT_NO in ( select DOCUMENT_NO from TSPL_MILK_SHIFT_UPLOADER_HEAD where convert(date, SHIFT_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_MILK_SHIFT_UPLOADER_HEAD where convert(date, SHIFT_DATE,103)=" + TransDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                Next
            Next

            tran.Commit()
            clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        clsMilkShiftUploaderHead.MultipleDateSingleExport(Me)
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        clsMilkShiftUploaderHead.MultipleDateSingleImport(Me)
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        clsMilkShiftUploaderHead.MultipleDateSingleImportDBF(Me)
    End Sub

    Private Sub txtBulkRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBulkRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            'If Not SettShowAllMCC Then
            '    whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            'End If
            lblBulkRoute.Text = ""
            txtBulkRoute.Value = clsCommon.ShowSelectForm("dd33ShUp", qry, "Code", whrCls, txtBulkRoute.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtBulkRoute.Value) > 0 Then
                'qry = "select  TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Tanker_No,TSPL_TANKER_MASTER.TANKER_NAME from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + txtBulkRoute.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " where ROUTE_NO='" + txtBulkRoute.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblBulkRoute.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Dim frm As New frmMakeTempleteImportMP(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, "IMP-DBF")
        'frm.dtColsExcel = New DataTable
        'frm.dtColsExcel.Columns.Add("Code", GetType(String))
        'For ii As Integer = 0 To gv1.Columns.Count - 1
        '    If gv1.Columns(ii).IsVisible Then
        '        Dim dr As DataRow = frm.dtColsExcel.NewRow()
        '        dr("Code") = gv1.Columns(ii).Name
        '        frm.dtColsExcel.Rows.Add(dr)
        '    End If
        'Next
        frm.arrColsOrginal = New Dictionary(Of String, Boolean)
        frm.arrColsOrginal.Add(clsDBFTemplate.DDate, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.Shift, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.Route, False)
        frm.arrColsOrginal.Add(clsDBFTemplate.DCS, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.Qty, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.FAT, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.SNF, True)
        frm.arrColsOrginal.Add(clsDBFTemplate.EmpatyCAN, False)
        frm.arrColsOrginal.Add(clsDBFTemplate.DockCollectionMilkType, False)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Code from TSPL_MILK_REJECT_TYPE")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                frm.arrColsOrginal.Add(clsCommon.myCstr(dr("Code")) + "#" + clsDBFTemplate.Qty, False)
                frm.arrColsOrginal.Add(clsCommon.myCstr(dr("Code")) + "#" + clsDBFTemplate.FAT, False)
                frm.arrColsOrginal.Add(clsCommon.myCstr(dr("Code")) + "#" + clsDBFTemplate.SNF, False)
            Next
        End If
        frm.strDocNoForOpen = ""
        frm.IsDefaultValue = True
        frm.ShowDialog()
    End Sub
    Private Sub txtDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDate.KeyPress
        If e.Handled = Keys.Enter Then
            cboShift.Focus()
        End If
    End Sub

    Private Sub cboShift_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboShift.KeyPress
        If e.Handled = Keys.Enter Then
            cboLate.Focus()
        End If
    End Sub
    Private Sub cboLate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboLate.KeyPress
        If e.Handled = Keys.Enter Then
            txtVLC.Focus()
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If gv1.Rows.Count > 0 Then
                If UsLock1.Status = ERPTransactionStatus.Approved Then
                    clsCommon.MyMessageBoxShow(Me, "Document already posted [" + txtDocNo.Value + "]", Me.Text)
                    e.Cancel = True
                Else
                    If clsCommon.MyMessageBoxShow(Me, "Delete the current row data." + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        clsMilkShiftUploaderQCParameterDetail.DeleteRowData(clsCommon.myCstr(gv1.CurrentRow.Cells("TR_No").Value))
                        clsCommon.MyMessageBoxShow(Me, "Data deleted successfully", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    Else
                        e.Cancel = True
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Row not found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(i).Cells("SNo").Value = (i + 1)
        Next
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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        print(txtDocNo.Value)
    End Sub
    Public Sub print(ByVal StrDocNo As String)
        Dim strquery As String = Nothing
        Try

            strquery = "select TSPL_COMPANY_MASTER.Comp_Name,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No)TR_No,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo)SNo,(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC, (TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code) as [VLC Code],(TSPL_VLC_MASTER_HEAD.VLC_Name) as [VLC Name],(TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans) as [No of Cans],(TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO) as [Route Code],(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME) as [Route],(TSPL_BULK_ROUTE_MASTER.Tanker_No)Tanker_No,(TSPL_BULK_ROUTE_MASTER.Comp_Code)Comp_Code,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Mix_Milk)Mix_Milk,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift)Shift,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)Shift_Date,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Created_Date)Created_Date,(TSPL_BULK_ROUTE_MASTER.Schedule_Time_Morning)Schedule_Time_Morning,(TSPL_MCC_MASTER.mcc_Name) as mcc_Name,
case When (isnull(Reject_Type,''))='' then (isnull(No_Of_Cans,0)) else 0 end as [Good can qty]
,case When (isnull(Reject_Type,''))='' then (isnull(Milk_Weight,0)) else 0 end as [Good Qty]
,case When (isnull(Reject_Type,''))='' then (FAT) else 0 end as [Good FAT %]
,case When (isnull(Reject_Type,''))='' then (cast(Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [Good FATKg]
,case When (isnull(Reject_Type,''))='' then (SNF) else 0 end as [Good SNF %]
,case When (isnull(Reject_Type,''))='' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [Good SNFKG],
case When (isnull(Reject_Type,''))='SOUR' then (isnull(No_Of_Cans,0)) else 0 end as [SOUR can qty],
case When (isnull(Reject_Type,''))='SOUR' then (Milk_Weight) else 0 end as [SOUR Qty]
,case When (isnull(Reject_Type,''))='SOUR' then (FAT) else 0 end as [SOUR FAT %]
,case When (isnull(Reject_Type,''))='SOUR' then (cast (Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [SOUR FATKg]
,case When (isnull(Reject_Type,''))='SOUR' then (SNF) else 0 end as [SOUR SNF %]
,case When (isnull(Reject_Type,''))='SOUR' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [SOUR SNFKG],
case When (isnull(Reject_Type,''))='CURD' then (isnull(No_Of_Cans,0)) else 0 end as [CURD can qty],
case When (isnull(Reject_Type,''))='CURD' then (Milk_Weight) else 0 end as [CURD Qty]
,case When (isnull(Reject_Type,''))='CURD' then (FAT) else 0 end as [CURD FAT %]
,case When (isnull(Reject_Type,''))='CURD' then (cast (Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [CURD FATKg]
,case When (isnull(Reject_Type,''))='CURD' then (SNF) else 0 end as [CURD SNF %]
,case When (isnull(Reject_Type,''))='CURD' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [CURD SNFKG] ,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.PageNo)PageNo from TSPL_MILK_SHIFT_UPLOADER_DETAIL
left join  TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_MASTER_HEAD.Comp_Code
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO
left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code
where TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No='" + StrDocNo + "' ORDER BY SNO "

            If strquery IsNot Nothing AndAlso clsCommon.myLen(strquery) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "Milkshiftprint", "Bill Of Supply")
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRouteprint_Click(sender As Object, e As EventArgs) Handles btnRouteprint.Click
        Routeprint(txtDocNo.Value)
    End Sub

    Public Sub Routeprint(ByVal StrDocNo As String)
        Dim strquery As String = Nothing
        Try

            strquery = "select xx.TR_No,xx.SNo,xx.VLC,xx.VLC_name,xx.No_of_Cans,xx.Route,xx.Tanker_No,xx.Comp_Code,xx.Mix_Milk,xx.Shift,xx.Shift_Date,xx.Created_Date,
                        xx.Schedule_Time_Morning,xx.Good_Qty,xx.Good_FAT,xx.Good_FATKg,xx.Good_SNF,xx.Good_SNFKG,xx.SOUR_Qty,xx.SOUR_FAT,xx.SOUR_FATKg,xx.SOUR_SNF,
                        xx.SOUR_SNFKG,xx.CURD_Qty,xx.CURD_FAT,xx.CURD_FATKg,xx.CURD_SNFKG,xx.PageNo,xx.Route_Code,xx.mcc_Name,TSPL_COMPANY_MASTER.Comp_Name,
                        TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Add3,'" + objCommonVar.CurrentUser + "' as User_Name from (
                        Select Max(xxx.TR_No)TR_No,Max(xxx.SNo)SNo,Max(xxx.VLC)VLC,Max(xxx.[VLC Name])VLC_name,sum(xxx.[No of Cans])No_of_Cans,Max(xxx.[Route])Route,
                        Max(xxx.Tanker_No)Tanker_No,Max(xxx.Comp_Code)Comp_Code,Max(xxx.Mix_Milk)Mix_Milk,Max(xxx.[Shift])Shift,Max(xxx.Shift_Date)Shift_Date,
                        Max(xxx.Created_Date)Created_Date,Max(xxx.Schedule_Time_Morning)Schedule_Time_Morning,
                        sum(xxx.[Good Qty])Good_Qty,Max(xxx.[Good FAT %])Good_FAT,sum(xxx.[Good FATKg])Good_FATKg,sum(xxx.[Good SNF %])Good_SNF,
                        sum(xxx.[Good SNFKG])Good_SNFKG,sum(xxx.[SOUR Qty])SOUR_Qty,Max(xxx.[SOUR FAT %])SOUR_FAT,sum(xxx.[SOUR FATKg])SOUR_FATKg,
                        Max(xxx.[SOUR SNF %])SOUR_SNF,sum(xxx.[SOUR SNFKG])SOUR_SNFKG,sum(xxx.[CURD Qty])CURD_Qty,Max(xxx.[CURD FAT %])CURD_FAT,
                        sum(xxx.[CURD FATKg])CURD_FATKg,sum(xxx.[CURD SNFKG])CURD_SNFKG,Max(xxx.[PageNo])PageNo,Max(xxx.[Route Code])Route_Code,
                        Max(xxx.[mcc_Name])mcc_Name   
                        from
                                (select (TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No)TR_No,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo)SNo,(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC, (TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code) as [VLC Code],(TSPL_VLC_MASTER_HEAD.VLC_Name) as [VLC Name],(TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans) as [No of Cans],(TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO) as [Route Code],(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME) as [Route],(TSPL_BULK_ROUTE_MASTER.Tanker_No)Tanker_No,(TSPL_BULK_ROUTE_MASTER.Comp_Code)Comp_Code,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Mix_Milk)Mix_Milk,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift)Shift,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)Shift_Date,(TSPL_MILK_SHIFT_UPLOADER_HEAD.Created_Date)Created_Date,(TSPL_BULK_ROUTE_MASTER.Schedule_Time_Morning)Schedule_Time_Morning,(TSPL_MCC_MASTER.mcc_Name) as mcc_Name 
                                ,case When (isnull(Reject_Type,''))='' then (isnull(Milk_Weight,0)) else 0 end as [Good Qty]
                                ,case When (isnull(Reject_Type,''))='' then (FAT) else 0 end as [Good FAT %]
                                ,case When (isnull(Reject_Type,''))='' then (cast(Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [Good FATKg]
                                ,case When (isnull(Reject_Type,''))='' then (SNF) else 0 end as [Good SNF %]
                                ,case When (isnull(Reject_Type,''))='' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [Good SNFKG],case When (isnull(Reject_Type,''))='SOUR' then (Milk_Weight) else 0 end as [SOUR Qty]
                                ,case When (isnull(Reject_Type,''))='SOUR' then (FAT) else 0 end as [SOUR FAT %]
                                ,case When (isnull(Reject_Type,''))='SOUR' then (cast (Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [SOUR FATKg]
                                ,case When (isnull(Reject_Type,''))='SOUR' then (SNF) else 0 end as [SOUR SNF %]
                                ,case When (isnull(Reject_Type,''))='SOUR' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [SOUR SNFKG],case When (isnull(Reject_Type,''))='CURD' then (Milk_Weight) else 0 end as [CURD Qty]
                                ,case When (isnull(Reject_Type,''))='CURD' then (FAT) else 0 end as [CURD FAT %]
                                ,case When (isnull(Reject_Type,''))='CURD' then (cast (Milk_Weight*FAT/100 as decimal(18,3))) else 0 end as [CURD FATKg]
                                ,case When (isnull(Reject_Type,''))='CURD' then (SNF) else 0 end as [CURD SNF %]
                                ,case When (isnull(Reject_Type,''))='CURD' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [CURD SNFKG] ,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.PageNo)PageNo from TSPL_MILK_SHIFT_UPLOADER_DETAIL
                                left join  TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
                                left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO
                                left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code
                                where TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No='" + StrDocNo + "' ) xxx group by xxx.[Route Code] )xx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=xx.comp_code "

            If strquery IsNot Nothing AndAlso clsCommon.myLen(strquery) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "Milkshiftprintsec", "Bill Of Supply")
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub




End Class
