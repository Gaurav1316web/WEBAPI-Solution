Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Public Class rptPTPBill
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        Reset()
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtShed._MYValidating
        Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"
        txtShed.Value = clsLocation.getFinder(whrCls, txtShed.Value, isButtonClicked)
        lblShed.Text = clsLocation.GetName(txtShed.Value, Nothing)

    End Sub

    Private Sub fndSingleMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        If clsCommon.myLen(txtShed.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("First Select Shed")
            Return
        End If
        Dim qry As String = " Select MCC_Code , MCC_NAME  from TSPL_MCC_MASTER  "
        txtMCC.Value = clsCommon.ShowSelectForm("MCC@PTPBill", qry, "MCC_Code", "plant_code = '" + txtShed.Value + "' ", txtMCC.Value, "MCC_Code", isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_NAME from TSPL_MCC_MASTER where MCC_Code = '" + txtMCC.Value + "' "))
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        If clsCommon.myLen(txtMCC.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("First Select MCC")
            Return
        End If
        Dim qry As String = " select Route_Code as Code,Route_Name as Name from TSPL_MCC_ROUTE_MASTER  "
        txtRoute.Value = clsCommon.ShowSelectForm("Route@PTPBill", qry, "Code", "MCC_Code = '" + txtMCC.Value + "' ", txtRoute.Value, "Code", isButtonClicked)
        lblRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code = '" + txtRoute.Value + "' "))

    End Sub

    Private Sub btnDotMatrixPrint_Click(sender As Object, e As EventArgs) Handles btnDotMatrixPrint.Click
        Try
            If clsCommon.myLen(txtShed.Value) <= 0 Then
                txtShed.Focus()
                Throw New Exception("Please select Shed")
            End If
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            If clsCommon.myLen(txtRoute.Value) <= 0 Then
                txtRoute.Focus()
                Throw New Exception("Please select Route")
            End If
            If clsCommon.myLen(txtTaxText.Text) <= 0 Then
                txtTaxText.Focus()
                Throw New Exception("Please select Tax Text")
            End If
            If clsCommon.myLen(txtTaxPer.Value) <= 0 Then
                txtTaxPer.Focus()
                Throw New Exception("Please select Tax %")
            End If

            Dim qry As String = "select xx.*,MilkProcurment.Qty_Morning,MilkProcurment.Qty_Evening,MilkProcurment.Qty_Morning+MilkProcurment.Qty_Evening as Qty_Total,(select sum(1) from TSPL_MCC_ROUTE_VLC_MAPPING where Route_CODE='" + txtRoute.Value + "') as NoOfVLC,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,'" + txtTaxText.Text + "' as TaxText," + clsCommon.myCstr(txtTaxPer.Value) + " as TaxRate from (
select max(Vendor_Code) as Vendor_Code,max(Vendor_Name) as Vendor_Name,max(Address) as Address,max(Route_Code) as Route_Code,max(ROUTE_NAME) as ROUTE_NAME,max(MCC_CODE) as MCC_CODE,max(MCC_NAME) as MCC_NAME,max(PAN) as PAN,sum( Kilometer_Morning ) as Kilometer_Morning,sum(Kilometer_Evening) as Kilometer_Evening,sum(Kilometer_Total) as Kilometer_Total,Doc_Date,sum(Amount) as Amount,max(Vehicle_Code) as Vehicle_Code,max(Vehicle_Description) as Vehicle_Description
 from (
select TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name
,(isnull(TSPL_VENDOR_MASTER.Add1,'')+isnull(TSPL_VENDOR_MASTER.Add2,'') +isnull(TSPL_VENDOR_MASTER.Add3,'')) as Address,TSPL_MILK_Shift_End_HEAD.MCC_CODE, TSPL_MCC_MASTER.MCC_NAME, TSPL_PROVISION_ENTRY.Route_Code,TSPL_MCC_ROUTE_MASTER.ROUTE_NAME,TSPL_VENDOR_MASTER.PAN,case when SHIFT='M' then TSPL_MCC_ROUTE_MASTER.Kilometer_Morning else 0 end as Kilometer_Morning,case when SHIFT='M' then 0 else TSPL_MCC_ROUTE_MASTER.Kilometer_Evening end as Kilometer_Evening,(case when SHIFT='M' then isnull(TSPL_MCC_ROUTE_MASTER.Kilometer_Morning,0) else 0 end + case when SHIFT='M' then 0 else isnull(TSPL_MCC_ROUTE_MASTER.Kilometer_Evening,0) end) as Kilometer_Total
, convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) as Doc_Date,TSPL_PROVISION_ENTRY.Amount,TSPL_MILK_Shift_End_HEAD.SHIFT,TSPL_Primary_Vehicle_Master.Vehicle_Code,TSPL_Primary_Vehicle_Master.Description as Vehicle_Description
from TSPL_PROVISION_ENTRY 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_PROVISION_ENTRY.Vendor_Code
left outer join TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_PROVISION_ENTRY.Route_Code
left outer join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.DOC_CODE=TSPL_PROVISION_ENTRY.Ref_Doc_No
left outer join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE and TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE=TSPL_PROVISION_ENTRY.Route_Code
left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_Shift_End_HEAD.MCC_CODE
where TSPL_PROVISION_ENTRY.Prog_Code='M-Shift_End' and TSPL_PROVISION_ENTRY.Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_PROVISION_ENTRY.Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_PROVISION_ENTRY.Route_Code='" + txtRoute.Value + "'
)x group by Doc_Date
)xx
left outer join (select DOC_DATE,sum(Qty* case when SHIFT='M' then 1 else 0 end ) as Qty_Morning,sum(Qty* case when SHIFT='E' then 1 else 0 end ) as Qty_Evening from (
select TSPL_MILK_SRN_HEAD.DOC_CODE,  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR as  Qty 
from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
where TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.ROUTE_CODE='" + txtRoute.Value + "'
)x Group by DOC_DATE) as MilkProcurment on MilkProcurment.DOC_DATE=convert(date, xx.Doc_Date,103) order by xx.Doc_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptPTPBill", "PTP Bill")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
