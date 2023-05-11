' '' '' ''Created By preet Gupta
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class frmCreateReceivedCustomerDairySale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dt As DataTable
    Public strCrateReceived As String = Nothing
    Dim AllowWo_Outstanding As Boolean
    Dim Qry As String
    Public isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private strExcise As Boolean
    Dim isCellValueChangedOpen As Boolean = False
    Const ReportID As String = "CrateItemGrid"
    Const colLineNo As String = "COLLNO"
    Const colInvoiceCode As String = "colInvoiceCode"
    Const colCreateQty As String = "colCreateQty"
    Const colCreateQtyRecd As String = "colCreateQtyRecd"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colRemarks As String = "colRemarks"
    Const colSalesmanCode As String = "colSalesmanCode"
    Const colSalesmanName As String = "colSalesmanName"
    Const colLocationCode As String = "colLocationCode"
    Const colLocationName As String = "colLocationName"
    Const colVehicleCode As String = "colVehicleCode"
    Const colVehicleNo As String = "colVehicleNo"
    '' Anubhooti 10-Sep-2014 BM00000003847 
    Const colCrateBalance As String = "colCrateBalance"
    Const colOutQty As String = "colOutQty"
    Const colAdjustment As String = "colAdjustment"
    ''added by preeti Gupta===
    Const colJaali As String = "colJaali"
    Const colBox As String = "colBox"
    Const colCrateQtyManual As String = "colCrateQtyManual"
    Const colJaaliQtyRecd As String = "colJaaliQtyRecd"
    Const colBoxQtyRecd As String = "colBoxQtyRecd"
    Const colJaaliOutQty As String = "colJaaliOutQty"
    Const colBoxOutQty As String = "colBoxOutQty"
    Const colJaaliAdjustment As String = "colJaaliAdjustment"
    Const colBoxAdjustment As String = "colBoxAdjustment"
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.TransferCrateReceived)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadTypes()
        isInsideLoadData = True
        dt = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "I"
        dr("Name") = "In"
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Code") = "O"
        dr("Name") = "Out"
        dt.Rows.Add(dr)


        'dr = dt.NewRow
        'dr("Code") = "D"
        'dr("Name") = "Doc"
        'dt.Rows.Add(dr)

        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub
    Sub CalculateBalance(ByVal intRow As Integer)
        Try
            Dim dblCrateQty As Double = 0
            Dim dblCrateQtyRecd As Double = 0
            Dim dblCrateBalance As Double = 0
            dblCrateQty = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCreateQty).Value)
            dblCrateQtyRecd = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCreateQtyRecd).Value)
            dblCrateBalance = dblCrateQty - dblCrateQtyRecd
            gv1.Rows(intRow).Cells(colCrateBalance).Value = dblCrateBalance
            txtDate.Focus()
            gv1.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenCustomerFinder(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = " select Cust_Code as [Code],Customer_Name as [Customer Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from tspl_customer_master"
            Dim whrCls As String = "Status='N'"
            gv1.CurrentRow.Cells(colLocationCode).Value = clsCommon.ShowSelectForm("Customerfinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name as Name from tspl_customer_master where Cust_Code ='" + gv1.CurrentRow.Cells(colLocationCode).Value + "' "))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenInvoiceFinder(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells(colLocationCode).Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Customer")
            Else
                Dim qry As String = "select Document_Code as Code,Document_Date,CrateQty from TSPL_SD_SALE_INVOICE_HEAD "
                Dim whrCls As String = " customer_code ='" + gv1.CurrentRow.Cells(colLocationCode).Value + "' and Status=1  and Document_Code not in (select Sale_Invoice_No from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE)"
                gv1.CurrentRow.Cells(colInvoiceCode).Value = clsCommon.ShowSelectForm("Invoicefinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), "Code", isButtonClick)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceCode).Value) > 0 Then
                    gv1.CurrentRow.Cells(colInvoiceDate).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" + gv1.CurrentRow.Cells(colInvoiceCode).Value + "' "))
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenVehcileFinder(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
            Dim whrCls As String = ""
            gv1.CurrentRow.Cells(colVehicleCode).Value = clsCommon.ShowSelectForm("VehicaleFinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVehicleCode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colVehicleNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + gv1.CurrentRow.Cells(colVehicleCode).Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Sub LoadBlankGrid()

        Try


            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "Line No"
            repoLineNo.Name = colLineNo
            repoLineNo.Width = 50
            repoLineNo.ReadOnly = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoLineNo)

            Dim repoCustomer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCustomer.FormatString = ""
            repoCustomer.HeaderText = "Location"
            repoCustomer.Name = colLocationCode
            repoCustomer.HeaderImage = My.Resources.search4
            repoCustomer.TextImageRelation = TextImageRelation.TextBeforeImage

            If isNewEntry Then
                repoCustomer.ReadOnly = False
            Else
                repoCustomer.ReadOnly = True
            End If

            repoCustomer.Width = 100
            gv1.MasterTemplate.Columns.Add(repoCustomer)

            Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCustName.FormatString = ""
            repoCustName.HeaderText = "Location Name"
            repoCustName.Name = colLocationName
            repoCustName.Width = 100

            If isNewEntry Then
                repoCustName.ReadOnly = False
            Else
                repoCustName.ReadOnly = False

            End If

            gv1.MasterTemplate.Columns.Add(repoCustName)

            'Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            'repoICode.FormatString = ""
            'repoICode.HeaderText = "Sale Invoice"
            'repoICode.Name = colInvoiceCode
            'repoCustomer.HeaderImage = Global.ERP.My.Resources.Resources.search4
            'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
            'repoICode.Width = 100
            'gv1.MasterTemplate.Columns.Add(repoICode)


            'Dim repoinvoicedate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            'repoinvoicedate.Format = DateTimePickerFormat.Custom
            'repoinvoicedate.CustomFormat = "dd-mm-yyyy"
            'repoinvoicedate.HeaderText = "sale invoice date"
            'repoinvoicedate.WrapText = True
            'repoinvoicedate.FormatString = "{0:d}"
            'repoinvoicedate.Name = colInvoiceDate
            'repoinvoicedate.ReadOnly = True
            'repoinvoicedate.Width = 80
            'gv1.MasterTemplate.Columns.Add(repoinvoicedate)


            'Dim repoSalesman As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            'repoSalesman.FormatString = ""
            'repoSalesman.HeaderText = "Salesman"
            'repoSalesman.Name = colSalesmanCode
            'repoSalesman.HeaderImage = Global.ERP.My.Resources.Resources.search4
            'repoSalesman.TextImageRelation = TextImageRelation.TextBeforeImage
            'repoSalesman.Width = 100
            'gv1.MasterTemplate.Columns.Add(repoSalesman)

            'Dim repoSalesmanName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            'repoSalesmanName.FormatString = ""
            'repoSalesmanName.HeaderText = "Salesman Name"
            'repoSalesmanName.Name = colSalesmanName
            'repoSalesmanName.Width = 100
            'repoSalesmanName.ReadOnly = True
            'gv1.MasterTemplate.Columns.Add(repoSalesmanName)


            Dim repovehiclecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repovehiclecode.FormatString = ""
            repovehiclecode.HeaderText = "vehicle"
            repovehiclecode.Name = colVehicleCode
            'repovehiclecode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            'repovehiclecode.TextImageRelation = TextImageRelation.TextBeforeImage

            If isNewEntry Then
                repovehiclecode.ReadOnly = False
            Else
                repovehiclecode.ReadOnly = True

            End If


            repovehiclecode.Width = 100
            gv1.MasterTemplate.Columns.Add(repovehiclecode)

            Dim repovehiclename As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repovehiclename.FormatString = ""
            repovehiclename.HeaderText = "vehicle no"
            repovehiclename.Name = colVehicleNo
            repovehiclename.Width = 100

            If isNewEntry Then
                repovehiclename.ReadOnly = True
            Else
                repovehiclename.ReadOnly = True
            End If

            gv1.MasterTemplate.Columns.Add(repovehiclename)

            Dim repoCrateQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCrateQty = New GridViewDecimalColumn()
            repoCrateQty.FormatString = ""
            repoCrateQty.HeaderText = "Crate Qty"
            repoCrateQty.Name = colCreateQty
            repoCrateQty.Width = 80
            repoCrateQty.Minimum = 0
            repoCrateQty.ReadOnly = False
            repoCrateQty.IsVisible = False
            repoCrateQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoCrateQty)



            Dim repoCrateQtyBalance As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCrateQtyBalance = New GridViewDecimalColumn()
            repoCrateQtyBalance.FormatString = ""
            repoCrateQtyBalance.HeaderText = "Balance"
            repoCrateQtyBalance.Name = colCrateBalance
            repoCrateQtyBalance.Width = 80
            repoCrateQtyBalance.Minimum = 0
            repoCrateQtyBalance.ReadOnly = True
            repoCrateQtyBalance.IsVisible = False
            repoCrateQtyBalance.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoCrateQtyBalance)






            '============================Added By preeti Gupta======================
            Dim repoCrateQtyManual As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCrateQtyManual = New GridViewDecimalColumn()
            repoCrateQtyManual.FormatString = ""
            repoCrateQtyManual.HeaderText = "Crate Qty"
            repoCrateQtyManual.Name = colCrateQtyManual
            repoCrateQtyManual.Width = 100
            repoCrateQtyManual.Minimum = 0
            repoCrateQtyManual.ReadOnly = True
            repoCrateQtyManual.IsVisible = False
            repoCrateQtyManual.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoCrateQtyManual)

            Dim repojaali As GridViewDecimalColumn = New GridViewDecimalColumn()
            repojaali = New GridViewDecimalColumn()
            repojaali.FormatString = ""
            repojaali.HeaderText = "Jaali"
            repojaali.Name = colJaali
            repojaali.Width = 80
            repojaali.Minimum = 0
            repojaali.ReadOnly = True
            repojaali.IsVisible = False
            repojaali.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repojaali)

            Dim repoBox As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoBox = New GridViewDecimalColumn()
            repoBox.FormatString = ""
            repoBox.HeaderText = "Box"
            repoBox.Name = colBox
            repoBox.Width = 80
            repoBox.Minimum = 0
            repoBox.ReadOnly = True
            repoBox.IsVisible = False
            repoBox.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoBox)

            Dim repoCrateQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCrateQtyRecd = New GridViewDecimalColumn()
            repoCrateQtyRecd.FormatString = ""
            repoCrateQtyRecd.HeaderText = "Crate Qty Recd."
            repoCrateQtyRecd.Name = colCreateQtyRecd
            repoCrateQtyRecd.Width = 100
            repoCrateQtyRecd.Minimum = 0
            repoCrateQtyRecd.ReadOnly = False
            repoCrateQtyRecd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoCrateQtyRecd)



            Dim repojaaliQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
            repojaaliQtyRecd = New GridViewDecimalColumn()
            repojaaliQtyRecd.FormatString = ""
            repojaaliQtyRecd.HeaderText = "Jaali Qty Recd"
            repojaaliQtyRecd.Name = colJaaliQtyRecd
            repojaaliQtyRecd.Width = 100
            repojaaliQtyRecd.Minimum = 0
            repojaaliQtyRecd.ReadOnly = False
            repojaaliQtyRecd.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repojaaliQtyRecd)

            Dim repoBoxQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoBoxQtyRecd = New GridViewDecimalColumn()
            repoBoxQtyRecd.FormatString = ""
            repoBoxQtyRecd.HeaderText = "Box Qty Recd"
            repoBoxQtyRecd.Name = colBoxQtyRecd
            repoBoxQtyRecd.Width = 100
            repoBoxQtyRecd.Minimum = 0
            repoBoxQtyRecd.ReadOnly = False
            repoBoxQtyRecd.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoBoxQtyRecd)

            Dim repoOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoOutQty = New GridViewDecimalColumn()
            repoOutQty.FormatString = ""
            repoOutQty.HeaderText = "Crate Out Qty"
            repoOutQty.Name = colOutQty
            repoOutQty.Width = 100
            repoOutQty.Minimum = 0
            repoOutQty.ReadOnly = False

            repoOutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoOutQty)

            Dim repojaaliQutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repojaaliQutQty = New GridViewDecimalColumn()
            repojaaliQutQty.FormatString = ""
            repojaaliQutQty.HeaderText = "Jaali Out Qty"
            repojaaliQutQty.Name = colJaaliOutQty
            repojaaliQutQty.Width = 100
            repojaaliQutQty.Minimum = 0
            repojaaliQutQty.ReadOnly = False
            repojaaliQutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repojaaliQutQty)

            Dim repoBoxOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoBoxOutQty = New GridViewDecimalColumn()
            repoBoxOutQty.FormatString = ""
            repoBoxOutQty.HeaderText = "Box Out Qty"
            repoBoxOutQty.Name = colBoxOutQty
            repoBoxOutQty.Width = 100
            repoBoxOutQty.Minimum = 0
            repoBoxOutQty.ReadOnly = False
            repoBoxOutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoBoxOutQty)

            Dim repoAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoAdjustment = New GridViewDecimalColumn()
            repoAdjustment.FormatString = ""
            repoAdjustment.HeaderText = "Crate Adjustment"
            repoAdjustment.Name = colAdjustment
            repoAdjustment.Width = 100
            repoAdjustment.Minimum = 0
            repoAdjustment.ReadOnly = False
            repoAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoAdjustment)

            Dim repojaaliAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
            repojaaliAdjustment = New GridViewDecimalColumn()
            repojaaliAdjustment.FormatString = ""
            repojaaliAdjustment.HeaderText = "Jaali Adjustment"
            repojaaliAdjustment.Name = colJaaliAdjustment
            repojaaliAdjustment.Width = 100
            repojaaliAdjustment.Minimum = 0
            repojaaliAdjustment.ReadOnly = False
            repojaaliAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repojaaliAdjustment)

            Dim repoBoxAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoBoxAdjustment = New GridViewDecimalColumn()
            repoBoxAdjustment.FormatString = ""
            repoBoxAdjustment.HeaderText = "Box Adjustment"
            repoBoxAdjustment.Name = colBoxAdjustment
            repoBoxAdjustment.Width = 100
            repoBoxAdjustment.Minimum = 0
            repoBoxAdjustment.ReadOnly = False
            repoBoxAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
            gv1.MasterTemplate.Columns.Add(repoBoxAdjustment)


            Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRemarks.FormatString = ""
            repoRemarks.HeaderText = "Remarks"
            repoRemarks.Name = colRemarks
            repoRemarks.Width = 100
            gv1.MasterTemplate.Columns.Add(repoRemarks)
            '=======================================================================


            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = True
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub OpenSalesmanIn(ByVal isButtonClick As Boolean)
        Dim strInvoiceCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colInvoiceCode).Value)
        If clsCommon.myLen(strInvoiceCode) > 0 Then
            Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
            Dim whrCls As String = ""
            gv1.CurrentRow.Cells(colSalesmanCode).Value = clsCommon.ShowSelectForm("Salesmanfinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colSalesmanCode).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colSalesmanName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + gv1.CurrentRow.Cells(colSalesmanCode).Value + "' "))
        End If
    End Sub

    Private Sub fndCustomerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustomerNo._MYValidating
        Dim strWhr As String = " cust_code in (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='FS')"
        fndCustomerNo.Value = clsCustomerMaster.getFinder(strWhr, fndCustomerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
        Else
            lblCustomerName.Text = ""
        End If
    End Sub

    Private Function GetCrateQty(ByVal strVehicle As String) As Double
        Dim dblCrateQty As Double = 0
        dblCrateQty = clsDBFuncationality.getSingleValue("SELECT SUM(CrateQty) AS CrateQty FROM ( " & _
        "select Bill_To_Location,Customer_Code,Customer_Name,Vehicle_Code,TSPL_VEHICLE_MASTER.Description as VehicleNo ,CrateQty,CrateComments from " & _
        "TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "left  outer join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
        "where CrateQty > 0  AND " & _
        "Bill_To_Location='" & fndLocation.Value & "' and TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code='" & strVehicle & "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)= '" & clsCommon.GetPrintDate(txtTransDate.Value, "dd/MMM/yyyy") & "'  " & _
        "UNION ALL " & _
        "SELECT Location_Code,Customer_Code,Customer_Name,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as VehicleNo ,-1 * CrateQtyRecd,Remarks FROM " & _
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE ON " & _
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No  LEFT OUTER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "left  outer join TSPL_VEHICLE_MASTER on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
        " where " & _
        "Location_Code='" & fndLocation.Value & "' and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code='" & strVehicle & "'  and convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.invoice_date,103)= '" & clsCommon.GetPrintDate(txtTransDate.Value, "dd/MMM/yyyy") & "'  " & _
        ") FINAL GROUP BY Customer_Code,Vehicle_Code,VehicleNo")
        Return dblCrateQty
    End Function
    Dim locFinderValue As String = Nothing
    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim Qry As String = " SELECT Location_Code [Code] , [Location_Desc] [Loc Detail], type [Type], Loc_Segment_Code [Segment Code] , Location_Type [Loc Type] , GIT_Type [Git Type] ,  City_Code [City Code] , State [State]  FROM TSPL_LOCATION_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code "
            Dim WhrCls As String = " GIT_Type <> 'Y' AND Location_Type = 'Physical' "
            fndLocation.Value = clsCommon.ShowSelectForm("CrateLocFinder", Qry, "Code", WhrCls, "Type", "Code", isButtonClicked)
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
            Else
                lblLocationName.Text = ""
            End If
            Dim lblText As String = Nothing
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                '    Dim qryType As String = "select type FROM TSPL_LOCATION_MASTER where Location_Code = '" + fndLocation.Value + "'"
                '    lblText = clsDBFuncationality.getSingleValue(qryType, Nothing)
                '    locFinderValue = lblText
                '    If lblText = "PLANT" Then
                '        lblLocation.Text = "Plant"
                '        gv1.Columns(colLocationCode).HeaderText = "Branch"
                '        gv1.Columns(colLocationName).HeaderText = "Description"
                '    ElseIf lblText = "Depot" Then
                '        lblLocation.Text = "Branch"
                '        gv1.Columns(colLocationCode).HeaderText = "Plant"
                '        gv1.Columns(colLocationName).HeaderText = "Description"
                '    End If
                'Else
                '    If (ddlType.SelectedValue = "O") Then
                '        lblLocation.Text = "From Location"
                '    Else
                '        lblLocation.Text = "To Location"
                '    End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillGrid()
        LoadBlankGrid()
        Dim intLine As Integer = 0
        Dim strVehicleSaleInvoice As String = ""
        Dim strVehicleCrateInvoice As String = ""
        Dim strVehicleCrateSaleReturn As String = ""
        Dim strLocation As String = ""

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location")
            fndLocation.Focus()
            Exit Sub

        End If
        If clsCommon.myLen(fndVehicle.Value) > 0 Then
            strVehicleSaleInvoice = " and TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code='" & fndVehicle.Value & "' "
            strVehicleCrateInvoice = " and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code='" & fndVehicle.Value & "' "
            strVehicleCrateSaleReturn = " and TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code='" & fndVehicle.Value & "' "
        End If

        Qry = "SELECT Location_Code,MAX(Location_Desc) as  Location_Desc,Vehicle_Code,VehicleNo,SUM(Crate_Out) AS Crate,SUM(jaali_Out) AS jaali,SUM(Box_Out) AS Box FROM ( " & _
            "select 0 as posted, TSPL_TRANSFER_ORDER_HEAD.Document_No ,tspl_location_master.Location_Code,tspl_location_master.Location_Desc   ,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as VehicleNo ,  TSPL_TRANSFER_ORDER_HEAD.Crate_Out ,TSPL_TRANSFER_ORDER_HEAD.jaali_Out  ,TSPL_TRANSFER_ORDER_HEAD.Box_Out  from TSPL_TRANSFER_ORDER_HEAD " & _
            " left  outer join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
            " left join tspl_location_master on tspl_location_master.GIT_Location  =TSPL_TRANSFER_ORDER_HEAD.To_Location  " & _
            " where  (TSPL_TRANSFER_ORDER_HEAD.Crate_Out   > 0 or TSPL_TRANSFER_ORDER_HEAD.jaali_Out >0 or TSPL_TRANSFER_ORDER_HEAD.Box_Out >0 )And TSPL_TRANSFER_ORDER_HEAD.Transfer_Type ='O' and TSPL_TRANSFER_ORDER_HEAD.From_Location ='" & fndLocation.Value & "' " & strVehicleSaleInvoice & "  "
        If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
            Qry += " And convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)='" & clsCommon.GetPrintDate(txtTransDate.Value, "dd/MMM/yyyy") & "' "
        End If
        Qry += " ) FINAL  GROUP BY Location_Code,Vehicle_Code,VehicleNo  having sum(posted)=0 and sum(Crate_Out ) > 0  or sum(jaali_Out ) > 0  or sum(Box_Out ) > 0  "



        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                intLine += 1
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("Location_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleCode).Value = clsCommon.myCstr(dr("Vehicle_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleNo).Value = clsCommon.myCstr(dr("VehicleNo"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("Crate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJaali).Value = clsCommon.myCdbl(dr("jaali"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBox).Value = clsCommon.myCdbl(dr("Box"))

            Next
            'SetIDs()
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub
    'Private Sub SetIDs()
    '    For i As Integer = 0 To gv1.Rows.Count - 1
    '        gv1.Rows(i).Cells("RowsIDs").Value = i + 1.ToString()
    '    Next i

    '    For i As Integer = 0 To gv1.ChildRows.Count - 1
    '        gv1.ChildRows(i).Cells("ChildRowsIDs").Value = i + 1.ToString()
    '    Next i
    'End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            'If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) <= 0 Then
            '    gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
            '    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
            '    gv1.CurrentRow.Cells(colOutQty).Value = 0
            '    gv1.CurrentRow.Cells(colAdjustment).Value = 0
            'ElseIf clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) <= 0 Then
            '    gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
            '    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            'End If



            'If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) <= 0 Then
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
            '    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).Value = 0
            '    gv1.CurrentRow.Cells(colAdjustment).Value = 0
            'ElseIf clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) <= 0 Then
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
            '    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            'End If

            'If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) > 0 Then
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
            '    gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).Value = 0
            '    gv1.CurrentRow.Cells(colOutQty).Value = 0
            'ElseIf clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) <= 0 Then
            '    gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
            '    gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
            'End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) <= 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) <= 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            End If
            If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colJaaliOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxOutQty).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxOutQty).ReadOnly = False
            End If
            If clsCommon.CompairString(ddlType.SelectedValue, "O") = CompairStringResult.Equal Then
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colJaaliAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colJaaliQtyRecd).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliQtyRecd).ReadOnly = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
    '    Try
    '        If Not isInsideLoadData Then
    '            If Not isCellValueChangedOpen Then
    '                isCellValueChangedOpen = True
    '                'If e.Column Is gv1.Columns(colCustCode) Then
    '                '    OpenCustomerFinder(False)
    '                'ElseIf e.Column Is gv1.Columns(colInvoiceCode) Then
    '                '    OpenInvoiceFinder(False)
    '                'ElseIf e.Column Is gv1.Columns(colSalesmanCode) Then
    '                '    OpenSalesmanIn(False)
    '                'ElseIf e.Column Is gv1.Columns(colVehicleCode) Then
    '                '    OpenVehcileFinder(False)
    '                'End If

    '                If e.Column Is gv1.Columns(colCreateQtyRecd) Then
    '                    CalculateBalance(e.RowIndex)

    '                End If


    '            End If

    '            isCellValueChangedOpen = False
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Location")
                fndLocation.Focus()
                Return False
                'ElseIf clsCommon.myLen(fndVehicle.Value) <= 0 Then
                '    common.clsCommon.MyMessageBoxShow("Please select Vehicle")
                '    fndLocation.Focus()
                '    Return False
            End If
            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    Dim dblCrateQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCreateQtyRecd).Value)
            '    Dim strSalesman As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSalesmanCode).Value)
            '    Dim strCustomer As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value)
            '    Dim strInvoice As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colInvoiceCode).Value)
            '    Dim strVehicle As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colVehicleCode).Value)

            '    If dblCrateQty > 0 AndAlso clsCommon.myLen(strCustomer) = 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please enter Customer  At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
            '        Return False
            '    ElseIf dblCrateQty > 0 AndAlso clsCommon.myLen(strInvoice) = 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please enter Invoice no " & strInvoice & " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
            '        Return False
            '    ElseIf dblCrateQty > 0 AndAlso clsCommon.myLen(strSalesman) = 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please enter Salesman for Invoice no " & strInvoice & " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
            '        Return False
            '    ElseIf dblCrateQty > 0 AndAlso clsCommon.myLen(strVehicle) = 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please enter Vehicle for Invoice no " & strInvoice & " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
            '        Return False
            '    End If
            'Next
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Qry = "select count(*) from TSPL_CRATE_RECEIVED_HEAD_Transfer where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
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

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Qry = "SELECT Document_No as Code, ( case when  Type = 'O' then 'Out' when Type= 'I' then 'In' end) As [Transfer Type], CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date, Trans_Date as [Trans Date],Location_Code as Location, (select Location_Desc from TSPL_LOCATION_MASTER where Location_Code = x.Location_Code ) As Description , ( case when  Posted = 0 then 'Pending'  when Posted = 1 then 'Posted' end ) As Status FROM TSPL_CRATE_RECEIVED_HEAD_transfer x"
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            LoadData(clsCommon.ShowSelectForm("Docfnd", Qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try

            Dim blnSave As Boolean = False
            If (AllowToSave()) Then
                Dim obj As New clsTransferCrateReceivedHead
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Trans_Date = txtTransDate.Value
                obj.Location_Code = fndLocation.Value
                obj.Comments = txtComment.Text
                obj.Vehicle_Code = fndVehicle.Value
                obj.Type = ddlType.SelectedValue

                If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                If InOutDocNo.Value IsNot Nothing And InOutDocNo.Value.Length > 0 Then
                    obj.Tagged_DocNo = InOutDocNo.Value
                End If
            End If

            obj.Arr = New List(Of clsTransferCrateReceivedDetail)

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsTransferCrateReceivedDetail()
                    If (clsCommon.myLen(grow.Cells(colLocationCode).Value) > 0) Then
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Branch_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        'objTr.Sale_Invoice_No = clsCommon.myCstr(grow.Cells(colInvoiceCode).Value)
                        objTr.Sale_Invoice_Date = txtDate.Value
                        'objTr.Salesman_Code = clsCommon.myCstr(grow.Cells(colSalesmanCode).Value)
                        'objTr.Salesman_Name = clsCommon.myCstr(grow.Cells(colSalesmanName).Value)
                        objTr.Vehicle_Code = clsCommon.myCstr(grow.Cells(colVehicleCode).Value)
                        objTr.VehicleNo = clsCommon.myCstr(grow.Cells(colVehicleNo).Value)
                        objTr.CrateQty = clsCommon.myCdbl(grow.Cells(colCreateQty).Value)
                        objTr.CrateQtyRecd = clsCommon.myCdbl(grow.Cells(colCreateQtyRecd).Value)
                        objTr.Balance = clsCommon.myCdbl(grow.Cells(colCrateBalance).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.OutQty = clsCommon.myCdbl(grow.Cells(colOutQty).Value)
                        objTr.Adjustment = clsCommon.myCdbl(grow.Cells(colAdjustment).Value)
                        objTr.Jaali = clsCommon.myCdbl(grow.Cells(colJaali).Value)
                        objTr.Box = clsCommon.myCdbl(grow.Cells(colBox).Value)

                        objTr.CrateQtyManual = clsCommon.myCdbl(grow.Cells(colCrateQtyManual).Value)
                        objTr.JaaliQtyRecd = clsCommon.myCdbl(grow.Cells(colJaaliQtyRecd).Value)
                        objTr.BoxQtyRecd = clsCommon.myCdbl(grow.Cells(colBoxQtyRecd).Value)
                        objTr.jaaliAdjustment = clsCommon.myCdbl(grow.Cells(colJaaliAdjustment).Value)
                        objTr.boxAdjustment = clsCommon.myCdbl(grow.Cells(colBoxAdjustment).Value)
                        objTr.jaaliOutQty = clsCommon.myCdbl(grow.Cells(colJaaliOutQty).Value)
                        objTr.boxOutQty = clsCommon.myCdbl(grow.Cells(colBoxOutQty).Value)

                        If (clsCommon.myLen(objTr.CrateQtyRecd) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                Return
            End If

            If (obj.SaveData(obj, isNewEntry)) Then

                If ChekPostBtn = False Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsTransferCrateReceivedHead
            obj = clsTransferCrateReceivedHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                '' Anubhooti 14-Jan-2014 (Disable/Enable Loc after saving)
                fndLocation.Enabled = False
                txtTransDate.Value = obj.Trans_Date
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                fndVehicle.Value = obj.Vehicle_Code
                lblVehicle.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & fndVehicle.Value & "'")
                txtComment.Text = obj.Comments
                ddlType.SelectedValue = obj.Type
                'If clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                '    ddlType.Text = "IN"
                '    ' KUNAL > TASK GIVEN BY PREETI GUPTA WITHOUT TICKET > DATE 4-OCT-2016
                '    lblTransferDate.Visible = True
                '    txtTransDate.Visible = True
                'Else
                '    ddlType.Text = "OUT"
                '    ' KUNAL > TASK GIVEN BY PREETI GUPTA WITHOUT TICKET > DATE 4-OCT-2016
                '    lblTransferDate.Visible = False
                '    txtTransDate.Visible = False
                'End If
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                End If

                Dim LocType As String = Nothing
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsTransferCrateReceivedDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Branch_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc as Name from tspl_location_master where Location_Code ='" + objTr.Branch_Code + "' "))
                        LocType = gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value

                        If (clsCommon.myLen(LocType) > 0) Then
                            gv1.Columns(colLocationCode).HeaderText = "Location"
                            gv1.Columns(colLocationName).HeaderText = "Description"
                        
                        Else
                            gv1.Columns(colLocationCode).HeaderText = "Location"
                            gv1.Columns(colLocationName).HeaderText = "Description"
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleCode).Value = objTr.Vehicle_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleNo).Value = objTr.VehicleNo

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCreateQtyRecd).Value = objTr.CrateQtyRecd
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCreateQty).Value = objTr.CrateQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateBalance).Value = objTr.Balance
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOutQty).Value = objTr.OutQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustment).Value = objTr.Adjustment
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colJaali).Value = objTr.Jaali
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBox).Value = objTr.Box

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateQtyManual).Value = objTr.CrateQtyManual
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBoxQtyRecd).Value = objTr.BoxQtyRecd
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colJaaliQtyRecd).Value = objTr.JaaliQtyRecd
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBoxOutQty).Value = objTr.boxOutQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colJaaliOutQty).Value = objTr.jaaliOutQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBoxAdjustment).Value = objTr.boxAdjustment
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colJaaliAdjustment).Value = objTr.jaaliAdjustment

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        If obj.Posted = 1 Then
                            UsLock1.Status = ERPTransactionStatus.Approved
                        Else
                            UsLock1.Status = ERPTransactionStatus.Pending
                        End If


                    Next
                End If
            Else
                '' Anubhooti 14-Jan-2014 (Disable/Enable Loc after saving)
                fndLocation.Enabled = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub
    Sub AddNew()
        Try

            isNewEntry = True
            LoadBlankGrid()
            btnSave.Text = "Save"
            LoadTypes()
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            fndLocation.Enabled = True
            BlankAllControls()
            InOutDocNo.Value = ""
            ddlType.SelectedValue = "O"
            chkClosingCustomer.Visible = False
            btnGo.Visible = False
            lblTaggedDocs.Visible = False
            UsLock1.Status = ERPTransactionStatus.Pending
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub BlankAllControls()
        Try
            txtDocNo.Value = ""
            fndCustomerNo.Value = ""
            lblCustomerName.Text = ""
            fndLocation.Value = ""
            lblLocationName.Text = ""
            txtDate.Value = clsCommon.GETSERVERDATE
            txtTransDate.Value = clsCommon.GETSERVERDATE
            fndVehicle.Value = ""
            lblVehicle.Text = ""
            txtComment.Text = ""
            gv1.Rows.AddNew()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '' Anubhooti 11-Sep-2014
    Sub print()
        Try
            If clsCommon.myLen(fndCustomerNo.Value) < 0 Then
                Throw New Exception("Please select customer.")
            End If
            If clsCommon.myLen(fndLocation.Value) < 0 Then
                Throw New Exception("Please select location.")
            End If

            Dim strBillCollection As String = String.Empty
            'Dim StrCollection As String
            'Dim strBillsTotal As String
            'Dim strCollectionTotal As String
            'Dim FromDate As String = clsCommon.GetPrintDate("01/" + dtpMonth.Value.Month.ToString() + "/" + dtpMonth.Value.Year.ToString + "", "dd/MMM/yyyy")
            'Dim ToDate As String = clsCommon.GetPrintDate(dtpMonth.Value.Date.AddDays(-(dtpMonth.Value.Day - 1)).AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
            Dim FromDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
            Dim ToDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")

            Dim BaseQry As String = " select Cust_Code as ACode ,(cust_name) as AName, Sale_Invoice_No as DocNo, Sale_Invoice_Date  as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location, Empty_Value as EmptyAmt, Total_Invoice_Amt as Bill, 0 as [Collection] from TSPL_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location  WHERE  TSPL_SALE_INVOICE_HEAD.Is_Post='y'  "
            BaseQry += " UNION ALL"
            BaseQry += " Select Cust_Code as ACode,Customer_Name as AName,Receipt_No as DocNo,Receipt_Date as DocDate, RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location], 0 As EmptyAmt, 0 as Bill, case when Receipt_Type='F' Then Receipt_Amount*-1 Else Receipt_Amount End as [Collection] from TSPL_RECEIPT_HEADER   where  TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Receipt_Type not in ('M')   "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_Customer_Invoice_Head.Customer_Code ,TSPL_Customer_Invoice_Head.Customer_Name ,case when len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 then  TSPL_Customer_Invoice_Head.AgainstScrap else  TSPL_Customer_Invoice_Head.Document_No  end DocNo, CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,TSPL_Customer_Invoice_Head.Loc_Code, 0 as EmptyAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 Else TSPL_Customer_Invoice_Head.Document_Total end As Bill, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end As [Collection] from   TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Status=1 "
            BaseQry += " UNION ALL"
            BaseQry += " select Cust_Code as ACode ,(cust_name) as AName, Sale_Return_No  as DocNo, CONVERT(DATE,Sale_Return_Date,103) as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,Empty_Value as EmptyAmt, Total_Invoice_Amt*-1 as Bill, 0 as [Collection] from TSPL_SALE_RETURN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_HEAD.Location  where TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_SALE_RETURN_INTER_HEAD.Cust_Code as ACode ,(TSPL_SALE_RETURN_INTER_HEAD.cust_name) as AName, TSPL_SALE_RETURN_INTER_HEAD.Document_No  as DocNo, CONVERT(DATE,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)  as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location, TSPL_SALE_RETURN_INTER_HEAD.Empty_Value as  EmptyAmt, TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt*-1 as Bill, 0 as [Collection] from  TSPL_SALE_RETURN_INTER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location  where TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_Receipt_Adjustment_Header.Customer_No as ACode,TSPL_CUSTOMER_MASTER.Customer_Name as AName,Adjustment_No as DocNo, CONVERT(DATE,Adjustment_Date,103) as DocDate, TSPL_LOCATION_MASTER.Loc_Segment_Code as Location ,0 as EmptyAmt, 0 as Bill, TSPL_Receipt_Adjustment_Header.Adjustment_Amount as [Collection] from TSPL_Receipt_Adjustment_Header left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Receipt_Adjustment_Header.Customer_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_Receipt_Adjustment_Header.Doc_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location where TSPL_Receipt_Adjustment_Header.Is_Post='Y' "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_BANK_REVERSE.Cust_Code as ACode, TSPL_BANK_REVERSE.Cust_Name as AName,TSPL_BANK_REVERSE.Reverse_Code as DocNo ,TSPL_BANK_REVERSE.Reversal_Date as DocDate, Right( TSPL_BANK_MASTER.BANKACC, 3) as [Location], 0 as EmptyAmt, 0 as Bill, TSPL_BANK_REVERSE.Amount*-1 as [Collection] from TSPL_BANK_REVERSE  left outer join TSPL_BANK_MASTER on TSPL_BANK_REVERSE .Bank_Code =TSPL_BANK_MASTER.BANK_CODE     where TSPL_BANK_REVERSE.Source_Type='AR' and TSPL_BANK_REVERSE.post='P'   "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo, CONVERT(DATE,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Location_Segment as Location, Case When TSPL_VCGL_Head.Is_Empty=1 Then TSPL_VCGL_Head.Amount Else 0 End as EmptyValue, Case When TSPL_VCGL_Head.Is_Empty<>1 Then (Case When TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount else 0 End) Else 0 End as Bill, Case When TSPL_VCGL_Head.Is_Empty<>1 Then (Case When TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount else 0 End) Else 0 End as [Collection] from  TSPL_VCGL_Head  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 "
            BaseQry += " UNION ALL"
            BaseQry += " select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo, CONVERT(DATE,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Location_Segment as Location , 0 as EmptyAmt, TSPL_VCGL_Detail.Dr_Amount as Bill, TSPL_VCGL_Detail.Cr_Amount as [Collection] from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' "

            Dim Qry As String = " select TSPL_ROUTE_MASTER.Route_No, MAX(TSPL_ROUTE_MASTER.Route_Desc) As Route_Desc, ACode, MAX(AName) As AName, "
            Qry += " SUM(Case When DocDate<'" + FromDate + "' Then Bill-Collection Else 0 End) as OpeningBal, "
            Qry += " " + strBillCollection + " FROM ( " + BaseQry + ""
            Qry += " ) XXX left outer join TSPL_CUSTOMER_MASTER on ACode=TSPL_CUSTOMER_MASTER.Cust_Code"
            Qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No "
            Qry += " Where DocDate<='" + ToDate + "' And LEN(ACode)>0 "


            If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
                Qry += " AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & clsCommon.myCstr(fndCustomerNo.Value) & "'"
            ElseIf clsCommon.myLen(fndLocation.Value) > 0 Then
                Qry += " AND TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" & clsCommon.myCstr(fndLocation.Value) & "'"
            End If
            Qry += " Group By TSPL_ROUTE_MASTER.Route_No, ACode "



            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(Qry)

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dtMain.Rows.Count <= 0 Then
                Throw New Exception("No data found.")
            End If
            gv1.DataSource = dtMain

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''
    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub frmDeliveryNoteFreshSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub frmDeliveryNoteFreshSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
            SetUserMgmtNew()
            AddNew()
            lblLocation.Text = "Location"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            FillGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            'print()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            If gv1.Rows.Count > 0 Then
                arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "  ")

                If clsCommon.myLen(fndCustomerNo.Value) < 0 Then
                    Throw New Exception("Please select customer.")
                End If
                If clsCommon.myLen(fndLocation.Value) < 0 Then
                    Throw New Exception("Please select location.")
                End If
                'arrHeader.Add("Customer Name : " + clsCommon.myCstr(fndCustomerNo.Value))
                Dim BranchName As String = clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + fndLocation.Value + "' ")
                arrHeader.Add("Branch : " + clsCommon.myCstr(BranchName))
                clsCommon.MyExportToExcel("TRANSFER CRATE RECEIVED", gv1, arrHeader, "Crate Received For Transfer")
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsTransferCrateReceivedHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try

            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsTransferCrateReceivedHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    clsCommon.MyMessageBoxShow("Successfully Posted ")
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub gv1_Click(sender As Object, e As EventArgs) Handles gv1.Click
    '    If clsCommon.myLen(gv1.Columns(colCreateQtyRecd)) > 0 Then
    '        gv1.Columns(colOutQty).ReadOnly = True
    '    Else
    '        gv1.Columns(colOutQty).ReadOnly = False
    '    End If

    '    If clsCommon.myLen(gv1.Columns(colOutQty)) > 0 Then
    '        gv1.Columns(colCreateQtyRecd).ReadOnly = True
    '    Else
    '        gv1.Columns(colCreateQtyRecd).ReadOnly = False
    '    End If
    'End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    'Private Sub gv1_FilterChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.FilterChanged
    '    SetIDs()

    'End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        'If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
        '    isCellValueChangedOpen = True
        'If gv1.CurrentColumn Is gv1.Columns(colCustCode) Then
        '    gv1.CurrentColumn = gv1.Columns(colCustName)
        '    OpenCustomerFinder(True)
        '    gv1.CurrentColumn = gv1.Columns(colCustCode)
        'ElseIf gv1.CurrentColumn Is gv1.Columns(colSalesmanCode) Then
        '    gv1.CurrentColumn = gv1.Columns(colSalesmanName)
        '    OpenSalesmanIn(True)
        '    gv1.CurrentColumn = gv1.Columns(colSalesmanCode)
        'ElseIf gv1.CurrentColumn Is gv1.Columns(colInvoiceCode) Then
        '    gv1.CurrentColumn = gv1.Columns(colInvoiceDate)
        '    OpenInvoiceFinder(True)
        '    gv1.CurrentColumn = gv1.Columns(colInvoiceCode)
        'ElseIf gv1.CurrentColumn Is gv1.Columns(colVehicleCode) Then
        '    gv1.CurrentColumn = gv1.Columns(colVehicleNo)
        '    OpenVehcileFinder(True)
        '    gv1.CurrentColumn = gv1.Columns(colVehicleCode)
        'End If
        'End If
    End Sub

    Private Sub fndVehicle__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Qry = "select Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
        fndVehicle.Value = clsCommon.ShowSelectForm("CrateVehicle", Qry, "Code", "", fndVehicle.Value, "Code", isButtonClicked)
        lblVehicle.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & fndVehicle.Value & "'")
    End Sub
    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        Try
            If Not isInsideLoadData Then
                If clsCommon.CompairString(ddlType.SelectedValue, "O") = CompairStringResult.Equal Then
                    EntryType = ddlType.SelectedValue
                    lblTaggedDocs.Visible = False
                    InOutDocNo.Visible = False
                Else
                    EntryType = ddlType.SelectedValue
                    lblTaggedDocs.Visible = True
                    InOutDocNo.Visible = True
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016

    Sub FillGrid_OfOutEntries(ByVal inOutDocumentNo As String)
        LoadBlankGrid()
        Dim intLine As Integer = 0

        Dim whereCondition As String = Nothing
        If inOutDocumentNo IsNot Nothing AndAlso inOutDocumentNo.Length > 0 Then
            whereCondition = " WHERE CHD.Document_No = '" + inOutDocumentNo + "'  AND CHD.Type ='O' "
        Else
            whereCondition = " 1=1"
        End If

        '  Qry = "SELECT  CHD.Document_No , CHD.Location_Code , CHD.Document_Date , CHD.Type , CDT.Branch_Code Branch_Code, LM.Location_Desc, CDT.Vehicle_Code , CDT.VehicleNo , CDT.OutQty  , CDT.jaaliOutQty , CDT.boxOutQty FROM  TSPL_CRATE_RECEIVED_HEAD_transfer CHD  LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER CDT ON CHD.Document_No = CDT.Document_No  LEFT JOIN TSPL_VEHICLE_MASTER VM  ON CDT.Vehicle_Code = VM.Vehicle_Id LEFT JOIN TSPL_LOCATION_MASTER LM ON CDT.Branch_Code = LM.Location_Code " + whereCondition
        Qry = " SELECT CHD.Posted [IsPosted], CHD.Document_No , CHD.Location_Code , (SELECT TYPE FROM TSPL_LOCATION_MASTER WHERE Location_Code = CHD.Location_Code) Location_Type, (SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code = CHD.Location_Code) Location_Type_Desc, CHD.Document_Date , CHD.Type [Transfer_Type] , CDT.Branch_Code Branch_Code ,  (SELECT TYPE FROM TSPL_LOCATION_MASTER WHERE Location_Code = CDT.Branch_Code) Location_Type1 , (SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code = CDT.Branch_Code) Location_Type1_Desc, CDT.Vehicle_Code , CDT.VehicleNo , CDT.OutQty  , CDT.jaaliOutQty , CDT.boxOutQty FROM  TSPL_CRATE_RECEIVED_HEAD_transfer CHD  LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER CDT ON CHD.Document_No = CDT.Document_No  LEFT JOIN TSPL_VEHICLE_MASTER VM  ON CDT.Vehicle_Code = VM.Vehicle_Id " + whereCondition


        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                intLine += 1
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine

                'SELECTION OF VEHICLE ----------------------
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleCode).Value = clsCommon.myCstr(dr("Vehicle_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleNo).Value = clsCommon.myCstr(dr("VehicleNo"))

                'SELECTION OF CARRAIGE UNITS ----------------------
                gv1.Rows(gv1.Rows.Count - 1).Cells(colOutQty).Value = clsCommon.myCdbl(dr("OutQty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJaaliOutQty).Value = clsCommon.myCdbl(dr("jaaliOutQty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBoxOutQty).Value = clsCommon.myCdbl(dr("boxOutQty"))

                ' GRID AND FINDER VALUES
                If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                    fndLocation.Value = clsCommon.myCstr(dr("Branch_Code"))
                    lblLocationName.Text = clsCommon.myCstr(dr("Location_Type1_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("Location_Type_Desc"))
                    'If clsCommon.myCstr(dr("Location_Type")) = "Depot" Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location_Code"))
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("Location_Type_Desc"))
                    '    fndLocation.Value = clsCommon.myCstr(dr("Branch_Code"))
                    '    If clsCommon.myLen(fndLocation.Value) > 0 Then
                    '        lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                    '        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Type from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                    '    Else
                    '        lblLocationName.Text = ""
                    '    End If
                    'ElseIf clsCommon.myCstr(dr("Location_Type1")) = "Depot" Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Branch_Code"))
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("Location_Type1_Desc"))
                    '    fndLocation.Value = clsCommon.myCstr(dr("Location_Code"))
                    '    If clsCommon.myLen(fndLocation.Value) > 0 Then
                    '        lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                    '        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Type from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                    '    Else
                    '        lblLocationName.Text = ""
                    '    End If

                    'End If
                End If

                If clsCommon.myCdbl(dr("IsPosted")) = 1 Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending

                End If
                gv1.Columns(colLocationName).HeaderText = "Description"
                If txtDocNo.Value.Length <= 0 Then
                    isNewEntry = True
                    btnSave.Text = "Save"
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                End If
            Next
           
           

        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
    End Sub

    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016
    Dim EntryType As String = Nothing
    Private Sub InOutDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles InOutDocNo._MYValidating

        Dim qry As String = Nothing
        Dim whrCls As String = Nothing

        Try
            qry = " Select x.* from ( SELECT T.Document_No [Code] , T.Tagged_DocNo, T.Location_Code [Loc Code], T.Document_Date [Date], T.Trans_Date [Trans Date] , T.Posted , T.Comments , T.Vehicle_Code [Veh Code] , Comp_Code [Comp Code] , T.Type [Type] FROM tspl_crate_received_head_transfer T  "
            whrCls = " T.Document_No NOT IN ( SELECT isnull(Tagged_DocNo,'') FROM tspl_crate_received_head_transfer WHERE TYPE = 'I') AND T.Document_No NOT IN ( SELECT Document_No FROM tspl_crate_received_head_transfer WHERE TYPE = 'I') ) x  where x.Posted = 1 "
            InOutDocNo.Value = clsCommon.ShowSelectForm("CrateRcvdInOutFinder", qry, "Code", whrCls, "Code", "Code", isButtonClicked)
            If InOutDocNo.Value IsNot Nothing AndAlso InOutDocNo.Value.Length > 0 Then
                FillGrid_OfOutEntries(InOutDocNo.Value)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016
    Private Sub chkClosingCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkClosingCustomer.ToggleStateChanged
        Try

            If chkClosingCustomer.Checked Then
                InOutDocNo.Visible = True

            Else
                InOutDocNo.Visible = False

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016
    Private Sub BranchInfo_Finder(ByVal intRow As Integer)
        Try
            Dim WhrCls As String = "1=1"
            Dim qry As String = "select Location_Code Code , Location_Desc , Type , Location_Type from TSPL_LOCATION_MASTER"
            Dim locCode As String = clsCommon.ShowSelectForm("CrtRcvBFnd", qry, "Code", WhrCls, "", "Code", True)
            Dim locDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & locCode & "'"))
            isCellValueChanged = True

            gv1.Rows(intRow).Cells(colLocationCode).Value = locCode
            gv1.Rows(intRow).Cells(colLocationName).Value = locDesc
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016
    Private Sub VehicleInfo_Finder(ByVal intRow As Integer, ByVal frmLocFinder As String)
        Try
            Dim WhrCls As String = "Number <> ''"
            Dim qry As String = "select  Vehicle_Id Code , Number Name  from TSPL_VEHICLE_MASTER"

            isCellValueChanged = True
            Dim vehCode As String = Nothing
            Dim vehName As String = Nothing

            'If frmLocFinder.Length > 0 AndAlso cbxVehToGrid.CheckState = CheckState.Checked Then
            '    vehCode = frmLocFinder
            'Else
            '    vehCode = clsCommon.ShowSelectForm("CrateRVehFndr", qry, "Code", WhrCls, "", "Code", True)
            'End If
            vehCode = clsCommon.ShowSelectForm("CrateRVehFndr", qry, "Code", WhrCls, "", "Code", True)
            vehName = clsDBFuncationality.getSingleValue("select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" & vehCode & "'")

            gv1.Rows(intRow).Cells(colVehicleCode).Value = vehCode
            gv1.Rows(intRow).Cells(colVehicleNo).Value = vehName

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Dim isCellValueChanged As Boolean = False
    Dim fromLocationFinder As String = Nothing
    'KUNAL > TICKET : BM00000010096 > DATE : 21 - OCTOBER - 2016
    'Private Sub gv1_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellEndEdit
    '    If fndVehicle.Value IsNot Nothing AndAlso fndVehicle.Value.Length > 0 Then
    '        fromLocationFinder = fndVehicle.Value
    '    End If

    '    Try
    '        If gv1.CurrentRow.Index >= 0 Then
    '            If isCellValueChanged = False Then
    '                If e.Column Is gv1.Columns(colLocationCode) Then
    '                    BranchInfo_Finder(e.RowIndex)

    '                End If
    '            End If
    '            If isCellValueChanged = False Then
    '                If e.Column Is gv1.Columns(colVehicleCode) Then
    '                    VehicleInfo_Finder(e.RowIndex, fromLocationFinder)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        isCellValueChanged = False
    '    End Try
    'End Sub


    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If gv1.CurrentRow.Index >= 0 Then

                If Not isInsideLoadData Then
                    If Not isCellValueChangedOpen Then
                        isCellValueChangedOpen = True
                        If e.Column Is gv1.Columns(colLocationCode) Then
                            OpenICodeCustomer(True)
                        End If
                        If e.Column Is gv1.Columns(colVehicleCode) Then
                            OpenICodeVehicle(True)
                        End If
                    End If

                End If
                isCellValueChangedOpen = False

            End If

        Catch ex As Exception

            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub OpenICodeCustomer(ByVal isButtonClick As Boolean)

        Dim strCode As String = clsLocation.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), False)
        If strCode IsNot Nothing AndAlso clsCommon.myLen(strCode) > 0 Then
            gv1.CurrentRow.Cells(colLocationCode).Value = strCode
            gv1.CurrentRow.Cells(colLocationName).Value = clsLocation.GetName(strCode, Nothing)
        End If
    End Sub
    Sub OpenICodeVehicle(ByVal isButtonClick As Boolean)

        Dim strCode As String = ClsVehicleMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colVehicleCode).Value), False)
        If strCode IsNot Nothing AndAlso clsCommon.myLen(strCode) > 0 Then
            gv1.CurrentRow.Cells(colVehicleCode).Value = strCode
            gv1.CurrentRow.Cells(colVehicleNo).Value = ClsVehicleMaster.GetName(strCode, Nothing)
        End If

    End Sub

End Class


