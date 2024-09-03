Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmMCCSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim tmpValLoad As Boolean = True
    'preeti gupta ticket no.[BM00000004218]
    Public ReportLevel As Integer = 0
    Public strCurrentGrp As String = Nothing
    Public Frm_Date As Date
    Dim arrLoc As String = Nothing
    Dim StrPermission As String

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MccSummaryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub







    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        cboType.Text = "Mcc Wise"
        'rbtnMCCRouteVLCCAll.IsChecked = True
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadShiftFrom()
        LoadShiftTo()
        LoadMilkReceiveUOM()
        Reset()
        If ReportLevel = 1 Then
            Dim arr As New ArrayList()
            'cbtMCCRouteVLCC.CheckedValue=arr
            txtFromDate.Value = Frm_Date
            txtToDate.Value = Frm_Date
            Load_Report()
        End If
    End Sub

    Sub LoadMilkReceiveUOM()
        Dim qry As String = " SELECT '' AS Code,'Select...' as Name union SELECT 'KG' AS Code, 'KG' as Name union SELECT 'LTR' AS Code, 'LTR' as Name "
        cboMilkReceiveUOM.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboMilkReceiveUOM.ValueMember = "Code"
        cboMilkReceiveUOM.DisplayMember = "Name"
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
    '===================================================================


    Public Sub Load_Report()
        Try
            Dim sQuery As String = String.Empty

            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If ReportLevel = 1 Then
                sQuery = " Select TSPL_MILK_SRN_HEAD.* From (Select TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code],      TSPL_MILK_SRN_HEAD.MCC_CODE As MCC," _
                    & " TSPL_MCC_MASTER.MCC_NAME As      [MCC Name], Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Doc_Date ,TSPL_MILK_SRN_HEAD.DOC_DATE As Date," _
                    & " Case        When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening'      End As Shift, TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code]" _
                    & " ,      TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name],      TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code],      TSPL_MILK_SRN_HEAD.VSP_CODE " _
                    & " As [VSP Code],      TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],      TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code],      TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code]," _
                    & " TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name],      TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],      TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans],     " _
                    & " TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight],      TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)],      TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR " _
                    & " As [Milk Weight(LTR)],      TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As      [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.FAT_PER * " _
                    & " TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [FAT(KG)],      Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.SNF_PER *      TSPL_MILK_SRN_DETAIL.ACC_Qty / 100)" _
                    & " As [SNF(KG)],      TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No],      Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount],    " _
                    & " TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As      [SRN Qty]   From TSPL_MILK_SRN_DETAIL Left Outer Join TSPL_MILK_SRN_HEAD     " _
                    & " On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE   LEFT OUTER JOIN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No " _
                    & "    Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL  On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =   " _
                    & " TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join      TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE        = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " _
                    & " Left Outer Join      TSPL_MCC_MASTER        On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE      Left Outer Join TSPL_VLC_MASTER_HEAD On " _
                    & " TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_SRN_HEAD.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER        On TSPL_VENDOR_MASTER.Vendor_Code = " _
                    & " TSPL_MILK_SRN_HEAD.VSP_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =        TSPL_MILK_SRN_HEAD.ROUTE_CODE  " _
                    & " Left Outer Join      TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =        TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " _
                    & " ) As TSPL_MILK_SRN_HEAD"
            Else


                Dim strBaseQry As String = clsMilkRejectHead.GetMCCRegisterQuery(txtFromDate.Value, txtToDate.Value, clsCommon.myCstr(txtFromShift.SelectedValue), clsCommon.myCstr(txtToShift.SelectedValue), "", "", Nothing, txtMCC.arrValueMember, txtRoute.arrValueMember, txtVLC.arrValueMember, clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue), "", Nothing, False, Nothing)
                If chkShiftWise.Checked Then
                    sQuery = "Select  '' as [Total],[MCC Code] As MCC,max([MCC Name]) As [MCC Name],max([Doc Date]) As DocumentDate ,max([Doc Date]) As  [Doc_Date], Shift,    Sum([Milk Weight]) As [Milk Weight],      Sum([Milk Weight(KG)]) As [Milk Weight(KG)],     Sum([Milk Weight(LTR)]) As [Milk Weight(LTR)]      , Sum([FAT(KG)]) As [FAT(KG)],    Sum( [SNF(KG)]) As [SNF(KG)], Sum([FAT(LTR)]) As [FAT(LTR)] , Sum([SNF(LTR)]) As [SNF(LTR)] , Sum(NET_AMOUNT) As [Value]   from(" + strBaseQry + "  )XX group by [MCC Code],Date,Shift order by [MCC Code],Date,Shift desc"
                Else
                    sQuery = "Select  '' as [Total],[MCC Code] As MCC,max([MCC Name]) As [MCC Name],max([Doc Date]) As DocumentDate ,max([Doc Date]) As  [Doc_Date],     Sum([Milk Weight]) As [Milk Weight],      Sum([Milk Weight(KG)]) As [Milk Weight(KG)],     Sum([Milk Weight(LTR)]) As [Milk Weight(LTR)]      , Sum([FAT(KG)]) As [FAT(KG)],    Sum( [SNF(KG)]) As [SNF(KG)], Sum([FAT(LTR)]) As [FAT(LTR)] , Sum([SNF(LTR)]) As [SNF(LTR)] , Sum(NET_AMOUNT) As [Value]   from(" + strBaseQry + " )XX group by [MCC Code],Date order by [MCC Code],Date "
                End If
            End If

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                'If ReportLevel <> 1 Then
                FormatGrid()
                'End If
                If btnReferesh = False Then
                    'If rdbRate.IsChecked = True Then
                    '    'MilkProcurementReportViewer.funreport(dtgv, "MCCShiftReport(RouteWise)", "Milk Shift Report (Route Wise)")
                    '    MilkProcurementReportViewer.funsubreportWithdt(dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "MCCShiftReport(RouteWise)", "Milk Shift Report (Route Wise)", "Address.rpt")
                    'Else
                    '    'MilkProcurementReportViewer.funreport(dtgv, "MCCShiftReport(RouteWise)RateAndAmount", "Milk Shift Report (Route Wise)")
                    '    MilkProcurementReportViewer.funsubreportWithdt(dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "MCCShiftReport(RouteWise)RateAndAmount", "Milk Shift Report (Route Wise)", "Address.rpt")
                    'End If

                End If

                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControl(False)
            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()

        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        If ReportLevel <> 1 Then
            gv.Columns("MCC").IsVisible = True
            gv.Columns("MCC").Width = 150
            gv.Columns("MCC").HeaderText = " Mcc Code"



            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 200
            gv.Columns("MCC Name").HeaderText = " MCC Name"

            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 100
            gv.Columns("Doc_Date").HeaderText = " Date"
            gv.Columns("Doc_Date").FormatString = "{0:d}"
            If chkShiftWise.Checked = True Then
                gv.Columns("Shift").IsVisible = True
                gv.Columns("Shift").Width = 100
                gv.Columns("Shift").HeaderText = "Shift"
            End If

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = " Milk Weight"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = " Milk Weight(LTR)"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = " Milk Weight(KG)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 120
            gv.Columns("FAT(KG)").HeaderText = "FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 120
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"
            If chkShiftWise.Checked = True Then
                gv.Columns("FAT(LTR)").IsVisible = True
                gv.Columns("FAT(LTR)").Width = 120
                gv.Columns("FAT(LTR)").HeaderText = "FAT(LTR)"

                gv.Columns("SNF(LTR)").IsVisible = True
                gv.Columns("SNF(LTR)").Width = 120
                gv.Columns("SNF(LTR)").HeaderText = "SNF(LTR)"
            End If


            gv.Columns("Value").IsVisible = True
            gv.Columns("Value").Width = 150
            gv.Columns("Value").HeaderText = "Value"

        Else
            gv.Columns("Milk Receipt Code").IsVisible = True
            gv.Columns("Milk Receipt Code").Width = 150
            gv.Columns("Milk Receipt Code").HeaderText = "Milk Receipt Code"

            gv.Columns("MCC").IsVisible = True
            gv.Columns("MCC").Width = 150
            gv.Columns("MCC").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 150
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 150
            gv.Columns("Doc_Date").HeaderText = "Doc Date"

            'gv.Columns("Doc Date").IsVisible = False
            'gv.Columns("Doc Date").Width = 0
            'gv.Columns("Doc Date").HeaderText = "DocDate"

            gv.Columns("Shift").IsVisible = True
            gv.Columns("Shift").Width = 150
            gv.Columns("Shift").HeaderText = "Shift"

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 150
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 150
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vehicle Code").IsVisible = True
            gv.Columns("Vehicle Code").Width = 150
            gv.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            gv.Columns("VSP Code").IsVisible = True
            gv.Columns("VSP Code").Width = 150
            gv.Columns("VSP Code").HeaderText = "VSP Code"

            gv.Columns("VSP Name").IsVisible = True
            gv.Columns("VSP Name").Width = 150
            gv.Columns("VSP Name").HeaderText = "VSP Name"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 150
            gv.Columns("Vlc Code").HeaderText = "Vlc Code"

            gv.Columns("Vlc Uploader Code").IsVisible = True
            gv.Columns("Vlc Uploader Code").Width = 150
            gv.Columns("Vlc Uploader Code").HeaderText = "Vlc Uploader Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 150
            gv.Columns("VLC Name").HeaderText = "VLC Name"

            gv.Columns("Sample No").IsVisible = True
            gv.Columns("Sample No").Width = 150
            gv.Columns("Sample No").HeaderText = "Sample No"

            gv.Columns("No Of Cans").IsVisible = True
            gv.Columns("No Of Cans").Width = 150
            gv.Columns("No Of Cans").HeaderText = "No Of Cans"

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 150
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 150
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 150
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 150
            gv.Columns("FAT(%)").HeaderText = "FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 150
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 150
            gv.Columns("FAT(KG)").HeaderText = "FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 150
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"

            gv.Columns("SRN No").IsVisible = True
            gv.Columns("SRN No").Width = 150
            gv.Columns("SRN No").HeaderText = "SRN No"

            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 150
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("SRN Rate").IsVisible = True
            gv.Columns("SRN Rate").Width = 150
            gv.Columns("SRN Rate").HeaderText = "SRN Rate"

            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 150
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"

        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item8 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item3 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("RATE", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        If ReportLevel <> 1 Then
            Dim item7 As New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            '================
            Dim item4FAT_LTR As New GridViewSummaryItem("FAT(LTR)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4FAT_LTR)
            Dim item4SNF_LTR As New GridViewSummaryItem("SNF(LTR)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4SNF_LTR)
            '================

        Else
            Dim item7 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item9 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)

        End If

        If clsCommon.CompairString(cboType.Text, "Mcc Wise") = CompairStringResult.Equal Then
            If ReportLevel <> 1 Then
                gv.Columns("DocumentDate").IsVisible = False
                gv.Columns("DocumentDate").Width = 0

                gv.Columns("Total").IsVisible = False
                gv.Columns("Total").Width = 0
                If ChkGrandtotal.Checked Then
                    gv.GroupDescriptors.Add(New GridGroupByExpression("Total as Item format ""{0}: {1}"" Group By Total"))
                End If
            End If
            If Not ChkGrandtotal.Checked Then
                gv.GroupDescriptors.Add(New GridGroupByExpression("MCC as Item format ""{0}: {1}"" Group By MCC"))
            End If
        Else
            If ReportLevel <> 1 Then
                gv.Columns("DocumentDate").IsVisible = True
                gv.Columns("DocumentDate").Width = 120
                gv.Columns("DocumentDate").HeaderText = "Document Date"

                gv.Columns("Total").IsVisible = False
                gv.Columns("Total").Width = 0

                If ChkGrandtotal.Checked Then
                    gv.GroupDescriptors.Add(New GridGroupByExpression("Total as Item format ""{0}: {1}"" Group By Total"))
                End If
            End If
            If Not ChkGrandtotal.Checked Then
                gv.GroupDescriptors.Add(New GridGroupByExpression("Doc_Date as Item format ""{0}: {1}"" Group By Doc_Date"))
            End If
        End If

        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_UPLOADER as Item format ""{0}: {1}"" Group By VLC_UPLOADER"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub


    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        GetReportID()
        Load_Report()
        tmpValLoad = False
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If ChkGrandtotal.Checked Then
            VarID += "_G"
        ElseIf chkShiftWise.Checked Then
            VarID += "_S"
        End If

        If clsCommon.CompairString(cboType.SelectedItem.Value, "Mcc Wise") = CompairStringResult.Equal Then
            VarID += "_MW"
        ElseIf clsCommon.CompairString(cboType.SelectedItem.Value, "Date Wise") = CompairStringResult.Equal Then
            VarID += "_R"
        End If
        gv.VarID = VarID

    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    'Sub Export()
    '    'If gv.Rows.Count > 0 Then
    '    '    ExportToExcel()
    '    'Else
    '    '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '    'End If
    '    Dim arr As New List(Of String)()
    '    arr.Add()
    '    arr.Add("Date Range From :  " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To : " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

    '    If gv1.Rows.Count > 0 Then
    '        clsCommon.MyExportToExcelGrid("Milk Shift Report (Route Wise)", gv1, arr, "Summary")
    '    Else
    '        clsCommon.MyMessageBoxShow("No data found.")
    '    End If
    'End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If exporter = EnumExportTo.Excel Then
                    arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " " + clsCommon.myCstr(txtFromShift.SelectedValue) + "  " + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " " + clsCommon.myCstr(txtToShift.SelectedValue))
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
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
                If ReportLevel <> 1 Then
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid("Milk Summary Report", gv, arrHeader, Me.Text)
                    Else
                        clsCommon.MyExportToPDF("Milk Summary Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                Else
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid("Milk Summary Detail Report", gv, arrHeader, Me.Text)
                    Else
                        clsCommon.MyExportToPDF("Milk Summary Detail Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub FrmMCCSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            '============================================
            'Dim frm As New FrmMCCSummary
            'frm.SetUserMgmt(clsUserMgtCode.MccSummaryReport)
            'frm.ReportLevel = 1
            'frm.Frm_Date = clsCommon.myCstr(gv.CurrentRow.Cells("Doc_Date").Value)
            'frm.strCurrentGrp = clsCommon.myCstr(gv.CurrentRow.Cells("Mcc").Value)
            'frm.WindowState = FormWindowState.Maximized
            'frm.Show()
            '==================================================================================================
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.ExciseSummary1 & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCSMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
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

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCSRUT", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Secretary Code],TSPL_VENDOR_MASTER.Vendor_Name as [Secretary Name] from TSPL_VLC_MASTER_HEAD 
left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1'"
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCSVLC", qry, "DCS Code", "DCS Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
