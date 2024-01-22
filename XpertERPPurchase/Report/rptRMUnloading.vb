Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class rptRMUnloading
    Inherits FrmMainTranScreen
    Dim StrPermission As String
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing


    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        Dim selectedMonth As Integer = txtMonth.Value.Month
        Dim selectedYear As Integer = txtMonth.Value.Year

        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        txtMonth.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = Nothing
        lblLocation.Text = ""
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Try
            Dim arrItem As New List(Of String)
            Dim item As String = Nothing
            Dim RMweight As String = "select  '" + clsCommon.GetPrintDate((Slot1FD), "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate((Slot3TD), "dd/MM/yyyy") + "' as ToDate,
                                    max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,max(TSPL_LOCATION_MASTER.Add1) as add1,max(TSPL_LOCATION_MASTER.Add2) as add2 ,max(TSPL_LOCATION_MASTER.Add3) as add3,max(TSPL_LOCATION_MASTER.Add4) add4,CONVERT(DATE,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) AS Weighment_Date,(tspl_item_master.Short_Description) AS Short_Description,
                                    cast(SUM(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) as int) as weight ,COUNT(*) as noofdelivery

                                    from TSPL_GRN_HEAD
                                    LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                                    LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_HEAD ON TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                                    LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_DETAIL ON TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                                    left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code= TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                                    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PO_WEIGHTMENT_HEAD.Location_Code
                                    where 
                                    TSPL_PO_WEIGHTMENT_HEAD.Location_Code= '" + clsCommon.myCstr(txtLocation.Value) + "' AND TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date>='" + clsCommon.GetPrintDate(Slot1FD) + "' 
                                    AND TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date<='" + clsCommon.GetPrintDate(Slot3TD) + "' AND TSPL_GRN_HEAD.IsCancel=0 and TSPL_PO_WEIGHTMENT_HEAD.Status=1
                                    GROUP BY  Weighment_Date,Short_Description"
            'and tspl_item_master.Structure_Code='RM'  ('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019')
            Dim dtRMweight As DataTable = clsDBFuncationality.GetDataTable(RMweight)

            If dtRMweight IsNot Nothing And dtRMweight.Rows.Count > 0 Then
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funreport(CrystalReportFolder.PRODUCTION, dtRMweight, "rptRMUnloadingReport", "")
                'frmCRV = Nothing
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dtRMweight, "rptRMUnloading", "")
                'PDFPath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.MilkProcurement, dt, dtAdditionFinance, "crptMilkPurchaseBillPaymentProcessNewJPR", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeductionFinance, "subReduceDeduction.rpt", dtReduceDeduction, "subSaving.rpt", dtSaving, "SubAdditionOther.rpt", dtAdditionOther, "SubDeductionOther.rpt", dtDeductionOther)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
        End If
        Catch ex As Exception
        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub txtLocation_Load(sender As Object, e As EventArgs)

    End Sub


End Class


