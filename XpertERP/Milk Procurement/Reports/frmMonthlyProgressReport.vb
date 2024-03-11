Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class frmMonthlyProgressReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim tmpValLoad As Boolean = True
    'preeti gupta ticket no.[BM00000004218]
    Public ReportLevel As Integer = 0
    Public strCurrentGrp As String = Nothing
    Public Frm_Date As Date
    Dim arrLoc As String = Nothing

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

        'btnExportToExcel.Visible = MyBase.isExport

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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageViewPage2.Item.Visibility = ElementVisibility.Hidden
        Reset()
       
    End Sub
    Public Sub Load_Report()
        Try
            If clsCommon.myLen(fndSuperVisorCode.Value) <= 0 Then
                fndSuperVisorCode.Focus()
                fndSuperVisorCode.Select()
                'common.clsCommon.MyMessageBoxShow("Please select Supervisor", Me.Text)
                Throw New Exception("Please select Staff.")
            End If
            Dim sQuery As String = String.Empty
            Dim FromDate As Date
            Dim ToDate As Date
            Dim strd As String = "01/" + clsCommon.myCstr(txtFromDate.Value.Month) + "/" + clsCommon.myCstr(txtFromDate.Value.Year)
            FromDate = clsCommon.myCDate(strd)

            Dim strmd As String = Nothing
            Dim days As Integer = 0
            days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "MM"))))
            strmd = clsCommon.myCstr(clsCommon.myCstr(days) + "/" + clsCommon.GetPrintDate(txtFromDate.Value, "MMM") + "/" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy"))
            ToDate = clsCommon.myCDate(strmd)

            sQuery = "Select (TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT/" & days & ")*2 as Milk_handledpertrip, convert(varchar(3),datename(month,TSPL_MILK_RECEIPT_HEAD.DOC_DATE)) AS MONTH , TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC," & _
                     " TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], " & _
                     "  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name],tspl_mcc_route_master.Supervisor_Name as [Supervisor Name],  TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS, TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,CATTLE.Qty, " & _
                     " TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)],CATTLE.Amount as Cattle_amount ," & _
                     " case when TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT >=500 then TSPL_VLC_MASTER_HEAD.VLC_Code end as 'upto 500 vlc code'" & _
                     " ,case when (TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT >=300 and TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT <=499 )then TSPL_VLC_MASTER_HEAD.VLC_Code else '' end as 'upto 300-499 vlc code'" & _
                     " ,case when (TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT >=100 and TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT <=299 )then TSPL_VLC_MASTER_HEAD.VLC_Code end as 'upto 100-299 vlc code'" & _
                     " ,case when (TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT <=100 )then TSPL_VLC_MASTER_HEAD.VLC_Code end as 'Less then 100 vlc code'" & _
                     " From TSPL_MILK_RECEIPT_DETAIL" & _
                " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " & _
                " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
                " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
                " left outer join (select Vendor_Code as TransporterCode,Vendor_Name as TransporterName,TSPL_MCC_Transporter_MAPPING.MCC_CODE from TSPL_MCC_Transporter_MAPPING  left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code=TSPL_MCC_Transporter_MAPPING.Transporter_CODE  )Transporter on Transporter.MCC_CODE=TSPL_MILK_RECEIPT_HEAD.MCC_CODE  Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " & _
                " left join (select Route_Code ,VLC_Code ,Bill_To_Location,Customer_Code ,sum(Amount) as Amount,sum(Qty) as Qty from (select TSPL_SD_SHIPMENT_DETAIL.Qty,  TSPL_ITEM_MASTER.ITEM_DESC, TSPL_VENDOR_MASTER.Vendor_Code as VSP,TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE ,TSPL_VLC_MASTER_HEAD.Route_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_ROUTE_MASTER.Route_No,Document_Date,TSPL_SD_SHIPMENT_detail.Amount,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location ,TSPL_SD_SHIPMENT_HEAD.Customer_Code  from  TSPL_SD_SHIPMENT_HEAD " & _
                " left join TSPL_SD_SHIPMENT_DETAIL  on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
                " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SHIPMENT_HEAD.Customer_Code" & _
                " left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No" & _
                " left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                " left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & _
                " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code " & _
                " left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code" & _
                " where TSPL_SD_SHIPMENT_HEAD.Trans_Type ='MCC'   "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location   IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            ' sQuery += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location   IN ('" + fndMccCode.Value + "') "
            sQuery += " and ITEM_DESC LIKE '%CATTLE%'" & _
                " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >=convert(date,'" + FromDate + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=convert(date,'" + ToDate + "',103 )" & _
                " ) final group by Bill_To_Location,Route_Code ,VLC_Code ,Customer_Code  " & _
                " ) as CATTLE" & _
                " on CATTLE.Bill_To_Location=TSPL_MILK_RECEIPT_HEAD.MCC_CODE" & _
                "  and CATTLE.Customer_Code =TSPL_MILK_RECEIPT_detail.VSP_CODE " & _
                " and CATTLE.Route_Code =TSPL_MILK_RECEIPT_detail.ROUTE_CODE " & _
                " and CATTLE.VLC_Code =TSPL_MILK_RECEIPT_detail.VLC_CODE " & _
                " where 2 = 2  and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) >=convert(date,'" + FromDate + "' ,103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + ToDate + "',103) "

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_MILK_RECEIPT_HEAD.MCC_CODE   IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")   "
            End If
            sQuery += " And tspl_mcc_route_master.Supervisor_Name='" + fndSuperVisorCode.Value + "'"
            'sQuery += " and TSPL_MILK_RECEIPT_HEAD.MCC_CODE   IN ('" + fndMccCode.Value + "')  And tspl_mcc_route_master.Supervisor_Name='" + fndSuperVisorCode.Value + "' "
            Dim finalquery As String
            finalquery = "  select MCC,max(XX.[Supervisor Name]) as [Supervisor Name], max(tspl_employee_master.Emp_Name) as [Emp Name],MAX(TSPL_MCC_MASTER.MCC_NAME) AS MCC_NAME ,MONTH," & _
                            " count(distinct([Route Code])) as Count_of_route,COUNT(distinct([Vlc Code])) as Count_VLC,sum(NO_OF_CANS) as NO_OF_CANS,COUNT(DISTINCT(VSP_CODE)) AS No_Of_Suppliers,case when COUNT(distinct(VSP_CODE))=0 then 0 else sum([Milk Weight(KG)])/COUNT(distinct(VSP_CODE)) end as Avg_Per_Farmer_Milk ," & _
                            " sum([Milk Weight(KG)]) as Total_MILK_Weight_KG,case when COUNT(distinct([Vlc Code]))=0 then 0 else sum([Milk Weight(KG)])/COUNT(distinct([Vlc Code])) end as Milk_weight_Per_VLC " & _
                            " ,sum(Milk_handledpertrip) as Milk_handledpertrip" & _
                            " ,ISNULL(sum(Cattle_amount),0) as Cattle_amount,  isnull(SUM(Qty),0) AS Total_Sale_Qty,count(([upto 500 vlc code])) as 'upto 500 vlc code'" & _
                            " ,count(distinct([upto 300-499 vlc code])) as'upto 300-499 vlc code'," & _
                            " count(distinct([upto 100-299 vlc code])) as 'upto 100-299 vlc code'," & _
                            " count(distinct('Less then 100 vlc code')) as 'Less then 100 vlc code'" & _
                            " from ( " & _
                            "" & sQuery & "" & _
                             "     ) xx " & _
                            " LEFT JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code =XX.MCC " & _
                            " left outer join tspl_employee_master on tspl_employee_master.EMP_CODE = xx.[Supervisor Name] " & _
                            " group by  MCC ,MONTH"




            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(finalquery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()

                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, Nothing, "RPTMONTHLYPROGRESSREPORT", "MD Conversion", "RPTCOMPANYADDMDCONVERSION.rpt")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

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

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

       
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
       

       
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        fndSuperVisorCode.Value = Nothing
        txtMCC.arrValueMember = Nothing
        txtSuperVisorName.Text = String.Empty
    End Sub



    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        Load_Report()

    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " " + clsCommon.myCstr(txtFromShift.SelectedValue) + "  " + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " " + clsCommon.myCstr(txtToShift.SelectedValue))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String)
            arr = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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


    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            '============================================
            Dim frm As New FrmMCCSummary
            frm.ReportLevel = 1
            frm.Frm_Date = clsCommon.myCstr(gv.CurrentRow.Cells("Doc_Date").Value)
            frm.strCurrentGrp = clsCommon.myCstr(gv.CurrentRow.Cells("Mcc").Value)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
            '==================================================================================================
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

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
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name from tspl_mcc_master "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "Code", "Name", txtMCC.arrValueMember, Nothing)
    End Sub

    Private Sub fndSuperVisorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSuperVisorCode._MYValidating
        fndSuperVisorCode.Value = clsEmployeeMaster.getFinder(" emp_status='Active' ", fndSuperVisorCode.Value, isButtonClicked)
        If clsCommon.myLen(fndSuperVisorCode.Value) > 0 Then
            txtSuperVisorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Emp_Name  from tspl_employee_master where EMP_CODE='" + fndSuperVisorCode.Value + "'"))
        Else
            txtSuperVisorName.Text = ""
        End If
    End Sub

    Private Sub fndMccCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMccCode._MYValidating
        fndMccCode.Value = clsMccMaster.getFinder("", fndMccCode.Value, isButtonClicked)
        If clsCommon.myLen(fndMccCode.Value) > 0 Then
            txtMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + fndMccCode.Value + "'"))
        Else
            txtMccName.Text = ""
        End If

    End Sub


End Class
