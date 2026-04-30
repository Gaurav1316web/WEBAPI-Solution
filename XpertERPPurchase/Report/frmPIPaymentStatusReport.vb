Imports common
Public Class frmPIPaymentStatusReport
    Private Sub frmPIPaymentStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub funreset()
        ReportGrid()
        'ReportDetailsGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
        'RadPageViewPage3.Visible = False
        txtLocation.arrValueMember = Nothing
        TxtRAL.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        TxtVendor.arrValueMember = Nothing
        EnableDisableCtrl(True)
    End Sub

    Sub ReportGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
        gv1.ReadOnly = True
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 AndAlso clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID & "_RS", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If obj IsNot Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            If objCommonVar.ApplyLocationFilterBasedOnPermission AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("@Loc", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            Dim qry As String = " select DocumentCode As RALNo, Convert(Varchar(10),DocumentDate,103) As [Document Date]  from TSPL_TENDER_Header "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("@RAL", qry, "RALNo", "", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " Select Item_Code as Code,Item_Desc as Name,Short_Description,Structure_Code from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("@Item", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("@Vendor", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strQry As String = ";With BaseQry As (" & ReturnBaseQry() & ")"
            strQry &= " Select Row_Number() Over (Order By (Select 1)) As [S.No.], PI_No As [PI No.],Max(PI_Date) As [PI Date],
Max(Case When Status=1 Then 'Approved' Else 'Pending' End) As [PI Status],Max(Bill_To_Location) As [Location],Max(Vendor_Code) As [Vendor Code],
Max(Vendor_Name) As [Vendor Name],Max(Item_Code) As [Item Code],Max(Item_Desc) As [Item Name],Max(Item_Cost) As [Item Rate],
Max(Ref_No) As [RAL No.],Sum(PI_Qty) As [Qty In QTL],Sum(JuteBagWeight) As [Jute Bag Weight],Sum(PPBagWeight) As [HDPE Bag Weight],Sum(NetQtyInQtl) As [Net Qty In QTL],Sum(Amount) As [Amount (Rs.)]
,Sum(QualityAmt) As [Quality Amt],Sum(SecurityAmt) As [Security Amt],Sum(Penalty) As [Late Penalty Amt],Sum(Ded_Amt) As [Total Deduction Amt],
Sum(Case 
When Tax_Master1.Type='CGST' Then BaseQry.TAX1_Amt
When Tax_Master2.Type='CGST' Then BaseQry.TAX2_Amt
When Tax_Master3.Type='CGST' Then BaseQry.TAX3_Amt
When Tax_Master4.Type='CGST' Then BaseQry.TAX4_Amt
When Tax_Master5.Type='CGST' Then BaseQry.TAX5_Amt
When Tax_Master6.Type='CGST' Then BaseQry.TAX6_Amt
When Tax_Master7.Type='CGST' Then BaseQry.TAX7_Amt
When Tax_Master8.Type='CGST' Then BaseQry.TAX8_Amt
When Tax_Master9.Type='CGST' Then BaseQry.TAX9_Amt
When Tax_Master10.Type='CGST' Then BaseQry.TAX10_Amt Else 0 End) As [CGST Amt],
Sum(Case 
When Tax_Master1.Type='SGST' Then BaseQry.TAX1_Amt
When Tax_Master2.Type='SGST' Then BaseQry.TAX2_Amt
When Tax_Master3.Type='SGST' Then BaseQry.TAX3_Amt
When Tax_Master4.Type='SGST' Then BaseQry.TAX4_Amt
When Tax_Master5.Type='SGST' Then BaseQry.TAX5_Amt
When Tax_Master6.Type='SGST' Then BaseQry.TAX6_Amt
When Tax_Master7.Type='SGST' Then BaseQry.TAX7_Amt
When Tax_Master8.Type='SGST' Then BaseQry.TAX8_Amt
When Tax_Master9.Type='SGST' Then BaseQry.TAX9_Amt
When Tax_Master10.Type='SGST' Then BaseQry.TAX10_Amt Else 0 End) As [SGST Amt],
Sum(Case 
When Tax_Master1.Type='IGST' Then BaseQry.TAX1_Amt
When Tax_Master2.Type='IGST' Then BaseQry.TAX2_Amt
When Tax_Master3.Type='IGST' Then BaseQry.TAX3_Amt
When Tax_Master4.Type='IGST' Then BaseQry.TAX4_Amt
When Tax_Master5.Type='IGST' Then BaseQry.TAX5_Amt
When Tax_Master6.Type='IGST' Then BaseQry.TAX6_Amt
When Tax_Master7.Type='IGST' Then BaseQry.TAX7_Amt
When Tax_Master8.Type='IGST' Then BaseQry.TAX8_Amt
When Tax_Master9.Type='IGST' Then BaseQry.TAX9_Amt
When Tax_Master10.Type='IGST' Then BaseQry.TAX10_Amt Else 0 End) As [IGST Amt],
Sum(TDS) As [TDS Amt],Sum(RoundOffAmt) As [Round Off Amt],Sum(Item_Net_Amt) As [Net Payable Amt],Max(Payment_No) As [Payment No.],Max(Payment_Date) As [Payment Date],Max(Payment_Amount) As [Payment Amt],Max(PaymentStatus) As [Status] 
from BaseQry 
Left Join TSPL_TAX_MASTER As Tax_Master1 On Tax_Master1.Type=BaseQry.TAX1
Left Join TSPL_TAX_MASTER As Tax_Master2 On Tax_Master2.Type=BaseQry.TAX2
Left Join TSPL_TAX_MASTER As Tax_Master3 On Tax_Master3.Type=BaseQry.TAX3
Left Join TSPL_TAX_MASTER As Tax_Master4 On Tax_Master4.Type=BaseQry.TAX4
Left Join TSPL_TAX_MASTER As Tax_Master5 On Tax_Master5.Type=BaseQry.TAX5
Left Join TSPL_TAX_MASTER As Tax_Master6 On Tax_Master6.Type=BaseQry.TAX6
Left Join TSPL_TAX_MASTER As Tax_Master7 On Tax_Master7.Type=BaseQry.TAX7
Left Join TSPL_TAX_MASTER As Tax_Master8 On Tax_Master8.Type=BaseQry.TAX8
Left Join TSPL_TAX_MASTER As Tax_Master9 On Tax_Master9.Type=BaseQry.TAX9
Left Join TSPL_TAX_MASTER As Tax_Master10 On Tax_Master10.Type=BaseQry.TAX10
Group By PI_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ReportGrid()
                gv1.DataSource = dt
                gv1.EnableFiltering = True
                gv1.ShowFilteringRow = True
                ReStoreGridLayout()
                gv1.ReadOnly = True
                View()
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
                gv1.Columns("Location").IsVisible = False
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim item1 As New GridViewSummaryItem("RAL Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item1)
                'Dim item2 As New GridViewSummaryItem("GRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item2)
                'Dim item3 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item3)

                'gv1.MasterTemplate.AutoExpandGroups = True
                'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                'gv1.MasterTemplate.ShowTotals = True
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnBaseQry() As String
        Dim qry As String = "Select TSPL_PI_HEAD.PI_No,Convert(Varchar(10),TSPL_PI_HEAD.PI_Date,103) As PI_Date,TSPL_PI_HEAD.Status,TSPL_PI_HEAD.Bill_To_Location,TSPL_PI_HEAD.Vendor_Code,TSPL_PI_HEAD.Vendor_Name,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Item_Desc,TSPL_PI_DETAIL.Item_Cost,TSPL_PI_HEAD.Ref_No,TSPL_PI_DETAIL.PI_Qty,TSPL_PI_DETAIL.Unit_code,TSPL_PO_WEIGHTMENT_GUNNY.JuteBagWeight,TSPL_PO_WEIGHTMENT_GUNNY.PPBagWeight,
TSPL_PI_DETAIL.PI_Qty- Case When TSPL_PO_WEIGHTMENT_GUNNY.JuteBagWeight>0 Then TSPL_PO_WEIGHTMENT_GUNNY.JuteBagWeight
Else TSPL_PO_WEIGHTMENT_GUNNY.PPBagWeight End As NetQtyInQtl,TSPL_PI_DETAIL.Amount,isnull (TSPL_SRN_DEDUCTION.Ded_Amt,0) as QualityAmt,isnull(TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt,0) as SecurityAmt,isnull(TSPL_SRN_TENDER.Penalty,0) as Penalty,isnull(TSPL_SRN_DEDUCTION.Ded_Amt,0) as Ded_Amt,
TSPL_PI_DETAIL.TAX1,TSPL_PI_DETAIL.TAX1_Amt,TSPL_PI_DETAIL.TAX2,TSPL_PI_DETAIL.TAX2_Amt,TSPL_PI_DETAIL.TAX3,TSPL_PI_DETAIL.TAX3_Amt,TSPL_PI_DETAIL.TAX4,TSPL_PI_DETAIL.TAX4_Amt,TSPL_PI_DETAIL.TAX5,TSPL_PI_DETAIL.TAX5_Amt,TSPL_PI_DETAIL.TAX6,TSPL_PI_DETAIL.TAX6_Amt,TSPL_PI_DETAIL.TAX7,TSPL_PI_DETAIL.TAX7_Amt,TSPL_PI_DETAIL.TAX8,TSPL_PI_DETAIL.TAX8_Amt,TSPL_PI_DETAIL.TAX9,TSPL_PI_DETAIL.TAX9_Amt,TSPL_PI_DETAIL.TAX10,TSPL_PI_DETAIL.TAX10_Amt,
cast(case when isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0)>0 and isnull( tspl_pi_detail .Taxable_Amount,0)>0 then
                         ( isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0) 
                          / (select sum(isnull( tspl_pi_detail .Taxable_Amount,0)) from  tspl_pi_detail  where PI_NO=TSPL_PI_HEAD.PI_No  ))
                          * isnull(  tspl_pi_detail .Taxable_Amount,0) else 0 end as decimal(18,2)) as TDS,TSPL_PI_HEAD.RoundOffAmt,
						  TSPL_PI_DETAIL.Item_Net_Amt,TSPL_PAYMENT_HEADER.Payment_No,Convert(Varchar(10),TSPL_PAYMENT_HEADER.Payment_Date,103) As Payment_Date,TSPL_PAYMENT_HEADER.Payment_Amount,Case When TSPL_PAYMENT_HEADER.Posted=1 Then 'Approved' Else 'Pending' End As PaymentStatus
 from TSPL_PI_DETAIL 
left outer join TSPL_PI_HEAD On TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No
LEFT Outer Join TSPL_TENDER_HEADER On TSPL_TENDER_HEADER.DocumentCode=TSPL_PI_HEAD.Ref_No
left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No=tspl_pi_detail .SRN_Id and TSPL_SRN_DETAIL.Item_Code= tspl_pi_detail.Item_Code
left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
left outer join TSPL_GRN_HEAD On TSPL_GRN_HEAD.GRN_No=TSPL_PI_HEAD.Against_GRN
left outer join TSPL_PO_WEIGHTMENT_HEAD On TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No

left join (SELECT Weighment_Code, ISNULL([PM0001],0) AS 'JuteBagWeight',ISNULL([PM0002],0) AS 'PPBagWeight'  FROM (SELECT TSPL_PO_WEIGHTMENT_GUNNY.Weighment_Code,TSPL_PO_WEIGHTMENT_GUNNY.Item_Code,ROUND(CAST(ISNULL(TSPL_PO_WEIGHTMENT_GUNNY.Qty,0)/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)/100 AS decimal(18,3)),2) AS QTY FROM   TSPL_PO_WEIGHTMENT_GUNNY
					left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PO_WEIGHTMENT_GUNNY.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' ) AS PO_WEIGHTMENT_GUNNY
					PIVOT (SUM(QTY) FOR ITEM_CODE IN ([PM0001],[PM0002])) AS TSPL_PO_WEIGHTMENT_GUNNY) TSPL_PO_WEIGHTMENT_GUNNY on TSPL_PO_WEIGHTMENT_GUNNY.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No = tspl_pi_detail .SRN_Id and TSPL_SRN_DEDUCTION.Item_Code = tspl_pi_detail .Item_Code
  left join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION_SECURITY.Item_Code=TSPL_SRN_Detail.Item_Code
left outer join (SELECT SRN_NO,Item_Code,ISNULL(SUM(Penalty),0) AS Penalty FROM TSPL_SRN_TENDER GROUP BY SRN_NO,Item_Code)TSPL_SRN_TENDER on TSPL_SRN_TENDER.SRN_No =   tspl_pi_detail .SRN_Id and TSPL_SRN_TENDER.Item_Code=tspl_pi_detail .Item_Code
left outer join TSPL_PI_REMITTANCE on TSPL_PI_REMITTANCE.Document_No=  TSPL_PI_HEAD.pi_no
Left Outer Join TSPL_VENDOR_INVOICE_HEAD On TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.pi_no
Left Outer Join TSPL_PAYMENT_DETAIL On TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
Left Outer Join TSPL_PAYMENT_HEADER On TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No
where TSPL_TENDER_HEADER.DocumentDate>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And TSPL_TENDER_HEADER.DocumentDate<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'"
        If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
            qry &= " And TSPL_PI_HEAD.Vendor_Code In (" & clsCommon.GetMulcallString(TxtVendor.arrValueMember) & ")"
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry &= " And TSPL_PI_HEAD.Bill_To_Location In (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ") "
        End If
        If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
            qry &= " And TSPL_PI_HEAD.Ref_No In (" & clsCommon.GetMulcallString(TxtRAL.arrValueMember) & ")"
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            qry &= " And TSPL_PI_DETAIL.Item_Code  In (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ")"
        End If
        Return qry
    End Function

    Sub View()
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("S.No.").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("UnionName").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("location_Code").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Weighment_Date").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("ROUTE_NO").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Purchase Invoice Details"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("PI No.").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("PI Date").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("PI Status").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Location").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Vendor Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Vendor Name").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Item Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Item Name").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Item Rate").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("RAL No.").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity Details"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Qty In QTL").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Jute Bag Weight").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("HDPE Bag Weight").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Net Qty In QTL").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Amount (Rs.)").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Deduction Details"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Quality Amt").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Security Amt").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Late Penalty Amt").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Total Deduction Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Tax Details"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("CGST Amt").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("SGST Amt").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("IGST Amt").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("TDS Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Payable Amount"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Round Off Amt").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Net Payable Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Payment Status"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Payment No.").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Payment Date").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Payment Amt").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Status").Name)
            gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("PI No.").Value)) > 0 Then
                clsPurchaseInvoiceHead.funPIPrint(MyBase.Form_ID, False, clsCommon.myCstr(gv1.CurrentRow.Cells("PI Date").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("PI No.").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Item Code").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Location").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("RAL No.").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Vendor Code").Value), Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo, ByVal gv As RadGridView)
        Try
            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strtemp As String = "Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
                'End If
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("PI Payment Status Report", gv, arrHeader, Me.Text, False)
                Else
                    clsCommon.MyExportToPDF("PI Payment Status Report", gv, arrHeader, "PI Payment Status Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            ExportToExcel(EnumExportTo.Excel, gv1)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            ExportToExcel(EnumExportTo.PDF, gv1)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class