Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common


Public Class FrmFreightCosting
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFreightCosting)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub FrmFreightCosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        LoadSrn()
        LoadVendor()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData()
    End Sub
    Public Sub printData()
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim SrnArr As ArrayList = cbgPoInvoice.CheckedValue
            Dim VendorArr As ArrayList = cbgVendor.CheckedValue
            Dim Vendor As String = ""
            Dim Srno As String = ""
            Dim StrVendor As String = ""
            Dim StrSrno As String = ""
            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
                Vendor = "'" + clsCommon.GetMulcallString(VendorArr) + "'"
                StrVendor = Vendor.Replace("'", "")
            End If
            If chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count > 0 Then
                Srno = "'" + clsCommon.GetMulcallString(SrnArr) + "'"
                StrSrno = Srno.Replace("'", "")

            End If
            qry = "select  '" + fromdate + "' as FromDate,'" + Todate + "' as ToDate, '" + StrVendor + "' as StrVendor,'" + StrSrno + "' as StrSrno,xxx.PI_Date as [PI Date],xxx.[SRN NO] ,xxx.[PI NO] ,xxx.[Vendor Name] ,xxx.[SRN Qty] ,xxx.[FA Post] ,xxx.Freight ,xxx.OtherExp, (xxx.[FA Post]+xxx.Freight +xxx.OtherExp ) as TotalCost,((xxx.[FA Post]+xxx.Freight +xxx.OtherExp)/xxx.[SRN Qty] ) as [Per Quantity Cost]," & _
                 "   TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code =xxx.Bill_To_Location )  as address  From " & _
                 " (select max(xx.PI_Date) as PI_Date  ,xx.SRNNo as [SRN NO],(xx.PINo )as [PI NO],max(xx.[Vendor Name]) as [Vendor Name] ,sum(xx.SRNQTY) as [SRN Qty],sum(xx.[FA Post]) as [FA Post]  ,sum(xx.Freight) as Freight  ,sum(xx.OtherExp)as OtherExp ,max(xx.Comp_Code)as Comp_Code,max(xx.Bill_To_Location) as Bill_To_Location from " & _
                 "( select APhead.Vendor_Invoice_Date AS PI_Date ,Aphead.RefDocNo     as SRNNo,APhead.Document_No  as PINo,(select sum(SRN_Qty)  from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL .SRN_No =APhead .RefDocNo  and APhead .RefDocType  ='S'  ) as SRNQTY,APhead .Vendor_Name as [Vendor Name],(  select sum(Amount_Less_Discount ) from TSPL_SRN_head  where TSPL_SRN_HEAD .SRN_No =APhead .RefDocNo  and APhead .RefDocType  ='S') as [FA Post]," & _
                 " (Case when Add1.FreightCharges ='Y'then APhead .Add_Charge_Amt1   else 0 end+Case when Add2.FreightCharges ='Y'then APhead.Add_Charge_Amt2  else 0 end+Case when Add3.FreightCharges ='Y'then APhead.Add_Charge_Amt3  else 0 end+Case when Add4.FreightCharges ='Y'then APhead.Add_Charge_Amt4  else 0 end+Case when Add5.FreightCharges ='Y'then APhead.Add_Charge_Amt5  else 0 end+Case when Add6.FreightCharges ='Y'then APhead.Add_Charge_Amt6  else 0 end+Case when Add7.FreightCharges ='Y'then APhead.Add_Charge_Amt7  else 0 end+Case when Add8.FreightCharges ='Y'then APhead.Add_Charge_Amt8  else 0 end+Case when Add9.FreightCharges ='Y'then APhead.Add_Charge_Amt9  else 0 end+Case when Add10.FreightCharges ='Y'then APhead.Add_Charge_Amt10  else 0 end)as Freight, (Case when Add1.FreightCharges ='N'then APhead.Add_Charge_Amt1   else 0 end+Case when Add2.FreightCharges ='N'then APhead.Add_Charge_Amt2  else 0 end+Case when Add3.FreightCharges ='N'then APhead.Add_Charge_Amt3  else 0 end+Case when Add4.FreightCharges ='N'then APhead.Add_Charge_Amt4  else 0 end+Case when Add5.FreightCharges ='N'then APhead.Add_Charge_Amt5  else 0 end+Case when Add6.FreightCharges ='N'then APhead.Add_Charge_Amt6  else 0 end+Case when Add7.FreightCharges ='N'then APhead.Add_Charge_Amt7  else 0 end+Case when Add8.FreightCharges ='N'then APhead.Add_Charge_Amt8  else 0 end+Case when Add9.FreightCharges ='N'then APhead.Add_Charge_Amt9  else 0 end+Case when Add10.FreightCharges ='N'then APhead.Add_Charge_Amt10  else 0 end)as OtherExp, APhead.Comp_Code, '' as Bill_To_Location " & _
                 "  from TSPL_VENDOR_INVOICE_HEAD   as APhead   left outer join TSPL_Additional_Charges  as Add1 on APhead.Add_charge_Code1=Add1.Code left outer join TSPL_Additional_Charges  as Add2 on APhead.Add_charge_Code2=Add2.Code left outer join TSPL_Additional_Charges  as Add3 on APhead.Add_charge_Code3=Add3.Code left outer join TSPL_Additional_Charges  as Add4 on APhead.Add_charge_Code4=Add4.Code left outer join TSPL_Additional_Charges  as Add5 on APhead.Add_charge_Code5=Add5.Code left outer join TSPL_Additional_Charges  as Add6 on APhead.Add_charge_Code6=Add6.Code left outer join TSPL_Additional_Charges  as Add7 on APhead.Add_charge_Code7=Add7.Code left outer join TSPL_Additional_Charges  as Add8 on APhead.Add_charge_Code8=Add8.Code left outer join TSPL_Additional_Charges  as Add9 on APhead.Add_charge_Code9=Add9.Code left outer join TSPL_Additional_Charges  as Add10 on APhead.Add_charge_Code10=Add10.Code"

            qry += " where  Convert(date,APhead.Vendor_Invoice_Date ,103) >=Convert(date,'" & dtpfromdate.Value & "',103) and Convert(Date,APhead.Vendor_Invoice_Date ,103) <=Convert(date,'" & dtpToDate.Value & "',103)and APhead .RefDocType ='S'"
            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                Return
            ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
                qry += " and APhead.Vendor_Code  in (" + clsCommon.GetMulcallString(VendorArr) + ")"
            End If
            If chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one SRN No")
                Return
            ElseIf chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count > 0 Then

                qry += " and  APhead.RefDocNo in (" + clsCommon.GetMulcallString(SrnArr) + ")  "

            End If
            qry += " union all" & _
                  " select pihead.PI_Date ,Pihead.Against_SRN   as SRNNo,pihead.PI_No as PINo, (select Sum(SRN_Qty)  from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL .SRN_No =Pihead.Against_SRN) as SRNQTY, pihead.Vendor_Name as [Vendor Name],(select sum(Amount_Less_Discount ) from tspl_srn_Head where tspl_srn_head.SRN_No =Pihead .Against_SRN ) as [FA Post]," & _
                  "(Case when Add1.FreightCharges ='Y'then Pihead.Add_Charge_Amt1   else 0 end+Case when Add2.FreightCharges ='Y'then Pihead.Add_Charge_Amt2  else 0 end+Case when Add3.FreightCharges ='Y'then Pihead.Add_Charge_Amt3  else 0 end+Case when Add4.FreightCharges ='Y'then Pihead.Add_Charge_Amt4  else 0 end+Case when Add5.FreightCharges ='Y'then Pihead.Add_Charge_Amt5  else 0 end+Case when Add6.FreightCharges ='Y'then Pihead.Add_Charge_Amt6  else 0 end+Case when Add7.FreightCharges ='Y'then Pihead.Add_Charge_Amt7  else 0 end+Case when Add8.FreightCharges ='Y'then Pihead.Add_Charge_Amt8  else 0 end+Case when Add9.FreightCharges ='Y'then Pihead.Add_Charge_Amt9  else 0 end+Case when Add10.FreightCharges ='Y'then Pihead.Add_Charge_Amt10  else 0 end)as Freight, (Case when Add1.FreightCharges ='N'then Pihead.Add_Charge_Amt1   else 0 end+Case when Add2.FreightCharges ='N'then Pihead.Add_Charge_Amt2  else 0 end+Case when Add3.FreightCharges ='N'then Pihead.Add_Charge_Amt3  else 0 end+Case when Add4.FreightCharges ='N'then Pihead.Add_Charge_Amt4  else 0 end+Case when Add5.FreightCharges ='N'then Pihead.Add_Charge_Amt5  else 0 end+Case when Add6.FreightCharges ='N'then Pihead.Add_Charge_Amt6  else 0 end+Case when Add7.FreightCharges ='N'then Pihead.Add_Charge_Amt7  else 0 end+Case when Add8.FreightCharges ='N'then Pihead.Add_Charge_Amt8  else 0 end+Case when Add9.FreightCharges ='N'then Pihead.Add_Charge_Amt9  else 0 end+Case when Add10.FreightCharges ='N'then Pihead.Add_Charge_Amt10  else 0 end)as OtherExp, Pihead.Comp_Code, Pihead.Bill_To_Location " & _
                  " from  TSPL_PI_HEAD  as Pihead  left outer join TSPL_Additional_Charges  as Add1 on pihead.Add_charge_Code1=Add1.Code left outer join TSPL_Additional_Charges  as Add2 on pihead.Add_charge_Code2=Add2.Code left outer join TSPL_Additional_Charges  as Add3 on pihead.Add_charge_Code3=Add3.Code left outer join TSPL_Additional_Charges  as Add4 on pihead.Add_charge_Code4=Add4.Code left outer join TSPL_Additional_Charges  as Add5 on pihead.Add_charge_Code5=Add5.Code left outer join TSPL_Additional_Charges  as Add6 on pihead.Add_charge_Code6=Add6.Code left outer join TSPL_Additional_Charges  as Add7 on pihead.Add_charge_Code7=Add7.Code left outer join TSPL_Additional_Charges  as Add8 on pihead.Add_charge_Code8=Add8.Code left outer join TSPL_Additional_Charges  as Add9 on pihead.Add_charge_Code9=Add9.Code left outer join TSPL_Additional_Charges  as Add10 on pihead.Add_charge_Code10=Add10.Code"

            qry += " where  Convert(date,pihead.PI_Date,103) >=Convert(date,'" & dtpfromdate.Value & "',103) and Convert(Date,pihead.PI_Date,103) <=Convert(date,'" & dtpToDate.Value & "',103) "

            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                Return
            ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
                qry += " and pihead .Vendor_Code  in (" + clsCommon.GetMulcallString(VendorArr) + ")"
            End If
            If chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one SRN No")
                Return
            ElseIf chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count > 0 Then

                qry += " and  Pihead.Against_SRN in (" + clsCommon.GetMulcallString(SrnArr) + ")  "

            End If

            qry += "   ) as xx group By xx.SRNNo,xx.PINo  )as xxx left outer join TSPL_COMPANY_MASTER on  xxx.Comp_Code  =TSPL_COMPANY_MASTER .Comp_Code  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else

                dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "FreightCosting", "Freight Costing")
                frmCRV = Nothing

            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub LoadSrn()
        Dim Qry As String = "select distinct tspl_vendor_invoice_head.RefDocNo As Code  From TSPL_VENDOR_INVOICE_HEAD inner join TSPL_SRN_HEAD on TSPL_VENDOR_INVOICE_HEAD .RefDocNo =TSPL_SRN_HEAD .SRN_No  where TSPL_SRN_HEAD .Status ='1' union all select tspl_srn_head.srn_no as Code From tspl_srn_head  inner  join TSPL_PI_HEAD on TSPL_SRN_HEAD .SRN_No =TSPL_PI_HEAD .Against_SRN where TSPL_SRN_HEAD .Status ='1' and TSPL_SRN_HEAD .SRN_No not in (select tspl_vendor_invoice_head.RefDocNo  From TSPL_VENDOR_INVOICE_HEAD )"
        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPoInvoice.ValueMember = "Code"
    End Sub
    Public Sub LoadVendor()
        Dim Qry As String = "select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgVendor.ValueMember = "Code"
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        LoadVendor()
        LoadSrn()

    End Sub
    Private Sub chkPoInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPoInvoiceAll.ToggleStateChanged
        cbgPoInvoice.Enabled = Not chkPoInvoiceAll.IsChecked
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    
    Private Sub FrmFreightCosting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

 
End Class
