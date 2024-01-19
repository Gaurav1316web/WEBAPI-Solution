''created By Richa Agarwal ERO/16/08/19-000993
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmMCCMaterialSaleUploader
    Inherits FrmMainTranScreen
    Dim AllowManualItemPriceOnMCCSale As Boolean = False
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
    Dim AllowPlandDeptMCCLocation As Boolean = False

    Private Sub frmMCCMaterialSaleUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("MCCSaleDate", "date NULL")
        coll.Add("Location", "varchar(30)  NUll")
        coll.Add("Sub_Location_code", "varchar(30)  NUll")
        coll.Add("Is_CashSale", "char(1) default 'N'")
        coll.Add("Customer", "varchar(30)  NUll")
        coll.Add("VLCCode", "varchar(30)  NUll")
        coll.Add("Item_code", "varchar(30)  NUll")
        coll.Add("Qty", "float NULL")
        coll.Add("UOM", "varchar(30)  NUll")
        coll.Add("rate", "float NULL")
        coll.Add("Amount", "float NULL")
        coll.Add("InvoiceType", "varchar(30)  NUll")
        coll.Add("taxGroup", "varchar(30)  NUll")
        coll.Add("Tax1", "varchar(30)  NUll")
        coll.Add("TAx1rate", "float NULL")
        coll.Add("Tax1Amt", "float NULL")
        coll.Add("Tax2", "varchar(30)  NUll")
        coll.Add("TAx2rate", "float NULL")
        coll.Add("Tax2Amt", "float NULL")
        clsCommonFunctionality.CreateOrAlterTable("Temp_table_MCC_Material_Sale_uploader", coll)


        AllowManualItemPriceOnMCCSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualItemPriceOnMCCSale, clsFixedParameterCode.AllowManualItemPriceOnMCCSale, Nothing)) = 0, False, True)
        Gv1.Visible = True
        btnSaveAndPost.Enabled = True
        AllowPlandDeptMCCLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_Plant_Depot_MCC_typeLocation, clsFixedParameterCode.Allow_Plant_Depot_MCC_typeLocation, Nothing)) = "1", True, False))
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        'Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = Nothing
        btnSaveAndPost.Enabled = True
        If rdbAgainstBulkSale.IsChecked Then
            Dim colImport As String = Nothing
            If AllowPlandDeptMCCLocation Then
                If transportSql.importExcel(Gv1, "BILL TO LOCATION", "SUB LOCATION", "DATE", "CUSTOMER NO", "CUSTOMER NAME", "VLC CODE", "VLC NAME", "ITEM CODE", "ITEM NAME", "QTY", "RATE", "AMOUNT", "Taxable", "UOM", "Cash Sale") Then
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
            Else
                If transportSql.importExcel(Gv1, "BILL TO LOCATION ", "DATE", "CUSTOMER NO", "CUSTOMER NAME", "VLC CODE", "VLC NAME", "ITEM CODE", "ITEM NAME", "QTY", "RATE", "AMOUNT", "Taxable", "UOM") Then
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

        End If
    End Sub
    Sub LoadBlankGrid()

    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()
    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim qry As String = ""
        If rdbAgainstBulkSale.IsChecked Then
            If AllowPlandDeptMCCLocation Then
                qry = "select Location  as [BILL TO LOCATION],Sub_Location_code as [SUB LOCATION], MCCSaleDate as [DATE], Customer as [CUSTOMER NO], '' as [CUSTOMER NAME], VLCCode as [VLC CODE], '' as [VLC NAME], Item_code as [ITEM CODE], '' as [ITEM NAME], Qty as [QTY],UOM AS UOM, rate as [RATE], Amount as [AMOUNT],'' as [Taxable],'' as [Cash Sale] from Temp_table_MCC_Material_Sale_uploader"
            Else
                qry = "  Select Case Location  As [BILL To LOCATION], MCCSaleDate As [DATE], Customer As [CUSTOMER NO], '' as [CUSTOMER NAME], VLCCode as [VLC CODE], '' as [VLC NAME], Item_code as [ITEM CODE], '' as [ITEM NAME], Qty as [QTY],UOM AS UOM, rate as [RATE], Amount as [AMOUNT],'' as [Taxable],'' as [Cash Sale] from Temp_table_MCC_Material_Sale_uploader"
            End If
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"BILL TO LOCATION"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"BILL TO LOCATION"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)

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
            clsCommon.MyMessageBoxShow(Me, "There is no row in grid please select a sheet to import", Me.Text)
        End If
        If ValidatedCount = Gv1.Rows.Count Then
            clsCommon.MyMessageBoxShow(Me, "All Rows are already validated", Me.Text)
            Exit Sub
        End If
        ValidatedCount = 0
        Dim strCellValue
        Dim strSubLocation
        If rdbAgainstBulkSale.IsChecked Then
            For i As Integer = 0 To Gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / Gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & Gv1.Rows.Count)
                ValidateStatus = ""


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Bill To Location Must not be Blank" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where Location_code='" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Bill To Location not found in master" & Environment.NewLine
                End If
                If AllowPlandDeptMCCLocation Then

                    strSubLocation = clsCommon.myCstr(Gv1.Rows(i).Cells("SUB LOCATION").Value)
                    If clsCommon.myLen(strSubLocation) <= 0 Then
                        ValidateStatus = ValidateStatus & "SUB Location Must not be Blank" & Environment.NewLine
                    End If
                    Dim Sub_LocationExist As Integer = clsDBFuncationality.getSingleValue("select count(Location_Code) from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   
 where  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" & strCellValue & "' and Location_Code = '" & strSubLocation & "'")
                    If clsCommon.myCdbl(Sub_LocationExist) < 1 Then
                        ValidateStatus = ValidateStatus & "Sub Location not found in master" & Environment.NewLine
                    End If
                End If

                Dim LocationType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Location_Type,'')  as Location_Type from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(LocationType, "Physical") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Bill To Location Type must be Physical " & Environment.NewLine
                End If

                Dim Is_Sub_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Is_Sub_Location,'')  as Is_Sub_Location from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(Is_Sub_Location, "N") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Bill To Location should not be Sub Location" & Environment.NewLine
                End If

                Dim Is_Section As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Is_Section,'')  as Is_Section from tspl_location_master where Location_code='" & strCellValue & "'"))
                If clsCommon.CompairString(Is_Section, "N") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "Bill To Location should not be Section" & Environment.NewLine
                End If

                If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(i).Cells("VLC CODE").Value)) <= 0 Then
                    ValidateStatus = ValidateStatus & "VLC Code Must not be Blank" & Environment.NewLine
                End If
                Dim CustCode As String = ""
                If AllowPlandDeptMCCLocation Then
                    CustCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_MASTER.Cust_Code from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No   left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where 2=2 and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("VLC CODE").Value) & "'"))
                Else
                    CustCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_MASTER.Cust_Code from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No   left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  and mcc='" & clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value) & "' where 2=2 and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("VLC CODE").Value) & "'"))

                End If
                strCellValue = clsCommon.myCstr(CustCode)
                Gv1.Rows(i).Cells("CUSTOMER NO").Value = clsCommon.myCstr(strCellValue)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer No Must not be Blank" & Environment.NewLine

                End If
                If clsCommon.CompairString(AllowPlandDeptMCCLocation, False) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Customer No not found in master" & Environment.NewLine
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


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & strCellValue & "' and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and coalesce(Product_Type,'')<>'MI' and Item_Type<>'A' and coalesce(Item_used_as,'')='S'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item Code not found in master according to sale criteria." & Environment.NewLine
                End If


                Dim vlcCode As String = Nothing
                If AllowPlandDeptMCCLocation Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(Gv1.Rows(i).Cells("VLC CODE").Value) & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "VLC Code not found in master" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code = '" & clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value) & "'")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Customer No not found in master" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*)   from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No   left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  where 2=2 and CUSTOMER_FORM_TYPE = 'VSP' AND TSPL_CUSTOMER_MASTER.Status = 'N' and TSPL_CUSTOMER_MASTER.Cust_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value) & "'")) <= 0 Then

                        ValidateStatus = ValidateStatus & "VLC Code is Not Active"
                    End If
                Else
                    vlcCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code_VLC_Uploader as [Vlc Code]   from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No   left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  and mcc='" & clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value) & "' where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" & clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value) & "'"))
                    If clsCommon.CompairString(vlcCode, clsCommon.myCstr(Gv1.Rows(i).Cells("VLC CODE").Value)) = CompairStringResult.Equal Then
                    Else
                        '  ValidateStatus = ValidateStatus & "VLC Code is Not correct according to mapping " & Environment.NewLine
                    End If
                End If
                Dim strCashSale As String = clsCommon.myCstr(Gv1.Rows(i).Cells("Cash Sale").Value)
                If clsCommon.myLen(strCashSale) <= 0 Then
                    ValidateStatus = ValidateStatus & "Cash Sale Must not be Blank" & Environment.NewLine
                End If


                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("QTY").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "QTY Must not be Zero or Negative" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("AMOUNT").Value)
                If strCellValue <= 0 Then
                    ValidateStatus = ValidateStatus & "AMOUNT Must not be Zero or Negative" & Environment.NewLine
                End If

                If AllowManualItemPriceOnMCCSale = True Then
                    strCellValue = clsCommon.myCdbl(Gv1.Rows(i).Cells("RATE").Value)
                    If strCellValue <= 0 Then
                        ValidateStatus = ValidateStatus & "RATE Weight Must not be Zero or Negative" & Environment.NewLine
                    End If

                    If (clsCommon.myCdbl(Gv1.Rows(i).Cells("QTY").Value) * clsCommon.myCdbl(Gv1.Rows(i).Cells("RATE").Value)) <> clsCommon.myCdbl(Gv1.Rows(i).Cells("AMOUNT").Value) Then
                        ValidateStatus = ValidateStatus & "AMOUNT is not correct" & Environment.NewLine
                    End If
                Else
                    Dim RateFromPriceChart As Double = GetRateMccSale(clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("UOM").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("Date").Value), Nothing)
                    If RateFromPriceChart <= 0 Then
                        ValidateStatus = ValidateStatus & "create price chart for Item " & clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value) & " " & Environment.NewLine
                    Else
                        Gv1.Rows(i).Cells("RATE").Value = RateFromPriceChart
                        Gv1.Rows(i).Cells("AMOUNT").Value = RateFromPriceChart * Gv1.Rows(i).Cells("QTY").Value
                    End If

                End If


                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("TAXABLE").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "TAXABLE Must be Y or N " & Environment.NewLine
                End If

                If clsCommon.CompairString(strCellValue.ToString.ToUpper, "Y") = CompairStringResult.Equal Or clsCommon.CompairString(strCellValue.ToString.ToUpper, "N") = CompairStringResult.Equal Then
                Else
                    ValidateStatus = ValidateStatus & "TAXABLE Must be Y or N " & Environment.NewLine
                End If

                If clsCommon.CompairString(strCellValue.ToString.ToUpper, "Y") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value) & "' AND IsTaxable=1 ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item Code must be Taxable" & Environment.NewLine
                    End If
                End If
                If clsCommon.CompairString(strCellValue.ToString.ToUpper, "N") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where Item_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value) & "' AND IsTaxable=0 ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item Code must be Non Taxable" & Environment.NewLine
                    End If
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("UOM").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "UOM Must not be blank." & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_uom_detail where Item_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("Item Code").Value) & "' and UOM_Code='" & clsCommon.myCstr(Gv1.Rows(i).Cells("UOM").Value) & "' ")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Please enter correct UOM , this UOM is not for this Item." & Environment.NewLine
                End If

                If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("TAXABLE").Value).ToUpper, "N") = CompairStringResult.Equal Then
                    Gv1.Rows(i).Cells(colTaxGroup).Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value), "S", clsCommon.myCstr(Gv1.Rows(i).Cells("Date").Value))
                    Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(Gv1.Rows(i).Cells(colTaxGroup).Value), "S", clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value))
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        For Each dr As DataRow In dt.Rows
                            Gv1.Rows(i).Cells(coltax1).Value = clsCommon.myCstr(dr("Tax_Code"))
                            Gv1.Rows(i).Cells(coltax1rate).Value = clsCommon.myCdbl(dr("TaxRate"))
                            Gv1.Rows(i).Cells(coltax1Amt).Value = (clsCommon.myCdbl(dr("TaxRate")) * clsCommon.myCdbl(Gv1.Rows(i).Cells("Amount").Value)) / 100

                            Gv1.Rows(i).Cells(coltax2).Value = ""
                            Gv1.Rows(i).Cells(coltax2rate).Value = 0
                            Gv1.Rows(i).Cells(coltax2Amt).Value = 0
                        Next
                    Else
                        ValidateStatus = ValidateStatus & "create tax of exempted type for this location and customer." & Environment.NewLine
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells("TAXABLE").Value).ToUpper, "Y") = CompairStringResult.Equal Then
                    Gv1.Rows(i).Cells(colTaxGroup).Value = clsLocationWiseTax.GetDefaultTaxGroup(clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value), "S", clsCommon.myCstr(Gv1.Rows(i).Cells("Date").Value))
                    Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(Gv1.Rows(i).Cells(colTaxGroup).Value), "S", clsCommon.myCstr(Gv1.Rows(i).Cells("CUSTOMER NO").Value), clsCommon.myCstr(Gv1.Rows(i).Cells("BILL TO LOCATION").Value))
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        Dim j As Integer = 0
                        For Each dr As DataRow In dt.Rows
                            If j = 0 Then
                                Gv1.Rows(i).Cells(coltax1).Value = clsCommon.myCstr(dr("Tax_Code"))
                                Gv1.Rows(i).Cells(coltax1rate).Value = clsCommon.myCdbl(dr("TaxRate"))
                                Gv1.Rows(i).Cells(coltax1Amt).Value = (clsCommon.myCdbl(dr("TaxRate")) * clsCommon.myCdbl(Gv1.Rows(i).Cells("Amount").Value)) / 100
                            ElseIf j = 1 Then
                                Gv1.Rows(i).Cells(coltax2).Value = clsCommon.myCstr(dr("Tax_Code"))
                                Gv1.Rows(i).Cells(coltax2rate).Value = clsCommon.myCdbl(dr("TaxRate"))
                                Gv1.Rows(i).Cells(coltax2Amt).Value = (clsCommon.myCdbl(dr("TaxRate")) * clsCommon.myCdbl(Gv1.Rows(i).Cells("Amount").Value)) / 100
                            End If
                            j = j + 1
                        Next
                    Else
                        ValidateStatus = ValidateStatus & "create tax for this location and customer." & Environment.NewLine
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
                    clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                    btnSaveAndPost.Enabled = False
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
        Dim SubLocationCode As String = String.Empty
        Dim CashSale As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Dim SalePriceCode As String = String.Empty
        Dim ItemCode As String = String.Empty
        Dim UnitCode As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim InvoiceAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            'Dim dt1 As DataTable = Nothing  ERO/22/11/19-001128
            'dt1 = clsDBFuncationality.GetDataTable("Select '' as MCCSaleDate,'' as Location,'' as Customer,'' as VLCCode,'' as Item_code,'' as Qty,'' as UOM,'' as rate,'' as Amount,'' as InvoiceType,'' as taxGroup,'' as Tax1,''as TAx1rate,'' as Tax1Amt,'' as Tax2,'' as TAx2rate,'' as Tax2Amt", trans)
            'dt1.Rows.RemoveAt(0)
            If ValidatedCount > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from Temp_table_MCC_Material_Sale_uploader", trans)
                If rdbAgainstBulkSale.IsChecked Then
                    For Each grow As GridViewRowInfo In Gv1.Rows
                        If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                            ''dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells("Date").Value) + "", "" + clsCommon.myCstr(grow.Cells("BILL TO LOCATION").Value) + "", "" + clsCommon.myCstr(grow.Cells("CUSTOMER NO").Value) + "", "" + clsCommon.myCstr(grow.Cells("VLC CODE").Value) + "", "" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "", "" + clsCommon.myCstr(grow.Cells("QTY").Value) + "", "" + clsCommon.myCstr(grow.Cells("UOM").Value) + "", " " + clsCommon.myCstr(grow.Cells("RATE").Value) + "", "" + clsCommon.myCstr(grow.Cells("AMOUNT").Value) + "", "" + clsCommon.myCstr(grow.Cells("TAXABLE").Value) + "", "" + clsCommon.myCstr(grow.Cells(colTaxGroup).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax1).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax1rate).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax1Amt).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax2).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax2rate).Value) + "", "" + clsCommon.myCstr(grow.Cells(coltax2Amt).Value) + "")
                            clsDBFuncationality.ExecuteNonQuery("Insert into Temp_table_MCC_Material_Sale_uploader values ('" + clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MMM/yyyy") + "', '" + clsCommon.myCstr(grow.Cells("BILL TO LOCATION").Value) + "', '" + clsCommon.myCstr(grow.Cells("CUSTOMER NO").Value) + "', '" + clsCommon.myCstr(grow.Cells("VLC CODE").Value) + "', '" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "', " + clsCommon.myCstr(grow.Cells("QTY").Value) + ", '" + clsCommon.myCstr(grow.Cells("UOM").Value) + "',  " + clsCommon.myCstr(grow.Cells("RATE").Value) + ", " + clsCommon.myCstr(grow.Cells("AMOUNT").Value) + ", '" + clsCommon.myCstr(grow.Cells("TAXABLE").Value) + "', '" + clsCommon.myCstr(grow.Cells(colTaxGroup).Value) + "', '" + clsCommon.myCstr(grow.Cells(coltax1).Value) + "', " + clsCommon.myCstr(grow.Cells(coltax1rate).Value) + ", " + clsCommon.myCstr(grow.Cells(coltax1Amt).Value) + ", '" + clsCommon.myCstr(grow.Cells(coltax2).Value) + "', " + clsCommon.myCstr(grow.Cells(coltax2rate).Value) + ", " + clsCommon.myCstr(grow.Cells(coltax2Amt).Value) + ",'" + clsCommon.myCstr(grow.Cells("SUB LOCATION").Value) + "'" + ",'" + clsCommon.myCstr(grow.Cells("Cash Sale").Value) + "'" + ")", trans)
                        End If
                    Next
                End If
            End If

            Dim dtout As DataTable = Nothing
            'dt1.DefaultView.Sort = "Location,Customer,InvoiceType,MCCSaleDate,Item_code,UOM"
            'dtout = dt1.DefaultView.ToTable()

            dtout = clsDBFuncationality.GetDataTable("Select MCCSaleDate,Location,Customer,MAX(VLCCode) As VLCCode,Item_code,SUM(Qty) As Qty, UOM,max(rate) As rate,sum(Amount) As Amount,InvoiceType,max(taxGroup) As taxGroup,max(Tax1) As Tax1,max(TAx1rate) As TAx1rate,sum(Tax1Amt) As Tax1Amt,max(Tax2) As Tax2,max(TAx2rate) As TAx2rate,sum(Tax2Amt) As Tax2Amt,Sub_Location_code,Is_CashSale from Temp_table_MCC_Material_Sale_uploader GROUP BY Location,Sub_Location_code,Customer,InvoiceType,MCCSaleDate,Item_code,UOM,Is_CashSale order by VLCCode , Is_CashSale", trans)

            dtmain = clsDBFuncationality.GetDataTable("Select '' as SrNo,'' as MCCSaleDate,'' as Location,'' as Customer,'' as VLCCode,'' as Item_code,'' as Qty,'' as UOM,'' as rate,'' as Amount,'' as InvoiceType,'' as taxGroup,'' as Tax1,''as TAx1rate,'' as Tax1Amt,'' as Tax2,'' as TAx2rate,'' as Tax2Amt, '' as Sub_Location_code , '' as Is_CashSale", trans)
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows


                    If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("MCCSaleDate"))) = CompairStringResult.Equal And clsCommon.CompairString(SalePriceCode, clsCommon.myCstr(dr("InvoiceType"))) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Customer"))) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(dr("Location"))) = CompairStringResult.Equal And clsCommon.CompairString(CashSale, clsCommon.myCstr(dr("Is_CashSale"))) = CompairStringResult.Equal Then
                        InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(dr("Amount"))
                    Else
                        CustomerCount = CustomerCount + 1
                        InvoiceAmount = 0
                        InvoiceAmount = clsCommon.myCdbl(dr("Amount"))
                    End If
                    CustomerCode = clsCommon.myCstr(dr("Customer"))
                    LocationCode = clsCommon.myCstr(dr("Location"))
                    SubLocationCode = clsCommon.myCstr(dr("Sub_Location_code"))
                    CashSale = clsCommon.myCstr(dr("Is_CashSale"))
                    SalePriceCode = clsCommon.myCstr(dr("InvoiceType"))
                    strdocdate = clsCommon.myCDate(dr("MCCSaleDate"))

                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("MCCSaleDate")) + "", "" + clsCommon.myCstr(dr("Location")) + "", "" + clsCommon.myCstr(dr("Customer")) + "", "" + clsCommon.myCstr(dr("VLCCode")) + "", "" + clsCommon.myCstr(dr("Item_code")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", "" + clsCommon.myCstr(dr("UOM")) + "", "" + clsCommon.myCstr(dr("rate")) + "", "" + clsCommon.myCstr(dr("Amount")) + "", " " + clsCommon.myCstr(dr("InvoiceType")) + "", "" + clsCommon.myCstr(dr("taxGroup")) + "", "" + clsCommon.myCstr(dr("Tax1")) + "", "" + clsCommon.myCstr(dr("TAx1rate")) + "", "" + clsCommon.myCstr(dr("Tax1Amt")) + "", " " + clsCommon.myCstr(dr("Tax2")) + "", "" + clsCommon.myCstr(dr("TAx2rate")) + "", "" + clsCommon.myCstr(dr("Tax2Amt")) + "", "" + clsCommon.myCstr(dr("Sub_Location_code")) + "", "" + clsCommon.myCstr(dr("Is_CashSale")) + "")
                Next
                'If AllowToSave(False, trans) Then

                'End If
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
        Dim objTr As clsMCCMaterialSaleDetail = Nothing
        Dim obj As clsMCCMaterialSale = Nothing
        Try


            Dim DocuAmount As Double = 0
            Dim Tax1Amt As Double = 0
            Dim Tax2Amt As Double = 0
            Dim TaxBaseAmount As Double = 0

            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            For Each dr As DataRow In dtmain.Rows
                j += 1
                clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating  Invoice Records " & j & " of Total " & dtmain.Rows.Count)
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("SrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("SrNo"))) <> CompairStringResult.Equal Then

                    DocuAmount = 0
                    Tax1Amt = 0
                    Tax2Amt = 0
                    TaxBaseAmount = 0
                    obj = New clsMCCMaterialSale()
                    obj.Sale_Invoice_No = ""
                    obj.Road_Permit_No = ""
                    obj.Invoice_Type = IIf(clsCommon.myCstr(dr("InvoiceType")) = "Y", "T", "N")
                    obj.HeadDisc_Amt = 0
                    obj.HeadDisc_PerAmt = 0
                    obj.Is_CashSale = clsCommon.myCstr(dr("Is_CashSale"))
                    obj.ConvRate = 1
                    obj.Document_Date = clsCommon.myCstr(dr("MCCSaleDate"))
                    obj.Customer_Code = clsCommon.myCstr(dr("Customer"))
                    obj.Customer_Name = clsCustomerMaster.GetName(clsCommon.myCstr(dr("Customer")), trans)
                    obj.Bill_To_Location = clsCommon.myCstr(dr("Location"))
                    If AllowPlandDeptMCCLocation Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(obj.Bill_To_Location) & "'", trans)), "Y") = CompairStringResult.Equal Then
                            obj.Sub_Location_code = clsCommon.myCstr(dr("Sub_Location_code"))
                        End If
                    End If
                    obj.Comments = "Entry created through MCC Material Sale uploader"
                    obj.Tax_Group = clsCommon.myCstr(dr("TaxGroup"))
                    obj.Is_Create_Auto_Invoice = 1

                    obj.Inv_Date = clsCommon.myCstr(dr("MCCSaleDate"))
                    obj.Challan_Date = clsCommon.myCstr(dr("MCCSaleDate"))
                    If clsCommon.CompairString(clsCommon.myCstr(dr("InvoiceType")), "Y") = CompairStringResult.Equal Then
                        obj.Is_Taxable = 1
                    End If
                    obj.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dr("TAX1Rate"))
                    Tax1Amt = Tax1Amt + clsCommon.myCdbl(dr("TAX1Amt"))


                    obj.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dr("TAX2Rate"))
                    Tax2Amt = Tax2Amt + clsCommon.myCdbl(dr("TAX2Amt"))


                    obj.Terms_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code  from tspl_customer_master where CUST_CODE='" & clsCommon.myCstr(dr("Customer")) & "'", trans))

                    Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Due_Date = clsCommon.myCDate(dr("MCCSaleDate")).AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
                    Else
                        obj.Due_Date = clsCommon.myCstr(dr("MCCSaleDate"))
                    End If


                    ''for detail table
                    obj.Arr = New List(Of clsMCCMaterialSaleDetail)
                    objTr = New clsMCCMaterialSaleDetail()

                    Dim Rate_Mcc_Item As Double = GetRateMccSale(clsCommon.myCstr(dr("Location")), clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dr("UOM")), clsCommon.myCstr(dr("MCCSaleDate")), trans)
                    If Rate_Mcc_Item <> clsCommon.myCdbl(dr("Rate")) Then
                        obj.Rate_Status = 2
                    End If

                    objTr.Line_No = 1
                    objTr.Row_Type = "Item"
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(dr("Item_Code")), trans)
                    objTr.Qty = clsCommon.myCdbl(dr("QTY"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UOM"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Rate"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("Rate"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("Rate"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX1 = clsCommon.myCstr(dr("Tax1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("Tax2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt")) + clsCommon.myCdbl(dr("Amount"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.Conv_Factor = 1
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("UOM"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("QTY"))
                    objTr.Item_Weight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Weight_Value  from tspl_item_master where Item_Code='" & objTr.Item_Code & "' ", trans))

                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.Amount, 2)
                    obj.Arr.Add(objTr)
                Else
                    objTr = New clsMCCMaterialSaleDetail()

                    objTr.Line_No = 1
                    Dim Rate_Mcc_Item As Double = GetRateMccSale(clsCommon.myCstr(dr("Location")), clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dr("UOM")), clsCommon.myCstr(dr("MCCSaleDate")), trans)
                    If Rate_Mcc_Item <> clsCommon.myCdbl(dr("Rate")) Then
                        obj.Rate_Status = 2
                    End If
                    objTr.Row_Type = "Item"
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(dr("Item_Code")), trans)
                    objTr.Qty = clsCommon.myCdbl(dr("QTY"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UOM"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Rate"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("Rate"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("Rate"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX1 = clsCommon.myCstr(dr("Tax1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("Tax2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("Amount"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("TAX1Amt")) + clsCommon.myCdbl(dr("TAX2Amt")) + clsCommon.myCdbl(dr("Amount"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("QTY"))
                    objTr.Conv_Factor = 1
                    objTr.OrgUnit_code = clsCommon.myCstr(dr("UOM"))
                    objTr.Item_Weight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Weight_Value  from tspl_item_master where Item_Code='" & objTr.Item_Code & "' ", trans))

                    Tax1Amt = Tax1Amt + clsCommon.myCdbl(dr("TAX1Amt"))
                    Tax2Amt = Tax2Amt + clsCommon.myCdbl(dr("TAX2Amt"))

                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.Amount, 2)
                    obj.Arr.Add(objTr)
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("SrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    obj.Discount_Base = clsCommon.myCdbl(DocuAmount)
                    obj.Amount_Less_Discount = clsCommon.myCdbl(DocuAmount)
                    obj.TAX1_Amt = clsCommon.myCdbl(Tax1Amt)
                    obj.TAX2_Amt = clsCommon.myCdbl(Tax2Amt)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(DocuAmount)
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(DocuAmount)
                    obj.Total_Tax_Amt = clsCommon.myCdbl(obj.TAX1_Amt) + clsCommon.myCdbl(obj.TAX2_Amt)

                    DocuAmount = DocuAmount + obj.Total_Tax_Amt
                    ' If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                    obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2)
                    obj.Total_Amt = clsCommon.myCdbl(DocuAmount)
                    'Else
                    'obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2)
                    'obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    'End If

                    ' objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_SCRAPINVOICE_HEAD", "Created_By", obj.Bill_To_Location, "Loc_Code", trans)


                    obj.SaveData(obj, True, trans)
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

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
    End Sub
End Class