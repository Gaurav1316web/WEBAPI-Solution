Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmVehicleWiseTransfe
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ReportTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Ctrl+P Print the Report")
        lblVehicleNo.Text = ""
        txtFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        txtToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
    End Sub

    Private Sub txtTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleNo._MYValidating
        Dim qry As String = "select Vehicle_Id as Code ,Number as [Vehicle Number] from TSPL_VEHICLE_MASTER"
        txtVehicleNo.Value = clsCommon.ShowSelectForm("Fndvehicle", qry, "Code", "", txtVehicleNo.Value, "", isButtonClicked)
        lblVehicleNo.Text = clsDBFuncationality.getSingleValue("select  Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleNo.Value + "'")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptVehicleWiseLoadout)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        PrintData()
    End Sub

    Sub PrintData()
        If clsCommon.myLen(txtVehicleNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Vehicle No", Me.Text)
            Exit Sub
        End If

        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy hh:mm tt")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy hh:mm tt")
        Dim strVehicle As String = txtVehicleNo.Value + " - " + lblVehicleNo.Text
        Dim qry As String = "select ROW_NUMBER() OVER (ORDER BY xxx.Item_Code) as SNo, xxx.Item_Code,MAX(xxx.Item_Desc) as Item_Desc,SUM(FCQTY) as FCQTY,SUM(FBQTY) as FBQTY,SUM(SHQTY) as SHQTY,SUM(ECQTY) AS ECQTY,max('" + strFromDate + "') as FromDate,max('" + strToDate + "') as ToDate, '" + strVehicle + "' as Vehicle   from("
        qry += " select TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,"
        qry += " case when Uom='FC' then Item_Qty else 0 end as FCQTY,"
        qry += " case when Uom='FB' then Item_Qty else 0 end as FBQTY ,"
        qry += " case when Uom='SH' then Item_Qty else 0 end as SHQTY,"
        qry += " case when Uom='EC' then Item_Qty else 0 end as ECQTY "
        qry += " from TSPL_TRANSFER_DETAIL "
        qry += " left outer join  TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No"
        qry += " where EntryDateTime>='" + strFromDate + "' and EntryDateTime<='" + strToDate + "' and Vehicle_Code='" + txtVehicleNo.Value + "' and Transfer_Type='LO'"
        qry += " UNION ALL "
        qry += " Select TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc , "
        qry += " case when Unit_code ='FC' then Invoice_Qty  else 0 end as FCQTY, "
        qry += " case when Unit_code ='FB' then Invoice_Qty else 0 end as FBQTY, "
        qry += " TSPL_SALE_INVOICE_HEAD.Shell_Qty as SHQTY, 0 as ECQTY from TSPL_SALE_INVOICE_DETAIL "
        qry += " Left Outer Join TSPL_SALE_INVOICE_HEAD ON  TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No "
        qry += " Where Sale_Invoice_Date>='" + strFromDate + "' AND Sale_Invoice_Date<='" + strToDate + "' AND TSPL_SALE_INVOICE_HEAD.Vehicle_Code='" + txtVehicleNo.Value + "' AND Shipment_Type ='Sale'"
        qry += " )xxx "
        qry += " group by xxx.Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "rptVehicleWiseTransfer", Me.Text)
        frmCRV = Nothing
    End Sub

    Private Sub frmVehicleWiseTransfe_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            PrintData()
        End If
    End Sub
End Class
