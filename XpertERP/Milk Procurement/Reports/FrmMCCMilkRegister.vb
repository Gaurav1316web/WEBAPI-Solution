'' work doen against ticket no. MIL/30/01/19-000035
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class FrmMCCMilkRegister
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    'Dim arrLoc As String = Nothing
    Dim TankerFromMaster As Integer
    Dim isShowTreeView As Boolean = True
    Dim StrPermission As String

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterMCCCode As String
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Dim arrBack As List(Of String)
    Public arrPlant As ArrayList
    Public arrMcc As ArrayList
    Public arrRoute As ArrayList
    Public arrVLC As ArrayList
    Dim SetCowFatPer As Integer
    Dim SetMixFatPer As Integer
    Private Sub FrmMCCMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetCowFatPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
        SetMixFatPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MixFATPer, clsFixedParameterCode.MixFATPer, Nothing))
        SetUserMgmtNew()
        ChkDetailWise.Checked = True
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        isShowTreeView = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsShowTreeView, clsFixedParameterCode.IsShowTreeView, Nothing)) = 1
        chkShowVLCUploaderData.Checked = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        If isShowTreeView Then
            LoadMCCRouteVLCTree()
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.Panel2Collapsed = True
        Else
            SplitContainer2.Panel1Collapsed = True
            SplitContainer2.Panel2Collapsed = False
        End If
        LoadMilkReceiveUOM()
        LoadShiftFrom()
        LoadShiftTo()
        LoadSRNAmountType()
        arrBack = New List(Of String)
        Reset()
        If FilterON Then
            txtFromDate.Value = FilterfromDate
            txtToDate.Value = FilterToDate
            ChkDetailWise.Checked = True
            Dim arr As New ArrayList
            arr.Add(FilterMCCCode)
            txtMCC.arrValueMember = arr
            txtFromShift.Text = "M"
            txtToShift.Text = "E"
            btnGo.PerformClick()
        End If
    End Sub

    Sub LoadMilkReceiveUOM()
        Dim qry As String = " SELECT '' AS Code,'Select...' as Name union SELECT 'KG' AS Code, 'KG' as Name union SELECT 'LTR' AS Code, 'LTR' as Name "
        cboMilkReceiveUOM.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboMilkReceiveUOM.ValueMember = "Code"
        cboMilkReceiveUOM.DisplayMember = "Name"
    End Sub

    'Private Sub LOCATIONRIGTHS()
    '    Try
    '        Dim obj As New clsMCCCodes()
    '        obj = clsMCCCodes.GetData()

    '        If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
    '            arrLoc = obj.arrLocCodes
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.MCCMilkRegister)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        radbtnBulkExp.Visible = MyBase.isExport
        btnLock.Enabled = MyBase.isPostFlag
    End Sub

    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(StrPermission) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + StrPermission + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub

    Sub LoadSRNAmountType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow

        dr = dt.NewRow
        dr("Code") = "ZeroAndNonZero"
        dr("Name") = "Zero And Non Zero"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Zero"
        dr("Name") = "Zero"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "NonZero"
        dr("Name") = "Non Zero"
        dt.Rows.Add(dr)

        cboSRNAmounType.DataSource = dt
        cboSRNAmounType.ValueMember = "Code"
        cboSRNAmounType.DisplayMember = "Name"

        cboSRNAmounType.SelectedValue = "ZeroAndNonZero"
    End Sub

    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).FormatString = "{0:n2}"
        Next

        If gv.Columns.Contains("Cow Milk Qty (Ltr)") = True Then
            gv.Columns("Cow Milk Qty (Ltr)").IsVisible = False
        End If
        If gv.Columns.Contains("Buffalo Milk Qty (Ltr)") = True Then
            gv.Columns("Buffalo Milk Qty (Ltr)").IsVisible = False
        End If
        If gv.Columns.Contains("Handling_Charges_Amount") = True Then
            gv.Columns("Handling_Charges_Amount").IsVisible = True
            gv.Columns("Handling_Charges_Amount").Width = 100
            gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"
        End If

        If gv.Columns.Contains("Transporter Code") = True Then
            gv.Columns("Transporter Code").IsVisible = True
            gv.Columns("Transporter Code").Width = 75
            gv.Columns("Transporter Code").HeaderText = "Transporter Code"
        End If

        If gv.Columns.Contains("Transporter Name") = True Then
            gv.Columns("Transporter Name").IsVisible = True
            gv.Columns("Transporter Name").Width = 75
            gv.Columns("Transporter Name").HeaderText = "Transporter Name"
        End If

        If gv.Columns.Contains("price_code") = True Then
            gv.Columns("price_code").IsVisible = True
            gv.Columns("price_code").Width = 100
            gv.Columns("price_code").HeaderText = "Price Code"
        End If

        If gv.Columns.Contains("Plant Code") = True Then
            gv.Columns("Plant Code").IsVisible = False
            gv.Columns("Plant Name").IsVisible = False
        End If

        If gv.Columns.Contains("Planning_Code") = True Then
            gv.Columns("Planning_Code").IsVisible = True
            gv.Columns("Planning_Code").HeaderText = "Price Plan Code"
        End If
        If gv.Columns.Contains("Planning_Posted_Date") = True Then
            gv.Columns("Planning_Posted_Date").IsVisible = True
            gv.Columns("Planning_Posted_Date").HeaderText = "Price Plan Posted Date"
        End If
        If gv.Columns.Contains("Planning_Posted_Time") = True Then
            gv.Columns("Planning_Posted_Time").IsVisible = True
            gv.Columns("Planning_Posted_Time").HeaderText = "Price Plan Posted Time"
        End If

        If gv.Columns.Contains("Declared_Rate") = True Then
            gv.Columns("Declared_Rate").IsVisible = True
            gv.Columns("Declared_Rate").HeaderText = "Standard Rate"
        End If

        gv.Columns("VSP_Commission_Amount").IsVisible = True
        gv.Columns("VSP_Commission_Amount").Width = 100
        gv.Columns("VSP_Commission_Amount").HeaderText = "VSP Commission Amount"

        gv.Columns("VSP_Deduction_Amount").IsVisible = True
        gv.Columns("VSP_Deduction_Amount").Width = 100
        gv.Columns("VSP_Deduction_Amount").HeaderText = "VSP Quality Deduction Amount"
        If gv.Columns.Contains("VSP_Day_Wise_Incentive") = True Then
            gv.Columns("VSP_Day_Wise_Incentive").IsVisible = True
            gv.Columns("VSP_Day_Wise_Incentive").Width = 100
            gv.Columns("VSP_Day_Wise_Incentive").HeaderText = "VSP Day Incentive Amount"
        End If

        If gv.Columns.Contains("Vehicle") Then
            gv.Columns("Vehicle").Width = 100
            gv.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster
        End If

        If ChkDetailWise.Checked Then
            gv.Columns("Milk Receipt Code").IsVisible = True
            gv.Columns("Milk Receipt Code").Width = 100
            gv.Columns("Milk Receipt Code").HeaderText = " Milk Receipt Code"

            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Type").IsVisible = False
            gv.Columns("Chilling Center").IsVisible = False

            gv.Columns("Shift").IsVisible = True

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Doc Date").IsVisible = True
            gv.Columns("Doc Date").Width = 100
            gv.Columns("Doc Date").HeaderText = " Doc Date"

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vehicle Code").IsVisible = True
            gv.Columns("Vehicle Code").Width = 100
            gv.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            gv.Columns("VSP Code").IsVisible = True
            gv.Columns("VSP Code").Width = 100
            gv.Columns("VSP Code").HeaderText = " VSP Code"

            gv.Columns("VSP Name").IsVisible = True
            gv.Columns("VSP Name").Width = 100
            gv.Columns("VSP Name").HeaderText = "VSP Name"

            gv.Columns("Vendor Group Code").IsVisible = True
            gv.Columns("Vendor Group Code").Width = 100
            gv.Columns("Vendor Group Code").HeaderText = "Vendor Group Code"

            gv.Columns("Vlc Uploader Code").IsVisible = True
            gv.Columns("Vlc Uploader Code").Width = 100
            gv.Columns("Vlc Uploader Code").HeaderText = "Vlc Uploader Code"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = " Vlc Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"

            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 100
            gv.Columns("Item_Code").HeaderText = "Item Code"

            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 150
            gv.Columns("Item_Desc").HeaderText = "Item"

            gv.Columns("Sample No").IsVisible = True
            gv.Columns("Sample No").Width = 100
            gv.Columns("Sample No").HeaderText = "Sample No"
            gv.Columns("Sample No").FormatString = "{0:n0}"

            gv.Columns("No Of Cans").IsVisible = True
            gv.Columns("No Of Cans").Width = 100
            gv.Columns("No Of Cans").HeaderText = "No Of Cans"
            gv.Columns("No Of Cans").FormatString = "{0:n0}"

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            If gv.Columns.Contains("Capping_FAT") Then
                gv.Columns("Capping_FAT").IsVisible = False
                gv.Columns("Capping_FAT").HeaderText = "Before Capping FAT(%)"
            End If
            If gv.Columns.Contains("Capping_SNF") Then
                gv.Columns("Capping_SNF").IsVisible = False
                gv.Columns("Capping_SNF").HeaderText = "Before Capping SNF(%)"
            End If

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"


            gv.Columns("Cow Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Cow CLR").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow CLR").Width = 100
            gv.Columns("Cow CLR").HeaderText = "Buffalo CLR"

            gv.Columns("Buffalo CLR").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo CLR").Width = 100
            gv.Columns("Buffalo CLR").HeaderText = "Buffalo CLR"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo SNF (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF (KG)").Width = 100
            gv.Columns("Buffalo SNF (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("Milk Type").IsVisible = True
            gv.Columns("Milk Type").Width = 100
            gv.Columns("Milk Type").HeaderText = " Milk Type"

            gv.Columns("SRN No").IsVisible = True
            gv.Columns("SRN No").Width = 100
            gv.Columns("SRN No").HeaderText = "SRN No"

            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("SRN No").IsVisible = True
            gv.Columns("SRN No").Width = 100
            gv.Columns("SRN No").HeaderText = "SRN No"

            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"

            gv.Columns("SRN Rate").IsVisible = True
            gv.Columns("SRN Rate").Width = 100
            gv.Columns("SRN Rate").HeaderText = "SRN Rate"
            gv.Columns("SRN Rate").FormatString = "{0:n3}"

            gv.Columns("Shift Status").IsVisible = True
            gv.Columns("Shift Status").Width = 100
            gv.Columns("Shift Status").HeaderText = "Shift Status"

            gv.Columns("Invoice_no").IsVisible = True
            gv.Columns("Invoice_no").Width = 100
            gv.Columns("Invoice_no").HeaderText = "Invoice No"

            gv.Columns("Invoice_Date").IsVisible = True
            gv.Columns("Invoice_Date").Width = 100
            gv.Columns("Invoice_Date").HeaderText = "Invoice Date"
            'TankerFromMaster = 1 AndAlso
            gv.Columns("Purchase_Order_No").IsVisible = (chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True) ''BHA/09/07/18-000138  by balwinder on 11/07/2018
            gv.Columns("Purchase_Order_No").Width = 100
            gv.Columns("Purchase_Order_No").HeaderText = "PO No"

            gv.Columns("Date").IsVisible = False

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

            gv.Columns("SNF_Ded_Value").IsVisible = False
            gv.Columns("SNF_Ded_Value").HeaderText = "SNF Deduction Value"

            gv.Columns("SNF_Ded_Rate").IsVisible = False
            gv.Columns("SNF_Ded_Rate").HeaderText = "SNF Deduction Rate"

            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            If gv.Columns.Contains("Doc Date Time") Then
                gv.Columns("Doc Date Time").IsVisible = False
            End If


            'TankerFromMaster = 1 AndAlso
            If chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True Then
                gv.Columns("EMP_Amount").IsVisible = True
                gv.Columns("EMP_Amount").Width = 100
                gv.Columns("EMP_Amount").HeaderText = "SRN Emp Amount"

                gv.Columns("TIP_Amount").IsVisible = True
                gv.Columns("TIP_Amount").Width = 100
                gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

                gv.Columns("Service_Charge_Amount").IsVisible = True
                gv.Columns("Service_Charge_Amount").Width = 150
                gv.Columns("Service_Charge_Amount").HeaderText = "Service Charge Amount"

                gv.Columns("NetAmount").IsVisible = True
                gv.Columns("NetAmount").Width = 150
                gv.Columns("NetAmount").HeaderText = "SRN Net Amount" '"NetAmount (SRNAmt+EmpAmt-ServiceAmt)"


            Else
                Try
                    gv.Columns("EMP_Amount").IsVisible = True
                    gv.Columns("EMP_Amount").Width = 100
                    gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

                    gv.Columns("TIP_Amount").IsVisible = True
                    gv.Columns("TIP_Amount").Width = 100
                    gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"
                Catch ex As Exception

                End Try
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                gv.Columns("IS_MANUAL").IsVisible = False
                gv.Columns("IS_MILK_SAMPLE_MANUAL").IsVisible = True
                gv.Columns("IS_MILK_SAMPLE_MANUAL").Width = 75
                gv.Columns("IS_MILK_SAMPLE_MANUAL").HeaderText = "Is Manual"

                gv.Columns("Transporter Code").IsVisible = True
                gv.Columns("Transporter Code").Width = 75
                gv.Columns("Transporter Code").HeaderText = "Transporter Code"

                gv.Columns("Transporter Name").IsVisible = True
                gv.Columns("Transporter Name").Width = 75
                gv.Columns("Transporter Name").HeaderText = "Transporter Name"
            Else
                gv.Columns("IS_MILK_SAMPLE_MANUAL").IsVisible = False
                gv.Columns("IS_MANUAL").IsVisible = True
                gv.Columns("IS_MANUAL").Width = 75
                gv.Columns("IS_MANUAL").HeaderText = "Is Manual"
            End If


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)



            Dim item51 As New GridViewSummaryItem("FAT(LTR)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)


            Dim item52 As New GridViewSummaryItem("SNF(LTR)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item52)


            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)



            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)




            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)

            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            'TankerFromMaster = 1 AndAlso
            If chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True Then
                Dim item22 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item22)
                Dim item122 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item122)
                Dim item23 As New GridViewSummaryItem("Service_Charge_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item23)
                Dim item24 As New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item24)
            Else
                Try
                    Dim item111 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item111)
                    Dim item1111 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1111)
                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception

                End Try

            End If

            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item14 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)


            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            ''richa agarwal 11 Dec,2018 ERO/11/12/18-000429
            Dim summaryItem50 As New GridViewSummaryItem()
            summaryItem50.FormatString = "{0:F3}"
            summaryItem50.Name = "SRN Rate"
            summaryItem50.AggregateExpression = "sum([SRN Amount])/sum([SRN Qty])"
            summaryRowItem.Add(summaryItem50)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf ChkMCCWise.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("MCC Type").IsVisible = False
            gv.Columns("Chilling Center").IsVisible = False

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"




            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            'TankerFromMaster = 0 AndAlso
            If chkRejection.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                Try
                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception

                End Try

            End If

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)
            Dim abcd As New GridViewSummaryItem("EMP_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcd)
            Dim abcde As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcde)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "IIf(sum([Buffalo FAT (KG)])>0,sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "IIf(sum([Buffalo SNF (KG)])>0,sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf rbtnPlantWise.Checked Then
            gv.Columns("Plant Code").IsVisible = True
            gv.Columns("Plant Code").Width = 100
            gv.Columns("Plant Code").HeaderText = "Plant Code"

            gv.Columns("Plant Name").IsVisible = True
            gv.Columns("Plant Name").Width = 100
            gv.Columns("Plant Name").HeaderText = "Plant Name"

            gv.Columns("MCC Code").IsVisible = False
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = False
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Total Solid").IsVisible = True
            gv.Columns("Total Solid").Width = 100
            gv.Columns("Total Solid").HeaderText = "Total Solid"
            gv.Columns("Total Solid").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Cow Total Solid").IsVisible = True
            gv.Columns("Cow Total Solid").Width = 100
            gv.Columns("Cow Total Solid").HeaderText = "Cow Total Solid"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("Buffalo Total Solid").IsVisible = True
            gv.Columns("Buffalo Total Solid").Width = 100
            gv.Columns("Buffalo Total Solid").HeaderText = "Buffalo Total Solid"

            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"




            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            'TankerFromMaster = 0 AndAlso
            If chkRejection.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                Try


                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception

                End Try

            End If

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)
            Dim abcd As New GridViewSummaryItem("EMP_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcd)
            Dim abcde As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcde)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item51 As New GridViewSummaryItem("Total Solid", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item51)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)

            Dim item81 As New GridViewSummaryItem("Cow Total Solid", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item81)

            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item1Baff As New GridViewSummaryItem("Buffalo Total Solid", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1Baff)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "IIf(sum([Buffalo FAT (KG)])>0,sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "IIf(sum([Buffalo SNF (KG)])>0,sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf chkRoutewise.Checked Then
            'TankerFromMaster = 1 AndAlso
            If chkShiftWise.Checked And (chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True) Then
                gv.Columns("Shift").IsVisible = True
                gv.Columns("Shift").Width = 80
                gv.Columns("Shift").HeaderText = "Shift"
            End If
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("MCC Type").IsVisible = False
            gv.Columns("Chilling Center").IsVisible = False

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 170
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"


            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"





            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            'TankerFromMaster = 0 AndAlso
            If chkRejection.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                Try

                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception
                End Try

            End If

            Dim intCount As Integer = 0
            Dim item111 As New GridViewSummaryItem("EMP_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item111)
            Dim item1111 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1111)
            Dim abcd As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(abcd)
            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            'And TankerFromMaster = 1
            If chkShiftWise.Checked AndAlso (chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True) Then
                gv.GroupDescriptors.Add(New GridGroupByExpression("[Route Code] as [Route Code] format ""{0}: {1}"" Group By [Route Code]"))
            End If
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf rbtnVLCWise.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("MCC Type").IsVisible = False
            gv.Columns("Chilling Center").IsVisible = False

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = "VLC Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"


            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"


            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            ''richa agarwal MIL/01/02/19-000039 12 Feb,2019
            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"


            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            'TankerFromMaster = 0 AndAlso
            If chkRejection.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                Try
                    gv.Columns("NET_AMOUNT").IsVisible = True
                    gv.Columns("NET_AMOUNT").Width = 100
                    gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"

                    gv.Columns("Round_Off").IsVisible = True
                    gv.Columns("Round_Off").Width = 100
                    gv.Columns("Round_Off").HeaderText = "SRN Round Off"

                    gv.Columns("Handling_Charges_Amount").IsVisible = True
                    gv.Columns("Handling_Charges_Amount").Width = 100
                    gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


                    Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item112)
                    Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item113)
                    Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item114)
                Catch ex As Exception
                End Try

            End If

            If gv.Columns.Contains("Handling_Charges_Amount") = True Then
                Dim item555 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item555)
            End If

            Dim item111 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item111)
            Dim item1111 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1111)
            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf chkVLCWisePayable.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("MCC Type").IsVisible = False
            gv.Columns("Chilling Center").IsVisible = False

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"


            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = "VLC Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"



            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"


            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = Not objCommonVar.DisplayTypeInMilkReceipt
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            ''richa agarwal MIL/01/02/19-000039 12 Feb,2019
            gv.Columns("EMP_Amount").IsVisible = True
            gv.Columns("EMP_Amount").Width = 100
            gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            gv.Columns("TIP_Amount").IsVisible = True
            gv.Columns("TIP_Amount").Width = 100
            gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

            gv.Columns("Head_Load_Amount").IsVisible = True
            gv.Columns("Head_Load_Amount").Width = 100
            gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"


            gv.Columns("SNF_Ded_Amount").IsVisible = False
            gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            If gv.Columns.Contains("NET_AMOUNT") Then
                gv.Columns("NET_AMOUNT").IsVisible = True
                gv.Columns("NET_AMOUNT").Width = 100
                gv.Columns("NET_AMOUNT").HeaderText = "SRN Net Amount"
            End If

            If gv.Columns.Contains("Round_Off") Then
                gv.Columns("Round_Off").IsVisible = True
                gv.Columns("Round_Off").Width = 100
                gv.Columns("Round_Off").HeaderText = "SRN Round Off"
            End If


            gv.Columns("Handling_Charges_Amount").IsVisible = True
            gv.Columns("Handling_Charges_Amount").Width = 100
            gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"


            If gv.Columns.Contains("SaleAmt") Then
                gv.Columns("SaleAmt").IsVisible = True
                gv.Columns("SaleAmt").Width = 100
                gv.Columns("SaleAmt").HeaderText = "Sale Amount"
            End If


            gv.Columns("VSP_Commission_Amount").IsVisible = True
            gv.Columns("VSP_Commission_Amount").Width = 100
            gv.Columns("VSP_Commission_Amount").HeaderText = "VSP Commission Amount"

            gv.Columns("VSP_Deduction_Amount").IsVisible = True
            gv.Columns("VSP_Deduction_Amount").Width = 100
            gv.Columns("VSP_Deduction_Amount").HeaderText = "VSP Deduction Amount"


            gv.Columns("VSP_Day_Wise_Incentive").IsVisible = True
            gv.Columns("VSP_Day_Wise_Incentive").Width = 100
            gv.Columns("VSP_Day_Wise_Incentive").HeaderText = "VSP Day Wise Incentive"

            If gv.Columns.Contains("DeductionAmt") Then
                gv.Columns("DeductionAmt").IsVisible = True
                gv.Columns("DeductionAmt").Width = 100
                gv.Columns("DeductionAmt").HeaderText = "Deduction Amt"
            End If


            If gv.Columns.Contains("IncetiveAmt") Then
                gv.Columns("IncetiveAmt").IsVisible = True
                gv.Columns("IncetiveAmt").Width = 100
                gv.Columns("IncetiveAmt").HeaderText = "Incentive Amt"
            End If

            If gv.Columns.Contains("DeductionDesc") Then
                gv.Columns("DeductionDesc").IsVisible = True
                gv.Columns("DeductionDesc").Width = 100
                gv.Columns("DeductionDesc").HeaderText = "Deduction Desc"
            End If

            If gv.Columns.Contains("DeductionCode") Then
                gv.Columns("DeductionCode").IsVisible = True
                gv.Columns("DeductionCode").Width = 100
                gv.Columns("DeductionCode").HeaderText = "Deduction Code"
            End If
            If gv.Columns.Contains("Addition") Then
                gv.Columns("Addition").IsVisible = True
                gv.Columns("Addition").Width = 100
                gv.Columns("Addition").HeaderText = "Manual Add"
            End If

            If gv.Columns.Contains("ManualDed") Then
                gv.Columns("ManualDed").IsVisible = True
                gv.Columns("ManualDed").Width = 100
                gv.Columns("ManualDed").HeaderText = "Manual Ded"
            End If


            If gv.Columns.Contains("PayableAmt") Then
                gv.Columns("PayableAmt").IsVisible = True
                gv.Columns("PayableAmt").Width = 100
                gv.Columns("PayableAmt").HeaderText = "Payable Amt"
            End If


            Dim item1141 As New GridViewSummaryItem("SaleAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1141)
            Dim item1145 As New GridViewSummaryItem("DeductionAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1145)
            Dim item1146 As New GridViewSummaryItem("IncetiveAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1146)
            Dim item21147 As New GridViewSummaryItem("PayableAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21147)


            Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item112)
            Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item113)
            Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item114)


            Dim item111 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item111)
            Dim item1111 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1111)
            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item101)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf rdoVLCWisePaymentSummary.Checked Then
            '[MCC Code],[MCC Name],[Vlc Code] , [VLC Name], 
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            'gv.Columns("MCC Type").IsVisible = False
            'gv.Columns("Chilling Center").IsVisible = False

            'gv.Columns("Route Code").IsVisible = False
            'gv.Columns("Route Code").Width = 100
            'gv.Columns("Route Code").HeaderText = "Route Code"


            'gv.Columns("Route Name").IsVisible = False
            'gv.Columns("Route Name").Width = 100
            'gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = "VLC Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"

            '[VLC Uploader Code], [VSP Code],   

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            'gv.Columns("FAT(%)").IsVisible = False
            'gv.Columns("FAT(%)").Width = 100
            'gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            'gv.Columns("SNF(%)").IsVisible = False
            'gv.Columns("SNF(%)").Width = 100
            'gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"
            gv.Columns("FAT(KG)").FormatString = "{0:n3}"


            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            gv.Columns("SNF(KG)").FormatString = "{0:n3}"

            gv.Columns("FAT(LTR)").IsVisible = True
            gv.Columns("FAT(LTR)").Width = 100
            gv.Columns("FAT(LTR)").HeaderText = " FAT(LTR)"
            gv.Columns("FAT(LTR)").FormatString = "{0:n3}"


            gv.Columns("SNF(LTR)").IsVisible = True
            gv.Columns("SNF(LTR)").Width = 100
            gv.Columns("SNF(LTR)").HeaderText = "SNF(LTR)"
            gv.Columns("SNF(LTR)").FormatString = "{0:n3}"

            'gv.Columns("Cow Milk Qty (KG)").IsVisible = False 'Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Cow Milk Qty (KG)").Width = 100
            'gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            'gv.Columns("Cow FAT(%)").IsVisible = False 'Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Cow FAT(%)").Width = 100
            'gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            'gv.Columns("Cow SNF(%)").IsVisible = False 'Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Cow SNF(%)").Width = 100
            'gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            'gv.Columns("Cow FAT (KG)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Cow FAT (KG)").Width = 100
            'gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            'gv.Columns("Cow SNF (KG)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Cow SNF (KG)").Width = 100
            'gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            'gv.Columns("Buffalo Milk Qty (KG)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            'gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            'gv.Columns("Buffalo SNF(%)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Buffalo SNF(%)").Width = 100
            'gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            'gv.Columns("Buffalo FAT(%)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Buffalo FAT(%)").Width = 100

            'gv.Columns("Buffalo FAT (KG)").IsVisible = False ' Not objCommonVar.DisplayTypeInMilkReceipt
            'gv.Columns("Buffalo FAT (KG)").Width = 100
            'gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("FAT_Amount").IsVisible = True
            gv.Columns("FAT_Amount").Width = 100
            gv.Columns("FAT_Amount").HeaderText = "FAT Amount"

            gv.Columns("SNF_Amount").IsVisible = True
            gv.Columns("SNF_Amount").Width = 100
            gv.Columns("SNF_Amount").HeaderText = "SNF Amount"



            gv.Columns("REJ_AMOUNT").IsVisible = True
            gv.Columns("REJ_AMOUNT").Width = 100
            gv.Columns("REJ_AMOUNT").HeaderText = "Reject Milk Amt"

            'gv.Columns("SRN Qty").IsVisible = True
            'gv.Columns("SRN Qty").Width = 100
            'gv.Columns("SRN Qty").HeaderText = "SRN Qty"

            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "Gross Milk Amount"

            ''richa agarwal MIL/01/02/19-000039 12 Feb,2019
            'gv.Columns("EMP_Amount").IsVisible = True
            'gv.Columns("EMP_Amount").Width = 100
            'gv.Columns("EMP_Amount").HeaderText = "SRN EMP Amount"

            'gv.Columns("TIP_Amount").IsVisible = True
            'gv.Columns("TIP_Amount").Width = 100
            'gv.Columns("TIP_Amount").HeaderText = "SRN TIP Amount"

            'gv.Columns("Head_Load_Amount").IsVisible = True
            'gv.Columns("Head_Load_Amount").Width = 100
            'gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"


            'gv.Columns("SNF_Ded_Amount").IsVisible = False
            'gv.Columns("SNF_Ded_Amount").HeaderText = "SNF Deduction Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            gv.Columns("NET_AMOUNT").IsVisible = True
            gv.Columns("NET_AMOUNT").Width = 100
            gv.Columns("NET_AMOUNT").HeaderText = "Net Amount"

            'gv.Columns("Round_Off").IsVisible = True
            'gv.Columns("Round_Off").Width = 100
            'gv.Columns("Round_Off").HeaderText = "SRN Round Off"

            'gv.Columns("Handling_Charges_Amount").IsVisible = True
            'gv.Columns("Handling_Charges_Amount").Width = 100
            'gv.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges"



            gv.Columns("SaleAmt").IsVisible = True
            gv.Columns("SaleAmt").Width = 100
            gv.Columns("SaleAmt").HeaderText = "Sale Amount"

            gv.Columns("VSP_Commission_Amount").IsVisible = True
            gv.Columns("VSP_Commission_Amount").Width = 100
            gv.Columns("VSP_Commission_Amount").HeaderText = "EMP"

            gv.Columns("VSP_Deduction_Amount").IsVisible = True
            gv.Columns("VSP_Deduction_Amount").Width = 100
            gv.Columns("VSP_Deduction_Amount").HeaderText = "CB Ded."


            'gv.Columns("VSP_Day_Wise_Incentive").IsVisible = True
            'gv.Columns("VSP_Day_Wise_Incentive").Width = 100
            'gv.Columns("VSP_Day_Wise_Incentive").HeaderText = "VSP Day Wise Incentive"

            gv.Columns("DeductionAmt").IsVisible = False
            gv.Columns("DeductionAmt").Width = 100
            gv.Columns("DeductionAmt").HeaderText = "TotalDed."

            gv.Columns("DeductionAmt_RM").IsVisible = True
            gv.Columns("DeductionAmt_RM").Width = 100
            gv.Columns("DeductionAmt_RM").HeaderText = "RM Ded."

            gv.Columns("DeductionAmt_RATE_DIFF").IsVisible = True
            gv.Columns("DeductionAmt_RATE_DIFF").Width = 100
            gv.Columns("DeductionAmt_RATE_DIFF").HeaderText = "Rate Diff."

            gv.Columns("DeductionAmt_Advance").IsVisible = True
            gv.Columns("DeductionAmt_Advance").Width = 100
            gv.Columns("DeductionAmt_Advance").HeaderText = "Advance"

            gv.Columns("Local_Sale_Amount").IsVisible = True
            gv.Columns("Local_Sale_Amount").Width = 100
            gv.Columns("Local_Sale_Amount").HeaderText = "Local Sale"

            gv.Columns("Std_Deduction_Amount").IsVisible = True
            gv.Columns("Std_Deduction_Amount").Width = 100
            gv.Columns("Std_Deduction_Amount").HeaderText = "Standard Deduction"


            'gv.Columns("IncetiveAmt").IsVisible = True
            'gv.Columns("IncetiveAmt").Width = 100
            'gv.Columns("IncetiveAmt").HeaderText = "Incentive Amt"


            gv.Columns("Total Payment").IsVisible = True
            gv.Columns("Total Payment").Width = 100
            gv.Columns("Total Payment").HeaderText = "Total Payment"

            gv.Columns("Excess Paid").IsVisible = True
            gv.Columns("Excess Paid").Width = 100
            gv.Columns("Excess Paid").HeaderText = "Excess Paid"

            gv.Columns("Final Payment").IsVisible = True
            gv.Columns("Final Payment").Width = 100
            gv.Columns("Final Payment").HeaderText = "Final Payment"

            ' 

            gv.Columns("Pro Diff").IsVisible = False
            gv.Columns("Pro Diff").Width = 100
            gv.Columns("Pro Diff").HeaderText = "PRO"

            Dim item1141 As New GridViewSummaryItem("SaleAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1141)
            Dim item1145 As New GridViewSummaryItem("DeductionAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1145)
            'Dim item1146 As New GridViewSummaryItem("IncetiveAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1146)
            Dim item21147 As New GridViewSummaryItem("Total Payment", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21147)

            Dim item21147_ExcessPaid As New GridViewSummaryItem("Excess Paid", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21147_ExcessPaid)

            Dim item21147_FinalPayment As New GridViewSummaryItem("Final Payment", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21147_FinalPayment)

            'VSP_Commission_Amount,VSP_Deduction_Amount,DeductionAmt,[NET_AMOUNT], SaleAmt,[Total Payment] [Excess Paid],[Final Payment]



            Dim item112 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item112)
            'Dim item113 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item113)
            'Dim item114 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item114)


            'Dim item111 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item111)
            'Dim item1111 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1111)
            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            'Dim item101 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item101)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            'Dim summaryItem1 As New GridViewSummaryItem()
            'summaryItem1.FormatString = "{0:F2}"
            'summaryItem1.Name = "FAT(%)"
            'summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            'summaryRowItem.Add(summaryItem1)

            'Dim summaryItem2 As New GridViewSummaryItem()
            'summaryItem2.FormatString = "{0:F2}"
            'summaryItem2.Name = "SNF(%)"
            'summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            'summaryRowItem.Add(summaryItem2)

            'Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item6)
            'Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item7)

            'Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            'summaryRowItem.Add(item8)
            'Dim summaryItem3 As New GridViewSummaryItem()
            'summaryItem3.FormatString = "{0:F2}"
            'summaryItem3.Name = "Cow SNF(%)"
            'summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            'summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            'Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item9)
            'Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item10)
            'Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item11)
            'Dim summaryItem5 As New GridViewSummaryItem()
            'summaryItem5.FormatString = "{0:F2}"
            'summaryItem5.Name = "Buffalo FAT(%)"
            'summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            'summaryRowItem.Add(summaryItem5)

            'Dim summaryItem6 As New GridViewSummaryItem()
            'summaryItem6.FormatString = "{0:F2}"
            'summaryItem6.Name = "Buffalo SNF(%)"
            'summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            'summaryRowItem.Add(summaryItem6)
            'Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            'Dim item15 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item15)

            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)

            Dim SummaryVSPProDiff As New GridViewSummaryItem("Pro Diff", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPProDiff)

            Dim SummaryVSPFAT_Amount As New GridViewSummaryItem("FAT_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPFAT_Amount)
            Dim SummaryVSPSNF_Amount As New GridViewSummaryItem("SNF_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPSNF_Amount)


            SummaryVSPSNF_Amount = New GridViewSummaryItem("Local_Sale_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPSNF_Amount)
            SummaryVSPSNF_Amount = New GridViewSummaryItem("Std_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPSNF_Amount)

            ' FAT(LTR), SNF(LTR)
            Dim SummaryVSPFAT_LTR As New GridViewSummaryItem("FAT(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPFAT_LTR)
            Dim SummaryVSPSNF_LTR As New GridViewSummaryItem("SNF(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPSNF_LTR)
            'Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SummaryVSPDayWiseInc)
            Dim SummaryREJ_AMOUNT As New GridViewSummaryItem("REJ_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryREJ_AMOUNT)

            Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable("select distinct tspl_deduction_master.Description as deduction FROM TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)")

            If dtDeduction IsNot Nothing AndAlso dtDeduction.Rows.Count > 0 Then
                For i As Integer = 0 To dtDeduction.Rows.Count - 1
                    Dim aa = clsCommon.myCstr(dtDeduction.Rows(i).Item(0))
                    Dim item111 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item111)
                    gv.Columns(i).FormatString = "{0:F2}"
                Next
            End If

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf rdbPlantWisePaymentSummary.Checked Then

            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).Width = 100
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "IIf(sum([Cow SNF (KG)])>0,sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "IIf(sum([Cow FAT (KG)])>0,sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)]),0)"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)
            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)

            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item14 As New GridViewSummaryItem("EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            Dim item15 As New GridViewSummaryItem("TIP_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)
            Dim item16 As New GridViewSummaryItem("NET_AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
            Dim item17 As New GridViewSummaryItem("Round_Off", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item18 As New GridViewSummaryItem("Handling_Charges_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)
            Dim item19 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("SNF_Ded_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item21 As New GridViewSummaryItem("SaleAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21)
            Dim SummaryVSPCommission As New GridViewSummaryItem("VSP_Commission_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPCommission)
            Dim SummaryVSPQualityDeduction As New GridViewSummaryItem("VSP_Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPQualityDeduction)
            Dim SummaryVSPDayWiseInc As New GridViewSummaryItem("VSP_Day_Wise_Incentive", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SummaryVSPDayWiseInc)
            Dim item22 As New GridViewSummaryItem("DeductionAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item22)
            Dim item23 As New GridViewSummaryItem("PayableAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item23)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        End If
    End Sub

    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
        EnableDisableControl(True)
        If chkRoutewise.Checked = True AndAlso chkShiftWise.Checked = True AndAlso chkRejection.Checked = False AndAlso chkShowVLCUploaderData.Checked = False AndAlso chkOnlyRejection.Checked = False Then
            RadButton1.Enabled = True
        Else
            RadButton1.Enabled = False
        End If
        'If ChkDetailWise.Checked = True Then
        '    btnPrintMccDetails.Visible = True
        'Else
        '    btnPrintMccDetails.Visible = False
        'End If
        btnPrintMccDetails.Enabled = False
        arrBack = New List(Of String)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

        RadGroupBox2.Enabled = val
    End Sub

    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try
            If chkRoutewise.Checked = True AndAlso chkShiftWise.Checked = True AndAlso chkRejection.Checked = False AndAlso chkShowVLCUploaderData.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                RadButton1.Enabled = True
            Else
                RadButton1.Enabled = False
            End If
            If ChkDetailWise.Checked = True Then
                btnPrintMccDetails.Enabled = True
            Else
                btnPrintMccDetails.Enabled = False
            End If

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If isShowTreeView Then
                If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
                    Exit Sub
                End If
            End If
            Dim FinalQuery As String = Nothing
            Dim qry As String = Nothing
            Dim arrMCC As ArrayList = Nothing
            Dim arrRoute As ArrayList = Nothing
            Dim arrVLC As ArrayList = Nothing

            If isShowTreeView Then
                Dim arr As List(Of String) = Nothing
                If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedValue(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrMCC = New ArrayList
                        For Each str As String In arr
                            arrMCC.Add(str)
                        Next
                    Else
                        Throw New Exception("Please select at least one MCC")
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedValue(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrRoute = New ArrayList
                        For Each str As String In arr
                            arrRoute.Add(str)
                        Next
                    Else
                        Throw New Exception("Please select at least one Route")
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedValue(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrVLC = New ArrayList
                        For Each str As String In arr
                            arrVLC.Add(str)
                        Next
                    Else
                        Throw New Exception("Please select at least one VLC Code")
                    End If
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrMCC = txtMCC.arrValueMember
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrRoute = txtRoute.arrValueMember
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrVLC = txtVLC.arrValueMember
                End If
            End If
            If chkRejection.Checked = False AndAlso chkShiftWise.Checked = False AndAlso chkOnlyRejection.Checked = False Then
                qry = clsMilkRejectHead.GetMCCRegisterQuery(txtFromDate.Value, txtToDate.Value, txtFromShift.Text, txtToShift.Text, clsCommon.myCstr(cboSRNAmounType.SelectedValue), StrPermission, arrMCC, arrRoute, arrVLC, clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue))
                If ChkDetailWise.Checked Then
                    '============update by preeti gupta Against ticket no[BHA/15/05/19-000890]
                    If BulkExport = 4 Then
                        FinalQuery += "" & qry & " "
                    Else
                        FinalQuery = "" & qry & " order by final.[Doc Date],final.[Milk Receipt Code] ,final.[Sample No] "
                    End If

                ElseIf rbtnVLCWise.Checked Then
                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[Vendor Group Code],aa.[Vendor Group Desc],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.CLR,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle  from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,MAX (pp.[Vendor Group Code]) as [Vendor Group Code] ,MAX ([Vendor Group Desc]) as [Vendor Group Desc],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount ) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive ) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine & ""
                    FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code] "
                    FinalQuery += " )as xx"
                    FinalQuery += " ) as xxx"
                    If BulkExport = 4 Then
                        FinalQuery += " ) as aa "
                    Else
                        FinalQuery += " ) as aa order  by [MCC Code],[Route Code],[Vlc Code] "
                    End If

                ElseIf chkRoutewise.Checked Then
                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)],aa.CLR ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)],aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo SNF(%)],aa.[Buffalo CLR] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount )as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive)as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle  from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                    FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Route Code]"
                    FinalQuery += " )as xx"
                    FinalQuery += " ) as xxx"
                    If BulkExport = 4 Then
                        FinalQuery += " ) as aa "
                    Else
                        FinalQuery += " ) as aa order by [MCC Code],[Route Code]"
                    End If

                ElseIf ChkMCCWise.Checked Then
                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)],aa.CLR ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)],aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)],aa.[Buffalo CLR] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle  from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                    FinalQuery += " ) as  pp group by pp.[MCC Code] "
                    FinalQuery += " )as xx"
                    FinalQuery += " ) as xxx"
                    If BulkExport = 4 Then
                        FinalQuery += " ) as aa  "
                    Else
                        FinalQuery += " ) as aa order by [MCC Code] "
                    End If
                ElseIf rbtnPlantWise.Checked Then
                    FinalQuery = "select aa.[Plant Code],aa.[Plant Name],aa.[MCC Code],aa.[MCC Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)],aa.CLR ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)],aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)],aa.[Buffalo CLR] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name]) as [Plant Name],'' as [MCC Code] ,'' as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                    FinalQuery += " ) as  pp group by pp.[Plant Code] "
                    FinalQuery += " )as xx"
                    FinalQuery += " ) as xxx"
                    If BulkExport = 4 Then
                        FinalQuery += " ) as aa  "
                    Else
                        FinalQuery += " ) as aa order by [Plant Code] "
                    End If
                ElseIf chkVLCWisePayable.Checked Then
                    FinalQuery = clsMilkPurchaseInvoiceProvisionHead.GetQryPayableMccMilkRegister(qry, txtFromDate.Value, txtToDate.Value)
                    If BulkExport = 4 Then
                        FinalQuery += "  order  by [MCC Code],[Route Code],[Vlc Code] "
                    End If
                ElseIf rdbPlantWisePaymentSummary.Checked = True Then
                    FinalQuery = clsMilkPurchaseInvoiceProvisionHead.GetQryPayableMccMilkRegister(qry, txtFromDate.Value, txtToDate.Value)
                    FinalQuery = " Select FinalPWPS.[Plant Code]  as [Plant Code],max(FinalPWPS.[Plant Name]) as [Plant Name],FinalPWPS.[MCC Code] as [MCC Code] ,max(FinalPWPS.[MCC Name]) as [MCC Name] ,sum(FinalPWPS.[Milk Weight]) as [Milk Weight] ,sum(FinalPWPS.[Milk Weight(KG)]) as [Milk Weight(KG)],sum(FinalPWPS.[Milk Weight(LTR)]) as [Milk Weight(LTR)],case when sum(FinalPWPS.[Milk Weight(KG)] )=0 then 0 else (sum(FinalPWPS.[FAT(KG)] )/sum(FinalPWPS.[Milk Weight(KG)] ))*100 end as [FAT(%)], case when sum(FinalPWPS.[Milk Weight(KG)] )=0 then 0 else (sum(FinalPWPS.[SNF(KG)] )/sum(FinalPWPS.[Milk Weight(KG)] ))*100 end as [SNF(%)] ,avg(FinalPWPS.CLR) as CLR,sum(FinalPWPS.[FAT(KG)]) as [FAT(KG)],sum(FinalPWPS.[SNF(KG)]) as [SNF(KG)],sum(FinalPWPS.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],case when sum(FinalPWPS.[Cow Milk Qty (KG)] )=0 then 0 else (sum(FinalPWPS.[Cow FAT (KG)] )/sum(FinalPWPS.[Cow Milk Qty (KG)] ))*100 end as [Cow FAT(%)], case when sum(FinalPWPS.[Cow Milk Qty (KG)] )=0 then 0 else (sum(FinalPWPS.[Cow Snf (KG)] )/sum(FinalPWPS.[Cow Milk Qty (KG)] ))*100 end as [Cow SNF(%)],avg(FinalPWPS.[Cow CLR]) as [Cow CLR],sum(FinalPWPS.[Cow FAT (KG)]) as [Cow FAT (KG)],sum(FinalPWPS.[Cow Snf (KG)]) as [Cow SNF (KG)],sum(FinalPWPS.[Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],case when sum(FinalPWPS.[Buffalo Milk Qty (KG)])=0 then 0 else (sum(FinalPWPS.[Buffalo FAT (KG)])/sum(FinalPWPS.[Buffalo Milk Qty (KG)] ))*100 end as [Buffalo FAT(%)], case when sum(FinalPWPS.[Buffalo Milk Qty (KG)] )=0 then 0 else (sum(FinalPWPS.[Buffalo SNF (KG)] )/sum(FinalPWPS.[Buffalo Milk Qty (KG)] ))*100 end as [Buffalo SNF(%)],avg(FinalPWPS.[Buffalo CLR]) as [Buffalo CLR],sum(FinalPWPS.[Buffalo FAT (KG)]) as [Buffalo FAT (KG)],sum(FinalPWPS.[Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum(FinalPWPS.[SRN Qty]) as [SRN Qty],sum(FinalPWPS.[SRN Amount]) as [SRN Amount],sum(FinalPWPS.EMP_Amount) as EMP_Amount,sum(FinalPWPS.TIP_Amount) as TIP_Amount,sum(FinalPWPS.NET_AMOUNT) as NET_AMOUNT,sum(FinalPWPS.Round_Off) as Round_Off,sum(FinalPWPS.Handling_Charges_Amount) as Handling_Charges_Amount,sum(FinalPWPS.Head_Load_Amount) as Head_Load_Amount,sum(FinalPWPS.SNF_Ded_Amount) as SNF_Ded_Amount,sum(FinalPWPS.SaleAmt) as SaleAmt,sum(FinalPWPS.VSP_Commission_Amount) as VSP_Commission_Amount,sum(FinalPWPS.VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(FinalPWPS.VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,sum(FinalPWPS.DeductionAmt) as DeductionAmt,sum(isnull(FinalPWPS.NET_AMOUNT,0))+sum(isnull(FinalPWPS.VSP_Day_Wise_Incentive,0))-sum(isnull(FinalPWPS.DeductionAmt,0)) as PayableAmt from (" & FinalQuery & ")FinalPWPS group by FinalPWPS.[MCC Code],FinalPWPS.[Plant Code]  "

                    If BulkExport = 4 Then
                        FinalQuery += "  order  by [MCC Code],[Plant Code] "
                    End If
                ElseIf rdoVLCWisePaymentSummary.Checked = True Then
                    Dim strITEMType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+ITEM_TYPE+']' from (select distinct case when TSPL_ITEM_MASTER.CSA_TYPE = 'None' then 'Other Sale' else TSPL_ITEM_MASTER.CSA_TYPE +' Sale' end ITEM_TYPE  from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_SD_SALE_INVOICE_Detail on TSPL_SD_SALE_INVOICE_Detail.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.document_code left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  left outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.ITEM_CODE =  TSPL_SD_SALE_INVOICE_Detail.Item_Code  where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) XXX For XML Path('')),1,1,'') "))

                    Dim ItemColumnName As String = ""
                    If clsCommon.myLen(strITEMType) > 0 Then
                        ItemColumnName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_MILK_SALE_DETAIL.['+ITEM_TYPE+'],0) as ' +'['+ ITEM_TYPE+']' from (select distinct case when TSPL_ITEM_MASTER.CSA_TYPE = 'None' then 'Other Sale' else TSPL_ITEM_MASTER.CSA_TYPE +' Sale' end ITEM_TYPE from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_SD_SALE_INVOICE_Detail on TSPL_SD_SALE_INVOICE_Detail.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.document_code left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  left outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.ITEM_CODE =  TSPL_SD_SALE_INVOICE_Detail.Item_Code where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) XXX For XML Path('')),1,1,'')   "))
                        ItemColumnName += " ,"
                    End If

                    'Dim strDeduction As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)) XXX For XML Path('')),1,1,'') "))

                    'Dim ItemDeductionName As String = ""
                    'If clsCommon.myLen(strDeduction) > 0 Then
                    '    ItemDeductionName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_DEDUCTION.['+deduction+'],0) as ' +'['+ deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)) XXX For XML Path('')),1,1,'')   "))
                    '    ItemDeductionName += " ,"
                    'End If

                    Dim strDeduction As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_DEDUCTION_MASTER) XXX For XML Path('')),1,1,'') "))

                    Dim ItemDeductionName As String = ""
                    If clsCommon.myLen(strDeduction) > 0 Then
                        ItemDeductionName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_DEDUCTION.['+deduction+'],0) as ' +'['+ deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_DEDUCTION_MASTER ) XXX For XML Path('')),1,1,'')   "))
                        ItemDeductionName += " ,"
                    End If

                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[Vendor Group Code],aa.[Vendor Group Desc],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.CLR,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[FAT(LTR)],aa.[SNF(LTR)],aa.FAT_Amount,aa.SNF_Amount,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],isnull (TBL_MILK_REJ.REJ_AMOUNT,0) as REJ_AMOUNT ,aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount, aa.[VSP Code],ExtrCol.SaleAmt,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,ExtrCol.DeductionAmt, IncetiveAmt,aa.NET_AMOUNT+Round_Off+Handling_Charges_Amount+Head_Load_Amount-isnull(SNF_Ded_Amount,0)-isnull(SaleAmt,0)+VSP_Commission_Amount-VSP_Deduction_Amount+VSP_Day_Wise_Incentive-isnull(DeductionAmt,0)+IncetiveAmt as PayableAmt  from ( "
                    FinalQuery = " select aa.[MCC Code] ,aa.[MCC Name] ,aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[VSP Code],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[FAT(LTR)],aa.[SNF(LTR)],aa.FAT_Amount + isnull (TBL_PRO_DATA.FAT_Amount,0) as FAT_Amount ,aa.SNF_Amount + isnull (TBL_PRO_DATA.SNF_Amount,0) as SNF_Amount , isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) as [Pro Diff] ,isnull (aa.VSP_Commission_Amount,0) as VSP_Commission_Amount ,isnull (aa.VSP_Deduction_Amount,0) as VSP_Deduction_Amount, (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0)) as [SRN Amount],isnull (TBL_MILK_REJ.REJ_AMOUNT,0) as REJ_AMOUNT ,isnull( ExtrCol.DeductionAmt,0) as DeductionAmt,isnull (ExtrCol.DeductionAmt_RM,0) as  DeductionAmt_RM,isnull (ExtrCol.Local_Sale_Amount,0) as  Local_Sale_Amount,isnull (ExtrCol.Std_Deduction_Amount,0) as  Std_Deduction_Amount, isnull (ExtrCol.DeductionAmt_RATE_DIFF,0)  as DeductionAmt_RATE_DIFF, isnull (ExtrCol.DeductionAmt_Advance,0) as DeductionAmt_Advance ,(isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) as [NET_AMOUNT], " + ItemColumnName + " ExtrCol.SaleAmt, (isnull (aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) as [Total Payment],case when (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) < 0 then (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) else 0 end   as [Excess Paid]," + ItemDeductionName + " case when ((isnull(aa.FAT_Amount,0)+ isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0))  > 0 then (isnull(aa.FAT_Amount,0)+ isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull (TBL_MILK_REJ.REJ_AMOUNT,0) - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) else 0 end as [Final Payment] from ( "
                    FinalQuery += " select xxx.* ,"
                    FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                    FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                    FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                    FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                    FinalQuery += " from ("
                    FinalQuery += " select xx.*"
                    FinalQuery += " from ( "
                    FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,MAX (pp.[Vendor Group Code]) as [Vendor Group Code] ,MAX ([Vendor Group Desc]) as [Vendor Group Desc],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," ' pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                    FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                    FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                    FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)], Sum (FAT_Amount) as FAT_Amount , Sum (SNF_Amount) as SNF_Amount , "
                    FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                    FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                    FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,max([VSP Code]) as [VSP Code],sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,sum(IncetiveAmt) as IncetiveAmt  from ("
                    FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine & ""
                    FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Vlc Code] " ' pp.[Route Code]
                    FinalQuery += " )as xx" + Environment.NewLine
                    FinalQuery += " ) as xxx ) as aa" + Environment.NewLine +
                    " left join ( select vendor_code,sum(Total_Amt* case when RI=1 then 1 else 0 end) as SaleAmt,sum(Total_Amt* case when RI=2 then 1 else 0 end) as DeductionAmt , sum(Total_Amt* case when RI=2  and DeductionCode = 'R/M MACHINERY (-)' then 1 else 0 end) as DeductionAmt_RM ,sum(Total_Amt* case when RI=2 and  DeductionCode = 'RATE DIFF' then 1 else 0 end) as DeductionAmt_RATE_DIFF ,  sum(Total_Amt* case when RI=2 and  DeductionCode = 'Advance' then 1 else 0 end) as DeductionAmt_Advance,sum(Total_Amt* case when RI=2 and  RefDocType = 'PRO-LCS' then 1 else 0 end) as Local_Sale_Amount,sum(Total_Amt* case when RI=2 and  RefDocType = 'PRO-STD' then 1 else 0 end) as Std_Deduction_Amount  from (" + Environment.NewLine +
                    " select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,1 as RI ,  'Sale'  as DeductionCode,'' as RefDocType " + Environment.NewLine +
                    " from TSPL_SD_SALE_INVOICE_HEAD  " + Environment.NewLine +
                    " left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     " + Environment.NewLine +
                    " where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine +
                    " union all" + Environment.NewLine +
                    " select  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code , TSPL_VENDOR_INVOICE_HEAD.Document_Total as Total_Amount ,2 as RI , DeductionCode,TSPL_VENDOR_INVOICE_HEAD.RefDocType  " + Environment.NewLine +
                    " from TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine +
                    " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  " + Environment.NewLine +
                    " where  Document_Type='D' and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and RefDocType  not in ( 'MILK-REJ','VSP-QLT','PRO-VFD')   " + Environment.NewLine +
                    " and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'') or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)   " + Environment.NewLine +
                    " and TSPL_VENDOR_INVOICE_HEAD.POSTING_DATE  between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  " + Environment.NewLine +
                    " union All
                                          select Vendor_CODE,sum(Item_Net_AMount) as	Item_Net_AMount, 	'2' as  RI , 'Advance' DeductionCode,'' as RefDocType  from  ( select '' as  RefDocType, 'Advance' as Trans_Type, TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No , Payment_No as AP_Invoice_No, Vendor_code, 'Advance' as Item_Desc, 0 as Paymnet_Amount, Payment_Amount as Item_Net_Amount, 0 as Show_FAT_SNF from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT  left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.doc_no = TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.doc_no
                                          where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  ),103)    
                                          )   xxx group by RefDocType,Vendor_CODE            

                                        )x group by vendor_code) as ExtrCol on ExtrCol.vendor_code= aa.[VSP Code] 

                                        left Outer Join (select  TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, sum (TSPL_PAYMENT_PROCESS_DEDUCTION.Amount ) as REJ_AMOUNT  
                                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                                        left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                                        left outer join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_VENDOR_INVOICE_HEAD.RefDocNo and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_VENDOR_INVOICE_HEAD.Ref_SNo 
                                        left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE
                                        where   RefDocType='MILK-REJ' and TSPL_MILK_REJECT_HEAD.DOC_DATE between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   group by TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE) as TBL_MILK_REJ on TBL_MILK_REJ.vendor_code = aa.[VSP Code]

                                        left outer Join ( select Vendor_CODE,sum(FAT_Amount) as FAT_Amount , sum (SNF_Amount) as SNF_Amount, sum(Amount) as Amount from( select  Vendor_CODE ,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0)  as Amount from (
                    select  max(xxx.doc_no) as doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from(
                    select RefDocType,case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
                    where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.RefDocType= 'PRO-VFC' and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103)
                    union all
                    select RefDocType,case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, 
                     TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo
                    from TSPL_PAYMENT_PROCESS_DEDUCTION 
                    left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1
                    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                     where  (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 and RefDocType='PRO-VFD'  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103)
                    ) 
                    ) xxx group by  Vendor_CODE,SNo,trans_type
                     ) as final 
                     left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no
                    left outer join (


                    select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (select  isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPI_Code,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address,     '11/02/2021'  as fromDate ,'20/02/2021'  as Todate ,'  '  as companyADD, 'Bhole Baba Milk Food Industries (Dholpur) Pvt. Ltd.'  as CompName,'BHBA'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate
                    ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate
                    ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate
                    ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT, TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Buffalo' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type 
                    ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
                    ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT
                    ,TSPL_MILK_SRN_HEAD.VEHICLE_CODE
                    ,cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
                    ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
                    ,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
                    ,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
                    ,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
                    ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
                    ,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
                    ,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
                    ,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
                    ,(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply )  as QBD
                     from TSPL_MILK_PURCHASE_INVOICE_DETAIL  
                     Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
                     left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE 
                     Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  
                      Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  
                     left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE 
                     left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE 
                      left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE 
                     Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  
                     Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  
                     left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code 
                     left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  
                     left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code 
                     left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code 
                     left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103)    


                      ) as pp group by VSP_CODE,VLC_Code ) as PaymentProcess on   PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code 
                     left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code 
                     left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code  left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code 
                     left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no   where 2=2   and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103)   


                     and exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE)  )x group by vsp_code
                     )  Tab on tab.VSP_CODE=final.Vendor_CODE where   convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103)
                     ) PrvPRO_DATA group by Vendor_CODE) as TBL_PRO_DATA on TBL_PRO_DATA.Vendor_CODE = aa.[VSP Code]



                                        "

                    If clsCommon.myLen(strITEMType) > 0 Then
                        FinalQuery += " left outer Join (select vendor_code, " + strITEMType + "  from (
                                                        select vendor_code, ITEM_TYPE , Sum (Item_Net_Amt) as Item_Net_Amt  from (
                                                        select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,TSPL_SD_SALE_INVOICE_Detail.Item_Code ,case when TSPL_ITEM_MASTER.CSA_TYPE = 'None' then 'Other Sale' else TSPL_ITEM_MASTER.CSA_TYPE +' Sale' end ITEM_TYPE , TSPL_SD_SALE_INVOICE_Detail.Item_Net_Amt 
                                                        from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_SD_SALE_INVOICE_Detail on TSPL_SD_SALE_INVOICE_Detail.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.document_code
                                                        left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  
                                                        left outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.ITEM_CODE =  TSPL_SD_SALE_INVOICE_Detail.Item_Code
                                                        where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   
                                                        ) XXX_SALE  group by vendor_code, ITEM_TYPE
                                                        ) XXX_SALE_FINAL
                                                        pivot
                                                        (
                                                          sum(Item_Net_Amt)
                                                          for ITEM_TYPE in (" + strITEMType + ")
                                                        ) piv) as TBL_MILK_SALE_DETAIL on TBL_MILK_SALE_DETAIL.vendor_code = aa.[VSP Code] "

                    End If

                    If clsCommon.myLen(strDeduction) > 0 Then
                        FinalQuery += " left outer Join (select vendor_code, " + strDeduction + "   from (
                                         select vendor_code, Description , Sum (amount) as amount  from (
                                        SELECT TSPL_PAYMENT_PROCESS_DEDUCTION.vendor_code,TSPL_DEDUCTION_MASTER.Description,TSPL_PAYMENT_PROCESS_DEDUCTION.amount FROM TSPL_PAYMENT_PROCESS_DEDUCTION
                                        left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                                        left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                                        WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  ),103) 
                                         ) XXX_DEDUCTION  group by vendor_code, Description
                                         ) XXX_DEDUCTION_FINAL
                                          pivot
                                         ( sum(amount)
                                         for Description in (" + strDeduction + ")
                                         ) piv) as TBL_DEDUCTION on TBL_DEDUCTION.vendor_code = aa.[VSP Code]  "

                    End If

                    '                    Dim strITEMType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+ITEM_TYPE+']' from (select distinct case when TSPL_ITEM_MASTER.CSA_TYPE = 'None' then 'Other Sale' else TSPL_ITEM_MASTER.CSA_TYPE +' Sale' end ITEM_TYPE  from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_SD_SALE_INVOICE_Detail on TSPL_SD_SALE_INVOICE_Detail.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.document_code left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  left outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.ITEM_CODE =  TSPL_SD_SALE_INVOICE_Detail.Item_Code  where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) XXX For XML Path('')),1,1,'') "))

                    '                    Dim ItemColumnName As String = ""
                    '                    If clsCommon.myLen(strITEMType) > 0 Then
                    '                        ItemColumnName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_MILK_SALE_DETAIL.['+ITEM_TYPE+'],0) as ' +'['+ ITEM_TYPE+']' from (select distinct case when TSPL_ITEM_MASTER.CSA_TYPE = 'None' then 'Other Sale' else TSPL_ITEM_MASTER.CSA_TYPE +' Sale' end ITEM_TYPE from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_SD_SALE_INVOICE_Detail on TSPL_SD_SALE_INVOICE_Detail.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.document_code left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  left outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.ITEM_CODE =  TSPL_SD_SALE_INVOICE_Detail.Item_Code where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) XXX For XML Path('')),1,1,'')   "))
                    '                        ItemColumnName += " ,"
                    '                    End If

                    '                    Dim strDeduction As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)) XXX For XML Path('')),1,1,'') "))

                    '                    Dim ItemDeductionName As String = ""
                    '                    If clsCommon.myLen(strDeduction) > 0 Then
                    '                        ItemDeductionName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_DEDUCTION.['+deduction+'],0) as ' +'['+ deduction+']' from (select distinct tspl_deduction_master.Description as deduction FROM TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)) XXX For XML Path('')),1,1,'')   "))
                    '                        ItemDeductionName += " ,"
                    '                    End If

                    '                    FinalQuery = " select aa.[MCC Code] ,aa.[MCC Name] ,aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[VSP Code],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[FAT(LTR)],aa.[SNF(LTR)],aa.FAT_Amount + isnull (TBL_PRO_DATA.FAT_Amount,0) as FAT_Amount ,aa.SNF_Amount + isnull (TBL_PRO_DATA.SNF_Amount,0) as SNF_Amount , isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) as [Pro Diff] ,isnull (aa.VSP_Commission_Amount,0) as VSP_Commission_Amount ,isnull (aa.VSP_Deduction_Amount,0) as VSP_Deduction_Amount
                    ' , (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0)) as [SRN Amount]
                    ' ,isnull (ExtrCol.Local_Sale_Amount,0) as  Local_Sale_Amount , isnull (ExtrCol.DeductionAmt_Advance,0) as DeductionAmt_Advance 
                    ' ,(isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull( ExtrCol.DeductionAmt,0) as [NET_AMOUNT]
                    ' , isnull (TBL_MILK_SALE_DETAIL.[cattle feed Sale],0) as [cattle feed Sale], isnull (TBL_MILK_SALE_DETAIL.[ghee Sale],0) as [ghee Sale] 
                    ' , ExtrCol.SaleAmt
                    '  , TBL_DEDUCTION.[Std Deduction (short received)], TBL_DEDUCTION.[Local Sale (MILK)], TBL_DEDUCTION.[Assets Lost], TBL_DEDUCTION.[RETURN QTY], TBL_DEDUCTION.[PRO DATA DEDUCTION], TBL_DEDUCTION.[R/M MACHINERY (-)]
                    ' ,isnull( ExtrCol.DeductionAmt,0) as DeductionAmt
                    ' , (isnull (aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) as [Total Payment]
                    ' ,case when (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull(aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))- isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) < 0 then (isnull(aa.FAT_Amount,0)+isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0)+ isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))
                    ' - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) else 0 end   as [Excess Paid]
                    ', case when ((isnull(aa.FAT_Amount,0)+ isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0)) 
                    '  - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0))  > 0 then (isnull(aa.FAT_Amount,0)+ isnull(aa.SNF_Amount,0)+ isnull( aa.VSP_Commission_Amount,0) + isnull (TBL_PRO_DATA.FAT_Amount,0)+isnull (TBL_PRO_DATA.SNF_Amount,0) - isnull(aa.VSP_Deduction_Amount,0))
                    '  - isnull( ExtrCol.DeductionAmt,0) -isnull(SaleAmt,0) else 0 end as [Final Payment]  from (  select xxx.* ,  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)], case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)], case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)] from ( select xx.* from ( select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,MAX (pp.[Vendor Group Code]) as [Vendor Group Code] ,MAX ([Vendor Group Desc]) as [Vendor Group Desc],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)] ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)], sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)], Sum (FAT_Amount) as FAT_Amount , Sum (SNF_Amount) as SNF_Amount ,  sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)], sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,max([VSP Code]) as [VSP Code],sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,sum(IncetiveAmt) as IncetiveAmt  from (

                    'Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.Short_Description_MCC,final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ,final.[Route Code],final.[Route Name],final.Short_Description_Route ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code] ,final.[Vendor Group Desc],final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name],final.Short_Description_VLC , final.[Sample No] ,final.[No Of Cans] ,final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.Capping_FAT,final.[FAT(%)]  ,final.CLR,final.Capping_SNF,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)],final.[FAT(LTR)],final.[SNF(LTR)], final.FAT_Amount , final.SNF_Amount ,final.[Cow Milk Qty (KG)],final.[Cow Milk Qty (Ltr)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then final.CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)], final.[Buffalo Milk Qty (KG)],final.[Buffalo Milk Qty (Ltr)] ,Case When final.[FAT(%)] > 5 Then final.CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,(CASE WHEN [Sample Status]='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,[Transporter Code],[Transporter Name],EMP_Amount,TIP_Amount,NET_AMOUNT,Round_Off,Handling_Charges_Amount,final.Planning_Code,final.Planning_Posted_Date,final.Planning_Posted_Time ,final.Declared_Rate,final.[Price Code],final.Purchase_Order_No,final.Head_Load_Amount,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.IncetiveAmt,final.SubStandard,final.Vehicle, final.[SubStandardQty],final.[Doc Date Time]  From (Select case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_SNF else null end as Capping_SNF,TSPL_MCC_MASTER.MCC_Type as [MCC Type],TSPL_MCC_MASTER.Short_Description as Short_Description_MCC,TSPL_MCC_ROUTE_MASTER.Short_Description as Short_Description_Route,TSPL_VLC_MASTER_HEAD.Short_Description as Short_Description_VLC,case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc  ,Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Cow Milk Qty (Ltr)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)] ,Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (Ltr)]  , Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR, Convert(decimal(18,2), TSPL_MILK_SRN_DETAIL.FAT_KG) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.SNF_KG) As [SNF(KG)], Convert(decimal(18,2), ROUND(TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100,2,1)) As [FAT(LTR)], Convert(decimal(18,2),ROUND(TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100,2,1)) As [SNF(LTR)], cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount +isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0) )  * isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount , cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount ,  Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code,FORMAT ( TSPL_PRICE_CHART_PLANNING.Posted_Date , 'dd/MM/yyyy') as Planning_Posted_Date, FORMAT (TSPL_PRICE_CHART_PLANNING. Posted_Date , 'hh:mm:ss tt') as Planning_Posted_Time,TSPL_MILK_PRICE_MASTER.Declared_Rate,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount 
                    ',TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                    ' ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive 
                    ' ,case when TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code is null then isnull( TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.Incentive_Amount,0) else isnull(TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount,0) end as  IncetiveAmt,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then  TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR else 0 end as [SubStandardQty],Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) + ' ' + CONVERT(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,8) as [Doc Date Time] 
                    ' From TSPL_MILK_RECEIPT_DETAIL 
                    ' Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE 
                    ' Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE
                    ' Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE 
                    ' Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO 
                    ' Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
                    ' left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                    ' Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                    ' Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
                    ' Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                    ' Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE
                    ' Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE
                    ' left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                    ' Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE
                    ' Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                    ' Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                    ' Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
                    ' And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) 
                    ' And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code,max(Planning_Code) as Planning_Code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code 
                    ' left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code
                    ' left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code =  TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code 
                    ' left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE  where 2 = 2  and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='01/Jun/2022' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='10/Jun/2022'and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN ('MCC--000001')  ) As final where 2=2 
                    ') as  pp group by pp.[MCC Code],pp.[Vlc Code]  )as xx
                    ' ) as xxx ) as aa
                    ' left join ( select vendor_code,sum(Total_Amt* case when RI=1 then 1 else 0 end) as SaleAmt,sum(Total_Amt* case when RI=2 then 1 else 0 end) as DeductionAmt 
                    ' ,  sum(Total_Amt* case when RI=2 and  DeductionCode = 'Advance' then 1 else 0 end) as DeductionAmt_Advance
                    ' ,sum(Total_Amt* case when RI=2 and  RefDocType = 'PRO-LCS' then 1 else 0 end) as Local_Sale_Amount
                    ' ,sum(Total_Amt* case when RI=2 and  RefDocType = 'PRO-STD' then 1 else 0 end) as Std_Deduction_Amount  from (
                    ' select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,1 as RI ,  'Sale'  as DeductionCode,'' as RefDocType 
                    ' from TSPL_SD_SALE_INVOICE_HEAD  
                    ' left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     
                    ' where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '01/Jun/2022 12:00:00 AM' and '10/Jun/2022 11:59:59 PM'
                    ' union all
                    'SELECT TSPL_PAYMENT_PROCESS_DEDUCTION.vendor_code,TSPL_PAYMENT_PROCESS_DEDUCTION.amount as Total_Amount ,2 as RI
                    ' , TSPL_PAYMENT_PROCESS_DEDUCTION.ded_code as  DeductionCode,'' as RefDocType FROM TSPL_PAYMENT_PROCESS_DEDUCTION
                    'left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                    'left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                    'WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) 
                    'and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'  ),103) 
                    'union All
                    ' select Vendor_CODE,sum(Item_Net_AMount) as	Item_Net_AMount, 	'2' as  RI , 'Advance' DeductionCode,'' as RefDocType  from  ( select '' as  RefDocType, 'Advance' as Trans_Type, TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No , Payment_No as AP_Invoice_No, Vendor_code, 'Advance' as Item_Desc, 0 as Paymnet_Amount, Payment_Amount as Item_Net_Amount, 0 as Show_FAT_SNF from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT  left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.doc_no = TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.doc_no
                    '  where  convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'  ),103)    
                    ' )   xxx group by RefDocType,Vendor_CODE            
                    ' )x group by vendor_code) as ExtrCol on ExtrCol.vendor_code= aa.[VSP Code] 
                    '  left outer Join ( select Vendor_CODE,sum(FAT_Amount) as FAT_Amount , sum (SNF_Amount) as SNF_Amount, sum(Amount) as Amount from( select  Vendor_CODE ,trans_type,round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as  FAT_Amount,Amount - round( (coalesce(Amount,0)*tab.FAT_Amount)/(tab.FAT_Amount+tab.SNF_Amount),0) as SNF_Amount,coalesce(Amount,0)  as Amount from (
                    'select  max(xxx.doc_no) as doc_no,Vendor_CODE,SNo,trans_type,sum(Amount) as Amount from(
                    'select RefDocType,case when RefDocType ='VSP-COM' then 'EMP' else (case when RefDocType ='PRO-VFC' then 'PRO ADD.' else 'OTHER ADD.' end ) end as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,1 as   Show_FAT_SNF , case when RefDocType ='VSP-COM' then 1 else (case when RefDocType ='PRO-VFC' then 3 else 2 end ) end as SNo
                    'from TSPL_PAYMENT_PROCESS_CREDIT_NOTE   
                    'left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                    'left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
                    'where TSPL_VENDOR_INVOICE_HEAD.RefDocType not in ('VSP-DIT') and TSPL_VENDOR_INVOICE_HEAD.RefDocType= 'PRO-VFC' and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'),103)
                    'union all
                    'select RefDocType,case when RefDocType='VSP-QLT' then 'CBD' else case when RefDocType='MILK-REJ' then 'Rejection Ch.' else (case when RefDocType ='PRO-VFD' then 'PRO DED.' else 'OTHER DED.' end ) end end as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, 
                    ' TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Amount ,case when RefDocType='MILK-REJ' then 1 else TSPL_DEDUCTION_MASTER.Show_FAT_SNF  end Show_FAT_SNF, case when RefDocType='VSP-QLT' then 4 else case when RefDocType='MILK-REJ' then 5 else (case when RefDocType ='PRO-VFD' then 7 else 6 end ) end end as SNo
                    'from TSPL_PAYMENT_PROCESS_DEDUCTION 
                    'left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                    'left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.document_no=TSPL_VENDOR_INVOICE_HEAD.document_no and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1
                    'left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode
                    'left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                    ' where  (TSPL_DEDUCTION_MASTER.Show_FAT_SNF=1 and RefDocType='PRO-VFD'  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'),103)
                    ') 
                    ') xxx group by  Vendor_CODE,SNo,trans_type
                    ' ) as final 
                    ' left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no
                    'left outer join (
                    'select vsp_code,sum(FAT_Amount) as FAT_Amount,sum(SNF_Amount) as SNF_Amount from (select  isnull(TSPL_VENDOR_MASTER.Actual_charges,0) as Actual_charges,isnull (TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Handling_Charges_Amount,0) as Handling_Charges_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPI_Code,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address,     '11/02/2021'  as fromDate ,'20/02/2021'  as Todate ,'  '  as companyADD, 'Bhole Baba Milk Food Industries (Dholpur) Pvt. Ltd.'  as CompName,'BHBA'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  (Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0)) end as Standard_Rate
                    ',case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.Fat_ratio)/Price_Chart.FAT_Pers) as decimal(18,2)) end as Standard_FAT_Rate
                    ',case when TSPL_MILK_SRN_DETAIL.AMOUNT=0 then 0 else  Cast( (((Price_Chart.milk_rate+isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive_Rate,0))*Price_Chart.SNF_Ratio)/Price_Chart.SNF_Pers) as decimal(18,2)) end as Standard_SNF_Rate
                    ',TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT, TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Buffalo' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_VLC_MASTER_HEAD.Village_Code, TSPL_VILLAGE_MASTER.Village_Name, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type 
                    ',(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
                    ',TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT
                    ',TSPL_MILK_SRN_HEAD.VEHICLE_CODE
                    ',cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
                    ',TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
                    ',round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
                    ',cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
                    ',cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
                    ',TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
                    ',round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
                    ',cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
                    ',cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
                    ',(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply )  as QBD
                    ' from TSPL_MILK_PURCHASE_INVOICE_DETAIL  
                    ' Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
                    ' left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE 
                    ' Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  
                    '  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  
                    ' left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE 
                    ' left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE 
                    '  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE 
                    ' Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  
                    ' Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  
                    ' left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code 
                    ' left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  
                    ' left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code 
                    ' left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code 
                    ' left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  where 2=2   and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'),103)    
                    ' ) as pp group by VSP_CODE,VLC_Code ) as PaymentProcess on   PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code 
                    ' left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code 
                    ' left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code  left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code = TSPL_VLC_MASTER_HEAD.Village_Code 
                    ' left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no   where 2=2   and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('10/Jun/2022 11:59:59 PM'),103)   
                    ' and exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE)  )x group by vsp_code
                    ' )  Tab on tab.VSP_CODE=final.Vendor_CODE where   convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('01/Jun/2022 12:00:00 AM'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('10/Jun/2022 11:59:59 PM'),103)
                    ' ) PrvPRO_DATA group by Vendor_CODE) as TBL_PRO_DATA on TBL_PRO_DATA.Vendor_CODE = aa.[VSP Code] 
                    '                    "



                End If
            Else
                Dim strRejection As String = Nothing
                Dim strSRNQuery As String = Nothing
                Dim strRejectionQuery As String = Nothing
                If chkRejection.Checked = True OrElse chkOnlyRejection.Checked = True Then
                    strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
                Else
                    strRejection = ""
                End If
                'strSRNQuery = "Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount"
                'If objCommonVar.DisplayTypeInMilkReceipt Then
                '    strSRNQuery += ",Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
                '" Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," &
                '" Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
                '    strSRNQuery += ",TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type] "
                'Else
                '    strSRNQuery += ",Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
                '" Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," &
                '" Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
                '    strSRNQuery += ", Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type]"
                'End If
                'strSRNQuery += ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code]," &
                '" TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, " &
                '" Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
                '" TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code]," &
                '" TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
                '" TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], " &
                '" TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)]," &
                '" TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,  " &
                '" TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status]," &
                '" TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
                '" convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & strRejection & "  " &
                '" ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                '" ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " &
                '"  ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle " + Environment.NewLine +
                '" From TSPL_MILK_RECEIPT_DETAIL " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE" + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " &
                '" Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                '" left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
                '" Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" + Environment.NewLine +
                '" Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE" + Environment.NewLine +
                '" left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                '" Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" + Environment.NewLine +
                '" left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                '" Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
                '" And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) " + Environment.NewLine +
                '" And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " + Environment.NewLine +
                '" And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine +
                '"left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code" + Environment.NewLine +
                '"left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per" + Environment.NewLine +
                '" left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                '" where 2 = 2 "
                'strSRNQuery += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                '    strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
                'End If
                'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                '    strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
                'End If
                'If clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "Zero") = CompairStringResult.Equal Then
                '    strSRNQuery += " and  TSPL_MILK_SRN_DETAIL.AMOUNT = 0 "
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboSRNAmounType.SelectedValue), "NonZero") = CompairStringResult.Equal Then
                '    strSRNQuery += " and TSPL_MILK_SRN_DETAIL.AMOUNT <> 0 "
                'End If

                'If Not chkShowVLCUploaderData.Checked Then
                '    strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null "
                'End If
                'If clsCommon.myLen(cboMilkReceiveUOM.SelectedValue) > 0 Then
                '    strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.UOM_Code='" + clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue) + "'"
                'End If

                'If isShowTreeView Then
                '    Dim arr As List(Of String) = Nothing
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                '        arr = cbtMCCRouteVLCC.CheckedValue(1)
                '        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                '            strSRNQuery += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                '        Else
                '            Throw New Exception("Please select at least one MCC")
                '        End If
                '    End If
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                '        arr = cbtMCCRouteVLCC.CheckedValue(2)
                '        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                '            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(arr) + ")  "
                '        Else
                '            Throw New Exception("Please select at least one Route")
                '        End If
                '    End If
                '    '===============Update by Preeti Gupta Against Ticket No[BM00000008365]
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                '        arr = cbtMCCRouteVLCC.CheckedValue(3)
                '        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                '            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                '        Else
                '            Throw New Exception("Please select at least one VLC Code")
                '        End If
                '    End If
                'Else
                '    If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                '        strSRNQuery += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                '    Else
                '        strSRNQuery += "And TSPL_MILK_RECEIPT_HEAD.mcc_Code in (" & StrPermission & ")"
                '    End If
                '    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                '        strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                '    End If
                '    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                '        strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
                '    End If
                'End If
                Dim SetCowFatPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
                strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(txtFromDate.Value, txtToDate.Value, txtFromShift.Text, txtToShift.Text, clsCommon.myCstr(cboSRNAmounType.SelectedValue), StrPermission, arrMCC, arrRoute, arrVLC, clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue), strRejection, chkShowVLCUploaderData.Checked, SetCowFatPer)
                'strRejectionQuery = "  Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)], " & _
                '" Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)], " & _
                '" Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)], " & _
                '" Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)], " & _
                '" Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)], " & _
                '" Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)], "
                ''strRejectionQuery += " Case When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                'strRejectionQuery += " case when TSPL_MILK_REJECT_TYPE.Type is not null  then TSPL_MILK_REJECT_TYPE.Type When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                'strRejectionQuery += " TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], " &
                '" Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)], " &
                '" Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status], " &
                '" TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], " &
                '" TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status], " &
                '" TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , " &
                '" '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.description as RejectType, " &
                '" case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as RejectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  " + Environment.NewLine +
                '" ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                '" ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " + Environment.NewLine +
                '" ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,t1.Vehicle " + Environment.NewLine +
                '" From   TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
                '" left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                '" left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code" + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE " + Environment.NewLine +
                '" Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE " + Environment.NewLine +
                '"   " + Environment.NewLine +
                '" Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE " + Environment.NewLine +
                '"  left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                '" Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE " + Environment.NewLine +
                '" Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE " &
                '" And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) " &
                '" And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT " + Environment.NewLine +
                '" Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
                '" And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
                '" left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
                '" left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per " + Environment.NewLine +
                '" left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                '" left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type " &
                '" where 2=2  "
                'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                '    strRejectionQuery += " and Against_Reject_No <> ''"
                'End If
                'strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                'If clsCommon.CompairString(txtFromShift.Text, " E") = CompairStringResult.Equal Then
                '    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='M' then 3 else 2 end  )"
                'End If
                'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                '    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='E' then 3 else 2 end  )"
                'End If
                'If clsCommon.myLen(cboMilkReceiveUOM.SelectedValue) > 0 Then
                '    strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.UOM_Code='" + clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue) + "'"
                'End If
                'Dim arr1 As List(Of String) = Nothing
                'If isShowTreeView Then
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                '        arr1 = cbtMCCRouteVLCC.CheckedValue(1)
                '        If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                '            strRejectionQuery += "and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr1) + ") "
                '        Else
                '            Throw New Exception("Please select at least one MCC")
                '        End If
                '    End If
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                '        arr1 = cbtMCCRouteVLCC.CheckedValue(2)
                '        If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                '            strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(arr1) + ")  "
                '        Else
                '            Throw New Exception("Please select at least one Route")
                '        End If
                '    End If
                '    If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                '        arr1 = cbtMCCRouteVLCC.CheckedValue(3)
                '        If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                '            strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arr1) + ")  "
                '        Else
                '            Throw New Exception("Please select at least one VLC Code")
                '        End If
                '    End If
                'Else
                '    If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                '        strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                '    Else
                '        strRejectionQuery += " And TSPL_MILK_REJECT_HEAD.MCC_Code in (" & StrPermission & ") "
                '    End If
                '    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                '        strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                '    End If
                '    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                '        strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
                '    End If
                'End If
                'Dim SetCowFatPer As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
                strRejectionQuery = clsMilkRejectHead.GetMCCRegisterRejectionQuery(txtFromDate.Value, txtToDate.Value, txtFromShift.Text, txtToShift.Text, StrPermission, arrMCC, arrRoute, arrVLC, clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue), SetCowFatPer)

                If chkOnlyRejection.Checked = True Then
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strRejectionQuery & ") As final where 2=2 "
                ElseIf chkRejection.Checked = True Then
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "
                Else
                    qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                "final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc ,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)],final.[Buffalo FAT(%)],Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL, " &
                " final.EMP_Amount,final.TIP_Amount ,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+final.TIP_Amount-Service_Charge_Amount) as NetAmount,final.[SuperVisor Code] as [SuperVisor Code],final.Purchase_Order_No,final.Head_Load_Amount,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle   From ( " & strSRNQuery & " ) As final where 2=2  "
                End If
                If ChkDetailWise.Checked OrElse chkVLCWisePayable.Checked Then
                    If BulkExport = 4 Then
                        FinalQuery = qry
                    Else
                        FinalQuery = "" & qry & " order by final.[Doc Date],final.[Milk Receipt Code] ,final.[Sample No] "
                    End If
                    If ChkDetailWise.Checked = True AndAlso chkDairyMilkReportPrint.Checked = True Then
                        Dim strCompanyName As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.GSTINNo  from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' ")
                        Dim strCompanyGstinNo As String = clsDBFuncationality.getSingleValue("select  TSPL_COMPANY_MASTER.GSTINNo  from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' ")
                        Dim strFinYear As String = clsDBFuncationality.getSingleValue(" select  Fiscal_Name from TSPL_Fiscal_Year_Master where convert (date, '" + txtFromDate.Value + "',103) between [Start_Date] and End_Date ")
                        Dim qryPrint As String = " select  '" + strCompanyName + "' as Comp_Name, '" + strCompanyGstinNo + "' as Comp_GSTINNO , '" + strFinYear + "' as FinYear ,XXXQQQQ.[MCC Code],max(XXXQQQQ.[MCC Name]) as [MCC Name] , convert (varchar, XXXQQQQ.Date,103) as Date, XXXQQQQ.Shift,
                                                    sum(isnull(XXXQQQQ.[FAT(KG)_GM],0)) as [FAT(KG)_GM], sum(isnull([SNF(KG)_GM],0)) as [SNF(KG)_GM], sum (isnull([Milk Weight(KG)_GM],0)) as [Milk Weight(KG)_GM], 
                                                    sum(isnull(XXXQQQQ.[FAT(KG)_SOUR],0)) as [FAT(KG)_SOUR], sum(isnull([SNF(KG)_SOUR],0)) as [SNF(KG)_SOUR], sum (isnull([Milk Weight(KG)_SOUR],0)) as [Milk Weight(KG)_SOUR],
                                                    sum(isnull(XXXQQQQ.[FAT(KG)_CURD],0)) as [FAT(KG)_CURD], sum(isnull([SNF(KG)_CURD],0)) as [SNF(KG)_CURD], sum (isnull([Milk Weight(KG)_CURD],0)) as [Milk Weight(KG)_CURD], 
                                                    sum(isnull(XXXQQQQ.[FAT(KG)_GM],0)) + sum(isnull(XXXQQQQ.[FAT(KG)_SOUR],0)) +sum(isnull(XXXQQQQ.[FAT(KG)_CURD],0)) as [FAT(KG)_Total],
                                                    sum(isnull([SNF(KG)_GM],0)) + sum(isnull([SNF(KG)_SOUR],0)) + sum(isnull([SNF(KG)_CURD],0)) as [SNF(KG)_Total] ,
                                                    sum (isnull([Milk Weight(KG)_GM],0)) + sum (isnull([Milk Weight(KG)_SOUR],0)) + sum (isnull([Milk Weight(KG)_CURD],0)) as [Milk Weight(KG)_Total]
                                                    from (
                                                    select  XXXPPPP.[MCC Code],XXXPPPP.[MCC Name], XXXPPPP.Date, XXXPPPP.Shift,
                                                    case when XXXPPPP.RejectType = 'Good Milk' then  XXXPPPP.[FAT(KG)] else 0 end as [FAT(KG)_GM],case when XXXPPPP.RejectType = 'Good Milk' then   XXXPPPP.[SNF(KG)] else 0 end as [SNF(KG)_GM] ,case when XXXPPPP.RejectType = 'Good Milk' then   XXXPPPP.[Milk Weight(KG)] else 0 end [Milk Weight(KG)_GM],
                                                    case when XXXPPPP.RejectType = 'SOUR' then  XXXPPPP.[FAT(KG)] else 0 end as [FAT(KG)_SOUR],case when XXXPPPP.RejectType = 'SOUR' then   XXXPPPP.[SNF(KG)] else 0 end as [SNF(KG)_SOUR] ,case when XXXPPPP.RejectType = 'SOUR' then   XXXPPPP.[Milk Weight(KG)] else 0 end [Milk Weight(KG)_SOUR], 
                                                    case when XXXPPPP.RejectType = 'CURD' then  XXXPPPP.[FAT(KG)] else 0 end as [FAT(KG)_CURD],case when XXXPPPP.RejectType = 'CURD' then   XXXPPPP.[SNF(KG)] else 0 end as [SNF(KG)_CURD] ,case when XXXPPPP.RejectType = 'CURD' then   XXXPPPP.[Milk Weight(KG)] else 0 end [Milk Weight(KG)_CURD],
                                                    XXXPPPP.RejectType from  (
                                                    select PPPP.[MCC Code],PPPP.[MCC Name], PPPP.Date ,   Case When PPPP.Shift = 'Morning' Then 'M' Else 'E' End As Shift , [FAT(KG)] , [SNF(KG)] , [Milk Weight(KG)], Case when len ( isnull (RejectType,'')) <=0 then 'Good Milk' else  isnull (RejectType,'') end RejectType  from ( " + qry + "  ) PPPP ) XXXPPPP 
                                                    )XXXQQQQ group by [MCC Code], date , shift  order by convert (date,Date,103) asc, Shift desc"
                        Dim dtPrint As DataTable = clsDBFuncationality.GetDataTable(qryPrint)
                        If dtPrint Is Nothing OrElse dtPrint.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                        Else
                            gv.DataSource = Nothing
                            Dim frmCRV As New frmCrystalReportViewer()
                            frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDairyMilkReportPrint", "Dairy Milk Report")
                            frmCRV = Nothing

                        End If
                        Return
                    End If

                    ''richa MIL/01/02/19-000039 show empAmount in case of VLC Wise 
                ElseIf rbtnVLCWise.Checked Then
                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[Vendor Group Code],aa.[Vendor Group Desc],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.Head_Load_Amount, aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( " & Environment.NewLine &
                    " select xxx.* ," & Environment.NewLine &
                    "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                    " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                    "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                    " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                    " from (" & Environment.NewLine &
                    " select xx.*" & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                    "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],max(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,max (pp.[Vendor Group Code]) as [Vendor Group Code],max (pp.[Vendor Group Desc]) as [Vendor Group Desc] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                    " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                    " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                    " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                    " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],sum(Head_Load_Amount) as Head_Load_Amount,sum(PP.EMP_Amount ) as EMP_Amount,sum(PP.TIP_Amount) as TIP_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle  from (" & Environment.NewLine &
                    " " + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                    " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code] " & Environment.NewLine &
                    " )as xx" & Environment.NewLine &
                    " ) as xxx" & Environment.NewLine &
                    " ) as aa" & Environment.NewLine
                    If BulkExport <> 4 Then
                        FinalQuery += " order by [MCC Code],[Route Code],[Vlc Code] "
                    End If
                    ''richa MIL/01/02/19-000039 show empAmount in case of route wise 
                ElseIf chkRoutewise.Checked Then
                    If chkShiftWise.Checked = True Then
                        FinalQuery = "select "
                        If BulkExport = 3 Then
                            FinalQuery += " aa.Date,"
                        End If
                        FinalQuery += " aa.Shift,aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)],aa.[Buffalo SNF(%)]  ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount ,aa.Head_Load_Amount, aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle  from ( "
                        FinalQuery += " select xxx.* ,"
                        FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                        FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                        FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                        FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                        FinalQuery += " from ("
                        FinalQuery += " select xx.*"
                        FinalQuery += " from ( "
                        FinalQuery += "select case when pp.Shift='Morning' then 1 else 2 end as ShiftSeq"
                        If BulkExport = 3 Then
                            FinalQuery += " ,pp.Date"
                        End If
                        FinalQuery += ",pp.Shift,pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                        FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                        FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                        FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                        FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                        FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                        FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_AMOUNT) as EMP_AMOUNT,sum(TIP_Amount) as TIP_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                        FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                        FinalQuery += ") as  pp group by"
                        If BulkExport = 3 Then
                            FinalQuery += " pp.Date,"
                        End If
                        FinalQuery += " pp.[MCC Code],pp.[Route Code],Shift"
                        FinalQuery += " )as xx"
                        FinalQuery += " ) as xxx"
                        If BulkExport = 3 Or BulkExport = 4 Then
                            FinalQuery += " ) as aa  "
                        Else
                            FinalQuery += " ) as aa order by aa.[Route Code],ShiftSeq "
                        End If
                        ''richa MIL/01/02/19-000039 show empAmount in case of route Wise 
                    Else
                        FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount,aa.Head_Load_Amount , aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( " & Environment.NewLine &
                        " select xxx.* ," & Environment.NewLine &
                        "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                        " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                        "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                        " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                        " from (" & Environment.NewLine &
                        " select xx.*" & Environment.NewLine &
                        " from ( " & Environment.NewLine &
                        "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                        " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)]," & Environment.NewLine &
                        " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                        " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                        " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                        " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                        " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(Head_Load_Amount) as Head_Load_Amount,sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from (" & Environment.NewLine &
                        "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                        " ) as  pp group by pp.[MCC Code],pp.[Route Code]" & Environment.NewLine &
                        " )as xx" & Environment.NewLine &
                           " ) as xxx" & Environment.NewLine &
                        " ) as aa " & Environment.NewLine
                        If BulkExport <> 3 Then
                            If BulkExport <> 4 Then
                                FinalQuery += " order by [MCC Code],[Route Code] "
                            End If
                        End If

                    End If
                    ''richa MIL/01/02/19-000039 show empAmount in case of MCC Wise
                ElseIf ChkMCCWise.Checked Then
                    FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name] ,aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)]  ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount,aa.Head_Load_Amount , aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( " & Environment.NewLine &
                    " select xxx.* ," & Environment.NewLine &
                    "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                    " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                    "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                    " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                    " from (" & Environment.NewLine &
                    " select xx.*" & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                    "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                    " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                    " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                    " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                    " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_AMOUNT) as EMP_AMOUNT,sum(TIP_Amount) as TIP_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from (" & Environment.NewLine &
                    "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                    " ) as  pp group by pp.[MCC Code] " & Environment.NewLine &
                    " )as xx" & Environment.NewLine &
                    " ) as xxx" & Environment.NewLine &
                    " ) as aa" & Environment.NewLine
                    If BulkExport <> 4 Then
                        FinalQuery += " order by [MCC Code] "
                    End If

                ElseIf rbtnPlantWise.Checked Then
                    FinalQuery = "select aa.[Plant Code],aa.[Plant Name],aa.[MCC Code],aa.[MCC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid],aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)]  ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount,aa.Head_Load_Amount , aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( " & Environment.NewLine &
                    " select xxx.* ," & Environment.NewLine &
                    "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                    " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                    "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                    " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                    " from (" & Environment.NewLine &
                    " select xx.*" & Environment.NewLine &
                    " from ( " & Environment.NewLine &
                    "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)]," & Environment.NewLine &
                    " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                    " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                    " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                    " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                    " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_AMOUNT) as EMP_AMOUNT,sum(TIP_Amount) as TIP_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from (" & Environment.NewLine &
                    "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                    " ) as  pp group by pp.[Plant Code],pp.[MCC Code] " & Environment.NewLine &
                    " )as xx" & Environment.NewLine &
                    " ) as xxx" & Environment.NewLine &
                    " ) as aa" & Environment.NewLine
                    If BulkExport <> 4 Then
                        FinalQuery += " order by [Plant Code],[MCC Code] "
                    End If

                End If
            End If
            '' bulk export
            If BulkExport = 1 Then
                transportSql.BulkExport("MCC_Milk_Register", FinalQuery, "", "csv")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("MCC_Milk_Register", FinalQuery, "", "xls")
                Exit Sub
            ElseIf BulkExport = 3 Then
                Dim FinalQueryFinal As String = "  select kkkk.Date,max(kkkk.[Doc Date]) as [Doc Date],kkkk.[MCC Code],max(kkkk.[MCC Name]) as[MCC Name],max(kkkk.[Plant Code])  as [Plant Code],max(kkkk.[Plant Name] )  as [Plant Name] ,kkkk.[Route Code],max(kkkk.[Route Name]) as [Route Name] ,sum(kkkk.Mrn_qty) as Mrn_qty , sum(kkkk.eve_qty) as eve_qty ,sum (kkkk.Mrn_Fat) as Mrn_Fat ,sum(kkkk.eve_Fat) as eve_Fat,sum(kkkk.Mrn_Snf) as Mrn_Snf ,sum(kkkk.eve_Snf) as eve_Snf,( sum (kkkk.Mrn_qty) + sum (kkkk.eve_qty)) as Total_Qty, (sum (kkkk.Mrn_Fat) +sum (kkkk.eve_Fat))/2 as Total_Fat, (sum (kkkk.Mrn_Snf )+sum (kkkk.eve_Snf))/2 as Total_Snf  from (  select tttt.Date , convert (varchar, tttt.Date,103) as [Doc Date],tttt.[MCC Code], max (tttt.[MCC Name]) as [MCC Name],max(tttt.[Plant Code])  as [Plant Code],max(tttt.[Plant Name] )  as [Plant Name] ,tttt.[Route Code],max(tttt.[Route Name]) as [Route Name],sum (tttt.Mrn_qty) as Mrn_qty, sum (tttt.eve_qty) as eve_qty , case when sum(tttt.mrn_qty) =0 then 0 else  round((sum(tttt.Mrn_Fat)*100)/sum(tttt.mrn_qty),4) end as Mrn_Fat ,   case when sum(tttt.eve_qty) =0 then 0 else round((sum(tttt.eve_Fat)*100) / sum(tttt.eve_qty),4) end as eve_Fat ,   case when sum(tttt.mrn_qty) =0 then 0 else  round((sum(tttt.Mrn_Snf)*100)/sum(tttt.mrn_qty),4) end as Mrn_Snf ,  case when sum(tttt.eve_qty) =0 then 0 else  round((sum(tttt.eve_Snf)*100)/sum(tttt.eve_qty),4) end as eve_Snf   from (  select Date, [MCC Code],max([MCC Name]) as [MCC Name],max([Plant Code])  as [Plant Code],max([Plant Name] )  as [Plant Name] ,[Route Code],max([Route Name]) as [Route Name],  case when ([Shift])='Evening' then sum([Milk Weight(LTR)]) else 0 end as eve_qty, case when (shift)='Morning' then sum([Milk Weight(LTR)]) else 0 end as Mrn_qty ,  case when [Shift]='Evening' then sum( [FAT(KG)]) else 0 end as eve_Fat  ,case when shift='Morning' then sum([FAT(KG)]) else 0 end as Mrn_Fat,    case when [Shift]='Evening' then sum( [SNF(KG)]) else 0 end as eve_Snf,   case when shift='Morning' then sum([SNF(KG)]) else 0 end as Mrn_Snf     from (select finallll.Date,finallll.[MCC Code],max( finallll.[MCC Name]) as [MCC Name],max(finallll.[Plant Code])  as [Plant Code],max(finallll.[Plant Name] )  as [Plant Name]  ,finallll.[Route Code],max(finallll.[Route Name]) as [Route Name], finallll.Shift, max (finallll.[Milk Weight(LTR)]) as [Milk Weight(LTR)] , max (finallll.[FAT(KG)])  as [FAT(KG)] ,max(finallll.[SNF(KG)]) as [SNF(KG)],max(finallll.[Head_Load_Amount]) as [Head_Load_Amount]  from (  " &
                                                "  " + FinalQuery + "  " &
                                                " )finallll group by finallll.Date,[MCC Code],[Route Code],finallll.Shift ) pppp group by pppp.Date,pppp.[MCC Code],pppp.[Route Code],pppp.Shift) tttt group by tttt.Date ,tttt .[MCC Code],tttt.[Route Code]  ) kkkk group by kkkk.Date ,kkkk.[MCC Code],kkkk.[Route Code]  "

                dt = clsDBFuncationality.GetDataTable(FinalQueryFinal)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "rptMccMilkRegisterRoutShiftWise", "MCC Milk Register Route/Shift wise Report", "Address.rpt")
                frmCRV = Nothing
                Exit Sub
            ElseIf BulkExport = 4 Then
                Dim QtyforMccDetailsPrart1 As String = " select '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "'  as FromDate,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' as ToDate , XXXX.[MCC Code] , Max(XXXX.[MCC Name]) as [MCC Name],max(XXXX.[Plant Code])  as [Plant Code],max(XXXX.[Plant Name] )  as [Plant Name] ,XXXX.[Doc Date],sum( XXXX.[Milk Weight]) as [Milk Weight]  ,sum(XXXX.[Milk Weight(KG)] ) as [Milk Weight(KG)] , sum(XXXX.[Milk Weight(LTR)]) as [Milk Weight(LTR)] ,Sum(XXXX.[FAT(KG)]) as [FAT(KG)], sum(XXXX.[SNF(KG)]) as [SNF(KG)] ,sum(XXXX.[SRN Amount]) as NetAmount,sum(XXXX.Head_Load_Amount) as Head_Load_Amount  from ( " &
                                                 " " + FinalQuery + " " &
                                                 "  )XXXX group By XXXX.[Doc Date], XXXX.[MCC Code] order by convert (date,  XXXX.[Doc Date] , 103) , XXXX.[MCC Code] "

                Dim QtyforMccDetailsPrart2 As String = " Select case when  XXXFinal.[Milk Type]  = 'C' then 'Cow' else  'Buffalo' end as [Milk Type], Cast ((cast ( XXXFinal.TFAT as decimal(18,2)) * 100 / XXXFinal.QTY ) as decimal(10,2)) as FAT ,Cast( (cast ( XXXFinal.TSNF as decimal(18,2)) * 100 / XXXFinal.QTY )as decimal(10,2)) as SNF ,XXXFinal.QTY , XXXFinal.TFAT ,XXXFinal.TSNF , XXXFinal.NetAmount  from ( select XXX.[Milk Type]  ,Sum ( XXX.[Cow Milk Qty (KG)])  + sum (XXX.[Buffalo Milk Qty (KG)]) as QTY, sum( XXX.[Cow FAT (KG)]) + sum(XXX.[Buffalo FAT (KG)]) as TFAT , sum (XXX.[Cow SNF (KG)]) + Sum(XXX.[Buffalo SNF (KG)]) as TSNF ,sum( XXX.[SRN Amount]) as NetAmount,sum( XXX.Head_Load_Amount) as Head_Load_Amount   from ( " &
                                                       " " + FinalQuery + " " &
                                                       "  ) XXX group by XXX.[Milk Type] ) XXXFinal "
                Dim dtPart1 As DataTable = clsDBFuncationality.GetDataTable(QtyforMccDetailsPrart1)
                Dim dtPart2 As DataTable = clsDBFuncationality.GetDataTable(QtyforMccDetailsPrart2)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtPart1, clsERPFuncationality.CompanyAddresShowinHeader(), "rptMccMilkRegisterDetail", "MCC Milk Register Report", "Address.rpt", "rptMccMilkRegisterDetailTypeWise.rpt", dtPart2)
                frmCRV = Nothing
                Exit Sub
            End If


            dt = clsDBFuncationality.GetDataTable(FinalQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            ElseIf chkVLCWisePayable.Checked Then
                'For ii As Integer = 0 To dt.Rows.Count - 1
                '    Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive("", clsCommon.myCstr() objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
                'Next
            End If
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            

            gv.ShowGroupPanel = True
            gv.MasterTemplate.AutoExpandGroups = True

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            gv.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = MyBase.Form_ID
        If ChkMCCWise.Checked Then
            ReportID += "M"
        ElseIf chkRoutewise.Checked Then
            ReportID += "R"
        ElseIf rbtnVLCWise.Checked Then
            ReportID += "V"
        ElseIf ChkDetailWise.Checked Then
            ReportID += "D"
        ElseIf rbtnPlantWise.Checked Then
            ReportID += "P"
        ElseIf rbtnPlantWise.Checked Then
            ReportID += "PWPS"
        ElseIf rdoVLCWisePaymentSummary.Checked Then
            ReportID += "VLCWPS"
        End If
        If chkRejection.Checked = False AndAlso chkShiftWise.Checked = False AndAlso chkOnlyRejection.Checked = False Then
            ReportID += "1"
        End If
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            'Dim ReportID As String = GetReportID()
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FrmMCCMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.U Then
            If chkShowVLCUploaderData.Visible Then
                chkShowVLCUploaderData.Visible = False
            Else
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.MilkProcurementUploader
                pwd.strType = clsFixedParameterType.MilkProcurementUploader
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    chkShowVLCUploaderData.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        LoadData()

    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        ' Dim ReportID As String = GetReportID()
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


            ''If rbtnMCCRouteVLCCSelect.IsChecked Then
            'Dim arr As List(Of String)
            'If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(1)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            'If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(2)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            'If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
            '    arr = cbtMCCRouteVLCC.CheckedText(3)
            '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
            '    End If
            'End If
            ''End If

            Dim arr As List(Of String)
            If isShowTreeView Then
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("MCC Milk Register", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("MCC Milk Register", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadData(1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadData(2)
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            'If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            '    txtMCC.Focus()
            '    Throw New Exception("Please select MCC")
            'End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            'If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
            '    txtRoute.Focus()
            '    Throw New Exception("Please select at least route")
            'End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If

            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadData(3)
    End Sub

    Private Sub btnPrintMccDetails_Click(sender As Object, e As EventArgs) Handles btnPrintMccDetails.Click
        ' Ticket No : BHA/15/08/18-000428 By Prabhakar 
        LoadData(4)
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MCCMilkRegister & "'"))


            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String)
            If isShowTreeView Then
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If
            End If

            'End If


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            clsCommon.MyExportToExcelGrid(Me.Text, gv, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MCCMilkRegister & "'"))

            Dim arr As List(Of String)
            If isShowTreeView Then
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbPlantWisePaymentSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPlantWisePaymentSummary.CheckedChanged
        btnLock.Visible = rdbPlantWisePaymentSummary.Checked
    End Sub

    Private Sub btnLock_Click(sender As Object, e As EventArgs) Handles btnLock.Click
        Try
            'If RadGroupBox1.Enabled = True Then
            '    Throw New Exception("Please first press go button")
            'End If
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select at least one MCC")
            End If

            Dim sQuery As String = "select TSPL_MCC_MASTER.Payment_Cycle,max(TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE) as Type,max(TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE) as Value,max(case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * 30  end) as Pc_Value   from TSPL_MCC_MASTER " + Environment.NewLine +
            "left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.payment_cycle" + Environment.NewLine +
            "where MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") group by Payment_Cycle"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set payment cycle in Mcc master")
            End If
            If dt.Rows.Count > 1 Then
                Throw New Exception("All MCC Should have same payment cycle")
            End If
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                Dim today As Date = txtFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 1, DayOfWeek.Sunday, IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 2, DayOfWeek.Monday, IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 3, DayOfWeek.Tuesday, IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 4, DayOfWeek.Wednesday, IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 5, DayOfWeek.Thursday, IIf(clsCommon.myCdbl(dt.Rows(0)("Value")) = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                Dim CalcFromDate As Date = today.AddDays(-dayDiff)
                Dim CalcToDate As Date = txtFromDate.Value.AddDays(6)
                If clsCommon.GetDateWithEndTime(CalcFromDate) <> clsCommon.GetDateWithEndTime(txtFromDate.Value) Then
                    Throw New Exception("Invalid From date.It should be [" + clsCommon.GetPrintDate(CalcFromDate, "dd/MM/yyyy") + "]")
                End If
                If clsCommon.GetDateWithEndTime(CalcToDate) <> clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                    Throw New Exception("Invalid To date.It should be [" + clsCommon.GetPrintDate(CalcToDate, "dd/MM/yyyy") + "]")
                End If
            Else
                Dim PaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    Throw New Exception("Invalid From date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                End If
                Dim CalcToDate As Date = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    CalcToDate = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    CalcToDate = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                If clsCommon.GetDateWithEndTime(CalcToDate) <> clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                    Throw New Exception("Invalid To date.It should be [" + clsCommon.GetPrintDate(CalcToDate, "dd/MM/yyyy") + "]")
                End If
            End If
            Dim frm As FrmPaymentDetail = New FrmPaymentDetail()
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.desc = "Against Payment Process "
            frm.ShowDialog()
            If frm.btnOkClicked Then
                Dim Arr As New List(Of clsMCCPaymentCycleLockForScheduler)
                For Each strMCC As String In txtMCC.arrValueMember
                    Dim obj As New clsMCCPaymentCycleLockForScheduler
                    obj.MCC_Code = strMCC
                    obj.From_Date = txtFromDate.Value
                    obj.To_Date = txtToDate.Value
                    obj.Bank_Code = frm.bankCode
                    obj.Payment_Mode = frm.paymentMode
                    Arr.Add(obj)
                Next
                clsMCCPaymentCycleLockForScheduler.SaveData(Arr)
                clsCommon.MyMessageBoxShow("Successfully Locked")
                Arr = Nothing
            End If
            frm = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ChkOnlyRejection_CheckStateChanged(sender As Object, e As EventArgs) Handles chkOnlyRejection.CheckStateChanged
        If chkOnlyRejection.Checked = True Then
            chkRejection.Checked = False
        End If
    End Sub

    Private Sub ChkRejection_CheckStateChanged(sender As Object, e As EventArgs) Handles chkRejection.CheckStateChanged
        If chkRejection.Checked = True Then
            chkOnlyRejection.Checked = False
            If ChkDetailWise.Checked = True Then
                chkDairyMilkReportPrint.Enabled = True
            Else
                chkDairyMilkReportPrint.Enabled = False
                chkDairyMilkReportPrint.Checked = False
            End If

        Else
            chkDairyMilkReportPrint.Enabled = False
            chkDairyMilkReportPrint.Checked = False
        End If
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If rbtnPlantWise.Checked = True Then
                If Not arrBack.Contains("Plant Wise") Then
                    arrBack.Add("Plant Wise")
                End If

                Dim tempPlantcode As String = clsCommon.myCstr(gv.CurrentRow.Cells("Plant Code").Value)
                arrPlant = New ArrayList()
                arrPlant.Add(tempPlantcode)

                ChkMCCWise.Checked = True
                LoadData()
                Dim filter As New FilterDescriptor()
                filter.PropertyName = "Plant Code"
                filter.[Operator] = FilterOperator.IsEqualTo
                filter.Value = tempPlantcode
                filter.IsFilterEditor = True
                gv.FilterDescriptors.Add(filter)
            ElseIf ChkMCCWise.Checked = True Then
                If Not arrBack.Contains("Mcc Wise") Then
                    arrBack.Add("Mcc Wise")
                End If
                arrMcc = New ArrayList()
                arrMcc = txtMCC.arrValueMember
                chkRoutewise.Checked = True
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("MCC Code").Value))
                txtMCC.arrValueMember = tmp
                LoadData()
            ElseIf chkRoutewise.Checked = True Then
                If Not arrBack.Contains("Route Wise") Then
                    arrBack.Add("Route Wise")
                End If
                arrRoute = New ArrayList()
                arrRoute = txtRoute.arrValueMember
                rbtnVLCWise.Checked = True
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Route Code").Value))
                txtRoute.arrValueMember = tmp
                LoadData()
            ElseIf rbtnVLCWise.Checked = True Then
                If Not arrBack.Contains("VLC Wise") Then
                    arrBack.Add("VLC Wise")
                End If
                arrVLC = New ArrayList()
                arrVLC = txtVLC.arrValueMember
                ChkDetailWise.Checked = True
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("VLC Code").Value))
                txtVLC.arrValueMember = tmp
                LoadData()
            ElseIf ChkDetailWise.Checked = True Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(gv.CurrentRow.Cells("SRN No").Value))
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnPlantWise.Checked = True Then

            ElseIf ChkMCCWise.Checked = True AndAlso arrBack.Contains("Plant Wise") Then
                arrBack.Remove("Plant Wise")
                rbtnPlantWise.Checked = True
                LoadData()

            ElseIf chkRoutewise.Checked = True AndAlso arrBack.Contains("Mcc Wise") Then
                arrBack.Remove("Mcc Wise")
                ChkMCCWise.Checked = True
                txtMCC.arrValueMember = arrMcc
                LoadData()
                If arrPlant IsNot Nothing AndAlso arrPlant.Count > 0 AndAlso arrBack.Contains("Plant Wise") Then
                    Dim filter As New FilterDescriptor()
                    filter.PropertyName = "Plant Code"
                    filter.[Operator] = FilterOperator.IsEqualTo
                    filter.Value = arrPlant.Item(0)
                    filter.IsFilterEditor = True
                    gv.FilterDescriptors.Add(filter)
                End If

            ElseIf rbtnVLCWise.Checked = True AndAlso arrBack.Contains("Route Wise") Then
                arrBack.Remove("Route Wise")
                chkRoutewise.Checked = True
                txtRoute.arrValueMember = arrRoute

                LoadData()
            ElseIf ChkDetailWise.Checked = True AndAlso arrBack.Contains("VLC Wise") Then
                arrBack.Remove("VLC Wise")
                rbtnVLCWise.Checked = True
                txtVLC.arrValueMember = arrVLC

                LoadData()
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ChkDetailWise_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDetailWise.CheckedChanged
        If ChkDetailWise.Checked = True Then
            chkDairyMilkReportPrint.Enabled = True
        Else
            chkDairyMilkReportPrint.Enabled = False
            chkDairyMilkReportPrint.Checked = False
        End If
    End Sub


End Class
