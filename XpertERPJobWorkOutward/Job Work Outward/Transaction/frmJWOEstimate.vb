Imports common
Imports System.Data.SqlClient

Public Class frmJWOEstimate
    Inherits XpertERPEngine.FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As New clsErrorControl()
    Const colWeightSNo As String = "colWeightSNo"
    Const colWeightParentSNo As String = "colWeightParentSNo"
    Const colTransferDocCode As String = "colTransferDocCode"
    Const colTransferDocDate As String = "colTransferDocDate"
    Const colWeightTankerNo As String = "colWeightTankerNo"
    Const colWeightItemCode As String = "colWeightItemCode"
    Const colWeightItemName As String = "colWeightItemName"
    Const colWeightQty As String = "colWeightQty"
    Const colWeightUOM As String = "colWeightUOM"
    Const colWeightFATPer As String = "colWeightFATPer"
    Const colWeightFATKg As String = "colWeightFATKg"
    Const colWeightFATKgEst As String = "colWeightFATKgEst"
    Const colWeightCLR As String = "colWeightCLR"
    Const colWeightCorrectionFactor As String = "colWeightCorrectionFactor"
    Const colWeightSNFPer As String = "colWeightSNFPer"
    Const colWeightSNFKG As String = "colWeightSNFKG"
    Const colWeightSNFKGEst As String = "colWeightSNFKGEst"


    Const colFATSNo As String = "colFATSNo"
    Const colFATTRDate As String = "colTRDate"
    Const colFATBatchNo As String = "colFATBatchNo"
    Const colFATItemCode As String = "colFATItemCode"
    Const colFATItemName As String = "colFATItemName"
    Const colFATBOMCode As String = "colFATBOMCode"
    Const colFATQty As String = "colFATQty"
    Const colFATQtyEst As String = "colFATQtyEst"
    Const colFATUOM As String = "colFATUOM"
    Const colFATFATPer As String = "colFATFATPer"
    Const colFATFATKg As String = "colFATFATKg"
    Const colFATFATKgEst As String = "colFATFATKgEst"


    Const colSNFSNo As String = "colSNFSNo"
    Const colSNFTRDate As String = "colTRDate"
    Const colSNFBatchNo As String = "colSNFBatchNo"
    Const colSNFItemCode As String = "colSNFItemCode"
    Const colSNFItemName As String = "colSNFItemName"
    Const colSNFBOMCode As String = "colSNFBOMCode"
    Const colSNFQty As String = "colSNFQty"
    Const colSNFQtyEst As String = "colSNFQtyEst"
    Const colSNFUOM As String = "colSNFUOM"
    Const colSNFSNFPer As String = "colSNFSNFPer"
    Const colSNFSNFKg As String = "colSNFFATKg"
    Const colSNFSNFKgEst As String = "colSNFFATKgEst"

    Const colRawSNo As String = "colRawSNo"
    Const colRawTRDate As String = "colRawTRDate"
    Const colRawParentSNo As String = "colRawParentSNo"
    Const colRawParentType As String = "colRawParentType"
    Const colRawBomCode As String = "colRawBomCode"
    Const colRawMainItem As String = "colRawMainItem"
    Const colRawMainItemName As String = "colRawMainItemName"
    Const colRawMainItemQty As String = "colMainItemQty"
    Const colRawMainUOM As String = "colRawMainUOM"
    Const colRawIcode As String = "colRawIcode"
    Const colRawIname As String = "colRawIname"
    Const colRawQty As String = "colRawQty"
    Const colRawUOM As String = "colRawUOM"
    Const colRawFatPer As String = "colRawFatPer"
    Const colRawFATKG As String = "colRawFATKG"
    Const colRawSNFPer As String = "colRawSNFPer"
    Const colRawSNFKG As String = "colRawSNFKG"
    Const colWeightGEFATPer As String = "colWeightGEFATPer"
    Const colWeightGESNFPer As String = "colWeightGESNFPer"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Private isNewEntry As Boolean = False

    Dim contQCParameterStartColumnInedx As Integer = 11
    Dim FATColName As String
    Dim SNFColName As String
    Dim DeleteParentSNoExist As Boolean
    Dim DeleteParentSNo As Integer
    Dim settSNFFromCLRAndCorrectionFactorInJWIEst As Boolean = False ''ERO/28/01/19-000477 by balwinder on 29/01/2019
    Dim settJOBdefaultCorrectionFactorBS As Decimal = 0
    Dim settAutoCalculateProduceQty As Boolean = False ''ERO/30/01/19-000479 by balwinder on 31/01/2019,ERO/11/03/19-000510 by balwinder on  14/03/2019
#End Region

    Private Sub FrmSRNJobWorkEstimate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        settSNFFromCLRAndCorrectionFactorInJWIEst = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFFromCLRAndCorrectionFactorInJWIEst, clsFixedParameterCode.SNFFromCLRAndCorrectionFactorInJWIEst, Nothing) = 1))
        settJOBdefaultCorrectionFactorBS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing))
        settAutoCalculateProduceQty = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoCalculateProduceQty, clsFixedParameterCode.AutoCalculateProduceQty, Nothing) = 1))
        SetUserMgmtNew()
        AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnGo.Visible = MyBase.isModifyFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadBlankGridWeighment()
        gvWeighment.Rows.Clear()
        gvWeighment.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colWeightSNo
        repoNumBox.Width = 50
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Parent SNo"
        repoNumBox.Name = colWeightParentSNo
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoTxtBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Weighment Code"
        repoTxtBox.Name = colTransferDocCode
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvWeighment.MasterTemplate.Columns.Add(repoTxtBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Weighment Date"
        repoDateBox.Name = colTransferDocDate
        repoDateBox.ReadOnly = True
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gvWeighment.MasterTemplate.Columns.Add(repoDateBox)


        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Tanker No"
        repoTxtBox.Name = colWeightTankerNo
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvWeighment.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Code"
        repoTxtBox.Name = colWeightItemCode
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvWeighment.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Name"
        repoTxtBox.Name = colWeightItemName
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvWeighment.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = colWeightQty
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "UOM"
        repoTxtBox.Name = colWeightUOM
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvWeighment.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = colWeightFATPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        '-===============Added by preeti Gupta Against ticket no[RA/ERO/15/10/19-000009]=======
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.HeaderText = "GE FAT %"
        repoNumBox.Name = colWeightGEFATPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)
        '======================================================================================

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = colWeightFATKg
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG Estimated"
        repoNumBox.Name = colWeightFATKgEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "CLR"
        repoNumBox.Name = colWeightCLR
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = Not settSNFFromCLRAndCorrectionFactorInJWIEst
        repoNumBox.IsVisible = settSNFFromCLRAndCorrectionFactorInJWIEst
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Correction Factor"
        repoNumBox.Name = colWeightCorrectionFactor
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = Not settSNFFromCLRAndCorrectionFactorInJWIEst
        repoNumBox.IsVisible = settSNFFromCLRAndCorrectionFactorInJWIEst
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = colWeightSNFPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = settSNFFromCLRAndCorrectionFactorInJWIEst
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        '=====================Added bypreeti Gupta Against ticket no[RA/ERO/15/10/19-000009]
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.HeaderText = "GE SNF %"
        repoNumBox.Name = colWeightGESNFPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)
        '=================================================================================

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = colWeightSNFKG
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG Estimated"
        repoNumBox.Name = colWeightSNFKGEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvWeighment.MasterTemplate.Columns.Add(repoNumBox)



        gvWeighment.AllowAddNewRow = False
        gvWeighment.ShowGroupPanel = False
        gvWeighment.AllowColumnReorder = True
        gvWeighment.AllowRowReorder = False
        gvWeighment.EnableSorting = False
        gvWeighment.AllowDeleteRow = True
        gvWeighment.EnableFiltering = False
        gvWeighment.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvWeighment.AllowColumnChooser = True
        gvWeighment.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadBlankGridFAT()
        If (Not clsfrmParameterMaster.isFATParmExist(True)) Then
            clsCommon.MyMessageBoxShow("FAT parameter For Production Does not exist. Please make it first")
            Exit Sub
        End If
        If Not clsfrmParameterMaster.isSNFParmExist(True) Then
            clsCommon.MyMessageBoxShow("SNF parameter For Production Does not exist. Please make it first")
            Exit Sub
        End If
        Dim whrCls As String = String.Empty = " where 1=1 and IsProduction=1 "

        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60

        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If

        gvFAT.Rows.Clear()
        gvFAT.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colFATSNo
        repoNumBox.Width = 50
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Date"
        repoDateBox.Name = colFATTRDate
        repoDateBox.ReadOnly = True
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gvFAT.MasterTemplate.Columns.Add(repoDateBox)

        Dim repoTxtBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Batch No"
        repoTxtBox.Name = colFATBatchNo
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = False
        gvFAT.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Code"
        repoTxtBox.Name = colFATItemCode
        repoTxtBox.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTxtBox.Width = 100
        gvFAT.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Name"
        repoTxtBox.Name = colFATItemName
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvFAT.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "BOM Code"
        repoTxtBox.Name = colFATBOMCode
        repoTxtBox.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTxtBox.Width = 100
        gvFAT.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = colFATQty
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty Estimated"
        repoNumBox.Name = colFATQtyEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "UOM"
        repoTxtBox.Name = colFATUOM
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvFAT.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = colFATFATPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = colFATFATKg
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG Estimated"
        repoNumBox.Name = colFATFATKgEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvFAT.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoComboColumn As GridViewComboBoxColumn

        contQCParameterStartColumnInedx = gvFAT.Columns.Count - 1
        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    FATColName = dt.Rows(i)("Code")
                    Continue For
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    'SNFColName = dt.Rows(i)("Code")
                    Continue For
                End If
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                    repoNumBox = New GridViewDecimalColumn()
                    repoNumBox.Name = dt.Rows(i)("Code")
                    repoNumBox.Width = 120
                    repoNumBox.FormatString = "{0:n3}"
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                        repoNumBox.FormatString = "{0:n2}"
                    End If
                    repoNumBox.DecimalPlaces = 3
                    repoNumBox.HeaderText = dt.Rows(i)("Description")
                    repoNumBox.Tag = dt.Rows(i)("Type")
                    repoNumBox.ReadOnly = False
                    gvFAT.MasterTemplate.Columns.Add(repoNumBox)
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
                    gvFAT.MasterTemplate.Columns.Add(repoComboColumn)
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
                    gvFAT.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTxtBox = New GridViewTextBoxColumn()
                    repoTxtBox.Name = dt.Rows(i)("Code")
                    repoTxtBox.Width = 120
                    repoTxtBox.HeaderText = dt.Rows(i)("Description")
                    repoTxtBox.Tag = dt.Rows(i)("Type")
                    repoTxtBox.ReadOnly = False
                    gvFAT.MasterTemplate.Columns.Add(repoNumBox)
                End If
            Next
        End If

        gvFAT.AllowAddNewRow = False
        gvFAT.ShowGroupPanel = False
        gvFAT.AllowColumnReorder = True
        gvFAT.AllowRowReorder = False
        gvFAT.EnableSorting = False
        gvFAT.AllowDeleteRow = False
        gvFAT.EnableFiltering = False
        gvFAT.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFAT.AllowColumnChooser = True

        gvFAT.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Function FillYesNoValue() As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Sub LoadBlankGridSNF()
        If (Not clsfrmParameterMaster.isFATParmExist(True)) Then
            clsCommon.MyMessageBoxShow("FAT parameter For Production Does not exist. Please make it first")
            Exit Sub
        End If
        If Not clsfrmParameterMaster.isSNFParmExist(True) Then
            clsCommon.MyMessageBoxShow("SNF parameter For Production Does not exist. Please make it first")
            Exit Sub
        End If
        Dim whrCls As String = String.Empty = " where 1=1 and IsProduction=1 "

        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60

        ''---------------------
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If

        gvSNF.Rows.Clear()
        gvSNF.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colSNFSNo
        repoNumBox.Width = 50
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Date"
        repoDateBox.Name = colSNFTRDate
        repoDateBox.ReadOnly = True
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gvSNF.MasterTemplate.Columns.Add(repoDateBox)


        Dim repoTxtBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Batch No"
        repoTxtBox.Name = colSNFBatchNo
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = False
        gvSNF.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Code"
        repoTxtBox.Name = colSNFItemCode
        repoTxtBox.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTxtBox.Width = 100
        gvSNF.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Item Name"
        repoTxtBox.Name = colSNFItemName
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvSNF.MasterTemplate.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "BOM Code"
        repoTxtBox.Name = colSNFBOMCode
        repoTxtBox.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoTxtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTxtBox.Width = 100
        gvSNF.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = colSNFQty
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty Estimated"
        repoNumBox.Name = colSNFQtyEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "UOM"
        repoTxtBox.Name = colSNFUOM
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        gvSNF.MasterTemplate.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = colSNFSNFPer
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = colSNFSNFKg
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG Estimated"
        repoNumBox.Name = colSNFSNFKgEst
        repoNumBox.Width = 80
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSNF.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoComboColumn As GridViewComboBoxColumn
        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    'FATColName = dt.Rows(i)("Code")
                    Continue For
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    SNFColName = dt.Rows(i)("Code")
                    Continue For
                End If
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                    repoNumBox = New GridViewDecimalColumn()
                    repoNumBox.Name = dt.Rows(i)("Code")
                    repoNumBox.Width = 120
                    repoNumBox.FormatString = "{0:n3}"
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                        repoNumBox.FormatString = "{0:n2}"
                    End If
                    repoNumBox.DecimalPlaces = 3
                    repoNumBox.HeaderText = dt.Rows(i)("Description")
                    repoNumBox.Tag = dt.Rows(i)("Type")
                    repoNumBox.ReadOnly = False
                    gvSNF.MasterTemplate.Columns.Add(repoNumBox)
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
                    gvSNF.MasterTemplate.Columns.Add(repoComboColumn)
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
                    gvSNF.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTxtBox = New GridViewTextBoxColumn()
                    repoTxtBox.Name = dt.Rows(i)("Code")
                    repoTxtBox.Width = 120
                    repoTxtBox.HeaderText = dt.Rows(i)("Description")
                    repoTxtBox.Tag = dt.Rows(i)("Type")
                    repoTxtBox.ReadOnly = False
                    gvSNF.MasterTemplate.Columns.Add(repoNumBox)
                End If
            Next
        End If

        gvSNF.AllowAddNewRow = False
        gvSNF.ShowGroupPanel = False
        gvSNF.AllowColumnReorder = False
        gvSNF.AllowRowReorder = False
        gvSNF.EnableSorting = False
        gvSNF.AllowDeleteRow = False
        gvSNF.EnableFiltering = False
        gvSNF.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFAT.AllowColumnChooser = True
        gvSNF.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadBlankGridConsumption()
        gvRawItem.Rows.Clear()
        gvRawItem.Columns.Clear()

        Dim repoNumBox As New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colRawSNo
        repoNumBox.Width = 50
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Parent SNo"
        repoNumBox.Name = colRawParentSNo
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Date"
        repoDateBox.Name = colRawTRDate
        repoDateBox.ReadOnly = True
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gvRawItem.MasterTemplate.Columns.Add(repoDateBox)

        Dim repoTxtBox As New GridViewTextBoxColumn
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Parent Type"
        repoTxtBox.Name = colRawParentType
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "BOM Code"
        repoTxtBox.Name = colRawBomCode
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Main Item Code"
        repoTxtBox.Name = colRawMainItem
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Main Item Desc"
        repoTxtBox.Name = colRawMainItemName
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.Name = colRawMainItemQty
        repoNumBox.Width = 100
        repoNumBox.HeaderText = "Main Item Qty"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        gvRawItem.MasterTemplate.Columns.Add(repoNumBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Main UOM"
        repoTxtBox.Name = colRawMainUOM
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Consume Item Code"
        repoTxtBox.Name = colRawIcode
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Consume Item Desc"
        repoTxtBox.Name = colRawIname
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvRawItem.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.Name = colRawQty
        repoNumBox.Width = 100
        repoNumBox.HeaderText = "Consume Qty"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        gvRawItem.MasterTemplate.Columns.Add(repoNumBox)

        repoTxtBox = New GridViewTextBoxColumn()
        repoTxtBox.FormatString = ""
        repoTxtBox.HeaderText = "Consume UOM"
        repoTxtBox.Name = colRawUOM
        repoTxtBox.Width = 100
        repoTxtBox.ReadOnly = True
        repoTxtBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoTxtBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.Name = colRawFatPer
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.Name = colRawFATKG
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.Name = colRawSNFPer
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.DecimalPlaces = 2
        repoNumBox.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repoNumBox.Name = colRawSNFKG
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRawItem.Columns.Add(repoNumBox)

        gvRawItem.AllowDeleteRow = False
        gvRawItem.AllowAddNewRow = False
        gvRawItem.ShowGroupPanel = False
        gvRawItem.AllowColumnReorder = False
        gvFAT.AllowColumnChooser = True
        gvRawItem.AllowRowReorder = False
        gvRawItem.EnableSorting = False
        gvRawItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvRawItem.MasterTemplate.ShowRowHeaderColumn = False
        gvRawItem.EnableFiltering = False
    End Sub

    Private Function LoadCombobox() As DataTable
        Dim qry As String = "select a.Code,a.Name from (select 'Yes' as code,'Yes' as Name union all select 'No' as code,'No' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub txtJobWorkLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code as [Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],TSPL_LOCATION_MASTER.Jobwork_Vendor as [Vendor Code], tspl_vendor_master.Vendor_Name as [Vendor Name] from TSPL_LOCATION_MASTER left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_LOCATION_MASTER.Jobwork_Vendor"
        Dim whrclas As String = " TSPL_LOCATION_MASTER.Is_Jobwork=1"
        txtLocation.Value = clsCommon.ShowSelectForm("Jobwork@Finder", qry, "Location Code", "TSPL_LOCATION_MASTER.is_JobWork = 1 ", txtLocation.Value, "", isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where TSPL_LOCATION_MASTER.Location_Code='" + txtLocation.Value + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location Desc"))
            lblVendorCode.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
        Else
            lblLocation.Text = ""
            lblVendorCode.Text = ""
            lblVendorName.Text = ""
        End If
        LoadBlankGridWeighment()
        LoadBlankGridFAT()
        LoadBlankGridSNF()
        LoadBlankGridConsumption()
        FillFormula()
    End Sub

    Private Sub txtItemStructure__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemStructureFAT._MYValidating
        If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.myLen(lblVendorCode.Text) > 0 Then
            Dim qry As String = "select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
            txtItemStructureFAT.Value = clsCommon.ShowSelectForm("IMStruCode", qry, "Code", "", txtItemStructureFAT.Value, "", isButtonClicked)
            lblItemStructureFAT.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtItemStructureFAT.Value + "'"))
            qry = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " &
                                " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " &
                                " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode " &
                                "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureFAT.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103)  " &
                                "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate desc "
            ' "  order by convert (datetime, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) desc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblFormulaCodeFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Code"))
                lblFormulaNameFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Desc"))
                lblFormulaDateFAT.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
                lblFormulaFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula"))
            Else
                lblFormulaCodeFAT.Text = ""
                lblFormulaNameFAT.Text = ""
                lblFormulaDateFAT.Text = ""
                lblFormulaFAT.Text = ""
            End If

            LoadBlankGridWeighment()
            LoadBlankGridFAT()
            LoadBlankGridSNF()
            LoadBlankGridConsumption()
        Else
            clsCommon.MyMessageBoxShow("First Select Location.", Me.Text)
        End If
    End Sub

    Private Sub txtItemStructureSNF__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemStructureSNF._MYValidating
        If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.myLen(lblVendorCode.Text) > 0 Then
            Dim qry As String = "select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
            txtItemStructureSNF.Value = clsCommon.ShowSelectForm("IMStruCode", qry, "Code", "", txtItemStructureSNF.Value, "", isButtonClicked)
            lblItemStructureSNF.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtItemStructureSNF.Value + "'"))
            qry = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " &
                                " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " &
                                " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode " &
                                "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureSNF.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103) " &
                                "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate desc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblFormulaCodeSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Code"))
                lblFormulaNameSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Desc"))
                lblFormulaDateSNF.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
                lblFormulaSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula"))
            Else
                lblFormulaCodeSNF.Text = ""
                lblFormulaNameSNF.Text = ""
                lblFormulaDateSNF.Text = ""
                lblFormulaSNF.Text = ""
            End If
            LoadBlankGridWeighment()
            LoadBlankGridFAT()
            LoadBlankGridSNF()
            LoadBlankGridConsumption()
        Else
            clsCommon.MyMessageBoxShow("First Select Location.", Me.Text)
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Sub AddNew()
        GroupBox92.Visible = False
        txtDocumentNo.Value = ""
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtDocumentDate.Value
        txtFromDate.Value = txtDocumentDate.Value
        ucStatus.Status = ERPTransactionStatus.Pending
        txtLocation.Value = ""
        lblLocation.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblVendorName.Text = ""
        isNewEntry = True
        txtItemStructureFAT.Value = ""
        lblItemStructureFAT.Text = ""
        lblFormulaCodeFAT.Text = ""
        lblFormulaNameFAT.Text = ""
        lblFormulaDateFAT.Text = ""
        lblFormulaFAT.Text = ""

        txtItemStructureSNF.Value = ""
        lblItemStructureSNF.Text = ""
        lblFormulaCodeSNF.Text = ""
        lblFormulaNameSNF.Text = ""
        lblFormulaDateSNF.Text = ""
        lblFormulaSNF.Text = ""
        lblQtyWeighment.Text = ""
        lblFATKGWeighment.Text = ""
        lblSNFKGWeighment.Text = ""
        lblFATKGWeighmentEst.Text = ""
        lblSNFKGWeighmentEst.Text = ""
        lblFATKGRawItem.Text = ""
        lblSNFKGRawItem.Text = ""
        btnSave.Text = "Save"
        txtLocation.Enabled = True
        LoadBlankGridWeighment()
        LoadBlankGridFAT()
        LoadBlankGridSNF()
        LoadBlankGridConsumption()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
        btnPrint.Visible = False


    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadBlankGridWeighment()
            LoadBlankGridFAT()
            LoadBlankGridSNF()
            LoadBlankGridConsumption()
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select locatioin")
            End If
            Dim BaseQry As String = "select   TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_snf_Per as GE_SNF_Per,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_fat_per as GE_FAT_Per ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_Net_Weight as Net_Weight,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.UOM " + Environment.NewLine +
"from TSPL_MILK_JOBWORK_TRANSFER_DETAILS " + Environment.NewLine +
"left join TSPL_MILK_JOBWORK_TRANSFER_HEAD on TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code =TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Document_Code " + Environment.NewLine +
"left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Item_Code" + Environment.NewLine +
"where 2=2 and TSPL_MILK_JOBWORK_TRANSFER_HEAD.isPosted=1 and TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location='" + txtLocation.Value + "' and  " + Environment.NewLine +
"not exists (select 1 from TSPL_JWO_ESTIMATION_TRANSFER where TSPL_JWO_ESTIMATION_TRANSFER.Transfer_Code=TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code and TSPL_JWO_ESTIMATION_TRANSFER.Document_NO not in ('" + txtDocumentNo.Value + "')) " + Environment.NewLine +
"and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"

            '"select Tspl_Gate_Entry_Details.snf_Per as GE_SNF_Per,Tspl_Gate_Entry_Details.fat_per as GE_FAT_Per , Weighment_No,Transfer_Date,Vehicle_No_Manual,TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code,tspl_item_master.Item_Desc, Net_Weight,'KG' as UOM from TSPL_GENERAL_WEIGHMENT_DETAIL " + Environment.NewLine +
            '"left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code" + Environment.NewLine +
            '"left join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No " + Environment.NewLine +
            '"where 2=2 and TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=1 and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork=1 and TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code='" + txtLocation.Value + "' and TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null" + Environment.NewLine +
            '"and not exists (select 1 from TSPL_JWO_ESTIMATION_TRANSFER where TSPL_JWO_ESTIMATION_TRANSFER.Transfer_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No and TSPL_JWO_ESTIMATION_TRANSFER.Document_NO not in ('" + txtDocumentNo.Value + "')) and Transfer_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Transfer_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine


            Dim qry As String = "select * from (select Item_Code,max(Item_Desc) as Item_Desc from  ( " + BaseQry + ")x group by Item_Code)xx"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim strICode As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count > 1 Then
                    strICode = clsCommon.ShowSelectForm("Item@JWOE", qry, "Item_Code", "", "", "", True)
                    If clsCommon.myLen(strICode) <= 0 Then
                        Exit Sub
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If

            qry = BaseQry
            If clsCommon.myLen(strICode) > 0 Then
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Item_Code='" + strICode + "'"
            End If
            qry += " order by TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date"

            dt = clsDBFuncationality.GetDataTable(qry)
            Dim isHighClassVendor As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when isnull(IsHighClass,0)=0 then 'N' else 'Y' end from tspl_vendor_master where Vendor_code='" & lblVendorCode.Text & "' "))

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim jj As Integer = 0
                Dim dtPrevious As Date = clsCommon.myCDate(dt.Rows(0)("Document_Date")).AddDays(-1)
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim dtDocDate As Date = clsCommon.myCDate(dt.Rows(ii)("Document_Date"))
                    If Not (dtDocDate.Year = dtPrevious.Year AndAlso dtDocDate.Month = dtPrevious.Month AndAlso dtDocDate.Day = dtPrevious.Day) Then
                        dtPrevious = dtDocDate
                        jj += 1

                        gvFAT.Rows.AddNew()
                        gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATSNo).Value = jj
                        gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATTRDate).Value = dtDocDate

                        gvSNF.Rows.AddNew()
                        gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFSNo).Value = jj
                        gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFTRDate).Value = dtDocDate
                        Try
                            gvSNF.Rows(gvSNF.Rows.Count - 1).Cells("MOISTURE").Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select isnull (TSPL_JWO_FORMULA_DETAILS.Value,0) as Value from TSPL_JWO_FORMULA_DETAILS Left Outer Join TSPL_JW_Parameter_MASTER on TSPL_JW_Parameter_MASTER.Code =TSPL_JWO_FORMULA_DETAILS.Parameter_Code where TSPL_JWO_FORMULA_DETAILS.code = '" + lblFormulaCodeSNF.Text + "' and IS_MOISTURE =1  "))
                        Catch ex As Exception
                        End Try

                    End If
                    gvWeighment.Rows.AddNew()
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNo).Value = ii + 1
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightParentSNo).Value = jj
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTransferDocCode).Value = clsCommon.myCstr(dt.Rows(ii)("Document_Code"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTransferDocDate).Value = clsCommon.myCDate(dt.Rows(ii)("Document_Date"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightTankerNo).Value = clsCommon.myCstr(dt.Rows(ii)("Tanker_No"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightItemCode).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightItemName).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Desc"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightQty).Value = clsCommon.myCdbl(dt.Rows(ii)("Net_Weight"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightUOM).Value = clsCommon.myCstr(dt.Rows(ii)("UOM"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightFATPer).Value = clsCommon.myCdbl(dt.Rows(ii)("GE_FAT_Per"))
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNFPer).Value = clsCommon.myCdbl(dt.Rows(ii)("GE_SNF_Per"))
                    calculateWeighmentFATSNFKG(gvWeighment.Rows.Count - 1)
                    If clsCommon.CompairString(isHighClassVendor, "Y") = CompairStringResult.Equal Then
                        gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightGEFATPer).Value = clsCommon.myCstr(dt.Rows(ii)("GE_FAT_Per"))
                        gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightGESNFPer).Value = clsCommon.myCstr(dt.Rows(ii)("GE_SNF_Per"))
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvWeighment_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvWeighment.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If (e.Column Is gvWeighment.Columns(colWeightFATPer) Or e.Column Is gvWeighment.Columns(colWeightGEFATPer)) Then
                    calculateWeighmentFATSNFKG(gvWeighment.CurrentRow.Index)
                ElseIf (e.Column Is gvWeighment.Columns(colWeightSNFPer) Or e.Column Is gvWeighment.Columns(colWeightGESNFPer)) Then
                    calculateWeighmentFATSNFKG(gvWeighment.CurrentRow.Index)
                ElseIf (e.Column Is gvWeighment.Columns(colWeightCLR)) Then
                    calculateWeighmentFATSNFKG(gvWeighment.CurrentRow.Index)
                ElseIf (e.Column Is gvWeighment.Columns(colWeightCorrectionFactor)) Then
                    calculateWeighmentFATSNFKG(gvWeighment.CurrentRow.Index)
                End If
                isCellValueChanged = False
            End If
        End If
    End Sub

    Private Sub gvWeighment_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvWeighment.UserDeletedRow
        Try
            If Not DeleteParentSNoExist Then
                gvFAT.Rows.RemoveAt(DeleteParentSNo - 1)
                gvSNF.Rows.RemoveAt(DeleteParentSNo - 1)
                For ii As Integer = 0 To gvFAT.Rows.Count - 1
                    gvFAT.Rows(ii).Cells(colFATSNo).Value = ii + 1
                Next
                For ii As Integer = 0 To gvSNF.Rows.Count - 1
                    gvSNF.Rows(ii).Cells(colSNFSNo).Value = ii + 1
                Next
            End If
            For ii As Integer = 0 To gvWeighment.Rows.Count - 1
                gvWeighment.Rows(ii).Cells(colWeightSNo).Value = ii + 1
                If Not DeleteParentSNoExist Then
                    If clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value) > DeleteParentSNo Then
                        gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value) - 1
                    End If
                End If
                calculateWeighmentFATSNFKG(ii)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvWeighment_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvWeighment.UserDeletingRow
        DeleteParentSNoExist = False
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
        DeleteParentSNo = clsCommon.myCdbl(gvWeighment.CurrentRow.Cells(colWeightParentSNo).Value)
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            If ii = gvWeighment.CurrentRow.Index Then
                Continue For
            End If
            If clsCommon.myCdbl(gvWeighment.CurrentRow.Cells(colWeightParentSNo).Value) = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value) Then
                DeleteParentSNoExist = True
                Exit For
            End If
        Next
    End Sub

    Sub calculateWeighmentFATSNFKG(ByVal intRowIndex As Integer)
        Dim isHighClassVendor As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when isnull(IsHighClass,0)=0 then 'N' else 'Y' end from tspl_vendor_master where Vendor_code='" & lblVendorCode.Text & "' "))
        If settSNFFromCLRAndCorrectionFactorInJWIEst Then
            If clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightCorrectionFactor).Value) = 0 Then
                gvWeighment.Rows(intRowIndex).Cells(colWeightCorrectionFactor).Value = settJOBdefaultCorrectionFactorBS
            End If
            gvWeighment.Rows(intRowIndex).Cells(colWeightSNFPer).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATPer).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightCLR).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightCorrectionFactor).Value))
        End If
        If clsCommon.CompairString(isHighClassVendor, "Y") = CompairStringResult.Equal Then
            gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value = Math.Round(clsBOM.GetFatSNFKG_AfterConversion(gvWeighment.Rows(intRowIndex).Cells(colWeightItemCode).Value, gvWeighment.Rows(intRowIndex).Cells(colWeightUOM).Value, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightQty).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightGEFATPer).Value), Nothing), 2, MidpointRounding.ToEven)
            gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value = Math.Round(clsBOM.GetFatSNFKG_AfterConversion(gvWeighment.Rows(intRowIndex).Cells(colWeightItemCode).Value, gvWeighment.Rows(intRowIndex).Cells(colWeightUOM).Value, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightQty).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightGESNFPer).Value), Nothing), 2, MidpointRounding.ToEven)
        Else
            gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value = Math.Round(clsBOM.GetFatSNFKG_AfterConversion(gvWeighment.Rows(intRowIndex).Cells(colWeightItemCode).Value, gvWeighment.Rows(intRowIndex).Cells(colWeightUOM).Value, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightQty).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATPer).Value), Nothing), 2, MidpointRounding.ToEven) ''ERO/20/06/19-000651 by balwinder on 01/07/2018
            gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value = Math.Round(clsBOM.GetFatSNFKG_AfterConversion(gvWeighment.Rows(intRowIndex).Cells(colWeightItemCode).Value, gvWeighment.Rows(intRowIndex).Cells(colWeightUOM).Value, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightQty).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFPer).Value), Nothing), 2, MidpointRounding.ToEven)
        End If

        Dim objJobEst As clsJWOFormulaSoln = clsJWOFormulaSoln.CalculateFormula(lblFormulaCodeFAT.Text, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value), Nothing)
        If objJobEst IsNot Nothing Then
            If objJobEst.Type = 1 Then
                gvWeighment.Rows(intRowIndex).Cells(colWeightFATKgEst).Value = clsCommon.myFormat(Math.Round(objJobEst.EstFATKG, 2, MidpointRounding.ToEven))
                If Math.Abs(clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value) - clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATKgEst).Value)) <= 0.02 Then
                    gvWeighment.Rows(intRowIndex).Cells(colWeightFATKgEst).Value = clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value)
                End If

            End If
        End If
        objJobEst = clsJWOFormulaSoln.CalculateFormula(lblFormulaCodeSNF.Text, clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightFATKg).Value), clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value), Nothing)
        If objJobEst IsNot Nothing Then
            If objJobEst.Type = 2 Then
                gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKGEst).Value = clsCommon.myFormat(Math.Round(objJobEst.EstSNFKG, 2, MidpointRounding.ToEven))
                If Math.Abs(clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value) - clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKGEst).Value)) <= 0.02 Then
                    gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKGEst).Value = clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightSNFKG).Value)
                End If
            End If
        End If
        calculateFAT(clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightParentSNo).Value))
        calculateSNF(clsCommon.myCdbl(gvWeighment.Rows(intRowIndex).Cells(colWeightParentSNo).Value))
    End Sub

    Sub calculateFAT(ByVal intParentSNo As Integer)
        Dim totalFATKG As Decimal = 0
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            If clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value) = intParentSNo Then
                totalFATKG += clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightFATKg).Value)
            End If
        Next
        Dim arrQC As New Dictionary(Of String, String)
        Dim RowIndex As Integer = -1
        For ii As Integer = 0 To gvFAT.Rows.Count - 1
            If clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATSNo).Value) = intParentSNo Then
                RowIndex = ii
                Continue For
            End If
        Next

        If RowIndex >= 0 Then
            For i As Integer = contQCParameterStartColumnInedx To gvFAT.Columns.Count - 1
                arrQC.Add(gvFAT.Columns(i).Name, clsCommon.myCstr(gvFAT.Rows(RowIndex).Cells(i).Value))
            Next
            If clsCommon.myLen(FATColName) > 0 Then
                arrQC.Add(FATColName, clsCommon.myCstr(gvFAT.Rows(RowIndex).Cells(colFATFATPer).Value))
            End If
            Dim objJobEst As clsJWOFormulaSoln = clsJWOFormulaSoln.CalculateFormula(lblFormulaCodeFAT.Text, totalFATKG, 0, arrQC)
            If objJobEst IsNot Nothing Then
                If objJobEst.Type = 1 Then
                    gvFAT.Rows(RowIndex).Cells(colFATFATKgEst).Value = clsCommon.myFormat(Math.Round(objJobEst.EstFATKG, 2, MidpointRounding.ToEven))
                    If Math.Abs(clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATFATKgEst).Value) - totalFATKG) <= 0.02 Then
                        gvFAT.Rows(RowIndex).Cells(colFATFATKgEst).Value = totalFATKG
                    End If
                End If
            End If

            Dim dblOneUnitFATKg As Decimal = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvFAT.Rows(RowIndex).Cells(colFATItemCode).Value), clsCommon.myCstr(gvFAT.Rows(RowIndex).Cells(colFATUOM).Value), 1, clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATFATPer).Value), Nothing)
            If dblOneUnitFATKg > 0 Then
                gvFAT.Rows(RowIndex).Cells(colFATQtyEst).Value = Math.Round(clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATFATKgEst).Value) / dblOneUnitFATKg, 2, MidpointRounding.ToEven)
            End If

            If settAutoCalculateProduceQty Then
                Try
                    gvFAT.Rows(RowIndex).Cells(colFATQty).Value = Math.Round(clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATQtyEst).Value) * clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATFATKg).Value) / clsCommon.myCdbl(gvFAT.Rows(RowIndex).Cells(colFATFATKgEst).Value), 2, MidpointRounding.AwayFromZero)
                Catch ex As Exception
                End Try
            End If
        End If
        calculateAll()
    End Sub

    Sub calculateSNF(ByVal intParentSNo As Integer)
        Dim totalSNFKG As Decimal = 0
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            If clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value) = intParentSNo Then
                totalSNFKG += clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNFKG).Value)
            End If
        Next
        Dim arrQC As New Dictionary(Of String, String)
        Dim RowIndex As Integer = -1
        For ii As Integer = 0 To gvSNF.Rows.Count - 1
            If clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNo).Value) = intParentSNo Then
                RowIndex = ii
                Continue For
            End If
        Next

        If RowIndex >= 0 Then
            For i As Integer = contQCParameterStartColumnInedx To gvSNF.Columns.Count - 1
                arrQC.Add(gvSNF.Columns(i).Name, clsCommon.myCstr(gvSNF.Rows(RowIndex).Cells(i).Value))
            Next
            If clsCommon.myLen(SNFColName) > 0 Then
                arrQC.Add(SNFColName, clsCommon.myCstr(gvSNF.Rows(RowIndex).Cells(colSNFSNFPer).Value))
            End If
            Dim objJobEst As clsJWOFormulaSoln = clsJWOFormulaSoln.CalculateFormula(lblFormulaCodeSNF.Text, 0, totalSNFKG, arrQC)
            If objJobEst IsNot Nothing Then
                If objJobEst.Type = 2 Then
                    gvSNF.Rows(RowIndex).Cells(colSNFSNFKgEst).Value = clsCommon.myFormat(Math.Round(objJobEst.EstSNFKG, 2, MidpointRounding.ToEven))
                    If Math.Abs(clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFSNFKgEst).Value) - totalSNFKG) <= 0.02 Then
                        gvSNF.Rows(RowIndex).Cells(colSNFSNFKgEst).Value = totalSNFKG
                    End If
                End If
            End If

            Dim dblOneUnitSNFKg As Decimal = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvSNF.Rows(RowIndex).Cells(colSNFItemCode).Value), clsCommon.myCstr(gvSNF.Rows(RowIndex).Cells(colSNFUOM).Value), 1, clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFSNFPer).Value), Nothing)
            If dblOneUnitSNFKg > 0 Then
                gvSNF.Rows(RowIndex).Cells(colSNFQtyEst).Value = Math.Round(clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFSNFKgEst).Value) / dblOneUnitSNFKg, 2, MidpointRounding.ToEven)
            End If

            If settAutoCalculateProduceQty Then
                Try
                    gvSNF.Rows(RowIndex).Cells(colSNFQty).Value = Math.Round(clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFQtyEst).Value) * clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFSNFKg).Value) / clsCommon.myCdbl(gvSNF.Rows(RowIndex).Cells(colSNFSNFKgEst).Value), 2, MidpointRounding.AwayFromZero)
                Catch ex As Exception
                End Try
            End If
        End If
        calculateAll()
    End Sub

    Sub calculateAll()
        Dim dclQty As Decimal = 0
        Dim dclFATKg As Decimal = 0
        Dim dclSNFKg As Decimal = 0
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            dclQty += clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightQty).Value)
            dclFATKg += clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightFATKg).Value)
            dclSNFKg += clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNFKG).Value)
        Next
        lblQtyWeighment.Text = clsCommon.myFormat(dclQty)
        lblFATKGWeighment.Text = clsCommon.myFormat(dclFATKg)
        lblSNFKGWeighment.Text = clsCommon.myFormat(dclSNFKg)

        dclFATKg = 0
        dclSNFKg = 0
        For ii As Integer = 0 To gvFAT.Rows.Count - 1
            dclFATKg += clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATFATKgEst).Value)
        Next
        lblFATKGWeighmentEst.Text = clsCommon.myFormat(dclFATKg)
        For ii As Integer = 0 To gvSNF.Rows.Count - 1
            dclSNFKg += clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNFKgEst).Value)
        Next
        lblSNFKGWeighmentEst.Text = clsCommon.myFormat(dclSNFKg)

        dclFATKg = 0
        dclSNFKg = 0
        For ii As Integer = 0 To gvRawItem.Rows.Count - 1
            dclFATKg += clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawFATKG).Value)
            dclSNFKg += clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawSNFKG).Value)
        Next
        lblFATKGRawItem.Text = clsCommon.myFormat(dclFATKg)
        lblSNFKGRawItem.Text = clsCommon.myFormat(dclSNFKg)
    End Sub

    Private Sub gvFAT_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvFAT.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If (e.Column Is gvFAT.Columns(colFATBOMCode)) OrElse (e.Column Is gvFAT.Columns(colFATQty)) OrElse (e.Column Is gvFAT.Columns(colFATUOM)) OrElse (e.Column Is gvFAT.Columns(colFATItemCode)) Then
                    If (e.Column Is gvFAT.Columns(colFATBOMCode)) Then
                        OpenFATBOMCode(False)
                    ElseIf (e.Column Is gvFAT.Columns(colFATItemCode)) Then
                        OpenFATICode(False)
                    ElseIf (e.Column Is gvFAT.Columns(colFATUOM)) Then
                        OpenFATUOM(False)
                    End If
                    FillRawItemGridFromBOM()
                End If
                calculateFAT(clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATSNo).Value))
                isCellValueChanged = False
            End If
        End If
    End Sub

    Sub OpenFATBOMCode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""
        icode = clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value)
        If clsCommon.myLen(icode) > 0 Then
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)=1 and TSPL_PP_BOM_HEAD.Vendor_Code='" + lblVendorCode.Text + "' and TSPL_PP_BOM_HEAD.prod_item_code='" + icode + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txtItemStructureFAT.Value + "' and tspl_item_master.Structure_Code='" + txtItemStructureFAT.Value + "' and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' "
        Else
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)=1 and TSPL_PP_BOM_HEAD.Vendor_Code='" + lblVendorCode.Text + "' and tspl_item_master.Structure_Code='" + txtItemStructureFAT.Value + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txtItemStructureFAT.Value + "' and tspl_item_master.item_type in ('F','S') and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' "
        End If
        Dim oldbomcode As String = clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATBOMCode).Value)
        bomcode = clsBOM.GetBOMFinderWithValidityCheck(whrCls, oldbomcode, txtDocumentDate.Value, isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select prod_item_unit_code,prod_quantity,prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvFAT.CurrentRow.Cells(colFATBOMCode).Value = bomcode
            gvFAT.CurrentRow.Cells(colFATUOM).Value = clsCommon.myCstr(dt.Rows(0)("prod_item_unit_code"))
            If clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATQty).Value) <= 0 Then
                gvFAT.CurrentRow.Cells(colFATQty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("prod_quantity")), 2)
            End If
            If clsCommon.myLen(icode) <= 0 Then
                gvFAT.CurrentRow.Cells(colFATItemCode).Value = clsCommon.myCstr(dt.Rows(0)("prod_item_code"))
                gvFAT.CurrentRow.Cells(colFATItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dt.Rows(0)("prod_item_code")), Nothing)
                gvFAT.CurrentRow.Cells(colFATFATPer).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvFAT.CurrentRow.Cells(colFATItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
                gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value), clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATUOM).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATQty).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATPer).Value), Nothing)

                gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATKgEst).Value)
            Else
                gvFAT.CurrentRow.Cells(colFATFATPer).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value))
            End If
        Else
            gvFAT.CurrentRow.Cells(colFATBOMCode).Value = ""
            If clsCommon.myLen(icode) <= 0 Then
                gvFAT.CurrentRow.Cells(colFATItemCode).Value = ""
                gvFAT.CurrentRow.Cells(colFATItemName).Value = ""
                gvFAT.CurrentRow.Cells(colFATUOM).Value = ""
                gvFAT.CurrentRow.Cells(colFATQty).Value = 0
            End If
        End If
    End Sub

    Sub OpenFATICode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATBOMCode).Value)
        If clsCommon.myLen(bomcode) > 0 Then
            Dim qry As String = "select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"
            whrCls = " tspl_item_master.item_code='" + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)) + "' and tspl_item_master.Structure_Code='" + txtItemStructureFAT.Value + "' and tspl_item_master.Active='1' "
        Else
            whrCls = " tspl_item_master.item_type in ('F','S') and tspl_item_master.Structure_Code='" + txtItemStructureFAT.Value + "' and tspl_item_master.Active='1' "
        End If
        icode = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value), isButtonClicked)
        If clsCommon.myLen(icode) > 0 Then
            gvFAT.CurrentRow.Cells(colFATItemCode).Value = icode
            gvFAT.CurrentRow.Cells(colFATItemName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
            gvFAT.CurrentRow.Cells(colFATUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            whrCls = "select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='FAT'"
            gvFAT.CurrentRow.Cells(colFATFATPer).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(whrCls)), 2)
            gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value), clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATUOM).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATQty).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATPer).Value), Nothing)
            '=============By Prabhakar ==============
            gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATKgEst).Value)
        Else
            gvFAT.CurrentRow.Cells(colFATItemCode).Value = ""
            gvFAT.CurrentRow.Cells(colFATItemName).Value = ""
            gvFAT.CurrentRow.Cells(colFATUOM).Value = ""
            gvFAT.CurrentRow.Cells(colFATFATPer).Value = Nothing
            gvFAT.CurrentRow.Cells(colFATFATKg).Value = Nothing
        End If
    End Sub

    Private Sub OpenFATUOM(ByVal isButtonClicked As Boolean)
        Dim uom As String = clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATUOM).Value)
        Dim icode As String = clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value)

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        uom = clsCommon.myCstr(clsCommon.ShowSelectForm("PPBUOM", qry, "Code", " tspl_item_uom_detail.item_code='" + icode + "'", uom, "Code", isButtonClicked))
        gvFAT.CurrentRow.Cells(colFATUOM).Value = uom
        gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATItemCode).Value), clsCommon.myCstr(gvFAT.CurrentRow.Cells(colFATUOM).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATQty).Value), clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATPer).Value), Nothing)
        '=============By Prabhakar ==============
        gvFAT.CurrentRow.Cells(colFATFATKg).Value = clsCommon.myCdbl(gvFAT.CurrentRow.Cells(colFATFATKgEst).Value)
    End Sub

    Private Sub gvSNF_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSNF.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If (e.Column Is gvSNF.Columns(colSNFBOMCode)) OrElse (e.Column Is gvSNF.Columns(colSNFItemCode)) OrElse (e.Column Is gvSNF.Columns(colSNFUOM)) OrElse (e.Column Is gvSNF.Columns(colSNFQty)) OrElse (e.Column Is gvSNF.Columns(colSNFSNFKg)) Then
                    If (e.Column Is gvSNF.Columns(colSNFBOMCode)) Then
                        OpenSNFBOMCode(False)
                    ElseIf (e.Column Is gvSNF.Columns(colSNFItemCode)) Then
                        OpenSNFICode(False)
                    ElseIf (e.Column Is gvSNF.Columns(colSNFUOM)) Then
                        OpenSNFUOM(False)
                    End If
                    FillRawItemGridFromBOM()
                End If
                calculateSNF(clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNo).Value))
                isCellValueChanged = False
            End If
        End If
    End Sub

    Sub OpenSNFBOMCode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""
        icode = clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value)
        If clsCommon.myLen(icode) > 0 Then
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)=1 and TSPL_PP_BOM_HEAD.Vendor_Code='" + lblVendorCode.Text + "' and TSPL_PP_BOM_HEAD.prod_item_code='" + icode + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txtItemStructureSNF.Value + "' and tspl_item_master.Structure_Code='" + txtItemStructureSNF.Value + "' and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' "
        Else
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)=1 and TSPL_PP_BOM_HEAD.Vendor_Code='" + lblVendorCode.Text + "' and tspl_item_master.Structure_Code='" + txtItemStructureSNF.Value + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txtItemStructureSNF.Value + "' and tspl_item_master.item_type in ('F','S') and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' "
        End If
        Dim oldbomcode As String = clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFBOMCode).Value)
        bomcode = clsBOM.GetBOMFinderWithValidityCheck(whrCls, oldbomcode, txtDocumentDate.Value, isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select prod_item_unit_code,prod_quantity,prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvSNF.CurrentRow.Cells(colSNFBOMCode).Value = bomcode
            gvSNF.CurrentRow.Cells(colSNFUOM).Value = clsCommon.myCstr(dt.Rows(0)("prod_item_unit_code"))
            If clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFQty).Value) <= 0 Then
                gvSNF.CurrentRow.Cells(colSNFQty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("prod_quantity")), 2)

            End If
            If clsCommon.myLen(icode) <= 0 Then
                gvSNF.CurrentRow.Cells(colSNFItemCode).Value = clsCommon.myCstr(dt.Rows(0)("prod_item_code"))
                gvSNF.CurrentRow.Cells(colSNFItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dt.Rows(0)("prod_item_code")), Nothing)
                gvSNF.CurrentRow.Cells(colSNFSNFPer).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvSNF.CurrentRow.Cells(colSNFItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
                gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value), clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFUOM).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFQty).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFPer).Value), Nothing)

                gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFKgEst).Value)
            Else
                gvSNF.CurrentRow.Cells(colSNFSNFPer).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value))
            End If
        Else
            gvSNF.CurrentRow.Cells(colSNFBOMCode).Value = ""
            If clsCommon.myLen(icode) <= 0 Then
                gvSNF.CurrentRow.Cells(colSNFItemCode).Value = ""
                gvSNF.CurrentRow.Cells(colSNFItemName).Value = ""
                gvSNF.CurrentRow.Cells(colSNFUOM).Value = ""
                gvSNF.CurrentRow.Cells(colSNFQty).Value = 0
            End If
        End If
    End Sub

    Sub OpenSNFICode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFBOMCode).Value)
        If clsCommon.myLen(bomcode) > 0 Then
            Dim qry As String = "select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"
            whrCls = " tspl_item_master.item_code='" + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)) + "' and tspl_item_master.Structure_Code='" + txtItemStructureSNF.Value + "' and tspl_item_master.Active='1' "
        Else
            whrCls = " tspl_item_master.item_type in ('F','S') and tspl_item_master.Structure_Code='" + txtItemStructureSNF.Value + "' and tspl_item_master.Active='1' "
        End If
        icode = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value), isButtonClicked)
        If clsCommon.myLen(icode) > 0 Then
            gvSNF.CurrentRow.Cells(colSNFItemCode).Value = icode
            gvSNF.CurrentRow.Cells(colSNFItemName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
            gvSNF.CurrentRow.Cells(colSNFUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            gvSNF.CurrentRow.Cells(colSNFSNFPer).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
            gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value), clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFUOM).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFQty).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFPer).Value), Nothing)
            '========== By Prabhakar
            gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFKgEst).Value)
        Else
            gvSNF.CurrentRow.Cells(colSNFItemCode).Value = ""
            gvSNF.CurrentRow.Cells(colSNFItemName).Value = ""
            gvSNF.CurrentRow.Cells(colSNFUOM).Value = ""
            gvSNF.CurrentRow.Cells(colSNFSNFPer).Value = Nothing
            gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = Nothing
        End If
    End Sub

    Private Sub OpenSNFUOM(ByVal isButtonClicked As Boolean)
        Dim uom As String = clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFUOM).Value)
        Dim icode As String = clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value)

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        uom = clsCommon.myCstr(clsCommon.ShowSelectForm("PPBUOM", qry, "Code", " tspl_item_uom_detail.item_code='" + icode + "'", uom, "Code", isButtonClicked))
        gvSNF.CurrentRow.Cells(colSNFUOM).Value = uom
        gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFItemCode).Value), clsCommon.myCstr(gvSNF.CurrentRow.Cells(colSNFUOM).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFQty).Value), clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFPer).Value), Nothing)
        '========== By Prabhakar
        gvSNF.CurrentRow.Cells(colSNFSNFKg).Value = clsCommon.myCdbl(gvSNF.CurrentRow.Cells(colSNFSNFKgEst).Value)
    End Sub

    Sub FillRawItemGridFromBOM()
        Dim strLastICode As String = ""
        Try
            strLastICode = clsCommon.myCstr(gvWeighment.Rows(0).Cells(colWeightItemCode).Value)
        Catch ex As Exception
        End Try
        Dim objlist As New List(Of clsRecursiveitems)
        Dim BOMList As New List(Of String)
        gvRawItem.Rows.Clear()
        For Each grow As GridViewRowInfo In gvFAT.Rows
            If clsCommon.myLen(grow.Cells(colFATBOMCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colFATItemCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colFATUOM).Value) > 0 Then
                objlist = New List(Of clsRecursiveitems)
                clsRecursiveitems.GetItemOfBOM(strLastICode, 0, 0, objlist, grow.Cells(colFATItemCode).Value, grow.Cells(colFATQty).Value, grow.Cells(colFATUOM).Value, "", "", txtDocumentDate.Value, Nothing, 1, True, grow.Cells(colFATBOMCode).Value)
                If objlist IsNot Nothing AndAlso objlist.Count > 0 Then
                    For Each objtr As clsRecursiveitems In objlist
                        gvRawItem.Rows.AddNew()
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNo).Value = gvRawItem.Rows.Count
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentType).Value = "FAT"
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentSNo).Value = clsCommon.myCdbl(grow.Cells(colFATSNo).Value)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawTRDate).Value = clsCommon.myCDate(grow.Cells(colFATTRDate).Value)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItem).Value = grow.Cells(colFATItemCode).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemName).Value = clsItemMaster.GetItemName(grow.Cells(colFATItemCode).Value, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemQty).Value = grow.Cells(colFATQty).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawBomCode).Value = grow.Cells(colFATBOMCode).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainUOM).Value = grow.Cells(colFATUOM).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIcode).Value = objtr.ITEM_CODE
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIname).Value = clsItemMaster.GetItemName(objtr.ITEM_CODE, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawUOM).Value = objtr.UNIT_CODE
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawQty).Value = objtr.QUANTITY
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFatPer).Value = objtr.FAT
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFATKG).Value = objtr.FAT_KG
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFPer).Value = objtr.SNF
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = objtr.SNF_KG
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.FAT, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.SNF, Nothing)
                        ' '' check stock for import only
                        'If import Then
                        '    If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MP") <> CompairStringResult.Equal Then
                        '        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colFATQty).Value = objtr.QUANTITY
                        '        Dim availQty As Decimal = clsItemLocationDetails.getBalance(objtr.ITEM_CODE, clsCommon.myCstr(gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colSP_Loaction_Code).Value), txtCode.Value, txtDocumentDate.Value, Nothing, gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colFATUOM).Value, 0)
                        '        If objtr.QUANTITY > availQty Then
                        '            gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colFATQty).Value = availQty
                        '        End If
                        '    End If
                        'End If
                    Next
                End If
            End If
        Next

        For Each grow As GridViewRowInfo In gvSNF.Rows
            If clsCommon.myLen(grow.Cells(colSNFBOMCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colSNFItemCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colSNFUOM).Value) > 0 Then
                objlist = New List(Of clsRecursiveitems)
                clsRecursiveitems.GetItemOfBOM(strLastICode, 0, 0, objlist, grow.Cells(colSNFItemCode).Value, grow.Cells(colSNFQty).Value, grow.Cells(colSNFUOM).Value, "", "", txtDocumentDate.Value, Nothing, 1, True, grow.Cells(colSNFBOMCode).Value)
                If objlist IsNot Nothing AndAlso objlist.Count > 0 Then
                    For Each objtr As clsRecursiveitems In objlist
                        gvRawItem.Rows.AddNew()
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNo).Value = gvRawItem.Rows.Count
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentType).Value = "SNF"
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentSNo).Value = clsCommon.myCdbl(grow.Cells(colSNFSNo).Value)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawTRDate).Value = clsCommon.myCDate(grow.Cells(colSNFTRDate).Value)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItem).Value = grow.Cells(colSNFItemCode).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemName).Value = clsItemMaster.GetItemName(grow.Cells(colSNFItemCode).Value, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawBomCode).Value = grow.Cells(colSNFBOMCode).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainUOM).Value = grow.Cells(colSNFUOM).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIcode).Value = objtr.ITEM_CODE
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIname).Value = clsItemMaster.GetItemName(objtr.ITEM_CODE, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemQty).Value = grow.Cells(colSNFQty).Value
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawUOM).Value = objtr.UNIT_CODE
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawQty).Value = objtr.QUANTITY
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFatPer).Value = objtr.FAT
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFATKG).Value = objtr.FAT_KG
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFPer).Value = objtr.SNF
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = objtr.SNF_KG
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.SNF, Nothing)
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.SNF, Nothing)
                        ' '' check stock for import only
                        'If import Then
                        '    If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MP") <> CompairStringResult.Equal Then
                        '        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colSNFQty).Value = objtr.QUANTITY
                        '        Dim availQty As Decimal = clsItemLocationDetails.getBalance(objtr.ITEM_CODE, clsCommon.myCstr(gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colSP_Loaction_Code).Value), txtCode.Value, txtDocumentDate.Value, Nothing, gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colSNFUOM).Value, 0)
                        '        If objtr.QUANTITY > availQty Then
                        '            gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colSNFQty).Value = availQty
                        '        End If
                        '    End If
                        'End If
                    Next
                End If
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If AllowFutureDateTransaction(txtDocumentDate.Value, Nothing) = False Then
            txtDocumentDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select loaction")
        End If
        If clsCommon.myLen(lblVendorCode.Text) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Vendor not found")
        End If
        If clsCommon.myLen(txtItemStructureFAT.Value) <= 0 Then
            txtItemStructureFAT.Focus()
            Throw New Exception("Please select FAT Item Structure")
        End If
        If clsCommon.myLen(txtItemStructureSNF.Value) <= 0 Then
            txtItemStructureSNF.Focus()
            Throw New Exception("Please select SNF Item Structure")
        End If
        FillFormula()
        If clsCommon.myLen(lblFormulaCodeFAT.Text) <= 0 Then
            txtItemStructureFAT.Focus()
            Throw New Exception("Formula is not avilable for FAT Item Structure Code - " + txtItemStructureFAT.Value + "")
        End If
        If clsCommon.myLen(lblFormulaCodeSNF.Text) <= 0 Then
            txtItemStructureFAT.Focus()
            Throw New Exception("Formula is not avilable for SNF Item Structure Code - " + txtItemStructureSNF.Value + "")
        End If
        If gvWeighment.Rows.Count <= 0 Then
            Throw New Exception("SRN Document Not Available between " + clsCommon.myCstr(txtFromDate.Value) + " to " + clsCommon.myCstr(txtToDate.Value) + "")
        End If
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            calculateWeighmentFATSNFKG(ii)
        Next
        'If clsCommon.myCdbl(lblFATKGWeighmentEst.Text) = 0 AndAlso clsCommon.myCdbl(lblFATKGRawItem.Text) > 0 Then
        '    Throw New Exception("Raw Item FAT KG Should be Zero" + Environment.NewLine + " Total Estimated FAT Kg [" + clsCommon.myCstr(lblFATKGWeighmentEst.Text) + "] but Raw Item FAT KG [" + clsCommon.myCstr(lblFATKGRawItem.Text) + "]")
        'End If
        'If clsCommon.myCdbl(lblSNFKGWeighmentEst.Text) = 0 AndAlso clsCommon.myCdbl(lblSNFKGRawItem.Text) > 0 Then
        '    Throw New Exception("Raw Item SNF KG Should be Zero" + Environment.NewLine + "Total Estimated SNF Kg [" + clsCommon.myCstr(lblSNFKGWeighmentEst.Text) + "] but Raw Item SNF KG [" + clsCommon.myCstr(lblSNFKGRawItem.Text) + "]")
        'End If

        'If clsCommon.myCdbl(lblFATKGWeighmentEst.Text) > 0 AndAlso clsCommon.myCdbl(lblFATKGRawItem.Text) = 0 Then
        '    Throw New Exception("Raw Item FAT KG Should be above Zero" + Environment.NewLine + "Total Estimated FAT Kg [" + clsCommon.myCstr(lblFATKGWeighmentEst.Text) + "] but Raw Item FAT KG [" + clsCommon.myCstr(lblFATKGRawItem.Text) + "]")
        'End If
        'If clsCommon.myCdbl(lblSNFKGWeighmentEst.Text) > 0 AndAlso clsCommon.myCdbl(lblSNFKGRawItem.Text) = 0 Then
        '    Throw New Exception("Estimated SNF KG Should be above Zero" + Environment.NewLine + "Total Estimated SNF Kg [" + clsCommon.myCstr(lblSNFKGWeighmentEst.Text) + "] but Raw Item SNF KG [" + clsCommon.myCstr(lblSNFKGRawItem.Text) + "]")
        'End If
        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsJWOEstimate()
                obj.Document_NO = txtDocumentNo.Value
                obj.Document_Date = txtDocumentDate.Value
                obj.Location_Code = txtLocation.Value
                obj.Vendor_Code = lblVendorCode.Text
                obj.Item_Structure_FAT = txtItemStructureFAT.Value
                obj.Formula_code_FAT = lblFormulaCodeFAT.Text
                obj.Formula_Date_FAT = clsCommon.myCDate(lblFormulaDateFAT.Text)
                obj.Formula_FAT = lblFormulaFAT.Text
                obj.Item_Structure_SNF = txtItemStructureSNF.Value
                obj.Formula_code_SNF = lblFormulaCodeSNF.Text
                obj.Formula_Date_SNF = clsCommon.myCDate(lblFormulaDateSNF.Text)
                obj.Formula_SNF = lblFormulaSNF.Text
                obj.Qty_Weighment = clsCommon.myCdbl(lblQtyWeighment.Text)
                obj.FAT_KG_Weighment = clsCommon.myCdbl(lblFATKGWeighment.Text)
                obj.SNF_KG_Weighment = clsCommon.myCdbl(lblSNFKGWeighment.Text)
                obj.Estimated_FAT_KG_Weighment = clsCommon.myCdbl(lblFATKGWeighmentEst.Text)
                obj.Estimated_SNF_KG_Weighment = clsCommon.myCdbl(lblSNFKGWeighmentEst.Text)
                obj.FAT_KG_Raw_Item = clsCommon.myCdbl(lblFATKGRawItem.Text)
                obj.SNF_KG_Raw_Item = clsCommon.myCdbl(lblSNFKGRawItem.Text)

                obj.ArrWeighment = New List(Of clsJWOEstimateTransfer)
                For ii As Integer = 0 To gvWeighment.Rows.Count - 1
                    Dim objTr As New clsJWOEstimateTransfer()
                    objTr.SNo = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNo).Value)
                    objTr.Parent_SNo = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightParentSNo).Value)
                    objTr.Transfer_Code = clsCommon.myCstr(gvWeighment.Rows(ii).Cells(colTransferDocCode).Value)
                    objTr.Transfer_Date = clsCommon.myCDate(gvWeighment.Rows(ii).Cells(colTransferDocDate).Value)
                    objTr.Item_Code = clsCommon.myCstr(gvWeighment.Rows(ii).Cells(colWeightItemCode).Value)
                    objTr.Qty = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightQty).Value)
                    objTr.UOM = clsCommon.myCstr(gvWeighment.Rows(ii).Cells(colWeightUOM).Value)
                    objTr.FAT_PER = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightFATPer).Value)
                    objTr.FAT_KG = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightFATKg).Value)
                    objTr.Estimated_FAT_KG = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightFATKgEst).Value)
                    objTr.CLR = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightCLR).Value)
                    objTr.Correction_Factor = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightCorrectionFactor).Value)
                    objTr.SNF_PER = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNFPer).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNFKG).Value)
                    objTr.Estimated_SNF_KG = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightSNFKGEst).Value)
                    '==================Added by preeti Gupta Against ticket no[RA/ERO/15/10/19-000009]===========
                    objTr.GE_SNF_PER = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightGESNFPer).Value)
                    objTr.GE_FAT_PER = clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colWeightGEFATPer).Value)
                    '=============================================================================================
                    If clsCommon.myLen(gvWeighment.Rows(ii).Cells(colWeightFATPer).Value) <= 0 Then
                        Throw New Exception("Weighment Detail tab." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objTr.SNo) + Environment.NewLine + "Please enter FAT %")
                    End If
                    If clsCommon.myLen(gvWeighment.Rows(ii).Cells(colWeightSNFPer).Value) <= 0 Then
                        Throw New Exception("Weighment Detail tab." + Environment.NewLine + "Row no-" + clsCommon.myCstr(objTr.SNo) + Environment.NewLine + "Please enter SNF %")
                    End If
                    obj.ArrWeighment.Add(objTr)
                Next
                If obj.ArrWeighment Is Nothing OrElse obj.ArrWeighment.Count <= 0 Then
                    Throw New Exception("No weighment found To save")
                End If

                obj.ArrFATProduction = New List(Of clsJWOEstimateFATProduction)
                For ii As Integer = 0 To gvFAT.Rows.Count - 1
                    Dim objTr As New clsJWOEstimateFATProduction()
                    objTr.SNo = clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATSNo).Value)
                    objTr.TR_Date = clsCommon.myCDate(gvFAT.Rows(ii).Cells(colFATTRDate).Value)
                    objTr.Batch_No = clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATBatchNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATItemCode).Value)
                    objTr.BOM_CODE = clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATBOMCode).Value)
                    objTr.Qty = clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATQty).Value)
                    objTr.UOM = clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATUOM).Value)
                    objTr.FAT_Per = clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATFATPer).Value)
                    objTr.FAT_KG = clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATFATKg).Value)
                    objTr.Estimated_Qty = clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATQtyEst).Value)
                    objTr.Estimated_FAT_KG = clsCommon.myCdbl(gvFAT.Rows(ii).Cells(colFATFATKgEst).Value)
                    objTr.ArrQC = New List(Of clsJWOEstimateFATProductionQCParam)
                    For jj As Integer = contQCParameterStartColumnInedx To gvFAT.Columns.Count - 1
                        Dim objParam As clsJWOEstimateFATProductionQCParam = New clsJWOEstimateFATProductionQCParam()
                        objParam.Param_Field_Code = clsCommon.myCstr(gvFAT.Columns(jj).Name)
                        objParam.Param_Field_Desc = clsCommon.myCstr(gvFAT.Columns(jj).HeaderText)
                        objParam.Param_Field_Value = clsCommon.myCstr(gvFAT.Rows(ii).Cells(jj).Value)
                        objParam.Param_Type = clsCommon.myCstr(gvFAT.Columns(jj).Tag)
                        objParam.LINE_NO = jj
                        objTr.ArrQC.Add(objParam)
                    Next
                    obj.ArrFATProduction.Add(objTr)
                Next
                obj.ArrSNFProducion = New List(Of clsJWOEstimateSNFProduction)
                For ii As Integer = 0 To gvSNF.Rows.Count - 1
                    Dim objTr As New clsJWOEstimateSNFProduction()
                    objTr.SNo = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNo).Value)
                    objTr.TR_Date = clsCommon.myCDate(gvSNF.Rows(ii).Cells(colSNFTRDate).Value)
                    objTr.Batch_No = clsCommon.myCstr(gvSNF.Rows(ii).Cells(colSNFBatchNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(gvSNF.Rows(ii).Cells(colSNFItemCode).Value)
                    objTr.BOM_CODE = clsCommon.myCstr(gvSNF.Rows(ii).Cells(colSNFBOMCode).Value)
                    objTr.Qty = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFQty).Value)
                    objTr.UOM = clsCommon.myCstr(gvSNF.Rows(ii).Cells(colSNFUOM).Value)
                    objTr.SNF_Per = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNFPer).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNFKg).Value)
                    objTr.Estimated_Qty = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFQtyEst).Value)
                    objTr.Estimated_SNF_KG = clsCommon.myCdbl(gvSNF.Rows(ii).Cells(colSNFSNFKgEst).Value)
                    objTr.ArrQC = New List(Of clsJWOEstimateSNFProductionQCParam)
                    For jj As Integer = contQCParameterStartColumnInedx To gvSNF.Columns.Count - 1
                        Dim objParam As clsJWOEstimateSNFProductionQCParam = New clsJWOEstimateSNFProductionQCParam()
                        objParam.Param_Field_Code = clsCommon.myCstr(gvSNF.Columns(jj).Name)
                        objParam.Param_Field_Desc = clsCommon.myCstr(gvSNF.Columns(jj).HeaderText)
                        objParam.Param_Field_Value = clsCommon.myCstr(gvSNF.Rows(ii).Cells(jj).Value)
                        objParam.Param_Type = clsCommon.myCstr(gvSNF.Columns(jj).Tag)
                        objParam.LINE_NO = jj
                        objTr.ArrQC.Add(objParam)
                    Next
                    obj.ArrSNFProducion.Add(objTr)
                Next
                obj.ArrRawItem = New List(Of clsJWOEstimateRawItem)
                For ii As Integer = 0 To gvRawItem.Rows.Count - 1
                    Dim objTr As New clsJWOEstimateRawItem()
                    objTr.TR_Date = clsCommon.myCDate(gvRawItem.Rows(ii).Cells(colRawTRDate).Value)
                    objTr.SNo = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawSNo).Value)
                    objTr.Parent_SNo = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawParentSNo).Value)
                    objTr.Parent_Type = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawParentType).Value)
                    objTr.Main_Item_Code = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawMainItem).Value)
                    objTr.Main_Item_Qty = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawMainItemQty).Value)
                    objTr.Main_Item_UOM = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawMainUOM).Value)
                    objTr.Main_BOM_CODE = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawBomCode).Value)
                    objTr.Raw_Item_Code = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawIcode).Value)
                    objTr.Raw_Item_Qty = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawQty).Value)
                    objTr.Raw_Item_UOM = clsCommon.myCstr(gvRawItem.Rows(ii).Cells(colRawUOM).Value)
                    objTr.Raw_Item_FAT_Per = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawFatPer).Value)
                    objTr.Raw_Item_FAT_KG = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawFATKG).Value)
                    objTr.Raw_Item_SNF_Per = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawSNFPer).Value)
                    objTr.Raw_Item_SNF_KG = clsCommon.myCdbl(gvRawItem.Rows(ii).Cells(colRawSNFKG).Value)
                    obj.ArrRawItem.Add(objTr)
                Next
                If obj.saveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Document_NO, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            AddNew()
            Dim obj As clsJWOEstimate = clsJWOEstimate.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_NO) > 0) Then

                isNewEntry = False
                btnSave.Text = "Update"
                txtLocation.Enabled = False
                ucStatus.Status = obj.Status
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnPrint.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btnPrint.Enabled = True
                End If
                txtDocumentNo.Value = obj.Document_NO
                txtDocumentDate.Value = obj.Document_Date
                txtLocation.Value = obj.Location_Code
                lblLocation.Text = clsLocation.GetName(obj.Location_Code, Nothing)
                lblVendorCode.Text = obj.Vendor_Code
                lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                txtItemStructureFAT.Value = obj.Item_Structure_FAT
                lblItemStructureFAT.Text = clsItemStructureMaster.GetName(obj.Item_Structure_FAT)
                lblFormulaCodeFAT.Text = obj.Formula_code_FAT
                lblFormulaNameFAT.Text = clsJWFormula.GetName(obj.Formula_code_FAT)
                lblFormulaDateFAT.Text = clsCommon.GetPrintDate(obj.Formula_Date_FAT, "dd/MMM/yyyy")
                lblFormulaFAT.Text = obj.Formula_FAT
                txtItemStructureSNF.Value = obj.Item_Structure_SNF
                lblItemStructureSNF.Text = clsItemStructureMaster.GetName(obj.Item_Structure_SNF)
                lblFormulaCodeSNF.Text = obj.Formula_code_SNF
                lblFormulaNameSNF.Text = clsJWFormula.GetName(obj.Formula_code_SNF)
                lblFormulaDateSNF.Text = clsCommon.GetPrintDate(obj.Formula_Date_SNF, "dd/MMM/yyyy")
                lblFormulaSNF.Text = obj.Formula_SNF
                lblQtyWeighment.Text = clsCommon.myFormat(obj.Qty_Weighment)
                lblFATKGWeighment.Text = clsCommon.myFormat(obj.FAT_KG_Weighment)
                lblSNFKGWeighment.Text = clsCommon.myFormat(obj.SNF_KG_Weighment)
                lblFATKGWeighmentEst.Text = clsCommon.myFormat(obj.Estimated_FAT_KG_Weighment)
                lblSNFKGWeighmentEst.Text = clsCommon.myFormat(obj.Estimated_SNF_KG_Weighment)
                lblFATKGRawItem.Text = clsCommon.myFormat(obj.FAT_KG_Raw_Item)
                lblSNFKGRawItem.Text = clsCommon.myFormat(obj.SNF_KG_Raw_Item)

                For ii As Integer = 0 To obj.ArrWeighment.Count - 1
                    If ii = 0 Then
                        txtFromDate.Value = obj.ArrWeighment(ii).Transfer_Date
                    End If
                    If ii = obj.ArrWeighment.Count - 1 Then
                        txtToDate.Value = obj.ArrWeighment(ii).Transfer_Date
                    End If
                    gvWeighment.Rows.AddNew()
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNo).Value = obj.ArrWeighment(ii).SNo
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightParentSNo).Value = obj.ArrWeighment(ii).Parent_SNo
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTransferDocCode).Value = obj.ArrWeighment(ii).Transfer_Code
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTransferDocDate).Value = obj.ArrWeighment(ii).Transfer_Date
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightTankerNo).Value = obj.ArrWeighment(ii).Tanker_No
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightItemCode).Value = obj.ArrWeighment(ii).Item_Code
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightItemName).Value = obj.ArrWeighment(ii).Item_Name
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightQty).Value = obj.ArrWeighment(ii).Qty
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightUOM).Value = obj.ArrWeighment(ii).UOM
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightFATPer).Value = obj.ArrWeighment(ii).FAT_PER
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightFATKg).Value = obj.ArrWeighment(ii).FAT_KG
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightFATKgEst).Value = obj.ArrWeighment(ii).Estimated_FAT_KG
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightCLR).Value = obj.ArrWeighment(ii).CLR
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightCorrectionFactor).Value = obj.ArrWeighment(ii).Correction_Factor
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNFPer).Value = obj.ArrWeighment(ii).SNF_PER
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNFKG).Value = obj.ArrWeighment(ii).SNF_KG
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightSNFKGEst).Value = obj.ArrWeighment(ii).Estimated_SNF_KG
                    '=================Added by preeti gupta Agisnt ticket no[]=================
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightGESNFPer).Value = obj.ArrWeighment(ii).GE_SNF_PER
                    gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colWeightGEFATPer).Value = obj.ArrWeighment(ii).GE_FAT_PER
                    '==========================================================================
                Next

                For ii As Integer = 0 To obj.ArrFATProduction.Count - 1
                    gvFAT.Rows.AddNew()
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATSNo).Value = obj.ArrFATProduction(ii).SNo
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATTRDate).Value = obj.ArrFATProduction(ii).TR_Date
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATBatchNo).Value = obj.ArrFATProduction(ii).Batch_No
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATItemCode).Value = obj.ArrFATProduction(ii).Item_Code
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATItemName).Value = obj.ArrFATProduction(ii).Item_Name
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATBOMCode).Value = obj.ArrFATProduction(ii).BOM_CODE
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATQty).Value = obj.ArrFATProduction(ii).Qty
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATUOM).Value = obj.ArrFATProduction(ii).UOM
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATFATPer).Value = obj.ArrFATProduction(ii).FAT_Per
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATFATKg).Value = obj.ArrFATProduction(ii).FAT_KG
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATQtyEst).Value = obj.ArrFATProduction(ii).Estimated_Qty
                    gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(colFATFATKgEst).Value = obj.ArrFATProduction(ii).Estimated_FAT_KG
                    For Each objTr As clsJWOEstimateFATProductionQCParam In obj.ArrFATProduction(ii).ArrQC
                        Try
                            gvFAT.Rows(gvFAT.Rows.Count - 1).Cells(objTr.Param_Field_Code).Value = objTr.Param_Field_Value
                        Catch ex As Exception
                        End Try
                    Next
                Next

                For ii As Integer = 0 To obj.ArrSNFProducion.Count - 1
                    gvSNF.Rows.AddNew()
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFSNo).Value = obj.ArrSNFProducion(ii).SNo
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFTRDate).Value = obj.ArrSNFProducion(ii).TR_Date
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFBatchNo).Value = obj.ArrSNFProducion(ii).Batch_No
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFItemCode).Value = obj.ArrSNFProducion(ii).Item_Code
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFItemName).Value = obj.ArrSNFProducion(ii).Item_Name
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFBOMCode).Value = obj.ArrSNFProducion(ii).BOM_CODE
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFQty).Value = obj.ArrSNFProducion(ii).Qty
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFUOM).Value = obj.ArrSNFProducion(ii).UOM
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFSNFPer).Value = obj.ArrSNFProducion(ii).SNF_Per
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFSNFKg).Value = obj.ArrSNFProducion(ii).SNF_KG
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFQtyEst).Value = obj.ArrSNFProducion(ii).Estimated_Qty
                    gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(colSNFSNFKgEst).Value = obj.ArrSNFProducion(ii).Estimated_SNF_KG
                    For Each objTr As clsJWOEstimateSNFProductionQCParam In obj.ArrSNFProducion(ii).ArrQC
                        Try
                            gvSNF.Rows(gvSNF.Rows.Count - 1).Cells(objTr.Param_Field_Code).Value = objTr.Param_Field_Value
                        Catch ex As Exception
                        End Try
                    Next
                Next
                If obj.ArrRawItem IsNot Nothing AndAlso obj.ArrRawItem.Count > 0 Then
                    For ii As Integer = 0 To obj.ArrRawItem.Count - 1
                        gvRawItem.Rows.AddNew()
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawTRDate).Value = obj.ArrRawItem(ii).TR_Date
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNo).Value = obj.ArrRawItem(ii).SNo
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentSNo).Value = obj.ArrRawItem(ii).Parent_SNo
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawParentType).Value = obj.ArrRawItem(ii).Parent_Type
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItem).Value = obj.ArrRawItem(ii).Main_Item_Code
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemName).Value = obj.ArrRawItem(ii).Main_Item_Name
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainItemQty).Value = obj.ArrRawItem(ii).Main_Item_Qty
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawMainUOM).Value = obj.ArrRawItem(ii).Main_Item_UOM
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawBomCode).Value = obj.ArrRawItem(ii).Main_BOM_CODE
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIcode).Value = obj.ArrRawItem(ii).Raw_Item_Code
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawIname).Value = obj.ArrRawItem(ii).Raw_Item_Name
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawQty).Value = obj.ArrRawItem(ii).Raw_Item_Qty
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawUOM).Value = obj.ArrRawItem(ii).Raw_Item_UOM
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFatPer).Value = obj.ArrRawItem(ii).Raw_Item_FAT_Per
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawFATKG).Value = obj.ArrRawItem(ii).Raw_Item_FAT_KG
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFPer).Value = obj.ArrRawItem(ii).Raw_Item_SNF_Per
                        gvRawItem.Rows(gvRawItem.Rows.Count - 1).Cells(colRawSNFKG).Value = obj.ArrRawItem(ii).Raw_Item_SNF_KG
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        LoadData(txtDocumentNo.Value, NavType)
    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            Dim qst As String = "Select count(*) from TSPL_JWO_ESTIMATION_HEAD where document_No='" + txtDocumentNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            If txtDocumentNo.MyReadOnly OrElse isButtonClicked Then
                LoadData(clsJWOEstimate.getFinder("", txtDocumentNo.Value, isButtonClicked), NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClosed_Click(sender As Object, e As EventArgs) Handles btnClosed.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Document No To delete ")
            Else
                If myMessages.deleteConfirm() Then
                    If clsJWOEstimate.DeleteData(txtDocumentNo.Value) Then
                        myMessages.delete()
                        AddNew()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Document not found to Print", Me.Text)
        Else
            funPrint(txtDocumentNo.Value)
        End If
    End Sub

    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " select TSPL_JW_ESTIMATE_DETAILS.Document_No , convert(varchar,TSPL_JWO_ESTIMATION_HEAD.Document_Date,103) as Document_Date , TSPL_JWO_ESTIMATION_HEAD.JW_Location , tspl_location_master.location_Desc ,TSPL_JWO_ESTIMATION_HEAD.Vendor_code,tspl_vendor_master.Vendor_Name, TSPL_JWO_ESTIMATION_HEAD.Item_Structure, convert(date, TSPL_JWO_ESTIMATION_HEAD.Formula_Date,103) as Formula_Date , TSPL_JWO_ESTIMATION_HEAD.Formula, TSPL_JWO_ESTIMATION_HEAD.JW_Estimat, TSPL_JWO_ESTIMATION_HEAD.Created_By, convert(varchar, TSPL_JWO_ESTIMATION_HEAD.Created_Date,103) as Created_Date, TSPL_JWO_ESTIMATION_HEAD.Modified_By, convert (varchar,TSPL_JWO_ESTIMATION_HEAD.Modified_Date,103) as Modified_Date , convert(varchar, TSPL_JWO_ESTIMATION_HEAD.From_Date,103) as From_Date,convert(varchar, TSPL_JWO_ESTIMATION_HEAD.To_Date,103) as To_Date, " &
                                " TSPL_JW_ESTIMATE_DETAILS.SRN_NO , Convert(varchar, TSPL_JW_ESTIMATE_DETAILS.SRN_Date,103) as SRN_Date , TSPL_JW_ESTIMATE_DETAILS.UOM , TSPL_JW_ESTIMATE_DETAILS.Qty , TSPL_JW_ESTIMATE_DETAILS.FAT_PER ,TSPL_JW_ESTIMATE_DETAILS. SNF_PER , TSPL_JW_ESTIMATE_DETAILS.FAT_KG ,TSPL_JW_ESTIMATE_DETAILS. SNF_KG " &
                                " from   TSPL_JW_ESTIMATE_DETAILS left outer join TSPL_JWO_ESTIMATION_HEAD on TSPL_JW_ESTIMATE_DETAILS.Document_No = TSPL_JWO_ESTIMATION_HEAD.Document_No " &
                                " left outer join tspl_location_master on tspl_location_master.Location_Code = TSPL_JWO_ESTIMATION_HEAD.JW_Location " &
                                " left  outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code= TSPL_JWO_ESTIMATION_HEAD.Vendor_code " &
                                " where TSPL_JW_ESTIMATE_DETAILS.Document_No = '" + strDocNo + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crpJobworkEstimate", "Jobwork Estimate", "rptCompanyAddress.rpt")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Dim isItemfound As Boolean = False
        For ii As Integer = 0 To gvFAT.Rows.Count - 1
            If clsCommon.myLen(clsCommon.myCstr(gvFAT.Rows(ii).Cells(colFATItemCode).Value)) > 0 Then
                isItemfound = True
            End If
        Next
        If isItemfound = False Then
            For ii As Integer = 0 To gvSNF.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gvSNF.Rows(ii).Cells(colSNFItemCode).Value)) > 0 Then
                    isItemfound = True
                End If
            Next
        End If
        If isItemfound = True Then
            PostData()
        Else
            clsCommon.MyMessageBoxShow("Please select Item code in FAT/SNF Production Tab", Me.Text)
        End If

    End Sub

    Sub PostData()
        Try
            'BHA/04/12/18-000741 by balwinder on 12/12/2018
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtDocumentNo, "Select Document code for posting")
                txtDocumentNo.Focus()
                txtDocumentNo.Select()
                Throw New Exception("Select batch order code for posting")
            Else
                Errorcontrol.ResetError(txtDocumentNo)
            End If

            If clsCommon.myCdbl(lblFATKGWeighmentEst.Text) = 0 AndAlso clsCommon.myCdbl(lblFATKGRawItem.Text) > 0 Then
                Throw New Exception("Raw Item FAT KG Should be Zero" + Environment.NewLine + " Total Estimated FAT Kg [" + clsCommon.myCstr(lblFATKGWeighmentEst.Text) + "] but Raw Item FAT KG [" + clsCommon.myCstr(lblFATKGRawItem.Text) + "]")
            End If
            If clsCommon.myCdbl(lblSNFKGWeighmentEst.Text) = 0 AndAlso clsCommon.myCdbl(lblSNFKGRawItem.Text) > 0 Then
                Throw New Exception("Raw Item SNF KG Should be Zero" + Environment.NewLine + "Total Estimated SNF Kg [" + clsCommon.myCstr(lblSNFKGWeighmentEst.Text) + "] but Raw Item SNF KG [" + clsCommon.myCstr(lblSNFKGRawItem.Text) + "]")
            End If

            If clsCommon.myCdbl(lblFATKGWeighmentEst.Text) > 0 AndAlso clsCommon.myCdbl(lblFATKGRawItem.Text) = 0 Then
                Throw New Exception("Raw Item FAT KG Should be above Zero" + Environment.NewLine + "Total Estimated FAT Kg [" + clsCommon.myCstr(lblFATKGWeighmentEst.Text) + "] but Raw Item FAT KG [" + clsCommon.myCstr(lblFATKGRawItem.Text) + "]")
            End If
            If clsCommon.myCdbl(lblSNFKGWeighmentEst.Text) > 0 AndAlso clsCommon.myCdbl(lblSNFKGRawItem.Text) = 0 Then
                Throw New Exception("Estimated SNF KG Should be above Zero" + Environment.NewLine + "Total Estimated SNF Kg [" + clsCommon.myCstr(lblSNFKGWeighmentEst.Text) + "] but Raw Item SNF KG [" + clsCommon.myCstr(lblSNFKGRawItem.Text) + "]")
            End If

            If clsCommon.MyMessageBoxShow("Post the current Document No [" + txtDocumentNo.Value + "]", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If clsJWOEstimate.PostData(txtDocumentNo.Value) Then
                    myMessages.post()
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmSRNJobWorkEstimate_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                AddNew()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                btnSave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                btnDelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                btnPost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
                GroupBox92.Visible = True

            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_HEAD" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_TRANSFER" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_FAT_PRODUCTION" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_FAT_PRODUCTION_QC_PARAMETER" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_SNF_PRODUCTION" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_SNF_PRODUCTION_QC_PARAMETER" + Environment.NewLine +
                                        "TSPL_JWO_ESTIMATION_RAW_ITEM")
                If btnunpost.Visible Then
                    btnunpost.Visible = False
                Else
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnunpost.Visible = True
                    End If
                End If
                ''commented because not used currently
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnunpost_Click(sender As Object, e As EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtDocumentNo, "Select Document code for unposting")
                txtDocumentNo.Focus()
                txtDocumentNo.Select()
                Throw New Exception("Select batch order code for unposting")
            Else
                Errorcontrol.ResetError(txtDocumentNo)
            End If
            If clsCommon.MyMessageBoxShow("Unpost the current Document No [" + txtDocumentNo.Value + "]", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If clsJWOEstimate.ReverseAndUnpostData(txtDocumentNo.Value) Then
                    clsCommon.MyMessageBoxShow("Transaction unposted successfully", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocumentNo.Value)
    End Sub

    Private Sub txtDocumentDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDocumentDate.ValueChanged
        FillFormula()
        'If clsCommon.myLen(txtItemStructureFAT.Value) > 0 Then
        '    If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.myLen(lblVendorCode.Text) > 0 Then
        '        Dim qry As String = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " & _
        '                            " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " & _
        '                            " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING.FormulaCode " & _
        '                            "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureFAT.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103)  " & _
        '                            "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode desc "
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '            lblFormulaCodeFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Code"))
        '            lblFormulaNameFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Desc"))
        '            lblFormulaDateFAT.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
        '            lblFormulaFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula"))

        '        Else
        '            lblFormulaCodeFAT.Text = ""
        '            lblFormulaNameFAT.Text = ""
        '            lblFormulaDateFAT.Text = ""
        '            lblFormulaFAT.Text = ""

        '        End If
        '    End If
        'End If
        'If clsCommon.myLen(txtItemStructureSNF.Value) > 0 Then
        '    If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.myLen(lblVendorCode.Text) > 0 Then

        '        Dim qry As String = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " & _
        '                            " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " & _
        '                            " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING.FormulaCode " & _
        '                            "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureSNF.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103) " & _
        '                            "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode desc "

        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '            lblFormulaCodeSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Code"))
        '            lblFormulaNameSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Desc"))
        '            lblFormulaDateSNF.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
        '            lblFormulaSNF.Text = clsCommon.myCstr(dt.Rows(0)("Formula"))
        '        Else
        '            lblFormulaCodeSNF.Text = ""
        '            lblFormulaNameSNF.Text = ""
        '            lblFormulaDateSNF.Text = ""
        '            lblFormulaSNF.Text = ""
        '        End If
        '    End If
        'End If
    End Sub
    Public Sub FillFormula()
        If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.myLen(lblVendorCode.Text) > 0 Then
            'For FAT
            Dim qry As String = ""
            If clsCommon.myLen(txtItemStructureFAT.Value) > 0 Then
                qry = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " &
                                    " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " &
                                    " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode " &
                                    "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureFAT.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103)  " &
                                    "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode desc "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    lblFormulaCodeFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Code"))
                    lblFormulaNameFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula_Desc"))
                    lblFormulaDateFAT.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
                    lblFormulaFAT.Text = clsCommon.myCstr(dt.Rows(0)("Formula"))

                Else

                    lblFormulaCodeFAT.Text = ""
                    lblFormulaNameFAT.Text = ""
                    lblFormulaDateFAT.Text = ""
                    lblFormulaFAT.Text = ""

                End If
            End If
            ' For SNF
            If clsCommon.myLen(txtItemStructureSNF.Value) > 0 Then


                qry = " select  top 1 TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode,TSPL_JWO_FORMULA.Code as Formula_Code, TSPL_JWO_FORMULA.Description as Formula_Desc,TSPL_JWO_FORMULA.Formula ,convert (varchar,TSPL_JWO_FORMULA.Created_Date,103) as Formula_Date  from TSPL_JWO_VENDOR_FORMULA_MAPPING  " &
                                    " left outer join TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL on TSPL_JWO_VENDOR_FORMULA_MAPPING.DocCode = TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.DocCode " &
                                    " left outer join TSPL_JWO_FORMULA on TSPL_JWO_FORMULA.Code =TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.FormulaCode " &
                                    "  where TSPL_JWO_VENDOR_FORMULA_MAPPING_DETAIL.vendorCode = '" + lblVendorCode.Text + "' and TSPL_JWO_FORMULA.Structure_Code = '" + txtItemStructureSNF.Value + "' and TSPL_JWO_VENDOR_FORMULA_MAPPING.Posted = 1 and  convert (date, TSPL_JWO_VENDOR_FORMULA_MAPPING.DocDate,103) <=  Convert (date,'" + txtDocumentDate.Value + "',103) " &
                                    "  order by TSPL_JWO_VENDOR_FORMULA_MAPPING.Doccode desc "

                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then

                    lblFormulaCodeSNF.Text = clsCommon.myCstr(dt2.Rows(0)("Formula_Code"))
                    lblFormulaNameSNF.Text = clsCommon.myCstr(dt2.Rows(0)("Formula_Desc"))
                    lblFormulaDateSNF.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dt2.Rows(0)("Formula_Date")), "dd/MMM/yyyy")
                    lblFormulaSNF.Text = clsCommon.myCstr(dt2.Rows(0)("Formula"))
                Else
                    lblFormulaCodeSNF.Text = ""
                    lblFormulaNameSNF.Text = ""
                    lblFormulaDateSNF.Text = ""
                    lblFormulaSNF.Text = ""
                End If
            End If
        End If

    End Sub
    ' Job Work Estimate JV recreation
    Private Sub RadButton292_Click(sender As Object, e As EventArgs) Handles RadButton292.Click
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("DocumentNo", "varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TEMP_CREATED_JW_ESTIMATE", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("DocumentNo", "varchar(30) null")
        coll.Add("VoucherNo", "varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TEMP_DELETED_JW_ESTIMATE", coll)

        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_JW_ESTIMATE")
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_JW_ESTIMATE")
    End Sub

    Private Sub RadButton291_Click(sender As Object, e As EventArgs) Handles RadButton291.Click
        Try
            Dim qry As String = " select TSPL_JWO_ESTIMATION_HEAD.Document_NO as DocumentNo,TSPL_JOURNAL_MASTER.Voucher_No as VoucherNo from TSPL_JWO_ESTIMATION_HEAD" + Environment.NewLine +
            " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_JWO_ESTIMATION_HEAD.Document_NO " + Environment.NewLine +
            " where TSPL_JWO_ESTIMATION_HEAD.Status=1 "
            Dim QryInsert As String = ""
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm(False, "JWESTIMATE", qry, "DocumentNo", "", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_JW_ESTIMATE")
                QryInsert = "insert into TEMP_DELETED_JW_ESTIMATE "
                QryInsert += "select DocumentNo,VoucherNo from (" & qry & " and TSPL_JWO_ESTIMATION_HEAD.Document_NO in(" + clsCommon.GetMulcallString(arr) & ")) Rev"
                clsDBFuncationality.ExecuteNonQuery(QryInsert)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton293_Click(sender As Object, e As EventArgs) Handles RadButton293.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETED_JW_ESTIMATE where DocumentNo not in (select DocumentNo from TEMP_CREATED_JW_ESTIMATE)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Recreate Journal Entry of " + clsCommon.myCstr(dt.Rows.Count) + " Jobwork ESTIMATE", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("DocumentNo"))
                        Dim strVoucherNo As String = clsCommon.myCstr(dt.Rows(ii)("VoucherNo"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            Dim obj As clsJWOEstimate = clsJWOEstimate.GetData(strDocNo, NavigatorType.Current, trans)

                            Dim CreateJVofPackingMaterialofJWInwardinJWEstimate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJVofPackingMaterialofJWInwardinJWEstimate, clsFixedParameterCode.CreateJVofPackingMaterialofJWInwardinJWEstimate, trans)) = 1, True, False)
                            If CreateJVofPackingMaterialofJWInwardinJWEstimate Then
                                clsJWOEstimate.CreateJournalEntry(trans, obj.Document_Date, obj.Document_NO, obj.Location_Code, "")
                            End If


                            clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_JW_ESTIMATE values('" & strDocNo & "')", trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "Jobwork ESTIMATE - " + strDocNo + " Voucher No - " + strVoucherNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Recreate journal entry " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow("Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class