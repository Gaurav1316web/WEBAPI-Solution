'' against Ticket No: BM00000007851,BM00000008216 by Pankaj Jha
''UPDATION BY RICHA AGARWAL AGAINST TICKET NO. BM00000007897,BM00000007907,BM00000008004,BM00000008087,BM00000008141
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmBulkPurchaseUploader
    Inherits FrmMainTranScreen
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Public Const colSlno As String = "colSlno"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colDispatchCode As String = "colDispatchCode"
    Public Const colSNRNO As String = "colSNRNO"
    Public TextCol As GridViewTextBoxColumn = Nothing
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing
    Dim arrVendorInvoiceNo As List(Of String) = Nothing

    Private Sub frmBulkPurchaseUploader_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 And rdbAgainstBulkprocurement.IsChecked Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnMergeAndRecreate.Visible = True
            End If
        End If
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 And rdbAgainstBulkSaleTrade.IsChecked Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnMergeAndRecreateTrade.Visible = True
            End If
        End If
    End Sub
    Private Sub frmBulkPurchaseUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        Gv1.Visible = True
        btnMergeAndRecreate.Visible = False
        btnMergeAndRecreateTrade.Visible = False
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        'Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = Nothing
        If rdbAgainstBulkprocurement.IsChecked Then
            If transportSql.importExcel(Gv1, "Party Code / Vendor Code", "Gate_Entry_DATE", "Location code", "TankerNo.", "Item code", "GROSS WEIGHT", "DIP Value", "TARE WEIGHT", "Net_MILK QTY.", "PM00001(FAT %)", "PM00002(SNF %)", "PM00003(CLR)", "PM14-151(RM VALUE)", "PM14-1510(Acidity (B.B))", "PM14-1511(Temprature ° C)", "PM14-1512(Alcohol)", "PM14-1513(ACI 8.5% SNF)", "PM14-1515(Taste)", "PM14-1516(Chenna %)", "PM14-1517(B.R. Reading)", "PM14-1518(Detergent)", "PM14-1519(Acidity (A.B))", "PM14-152(FFA%)", "PM14-1520(Adultration)", "PM14-1521(Flavour)", "PM14-153(PROTEIN)", "PM14-154(NA+ PPM)", "PM14-155(K PPM)", "PM14-156(MILK ASH %)", "PM14-157(SUGAR)", "PM14-158(MALTOSE)", "PM14-159(GLUCOSE)", "Silo No", "SRN Price Chart Code", "FAT Ratio", "SNF Ratio", "FAT Weightage", "SNF Weightage", "Purchase_Rate", "Purchase_Amount", "FAT Qty.", "SNF Qty", "Fat Value", "SNF Value", "Vendor Invoice No", "IsJobWork", "JobWork Location") Then
                If Gv1.Columns.Count > 0 Then
                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colSlno
                    TextCol.HeaderText = "SL. No."
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(0, TextCol)

                    ChkBoxColumn = New GridViewCheckBoxColumn()
                    ChkBoxColumn.Name = colIsValidated
                    ChkBoxColumn.HeaderText = "Validated"
                    ChkBoxColumn.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colErrorStatus
                    TextCol.HeaderText = "Error Status"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(2, TextCol)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colDispatchCode
                    TextCol.HeaderText = "SRNNo"
                    TextCol.ReadOnly = True
                    TextCol.IsVisible = False
                    Gv1.MasterTemplate.Columns.Insert(3, TextCol)

                    'TryCast(Gv1.Columns("Gate_Entry_DATE"), GridViewDataColumn).CustomFormat = "dd/MM/yyyy"
                    'TryCast(Gv1.Columns("Gate_Entry_DATE"), GridViewDataColumn).FormatString = "{0:dd/MM/yyyy}"

                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells(colSlno).Value = (i + 1)
                        Gv1.Rows(i).Cells(colIsValidated).Value = False
                        ValidatedCount = 0
                        Gv1.Rows(i).Cells(colErrorStatus).Value = ""
                        Gv1.Rows(i).Cells("Gate_Entry_DATE").Value = clsCommon.GetPrintDate(Gv1.Rows(i).Cells("Gate_Entry_DATE").Value, "dd/MM/yyyy")
                    Next
                    For i As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(i).ReadOnly = True
                    Next
                    Gv1.AllowAddNewRow = False
                    Gv1.AllowDeleteRow = True
                    Gv1.EnableFiltering = True
                    Gv1.EnableSorting = False
                    Gv1.EnableGrouping = False
                    Gv1.AllowColumnChooser = False
                    Gv1.AllowColumnReorder = True
                    Gv1.BestFitColumns()
                    Gv1.AutoSizeRows = True
                    Gv1.TableElement.TableHeaderHeight = 30
                End If
            End If
        ElseIf rdbAgainstBulkSale.IsChecked Then
            If transportSql.importExcel(Gv1, "Customer code", "Location Code", "Gate_entry_Date", "Sale_TankerNo.", "Item Code", "UOM", "GROSS WEIGHT", "TARE WEIGHT", "Sale_MILK QTY.", "SILO No", "Sale_Fat %", "Sale_SNF %", "Sale_Rate", "Sale_Amount", "FAT Qty.", "SNF Qty", "Fat Per Kg", "Snf Per Kg", "Fat Value", "SNF Value", "FAT Rate", "SNF Rate", "Sale Price Code") Then
                If Gv1.Columns.Count > 0 Then
                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colSlno
                    TextCol.HeaderText = "SL. No."
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(0, TextCol)

                    ChkBoxColumn = New GridViewCheckBoxColumn()
                    ChkBoxColumn.Name = colIsValidated
                    ChkBoxColumn.HeaderText = "Validated"
                    ChkBoxColumn.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colErrorStatus
                    TextCol.HeaderText = "Error Status"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(2, TextCol)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colDispatchCode
                    TextCol.HeaderText = "DispatchCode"
                    TextCol.ReadOnly = True
                    TextCol.IsVisible = False
                    Gv1.MasterTemplate.Columns.Insert(3, TextCol)


                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells(colSlno).Value = (i + 1)
                        Gv1.Rows(i).Cells(colIsValidated).Value = False
                        ValidatedCount = 0
                        Gv1.Rows(i).Cells(colErrorStatus).Value = ""
                    Next
                    For i As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(i).ReadOnly = True
                    Next
                    Gv1.AllowAddNewRow = False
                    Gv1.AllowDeleteRow = True
                    Gv1.EnableFiltering = True
                    Gv1.EnableSorting = False
                    Gv1.EnableGrouping = False
                    Gv1.AllowColumnChooser = False
                    Gv1.AllowColumnReorder = True
                    Gv1.BestFitColumns()
                    Gv1.AutoSizeRows = True
                    Gv1.TableElement.TableHeaderHeight = 30
                End If
            End If
        Else
            If transportSql.importExcel(Gv1, "Customer code", "Location Code", "Document_Date", "TankerNo.", "Item Code", "Dispatch Price Code", "MILK QTY.", "Sale_Fat %", "Sale_SNF %", "Sale_Rate", "Dispatch_Sale_Amount", "Dispatch FAT Qty.", "Dispatch SNF Qty", "Dispatch Fat Per Kg", "Dispatch Snf Per Kg", "Dispatch Fat Value", "Dispatch SNF Value", "Dispatch Rate/100Kg.", "Dispatch FAT%", "Dispatch SNF%", "Dispatch FAT", "Dispatch SNF", "Vendor_Code", "Vendor Bill No", "SRN Price Code", "SRN FAT Qty.", "SRN SNF Qty", "SRN Fat Per Kg", "SRN Snf Per Kg", "SRN Fat Value", "SRN SNF Value", "SRN Sale Amount", "SRN Rate/100Kg.", "SRN FAT%", "SRN SNF%", "SRN FAT", "SRN SNF", "SRN Standard Rate") Then
                If Gv1.Columns.Count > 0 Then
                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colSlno
                    TextCol.HeaderText = "SL. No."
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(0, TextCol)

                    ChkBoxColumn = New GridViewCheckBoxColumn()
                    ChkBoxColumn.Name = colIsValidated
                    ChkBoxColumn.HeaderText = "Validated"
                    ChkBoxColumn.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colErrorStatus
                    TextCol.HeaderText = "Error Status"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(2, TextCol)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colDispatchCode
                    TextCol.HeaderText = "DispatchCode"
                    TextCol.ReadOnly = True
                    TextCol.IsVisible = False
                    Gv1.MasterTemplate.Columns.Insert(3, TextCol)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colSNRNO
                    TextCol.HeaderText = "SrnNo"
                    TextCol.ReadOnly = True
                    TextCol.IsVisible = False
                    Gv1.MasterTemplate.Columns.Insert(4, TextCol)

                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells(colSlno).Value = (i + 1)
                        Gv1.Rows(i).Cells(colIsValidated).Value = False
                        ValidatedCount = 0
                        Gv1.Rows(i).Cells(colErrorStatus).Value = ""
                    Next
                    For i As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(i).ReadOnly = True
                    Next
                    Gv1.AllowAddNewRow = False
                    Gv1.AllowDeleteRow = True
                    Gv1.EnableFiltering = True
                    Gv1.EnableSorting = False
                    Gv1.EnableGrouping = False
                    Gv1.AllowColumnChooser = False
                    Gv1.AllowColumnReorder = True
                    Gv1.BestFitColumns()
                    Gv1.AutoSizeRows = True
                    Gv1.TableElement.TableHeaderHeight = 30
                End If
            End If
        End If
    End Sub
    Sub LoadBlankGrid()

    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()
    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim qry As String = String.Empty
        If rdbAgainstBulkprocurement.IsChecked Then
            qry = "select '' As [Party Code / Vendor Code],	'' As [Gate_Entry_DATE],	'' As [Location code],	'' As [TankerNo.],	'' As [Item code],	'' As [GROSS WEIGHT],	'' As [DIP Value],	'' As [TARE WEIGHT],	'' As [Net_MILK QTY.],	'' As [PM00001(FAT %)],	'' As [PM00002(SNF %)],	'' As [PM00003(CLR)],	'' As [PM14-151(RM VALUE)],	'' As [PM14-1510(Acidity (B.B))],	'' As [PM14-1511(Temprature ° C)],	'' As [PM14-1512(Alcohol)],	'' As [PM14-1513(ACI 8.5% SNF)],	'' As [PM14-1515(Taste)],	'' As [PM14-1516(Chenna %)],	'' As [PM14-1517(B.R. Reading)],	'' As [PM14-1518(Detergent)],	'' As [PM14-1519(Acidity (A.B))],	'' As [PM14-152(FFA%)],	'' As [PM14-1520(Adultration)],	'' As [PM14-1521(Flavour)],	'' As [PM14-153(PROTEIN)],	'' As [PM14-154(NA+ PPM)],	'' As [PM14-155(K PPM)],	'' As [PM14-156(MILK ASH %)],	'' As [PM14-157(SUGAR)],	'' As [PM14-158(MALTOSE)],	'' As [PM14-159(GLUCOSE)],	'' As [Silo No],	'' As [SRN Price Chart Code],	'' As [FAT Ratio],	'' As [SNF Ratio],	'' As [FAT Weightage],	'' As [SNF Weightage],	'' As [Purchase_Rate],	'' As [Purchase_Amount],	'' As [FAT Qty.],	'' As [SNF Qty],	'' As [Fat Value],	'' As [SNF Value],	'' As [Vendor Invoice No] , '0' as IsJobWork,'' as [JobWork Location]"
            transportSql.ExporttoExcel(qry, Me)
        ElseIf rdbAgainstBulkSale.IsChecked Then
            qry = "select '' As [Customer code], '' As [Location Code], '' As [Gate_entry_Date], '' As [Sale_TankerNo.], '' As [Item Code],'' as UOM, '' As [GROSS WEIGHT], '' As [TARE WEIGHT], '' As [Sale_MILK QTY.], '' As [SILO No], '' As [Sale_Fat %], '' As [Sale_SNF %], '' As [Sale_Rate], '' As [Sale_Amount], '' As [FAT Qty.], '' As [SNF Qty], '' As [Fat Per Kg], '' As [Snf Per Kg], '' As [Fat Value], '' As [SNF Value],'' AS [FAT Rate],'' AS [SNF Rate],  '' As [Sale Price Code]"
            transportSql.ExporttoExcel(qry, Me)
        Else
            qry = "select '' As [Customer code],'' as [Location Code],'' as [Document_Date],'' as [TankerNo.],'' as [Item Code],'' as [Dispatch Price Code],'' as [MILK QTY.],'' as [Sale_Fat %],'' as [Sale_SNF %],'' as [Sale_Rate],'' as [Dispatch_Sale_Amount],'' as [Dispatch FAT Qty.],'' as [Dispatch SNF Qty],'' as [Dispatch Fat Per Kg],'' as [Dispatch Snf Per Kg],'' as [Dispatch Fat Value],'' as [Dispatch SNF Value],'' as [Dispatch Rate/100Kg.],'' as [Dispatch FAT%],'' as [Dispatch SNF%],'' as [Dispatch FAT],'' as [Dispatch SNF],'' as [Vendor_Code],'' as [Vendor Bill No],'' as [SRN Price Code],'' as [SRN FAT Qty.],'' as [SRN SNF Qty],'' as [SRN Fat Per Kg],'' as [SRN Snf Per Kg],'' as [SRN Fat Value],'' as [SRN SNF Value],'' as [SRN Sale Amount],'' as [SRN Rate/100Kg.],'' as [SRN FAT%],'' as [SRN SNF%],'' as [SRN FAT],'' as [SRN SNF],'' as [SRN Standard Rate]"
            transportSql.ExporttoExcel(qry, Me)
        End If
        qry = Nothing
    End Sub

    Private Sub btnExportInvalid_Click(sender As Object, e As EventArgs) Handles btnExportInvalid.Click
        If rdbAgainstBulkprocurement.IsChecked Then
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            transportSql.exportdata(Gv1, dirName & "\InvalidBulkMilkPurchaseUploderData.xlsx", "Sheet1")
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            Process.Start(dirName & "\InvalidBulkMilkPurchaseUploderData.xlsx")

        ElseIf rdbAgainstBulkSale.IsChecked Then
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            transportSql.exportdata(Gv1, dirName & "\InvalidBulkSaleUploderData.xlsx", "Sheet1")
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            Process.Start(dirName & "\InvalidBulkSaleUploderData.xlsx")
        Else
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            transportSql.exportdata(Gv1, dirName & "\InvalidBulkSaleTradeUploderData.xlsx", "Sheet1")
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            Process.Start(dirName & "\InvalidBulkSaleTradeUploderData.xlsx")
        End If
    End Sub

    Private Sub btnSaveAndPost_Click(sender As Object, e As EventArgs) Handles btnSaveAndPost.Click
        SaveAndPost()
    End Sub
    Sub CheckAndValidate()
        Dim ValidateStatus As String = String.Empty
        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("There are no row is grid please select a sheet to import")
        End If
        If ValidatedCount = Gv1.Rows.Count Then
            clsCommon.MyMessageBoxShow("All Rows are already validated")
            Exit Sub
        End If
        ValidatedCount = 0
        Dim strCellValue
        If rdbAgainstBulkprocurement.IsChecked Then
            For i As Integer = 0 To Gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""
                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Party Code/Vendor Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_vendor_master where vendor_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Party Code/Vendor Code not found in master" & Environment.NewLine
                End If
                Dim VendorType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(vendor_type,'')  as vendor_type from tspl_vendor_master where vendor_code='" & strCellValue & "'"))
                If clsCommon.CompairString(VendorType, "A") = CompairStringResult.Equal Or clsCommon.CompairString(VendorType, "B") = CompairStringResult.Equal Or clsCommon.CompairString(VendorType, "C") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Party/Vendor Type must be in A/B/C " & Environment.NewLine
                End If

                strCellValue = clsCommon.myCDate(Gv1.Rows(i).Cells("Gate_Entry_DATE").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Gate Entry Date Time Must not be Blank" & Environment.NewLine
                End If
                If IsDate(strCellValue) Then
                Else
                    ValidateStatus = ValidateStatus & "Gate Entry Date Time Must  be a Date Time Value" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code not found in master" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Tanker No. Must not be Blank" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains(" ") Then
                    ValidateStatus = ValidateStatus & "Tanker No Must not contain any Blank Space" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains("!") OrElse strCellValue.ToString.Contains("@") OrElse strCellValue.ToString.Contains("#") OrElse strCellValue.ToString.Contains("$") OrElse strCellValue.ToString.Contains("%") OrElse strCellValue.ToString.Contains("^") OrElse strCellValue.ToString.Contains("&") OrElse strCellValue.ToString.Contains("*") OrElse strCellValue.ToString.Contains("(") OrElse strCellValue.ToString.Contains(")") OrElse strCellValue.ToString.Contains("_") OrElse strCellValue.ToString.Contains("-") OrElse strCellValue.ToString.Contains("+") OrElse strCellValue.ToString.Contains("=") OrElse strCellValue.ToString.Contains("\") OrElse strCellValue.ToString.Contains("|") OrElse strCellValue.ToString.Contains("/") OrElse strCellValue.ToString.Contains("?") OrElse strCellValue.ToString.Contains(">") OrElse strCellValue.ToString.Contains("<") OrElse strCellValue.ToString.Contains(".") OrElse strCellValue.ToString.Contains("]") OrElse strCellValue.ToString.Contains("[") OrElse strCellValue.ToString.Contains("{") OrElse strCellValue.ToString.Contains("}") Then
                    ValidateStatus = ValidateStatus & "Tanker No Must not contain any Special Symbol" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
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

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Gross Weight Must not be Zero or Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Tare Weight Must not be Zero or Negative" & Environment.NewLine
                End If

                If strCellValue > clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) Then
                    ValidateStatus = ValidateStatus & "Tare Weight Must not be Larger Then Gross Weight" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Net Weight Must not be Zero or Negative" & Environment.NewLine
                End If

                If strCellValue > clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) Then
                    ValidateStatus = ValidateStatus & "Net Weight Must not be Larger Then Gross Weight" & Environment.NewLine
                End If

                Dim netWeight As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) - clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                If strCellValue <> netWeight Then
                    ValidateStatus = ValidateStatus & "Net Weight Should be " & netWeight & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("DIP Value").Value)
                If strCellValue < 0 Then
                    ValidateStatus = ValidateStatus & "Dip Value Must not be Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("PM00001(FAT %)").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "PM00001(FAT %) Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("PM00002(SNF %)").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "PM00002(SNF %) Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("PM00003(CLR)").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "PM00003(CLR) Value Must not be Negative or Zero" & Environment.NewLine
                End If
                Dim paramCode As String = ""


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-151(RM VALUE)").Value)
                paramCode = "PM14-151"
                Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-151(RM VALUE) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-151(RM VALUE), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-151(RM VALUE), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1510(Acidity (B.B))").Value)
                paramCode = "PM14-1510"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1510(Acidity (B.B)) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1510(Acidity (B.B)), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1510(Acidity (B.B)), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If



                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1511(Temprature ° C)").Value)
                paramCode = "PM14-1511"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1511(Temprature ° C) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1511(Temprature ° C), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1511(Temprature ° C), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1512(Alcohol)").Value)
                paramCode = "PM14-1512"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1512(Alcohol) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1512(Alcohol), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1512(Alcohol), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1513(ACI 8.5% SNF)").Value)
                paramCode = "PM14-1513"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1513(ACI 8.5% SNF) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1513(ACI 8.5% SNF), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1513(ACI 8.5% SNF), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1515(Taste)").Value)
                paramCode = "PM14-1515"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1515(Taste) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1515(Taste), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1515(Taste), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If



                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1516(Chenna %)").Value)
                paramCode = "PM14-1516"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1516(Chenna %) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1516(Chenna %), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1516(Chenna %), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1517(B.R. Reading)").Value)
                paramCode = "PM14-1517"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1517(B.R. Reading) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1517(B.R. Reading), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1517(B.R. Reading), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1518(Detergent)").Value)
                paramCode = "PM14-1518"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1518(Detergent) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1518(Detergent), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1518(Detergent), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1519(Acidity (A.B))").Value)
                paramCode = "PM14-1519"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1519(Acidity (A.B)) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1519(Acidity (A.B)), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1519(Acidity (A.B)), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-152(FFA%)").Value)
                paramCode = "PM14-152"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-152(FFA%) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-152(FFA%), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-152(FFA%), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1520(Adultration)").Value)
                paramCode = "PM14-1520"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1520(Adultration) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1520(Adultration), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1520(Adultration), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1521(Flavour)").Value)
                paramCode = "PM14-1521"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-1521(Flavour) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-1521(Flavour), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-1521(Flavour), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-153(PROTEIN)").Value)
                paramCode = "PM14-153"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-153(PROTEIN) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-153(PROTEIN), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-153(PROTEIN), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-154(NA+ PPM)").Value)
                paramCode = "PM14-154"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-154(NA+ PPM) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-154(NA+ PPM), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-154(NA+ PPM), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-155(K PPM)").Value)
                paramCode = "PM14-155"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-155(K PPM) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-155(K PPM), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-155(K PPM), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-156(MILK ASH %)").Value)
                paramCode = "PM14-156"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-156(MILK ASH %) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-156(MILK ASH %), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-156(MILK ASH %), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If



                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-157(SUGAR)").Value)
                paramCode = "PM14-157"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-157(SUGAR) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-157(SUGAR), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-157(SUGAR), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-158(MALTOSE)").Value)
                paramCode = "PM14-158"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-158(MALTOSE) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-158(MALTOSE), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-158(MALTOSE), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If



                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-159(GLUCOSE)").Value)
                paramCode = "PM14-159"
                nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from  tspl_parameter_master where Code='" & paramCode & "'"))
                If clsCommon.myLen(strCellValue) > 0 Then
                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal Then
                        Else
                            ValidateStatus = ValidateStatus & "PM14-159(GLUCOSE) Value Must be either Yes/No" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & paramCode & "' and Value='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "PM14-159(GLUCOSE), the value specified not found in parameter value master" & Environment.NewLine
                        End If
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If Not IsNumeric(strCellValue) Then
                            ValidateStatus = ValidateStatus & "PM14-159(GLUCOSE), must be Numeric" & Environment.NewLine
                        End If
                    End If
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Silo No").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Silo No Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Silo No not found in master" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where isnull(Is_Sub_Location,'')='Y' and Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & "' and Location_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Invalid Silo No or Not For Location " & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & Environment.NewLine
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SRN Price Chart Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Price Chart Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Price Chart Code not found in master" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_vendor_price_chart_mapping where isnull(priceCode,'')='" & strCellValue & "' and vendorCode='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Invalid SRN Price Chart Code or Not For Vendor " & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Rate").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Purchase_Rate").Value) Then
                    ValidateStatus = ValidateStatus & "Purchase_Rate Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Purchase_Rate Must not be negative or Zero" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Amount").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Purchase_Amount").Value) Then
                    ValidateStatus = ValidateStatus & "Purchase_Amount Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Purchase_Amount Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("FAT Qty.").Value) Then
                    ValidateStatus = ValidateStatus & "FAT Qty. Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "FAT Qty. Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("SNF Qty").Value) Then
                    ValidateStatus = ValidateStatus & "SNF Qty Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Qty Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Fat Value").Value) Then
                    ValidateStatus = ValidateStatus & "Fat Value Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat Value Must not be negative or Zero" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("SNF Value").Value) Then
                    ValidateStatus = ValidateStatus & "SNF Value Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Value Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Invoice No").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Vendor Invoice No Must not be Blank " & Environment.NewLine
                End If

               
                If AllowJobWorkonGateEntryBulkProc = 0 Then
                    Gv1.Rows(i).Cells("IsJobWork").Value = 0
                    Gv1.Rows(i).Cells("JobWork Location").Value = ""
                Else
                    Dim strLocation As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                    Dim strJobWork As String = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                    Dim strJobWorkLoc As String = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
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
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Main_Location_Code='" & strLocation & "' and is_sub_location='Y' and Location_Type='Virtual' and UseInJobWork=1  ")) <= 0 Then
                            ValidateStatus = ValidateStatus & "Virtual Location code not found in master for Location " & strLocation & " " & Environment.NewLine
                        End If

                    Else
                        Gv1.Rows(i).Cells("JobWork Location").Value = ""
                    End If
                End If
                Dim vendorInvoiceNo As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Invoice No").Value)
                Dim LocCode As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                Dim VenCode As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                Dim isVenFlag As Boolean = False
                Dim isLovFlag As Boolean = False
                For jj As Integer = 0 To Gv1.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells("Vendor Invoice No").Value), vendorInvoiceNo) = CompairStringResult.Equal Then
                        If Not isVenFlag Then
                            If clsCommon.CompairString(Gv1.Rows(jj).Cells("Party Code / Vendor Code").Value, VenCode) <> CompairStringResult.Equal Then
                                ValidateStatus = ValidateStatus & "Vendor Code Must be Same if Vendor Invoice Number is same for all the records " & Environment.NewLine
                                isVenFlag = True
                            End If
                        End If
                        If Not isLovFlag Then
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells("Location code").Value), LocCode) <> CompairStringResult.Equal Then
                                ValidateStatus = ValidateStatus & "Location Code Must be Same if Vendor Invoice Number is same, for all the records  " & Environment.NewLine
                                isLovFlag = True
                            End If
                        End If
                    End If
                Next

                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsVendorInvoiceNo ,'') from tspl_vendor_master where Vendor_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & "'")) = 1 Then
                '    If clsCommon.myLen(strCellValue) <= 0 Then
                '        ValidateStatus = ValidateStatus & "Vendor Invoice No Must not be Blank Because this vendor provides Vendor Invoice No." & Environment.NewLine
                '    End If
                'End If

                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    Gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    'Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    'Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.White
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                Else
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    'Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    'Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.Blue
                    Gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                End If
                'Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                'Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                'Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
            Next
        ElseIf rdbAgainstBulkSale.IsChecked Then
            For i As Integer = 0 To Gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""
                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Customer code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code not found in master" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code not found in master" & Environment.NewLine
                End If

                Dim LocationType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Location_Type,'')  as Location_Type from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(LocationType, "Physical") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Location Type must be Physical " & Environment.NewLine
                End If

                Dim Is_Sub_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Is_Sub_Location,'')  as Is_Sub_Location from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(Is_Sub_Location, "N") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Location should not be Sub Location" & Environment.NewLine
                End If

                Dim Is_Section As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Is_Section,'')  as Is_Section from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(Is_Section, "N") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Location should not be Section" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Gate_entry_Date").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Gate Entry Date Time Must not be Blank" & Environment.NewLine
                End If
                If IsDate(strCellValue) Then
                Else
                    ValidateStatus = ValidateStatus & "Gate Entry Date Time Must  be a Date Time Value" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Sale_TankerNo.").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale Tanker No. Must not be Blank" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains(" ") Then
                    ValidateStatus = ValidateStatus & "Sale Tanker No Must not contain any Blank Space" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains("!") OrElse strCellValue.ToString.Contains("@") OrElse strCellValue.ToString.Contains("#") OrElse strCellValue.ToString.Contains("$") OrElse strCellValue.ToString.Contains("%") OrElse strCellValue.ToString.Contains("^") OrElse strCellValue.ToString.Contains("&") OrElse strCellValue.ToString.Contains("*") OrElse strCellValue.ToString.Contains("(") OrElse strCellValue.ToString.Contains(")") OrElse strCellValue.ToString.Contains("_") OrElse strCellValue.ToString.Contains("-") OrElse strCellValue.ToString.Contains("+") OrElse strCellValue.ToString.Contains("=") OrElse strCellValue.ToString.Contains("\") OrElse strCellValue.ToString.Contains("|") OrElse strCellValue.ToString.Contains("/") OrElse strCellValue.ToString.Contains("?") OrElse strCellValue.ToString.Contains(">") OrElse strCellValue.ToString.Contains("<") OrElse strCellValue.ToString.Contains(".") OrElse strCellValue.ToString.Contains("]") OrElse strCellValue.ToString.Contains("[") OrElse strCellValue.ToString.Contains("{") OrElse strCellValue.ToString.Contains("}") Then
                    ValidateStatus = ValidateStatus & "Sale Tanker No Must not contain any Special Symbol" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value)
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

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Gross Weight Must not be Zero or Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Tare Weight Must not be Zero or Negative" & Environment.NewLine
                End If

                If strCellValue > clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) Then
                    ValidateStatus = ValidateStatus & "Tare Weight Must not be Larger Then Gross Weight" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_MILK QTY.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_MILK QTY. Must not be Zero or Negative" & Environment.NewLine
                End If

                If strCellValue > clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) Then
                    ValidateStatus = ValidateStatus & "Sale_MILK QTY. Must not be Larger Then Gross Weight" & Environment.NewLine
                End If

                Dim netWeight As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value) - clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                If strCellValue <> netWeight Then
                    ValidateStatus = ValidateStatus & "Sale_MILK QTY. Should be " & netWeight & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SILO No").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "SILO No Must not be Blank" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Silo No not found in master" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where isnull(Is_Sub_Location,'')='Y' and Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & "' and Location_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Invalid Silo No or Not For Location " & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Sale Price Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale Price Code Must not be Blank" & Environment.NewLine
                End If

                ''richa agarwal 12 Sep, 2016
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" & strCellValue & "'  and TSPL_BulkSalePrice_MASTER.Posted='1'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Sale Price Code not found in master" & Environment.NewLine
                    End If
                Else
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Sale Price Code not found in master" & Environment.NewLine
                    End If
                End If

                Dim objP As ClsBulkSalePriceChart = ClsBulkSalePriceChart.GetData(clsCommon.myCstr(Gv1.Rows(i).Cells("Sale Price Code").Value), NavigatorType.Current)

                Dim FatW As Double = objP.Fat_Weightage
                Dim SNFW As Double = objP.Snf_Weightage
                Dim FATRATE As Double = objP.FatRate
                Dim SNFRATE As Double = objP.SNFRate
                Dim Rate As Double = objP.Standard_Rate
                Gv1.Rows(i).Cells("FAT Rate").Value = Math.Round(FATRATE, 2)
                Gv1.Rows(i).Cells("SNF Rate").Value = Math.Round(SNFRATE, 2)
                Gv1.Rows(i).Cells("Sale_Rate").Value = Math.Round(Rate, 2)

                Dim FATPer As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Fat %").Value)
                Dim SNFPer As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_SNF %").Value)
                If FATPer > 0 Then
                    Gv1.Rows(i).Cells("FAT Qty.").Value = Math.Round(netWeight * FATPer / 100, 2)
                    Gv1.Rows(i).Cells("Sale_Fat %").Value = Math.Round(FATPer, 2)
                End If
                If SNFPer > 0 Then
                    Gv1.Rows(i).Cells("SNF Qty").Value = Math.Round(netWeight * SNFPer / 100, 2)
                    Gv1.Rows(i).Cells("Sale_SNF %").Value = Math.Round(SNFPer, 2)
                End If
                Dim FATQty As Double = Gv1.Rows(i).Cells("FAT Qty.").Value
                Dim SNFQty As Double = Gv1.Rows(i).Cells("SNF Qty").Value

                Dim ConversionFactor As Double = clsDBFuncationality.getSingleValue("select  Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code = '" & Gv1.Rows(i).Cells("Item Code").Value & "' and UOM_Code = '" & Gv1.Rows(i).Cells("UOM").Value & "'")

                Gv1.Rows(i).Cells("Fat Per Kg").Value = Math.Round(netWeight * ConversionFactor * FATPer / 100, 2)
                Gv1.Rows(i).Cells("Snf Per Kg").Value = Math.Round(netWeight * ConversionFactor * SNFPer / 100, 2)

                Gv1.Rows(i).Cells("Sale_Amount").Value = Math.Round((Gv1.Rows(i).Cells("FAT Rate").Value * Gv1.Rows(i).Cells("Fat Per Kg").Value) + (Gv1.Rows(i).Cells("SNF Rate").Value * Gv1.Rows(i).Cells("Snf Per Kg").Value), 2)

                Gv1.Rows(i).Cells("Fat Value").Value = Math.Round(FATRATE * Gv1.Rows(i).Cells("Fat Per Kg").Value, 2)
                Gv1.Rows(i).Cells("SNF Value").Value = Math.Round(SNFRATE * Gv1.Rows(i).Cells("Snf Per Kg").Value, 2)

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Fat %").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_Fat % Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_SNF %").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_SNF % Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Rate").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_Rate Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Amount").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_Amount Value Must not be Negative or Zero" & Environment.NewLine
                End If
                Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))
                If AmountLimitInvoiceBulkSale > 0 Then
                    If strCellValue > AmountLimitInvoiceBulkSale Then
                        ValidateStatus = ValidateStatus & "Sale_Amount Value should not be greater than Bulk Sale Invoice Amount Limit." & Environment.NewLine
                    End If
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "FAT Qty. Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Qty Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Snf Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Snf Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If
                Dim paramCode As String = ""


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If


                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    Gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
                Else
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.Red
                End If

            Next
        Else
            For i As Integer = 0 To Gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""
                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Customer code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code not found in master" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Location Code not found in master" & Environment.NewLine
                End If

                Dim LocationType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Location_Type,'')  as Location_Type from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(LocationType, "Virtual") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Location Type must be Virtual " & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Document_Date").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Document_Date Time Must not be Blank" & Environment.NewLine
                End If
                If IsDate(strCellValue) Then
                Else
                    ValidateStatus = ValidateStatus & "Document_Date Time Must  be a Date Time Value" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Tanker No. Must not be Blank" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains(" ") Then
                    ValidateStatus = ValidateStatus & "Tanker No. Must not contain any Blank Space" & Environment.NewLine
                End If
                If strCellValue.ToString.Contains("!") OrElse strCellValue.ToString.Contains("@") OrElse strCellValue.ToString.Contains("#") OrElse strCellValue.ToString.Contains("$") OrElse strCellValue.ToString.Contains("%") OrElse strCellValue.ToString.Contains("^") OrElse strCellValue.ToString.Contains("&") OrElse strCellValue.ToString.Contains("*") OrElse strCellValue.ToString.Contains("(") OrElse strCellValue.ToString.Contains(")") OrElse strCellValue.ToString.Contains("_") OrElse strCellValue.ToString.Contains("-") OrElse strCellValue.ToString.Contains("+") OrElse strCellValue.ToString.Contains("=") OrElse strCellValue.ToString.Contains("\") OrElse strCellValue.ToString.Contains("|") OrElse strCellValue.ToString.Contains("/") OrElse strCellValue.ToString.Contains("?") OrElse strCellValue.ToString.Contains(">") OrElse strCellValue.ToString.Contains("<") OrElse strCellValue.ToString.Contains(".") OrElse strCellValue.ToString.Contains("]") OrElse strCellValue.ToString.Contains("[") OrElse strCellValue.ToString.Contains("{") OrElse strCellValue.ToString.Contains("}") Then
                    ValidateStatus = ValidateStatus & "Tanker No. Must not contain any Special Symbol" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value)
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

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Dispatch Price Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Price Code Must not be Blank" & Environment.NewLine
                End If

                ''richa agarwal 12 Sep, 2016
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" & strCellValue & "' and TSPL_BulkSalePrice_MASTER.Posted='1'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Dispatch Price Code not found in master" & Environment.NewLine
                    End If
                Else
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Dispatch Price Code not found in master" & Environment.NewLine
                    End If
                End If

               

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("MILK QTY.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "MILK QTY. Must not be Zero or Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Fat %").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_Fat % Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_SNF %").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_SNF % Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Rate").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_Rate Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch_Sale_Amount").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & " Dispatch_Sale_Amount Value Must not be Negative or Zero" & Environment.NewLine
                End If

                Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))
                If AmountLimitInvoiceBulkSale > 0 Then
                    If strCellValue > AmountLimitInvoiceBulkSale Then
                        ValidateStatus = ValidateStatus & "Dispatch_Sale_Amount Value should not be greater than Bulk Sale Invoice Amount Limit." & Environment.NewLine
                    End If
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT Qty.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch FAT Qty. Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF Qty").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch SNF Qty Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Fat Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Fat Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Snf Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Snf Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If
                Dim paramCode As String = ""


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Fat Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Fat Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Rate/100Kg.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Rate/100Kg. Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch FAT% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch SNF% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch FAT Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor_Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Vendor_Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Vendor_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Vendor_Code not found in master" & Environment.NewLine
                End If

                If isVendorInvoiceNo(clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor_Code").Value), Nothing) Then
                    strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Bill No").Value)
                    If clsCommon.myLen(strCellValue) <= 0 Then
                        ValidateStatus = ValidateStatus & "Vendor Bill No Must not be blank" & Environment.NewLine
                    End If
                End If

                Dim vendorInvoiceNo As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Bill No").Value)
                Dim LocCode As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                Dim VenCode As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor_Code").Value)
                Dim isVenFlag As Boolean = False
                Dim isLovFlag As Boolean = False
                If clsCommon.myLen(vendorInvoiceNo) > 0 Then
                    For jj As Integer = 0 To Gv1.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells("Vendor Bill No").Value), vendorInvoiceNo) = CompairStringResult.Equal Then

                            If Not isVenFlag Then
                                If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells("Vendor_Code").Value), VenCode) <> CompairStringResult.Equal Then
                                    ValidateStatus = ValidateStatus & "Vendor Code Must be Same if Vendor Bill No is same for all the records " & Environment.NewLine
                                    isVenFlag = True
                                End If
                            End If
                            If Not isLovFlag Then
                                If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells("Location code").Value), LocCode) <> CompairStringResult.Equal Then
                                    ValidateStatus = ValidateStatus & "Location Code Must be Same if Vendor Bill No is same, for all the records  " & Environment.NewLine
                                    isLovFlag = True
                                End If
                            End If
                        End If
                    Next
                End If


                'strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Bill No").Value)
                'If clsCommon.myLen(strCellValue) <= 0 Then
                '    ValidateStatus = ValidateStatus & "Vendor Bill No Must not be blank" & Environment.NewLine
                'End If
                If clsCommon.myLen(strCellValue) > 30 Then
                    ValidateStatus = ValidateStatus & "Vendor Bill No Must not be greater than 30 length" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SRN Price Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Price Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Price Code not found in master" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT Qty.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN FAT Qty. Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF Qty").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN SNF Qty Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Fat Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Fat Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Snf Per Kg").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Snf Per Kg Value Must not be Negative or Zero" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Fat Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Fat Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Sale Amount").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Sale Amount Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Rate/100Kg.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Rate/100Kg. Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN FAT% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN SNF% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN FAT Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Standard Rate").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SRN Standard Rate Value Must not be Negative or Zero" & Environment.NewLine
                End If
                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    Gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
                Else
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    Gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.Red
                End If

            Next
        End If
        Gv1.BestFitColumns()
        Gv1.AutoSizeRows = True
        Gv1.Columns(colSlno).PinPosition = PinnedColumnPosition.Left
        Gv1.Columns(colIsValidated).PinPosition = PinnedColumnPosition.Left
        Gv1.Columns(colErrorStatus).PinPosition = PinnedColumnPosition.Left
        clsCommon.ProgressBarPercentHide()
    End Sub

    Sub SaveAndPost()
        arrVendorInvoiceNo = New List(Of String)
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim trans As SqlTransaction = Nothing
        Try
            If rdbAgainstBulkprocurement.IsChecked Then
                If ValidatedCount > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()
                    Dim obj
                    Dim dt As Date = Nothing
                    For i = 0 To Gv1.Rows.Count - 1
                        Dim strJobLoc = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                        If Gv1.Rows(i).Cells(colIsValidated).Value Then
                            j = j + 1

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount)
                            dt = clsCommon.GetPrintDate(Gv1.Rows(i).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy hh:mm:ss tt")
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Gate Entry Document ")
                            obj = New clsGateEntry()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                                Throw New Exception("Error in Gate Entry  No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Sublocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            obj.Doc_Type = "BulkProc"
                            obj.Date_And_Time = clsCommon.GetPrintDate(Gv1.Rows(i).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.Challan_No = clsCommon.myCstr("ND")
                            obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(obj.Item_Code, trans)
                            obj.UOM = clsCommon.myCstr(DefaultUOm)
                            obj.Qty_In_Kg = 0
                            obj.fat_per = 0
                            obj.snf_Per = 0
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.isNewEntry = True
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("Tspl_Gate_Entry_Details", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsGateEntry.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim GateEntryNo As String = obj.Gate_Entry_No
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Weighment Document ")
                            obj = New clsWeighment()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(obj.Weighment_No) <= 0 Then
                                Throw New Exception("Error in Weighment No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            obj.Tare_Weight_date = dt
                            obj.Weighment_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            obj.Doc_Type = "BulkProc"
                            obj.Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Challan_No = clsCommon.myCstr("ND")
                            obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            obj.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.Qty_In_Kg = 0
                            obj.snf_Per = 0
                            obj.fat_per = 0
                            obj.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                            obj.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                            obj.Net_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            obj.UOM = DefaultUOm
                            obj.Dip_Value = clsCommon.myCdbl(Gv1.Rows(i).Cells("DIP Value").Value)
                            obj.Weighment_Slip_No = ""
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.isNewEntry = True
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Weighment_Detail", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsWeighment.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim weighmentNo As String = obj.Weighment_No
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " QC Document ")
                            obj = New clsQualityCheck()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(obj.QC_No) <= 0 Then
                                Throw New Exception("Error in QC No genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            obj.Doc_Type = "BulkProc"
                            obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.QC_In_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                            obj.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                            obj.Challan_No = clsCommon.myCstr("ND")
                            obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.Weighment_No = clsCommon.myCstr(weighmentNo)
                            obj.Weighment_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy")
                            obj.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.Remarks = clsCommon.myCstr("")
                            obj.UOM = DefaultUOm
                            obj.Qty_In_Kg = 0
                            obj.fat_per = 0
                            obj.snf_Per = 0
                            obj.snf_KG = 0
                            obj.fat_KG = 0
                            obj.Dip_Value = clsCommon.myCdbl(Gv1.Rows(i).Cells("DIP Value").Value)
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
                            Dim objParam As New clsQcParam
                            obj.arrQcParam = New List(Of clsQcParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM00001")
                            objParam.Param_Field_Desc = clsCommon.myCstr("FAT %")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM00001(FAT %)").Value)
                            objParam.Param_Type = clsCommon.myCstr("FAT")
                            If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(Gv1.Rows(i).Cells("PM00001(FAT %)").Value))
                            End If
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("AutoFat")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Auto Fat")
                            objParam.Param_Field_Value = clsCommon.myCstr("")
                            objParam.Param_Type = clsCommon.myCstr("AutoFat")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("AutoCLR")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Auto CLR")
                            objParam.Param_Field_Value = clsCommon.myCstr("")
                            objParam.Param_Type = clsCommon.myCstr("AutoCLR")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM00004")
                            objParam.Param_Field_Desc = clsCommon.myCstr("CF")
                            objParam.Param_Field_Value = clsCommon.myCstr("0.14")
                            objParam.Param_Type = clsCommon.myCstr("CF")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("AutoSnf")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Auto SNF")
                            objParam.Param_Field_Value = clsCommon.myCstr("")
                            objParam.Param_Type = clsCommon.myCstr("AutoSnf")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM00002")
                            objParam.Param_Field_Desc = clsCommon.myCstr("SNF %")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM00002(SNF %)").Value)
                            objParam.Param_Type = clsCommon.myCstr("SNF")
                            If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(Gv1.Rows(i).Cells("PM00002(SNF %)").Value))
                            End If
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM00003")
                            objParam.Param_Field_Desc = clsCommon.myCstr("CLR")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM00003(CLR)").Value)
                            objParam.Param_Type = clsCommon.myCstr("CLR")
                            If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(Gv1.Rows(i).Cells("PM00003(CLR)").Value))
                            End If
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-151")
                            objParam.Param_Field_Desc = clsCommon.myCstr("RM VALUE")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-151(RM VALUE)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1510")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Acidity (B.B)")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1510(Acidity (B.B))").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1511")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Temprature ° C")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1511(Temprature ° C)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1512")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Alcohol")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1512(Alcohol)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1513")
                            objParam.Param_Field_Desc = clsCommon.myCstr("ACI 8.5% SNF")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1513(ACI 8.5% SNF)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1515")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Taste")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1515(Taste)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1516")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Chenna %")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1516(Chenna %)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1517")
                            objParam.Param_Field_Desc = clsCommon.myCstr("B.R. Reading")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1517(B.R. Reading)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1518")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Detergent")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1518(Detergent)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1519")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Acidity (A.B)")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1519(Acidity (A.B))").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-152")
                            objParam.Param_Field_Desc = clsCommon.myCstr("FFA%")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-152(FFA%)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1520")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Adultration")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1520(Adultration)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-1521")
                            objParam.Param_Field_Desc = clsCommon.myCstr("Flavour")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-1521(Flavour)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-153")
                            objParam.Param_Field_Desc = clsCommon.myCstr("PROTEIN")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-153(PROTEIN)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-154")
                            objParam.Param_Field_Desc = clsCommon.myCstr("NA+ PPM")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-154(NA+ PPM)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-155")
                            objParam.Param_Field_Desc = clsCommon.myCstr("K PPM")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-155(K PPM)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-156")
                            objParam.Param_Field_Desc = clsCommon.myCstr("MILK ASH %")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-156(MILK ASH %)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)

                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-157")
                            objParam.Param_Field_Desc = clsCommon.myCstr("SUGAR")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-157(SUGAR)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-158")
                            objParam.Param_Field_Desc = clsCommon.myCstr("MALTOSE")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-158(MALTOSE)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)


                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr("PM14-159")
                            objParam.Param_Field_Desc = clsCommon.myCstr("GLUCOSE")
                            objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells("PM14-159(GLUCOSE)").Value)
                            objParam.Param_Type = clsCommon.myCstr("OTHERS")
                            obj.arrQcParam.Add(objParam)
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_QUALITY_CHECK", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsQualityCheck.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim qc_no As String = obj.QC_No
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Unloading Document ")
                            obj = New clsUnloading()
                            obj.isNewEntry = True
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                                Throw New Exception("Error In Unloading  No Genertion")
                                Exit Sub
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                Dim strVirtualLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value) & "' and is_sub_location='Y' and Location_Type='Virtual' and UseInJobWork=1  ", trans))
                                obj.Sub_location_Code = strVirtualLoc
                            Else
                                obj.Sub_location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Silo No").Value)
                            End If
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.Weighment_No = weighmentNo
                            obj.QC_No = qc_no
                            obj.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)

                            obj.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.UOM = DefaultUOm
                            obj.Qty = 0
                            obj.fat_per = 0
                            obj.snf_Per = 0
                            obj.SNF_KG = 0
                            obj.fat_KG = 0
                            obj.isPosted = 1
                            obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_MILK_UNLOADING", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsUnloading.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Gate Out Document ")
                            obj = New clsGateOut()
                            obj.isNewEntry = True
                            ''richa agarwal 04/jan/2017
                            ' obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut,"", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            ''-----------
                            If clsCommon.myLen(obj.Doc_No) <= 0 Then
                                Throw New Exception("Error In GateOut  No Genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.Weighment_No = weighmentNo
                            obj.QC_No = qc_no
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Gate_Out", "Created_By", "", "", trans)
                            clsGateOut.saveData(obj, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Bulk Milk SRN Document ")
                            obj = New clsBulkMilkSRN()
                            obj.isNewEntry = True
                            'obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN,"", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            If clsCommon.myLen(obj.SRN_NO) <= 0 Then
                                Throw New Exception("Error In SRN  No Genertion")
                            End If
                            obj.PO_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPO, "", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            If clsCommon.myLen(obj.PO_NO) <= 0 Then
                                Throw New Exception("Error In Auto PO  No Genertion")
                            End If
                            obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            obj.PO_Date = dt
                            obj.isApproved = 1
                            obj.SRN_Date = dt
                            obj.Gate_Entry_No = GateEntryNo
                            obj.Weighment_No = weighmentNo
                            obj.Weighment_Date = dt
                            obj.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            obj.Loc_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            obj.Challan_No = clsCommon.myCstr("ND")
                            obj.Challan_Date = dt
                            obj.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            obj.Price_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("SRN Price Chart Code").Value)
                            obj.QC_No = qc_no
                            obj.Qc_Date = dt
                            obj.isPosted = 0
                            obj.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                            obj.UOM = DefaultUOm
                            obj.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                            obj.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                            obj.Net_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            obj.snf_Per = clsCommon.myCdbl(Gv1.Rows(i).Cells("PM00002(SNF %)").Value)
                            obj.fat_per = clsCommon.myCdbl(Gv1.Rows(i).Cells("PM00001(FAT %)").Value)
                            obj.fat_KG = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                            obj.SNF_KG = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                            obj.FatAmt = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                            obj.SnfAmt = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                            obj.fat_Rate = clsCommon.myFormat(obj.FatAmt / obj.fat_KG)
                            obj.SNF_Rate = clsCommon.myFormat(obj.SnfAmt / obj.SNF_KG)
                            obj.Amount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Amount").Value)
                            obj.SpecialDeduction = 0
                            obj.Deduction = 0
                            obj.Incentive = 0
                            obj.Actual_Amount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Amount").Value)
                            obj.BasicRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Rate").Value)
                            obj.Standardrate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Rate").Value)
                            obj.NetRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Rate").Value)
                            obj.FinalMilkRate = obj.Amount / obj.Net_Weight
                            obj.Modify_By = objCommonVar.CurrentUserCode
                            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            obj.comp_code = objCommonVar.CurrentCompanyCode
                            obj.Created_By = objCommonVar.CurrentUserCode
                            obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Bulk_MILK_SRN", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Loc_Code", trans)
                            clsBulkMilkSRN.saveData(obj, trans)
                            clsBulkMilkSRN.postData(obj.SRN_NO, clsUserMgtCode.frmBulkMilkSRN, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            Dim SrnNo As String = obj.SRN_NO

                            Gv1.Rows(i).Cells(colDispatchCode).Value = SrnNo
                        End If
                    Next
                    ''richa agarwal 10 Jan,2018
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSkipfunctionalityafterSRNOnBulkProcurement, clsFixedParameterCode.AllowtoSkipfunctionalityafterSRNOnBulkProcurement, trans)), "0") = CompairStringResult.Equal Then
                        j = 0
                        arrVendorInvoiceNo = New List(Of String)
                        For jjj As Integer = 0 To Gv1.Rows.Count - 1
                            If Gv1.Rows(jjj).Cells(colIsValidated).Value Then
                                If Not arrVendorInvoiceNo.Contains((Gv1.Rows(jjj).Cells("Vendor Invoice No").Value) + (Gv1.Rows(jjj).Cells("IsJobWork").Value)) Then
                                    arrVendorInvoiceNo.Add(clsCommon.myCstr(Gv1.Rows(jjj).Cells("Vendor Invoice No").Value + clsCommon.myCstr(Gv1.Rows(jjj).Cells("IsJobWork").Value)))
                                End If
                            End If
                        Next

                        Dim cnt As Integer = 0
                        Dim arrProcessedInvoice As List(Of String) = Nothing
                        Dim ProcessedRow As New List(Of String)
                        arrProcessedInvoice = New List(Of String)
                        'For cnt = 0 To arrVendorInvoiceNo.Count - 1
                        Dim CurrentVendorInvoiceNO As String = ""
                        For k As Integer = 0 To Gv1.Rows.Count - 1
                            If Gv1.Rows(k).Cells(colIsValidated).Value Then
                                If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)) <= 0 Then
                                    If (Not ProcessedRow.Contains(k)) Then
                                        arrProcessedInvoice.Add(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value))
                                        CurrentVendorInvoiceNO = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        j += 1
                                        clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                                        obj = New clsMilkPurchaseInvoiceHead
                                        obj.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        obj = New clsMilkPurchaseInvoiceHead
                                        If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(k).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.BulkProcJobWorkOutward, clsCommon.myCstr(Gv1.Rows(k).Cells("JobWork Location").Value))
                                        Else
                                            If isVendorInvoiceNo(clsCommon.myCstr(Gv1.Rows(k).Cells("Party Code / Vendor Code").Value), trans) Then
                                                obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                            Else
                                                obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                            End If
                                        End If
                                        obj.isNewEntry = True

                                        If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                                            Throw New Exception("Error In Document No Genertion")
                                        End If
                                        obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(k).Cells("IsJobWork").Value)
                                        obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("JobWork Location").Value)
                                        dt = clsCommon.GetPrintDate(Gv1.Rows(k).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy")
                                        obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                                        obj.vendor_code = clsCommon.myCstr(Gv1.Rows(k).Cells("Party Code / Vendor Code").Value)
                                        obj.Loc_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value)
                                        obj.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        obj.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                        obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                        obj.Total_FAT_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("FAT Qty.").Value)
                                        obj.Total_SNF_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SNF Qty").Value)
                                        obj.Total_QTY = clsCommon.myCstr(Gv1.Rows(k).Cells("Net_MILK QTY.").Value)
                                        obj.Total_AMT = clsCommon.myCstr(Gv1.Rows(k).Cells("Purchase_Amount").Value)
                                        obj.RoundOffAmount = 0
                                        obj.isSRNTradeInvoice = 0
                                        obj.isPosted = 0
                                        obj.Modified_By = objCommonVar.CurrentUserCode
                                        obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                                        obj.Created_By = objCommonVar.CurrentUserCode
                                        obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                                        Dim TotalFatKg As Double = 0
                                        Dim TotalSNFKg As Double = 0
                                        Dim TotalQty As Double = 0
                                        Dim TotalAmt As Double = 0
                                        Dim objDetail As New clsMilkPurchaseInvoiceDetail
                                        obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                                        objDetail = New clsMilkPurchaseInvoiceDetail
                                        objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                                        objDetail.SL_NO = clsCommon.myCstr(1)
                                        objDetail.SRN_NO = Gv1.Rows(k).Cells(colDispatchCode).Value
                                        objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                        objDetail.Item_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("Item code").Value)
                                        objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                                        Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(Gv1.Rows(k).Cells("Item code").Value), trans)
                                        objDetail.UOM = DefaultUOm
                                        objDetail.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(k).Cells("GROSS WEIGHT").Value)
                                        objDetail.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(k).Cells("TARE WEIGHT").Value)
                                        objDetail.Net_Weight = clsCommon.myCdbl(Gv1.Rows(k).Cells("Net_MILK QTY.").Value)
                                        objDetail.Invoice_Qty = clsCommon.myCdbl(Gv1.Rows(k).Cells("Net_MILK QTY.").Value)
                                        TotalQty += objDetail.Invoice_Qty
                                        objDetail.fat_per = clsCommon.myCdbl(Gv1.Rows(k).Cells("PM00001(FAT %)").Value)
                                        objDetail.fat_KG = clsCommon.myCdbl(Gv1.Rows(k).Cells("FAT Qty.").Value)
                                        TotalFatKg += objDetail.fat_KG
                                        objDetail.fat_Rate = clsCommon.myCdbl(Gv1.Rows(k).Cells("Fat Value").Value) / objDetail.fat_KG
                                        objDetail.snf_Per = clsCommon.myCdbl(Gv1.Rows(k).Cells("PM00002(SNF %)").Value)
                                        objDetail.SNF_KG = clsCommon.myCdbl(Gv1.Rows(k).Cells("SNF Qty").Value)
                                        TotalSNFKg += objDetail.SNF_KG
                                        objDetail.SNF_Rate = clsCommon.myCdbl(Gv1.Rows(k).Cells("SNF Value").Value) / objDetail.SNF_KG
                                        objDetail.Amount = clsCommon.myCdbl(Gv1.Rows(k).Cells("Purchase_Amount").Value)
                                        TotalAmt += objDetail.Amount
                                        objDetail.Deduction = 0
                                        objDetail.Incentive = 0
                                        objDetail.Special_Deduction = 0
                                        objDetail.Actual_Amount = clsCommon.myCdbl(Gv1.Rows(k).Cells("Purchase_Amount").Value)
                                        objDetail.price_code = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN Price Chart Code").Value)
                                        objDetail.NetRate = clsCommon.myCdbl(Gv1.Rows(k).Cells("Purchase_Rate").Value)
                                        obj.arrDetail.Add(objDetail)
                                        obj.Total_FAT_KG = TotalFatKg
                                        obj.Total_SNF_KG = TotalSNFKg
                                        obj.Total_QTY = TotalQty
                                        obj.Total_AMT = TotalAmt
                                        objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), "Loc_Code", trans)
                                        clsMilkPurchaseInvoiceHead.saveData(obj, trans)
                                        clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                                        ProcessedRow.Add(k)
                                    End If
                                Else
                                    If (Not ProcessedRow.Contains(k)) Then
                                        dt = clsCommon.GetPrintDate(Gv1.Rows(k).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy")
                                        arrProcessedInvoice.Add(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value))
                                        CurrentVendorInvoiceNO = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        j += 1
                                        clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                                        obj = New clsMilkPurchaseInvoiceHead
                                        obj.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        obj = New clsMilkPurchaseInvoiceHead
                                        obj.isNewEntry = True
                                        If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(k).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.BulkProcJobWorkOutward, clsCommon.myCstr(Gv1.Rows(k).Cells("JobWork Location").Value))
                                        Else
                                            If isVendorInvoiceNo(clsCommon.myCstr(Gv1.Rows(k).Cells("Party Code / Vendor Code").Value), trans) Then
                                                obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                            Else
                                                obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                            End If
                                        End If
                                        If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                                            Throw New Exception("Error In Document No Genertion")
                                        End If
                                        obj.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(k).Cells("IsJobWork").Value)
                                        obj.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("JobWork Location").Value)
                                        obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                                        obj.vendor_code = clsCommon.myCstr(Gv1.Rows(k).Cells("Party Code / Vendor Code").Value)
                                        obj.Loc_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value)
                                        obj.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Invoice No").Value)
                                        obj.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                        obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                        obj.Total_FAT_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("FAT Qty.").Value)
                                        obj.Total_SNF_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SNF Qty").Value)
                                        obj.Total_QTY = clsCommon.myCstr(Gv1.Rows(k).Cells("Net_MILK QTY.").Value)
                                        obj.Total_AMT = clsCommon.myCstr(Gv1.Rows(k).Cells("Purchase_Amount").Value)
                                        obj.RoundOffAmount = 0
                                        obj.isSRNTradeInvoice = 0
                                        obj.isPosted = 0
                                        obj.Modified_By = objCommonVar.CurrentUserCode
                                        obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                                        obj.Created_By = objCommonVar.CurrentUserCode
                                        obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                                        Dim TotalFatKg As Double = 0
                                        Dim TotalSNFKg As Double = 0
                                        Dim TotalQty As Double = 0
                                        Dim TotalAmt As Double = 0
                                        Dim objDetail As New clsMilkPurchaseInvoiceDetail
                                        obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                                        For jj As Integer = 0 To Gv1.Rows.Count - 1
                                            If Gv1.Rows(jj).Cells(colIsValidated).Value AndAlso clsCommon.CompairString(CurrentVendorInvoiceNO, Gv1.Rows(jj).Cells("Vendor Invoice No").Value) = CompairStringResult.Equal AndAlso ((Not ProcessedRow.Contains(jj))) Then
                                                ProcessedRow.Add(jj)
                                                dt = clsCommon.GetPrintDate(Gv1.Rows(jj).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy")
                                                If dt > obj.doc_date Then
                                                    obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                                    obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                                End If
                                                objDetail = New clsMilkPurchaseInvoiceDetail
                                                objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                                                objDetail.SL_NO = clsCommon.myCstr(1)
                                                objDetail.SRN_NO = Gv1.Rows(jj).Cells(colDispatchCode).Value
                                                objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                                objDetail.Item_Code = clsCommon.myCstr(Gv1.Rows(jj).Cells("Item code").Value)
                                                objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                                                Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(Gv1.Rows(jj).Cells("Item code").Value), trans)
                                                objDetail.UOM = DefaultUOm
                                                objDetail.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(jj).Cells("GROSS WEIGHT").Value)
                                                objDetail.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(jj).Cells("TARE WEIGHT").Value)
                                                objDetail.Net_Weight = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Net_MILK QTY.").Value)
                                                objDetail.Invoice_Qty = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Net_MILK QTY.").Value)
                                                TotalQty += objDetail.Invoice_Qty
                                                objDetail.fat_per = clsCommon.myCdbl(Gv1.Rows(jj).Cells("PM00001(FAT %)").Value)
                                                objDetail.fat_KG = clsCommon.myCdbl(Gv1.Rows(jj).Cells("FAT Qty.").Value)
                                                TotalFatKg += objDetail.fat_KG
                                                objDetail.fat_Rate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Fat Value").Value) / objDetail.fat_KG
                                                objDetail.snf_Per = clsCommon.myCdbl(Gv1.Rows(jj).Cells("PM00002(SNF %)").Value)
                                                objDetail.SNF_KG = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SNF Qty").Value)
                                                TotalSNFKg += objDetail.SNF_KG
                                                objDetail.SNF_Rate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SNF Value").Value) / objDetail.SNF_KG
                                                objDetail.Amount = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Purchase_Amount").Value)
                                                TotalAmt += objDetail.Amount
                                                objDetail.Deduction = 0
                                                objDetail.Incentive = 0
                                                objDetail.Special_Deduction = 0
                                                objDetail.Actual_Amount = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Purchase_Amount").Value)
                                                objDetail.price_code = clsCommon.myCstr(Gv1.Rows(jj).Cells("SRN Price Chart Code").Value)
                                                objDetail.NetRate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Purchase_Rate").Value)
                                                obj.arrDetail.Add(objDetail)
                                            End If
                                        Next
                                        obj.Total_FAT_KG = TotalFatKg
                                        obj.Total_SNF_KG = TotalSNFKg
                                        obj.Total_QTY = TotalQty
                                        obj.Total_AMT = TotalAmt
                                        objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), "Loc_Code", trans)
                                        clsMilkPurchaseInvoiceHead.saveData(obj, trans)
                                        clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                                    End If
                                End If





                            End If
                        Next

                    End If

                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Saved Successfully")
                Else
                    Throw New Exception("No Validated Rows found to save")
                End If
            ElseIf rdbAgainstBulkSale.IsChecked Then
                If ValidatedCount > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()

                    Dim objGateEntry As New clsGateEntrySale()
                    Dim objWeighmentEntry As New ClsWeighmentEntry()
                    Dim objLoadingEntry As New ClsLoadingTanker()
                    Dim objQCEntry As New ClsQualityCheckBulkSale()
                    Dim objDispatchMasterEntry As New ClsDispatchBulkSale()
                    Dim objDispatchDetailEntry As New clsDispatchDetailBulkSale()
                    Dim objGateOutEntry As New ClsTankerOut()
                    Dim strCustomerCode As String = String.Empty
                    Dim strLocationCode As String = String.Empty
                    Dim strGate_entry_Date As Date? = Nothing
                    Dim strTankerNo As String = String.Empty
                    Dim strItemCode As String = String.Empty
                    Dim strSilo As String = String.Empty
                    Dim UOM As String = String.Empty
                    Dim DblGrossWeight As Double = 0
                    Dim DblTareWeight As Double = 0
                    Dim DblNetWeight As Double = 0
                    Dim DblSnfPer As Double = 0
                    Dim DblFatPer As Double = 0
                    Dim dblSaleAmount As Double = 0
                    Dim DblClr As Double = 0
                    Dim strPriceCode As String = String.Empty
                    Dim dblFatKg As Double = 0
                    Dim DblSNFKG As Double = 0
                    Dim DblStandardrate As Double = 0
                    Dim DblFatRate As Double = 0
                    Dim DblSNFRate As Double = 0
                    Dim DblFatAmount As Double = 0
                    Dim DblSNFAmount As Double = 0
                    Dim DblNetMilkRate As Double = 0
                    Dim DblFATweightage As Double = 0
                    Dim DblSNFWeightage As Double = 0
                    Dim DblFATRatio As Double = 0
                    Dim DblSNFRatio As Double = 0
                    'If Gv1.Columns("").Then Then

                    'End If
                    'Gv1.Columns.Add("DispatchCode")
                    'Gv1.Columns("DispatchCode").IsVisible = True
                    Gv1.AllowMultiColumnSorting = True
                    '   Gv1.Columns("Sale Price Code").AllowSort = True

                    For i = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colIsValidated).Value Then
                            j = j + 1

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount)


                            strCustomerCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Customer code").Value)
                            strLocationCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value)
                            strGate_entry_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells("Gate_entry_Date").Value, "dd/MMM/yyyy")
                            strTankerNo = clsCommon.myCstr(Gv1.Rows(i).Cells("Sale_TankerNo.").Value)
                            strItemCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value)
                            strSilo = clsCommon.myCstr(Gv1.Rows(i).Cells("SILO No").Value)

                            DblGrossWeight = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                            DblTareWeight = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                            DblNetWeight = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_MILK QTY.").Value)
                            DblSnfPer = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_SNF %").Value)
                            DblFatPer = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Fat %").Value)
                            dblSaleAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Amount").Value)

                            strPriceCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Sale Price Code").Value)
                            UOM = clsCommon.myCstr(Gv1.Rows(i).Cells("UOM").Value)
                            dblFatKg = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                            DblSNFKG = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                            DblStandardrate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Rate").Value)
                            DblFatRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Rate").Value)
                            DblSNFRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Rate").Value)
                            DblFatAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                            DblSNFAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)


                            objGateEntry = New clsGateEntrySale()
                            objWeighmentEntry = New ClsWeighmentEntry()
                            objLoadingEntry = New ClsLoadingTanker()
                            objQCEntry = New ClsQualityCheckBulkSale()
                            objDispatchMasterEntry = New ClsDispatchBulkSale()
                            objDispatchDetailEntry = New clsDispatchDetailBulkSale()
                            objGateOutEntry = New ClsTankerOut()



                            '' insert data into gate entry objects
                            objGateEntry.Document_Date = strGate_entry_Date
                            objGateEntry.Tanker_No = strTankerNo
                            objGateEntry.Location_Code = strLocationCode
                            objGateEntry.Customer_Code = strCustomerCode
                            objGateEntry.IsSaleReturn = "N"

                            ''---save and post data of gate entry class
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_GATEENTRY_SALE", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            clsGateEntrySale.SaveData(objGateEntry, True, trans)
                            clsGateEntrySale.PostData("", "", objGateEntry.Document_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_GATEENTRY_SALE set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where Document_No='" & objGateEntry.Document_No & "' ", trans)
                            ''-----------------------------------------


                            '' insert data into Weighment entry objects
                            objWeighmentEntry.Weighment_Date = strGate_entry_Date
                            objWeighmentEntry.GateEntry_Document_No = objGateEntry.Document_No
                            objWeighmentEntry.Tanker_No = strTankerNo
                            objWeighmentEntry.Location_Code = strLocationCode
                            objWeighmentEntry.Gross_Weight = DblGrossWeight
                            objWeighmentEntry.Tare_Weight = DblTareWeight
                            objWeighmentEntry.Net_Weight = DblNetWeight

                            '' save and post data of weighment entry class
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_WEIGHMENT_DETAIL_BULKSALE", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsWeighmentEntry.SaveData(objWeighmentEntry, True, trans)
                            ClsWeighmentEntry.PostData("", "", objWeighmentEntry.Weighment_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_WEIGHMENT_DETAIL_BULKSALE set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where Weighment_No='" & objWeighmentEntry.Weighment_No & "' ", trans)

                            '' insert data into Loading Tanker entry objects
                            objLoadingEntry.LoadingTanker_Date = strGate_entry_Date
                            objLoadingEntry.Weighment_No = objWeighmentEntry.Weighment_No
                            objLoadingEntry.Tanker_No = strTankerNo
                            objLoadingEntry.Location_Code = strLocationCode
                            objLoadingEntry.Item_Code = strItemCode
                            objLoadingEntry.Silo_No = strSilo
                            objLoadingEntry.Quantity = 0

                            '' save and post data of Loading Tanker entry class
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_LOADING_TANKER_DETAIL_BULKSALE", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsLoadingTanker.SaveData(objLoadingEntry, True, trans)
                            ClsLoadingTanker.PostData("", "", objLoadingEntry.LoadingTanker_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_LOADING_TANKER_DETAIL_BULKSALE set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where LoadingTanker_No='" & objLoadingEntry.LoadingTanker_No & "' ", trans)

                            '' insert data into Quality Check entry objects
                            objQCEntry.QC_Date = strGate_entry_Date
                            objQCEntry.Weighment_No = objWeighmentEntry.Weighment_No
                            objQCEntry.LoadingTanker_No = objLoadingEntry.LoadingTanker_No
                            objQCEntry.GateEntry_Document_No = objGateEntry.Document_No
                            objQCEntry.Tanker_No = strTankerNo
                            objQCEntry.Location_Code = strLocationCode
                            objQCEntry.Silo_No = strSilo
                            objQCEntry.Correction_Factor = 0.14
                            objQCEntry.Item_Code = strItemCode
                            objQCEntry.Unit_code = UOM
                            objQCEntry.Qty = 0
                            objQCEntry.Fat = DblFatPer
                            objQCEntry.CLR = DblClr
                            objQCEntry.SNF = DblSnfPer
                            objQCEntry.Remarks = ""
                            objQCEntry.Customer_Code = strCustomerCode


                            Dim objQCParam As New clsQcParamBulkSale()
                            objQCEntry.arrQcParamDetail = New List(Of clsQcParamBulkSale)
                            ' objQCParam.QC_No = clsCommon.myCstr(objQCEntry.QC_No)
                            objQCParam.Param_Field_Code = "colFAT"
                            objQCParam.Param_Field_Desc = "FAT"
                            objQCParam.Param_Field_Value = DblFatPer
                            objQCParam.Fat = DblFatPer
                            objQCParam.Param_Type = "FAT"
                            objQCParam.Item_code = strItemCode
                            objQCParam.Unit_code = UOM
                            objQCEntry.arrQcParamDetail.Add(objQCParam)

                            objQCParam = New clsQcParamBulkSale()
                            objQCParam.Item_code = strItemCode
                            objQCParam.Unit_code = UOM
                            objQCParam.SNF = DblSnfPer
                            objQCParam.Param_Field_Code = "colSNF"
                            objQCParam.Param_Field_Desc = "SNF"
                            objQCParam.Param_Field_Value = DblSnfPer
                            objQCParam.Param_Type = "SNF"

                            objQCEntry.arrQcParamDetail.Add(objQCParam)

                            '' save and post data of Quality Check entry class
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Quality_Check_BulkSale", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsQualityCheckBulkSale.SaveData(objQCEntry, True, trans)
                            ClsQualityCheckBulkSale.PostData("", "", objQCEntry.QC_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Quality_Check_BulkSale set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where QC_No='" & objQCEntry.QC_No & "' ", trans)

                            '' insert data into Dispatch Master entry objects
                            objDispatchMasterEntry.Document_Date = strGate_entry_Date
                            objDispatchMasterEntry.Customer_Code = strCustomerCode
                            objDispatchMasterEntry.QC_Code = objQCEntry.QC_No
                            objDispatchMasterEntry.Tanker_Code = strTankerNo
                            objDispatchMasterEntry.Location_Code = strLocationCode
                            objDispatchMasterEntry.Dip_marking = ""
                            objDispatchMasterEntry.Challan_No = ""
                            objDispatchMasterEntry.Gross_Weight = DblGrossWeight
                            objDispatchMasterEntry.Tare_Weight = DblTareWeight
                            objDispatchMasterEntry.Net_Weight = DblNetWeight
                            objDispatchMasterEntry.Price_Code = strPriceCode
                            objDispatchMasterEntry.Total_Amt = dblSaleAmount
                            objDispatchMasterEntry.ApprovalRequired = "N"
                            objDispatchMasterEntry.Approved = "N"
                            objDispatchMasterEntry.Status = "Open"
                            '   objDispatchMasterEntry.Is_Create_Auto_Invoice = 1
                            objDispatchMasterEntry.Is_Create_Auto_Invoice = 0
                            objDispatchMasterEntry.ReverseFlag = "N"

                            ''insert data into dispatch Detail bulk sale
                            objDispatchMasterEntry.arrDispatchDetailBulkSale = New List(Of clsDispatchDetailBulkSale)

                            objDispatchDetailEntry.Item_Code = strItemCode
                            objDispatchDetailEntry.Unit_code = uom
                            objDispatchDetailEntry.Qty = DblNetWeight

                            objDispatchDetailEntry.FatPer = DblFatPer
                            objDispatchDetailEntry.SNFPer = DblSnfPer
                            objDispatchDetailEntry.CLR = DblClr
                            objDispatchDetailEntry.Fat_KG = dblFatKg
                            objDispatchDetailEntry.SNF_KG = DblSNFKG
                            objDispatchDetailEntry.FatAmount = DblFatAmount

                            objDispatchDetailEntry.SNFAmount = DblSNFAmount
                            objDispatchDetailEntry.NetMilkRate = DblNetMilkRate
                            objDispatchDetailEntry.Amount = dblSaleAmount
                            objDispatchDetailEntry.FatRate = DblFatRate
                            objDispatchDetailEntry.SNFRate = DblSNFRate
                            objDispatchDetailEntry.StandardRate = DblStandardrate

                            objDispatchMasterEntry.arrDispatchDetailBulkSale.Add(objDispatchDetailEntry)

                            Dim balqty As Double = 0
                            ''richa agarwal 31/03/2016 regarding to check stocking unit before doing transactions
                            If clsCommon.myLen(strSilo) > 0 Then
                                balqty = ClsLoadingTanker.getBalance(strItemCode, strLocationCode, strSilo, "", strGate_entry_Date, trans, "KG")
                            Else
                                balqty = ClsLoadingTanker.getBalance(strItemCode, strLocationCode, strSilo, "", strGate_entry_Date, trans, "KG")

                            End If
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, trans), "1") = CompairStringResult.Equal Then
                                If balqty > 0 Then
                                    balqty = GetTolerane(balqty, DblNetWeight, trans)
                                End If
                            End If

                            If balqty < DblNetWeight Then
                                Throw New Exception("You cannot save this entry because stock quantity is less than dispatch quantity")
                            End If
                            ''---------------------------------------------------


                            '' save and post data of Dispatch entry class and also create auto invoice
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Dispatch_BulkSale", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsDispatchBulkSale.SaveData(objDispatchMasterEntry, True, trans)
                            ClsDispatchBulkSale.PostData("", "", objDispatchMasterEntry.Document_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Dispatch_BulkSale set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where Document_No='" & objDispatchMasterEntry.Document_No & "' ", trans)

                            Gv1.Rows(i).Cells(colDispatchCode).Value = objDispatchMasterEntry.Document_No


                            ''insert data into gate out entry objects
                            objGateOutEntry.Document_Date = strGate_entry_Date
                            objGateOutEntry.GateEntryNo = objGateEntry.Document_No
                            objGateOutEntry.Tanker_No = strTankerNo
                            objGateOutEntry.Location_Code = strLocationCode
                            objGateOutEntry.Customer_Code = strCustomerCode
                            objGateOutEntry.IsGateOut = 1

                            '' save and post data of gate out entry
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_GATEOUT_SALE", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsTankerOut.SaveData(objGateOutEntry, True, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_GATEOUT_SALE set Created_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strGate_entry_Date, "dd/MMM/yyyy") & "' where Document_No='" & objGateOutEntry.Document_No & "' ", trans)
                        End If
                    Next

                    ''to create invoice against multiple dispatched or single acc. to sale price code,customer code, location Code
                    'CreateAutoInvoiceAgainstMultipleDispatch(trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    ' clsDBFuncationality.ExecuteNonQuery("update TSPL_INVOICE_MASTER_BULKSALE set Created_Date=document_date ,Modified_Date=document_date,Posting_Date=document_date where IsUploader=1 ", trans)
                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    'trans.Rollback()
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    Throw New Exception("No Validated Rows found to save")
                End If
            Else
                If ValidatedCount > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()

                    Dim objDispatchMasterEntryTrade As New ClsDispatchBulkSaleTrade()
                    Dim objDispatchDetailEntryTrade As New clsDispatchDetailTradeBulkSale()
                    Dim objBulkMilkSRNTrade As New clsBulkMilkSRNTrade()
                    Dim objMilkPurchaseInvoiceHeadTrade As New clsMilkPurchaseInvoiceHead()
                    Dim objMilkPurchaseInvoiceDetailTrade As New clsMilkPurchaseInvoiceDetail()


                    Dim strCustomerCode As String = String.Empty
                    Dim strLocationCode As String = String.Empty
                    Dim strDocument_Date As Date? = Nothing
                    Dim strTankerNo As String = String.Empty
                    Dim strItemCode As String = String.Empty
                    Dim strDispatchPriceCode As String = String.Empty
                    Dim DblMilkQty As Double = 0
                    Dim DblSnfPer As Double = 0
                    Dim DblFatPer As Double = 0
                    Dim DblStandardrate As Double = 0
                    Dim dblDispatchSaleAmount As Double = 0
                    Dim dblDispatchFatKg As Double = 0
                    Dim DblDispatchSNFKG As Double = 0
                    Dim DblDispatchFatRate As Double = 0
                    Dim DblDispatchSNFRate As Double = 0
                    Dim DblDispatchFatAmount As Double = 0
                    Dim DblDispatchNetMilkRate As Double = 0
                    Dim DblDispatchSNFAmount As Double = 0
                    Dim DblDispatchFATweightage As Double = 0
                    Dim DblDispatchSNFWeightage As Double = 0
                    Dim DblDispatchFATRatio As Double = 0
                    Dim DblDispatchSNFRatio As Double = 0
                    Dim strVendorCode As String = String.Empty
                    Dim strVendorInvoiceNo As String = String.Empty
                    Dim strSRNPriceCode As String = String.Empty
                    Dim dblSRNFatKg As Double = 0
                    Dim DblSRNSNFKG As Double = 0
                    Dim DblSRNFatRate As Double = 0
                    Dim DblSRNSNFRate As Double = 0
                    Dim DblSRNFatAmount As Double = 0
                    Dim DblSRNSNFAmount As Double = 0
                    Dim DblSRNNetMilkRate As Double = 0
                    Dim DblSRNFATweightage As Double = 0
                    Dim DblSRNSNFWeightage As Double = 0
                    Dim dblSRNSaleAmount As Double = 0
                    Dim DblSRNFATRatio As Double = 0
                    Dim DblSRNSNFRatio As Double = 0
                    Dim dblSRNStandardRate As Double = 0
                    For i = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colIsValidated).Value Then
                            j = j + 1

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount)



                            strCustomerCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Customer code").Value)
                            strLocationCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value)
                            strDocument_Date = clsCommon.myCDate(Gv1.Rows(i).Cells("Document_Date").Value)
                            strTankerNo = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            strItemCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value)
                            strDispatchPriceCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Dispatch Price Code").Value)

                            DblMilkQty = clsCommon.myCdbl(Gv1.Rows(i).Cells("MILK QTY.").Value)
                            DblSnfPer = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_SNF %").Value)
                            DblFatPer = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Fat %").Value)
                            DblStandardrate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Rate").Value)
                            dblDispatchSaleAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch_Sale_Amount").Value)

                            dblDispatchFatKg = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT Qty.").Value)
                            DblDispatchSNFKG = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF Qty").Value)
                            DblDispatchFatRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Fat Per Kg").Value)
                            DblDispatchSNFRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Snf Per Kg").Value)

                            DblDispatchFatAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Fat Value").Value)
                            DblDispatchSNFAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF Value").Value)
                            DblDispatchNetMilkRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Rate/100Kg.").Value)

                            DblDispatchFATweightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT%").Value)
                            DblDispatchSNFWeightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF%").Value)
                            DblDispatchFATRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch FAT").Value)
                            DblDispatchSNFRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch SNF").Value)

                            strVendorCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor_Code").Value)
                            strVendorInvoiceNo = clsCommon.myCstr(Gv1.Rows(i).Cells("Vendor Bill No").Value)
                            strSRNPriceCode = clsCommon.myCstr(Gv1.Rows(i).Cells("SRN Price Code").Value)

                            dblSRNFatKg = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT Qty.").Value)
                            DblSRNSNFKG = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF Qty").Value)
                            DblSRNFatRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Fat Per Kg").Value)
                            DblSRNSNFRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Snf Per Kg").Value)
                            DblSRNFatAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Fat Value").Value)

                            DblSRNSNFAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF Value").Value)
                            DblSRNNetMilkRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Rate/100Kg.").Value)
                            DblSRNFATweightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT%").Value)
                            DblSRNSNFWeightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF%").Value)
                            dblSRNSaleAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Sale Amount").Value)

                            DblSRNFATRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN FAT").Value)
                            DblSRNSNFRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN SNF").Value)
                            dblSRNStandardRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("SRN Standard Rate").Value)

                            objDispatchMasterEntryTrade = New ClsDispatchBulkSaleTrade()
                            objDispatchDetailEntryTrade = New clsDispatchDetailTradeBulkSale()
                            objBulkMilkSRNTrade = New clsBulkMilkSRNTrade()
                            objMilkPurchaseInvoiceHeadTrade = New clsMilkPurchaseInvoiceHead()
                            objMilkPurchaseInvoiceDetailTrade = New clsMilkPurchaseInvoiceDetail()

                            '' insert data into Dispatch Master Trade entry objects
                            objDispatchMasterEntryTrade.Document_Date = strDocument_Date
                            objDispatchMasterEntryTrade.Location_Code = strLocationCode
                            objDispatchMasterEntryTrade.Customer_Code = strCustomerCode
                            objDispatchMasterEntryTrade.Price_Code = strDispatchPriceCode
                            objDispatchMasterEntryTrade.Total_Amt = dblDispatchSaleAmount
                            objDispatchMasterEntryTrade.Tanker_No = strTankerNo
                            'objDispatchMasterEntryTrade.Is_Create_Auto_Invoice = 1
                            objDispatchMasterEntryTrade.Is_Create_Auto_Invoice = 0

                            ''insert data into dispatch Detail Trade bulk sale
                            objDispatchMasterEntryTrade.arrDispatchDetailTradeBulkSale = New List(Of clsDispatchDetailTradeBulkSale)

                            objDispatchDetailEntryTrade.Item_Code = strItemCode
                            objDispatchDetailEntryTrade.Unit_code = "KG"
                            objDispatchDetailEntryTrade.Qty = DblMilkQty
                            objDispatchDetailEntryTrade.FatPer = DblFatPer
                            objDispatchDetailEntryTrade.SNFPer = DblSnfPer
                            objDispatchDetailEntryTrade.FatRate = DblDispatchFatRate
                            objDispatchDetailEntryTrade.SNFRate = DblDispatchSNFRate
                            objDispatchDetailEntryTrade.Fat_KG = dblDispatchFatKg
                            objDispatchDetailEntryTrade.SNF_KG = DblDispatchSNFKG
                            objDispatchDetailEntryTrade.FatAmount = DblDispatchFatAmount
                            objDispatchDetailEntryTrade.SNFAmount = DblDispatchSNFAmount
                            objDispatchDetailEntryTrade.Rate = DblDispatchNetMilkRate
                            objDispatchDetailEntryTrade.Amount = dblDispatchSaleAmount
                            objDispatchDetailEntryTrade.StandardRate = DblStandardrate

                            objDispatchMasterEntryTrade.arrDispatchDetailTradeBulkSale.Add(objDispatchDetailEntryTrade)

                            '' save and post data of Dispatch entry class and also create auto invoice
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Dispatch_BulkSale_Trade", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Location_Code", trans)
                            ClsDispatchBulkSaleTrade.SaveData(objDispatchMasterEntryTrade, True, trans)

                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Dispatch_BulkSale_Trade set Created_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "' where Document_No='" & objDispatchMasterEntryTrade.Document_No & "' ", trans)

                            ''insert data into Bulk Milk SRn Trade entry objects
                            objBulkMilkSRNTrade.SRN_Date = strDocument_Date
                            objBulkMilkSRNTrade.Vendor_Code = strVendorCode
                            objBulkMilkSRNTrade.Loc_Code = clsLocation.GetSegmentCode(strLocationCode, trans)
                            objBulkMilkSRNTrade.sub_location = strLocationCode
                            objBulkMilkSRNTrade.Challan_No = objDispatchMasterEntryTrade.Document_No
                            objBulkMilkSRNTrade.Challan_Date = strDocument_Date
                            objBulkMilkSRNTrade.Price_Code = strSRNPriceCode
                            objBulkMilkSRNTrade.Item_Code = strItemCode
                            objBulkMilkSRNTrade.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                            objBulkMilkSRNTrade.UOM = "KG"
                            objBulkMilkSRNTrade.Net_Weight = DblMilkQty
                            objBulkMilkSRNTrade.snf_Per = DblSnfPer
                            objBulkMilkSRNTrade.fat_per = DblFatPer
                            objBulkMilkSRNTrade.fat_KG = dblSRNFatKg
                            objBulkMilkSRNTrade.SNF_KG = DblSRNSNFKG
                            objBulkMilkSRNTrade.fat_Rate = DblSRNFatRate
                            objBulkMilkSRNTrade.SNF_Rate = DblSRNSNFRate
                            objBulkMilkSRNTrade.Amount = dblSRNSaleAmount
                            objBulkMilkSRNTrade.Actual_Amount = dblSRNSaleAmount
                            objBulkMilkSRNTrade.fat_amount = DblSRNFatAmount
                            objBulkMilkSRNTrade.SNF_Amount = DblSRNSNFAmount
                            ' objBulkMilkSRNTrade.StandardRate = DblStandardrate
                            objBulkMilkSRNTrade.StandardRate = dblSRNStandardRate

                            ''RICHA AGARWAL 08/03/2016
                            objBulkMilkSRNTrade.Tanker_No = strTankerNo
                            ''----------
                            ''-----------------

                            '' save and post data of Bulk Milk SRN Trade class
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Bulk_MILK_SRN", "Created_By", clsLocation.GetSegmentCode(strLocationCode, trans), "Loc_Code", trans)
                            clsBulkMilkSRNTrade.SaveData(objBulkMilkSRNTrade, True, trans)
                            clsBulkMilkSRNTrade.PostData(objBulkMilkSRNTrade.SRN_NO, "", trans)

                            Gv1.Rows(i).Cells(colSNRNO).Value = objBulkMilkSRNTrade.SRN_NO

                            ''-------- richa agarwal regarding to check balance qty before doing transaction
                            Dim balqty As Double = 0

                            ''richa agarwal 22 July 2016
                            'balqty = ClsLoadingTanker.getBalance(strItemCode, strLocationCode, "", "", strDocument_Date, trans, "KG")
                            balqty = ClsLoadingTanker.getBalance(strItemCode, clsLocation.GetSegmentCode(strLocationCode, trans), strLocationCode, "", strDocument_Date, trans, "KG")
                            ''-----------------------------

                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, trans), "1") = CompairStringResult.Equal Then
                                If balqty > 0 Then
                                    balqty = GetTolerane(balqty, DblMilkQty, trans)
                                End If
                            End If

                            If balqty < DblMilkQty Then
                                Throw New Exception("You cannot save this entry because stock quantity is less than dispatch quantity")
                            End If
                            ''---------------------------------------------------


                            '' post data of Bulk Milk SRN Trade class
                            ClsDispatchBulkSaleTrade.PostData("", objDispatchMasterEntryTrade.Document_No, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            Gv1.Rows(i).Cells(colDispatchCode).Value = objDispatchMasterEntryTrade.Document_No



                            ''bulk purchase invoice head and detail code

                            'If isVendorInvoiceNo(strVendorCode, trans) Then
                            '    objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, strDocument_Date, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, strLocationCode)
                            'Else
                            '    objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, strDocument_Date, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, strLocationCode)
                            'End If
                            'If clsCommon.myLen(objMilkPurchaseInvoiceHeadTrade.DOC_NO) <= 0 Then
                            '    Throw New Exception("Error In Document No Generation")
                            'End If

                            'objMilkPurchaseInvoiceHeadTrade.DOC_DATE = clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy")
                            'objMilkPurchaseInvoiceHeadTrade.vendor_code = strVendorCode
                            'objMilkPurchaseInvoiceHeadTrade.Loc_Code = strLocationCode
                            'objMilkPurchaseInvoiceHeadTrade.Vendor_Invoice_No = strVendorInvoiceNo
                            'objMilkPurchaseInvoiceHeadTrade.SRN_From_Date = clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy hh:mm:ss tt")
                            'objMilkPurchaseInvoiceHeadTrade.SRN_TO_Date = clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy hh:mm:ss tt")
                            'objMilkPurchaseInvoiceHeadTrade.Total_FAT_KG = dblSRNFatKg
                            'objMilkPurchaseInvoiceHeadTrade.Total_SNF_KG = DblSRNSNFKG
                            'objMilkPurchaseInvoiceHeadTrade.Total_QTY = DblMilkQty
                            'objMilkPurchaseInvoiceHeadTrade.Total_AMT = dblSRNSaleAmount
                            'objMilkPurchaseInvoiceHeadTrade.RoundOffAmount = 0
                            'objMilkPurchaseInvoiceHeadTrade.isSRNTradeInvoice = 1
                            'objMilkPurchaseInvoiceHeadTrade.isPosted = 0
                            'objMilkPurchaseInvoiceHeadTrade.Modified_By = objCommonVar.CurrentUserCode
                            'objMilkPurchaseInvoiceHeadTrade.Modified_Date = clsCommon.GetPrintDate(strDocument_Date, "dd/MM/yyyy hh:mm:ss tt")
                            'objMilkPurchaseInvoiceHeadTrade.Comp_Code = objCommonVar.CurrentCompanyCode
                            'objMilkPurchaseInvoiceHeadTrade.Created_By = objCommonVar.CurrentUserCode
                            'objMilkPurchaseInvoiceHeadTrade.Created_Date = clsCommon.GetPrintDate(strDocument_Date, "dd/MM/yyyy hh:mm:ss tt")
                            'objMilkPurchaseInvoiceHeadTrade.isNewEntry = True

                            'objMilkPurchaseInvoiceHeadTrade.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)

                            'objMilkPurchaseInvoiceDetailTrade.DOC_NO = clsCommon.myCstr(objMilkPurchaseInvoiceHeadTrade.DOC_NO)
                            'objMilkPurchaseInvoiceDetailTrade.SL_NO = clsCommon.myCstr(1)
                            'objMilkPurchaseInvoiceDetailTrade.SRN_NO = objBulkMilkSRNTrade.SRN_NO
                            'objMilkPurchaseInvoiceDetailTrade.SRN_Date = clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy hh:mm:ss tt")
                            'objMilkPurchaseInvoiceDetailTrade.Item_Code = strItemCode
                            'objMilkPurchaseInvoiceDetailTrade.Item_Desc = clsItemMaster.GetItemName(strItemCode, trans)
                            'objMilkPurchaseInvoiceDetailTrade.UOM = "KG"
                            'objMilkPurchaseInvoiceDetailTrade.Gross_Weight = 0
                            'objMilkPurchaseInvoiceDetailTrade.Tare_Weight = 0
                            'objMilkPurchaseInvoiceDetailTrade.Net_Weight = DblMilkQty
                            'objMilkPurchaseInvoiceDetailTrade.Invoice_Qty = DblMilkQty
                            'objMilkPurchaseInvoiceDetailTrade.fat_per = DblFatPer
                            'objMilkPurchaseInvoiceDetailTrade.fat_KG = dblSRNFatKg
                            'objMilkPurchaseInvoiceDetailTrade.fat_Rate = DblSRNFatRate
                            'objMilkPurchaseInvoiceDetailTrade.snf_Per = DblSnfPer
                            'objMilkPurchaseInvoiceDetailTrade.SNF_KG = DblSRNSNFKG
                            'objMilkPurchaseInvoiceDetailTrade.SNF_Rate = DblSRNSNFRate
                            'objMilkPurchaseInvoiceDetailTrade.Amount = dblSRNSaleAmount
                            'objMilkPurchaseInvoiceDetailTrade.Deduction = 0
                            'objMilkPurchaseInvoiceDetailTrade.Incentive = 0
                            'objMilkPurchaseInvoiceDetailTrade.Special_Deduction = 0
                            'objMilkPurchaseInvoiceDetailTrade.Actual_Amount = dblSRNSaleAmount
                            'objMilkPurchaseInvoiceDetailTrade.price_code = strSRNPriceCode
                            ''objMilkPurchaseInvoiceDetailTrade.NetRate = DblStandardrate
                            'objMilkPurchaseInvoiceDetailTrade.NetRate = dblSRNStandardRate

                            'objMilkPurchaseInvoiceHeadTrade.arrDetail.Add(objMilkPurchaseInvoiceDetailTrade)

                            'objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Loc_Code", trans)
                            'clsMilkPurchaseInvoiceHead.saveData(objMilkPurchaseInvoiceHeadTrade, trans)
                            'clsMilkPurchaseInvoiceHead.postData(objMilkPurchaseInvoiceHeadTrade.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                            'objCommonVar.CurrentUserCode = CurrentUserCode
                            'clsDBFuncationality.ExecuteNonQuery("update tspl_Bulk_milk_purchase_Invoice_head set Created_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "',Modified_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "',Posting_Date='" & clsCommon.GetPrintDate(strDocument_Date, "dd/MMM/yyyy") & "' where Doc_No='" & objMilkPurchaseInvoiceHeadTrade.DOC_NO & "' ", trans)


                            ''--------------------


                        End If
                    Next

                    ''Create Bulk Milk purchase invoice

                    j = 0
                    arrVendorInvoiceNo = New List(Of String)
                    For jjj As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(jjj).Cells(colIsValidated).Value Then
                            If Not arrVendorInvoiceNo.Contains(clsCommon.myCstr(Gv1.Rows(jjj).Cells("Vendor Bill No").Value)) Then
                                arrVendorInvoiceNo.Add(clsCommon.myCstr(Gv1.Rows(jjj).Cells("Vendor Bill No").Value))
                            End If
                        End If
                    Next
                    Dim dt As Date = Nothing
                    Dim cnt As Integer = 0
                    Dim arrProcessedInvoice As List(Of String) = Nothing
                    Dim ProcessedRow As New List(Of String)
                    arrProcessedInvoice = New List(Of String)
                    'For cnt = 0 To arrVendorInvoiceNo.Count - 1
                    Dim CurrentVendorInvoiceNO As String = ""
                    For k As Integer = 0 To Gv1.Rows.Count - 1
                        dt = clsCommon.GetPrintDate(Gv1.Rows(k).Cells("Document_Date").Value, "dd/MMM/yyyy")
                        If Gv1.Rows(k).Cells(colIsValidated).Value Then
                            If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)) <= 0 Then
                                If (Not ProcessedRow.Contains(k)) Then
                                    arrProcessedInvoice.Add(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value))
                                    CurrentVendorInvoiceNO = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    j += 1
                                    clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                                    objMilkPurchaseInvoiceHeadTrade = New clsMilkPurchaseInvoiceHead
                                    objMilkPurchaseInvoiceHeadTrade.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    objMilkPurchaseInvoiceHeadTrade = New clsMilkPurchaseInvoiceHead
                                    objMilkPurchaseInvoiceHeadTrade.isNewEntry = True
                                    If isVendorInvoiceNo(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor_Code").Value), trans) Then
                                        objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                    Else
                                        objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                    End If
                                    If clsCommon.myLen(objMilkPurchaseInvoiceHeadTrade.DOC_NO) <= 0 Then
                                        Throw New Exception("Error In Document No Genertion")
                                    End If

                                    objMilkPurchaseInvoiceHeadTrade.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                                    objMilkPurchaseInvoiceHeadTrade.vendor_code = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor_Code").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Loc_Code = clsLocation.GetSegmentCode(clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), trans)
                                    objMilkPurchaseInvoiceHeadTrade.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    objMilkPurchaseInvoiceHeadTrade.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.Total_FAT_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN FAT Qty.").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_SNF_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN SNF Qty").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_QTY = clsCommon.myCstr(Gv1.Rows(k).Cells("MILK QTY.").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_AMT = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN Sale Amount").Value)
                                    objMilkPurchaseInvoiceHeadTrade.RoundOffAmount = 0
                                    objMilkPurchaseInvoiceHeadTrade.isSRNTradeInvoice = 1
                                    objMilkPurchaseInvoiceHeadTrade.isPosted = 0
                                    objMilkPurchaseInvoiceHeadTrade.Modified_By = objCommonVar.CurrentUserCode
                                    objMilkPurchaseInvoiceHeadTrade.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.Comp_Code = objCommonVar.CurrentCompanyCode
                                    objMilkPurchaseInvoiceHeadTrade.Created_By = objCommonVar.CurrentUserCode
                                    objMilkPurchaseInvoiceHeadTrade.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                                    Dim TotalFatKg As Double = 0
                                    Dim TotalSNFKg As Double = 0
                                    Dim TotalQty As Double = 0
                                    Dim TotalAmt As Double = 0
                                    Dim objDetail As New clsMilkPurchaseInvoiceDetail
                                    objMilkPurchaseInvoiceHeadTrade.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                                    objDetail = New clsMilkPurchaseInvoiceDetail
                                    objDetail.DOC_NO = clsCommon.myCstr(objMilkPurchaseInvoiceHeadTrade.DOC_NO)
                                    objDetail.SL_NO = clsCommon.myCstr(1)
                                    objDetail.SRN_NO = Gv1.Rows(k).Cells(colSNRNO).Value
                                    objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                    objDetail.Item_Code = clsCommon.myCstr(Gv1.Rows(k).Cells("Item code").Value)
                                    objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                                    Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(Gv1.Rows(k).Cells("Item code").Value), trans)
                                    objDetail.UOM = DefaultUOm
                                    objDetail.Gross_Weight = 0
                                    objDetail.Tare_Weight = 0
                                    objDetail.Net_Weight = clsCommon.myCdbl(Gv1.Rows(k).Cells("MILK QTY.").Value)
                                    objDetail.Invoice_Qty = clsCommon.myCdbl(Gv1.Rows(k).Cells("MILK QTY.").Value)
                                    TotalQty += objDetail.Invoice_Qty
                                    objDetail.fat_per = clsCommon.myCdbl(Gv1.Rows(k).Cells("Sale_Fat %").Value)
                                    objDetail.fat_KG = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN FAT Qty.").Value)
                                    TotalFatKg += objDetail.fat_KG
                                    objDetail.fat_Rate = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN Fat Value").Value) / objDetail.fat_KG
                                    objDetail.snf_Per = clsCommon.myCdbl(Gv1.Rows(k).Cells("Sale_SNF %").Value)
                                    objDetail.SNF_KG = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN SNF Qty").Value)
                                    TotalSNFKg += objDetail.SNF_KG
                                    objDetail.SNF_Rate = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN SNF Value").Value) / objDetail.SNF_KG
                                    objDetail.Amount = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN Sale Amount").Value)
                                    TotalAmt += objDetail.Amount
                                    objDetail.Deduction = 0
                                    objDetail.Incentive = 0
                                    objDetail.Special_Deduction = 0
                                    objDetail.Actual_Amount = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN Sale Amount").Value)
                                    objDetail.price_code = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN Price Code").Value)
                                    objDetail.NetRate = clsCommon.myCdbl(Gv1.Rows(k).Cells("SRN Standard Rate").Value)
                                    objMilkPurchaseInvoiceHeadTrade.arrDetail.Add(objDetail)
                                    objMilkPurchaseInvoiceHeadTrade.Total_FAT_KG = TotalFatKg
                                    objMilkPurchaseInvoiceHeadTrade.Total_SNF_KG = TotalSNFKg
                                    objMilkPurchaseInvoiceHeadTrade.Total_QTY = TotalQty
                                    objMilkPurchaseInvoiceHeadTrade.Total_AMT = TotalAmt
                                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsLocation.GetSegmentCode(clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), trans), "Loc_Code", trans)
                                    clsMilkPurchaseInvoiceHead.saveData(objMilkPurchaseInvoiceHeadTrade, trans)
                                    clsMilkPurchaseInvoiceHead.postData(objMilkPurchaseInvoiceHeadTrade.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                                End If
                            Else
                                If (Not ProcessedRow.Contains(k)) Then

                                    arrProcessedInvoice.Add(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value))
                                    CurrentVendorInvoiceNO = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    j += 1
                                    clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                                    objMilkPurchaseInvoiceHeadTrade = New clsMilkPurchaseInvoiceHead
                                    objMilkPurchaseInvoiceHeadTrade.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    objMilkPurchaseInvoiceHeadTrade = New clsMilkPurchaseInvoiceHead
                                    objMilkPurchaseInvoiceHeadTrade.isNewEntry = True
                                    If isVendorInvoiceNo(clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor_Code").Value), trans) Then
                                        objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                    Else
                                        objMilkPurchaseInvoiceHeadTrade.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value))
                                    End If
                                    If clsCommon.myLen(objMilkPurchaseInvoiceHeadTrade.DOC_NO) <= 0 Then
                                        Throw New Exception("Error In Document No Genertion")
                                    End If
                                    dt = clsCommon.GetPrintDate(Gv1.Rows(k).Cells("Document_Date").Value, "dd/MMM/yyyy")
                                    objMilkPurchaseInvoiceHeadTrade.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                                    objMilkPurchaseInvoiceHeadTrade.vendor_code = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor_Code").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Loc_Code = clsLocation.GetSegmentCode(clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), trans)
                                    objMilkPurchaseInvoiceHeadTrade.Vendor_Invoice_No = clsCommon.myCstr(Gv1.Rows(k).Cells("Vendor Bill No").Value)
                                    objMilkPurchaseInvoiceHeadTrade.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.Total_FAT_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN FAT Qty.").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_SNF_KG = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN SNF Qty").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_QTY = clsCommon.myCstr(Gv1.Rows(k).Cells("MILK QTY.").Value)
                                    objMilkPurchaseInvoiceHeadTrade.Total_AMT = clsCommon.myCstr(Gv1.Rows(k).Cells("SRN Sale Amount").Value)
                                    objMilkPurchaseInvoiceHeadTrade.RoundOffAmount = 0
                                    objMilkPurchaseInvoiceHeadTrade.isSRNTradeInvoice = 1
                                    objMilkPurchaseInvoiceHeadTrade.isPosted = 0
                                    objMilkPurchaseInvoiceHeadTrade.Modified_By = objCommonVar.CurrentUserCode
                                    objMilkPurchaseInvoiceHeadTrade.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                                    objMilkPurchaseInvoiceHeadTrade.Comp_Code = objCommonVar.CurrentCompanyCode
                                    objMilkPurchaseInvoiceHeadTrade.Created_By = objCommonVar.CurrentUserCode
                                    objMilkPurchaseInvoiceHeadTrade.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                                    Dim TotalFatKg As Double = 0
                                    Dim TotalSNFKg As Double = 0
                                    Dim TotalQty As Double = 0
                                    Dim TotalAmt As Double = 0
                                    Dim objDetail As New clsMilkPurchaseInvoiceDetail
                                    objMilkPurchaseInvoiceHeadTrade.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                                    For jj As Integer = 0 To Gv1.Rows.Count - 1
                                        If Gv1.Rows(jj).Cells(colIsValidated).Value AndAlso clsCommon.CompairString(CurrentVendorInvoiceNO, clsCommon.myCstr(Gv1.Rows(jj).Cells("Vendor Bill No").Value)) = CompairStringResult.Equal AndAlso ((Not ProcessedRow.Contains(jj))) Then
                                            ProcessedRow.Add(jj)
                                            objDetail = New clsMilkPurchaseInvoiceDetail
                                            dt = clsCommon.GetPrintDate(Gv1.Rows(jj).Cells("Document_Date").Value, "dd/MMM/yyyy")
                                            objDetail.DOC_NO = clsCommon.myCstr(objMilkPurchaseInvoiceHeadTrade.DOC_NO)
                                            objDetail.SL_NO = clsCommon.myCstr(1)
                                            objDetail.SRN_NO = Gv1.Rows(jj).Cells(colSNRNO).Value
                                            objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                            objDetail.Item_Code = clsCommon.myCstr(Gv1.Rows(jj).Cells("Item code").Value)
                                            objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                                            Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(Gv1.Rows(jj).Cells("Item code").Value), trans)
                                            objDetail.UOM = DefaultUOm
                                            objDetail.Gross_Weight = 0
                                            objDetail.Tare_Weight = 0
                                            objDetail.Net_Weight = clsCommon.myCdbl(Gv1.Rows(jj).Cells("MILK QTY.").Value)
                                            objDetail.Invoice_Qty = clsCommon.myCdbl(Gv1.Rows(jj).Cells("MILK QTY.").Value)
                                            TotalQty += objDetail.Invoice_Qty
                                            objDetail.fat_per = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Sale_Fat %").Value)
                                            objDetail.fat_KG = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN FAT Qty.").Value)
                                            TotalFatKg += objDetail.fat_KG
                                            objDetail.fat_Rate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN Fat Value").Value) / objDetail.fat_KG
                                            objDetail.snf_Per = clsCommon.myCdbl(Gv1.Rows(jj).Cells("Sale_SNF %").Value)
                                            objDetail.SNF_KG = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN SNF Qty").Value)
                                            TotalSNFKg += objDetail.SNF_KG
                                            objDetail.SNF_Rate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN SNF Value").Value) / objDetail.SNF_KG
                                            objDetail.Amount = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN Sale Amount").Value)
                                            TotalAmt += objDetail.Amount
                                            objDetail.Deduction = 0
                                            objDetail.Incentive = 0
                                            objDetail.Special_Deduction = 0
                                            objDetail.Actual_Amount = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN Sale Amount").Value)
                                            objDetail.price_code = clsCommon.myCstr(Gv1.Rows(jj).Cells("SRN Price Code").Value)
                                            objDetail.NetRate = clsCommon.myCdbl(Gv1.Rows(jj).Cells("SRN Standard Rate").Value)
                                            objMilkPurchaseInvoiceHeadTrade.arrDetail.Add(objDetail)
                                        End If
                                    Next
                                    objMilkPurchaseInvoiceHeadTrade.Total_FAT_KG = TotalFatKg
                                    objMilkPurchaseInvoiceHeadTrade.Total_SNF_KG = TotalSNFKg
                                    objMilkPurchaseInvoiceHeadTrade.Total_QTY = TotalQty
                                    objMilkPurchaseInvoiceHeadTrade.Total_AMT = TotalAmt
                                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsLocation.GetSegmentCode(clsCommon.myCstr(Gv1.Rows(k).Cells("Location code").Value), trans), "Loc_Code", trans)
                                    clsMilkPurchaseInvoiceHead.saveData(objMilkPurchaseInvoiceHeadTrade, trans)
                                    clsMilkPurchaseInvoiceHead.postData(objMilkPurchaseInvoiceHeadTrade.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                                End If
                            End If
                        End If
                    Next


                    ''


                    ''to create invoice against multiple dispatched or single acc. to sale price code,customer code, location Code
                    CreateAutoInvoiceAgainstMultipleDispatch(trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_INVOICE_MASTER_BULKSALE SET Posting_Date =Document_Date ,Created_Date =Document_Date ,Modified_Date =Document_Date  WHERE IsUploader=1 ", trans)

                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Saved Successfully")
                Else
                    Throw New Exception("No Validated Rows found to save")
                End If


            End If
        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message & " At Row No " & (i + 1))
        End Try
    End Sub
    Public Shared Function GetTolerane(ByVal dblBalanceQty As Double, ByVal dblQty As Double, ByVal trans As SqlTransaction) As Double
        Dim dblToleranceQty As Double = 0
        Dim dblAllowedDispatchQty As Double = 0
        If dblBalanceQty < dblQty Then
            Dim dblTolerance As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StockToleranceLimit, clsFixedParameterCode.StockToleranceLimit, trans))
            If dblTolerance > 0 Then
                dblToleranceQty = (dblBalanceQty * dblTolerance) / 100
                dblAllowedDispatchQty = dblBalanceQty + dblToleranceQty
            End If
        Else
            dblAllowedDispatchQty = dblBalanceQty
        End If

        Return dblAllowedDispatchQty
    End Function

    Private Sub CreateAutoInvoiceAgainstMultipleDispatch(ByVal trans As SqlTransaction)
        Dim LocationCode As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Dim SalePriceCode As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            Dim dt1 As DataTable = Nothing
            dt1 = clsDBFuncationality.GetDataTable("select '' as SalePriceCode,'' as Customer,'' as Location,'' as DispatchCode,'' as DispatchDate,'' as ItemCode,'' as UnitCode,'' as TankerCode,'' as DispatchQty,'' as DispatchFatPer,'' as DispatchSNFPer,'' as DispatchRate,'' as DispatchAmount,'' as InvoiceFatKg,'' as InvoiceSNFKg", trans)
            dt1.Rows.RemoveAt(0)
            If ValidatedCount > 0 Then
                If rdbAgainstBulkSale.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Sale Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Gate_entry_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("UOM").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF Qty").Value) + "")
                        End If
                    Next
                ElseIf rdbAgainstBulkSaleTrade.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Dispatch Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Document_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "KG", "" + clsCommon.myCstr(grow.Cells("TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Dispatch Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Dispatch_Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("Dispatch FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Dispatch SNF Qty").Value) + "")
                        End If

                    Next
                End If
            End If

            Dim dtout As DataTable = Nothing
            dt1.DefaultView.Sort = "SalePriceCode,Customer,Location,DispatchDate"
            dtout = dt1.DefaultView.ToTable()

            dtmain = clsDBFuncationality.GetDataTable("Select '' as InvoiceSrNo,'' as SalePriceCode,'' as Customer,'' as Location,'' as DispatchCode,'' as DispatchDate,'' as ItemCode,'' as UnitCode,'' as TankerCode,'' as DispatchQty,'' as DispatchFatPer,'' as DispatchSNFPer,'' as DispatchRate,'' as DispatchAmount,'' as InvoiceQty,'' as InvoiceFatPer,'' as InvoiceSNFPer,'' as InvoiceRate,'' as InvoiceAmount,'' as InvoiceFatKg,'' as InvoiceSNFKg", trans)
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("DispatchDate"))) = CompairStringResult.Equal And clsCommon.CompairString(SalePriceCode, clsCommon.myCstr(dr("SalePriceCode"))) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Customer"))) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(dr("Location"))) = CompairStringResult.Equal Then
                        InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("DispatchAmount"))
                    Else
                        CustomerCount = CustomerCount + 1
                        InvoiceAmount = 0
                        InvoiceAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    End If
                    CustomerCode = clsCommon.myCstr(dr("Customer"))
                    LocationCode = clsCommon.myCstr(dr("Location"))
                    SalePriceCode = clsCommon.myCstr(dr("SalePriceCode"))
                    strdocdate = clsCommon.myCDate(dr("DispatchDate"))
                    Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans))
                    If AmountLimitInvoiceBulkSale > 0 Then
                        If InvoiceAmount > AmountLimitInvoiceBulkSale Then
                            If clsCommon.myCdbl(dr("DispatchAmount")) <= AmountLimitInvoiceBulkSale Then
                                'count = count + 1
                                CustomerCount = CustomerCount + 1
                                dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("SalePriceCode")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("DispatchCode")) + "", "" + clsCommon.myCstr(dr("DispatchDate")) + "", "" + clsCommon.myCstr(dr("ItemCode")) + "", "" + clsCommon.myCstr(dr("UnitCode")) + "", "" + clsCommon.myCstr(dr("TankerCode")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("InvoiceFatKg")) + "", "" + clsCommon.myCstr(dr("InvoiceSNFKg")) + "")
                                InvoiceAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                            End If
                        Else
                            dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("SalePriceCode")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("DispatchCode")) + "", "" + clsCommon.myCstr(dr("DispatchDate")) + "", "" + clsCommon.myCstr(dr("ItemCode")) + "", "" + clsCommon.myCstr(dr("UnitCode")) + "", "" + clsCommon.myCstr(dr("TankerCode")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("InvoiceFatKg")) + "", "" + clsCommon.myCstr(dr("InvoiceSNFKg")) + "")
                        End If
                    Else
                        dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("SalePriceCode")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("DispatchCode")) + "", "" + clsCommon.myCstr(dr("DispatchDate")) + "", "" + clsCommon.myCstr(dr("ItemCode")) + "", "" + clsCommon.myCstr(dr("UnitCode")) + "", "" + clsCommon.myCstr(dr("TankerCode")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("DispatchQty")) + "", " " + clsCommon.myCstr(dr("DispatchFatPer")) + "", "" + clsCommon.myCstr(dr("DispatchSNFPer")) + "", "" + clsCommon.myCstr(dr("DispatchRate")) + "", "" + clsCommon.myCstr(dr("DispatchAmount")) + "", "" + clsCommon.myCstr(dr("InvoiceFatKg")) + "", "" + clsCommon.myCstr(dr("InvoiceSNFKg")) + "")
                    End If
                Next
                InvoiceSaveData(trans)
            End If



            'If ValidatedCount > 0 Then
            '    For Each grow As GridViewRowInfo In Gv1.Rows
            '        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
            '            If clsCommon.CompairString(SalePriceCode, clsCommon.myCstr(grow.Cells("Sale Price Code").Value)) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(grow.Cells("Customer code").Value)) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(grow.Cells("Location Code").Value)) = CompairStringResult.Equal Then
            '                InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(grow.Cells("Sale_Amount").Value)
            '            Else1
            '                CustomerCount = CustomerCount + 1
            '                InvoiceAmount = 0
            '                InvoiceAmount = clsCommon.myCdbl(grow.Cells("Sale_Amount").Value)
            '            End If
            '            CustomerCode = clsCommon.myCstr(grow.Cells("Customer code").Value)
            '            LocationCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
            '            SalePriceCode = clsCommon.myCstr(grow.Cells("Sale Price Code").Value)
            '            Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans))
            '            If AmountLimitInvoiceBulkSale > 0 Then
            '                If InvoiceAmount > AmountLimitInvoiceBulkSale Then
            '                    If clsCommon.myCdbl(grow.Cells("Sale_Amount").Value) <= AmountLimitInvoiceBulkSale Then
            '                        'count = count + 1
            '                        CustomerCount = CustomerCount + 1
            '                        dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells("Sale Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Gate_entry_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "KG", "" + clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF Qty").Value) + "")
            '                        InvoiceAmount = clsCommon.myCdbl(grow.Cells("Sale_Amount").Value)
            '                    End If
            '                Else
            '                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells("Sale Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Gate_entry_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "KG", "" + clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF Qty").Value) + "")
            '                End If
            '            Else
            '                dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells("Sale Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Gate_entry_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "KG", "" + clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF Qty").Value) + "")
            '            End If
            '        End If
            '    Next
            '    InvoiceSaveData(trans)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Sub InvoiceSaveData(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As ClsInvoiceDetailBulkSale = Nothing
        Dim obj As ClsInvoiceBulkSale = Nothing
        'Dim strmaxdocdate As Date? = Nothing
        'Dim strmaxdatequery As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            For Each dr As DataRow In dtmain.Rows
                j += 1
                clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating  Invoice Records " & j & " of Total " & dtmain.Rows.Count)
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("InvoiceSrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("InvoiceSrNo"))) <> CompairStringResult.Equal Then
                    obj = New ClsInvoiceBulkSale()
                    obj.Document_Date = clsCommon.myCstr(dr("DispatchDate"))
                    obj.Customer_Code = clsCommon.myCstr(dr("Customer"))
                    obj.Location_Code = clsCommon.myCstr(dr("Location"))
                    ' strmaxdatequery = strmaxdatequery + (" Select '" & clsCommon.GetPrintDate(obj.Document_Date, "dd-MMM-yyyy") & "' as c1 Union All  ")
                    If rdbAgainstBulkSale.IsChecked Then
                        obj.InvoiceAgainst = "Against Dispatch"
                    ElseIf rdbAgainstBulkSaleTrade.IsChecked Then
                        obj.InvoiceAgainst = "Against Dispatch Trade"
                    End If
                    'obj.fromdate = clsCommon.GETSERVERDATE(trans)
                    'obj.todate = clsCommon.GETSERVERDATE(trans)
                    obj.IsUploader = 1
                    obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    If rdbAgainstBulkSale.IsChecked Then
                        objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    ElseIf rdbAgainstBulkSaleTrade.IsChecked Then
                        objTr.TradeTanker_No = clsCommon.myCstr(dr("TankerCode"))
                    End If

                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceFatKG = clsCommon.myCdbl(dr("InvoiceFatKg"))
                    objTr.InvoiceSNFKG = clsCommon.myCdbl(dr("InvoiceSNFKg"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))
                    DocuAmount = 0
                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                Else
                    objTr = New ClsInvoiceDetailBulkSale()
                    ' strmaxdatequery = strmaxdatequery + (" Select '" & clsCommon.GetPrintDate(clsCommon.myCDate(dr("DispatchDate")), "dd-MMM-yyyy") & "' as c1 Union All  ")
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    If rdbAgainstBulkSale.IsChecked Then
                        objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    ElseIf rdbAgainstBulkSaleTrade.IsChecked Then
                        objTr.TradeTanker_No = clsCommon.myCstr(dr("TankerCode"))
                    End If
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))

                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("InvoiceSrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    Else
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    End If
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_INVOICE_MASTER_BULKSALE", "Created_By", obj.Location_Code, "Location_Code", trans)
                    'If obj.arrInvoiceDetailBulkSale.Count > 1 Then
                    '    strmaxdatequery = strmaxdatequery.Substring(0, strmaxdatequery.Length - 11)
                    '    strmaxdatequery = "select MAX(c.c1 ) from (" & strmaxdatequery & ")c"
                    '    strmaxdocdate = clsCommon.myCDate(clsDBFuncationality.getSingleValue(strmaxdatequery, trans))
                    '    obj.Document_Date = strmaxdocdate
                    'End If

                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_INVOICE_MASTER_BULKSALE", "Created_By", obj.Location_Code, "Location_Code", trans)
                    ClsInvoiceBulkSale.SaveData(obj, True, trans)
                    ClsInvoiceBulkSale.PostData("", "'" + obj.Location_Code + "'", obj.Document_No, trans)
                    '  strmaxdatequery = String.Empty
                End If
                intCounter += 1

            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    Public Shared Function isVendorInvoiceNo(ByVal strVendor As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select isvendorInvoiceNo from tspl_vendor_master where Vendor_Code='" & strVendor & "'"
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub FinddeletePIandItsReferences(trans As SqlTransaction)
        Try
            Dim docNo As String = ""
            Dim qry As String = ";with FinalInvoiceData as (select distinct t1.*  from ( select tspl_Bulk_milk_purchase_Invoice_head.*,TSPL_VENDOR_INVOICE_HEAD.Document_No from tspl_Bulk_milk_purchase_Invoice_head inner join TSPL_VENDOR_INVOICE_HEAD on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ) as t1 left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=t1.Document_No where TSPL_PAYMENT_DETAIL.Payment_No is  null) , DuplicateVendorInvoice as (select DOC_NO ,Vendor_Invoice_No  from FinalInvoiceData where doc_no in ( select  tspl_bulk_milk_purchase_invoice_detail.doc_no     from tspl_bulk_milk_purchase_invoice_detail inner  join tspl_bulk_milk_srn on tspl_bulk_milk_srn.SRN_NO =tspl_bulk_milk_purchase_invoice_detail.SRN_NO  left outer join tspl_Bulk_milk_purchase_Invoice_head on  tspl_Bulk_milk_purchase_Invoice_head.doc_no =tspl_bulk_milk_purchase_invoice_detail.doc_no where convert(date,tspl_bulk_milk_srn.srn_date,108)<>convert(date, tspl_bulk_milk_purchase_invoice_detail.srn_date,108)  )  and isSRNTradeInvoice=0 )select DOC_NO from DuplicateVendorInvoice "
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate((i + 1) * 100 / dtt.Rows.Count, "Deleting Invoice " & (i + 1) & " of Total " & dtt.Rows.Count & " Records")
                    qry = "select tspl_Bulk_milk_purchase_Invoice_head.DOC_NO,InvGL.Voucher_No as InvGLVoucherNo,TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_VENDOR_INVOICE_HEAD.Document_No as VendorInvoiceNo ,TSPL_JOURNAL_MASTER.Voucher_No as APINVVoucherNO from tspl_Bulk_milk_purchase_Invoice_head  left outer join TSPL_JOURNAL_MASTER InvGL on InvGL.Source_Doc_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Against_Bulk_Srn_PI_adjustment=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No where tspl_Bulk_milk_purchase_Invoice_head.DOC_NO='" & dtt.Rows(i)("DOC_NO") & "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim dtAP As DataTable
                            ' '' Get Payment Entry Against AP Invoice
                            'docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            'If clsCommon.myLen(docNo) > 0 Then
                            '    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                            '    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                            '    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                            '        qry = "AP-Invoice " + docNo + " is used in following Payment -"
                            '        For Each drAP As DataRow In dtAP.Rows
                            '            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                            '        Next
                            '        Exit For
                            '    End If
                            'End If

                            ' '' Get Payment Entry Against Purchase  Invoice
                            'docNo = clsCommon.myCstr(dr("DOC_NO"))
                            'If clsCommon.myLen(docNo) > 0 Then
                            '    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                            '    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                            '    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                            '        qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                            '        For Each drAP As DataRow In dtAP.Rows
                            '            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                            '        Next
                            '        Exit For
                            '    End If
                            'End If
                            ''Delete Purchase Invoice Journal Entry 
                            docNo = clsCommon.myCstr(dr("InvGLVoucherNo"))
                            If clsCommon.myLen(docNo) > 0 Then

                                qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            End If

                            ''Delete AP Invoice Journal Entry 
                            docNo = clsCommon.myCstr(dr("APINVVoucherNO"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                            '' Get Payment Entry Against AP Invoice
                            docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                    qry = "AP-Invoice " + docNo + " is used in following Payment -"
                                    For Each drAP As DataRow In dtAP.Rows
                                        qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                    Next
                                    Throw New Exception(qry)
                                End If
                            End If

                            '' Get Payment Entry Against Purchase  Invoice
                            docNo = clsCommon.myCstr(dr("DOC_NO"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                    qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                                    For Each drAP As DataRow In dtAP.Rows
                                        qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                    Next
                                    Throw New Exception(qry)
                                End If
                            End If


                            ''Delete AP Invoice

                            docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in ('" & docNo & "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in ('" & docNo & "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If



                            docNo = clsCommon.myCstr(dr("Adjustment_No"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "'))"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete  from TSPL_INVENTORY_MOVEMENT_NEW  where Trans_Type='IC-AD' and Source_Doc_No in('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                                qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                            ''''
                            'Delete Purchase Invoice''''''''''''''
                            docNo = clsCommon.myCstr(dr("DOC_NO"))
                            qry = "delete tspl_Bulk_milk_purchase_Invoice_Detail  where DOC_NO in ('" + docNo + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            qry = "delete tspl_Bulk_milk_purchase_Invoice_head  where DOC_NO in ('" + docNo + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        Next
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub FinddeletePIandItsReferencesTrade(trans As SqlTransaction)
        Try
            Dim docNo As String = ""
            Dim qry As String = ";with FinalInvoiceData as (select distinct t1.*  from ( select tspl_Bulk_milk_purchase_Invoice_head.*,TSPL_VENDOR_INVOICE_HEAD.Document_No from tspl_Bulk_milk_purchase_Invoice_head inner join TSPL_VENDOR_INVOICE_HEAD on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ) as t1 left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=t1.Document_No where TSPL_PAYMENT_DETAIL.Payment_No is  null) , DuplicateVendorInvoice as (select DOC_NO ,Vendor_Invoice_No  from FinalInvoiceData where isSRNTradeInvoice=1 AND LEFT(DOC_NO,5)='BMP-0' )  select DOC_NO from DuplicateVendorInvoice "
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate((i + 1) * 100 / dtt.Rows.Count, "Deleting Invoice " & (i + 1) & " of Total " & dtt.Rows.Count & " Records")
                    qry = "select tspl_Bulk_milk_purchase_Invoice_head.DOC_NO,InvGL.Voucher_No as InvGLVoucherNo,TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_VENDOR_INVOICE_HEAD.Document_No as VendorInvoiceNo ,TSPL_JOURNAL_MASTER.Voucher_No as APINVVoucherNO from tspl_Bulk_milk_purchase_Invoice_head  left outer join TSPL_JOURNAL_MASTER InvGL on InvGL.Source_Doc_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Against_Bulk_Srn_PI_adjustment=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No where tspl_Bulk_milk_purchase_Invoice_head.DOC_NO='" & dtt.Rows(i)("DOC_NO") & "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim dtAP As DataTable
                            ' '' Get Payment Entry Against AP Invoice
                            'docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            'If clsCommon.myLen(docNo) > 0 Then
                            '    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                            '    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                            '    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                            '        qry = "AP-Invoice " + docNo + " is used in following Payment -"
                            '        For Each drAP As DataRow In dtAP.Rows
                            '            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                            '        Next
                            '        Exit For
                            '    End If
                            'End If

                            ' '' Get Payment Entry Against Purchase  Invoice
                            'docNo = clsCommon.myCstr(dr("DOC_NO"))
                            'If clsCommon.myLen(docNo) > 0 Then
                            '    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                            '    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                            '    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                            '        qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                            '        For Each drAP As DataRow In dtAP.Rows
                            '            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                            '        Next
                            '        Exit For
                            '    End If
                            'End If
                            ''Delete Purchase Invoice Journal Entry 
                            docNo = clsCommon.myCstr(dr("InvGLVoucherNo"))
                            If clsCommon.myLen(docNo) > 0 Then

                                qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            End If

                            ''Delete AP Invoice Journal Entry 
                            docNo = clsCommon.myCstr(dr("APINVVoucherNO"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                            '' Get Payment Entry Against AP Invoice
                            docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                    qry = "AP-Invoice " + docNo + " is used in following Payment -"
                                    For Each drAP As DataRow In dtAP.Rows
                                        qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                    Next
                                    Throw New Exception(qry)
                                End If
                            End If

                            '' Get Payment Entry Against Purchase  Invoice
                            docNo = clsCommon.myCstr(dr("DOC_NO"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                    qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                                    For Each drAP As DataRow In dtAP.Rows
                                        qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                    Next
                                    Throw New Exception(qry)
                                End If
                            End If


                            ''Delete AP Invoice

                            docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in ('" & docNo & "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in ('" & docNo & "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If



                            docNo = clsCommon.myCstr(dr("Adjustment_No"))
                            If clsCommon.myLen(docNo) > 0 Then
                                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "'))"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "delete  from TSPL_INVENTORY_MOVEMENT_NEW  where Trans_Type='IC-AD' and Source_Doc_No in('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                                qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                            ''''
                            'Delete Purchase Invoice''''''''''''''
                            docNo = clsCommon.myCstr(dr("DOC_NO"))
                            qry = "delete tspl_Bulk_milk_purchase_Invoice_Detail  where DOC_NO in ('" + docNo + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            qry = "delete tspl_Bulk_milk_purchase_Invoice_head  where DOC_NO in ('" + docNo + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        Next
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function FindDuplicateVendorInvoiceWithDifferentPI(trans As SqlTransaction) As DataTable
        Dim dt As DataTable = Nothing
        Try
            ''Old Query 
            'Dim qry As String = " ;with FinalInvoiceData as (select distinct t1.*  from ( select tspl_Bulk_milk_purchase_Invoice_head.*,TSPL_VENDOR_INVOICE_HEAD.Document_No from tspl_Bulk_milk_purchase_Invoice_head inner join TSPL_VENDOR_INVOICE_HEAD on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ) as t1 left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=t1.Document_No where TSPL_PAYMENT_DETAIL.Payment_No is  null) , DuplicateVendorInvoice as (select DOC_NO ,Vendor_Invoice_No  from FinalInvoiceData where Vendor_Invoice_No in ( select  Vendor_Invoice_No  from FinalInvoiceData   group by Vendor_Invoice_No having ISNULL(Vendor_Invoice_No,'')<>'' and  COUNT(Vendor_Invoice_No )>=2 )  and isSRNTradeInvoice=0 ) ,FinalDuplicateInvoice as( select DOC_NO,vendor_invoice_no from DuplicateVendorInvoice) ,final as (select SRN_NO,FinalDuplicateInvoice.Vendor_Invoice_No   from tspl_Bulk_milk_purchase_Invoice_Detail left outer join FinalDuplicateInvoice on FinalDuplicateInvoice.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO   where tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO in (select doc_no from FinalDuplicateInvoice )  ) select ROW_NUMBER () over(partition by  final.Vendor_Invoice_No  order by final.Vendor_Invoice_No ) as SLNO,final.Vendor_Invoice_No as [Vendor Invoice No] ,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.SRN_Date,TSPL_Bulk_MILK_SRN.Loc_Code as [Location Code],TSPL_Bulk_MILK_SRN.fat_KG as [FAT Qty.],TSPL_Bulk_MILK_SRN.SNF_KG as [SNF Qty],TSPL_Bulk_MILK_SRN.Net_Weight as [Net_Milk Qty.],TSPL_Bulk_MILK_SRN.Actual_Amount as [Purchase Amount] ,TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Item_Code as [Item Code],TSPL_Bulk_MILK_SRN.Gross_Weight as [Grsoss Weight],TSPL_Bulk_MILK_SRN.Tare_Weight as [Tare Weight],TSPL_Bulk_MILK_SRN.fat_per ,TSPL_Bulk_MILK_SRN.snf_Per,TSPL_Bulk_MILK_SRN.FatAmt ,TSPL_Bulk_MILK_SRN.SnfAmt ,TSPL_Bulk_MILK_SRN.fat_Rate ,TSPL_Bulk_MILK_SRN.SNF_Rate,TSPL_Bulk_MILK_SRN.NetRate as [Purchase_Rate],TSPL_Bulk_MILK_SRN.Price_Code as [SRN Price Chart Code]    from TSPL_Bulk_MILK_SRN inner join final on final.SRN_NO=TSPL_Bulk_MILK_SRN.SRN_NO  "
            ''New Query
            Dim qry As String = " ;with final as (select  tspl_bulk_milk_purchase_invoice_detail.SRN_NO  ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No   from tspl_bulk_milk_purchase_invoice_detail inner  join tspl_bulk_milk_srn on tspl_bulk_milk_srn.SRN_NO =tspl_bulk_milk_purchase_invoice_detail.SRN_NO  left outer join tspl_Bulk_milk_purchase_Invoice_head on  tspl_Bulk_milk_purchase_Invoice_head.doc_no =tspl_bulk_milk_purchase_invoice_detail.doc_no where convert(date,tspl_bulk_milk_srn.srn_date,108)<>convert(date, tspl_bulk_milk_purchase_invoice_detail.srn_date,108)  ) select ROW_NUMBER () over(partition by  final.Vendor_Invoice_No  order by final.Vendor_Invoice_No ) as SLNO,final.Vendor_Invoice_No as [Vendor Invoice No] ,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.SRN_Date,TSPL_Bulk_MILK_SRN.Loc_Code as [Location Code],TSPL_Bulk_MILK_SRN.fat_KG as [FAT Qty.],TSPL_Bulk_MILK_SRN.SNF_KG as [SNF Qty],TSPL_Bulk_MILK_SRN.Net_Weight as [Net_Milk Qty.],TSPL_Bulk_MILK_SRN.Actual_Amount as [Purchase Amount] ,TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Item_Code as [Item Code],TSPL_Bulk_MILK_SRN.Gross_Weight as [Grsoss Weight],TSPL_Bulk_MILK_SRN.Tare_Weight as [Tare Weight],TSPL_Bulk_MILK_SRN.fat_per ,TSPL_Bulk_MILK_SRN.snf_Per,TSPL_Bulk_MILK_SRN.FatAmt ,TSPL_Bulk_MILK_SRN.SnfAmt ,TSPL_Bulk_MILK_SRN.fat_Rate ,TSPL_Bulk_MILK_SRN.SNF_Rate,TSPL_Bulk_MILK_SRN.NetRate as [Purchase_Rate],TSPL_Bulk_MILK_SRN.Price_Code as [SRN Price Chart Code]    from TSPL_Bulk_MILK_SRN inner join final on final.SRN_NO=TSPL_Bulk_MILK_SRN.SRN_NO "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Function FindDuplicateVendorInvoiceWithDifferentPITrade(trans As SqlTransaction) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = "  ;with FinalInvoiceData as (select distinct t1.*  from ( select tspl_Bulk_milk_purchase_Invoice_head.*,TSPL_VENDOR_INVOICE_HEAD.Document_No from tspl_Bulk_milk_purchase_Invoice_head inner join TSPL_VENDOR_INVOICE_HEAD on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No ) as t1 left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=t1.Document_No where TSPL_PAYMENT_DETAIL.Payment_No is  null)  , DuplicateVendorInvoice as (select DOC_NO ,Vendor_Invoice_No  from FinalInvoiceData where isSRNTradeInvoice=1 AND LEFT(DOC_NO,5)='BMP-0' )  ,FinalDuplicateInvoice as( select DOC_NO,vendor_invoice_no from DuplicateVendorInvoice)  ,final as (select SRN_NO,FinalDuplicateInvoice.Vendor_Invoice_No   from tspl_Bulk_milk_purchase_Invoice_Detail left outer join FinalDuplicateInvoice on FinalDuplicateInvoice.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO   where tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO in (select doc_no from FinalDuplicateInvoice )  )  select ROW_NUMBER () over(partition by  final.Vendor_Invoice_No  order by final.Vendor_Invoice_No ) as SLNO,final.Vendor_Invoice_No as [Vendor Invoice No] ,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.SRN_Date,TSPL_Bulk_MILK_SRN.Loc_Code as [Location Code],TSPL_Bulk_MILK_SRN.fat_KG as [FAT Qty.],TSPL_Bulk_MILK_SRN.SNF_KG as [SNF Qty],TSPL_Bulk_MILK_SRN.Net_Weight as [Net_Milk Qty.],TSPL_Bulk_MILK_SRN.Actual_Amount as [Purchase Amount] ,TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Item_Code as [Item Code],TSPL_Bulk_MILK_SRN.Gross_Weight as [Grsoss Weight],TSPL_Bulk_MILK_SRN.Tare_Weight as [Tare Weight],TSPL_Bulk_MILK_SRN.fat_per ,TSPL_Bulk_MILK_SRN.snf_Per,TSPL_Bulk_MILK_SRN.FatAmt ,TSPL_Bulk_MILK_SRN.SnfAmt ,TSPL_Bulk_MILK_SRN.fat_Rate ,TSPL_Bulk_MILK_SRN.SNF_Rate,TSPL_Bulk_MILK_SRN.StandardRate as [Purchase_Rate],TSPL_Bulk_MILK_SRN.Price_Code as [SRN Price Chart Code]    from TSPL_Bulk_MILK_SRN inner join final on final.SRN_NO=TSPL_Bulk_MILK_SRN.SRN_NO  "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Sub createPurchaseInvoice(dtt As DataTable, trans As SqlTransaction)
        Try
            Dim j As Integer = 0
            arrVendorInvoiceNo = New List(Of String)
            For jjj As Integer = 0 To dtt.Rows.Count - 1
                If Not arrVendorInvoiceNo.Contains(dtt.Rows(jjj)("Vendor Invoice No")) Then
                    arrVendorInvoiceNo.Add(dtt.Rows(jjj)("Vendor Invoice No"))
                End If
            Next

            Dim cnt As Integer = 0
            Dim arrProcessedInvoice As List(Of String) = Nothing
            Dim ProcessedRow As New List(Of String)
            arrProcessedInvoice = New List(Of String)
            Dim CurrentVendorInvoiceNO As String = ""
            For k As Integer = 0 To dtt.Rows.Count - 1
                If (Not ProcessedRow.Contains(k)) Then
                    arrProcessedInvoice.Add(clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No")))
                    CurrentVendorInvoiceNO = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                    j += 1
                    Dim dt As Date = clsCommon.myCDate(dtt.Rows(k)("SRN_Date"))
                    clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                    Dim obj As clsMilkPurchaseInvoiceHead = New clsMilkPurchaseInvoiceHead
                    obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                    obj.isNewEntry = True
                    If isVendorInvoiceNo(clsCommon.myCstr(dtt.Rows(k)("Vendor_Code")), trans) Then
                        obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                    Else
                        obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                    End If
                    If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                        Throw New Exception("Error In Document No Genertion")
                    End If

                    obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                    obj.vendor_code = clsCommon.myCstr(dtt.Rows(k)("Vendor_Code"))
                    obj.Loc_Code = clsCommon.myCstr(dtt.Rows(k)("Location code"))
                    obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                    obj.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.Total_FAT_KG = clsCommon.myCstr(dtt.Rows(k)("FAT Qty."))
                    obj.Total_SNF_KG = clsCommon.myCstr(dtt.Rows(k)("SNF Qty"))
                    obj.Total_QTY = clsCommon.myCstr(dtt.Rows(k)("Net_MILK QTY."))
                    obj.Total_AMT = clsCommon.myCstr(dtt.Rows(k)("Purchase Amount"))
                    obj.RoundOffAmount = 0
                    obj.isSRNTradeInvoice = 0
                    obj.isPosted = 0
                    obj.Modified_By = objCommonVar.CurrentUserCode
                    obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.Comp_Code = objCommonVar.CurrentCompanyCode
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                    Dim TotalFatKg As Double = 0
                    Dim TotalSNFKg As Double = 0
                    Dim TotalQty As Double = 0
                    Dim TotalAmt As Double = 0
                    Dim objDetail As New clsMilkPurchaseInvoiceDetail
                    obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                    Dim slno As Integer = 0
                    For jj As Integer = 0 To dtt.Rows.Count - 1
                        If clsCommon.CompairString(CurrentVendorInvoiceNO, dtt.Rows(jj)("Vendor Invoice No")) = CompairStringResult.Equal AndAlso ((Not ProcessedRow.Contains(jj))) Then
                            ProcessedRow.Add(jj)
                            slno += 1
                            objDetail = New clsMilkPurchaseInvoiceDetail
                            objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                            objDetail.SL_NO = clsCommon.myCstr(slno)
                            objDetail.SRN_NO = dtt.Rows(jj)("SRN_NO")
                            dt = clsCommon.myCDate(dtt.Rows(jj)("SRN_Date"))
                            If dt > obj.DOC_DATE Then
                                obj.DOC_DATE = dt
                            End If
                            objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objDetail.Item_Code = clsCommon.myCstr(dtt.Rows(jj)("Item code"))
                            objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                            Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(dtt.Rows(jj)("Item code")), trans)
                            objDetail.UOM = DefaultUOm
                            objDetail.Gross_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Grsoss Weight"))
                            objDetail.Tare_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Tare Weight"))
                            objDetail.Net_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Net_MILK QTY."))
                            objDetail.Invoice_Qty = clsCommon.myCdbl(dtt.Rows(jj)("Net_MILK QTY."))
                            TotalQty += objDetail.Invoice_Qty
                            objDetail.fat_per = clsCommon.myCdbl(dtt.Rows(jj)("fat_per"))
                            objDetail.fat_KG = clsCommon.myCdbl(dtt.Rows(jj)("FAT Qty."))
                            TotalFatKg += objDetail.fat_KG
                            objDetail.fat_Rate = clsCommon.myCdbl(dtt.Rows(jj)("Fat_Rate"))
                            objDetail.snf_Per = clsCommon.myCdbl(dtt.Rows(jj)("Snf_per"))
                            objDetail.SNF_KG = clsCommon.myCdbl(dtt.Rows(jj)("SNF Qty"))
                            TotalSNFKg += objDetail.SNF_KG
                            objDetail.SNF_Rate = clsCommon.myCdbl(dtt.Rows(jj)("Snf_Rate"))
                            objDetail.Amount = clsCommon.myCdbl(dtt.Rows(jj)("Purchase Amount"))
                            TotalAmt += objDetail.Amount
                            objDetail.Deduction = 0
                            objDetail.Incentive = 0
                            objDetail.Special_Deduction = 0
                            objDetail.Actual_Amount = clsCommon.myCdbl(dtt.Rows(jj)("Purchase Amount"))
                            objDetail.price_code = clsCommon.myCstr(dtt.Rows(jj)("SRN Price Chart Code"))
                            objDetail.NetRate = clsCommon.myCdbl(dtt.Rows(jj)("Purchase_Rate"))
                            obj.arrDetail.Add(objDetail)
                        End If
                    Next
                    For ii As Integer = 0 To obj.arrDetail.Count - 1
                        If obj.arrDetail(ii).SRN_Date > obj.DOC_DATE Then
                            obj.DOC_DATE = obj.arrDetail(ii).SRN_Date
                            obj.SRN_TO_Date = obj.DOC_DATE
                        End If
                    Next
                    obj.Total_FAT_KG = TotalFatKg
                    obj.Total_SNF_KG = TotalSNFKg
                    obj.Total_QTY = TotalQty
                    obj.Total_AMT = TotalAmt
                    Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(dtt.Rows(k)("Location code")), "Loc_Code", trans)
                    clsMilkPurchaseInvoiceHead.saveData(obj, trans)
                    clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub createPurchaseInvoiceTrade(dtt As DataTable, trans As SqlTransaction)
        Try
            Dim j As Integer = 0
            arrVendorInvoiceNo = New List(Of String)
            For jjj As Integer = 0 To dtt.Rows.Count - 1
                If Not arrVendorInvoiceNo.Contains(dtt.Rows(jjj)("Vendor Invoice No")) Then
                    arrVendorInvoiceNo.Add(dtt.Rows(jjj)("Vendor Invoice No"))
                End If
            Next

            Dim cnt As Integer = 0
            Dim arrProcessedInvoice As List(Of String) = Nothing
            Dim ProcessedRow As New List(Of String)
            arrProcessedInvoice = New List(Of String)
            Dim CurrentVendorInvoiceNO As String = ""
            For k As Integer = 0 To dtt.Rows.Count - 1
                If clsCommon.myLen(dtt.Rows(k)("Vendor Invoice No")) <= 0 Then
                    If (Not ProcessedRow.Contains(k)) Then
                        arrProcessedInvoice.Add(clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No")))
                        CurrentVendorInvoiceNO = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                        j += 1
                        Dim dt As Date = clsCommon.myCDate(dtt.Rows(k)("SRN_Date"))
                        clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                        Dim obj As clsMilkPurchaseInvoiceHead = New clsMilkPurchaseInvoiceHead
                        obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                        obj.isNewEntry = True
                        If isVendorInvoiceNo(clsCommon.myCstr(dtt.Rows(k)("Vendor_Code")), trans) Then
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                        Else
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                        End If
                        If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                            Throw New Exception("Error In Document No Genertion")
                        End If

                        obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                        obj.vendor_code = clsCommon.myCstr(dtt.Rows(k)("Vendor_Code"))
                        obj.Loc_Code = clsCommon.myCstr(dtt.Rows(k)("Location code"))
                        obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                        obj.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                        obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                        obj.Total_FAT_KG = clsCommon.myCstr(dtt.Rows(k)("FAT Qty."))
                        obj.Total_SNF_KG = clsCommon.myCstr(dtt.Rows(k)("SNF Qty"))
                        obj.Total_QTY = clsCommon.myCstr(dtt.Rows(k)("Net_MILK QTY."))
                        obj.Total_AMT = clsCommon.myCstr(dtt.Rows(k)("Purchase Amount"))
                        obj.RoundOffAmount = 0
                        obj.isSRNTradeInvoice = 1
                        obj.isPosted = 0
                        obj.Modified_By = objCommonVar.CurrentUserCode
                        obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                        obj.Created_By = objCommonVar.CurrentUserCode
                        obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                        Dim TotalFatKg As Double = 0
                        Dim TotalSNFKg As Double = 0
                        Dim TotalQty As Double = 0
                        Dim TotalAmt As Double = 0
                        Dim objDetail As New clsMilkPurchaseInvoiceDetail
                        obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                        Dim slno As Integer = 0
                        slno += 1
                        objDetail = New clsMilkPurchaseInvoiceDetail
                        objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                        objDetail.SL_NO = clsCommon.myCstr(slno)
                        objDetail.SRN_NO = dtt.Rows(k)("SRN_NO")
                        objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                        objDetail.Item_Code = clsCommon.myCstr(dtt.Rows(k)("Item code"))
                        objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                        Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(dtt.Rows(k)("Item code")), trans)
                        objDetail.UOM = DefaultUOm
                        objDetail.Gross_Weight = clsCommon.myCdbl(dtt.Rows(k)("Grsoss Weight"))
                        objDetail.Tare_Weight = clsCommon.myCdbl(dtt.Rows(k)("Tare Weight"))
                        objDetail.Net_Weight = clsCommon.myCdbl(dtt.Rows(k)("Net_MILK QTY."))
                        objDetail.Invoice_Qty = clsCommon.myCdbl(dtt.Rows(k)("Net_MILK QTY."))
                        TotalQty += objDetail.Invoice_Qty
                        objDetail.fat_per = clsCommon.myCdbl(dtt.Rows(k)("fat_per"))
                        objDetail.fat_KG = clsCommon.myCdbl(dtt.Rows(k)("FAT Qty."))
                        TotalFatKg += objDetail.fat_KG
                        objDetail.fat_Rate = clsCommon.myCdbl(dtt.Rows(k)("Fat_Rate"))
                        objDetail.snf_Per = clsCommon.myCdbl(dtt.Rows(k)("Snf_per"))
                        objDetail.SNF_KG = clsCommon.myCdbl(dtt.Rows(k)("SNF Qty"))
                        TotalSNFKg += objDetail.SNF_KG
                        objDetail.SNF_Rate = clsCommon.myCdbl(dtt.Rows(k)("Snf_Rate"))
                        objDetail.Amount = clsCommon.myCdbl(dtt.Rows(k)("Purchase Amount"))
                        TotalAmt += objDetail.Amount
                        objDetail.Deduction = 0
                        objDetail.Incentive = 0
                        objDetail.Special_Deduction = 0
                        objDetail.Actual_Amount = clsCommon.myCdbl(dtt.Rows(k)("Purchase Amount"))
                        objDetail.price_code = clsCommon.myCstr(dtt.Rows(k)("SRN Price Chart Code"))
                        objDetail.NetRate = clsCommon.myCdbl(dtt.Rows(k)("Purchase_Rate"))
                        obj.arrDetail.Add(objDetail)
                        obj.Total_FAT_KG = TotalFatKg
                        obj.Total_SNF_KG = TotalSNFKg
                        obj.Total_QTY = TotalQty
                        obj.Total_AMT = TotalAmt
                        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
                        objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(dtt.Rows(k)("Location code")), "Loc_Code", trans)
                        clsMilkPurchaseInvoiceHead.saveData(obj, trans)
                        clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                        objCommonVar.CurrentUserCode = CurrentUserCode
                    End If
                Else
                    If (Not ProcessedRow.Contains(k)) Then
                        arrProcessedInvoice.Add(clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No")))
                        CurrentVendorInvoiceNO = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))


                        j += 1
                        Dim dt As Date = clsCommon.myCDate(dtt.Rows(k)("SRN_Date"))
                        clsCommon.ProgressBarPercentUpdate(j / arrVendorInvoiceNo.Count * 100, " Saving and posting Record(s) " & j & " of Total " & arrVendorInvoiceNo.Count & " Bulk Milk Purchase Invoice Document ")
                        Dim obj As clsMilkPurchaseInvoiceHead = New clsMilkPurchaseInvoiceHead
                        obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                        obj.isNewEntry = True
                        If isVendorInvoiceNo(clsCommon.myCstr(dtt.Rows(k)("Vendor_Code")), trans) Then
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                        Else
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(dtt.Rows(k)("Location code")))
                        End If
                        If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                            Throw New Exception("Error In Document No Genertion")
                        End If

                        obj.DOC_DATE = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                        obj.vendor_code = clsCommon.myCstr(dtt.Rows(k)("Vendor_Code"))
                        obj.Loc_Code = clsCommon.myCstr(dtt.Rows(k)("Location code"))
                        obj.Vendor_Invoice_No = clsCommon.myCstr(dtt.Rows(k)("Vendor Invoice No"))
                        obj.SRN_From_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                        obj.SRN_TO_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                        obj.Total_FAT_KG = clsCommon.myCstr(dtt.Rows(k)("FAT Qty."))
                        obj.Total_SNF_KG = clsCommon.myCstr(dtt.Rows(k)("SNF Qty"))
                        obj.Total_QTY = clsCommon.myCstr(dtt.Rows(k)("Net_MILK QTY."))
                        obj.Total_AMT = clsCommon.myCstr(dtt.Rows(k)("Purchase Amount"))
                        obj.RoundOffAmount = 0
                        obj.isSRNTradeInvoice = 1
                        obj.isPosted = 0
                        obj.Modified_By = objCommonVar.CurrentUserCode
                        obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                        obj.Created_By = objCommonVar.CurrentUserCode
                        obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                        Dim TotalFatKg As Double = 0
                        Dim TotalSNFKg As Double = 0
                        Dim TotalQty As Double = 0
                        Dim TotalAmt As Double = 0
                        Dim objDetail As New clsMilkPurchaseInvoiceDetail
                        obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
                        Dim slno As Integer = 0
                        For jj As Integer = 0 To dtt.Rows.Count - 1
                            If clsCommon.CompairString(CurrentVendorInvoiceNO, dtt.Rows(jj)("Vendor Invoice No")) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.vendor_code, dtt.Rows(jj)("Vendor_Code")) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Loc_Code, dtt.Rows(jj)("Location code")) = CompairStringResult.Equal AndAlso ((Not ProcessedRow.Contains(jj))) Then
                                ProcessedRow.Add(jj)
                                dt = clsCommon.myCDate(dtt.Rows(k)("SRN_Date"))
                                If dt > obj.DOC_DATE Then
                                    obj.DOC_DATE = dt
                                End If
                                slno += 1
                                objDetail = New clsMilkPurchaseInvoiceDetail
                                objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                                objDetail.SL_NO = clsCommon.myCstr(slno)
                                objDetail.SRN_NO = dtt.Rows(jj)("SRN_NO")
                                objDetail.SRN_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                                objDetail.Item_Code = clsCommon.myCstr(dtt.Rows(jj)("Item code"))
                                objDetail.Item_Desc = clsItemMaster.GetItemName(objDetail.Item_Code, trans)
                                Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(clsCommon.myCstr(dtt.Rows(jj)("Item code")), trans)
                                objDetail.UOM = DefaultUOm
                                objDetail.Gross_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Grsoss Weight"))
                                objDetail.Tare_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Tare Weight"))
                                objDetail.Net_Weight = clsCommon.myCdbl(dtt.Rows(jj)("Net_MILK QTY."))
                                objDetail.Invoice_Qty = clsCommon.myCdbl(dtt.Rows(jj)("Net_MILK QTY."))
                                TotalQty += objDetail.Invoice_Qty
                                objDetail.fat_per = clsCommon.myCdbl(dtt.Rows(jj)("fat_per"))
                                objDetail.fat_KG = clsCommon.myCdbl(dtt.Rows(jj)("FAT Qty."))
                                TotalFatKg += objDetail.fat_KG
                                objDetail.fat_Rate = clsCommon.myCdbl(dtt.Rows(jj)("Fat_Rate"))
                                objDetail.snf_Per = clsCommon.myCdbl(dtt.Rows(jj)("Snf_per"))
                                objDetail.SNF_KG = clsCommon.myCdbl(dtt.Rows(jj)("SNF Qty"))
                                TotalSNFKg += objDetail.SNF_KG
                                objDetail.SNF_Rate = clsCommon.myCdbl(dtt.Rows(jj)("Snf_Rate"))
                                objDetail.Amount = clsCommon.myCdbl(dtt.Rows(jj)("Purchase Amount"))
                                TotalAmt += objDetail.Amount
                                objDetail.Deduction = 0
                                objDetail.Incentive = 0
                                objDetail.Special_Deduction = 0
                                objDetail.Actual_Amount = clsCommon.myCdbl(dtt.Rows(jj)("Purchase Amount"))
                                objDetail.price_code = clsCommon.myCstr(dtt.Rows(jj)("SRN Price Chart Code"))
                                objDetail.NetRate = clsCommon.myCdbl(dtt.Rows(jj)("Purchase_Rate"))
                                obj.arrDetail.Add(objDetail)
                            End If
                        Next
                        For ii As Integer = 0 To obj.arrDetail.Count - 1
                            If obj.arrDetail(ii).SRN_Date > obj.DOC_DATE Then
                                obj.DOC_DATE = obj.arrDetail(ii).SRN_Date
                            End If
                        Next
                        obj.Total_FAT_KG = TotalFatKg
                        obj.Total_SNF_KG = TotalSNFKg
                        obj.Total_QTY = TotalQty
                        obj.Total_AMT = TotalAmt
                        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
                        objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("tspl_Bulk_milk_purchase_Invoice_head", "Created_By", clsCommon.myCstr(dtt.Rows(k)("Location code")), "Loc_Code", trans)
                        clsMilkPurchaseInvoiceHead.saveData(obj, trans)
                        clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
                        objCommonVar.CurrentUserCode = CurrentUserCode
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Sub RecreatePIWithDuplicateVendorInvoice()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommon.ProgressBarPercentShow()

            Dim dt As DataTable = FindDuplicateVendorInvoiceWithDifferentPI(trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FinddeletePIandItsReferences(trans)
                createPurchaseInvoice(dt, trans)
            End If
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Recreated Successfully")
        Catch ex As Exception
            trans.Rollback()
            Try
                clsCommon.ProgressBarPercentHide()
            Catch ex1 As Exception

            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbAgainstBulkprocurement_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstBulkprocurement.ToggleStateChanged
        btnMergeAndRecreate.Visible = False
    End Sub

    Private Sub btnMergeAndRecreate_Click(sender As Object, e As EventArgs) Handles btnMergeAndRecreate.Click
        RecreatePIWithDuplicateVendorInvoice()
    End Sub


    Sub RecreatePIWithDuplicateVendorInvoiceTrade()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsCommon.ProgressBarPercentShow()

            Dim dt As DataTable = FindDuplicateVendorInvoiceWithDifferentPITrade(trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FinddeletePIandItsReferencesTrade(trans)
                createPurchaseInvoiceTrade(dt, trans)
            End If
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Recreated Successfully")
        Catch ex As Exception
            trans.Rollback()
            Try
                clsCommon.ProgressBarPercentHide()
            Catch ex1 As Exception

            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnMergeAndRecreateTrade_Click(sender As Object, e As EventArgs) Handles btnMergeAndRecreateTrade.Click
        RecreatePIWithDuplicateVendorInvoiceTrade()
    End Sub

    Private Sub rdbAgainstBulkSaleTrade_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstBulkSaleTrade.ToggleStateChanged
        btnMergeAndRecreateTrade.Visible = False
    End Sub
End Class