Imports System.ComponentModel
Imports System.Data.SqlClient
Imports common
Imports Telerik
Public Class frmMilkCollectionMCC
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim intFormType As Integer = 1 ''1-MCC Milk Collectin;2-BMC Gate Entry;
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const ColPKID As String = "ColPKID"
    Const ColAgainst_Multiple_Days As String = "ColAgainst_Multiple_Days"
    Const ColREF_PK_ID_BMCDCS_TRIP As String = "ColREF_PK_ID_BMCDCS_TRIP"
    Const ColAgainst_Multiple_Days_Merge_Day_Detail As String = "ColAgainst_Multiple_Days_Merge_Day_Detail"
    Const colMilkType As String = "colMilkType"
    Const colMCCUploaderCode As String = "colMCCUploaderCode"
    Const colMCCCode As String = "colMCCCode"
    Const colMCCName As String = "colMCCName"
    Const colSampleNo As String = "colSampleNo"
    Const colMCCSiloCapacity As String = "colMCCSiloCapacity"
    Const colGazeReadingCode As String = "colGazeReadingCode"
    Const colGazeReading As String = "colGazeReading"
    Const colGazeQty As String = "colGazeQty"
    Const colQty As String = "colQty"
    Const colFATPerNoDecimal As String = "colFATPerNoDecimal"
    Const colSNFPerNoDecimal As String = "colSNFPerNoDecimal"
    Const colFATPer As String = "colFATPer"
    Const colSNFPer As String = "colSNFPer"
    Const colFATKG As String = "colFATKG"
    Const colSNFKG As String = "colSNFKG"
    Const colTemp As String = "colTemp"


    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim SettMilkCollectionFATSNFTypeHeader As Integer
    Dim SettMilkCollectionFATSNFType As Integer
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim settFillRouteTankerNo As Boolean = False
    Dim SettFATSNFNoDecimalMCC As Boolean
    Dim SettShowAllMCC As Boolean
    Dim SettApplyGaze As Boolean
    Dim SettShowSampleNo As Boolean = False
    Dim SettShowTemprature As Boolean = False
    Dim corrFactor As Decimal = 0
    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoCLR As String = "AutoCLR"
    Dim DtError As DataTable
    Dim dr As DataRow
    'Default Mcc create''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim objExportTemplate As clsExportTemplate = Nothing
    Dim arrExistCols As New List(Of String)
    Dim dtDefault As DataTable = Nothing
    '''''''''''''''''''''''''''''''''''''''''''''''''
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnBlankSheetUploder.Visible = MyBase.isExport
        btnBlankSheetImportUploder.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnReverse.Visible = False

        'If btnSave.Visible = True Then
        '    btnBlankSheetImportUploder.Enabled = True
        '    btnBlankSheetUploder.Enabled = True

        'Else
        '    btnBlankSheetImportUploder.Enabled = False
        '    btnBlankSheetUploder.Enabled = False

        'End If
        If MyBase.isExport = True Then
            btnBlankSheetImportUploder.Enabled = True
            btnBlankSheetUploder.Enabled = True

        Else
            btnBlankSheetImportUploder.Enabled = False
            btnBlankSheetUploder.Enabled = False

        End If
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        'If MyBase.isReverse Then
        '    btnreverse.Enabled = True
        'Else
        '    btnreverse.Enabled = False
        'End If
    End Sub
    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
        isPickCLRInsteadOfSNF = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        SettMilkCollectionFATSNFTypeHeader = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFTypeHeader, clsFixedParameterCode.MilkCollectionFATSNFTypeHeader, Nothing))
        SettMilkCollectionFATSNFType = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
        SettFATSNFNoDecimalMCC = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FATSNFNoDecimalMCC, clsFixedParameterCode.FATSNFNoDecimalMCC, Nothing)) = 1)
        SettShowAllMCC = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Nothing)) = 1)
        SettApplyGaze = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyGaze, clsFixedParameterCode.ApplyGaze, Nothing)) = 1)
        settFillRouteTankerNo = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.FillRouteTankerNo, clsFixedParameterCode.FillRouteTankerNo, Nothing)) = 1)
        SettShowSampleNo = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowSampleNoOnBMC, clsFixedParameterCode.ShowSampleNoOnBMC, Nothing)) = 1)
        SettShowTemprature = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowTempratureOnBMC, clsFixedParameterCode.ShowTempratureOnBMC, Nothing)) = 1)
        MyBase.SetUserMgmt(MyBase.Form_ID)
        If clsCommon.CompairString(clsUserMgtCode.MilkCollectionMCCGateEntry, MyBase.Form_ID) = CompairStringResult.Equal Then
            intFormType = 2
        Else
            intFormType = 1
        End If
        'If isPickCLRInsteadOfSNF Then
        '    MyLabel23.Text = "CLR"
        'End If
        LoadLate()
        LoadFATSNFType()
        AddNew()
        SetUserMgmtNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtMDate.Value = clsCommon.GETSERVERDATE()
        If SettMilkCollectionFATSNFTypeHeader = 0 Then
            txtTotEnteredFATPer.Enabled = True
            txtTotEnteredSNFPer.Enabled = True
            txtTotEnteredFAT.Enabled = False
            txtTotEnteredSNF.Enabled = False
        Else
            txtTotEnteredFATPer.Enabled = False
            txtTotEnteredSNFPer.Enabled = False
            txtTotEnteredFAT.Enabled = True
            txtTotEnteredSNF.Enabled = True
        End If
    End Sub
    Public Sub LoadLate()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "No"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)
        cboLate.DataSource = dt
        cboLate.ValueMember = "Code"
        cboLate.DisplayMember = "Name"
    End Sub
    Public Sub LoadFATSNFType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "%"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "KG"
        dt.Rows.Add(dr)
        cboFATSNFType.DataSource = dt
        cboFATSNFType.ValueMember = "Code"
        cboFATSNFType.DisplayMember = "Name"
    End Sub
    Public Function LoadShift() As DataTable
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
        Return dt
    End Function
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
        txtTripNo.Value = 1
    End Sub
    Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        cboLate.SelectedValue = "0"
        If SettMilkCollectionFATSNFType = 0 Then
            cboFATSNFType.SelectedValue = "0"
        Else
            cboFATSNFType.SelectedValue = "1"
        End If
        txtDocNo.Value = ""
        txtTankerNo.Value = ""
        txtDesc.Text = ""
        isNewEntry = True
        txtRoute.Value = ""
        lblRoute.Text = ""
        txtVehicleNo.Text = ""
        txtTotEnteredQty.Text = ""
        txtTotEnteredFAT.Text = ""
        txtTotEnteredSNF.Text = ""
        txtTotPendingFAT.Text = ""
        txtTotPendingQty.Text = ""
        txtTotPendingSNF.Text = ""
        txtTotReceivedFAT.Text = ""
        txtTotReceivedQty.Text = ""
        txtTotReceivedSNF.Text = ""
        txtTotPendingFATPer.Text = ""
        txtTotPendingSNFPer.Text = ""
        txtSlipNo.Text = ""
        txtTotEnteredFATPer.Text = ""
        txtTotEnteredSNFPer.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colMilkType).Value = "Good"
        gvTotal.DataSource = Nothing
        btnAddMissing.Enabled = False
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        isCellValueChangedOpen = False
        txtDate.Enabled = True
        txtRoute.Focus()
    End Sub
    Sub loadBlankParameterGrid()
        Dim whrCls As String = String.Empty
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering,IsShowInMMC from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
        gvParam.Columns.Add("colSLNO", "SL. No.")
        gvParam.Columns("colSLNO").Width = 60
        gvParam.Columns("colSLNO").ReadOnly = True
        gvParam.Columns("colSLNO").Tag = "SLNO"
        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
                    CfColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    FATColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    SNFColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                    CLRColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                    repoDecimalColumn = New GridViewDecimalColumn()
                    repoDecimalColumn.Name = dt.Rows(i)("Code")
                    repoDecimalColumn.Width = 120
                    repoDecimalColumn.FormatString = "{0:n3}"
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    '    If settMODValueForFAT = 0 Then
                    '        repoDecimalColumn.FormatString = "{0:n2}"
                    '    End If
                    'End If
                    repoDecimalColumn.DecimalPlaces = 3
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    repoDecimalColumn.ReadOnly = True
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                    '    repoDecimalColumn.ReadOnly = True
                    'Else
                    '    repoDecimalColumn.ReadOnly = False
                    'End If
                    If clsCommon.CompairString(dt.Rows(i)("IsShowInMMC"), "1") = CompairStringResult.Equal Then
                        repoDecimalColumn.IsVisible = True
                    Else
                        repoDecimalColumn.IsVisible = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                    repoComboColumn.ReadOnly = True
                    'Else
                    '    repoComboColumn.ReadOnly = False
                    'End If
                    If clsCommon.CompairString(dt.Rows(i)("IsShowInMMC"), "1") = CompairStringResult.Equal Then
                        repoComboColumn.IsVisible = True
                    Else
                        repoComboColumn.IsVisible = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                    repoComboColumn.ReadOnly = True
                    'Else
                    '    repoComboColumn.ReadOnly = False
                    'End If
                    If clsCommon.CompairString(dt.Rows(i)("IsShowInMMC"), "1") = CompairStringResult.Equal Then
                        repoComboColumn.IsVisible = True
                    Else
                        repoComboColumn.IsVisible = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    repoTextColumn.ReadOnly = True
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                    '    repoTextColumn.ReadOnly = True
                    'Else
                    '    repoTextColumn.ReadOnly = False
                    'End If
                    If clsCommon.CompairString(dt.Rows(i)("IsShowInMMC"), "1") = CompairStringResult.Equal Then
                        repoTextColumn.IsVisible = True
                    Else
                        repoTextColumn.IsVisible = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoTextColumn)
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoFat, "Auto FAT")
                    gvParam.Columns(colAutoFat).Width = 120
                    gvParam.Columns(colAutoFat).ReadOnly = True
                    gvParam.Columns(colAutoFat).Tag = "AutoFAT"
                    gvParam.Columns(colAutoFat).IsVisible = False
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoSnf, "Auto SNF")
                    gvParam.Columns(colAutoSnf).Width = 120
                    gvParam.Columns(colAutoSnf).ReadOnly = True
                    gvParam.Columns(colAutoSnf).Tag = "AutoSNF"
                    gvParam.Columns(colAutoSnf).IsVisible = False
                End If
                'If Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoCLR, "Auto CLR")
                    gvParam.Columns(colAutoCLR).Width = 120
                    gvParam.Columns(colAutoCLR).ReadOnly = True
                    gvParam.Columns(colAutoCLR).Tag = "AutoCLR"
                    gvParam.Columns(colAutoCLR).IsVisible = False
                End If
                'End If
            Next
            ''richa 22 Sep,2016 BM00000009810
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                gvParam.Columns.Add("ColDifference", "Difference")
                gvParam.Columns("ColDifference").Width = 300
                gvParam.Columns("ColDifference").ReadOnly = True
                gvParam.Columns("ColDifference").Tag = "Difference"
                gvParam.Columns("ColDifference").WrapText = True
            End If
            ''---------------------
            gvParam.Columns.Add("colRemarks", "Remarks")
            gvParam.Columns("colRemarks").Width = 300
            gvParam.Columns("colRemarks").ReadOnly = False
            gvParam.Columns("colRemarks").Tag = "REM"
            gvParam.Columns("colRemarks").WrapText = True
        End If
        gvParam.AllowAddNewRow = False
        gvParam.AllowDeleteRow = False
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
        'gvParam.AutoSizeRows = True
        gvParam.AllowColumnChooser = True
        gvParam.AllowColumnReorder = True
        gvParam.Rows.AddNew()
        'ReStoreGridLayout()
    End Sub
    Private Sub loadDataParameterGrid()
        'Parameter Data
        Dim objQC As clsQualityCheck = Nothing
        Dim strSql As String = "Select TSPL_QUALITY_CHECK.QC_NO from TSPL_QUALITY_CHECK
                        Left Join tspl_gate_entry_details ON tspl_gate_entry_details.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No
                        WHERE Convert(Date, tspl_gate_entry_details.Date_And_Time,103) = Convert(Date, '" + txtDate.Value + "',103)
                        And tspl_gate_entry_details.ROUTE_NO='" + txtRoute.Value + "'"
        Dim StrQcNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strSql))
        If clsCommon.myLen(StrQcNo) > 0 Then
            objQC = clsQualityCheck.getData(StrQcNo, NavigatorType.Current)
            If objQC.arrQcParam IsNot Nothing Then
                loadBlankParameterGrid()
                Dim intCount As Integer = 0
                gvParam.Rows(0).Cells("colSLNO").Value = 1
                For i As Integer = 0 To objQC.arrQcParam.Count - 1
                    Try
                        If objQC.isPosted = 0 AndAlso (clsCommon.CompairString(objQC.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objQC.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal) Then
                            If TypeOf (gvParam.Columns(objQC.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(objQC.arrQcParam(i).LINE_NO - 1).Cells(objQC.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(clsCommon.myCDecimal(objQC.arrQcParam(i).Param_Field_Value), 2)
                            Else
                                gvParam.Rows(objQC.arrQcParam(i).LINE_NO - 1).Cells(objQC.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(objQC.arrQcParam(i).Param_Field_Value, 2)
                            End If
                        Else
                            If TypeOf (gvParam.Columns(objQC.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(objQC.arrQcParam(i).LINE_NO - 1).Cells(objQC.arrQcParam(i).Param_Field_Code).Value = clsCommon.myCDecimal(objQC.arrQcParam(i).Param_Field_Value)
                            Else
                                gvParam.Rows(objQC.arrQcParam(i).LINE_NO - 1).Cells(objQC.arrQcParam(i).Param_Field_Code).Value = objQC.arrQcParam(i).Param_Field_Value
                            End If
                        End If
                        gvParam.Rows(objQC.arrQcParam(i).LINE_NO - 1).Cells("colRemarks").Value = objQC.arrQcParam(i).Remarks
                    Catch exx As Exception
                        common.clsCommon.MyMessageBoxShow(Me, exx.Message, Me.Text)
                    End Try
                Next
            End If
        End If
    End Sub
    Function FillYesNoValue() As DataTable
        Dim dt As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "PKID"
        repoNumBox.Name = ColPKID
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Against_Multiple_Days"
        repoNumBox.Name = ColAgainst_Multiple_Days
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "REF_PK_ID_BMCDCS_TRIP"
        repoNumBox.Name = ColREF_PK_ID_BMCDCS_TRIP
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Against_Multiple_Days_Merge_Day_Detail"
        repoNumBox.Name = ColAgainst_Multiple_Days_Merge_Day_Detail
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)



        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoComboBox = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Milk Type"
        repoComboBox.Name = colMilkType
        repoComboBox.Width = 100
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoComboBox.DataSource = clsMilkReceiptMCC.GetReject(True)
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoComboBox)
        Dim repoTextBox2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox2.FormatString = ""
        repoTextBox2.HeaderText = "BMC/MCC Code"
        repoTextBox2.Name = colMCCUploaderCode
        repoTextBox2.HeaderImage = Global.XpertERPBulkProcurement.My.Resources.Resources.search4
        repoTextBox2.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox2.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox2)
        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "BMC/MCC"
        repoTextBox.Name = colMCCCode
        repoTextBox.IsVisible = False
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)
        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "BMC/MCC Name"
        repoTextBox.Name = colMCCName
        repoTextBox.Width = 200
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n0}"
        repoNumBox.HeaderText = "Sample No"
        repoNumBox.Step = 0
        repoNumBox.Name = colSampleNo
        repoNumBox.Width = 100
        repoNumBox.DecimalPlaces = 1
        repoNumBox.IsVisible = SettShowSampleNo
        repoNumBox.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n0}"
        repoNumBox.HeaderText = "Silo Capacity"
        repoNumBox.Step = 0
        repoNumBox.Name = colMCCSiloCapacity
        repoNumBox.Width = 100
        repoNumBox.IsVisible = SettApplyGaze
        repoNumBox.ReadOnly = Not SettApplyGaze
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Gaze Reading Code"
        repoTextBox.Name = colGazeReadingCode
        repoTextBox.IsVisible = False
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n1}"
        repoNumBox.HeaderText = "Gaze Reading"
        repoNumBox.Step = 0
        repoNumBox.Name = colGazeReading
        repoNumBox.Width = 100
        repoNumBox.DecimalPlaces = 1
        repoNumBox.IsVisible = SettApplyGaze
        repoNumBox.ReadOnly = Not SettApplyGaze
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "Gaze Qty"
        repoNumBox.Name = colGazeQty
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = SettApplyGaze
        repoNumBox.ReadOnly = SettApplyGaze
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = colQty
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.ReadOnly = SettApplyGaze
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n0}"
        repoNumBox.HeaderText = "FAT"
        repoNumBox.Name = colFATPerNoDecimal
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = SettFATSNFNoDecimalMCC
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n0}"
        repoNumBox.HeaderText = "SNF"
        repoNumBox.Name = colSNFPerNoDecimal
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = SettFATSNFNoDecimalMCC
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n2}"
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = colFATPer
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 15
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalMCC)
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n2}"
        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "CLR %", "SNF %")
        repoNumBox.Name = colSNFPer
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = IIf(isPickCLRInsteadOfSNF, 50, 15)
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0) AndAlso Not SettFATSNFNoDecimalMCC)
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = colFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 9999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalMCC)
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "SNF KG" ''If(isPickCLRInsteadOfSNF, "CLR KG", "SNF KG")
        repoNumBox.Name = colSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 9999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = ((clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1) AndAlso Not SettFATSNFNoDecimalMCC)
        repoNumBox.ReadOnly = Not repoNumBox.IsVisible
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n2}"
        repoNumBox.HeaderText = "Temperature"
        repoNumBox.Step = 0
        repoNumBox.Name = colTemp
        repoNumBox.DecimalPlaces = 2
        repoNumBox.Width = 100
        repoNumBox.IsVisible = SettShowTemprature
        repoNumBox.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
        'ReStoreGridLayout()
        If intFormType = 2 Then
            gv1.MasterTemplate.Columns(colFATPerNoDecimal).ReadOnly = True
            gv1.MasterTemplate.Columns(colFATPerNoDecimal).IsVisible = False
            gv1.MasterTemplate.Columns(colSNFPerNoDecimal).ReadOnly = True
            gv1.MasterTemplate.Columns(colSNFPerNoDecimal).IsVisible = False
            gv1.MasterTemplate.Columns(colFATPer).ReadOnly = True
            gv1.MasterTemplate.Columns(colFATPer).IsVisible = False
            gv1.MasterTemplate.Columns(colSNFPer).ReadOnly = True
            gv1.MasterTemplate.Columns(colSNFPer).IsVisible = False
            gv1.MasterTemplate.Columns(colFATKG).ReadOnly = True
            gv1.MasterTemplate.Columns(colFATKG).IsVisible = False
            gv1.MasterTemplate.Columns(colSNFKG).ReadOnly = True
            gv1.MasterTemplate.Columns(colSNFKG).IsVisible = False
        End If
    End Sub
    Sub UpdateCurrentRow(ByVal ii As Integer)
        If clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0 Then
            gv1.Rows(ii).Cells(colFATKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) / 100, 3, MidpointRounding.ToEven)
            gv1.Rows(ii).Cells(colSNFKG).Value = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) / 100, 3, MidpointRounding.ToEven)
        ElseIf clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 1 Then
            gv1.Rows(ii).Cells(colFATPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value), 1, MidpointRounding.ToEven)
            gv1.Rows(ii).Cells(colSNFPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value), 1, MidpointRounding.ToEven)
        End If
        UpdateAllTotal()
    End Sub
    Private Sub UpdateAllTotal()
        Dim TotQty As Decimal = 0
        Dim TotFATKG As Decimal = 0
        Dim TotSNFKG As Decimal = 0
        Dim arrType As New List(Of String)
        Dim dt As New DataTable
        dt.Columns.Add("Type", GetType(String))
        dt.Columns.Add("Qty", GetType(Decimal))
        dt.Columns.Add("FAT KG", GetType(Decimal))
        dt.Columns.Add("FAT %", GetType(Decimal))
        dt.Columns.Add("SNF KG", GetType(Decimal))
        dt.Columns.Add("SNF %", GetType(Decimal))
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                TotQty += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value)
                TotFATKG += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)
                Dim dclCurrSNFKG As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFKG).Value)
                If isPickCLRInsteadOfSNF Then
                    Dim snfPer As Decimal = clsEkoPro.getSnfOnCalculation(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value), clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value), corrFactor)
                    dclCurrSNFKG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) * snfPer / 100
                    gv1.Rows(ii).Cells(colSNFKG).Value = dclCurrSNFKG
                End If
                TotSNFKG += dclCurrSNFKG
                If Not arrType.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)) Then
                    Dim dr As DataRow = dt.NewRow
                    dr("Type") = clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)
                    arrType.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value))
                    dt.Rows.Add(dr)
                End If
                Dim indx As Integer = -1
                For jj = 0 To arrType.Count - 1
                    If clsCommon.CompairString(arrType(jj), clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)) = CompairStringResult.Equal Then
                        indx = jj
                        Exit For
                    End If
                Next
                If indx >= 0 Then
                    dt.Rows(indx)("Qty") = clsCommon.myCDecimal(dt.Rows(indx)("Qty")) + clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value)
                    dt.Rows(indx)("FAT KG") = clsCommon.myCDecimal(dt.Rows(indx)("FAT KG")) + clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)
                    dt.Rows(indx)("SNF KG") = clsCommon.myCDecimal(dt.Rows(indx)("SNF KG")) + dclCurrSNFKG
                End If
            End If
        Next
        For Each row As DataRow In dt.Rows
            Dim fatKG As Decimal = clsCommon.myCDecimal(row("FAT KG"))
            Dim qty As Decimal = clsCommon.myCDecimal(row("Qty"))

            If qty > 0 Then
                Dim fatPercentage As Decimal = 100 * fatKG / qty
                Dim formattedFatPercentage As Decimal = Decimal.Round(fatPercentage, 2)
                row("FAT %") = formattedFatPercentage
            Else
                row("FAT %") = 0 ' Handle division by zero scenario
            End If
        Next
        For Each row As DataRow In dt.Rows
            Dim Snfkg As Decimal = clsCommon.myCDecimal(row("SNF KG"))
            Dim qty As Decimal = clsCommon.myCDecimal(row("Qty"))

            If qty > 0 Then
                Dim fatPercentage As Decimal = 100 * Snfkg / qty
                Dim formattedFatPercentage As Decimal = Decimal.Round(fatPercentage, 2)
                row("SNF %") = formattedFatPercentage
            Else
                row("SNF %") = 0 ' Handle division by zero scenario
            End If
        Next



        txtTotReceivedQty.Text = clsCommon.myCstr(Math.Round(TotQty, 3, MidpointRounding.ToEven))
        txtTotReceivedFAT.Text = clsCommon.myCstr(Math.Round(TotFATKG, 3, MidpointRounding.ToEven))
        txtTotReceivedSNF.Text = clsCommon.myCstr(Math.Round(TotSNFKG, 3, MidpointRounding.ToEven))
        If SettMilkCollectionFATSNFTypeHeader = 0 Then
            txtTotEnteredFAT.Value = Math.Round((txtTotEnteredQty.Value * txtTotEnteredFATPer.Value / 100), 3, MidpointRounding.ToEven)
            txtTotEnteredSNF.Value = Math.Round((txtTotEnteredQty.Value * txtTotEnteredSNFPer.Value / 100), 3, MidpointRounding.ToEven)
        Else
            txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
            txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
        End If
        txtTotPendingQty.Text = clsCommon.myCstr(Math.Round((txtTotEnteredQty.Value - (TotQty)), 3, MidpointRounding.ToEven))
        txtTotPendingFAT.Text = clsCommon.myCstr(Math.Round((txtTotEnteredFAT.Value - (TotFATKG)), 3, MidpointRounding.ToEven))
        txtTotPendingSNF.Text = clsCommon.myCstr(Math.Round((txtTotEnteredSNF.Value - (TotSNFKG)), 3, MidpointRounding.ToEven))
        txtTotPendingFATPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingFAT.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 2, MidpointRounding.ToEven)
        txtTotPendingSNFPer.Text = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(txtTotPendingSNF.Text) * 100, clsCommon.myCDecimal(txtTotPendingQty.Text)), 2, MidpointRounding.ToEven)
        gvTotal.DataSource = dt
        For ii As Integer = 0 To gvTotal.Columns.Count - 1
            gvTotal.Columns(ii).ReadOnly = True
            gvTotal.Columns(ii).BestFit()
        Next
        gvTotal.AllowAddNewRow = False
        gvTotal.ShowGroupPanel = False
        gvTotal.AllowColumnReorder = False
        gvTotal.AllowRowReorder = False
        gvTotal.EnableSorting = False
        gvTotal.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTotal.MasterTemplate.ShowRowHeaderColumn = False
        gvTotal.TableElement.TableHeaderHeight = 40
        gvTotal.Columns("Qty").HeaderText = "Qty"
        gvTotal.Columns("FAT KG").HeaderText = "FAT KG"
        gvTotal.Columns("SNF KG").HeaderText = "SNF KG"
        'gvTotal.Columns("FAT PER").HeaderText = "FAT PER"
    End Sub
    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colMCCUploaderCode) Then
                        OpenMCCFinder(False)
                    ElseIf e.Column Is gv1.Columns(colMCCSiloCapacity) Then
                        OpenSiloFinder(False)
                    ElseIf e.Column Is gv1.Columns(colGazeReading) Then
                        Dim dclMM As String = clsCommon.myCDecimal(gv1.CurrentRow.Cells(colGazeReading).Value) * 10
                        Dim qry As String = "select Value from TSPL_GAZE_READING_Detail where MM=" + dclMM + " and Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colGazeReadingCode).Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            gv1.CurrentRow.Cells(colQty).Value = 0
                            Throw New Exception("No Gaze Reading found for Silo Capacity [" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCSiloCapacity).Value) + "] and Gaze Reading Code [" + clsCommon.myCstr(gv1.CurrentRow.Cells(colGazeReadingCode).Value) + "] Reading [" + clsCommon.myCstr(gv1.CurrentRow.Cells(colGazeReading).Value) + "]")
                        Else
                            gv1.CurrentRow.Cells(colGazeQty).Value = clsCommon.myCDecimal(dt.Rows(0)("Value"))
                            gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCDecimal(dt.Rows(0)("Value"))
                        End If
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colFATPer) OrElse e.Column Is gv1.Columns(colFATKG) OrElse e.Column Is gv1.Columns(colSNFPer) OrElse e.Column Is gv1.Columns(colSNFKG) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    ElseIf e.Column Is gv1.Columns(colFATPerNoDecimal) Then
                        gv1.CurrentRow.Cells(colFATPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colFATPerNoDecimal).Value)
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    ElseIf e.Column Is gv1.Columns(colSNFPerNoDecimal) Then
                        gv1.CurrentRow.Cells(colSNFPer).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colSNFPerNoDecimal).Value)
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenSiloFinder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colMCCCode).Value) <= 0 Then
            Throw New Exception("Please select BMC/MCC")
        End If
        Dim qry As String = " select tspl_Silo_Detail.Silo_Area as Capacity,tspl_Silo_Detail.Gaze_Reading_Code as [Gaze Reading Code],TSPL_GAZE_READING.Description as [Description] from tspl_Silo_Detail 
Left outer join TSPL_GAZE_READING on TSPL_GAZE_READING.Code=tspl_Silo_Detail.Gaze_Reading_Code "
        Dim whr As String = "tspl_Silo_Detail.Prog_Code='MCC-MST' and tspl_Silo_Detail.Trans_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCCode).Value) + "'"
        gv1.CurrentRow.Cells(colMCCSiloCapacity).Value = clsCommon.ShowSelectForm("MCCSiloF", qry, "Capacity", whr, clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCSiloCapacity).Value), "Capacity", isButtonClick)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr + " and tspl_Silo_Detail.Silo_Area='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCSiloCapacity).Value) + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colGazeReadingCode).Value = clsCommon.myCstr(dt.Rows(0)("Gaze Reading Code"))
        End If
    End Sub
    Sub OpenMCCFinder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtRoute.Value) <= 0 Then
            txtRoute.Focus()
            Throw New Exception("Please provide Route code ")
        End If
        Dim whr As String = "len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 "
        If Not SettShowAllMCC Then
            whr += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txtRoute.Value + "' "
        End If
        gv1.CurrentRow.Cells(colMCCUploaderCode).Value = clsMccMaster.getFinderUploader(whr, clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCUploaderCode).Value), isButtonClick)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCUploaderCode).Value) + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colMCCCode).Value = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            gv1.CurrentRow.Cells(colMCCName).Value = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            If SettApplyGaze Then
                whr = " select Silo_Area,Gaze_Reading_Code from tspl_Silo_Detail where Prog_Code='MCC-MST' and Trans_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMCCCode).Value) + "'"
                dt = clsDBFuncationality.GetDataTable(whr)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows.Count = 1 Then
                        gv1.CurrentRow.Cells(colMCCSiloCapacity).Value = clsCommon.myCstr(dt.Rows(0)("Silo_Area"))
                        gv1.CurrentRow.Cells(colGazeReadingCode).Value = clsCommon.myCstr(dt.Rows(0)("Gaze_Reading_Code"))
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        'Prevent future date transaction
        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            txtDate.Focus()
            Throw New Exception("Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
        End If
        If clsCommon.myLen(txtTankerNo.Value) <= 0 Then
            txtTankerNo.Focus()
            Throw New Exception("Please select Tanker No")
        End If
        If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
            txtVehicleNo.Focus()
            Throw New Exception("Please select Vehicle No")
        End If
        Dim qry As String = "select Count(Trip_No) as TripNo from TSPL_MILK_COLLECTION_MCC where Trip_No='" + clsCommon.myCstr(txtTripNo.Value) + "' And Tanker_No='" + clsCommon.myCstr(txtTankerNo.Value) + "' And convert (date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) = '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'and Document_No='" + txtDocNo.Value + "'"
        Dim _count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
        If (_count > 0) Then
            txtTripNo.Focus()
            'Throw New Exception("Data already exist")
            'clsCommon.MyMessageBoxShow("Data already exist")
        End If
        UpdateAllTotal()
        Return True
    End Function
    Public Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkCollectionMCC()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Late = clsCommon.myCDecimal(cboLate.SelectedValue)
                obj.FAT_SNF_Type = clsCommon.myCDecimal(cboFATSNFType.SelectedValue)
                obj.Route_Code = txtRoute.Value
                obj.Tanker_No = txtTankerNo.Value
                obj.Vehicle_No = txtVehicleNo.Text
                obj.Trip_No = txtTripNo.Value
                obj.Entered_Qty = txtTotEnteredQty.Value
                obj.Entered_FATKg = txtTotEnteredFAT.Value
                obj.Entered_SNFKg = txtTotEnteredSNF.Value
                obj.Original_Qty = txtTotEnteredQty.Value
                obj.Original_FATKg = txtTotEnteredFAT.Value
                obj.Original_SNFKg = txtTotEnteredSNF.Value
                obj.Slip_No = txtSlipNo.Text
                obj.Description = txtDesc.Text
                obj.Arr = GetTRData(False)
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Function GetTRData(ByVal isMissingOnly As Boolean) As List(Of clsMilkCollectionMCCDetail)
        Dim Arr As New List(Of clsMilkCollectionMCCDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colMCCCode).Value) > 0 Then
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                    Dim flag As Boolean = True
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColPKID).Value) > 0 AndAlso isMissingOnly Then
                        flag = False
                    End If
                    If flag Then
                        Dim objTr As New clsMilkCollectionMCCDetail()
                        objTr.SNo = ii + 1

                        objTr.Against_Multiple_Days = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColAgainst_Multiple_Days).Value)
                        objTr.REF_PK_ID_BMCDCS_TRIP = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColREF_PK_ID_BMCDCS_TRIP).Value)
                        objTr.Against_Multiple_Days_Merge_Day_Detail = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColAgainst_Multiple_Days_Merge_Day_Detail).Value)

                        objTr.Sample_No = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSampleNo).Value)
                        objTr.MCC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCCode).Value)
                        objTr.Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colMilkType).Value)
                        objTr.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value)
                        objTr.FAT = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value), 2, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value), 2, MidpointRounding.ToEven)
                        objTr.FATKG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)
                        objTr.SNFKG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFKG).Value)
                        objTr.Original_Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value)
                        objTr.Original_FATKg = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)
                        objTr.Original_SNFKg = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFKG).Value)
                        objTr.Temp = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colTemp).Value)
                        objTr.Gaze_Reading_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colGazeReadingCode).Value)
                        objTr.Gaze_Reading = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colGazeReading).Value)
                        objTr.Silo_Capacity = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMCCSiloCapacity).Value)
                        objTr.Gaze_Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colGazeQty).Value)
                        Arr.Add(objTr)
                    End If
                End If
            End If
        Next
        Return Arr
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            Dim obj As New clsMilkCollectionMCC()
            obj = clsMilkCollectionMCC.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                txtDate.Enabled = False
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnAddMissing.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnAddMissing.Enabled = False
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                cboLate.SelectedValue = clsCommon.myCstr(obj.Late)
                cboFATSNFType.SelectedValue = clsCommon.myCstr(obj.FAT_SNF_Type)
                txtRoute.Value = obj.Route_Code
                lblRoute.Text = obj.Route_Name
                txtTripNo.Text = obj.Trip_No
                txtTankerNo.Value = obj.Tanker_No
                txtVehicleNo.Text = obj.Vehicle_No
                txtTotEnteredQty.Value = obj.Entered_Qty
                txtTotEnteredFAT.Value = obj.Entered_FATKg
                txtTotEnteredSNF.Value = obj.Entered_SNFKg
                txtTotEnteredFATPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredFAT.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
                txtTotEnteredSNFPer.Value = Math.Round((clsCommon.myCDivide((txtTotEnteredSNF.Value * 100), txtTotEnteredQty.Value)), 2, MidpointRounding.ToEven)
                txtDesc.Text = obj.Description
                txtSlipNo.Text = obj.Slip_No
                Dim PreviousSNo As Integer = -1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkCollectionMCCDetail In obj.Arr
                        If objTr.SNo <> PreviousSNo Then
                            gv1.Rows.AddNew()
                            PreviousSNo = objTr.SNo
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSampleNo).Value = objTr.Sample_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = objTr.Milk_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCCCode).Value = objTr.MCC_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCCName).Value = objTr.MCC_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCCUploaderCode).Value = objTr.MCC_Uploader_Code
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPKID).Value = objTr.PK_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAgainst_Multiple_Days).Value = objTr.Against_Multiple_Days
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColREF_PK_ID_BMCDCS_TRIP).Value = objTr.REF_PK_ID_BMCDCS_TRIP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAgainst_Multiple_Days_Merge_Day_Detail).Value = objTr.Against_Multiple_Days_Merge_Day_Detail
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).Value = objTr.FATKG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNFKG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPerNoDecimal).Value = clsCommon.myCstr(objTr.FAT).Replace(".", "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPerNoDecimal).Value = clsCommon.myCstr(objTr.SNF).Replace(".", "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGazeReadingCode).Value = objTr.Gaze_Reading_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGazeReading).Value = objTr.Gaze_Reading
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGazeQty).Value = objTr.Gaze_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMCCSiloCapacity).Value = objTr.Silo_Capacity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTemp).Value = objTr.Temp
                    Next
                End If
                UpdateAllTotal()
                loadDataParameterGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnClose.Enabled AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            CancelPressed()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F11 Then
            If btnAddMissing.Visible Then
                btnAddMissing.Visible = Not btnAddMissing.Visible
            Else
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.MilkSetting
                pwd.strType = clsFixedParameterType.MCCMilkSRNRepost
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    btnAddMissing.Visible = Not btnAddMissing.Visible
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "MulProcDedReversAndCreate"
                frm.strCode = "MulProcDedReversAndCreate"
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
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If UsLock1.Status = ERPTransactionStatus.Approved Then
                    clsMilkCollectionMCCDetail.DeleteData(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColPKID).Value))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = "Good"
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qst))
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
        Dim qry As String = "select  TSPL_MILK_COLLECTION_MCC.Document_No,convert (varchar,TSPL_MILK_COLLECTION_MCC.Document_Date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC.Description,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,
TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,
case when TSPL_MILK_COLLECTION_MCC.Status=1 then 'Posted' else 'Pending' end as Status ,TSPL_MILK_COLLECTION_MCC.Trip_No from TSPL_MILK_COLLECTION_MCC left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO= TSPL_MILK_COLLECTION_MCC.Route_Code"
        LoadData(clsCommon.ShowSelectForm("SMP2FINOC", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkCollectionMCC.DeleteData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            'If clsCommon.myCDecimal(txtTotPendingQty.Text) > 0 Then
            '    Throw New Exception("Plese fill all MCC Details Qty is Remaining")
            'End If
            'If clsCommon.myCDecimal(txtTotPendingFAT.Text) > 0 Then
            '    Throw New Exception("Plese fill all MCC Details FAT is Remaining")
            'End If
            'If clsCommon.myCDecimal(txtTotPendingSNF.Text) > 0 Then
            '    Throw New Exception("Plese fill all MCC Details SNF is Remaining")
            'End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkCollectionMCC.PostData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = colMilkType Then
                gv1.CurrentColumn = gv1.Columns(colMCCUploaderCode)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colMCCUploaderCode Then
                If SettShowSampleNo Then
                    gv1.CurrentColumn = gv1.Columns(colSampleNo)
                Else
                    If SettApplyGaze Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colMCCSiloCapacity).Value) > 0 Then
                            gv1.CurrentColumn = gv1.Columns(colGazeReading)
                        Else
                            gv1.CurrentColumn = gv1.Columns(colMCCSiloCapacity)
                        End If
                    Else
                        gv1.CurrentColumn = gv1.Columns(colQty)
                    End If
                End If
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colSampleNo Then
                If SettApplyGaze Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colMCCSiloCapacity).Value) > 0 Then
                        gv1.CurrentColumn = gv1.Columns(colGazeReading)
                    Else
                        gv1.CurrentColumn = gv1.Columns(colMCCSiloCapacity)
                    End If
                Else
                    gv1.CurrentColumn = gv1.Columns(colQty)
                End If
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colMCCSiloCapacity Then
                gv1.CurrentColumn = gv1.Columns(colGazeReading)
            ElseIf ((gv1.CurrentCell.ColumnInfo.Name = colGazeReading) OrElse (gv1.CurrentCell.ColumnInfo.Name = colQty)) Then
                If intFormType = 2 Then
                    If SettShowTemprature Then
                        gv1.CurrentColumn = gv1.Columns(colTemp)
                    Else
                        setNxtRow = True
                    End If
                Else
                    If SettFATSNFNoDecimalMCC Then
                        gv1.CurrentColumn = gv1.Columns(colFATPerNoDecimal)
                    ElseIf clsCommon.myCDecimal(cboFATSNFType.SelectedValue) = 0 Then
                        gv1.CurrentColumn = gv1.Columns(colFATPer)
                    Else
                        gv1.CurrentColumn = gv1.Columns(colFATKG)
                    End If
                End If
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colFATPerNoDecimal) Then
                gv1.CurrentColumn = gv1.Columns(colSNFPerNoDecimal)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colFATPer) Then
                gv1.CurrentColumn = gv1.Columns(colSNFPer)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colFATKG) Then
                gv1.CurrentColumn = gv1.Columns(colSNFKG)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colSNFPerNoDecimal) Then
                If SettShowTemprature Then
                    gv1.CurrentColumn = gv1.Columns(colTemp)
                Else
                    setNxtRow = True
                End If
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colSNFPer) Then
                If SettShowTemprature Then
                    gv1.CurrentColumn = gv1.Columns(colTemp)
                Else
                    setNxtRow = True
                End If
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colSNFKG) Then
                If SettShowTemprature Then
                    gv1.CurrentColumn = gv1.Columns(colTemp)
                Else
                    setNxtRow = True
                End If
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colTemp Then
                setNxtRow = True
            End If
            If setNxtRow Then
                Dim str As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colMilkType).Value)
                If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
                    gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colMilkType).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colMilkType).Value = str
                    End If
                End If
                gv1.CurrentColumn = gv1.Columns(colMCCUploaderCode)
            End If
        End If
    End Sub
    Private Sub txtDesc_Leave(sender As Object, e As EventArgs) Handles txtDesc.Leave
        If gv1.Rows.Count > 0 Then
            gv1.Focus()
            gv1.CurrentColumn = gv1.Columns(colMCCUploaderCode)
        End If
    End Sub
    Private Sub txRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            If Not SettShowAllMCC Then
                whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            End If
            txtRoute.Value = clsCommon.ShowSelectForm("dd22ShUp", qry, "Code", whrCls, txtRoute.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtRoute.Value) > 0 Then
                qry = "select  TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Tanker_No,TSPL_TANKER_MASTER.TANKER_NAME from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + txtRoute.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblRoute.Text = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME"))
                    If settFillRouteTankerNo Then
                        txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                        txtVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("TANKER_NAME"))
                    End If
                End If
                loadDataParameterGrid()
            Else
                loadBlankParameterGrid()
            End If
            LoadTransactionData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTotEnteredQty_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTotEnteredQty.Validating, txtTotEnteredFAT.Validating, txtTotEnteredSNF.Validating, txtTotEnteredFATPer.Validating, txtTotEnteredSNFPer.Validating
        UpdateAllTotal()
    End Sub
    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.Enter Then
            gv1.BeginEdit()
        End If
    End Sub
    Private Sub gv1_KeyUp(sender As Object, e As KeyEventArgs) Handles gv1.KeyUp
        If e.KeyCode = Keys.Home Then
            If gv1.Rows.Count = gv1.CurrentRow.Index + 1 Then
                gv1.Rows.AddNew()
            End If
            gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            gv1.CurrentColumn = gv1.Columns(colMCCUploaderCode)
            gv1.CurrentRow.Cells(colMilkType).Value = "Good"
        End If
    End Sub
    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
            txtVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" & txtTankerNo.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnBlankSheetUploder_Click(sender As Object, e As EventArgs) Handles btnBlankSheetUploder.Click
        Try
            Dim Str As String = ""
            If SettApplyGaze = True Then
                Str = " select '' as Date, '' as Route, 0 as Late ,'' as MilkType,'' as TankerNo,0.00 as TankerQty,0.00 as TankerFatKg,0.00 as TankerSnfKg ,'' as BMCMCCCode,0 as Capacity,0 as SampleNo,'' as GazeReading,0.00 as Temp,0.00 as Age,'' as ALCOB,0.00 as Acidity "
            Else
                Str = " select '' as Date, '' as Route, 0 as Late ,'' as MilkType,'' as TankerNo,0.00 as TankerQty,0.00 as TankerFatKg,0.00 as TankerSnfKg , '' as BMCMCCCode, 0.00 as Qty, 0.00 as FAT, 0.00 as " + IIf(isPickCLRInsteadOfSNF, "CLR", "SNF") + ",0.00 as Temp,0.00 as Age,'' as ALCOB,0.00 as Acidity "
            End If
            transportSql.ExporttoExcel(Str, Me)
            Str = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnBlankSheetImportUploder_Click(sender As Object, e As EventArgs) Handles btnBlankSheetImportUploder.Click
        Dim gv As New RadGridView()
        Dim totqty As Double = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today '"VehicleNo",
        dtDefault = New DataTable()
        Dim newBlankRow1 As DataRow = dtDefault.NewRow
        dtDefault.Rows.Add(newBlankRow1)
        Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData("MP-IMP-TMP")
        If (objDefaultTemplate IsNot Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) > 0) Then
            If objDefaultTemplate.Arr IsNot Nothing AndAlso objDefaultTemplate.Arr.Count > 0 Then
                For Each objTr As clsExportTemplateDetail In objDefaultTemplate.Arr
                    If clsCommon.myLen(objTr.Column_Name) > 0 Then
                        arrExistCols.Add(objTr.Column_Name)
                        Dim newColumn As New DataColumn(clsCommon.myCstr(objTr.Column_Name), GetType(System.String))
                        dtDefault.Columns.Add(newColumn)
                        dtDefault.Rows(0).Item(clsCommon.myCstr(objTr.Column_Name)) = clsCommon.myCstr(objTr.Column_Header)
                    End If
                Next
            End If
        End If
        Dim colSNForCLR As String = "SNF"
        Dim isCorrect As Boolean = False
        If isPickCLRInsteadOfSNF Then
            colSNForCLR = "CLR"
        End If
        Dim inputs() As String = {}
        Dim Strs As List(Of String) = New List(Of String)(inputs)
        If SettApplyGaze = True Then
            inputs = {"Date", "Route", "Late", "MilkType", "TankerNo", "TankerQty", "TankerFatKg", "TankerSnfKg", "BMCMCCCode", "Capacity", "SampleNo", "GazeReading", "Temp", "Age", "ALCOB", "Acidity"}
        Else
            inputs = {"Date", "Route", "Late", "MilkType", "TankerNo", "TankerQty", "TankerFatKg", "TankerSnfKg", "BMCMCCCode", "Qty", "FAT", colSNForCLR, "Temp", "Age", "ALCOB", "Acidity"}
        End If
        'If transportSql.importExcel(gv, "Date", "Route", "Late", "TankerNo", "MilkType", "BMCMCCCode", "Qty", "FAT", colSNForCLR, "Temp", "Age", "ALCOB", "Acidity") Then
        If transportSql.importExcel(gv, Strs.ToArray()) Then
            '**************************Validation Check**********************************
            Dim DocDate As DateTime = Nothing
            Dim Route As String = ""
            Dim Late As Integer = 0
            Dim TankerNo As String = ""
            Dim VehicleNo As String = ""
            Dim FATSNFType As Integer = 0
            Dim MilkType As String = ""
            Dim BMCMCCCode As String = ""
            Dim Qty As Double = 0
            Dim FAT As Double = 0
            Dim SNF As String = 0
            Dim dclCapacity As Decimal = 0
            Dim dclGazeReading As Decimal = 0
            Dim dclSampleNo As Decimal = 0
            clsCommon.ProgressBarShow()
            Try
                Dim counter As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    counter += 1
                    DocDate = clsCommon.myCstr(grow.Cells("Date").Value)
                    Route = clsCommon.myCstr(grow.Cells("Route").Value)
                    Late = clsCommon.myCDecimal(grow.Cells("Late").Value)
                    TankerNo = clsCommon.myCstr(grow.Cells("TankerNo").Value)
                    'VehicleNo = clsCommon.myCstr(grow.Cells("VehicleNo").Value)
                    FATSNFType = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
                    'MilkType = clsCommon.myCstr(grow.Cells("MilkType").Value)
                    BMCMCCCode = clsCommon.myCstr(grow.Cells("BMCMCCCode").Value)
                    If SettApplyGaze = False Then
                        Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)
                        FAT = clsCommon.myCDecimal(grow.Cells("FAT").Value)
                        SNF = clsCommon.myCDecimal(grow.Cells(colSNForCLR).Value)
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("ALCOB").Value)) > 0 Then
                        If Not ((clsCommon.CompairString(clsCommon.myCstr(grow.Cells("ALCOB").Value), "+ve") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("ALCOB").Value), "-ve") = CompairStringResult.Equal)) Then
                            Throw New Exception("ALCOB shoule be +ve/-ve ")
                        End If
                    End If
                    qry = " select count(*) from TSPL_BULK_ROUTE_MASTER where ROUTE_NO = '" + Route + "'  "
                    Dim checkValid As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                    If checkValid = False Then
                        Throw New Exception("Invalid Route At Line No " + clsCommon.myCstr(counter) + "")
                    End If
                    qry = " select count (*)  from TSPL_TANKER_MASTER where Tanker_No = '" + TankerNo + "'  "
                    checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                    If checkValid = False Then
                        Throw New Exception("Invalid Tanker No At Line No " + clsCommon.myCstr(counter) + "")
                    End If
                    VehicleNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" + TankerNo + "' "))
                    'If FATSNFType = 0 Then
                    'ElseIf FATSNFType = 1 Then
                    '    Throw New Exception("FATSNFType should be 0 or 1  At Line No " + clsCommon.myCstr(counter) + "")
                    'End If
                    ''qry = " select count(*) From tspl_mcc_master Left outer join TSPL_BULK_ROUTE_MASTER_MCC on TSPL_BULK_ROUTE_MASTER_MCC.MCC_Code=TSPL_MCC_MASTER.MCC_Code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code
                    ''        where len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + Route + "'  and tspl_mcc_master.Mcc_Code_VLC_Uploader = '" + BMCMCCCode + "'  "
                    ''checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                    ''If checkValid = False Then
                    ''    Throw New Exception("Invalid BMCMCCCode At Line No " + clsCommon.myCstr(counter) + "")
                    ''End If
                    'Create new MCC Master
                    qry = " select count (*)  from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + BMCMCCCode + "'  "
                    checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                    If checkValid = False Then
                        If (dtDefault IsNot Nothing AndAlso clsCommon.myLen(dtDefault.Rows.Count) > 0) Then
                            CreateNewMCC(BMCMCCCode)
                        Else
                            Throw New Exception("Please set Default Templete")
                        End If
                    End If
                    'Create new MCC Master
                    If SettApplyGaze = False Then
                        If Qty <= 0 Then
                            Throw New Exception("Qty Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                        If FAT <= 0 Then
                            Throw New Exception("FAT Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                        If SNF <= 0 Then
                            Throw New Exception("SNF Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    qry = " select count (Code)  from ( select 'Good' as Code Union All select Code from TSPL_MILK_REJECT_TYPE where Code='" + MilkType + "'  )ttt  "
                    checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                    If checkValid = False Then
                        Throw New Exception("Invalid MilkType At Line No " + clsCommon.myCstr(counter) + "")
                    End If
                    If Late = 0 Then
                    ElseIf Late = 1 Then
                    Else
                        Throw New Exception("Late value should be 0 or 1 At Line No " + clsCommon.myCstr(counter) + "")
                    End If
                    If SettApplyGaze = True Then
                        dclSampleNo = clsCommon.myCDecimal(grow.Cells("SampleNo").Value)
                        If dclSampleNo <= 0 Then
                            Throw New Exception("Sample No Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                        dclCapacity = clsCommon.myCDecimal(grow.Cells("Capacity").Value)
                        If dclCapacity <= 0 Then
                            Throw New Exception("Capacity Should be greater than 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                        dclGazeReading = clsCommon.myCDecimal(grow.Cells("GazeReading").Value)
                        If dclGazeReading < 0 Then
                            Throw New Exception("Gaze Reading Should be greater than equal to 0 At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                        qry = "select count(*) from tspl_Silo_Detail
                             inner join TSPL_GAZE_READING_DETAIL on tspl_Silo_Detail.Gaze_Reading_Code=TSPL_GAZE_READING_DETAIL.code
                             left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=tspl_Silo_Detail.Trans_Code
                             where tspl_Silo_Detail.Prog_Code='MCC-MST' and TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader='" + BMCMCCCode + "' and tspl_Silo_Detail.Silo_Area='" + clsCommon.myCstr(dclCapacity) + "'
                             and TSPL_GAZE_READING_DETAIL.MM=" + clsCommon.myCstr(dclGazeReading * 10) + ""
                        checkValid = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                        If checkValid = False Then
                            Throw New Exception("Silo Gaze detail not found At Line No " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                Next
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Exit Sub
            End Try
            '************************************************************
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim dtgv As DataTable = CType(gv.DataSource, DataTable)
            '===================================================
            'For Each row As DataRow In dtgv.Rows
            '    If clsCommon.myLen(clsCommon.myCstr(row("Issued To"))) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(row("VLC Uploader Code"))) > 0 Then
            '        Dim strIssueTo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VSP_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + clsCommon.myCstr(row("VLC Uploader Code")) + "' and MCC = '" + clsCommon.myCstr(row("From Location")) + "'", trans))
            '        row.SetField("Issued To", strIssueTo)
            '    End If
            'Next
            '===================================================
            Dim dtData As DataTable = dtgv.Copy()
            Dim dtResult As DataTable = dtgv.Clone()
            Dim view As DataView = New DataView(dtData)
            Dim DistinctcolumnName() As String = {"Date", "Route", "Late", "TankerNo"}
            Dim dtCust As DataTable = view.ToTable(True, DistinctcolumnName)
            Try
                For i As Integer = 0 To dtCust.Rows.Count - 1
                    Dim BeforeTax_Amt As Double = 0
                    Dim Doc_Amt As Double = 0
                    Dim dtCustData As DataTable = Nothing
                    Dim dr As DataRow() = dtData.Select(" [Date]='" + clsCommon.myCstr(dtCust.Rows(i)("Date")) + "' AND [Route]='" + clsCommon.myCstr(dtCust.Rows(i)("Route")) + "'  ")
                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
                        dtCustData = dr.CopyToDataTable()
                        '======================================================================
                        Dim obj As New clsMilkCollectionMCC()
                        obj.Document_No = ""
                        obj.Document_Date = clsCommon.myCstr(dtCustData.Rows(0)("Date"))
                        obj.Late = clsCommon.myCstr(dtCustData.Rows(0)("Late"))
                        obj.FAT_SNF_Type = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, trans))
                        obj.Route_Code = clsCommon.myCstr(dtCustData.Rows(0)("Route"))
                        obj.Tanker_No = clsCommon.myCstr(dtCustData.Rows(0)("TankerNo"))
                        obj.Vehicle_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" + TankerNo + "' ", trans)) 'clsCommon.myCstr(dtCus.Rows(0)("VehicleNo"))
                        obj.Entered_Qty = clsCommon.myCDecimal(dtCustData.Rows(0)("TankerQty"))
                        obj.Entered_FATKg = clsCommon.myCDecimal(dtCustData.Rows(0)("TankerFatKg"))
                        obj.Entered_SNFKg = clsCommon.myCDecimal(dtCustData.Rows(0)("TankerSnfKg"))
                        obj.Description = ""
                        obj.Temp = clsCommon.myCDecimal(dtCustData.Rows(0)("Temp"))
                        obj.Age = clsCommon.myCDecimal(dtCustData.Rows(0)("Age"))
                        obj.ALCOB = clsCommon.myCstr(dtCustData.Rows(0)("ALCOB"))
                        obj.Acidity = clsCommon.myCDecimal(dtCustData.Rows(0)("Acidity"))
                        obj.Arr = New List(Of clsMilkCollectionMCCDetail)
                        Dim TempdtCustData As DataTable = dtCustData.Copy()
                        Dim viewItem As DataView = New DataView(TempdtCustData)
                        If SettApplyGaze = True Then
                            Dim DistinctcolumnNameWithItem() As String = {"Date", "Route", "MilkType", "BMCMCCCode", "Capacity", "SampleNo", "GazeReading"}
                            Dim dtItem As DataTable = viewItem.ToTable(True, DistinctcolumnNameWithItem)
                            For k As Integer = 0 To dtItem.Rows.Count - 1
                                Dim dtItemData As DataTable = Nothing
                                Dim drItem As DataRow() = TempdtCustData.Select(" [Date]='" + clsCommon.myCstr(dtItem.Rows(k)("Date")) + "' AND [Route]='" + clsCommon.myCstr(dtItem.Rows(k)("Route")) + "' ")
                                If drItem IsNot Nothing AndAlso drItem.Length > 0 Then
                                    dtItemData = drItem.CopyToDataTable()
                                    Dim objTr As New clsMilkCollectionMCCDetail()
                                    objTr.SNo = k + 1
                                    objTr.MCC_Code = clsDBFuncationality.getSingleValue(" select MCC_Code from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + clsCommon.myCstr(dtItemData.Rows(k)("BMCMCCCode")) + "'", trans)
                                    objTr.Milk_Type = clsCommon.myCstr(dtItemData.Rows(k)("MilkType"))
                                    objTr.Sample_No = clsCommon.myCstr(dtItemData.Rows(k)("SampleNo"))
                                    objTr.Gaze_Reading = clsCommon.myCDecimal(dtItemData.Rows(k)("GazeReading"))
                                    objTr.Silo_Capacity = clsCommon.myCDecimal(dtItemData.Rows(k)("Capacity"))
                                    Dim qry As String = "select TSPL_GAZE_READING_DETAIL.Code,TSPL_GAZE_READING_DETAIL.Value from tspl_Silo_Detail
                                     inner join TSPL_GAZE_READING_DETAIL on tspl_Silo_Detail.Gaze_Reading_Code=TSPL_GAZE_READING_DETAIL.code
                                     left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=tspl_Silo_Detail.Trans_Code
                                     where tspl_Silo_Detail.Prog_Code='MCC-MST' and TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(dtItemData.Rows(k)("BMCMCCCode")) + "' and tspl_Silo_Detail.Silo_Area='" + clsCommon.myCstr(objTr.Silo_Capacity) + "'
                                     and TSPL_GAZE_READING_DETAIL.MM=" + clsCommon.myCstr(objTr.Gaze_Reading * 10) + ""
                                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        objTr.Qty = clsCommon.myCDecimal(dt.Rows(0)("Value"))
                                        objTr.Gaze_Reading_Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                                    End If
                                    ''objTr.Qty = clsCommon.myCDecimal(dtItemData.Rows(k)("Qty"))
                                    ''Dim dblFatKg As Double = 0
                                    ''Dim dblSnfKg As Double = 0
                                    ''Dim dblFat As Double = 0
                                    ''Dim dblSnf As Double = 0
                                    ''If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 0 Then
                                    ''    dblFatKg = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItemData.Rows(k)("FAT")) / 100, 3, MidpointRounding.ToEven)
                                    ''    dblSnfKg = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR)) / 100, 3, MidpointRounding.ToEven)
                                    ''ElseIf clsCommon.myCDecimal(obj.FAT_SNF_Type) = 1 Then
                                    ''    dblFat = Math.Round((100 * clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
                                    ''    dblSnf = Math.Round((100 * clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
                                    ''End If
                                    ''If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 0 Then
                                    ''    objTr.FAT = clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))
                                    ''    objTr.SNF = clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))
                                    ''    objTr.FATKG = dblFatKg
                                    ''    objTr.SNFKG = dblSnfKg
                                    ''End If
                                    ''If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 1 Then
                                    ''    objTr.FAT = dblFat
                                    ''    objTr.SNF = dblSnf
                                    ''    objTr.FATKG = clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))
                                    ''    objTr.SNFKG = clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))
                                    ''End If
                                    obj.Arr.Add(objTr)
                                End If
                            Next
                        Else
                            Dim DistinctcolumnNameWithItem() As String = {"Date", "Route", "MilkType", "BMCMCCCode", "Qty", "FAT", colSNForCLR}
                            Dim dtItem As DataTable = viewItem.ToTable(True, DistinctcolumnNameWithItem)
                            For k As Integer = 0 To dtItem.Rows.Count - 1
                                Dim dtItemData As DataTable = Nothing
                                Dim drItem As DataRow() = TempdtCustData.Select(" [Date]='" + clsCommon.myCstr(dtItem.Rows(k)("Date")) + "' AND [Route]='" + clsCommon.myCstr(dtItem.Rows(k)("Route")) + "' ")
                                If drItem IsNot Nothing AndAlso drItem.Length > 0 Then
                                    dtItemData = drItem.CopyToDataTable()
                                    Dim objTr As New clsMilkCollectionMCCDetail()
                                    objTr.SNo = k + 1
                                    objTr.MCC_Code = clsDBFuncationality.getSingleValue(" select MCC_Code from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader = '" + clsCommon.myCstr(dtItemData.Rows(k)("BMCMCCCode")) + "'", trans)
                                    objTr.Milk_Type = clsCommon.myCstr(dtItemData.Rows(k)("MilkType"))
                                    objTr.Qty = clsCommon.myCDecimal(dtItemData.Rows(k)("Qty"))
                                    Dim dblFatKg As Double = 0
                                    Dim dblSnfKg As Double = 0
                                    Dim dblFat As Double = 0
                                    Dim dblSnf As Double = 0
                                    If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 0 Then
                                        dblFatKg = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItemData.Rows(k)("FAT")) / 100, 3, MidpointRounding.ToEven)
                                        dblSnfKg = Math.Round(clsCommon.myCDecimal(objTr.Qty) * clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR)) / 100, 3, MidpointRounding.ToEven)
                                    ElseIf clsCommon.myCDecimal(obj.FAT_SNF_Type) = 1 Then
                                        dblFat = Math.Round((100 * clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
                                        dblSnf = Math.Round((100 * clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))) / clsCommon.myCDecimal(objTr.Qty), 1, MidpointRounding.ToEven)
                                    End If
                                    If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 0 Then
                                        objTr.FAT = clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))
                                        objTr.SNF = clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))
                                        objTr.FATKG = dblFatKg
                                        objTr.SNFKG = dblSnfKg
                                    End If
                                    If clsCommon.myCDecimal(obj.FAT_SNF_Type) = 1 Then
                                        objTr.FAT = dblFat
                                        objTr.SNF = dblSnf
                                        objTr.FATKG = clsCommon.myCDecimal(dtItemData.Rows(k)("FAT"))
                                        objTr.SNFKG = clsCommon.myCDecimal(dtItemData.Rows(k)(colSNForCLR))
                                    End If
                                    If isPickCLRInsteadOfSNF Then
                                        Dim snfPer As Decimal = clsEkoPro.getSnfOnCalculation(objTr.FAT, objTr.SNF, corrFactor)
                                        objTr.SNFKG = Math.Round(objTr.Qty * snfPer / 100, 3, MidpointRounding.ToEven)
                                    End If
                                    'objTr.FAT = dblFat
                                    'objTr.SNF = dblSnf
                                    'objTr.FATKG = dblFatKg
                                    'objTr.SNFKG = dblSnfKg
                                    'obj.Entered_Qty += objTr.Qty
                                    'obj.Entered_FATKg += objTr.FATKG
                                    'obj.Entered_SNFKg += objTr.SNFKG
                                    obj.Arr.Add(objTr)
                                End If
                            Next
                        End If
                        If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                            Throw New Exception("Please Fill at list one Item")
                        End If
                        'obj.SaveData(obj, isNewEntry)
                        If (obj.SaveData(obj, True, trans)) = False Then
                            trans.Rollback()
                        End If
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Successfully Imported.", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Function CreateNewMCC(ByVal BMCMCCCode As String) As Boolean
        Try
            Dim obj As New clsMccMaster()
            Dim dtDefaultUOM As New DataTable
            Dim qry As String = ""
            If objCommonVar.ApplyDefaultsInMaster = True Then
                qry = "select * from TSPL_UNIT_MASTER  WHERE IsDefault=1"
                dtDefaultUOM = clsDBFuncationality.GetDataTable(qry)
            End If
            Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
            If arrExistCols.Contains(clsMasterDefault.colMCCState) Then
                obj.State_Code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCState))
            End If
            If clsCommon.myLen(obj.State_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.State_Code = clsStateMaster.GetDefault()
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                obj.Is_MCC = IIf(clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBMCC)) = 0, 0, 1)
                If obj.Is_MCC = 1 Then
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, "", obj.State_Code, False, True, True)
                Else
                    obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.BMCU, obj.State_Code, False, True, True)
                End If
            Else
                obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, "", obj.State_Code, False, True, True)
            End If
            If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                Throw New Exception("Error In BMC Code Genertion for Uploader code- " + BMCMCCCode)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCType) Then
                obj.MCC_Type = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCType))
            End If
            If clsCommon.myLen(obj.MCC_Type) <= 0 Then
                obj.MCC_Type = "Co. Owned"
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingVendorCode) Then
                obj.Chilling_Vendor = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingVendorCode))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCName) Then
                obj.MCC_NAME = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCName))
            End If
            If clsCommon.myLen(obj.MCC_NAME) <= 0 Then
                obj.MCC_NAME = BMCMCCCode
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAddress1) Then
                obj.Add1 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAddress1))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAddress2) Then
                obj.Add2 = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAddress2))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCTehsil) Then
                obj.Tehsil = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTehsil))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCPinCode) Then
                obj.Pin_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPinCode))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCity) Then
                obj.City_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCity))
            End If
            If clsCommon.myLen(obj.City_code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.City_code = clsCityMaster.GetDefault()
            End If
            'obj.Country_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCCountry).Value)
            If arrExistCols.Contains(clsMasterDefault.colMCCCountry) Then
                obj.Country_code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCountry))
            End If
            If clsCommon.myLen(obj.Country_code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.Country_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCTelphone) Then
                obj.Telphone = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTelphone))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEmail) Then
                obj.Email = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEmail))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCFax) Then
                obj.Fax = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFax))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMccSuperArea) Then
                obj.MCC_Area = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMccSuperArea))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfStore) Then
                obj.Area_Of_Store = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfStore))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOffice) Then
                obj.Area_Of_Office = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfOffice))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTanker) Then
                obj.Open_Area_For_tanker = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCOpenAreaForTanker))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLab) Then
                obj.Area_Of_LAB = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfLab))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCTotalStorageCapacity) Then
                obj.Total_Storage_capacity = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCTotalStorageCapacity))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDock) Then
                obj.Area_Of_Receiving_DOCK = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfReceivingDock))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCFssaiNo) Then
                obj.FSSAI_NO = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFssaiNo))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDripSaver) Then
                obj.DripSaver = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDripSaver))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCanWasher) Then
                obj.CanWasher = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCanWasher))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCanScrubber) Then
                obj.CanScrubber = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCanScrubber))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCETP) Then
                obj.ETP = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCETP))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEarthing) Then
                obj.Earthing = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEarthing))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCBoiler) Then
                obj.Boiler = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBoiler))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCApplyFailedSample) Then
                obj.Failed_Sample_Apply = (clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCApplyFailedSample)), "Y") = CompairStringResult.Equal)
                If obj.Failed_Sample_Apply Then
                    If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleFAT) Then
                        obj.Failed_Sample_FAT = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFailedSampleFAT))
                        If obj.Failed_Sample_FAT <= 0 Then
                            Throw New Exception("Please provide Failed Sample FAT %")
                        End If
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleSNF) Then
                        obj.Failed_Sample_SNF = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCFailedSampleSNF))
                        If obj.Failed_Sample_SNF <= 0 Then
                            Throw New Exception("Please provide Failed Sample SNF %")
                        End If
                    End If
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Status) Then
                obj.agreemnt = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Status))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Date) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Date)) > 0 Then
                obj.agrmnt_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreement_Date))
            Else
                obj.agrmnt_date = clsCommon.GETSERVERDATE()
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAgreementExpiryDate) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreementExpiryDate)) > 0 Then
                obj.expired_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAgreementExpiryDate))
            Else
                obj.expired_date = clsCommon.GETSERVERDATE()
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCSecurity_Status) Then
                obj.secutiy = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSecurity_Status))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Amt) Then
                obj.chq_amt = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Amt))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_No) Then
                obj.chq_no = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_No))
            End If
            'If clsCommon.myLen(obj.chq_no) > 0 Then
            '    If arrExistCols.Contains(colMCCCheque_Date) Then
            '        If clsCommon.myLen(dtDefault.Rows(0).Item(colMCCCheque_Date)) > 0 Then
            '            obj.chq_date = clsCommon.myCDate(dtDefault.Rows(0).Item(colMCCCheque_Date))
            '        Else
            '            obj.chq_date = clsCommon.GETSERVERDATE()
            '        End If
            '    End If
            'End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Date) AndAlso clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Date)) > 0 Then
                obj.chq_date = clsCommon.myCDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCheque_Date))
            Else
                obj.chq_date = clsCommon.GETSERVERDATE()
            End If
            If clsCommon.CompairString(obj.secutiy, "YES") = CompairStringResult.Equal AndAlso (clsCommon.myCDecimal(obj.chq_amt) <= 0 Or clsCommon.myLen(obj.chq_no) <= 0) Then
                Throw New Exception("Please Fill Cheque Amount And Cheque No./Date")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCIndustryType) Then
                obj.industry_type = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCIndustryType))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCMonthlyProvision) Then
                If clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMonthlyProvision)), "Y") = CompairStringResult.Equal Then
                    obj.is_Chilling_Provision_Monthly = True
                Else
                    obj.is_Chilling_Provision_Monthly = False
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingCharges) Then
                obj.chilling_rate = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingCharges))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnQty) Then
                obj.chilling_qty = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnQty))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOn) Then
                obj.chilling_kg_ltr = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOn))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty) Then
                obj.chilling_assur_qty = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriod) Then
                obj.chilling_assur_period = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriod))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingStartingDate) Then
                If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingStartingDate)) > 0 Then
                    obj.Chilling_Period_Starting_Date = clsCommon.GetPrintDate(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingStartingDate), "dd-MMM-yyyy")
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeaseCharges) Then
                obj.lease_rate = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeaseCharges))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMHandledDispatched) Then
                obj.Unit_ChillingOnQty = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnUOMHandledDispatched)) = "Handled", "H", "D")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) Then
                obj.Unit_ChillingOn = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingOnUOMKGLTR)) = "KG", "K", "L")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM) Then
                obj.Unit_ChillingMinGuaranteePeriod = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM)) = "Day", "D", IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM)) = "Month", "M", "Y"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeasedChargesUOM) Then
                obj.Unit_RateOfLeasedCharges = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeasedChargesUOM)) = "Day", "D", IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRateofLeasedChargesUOM)) = "Month", "M", "Y"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaofStoreUOM) Then
                obj.Unit_AreaOfStore = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaofStoreUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDockUOM) Then
                obj.Unit_AreaOfReceivingDock = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfReceivingDockUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOfficeUOM) Then
                obj.Unit_AreaOfOffice = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfOfficeUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLabUOM) Then
                obj.Unit_AreaOfLab = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAreaOfLabUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTankerUOM) Then
                obj.Unit_OpenAreaForTankerMovement = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCOpenAreaForTankerUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMccSuperAreaUOM) Then
                obj.Unit_MccSuperArea = IIf(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMccSuperAreaUOM)) = "Sq. Mt.", "M", "F")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCWeighingMachine) Then
                obj.Weighing_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Prompt") = CompairStringResult.Equal, "P", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Delta") = CompairStringResult.Equal, "D", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Panasonic") = CompairStringResult.Equal, "B", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingMachine)), "Supertech") = CompairStringResult.Equal, "S", "C"))))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCSampleMachine) Then
                obj.Sample_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleMachine)), "Kanha") = CompairStringResult.Equal, "K", IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleMachine)), "Everest New") = CompairStringResult.Equal, "N", "E"))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCWeighingComPort) Then
                obj.Weighing_Comport = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCWeighingComPort))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCSampleComPort) Then
                obj.Sample_comport = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSampleComPort))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCPaymentCycle) Then
                If clsCommon.myLen(obj.Payment_Cycle) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                    obj.Payment_Cycle = clsPaymentCycleMaster.GetDefault()
                Else
                    obj.Payment_Cycle = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPaymentCycle))
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningOpeningTime) Then
                obj.Shift_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftMorningOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningClosingTime) Then
                obj.Shift_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftMorningClosingTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningOpeningTime) Then
                obj.Shift_Eve_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftEveningOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningClosingTime) Then
                obj.Shift_Eve_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCShiftEveningClosingTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRequiredGateEntry) Then
                obj.is_Reuired_Gate_Entry = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRequiredGateEntry)), "Yes") = CompairStringResult.Equal, True, False)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCRM) Then
                obj.EMP_CODE = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCRM))
            End If
            obj.MCC_Code_VLC_Uploader = BMCMCCCode
            ''obj.Loc_Segment_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCLocSegmentCode).Value)
            If clsCommon.myLen(obj.Loc_Segment_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                obj.Loc_Segment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code from TSPL_GL_SEGMENT_CODE where seg_no=7 and len(Segment_code)>0 "))
            End If
            If True Then
                Dim isValidSegmentcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count (*) from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"))
                If isValidSegmentcode = False AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                    'Create Segment code
                    Dim coll As New Hashtable()
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Seg_No", "7")
                    clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                    clsCommon.AddColumnsForChange(coll, "Segment_Name", "Location")
                    clsCommon.AddColumnsForChange(coll, "Description", obj.MCC_NAME)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "GIT", "N")
                    clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.State_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_CODE", OMInsertOrUpdate.Insert, "")
                ElseIf isValidSegmentcode = False Then
                    Throw New Exception("Invalid Loc Segment Code.")
                End If
            End If
            'Create GL Security
            qry = "select count(User_Code) from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' and GL_Segment='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"
            Dim check1 As Integer = CInt(clsDBFuncationality.getSingleValue(qry))
            If check1 <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                Dim coll As New Hashtable()
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "GL_Segment", "7")
                clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Default_Segment", "N")
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_PERMISSION", OMInsertOrUpdate.Insert, "")
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                obj.Is_MCC = IIf(clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCBMCC)) = 0, 0, 1)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCIsTruckSheetMandatory) Then
                obj.Is_Truck_Sheet = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCIsTruckSheetMandatory)), "Yes") = CompairStringResult.Equal, 1, 0)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAllowAutoMilkIn) Then
                obj.AllowAutoMilkIn = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAllowAutoMilkIn))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCAutoIn_Location) Then
                obj.AutoIn_Location = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAutoIn_Location))
                qry = "select 1  from TSPL_LOCATION_MASTER where Location_Code='" + obj.AutoIn_Location + "' and Location_Category='MCC'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                        obj.SILOIn_Location = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSILOIn_Location))
                    End If
                Else
                    obj.SILOIn_Location = ""
                End If
                If clsCommon.myCDecimal(obj.AllowAutoMilkIn) = 1 Then
                    If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCAutoIn_Location)) <= 0 Then
                        Throw New Exception("Allow auto Milk is true So Auto In Location cannot be blank")
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                        If clsCommon.myLen(dtDefault.Rows(0).Item(clsMasterDefault.colMCCSILOIn_Location)) <= 0 Then
                            Throw New Exception("Allow auto Milk is true So Silo In Location cannot be blank")
                        End If
                    End If
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCApplyReceiptWeightTolerance) Then
                obj.Receipt_Weight_tolerance_Apply = IIf(clsCommon.CompairString(clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCApplyReceiptWeightTolerance)), "Y") = CompairStringResult.Equal, True, False)
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCReceiptWeightToleranceValue) Then
                obj.Receipt_Weight_tolerance_Value = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCReceiptWeightToleranceValue))
            End If
            If obj.Receipt_Weight_tolerance_Apply Then
                If obj.Receipt_Weight_tolerance_Value < 0 Then
                    Throw New Exception("Value of ReceiptWeightToleranceValue can't be -ve")
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionRate) Then
                obj.Commission_Rate = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionRate))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle) Then
                obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumQtyInShift) Then
                obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionMinimumQtyInShift))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP) Then
                obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumFATPer) Then
                obj.Deduction_Minimum_FAT_Per = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionMinimumFATPer))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumSNFPer) Then
                obj.Deduction_Minimum_SNF_Per = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionMinimumSNFPer))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP) Then
                obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCDecimal(dtDefault.Rows(0).Item(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCPlant) Then
                obj.Plant_Code = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPlant))
                If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                    Throw New Exception("Please define Main Plant in location master")
                End If
                obj.Plant_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where location_code='" + obj.Plant_Code + "'"))
                If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                    Throw New Exception("Invalid location [" + clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCPlant)) + "]")
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftOpeningTime) Then
                obj.Shift_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMorningShiftOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftClosingTime) Then
                obj.Shift_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCMorningShiftClosingTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftOpeningTime) Then
                obj.Shift_Eve_Opening_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEveningShiftOpeningTime))
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftClosingTime) Then
                obj.Shift_Eve_Closing_Time = clsCommon.myCstr(dtDefault.Rows(0).Item(clsMasterDefault.colMCCEveningShiftClosingTime))
            End If
            If True Then
                Dim objgn As clsGenSetDetail
                obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                For j As Integer = 0 To obj.NoOfDG
                    objgn = New clsGenSetDetail()
                    objgn.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objgn.Trans_Code = obj.MCC_Code
                    objgn.Line_No = (j + 1)
                    objgn.Gen_Set_Desc = "N/A"
                    obj.arrGenSetDetail.Add(objgn)
                Next
                Dim objcomp As clsCompressorDetail
                obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                For j As Integer = 0 To obj.NoOfCompressor
                    objcomp = New clsCompressorDetail
                    objcomp.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objcomp.Trans_Code = obj.MCC_Code
                    objcomp.Line_No = (j + 1)
                    objcomp.Compressor_Desc = "N/A"
                    obj.arrCompressorDetail.Add(objcomp)
                Next
                Dim objSilo As clsSiloDetail
                obj.arrSiloDetail = New List(Of clsSiloDetail)
                For j As Integer = 0 To obj.No_Of_SILO
                    objSilo = New clsSiloDetail()
                    objSilo.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objSilo.Trans_Code = obj.MCC_Code
                    objSilo.Line_No = (j + 1)
                    objSilo.Silo_Desc = "N/A"
                    objSilo.Silo_Area = 0
                    objSilo.Silo_Unit = ""
                    obj.arrSiloDetail.Add(objSilo)
                Next
                Dim objmilkpump As clsMilkPumpDetail
                obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                For j As Integer = 0 To obj.No_Of_MilkPump
                    objmilkpump = New clsMilkPumpDetail()
                    objmilkpump.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objmilkpump.Trans_Code = obj.MCC_Code
                    objmilkpump.Line_No = (j + 1)
                    objmilkpump.Pump_Desc = "N/A"
                    objmilkpump.Pump_Area = 0
                    objmilkpump.Pump_Unit = ""
                    obj.arrMilkPumpDetail.Add(objmilkpump)
                Next
                Dim objChiller As clsChillerDetail
                obj.arrChillerDetail = New List(Of clsChillerDetail)
                For j As Integer = 0 To obj.No_Of_Chiller
                    objChiller = New clsChillerDetail()
                    ' objChiller = New clsMilkPumpDetail()
                    objChiller.Prog_Code = clsUserMgtCode.frmMCCMaster
                    objChiller.Trans_Code = obj.MCC_Code
                    objChiller.Chiller_Desc = "N/A"
                    objChiller.Chiller_Brand = ""
                    objChiller.Chiller_Capacity = 0
                    obj.arrChillerDetail.Add(objChiller)
                Next
                Dim objMccUOM As clsMccUOMDetails
                obj.ArrUomDetails = New List(Of clsMccUOMDetails)
                If objCommonVar.ApplyDefaultsInMaster = True Then
                    If dtDefaultUOM IsNot Nothing AndAlso dtDefaultUOM.Rows.Count > 0 Then
                        objMccUOM = New clsMccUOMDetails()
                        objMccUOM.Mcc_Code = obj.MCC_Code
                        objMccUOM.UOM_Code = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_code"))
                        objMccUOM.UOM_Description = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_Desc"))
                        objMccUOM.Stocking_Unit = "Y"
                        objMccUOM.Conversion_Factor = clsCommon.myCDecimal(dtDefaultUOM.Rows(0)("Conv_Factor"))
                        obj.ArrUomDetails.Add(objMccUOM)
                    End If
                ElseIf arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) AndAlso clsCommon.myLen(dtDefaultUOM.Rows(0)(clsMasterDefault.colMCCChillingOnUOMKGLTR)) > 0 Then
                    qry = "select * from TSPL_UNIT_MASTER  WHERE unit_code='" + clsCommon.myCstr(dtDefaultUOM.Rows(0)(clsMasterDefault.colMCCChillingOnUOMKGLTR)) + "'"
                    Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                    objMccUOM = New clsMccUOMDetails()
                    objMccUOM.Mcc_Code = obj.MCC_Code
                    objMccUOM.UOM_Code = clsCommon.myCstr(dttemp.Rows(0)("Unit_code"))
                    objMccUOM.UOM_Description = clsCommon.myCstr(dttemp.Rows(0)("Unit_Desc"))
                    objMccUOM.Stocking_Unit = "Y"
                    objMccUOM.Conversion_Factor = clsCommon.myCDecimal(dttemp.Rows(0)("Conv_Factor"))
                    obj.ArrUomDetails.Add(objMccUOM)
                End If
                obj.arrChequeDetail = New List(Of clsMCCChequeDetails)
                obj.isNewEntry = True
                obj.Modified_By = objCommonVar.CurrentUserCode
                obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                obj.Comp_Code = objCommonVar.CurrentCompanyCode
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                clsMccMaster.SaveData(obj)
            End If
        Catch ex As Exception
            Return False
            myMessages.myExceptions(ex)
        End Try
        Return True
    End Function
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            UpdateAllTotal()
            If clsCommon.myCDecimal(txtTotPendingQty.Text) > 0 AndAlso clsCommon.myCDecimal(txtTotPendingFAT.Text) > 0 AndAlso clsCommon.myCDecimal(txtTotPendingSNF.Text) > 0 Then
                Dim qry As String = "select top 1 MCC_Code,MCC_NAME,Mcc_Code_VLC_Uploader from TSPL_MCC_MASTER where IsSuspense=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please Set Suspence BMC")
                End If
                Dim flag As Boolean = False
                Dim ii As Integer = 0
                For ii = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value) = 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colMCCCode).Value) <= 0 Then
                        flag = True
                        Exit For
                    End If
                Next
                If Not flag Then
                    gv1.Rows.AddNew()
                    ii = (gv1.Rows.Count - 1)
                End If
                Try
                    isCellValueChangedOpen = True
                    gv1.Rows(ii).Cells(colMilkType).Value = "Good"
                    gv1.Rows(ii).Cells(colMCCUploaderCode).Value = clsCommon.myCstr(dt.Rows(0)("Mcc_Code_VLC_Uploader"))
                    gv1.Rows(ii).Cells(colMCCCode).Value = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                    gv1.Rows(ii).Cells(colMCCName).Value = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
                    gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCDecimal(txtTotPendingQty.Text)
                    gv1.Rows(ii).Cells(colFATKG).Value = clsCommon.myCDecimal(txtTotPendingFAT.Text)
                    gv1.Rows(ii).Cells(colSNFKG).Value = clsCommon.myCDecimal(txtTotPendingSNF.Text)
                    gv1.Rows(ii).Cells(colFATPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value), 1, MidpointRounding.ToEven)
                    gv1.Rows(ii).Cells(colSNFPer).Value = Math.Round((100 * clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFKG).Value)) / clsCommon.myCDecimal(gv1.Rows(ii).Cells(colQty).Value), 1, MidpointRounding.ToEven)
                    gv1.Rows(ii).Cells(colFATPerNoDecimal).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colFATPer).Value).Replace(".", "")
                    gv1.Rows(ii).Cells(colSNFPerNoDecimal).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(colSNFPer).Value).Replace(".", "")
                    UpdateAllTotal()
                Catch ex As Exception
                Finally
                    isCellValueChangedOpen = False
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select document code first..", Me.Text)
            End If
            Dim qry = " select TSPL_COMPANY_MASTER.Comp_Code , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 , TSPL_COMPANY_MASTER.Add2 , TSPL_COMPANY_MASTER.Add3 ,TSPL_COMPANY_MASTER.City_Code, TSPL_COMPANY_MASTER.State ,TSPL_COMPANY_MASTER.Pan_No ,TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.GSTINNo, TSPL_COMPANY_MASTER.CINNo ,TSPL_COMPANY_MASTER.Phone1 , TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No  ,TSPL_COMPANY_MASTER.Email ,TSPL_MILK_COLLECTION_MCC.Document_No,convert (varchar, TSPL_MILK_COLLECTION_MCC.Document_Date,103) as Document_Date, TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME , TSPL_MILK_COLLECTION_MCC.Tanker_No, TSPL_MILK_COLLECTION_MCC.Vehicle_No , TSPL_MILK_COLLECTION_MCC.Entered_Qty , TSPL_MILK_COLLECTION_MCC.Entered_FATKg, TSPL_MILK_COLLECTION_MCC.Entered_SNFKg , TSPL_MILK_COLLECTION_MCC.Status , TSPL_MILK_COLLECTION_MCC_DETAIL.SNo , TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code , TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type, TSPL_MILK_COLLECTION_MCC_DETAIL.Qty , TSPL_MILK_COLLECTION_MCC_DETAIL.FAT , TSPL_MILK_COLLECTION_MCC_DETAIL.SNF , TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG , TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG
                        from TSPL_MILK_COLLECTION_MCC_DETAIL 
                        left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                        left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_MILK_COLLECTION_MCC.Created_By
                        left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_USER_MASTER.Comp_Code
                        left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
                         where TSPL_MILK_COLLECTION_MCC.Document_No = '" + txtDocNo.Value + "'
                        "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBMCTrackSheet", "BMC Truck Sheet", clsCommon.myCDate(txtDate.Value))
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please Select Document No")
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_MILK_COLLECTION_MCC", "TSPL_MILK_COLLECTION_MCC_DETAIL")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDate_Leave(sender As Object, e As EventArgs) Handles txtDate.Leave
        LoadTransactionData()
    End Sub
    Private Sub LoadTransactionData()
        Try
            If clsCommon.myLen(txtRoute.Value) > 0 Or clsCommon.myLen(txtTankerNo.Value) > 0 Or clsCommon.myLen(txtTripNo.Value) > 0 Then
                Dim strQry1 As String = ""
                If clsCommon.myLen(txtTankerNo.Value) > 0 Then
                    strQry1 += " And Tanker_No='" + txtTankerNo.Value + "'"
                End If
                If clsCommon.myLen(txtTripNo.Value) > 0 Then
                    strQry1 += " And Trip_No='" + clsCommon.myCstr(txtTripNo.Value) + "'"
                End If
                Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_MILK_COLLECTION_MCC where convert(date, Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and Route_Code='" + txtRoute.Value + "'" + strQry1))
                If clsCommon.myLen(strDocNo) > 0 Then
                    LoadData(strDocNo, NavigatorType.Current)
                ElseIf clsCommon.myLen(txtTripNo.Value) > 0 AndAlso clsCommon.myLen(txtRoute.Value) = 0 Then
                    AddNewTrip()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnAddMissing_Click(sender As Object, e As EventArgs) Handles btnAddMissing.Click
        Try
            Dim Arr As List(Of clsMilkCollectionMCCDetail) = GetTRData(True)
            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                Throw New Exception("Please Fill at list one Item")
            Else
                If clsCommon.MyMessageBoxShow(Me, "This Feature will add only Missing Simples" + Environment.NewLine + "Add [" + clsCommon.myCstr(Arr.Count) + "] Missing Samples" + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    clsMilkCollectionMCCDetail.AddMissing(txtDocNo.Value, Arr)
                    clsCommon.MyMessageBoxShow(Me, "Missing Samples Added successfully", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(txtRoute.Value) <= 0 Then
                Throw New Exception("Please First select Route")
            End If
            Dim qry As String = "select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as UploaderNo,TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO as [Route Code],TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route Name]
from TSPL_BULK_ROUTE_MASTER_MCC
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_BULK_ROUTE_MASTER_MCC.MCC_Code
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO
where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO not in ('" + txtRoute.Value + "')"
            Dim arrList As ArrayList = clsCommon.ShowMultipleSelectForm("addBMCMCC", qry, "Code", "", Nothing, Nothing)
            If arrList IsNot Nothing AndAlso arrList.Count > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For Each MCCCode As String In arrList
                        qry = "Delete from TSPL_BULK_ROUTE_MASTER_MCC where MCC_Code='" + MCCCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "ROUTE_NO", txtRoute.Value)
                        clsCommon.AddColumnsForChange(coll, "MCC_Code", MCCCode)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_ROUTE_MASTER_MCC", OMInsertOrUpdate.Insert, "", trans)
                        'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, vlcCode, "TSPL_BULK_ROUTE_MASTER_MCC_Hist_Data", "vlc_code", trans)
                    Next
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub AddNewTrip()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        cboLate.SelectedValue = "0"
        If SettMilkCollectionFATSNFType = 0 Then
            cboFATSNFType.SelectedValue = "0"
        Else
            cboFATSNFType.SelectedValue = "1"
        End If
        txtDocNo.Value = ""
        txtDesc.Text = ""
        isNewEntry = True
        'txtRoute.Value = ""
        lblRoute.Text = ""
        txtVehicleNo.Text = ""
        txtTotEnteredQty.Text = ""
        txtTotEnteredFAT.Text = ""
        txtTotEnteredSNF.Text = ""
        txtTotPendingFAT.Text = ""
        txtTotPendingQty.Text = ""
        txtTotPendingSNF.Text = ""
        txtTotReceivedFAT.Text = ""
        txtTotReceivedQty.Text = ""
        txtTotReceivedSNF.Text = ""
        txtTotPendingFATPer.Text = ""
        txtTotPendingSNFPer.Text = ""
        txtSlipNo.Text = ""
        txtTotEnteredFATPer.Text = ""
        txtTotEnteredSNFPer.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colMilkType).Value = "Good"
        gvTotal.DataSource = Nothing
        btnAddMissing.Enabled = False
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        isCellValueChangedOpen = False
        txtDate.Enabled = True
        txtRoute.Focus()
    End Sub
    Private Sub txtTripNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTripNo.Validating
        LoadTransactionData()
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            'If clsMilkCollectionMCC.ReverseAndUnpost(False) Then
            '    clsCommon.MyMessageBoxShow("Cannot be reversed and posted.", Me.Text)
            '    Exit Sub
            'End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                clsMilkCollectionMCC.ReverseAndUnpost(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Dim intCountDB As Integer = 0
    Private Sub RadLabel4_DoubleClick(sender As Object, e As EventArgs) Handles RadLabel4.DoubleClick
        If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso UsLock1.Status = ERPTransactionStatus.Pending Then
            txtDate.Enabled = True
            intCountDB += 1
            If intCountDB = 3 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    'If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    '' REASON FOR DELETE 
                    Dim Reason As String = ""
                    Dim frm1 As New frmMyDateTimePicker2
                    frm1.Text = "Update The Date"
                    frm1.RetValue = Me.txtDate.Value
                    frm1.ShowDialog()
                    'If clsCommon.myLen(frm1.strRmks) <= 0 Then
                    '    Exit Sub
                    'Else
                    '    Reason = frm1.strRmks
                    txtDate.Value = frm1.RetValue
                    frm1.Dispose()
                    'End If
                End If
                intCountDB = 0
            End If
        End If
    End Sub
    Private Sub btnMGo_Click(sender As Object, e As EventArgs) Handles btnMGo.Click
        Try
            Dim Arr As New List(Of clsBMCDCSMobile)
            For Each lst As clsBMCDCSMobile In clsBMCDCSMobile.GetData(txtMDate.Value)
                Arr.Add(lst)
            Next
            ' Add MCC Truck Sheet Entry
            If Arr.Count > 0 Then
                For Each lst As clsBMCDCSMobile In Arr
                    Dim strQry = "select Document_No from TSPL_MILK_COLLECTION_MCC where Route_Code='" + clsCommon.myCstr(lst.Route_Code) + "' and Document_Date='" + clsCommon.GetPrintDate(lst.Document_Date) + "' and Trip_No=" + clsCommon.myCstr(lst.Trip_No)
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        lst.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                        isNewEntry = False
                        BMCEntry(lst)
                    Else
                        isNewEntry = True
                        BMCEntry(lst)
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "BMC Truck Sheet Data saved successfully", Me.Text)
            Else
                Throw New Exception("No Data Found!")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub BMCEntry(ByRef lst As clsBMCDCSMobile)
        Try
            Dim obj As New clsMilkCollectionMCC()
            'obj.REF_PK_ID = lst.REF_PK_ID
            obj.Document_No = lst.Document_No
            obj.Document_Date = lst.Document_Date
            obj.Route_Code = lst.Route_Code
            obj.Tanker_No = lst.Tanker_No
            obj.Vehicle_No = lst.Vehicle_No
            obj.Trip_No = lst.Trip_No
            obj.Entered_Qty = lst.Entered_Qty
            obj.Entered_FATKg = lst.Entered_FATKg
            obj.Entered_SNFKg = lst.Entered_SNFKg
            obj.Description = "Uploaded By Mobile APP"
            obj.Arr = GetBMCTRData(False, lst)
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                Throw New Exception("Please Fill at list one Item")
            End If
            obj.SaveData(obj, isNewEntry)
            'DCSEntry(lst)
            ' clsCommon.MyMessageBoxShow(Me, "BMC Truck Sheet Data saved successfully", Me.Text)
            'LoadData(obj.Document_No, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetBMCTRData(ByVal isMissingOnly As Boolean, ByRef lst As clsBMCDCSMobile) As List(Of clsMilkCollectionMCCDetail)
        Dim Arr As New List(Of clsMilkCollectionMCCDetail)
        For ii As Integer = 0 To lst.Arr_BMCDCS_Trip.Count - 1
            If clsCommon.myLen(lst.MCC_Code) > 0 Then
                If clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty) > 0 Then
                    Dim flag As Boolean = True
                    If clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).PK_ID) > 0 AndAlso isMissingOnly Then
                        flag = False
                    End If
                    If flag Then
                        Dim objTr As New clsMilkCollectionMCCDetail()
                        objTr.SNo = ii + 1
                        'objTr.Sample_No = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSampleNo).Value)
                        objTr.MCC_Code = clsCommon.myCstr(lst.Arr_BMCDCS_Trip(ii).MCC_Code)
                        objTr.Milk_Type = clsCommon.myCstr("Good")
                        objTr.Qty = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty)
                        objTr.FAT = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FAT), 2, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNF), 2, MidpointRounding.ToEven)
                        objTr.FATKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).FATKG)
                        objTr.SNFKG = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).SNFKG)
                        objTr.Temp = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Temp)
                        objTr.Gaze_Reading_Code = clsCommon.myCstr(lst.Arr_BMCDCS_Trip(ii).Gaze_Reading_Code)
                        objTr.Gaze_Reading = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Gaze_Reading)
                        objTr.Silo_Capacity = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Silo_Capacity)
                        objTr.Sample_No = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Sample_No)
                        objTr.REF_PK_ID_BMCDCS_TRIP = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).PK_ID)
                        If clsCommon.myLen(objTr.Gaze_Reading_Code) > 0 Then
                            objTr.Gaze_Qty = clsCommon.myCDecimal(lst.Arr_BMCDCS_Trip(ii).Qty)
                        End If
                        Arr.Add(objTr)
                    End If
                End If
            End If
        Next
        Return Arr
    End Function

    Private Sub gv1_Validating(sender As Object, e As CancelEventArgs) Handles gv1.Validating

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Import()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub
    Public Sub Import()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim Arr As New List(Of clsMilkCollectionMCC)
            DtError = New DataTable
            DtError.Columns.Add("Code", GetType(String))
            DtError.Columns.Add("Error", GetType(String))
            Dim chkdp As New List(Of String)
            Dim str As String = ""
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Tanker No", "Route No", "TEMP.", "FAT", "SNF", "ACIDITY", "ORG.", "Document Date", "Remark", "Qty", "Trip No") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try
                    'trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows

                        Dim obj As New clsMilkCollectionMCC()
                        Try
                            linno += 1

                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Document Date").Value))) Then
                                Throw New Exception("Document Date is Blank at Line No" + clsCommon.myCstr(linno))
                            Else
                                obj.Document_Date = clsCommon.GetPrintDate(grow.Cells("Document Date").Value)
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Route No").Value))) Then
                                Throw New Exception("Route No is Blank at Line No" + clsCommon.myCstr(linno))
                            Else
                                'Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Route_No from TSPL_ROUTE_MASTER where Route_No='" + clsCommon.myCstr(grow.Cells("Route No").Value) + "'"))
                                'If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Route No").Value)) = CompairStringResult.Equal Then
                                obj.Route_Code = clsCommon.myCstr(grow.Cells("Route No").Value)
                                'Else
                                'Throw New Exception("Route No is Invalid at Line No" + clsCommon.myCstr(linno))
                                'End If

                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Tanker No").Value))) Then
                                Throw New Exception("Tanker No is Blank at Line No" + clsCommon.myCstr(linno))

                            Else
                                obj.Tanker_No = clsCommon.myCstr(grow.Cells("Tanker No").Value)
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Trip No").Value))) Then
                                Throw New Exception("Trip No is Blank at Line No" + clsCommon.myCstr(linno))
                            Else
                                obj.Trip_No = clsCommon.myCDecimal(grow.Cells("Trip No").Value)
                            End If

                            obj.Temp = clsCommon.myCDecimal(grow.Cells("TEMP.").Value)
                            obj.Acidity = clsCommon.myCDecimal(grow.Cells("ACIDITY").Value)
                            obj.ORG = clsCommon.myCstr(grow.Cells("ORG.").Value)

                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Remark").Value)) > 0 Then
                                obj.Description = clsCommon.myCstr(grow.Cells("Remark").Value)
                            Else
                                obj.Description = txtDesc.Text
                            End If

                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Qty").Value))) Then
                                Throw New Exception("Qty is Blank at Line No" + clsCommon.myCstr(linno))

                            Else
                                If (clsCommon.myCDecimal(grow.Cells("Qty").Value)) > 0 Then
                                    obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)
                                Else
                                    Throw New Exception("Qty is Zero at Line No" + clsCommon.myCstr(linno))
                                End If

                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("FAT").Value))) Then
                                Throw New Exception("FAT is Blank at Line No" + clsCommon.myCstr(linno))

                            Else
                                If (clsCommon.myCDecimal(grow.Cells("FAT").Value)) > 0 Then
                                    If SettMilkCollectionFATSNFType = 0 Then
                                        obj.Entered_FATKg = obj.Entered_Qty * (clsCommon.myCstr(grow.Cells("FAT").Value) / 100)
                                    Else
                                        obj.Entered_FATKg = clsCommon.myCDecimal(grow.Cells("FAT").Value)

                                    End If
                                Else
                                    Throw New Exception("FAT is Zero at Line No" + clsCommon.myCstr(linno))

                                End If

                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCDecimal(grow.Cells("SNF").Value))) Then
                                Throw New Exception("SNF is Blank at Line No" + clsCommon.myCstr(linno))

                            Else
                                If (clsCommon.myCDecimal(grow.Cells("SNF").Value)) > 0 Then
                                    If SettMilkCollectionFATSNFType = 0 Then
                                        obj.Entered_SNFKg = obj.Entered_Qty * (clsCommon.myCDecimal(grow.Cells("SNF").Value) / 100)
                                    Else
                                        obj.Entered_SNFKg = clsCommon.myCDecimal(grow.Cells("SNF").Value)
                                    End If
                                Else
                                    Throw New Exception("SNF is Zero at Line No" + clsCommon.myCstr(linno))
                                End If


                            End If
                            Dim Doc_No As String = clsDBFuncationality.getSingleValue("select TSPL_MILK_COLLECTION_MCC.Document_No from  TSPL_MILK_COLLECTION_MCC where CONVERT(Date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(grow.Cells("Document Date").Value) + "' and TSPL_MILK_COLLECTION_MCC.Route_Code='" + obj.Route_Code + "' and Trip_No=" + clsCommon.myCstr(obj.Trip_No) + "  and Status=0")

                            If clsCommon.myLen(Doc_No) <= 0 Then
                                Throw New Exception("Data not exists for Line No" + clsCommon.myCstr(linno))
                            Else
                                Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No and TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + clsCommon.myCstr(Doc_No) + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                    Throw New Exception("Milk Purchase Invoice Generated [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + "]")
                                End If
                                obj.Document_No = Doc_No

                            End If
                            str = clsCommon.myCstr(obj.Document_Date) + clsCommon.myCstr(obj.Route_Code) + clsCommon.myCstr(obj.Trip_No)
                            If chkdp.Contains(str) Then
                                Throw New Exception("Duplicate Data Found at Line No " + clsCommon.myCstr(linno))
                            Else
                                chkdp.Add(str)
                            End If
                            Arr.Add(obj)
                        Catch ex As Exception
                            dr = DtError.NewRow()
                            dr("Code") = clsCommon.myCstr(linno)
                            dr("Error") = ex.Message
                            DtError.Rows.Add(dr)
                        End Try

                    Next
                    clsCommon.ProgressBarHide()

                    If clsCommon.MyMessageBoxShow(Me, "Total Valid Document [" + clsCommon.myCstr(Arr.Count) + "] and Invalid Document  [" + clsCommon.myCstr(linno - Arr.Count) + "] Are You Sure.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.OK Then
                        If DtError IsNot Nothing AndAlso DtError.Rows.Count > 0 Then
                            Dim frm As New FrmFreeGrid()
                            frm.strFormName = "Error In Import Tranker Data "
                            frm.dt = DtError
                            frm.ReportID = "BMC Truck Sheet"
                            frm.ShowDialog()
                        Else
                            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                                Dim isCorrection As Integer = 0
                                For Each objMCC As clsMilkCollectionMCC In Arr
                                    clsMilkCollectionMCC.CorrectionData(objMCC, isCorrection)
                                    'clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
                                Next
                                common.clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text, MessageBoxButtons.OK)
                            End If
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Failed", Me.Text, MessageBoxButtons.OK)
                    End If
                    clsCommon.ProgressBarHide()

                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Export()
        Try
            Dim str As String = "select  '' as [Tanker No], '' as [Route No], '' as [TEMP.], '' as [FAT], '' as [SNF],'' as [ACIDITY],'' as [ORG.],'' as [Document Date],'' as [Remark], '' as [Qty], '' as [Trip No]"
            Dim whrCls As String = ""

            ListImpExpColumnsMandatory = New List(Of String)({"Document Date", "Route No", "Tanker No", "Trip No", "Qty", "FAT", "SNF"})
            transportSql.ExporttoExcel(str, whrCls, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
