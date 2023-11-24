Imports common
Public Class frmDemandHistory
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub frmDemandHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnGo, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub frmDemandHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnGo.Enabled Then
            fillGridReport(txtDate.Value, cmbShift.Text, txtBooth.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub Reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBooth.Value = ""
        cmbShift.SelectedIndex = 0
        txtBoothDesc.Text = ""
        gv1.DataSource = Nothing
        gv1.Visible = False
    End Sub
    Private Sub fillGridReport(ByVal frmdate As Date, ByVal Shift As String, ByVal BoothCode As String)
        Try
            Dim StrQry As String = "select 
TSPL_BOOKING_DETAIL_Hist_Data.Hist_Version as [History Version],
TSPL_BOOKING_DETAIL_Hist_Data.Against_DemandBooking_No as [Document No],
TSPL_BOOKING_DETAIL_Hist_Data.route_no as [Route No],
TSPL_ITEM_MASTER.Short_Description as [Item Name],
TSPL_BOOKING_DETAIL_Hist_Data.Amount_with_Tax as [Amount],
TSPL_BOOKING_DETAIL_Hist_Data.Booking_Qty as [Qty],
TSPL_BOOKING_DETAIL_Hist_Data.Unit_code as [Unit Code],
TSPL_BOOKING_DETAIL_Hist_Data.Hist_By as [History By],
TSPL_BOOKING_DETAIL_Hist_Data.Hist_on as [History ON]
from TSPL_BOOKING_MATSER_Hist_Data
left join TSPL_BOOKING_DETAIL_Hist_Data on TSPL_BOOKING_MATSER_Hist_Data.Document_No=TSPL_BOOKING_DETAIL_Hist_Data.Document_No
left join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL_Hist_Data.Item_Code=TSPL_ITEM_MASTER.Item_Code where 2=2"
            Dim whrcls As String = " and Convert(date,TSPL_BOOKING_DETAIL_Hist_Data.Hist_On,103) >=Convert(date,'" + clsCommon.GetPrintDate(frmdate) + "',103)
and Convert(date,TSPL_BOOKING_DETAIL_Hist_Data.Hist_On,103) <=Convert(date,'" + clsCommon.GetPrintDate(frmdate) + "',103)
and TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code='" + txtBooth.Value + "' "
            If clsCommon.CompairString(Shift, "Morning") = CompairStringResult.Equal Then
                whrcls += " And TSPL_BOOKING_MATSER_Hist_Data.GatePass_Type='AM'"
            ElseIf clsCommon.CompairString(Shift, "Evening") = CompairStringResult.Equal Then
                whrcls += " And TSPL_BOOKING_MATSER_Hist_Data.GatePass_Type='PM'"
            End If

            StrQry += whrcls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.Visible = True
                gv1.BestFitColumns()
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.ReadOnly = True
                'gv1.Visible = False
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found for this Booth  ")
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtBooth.Value) > 0 AndAlso clsCommon.myLen(cmbShift.Text) > 0 Then
                fillGridReport(txtDate.Value, cmbShift.Text, txtBooth.Value)
            Else
                Throw New Exception(" Please Select Shift/Booth ")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtBooth__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBooth._MYValidating
        Try
            Dim StrQry As String = "select Cust_Code as Code,Customer_Name as [Customer Name],Route_No as [Route No],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER "
            Dim WhrCls As String = " Status='N' and Cust_Group_Code='BOOTH'"
            txtBooth.Value = clsCommon.ShowSelectForm("BoothDetails", StrQry, "Code", WhrCls, txtBooth.Value, "Code", isButtonClicked)
            txtBoothDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER  where Cust_Code ='" + txtBooth.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDemandHistory & "'"))
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            If clsCommon.CompairString(cmbShift.Text, "Morning") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Morning")
            ElseIf clsCommon.CompairString(cmbShift.Text, "Evening") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Evening")
            End If
            arrHeader.Add("Booth : " + txtBoothDesc.Text)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class