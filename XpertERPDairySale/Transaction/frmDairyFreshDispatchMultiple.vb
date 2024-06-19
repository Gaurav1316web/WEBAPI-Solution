Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class frmDairyFreshDispatchMultiple
    Inherits FrmMainTranScreen
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnPrint.Visible = MyBase.isPrintFlag
        'btnDelete.Visible = False
        'If MyBase.isReverse Then
        '    btnReverseAndUnpost.Enabled = True
        'Else
        '    btnReverseAndUnpost.Enabled = False
        'End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub frmDairyFreshDispatchMultiple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
    End Sub
    Public Sub AddNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        rbtnMorning.Checked = True
        rbtnTaxable.Checked = True
        txtTotalCrateQty.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim whrcls As String = " where convert(date,TSPL_BOOKING_MATSER.document_date,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value) & "' and convert(date,TSPL_BOOKING_MATSER.document_date,103)<='" & clsCommon.GetPrintDate(txtToDate.Value) & "' and TSPL_BOOKING_MATSER.Location_Code='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.Posted=1 "
            If rbtnMorning.Checked Then
                whrcls += " and TSPL_BOOKING_MATSER.GatePass_Type='AM' "
            ElseIf rbtnEvening.Checked Then
                whrcls += " and TSPL_BOOKING_MATSER.GatePass_Type='PM' "
            End If
            If rbtnTaxable.Checked Then
                whrcls += " and TSPL_ITEM_MASTER.IsTaxable=1 "
            ElseIf rbtnNonTaxable.Checked Then
                whrcls += " and TSPL_ITEM_MASTER.IsTaxable=0 "
            End If
            whrcls += " and TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code is not null  and not exists(select 1 from TSPL_SD_SHIPMENT_BOOKING_DETAIL where TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code=TSPL_BOOKING_DETAIL.Against_DemandBooking_TR_Code) "
            Dim qry As String = "select Final.Document_Date,Final.route_no,max(Final.ShiftType) as ShiftType,max(Final.LocationCode) as LocationCode,max(Final.IsTaxable) as IsTaxable from( select convert(date,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_BOOKING_DETAIL.route_no,max(TSPL_BOOKING_MATSER.GatePass_Type) as ShiftType,max(TSPL_BOOKING_MATSER.Location_Code) as LocationCode,max(TSPL_ITEM_MASTER.IsTaxable) as IsTaxable
from TSPL_BOOKING_MATSER
left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " & whrcls & " group by TSPL_BOOKING_MATSER.document_date,TSPL_BOOKING_DETAIL.route_no )Final  group by Final.Document_Date,Final.route_no "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.ProgressBarShow()
                For Each dr As DataRow In dt.Rows
                    Dim frm As New frmShipmentDairy
                    frm.routeno = clsCommon.myCstr(dr("route_no"))
                    frm.LocationCode = clsCommon.myCstr(dr("LocationCode"))
                    frm.Supplydate = clsCommon.GetPrintDate(dr("Document_Date"))
                    frm.Shifttype = clsCommon.myCstr(dr("ShiftType"))
                    frm.IsTaxable = clsCommon.myCstr(dr("IsTaxable"))
                    frm.IsAutoClose = True
                    frm.ShowDialog()
                Next
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Saved Succeffully.", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, " No Data Found!", Me.Text)
            End If
            qry = "select ROW_NUMBER() OVER (ORDER BY Final.Route_NO) AS Sl_No,Final.* from ( select 
max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as Document_Date,max(TSPL_SD_SHIPMENT_HEAD.ParentDocNo) as Document_Code,max(TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No) as Sale_Invoice_No,
max(TSPL_SD_SHIPMENT_HEAD.Route_No) as Route_No ,max(TSPL_SD_SHIPMENT_HEAD.Route_Desc) as Route_Desc,max(TSPL_SD_SHIPMENT_HEAD.Customer_Code) as Customer_Code,
max(TSPL_CUSTOMER_MASTER.customer_name) as customer_name,TSPL_SD_SHIPMENT_DETAIL.Item_Code,max(TSPL_ITEM_MASTER.Short_Description) as Short_Description ,
TSPL_SD_SHIPMENT_DETAIL.Unit_code,sum(TSPL_SD_SHIPMENT_DETAIL.Qty) as QTY
from TSPL_SD_SHIPMENT_HEAD
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
left join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'
and TSPL_SD_SHIPMENT_HEAD.Shift_Type='" + IIf(rbtnMorning.Checked, "AM", "PM") + "'
and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='" + IIf(rbtnNonTaxable.Checked, "NT", "T") + "'  and TSPL_SD_SHIPMENT_HEAD.Status=0 group by TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_HEAD.Route_No
)Final order by Final.Route_No "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                gv1.DataSource = dt1
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()
            End If
            Dim totalCrate As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(grow.Cells("Unit_Code").Value, "Crate") = CompairStringResult.Equal Then
                    totalCrate += clsCommon.myCdbl(grow.Cells("QTY").Value)
                End If
            Next
            txtTotalCrateQty.Text = totalCrate
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = "  location_code in (select distinct loc_code from tspl_booking_detail) and Location_Category not in('MCC')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("DS-SHLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            Dim lstDoc As New List(Of String)
            For Each grow As GridViewRowInfo In gv1.Rows
                If Not lstDoc.Contains(grow.Cells("Document_Code").Value) Then
                    lstDoc.Add(grow.Cells("Document_Code").Value)
                End If
            Next
            If (myMessages.postConfirm()) Then
                For Each str As String In lstDoc
                    Dim obj As New clsPSShipmentHead()
                    obj = clsPSShipmentHead.GetData(str, NavigatorType.Current)
                    If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                        clsPSShipmentHead.PostData("DISPATCH-DS", str, True)
                    End If
                Next
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
