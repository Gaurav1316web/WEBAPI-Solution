Imports common
Imports System.IO
Imports System.Data.SqlClient

Public Class frmMilkShiftUploader
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const colDockCollectionMilkType As String = "colDockCollectionMilkType"
    Const colUploaderCode As String = "colUploaderCode"
    Const colVLCCode As String = "colVLCCode"
    Const colVLCName As String = "colVLCName"
    Const colNoOfCan As String = "colNoOfCan"
    Const colMilkWeight As String = "colMilkWeight"
    Const colFATPer As String = "colFATPer"
    Const colSNFPer As String = "colSNFPer"
    Const colRejectRejectType As String = "colRejectRejectType"
    Const colRejectDefaulter As String = "colRejectDefaulter"
    Const ReportID As String = "MSUShiRws"

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

    Dim BuffaloQty As Decimal = 0
    Dim BuffaloFatKg As Decimal = 0
    Dim BuffaloSnfKg As Decimal = 0

    Dim CowQty As Decimal = 0
    Dim CowFatKg As Decimal = 0
    Dim CowSnfKg As Decimal = 0

    Dim TotalQty As Decimal = 0
    Dim TotalFatKg As Decimal = 0
    Dim TotalSnfKg As Decimal = 0
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.SetUserMgmt(clsUserMgtCode.MilkProcurementUploader)
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
        LoadShiftFrom()
        AddNew()
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
        txtTotalQty.Value = 0
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDesc.Text = ""
        isNewEntry = True
        fndMCCCode.Value = ""
        LblMccName.Text = ""
        txtDockCode.Value = ""
        lblDockName.Text = ""
        cboShift.SelectedValue = "M"

        ''''''


        GroupBox76.Visible = False
        gbFatSnfDetails.Visible = True
        txtMCCFromDate.Value = DateTime.Now
        txtMCCToDate.Value = DateTime.Now
        txtFromShift.SelectedValue = "M"
        TxtMultiSelectFinder8.arrValueMember = Nothing

        lblBuffaloQty.Text = ""
        lblBuffaloFatKg.Text = ""
        lblBuffaloSnfKg.Text = ""

        lblCowQty.Text = ""
        lblCowFatKg.Text = ""
        lblCowSnfKg.Text = ""

        lblTotalQty.Text = ""
        lblTotalFatKg.Text = ""
        lblTotalSnfKg.Text = ""
        ''''''
        chkMixMilk.Checked = False
        UsLock1.Status = ERPTransactionStatus.Pending
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


        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoComboBox = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Milk Type"
        repoComboBox.Name = colDockCollectionMilkType
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        If objCommonVar.DisplayTypeInMilkReceipt Then
            repoComboBox.DataSource = clsMilkReceiptMCC.GetMilkType()
        Else
            repoComboBox.DataSource = frmMilkReceiptMCC.GetDockCollectionMilkType(True)
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
        repoTextBox.HeaderText = "VLC Code"
        repoTextBox.Name = colVLCCode
        repoTextBox.IsVisible = False
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "VLC Name"
        repoTextBox.Name = colVLCName
        repoTextBox.Width = 150
        repoTextBox.IsVisible = False
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "No Of Cans"
        repoNumBox.Name = colNoOfCan
        repoNumBox.IsVisible = False
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
        Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = ""
            dr("Name") = "Good"
            dt.Rows.InsertAt(dr, 0)
        End If
        repoComboBox.DataSource = dt
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        repoComboBox.IsVisible = True
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
        repoComboBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoComboBox)

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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
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

                    If (e.Column Is gv1.Columns(colMilkWeight) OrElse e.Column Is gv1.Columns(colDockCollectionMilkType) OrElse e.Column Is gv1.Columns(colFATPer) OrElse e.Column Is gv1.Columns(colSNFPer)) Then
                        BuffaloQty = 0
                        BuffaloFatKg = 0
                        BuffaloSnfKg = 0

                        CowQty = 0
                        CowFatKg = 0
                        CowSnfKg = 0 ' gv1.Rows(ii).Cells(colDockCollectionMilkType).Value
                        For ii As Integer = 0 To gv1.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "B") = CompairStringResult.Equal Then
                                BuffaloQty += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                                BuffaloFatKg += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) / 100
                                BuffaloSnfKg += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) / 100
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "C") = CompairStringResult.Equal Then
                                CowQty += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                                CowFatKg += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) / 100
                                CowSnfKg += clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) / 100
                            End If
                        Next
                        lblBuffaloQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven)
                        lblBuffaloFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven)
                        lblBuffaloSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven)

                        lblCowQty.Text = Math.Round(CowQty, 2, MidpointRounding.ToEven)
                        lblCowFatKg.Text = Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                        lblCowSnfKg.Text = Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)

                        lblTotalQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven) + Math.Round(CowQty, 2, MidpointRounding.ToEven)
                        lblTotalFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven) + Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                        lblTotalSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven) + Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)
                    End If
                    ''SetGridFocus()
                    isCellValueChangedOpen = False

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenVLCFinder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            Throw New Exception("Please provide MCC code ")
        End If


        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],  TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] " + Environment.NewLine + _
        " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine + _
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine + _
        " inner join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  " + Environment.NewLine + _
        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine

        Dim whrCls As String = "  TSPL_VLC_MASTER_HEAD.MCC  ='" + fndMCCCode.Value + "'"
        gv1.CurrentRow.Cells(colUploaderCode).Value = clsCommon.ShowSelectForm("SMSRNUdC", qry, "Uploader_Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUploaderCode).Value), "Uploader_Code", isButtonClick)
        gv1.CurrentRow.Cells(colVLCCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VLC_MASTER_HEAD.VLC_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUploaderCode).Value) + "' and mcc='" + fndMCCCode.Value + "'"))
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
        'Prevent future date transaction
        If clsCommon.myCDate(txtDate.Value).Date() > clsCommon.GETSERVERDATE().Date() Then
            clsCommon.MyMessageBoxShow("Cannot allow future date -  " & clsCommon.myCDate(txtDate.Value).Date())
            txtDate.Focus()
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(colRejectRejectType).Value) > 0 Then
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
                            Dim dtLastQty As DataTable = clsDBFuncationality.GetDataTable("select QTY,cast( case when (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) < 0 then 0 else (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) end as decimal(18,2)) as MinQty,cast( (QTY+(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) as decimal(18,2)) as MaxQty from TSPL_MILK_SRN_DETAIL where DOC_CODE in (select top 1  DOC_CODE from tspl_milk_srn_head where DOC_DATE<'" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and VLC_CODE='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value) + "' order by doc_date desc,SHIFT)")
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

        For i As Integer = 0 To gv1.Columns.Count - 1
            If clsCommon.myLen(gv1.Columns(i).Tag) > 0 Then
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(jj).Cells(colRejectRejectType).Value) <= 0 Then
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
                    End If
                Next
            End If
        Next
        chkMixMilk.Checked = (clsCommon.MyMessageBoxShow(Me, "Do you want to mix the milk", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes)
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
        ElseIf e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.DeleteMccMilkShiftPassword
            pwd.strType = clsFixedParameterType.DeleteMccMilkShiftPassword
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                GroupBox76.Visible = True
                gbFatSnfDetails.Visible = False
            Else
                GroupBox76.Visible = False
                gbFatSnfDetails.Visible = True
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
            End If
        End If
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No,convert (varchar,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) as Shift_Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift,TSPL_MILK_SHIFT_UPLOADER_HEAD.Description,case when TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1 then 'Posted' else 'Pending' end as Status" & _
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code as [MCC Code]  ,tspl_mcc_master.MCC_NAME as [Mcc Name] " & _
        ",tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name]" & _
        ",TSPL_MILK_SHIFT_UPLOADER_HEAD.DOCK_CODE as [Dock Code]" & _
        ",TSPL_DOCK_MASTER.Description as [Dock Name]" & _
        " from TSPL_MILK_SHIFT_UPLOADER_HEAD" & _
        " left join  tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MILK_SHIFT_UPLOADER_HEAD.mcc_code" & _
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" & _
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
                obj.Description = txtDesc.Text
                obj.MCC_Code = fndMCCCode.Value
                obj.Dock_Code = txtDockCode.Value
                obj.Mix_Milk = chkMixMilk.Checked
                obj.Arr = New List(Of clsMilkShiftUploaderDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colVLCCode).Value) > 0 Then
                        Dim objTr As New clsMilkShiftUploaderDetail()
                        objTr.SNo = ii + 1
                        objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value)
                        objTr.VLC_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value)
                        objTr.No_Of_Cans = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNoOfCan).Value)
                        objTr.Milk_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkWeight).Value)
                        If objTr.No_Of_Cans = 0 Then
                            objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                        End If
                        objTr.FAT = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value), 1, MidpointRounding.ToEven)
                        objTr.SNF = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value), IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.ToEven)
                        objTr.Reject_Defaulter = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectDefaulter).Value)
                        objTr.Reject_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value)
                        objTr.arrQCParameter = GetParamCollection(ii)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            frmSRN.IsPoSavedAuto = False
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function

    Private Function GetParamCollection(ByVal intRowIndex As Integer) As List(Of clsMilkShiftUploaderQCParameterDetail)
        Dim ArrParamDetail = New List(Of clsMilkShiftUploaderQCParameterDetail)
        Dim objParam As clsMilkShiftUploaderQCParameterDetail = Nothing
        For i As Integer = 0 To gv1.Columns.Count - 1
            If clsCommon.myLen(gv1.Columns(i).Tag) > 0 Then
                'For jj As Integer = 0 To gv1.Rows.Count - 1
                objParam = New clsMilkShiftUploaderQCParameterDetail
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
            Dim obj As New clsMilkShiftUploaderHead()
            obj = clsMilkShiftUploaderHead.GetData(strCode, NavTyep, Nothing)

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
                txtDesc.Text = obj.Description
                UsLock1.Status = obj.Status
                fndMCCCode.Value = obj.MCC_Code
                LblMccName.Text = obj.MCC_Name
                txtDockCode.Value = obj.Dock_Code
                lblDockName.Text = obj.Dock_Name
                TotQty = 0

                BuffaloQty = 0
                BuffaloFatKg = 0
                BuffaloSnfKg = 0

                CowQty = 0
                CowFatKg = 0
                CowSnfKg = 0

                chkMixMilk.Checked = obj.Mix_Milk
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkShiftUploaderDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDockCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = objTr.VLC_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCan).Value = objTr.No_Of_Cans
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkWeight).Value = objTr.Milk_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectDefaulter).Value = objTr.Reject_Defaulter
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUploaderCode).Value = objTr.VLC_Uploader_Code
                        TotQty += clsCommon.myCdbl(objTr.Milk_Weight)
                        If clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal Then
                            BuffaloQty += clsCommon.myCdbl(objTr.Milk_Weight)
                            BuffaloFatKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.FAT) / 100
                            BuffaloSnfKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.SNF) / 100
                        ElseIf clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal Then
                            CowQty += clsCommon.myCdbl(objTr.Milk_Weight)
                            CowFatKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.FAT) / 100
                            CowSnfKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.SNF) / 100
                        End If
                    Next
                    txtTotalQty.Value = TotQty

                    lblBuffaloQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven)
                    lblBuffaloFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven)
                    lblBuffaloSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven)

                    lblCowQty.Text = Math.Round(CowQty, 2, MidpointRounding.ToEven)
                    lblCowFatKg.Text = Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                    lblCowSnfKg.Text = Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)

                    lblTotalQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven) + Math.Round(CowQty, 2, MidpointRounding.ToEven)
                    lblTotalFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven) + Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                    lblTotalSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven) + Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)

                End If
                Dim ArrParamDetail As List(Of clsMilkShiftUploaderQCParameterDetail) = clsMilkShiftUploaderQCParameterDetail.getData(obj.Document_No, Nothing, 0)
                If ArrParamDetail IsNot Nothing AndAlso ArrParamDetail.Count > 0 Then
                    For Each objTr As clsMilkShiftUploaderQCParameterDetail In ArrParamDetail
                        Try
                            gv1.Rows(objTr.Sample_No - 1).Cells(objTr.Param_Field_Code).Value = objTr.Param_Field_Value
                        Catch ex As Exception
                        End Try
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
                clsMilkShiftUploaderHead.DeleteData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow("Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow("Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If settMilkProcurementBatchPosting Then
                    clsMilkShiftUploaderHead.PostDataForBatch(txtDocNo.Value)
                Else
                    clsMilkShiftUploaderHead.PostData(txtDocNo.Value)
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
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        fndMCCCode.Value = clsCommon.ShowSelectForm("frmCorrection@M", qry, "Code", "", fndMCCCode.Value, "Code", isButtonClicked)
        LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim qry As String = "select 1 as 'S NO',null as 'VLC Uploader Code',null as 'Qty (Ltr)',null as 'FAT%',null as 'SNF%','' as [Milk Type(M/C/B)],'' as  [Reject Type],'' as [Reject Defaulter]"
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Document Not found to export.")
        End If
        Dim qry As String = "select SNo as 'S NO',TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as 'VLC Uploader Code',Milk_Weight as 'Qty (Ltr)',FAT as 'FAT%',SNF as 'SNF%',Dock_Collection_Milk_Type as [Milk Type(M/C/B)],Reject_Type as  [Reject Type],Reject_Defaulter as [Reject Defaulter]" + Environment.NewLine + _
        "from TSPL_MILK_SHIFT_UPLOADER_DETAIL" + Environment.NewLine + _
        "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code" + Environment.NewLine + _
        "where Document_No='" + txtDocNo.Value + "'"
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                Throw New Exception("Please select MCC Code")
            End If
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "S NO", "VLC Uploader Code", "Qty (Ltr)", "FAT%", "SNF%", "Milk Type(M/C/B)", "Reject Type", "Reject Defaulter") Then
                Dim Arr As New List(Of clsMilkShiftUploaderDetail)
                Dim ii As Integer = 0

                Dim dtt As DataTable = TryCast(gv.DataSource, DataTable)
                dtt.Columns.Add("ErrorDesc", "".GetType())
                Try
                    Dim qry As String = ""
                    Dim ErrCount As Integer = 0
                    clsCommon.ProgressBarShow()
                    For ii = 0 To gv.RowCount - 1
                        If clsCommon.myLen(gv.Rows(ii).Cells("VLC Uploader Code").Value) > 0 Then
                            Dim objTr As New clsMilkShiftUploaderDetail()
                            objTr.SNo = ii + 1
                            objTr.Reject_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value)
                            If clsCommon.myLen(objTr.Reject_Type) > 0 Then
                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Type." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                qry = "select Code from TSPL_MILK_REJECT_TYPE where Code='" + objTr.Reject_Type + "'"
                                objTr.Reject_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Type " + clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If

                                objTr.Reject_Defaulter = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Defaulter").Value)
                                If clsCommon.myLen(objTr.Reject_Defaulter) <= 0 Then
                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Defaulter." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                If Not (clsCommon.CompairString(objTr.Reject_Defaulter, "VSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Transporter") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Company") = CompairStringResult.Equal) Then
                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Defaulter It should be [Company/Transporter/VSP]" & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC='" + fndMCCCode.Value + "' and VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value) + "'"
                            objTr.VLC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.myLen(objTr.VLC_Code) <= 0 Then
                                dtt.Rows(ii)("ErrorDesc") = "Invalid VSP Uploader code " + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If

                            objTr.VLC_Uploader_Code = clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value)
                            objTr.Milk_Weight = clsCommon.myCdbl(gv.Rows(ii).Cells("Qty (Ltr)").Value)
                            objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
                            objTr.FAT = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells("FAT%").Value), 1, MidpointRounding.AwayFromZero)
                            If settMaxFATPerLimit > 0 Then
                                If objTr.FAT > settMaxFATPerLimit Then
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
                                    dtt.Rows(ii)("ErrorDesc") = "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Milk Type(M/C/B)").Value).ToUpper()
                            If Not (clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
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
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LocalException(ByVal str As String)
        Throw New Exception(str)
    End Sub

    Sub AddRowsByImportAfterValidation(ByVal Arr As List(Of clsMilkShiftUploaderDetail), ByVal isShowPercent As Boolean)
        Try
            isInsideLoadData = True
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                LoadBlankGrid()
                TotQty = 0

                BuffaloQty = 0
                BuffaloFatKg = 0
                BuffaloSnfKg = 0
                CowQty = 0
                CowFatKg = 0
                CowSnfKg = 0

                Dim ii As Decimal = 1
                For Each objTr As clsMilkShiftUploaderDetail In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUploaderCode).Value = objTr.VLC_Uploader_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objTr.VLC_Code + "'"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCan).Value = objTr.No_Of_Cans
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkWeight).Value = objTr.Milk_Weight
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDockCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type
                    TotQty += clsCommon.myCdbl(objTr.Milk_Weight)

                    '===========================
                    If clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal Then
                        BuffaloQty += clsCommon.myCdbl(objTr.Milk_Weight)
                        BuffaloFatKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.FAT) / 100
                        BuffaloSnfKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.SNF) / 100
                    ElseIf clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal Then
                        CowQty += clsCommon.myCdbl(objTr.Milk_Weight)
                        CowFatKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.FAT) / 100
                        CowSnfKg += clsCommon.myCdbl(objTr.Milk_Weight) * clsCommon.myCdbl(objTr.SNF) / 100
                    End If
                    '===========================

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                    If clsCommon.myLen(objTr.Reject_Type) > 0 Then
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
                '=================================
                lblBuffaloQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven)
                lblBuffaloFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven)
                lblBuffaloSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven)

                lblCowQty.Text = Math.Round(CowQty, 2, MidpointRounding.ToEven)
                lblCowFatKg.Text = Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                lblCowSnfKg.Text = Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)

                lblTotalQty.Text = Math.Round(BuffaloQty, 2, MidpointRounding.ToEven) + Math.Round(CowQty, 2, MidpointRounding.ToEven)
                lblTotalFatKg.Text = Math.Round(BuffaloFatKg, 2, MidpointRounding.ToEven) + Math.Round(CowFatKg, 2, MidpointRounding.ToEven)
                lblTotalSnfKg.Text = Math.Round(BuffaloSnfKg, 2, MidpointRounding.ToEven) + Math.Round(CowSnfKg, 2, MidpointRounding.ToEven)
                '=================================

            End If
        Catch ex As Exception
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtDockCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDockCode._MYValidating
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            fndMCCCode.Focus()
            clsCommon.MyMessageBoxShow("Please first select MCC", Me.Text)
            Exit Sub
        End If
        txtDockCode.Value = clsDockMaster.getFinder("TSPL_DOCK_MASTER.MCC_Code='" + fndMCCCode.Value + "'", txtDockCode.Value, isButtonClicked)
        lblDockName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DOCK_MASTER where Code='" + txtDockCode.Value + "' "))
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
            clsCommon.MyMessageBoxShow("Please First select MCC")
        End If


        If clsCommon.MyMessageBoxShow("Delete the collection data", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
            clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiSelectFinder8__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder8._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        TxtMultiSelectFinder8.arrValueMember = clsCommon.ShowMultipleSelectForm("BulkMCC@Uti1", qry, "MCC_Code", "MCC_NAME", TxtMultiSelectFinder8.arrValueMember, TxtMultiSelectFinder8.arrDispalyMember)
    End Sub

    'Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
    '    'If e.KeyCode = Keys.Enter Then
    '    '    If gv1.CurrentCell IsNot Nothing Then
    '    '        If gv1.CurrentCell.ColumnInfo.Name = colDockCollectionMilkType OrElse
    '    '            gv1.CurrentCell.ColumnInfo.Name = colUploaderCode OrElse
    '    '            gv1.CurrentCell.ColumnInfo.Name = colNoOfCan OrElse
    '    '            gv1.CurrentCell.ColumnInfo.Name = colMilkWeight OrElse
    '    '            gv1.CurrentCell.ColumnInfo.Name = colFATPer OrElse
    '    '            gv1.CurrentCell.ColumnInfo.Name = colSNFPer Then
    '    '            gv1.EndEdit()
    '    '            SetGridFocus()
    '    '            gv1.BeginEdit()
    '    '        End If
    '    '    End If
    '    'End If
    'End Sub

    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            If gv1.CurrentCell.ColumnInfo.Name = colDockCollectionMilkType Then
                gv1.CurrentColumn = gv1.Columns(colUploaderCode)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colUploaderCode Then
                gv1.CurrentColumn = gv1.Columns(colMilkWeight)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colMilkWeight) Then
                gv1.CurrentColumn = gv1.Columns(colFATPer)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colFATPer) Then
                gv1.CurrentColumn = gv1.Columns(colSNFPer)
            ElseIf (gv1.CurrentCell.ColumnInfo.Name = colSNFPer) Then
                Dim str As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDockCollectionMilkType).Value)
                If gv1.Rows.Count >= gv1.CurrentRow.Index + 1 Then
                    gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colDockCollectionMilkType).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colDockCollectionMilkType).Value = str
                    End If
                End If
                gv1.CurrentColumn = gv1.Columns(colUploaderCode)
            End If
        End If
    End Sub

    Private Sub txtDesc_Leave(sender As Object, e As EventArgs) Handles txtDesc.Leave
        If gv1.Rows.Count > 0 Then
            gv1.Focus()
            gv1.CurrentColumn = gv1.Columns(colDockCollectionMilkType)
        End If
    End Sub

    Private Sub gv1_CellValidating(sender As Object, e As CellValidatingEventArgs) Handles gv1.CellValidating
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
