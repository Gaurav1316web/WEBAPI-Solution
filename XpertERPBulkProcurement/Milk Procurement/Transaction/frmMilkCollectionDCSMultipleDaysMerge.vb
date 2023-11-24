Imports System.Data.SqlClient
Imports common
Imports Telerik

Public Class frmMilkCollectionDCSMultipleDaysMerge
    Inherits FrmMainTranScreen
    '#Region "Variables"
    '    Dim dtDefault As DataTable = Nothing
    '    Dim arrExistCols As New List(Of String)
    '    Dim SettShowAllMCC As Boolean
    '    Dim settFillRouteTankerNo As Boolean = False
    '    Dim isNewEntry As Boolean = False
    '    Const ColSNo As String = "ColSNo"
    '    Const colCollectionDate As String = "colCollectionDate"
    '    Const colMilkType As String = "colMilkType"
    '    Const colDocCollectionMilkType As String = "colDocCollectionMilkType"
    '    Const colVLCUploaderCode As String = "colVLCUploaderCode"
    '    Const colVLCCode As String = "colVLCCode"
    '    Const colVLCName As String = "colVLCName"
    '    Const colEveningPKID As String = "colEveningPKID"
    '    Const colEveningQty As String = "colEveningQty"
    '    Const colEveningFATPerNoDecimal As String = "colEveningFATPerNoDecimal"
    '    Const colEveningSNFPerNoDecimal As String = "colEveningSNFPerNoDecimal"
    '    Const colEveningFATPer As String = "colEveningFATPer"
    '    Const colEveningSNFPer As String = "colEveningSNFPer"
    '    Const colEveningFATKG As String = "colEveningFATKG"
    '    Const colEveningSNFKG As String = "colEveningSNFKG"
    '    Const colMorningPKID As String = "colMorningPKID"
    '    Const colMorningQty As String = "colMorningQty"
    '    Const colMorningFATPerNoDecimal As String = "colMorningFATPerNoDecimal"
    '    Const colMorningSNFPerNoDecimal As String = "colMorningSNFPerNoDecimal"
    '    Const colMorningFATPer As String = "colMorningFATPer"
    '    Const colMorningSNFPer As String = "colMorningSNFPer"
    '    Const colMorningFATKG As String = "colMorningFATKG"
    '    Const colMorningSNFKG As String = "colMorningSNFKG"

    '    Dim isInsideLoadData As Boolean = False
    '    Dim isCellValueChangedOpen As Boolean = False

    '    Dim SettMilkCollectionFATSNFType As Integer
    '    Dim SettFATSNFNoDecimalDCS As Boolean
    '    Dim SettShowAllDCS As Boolean
    '    Dim SettHideShiftCollection As Integer ''1-Hide Evending;2-Hide Morning else Both
    '    Dim settSNFDecimalPlace As Integer = 0
    '    Dim SettHeaderFATSNFKGDecimalPlaces As Integer = 3
    '    Dim isPickCLRInsteadOfSNF As Boolean = False
    '    Dim settMaxFATPerLimit As Decimal = 0
    '    Dim settMaxSNFPerLimit As Decimal = 0
    '    Dim corrFactor As Decimal = 0
    '    Dim SettMilkCollectionFATSNFTypeHeader As Integer

    '#End Region
    '    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        Dim coll As New Dictionary(Of String, String)
    '        coll.Add("Document_No", "Varchar(30) not null Primary key")
    '        coll.Add("Document_Date", "Datetime NOT NULL")
    '        coll.Add("Route_Code", "Varchar(30) not null references TSPL_BULK_ROUTE_MASTER(ROUTE_NO)")
    '        coll.Add("Tanker_No", "Varchar(20) not null references TSPL_TANKER_MASTER(Tanker_No)")
    '        coll.Add("Entered_Qty", "Decimal(18,3) null")
    '        coll.Add("Entered_FATKg", "Decimal(18,3) null")
    '        coll.Add("Entered_SNFKg", "Decimal(18,3) null")
    '        coll.Add("Description", "Varchar(200) null")
    '        coll.Add("FAT_SNF_Type", "int Null")
    '        coll.Add("Status", "Integer NOT NULL DEFAULT 0")
    '        coll.Add("Created_By", "varchar(12) NOT NULL")
    '        coll.Add("Created_Date", "Datetime NOT NULL")
    '        coll.Add("Modified_By", "varchar(12) NOT NULL")
    '        coll.Add("Modified_Date", "Datetime NOT NULL")
    '        coll.Add("Posted_Date", "datetime null")
    '        coll.Add("Posted_By", "varchar(12)  NULL")
    '        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", coll, Nothing, True, False, "", "Document_No", "Document_Date")

    '        coll = New Dictionary(Of String, String)
    '        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
    '        coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE(Document_No)")
    '        coll.Add("Against_DCS_Multiple_Days", "Varchar(30) not null unique references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS(Document_No)")
    '        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DOCS", coll, Nothing, True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", "Document_No", "")

    '        coll = New Dictionary(Of String, String)
    '        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
    '        coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE(Document_No)")
    '        coll.Add("IDate", "Date NOT NULL")
    '        coll.Add("Qty", "Decimal(18,2) null")
    '        coll.Add("FATKG", "Decimal(18,3) null")
    '        coll.Add("SNFKG", "Decimal(18,3) null")
    '        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE_DAY_DETAIL", coll, Nothing, True, False, "TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_MERGE", "Document_No", "")



    '        SettShowAllMCC = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Nothing)) = 1)
    '        settFillRouteTankerNo = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FillRouteTankerNo, clsFixedParameterCode.FillRouteTankerNo, Nothing)) = 1)
    '        corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
    '        isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
    '        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
    '        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
    '        SettMilkCollectionFATSNFType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
    '        SettFATSNFNoDecimalDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFNoDecimalDCS, clsFixedParameterCode.FATSNFNoDecimalDCS, Nothing))
    '        SettShowAllDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllDCS, clsFixedParameterCode.ShowAllDCS, Nothing))
    '        SettHideShiftCollection = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HideShiftCollection, clsFixedParameterCode.HideShiftCollection, Nothing))
    '        settSNFDecimalPlace = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, Nothing))
    '        SettHeaderFATSNFKGDecimalPlaces = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HeaderFATSNFKGDecimalPlaces, clsFixedParameterCode.HeaderFATSNFKGDecimalPlaces, Nothing))
    '        SettMilkCollectionFATSNFTypeHeader = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFTypeHeader, clsFixedParameterCode.MilkCollectionFATSNFTypeHeader, Nothing))
    '        If isPickCLRInsteadOfSNF Then
    '            MyLabel14.Text = "CLR"
    '        End If
    '        MyBase.SetUserMgmt(clsUserMgtCode.MilkCollectionDCS)
    '        LoadFATSNFType()
    '        txtDate.Value = clsCommon.GETSERVERDATE()
    '        AddNew()
    '        If SettMilkCollectionFATSNFTypeHeader = 0 Then
    '            txtTotEnteredFATPer.Enabled = True
    '            txtTotEnteredSNFPer.Enabled = True
    '            txtTotEnteredFAT.Enabled = False
    '            txtTotEnteredSNF.Enabled = False
    '        Else
    '            txtTotEnteredFATPer.Enabled = False
    '            txtTotEnteredSNFPer.Enabled = False
    '            txtTotEnteredFAT.Enabled = True
    '            txtTotEnteredSNF.Enabled = True
    '        End If
    '    End Sub
    '    Public Sub LoadFATSNFType()
    '        Dim dt As DataTable = New DataTable()
    '        dt.Columns.Add("Code", GetType(String))
    '        dt.Columns.Add("Name", GetType(String))

    '        Dim dr As DataRow = dt.NewRow()
    '        dr("Code") = "0"
    '        dr("Name") = "%"
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("Code") = "1"
    '        dr("Name") = "KG"
    '        dt.Rows.Add(dr)

    '        cboFATSNFType.DataSource = dt
    '        cboFATSNFType.ValueMember = "Code"
    '        cboFATSNFType.DisplayMember = "Name"
    '    End Sub
    '    Public Function LoadShift() As DataTable
    '        Dim dt As DataTable = New DataTable()
    '        dt.Columns.Add("Code", GetType(String))
    '        dt.Columns.Add("Name", GetType(String))

    '        Dim dr As DataRow = dt.NewRow()
    '        dr("Code") = "M"
    '        dr("Name") = "Morning"
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("Code") = "E"
    '        dr("Name") = "Evening"
    '        dt.Rows.Add(dr)

    '        Return dt
    '    End Function
    '    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
    '        AddNew()
    '    End Sub
    '    Sub AddNew()
    '        btnSave.Enabled = True
    '        btnPost.Enabled = True
    '        btnDelete.Enabled = True

    '        If SettMilkCollectionFATSNFType = 0 Then
    '            cboFATSNFType.SelectedValue = "0"
    '        Else
    '            cboFATSNFType.SelectedValue = "1"
    '        End If
    '        txtDocNo.Value = ""

    '        txtDesc.Text = ""

    '        txtMCC.Tag = Nothing
    '        txtMCC.Value = ""
    '        lblMCC.Text = ""
    '        txtRoute.Value = ""
    '        lblRoute.Text = ""
    '        txtTankerNo.Value = ""
    '        txtVehicleNo.Text = ""
    '        txtTotEnteredQty.Text = ""

    '        isNewEntry = True
    '        txtMCC.Value = ""
    '        lblMCC.Text = ""

    '        txtTotReceivedQty.Text = ""
    '        txtTotPendingQty.Text = ""
    '        txtTotEnteredFATPer.Text = ""
    '        txtTotEnteredFAT.Text = ""
    '        txtTotReceivedFAT.Text = ""
    '        txtTotPendingFAT.Text = ""
    '        txtTotEnteredSNFPer.Text = ""
    '        txtTotEnteredSNF.Text = ""
    '        txtTotReceivedSNF.Text = ""
    '        txtTotPendingSNF.Text = ""
    '        txtTotPendingFATPer.Text = ""
    '        txtTotPendingSNFPer.Text = ""

    '        UsLock1.Status = ERPTransactionStatus.Pending
    '        LoadBlankGrid()
    '        gv1.Rows.AddNew()
    '        gv1.Rows(0).Cells(colMilkType).Value = "Good"
    '        txtDate.Enabled = True
    '        txtMCC.Focus()
    '    End Sub
    '    Sub LoadBlankGrid()
    '        gv1.Rows.Clear()
    '        gv1.Columns.Clear()

    '        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = ""
    '        repoNumBox.HeaderText = "SNo"
    '        repoNumBox.Name = ColSNo
    '        repoNumBox.Width = 40
    '        repoNumBox.ReadOnly = True
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
    '        repoDate.Format = DateTimePickerFormat.Custom
    '        repoDate.CustomFormat = "dd/MM/yyyy"
    '        repoDate.HeaderText = "Collection Date"
    '        repoDate.FormatString = "{0:d}"
    '        repoDate.Name = colCollectionDate
    '        repoDate.WrapText = True
    '        repoDate.ReadOnly = False
    '        repoDate.Width = 80
    '        gv1.MasterTemplate.Columns.Add(repoDate)

    '        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    '        repoComboBox = New GridViewComboBoxColumn()
    '        repoComboBox.FormatString = ""
    '        repoComboBox.HeaderText = "Milk Type"
    '        repoComboBox.Name = colMilkType
    '        repoComboBox.Width = 100
    '        repoComboBox.ReadOnly = False
    '        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '        repoComboBox.DataSource = clsMilkReceiptMCC.GetReject(True)
    '        repoComboBox.ValueMember = "Code"
    '        repoComboBox.DisplayMember = "Name"
    '        gv1.MasterTemplate.Columns.Add(repoComboBox)


    '        repoComboBox = New GridViewComboBoxColumn()
    '        repoComboBox.FormatString = ""
    '        repoComboBox.HeaderText = "Type"
    '        repoComboBox.Name = colDocCollectionMilkType
    '        repoComboBox.Width = 100
    '        repoComboBox.ReadOnly = Not objCommonVar.DisplayTypeInMilkReceipt
    '        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '        repoComboBox.DataSource = clsMilkReceiptMCC.GetMilkType(False)
    '        repoComboBox.ValueMember = "Code"
    '        repoComboBox.DisplayMember = "Name"
    '        repoComboBox.IsVisible = objCommonVar.DisplayTypeInMilkReceipt
    '        gv1.MasterTemplate.Columns.Add(repoComboBox)

    '        Dim repoTextBox2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '        repoTextBox2.FormatString = ""
    '        repoTextBox2.HeaderText = "DCS/PDCS Code"
    '        repoTextBox2.Name = colVLCUploaderCode
    '        repoTextBox2.HeaderImage = Global.XpertERPBulkProcurement.My.Resources.Resources.search4
    '        repoTextBox2.TextImageRelation = TextImageRelation.TextBeforeImage
    '        repoTextBox2.Width = 150
    '        gv1.MasterTemplate.Columns.Add(repoTextBox2)

    '        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '        repoTextBox.FormatString = ""
    '        repoTextBox.HeaderText = "DCS/PDCS Code"
    '        repoTextBox.Name = colVLCCode
    '        repoTextBox.IsVisible = False
    '        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
    '        repoTextBox.ReadOnly = True
    '        gv1.MasterTemplate.Columns.Add(repoTextBox)

    '        repoTextBox = New GridViewTextBoxColumn()
    '        repoTextBox.FormatString = ""
    '        repoTextBox.HeaderText = "DCS/PDCS Name"
    '        repoTextBox.Name = colVLCName
    '        repoTextBox.Width = 200
    '        repoTextBox.IsVisible = True
    '        repoTextBox.ReadOnly = True
    '        gv1.MasterTemplate.Columns.Add(repoTextBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = ""
    '        repoNumBox.HeaderText = "PKID Evening"
    '        repoNumBox.Name = colEveningPKID
    '        repoNumBox.IsVisible = False
    '        repoNumBox.ReadOnly = True
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n3}"
    '        repoNumBox.HeaderText = "Evening Qty"
    '        repoNumBox.Name = colEveningQty
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettHideShiftCollection <> 1)
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n0}"
    '        repoNumBox.HeaderText = "FAT"
    '        repoNumBox.Name = colEveningFATPerNoDecimal
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 0
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n0}"
    '        repoNumBox.HeaderText = "SNF"
    '        repoNumBox.Name = colEveningSNFPerNoDecimal
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 0
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n1}"
    '        repoNumBox.HeaderText = "Evening Fat %"
    '        repoNumBox.Name = colEveningFATPer
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 15
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 1
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n" + clsCommon.myCstr(settSNFDecimalPlace) + "}"
    '        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "Evening CLR %", "Evening SNF %")
    '        repoNumBox.Name = colEveningSNFPer
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = IIf(isPickCLRInsteadOfSNF, 50, 15)
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = settSNFDecimalPlace
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n3}"
    '        repoNumBox.HeaderText = "Evening Fat KG"
    '        repoNumBox.Name = colEveningFATKG
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 9999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n3}"
    '        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "Evening CLR KG", "Evening SNF KG")
    '        repoNumBox.Name = colEveningSNFKG
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 9999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 1))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = ""
    '        repoNumBox.HeaderText = "PKID Morning"
    '        repoNumBox.Name = colMorningPKID
    '        repoNumBox.IsVisible = False
    '        repoNumBox.ReadOnly = True
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = ""
    '        repoNumBox.HeaderText = "Morning Qty"
    '        repoNumBox.Name = colMorningQty
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettHideShiftCollection <> 2)
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n0}"
    '        repoNumBox.HeaderText = "FAT"
    '        repoNumBox.Name = colMorningFATPerNoDecimal
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 0
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n0}"
    '        repoNumBox.HeaderText = "SNF"
    '        repoNumBox.Name = colMorningSNFPerNoDecimal
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 0
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = (SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n1}"
    '        repoNumBox.HeaderText = "Morning Fat %"
    '        repoNumBox.Name = colMorningFATPer
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 15
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 1
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n" + clsCommon.myCstr(settSNFDecimalPlace) + "}"
    '        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "Morning CLR %", "Morning SNF %")
    '        repoNumBox.Name = colMorningSNFPer
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = IIf(isPickCLRInsteadOfSNF, 50, 15)
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = settSNFDecimalPlace
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n3}"
    '        repoNumBox.HeaderText = "Morning Fat KG"
    '        repoNumBox.Name = colMorningFATKG
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 9999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        repoNumBox = New GridViewDecimalColumn()
    '        repoNumBox.FormatString = "{0:n3}"
    '        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "Morning CLR KG", "Morning SNF KG")
    '        repoNumBox.Name = colMorningSNFKG
    '        repoNumBox.Width = 100
    '        repoNumBox.Minimum = 0
    '        repoNumBox.Maximum = 9999
    '        repoNumBox.ShowUpDownButtons = False
    '        repoNumBox.Step = 0
    '        repoNumBox.DecimalPlaces = 3
    '        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalDCS AndAlso (SettHideShiftCollection <> 2))
    '        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        gv1.AllowAddNewRow = False
    '        gv1.ShowGroupPanel = False
    '        gv1.AllowColumnReorder = False
    '        gv1.AllowRowReorder = False
    '        gv1.EnableSorting = False
    '        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '        gv1.MasterTemplate.ShowRowHeaderColumn = False
    '        gv1.TableElement.TableHeaderHeight = 40

    '        gv1.AllowDeleteRow = True
    '        setShiftDate()
    '        'ReStoreGridLayout()
    '    End Sub
    '    Sub UpdateCurrentRow(ByVal Shift As String, ByVal ii As Integer)
    '        If clsCommon.CompairString(Shift, "E") = CompairStringResult.Equal Then
    '            If clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 0 Then
    '                gv1.Rows(ii).Cells(colEveningFATKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningFATPer).Value) / 100, 3, MidpointRounding.ToEven)
    '                gv1.Rows(ii).Cells(colEveningSNFKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningSNFPer).Value) / 100, 3, MidpointRounding.ToEven)
    '            ElseIf clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 1 Then
    '                gv1.Rows(ii).Cells(colEveningFATPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningFATKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value), 1, MidpointRounding.ToEven)
    '                gv1.Rows(ii).Cells(colEveningSNFPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningSNFKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value), 2, MidpointRounding.ToEven)
    '            End If
    '        Else
    '            If clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 0 Then
    '                gv1.Rows(ii).Cells(colMorningFATKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningFATPer).Value) / 100, 3, MidpointRounding.ToEven)
    '                gv1.Rows(ii).Cells(colMorningSNFKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningSNFPer).Value) / 100, 3, MidpointRounding.ToEven)
    '            ElseIf clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 1 Then
    '                gv1.Rows(ii).Cells(colMorningFATPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningFATKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value), 1, MidpointRounding.ToEven)
    '                gv1.Rows(ii).Cells(colMorningSNFPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningSNFKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value), 2, MidpointRounding.ToEven)
    '            End If
    '        End If
    '        UpdateAllTotal()
    '    End Sub
    '    Private Sub UpdateAllTotal()
    '        Dim TotEveningQty As Decimal = 0
    '        Dim TotEveningFATKG As Decimal = 0
    '        Dim TotEveningSNFKG As Decimal = 0
    '        Dim TotMorningQty As Decimal = 0
    '        Dim TotMorningFATKG As Decimal = 0
    '        Dim TotMorningSNFKG As Decimal = 0

    '        For ii As Integer = 0 To gv1.Rows.Count - 1
    '            If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value) > 0 OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value) Then
    '                TotEveningQty += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value)
    '                TotEveningFATKG += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningFATKG).Value)


    '                TotMorningQty += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value)
    '                TotMorningFATKG += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningFATKG).Value)


    '                Dim dclCurrSNFKGE As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningSNFKG).Value)
    '                Dim dclCurrSNFKGM As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningSNFKG).Value)
    '                If isPickCLRInsteadOfSNF Then
    '                    Dim snfPer As Decimal = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningFATPer).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningSNFPer).Value), corrFactor)
    '                    dclCurrSNFKGE = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningQty).Value) * snfPer / 100
    '                    gv1.Rows(ii).Cells(colEveningSNFKG).Value = dclCurrSNFKGE

    '                    snfPer = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningFATPer).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningSNFPer).Value), corrFactor)
    '                    dclCurrSNFKGM = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value) * snfPer / 100
    '                    gv1.Rows(ii).Cells(colMorningSNFKG).Value = dclCurrSNFKGM
    '                End If

    '                TotEveningSNFKG += dclCurrSNFKGE
    '                TotMorningSNFKG += dclCurrSNFKGM
    '            End If
    '        Next
    '        'txtTotReceivedQty.Text = (TotEveningQty + TotMorningQty)
    '        'txtTotReceivedFAT.Text = clsCommon.myCstr(Math.Round((TotEveningFATKG + TotMorningFATKG), SettHeaderFATSNFKGDecimalPlaces, MidpointRounding.ToEven))
    '        'txtTotReceivedSNF.Text = clsCommon.myCstr(Math.Round((TotEveningSNFKG + TotMorningSNFKG), SettHeaderFATSNFKGDecimalPlaces, MidpointRounding.ToEven))

    '        'txtTotPendingQty.Text = clsCommon.myCstr(clsCommon.myCDecimal(txtTotReceivedQty.Text) - (TotEveningQty + TotMorningQty))
    '        'txtTotPendingFAT.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(txtTotEnteredFAT.Text) - (TotEveningFATKG + TotMorningFATKG)), SettHeaderFATSNFKGDecimalPlaces, MidpointRounding.ToEven))
    '        'txtTotPendingSNF.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(txtTotEnteredSNF.Text) - (TotEveningSNFKG + TotMorningSNFKG)), SettHeaderFATSNFKGDecimalPlaces, MidpointRounding.ToEven))


    '        'txtTotPendingFATPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingFAT.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)
    '        'txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingSNF.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)


    '        txtTotReceivedQty.Text = clsCommon.myCstr(Math.Round((TotEveningQty + TotMorningQty), 3, MidpointRounding.ToEven))
    '        txtTotReceivedFAT.Text = clsCommon.myCstr(Math.Round((TotEveningFATKG + TotMorningFATKG), 3, MidpointRounding.ToEven))
    '        txtTotReceivedSNF.Text = clsCommon.myCstr(Math.Round((TotEveningSNFKG + TotMorningSNFKG), 3, MidpointRounding.ToEven))
    '        If SettMilkCollectionFATSNFTypeHeader = 0 Then
    '            txtTotEnteredFAT.Value = Math.Round((txtTotEnteredQty.Value * txtTotEnteredFATPer.Value / 100), 3, MidpointRounding.ToEven)
    '            Dim snfPer As Decimal = txtTotEnteredSNFPer.Value
    '            If isPickCLRInsteadOfSNF Then
    '                snfPer = clsEkoPro.getSnfOnCalculation(txtTotEnteredFATPer.Value, txtTotEnteredSNFPer.Value, corrFactor)
    '            End If
    '            txtTotEnteredSNF.Value = Math.Round((txtTotEnteredQty.Value * snfPer / 100), 3, MidpointRounding.ToEven)
    '        Else
    '            txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
    '            txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
    '        End If
    '        txtTotPendingQty.Text = clsCommon.myCstr(Math.Round((txtTotEnteredQty.Value - (TotEveningQty + TotMorningQty)), 3, MidpointRounding.ToEven))
    '        txtTotPendingFAT.Text = clsCommon.myCstr(Math.Round((txtTotEnteredFAT.Value - (TotEveningFATKG + TotMorningFATKG)), 3, MidpointRounding.ToEven))
    '        txtTotPendingSNF.Text = clsCommon.myCstr(Math.Round((txtTotEnteredSNF.Value - (TotEveningSNFKG + TotMorningSNFKG)), 3, MidpointRounding.ToEven))

    '        txtTotPendingFATPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingFAT.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)
    '        txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingSNF.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 1, MidpointRounding.ToEven)

    '    End Sub
    '    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
    '        Try
    '            SetGridFocus()
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
    '        Try
    '            If (Not isInsideLoadData) Then
    '                If Not isCellValueChangedOpen Then
    '                    isCellValueChangedOpen = True
    '                    If e.Column Is gv1.Columns(colVLCUploaderCode) Then
    '                        OpenVLCFinder(False)
    '                    ElseIf e.Column Is gv1.Columns(colEveningFATPerNoDecimal) Then
    '                        gv1.CurrentRow.Cells(colEveningFATPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colEveningFATPerNoDecimal).Value)
    '                        If settMaxFATPerLimit > 0 Then
    '                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colEveningFATPer).Value) > settMaxFATPerLimit Then
    '                                clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ".", Me.Text)
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("E", gv1.CurrentRow.Index)
    '                    ElseIf e.Column Is gv1.Columns(colEveningSNFPerNoDecimal) Then
    '                        gv1.CurrentRow.Cells(colEveningSNFPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colEveningSNFPerNoDecimal).Value)
    '                        If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
    '                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colEveningSNFPer).Value) > settMaxSNFPerLimit Then
    '                                clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ".", Me.Text)
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("E", gv1.CurrentRow.Index)
    '                    ElseIf e.Column Is gv1.Columns(colMorningFATPerNoDecimal) Then
    '                        gv1.CurrentRow.Cells(colMorningFATPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colMorningFATPerNoDecimal).Value)
    '                        If settMaxFATPerLimit > 0 Then
    '                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorningFATPer).Value) > settMaxFATPerLimit Then
    '                                clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ".", Me.Text)
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("M", gv1.CurrentRow.Index)
    '                    ElseIf e.Column Is gv1.Columns(colMorningSNFPerNoDecimal) Then
    '                        gv1.CurrentRow.Cells(colMorningSNFPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colMorningSNFPerNoDecimal).Value)
    '                        If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
    '                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorningSNFPer).Value) > settMaxSNFPerLimit Then
    '                                clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ".", Me.Text)
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("M", gv1.CurrentRow.Index)
    '                    ElseIf e.Column Is gv1.Columns(colEveningQty) OrElse e.Column Is gv1.Columns(colEveningFATPer) OrElse e.Column Is gv1.Columns(colEveningFATKG) OrElse e.Column Is gv1.Columns(colEveningSNFPer) OrElse e.Column Is gv1.Columns(colEveningSNFKG) Then
    '                        If e.Column Is gv1.Columns(colEveningFATPer) Then
    '                            If settMaxFATPerLimit > 0 Then
    '                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colEveningFATPer).Value) > settMaxFATPerLimit Then
    '                                    clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ".", Me.Text)
    '                                End If
    '                            End If
    '                        End If
    '                        If e.Column Is gv1.Columns(colEveningSNFPer) Then
    '                            If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
    '                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colEveningSNFPer).Value) > settMaxSNFPerLimit Then
    '                                    clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ".", Me.Text)
    '                                End If
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("E", gv1.CurrentRow.Index)
    '                    ElseIf e.Column Is gv1.Columns(colMorningQty) OrElse e.Column Is gv1.Columns(colMorningFATPer) OrElse e.Column Is gv1.Columns(colMorningFATKG) OrElse e.Column Is gv1.Columns(colMorningSNFPer) OrElse e.Column Is gv1.Columns(colMorningSNFKG) Then
    '                        If e.Column Is gv1.Columns(colMorningFATPer) Then
    '                            If settMaxFATPerLimit > 0 Then
    '                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorningFATPer).Value) > settMaxFATPerLimit Then
    '                                    clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ".", Me.Text)
    '                                End If
    '                            End If
    '                        End If
    '                        If e.Column Is gv1.Columns(colMorningSNFPer) Then
    '                            If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
    '                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colMorningSNFPer).Value) > settMaxSNFPerLimit Then
    '                                    clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ".", Me.Text)
    '                                End If
    '                            End If
    '                        End If
    '                        UpdateCurrentRow("M", gv1.CurrentRow.Index)
    '                    End If
    '                    isCellValueChangedOpen = False
    '                End If
    '            End If
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
    '            isCellValueChangedOpen = False
    '        End Try
    '    End Sub
    '    Sub OpenVLCFinder(ByVal isButtonClick As Boolean)
    '        If clsCommon.myLen(txtMCC.Tag) <= 0 Then
    '            txtMCC.Focus()
    '            Throw New Exception("Please provide MCC code ")
    '        End If

    '        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] " + Environment.NewLine +
    '        " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
    '        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
    '        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine
    '        Dim whrCls As String = ""
    '        If Not SettShowAllDCS Then
    '            whrCls = "  TSPL_VLC_MASTER_HEAD.MCC  ='" + clsCommon.myCstr(txtMCC.Tag) + "'"
    '        End If

    '        gv1.CurrentRow.Cells(colVLCUploaderCode).Value = clsCommon.ShowSelectForm("SMaRNUdC", qry, "Uploader_Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVLCUploaderCode).Value), "Uploader_Code", isButtonClick)
    '        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,VLC_Name,Apply_Cow_Price,MCC from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVLCUploaderCode).Value) + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            gv1.CurrentRow.Cells(colVLCCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
    '            gv1.CurrentRow.Cells(colVLCName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
    '            If Not objCommonVar.DisplayTypeInMilkReceipt Then
    '                gv1.CurrentRow.Cells(colDocCollectionMilkType).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Apply_Cow_Price")) = 1, "C", "M")
    '            End If
    '            If Not clsCommon.CompairString(clsCommon.myCstr(txtMCC.Tag), clsCommon.myCstr(dt.Rows(0)("MCC"))) = CompairStringResult.Equal Then
    '                clsCommon.MyMessageBoxShow(Me, "DCS does not belong to BMC [" + txtMCC.Value + "]", Me.Text)
    '            End If
    '        End If

    '    End Sub
    '    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '        SaveData()
    '    End Sub
    '    Private Function AllowToSave() As Boolean
    '        'Prevent future date transaction
    '        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
    '            clsCommon.MyMessageBoxShow(Me, "Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
    '            txtDate.Focus()
    '            Return False
    '        End If
    '        Return True
    '    End Function
    '    Public Function SaveData() As Boolean
    '        Try
    '            If (AllowToSave()) Then
    '                Dim obj As New clsMilkCollectionDCSMulipleDays()
    '                obj.Document_No = txtDocNo.Value
    '                obj.Document_Date = txtDate.Value
    '                obj.Description = txtDesc.Text
    '                obj.Route_Code = txtRoute.Value
    '                obj.Tanker_No = txtTankerNo.Value
    '                obj.MCC_Code = txtMCC.Tag
    '                obj.Vehicle_No = txtVehicleNo.Text
    '                obj.Entered_Qty = clsCommon.myCDecimal(txtTotEnteredQty.Text)
    '                obj.Entered_FATKg = clsCommon.myCDecimal(txtTotEnteredFAT.Text)
    '                obj.Entered_SNFKg = clsCommon.myCDecimal(txtTotEnteredSNF.Text)

    '                obj.FAT_SNF_Type = cboFATSNFType.SelectedValue
    '                obj.Arr = GetTRData(False)
    '                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
    '                    Throw New Exception("Please Fill at list one Item")
    '                End If

    '                obj.SaveData(obj, isNewEntry)
    '                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
    '                LoadData(obj.Document_No, NavigatorType.Current)
    '            End If
    '        Catch ex As Exception
    '            'frmSRN.IsPoSavedAuto = False
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '        Return True
    '    End Function
    '    Function GetTRData(ByVal isMissingOnly As Boolean) As List(Of clsMilkCollectionDCSMulipleDaysDetail)
    '        Dim Arr As New List(Of clsMilkCollectionDCSMulipleDaysDetail)
    '        For ii As Integer = 0 To gv1.RowCount - 1
    '            If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
    '                Dim flag As Boolean = True
    '                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningQty).Value) > 0 Then
    '                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEveningPKID).Value) > 0 AndAlso isMissingOnly Then
    '                        flag = False
    '                    End If
    '                    If flag Then
    '                        Dim objTr As New clsMilkCollectionDCSMulipleDaysDetail()
    '                        objTr.SNo = ii + 1
    '                        objTr.VLC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value)
    '                        objTr.Shift = "E"
    '                        objTr.Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)
    '                        If clsCommon.myLen(gv1.Rows(ii).Cells(colCollectionDate).Value) <= 0 Then
    '                            Throw New Exception("Date Can not be left blank at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                        objTr.Collection_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colCollectionDate).Value)
    '                        objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocCollectionMilkType).Value)
    '                        objTr.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningQty).Value)
    '                        objTr.FAT = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningFATPer).Value), 1, MidpointRounding.ToEven)
    '                        objTr.SNF = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningSNFPer).Value), 2, MidpointRounding.ToEven)
    '                        objTr.FATKG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningFATKG).Value)
    '                        objTr.SNFKG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEveningSNFKG).Value)
    '                        Dim intRejectApplicableOn As Integer = clsMilkRejectType.GetApplicableOn(objTr.Milk_Type, Nothing)
    '                        If intRejectApplicableOn <> 1 Then
    '                            If objTr.FAT <= 0 Then
    '                                Throw New Exception("FAT Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                            End If
    '                            If objTr.SNF <= 0 Then
    '                                Throw New Exception("SNF Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                            End If
    '                        End If
    '                        Arr.Add(objTr)
    '                    End If
    '                End If

    '                flag = True
    '                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningQty).Value) > 0 Then
    '                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningPKID).Value) > 0 AndAlso isMissingOnly Then
    '                        flag = False
    '                    End If
    '                    If flag Then
    '                        Dim objTr As New clsMilkCollectionDCSMulipleDaysDetail()
    '                        objTr.SNo = ii + 1
    '                        objTr.VLC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value)
    '                        objTr.Shift = "M"
    '                        objTr.Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)
    '                        If clsCommon.myLen(gv1.Rows(ii).Cells(colCollectionDate).Value) <= 0 Then
    '                            Throw New Exception("Date Can not be left blank at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                        End If
    '                        objTr.Collection_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colCollectionDate).Value)
    '                        objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocCollectionMilkType).Value)
    '                        objTr.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningQty).Value)
    '                        objTr.FAT = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningFATPer).Value), 1, MidpointRounding.ToEven)
    '                        objTr.SNF = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningSNFPer).Value), 2, MidpointRounding.ToEven)
    '                        objTr.FATKG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningFATKG).Value)
    '                        objTr.SNFKG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMorningSNFKG).Value)
    '                        Dim intRejectApplicableOn As Integer = clsMilkRejectType.GetApplicableOn(objTr.Milk_Type, Nothing)
    '                        If intRejectApplicableOn <> 1 Then
    '                            If objTr.FAT <= 0 Then
    '                                Throw New Exception("FAT Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                            End If
    '                            If objTr.SNF <= 0 Then
    '                                Throw New Exception("SNF Can not be Zero at Row No [" + clsCommon.myCstr(ii + 1) + "]")
    '                            End If
    '                        End If
    '                        Arr.Add(objTr)
    '                    End If
    '                End If
    '            End If
    '        Next
    '        Return Arr
    '    End Function
    '    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '        Try
    '            isInsideLoadData = True
    '            LoadBlankGrid()
    '            Dim obj As New clsMilkCollectionDCSMulipleDays()
    '            obj = clsMilkCollectionDCSMulipleDays.GetData(strCode, NavTyep, Nothing)
    '            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
    '                txtDate.Enabled = False
    '                isNewEntry = False
    '                If obj.Status = ERPTransactionStatus.Approved Then
    '                    btnSave.Enabled = False
    '                    btnPost.Enabled = False
    '                    btnDelete.Enabled = False
    '                Else
    '                    btnSave.Enabled = True
    '                    btnPost.Enabled = True
    '                    btnDelete.Enabled = True
    '                End If
    '                UsLock1.Status = obj.Status
    '                txtDocNo.Value = obj.Document_No
    '                txtDate.Value = obj.Document_Date
    '                txtRoute.Value = obj.Route_Code
    '                lblRoute.Text = obj.Route_Name
    '                txtTankerNo.Value = obj.Tanker_No
    '                txtVehicleNo.Text = obj.Vehicle_No
    '                txtMCC.Value = obj.MCC_Uploader_No
    '                txtMCC.Tag = obj.MCC_Code
    '                lblMCC.Text = obj.MCC_Name
    '                txtTotEnteredQty.Value = obj.Entered_Qty
    '                txtTotEnteredFAT.Value = obj.Entered_FATKg
    '                txtTotEnteredSNF.Value = obj.Entered_SNFKg
    '                txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
    '                txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)


    '                If isPickCLRInsteadOfSNF Then
    '                    txtTotEnteredSNFPer.Value = clsEkoPro.getClrOnCalculation(txtTotEnteredFATPer.Value, txtTotEnteredSNFPer.Value, corrFactor)
    '                End If


    '                txtDesc.Text = obj.Description
    '                Dim PreviousSNo As Integer = -1
    '                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '                    For Each objTr As clsMilkCollectionDCSMulipleDaysDetail In obj.Arr
    '                        If objTr.SNo <> PreviousSNo Then
    '                            gv1.Rows.AddNew()
    '                            PreviousSNo = objTr.SNo
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCollectionDate).Value = objTr.Collection_Date
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = objTr.Milk_Type
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDocCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = objTr.VLC_Name
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCUploaderCode).Value = objTr.VLC_Uploader_Code
    '                        End If
    '                        If clsCommon.CompairString(objTr.Shift, "E") = CompairStringResult.Equal Then
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningPKID).Value = objTr.PK_Id
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningQty).Value = objTr.Qty
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningFATPer).Value = objTr.FAT
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningSNFPer).Value = objTr.SNF
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningFATKG).Value = objTr.FATKG
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningSNFKG).Value = objTr.SNFKG

    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningFATPerNoDecimal).Value = clsCommon.myCstr(objTr.FAT).Replace(".", "")
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEveningSNFPerNoDecimal).Value = clsCommon.myCstr(objTr.SNF).Replace(".", "")
    '                        Else
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningPKID).Value = objTr.PK_Id
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningQty).Value = objTr.Qty
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningFATPer).Value = objTr.FAT
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningSNFPer).Value = objTr.SNF
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningFATKG).Value = objTr.FATKG
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningSNFKG).Value = objTr.SNFKG

    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningFATPerNoDecimal).Value = clsCommon.myCstr(objTr.FAT).Replace(".", "")
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMorningSNFPerNoDecimal).Value = clsCommon.myCstr(objTr.SNF).Replace(".", "")
    '                        End If
    '                    Next
    '                End If
    '                setShiftDate()
    '                UpdateAllTotal()
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message)
    '        Finally
    '            isInsideLoadData = False
    '        End Try
    '    End Sub
    '    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '        If e.Alt AndAlso e.KeyCode = Keys.N Then
    '            AddNew()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
    '            SaveData()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
    '            DeleteData()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnClose.Enabled AndAlso btnPost.Enabled Then
    '            PostData()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
    '            CancelPressed()
    '        End If
    '    End Sub
    '    Sub CancelPressed()
    '        Me.Close()
    '    End Sub
    '    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
    '        RefeshSNO()
    '    End Sub
    '    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
    '        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
    '            e.Cancel = True
    '        End If
    '    End Sub
    '    Sub RefeshSNO()
    '        For ii As Integer = 1 To gv1.Rows.Count
    '            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
    '        Next
    '    End Sub
    '    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '        CancelPressed()
    '    End Sub
    '    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '        If gv1.RowCount > 0 Then
    '            Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '            If intCurrRow = gv1.Rows.Count - 1 Then
    '                gv1.Rows.AddNew()
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = "Good"
    '                gv1.CurrentRow = gv1.Rows(intCurrRow)
    '            End If
    '        End If
    '    End Sub
    '    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
    '        Try
    '            Dim qst As String = "select count(*) from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS where Document_No='" + txtDocNo.Value + "'"
    '            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
    '            If count = 0 Then
    '                txtDocNo.MyReadOnly = False
    '            Else
    '                txtDocNo.MyReadOnly = True
    '            End If
    '            LoadData(txtDocNo.Value, NavType)
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
    '        End Try
    '    End Sub
    '    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
    '        Dim qry As String = "select TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No,convert (varchar,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) as Document_Date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Description,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as BMC,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code as BMC_Code,TSPL_MCC_MASTER.MCC_NAME as BMC_Name,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Vehicle_No,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Status=1 then 'Posted' else 'Pending' end as Status 
    'from TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS
    'left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
    'left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code "
    '        LoadData(clsCommon.ShowSelectForm("SMP3FINOC", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
    '    End Sub
    '    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '        DeleteData()
    '    End Sub
    '    Sub DeleteData()
    '        Try
    '            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '                Throw New Exception("Please select document no to delete")
    '            End If
    '            If clsCommon.MyMessageBoxShow(Me, "Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                clsMilkCollectionDCSMulipleDays.DeleteData(txtDocNo.Value)
    '                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
    '                AddNew()
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
    '        PostData()
    '    End Sub
    '    Sub PostData()
    '        Try
    '            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '                Throw New Exception("No document found to post")
    '            End If
    '            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                clsMilkCollectionDCSMulipleDays.PostData(txtDocNo.Value)
    '                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
    '                LoadData(txtDocNo.Value, NavigatorType.Current)
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Sub SetGridFocus()
    '        If gv1.CurrentCell IsNot Nothing Then
    '            If gv1.CurrentCell.ColumnInfo.Name = colCollectionDate Then
    '                gv1.CurrentColumn = gv1.Columns(colMilkType)
    '            ElseIf gv1.CurrentCell.ColumnInfo.Name = colMilkType Then
    '                If objCommonVar.DisplayTypeInMilkReceipt Then
    '                    gv1.CurrentColumn = gv1.Columns(colDocCollectionMilkType)
    '                Else
    '                    gv1.CurrentColumn = gv1.Columns(colVLCUploaderCode)
    '                End If
    '            ElseIf gv1.CurrentCell.ColumnInfo.Name = colDocCollectionMilkType Then
    '                gv1.CurrentColumn = gv1.Columns(colVLCUploaderCode)
    '            ElseIf gv1.CurrentCell.ColumnInfo.Name = colVLCUploaderCode Then
    '                If SettHideShiftCollection <> 1 Then
    '                    gv1.CurrentColumn = gv1.Columns(colEveningQty)
    '                Else
    '                    gv1.CurrentColumn = gv1.Columns(colMorningQty)
    '                End If
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colEveningQty) Then
    '                If SettFATSNFNoDecimalDCS Then
    '                    gv1.CurrentColumn = gv1.Columns(colEveningFATPerNoDecimal)
    '                ElseIf clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 0 Then
    '                    gv1.CurrentColumn = gv1.Columns(colEveningFATPer)
    '                Else
    '                    gv1.CurrentColumn = gv1.Columns(colEveningFATKG)
    '                End If
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colEveningFATPerNoDecimal) Then
    '                gv1.CurrentColumn = gv1.Columns(colEveningSNFPerNoDecimal)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colEveningFATPer) Then
    '                gv1.CurrentColumn = gv1.Columns(colEveningSNFPer)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colEveningFATKG) Then
    '                gv1.CurrentColumn = gv1.Columns(colEveningSNFKG)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colEveningSNFPer OrElse gv1.CurrentCell.ColumnInfo.Name = colEveningSNFKG OrElse gv1.CurrentCell.ColumnInfo.Name = colEveningSNFPerNoDecimal) Then
    '                If SettHideShiftCollection <> 2 Then
    '                    gv1.CurrentColumn = gv1.Columns(colMorningQty)
    '                Else
    '                    Dim strMilkType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colMilkType).Value)
    '                    Dim strDocCollectionMilkType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDocCollectionMilkType).Value)
    '                    If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
    '                        gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
    '                        If clsCommon.myLen(gv1.CurrentRow.Cells(colMilkType).Value) <= 0 Then
    '                            gv1.CurrentRow.Cells(colMilkType).Value = strMilkType
    '                        End If
    '                        If clsCommon.myLen(gv1.CurrentRow.Cells(colDocCollectionMilkType).Value) <= 0 Then
    '                            gv1.CurrentRow.Cells(colDocCollectionMilkType).Value = strDocCollectionMilkType
    '                        End If
    '                    End If
    '                    'If objCommonVar.DisplayTypeInMilkReceipt Then
    '                    '    gv1.CurrentColumn = gv1.Columns(colDocCollectionMilkType)
    '                    'Else
    '                    '    gv1.CurrentColumn = gv1.Columns(colVLCUploaderCode)
    '                    'End If
    '                    gv1.CurrentColumn = gv1.Columns(colCollectionDate)
    '                End If
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMorningQty) Then
    '                If SettFATSNFNoDecimalDCS Then
    '                    gv1.CurrentColumn = gv1.Columns(colMorningFATPerNoDecimal)
    '                ElseIf clsCommon.myCdbl(cboFATSNFType.SelectedValue) = 0 Then
    '                    gv1.CurrentColumn = gv1.Columns(colMorningFATPer)
    '                Else
    '                    gv1.CurrentColumn = gv1.Columns(colMorningFATKG)
    '                End If
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMorningFATPerNoDecimal) Then
    '                gv1.CurrentColumn = gv1.Columns(colMorningSNFPerNoDecimal)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMorningFATPer) Then
    '                gv1.CurrentColumn = gv1.Columns(colMorningSNFPer)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMorningFATKG) Then
    '                gv1.CurrentColumn = gv1.Columns(colMorningSNFKG)
    '            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMorningSNFPer OrElse gv1.CurrentCell.ColumnInfo.Name = colMorningSNFKG OrElse gv1.CurrentCell.ColumnInfo.Name = colMorningSNFPerNoDecimal) Then
    '                Dim strMilkType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colMilkType).Value)
    '                Dim strDocCollectionMilkType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDocCollectionMilkType).Value)
    '                If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
    '                    gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
    '                    If clsCommon.myLen(gv1.CurrentRow.Cells(colMilkType).Value) <= 0 Then
    '                        gv1.CurrentRow.Cells(colMilkType).Value = strMilkType
    '                    End If
    '                    If clsCommon.myLen(gv1.CurrentRow.Cells(colDocCollectionMilkType).Value) <= 0 Then
    '                        gv1.CurrentRow.Cells(colDocCollectionMilkType).Value = strDocCollectionMilkType
    '                    End If
    '                End If
    '                'If objCommonVar.DisplayTypeInMilkReceipt Then
    '                '    gv1.CurrentColumn = gv1.Columns(colDocCollectionMilkType)
    '                'Else
    '                '    gv1.CurrentColumn = gv1.Columns(colVLCUploaderCode)
    '                'End If

    '                gv1.CurrentColumn = gv1.Columns(colCollectionDate)
    '            End If
    '        End If
    '    End Sub

    '    Sub setShiftDate()
    '        'Dim PrevShift As String = clsCommon.GetPrintDate(txtDate.Value.AddDays(-1), "dd/MM/yyyy")
    '        'Dim CurrShift As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")

    '        Dim PrevShift As String = ""
    '        Dim CurrShift As String = ""

    '        gv1.Columns(colEveningQty).HeaderText = "Evening Qty" + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningFATPerNoDecimal).HeaderText = "Evening FAT " + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningSNFPerNoDecimal).HeaderText = "Evening SNF " + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningFATPer).HeaderText = "Evening FAT %" + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningSNFPer).HeaderText = If(isPickCLRInsteadOfSNF, "Evening CLR %", "Evening SNF %") + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningFATKG).HeaderText = "Evening FAT KG" + Environment.NewLine + PrevShift
    '        gv1.Columns(colEveningSNFKG).HeaderText = "Evening SNF KG" + Environment.NewLine + PrevShift ''If(isPickCLRInsteadOfSNF, "Evening CLR KG", "Evening SNF KG") + Environment.NewLine + PrevShift

    '        gv1.Columns(colMorningQty).HeaderText = "Morning Qty" + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningFATPerNoDecimal).HeaderText = "Morning FAT" + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningSNFPerNoDecimal).HeaderText = "Morning SNF" + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningFATPer).HeaderText = "Morning FAT %" + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningSNFPer).HeaderText = If(isPickCLRInsteadOfSNF, "Morning CLR %", "Morning SNF %") + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningFATKG).HeaderText = "Morning FAT KG" + Environment.NewLine + CurrShift
    '        gv1.Columns(colMorningSNFKG).HeaderText = "Morning SNF KG" + Environment.NewLine + CurrShift ''If(isPickCLRInsteadOfSNF, "Morning CLR KG", "Morning SNF KG") + Environment.NewLine + CurrShift
    '    End Sub
    '    Private Sub txtDate_Validated(sender As Object, e As EventArgs) Handles txtDate.Validated
    '        setShiftDate()
    '    End Sub
    '    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
    '        Try
    '            If clsCommon.myLen(txtRoute.Value) <= 0 Then
    '                txtRoute.Focus()
    '                Throw New Exception("Please provide Route code ")
    '            End If
    '            Dim whr As String = "len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 "
    '            If Not SettShowAllMCC Then
    '                whr += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txtRoute.Value + "' "
    '            End If
    '            txtMCC.Value = clsMccMaster.getFinderUploader(whr, clsCommon.myCstr(txtMCC.Value), isButtonClicked)
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(txtMCC.Value) + "'")
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                txtMCC.Tag = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
    '                lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
    '                'If SettApplyGaze Then
    '                '    whr = " select Silo_Area,Gaze_Reading_Code from tspl_Silo_Detail where Prog_Code='MCC-MST' and Trans_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCCode).Value) + "'"
    '                '    dt = clsDBFuncationality.GetDataTable(whr)
    '                '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                '        If dt.Rows.Count = 1 Then
    '                '            gv1.CurrentRow.Cells(colMCCSiloCapacity).Value = clsCommon.myCstr(dt.Rows(0)("Silo_Area"))
    '                '            gv1.CurrentRow.Cells(colGazeReadingCode).Value = clsCommon.myCstr(dt.Rows(0)("Gaze_Reading_Code"))
    '                '        End If
    '                '    End If
    '                'End If
    '            End If
    '            'LoadTransactionData()
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
    '        If e.KeyCode = Keys.Enter Then
    '            gv1.BeginEdit()
    '        End If
    '    End Sub
    '    Private Sub gv1_KeyUp(sender As Object, e As KeyEventArgs) Handles gv1.KeyUp
    '        If e.KeyCode = Keys.Home Then
    '            If gv1.Rows.Count = gv1.CurrentRow.Index + 1 Then
    '                gv1.Rows.AddNew()
    '            End If
    '            gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
    '            gv1.CurrentColumn = gv1.Columns(colVLCUploaderCode)
    '            gv1.CurrentRow.Cells(colMilkType).Value = "Good"
    '        End If
    '    End Sub
    '    Private Sub btnExport_Click(sender As Object, e As EventArgs)
    '        Try
    '            Dim Str As String = "select '01/Jan/2022' as [Date],'' AS [MCCUploader],'' AS [RouteCode],'' AS [DCSUploader], '' as [Shift], '' as MilkType, '' as [Type], 0.00 as Qty, 0.00 as FAT, 0.00 as " + IIf(isPickCLRInsteadOfSNF, "CLR", "SNF") + " "
    '            transportSql.ExporttoExcel(Str, Me)
    '            Str = Nothing
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub btnImport_Click(sender As Object, e As EventArgs)
    '        Dim gv As New RadGridView()
    '        Dim totqty As Double = 0
    '        Me.Controls.Add(gv)
    '        Dim currentdate As Date = Date.Today

    '        dtDefault = New DataTable()
    '        Dim newBlankRow1 As DataRow = dtDefault.NewRow
    '        dtDefault.Rows.Add(newBlankRow1)
    '        Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
    '        If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
    '            If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
    '                For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
    '                    If clsCommon.myLen(objTr.Column_Name) > 0 Then
    '                        arrExistCols.Add(objTr.Column_Name)
    '                        Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
    '                        dtDefault.Columns.Add(newColumn)
    '                        dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
    '                    End If
    '                Next
    '            End If
    '        End If
    '        Dim colSNForCLR As String = "SNF"
    '        Dim isCorrect As Boolean = False
    '        If isPickCLRInsteadOfSNF Then
    '            colSNForCLR = "CLR"
    '        End If
    '        If transportSql.importExcel(gv, "Date", "MCCUploader", "RouteCode", "DCSUploader", "Shift", "MilkType", "Type", "Qty", "FAT", colSNForCLR) Then
    '            '*********Validation Check***********
    '            Dim DocDate As DateTime = Nothing

    '            clsCommon.ProgressBarShow()
    '            Dim sno As Integer = 0
    '            Dim MShiftCount As Integer = 0
    '            Dim EShiftCount As Integer = 0
    '            Dim counter As Integer = 0
    '            Dim qry As String = ""
    '            Dim check As Integer = 0
    '            Dim StrShift As String = ""
    '            Dim MilkType As String = ""
    '            Dim StrType As String = ""
    '            Dim VLCUploader As String = ""
    '            Dim MCCUploader As String = ""
    '            Dim RouteCode As String = ""
    '            Dim MCCCode As String = ""
    '            Dim VLCCode As String = ""
    '            Dim DblQty As Double = 0
    '            Dim DblFAT As Double = 0
    '            Dim DblSNF As Double = 0
    '            Dim MilkCollectionRef As Integer = 0
    '            Try

    '                For Each grow As GridViewRowInfo In gv.Rows

    '                    counter += 1
    '                    DocDate = clsCommon.myCstr(grow.Cells("Date").Value)
    '                    VLCUploader = clsCommon.myCstr(grow.Cells("DCSUploader").Value)
    '                    MCCUploader = clsCommon.myCstr(grow.Cells("MCCUploader").Value)
    '                    RouteCode = clsCommon.myCstr(grow.Cells("RouteCode").Value)
    '                    DblQty = clsCommon.myCdbl(grow.Cells("Qty").Value)
    '                    DblFAT = clsCommon.myCdbl(grow.Cells("FAT").Value)
    '                    DblSNF = clsCommon.myCdbl(grow.Cells(colSNForCLR).Value)
    '                    StrShift = clsCommon.myCstr(grow.Cells("Shift").Value)
    '                    MilkType = clsCommon.myCstr(grow.Cells("MilkType").Value)
    '                    StrType = clsCommon.myCstr(grow.Cells("Type").Value)

    '                    MCCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT MCC_CODE FROM TSPL_MCC_MASTER WHERE Mcc_Code_VLC_Uploader = '" + MCCUploader + "' "))
    '                    If clsCommon.myLen(MCCCode) <= 0 Then
    '                        Throw New Exception("Invalid MCC Uploader code At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If

    '                    qry = " select count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" + VLCUploader + "'  "
    '                    Dim checkValid As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
    '                    If checkValid = False Then
    '                        'Throw New Exception("Invalid VLC Uploader code At Line No " + clsCommon.myCstr(counter) + "")
    '                        If (dtDefault IsNot Nothing AndAlso clsCommon.myLen(dtDefault.Rows.Count) > 0) Then
    '                            clsfrmVLCMaster.CreateNewVSP_VLC(VLCUploader, MCCCode)
    '                        Else
    '                            Throw New Exception("Please set Default Templete")
    '                        End If
    '                    End If
    '                    'MCCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" + VLCUploader + "' "))


    '                    VLCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" + VLCUploader + "' "))

    '                    qry = " select count(*) from TSPL_BULK_ROUTE_MASTER where ROUTE_NO = '" + RouteCode + "'"
    '                    checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
    '                    If checkValid = False Then
    '                        Throw New Exception("Invalid Route code At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If

    '                    If Not SettShowAllDCS Then
    '                        qry = " select count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" + VLCUploader + "' and mcc='" + MCCCode + "' "
    '                        checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
    '                        If checkValid = False Then
    '                            Throw New Exception("Invalid MCC Uploader code/VLC Uploader code At Line No " + clsCommon.myCstr(counter) + "")
    '                        End If
    '                    End If

    '                    qry = "select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id from TSPL_MILK_COLLECTION_MCC_DETAIL
    '                    left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
    '                    where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)=convert(date,'" + DocDate + "',103)
    '                    and MCC_CODE='" & MCCCode & "' and TSPL_MILK_COLLECTION_MCC.Route_Code='" & RouteCode & "' "
    '                    MilkCollectionRef = CInt(clsDBFuncationality.getSingleValue(qry))

    '                    If MilkCollectionRef <= 0 Then
    '                        Throw New Exception("No Milk collection found for data At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If

    '                    If SettHideShiftCollection = 1 Then
    '                        If clsCommon.CompairString(StrShift, "M") = CompairStringResult.Equal Then
    '                        Else
    '                            Throw New Exception("Shift should be (M-Morning)  At Line No " + clsCommon.myCstr(counter) + "")
    '                        End If
    '                    ElseIf SettHideShiftCollection = 2 Then
    '                        If clsCommon.CompairString(StrShift, "E") = CompairStringResult.Equal Then
    '                        Else
    '                            Throw New Exception("Shift should be (E-Evening)  At Line No " + clsCommon.myCstr(counter) + "")
    '                        End If
    '                    Else
    '                        If clsCommon.CompairString(StrShift, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(StrShift, "E") = CompairStringResult.Equal Then
    '                        Else
    '                            Throw New Exception("Shift should be (M-Morning/E-Evening)  At Line No " + clsCommon.myCstr(counter) + "")
    '                        End If
    '                    End If

    '                    If clsCommon.CompairString(MilkType, "Good") = CompairStringResult.Equal OrElse clsCommon.CompairString(MilkType, "Curd") = CompairStringResult.Equal OrElse clsCommon.CompairString(MilkType, "Sour") = CompairStringResult.Equal Then
    '                    Else
    '                        Throw New Exception("Milk Type should be (Good/Curd/Sour)  At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If
    '                    If clsCommon.CompairString(StrType, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(StrType, "C") = CompairStringResult.Equal Then
    '                    Else
    '                        Throw New Exception("Shift should be (M-Mix/C-Cow)  At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If

    '                    If DblFAT <= 0 Then
    '                        Throw New Exception("FAT Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If

    '                    If DblSNF <= 0 Then
    '                        Throw New Exception("SNF Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If
    '                    If DblQty <= 0 Then
    '                        Throw New Exception("SNF Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
    '                    End If
    '                Next
    '            Catch ex As Exception
    '                clsCommon.ProgressBarHide()
    '                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '                Exit Sub
    '            End Try
    '            '********************
    '            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()



    '            Dim dtgv As DataTable = CType(gv.DataSource, DataTable)


    '            Dim dtData As DataTable = dtgv.Copy()
    '            Dim dtResult As DataTable = dtgv.Clone()
    '            Dim view As DataView = New DataView(dtData)
    '            Dim DistinctcolumnName() As String = {"Date", "MCCUploader", "RouteCode"}
    '            Dim dtCust As DataTable = view.ToTable(True, DistinctcolumnName)
    '            Try
    '                For i As Integer = 0 To dtCust.Rows.Count - 1

    '                    Dim dtCustData As DataTable = Nothing
    '                    Dim dr As DataRow() = dtData.Select(" [Date]='" + clsCommon.myCstr(dtCust.Rows(i)("Date")) + "' AND [MCCUploader]='" + clsCommon.myCstr(dtCust.Rows(i)("MCCUploader")) + "' AND [RouteCode]='" + clsCommon.myCstr(dtCust.Rows(i)("RouteCode")) + "'")
    '                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
    '                        dtCustData = dr.CopyToDataTable()
    '                        '======================================================================
    '                        Dim obj As New clsMilkCollectionDCSMulipleDays()
    '                        obj.Document_No = ""
    '                        obj.Document_Date = clsCommon.myCstr(dtCust.Rows(i)("Date"))

    '                        MCCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT MCC_CODE FROM TSPL_MCC_MASTER WHERE Mcc_Code_VLC_Uploader = '" + clsCommon.myCstr(dtCust.Rows(i)("MCCUploader")) + "' ", trans))
    '                        MCCUploader = clsCommon.myCstr(dtCust.Rows(i)("MCCUploader"))
    '                        RouteCode = clsCommon.myCstr(dtCust.Rows(i)("RouteCode"))
    '                        qry = "select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id from TSPL_MILK_COLLECTION_MCC_DETAIL
    '                                left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
    '                                where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)=convert(date,'" + clsCommon.myCstr(dtCust.Rows(i)("Date")) + "',103)
    '                                and MCC_CODE='" & MCCCode & "'  and TSPL_MILK_COLLECTION_MCC.Route_Code='" & RouteCode & "' "
    '                        obj.Description = ""
    '                        obj.Arr = New List(Of clsMilkCollectionDCSMulipleDaysDetail)

    '                        Dim TempdtCustData As DataTable = dtCustData.Copy()
    '                        Dim viewItem As DataView = New DataView(TempdtCustData)
    '                        Dim DistinctcolumnNameWithItem() As String = {"Date", "MCCUploader", "RouteCode", "DCSUploader"}
    '                        Dim dtItemData As DataTable = viewItem.ToTable(True, DistinctcolumnNameWithItem)
    '                        sno = 0
    '                        For k As Integer = 0 To dtItemData.Rows.Count - 1
    '                            Dim dtItem As DataTable = Nothing
    '                            Dim drItem As DataRow() = TempdtCustData.Select("[Date]='" + clsCommon.myCstr(dtItemData.Rows(k)("Date")) + "' AND [MCCUploader]='" + clsCommon.myCstr(dtItemData.Rows(k)("MCCUploader")) + "'  AND [RouteCode]='" + clsCommon.myCstr(dtItemData.Rows(k)("RouteCode")) + "' AND [DCSUploader]='" + clsCommon.myCstr(dtItemData.Rows(k)("DCSUploader")) + "'")
    '                            If drItem IsNot Nothing AndAlso drItem.Length > 0 Then
    '                                dtItem = drItem.CopyToDataTable()
    '                                If SettHideShiftCollection = 0 Then
    '                                    sno = sno + 1
    '                                End If
    '                                MShiftCount = 0
    '                                EShiftCount = 0
    '                                For j As Integer = 0 To dtItem.Rows.Count - 1
    '                                    Dim objTr As New clsMilkCollectionDCSMulipleDaysDetail()

    '                                    If SettHideShiftCollection = 0 Then

    '                                        If clsCommon.CompairString(clsCommon.myCstr(dtItem.Rows(j)("Shift")), "M") = CompairStringResult.Equal Then
    '                                            MShiftCount = MShiftCount + 1
    '                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtItem.Rows(j)("Shift")), "E") = CompairStringResult.Equal Then
    '                                            EShiftCount = EShiftCount + 1
    '                                        End If

    '                                        If clsCommon.CompairString(clsCommon.myCstr(dtItem.Rows(j)("Shift")), "M") = CompairStringResult.Equal AndAlso MShiftCount > 1 Then
    '                                            sno = sno + 1
    '                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtItem.Rows(j)("Shift")), "E") = CompairStringResult.Equal AndAlso EShiftCount > 1 Then
    '                                            sno = sno + 1
    '                                        End If
    '                                    Else
    '                                        sno = sno + 1
    '                                    End If


    '                                    objTr.SNo = sno
    '                                    objTr.Milk_Type = clsCommon.myCstr(dtItem.Rows(j)("MilkType"))

    '                                    objTr.VLC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" + clsCommon.myCstr(dtItem.Rows(j)("DCSUploader")) + "' ", trans))
    '                                    objTr.Qty = clsCommon.myCdbl(dtItem.Rows(j)("Qty"))

    '                                    If SettMilkCollectionFATSNFType = 0 Then
    '                                        If SettFATSNFNoDecimalDCS = True Then
    '                                            objTr.FAT = Xtra.MyNoDecimalToDecimal(dtItem.Rows(j)("FAT"))
    '                                            objTr.SNF = Xtra.MyNoDecimalToDecimal(dtItem.Rows(j)(colSNForCLR))
    '                                            objTr.FATKG = Math.Round(clsCommon.myCDecimal(objTr.Qty) * Xtra.MyNoDecimalToDecimal(dtItem.Rows(j)("FAT")) / 100, 3, MidpointRounding.ToEven)
    '                                            objTr.SNFKG = Math.Round(clsCommon.myCDecimal(objTr.Qty) * Xtra.MyNoDecimalToDecimal(dtItem.Rows(j)(colSNForCLR)) / 100, 3, MidpointRounding.ToEven)
    '                                        Else
    '                                            objTr.FAT = clsCommon.myCDecimal(dtItem.Rows(j)("FAT"))
    '                                            objTr.SNF = clsCommon.myCDecimal(dtItem.Rows(j)(colSNForCLR))
    '                                            objTr.FATKG = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItem.Rows(j)("FAT")) / 100, 3, MidpointRounding.ToEven)
    '                                            objTr.SNFKG = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItem.Rows(j)(colSNForCLR)) / 100, 3, MidpointRounding.ToEven)
    '                                        End If
    '                                    Else
    '                                        objTr.FAT = Math.Round((100 * clsCommon.myCDecimal(dtItem.Rows(j)("FAT"))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
    '                                        objTr.SNF = Math.Round((100 * clsCommon.myCDecimal(dtItem.Rows(j)(colSNForCLR))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
    '                                        objTr.FATKG = clsCommon.myCDecimal(dtItem.Rows(j)("FAT"))
    '                                        objTr.SNFKG = clsCommon.myCDecimal(dtItem.Rows(j)(colSNForCLR))

    '                                    End If
    '                                    obj.Arr.Add(objTr)
    '                                Next
    '                            End If
    '                        Next
    '                        If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
    '                            Throw New Exception("Please Fill at list one Item")
    '                        End If
    '                        If (obj.SaveData(obj, True, trans)) = False Then
    '                            trans.Rollback()
    '                        End If
    '                    End If


    '                Next
    '                trans.Commit()
    '                clsCommon.ProgressBarHide()
    '                common.clsCommon.MyMessageBoxShow(Me, "Successfully Imported.", Me.Text)
    '            Catch ex As Exception
    '                trans.Rollback()
    '                clsCommon.ProgressBarHide()
    '                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '            End Try
    '        End If
    '        Me.Controls.Remove(gv)
    '    End Sub
    '    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
    '        Try
    '            UpdateAllTotal()
    '            If clsCommon.myCDecimal(txtTotPendingQty.Text) > 0 AndAlso clsCommon.myCDecimal(txtTotPendingFAT.Text) > 0 AndAlso clsCommon.myCDecimal(txtTotPendingSNF.Text) > 0 Then
    '                Dim qry As String = "select top 1 VLC_Code,VLC_Name,VLC_Code_VLC_Uploader,Apply_Cow_Price from TSPL_VLC_MASTER_HEAD where IsSuspense=1"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    Throw New Exception("Please Set Suspence DCS")
    '                End If


    '                Dim flag As Boolean = False
    '                Dim ii As Integer = 0
    '                For ii = 0 To gv1.Rows.Count - 1
    '                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value) = 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) <= 0 Then
    '                        flag = True
    '                        Exit For
    '                    End If
    '                Next
    '                If Not flag Then
    '                    gv1.Rows.AddNew()
    '                    ii = (gv1.Rows.Count - 1)
    '                End If

    '                Try
    '                    isCellValueChangedOpen = True
    '                    gv1.Rows(ii).Cells(colMilkType).Value = "Good"
    '                    gv1.CurrentRow.Cells(colDocCollectionMilkType).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Apply_Cow_Price")) = 1, "C", "M")
    '                    gv1.Rows(ii).Cells(colVLCUploaderCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader"))
    '                    gv1.Rows(ii).Cells(colVLCCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
    '                    gv1.Rows(ii).Cells(colVLCName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
    '                    gv1.Rows(ii).Cells(colMorningQty).Value = clsCommon.myCDecimal(txtTotPendingQty.Text)
    '                    gv1.Rows(ii).Cells(colMorningFATKG).Value = clsCommon.myCDecimal(txtTotPendingFAT.Text)
    '                    gv1.Rows(ii).Cells(colMorningSNFKG).Value = clsCommon.myCDecimal(txtTotPendingSNF.Text)
    '                    gv1.Rows(ii).Cells(colMorningFATPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningFATKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value), 1, MidpointRounding.ToEven)
    '                    gv1.Rows(ii).Cells(colMorningSNFPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningSNFKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMorningQty).Value), 1, MidpointRounding.ToEven)
    '                    gv1.Rows(ii).Cells(colMorningFATPerNoDecimal).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colMorningFATPer).Value).Replace(".", "")
    '                    gv1.Rows(ii).Cells(colMorningSNFPerNoDecimal).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colMorningSNFPer).Value).Replace(".", "")
    '                    UpdateAllTotal()
    '                Catch ex As Exception
    '                Finally
    '                    isCellValueChangedOpen = False
    '                End Try

    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
    '        Try
    '            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '                clsCommon.MyMessageBoxShow(Me, "Please select document code first..", Me.Text)
    '            End If

    '            Dim qry = " select  TSPL_COMPANY_MASTER.Comp_Code , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 , TSPL_COMPANY_MASTER.Add2 , TSPL_COMPANY_MASTER.Add3 ,TSPL_COMPANY_MASTER.City_Code, TSPL_COMPANY_MASTER.State ,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.GSTINNo, TSPL_COMPANY_MASTER.CINNo ,TSPL_COMPANY_MASTER.Phone1 , TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No  ,TSPL_COMPANY_MASTER.Email, XXXMain.Comp_Code, XXXMain.Document_No , XXXMain.Document_Date, XXXMain.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME ,XXXMain.Vehicle_No , XXXMain.Tanker_No, XXXMain.MCC_Code, XXXMain.MCC_NAME, XXXMain.Mcc_Code_VLC_Uploader, XXXMain.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name , XXXMain.Milk_Type as Milk_Type ,XXXMain.Dock_Collection_Milk_Type as Dock_Collection_Milk_Type , isnull ( XXXMorning.Qty,0) as Morning_Qty , isnull (XXXMorning.FAT,0) as Morning_FAT, isnull(XXXMorning.SNF,0) as Morning_SNF , isnull(XXXMorning.FATKG,0) as Morning_FATKG, isnull (XXXMorning.SNFKG,0) as Morning_SNFKG  

    '                        ,XXXEvening.Milk_Type as Evening_Milk_Type ,XXXEvening.Dock_Collection_Milk_Type as Evening_Dock_Collection_Milk_Type , isnull (XXXEvening.Qty,0) as Evening_Qty , isnull (XXXEvening.FAT,0) as Evening_FAT, isnull(XXXEvening.SNF,0) as Evening_SNF , isnull(XXXEvening.FATKG,0) as Evening_FATKG, isnull (XXXEvening.SNFKG,0) as Evening_SNFKG                        from (
    '                        select max(Comp_Code) as Comp_Code,  max(Document_No) as Document_No , max(Document_Date) as Document_Date, max(Route_Code) as Route_Code, max(Vehicle_No) as Vehicle_No, max(Tanker_No) as Tanker_No , max(MCC_Code) as MCC_Code , max(MCC_NAME) as MCC_NAME, max(Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader , VLC_Code as VLC_Code, Milk_Type,Dock_Collection_Milk_Type  from (
    '                        select TSPL_USER_MASTER.Comp_Code, TSPL_MILK_COLLECTION_DCS.Document_No , convert (varchar, TSPL_MILK_COLLECTION_DCS.Document_Date,103) as Document_Date , TSPL_MILK_COLLECTION_MCC.Route_Code ,TSPL_MILK_COLLECTION_MCC.Vehicle_No , TSPL_MILK_COLLECTION_MCC.Tanker_No , TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code ,TSPL_MCC_MASTER.MCC_NAME, TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader , TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code ,TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type , TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type

    '                        from TSPL_MILK_COLLECTION_DCS_DETAIL 
    '                        left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
    '                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
    '                        inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id = TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
    '                        left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
    '                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code 
    '                        left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_MILK_COLLECTION_DCS.Created_By
    '                        where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No = '" + txtDocNo.Value + "' 
    '                        ) XXX group by XXX.VLC_Code , XXX.Milk_Type, XXX.Dock_Collection_Milk_Type
    '                        ) XXXMain
    '                        left outer join   (
    '                        select TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code ,TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type , TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG 

    '                        from TSPL_MILK_COLLECTION_DCS_DETAIL  where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No = '" + txtDocNo.Value + "' 

    '                        and TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = 'M'
    '                        ) as  XXXMorning on XXXMorning.VLC_Code = XXXMain.VLC_Code and  XXXMorning.Milk_Type = XXXMain.Milk_Type and  XXXMorning.Dock_Collection_Milk_Type = XXXMain.Dock_Collection_Milk_Type

    '                        left outer join   (
    '                        select TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code ,TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type , TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG 

    '                        from TSPL_MILK_COLLECTION_DCS_DETAIL  where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No = '" + txtDocNo.Value + "' 

    '                        and TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = 'E'
    '                        ) as  XXXEvening on XXXEvening.VLC_Code = XXXMain.VLC_Code and  XXXEvening.Milk_Type = XXXMain.Milk_Type and  XXXEvening.Dock_Collection_Milk_Type = XXXMain.Dock_Collection_Milk_Type
    '                        left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = XXXMain.Route_Code
    '                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = XXXMain.VLC_Code
    '                        left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = XXXMain.Comp_Code
    '                        "
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptDCSTrackSheet", "DCS Truck Sheet", clsCommon.myCDate(txtDate.Value))
    '            frmCRV = Nothing

    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
    '        Try
    '            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '                Throw New Exception("Please Select Document No")
    '            End If
    '            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_MILK_COLLECTION_DCS", "TSPL_MILK_COLLECTION_DCS_DETAIL")
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub txtDate_Leave(sender As Object, e As EventArgs) Handles txtDate.Leave
    '        'LoadTransactionData()
    '    End Sub
    '    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs)
    '        Try
    '            If clsCommon.myLen(txtMCC.Value) <= 0 OrElse clsCommon.myLen(txtMCC.Tag) <= 0 Then
    '                Throw New Exception("Please First select BMC")
    '            End If
    '            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as UploaderNo,TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.VSP_Code as DCSName,TSPL_VLC_MASTER_HEAD.MCC as BMC,TSPL_MCC_MASTER.MCC_NAME as BMCName
    'from TSPL_VLC_MASTER_HEAD
    'left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
    'where TSPL_VLC_MASTER_HEAD.MCC not in ('" + clsCommon.myCstr(txtMCC.Tag) + "')"
    '            Dim arrList As ArrayList = clsCommon.ShowMultipleSelectForm("addvlcDCS", qry, "Code", "", Nothing, Nothing)
    '            If arrList IsNot Nothing AndAlso arrList.Count > 0 Then
    '                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '                Try
    '                    For Each vlcCode As String In arrList
    '                        Dim coll As New Hashtable()
    '                        clsCommon.AddColumnsForChange(coll, "MCC", clsCommon.myCstr(txtMCC.Tag))
    '                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + vlcCode + "'", trans)
    '                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, vlcCode, "TSPL_VLC_MASTER_HEAD", "vlc_code", trans)
    '                    Next
    '                    trans.Commit()
    '                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
    '                Catch ex As Exception
    '                    trans.Rollback()
    '                    Throw New Exception(ex.Message)
    '                End Try
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub txRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
    '        Try
    '            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
    '            Dim whrCls As String = ""
    '            If Not SettShowAllMCC Then
    '                whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
    '            End If
    '            txtRoute.Value = clsCommon.ShowSelectForm("dd33ShUp", qry, "Code", whrCls, txtRoute.Value, "Code", isButtonClicked)
    '            If clsCommon.myLen(txtRoute.Value) > 0 Then
    '                qry = "select  TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Tanker_No,TSPL_TANKER_MASTER.TANKER_NAME from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + txtRoute.Value + "'"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    lblRoute.Text = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME"))
    '                    If settFillRouteTankerNo Then
    '                        txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
    '                        txtVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("TANKER_NAME"))
    '                    End If
    '                End If
    '            End If
    '            'LoadTransactionData()
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
    '        Try
    '            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                If dt.Rows.Count = 1 Then
    '                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
    '                End If
    '            End If
    '            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
    '            txtVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" & txtTankerNo.Value & "'"))
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Private Sub txtTotEnteredQty_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTotEnteredQty.Validating, txtTotEnteredFAT.Validating, txtTotEnteredSNF.Validating, txtTotEnteredFATPer.Validating, txtTotEnteredSNFPer.Validating
    '        UpdateAllTotal()
    '    End Sub



    '    Private Sub txtDesc_Leave(sender As Object, e As EventArgs) Handles txtDesc.Leave
    '        If gv1.Rows.Count > 0 Then
    '            gv1.Focus()
    '            gv1.CurrentColumn = gv1.Columns(colCollectionDate)
    '        End If
    '    End Sub

End Class
