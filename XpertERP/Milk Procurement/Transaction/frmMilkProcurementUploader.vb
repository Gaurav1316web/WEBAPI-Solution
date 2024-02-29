Imports common
Imports System.IO


Public Class frmMilkProcurementUploader
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const colShiftDate As String = "colShiftDate"
    Const colShift As String = "colShift"
    Const colDockCollectionMilkType As String = "colDockCollectionMilkType"
    Const colUploaderCode As String = "colUploaderCode"
    Const colVLCCode As String = "colVLCCode"
    Const colVLCName As String = "colVLCName"
    Const colBulkRouteCode As String = "colBulkRouteCode"
    Const colNoOfCan As String = "colNoOfCan"
    Const colMilkWeight As String = "colMilkWeight"
    Const colFATPer As String = "colFATPer"
    Const colSNFPer As String = "colSNFPer"
    Const colRejectRejectType As String = "colRejectRejectType"
    Const colRejectDefaulter As String = "colRejectDefaulter"

    Const colManualWeight As String = "colManualWeight"
    Const colManualSample As String = "colManualSample"
    Const colEmptySample As String = "colEmptySample"
    Const colPageNo As String = "colPageNo"

    Const ReportID As String = "BatchInvIn"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim MilkWeight_Setting As Decimal = 0
    Dim CreateNewDocumentOnUploader As Boolean = False
    Dim strFolderPath As String = ""
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim settMaxReceiveSNFPer As Decimal = 0
    Dim settMaxFATPerLimit As Decimal = 0 ''UDL/02/11/18-000239 by balwinder on 13/11/2018
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim settLastMilkReceiptQtyTollerance As Decimal = 0

    Dim settAlwaysVSPDefaulter As Boolean = False
    Dim settSelectMilkRejectDefaulterManually As Boolean = False
    Dim TotQty As Decimal = 0
    Dim settMilkProcurementBatchPosting As Boolean = False
    Dim SettShowAllDCS As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        RadMenu1.Visible = MyBase.isExport
        If MyBase.isReverse Then
            RadButton2.Enabled = True
        Else
            RadButton2.Enabled = False
        End If
        If MyBase.isExport = True Then
            RadMenuItem4.Enabled = True
            RadMenuItem5.Enabled = True
        Else
            RadMenuItem4.Enabled = False
            RadMenuItem5.Enabled = False
        End If
        'btnPrint.Visible = MyBase.isPrintFlag
        'btnImport.Visible = MyBase.isExport

        'If btnSave.Visible = True Then
        '    btnImport.Enabled = True
        'Else
        '    btnImport.Enabled = False
        'End If
    End Sub
    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.SetUserMgmt(clsUserMgtCode.MilkShiftUploader)
        SettShowAllDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllDCS, clsFixedParameterCode.ShowAllDCS, Nothing))
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
        chkMilkReject.Visible = (settAlwaysVSPDefaulter AndAlso settSelectMilkRejectDefaulterManually)
        AddNew()
        SetUserMgmtNew()

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtTotalQty.Value = 0
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDesc.Text = ""
        isNewEntry = True
        fndMCCCode.Value = ""
        LblMccName.Text = ""
        txtDockCode.Value = ""
        lblDockName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        chkMilkReject.Checked = False
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub

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


        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Shift Date"
        repoDateBox.Name = colShiftDate
        repoDateBox.ReadOnly = False
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDateBox)


        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Shift"
        repoComboBox.Name = colShift
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoComboBox.DataSource = GetItemType()
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoComboBox)

        repoComboBox = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Milk Type"
        repoComboBox.Name = colDockCollectionMilkType
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        If objCommonVar.DisplayTypeInMilkReceipt Then
            repoComboBox.DataSource = clsMilkReceiptMCC.GetMilkType() ''VIJ/16/10/19-000018 by balwinder on 04/10/2019
        Else
            repoComboBox.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        End If
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoComboBox)

        Dim repoTextBox2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox2.FormatString = ""
        repoTextBox2.HeaderText = "Uploader Code"
        repoTextBox2.Name = colUploaderCode
        repoTextBox2.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox2.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox2.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox2)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Bulk Route"
        repoTextBox.Name = colBulkRouteCode
        'repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "VLC Code"
        repoTextBox.Name = colVLCCode
        'repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "VLC Name"
        repoTextBox.Name = colVLCName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "No Of Cans"
        repoNumBox.Name = colNoOfCan
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Milk Weight"
        repoNumBox.Name = colMilkWeight
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Fat %"
        repoNumBox.Name = colFATPer
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = IIf(settMaxFATPerLimit > 0, settMaxFATPerLimit, 15)
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 1
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = If(isPickCLRInsteadOfSNF, "CLR %", "SNF %")
        repoNumBox.Name = colSNFPer
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = IIf(settMaxReceiveSNFPer > 0, settMaxReceiveSNFPer, IIf(isPickCLRInsteadOfSNF, 50, 15))
        repoNumBox.Maximum = IIf(settMaxSNFPerLimit > 0, settMaxSNFPerLimit, IIf(isPickCLRInsteadOfSNF, 50, 15))
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1)
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)


        repoComboBox = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Reject Type"
        repoComboBox.Name = colRejectRejectType
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Dim dt As DataTable = clsMilkRejectType.GetCombo(True)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = "#COB#"
            dr("Name") = "COB"
            dt.Rows.InsertAt(dr, 1)

            dr = dt.NewRow
            dr("Code") = "#Drain#"
            dr("Name") = "Drain"
            dt.Rows.InsertAt(dr, 1)

            dr = dt.NewRow
            dr("Code") = "#Return#"
            dr("Name") = "Return"
            dt.Rows.InsertAt(dr, 1)

        End If
        repoComboBox.DataSource = dt
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        repoComboBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComboBox)

        repoComboBox = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Reject Defaulter"
        repoComboBox.Name = colRejectDefaulter
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoComboBox.DataSource = GetRejectDefaulter()
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        repoComboBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComboBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Manual Weight"
        repoNumBox.Name = colManualWeight
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Manual Sample"
        repoNumBox.Name = colManualSample
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Empty Sample"
        repoNumBox.Name = colEmptySample
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Page No"
        repoNumBox.Name = colPageNo
        repoNumBox.IsVisible = False
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        loadBlankParameterGrid()

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        gv1.AllowDeleteRow = True

        ReStoreGridLayout()
    End Sub

    Function GetRejectDefaulter() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "VSP"
        dr("Name") = "VSP"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Transporter"
        dr("Name") = "Transporter"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Company"
        dr("Name") = "Company"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub loadBlankParameterGrid() ''VIJ/16/10/19-000018 By Balwinder on 14/09/2019
        Dim gridWidth As Integer = 60
        Dim dt As DataTable = clsMilkSampleQCParameterDetail.GetExtraQCParameters()
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            Dim repoComboColumn As GridViewComboBoxColumn
            Dim repoTextColumn As GridViewTextBoxColumn
            Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
            For i As Integer = 0 To dt.Rows.Count() - 1
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
                    repoDecimalColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    repoTextColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoDecimalColumn)
                End If
            Next
        End If
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Function FillYesNoValue() As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    'If e.Column Is gv1.Columns(colVLCCode) Then
                    '    OpenVLCFinder(False)
                    'End If
                    If e.Column Is gv1.Columns(colUploaderCode) Then
                        OpenVLCFinder(False)
                    End If
                    If e.Column Is gv1.Columns(colMilkWeight) Then
                        TotQty = 0
                        For ii As Integer = 0 To gv1.RowCount - 1
                            TotQty += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                        Next
                        txtTotalQty.Value = TotQty
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenVLCFinder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            Throw New Exception("Please provide MCC code ")
        End If


        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],  TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] " + Environment.NewLine +
        " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
        " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  " + Environment.NewLine +
        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine
        Dim whrCls As String = "2=2 "
        If Not SettShowAllDCS Then
            whrCls += " and TSPL_VLC_MASTER_HEAD.MCC  ='" + fndMCCCode.Value + "'"
        End If

        'gv1.CurrentRow.Cells(colVLCCode).Value = clsCommon.ShowSelectForm("MSRNUVLCC", qry, "VLC_Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVLCCode).Value), "VLC_Code", isButtonClick)

        gv1.CurrentRow.Cells(colUploaderCode).Value = clsCommon.ShowSelectForm("MSRNUupC", qry, "Uploader_Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUploaderCode).Value), "Uploader_Code", isButtonClick)

        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUploaderCode).Value) + "' "
        If Not SettShowAllDCS Then
            qry += " and mcc='" + fndMCCCode.Value + "'"
        End If
        gv1.CurrentRow.Cells(colVLCCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        gv1.CurrentRow.Cells(colVLCName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVLCCode).Value) + "'"))
    End Sub

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
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
                If chkMilkReject.Checked Then
                    gv1.Rows(ii).Cells(colShiftDate).Value = txtDate.Value
                    If Not (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value), "#Return#") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value), "#Drain#") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value), "#COB#") = CompairStringResult.Equal) Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colRejectRejectType).Value) <= 0 Then
                            Throw New Exception("Please select Reject Type")
                        End If
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colRejectDefaulter).Value) <= 0 Then
                            Throw New Exception("Please select Defaulter")
                        End If
                        If objCommonVar.DisplayTypeInMilkReceipt Then
                            Dim RejectMasterMilkType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Type from TSPL_MILK_REJECT_TYPE where Code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value) + "'"))
                            If Not clsCommon.CompairString(RejectMasterMilkType, clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value)) = CompairStringResult.Equal Then
                                Throw New Exception("Rejection Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value) + "] is [" + RejectMasterMilkType + "] But Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "]")
                            End If
                        End If
                    End If
                Else
                    If objCommonVar.AddValidationofMilkTypeinsample Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "M") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinMix OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxMix Then
                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinMix) + " - " + clsCommon.myCstr(objCommonVar.FatMaxMix) + "]")
                            ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinMix OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxMix Then

                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinMix) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxMix) + "]")
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "C") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinCow OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxCow Then
                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinCow) + " - " + clsCommon.myCstr(objCommonVar.FatMaxCow) + "]")
                            ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinCow OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxCow Then
                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinCow) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxCow) + "]")
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "B") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinBuff OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxBuff Then
                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinBuff) + " - " + clsCommon.myCstr(objCommonVar.FatMaxBuff) + "]")
                            ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinBuff OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxBuff Then
                                Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinBuff) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxBuff) + "]")
                            End If
                        Else
                            Throw New Exception("Milk Type should be M/B/C")
                        End If
                    End If
                    If settLastMilkReceiptQtyTollerance > 0 Then
                        Dim qty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                        If qty > 0 Then
                            Dim dtLastQty As DataTable = clsDBFuncationality.GetDataTable("select QTY,cast( case when (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) < 0 then 0 else (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) end as decimal(18,2)) as MinQty,cast( (QTY+(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) as decimal(18,2)) as MaxQty from TSPL_MILK_SRN_DETAIL where DOC_CODE in (select top 1  DOC_CODE from tspl_milk_srn_head where DOC_DATE<'" + clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colShiftDate).Value), "dd/MMM/yyyy") + "' and VLC_CODE='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value) + "' order by doc_date desc,SHIFT)")
                            If dtLastQty IsNot Nothing AndAlso dtLastQty.Rows.Count > 0 Then
                                If qty < clsCommon.myCdbl(dtLastQty.Rows(0)("MinQty")) OrElse qty > clsCommon.myCdbl(dtLastQty.Rows(0)("MaxQty")) Then
                                    If clsCommon.MyMessageBoxShow("Row No [" + clsCommon.myCstr(ii + 1) + "] Qty [" + clsCommon.myCstr(qty) + "] Tollerance [" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "] and Valid Qty Range [" + clsCommon.myCstr(dtLastQty.Rows(0)("MinQty")) + "-" + clsCommon.myCstr(dtLastQty.Rows(0)("MaxQty")) + "]" + Environment.NewLine + "Do you want to continue...", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                                        Return False
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
        If Not chkMilkReject.Checked Then
            For i As Integer = 0 To gv1.Columns.Count - 1
                If clsCommon.myLen(gv1.Columns(i).Tag) > 0 Then
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(jj).Cells(colVLCCode).Value) > 0 Then
                            Dim isManadatory As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsMandatory from TSPL_PARAMETER_MASTER where Code='" & gv1.Columns(i).Name & "'"))
                            Dim NatureType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Nature  from TSPL_PARAMETER_MASTER where Code='" & gv1.Columns(i).Name & "'"))

                            If clsCommon.CompairString(NatureType, "R") = CompairStringResult.Equal Then
                                If isManadatory = 1 And clsCommon.myCdbl(gv1.Rows(jj).Cells(i).Value) = 0 Then
                                    Throw New Exception("Row No [" + clsCommon.myCstr(jj + 1) + "] Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            ElseIf clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                                If isManadatory = 1 And clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 Then
                                    Throw New Exception("Row No [" + clsCommon.myCstr(jj + 1) + "] Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            ElseIf clsCommon.CompairString(NatureType, "B") = CompairStringResult.Equal Then
                                If isManadatory = 1 And (clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gv1.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gv1.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                                    Throw New Exception("Row No [" + clsCommon.myCstr(jj + 1) + "] Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            End If
                        End If
                    Next
                End If
            Next
        End If
        'Prevent future date transaction
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
                If clsCommon.myCDate(gv1.Rows(ii).Cells(colShiftDate).Value).Date() > clsCommon.GETSERVERDATE().Date() Then
                    clsCommon.MyMessageBoxShow("Row No [" + clsCommon.myCstr(ii + 1) + "] Cannot allow future date -  " & clsCommon.myCDate(gv1.Rows(ii).Cells(colShiftDate).Value).Date())
                    Return False
                End If
            End If
        Next

        Return True
    End Function

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
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        ElseIf e.Alt And e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                RadButton1.Visible = True
                RadButton2.Visible = True
            End If
        End If
    End Sub

    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        RefeshSNO()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        'If gv1.RowCount <= clsCommon.myCdbl(lblQty.Text) Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        'If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '    e.Cancel = True
        'End If
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
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                If gv1.CurrentRow.Cells(colManualWeight).Value Is Nothing Then
                    gv1.CurrentRow.Cells(colManualWeight).Value = 1
                End If
                If gv1.CurrentRow.Cells(colManualSample).Value Is Nothing Then
                    gv1.CurrentRow.Cells(colManualSample).Value = 1
                End If
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No='" + txtDocNo.Value + "'"
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
            whrcls = " final.loc_segment_Code in (" & arrLoc & ") or final.[MCC Code] in (" & arrLoc & ")"
        End If
        'Dim qry As String = "select Document_No,convert (varchar,Document_Date,103) as Document_Date,Description,case when Status=1 then 'Posted' else 'Pending' end as Status from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD"
        Dim qry As String = "select final.Document_No , FINAL.Document_Date , final.Description , final.Status , final.[MCC Code] , final.[Mcc Name] , final.[Plant Code] , final.[Plant Name] , final.[Dock Code] , final.[Dock Name] , final.Reject , final.[From Date - To Date] , final.Shift from " &
      "(select xx.Document_No , xx.Document_Date , xx.Description , xx.Status , xx.[MCC Code] , xx.[Mcc Name] , xx.[Plant Code] , xx.[Plant Name] , xx.[Dock Code] , xx.[Dock Name] , xx.Reject , xx.[From Date - To Date] , xxx.Shift , xx.Loc_Segment_Code from " &
        "(select TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No,convert (varchar,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as Document_Date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Description,case when TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1 then 'Posted' else 'Pending' end as Status" &
        ",TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code as [MCC Code]  ,tspl_mcc_master.MCC_NAME as [Mcc Name] " &
        ",tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name]" &
        ",TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.DOCK_CODE as [Dock Code]" &
        ",TSPL_DOCK_MASTER.Description as [Dock Name] , CASE WHEN Reject = 1 THEN 'Reject Milk' ELSE 'Good Milk' END AS Reject " &
        ", (select  convert(varchar,min(shift_date),103) + '  To  ' + convert(varchar,max(shift_date),103) from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No) AS [From Date - To Date],TSPL_LOCATION_MASTER.loc_segment_Code " &
        " from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD" &
        " left join  tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.mcc_code" &
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" &
        " left join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.dock_code " &
        " ) xx left join (select Document_No,case when COUNT(1)=1 then max(Shift) else 'Both' end as [Shift]  from (" &
     "select Document_No, shift from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL group by Document_No,shift " &
   ") x group by Document_No) xxx on xx.Document_No = xxx.Document_No )final  "
        LoadData(clsCommon.ShowSelectForm("MPUFINDOC", qry, "Document_No", whrcls, txtDocNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
    End Sub

    Public Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMilkProcurementUploaderHead()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.MCC_Code = fndMCCCode.Value
                obj.Dock_Code = txtDockCode.Value ''ERO/25/02/19-000492 by balwinder on 28/02/2019
                obj.Reject = chkMilkReject.Checked
                obj.Arr = New List(Of clsMilkProcurementUploaderDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
                        Dim objTr As New clsMilkProcurementUploaderDetail()
                        objTr.SNo = ii + 1
                        objTr.Shift_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colShiftDate).Value)
                        objTr.Shift = clsCommon.myCstr(gv1.Rows(ii).Cells(colShift).Value)
                        objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value)
                        objTr.VLC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value)
                        objTr.Bulk_Route_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colBulkRouteCode).Value)
                        objTr.No_Of_Cans = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNoOfCan).Value)
                        objTr.Milk_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                        objTr.FAT = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value), 1, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value), IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.ToEven)
                        If obj.Document_Date > objTr.Shift_Date Then
                            obj.Document_Date = objTr.Shift_Date
                        End If
                        objTr.Reject_Defaulter = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectDefaulter).Value)
                        objTr.Reject_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value)

                        objTr.Manual_Weight = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colManualWeight).Value)
                        objTr.Manual_Sample = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colManualSample).Value)
                        objTr.Empty_Sample = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colEmptySample).Value)
                        objTr.Page_No = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPageNo).Value)

                        objTr.arrQCParameter = GetParamCollection(ii)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            frmSRN.IsPoSavedAuto = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Function GetParamCollection(ByVal intRowIndex As Integer) As List(Of clsMilkProcurementUploaderQCParameterDetail)
        Dim ArrParamDetail = New List(Of clsMilkProcurementUploaderQCParameterDetail)
        Dim objParam As clsMilkProcurementUploaderQCParameterDetail = Nothing
        For i As Integer = 0 To gv1.Columns.Count - 1
            If clsCommon.myLen(gv1.Columns(i).Tag) > 0 Then
                'For jj As Integer = 0 To gv1.Rows.Count - 1
                objParam = New clsMilkProcurementUploaderQCParameterDetail
                objParam.Param_Field_Code = clsCommon.myCstr(gv1.Columns(i).Name)
                objParam.Param_Field_Desc = clsCommon.myCstr(gv1.Columns(i).HeaderText)
                objParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(intRowIndex).Cells(i).Value)
                objParam.Param_Type = clsCommon.myCstr(gv1.Columns(i).Tag)
                objParam.Sample_No = clsCommon.myCdbl(gv1.Rows(intRowIndex).Cells(ColSNo).Value)
                ArrParamDetail.Add(objParam)
                'Next
            End If
        Next
        Return ArrParamDetail
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            Dim obj As New clsMilkProcurementUploaderHead()
            obj = clsMilkProcurementUploaderHead.GetData(strCode, NavTyep, Nothing)

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
                txtDate.Value = obj.Document_Date
                txtDesc.Text = obj.Description
                UsLock1.Status = obj.Status

                fndMCCCode.Value = obj.MCC_Code
                LblMccName.Text = obj.MCC_Name

                txtDockCode.Value = obj.Dock_Code
                lblDockName.Text = obj.Dock_Name
                chkMilkReject.Checked = obj.Reject
                SetRejectGridColumn()
                TotQty = 0
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkProcurementUploaderDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftDate).Value = objTr.Shift_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShift).Value = objTr.Shift
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDockCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = objTr.VLC_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBulkRouteCode).Value = objTr.Bulk_Route_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCan).Value = objTr.No_Of_Cans
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkWeight).Value = objTr.Milk_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectDefaulter).Value = objTr.Reject_Defaulter
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUploaderCode).Value = objTr.Uploader_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colManualWeight).Value = objTr.Manual_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colManualSample).Value = objTr.Manual_Sample
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmptySample).Value = objTr.Empty_Sample
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPageNo).Value = objTr.Page_No


                        TotQty += clsCommon.myCdbl(objTr.Milk_Weight)
                    Next
                    txtTotalQty.Value = TotQty
                End If
                Dim ArrParamDetail As List(Of clsMilkProcurementUploaderQCParameterDetail) = clsMilkProcurementUploaderQCParameterDetail.getData(obj.Document_No, Nothing, 0)
                If ArrParamDetail IsNot Nothing AndAlso ArrParamDetail.Count > 0 Then
                    For Each objTr As clsMilkProcurementUploaderQCParameterDetail In ArrParamDetail
                        Try
                            gv1.Rows(objTr.Sample_No - 1).Cells(objTr.Param_Field_Code).Value = objTr.Param_Field_Value
                        Catch ex As Exception
                        End Try
                    Next
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
            If clsCommon.MyMessageBoxShow("Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkProcurementUploaderHead.DeleteData(txtDocNo.Value)
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
            If clsCommon.MyMessageBoxShow("Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If settMilkProcurementBatchPosting Then
                    clsMilkProcurementUploaderHead.PostDataForBatch(txtDocNo.Value)
                Else
                    clsMilkProcurementUploaderHead.PostData(txtDocNo.Value)
                End If
                clsCommon.MyMessageBoxShow("Data posted successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        'fndMCCCode.Value = clsMccMaster.getFinder("", fndMCCCode.Value, isButtonClicked)
        Dim qry As String
        Dim arrLoc As String
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 1 Then
            arrLoc = "'" + obj.Default_LocCode + "'"
        Else
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        fndMCCCode.Value = clsCommon.ShowSelectForm("frmCorrection@MC", qry, "Code", "", fndMCCCode.Value, "Code", isButtonClicked)
        LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim qry As String = "select '01/JAN/2017' as Date,'M' as Shift,1 as 'S NO','' as 'Rt.Code',null as  'Route Name',null as 'VLC Code',null as 'VLC Name',null as 'VSP Name',null as 'Milk Type',null as 'Basic Rate',0 as 'Commission%',null as 'Qty (Ltr)',null as 'KGFat',null as 'KGSNF',null as 'FAT%',null as '" + IIf(isPickCLRInsteadOfSNF, "CLR", "SNF") + "%',0 as 'Avg. Rate',0 as 'KGFat Rate',0 as 'KGSNF Rate',0 as 'Milk Amount',0 as 'Std Milk (Ltr)',0 as 'Commission',0 as 'Incentive',0 as 'Total','' as [Milk Type(M/C/B)] "
        If chkMilkReject.Checked Then
            qry = "select 'M' as Shift,1 as 'S NO',null as 'VLC Code',null as 'Qty (Ltr)',null as 'FAT%',null as 'SNF%','' as [Milk Type(M/C/B)],'' as  [Reject Type],'' as [Reject Defaulter]"
        End If
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            'TEC/22/05/18-000244 by balwinder on 25/05/2018
            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                Throw New Exception("Please select MCC Code")
            End If
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            Dim isCorrect As Boolean = False
            If chkMilkReject.Checked Then
                isCorrect = transportSql.importExcel(gv, "Shift", "S NO", "VLC Code", "Qty (Ltr)", "FAT%", "SNF%", "Milk Type(M/C/B)", "Reject Type", "Reject Defaulter")
            ElseIf isPickCLRInsteadOfSNF Then
                isCorrect = transportSql.importExcel(gv, "Date", "Shift", "S NO", "Rt.Code", "Route Name", "VLC Code", "VLC Name", "VSP Name", "Milk Type", "Basic Rate", "Commission%", "Qty (Ltr)", "KGFat", "KGSNF", "FAT%", "CLR%", "Avg. Rate", "KGFat Rate", "KGSNF Rate", "Milk Amount", "Std Milk (Ltr)", "Commission", "Incentive", "Total", "Milk Type(M/C/B)")
            Else
                isCorrect = transportSql.importExcel(gv, "Date", "Shift", "S NO", "Rt.Code", "Route Name", "VLC Code", "VLC Name", "VSP Name", "Milk Type", "Basic Rate", "Commission%", "Qty (Ltr)", "KGFat", "KGSNF", "FAT%", "SNF%", "Avg. Rate", "KGFat Rate", "KGSNF Rate", "Milk Amount", "Std Milk (Ltr)", "Commission", "Incentive", "Total", "Milk Type(M/C/B)")
            End If

            If isCorrect Then
                Dim Arr As New List(Of clsMilkProcurementUploaderDetail)
                Dim ii As Integer = 0

                Dim dtt As DataTable = TryCast(gv.DataSource, DataTable)
                dtt.Columns.Add("ErrorDesc", "".GetType())
                Try
                    Dim qry As String = ""
                    Dim ErrCount As Integer = 0
                    clsCommon.ProgressBarShow()
                    For ii = 0 To gv.RowCount - 1
                        If clsCommon.myLen(gv.Rows(ii).Cells("VLC Code").Value) > 0 Then
                            Dim objTr As New clsMilkProcurementUploaderDetail()
                            objTr.SNo = ii + 1
                            If chkMilkReject.Checked Then
                                objTr.Shift_Date = txtDate.Value
                                objTr.Reject_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value)
                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
                                    'Throw New Exception("Please enter Reject Type.")
                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Type." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                qry = "select Code from TSPL_MILK_REJECT_TYPE where Code='" + objTr.Reject_Type + "'"
                                objTr.Reject_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
                                    'Throw New Exception("Invalid Reject Type " + clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value))
                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Type " + clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If

                                objTr.Reject_Defaulter = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Defaulter").Value)
                                If clsCommon.myLen(objTr.Reject_Defaulter) <= 0 Then
                                    'Throw New Exception("Please enter Reject Defaulter.")
                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Defaulter." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                If Not (clsCommon.CompairString(objTr.Reject_Defaulter, "VSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Transporter") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Company") = CompairStringResult.Equal) Then
                                    'Throw New Exception("Invalid Reject Defaulter It should be [VSP/Transporter/Company]")
                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Defaulter It should be [VSP/Transporter/Company]" & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                objTr.Shift_Date = clsCommon.myCDate(gv.Rows(ii).Cells("Date").Value)
                            End If
                            objTr.Shift = clsCommon.myCstr(gv.Rows(ii).Cells("Shift").Value)

                            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Code").Value) + "'"
                            If Not SettShowAllDCS Then
                                qry += " and MCC='" + fndMCCCode.Value + "'"
                            End If
                            objTr.VLC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.myLen(objTr.VLC_Code) <= 0 Then
                                dtt.Rows(ii)("ErrorDesc") = "Invalid VSP Uploader code " + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Code").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If

                            objTr.Uploader_Code = clsCommon.myCstr(gv.Rows(ii).Cells("VLC Code").Value)
                            objTr.Milk_Weight = clsCommon.myCdbl(gv.Rows(ii).Cells("Qty (Ltr)").Value)
                            objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                            objTr.FAT = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells("FAT%").Value), 1, MidpointRounding.AwayFromZero)
                            If settMaxFATPerLimit > 0 Then
                                If objTr.FAT > settMaxFATPerLimit Then
                                    'Throw New Exception("FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit))
                                    dtt.Rows(ii)("ErrorDesc") = "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            objTr.SNF = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells(IIf(isPickCLRInsteadOfSNF, "CLR%", "SNF%")).Value), IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.AwayFromZero)
                            If settMaxReceiveSNFPer > 0 AndAlso objTr.SNF > settMaxReceiveSNFPer Then
                                objTr.SNF = settMaxReceiveSNFPer
                            End If
                            If settMaxSNFPerLimit > 0 Then
                                If objTr.SNF > settMaxSNFPerLimit Then
                                    'Throw New Exception("SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit))
                                    dtt.Rows(ii)("ErrorDesc") = "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Milk Type(M/C/B)").Value).ToUpper()
                            If Not (clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
                                'Throw New Exception("Milk Type can be M,B or C")
                                dtt.Rows(ii)("ErrorDesc") = "Milk Type can be M,B or C" & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            Arr.Add(objTr)
                        End If

ExitLOOP:

                    Next

                    dtt.DefaultView.RowFilter = "ErrorDesc<>''"
                    dtt = dtt.DefaultView.ToTable

                    If dtt.Rows.Count > 0 Then
                        clsCommon.ProgressBarHide()
                        common.clsCommon.MyMessageBoxShow("Error in " & dtt.Rows.Count & " Records.", Me.Text, MessageBoxButtons.OK)
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "UnImportedList"
                        ff.Text = "Record Could not Loaded"
                        ff.dt = dtt
                        ff.ShowDialog()
                        Exit Sub
                    End If


                    clsCommon.ProgressBarUpdate("Loading data in Grid.Please wait...")
                    AddRowsByImportAfterValidation(Arr, False)
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Row No" + clsCommon.myCstr(ii + 1) + Environment.NewLine + ex.Message)
                Finally
                    clsCommon.ProgressBarHide()
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete and Clean the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkProcurementUploaderHead.DeleteAndCleanData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Try
            If Not System.IO.Directory.Exists("C:\\ERPTempFolder") Then
                System.IO.Directory.CreateDirectory("C:\\ERPTempFolder")
            End If

            If Not File.Exists(Application.StartupPath + "\XpertBennyDecrptor.exe") Then
                LocalException("Please add File - XpertBennyDecrptor.exe.")
            End If
            Dim qry As String
            Dim strOPFile As String = "C:\\ERPTempFolder\BSP.CSV"
            Dim ofd As FolderBrowserDialog = New FolderBrowserDialog()
            If clsCommon.myLen(strFolderPath) > 0 Then
                ofd.SelectedPath = strFolderPath
            End If
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                strFolderPath = ofd.SelectedPath
                Dim totalFiles As Decimal = Directory.GetFiles(ofd.SelectedPath, "*.BDF").Count
                Dim FilesCounter As Integer = 1
                Dim Arr As New List(Of clsMilkProcurementUploaderDetail)
                clsCommon.ProgressBarPercentShow()
                Try
                    For Each FileName As String In Directory.GetFiles(ofd.SelectedPath, "*.BDF")
                        Try
                            If System.IO.File.Exists(strOPFile) Then
                                File.Delete(strOPFile)
                            End If
                            Process.Start(Application.StartupPath + "\XpertBennyDecrptor.exe", "-i " + FileName + " -o " + strOPFile + " -s , -f")
                            Dim dt As DataTable = transportSql.GetExcelData(strOPFile, Path.GetFileName(FileName))

                            For ii As Integer = 0 To dt.Rows.Count - 1
                                If clsCommon.myLen(dt.Rows(ii)("Extended_Code")) > 0 Then
                                    If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                                        Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable("select MCC_Code,Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + clsCommon.myCstr(dt.Rows(ii)("CP_CODE")) + "' ")
                                        If dtMCC Is Nothing OrElse dtMCC.Rows.Count <= 0 Then
                                            LocalException("Not a valid MCC " + clsCommon.myCstr(dt.Rows(ii)("CP_CODE")))
                                        Else
                                            fndMCCCode.Value = clsCommon.myCstr(dtMCC.Rows(0)("MCC_Code"))
                                            LblMccName.Text = clsCommon.myCstr(dtMCC.Rows(0)("Mcc_Name"))
                                        End If
                                    End If
                                    If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("CP_CODE")), fndMCCCode.Value) = CompairStringResult.Equal Then
                                        LocalException("MCC Should be same for all collection.Selected MCC " + fndMCCCode.Value + "And reading MCC-" + clsCommon.myCstr(dt.Rows(ii)("CP_CODE")))
                                    End If

                                    Dim objTr As New clsMilkProcurementUploaderDetail()
                                    objTr.SNo = ii + 1
                                    objTr.Shift_Date = clsCommon.myCDate(dt.Rows(ii)("Date"))
                                    objTr.Shift = clsCommon.myCstr(dt.Rows(ii)("SHIFT"))
                                    qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC='" + fndMCCCode.Value + "' and VLC_Code_VLC_Uploader='" + clsCommon.myCstr(dt.Rows(ii)("Extended_Code")) + "'"
                                    objTr.VLC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                    If clsCommon.myLen(objTr.VLC_Code) <= 0 Then
                                        LocalException("Invalid VSP Uploader code " + clsCommon.myCstr(dt.Rows(ii)("Extended_Code")))
                                    End If
                                    objTr.Milk_Weight = clsCommon.myCdbl(dt.Rows(ii)("Quantity"))
                                    objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                                    objTr.FAT = clsCommon.myCdbl(dt.Rows(ii)("FAT"))
                                    objTr.SNF = clsCommon.myCdbl(dt.Rows(ii)("SNF"))
                                    objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(dt.Rows(ii)("MILK_Type"))
                                    If clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "MIXED") = CompairStringResult.Equal Then
                                        objTr.Dock_Collection_Milk_Type = "M"
                                    ElseIf clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "COW") = CompairStringResult.Equal Then
                                        objTr.Dock_Collection_Milk_Type = "C"
                                    ElseIf clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "Buffalo") = CompairStringResult.Equal Then
                                        objTr.Dock_Collection_Milk_Type = "B"
                                    End If
                                    If Not (clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
                                        LocalException("Milk Type can be M,B or C")
                                    End If
                                    Arr.Add(objTr)
                                End If
                            Next
                            dt = Nothing
                        Catch ex As Exception
                            LocalException("Error in file " + FileName + Environment.NewLine + ex.Message)
                        End Try
                        clsCommon.ProgressBarPercentUpdate(((FilesCounter) * 100 / totalFiles), "Reading File " + clsCommon.myCstr(FilesCounter) & "/" & clsCommon.myCstr(totalFiles) & "")
                        FilesCounter += 1
                    Next
                    AddRowsByImportAfterValidation(Arr, True)
                    clsCommon.ProgressBarPercentHide()
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    LocalException(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LocalException(ByVal str As String)
        Throw New Exception(str)
    End Sub

    Sub AddRowsByImportAfterValidation(ByVal Arr As List(Of clsMilkProcurementUploaderDetail), ByVal isShowPercent As Boolean)
        Try
            isInsideLoadData = True
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                LoadBlankGrid()
                TotQty = 0
                Dim ii As Decimal = 1
                For Each objTr As clsMilkProcurementUploaderDetail In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftDate).Value = objTr.Shift_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShift).Value = objTr.Shift
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUploaderCode).Value = objTr.Uploader_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objTr.VLC_Code + "'"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCan).Value = objTr.No_Of_Cans
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkWeight).Value = objTr.Milk_Weight
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDockCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type
                    TotQty += clsCommon.myCdbl(objTr.Milk_Weight)
                    If chkMilkReject.Checked Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectDefaulter).Value = objTr.Reject_Defaulter

                    End If
                    If isShowPercent Then
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / Arr.Count), "Loading data in grid" + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(Arr.Count) + "")
                    Else
                        clsCommon.ProgressBarUpdate("Loading data in grid " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(Arr.Count) + "")
                    End If
                    ii += 1
                    gv1.Refresh()
                Next
                txtTotalQty.Value = TotQty
                SetRejectGridColumn()
            End If
        Catch ex As Exception
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtDockCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDockCode._MYValidating
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please first select MCC", Me.Text)
            Exit Sub
        End If
        txtDockCode.Value = clsDockMaster.getFinder("TSPL_DOCK_MASTER.MCC_Code='" + fndMCCCode.Value + "'", txtDockCode.Value, isButtonClicked)
        lblDockName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DOCK_MASTER where Code='" + txtDockCode.Value + "' "))
    End Sub

    Private Sub chkMilkReject_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMilkReject.ToggleStateChanged
        SetRejectGridColumn()
    End Sub

    Sub SetRejectGridColumn()
        gv1.Columns(colRejectRejectType).IsVisible = chkMilkReject.Checked
        gv1.Columns(colRejectDefaulter).IsVisible = chkMilkReject.Checked
        gv1.Columns(colShiftDate).IsVisible = Not chkMilkReject.Checked
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow("Reverse the current document " + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkProcurementUploaderHead.RevereAndUnpost(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Unposed successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
