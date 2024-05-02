Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine

Public Class FrmDaywisePendingComplaint
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt1 As DataTable = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDaywisePendingComplaint)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rdalloutlet_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdalloutlet.ToggleStateChanged, rdselectoutlet.ToggleStateChanged
        cbgCustomer.Enabled = rdselectoutlet.IsChecked
        If rdselectoutlet.IsChecked = True Then
            rdalloutlet.IsChecked = False
        End If
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmDaywisePendingComplaint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadOutlet()
        LoadAsset()
        LoadComplaintType()
        LoadPrimary()
        LoadSecondary()
    End Sub

    Sub LoadOutlet()
        Dim qry As String = "select distinct TSPL_VISI_MASTER.Customer_id as [Outlet Code] ,tspl_customer_master.Customer_Name as [Outlet Description] from tspl_customer_master left outer join TSPL_VISI_MASTER on TSPL_CUSTOMER_MASTER .cust_code =TSPL_VISI_MASTER.customer_id where tspl_visi_master.customer_id<>''"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.DataSource = dt

        cbgasset.DisplayMember = "Outlet Description"
        cbgCustomer.ValueMember = "Outlet Code"
    End Sub

    Sub LoadAsset()
        Dim qry As String = "select distinct tspl_visi_master.asset_no as [Asset Code],tspl_item_master.item_desc as [Asset Description] from tspl_visi_master left outer join tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no"
        cbgasset.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgasset.DisplayMember = "Asset Description"
        cbgasset.ValueMember = "Asset Code"
    End Sub

    Sub LoadComplaintType()
        Dim qry As String = "select distinct TSPL_COMPLAINT_GROUP_MASTER.complaint_code as [Complaint Type Code],TSPL_COMPLAINT_GROUP_MASTER.Description from TSPL_COMPLAINT_GROUP_MASTER"
        chkcompl.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkcompl.DisplayMember = "Description"
        chkcompl.ValueMember = "Complaint Type Code"
    End Sub

    Sub LoadPrimary()
        Dim qry As String = "select distinct TSPL_PRIMARY_REASON_MASTER.reason_code as [Primary Complaint Code],TSPL_PRIMARY_REASON_MASTER.description as [Complaint Description] from TSPL_PRIMARY_REASON_MASTER"
        chkprimary.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkprimary.DisplayMember = "Complaint Description"
        chkprimary.ValueMember = "Primary Complaint Code"
    End Sub

    Sub LoadSecondary()
        Dim qry As String = "select distinct complaint_code as [Secondary Code],Description as [Secondary Reason] from TSPL_COMPLAINT_MASTER"
        chksecndry.DataSource = clsDBFuncationality.GetDataTable(qry)
        chksecndry.DisplayMember = "Secondary Reason"
        chksecndry.ValueMember = "Secondary Code"
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print(Exporter.Refresh)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Public Sub print(ByVal IsPrint As Exporter)
        Try
            Dim complt_status As String = Nothing
            Dim outletcode As String = Nothing
            Dim assetcode As String = Nothing
            Dim complttype As String = Nothing
            Dim primarycode As String = Nothing
            Dim secndrycode As String = Nothing
            Dim title As String = Nothing

            If rdbAll.IsChecked = False AndAlso rdbComplete.IsChecked = False AndAlso rdbnotcomplete.IsChecked = False AndAlso rdbpending.IsChecked = False Then
                clsCommon.MyMessageBoxShow("Please Select Any One Option Form Status")
                Return
            End If

            If rdselectoutlet.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Outlet")
                Return
            End If

            If rdselectasset.IsChecked = True AndAlso cbgasset.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Asset")
                Return
            End If

            If rdslctcompl.IsChecked = True AndAlso chkcompl.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Complaint Type")
                Return
            End If

            If rdselctprimary.IsChecked = True AndAlso chkprimary.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Primary Complaint Reason")
                Return
            End If

            If rdselctsecndry.IsChecked = True AndAlso chksecndry.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Secondary Complaint Reason")
                Return
            End If
            '-----------------------------------------------------------------------------

            Dim arrHeader As List(Of String) = New List(Of String)()

            If rdbAll.IsChecked = True Then
                complt_status = " and 1=1"
                'title = "Major Complaints (From " + fromDate.Text + " To " + ToDate.Text + ")"
                arrHeader.Add("Major Complaints : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            ElseIf rdbComplete.IsChecked = True Then
                complt_status = " and tspl_complaint_detail.compl_status='C'"
                'title = "Major Completed Complaints (From " + fromDate.Text + " To " + ToDate.Text + ")"
                arrHeader.Add("Major Completed Complaints : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            ElseIf rdbnotcomplete.IsChecked = True Then
                complt_status = " and tspl_complaint_detail.compl_status='N'"
                'title = "Major Not-Completed Complaints (From " + fromDate.Text + " To " + ToDate.Text + ")"
                arrHeader.Add("Major Not-Completed Complaints : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            ElseIf rdbpending.IsChecked = True Then
                complt_status = " and tspl_complaint_detail.compl_status='P'"
                'title = "Major Pending Complaints (From " + fromDate.Text + " To " + ToDate.Text + ")"
                arrHeader.Add("Major Pending Complaints : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            End If

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " ")
            '------------------------------------------------------------------

            If rdallcompl.IsChecked = True Then
                complttype = " and 1=1"
            ElseIf rdslctcompl.IsChecked = True Then
                complttype = " and tspl_complaint_detail.compl_type_code in (" + clsCommon.GetMulcallString(chkcompl.CheckedValue) + ")"
            End If

            If rdallasset.IsChecked = True Then
                assetcode = " and 1=1"
            ElseIf rdselectasset.IsChecked = True Then
                assetcode = " and tspl_complaint_detail.item_code in (" + clsCommon.GetMulcallString(cbgasset.CheckedValue) + ")"
            End If

            If rdalloutlet.IsChecked = True Then
                outletcode = " and 1=1"
            ElseIf rdselectoutlet.IsChecked = True Then
                outletcode = " and tspl_complaint_detail.cust_code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If

            If rdallprimary.IsChecked = True Then
                primarycode = " and 1=1"
            ElseIf rdselctprimary.IsChecked = True Then
                primarycode = " and tspl_complaint_detail.primary_reason_code in (" + clsCommon.GetMulcallString(chkprimary.CheckedValue) + ")"
            End If

            If rdallsecndry.IsChecked = True Then
                secndrycode = " and 1=1"
            ElseIf rdselctsecndry.IsChecked = True Then
                secndrycode = " and tspl_complaint_detail.sec_reason_code in (" + clsCommon.GetMulcallString(chksecndry.CheckedValue) + ")"
            End If
            '-------------------------------------------------------------------------------

            Dim qry As String = "select distinct '" + fromDate.Text + "' as fdate,'" + ToDate.Text + "' as tdate,tspl_complaint_detail.comp_id,tspl_complaint_detail.description,tspl_complaint_detail.comp_date,tspl_complaint_detail.work_order_no,tspl_complaint_detail.executive_code,TSPL_EMPLOYEE_MASTER.emp_name as technician,tspl_complaint_detail.cust_code as outletcode,TSPL_CUSTOMER_MASTER.customer_name as outletdesc,(TSPL_CUSTOMER_MASTER.add1+' '+TSPL_CUSTOMER_MASTER.add2+' '+TSPL_CUSTOMER_MASTER.add3) as landmark,TSPL_CUSTOMER_MASTER.city_code,TSPL_CITY_MASTER.city_name,TSPL_CUSTOMER_MASTER.state,tspl_complaint_detail.phone_no,tspl_complaint_detail.compl_type_code,TSPL_COMPLAINT_GROUP_MASTER.description as complaintdesc,tspl_complaint_detail.primary_reason_code,TSPL_PRIMARY_REASON_MASTER.description as primarydesc,tspl_complaint_detail.sec_reason_code,TSPL_COMPLAINT_MASTER.description as secndrydesc,tspl_visi_master.visimake as item_make,TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as makedesc,tspl_visi_master.model_no,a1.description as modeldesc,tspl_visi_master.visi_size as size,a2.description as sizedesc,tspl_complaint_detail.serial_no,tspl_complaint_detail.tag_no,tspl_complaint_detail.remarks,aa.year as year,aa.Security_Amount as security_amt "
            qry += "from tspl_complaint_detail left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=tspl_complaint_detail.executive_code and TSPL_EMPLOYEE_MASTER.emp_type='service dealer' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=tspl_complaint_detail.cust_code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.city_code=tspl_customer_master.city_code left outer join TSPL_COMPLAINT_GROUP_MASTER on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join TSPL_PRIMARY_REASON_MASTER on TSPL_PRIMARY_REASON_MASTER.reason_code=tspl_complaint_detail.primary_reason_code and TSPL_PRIMARY_REASON_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join TSPL_COMPLAINT_MASTER on TSPL_COMPLAINT_MASTER.complaint_code=tspl_complaint_detail.sec_reason_code"
            qry += " left outer join (select TSPL_RGP_DETAIL.Item_Code ,year(TSPL_RGP_HEAD.RGP_Date) as year ,TSPL_RGP_DETAIL.security_amount from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where isnull(TSPL_RGP_DETAIL.security_amount,0)>0)aa"
            qry += " on aa.Item_Code=TSPL_COMPLAINT_DETAIL.item_code "
            qry += " left outer join tspl_visi_master on tspl_visi_master.asset_no=tspl_complaint_detail.item_code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.code=tspl_visi_master.visimake left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES as a1 on a1.code=tspl_visi_master.model_no left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES as a2 on a2.code=tspl_visi_master.visi_size"
            qry += " where tspl_complaint_detail.comp_code='" + objCommonVar.CurrentCompanyCode + "' " + outletcode + " " + assetcode + " " + complttype + " " + primarycode + " " + secndrycode + " " + complt_status + ""
            qry += " and convert(date,tspl_complaint_detail.comp_date,103)>=convert(date,'" + clsCommon.myCDate(fromDate.Text).ToString("dd/MM/yyyy") + "',103) and convert(date,tspl_complaint_detail.comp_date,103) <=convert(date,'" + clsCommon.myCDate(ToDate.Text).ToString("dd/MM/yyyy") + "',103)"

            dt1 = clsDBFuncationality.GetDataTable(qry)

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found For Printing", Me.Text)
            Else
                gv.DataSource = dt1
                SetGridFormat()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If


            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcel(title, gv, arrHeader, Me.Text)
            End If

            If IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF(title, gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.DataSource = dt1
        gv.AllowAddNewRow = False
        gv.AllowDragToGroup = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next


        gv.Columns("Fdate").IsVisible = True
        gv.Columns("Fdate").Width = 100
        gv.Columns("Fdate").HeaderText = "From Date"

        gv.Columns("Tdate").IsVisible = True
        gv.Columns("Tdate").Width = 100
        gv.Columns("Tdate").HeaderText = "To date"

        gv.Columns("comp_id").IsVisible = True
        gv.Columns("comp_id").Width = 70
        gv.Columns("comp_id").HeaderText = "Complaint ID"

        gv.Columns("description").IsVisible = True
        gv.Columns("description").Width = 100
        gv.Columns("description").HeaderText = "Complaint Description"

        gv.Columns("comp_date").IsVisible = True
        gv.Columns("comp_date").Width = 100
        gv.Columns("comp_date").HeaderText = "Complaint Date"

        gv.Columns("work_order_no").IsVisible = True
        gv.Columns("work_order_no").Width = 100
        gv.Columns("work_order_no").HeaderText = "Move/Work No."

        gv.Columns("executive_code").IsVisible = True
        gv.Columns("executive_code").Width = 100
        gv.Columns("executive_code").HeaderText = "Technician Code"

        gv.Columns("technician").IsVisible = True
        gv.Columns("technician").Width = 180
        gv.Columns("technician").HeaderText = "Technician Person"


        gv.Columns("outletcode").IsVisible = True
        gv.Columns("outletcode").Width = 100
        gv.Columns("outletcode").HeaderText = "Outlet Code"

        gv.Columns("outletdesc").IsVisible = True
        gv.Columns("outletdesc").Width = 150
        gv.Columns("outletdesc").HeaderText = "Outlet Description"

        gv.Columns("landmark").IsVisible = True
        gv.Columns("landmark").Width = 100
        gv.Columns("landmark").HeaderText = "LandMark"

        gv.Columns("city_code").IsVisible = False

        gv.Columns("city_name").IsVisible = True
        gv.Columns("city_name").Width = 100
        gv.Columns("city_name").HeaderText = "City"

        gv.Columns("state").IsVisible = True
        gv.Columns("state").Width = 100
        gv.Columns("state").HeaderText = "State"

        gv.Columns("phone_no").IsVisible = True
        gv.Columns("phone_no").Width = 100
        gv.Columns("phone_no").HeaderText = "Contact No."


        gv.Columns("compl_type_code").IsVisible = False

        gv.Columns("complaintdesc").IsVisible = True
        gv.Columns("complaintdesc").Width = 100
        gv.Columns("complaintdesc").HeaderText = "Complaint Type"

        gv.Columns("primary_reason_code").IsVisible = False

        gv.Columns("primarydesc").IsVisible = True
        gv.Columns("primarydesc").Width = 100
        gv.Columns("primarydesc").HeaderText = "Primary Complaint"

        gv.Columns("sec_reason_code").IsVisible = False

        gv.Columns("secndrydesc").IsVisible = True
        gv.Columns("secndrydesc").Width = 100
        gv.Columns("secndrydesc").HeaderText = "Secondary Complaint"


        gv.Columns("item_make").IsVisible = True
        gv.Columns("item_make").Width = 80
        gv.Columns("item_make").HeaderText = "Make Code"

        gv.Columns("makedesc").IsVisible = True
        gv.Columns("makedesc").Width = 80
        gv.Columns("makedesc").HeaderText = "Make Description"

        gv.Columns("model_no").IsVisible = True
        gv.Columns("model_no").Width = 80
        gv.Columns("model_no").HeaderText = "Model Code"

        gv.Columns("modeldesc").IsVisible = True
        gv.Columns("modeldesc").Width = 80
        gv.Columns("modeldesc").HeaderText = "Model Description"

        gv.Columns("size").IsVisible = True
        gv.Columns("size").Width = 80
        gv.Columns("size").HeaderText = "Size Code"

        gv.Columns("sizedesc").IsVisible = True
        gv.Columns("sizedesc").Width = 80
        gv.Columns("sizedesc").HeaderText = "Size Of Asset"

        gv.Columns("serial_no").IsVisible = True
        gv.Columns("serial_no").Width = 80
        gv.Columns("serial_no").HeaderText = "Serial No."

        gv.Columns("tag_no").IsVisible = True
        gv.Columns("tag_no").Width = 80
        gv.Columns("tag_no").HeaderText = "Tag No."

        gv.Columns("remarks").IsVisible = True
        gv.Columns("remarks").Width = 100
        gv.Columns("remarks").HeaderText = "Remarks"

        gv.Columns("year").IsVisible = True
        gv.Columns("year").Width = 80
        gv.Columns("year").HeaderText = "Year"

        gv.Columns("security_amt").IsVisible = True
        gv.Columns("security_amt").Width = 80
        gv.Columns("security_amt").HeaderText = "Security Amount"

        gv.Columns.Add("Sale(Per year)")

        gv.Columns("Sale(Per year)").IsVisible = True
        gv.Columns("Sale(Per year)").ReadOnly = True
        gv.Columns("Sale(Per year)").Width = 60
        gv.Columns("Sale(Per year)").HeaderText = "Sale(Per year)"
    End Sub

    Public Sub Reset()
        fromDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy")
        ToDate.Text = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy")
        rdbAll.IsChecked = True
        rdalloutlet.IsChecked = True
        rdallasset.IsChecked = True
        rdallcompl.IsChecked = True
        rdallprimary.IsChecked = True
        rdallsecndry.IsChecked = True
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If gv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data For Excel Export", Me.Text)
            Reset()
            Return
        End If
        print(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If gv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data For PDF Export", Me.Text)
            Reset()
            Return
        End If
        print(Exporter.PDF)
    End Sub

    Private Sub rdallasset_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdallasset.ToggleStateChanged, rdselectasset.ToggleStateChanged
        cbgasset.Enabled = rdselectasset.IsChecked
    End Sub

    Private Sub rdallcompl_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdallcompl.ToggleStateChanged, rdslctcompl.ToggleStateChanged
        chkcompl.Enabled = rdslctcompl.IsChecked
    End Sub

    Private Sub rdallprimary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdallprimary.ToggleStateChanged, rdselctprimary.ToggleStateChanged
        chkprimary.Enabled = rdselctprimary.IsChecked
    End Sub

    Private Sub rdallsecndry_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdallsecndry.ToggleStateChanged, rdselctsecndry.ToggleStateChanged
        chksecndry.Enabled = rdselctsecndry.IsChecked
    End Sub
End Class
