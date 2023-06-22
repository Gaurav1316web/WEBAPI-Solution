''created By Richa Agarwal 
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmDairyBookingUploader
    Inherits FrmMainTranScreen
    Dim isCDExist As Boolean = False
    Dim isCashExist As Boolean = False
    Dim isCRExist As Boolean = False
    Dim isSOExist As Boolean = False
    Dim isGatePassCashExist As Boolean = False
    Dim AllowZeroQtyOnDairyBookingUploader As Boolean = False
    Public Const colSlno As String = "colSlno"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colDispatchCode As String = "colDispatchCode"
    Public Const colSNRNO As String = "colSNRNO"
    Const colSellingRate As String = "colSellingRate"
    Const colAmt As String = "colAmt"
    Public colCrateQty As String = "colCrateQty"
    Const colPaymentMode As String = "colPaymentMode"
    Const colCardSaleCode As String = "colCardSaleCode"
    Const colAmountWithTax As String = "colAmountWithTax"
    Const colItemBasicPrice As String = "colItemBasicPrice"
    Const colOrgRate As String = "colOrgRate"
    Const colRate As String = "colRate"
    Const colPriceCode As String = "colPriceCode"
    Const colZeroQtyForAllItem As String = "colZeroQtyForAllItem"
    Private CheckNoOfDaysforCardSaleBooking As Double = 0
    Public TextCol As GridViewTextBoxColumn = Nothing
    Public DecCol As GridViewDecimalColumn = Nothing
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing
    Dim arrVendorInvoiceNo As List(Of String) = Nothing
    Dim AllowToCreateNoOfBookingPerDay As Integer = 0
    Dim ShowBookingTypeDropDownonDairyBookingCustomer As Boolean = False


    Dim colTSLine As String = "colTSLine"
    Dim colTSDetail As String = "colTSDetail"


    Dim colTSISNo As String = "colTSISNo"
    Dim colTSIPSNo As String = "colTSIPSNo"
    Dim colTSIZone As String = "colTSIZone"
    Dim colTSIRoute As String = "colTSIRoute"
    Dim colTSIBoothID As String = "colTSIBoothID"
    Dim colTSIType As String = "colTSIType"
    Dim colTSICR As String = "colTSICR"
    Dim colTSICD As String = "colTSICD"
    Dim colTSISO As String = "colTSISO"
    Dim colTSICash As String = "colTSICash"
    Dim colTSIBookingCountCash As String = "colTSIBookingCountCash"
    Dim colTSIBookingCountCD As String = "colTSIBookingCountCD"
    Dim colTSIBookingCountCR As String = "colTSIBookingCountCR"
    Dim colTSIBookingCountSO As String = "colTSIBookingCountSO"


    Dim colGPSNo As String = "colGPSNo"
    Dim colGPFile As String = "colGPFile"
    Dim colGPFileSNo As String = "colGPFileSNo"
    Dim colGPDetail As String = "colTSDetail"



    Dim colGPISNo As String = "colGPISNo"
    Dim colGPIPSNo As String = "colGPIPSNo"
    Dim colGPIFile As String = "colGPIFile"
    Dim colGPIDate As String = "colGPIDate"
    Dim colGPIShift As String = "colGPIShift"
    Dim colGPIRoute As String = "colGPIRoute"
    Dim colGPIBoothID As String = "colGPIBoothID"
    Dim colGPIType As String = "colGPIType"
    Dim colGPICash As String = "colGPICash"
    Dim colGPIBookingCountCash As String = "colGPIBookingCountCash"


    Private Sub frmMCCMaterialSaleUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As New Dictionary(Of String, String)
        coll.Add("Name", "VARCHAR(200)")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_Noetpad_Read_Except", coll)


        RadPageView1.SelectedPage = RadPageViewPage1
        'RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        CheckNoOfDaysforCardSaleBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckNoOfDaysforCardSaleBooking, clsFixedParameterCode.CheckNoOfDaysforCardSaleBooking, Nothing))
        AllowZeroQtyOnDairyBookingUploader = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowZeroQtyOnDairyBookingUploader, clsFixedParameterCode.AllowZeroQtyOnDairyBookingUploader, Nothing)) = 1, True, False)
        AllowToCreateNoOfBookingPerDay = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToCreateNoOfBookingPerDay, clsFixedParameterCode.AllowToCreateNoOfBookingPerDay, Nothing))
        ShowBookingTypeDropDownonDairyBookingCustomer = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowBookingTypeDropDownonDairyBookingCustomer, clsFixedParameterCode.ShowBookingTypeDropDownonDairyBookingCustomer, Nothing)) = 1, True, False)

        Gv1.Visible = True
        rdbAgainstCashIndent.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtLocation.Value + "' "))
        End If

    End Sub
    Function CrateQuantity(ByVal ItemCode As String, ByVal UOM As String, ByVal Qty As Double, ByVal trans As SqlTransaction)
        Dim dblTotalCrateRowWise As Double = 0
        Dim TotalCrate As Double = 0
        If clsCommon.myLen(clsCommon.myCstr(ItemCode)) > 0 Then
            Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(ItemCode) & "'", trans))
            If ItemCrateType = 1 Then
                Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(ItemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(UOM) & "'", trans))
                Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(ItemCode) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(ItemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(UOM) & "' ", trans))

                If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                    Dim DispatchQty As Double = Qty * ItemConvFactor
                    If DispatchQty >= CrateConvFactor Then
                        dblTotalCrateRowWise = Math.Floor(DispatchQty / CrateConvFactor)
                    Else
                        dblTotalCrateRowWise = 0
                    End If
                Else
                    ' clsCommon.MyMessageBoxShow("Please fill conversion factor for this unit at line no." & i + 1 & "")
                End If
            End If
        End If
        Return dblTotalCrateRowWise
    End Function
    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        If rdbAgainstTruckSheet.IsChecked = False AndAlso rdbAgainstGatePass.IsChecked = False Then
            Gv1.Columns.Clear()
            Gv1.DataSource = Nothing
            If rdbAgainstCashIndent.IsChecked Then
                'transportSql.importExcel(Gv1, "Sequence", "Date", "Location Code", "Booking Type", "Customer code", "Item code", "UOM", "QTY", "Sampling")
                transportSql.importExcelWithoutReadColumnName(Gv1, "")
            Else
                ''transportSql.importExcel(Gv1, "Sequence", "Date", "Location Code", "Booking Type", "Customer code", "Item code", "UOM", "QTY", "Sampling", "Payment Mode")
                transportSql.importExcelWithoutReadColumnName(Gv1, "")
            End If
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

                'TextCol = New GridViewTextBoxColumn()
                'TextCol.Name = colDispatchCode
                'TextCol.HeaderText = "BookingCode"
                'TextCol.ReadOnly = True
                'TextCol.IsVisible = False
                'Gv1.MasterTemplate.Columns.Insert(3, TextCol)

                TextCol = New GridViewTextBoxColumn()
                TextCol.Name = colPriceCode
                TextCol.HeaderText = "PriceCode"
                TextCol.ReadOnly = True
                TextCol.IsVisible = False
                Gv1.MasterTemplate.Columns.Insert(4, TextCol)

                TextCol = New GridViewTextBoxColumn()
                TextCol.Name = colZeroQtyForAllItem
                TextCol.HeaderText = "ZeroQtyForAllItem"
                TextCol.ReadOnly = True
                TextCol.IsVisible = False
                Gv1.MasterTemplate.Columns.Insert(5, TextCol)


                'If rdbAgainstCardIndent.IsChecked = True Then

                '    TextCol = New GridViewTextBoxColumn()
                '    TextCol.Name = colCardSaleCode
                '    TextCol.HeaderText = "CardSaleCode"
                '    TextCol.ReadOnly = True
                '    TextCol.IsVisible = False
                '    Gv1.MasterTemplate.Columns.Insert(5, TextCol)


                'End If
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

    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        If rdbAgainstCashIndent.IsChecked = True OrElse rdbAgainstCardIndent.IsChecked = True Then
            CheckAndValidate()
        ElseIf rdbAgainstTruckSheet.IsChecked = True Then
            If gvTSItem.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("There is no row in grid please select a truck sheet to import")
            End If
        Else
            If gvGPItem.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("There is no row in grid please select a Gate Pass sheet to import")
            End If
        End If

    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim qry As String = String.Empty
        'If rdbAgainstCashIndent.IsChecked Then
        '    qry = "select '' as Sequence, '' as Date, '' as [Location Code], '' as [Booking Type], '' as [Customer code], '' as [Item code], '' as UOM, '' as QTY, '' as Sampling"
        '    transportSql.ExporttoExcel(qry, Me)
        'Else
        '    qry = "select '' as Sequence, '' as Date, '' as [Location Code], '' as [Booking Type], '' as [Customer code], '' as [Item code], '' as UOM, '' as QTY, '' as Sampling,'' as [Payment Mode]"
        '    transportSql.ExporttoExcel(qry, Me)
        'End If
        If rdbAgainstCashIndent.IsChecked Then
            qry = "select '' as BoothId,'' as ROUTE,'' as supplyType"
            transportSql.ExporttoExcel(qry, Me)
        ElseIf rdbAgainstCardIndent.IsChecked Then
            qry = "select '' as BoothId,'' as ROUTE,'' as supplyType,'' as [Payment Mode]"
            transportSql.ExporttoExcel(qry, Me)
        End If

        qry = Nothing
    End Sub

    Private Sub btnExportInvalid_Click(sender As Object, e As EventArgs) Handles btnExportInvalid.Click
        If rdbAgainstCashIndent.IsChecked Or rdbAgainstCardIndent.IsChecked Then
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
        If rdbAgainstTruckSheet.IsChecked = True Then
            'If isCDExist = True Then
            '    If clsCommon.myLen(clsCommon.myCstr(txtCardSaleCode.Value)) <= 0 Then
            '        clsCommon.MyMessageBoxShow("Please select Card Sale Code")
            '        Exit Sub
            '    End If
            '    If clsCommon.myLen(clsCommon.myCstr(txtPaymentMode.Value)) <= 0 Then
            '        clsCommon.MyMessageBoxShow("Please select Payment Mode")
            '        Exit Sub
            '    End If
            'End If

            If gvTSItem.Rows.Count > 0 Then
                SaveAndPostAgainstTruckSheet()
            Else
                clsCommon.MyMessageBoxShow("No Rows found to save")
            End If
        ElseIf rdbAgainstGatePass.IsChecked = True Then
            If gvGPItem.Rows.Count > 0 Then
                SaveAndPostAgainstGatePass()
            Else
                clsCommon.MyMessageBoxShow("No Rows found to save")
            End If
        Else
            SaveAndPost()
        End If

    End Sub
    Sub CheckAndValidate()
        Dim ValidateStatus As String = String.Empty

        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("There is no row in grid please select a sheet to import")
        End If
        If ValidatedCount = Gv1.Rows.Count Then
            clsCommon.MyMessageBoxShow("All Rows are already validated")
            Exit Sub
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location")
            Exit Sub
        End If
        If rdbAgainstCardIndent.IsChecked = True Then
            If clsCommon.myLen(clsCommon.myCstr(txtCardSaleCode.Value)) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Create Card Sale No")
                Exit Sub
            End If
        End If
        ValidatedCount = 0
        Dim custCounter As Integer = 0
        Dim strCellValue
        If rdbAgainstCashIndent.IsChecked Or rdbAgainstCardIndent.IsChecked = True Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                custCounter = 1
                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer No not found in master" & Environment.NewLine
                End If

                Dim qry As String = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
            "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
            "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & strCellValue & "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    Gv1.Rows(i).Cells(colPriceCode).Value = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
                Else
                    ValidateStatus = ValidateStatus & "price_CodeNon not found " & Environment.NewLine
                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("SupplyType").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Supply Type Must not be Blank" & Environment.NewLine
                End If
                If rdbAgainstCashIndent.IsChecked = True Then
                    If clsCommon.CompairString(strCellValue, "FESTIVE OFFER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "SO") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "CR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "CASH") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "FN") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "UP") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strCellValue, "PS") <> CompairStringResult.Equal Then
                        ValidateStatus = ValidateStatus & "Booking Type Must be CASH/CR/SO/FESTIVE OFFER/FN/UP/PS" & Environment.NewLine
                    End If
                Else
                    If clsCommon.CompairString(strCellValue, "CD") <> CompairStringResult.Equal Then
                        ValidateStatus = ValidateStatus & "Booking Type Must be CD" & Environment.NewLine
                    End If
                End If

                If rdbAgainstCashIndent.IsChecked = True Then
                    If AllowToCreateNoOfBookingPerDay > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value) & "' ")), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value) & "' ")), "") <> CompairStringResult.Equal Then

                            Dim STRSQL As String = "select count(distinct TSPL_BOOKING_MATSER.Document_No) as cc from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)"
                            If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                                STRSQL += " And TSPL_CUSTOMER_MASTER.customer_category<>'Others' and TSPL_BOOKING_MATSER.Booking_Type<>'CD' "
                            End If
                            Dim TempBookingExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(STRSQL))
                            If TempBookingExist >= AllowToCreateNoOfBookingPerDay Then
                                ValidateStatus = ValidateStatus & "Booking already exist for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "] and Booth Id " & clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value) & ""
                            End If

                            Dim strCustomerCode As String = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)

                            If clsCommon.myLen(strCustomerCode) > 0 Then
                                For jj As Integer = i + 1 To Gv1.Rows.Count - 1
                                    Dim strInCustomerCode As String = clsCommon.myCstr(Gv1.Rows(jj).Cells("BoothId").Value)
                                    If clsCommon.CompairString(strCustomerCode, strInCustomerCode) = CompairStringResult.Equal Then
                                        custCounter += 1
                                        If custCounter >= AllowToCreateNoOfBookingPerDay Then
                                            ValidateStatus = ValidateStatus & "Booking of same Booth id not allowed for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "] and Booth Id " & clsCommon.myCstr(Gv1.Rows(jj).Cells("BoothId").Value) & ""
                                        End If
                                    End If
                                Next
                            End If
                        End If

                    End If
                End If

                'If rdbAgainstCardIndent.IsChecked = True Then
                '    Dim strCardSaleCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Card_no from Tspl_Card_Sale where status=1 and CONVERT(date,Tspl_Card_Sale.FROM_DATE,103)>='" & clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Value).AddDays(CheckNoOfDaysforCardSaleBooking), "dd/MMM/yyyy") & "' and CONVERT(date,Tspl_Card_Sale.TO_Date,103)>='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'   order by convert(datetime,Tspl_Card_Sale.FROM_DATE,103),card_no"))
                '    If clsCommon.myLen(strCardSaleCode) <= 0 Then
                '        ValidateStatus = ValidateStatus & "Please Create Card Sale No." & Environment.NewLine
                '    End If
                '    Gv1.Rows(i).Cells(colCardSaleCode).Value = strCardSaleCode
                'End If
                Dim dblTotalQty As Double = 0
                Dim counterstart As Integer = 0
                'If rdbAgainstCashIndent.IsChecked = True Then
                '    counterstart = 7
                'Else
                '    counterstart = 8
                'End If

                If rdbAgainstCashIndent.IsChecked = True Then
                    counterstart = 8
                Else
                    counterstart = 9
                End If

                For k As Integer = counterstart To Gv1.Columns.Count - 1
                    strCellValue = clsCommon.myCstr(Gv1.Columns(k).Name)
                    If clsCommon.myLen(strCellValue) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item Desc Must not be Blank" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Short_Description='" & strCellValue & "'  and  isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item code not found for item desc '" & strCellValue & "' in master according to sale criteria." & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                    dblTotalQty = dblTotalQty + clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                    If strCellValue < 0 Then
                        ValidateStatus = ValidateStatus & "QTY Must not be Negative" & Environment.NewLine
                    End If

                Next

                If AllowZeroQtyOnDairyBookingUploader = True Then
                    If dblTotalQty <= 0 Then
                        Gv1.Rows(i).Cells(colZeroQtyForAllItem).Value = 1
                    Else
                        Gv1.Rows(i).Cells(colZeroQtyForAllItem).Value = 0
                    End If
                Else
                    Gv1.Rows(i).Cells(colZeroQtyForAllItem).Value = 0
                    If dblTotalQty <= 0 Then
                        ValidateStatus = ValidateStatus & "Plz enter qty in any column of item" & Environment.NewLine
                    End If
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

            If ValidatedCount > 0 Then
                clsCommon.ProgressBarPercentShow()
                trans = clsDBFuncationality.GetTransactin()

                objCommonVar.CurrentUserCode = CurrentUserCode
                BookingSaveData(trans)

                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                Gv1.Columns.Clear()
            Else
                Throw New Exception("No Validated Rows found to save")
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
    Private Sub CreateAutoBookingAgainstMultipleCardIndent(ByVal trans As SqlTransaction)
        Dim LocationCode As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Dim SalePriceCode As String = String.Empty
        Dim ItemCode As String = String.Empty
        Dim UnitCode As String = String.Empty
        Dim Sampling As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            If ValidatedCount > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from Temp_table_Dairy_Booking_uploader", trans)
                If rdbAgainstCardIndent.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            clsDBFuncationality.ExecuteNonQuery("Insert into Temp_table_Dairy_Booking_uploader values ('" + clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MMM/yyyy") + "', '" + clsCommon.myCstr(grow.Cells("LOCATION Code").Value) + "', '" + clsCommon.myCstr(grow.Cells("CUSTOMER Code").Value) + "',  '" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "', " + clsCommon.myCstr(grow.Cells("QTY").Value) + ", '" + clsCommon.myCstr(grow.Cells("UOM").Value) + "', '" + clsCommon.myCstr(grow.Cells("Sampling").Value) + "','" + clsCommon.myCstr(grow.Cells("Booking Type").Value) + "','" + clsCommon.myCstr(grow.Cells(colPriceCode).Value) + "', " + clsCommon.myCstr(grow.Cells(colSellingRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colOrgRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colItemBasicPrice).Value) + ", " + clsCommon.myCstr(grow.Cells(colAmountWithTax).Value) + ", " + clsCommon.myCstr(grow.Cells(colAmt).Value) + ",  '" + clsCommon.myCstr(grow.Cells("Payment Mode").Value) + "','" + clsCommon.myCstr(grow.Cells(colCardSaleCode).Value) + "'," + clsCommon.myCstr(grow.Cells(colCrateQty).Value) + ")", trans)
                        End If
                    Next
                End If
            End If

            Dim dtout As DataTable = Nothing

            dtout = clsDBFuncationality.GetDataTable("sELECT Location,Customer,BookingType,BookingDate,Item_code,UOM,IsSampling,SUM(Qty) AS Qty,max(PriceCode) as PriceCode,max(SellingRate) as SellingRate,max(OrgRate) as OrgRate,max(Rate) as Rate,max(ItemBasicPrice) as ItemBasicPrice,max(AmountWithTax) as AmountWithTax,max(Amount) as Amount,max(PaymentMode) as PaymentMode,max(CardSaleCode) as CardSaleCode,sum(CrateQty) as CrateQty from Temp_table_Dairy_Booking_uploader GROUP BY Location,Customer,BookingType,BookingDate,Item_code,UOM,IsSampling", trans)

            dtmain = clsDBFuncationality.GetDataTable("Select '' as SrNo,'' as BookingDate,'' as Location,'' as Customer,'' as IsSampling,'' as Item_code,'' as Qty,'' as UOM,'' as BookingType,'' as PriceCode,'' as SellingRate,'' as OrgRate,'' as Rate,'' as ItemBasicPrice,'' as AmountWithTax,'' as Amount,'' as PaymentMode,'' as CardSaleCode,'' as CrateQty", trans)
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("BookingDate"))) = CompairStringResult.Equal And clsCommon.CompairString(SalePriceCode, clsCommon.myCstr(dr("BookingType"))) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Customer"))) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(dr("Location"))) = CompairStringResult.Equal And clsCommon.CompairString(Sampling, clsCommon.myCstr(dr("IsSampling"))) = CompairStringResult.Equal Then
                        ' InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("Amount"))
                    Else
                        CustomerCount = CustomerCount + 1
                        InvoiceAmount = 0
                        'InvoiceAmount = clsCommon.myCdbl(dr("Amount"))
                    End If
                    CustomerCode = clsCommon.myCstr(dr("Customer"))
                    LocationCode = clsCommon.myCstr(dr("Location"))
                    SalePriceCode = clsCommon.myCstr(dr("BookingType"))
                    strdocdate = clsCommon.myCDate(dr("BookingDate"))
                    Sampling = clsCommon.myCstr(dr("IsSampling"))

                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("BookingDate")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("IsSampling")) + "", "" + clsCommon.myCstr(dr("Item_code")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", "" + clsCommon.myCstr(dr("UOM")) + "", "" + clsCommon.myCstr(dr("BookingType")) + "", "" + clsCommon.myCstr(dr("PriceCode")) + "", "" + clsCommon.myCstr(dr("SellingRate")) + "", "" + clsCommon.myCstr(dr("OrgRate")) + "", "" + clsCommon.myCstr(dr("Rate")) + "", "" + clsCommon.myCstr(dr("ItemBasicPrice")) + "", "" + clsCommon.myCstr(dr("AmountWithTax")) + "", "" + clsCommon.myCstr(dr("Amount")) + "", "" + clsCommon.myCstr(dr("PaymentMode")) + "", "" + clsCommon.myCstr(dr("CardSaleCode")) + "", "" + clsCommon.myCstr(dr("CrateQty")) + "")
                Next

                BookingSaveData(trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub CreateAutoBookingAgainstMultipleCashIndent(ByVal trans As SqlTransaction)
        Dim LocationCode As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Dim SalePriceCode As String = String.Empty
        Dim ItemCode As String = String.Empty
        Dim UnitCode As String = String.Empty
        Dim Sampling As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            If ValidatedCount > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from Temp_table_Dairy_Booking_uploader", trans)
                If rdbAgainstCashIndent.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            clsDBFuncationality.ExecuteNonQuery("Insert into Temp_table_Dairy_Booking_uploader values ('" + clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MMM/yyyy") + "', '" + clsCommon.myCstr(grow.Cells("LOCATION Code").Value) + "', '" + clsCommon.myCstr(grow.Cells("CUSTOMER Code").Value) + "',  '" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "', " + clsCommon.myCstr(grow.Cells("QTY").Value) + ", '" + clsCommon.myCstr(grow.Cells("UOM").Value) + "', '" + clsCommon.myCstr(grow.Cells("Sampling").Value) + "','" + clsCommon.myCstr(grow.Cells("Booking Type").Value) + "','" + clsCommon.myCstr(grow.Cells(colPriceCode).Value) + "', " + clsCommon.myCstr(grow.Cells(colSellingRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colOrgRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colRate).Value) + ", " + clsCommon.myCstr(grow.Cells(colItemBasicPrice).Value) + ", " + clsCommon.myCstr(grow.Cells(colAmountWithTax).Value) + ", " + clsCommon.myCstr(grow.Cells(colAmt).Value) + ", NULL,NULL," + clsCommon.myCstr(grow.Cells(colCrateQty).Value) + ")", trans)
                        End If
                    Next
                End If
            End If

            Dim dtout As DataTable = Nothing

            dtout = clsDBFuncationality.GetDataTable("sELECT Location,Customer,BookingType,BookingDate,Item_code,UOM,IsSampling,SUM(Qty) AS Qty,max(PriceCode) as PriceCode,max(SellingRate) as SellingRate,max(OrgRate) as OrgRate,max(Rate) as Rate,max(ItemBasicPrice) as ItemBasicPrice,max(AmountWithTax) as AmountWithTax,max(Amount) as Amount,sum(CrateQty) as CrateQty from Temp_table_Dairy_Booking_uploader GROUP BY Location,Customer,BookingType,BookingDate,Item_code,UOM,IsSampling", trans)

            dtmain = clsDBFuncationality.GetDataTable("Select '' as SrNo,'' as BookingDate,'' as Location,'' as Customer,'' as IsSampling,'' as Item_code,'' as Qty,'' as UOM,'' as BookingType,'' as PriceCode,'' as SellingRate,'' as OrgRate,'' as Rate,'' as ItemBasicPrice,'' as AmountWithTax,'' as Amount,'' as CrateQty", trans)
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("BookingDate"))) = CompairStringResult.Equal And clsCommon.CompairString(SalePriceCode, clsCommon.myCstr(dr("BookingType"))) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Customer"))) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(dr("Location"))) = CompairStringResult.Equal And clsCommon.CompairString(Sampling, clsCommon.myCstr(dr("IsSampling"))) = CompairStringResult.Equal Then
                        ' InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("Amount"))
                    Else
                        CustomerCount = CustomerCount + 1
                        InvoiceAmount = 0
                        'InvoiceAmount = clsCommon.myCdbl(dr("Amount"))
                    End If
                    CustomerCode = clsCommon.myCstr(dr("Customer"))
                    LocationCode = clsCommon.myCstr(dr("Location"))
                    SalePriceCode = clsCommon.myCstr(dr("BookingType"))
                    strdocdate = clsCommon.myCDate(dr("BookingDate"))
                    Sampling = clsCommon.myCstr(dr("IsSampling"))

                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("BookingDate")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("IsSampling")) + "", "" + clsCommon.myCstr(dr("Item_code")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", "" + clsCommon.myCstr(dr("UOM")) + "", "" + clsCommon.myCstr(dr("BookingType")) + "", "" + clsCommon.myCstr(dr("PriceCode")) + "", "" + clsCommon.myCstr(dr("SellingRate")) + "", "" + clsCommon.myCstr(dr("OrgRate")) + "", "" + clsCommon.myCstr(dr("Rate")) + "", "" + clsCommon.myCstr(dr("ItemBasicPrice")) + "", "" + clsCommon.myCstr(dr("AmountWithTax")) + "", "" + clsCommon.myCstr(dr("Amount")) + "", "" + clsCommon.myCstr(dr("CrateQty")) + "")
                Next

                BookingSaveData(trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    'Sub BookingSaveData(ByVal trans As SqlTransaction)

    '    Dim strcountno As String = ""
    '    Dim objTr As clsBookingDetailDairySale = Nothing
    '    Dim obj As clsBookingEntryDairySale = Nothing
    '    Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
    '    Dim LineNo As Integer = 1
    '    Try


    '        Dim DocuAmount As Double = 0
    '        Dim Tax1Amt As Double = 0
    '        Dim Tax2Amt As Double = 0
    '        Dim TaxBaseAmount As Double = 0
    '        Dim totalQty As Double = 0
    '        Dim intCounter As Integer = 0
    '        Dim j As Integer = 0
    '        Dim TotalCrate As Double = 0
    '        For i As Integer = 0 To Gv1.Rows.Count - 1
    '            j += 1
    '            clsCommon.ProgressBarPercentUpdate(j * 100 / Gv1.Rows.Count, " Creating  Booking Records " & j & " of Total " & Gv1.Rows.Count)
    '            Dim intCurrInvNo As Integer = clsCommon.myCdbl(Gv1.Rows(i).Cells(colSlno).Value)

    '            If clsCommon.CompairString(strcountno, clsCommon.myCstr(Gv1.Rows(i).Cells(colSlno).Value)) <> CompairStringResult.Equal Then
    '                LineNo = 1
    '                DocuAmount = 0
    '                Tax1Amt = 0
    '                Tax2Amt = 0
    '                TaxBaseAmount = 0
    '                TotalCrate = 0
    '                obj = New clsBookingEntryDairySale()

    '                ' obj.IsSampling = IIf(clsCommon.myCstr(dr("isSampling")) = "Y", 1, 0)
    '                If rdbAgainstCardIndent.IsChecked Then
    '                    obj.Document_Date = clsCommon.myCDate(LblFromDate.Text)
    '                Else
    '                    obj.Document_Date = clsCommon.myCDate(txtDate.Value)
    '                End If

    '                obj.location_code = clsCommon.myCstr(txtLocation.Value)
    '                obj.AgainstGatePass = 0
    '                obj.Is_Taxable = 2
    '                obj.TRANSACTION_TYPE = ""
    '                If rdbAgainstCashIndent.IsChecked = True Then
    '                    obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
    '                Else
    '                    obj.From_Screen_code = clsUserMgtCode.frmbookingdairyFreshSale
    '                    obj.Card_SALE_No = clsCommon.myCstr(txtCardSaleCode.Value)
    '                    If clsCommon.myLen(obj.Card_SALE_No) > 0 Then
    '                        obj.CardSale_FROM_DATE = clsCommon.myCstr(LblFromDate.Text)
    '                        obj.CardSale_TO_DATE = clsCommon.myCstr(lblToDate.Text)
    '                    End If
    '                End If

    '                obj.Booking_Type = clsCommon.myCstr(Gv1.Rows(i).Cells("SupplyType").Value)
    '                obj.BookingThrough = "Uploader"
    '                obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
    '                ''for detail table
    '                obj.Arr = New List(Of clsBookingDetailDairySale)
    '                Dim counterstart As Integer = 0
    '                If rdbAgainstCashIndent.IsChecked = True Then
    '                    counterstart = 7
    '                Else
    '                    counterstart = 8
    '                End If

    '                For k As Integer = counterstart To Gv1.Columns.Count - 1
    '                    If clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value) > 0 Then
    '                        objTr = New clsBookingDetailDairySale()
    '                        objTr.Line_No = LineNo
    '                        objTr.Booking_Qty = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
    '                        objTr.Cust_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)
    '                        objTr.Sampling = 0
    '                        objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
    '                        objTr.Short_Description = clsCommon.myCstr(Gv1.Columns(k).Name)
    '                        'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

    '                        Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
    '                        Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
    '            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
    '            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
    '            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
    '            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

    '                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
    '                            For Each dr As DataRow In dt1.Rows
    '                                objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
    '                                objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
    '                                objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
    '                                objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

    '                            Next
    '                        End If


    '                        'objTr.Unit_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "'  AND Default_UOM =1 ", trans))

    '                        qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
    '           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
    '           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

    '                        dt1 = clsDBFuncationality.GetDataTable(qry, trans)
    '                        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
    '                            objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
    '                            objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
    '                        End If

    '                        Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
    '                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
    '                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
    '                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
    '                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
    '                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
    '                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


    '                        Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
    '                        If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
    '                            Dim objD As clsSchemeApplyOnDairy = Nothing
    '                            objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

    '                            If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
    '                                For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
    '                                    objTr.SchemeType = objtrScheme.schm_Type
    '                                    objTr.Scheme_Qty = objtrScheme.Schm_Qty
    '                                Next

    '                            End If
    '                        End If


    '                        'crate conversion
    '                        ''Gv1.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

    '                        Dim dblTotalCrateRowWise As Double = 0
    '                        If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
    '                            Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
    '                            If ItemCrateType = 1 Then
    '                                Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
    '                                Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
    '                                Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

    '                                If CrateConvFactor > 0 And ItemConvFactor > 0 Then
    '                                    Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
    '                                    If DispatchQty >= CrateConvFactor Then
    '                                        TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

    '                                    End If
    '                                Else
    '                                    Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
    '                                End If
    '                            End If
    '                        End If

    '                        ''end of crate Conversion



    '                        Dim dt As New DataTable()
    '                        Dim dblRate As Double = 0
    '                        Dim dblTotal As Double = 0
    '                        Dim dblItemBasicPrice As Double = 0

    '                        qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
    '            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
    '            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
    '            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
    '            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
    '            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
    '            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt  from ( " &
    '            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
    '            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
    '            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
    '            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
    '            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
    '            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
    '            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
    '            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
    '            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
    '            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
    '            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Gv1.Rows(i).Cells(colPriceCode).Value & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
    '            ") XXXE WHERE RowNo=1  "
    '                        dt = clsDBFuncationality.GetDataTable(qry, trans)
    '                        If dt.Rows.Count > 0 Then
    '                            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
    '                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
    '                                dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
    '                            Else
    '                                dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
    '                            End If

    '                            If dblRate = 0 Then
    '                                Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
    '                            End If

    '                            objTr.Item_Rate = clsCommon.myCdbl(dblRate)
    '                            objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
    '                            objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
    '                            objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
    '                            objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
    '                            objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


    '                            objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
    '                            objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
    '                            objTr.Booking_Status = 1

    '                        Else
    '                            Throw New Exception("Please create Price chart for customer " & Gv1.Rows(i).Cells("BoothId").Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
    '                        End If

    '                        'objTr.Tax_NonTax = clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "' ", trans)
    '                        'objTr.FreshAmbient = clsDBFuncationality.getSingleValue("select case when Is_Ambient=1 then 'PS' WHEN Is_FreshItem=1 THEN 'FS' ELSE '' END from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "' ", trans)

    '                        DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
    '                        totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
    '                        obj.Arr.Add(objTr)

    '                        LineNo = LineNo + 1
    '                    End If
    '                Next
    '            End If
    '            LineNo = 0
    '            strcountno = intCurrInvNo
    '            Dim intNextInvNo As Integer = -1

    '            If intCounter + 1 < Gv1.Rows.Count Then
    '                intNextInvNo = clsCommon.myCdbl(Gv1.Rows(intCounter + 1).Cells(colSlno).Value)
    '            End If

    '            If Not (intCurrInvNo = intNextInvNo) Then
    '                ''for detail table Booking Payment Mode
    '                If rdbAgainstCardIndent.IsChecked = True Then
    '                    Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
    '                    obj.AdvanceAmount = DocuAmount * dblCardSaleDays

    '                    obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
    '                    objTrPM = New clsBookingDetailDairySalePaymentMode()
    '                    objTrPM.SNo = 1
    '                    objTrPM.Payment_Mode = clsCommon.myCstr(Gv1.Rows(i).Cells("Payment Mode").Value)
    '                    objTrPM.Amount = obj.AdvanceAmount
    '                    obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)

    '                End If


    '                obj.TotalCrate = TotalCrate
    '                obj.SaveData(obj, True, trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)
    '                If rdbAgainstCardIndent.IsChecked Then
    '                    clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
    '                Else
    '                    clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
    '                End If

    '            End If
    '            intCounter += 1

    '        Next

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        strcountno = Nothing
    '        obj = Nothing
    '        objTr = Nothing
    '    End Try

    'End Sub

    Sub BookingSaveData(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim LineNo As Integer = 1
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colIsValidated).Value = True Then
                    j += 1
                    clsCommon.ProgressBarPercentUpdate(j * 100 / Gv1.Rows.Count, " Creating  Booking Records " & j & " of Total " & Gv1.Rows.Count)
                    Dim intCurrInvNo As Integer = clsCommon.myCdbl(Gv1.Rows(i).Cells(colSlno).Value)

                    If clsCommon.CompairString(strcountno, clsCommon.myCstr(Gv1.Rows(i).Cells(colSlno).Value)) <> CompairStringResult.Equal Then
                        LineNo = 1
                        DocuAmount = 0
                        Tax1Amt = 0
                        Tax2Amt = 0
                        TaxBaseAmount = 0
                        TotalCrate = 0
                        obj = New clsBookingEntryDairySale()

                        ' obj.IsSampling = IIf(clsCommon.myCstr(dr("isSampling")) = "Y", 1, 0)
                        If rdbAgainstCardIndent.IsChecked Then
                            obj.Document_Date = clsCommon.myCDate(LblFromDate.Text)
                        Else
                            '                    obj.Document_Date = clsCommon.myCDate(txtDate.Value)
                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)
                        End If

                        obj.location_code = clsCommon.myCstr(txtLocation.Value)

                        obj.Is_Taxable = 2
                        obj.TRANSACTION_TYPE = ""
                        If rdbAgainstCashIndent.IsChecked = True Then
                            obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
                        Else
                            obj.From_Screen_code = ""
                            obj.Card_SALE_No = clsCommon.myCstr(txtCardSaleCode.Value)
                            If clsCommon.myLen(obj.Card_SALE_No) > 0 Then
                                obj.CardSale_FROM_DATE = clsCommon.myCstr(LblFromDate.Text)
                                obj.CardSale_TO_DATE = clsCommon.myCstr(lblToDate.Text)
                            End If
                        End If

                        obj.Booking_Type = clsCommon.myCstr(Gv1.Rows(i).Cells("SupplyType").Value)
                        If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                            obj.AgainstGatePass = 1
                        Else
                            obj.AgainstGatePass = 0
                        End If
                        obj.BookingThrough = "Uploader"
                        obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                        ''for detail table
                        obj.Arr = New List(Of clsBookingDetailDairySale)
                        Dim counterstart As Integer = 0
                        If rdbAgainstCashIndent.IsChecked = True Then
                            counterstart = 8
                        Else
                            counterstart = 9
                        End If

                        For k As Integer = counterstart To Gv1.Columns.Count - 1
                            If clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colZeroQtyForAllItem).Value), "0") = CompairStringResult.Equal Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                                objTr.Cust_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(Gv1.Columns(k).Name)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                    " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                    " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                    " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If


                                'objTr.Unit_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "'  AND Default_UOM =1 ", trans))

                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                   "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                   "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                            " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                            " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                            " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                            " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                            " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                             " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''Gv1.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                    " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                    "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                    " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                    " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                    " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                    " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                    "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                    "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                    "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                    "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                    " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                    "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                    "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                    "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Gv1.Rows(i).Cells(colPriceCode).Value & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                    ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & Gv1.Rows(i).Cells("BoothId").Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1

                                '''' for zero qty doc
                            Else
                                If clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value) = 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colZeroQtyForAllItem).Value), "1") = CompairStringResult.Equal Then
                                    objTr = New clsBookingDetailDairySale()
                                    objTr.Line_No = LineNo
                                    objTr.Booking_Qty = clsCommon.myCdbl(Gv1.Rows(i).Cells(k).Value)
                                    objTr.Cust_Code = clsCommon.myCstr(Gv1.Rows(i).Cells("BoothId").Value)
                                    objTr.Sampling = 0
                                    objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                    objTr.Short_Description = clsCommon.myCstr(Gv1.Columns(k).Name)


                                    Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                    Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                    " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                    " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                    " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                        For Each dr As DataRow In dt1.Rows
                                            objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                            objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                            objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                            objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                        Next
                                    End If

                                    qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                   "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                   "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                    dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                        objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                        objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    End If

                                    objTr.Item_Rate = 0
                                    objTr.Tax_On_Amount = 0
                                    objTr.Item_Basic_Rate = 0
                                    objTr.SellingPrice = 0
                                    objTr.OrgRate = 0
                                    objTr.DocumentAmount = 0
                                    objTr.Price_with_Tax = 0
                                    objTr.Amount_with_Tax = 0
                                    objTr.Booking_Status = 1
                                    DocuAmount = 0
                                    totalQty = 0

                                    obj.Arr.Add(objTr)

                                    LineNo = LineNo + 1
                                End If
                            End If
                        Next
                    End If
                    LineNo = 0
                    strcountno = intCurrInvNo
                    Dim intNextInvNo As Integer = -1

                    If intCounter + 1 < Gv1.Rows.Count Then
                        intNextInvNo = clsCommon.myCdbl(Gv1.Rows(intCounter + 1).Cells(colSlno).Value)
                    End If

                    If Not (intCurrInvNo = intNextInvNo) Then
                        ''for detail table Booking Payment Mode
                        If rdbAgainstCardIndent.IsChecked = True Then
                            Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
                            obj.AdvanceAmount = DocuAmount * dblCardSaleDays

                            obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
                            objTrPM = New clsBookingDetailDairySalePaymentMode()
                            objTrPM.SNo = 1
                            objTrPM.Payment_Mode = clsCommon.myCstr(Gv1.Rows(i).Cells("Payment Mode").Value)
                            objTrPM.Amount = obj.AdvanceAmount
                            obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)

                        End If


                        obj.TotalCrate = TotalCrate
                        obj.SaveData(obj, True, trans)
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)
                        If rdbAgainstCardIndent.IsChecked Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
                        Else
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
                        End If

                    End If
                    intCounter += 1
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub

    'Sub BookingSaveData(ByVal trans As SqlTransaction)

    '    Dim strcountno As String = ""
    '    Dim objTr As clsBookingDetailDairySale = Nothing
    '    Dim obj As clsBookingEntryDairySale = Nothing
    '    Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
    '    Dim LineNo As Integer = 1
    '    Try


    '        Dim DocuAmount As Double = 0
    '        Dim Tax1Amt As Double = 0
    '        Dim Tax2Amt As Double = 0
    '        Dim TaxBaseAmount As Double = 0
    '        Dim totalQty As Double = 0
    '        Dim intCounter As Integer = 0
    '        Dim j As Integer = 0
    '        Dim TotalCrate As Double = 0
    '        For Each dr As DataRow In dtmain.Rows
    '            j += 1
    '            clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating  Booking Records " & j & " of Total " & dtmain.Rows.Count)
    '            Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("SrNo"))

    '            If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("SrNo"))) <> CompairStringResult.Equal Then
    '                LineNo = 1
    '                DocuAmount = 0
    '                Tax1Amt = 0
    '                Tax2Amt = 0
    '                TaxBaseAmount = 0
    '                obj = New clsBookingEntryDairySale()

    '                obj.IsSampling = IIf(clsCommon.myCstr(dr("isSampling")) = "Y", 1, 0)
    '                obj.Document_Date = clsCommon.myCDate(dr("BookingDate"))
    '                obj.location_code = clsCommon.myCstr(dr("Location"))
    '                obj.AgainstGatePass = 0
    '                obj.Is_Taxable = 2
    '                obj.TRANSACTION_TYPE = ""
    '                If rdbAgainstCashIndent.IsChecked = True Then
    '                    obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
    '                Else
    '                    obj.From_Screen_code = clsUserMgtCode.frmbookingdairyFreshSale
    '                    obj.Card_SALE_No = clsCommon.myCstr(dr("CardSaleCode"))
    '                    If clsCommon.myLen(obj.Card_SALE_No) > 0 Then
    '                        obj.CardSale_FROM_DATE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DATEDIFF(dd, 0,cast(from_date as date)) + CONVERT(DATETIME,CAST(GETDATE() AS TIME))  from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
    '                        obj.CardSale_TO_DATE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TO_DATE from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
    '                    End If
    '                End If

    '                obj.Booking_Type = clsCommon.myCstr(dr("BookingType"))
    '                obj.BookingThrough = "Uploader"

    '                ''for detail table
    '                obj.Arr = New List(Of clsBookingDetailDairySale)
    '                objTr = New clsBookingDetailDairySale()

    '                objTr.Line_No = 1
    '                objTr.Booking_Qty = clsCommon.myCdbl(dr("QTY"))
    '                objTr.Cust_Code = clsCommon.myCstr(dr("Customer"))
    '                objTr.Sampling = 0
    '                objTr.Loc_Code = clsCommon.myCstr(dr("Location"))
    '                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
    '                objTr.Short_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
    '                objTr.Unit_code = clsCommon.myCstr(dr("UOM"))


    '                Dim qry As String = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
    '       "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
    '       "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(dr("Customer")) & "'"

    '                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
    '                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
    '                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
    '                End If

    '                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
    '                " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
    '                " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
    '                " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
    '                " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
    '                " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
    '                 " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


    '                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
    '                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
    '                    Dim objD As clsSchemeApplyOnDairy = Nothing
    '                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

    '                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
    '                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
    '                            objTr.SchemeType = objtrScheme.schm_Type
    '                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
    '                        Next

    '                    End If
    '                End If

    '                'objTr.Total_Qty = clsCommon.myCdbl(dr("QTY"))
    '                objTr.Tax_On_Amount = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.Item_Basic_Rate = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.SellingPrice = clsCommon.myCdbl(dr("SellingRate"))
    '                objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
    '                objTr.DocumentAmount = clsCommon.myCdbl(dr("Amount"))

    '                objTr.Item_Rate = clsCommon.myCdbl(dr("Rate"))
    '                objTr.Tax_NonTax = clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ", trans)
    '                objTr.FreshAmbient = clsDBFuncationality.getSingleValue("select case when Is_Ambient=1 then 'PS' WHEN Is_FreshItem=1 THEN 'FS' ELSE '' END from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ", trans)

    '                objTr.Price_with_Tax = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.Amount_with_Tax = clsCommon.myCdbl(dr("AmountWithTax"))
    '                objTr.Booking_Status = 1

    '                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(dr("Amount")), 2)
    '                totalQty = totalQty + clsCommon.myCdbl(dr("Qty"))
    '                TotalCrate = TotalCrate + clsCommon.myCdbl(dr("CrateQty"))
    '                obj.Arr.Add(objTr)


    '            Else
    '                objTr = New clsBookingDetailDairySale()

    '                objTr.Line_No = LineNo
    '                objTr.Booking_Qty = clsCommon.myCdbl(dr("QTY"))
    '                objTr.Cust_Code = clsCommon.myCstr(dr("Customer"))
    '                objTr.Sampling = 0
    '                objTr.Loc_Code = clsCommon.myCstr(dr("Location"))
    '                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
    '                objTr.Short_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans))
    '                objTr.Unit_code = clsCommon.myCstr(dr("UOM"))


    '                Dim qry As String = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
    '       "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
    '       "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(dr("Customer")) & "'"

    '                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
    '                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
    '                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
    '                End If

    '                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
    '                " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
    '                " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
    '                " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
    '                " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
    '                " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
    '                 " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


    '                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
    '                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
    '                    Dim objD As clsSchemeApplyOnDairy = Nothing
    '                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

    '                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
    '                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
    '                            objTr.SchemeType = objtrScheme.schm_Type
    '                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
    '                        Next

    '                    End If
    '                End If


    '                objTr.Tax_On_Amount = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.Item_Basic_Rate = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.SellingPrice = clsCommon.myCdbl(dr("SellingRate"))
    '                objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
    '                objTr.DocumentAmount = clsCommon.myCdbl(dr("Amount"))

    '                objTr.Item_Rate = clsCommon.myCdbl(dr("Rate"))
    '                objTr.Tax_NonTax = clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ", trans)
    '                objTr.FreshAmbient = clsDBFuncationality.getSingleValue("select case when Is_Ambient=1 then 'PS' WHEN Is_FreshItem=1 THEN 'FS' ELSE '' END from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ", trans)

    '                objTr.Price_with_Tax = clsCommon.myCdbl(dr("ItemBasicPrice"))
    '                objTr.Amount_with_Tax = clsCommon.myCdbl(dr("AmountWithTax"))
    '                objTr.Booking_Status = 1


    '                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(dr("Amount")), 2)
    '                totalQty = totalQty + clsCommon.myCdbl(dr("Qty"))
    '                TotalCrate = TotalCrate + clsCommon.myCdbl(dr("CrateQty"))
    '                obj.Arr.Add(objTr)
    '            End If
    '            LineNo = LineNo + 1
    '            strcountno = intCurrInvNo
    '            Dim intNextInvNo As Integer = -1

    '            If intCounter + 1 < dtmain.Rows.Count Then
    '                intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("SrNo"))
    '            End If

    '            If Not (intCurrInvNo = intNextInvNo) Then
    '                ''for detail table Booking Payment Mode
    '                If rdbAgainstCardIndent.IsChecked = True Then
    '                    Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
    '                    obj.AdvanceAmount = DocuAmount * dblCardSaleDays

    '                    obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
    '                    objTrPM = New clsBookingDetailDairySalePaymentMode()
    '                    objTrPM.SNo = 1
    '                    objTrPM.Payment_Mode = clsCommon.myCstr(dr("PaymentMode"))
    '                    objTrPM.Amount = obj.AdvanceAmount
    '                    obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)

    '                End If


    '                obj.TotalCrate = TotalCrate
    '                obj.SaveData(obj, True, trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)
    '                clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
    '            End If
    '            intCounter += 1

    '        Next

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        strcountno = Nothing
    '        obj = Nothing
    '        objTr = Nothing
    '    End Try

    'End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub txtCardSaleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCardSaleCode._MYValidating
        If rdbAgainstCardIndent.IsChecked = True OrElse rdbAgainstTruckSheet.IsChecked = True Then
            Dim qry As String = "select * from Tspl_Card_Sale "
            Dim WhrCls As String = " status=1 "
            txtCardSaleCode.Value = clsCommon.ShowSelectForm("CardSaleFndr", qry, "Card_no", WhrCls, txtCardSaleCode.Value, "convert(datetime,Tspl_Card_Sale.FROM_DATE,103),card_no", isButtonClicked)

            If clsCommon.myLen(txtCardSaleCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Create Card Sale No." & Environment.NewLine)
                txtCardSaleCode.Value = ""
                LblFromDate.Text = ""
                lblToDate.Text = ""
            Else
                LblFromDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DATEDIFF(dd, 0,cast(from_date as date)) + CONVERT(DATETIME,CAST(GETDATE() AS TIME))  from Tspl_Card_Sale where Card_No ='" & txtCardSaleCode.Value & "'"))
                lblToDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TO_DATE from Tspl_Card_Sale where Card_No ='" & txtCardSaleCode.Value & "'"))
            End If

        End If

    End Sub

    Private Sub rdbAgainstCashIndent_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstCashIndent.ToggleStateChanged
        If rdbAgainstCashIndent.IsChecked Then
            txtCardSaleCode.Enabled = False
        Else
            txtCardSaleCode.Enabled = True
        End If
        txtCardSaleCode.Value = ""
        LblFromDate.Text = ""
        lblToDate.Text = ""
    End Sub


    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        OpenFileDialog.FileName = ""
        OpenFileDialog.Filter = "Text Files (*.TXT)|*.TXT"
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtBrowse.Text = OpenFileDialog.FileName
            Me.Tag = OpenFileDialog.SafeFileName
        Else
            Exit Sub
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtBrowse.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If txtBrowse.Text.Contains(".TXT") = True Or txtBrowse.Text.Contains(".txt") = True Then
                LoadBlankGridTS()
                Dim path As String = txtBrowse.Text
                Try
                    Dim lineCount = System.IO.File.ReadAllLines(path).Length
                    Dim sr As System.IO.StreamReader = New System.IO.StreamReader(path)
                    Dim line As String = ""
                    clsCommon.ProgressBarPercentShow()
                    Do While sr.Peek() >= 0
                        line = sr.ReadLine()
                        gvTS.Rows.AddNew()
                        clsCommon.ProgressBarPercentUpdate(gvTS.Rows.Count * 100 / lineCount, "Loading selected File " + clsCommon.myCstr(gvTS.Rows.Count) + "/" + clsCommon.myCstr(lineCount))

                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colTSLine).Value = gvTS.Rows.Count
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colTSDetail).Value = line
                        Gv1.Refresh()
                    Loop
                    clsCommon.ProgressBarPercentHide()
                    sr.Close()
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error in File Reading " + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridTS()


        gvTS.Rows.Clear()
        gvTS.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colTSLine
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTS.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Details"
        repoLineNo.Name = colTSDetail
        repoLineNo.Width = 1000
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTS.MasterTemplate.Columns.Add(repoLineNo)

        gvTS.AllowDeleteRow = True
        gvTS.AllowAddNewRow = False
        gvTS.ShowGroupPanel = False
        gvTS.AllowColumnReorder = False
        gvTS.AllowRowReorder = False
        gvTS.EnableSorting = False
        gvTS.EnableFiltering = True
        gvTS.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTS.MasterTemplate.ShowRowHeaderColumn = False
        gvTS.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtBrowse.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If gvTS.Rows.Count > 0 Then
                LoadBlankGridTSItems()
                Dim ii As Integer = 0
                Dim arrExcept As New List(Of String)
                Try
                    Dim qry As String = "select Name from TSPL_Noetpad_Read_Except"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            arrExcept.Add(clsCommon.myCstr(dr("Name")))
                        Next
                    End If

                    Dim strZone As String = Nothing
                    Dim strRoute As String = Nothing
                    Dim strBoothID As String = Nothing
                    Dim strZone1 As String = Nothing
                    Dim strRoute1 As String = Nothing
                    Dim strBoothID1 As String = Nothing
                    Dim isPreviousLineDash As Boolean = False
                    Dim isPreviousToPReviousLineDash As Boolean = False
                    Dim isLastLineStar As Boolean = False
                    Dim isRouteTotalStart As Boolean = False
                    Dim BookingCount As Integer = 0
                    clsCommon.ProgressBarPercentShow()
                    For ii = 0 To gvTS.Rows.Count - 1
                        If ii = 3139 - 1 Then
                            Dim x As Integer = 0
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1) / gvTS.Rows.Count * 100, "Separating " & (ii + 1) & "  of  Total " & gvTS.Rows.Count)
                        Gv1.Refresh()
                        Dim strLine As String = (gvTS.Rows(ii).Cells(colTSDetail).Value)
                        If strLine.Contains("THE TELANGANA STATE DAIRY") Or
                            strLine.StartsWith("Page:") Or
                            strLine.StartsWith("SPECIAL GHEE MYSORE") Or
                             strLine.StartsWith("Booth Name") Then
                            Continue For
                        End If
                        Dim strTemp As String = strLine
                        If clsCommon.myLen(strLine) > 200 Then
                            strTemp = strLine.Substring(0, 199)
                        End If

                        If arrExcept IsNot Nothing AndAlso arrExcept.Count > 0 Then
                            Dim isContinueFor As Boolean = False
                            If arrExcept.Contains(strTemp) Then
                                isContinueFor = True
                            Else
                                For Each strT As String In arrExcept
                                    If strTemp.StartsWith(strT) Then
                                        isContinueFor = True
                                        Exit For
                                    End If
                                Next
                            End If
                            If isContinueFor Then
                                isPreviousLineDash = False
                                isPreviousToPReviousLineDash = False
                                Continue For
                            End If
                        End If


                        If strLine.Contains("-------------------------") Then
                            isLastLineStar = False
                            isPreviousLineDash = True
                            Continue For
                        End If
                        If strLine.EndsWith("*") Then
                            isPreviousLineDash = False
                            isPreviousToPReviousLineDash = False
                            isLastLineStar = True
                            Continue For
                        End If

                        If strLine.StartsWith("ZONE") Then
                            strZone = Microsoft.VisualBasic.Mid(strLine, 7, 15)
                            Continue For
                        End If


                        If strLine.StartsWith("ROUTE TOTAL:") Then
                            isRouteTotalStart = True
                            Continue For
                        End If
                        If strLine.Contains("Q.P.S") And strLine.Contains("LOADED BY") And strLine.Contains("SECURITY") And strLine.Contains("RMRD") Then
                            isRouteTotalStart = False
                            Continue For
                        End If
                        If isRouteTotalStart Then
                            Continue For
                        End If

                        If strLine.StartsWith("ROUTE") Then
                            strRoute = Microsoft.VisualBasic.Mid(strLine, 8, 15)
                            Continue For
                        End If

                        If isPreviousToPReviousLineDash Then
                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 1, 5)) > 0 Then
                                strBoothID = Microsoft.VisualBasic.Mid(strLine, 1, 5)
                                If strBoothID.Contains("(") Then
                                    Dim strTempBreak As String() = strBoothID.Split("(")
                                    strBoothID = strTempBreak(0)
                                End If
                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIBoothID).Value = strBoothID
                                isPreviousLineDash = False
                                isPreviousToPReviousLineDash = False
                            End If
                        End If

                        If Not isLastLineStar Then
                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 21, 9)) > 0 Then
                                gvTSItem.Rows.AddNew()
                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSISNo).Value = gvTSItem.Rows.Count
                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIPSNo).Value = gvTS.Rows(ii).Cells(colTSLine).Value

                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIZone).Value = strZone
                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIRoute).Value = strRoute
                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIBoothID).Value = strBoothID

                                strTemp = Microsoft.VisualBasic.Mid(strLine, 21, 10)
                                strTemp = strTemp.Replace("         ", " ")
                                strTemp = strTemp.Replace("        ", " ")
                                strTemp = strTemp.Replace("       ", " ")
                                strTemp = strTemp.Replace("      ", " ")
                                strTemp = strTemp.Replace("     ", " ")
                                strTemp = strTemp.Replace("    ", " ")
                                strTemp = strTemp.Replace("   ", " ")
                                strTemp = strTemp.Replace("  ", " ")

                                gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSIType).Value = strTemp


                                Try
                                    strTemp = strLine.Substring(30).TrimStart()
                                    strTemp = strTemp.Replace("               ", "#")
                                    strTemp = strTemp.Replace("              ", "#")
                                    strTemp = strTemp.Replace("             ", "#")
                                    strTemp = strTemp.Replace("            ", "#")
                                    strTemp = strTemp.Replace("           ", "#")
                                    strTemp = strTemp.Replace("          ", "#")
                                    strTemp = strTemp.Replace("         ", "#")
                                    strTemp = strTemp.Replace("        ", "#")
                                    strTemp = strTemp.Replace("       ", "#")
                                    strTemp = strTemp.Replace("      ", "#")
                                    strTemp = strTemp.Replace("     ", "#")
                                    strTemp = strTemp.Replace("    ", "#")
                                    strTemp = strTemp.Replace("   ", "#")
                                    strTemp = strTemp.Replace("  ", "#")
                                    strTemp = strTemp.Replace(" ", "#")
                                    Dim strTempBreak As String() = strTemp.Split("#")
                                    gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSICR).Value = strTempBreak(0)
                                    gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSICD).Value = strTempBreak(1)
                                    gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSISO).Value = strTempBreak(2)
                                    gvTSItem.Rows(gvTSItem.Rows.Count - 1).Cells(colTSICash).Value = strTempBreak(3)

                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If

                        isPreviousToPReviousLineDash = isPreviousLineDash
                        isPreviousLineDash = False
                    Next
                    clsCommon.ProgressBarPercentHide()
                    loadBookingCountNumber()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at " & ii + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub loadBookingCountNumber()
        Try
            Dim strZone As String = Nothing
            Dim strRoute As String = Nothing
            Dim strBoothID As String = Nothing
            isCDExist = False
            isCashExist = False
            isCRExist = False
            isSOExist = False

            Dim BookingCount As Integer = 0
            'clsCommon.ProgressBarPercentShow()
            For i = 0 To gvTSItem.Rows.Count - 1

                If clsCommon.CompairString(strZone, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIZone).Value)) = CompairStringResult.Equal And clsCommon.CompairString(strRoute, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIRoute).Value)) = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(strBoothID), clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)) = CompairStringResult.Equal Then
                Else
                    BookingCount = BookingCount + 1
                End If
                gvTSItem.Rows(i).Cells(colTSIBookingCountCash).Value = "0"
                gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value = "0"
                gvTSItem.Rows(i).Cells(colTSIBookingCountCR).Value = "0"
                gvTSItem.Rows(i).Cells(colTSIBookingCountSO).Value = "0"
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                    gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value = clsCommon.myCstr(BookingCount)
                    isCDExist = True
                End If
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value) > 0 Then
                    gvTSItem.Rows(i).Cells(colTSIBookingCountCash).Value = clsCommon.myCstr(BookingCount)
                    isCashExist = True
                End If
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value) > 0 Then
                    gvTSItem.Rows(i).Cells(colTSIBookingCountCR).Value = clsCommon.myCstr(BookingCount)
                    isCRExist = True
                End If
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value) > 0 Then
                    gvTSItem.Rows(i).Cells(colTSIBookingCountSO).Value = clsCommon.myCstr(BookingCount)
                    isSOExist = True
                End If

                strZone = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIZone).Value)
                strBoothID = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                strRoute = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIRoute).Value)

            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridTSItems()
        gvTSItem.Rows.Clear()
        gvTSItem.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colTSISNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo"
        repoLineNo.Name = colTSIPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Zone"
        repoLineNo.Name = colTSIZone
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Route"
        repoLineNo.Name = colTSIRoute
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booth ID"
        repoLineNo.Name = colTSIBoothID
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Type"
        repoLineNo.Name = colTSIType
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTSItem.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CR"
        repoTaxBaseAmt.Name = colTSICR
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "CD"
        repoTaxBaseAmt.Name = colTSICD
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "SO"
        repoTaxBaseAmt.Name = colTSISO
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Cash"
        repoTaxBaseAmt.Name = colTSICash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "BookingCountCash"
        repoTaxBaseAmt.Name = colTSIBookingCountCash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "BookingCountCD"
        repoTaxBaseAmt.Name = colTSIBookingCountCD
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "BookingCountCR"
        repoTaxBaseAmt.Name = colTSIBookingCountCR
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "BookingCountSO"
        repoTaxBaseAmt.Name = colTSIBookingCountSO
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTSItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        gvTSItem.AllowDeleteRow = True
        gvTSItem.AllowAddNewRow = False
        gvTSItem.ShowGroupPanel = False
        gvTSItem.AllowColumnReorder = False
        gvTSItem.AllowRowReorder = False
        gvTSItem.EnableSorting = False
        gvTSItem.EnableFiltering = True
        gvTSItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTSItem.MasterTemplate.ShowRowHeaderColumn = False
        gvTSItem.TableElement.TableHeaderHeight = 40
    End Sub



    Sub SaveAndPostAgainstTruckSheet()
        arrVendorInvoiceNo = New List(Of String)
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim trans As SqlTransaction = Nothing
        Try

            If gvTSItem.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                trans = clsDBFuncationality.GetTransactin()

                objCommonVar.CurrentUserCode = CurrentUserCode
                'If isCDExist = True Then
                '    BookingSaveDataTruckSheet_CD(trans)
                'End If
                If isCashExist = True Then
                    BookingSaveDataTruckSheet_Cash(trans)
                End If
                If isCRExist = True Then
                    BookingSaveDataTruckSheet_CR(trans)
                End If
                If isSOExist = True Then
                    BookingSaveDataTruckSheet_SO(trans)
                End If

                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                gvTSItem.Columns.Clear()
            Else
                Throw New Exception("No Rows found to save")
            End If
        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message, "Dairy Booking Uploader")
        End Try
    End Sub
    Sub BookingSaveDataTruckSheet_CR(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 1
            For i As Integer = 0 To gvTSItem.Rows.Count - 1
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCR).Value) > 0 Then
                    If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvTSItem.Rows.Count, " Creating  Booking CR Records " & j & " of Total " & gvTSItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCR).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBookingCountCR).Value)) <> CompairStringResult.Equal Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()


                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)
                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer



                            obj.Booking_Type = "CR"
                            If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                                obj.AgainstGatePass = 1
                            Else
                                obj.AgainstGatePass = 0
                            End If
                            obj.BookingThrough = "Uploader"
                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If


                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICR).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If


                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvTSItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountCR).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        If obj IsNot Nothing Then

                            If AllowToCreateNoOfBookingPerDay > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "") <> CompairStringResult.Equal Then

                                    Dim STRSQL As String = "select count(distinct TSPL_BOOKING_MATSER.Document_No) as cc from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)"
                                    If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                                        STRSQL += " And TSPL_CUSTOMER_MASTER.customer_category<>'Others' and TSPL_BOOKING_MATSER.Booking_Type<>'CD' "
                                    End If
                                    Dim TempBookingExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(STRSQL, trans))
                                    If TempBookingExist >= AllowToCreateNoOfBookingPerDay Then
                                        Throw New Exception("Booking already exist for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "] and Booth Id " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If


                                End If

                            End If

                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)


                        End If


                    End If
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

    Sub BookingSaveDataTruckSheet_SO(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 1
            For i As Integer = 0 To gvTSItem.Rows.Count - 1
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountSO).Value) > 0 Then
                    If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvTSItem.Rows.Count, " Creating  Booking SO Records " & j & " of Total " & gvTSItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountSO).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBookingCountSO).Value)) <> CompairStringResult.Equal Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()


                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)
                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer



                            obj.Booking_Type = "SO"
                            If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                                obj.AgainstGatePass = 1
                            Else
                                obj.AgainstGatePass = 0
                            End If
                            obj.BookingThrough = "Uploader"
                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If


                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSISO).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If


                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvTSItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountSO).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        If obj IsNot Nothing Then
                            If AllowToCreateNoOfBookingPerDay > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "") <> CompairStringResult.Equal Then

                                    Dim STRSQL As String = "select count(distinct TSPL_BOOKING_MATSER.Document_No) as cc from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)"
                                    If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                                        STRSQL += " And TSPL_CUSTOMER_MASTER.customer_category<>'Others' and TSPL_BOOKING_MATSER.Booking_Type<>'CD' "
                                    End If
                                    Dim TempBookingExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(STRSQL, trans))
                                    If TempBookingExist >= AllowToCreateNoOfBookingPerDay Then
                                        Throw New Exception("Booking already exist for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "] and Booth Id " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If


                                End If

                            End If

                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)


                        End If


                    End If
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
    Sub BookingSaveDataTruckSheet_Cash(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 1
            For i As Integer = 0 To gvTSItem.Rows.Count - 1
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCash).Value) > 0 Then
                    If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvTSItem.Rows.Count, " Creating  Booking Cash Records " & j & " of Total " & gvTSItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCash).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBookingCountCash).Value)) <> CompairStringResult.Equal OrElse obj Is Nothing Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()


                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)
                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer



                            obj.Booking_Type = "CASH"
                            If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                                obj.AgainstGatePass = 1
                            Else
                                obj.AgainstGatePass = 0
                            End If
                            obj.BookingThrough = "Uploader"
                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value)
                                ' objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICash).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If


                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvTSItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountCash).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        If obj IsNot Nothing Then

                            If AllowToCreateNoOfBookingPerDay > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "' ", trans)), "") <> CompairStringResult.Equal Then

                                    Dim STRSQL As String = "select count(distinct TSPL_BOOKING_MATSER.Document_No) as cc from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)"
                                    If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                                        STRSQL += " And TSPL_CUSTOMER_MASTER.customer_category<>'Others' and TSPL_BOOKING_MATSER.Booking_Type<>'CD' "
                                    End If
                                    Dim TempBookingExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(STRSQL, trans))
                                    If TempBookingExist >= AllowToCreateNoOfBookingPerDay Then
                                        Throw New Exception("Booking already exist for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "] and Booth Id " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If


                                End If

                            End If



                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)


                        End If


                    End If
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
    Sub BookingSaveDataTruckSheet_CD(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 0
            For i As Integer = 0 To gvTSItem.Rows.Count - 1
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value) > 0 Then
                    If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvTSItem.Rows.Count, " Creating  Booking CD Records " & j & " of Total " & gvTSItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value)) <> CompairStringResult.Equal Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()

                            obj.Document_Date = clsCommon.myCDate(LblFromDate.Text)
                            '' obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)


                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            ''obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
                            obj.From_Screen_code = ""
                            obj.Card_SALE_No = clsCommon.myCstr(txtCardSaleCode.Value)
                            If clsCommon.myLen(obj.Card_SALE_No) > 0 Then
                                obj.CardSale_FROM_DATE = clsCommon.myCstr(LblFromDate.Text)
                                obj.CardSale_TO_DATE = clsCommon.myCstr(lblToDate.Text)
                            End If


                            obj.Booking_Type = "CD"
                            If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                                obj.AgainstGatePass = 1
                            Else
                                obj.AgainstGatePass = 0
                            End If
                            obj.BookingThrough = "Uploader"
                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value)
                                objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))


                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & "")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value)
                                objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If

                        'End if 
                        'LineNo = 0
                        'strcountno = intCurrInvNo
                        'Dim intNextInvNo As Integer = -1

                        'If intCounter + 1 < gvTSItem.Rows.Count Then

                        '    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountCD).Value)

                        'End If
                        'If intNextInvNo > 0 Then
                        '    If Not (intCurrInvNo = intNextInvNo) Then
                        '        ''for detail table Booking Payment Mode
                        '        Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
                        '        obj.AdvanceAmount = DocuAmount * dblCardSaleDays

                        '        obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
                        '        objTrPM = New clsBookingDetailDairySalePaymentMode()
                        '        objTrPM.SNo = 1
                        '        objTrPM.Payment_Mode = "CASH"
                        '        objTrPM.Amount = obj.AdvanceAmount
                        '        obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)


                        '        obj.TotalCrate = TotalCrate
                        '        obj.SaveData(obj, True, trans)
                        '        clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)
                        '        If rdbAgainstCardIndent.IsChecked Then
                        '            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
                        '        Else
                        '            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)
                        '        End If

                        '    End If
                        'End If

                        'intCounter += 1
                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvTSItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountCD).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        ''for detail table Booking Payment Mode
                        If obj IsNot Nothing Then
                            Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
                            obj.AdvanceAmount = DocuAmount * dblCardSaleDays

                            obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
                            objTrPM = New clsBookingDetailDairySalePaymentMode()
                            objTrPM.SNo = 1
                            objTrPM.Payment_Mode = clsCommon.myCstr(txtPaymentMode.Value)
                            objTrPM.Amount = obj.AdvanceAmount
                            obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)


                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)

                        End If


                    End If
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
    Sub BookingSaveDataTruckSheet_CD_WithoutReference(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objTrPM As clsBookingDetailDairySalePaymentMode = Nothing
        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 0
            For i As Integer = 0 To gvTSItem.Rows.Count - 1
                If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value) > 0 Then
                    If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvTSItem.Rows.Count, " Creating  Booking CD Records " & j & " of Total " & gvTSItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBookingCountCD).Value)) <> CompairStringResult.Equal OrElse obj Is Nothing Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()

                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")).AddHours(9)

                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            ''obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
                            'obj.From_Screen_code = clsUserMgtCode.frmbookingdairyFreshSale
                            'obj.Card_SALE_No = clsCommon.myCstr(txtCardSaleCode.Value)
                            'If clsCommon.myLen(obj.Card_SALE_No) > 0 Then
                            '    obj.CardSale_FROM_DATE = clsCommon.myCstr(LblFromDate.Text)
                            '    obj.CardSale_TO_DATE = clsCommon.myCstr(lblToDate.Text)
                            'End If


                            obj.Booking_Type = "CD"
                            If clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
                                obj.AgainstGatePass = 1
                            Else
                                obj.AgainstGatePass = 0
                            End If
                            obj.BookingThrough = "Uploader"
                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)

                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If

                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))


                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & "")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvTSItem.Rows(i).Cells(colTSICD).Value)
                                '                                objTr.Cust_Code = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIBoothID).Value) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSISNo).Value) & ".")
                                End If


                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvTSItem.Rows(i).Cells(colTSIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvTSItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If


                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvTSItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvTSItem.Rows(intCounter + 1).Cells(colTSIBookingCountCD).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        ''for detail table Booking Payment Mode
                        If obj IsNot Nothing Then
                            ' Dim dblCardSaleDays As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select No_Of_Days from Tspl_Card_Sale where Card_No ='" & obj.Card_SALE_No & "'", trans))
                            'obj.AdvanceAmount = DocuAmount * dblCardSaleDays
                            obj.AdvanceAmount = DocuAmount

                            'obj.arrBookingDetailDairySalePaymentMode = New List(Of clsBookingDetailDairySalePaymentMode)
                            'objTrPM = New clsBookingDetailDairySalePaymentMode()
                            'objTrPM.SNo = 1
                            'objTrPM.Payment_Mode = clsCommon.myCstr(txtPaymentMode.Value)
                            'objTrPM.Amount = obj.AdvanceAmount
                            'obj.arrBookingDetailDairySalePaymentMode.Add(objTrPM)


                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)

                        End If


                    End If
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
    Private Sub txtPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentMode._MYValidating
        Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
        txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector45", Qry1, "PaymentMode", "", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
    End Sub

    Private Sub frmDairyBookingUploader_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                lblExcept.Visible = True
                txtExcept.Visible = True
                btnExcept.Visible = True
            End If
        End If
    End Sub

    Private Sub btnExcept_Click(sender As Object, e As EventArgs) Handles btnExcept.Click
        Try
            If clsCommon.myLen(txtExcept.Text) <= 0 Then
                Throw New Exception("Please Enter Exception")
            End If
            Dim qry As String = "select Name from TSPL_Noetpad_Read_Except where Name like '%" + txtExcept.Text + "%'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Already Saved")
            End If
            qry = "Insert into TSPL_Noetpad_Read_Except(Name) values ('" + txtExcept.Text + "') "
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow(Me, "Saved successfully", Me.Text)
            txtExcept.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnFolderBrowse_Click(sender As Object, e As EventArgs) Handles btnFolderBrowse.Click
        Try
            txtFolderBrowse.Text = ""
            FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
            FolderBrowserDialog1.ShowNewFolderButton = False
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                txtFolderBrowse.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            If clsCommon.myLen(txtFolderBrowse.Text) <= 0 Then
                Throw New Exception("Please Select file to upload")
            End If
            If clsCommon.myLen(txtFolderBrowse.Text) > 0 Then
                LoadBlankGridGP()
                Dim strFileSize As String = ""
                Dim di As New IO.DirectoryInfo(txtFolderBrowse.Text)
                Dim aryFi As IO.FileInfo() = di.GetFiles("*.txt")
                Dim fi As IO.FileInfo
                Dim ii As Integer = 0
                Dim Total As Integer = aryFi.Count
                clsCommon.ProgressBarPercentShow()
                For Each fi In aryFi
                    Try
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Files [" & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "]")
                        Try
                            Dim path As String = fi.FullName
                            Dim lineCount = System.IO.File.ReadAllLines(path).Length
                            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(path)
                            Dim line As String = ""
                            Dim FileSno As Integer = 1
                            Do While sr.Peek() >= 0
                                line = sr.ReadLine()
                                gvGP.Rows.AddNew()
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPSNo).Value = gvGP.Rows.Count
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPFile).Value = fi.Name
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPFileSNo).Value = FileSno
                                gvGP.Rows(gvGP.Rows.Count - 1).Cells(colGPDetail).Value = line
                                FileSno += 1
                                Gv1.Refresh()
                            Loop
                            sr.Close()
                        Catch ex As Exception
                            clsCommon.ProgressBarPercentHide()
                            Throw New Exception("Error in File Reading " + Environment.NewLine + ex.ToString())
                        End Try

                    Catch ex As Exception
                        Throw New Exception("Error in File " + fi.Name + Environment.NewLine + ex.Message)
                    End Try
                Next
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridGP()
        gvGP.Rows.Clear()
        gvGP.Columns.Clear()


        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colGPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "File"
        repoLineNo.Name = colGPFile
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "File SNo"
        repoLineNo.Name = colGPFileSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Details"
        repoLineNo.Name = colGPDetail
        repoLineNo.Width = 1000
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGP.MasterTemplate.Columns.Add(repoLineNo)

        gvGP.AllowDeleteRow = True
        gvGP.AllowAddNewRow = False
        gvGP.ShowGroupPanel = False
        gvGP.AllowColumnReorder = False
        gvGP.AllowRowReorder = False
        gvGP.EnableSorting = False
        gvGP.EnableFiltering = True
        gvGP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvGP.MasterTemplate.ShowRowHeaderColumn = False
        gvGP.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(txtFolderBrowse.Text) <= 0 Then
                Throw New Exception("Please Select folder to upload")
            End If
            If gvGP.Rows.Count > 0 Then
                LoadBlankGridGPItems()
                Dim ii As Integer = 0
                Try
                    Dim strFileName As String = ""
                    Dim strDate As String = Nothing
                    Dim strShift As String = Nothing
                    Dim strRoute As String = Nothing
                    Dim strBoothID As String = Nothing
                    Dim strTemp As String = Nothing
                    clsCommon.ProgressBarPercentShow()
                    Dim isReadFile As Boolean = False
                    For ii = 0 To gvGP.Rows.Count - 1
                        If ii = 20 - 1 Then
                            Dim x As Integer = 0
                        End If
                        If Not clsCommon.CompairString(strFileName, clsCommon.myCstr(gvGP.Rows(ii).Cells(colGPFile).Value)) = CompairStringResult.Equal Then
                            isReadFile = True
                            strFileName = clsCommon.myCstr(gvGP.Rows(ii).Cells(colGPFile).Value)
                        End If
                        clsCommon.ProgressBarPercentUpdate((ii + 1) / gvGP.Rows.Count * 100, "Separating " & (ii + 1) & "  of  Total " & gvGP.Rows.Count)
                        gvGPItem.Refresh()

                        Dim strLine As String = (gvGP.Rows(ii).Cells(colGPDetail).Value)
                        If clsCommon.myLen(strLine) <= 0 Then
                            Continue For
                        End If
                        strTemp = strLine.Trim()
                        If strTemp.Contains("THE TELANGANA STATE DAIRY") Or
                                strTemp.StartsWith("DES") Or
                                strTemp.StartsWith("01-04-11") Or
                                strTemp.StartsWith("Time :") Or
                                strTemp.StartsWith("Booth Name") Then
                            Continue For
                        End If
                        If strLine.Contains("DISTRIBUTOR WISE") Then
                            strDate = Microsoft.VisualBasic.Mid(strLine, 63, 10)
                            strShift = Microsoft.VisualBasic.Mid(strLine, 29, 7)
                            Continue For
                        End If
                        If strLine.Contains("-------------------------") Then
                            strTemp = (gvGP.Rows(ii - 1).Cells(colGPDetail).Value)
                            If clsCommon.myLen(strTemp) <= 0 Or (strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/")) Then
                                strTemp = (gvGP.Rows(ii + 1).Cells(colGPDetail).Value)
                                If clsCommon.myLen(strTemp) <= 0 Then
                                    isReadFile = False
                                End If
                            End If
                            Continue For
                        End If
                        If strLine.StartsWith("Route :") Then
                            strRoute = Microsoft.VisualBasic.Mid(strLine, 8, 10)
                        End If

                        If isReadFile Then ''DashCounter = 2
                            strTemp = Microsoft.VisualBasic.Mid(strLine, 1, 18)
                            If strTemp.Contains("(") AndAlso strTemp.Contains(")") AndAlso strTemp.Contains("/") Then
                                Dim strTempBreak As String() = strTemp.Split("(")
                                strBoothID = strTempBreak(0).Trim
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID
                                Try
                                    If clsCommon.myLen(gvGPItem.Rows(gvGPItem.Rows.Count - 2).Cells(colGPIBoothID).Value) <= 0 Then
                                        gvGPItem.Rows(gvGPItem.Rows.Count - 2).Cells(colGPIBoothID).Value = strBoothID
                                    End If
                                Catch ex As Exception
                                End Try
                            End If

                            If clsCommon.myLen(Microsoft.VisualBasic.Mid(strLine, 21, 9)) > 0 Then
                                gvGPItem.Rows.AddNew()
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPISNo).Value = gvGPItem.Rows.Count
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIPSNo).Value = gvGP.Rows(ii).Cells(colGPSNo).Value
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIFile).Value = gvGP.Rows(ii).Cells(colGPFile).Value
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIDate).Value = strDate
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIShift).Value = strShift
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIRoute).Value = strRoute
                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIBoothID).Value = strBoothID

                                strTemp = Microsoft.VisualBasic.Mid(strLine, 20, 10)
                                strTemp = strTemp.Replace("         ", " ")
                                strTemp = strTemp.Replace("        ", " ")
                                strTemp = strTemp.Replace("       ", " ")
                                strTemp = strTemp.Replace("      ", " ")
                                strTemp = strTemp.Replace("     ", " ")
                                strTemp = strTemp.Replace("    ", " ")
                                strTemp = strTemp.Replace("   ", " ")
                                strTemp = strTemp.Replace("  ", " ")

                                gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPIType).Value = strTemp


                                Try
                                    strTemp = strLine.Substring(31).TrimStart()
                                    strTemp = strTemp.Replace("               ", "#")
                                    strTemp = strTemp.Replace("              ", "#")
                                    strTemp = strTemp.Replace("             ", "#")
                                    strTemp = strTemp.Replace("            ", "#")
                                    strTemp = strTemp.Replace("           ", "#")
                                    strTemp = strTemp.Replace("          ", "#")
                                    strTemp = strTemp.Replace("         ", "#")
                                    strTemp = strTemp.Replace("        ", "#")
                                    strTemp = strTemp.Replace("       ", "#")
                                    strTemp = strTemp.Replace("      ", "#")
                                    strTemp = strTemp.Replace("     ", "#")
                                    strTemp = strTemp.Replace("    ", "#")
                                    strTemp = strTemp.Replace("   ", "#")
                                    strTemp = strTemp.Replace("  ", "#")
                                    strTemp = strTemp.Replace(" ", "#")
                                    Dim strTempBreak As String() = strTemp.Split("#")
                                    For index As Integer = 0 To 6
                                        If clsCommon.myCdbl(strTempBreak(index)) > 0 Then
                                            gvGPItem.Rows(gvGPItem.Rows.Count - 1).Cells(colGPICash).Value = strTempBreak(index)
                                            Exit For
                                        End If
                                    Next

                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If

                    Next
                    clsCommon.ProgressBarPercentHide()
                    loadGatePassBookingCountNumber()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at " & ii + Environment.NewLine + ex.ToString())
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridGPItems()
        gvGPItem.Rows.Clear()
        gvGPItem.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colGPISNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PSNo"
        repoLineNo.Name = colGPIPSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "P File"
        repoLineNo.Name = colGPIFile
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Date"
        repoLineNo.Name = colGPIDate
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Shift"
        repoLineNo.Name = colGPIShift
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Route"
        repoLineNo.Name = colGPIRoute
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booth ID"
        repoLineNo.Name = colGPIBoothID
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Type"
        repoLineNo.Name = colGPIType
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvGPItem.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Cash"
        repoTaxBaseAmt.Name = colGPICash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        repoTaxBaseAmt = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "BookingCountCash"
        repoTaxBaseAmt.Name = colGPIBookingCountCash
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvGPItem.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        gvGPItem.AllowDeleteRow = True
        gvGPItem.AllowAddNewRow = False
        gvGPItem.ShowGroupPanel = False
        gvGPItem.AllowColumnReorder = False
        gvGPItem.AllowRowReorder = False
        gvGPItem.EnableSorting = False
        gvGPItem.EnableFiltering = True
        gvGPItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvGPItem.MasterTemplate.ShowRowHeaderColumn = False
        gvGPItem.TableElement.TableHeaderHeight = 40
    End Sub
    Sub SaveAndPostAgainstGatePass()

        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim trans As SqlTransaction = Nothing
        Try

            If gvGPItem.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                trans = clsDBFuncationality.GetTransactin()

                objCommonVar.CurrentUserCode = CurrentUserCode

                If isGatePassCashExist = True Then
                    BookingSaveDataGatePass_Cash(trans)
                End If


                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                gvGPItem.Columns.Clear()
            Else
                Throw New Exception("No Rows found to save")
            End If
        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message, "Dairy Booking Uploader")
        End Try
    End Sub

    Sub BookingSaveDataGatePass_Cash(ByVal trans As SqlTransaction)

        Dim strcountno As String = ""
        Dim objTr As clsBookingDetailDairySale = Nothing
        Dim obj As clsBookingEntryDairySale = Nothing

        Dim strZone As String = String.Empty
        Dim strRoute As String = String.Empty
        Dim strBooth As String = String.Empty
        Dim LineNo As Integer = 1
        Dim strPriceCode As String = String.Empty
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0
            Dim totalQty As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim TotalCrate As Double = 0
            Dim intCurrInvNo As Integer = 1
            For i As Integer = 0 To gvGPItem.Rows.Count - 1
                If clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPIBookingCountCash).Value) > 0 Then
                    If clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value) > 0 Then
                        j += 1
                        clsCommon.ProgressBarPercentUpdate(j * 100 / gvGPItem.Rows.Count, " Creating  Booking Cash Records Of GatePass Type " & j & " of Total " & gvGPItem.Rows.Count)
                        intCurrInvNo = clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPIBookingCountCash).Value)

                        If clsCommon.CompairString(strcountno, clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBookingCountCash).Value)) <> CompairStringResult.Equal OrElse obj Is Nothing Then

                            LineNo = 1
                            DocuAmount = 0
                            Tax1Amt = 0
                            Tax2Amt = 0
                            TaxBaseAmount = 0
                            TotalCrate = 0
                            obj = New clsBookingEntryDairySale()


                            obj.Document_Date = clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIDate).Value), "dd/MMM/yyyy"))
                            obj.location_code = clsCommon.myCstr(txtLocation.Value)

                            obj.Is_Taxable = 2
                            obj.TRANSACTION_TYPE = ""
                            obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer



                            obj.Booking_Type = "CASH"
                            obj.AgainstGatePass = 1

                            obj.BookingThrough = "Uploader"
                            If clsCommon.CompairString(clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIShift).Value), "MORNING") = CompairStringResult.Equal Then
                                obj.GatePass_Type = "AM"
                            Else
                                obj.GatePass_Type = "PM"
                            End If

                            obj.Uploading_date = clsCommon.GETSERVERDATE(trans)
                            ''for detail table
                            obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value)
                                ' objTr.Cust_Code = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvGPItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        Else
                            ''for detail table
                            'obj.Arr = New List(Of clsBookingDetailDairySale)


                            If clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value) > 0 Then
                                objTr = New clsBookingDetailDairySale()
                                objTr.Line_No = LineNo
                                objTr.Booking_Qty = clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value)
                                'objTr.Cust_Code = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value)
                                objTr.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select cust_code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value) + "'", trans))
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Cust_Code)) <= 0 Then
                                    Throw New Exception("Customer Code " & clsCommon.myCstr(objTr.Cust_Code) & " not exist at row no " & clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPISNo).Value) & ".")
                                End If
                                objTr.Sampling = 0
                                objTr.Loc_Code = clsCommon.myCstr(txtLocation.Value)
                                objTr.Short_Description = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIType).Value)
                                'objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from tspl_item_master where Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'", trans))

                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) <= 0 Then
                                    Throw New Exception("Item not exist for short description " & clsCommon.myCstr(objTr.Short_Description) & " at row no " & clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPISNo).Value) & ".")
                                End If

                                Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(objTr.Cust_Code) & "'", trans))
                                Dim qry As String = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                            " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                            " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                            " where tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and tspl_item_master.Short_Description='" + clsCommon.myCstr(objTr.Short_Description) + "'  "

                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt1.Rows
                                        objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                        objTr.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                                        objTr.Tax_NonTax = clsCommon.myCstr(dr("IsTaxable"))
                                        objTr.FreshAmbient = clsCommon.myCstr(dr("IsFreshAmbient"))

                                    Next
                                End If




                                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"

                                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                    objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                    strPriceCode = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))

                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                        Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                        Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                        Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                    End If
                                End If

                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code " & Environment.NewLine &
                                    " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & Environment.NewLine &
                                    " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from " & Environment.NewLine &
                                    " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objTr.Item_Code + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)" & Environment.NewLine &
                                    " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'" & Environment.NewLine &
                                     " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objTr.Item_Code + "' order by TSPL_SCHEME_MASTER_NEW.Scheme_Code "


                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

                                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                        For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                            objTr.SchemeType = objtrScheme.schm_Type
                                            objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                        Next

                                    End If
                                End If


                                'crate conversion
                                ''gvGPItem.Rows(i).Cells(colCrateQty).Value = CrateQuantity(objTr.Item_Code, objTr.Unit_code, objTr.Booking_Qty, trans)

                                Dim dblTotalCrateRowWise As Double = 0
                                If clsCommon.myLen(clsCommon.myCstr(objTr.Item_Code)) > 0 Then
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_code) & "'", trans))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where 1=1 and TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' ", trans))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_code) & "' ", trans))

                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Booking_Qty) * ItemConvFactor
                                            If DispatchQty >= CrateConvFactor Then
                                                TotalCrate = TotalCrate + Math.Floor(DispatchQty / CrateConvFactor)

                                            End If
                                        Else
                                            Throw New Exception("Please fill conversion factor for this unit at line no." & i + 1 & "" & Environment.NewLine)
                                        End If
                                    End If
                                End If

                                ''end of crate Conversion



                                Dim dt As New DataTable()
                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0
                                Dim dblItemBasicPrice As Double = 0

                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                            " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                            "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                            " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                            " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & objTr.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objTr.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                            ") XXXE WHERE RowNo=1  "
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt.Rows.Count > 0 Then
                                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                                        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
                                    Else
                                        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                    End If

                                    If dblRate = 0 Then
                                        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                    End If

                                    objTr.Item_Rate = clsCommon.myCdbl(dblRate)
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Item_Basic_Rate = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                    objTr.DocumentAmount = Math.Round(clsCommon.myCdbl(objTr.Booking_Qty) * clsCommon.myCdbl(objTr.Item_Rate), 2)


                                    objTr.Price_with_Tax = clsCommon.myCdbl(dblItemBasicPrice)
                                    objTr.Amount_with_Tax = dblItemBasicPrice * clsCommon.myCdbl(objTr.Booking_Qty)
                                    objTr.Booking_Status = 1
                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Rows(0).Item("Item_Price_ID"))
                                    objTr.Price_IdStartDate = clsCommon.myCstr(dt.Rows(0).Item("Start_date"))
                                    objTr.PricePlanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Plan_Code  from TSPL_ITEM_PRICE_PLAN_DETAIL WHERE PLAN_TR_CODE='" & clsCommon.myCstr(dt.Rows(0).Item("Against_Plan_TR_Code")) & "'", trans))

                                Else
                                    Throw New Exception("Please create Price chart for customer " & objTr.Cust_Code & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(objTr.Short_Description) & Environment.NewLine)
                                End If

                                DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(clsCommon.myCdbl(objTr.DocumentAmount), 2)
                                totalQty = totalQty + clsCommon.myCdbl(objTr.Booking_Qty)
                                obj.Arr.Add(objTr)

                                LineNo = LineNo + 1


                            End If

                        End If


                    End If
                    'intCounter += 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < gvGPItem.Rows.Count Then

                    intNextInvNo = clsCommon.myCdbl(gvGPItem.Rows(intCounter + 1).Cells(colGPIBookingCountCash).Value)

                End If
                If intNextInvNo <> 0 Then
                    If Not (intCurrInvNo = intNextInvNo) Then
                        If obj IsNot Nothing Then



                            obj.TotalCrate = TotalCrate
                            obj.SaveData(obj, True, trans)
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DocumentAmount =" & DocuAmount & ", Total_Qty =" & totalQty & " where Document_No ='" & obj.Document_No & "' and Scheme_Item='N'", trans)

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_MATSER set Created_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "',Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.myCDate(obj.Document_Date).AddDays(-1), "dd/MMM/yyyy hh:mm tt") & "' where Document_No ='" & obj.Document_No & "'", trans)


                        End If


                    End If
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
    Sub loadGatePassBookingCountNumber()
        Try
            Dim strZone As String = Nothing
            Dim strRoute As String = Nothing
            Dim strBoothID As String = Nothing
            Dim strShift As String = Nothing
            Dim strDate As String = Nothing
            Dim strFile As String = Nothing
            isGatePassCashExist = False

            Dim BookingCount As Integer = 0

            For i = 0 To gvGPItem.Rows.Count - 1

                If clsCommon.CompairString(strDate, clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIDate).Value)) = CompairStringResult.Equal And clsCommon.CompairString(strFile, clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIFile).Value)) = CompairStringResult.Equal And clsCommon.CompairString(strShift, clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIShift).Value)) = CompairStringResult.Equal And clsCommon.CompairString(strRoute, clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIRoute).Value)) = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(strBoothID), clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value)) = CompairStringResult.Equal Then
                Else
                    BookingCount = BookingCount + 1
                End If
                gvGPItem.Rows(i).Cells(colGPIBookingCountCash).Value = "0"


                If clsCommon.myCdbl(gvGPItem.Rows(i).Cells(colGPICash).Value) > 0 Then
                    gvGPItem.Rows(i).Cells(colGPIBookingCountCash).Value = clsCommon.myCstr(BookingCount)
                    isGatePassCashExist = True
                End If

                strShift = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIShift).Value)
                strBoothID = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIBoothID).Value)
                strRoute = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIRoute).Value)
                strDate = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIDate).Value)
                strFile = clsCommon.myCstr(gvGPItem.Rows(i).Cells(colGPIFile).Value)

            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        If rdbAgainstTruckSheet.IsChecked = True Then
            If isCDExist = True Then
                If gvTSItem.Rows.Count > 0 Then
                    SaveAndPostAgainstTruckSheet_CD_WithoutReference()
                Else
                    clsCommon.MyMessageBoxShow("No Rows found to save")
                End If
            Else
                clsCommon.MyMessageBoxShow("No Rows found of CD type to save")
            End If

        End If
    End Sub
    Sub SaveAndPostAgainstTruckSheet_CD_WithoutReference()
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim trans As SqlTransaction = Nothing
        Try

            If gvTSItem.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                trans = clsDBFuncationality.GetTransactin()

                objCommonVar.CurrentUserCode = CurrentUserCode
                If isCDExist = True Then
                    BookingSaveDataTruckSheet_CD_WithoutReference(trans)
                End If

                clsCommon.ProgressBarPercentHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                gvTSItem.Columns.Clear()
            Else
                Throw New Exception("No Rows found to save")
            End If
        Catch ex As Exception
            Try
                clsCommon.ProgressBarPercentHide()
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message, "Dairy Booking Uploader")
        End Try
    End Sub
End Class