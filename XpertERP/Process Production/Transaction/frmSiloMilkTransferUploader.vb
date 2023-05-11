''created By Richa Agarwal 29 Aug,2019 GKD/02/09/19-000184
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmSiloMilkTransferUploader
    Inherits FrmMainTranScreen
    Public Const colSlno As String = "colSlno"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colDispatchCode As String = "colDispatchCode"
    Public Const colSNRNO As String = "colSNRNO"
    Public Const colTaxGroup As String = "colTaxGroup"
    Public Const coltax1 As String = "coltax1"
    Public Const coltax1rate As String = "coltax1rate"
    Public Const coltax1Amt As String = "coltax1Amt"
    Public Const coltax2 As String = "coltax2"
    Public Const coltax2rate As String = "coltax2rate"
    Public Const coltax2Amt As String = "coltax2Amt"


    Public TextCol As GridViewTextBoxColumn = Nothing
    Public DecCol As GridViewDecimalColumn = Nothing
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing
    Dim arrVendorInvoiceNo As List(Of String) = Nothing
    Dim AllowSiloMilkTransfertoMainLocation As Boolean = False

    Private Sub frmSiloMilkTransferUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowSiloMilkTransfertoMainLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSiloMilkTransfertoMainLocation, clsFixedParameterCode.AllowSiloMilkTransfertoMainLocation, Nothing)) = 1, True, False))
        Gv1.Visible = True
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        'Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = Nothing
        If rdbAgainstBulkSale.IsChecked Then
            If transportSql.importExcel(Gv1, "Date", "Main Location", "Main location Silo", "Header Item code", "Header UOM", "from Silo", "Item code", "UOM", "Qty", "Fat %", "Fat KG", "SNF %", "Snf kg") Then
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
                    TextCol.Name = colTaxGroup
                    TextCol.HeaderText = "Tax group"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(4, TextCol)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = coltax1
                    TextCol.HeaderText = "Tax1"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(5, TextCol)

                    DecCol = New GridViewDecimalColumn()
                    DecCol.Name = coltax1rate
                    DecCol.HeaderText = "Tax1Rate"
                    DecCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(6, DecCol)

                    DecCol = New GridViewDecimalColumn()
                    DecCol.Name = coltax1Amt
                    DecCol.HeaderText = "Tax1Amt"
                    DecCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(7, DecCol)


                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = coltax2
                    TextCol.HeaderText = "Tax2"
                    TextCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(8, TextCol)

                    DecCol = New GridViewDecimalColumn()
                    DecCol.Name = coltax2rate
                    DecCol.HeaderText = "Tax2Rate"
                    DecCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(9, DecCol)

                    DecCol = New GridViewDecimalColumn()
                    DecCol.Name = coltax2Amt
                    DecCol.HeaderText = "Tax2Amt"
                    DecCol.ReadOnly = True
                    Gv1.MasterTemplate.Columns.Insert(10, DecCol)

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
        If rdbAgainstBulkSale.IsChecked Then
            qry = "select '' as [Date],'' as [Main Location],'' as [Main location Silo],'' as [Header Item code],'' as [Header UOM],'' as [from Silo],'' as [Item code],'' as [UOM],'' as [Qty],'' as [Fat %],'' as [Fat KG],'' as [SNF %],'' as [Snf kg]"
            transportSql.ExporttoExcel(qry, Me)
        End If
        qry = Nothing
    End Sub

    Private Sub btnExportInvalid_Click(sender As Object, e As EventArgs) Handles btnExportInvalid.Click
        If rdbAgainstBulkSale.IsChecked Then
            Gv1.Columns(colIsValidated).FilterDescriptor = New FilterDescriptor("ProductName", FilterOperator.IsEqualTo, False)
            Dim dirName As String = "c:\ERPTempFolder"

            If Not System.IO.Directory.Exists(dirName) Then
                System.IO.Directory.CreateDirectory(dirName)
            End If
            transportSql.QuickExportToExcel(Gv1, "", Me.Text)
            Gv1.Columns(colIsValidated).FilterDescriptor = Nothing
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
        If rdbAgainstBulkSale.IsChecked Then
            For i As Integer = 0 To Gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Main Location Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "' and ((Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) )")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Main Location not found in master according to Silo Milk Transfer criteria" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Main location Silo").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Main location Silo Must not be Blank" & Environment.NewLine
                End If
                ''richa BHA/10/09/19-000927
                Dim qry As String = String.Empty
                If AllowSiloMilkTransfertoMainLocation = True Then
                    qry = "select count (*) from (select Location_Code as [Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "' " & Environment.NewLine & _
                    "  union all " & Environment.NewLine & _
                    " select Location_Code as [Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where(Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) and Location_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "')z where z.Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main location Silo").Value) & "' "

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateStatus = ValidateStatus & "Main location Silo not found in master according to Silo Milk Transfer criteria" & Environment.NewLine
                    End If
                Else

                    qry = "select count (*) from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "' and Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main location Silo").Value) & "' "

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateStatus = ValidateStatus & "Main location Silo not found in master according to Silo Milk Transfer criteria" & Environment.NewLine
                    End If
                End If

             


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Date").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Date Time Must not be Blank" & Environment.NewLine
                End If
                If IsDate(strCellValue) Then
                Else
                    ValidateStatus = ValidateStatus & "Date Time Must  be a Date Time Value" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Header Item code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Header Item code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & strCellValue & "' and  TSPL_ITEM_MASTER.Active=1  and  Product_Type ='MI' ")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Header Item code not found in master according to Silo Milk Transfer criteria." & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Header UOM").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Header UOM Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Header Item code").Value) & "' and  UOM_Code ='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Header UOM not found in master." & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("from Silo").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "From Silo Must not be Blank" & Environment.NewLine
                End If
                ''richa BHA/10/09/19-000927
                If AllowSiloMilkTransfertoMainLocation = True Then
                    qry = "select count (*) from (select Location_Code as [Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "' " & Environment.NewLine & _
               "  union all " & Environment.NewLine & _
               " select Location_Code as [Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where(Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) and Location_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "')z where Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("from Silo").Value) & "'"

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateStatus = ValidateStatus & "From Silo not found in master according to Silo Milk Transfer criteria" & Environment.NewLine
                    End If
                Else
                    qry = "select count (*) from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code and Seg.Seg_No='7'  where (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Main Location").Value) & "' and Location_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("from Silo").Value) & "' "
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateStatus = ValidateStatus & "From Silo not found in master according to Silo Milk Transfer criteria" & Environment.NewLine
                    End If
                End If
               

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Item code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & strCellValue & "' and  TSPL_ITEM_MASTER.Active=1  and  Product_Type ='MI' ")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item code not found in master according to Silo Milk Transfer criteria." & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("QTY").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "QTY Must not be Zero or Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat %").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Fat %").Value) Then
                    ValidateStatus = ValidateStatus & "Fat % Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat % Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat KG").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Fat KG").Value) Then
                    ValidateStatus = ValidateStatus & "Fat KG Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Fat KG Must not be negative or Zero" & Environment.NewLine
                End If

                Dim dblFatKG As Double = Math.Round(clsCommon.myCdbl(Gv1.Rows(i).Cells("QTY").Value) * clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat %").Value) / 100, 2)

                If dblFatKG <> Math.Round(clsCommon.myCdbl(Gv1.Rows(i).Cells("Fat KG").Value), 2) Then
                    ValidateStatus = ValidateStatus & "Fat KG is not correct" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF %").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("SNF %").Value) Then
                    ValidateStatus = ValidateStatus & "SNF % Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "SNF % Must not be negative or Zero" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("Snf kg").Value)
                If Not IsNumeric(Gv1.Rows(i).Cells("Snf kg").Value) Then
                    ValidateStatus = ValidateStatus & "Snf kg Must be a number" & Environment.NewLine
                End If
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "Snf kg Must not be negative or Zero" & Environment.NewLine
                End If

                Dim dblSNFKG As Double = Math.Round(clsCommon.myCdbl(Gv1.Rows(i).Cells("QTY").Value) * clsCommon.myCdbl(Gv1.Rows(i).Cells("SNF %").Value) / 100, 2)

                If dblSNFKG <> Math.Round(clsCommon.myCdbl(Gv1.Rows(i).Cells("Snf KG").Value), 2) Then
                    ValidateStatus = ValidateStatus & "SNF KG is not correct" & Environment.NewLine
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
    Public Shared Function GetRateMccSale(ByVal mccCode As String, ByVal Itemcode As String, ByVal Unitcode As String, ByVal Effctv_date As Date, ByVal trans As SqlTransaction)
        Dim tranDate As String = clsCommon.GetPrintDate(Effctv_date, "dd/MMM/yyyy")
        Dim Rate As Double = 0
        Dim qry As String = "select top 1 Price from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
              & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
              & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code where Item_Code='" & Itemcode & "' " _
              & " and TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM='" & Unitcode & "' and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
        Rate = clsDBFuncationality.getSingleValue(qry, trans)
        If Rate <= 0 Then
            qry = "select top 1 TSPL_ITEM_UOM_DETAIL.Item_Code,Price,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_MCC_RATE_UPLOADER_master inner join TSPL_MCC_RATE_UPLOADER_MCC on " _
             & " TSPL_MCC_RATE_UPLOADER_MCC.MCC_Code='" & mccCode & "' and   TSPL_MCC_RATE_UPLOADER_master.Code=TSPL_MCC_RATE_UPLOADER_MCC.Code " _
             & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code inner join TSPL_ITEM_UOM_DETAIL on " _
             & " TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MCC_RATE_UPLOADER_Detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM where TSPL_MCC_RATE_UPLOADER_Detail.Item_Code='" & Itemcode & "' " _
             & " and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If Dt.Rows.Count > 0 Then
                Dim Conv_Fac As Double = clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Itemcode & "'  and Uom_Code='" & Unitcode & "' ", trans)
                Rate = Conv_Fac * clsCommon.myCdbl(Dt.Rows(0)("Price")) / IIf(clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")) > 0, clsCommon.myCdbl(Dt.Rows(0)("Conversion_Factor")), 1)
                Return Rate
            Else
                Return Rate
            End If
        End If
        Return Rate
    End Function
    Sub SaveAndPost()
        arrVendorInvoiceNo = New List(Of String)
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim trans As SqlTransaction = Nothing
        Try
            If rdbAgainstBulkSale.IsChecked Then
                If ValidatedCount > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    trans = clsDBFuncationality.GetTransactin()


                    CreateAutoInvoiceAgainstMultipleDispatch(trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    'trans.Rollback()
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
        Dim Main_location_Silo As String = String.Empty
        Dim Header_Item_code As String = String.Empty
        Dim Main_Location As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            Dim dt1 As DataTable = Nothing
            dt1 = clsDBFuncationality.GetDataTable("select '' as [Date],'' as [Main_Location],'' as [Main_location_Silo],'' as [Header_Item_code],'' as [Header UOM],'' as [from Silo],'' as [Item code],'' as [UOM],'' as [Qty],'' as [Fat %],'' as [Fat KG],'' as [SNF %],'' as [Snf kg]", trans)
            dt1.Rows.RemoveAt(0)
            If ValidatedCount > 0 Then
                If rdbAgainstBulkSale.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("Main Location").Value) + "", "" + clsCommon.myCstr(grow.Cells("Main location Silo").Value) + "", "" + clsCommon.myCstr(grow.Cells("Header Item code").Value) + "", "" + clsCommon.myCstr(grow.Cells("Header UOM").Value) + "", "" + clsCommon.myCstr(grow.Cells("from Silo").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item code").Value) + "", " " + clsCommon.myCstr(grow.Cells("UOM").Value) + "", "" + clsCommon.myCstr(grow.Cells("Qty").Value) + "", "" + clsCommon.myCstr(grow.Cells("Fat %").Value) + "", "" + clsCommon.myCstr(grow.Cells("Fat KG").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF %").Value) + "", "" + clsCommon.myCstr(grow.Cells("SNF kg").Value) + "")
                        End If
                    Next
                End If
            End If

            Dim dtout As DataTable = Nothing
            dt1.DefaultView.Sort = "Main_Location,Main_location_Silo,Header_Item_code,Date"
            dtout = dt1.DefaultView.ToTable()

            dtmain = clsDBFuncationality.GetDataTable("Select '' as SrNo,'' as [Date],'' as [Main_Location],'' as [Main_location_Silo],'' as [Header_Item_code],'' as [Header UOM],'' as [from Silo],'' as [Item code],'' as [UOM],'' as [Qty],'' as [Fat %],'' as [Fat KG],'' as [SNF %],'' as [Snf kg]", trans)
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("Date"))) = CompairStringResult.Equal And clsCommon.CompairString(Main_Location, clsCommon.myCstr(dr("Main_Location"))) = CompairStringResult.Equal And clsCommon.CompairString(Header_Item_code, clsCommon.myCstr(dr("Header_Item_code"))) = CompairStringResult.Equal And clsCommon.CompairString(Main_location_Silo, clsCommon.myCstr(dr("Main_location_Silo"))) = CompairStringResult.Equal Then
                        'InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("Amount"))
                    Else
                        CustomerCount = CustomerCount + 1
                        'InvoiceAmount = 0
                        'InvoiceAmount = clsCommon.myCdbl(dr("Amount"))
                    End If
                    Header_Item_code = clsCommon.myCstr(dr("Header_Item_code"))
                    Main_location_Silo = clsCommon.myCstr(dr("Main_location_Silo"))
                    Main_Location = clsCommon.myCstr(dr("Main_Location"))
                    strdocdate = clsCommon.myCDate(dr("Date"))

                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("Date")) + "", "" + clsCommon.myCstr(dr("Main_Location")) + "", "" + clsCommon.myCstr(dr("Main_location_Silo")) + "", "" + clsCommon.myCstr(dr("Header_Item_code")) + "", "" + clsCommon.myCstr(dr("Header UOM")) + "", "" + clsCommon.myCstr(dr("from Silo")) + "", "" + clsCommon.myCstr(dr("Item code")) + "", "" + clsCommon.myCstr(dr("UOM")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", " " + clsCommon.myCstr(dr("Fat %")) + "", "" + clsCommon.myCstr(dr("Fat KG")) + "", "" + clsCommon.myCstr(dr("SNF %")) + "", "" + clsCommon.myCstr(dr("SNF KG")) + "")
                Next
              
                InvoiceSaveData(trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Function AllowToSave(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strcountno As String = ""
            Dim intCounter As Integer = 0
            Dim dblEnteredQty As Double = 0
            Dim dblBalQty As Double = 0
            For ii As Integer = 0 To dtmain.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dtmain.Rows(ii)("Item_code"))
                Dim strLocation As String = clsCommon.myCstr(dtmain.Rows(ii)("Location"))
                Dim dblQty As Double = clsCommon.myCdbl(dtmain.Rows(ii)("QTY"))
                Dim strUOM As String = clsCommon.myCstr(dtmain.Rows(ii)("UOM"))
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dtmain.Rows(ii)("SrNo"))


                If clsCommon.CompairString(strcountno, clsCommon.myCdbl(dtmain.Rows(ii)("SrNo"))) <> CompairStringResult.Equal Then
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)
                    dblBalQty = clsItemLocationDetails.getBalance(strICode, strLocation, "", clsCommon.myCstr(dtmain.Rows(ii)("MCCSaleDate")), trans, strUOM, 0)
                    dblEnteredQty = dblQty

                    'For jj As Integer = 0 To dtmain.Rows.Count - 1
                    '    If ii = jj Then
                    '        Continue For
                    '    End If
                    '    Dim strICodeInner As String = clsCommon.myCstr(dtmain.Rows(jj)("Item_code"))
                    '    Dim strUOMInner As String = clsCommon.myCstr(dtmain.Rows(jj)("UOM"))
                    '    Dim dblQtyInner As Double = clsCommon.myCdbl(dtmain.Rows(jj)("QTY"))
                    '    Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)

                    '    'If clsCommon.CompairString(strICode, strICodeInner) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strUOMInner) = CompairStringResult.Equal Then
                    '    '    common.clsCommon.MyMessageBoxShow("Item Code " + strICode + " and Unit " + strUOM + " is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                    '    '    Return False
                    '    'End If

                    '    If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOMInner, strUOM) = CompairStringResult.Equal Then
                    '        dblEnteredQty += dblQtyInner
                    '    End If
                    'Next
                    'dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    'If dblEnteredQty > dblBalQty Then ' AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                    '    Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    '    'Return False
                    'End If
                Else
                    Dim strICodeInner As String = clsCommon.myCstr(dtmain.Rows(ii)("Item_code"))
                    Dim strUOMInner As String = clsCommon.myCstr(dtmain.Rows(ii)("UOM"))
                    Dim dblQtyInner As Double = clsCommon.myCdbl(dtmain.Rows(ii)("QTY"))
                    Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, trans)
                    If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOMInner, strUOM) = CompairStringResult.Equal Then
                        dblEnteredQty += dblQtyInner
                    End If
                End If
                'If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                '    For jj As Integer = ii + 1 To Gv1.Rows.Count - 1
                '        Dim strInICode As String = clsCommon.myCstr(Gv1.Rows(jj).Cells(colICode).Value)
                '        Dim strInUOM As String = clsCommon.myCstr(Gv1.Rows(jj).Cells(colUnit).Value)


                '        If clsCommon.CompairString(strICode, strInICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInUOM) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(jj).Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal Then
                '            common.clsCommon.MyMessageBoxShow("Item Code " + strICode + " and Unit " + strUOM + " is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                '            Return False
                '        End If
                '    Next
                'End If

                dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                If dblEnteredQty > dblBalQty Then
                    Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    'Return False
                End If

                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("SrNo"))
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub InvoiceSaveData(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As ClsSiloMilkTransferDetails = Nothing
        Dim obj As ClsSiloMilkTransfer = Nothing
        Try


            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            For Each dr As DataRow In dtmain.Rows
                j += 1
                clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating Silo Milk Transfer Records " & j & " of Total " & dtmain.Rows.Count)
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("SrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("SrNo"))) <> CompairStringResult.Equal Then

                    obj = New ClsSiloMilkTransfer()

                    ' obj.Document_Code = txtAdjustmentNo.Value
                    obj.Document_Date = clsCommon.myCstr(dr("Date"))
                    obj.Silo_Code = clsCommon.myCstr(dr("Main_location_Silo"))
                    obj.Description = clsCommon.myCstr(dr("Date"))
                    obj.MainLocation_Code = clsCommon.myCstr(dr("Main_Location"))
                    obj.Item_Code = clsCommon.myCstr(dr("Header_Item_code"))
                    obj.Item_UOM = clsCommon.myCstr(dr("Header UOM"))
                    obj.Description = "Entry created through Silo Milk Transfer uploader"
                    obj.IsJobWork = 0
                    obj.IsCreatedByUploader = 1
                    ''for detail table
                    obj.Arr = New List(Of ClsSiloMilkTransferDetails)
                    objTr = New ClsSiloMilkTransferDetails()


                    objTr.Line_No = 1
                    objTr.Silo_Code = clsCommon.myCstr(dr("from Silo"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    objTr.fat_pers = clsCommon.myCdbl(dr("Fat %"))
                    objTr.fat_kg = clsCommon.myCdbl(dr("Fat KG"))
                    objTr.snf_kg = clsCommon.myCdbl(dr("SNF kg"))
                    objTr.snf_pers = clsCommon.myCdbl(dr("SNF %"))

                    obj.Arr.Add(objTr)
                Else
                    objTr = New ClsSiloMilkTransferDetails()

                    objTr.Line_No = j
                     objTr.Silo_Code = clsCommon.myCstr(dr("from Silo"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item Code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    objTr.fat_pers = clsCommon.myCdbl(dr("Fat %"))
                    objTr.fat_kg = clsCommon.myCdbl(dr("Fat KG"))
                    objTr.snf_kg = clsCommon.myCdbl(dr("SNF kg"))
                    objTr.snf_pers = clsCommon.myCdbl(dr("SNF %"))
                    obj.Arr.Add(objTr)
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("SrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    obj.SaveData(obj, True, "", trans)
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

End Class