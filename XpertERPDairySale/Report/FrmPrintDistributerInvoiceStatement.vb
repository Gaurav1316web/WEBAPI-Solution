Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'' ERO/05/04/19-000545 (work on only on report format in case of taxable invoice)
Public Class FrmPrintDistributerInvoiceStatement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim strQry As String = ""
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Dim ShowShipToPartyInDairyDispatch As Integer = 0
    Dim AllowSeperateSchemeItemOnPrint As Boolean = False
    Dim strRptPath As String = ""
    Dim ApplyMilkPouchPrint As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPrintDistributerInvoiceStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Public Sub loadReport()
        Dim WhrCls As String = " and 2=2 "
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
        '    Exit Sub
        'End If
        If clsCommon.myCDate(txtFromDate.Value) >= objCommonVar.GSTApplicableDate AndAlso clsCommon.myCDate(txtToDate.Value) >= objCommonVar.GSTApplicableDate Then
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Report Type.", Me.Text)
                cboReportType.Focus()
                Exit Sub
            End If
        End If


        Dim sQuery As String = " select Cast(1 as BIT) as 'Check', Document_Code ,convert(varchar,Document_Date,103) as Document_Date ,Customer_Code,Customer_Name  ,Location_Desc ,Total_Amt  from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_LOCATION_MASTER.State where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS' "


        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State  and 0= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "LWM") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State and 1 <= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "UT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State=TSPL_CUSTOMER_MASTER.State and isnull(TSPL_STATE_MASTER.Is_GST_UT,0)=1"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IT") = CompairStringResult.Equal Then
            sQuery += "and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State<>TSPL_CUSTOMER_MASTER.State and 0= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "IWM") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=1 and TSPL_LOCATION_MASTER.State<>TSPL_CUSTOMER_MASTER.State and 1 <= (select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code=TSPL_SD_SALE_INVOICE_HEAD.Tax_Group and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y'))"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "NT") = CompairStringResult.Equal Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.is_taxable=0 "
        End If

        sQuery += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)"
        '==============update by preeti gupta Against Ticket No [BM00000005410]

        'If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        'End If
        If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
            WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
        End If

        ''RICHA AGARWAL 19 jUNE,2019 SHOW ROUTE NO FROM TSPL_SD_SALE_INVOICE_HEAD INSTEAD OF CUSTOMER MASTER ERO/18/06/19-000646
        If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
            WhrCls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
        End If

        If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
            WhrCls += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
        End If

        sQuery += WhrCls

        sQuery += " order by TSPL_SD_SALE_INVOICE_HEAD.Document_Date"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
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
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Check").IsVisible = True
        gv.Columns("Check").Width = 100
        gv.Columns("Check").HeaderText = " "
        gv.Columns("Check").ReadOnly = False



        gv.Columns("Document_Code").IsVisible = True
        gv.Columns("Document_Code").Width = 100
        gv.Columns("Document_Code").HeaderText = "Sale Invoice No."



        gv.Columns("Document_Date").IsVisible = True
        gv.Columns("Document_Date").Width = 100
        gv.Columns("Document_Date").HeaderText = " Date"
        'gv.Columns("Document_Date").FormatString = "{0:d}"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location"

        gv.Columns("Customer_Code").IsVisible = True
        gv.Columns("Customer_Code").Width = 100
        gv.Columns("Customer_Code").HeaderText = "Customer Code"

        gv.Columns("Customer_Name").IsVisible = True
        gv.Columns("Customer_Name").Width = 150
        gv.Columns("Customer_Name").HeaderText = "Customer"



        gv.Columns("Total_Amt").IsVisible = True
        gv.Columns("Total_Amt").Width = 100
        gv.Columns("Total_Amt").HeaderText = "Amount"

        'gv.Columns("DespatchDocumentNo").IsVisible = False
        'gv.Columns("DespatchDocumentNo").Width = 100
        'gv.Columns("DespatchDocumentNo").HeaderText = "DespatchDocumentNo"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)



        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadLocation()
        'chkLocationAll.CheckState = CheckState.Checked
        txtMultCustomer.arrValueMember = Nothing
        txtMultRoute.arrValueMember = Nothing
        txtMultLocation.arrValueMember = Nothing
        cboReportType.SelectedValue = ""
        btnPrePrintFormat.Visible = False
        gv.DataSource = Nothing
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
            btnBatchWiseInvoice.Visible = True
        Else
            btnBatchWiseInvoice.Visible = False
        End If
        fndCustom.Value = ""
        lblCustomer.Text = ""
        If ApplyMilkPouchPrint = True Then
            MyLabel1.Visible = False
            txtMultCustomer.Visible = False
            MyLabel4.Visible = False
            cboReportType.Visible = False
            lblLocation.Visible = False
            txtMultLocation.Visible = False
            lblRoute.Visible = False
            txtMultRoute.Visible = False
            btnGo.Visible = False
            btnUnSelect.Visible = False
            RadSplitButton1.Visible = False
            BtnPrintChallan.Visible = False
            BtnEmailSms.Visible = False
            btnPrePrintFormat.Visible = False
            btnBatchWiseInvoice.Visible = False
            btnCombinedInvoice.Visible = False
            BtnPrint.Visible = True
            BtnPrint.Enabled = True
            BtnReset.Visible = True
            BtnReset.Enabled = True
        Else
            MyLabel2.Visible = False
            fndCustom.Visible = False
            lblCustomer.Visible = False
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-Select-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LT"
        dr("Name") = "Local Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LWM"
        dr("Name") = "Local With MandiTax"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IT"
        dr("Name") = "Interstate Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IWM"
        dr("Name") = "Interstate With MandiTax"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "UT"
        dr("Name") = "UT Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "NT"
        dr("Name") = "NonTaxable"
        dt.Rows.Add(dr)

        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"
        cboReportType.SelectedValue = ""
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        If ApplyMilkPouchPrint = True Then
            Try
                'If txtFromDate.ToString("yyyyMM") < txtToDate.ToString("yyyyMM") Then
                '    clsCommon.MyMessageBoxShow("From Date and To Date should be same month of year.", Me.Text)
                '    Return
                'ElseIf txtFromDate.ToString("yyyyMM") > txtToDate.ToString("yyyyMM") Then
                '    clsCommon.MyMessageBoxShow("From Date and To Date should be same month of year.", Me.Text)
                '    Return
                'End If
                'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                If clsCommon.CompairString(clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"), clsCommon.GetPrintDate(txtToDate.Value, "MMM-yyyy")) <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "From Date and To Date should be same month of year.", Me.Text)
                    Return
                End If

                If clsCommon.myLen(fndCustom.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Customer First", Me.Text)
                    Return
                End If

                Dim strMonth As String = clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy")
                Dim strCurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy")
                Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")
                Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")
                Dim strMonthlyQry As String = " select top 1 isnull (TSPL_SD_SALE_INVOICE_HEAD.MonthlySaleInvoiceNo,'')   FROM   TSPL_SD_SALE_INVOICE_DETAIL 
                                                left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code  
                                                inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  
                                                where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS'  
                                                and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code= '" + fndCustom.Value + "' and TSPL_ITEM_MASTER.Is_Milk_Pouch =1  and TSPL_SD_SALE_INVOICE_HEAD.Status =1 and len( isnull (TSPL_SD_SALE_INVOICE_HEAD.MonthlySaleInvoiceNo,'')) > 0 "
                Dim ExistMonthlySaleInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strMonthlyQry))
                If clsCommon.myLen(ExistMonthlySaleInvoiceNo) > 0 Then
                Else
                    ExistMonthlySaleInvoiceNo = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.rptMonthlySalesInvoice, "", "")

                End If
                Dim qryUpdate As String = "  update  TSPL_SD_SALE_INVOICE_HEAD  set MonthlySaleInvoiceNo = '" + ExistMonthlySaleInvoiceNo + "'  where  Document_Code in (
                                             select distinct TSPL_SD_SALE_INVOICE_HEAD.Document_Code   FROM   TSPL_SD_SALE_INVOICE_DETAIL 
                                             left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code  
                                             inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  
                                             where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS'  
                                             and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code= '" + fndCustom.Value + "' and TSPL_ITEM_MASTER.Is_Milk_Pouch =1  and TSPL_SD_SALE_INVOICE_HEAD.Status =1 and  len (isnull (MonthlySaleInvoiceNo,'')) <=0 ) "
                clsDBFuncationality.ExecuteNonQuery(qryUpdate)
                ' strMonth, strCurrentDate , strFromDate,strToDate 
                Dim qry As String = "  select '" + strMonth + "' as Month , '" + strCurrentDate + "' as CurrentDate , '" + strFromDate + "' as FromDate , '" + strToDate + "' as ToDate, '" + ExistMonthlySaleInvoiceNo + "' as MonthlySaleInvoiceNo,  max(Comp_Code) as Comp_Code,  max(Comp_Name ) as Comp_Name ,  max(Comp_Add1) as Comp_Add1, max(Comp_Add2) as Comp_Add2,max( Comp_Add3) as Comp_Add3, max(Comp_City_Code ) as Comp_City_Code, max(Comp_PinCode) as Comp_PinCode ,max(Comp_Phone1) as Comp_Phone1 ,max(Comp_Phone2) as Comp_Phone2 ,max(Comp_GSTINNo) as Comp_GSTINNo, max(Comp_StateCode) as Comp_StateCode ,max( Comp_StateName) as Comp_StateName , max(Cust_Code) as Cust_Code ,max(Customer_Name) as Customer_Name , max(Cust_Add1) as Cust_Add1 ,max(Cust_Add2) as Cust_Add2,max(Cust_Add3) as Cust_Add3 ,max(Cust_GSTNO) as Cust_GSTNO, max(Cust_StateCode) as  Cust_StateCode,  max(Cust_State_Name) as Cust_State_Name , max(Route_No) as Route_No,  max(Route_Desc) as Route_Desc, max(HSN_Code) as HSN_Code,Doc_Date  ,
 
                                         sum (isnull([Item1_Qty],0)) as [Item1_Qty],
                                         sum(isnull([Item1_Amt],0)) as [Item1_Amt],
                                         isnull(convert (decimal(18,2), (sum(isnull([Item1_Amt],0)) / nullif (sum (isnull([Item1_Qty],0)),0))),0) as [Item1_Rate],
 
                                         sum (isnull([Item2_Qty],0)) as [Item2_Qty],
                                         sum(isnull([Item2_Amt],0)) as [Item2_Amt],
                                         isnull(convert (decimal(18,2), (sum(isnull([Item2_Amt],0)) / nullif (sum (isnull([Item2_Qty],0)),0))),0) as [Item2_Rate],
 
                                         sum (isnull([Item3_Qty],0)) as [Item3_Qty],
                                         sum(isnull([Item3_Amt],0)) as [Item3_Amt],
                                         isnull(convert (decimal(18,2), (sum(isnull([Item3_Amt],0)) / nullif (sum (isnull([Item3_Qty],0)),0))),0) as [Item3_Rate],
 
                                          sum (isnull([Item4_Qty],0)) as [Item4_Qty],
                                         sum(isnull([Item4_Amt],0)) as [Item4_Amt],
                                         isnull( convert (decimal(18,2), (sum(isnull([Item4_Amt],0)) / nullif (sum (isnull([Item4_Qty],0)),0))),0) as [Item4_Rate],

                                          sum (isnull([Item5_Qty],0)) as [Item5_Qty],
                                         sum(isnull([Item5_Amt],0)) as [Item5_Amt],
                                         isnull( convert (decimal(18,2), (sum(isnull([Item5_Amt],0)) / nullif (sum (isnull([Item3_Qty],0)),0))),0) as [Item5_Rate]

                                         ,max(TTT.Item1) as Item1 , max(TTT.Item2) as Item2, max(TTT.Item3 ) as Item3, max(TTT.Item4) as Item4 , max(TTT.Item5) as Item5
 
 
                                         from (
                                         select 1 as SNO, Comp_Code,  Comp_Name ,  Comp_Add1, Comp_Add2, Comp_Add3, Comp_City_Code , Comp_PinCode ,Comp_Phone1 ,Comp_Phone2 ,Comp_GSTINNo, Comp_StateCode , Comp_StateName , Cust_Code ,Customer_Name , Cust_Add1,Cust_Add2,Cust_Add3,Cust_GSTNO, Cust_StateCode ,  Cust_State_Name , Route_No,  Route_Desc, HSN_Code,Doc_Date  ,
 
                                         [Item1_Qty],[Item2_Qty],[Item3_Qty],[Item4_Qty] ,[Item5_Qty], 
 
                                         [Item1_Amt],[Item2_Amt],[Item3_Amt],[Item4_Amt],[Item5_Amt]
 
 
                                         from (
                                         select max(XXXFinal.Comp_Code) as Comp_Code, max( XXXFinal.Comp_Name) as Comp_Name , max( Comp_Add1) as Comp_Add1,max(Comp_Add2) as Comp_Add2,max(Comp_Add3) as Comp_Add3,max(Comp_City_Code) as Comp_City_Code ,max(Comp_PinCode) as Comp_PinCode ,max(Comp_Phone1) as Comp_Phone1 ,max(Comp_Phone2) as Comp_Phone2 ,max(Comp_GSTINNo) as Comp_GSTINNo, max(Comp_StateCode) as Comp_StateCode , max( Comp_StateName) as Comp_StateName , Cust_Code ,max( Customer_Name) as Customer_Name , max(Cust_Add1) as Cust_Add1,max( Cust_Add2 ) as Cust_Add2,max(Cust_Add3) as Cust_Add3,max(Cust_GSTNO) as Cust_GSTNO, max(Cust_StateCode) as Cust_StateCode , max(Cust_State_Name) as Cust_State_Name ,max( Route_No) as Route_No, max(Route_Desc) as Route_Desc, Doc_Date, Item_Code , max(Item_Desc) as Item_Desc, max(Item_Desc2) as Item_Desc2 , max(HSN_Code) as HSN_Code,sum( QtyInLtr) as QtyInLtr , sum (Item_Net_Amt) as Item_Net_Amt , convert (decimal(18,2), sum (Item_Net_Amt) / nullif (sum( QtyInLtr),0) )  as Rate from (
                                         select  TSPL_COMPANY_MASTER.Comp_Code , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.City_Code as Comp_City_Code, TSPL_COMPANY_MASTER.Pincode as Comp_PinCode ,TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1, TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.GSTINNo as Comp_GSTINNo,TSPL_STATE_MASTER_comp.GST_STATE_Code as Comp_StateCode, TSPL_STATE_MASTER_comp.STATE_NAME as Comp_StateName, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name , TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1 , TSPL_CUSTOMER_MASTER.Add2 as Cust_Add2, TSPL_CUSTOMER_MASTER.Add3 as Cust_Add3, TSPL_CUSTOMER_MASTER.GSTNO as Cust_GSTNO, TSPL_STATE_MASTER_Cust.GST_STATE_Code as Cust_StateCode, TSPL_STATE_MASTER_Cust.STATE_NAME as Cust_State_Name,TSPL_CUSTOMER_MASTER.Route_No, TSPL_ROUTE_MASTER.Route_Desc , convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,6) as Doc_Date,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,XXFinal.Item + '_Qty' as Item_Desc, XXFinal.Item + '_Amt' as Item_Desc2 ,TSPL_item_master.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code , convert (decimal(18,2), TSPL_SD_SALE_INVOICE_DETAIL.Qty * isnull( StockUOM.Conversion_Factor,0) / nullif( isnull (TargetUOM.Conversion_Factor,0),0) ) as QtyInLtr , TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt , TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt / (TSPL_SD_SALE_INVOICE_DETAIL.Qty * isnull( StockUOM.Conversion_Factor,0) / nullif (isnull (TargetUOM.Conversion_Factor,0),0) ) as Rate
                                         from TSPL_SD_SALE_INVOICE_DETAIL 
                                         left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                                         left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_comp on TSPL_STATE_MASTER_comp.STATE_CODE = TSPL_COMPANY_MASTER.State
                                         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Cust on TSPL_STATE_MASTER_Cust.STATE_CODE = TSPL_CUSTOMER_MASTER.State
                                         left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No
                                         left outer join TSPL_ITEM_UOM_DETAIL as StockUOM on StockUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and  StockUOM.UOM_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                         left outer join TSPL_ITEM_UOM_DETAIL as TargetUOM on TargetUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TargetUOM.UOM_Code = 'Ltr'
                                         inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
 
                                         left outer join (  
                                         select Case when ROW_NUMBER = 1 then 'Item1'   when ROW_NUMBER = 2 then 'Item2'  when ROW_NUMBER = 3 then 'Item3' when ROW_NUMBER = 4 then 'Item4' when ROW_NUMBER = 5 then 'Item5' end as Item, Item_Desc , Item_Code     from (
                                         select ROW_NUMBER() OVER(ORDER BY Item_Desc) AS Row_Number , Item_Desc, Item_Code  from (
                                         SELECT Distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_ITEM_MASTER .Alies_Name as Item_Desc   FROM   TSPL_SD_SALE_INVOICE_DETAIL 
                                         left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                                         left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_comp on TSPL_STATE_MASTER_comp.STATE_CODE = TSPL_COMPANY_MASTER.State
                                         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Cust on TSPL_STATE_MASTER_Cust.STATE_CODE = TSPL_CUSTOMER_MASTER.State
                                         left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No
                                         left outer join TSPL_ITEM_UOM_DETAIL as StockUOM on StockUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and  StockUOM.UOM_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                         left outer join TSPL_ITEM_UOM_DETAIL as TargetUOM on TargetUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TargetUOM.UOM_Code = 'Ltr'
                                         inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
 
                                         where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS'  
                                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_CUSTOMER_MASTER.Cust_Code= '" + fndCustom.Value + "' and TSPL_ITEM_MASTER.Is_Milk_Pouch =1  and TSPL_SD_SALE_INVOICE_HEAD.Status =1
                                         ) Final ) XFinal ) XXFinal  on XXFinal.Item_Desc = TSPL_ITEM_MASTER.Alies_Name and XXFinal.Item_Code = TSPL_ITEM_MASTER.Item_Code


                                         where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS'  
                                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_CUSTOMER_MASTER.Cust_Code= '" + fndCustom.Value + "' and TSPL_ITEM_MASTER.Is_Milk_Pouch =1  and TSPL_SD_SALE_INVOICE_HEAD.Status =1
                                         ) XXXFinal group by XXXFinal.Doc_Date, XXXFinal.Cust_Code, XXXFinal.Item_Code
                                         ) Final
                                         pivot ( sum(QtyInLtr) for Item_Desc in ([Item1_Qty],[Item2_Qty],[Item3_Qty],[Item4_Qty],[Item5_Qty]) ) QtyPivot 

                                         pivot ( sum(Item_Net_Amt) for Item_Desc2 in ([Item1_Amt],[Item2_Amt],[Item3_Amt],[Item4_Amt],[Item5_Amt]) ) QtyPivot2

                                          ) XFinal 

 
                                          left outer join  ( select 1 as SNO, max(isnull(Item1,'')) as Item1, max(isnull(Item2,'')) as Item2 , max(isnull(Item3,'')) as Item3, max(isnull(Item4,'')) as Item4, max(isnull(Item5,'')) as Item5 from (
                                         select Case when ROW_NUMBER = 1 then Item_Desc end as Item1,Case when ROW_NUMBER = 2 then Item_Desc end as Item2,Case when ROW_NUMBER = 3 then Item_Desc end as Item3, Case when ROW_NUMBER = 4 then Item_Desc end as Item4, Case when ROW_NUMBER = 5 then Item_Desc end as Item5     from (
                                         select ROW_NUMBER() OVER(ORDER BY Item_Desc) AS Row_Number , Item_Desc from (
                                         SELECT Distinct TSPL_ITEM_MASTER.Alies_Name as  Item_Desc  FROM   TSPL_SD_SALE_INVOICE_DETAIL 
                                         left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                                         left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_comp on TSPL_STATE_MASTER_comp.STATE_CODE = TSPL_COMPANY_MASTER.State
                                         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                         left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Cust on TSPL_STATE_MASTER_Cust.STATE_CODE = TSPL_CUSTOMER_MASTER.State
                                         left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No
                                         left outer join TSPL_ITEM_UOM_DETAIL as StockUOM on StockUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and  StockUOM.UOM_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                         left outer join TSPL_ITEM_UOM_DETAIL as TargetUOM on TargetUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TargetUOM.UOM_Code = 'Ltr'
                                         inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
 
                                         where  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') AND Screen_Type='DS'  
                                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_CUSTOMER_MASTER.Cust_Code= '" + fndCustom.Value + "' and TSPL_ITEM_MASTER.Is_Milk_Pouch =1  and TSPL_SD_SALE_INVOICE_HEAD.Status =1
                                         ) Final ) xfinal ) as XXFinal) TTT on TTT.SNO = XFinal.SNO
                                          group by Doc_Date, Cust_Code "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(False, CrystalReportFolder.SalesReport, dt, "rptMonthlyInvoicePrint", "Customer Monthly Sales")
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
            End Try
        Else
            Printing()
        End If

    End Sub

    Private Sub Printing(Optional ByVal DocNo As String = "", Optional ByVal isPdf As Boolean = False, Optional ByVal isPrePrintFormat As Boolean = False)
        'loadData(1)
        ArrInvoice_Arr = New ArrayList
        Dim InvoiceNo As String = ""
        Dim frmCRV As New frmCrystalReportViewer()
        If isPdf = True Then
            InvoiceNo = DocNo
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Document_Code").Value)
                End If
            Next

            If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
                InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)

            End If
        End If
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowToPrintInvoiceAfterPosting & "'")) = 1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct status from tspl_sd_sale_invoice_head where document_code in ('" & InvoiceNo & "')")) = 0 Then
                frmCRV.ShowCystalReportToolbar = False
            End If
        End If
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "WHOLLY") = CompairStringResult.Equal Then

            Dim Qry As String = " WITH A AS( " & _
            " SELECT IT.ITEM_DESC AS Item_Desc,CRATE_QTY=SUM(Qty),PCS_QTY=(SUM(Qty) *(ISNULL(U.Conversion_Factor,0))),Scheme_Qty=0,SCHEME_PCS_QTY=0,Item_Cost= ROUND((case when ISNULL(U.Conversion_Factor,0)>0 THEN ( CASE WHEN d.Unit_code!='PP' THEN  Item_Cost/ISNULL(U.Conversion_Factor,0) ELSE Item_Cost END ) else Item_Cost end) ,2) ,Amount=SUM(Amount),d.Unit_code,Amt_Less_Discount=SUM(Amt_Less_Discount) FROM TSPL_SD_SALE_INVOICE_DETAIL D  " & _
            " LEFT JOIN TSPL_ITEM_MASTER IT ON D.Item_Code=IT.Item_Code " & _
            " LEFT JOIN TSPL_ITEM_UOM_DETAIL U ON U.Item_Code=D.Item_Code AND D.Unit_code=U.UOM_Code  " & _
            " WHERE  D.Document_Code='" + InvoiceNo + "'  and Scheme_Item='N' " & _
            " GROUP BY IT.ITEM_DESC,Item_Cost,U.Conversion_Factor,d.Unit_code " & _
            " UNION ALL " & _
            " SELECT IT.ITEM_DESC AS Item_Desc,CRATE_QTY=0,PCS_QTY=0,Scheme_Qty=SUM(Qty),SCHEME_PCS_QTY=(case when d.Unit_code='CRATES' then (SUM(Qty) *(ISNULL(U.Conversion_Factor,0))) else 0 end),Item_Cost= ROUND((case when ISNULL(U.Conversion_Factor,0)>0 THEN ( CASE WHEN d.Unit_code!='PP' THEN  Item_Cost/ISNULL(U.Conversion_Factor,0) ELSE Item_Cost END ) else Item_Cost end) ,2) ,Amount=SUM(Amount),d.Unit_code,Amt_Less_Discount=SUM(Amt_Less_Discount) FROM TSPL_SD_SALE_INVOICE_DETAIL D  " & _
            " LEFT JOIN TSPL_ITEM_MASTER IT ON D.Item_Code=IT.Item_Code " & _
            " LEFT JOIN TSPL_ITEM_UOM_DETAIL U ON U.Item_Code=D.Item_Code AND D.Unit_code=U.UOM_Code " & _
            " WHERE D. Document_Code='" + InvoiceNo + "'  and Scheme_Item='Y' " & _
            " GROUP BY IT.ITEM_DESC,Item_Cost,U.Conversion_Factor,d.Unit_code ,d.Unit_code ) " & _
            " SELECT Comp_Name,COMP_ADDRESS=(CM.Add1+' '+CM.Add2+' '+CM.Add3), STATE=(CM.Phone1 ),CM.Tin_No, " & _
            " LOCATION_ADDRESS=(L.Add1+' '+L.Add2+' '+L.Add3), " & _
            " C.Customer_Name,CUST_ADD=(C.Add1+' '+C.Add2+' '+C.Add3),CT.City_Name,C.PAN, " & _
            " I.Document_Code, " & _
            " Document_Date=CONVERT(VARCHAR(100),Document_Date,103),Cust_PO_Date=CONVERT(VARCHAR(100),Cust_PO_Date,103),I.Against_Shipment_No ,Challan_Date=CONVERT(VARCHAR(100),I.Challan_Date,103),I.VehicleNo,i.discount_amt, " & _
            " A.*,TOTAL_SCHEME_AMOUNT=(SELECT ISNULL(SUM(AMOUNT),0) FROM TSPL_SD_SALE_INVOICE_DETAIL I WHERE  I.Document_Code='" + InvoiceNo + "' AND  Scheme_Item='Y'),CM.Logo_Img,I.Created_By " & _
            " FROM TSPL_SD_SALE_INVOICE_HEAD I " & _
            " JOIN TSPL_CUSTOMER_MASTER C ON C.Cust_Code=I.Customer_Code " & _
            " LEFT JOIN TSPL_CITY_MASTER CT ON C.City_Code=CT.City_Code " & _
            " JOIN TSPL_LOCATION_MASTER L ON L.Location_Code=I.Bill_To_Location " & _
            " JOIN TSPL_COMPANY_MASTER CM ON CM.Comp_Code=I.Comp_Code " & _
            " JOIN A A ON 1=1 " & _
            " WHERE I.Document_Code='" + InvoiceNo + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            strRptPath = frmCRV.funreport(isPdf, CrystalReportFolder.SalesReport, dt, "rptProductionSaleInvoiceWHC", "Sale Report")
        Else
            Dim IsTaxable As Double = 0
            Dim dtDocdate As Date?
            dtDocdate = Nothing
            Dim StrSql = "Select Document_Date,Customer_Code,Bill_To_Location,is_taxable,Tax_Group from TSPL_SD_SALE_INVOICE_HEAD where Document_Code in ('" & InvoiceNo & "')"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
            If dt1.Rows.Count > 0 Then
                IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
                dtDocdate = clsCommon.myCDate(dt1.Rows(0)("Document_Date"))
            End If
            Dim objInvoice As New frmSaleInvoiceProductSale
            If IsTaxable = 1 Then
                strRptPath = ""
                objInvoice.funPrint(InvoiceNo, False, Nothing, Nothing, False, isPdf)
                If isPdf = True Then
                    strRptPath = objInvoice.strrptpath
                End If
            Else
                Dim Qry As String = Nothing
                Dim objMultPrintInvoice As New FrmPrintFreshInvoice

                If AllowSeperateSchemeItemOnPrint Then
                    objInvoice.GetSeperateSchemeItemPrintQry(InvoiceNo)
                Else

                    '====================Added by richa agarwal 23 Nov,2018=======================
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                        Dim CreateFreshInvoiceOnDispatchSave As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, Nothing))
                        If CreateFreshInvoiceOnDispatchSave = 0 Then
                            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select document_code from TSPL_SD_SHIPMENT_HEAD where Sale_Invoice_No in ('" & InvoiceNo & "')")
                            Dim dispatchno As String = String.Empty
                            For Each dr As DataRow In dt3.Rows
                                dispatchno = dispatchno + "','" + clsCommon.myCstr(dr("document_code"))
                            Next

                            If clsCommon.myLen(dispatchno) > 0 AndAlso clsCommon.myCstr(dispatchno).Substring(0, 3) = "','" Then
                                dispatchno = dispatchno.Substring(3, dispatchno.Length - 3)

                            End If
                            Qry = objMultPrintInvoice.LoadPrintQuery(dispatchno)
                        Else
                            ''richa 2 Apr,2019 ERO/04/04/19-000543
                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then

                                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable("select document_code from TSPL_SD_SHIPMENT_HEAD where Sale_Invoice_No in ('" & InvoiceNo & "')")
                                Dim dispatchno As String = String.Empty
                                For Each dr As DataRow In dt3.Rows
                                    dispatchno = dispatchno + "','" + clsCommon.myCstr(dr("document_code"))
                                Next

                                If clsCommon.myLen(dispatchno) > 0 AndAlso clsCommon.myCstr(dispatchno).Substring(0, 3) = "','" Then
                                    dispatchno = dispatchno.Substring(3, dispatchno.Length - 3)

                                End If
                                Qry = objMultPrintInvoice.LoadPrintQuery(dispatchno)

                            Else
                                Qry = objMultPrintInvoice.LoadPrintQuery(InvoiceNo)
                            End If

                        End If
                    Else
                        Qry = objMultPrintInvoice.LoadPrintQuery(InvoiceNo)
                    End If
                    '================================================
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                        If isPdf = True Then
                            Qry = " Select * from ( " + Qry + " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  '' as CopyType1 ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2 ,xxx.Line_No "
                        ElseIf isPrePrintFormat = True Then
                            ' Qry = " Select *,count(*) over ( partition by Sale_invoice_no ) as TotalNoOfItem from ( " + Replace(Qry, " upper(SUBSTRING(Batch_No,1,7))", " case when len ( isnull (Batch_No,'') ) > 0 then SUBSTRING (Batch_No,1,7) else '' end as Batch_No ") + " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2 ,xxx.Line_No "
                            Qry = " Select *,count(*) over ( partition by Sale_invoice_no ) as TotalNoOfItem from ( " + Qry + " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2 ,xxx.Line_No "
                        Else
                            Qry = " Select * from ( " + Qry + " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE' as CopyType1 ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2 ,xxx.Line_No "
                        End If
                    End If



                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                    If dt.Rows.Count > 0 Then
                        If ShowShipToPartyInDairyDispatch = 1 OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                            ' done by richa agarwal 23 Nov,2018 add customer dashbord into existing report for Erode client ERO/01/04/19-000534
                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                                Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & InvoiceNo & "')"
                                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                Dim CustomerCode As String = ""
                                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                                    For Each dr As DataRow In dt2.Rows
                                        CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                    Next
                                    If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                        CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                    End If
                                End If
                                Dim dtCustomerOutstanding As DataTable = Nothing
                                dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Document_date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Document_date")), "dd/MMM/yyyy"))
                                ' Pre-Format Print for EROD
                                If isPrePrintFormat = True Then
                                    strRptPath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoicePartyForPreFormat", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                Else
                                    strRptPath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                End If


                            Else
                                strRptPath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            End If

                        Else
                            strRptPath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                        End If

                    End If
                End If
            End If
        End If
        frmCRV = Nothing
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmPrintDistributerInvoiceStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Dim coll As Dictionary(Of String, String)
            'coll = New Dictionary(Of String, String)
            'coll.Add("MonthlySaleInvoiceNo", "varchar(30) NULL")
            'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_SD_SALE_INVOICE_HEAD", coll, Nothing, False, True, "", "Document_Code", "Document_Date")
            isInsideLoadData = True
            SetUserMgmtNew()
            ApplyMilkPouchPrint = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyMilkPouchPrint, clsFixedParameterCode.ApplyMilkPouchPrint, Nothing)) = 0, False, True)
            AllowSeperateSchemeItemOnPrint = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSeprateSchemeItemPrintDairySaleInvoice, clsFixedParameterCode.AllowSeprateSchemeItemPrintDairySaleInvoice, Nothing)) = 0, False, True)
            ShowShipToPartyInDairyDispatch = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ShowShipToPartyInDairyDispatch & "'")) = 0, 0, 1)
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            LoadReportType()
            Reset()
            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
        End Try
        
    End Sub
    Sub loadData(ByVal printType As Integer)


        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""
        Dim DispatchNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Document_Code").Value)

            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)

        End If




        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If


        Dim objInvoice As New clsPSShipmentHead
        Dim Qry As String = objInvoice.GetQuery()
        Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + InvoiceNo + "')"
        ''===============================================================================
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "WHOLLY") = CompairStringResult.Equal Then

            Qry = "select max(Remarks) as Remarks,sum(isnull(Roundoff,0)) as Roundoff,max(Transporter_Name) as Transporter_Name,sum(crate) as crate,sum(jaali) as jaali,sum(box) as box,max(GP_Invoice_Type) as GP_Invoice_Type,max(Loc_Tin_No) as Loc_Tin_No,max(Location_Address_GP) as Location_Address_GP,max(comp_code) as comp_code,max(Comp_Add_GP) as Comp_Add_GP,max(GP_Division)as GP_Division,max(GP_ECC_No) as GP_ECC_No,max(GP_CE_Range) as GP_CE_Range,max(ITF_CODE) as ITF_CODE,max(Loc_State_Name) as Loc_State_Name,max(City_Name) as City_Name,max(Loc_state_code) as Loc_state_code,max(HOAdd1) as HOAdd1,max(HOAdd2) as HOAdd2,max(Comp_CSt_LST) as Comp_CSt_LST,max(Transport_Id) as Transport_Id,max(GRNo) as GRNo,max(GR_Date) as GR_Date,max(VehicleNo) as VehicleNo,sum(isnull(Alter_UnitQty,0)) as Alter_UnitQty,sum(isnull(HeadDisc_Amt,0)) as HeadDisc_Amt,max(Chap_Desc) as Chap_Desc,sum(isnull(HeadDisc_PerAmt,0)) as HeadDisc_PerAmt " & _
          " ,max(Registration_Number) as Registration_Number,max(Payment_Terms) as Payment_Terms,max(Modify_By) as Modify_By,max(Comp_PANNO) as Comp_PANNO,max(Road_Permit_No) as Road_Permit_No,max(Cheapter_Heads) as Cheapter_Heads,max(RoadPermit_Date)as RoadPermit_Date,max(Location_Address) as Location_Address,max(Loc_CSTNo) as Loc_CSTNo,max(loc_Excisable) as loc_Excisable,max(Loc_range_Add) as Loc_range_Add,max(loc_Division_Address) as loc_Division_Address,max(Loc_Commissionerate) as Loc_Commissionerate " & _
          " ,max(Challan_No) as Challan_No,max(Challan_Date) as Challan_Date	,max(Removal_Date) as Removal_Date,sum(isnull(total_add_charge,0))as total_add_charge,max(WayBillNo) as WayBillNo,max(Comp_Address) as Comp_Address,max(Loc_Add1) as Loc_Add1,max(Loc_ADd2) as Loc_ADd2,max(Loc_Add3) as Loc_Add3,max(Loc_Pin_Code) as Loc_Pin_Code,max(Loc_TinNo) as Loc_TinNo,max(Loc_Phn) as Loc_Phn,max(Loc_Email) as Loc_Email,max(Invoice_Type) as Invoice_Type,sum(isnull(Alternet_Qty,0)) as Alternet_Qty,max(Alternate_UOM) as Alternate_UOM,sum(isnull(Scheme_Qty,0)) as Scheme_Qty,max(Scheme_Item_UOM) as Scheme_Item_UOM,sum(isnull(Discount_Base,0)) as Discount_Base,max(Dis_Doc_No) as Dis_Doc_No,max([Description]) as [Description],sum(isnull(Print_Discount_Amt,0)) as Print_Discount_Amt,max(Comp_State) as Comp_State,max(Buyer_order_no) as Buyer_order_no,max(Buyer_order_date) as Buyer_order_date,max(Terms_of_delivery) as Terms_of_delivery,max(Loc_Short_Name)Loc_Short_Name,InvoiceNo,Distributer_Dispatch_No,max(Date_Time_Invoice) as Date_Time_Invoice,max(InvoiceDate)as InvoiceDate,ShipmentNo,sum(isnull(Alt_Qty,0)) as Alt_Qty,max(Alt_UOM) as Alt_UOM,max(ShipmentDate) as ShipmentDate,DeliveryOrderNo,max(TermCondition) as TermCondition,max(Location_Desc) as Location_Desc,max(CompName) as CompName,max(CompPhone) as CompPhone,max(CompFax) as CompFax,max(ComMail) as ComMail,max(CompCinNo) as CompCinNo,max(ComPanNo) as ComPanNo,max(CompCSTLST) as CompCSTLST,max(ComPINCode) as ComPINCode,max(ComTinNO) as ComTinNO,max(Compaddress2) as Compaddress2,max(Compaddress3) as Compaddress3,max(P_Add1) as P_Add1,max(P_Add2) as P_Add2,max(P_Add3) as P_Add3,max(P_PinNo) as P_PinNo,max(P_CstNo) as P_CstNo,max(P_TinNo) as P_TinNo,max(P_Email) as P_Email,max(P_Fax) as P_Fax,max(P_LstNo) as P_LstNo,P_CustCode,max(P_Cust_Name) as P_Cust_Name,max(P_City_Name) as P_City_Name,max(P_State_Name) as P_State_Name,max(P_Cust_Phn) as P_Cust_Phn,max(P_Pan) as P_Pan,Cust_Code,max(Customer_Name) as Customer_Name,max(Cust_Add1) as Cust_Add1,max(Cust_add2) as Cust_add2,max(cust_add3) as cust_add3,max(Cust_Phn) as Cust_Phn,max(Cust_TinNo) as Cust_TinNo,max(Cust_CSTNo) as Cust_CSTNo,max(Cust_LSTNo) as Cust_LSTNo,max(Cust_Email) as Cust_Email,max(Cust_PinCode) as Cust_PinCode,max(Cust_City_Name) as Cust_City_Name,max(Cust_Fax) as Cust_Fax,max(Cust_State_Name) as Cust_State_Name,max(Customer_Pan) as Customer_Pan,item_code,max(itemdesc)as itemdesc,sum(isnull(qty,0)) as qty,mrp,sum(isnull(amount,0)) as amount,sum(isnull(Amt_Less_Discount,0)) as Amt_Less_Discount,sum(Scheme_Amount) as Scheme_Amount,SUM(CRATES_QTY) AS CRATES_QTY,SUM(PCS_QTY) AS PCS_QTY,SUM(SCH_PCS_QTY) AS SCH_PCS_QTY,sum(Item_Cost_Main) as Item_Cost_Main,max(uom) as uom,max(RATE_UOM) as RATE_UOM,itemcost,max(tax1name) as tax1name,sum(isnull(txt1amt,0)) as txt1amt,max(tax2name) as tax2name,sum(isnull(txt2amt,0)) as txt2amt,max(tax3name) as tax3name,sum(isnull(txt3amt,0)) as txt3amt,max(tax4name) as tax4name,sum(isnull(txt4amt,0)) as txt4amt,max(tax5name) as tax5name,sum(isnull(txt5amt,0)) as txt5amt,max(tax6name) as tax6name,sum(isnull(txt6amt,0)) as txt6amt, max(tax7name) as tax7name,sum(isnull(txt7amt,0)) as txt7amt,max(tax8name) as tax8name,sum(isnull(txt8amt,0)) as txt8amt, max(tax9name) as tax9name,sum(isnull(txt9amt,0)) as txt9amt,max(tax10name) as tax10name,sum(isnull(txt10amt,0)) as txt10amt " & _
          " ,max(TAX1_Rate) as TAX1_Rate ,max(TAX2_Rate) as TAX2_Rate ,max(TAX3_Rate) as TAX3_Rate,max(TAX4_Rate) as TAX4_Rate,max(TAX5_Rate) as TAX5_Rate,max(TAX6_Rate) as TAX6_Rate,max(TAX7_Rate) as TAX7_Rate,max(TAX8_Rate) as TAX8_Rate,max(TAX9_Rate) as TAX9_Rate,max(TAX10_Rate) as TAX10_Rate,max(Disc_Per) as Disc_Per,sum(isnull( Discount_Amt,0)) as  Discount_Amt,sum(isnull(Amount_Less_Discount,0)) as Amount_Less_Discount,sum(isnull(Total_Tax_Amt,0)) as Total_Tax_Amt,sum(isnull(Total_Amt,0)) as Total_Amt,max(CopyType) as CopyType ,max(Add_Charge_Name1) as Add_Charge_Name1,sum(isnull(Add_Charge_Amt1,0)) as Add_Charge_Amt1,max(Add_Charge_Name2) as Add_Charge_Name2,sum(isnull(Add_Charge_Amt2,0)) as Add_Charge_Amt2,max(Add_Charge_Name3) as Add_Charge_Name3,sum(isnull(Add_Charge_Amt3,0)) as Add_Charge_Amt3,max(Add_Charge_Name4) as Add_Charge_Name4,sum(isnull(Add_Charge_Amt4,0)) as Add_Charge_Amt4,max(Add_Charge_Name5) as Add_Charge_Name5,sum(isnull(Add_Charge_Amt5,0)) as Add_Charge_Amt5,max(Add_Charge_Name6) as Add_Charge_Name6,sum(isnull(Add_Charge_Amt6,0)) as Add_Charge_Amt6,max(Add_Charge_Name7) as Add_Charge_Name7,sum(isnull(Add_Charge_Amt7,0)) as Add_Charge_Amt7,max(Add_Charge_Name8) as Add_Charge_Name8,sum(isnull(Add_Charge_Amt8,0)) as Add_Charge_Amt8,max(Add_Charge_Name9) as Add_Charge_Name9,sum(isnull(Add_Charge_Amt9,0)) as Add_Charge_Amt9,max(Add_Charge_Name10) as Add_Charge_Name10,sum(isnull(Add_Charge_Amt10,0)) as Add_Charge_Amt10  ,sum(isnull(RoundOffAmount,0)) as RoundOffAmount,Booking_No,sum(isnull(Distr_Commision,0)) as Distr_Commision,Line_No from  " & _
          " (" & Qry & ")final group by InvoiceNo,Distributer_Dispatch_No,ShipmentNo,DeliveryOrderNo,P_CustCode,Cust_Code,item_code,mrp,itemcost,Booking_No,Line_No "
            ''=========================================================================


            Qry = "Select cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,* from (" & Qry & ") XXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=xxx.comp_code "

            If printType = 2 Then
                Qry += " LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2,Item_Code,Line_No asc"
            Else
                Qry += " LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2,Item_Code,Line_No asc"
            End If
        Else

            Qry = "select max(Remarks) as Remarks,sum(isnull(Roundoff,0)) as Roundoff,max(Transporter_Name) as Transporter_Name,sum(crate) as crate,sum(jaali) as jaali,sum(box) as box,max(GP_Invoice_Type) as GP_Invoice_Type,max(Loc_Tin_No) as Loc_Tin_No,max(Location_Address_GP) as Location_Address_GP,max(comp_code) as comp_code,max(Comp_Add_GP) as Comp_Add_GP,max(GP_Division)as GP_Division,max(GP_ECC_No) as GP_ECC_No,max(GP_CE_Range) as GP_CE_Range,max(ITF_CODE) as ITF_CODE,max(Loc_State_Name) as Loc_State_Name,max(City_Name) as City_Name,max(Loc_state_code) as Loc_state_code,max(HOAdd1) as HOAdd1,max(HOAdd2) as HOAdd2,max(Comp_CSt_LST) as Comp_CSt_LST,max(Transport_Id) as Transport_Id,max(GRNo) as GRNo,max(GR_Date) as GR_Date,max(VehicleNo) as VehicleNo,sum(isnull(Alter_UnitQty,0)) as Alter_UnitQty,sum(isnull(HeadDisc_Amt,0)) as HeadDisc_Amt,max(Chap_Desc) as Chap_Desc,sum(isnull(HeadDisc_PerAmt,0)) as HeadDisc_PerAmt " & _
                " ,max(Registration_Number) as Registration_Number,max(Payment_Terms) as Payment_Terms,max(Modify_By) as Modify_By,max(Comp_PANNO) as Comp_PANNO,max(Road_Permit_No) as Road_Permit_No,max(Cheapter_Heads) as Cheapter_Heads,max(RoadPermit_Date)as RoadPermit_Date,max(Location_Address) as Location_Address,max(Loc_CSTNo) as Loc_CSTNo,max(loc_Excisable) as loc_Excisable,max(Loc_range_Add) as Loc_range_Add,max(loc_Division_Address) as loc_Division_Address,max(Loc_Commissionerate) as Loc_Commissionerate " & _
                " ,max(Challan_No) as Challan_No,max(Challan_Date) as Challan_Date	,max(Removal_Date) as Removal_Date,sum(isnull(total_add_charge,0))as total_add_charge,max(WayBillNo) as WayBillNo,max(Comp_Address) as Comp_Address,max(Loc_Add1) as Loc_Add1,max(Loc_ADd2) as Loc_ADd2,max(Loc_Add3) as Loc_Add3,max(Loc_Pin_Code) as Loc_Pin_Code,max(Loc_TinNo) as Loc_TinNo,max(Loc_Phn) as Loc_Phn,max(Loc_Email) as Loc_Email,max(Invoice_Type) as Invoice_Type,sum(isnull(Alternet_Qty,0)) as Alternet_Qty,max(Alternate_UOM) as Alternate_UOM,sum(isnull(Scheme_Qty,0)) as Scheme_Qty,max(Scheme_Item_UOM) as Scheme_Item_UOM,sum(isnull(Discount_Base,0)) as Discount_Base,max(Dis_Doc_No) as Dis_Doc_No,max([Description]) as [Description],sum(isnull(Print_Discount_Amt,0)) as Print_Discount_Amt,max(Comp_State) as Comp_State,max(Buyer_order_no) as Buyer_order_no,max(Buyer_order_date) as Buyer_order_date,max(Terms_of_delivery) as Terms_of_delivery,max(Loc_Short_Name)Loc_Short_Name,InvoiceNo,Distributer_Dispatch_No,max(Date_Time_Invoice) as Date_Time_Invoice,max(InvoiceDate)as InvoiceDate,ShipmentNo,sum(isnull(Alt_Qty,0)) as Alt_Qty,max(Alt_UOM) as Alt_UOM,max(ShipmentDate) as ShipmentDate,DeliveryOrderNo,max(TermCondition) as TermCondition,max(Location_Desc) as Location_Desc,max(CompName) as CompName,max(CompPhone) as CompPhone,max(CompFax) as CompFax,max(ComMail) as ComMail,max(CompCinNo) as CompCinNo,max(ComPanNo) as ComPanNo,max(CompCSTLST) as CompCSTLST,max(ComPINCode) as ComPINCode,max(ComTinNO) as ComTinNO,max(Compaddress2) as Compaddress2,max(Compaddress3) as Compaddress3,max(P_Add1) as P_Add1,max(P_Add2) as P_Add2,max(P_Add3) as P_Add3,max(P_PinNo) as P_PinNo,max(P_CstNo) as P_CstNo,max(P_TinNo) as P_TinNo,max(P_Email) as P_Email,max(P_Fax) as P_Fax,max(P_LstNo) as P_LstNo,P_CustCode,max(P_Cust_Name) as P_Cust_Name,max(P_City_Name) as P_City_Name,max(P_State_Name) as P_State_Name,max(P_Cust_Phn) as P_Cust_Phn,max(P_Pan) as P_Pan,Cust_Code,max(Customer_Name) as Customer_Name,max(Cust_Add1) as Cust_Add1,max(Cust_add2) as Cust_add2,max(cust_add3) as cust_add3,max(Cust_Phn) as Cust_Phn,max(Cust_TinNo) as Cust_TinNo,max(Cust_CSTNo) as Cust_CSTNo,max(Cust_LSTNo) as Cust_LSTNo,max(Cust_Email) as Cust_Email,max(Cust_PinCode) as Cust_PinCode,max(Cust_City_Name) as Cust_City_Name,max(Cust_Fax) as Cust_Fax,max(Cust_State_Name) as Cust_State_Name,max(Customer_Pan) as Customer_Pan,item_code,max(itemdesc)as itemdesc,sum(isnull(qty,0)) as qty,mrp,sum(isnull(amount,0)) as amount,sum(isnull(Amt_Less_Discount,0)) as Amt_Less_Discount,max(uom) as uom,max(RATE_UOM) as RATE_UOM,itemcost,max(tax1name) as tax1name,sum(isnull(txt1amt,0)) as txt1amt,max(tax2name) as tax2name,sum(isnull(txt2amt,0)) as txt2amt,max(tax3name) as tax3name,sum(isnull(txt3amt,0)) as txt3amt,max(tax4name) as tax4name,sum(isnull(txt4amt,0)) as txt4amt,max(tax5name) as tax5name,sum(isnull(txt5amt,0)) as txt5amt,max(tax6name) as tax6name,sum(isnull(txt6amt,0)) as txt6amt, max(tax7name) as tax7name,sum(isnull(txt7amt,0)) as txt7amt,max(tax8name) as tax8name,sum(isnull(txt8amt,0)) as txt8amt, max(tax9name) as tax9name,sum(isnull(txt9amt,0)) as txt9amt,max(tax10name) as tax10name,sum(isnull(txt10amt,0)) as txt10amt " & _
                " ,max(TAX1_Rate) as TAX1_Rate ,max(TAX2_Rate) as TAX2_Rate ,max(TAX3_Rate) as TAX3_Rate,max(TAX4_Rate) as TAX4_Rate,max(TAX5_Rate) as TAX5_Rate,max(TAX6_Rate) as TAX6_Rate,max(TAX7_Rate) as TAX7_Rate,max(TAX8_Rate) as TAX8_Rate,max(TAX9_Rate) as TAX9_Rate,max(TAX10_Rate) as TAX10_Rate,max(Disc_Per) as Disc_Per,sum(isnull( Discount_Amt,0)) as  Discount_Amt,sum(isnull(Amount_Less_Discount,0)) as Amount_Less_Discount,sum(isnull(Total_Tax_Amt,0)) as Total_Tax_Amt,sum(isnull(Total_Amt,0)) as Total_Amt,max(CopyType) as CopyType ,max(Add_Charge_Name1) as Add_Charge_Name1,sum(isnull(Add_Charge_Amt1,0)) as Add_Charge_Amt1,max(Add_Charge_Name2) as Add_Charge_Name2,sum(isnull(Add_Charge_Amt2,0)) as Add_Charge_Amt2,max(Add_Charge_Name3) as Add_Charge_Name3,sum(isnull(Add_Charge_Amt3,0)) as Add_Charge_Amt3,max(Add_Charge_Name4) as Add_Charge_Name4,sum(isnull(Add_Charge_Amt4,0)) as Add_Charge_Amt4,max(Add_Charge_Name5) as Add_Charge_Name5,sum(isnull(Add_Charge_Amt5,0)) as Add_Charge_Amt5,max(Add_Charge_Name6) as Add_Charge_Name6,sum(isnull(Add_Charge_Amt6,0)) as Add_Charge_Amt6,max(Add_Charge_Name7) as Add_Charge_Name7,sum(isnull(Add_Charge_Amt7,0)) as Add_Charge_Amt7,max(Add_Charge_Name8) as Add_Charge_Name8,sum(isnull(Add_Charge_Amt8,0)) as Add_Charge_Amt8,max(Add_Charge_Name9) as Add_Charge_Name9,sum(isnull(Add_Charge_Amt9,0)) as Add_Charge_Amt9,max(Add_Charge_Name10) as Add_Charge_Name10,sum(isnull(Add_Charge_Amt10,0)) as Add_Charge_Amt10  ,sum(isnull(RoundOffAmount,0)) as RoundOffAmount,Booking_No,sum(isnull(Distr_Commision,0)) as Distr_Commision from  " & _
                " (" & Qry & ")final group by InvoiceNo,Distributer_Dispatch_No,ShipmentNo,DeliveryOrderNo,P_CustCode,Cust_Code,item_code,mrp,itemcost,Booking_No "
            ''=========================================================================

            'scheme_Rate

            Qry = "Select cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,isnull((select sum(qty*Amt_Less_Discount) from TSPL_SD_SALE_INVOICE_DETAIL where  document_code in ('" + InvoiceNo + "')  and Scheme_Item='Y'),0) as Scheme_Amount,* from (" & Qry & ") XXX left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=xxx.comp_code "

            If printType = 2 Then
                Qry += " LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2,Item_Code desc"
            Else
                Qry += " LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2,Item_Code "
            End If
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        Dim count As Integer = 0
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim UOMKG As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("UOM")), clsCommon.myCstr(dt.Rows(0)("UOM"))) = CompairStringResult.Equal Then
                    count = count + 1

                End If
            Next
        End If
        dt.Columns.Add("UOMKG")
        If dt.Rows.Count = count Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("UOMKG") = "T"
            Next
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("UOMKG") = "F"
            Next
        End If



        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If printType = 1 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDistributerProductSaleInvoice", "Distributer Invoice Statement", "")
            ElseIf printType = 2 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDistributerProductSaleInvoice_Challan", "Challan", "rptCompanyAddress.rpt")
            End If
            frmCRV = Nothing

        End If
        'Next

    End Sub
    Private Sub FrmPrintDistributerInvoiceStatement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            loadData(1)
        End If
    End Sub

    Private Sub btnUnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Invoice Type : " & cboReportType.Text)
            arrHeader.Add("Customer : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultCustomer.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultCustomer.arrDispalyMember)))
            arrHeader.Add("Location : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultLocation.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultLocation.arrDispalyMember)))
            arrHeader.Add("Route : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultRoute.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultRoute.arrDispalyMember)))


            'If chkLocationSelect.IsChecked Then
            '    Dim stVSPName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(stVSPName) > 0 Then
            '            stVSPName += ", "
            '        End If
            '        stVSPName += StrName
            '    Next
            '    Dim strVSPCode As String = ""
            '    For Each StrCode As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strVSPCode) > 0 Then
            '            strVSPCode += ", "
            '        End If
            '        strVSPCode += StrCode
            '    Next
            '    arrHeader.Add(("Location: " + stVSPName + " "))
            'End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Product Sale Invoice", gv, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Product Sale Invoice", gv, arrHeader, "Product Sale Invoice", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        'If gv.Rows.Count > 0 Then
        '    ExportToExcel(EnumExportTo.Excel)
        'Else
        '    RadMessageBox.Show("No Data Found to Display", Me.Text)
        'End If
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPrintDistributerInvoiceStatement & "'"))
            arrHeader.Add("Invoice Type : " & cboReportType.Text)
            arrHeader.Add("Customer : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultCustomer.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultCustomer.arrDispalyMember)))
            arrHeader.Add("Location : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultLocation.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultLocation.arrDispalyMember)))
            arrHeader.Add("Route : " & IIf(clsCommon.myLen(clsCommon.GetMulcallString(txtMultRoute.arrDispalyMember)) = 2, "All", clsCommon.GetMulcallString(txtMultRoute.arrDispalyMember)))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
        Dim obj As New clsPSShipmentHead
        obj.CreateEmailContent("ORDBOOK00008", Nothing)
        'For Each grw As GridViewRowInfo In gv.Rows
        '    ''Distributer_Dispatch_No
        '    obj.CreateEmailContent("ORDBOOK00008", Nothing)
        'Next

    End Sub

    Private Sub BtnPrintChallan_Click(sender As Object, e As EventArgs) Handles BtnPrintChallan.Click
        loadData(2)
    End Sub

    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
    End Sub

    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        strQry = "select TSPL_ROUTE_MASTER.Route_No as Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER "
        txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultRoute", strQry, "Code", "Name", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
    End Sub

    'Private Sub RadPageViewPage3_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage3.Paint

    'End Sub

    'Private Sub RadGroupBox3_Click(sender As Object, e As EventArgs) Handles RadGroupBox3.Click

    'End Sub

    'Private Sub MyLabel1_Click(sender As Object, e As EventArgs) Handles MyLabel1.Click

    'End Sub

    'Private Sub lblRoute_Click(sender As Object, e As EventArgs) Handles lblRoute.Click

    'End Sub

    'Private Sub RadPageViewPage1_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage1.Paint

    'End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiSaveLayout_Click(sender As Object, e As EventArgs) Handles rmiSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmiDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmiDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    'Ticket No-ERO/03/09/19-001018,Send Email with Invoice PDF attachment
    Private Sub BtnEmailSms_Click(sender As Object, e As EventArgs) Handles BtnEmailSms.Click
        Try
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.FrmPrintDistributerInvoiceStatement + "'", Nothing)
            If dtContent Is Nothing OrElse dtContent.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "First do email setting", Me.Text)
                Exit Sub
            End If

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    Dim objEmailH As New clsEMailHead()
                    objEmailH.arrEMail = New List(Of String)()

                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(grow.Cells("Document_Code").Value))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(grow.Cells("Document_Date").Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(grow.Cells("Document_Code").Value))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(grow.Cells("Document_Date").Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, clsCommon.myCstr(grow.Cells("Customer_Code").Value))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, clsCommon.myCstr(grow.Cells("Customer_Name").Value))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(grow.Cells("Total_Amt").Value))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.LocationName, clsCommon.myCstr(grow.Cells("Location_Desc").Value))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, Me.Form_ID)
                    '------------------------code for attchament-------------------------------------
                    strRptPath = ""
                    Printing(clsCommon.myCstr(grow.Cells("Document_Code").Value), True)
                    objEmailH.Attachment_1_Path = strRptPath
                    '---------------------------------------------------------------------------


                    Dim emailId As String = ""
                    emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & clsCommon.myCstr(grow.Cells("Customer_Code").Value) & "' ")

                    If clsCommon.myLen(emailId) > 0 Then
                        objEmailH.arrEMail.Add(emailId)
                    End If

                    objEmailH.SaveData(clsUserMgtCode.FrmPrintDistributerInvoiceStatement, objEmailH, Nothing)
                    objEmailH = Nothing
                End If
            Next
            clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub cboReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboReportType.SelectedIndexChanged
        Try
            If isInsideLoadData = False Then
                If clsCommon.CompairString(cboReportType.SelectedValue, "NT") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                    btnPrePrintFormat.Visible = True
                Else
                    btnPrePrintFormat.Visible = False
                End If
                If (clsCommon.CompairString(cboReportType.SelectedValue, "NT") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboReportType.SelectedValue, "LT") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboReportType.SelectedValue, "IT") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                    btnBatchWiseInvoice.Visible = True
                Else
                    btnBatchWiseInvoice.Visible = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPrePrintFormat_Click(sender As Object, e As EventArgs) Handles btnPrePrintFormat.Click
        Printing("", False, True)
    End Sub

    Private Sub btnBatchWiseInvoice_Click(sender As Object, e As EventArgs) Handles btnBatchWiseInvoice.Click
        Try
            Dim InvoiceNo As String = ""
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("Document_Code").Value)
                End If
            Next

            If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
                InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)

            End If
            clsPSShipmentPrint.PrintDataBatchWiseInvoice(InvoiceNo)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCombinedInvoice_Click(sender As Object, e As EventArgs) Handles btnCombinedInvoice.Click
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            Dim WhrCls As String = ""
            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If
            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If
            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If
            Dim Qry As String = " Select TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE + ' Customer :'+TSPL_SD_SALE_INVOICE_HEAD.Customer_Code + '('+TSPL_CUSTOMER_MASTER.Customer_Name +')' + ' Route :'+ TSPL_SD_SALE_INVOICE_HEAD.Route_No + '('+TSPL_ROUTE_MASTER.Route_Desc+')' as Group_column1, TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE , Convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1, TSPL_CUSTOMER_MASTER.Add2 as Customer_Add2, TSPL_CUSTOMER_MASTER.Add3 as Customer_Add3,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Amount, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2 ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, TSPL_COMPANY_MASTER.City_Code as Comp_City_Code,TSPL_COMPANY_MASTER.Fax as Comp_Fax,TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Pincode as Comp_Pincode ,TSPL_COMPANY_MASTER.State,TSPL_COMPANY_MASTER.Pan_No as Comp_Pan_No, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTIN_No,TSPL_COMPANY_MASTER.Access_Officer as Comp_FASSAI_NO , TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1 , TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2 " & _
                                " from TSPL_SD_SALE_INVOICE_DETAIL " & _
                                " Left Outer Join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                                " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                                " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                                " Left Outer Join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No " & _
                                " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.Comp_Code " & _
                                " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_DETAIL.Location " & _
                                " where TSPL_SD_SALE_INVOICE_HEAD.Status = 1 and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type = 'DS'  and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)    " + WhrCls + " " & _
                                " Order By TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE , TSPL_SD_SALE_INVOICE_DETAIL.Line_No  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "Customer Copy"
                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Phone No", clsCommon.myCstr(dt.Rows(0)("Comp_Phone1"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Email", clsCommon.myCstr(dt.Rows(0)("Comp_Email"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("GSTIN NO", clsCommon.myCstr(dt.Rows(0)("Comp_GSTIN_No"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("FASSAI NO", clsCommon.myCstr(dt.Rows(0)("Comp_FASSAI_NO"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("FASSAI NO", clsCommon.myCstr(dt.Rows(0)("Comp_FASSAI_NO"))))

                obj.arrGroup = New List(Of clsDosPrintGroup)()
                obj.arrGroup.Add(clsDosPrintGroup.GetObject("DOCUMENT_CODE", "Invoice", ""))
                'obj.arrGroup.Add(clsDosPrintGroup.GetObject("Group_column1", "Invoice", ""))


                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Line_No", "SNo", True, DosPrintAlignment.Left, 5, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Item_Code", "Product", True, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ITEM_DESC", "Description", True, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty", "Quantity", True, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Item_Cost", "UnitPrice(Rs)", False, DosPrintAlignment.Right, 20, True, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount", "Amount(Rs)", False, DosPrintAlignment.Right, 20, True, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Potrate)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found!", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCustom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustom._MYValidating
        Dim strQuery As String = " select Cust_Code as Code , Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        fndCustom.Value = clsCommon.ShowSelectForm("PrintSaleInvoiceMonthly", strQuery, "Code", "", fndCustom.Value, "Code", isButtonClicked)
        lblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndCustom.Value + "' "))
    End Sub
End Class
