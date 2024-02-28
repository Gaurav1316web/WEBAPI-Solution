' '' '' ''Created By preet Gupta
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class frmCreateReceivedDairySale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colRouteCode As String = "colRouteCode"
    Const colRouteName As String = "colRouteName"
    Const colVehicleCode As String = "colVehicleCode"
    Const colVehicleNo As String = "colVehicleNo"
    Const colCrateQtyPreviousDay As String = "colCrateQtyPreviousDay"
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
    '=======Added by preeti Gupta================
    Const colCANQty As String = "colCANQty"
    Const colCANRecQty As String = "colCANRecQty"
    Const colCANOutQty As String = "colCANOutQty"
    Const colCANAdjustment As String = "colCANAdjustment"
    Const colDamageCrateQtyRecd As String = "colDamageCrateQtyRecd"
    Private AllowCrateReceiveddairyRouteWise As Boolean = False
    Dim ItemDefaultCanRate As Integer = 0
    Dim ItemDefaultCrateRate As Integer = 0
    Dim ItemDefaultJalliRate As Integer = 0
    Dim ItemDefaultBoxRate As Integer = 0
    Private isInsideLoadMCCAndScrapCustomer As Boolean = False
    Private AllowCrateReceiveddairyCustomerWise As Boolean = False
    Private AllowShowCoumnInCrateReceivedDairy As Boolean = False
    Private CrateReceivingWithMultipleRoute As Boolean = False
    Private CrateReceiveddairyCustomerWise As Boolean = False
    Private AllowOutEntryOnCrateReceivedDairyForAdjustment As Boolean = False
#End Region
    'checkin by sanjay 20200617
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmCrateReceviedDairySale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'Uncomment below lines
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadTypes()
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

        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Name"
    End Sub
    Sub LoadDriver()
        dt = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "H"
        dr("Name") = "Hold"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "R"
        dr("Name") = "Release"
        dt.Rows.Add(dr)

        comDriver.DataSource = dt
        comDriver.ValueMember = "Code"
        comDriver.DisplayMember = "Name"
    End Sub
    Sub LoadSalesMan()
        dt = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "H"
        dr("Name") = "Hold"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "R"
        dr("Name") = "Release"
        dt.Rows.Add(dr)

        comSalesMan.DataSource = dt
        comSalesMan.ValueMember = "Code"
        comSalesMan.DisplayMember = "Name"
    End Sub

    Sub CalculateBalance(ByVal intRow As Integer)
        Try
            Dim dblCrateQty As Double = 0
            Dim dblCrateQtyRecd As Double = 0
            Dim dblCrateQtymanual As Double = 0
            Dim dblCrateBalance As Double = 0
            dblCrateQty = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCreateQty).Value)
            dblCrateQtymanual = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCrateQtyManual).Value)
            dblCrateQtyRecd = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCreateQtyRecd).Value)
            'dblCrateBalance = dblCrateQty - dblCrateQtyRecd
            dblCrateBalance = dblCrateQtymanual - dblCrateQtyRecd
            gv1.Rows(intRow).Cells(colCrateBalance).Value = dblCrateBalance
            txtDate.Focus()
            gv1.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenCustomerFinder(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = " select Cust_Code as [Code],Customer_Name as [Customer Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from tspl_customer_master"
            Dim whrCls As String = "Status='N'"
            gv1.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("Customerfinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colCustName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name as Name from tspl_customer_master where Cust_Code ='" + gv1.CurrentRow.Cells(colCustCode).Value + "' "))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenInvoiceFinder(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCustCode).Value) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
            Else
                Dim qry As String = "select Document_Code as Code,Document_Date,CrateQty from TSPL_SD_SALE_INVOICE_HEAD "
                Dim whrCls As String = " customer_code ='" + gv1.CurrentRow.Cells(colCustCode).Value + "' and Status=1  and Document_Code not in (select Sale_Invoice_No from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE)"
                gv1.CurrentRow.Cells(colInvoiceCode).Value = clsCommon.ShowSelectForm("Invoicefinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), "Code", isButtonClick)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceCode).Value) > 0 Then
                    gv1.CurrentRow.Cells(colInvoiceDate).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" + gv1.CurrentRow.Cells(colInvoiceCode).Value + "' "))
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenVehcileFinder(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
            Dim whrCls As String = ""
            gv1.CurrentRow.Cells(colVehicleCode).Value = clsCommon.ShowSelectForm("VehicaleFinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVehicleCode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colVehicleNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + gv1.CurrentRow.Cells(colVehicleCode).Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub LoadBlankGrid()
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

        If CrateReceivingWithMultipleRoute = True Then
            Dim repoRouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRouteCode.FormatString = ""
            repoRouteCode.HeaderText = "Route Code"
            repoRouteCode.Name = colRouteCode
            repoRouteCode.Width = 100
            gv1.MasterTemplate.Columns.Add(repoRouteCode)

            Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRouteName.FormatString = ""
            repoRouteName.HeaderText = "Route Name"
            repoRouteName.Name = colRouteName
            repoRouteName.Width = 100
            repoRouteName.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoRouteName)

        End If
        Dim repoCustomer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustomer.FormatString = ""
        repoCustomer.HeaderText = "Customer Code"
        repoCustomer.Name = colCustCode
        'repoCustomer.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoCustomer.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustomer.Width = 100
        'repoCustomer.ReadOnly = True
        If CrateReceivingWithMultipleRoute = True Then
            repoCustomer.IsVisible = False
        Else
            repoCustomer.IsVisible = True
        End If
        gv1.MasterTemplate.Columns.Add(repoCustomer)

        Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Customer Name"
        repoCustName.Name = colCustName
        repoCustName.Width = 100
        repoCustName.ReadOnly = True
        If CrateReceivingWithMultipleRoute = True Then
            repoCustomer.IsVisible = False
        Else
            repoCustomer.IsVisible = True
        End If
        gv1.MasterTemplate.Columns.Add(repoCustName)



        Dim repovehiclecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovehiclecode.FormatString = ""
        repovehiclecode.HeaderText = "vehicle Code"
        repovehiclecode.Name = colVehicleCode
        repovehiclecode.HeaderImage = My.Resources.search4
        repovehiclecode.TextImageRelation = TextImageRelation.TextBeforeImage
        repovehiclecode.Width = 100
        'repovehiclecode.ReadOnly = True
        If chkCustomerWise.Checked Then
            repovehiclecode.IsVisible = False
        Else
            repovehiclecode.IsVisible = True
        End If
        gv1.MasterTemplate.Columns.Add(repovehiclecode)

        Dim repovehiclename As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovehiclename.FormatString = ""
        repovehiclename.HeaderText = "vehicle no"
        repovehiclename.Name = colVehicleNo
        repovehiclename.Width = 100
        repovehiclename.ReadOnly = True
        If chkCustomerWise.Checked Then
            repovehiclename.IsVisible = False
        Else
            repovehiclename.IsVisible = True
        End If
        gv1.MasterTemplate.Columns.Add(repovehiclename)

        If CrateReceivingWithMultipleRoute = True Then
            Dim repoCrateQtyPreviousDay As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoCrateQtyPreviousDay = New GridViewDecimalColumn()
            repoCrateQtyPreviousDay.FormatString = "{0:n0}"
            repoCrateQtyPreviousDay.DecimalPlaces = 0
            repoCrateQtyPreviousDay.HeaderText = "Previous Day Crate Qty"
            repoCrateQtyPreviousDay.Name = colCrateQtyPreviousDay
            repoCrateQtyPreviousDay.Width = 80
            repoCrateQtyPreviousDay.Minimum = 0
            repoCrateQtyPreviousDay.ReadOnly = False
            repoCrateQtyPreviousDay.IsVisible = False
            repoCrateQtyPreviousDay.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoCrateQtyPreviousDay)
        End If

        Dim repoCrateQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCrateQty = New GridViewDecimalColumn()
        repoCrateQty.FormatString = "{0:n0}"
        repoCrateQty.DecimalPlaces = 0
        repoCrateQty.HeaderText = "Crate Outstanding Qty"
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




        Dim rowsIDColumn As New GridViewTextBoxColumn()
        rowsIDColumn.HeaderText = "Rows IDs"
        rowsIDColumn.Name = "RowsIDs"
        rowsIDColumn.IsVisible = False
        gv1.Columns.Add(rowsIDColumn)

        Dim childRowsIDColumn As New GridViewTextBoxColumn()
        childRowsIDColumn.HeaderText = "ChildRows IDs"
        childRowsIDColumn.Name = "ChildRowsIDs"
        childRowsIDColumn.IsVisible = False
        gv1.Columns.Add(childRowsIDColumn)

        '============================Added By preeti Gupta against ticket no[ERO/13/06/18-000344,ERO/01/07/19-000661]======================
        Dim repoCrateQtyManual As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCrateQtyManual = New GridViewDecimalColumn()
        repoCrateQtyManual.FormatString = "{0:n0}"
        repoCrateQtyManual.DecimalPlaces = 0
        repoCrateQtyManual.HeaderText = IIf(AllowShowCoumnInCrateReceivedDairy, "Crate Issued Qty", "Crate Qty")
        repoCrateQtyManual.Name = colCrateQtyManual
        repoCrateQtyManual.Width = 100
        repoCrateQtyManual.Minimum = 0
        repoCrateQtyManual.ReadOnly = True
        'repoCrateQtyManual.IsVisible = False
        repoCrateQtyManual.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoCrateQtyManual)

        Dim repoCANQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCANQty = New GridViewDecimalColumn()
        repoCANQty.FormatString = "{0:n0}"
        repoCANQty.DecimalPlaces = 0
        repoCANQty.HeaderText = IIf(AllowShowCoumnInCrateReceivedDairy, "CAN Issued Qty", "CAN Qty")
        repoCANQty.Name = colCANQty
        repoCANQty.Width = 100
        repoCANQty.Minimum = 0
        repoCANQty.ReadOnly = True
        'repoCrateQtyManual.IsVisible = False
        repoCANQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoCANQty)



        Dim repojaali As GridViewDecimalColumn = New GridViewDecimalColumn()
        repojaali = New GridViewDecimalColumn()
        repojaali.FormatString = "{0:n0}"
        repojaali.DecimalPlaces = 0
        repojaali.HeaderText = "Jaali"
        'repojaali.IsVisible = False
        repojaali.Name = colJaali
        repojaali.Width = 80
        repojaali.Minimum = 0
        repojaali.ReadOnly = True
        repojaali.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
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
        repoBox.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoBox.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoBox)

        Dim repoCrateQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCrateQtyRecd = New GridViewDecimalColumn()
        repoCrateQtyRecd.FormatString = "{0:n0}"
        repoCrateQtyRecd.DecimalPlaces = 0
        repoCrateQtyRecd.HeaderText = "Crate Qty Recd."
        repoCrateQtyRecd.Name = colCreateQtyRecd
        repoCrateQtyRecd.Width = 100
        repoCrateQtyRecd.Minimum = 0
        repoCrateQtyRecd.ReadOnly = False
        repoCrateQtyRecd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCrateQtyRecd)


        Dim repoCANRecQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCANRecQty = New GridViewDecimalColumn()
        repoCANRecQty.FormatString = "{0:n0}"
        repoCANRecQty.DecimalPlaces = 0
        repoCANRecQty.HeaderText = "CAN Qty Recd."
        repoCANRecQty.Name = colCANRecQty
        repoCANRecQty.Width = 80
        repoCANRecQty.Minimum = 0
        repoCANRecQty.ReadOnly = False
        repoCANRecQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCANRecQty)


        Dim repojaaliQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
        repojaaliQtyRecd = New GridViewDecimalColumn()
        repojaaliQtyRecd.FormatString = "{0:n0}"
        repojaaliQtyRecd.DecimalPlaces = 0
        repojaaliQtyRecd.HeaderText = "Jaali Qty Recd"
        repojaaliQtyRecd.Name = colJaaliQtyRecd
        repojaaliQtyRecd.Width = 100
        repojaaliQtyRecd.Minimum = 0
        repojaaliQtyRecd.ReadOnly = False
        repojaaliQtyRecd.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repojaaliQtyRecd.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repojaaliQtyRecd)

        Dim repoBoxQtyRecd As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBoxQtyRecd = New GridViewDecimalColumn()
        repoBoxQtyRecd.FormatString = "{0:n0}"
        repoBoxQtyRecd.DecimalPlaces = 0
        repoBoxQtyRecd.HeaderText = "Box Qty Recd"
        repoBoxQtyRecd.Name = colBoxQtyRecd
        repoBoxQtyRecd.Width = 100
        repoBoxQtyRecd.Minimum = 0
        repoBoxQtyRecd.ReadOnly = False
        repoBoxQtyRecd.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoBoxQtyRecd.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoBoxQtyRecd)

        Dim repoOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOutQty = New GridViewDecimalColumn()
        repoOutQty.FormatString = "{0:n0}"
        repoOutQty.DecimalPlaces = 0
        repoOutQty.HeaderText = "Crate Out Qty"
        repoOutQty.Name = colOutQty
        repoOutQty.Width = 100
        repoOutQty.Minimum = 0
        repoOutQty.ReadOnly = False
        repoOutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoOutQty)


        Dim repoCANOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCANOutQty = New GridViewDecimalColumn()
        repoCANOutQty.FormatString = "{0:n0}"
        repoCANOutQty.DecimalPlaces = 0
        repoCANOutQty.HeaderText = "CAN Out Qty"
        repoCANOutQty.Name = colCANOutQty
        repoCANOutQty.Width = 100
        repoCANOutQty.Minimum = 0
        repoCANOutQty.ReadOnly = False

        repoCANOutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoCANOutQty)

        Dim repojaaliQutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repojaaliQutQty = New GridViewDecimalColumn()
        repojaaliQutQty.FormatString = "{0:n0}"
        repojaaliQutQty.DecimalPlaces = 0
        repojaaliQutQty.HeaderText = "Jaali Out Qty"
        repojaaliQutQty.Name = colJaaliOutQty
        repojaaliQutQty.Width = 100
        repojaaliQutQty.Minimum = 0
        repojaaliQutQty.ReadOnly = False
        repojaaliQutQty.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repojaaliQutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repojaaliQutQty)

        Dim repoBoxOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBoxOutQty = New GridViewDecimalColumn()
        repoBoxOutQty.FormatString = "{0:n0}"
        repoBoxOutQty.DecimalPlaces = 0
        repoBoxOutQty.HeaderText = "Box Out Qty"
        repoBoxOutQty.Name = colBoxOutQty
        repoBoxOutQty.Width = 100
        repoBoxOutQty.Minimum = 0
        repoBoxOutQty.ReadOnly = False
        repoBoxOutQty.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoBoxOutQty.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoBoxOutQty)

        Dim repoAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAdjustment = New GridViewDecimalColumn()
        repoAdjustment.FormatString = "{0:n0}"
        repoAdjustment.DecimalPlaces = 0
        repoAdjustment.HeaderText = "Crate Adjustment"
        repoAdjustment.Name = colAdjustment
        repoAdjustment.Width = 100
        repoAdjustment.Minimum = 0
        repoAdjustment.ReadOnly = False
        repoAdjustment.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoAdjustment)

        Dim repoCANAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCANAdjustment = New GridViewDecimalColumn()
        repoCANAdjustment.FormatString = "{0:n0}"
        repoCANAdjustment.DecimalPlaces = 0
        repoCANAdjustment.HeaderText = "CAN Adjustment"
        repoCANAdjustment.Name = colCANAdjustment
        repoCANAdjustment.Width = 100
        repoCANAdjustment.Minimum = 0
        repoCANAdjustment.ReadOnly = False
        repoCANAdjustment.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoCANAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoCANAdjustment)


        Dim repojaaliAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
        repojaaliAdjustment = New GridViewDecimalColumn()
        repojaaliAdjustment.FormatString = "{0:n0}"
        repojaaliAdjustment.DecimalPlaces = 0
        repojaaliAdjustment.HeaderText = "Jaali Adjustment"
        repojaaliAdjustment.Name = colJaaliAdjustment
        repojaaliAdjustment.Width = 100
        repojaaliAdjustment.Minimum = 0
        repojaaliAdjustment.ReadOnly = False
        repojaaliAdjustment.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repojaaliAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repojaaliAdjustment)

        Dim repoBoxAdjustment As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBoxAdjustment = New GridViewDecimalColumn()
        repoBoxAdjustment.FormatString = "{0:n0}"
        repoBoxAdjustment.DecimalPlaces = 0
        repoBoxAdjustment.HeaderText = "Box Adjustment"
        repoBoxAdjustment.Name = colBoxAdjustment
        repoBoxAdjustment.Width = 100
        repoBoxAdjustment.Minimum = 0
        repoBoxAdjustment.ReadOnly = False
        repoBoxAdjustment.IsVisible = IIf(AllowShowCoumnInCrateReceivedDairy, False, True)
        repoBoxAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        gv1.MasterTemplate.Columns.Add(repoBoxAdjustment)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoDamageCrateQtyRecd As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDamageCrateQtyRecd.FormatString = ""
        repoDamageCrateQtyRecd.HeaderText = "Damage Crate Qty"
        repoDamageCrateQtyRecd.Name = colDamageCrateQtyRecd
        repoBoxAdjustment.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        repoDamageCrateQtyRecd.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDamageCrateQtyRecd)
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
        "Bill_To_Location='" & fndLocation.Value & "' and TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code='" & strVehicle & "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)= '" & clsCommon.GetPrintDate(txtInvoiceDate.Value, "dd/MMM/yyyy") & "'  " & _
        "UNION ALL " & _
        "SELECT Location_Code,Customer_Code,Customer_Name,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as VehicleNo ,-1 * CrateQtyRecd,Remarks FROM " & _
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE LEFT OUTER JOIN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE ON " & _
        "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No  LEFT OUTER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "left  outer join TSPL_VEHICLE_MASTER on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
        " where " & _
        "Location_Code='" & fndLocation.Value & "' and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Vehicle_Code='" & strVehicle & "'  and convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.invoice_date,103)= '" & clsCommon.GetPrintDate(txtInvoiceDate.Value, "dd/MMM/yyyy") & "'  " & _
        ") FINAL GROUP BY Customer_Code,Vehicle_Code,VehicleNo")
        Return dblCrateQty
    End Function
    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder(" Location_Type='Physical'", fndLocation.Value, isButtonClicked)
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            lblLocationName.Text = ""
        End If
    End Sub
    '===========ADded by preeti gupta Against Ticket no[BHA/13/08/18-000416,BHA/13/08/18-000415,BHA/14/02/19-000815,BHA/26/06/19-000914,ERO/25/06/19-000656]
    Sub fillClosingGrid()
        Try
            LoadBlankGrid()
            Dim intLine As Integer = 0
            Dim strVehicleSaleInvoice As String = ""
            Dim strVehicleCrateInvoice As String = ""
            Dim strVehicleCrateSaleReturn As String = ""
            Dim strLocation As String = ""
            Dim strRoute As String = Nothing
            Dim variable1 As String = Nothing
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                fndLocation.Focus()
                Exit Sub

            End If

            If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select distinct Customer_Code from (select  Customer_Code  from  TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 and convert(date,Document_Date ,103)<=convert(date,'" + txtDate.Value + "',103) and Route_No ='" & fndRouteNo.Value & "' union all select  Customer_Code  from  tspl_sd_sale_return_head  where tspl_sd_sale_return_head.screen_type='DS' AND tspl_sd_sale_return_head.Status =1 and convert(date,Document_Date ,103)<=convert(date,'" + txtDate.Value + "',103) and Route_No ='" & fndRouteNo.Value & "') as xx")
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt1.Rows.Count - 1
                        If ii <> 0 Then
                            variable1 += ","
                        End If
                        variable1 += "'" + clsCommon.myCstr(dt1.Rows(ii)("Customer_Code")).Trim() + "'"
                    Next
                End If
                If clsCommon.myLen(variable1) > 0 Then
                    strRoute = " and TSPL_CUSTOMER_MASTER.Cust_Code in (" & variable1 & ")"
                Else
                    strRoute = " and TSPL_CUSTOMER_MASTER.Cust_Code in ('')"
                End If
            End If
            '===========================================================================
            Dim QryForCustomerOpening As String = Nothing
            Dim QryForCustomerclosing As String = Nothing
            Dim finalQueryForCustomer As String = Nothing
            Dim qry As String = Nothing




            QryForCustomerOpening = "select "
            If chkCustomerWise.Checked Then
                QryForCustomerOpening += " convert(date,'" + txtDate.Value + "',103) as Doc_Date,"
            Else
                QryForCustomerOpening += "convert(date,'" + txtDate.Value + "',103) as Doc_Date,Opening.Vehicle_Code ,"
            End If

            ''richa BHA/20/06/19-000909 SHOW ONLY POSTED DATA ON REPORT
            QryForCustomerOpening += " Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from " &
                       " (" &
                        " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'   AND TSPL_SD_SHIPMENT_HEAD.Status =1"
            If CrateReceiveddairyCustomerWise = False Then
                QryForCustomerOpening += " union all " &
                        "select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'   AND TSPL_sd_SALE_RETURN_HEAD.Status =1 "
            End If
            QryForCustomerOpening += " union all " &
                        " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," &
                        " TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty," &
                        " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," &
                        " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," &
                        " TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty ," &
                        " 0 as CrateQtyRecd,0 JaaliQtyRecd ," &
                        " 0 BoxQtyRecd ," &
                        " 0 CanQtyRecd ," &
                        " 0 as CrateOutQty," &
                        " 0 jaaliOutQty," &
                        " 0 boxoutqty," &
                         " 0 Canoutqty," &
                        " 0  as CrateAdjQty," &
                        " 0  as JaaliAdjQty," &
                        " 0  as BoxAdjQty," &
                          " 0  as CanAdjQty" &
                          " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" &
                        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " &
                        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " &
                        " union all" &
                        " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," &
                        " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," &
                        " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," &
                        " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," &
                         " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty," &
                        " 0 as CrateQtyRecd,0 JaaliQtyRecd ," &
                        " 0 BoxQtyRecd ," &
                        " 0 CanQtyRecd ," &
                        " 0 as CrateOutQty," &
                        " 0 jaaliOutQty," &
                        " 0 boxoutqty," &
                        " 0 Canoutqty," &
                        " 0  as CrateAdjQty," &
                         " 0  as JaaliAdjQty," &
                        " 0  as BoxAdjQty," &
                          " 0  as CanAdjQty" &
                          " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" &
                        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " &
                        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " &
                        " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + txtDate.Value + "',103))"
            If chkCustomerWise.Checked Then
                QryForCustomerOpening += " group by Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
            Else
                QryForCustomerOpening += " group by Vehicle_Code,Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
            End If




            QryForCustomerclosing = "select Document_Date,"
            If chkCustomerWise.Checked = False Then
                QryForCustomerclosing += " Vehicle_Code,"

            End If
            QryForCustomerclosing += " Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," + Environment.NewLine &
                        " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " + Environment.NewLine &
                         " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine &
                        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine &
                        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  )" &
                        " union all " + Environment.NewLine &
                        " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine &
                        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine &
                        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " &
                        " union all " + Environment.NewLine &
                        " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty," &
                        " 0 as CrateQtyRecd, 0 JaaliQtyRecd , " &
                        " 0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty   " &
                        " from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 "
            If CrateReceiveddairyCustomerWise = False Then
                QryForCustomerclosing += " union all " + Environment.NewLine &
                                     " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN as CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 "
            End If

            QryForCustomerclosing += " ) " &
                        " ) as Closing " + Environment.NewLine &
                        " WHERE convert(date,Document_Date ,103)>=convert(date,'" + txtDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + txtDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'

            finalQueryForCustomer = "select "
            If chkCustomerWise.Checked Then
                finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date"
            Else
                finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date,xx.Vehicle_Code as Vehicle_Code"
            End If


            finalQueryForCustomer += ",xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," &
                        " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," &
                        " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " &
                         " , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing " &
                        " from (" &
                        "" & QryForCustomerOpening & "" &
                        " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
            finalQueryForCustomer += "" & QryForCustomerclosing & "" &
                        "   ) as xx where 2=2  "
            'If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            '    finalQueryForCustomer += " and xx.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            'End If
            'If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            '    finalQueryForCustomer += " and xx.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
            'End If

            If chkCustomerWise.Checked Then
                finalQueryForCustomer += " GROUP BY Customer_Code,convert(date,Doc_Date,103) "
            Else
                finalQueryForCustomer += " GROUP BY Vehicle_Code,Customer_Code,convert(date,Doc_Date,103) "
            End If


            '==========================================END CUSTOMER=========================================================================


            qry = "select  pp.Doc_Date  as Doc_Date,"
            If chkCustomerWise.Checked = False Then
                qry += " pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,"
            End If

            qry += " pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine &
                         " " & finalQueryForCustomer & "" + Environment.NewLine &
                        " ) as pp  "
            qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " + Environment.NewLine &
                    " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No"
            If chkCustomerWise.Checked = False Then
                qry += " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code"
            End If

            qry += " where 2=2 " & strRoute & ""


            Dim qryfinal As String = " With CTETemp as (" &
                       " Select convert(varchar,Doc_Date,103) as Doc_Date,"
            If chkCustomerWise.Checked = False Then
                qryfinal += " Vehicle_Code, Vehicle_Name,"
            End If
            qryfinal += " Customer_Code, Customer_Name, OpencrateQty, OpenJaaliQty, OpenBoxQty, OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd, CanQtyRecd, CrateOutQty, " &
                       " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"
            'If chkCustomerWise.Checked Then
            '    qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CrateQtyClosing, " & _
            '          " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as JaaliQtyClosing, " & _
            '          " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as BoxQtyClosing," & _
            '          " SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code) as CanQtyClosing ," & _
            '          " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code) as RowNo"

            'Else
            qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as CrateQtyClosing, " &
                  " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as JaaliQtyClosing, " &
                  " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as BoxQtyClosing," &
                  " SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as CanQtyClosing ," &
                  " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as RowNo"
            'End If

            If chkCustomerWise.Checked Then
                qryfinal += " from(" + Environment.NewLine &
                      " " & qry & " " &
                      " ) YYY )" &
                      " Select convert(varchar,Doc_Date,103) as Date, ZZZ.Customer_Code as [Customer Code],Customer_Name as [Customer Name], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty," &
                      " jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty," &
                      " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," &
                      " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," &
                      " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing" &
                      ",OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ," &
                       " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," &
                      " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," &
                      " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " &
                      " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing , case when  isnull (XXX.Crate_Qty,0) < 0 then 0 else  isnull (XXX.Crate_Qty,0) end as Crate_Qty  " &
                      " from (Select CTETemp.Doc_Date ,CTETemp.Customer_Code,CTETemp.Customer_Name,  CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " &
                      " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " &
                      " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," &
                      " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " &
                      " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," &
                      " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " &
                      " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code  " &
                      " AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ " &
                      "  Left Outer Join ( select dddd.Customer_Code , dddd.document_Date, sum(isnull (Crate_Qty,0)) as Crate_Qty  from ( select Customer_code, convert (varchar, document_Date,103) as Document_Date, sum (isnull(Crate,0)) as Crate_Qty from " &
                      "  TSPL_SD_SHIPMENT_HEAD  where convert (date, document_Date,103) = convert (date, '" + txtDate.Value + "',103)  and Status = 1 "
                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    qryfinal += " and Route_No = '" + fndRouteNo.Value + "' "
                End If
                qryfinal += "  group by Customer_code, convert (varchar, document_Date,103) "

                qryfinal += " Union All " &
                      " select  TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code ,convert (varchar, document_Date,103) as document_Date, sum (isnull (TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0)) * -1 as Crate_Qty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " &
                      "  where convert (date, document_Date,103) = convert (date, '" + txtDate.Value + "',103) "
                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    qryfinal += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_Code = '" + fndRouteNo.Value + "' "
                End If
                qryfinal += "  group by Customer_code, convert (varchar, document_Date,103) " &
                      " )dddd group by Customer_code, document_Date ) XXX on XXX.Customer_Code = ZZZ.Customer_Code " &
                      " ORDER BY  ZZZ.Customer_Code,convert(date,Doc_Date,103)"

            Else
                qryfinal += " from(" + Environment.NewLine &
                                  " " & qry & " " &
                                  " ) YYY )" &
                                  " Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Customer_Code as [Customer Code],Customer_Name as [Customer Name], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty," &
                                  " jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty," &
                                  " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," &
                                  " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," &
                                  " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing" &
                                  ",OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ," &
                                   " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," &
                                  " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," &
                                  " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " &
                                  " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing " &
                                  " from (Select CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name,  CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " &
                                  " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " &
                                  " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," &
                                  " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " &
                                  " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," &
                                  " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " &
                                  " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code And " &
                                  " CT1.Vehicle_Code = CTETemp.Vehicle_Code " &
                                  " And (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Customer_Code,convert(date,Doc_Date,103),Vehicle_Code"

            End If





            '===========================================================================

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qryfinal)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    intLine += 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("Customer Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer Name"))
                    If chkCustomerWise.Checked = False Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleCode).Value = clsCommon.myCstr(dr("Vehicle Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleNo).Value = clsDBFuncationality.getSingleValue("select Number  from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & clsCommon.myCstr(dr("Vehicle Code")) & "'")
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("CrateQtyClosing"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colJaali).Value = clsCommon.myCdbl(dr("JaaliQtyClosing"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBox).Value = clsCommon.myCdbl(dr("BoxQtyClosing"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCANQty).Value = clsCommon.myCdbl(dr("CanQtyClosing"))
                    If chkCustomerWise.Checked Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCreateQtyRecd).Value = clsCommon.myCdbl(dr("Crate_Qty"))
                    End If

                Next
                SetIDs()
                'Set balance - Get from crate jali report
                Dim Tempfrm As New FrmCrateJaliReport
                Tempfrm.CrateReceivingWithMultipleRoute = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceivingWithMultipleRoute, clsFixedParameterCode.CrateReceivingWithMultipleRoute, Nothing)) = 1, True, False)
                Tempfrm.CrateReceiveddairyCustomerWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceiveddairyCustomerWise, clsFixedParameterCode.CrateReceiveddairyCustomerWise, Nothing)) = 1, True, False)
                Tempfrm.fromDate.Value = txtDate.Value
                Tempfrm.ToDate.Value = txtDate.Value
                Tempfrm.chkCrate.Checked = True
                Tempfrm.chkCustomerWise.Checked = True
                Tempfrm.chkAll.Checked = True
                Tempfrm.btnGo.PerformClick()
                Dim TempDTBal As DataTable = Tempfrm.dt
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim rows As DataRow() = TempDTBal.Select("[Customer Code]='" + clsCommon.myCstr(grow.Cells(colCustCode).Value) + "'")
                    If rows IsNot Nothing AndAlso rows.Length > 0 Then
                        grow.Cells(colCreateQty).Value = rows(0).Item("CrateQtyClosing")
                    End If
                Next
                Tempfrm.Close()
                Tempfrm.Dispose()
                'Set balance - Get from crate jali report
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FillGrid()
        LoadBlankGrid()
        Dim intLine As Integer = 0
        Dim strVehicleSaleInvoice As String = ""
        Dim strVehicleCrateInvoice As String = ""
        Dim strVehicleCrateSaleReturn As String = ""
        Dim strLocation As String = ""
        Dim strSaleInvRoute As String = Nothing
        Dim strSaleReturnRoute As String = Nothing
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            fndLocation.Focus()
            Exit Sub

        End If
        If clsCommon.myLen(fndVehicle.Value) > 0 Then
            strVehicleSaleInvoice = " and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" & fndVehicle.Value & "' "
            strVehicleCrateInvoice = " and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code='" & fndVehicle.Value & "' "
            strVehicleCrateSaleReturn = " and TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code='" & fndVehicle.Value & "' "
        End If
        If clsCommon.myLen(fndRouteNo.Value) > 0 Then
            strSaleInvRoute = " and TSPL_SD_SHIPMENT_HEAD.route_no='" & fndRouteNo.Value & "'"
            strSaleReturnRoute = " and TSPL_SD_SALE_RETURN_HEAD.route_no='" & fndRouteNo.Value & "'"
        End If



        Qry = "SELECT Customer_Code,MAX(Customer_Name) as  Customer_Name,Vehicle_Code,VehicleNo,SUM(Crate) AS Crate,SUM(jaali) AS jaali,SUM(Box) AS Box,sum(shippedCAN) as CAN FROM ( " & _
            "select 0 as posted, TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,Customer_Name,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as VehicleNo ,TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc ,  TSPL_SD_SHIPMENT_HEAD.Crate,TSPL_SD_SHIPMENT_HEAD.jaali ,TSPL_SD_SHIPMENT_HEAD.Box  as Box,TSPL_SD_SHIPMENT_HEAD.shippedCAN  from TSPL_SD_SHIPMENT_HEAD " & _
            "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
            "left  outer join TSPL_VEHICLE_MASTER on TSPL_SD_SHIPMENT_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
              " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_SD_SHIPMENT_HEAD.Route_No " & _
            " where  TSPL_SD_SHIPMENT_HEAD.screen_type='DS' and (TSPL_SD_SHIPMENT_HEAD.Crate  > 0 or TSPL_SD_SHIPMENT_HEAD.jaali>0 or TSPL_SD_SHIPMENT_HEAD.Box>0 ) and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & fndLocation.Value & "' " & strVehicleSaleInvoice & " " & strSaleInvRoute & " "
        If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
            Qry += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' "
        End If
        Qry += " UNION ALL "
        Qry += " select 0 as posted, TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Customer_Code,Customer_Name,TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code, " & _
        "TSPL_VEHICLE_MASTER.Description as VehicleNo  ,TSPL_SD_SALE_RETURN_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc , -1 * TSPL_SD_SALE_RETURN_HEAD.CrateMan  as Crate,-1 * TSPL_SD_SALE_RETURN_HEAD.jaali as Jaali ,-1 * TSPL_SD_SALE_RETURN_HEAD.box as Box,TSPL_SD_SALE_RETURN_HEAD.shippedCAN  from TSPL_SD_SALE_RETURN_HEAD  " & _
       " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SD_SALE_RETURN_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left  outer join " & _
        " TSPL_VEHICLE_MASTER on TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
        " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_SD_SALE_RETURN_HEAD.Route_No " & _
        "where  TSPL_SD_SALE_RETURN_HEAD.screen_type='DS' and (TSPL_SD_SALE_RETURN_HEAD.CrateMan > 0 or TSPL_SD_SALE_RETURN_HEAD.jaali > 0 or TSPL_SD_SALE_RETURN_HEAD.box > 0 )  and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location='" & fndLocation.Value & "' " & strVehicleCrateSaleReturn & " " & strSaleReturnRoute & "  "
        If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
            Qry += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  "
        End If
        Qry += ") FINAL  GROUP BY Customer_Code,Vehicle_Code,VehicleNo  having sum(posted)=0 and sum(Crate ) > 0  or sum(jaali ) > 0  or sum(box ) > 0  "



        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                intLine += 1
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("Customer_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleCode).Value = clsCommon.myCstr(dr("Vehicle_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicleNo).Value = clsCommon.myCstr(dr("VehicleNo"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("Crate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJaali).Value = clsCommon.myCdbl(dr("jaali"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBox).Value = clsCommon.myCdbl(dr("Box"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCANQty).Value = clsCommon.myCdbl(dr("CAN"))
            Next
            SetIDs()
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Private Sub SetIDs()
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(i).Cells("RowsIDs").Value = i + 1.ToString()
        Next i

        For i As Integer = 0 To gv1.ChildRows.Count - 1
            gv1.ChildRows(i).Cells("ChildRowsIDs").Value = i + 1.ToString()
        Next i
    End Sub

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
                gv1.CurrentRow.Cells(colCANQty).ReadOnly = False
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colCreateQtyRecd).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colCANAdjustment).ReadOnly = False
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colOutQty).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colCANRecQty).ReadOnly = True
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                If AllowOutEntryOnCrateReceivedDairyForAdjustment = True Then
                    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
                End If
            End If
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustment).Value) > 0 Then
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colCANRecQty).ReadOnly = True
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
            End If
            If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colJaaliOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxOutQty).ReadOnly = True
                gv1.CurrentRow.Cells(colCANOutQty).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxOutQty).ReadOnly = False
                gv1.CurrentRow.Cells(colCANOutQty).ReadOnly = False
            End If
            If clsCommon.CompairString(ddlType.SelectedValue, "O") = CompairStringResult.Equal Then
                If AllowOutEntryOnCrateReceivedDairyForAdjustment = True Then
                    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(colAdjustment).ReadOnly = True
                End If
                gv1.CurrentRow.Cells(colJaaliAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colCANAdjustment).ReadOnly = True
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colBoxQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colJaaliQtyRecd).ReadOnly = True
                gv1.CurrentRow.Cells(colCANRecQty).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colCANAdjustment).ReadOnly = False
                gv1.CurrentRow.Cells(colCreateQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colBoxQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colJaaliQtyRecd).ReadOnly = False
                gv1.CurrentRow.Cells(colCANRecQty).ReadOnly = False
            End If
            If chkMCCAndScrap.Checked Then
                gv1.CurrentRow.Cells(colCustCode).ReadOnly = False
                gv1.CurrentRow.Cells(colVehicleCode).ReadOnly = False
            Else
                gv1.CurrentRow.Cells(colCustCode).ReadOnly = True
                gv1.CurrentRow.Cells(colVehicleCode).ReadOnly = True


            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If gv1.CurrentRow.Index >= 0 Then
                If Not isInsideLoadMCCAndScrapCustomer Then
                    If Not isInsideLoadData Then
                        If Not isCellValueChangedOpen Then
                            isCellValueChangedOpen = True

                            If e.Column Is gv1.Columns(colCustCode) Then
                                OpenICodeCustomer(True)
                            End If
                            If e.Column Is gv1.Columns(colVehicleCode) Then
                                OpenICodeVehicle(True)
                            End If
                            If CrateReceivingWithMultipleRoute = True Then
                                If e.Column Is gv1.Columns(colRouteCode) Then
                                    OpenRouteCode(True)
                                End If
                            End If
                            If e.Column Is gv1.Columns(colCreateQtyRecd) Then
                                CalculateBalance(e.RowIndex)
                                UpdateAllTotal()
                            ElseIf e.Column Is gv1.Columns(colAdjustment) Then
                                UpdateAllTotal()
                            ElseIf e.Column Is gv1.Columns(colCANRecQty) Then
                                UpdateAllTotal()
                            ElseIf e.Column Is gv1.Columns(colCANAdjustment) Then
                                UpdateAllTotal()
                                '===========added by preeti gupta Against ticket no[]======
                            ElseIf e.Column Is gv1.Columns(colCANOutQty) Then
                                UpdateAllTotal()
                            ElseIf e.Column Is gv1.Columns(colOutQty) Then
                                UpdateAllTotal()
                            ElseIf e.Column Is gv1.Columns(colCustCode) OrElse e.Column Is gv1.Columns(colVehicleCode) Then
                                GetMCCAndScrapClosing(e.RowIndex, True)

                                '==========================================================
                            End If




                        End If
                    End If
                    isCellValueChangedOpen = False

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetMCCAndScrapClosing(ByVal intRow As Integer, ByVal isCellValuedChanged As Boolean)
        If chkMCCAndScrap.Checked Then
            Dim strLocation As String = Nothing
            Dim strCustomer As String = Nothing
            Dim strVehicle As String = Nothing
            Dim dblCrateQty As Double = 0
            Dim strWhrClause As String = ""
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                fndLocation.Focus()
                Exit Sub

            End If

            strLocation = clsCommon.myCstr(fndLocation.Value)

            If isCellValuedChanged Then
                strCustomer = clsCommon.myCstr(gv1.Rows(intRow).Cells(colCustCode).Value)
                strVehicle = clsCommon.myCstr(gv1.Rows(intRow).Cells(colVehicleCode).Value)
                strWhrClause = ""
                'strWhrClause += "and Location_Code in ('" + strLocation + "')  "
                strWhrClause += "and Customer_Code in ('" + strCustomer + "')  "

                If clsCommon.myLen(strVehicle) > 0 Then
                    strWhrClause += "and Vehicle_Code in ('" + strVehicle + "')  "
                End If

                QueryforMCCAndScrap(intRow, strWhrClause)
            Else
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colCustCode).Value) > 0 Then
                        strCustomer = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        strVehicle = clsCommon.myCstr(grow.Cells(colVehicleCode).Value)
                        strWhrClause = ""
                        'strWhrClause += "and Location_Code in ('" + strLocation + "')  "
                        strWhrClause += "and Customer_Code in ('" + strCustomer + "')  "

                        If clsCommon.myLen(strVehicle) > 0 Then
                            strWhrClause += "and Vehicle_Code in ('" + strVehicle + "')  "
                        End If
                        QueryforMCCAndScrap(grow.Index, strWhrClause)
                    End If
                Next
            End If


        End If

    End Sub
    Private Sub QueryforMCCAndScrap(ByVal intRow As Integer, ByVal strWhrclause As String)
        Dim QryForCustomerOpening As String = Nothing
        Dim QryForCustomerclosing As String = Nothing
        Dim finalQueryForCustomer As String = Nothing
        Dim qry As String = Nothing

        QryForCustomerOpening = "select "
        QryForCustomerOpening += "convert(date,'" + txtDate.Value + "',103) as Doc_Date,Opening.Vehicle_Code ,"
        QryForCustomerOpening += " Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from " & _
                   " (" & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty ," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 CanQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                     " 0 Canoutqty," & _
                    " 0  as CrateAdjQty," & _
                    " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty," & _
                      " 0  as CanAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' " & _
                    " union all" & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," & _
                     " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 CanQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                    " 0 Canoutqty," & _
                    " 0  as CrateAdjQty," & _
                     " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty," & _
                      " 0  as CanAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' " & _
                    " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + txtDate.Value + "',103))"
        'If chkCustomerWise.Checked Then
        '    QryForCustomerOpening += " group by Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
        'Else
        QryForCustomerOpening += " group by Vehicle_Code,Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
        'End If




        QryForCustomerclosing = "select Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," + Environment.NewLine & _
                    " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " + Environment.NewLine & _
                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I')" & _
                    " union all " + Environment.NewLine & _
                    " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'" & _
                    " ) ) as Closing " + Environment.NewLine & _
                    " WHERE convert(date,Document_Date ,103)>=convert(date,'" + txtDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + txtDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'

        finalQueryForCustomer = "select "
        finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date,xx.Vehicle_Code as Vehicle_Code"

        finalQueryForCustomer += ",xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," & _
                    " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," & _
                    " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " & _
                     " , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing " & _
                    " from (" & _
                    "" & QryForCustomerOpening & "" & _
                    " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
        finalQueryForCustomer += "" & QryForCustomerclosing & "" & _
                    "   ) as xx where 2=2 " & strWhrclause & " "

        finalQueryForCustomer += " GROUP BY Vehicle_Code,Customer_Code,convert(date,Doc_Date,103) "

        '==========================================END CUSTOMER=========================================================================


        qry = "select  pp.Doc_Date  as Doc_Date,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine & _
                     " " & finalQueryForCustomer & "" + Environment.NewLine & _
                    " ) as pp  "
        qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " + Environment.NewLine & _
 " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No" & _
" left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2  "


        Dim qryfinal As String = " With CTETemp as (" & _
                   " Select convert(varchar,Doc_Date,103) as Doc_Date,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name, OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"

        qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as CrateQtyClosing, " & _
              " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as JaaliQtyClosing, " & _
              " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as BoxQtyClosing," & _
              " SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as CanQtyClosing, " & _
        " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code, Doc_Date) as RowNo"


        qryfinal += " from(" + Environment.NewLine & _
                   " " & qry & " " & _
                   " ) YYY )" & _
                   " Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Customer_Code as [Customer Code],Customer_Name as [Customer Name], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty," & _
                   " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," & _
                   " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," & _
                   " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing" & _
                   ",OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ," & _
                    " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," & _
                   " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," & _
                   " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " & _
                   " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing " & _
                   " from (Select CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name,  CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " & _
                   " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " & _
                   " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," & _
                   " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " & _
                   " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," & _
                   " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " & _
                   " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code and " & _
                   " CT1.Vehicle_Code = CTETemp.Vehicle_Code " & _
                   " AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Customer_Code,convert(date,Doc_Date,103),Vehicle_Code"


        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qryfinal)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                'gv1.Rows.AddNew()
                gv1.Rows(intRow).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("CrateQtyClosing"))
                gv1.Rows(intRow).Cells(colJaali).Value = clsCommon.myCdbl(dr("JaaliQtyClosing"))
                gv1.Rows(intRow).Cells(colBox).Value = clsCommon.myCdbl(dr("BoxQtyClosing"))
                gv1.Rows(intRow).Cells(colCANQty).Value = clsCommon.myCdbl(dr("CanQtyClosing"))

            Next
            'SetIDs()
        Else
            gv1.Rows(intRow).Cells(colCrateQtyManual).Value = 0
            gv1.Rows(intRow).Cells(colJaali).Value = 0
            gv1.Rows(intRow).Cells(colBox).Value = 0
            gv1.Rows(intRow).Cells(colCANQty).Value = 0
        End If


        '===========================================================================

    End Sub

    Sub OpenICodeCustomer(ByVal isButtonClick As Boolean)

        Dim strCode As String = clsCustomerMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), False)
        If strCode IsNot Nothing AndAlso clsCommon.myLen(strCode) > 0 Then
            gv1.CurrentRow.Cells(colCustCode).Value = strCode
            gv1.CurrentRow.Cells(colCustName).Value = clsCustomerMaster.GetName(strCode, Nothing)
        End If

    End Sub
    Sub OpenICodeVehicle(ByVal isButtonClick As Boolean)

        Dim strCode As String = ClsVehicleMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colVehicleCode).Value), False)
        If strCode IsNot Nothing AndAlso clsCommon.myLen(strCode) > 0 Then
            gv1.CurrentRow.Cells(colVehicleCode).Value = strCode
            gv1.CurrentRow.Cells(colVehicleNo).Value = ClsVehicleMaster.GetName(strCode, Nothing)
        End If

    End Sub
    Sub OpenRouteCode(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Route_No,Route_Desc AS Description,Type,vehicle_code AS [Vehicle Code] From TSPL_ROUTE_MASTER "
        gv1.CurrentRow.Cells(colRouteCode).Value = clsCommon.ShowSelectForm("DSGridcrateRecRoute", qry, "Route_No", "", gv1.CurrentRow.Cells(colRouteCode).Value, "Route_No", False)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colRouteCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colRouteName).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + gv1.CurrentRow.Cells(colRouteCode).Value + "' ")
        Else
            gv1.CurrentRow.Cells(colRouteName).Value = ""
        End If
        fillRouteWiseGrid(gv1.CurrentRow.Cells(colRouteCode).Value, gv1.CurrentRow.Index, False, "")

    End Sub

    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                fndLocation.Focus()
                Return False
            End If
           
            If AllowCrateReceiveddairyRouteWise OrElse CrateReceivingWithMultipleRoute = True Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim strCustomerCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value)
                    Dim dblCrateOutStandingQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCreateQty).Value)
                    Dim dblCanOutStandingQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANQty).Value)
                    Dim dblCrateRecQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCreateQtyRecd).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAdjustment).Value)
                    Dim dblCanREcQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANRecQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANAdjustment).Value)
                    'Dim strRouteCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRouteCode).Value)
                    If clsCommon.myLen(strCustomerCode) > 0 AndAlso clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                        If dblCrateRecQty > 0 Then
                            If dblCrateRecQty > dblCrateOutStandingQty Then
                                clsCommon.MyMessageBoxShow(Me, "Crate/Can Qty recevied is not greater then Crate/Can out Standing Qty. At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                                Return False
                            End If
                        End If
                        If dblCanREcQty > 0 Then
                            If dblCanREcQty > dblCanOutStandingQty Then
                                clsCommon.MyMessageBoxShow(Me, "Crate/Can Qty recevied is not greater then Crate/Can out Standing Qty. At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                                Return False
                            End If
                        End If

                    End If

                    If CrateReceivingWithMultipleRoute = True Then
                        Dim strRouteCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRouteCode).Value)
                        If clsCommon.myLen(strRouteCode) > 0 AndAlso clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                            If dblCrateRecQty > 0 Then
                                If dblCrateRecQty > dblCrateOutStandingQty Then
                                    clsCommon.MyMessageBoxShow(Me, "Crate/Can Qty recevied is not greater then Crate/Can out Standing Qty. At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                                    Return False
                                End If
                            End If
                            If dblCanREcQty > 0 Then
                                If dblCanREcQty > dblCanOutStandingQty Then
                                    clsCommon.MyMessageBoxShow(Me, "Crate/Can Qty recevied is not greater then Crate/Can out Standing Qty. At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                                    Return False
                                End If
                            End If

                        End If


                        If (clsCommon.myLen(strRouteCode) > 0) Then

                            For jj As Integer = 0 To gv1.Rows.Count - 1
                                If jj = ii Then
                                    Continue For
                                End If
                                Dim strInnerRouteCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colRouteCode).Value)

                                If clsCommon.CompairString(strRouteCode, strInnerRouteCode) = CompairStringResult.Equal Then
                                    Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                    common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                                    Return False
                                End If

                            Next
                        End If

                    End If


                Next
            End If

            UpdateAllTotal()
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Qry = "select count(*) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Qry = "SELECT Document_No as Code, CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date, Invoice_Date as [DO Date],Location_Code as Location,Posted FROM TSPL_CRATE_RECEIVED_HEAD_FRESHSALE "
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            LoadData(clsCommon.ShowSelectForm("Docfnd", Qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Sub UpdateAllTotal()
        Dim dblCrateRecd As Double = 0
        Dim dblCrateAdjust As Double = 0
        Dim dblCrate As Double = 0
        Dim dblTotalCrate As Double = 0
        Dim dblCanRecd As Double = 0
        Dim dblCanAdjust As Double = 0
        Dim dblCan As Double = 0
        Dim dblTotalCan As Double = 0
        '======Added by preeti gupta Against ticket no[BHA/16/08/18-000435]
        Dim dblCanOut As Double = 0
        Dim dblCrateOut As Double = 0
        '=========================================================
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                dblCrateAdjust = 0
                dblCrateRecd = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCreateQtyRecd).Value)
                If clsCommon.CompairString(ddlType.SelectedValue, "I") = CompairStringResult.Equal Then
                    dblCrateAdjust = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAdjustment).Value)
                End If
                dblCrate = dblCrateRecd + dblCrateAdjust
                dblCanRecd = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANRecQty).Value)
                dblCanAdjust = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANAdjustment).Value)
                dblCan = dblCanRecd + dblCanAdjust
                '=================Added by preeti gupta ==================
                dblCanOut = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCANOutQty).Value)
                dblCrateAdjust = 0
                If clsCommon.CompairString(ddlType.SelectedValue, "O") = CompairStringResult.Equal Then
                    dblCrateAdjust = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAdjustment).Value)
                End If
                dblCrateOut = clsCommon.myCdbl(gv1.Rows(ii).Cells(colOutQty).Value) + dblCrateAdjust
                If dblCanOut > 0 OrElse dblCrateOut > 0 Then
                    dblTotalCan += dblCanOut
                    dblTotalCrate += dblCrateOut
                End If
                '=========================================================

                If dblCan > 0 OrElse dblCrate > 0 Then
                    dblTotalCan += dblCan
                    dblTotalCrate += dblCrate
                End If
            Next

            txtCrateQty.Value = dblTotalCrate
            txtCanQty.Value = dblTotalCan


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            'Dim strQuery As String
            Dim blnSave As Boolean = False
            If (AllowToSave()) Then
                Dim obj As New clsCrateReceivedHead

                obj.TotalCrateQty = clsCommon.myCdbl(txtCrateQty.Value)
                obj.TotalCanQty = clsCommon.myCdbl(txtCanQty.Value)
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Invoice_Date = txtInvoiceDate.Value
                obj.Location_Code = fndLocation.Value
                obj.Comments = txtComment.Text
                obj.Vehicle_Code = fndVehicle.Value

                obj.Type = ddlType.SelectedValue

                obj.Route_code = fndRouteNo.Value
                obj.Driver = comDriver.SelectedValue
                obj.SalesMan = comSalesMan.SelectedValue
                If chkMCCAndScrap.Checked Then
                    obj.Closing_Customer = 1
                Else
                    obj.Closing_Customer = 0
                End If
                If rbtnEvng.Checked = True Then
                    obj.ShiftType = "E"
                Else
                    obj.ShiftType = "M"
                End If
                obj.Arr = New List(Of clsCrateReceivedDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsCrateReceivedDetail()
                    If (clsCommon.myCdbl(grow.Cells(colLineNo).Value)) > 0 Then
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
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

                        objTr.CANQty = clsCommon.myCdbl(grow.Cells(colCANQty).Value)
                        objTr.CANRecQty = clsCommon.myCdbl(grow.Cells(colCANRecQty).Value)
                        objTr.CANOutQty = clsCommon.myCdbl(grow.Cells(colCANOutQty).Value)
                        objTr.CANAdjustment = clsCommon.myCdbl(grow.Cells(colCANAdjustment).Value)
                        ''objTr.crat = clsCommon.myCdbl(grow.Cells(colCANAdjustment).Value)
                        If CrateReceivingWithMultipleRoute = True Then
                            objTr.Route_Code = clsCommon.myCstr(grow.Cells(colRouteCode).Value)
                            objTr.CrateQtyPreviousDay = clsCommon.myCstr(grow.Cells(colCrateQtyPreviousDay).Value)
                        End If
                        If clsCommon.CompairString(obj.Type, "O") = CompairStringResult.Equal Then
                            If (clsCommon.myCdbl(objTr.CANOutQty) > 0 OrElse (clsCommon.myCdbl(objTr.OutQty) > 0 OrElse clsCommon.myCdbl(objTr.Adjustment) > 0)) Then
                                obj.Arr.Add(objTr)
                            End If
                        Else
                            ''richa ERO/14/07/21-001449
                            If (clsCommon.myCdbl(objTr.CrateQtyRecd) > 0 OrElse clsCommon.myCdbl(objTr.CANRecQty) > 0 OrElse clsCommon.myCdbl(objTr.CANAdjustment) > 0 OrElse clsCommon.myCdbl(objTr.Adjustment) > 0) Then
                                obj.Arr.Add(objTr)
                            End If
                        End If
                        
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                If (obj.SaveData(obj, isNewEntry)) Then

                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try


            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsCrateReceivedHead
            obj = clsCrateReceivedHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isInsideLoadMCCAndScrapCustomer = True
                isNewEntry = False
                btnSave.Text = "Update"
                '' Anubhooti 14-Jan-2014 (Disable/Enable Loc after saving)
                txtCrateQty.Value = obj.TotalCrateQty
                txtCanQty.Value = obj.TotalCanQty
                fndLocation.Enabled = False
                txtInvoiceDate.Value = obj.Invoice_Date
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                fndVehicle.Value = obj.Vehicle_Code
                lblVehicle.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & fndVehicle.Value & "'")
                txtComment.Text = obj.Comments
                '============Added by preeti gupta against ticket no[BHA/06/08/18-000393]
                fndRouteNo.Value = obj.Route_code
                txtRouteName.Text = obj.Route_Name


                If obj.Closing_Customer = 1 Then
                    txtInvoiceDate.Enabled = False
                    fndRouteNo.Enabled = False
                    btnGo.Enabled = False
                    chkMCCAndScrap.Checked = True

                Else
                    txtInvoiceDate.Enabled = True
                    fndVehicle.Enabled = True
                    fndRouteNo.Enabled = True
                    chkMCCAndScrap.Checked = False

                End If
                If clsCommon.CompairString(obj.Type, "I") = CompairStringResult.Equal Then
                    ddlType.SelectedValue = "I"
                    ' KUNAL > TASK GIVEN BY PREETI GUPTA WITHOUT TICKET > 4 OCT 2016
                    txtInvoiceDate.Visible = True
                    MyLabel2.Visible = True
                Else
                    ddlType.SelectedValue = "O"
                    ' KUNAL > TASK GIVEN BY PREETI GUPTA WITHOUT TICKET > 4 OCT 2016
                    txtInvoiceDate.Visible = False
                    MyLabel2.Visible = False
                End If
                If clsCommon.CompairString(obj.Driver, "H") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Driver, "") = CompairStringResult.Equal Then
                    comDriver.Text = "Hold"
                Else
                    comDriver.Text = "Release"
                End If
                If clsCommon.CompairString(obj.SalesMan, "H") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Driver, "") = CompairStringResult.Equal Then
                    comSalesMan.Text = "Hold"
                Else
                    comSalesMan.Text = "Release"
                End If
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                End If

                If clsCommon.CompairString(obj.ShiftType, "E") = CompairStringResult.Equal Then
                    rbtnEvng.Checked = True
                ElseIf clsCommon.CompairString(obj.ShiftType, "M") = CompairStringResult.Equal Then
                    rbtnMrng.Checked = True
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCrateReceivedDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Customer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name as Name from tspl_customer_master where Cust_Code ='" + objTr.Customer_Code + "' "))
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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCANQty).Value = objTr.CANQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCANRecQty).Value = objTr.CANRecQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCANOutQty).Value = objTr.CANOutQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCANAdjustment).Value = objTr.CANAdjustment

                        If CrateReceivingWithMultipleRoute = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCrateQtyPreviousDay).Value = objTr.CrateQtyPreviousDay
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value = objTr.Route_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + objTr.Route_Code + "' "))

                        End If

                    Next
                End If

                If obj.Posted = 0 AndAlso CrateReceivingWithMultipleRoute Then
                    FillGridRouteWithQty()
                End If

            Else
                '' Anubhooti 14-Jan-2014 (Disable/Enable Loc after saving)
                fndLocation.Enabled = True
            End If

            ReStoreGridLayout()


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            isInsideLoadMCCAndScrapCustomer = False
        End Try

    End Sub
    Sub AddNew()
        LoadBlankGrid()
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
        LoadTypes()
        LoadDriver()
        LoadSalesMan()
        DisableEnableControls()
        'btnGo.Visible = False

        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        '' Anubhooti 14-Jan-2014 (Disable/Enable Loc after saving)
        fndLocation.Enabled = True
        If AllowCrateReceiveddairyRouteWise Then
            fndRouteNo.Visible = True
            txtRouteName.Visible = True
            lblRouteNo.Visible = True
            MyLabel3.Visible = False
            fndVehicle.Visible = False
            lblVehicle.Visible = False
            chkMCCAndScrap.Visible = True

        Else
            fndRouteNo.Visible = False
            txtRouteName.Visible = False
            lblRouteNo.Visible = False
            MyLabel3.Visible = True
            fndVehicle.Visible = True
            lblVehicle.Visible = True
            chkMCCAndScrap.Visible = False
        End If
        chkMCCAndScrap.Checked = False
        If AllowCrateReceiveddairyCustomerWise Then
            chkCustomerWise.Checked = True
        Else
            chkCustomerWise.Checked = False
        End If
        ReStoreGridLayout()
        If CrateReceivingWithMultipleRoute Then
            'btnGo.Enabled = False
            fndVehicle.Enabled = False
        Else
            btnGo.Enabled = True
            fndVehicle.Enabled = True
        End If
    End Sub
    Sub BlankAllControls()
        txtCanQty.Value = 0
        txtCrateQty.Value = 0
        txtDocNo.Value = ""
        fndCustomerNo.Value = ""
        lblCustomerName.Text = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        txtInvoiceDate.Value = clsCommon.GETSERVERDATE
        fndVehicle.Value = ""
        lblVehicle.Text = ""
        txtComment.Text = ""
        fndRouteNo.Value = ""
        txtRouteName.Text = ""
        gv1.Rows.AddNew()
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
            'Add Tool tip Task No- TEC/18/05/18-000237
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnDeleteInvoiceafterPost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                             "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE " + Environment.NewLine +
                                             "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE ")
                'Add Tool tip Task No- TEC/18/05/18-000237
            End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub frmDeliveryNoteFreshSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
        'Added by preeti Gupta for Client BHARAT against ticket no[]==========
        AllowCrateReceiveddairyRouteWise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CrateReceiveddairyRouteWise & "'")) = 0, False, True)
        AllowCrateReceiveddairyCustomerWise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CrateReceiveddairyCustomerWise & "'")) = 0, False, True)
        ItemDefaultCanRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
        ItemDefaultCrateRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, Nothing))
        ItemDefaultJalliRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemJaaliRate, clsFixedParameterCode.ItemJaaliRate, Nothing))
        ItemDefaultBoxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemBoxRate, clsFixedParameterCode.ItemBoxRate, Nothing))
        AllowShowCoumnInCrateReceivedDairy = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowShowCoumnInCrateReceivedDairy & "'")) = 0, False, True)
        CrateReceivingWithMultipleRoute = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceivingWithMultipleRoute, clsFixedParameterCode.CrateReceivingWithMultipleRoute, Nothing)) = 1, True, False)
        CrateReceiveddairyCustomerWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceiveddairyCustomerWise, clsFixedParameterCode.CrateReceiveddairyCustomerWise, Nothing)) = 1, True, False)
        AllowOutEntryOnCrateReceivedDairyForAdjustment = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOutEntryOnCrateReceivedDairyForAdjustment, clsFixedParameterCode.AllowOutEntryOnCrateReceivedDairyForAdjustment, Nothing)) = 1, True, False)
        '=================================================================================================
        SetUserMgmtNew()

        AddNew()
        If clsCommon.myLen(strCrateReceived) > 0 Then
            LoadData(strCrateReceived, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
         Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_No", "Varchar(30) not null References TSPL_CRATE_RECEIVED_HEAD_FRESHSALE(Document_No)")
        coll.Add("Line_No", "integer not null default 0")
        coll.Add("Customer_Code", "VARCHAR(12) NULL")
        coll.Add("Sale_Invoice_No", "Varchar(30) null References TSPL_SD_SALE_INVOICE_HEAD(DOCUMENT_CODE)")
        coll.Add("Sale_Invoice_Date", "datetime not NULL")
        coll.Add("Salesman_Code", "Varchar(12) null References TSPL_EMPLOYEE_MASTER(EMP_CODE)")
        coll.Add("Salesman_Name", "varchar(30) NULL")
        coll.Add("Vehicle_Code", "varchar(12) NULL")
        coll.Add("VehicleNo", "Varchar(50) NULL")
        coll.Add("CrateQty", "decimal(18, 2) NULL")
        coll.Add("CrateQtyRecd", "decimal(18, 2) NULL")
        coll.Add("Balance", "decimal(18, 2) NULL")
        coll.Add("Remarks", "varchar(500) null")
        coll.Add("OutQty", "decimal(18, 2) NULL")
        coll.Add("Adjustment", "decimal(18, 2) NULL")
        coll.Add("jaali", "decimal(18, 2) NULL")
        coll.Add("box", "decimal(18, 2) NULL")
        '=================Preeti========================
        coll.Add("CrateQtyManual", "decimal(18, 2) NULL")
        coll.Add("JaaliQtyRecd", "decimal(18, 2) NULL")
        coll.Add("BoxQtyRecd", "decimal(18, 2) NULL")
        coll.Add("jaaliOutQty", "decimal(18, 2) NULL")
        coll.Add("boxOutQty", "decimal(18, 2) NULL")
        coll.Add("jaaliAdjustment", "decimal(18, 2) NULL")
        coll.Add("boxAdjustment", "decimal(18, 2) NULL")
        coll.Add("LinerQty", "decimal(18, 2) not null default 0")

        coll.Add("CANQty", "decimal(18, 2) NULL")
        coll.Add("CANQtyRec", "decimal(18, 2) NULL")
        coll.Add("CANOutQty", "decimal(18, 2) NULL")
        coll.Add("CANAdjustment", "decimal(18, 2) NULL")
        coll.Add("Route_code", "varchar(12) NULL")
        coll.Add("CrateQtyPreviousDay", "decimal(18, 2) not NULL default 0")
        coll.Add("DamageCrateQtyRecd", "int NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE", coll, Nothing, True, False, "TSPL_CRATE_RECEIVED_HEAD_FRESHSALE", "Document_No", "")

    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    End Sub



    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If CrateReceivingWithMultipleRoute Then
            FillGridRouteWithQty()
        ElseIf AllowCrateReceiveddairyRouteWise Then
            fillClosingGrid()
        Else
            FillGrid()
        End If
        ReStoreGridLayout()
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
                arrHeader.Add("Customer Name : " + clsCommon.myCstr(fndCustomerNo.Value))
                arrHeader.Add("Location : " + clsCommon.myCstr(fndLocation.Value))
                clsCommon.MyExportToExcel("CRATE RECEIVED", gv1, arrHeader, "Crate Received For Fresh Sale")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsCrateReceivedHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsCrateReceivedHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted ", Me.Text)
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        If AllowCrateReceiveddairyRouteWise Then
            If chkMCCAndScrap.Checked Then
                If gv1.RowCount > 0 Then
                    Dim intCurrRow As Integer = gv1.CurrentRow.Index
                    gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                    If intCurrRow = gv1.Rows.Count - 1 Then
                        gv1.Rows.AddNew()
                        gv1.CurrentRow = gv1.Rows(intCurrRow)
                    End If
                End If

            End If
        End If

    End Sub

    Private Sub gv1_FilterChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.FilterChanged
        SetIDs()

    End Sub

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

    Private Sub fndVehicle__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVehicle._MYValidating
        Qry = "select Vehicle_Id as Code,Description from TSPL_VEHICLE_MASTER"
        fndVehicle.Value = clsCommon.ShowSelectForm("CrateVehicle", Qry, "Code", "", fndVehicle.Value, "Code", isButtonClicked)
        lblVehicle.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & fndVehicle.Value & "'")
    End Sub

    Private Sub RadPageViewPage1_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        Try
            If isInsideLoadData Then
                If clsCommon.CompairString(ddlType.SelectedValue, "O") = CompairStringResult.Equal Then
                    txtInvoiceDate.Visible = False
                    MyLabel2.Visible = False
                Else
                    txtInvoiceDate.Visible = True
                    MyLabel2.Visible = True
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub fndRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRouteNo._MYValidating
        Dim qry As String = "Select Route_No,Route_Desc AS Description,Type,vehicle_code AS [Vehicle Code] From TSPL_ROUTE_MASTER "
        fndRouteNo.Value = clsCommon.ShowSelectForm("DScrateRecRoute", qry, "Route_No", "", fndRouteNo.Value, "Route_No", isButtonClicked)
        If clsCommon.myLen(fndRouteNo.Value) > 0 Then
            txtRouteName.Text = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + fndRouteNo.Value + "' ")
        Else
            txtRouteName.Text = ""
        End If
    End Sub

    Private Sub chkMCCAndScrap_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAndScrap.ToggleStateChanged
        If AllowCrateReceiveddairyRouteWise Then
            isInsideLoadMCCAndScrapCustomer = True
            If chkMCCAndScrap.Checked Then
                fndRouteNo.Enabled = False
                btnGo.Enabled = False
            Else
                fndRouteNo.Enabled = True
                btnGo.Enabled = True
            End If
            isInsideLoadMCCAndScrapCustomer = False
        End If

    End Sub

    Private Sub txtDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        If chkMCCAndScrap.Checked Then
            GetMCCAndScrapClosing(0, False)
        End If
    End Sub
    ' Ticket : TEC/29/10/18-000348 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
    '========Added by preeti Gupta Against ticket no[ERO/04/04/19-000544]
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Public Sub DisableEnableControls()
        If AllowShowCoumnInCrateReceivedDairy Then
            lblDriver.Visible = True
            comDriver.Visible = True
            lblSalesMan.Visible = True
            comSalesMan.Visible = True
        Else
            lblDriver.Visible = False
            comDriver.Visible = False
            lblSalesMan.Visible = False
            comSalesMan.Visible = False
        End If
    End Sub
    '' Ticket : TEC/28/06/19-000574 By Prabhakar 
    Private Sub btnDeleteInvoiceafterPost_Click(sender As Object, e As EventArgs) Handles btnDeleteInvoiceafterPost.Click
        ReverseAndUnPost()
    End Sub

    Private Sub ReverseAndUnPost()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (deleteConfirm()) Then
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE where Document_No ='" & txtDocNo.Value & "' ", trans) > 0 Then
                    If (clsCrateReceivedHead.ReverseAndRecrate(txtDocNo.Value, trans)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                        trans.Commit()
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                Else
                    Throw New Exception("Document " & txtDocNo.Value & " can't be deleted,")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String

        str = " select '' as Date, '' as Type,'' as Location,'' as [Vehicle Code],'' as [Customer Code],0 as [Crate Qty],0 as [CAN Qty] "
        transportSql.ExporttoExcel(str, Me)


    End Sub
    Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Counter As Integer = 0


        If transportSql.importExcel(gv, " Date", "Type", "Location", "Vehicle Code", "Customer Code", "Crate Qty", "CAN Qty") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strDate As String = clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MM/yyyy")
                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    Dim strLocation As String = clsCommon.myCstr(grow.Cells("Location").Value)
                    Dim strcustomer As String = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    Dim strVehicle As String = clsCommon.myCstr(grow.Cells("Vehicle Code").Value)
                    Dim dblCrateQty As Double = clsCommon.myCdbl(grow.Cells("Crate Qty").Value)
                    Dim dblCanQty As Double = clsCommon.myCdbl(grow.Cells("Can Qty").Value)
                    Dim strName As String = ""
                    Counter += 1


                    If clsCommon.myLen(Location) <= 0 Then
                        Throw New Exception("Location not found")
                    End If
                    If strLocation.Length > 12 Then
                        Throw New Exception("length of Location can not be greater than 12")
                    Else
                        If strLocation.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_LOCATION_MASTER Where location_Code ='" & strLocation & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Location (" & strLocation & ") does not exist in Location Master . Please make it entry first.")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(strcustomer) <= 0 Then
                        Throw New Exception("Customer not found")
                    End If
                    If strcustomer.Length > 12 Then
                        Throw New Exception("length of Customer can not be greater than 12")
                    Else
                        If strcustomer.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM tspl_Customer_Master Where cust_Code ='" & strcustomer & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Customer (" & strcustomer & ") does not exist in Customer Master . Please make it entry first.")
                            End If
                        End If
                    End If

                    If strVehicle.Length > 12 Then
                        Throw New Exception("length of Vehicle can not be greater than 12")
                    Else
                        If strVehicle.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_VEHICLE_MASTER Where vehicle_id ='" & strVehicle & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Vehicle (" & strVehicle & ") does not exist in Vehicle Master . Please make it entry first.")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(grow.Cells("Date").Value) <= 0 Then
                        Throw New Exception("Transaction Date is not found")
                    End If
                    If dblCrateQty = 0 AndAlso dblCanQty = 0 Then
                        Throw New Exception("Qty of Crate or Can should be greater then Zero")

                    End If

                    If strType.Length <= 0 Then
                        Throw New Exception("Type is not Blank")
                    ElseIf clsCommon.CompairString(strType, "I") <> CompairStringResult.Equal And clsCommon.CompairString(strType, "O") <> CompairStringResult.Equal Then
                        Throw New Exception("Type should be amoung 'I','O' ")
                    End If


                    Dim obj As New clsCrateReceivedHead()
                    obj.TotalCrateQty = dblCrateQty
                    obj.TotalCanQty = dblCanQty
                    'obj.Document_No = txtDocNo.Value
                    obj.Document_Date = strDate
                    obj.Invoice_Date = strDate
                    obj.Location_Code = strLocation
                    obj.Comments = ""
                    obj.Vehicle_Code = ""
                    obj.Type = strType.ToUpper
                    obj.Route_code = ""
                    obj.Driver = ""
                    obj.SalesMan = ""



                    obj.Arr = New List(Of clsCrateReceivedDetail)
                    ''For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsCrateReceivedDetail()
                    objTr.Line_No = 1

                    objTr.Customer_Code = strcustomer

                    objTr.Sale_Invoice_Date = strDate

                    '=====update by preeti gupta Against ticket no[ERO/01/07/19-000663]
                    objTr.Vehicle_Code = strVehicle
                    objTr.VehicleNo = clsDBFuncationality.getSingleValue("Select Number from tspl_vehicle_master where vehicle_id='" & strVehicle & "'", trans)
                    objTr.CrateQty = 0
                    objTr.CrateQtyRecd = 0
                    objTr.Balance = 0
                    objTr.Remarks = ""
                    objTr.OutQty = dblCrateQty
                    objTr.Adjustment = 0
                    objTr.Jaali = 0
                    objTr.Box = 0

                    objTr.CrateQtyManual = 0
                    objTr.JaaliQtyRecd = 0
                    objTr.BoxQtyRecd = 0
                    objTr.jaaliAdjustment = 0
                    objTr.boxAdjustment = 0
                    objTr.jaaliOutQty = 0
                    objTr.boxOutQty = 0

                    objTr.CANQty = 0
                    objTr.CANRecQty = 0
                    objTr.CANOutQty = dblCanQty
                    objTr.CANAdjustment = 0

                    If (clsCommon.myLen(objTr.OutQty) > 0 OrElse clsCommon.myLen(objTr.CANOutQty) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    obj.SaveData(obj, True, trans)
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                common.clsCommon.MyMessageBoxShow(Me, "Error at Rowno " + clsCommon.myCstr(Counter) + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            Finally
                clsCommon.ProgressBarHide()
                Me.Controls.Remove(gv)
            End Try
        End If

    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Import()
    End Sub
    Sub fillRouteWiseGrid(ByVal strRouteCode As String, ByVal strIndex As Integer, ByVal isLoadAllRoutes As Boolean, ByVal strrouteQry As String)
        'LoadBlankGrid()

        Dim qry As String = Nothing

        qry = " With CTETemp as ( Select convert(varchar,Doc_Date,103) as Doc_Date, Vehicle_Code, Vehicle_Name, Route_No , Route_Desc , OpencrateQty, OpenJaaliQty, OpenBoxQty, OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd, CanQtyRecd, CrateOutQty,  jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty,SUM(CrateQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as CrateQtyClosing,  SUM(JaaliQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as JaaliQtyClosing,  SUM(BoxQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as BoxQtyClosing, SUM(CanQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as CanQtyClosing , Row_Number() OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as RowNo from(" & Environment.NewLine &
        " select pp.Doc_Date  as Doc_Date, pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name , pp.Route_No ,TSPL_ROUTE_MASTER .Route_Desc ,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " & Environment.NewLine &
        " select  convert(date,Doc_Date,103)  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code,xx.Route_No,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing, (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing, (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing  , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing  from (" & Environment.NewLine &
        " select convert(date,'" + txtInvoiceDate.Value + "',103) as Doc_Date, max(Vehicle_Code) as Vehicle_Code , " & Environment.NewLine &
        " Opening.Route_No ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from  (" & Environment.NewLine &
        " -----------chnage query" & Environment.NewLine &
        " Select MAX(Convert(date,XXXXX2.Document_Date,103)) as Document_Date,max( XXXXX2.Vehicle_Code ) as Vehicle_Code ,XXXXX2.route_no,XXXXX2.Type as Type,XXXXX2.Type1 ,sum(XXXXX2.CEILING_OpencrateQty ) as OpencrateQty  ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine &
        " from (  select  XXXXX.*" & Environment.NewLine &
        " from" & Environment.NewLine &
        " (    Select XXXFinal.*   from ( " & Environment.NewLine &
        " Select XXFinal.Document_Date as Document_Date,'' as Vehicle_Code, XXFinal.route_no, XXFinal.Type  as Type,Type1  as Type1, XXFinal.Item_Code,Unit_Code_For_Create  as Unit_Code_For_Create," & Environment.NewLine &
        " -- (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as OpencrateQty ," & Environment.NewLine &
        " CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_OpencrateQty" & Environment.NewLine &
        " ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine &
        " from ( select max(XFinal.Document_Date) as Document_Date,'' as Vehicle_Code, XFinal.route_no,XFinal.Type  as Type,Type1  as Type1, XFinal.Item_Code,max (Unit_Code) as Unit_Code_For_Create,  sum (OpencrateQty) as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty from " & Environment.NewLine &
        " ( " & Environment.NewLine &
        " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,TSPL_SD_SHIPMENT_DETAIL .Qty  as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_DETAIL  LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD " & Environment.NewLine &
        " ON TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'   AND TSPL_SD_SHIPMENT_HEAD.Status =1" & Environment.NewLine &
        " union all " & Environment.NewLine &
        " select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,-1 as Type  ,'I' as Type1,tspl_sd_sale_return_detail.Item_Code ,tspl_sd_sale_return_detail.Unit_code ,tspl_sd_sale_return_detail.Qty  as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from tspl_sd_sale_return_detail LEFT OUTER JOIN TSPL_sd_SALE_RETURN_HEAD  ON TSPL_sd_SALE_RETURN_HEAD.Document_Code = tspl_sd_sale_return_detail.DOCUMENT_CODE  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'   AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & Environment.NewLine &
        " ) XFinal Group by convert(date, Document_Date,103)," & Environment.NewLine &
        " XFinal.route_no, XFinal.Item_Code,Unit_code,Type,Type1" & Environment.NewLine &
        " ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX ) XXXXX2 group by convert(date, Document_Date,103), route_no,Type,Type1 " & Environment.NewLine &
        " ---change query end" & Environment.NewLine &
        " union all  select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty , 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   union all select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty , isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty, isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty, 0 as CrateQtyRecd,0 JaaliQtyRecd , 0 BoxQtyRecd , 0 CanQtyRecd , 0 as CrateOutQty, 0 jaaliOutQty, 0 boxoutqty, 0 Canoutqty, 0  as CrateAdjQty, 0  as JaaliAdjQty, 0  as BoxAdjQty, 0  as CanAdjQty from TSPL_CRATE_RECEIVED_detail_FRESHSALE left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + txtInvoiceDate.Value + "',103)) and Route_No in (" & IIf(isLoadAllRoutes = True, " " & strrouteQry & " ", "'" & strRouteCode & "'") & ") group by Route_No " & Environment.NewLine &
"--- start closing query" & Environment.NewLine &
" UNION All " & Environment.NewLine &
        " select Document_Date, Vehicle_Code, Route_No,OpencrateQty as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRecd Else 0 End as CANQtyRec," & Environment.NewLine &
        " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CanAdjQty  Else 0 End as CanAdjQty " & Environment.NewLine &
        " from ((" & Environment.NewLine &
        " --chnage query " & Environment.NewLine &
        " Select MAX(Convert(date,XXXXX2.Document_Date,103)) as Document_Date,max( XXXXX2.Vehicle_Code ) as Vehicle_Code ,XXXXX2.route_no,XXXXX2.Type as Type,XXXXX2.Type1 ,sum(XXXXX2.CEILING_OpencrateQty ) as OpencrateQty  ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine &
        " from (  select  XXXXX.*" & Environment.NewLine &
        " from" & Environment.NewLine &
        " (    Select XXXFinal.*   from ( " & Environment.NewLine &
        " Select XXFinal.Document_Date as Document_Date,'' as Vehicle_Code, XXFinal.route_no, XXFinal.Type  as Type,Type1  as Type1, XXFinal.Item_Code,Unit_Code_For_Create  as Unit_Code_For_Create," & Environment.NewLine &
        " -- (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as OpencrateQty ," & Environment.NewLine &
        " CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_OpencrateQty" & Environment.NewLine &
        " ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine &
        " from" & Environment.NewLine &
        " ( select max(XFinal.Document_Date) as Document_Date,'' as Vehicle_Code, XFinal.route_no,XFinal.Type  as Type,Type1  as Type1, XFinal.Item_Code,max (Unit_Code) as Unit_Code_For_Create,  sum (OpencrateQty) as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty from " & Environment.NewLine &
        " ( " & Environment.NewLine &
        " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,TSPL_SD_SHIPMENT_DETAIL .Qty  as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_DETAIL  LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD " & Environment.NewLine &
        " ON TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'   AND TSPL_SD_SHIPMENT_HEAD.Status =1" & Environment.NewLine &
        " --and TSPL_SD_SHIPMENT_HEAD.Document_Code ='SSO-100/1920/000001'" & Environment.NewLine &
        "union all " & Environment.NewLine &
        " select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,-1 as Type  ,'I' as Type1,tspl_sd_sale_return_detail.Item_Code ,tspl_sd_sale_return_detail.Unit_code ,tspl_sd_sale_return_detail.Qty  as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from tspl_sd_sale_return_detail LEFT OUTER JOIN TSPL_sd_SALE_RETURN_HEAD  ON TSPL_sd_SALE_RETURN_HEAD.Document_Code = tspl_sd_sale_return_detail.DOCUMENT_CODE  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'   AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & Environment.NewLine &
        " ) XFinal Group by convert(date, Document_Date,103)," & Environment.NewLine &
        " XFinal.route_no, XFinal.Item_Code,Unit_code,Type,Type1 " & Environment.NewLine &
        " ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX ) XXXXX2 group by convert(date, Document_Date,103), route_no,Type,Type1 " & Environment.NewLine &
        " union all" & Environment.NewLine &
        " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine &
        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine &
        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   union all " & Environment.NewLine &
        " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine &
        " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine &
        " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1" & Environment.NewLine &
        " ) " & Environment.NewLine &
        " ) as Closing " & Environment.NewLine &
        " WHERE convert(date,Document_Date ,103)>=convert(date,'" + txtInvoiceDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + txtInvoiceDate.Value + "',103) " & Environment.NewLine &
        " ) as xx where 2=2   GROUP BY Route_No ,convert(date,Doc_Date,103) " & Environment.NewLine &
        " ) as pp   left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =pp .Route_No  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2 and TSPL_ROUTE_MASTER.Route_No in (" & IIf(isLoadAllRoutes = True, " " & strrouteQry & " ", "'" & strRouteCode & "'") & ") ) YYY ) select Route_No  as Route_No ,MAX([Route Name]) as [Route Name] ,sum(CrateQtyClosing ) as CrateQtyClosing ,sum(CanQtyClosing ) as CanQtyClosing ,sum(BoxQtyClosing )as BoxQtyClosing ,sum(JaaliQtyClosing ) as JaaliQtyClosing from (  Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Route_No ,Route_Desc  as [Route Name], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty, jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty, OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing, OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing, OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing,OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing , (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* 0 as CrateValueClosing, (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* 0 as JaaliValueClosing, (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* 0 as BoxValueClosing ,  (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* 0 as CanValueClosing  from (Select  CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Route_No ,CTETemp.Route_Desc , CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty,  CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty,  CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty, CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty,  CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty  from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Route_No =CTETemp.Route_No  AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ )z group by Route_No" & Environment.NewLine



        '===========================================================================
        Dim intLine As Integer = 0
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                intLine += 1
                If isLoadAllRoutes = True Then
                    strIndex = gv1.Rows.Count - 1
                End If
                'gv1.Rows.AddNew()
                gv1.Rows(strIndex).Cells(colLineNo).Value = intLine
                gv1.Rows(strIndex).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                gv1.Rows(strIndex).Cells(colRouteName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value + "' "))

                gv1.Rows(strIndex).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("CrateQtyClosing"))
                gv1.Rows(strIndex).Cells(colJaali).Value = clsCommon.myCdbl(dr("JaaliQtyClosing"))
                gv1.Rows(strIndex).Cells(colBox).Value = clsCommon.myCdbl(dr("BoxQtyClosing"))
                gv1.Rows(strIndex).Cells(colCANQty).Value = clsCommon.myCdbl(dr("CanQtyClosing"))

                PreviousDayCrateQty(strIndex, strRouteCode)
                If isLoadAllRoutes = True Then
                    gv1.Rows.AddNew()
                End If
            Next
            If isLoadAllRoutes = False Then
                gv1.Rows.AddNew()
            End If
            'Else
            '    clsCommon.MyMessageBoxShow("No Data Found")
        End If

    End Sub


    '===========Added by richa agarwal
    '    Sub fillRouteWiseGrid(ByVal strRouteCode As String, ByVal strIndex As Integer)
    '        'LoadBlankGrid()
    '        Dim intLine As Integer = 0
    '        Dim strVehicleSaleInvoice As String = ""
    '        Dim strVehicleCrateInvoice As String = ""
    '        Dim strVehicleCrateSaleReturn As String = ""
    '        Dim strLocation As String = ""
    '        Dim strRoute As String = Nothing
    '        Dim variable1 As String = Nothing
    '        If clsCommon.myLen(fndLocation.Value) <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select Location")
    '            fndLocation.Focus()
    '            Exit Sub

    '        End If


    '        Dim QryForCustomerOpening As String = Nothing
    '        Dim QryForCustomerclosing As String = Nothing
    '        Dim finalQueryForCustomer As String = Nothing
    '        Dim qry As String = Nothing




    '        QryForCustomerOpening = "select "
    '        If chkCustomerWise.Checked Then
    '            QryForCustomerOpening += " convert(date,'" + txtDate.Value + "',103) as Doc_Date,"
    '        Else
    '            QryForCustomerOpening += "convert(date,'" + txtDate.Value + "',103) as Doc_Date,Opening.Vehicle_Code ,"
    '        End If

    '        ''richa BHA/20/06/19-000909 SHOW ONLY POSTED DATA ON REPORT
    '        QryForCustomerOpening += " Opening.Route_No ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from " & _
    '                   " (" & _
    '                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'   AND TSPL_SD_SHIPMENT_HEAD.Status =1" & _
    '                    " union all " & _
    '                    "select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'   AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & _
    '                    " union all " & _
    '                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
    '                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty," & _
    '                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," & _
    '                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," & _
    '                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty ," & _
    '                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
    '                    " 0 BoxQtyRecd ," & _
    '                    " 0 CanQtyRecd ," & _
    '                    " 0 as CrateOutQty," & _
    '                    " 0 jaaliOutQty," & _
    '                    " 0 boxoutqty," & _
    '                     " 0 Canoutqty," & _
    '                    " 0  as CrateAdjQty," & _
    '                    " 0  as JaaliAdjQty," & _
    '                    " 0  as BoxAdjQty," & _
    '                      " 0  as CanAdjQty" & _
    '                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
    '                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
    '                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " & _
    '                    " union all" & _
    '                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
    '                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," & _
    '                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," & _
    '                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," & _
    '                     " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty," & _
    '                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
    '                    " 0 BoxQtyRecd ," & _
    '                    " 0 CanQtyRecd ," & _
    '                    " 0 as CrateOutQty," & _
    '                    " 0 jaaliOutQty," & _
    '                    " 0 boxoutqty," & _
    '                    " 0 Canoutqty," & _
    '                    " 0  as CrateAdjQty," & _
    '                     " 0  as JaaliAdjQty," & _
    '                    " 0  as BoxAdjQty," & _
    '                      " 0  as CanAdjQty" & _
    '                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
    '                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
    '                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " & _
    '                    " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + txtDate.Value + "',103))"

    '        QryForCustomerOpening += " group by Vehicle_Code,Route_No " + Environment.NewLine '----------Qry for Branch opening'





    '        QryForCustomerclosing = "select Document_Date,"
    '        If chkCustomerWise.Checked = False Then
    '            QryForCustomerclosing += " Vehicle_Code,"

    '        End If
    '        QryForCustomerclosing += " Route_code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," + Environment.NewLine & _
    '                    " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " + Environment.NewLine & _
    '                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
    '                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
    '                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  )" & _
    '                    " union all " + Environment.NewLine & _
    '                    " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
    '                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
    '                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " & _
    '                    " union all " + Environment.NewLine & _
    '                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty," & _
    '                    " 0 as CrateQtyRecd, 0 JaaliQtyRecd , " & _
    '                    " 0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty   " & _
    '                    " from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1 " & _
    '                      " union all " + Environment.NewLine & _
    '        " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN as CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & _
    '                    ") " & _
    '                    " ) as Closing " + Environment.NewLine & _
    '                    " WHERE convert(date,Document_Date ,103)>=convert(date,'" + txtDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + txtDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'

    '        finalQueryForCustomer = "select "
    '        If chkCustomerWise.Checked Then
    '            finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date"
    '        Else
    '            finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code"
    '        End If


    '        finalQueryForCustomer += ",xx.Route_No,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," & _
    '                    " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," & _
    '                    " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " & _
    '                     " , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing " & _
    '                    " from (" & _
    '                    "" & QryForCustomerOpening & "" & _
    '                    " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
    '        finalQueryForCustomer += "" & QryForCustomerclosing & "" & _
    '                    "   ) as xx where 2=2  "

    '            finalQueryForCustomer += " GROUP BY Route_No ,convert(date,Doc_Date,103) "


    '        '==========================================END CUSTOMER=========================================================================


    '        qry = "select pp.Doc_Date  as Doc_Date,"
    '        If chkCustomerWise.Checked = False Then
    '            qry += " pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,"
    '        End If

    '        qry += " pp.Route_No ,TSPL_ROUTE_MASTER .Route_Desc ,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine & _
    '                     " " & finalQueryForCustomer & "" + Environment.NewLine & _
    '                    " ) as pp  "
    '        qry += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =pp .Route_No " & _
    '        " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code"


    '        qry += " where 2=2 and TSPL_ROUTE_MASTER.Route_No ='" & strRouteCode & "'  "


    '        Dim qryfinal As String = " With CTETemp as (" & _
    '                   " Select convert(varchar,Doc_Date,103) as Doc_Date,"
    '        If chkCustomerWise.Checked = False Then
    '            qryfinal += " Vehicle_Code, Vehicle_Name,"
    '        End If
    '        qryfinal += " Route_No , Route_Desc , OpencrateQty, OpenJaaliQty, OpenBoxQty, OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd, CanQtyRecd, CrateOutQty, " & _
    '                   " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"

    '        qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as CrateQtyClosing, " & _
    '              " SUM(JaaliQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as JaaliQtyClosing, " & _
    '              " SUM(BoxQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as BoxQtyClosing," & _
    '              " SUM(CanQtyClosing) OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as CanQtyClosing ," & _
    '              " Row_Number() OVER (Partition BY route_No ORDER BY route_No, Doc_Date) as RowNo"
    '        'End If


    '        qryfinal += " from(" + Environment.NewLine & _
    '                          " " & qry & " " & _
    '                          " ) YYY )" & _
    '                          " select Route_No  as Route_No ,MAX([Route Name]) as [Route Name] ,sum(CrateQtyClosing ) as CrateQtyClosing ,sum(CanQtyClosing ) as CanQtyClosing ,sum(BoxQtyClosing )as BoxQtyClosing ,sum(JaaliQtyClosing ) as JaaliQtyClosing from ( " & _
    '" Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Route_No ,Route_Desc  as [Route Name], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty," & _
    '                          " jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty," & _
    '                          " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," & _
    '                          " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," & _
    '                          " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing" & _
    '                          ",OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ," & _
    '                           " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," & _
    '                          " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," & _
    '                          " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " & _
    '                          " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing " & _
    '                          " from (Select  CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Route_No ,CTETemp.Route_Desc , CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " & _
    '                          " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " & _
    '                          " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," & _
    '                          " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " & _
    '                          " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," & _
    '                          " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " & _
    '                          " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Route_No =CTETemp.Route_No  and " & _
    '                          " CT1.Vehicle_Code = CTETemp.Vehicle_Code " & _
    '                          " AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ )z group by Route_No"







    '        '===========================================================================

    '        Dim dt As DataTable
    '        dt = clsDBFuncationality.GetDataTable(qryfinal)
    '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '            For Each dr As DataRow In dt.Rows
    '                intLine += 1
    '                gv1.Rows.AddNew()
    '                gv1.Rows(strIndex).Cells(colLineNo).Value = intLine
    '                gv1.Rows(strIndex).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
    '                gv1.Rows(strIndex).Cells(colRouteName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value + "' "))

    '                gv1.Rows(strIndex).Cells(colCrateQtyManual).Value = clsCommon.myCdbl(dr("CrateQtyClosing"))
    '                gv1.Rows(strIndex).Cells(colJaali).Value = clsCommon.myCdbl(dr("JaaliQtyClosing"))
    '                gv1.Rows(strIndex).Cells(colBox).Value = clsCommon.myCdbl(dr("BoxQtyClosing"))
    '                gv1.Rows(strIndex).Cells(colCANQty).Value = clsCommon.myCdbl(dr("CanQtyClosing"))

    '                PreviousDayCrateQty(strIndex, strRouteCode)
    '            Next
    '            gv1.Rows.AddNew()
    '        Else
    '            clsCommon.MyMessageBoxShow("No Data Found")
    '        End If

    '    End Sub

    Sub PreviousDayCrateQty(ByVal index As Integer, ByVal strRoute As String)
        '       Qry = " select CrateOutQty  as CrateValuePreviousDay from (select Document_Date, Vehicle_Code, Route_code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," & Environment.NewLine & _
        '" Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " & Environment.NewLine & _
        '" from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " & Environment.NewLine & _
        '" left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " & Environment.NewLine & _
        '" where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  ) union all  " & Environment.NewLine & _
        '" (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE " & Environment.NewLine & _
        '" left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No  " & Environment.NewLine & _
        '" where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  union all  " & Environment.NewLine & _
        '" select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, 0 as CrateQtyRecd, 0 JaaliQtyRecd ,  0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1  union all  " & Environment.NewLine & _
        '" select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN as CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 )  ) as Closing  " & Environment.NewLine & _
        '" WHERE convert(date,Document_Date ,103)>=convert(date,'" & txtInvoiceDate.Value & "',103) AND convert(date,Document_Date,103)<=convert(date,'" & txtInvoiceDate.Value & "',103) " & Environment.NewLine & _
        '" )PreviousDayCrateQty where Route_code='" & strRoute & "' " & Environment.NewLine
        'CrateOutQty
        Qry = " select OpencrateQty  as CrateValuePreviousDay from (select Document_Date, Vehicle_Code, Route_No,OpencrateQty as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRecd Else 0 End as CANQtyRec," & Environment.NewLine & _
 " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CanAdjQty  Else 0 End as CanAdjQty " & Environment.NewLine & _
 " from ((" & Environment.NewLine & _
 " --chnage query " & Environment.NewLine & _
 " Select MAX(Convert(date,XXXXX2.Document_Date,103)) as Document_Date,max( XXXXX2.Vehicle_Code ) as Vehicle_Code ,XXXXX2.route_no,XXXXX2.Type as Type,XXXXX2.Type1 ,sum(XXXXX2.CEILING_OpencrateQty ) as OpencrateQty  ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine & _
 " from (  select  XXXXX.*" & Environment.NewLine & _
 " from" & Environment.NewLine & _
 " (    Select XXXFinal.*   from ( " & Environment.NewLine & _
 " Select XXFinal.Document_Date as Document_Date,'' as Vehicle_Code, XXFinal.route_no, XXFinal.Type  as Type,Type1  as Type1, XXFinal.Item_Code,Unit_Code_For_Create  as Unit_Code_For_Create," & Environment.NewLine & _
 " -- (case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end) as OpencrateQty ," & Environment.NewLine & _
 " CEILING ((case when coalesce(StockCrate.Conversion_Factor,0)=0 then 0 else cast((XXFinal.OpencrateQty)* (Stock_SU.Conversion_Factor)/(coalesce(StockCrate.Conversion_Factor,1)) as numeric(18,3)) end)) as CEILING_OpencrateQty" & Environment.NewLine & _
 " ,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty" & Environment.NewLine & _
 " from" & Environment.NewLine & _
 " ( select max(XFinal.Document_Date) as Document_Date,'' as Vehicle_Code, XFinal.route_no,XFinal.Type  as Type,Type1  as Type1, XFinal.Item_Code,max (Unit_Code) as Unit_Code_For_Create,  sum (OpencrateQty) as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty from " & Environment.NewLine & _
 " ( " & Environment.NewLine & _
 " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,TSPL_SD_SHIPMENT_DETAIL .Qty  as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_DETAIL  LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD " & Environment.NewLine & _
 " ON TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'   AND TSPL_SD_SHIPMENT_HEAD.Status =1" & Environment.NewLine & _
 " --and TSPL_SD_SHIPMENT_HEAD.Document_Code ='SSO-100/1920/000001'" & Environment.NewLine & _
" union all " & Environment.NewLine & _
 " select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No   ,-1 as Type  ,'I' as Type1,tspl_sd_sale_return_detail.Item_Code ,tspl_sd_sale_return_detail.Unit_code ,tspl_sd_sale_return_detail.Qty  as OpencrateQty,0 as OpenJaaliQty, 0  as OpenBoxQty , 0  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from tspl_sd_sale_return_detail LEFT OUTER JOIN TSPL_sd_SALE_RETURN_HEAD  ON TSPL_sd_SALE_RETURN_HEAD.Document_Code = tspl_sd_sale_return_detail.DOCUMENT_CODE  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'   AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & Environment.NewLine & _
 " ) XFinal Group by convert(date, Document_Date,103)," & Environment.NewLine & _
 " XFinal.route_no, XFinal.Item_Code,Unit_code,Type,Type1 " & Environment.NewLine & _
 " ) XXFinal   left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXFinal.Item_Code =Stock_SU.Item_Code and XXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockCrate on XXFinal.Item_Code =StockCrate.Item_Code  ) XXXFinal  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on XXXFinal.Item_Code =Stock_SU.Item_Code and XXXFinal.Unit_Code_For_Create  =Stock_SU.UOM_Code  left outer join  (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='CRATE') as StockPcs on XXXFinal.Item_Code =StockPcs.Item_Code  )  XXXXX ) XXXXX2 group by convert(date, Document_Date,103), route_no,Type,Type1 " & Environment.NewLine & _
 " union all" & Environment.NewLine & _
 " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
 " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine & _
 " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1   union all " & Environment.NewLine & _
 " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & Environment.NewLine & _
 " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & Environment.NewLine & _
 " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O'  AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1" & Environment.NewLine & _
 " ) " & Environment.NewLine & _
 " ) as Closing " & Environment.NewLine & _
 " WHERE convert(date,Document_Date ,103)>=convert(date,'" & txtInvoiceDate.Value & "',103) AND convert(date,Document_Date,103)<=convert(date,'" & txtInvoiceDate.Value & "',103) " & Environment.NewLine & _
  " )PreviousDayCrateQty where Route_No ='" & strRoute & "' " & Environment.NewLine


        gv1.Rows(index).Cells(colCrateQtyPreviousDay).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))

    End Sub
    Sub FillGridRouteWithQty()
        'If CrateReceivingWithMultipleRoute = True Then
        Dim qry As String = "Select Route_No From TSPL_ROUTE_MASTER "
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            qry += " where Route_No not in (select Route_code from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE where Document_No ='" & txtDocNo.Value & "')"
            gv1.Rows.AddNew()
        End If



        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0) Then
            isInsideLoadData = True
            isCellValueChangedOpen = True
            'gv1.Rows.AddNew()

            fillRouteWiseGrid("", 0, True, qry)
            isInsideLoadData = False
        End If
        'End If
    End Sub
    'Sub FillGridRouteWithQty()
    '    'If CrateReceivingWithMultipleRoute = True Then
    '    Dim qry As String = "Select Route_No From TSPL_ROUTE_MASTER "
    '    If clsCommon.myLen(txtDocNo.Value) > 0 Then
    '        qry += " where Route_No not in (select Route_code from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE where Document_No ='" & txtDocNo.Value & "')"
    '        gv1.Rows.AddNew()
    '    End If
    '    Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)

    '    If (dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0) Then
    '        isInsideLoadData = True
    '        For Each dr As DataRow In dttemp.Rows
    '            isCellValueChangedOpen = True
    '            'gv1.Rows.AddNew()

    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))

    '            If clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value) > 0 Then
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteName).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + gv1.CurrentRow.Cells(colRouteCode).Value + "' ")
    '            Else
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteName).Value = ""
    '            End If
    '            fillRouteWiseGrid(gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteCode).Value, gv1.Rows(gv1.Rows.Count - 1).Index)


    '        Next
    '        isInsideLoadData = False
    '    End If
    '    'End If
    'End Sub

End Class
