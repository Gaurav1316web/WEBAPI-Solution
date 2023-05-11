'-28/11/2012-12:47PM--Updation By--Pankaj Kumar--Added New Column [] in TSPL_SALE_RETURN_INTER_HEAD And TSPL_SALE_RETURN_INTER_DETAIL Ans LineNo In Detail----
'by vipin for check post status on update
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO


Public Class frmSaleReturnInter
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Public clicked As Boolean =False 

#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Const ReportID As String = "SRInter"
    Public strLoadOutNo As String
    Dim conversionnumber As Decimal
    Dim emptyvalue123 As Decimal = 0
    Dim checkpost As String = "N"
    Dim Invoiceno As String = String.Empty
    Dim TaxGroupvalue As String = String.Empty

    Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim customdatarow As DataRow = Nothing
    Dim startdate As String
    Dim mrp As String
    Dim strCustAccount As String
    Dim sql As String
    Dim dr As DataTable
    Dim ds As New DataSet

    Dim tableName As String = "TSPL_SHIPMENT_MASTER"
    Dim tableCode As String = "Shipment_No"
    Dim codePrefix As String = "SHPNO"
    Dim noOfZero As Integer = 3

    Dim l1User, l2User, l3User, l4User, l5User As String
    Private ttlItemShpQtyForCheck As Decimal = 0
    Dim btntooltip As ToolTip = New ToolTip()

    Const ColICode As String = "itemCode"
    Const colSchemeItem As String = "schemeItem"
    Const colSampleItem As String = "sampleItem"
    Const colPromoSchemeItem As String = "promoSchemeItem"
    Const collocation As String = "location"

    Const colActualRetrunQty As String = "colActualRetrunQty"
    Const colLeakQty As String = "colLeakQty"
    Const colBurstQty As String = "colBurstQty"
    Const colShortQty As String = "colShortQty"


    Const colShippedQty As String = "shippedQty"

    Const colUnitCode As String = "unitCode"
    Const colMRP As String = "mrp"
    Const colSchemeApplicable As String = "schemeApplicable"
    Const colPromoSchemeApplicable As String = "promoSchemeApplicable"
    Const colSchemeDiscountApplicable As String = "schemeDiscountApplicable"
    Const colitemNetAmount As String = "itemNetAmount"
    Const colBatchNumber As String = "batchnumber"
    Const colFromSchemeCode As String = "fromSchemeCode"
    Const colEmptyValue As String = "emptyValue"
    Const colEmptyValueBottle As String = "emptyValueBottle"
    Const colEmptyValueShell As String = "emptyValueShell"
    Const colTotalBasicAmount As String = "totalBasicAmount"
    Const colTotalNetAmount As String = "totalNetAmount"
    Const colTotalTPT As String = "totalTPT"
    Const colTotalCustDiscount As String = "totalCustDiscount"
    Const colBasicAmount As String = "basicAmount"
    Const colTotalItemAmount As String = "totalItemAmount"
    Const colPriceDate As String = "pricedate"
    Const colTotalMRP As String = "totalMRP"
    Const colTotalTaxAmount As String = "totaltaxamount"
    Const colTaxamount As String = "taxamount"
    Const colSchemeCodeItem As String = "schemeCodeItem"
    Const colMainItem As String = "mainitem"
    Const colCheckvalue As String = "checkvalue"
    Const colDiscountCode As String = "COLDISCOUNTCODE"
    Const ColCustDisNoTax As String = "COLCUSTDISNOTAX"
    Const ColTargetDisAmt As String = "COLTARGETDISAMT"
    Const ColPriceToShow As String = "PRICETOSHOW"
    Const colPriceDateActual As String = "PriceDateActual"
    Const colAbatementRate As String = "colAbatementRate"
    Const colTAssessibleAmount As String = "assessibleAmount"
    Const colTTaxAmount As String = "taxAmount"
    Const colTTaxRate As String = "taxRate"
    Const colTBasicAmount As String = "basicAmount"
    Const colAssess1 As String = "assess1"
    Const colAssess2 As String = "assess2"
    Const colAssess3 As String = "assess3"
    Const colAssess4 As String = "assess4"
    Const colAssess5 As String = "assess5"
    Const colAssess6 As String = "assess6"


    Const colTTaxAutCode As String = "taxAuthority"
    Const ColTaxDescription As String = "description"
    Const ColTaxTaxRate As String = "taxRate"
    Const colTBaseAmt As String = "basicAmount"
    Const ColTaxAssessibleAmount As String = "assessibleAmount"
    Const colTTaxAmt As String = "taxAmount"
    Const ColTaxTaxable As String = "taxable"
    Const ColTaxSurtax As String = "surtax"
    Const ColTaxSurtaxCode As String = "surtaxCode"
    Const ColTaxiItemAssess As String = "itemAssess"
    Const ColTaxItemTaxAmt As String = "itemTaxAmt"
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SaleReturnInterCompany)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnAdd.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Public Sub SetLength()
        txtDocumentNo.MyMaxLength = 30
        txtcustomerinvoiceno.MaxLength = 30
        txtRemarks.MaxLength = 100
        txtDesc.MaxLength = 100
        txtRef.MaxLength = 100
    End Sub

    Private Sub FrmShipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")

        txtshellqty.Text = 0
        gvLoadOut.Columns.Add("batchnumber", "Batch No")
        gvLoadOut.Columns("batchnumber").IsVisible = False
        gvLoadOut.Columns("batchnumber").ReadOnly = True

        Dim repoDiscCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDiscCode.FormatString = ""
        repoDiscCode.HeaderText = "Discount Code"
        repoDiscCode.Name = colDiscountCode
        repoDiscCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDiscCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDiscCode.Width = 100
        gvLoadOut.MasterTemplate.Columns.Add(repoDiscCode)

        Dim repoCustDisNoTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDisNoTax.FormatString = ""
        repoCustDisNoTax.HeaderText = "Cust Discount Without Tax"
        repoCustDisNoTax.Name = ColCustDisNoTax
        repoCustDisNoTax.Width = 80
        repoCustDisNoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDisNoTax.ReadOnly = True
        gvLoadOut.MasterTemplate.Columns.Add(repoCustDisNoTax)

        Dim repoTargetDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTargetDisAmt.FormatString = ""
        repoTargetDisAmt.HeaderText = "Target Discount Amount"
        repoTargetDisAmt.Name = ColTargetDisAmt
        repoTargetDisAmt.Width = 80
        repoTargetDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTargetDisAmt.ReadOnly = True
        gvLoadOut.MasterTemplate.Columns.Add(repoTargetDisAmt)


        
        Dim repoPriceToShow As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPriceToShow.FormatString = ""
        repoPriceToShow.HeaderText = "Price"
        repoPriceToShow.Name = ColPriceToShow
        repoPriceToShow.Width = 80
        repoPriceToShow.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPriceToShow.ReadOnly = True
        gvLoadOut.MasterTemplate.Columns.Add(repoPriceToShow)

        Dim repoPriceDateActual As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDateActual.Format = DateTimePickerFormat.Custom
        repoPriceDateActual.CustomFormat = "dd-MM-yyyy"
        repoPriceDateActual.HeaderText = "Price Date Actual"
        repoPriceDateActual.FormatString = "{0:d}"
        repoPriceDateActual.Name = colPriceDateActual
        repoPriceDateActual.WrapText = True
        repoPriceDateActual.ReadOnly = True
        repoPriceDateActual.IsVisible = False
        gvLoadOut.MasterTemplate.Columns.Add(repoPriceDateActual)

        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.FormatString = ""
        repoAbatementRate.HeaderText = "Abatement Rate"
        repoAbatementRate.Name = colAbatementRate
        repoAbatementRate.Width = 80
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAbatementRate.ReadOnly = True
        repoAbatementRate.IsVisible = False
        gvLoadOut.MasterTemplate.Columns.Add(repoAbatementRate)


        handlers()
        resetForm()
        ddlModeofTransport.Text = "By Road"
        pageLoadOut.IsContentVisible = True
        
        gvTaxDetails.AllowColumnReorder = False
        gvTaxDetails.AllowRowReorder = False
        gvTaxDetails.EnableSorting = False


        Dim lds As DataSet = connectSql.RunSQLReturnDS(clsERPFuncationality.UserAvailableLocationQuery)
        If lds.Tables(0).Rows.Count = 1 Then
            fndLocation.Value = lds.Tables(0).Rows(0)("Code").ToString()
        End If
        btntooltip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnClose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        
        txtShipmentTotal.Text = 0
        'gvLoadOut.AllowColumnChooser = True
        ReStoreGridLayout()
        gvLoadOut.AllowRowReorder = False
        gvLoadOut.EnableSorting = False
        pvLoadOut.SelectedPage = pageLoadOut
        resetdata()

        If clsCommon.myLen(strLoadOutNo) > 0 Then
            txtDocumentNo.Value = strLoadOutNo
            LoadData(strLoadOutNo, NavigatorType.Current)
        End If
        gvTaxDetails.AllowColumnChooser = True
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvLoadOut.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvLoadOut.Columns.Count - 1 Step ii + 1
                        gvLoadOut.Columns(ii).IsVisible = False
                        gvLoadOut.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvLoadOut.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub handlers()
        AddHandler fndRouteNo.txtValue.TextChanged, AddressOf fndRouteNo_TextChanged
        AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
        AddHandler fndVehicleCode.txtValue.TextChanged, AddressOf fndVehicleCode_TextChanged
        AddHandler txtAdditionalCharges.KeyPress, AddressOf keyPress1
        AddHandler txtDesc.KeyPress, AddressOf keyPress1
        AddHandler txtCustDisc.KeyPress, AddressOf keyPress1
        AddHandler txtFreight.KeyPress, AddressOf keyPress1
        AddHandler txtOtherCharges.KeyPress, AddressOf keyPress1
        AddHandler txtRef.KeyPress, AddressOf keyPress1
        AddHandler txtRemarks.KeyPress, AddressOf keyPress1
        AddHandler txtShipmentAmt.KeyPress, AddressOf keyPress1
        AddHandler txtShipmentTotal.KeyPress, AddressOf keyPress1
        AddHandler txtTotalTaxAmount.KeyPress, AddressOf keyPress1
        AddHandler txtTripNo.KeyPress, AddressOf NumericKeyPress
        AddHandler txtKMReading.KeyPress, AddressOf NumericKeyPress
        AddHandler fndSalesman.txtValue.TextChanged, AddressOf fndSalesman_TextChanged
        AddHandler fndemployeecode.txtValue.TextChanged, AddressOf EmployeeCode_TextChanged
        AddHandler fndVehicleCode.txtValue.Leave, AddressOf fndVehicleCode_Leave
    End Sub

    Private Sub fndVehicleCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndVehicleCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndVehicleCode.Load
        fndVehicleCode.ConnectionString = connectSql.SqlCon()
        fndVehicleCode.Query = "Select Vehicle_Id as 'Vehicle Code',Number,Description,Model from TSPL_VEHICLE_MASTER ORDER BY Number"
        fndVehicleCode.ValueToSelect = "Vehicle Code"
        fndVehicleCode.ValueToSelect1 = "Number"
        fndVehicleCode.Caption = "Vhicles"
    End Sub

    Private Sub fndRouteNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndRouteNo.Load
        fndRouteNo.ConnectionString = connectSql.SqlCon()
        fndRouteNo.Query = "Select Route_No as 'Route No',Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER ORDER BY Route_No"
        fndRouteNo.ValueToSelect = "Route No"
        fndRouteNo.ValueToSelect1 = "Description"
        fndRouteNo.Caption = "Routes"
    End Sub

    Private Sub fndSalesman_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndSalesman.Load
        fndSalesman.ConnectionString = connectSql.SqlCon()
        fndSalesman.Query = "SELECT Emp_Code as 'Salesman Code',Emp_Name as 'Name' from TSPL_EMPLOYEE_MASTER WHERE Emp_Type='SalesMan' ORDER BY  Emp_Name"
        fndSalesman.ValueToSelect = "Salesman Code"
        fndSalesman.ValueToSelect1 = "Name"
        fndSalesman.Caption = "Salesman"
    End Sub

    Private Sub fndTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndTaxGroup.Load
        fndTaxGroup.ConnectionString = connectSql.SqlCon()
        fndTaxGroup.Query = clsERPFuncationality.UserAvailableTaxGroup + " AND M.Tax_Group_Type='S'"
        fndTaxGroup.ValueToSelect = "Code"
        fndTaxGroup.ValueToSelect1 = "Description"
        fndTaxGroup.Caption = "Tax Groups"
    End Sub

    Private Sub fndPaymentTerms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndPaymentTerms.Load
        sql = "SELECT Terms_Code as 'Payment Terms', Terms_Desc as Description, No_Days as 'No of Days' FROM TSPL_TERMS_MASTER ORDER BY Terms_Code"
        fndPaymentTerms.ConnectionString = connectSql.SqlCon()
        fndPaymentTerms.Query = sql
        fndPaymentTerms.ValueToSelect = "Payment Terms"
        fndPaymentTerms.ValueToSelect1 = "Description"
        fndPaymentTerms.Caption = "Payment Terms"
    End Sub

    Private Sub fndSalesman_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sql = "Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE Emp_Code='" + fndSalesman.txtValue.Text + "'"
        txtSalesMan.Text = connectSql.RunScalar(sql)
    End Sub

    Private Sub fndCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndRouteNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sql = "Select Route_Desc,Employee_Code from TSPL_ROUTE_MASTER where Route_No='" + fndRouteNo.txtValue.Text + "'"
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
            txtRouteDesc.Text = dr1.Rows(0)(0).ToString()
            fndSalesman.txtValue.Text = dr1.Rows(0)(1).ToString()
        Else
            txtRouteDesc.Text = String.Empty
        End If
    End Sub

    Private Sub fndTaxGroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetTaxGroup()
    End Sub

    Private Sub SetTaxGroup()
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
        ds = RunSQLReturnDS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvTaxDetails.DataSource = ds.Tables(0)
            ''fndLocation_TextChanged(Me, New EventArgs())
        Else
            resetFNDTaxGroup()
        End If
        Dim mrp As Decimal = 0.0
        Dim basic As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim totalAssessibleAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
            For Each dataRowInfo As GridViewRowInfo In gvLoadOut.Rows
                ' If clsCommon.myCdbl(dataRowInfo.Cells("shippedqty").Value) <> 0 Then
                SnDUtility.calculateTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails)
                If dataRowInfo.Cells("schemeItem").Value = "Yes" Or dataRowInfo.Cells("sampleItem").Value = "Yes" Or dataRowInfo.Cells("promoSchemeItem").Value = "Yes" Then
                    For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                        sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                        If connectSql.RunScalar(sql) = "N" Then
                            gr.Cells(2).Value = 0
                            gr.Cells(5).Value = 0
                        End If
                    Next
                End If
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
                dataRowInfo.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells("taxAmount").Value

                dataRowInfo.Cells("taxAmount").Value = Math.Round(SnDUtility.calculateItemTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails), 4, MidpointRounding.ToEven)
                dataRowInfo.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("shippedQty").Value) * clsCommon.myCdbl(dataRowInfo.Cells("taxAmount").Value), 0)
                dataRowInfo.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("totalNetAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTPT").Value), 0)
                'End If

            Next
        Next
        Dim taxAmt As Decimal = 0
        For Each gr As GridViewRowInfo In gvLoadOut.Rows
            'mrp = mrp + clsCommon.myCdbl(gr.Cells("totalMRP").Value)
            netAmount = netAmount + clsCommon.myCdbl(gr.Cells("totalNetAmount").Value)
        Next
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalAssessibleAmt = 0
            taxAmt = 0
            For Each gr1 As GridViewRowInfo In gvLoadOut.Rows
                If gr1.Cells("schemeItem").Value = "No" And gr1.Cells("sampleItem").Value = "No" And gr1.Cells("promoSchemeItem").Value = "No" Then
                    totalAssessibleAmt = totalAssessibleAmt + clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr1.Cells("shippedQty").Value)
                    taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells("tax" + (gr.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(gr1.Cells("shippedQty").Value)
                End If
            Next
            If totalAssessibleAmt = 0 AndAlso taxAmt = 0 Then
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells("taxAmount").Value = taxAmt
            End If
            If totalAssessibleAmt <> 0 Then
                gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / totalAssessibleAmt, 0)
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells("taxAmount").Value = taxAmt
            End If
        Next
        txtShipmentTotal.Text = netAmount
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalTax = totalTax + Math.Round(clsCommon.myCdbl(gr.Cells(5).Value), 2)
        Next
        txtTotalTaxAmount.Text = totalTax
        totalAmounts()
    End Sub

    Private Sub SetTaxGroup(ByVal taxgroup As String)
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + taxgroup + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
        ds = RunSQLReturnDS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvTaxDetails.DataSource = ds.Tables(0)
            ''fndLocation_TextChanged(Me, New EventArgs())
        Else
            resetFNDTaxGroup()
        End If
        Dim mrp As Decimal = 0.0
        Dim basic As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim totalAssessibleAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
            For Each dataRowInfo As GridViewRowInfo In gvLoadOut.Rows
                ' If clsCommon.myCdbl(dataRowInfo.Cells("shippedqty").Value) <> 0 Then
                SnDUtility.calculateTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails)
                If dataRowInfo.Cells("schemeItem").Value = "Yes" Or dataRowInfo.Cells("sampleItem").Value = "Yes" Or dataRowInfo.Cells("promoSchemeItem").Value = "Yes" Then
                    For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                        sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                        If connectSql.RunScalar(sql) = "N" Then
                            gr.Cells(2).Value = 0
                            gr.Cells(5).Value = 0
                        End If
                    Next
                End If
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
                dataRowInfo.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells("taxAmount").Value

                dataRowInfo.Cells("taxAmount").Value = Math.Round(SnDUtility.calculateItemTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails), 4, MidpointRounding.ToEven)
                dataRowInfo.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("shippedQty").Value) * clsCommon.myCdbl(dataRowInfo.Cells("taxAmount").Value), 0)
                dataRowInfo.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("totalNetAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTPT").Value), 0)
                'End If

            Next
        Next
        Dim taxAmt As Decimal = 0
        For Each gr As GridViewRowInfo In gvLoadOut.Rows
            'mrp = mrp + clsCommon.myCdbl(gr.Cells("totalMRP").Value)
            netAmount = netAmount + clsCommon.myCdbl(gr.Cells("totalNetAmount").Value)
        Next
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalAssessibleAmt = 0
            taxAmt = 0
            For Each gr1 As GridViewRowInfo In gvLoadOut.Rows
                If gr1.Cells("schemeItem").Value = "No" And gr1.Cells("sampleItem").Value = "No" And gr1.Cells("promoSchemeItem").Value = "No" Then
                    totalAssessibleAmt = totalAssessibleAmt + clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr1.Cells("shippedQty").Value)
                    taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells("tax" + (gr.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(gr1.Cells("shippedQty").Value)
                End If
            Next
            If totalAssessibleAmt = 0 AndAlso taxAmt = 0 Then
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells("taxAmount").Value = taxAmt
            End If
            If totalAssessibleAmt <> 0 Then
                gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / totalAssessibleAmt, 0)
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells("taxAmount").Value = taxAmt
            End If
        Next
        txtShipmentTotal.Text = netAmount
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalTax = totalTax + Math.Round(clsCommon.myCdbl(gr.Cells(5).Value), 2)
        Next
        txtTotalTaxAmount.Text = totalTax
        totalAmounts()
    End Sub

    Private Sub CalculateTaxratvalue()
        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
            For Each dataRowInfo As GridViewRowInfo In gvLoadOut.Rows
                If clsCommon.myCdbl(dataRowInfo.Cells("shippedqty").Value) = 0 Then
                    SnDUtility.calculateTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails)
                    If dataRowInfo.Cells("schemeItem").Value = "Yes" Or dataRowInfo.Cells("sampleItem").Value = "Yes" Or dataRowInfo.Cells("promoSchemeItem").Value = "Yes" Then
                        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                            If connectSql.RunScalar(sql) = "N" Then
                                gr.Cells(2).Value = 0
                                gr.Cells(5).Value = 0
                            End If
                        Next
                    End If
                    dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
                    dataRowInfo.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
                    dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells("taxAmount").Value

                    dataRowInfo.Cells("taxAmount").Value = Math.Round(SnDUtility.calculateItemTax(clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value) * abatement(dataRowInfo) / 100, clsCommon.myCdbl(dataRowInfo.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails), 4, MidpointRounding.ToEven)
                    dataRowInfo.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("shippedQty").Value) * clsCommon.myCdbl(dataRowInfo.Cells("taxAmount").Value), 0)
                    dataRowInfo.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("totalNetAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(dataRowInfo.Cells("totalTPT").Value), 0)
                End If
            Next
        Next
    End Sub

    Private Sub fndVehicleCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + fndVehicleCode.txtValue.Text + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            txtVhicleNo.Text = dr.Rows(0)(0).ToString()
        Else
            txtVhicleNo.Text = String.Empty
        End If
    End Sub

    Private Sub EmployeeCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtempname.Text = connectSql.RunScalar("select isnull(Emp_Name, '')  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + fndemployeecode.txtValue.Text + "'")
    End Sub

    Private Sub addCashDiscountScheme(ByVal grow As GridViewRowInfo)
        '  RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
        If grow.Cells("schemeDiscountApplicable").Value = "No" Then
            grow.Cells("itemNetAmount").Value = clsCommon.myCdbl(grow.Cells("basicAmount").Value) - clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value)
            grow.Cells("totalNetAmount").Value = 0 'Math.Round(clsCommon.myCdbl(grow.Cells("itemNetAmount").Value) * clsCommon.myCdbl(grow.Cells("itemNetAmount").Value), 4)
            grow.Cells("discountAmount").Value = "0"
            grow.Cells("totalDiscountAmount").Value = "0"
            grow.Cells("schemeCodeDiscount").Value = String.Empty
        ElseIf grow.Cells("schemeDiscountApplicable").Value = "Yes" Then
            sql = "SELECT Scheme_Code,Amount,Main_Item_Qty FROM TSPL_SCHEME_MASTER " & _
                  " WHERE  (Scheme_Type = 'C') AND (Main_Item_Code = '" + grow.Cells("itemCode").Value + "') AND (Start_Date <='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "') AND (End_Date >='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "' OR End_Date is NULL ) AND (Main_Item_Qty <= '" + grow.Cells("shippedQty").Value + "') AND (Main_Item_Uom = '" + grow.Cells("unitCode").Value + "' AND (MRP = '" + grow.Cells("mrp").Value + "') AND (Item_Basic_Price = '" + grow.Cells("transferBasicAmount").Value + "') AND Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + fndCustomer.Value + "'))"
            Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

                Dim mode As Decimal = clsCommon.myCdbl(grow.Cells("shippedQty").Value) Mod clsCommon.myCdbl(dr1.Rows(0)(2).ToString())
                Dim disRatio As Integer = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - mode) / clsCommon.myCdbl(dr1.Rows(0)(2).ToString())
                grow.Cells("schemeCodeDiscount").Value = dr1.Rows(0)(0).ToString
                If clsCommon.myCdbl(dr1.Rows(0)(1).ToString()) < 0 Then
                    grow.Cells("discountAmount").Value = (clsCommon.myCdbl(grow.Cells("basicAmount").Value) * Math.Abs(clsCommon.myCdbl(dr1.Rows(0)(1).ToString())) / 100)
                Else
                    grow.Cells("discountAmount").Value = clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                End If
                grow.Cells("totalDiscountAmount").Value = disRatio * clsCommon.myCdbl(grow.Cells("discountAmount").Value)
                grow.Cells("itemNetAmount").Value = clsCommon.myCdbl(grow.Cells("basicAmount").Value) - clsCommon.myCdbl(grow.Cells("discountAmount").Value) - clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value)

            Else
                common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                grow.Cells("schemeDiscountApplicable").Value = "No"
            End If
        End If
        loadoutCellValueChange(grow)
    End Sub

    Private Sub loadoutCellValueChange(ByVal grow As GridViewRowInfo)
        Dim IntRoundOff As Integer = 4
        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + grow.Cells("itemCode").Value + "' AND " & _
                    " Uom_Code='" + grow.Cells("unitCode").Value + "'"
        Dim convertFact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql))
        If Not convertFact = 0 Then
            Dim shells As Integer
            Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells("shippedQty").Value) Mod convertFact
            If bottles = 0 Then
                shells = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - bottles) / convertFact
                grow.Cells("emptyValue").Value = Math.Round(clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells("emptyValueShell").Value) * shells + clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value) * bottles, 2)
            Else
                shells = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - bottles) / convertFact + 1
                'grow.Cells("emptyValue").Value = Math.Round(clsCommon.myCdbl()

                grow.Cells("emptyValue").Value = Math.Round(clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells("emptyValueShell").Value) * shells + clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value) * bottles, 2)
            End If
            If bottles = 0 And shells = 0 Then
                grow.Cells("emptyValue").Value = clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value) + clsCommon.myCdbl(grow.Cells("emptyValueShell").Value)
            End If
        End If
        If convertFact = 1 Then
            IntRoundOff = 2
        End If
        grow.Cells("totalCustDiscount").Value = Math.Round(clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value), 2)
        grow.Cells("taxAmount").Value = SnDUtility.calculateItemTax(itemAssessibleAmt(grow), clsCommon.myCdbl(grow.Cells("itemNetAmount").Value), fndLocation.Value, gvLoadOut, gvTaxDetails, 0)
        grow.Cells("taxAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("tax1Amt").Value) + clsCommon.myCdbl(grow.Cells("tax2Amt").Value) + clsCommon.myCdbl(grow.Cells("tax3Amt").Value) + clsCommon.myCdbl(grow.Cells("tax4Amt").Value) + clsCommon.myCdbl(grow.Cells("tax5Amt").Value) + clsCommon.myCdbl(grow.Cells("tax6Amt").Value), 4, MidpointRounding.ToEven)
        grow.Cells("totalMRP").Value = Math.Round(clsCommon.myCdbl(grow.Cells("mrp").Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value), 2)
        If ((clsCommon.CompairString(clsCommon.myCstr(grow.Cells("schemeItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("sampleItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("promoSchemeItem").Value), "Yes") = CompairStringResult.Equal)) Then
            grow.Cells("totalBasicAmount").Value = 0

        Else
            grow.Cells("totalBasicAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("basicAmount").Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value), 2)

        End If
        grow.Cells("totalTaxAmount").Value = clsCommon.myCdbl(grow.Cells("taxAmount").Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value)
        grow.Cells("totalNetAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("itemNetAmount").Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value), 2)
        If ((clsCommon.CompairString(clsCommon.myCstr(grow.Cells("schemeItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("sampleItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("promoSchemeItem").Value), "Yes") = CompairStringResult.Equal)) Then
            grow.Cells("totalTPT").Value = 0
        Else
            If convertFact = 1 Then
                grow.Cells("totalTPT").Value = Math.Round(clsCommon.myCdbl(grow.Cells("tpt").Value) * clsCommon.myCdbl(grow.Cells("shippedQty").Value), 2)
            Else
                Dim fullbottlcase As Decimal = Math.Ceiling(clsCommon.myCdbl(grow.Cells("shippedQty").Value) / convertFact)
                Dim tptdr As DataTable
                Dim tpt As String = Nothing
                Dim tptcheck As String = "N"
                tptdr = clsDBFuncationality.GetDataTable("SELECT  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10   FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' AND Item_Basic_Net ='" + Convert.ToString(grow.Cells("mrp").Value * convertFact) + "' AND Tax_group = '" + fndTaxGroup.txtValue.Text + "' and UOM = 'FC'")
                If tptdr IsNot Nothing AndAlso tptdr.Rows.Count > 0 Then
                    For j As Integer = 1 To 10
                        Dim Price_Amount As String = "Price_Amount" + j.ToString()
                        Dim Price_Comp As String = "Price_Comp" + j.ToString()
                        tptcheck = connectSql.RunScalar("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tptdr.Rows(0)(Price_Comp)) + "'")
                        If tptcheck = "Y" Then
                            tpt = Convert.ToString(tptdr.Rows(0)(Price_Amount))
                            Exit For
                        End If
                    Next
                End If
                Dim tptval As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells("tpt").Value) * convertFact, 0)
                grow.Cells("totalTPT").Value = Math.Round(clsCommon.myCdbl(grow.Cells("shippedQty").Value) / convertFact * clsCommon.myCdbl(tpt), 2)
            End If
        End If
        grow.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("totalNetAmount").Value) + clsCommon.myCdbl(grow.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(grow.Cells("totalTPT").Value), 2)
        Dim netAmount As Decimal = totalNetAmount()
        txtShipmentTotal.Text = clsCommon.myFormat(totalBasicAmt())
        SnDUtility.calculateTax(totalAssessibleAmt(), netAmount, fndLocation.Value, gvLoadOut, gvTaxDetails)
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
        Next
        Dim ttlCustDiscount As Decimal = 0
        For Each gro As GridViewRowInfo In gvLoadOut.Rows
            ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells("totalCustDiscount").Value)
        Next
        txtCustDisc.Text = ttlCustDiscount
        txtTotalTaxAmount.Text = totalTax
        txtShipmentAmt.Text = netAmount + totalTax + totalTransport()
    End Sub

    Private Sub gvLoadOut1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLoadOut.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gvLoadOut.Columns(colActualRetrunQty) OrElse e.Column Is gvLoadOut.Columns(colLeakQty) OrElse e.Column Is gvLoadOut.Columns(colBurstQty) OrElse e.Column Is gvLoadOut.Columns(colShortQty) Then
                    gvLoadOut.CurrentRow.Cells(colShippedQty).Value = clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells(colActualRetrunQty).Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells(colShortQty).Value)
                End If
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvLoadOut.Columns(ColICode) OrElse e.Column Is gvLoadOut.Columns(colSchemeApplicable) OrElse e.Column Is gvLoadOut.Columns(collocation) OrElse e.Column Is gvLoadOut.Columns(colSchemeDiscountApplicable) OrElse e.Column Is gvLoadOut.Columns(colitemNetAmount) OrElse e.Column Is gvLoadOut.Columns(colShippedQty) OrElse e.Column Is gvLoadOut.Columns(colMainItem) OrElse e.Column Is gvLoadOut.Columns(colDiscountCode) OrElse e.Column Is gvLoadOut.Columns(colTotalTPT) Then
                        If e.Column Is gvLoadOut.Columns(ColICode) AndAlso gvLoadOut.CurrentRow.Cells(ColICode).Value <> String.Empty Then
                            Dim qry As String = "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM View_TSPL_SHIPMENT_ITEMS  Where Show='N' AND UOM in ('FB','FC') AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + fndLocation.Value + "' AND Item_Qty <> 0 and MRP=View_TSPL_SHIPMENT_ITEMS.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=View_TSPL_SHIPMENT_ITEMS.Item_Code and UOM_Code=View_TSPL_SHIPMENT_ITEMS.UOM))"
                            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ShipmentItemSelect", qry, "Item_Code", clsCommon.myCstr(gvLoadOut.CurrentRow.Cells("itemcode").Value))
                            If dr IsNot Nothing Then
                                Dim itemcode As String = Convert.ToString(dr("Item_Code"))
                                Dim startdate As String = CDate(dr("start_date")).ToString("dd/MM/yyyy")
                                Dim mrp As Decimal = clsCommon.myCdbl(dr("MRP"))
                                Dim strUOM As String = clsCommon.myCstr(dr("UOM"))
                                currentmanualscheme(itemcode, startdate, mrp, strUOM)
                                isCellValueChangedOpen = False
                                Exit Sub
                            Else
                                gvLoadOut.CurrentRow.Cells("itemCode").Value = String.Empty
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                        End If
                        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                        If grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" And grow.Cells(colPromoSchemeItem).Value = "No" Then
                            grow.Cells("shippedQty").ReadOnly = False
                            If e.Column Is gvLoadOut.Columns(collocation) Then
                                sql = "SELECT LD.Item_Qty * UD.Conversion_Factor AS Expr1 FROM TSPL_ITEM_LOCATION_DETAILS AS LD INNER JOIN " & _
                                      " TSPL_ITEM_UOM_DETAIL AS UD ON LD.Item_Code = UD.Item_Code WHERE LD.Item_Code='" + grow.Cells(ColICode).Value + "' AND " & _
                                      " LD.Location_Code='" + grow.Cells(collocation).Value + "' and LD.MRP='" + grow.Cells(colMRP).Value + "' AND UD.UOM_Code='" + grow.Cells(colUnitCode).Value + "'"
                                grow.Cells("balanceQty").Value = Math.Round(Convert.ToDecimal(connectSql.RunScalar(sql)), 2)

                            ElseIf e.Column Is gvLoadOut.Columns(colSchemeApplicable) Then
                                findQtyandPromoSchemeCode(grow, "Q")
                            ElseIf e.Column Is gvLoadOut.Columns(collocation) Then
                                findQtyandPromoSchemeCode(grow, "P")
                            ElseIf e.Column Is gvLoadOut.Columns(colSchemeDiscountApplicable) Then
                                addCashDiscountScheme(grow)
                            ElseIf e.Column Is gvLoadOut.Columns(colitemNetAmount) Then
                                calculateItemTaxAgainstItemRate(grow)
                                CalculateTaxByLocation(grow)
                            ElseIf e.Column Is gvLoadOut.Columns(colShippedQty) Then
                                If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                                    common.clsCommon.MyMessageBoxShow("Please select customer.")
                                    grow.Cells("shippedQty").Value = 0
                                    fndCustomer.Focus()
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                Else
                                    ''For Same item with diffenet price can select.
                                    '' ''For Each gro As GridViewRowInfo In gvLoadOut.Rows
                                    '' ''    If Not gro.Index = grow.Index Then
                                    '' ''        If gro.Cells(ColICode).Value = grow.Cells(ColICode).Value AndAlso clsCommon.myCdbl(gro.Cells(colShippedQty).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colShippedQty).Value) > 0 AndAlso grow.Cells(colUnitCode).Value = gro.Cells(colUnitCode).Value AndAlso clsCommon.myCdbl(gro.Cells(colMRP).Value) <> clsCommon.myCdbl(grow.Cells(colMRP).Value) Then
                                    '' ''            If gro.Cells(colSchemeItem).Value = "No" AndAlso gro.Cells(colPromoSchemeItem).Value = "No" AndAlso gro.Cells(colSampleItem).Value = "No" Then
                                    '' ''                common.clsCommon.MyMessageBoxShow("Same item for different MRP can not be shipped.")
                                    '' ''                grow.Cells(colShippedQty).Value = 0
                                    '' ''                isCellValueChangedOpen = False
                                    '' ''                Exit For
                                    '' ''            End If
                                    '' ''        End If
                                    '' ''    End If
                                    '' ''Next
                                    addCashDiscountScheme(grow)
                                    findQtyandPromoSchemeCode(grow, "P")
                                    findQtyandPromoSchemeCode(grow, "Q")


                                    loadoutCellValueChange(grow)
                                    calculateItemTaxAgainstItemRate(grow)
                                    CalculateTaxByLocation(grow)
                                End If
                            End If
                        End If
                        Dim dblBasicAmt As Double = 0.0
                        Dim netAmount As Decimal = 0.0
                        Dim totalTPT As Decimal = 0.0
                        Dim ttlCustDiscount As Decimal = 0
                        For Each gro As GridViewRowInfo In gvLoadOut.Rows
                            dblBasicAmt = dblBasicAmt + clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value)
                            netAmount = netAmount + clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value)
                            totalTPT = totalTPT + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value)
                            ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells(colTotalCustDiscount).Value)
                        Next
                        txtShipmentTotal.Text = clsCommon.myFormat(dblBasicAmt)
                        MyTextBox3.Text = clsCommon.myFormat(netAmount)
                        txtCustDisc.Text = ttlCustDiscount
                        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                            Dim assess As Decimal = 0
                            Dim taxAmt As Decimal = 0
                            Dim basicprice As Decimal = 0
                            For Each gr1 As GridViewRowInfo In gvLoadOut.Rows
                                If Not clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) = 0 Then
                                    If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                                        assess = assess + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value)
                                        taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value)
                                        Try
                                            Dim x As Double = clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                                        Catch ex As Exception
                                            common.clsCommon.MyMessageBoxShow(ex.Message)
                                        End Try

                                        basicprice = basicprice + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells(colBasicAmount).Value)
                                    End If
                                End If
                            Next
                            If assess = 0 AndAlso taxAmt = 0 Then
                                gr.Cells(colTAssessibleAmount).Value = assess
                                gr.Cells(colTTaxAmount).Value = taxAmt
                            End If
                            If assess <> 0 Then
                                gr.Cells(colTTaxRate).Value = Math.Round(taxAmt * 100 / assess, 0)
                                gr.Cells(colTAssessibleAmount).Value = Math.Round(assess, 2)
                                gr.Cells(colTTaxAmount).Value = Math.Round(taxAmt, 2)
                                gr.Cells(colTBasicAmount).Value = basicprice
                            End If
                        Next



                        Dim totalTax As Decimal = 0.0
                        Dim totaltaxamt As Decimal = 0
                        For Each g As GridViewRowInfo In gvTaxDetails.Rows
                            totaltaxamt = totaltaxamt + clsCommon.myCdbl(g.Cells(colTTaxAmount).Value)
                        Next
                        txtTotalTaxAmount.Text = totaltaxamt
                        txtShipmentAmt.Text = 0
                        For Each t As GridViewRowInfo In gvLoadOut.Rows
                            txtShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + t.Cells(colTotalItemAmount).Value
                        Next
                        txttotaltpt.Text = totalTPT

                        If e.Column Is gvLoadOut.Columns(colUnitCode) Then
                            'Dim pricedate As Date
                            'Dim pricedt As String

                            If clsCommon.myLen(gvLoadOut.CurrentRow.Cells(colFromSchemeCode).Value) > 0 AndAlso gvLoadOut.CurrentRow.Cells(colFromSchemeCode).Value.ToString().Contains("MS") Then
                                If e.Column Is gvLoadOut.Columns(colShippedQty) Then
                                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gvLoadOut.CurrentRow.Cells(ColICode).Value + "' AND " & _
              " Uom_Code='" + gvLoadOut.CurrentRow.Cells(colUnitCode).Value + "'"

                                    Dim convertFact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql))
                                    If Not convertFact = 0 Then
                                        Dim shells As Integer
                                        Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
                                        If bottles = 0 Then
                                            shells = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - bottles) / convertFact
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        Else
                                            grow.Cells(colEmptyValueShell).Value = "0.00"
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        End If
                                        If bottles = 0 And shells = 0 Then
                                            grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                                        End If
                                    End If
                                    grow.Cells(colTotalMRP).Value = grow.Cells(colShippedQty).Value * grow.Cells(colMRP).Value
                                    grow.Cells(colTotalTaxAmount).Value = grow.Cells(colTaxamount).Value * grow.Cells(colShippedQty).Value
                                End If
                            End If
                        End If
                        Dim containerdeposit As Decimal = 0
                        For Each g As GridViewRowInfo In gvLoadOut.Rows
                            If clsCommon.myCdbl(g.Cells(colShippedQty).Value) > 0 Then
                                containerdeposit = containerdeposit + clsCommon.myCdbl(g.Cells(colEmptyValue).Value)
                            End If
                        Next
                        containerdeposit = containerdeposit + clsCommon.myCdbl(txtshellqty.Text) * 100
                        MyTextBox4.Text = containerdeposit
                        txtNetShipmentAmt.Text = containerdeposit + clsCommon.myCdbl(txtShipmentAmt.Text)
                        funtotalfcfb()
                        txtShipmentAmt.Text = 0
                        For Each gr As GridViewRowInfo In gvLoadOut.Rows
                            If Not clsCommon.myCdbl(gr.Cells(colShippedQty).Value) = 0 Then
                                If gr.Cells(colSchemeItem).Value = "No" And gr.Cells(colPromoSchemeItem).Value = "No" Then
                                    txtShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + gr.Cells(colTotalItemAmount).Value
                                End If
                            End If
                        Next
                        txtShipmentAmt.Text = clsCommon.myCdbl(MyTextBox3.Text) + clsCommon.myCdbl(txtTotalTaxAmount.Text) + clsCommon.myCdbl(txttotaltpt.Text)
                        txtNetShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + clsCommon.myCdbl(MyTextBox4.Text)


                        If e.Column.Name = "shippedqty" Then
                            If clsCommon.myCdbl(e.Row.Cells(colShippedQty).Value) = 0 And e.Row.Cells(colSchemeCodeItem).Value <> "" Then
                                For Each gr As GridViewRowInfo In gvLoadOut.Rows
                                    If gr.Cells(colMainItem).Value = e.Row.Cells(colSchemeCodeItem).Value Then
                                        gvLoadOut.Rows.RemoveAt(gr.Index)
                                    End If
                                Next
                            End If
                        End If

                        If gvLoadOut.CurrentColumn Is gvLoadOut.Columns(colMainItem) Then
                            If clsCommon.myLen(e.Value) > 0 Then
                                Dim grow1 As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                                Dim qry As String = ""

                                qry += "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM View_TSPL_SHIPMENT_ITEMS  Where Show='N' AND UOM='FC' AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + fndLocation.Value + "' AND Item_Qty <> 0 and MRP=View_TSPL_SHIPMENT_ITEMS.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=View_TSPL_SHIPMENT_ITEMS.Item_Code and UOM_Code=View_TSPL_SHIPMENT_ITEMS.UOM)) "

                                Dim itemcode As String = String.Empty
                                Dim startdate As String = String.Empty
                                Dim mrpvalue As Decimal = 0
                                If grow1.Cells(colMainItem).Value <> String.Empty Or grow1.Cells(colCheckvalue).Value <> String.Empty Then
                                    customdatarow = clsCommon.ShowSelectFormForRow("ShipmentItemMain", qry, "Item_Code", clsCommon.myCstr(grow1.Cells(colMainItem).Value))
                                    If customdatarow IsNot Nothing Then
                                        itemcode = Convert.ToString(customdatarow("Item_Code"))
                                        startdate = CDate(customdatarow("start_date")).ToString("dd/MM/yyyy")
                                        mrpvalue = clsCommon.myCdbl(customdatarow("MRP"))
                                        Dim strcheck As String = "N"
                                        Dim ConvFactor As Decimal = 0
                                        For Each gr As GridViewRowInfo In gvLoadOut.Rows
                                            If gr.Cells(ColICode).Value = itemcode AndAlso gr.Cells(colPriceDate).Value = startdate Then
                                                ConvFactor = connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Convert.ToString(gr.Cells(ColICode).Value) + "' and UOM_Code = '" + Convert.ToString(gr.Cells(colUnitCode).Value) + "'")
                                                If gr.Cells(colMRP).Value = mrpvalue / ConvFactor Then
                                                    If gr.Cells(colShippedQty).Value > 0 Then
                                                        strcheck = "Y"
                                                        If String.IsNullOrEmpty(Convert.ToString(gr.Cells(colSchemeCodeItem).Value)) Then
                                                            gr.Cells(colSchemeCodeItem).Value = gvLoadOut.CurrentRow.Cells(colFromSchemeCode).Value
                                                        Else
                                                            gvLoadOut.CurrentRow.Cells(colFromSchemeCode).Value = gr.Cells(colSchemeCodeItem).Value
                                                        End If
                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Next
                                        If strcheck = "N" Then
                                            common.clsCommon.MyMessageBoxShow("Main Item didn't shipped any qty")
                                            gvLoadOut.CurrentRow.Cells(colMainItem).Value = String.Empty
                                            gvLoadOut.CurrentRow.Cells(colCheckvalue).Value = String.Empty
                                        Else
                                            gvLoadOut.CurrentRow.Cells(colMainItem).Value = itemcode
                                            gvLoadOut.CurrentRow.Cells(colCheckvalue).Value = "some"
                                        End If
                                    Else
                                        gvLoadOut.CurrentRow.Cells(colMainItem).Value = String.Empty
                                        gvLoadOut.CurrentRow.Cells(colCheckvalue).Value = String.Empty
                                    End If
                                End If
                                gvLoadOut.CurrentColumn = gvLoadOut.Columns(ColTargetDisAmt)
                            End If
                        ElseIf gvLoadOut.CurrentColumn Is gvLoadOut.Columns(colDiscountCode) Then
                            sql = "select Code,Description from TSPL_Discount_Master "
                            Dim whrclas As String = " skuwise='Y' "
                            gvLoadOut.CurrentRow.Cells(colDiscountCode).Value = clsCommon.ShowSelectForm("ShipmtDisCodFND", sql, "Code", whrclas, clsCommon.myCstr(gvLoadOut.CurrentRow.Cells(colDiscountCode).Value))
                            gvLoadOut.CurrentRow.Cells("mainitem").Value = ""
                        End If



                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateTaxByLocation(ByVal grow As GridViewRowInfo)
        Dim dblTotalTax As Double = 0
        For ii As Integer = 1 To 6
            dblTotalTax += clsCommon.myCdbl(grow.Cells("tax" + clsCommon.myCstr(ii) + "Amt").Value)
        Next
        grow.Cells(colTotalTaxAmount).Value = dblTotalTax * clsCommon.myCdbl(grow.Cells(colShippedQty).Value)

    End Sub

    Private Sub funtotalfcfb()
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        For Each g As GridViewRowInfo In gvLoadOut.Rows
            If clsCommon.myCdbl(g.Cells("shippedQty").Value) <> 0 Or g.Cells("shippedQty").Value <> String.Empty Then
                If g.Cells("unitCode").Value = "FC" Then
                    totalfc = totalfc + clsCommon.myCdbl(g.Cells("shippedQty").Value)
                End If
                If g.Cells("unitCode").Value = "FB" Then
                    totalfb = totalfb + clsCommon.myCdbl(g.Cells("shippedQty").Value)
                End If
            End If
        Next
        If totalfc = 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = 0
        ElseIf totalfb = 0 Then
            lblfc.Text = CStr(totalfc)
            lblfb.Text = 0
        ElseIf totalfb <> 0 And totalfc <> 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb = 0 And totalfc = 0 Then
            lblfc.Text = 0
            lblfb.Text = 0
        End If
    End Sub

    Private Sub gvLoadOut1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLoadOut.CellEditorInitialized
        If TypeOf Me.gvLoadOut.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gvLoadOut.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor(editor.DisplayMember, FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub gvLoadOut1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gvLoadOut.CellBeginEdit
        If TypeOf Me.gvLoadOut.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gvLoadOut.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
        End If
        If TypeOf Me.gvLoadOut.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gvLoadOut.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)
            If e.Column.Name = "shippedQty" Then
                Dim balanceqty As Decimal = 0
                Dim convertFact As Decimal = 0
                If gvLoadOut.CurrentRow.Cells("shippedQty").Value > 0 Then
                    AddHandler editorElement.KeyPress, AddressOf NumericKeyPress
                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + e.Row.Cells("itemCode").Value + "' AND " & _
                  " Uom_Code='" + e.Row.Cells("unitCode").Value + "'"
                    convertFact = clsCommon.myCdbl(connectSql.RunScalar(sql))
                    If convertFact = 0 Then
                        common.clsCommon.MyMessageBoxShow("Conversion factor is not available.")
                        Exit Sub
                    End If
                End If
                Dim shippingQty As Decimal = 0
                For Each gro As GridViewRowInfo In gvLoadOut.Rows
                    If Not gro.Index = e.Row.Index AndAlso clsCommon.myCdbl(gro.Cells("shippedQty").Value) > 0 Then
                        If gro.Cells("itemCode").Value = e.Row.Cells("itemCode").Value Then
                            sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gro.Cells("itemCode").Value + "' AND " & _
                                      " Uom_Code='" + gro.Cells("unitCode").Value + "'"
                            Dim Fact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql))
                            If Fact = 0 Then
                                common.clsCommon.MyMessageBoxShow("Conversion factor is not available.")
                                Exit Sub
                            End If
                            shippingQty = shippingQty + Math.Round(clsCommon.myCdbl(gro.Cells("shippedQty").Value) / Fact, 2)
                        End If
                    End If
                Next
                balanceqty = Math.Round(balanceqty - shippingQty, 2)
                ttlItemShpQtyForCheck = balanceqty * convertFact
            ElseIf e.ColumnIndex = 1 Then
            End If
        End If
    End Sub

    Private Sub gvLoadOut1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLoadOut.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        If e.Column.Name = "complete" Then
            If grow.Cells("complete").Value = "No" Then
                grow.Cells("complete").Value = "Yes"
            ElseIf grow.Cells("complete").Value = "Yes" Then
                grow.Cells("complete").Value = "No"
            End If
        ElseIf e.Column.Name = "schemeApplicable" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("schemeItem").Value = "No" And grow.Cells("sampleItem").Value = "No" Then
            If grow.Cells("schemeApplicable").Value = "Yes" Then
                grow.Cells("schemeApplicable").Value = "No"

            ElseIf grow.Cells("schemeApplicable").Value = "No" Then
                grow.Cells("schemeApplicable").Value = "Yes"
            End If
        ElseIf e.Column.Name = "promoSchemeApplicable" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("schemeItem").Value = "No" And grow.Cells("sampleItem").Value = "No" Then
            If grow.Cells("promoSchemeApplicable").Value = "Yes" Then
                grow.Cells("promoSchemeApplicable").Value = "No"
            ElseIf grow.Cells("promoSchemeApplicable").Value = "No" Then
                grow.Cells("promoSchemeApplicable").Value = "Yes"
            End If
        ElseIf e.Column.Name = "schemeDiscountApplicable" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("schemeItem").Value = "No" And grow.Cells("sampleItem").Value = "No" Then
            If grow.Cells("schemeDiscountApplicable").Value = "Yes" Then
                grow.Cells("schemeDiscountApplicable").Value = "No"
            ElseIf grow.Cells("schemeDiscountApplicable").Value = "No" Then
                grow.Cells("schemeDiscountApplicable").Value = "Yes"
            End If
        ElseIf e.Column.Name = "taxAmount" Then
            If e.Row.Cells("shippedqty").Value <> 0 Then
                Dim frm As New FrmTaxDetails()
                frm.locationCode = fndLocation.Value
                frm.taxGroupCode = fndTaxGroup.txtValue.Text

                frm.assessibleAmount = Math.Round(Math.Round(clsCommon.myCdbl(e.Row.Cells("mrp").Value) * abatement(e.Row) / 100, 2), 2).ToString()
                frm.gridRow = e.Row
                frm.ShowDialog()
            End If


            'If frm.btnFlag Then
            '    Dim taxDetails As ArrayList = frm.taxDetails
            '    RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
            '    Dim i As Integer = 1
            '    For Each item() As String In taxDetails
            '        e.Row.Cells("tax" + i.ToString() + "Rate").Value = item(0)
            '        e.Row.Cells("assess" + i.ToString()).Value = item(1)
            '        e.Row.Cells("tax" + i.ToString() + "Amt").Value = item(2)
            '        i = i + 1
            '    Next
            '    AddHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
            '    e.Row.Cells("taxAmount").Value = frm.totalItemTax
            '    RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
            '    grow.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("taxAmount").Value) * clsCommon.myCdbl(grow.Cells("shippedqty").Value), 2)
            '    grow.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(grow.Cells("totalNetAmount").Value) + clsCommon.myCdbl(grow.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(grow.Cells("totalTPT").Value), 2)
            '    AddHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
            'End If
        End If
    End Sub

    Private Sub gvLoadOut1_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvLoadOut.EditorRequired
        Dim col As GridViewMultiComboBoxColumn = TryCast(gvLoadOut.Columns("unitCode"), GridViewMultiComboBoxColumn)
        Dim col1 As GridViewComboBoxColumn = TryCast(gvLoadOut.Columns("priceDate"), GridViewComboBoxColumn)
        If gvLoadOut.CurrentColumn.Name = "unitCode" Then
            sql = "SELECT DISTINCT U.Unit_Code, U.Unit_Desc, U.Conv_Factor FROM TSPL_UNIT_MASTER AS U INNER JOIN TSPL_ITEM_PRICE_MASTER AS P " & _
   " ON U.Unit_Code=P.UOM WHERE P.Item_Code='" + gvLoadOut.CurrentRow.Cells("itemCode").Value + "' AND P.Price_Code='" + txtPriceCode.Text + "' ORDER BY Unit_Code"
            ds = connectSql.RunSQLReturnDS(sql)
            col.ValueMember = "Unit_Code"
            col.DataSource = ds.Tables(0)
        ElseIf gvLoadOut.CurrentColumn.Name = "priceDate" Then
            sql = "select distinct CONVERT(varchar(10), Start_Date, 103) as Start_Date FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code='" + gvLoadOut.CurrentRow.Cells("itemCode").Value + "' ORDER BY Start_Date"
            ds = connectSql.RunSQLReturnDS(sql)
            col1.ValueMember = "Start_Date"
            col1.DataSource = ds.Tables(0)
        End If
    End Sub

    Private Sub gvTaxDetails_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvTaxDetails.EditorRequired
        If TypeOf gvTaxDetails.CurrentColumn Is GridViewComboBoxColumn Then
            Dim coltaxrate As GridViewComboBoxColumn = TryCast(gvTaxDetails.Columns("taxRate"), GridViewComboBoxColumn)
            sql = "select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Code='" + gvTaxDetails.CurrentRow.Cells("taxAuthority").Value + "' AND Tax_Type='S'"
            ds = connectSql.RunSQLReturnDS(sql)
            ' coltaxrate.DisplayMember = "Tax_Rate"
            coltaxrate.ValueMember = "Tax_Rate"
            coltaxrate.DataSource = ds.Tables(0)
            'End If
        End If
    End Sub

    Private Sub gvTaxDetails_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTaxDetails.CellValueChanged
        'Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        'Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If column.Name = "taxRate" Then
        '    Dim netAmount As Decimal = totalNetAmount()
        '    For Each gro As GridViewRowInfo In gvLoadOut.Rows
        '        gro.Cells("taxAmount").Value = Math.Round(calculateItemTax(clsCommon.myCdbl(gro.Cells("mrp").Value) * abatement(gro) / 100, clsCommon.myCdbl(gro.Cells("itemNetAmount").Value)), 4)
        '        gro.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells("shippedQty").Value) * clsCommon.myCdbl(gro.Cells("taxAmount").Value), 4)
        '        gro.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells("totalNetAmount").Value) + clsCommon.myCdbl(gro.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(gro.Cells("totalTPT").Value), 4)
        '    Next
        '    txtShipmentTotal.Text = netAmount

        '    calculateTax(totalAssessibleAmt, netAmount)
        '    Dim totalTax As Decimal = 0.0
        '    For Each gr As GridViewRowInfo In gvTaxDetails.Rows
        '        totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
        '    Next
        '    txtTotalTaxAmount.Text = totalTax
        '    totalAmounts()
        'End If
    End Sub

    Private Sub gvTaxDetails_ContextMenuOpening(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ContextMenuOpeningEventArgs) Handles gvTaxDetails.ContextMenuOpening
        e.ContextMenu.Enabled = False
    End Sub

    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = fndVehicleCode.txtValue.Text
        sql = "select Description from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(fndVehicleCode.txtValue.Text) + "' or Description = '" + Convert.ToString(fndVehicleCode.txtValue.Text) + "'"
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            Dim strVehicalNo As String = fndVehicleCode.txtValue.Text
            strmessage += "Do you want to continue "
            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'"))

                fndVehicleCode.txtValue.Text = Convert.ToString(count + 1) + ""
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
                clsDBFuncationality.SaveAStorePorcedure("sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", fndVehicleCode.txtValue.Text), New SqlParameter("@desc", strVehicalNo), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                clsDBFuncationality.SaveAStorePorcedure("SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", fndVehicleCode.txtValue.Text), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                txtVhicleNo.Text = strvehiclenum
                Return True
            Else
                fndVehicleCode.txtValue.Text = String.Empty
                Return False
            End If
        End If

    End Function

    Private Function abatement(ByVal grow As GridViewRowInfo) As Decimal
        Dim abat As Decimal
        sql = "Select Abatement_Rate from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + grow.Cells("itemCode").Value + "' AND Item_Basic_Net='" + grow.Cells("mrp").Value + "' AND Start_Date='" + Format(CDate(grow.Cells("priceDate").Value), "MM/dd/yyyy") + "'"
        abat = connectSql.RunScalar(sql)
        Return abat
    End Function

    Private Function abatement1(ByVal grow As GridViewRowInfo, Optional ByVal trans As SqlTransaction = Nothing) As Decimal

        Dim abat As Decimal
        sql = "Select Abatement from TSPL_SHIPMENT_DETAILS WHERE Shipment_Id='" + grow.Cells("lineNo").Value + "' AND Shipment_No='" + txtDocumentNo.Value + "'"
        If trans Is Nothing Then
            abat = connectSql.RunScalar(sql)
        Else
            abat = connectSql.RunScalar(trans, sql)
        End If
        Return abat
    End Function

    Private Function totalItemAssessibleAmt1(ByVal grow As GridViewRowInfo, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Return Math.Round(clsCommon.myCdbl(grow.Cells("totalMRP").Value) * abatement1(grow, trans) / 100, 2)
    End Function

    Private Function itemAssessibleAmt(ByVal grow As GridViewRowInfo) As Decimal
        Dim TAX As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells("mrp").Value) * abatement(grow) / 100, 2)

        Return Math.Round(clsCommon.myCdbl(grow.Cells("mrp").Value) * abatement(grow) / 100, 2)
    End Function

    Private Function totalItemAssessibleAmt(ByVal grow As GridViewRowInfo) As Decimal
        Dim amt As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells("totalMRP").Value) * abatement(grow) / 100, 2)
        Return amt
    End Function

    Private Function totalAssessibleAmt() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            If Not clsCommon.myCdbl(grow.Cells("shippedqty").Value) = 0 Then
                total = total + Math.Round(clsCommon.myCdbl(grow.Cells("totalMRP").Value) * abatement(grow) / 100, 2)
            End If
            'If grow.Cells("schemeItem").Value = "No" AndAlso grow.Cells("promoSchemeItem").Value = "No" AndAlso grow.Cells("sampleItem").Value = "No" Then
            '    total = total + Math.Round(clsCommon.myCdbl(grow.Cells("totalMRP").Value) * abatement(grow) / 100, 4)

            'End If
        Next
        Return total
    End Function

    Private Function totalMRP() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            If grow.Cells("schemeItem").Value = "No" AndAlso grow.Cells("promoSchemeItem").Value = "No" AndAlso grow.Cells("sampleItem").Value = "No" Then
                total = total + clsCommon.myCdbl(grow.Cells("totalMRP").Value)
            End If
        Next
        Return total
    End Function

    Private Function totalBasicAmt() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            total = total + clsCommon.myCdbl(grow.Cells("totalBasicAmount").Value)
        Next
        Return total
    End Function

    Private Function totalNetAmount() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            If grow.Cells("schemeItem").Value = "No" AndAlso grow.Cells("promoSchemeItem").Value = "No" AndAlso grow.Cells("sampleItem").Value = "No" Then
                total = total + clsCommon.myCdbl(grow.Cells("totalNetAmount").Value)
            End If
        Next
        Return total
    End Function

    Private Function totalDiscount() As Decimal
        Dim total As Decimal = 0
        'If clsCommon.myCdbl(MyTextBox1.Text) <> 0 Then
        '    total = connectSql.RunScalar("select Shipment_Discount_Amt  from tspl_shipment_master where Shipment_No = '" + Convert.ToString(fndLoadOut.Value) + "'")
        'Else
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            total = total + clsCommon.myCdbl(grow.Cells("totalDiscountAmount").Value)
        Next
        'End If

        Return total
    End Function

    Private Function totalEmptyAmount() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            If Not clsCommon.myCdbl(grow.Cells("shippedqty").Value) = 0 Then
                total = total + Math.Round(clsCommon.myCdbl(grow.Cells("emptyValue").Value), 2)

            End If
        Next
        Return total
    End Function

    Private Function totalTransport() As Decimal
        Dim total As Decimal = 0
        If 0 > 0 Then
            total = txttotaltpt.Text
        Else
            For Each grow As GridViewRowInfo In gvLoadOut.Rows
                total = total + clsCommon.myCdbl(grow.Cells("totalTPT").Value)
            Next
        End If

        Return total
    End Function

    Private Function promoSchemeApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("promoSchemeApplicable").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeItemApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("schemeApplicable").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeDiscApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("schemeDiscountApplicable").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("schemeItem").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function promoSchemeItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("promoSchemeItem").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function sampleItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("sampleItem").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function complete(ByVal grow As GridViewRowInfo) As String
        If grow.Cells("complete").Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function checkItemonLocation12(ByVal itemcode As String, ByVal shippedqty As Decimal, ByVal location As String, ByVal uom As String, ByVal mrp As Decimal, ByVal scheme As Boolean, ByVal batchnumber As String) As Boolean
        Dim stockQty As Decimal = 0
        If shippedqty > 0 Then
            sql = "SELECT Allow_Negative_Inv FROM TSPL_INV_PARAMETERS"
            If connectSql.RunScalar(sql) = "N" Then
                Dim orderedQty As Decimal = 0
                Dim totalShippedQty As Decimal = 0
                sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + itemcode + "' AND " & _
                       " Uom_Code='" + uom + "'"
                Dim convertFact As Decimal = connectSql.RunScalar(sql)
                conversionnumber = convertFact
                If convertFact = 0 Then
                    common.clsCommon.MyMessageBoxShow("Conversion factor is not defined.")
                    Return False
                End If
                Dim standardMRP As Decimal = Math.Round(mrp * convertFact, 2)

                Dim JKL As Integer = gvLoadOut.CurrentRow.Index
                Dim conversion As Decimal = connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gvLoadOut.CurrentRow.Cells("itemCode").Value + "' AND UOM_Code  = '" + gvLoadOut.CurrentRow.Cells("unitCode").Value + "' ")
                sql = "SELECT isnull(sum(Item_Qty),0) as [Item_Qty] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + itemcode + "'  AND location_code='" + location + "' and MRP='" + standardMRP.ToString() + "' "
                stockQty = clsCommon.myCdbl(connectSql.RunScalar(sql))
                stockQty = stockQty * conversion

                For Each gr As GridViewRowInfo In gvLoadOut.Rows
                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gvLoadOut.CurrentRow.Cells("itemCode").Value + "' AND " & _
                       " Uom_Code='" + gr.Cells("unitcode").Value + "'"
                    Dim fact As Decimal = connectSql.RunScalar(sql)
                    If clsCommon.myCdbl(gr.Cells("shippedQty").Value) > 0 AndAlso gr.Cells("itemCode").Value = itemcode AndAlso Math.Round(clsCommon.myCdbl(gr.Cells("mrp").Value) * fact, 2) = standardMRP AndAlso gr.Cells("batchnumber").Value = batchnumber Then
                        totalShippedQty = totalShippedQty + clsCommon.myCdbl(gr.Cells("shippedQty").Value) * conversion / fact
                    End If
                Next
                If totalShippedQty < stockQty Then
                ElseIf totalShippedQty = stockQty Then
                Else
                    common.clsCommon.MyMessageBoxShow("This quantity is not available at the location.")
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub resetFNDCustomer()
        fndCustomer.Value = String.Empty
        txtCustomerName.Text = String.Empty
        fndRouteNo.txtValue.Text = String.Empty
        txtRouteDesc.Text = String.Empty
        txtPriceCode.Text = String.Empty
        fndTaxGroup.txtValue.Text = String.Empty
        txttaxdesc.Text = ""
        fndSalesman.txtValue.Text = String.Empty
        ddlPriceDate.Text = "Select"
        ddlPriceDate.DataSource = Nothing
        ddlPriceDate.Items.Clear()
        ddlPriceDate.Text = "Select"
        gvLoadOut.Rows.Clear()
    End Sub

    Private Sub resetFNDTaxGroup()
        gvTaxDetails.DataSource = Nothing
        gvTaxDetails.Rows.Clear()
    End Sub

    Private Sub resetForm()
        txtcustomerinvoiceno.Text = ""
        txtshellqty.Text = 0
        lblfc.Text = 0
        lblfb.Text = 0
        ddlPriceDate.Text = connectSql.myDate()
        fndCustomer.Enabled = True
        rbFB.Enabled = True
        rbFC.Enabled = True

        rbAll.Enabled = True
        rbFB.IsChecked = False
        fndemployeecode.txtValue.Text = ""
        rbFC.IsChecked = False
        rbAll.IsChecked = False
        fndCustomer.Enabled = True
        fndLocation.Enabled = True
        fndLocation.Enabled = True

        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        dtpShipDate.Value = clsCommon.GETSERVERDATE()


        btnAdd.Text = "Save"
        btnAdd.Enabled = True

        btnDelete.Enabled = False
        btnPost.Enabled = False

        txtRef.Text = String.Empty

        txtOtherCharges.Text = 0.0
        txtRemarks.Text = String.Empty
        txtShipTo.Value = ""
        lblShipTo.Text = ""

        txtAdditionalCharges.Text = 0.0
        txtDesc.Text = String.Empty
        txtCustDisc.Text = 0.0
        txtFreight.Text = 0.0
        txtKMReading.Text = 0
        fndRouteNo.txtValue.Text = String.Empty
        txtShipmentAmt.Text = 0.0
        txtShipmentTotal.Text = 0.0

        UsLock1.Status = ERPTransactionStatus.Pending
        txtTotalTaxAmount.Text = 0.0
        txtTripNo.Text = 0
        fndVehicleCode.txtValue.Text = String.Empty
        fndPaymentTerms.txtValue.Text = String.Empty
        fndLocation.Value = String.Empty
        ddlModeofTransport.Text = "Select"

        txtDocumentNo.Value = ""
        ddlPriceDate.DropDownStyle = RadDropDownStyle.DropDownList

        ddlModeofTransport.DropDownStyle = RadDropDownStyle.DropDownList
        pvLoadOut.SelectedPage.Name = "pageLoadOut"
        gvLoadOut.AllowAddNewRow = False
        ''fndCustomer_Load(Me, New EventArgs())
        fndCustomer.Value = ""
        gvLoadOut.Columns("promoSchemeApplicable").DataSourceNullValue = "No"
        gvLoadOut.Columns("promoSchemeItem").DataSourceNullValue = "No"
        gvLoadOut.Columns("promoSchemeCode").DataSourceNullValue = ""
        gvLoadOut.Columns("SchemeItem").DataSourceNullValue = "No"
        gvLoadOut.Columns("schemeApplicable").DataSourceNullValue = "No"
        gvLoadOut.Columns("sampleItem").DataSourceNullValue = "No"
        gvLoadOut.Columns("schemeCodeItem").DataSourceNullValue = ""
        gvLoadOut.Columns("schemeCodeDiscount").DataSourceNullValue = ""
        gvLoadOut.Columns("emptyValue").DataSourceNullValue = 0
        gvLoadOut.Columns("priceCode").DataSourceNullValue = ""

        gvLoadOut.Columns("unitCode").DataSourceNullValue = ""
        If gvLoadOut.Columns.Contains("transferId") Then
            gvLoadOut.Columns.Remove("transferId")
        End If
        ddlModeofTransport.Text = "By Road"
        rbFB.Enabled = True
        rbFC.Enabled = True
        rbAll.Enabled = True
        txttotaltpt.Text = 0

        txtDiscAmt.Text = 0
        MyTextBox3.Text = 0
        MyTextBox4.Text = 0
        txtNetShipmentAmt.Text = 0

    End Sub

    Private Sub NumericKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If gvLoadOut.CurrentColumn.Name = "shippedQty" Then
            e.Handled = globalFunc.TrapKey(Asc(e.KeyChar))
        End If
    End Sub

    Private Sub keyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub bindPriceDate()
        sql = "select distinct CONVERT(varchar(10), Start_Date, 103) as Start_Date FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' ORDER BY Start_Date"
        transportSql.FillComboBox(sql, ddlPriceDate, "Start_Date", "Start_Date")
    End Sub

    Private Sub bindgvLoadOut1Columns()
        Dim colUnitCode As GridViewMultiComboBoxColumn = TryCast(gvLoadOut.Columns("unitCode"), GridViewMultiComboBoxColumn)
        ''Dim colLocation As GridViewMultiComboBoxColumn = TryCast(gvLoadOut.Columns("location"), GridViewMultiComboBoxColumn)
        'sql = "Select Location_Code as 'Location Code',Location_Desc as Description from TSPL_LOCATION_MASTER ORDER BY Location_Code"
        'ds = connectSql.RunSQLReturnDS(sql)
        'colLocation.ValueMember = "Location Code"
        'colLocation.DataSource = ds.Tables(0)

    End Sub

    Private Sub calculateItemTaxAgainstItemRate(ByVal gr As GridViewRowInfo)
        Dim basic As Decimal = gr.Cells("itemNetAmount").Value
        Dim assessible As Decimal = itemAssessibleAmt(gr)
        Dim ttlItemtax As Decimal = 0


        If gvTaxDetails.RowCount <> 0 Then
            If rbtnExcise.IsChecked Then
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    ' grow.Cells(3).Value = basic.ToString()
                    If grow.Index = 0 Then
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assessible, 2)
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Index < grow.Index Then
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + clsCommon.myCdbl(gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value)
                                End If
                            End If
                        Next
                        If basic = 0 Then
                            gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic
                        Else
                            gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic + taxabletaxTotal
                        End If
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value
                                Exit For
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assess, 2)
                    End If
                    gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value = Math.Round((clsCommon.myCdbl(gr.Cells("assess" + (grow.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Rate").Value) / 100), 4).ToString()
                    ttlItemtax = ttlItemtax + clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value)
                Next
            Else
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    If grow.Index = 0 Then
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Index < grow.Index Then
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + clsCommon.myCdbl(gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value)
                                End If
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic + taxabletaxTotal
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value
                                Exit For
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assess, 2)
                    End If
                    gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value = Math.Round((clsCommon.myCdbl(gr.Cells("assess" + (grow.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Rate").Value) / 100), 2).ToString()
                    ttlItemtax = ttlItemtax + clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value)
                Next
                gr.Cells("taxAmount").Value = Math.Round(ttlItemtax, 4, MidpointRounding.ToEven)
            End If
        Else
        End If
    End Sub

    Private Sub findQtyandPromoSchemeCode(ByVal grow As GridViewRowInfo, ByVal schemeType As String)
        Dim dr1 As datatable
        Dim schemeAppCol As String = ""
        Dim schemeCodeCol As String = ""
        Dim schemeItemCol As String = ""
        If schemeType = "P" Then
            schemeAppCol = "promoSchemeApplicable"
            schemeCodeCol = "promoSchemeCode"
            schemeItemCol = "promoSchemeItem"
        ElseIf schemeType = "Q" Then
            schemeAppCol = "schemeApplicable"
            schemeCodeCol = "schemeCodeItem"
            schemeItemCol = "schemeItem"
        End If
        Try


            sql = "SELECT S.Scheme_Code,S.Main_Item_Qty FROM TSPL_SCHEME_MASTER AS S INNER JOIN TSPL_SCHEME_DETAILS AS D ON S.Scheme_Code = D.Scheme_Code " & _
                  " WHERE  (S.Main_Item_Code = '" + grow.Cells("itemCode").Value + "') AND S.Start_Date <='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "' " & _
                  " AND (S.End_Date >='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "' OR S.End_Date is NULL ) AND S.Main_Item_Qty <= '" + grow.Cells("shippedQty").Value + "' " & _
                  " AND S.Main_item_UOM='" + grow.Cells("unitCode").Value + "' AND S.MRP='" + grow.Cells("mrp").Value + "' AND S.Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + fndCustomer.Value + "')"

            If grow.Cells(schemeAppCol).Value = "No" Then

                For schemeRow As Integer = gvLoadOut.Rows.Count - 1 To 0 Step -1
                    If Not grow.Cells(schemeCodeCol).Value.ToString().Trim() = String.Empty Then
                        If gvLoadOut.Rows(schemeRow).Cells("fromSchemeCode").Value = grow.Cells(schemeCodeCol).Value Then
                            gvLoadOut.Rows.RemoveAt(schemeRow)

                        End If
                    End If
                Next
                grow.Cells(schemeCodeCol).Value = String.Empty
            ElseIf grow.Cells(schemeAppCol).Value = "Yes" Then
                sql = "SELECT S.Scheme_Code,S.Main_Item_Qty FROM TSPL_SCHEME_MASTER AS S INNER JOIN TSPL_SCHEME_DETAILS AS D ON S.Scheme_Code = D.Scheme_Code " & _
                      " WHERE  (S.Scheme_Type = '" + schemeType + "') AND (S.Main_Item_Code = '" + grow.Cells("itemCode").Value + "') AND " & _
                      " S.Start_Date <='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "' AND (S.End_Date >='" + Format(connectSql.myDate(), "MM/dd/yyyy") + "' OR S.End_Date is NULL ) " & _
                      " AND S.Main_Item_Qty <= '" + grow.Cells("shippedQty").Value + "' " & _
                      " AND S.Main_item_UOM='" + grow.Cells("unitCode").Value + "' AND S.MRP='" + grow.Cells("mrp").Value + "' AND S.Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + fndCustomer.Value + "')"

                dr1 = clsDBFuncationality.GetDataTable(sql)
                Dim discountRatio As Integer = 0
                If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

                    grow.Cells(schemeCodeCol).Value = dr1.Rows(0)(0).ToString
                    Dim mainItemQty As Decimal = clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    Dim mainItemCode As String = grow.Cells("itemCode").Value
                    Dim mode As Decimal = clsCommon.myCdbl(grow.Cells("shippedQty").Value) Mod clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    discountRatio = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - mode) / clsCommon.myCdbl(dr1.Rows(0)(1).ToString())

                    If discountRatio > 0 Then
                        For schemeRow As Integer = gvLoadOut.Rows.Count - 1 To 0 Step -1
                            If Not grow.Cells(schemeCodeCol).Value.ToString().Trim() = String.Empty Then
                                If gvLoadOut.Rows(schemeRow).Cells("fromSchemeCode").Value = grow.Cells(schemeCodeCol).Value Then
                                    gvLoadOut.Rows.RemoveAt(schemeRow)
                                End If
                            End If
                        Next
                        sql = "SELECT SD.Scheme_Item_Code, SD.Qty,SD.UOM,SD.MRP FROM TSPL_SCHEME_MASTER AS SM INNER JOIN TSPL_SCHEME_DETAILS " & _
            "  AS SD ON SM.Scheme_Code = SD.Scheme_Code WHERE (SM.Scheme_Code = '" + grow.Cells(schemeCodeCol).Value + "')"
                        dr1 = clsDBFuncationality.GetDataTable(sql)
                        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

                            Dim usedItemQty As Decimal = clsCommon.myCdbl(dr1.Rows(0)(1).ToString()) * discountRatio

                            If Not checkItemonLocation12(dr1.Rows(0)(0).ToString(), usedItemQty, fndLocation.Value, dr1.Rows(0)(2).ToString(), clsCommon.myCdbl(dr1.Rows(0)(3).ToString()), True, grow.Cells("batchnumber").Value) Then
                                grow.Cells(schemeCodeCol).Value = String.Empty
                                grow.Cells(schemeAppCol).Value = "No"
                                Exit Sub
                            End If
                        End If
                    End If
                    addDiscountItemRow(discountRatio, grow, schemeType, schemeCodeCol)
                Else
                    common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                    grow.Cells(schemeAppCol).Value = "No"
                    grow.Cells(schemeCodeCol).Value = String.Empty
                End If
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
            Exit Sub
        End Try

    End Sub

    Private Sub addDiscountItemRow(ByVal disRatio As Integer, ByVal grow As GridViewRowInfo, ByVal schemeType As String, ByVal schemeCodeCol As String)
        sql = "SELECT Scheme_Item_Code, Scheme_Item_Desc, Qty, UOM,MRP  FROM TSPL_SCHEME_DETAILS WHERE (Scheme_Code = '" + grow.Cells(schemeCodeCol).Value + "')"

        Dim schemeDR As DataTable = clsDBFuncationality.GetDataTable(sql)
        If schemeDR IsNot Nothing AndAlso schemeDR.Rows.Count > 0 Then
            Dim i As Integer = grow.Index + 1
            For Each tdr As DataRow In schemeDR.Rows
                Dim viewInfo As New GridViewInfo(gvLoadOut.MasterTemplate)
                Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
                sql = "SELECT  top 1 Empty_Value_Bottle , Empty_Value_Shell,Item_Basic_Price,Start_Date,(TSPL_ITEM_PRICE_MASTER.NetLTPT+TSPL_ITEM_PRICE_MASTER.Price_Amount10) as PriceToShow FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + Convert.ToString(tdr("Scheme_Item_Code")) + "' AND " & _
                "UOM = '" + Convert.ToString(tdr("UOM")) + "' AND Item_Basic_Net = '" + Convert.ToString(tdr("MRP")) + "' and Price_Code='" + txtPriceCode.Text + "'  order by Start_Date desc"
                Dim emptydr As DataTable = clsDBFuncationality.GetDataTable(sql)
                '  Dim emptybottle, emptyshell As Decimal
                Dim dblBasicPrice As Double = 0
                Dim strStartDate As String = ""
                If emptydr IsNot Nothing AndAlso emptydr.Rows.Count > 0 Then

                    dataRowInfo.Cells("emptyValueBottle").Value = emptydr.Rows(0)("Empty_Value_Bottle")
                    dataRowInfo.Cells("emptyValueShell").Value = emptydr.Rows(0)("Empty_Value_Shell")
                    dblBasicPrice = clsCommon.myCdbl(emptydr.Rows(0)("Item_Basic_Price"))
                    strStartDate = clsCommon.GetPrintDate(emptydr.Rows(0)("Start_Date"), "dd/MM/yyyy")
                    dataRowInfo.Cells(ColPriceToShow).Value = emptydr.Rows(0)("PriceToShow")
                End If

                sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + Convert.ToString(tdr("Scheme_Item_Code")) + "' AND " & _
                    " Uom_Code='" + Convert.ToString(tdr("UOM")) + "'"
                Dim convertFact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql))
                If Not convertFact = 0 Then
                    Dim shells As Integer
                    Dim bottles As Decimal = clsCommon.myCdbl(tdr("Qty")) * disRatio Mod convertFact
                    If bottles = 0 Then
                        shells = (clsCommon.myCdbl(tdr("Qty")) - bottles) / convertFact
                        dataRowInfo.Cells("emptyValue").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("emptyValueBottle").Value) * (shells) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells("emptyValueShell").Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells("emptyValueBottle").Value) * bottles, 2)
                    Else
                        shells = (clsCommon.myCdbl(grow.Cells("shippedQty").Value) - bottles) / convertFact + 1
                        dataRowInfo.Cells("emptyValue").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("emptyValueBottle").Value) * (shells - 1) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells("emptyValueShell").Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells("emptyValueBottle").Value) * bottles, 2)
                    End If
                    If bottles = 0 And shells = 0 Then
                        dataRowInfo.Cells("emptyValue").Value = clsCommon.myCdbl(dataRowInfo.Cells("emptyValueBottle").Value) + clsCommon.myCdbl(dataRowInfo.Cells("emptyValueShell").Value)
                    End If

                End If
                dataRowInfo.Cells("fromSchemeCode").Value = grow.Cells(schemeCodeCol).Value
                dataRowInfo.Cells("complete").Value = "Yes"

                RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
                dataRowInfo.Cells("itemCode").Value = tdr(0).ToString()
                Dim shpQty As Decimal = tdr(2).ToString()
                dataRowInfo.Cells("itemName").Value = tdr(1).ToString()
                If clsCommon.myLen(strStartDate) <= 0 Then
                    Throw New Exception("Scheme can't be Applied")
                Else
                    dataRowInfo.Cells("priceDate").Value = strStartDate
                End If
                dataRowInfo.Cells("priceCode").Value = txtPriceCode.Text
                dataRowInfo.Cells("orderedQty").ReadOnly = True
                dataRowInfo.Cells("orderedQty").Value = 0
                dataRowInfo.Cells("shippedQty").ReadOnly = True
                dataRowInfo.Cells("balanceQty").ReadOnly = True
                dataRowInfo.Cells("balanceQty").Value = 0
                dataRowInfo.Cells("unitCode").Value = tdr(3).ToString()
                Dim emptyshell1 As Decimal = connectSql.RunScalar("select Empty_Value_Shell   from TSPL_ITEM_PRICE_MASTER where Item_Code =  '" + dataRowInfo.Cells("itemCode").Value + "' AND UOM = '" + dataRowInfo.Cells("unitCode").Value + "'")
                Dim emptybottle1 As Decimal = connectSql.RunScalar("select Empty_Value_Bottle  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + dataRowInfo.Cells("itemCode").Value + "' AND UOM = '" + dataRowInfo.Cells("unitCode").Value + "'")
                Dim conversion As Decimal = connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + dataRowInfo.Cells("itemCode").Value + "' AND UOM_Code  = '" + dataRowInfo.Cells("unitCode").Value + "'")


                If schemeType = "Q" Then
                    dataRowInfo.Cells("schemeItem").Value = "Yes"
                    dataRowInfo.Cells("promoSchemeItem").Value = "No"
                Else
                    dataRowInfo.Cells("schemeItem").Value = "No"
                    dataRowInfo.Cells("promoSchemeItem").Value = "Yes"
                End If
                dataRowInfo.Cells("promoSchemeCode").Value = ""
                dataRowInfo.Cells("schemeCodeItem").Value = ""
                dataRowInfo.Cells("schemeApplicable").Value = "No"
                dataRowInfo.Cells("promoSchemeApplicable").Value = "No"
                dataRowInfo.Cells("schemeDiscountApplicable").Value = "No"
                dataRowInfo.Cells("schemeCodeDiscount").Value = ""
                dataRowInfo.Cells("sampleItem").Value = "No"
                dataRowInfo.Cells("tpt").Value = "0"
                dataRowInfo.Cells("basicAmount").Value = dblBasicPrice
                dataRowInfo.Cells("transferBasicAmount").Value = dblBasicPrice
                dataRowInfo.Cells("discountAmount").Value = "0"
                dataRowInfo.Cells("itemNetAmount").Value = "0"
                dataRowInfo.Cells("custDiscount").Value = 0
                dataRowInfo.Cells("totalCustDiscount").Value = 0
                dataRowInfo.Cells("mrp").Value = tdr(4).ToString()
                Dim assessibleamt As Decimal = Math.Round(clsCommon.myCdbl(tdr(4).ToString()) * abatement(dataRowInfo) / 100, 2)
                ''sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"
                ''Dim strexcisable As String = connectSql.RunScalar(sql)
                If rbtnExcise.IsChecked Then
                    dataRowInfo.Cells("mrp").Value = tdr(4).ToString()
                    dataRowInfo.Cells("taxAmount").Value = 0
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(Convert.ToString(dataRowInfo.Cells("itemCode").Value), clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value), CDate(strStartDate).ToString("yyyy-MM-dd"), clsCommon.myCdbl(dataRowInfo.Cells("basicAmount").Value), Convert.ToString(dataRowInfo.Cells("unitcode").Value), fndTaxGroup.txtValue.Text, txtPriceCode.Text)
                    If dr IsNot Nothing AndAlso dr.ItemArray.Length > 0 Then
                        dataRowInfo.Cells("tax1Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax1Rate")), 2)
                        dataRowInfo.Cells("assess1").Value = Math.Round(clsCommon.myCdbl(dr("Tax1BaseAmt")), 2)
                        dataRowInfo.Cells("tax1Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax1Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells("tax2Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax2Rate")), 2)
                        dataRowInfo.Cells("assess2").Value = Math.Round(clsCommon.myCdbl(dr("Tax2BaseAmt")), 2)
                        dataRowInfo.Cells("tax2Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax2Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells("tax3Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax3Rate")), 2)
                        dataRowInfo.Cells("assess3").Value = Math.Round(clsCommon.myCdbl(dr("Tax3BaseAmt")), 2)
                        dataRowInfo.Cells("tax3Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax3Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells("tax4Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax4Rate")), 2)
                        dataRowInfo.Cells("assess4").Value = Math.Round(clsCommon.myCdbl(dr("Tax4BaseAmt")), 2)
                        dataRowInfo.Cells("tax4Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax4Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells("tax5Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax5Rate")), 2)
                        dataRowInfo.Cells("assess5").Value = Math.Round(clsCommon.myCdbl(dr("Tax5BaseAmt")), 2)
                        dataRowInfo.Cells("tax5Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax5Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells("tax6Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax6Rate")), 2)
                        dataRowInfo.Cells("assess6").Value = Math.Round(clsCommon.myCdbl(dr("Tax6BaseAmt")), 2)
                        dataRowInfo.Cells("tax6Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax6Amt")), 4, MidpointRounding.ToEven)
                    Else
                        dataRowInfo.Cells("tax1Rate").Value = 0
                        dataRowInfo.Cells("assess1").Value = 0
                        dataRowInfo.Cells("tax1Amt").Value = 0

                        dataRowInfo.Cells("tax2Rate").Value = 0
                        dataRowInfo.Cells("assess2").Value = 0
                        dataRowInfo.Cells("tax2Amt").Value = 0

                        dataRowInfo.Cells("tax3Rate").Value = 0
                        dataRowInfo.Cells("assess3").Value = 0
                        dataRowInfo.Cells("tax3Amt").Value = 0

                        dataRowInfo.Cells("tax4Rate").Value = 0
                        dataRowInfo.Cells("assess4").Value = 0
                        dataRowInfo.Cells("tax4Amt").Value = 0

                        dataRowInfo.Cells("tax5Rate").Value = 0
                        dataRowInfo.Cells("assess5").Value = 0
                        dataRowInfo.Cells("tax5Amt").Value = 0

                        dataRowInfo.Cells("tax6Rate").Value = 0
                        dataRowInfo.Cells("assess6").Value = 0
                        dataRowInfo.Cells("tax6Amt").Value = 0
                    End If

                Else
                    dataRowInfo.Cells("taxAmount").Value = 0
                    dataRowInfo.Cells("mrp").Value = tdr(4).ToString()
                    For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                        dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0
                        dataRowInfo.Cells("assess" + (gr.Index + 1).ToString()).Value = 0
                        dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0
                    Next
                End If

                dataRowInfo.Cells("totalBasicAmount").Value = "0"
                dataRowInfo.Cells("totalDiscountAmount").Value = "0"
                dataRowInfo.Cells("totalNetAmount").Value = "0"
                dataRowInfo.Cells("totalTPT").Value = "0"
                dataRowInfo.Cells("shippedQty").ReadOnly = True
                dataRowInfo.Cells("shippedQty").Value = shpQty * disRatio
                dataRowInfo.Cells("emptyValue").Value = dataRowInfo.Cells("emptyValueBottle").Value * dataRowInfo.Cells("shippedqty").Value

                Dim dblConvFact As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dataRowInfo.Cells("itemCode").Value), clsCommon.myCstr(dataRowInfo.Cells("unitCode").Value), Nothing)
                Dim dblmrp As Double = clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value)

                If dblConvFact <> 1 Then
                    dblmrp = dblmrp * dblConvFact
                End If


                dataRowInfo.Cells("batchnumber").Value = connectSql.RunScalar("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + dataRowInfo.Cells("itemCode").Value + "'  AND location_code='" + fndLocation.Value + "' AND MRP = '" + clsCommon.myCstr(dblmrp) + "'")


                dataRowInfo.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("shippedQty").Value) * clsCommon.myCdbl(dataRowInfo.Cells("taxAmount").Value), 2)
                dataRowInfo.Cells("totalMRP").Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells("shippedQty").Value) * clsCommon.myCdbl(dataRowInfo.Cells("mrp").Value), 2)
                dataRowInfo.Cells("totalItemAmount").Value = dataRowInfo.Cells("totalTaxAmount").Value
                dataRowInfo.Cells(schemeCodeCol).Value = grow.Cells(schemeCodeCol).Value

                gvLoadOut.Rows.Insert(i, dataRowInfo)
                i = i + 1
            Next
             
            For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                Dim assess As Decimal = 0
                Dim taxAmt As Decimal = 0
                For Each gr1 As GridViewRowInfo In gvLoadOut.Rows
                    If Not clsCommon.myCdbl(gr1.Cells("shippedQty").Value) = 0 Then
                        If gr1.Cells("schemeItem").Value = "No" And gr1.Cells("sampleItem").Value = "No" And gr1.Cells("promoSchemeItem").Value = "No" Then
                            assess = assess + clsCommon.myCdbl(gr1.Cells("shippedQty").Value) * clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value)
                            taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells("shippedQty").Value) * clsCommon.myCdbl(gr1.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value)
                        End If
                    End If
                Next
                If assess = 0 AndAlso taxAmt = 0 Then
                    gr.Cells("assessibleAmount").Value = assess
                    gr.Cells("taxAmount").Value = taxAmt
                End If
                If assess <> 0 Then
                    gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / assess, 2)
                    gr.Cells("assessibleAmount").Value = assess
                    gr.Cells("taxAmount").Value = taxAmt
                End If
            Next

            Dim netAmount As Decimal = 0.0
            Dim totalTPT As Decimal = 0.0
            Dim ttlCustDiscount As Decimal = 0
            Dim dblBasicAmt As Double = 0
            For Each gro As GridViewRowInfo In gvLoadOut.Rows
                If gro.Cells("shippedQty").Value <> 0 Then
                    If gro.Cells("schemeitem").Value = "No" And gro.Cells("promoschemeitem").Value = "No" Then
                        dblBasicAmt = dblBasicAmt + clsCommon.myCdbl(gro.Cells("totalBasicAmount").Value)
                        netAmount = netAmount + clsCommon.myCdbl(gro.Cells("totalNetAmount").Value)
                        totalTPT = totalTPT + clsCommon.myCdbl(gro.Cells("totalTPT").Value)
                        ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells("totalCustDiscount").Value)
                    End If
                End If
            Next
            txtShipmentTotal.Text = clsCommon.myFormat(dblBasicAmt)
            txtCustDisc.Text = ttlCustDiscount

            Dim totalTax As Decimal = 0.0
            For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
            Next
            txtTotalTaxAmount.Text = totalTax
            txtShipmentAmt.Text = netAmount + totalTax + totalTPT

        Else

        End If
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        For Each g As GridViewRowInfo In gvLoadOut.Rows
            If clsCommon.myCdbl(g.Cells("shippedQty").Value) <> 0 Or g.Cells("shippedQty").Value <> String.Empty Then
                If g.Cells("unitCode").Value = "FC" Then
                    totalfc = totalfc + clsCommon.myCdbl(g.Cells("shippedQty").Value)
                End If
                If g.Cells("unitCode").Value = "FB" Then
                    totalfb = totalfb + clsCommon.myCdbl(g.Cells("shippedQty").Value)
                End If
            End If
        Next
        If totalfc = 0 Then
            lblfb.Text = CStr(totalfb)
        ElseIf totalfb = 0 Then
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb <> 0 And totalfc <> 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb = 0 And totalfc = 0 Then
            lblfc.Text = 0
            lblfb.Text = 0
        End If
        AddHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
    End Sub

    Private Function validateData() As Boolean


        Return True

    End Function

    Private Function isAllTargetItem() As Boolean
        For ii As Integer = 0 To gvLoadOut.Rows.Count - 1
            If clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedQty").Value) > 0 Then
                If clsCommon.myLen(gvLoadOut.Rows(ii).Cells(colDiscountCode).Value) <= 0 Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Function GetRecoverableAmt(ByRef dblTaxRecAmt As Double, ByRef dblTaxRecAmt2 As Double, ByRef dblTaxRecAmt3 As Double) As Double
        Dim dblTotDisPer As Double = 0

        If isAllTargetItem() Then
            dblTotDisPer = 100
        Else
            If clsCommon.myCdbl(txtShipmentTotal.Text) = 0 Then
                dblTotDisPer = 0
            Else
                dblTotDisPer = (clsCommon.myCdbl(txtCustDisc.Text) + clsCommon.myCdbl(txtDiscAmt.Text)) * 100 / clsCommon.myCdbl(txtShipmentTotal.Text)
            End If
        End If

        dblTotDisPer = Math.Round(dblTotDisPer, 2, MidpointRounding.ToEven)
        Dim dblTax1Amt As Double = 0
        Dim dblTax2Amt As Double = 0
        Dim dblTax3Amt As Double = 0
        For ii As Integer = 0 To gvLoadOut.Rows.Count - 1
            dblTax1Amt += clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("tax1Amt").Value) * clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedQty").Value)
            dblTax2Amt += clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("tax2Amt").Value) * clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedQty").Value)
            dblTax3Amt += clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("tax3Amt").Value) * clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedQty").Value)
        Next
        dblTaxRecAmt = dblTax1Amt - ((dblTotDisPer * dblTax1Amt) / 100)
        dblTaxRecAmt = Math.Round(dblTaxRecAmt, 2, MidpointRounding.ToEven)

        dblTaxRecAmt2 = dblTax2Amt - ((dblTotDisPer * dblTax2Amt) / 100)
        dblTaxRecAmt2 = Math.Round(dblTaxRecAmt2, 2, MidpointRounding.ToEven)

        dblTaxRecAmt3 = dblTax3Amt - ((dblTotDisPer * dblTax3Amt) / 100)
        dblTaxRecAmt3 = Math.Round(dblTaxRecAmt3, 2, MidpointRounding.ToEven)

        Return dblTotDisPer
    End Function

    Private Sub SaveDataNew(ByVal mrp As Decimal, ByVal basic As Decimal, ByVal assessible As Decimal, ByVal detailDiscount As Decimal, ByVal shipmentDiscAmt As Decimal, ByVal shipmentDiscPer As Decimal, ByVal shipmentTaxAmt As Decimal, ByVal netAmount As Decimal, ByVal additionalCharges As Decimal, ByVal OtherCharges As Decimal, ByVal freightCharges As Decimal, ByVal totalTPT As Decimal)

        Try

            Dim emptyvalue As Decimal = 0
            For Each grow As GridViewRowInfo In gvLoadOut.Rows
                If Not clsCommon.myCdbl(grow.Cells("shippedQty").Value) = 0 Then
                    emptyvalue = emptyvalue + grow.Cells("emptyvalue").Value
                End If
            Next
            Dim totaltaxamt As Decimal = 0
            For Each g As GridViewRowInfo In gvTaxDetails.Rows
                totaltaxamt = totaltaxamt + clsCommon.myCdbl(g.Cells("taxamount").Value)
            Next
            txtTotalTaxAmount.Text = totaltaxamt
            emptyvalue = clsCommon.myCdbl(txtshellqty.Text) * 100 + clsCommon.myCdbl(emptyvalue)
            emptyvalue123 = emptyvalue

            Dim priceDate As Date = CDate(ddlPriceDate.Text).ToString("dd/MM/yyyy")

            Dim dblTaxRecAmt As Double = 0
            Dim dblTaxRecAmt2 As Double = 0
            Dim dblTaxRecAmt3 As Double = 0
            Dim dblTotDisPer As Double = GetRecoverableAmt(dblTaxRecAmt, dblTaxRecAmt2, dblTaxRecAmt3)
            Dim obj As clsSaleReturnInterCompany = New clsSaleReturnInterCompany()

            If funvalidatevehicle() Then
                obj.Employee_Code = fndemployeecode.txtValue.Text
                obj.Shell_Qty = txtshellqty.Text
                obj.Customer_Invoice_No = txtcustomerinvoiceno.Text
                obj.Document_Type = IIf(rbtnExcise.IsChecked, 0, 1)
                obj.Document_No = txtDocumentNo.Value
                obj.Document_Date = dtpShipDate.Value
                obj.Cust_Code = fndCustomer.Value
                obj.Cust_Name = txtCustomerName.Text
                obj.On_Hold = IIf(chkOnHold.Checked, True, False)
                obj.Ref_No = txtRef.Text
                obj.Description = txtDesc.Text
                obj.Remarks = txtRemarks.Text
                obj.Price_Code = txtPriceCode.Text
                obj.Tax_Group = fndTaxGroup.txtValue.Text
                obj.Location = fndLocation.Value
                obj.Cust_Account = strCustAccount
                obj.Total_Assessable_Amount = assessible
                ''obj.DIS shipmentDiscPer),
                obj.Discount_Amt = shipmentDiscAmt
                obj.Detail_Disc_Amt = detailDiscount
                obj.Detail_Total_Amt = clsCommon.myCdbl(MyTextBox3.Text)
                obj.Tax_Amt = shipmentTaxAmt
                obj.Freight_Amt = freightCharges
                obj.Other_Charges = OtherCharges
                obj.Add_Charges = additionalCharges
                obj.Total_Order_Amt = txtShipmentAmt.Text
                obj.Salesman_Code = fndSalesman.txtValue.Text
                obj.Mode_Of_Transport = ddlModeofTransport.Text
                obj.Vehicle_Code = fndVehicleCode.txtValue.Text
                obj.Vehicle_No = txtVhicleNo.Text
                obj.KM_Reading = txtKMReading.Text
                obj.Route_No = fndRouteNo.txtValue.Text
                obj.Route_Desc = txtRouteDesc.Text
                obj.Trip_No = txtTripNo.Text
                obj.Price_Date = priceDate
                obj.Terms_Code = fndPaymentTerms.txtValue.Text
                obj.Comments = txtRemarks.Text
                obj.Level1_User_code = l1User
                obj.Level2_User_code = l2User
                obj.Level3_User_code = l3User
                obj.Level4_User_code = l4User
                obj.Level5_User_code = l5User
                obj.Total_TPT = txttotaltpt.Text
                obj.Total_Disc_Percent = dblTotDisPer
                obj.Tax_Recoverable_Amt = dblTaxRecAmt
                obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(txtCustDisc.Text)
                obj.Tax_Recoverable_Amt2 = dblTaxRecAmt2
                obj.Tax_Recoverable_Amt3 = dblTaxRecAmt3
                obj.Empty_Value = emptyvalue
                If (gvTaxDetails.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gvTaxDetails.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(0).Cells(colTAssessibleAmount).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gvTaxDetails.Rows(1).Cells(colTTaxAutCode).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(1).Cells(colTTaxRate).Value)
                    obj.TAX2_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(1).Cells(colTAssessibleAmount).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gvTaxDetails.Rows(2).Cells(colTTaxAutCode).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(2).Cells(colTTaxRate).Value)
                    obj.TAX3_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(2).Cells(colTAssessibleAmount).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gvTaxDetails.Rows(3).Cells(colTTaxAutCode).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(3).Cells(colTTaxRate).Value)
                    obj.TAX4_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(3).Cells(colTAssessibleAmount).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gvTaxDetails.Rows(4).Cells(colTTaxAutCode).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(4).Cells(colTTaxRate).Value)
                    obj.TAX5_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(4).Cells(colTAssessibleAmount).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gvTaxDetails.Rows(5).Cells(colTTaxAutCode).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(5).Cells(colTTaxRate).Value)
                    obj.TAX6_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(5).Cells(colTAssessibleAmount).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gvTaxDetails.Rows(6).Cells(colTTaxAutCode).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(6).Cells(colTTaxRate).Value)
                    obj.TAX7_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(6).Cells(colTAssessibleAmount).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gvTaxDetails.Rows(7).Cells(colTTaxAutCode).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(7).Cells(colTTaxRate).Value)
                    obj.TAX8_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(7).Cells(colTAssessibleAmount).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gvTaxDetails.Rows(8).Cells(colTTaxAutCode).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(8).Cells(colTTaxRate).Value)
                    obj.TAX9_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(8).Cells(colTAssessibleAmount).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gvTaxDetails.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gvTaxDetails.Rows(9).Cells(colTTaxAutCode).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gvTaxDetails.Rows(9).Cells(colTTaxRate).Value)
                    obj.TAX10_Assessable_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(9).Cells(colTAssessibleAmount).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gvTaxDetails.Rows(9).Cells(colTTaxAmt).Value)
                End If

                obj.Ship_To = txtShipTo.Value
                obj.Ship_To_Desc = lblShipTo.Text
            End If

            obj.Arr = New List(Of clsSaleReturnInterCompanyDetail)
            Dim shipmentId As Integer = 1
            For Each grow As GridViewRowInfo In gvLoadOut.Rows
                If clsCommon.myCdbl(grow.Cells("shippedQty").Value) > 0 Then
                    Dim objtr As New clsSaleReturnInterCompanyDetail()
                    Dim abat As Decimal = abatement(grow)
                    Dim schemeItemCode As String = grow.Cells("schemeCodeItem").Value
                    If String.IsNullOrEmpty(schemeItemCode) Then
                        schemeItemCode = ""
                    End If
                    priceDate = CDate(grow.Cells("priceDate").Value).ToString("dd/MM/yyyy")
                    Dim dblItemNetAmt As Double = clsCommon.myCdbl(grow.Cells("itemNetAmount").Value)

                    'objtr.Sample_Scheme_Code = fndSchemeSample.txtValue.Text
                    objtr.Main_Item = clsCommon.myCstr(grow.Cells("mainitem").Value)
                    objtr.Batch_No = clsCommon.myCstr(grow.Cells("batchnumber").Value)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells("itemCode").Value)
                    objtr.Item_Desc = clsCommon.myCstr(grow.Cells("itemName").Value)
                    objtr.Price_Date = priceDate
                    objtr.Qty = clsCommon.myCdbl(grow.Cells("shippedQty").Value)

                    objtr.Actual_Return_Qty = clsCommon.myCdbl(grow.Cells(colActualRetrunQty).Value)
                    objtr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objtr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objtr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)

                    objtr.Unit_code = clsCommon.myCstr(grow.Cells("unitCode").Value)
                    objtr.Price_code = clsCommon.myCstr(grow.Cells("priceCode").Value)
                    objtr.Scheme_Applicable = schemeItemApplicable(grow)
                    objtr.Scheme_Code_Qty = schemeItemCode
                    objtr.Scheme_Item = schemeItem(grow)
                    objtr.Scheme_Disc_Applicable = schemeDiscApplicable(grow)
                    objtr.Scheme_Code_Cash = clsCommon.myCstr(grow.Cells("schemeCodeDiscount").Value)
                    objtr.Sampling_Item = sampleItem(grow)
                    objtr.Empty_Value = clsCommon.myCstr(grow.Cells("emptyValue").Value)
                    objtr.Empty_Value_Shell = clsCommon.myCstr(grow.Cells("emptyValueShell").Value)
                    objtr.Empty_Value_Bottle = clsCommon.myCdbl(grow.Cells("emptyValueBottle").Value)
                    objtr.MRP_Amt = clsCommon.myCdbl(grow.Cells("mrp").Value)
                    objtr.Basic_Rate = clsCommon.myCdbl(grow.Cells("basicAmount").Value)
                    objtr.Item_Assessable_Rate = itemAssessibleAmt(grow)
                    objtr.Disc_Amt = clsCommon.myCdbl(grow.Cells("discountAmount").Value)
                    objtr.Item_Net_Amt = dblItemNetAmt
                    objtr.Item_Tax = clsCommon.myCdbl(grow.Cells("taxAmount").Value)
                    objtr.Total_Assessable_Amt = totalItemAssessibleAmt(grow)
                    objtr.Total_MRP_Amt = clsCommon.myCdbl(grow.Cells("totalMRP").Value)
                    objtr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells("totalBasicAmount").Value)
                    objtr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells("totalDiscountAmount").Value)
                    objtr.Total_net_Amt = clsCommon.myCdbl(grow.Cells("totalNetAmount").Value)
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells("totaltaxAmount").Value)
                    objtr.TPT = clsCommon.myCdbl(grow.Cells("tpt").Value)
                    objtr.Total_Item_Amt = clsCommon.myCdbl(grow.Cells("totalItemAmount").Value)
                    objtr.Total_TPT = clsCommon.myCdbl(grow.Cells("totalTPT").Value)
                    objtr.Promo_Scheme_Applicable = clsCommon.myCstr(promoSchemeApplicable(grow))
                    objtr.Promo_Scheme_Code = clsCommon.myCstr(grow.Cells("promoSchemeCode").Value)
                    objtr.Promo_Scheme_Item = clsCommon.myCstr(promoSchemeItem(grow))
                    objtr.Abatement = abat
                    objtr.Cust_Discount = clsCommon.myCdbl(grow.Cells("custDiscount").Value)
                    objtr.Total_Cust_Discount = clsCommon.myCdbl(grow.Cells("totalCustDiscount").Value)

                    Dim convFact As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, Nothing)
                    Dim qry As String = "select Cost from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "'"
                    objtr.Unit_COGS = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) / convFact, 2)

                    Dim objConvFac As clsTempUOMForConversion = clsTempUOMForConversion.GetConvertsionFactors(clsCommon.myCstr(grow.Cells(ColICode).Value), Nothing)
                    Dim RAWQty As Double = 0
                    Dim ConvertQty As Double = 0
                    Dim OZQty As Double = 0

                    If objConvFac IsNot Nothing Then
                        If clsCommon.CompairString("FB", clsCommon.myCstr(grow.Cells(colUnitCode).Value)) = CompairStringResult.Equal Then
                            RAWQty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) / (objConvFac.Raw)
                        Else
                            RAWQty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                        End If
                        RAWQty = Math.Round(RAWQty, 2, MidpointRounding.ToEven)
                        If objConvFac.Converted <> 0 Then
                            ConvertQty = RAWQty * (objConvFac.Raw) / objConvFac.Converted
                            ConvertQty = Math.Round(ConvertQty, 2, MidpointRounding.ToEven)
                        End If

                        OZQty = ConvertQty * objConvFac.OZ
                        OZQty = Math.Round(OZQty, 2, MidpointRounding.ToEven)
                    End If

                    Dim Abatmentrate As Decimal = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                    objtr.Transfer_Basic_Amount = clsCommon.myCdbl(grow.Cells("transferBasicAmount").Value)
                    objtr.From_Scheme_Code = clsCommon.myCstr(grow.Cells("fromSchemeCode").Value)
                    'objtr.Target_Discount_Amt='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(ColTargetDisAmt).Value)) + "',
                    objtr.Price_To_Show = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(ColPriceToShow).Value))
                    If clsCommon.myLen(grow.Cells(colPriceDateActual).Value) > 0 Then
                        objtr.Price_Date_Actual = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colPriceDateActual).Value), "dd/MMM/yyyy")
                    End If
                    objtr.RAW_Qty = clsCommon.myCstr(RAWQty)
                    objtr.Converted_Qty = ConvertQty
                    objtr.OZ_Qty = OZQty
                    objtr.Abatement_rate = Abatmentrate
                    objtr.TAX1 = obj.TAX1
                    objtr.TAX1_Rate = clsCommon.myCdbl(grow.Cells("Tax1Rate").Value)
                    objtr.TAX1_Amt = clsCommon.myCdbl(grow.Cells("Tax1Amt").Value)
                    objtr.TAX1_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess1").Value)

                    objtr.TAX2 = obj.TAX2
                    objtr.TAX2_Rate = clsCommon.myCdbl(grow.Cells("Tax2Rate").Value)
                    objtr.TAX2_Amt = clsCommon.myCdbl(grow.Cells("Tax2Amt").Value)
                    objtr.TAX2_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess2").Value)

                    objtr.TAX3 = obj.TAX3
                    objtr.TAX3_Rate = clsCommon.myCdbl(grow.Cells("Tax3Rate").Value)
                    objtr.TAX3_Amt = clsCommon.myCdbl(grow.Cells("Tax3Amt").Value)
                    objtr.TAX3_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess3").Value)
                    objtr.TAX4 = obj.TAX4
                    objtr.TAX4_Rate = clsCommon.myCdbl(grow.Cells("Tax4Rate").Value)
                    objtr.TAX4_Amt = clsCommon.myCdbl(grow.Cells("Tax4Amt").Value)
                    objtr.TAX4_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess4").Value)
                    objtr.TAX5 = obj.TAX5
                    objtr.TAX5_Rate = clsCommon.myCdbl(grow.Cells("Tax5Rate").Value)
                    objtr.TAX5_Amt = clsCommon.myCdbl(grow.Cells("Tax5Amt").Value)
                    objtr.TAX5_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess5").Value)
                    objtr.TAX6 = obj.TAX6
                    objtr.TAX6_Rate = clsCommon.myCdbl(grow.Cells("Tax6Rate").Value)
                    objtr.TAX6_Amt = clsCommon.myCdbl(grow.Cells("Tax6Amt").Value)
                    objtr.TAX6_Assessable_Amt = clsCommon.myCdbl(grow.Cells("assess6").Value)
                    obj.Arr.Add(objtr)
                End If
            Next
            If obj.SaveData(obj, isNewEntry) Then
                If clicked = False Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                End If

                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenSettlementEntry(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction)
        If clsCommon.myCdbl(txtShipmentAmt.Text) > 0 AndAlso clsCommon.myLen(strInvoiceNo) > 0 Then
            Dim frm As frmAdj = New frmAdj()
            frm.isFromLoadout = True
            frm.strCustomerNo = fndCustomer.Value
            frm.strCustomerName = txtCustomerName.Text
            frm.strDocumnentNo = strInvoiceNo
            frm.dblDocumnentAmt = clsCommon.myCdbl(txtShipmentAmt.Text)
            frm.dtLoadOut = dtpShipDate.Value
            frm.ShowDialog()
            ''Dim qry As String = "select SUM(Adjustment_Amount)  from TSPL_Receipt_Adjustment_Header where Doc_No='" + strInvoiceNo + "' and Is_Post='Y'"
            ''Dim dblAdjAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            ''txtRcptAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(txtRcptAmt.Text) - dblAdjAmt)
        End If
    End Sub

    Private Sub updateItemLocationDetails(ByVal trans As SqlTransaction, ByVal grow As GridViewRowInfo, ByVal shipmentNo As String)
        Dim itemQty As Decimal = 0
        Dim cogs As Decimal = 0
        Dim unitCogs As Decimal = 0
        Dim shipmentCogs As Decimal = 0
        Dim itemlocationqty As Decimal = 0
        Dim amount As Decimal = 0
        Dim shipmentcogs1 As Decimal = 0
        Dim shipmentqty1 As Decimal = 0
        Dim shipmentamt1 As Decimal = 0
        Dim convertFact As Decimal = connectSql.ReturnConvFact(Convert.ToString(grow.Cells("itemcode").Value), Convert.ToString(grow.Cells("unitcode").Value), trans)


        sql = "SELECT sum(isnull(Amount,0))/sum(isnull(Item_Qty,0))  FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells("itemCode").Value + "' " & _
     " AND location_code='" + fndLocation.Value + "' and MRP='" + (clsCommon.myCdbl(grow.Cells("mrp").Value) * convertFact).ToString() + "'"
        unitCogs = Math.Round(clsCommon.myCdbl(connectSql.RunScalar(trans, sql)), 2)

        '--------------------------------update Item location details---------------------------------
        Dim countloop As Decimal = 0
        Dim shippedqty As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells("shippedQty").Value / convertFact), 2)

        Dim batchnumber As String = ""
        Dim balanceqty1 As Decimal = 0
        Dim applyqty As Decimal = 0
        Dim b As String = ""
        sql = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells("itemCode").Value + "' " & _
" AND location_code='" + grow.Cells("location").Value + "' and MRP='" + (clsCommon.myCdbl(grow.Cells("mrp").Value) * convertFact).ToString() + "' and batch_no <> '' order by Expiry_Date asc"
        Dim dt As DataTable
        ds = connectSql.RunSQLReturnDS(trans, sql)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For k As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myCdbl(dt.Rows(k)("Item_Qty")) <> 0 Then
                    If clsCommon.myCdbl(dt.Rows(k)("Item_Qty")) <> 0 Or clsCommon.myCdbl(dt.Rows(k)("amount")) <> 0 Then
                        itemQty = clsCommon.myCdbl(dt.Rows(k)("Item_Qty"))
                        cogs = clsCommon.myCdbl(dt.Rows(k)("Amount"))
                        unitCogs = Math.Round(cogs / itemQty, 2)
                        batchnumber = Convert.ToString(dt.Rows(k)("Batch_No"))
                        If shippedqty > itemQty Then
                            applyqty = itemQty
                            shippedqty = shippedqty - itemQty
                            shipmentqty1 = shipmentqty1 + applyqty
                            shipmentamt1 = shipmentamt1 + cogs
                        Else
                            applyqty = shippedqty
                            shippedqty = shippedqty - applyqty
                            shipmentqty1 = shipmentqty1 + applyqty
                            shipmentamt1 = shipmentamt1 + Math.Round(cogs / itemQty * applyqty, 2)
                            shipmentcogs1 = Math.Round(shipmentamt1 / shipmentqty1, 2)
                        End If

                    End If
                    If applyqty > 0 Then
                        itemlocationqty = itemQty - applyqty
                        amount = cogs - (unitCogs * applyqty)
                        itemlocationqty = Math.Round(itemlocationqty, 2)
                        amount = Math.Round(amount, 2)
                        If itemlocationqty < 0.04 Then
                            itemlocationqty = 0
                            amount = 0
                        End If
                        sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(itemlocationqty) + "', " & _
                            "Amount='" + Convert.ToString(amount) + "' where Item_Code='" + grow.Cells("itemCode").Value + "' " & _
                            " AND location_code='" + grow.Cells("location").Value + "' and MRP='" + (clsCommon.myCdbl(grow.Cells("mrp").Value) * convertFact).ToString() + "' and batch_no = '" + batchnumber + "'"
                        connectSql.RunSqlTransaction(trans, sql)
                    End If
                    If shippedqty = 0 Then
                        Exit For
                    End If
                End If
            Next
            sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Unit_COGS='" + shipmentcogs1.ToString() + "' WHERE Item_Code='" + grow.Cells("itemCode").Value + "' AND " & _
                " Shipment_No='" + shipmentNo + "' AND Unit_Code='" + grow.Cells("unitCode").Value + "' AND " & _
                " Price_Date='" + Format(CDate(grow.Cells("priceDate").Value), "MM/dd/yyyy") + "' AND MRP_Amt='" + grow.Cells("mrp").Value + "' AND Transfer_Basic_Amount='" + grow.Cells("transferBasicAmount").Value + "'"
            connectSql.RunSqlTransaction(trans, sql)

        End If
    End Sub

    Private Sub totalAmounts()
        Dim otherCharges As Decimal
        Dim additionalCharges As Decimal
        Dim freight As Decimal
        Dim discountAmt As Decimal
        Dim totalTaxAmt As Decimal
        Dim shipmentTotal As Decimal
        If txtOtherCharges.Text = String.Empty Then
            otherCharges = 0.0
        Else
            otherCharges = clsCommon.myCdbl(txtOtherCharges.Text)
        End If
        If txtAdditionalCharges.Text = String.Empty Then
            additionalCharges = 0.0
        Else
            additionalCharges = clsCommon.myCdbl(txtAdditionalCharges.Text)
        End If
        If txtFreight.Text = String.Empty Then
            freight = 0.0
        Else
            freight = clsCommon.myCdbl(txtFreight.Text)
        End If
        If txtCustDisc.Text = String.Empty Then
            discountAmt = 0.0
        Else
            discountAmt = clsCommon.myCdbl(txtCustDisc.Text)
        End If
        If txtTotalTaxAmount.Text = String.Empty Then
            totalTaxAmt = 0
        Else
            totalTaxAmt = txtTotalTaxAmount.Text
        End If
        If txtShipmentTotal.Text = String.Empty Then
            shipmentTotal = 0
        Else
            shipmentTotal = txtShipmentTotal.Text
        End If
        'Dim shipmentDiscPer As Decimal
        'Dim shipmentDiscAmt As Decimal
        txtShipmentAmt.Text = Math.Round(shipmentTotal + totalTaxAmt + otherCharges + additionalCharges + freight + totalTransport(), 4)
    End Sub

    Private Sub priceDateSelection(Optional ByVal items As String = "All")
        priceDateSelection(False, items)
            End Sub

    Private Sub priceDateSelection(ByVal isForUpdate As Boolean, Optional ByVal items As String = "All")
                isInsideLoadData = True

        Dim tptcheck As String = "N"
        If Not isForUpdate Then
            gvLoadOut.DataSource = Nothing
            gvLoadOut.Rows.Clear()
        End If
        If items = "FB" Or rbFB.ToggleState = ToggleState.On Then
            sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate FROM View_TSPL_SHIPMENT_ITEMS  Where Show='N' AND UOM='FB' and  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "'      Order By Sku_Seq,Start_Date,UOM desc"
        ElseIf items = "FC" Or rbFC.ToggleState = ToggleState.On Then
            sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate FROM View_TSPL_SHIPMENT_ITEMS  Where Show='N' AND  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "'  "
            If isForUpdate Then
                sql += " And item_code not in (select Item_Code from TSPL_SALE_RETURN_INTER_DETAIL where Document_No = '" + txtDocumentNo.Value + "')"
            Else
                sql += " AND UOM='FC'"
            End If
            sql += "  Order By Sku_Seq,Start_Date,UOM desc"
        ElseIf items = "All" Then
            sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate FROM View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Text.Trim() + "' AND  ITEM_TYPE = 'F' and Tax_group = '" + fndTaxGroup.txtValue.Text + "'   Order By Sku_Seq,Start_Date,UOM desc"
        End If

        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        Dim i As Integer = 0
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows
                ' Dim isAddNewRow As Boolean = True

                'Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr5("Item_Code")), clsCommon.myCstr(dr5("UOM")), Nothing)
                'Dim sql2 As String = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + dr5(0).ToString() + "' AND Location_Code='" + fndLocation.Value + "' and MRP='" + clsCommon.myCstr(clsCommon.myCdbl(dr5(5)) * convFac) + "'"
                'Dim balanceamt As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql2))

                'If balanceamt > 0 Then
                '    isAddNewRow = True
                'End If

                'If convFac <> 1 Then
                '    balanceamt = 0
                'End If

                'If isAddNewRow Then
                Dim datarowinfo As GridViewRowInfo = gvLoadOut.Rows.AddNew()
                datarowinfo.Cells("itemCode").Value = tdr(0).ToString()
                datarowinfo.Cells("itemcode").ReadOnly = True
                datarowinfo.Cells("itemName").Value = tdr(1).ToString()
                Dim colPriceDate As GridViewComboBoxColumn = TryCast(gvLoadOut.Columns("priceDate"), GridViewComboBoxColumn)
                sql = "Select  CONVERT(varchar(10), Start_Date, 103) as Start_Date from TSPL_ITEM_PRICE_MASTER Where Item_Code='" + datarowinfo.Cells("itemCode").Value + "' and Price_Code='" + txtPriceCode.Text + "'"
                ds = connectSql.RunSQLReturnDS(sql)
                colPriceDate.ValueMember = "Start_Date"
                colPriceDate.DataSource = ds.Tables(0)
                datarowinfo.Cells("priceDate").Value = tdr(2).ToString()
                datarowinfo.Cells("priceDate").ReadOnly = True

                datarowinfo.Cells("shippedQty").Value = "0"
                datarowinfo.Cells("unitCode").Value = tdr(3).ToString()

                If datarowinfo.Cells("itemCode").Value.ToString().Trim() = "7n600pfc" Then
                    common.clsCommon.MyMessageBoxShow("itemcaught")
                End If

                datarowinfo.Cells("priceCode").Value = tdr(4).ToString()
                datarowinfo.Cells("schemeApplicable").Value = "No"
                datarowinfo.Cells("promoSchemeApplicable").Value = "No"
                datarowinfo.Cells("promoSchemeItem").Value = "No"
                datarowinfo.Cells("promoSchemeCode").Value = ""
                datarowinfo.Cells("schemeCodeItem").Value = ""
                datarowinfo.Cells("schemeItem").Value = "No"
                datarowinfo.Cells("schemeDiscountApplicable").Value = "No"
                datarowinfo.Cells("schemeCodeDiscount").Value = ""
                datarowinfo.Cells("sampleItem").Value = "No"
                datarowinfo.Cells("emptyValue").Value = Convert.ToDecimal(tdr(7).ToString()) + Convert.ToDecimal(tdr(8).ToString())
                datarowinfo.Cells("emptyValueShell").Value = Convert.ToDecimal(tdr(7).ToString())
                datarowinfo.Cells("emptyValueBottle").Value = Convert.ToDecimal(tdr(8).ToString())
                datarowinfo.Cells("mrp").Value = tdr(5).ToString()

                Dim assessibleamt As Decimal = 0
                datarowinfo.Cells("basicAmount").Value = tdr(6).ToString()
                datarowinfo.Cells("transferBasicAmount").Value = tdr(6).ToString()
                assessibleamt = Math.Round(clsCommon.myCdbl(tdr(5).ToString()) * abatement(datarowinfo) / 100, 2)

                datarowinfo.Cells(colAbatementRate).Value = tdr("Abatement_Rate").ToString()
                Dim discAmount As String = 0

                Dim dblFirstTaxRate As Double = clsCommon.myCdbl(tdr("TAX1_Rate"))
                Dim dblCustDisWithoutTax As Double = Math.Round(discAmount * 100 / (100 + dblFirstTaxRate), 2, MidpointRounding.ToEven)


                datarowinfo.Cells(ColCustDisNoTax).Value = dblCustDisWithoutTax
                datarowinfo.Cells("itemNetAmount").Value = clsCommon.myCdbl(datarowinfo.Cells("basicAmount").Value) - dblCustDisWithoutTax
                datarowinfo.Cells("custDiscount").Value = discAmount
                datarowinfo.Cells("totalCustDiscount").Value = 0
                datarowinfo.Cells("discountAmount").Value = "0"

                For counttax As Integer = 1 To 6
                    Dim taxrate As String = "Tax" + counttax.ToString() + "_Rate"
                    Dim taxr As String = "tax" + counttax.ToString() + "rate"
                    datarowinfo.Cells(taxr).Value = Convert.ToString(tdr(taxrate))
                Next
                datarowinfo.Cells("TaxAmount").Value = "0"


                If Not isForUpdate Then
                    SnDUtility.calculateTax(0, 0, fndLocation.Value, gvLoadOut, gvTaxDetails)
                End If


                datarowinfo.Cells("totalMRP").Value = "0"
                datarowinfo.Cells("totalBasicAmount").Value = "0"
                datarowinfo.Cells("totalDiscountAmount").Value = "0"
                datarowinfo.Cells("totalNetAmount").Value = "0"
                datarowinfo.Cells("totalTaxAmount").Value = "0"
                sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + datarowinfo.Cells("itemCode").Value + "' AND " & _
                  " Uom_Code='" + datarowinfo.Cells("unitCode").Value + "'"
                Dim convertFact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(sql))
                Dim tptdr As DataTable
                Dim tpt As String = ""
                tptdr = clsDBFuncationality.GetDataTable("SELECT  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,(TSPL_ITEM_PRICE_MASTER.NetLTPT+TSPL_ITEM_PRICE_MASTER.Price_Amount10) as PriceToShow   FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code = '" + datarowinfo.Cells("itemCode").Value + "' AND Item_Basic_Net ='" + tdr("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + tdr("Item_Basic_Price").ToString() + "'")
                If tptdr IsNot Nothing AndAlso tptdr.Rows.Count > 0 Then


                    datarowinfo.Cells(ColPriceToShow).Value = clsCommon.myCdbl(tptdr.Rows(0)("PriceToShow"))
                    For j As Integer = 1 To 10
                        Dim Price_Amount As String = "Price_Amount" + j.ToString()
                        Dim Price_Comp As String = "Price_Comp" + j.ToString()
                        tptcheck = connectSql.RunScalar("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tptdr.Rows(0)(Price_Comp)) + "'")
                        If tptcheck = "Y" Then
                            tpt = Convert.ToString(tptdr.Rows(0)(Price_Amount))
                            Exit For
                        End If
                    Next
                End If
                datarowinfo.Cells("tpt").Value = tpt
                datarowinfo.Cells("totalItemAmount").Value = "0"
                datarowinfo.Cells("totalTPT").Value = "0"
                datarowinfo.Cells("fromSchemeCode").Value = ""
                datarowinfo.Cells("batchnumber").Value = connectSql.RunScalar("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + datarowinfo.Cells("itemCode").Value + "' AND Location_Code = '" + fndLocation.Value + "'")
                'End If
            Next
            
            bindgvLoadOut1Columns()
            gvLoadOut.AllowAddNewRow = False
            AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
            AddHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
            AddHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
            AddHandler txtFreight.TextChanged, AddressOf totalAmounts
        End If
        gvLoadOut.AllowAddNewRow = True
        isInsideLoadData = False
            End Sub

    Private Sub currentmanualscheme(ByVal itemcode As String, ByVal startdate As String, ByVal mrp As Decimal, ByVal strUOM As String)
        Dim tptcheck As String = "N"
        sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle,UOM FROM View_TSPL_SHIPMENT_ITEMS  Where Show='N' AND ITEM_CODE = '" + itemcode + "' AND UOM='" + strUOM + "' and  Price_Code='" + txtPriceCode.Text.Trim() + "' and Start_Date = '" + startdate + "' and Item_Basic_Net = '" + CStr(mrp) + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + fndLocation.Value + "' AND Item_Qty <> 0)  Order By Sku_Seq"
        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows
                gvLoadOut.CurrentRow.Cells("complete").Value = "Yes"
                gvLoadOut.CurrentRow.Cells("itemCode").Value = itemcode
                gvLoadOut.CurrentRow.Cells("itemName").Value = Convert.ToString(tdr(1))
                gvLoadOut.CurrentRow.Cells("priceDate").Value = startdate
                gvLoadOut.CurrentRow.Cells("priceDate").ReadOnly = True
                gvLoadOut.CurrentRow.Cells("orderedQty").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("shippedQty").Value = "1"
                gvLoadOut.CurrentRow.Cells("unitCode").Value = clsCommon.myCstr(tdr("UOM"))  ''"FB"
                gvLoadOut.CurrentRow.Cells("unitCode").ReadOnly = True

                gvLoadOut.CurrentRow.Cells("balanceQty").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("priceCode").Value = tdr(4).ToString()
                gvLoadOut.CurrentRow.Cells("schemeApplicable").Value = "No"
                gvLoadOut.CurrentRow.Cells("promoSchemeApplicable").Value = "No"
                gvLoadOut.CurrentRow.Cells("promoSchemeItem").Value = "No"
                gvLoadOut.CurrentRow.Cells("promoSchemeCode").Value = ""
                gvLoadOut.CurrentRow.Cells("schemeCodeItem").Value = ""
                gvLoadOut.CurrentRow.Cells("schemeItem").Value = "Yes"
                gvLoadOut.CurrentRow.Cells("schemeDiscountApplicable").Value = "No"
                gvLoadOut.CurrentRow.Cells("schemeCodeDiscount").Value = ""
                gvLoadOut.CurrentRow.Cells("sampleItem").Value = "No"
                gvLoadOut.CurrentRow.Cells("emptyValue").Value = Convert.ToString(clsCommon.myCdbl(tdr("Empty_Value_Bottle") + tdr("Empty_Value_Shell")))
                gvLoadOut.CurrentRow.Cells("emptyValueShell").Value = clsCommon.myCstr(clsCommon.myCdbl(tdr("Empty_Value_Shell")))
                gvLoadOut.CurrentRow.Cells("emptyValueBottle").Value = Convert.ToString(tdr("Empty_Value_Bottle"))
                gvLoadOut.CurrentRow.Cells("mrp").Value = tdr(5).ToString()
                gvLoadOut.CurrentRow.Cells("basicAmount").Value = tdr(6).ToString()
                gvLoadOut.CurrentRow.Cells("transferBasicAmount").Value = tdr(6).ToString()
                gvLoadOut.CurrentRow.Cells("itemNetAmount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("custDiscount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("totalCustDiscount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("discountAmount").Value = "0.00"
                'sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"
                'Dim strexcisable As String = connectSql.RunScalar(sql)
                If rbtnExcise.IsChecked Then
                    Dim strDate As String = ""
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(itemcode, clsCommon.myCdbl(mrp), clsCommon.GetPrintDate(startdate, "yyyy-MM-dd"), clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("basicAmount").Value), Convert.ToString(gvLoadOut.CurrentRow.Cells("unitcode").Value), fndTaxGroup.txtValue.Text, txtPriceCode.Text)
                    If dr IsNot Nothing AndAlso dr.ItemArray.Length > 0 Then
                        gvLoadOut.CurrentRow.Cells("tax1Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax1Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess1").Value = Math.Round(clsCommon.myCdbl(dr("Tax1BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax1Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax1Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("tax2Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax2Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess2").Value = Math.Round(clsCommon.myCdbl(dr("Tax2BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax2Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax2Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("tax3Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax3Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess3").Value = Math.Round(clsCommon.myCdbl(dr("Tax3BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax3Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax3Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("tax4Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax4Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess4").Value = Math.Round(clsCommon.myCdbl(dr("Tax4BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax4Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax4Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("tax5Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax5Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess5").Value = Math.Round(clsCommon.myCdbl(dr("Tax5BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax5Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax5Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("tax6Rate").Value = Math.Round(clsCommon.myCdbl(dr("Tax6Rate")), 2)
                        gvLoadOut.CurrentRow.Cells("assess6").Value = Math.Round(clsCommon.myCdbl(dr("Tax6BaseAmt")), 2)
                        gvLoadOut.CurrentRow.Cells("tax6Amt").Value = Math.Round(clsCommon.myCdbl(dr("Tax6Amt")), 4, MidpointRounding.ToEven)

                        gvLoadOut.CurrentRow.Cells("taxAmount").Value = clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax1Amt").Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax2Amt").Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax3Amt").Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax4Amt").Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax5Amt").Value) + clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("tax6Amt").Value)
                    Else
                        gvLoadOut.CurrentRow.Cells("tax1Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess1").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax1Amt").Value = 0

                        gvLoadOut.CurrentRow.Cells("tax2Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess2").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax2Amt").Value = 0

                        gvLoadOut.CurrentRow.Cells("tax3Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess3").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax3Amt").Value = 0

                        gvLoadOut.CurrentRow.Cells("tax4Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess4").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax4Amt").Value = 0

                        gvLoadOut.CurrentRow.Cells("tax5Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess5").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax5Amt").Value = 0

                        gvLoadOut.CurrentRow.Cells("tax6Rate").Value = 0
                        gvLoadOut.CurrentRow.Cells("assess6").Value = 0
                        gvLoadOut.CurrentRow.Cells("tax6Amt").Value = 0
                        gvLoadOut.CurrentRow.Cells("taxAmount").Value = 0.0

                    End If
                    ''Dim assessibleamt As Decimal = Math.Round(clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("mrp").Value.ToString()) * abatement(gvLoadOut.CurrentRow) / 100, 2)
                    ''SnDUtility.calculateTax(assessibleamt, Convert.ToString(tdr("Item_Basic_Price")), fndLocation.Value, gvLoadOut, gvTaxDetails)
                    ''RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
                    ''For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                    ''    gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = gr.Cells(2).Value ' tds.Tables(0).Rows(0)(0).ToString()
                    ''    gvLoadOut.CurrentRow.Cells("assess" + (gr.Index + 1).ToString()).Value = gr.Cells(4).Value 'tds.Tables(0).Rows(0)(1).ToString()
                    ''    gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = gr.Cells(5).Value 'tds.Tables(0).Rows(0)(2).ToString()
                    ''    sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                    ''    If connectSql.RunScalar(sql) = "N" Then
                    ''        gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0
                    ''        gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0
                    ''        gvLoadOut.CurrentRow.Cells("taxAmount").Value = clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("taxAmount").Value)
                    ''    Else
                    ''        gvLoadOut.CurrentRow.Cells("taxAmount").Value = clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells("taxAmount").Value) + clsCommon.myCdbl(gr.Cells(5).Value)
                    ''    End If
                    ''Next
                    ''AddHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
                Else
                    gvLoadOut.CurrentRow.Cells("taxAmount").Value = 0
                    gvLoadOut.CurrentRow.Cells("mrp").Value = gvLoadOut.CurrentRow.Cells("mrp").Value.ToString()
                    For Each gr As GridViewRowInfo In gvTaxDetails.Rows
                        gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0 ' tds.Tables(0).Rows(0)(0).ToString()
                        gvLoadOut.CurrentRow.Cells("assess" + (gr.Index + 1).ToString()).Value = 0 'tds.Tables(0).Rows(0)(1).ToString()
                        gvLoadOut.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0 'tds.Tables(0).Rows(0)(2).ToString()
                    Next
                End If
                gvLoadOut.CurrentRow.Cells("totalMRP").Value = Convert.ToString(tdr(5))
                gvLoadOut.CurrentRow.Cells("totalBasicAmount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("totalDiscountAmount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("totalNetAmount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("totalTaxAmount").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("tpt").Value = "0.00"
                gvLoadOut.CurrentRow.Cells("totalItemAmount").Value = "0"
                gvLoadOut.CurrentRow.Cells("totalTPT").Value = "0.00"
                Dim count As Decimal = 0
                For Each grow As GridViewRowInfo In gvLoadOut.Rows
                    If grow.Cells("shippedqty").Value > 0 Then
                        If Convert.ToString(grow.Cells("fromSchemeCode").Value).Contains("MS") Then
                            count = count + 1
                        End If
                    End If
                Next
                gvLoadOut.CurrentRow.Cells("fromSchemeCode").Value = "MS" + Convert.ToString(count + 1)
                gvLoadOut.CurrentRow.Cells("batchnumber").Value = connectSql.RunScalar("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + gvLoadOut.CurrentRow.Cells("itemCode").Value + "' AND Location_Code = '" + fndLocation.Value + "'")

            Next
            
        End If

    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        isInsideLoadData = True
        resetdata()
        GroupBox1.Enabled = False
        Dim obj As clsSaleReturnInterCompany = clsSaleReturnInterCompany.GetData(strCode, NavType, Nothing)
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnAdd.Enabled = False
        If obj IsNot Nothing Then
            isNewEntry = False
            txtDocumentNo.Value = obj.Document_No
            dtpShipDate.Value = obj.Document_Date
            fndemployeecode.txtValue.Text = obj.Employee_Code
            txtshellqty.Text = obj.Shell_Qty
            txtcustomerinvoiceno.Text = obj.Customer_Invoice_No

            If obj.Document_Type = 0 Then
                rbtnExcise.IsChecked = True
            Else
                rbtnNonExcise.IsChecked = True
            End If


            UsLock1.Status = IIf(obj.Is_Post = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)
            fndCustomer.Value = obj.Cust_Code
            txtCustomerName.Text = obj.Cust_Name
            chkOnHold.Checked = IIf(obj.On_Hold, True, False)

            txtRef.Text = obj.Ref_No
            txtDesc.Text = obj.Description
            txtRemarks.Text = obj.Remarks
            txtPriceCode.Text = obj.Price_Code
            fndTaxGroup.txtValue.Text = obj.Tax_Group
            fndLocation.Value = obj.Location
            strCustAccount = obj.Cust_Account
            'obj.Total_Assessable_Amount = assessible
            '
            'obj.Discount_Amt = shipmentDiscAmt
            'obj.Detail_Disc_Amt = detailDiscount
            MyTextBox3.Text = obj.Detail_Total_Amt
            txtShipmentTotal.Text = obj.Detail_Total_Amt
            txtTotalTaxAmount.Text = obj.Tax_Amt
            txttotaltpt.Text = obj.Total_TPT
            MyTextBox4.Text = obj.Empty_Value
            txtNetShipmentAmt.Text = obj.Empty_Value + obj.Total_Order_Amt
            'obj.Tax_Amt = shipmentTaxAmt
            'obj.Freight_Amt = freightCharges
            'obj.Other_Charges = OtherCharges
            'obj.Add_Charges = additionalCharges
            txtShipmentAmt.Text = obj.Total_Order_Amt
            fndSalesman.txtValue.Text = obj.Salesman_Code
            ddlModeofTransport.Text = obj.Mode_Of_Transport
            fndVehicleCode.txtValue.Text = obj.Vehicle_Code
            txtVhicleNo.Text = obj.Vehicle_No
            txtKMReading.Text = obj.KM_Reading
            fndRouteNo.txtValue.Text = obj.Route_No
            txtRouteDesc.Text = obj.Route_Desc
            txtTripNo.Text = obj.Trip_No
            'priceDate = obj.Price_Date
            fndPaymentTerms.txtValue.Text = obj.Terms_Code
            txtRemarks.Text = obj.Comments
            l1User = obj.Level1_User_code
            l2User = obj.Level2_User_code
            l3User = obj.Level3_User_code
            l4User = obj.Level4_User_code
            l5User = obj.Level5_User_code
            obj.Total_TPT = txttotaltpt.Text
            'obj.Total_Disc_Percent = dblTotDisPer
            'obj.Tax_Recoverable_Amt = dblTaxRecAmt
            txtCustDisc.Text = obj.Tot_Customer_Dis_Amt
            'obj.Tax_Recoverable_Amt2 = dblTaxRecAmt2
            'obj.Tax_Recoverable_Amt3 = dblTaxRecAmt3
            'obj.Empty_Value = emptyvalue













            ''For i1 = 1 To 10
            ''            sql = "Select (case When Tax" + i1.ToString() + " is NULL THEN '' else Tax" + i1.ToString() + " end),Tax" + i1.ToString() + "_Rate,Tax" + i1.ToString() + "_Assessable_Amt,Tax" + i1.ToString() + "_Amt from TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + fndLoadOut.Value + "'"
            ''            ds = connectSql.RunSQLReturnDS(sql)
            ''            If Not ds.Tables(0).Rows(0)(0).ToString() = String.Empty Then
            ''                Dim taxCode As String = ds.Tables(0).Rows(0)(0).ToString()
            ''                Dim taxRate As Decimal = ds.Tables(0).Rows(0)(1).ToString()
            ''                Dim assAmt As Decimal = ds.Tables(0).Rows(0)(2).ToString()
            ''                Dim taxAmt As Decimal = ds.Tables(0).Rows(0)(3).ToString()
            ''                If Not ds.Tables(0).Rows(0)(0).ToString() = "" Then
            ''                    sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Tables(0).Rows(0)(0).ToString() + "'"
            ''                    Dim taxCodeDesc As String = connectSql.RunScalar(sql)
            ''                    Dim grow As GridViewRowInfo = gvTaxDetails.Rows.AddNew()
            ''                    grow.Cells("taxAuthority").Value = taxCode
            ''                    grow.Cells("description").Value = taxCodeDesc
            ''                    grow.Cells("taxRate").Value = taxRate
            ''                    grow.Cells("basicAmount").Value = basicamt1
            ''                    grow.Cells("assessibleAmount").Value = assAmt
            ''                    grow.Cells("taxAmount").Value = taxAmt
            ''                End If
            ''            End If
            ''        Next
            ''        Dim SQLTAX As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code as [taxcode]  FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code  ORDER BY G.Trans_Code"
            ''        ds = connectSql.RunSQLReturnDS(SQLTAX)
            ''        RemoveHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged
            ''        For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
            ''            gvTaxDetails.Rows(k).Cells("taxable").Value = ds.Tables(0).Rows(k)("Taxable")
            ''            gvTaxDetails.Rows(k).Cells("surtax").Value = ds.Tables(0).Rows(k)("surtax")
            ''            RemoveHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged
            ''            gvTaxDetails.Rows(k).Cells(8).Value = ds.Tables(0).Rows(k)("taxcode")
            ''        Next








            obj.Ship_To = txtShipTo.Value
            obj.Ship_To_Desc = lblShipTo.Text

            Dim objTaxGrpMaster As New clsTaxGroupMaster()
            objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
            If (objTaxGrpMaster IsNot Nothing) Then
                txttaxdesc.Text = objTaxGrpMaster.Tax_Group_Desc
            End If
            gvTaxDetails.DataSource = Nothing

            gvTaxDetails.Rows.Clear()
            If (clsCommon.myLen(obj.TAX1) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX1_Assessable_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code

                            Exit For
                        End If
                    Next
                End If
            End If

            If (clsCommon.myLen(obj.TAX2) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX2_Assessable_Amt

                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX3) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX3_Assessable_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX4) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX4_Assessable_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX5) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX5_Assessable_Amt

                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX6) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX6_Assessable_Amt

                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX7) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt

                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX7_Assessable_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX8) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX8_Assessable_Amt

                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX9) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX9_Assessable_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX10) > 0) Then
                gvTaxDetails.Rows.AddNew()
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Detail_Total_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxAssessibleAmount).Value = obj.TAX10_Assessable_Amt

                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxDescription).Value = objTaxGrpTr.Tax_Code_Desc
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxTaxable).Value = IIf(objTaxGrpTr.Taxable, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtax).Value = IIf(objTaxGrpTr.Surtax, "Y", "N")
                            gvTaxDetails.Rows(gvTaxDetails.Rows.Count - 1).Cells(ColTaxSurtaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If



            Dim shipmentId As Integer = 1
            Dim dblTotQtyFC As Double = 0
            Dim dblTotQtyFB As Double = 0
            For Each objtr As clsSaleReturnInterCompanyDetail In obj.Arr

                If clsCommon.CompairString(objtr.Unit_code, "FC") = CompairStringResult.Equal Then
                    dblTotQtyFC += objtr.Qty
                ElseIf clsCommon.CompairString(objtr.Unit_code, "FB") = CompairStringResult.Equal Then
                    dblTotQtyFB += objtr.Qty
                End If

                gvLoadOut.Rows.AddNew()
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colMainItem).Value = objtr.Main_Item
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colBatchNumber).Value = objtr.Batch_No
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(ColICode).Value = objtr.Item_Code
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("itemName").Value = objtr.Item_Desc
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colPriceDate).Value = objtr.Price_Date
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("shippedQty").Value = objtr.Qty

                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colActualRetrunQty).Value = objtr.Actual_Return_Qty
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colLeakQty).Value = objtr.Leak_Qty
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colBurstQty).Value = objtr.Burst_Qty
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colShortQty).Value = objtr.Short_Qty


                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("unitCode").Value = objtr.Unit_code
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("priceCode").Value = objtr.Price_code
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colSchemeApplicable).Value = IIf(clsCommon.CompairString(objtr.Scheme_Applicable, "N") = CompairStringResult.Equal, "No", "Yes")
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("schemeCodeItem").Value = objtr.Scheme_Code_Qty
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colSchemeItem).Value = IIf(clsCommon.CompairString(objtr.Scheme_Item, "N") = CompairStringResult.Equal, "No", "Yes")
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colSchemeDiscountApplicable).Value = IIf(clsCommon.CompairString(objtr.Scheme_Disc_Applicable, "N") = CompairStringResult.Equal, "No", "Yes")
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("schemeCodeDiscount").Value = objtr.Scheme_Code_Cash
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colSampleItem).Value = IIf(clsCommon.CompairString(objtr.Sampling_Item, "N") = CompairStringResult.Equal, "No", "Yes")
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("emptyValue").Value = objtr.Empty_Value
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("emptyValueShell").Value = objtr.Empty_Value_Shell
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("emptyValueBottle").Value = objtr.Empty_Value_Bottle
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("mrp").Value = objtr.MRP_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("basicAmount").Value = objtr.Basic_Rate
                'objtr.Item_Assessable_Rate = itemAssessibleAmt(grow)
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("discountAmount").Value = objtr.Disc_Amt
                'objtr.Item_Net_Amt = dblItemNetAmt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("taxAmount").Value = objtr.Item_Tax
                ' objtr.Total_Assessable_Amt = totalItemAssessibleAmt(grow)
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalMRP").Value = objtr.Total_MRP_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalBasicAmount").Value = objtr.Total_Basic_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalDiscountAmount").Value = objtr.Total_Disc_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalNetAmount").Value = objtr.Total_net_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totaltaxAmount").Value = objtr.Total_Tax_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("tpt").Value = objtr.TPT
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalItemAmount").Value = objtr.Total_Item_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalTPT").Value = objtr.Total_TPT
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colPromoSchemeApplicable).Value = IIf(clsCommon.CompairString(objtr.Promo_Scheme_Applicable, "N") = CompairStringResult.Equal, "No", "Yes")
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("promoSchemeCode").Value = objtr.Promo_Scheme_Code
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colPromoSchemeItem).Value = IIf(clsCommon.CompairString(objtr.Promo_Scheme_Item, "N") = CompairStringResult.Equal, "No", "Yes")
                ' gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colAbatement).Value = objtr.Abatement
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("custDiscount").Value = objtr.Cust_Discount
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("totalCustDiscount").Value = objtr.Total_Cust_Discount
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("transferBasicAmount").Value = objtr.Transfer_Basic_Amount


                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colAbatementRate).Value = objtr.Abatement_rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("transferBasicAmount").Value = objtr.Transfer_Basic_Amount
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("fromSchemeCode").Value = objtr.From_Scheme_Code
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(ColPriceToShow).Value = objtr.Price_To_Show
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells(colPriceDateActual).Value = objtr.Price_Date_Actual


                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax1Rate").Value = objtr.TAX1_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax1Amt").Value = objtr.TAX1_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess1").Value = objtr.TAX1_Assessable_Amt


                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax2Rate").Value = objtr.TAX2_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax2Amt").Value = objtr.TAX2_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess2").Value = objtr.TAX2_Assessable_Amt


                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax3Rate").Value = objtr.TAX3_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax3Amt").Value = objtr.TAX3_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess3").Value = objtr.TAX3_Assessable_Amt

                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax4Rate").Value = objtr.TAX4_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax4Amt").Value = objtr.TAX4_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess4").Value = objtr.TAX4_Assessable_Amt

                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax5Rate").Value = objtr.TAX5_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax5Amt").Value = objtr.TAX5_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess5").Value = objtr.TAX5_Assessable_Amt

                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax6Rate").Value = objtr.TAX6_Rate
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("Tax6Amt").Value = objtr.TAX6_Amt
                gvLoadOut.Rows(gvLoadOut.Rows.Count - 1).Cells("assess6").Value = objtr.TAX6_Assessable_Amt
            Next
            lblfc.Text = clsCommon.myFormat(dblTotQtyFC)
            lblfb.Text = clsCommon.myFormat(dblTotQtyFB)
            If obj.Is_Post = 0 Then
                priceDateSelection(True, "FC")
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btnAdd.Enabled = True
            End If
            funSetFirstRow()
        End If
        isInsideLoadData = False
    End Sub

    Private Sub LoadDataOld(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        ''isInsideLoadData = True
        ''gvLoadOut.DataSource = Nothing
        ''gvTaxDetails.DataSource = Nothing
        ''gvLoadOut.Rows.Clear()
        ''gvTaxDetails.Rows.Clear()
        ''Dim checkpost As String = ""
        ''Dim shipmentamt As Decimal = 0
        ''Dim taxamt1 As Decimal = 0
        ''Dim pricecode As String = ""
        ''Dim TAXGROUP As String = ""
        ''Dim basicamt1 As Decimal = 0
        ''Try
        ''    Dim strLocation As String = ""
        ''    If clsCommon.myLen(objCommonVar.strCurrUserLocations) Then
        ''        strLocation += " and TSPL_SHIPMENT_MASTER.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        ''    End If
        ''    sql = "SELECT Order_No, Order_Date, Shipment_Date, Cust_Code, Cust_Name, Cust_PONo, Expected_Ship_Date, Status, On_Hold, Multiple_Orders, " & _
        ''   "Ref_No, Description, Remarks, Price_Code, Tax_Group, Location, Shipment_Discount_Amt, " & _
        ''   "Shipment_Tax_Amt, Freight_Amt, Other_Charges, Add_Charges," & _
        ''   "Total_Order_Amt, Salesman_Code, Mode_Of_Transport, Vehicle_Code, Vehicle_No, KM_Reading, Route_No, Route_Desc, Trip_No, " & _
        ''   "Scheme_Sample_Code, Price_Date, Terms_Code, Comments,Shipment_Type,Is_Post,Transfer_No,Transfer_Date,Shipment_Detail_Total_Amt, EMPTY_VALUE, shell_qty,Customer_Invoice_No, Date_Time_Removal, isnull(Employee_Code, '') as [Employee_Code], Shipment_Disc_Percent , Shipment_Discount_Amt ,Shipment_No,is_Sample,Tot_Customer_Dis_Amt,Ship_To,Ship_To_Desc,Against_C_Form,Transaction_Type FROM TSPL_SHIPMENT_MASTER WHERE 2=2"
        ''    Select Case NavType
        ''        Case NavigatorType.First
        ''            sql += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select MIN(Shipment_No) from TSPL_SHIPMENT_MASTER where 2=2 " + strLocation + ")"
        ''        Case NavigatorType.Last
        ''            sql += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Max(Shipment_No) from TSPL_SHIPMENT_MASTER where 2=2 " + strLocation + ")"
        ''        Case NavigatorType.Current
        ''            sql += " and TSPL_SHIPMENT_MASTER.Shipment_No = '" + strCode + "'"
        ''        Case NavigatorType.Next
        ''            sql += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Min(Shipment_No) from TSPL_SHIPMENT_MASTER where Shipment_No>'" + strCode + "' " + strLocation + ")"
        ''        Case NavigatorType.Previous
        ''            sql += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Max(Shipment_No) from TSPL_SHIPMENT_MASTER where Shipment_No<'" + strCode + "' " + strLocation + ")"
        ''    End Select



        ''    Dim dr6 As SqlDataReader = connectSql.RunSqlReturnDR(sql)
        ''    If dr6.HasRows Then
        ''        dr6.Read()
        ''        fndLoadOut.Value = clsCommon.myCstr(dr6("Shipment_No"))
        ''        checkpost = Convert.ToString(dr6("Is_Post"))

        ''        gvTaxDetails.DataSource = Nothing
        ''        gvTaxDetails.Rows.Clear()
        ''        gvTaxDetails.AllowAddNewRow = True
        ''        txtPriceCode.Text = dr6(13).ToString()

        ''        RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
        ''        RemoveHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged

        ''        RemoveHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
        ''        RemoveHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
        ''        RemoveHandler txtFreight.TextChanged, AddressOf totalAmounts
        ''        RemoveHandler txtCustDisc.TextChanged, AddressOf totalAmounts
        ''        RemoveHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

        ''        'RemoveHandler ddlLoadOutType.SelectedIndexChanged, AddressOf ddlLoadOutType_SelectedIndexChanged
        ''        ddlLoadOutType.Text = dr6(34).ToString()
        ''        taxamt1 = dr6("Shipment_Tax_Amt")
        ''        shipmentamt = dr6("Total_Order_Amt")

        ''        If ddlLoadOutType.Text = "Sale" Then

        ''            dtpShipDate.Enabled = True
        ''            btnPost.Visible = True AndAlso MyBase.isPostFlag
        ''            btnSettlement.Visible = False

        ''            lblTransaction.Visible = False
        ''            CmbTransaction.Visible = False

        ''        End If
        ''        txtshellqty.Text = Convert.ToString(dr6("shell_qty"))
        ''        dtpShipDate.Value = CDate(dr6(2).ToString())
        ''        CmbTransaction.SelectedValue = clsCommon.myCstr(dr6("Transaction_Type"))
        ''        chkAgainstCForm.CheckedListBox = IIf(clsCommon.myCdbl(dr6("Against_C_Form")) = 1, True, False)
        ''        Try
        ''            Dim dt As DateTime = DateTime.Parse(dr6("date_time_removal").ToString())
        ''            Dim time As String = dt.ToString("hh:mm:ss tt")
        ''            dtpremoval.Value = DateTime.Parse(time)
        ''        Catch ex As Exception
        ''            Dim time As DateTime = clsCommon.GETSERVERDATE()
        ''            time = time.ToString("hh:mm tt")
        ''            dtpremoval.Value = DateTime.Parse(time)
        ''        End Try
        ''        txtCustDisc.Text = clsCommon.myCstr(clsCommon.myCdbl(dr6("Tot_Customer_Dis_Amt")))
        ''        txtDiscPer.Text = Convert.ToString(dr6("Shipment_Disc_Percent"))
        ''        txtDiscAmt.Text = Convert.ToString(dr6("Shipment_Discount_Amt"))
        ''        MyTextBox3.Text = Convert.ToString(dr6("Shipment_Detail_Total_Amt"))
        ''        MyTextBox4.Text = Convert.ToString(dr6("EMPTY_VALUE"))

        ''        fndCustomer.Value = dr6(3).ToString()
        ''        txtcustomerinvoiceno.Text = dr6("Customer_Invoice_No")
        ''        sql = "SELECT Cust_Account FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + fndCustomer.Value + "'"
        ''        strCustAccount = connectSql.RunScalar(sql)
        ''        txtCustomerName.Text = dr6(4).ToString()


        ''        UsLock1.Status = IIf(clsCommon.CompairString("In Progress", clsCommon.myCstr(dr6(7))) = CompairStringResult.Equal, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        ''        ''txtStatus.Text = dr6(7).ToString()
        ''        If dr6(8).ToString() = "Y" Then
        ''            chkOnHold.Checked = True
        ''        Else
        ''            chkOnHold.Checked = False
        ''        End If
        ''        If dr6(9).ToString() = "Y" Then
        ''            chkMultipleOrder.Checked = True
        ''        Else
        ''            chkMultipleOrder.Checked = False
        ''        End If

        ''        If clsCommon.myCdbl(dr6("is_Sample")) > 0 Then
        ''            chkSample.Checked = True
        ''            txtDiscPer.ReadOnly = True
        ''        Else
        ''            chkSample.Checked = False
        ''            txtDiscPer.ReadOnly = False
        ''        End If

        ''        txtRef.Text = dr6(10).ToString()
        ''        'txtshellqty.Text = dr6("EMPTY_VALUE")
        ''        txtDesc.Text = dr6(11).ToString()
        ''        txtRemarks.Text = dr6(12).ToString()

        ''        txtShipTo.Value = clsCommon.myCstr(dr6("Ship_To"))
        ''        lblShipTo.Text = clsCommon.myCstr(dr6("Ship_To_Desc"))

        ''        'fndTaxGroup.txtValue.Text = dr6(14).ToString()
        ''        TAXGROUP = Convert.ToString(dr6(14))
        ''        fndLocation.Value = dr6(15).ToString()
        ''        basicamt1 = dr6("Shipment_Detail_Total_Amt").ToString()
        ''        '' txtCustDisc.Text = dr6(16).ToString()
        ''        txtTotalTaxAmount.Text = dr6(17).ToString()
        ''        txtFreight.Text = dr6(18).ToString()
        ''        txtOtherCharges.Text = dr6(19).ToString()
        ''        txtAdditionalCharges.Text = dr6(20).ToString()
        ''        txtShipmentAmt.Text = dr6(21).ToString()
        ''        If ddlLoadOutType.Text <> "Sale" Then
        ''            txtRcptAmt.Text = txtShipmentAmt.Text
        ''        End If
        ''        If dr6(34).ToString() = "Sale" Then
        ''            dtpOrderDate.Value = CDate(dr6(1).ToString())
        ''            fndOrderNo.txtValue.Text = dr6(0).ToString()
        ''        Else
        ''            'dtpTransferDate.Visible = True
        ''            'fndTransferNo.Visible = True
        ''            'lblTransferDate.Visible = True
        ''            'lblTransferNo.Visible = True
        ''            dtpTransferDate.Value = clsCommon.myCDate(dr6(37))
        ''            ''RemoveHandler fndTransferNo.txtValue.TextChanged, AddressOf fndTransferNo_TextChanged

        ''            fndTransferNo.Value = dr6(36).ToString()
        ''            ''AddHandler fndTransferNo.txtValue.TextChanged, AddressOf fndTransferNo_TextChanged

        ''            ' fndTransferNo_TextChanged(Me, New EventArgs())
        ''        End If
        ''        fndemployeecode.txtValue.Text = Convert.ToString(dr6("Employee_Code"))

        ''        sql = "SELECT  Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_SALESMAN_MAPPING WHERE Salesman_Code='" + Convert.ToString(dr6("Route_No")) + "'"
        ''        dr = connectSql.RunSqlReturnDR(sql)
        ''        If dr.HasRows Then
        ''            dr.Read()
        ''            l1User = dr(0).ToString()
        ''            l2User = dr(1).ToString()
        ''            l3User = dr(2).ToString()
        ''            l4User = dr(3).ToString()
        ''            l5User = dr(4).ToString()
        ''            dr.Close()
        ''        Else
        ''            common.clsCommon.MyMessageBoxShow("Salesman does not exist.")
        ''            fndSalesman.txtValue.Focus()
        ''            Exit Function
        ''        End If
        ''        txttotaltpt.Text = connectSql.RunScalar("SELECT ISNULL(SUM(ISNULL(Total_TPT,0)),0)  FROM TSPL_SHIPMENT_DETAILS  WHERE Shipment_No = '" + fndLoadOut.Value + "'")
        ''        ddlModeofTransport.Text = dr6(23).ToString()
        ''        fndVehicleCode.txtValue.Text = dr6(24).ToString()
        ''        txtVhicleNo.Text = dr6(25).ToString()
        ''        txtKMReading.Text = dr6(26).ToString()
        ''        fndRouteNo.txtValue.Text = dr6(27).ToString()
        ''        txtRouteDesc.Text = dr6(28).ToString()
        ''        fndSalesman.txtValue.Text = dr6(22).ToString()
        ''        txtTripNo.Text = dr6(29).ToString()
        ''        fndSchemeSample.txtValue.Text = dr6(30).ToString()
        ''        ddlPriceDate.Text = Format(CDate(dr6(31).ToString()), "dd/MM/yyyy")
        ''        fndPaymentTerms.txtValue.Text = dr6(32).ToString()
        ''        btnAdd.Text = "Update"
        ''        ddlLoadOutType.Enabled = False
        ''        fndCustomer.Enabled = False
        ''        fndLocation.Enabled = False
        ''        If dr6(35).ToString() = "Y" Then
        ''            btnAdd.Enabled = False
        ''            btnSaveAndPrint.Enabled = False
        ''            btnDelete.Enabled = False
        ''            btnPost.Enabled = False
        ''            btnSettlement.Enabled = False
        ''        Else
        ''            btnAdd.Enabled = True
        ''            btnSaveAndPrint.Enabled = True
        ''            btnDelete.Enabled = True
        ''            btnPost.Enabled = True
        ''            btnSettlement.Enabled = True
        ''        End If
        ''        Dim MRP As Decimal = totalMRP()
        ''        Dim basicAmt As Decimal = totalBasicAmt()
        ''        Dim netAmt As Decimal = totalNetAmount()
        ''        dr6.Close()

        ''        If fndOrderNo.txtValue.Text <> "" Then
        ''            funfilldetail()
        ''        ElseIf ddlLoadOutType.Text = "Transfer" Then
        ''            funfilldetail()
        ''            If UsLock1.Status = ERPTransactionStatus.Pending Then
        ''                Dim dtNoOfUnits As DataTable = clsDBFuncationality.GetDataTable("select distinct Unit_code from TSPL_SHIPMENT_DETAILS where Shipment_No='" + fndLoadOut.Value + "'")
        ''                Dim strUOMType As String = "FC"
        ''                If dtNoOfUnits.Rows.Count > 1 Then
        ''                    strUOMType = "ALL"
        ''                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtNoOfUnits.Rows(0)("Unit_code")), "FC") = CompairStringResult.Equal Then
        ''                    strUOMType = "FC"
        ''                Else
        ''                    strUOMType = "FB"
        ''                End If
        ''                funfilltransfer(True, strUOMType)
        ''            End If
        ''            LoadPendingBalanceAgainstTransfer()
        ''        ElseIf checkpost.Trim() = "Y" Then
        ''            funfilldetail()
        ''        Else
        ''            funfilldetail1()
        ''            priceDateSelection1(TAXGROUP)
        ''        End If

        ''        If clsCommon.myCdbl(txtDiscPer.Text) < 100 Then
        ''            txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text) + clsCommon.myCdbl(MyTextBox3.Text)
        ''        ElseIf clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''            txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text)
        ''            MyTextBox3.Text = 0

        ''        End If
        ''        funtotalfcfb()
        ''        txtShipmentAmt.Text = shipmentamt
        ''        txtNetShipmentAmt.Text = clsCommon.myCdbl(MyTextBox4.Text) + clsCommon.myCdbl(txtShipmentAmt.Text)
        ''        txtTotalTaxAmount.Text = taxamt1
        ''        AddHandler fndOrderNo.txtValue.TextChanged, AddressOf fndOrderNo_TextChanged
        ''        RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
        ''        fndTaxGroup.txtValue.Text = ""
        ''        fndTaxGroup.txtValue.Text = TAXGROUP
        ''        txttaxdesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
        ''        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
        ''   " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
        ''   " WHERE G.Tax_Group_Code = '" + TAXGROUP + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
        ''   " ORDER BY G.Trans_Code"
        ''        ds = RunSQLReturnDS(sql)
        ''        If ds.Tables(0).Rows.Count > 0 Then
        ''            gvTaxDetails.DataSource = ds.Tables(0)
        ''        End If
        ''        'CalculateTaxratvalue()
        ''        AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
        ''        AddHandler fndSchemeSample.txtValue.TextChanged, AddressOf fndSchemeSample_TextChanged
        ''        AddHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
        ''        AddHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
        ''        AddHandler txtFreight.TextChanged, AddressOf totalAmounts
        ''        AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
        ''        AddHandler ddlLoadOutType.SelectedIndexChanged, AddressOf ddlLoadOutType_SelectedIndexChanged
        ''        AddHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged
        ''        AddHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

        ''        gvTaxDetails.AllowAddNewRow = False
        ''        Dim i1 As Integer
        ''        gvTaxDetails.DataSource = Nothing
        ''        gvTaxDetails.Rows.Clear()
        ''        For i1 = 1 To 10
        ''            sql = "Select (case When Tax" + i1.ToString() + " is NULL THEN '' else Tax" + i1.ToString() + " end),Tax" + i1.ToString() + "_Rate,Tax" + i1.ToString() + "_Assessable_Amt,Tax" + i1.ToString() + "_Amt from TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + fndLoadOut.Value + "'"
        ''            ds = connectSql.RunSQLReturnDS(sql)
        ''            If Not ds.Tables(0).Rows(0)(0).ToString() = String.Empty Then
        ''                Dim taxCode As String = ds.Tables(0).Rows(0)(0).ToString()
        ''                Dim taxRate As Decimal = ds.Tables(0).Rows(0)(1).ToString()
        ''                Dim assAmt As Decimal = ds.Tables(0).Rows(0)(2).ToString()
        ''                Dim taxAmt As Decimal = ds.Tables(0).Rows(0)(3).ToString()
        ''                If Not ds.Tables(0).Rows(0)(0).ToString() = "" Then
        ''                    sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Tables(0).Rows(0)(0).ToString() + "'"
        ''                    Dim taxCodeDesc As String = connectSql.RunScalar(sql)
        ''                    Dim grow As GridViewRowInfo = gvTaxDetails.Rows.AddNew()
        ''                    grow.Cells("taxAuthority").Value = taxCode
        ''                    grow.Cells("description").Value = taxCodeDesc
        ''                    grow.Cells("taxRate").Value = taxRate
        ''                    grow.Cells("basicAmount").Value = basicamt1
        ''                    grow.Cells("assessibleAmount").Value = assAmt
        ''                    grow.Cells("taxAmount").Value = taxAmt
        ''                End If
        ''            End If
        ''        Next
        ''        Dim SQLTAX As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code as [taxcode]  FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code  ORDER BY G.Trans_Code"
        ''        ds = connectSql.RunSQLReturnDS(SQLTAX)
        ''        RemoveHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged
        ''        For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
        ''            gvTaxDetails.Rows(k).Cells("taxable").Value = ds.Tables(0).Rows(k)("Taxable")
        ''            gvTaxDetails.Rows(k).Cells("surtax").Value = ds.Tables(0).Rows(k)("surtax")
        ''            RemoveHandler gvTaxDetails.CellValueChanged, AddressOf gvTaxDetails_CellValueChanged
        ''            gvTaxDetails.Rows(k).Cells(8).Value = ds.Tables(0).Rows(k)("taxcode")
        ''        Next

        ''    Else
        ''        btnAdd.Text = "Save"
        ''        btnDelete.Enabled = False
        ''        btnPost.Enabled = False
        ''        btnSettlement.Enabled = False
        ''        resetFNDCustomer()
        ''        resetFNDTaxGroup()
        ''        resetForm()
        ''    End If
        ''    lblSaleInvoiceNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sale_Invoice_No from TSPL_SALE_INVOICE_HEAD where Shipment_No='" + fndLoadOut.Value + "'"))
        ''Catch ex As Exception
        ''    myMessages.myExceptions(ex)
        ''Finally
        ''    isInsideLoadData = False
        ''End Try
        ''funSetFirstRow()
    End Sub

    Public Shared Function jrnlEntryNo(ByVal trans As SqlTransaction) As String
        Dim Maxvlu As String
        Dim NxtMaxNo As Int32
        Dim sql As String = "SELECT MAX(Voucher_No) as MaxValue from TSPL_JOURNAL_MASTER  where Voucher_No like '%JRNL%' "
        Dim ds As DataSet = connectSql.RunSQLReturnDS(trans, sql)
        'ds = clsDBFuncationality.getSingleValue(sql, trans)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString <> "" Then
                Maxvlu = ds.Tables(0).Rows(0)(0).ToString()
                Maxvlu = Maxvlu.Remove(0, 4)
                NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
                NxtMaxNo = NxtMaxNo + 1
                Dim strCount As String
                strCount = NxtMaxNo.ToString()
                If strCount.Length = 1 Then
                    Maxvlu = "JRNL" & "000" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 2 Then
                    Maxvlu = "JRNL" & "00" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 3 Then
                    Maxvlu = "JRNL" & "0" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 4 Then
                    Maxvlu = "JRNL" & NxtMaxNo.ToString()
                End If
                Return Maxvlu
            Else
                Maxvlu = "JRNL0001"
                Return Maxvlu
            End If
        Else
            Maxvlu = "JRNL0001"
            Return Maxvlu
        End If
        Return Maxvlu
    End Function

    Private Function postInvoice(ByVal trans As SqlTransaction, ByVal invNo As String, ByVal SaleInvoiceNo As String) As Boolean
        ''Dim totaltaxrecover As Decimal = 0
        ''Dim Excisedutyacct, ecessacct, hcessacct, Excisedutydesc, ecessdesc, hcessdesc As String
        ''Dim Exciseamt As Decimal = 0
        ''Dim exciserate As Decimal = 0
        ''Dim ecessrate As Decimal = 0
        ''Dim totalexciseamt As Decimal = 0
        ''Dim totalecessamt As Decimal = 0
        ''Dim nonexcise As String = ""
        ''Dim totalhcessamt As Decimal = 0
        ''Dim exicsable As String = String.Empty
        ''Dim locationsegcode As String = String.Empty
        ''Dim hcessrate As Decimal = 0
        ''Dim ecessamt As Decimal = 0
        ''Dim hcessamt As Decimal = 0
        ''sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "'"
        ''Dim locSegCode As String = connectSql.RunScalar(trans, sql)
        ''sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET Is_Post='Y' WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''connectSql.RunSqlTransaction(trans, sql)
        ''Dim totaltax As Decimal = 0
        ''For Each g As GridViewRowInfo In gvTaxDetails.Rows
        ''    totaltax = totaltax + g.Cells("taxAmount").Value
        ''Next
        ''sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET Balance_Amt='" + (clsCommon.myCdbl(txtShipmentAmt.Text) + emptyvalue123).ToString() + "' WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''connectSql.RunSqlTransaction(trans, sql)
        ''Dim lineNo As Integer = 1
        ''Dim StrVoucher As String = jrnlEntryNo(trans)
        ''sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'SD-IN'"
        ''Dim strSourceDesc As String = connectSql.RunScalar(trans, sql)
        ''Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        ''Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1
        ''Dim dt As String = dtpShipDate.Value.ToString("dd/MM/yyyy")
        ''connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Source_Code", "SD-IN"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", invNo), New SqlParameter("@Source_Doc_Date", dt), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Voucher_Desc", "Sale against Invoice No " + invNo), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", "Shipment No " + fndLoadOut.Value + " for customer " + txtCustomerName.Text), New SqlParameter("@Comments", Me.txtRemarks.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", dt), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndCustomer.Value), New SqlParameter("@CustVend_Name", Me.txtCustomerName.Text), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", txtShipmentAmt.Text), New SqlParameter("@Total_Credit_Amt", txtShipmentAmt.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy")), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy")), New SqlParameter("@Comp_Code", companyCode))
        ''Dim schemeMrp As Decimal = 0
        ''Dim schemeAmt As Decimal = 0
        ''Dim promoMrp As Decimal = 0
        ''Dim promoAmt As Decimal = 0
        ''Dim schemeCogs As Decimal = 0
        ''Dim taxamountwithdisc As Decimal = connectSql.RunScalar(trans, "select isnull(shipment_tax_amt,0) from tspl_shipment_master where shipment_no = '" + fndLoadOut.Value + "'")
        ''Dim promoCogs As Decimal = 0
        ''Dim netAmt As Decimal = connectSql.RunScalar(trans, "select isnull(Shipment_Detail_Total_Amt,0)  from TSPL_SHIPMENT_MASTER   where Shipment_No = '" + fndLoadOut.Value + "'")

        ''Dim discAmt As Decimal = totalDiscount()
        ''Dim cogs As Decimal = 0
        ''Dim COGS1 As Decimal = 0
        ''Dim shipmentDiscAmt As Decimal
        ''Dim emptyvalue As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "SELECT ISNULL(Empty_Value,0)  FROM TSPL_SHIPMENT_MASTER WHERE Shipment_No = '" + fndLoadOut.Value + "'"))
        ''If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
        ''    discAmt = connectSql.RunScalar(trans, "select Shipment_Discount_Amt  from tspl_shipment_master where Shipment_No = '" + Convert.ToString(fndLoadOut.Value) + "'")
        ''ElseIf clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
        ''    discAmt = totalDiscount()
        ''    shipmentDiscAmt = txtCustDisc.Text
        ''    discAmt = discAmt + shipmentDiscAmt
        ''End If
        ''Dim schemeamount As Decimal = 0
        ''Dim convfact As Decimal = 0
        ''Dim intCounter As Integer = 0
        ''Dim CurrSchemeCogs As Decimal = 0
        ''Dim CurrCogs As Decimal = 0
        ''Dim CurrSchemeAmount As Decimal = 0
        ''Dim CurrPromoCogs As Decimal = 0
        ''For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''    If clsCommon.myCdbl(grow.Cells("shippedqty").Value) > 0 Then
        ''        intCounter = intCounter + 1
        ''        sql = "SELECT Unit_COGS,Scheme_Item,Promo_Scheme_Item,Sampling_Item from TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + fndLoadOut.Value + "' " & _
        ''        " AND Shipment_Id='" + (intCounter).ToString() + "'"
        ''        Dim postDr As SqlDataReader = connectSql.RunSqlReturnDR(trans, sql)
        ''        While postDr.Read()
        ''            If postDr(1).ToString() = "N" AndAlso postDr(2).ToString() = "N" AndAlso postDr(3).ToString() = "N" Then
        ''                convfact = connectSql.ReturnConvFact(grow.Cells("itemcode").Value, grow.Cells("unitcode").Value)
        ''                CurrCogs = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString()) / convfact, 2, MidpointRounding.ToEven)
        ''                cogs = cogs + CurrCogs

        ''            ElseIf postDr(1).ToString() = "Y" Or postDr(3).ToString() = "Y" Then
        ''                convfact = connectSql.ReturnConvFact(grow.Cells("itemcode").Value, grow.Cells("unitcode").Value)
        ''                CurrSchemeCogs = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString() / convfact), 2, MidpointRounding.ToEven)
        ''                schemeCogs = schemeCogs + CurrSchemeCogs

        ''                CurrSchemeAmount = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString() / convfact), 2, MidpointRounding.ToEven)
        ''                schemeamount = schemeamount + CurrSchemeAmount
        ''            ElseIf postDr(2).ToString() = "Y" Then
        ''                CurrPromoCogs = Math.Round(+clsCommon.myCdbl(grow.Cells("shippedQty").Value) * clsCommon.myCdbl(postDr(0).ToString()), 2, MidpointRounding.ToEven)
        ''                promoCogs = promoCogs + CurrPromoCogs

        ''            End If
        ''        End While
        ''        postDr.Close()
        ''        If clsCommon.myCdbl(grow.Cells("shippedQty").Value) > 0 Then
        ''            If grow.Cells("schemeItem").Value = "Yes" Or grow.Cells("sampleItem").Value = "Yes" Then
        ''                schemeMrp = schemeMrp + clsCommon.myCdbl(grow.Cells("totalMRP").Value)
        ''            End If
        ''            If grow.Cells("promoSchemeItem").Value = "Yes" Then
        ''                promoMrp = promoMrp + clsCommon.myCdbl(grow.Cells("totalMRP").Value)
        ''            End If
        ''        End If
        ''    End If
        ''Next
        ''Dim obj As Accountsegment
        ''sql = "SELECT sum(Unit_COGS*Shipped_Qty) from TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + fndLoadOut.Value + "' "
        ''Dim strRecAcc As String
        ''Dim strRecAccDesc As String
        ''sql = "SELECT A.Receivable_Control_acct FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN " & _
        ''      "  TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  C.Cust_Code = '" + fndCustomer.Value + "'"
        ''strRecAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecAcc + "'"
        ''strRecAccDesc = connectSql.RunScalar(trans, sql)
        ''If strRecAccDesc Is Nothing Then
        ''    Throw New Exception("Receivable Control Account not found.")
        ''End If
        ''If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''Else
        ''    If Not clsCommon.myCdbl(Me.txtShipmentAmt.Text) = 0 Then
        ''        Dim totalcustamt As Decimal = 0
        ''        If schemeCogs <> 0 Then
        ''            totalcustamt = clsCommon.myCdbl(Me.txtShipmentAmt.Text)
        ''        Else
        ''            totalcustamt = clsCommon.myCdbl(Me.txtShipmentAmt.Text)
        ''        End If
        ''        obj = Accountsegment.Getaccountcodedesc(strRecAcc, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecAcc), New SqlParameter("@Account_Desc", strRecAccDesc), New SqlParameter("@Amount", Math.Round(totalcustamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''End If
        ''Dim strContainerAcc As String = String.Empty
        ''Dim strContainerDesc As String = String.Empty
        ''sql = "SELECT A.container_deposit FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN   TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  C.Cust_Code = '" + fndCustomer.Value + "'"
        ''strContainerAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strContainerAcc + "'"
        ''strContainerDesc = connectSql.RunScalar(trans, sql)
        ''If strContainerDesc Is Nothing Then
        ''    Throw New Exception("Container Account not found.")
        ''End If
        ''If emptyvalue > 0 Then
        ''    obj = Accountsegment.Getaccountcodedesc(strContainerAcc, trans)
        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strContainerAcc), New SqlParameter("@Account_Desc", strContainerDesc), New SqlParameter("@Amount", Math.Round(emptyvalue, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If

        ''Dim strSalesAcc As String
        ''Dim strSalesAccDesc As String
        ''sql = "SELECT SA.Sales_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
        ''         "TSPL_SALES_ACCOUNTS AS SA ON IM.Sale_Class_Code = SA.Sales_Class_Code " & _
        ''         " WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''strSalesAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strSalesAcc + "'"
        ''strSalesAccDesc = connectSql.RunScalar(trans, sql)
        ''If strSalesAccDesc Is Nothing Then
        ''    Throw New Exception("Sales Account not found.")
        ''End If
        ''obj = Accountsegment.Getaccountcodedesc(strSalesAcc, trans)

        ''connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strSalesAcc), New SqlParameter("@Account_Desc", strSalesAccDesc), New SqlParameter("@Amount", (-1) * Math.Round((discAmt + netAmt), 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''lineNo = lineNo + 1
        ''Dim shipmentDiscPer As Decimal
        ''shipmentDiscAmt = txtCustDisc.Text
        ''Dim strRecDiscAcc As String
        ''Dim strRecDiscAccDesc As String
        ''sql = "SELECT  A.Receipts_Discount_acct FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN " & _
        ''      " TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  (C.Cust_Code = '" + fndCustomer.Value + "')"
        ''strRecDiscAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecDiscAcc + "'"
        ''strRecDiscAccDesc = connectSql.RunScalar(trans, sql)
        ''If strRecDiscAccDesc Is Nothing Then
        ''    Throw New Exception("Receipts discount Account not found.")
        ''End If

        ''If fndSchemeSample.txtValue.Text = String.Empty Then
        ''    If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''        If Not discAmt + taxamountwithdisc = 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strRecDiscAcc, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecDiscAcc), New SqlParameter("@Account_Desc", strRecDiscAccDesc), New SqlParameter("@Amount", Math.Round(discAmt + taxamountwithdisc, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    Else
        ''        If Not discAmt = 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strRecDiscAcc, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecDiscAcc), New SqlParameter("@Account_Desc", strRecDiscAccDesc), New SqlParameter("@Amount", Math.Round(discAmt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If
        ''End If

        ''exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''If exicsable = "N" And nonexcise = "F" Then
        ''    locationsegcode = connectSql.RunScalar(trans, "SELECT Loc_Segment_Code   FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    Excisedutyacct = "3004-" + Convert.ToString(locationsegcode)
        ''    Excisedutydesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + Excisedutyacct + "'")
        ''    If Excisedutydesc Is Nothing Then
        ''        Throw New Exception("Excise Duty Recovered Account not found.")
        ''    End If
        ''    ecessacct = "3011-" + Convert.ToString(locationsegcode)
        ''    ecessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + ecessacct + "'")
        ''    If ecessdesc Is Nothing Then
        ''        Throw New Exception("E.Cess Duty Recovered Account not found.")
        ''    End If
        ''    hcessacct = "3012-" + Convert.ToString(locationsegcode)
        ''    hcessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + hcessacct + "'")
        ''    If hcessdesc Is Nothing Then
        ''        Throw New Exception("H.Cess Duty Recovered Account not found.")
        ''    End If
        ''    For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''        If clsCommon.myCdbl(grow.Cells("shippedqty").Value) > 0 Then
        ''            If grow.Cells("schemeItem").Value = "No" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("sampleItem").Value = "No" Then
        ''                Exciseamt = 0
        ''                Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                Exciseamt = Exciseamt * exciserate / 100
        ''                totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''            End If
        ''            If grow.Cells("sampleItem").Value = "Yes" Then
        ''                Exciseamt = 0
        ''                If fndSchemeSample.txtValue.Text = String.Empty Then
        ''                    Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))

        ''                Else
        ''                    Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'Y' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))

        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                Exciseamt = Exciseamt * exciserate / 100
        ''                totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''            End If
        ''        End If
        ''    Next

        ''    totaltaxrecover = totalexciseamt + totalecessamt + totalhcessamt

        ''    If Not clsCommon.myCdbl(totalexciseamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(Excisedutyacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", Excisedutyacct), New SqlParameter("@Account_Desc", Excisedutydesc), New SqlParameter("@Amount", Math.Round(totalexciseamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not clsCommon.myCdbl(totalecessamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(ecessacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", ecessacct), New SqlParameter("@Account_Desc", ecessdesc), New SqlParameter("@Amount", Math.Round(totalecessamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not clsCommon.myCdbl(totalhcessamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(hcessacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", hcessacct), New SqlParameter("@Account_Desc", hcessdesc), New SqlParameter("@Amount", Math.Round(totalhcessamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''End If
        ''Dim taxAmt As Decimal
        ''Dim countRow As Integer = 0
        ''For countRow = 0 To gvTaxDetails.Rows.Count - 1
        ''    Dim gro As GridViewRowInfo = gvTaxDetails.Rows(countRow)
        ''    Dim strTaxCode As String = gro.Cells("taxAuthority").Value

        ''    taxAmt = clsCommon.myCdbl(gro.Cells("taxAmount").Value)
        ''    Dim strLiableAcc As String = ""
        ''    Dim strLiableAccDesc As String = ""
        ''    sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER  WHERE Tax_Code = '" + strTaxCode + "'"
        ''    strLiableAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strLiableAcc + "'"
        ''    strLiableAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strLiableAccDesc Is Nothing Then
        ''        Throw New Exception("Tax Liability Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''    End If
        ''    Dim strRecoverableAcc As String = ""
        ''    Dim strRecoverableAccDesc As String = ""
        ''    Dim strNetPayAcc As String = ""
        ''    Dim strNetPayAccDesc As String = ""
        ''    sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
        ''    If Not connectSql.RunScalar(trans, sql).ToString() = "" Then
        ''        strRecoverableAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    End If

        ''    If Not strRecoverableAcc = "" Then
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecoverableAcc + "'"
        ''        strRecoverableAccDesc = connectSql.RunScalar(trans, sql)
        ''    End If
        ''    sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
        ''    If Not connectSql.RunScalar(trans, sql).ToString() = "" Then
        ''        strNetPayAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    End If

        ''    If Not strNetPayAcc = "" Then
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
        ''        strNetPayAccDesc = connectSql.RunScalar(trans, sql)
        ''    End If
        ''    If Not taxAmt = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(strLiableAcc, trans)

        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strLiableAcc), New SqlParameter("@Account_Desc", strLiableAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not strRecoverableAcc = "" AndAlso Not strNetPayAcc = "" Then
        ''        If strRecoverableAccDesc Is Nothing Then
        ''            Throw New Exception("Tax Recoverable Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''        ElseIf strNetPayAccDesc Is Nothing Then
        ''            Throw New Exception("Tax Net Payable Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''        End If
        ''    End If
        ''    If Not strNetPayAcc = "" AndAlso Not strRecAcc = "" Then
        ''        If Not taxAmt = 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strRecoverableAcc, trans)

        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecoverableAcc), New SqlParameter("@Account_Desc", strRecoverableAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''        If Not schemeMrp = 0 Then
        ''            For Each grr As GridViewRowInfo In gvLoadOut.Rows
        ''                If grr.Cells("schemeItem").Value = "Yes" Or grr.Cells("sampleItem").Value = "Yes" Then
        ''                    taxAmt = taxAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("shippedQty").Value)
        ''                    schemeAmt = schemeAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("shippedQty").Value)
        ''                End If
        ''            Next
        ''        End If
        ''        If Not promoMrp = 0 Then
        ''            For Each grr As GridViewRowInfo In gvLoadOut.Rows
        ''                If grr.Cells("promoSchemeItem").Value = "Yes" Then
        ''                    promoAmt = promoAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("shippedQty").Value)
        ''                    taxAmt = taxAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("shippedQty").Value)
        ''                End If
        ''            Next
        ''        End If
        ''        If Not taxAmt = 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)

        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If
        ''Next
        ''Dim strMarginAcc As String
        ''Dim strMarginAccDesc As String
        ''Dim marginAmt As Decimal
        ''Dim gTotalMargin As Decimal = 0

        ''sql = "SELECT Price_Comp_Account_Code  FROM TSPL_PRICE_COMPONENT_MASTER WHERE TPT_Type = 'Y' AND Price_Comp_code IN (SELECT Price_Comp_Code  FROM TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "')"

        ''If Not String.IsNullOrEmpty(connectSql.RunScalar(trans, sql)) Then
        ''    strMarginAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''End If
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strMarginAcc + "'"
        ''strMarginAccDesc = connectSql.RunScalar(trans, sql)
        ''If strMarginAccDesc Is Nothing Or strMarginAccDesc = "" Then
        ''    Throw New Exception("Margin Account not found.")
        ''End If
        ''marginAmt = 0
        ''If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
        ''    marginAmt = connectSql.RunScalar(trans, "select isnull(Total_TPT,0)    from TSPL_SHIPMENT_MASTER where Shipment_No = '" + fndLoadOut.Value.ToString() + "'")
        ''Else
        ''    For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''        If grow.Cells("shippedqty").Value > 0 Then

        ''            If grow.Cells("schemeItem").Value = "No" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("SampleItem").Value = "No" Then
        ''                marginAmt = marginAmt + clsCommon.myCdbl(grow.Cells("totalTPT").Value)
        ''            End If
        ''        End If
        ''    Next
        ''End If

        ''If Not marginAmt = 0 Then
        ''    obj = Accountsegment.Getaccountcodedesc(strMarginAcc, trans)

        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strMarginAcc), New SqlParameter("@Account_Desc", strMarginAccDesc), New SqlParameter("@Amount", Math.Round(marginAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If
        ''Dim strCogAcc As String = ""
        ''Dim strCogAccDesc As String = ""
        ''Dim strShpClrAcc As String = ""
        ''Dim strShpClrAccDesc As String = ""
        ''sql = " SELECT SA.Cost_Of_Goods_Sold FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''     " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''     " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''strCogAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strCogAcc + "'"
        ''strCogAccDesc = connectSql.RunScalar(trans, sql)
        ''If strCogAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''    Throw New Exception("Cost of goods sold Account not found.")
        ''End If
        ''sql = "SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
        ''      " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
        ''      " TSPL_GL_ACCOUNTS AS GLA ON PA.Shipment_Clearing = GLA.Account_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''strShpClrAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strShpClrAcc + "'"
        ''strShpClrAccDesc = connectSql.RunScalar(trans, sql)
        ''If strShpClrAccDesc Is Nothing Or strShpClrAccDesc = "" Then
        ''    Throw New Exception("Shipment Clearing Account not found.")
        ''End If
        ''Dim dblAmt As Double = 0
        ''If Not cogs + promoCogs + schemeCogs = 0 Then
        ''    dblAmt = (cogs + promoCogs + schemeCogs) * (-1)
        ''    obj = Accountsegment.Getaccountcodedesc(strShpClrAcc, trans)

        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strShpClrAcc), New SqlParameter("@Account_Desc", strShpClrAccDesc), New SqlParameter("@Amount", Math.Round(dblAmt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If
        ''If cogs > 0 Then
        ''    dblAmt = cogs
        ''    obj = Accountsegment.Getaccountcodedesc(strCogAcc, trans)

        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strCogAcc), New SqlParameter("@Account_Desc", strCogAccDesc), New SqlParameter("@Amount", Math.Round(dblAmt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If
        ''exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")

        ''If exicsable = "N" And nonexcise = "F" Then
        ''    totalexciseamt = 0
        ''    Exciseamt = 0
        ''    ecessamt = 0
        ''    hcessamt = 0
        ''    totalecessamt = 0
        ''    totalhcessamt = 0
        ''    locationsegcode = connectSql.RunScalar(trans, "SELECT Loc_Segment_Code   FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    Excisedutyacct = "4817-" + Convert.ToString(locationsegcode)
        ''    Excisedutydesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + Excisedutyacct + "'")
        ''    If Excisedutydesc Is Nothing Then
        ''        Throw New Exception("Excise Duty Paid Account not found.")
        ''    End If
        ''    ecessacct = "4818-" + Convert.ToString(locationsegcode)
        ''    ecessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + ecessacct + "'")
        ''    If ecessdesc Is Nothing Then
        ''        Throw New Exception("E.Cess Duty Paid Account not found.")
        ''    End If
        ''    hcessacct = "4819-" + Convert.ToString(locationsegcode)
        ''    hcessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + hcessacct + "'")
        ''    If hcessdesc Is Nothing Then
        ''        Throw New Exception("H.Cess Duty Paid Account not found.")
        ''    End If
        ''    Dim countexcise As Integer = 0
        ''    For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''        If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''            countexcise = countexcise + 1
        ''            Exciseamt = 0
        ''            Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' AND Sale_Invoice_Id = '" + countexcise.ToString() + "'"))
        ''            If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''            Else
        ''                Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''            End If
        ''            If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''            Else
        ''                Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''            End If
        ''            If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''            Else
        ''                Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''            End If
        ''            totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''            Exciseamt = Exciseamt * exciserate / 100
        ''            totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''            totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''        End If
        ''        ''End If
        ''    Next
        ''    If Not clsCommon.myCdbl(totalexciseamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(Excisedutyacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", Excisedutyacct), New SqlParameter("@Account_Desc", Excisedutydesc), New SqlParameter("@Amount", Math.Round(totalexciseamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not clsCommon.myCdbl(totalecessamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(ecessacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", ecessacct), New SqlParameter("@Account_Desc", ecessdesc), New SqlParameter("@Amount", Math.Round(totalecessamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not clsCommon.myCdbl(totalhcessamt) = 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(hcessacct, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", hcessacct), New SqlParameter("@Account_Desc", hcessdesc), New SqlParameter("@Amount", Math.Round(totalhcessamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''End If
        ''nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''If exicsable = "N" And nonexcise = "F" Then
        ''    Dim totalsaleamt As Decimal = (discAmt + netAmt) - totaltaxrecover
        ''    cogs = cogs - totaltaxrecover - promoCogs
        ''    totalsaleamt = totalsaleamt * (-1)
        ''    sql = "update TSPL_JOURNAL_DETAILS set amount = '" + Convert.ToString(totalsaleamt) + "' where Voucher_No = '" + Convert.ToString(StrVoucher) + "' and Account_code = '" + Convert.ToString(strSalesAcc) + "'"
        ''    connectSql.RunSqlTransaction(trans, sql)
        ''    sql = "update TSPL_JOURNAL_DETAILS set amount = '" + Convert.ToString(cogs) + "' where Voucher_No = '" + Convert.ToString(StrVoucher) + "'  and Account_code = '" + Convert.ToString(strCogAcc) + "'"
        ''    connectSql.RunSqlTransaction(trans, sql)
        ''End If
        ''Dim strRetContAcc As String = ""
        ''Dim strRetContAccDesc As String = ""
        ''sql = " SELECT SA.Returnable_Container FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''     " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Returnable_Container INNER JOIN " & _
        ''     " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''strRetContAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRetContAcc + "'"
        ''strRetContAccDesc = connectSql.RunScalar(trans, sql)
        ''If strRetContAccDesc Is Nothing Or strRetContAccDesc = "" Then
        ''    Throw New Exception("Returnable Container Account not found.")
        ''End If

        ''If Not emptyvalue = 0 Then
        ''    obj = Accountsegment.Getaccountcodedesc(strRetContAcc, trans)

        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRetContAcc), New SqlParameter("@Account_Desc", strRetContAccDesc), New SqlParameter("@Amount", Math.Round(emptyvalue, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If
        ''If Not schemeCogs = 0 Then
        ''    Dim strSchemeAcc As String = ""
        ''    Dim strSchemeAccDesc As String = ""
        ''    exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    Dim countscheme As Integer = 0
        ''    If exicsable = "N" And nonexcise = "F" Then
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''                countscheme = countscheme + 1
        ''                If grow.Cells("schemeItem").Value = "Yes" Or grow.Cells("sampleItem").Value = "Yes" Then
        ''                    Exciseamt = 0
        ''                    totalexciseamt = 0
        ''                    Exciseamt = 0
        ''                    ecessamt = 0
        ''                    hcessamt = 0
        ''                    totalecessamt = 0
        ''                    totalhcessamt = 0
        ''                    Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' AND Sale_Invoice_Id = '" + countscheme.ToString() + "'"))
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                    Exciseamt = Exciseamt * exciserate / 100
        ''                    totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                    totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''                End If
        ''            End If
        ''        Next
        ''        Dim totalscheme As Decimal = totalexciseamt + totalecessamt + totalhcessamt
        ''        schemeCogs = schemeCogs - totalscheme
        ''    End If
        ''    sql = " SELECT SA.Schemes FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''     " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''     " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strSchemeAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strSchemeAcc + "'"
        ''    strSchemeAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strSchemeAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''        Throw New Exception("Schemes Account not found.")
        ''    End If

        ''    If fndSchemeSample.txtValue.Text = String.Empty Then
        ''        obj = Accountsegment.Getaccountcodedesc(strSchemeAcc, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strSchemeAcc), New SqlParameter("@Account_Desc", strSchemeAccDesc), New SqlParameter("@Amount", Math.Round(schemeCogs, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    Else
        ''        Dim sampleshemeacc As String = String.Empty
        ''        Dim sampleschemedesc As String = String.Empty
        ''        sql = "SELECT Account_Code  FROM TSPL_Sampling_Master WHERE Sampling_Code = '" + Convert.ToString(fndSchemeSample.txtValue.Text) + "'"
        ''        sampleshemeacc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + sampleshemeacc + "'"
        ''        sampleschemedesc = connectSql.RunScalar(trans, sql)
        ''        If sampleschemedesc Is Nothing Then
        ''            Throw New Exception("Sample Schemes Account not found.")
        ''        End If
        ''        obj = Accountsegment.Getaccountcodedesc(strSchemeAcc, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", sampleshemeacc), New SqlParameter("@Account_Desc", sampleschemedesc), New SqlParameter("@Amount", Math.Round(schemeCogs + discAmt + taxamountwithdisc, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''End If
        ''If Not promoCogs = 0 Then
        ''    Dim strPromoAcc As String = ""
        ''    Dim strPromoAccDesc As String = ""
        ''    exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    Dim promocount As Integer = 0
        ''    If exicsable = "N" And nonexcise = "F" Then
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''                promocount = promocount + 1
        ''                If grow.Cells("promoSchemeItem").Value = "Yes" Then
        ''                    Exciseamt = 0
        ''                    totalexciseamt = 0
        ''                    Exciseamt = 0
        ''                    ecessamt = 0
        ''                    hcessamt = 0
        ''                    totalecessamt = 0
        ''                    totalhcessamt = 0
        ''                    Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' AND Sale_Invoice_Id = '" + promocount.ToString() + "'"))
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                    Exciseamt = Exciseamt * exciserate / 100
        ''                    totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                    totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''                End If
        ''            End If

        ''        Next
        ''        Dim totalscheme As Decimal = totalexciseamt + totalecessamt + totalhcessamt
        ''        promoCogs = promoCogs - totalscheme
        ''    End If
        ''    sql = " SELECT SA.promotional FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''     " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''     " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strPromoAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strPromoAcc + "'"
        ''    strPromoAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strPromoAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''        Throw New Exception("Promotional Account not found.")
        ''    End If
        ''    obj = Accountsegment.Getaccountcodedesc(strPromoAcc, trans)
        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strPromoAcc), New SqlParameter("@Account_Desc", strPromoAccDesc), New SqlParameter("@Amount", Math.Round(promoCogs, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''    lineNo = lineNo + 1
        ''End If
        ''sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
        ''connectSql.RunSqlTransaction(trans, sql)

        ''sql = "Select SUM(Amount) from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + StrVoucher + "'"
        ''If Not clsCommon.myCdbl(connectSql.RunScalar(trans, sql)) = 0 Then
        ''    ''Throw New Exception(GetJounalEntryException(StrVoucher, trans))
        ''End If
        '' ''Saving in Invoice Price Component Table.
        ''Dim objPC As clsInvoicePriceCompHead = clsInvoicePriceCompHead.GetPriceComponentObject(SaleInvoiceNo, trans)
        ''objPC.SaveData(objPC, trans)
        Return True
    End Function

    Private Function AllowToSave() As Boolean
        'If btnAdd.Text = "Update" Then
        Dim strchk As String = "select Is_Post from TSPL_SALE_RETURN_INTER_HEAD where Document_No='" + txtDocumentNo.Value + "'"
        Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
        If chkpost = "1" Then
            clsCommon.MyMessageBoxShow("Transection already posted")
            Return False
        End If
        'End If



        Dim isCalculateShall As Boolean = IIf(clsCommon.myCdbl(txtshellqty.Text) > 0, False, True)
        If gvLoadOut.CurrentRow.Index < 0 Then
            If clsCommon.myLen(gvLoadOut.CurrentRow.Cells("itemCode").Value) > 0 Then
                Throw New Exception("Last Row is not inserted Properly")
            End If
        End If
        gvLoadOut.CurrentRow = gvLoadOut.Rows(0)
        gvLoadOut.CurrentColumn = gvLoadOut.Columns(0)

        ''RemoveHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
        Dim unitcode As String
        Dim discamt As Double = 0
        Dim TotDiscAmt As Double = 0


        CalculateDiscountAmount()

        Dim arrDiscountCode As New Dictionary(Of String, Integer)
        Dim dtCust As DataTable = clsDBFuncationality.GetDataTable("select Cust_Type_Code,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'")
        Dim strCustType As String = clsCommon.myCstr(dtCust.Rows(0)("Cust_Type_Code"))
        Dim arrShall As New Dictionary(Of Integer, Double)
        For ii As Integer = 0 To gvLoadOut.Rows.Count - 1
            gvLoadOut.Rows(ii).Cells(ColTargetDisAmt).Value = 0
            Dim strSchemeCode As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells("fromSchemeCode").Value)
            Dim strICode As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells("itemCode").Value)
            Dim strPriceDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(gvLoadOut.Rows(ii).Cells("priceDate").Value), "yyyy-MM-dd")
            Dim strUOM As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells("unitCode").Value)
            Dim strMRP As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells("mrp").Value)
            Dim strPriceCode As String = txtPriceCode.Text
            Dim dblQty As Double = clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedqty").Value)
            Dim qry As String = ""
            ''Dim strBatchNo As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells("batchnumber").Value)
            If clsCommon.myLen(strSchemeCode) >= 2 Then
                Dim strTwoCharacher As String = strSchemeCode.Substring(0, 2)
                If clsCommon.CompairString(strTwoCharacher, "MS") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvLoadOut.Rows(ii).Cells("mainitem").Value) <= 0 AndAlso clsCommon.myLen(gvLoadOut.Rows(ii).Cells(colDiscountCode).Value) <= 0 Then
                        Throw New Exception("Please fill the Main Item/Discount Code at Row No" + clsCommon.myCstr(ii + 1))
                    End If
                    Dim strDisCode As String = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colDiscountCode).Value).Trim()
                    If clsCommon.myLen(strDisCode) > 0 Then
                        If Not arrDiscountCode.ContainsKey(strDisCode) Then
                            arrDiscountCode.Add(strDisCode, 0)
                        End If

                        qry = "select Price_Amount1,Price_Amount4,Price_Amount5,Price_Amount6,Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                        Dim dtPC As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtPC Is Nothing OrElse dtPC.Rows.Count <= 0 Then
                            Throw New Exception("Price Component not found for item " + strICode + " at Row No " + (clsCommon.myCstr(ii + 1)))
                        End If
                        Dim dblActMRP As Double = clsCommon.myCdbl(strMRP) - (clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount1")))
                        If clsCommon.CompairString(strCustType, "A") = CompairStringResult.Equal Then
                            dblActMRP = dblActMRP - (clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount5")))
                        ElseIf clsCommon.CompairString(strCustType, "S") = CompairStringResult.Equal Then
                            dblActMRP = dblActMRP - (clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount4")) + clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount6")) + clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount7")))
                        End If

                        Dim dblTargetDiscountAmt As Double = clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("shippedQty").Value) * dblActMRP
                        gvLoadOut.Rows(ii).Cells(ColTargetDisAmt).Value = dblTargetDiscountAmt
                        arrDiscountCode(strDisCode) += dblTargetDiscountAmt
                    End If
                End If
            End If

            If dblQty > 0 Then
                If isCalculateShall AndAlso clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvLoadOut.Rows(ii).Cells("emptyValue").Value) > 0 Then
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    If Not arrShall.ContainsKey(dblConvFac) Then
                        arrShall.Add(dblConvFac, 0)
                    End If
                    arrShall(dblConvFac) += dblQty
                End If


                If clsCommon.myLen(gvLoadOut.Rows(ii).Cells(colPriceDateActual).Value) > 0 Then
                    strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(gvLoadOut.Rows(ii).Cells(colPriceDateActual).Value), "yyyy-MM-dd")
                End If
                qry = "select Start_Date from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                    qry = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' and Start_Date<='" + clsCommon.GetPrintDate(dtpShipDate.Value, "dd/MMM/yyyy") + "'"
                    If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) <= 0 Then
                        Throw New Exception("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "'")
                        Return False
                    End If
                    strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "")
                End If
            End If
        Next

        If isCalculateShall AndAlso arrShall.Count > 0 Then
            Dim dblTotShall As Double
            For Each Keys As Integer In arrShall.Keys
                dblTotShall += Math.Ceiling(arrShall(Keys) / Keys)
            Next
            txtshellqty.Text = clsCommon.myCstr(dblTotShall)
        End If

        If arrDiscountCode IsNot Nothing AndAlso arrDiscountCode.Count > 0 Then
            Dim dt As DataTable = clsTargetMaster.GetBalance(txtDocumentNo.Value, fndCustomer.Value, dtpShipDate.Value)
            For Each strDisCode As String In arrDiscountCode.Keys
                Dim isFound As Boolean = False
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(strDisCode, clsCommon.myCstr(dr("Discount_Type"))) = CompairStringResult.Equal Then
                        isFound = True
                        If arrDiscountCode(strDisCode) > clsCommon.myCdbl(dr("Amount")) Then
                            Throw New Exception("Available Target Balance :" + clsCommon.myCstr(clsCommon.myCdbl(dr("Amount"))) + Environment.NewLine + "Total Enterd Target Amount : " + clsCommon.myCstr(arrDiscountCode(strDisCode)) + Environment.NewLine + "For Discount Code :" + strDisCode)
                        End If
                    End If
                Next
                If Not isFound Then
                    Throw New Exception("Available Target Balance : 0" + Environment.NewLine + "Total Enterd Target Amount : " + clsCommon.myCstr(arrDiscountCode(strDisCode)) + Environment.NewLine + "For Discount Code :" + strDisCode)
                End If
            Next
        End If



        If clsCommon.myLen(txtShipTo.Value) > 0 Then
            Dim qry As String = "select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipTo.Value + "' and Ship_To_Type_Code='" + fndCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Ship to location is not for current customer")
            End If
        End If
        Dim shipQty As Integer = 0
        Dim location As String = fndLocation.Value
        For Each grow As GridViewRowInfo In gvLoadOut.Rows
            If clsCommon.myCdbl(grow.Cells("shippedQty").Value) > 0 Then
                shipQty = shipQty + CInt(grow.Cells("shippedQty").Value)
                unitcode = grow.Cells("unitCode").Value

                If location = "" Then
                    Throw New Exception("Location for item " + grow.Cells("itemCode").Value + " can not be left blank.")
                ElseIf unitcode = "" Then
                    Throw New Exception("Unit Code for item " + grow.Cells("itemCode").Value + " can not be left blank.")
                End If
            End If
        Next
        If fndCustomer.Value = String.Empty Then
            fndCustomer.Focus()
            Throw New Exception("Customer  can not be left blank.")
        ElseIf txtTripNo.Text = String.Empty Then
            txtTripNo.Focus()
            Throw New Exception("Trip No can not be left blank.")
        ElseIf fndSalesman.txtValue.Text = String.Empty Then
            fndSalesman.txtValue.Focus()
            Throw New Exception("Salesman can not be left blank.")
        ElseIf fndLocation.Value = String.Empty Then
            fndLocation.Focus()
            Throw New Exception("Location can not be left blank.")
        ElseIf fndVehicleCode.txtValue.Text = String.Empty Then
            fndVehicleCode.txtValue.Focus()
            Throw New Exception("Vehicle can not be left blank.")
        ElseIf fndPaymentTerms.txtValue.Text = String.Empty Then
            fndPaymentTerms.txtValue.Focus()
            Throw New Exception("Payment Terms can not be left blank.")
        ElseIf fndTaxGroup.txtValue.Text = String.Empty Then
            fndTaxGroup.txtValue.Focus()
            Throw New Exception("Tax Group can not be left blank.")
        ElseIf ddlModeofTransport.Text = "Select" Then
            ddlModeofTransport.Focus()
            Throw New Exception("Please choose a mode of transport from the list.")
        ElseIf ddlPriceDate.Text = "Select" Then
            ddlModeofTransport.Focus()
            Throw New Exception("Please choose a price date from the list.")
        ElseIf shipQty = 0 Then
            Throw New Exception("There is no item to ship.")
        End If
        Return True
    End Function

    Private Function saveDataClicked() As Boolean
        Try
            If AllowToSave() Then
                savedata()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If saveDataClicked() Then
        End If
    End Sub

    Private Function schemeqtyvalidate() As Boolean
        ''commnet for not check the qty 25/06/2012 
        ''Dim originalqty As Decimal = 0
        ''Dim sql As String = "select Item_Code,Pending_Qty, Pending_Balance_In_Bottle from TSPL_TRANSFER_DETAIL where Transfer_No = '" + fndTransferNo.Value + "'"
        ''Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(sql)
        ''Dim conversionamt1 As Decimal
        ''If dr.HasRows Then
        ''    While dr.Read()
        ''        Dim applyqty As Decimal = 0
        ''        Dim itemcode As String = dr("item_code")
        ''        originalqty = dr("Pending_Balance_In_Bottle")
        ''        Dim intCounter As Integer = 1
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If grow.Cells("shippedqty").Value > 0 Then
        ''                If itemcode = grow.Cells("itemcode").Value Then
        ''                    Dim conversionamt As Decimal = connectSql.RunScalar("select  Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + grow.Cells("itemcode").Value + "' and UOM_Code = '" + grow.Cells("unitcode").Value + "'")
        ''                    If conversionamt = 1 Then
        ''                        conversionamt1 = connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and UOM_Code <> '" + Convert.ToString(grow.Cells("unitcode").Value) + "' AND UM.Create_Price = 'Y'")

        ''                        applyqty = applyqty + grow.Cells("shippedQty").Value * conversionamt1
        ''                    Else







        ''                        applyqty = applyqty + grow.Cells("shippedQty").Value
        ''                    End If
        ''                End If
        ''            End If
        ''        Next
        ''        If applyqty > 0 AndAlso applyqty > originalqty Then
        ''            common.clsCommon.MyMessageBoxShow("Apply Qty is more than Shipped Qty of Item " + itemcode)
        ''            dr.Close()
        ''            Return False
        ''        Else
        ''        End If
        ''    End While
        ''    dr.Close()
        ''End If
        ''commnet for not check the qty 25/06/2012 
        Return True

    End Function

    Public Sub savedata()
        Dim shipmentTaxAmt As Decimal = 0.0
        Dim netAmount As Decimal = totalNetAmount()
        Dim shipmentDiscPer As Decimal = 0.0
        Dim shipmentDiscAmt As Decimal = 0.0
        Dim additionalCharges As Decimal = 0.0
        Dim OtherCharges As Decimal = 0.0
        Dim freightCharges As Decimal = 0.0
        Try
            Dim sql As String = "SELECT  Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_SALESMAN_MAPPING WHERE Salesman_Code='" + fndRouteNo.txtValue.Text + "'"
            dr = clsDBFuncationality.GetDataTable(sql)
            If dr IsNot Nothing AndAlso dr.Rows.Count Then
                l1User = Convert.ToString(dr.Rows(0)(0))
                l2User = Convert.ToString(dr.Rows(0)(1))
                l3User = Convert.ToString(dr.Rows(0)(2))
                l4User = Convert.ToString(dr.Rows(0)(3))
                l5User = Convert.ToString(dr.Rows(0)(4))
            Else
                Throw New Exception("Salesman does not exist.")
                fndSalesman.txtValue.Focus()
                Exit Sub
            End If



            If Not txtAdditionalCharges.Text = String.Empty Then
                additionalCharges = txtAdditionalCharges.Text
            End If
            If Not txtOtherCharges.Text = String.Empty Then
                OtherCharges = txtOtherCharges.Text
            End If
            If Not txtFreight.Text = String.Empty Then
                freightCharges = txtFreight.Text
            End If

            For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                shipmentTaxAmt = shipmentTaxAmt + clsCommon.myCdbl(grow.Cells(5).Value)
            Next
            shipmentDiscPer = 0
            shipmentDiscAmt = clsCommon.myCdbl(txtDiscAmt.Text) + clsCommon.myCdbl(txtCustDisc.Text)
            SaveDataNew(totalMRP, totalBasicAmt, totalAssessibleAmt, totalDiscount, shipmentDiscAmt, shipmentDiscPer, shipmentTaxAmt, netAmount, additionalCharges, OtherCharges, freightCharges, totalTransport)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        resetdata()
    End Sub

    Public Sub resetdata()
        gvLoadOut.DataSource = Nothing
        gvTaxDetails.DataSource = Nothing
        gvLoadOut.Rows.Clear()
        gvTaxDetails.Rows.Clear()
        resetFNDCustomer()
        resetFNDTaxGroup()
        resetForm()
        isNewEntry = True
        isCellValueChangedOpen = False
        dtpShipDate.Enabled = True
        btnPost.Visible = True
        rbtnExcise.IsChecked = True
        GroupBox1.Enabled = True
        btnAdd.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub

    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub

    Private Sub deletedata()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then

                Dim Reason As String = ""
                If common.clsCommon.MyMessageBoxShow("Delete the Current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If clsCancelLog.CheckForReasonOnDelete() Then
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Delete"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                    If clsSaleReturnInterCompany.DeleteData(txtDocumentNo.Value) Then
                        saveCancelLog(Reason, Nothing)
                        common.clsCommon.MyMessageBoxShow("Data Deleted successfully", Me.Text)
                        resetdata()
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocumentNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Delete"
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        postdata()
    End Sub

    Private Sub postdata()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Post the Current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    savedata()
                    clsSaleReturnInterCompany.PostData(txtDocumentNo.Value)
                    common.clsCommon.MyMessageBoxShow("Data Posted successfully", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        clicked = False
    End Sub

    Private Sub rdbPer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbPer.ToggleStateChanged
        totalAmounts()
    End Sub

    Private Sub rbFB_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFB.ToggleStateChanged
        If rbFB.ToggleState = ToggleState.On Then
            priceDateSelection("FB")
        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbFC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFC.ToggleStateChanged
        If rbFC.ToggleState = ToggleState.On Then
            priceDateSelection("FC")
        End If

        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbAll.ToggleStateChanged

        If rbAll.ToggleState = ToggleState.On Then
            priceDateSelection()
        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub FrmShipment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            saveDataClicked()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()

        ElseIf e.KeyCode = Keys.Enter Then
            If gvLoadOut.Rows.Count > 0 Then
                Dim i As Integer = gvLoadOut.CurrentRow.Index + 1
                If gvLoadOut.Rows.Count > i Then
                    gvLoadOut.CurrentRow = gvLoadOut.Rows(i)
                ElseIf gvLoadOut.Rows.Count = i Then
                    gvLoadOut.CurrentRow = gvLoadOut.Rows(0)
                End If
            End If
        ElseIf e.Control And e.Alt And e.Shift And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndRecreate.Visible = True
            End If
        End If
    End Sub

    Private Sub txtshellqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshellqty.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub fndemployeecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndemployeecode.Load
        fndemployeecode.ConnectionString = connectSql.SqlCon()
        fndemployeecode.Query = "select EMP_CODE as [Employee Code], Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER"
        fndemployeecode.ValueToSelect = "Employee Code"
        fndemployeecode.Caption = "Employee Details"
        fndemployeecode.ValueToSelect1 = "Employee Name"
    End Sub

    Private Function postInvoice(ByVal trans As SqlTransaction, ByVal SaleInvoiceNo As String) As Boolean
        ''Dim ttlMrp As Decimal = totalMRP()
        ''Dim totaltaxrecover As Decimal
        ''Dim schemeMrp As Decimal = 0
        ''Dim totalexciseamt As Decimal = 0
        ''Dim totalecessamt As Decimal = 0
        ''Dim nonexcise As String = ""
        ''Dim totalhcessamt As Decimal = 0
        ''Dim exicsable As String = String.Empty
        ''Dim locationsegcode As String = String.Empty
        ''Dim Excisedutyacct, ecessacct, hcessacct, Excisedutydesc, ecessdesc, hcessdesc As String
        ''Dim Exciseamt As Decimal = 0
        ''Dim exciserate As Decimal = 0
        ''Dim ecessrate As Decimal = 0
        ''Dim hcessrate As Decimal = 0
        ''Dim ecessamt As Decimal = 0
        ''Dim hcessamt As Decimal = 0
        ''Dim schemeAmt As Decimal = 0
        ''Dim promoMrp As Decimal = 0
        ''Dim promoAmt As Decimal = 0
        ''Dim discAmt As Decimal = totalDiscount()
        ''Dim netAmt As Decimal = connectSql.RunScalar(trans, "select isnull(Shipment_Detail_Total_Amt,0)  from TSPL_SHIPMENT_MASTER   where Shipment_No = '" + fndLoadOut.Value + "'")
        ''Dim cogs As Decimal = 0
        ''Dim totalsaleamt As Decimal = 0
        ''Dim schemeCogs As Decimal = 0
        ''Dim promoCogs As Decimal = 0

        ''Dim CurrCogs As Decimal = 0
        ''Dim CurrSchemeCogs As Decimal = 0
        ''Dim CurrPromoCogs As Decimal = 0
        ''Dim CurrSchemeAmount As Decimal = 0

        ''Dim dblTotExciseTaxAmtFor100Per As Double = 0
        ''Dim dblTotExciseTaxAmtForSample As Double = 0
        ''Dim taxamountwithdisc As Decimal = connectSql.RunScalar(trans, "select isnull(shipment_tax_amt,0) from tspl_shipment_master where shipment_no = '" + fndLoadOut.Value + "'")
        ''Dim emptyValue As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "SELECT ISNULL(Empty_Value,0)  FROM TSPL_SHIPMENT_MASTER WHERE Shipment_No = '" + fndLoadOut.Value + "'"))
        ''Dim shipmentDiscAmt As Decimal
        ''Dim conversionamt As Decimal = 0
        ''Dim schemeamount As Decimal = 0

        ''If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
        ''    discAmt = connectSql.RunScalar(trans, "select Shipment_Discount_Amt  from tspl_shipment_master where Shipment_No = '" + Convert.ToString(fndLoadOut.Value) + "'")
        ''ElseIf clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
        ''    discAmt = totalDiscount()
        ''    shipmentDiscAmt = txtCustDisc.Text
        ''    discAmt = discAmt + shipmentDiscAmt
        ''End If

        ''Dim count As Integer
        ''Dim convfact As Decimal = 0
        ''For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''    If clsCommon.myCdbl(grow.Cells("shippedqty").Value) > 0 Then
        ''        count = count + 1
        ''        sql = "SELECT Unit_COGS,Scheme_Item,Promo_Scheme_Item,Sampling_Item from TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + fndLoadOut.Value + "' " & _
        ''        " AND Shipment_Id='" + count.ToString() + "'"
        ''        Dim postDr As SqlDataReader = connectSql.RunSqlReturnDR(trans, sql)
        ''        While postDr.Read()
        ''            If postDr(1).ToString() = "N" AndAlso postDr(2).ToString() = "N" AndAlso postDr(3).ToString() = "N" Then
        ''                convfact = connectSql.ReturnConvFact(grow.Cells("itemcode").Value, grow.Cells("unitcode").Value)
        ''                CurrCogs = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString()) / convfact, 2, MidpointRounding.ToEven)
        ''                cogs = cogs + CurrCogs
        ''            ElseIf postDr(1).ToString() = "Y" Or postDr(3).ToString() = "Y" Then
        ''                convfact = connectSql.ReturnConvFact(grow.Cells("itemcode").Value, grow.Cells("unitcode").Value)
        ''                CurrSchemeCogs = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString() / convfact), 2, MidpointRounding.ToEven)
        ''                schemeCogs = schemeCogs + CurrSchemeCogs

        ''                CurrSchemeAmount = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString() / convfact), 2, MidpointRounding.ToEven)
        ''                schemeamount = schemeamount + CurrSchemeAmount
        ''            ElseIf postDr(2).ToString() = "Y" Then
        ''                CurrPromoCogs = Math.Round(clsCommon.myCdbl(grow.Cells("shippedqty").Value) * clsCommon.myCdbl(postDr(0).ToString()), 2, MidpointRounding.ToEven)
        ''                promoCogs = promoCogs + CurrPromoCogs
        ''            End If
        ''        End While
        ''        postDr.Close()
        ''        If clsCommon.myCdbl(grow.Cells("shippedqty").Value) > 0 Then
        ''            If grow.Cells("schemeItem").Value = "Yes" Or grow.Cells("sampleItem").Value = "Yes" Then
        ''                schemeMrp = schemeMrp + clsCommon.myCdbl(grow.Cells("totalMRP").Value)
        ''            End If
        ''            If grow.Cells("promoSchemeItem").Value = "Yes" Then
        ''                promoMrp = promoMrp + clsCommon.myCdbl(grow.Cells("totalMRP").Value)
        ''            End If

        ''        End If
        ''    End If
        ''Next
        ''sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "'"
        ''Dim locSegCode As String = connectSql.RunScalar(trans, sql)
        ''sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET Is_Post='Y' WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''connectSql.RunSqlTransaction(trans, sql)
        ''sql = "Select Empty_Value from TSPL_SALE_INVOICE_HEAD WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''Dim InvoiceEmptyVal As Decimal = connectSql.RunScalar(trans, sql)
        ''sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET Balance_Amt='" + (clsCommon.myCdbl(txtShipmentAmt.Text) + InvoiceEmptyVal).ToString() + "' WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''connectSql.RunSqlTransaction(trans, sql)

        ''If (ttlMrp + schemeMrp + promoMrp) > 0 Then
        ''    Dim lineNo As Integer = 1
        ''    Dim frmj As New frmJournalEntry(userCode, companyCode)
        ''    Dim StrVoucher As String = frmj.fnAutoGenerateNo(trans)
        ''    sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'SD-IN'"
        ''    Dim strSourceDesc As String = connectSql.RunScalar(trans, sql)
        ''    Dim strInvoiceNo As String = Invoiceno

        ''    Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        ''    Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1
        ''    Dim dt As String = dtpShipDate.Value.ToString("dd/MM/yyyy")
        ''    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Source_Code", "SD-IN"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", dt), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Voucher_Desc", "Sale against Invoice No " + Invoiceno), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", "Shipment No " + fndLoadOut.Value + " for customer " + txtCustomerName.Text), New SqlParameter("@Comments", Me.txtRemarks.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", dt), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", fndCustomer.Value), New SqlParameter("@CustVend_Name", Me.txtCustomerName.Text), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", txtShipmentAmt.Text), New SqlParameter("@Total_Credit_Amt", txtShipmentAmt.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy")), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy")), New SqlParameter("@Comp_Code", companyCode))

        ''    Dim strRecAcc As String
        ''    Dim strRecAccDesc As String
        ''    sql = "SELECT A.Receivable_Control_acct FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN " & _
        ''          "  TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  C.Cust_Code = '" + fndCustomer.Value + "'"
        ''    strRecAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecAcc + "'"
        ''    strRecAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strRecAccDesc Is Nothing Then
        ''        Throw New Exception("Receivable Control Account not found.")
        ''    End If
        ''    Dim obj As Accountsegment
        ''    If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''    Else
        ''        If Not clsCommon.myCdbl(Me.txtShipmentAmt.Text) = 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strRecAcc, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecAcc), New SqlParameter("@Account_Desc", strRecAccDesc), New SqlParameter("@Amount", Math.Round(clsCommon.myCdbl(Me.txtShipmentAmt.Text), 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If

        ''    Dim strContainerAcc As String = String.Empty
        ''    Dim strContainerDesc As String = String.Empty
        ''    sql = "SELECT A.container_deposit FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN   TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  C.Cust_Code = '" + fndCustomer.Value + "'"
        ''    strContainerAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strContainerAcc + "'"
        ''    strContainerDesc = connectSql.RunScalar(trans, sql)
        ''    If strContainerDesc Is Nothing Then
        ''        Throw New Exception("Container Account not found.")
        ''    End If
        ''    If emptyValue > 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(strContainerAcc, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strContainerAcc), New SqlParameter("@Account_Desc", strContainerDesc), New SqlParameter("@Amount", Math.Round(emptyValue, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If

        ''    Dim strSalesAcc As String
        ''    Dim strSalesAccDesc As String
        ''    sql = "SELECT SA.Sales_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
        ''             "TSPL_SALES_ACCOUNTS AS SA ON IM.Sale_Class_Code = SA.Sales_Class_Code " & _
        ''             " WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strSalesAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strSalesAcc + "'"
        ''    strSalesAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strSalesAccDesc Is Nothing Then
        ''        Throw New Exception("Sales Account not found.")
        ''    End If
        ''    If netAmt > 0 Then

        ''        totalsaleamt = netAmt
        ''        obj = Accountsegment.Getaccountcodedesc(strSalesAcc, trans)

        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strSalesAcc), New SqlParameter("@Account_Desc", strSalesAccDesc), New SqlParameter("@Amount", Math.Round(netAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If


        ''    Dim strRecDiscAcc As String
        ''    Dim strRecDiscAccDesc As String
        ''    sql = "SELECT  A.Receipts_Discount_acct FROM TSPL_CUSTOMER_ACCOUNT_SET AS A INNER JOIN " & _
        ''          " TSPL_CUSTOMER_MASTER AS C ON A.Cust_Account = C.Cust_Account WHERE  (C.Cust_Code = '" + fndCustomer.Value + "')"
        ''    strRecDiscAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecDiscAcc + "'"
        ''    strRecDiscAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strRecDiscAccDesc Is Nothing Then
        ''        Throw New Exception("Receipts discount Account not found.")
        ''    End If




        ''    exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    If exicsable = "N" And nonexcise = "F" Then
        ''        dblTotExciseTaxAmtFor100Per = 0
        ''        locationsegcode = connectSql.RunScalar(trans, "SELECT Loc_Segment_Code   FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        Excisedutyacct = "3004-" + Convert.ToString(locationsegcode)
        ''        Excisedutydesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + Excisedutyacct + "'")
        ''        If Excisedutydesc Is Nothing Then
        ''            Throw New Exception("Excise Duty Recovered Account not found.")
        ''        End If
        ''        ecessacct = "3011-" + Convert.ToString(locationsegcode)
        ''        ecessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + ecessacct + "'")
        ''        If ecessdesc Is Nothing Then
        ''            Throw New Exception("E.Cess Duty Recovered Account not found.")
        ''        End If
        ''        hcessacct = "3012-" + Convert.ToString(locationsegcode)
        ''        hcessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + hcessacct + "'")
        ''        If hcessdesc Is Nothing Then
        ''            Throw New Exception("H.Cess Duty Recovered Account not found.")
        ''        End If
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If clsCommon.myCdbl(grow.Cells("shippedqty").Value) > 0 Then

        ''                If grow.Cells("schemeItem").Value = "No" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("sampleItem").Value = "No" Then
        ''                    Exciseamt = 0

        ''                    Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'Y' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))

        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                Exciseamt = Exciseamt * exciserate / 100
        ''                totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100

        ''            End If
        ''                If grow.Cells("sampleItem").Value = "Yes" Then
        ''                    Exciseamt = 0
        ''                    If fndSchemeSample.txtValue.Text = String.Empty Then
        ''                        Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))

        ''                    Else
        ''                        Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'Y' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and Unit_code = '" + Convert.ToString(grow.Cells("unitCode").Value) + "'"))

        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                        hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                    Else
        ''                        Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                    End If
        ''                    totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                    Exciseamt = Exciseamt * exciserate / 100
        ''                    totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                    totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''                End If
        ''            End If

        ''        Next
        ''        totaltaxrecover = Math.Round(totalexciseamt + totalecessamt + totalhcessamt, 2, MidpointRounding.ToEven)
        ''        If Not clsCommon.myCdbl(totalexciseamt) = 0 Then
        ''            If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''                dblTotExciseTaxAmtFor100Per += totalexciseamt
        ''            Else
        ''                obj = Accountsegment.Getaccountcodedesc(Excisedutyacct, trans)
        ''                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", Excisedutyacct), New SqlParameter("@Account_Desc", Excisedutydesc), New SqlParameter("@Amount", Math.Round(totalexciseamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                lineNo = lineNo + 1
        ''            End If
        ''        End If
        ''        If Not clsCommon.myCdbl(totalecessamt) = 0 Then
        ''            If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''                dblTotExciseTaxAmtFor100Per += totalecessamt
        ''            Else
        ''                obj = Accountsegment.Getaccountcodedesc(ecessacct, trans)
        ''                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", ecessacct), New SqlParameter("@Account_Desc", ecessdesc), New SqlParameter("@Amount", Math.Round(totalecessamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                lineNo = lineNo + 1
        ''            End If
        ''        End If

        ''        If Not clsCommon.myCdbl(totalhcessamt) = 0 Then
        ''            If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''                dblTotExciseTaxAmtFor100Per += totalhcessamt
        ''            Else
        ''                obj = Accountsegment.Getaccountcodedesc(hcessacct, trans)
        ''                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", hcessacct), New SqlParameter("@Account_Desc", hcessdesc), New SqlParameter("@Amount", Math.Round(totalhcessamt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                lineNo = lineNo + 1
        ''            End If
        ''        End If
        ''    End If
        ''    Dim taxAmt As Decimal
        ''    Dim icount As Integer = 0
        ''    Dim countRow As Integer = 0
        ''    Try
        ''        For countRow = 0 To gvTaxDetails.Rows.Count - 1
        ''            taxAmt = 0
        ''            Dim gro As GridViewRowInfo = gvTaxDetails.Rows(countRow)
        ''            Dim strTaxCode As String = gro.Cells("taxAuthority").Value
        ''            icount = icount + 1
        ''            Dim check As String = connectSql.RunScalar(trans, "select tm.Excisable  from TSPL_TAX_GROUP_DETAILS gd join TSPL_TAX_MASTER tm on gd.Tax_Code = tm.Tax_Code  where gd .Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' and gd.Tax_Code = '" + strTaxCode + "'")
        ''            If check.Trim() = "Y" Then
        ''                sql = "select TAX" + icount.ToString() + "_Amt as [tax],Invoice_Qty as [qty]  from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No ='" + Invoiceno + "' AND Scheme_Item = 'N' AND Promo_Scheme_Item = 'N'"
        ''                Dim drtax As SqlDataReader = connectSql.RunSqlReturnDR(trans, sql)
        ''                If drtax.HasRows Then
        ''                    While drtax.Read()
        ''                        taxAmt = taxAmt + clsCommon.myCdbl(drtax("tax")) * clsCommon.myCdbl(drtax("qty"))
        ''                    End While
        ''                End If
        ''                drtax.Close()
        ''            Else
        ''                taxAmt = clsCommon.myCdbl(gro.Cells("taxAmount").Value)
        ''            End If
        ''            Dim strLiableAcc As String = ""
        ''            Dim strLiableAccDesc As String = ""
        ''            sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER  WHERE Tax_Code = '" + strTaxCode + "'"


        ''            strLiableAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''            sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strLiableAcc + "'"
        ''            strLiableAccDesc = connectSql.RunScalar(trans, sql)
        ''            If strLiableAccDesc Is Nothing Then
        ''                Throw New Exception("Tax Liability Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''            End If
        ''            Dim strRecoverableAcc As String = ""
        ''            Dim strRecoverableAccDesc As String = ""
        ''            Dim strNetPayAcc As String = ""
        ''            Dim strNetPayAccDesc As String = ""
        ''            sql = "SELECT Tax_Recoverable_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
        ''            If Not connectSql.RunScalar(trans, sql).ToString() = "" Then
        ''                strRecoverableAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''            End If

        ''            If Not strRecoverableAcc = "" Then
        ''                sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRecoverableAcc + "'"
        ''                strRecoverableAccDesc = connectSql.RunScalar(trans, sql)
        ''            End If
        ''            sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
        ''            If Not connectSql.RunScalar(trans, sql).ToString() = "" Then
        ''                strNetPayAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''            End If
        ''            If Not strNetPayAcc = "" Then
        ''                sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
        ''                strNetPayAccDesc = connectSql.RunScalar(trans, sql)
        ''            End If
        ''            If Not taxAmt = 0 Then
        ''                If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''                    dblTotExciseTaxAmtFor100Per += taxAmt
        ''                Else
        ''                    obj = Accountsegment.Getaccountcodedesc(strLiableAcc, trans)
        ''                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strLiableAcc), New SqlParameter("@Account_Desc", strLiableAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                    lineNo = lineNo + 1
        ''                End If
        ''            End If
        ''            If Not strRecoverableAcc = "" AndAlso Not strNetPayAcc = "" Then
        ''                If strRecoverableAccDesc Is Nothing Then
        ''                    Throw New Exception("Tax Recoverable Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''                    Return False
        ''                ElseIf strNetPayAccDesc Is Nothing Then
        ''                    Throw New Exception("Tax Net Payable Account for " + gro.Cells("taxAuthority").Value + " not found.")
        ''                    Return False
        ''                End If
        ''            End If
        ''            If Not strNetPayAcc = "" AndAlso Not strRecAcc = "" Then
        ''                Dim taxamt12 As String = "Tax" + icount.ToString() + "Amt"
        ''                If Not schemeCogs = 0 Then
        ''                    For Each grr As GridViewRowInfo In gvLoadOut.Rows
        ''                        If grr.Cells("schemeItem").Value = "Yes" Or grr.Cells("sampleItem").Value = "Yes" Then
        ''                            taxAmt = taxAmt + clsCommon.myCdbl(grr.Cells(taxamt12).Value) * clsCommon.myCdbl(grr.Cells("shippedqty").Value)
        ''                        End If
        ''                    Next
        ''                End If
        ''                If Not promoCogs = 0 Then
        ''                    For Each grr As GridViewRowInfo In gvLoadOut.Rows
        ''                        If grr.Cells("promoSchemeItem").Value = "Yes" Then
        ''                            promoAmt = promoAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("invQty").Value)
        ''                            promoMrp = promoMrp + promoAmt
        ''                            ' taxAmt = taxAmt + clsCommon.myCdbl(grr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(grr.Cells("invQty").Value)
        ''                            taxAmt = taxAmt + clsCommon.myCdbl(grr.Cells(taxamt12).Value) * clsCommon.myCdbl(grr.Cells("shippedqty").Value)
        ''                        End If
        ''                    Next
        ''                End If
        ''                If Not taxAmt = 0 Then
        ''                    If chkSample.Checked Then
        ''                        dblTotExciseTaxAmtForSample += taxAmt
        ''                    End If
        ''                    obj = Accountsegment.Getaccountcodedesc(strRecoverableAcc, trans)

        ''                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRecoverableAcc), New SqlParameter("@Account_Desc", strRecoverableAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                    lineNo = lineNo + 1
        ''                End If

        ''                ''If Not taxAmt = 0  for 100 % Tax
        ''                If Not taxAmt = 0 AndAlso Not clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''                    obj = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
        ''                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", Math.Round(taxAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''                    lineNo = lineNo + 1
        ''                End If
        ''            End If
        ''        Next
        ''    Catch ex As Exception
        ''        Throw New Exception(ex.Message)
        ''    End Try
        ''    Dim strMarginAcc As String = ""
        ''    Dim strMarginAccDesc As String = ""
        ''    Dim marginAmt As Decimal = 0
        ''    Dim gTotalMargin As Decimal = 0
        ''    sql = "SELECT Price_Comp_Account_Code  FROM TSPL_PRICE_COMPONENT_MASTER WHERE TPT_Type = 'Y' AND Price_Comp_code IN (SELECT Price_Comp_Code  FROM TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "')"
        ''    If Not String.IsNullOrEmpty(connectSql.RunScalar(trans, sql)) Then
        ''        strMarginAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strMarginAcc + "'"
        ''        strMarginAccDesc = connectSql.RunScalar(trans, sql)

        ''    End If
        ''    If strMarginAccDesc Is Nothing Or strMarginAccDesc = "" Then
        ''        Throw New Exception("TPT Account not found.")
        ''        Return False
        ''    End If
        ''    marginAmt = 0
        ''    If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
        ''        marginAmt = connectSql.RunScalar(trans, "select isnull(Total_TPT,0)    from TSPL_SHIPMENT_MASTER where Shipment_No = '" + fndLoadOut.Value + "'")
        ''    Else
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If grow.Cells("shippedqty").Value > 0 Then

        ''                If grow.Cells("schemeItem").Value = "No" And grow.Cells("promoSchemeItem").Value = "No" And grow.Cells("SampleItem").Value = "No" Then
        ''                    marginAmt = marginAmt + clsCommon.myCdbl(grow.Cells("totalTPT").Value)
        ''                End If
        ''            End If
        ''        Next
        ''    End If

        ''    If fndSchemeSample.txtValue.Text = String.Empty Then
        ''        If marginAmt > 0 Then
        ''            obj = Accountsegment.Getaccountcodedesc(strMarginAcc, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strMarginAcc), New SqlParameter("@Account_Desc", strMarginAccDesc), New SqlParameter("@Amount", Math.Round(marginAmt, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If

        ''    Dim strCogAcc As String = ""
        ''    Dim strCogAccDesc As String = ""
        ''    Dim strShpClrAcc As String = ""
        ''    Dim strShpClrAccDesc As String = ""
        ''    sql = " SELECT SA.Cost_Of_Goods_Sold FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''         " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''         " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strCogAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strCogAcc + "'"
        ''    strCogAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strCogAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''        Throw New Exception("Cost of goods sold Account not found.")
        ''        Return False
        ''    End If
        ''    sql = "SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
        ''          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
        ''          " TSPL_GL_ACCOUNTS AS GLA ON PA.Shipment_Clearing = GLA.Account_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strShpClrAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strShpClrAcc + "'"
        ''    strShpClrAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strShpClrAccDesc Is Nothing Or strShpClrAccDesc = "" Then
        ''        Throw New Exception("Shipment Clearing Account not found.")
        ''        Return False
        ''    End If
        ''    Dim dblSamaplingAmt As Double = 0
        ''    If (cogs + schemeCogs + promoCogs) > 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(strShpClrAcc, trans)

        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strShpClrAcc), New SqlParameter("@Account_Desc", strShpClrAccDesc), New SqlParameter("@Amount", Math.Round((cogs + promoCogs + schemeCogs), 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''        dblSamaplingAmt = cogs + schemeCogs + promoCogs
        ''    End If
        ''    If cogs > 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(strCogAcc, trans)

        ''        Dim dblAmtToJE As Double = cogs
        ''        If clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
        ''            dblAmtToJE = cogs - dblTotExciseTaxAmtFor100Per
        ''        End If

        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strCogAcc), New SqlParameter("@Account_Desc", strCogAccDesc), New SqlParameter("@Amount", Math.Round(dblAmtToJE, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''    nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")

        ''    If exicsable = "N" And nonexcise = "F" Then   ''Exciable means Duty paid and NonExcise means Excisable in database 
        ''        dblTotExciseTaxAmtForSample = 0
        ''        totalexciseamt = 0
        ''        Exciseamt = 0
        ''        ecessamt = 0
        ''        hcessamt = 0
        ''        totalecessamt = 0
        ''        totalhcessamt = 0
        ''        locationsegcode = connectSql.RunScalar(trans, "SELECT Loc_Segment_Code   FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        Excisedutyacct = "4817-" + Convert.ToString(locationsegcode)
        ''        Excisedutydesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + Excisedutyacct + "'")
        ''        If Excisedutydesc Is Nothing Then
        ''            Throw New Exception("Excise Duty Paid Account not found.")
        ''        End If
        ''        ecessacct = "4818-" + Convert.ToString(locationsegcode)
        ''        ecessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + ecessacct + "'")
        ''        If ecessdesc Is Nothing Then
        ''            Throw New Exception("E.Cess Duty Paid Account not found.")
        ''        End If
        ''        hcessacct = "4819-" + Convert.ToString(locationsegcode)
        ''        hcessdesc = connectSql.RunScalar(trans, "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + hcessacct + "'")
        ''        If hcessdesc Is Nothing Then
        ''            Throw New Exception("H.Cess Duty Paid Account not found.")
        ''        End If
        ''        Dim count1 As Integer = 0
        ''        For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''            If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''                count1 = count1 + 1
        ''                Exciseamt = 0
        ''                Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' and Sale_Invoice_Id ='" + count1.ToString() + "'"))
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                    hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                Else
        ''                    Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                End If
        ''                totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                Exciseamt = Exciseamt * exciserate / 100
        ''                totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''            End If

        ''        Next
        ''        If Not clsCommon.myCdbl(totalexciseamt) = 0 Then
        ''            If chkSample.Checked Then
        ''                dblTotExciseTaxAmtForSample += totalexciseamt
        ''            End If
        ''            obj = Accountsegment.Getaccountcodedesc(Excisedutyacct, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", Excisedutyacct), New SqlParameter("@Account_Desc", Excisedutydesc), New SqlParameter("@Amount", Math.Round(totalexciseamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''        If Not clsCommon.myCdbl(totalecessamt) = 0 Then
        ''            If chkSample.Checked Then
        ''                dblTotExciseTaxAmtForSample += totalecessamt
        ''            End If
        ''            obj = Accountsegment.Getaccountcodedesc(ecessacct, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", ecessacct), New SqlParameter("@Account_Desc", ecessdesc), New SqlParameter("@Amount", Math.Round(totalecessamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''        If Not clsCommon.myCdbl(totalhcessamt) = 0 Then
        ''            If chkSample.Checked Then
        ''                dblTotExciseTaxAmtForSample += totalhcessamt
        ''            End If
        ''            obj = Accountsegment.Getaccountcodedesc(hcessacct, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", hcessacct), New SqlParameter("@Account_Desc", hcessdesc), New SqlParameter("@Amount", Math.Round(totalhcessamt, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If
        ''    ''Dim totaltaxrecover As Decimal = Math.Round(totalexciseamt + totalecessamt + totalhcessamt, 2)
        ''    nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")

        ''    If exicsable = "N" And nonexcise = "F" Then
        ''        totalsaleamt = totalsaleamt - totaltaxrecover
        ''        cogs = cogs - totaltaxrecover
        ''        totalsaleamt = totalsaleamt * (-1)
        ''        sql = "update TSPL_JOURNAL_DETAILS set amount = '" + Convert.ToString(totalsaleamt) + "' where Voucher_No = '" + Convert.ToString(StrVoucher) + "' and Account_code = '" + Convert.ToString(strSalesAcc) + "'"
        ''        connectSql.RunSqlTransaction(trans, sql)
        ''        sql = "update TSPL_JOURNAL_DETAILS set amount = '" + Convert.ToString(cogs) + "' where Voucher_No = '" + Convert.ToString(StrVoucher) + "'  and Account_code = '" + Convert.ToString(strCogAcc) + "'"
        ''        connectSql.RunSqlTransaction(trans, sql)
        ''    End If
        ''    Dim strRetContAcc As String = ""
        ''    Dim strRetContAccDesc As String = ""
        ''    sql = " SELECT SA.Returnable_Container FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''         " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Returnable_Container INNER JOIN " & _
        ''         " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''    strRetContAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''    sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strRetContAcc + "'"
        ''    strRetContAccDesc = connectSql.RunScalar(trans, sql)
        ''    If strRetContAccDesc Is Nothing Or strRetContAccDesc = "" Then
        ''        Throw New Exception("Returnable Container Account not found.")
        ''        Return False
        ''    End If
        ''    If emptyValue > 0 Then
        ''        obj = Accountsegment.Getaccountcodedesc(strRetContAcc, trans)
        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strRetContAcc), New SqlParameter("@Account_Desc", strRetContAccDesc), New SqlParameter("@Amount", Math.Round(emptyValue, 2, MidpointRounding.ToEven) * (-1)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    If Not schemeCogs = 0 Then
        ''        Dim strSchemeAcc As String = ""
        ''        Dim strSchemeAccDesc As String = ""
        ''        exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        Dim countscheme As Integer = 0
        ''        If exicsable = "N" And nonexcise = "F" Then
        ''            For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''                If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''                    countscheme = countscheme + 1
        ''                    If grow.Cells("schemeItem").Value = "Yes" Or grow.Cells("sampleItem").Value = "Yes" Then
        ''                        Exciseamt = 0
        ''                        totalexciseamt = 0
        ''                        Exciseamt = 0
        ''                        ecessamt = 0
        ''                        hcessamt = 0
        ''                        totalecessamt = 0
        ''                        totalhcessamt = 0
        ''                        Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' and Sale_Invoice_Id = '" + countscheme.ToString() + "'"))
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                        Exciseamt = Exciseamt * exciserate / 100
        ''                        totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                        totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''                    End If
        ''                End If

        ''            Next
        ''            Dim totalscheme As Decimal = Math.Round(totalexciseamt + totalecessamt + totalhcessamt, 2, MidpointRounding.ToEven)
        ''            schemeCogs = schemeCogs - totalscheme
        ''        End If

        ''        sql = " SELECT SA.Schemes FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''         " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''         " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''        strSchemeAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strSchemeAcc + "'"
        ''        strSchemeAccDesc = connectSql.RunScalar(trans, sql)
        ''        If strSchemeAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''            Throw New Exception("Schemes Account not found.")
        ''            Return False
        ''        End If

        ''        If fndSchemeSample.txtValue.Text = String.Empty Then
        ''            obj = Accountsegment.Getaccountcodedesc(strSchemeAcc, trans)
        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strSchemeAcc), New SqlParameter("@Account_Desc", strSchemeAccDesc), New SqlParameter("@Amount", Math.Round(schemeCogs, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            lineNo = lineNo + 1
        ''        Else
        ''            Dim sampleshemeacc As String = String.Empty
        ''            Dim sampleschemedesc As String = String.Empty
        ''            sql = "SELECT Account_Code  FROM TSPL_Sampling_Master WHERE Sampling_Code = '" + Convert.ToString(fndSchemeSample.txtValue.Text) + "'"
        ''            sampleshemeacc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''            sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + sampleshemeacc + "'"
        ''            sampleschemedesc = connectSql.RunScalar(trans, sql)
        ''            If sampleschemedesc Is Nothing Then
        ''                Throw New Exception("Sample Schemes Account not found.")
        ''                Return False
        ''            End If
        ''            obj = Accountsegment.Getaccountcodedesc(strSchemeAcc, trans)
        ''            ''connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", sampleshemeacc), New SqlParameter("@Account_Desc", sampleschemedesc), New SqlParameter("@Amount", Math.Round(schemeCogs + discAmt + taxamountwithdisc, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''            Dim dblAmtForGL As Double = dblSamaplingAmt

        ''            If chkSample.Checked Then
        ''                dblAmtForGL = dblSamaplingAmt - dblTotExciseTaxAmtForSample
        ''            End If

        ''            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", sampleshemeacc), New SqlParameter("@Account_Desc", sampleschemedesc), New SqlParameter("@Amount", Math.Round((dblAmtForGL), 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))

        ''            lineNo = lineNo + 1
        ''        End If
        ''    End If
        ''    If Not promoCogs = 0 Then
        ''        Dim strPromoAcc As String = ""
        ''        Dim strPromoAccDesc As String = ""
        ''        exicsable = connectSql.RunScalar(trans, "SELECT DutyPaid  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        nonexcise = connectSql.RunScalar(trans, "SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'")
        ''        Dim promocount As Integer = 0
        ''        If exicsable = "N" And nonexcise = "F" Then
        ''            For Each grow As GridViewRowInfo In gvLoadOut.Rows
        ''                If clsCommon.myCdbl(grow.Cells("shippedqty").Value) <> 0 Then
        ''                    promocount = promocount + 1
        ''                    If grow.Cells("promoSchemeItem").Value = "Yes" Then
        ''                        Exciseamt = 0
        ''                        totalexciseamt = 0
        ''                        Exciseamt = 0
        ''                        ecessamt = 0
        ''                        hcessamt = 0
        ''                        totalecessamt = 0
        ''                        totalhcessamt = 0
        ''                        Exciseamt = Exciseamt + clsCommon.myCdbl(connectSql.RunScalar(trans, "select ISNULL(SUM(ISNULL(Total_Assessable_Amt,0) ),0) from TSPL_SALE_INVOICE_DETAIL  where Sale_Invoice_No = '" + Invoiceno + "' and Item_Code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "' and unit_code = '" + Convert.ToString(grow.Cells("unitcode").Value) + "' AND Sale_Invoice_Id = '" + promocount.ToString() + "'"))
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            exciserate = connectSql.RunScalar(trans, "select excise from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("Excise Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            ecessrate = connectSql.RunScalar(trans, "select ecess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("E.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        If Not IsDBNull(connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")) Then
        ''                            hcessrate = connectSql.RunScalar(trans, "select hcess from tspl_item_tax where item_code = '" + Convert.ToString(grow.Cells("itemcode").Value) + "'")
        ''                        Else
        ''                            Throw New Exception("H.Cess Rate Not defined for this item code " + Convert.ToString(grow.Cells("itemcode").Value))
        ''                        End If
        ''                        totalexciseamt = totalexciseamt + Exciseamt * exciserate / 100
        ''                        Exciseamt = Exciseamt * exciserate / 100
        ''                        totalecessamt = totalecessamt + Exciseamt * ecessrate / 100
        ''                        totalhcessamt = totalhcessamt + Exciseamt * hcessrate / 100
        ''                    End If
        ''                End If
        ''            Next
        ''            Dim totalscheme As Decimal = Math.Round(totalexciseamt + totalecessamt + totalhcessamt, 2, MidpointRounding.ToEven)
        ''            promoCogs = promoCogs - totalscheme
        ''        End If
        ''        sql = " SELECT SA.promotional FROM TSPL_GL_ACCOUNTS AS G INNER JOIN " & _
        ''         " TSPL_SALES_ACCOUNTS AS SA ON G.Account_Code = SA.Cost_Of_Goods_Sold INNER JOIN " & _
        ''         " TSPL_ITEM_MASTER AS IM ON SA.Sales_Class_Code = IM.Sale_Class_Code WHERE IM.Item_Code='" + gvLoadOut.Rows(0).Cells("itemCode").Value + "'"
        ''        strPromoAcc = connectSql.RunScalar(trans, sql).Replace(connectSql.RunScalar(trans, sql).ToString().Substring(connectSql.RunScalar(trans, sql).ToString().Length - 3, 3), locSegCode)
        ''        sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strPromoAcc + "'"
        ''        strPromoAccDesc = connectSql.RunScalar(trans, sql)
        ''        If strPromoAccDesc Is Nothing Or strCogAccDesc = "" Then
        ''            Throw New Exception("Promotional Account not found.")
        ''            Return False
        ''        End If
        ''        obj = Accountsegment.Getaccountcodedesc(strPromoAcc, trans)

        ''        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strPromoAcc), New SqlParameter("@Account_Desc", strPromoAccDesc), New SqlParameter("@Amount", Math.Round(promoCogs, 2, MidpointRounding.ToEven)), New SqlParameter("@Description", Me.txtDesc.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
        ''        lineNo = lineNo + 1
        ''    End If
        ''    sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET Is_Post='Y' WHERE Sale_invoice_No='" + Invoiceno + "'"
        ''    connectSql.RunSqlTransaction(trans, sql)
        ''    sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
        ''    connectSql.RunSqlTransaction(trans, sql)

        ''    sql = "Select SUM(Amount) from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + StrVoucher + "' and Amount <'0'"
        ''    Dim ttlCredit As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, sql))

        ''    sql = "Select SUM(Amount) from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + StrVoucher + "' and Amount >'0'"
        ''    Dim ttlDebit As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, sql))

        ''    ''sql = "Select SUM(Amount) from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + StrVoucher + "'"
        ''    ''''''''If Not clsCommon.myCdbl(connectSql.RunScalar(trans, sql)) = 0 Then
        ''    ''If True Then
        ''    ''    Throw New Exception(GetJounalEntryException(StrVoucher, trans))
        ''    ''End If

        ''End If
        '' ''Saving in Invoice Price Component Table.
        ''Dim objPC As clsInvoicePriceCompHead = clsInvoicePriceCompHead.GetPriceComponentObject(SaleInvoiceNo, trans)
        ''objPC.SaveData(objPC, trans)
        Return True
    End Function

    Private Function GetJounalEntryException(ByVal VoucherNo As String, ByVal trans As SqlTransaction) As String
        Dim sql As String = "Select Account_code,Account_Desc,case when Amount>0 then Amount end as DrAmt,case when Amount<0 then -1*Amount end as CrAmt from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + VoucherNo + "'"
        Dim dtError As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
        Dim msg As String = "Please Check Journal Entry" + Environment.NewLine
        Dim counter As Integer = 1
        Dim TotDrAmt As Double = 0
        Dim TotCrAmt As Double = 0
        For Each dr As DataRow In dtError.Rows
            msg += clsCommon.myCstr(counter) + "   "
            msg += clsCommon.myCstr(dr("Account_code")) + "             "
            msg += clsCommon.myCstr(dr("DrAmt")) + "                  "
            msg += clsCommon.myCstr(dr("CrAmt")) + Environment.NewLine
            TotDrAmt += clsCommon.myCdbl(dr("DrAmt"))
            TotCrAmt += clsCommon.myCdbl(dr("CrAmt"))
            counter += 1
        Next
        msg += "-------------------------------------------------------------------------" + Environment.NewLine
        msg += clsCommon.myCstr("Total") + "             "
        msg += clsCommon.myCstr(TotDrAmt) + "                  "
        msg += clsCommon.myCstr(TotCrAmt) + Environment.NewLine
        msg += "-------------------------------------------------------------------------"

        Return msg
    End Function

    Private Sub MyTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else


        End If
        If e.KeyChar = Chr(46) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txtShipmentTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MyTextBox3.Text = txtShipmentTotal.Text
    End Sub

    Private Sub MyTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CalculateDiscountAmount()


    End Sub

    Private Sub CalculateDiscountAmount()
        Dim discountrate As Decimal = 0
        Dim disamt As Decimal = 0
        Dim dblCustDiscountNoTax As Double = 0
        If String.IsNullOrEmpty(txtShipmentTotal.Text) Then
            txtShipmentTotal.Text = 0
        End If
        Dim pricedate As String = String.Empty
        For Each gro As GridViewRowInfo In gvLoadOut.Rows
            If clsCommon.myCdbl(gro.Cells("shippedqty").Value) <> 0 Then

                pricedate = CDate(gro.Cells("priceDate").Value).ToString("yyyy-MM-dd")
                If (clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
                    disamt = 0
                Else
                    Dim itemnameamt As Decimal = connectSql.RunScalar("select Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(gro.Cells("itemcode").Value) + "' and Item_Basic_Net = '" + Convert.ToString(gro.Cells("mrp").Value) + "' and Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "' and Start_Date = '" + pricedate + "'")
                    disamt = Math.Round(clsCommon.myCdbl(itemnameamt) * discountrate / 100, 2)
                End If

                dblCustDiscountNoTax = clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)
                gro.Cells("discountAmount").Value = disamt
                gro.Cells("totalDiscountAmount").Value = Math.Round((disamt + dblCustDiscountNoTax) * gro.Cells("shippedqty").Value, 2)
                If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells("schemeItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells("sampleItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells("promoSchemeItem").Value), "Yes") = CompairStringResult.Equal)) Then
                    gro.Cells("itemNetAmount").Value = 0
                Else
                    gro.Cells("itemNetAmount").Value = Math.Round(gro.Cells("basicAmount").Value - disamt - dblCustDiscountNoTax, 2)
                End If
                gro.Cells("totalNetAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells("totalBasicAmount").Value - gro.Cells("totalCustDiscount").Value - gro.Cells("totalDiscountAmount").Value), 2)
                Dim disctpt As Decimal = Math.Round(clsCommon.myCdbl(gro.Cells("tpt").Value * gro.Cells("shippedqty").Value) * discountrate / 100, 2)
                loadoutCellValueChange(gro)
                If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells("schemeItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells("sampleItem").Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells("promoSchemeItem").Value), "Yes") = CompairStringResult.Equal)) Then
                    gro.Cells("totalTPT").Value = 0
                Else
                    gro.Cells("totaltpt").Value = clsCommon.myCdbl(gro.Cells("totaltpt").Value) - disctpt
                End If
                gro.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells("totalNetAmount").Value) + clsCommon.myCdbl(gro.Cells("totalTaxAmount").Value) + clsCommon.myCdbl(gro.Cells("totalTPT").Value), 2)
            End If
        Next
        If discountrate = 0 Then
            txtDiscAmt.Text = 0
        ElseIf discountrate = 100 Then
            Dim txtvalue As Decimal = 0
            For Each g As GridViewRowInfo In gvLoadOut.Rows
                If clsCommon.myCdbl(g.Cells("shippedqty").Value) > 0 Then
                    txtShipmentTotal.Text = txtShipmentTotal.Text + clsCommon.myCdbl(g.Cells("basicamount").Value) * clsCommon.myCdbl(g.Cells("shippedqty").Value)
                    txtvalue += clsCommon.myCdbl(g.Cells("totalItemAmount").Value)
                End If
            Next
            txtShipmentAmt.Text = txtTotalTaxAmount.Text
            txtDiscAmt.Text = txtShipmentTotal.Text
            MyTextBox3.Text = 0
        Else
            txtDiscAmt.Text = Math.Round(clsCommon.myCdbl(txtShipmentTotal.Text) * discountrate / 100, 2)
        End If
        Dim totaltpt As Decimal = 0
        For Each tptcount As GridViewRowInfo In gvLoadOut.Rows
            totaltpt = totaltpt + clsCommon.myCdbl(tptcount.Cells("totaltpt").Value)
        Next
    End Sub

    Private Sub txtShipmentAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShipmentAmt.TextChanged
        txtNetShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + clsCommon.myCdbl(MyTextBox4.Text)
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvLoadOut.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvLoadOut.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvLoadOut.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
        End If
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            Exit Sub
        End If
        Dim WhrCls As String = " 2=2"
        Dim qry As String = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code]  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account"
        WhrCls += " and M.status='N'"
        fndCustomer.Value = clsCommon.ShowSelectForm("SRICustFinder", qry, "Code", WhrCls, fndCustomer.Value, "Code", isButtonClicked)
        Dim dtStartTime As DateTime = DateTime.Now
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            qry = "select Customer_Name,Price_Code,price_CodeNon,State from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            If rbtnExcise.IsChecked Then
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            Else
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
            End If

            sql = "select isnull(rm.vehicle_code,'') as [vehicle_code] , rm.Route_No , cm.Salesman_Code, cm.Customer_Name ,rm.Route_Desc ,cm.Price_Code , cm.Tax_Group , cm.Cust_Account , cm.Terms_Code, cm.price_codenon , cm.Credit_Customer    "
            sql += " from TSPL_CUSTOMER_MASTER cm  "
            sql += " left outer join TSPL_ROUTE_MASTER rm on rm.Route_No= cm.Route_No"
            sql += " where cm.Cust_Code = '" + fndCustomer.Value + "' "
            dt = clsDBFuncationality.GetDataTable(sql)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtCustomerName.Text = dt.Rows(0)("Customer_Name").ToString()
                fndRouteNo.txtValue.Text = dt.Rows(0)("Route_No").ToString()
                txtRouteDesc.Text = dt.Rows(0)("Route_Desc").ToString()
                If rbtnExcise.IsChecked Then
                    txtPriceCode.Text = dt.Rows(0)("Price_Code").ToString()
                Else
                    txtPriceCode.Text = dt.Rows(0)("price_codenon").ToString()
                End If
                fndVehicleCode.txtValue.Text = dt.Rows(0)("vehicle_code")

                fndSalesman.txtValue.Text = dt.Rows(0)("Salesman_Code").ToString()
                strCustAccount = dt.Rows(0)("Cust_Account").ToString()
                fndPaymentTerms.txtValue.Text = dt.Rows(0)("Terms_Code").ToString()
                fndTaxGroup.txtValue.Text = GetTaxGroupFromPrice(txtPriceCode.Text)
                txttaxdesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
                rbFC.IsChecked = True

                GroupBox1.Enabled = False
            End If
        End If
        funSetFirstRow()
    End Sub

    Private Function GetTaxGroupFromPrice(ByVal strPriceCode As String) As String
        Dim qry As String = "select top 1 Tax_group  from TSPL_ITEM_PRICE_MASTER where Price_Code='" + strPriceCode + "' order by Start_Date desc"
        Return clsDBFuncationality.getSingleValue(qry)

    End Function

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Try
            Dim qry As String = "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T' then 'Excisable' else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM "
            Dim whrClus As String = "  Location_Type = 'Physical' and  LM.Excisable <> 'T'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClus += " and LM.Location_Code  in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsCommon.ShowSelectForm("SRILoFND", qry, "Code", whrClus, fndLocation.Value, "Code", isButtonClicked)


            sql = "Select Excisable,Sales_Tax_Group from TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "' "
            Dim lDr As DataTable = clsDBFuncationality.GetDataTable(sql)
            If lDr IsNot Nothing AndAlso lDr.Rows.Count > 0 Then
                If Not rbtnExcise.IsChecked Then
                    If gvTaxDetails.Rows.Count > 0 Then
                        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + grow.Cells("taxAuthority").Value + "'"
                            If connectSql.RunScalar(sql) = "Y" Then
                                common.clsCommon.MyMessageBoxShow("Tax group with excisable tax authority is not applicable for given location")
                                fndTaxGroup.txtValue.Text = ""
                                Exit For
                            End If
                        Next
                    End If
                Else
                    If gvTaxDetails.Rows.Count > 0 Then
                        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + grow.Cells("taxAuthority").Value + "'"
                            If connectSql.RunScalar(sql) = "N" Then
                                If grow.Index = gvTaxDetails.RowCount - 1 Then
                                    common.clsCommon.MyMessageBoxShow("There should be atleast one tax authority should be excisable.")
                                    fndTaxGroup.txtValue.Text = ""
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next
                    End If
                End If

            End If
            ''fndTaxGroup.txtValue.Text = Convert.ToString(connectSql.RunScalar("SELECT Sales_Tax_Group  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + fndLocation.Value + "'"))
            ''txttaxdesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLoadOut__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try

            LoadData(txtDocumentNo.Value, NavType)
            funSetFirstRow()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub funSetFirstRow()
        If gvLoadOut.Rows.Count > 0 Then
            gvLoadOut.CurrentRow = gvLoadOut.Rows(0)
        End If
    End Sub

    Private Sub fndLoadOut__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Dim qry As String = "Select Document_No as Code ,Cust_Code as 'Customer Code',Cust_Name as 'Customer Name',CONVERT(varchar(11), Document_Date,103) as [Date],  Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location, case when  Is_Post=0 then  'Pending' else 'Posted' end as [Status] FROM TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location"
        Dim whrClas As String = " 2=2"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_SALE_RETURN_INTER_HEAD.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("ShipINRFND", qry, "Code", whrClas, txtDocumentNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub gvLoadOut_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvLoadOut.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gvLoadOut.Columns("unitCode") Then
                    gvLoadOut.CurrentRow.Cells("unitCode").ReadOnly = True
                ElseIf (e.Column Is gvLoadOut.Columns(colPriceDateActual)) Then
                    gvLoadOut.Columns(colPriceDateActual).FormatString = "{0:d}"
                ElseIf (e.Column Is gvLoadOut.Columns(colLeakQty)) Then
                    If clsCommon.myCdbl(gvLoadOut.CurrentRow.Cells(colEmptyValue).Value) > 0 Then
                        gvLoadOut.Columns(colLeakQty).ReadOnly = False
                    Else
                        gvLoadOut.Columns(colLeakQty).ReadOnly = True
                    End If


                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtShipTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipTo._MYValidating
        If clsCommon.myLen(fndCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Customer", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description,Add1,Add2,Add3,Add4,City_Code as City,State,Country,Telphone,Email,Pin_Code from TSPL_SHIP_TO_LOCATION"
        Dim whrClas As String = "Ship_To_Type_Code='" + fndCustomer.Value + "'"
        txtShipTo.Value = clsCommon.ShowSelectForm("LOCONSIGNEE", qry, "Code", whrClas, txtShipTo.Value, "", isButtonClicked)
        lblShipTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipTo.Value + "'"))
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrintReport(txtDocumentNo.Value, rbtnExcise.IsChecked)
    End Sub

    Public Sub funPrintReport(ByVal strDocNo As String, ByVal isLocationExcisable As Boolean)
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Document No not found to print")
            End If

            Dim whereclause As String
            whereclause = " where TSPL_SALE_RETURN_INTER_HEAD.Document_No = '" + strDocNo + "' "
            Dim strWithoutShall As String = ",'' as totalqty,"
            Dim str As String = ",'' as totalqty,"
            Dim qry As String = ""
            Dim dt As DataTable
            If isLocationExcisable Then
                qry = "select SUM(Qty) as Qty,MAX(Unit_Code) as Unit from("
                qry += " select Qty/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and UOM_Code=TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ) as Qty,(select UOM_Code  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and Conversion_Factor=1)as Unit_Code  from TSPL_SALE_RETURN_INTER_DETAIL where Document_No = '" + strDocNo + "' "
                qry += " ) xxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'" + clsCommon.myCstr(dt.Rows(0)("Unit")) + " - " + clsCommon.myFormat(dt.Rows(0)("Qty")) + " ' as totalqty,"
                End If
            Else
                qry = "select  Unit_code as Unit,sum(qty) as Qty,(select shell_qty from TSPL_SALE_RETURN_INTER_HEAD where Document_No='" + strDocNo + "')as SH from TSPL_SALE_RETURN_INTER_DETAIL where Document_No = '" + strDocNo + "' group by Unit_code"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    str = ",'"
                    For Each dr As DataRow In dt.Rows
                        str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
                    Next
                    strWithoutShall = str + "'" + " as totalqty,"
                    str = str + " SH - " + clsCommon.myCstr(dt.Rows(0)("SH"))
                    str = str + "'" + " as totalqty,"
                End If
            End If


            Dim qryForGettingTax As String = "select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code"
            Dim strIsFOCItem As String = "(CASE WHEN TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item='Y' or TSPL_SALE_RETURN_INTER_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_RETURN_INTER_DETAIL.Sampling_Item='Y' THEN 'Y' ELSE 'N' END) as FOCItem "
            Dim strInvoiceType As String = "(Case when TSPL_SALE_RETURN_INTER_HEAD.Document_Type=0 then 'Excise Sale Return Inter Company' else   'Non-Excise Sale Return Inter Comapny' end) as Invoice_Type"
            Dim strQuery As String = ""
            Dim qryForShipToAdd As String = "(TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3  )"

            If isLocationExcisable Then

                strQuery = " SELECT '' as Cust_PONo,TSPL_CHAPTER_HEAD.Description as ChapterName," + qryForShipToAdd + " AS Address, " & _
                        "  TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as Sale_Invoice_Date, TSPL_SALE_RETURN_INTER_HEAD.Cust_Name, " & _
                        "  (" + qryForGettingTax + "=TSPL_SALE_RETURN_INTER_HEAD.TAX1) as TAX1, TSPL_SALE_RETURN_INTER_HEAD.TAX1_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX1_Assessable_Amt, " & _
                         " TSPL_SALE_RETURN_INTER_HEAD.TAX1_Amt,(" + qryForGettingTax + " = TSPL_SALE_RETURN_INTER_HEAD.TAX2) as TAX2 , TSPL_SALE_RETURN_INTER_HEAD.TAX2_Rate, " & _
                         " TSPL_SALE_RETURN_INTER_HEAD.TAX2_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX2_Amt  as TAX2_Amt, (" + qryForGettingTax + " =TSPL_SALE_RETURN_INTER_HEAD.TAX3) as TAX3, " & _
                         " TSPL_SALE_RETURN_INTER_HEAD.TAX3_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX3_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX3_Amt  as TAX3_Amt, " & _
                         " (" + qryForGettingTax + " =TSPL_SALE_RETURN_INTER_HEAD.TAX4) as TAX4, TSPL_SALE_RETURN_INTER_HEAD.TAX4_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX4_Assessable_Amt, " & _
                         "  TSPL_SALE_RETURN_INTER_HEAD.TAX4_Amt  as TAX4_Amt ,isnull((" + qryForGettingTax + " = TSPL_SALE_RETURN_INTER_HEAD.TAX5),'') as TAX5, TSPL_SALE_RETURN_INTER_HEAD.TAX5_Rate, " & _
                         " TSPL_SALE_RETURN_INTER_HEAD.TAX5_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX5_Amt  as TAX5_Amt ,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt as Total_Invoice_Amt , TSPL_SALE_RETURN_INTER_DETAIL.Qty as Invoice_Qty, " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc as Item_Desc, TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt, " & _
                         "  TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate  as Basic_Rate , TSPL_SALE_RETURN_INTER_DETAIL.Item_Assessable_Rate, TSPL_SALE_RETURN_INTER_DETAIL.Item_Net_Amt, " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass'," & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt AS Dtax1Amt, " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.Total_Assessable_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_MRP_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt  as Total_Basic_Amt , " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Tax_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt, " & _
                         " TSPL_SALE_RETURN_INTER_HEAD.Empty_Value,TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT,TSPL_SALE_RETURN_INTER_HEAD.Total_TPT as 'ttlTPT', " & _
                         " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  " & _
                         " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_RETURN_INTER_HEAD.Remarks  ,TSPL_SALE_RETURN_INTER_HEAD.Description," + strInvoiceType + ",(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then 'FC' else '' end end) ) as [Conversion] ,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and UOM_Code=TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ) as OrgConversionFactor,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName," + strIsFOCItem + "" & _
                         ",  TSPL_SALE_RETURN_INTER_HEAD.Discount_Amt as Inv_Discount_Amt,TSPL_SALE_RETURN_INTER_HEAD.TAX1 as Tax1Code,TSPL_SALE_RETURN_INTER_HEAD.TAX2 as Tax2Code,TSPL_SALE_RETURN_INTER_HEAD.TAX3 as Tax3Code,TSPL_SALE_RETURN_INTER_HEAD.TAX4 as Tax4Code,TSPL_SALE_RETURN_INTER_HEAD.TAX5 as Tax5Code ,(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_RETURN_INTER_HEAD.Created_By) as CreateByName" & _
                         ",(case when TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_RETURN_INTER_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code,TSPL_SALE_RETURN_INTER_HEAD.Route_No,TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, '' as TransferNo," & _
                         "'' as CustomerInvNo ,'' as VerifyByName,TSPL_SALE_RETURN_INTER_DETAIL.Actual_Return_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Leak_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Burst_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Short_Qty  FROM  TSPL_CUSTOMER_MASTER INNER JOIN " & _
                         " TSPL_SALE_RETURN_INTER_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_INTER_HEAD.Cust_Code INNER JOIN " & _
                         " TSPL_SALE_RETURN_INTER_DETAIL ON TSPL_SALE_RETURN_INTER_HEAD.Document_No = TSPL_SALE_RETURN_INTER_DETAIL.Document_No left outer join TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads " & _
                         " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_RETURN_INTER_HEAD.Ship_To "
                strQuery = strQuery & whereclause

                strQuery += " order by TSPL_ITEM_MASTER.Sku_Seq"
                Dim dtBasic As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                Dim dtAfterModify As DataTable = dtBasic.Clone()


                Dim drAfterModify As DataRow
                For Each drBasic As DataRow In dtBasic.Rows
                    drAfterModify = dtAfterModify.NewRow()
                    For ColNo As Integer = 0 To dtBasic.Columns.Count - 1
                        drAfterModify(ColNo) = drBasic(ColNo)
                    Next
                    Dim dblConvFactor As Double = clsCommon.myCdbl(drBasic("OrgConversionFactor"))
                    If dblConvFactor <> 1 Then
                        drAfterModify("Invoice_Qty") = Math.Round(clsCommon.myCdbl(drBasic("Invoice_Qty")) / dblConvFactor, 2, MidpointRounding.ToEven)
                        Dim isFound As Boolean = False
                        For Each drBasicTemp As DataRow In dtBasic.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(drBasicTemp("Item_Code")), clsCommon.myCstr(drBasic("Item_Code"))) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(drBasicTemp("OrgConversionFactor")) = 1 Then
                                drAfterModify("MRP_Amt") = clsCommon.myCdbl(drBasicTemp("MRP_Amt"))
                                drAfterModify("Basic_Rate") = clsCommon.myCdbl(drBasicTemp("Basic_Rate"))
                                isFound = True
                                Exit For
                            End If
                        Next
                        If Not isFound Then
                            drAfterModify("MRP_Amt") = Math.Round(clsCommon.myCdbl(drBasic("MRP_Amt")) * dblConvFactor, 2, MidpointRounding.ToEven)
                            drAfterModify("Basic_Rate") = Math.Round(clsCommon.myCdbl(drBasic("Basic_Rate")) * dblConvFactor, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    dtAfterModify.Rows.Add(drAfterModify)
                Next

                SetItemWiseTax(dtAfterModify, strDocNo)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtAfterModify, EnumTecxpertPaperSize.NA, "crptSaleReturnInterExcise", "Sales Report", True)
            Else
                strQuery = "  SELECT  '' as Cust_PONo," + qryForShipToAdd + " AS Address, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No,  convert(varchar(10),TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as Sale_Invoice_Date, TSPL_SALE_RETURN_INTER_HEAD.Cust_Name, " & _
               " (" + qryForGettingTax + "= TSPL_SALE_RETURN_INTER_HEAD.TAX1) as TAX1, TSPL_SALE_RETURN_INTER_HEAD.TAX1_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX1_Assessable_Amt, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.TAX1_Amt, (" + qryForGettingTax + "=TSPL_SALE_RETURN_INTER_HEAD.TAX2) as TAX2, TSPL_SALE_RETURN_INTER_HEAD.TAX2_Rate, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.TAX2_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX2_Amt,(" + qryForGettingTax + "= TSPL_SALE_RETURN_INTER_HEAD.TAX3) as TAX3, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.TAX3_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX3_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX3_Amt, " & _
               " (" + qryForGettingTax + "= TSPL_SALE_RETURN_INTER_HEAD.TAX4) as TAX4, TSPL_SALE_RETURN_INTER_HEAD.TAX4_Rate, TSPL_SALE_RETURN_INTER_HEAD.TAX4_Assessable_Amt, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.TAX4_Amt, (" + qryForGettingTax + "= TSPL_SALE_RETURN_INTER_HEAD.TAX5) as TAX5, TSPL_SALE_RETURN_INTER_HEAD.TAX5_Rate, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.TAX5_Assessable_Amt, TSPL_SALE_RETURN_INTER_HEAD.TAX5_Amt,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt as Total_Invoice_Amt,TSPL_SALE_RETURN_INTER_HEAD.Vehicle_No,TSPL_SALE_RETURN_INTER_HEAD.KM_Reading , TSPL_SALE_RETURN_INTER_DETAIL.Qty as Invoice_Qty, " & _
               " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc   + ' (' + TSPL_SALE_RETURN_INTER_DETAIL.Unit_Code + ')' as Item_Desc,   TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt/ (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then 'FC' else '' end end))  as [MRP_Amt], " & _
               " TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate, TSPL_SALE_RETURN_INTER_DETAIL.Item_Assessable_Rate, TSPL_SALE_RETURN_INTER_DETAIL.Item_Net_Amt, " & _
               "  TSPL_SALE_RETURN_INTER_DETAIL.TAX1 AS 'DTax1', TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate AS 'DTax1Rate', " & _
               " TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Assessable_Amt AS 'DTax1Ass', '0.00' AS 'Dtax1Amt', " & _
               " TSPL_SALE_RETURN_INTER_DETAIL.Total_Assessable_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_MRP_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt, " & _
               "  TSPL_SALE_RETURN_INTER_DETAIL.Total_net_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Tax_Amt, TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt, " & _
               " TSPL_SALE_RETURN_INTER_HEAD.Empty_Value, TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT,TSPL_SALE_RETURN_INTER_HEAD.Total_TPT as 'ttlTPT', " & _
               " TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  " & _
               " " + str + "TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALE_RETURN_INTER_HEAD.Remarks  ,TSPL_SALE_RETURN_INTER_HEAD.Description," + strInvoiceType + " ,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_SALE_RETURN_INTER_DETAIL.Item_Code and UOM_Code = (case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' then 'FB' else  case when TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB' then 'FC' else '' end end) ) as [Conversion],TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName , " & _
                "" + strIsFOCItem + ",  TSPL_SALE_RETURN_INTER_HEAD.Discount_Amt  as Inv_Discount_Amt,TSPL_SALE_RETURN_INTER_HEAD.TAX1 as Tax1Code,TSPL_SALE_RETURN_INTER_HEAD.TAX2 as Tax2Code,TSPL_SALE_RETURN_INTER_HEAD.TAX3 as Tax3Code,TSPL_SALE_RETURN_INTER_HEAD.TAX4 as Tax4Code,TSPL_SALE_RETURN_INTER_HEAD.TAX5 as Tax5Code" & _
               ",(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALE_RETURN_INTER_HEAD.Created_By) as CreateByName" & _
               ",(case when TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALE_RETURN_INTER_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code,TSPL_SALE_RETURN_INTER_HEAD.Route_No,TSPL_SALE_RETURN_INTER_HEAD.Route_Desc,'' as TransferNo,'' as CustomerInvNo ,'' as VerifyByName,TSPL_SALE_RETURN_INTER_DETAIL.Actual_Return_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Leak_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Burst_Qty,TSPL_SALE_RETURN_INTER_DETAIL.Short_Qty FROM TSPL_CUSTOMER_MASTER INNER JOIN " & _
               "  TSPL_SALE_RETURN_INTER_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_INTER_HEAD.Cust_Code INNER JOIN " & _
               " TSPL_SALE_RETURN_INTER_DETAIL ON TSPL_SALE_RETURN_INTER_HEAD.Document_No = TSPL_SALE_RETURN_INTER_DETAIL.Document_No   left outer join TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code " & _
               " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_RETURN_INTER_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code and TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SALE_RETURN_INTER_HEAD.Ship_To "
                strQuery = strQuery & whereclause

                strQuery += " order by TSPL_ITEM_MASTER.Sku_Seq"
                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                SetItemWiseTax(dtNew, strDocNo)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dtNew, EnumTecxpertPaperSize.NA, "crptSaleReturnInterNonExcise", "Sales Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strShipFrm As String) As DataTable
        dtAfterModify.Columns.Add("TAX1_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX2_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX3_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX4_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX5_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt3", GetType(Double))



        Dim qry As String = "select Tax,Rate,SUM(Amt*Qty) as TaxAmt" + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TAX1_Amt as Amt,Qty" + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX1" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt,Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX2" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt,Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX3" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += "  union all " + Environment.NewLine
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt,Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX4" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt,Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX5" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt,Qty " + Environment.NewLine
        qry += " from TSPL_SALE_RETURN_INTER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_SALE_RETURN_INTER_DETAIL.TAX6" + Environment.NewLine
        qry += " where Document_No='" + strShipFrm + "'  and  TSPL_TAX_MASTER.Type not in ('E')" + Environment.NewLine
        qry += " )xxx " + Environment.NewLine
        qry += " group by Tax,Rate   " + Environment.NewLine

        Dim qryMain As String = qry + " having Tax in( select Tax from(" + qry + ")xxxx group by tax having SUM(1)>1)"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryMain)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + "Code"
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        Else
                            Throw New Exception("Printing Support only 3 Diffent Rates")
                        End If

                    End If
                Next
            Next
        End If

        Return dtAfterModify
    End Function

    Private Sub btnReverseAndRecreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndRecreate.Click
        If clsCommon.myLen(txtDocumentNo.Value) > 0 AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
            If common.clsCommon.MyMessageBoxShow("Reverse and unpost Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim obj As clsSaleReturnInterCompany = clsSaleReturnInterCompany.GetData(txtDocumentNo.Value, NavigatorType.Current, trans)
                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                        Throw New Exception("No Data found to Create Journal entry")
                    End If
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-SR' and Source_Doc_No='" + txtDocumentNo.Value + "'", trans)
                    If clsCommon.myLen(VoucherNo) <= 0 Then
                        Throw New Exception("Journal voucher no not found to recreate journal Enry")
                    End If

                    Dim Qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocumentNo.Value + "' and Trans_Type='Sale Return'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                    Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                    For Each objtr As DataRow In dt.Rows
                        Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                        Dim objLocationDetails As New clsItemLocationDetails()
                        objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                        objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                        objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                        objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                        objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                        objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                        objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                        objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                        ArrLocationDetails.Add(objLocationDetails)
                    Next
                    Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

                    Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocumentNo.Value + "' and Trans_Type='Sale Return'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Qry = "Update TSPL_SALE_RETURN_INTER_HEAD set Is_Post = '0' where Document_No='" + txtDocumentNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                Catch ex As Exception
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(ex.Message)
                End Try
            End If
        End If
    End Sub
End Class
