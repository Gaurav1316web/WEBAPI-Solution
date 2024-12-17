Imports common
Imports System.ComponentModel
Imports System.IO

Public Class FrmLocalMilkSaleRegister
    Inherits FrmMainTranScreen

    Private Sub txtMultiBMC__My_Click(sender As Object, e As EventArgs) Handles txtMultiBMC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2 "
        txtMultiBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@VMPIFSC", qry, "MCC_Code", "MCC_NAME", txtMultiBMC.arrValueMember, txtMultiBMC.arrDispalyMember)
    End Sub

    Private Sub TxtMultiDCS__My_Click(sender As Object, e As EventArgs) Handles TxtMultiDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code AS DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader Code],TSPL_VLC_MASTER_HEAD.VLC_Name as DCS_Name from TSPL_VLC_MASTER_HEAD "
            If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ") "
            End If
            TxtMultiDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VLC@VMPIFSC", qry, "DCS_Code", "DCS_Name", TxtMultiDCS.arrValueMember, TxtMultiDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnSavingSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnDateMilkType.ToggleStateChanged

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If rbtnDetails.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_DET"
        ElseIf rbtnBMCWise.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_BMC"
        ElseIf rbtnDCSWise.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_DCS"
        ElseIf rbtnDateMilkType.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_MILK "
        ElseIf rbtnDateShiftType.IsChecked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + "_SHIF"
        End If
        Print(False, Nothing, Nothing)
    End Sub
    Public Sub Print(ByVal isPrint As Boolean, ByVal BankAdviseDocNo As String, ByVal MCC As String)
        Dim BaseQry As String = Nothing
        Dim FinalQry As String = Nothing
        Dim whre As String = ""
        Dim whrclsShift As String = ""
        Dim whrclsMilk As String = ""
        If clsCommon.CompairString(rddlShift.Text, "Morning") = CompairStringResult.Equal Then
            whrclsShift += " and Shift='M' "
        ElseIf clsCommon.CompairString(rddlShift.Text, "Evening") = CompairStringResult.Equal Then
            whrclsShift += " and Shift='E' "
        ElseIf clsCommon.CompairString(rddlShift.Text, "Both") = CompairStringResult.Equal Then
            whrclsShift += "  "
        End If

        If clsCommon.CompairString(rddlMilk.Text, "Cow") = CompairStringResult.Equal Then
            whrclsMilk += " and Milk_Type='C' "
        ElseIf clsCommon.CompairString(rddlMilk.Text, "Buffalo") = CompairStringResult.Equal Then
            whrclsMilk += " and Milk_Type='B' "
        ElseIf clsCommon.CompairString(rddlMilk.Text, "Mix") = CompairStringResult.Equal Then
            whrclsMilk = "  "
        End If


        'If rbtnMorning.IsChecked Then
        '    'whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'AM' "
        '    whrclsShift = " and Shift  = 'M' "
        'ElseIf rbtnEvening.IsChecked Then
        '    ' whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'PM' "
        '    whrclsShift = " and Shift  = 'E' "
        'ElseIf rbtnBothShift.IsChecked Then
        '    ' whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'PM' "
        '    whrclsShift = " and Shift  = 'B' "
        'End If
        'If rbtnCow.IsChecked Then
        '    'whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'AM' "
        '    whrclsMilk = " and Milk_Type  = 'C' "
        'ElseIf rbtnbuffalo.IsChecked Then
        '    ' whrclsShift = " and TSPL_BOOKING_MATSER.GatePass_Type  = 'PM' "
        '    whrclsMilk = " and Milk_Type  = 'B' "
        'ElseIf rbtnMix.IsChecked Then
        '    whrclsMilk = " and Milk_Type  = 'M' "
        'End If

        If txtMultiBMC.arrValueMember IsNot Nothing AndAlso txtMultiBMC.arrValueMember.Count > 0 Then
            whre += " and  xxx.MCC_Code in (" + clsCommon.GetMulcallString(txtMultiBMC.arrValueMember) + ") "
        End If
        If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
            whre += " and xxx.VLC_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ") "
        End If
        BaseQry = " (select TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader
,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_REIL_LOCAL_MILK_SALE.Doc_Date,
TSPL_REIL_LOCAL_MILK_SALE.Shift,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.Milk_Type,
TSPL_REIL_LOCAL_MILK_SALE_DETAIL.QTY,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.RATE,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.Amount from TSPL_REIL_LOCAL_MILK_SALE_DETAIL
left outer join TSPL_REIL_LOCAL_MILK_SALE ON TSPL_REIL_LOCAL_MILK_SALE.PK_ID=TSPL_REIL_LOCAL_MILK_SALE_DETAIL.REF_PK_ID
left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_REIL_LOCAL_MILK_SALE.VLC_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC)xxx "

        If rbtnBMCWise.IsChecked Then
            FinalQry = " Select xxx.MCC_Code,Max(xxx.MCC_NAME)MCC_NAME,max(xxx.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,sum(xxx.Qty)Qty,CASE 
        WHEN SUM(xxx.Qty) != 0 THEN SUM(xxx.Amount) / SUM(xxx.Qty)
        ELSE 0
    END AS Rate,sum(xxx.Amount)Amount from " + BaseQry + " where 2=2 " + whre + " " + whrclsShift + " " + whrclsMilk + "  and convert(date,Doc_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Doc_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
          group by MCC_Code"

        ElseIf rbtnDCSWise.IsChecked Then
            FinalQry = " select max(xxx.MCC_Code)MCC_Code,Max(xxx.MCC_NAME)MCC_NAME,max(xxx.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,XXX.VLC_Code as VLC_Code,MAX(XXX.VLC_Name)VLC_Name,max(XXX.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,SUM(XXX.QTY)QTY,CASE 
        WHEN SUM(xxx.Qty) != 0 THEN SUM(xxx.Amount) / SUM(xxx.Qty)
        ELSE 0
    END AS Rate,SUM(XXX.Amount)Amount
from  " + BaseQry + " where 2=2  " + whre + " " + whrclsMilk + " " + whrclsShift + "  and CONVERT(date,DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  group by VLC_Code"

        ElseIf rbtnDateMilkType.IsChecked Then
            FinalQry = " SELECT xxx.Doc_Date, CASE 
        WHEN xxx.Milk_Type = 'C' THEN 'Cow'
        WHEN xxx.Milk_Type = 'B' THEN 'Buffalo'
         WHEN xxx.Milk_Type = 'M' THEN 'MIX'
    END AS Milk_Type,Sum(xxx.Qty)qty,Sum(xxx.Rate)Rate,sum(xxx.Amount)Amount from " + BaseQry + " where 2=2 " + whre + "  " + whrclsMilk + " " + whrclsShift + " and CONVERT(date,DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' group by Doc_Date,Milk_Type "
        ElseIf rbtnDateShiftType.IsChecked Then
            FinalQry = " SELECT xxx.Doc_Date, CASE 
        WHEN xxx.Shift = 'M' THEN 'Morning'
        WHEN xxx.Shift = 'E' THEN 'Evening'
          WHEN xxx.Shift = 'B' THEN 'Both'    

    END AS Shift,Sum(xxx.Qty)qty,Sum(xxx.Rate)Rate,sum(xxx.Amount)Amount
from  " + BaseQry + " where 2=2  " + whre + "" + whrclsMilk + " " + whrclsShift + " and CONVERT(date,DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  group by Doc_Date,Shift "
        ElseIf rbtnDetails.IsChecked Then
            FinalQry = "select TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader
,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_REIL_LOCAL_MILK_SALE.Doc_Date,
TSPL_REIL_LOCAL_MILK_SALE.Shift,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.Milk_Type,
TSPL_REIL_LOCAL_MILK_SALE_DETAIL.QTY,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.RATE,TSPL_REIL_LOCAL_MILK_SALE_DETAIL.Amount from TSPL_REIL_LOCAL_MILK_SALE_DETAIL
left outer join TSPL_REIL_LOCAL_MILK_SALE ON TSPL_REIL_LOCAL_MILK_SALE.PK_ID=TSPL_REIL_LOCAL_MILK_SALE_DETAIL.REF_PK_ID
left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_REIL_LOCAL_MILK_SALE.VLC_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC Where 2=2 " + whre + "" + whrclsMilk + " " + whrclsShift + ""
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQry)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterView.Refresh()
        Gv1.GroupDescriptors.Clear()
        Gv1.EnableFiltering = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        If dt.Rows.Count > 0 Then
            Gv1.DataSource = dt
            Gv1.BestFitColumns()
            'View()
            SetGridFormation()
            'ReStoreGridLayout()
            Gv1.MasterTemplate.AutoExpandGroups = False
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.BestFitColumns()
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub

        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
        End If



    End Sub
    Sub SetGridFormation()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            If rbtnDetails.IsChecked Then
                'gv1.Columns("Document_No").HeaderText = "Document No."
                Gv1.Columns("MCC_Code").IsVisible = False
                Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
                Gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Code Uploader"
                Gv1.Columns("MCC_NAME").IsVisible = True
                Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"


                Gv1.Columns("VLC_Code").IsVisible = False
                Gv1.Columns("VLC_Code").HeaderText = "DCS Code."

                Gv1.Columns("VLC_Name").IsVisible = True
                Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
                'gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
                'gv1.Columns("Tanker_No").HeaderText = "Tanker No."
                Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
                Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS CODE Uploader"

                Gv1.Columns("Doc_Date").IsVisible = True
                Gv1.Columns("Doc_Date").HeaderText = "Documnent date"

                Gv1.Columns("Shift").IsVisible = True
                Gv1.Columns("Shift").HeaderText = "Shift"

                Gv1.Columns("Milk_Type").IsVisible = True
                Gv1.Columns("Milk_Type").HeaderText = "Milk Type"

                Gv1.Columns("QTY").IsVisible = True

                Gv1.Columns("QTY").HeaderText = "Qty"
                Gv1.Columns("RATE").IsVisible = True

                Gv1.Columns("RATE").HeaderText = "RATE"
                Gv1.Columns("Amount").IsVisible = True

                Gv1.Columns("Amount").HeaderText = "Amount"
            ElseIf rbtnBMCWise.IsChecked Then
                Gv1.Columns("MCC_Code").IsVisible = False
                Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
                Gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Code Uploader"
                Gv1.Columns("MCC_NAME").IsVisible = True
                Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"


                Gv1.Columns("Qty").IsVisible = True
                Gv1.Columns("Qty").HeaderText = "Qty"
                Gv1.Columns("Rate").IsVisible = True
                Gv1.Columns("Rate").HeaderText = "Rate"
                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").HeaderText = "Amount"
            ElseIf rbtnDCSWise.IsChecked Then

                Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
                Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code Uploader"
                Gv1.Columns("MCC_Code").IsVisible = False
                Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
                Gv1.Columns("MCC_NAME").IsVisible = True
                Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"
                Gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Code Uploader"
                Gv1.Columns("VLC_Code").IsVisible = False
                Gv1.Columns("VLC_Code").HeaderText = "DCS Code"
                Gv1.Columns("VLC_Name").IsVisible = True
                Gv1.Columns("VLC_Name").HeaderText = "DCS Name"

                Gv1.Columns("QTY").IsVisible = True
                Gv1.Columns("QTY").HeaderText = "QTY"
                Gv1.Columns("Rate").IsVisible = True
                Gv1.Columns("Rate").HeaderText = "Rate"
                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").HeaderText = "Amount"
            ElseIf rbtnDateMilkType.IsChecked Then
                Gv1.Columns("Doc_Date").IsVisible = True
                Gv1.Columns("Doc_Date").HeaderText = "Document Date"
                Gv1.Columns("Milk_Type").IsVisible = True
                Gv1.Columns("Milk_Type").HeaderText = "Milk Type"
                Gv1.Columns("qty").IsVisible = True
                Gv1.Columns("qty").HeaderText = "Qty"
                Gv1.Columns("Rate").IsVisible = True
                Gv1.Columns("Rate").HeaderText = "Rate"
                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").HeaderText = "Amount"
            ElseIf rbtnDateShiftType.IsChecked Then
                Gv1.Columns("Doc_Date").IsVisible = True
                Gv1.Columns("Doc_Date").HeaderText = "Document Date"
                Gv1.Columns("Shift").IsVisible = True
                Gv1.Columns("Shift").HeaderText = "Shift"
                Gv1.Columns("qty").IsVisible = True
                Gv1.Columns("qty").HeaderText = "Qty"
                Gv1.Columns("Rate").IsVisible = True
                Gv1.Columns("Rate").HeaderText = "Rate"
                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").HeaderText = "Amount"
            End If
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)
        Dim Amount As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Amount)
        Dim Qty As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Qty)
        'Dim fat_per As New GridViewSummaryItem("fat_per", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(fat_per)
        'Dim snf_Per As New GridViewSummaryItem("snf_Per", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(snf_Per)
        'Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Entered_SNFKg)
        'Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Entered_FATKg)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub


    Private Sub FrmLocalMilkSaleRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmLocalMilkSaleRegister & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Sub Reset()
        txtMultiBMC.arrValueMember = Nothing
        TxtMultiDCS.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadPageViewPage1_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub
End Class