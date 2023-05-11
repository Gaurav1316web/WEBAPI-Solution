'--------Created By Richa 28/07/2014 Against Ticket No BM00000003246
''-------------updation by richa Against Ticket no BM00000003769,BM00000005024
'' Updation By Richa Agarwal Against Ticket No. BM00000003776 on 10/09/2014 ,BM00000004030 on 24/09/2014,BM00000004190,BM00000006673
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmQualityCheckBulkSale
    Inherits FrmMainTranScreen
    Dim ApplyMultiChamber As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colChamberDesc As String = "ChamberDesc"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colCLR As String = "colCLR"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Dim Qry As String
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
    Dim AllowSNFNotManditoryInBulkSale As Boolean = False
    Dim BulkQCWithoutCLR As Boolean = False
    Dim SettCalculateSNFFromCLRForMCCMilk As Boolean = False
    Dim Allow0FatPerOnBulkSaleQualityCheckScreen As Boolean = False
    Dim AllowFatPerInanynumberofMultipesonBulkQC As Boolean = False
    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub FrmQualityCheckBulkSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub FrmQualityCheckBulkSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ApplyMultiChamber = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, Nothing)) = 1, True, False))
        AllowSNFNotManditoryInBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSNFNotManditoryInBulkSale, clsFixedParameterCode.AllowSNFNotManditoryInBulkSale, Nothing)) = 1, True, False))
        BulkQCWithoutCLR = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BulkQCWithoutCLR, clsFixedParameterCode.BulkQCWithoutCLR, Nothing)) = 1, True, False))
        SettCalculateSNFFromCLRForMCCMilk = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateSNFFromCLRForMCCMilk, clsFixedParameterCode.CalculateSNFFromCLRForMCCMilk, Nothing)) = 1)
        Allow0FatPerOnBulkSaleQualityCheckScreen = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow0FatPerOnBulkSaleQualityCheckScreen, clsFixedParameterCode.Allow0FatPerOnBulkSaleQualityCheckScreen, Nothing)) = 1, True, False))
        AllowFatPerInanynumberofMultipesonBulkQC = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowFatPerInanynumberofMultipesonBulkQC, clsFixedParameterCode.AllowFatPerInanynumberofMultipesonBulkQC, Nothing)) = 1, True, False))
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    'Sub loadBlankItemGrid()

    '    gvItem.Rows.Clear()
    '    gvItem.Columns.Clear()
    '    gvItem.DataSource = Nothing

    '    Dim lineNo As New GridViewTextBoxColumn()
    '    lineNo.FormatString = ""
    '    lineNo.HeaderText = "SL. No."
    '    lineNo.Name = colSlNo
    '    lineNo.Width = 60
    '    lineNo.ReadOnly = True
    '    lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvItem.Columns.Add(lineNo)


    '    Dim itemCode As New GridViewTextBoxColumn()
    '    itemCode.FormatString = ""
    '    itemCode.HeaderText = "Item Code"
    '    itemCode.Name = colItemCode
    '    itemCode.Width = 100
    '    itemCode.ReadOnly = True
    '    itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gvItem.Columns.Add(itemCode)


    '    Dim itemDesc As New GridViewTextBoxColumn()
    '    itemDesc.FormatString = ""
    '    itemDesc.HeaderText = "Item Desc"
    '    itemDesc.Name = colItemDesc
    '    itemDesc.Width = 320
    '    itemDesc.ReadOnly = True
    '    itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gvItem.Columns.Add(itemDesc)

    '    'Dim Qty As New GridViewDecimalColumn
    '    'Qty.FormatString = ""
    '    'Qty.HeaderText = "Qty"
    '    'Qty.Name = colQty
    '    'Qty.Width = 120
    '    'Qty.ReadOnly = True
    '    'Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvItem.Columns.Add(Qty)

    '    Dim strUOM As New GridViewTextBoxColumn()
    '    strUOM.FormatString = ""
    '    strUOM.HeaderText = "UOM"
    '    strUOM.Name = colUOM
    '    strUOM.Width = 120
    '    strUOM.ReadOnly = False
    '    strUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvItem.Columns.Add(strUOM)

    '    Dim fat As New GridViewDecimalColumn
    '    fat.FormatString = ""
    '    fat.HeaderText = "FAT"
    '    fat.Name = colFat
    '    fat.Width = 75
    '    fat.FormatString = "{0:n2}"
    '    fat.ReadOnly = False
    '    fat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvItem.Columns.Add(fat)

    '    Dim clr As New GridViewDecimalColumn
    '    clr.FormatString = ""
    '    clr.HeaderText = "CLR"
    '    clr.Name = colCLR
    '    clr.Width = 75
    '    clr.FormatString = "{0:n2}"
    '    clr.ReadOnly = False
    '    clr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvItem.Columns.Add(clr)

    '    Dim snf As New GridViewDecimalColumn
    '    snf.FormatString = ""
    '    snf.HeaderText = "SNF"
    '    snf.Name = colSNF
    '    snf.Width = 75
    '    snf.FormatString = "{0:n2}"
    '    snf.ReadOnly = True
    '    snf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvItem.Columns.Add(snf)

    '    gvItem.Rows.AddNew()
    '    gvItem.Rows(0).Cells(colSlNo).Value = "1"
    '    gvItem.AllowAddNewRow = False
    '    gvItem.AllowDeleteRow = False
    '    gvItem.AllowRowReorder = False
    '    gvItem.ShowGroupPanel = False
    '    gvItem.EnableFiltering = False
    '    gvItem.EnableSorting = False
    '    gvItem.EnableGrouping = False

    'End Sub
    ''Sub loadBlankParameterGrid()
    ''    Dim pFields As Boolean = True
    ''    Dim gridWidth As Integer = 60
    ''    Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  order by Ordering "
    ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    ''    If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
    ''        pFields = True
    ''    Else
    ''        pFields = False
    ''    End If
    ''    gvParam.Rows.Clear()
    ''    gvParam.Columns.Clear()
    ''    gvParam.DataSource = Nothing
    ''    If pFields Then
    ''        gvParam.Columns.Add("colSLNO", "SL. No.")
    ''        gvParam.Columns("colSLNO").Width = 60
    ''        gvParam.Columns("colSLNO").ReadOnly = True
    ''        gvParam.Columns("colSLNO").Tag = "SLNO"
    ''        For i As Integer = 0 To dt.Rows.Count() - 1
    ''            gvParam.Columns.Add(dt.Rows(i)("Code"), dt.Rows(i)("Description"))
    ''            gvParam.Columns(dt.Rows(i)("Code")).Width = 120
    ''            If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
    ''                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
    ''            ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
    ''                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
    ''            ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
    ''                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
    ''            ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
    ''                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
    ''            Else
    ''                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = False
    ''            End If
    ''            gvParam.Columns(dt.Rows(i)("Code")).Tag = dt.Rows(i)("Type")
    ''        Next
    ''        gvParam.Columns.Add("colRemarks", "Remarks")
    ''        gvParam.Columns("colRemarks").Width = 300
    ''        gvParam.Columns("colRemarks").ReadOnly = False
    ''        gvParam.Columns("colRemarks").Tag = "REM"
    ''    Else
    ''        Throw New Exception("There is No parameter defined in Parameter Master. Please Define It First")
    ''    End If
    ''    gvParam.Rows.AddNew()
    ''    gvParam.Rows(0).Cells("colSLNO").Value = "1"
    ''    gvParam.AllowAddNewRow = False
    ''    gvParam.AllowDeleteRow = False
    ''    gvParam.AllowRowReorder = False
    ''    gvParam.ShowGroupPanel = False
    ''    gvParam.EnableFiltering = False
    ''    gvParam.EnableSorting = False
    ''    gvParam.EnableGrouping = False
    ''End Sub
    Sub loadBlankParameterGrid()
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master where IsBulkSale=1 order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn = Nothing
        Dim repoTextColumn As GridViewTextBoxColumn = Nothing
        Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
        If pFields Then
            gvParam.Columns.Add("colSLNO", "SL. No.")
            gvParam.Columns("colSLNO").Width = 60
            gvParam.Columns("colSLNO").ReadOnly = True
            gvParam.Columns("colSLNO").Tag = "SLNO"
            ''richa 24/09/2014
            Dim itemCode As New GridViewTextBoxColumn()
            itemCode.FormatString = ""
            itemCode.HeaderText = "Item Code"
            itemCode.Name = colItemCode
            itemCode.Width = 100
            itemCode.ReadOnly = True
            itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvParam.Columns.Add(itemCode)


            Dim itemDesc As New GridViewTextBoxColumn()
            itemDesc.FormatString = ""
            itemDesc.HeaderText = "Item Desc"
            itemDesc.Name = colItemDesc
            itemDesc.Width = 320
            itemDesc.ReadOnly = True
            itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvParam.Columns.Add(itemDesc)

            Dim chamberDesc As New GridViewTextBoxColumn()
            chamberDesc.FormatString = ""
            chamberDesc.HeaderText = "Chamber Desc"
            chamberDesc.Name = colChamberDesc
            chamberDesc.Width = 320
            chamberDesc.ReadOnly = True
            chamberDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvParam.Columns.Add(chamberDesc)
            If ApplyMultiChamber Then
                gvParam.Columns(colChamberDesc).IsVisible = True
                gvParam.Columns(colChamberDesc).VisibleInColumnChooser = True
            Else
                gvParam.Columns(colChamberDesc).IsVisible = False
                gvParam.Columns(colChamberDesc).VisibleInColumnChooser = False
            End If

            Dim strUOM As New GridViewTextBoxColumn()
            strUOM.FormatString = ""
            strUOM.HeaderText = "UOM"
            strUOM.Name = colUOM
            strUOM.Width = 120
            strUOM.ReadOnly = True
            strUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvParam.Columns.Add(strUOM)

            'Dim fat As New GridViewDecimalColumn
            'fat.FormatString = ""
            'fat.HeaderText = "FAT"
            'fat.Name = colFat
            'fat.Width = 75
            'fat.FormatString = "{0:n2}"
            'fat.ReadOnly = False
            'fat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            'gvParam.Columns.Add(fat)

            'Dim clr As New GridViewDecimalColumn
            'clr.FormatString = ""
            'clr.HeaderText = "CLR"
            'clr.Name = colCLR
            'clr.Width = 75
            'clr.FormatString = "{0:n2}"
            'clr.ReadOnly = False
            'clr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            'gvParam.Columns.Add(clr)

            ''richa 18/11/2014
            Dim fat As New GridViewDecimalColumn
            'fat.FormatString = ""
            fat.HeaderText = "FAT"
            fat.Name = colFat
            fat.Width = 75
            fat.ReadOnly = False
            If AllowFatPerInanynumberofMultipesonBulkQC Then
                fat.FormatString = "{0:n3}"
            Else
                fat.FormatString = "{0:n2}"
            End If

            fat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvParam.Columns.Add(fat)

            Dim clr As New GridViewTextBoxColumn
            clr.FormatString = ""
            clr.HeaderText = "CLR"
            clr.Name = colCLR
            clr.Width = 75
            'clr.ReadOnly = False
            ''richa ERO/06/02/19-000486 6 Feb,2019
            If SettCalculateSNFFromCLRForMCCMilk = True Then
                clr.ReadOnly = False
            Else
                clr.ReadOnly = clsERPFuncationality.isLocationMcc(lblLocationCode.Text)
            End If

            clr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvParam.Columns.Add(clr)

            ''--------------

            Dim snf As New GridViewDecimalColumn
            snf.FormatString = ""
            snf.HeaderText = "SNF"
            snf.Name = colSNF
            snf.Width = 75
            snf.FormatString = "{0:n2}"
            'snf.ReadOnly = True
            ''richa ERO/06/02/19-000486 6 Feb,2019
            If SettCalculateSNFFromCLRForMCCMilk = True Then
                snf.ReadOnly = True
            Else
                snf.ReadOnly = Not clsERPFuncationality.isLocationMcc(lblLocationCode.Text)
            End If
            snf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvParam.Columns.Add(snf)
            ''====================
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "CF") <> CompairStringResult.Equal Then


                    If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                        repoDecimalColumn = New GridViewDecimalColumn()
                        repoDecimalColumn.Name = dt.Rows(i)("Code")
                        repoDecimalColumn.Width = 120
                        repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                        repoDecimalColumn.Tag = dt.Rows(i)("Type")

                        'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Or clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
                        '    repoDecimalColumn.ReadOnly = True
                        'Else
                        '    repoDecimalColumn.ReadOnly = False
                        'End If
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
                        'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                        '    repoComboColumn.ReadOnly = True
                        'Else
                        '    repoComboColumn.ReadOnly = False
                        'End If
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
                        'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                        '    repoComboColumn.ReadOnly = True
                        'Else
                        '    repoComboColumn.ReadOnly = False
                        'End If
                        gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                    Else
                        repoTextColumn = New GridViewTextBoxColumn()
                        repoTextColumn.Name = dt.Rows(i)("Code")
                        repoTextColumn.Width = 120
                        repoTextColumn.HeaderText = dt.Rows(i)("Description")
                        repoTextColumn.Tag = dt.Rows(i)("Type")
                        'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                        '    repoTextColumn.ReadOnly = True
                        'Else
                        '    repoTextColumn.ReadOnly = False
                        'End If
                        gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)
                    End If
                    'gvParam.Columns.Add(dt.Rows(i)("Code"), dt.Rows(i)("Description"))
                    'gvParam.Columns(dt.Rows(i)("Code")).Width = 120

                    'gvParam.Columns(dt.Rows(i)("Code")).Tag = dt.Rows(i)("Type")
                End If
            Next
            gvParam.Columns.Add("colRemarks", "Remarks")
            gvParam.Columns("colRemarks").Width = 300
            gvParam.Columns("colRemarks").ReadOnly = False
            gvParam.Columns("colRemarks").Tag = "REM"
            gvParam.Columns("colRemarks").WrapText = True
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define It First")
        End If
        If Not ApplyMultiChamber Then
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
        'gvParam.AutoSizeRows = True

        ReStoreGridLayout()

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
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmQualityCheckBulkSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub Reset()
        fndQcNo.Value = ""
        fndLoadingNo.Value = ""
        LblLoadingNo.Text = ""
        lblGateEntryNo.Text = ""
        LblWeighmentNo.Text = ""
        FndTankerNo.Value = ""
        ' LblTankerName.Text = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        ' lblTankerNoValue.Text = ""
        lblSiloNo.Text = ""
        LblSiloName.Text = ""
        lblCustomerCode.Text = ""
        lblCustomerName.Text = ""
        'txtCorrectionFactor.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where TYPE='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + "' and Code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'"))
        UsLock1.Status = ERPTransactionStatus.Pending
        txtQCdate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        'Dim DateTime As String = clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where Code ='" & clsFixedParameterCode.AllowToSaveTimeWithDocumentDate & "'")
        If DateTime = "1" Then
            txtQCdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtQCdate.CustomFormat = "dd/MM/yyyy"
        End If
        fndQcNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        ' loadBlankItemGrid()
        loadBlankParameterGrid()
        ReStoreGridLayout()
        LOCATIONRIGTHS()
        txtCorrectionFactor.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where TYPE='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + "' and Code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'"))
        'For i As Integer = 1 To gvParam.Columns.Count - 1
        '    If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
        '        gvParam.Rows(0).Cells(i).Value = txtCorrectionFactor.Value
        '    End If
        'Next
        isNewEntry = True
    End Sub

    Private Sub fndLoadingNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLoadingNo._MYValidating
        'Dim dt As DataTable
        ''Qry = "Select LoadingTanker_No as Code ,LoadingTanker_Date as [Loading Tanker Date],Weighment_No as [Weighment No]  from TSPL_LOADING_TANKER_DETAIL_BULKSALE  "
        'Qry = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as Code,Convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103)  as Date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE  Left Outer Join TSPL_TANKER_MASTER  on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code"
        'fndLoadingNo.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Posted=1 and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") and  LoadingTanker_No not in (Select LoadingTanker_No from TSPL_Quality_Check_BulkSale where QC_No<>'" + fndQcNo.Value + "' and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") ) ", fndLoadingNo.Value, "", isButtonClicked)
        'Qry = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No, TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No,TSPL_TANKER_MASTER.Tanker_Name,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No ,SubLocationMaster.Location_Desc as SubLocationDesc,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity from TSPL_WEIGHMENT_DETAIL_BULKSALE " & _
        '      " Left Outer Join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE .Weighment_No " & _
        '      " Left Outer Join TSPL_TANKER_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No=TSPL_TANKER_MASTER.Tanker_No " & _
        '      " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
        '      " Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No= SubLocationMaster.Location_Code " & _
        '      " Left Outer Join TSPL_ITEM_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
        '      " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No='" + fndLoadingNo.Value + "'"
        'dt = clsDBFuncationality.GetDataTable(Qry)
        'If dt.Rows.Count > 0 Then
        '    LblWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
        '    lblGateEntryNo.Text = clsCommon.myCstr(dt.Rows(0)("GateEntry_Document_No"))
        '    lblTankerNoValue.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
        '    LblTankerName.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_Name"))
        '    lblLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
        '    LblLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
        '    lblSiloNo.Text = clsCommon.myCstr(dt.Rows(0)("Silo_No"))
        '    LblSiloName.Text = clsCommon.myCstr(dt.Rows(0)("SubLocationDesc"))
        '    'gvItem.Rows(0).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
        '    'gvItem.Rows(0).Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
        '    'gvItem.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
        '    'gvItem.Rows(0).Cells(colQty).Value = clsCommon.myCstr(dt.Rows(0)("Quantity"))
        '    gvParam.Rows(0).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
        '    gvParam.Rows(0).Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
        '    gvParam.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
        'Else

        '    fndLoadingNo.Value = ""
        '    lblGateEntryNo.Text = ""
        '    LblWeighmentNo.Text = ""
        '    LblTankerName.Text = ""
        '    lblLocationCode.Text = ""
        '    LblLocationName.Text = ""
        '    lblTankerNoValue.Text = ""
        '    lblSiloNo.Text = ""
        '    LblSiloName.Text = ""
        '    'loadBlankItemGrid()
        'End If

    End Sub

    'Private Sub gvItem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
    '    If e.Column Is gvItem.Columns(colFat) Or e.Column Is gvItem.Columns(colCLR) Then
    '        CalculateSNF()
    '        For i As Integer = 1 To gvParam.Columns.Count - 1
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
    '                gvParam.Rows(0).Cells(i).Value = clsCommon.myCdbl(gvItem.Rows(0).Cells(colCLR).Value)
    '            End If
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
    '                gvParam.Rows(0).Cells(i).Value = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
    '            End If
    '        Next
    '        calculateSNFForParameter()
    '    ElseIf e.Column Is gvItem.Columns(colUOM) Then
    '        OpenUOMList(False)
    '    End If

    'End Sub
    'Sub OpenUOMList(ByVal isButtonClick As Boolean)
    '    Dim strICode As String = clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)
    '    If clsCommon.myLen(strICode) > 0 Then
    '        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
    '        Dim whrCls As String = "Item_Code='" + strICode + "'"
    '        gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("QCUnitFinder", qry, "Code", whrCls, clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
    '    Else
    '        gvItem.CurrentRow.Cells(colUOM).Value = ""
    '    End If
    'End Sub
    Private Sub CalculateSNF()
        Dim fat As Double = 0
        Dim clr As Double = 0
        Dim CorrectionFactor As Double = 0
        Dim Snf As Double = 0
        Dim i As Integer = 0
        ''richa ERO/06/02/19-000486
        If Not clsERPFuncationality.isLocationMcc(lblLocationCode.Text) Or SettCalculateSNFFromCLRForMCCMilk = True Then
            If gvParam.Rows.Count > 0 Then
                CorrectionFactor = clsCommon.myCdbl(txtCorrectionFactor.Value)
                For i = 0 To gvParam.Rows.Count - 1
                    fat = clsCommon.myCdbl(gvParam.Rows(i).Cells(colFat).Value)
                    clr = clsCommon.myCdbl(gvParam.Rows(i).Cells(colCLR).Value)
                    Snf = clsEkoPro.getSnfOnCalculation(fat, clr, CorrectionFactor)

                    'UDL/15/06/18-000189
                    ' gvParam.Rows(i).Cells(colSNF).Value = Math.Floor(Snf * 100) / 100
                    gvParam.Rows(i).Cells(colSNF).Value = (Snf * 100) / 100
                Next

            End If
        End If
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtCorrectionFactor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCorrectionFactor.TextChanged
        If clsCommon.myCdbl(txtCorrectionFactor.Value) <> 0 Then

            'For i As Integer = 1 To gvParam.Columns.Count - 1
            '    If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
            '        gvParam.Rows(0).Cells(i).Value = txtCorrectionFactor.Value
            '    End If

            'Next

            CalculateSNF()
            'calculateSNFForParameter()
        End If

    End Sub

    Private Sub gvParam_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvParam.CellValueChanged
        Try

            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                'If clsCommon.CompairString(gvParam.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                '    calculateSNFForParameter()
                'End If
                ''richa agarwal 18/11/2014
                If AllowFatPerInanynumberofMultipesonBulkQC = False Then
                    If e.Column Is gvParam.Columns(colFat) Then
                        gvParam.Rows(e.RowIndex).Cells(colFat).Value = Math.Truncate(Math.Round(clsCommon.myCdbl(gvParam.Rows(e.RowIndex).Cells(colFat).Value) * 100, 2)) / 100
                        Dim fracValue1 As Double = 0
                        If clsCommon.myLen(colFat) > 0 Then
                            fracValue1 = clsCommon.myCdbl(gvParam.Rows(e.RowIndex).Cells(colFat).Value)
                            fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                            If CInt(fracValue1) Mod 5 <> 0 Then
                                gvParam.Rows(e.RowIndex).Cells(colFat).Value = 0
                                gvParam.CurrentRow = gvParam.Rows(e.RowIndex)
                                gvParam.CurrentColumn = gvParam.Columns(colFat)
                                isCellValueChangedOpen = False
                                Throw New Exception("FAT value in Grid, must have its decimal part multiple of 5")
                            End If
                        End If
                    End If
                End If

                If e.Column Is gvParam.Columns(colCLR) Then
                    gvParam.Rows(e.RowIndex).Cells(colCLR).Value = Math.Truncate(clsCommon.myCdbl(gvParam.Rows(e.RowIndex).Cells(colCLR).Value) * 100) / 100
                End If
                ''------------------------

                If e.Column Is gvParam.Columns(colFat) Or e.Column Is gvParam.Columns(colCLR) Then
                    CalculateSNF()
                End If

                isCellValueChangedOpen = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Sub calculateSNFForParameter()
    '    Dim isParamOK As Boolean = True
    '    Dim snfField As String = ""
    '    Dim fatField As String = ""
    '    Dim clrField As String = ""
    '    Dim cfField As String = ""
    '    If gvParam.Rows.Count > 0 Then

    '        For i As Integer = 0 To gvParam.Columns.Count - 1
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
    '                If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(0).Cells(i).Value)) Then
    '                    isParamOK = False
    '                End If
    '            End If
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
    '                snfField = gvParam.Columns(i).Name
    '            End If
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
    '                fatField = gvParam.Columns(i).Name
    '            End If
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
    '                clrField = gvParam.Columns(i).Name
    '            End If
    '            If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
    '                cfField = gvParam.Columns(i).Name
    '            End If
    '        Next
    '        If isParamOK Then
    '            gvParam.Rows(0).Cells(snfField).Value = Math.Floor(clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(cfField).Value)) * 100) / 100
    '        Else
    '            gvParam.Rows(0).Cells(snfField).Value = 0
    '        End If
    '    End If
    'End Sub

    Private Function AllowToSave() As Boolean
        'If clsCommon.myLen(fndLoadingNo.Value) <= 0 Then
        '    fndLoadingNo.Focus()
        '    Throw New Exception("Loading No cannot be left blank")
        'End If
        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtQCdate.Value, Nothing) = False Then
            txtQCdate.Focus()
            txtQCdate.Select()
            Return False
        End If

        If clsCommon.myLen(FndTankerNo.Value) <= 0 Then
            FndTankerNo.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, LoadingTanker_Date,103) from TSPL_LOADING_TANKER_DETAIL_BULKSALE  where LoadingTanker_No ='" + LblLoadingNo.Text + "'")) > clsCommon.myCDate(txtQCdate.Value) Then
            txtQCdate.Focus()
            Throw New Exception("QC Date cannot be less than from Loading Tanker No Date")
        End If

        If clsCommon.myCdbl(txtCorrectionFactor.Value) < 0 Then
            txtCorrectionFactor.Focus()
            Throw New Exception("Correction Factor cannot be in negative")
        End If

        'For i As Integer = 0 To gvItem.Rows.Count - 1
        '    If clsCommon.myLen(gvItem.Rows(i).Cells(colFat).Value) <= 0 Then
        '        Throw New Exception("Fat cannot be left blank or zero")
        '    End If
        '    If clsCommon.myLen(gvItem.Rows(i).Cells(colCLR).Value) <= 0 Then
        '        Throw New Exception("CLR cannot be left blank or zero")
        '    End If
        'Next

        For i As Integer = 0 To gvParam.Rows.Count - 1

            If clsCommon.myCdbl(gvParam.Rows(i).Cells(colFat).Value) < 0 Then
                Throw New Exception("Fat cannot be negative")
            End If
            ''richa ERO/06/09/19-001021
            If Allow0FatPerOnBulkSaleQualityCheckScreen = False Then
                If clsCommon.myCdbl(gvParam.Rows(i).Cells(colFat).Value) = 0 Then
                    Throw New Exception("Fat cannot be left blank or zero")
                End If
            End If

            If Not clsERPFuncationality.isLocationMcc(lblLocationCode.Text) Then
                If BulkQCWithoutCLR = False Then
                    If clsCommon.myCdbl(gvParam.Rows(i).Cells(colCLR).Value) < 0 Then
                        Throw New Exception("CLR cannot be negative")
                    End If

                    If clsCommon.myCdbl(gvParam.Rows(i).Cells(colCLR).Value) = 0 Then
                        Throw New Exception("CLR cannot be left blank or zero")
                    End If
                End If
            End If
            If clsERPFuncationality.isLocationMcc(lblLocationCode.Text) Then
                If AllowSNFNotManditoryInBulkSale = False Then
                    If clsCommon.myCdbl(gvParam.Rows(i).Cells(colSNF).Value) < 0 Then
                        Throw New Exception("SNF cannot be negative")
                    End If
                    If clsCommon.myCdbl(gvParam.Rows(i).Cells(colSNF).Value) = 0 Then
                        Throw New Exception("SNF cannot be left blank or zero")
                    End If
                End If

            End If
        Next

        'For i As Integer = 0 To gvParam.Columns.Count - 1
        '    If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
        '        If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Then
        '            Throw New Exception("Please Fill " & gvParam.Columns(i).HeaderText & " , Because it is of type " & gvParam.Columns(i).Tag & " and it is Manadatory ")
        '        End If
        '        If Not IsNumeric(gvParam.Rows(0).Cells(i).Value) Then
        '            Throw New Exception("Field Name " & gvParam.Columns(i).HeaderText & "   is of type " & gvParam.Columns(i).Tag & " Which Must be Numeric ")
        '        End If
        '    End If
        'Next

        Return True
    End Function
    Private Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Try
            If (deleteConfirm()) Then
                arr.Add(fndQcNo.Value)
                If clsERPFuncationality.AddToHistory(arr, clsUserMgtCode.FrmQualityCheckBulkSale, Nothing) Then
                    If (ClsQualityCheckBulkSale.DeleteData(fndQcNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Data deleted successfully ")
                        Reset()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SaveData()
        Dim obj As New ClsQualityCheckBulkSale()
        Dim objParam As New clsQcParamBulkSale
        Try
            If AllowToSave() Then

                obj.QC_No = fndQcNo.Value
                obj.QC_Date = txtQCdate.Value
                obj.LoadingTanker_No = LblLoadingNo.Text
                obj.GateEntry_Document_No = lblGateEntryNo.Text
                obj.Weighment_No = LblWeighmentNo.Text
                'obj.Tanker_No = lblTankerNoValue.Text
                obj.Tanker_No = FndTankerNo.Value
                obj.Silo_No = lblSiloNo.Text
                obj.Location_Code = lblLocationCode.Text
                obj.Correction_Factor = txtCorrectionFactor.Value
                obj.Customer_Code = clsCommon.myCstr(lblCustomerCode.Text)
                obj.Remarks = clsCommon.myCstr(gvParam.Rows(0).Cells("colRemarks").Value)

                'obj.Item_Code = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value)
                '' obj.Qty = clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value)
                'obj.Qty = 0
                'obj.Unit_code = clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)
                'obj.Fat = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
                'obj.SNF = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)
                'obj.CLR = clsCommon.myCdbl(gvItem.Rows(0).Cells(colCLR).Value)
                obj.Item_Code = clsCommon.myCstr(gvParam.Rows(0).Cells(colItemCode).Value)
                obj.Qty = 0
                obj.Unit_code = clsCommon.myCstr(gvParam.Rows(0).Cells(colUOM).Value)
                obj.Fat = clsCommon.myCdbl(gvParam.Rows(0).Cells(colFat).Value)
                obj.SNF = clsCommon.myCdbl(gvParam.Rows(0).Cells(colSNF).Value)
                obj.CLR = clsCommon.myCdbl(gvParam.Rows(0).Cells(colCLR).Value)
                Dim i As Integer = 0
                Dim k As Integer = 0
                obj.arrQcParamDetail = New List(Of clsQcParamBulkSale)

                For i = 0 To gvParam.Columns.Count - 1
                    If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "ChamberDesc") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colSNF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colFAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colCLR") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "ItemCode") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "ItemDesc") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "UOM") = CompairStringResult.Equal Then
                    Else
                        For k = 0 To gvParam.Rows.Count - 1
                            objParam = New clsQcParamBulkSale
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                            objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                            objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                            objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(k).Cells(i).Value)
                            If ApplyMultiChamber Then
                                objParam.Chamber_Desc = clsCommon.myCstr(gvParam.Rows(k).Cells(colChamberDesc).Value)
                                objParam.Item_code = clsCommon.myCstr(gvParam.Rows(k).Cells(colItemCode).Value)
                                objParam.Unit_code = clsCommon.myCstr(gvParam.Rows(k).Cells(colUOM).Value)
                                objParam.Fat = clsCommon.myCdbl(gvParam.Rows(k).Cells(colFat).Value)
                                objParam.SNF = clsCommon.myCdbl(gvParam.Rows(k).Cells(colSNF).Value)
                                objParam.CLR = clsCommon.myCdbl(gvParam.Rows(k).Cells(colCLR).Value)
                                objParam.Remarks = clsCommon.myCstr(gvParam.Rows(k).Cells("colRemarks").Value)
                            End If
                            obj.arrQcParamDetail.Add(objParam)
                        Next
                    End If
                Next


                If (ClsQualityCheckBulkSale.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                        LoadData(obj.QC_No, NavigatorType.Current)

                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
            objParam = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsQualityCheckBulkSale = ClsQualityCheckBulkSale.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then

            isNewEntry = False
            fndQcNo.Value = obj.QC_No
            txtQCdate.Value = obj.QC_Date
            LblLoadingNo.Text = obj.LoadingTanker_No
            lblGateEntryNo.Text = obj.GateEntry_Document_No
            LblWeighmentNo.Text = obj.Weighment_No
            FndTankerNo.Value = obj.Tanker_No
            'lblTankerNoValue.Text = obj.Tanker_No
            ' LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
            'LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
            lblLocationCode.Text = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
            lblSiloNo.Text = obj.Silo_No
            LblSiloName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + lblSiloNo.Text + "' ")
            txtCorrectionFactor.Value = obj.Correction_Factor
            lblCustomerCode.Text = obj.Customer_Code
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'"))
            loadBlankParameterGrid()
            'gvItem.Rows(0).Cells(colSlNo).Value = "1"
            'gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
            'gvItem.Rows(0).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + obj.Item_Code + "' ")
            'gvItem.Rows(0).Cells(colUOM).Value = obj.Unit_code
            ''gvItem.Rows(0).Cells(colQty).Value = obj.Qty
            'gvItem.Rows(0).Cells(colFat).Value = obj.Fat
            'gvItem.Rows(0).Cells(colSNF).Value = obj.SNF
            'gvItem.Rows(0).Cells(colCLR).Value = obj.CLR
            If Not ApplyMultiChamber Then
                gvParam.Rows(0).Cells("colSLNO").Value = "1"
                gvParam.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvParam.Rows(0).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + obj.Item_Code + "' ")
                gvParam.Rows(0).Cells(colUOM).Value = obj.Unit_code
                gvParam.Rows(0).Cells(colFat).Value = obj.Fat
                gvParam.Rows(0).Cells(colSNF).Value = obj.SNF
                gvParam.Rows(0).Cells(colCLR).Value = obj.CLR
                gvParam.Rows(0).Cells("colRemarks").Value = obj.Remarks
                If obj.arrQcParamDetail IsNot Nothing Then
                    For i As Integer = 0 To obj.arrQcParamDetail.Count - 1
                        If clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "FAT") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "CLR") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "SNF") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "CF") <> CompairStringResult.Equal Then
                            If TypeOf (gvParam.Columns(obj.arrQcParamDetail(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(0).Cells(obj.arrQcParamDetail(i).Param_Field_Code).Value = clsCommon.myCdbl(obj.arrQcParamDetail(i).Param_Field_Value)
                            Else
                                gvParam.Rows(0).Cells(obj.arrQcParamDetail(i).Param_Field_Code).Value = obj.arrQcParamDetail(i).Param_Field_Value
                            End If
                        End If

                    Next
                    'gvParam.Rows(0).Cells("colRemarks").Value = obj.Remarks
                Else
                    'loadBlankParameterGrid()
                End If
            Else
                If obj.arrQcParamDetail IsNot Nothing Then
                    Dim arrChk As List(Of String) = New List(Of String)
                    For i As Integer = 0 To obj.arrQcParamDetail.Count - 1
                        If Not arrChk.Contains(obj.arrQcParamDetail(i).Item_code & obj.arrQcParamDetail(i).Chamber_Desc) Then
                            arrChk.Add(obj.arrQcParamDetail(i).Item_code & obj.arrQcParamDetail(i).Chamber_Desc)
                            gvParam.Rows.AddNew()
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells("colSLNO").Value = gvParam.Rows.Count
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colItemCode).Value = obj.arrQcParamDetail(i).Item_code
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colChamberDesc).Value = obj.arrQcParamDetail(i).Chamber_Desc
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + obj.arrQcParamDetail(i).Item_code + "' ")
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colUOM).Value = obj.arrQcParamDetail(i).Unit_code
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colFat).Value = obj.arrQcParamDetail(i).Fat
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colSNF).Value = obj.arrQcParamDetail(i).SNF
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells(colCLR).Value = obj.arrQcParamDetail(i).CLR
                            gvParam.Rows(gvParam.Rows.Count - 1).Cells("colRemarks").Value = obj.arrQcParamDetail(i).Remarks
                        End If
                        If clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "FAT") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "CLR") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "SNF") <> CompairStringResult.Equal And clsCommon.CompairString(obj.arrQcParamDetail(i).Param_Type, "CF") <> CompairStringResult.Equal Then
                            If TypeOf (gvParam.Columns(obj.arrQcParamDetail(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                gvParam.Rows(gvParam.Rows.Count - 1).Cells(obj.arrQcParamDetail(i).Param_Field_Code).Value = clsCommon.myCdbl(obj.arrQcParamDetail(i).Param_Field_Value)
                            Else
                                gvParam.Rows(gvParam.Rows.Count - 1).Cells(obj.arrQcParamDetail(i).Param_Field_Code).Value = obj.arrQcParamDetail(i).Param_Field_Value
                            End If
                        End If
                    Next
                End If
            End If


            fndQcNo.MyReadOnly = True
            btnsave.Text = "Update"
            ' btndelete.Enabled = True
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Else
            Reset()

        End If
        obj = Nothing
    End Sub

    Private Sub fndQcNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndQcNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_QUALITY_CHECK_BULKSALE where QC_No='" + fndQcNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndQcNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndQcNo.MyReadOnly = False
            End If

            LoadData(fndQcNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndQcNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndQcNo._MYValidating
        'Dim qry As String = "Select QC_No as Code,QC_Date from TSPL_QUALITY_CHECK_BULKSALE "
        ' Dim qry As String = "Select TSPL_QUALITY_CHECK_BULKSALE.QC_No as Code,Convert(varchar,TSPL_QUALITY_CHECK_BULKSALE.QC_Date,103) as Date,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No as [Loading No],TSPL_QUALITY_CHECK_BULKSALE.Weighment_No as [Weighment No],TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_QUALITY_CHECK_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Quality_Check_BulkSale.Tanker_No as [Tanker No],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_Quality_Check_BulkSale.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc] from TSPL_QUALITY_CHECK_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_QUALITY_CHECK_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  Left Outer Join TSPL_TANKER_MASTER  on TSPL_Quality_Check_BulkSale.Tanker_No=TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER SubLocationMaster on TSPL_Quality_Check_BulkSale.Silo_No=SubLocationMaster.Location_Code "
        Dim qry As String = "Select TSPL_QUALITY_CHECK_BULKSALE.QC_No as Code,Convert(varchar,TSPL_QUALITY_CHECK_BULKSALE.QC_Date,103) as Date,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No as [Loading No],TSPL_QUALITY_CHECK_BULKSALE.Weighment_No as [Weighment No],TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_QUALITY_CHECK_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Quality_Check_BulkSale.Customer_Code as [Customer No],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Quality_Check_BulkSale.Tanker_No as [Tanker No],TSPL_Quality_Check_BulkSale.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc] from TSPL_QUALITY_CHECK_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_QUALITY_CHECK_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER SubLocationMaster on TSPL_Quality_Check_BulkSale.Silo_No=SubLocationMaster.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Quality_Check_BulkSale.Customer_Code"
        fndQcNo.Value = clsCommon.ShowSelectForm("Quality Check", qry, "Code", " TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ")", fndQcNo.Value, "", isButtonClicked)
        LoadData(fndQcNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = ""
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsQualityCheckBulkSale.PostData(MyBase.Form_ID, arrLoc, fndQcNo.Value)) Then

                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndQcNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Dim obj As clsGridLayout = New clsGridLayout()
        Try
            'If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            '    Dim obj As clsGridLayout = New clsGridLayout()
            '    obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
            '    If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
            '        Dim ii As Integer
            '        For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
            '            gvItem.Columns(ii).IsVisible = False
            '            gvItem.Columns(ii).VisibleInColumnChooser = True
            '        Next
            '        gvItem.LoadLayout(obj.GridLayout)
            '        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            '    End If
            'End If
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

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
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub RDSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            'gvItem.MasterTemplate.FilterDescriptors.Clear()
            'Dim obj As New clsGridLayout()
            'obj.ReportID = MyBase.Form_ID & "gvItem"
            'obj.UserID = objCommonVar.CurrentUserCode
            'obj.GridLayout = New MemoryStream()
            'gvItem.SaveLayout(obj.GridLayout)
            'obj.GridColumns = gvItem.ColumnCount
            'obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            'If obj.SaveData() Then
            gvParam.MasterTemplate.FilterDescriptors.Clear()
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
            ' End If
        End If
    End Sub

    Private Sub RDDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDDeleteLayout.Click
        ' clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub gvParam_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gvParam.KeyPress
        Try
            If gvParam.CurrentColumn Is gvParam.Columns(colFat) Or gvParam.CurrentColumn Is gvParam.Columns(colCLR) Then
                If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FndTankerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndTankerNo._MYValidating
        Dim dt As DataTable = Nothing
        'Qry = "Select LoadingTanker_No as Code ,LoadingTanker_Date as [Loading Tanker Date],Weighment_No as [Weighment No]  from TSPL_LOADING_TANKER_DETAIL_BULKSALE  "
        'Qry = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as Code,Convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103)  as Date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE  Left Outer Join TSPL_TANKER_MASTER  on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code"
        'fndLoadingNo.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Posted=1 and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") and  LoadingTanker_No not in (Select LoadingTanker_No from TSPL_Quality_Check_BulkSale where QC_No<>'" + fndQcNo.Value + "' and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") ) ", fndLoadingNo.Value, "", isButtonClicked)
        LblLoadingNo.Text = ClsLoadingTanker.getTankerFinder("TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=1 and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") and  LoadingTanker_No not in (Select LoadingTanker_No from TSPL_Quality_Check_BulkSale where QC_No<>'" + fndQcNo.Value + "' and TSPL_QUALITY_CHECK_BULKSALE.Location_Code in (" + arrLoc + ") ) ", FndTankerNo.Value, isButtonClicked)
        If ApplyMultiChamber Then
            Qry = "Select max(TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No) as Weighment_No, max(TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No) as GateEntry_Document_No,max(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No) as Tanker_No,max(TSPL_TANKER_MASTER_SALE.Tanker_Code) as Tanker_Name,max(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc) as Chamber_Desc,max(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code) as Location_Code,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,max(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No) as Silo_No ,max(SubLocationMaster.Location_Desc) as SubLocationDesc,max(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Item_Code) as Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc ,max(TSPL_ITEM_MASTER.Unit_Code) as Unit_Code ,max(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity) as Quantity from TSPL_WEIGHMENT_DETAIL_BULKSALE " & _
             " Left Outer Join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE .Weighment_No " & _
             " Left Outer Join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE .Weighment_No " & _
             " Left Outer Join TSPL_TANKER_MASTER_SALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No=TSPL_TANKER_MASTER_SALE.Tanker_Code " & _
             " Left Outer Join TSPL_TANKER_MASTER_SALE_DETAIL on TSPL_TANKER_MASTER_SALE_DETAIL.Tanker_Code=TSPL_TANKER_MASTER_SALE.Tanker_Code " & _
             " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
             " Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No= SubLocationMaster.Location_Code " & _
             " Left Outer Join TSPL_ITEM_MASTER on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No='" + LblLoadingNo.Text + "' GROUP BY TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc"
        Else
            'Qry = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No, TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No,TSPL_TANKER_MASTER.Tanker_Name,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No ,SubLocationMaster.Location_Desc as SubLocationDesc,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity from TSPL_WEIGHMENT_DETAIL_BULKSALE " & _
            ' " Left Outer Join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE .Weighment_No " & _
            ' " Left Outer Join TSPL_TANKER_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No=TSPL_TANKER_MASTER.Tanker_No " & _
            ' " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
            ' " Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No= SubLocationMaster.Location_Code " & _
            ' " Left Outer Join TSPL_ITEM_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            ' " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No='" + LblLoadingNo.Text + "'"
            '' richa ERO/08/05/19-000597 show uom from sale oredr if sale order is created otherwise default uom of that item will be shown 
            Qry = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No, TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No,TSPL_TANKER_MASTER.Tanker_Name,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc, " & _
            " TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No ,SubLocationMaster.Location_Desc as SubLocationDesc,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code, " & _
            " TSPL_ITEM_MASTER.Item_Desc,case when isnull(TSPL_SALES_ORDER_DETAIL_BULKSALE.Unit_code,'')<>'' then TSPL_SALES_ORDER_DETAIL_BULKSALE.Unit_code else TSPL_ITEM_UOM_DETAIL.UOM_Code end as Unit_Code,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity from TSPL_WEIGHMENT_DETAIL_BULKSALE " & _
            " Left Outer Join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE .Weighment_No " & _
            " Left Outer Join TSPL_TANKER_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No=TSPL_TANKER_MASTER.Tanker_No " & _
            " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
            " Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No= SubLocationMaster.Location_Code " & _
            " Left Outer Join TSPL_ITEM_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            " left outer join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Document_No =TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No " & _
            " left outer join TSPL_SALES_ORDER_DETAIL_BULKSALE on TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No =TSPL_GATEENTRY_SALE.Bulk_SO_No  " & _
            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM =1 " & _
            " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No='" + LblLoadingNo.Text + "'"
        End If

        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then

            LblWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
            lblGateEntryNo.Text = clsCommon.myCstr(dt.Rows(0)("GateEntry_Document_No"))
            FndTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            ' lblTankerNoValue.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            ' LblTankerName.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_Name"))
            lblLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            LblLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            lblSiloNo.Text = clsCommon.myCstr(dt.Rows(0)("Silo_No"))
            LblSiloName.Text = clsCommon.myCstr(dt.Rows(0)("SubLocationDesc"))
            lblCustomerCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Code from TSPL_GATEENTRY_SALE where Document_No ='" & lblGateEntryNo.Text & "'"))
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'"))
            'gvItem.Rows(0).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            'gvItem.Rows(0).Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            'gvItem.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            'gvItem.Rows(0).Cells(colQty).Value = clsCommon.myCstr(dt.Rows(0)("Quantity"))
            loadBlankParameterGrid()
            If ApplyMultiChamber Then
                For Each dr As DataRow In dt.Rows
                    gvParam.Rows.AddNew()
                    gvParam.Rows(gvParam.Rows.Count - 1).Cells("colSlNo").Value = gvParam.Rows.Count
                    gvParam.Rows(gvParam.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gvParam.Rows(gvParam.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gvParam.Rows(gvParam.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("Chamber_Desc"))
                    gvParam.Rows(gvParam.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dr("Unit_Code"))

                Next
            Else
                gvParam.Rows(0).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                gvParam.Rows(0).Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                gvParam.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            End If

        Else

            LblLoadingNo.Text = ""
            lblGateEntryNo.Text = ""
            LblWeighmentNo.Text = ""
            'LblTankerName.Text = ""
            lblLocationCode.Text = ""
            LblLocationName.Text = ""
            'lblTankerNoValue.Text = ""
            FndTankerNo.Value = ""
            lblSiloNo.Text = ""
            LblSiloName.Text = ""
            'loadBlankItemGrid()
        End If
        dt = Nothing
    End Sub
End Class
