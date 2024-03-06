Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmJobMilkQualityCheck
    Inherits FrmMainTranScreen
    Public strDocCode As String = Nothing
    Public strDocType As String = Nothing
    Public Const colSLNo As String = "SLNO"
    Public Const colSealName As String = "SealName"
    Public Const colSealStatus As String = "SealStatus"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colDipValue As String = "colDipValue"
    Dim docType As String = String.Empty
    Public Shared isPortOpened As Boolean = False
    Dim obj As clsMilkQualityCheck = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoCLR As String = "AutoCLR"
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim oldValue As Integer = 0
    Dim objEco As New clsEkoPro
    Dim objSr As New clsSerialPort
    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub reset(ByVal isLoad As Boolean)
        reset(isLoad, True)
    End Sub

    Sub reset(ByVal isLoad As Boolean, ByVal isResetQCFinder As Boolean)
        fndLocation.Enabled = True
        chkBulkMilkProc.IsChecked = True
        GetECOPro()
        'grpBulkProc.Visible = True
        If isResetQCFinder Then
            fndQcNo.Value = ""
        End If
        btnReverse.Visible = False
        lblQcAcceptedOrRejected.Text = ""
        fndGateEntryNo.Value = ""
        dtpQCInDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpQCOutDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndTankerNo.Value = ""
        txtChallanNo.Text = ""
        txtDipValue.Text = ""
        dtpChallanDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        lblStatusValue.Text = ""
        txtWeighmentNo.Text = ""
        dtpWeighmentDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndVendor.Value = ""
        lblVendorName.Text = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""

        TxtDeductionAmount.Value = 0
        TxtDeductionAmount.Enabled = False
        ''-----------------------------
        If chkBulkMilkProc.IsChecked Then
            If Not clsfrmParameterMaster.isFATParmExist() Then
                Throw New Exception("FAT parameter Does not exist. Please make it first")
                'chkMccProc.IsChecked = True
            End If
            If Not clsfrmParameterMaster.isSNFParmExist() Then
                Throw New Exception("SNF parameter Does not exist. Please make it first")
                'chkMccProc.IsChecked = True
            End If
            'If (Not clsfrmParameterMaster.isCLRParmExist()) AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
            '    Throw New Exception("CLR parameter Does not exist. Please make it first")
            '    'chkMccProc.IsChecked = True
            'End If
            'If Not clsfrmParameterMaster.isCFParmExist() AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
            '    Throw New Exception("CF parameter Does not exist. Please make it first")
            '    'chkMccProc.IsChecked = True
            'End If
            lblVendor.Text = "Vendor"
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        ElseIf chkMccProc.IsChecked Then
            lblVendor.Text = "Vendor"
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
        End If
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSendForApproval.Enabled = False
        btnPrint.Enabled = False
        btnSave.Text = "Save"
        loadBlankItemGrid()
        loadBlankParameterGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        'RadPageView1.SelectedPage = RadPageViewPage1
        objSr.SetPortNameValues(cboComPort)
        cboECOPro.SelectedValue = 0 'Nothing
        clsPortSetting.GetMachineType(CboMachine)
        loadBlankGVPaperSeal()
        loadBlankGVManualSeal()
        GrpControlSample.Visible = chkMccProc.IsChecked
        txtDispControlSampleFAT.Text = ""
        txtDispControlSampleSNF.Text = ""
        txtRcptControlSampleFAT.Text = ""
        txtRcptControlSampleSNF.Text = ""
    End Sub
    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBulkMilkProc.ToggleStateChanged
        reset(False, True)
    End Sub
    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccProc.ToggleStateChanged
        reset(False, True)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then
            If clsCommon.myCdbl(gvParam.Rows(0).Cells(SNFColName).Value) <= 0 AndAlso clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value) <= 0 Then
                If clsCommon.MyMessageBoxShow("SNF Value is 0 and FAT value is 0, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    SaveData(False)
                Else
                    Exit Sub
                End If

            ElseIf clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value) <= 0 Then
                If clsCommon.MyMessageBoxShow("FAT Value is 0, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    SaveData(False)
                Else
                    Exit Sub
                End If

            ElseIf clsCommon.myCdbl(gvParam.Rows(0).Cells(SNFColName).Value) <= 0 Then
                If clsCommon.MyMessageBoxShow("SNF Value is 0 , Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    SaveData(False)
                Else
                    Exit Sub
                End If
            Else

                SaveData(False)
            End If
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset(False, True)
    End Sub
    Sub loadBlankItemGrid()

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSlNo, "SL. NO.")
        gvItem.Columns(colSlNo).Width = 60
        gvItem.Columns(colSlNo).ReadOnly = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 120
        gvItem.Columns(colQty).ReadOnly = True
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colQty).IsVisible = False

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colUOM).IsVisible = False

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colFat).IsVisible = False
        gvItem.Columns(colFat).ReadOnly = True

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colSNF).IsVisible = False
        gvItem.Columns(colSNF).ReadOnly = True

        gvItem.Columns.Add(colFatKG, "FAT (KG)")
        gvItem.Columns(colFatKG).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colFatKG).IsVisible = False
        gvItem.Columns(colFatKG).ReadOnly = True

        gvItem.Columns.Add(colSNFKG, "SNF (KG)")
        gvItem.Columns(colSNFKG).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colSNFKG).IsVisible = False
        gvItem.Columns(colSNFKG).ReadOnly = True


        'gvItem.Columns.Add(colDipValue, "DIP Value")
        'gvItem.Columns(colDipValue).Width = 75
        'gvItem.Columns(colDipValue).ReadOnly = False


        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSlNo).Value = "1"
        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        ReStoreGridLayout()
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function
    Function LoadSealStatus() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all  select  'Yes' as  Value union all  select  'Change' as  Value union all  select  'N/A' as  Value   "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function
    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Sub loadBlankParameterGrid()
        'Dim CfColName As String = String.Empty
        Dim whrCls As String = String.Empty
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            Exit Sub
        End If
        'If (Not clsfrmParameterMaster.isCLRParmExist()) AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
        '    Throw New Exception("CLR parameter Does not exist. Please make it first")
        '    'chkMccProc.IsChecked = True
        'End If
        'If Not clsfrmParameterMaster.isCFParmExist() AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
        '    Throw New Exception("CF parameter Does not exist. Please make it first")
        '    'chkMccProc.IsChecked = True
        'End If
        If clsERPFuncationality.isLocationMcc(fndLocation.Value) Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            If (Not clsfrmParameterMaster.isCLRParmExist()) Then
                clsCommon.MyMessageBoxShow("CLR parameter Does not exist. Please make it first")
                Exit Sub
            End If
            If Not clsfrmParameterMaster.isCFParmExist() Then
                clsCommon.MyMessageBoxShow("CF parameter Does not exist. Please make it first")
                Exit Sub
            End If
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
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
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                        repoDecimalColumn.FormatString = "{0:n2}"
                    End If
                    repoDecimalColumn.DecimalPlaces = 3
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                        repoDecimalColumn.ReadOnly = True
                    Else
                        repoDecimalColumn.ReadOnly = False
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
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                        repoComboColumn.ReadOnly = True
                    Else
                        repoComboColumn.ReadOnly = False
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
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                        repoComboColumn.ReadOnly = True
                    Else
                        repoComboColumn.ReadOnly = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                        repoTextColumn.ReadOnly = True
                    Else
                        repoTextColumn.ReadOnly = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)
                End If

                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoFat, "Auto FAT")
                    gvParam.Columns(colAutoFat).Width = 120
                    gvParam.Columns(colAutoFat).ReadOnly = True
                    gvParam.Columns(colAutoFat).Tag = "AutoFAT"
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoSnf, "Auto SNF")
                    gvParam.Columns(colAutoSnf).Width = 120
                    gvParam.Columns(colAutoSnf).ReadOnly = True
                    gvParam.Columns(colAutoSnf).Tag = "AutoSNF"
                End If
                If Not clsERPFuncationality.isLocationMcc(fndLocation.Value) Then
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                        gvParam.Columns.Add(colAutoCLR, "Auto CLR")
                        gvParam.Columns(colAutoCLR).Width = 120
                        gvParam.Columns(colAutoCLR).ReadOnly = True
                        gvParam.Columns(colAutoCLR).Tag = "AutoCLR"
                    End If
                End If
            Next
            Try
                If (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                    gvParam.Columns(CfColName).IsVisible = True
                Else
                    gvParam.Columns(CfColName).IsVisible = False
                End If
            Catch ex As Exception
            End Try

            Try
                If (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                    gvParam.Columns(CLRColName).IsVisible = True
                Else
                    gvParam.Columns(CLRColName).IsVisible = False
                End If
            Catch ex As Exception

            End Try
            gvParam.Columns.Add("colRemarks", "Remarks")
            gvParam.Columns("colRemarks").Width = 300
            gvParam.Columns("colRemarks").ReadOnly = False
            gvParam.Columns("colRemarks").Tag = "REM"
            gvParam.Columns("colRemarks").WrapText = True

        End If
        gvParam.Rows.AddNew()
        gvParam.Rows(0).Cells("colSLNO").Value = "1"
        If clsCommon.myLen(CfColName) > 0 Then
            gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        End If
        Try
            If (clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                gvParam.Columns(CLRColName).IsVisible = False
                gvParam.Columns(CfColName).IsVisible = False
            Else
                gvParam.Columns(CLRColName).IsVisible = True
                gvParam.Columns(CfColName).IsVisible = True
            End If
        Catch ex As Exception

        End Try
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
        ReStoreGridLayout()
    End Sub

    Private Sub FrmJobMilkQualityCheck_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.F2 Then
                If gvParam.Rows.Count > 0 Then
                gvParam.Rows(0).Cells(colAutoFat).Value = clsEkoPro.FAT
                gvParam.Rows(0).Cells(colAutoSnf).Value = clsEkoPro.SNF
                If clsCommon.myLen(CfColName) > 0 AndAlso (Not clsERPFuncationality.isLocationMcc(fndLocation.Value)) Then
                    gvParam.Rows(0).Cells(colAutoCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(colAutoFat).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(colAutoSnf).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(CfColName).Value))
                Else
                    gvParam.Rows(0).Cells(colAutoCLR).Value = "0"

                End If

            End If
            'ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.A And chkBulkMilkProc.IsChecked And btnSendForApproval.Enabled Then
            '    Dim frm As New FrmPWD(Nothing)
            '    frm.strType = "SIRC"
            '    frm.strCode = "SIReversAndCreate"
            '    frm.ShowDialog()
            '    If frm.isPasswordCorrect Then
            '        If clsCommon.myLen(fndQcNo) <= 0 Then
            '            clsCommon.MyMessageBoxShow("Please select QC document to approve")
            '            Exit Sub
            '        End If
            '        Dim qry As String = " update tspl_Milk_quality_check set is_Param_Accepted=2,isPosted=0 where QC_No ='" & fndQcNo.Value & "' and Isposted='1' and is_param_accepted=0 "
            '        clsDBFuncationality.ExecuteNonQuery(qry)
            '        loadData(fndQcNo.Value, "Tanker", NavigatorType.Current)
            '        clsCommon.MyMessageBoxShow("Approved.")
            '    End If
        End If
    End Sub

    Private Sub FrmJobMilkQualityCheck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        'If clsERPFuncationality.isCurrentUserMCC() Then
        '    chkMccProc.IsChecked = True
        '    chkBulkMilkProc.Enabled = False
        'Else
        '    chkBulkMilkProc.IsChecked = True
        '    chkBulkMilkProc.Enabled = True
        'End If
        reset(True, True)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        mnuEmailSmsSetting.Visibility = ElementVisibility.Collapsed
        If strDocCode IsNot Nothing AndAlso clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, strDocType, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), strDocType, NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmJobMilkQualityCheck)
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
    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim isFoundQc As String = String.Empty
        isFoundQc = clsDBFuncationality.getSingleValue("select QC_No from tspl_Milk_quality_check where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(isFoundQc) > 0 Then
            loadData(isFoundQc, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsMilkGateEntry()
        objGt = clsMilkGateEntry.getData(strGateEntryNo, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavigatorType.Current)
        If objGt IsNot Nothing Then
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            dtpGateEntryDateTime.Value = objGt.Date_And_Time
            fndTankerNo.Value = objGt.Tanker_No
            txtChallanNo.Text = objGt.Challan_No
            dtpChallanDate.Value = objGt.Challan_Date
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Milk_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_Milk_weighment_detail where  gate_entry_no='" & objGt.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = objGt.location_Code
            lblLocationName.Text = objGt.Location_Desc
            If chkBulkMilkProc.IsChecked Then
                fndVendor.Value = objGt.Vendor_Code
                lblVendorName.Text = objGt.Vendor_Desc
            Else
                fndVendor.Value = objGt.Vendor_Code
                lblVendorName.Text = objGt.Vendor_Desc
                LoadSealDataManual()
                'LoadSealDataPaper()
            End If
            gvItem.Rows(0).Cells(colSlNo).Value = "1"
            gvItem.Rows(0).Cells(colItemCode).Value = objGt.Item_Code
            gvItem.Rows(0).Cells(colItemDesc).Value = objGt.Item_Desc
            gvItem.Rows(0).Cells(colUOM).Value = objGt.UOM
            gvItem.Rows(0).Cells(colQty).Value = objGt.Qty_In_Kg
            gvItem.Rows(0).Cells(colFat).Value = objGt.fat_per
            gvItem.Rows(0).Cells(colSNF).Value = objGt.snf_Per
            gvItem.Rows(0).Cells(colFatKG).Value = objGt.Qty_In_Kg * objGt.fat_per / 100
            gvItem.Rows(0).Cells(colSNFKG).Value = objGt.Qty_In_Kg * objGt.snf_Per / 100
            loadBlankParameterGrid()
            lblQcAcceptedOrRejected.Text = ""
            btnSendForApproval.Enabled = False
            If chkMccProc.IsChecked Then
                GrpControlSample.Visible = True
                txtDispControlSampleFAT.Text = clsMilkQualityCheck.ControlSampleFAT(objGt.Gate_Entry_No)
                txtDispControlSampleSNF.Text = clsMilkQualityCheck.ControlSampleSNF(objGt.Gate_Entry_No)
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
                If clsMilkQualityCheck.isControlSample(objGt.Gate_Entry_No) Then
                    txtRcptControlSampleFAT.Enabled = True
                    txtRcptControlSampleSNF.Enabled = True
                Else
                    txtRcptControlSampleFAT.Enabled = False
                    txtRcptControlSampleSNF.Enabled = False
                End If
            Else
                GrpControlSample.Visible = False
                txtDispControlSampleFAT.Text = ""
                txtDispControlSampleSNF.Text = ""
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
            End If
        End If

    End Sub
    'Sub LoadSealDataPaper()
    '    loadBlankGVPaperSeal()
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details  where chalan_no='" & txtChallanNo.Text & "'")
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            gvPaperSeal.Rows.AddNew()
    '            gvPaperSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
    '            gvPaperSeal.Rows(i).Cells(colSealName).Value = clsCommon.myCstr(dt.Rows(i)("Seal_No"))
    '            gvPaperSeal.Rows(i).Cells(colSealStatus).Value = "Yes"
    '        Next
    '    End If
    'End Sub
    Sub LoadSealDataManual()
        loadBlankGVManualSeal()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select xx.Seal_No as Seal_No ,xx.Chalan_NO as Challan_No  from(select Seal_No1 as Seal_No,Chalan_NO   from TSPL_Mcc_Dispatch_Challan union All select Seal_No2  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No3  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No4  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No5  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No6  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No7  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No8  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No9  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No10  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan) xx where xx.Seal_No <>''  and xx.Chalan_NO='" & txtChallanNo.Text & "' ")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gvManualSeal.Rows.AddNew()
                gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                gvManualSeal.Rows(i).Cells(colSealName).Value = clsCommon.myCstr(dt.Rows(i)("Seal_No"))
                gvManualSeal.Rows(i).Cells(colSealStatus).Value = "Yes"
            Next
        End If
    End Sub
    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        'Dim whrcls As String = String.Empty
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrcls = "and  TSPL_MILK_GATE_ENTRY_DETAILS.location_code in ( " & objCommonVar.strCurrUserLocations & ")"
        '    End If
        'End If
        'fndGateEntryNo.Value = clsMilkQualityCheck.getGateEntryFinder("TSPL_MILK_GATE_ENTRY_DETAILS.isPosted='1' and TSPL_MILK_GATE_ENTRY_DETAILS.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt") & "' and TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No not in (select tspl_Milk_quality_check.Gate_Entry_No from tspl_Milk_quality_check where tspl_Milk_quality_check.gate_entry_no<>'" & fndGateEntryNo.Value & "' ) " & whrcls, fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset(False, False)
        'End If
    End Sub
    Sub calculateSNF()
        Dim isParamOK As Boolean = True
        Dim snfField As String = ""
        Dim fatField As String = ""
        Dim clrField As String = ""
        Dim cfField As String = ""
        For i As Integer = 0 To gvParam.Columns.Count - 1
            If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(0).Cells(i).Value)) Then
                    isParamOK = False
                End If
            End If
            If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                snfField = gvParam.Columns(i).Name
            End If
            If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                fatField = gvParam.Columns(i).Name
            End If
            If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                clrField = gvParam.Columns(i).Name
            End If
            If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                cfField = gvParam.Columns(i).Name
            End If
        Next
        Try
            If isParamOK Then
                gvParam.Rows(0).Cells(snfField).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(cfField).Value))
            Else
                gvParam.Rows(0).Cells(snfField).Value = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                Throw New Exception("Please enter Tanker No")
                errorControl.SetError(fndTankerNo, "Please enter Tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
                Throw New Exception("Please enter Gate Entry No")
                errorControl.SetError(fndGateEntryNo, "Please enter Gate Entry No")
            Else
                errorControl.ResetError(fndGateEntryNo)
            End If
            Dim isManadatory As Integer = 0

            For i As Integer = 0 To gvParam.Columns.Count - 1
                isManadatory = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsMandatory  from TSPL_PARAMETER_MASTER where Code='" & gvParam.Columns(i).Name & "'"))

                If isManadatory = 1 And clsCommon.myLen(gvParam.Columns(i).Name) <= 0 Then
                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                End If
                'If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                '    If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Then
                '        Throw New Exception("Please Fill :" & gvParam.Columns(i).HeaderText & " , Because it is of type :" & gvParam.Columns(i).Tag & " and it is Mandatory ")
                '    End If
                '    If Not IsNumeric(gvParam.Rows(0).Cells(i).Value) Then
                '        Throw New Exception("Field Name " & gvParam.Columns(i).HeaderText & "   is of type " & gvParam.Columns(i).Tag & " Which Must be Numeric ")
                '    End If
                'End If
            Next

            If dtpQCInDateTime.Value > dtpQCOutDateTime.Value Then
                Throw New Exception("'In Date Time' Can Not be Larger Then 'Out Date Time'")
            End If
            '            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_Milk_weighment_detail where gate_entry_no='" & fndGateEntryNo.Value & "' ")) <= 0 AndAlso clsCommon.myCdbl(txtDipValue.Text) <= 0 Then
            'txtDipValue.Focus()
            'Throw New Exception("Please fill Dip Value")
            'End If

            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            If TxtDeductionAmount.Enabled Then
                If clsCommon.myCdbl(TxtDeductionAmount.Value) <= 0 Then
                    Throw New Exception("Deduction amount cannot be left blank or 0")
                End If
            End If
            ''--------------------------------------------------
            If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then
                For i As Integer = 0 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSLNo).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealStatus).Value) = 0 Then
                        Throw New Exception("Please select Seal status for Manual Seal at row no. " & (i + 1))
                    End If
                Next
            End If
            If gvPaperSeal IsNot Nothing AndAlso gvPaperSeal.Rows.Count > 0 Then
                For i As Integer = 0 To gvPaperSeal.Rows.Count - 1
                    If clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSLNo).Value) > 0 AndAlso clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealStatus).Value) = 0 Then
                        Throw New Exception("Please select Seal status for Paper Seal at row no. " & (i + 1))
                    End If
                Next
            End If
            If Not clsMilkQualityCheck.chkIsGridColumnHasTag(gvParam) Then
                Throw New Exception(" Grid's Column is not being recognized, Please delete layout and try loading Document Again ")
            End If
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_Milk_quality_check where gate_entry_no='" & fndGateEntryNo.Value & "' and QC_NO <>'" & fndQcNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpQCInDateTime.Value = dt
                    Throw New Exception("QC In Date should not be Larger than current date")
                End If
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpQCOutDateTime.Value = dt
                    Throw New Exception("QC Out Date should not be Larger than current date")
                End If
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateBeforeGateEntryDateTime, Nothing)) = 0 Then
                If dtpGateEntryDateTime.Value > dtpQCInDateTime.Value Then
                    Throw New Exception("QC in Date/Time should not be Smaller than Gate Entry Date/Time")
                    dtpQCInDateTime.Focus()
                End If
                If dtpGateEntryDateTime.Value > dtpQCOutDateTime.Value Then
                    Throw New Exception("QC Out Date/Time should not be Smaller than Gate Entry Date/Time")
                    dtpQCOutDateTime.Focus()
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function

    Sub SaveData(ByVal isPost As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsMilkQualityCheck()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.JobMilkQualityCheck, "", fndLocation.Value)
                If clsCommon.myLen(obj.QC_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error In QC  No Genertion")
                    Exit Sub
                End If
            Else
                obj.QC_No = clsCommon.myCstr(fndQcNo.Value)
            End If
            fndQcNo.Value = obj.QC_No
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
            obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dtpGateEntryDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.QC_In_Date_Time = clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Location_Desc = clsCommon.myCstr(lblLocationName.Text)
            If chkBulkMilkProc.IsChecked Then
                obj.Doc_Type = "Tanker"
                obj.Vendor_Code = clsCommon.myCstr(fndVendor.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorName.Text)
            Else
                obj.Doc_Type = "Sku_Receipt"
                obj.Vendor_Code = clsCommon.myCstr(fndVendor.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorName.Text)
            End If
            obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
            obj.Challan_No = clsCommon.myCstr(txtChallanNo.Text)
            obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDate.Value, "dd/MMM/yyyy")
            If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
                obj.Weighment_Date = clsCommon.myCDate(dtpWeighmentDate.Value, "dd/MMM/yyyy")
            End If
            obj.Remarks = clsCommon.myCstr(gvParam.Rows(0).Cells("colRemarks").Value)
            obj.Item_Code = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value)
            obj.Item_Desc = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemDesc).Value)
            obj.UOM = clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)
            obj.Qty_In_Kg = clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value)
            obj.fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
            obj.snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)
            obj.snf_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value)
            obj.fat_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value)
            obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
            obj.Receipt_Control_FAT = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
            obj.Receipt_Control_SNF = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)

            obj.DeductionAmount = clsCommon.myCdbl(TxtDeductionAmount.Value)
            ''-----------------------------
            If Not isPost Then
                obj.isPosted = 0
            End If


            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If

            Dim i As Integer = 0
            Dim objParam As New clsMilkQcParam
            obj.arrQcParam = New List(Of clsMilkQcParam)
            For i = 0 To gvParam.Columns.Count - 1
                If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
                Else
                    objParam = New clsMilkQcParam
                    objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                    objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                    objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                    objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                    objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                    If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                        objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value))
                    End If
                    obj.arrQcParam.Add(objParam)
                End If
            Next
            If clsMilkQualityCheck.saveData(obj, trans) Then
                If (chkMccProc.IsChecked) Then
                    If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then
                        Dim arrMSeal As List(Of clsMilkQCManualSealDetail) = New List(Of clsMilkQCManualSealDetail)
                        Dim objMSeal As clsMilkQCManualSealDetail = Nothing
                        For i = 0 To gvManualSeal.Rows.Count - 1
                            objMSeal = New clsMilkQCManualSealDetail()
                            objMSeal.Chalan_No = clsCommon.myCstr(txtChallanNo.Text)
                            objMSeal.Seal_No = clsCommon.myCstr(gvManualSeal.Rows(i).Cells(colSealName).Value)
                            objMSeal.Status = clsCommon.myCstr(gvManualSeal.Rows(i).Cells(colSealStatus).Value)
                            arrMSeal.Add(objMSeal)
                        Next
                        clsMilkQCManualSealDetail.SaveData(arrMSeal, trans)
                    End If
                    If gvPaperSeal IsNot Nothing AndAlso gvPaperSeal.Rows.Count > 0 Then
                        Dim arrPSeal As List(Of clsMilkQCPaperSealDetail) = New List(Of clsMilkQCPaperSealDetail)
                        Dim objPSeal As clsMilkQCPaperSealDetail = Nothing
                        For i = 0 To gvPaperSeal.Rows.Count - 1
                            objPSeal = New clsMilkQCPaperSealDetail()
                            objPSeal.Chalan_No = clsCommon.myCstr(txtChallanNo.Text)
                            objPSeal.Seal_No = clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealName).Value)
                            objPSeal.Status = clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealStatus).Value)
                            arrPSeal.Add(objPSeal)
                        Next
                        clsMilkQCPaperSealDetail.SaveData(arrPSeal, trans)
                    End If


                End If

                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                btnSave.Text = "Update"
                fndQcNo.MyReadOnly = True
                'btnDelete.Enabled = True
                btnPost.Enabled = True
                btnPrint.Enabled = True
                btnSendForApproval.Enabled = False
                loadData(obj.QC_No, obj.Doc_Type, NavigatorType.Current)
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            btnSendForApproval.Enabled = False
            fndQcNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub
    Public Sub GetECOPro()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "[None]"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Eco Pro 1"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Eco Pro 2"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Eco Pro 3"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "Eco Pro 4"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "Eco Pro 5"
        dt.Rows.Add(dr)


        cboECOPro.DataSource = dt
        cboECOPro.ValueMember = "Code"
        cboECOPro.DisplayMember = "Name"


    End Sub

    Sub loadData(ByVal str As String, ByVal docType As String, ByVal navtype As NavigatorType)
        If clsCommon.myLen(docType) > 0 Then
            obj = clsMilkQualityCheck.getData(str, docType, navtype)
        Else
            obj = clsMilkQualityCheck.getData(str, navtype)
        End If

        If obj IsNot Nothing Then
            If clsCommon.CompairString(obj.Doc_Type, "Tanker") = CompairStringResult.Equal Then
                chkBulkMilkProc.IsChecked = True
            Else
                chkMccProc.IsChecked = True
            End If
            'reset(False, False)
            fndLocation.Enabled = False
            fndQcNo.Value = obj.QC_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpQCInDateTime.Value = obj.QC_In_Date_Time
            dtpQCOutDateTime.Value = obj.QC_Out_Date_Time
            dtpGateEntryDateTime.Value = obj.Gate_Entry_Date_And_Time
            fndTankerNo.Value = obj.Tanker_No
            txtChallanNo.Text = obj.Challan_No
            dtpChallanDate.Value = obj.Challan_Date
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Milk_weighment_detail where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = obj.location_Code
            lblLocationName.Text = obj.Location_Desc

            If clsCommon.CompairString(obj.Doc_Type, "Sku_Receipt") = CompairStringResult.Equal Then
                fndVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Dispatched_From_Mcc_Desc
                Dim arrPSeal As List(Of clsMilkQCPaperSealDetail) = clsMilkQCPaperSealDetail.getData(txtChallanNo.Text)
                Dim arrMSeal As List(Of clsMilkQCManualSealDetail) = clsMilkQCManualSealDetail.getData(txtChallanNo.Text)
                If arrPSeal IsNot Nothing AndAlso arrPSeal.Count > 0 Then
                    loadBlankGVPaperSeal()
                    For i As Integer = 0 To arrPSeal.Count - 1
                        gvPaperSeal.Rows.AddNew()
                        gvPaperSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                        gvPaperSeal.Rows(i).Cells(colSealName).Value = arrPSeal.Item(i).Seal_No
                        gvPaperSeal.Rows(i).Cells(colSealStatus).Value = arrPSeal.Item(i).Status
                    Next
                End If
                If arrMSeal IsNot Nothing AndAlso arrMSeal.Count > 0 Then
                    loadBlankGVManualSeal()
                    For i As Integer = 0 To arrMSeal.Count - 1
                        gvManualSeal.Rows.AddNew()
                        gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                        gvManualSeal.Rows(i).Cells(colSealName).Value = arrMSeal.Item(i).Seal_No
                        gvManualSeal.Rows(i).Cells(colSealStatus).Value = arrMSeal.Item(i).Status
                    Next
                End If
            Else
                fndVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Desc
            End If
            gvItem.Rows(0).Cells(colSlNo).Value = "1"
            gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
            gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
            gvItem.Rows(0).Cells(colUOM).Value = obj.UOM
            gvItem.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
            gvItem.Rows(0).Cells(colFat).Value = obj.fat_per
            gvItem.Rows(0).Cells(colSNF).Value = obj.snf_Per
            gvItem.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
            gvItem.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
            txtDipValue.Text = obj.Dip_Value

            TxtDeductionAmount.Value = obj.DeductionAmount
            ''-----------------------------
            If clsCommon.myCdbl(txtDipValue.Text) <= 0 Then
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_Milk_quality_check where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            End If

            If obj.arrQcParam IsNot Nothing Then
                loadBlankParameterGrid()
                For i As Integer = 0 To obj.arrQcParam.Count - 1
                    Try
                        If obj.isPosted = 0 AndAlso (clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal) Then
                            If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value), 2)
                            Else
                                gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(obj.arrQcParam(i).Param_Field_Value, 2)
                            End If
                        Else
                            If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value)
                            Else
                                gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = obj.arrQcParam(i).Param_Field_Value
                            End If
                        End If

                    Catch exx As Exception
                    End Try

                Next
            End If
            gvParam.Rows(0).Cells("colRemarks").Value = obj.Remarks
            If chkBulkMilkProc.IsChecked Then
                If obj.is_Param_accepted = 0 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC rejected"
                    btnSendForApproval.Enabled = True
                ElseIf obj.is_Param_accepted = 1 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted"
                    btnSendForApproval.Enabled = False
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                    btnSendForApproval.Enabled = False
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 0 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                    btnSendForApproval.Enabled = False
                    ''-------------------------------
                Else
                    lblQcAcceptedOrRejected.Text = ""
                    btnSendForApproval.Enabled = False
                End If

            End If

            If chkMccProc.IsChecked Then
                GrpControlSample.Visible = True
                txtDispControlSampleFAT.Text = clsMilkQualityCheck.ControlSampleFAT(obj.Gate_Entry_No)
                txtDispControlSampleSNF.Text = clsMilkQualityCheck.ControlSampleSNF(obj.Gate_Entry_No)
                txtRcptControlSampleFAT.Text = obj.Receipt_Control_FAT
                txtRcptControlSampleSNF.Text = obj.Receipt_Control_SNF
                If clsMilkQualityCheck.isControlSample(obj.Gate_Entry_No) Then
                    txtRcptControlSampleFAT.Enabled = True
                    txtRcptControlSampleSNF.Enabled = True
                Else
                    txtRcptControlSampleFAT.Enabled = False
                    txtRcptControlSampleSNF.Enabled = False
                End If
            Else
                GrpControlSample.Visible = False
                txtDispControlSampleFAT.Text = ""
                txtDispControlSampleSNF.Text = ""
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
            End If


            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If

            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            If clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") = CompairStringResult.Equal And clsCommon.CompairString(lblPending.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then  'And obj.DeductionAmount > 0
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            ElseIf clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") = CompairStringResult.Equal Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = False
                TxtDeductionAmount.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            Else
                TxtDeductionAmount.Enabled = False
            End If
            If isSentForApproval() Then
                lblQcAcceptedOrRejected.Text = "Sent For Special Approval"
                btnSendForApproval.Enabled = False
                btnPost.Enabled = False
            End If
            '-----------------------------------------------------
        End If
    End Sub
    Function isSentForApproval() As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_TRANSACTION_APPROVAL where Document_No='" & fndQcNo.Value & "' and Program_Code='" & clsUserMgtCode.FrmJobMilkQualityCheck & "' and Approve=0 and approval_type='Special Approval'")) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Dim trans As SqlTransaction = Nothing
        Try
            If clsCommon.myLen(fndQcNo.Value) > 0 Then
                If deleteConfirm() Then
                    trans = clsDBFuncationality.GetTransactin()
                    arr.Add(fndQcNo.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.FrmJobMilkQualityCheck, trans) Then
                        If clsMilkQualityCheck.deleteData(fndQcNo.Value, trans) Then
                            clsMilkQCPaperSealDetail.DeleteData(txtChallanNo.Text, trans)
                            clsMilkQCManualSealDetail.DeleteData(txtChallanNo.Text, trans)
                            trans.Commit()
                            reset(False, True)
                            clsCommon.MyMessageBoxShow("Deleted Successfully")
                        Else
                            clsCommon.MyMessageBoxShow("Could Not Deleted. Try Again")
                            trans.Rollback()
                        End If
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow("Please select a QC No To delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub
    Function isParamOk(ByVal strValueList As String, ByVal value As String) As Boolean
        Dim strValue() As String = strValueList.Split(",")
        If strValue IsNot Nothing AndAlso strValue.Count > 0 Then
            For i As Integer = 0 To strValue.Count - 1
                If clsCommon.CompairString(strValue(i), value) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    Function chkParameterRange() As String
        Dim i As Integer = 0
        Dim rvalue As String = String.Empty
        Dim q As String = String.Empty
        Dim dt As DataTable = Nothing
        For i = 0 To gvParam.Columns.Count - 1
            If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
            Else
                'q = "SELECT top 1 *,case when ISNULL( Lower_range,0)<>0 and ISNULL( Upper_range ,0)<>0 then 1 when ISNULL(Status,'')<>'' then 2 when ISNULL(Value,'')<>'' then 3  else 4 end  as Value_Type,   FROM TSPL_PARAMETER_RANGE_MASTER   where code='" & clsCommon.myCstr(gvParam.Columns(i).Name) & "' and loc_code='" & clsCommon.myCstr(fndLocation.ValidateChildren) & "' and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & fndVendor.Value & "' ") & "' and isReject=1 and convert(date,effective_date,103)<=" & clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy") & "'"
                q = "select TSPL_PARAMETER_RANGE_MASTER.*, case when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='R' then 1 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='B' then 2 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='A' then 3 else 0 end as Value_Type  from TSPL_PARAMETER_RANGE_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.IsReject=1 and CONVERT (date, Effective_Date,103) <= '" & clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy") & "'  and TSPL_PARAMETER_RANGE_MASTER.Code='" & clsCommon.myCstr(gvParam.Columns(i).Name) & "' and TSPL_PARAMETER_RANGE_MASTER.vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & fndVendor.Value & "' ") & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & fndLocation.Value & "' order by Effective_Date desc "

                dt = clsDBFuncationality.GetDataTable(q)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    If dt.Rows(0)("Value_Type") = 1 Then
                        'If dt.Rows(0)("Qc_status_Num") = 1 Then
                        If Not (clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) >= dt.Rows(0)("Lower_range") And clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) <= dt.Rows(0)("Upper_range")) Then
                        Else
                            rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Must Not Be  Upper Range: " & dt.Rows(0)("Upper_range") & ", Lower Range: " & dt.Rows(0)("Lower_range") & ", QC Value: " & clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value)
                        End If
                        'Else
                        '    If Not (clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) >= dt.Rows(0)("Lower_range") And clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) <= dt.Rows(0)("Upper_range")) Then
                        '    Else
                        '        rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Must Not Be Upper Range: " & dt.Rows(0)("Upper_range") & ", Lower Range: " & dt.Rows(0)("Lower_range") & ", QC Value: " & clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value)
                        '    End If
                        'End If

                    ElseIf dt.Rows(0)("Value_Type") = 2 Then
                        'If dt.Rows(0)("Qc_status_Num") = 1 Then
                        If Not (clsCommon.CompairString(dt.Rows(0)("Status"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) = CompairStringResult.Equal) Then
                        Else
                            rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Status Should Not Be , Parameter Status: " & dt.Rows(0)("Status") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        End If
                        'Else
                        '    If Not (clsCommon.CompairString(dt.Rows(0)("Status"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) = CompairStringResult.Equal) Then
                        '    Else
                        '        rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Status Should Not Be : " & dt.Rows(0)("Status") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        '    End If
                        ' End If

                    ElseIf dt.Rows(0)("Value_Type") = 3 Then
                        ' If dt.Rows(0)("Qc_status_Num") = 1 Then
                        If Not isParamOk(dt.Rows(0)("Condition_Value"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) Then
                        Else
                            rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value Should Not Be In : ( " & dt.Rows(0)("Condition_Value") & " ), But QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        End If
                        'Else
                        '    If Not isParamOk(dt.Rows(0)("Value1"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) Then
                        '    Else
                        '        rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value Should Not Be In : ( " & dt.Rows(0)("Value1") & " ), But QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        '    End If

                        'End If
                        'If clsCommon.CompairString(dt.Rows(0)("Value1"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) = CompairStringResult.Equal Then
                        'Else
                        '    rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value: " & dt.Rows(0)("Value1") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        'End If
                        'ElseIf dt.Rows(0)("Value_Type") = 4 Then
                        '    If clsCommon.CompairString(dt.Rows(0)("Value2"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) = CompairStringResult.Equal Then
                        '    Else
                        '        rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value: " & dt.Rows(0)("Value2") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        '    End If
                    End If
                    '' For Checking Parameter QC range is defined in Parameter Range Master For QC or not
                    'Else
                    '    Return "-1"
                End If
            End If
        Next
        If clsCommon.myLen(rvalue) > 0 Then
            Return rvalue
        Else
            Return "1"
        End If
    End Function
    Sub PostData()
        Dim str As String = String.Empty
        Try
            Dim strDocType As String = String.Empty
            If chkBulkMilkProc.IsChecked Then
                strDocType = "Tanker"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "Sku_Receipt"
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not chkMccProc.IsChecked Then
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    If clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") <> CompairStringResult.Equal Then

                        str = chkParameterRange()
                        If clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(str, "-1") = CompairStringResult.Equal Then
                            Throw New Exception("Some Of the Parameter Range Not Found in Master")
                        Else
                            If clsCommon.MyMessageBoxShow("Following Parameters Rejects The Milk, " & Environment.NewLine & str & Environment.NewLine & "Want To Continue Posting ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                                gvParam.Rows(0).Cells("colRemarks").Value = gvParam.Rows(0).Cells("colRemarks").Value & Environment.NewLine & "Rejection Remarks: " & str
                            Else
                                Exit Sub
                            End If
                        End If
                        ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    Else
                        str = "2"
                    End If

                Else
                    str = "1"
                End If
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsMilkQualityCheck.postData(fndQcNo.Value, strDocType, Me.Form_ID, Nothing)) Then
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    If clsCommon.CompairString(str, "2") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_Milk_quality_check set is_Param_Accepted=2 where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    Else
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_Milk_quality_check set is_Param_Accepted=" & IIf(clsCommon.CompairString(str, "1") = CompairStringResult.Equal, 1, 0) & " where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    End If
                    ''-----------------------------------------
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
                loadData(fndQcNo.Value, strDocType, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub funPrint(ByVal strDocNo As String)

        'Try

        '    Dim Qry As String = " select Weighment_No ,tspl_Milk_quality_check.remarks,case when ISNULL(weighment_no,'')='' then 'Not Done' else 'Done' end as Weighment_Status,convert(varchar,Weighment_Date,103)  as Weighment_Date,case when tspl_Milk_quality_check.Doc_Type ='MccProc'  then TSPL_MCC_MASTER .MCC_NAME else  case when tspl_Milk_quality_check.Doc_Type ='BulkProc' then TSPL_VENDOR_MASTER.Vendor_Name "
        '    Qry += "end end as MCC_Vendor_Name ,"

        '    Qry += " case when tspl_Milk_quality_check.Doc_Type ='MccProc'  then"

        '    Qry += " TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end  + case when LEN(TSPL_CITY_MASTER_for_MCC.City_Name )>0 then ', '+TSPL_CITY_MASTER_for_MCC.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_MCC.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_MCC.STATE_NAME  else '' end +   "
        '    Qry += " case when LEN(TSPL_MCC_MASTER.Pin_code   )>0 then ', '+TSPL_MCC_MASTER.Pin_code  else ' ' end + case when len(TSPL_MCC_MASTER.Email   )>0 then ', ' +TSPL_MCC_MASTER.Email   else '' end  "
        '    Qry += "   else case when tspl_Milk_quality_check.Doc_Type ='BulkProc'  then"

        '    Qry += " TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when len(TSPL_VENDOR_MASTER.add3)>0 then ', '+TSPL_VENDOR_MASTER.add3 else '' end  + case when LEN(TSPL_CITY_MASTER_for_Vendor.City_Name  )>0 then ', '+TSPL_CITY_MASTER_for_Vendor.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_Vendor.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_Vendor.STATE_NAME  else '' end +   "
        '    Qry += " case when LEN(TSPL_VENDOR_MASTER.Pin_code   )>0 then ', '+TSPL_VENDOR_MASTER.Pin_code  else ' ' end + case when len(TSPL_VENDOR_MASTER.Email   )>0 then ', ' +TSPL_VENDOR_MASTER.Email   else '' end end  end"

        '    Qry += " as MCC_Vendor_Add,TSPL_ITEM_MASTER .Item_Code ,TSPL_ITEM_MASTER .Item_Desc,isPosted,tspl_Milk_quality_check.Dip_Value ,is_Param_Accepted,case when isPosted ='0' and is_Param_Accepted ='0' then 'Pending' else  case  when isPosted ='1' and is_Param_Accepted ='0' then 'Rejected' else case when isPosted ='0' and is_Param_Accepted = is_Param_Accepted then 'Pending' else  case when isPosted ='1' and is_Param_Accepted ='1' then 'Accepted'  else case when isPosted ='1' and is_Param_Accepted ='2' then 'Accepted with Special Approval' end end end end end  as ParameterAccepted , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME  else '' end +   "
        '    Qry += " case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  "

        '    Qry += " as Comp_address ,TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,"

        '    Qry += " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Location.STATE_NAME  )>0 then TSPL_STATE_MASTER_For_Location.STATE_NAME  else '' end +   "
        '    Qry += " case when LEN(TSPL_LOCATION_MASTER.Tin_No  )>0 then ', '+TSPL_LOCATION_MASTER.Tin_No else ' ' end  "

        '    Qry += "  as Loc_address,tspl_Milk_quality_check.QC_No ,tspl_Milk_quality_check.QC_In_Date_Time ,tspl_Milk_quality_check.QC_Out_Date_Time ,tspl_Milk_quality_check.Tanker_No ,tspl_Milk_quality_check.Gate_Entry_No ,convert(varchar,tspl_Milk_quality_check.Gate_Entry_Date_And_Time,103) as Gate_Entry_Date_And_Time ,tspl_Milk_quality_check.Challan_No ,tspl_Milk_quality_check.Challan_Date ,TSPL_Milk_QC_Parameter_Detail.Param_Field_Desc ,TSPL_Milk_QC_Parameter_Detail.Param_Field_Value,TSPL_Milk_QC_Parameter_Detail.Param_type "
        '    Qry += "   from tspl_Milk_quality_check"
        '    Qry += " left outer join TSPL_Milk_QC_Parameter_Detail on TSPL_Milk_QC_Parameter_Detail.QC_No =tspl_Milk_quality_check.QC_No "
        '    Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_Milk_quality_check.comp_code "
        '    Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code "
        '    Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State "
        '    Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =tspl_Milk_quality_check.location_Code "
        '    Qry += " left outer join TSPL_CITY_MASTER  as TSPL_CITY_MASTER_For_Location on TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.location_Code "
        '    Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Location on TSPL_STATE_MASTER_For_Location.STATE_CODE =TSPL_LOCATION_MASTER.State  "
        '    Qry += "  left outer join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =tspl_Milk_quality_check.Item_Code"
        '    Qry += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =tspl_Milk_quality_check.Dispatched_From_Mcc_Code "
        '    Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_MCC on TSPL_CITY_MASTER_for_MCC .City_Code =TSPL_MCC_MASTER.City_code "
        '    Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_MCC on TSPL_STATE_MASTER_for_MCC .STATE_CODE =TSPL_MCC_MASTER.State_Code "
        '    Qry += " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code  =tspl_Milk_quality_check .Vendor_Code"
        '    Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_Vendor on TSPL_CITY_MASTER_for_Vendor .City_Code =TSPL_VENDOR_MASTER.City_code "
        '    Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_Vendor on TSPL_STATE_MASTER_for_Vendor .STATE_CODE =TSPL_VENDOR_MASTER.State_Code"
        '    Qry += " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_Milk_QC_Parameter_Detail.Param_Field_Code"
        '    Qry += "   where IsForPrintOnQC =1 and tspl_Milk_quality_check.QC_No ='" + strDocNo + "'"
        '    'Qry += " and TSPL_Milk_QC_Parameter_Detail.Param_Field_Desc <>'AUTO SNF' and TSPL_Milk_QC_Parameter_Detail.Param_Field_Desc <>'Auto FAT' and TSPL_Milk_QC_Parameter_Detail.Param_Field_Desc <>'AUTO CLR'"

        '    Qry = " select xx.*, case when xx.Param_Type='NA'  then 1 when  xx.Param_Type='FAT' then 2 when xx.Param_Type='SNF' then 3 when xx.Param_Type='CLR' then 4 when xx.Param_Type='OTHERS' then 5 else 6 end as Ordering from ( " & Qry & " ) xx  order by ordering"

        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        '    If dt.Rows.Count > 0 Then

        '        frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptQualityCheck", "Quality Check")
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndQcNo.Value) <= 0 Then
            myMessages.blankValue("Doc not found to Print")
        Else
            funPrint(fndQcNo.Value)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub fndQcNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndQcNo._MYNavigator
        'If chkBothDoc.Checked Then
        '    loadData(fndQcNo.Value, "", NavType)
        'Else
        loadData(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavType)
        'End If
    End Sub


    Private Sub fndQcNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndQcNo._MYValidating
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " And tspl_Milk_quality_check.location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If chkBothDoc.Checked Then
            fndQcNo.Value = clsMilkQualityCheck.getFinder(" 1=1 " & whrCls, fndQcNo.Value, isButtonClicked)
        Else
            fndQcNo.Value = clsMilkQualityCheck.getFinder(" Doc_type='" & IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker") & "' " & whrCls, fndQcNo.Value, isButtonClicked)
        End If

        If clsCommon.myLen(fndQcNo.Value) > 0 Then

            If chkBothDoc.Checked Then
                loadData(fndQcNo.Value, "", NavigatorType.Current)
            Else
                loadData(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt"), NavigatorType.Current)
            End If
        End If
    End Sub

    Private Sub gvParam_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gvParam.CellValidated
        If e.Column Is gvParam.Columns(FATColName) Then
            Dim fracValue1 As Double = 0
            If clsCommon.myLen(FATColName) > 0 Then
                fracValue1 = clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value)
                fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                If CInt(fracValue1) Mod 5 <> 0 Then
                    clsCommon.MyMessageBoxShow(" FAT value in Grid, must have its decimal part multiple of 5")
                    gvParam.Rows(0).Cells(FATColName).Value = 0

                    gvParam.CurrentRow = gvParam.Rows(0)
                    gvParam.CurrentColumn = gvParam.Columns(FATColName)
                    'gvParam.Rows(0).Cells(FATColName).BeginEdit()
                End If
            End If
            gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(FATColName).Value)
        End If
        If e.Column Is gvParam.Columns(SNFColName) Then
            gvParam.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(SNFColName).Value)
        End If
    End Sub

    Private Sub gvParam_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvParam.CellValueChanged
        If Not clsERPFuncationality.isLocationMcc(fndLocation.Value) Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If clsCommon.CompairString(gvParam.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                    calculateSNF()
                    gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(FATColName).Value, 2))
                    'gvParam.Rows(0).Cells(SNFColName).Value= Math.Truncate ( clsCommon.myCdbl  gvParam.Rows(0).Cells(SNFColName).Value)
                    gvParam.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(SNFColName).Value, 2))
                End If
                isCellValueChangedOpen = False
                'gvParam.AutoSizeRows = True
            End If
        End If

        'If e.Column Is gvParam.Columns(FATColName) Then
        '    gvParam.CurrentRow.Cells(FATColName).Value = Math.Floor(gvParam.CurrentRow.Cells(FATColName).Value)
        'End If
        'If e.Column Is gvParam.Columns(SNFColName) Then
        '    gvParam.CurrentRow.Cells(SNFColName).Value = Math.Floor(gvParam.CurrentRow.Cells(SNFColName).Value)
        'End If
    End Sub


    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkQualityCheck.ReverseAndUnpost(fndQcNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
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
                Dim obj1 As New clsGridLayout()
                obj1.ReportID = MyBase.Form_ID & "gvParam"
                obj1.UserID = objCommonVar.CurrentUserCode
                obj1.GridLayout = New MemoryStream()
                gvParam.SaveLayout(obj1.GridLayout)
                obj1.GridColumns = gvParam.ColumnCount
                obj1.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj1.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                ''stuti regarding memory leakage
                obj1.GridLayout.Close()
                obj1.GridLayout.Dispose()
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuEmailSmsSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEmailSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmJobMilkQualityCheck
        frm.ShowDialog()
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
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvParam", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvParam.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvParam.Columns.Count - 1 Step ii + 1
                        gvParam.Columns(ii).IsVisible = False
                        gvParam.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvParam.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    'Private Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmJobMilkQualityCheck)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.QcNo, fndQcNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.inDateTime, clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.outDateTime, clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.QcNo, fndQcNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.inDateTime, clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.outDateTime, clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))

    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationCode, fndLocation.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationName, lblLocationName.Text)

    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, fndVendor.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)

    '        Dim strRptPath As String = ""
    '        ''''' It will be done after creating report
    '        'If obj.atchmnt = "Y" Then
    '        '    attachQry = GetAttachQry()
    '        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptShipment", "Shippment Detail")
    '        '    End If
    '        'End If

    '        Dim strPath As String = ""
    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '            End If

    '            strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strPath)
    '        Next

    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub
    Private Sub btnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click

        Try
            If clsCommon.myLen(fndQcNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select QC No. First", Me.Text)
                fndQcNo.Focus()
                Return
            End If

            'If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective QC No. " + fndQcNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            '    Return
            'End If
            'loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)

            'Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            'Dim lstUsers As New List(Of String)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lstUsers.Add(dr("User_Code").ToString())
            '    Next
            'End If

            'If lstUsers.Count = 0 Then
            '    Throw New Exception("No Receiptent Found")
            'End If
            'SendEmail(lstUsers, True)
            Dim objApprov As New ClsTransactionApproval
            objApprov.Document_No = fndQcNo.Value
            objApprov.Doc_Date = clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            objApprov.Approval_Type = "Special Approval"
            objApprov.Approve = 0
            objApprov.Program_Code = Me.Form_ID
            Dim qryp As String = "select Program_Name a   from TSPL_PROGRAM_MASTER where  Program_Code ='" & Me.Form_ID & "'"
            objApprov.Screen_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryp))
            ClsTransactionApproval.SaveData(objApprov, True)
            btnSendForApproval.Enabled = False
            clsCommon.MyMessageBoxShow("Document Sent For special Approval Successfully", Me.Text)
            loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub Timer1_Start()
        Try
            Dim obj As clsPortSetting = clsPortSetting.getData(0, CboMachine.Text) ' 0 for Milk analyzer and 1 for weighing machine,
            If obj Is Nothing Then
                Throw New Exception("No setting found for device")
            End If
            objSr.BaudRate = obj.baud_rate
            objSr.Parity = obj.parity
            objSr.PortName = cboComPort.Text
            objSr.StopBits = obj.stop_bits
            objSr.DataBits = obj.data_bits
            objSr.DataForm = clsPortSetting.getMachineDataFormat(CboMachine.Text)
            objSr.isEkoProMachine = True
            objSr.isWeighingMachine = False
            objSr._LblFat = LblSnf
            objSr._LblSNF = LblFAT
            objSr._LblWeight = Nothing
            objSr.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine.Text, 0)
            objSr.OpenPort()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub BtnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStart.Click
        Try
            If clsCommon.CompairString(BtnStart.Text, "Start") = CompairStringResult.Equal Then
                FrmMccDispatch.isPortOpened = True
                Timer1_Start()
                cboComPort.Enabled = False
                CboMachine.Enabled = False
                cboECOPro.Enabled = False
                BtnStart.Text = "Stop"

            Else
                FrmMccDispatch.isPortOpened = False
                cboComPort.Enabled = True
                CboMachine.Enabled = True
                cboECOPro.Enabled = True
                BtnStart.Text = "Start"
                LblFAT.Text = "00.00"
                LblSnf.Text = "00.00"
                objSr.ClosePort()
            End If
        Catch ex As Exception
            cboComPort.Enabled = True
            CboMachine.Enabled = True
            cboECOPro.Enabled = True
            BtnStart.Text = "Start"
            LblFAT.Text = "00.00"
            LblSnf.Text = "00.00"
            objSr.ClosePort()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    Try
    '        If BtnStart.Text = "Stop" AndAlso clsCommon.myLen(cboECOPro.SelectedValue) > 0 AndAlso (isForcellyStarted) Then
    '            ' Timer1.Stop()
    '            BtnRead_Click(sender, e)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub BtnRead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRead.Click
    '    Try
    '        objEco.getDataMachineWise(cboComPort.Text, IIf(clsCommon.CompairString(CboMachine.Text, "kanha") = CompairStringResult.Equal, "K", "E"))
    '        LblSnf.Text = objEco.FAT.ToString()
    '        LblFAT.Text = objEco.SNF.ToString()
    '        If clsCommon.myCdbl(LblFAT.Text) > 0 Or clsCommon.myCdbl(LblSnf.Text) > 0 Then
    '            Timer1.Stop()
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvParam_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvParam.Validated

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating

        Dim whrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and  TSPL_MILK_GATE_ENTRY_DETAILS.location_code in ( " & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        whrcls = whrcls & " and TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_no not in( select Gate_Entry_No  from tspl_Milk_quality_check) "
        Dim gtNo As String = fndTankerNo.Value
        Dim dt As DataRow
        dt = clsMilkQualityCheck.getGateEntryFinder(gtNo, "TSPL_MILK_GATE_ENTRY_DETAILS.isPosted='1' and TSPL_MILK_GATE_ENTRY_DETAILS.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "Tanker", "Sku_Receipt") & "'  " & whrcls)
        If dt IsNot Nothing Then
            gtNo = dt("Gate Entry No")
        End If
        If clsCommon.myLen(gtNo) > 0 Then
            LoadGateEntryData(gtNo)
        Else
            reset(False, False)
        End If
    End Sub
    Sub loadBlankGVPaperSeal()
        gvPaperSeal.Rows.Clear()
        gvPaperSeal.Columns.Clear()
        gvPaperSeal.DataSource = Nothing

        gvPaperSeal.Columns.Add(colSlNo, "SL. No.")
        gvPaperSeal.Columns(colSlNo).ReadOnly = True
        gvPaperSeal.Columns(colSlNo).Width = 60

        gvPaperSeal.Columns.Add(colSealName, "Seal Desc")
        gvPaperSeal.Columns(colSealName).ReadOnly = True
        gvPaperSeal.Columns(colSealName).Width = 180

        Dim repoComboColumn As GridViewComboBoxColumn
        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colSealStatus
        repoComboColumn.Width = 120
        repoComboColumn.HeaderText = "Status"
        repoComboColumn.DataSource = LoadSealStatus()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gvPaperSeal.MasterTemplate.Columns.Add(repoComboColumn)
        'For i As Integer = 0 To 9
        '    gvPaperSeal.Rows.AddNew()
        '    gvPaperSeal.Rows(i).Cells(colSlNo).Value = (i + 1)
        '    gvPaperSeal.Rows(i).Cells(colSealName).Value = ""
        '    gvPaperSeal.Rows(i).Cells(colSealStatus).Value = ""
        'Next
        gvPaperSeal.AllowAddNewRow = False
        gvPaperSeal.AllowDeleteRow = False
        gvPaperSeal.AllowRowReorder = False
        gvPaperSeal.ShowGroupPanel = False
        gvPaperSeal.EnableFiltering = False
        gvPaperSeal.EnableSorting = False
        gvPaperSeal.EnableGrouping = False
    End Sub
    Sub loadBlankGVManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()
        gvManualSeal.DataSource = Nothing

        gvManualSeal.Columns.Add(colSLNo, "SL. No.")
        gvManualSeal.Columns(colSLNo).ReadOnly = True
        gvManualSeal.Columns(colSLNo).Width = 60

        gvManualSeal.Columns.Add(colSealName, "Seal Desc")
        gvManualSeal.Columns(colSealName).ReadOnly = True
        gvManualSeal.Columns(colSealName).Width = 180

        Dim repoComboColumn As GridViewComboBoxColumn
        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colSealStatus
        repoComboColumn.Width = 120
        repoComboColumn.HeaderText = "Status"
        repoComboColumn.DataSource = LoadSealStatus()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gvManualSeal.MasterTemplate.Columns.Add(repoComboColumn)
        'For i As Integer = 0 To 9
        '    gvManualSeal.Rows.AddNew()
        '    gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
        '    gvManualSeal.Rows(i).Cells(colSealName).Value = ""
        '    gvManualSeal.Rows(i).Cells(colSealStatus).Value = ""
        'Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.EnableFiltering = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.EnableGrouping = False
    End Sub

    Private Sub dtpQCOutDateTime_ValueChanged(sender As Object, e As EventArgs) Handles dtpQCOutDateTime.ValueChanged

    End Sub

    Private Sub gvParam_Click(sender As Object, e As EventArgs) Handles gvParam.Click

    End Sub

   
End Class
