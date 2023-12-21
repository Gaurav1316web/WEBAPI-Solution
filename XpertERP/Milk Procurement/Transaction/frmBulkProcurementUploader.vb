Imports common
Imports System.IO
Imports System.Data.SqlClient

Public Class frmBulkProcurementUploader
    Inherits FrmMainTranScreen
#Region "Variables"

    Dim paramcount As Integer = 0
    Dim intStartParam As Integer = 0
    Public TextCol As GridViewTextBoxColumn = Nothing
    Dim isdipmarkingmendatory As Boolean = False
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public ValidatedCount As Integer = 0
    Dim isNewEntry As Boolean = False
    Public Const colSlNo As String = "SlNo"
    Public Const colSealNo As String = "SealNo"
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoClr As String = "AutoClr"
    Const colACCode As String = "NAME"
    Public Const colFatKG As String = "colFatKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colFatRate As String = "colFatRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colAmount As String = "colAmount"
    Public Const colCleaningDoneby As String = "colCleaningDoneby"
    Public Const colCleaningCheckedBy As String = "colCleaningCheckedBy"

    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty


    Public Const colDispatchDate As String = "colDispatchDate"
    Public Const colDispatchTo As String = "colDispatchTo"
    Public Const colMccOrPlantCOde As String = "colMccOrPlantCOde"
    Public Const colMccCode As String = "colMccCode"
    Public Const colChallanNo As String = "colChallanNo"
    Public Const colMccName As String = "colMccName"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colTankerKM As String = "colTankerKM"
    Public Const colDripM As String = "colDripM"

    Public Const colTankerFull As String = "colTankerFull"
    Public Const colControlSample As String = "colControlSample"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colName As String = "colName"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"

    Public Const colUOM As String = "colUOM"
    Public Const colTransferPrice As String = "colTransferPrice"
    Public Const colControlSampFat As String = "colControlSampFat"
    Public Const colControlSampSNF As String = "colControlSampSNF"
    Public Const colTankerRemarks As String = "colTankerRemarks"
    Public Const colPriceChart As String = "colPriceChart"
    Public Const colFat_W As String = "colFt_W"
    Public Const colSNF_W As String = "colSNF_W"
    Public Const colFat_R As String = "colFat_R"
    Public Const colSNF_R As String = "colSNF_R"
    Public Const colIsJobWork As String = "colIsJobWork"
    Public Const colJobworkLoc As String = "colJobworkLoc"
    Public Const colFatPer As String = "colFatPer"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colWtGross As String = "colWtGross"
    Public Const colWtTare As String = "colWtTare"
    Public Const colWtNet As String = "colWtNet"
    Const ReportID As String = "BatchInvIn"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim MilkWeight_Setting As Decimal = 0
    Dim CreateNewDocumentOnUploader As Boolean = False
    Dim strFolderPath As String = ""
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load       
             AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        isdipmarkingmendatory = IIf(clsFixedParameter.GetData(clsFixedParameterType.DipMarkingMendatory, clsFixedParameterCode.DipMarkingMendatory, Nothing) = "1", True, False) 'added by stuti on 24/11/2016
        AddNew()
        btnSave.Enabled = False
        btnValidate.Enabled = False
    End Sub



    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colSlNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        ChkBoxColumn = New GridViewCheckBoxColumn()
        ChkBoxColumn.Name = colIsValidated
        ChkBoxColumn.HeaderText = "Validated"
        ChkBoxColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

        TextCol = New GridViewTextBoxColumn()
        TextCol.Name = colErrorStatus
        TextCol.HeaderText = "Error Status"
        TextCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Insert(2, TextCol)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Challan No"
        repoTextBox.Name = colChallanNo
        repoTextBox.Width = 150
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "MCC Code"
        repoTextBox.Name = colMccCode
        repoTextBox.Width = 150
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "MCC Name"
        repoTextBox.Name = colMccName
        repoTextBox.Width = 150
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Dispatch Date"
        repoDateBox.Name = colDispatchDate
        repoDateBox.ReadOnly = False
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDateBox)



        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Dispatch To"
        repoTextBox.Name = colDispatchTo
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "MCC OR Plant Code"
        repoTextBox.Name = colMccOrPlantCOde
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tanker No"
        repoTextBox.Name = colTankerNo
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tanker KM Reading"
        repoTextBox.Name = colTankerKM
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Drip Marking"
        repoTextBox.Name = colDripM
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tanker Full"
        repoTextBox.Name = colTankerFull
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Control Sample"
        repoTextBox.Name = colControlSample
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Name of Custodian"
        repoTextBox.Name = colName
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = colItemCode
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Desc"
        repoTextBox.Name = colItemDesc
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = colUOM
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

      
        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Gross Weight"
        repoTextBox.Name = colGrossWeight
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tare Weight"
        repoTextBox.Name = colTareWeight
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)


        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Net Weight"
        repoTextBox.Name = colNetWeight
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Transfer Price"
        repoTextBox.Name = colTransferPrice
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Control Sample Fat"
        repoTextBox.Name = colControlSampFat
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Control Sample SNF"
        repoTextBox.Name = colControlSampSNF
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tanker Remarks"
        repoTextBox.Name = colTankerRemarks
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Price Chart"
        repoTextBox.Name = colPriceChart
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Fat Weightage"
        repoTextBox.Name = colFat_W
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "SNF Weightage"
        repoTextBox.Name = colSNF_W
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Fat Ratio"
        repoTextBox.Name = colFat_R
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "SNF Ratio"
        repoTextBox.Name = colSNF_R
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Is JobWork"
        repoTextBox.Name = colIsJobWork
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Jobwork Loc"
        repoTextBox.Name = colJobworkLoc
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Weighment Gross"
        repoTextBox.Name = colWtGross
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Weighment Tare"
        repoTextBox.Name = colWtTare
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Weighment Net"
        repoTextBox.Name = colWtNet
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Cleaning Doneby"
        repoTextBox.Name = colCleaningDoneby
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Cleaning Checkedby"
        repoTextBox.Name = colCleaningCheckedBy
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "FatPer"
        repoTextBox.Name = colFatPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "SNFPer"
        repoTextBox.Name = colSNFPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDecimalColumn As GridViewDecimalColumn
        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFatKG
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 3
        repoDecimalColumn.HeaderText = "FAT KG"
        repoDecimalColumn.Tag = colFatKG
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFatRate
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = "FAT Rate"
        repoDecimalColumn.Tag = colFatRate
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colSNFKG
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 3
        repoDecimalColumn.HeaderText = "SNF KG"
        repoDecimalColumn.Tag = colSNFKG
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colSNFRate
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = "SNF Rate"
        repoDecimalColumn.Tag = colSNFRate
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colAmount
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = "Amount"
        repoDecimalColumn.Tag = colAmount
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        gv1.Columns.Add("colRemarks", "Remarks")
        gv1.Columns("colRemarks").Width = 300
        gv1.Columns("colRemarks").ReadOnly = False
        gv1.Columns("colRemarks").Tag = "REM"
        gv1.Columns("colRemarks").WrapText = True

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
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)
        AddNew()
    End Sub
    Sub AddNew()
        btnSave.Enabled = True       
        isNewEntry = True
        gv1.DataSource = Nothing
        LoadBlankGrid()
        ResetParameterGrid()
        gv1.Rows.AddNew()
        btnSave.Text = "Save"
        btnSave.Enabled = False
        btnValidate.Enabled = False
    End Sub



    Sub ResetParameterGrid()
       
        paramcount = 0

        Dim whrCls As String = String.Empty
        whrCls = " where Param_for='MCC' or Param_for='BOTH'"
       
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,description  + ' TD '   + Nature + convert(varchar(1),ismandatory) as Descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        For i As Integer = 0 To dt.Rows.Count() - 1
            paramcount += 1
            'If clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
            '    CfColName = dt.Rows(i)("Code")
            'End If
            'If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
            '    FATColName = dt.Rows(i)("Code")
            'End If
            'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
            '    SNFColName = dt.Rows(i)("Code")
            'End If
            'If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
            '    CLRColName = dt.Rows(i)("Code")
            'End If
            Dim objImportTemp As clsImportTemp = New clsImportTemp()
            objImportTemp.Code = dt.Rows(i)("Code")
            objImportTemp.Description = dt.Rows(i)("Description")
            objImportTemp.Type = dt.Rows(i)("Type")
            objImportTemp.Nature = dt.Rows(i)("Nature")
            objImportTemp.IsMandatory = dt.Rows(i)("IsMandatory")

            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = dt.Rows(i)("Descr")
            repoTextColumn.Width = 120
            repoTextColumn.HeaderText = dt.Rows(i)("Descr")
            'repoTextColumn.Tag = dt.Rows(i)("Type")

            repoTextColumn.ReadOnly = False
            repoTextColumn.Tag = objImportTemp
            gv1.MasterTemplate.Columns.Add(repoTextColumn)
        Next

        qry = " select *,description  + ' QC '   + Nature + convert(varchar(1),ismandatory) as Descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

      
        For i As Integer = 0 To dt1.Rows.Count() - 1
            Dim objImportTemp As clsImportTemp = New clsImportTemp()
            objImportTemp.Code = dt1.Rows(i)("Code")
            objImportTemp.Description = dt1.Rows(i)("Description")
            objImportTemp.Type = dt1.Rows(i)("Type")
            objImportTemp.Nature = dt1.Rows(i)("Nature")
            objImportTemp.IsMandatory = dt1.Rows(i)("IsMandatory")


            repoTextColumn = New GridViewTextBoxColumn()
            repoTextColumn.Name = dt1.Rows(i)("Descr")
            repoTextColumn.Width = 120
            repoTextColumn.HeaderText = dt1.Rows(i)("Descr")
            repoTextColumn.ReadOnly = False
            repoTextColumn.Tag = objImportTemp
            gv1.MasterTemplate.Columns.Add(repoTextColumn)
        Next

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        'gv1.AutoSizeRows = True
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        ReStoreGridLayout()
        isCellValueChangedOpen = False

        repoTextColumn = Nothing
        repoDecimalColumn = Nothing
        repoComboColumn = Nothing
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


    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.RowCount - 1
            If gv1.Rows(ii).Cells(colIsValidated).Value = False Then
                Return False
                Exit For
            End If
        Next

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

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs)
        RefeshSNO()
    End Sub

   
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSlNo).Value = ii
        Next
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Sub CheckAndValidate()
        Dim ValidateStatus As String = String.Empty
        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "There are no row is grid please select a sheet to import", Me.Text)
        End If
        'If ValidatedCount = Gv1.Rows.Count Then
        '    clsCommon.MyMessageBoxShow("All Rows are already validated")
        '    Exit Sub
        'End If
        ValidatedCount = 0
        Dim strCellValue

        For i As Integer = 0 To gv1.Rows.Count - 1
            If i = 0 Then
                clsCommon.ProgressBarPercentShow()
            End If
            clsCommon.ProgressBarPercentUpdate((i + 1) / gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & gv1.Rows.Count)
            ValidateStatus = ""

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colMccCode).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "MCC Code Must not be Blank" & Environment.NewLine
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                ValidateStatus = ValidateStatus & "MCC Code not found in master" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCDate(gv1.Rows(i).Cells(colDispatchDate).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Dispatch Date  Must not be Blank" & Environment.NewLine
            End If
            If IsDate(strCellValue) Then
            Else
                ValidateStatus = ValidateStatus & "Dispatch Date Must  be a Date Time Value" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colDispatchTo).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Please Select Tanker Dispatch To Either PLANT/MCC " & Environment.NewLine
            End If

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colMccOrPlantCOde).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "MCC OR Plant Code Must not be Blank" & Environment.NewLine
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                ValidateStatus = ValidateStatus & "MCC OR Plant Code not found in master" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colTankerNo).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Tanker No. Must not be Blank" & Environment.NewLine
            End If
            If strCellValue.ToString.Contains(" ") Then
                ValidateStatus = ValidateStatus & "Tanker No Must not contain any Blank Space" & Environment.NewLine
            End If
            If strCellValue.ToString.Contains("!") OrElse strCellValue.ToString.Contains("@") OrElse strCellValue.ToString.Contains("#") OrElse strCellValue.ToString.Contains("$") OrElse strCellValue.ToString.Contains("%") OrElse strCellValue.ToString.Contains("^") OrElse strCellValue.ToString.Contains("&") OrElse strCellValue.ToString.Contains("*") OrElse strCellValue.ToString.Contains("(") OrElse strCellValue.ToString.Contains(")") OrElse strCellValue.ToString.Contains("_") OrElse strCellValue.ToString.Contains("-") OrElse strCellValue.ToString.Contains("+") OrElse strCellValue.ToString.Contains("=") OrElse strCellValue.ToString.Contains("\") OrElse strCellValue.ToString.Contains("|") OrElse strCellValue.ToString.Contains("/") OrElse strCellValue.ToString.Contains("?") OrElse strCellValue.ToString.Contains(">") OrElse strCellValue.ToString.Contains("<") OrElse strCellValue.ToString.Contains(".") OrElse strCellValue.ToString.Contains("]") OrElse strCellValue.ToString.Contains("[") OrElse strCellValue.ToString.Contains("{") OrElse strCellValue.ToString.Contains("}") Then
                ValidateStatus = ValidateStatus & "Tanker No Must not contain any Special Symbol" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colTankerKM).Value)
            If strCellValue <= 0 Then
                ValidateStatus = ValidateStatus & "Tanker KM Reading not be Zero or Negative" & Environment.NewLine
            End If

            If isdipmarkingmendatory Then
                strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colDripM).Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Please Enter Dip Marking " & Environment.NewLine
                End If
            End If


            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Item Code Must not be Blank" & Environment.NewLine
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & strCellValue & "'")) <= 0 Then
                ValidateStatus = ValidateStatus & "Item Code not found in master" & Environment.NewLine
            End If

            Dim ItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Product_type,'') as  Product_type from tspl_item_master where Item_code='" & strCellValue & "'"))
            If clsCommon.CompairString(ItemType, "MI") = CompairStringResult.Equal Then
            Else
                ValidateStatus = ValidateStatus & "Item Type must be Milk Item " & Environment.NewLine
            End If

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colUOM).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Unit Must not be Blank" & Environment.NewLine
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_UNIT_MASTER where Unit_Code='" & strCellValue & "'")) <= 0 Then
                ValidateStatus = ValidateStatus & "Unit Code not found in master" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colGrossWeight).Value)
            If strCellValue <= 0 Then
                ValidateStatus = ValidateStatus & "Gross Weight Must not be Zero or Negative" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colTareWeight).Value)
            If strCellValue <= 0 Then
                ValidateStatus = ValidateStatus & "Tare Weight Must not be Zero or Negative" & Environment.NewLine
            End If

            If strCellValue > clsCommon.myCdbl(gv1.Rows(i).Cells(colGrossWeight).Value) Then
                ValidateStatus = ValidateStatus & "Tare Weight Must not be Larger Then Gross Weight" & Environment.NewLine
            End If

            Dim netWeight As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colGrossWeight).Value) - clsCommon.myCdbl(gv1.Rows(i).Cells(colTareWeight).Value)

            gv1.Rows(i).Cells(colNetWeight).Value = netWeight

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colTransferPrice).Value)
            If strCellValue < 0 Then
                ValidateStatus = ValidateStatus & "Dip Value Must not be Negative or Zero" & Environment.NewLine
            End If
            Dim dblTransferprice As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colTransferPrice).Value)

            Dim Sub_location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & (gv1.Rows(i).Cells(colMccCode).Value) & "'"))
            If clsCommon.myLen(Sub_location_Code) <= 0 Then
                ValidateStatus = ValidateStatus & "Please create silo location for " & gv1.Rows(i).Cells(colMccCode).Value & " " & Environment.NewLine
            End If

            If AllowJobWorkonGateEntryBulkProc = 0 Then
                gv1.Rows(i).Cells(colIsJobWork).Value = 0
                gv1.Rows(i).Cells(colJobworkLoc).Value = ""
            Else
                Dim strLocation As String = clsCommon.myCstr(gv1.Rows(i).Cells(colMccOrPlantCOde).Value)
                Dim strJobWork As String = clsCommon.myCstr(gv1.Rows(i).Cells(colIsJobWork).Value)
                Dim strJobWorkLoc As String = clsCommon.myCstr(gv1.Rows(i).Cells(colJobworkLoc).Value)
                If Not IsNumeric(strJobWork) Then
                    ValidateStatus = ValidateStatus & "Is Job Work, must be Numeric" & Environment.NewLine
                End If
                If clsCommon.CompairString(strJobWork, "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(strJobWork, "0") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Is Job Work Value Must be either 1/0" & Environment.NewLine
                End If
                If clsCommon.CompairString(strJobWork, "1") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strJobWorkLoc) <= 0 Then
                        ValidateStatus = ValidateStatus & "Job Work Location Code Must not be Blank" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Main_Location_Code='" & strLocation & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y' and location_code='" & strJobWorkLoc & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Job Work Location code not found in master for Location " & strLocation & " " & Environment.NewLine
                    End If
                Else
                    gv1.Rows(i).Cells(colJobworkLoc).Value = ""
                End If
            End If

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colWtGross).Value)
            If strCellValue <= 0 Then
                ValidateStatus = ValidateStatus & "Weighment Gross Weight Must not be Zero or Negative" & Environment.NewLine
            End If

            strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colWtTare).Value)
            If strCellValue <= 0 Then
                ValidateStatus = ValidateStatus & "Weighment Tare Weight Must not be Zero or Negative" & Environment.NewLine
            End If

            If strCellValue > clsCommon.myCdbl(gv1.Rows(i).Cells(colWtGross).Value) Then
                ValidateStatus = ValidateStatus & "Weighment Tare Weight Must not be Larger Then Gross Weight" & Environment.NewLine
            End If

            Dim WeighmentnetWeight As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colWtGross).Value) - clsCommon.myCdbl(gv1.Rows(i).Cells(colWtTare).Value)

            gv1.Rows(i).Cells(colWtNet).Value = WeighmentnetWeight

            strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colPriceChart).Value)
            If clsCommon.myLen(strCellValue) <= 0 Then
                ValidateStatus = ValidateStatus & "Price chart Must not be Blank" & Environment.NewLine
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                ValidateStatus = ValidateStatus & "Price chart not found in master" & Environment.NewLine
            End If
            Dim objP As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(clsCommon.myCstr(gv1.Rows(i).Cells(colPriceChart).Value), NavigatorType.Current)
            If objP IsNot Nothing Then
                gv1.Rows(i).Cells(colFat_W).Value = objP.Fat_Weightage
                gv1.Rows(i).Cells(colSNF_W).Value = objP.Snf_Weightage
                gv1.Rows(i).Cells(colFat_R).Value = objP.Fat_Percentage
                gv1.Rows(i).Cells(colSNF_R).Value = objP.Snf_Percentage
            Else
                gv1.Rows(i).Cells(colFat_W).Value = 0
                gv1.Rows(i).Cells(colSNF_W).Value = 0
                gv1.Rows(i).Cells(colFat_R).Value = 0
                gv1.Rows(i).Cells(colSNF_R).Value = 0
            End If
            Dim FatW As Double = objP.Fat_Weightage
            Dim SNFW As Double = objP.Snf_Weightage
            Dim FATR As Double = objP.Fat_Percentage
            Dim SNFR As Double = objP.Snf_Percentage



            Dim FATPer As Double = 0
            Dim SNFPer As Double = 0
            Dim CFValue As Double = 0

            Dim jj As Integer = 0
            jj = intStartParam
            Dim intCOunt As Integer = intStartParam

            For ii As Integer = jj To gv1.Columns.Count - 1
                Dim objImportTemp As clsImportTemp = TryCast(gv1.Columns(ii).Tag, clsImportTemp)
                Dim ismandatory As Integer = objImportTemp.IsMandatory
                Dim ParamType = objImportTemp.Type
                Dim Nature = objImportTemp.Nature
                Dim Code = objImportTemp.Code


                'Dim Desc As String = clsCommon.myCstr(gv1.Rows(i).Cells(ii).ColumnInfo.HeaderText)
                'Dim ismandatory = Desc(Desc.Length - 1)
                'Dim PapamType = Desc(Desc.Length - 2)

                If ismandatory = "1" Then
                    If Nature = "R" Then
                        strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(ii).Value)
                        If strCellValue <= 0 Then
                            ValidateStatus = ValidateStatus & clsCommon.myCstr(gv1.Rows(i).Cells(ii).ColumnInfo.HeaderText) & " Value Must not be Negative or Zero" & Environment.NewLine
                        End If
                        If intCOunt < jj + paramcount - 1 Then
                            If clsCommon.CompairString(ParamType, "Fat") = CompairStringResult.Equal Then
                                FATPer = clsCommon.myCdbl(gv1.Rows(i).Cells(ii).Value)
                            ElseIf clsCommon.CompairString(ParamType, "SNF") = CompairStringResult.Equal Then
                                SNFPer = clsCommon.myCdbl(gv1.Rows(i).Cells(ii).Value)
                            End If
                        End If
                    ElseIf Nature = "B" Then
                        strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(ii).Value)
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & clsCommon.myCstr(gv1.Rows(i).Cells(ii).ColumnInfo.HeaderText) & " Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf Nature = "A" Then
                        strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(ii).Value)
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & Code & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & clsCommon.myCstr(gv1.Rows(i).Cells(ii).ColumnInfo.HeaderText) & ", the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    End If                  
                End If
                If clsCommon.CompairString(ParamType, "CF") = CompairStringResult.Equal Then
                    CFValue = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
                    gv1.Rows(i).Cells(ii).Value = CFValue
                End If
                intCOunt = intCOunt + 1
            Next

            If FATPer > 0 Then
                gv1.Rows(i).Cells(colFatRate).Value = Math.Round(clsCommon.myCdbl(dblTransferprice) * FatW / FATPer, 4)
                gv1.Rows(i).Cells(colFatKG).Value = netWeight * FATPer / 100
                gv1.Rows(i).Cells(colFatRate).Value = Math.Round(clsCommon.myCdbl(dblTransferprice) * FatW / FATR, 4)
                gv1.Rows(i).Cells(colFatPer).Value = Math.Round(FATPer, 2)
            End If
            If SNFPer > 0 Then
                gv1.Rows(i).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(dblTransferprice) * SNFW / SNFPer, 4)
                gv1.Rows(i).Cells(colSNFKG).Value = netWeight * SNFPer / 100
                gv1.Rows(i).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(dblTransferprice) * SNFW / SNFR, 4)
                gv1.Rows(i).Cells(colSNFPer).Value = Math.Round(SNFPer, 2)
            End If
            gv1.Rows(i).Cells(colAmount).Value = Math.Round((gv1.Rows(i).Cells(colFatRate).Value * gv1.Rows(i).Cells(colFatKG).Value) + (gv1.Rows(i).Cells(colSNFRate).Value * gv1.Rows(i).Cells(colSNFKG).Value), 2)


            If clsCommon.myLen(ValidateStatus) <= 0 Then
                gv1.Rows(i).Cells(colIsValidated).Value = True
                ValidatedCount = ValidatedCount + 1
                gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.White
                gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                btnSave.Enabled = True
            Else
                gv1.Rows(i).Cells(colIsValidated).Value = False
                gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.Blue
                gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                btnSave.Enabled = False
            End If
        Next

        gv1.BestFitColumns()
        gv1.AutoSizeRows = True
        gv1.Columns(colSlNo).PinPosition = PinnedColumnPosition.Left
        gv1.Columns(colIsValidated).PinPosition = PinnedColumnPosition.Left
        gv1.Columns(colErrorStatus).PinPosition = PinnedColumnPosition.Left
        clsCommon.ProgressBarPercentHide()
    End Sub
    Public Function SaveData() As Boolean
        Dim trans As SqlTransaction = Nothing
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0

        Try
            If (AllowToSave()) Then
                clsCommon.ProgressBarPercentShow()

                Dim obj
                Dim dt As Date = Nothing
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colMccCode).Value) > 0 And clsCommon.myLen(grow.Cells(colChallanNo).Value) = 0 Then
                        trans = clsDBFuncationality.GetTransactin()
                        j = j + 1
                        clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount)
                        obj = New clsMccDispatch()
                        obj.isNewEntry = True

                        obj.RefBulkDispatchUploader = "Auto"
                        obj.Document_Date = clsCommon.GetPrintDate(grow.Cells(colDispatchDate).Value, "dd/MMM/yyyy hh:mm:ss tt")
                        obj.Description = ""
                        obj.Dispatch_Date = clsCommon.GetPrintDate(grow.Cells(colDispatchDate).Value, "dd/MMM/yyyy hh:mm:ss tt") 'clsCommon.myCDate(grow.Cells(colDispatchDate).Value)
                        dt = obj.Dispatch_Date
                        obj.Tanker_Dispatch_To = clsCommon.myCstr(grow.Cells(colDispatchTo).Value)
                        obj.MCC_Code = clsCommon.myCstr(grow.Cells(colMccCode).Value)
                        obj.MCC_Name = clsCommon.myCstr(grow.Cells(colMccName).Value)
                        obj.Mcc_Or_Plant_Code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                        obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                        obj.Tanker_KM_Reading = clsCommon.myCstr(grow.Cells(colTankerKM).Value)
                        obj.Drip_Marking = clsCommon.myCstr(grow.Cells(colDripM).Value)
                        obj.Tanker_Full = clsCommon.myCstr(grow.Cells(colTankerFull).Value)
                        obj.Control_Sample = clsCommon.myCstr(grow.Cells(colControlSample).Value)
                        obj.Name_Of_Custodian = clsCommon.myCstr(grow.Cells(colName).Value)
                        obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        obj.UOM_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        obj.UOM_desc = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        obj.Tare_Weight = clsCommon.myCstr(grow.Cells(colTareWeight).Value)
                        obj.Gross_Weight = clsCommon.myCstr(grow.Cells(colGrossWeight).Value)
                        obj.Net_Qty = clsCommon.myCstr(grow.Cells(colNetWeight).Value)
                        obj.Transfer_Price = clsCommon.myCstr(grow.Cells(colTransferPrice).Value)
                        obj.control_sample_fat = clsCommon.myCstr(grow.Cells(colControlSampFat).Value)
                        obj.control_sample_snf = clsCommon.myCstr(grow.Cells(colControlSampSNF).Value)
                        obj.Remarks = clsCommon.myCstr(grow.Cells(colTankerRemarks).Value)
                        obj.PriceCode = clsCommon.myCstr(grow.Cells(colPriceChart).Value)
                        obj.FAT_W = clsCommon.myCdbl(grow.Cells(colFat_W).Value)
                        obj.SNF_W = clsCommon.myCdbl(grow.Cells(colSNF_W).Value)
                        obj.FAT_R = clsCommon.myCdbl(grow.Cells(colFat_R).Value)
                        obj.SNF_R = clsCommon.myCdbl(grow.Cells(colSNF_R).Value)
                        obj.FAT_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                        obj.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        obj.FAT_RATE = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                        obj.SNF_RATE = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                        obj.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                        obj.IsAgainstJobWork = clsCommon.myCstr(grow.Cells(colIsJobWork).Value)
                        obj.Sublocation_Code = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)


                        Dim jj As Integer = 0
                        jj = intStartParam
                        Dim intTDLastColumn As Integer = 0
                        intTDLastColumn = intStartParam + paramcount

                        Dim objParam As New Mcc_Dispatch_Chalan_Parameter
                        obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
                        For ii As Integer = jj To intTDLastColumn - 1
                            'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ii).Value).Trim) > 0 Then
                            Dim objImportTemp As clsImportTemp = TryCast(gv1.Columns(ii).Tag, clsImportTemp)

                            objParam = New Mcc_Dispatch_Chalan_Parameter
                            objParam.Param_Field_Code = objImportTemp.Code
                            objParam.Param_Field_Desc = objImportTemp.Description
                            objParam.Param_Field_Value = clsCommon.myCstr(grow.Cells(ii).Value)
                            objParam.Param_Type = objImportTemp.Type
                            obj.arrParmValue.Add(objParam)
                            'End If
                        Next

                        clsMccDispatch.SaveData(obj, trans, 0, False, "Test")
                        Dim ChallanNo As String = obj.Chalan_NO

                        If (clsMccDispatch.PostData(MyBase.Form_ID, ChallanNo, trans)) Then
                            ' Gate Entry start here
                            Dim strJobLoc = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)
                            obj = New clsGateEntry()
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells(colIsJobWork).Value), "1") = CompairStringResult.Equal Then
                                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.MCCProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.MccProc, grow.Cells(colMccOrPlantCOde).Value)
                            End If
                            If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                                Throw New Exception("Error in Gate Entry  No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(grow.Cells(colIsJobWork).Value)
                            obj.Sublocation_Code = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)
                            obj.Doc_Type = "MccProc"
                            obj.Date_And_Time = clsCommon.GetPrintDate(grow.Cells(colDispatchDate).Value, "dd/MMM/yyyy hh:mm:ss tt")                          
                            obj.Vendor_Code = ""
                            obj.Vendor_Desc = ""
                            obj.Dispatched_From_Mcc = clsCommon.myCstr(grow.Cells(colMccCode).Value)
                            obj.location_Code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Challan_No = ChallanNo
                            obj.Challan_Date = clsCommon.GetPrintDate(grow.Cells(colDispatchDate).Value, "dd/MMM/yyyy")
                            obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            'Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(obj.Item_Code, trans)
                            obj.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            obj.Qty_In_Kg = clsCommon.myCdbl(grow.Cells(colNetWeight).Value)
                            obj.fat_per = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                            obj.snf_Per = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.isNewEntry = True
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                           
                            'objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("Tspl_Gate_Entry_Details", "Created_By", obj.location_Code, "location_Code", trans)
                            clsGateEntry.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim GateEntryNo As String = obj.Gate_Entry_No

                            ' Weighment start here

                            obj = New clsWeighment()
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells(colIsJobWork).Value), "1") = CompairStringResult.Equal Then
                                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.MCCProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.MccProc, grow.Cells(colMccOrPlantCOde).Value)
                            End If
                            If clsCommon.myLen(obj.Weighment_No) <= 0 Then
                                Throw New Exception("Error in Weighment No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(grow.Cells(colIsJobWork).Value)
                            obj.Joblocation_Code = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)
                            obj.Tare_Weight_date = dt
                            obj.Weighment_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            obj.Doc_Type = "MccProc"
                            obj.Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Challan_No = ChallanNo
                            obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Dispatched_From_Mcc = clsCommon.myCstr(grow.Cells(colMccCode).Value)
                            obj.location_Code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Vendor_Code = ""
                            obj.Vendor_Desc = ""
                            obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.Qty_In_Kg = 0
                            obj.snf_Per = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                            obj.fat_per = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                            obj.Gross_Weight = clsCommon.myCdbl(grow.Cells(colWtGross).Value)
                            obj.Tare_Weight = clsCommon.myCdbl(grow.Cells(colWtTare).Value)
                            obj.Net_Weight = clsCommon.myCdbl(grow.Cells(colWtNet).Value)
                            obj.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            obj.Dip_Value = clsCommon.myCdbl(grow.Cells(colDripM).Value)
                            obj.Weighment_Slip_No = ""
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.isNewEntry = True
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            clsWeighment.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim weighmentNo As String = obj.Weighment_No

                            ' Quality check start here
                            obj = New clsQualityCheck()
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells(colIsJobWork).Value), "1") = CompairStringResult.Equal Then
                                obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.MCCProcJobWorkOutward, strJobLoc)
                            Else
                                obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.MccProc, grow.Cells(colMccOrPlantCOde).Value)
                            End If
                            If clsCommon.myLen(obj.QC_No) <= 0 Then
                                Throw New Exception("Error in QC No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(grow.Cells(colIsJobWork).Value)
                            obj.Joblocation_Code = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)
                            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            obj.Doc_Type = "MccProc"
                            obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.QC_In_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Vendor_Code = ""
                            obj.Vendor_Desc = ""
                            obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(grow.Cells(colMccCode).Value)
                            obj.Dispatched_From_Mcc_Desc = clsLocation.GetName(obj.Dispatched_From_Mcc_Code, trans)
                            obj.location_Code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Challan_No = ChallanNo
                            obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Weighment_No = clsCommon.myCstr(weighmentNo)
                            obj.Weighment_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy")
                            obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.Remarks = ""
                            obj.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            obj.Qty_In_Kg = 0
                            obj.snf_Per = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                            obj.fat_per = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                            obj.snf_KG = 0
                            obj.fat_KG = 0
                            obj.Dip_Value = clsCommon.myCdbl(grow.Cells(colDripM).Value)
                            obj.Receipt_Control_FAT = 0
                            obj.Receipt_Control_SNF = 0
                            obj.DeductionAmount = 0
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.isNewEntry = True
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.is_Param_Accepted = 1


                            Dim intQCstartrColumn As Integer = 0
                            intTDLastColumn = intStartParam + paramcount

                            Dim objQCParam As New clsQcParam
                            obj.arrQcParam = New List(Of clsQcParam)
                            For ii As Integer = intTDLastColumn To gv1.Columns.Count - 1
                                'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ii).Value).Trim) > 0 Then
                                Dim objImportTemp As clsImportTemp = TryCast(gv1.Columns(ii).Tag, clsImportTemp)

                                objQCParam = New clsQcParam
                                objQCParam.QC_No = clsCommon.myCstr(obj.QC_No)
                                objQCParam.Param_Field_Code = objImportTemp.Code
                                objQCParam.Param_Field_Desc = objImportTemp.Description
                                objQCParam.Param_Field_Value = clsCommon.myCstr(grow.Cells(ii).Value)
                                objQCParam.Param_Type = objImportTemp.Type
                                obj.arrQcParam.Add(objQCParam)
                                'End If
                            Next
                            clsQualityCheck.saveData(obj, trans)
                            Dim QcNo = obj.QC_No
                            ' unloading start here 

                            obj = New clsUnloading()
                            obj.isNewEntry = True

                            ''  Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")

                            If obj.isNewEntry Then
                                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, grow.Cells(colMccOrPlantCOde).Value)
                                If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                                    Throw New Exception("Error In Unloading  No Genertion")
                                End If
                            End If
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Weighment_No = weighmentNo
                            obj.QC_No = QcNo
                            obj.location_Code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                            obj.Sub_location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & (gv1.Rows(i).Cells(colMccOrPlantCOde).Value) & "'", trans))
                            obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            obj.Qty = 0
                            obj.fat_per = 0
                            obj.snf_Per = 0
                            obj.SNF_KG = 0
                            obj.fat_KG = 0
                            obj.isPosted = 0
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            clsUnloading.saveData(obj, trans)
                            Dim Unloading = obj.Unloading_No

                            'Gate out start here
                            obj = New clsGateOut()
                            obj.isNewEntry = True
                            obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value))
                            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                                Throw New Exception("Error In Document  No Genertion")
                            End If
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Weighment_No = weighmentNo
                            obj.QC_No = QcNo
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            clsGateOut.saveData(obj, trans)

                            'Cleaning start here
                            obj = New clsCleaning()
                            obj.isNewEntry = True
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells(colIsJobWork).Value), "1") = CompairStringResult.Equal Then
                                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Cleaning, clsDocTransactionType.MCCProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Cleaning, clsDocTransactionType.NA, grow.Cells(colMccOrPlantCOde).Value)
                            End If
                            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                                Throw New Exception("Error in Cleaning No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(grow.Cells(colIsJobWork).Value)
                            obj.Joblocation_Code = clsCommon.myCstr(grow.Cells(colJobworkLoc).Value)
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Start_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.End_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Tanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                            obj.Weighment_No = weighmentNo
                            obj.QC_No = QcNo
                            obj.Done_by = clsCommon.myCstr(grow.Cells(colCleaningDoneby).Value)
                            obj.Checked_by = clsCommon.myCstr(grow.Cells(colCleaningCheckedBy).Value)
                            obj.Status = "OK"
                            obj.Remarks = ""
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.InTime = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.OutTime = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            clsCleaning.saveData(obj, trans)
                            'Milk Transfer In
                            obj = New clsMilkTransferIn
                            obj.isNewEntry = True
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells(colIsJobWork).Value), "1") = CompairStringResult.Equal Then
                                obj.Receipt_Challan_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MilkTransferIn, clsDocTransactionType.MCCProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Receipt_Challan_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MilkTransferIn, clsDocTransactionType.NA, grow.Cells(colMccOrPlantCOde).Value)
                            End If
                            If clsCommon.myLen(obj.Receipt_Challan_No) <= 0 Then
                                Throw New Exception("Error in Milk Transfer In genertion")
                            End If
                            obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Dispatch_Challan_No = ChallanNo
                            obj.Weighment_No = weighmentNo
                            obj.Qc_No = QcNo
                            obj.Gate_Entry_no = GateEntryNo
                            obj.location_code = clsCommon.myCstr(grow.Cells(colMccOrPlantCOde).Value)
                            obj.km_reading_receipt = 0
                            obj.Receipt_Control_FAT = 0
                            obj.Receipt_Control_SNF = 0
                            obj.Modified_By = objCommonVar.CurrentUserCode
                            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Comp_Code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            clsMilkTransferIn.saveData(obj, trans)
                            clsMilkTransferIn.postData(obj.Receipt_Challan_No, trans)

                            trans.Commit()

                            grow.Cells(colChallanNo).Value = ChallanNo
                        End If
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

  
   
    Private Sub Import()
        Dim dt1 As DataTable
        Dim variable1 As String = ""
        Dim inputs() As String = {}
        Dim whrCls As String = String.Empty

        whrCls = " where Param_for='MCC' or Param_for='BOTH'"

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        inputs = {"MCC Code", "MCC Name", "Dispatch Date", "Dispatch To", "MCC OR Plant Code", "Tanker No", "Tanker KM Reading", "Drip Marking", "Tanker Full", "Control Sample", "Name of Custodian", "Item Code", "Item Desc", "UOM", "Gross Weight", "Tare Weight", "Transfer Price", "Control Sample Fat", "Control Sample SNF", "Remarks", "Price Chart", "IsJobWork", "JobWork Location", "Weighment Gross Weight", "Weighment Tare Weight", "Cleaning Done by", "Cleaning Checked by"}
        dt1 = clsDBFuncationality.GetDataTable(" select *,description  + ' TD '   + Nature + convert(varchar(1),ismandatory) as descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering ")
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            'inputs = {"MCC Code", "MCC Name", "Dispatch Date", "Dispatch To", "MCC OR Plant Code", "Tanker No", "Tanker KM Reading", "Drip Marking", "Tanker Full", "Control Sample", "Name of Custodian", "Item Code", "Item Desc", "UOM", "Tare Weight", "Gross Weight", "Transfer Price", "Control Sample Fat", "Control Sample SNF", "Remarks", "Price Chart", "IsJobWork", "JobWork Location", "Weighment Tare Weight", "Weighment Gross Weight", "Cleaning Done by", "Cleaning Checked by"}
            For ii As Integer = 0 To dt1.Rows.Count - 1
                If ii <> 0 Then
                    variable1 += ","
                End If
                ReDim Preserve inputs(inputs.Length)
                inputs(inputs.Length - 1) = clsCommon.myCstr(dt1.Rows(ii)("descr")).Trim()
            Next
        End If

        dt1 = clsDBFuncationality.GetDataTable(" select *,description  + ' QC '   + Nature + convert(varchar(1),ismandatory) as descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering ")
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            'inputs = {"MCC Code", "MCC Name", "Dispatch Date", "Dispatch To", "MCC OR Plant Code", "Tanker No", "Tanker KM Reading", "Drip Marking", "Tanker Full", "Control Sample", "Name of Custodian", "Item Code", "Item Desc", "UOM", "Tare Weight", "Gross Weight", "Transfer Price", "Control Sample Fat", "Control Sample SNF", "Remarks", "Price Chart", "IsJobWork", "JobWork Location", "Weighment Tare Weight", "Weighment Gross Weight"}
            variable1 += ","
            For ii As Integer = 0 To dt1.Rows.Count - 1
                If ii <> 0 Then
                    variable1 += ","
                End If
                ReDim Preserve inputs(inputs.Length)
                inputs(inputs.Length - 1) = clsCommon.myCstr(dt1.Rows(ii)("descr")).Trim()
            Next
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)
        transportSql.importExcel(gv, Strs.ToArray())
        Dim linno As Integer = 1
        Try

            If gv.Rows.Count > 0 Then
                Dim i As Integer = 0
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                LoadBlankGrid()
                ResetParameterGrid()
                For Each grow As GridViewRowInfo In gv.Rows
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMccCode).Value = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMccName).Value = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.myCDate(grow.Cells("Dispatch Date").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchTo).Value = clsCommon.myCstr(grow.Cells("Dispatch To").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMccOrPlantCOde).Value = clsCommon.myCstr(grow.Cells("MCC OR Plant Code").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsCommon.myCstr(grow.Cells("Tanker No").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerKM).Value = clsCommon.myCdbl(grow.Cells("Tanker KM Reading").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDripM).Value = clsCommon.myCstr(grow.Cells("Drip Marking").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerFull).Value = clsCommon.myCstr(grow.Cells("Tanker Full").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colControlSample).Value = clsCommon.myCstr(grow.Cells("Control Sample").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colName).Value = clsCommon.myCstr(grow.Cells("Name of Custodian").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(grow.Cells("Item Desc").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(grow.Cells("UOM").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myCdbl(grow.Cells("Tare Weight").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(grow.Cells("Gross Weight").Value)
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myCdbl(grow.Cells("Net Weight").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTransferPrice).Value = clsCommon.myCdbl(grow.Cells("Transfer Price").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colControlSampFat).Value = clsCommon.myCdbl(grow.Cells("Control Sample Fat").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colControlSampSNF).Value = clsCommon.myCdbl(grow.Cells("Control Sample SNF").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceChart).Value = clsCommon.myCstr(grow.Cells("Price Chart").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsJobWork).Value = clsCommon.myCstr(grow.Cells("IsJobWork").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colJobworkLoc).Value = clsCommon.myCstr(grow.Cells("JobWork Location").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerRemarks).Value = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWtTare).Value = clsCommon.myCdbl(grow.Cells("Weighment Tare Weight").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWtGross).Value = clsCommon.myCdbl(grow.Cells("Weighment Gross Weight").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCleaningDoneby).Value = clsCommon.myCstr(grow.Cells("Cleaning Done by").Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCleaningCheckedBy).Value = clsCommon.myCstr(grow.Cells("Cleaning Checked by").Value)
                    Dim j As Integer = 0

                    'pls increase the intStartParam and J no in for loop for no of added columns 
                    intStartParam = 45
                    i = intStartParam

                    For j = 27 To gv.Columns.Count - 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(i).Value = clsCommon.myCstr(grow.Cells(j).Value)
                        i = i + 1
                    Next
                Next
                btnValidate.Enabled = True
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try



    End Sub
    Private Sub Export()
        Dim qry As String = ""
        Dim dt1 As DataTable
        Dim variable1 As String = ""
        Dim whrCls As String = String.Empty
        'If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
        '    whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        'Else
        '    whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        'End If
        whrCls = " where Param_for='MCC' or Param_for='BOTH'"

        dt1 = clsDBFuncationality.GetDataTable(" select *,description  + ' TD '   + Nature + convert(varchar(1),ismandatory) as descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering ")
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For ii As Integer = 0 To dt1.Rows.Count - 1
                If ii <> 0 Then
                    variable1 += ","
                End If
                variable1 += "'' as [" + clsCommon.myCstr(dt1.Rows(ii)("descr")).Trim() + "]"
            Next
        End If

        dt1 = clsDBFuncationality.GetDataTable(" select *,description  + ' QC '   + Nature + convert(varchar(1),ismandatory) as descr,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering ")
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            variable1 += ","
            For ii As Integer = 0 To dt1.Rows.Count - 1
                If ii <> 0 Then
                    variable1 += ","
                End If
                variable1 += "'' as [" + clsCommon.myCstr(dt1.Rows(ii)("descr")).Trim() + "]"
            Next
        End If
        qry = "select top 1 MCC_Code as [MCC Code],MCC_Name as [MCC Name],convert(varchar(12),Dispatch_Date,103) as [Dispatch Date],Tanker_Dispatch_To as [Dispatch To],Mcc_Or_Plant_Code as [MCC OR Plant Code],Tanker_No as [Tanker No],Tanker_KM_Reading as [Tanker KM Reading]"
        qry += " ,Drip_Marking as [Drip Marking],Tanker_Full as [Tanker Full],Control_Sample as [Control Sample],Name_Of_Custodian as [Name of Custodian],Item_Code as [Item Code],Item_Desc as [Item Desc],UOM_Code as [UOM],Gross_Weight as [Gross Weight],Tare_Weight as [Tare Weight],Transfer_Price as [Transfer Price],control_sample_fat as [Control Sample Fat],control_sample_snf as [Control Sample SNF],Remarks as Remarks,PriceCode as [Price Chart],IsAgainstJobWork as IsJobWork, " & _
            "Sublocation_Code as [JobWork Location],Gross_Weight as [Weighment Gross Weight],Tare_Weight as [Weighment Tare Weight],'' as [Cleaning Done by],'' as [Cleaning Checked by], " + variable1 + ""
        qry += " from TSPL_MCC_Dispatch_Challan  "
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub
  
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()
    End Sub

    Private Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        AddNew()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Import()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
End Class
