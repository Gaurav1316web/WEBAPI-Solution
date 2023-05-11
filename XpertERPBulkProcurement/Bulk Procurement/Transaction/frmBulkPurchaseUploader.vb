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
    Dim arrBulkProParameter As New Dictionary(Of String, clsfrmParameterMaster)

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
        Dim qry As String = " select Code+'('+Description+')' as Name,Code,Description,Type,Nature,IsMandatory,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from TSPL_PARAMETER_MASTER   where 1=1 AND ( Param_for='PLANT' or Param_for='BOTH')  AND Type NOT in ('AAB','ABB') order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsfrmParameterMaster
                obj.code = clsCommon.myCstr(dr("Code"))
                obj.desc = clsCommon.myCstr(dr("Description"))
                obj.type = clsCommon.myCstr(dr("Type"))
                obj.nature = clsCommon.myCstr(dr("Nature"))
                obj.IsMandatory = clsCommon.myCdbl(dr("IsMandatory"))
                arrBulkProParameter.Add(clsCommon.myCstr(dr("Name")), obj)
            Next
        End If
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
            ''If transportSql.importExcel(Gv1, "Party Code / Vendor Code", "Gate_Entry_DATE", "Location code", "TankerNo.", "Item code", "GROSS WEIGHT", "DIP Value", "TARE WEIGHT", "Net_MILK QTY.", "PM00001(FAT %)", "PM00002(SNF %)", "PM00003(CLR)", "PM14-151(RM VALUE)", "PM14-1510(Acidity (B.B))", "PM14-1511(Temprature ° C)", "PM14-1512(Alcohol)", "PM14-1513(ACI 8.5% SNF)", "PM14-1515(Taste)", "PM14-1516(Chenna %)", "PM14-1517(B.R. Reading)", "PM14-1518(Detergent)", "PM14-1519(Acidity (A.B))", "PM14-152(FFA%)", "PM14-1520(Adultration)", "PM14-1521(Flavour)", "PM14-153(PROTEIN)", "PM14-154(NA+ PPM)", "PM14-155(K PPM)", "PM14-156(MILK ASH %)", "PM14-157(SUGAR)", "PM14-158(MALTOSE)", "PM14-159(GLUCOSE)", "Silo No", "SRN Price Chart Code", "FAT Ratio", "SNF Ratio", "FAT Weightage", "SNF Weightage", "Purchase_Rate", "Purchase_Amount", "FAT Qty.", "SNF Qty", "Fat Value", "SNF Value", "Vendor Invoice No", "IsJobWork", "JobWork Location") Then

            Dim arr As New List(Of String)
            arr.Add("Party Code / Vendor Code")
            arr.Add("Gate_Entry_DATE")
            arr.Add("Location code")
            arr.Add("TankerNo.")
            arr.Add("Item code")
            arr.Add("GROSS WEIGHT")
            arr.Add("DIP Value")
            arr.Add("TARE WEIGHT")
            arr.Add("Net_MILK QTY.")
            For Each key As String In arrBulkProParameter.Keys
                arr.Add(key)
            Next
            arr.Add("Silo No")
            'arr.Add("SRN Price Chart Code")
            'arr.Add("FAT Ratio")
            'arr.Add("SNF Ratio")
            'arr.Add("FAT Weightage")
            'arr.Add("SNF Weightage")
            'arr.Add("Purchase_Rate")
            'arr.Add("Purchase_Amount")
            'arr.Add("FAT Qty.")
            'arr.Add("SNF Qty")
            'arr.Add("Fat Value")
            'arr.Add("SNF Value")
            arr.Add("Vendor Invoice No")
            arr.Add("IsJobWork")
            arr.Add("JobWork Location")

            If transportSql.importExcel(Gv1, arr.ToArray()) Then
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
                    Gv1.AutoSizeRows = False
                    Gv1.TableElement.TableHeaderHeight = 30
                End If
            End If
        ElseIf rdbAgainstBulkSale.IsChecked Then
            If transportSql.importExcel(Gv1, "Customer code", "Location Code", "Gate_entry_Date", "Sale_TankerNo.", "Item Code", "GROSS WEIGHT", "TARE WEIGHT", "Sale_MILK QTY.", "SILO No", "Sale_Fat %", "Sale_SNF %", "Sale_Rate", "Sale_Amount", "FAT Qty.", "SNF Qty", "Fat Per Kg", "Snf Per Kg", "Fat Value", "SNF Value", "Rate/100Kg.", "Invoice", "Sale Price Code", "FAT%", "SNF%", "FAT", "SNF", "Sale_CLR") Then
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
            qry = "select '' As [Party Code / Vendor Code],	'' As [Gate_Entry_DATE],	'' As [Location code],	'' As [TankerNo.],	'' As [Item code],	'' As [GROSS WEIGHT],	'' As [DIP Value],	'' As [TARE WEIGHT],	'' As [Net_MILK QTY.] "
            For Each key As String In arrBulkProParameter.Keys
                qry += " ,'' as [" + key + "]"
            Next
            qry += ",'' As [Silo No],'' As [Vendor Invoice No] , '0' as IsJobWork,'' as [JobWork Location]"
            transportSql.ExporttoExcel(qry, Me)
        ElseIf rdbAgainstBulkSale.IsChecked Then
            qry = "select '' As [Customer code], '' As [Location Code], '' As [Gate_entry_Date], '' As [Sale_TankerNo.], '' As [Item Code], '' As [GROSS WEIGHT], '' As [TARE WEIGHT], '' As [Sale_MILK QTY.], '' As [SILO No], '' As [Sale_Fat %], '' As [Sale_SNF %], '' As [Sale_Rate], '' As [Sale_Amount], '' As [FAT Qty.], '' As [SNF Qty], '' As [Fat Per Kg], '' As [Snf Per Kg], '' As [Fat Value], '' As [SNF Value], '' As [Rate/100Kg.], '' As [Invoice], '' As [Sale Price Code], '' As [FAT%], '' As [SNF%], '' As [FAT], '' As [SNF], '' As [Sale_CLR]"
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
            'transportSql.exportdata(Gv1, dirName & "\InvalidBulkMilkPurchaseUploderData.xlsx", "Sheet1")
            transportSql.QuickExportToExcel(Gv1, "", Me.Text)
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            'Process.Start(dirName & "\InvalidBulkMilkPurchaseUploderData.xlsx")

        ElseIf rdbAgainstBulkSale.IsChecked Then
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            'transportSql.exportdata(Gv1, dirName & "\InvalidBulkSaleUploderData.xlsx", "Sheet1")
            transportSql.QuickExportToExcel(Gv1, "", Me.Text)
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            'Process.Start(dirName & "\InvalidBulkSaleUploderData.xlsx")
        Else
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            'transportSql.exportdata(Gv1, dirName & "\InvalidBulkSaleTradeUploderData.xlsx", "Sheet1")
            transportSql.QuickExportToExcel(Gv1, "", Me.Text)
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
            'Process.Start(dirName & "\InvalidBulkSaleTradeUploderData.xlsx")
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
                For Each key As String In arrBulkProParameter.Keys
                    strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells(key).Value)
                    Dim objPM As clsfrmParameterMaster = arrBulkProParameter(key)
                    If objPM.IsMandatory > 0 Then
                        If clsCommon.myLen(strCellValue) <= 0 Then
                            ValidateStatus = ValidateStatus & " [" + key + "] Mandatory Field should have value " & Environment.NewLine
                        End If
                    End If
                    If clsCommon.myLen(strCellValue) > 0 Then
                        If clsCommon.CompairString(objPM.nature, "B") = CompairStringResult.Equal Then
                            If Not (clsCommon.CompairString(strCellValue, "YES") = CompairStringResult.Equal OrElse clsCommon.CompairString(strCellValue, "No") = CompairStringResult.Equal) Then
                                ValidateStatus = ValidateStatus & " [" + key + "], Value Must be either Yes/No" & Environment.NewLine
                            End If
                        ElseIf clsCommon.CompairString(objPM.nature, "A") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)  from tspl_Parameter_value_master where Parameter_CODE='" & objPM.code & "' and Value='" & strCellValue & "'")) <= 0 Then
                                ValidateStatus = ValidateStatus & " [" + key + "], the value specified not found in parameter value master" & Environment.NewLine
                            End If
                        ElseIf clsCommon.CompairString(objPM.nature, "R") = CompairStringResult.Equal Then
                            If Not IsNumeric(strCellValue) Then
                                ValidateStatus = ValidateStatus & " [" + key + "], must be Numeric" & Environment.NewLine
                            End If
                        End If
                    End If
                    If clsCommon.CompairString(objPM.type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objPM.type, "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objPM.type, "CLR") = CompairStringResult.Equal Then
                        strCellValue = clsCommon.myCdbl(strCellValue)
                        If strCellValue <= 0 Then
                            ValidateStatus = ValidateStatus & " [" + key + "],Value Must not be Negative or Zero" & Environment.NewLine
                        End If
                    End If
                Next

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


                'strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SRN Price Chart Code").Value)
                'If clsCommon.myLen(strCellValue) <= 0 Then
                '    ValidateStatus = ValidateStatus & "SRN Price Chart Code Must not be Blank" & Environment.NewLine
                'End If
                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" & strCellValue & "'")) <= 0 Then
                '    ValidateStatus = ValidateStatus & "SRN Price Chart Code not found in master" & Environment.NewLine
                'End If

                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_vendor_price_chart_mapping where isnull(priceCode,'')='" & strCellValue & "' and vendorCode='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & "'")) <= 0 Then
                '    ValidateStatus = ValidateStatus & "Invalid SRN Price Chart Code or Not For Vendor " & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & Environment.NewLine
                'End If

                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Rate").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("Purchase_Rate").Value) Then
                '    ValidateStatus = ValidateStatus & "Purchase_Rate Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "Purchase_Rate Must not be negative or Zero" & Environment.NewLine
                'End If


                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Purchase_Amount").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("Purchase_Amount").Value) Then
                '    ValidateStatus = ValidateStatus & "Purchase_Amount Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "Purchase_Amount Must not be negative or Zero" & Environment.NewLine
                'End If

                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("FAT Qty.").Value) Then
                '    ValidateStatus = ValidateStatus & "FAT Qty. Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "FAT Qty. Must not be negative or Zero" & Environment.NewLine
                'End If

                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("SNF Qty").Value) Then
                '    ValidateStatus = ValidateStatus & "SNF Qty Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "SNF Qty Must not be negative or Zero" & Environment.NewLine
                'End If

                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("Fat Value").Value) Then
                '    ValidateStatus = ValidateStatus & "Fat Value Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "Fat Value Must not be negative or Zero" & Environment.NewLine
                'End If


                'strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                'If Not IsNumeric(Gv1.Rows(i).Cells("SNF Value").Value) Then
                '    ValidateStatus = ValidateStatus & "SNF Value Must be a number" & Environment.NewLine
                'End If
                'If strCellValue <= 0 Then
                '    ValidateStatus = ValidateStatus & "SNF Value Must not be negative or Zero" & Environment.NewLine
                'End If

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

                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    Gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    Gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.White
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                Else
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    Gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.Blue
                    Gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                End If
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

                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Isnull(Type,'') FROM tspl_location_master where Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & "'")).ToUpper(), "PLANT") = CompairStringResult.Equal Then
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
                Else
                    strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SILO No").Value)
                    If clsCommon.myLen(strCellValue) > 0 Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "Silo No not found in master" & Environment.NewLine
                        End If

                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where isnull(Is_Sub_Location,'')='Y' and Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & "' and Location_Code='" & strCellValue & "'")) <= 0 Then
                            ValidateStatus = ValidateStatus & "Invalid Silo No or Not For Location " & clsCommon.myCstr(Gv1.Rows(i).Cells("Location Code").Value) & Environment.NewLine
                        End If
                    End If
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


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Rate/100Kg.").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Rate/100Kg. Value Must not be Negative or Zero" & Environment.NewLine
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


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "FAT% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF%").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF% Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "FAT Value Must not be Negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF Value Must not be Negative or Zero" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_CLR").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Sale_CLR Value Must not be Negative or Zero" & Environment.NewLine
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
                    Dim SettBulkMilkFATSNFKGDecimalPlaces As Decimal = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFKGDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFKGDecimalPlaces, Nothing)
                    Dim SettBulkMilkFATSNFAmtDecimalPlaces As Integer = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFAmtDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFAmtDecimalPlaces, Nothing)
                    Dim settConsiderAllParametersForIncetive As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkMilkConsiderAllParametersForIncetive, clsFixedParameterCode.BulkMilkConsiderAllParametersForIncetive, Nothing)) > 0)
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

                            Dim FATPer As Decimal = 0
                            Dim SNFPer As Decimal = 0
                            For Each key As String In arrBulkProParameter.Keys
                                Dim objPM As clsfrmParameterMaster = arrBulkProParameter(key)
                                If clsCommon.CompairString(objPM.type, "FAT") = CompairStringResult.Equal Then
                                    FATPer = clsCommon.myCstr(Gv1.Rows(i).Cells(key).Value)
                                ElseIf clsCommon.CompairString(objPM.type, "SNF") = CompairStringResult.Equal Then
                                    SNFPer = clsCommon.myCstr(Gv1.Rows(i).Cells(key).Value)
                                End If
                            Next

#Region "Gate Entry"
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Gate Entry Document ")
                            Dim objGateEntry As New clsGateEntry()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objGateEntry.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objGateEntry.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(objGateEntry.Gate_Entry_No) <= 0 Then
                                Throw New Exception("Error in Gate Entry  No genertion")
                            End If
                            objGateEntry.Gate_Entry_Type = "P"
                            objGateEntry.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objGateEntry.Sublocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            objGateEntry.Doc_Type = "BulkProc"
                            objGateEntry.Date_And_Time = clsCommon.GetPrintDate(Gv1.Rows(i).Cells("Gate_Entry_DATE").Value, "dd/MMM/yyyy hh:mm:ss tt")
                            objGateEntry.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            objGateEntry.Location_Desc = clsLocation.GetName(objGateEntry.location_Code, trans)
                            objGateEntry.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            objGateEntry.Vendor_Desc = clsVendorMaster.GetName(objGateEntry.Vendor_Code, trans)
                            objGateEntry.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objGateEntry.Challan_No = clsCommon.myCstr("ND")
                            objGateEntry.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            objGateEntry.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            objGateEntry.Item_Desc = clsItemMaster.GetItemName(objGateEntry.Item_Code, trans)
                            Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(objGateEntry.Item_Code, trans)
                            objGateEntry.UOM = clsCommon.myCstr(DefaultUOm)
                            objGateEntry.Qty_In_Kg = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            objGateEntry.fat_per = FATPer
                            objGateEntry.snf_Per = SNFPer
                            objGateEntry.isPosted = 1
                            objGateEntry.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objGateEntry.isNewEntry = True
                            objGateEntry.Modify_By = objCommonVar.CurrentUserCode
                            objGateEntry.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objGateEntry.Created_By = objCommonVar.CurrentUserCode
                            objGateEntry.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objGateEntry.comp_code = objCommonVar.CurrentCompanyCode
                            objGateEntry.MIKL_TYPE_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MILK_TYPE_CODE from TSPL_MILK_TYPE_MASTER where milk_type='Mix'", trans))


                            objGateEntry.Arr = New List(Of clsGateEntryChemberNoDetails)
                            Dim objGateEntryTR As New clsGateEntryChemberNoDetails()
                            objGateEntryTR.Line_No = 1
                            objGateEntryTR.Chamber_Desc = "FRONT"
                            objGateEntryTR.Item_Code = objGateEntry.Item_Code
                            objGateEntryTR.UOM = objGateEntry.UOM
                            objGateEntryTR.fat_per = objGateEntry.fat_per
                            objGateEntryTR.snf_Per = objGateEntry.snf_Per
                            objGateEntryTR.Chamber_Qty = objGateEntry.Qty_In_Kg
                            objGateEntryTR.DIP_Status = "F"
                            objGateEntryTR.Sample_Lifted = "Y"
                            objGateEntryTR.MIKL_TYPE_CODE = objGateEntry.MIKL_TYPE_CODE
                            objGateEntryTR.Dip_value = ""
                            objGateEntryTR.Seal_No = ""
                            objGateEntry.Arr.Add(objGateEntryTR)
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("Tspl_Gate_Entry_Details", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsGateEntry.saveData(objGateEntry, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim GateEntryNo As String = objGateEntry.Gate_Entry_No
#End Region

#Region "Weighment"
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Weighment Document ")
                            Dim objWeighment As New clsWeighment()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objWeighment.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objWeighment.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(objWeighment.Weighment_No) <= 0 Then
                                Throw New Exception("Error in Weighment No genertion")
                            End If
                            objWeighment.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objWeighment.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            objWeighment.Tare_Weight_date = dt
                            objWeighment.Weighment_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objWeighment.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            objWeighment.Doc_Type = "BulkProc"
                            objWeighment.Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objWeighment.Challan_No = clsCommon.myCstr("ND")
                            objWeighment.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            objWeighment.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objWeighment.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            objWeighment.Location_Desc = clsLocation.GetName(objWeighment.location_Code, trans)
                            objWeighment.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            objWeighment.Vendor_Desc = clsVendorMaster.GetName(objWeighment.Vendor_Code, trans)
                            objWeighment.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            objWeighment.Item_Desc = clsItemMaster.GetItemName(objWeighment.Item_Code, trans)
                            objWeighment.Qty_In_Kg = 0
                            objWeighment.snf_Per = 0
                            objWeighment.fat_per = 0
                            objWeighment.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                            objWeighment.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                            objWeighment.Net_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            objWeighment.UOM = DefaultUOm
                            objWeighment.Dip_Value = clsCommon.myCdbl(Gv1.Rows(i).Cells("DIP Value").Value)
                            objWeighment.Weighment_Slip_No = ""
                            objWeighment.isPosted = 1
                            objWeighment.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objWeighment.isNewEntry = True
                            objWeighment.Modify_By = objCommonVar.CurrentUserCode
                            objWeighment.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objWeighment.Created_By = objCommonVar.CurrentUserCode
                            objWeighment.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objWeighment.comp_code = objCommonVar.CurrentCompanyCode
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Weighment_Detail", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)

                            objWeighment.Arr = New List(Of clsWeighmentChemberNoDetails)

                            Dim objWeighmentTR As New clsWeighmentChemberNoDetails()
                            objWeighmentTR.Line_No = objGateEntryTR.Line_No
                            objWeighmentTR.Chamber_Desc = objGateEntryTR.Chamber_Desc
                            objWeighmentTR.Item_Code = objGateEntryTR.Item_Code
                            objWeighmentTR.UOM = objGateEntryTR.UOM
                            objWeighmentTR.fat_per = objGateEntryTR.fat_per
                            objWeighmentTR.snf_Per = objGateEntryTR.snf_Per
                            objWeighmentTR.Chamber_Qty = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            objWeighmentTR.Gross_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("GROSS WEIGHT").Value)
                            objWeighmentTR.Tare_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("TARE WEIGHT").Value)
                            objWeighmentTR.Net_Weight = clsCommon.myCdbl(Gv1.Rows(i).Cells("Net_MILK QTY.").Value)
                            objWeighmentTR.DIP_Status = objGateEntryTR.DIP_Status
                            objWeighmentTR.Sample_Lifted = objGateEntryTR.Sample_Lifted
                            objWeighmentTR.Weighment_Sequence = 0
                            objWeighmentTR.Vendor_Weight = 0
                            objWeighmentTR.Sublocation_Code = objGateEntry.Sublocation_Code
                            objWeighmentTR.isCanType = 0
                            objWeighment.Arr.Add(objWeighmentTR)

                            clsWeighment.saveData(objWeighment, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim weighmentNo As String = objWeighment.Weighment_No
#End Region

#Region "Quality Check"
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " QC Document ")
                            Dim objQC As New clsQualityCheck()
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objQC.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objQC.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.BulkProc, Gv1.Rows(i).Cells("Location code").Value)
                            End If
                            If clsCommon.myLen(objQC.QC_No) <= 0 Then
                                Throw New Exception("Error in QC No genertion")
                            End If
                            objQC.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objQC.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            objQC.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                            objQC.Doc_Type = "BulkProc"
                            objQC.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objQC.QC_In_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objQC.QC_Out_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objQC.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            objQC.Vendor_Desc = clsVendorMaster.GetName(objQC.Vendor_Code, trans)
                            objQC.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            objQC.Location_Desc = clsLocation.GetName(objQC.location_Code, trans)
                            objQC.Challan_No = clsCommon.myCstr("ND")
                            objQC.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                            objQC.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objQC.Weighment_No = clsCommon.myCstr(weighmentNo)
                            objQC.Weighment_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy")
                            objQC.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            objQC.Item_Desc = clsItemMaster.GetItemName(objQC.Item_Code, trans)
                            objQC.Remarks = clsCommon.myCstr("")
                            objQC.UOM = DefaultUOm
                            objQC.Qty_In_Kg = 0
                            objQC.fat_per = 0
                            objQC.snf_Per = 0
                            objQC.snf_KG = 0
                            objQC.fat_KG = 0
                            objQC.Dip_Value = clsCommon.myCdbl(Gv1.Rows(i).Cells("DIP Value").Value)
                            objQC.Receipt_Control_FAT = 0
                            objQC.Receipt_Control_SNF = 0
                            objQC.DeductionAmount = 0
                            objQC.isPosted = 1
                            objQC.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objQC.isNewEntry = True
                            objQC.Modify_By = objCommonVar.CurrentUserCode
                            objQC.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objQC.Created_By = objCommonVar.CurrentUserCode
                            objQC.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objQC.comp_code = objCommonVar.CurrentCompanyCode
                            objQC.is_Param_accepted = 1


                            Dim objParam As New clsQcParam
                            objQC.arrQcParam = New List(Of clsQcParam)


                            For Each key As String In arrBulkProParameter.Keys
                                Dim objPM As clsfrmParameterMaster = arrBulkProParameter(key)
                                objParam = New clsQcParam
                                objParam.LINE_NO = 1
                                objParam.QC_No = clsCommon.myCstr(objQC.QC_No)
                                objParam.Param_Field_Code = objPM.code
                                objParam.Param_Field_Desc = objPM.desc
                                objParam.Param_Field_Value = clsCommon.myCstr(Gv1.Rows(i).Cells(key).Value)
                                objParam.Param_Type = objPM.type
                                If clsCommon.CompairString(objPM.type, "FAT") = CompairStringResult.Equal Then
                                    objParam.Param_Field_Value = clsCommon.myFormat(objParam.Param_Field_Value)
                                    FATPer = objParam.Param_Field_Value
                                ElseIf clsCommon.CompairString(objPM.type, "SNF") = CompairStringResult.Equal Then
                                    objParam.Param_Field_Value = clsCommon.myFormat(objParam.Param_Field_Value)
                                    SNFPer = objParam.Param_Field_Value
                                End If
                                objQC.arrQcParam.Add(objParam)
                            Next

                            objQC.Arr = New List(Of clsQualityChemberNoDetails)
                            Dim objQCTR As New clsQualityChemberNoDetails()
                            objQCTR.Line_No = objGateEntryTR.Line_No
                            objQCTR.Chamber_Desc = objGateEntryTR.Chamber_Desc
                            objQCTR.Item_Code = objGateEntryTR.Item_Code
                            objQCTR.UOM = objGateEntryTR.UOM
                            objQCTR.fat_per = FATPer
                            objQCTR.snf_Per = SNFPer
                            objQCTR.Chamber_Qty = objGateEntryTR.Chamber_Qty
                            objQCTR.MILK_GRADE_CODE = ""
                            objQCTR.MIKL_TYPE_CODE = objGateEntryTR.MIKL_TYPE_CODE
                            objQCTR.Adjust_fat_per = 0
                            objQCTR.Adjust_snf_Per = 0
                            objQCTR.Adjust_clr = 0
                            objQC.Arr.Add(objQCTR)

                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_QUALITY_CHECK", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)
                            clsQualityCheck.saveData(objQC, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim qc_no As String = objQC.QC_No

#End Region

#Region "Unloading"
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Unloading Document ")
                            Dim objUnloading As New clsUnloading()
                            objUnloading.isNewEntry = True
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objUnloading.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objUnloading.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            If clsCommon.myLen(objUnloading.Unloading_No) <= 0 Then
                                Throw New Exception("Error In Unloading  No Genertion")
                                Exit Sub
                            End If
                            objUnloading.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objUnloading.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                Dim strVirtualLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value) & "' and is_sub_location='Y' and Location_Type='Virtual' and UseInJobWork=1  ", trans))
                                objUnloading.Sub_location_Code = strVirtualLoc
                            Else
                                objUnloading.Sub_location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Silo No").Value)
                            End If
                            objUnloading.Gate_Entry_No = GateEntryNo
                            objUnloading.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objUnloading.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objUnloading.Weighment_No = weighmentNo
                            objUnloading.QC_No = qc_no
                            objUnloading.location_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)

                            objUnloading.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            objUnloading.Item_Desc = clsItemMaster.GetItemName(objUnloading.Item_Code, trans)
                            objUnloading.UOM = DefaultUOm
                            objUnloading.Qty = 0
                            objUnloading.fat_per = 0
                            objUnloading.snf_Per = 0
                            objUnloading.SNF_KG = 0
                            objUnloading.fat_KG = 0
                            objUnloading.isPosted = 1
                            objUnloading.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objUnloading.Modify_By = objCommonVar.CurrentUserCode
                            objUnloading.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objUnloading.comp_code = objCommonVar.CurrentCompanyCode
                            objUnloading.Created_By = objCommonVar.CurrentUserCode
                            objUnloading.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_MILK_UNLOADING", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "location_Code", trans)

                            objUnloading.Arr = New List(Of clsUnloadingChemberNoDetails)
                            Dim objUnloadingTr As New clsUnloadingChemberNoDetails()
                            objUnloadingTr.Line_No = objGateEntryTR.Line_No
                            objUnloadingTr.Chamber_Desc = objGateEntryTR.Chamber_Desc
                            objUnloadingTr.Item_Code = objGateEntryTR.Item_Code
                            objUnloadingTr.UOM = objGateEntryTR.UOM
                            objUnloadingTr.fat_per = FATPer
                            objUnloadingTr.snf_Per = SNFPer
                            objUnloadingTr.Chamber_Qty = objGateEntryTR.Chamber_Qty
                            objUnloadingTr.Unloading_Sequence = 0
                            objUnloadingTr.Sublocation_Code = objGateEntry.Sublocation_Code
                            objUnloadingTr.IsBatchWise = "N"
                            objUnloadingTr.Batch_No = ""
                            objUnloadingTr.UnLoading_Status = 0
                            objUnloading.Arr.Add(objUnloadingTr)

                            clsUnloading.saveData(objUnloading, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode

                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Gate Out Document ")

#End Region

#Region "GateOut"
                            Dim objGateOut As New clsGateOut()
                            objGateOut.isNewEntry = True
                            ''richa agarwal 04/jan/2017
                            ' objGateOut.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut,"", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objGateOut.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objGateOut.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            ''-----------
                            If clsCommon.myLen(objGateOut.Doc_No) <= 0 Then
                                Throw New Exception("Error In GateOut  No Genertion")
                            End If
                            objGateOut.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objGateOut.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            objGateOut.Gate_Entry_No = GateEntryNo
                            objGateOut.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                            objGateOut.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objGateOut.Weighment_No = weighmentNo
                            objGateOut.QC_No = qc_no
                            objGateOut.Modify_By = objCommonVar.CurrentUserCode
                            objGateOut.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objGateOut.comp_code = objCommonVar.CurrentCompanyCode
                            objGateOut.Created_By = objCommonVar.CurrentUserCode
                            objGateOut.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Gate_Out", "Created_By", "", "", trans)
                            clsGateOut.saveData(objGateOut, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
#End Region

#Region "SRN"
                            clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount & " Bulk Milk SRN Document ")
                            Dim objSRNTr As New clsBulkMilkSRNChemberNoDetails()
                            Dim arrSRNPRM As List(Of clsSRNParam) = getBulkMilkSRNPRM(trans, objGateEntry.location_Code, objGateEntry.IsAgainstJobWork, objGateEntry.Sublocation_Code, qc_no, objGateEntryTR.MIKL_TYPE_CODE, dt, settConsiderAllParametersForIncetive)
                            Dim qry As String = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " &
                                                    "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " &
                                                    "left join TSPL_BULK_PRICE_DETAIL_ITEM_WISE on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code " &
                                                    "where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & objGateEntryTR.Item_Code & "' and  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm tt") & "' " &
                                                    "and  expirydate >= '" & clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm tt") & "' and " &
                                                    "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & objGateEntryTR.MIKL_TYPE_CODE & "' and  " &
                                                    "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " &
                                                    "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " &
                                                    "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) & "'  )  order by Price_Date desc,Price_Code desc"
                            objSRNTr.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                            If clsCommon.myLen(objSRNTr.Price_Code) <= 0 Then
                                Throw New Exception("Bulk Price not found for Vendor [" + clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value) + "] and Milk Type [" + objGateEntryTR.MIKL_TYPE_CODE + "]")
                            End If

                            qry = " select  TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_Bulk_Price_MASTER.Fat_Percentage, " &
                                 "TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Tolerance, " &
                                 "tspl_bulk_price_detail_item_wise.Fat_Weightage as DFat_Weightage,tspl_bulk_price_detail_item_wise.Snf_Weightage as DSnf_Weightage,tspl_bulk_price_detail_item_wise.Fat_Percentage as DFat_Percentage, " &
                                 "tspl_bulk_price_detail_item_wise.Snf_Percentage as DSnf_Percentage,tspl_bulk_price_detail_item_wise.Standard_Rate as DStandard_Rate,tspl_bulk_price_detail_item_wise.Tolerance as DTolerance,tspl_bulk_price_detail_item_wise.PriceType,tspl_bulk_price_detail_item_wise.TotalSolidRate,tspl_bulk_price_detail_item_wise.TotalSolidUom from TSPL_Bulk_Price_MASTER left outer join tspl_bulk_price_detail_item_wise on  " &
                                 "TSPL_Bulk_Price_MASTER.Price_Code=tspl_bulk_price_detail_item_wise.Price_Code  where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & objGateEntryTR.Item_Code & "'  and TSPL_Bulk_Price_MASTER.Price_Code='" & objSRNTr.Price_Code & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & objGateEntryTR.MIKL_TYPE_CODE & "'  "
                            Dim dtPM As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                            Dim FatW As Double = clsCommon.myCdbl(dtPM.Rows(0)("DFat_Weightage"))
                            Dim SNfW As Double = clsCommon.myCdbl(dtPM.Rows(0)("DSnf_Weightage"))
                            Dim FATRatio As Double = clsCommon.myCdbl(dtPM.Rows(0)("DFat_Percentage"))
                            Dim SNFRatio As Double = clsCommon.myCdbl(dtPM.Rows(0)("DSnf_Percentage"))



                            objSRNTr.Line_No = objGateEntryTR.Line_No
                            objSRNTr.Chamber_Desc = objGateEntryTR.Chamber_Desc
                            objSRNTr.Item_Code = objGateEntryTR.Item_Code
                            objSRNTr.UOM = objGateEntryTR.UOM
                            objSRNTr.fat_per = FATPer
                            objSRNTr.snf_Per = SNFPer
                            objSRNTr.Gross_Weight = objWeighment.Gross_Weight
                            objSRNTr.Tare_Weight = objWeighment.Tare_Weight
                            objSRNTr.Net_Weight = objWeighment.Net_Weight
                            objSRNTr.Net_Weight_Calculate = objWeighment.Net_Weight
                            objSRNTr.Standardrate = clsCommon.myCdbl(dtPM.Rows(0)("DStandard_Rate"))
                            objSRNTr.BasicRate = objSRNTr.Standardrate
                            'txtTolerance.Value = clsCommon.myCdbl(dtPM.Rows(0)("DTolerance"))
                            objSRNTr.UOM_Calculate = clsCommon.myCstr(dtPM.Rows(0)("Total_Solid_Unit_Code"))
                            objSRNTr.fat_KG = clsCommon.myFormat(MyMath.RoundDown(objSRNTr.Net_Weight * MyMath.RoundDown(FATPer, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            objSRNTr.SNF_KG = clsCommon.myFormat(MyMath.RoundDown(objSRNTr.Net_Weight * MyMath.RoundDown(SNFPer, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)


                            objSRNTr.SpecialDeduction = 0
                            objSRNTr.Deduction = 0
                            objSRNTr.Incentive = 0
                            For ll As Integer = 0 To arrSRNPRM.Count - 1
                                If clsCommon.myLen(arrSRNPRM(ll).Parameter) > 0 Then
                                    If arrSRNPRM(ll).Incen_Deduc > 0 Then
                                        objSRNTr.Incentive = objSRNTr.Incentive + arrSRNPRM(ll).Incen_Deduc
                                    ElseIf arrSRNPRM(ll).Incen_Deduc < 0 Then
                                        objSRNTr.Deduction = objSRNTr.Deduction + arrSRNPRM(ll).Incen_Deduc
                                    End If
                                End If
                            Next


                            objSRNTr.NetRate = clsCommon.myFormat(objSRNTr.Standardrate - objSRNTr.SpecialDeduction + objSRNTr.Deduction + objSRNTr.Incentive)
                            objSRNTr.fat_Rate = MyMath.RoundDown(objSRNTr.NetRate * FatW / FATRatio, 2)
                            objSRNTr.SNF_Rate = MyMath.RoundDown(objSRNTr.NetRate * SNfW / SNFRatio, 2)
                            objSRNTr.FatAmt = MyMath.RoundDown(objSRNTr.fat_KG * objSRNTr.fat_Rate, SettBulkMilkFATSNFAmtDecimalPlaces)
                            objSRNTr.SnfAmt = MyMath.RoundDown(objSRNTr.SNF_KG * objSRNTr.SNF_Rate, SettBulkMilkFATSNFAmtDecimalPlaces)
                            objSRNTr.fat_Qty = MyMath.RoundDown(objSRNTr.fat_KG * FatW / FATRatio, 2)
                            objSRNTr.SNF_Qty = MyMath.RoundDown(objSRNTr.SNF_KG * SNfW / SNFRatio, 2)
                            objSRNTr.TotalStandardQty = MyMath.RoundDown(objSRNTr.fat_Qty + objSRNTr.SNF_Qty, 2)
                            objSRNTr.Deduction_Amt = MyMath.RoundDown(objSRNTr.Deduction * objSRNTr.Net_Weight, 2)
                            objSRNTr.Incentive_Amt = MyMath.RoundDown(objSRNTr.Incentive * objSRNTr.Net_Weight, 2)
                            objSRNTr.Actual_Amount = clsCommon.myFormat(Math.Round(objSRNTr.FatAmt + objSRNTr.SnfAmt, 0))
                            objSRNTr.FinalMilkRate = clsCommon.myFormat(Math.Round(objSRNTr.Actual_Amount / objSRNTr.Net_Weight, 2))
                            objSRNTr.NetRate = clsCommon.myFormat(objSRNTr.Standardrate + objSRNTr.Incentive + objSRNTr.Deduction - objSRNTr.SpecialDeduction)
                            objSRNTr.Amount = 0
                            objSRNTr.MILK_GRADE_CODE = ""
                            objSRNTr.MIKL_TYPE_CODE = objGateEntryTR.MIKL_TYPE_CODE
                            objSRNTr.Net_Weight_LTR = 0
                            objSRNTr.Transport_Charges = 0
                            objSRNTr.Milk_Amount = 0


                            Dim objSRN As New clsBulkMilkSRN()
                            objSRN.isNewEntry = True
                            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                                objSRN.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN, clsDocTransactionType.BulkProcJobWorkOutward, strJobLoc)
                            Else
                                objSRN.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN, clsDocTransactionType.NA, clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            End If
                            If clsCommon.myLen(objSRN.SRN_NO) <= 0 Then
                                Throw New Exception("Error In SRN  No Genertion")
                            End If
                            objSRN.PO_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPO, "", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value))
                            If clsCommon.myLen(objSRN.PO_NO) <= 0 Then
                                Throw New Exception("Error In Auto PO  No Genertion")
                            End If
                            objSRN.IsAgainstJobWork = clsCommon.myCstr(Gv1.Rows(i).Cells("IsJobWork").Value)
                            objSRN.Joblocation_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("JobWork Location").Value)
                            objSRN.PO_Date = dt
                            objSRN.isApproved = 1
                            objSRN.SRN_Date = dt
                            objSRN.Gate_Entry_No = GateEntryNo
                            objSRN.Weighment_No = weighmentNo
                            objSRN.Weighment_Date = dt
                            objSRN.Vendor_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Party Code / Vendor Code").Value)
                            objSRN.Loc_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value)
                            objSRN.Challan_No = "ND"
                            objSRN.Challan_Date = dt
                            objSRN.Tanker_No = clsCommon.myCstr(Gv1.Rows(i).Cells("TankerNo.").Value)
                            objSRN.QC_No = qc_no
                            objSRN.Qc_Date = dt
                            objSRN.isPosted = 0
                            objSRN.Item_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                            objSRN.Item_Desc = clsItemMaster.GetItemName(objSRN.Item_Code, trans)
                            objSRN.UOM = DefaultUOm
                            objSRN.Gross_Weight = objSRNTr.Gross_Weight
                            objSRN.Tare_Weight = objSRNTr.Tare_Weight
                            objSRN.Net_Weight = objSRNTr.Net_Weight
                            objSRN.fat_per = objSRNTr.fat_per
                            objSRN.snf_Per = objSRNTr.snf_Per
                            objSRN.fat_KG = objSRNTr.fat_KG
                            objSRN.SNF_KG = objSRNTr.SNF_KG
                            objSRN.FatAmt = objSRNTr.FatAmt
                            objSRN.SnfAmt = objSRNTr.SnfAmt
                            objSRN.fat_Rate = objSRNTr.fat_Rate
                            objSRN.SNF_Rate = objSRNTr.SNF_Rate
                            objSRN.Amount = objSRNTr.Amount
                            objSRN.SpecialDeduction = objSRNTr.SpecialDeduction
                            objSRN.Deduction = objSRNTr.Deduction
                            objSRN.Incentive = objSRNTr.Incentive
                            objSRN.Actual_Amount = objSRNTr.Actual_Amount
                            objSRN.BasicRate = objSRNTr.BasicRate
                            objSRN.Standardrate = objSRNTr.Standardrate
                            objSRN.NetRate = objSRNTr.NetRate
                            objSRN.FinalMilkRate = objSRNTr.FinalMilkRate
                            objSRN.Modify_By = objCommonVar.CurrentUserCode
                            objSRN.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objSRN.comp_code = objCommonVar.CurrentCompanyCode
                            objSRN.Created_By = objCommonVar.CurrentUserCode
                            objSRN.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                            objSRN.Arr = New List(Of clsBulkMilkSRNChemberNoDetails)
                            objSRN.Arr.Add(objSRNTr)
                            objSRN.arrObj = arrSRNPRM
                            objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Bulk_MILK_SRN", "Created_By", clsCommon.myCstr(Gv1.Rows(i).Cells("Location code").Value), "Loc_Code", trans)
                            clsBulkMilkSRN.saveData(objSRN, trans)
                            clsBulkMilkSRN.postData(objSRN.SRN_NO, clsUserMgtCode.frmBulkMilkSRN, trans)
                            objCommonVar.CurrentUserCode = CurrentUserCode
                            Dim SrnNo As String = objSRN.SRN_NO
                            Gv1.Rows(i).Cells(colDispatchCode).Value = SrnNo
#End Region

                        End If
                    Next

#Region "Purchase Invoice"
                    ''richa agarwal 10 Jan,2018
                    If False AndAlso clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSkipfunctionalityafterSRNOnBulkProcurement, clsFixedParameterCode.AllowtoSkipfunctionalityafterSRNOnBulkProcurement, trans)), "0") = CompairStringResult.Equal Then
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

#End Region

                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsCommon.ProgressBarPercentHide()
                    'Throw New Exception("Balwinder Singh Premi")
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
                            ' strGate_entry_Date = 'clsCommon.myCDate(Gv1.Rows(i).Cells("Gate_entry_Date").Value)
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
                            DblClr = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_CLR").Value)

                            strPriceCode = clsCommon.myCstr(Gv1.Rows(i).Cells("Sale Price Code").Value)

                            dblFatKg = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT Qty.").Value)
                            DblSNFKG = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Qty").Value)
                            DblStandardrate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Sale_Rate").Value)
                            DblFatRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Per Kg").Value)
                            DblSNFRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Snf Per Kg").Value)
                            DblFatAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat Value").Value)
                            DblSNFAmount = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF Value").Value)
                            DblNetMilkRate = clsCommon.myCdbl(Gv1.Rows(i).Cells("Rate/100Kg.").Value)


                            DblFATweightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT%").Value)
                            DblSNFWeightage = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF%").Value)
                            DblFATRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("FAT").Value)
                            DblSNFRatio = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF").Value)


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
                            objQCEntry.Unit_code = "KG"
                            objQCEntry.Qty = 0
                            objQCEntry.Fat = DblFatPer
                            objQCEntry.CLR = DblClr
                            objQCEntry.SNF = DblSnfPer
                            objQCEntry.Remarks = ""
                            objQCEntry.Customer_Code = strCustomerCode


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
                            objDispatchDetailEntry.Unit_code = "KG"
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
                    CreateAutoInvoiceAgainstMultipleDispatch(trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsDBFuncationality.ExecuteNonQuery("update TSPL_INVOICE_MASTER_BULKSALE set Created_Date=document_date ,Modified_Date=document_date,Posting_Date=document_date where IsUploader=1 ", trans)
                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    'trans.Rollback()
                    clsCommon.MyMessageBoxShow("Saved Successfully")
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
        Finally
            objCommonVar.CurrentUserCode = CurrentUserCode
        End Try
    End Sub

    Shared Function getBulkMilkSRNPRM(ByVal trans As SqlTransaction, ByVal strLoc As String, ByVal isJobWork As Integer, ByVal SubLocation As String, ByVal strQCNo As String, ByVal strMilkType As String, ByVal TransDate As DateTime, ByVal settConsiderAllParametersForIncetive As Boolean) As List(Of clsSRNParam)
        Dim arrSRNPRM As New List(Of clsSRNParam)
        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(strLoc, trans) Then
            whrCls = " and (Param_for='MCC' or Param_for='BOTH')"
        Else
            whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
        End If
        Dim strlocation As String = ""
        If isJobWork = 1 Then
            strlocation = SubLocation
        Else
            strlocation = strLoc
        End If
        Dim paramName As String = String.Empty
        Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
        Dim qry2 As String = String.Empty
        Dim qry3 As String = String.Empty
        Dim paramValue As Double = 0
        Dim intRow As Integer = 1

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "' and line_No='1'  and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3, trans))
                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "'  and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'   and MIKL_TYPE_CODE='" & strMilkType & "' " &
    " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & strMilkType & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "
                If i <> dt1.Rows.Count - 1 Then
                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                End If
            Next

            qry2 = " select * from ( " & qry2 & " ) yyy"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                For j As Integer = 0 To dt2.Rows.Count - 1
                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))
                    Dim objParam As New clsSRNParam
                    objParam.Line_No = intRow
                    objParam.Parameter = dt2.Rows(j)("Code")
                    objParam.Lower_Range = dt2.Rows(j)("Lower_Range")
                    objParam.Upper_Range = dt2.Rows(j)("Upper_Range")
                    'objParam.value =
                    objParam.QCValue = dt2.Rows(j)("QCValue")
                    objParam.Incen_Deduc = dt2.Rows(j)("Value")
                    arrSRNPRM.Add(objParam)
                    intRow += 1
                    'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                Next
            End If
        End If

        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
        qry2 = ""
        Dim paramValue1 As String = ""
        dt1 = clsDBFuncationality.GetDataTable(qry1, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'  and line_No='" & intRow & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3, trans))
                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' then 2 else 3 end) end)  and  loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & strMilkType & "' " &
  " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & strMilkType & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

                If i <> dt1.Rows.Count - 1 Then
                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                End If
            Next

            qry2 = " select * from ( " & qry2 & " ) yyy"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                For j As Integer = 0 To dt2.Rows.Count - 1
                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))
                    Dim objParam As New clsSRNParam
                    objParam.Line_No = intRow
                    objParam.Parameter = dt2.Rows(j)("Code")
                    'objParam.Lower_Range = dt2.Rows(j)("Lower_Range")
                    'objParam.Upper_Range = dt2.Rows(j)("Upper_Range")
                    objParam.value = dt2.Rows(j)("Condition_value")
                    objParam.QCValue = dt2.Rows(j)("QCValue")
                    objParam.Incen_Deduc = dt2.Rows(j)("Value")
                    arrSRNPRM.Add(objParam)
                    intRow += 1

                    'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                Next
            End If
        End If


        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
        qry2 = ""
        paramValue1 = ""
        dt1 = clsDBFuncationality.GetDataTable(qry1, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'  and line_No='" & intRow & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3, trans))
                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' and loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & strMilkType & "'" &
     " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & strMilkType & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

                If i <> dt1.Rows.Count - 1 Then
                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                End If
            Next

            qry2 = " select * from ( " & qry2 & " ) yyy"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                For j As Integer = 0 To dt2.Rows.Count - 1
                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))

                    Dim objParam As New clsSRNParam
                    objParam.Line_No = intRow
                    objParam.Parameter = dt2.Rows(j)("Code")
                    'objParam.Lower_Range = dt2.Rows(j)("Lower_Range")
                    'objParam.Upper_Range = dt2.Rows(j)("Upper_Range")
                    objParam.value = dt2.Rows(j)("status")
                    objParam.QCValue = dt2.Rows(j)("QCValue")
                    objParam.Incen_Deduc = dt2.Rows(j)("Value")
                    arrSRNPRM.Add(objParam)
                    intRow += 1
                    'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                Next
            End If
        End If
        If settConsiderAllParametersForIncetive Then
            Dim qry As String = "   select TSPL_PARAMETER_RANGE_MASTER.Code  as Code from TSPL_PARAMETER_RANGE_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.loc_code='" & strlocation & "'  and TSPL_PARAMETER_RANGE_MASTER.MIKL_TYPE_CODE='" & strMilkType & "'  and TSPL_PARAMETER_RANGE_MASTER.effective_date<='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "'  and 2=(case when TSPL_PARAMETER_RANGE_MASTER.End_Date is null then 2 else (case when TSPL_PARAMETER_RANGE_MASTER.End_Date>='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' then 2 else 3 end) end) group by TSPL_PARAMETER_RANGE_MASTER.Code"
            Dim dtMandatoryParameter As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtMandatoryParameter IsNot Nothing AndAlso dtMandatoryParameter.Rows.Count > 0 Then
                For Each dr As DataRow In dtMandatoryParameter.Rows
                    Dim flag As Boolean = False
                    For ii As Integer = 0 To arrSRNPRM.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dr("Code")), arrSRNPRM(ii).Parameter) = CompairStringResult.Equal Then
                            flag = True
                            Exit For
                        End If
                    Next
                    If Not flag Then
                        arrSRNPRM = New List(Of clsSRNParam)
                        Exit For
                    End If
                Next
            End If
            Dim MinValue As Decimal = 0
            If arrSRNPRM.Count > 0 Then
                MinValue = clsCommon.myCdbl(arrSRNPRM(0).Incen_Deduc)
                For ii As Integer = 0 To arrSRNPRM.Count - 1
                    If MinValue > clsCommon.myCdbl(arrSRNPRM(ii).Incen_Deduc) Then
                        MinValue = clsCommon.myCdbl(arrSRNPRM(ii).Incen_Deduc)
                    End If
                Next
            End If
            If MinValue <> 0 Then
                Dim dclTotal As Decimal
                Dim dclRatio As Decimal = Math.Round(clsCommon.myCDivide(MinValue, arrSRNPRM.Count), 2)
                For ii As Integer = 0 To arrSRNPRM.Count - 1
                    If ii = arrSRNPRM.Count - 1 Then
                        arrSRNPRM(ii).Incen_Deduc = MinValue - dclTotal
                    Else
                        arrSRNPRM(ii).Incen_Deduc = dclRatio
                        dclTotal += dclRatio
                    End If
                Next
            End If
        End If
        Return arrSRNPRM
    End Function
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
                            dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Sale Price Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Customer code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Location Code").Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchCode).Value) + "", "" + clsCommon.myCstr(grow.Cells("Gate_entry_Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "KG", "" + clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_MILK QTY.").Value) + "", " " + clsCommon.myCstr(grow.Cells("Sale_Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Rate/100Kg.").Value) + "", "" + clsCommon.myCstr(grow.Cells("Sale_Amount").Value) + "", "" + clsCommon.myCstr(grow.Cells("FAT Qty.").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF Qty").Value) + "")
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