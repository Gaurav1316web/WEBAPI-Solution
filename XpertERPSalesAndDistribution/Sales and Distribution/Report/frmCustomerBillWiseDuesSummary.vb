'---------------------BM00000003413
'Anand Ticket No:BM00000003627 
'BM00000003549
Imports XpertERPEngine
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.IO

Public Class frmCustomerBillWiseDuesSummary
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerBillWiseDuesSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Public Sub loadCustomerCode()
        Dim qry11 As String = "SELECT  Cust_Code,Customer_Name FROM TSPL_CUSTOMER_MASTER where Parent_Customer_YN='N'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub
    Private Sub CustomerBillWiseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadCustomerCode()
        LoadLocation()
        LoadCategory()
        reset()
        btnpdf.Visibility = ElementVisibility.Collapsed
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_code , Location_desc  from TSPL_LOCATION_MASTER"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_desc"
    End Sub
    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkChapterAll.IsChecked = True
        MyRadioButton4.IsChecked = True
        cbgCustomer.CheckedAll()
        cbgLocation.CheckedAll()
        cbgCustomer.Enabled = False
        cbgLocation.Enabled = False
        tvCategory.Enabled = False
        rbtnCategoryAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Columns.Clear()
        chkcatewise.Checked = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2, '" & objCommonVar.CurrentUser & "' as User_Name ,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,  TSPL_Customer_Invoice_Head.Customer_Code , TSPL_CUSTOMER_MASTER.Customer_Name,(case when isnull(TSPL_CUSTOMER_MASTER.Add1,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add1 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add2,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add2 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add3,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add3 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.PIN_Code ,'')='' then ''  else TSPL_CUSTOMER_MASTER.PIN_Code  + ',' end ) as [Address],TSPL_CUSTOMER_MASTER.Contact_Person_Name ,TSPL_Customer_Invoice_Head.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc   ,tspl_customer_invoice_head.Document_Date , yyy.[Document Id], case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.Against_Sale_No end as 'DOC ID', TSPL_Customer_Invoice_Head.Document_Total, yyy.Pending   from  (  select [Document Id]  , sum([Due Amount]) as [Pending]  from (  SELECT TSPL_Customer_Invoice_Head.Document_No  as [Document Id], (case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end)-(Select ISNULL(SUM(Item_Net_Amt),0) from TSPL_SD_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Status=1 AND Invoice_Code=TSPL_Customer_Invoice_Head.Against_Sale_No) as [Due Amount] FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'  AND TSPL_CUSTOMER_MASTER.Status='N'   UNION All  select TSPL_RECEIPT_DETAIL.Document_No, Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No  where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0  AND TSPL_CUSTOMER_MASTER.Status='N'     ) xxx group by xxx.[Document Id]    having SUM(xxx.[Due Amount])>0     ) yyy     left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=yyy.[Document Id]    left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_Customer_Invoice_Head.Customer_Code   left outer join tspl_location_master on tspl_location_master.location_code=TSPL_Customer_Invoice_Head.Loc_Code   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Customer_Invoice_Head .Comp_Code  "



            If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
            End If
            If MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLoc = ""
            End If

            strDateRange = " where  CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer

            '-------------------------N-Level Category-------04/08/2014--BM00000003354-------------------------------------
            Dim whrcate As String = ""
            Dim Strr As String = Nothing

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True

                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            whrcate += " or "
                        End If

                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                Strr += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                Strr = ""
                Dim query As String = ""
                query = qry
                qry = ""

                whrcate = "and (" + whrcate + ")"

                Strr = "select distinct axs.Against_Sale_No,TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_ITEM_MASTER_CATEGORY.item_category_code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
                Strr += "from (select distinct TSPL_Customer_Invoice_Head.Against_Sale_No from TSPL_Customer_Invoice_Head " + strDateRange + " " + strLoc + " " + strCustomer + ")axs left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=axs.Against_Sale_No "
                Strr += "left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_sd_sale_invoice_detail.item_code "
                Strr += "where isnull(axs.Against_Sale_No,'')<>'' " + whrcate + ""

                qry = "select ada.*,(select distinct ','+a.Item_Category_Code,','+a.Item_Cagetory_Values from (" + Strr + ")a where a.Against_Sale_No=ada.[Doc Id]  for XML path ('')) as Category"
                qry += " from (" + query + ")ada where ada.[doc id] in (select distinct Against_Sale_No from (" + Strr + ")a)"
            End If

            '---------------------------------------------------------------------------------------------

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = dt
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptCustBillWiseDuesSummary", "Customer Bill Wise Dues Summry")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "Start Date Can Not Be Greater Then End Date", Me.Text)
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select At least One Customer", Me.Text)
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If

       
        print()
    End Sub

    Private Sub CategorywisePrint() '-------------------BM00000003379
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_Customer_Invoice_Head.Customer_Code , TSPL_CUSTOMER_MASTER.Customer_Name,(case when isnull(TSPL_CUSTOMER_MASTER.Add1,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add1 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add2,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add2 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add3,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add3 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.PIN_Code ,'')='' then ''  else TSPL_CUSTOMER_MASTER.PIN_Code  + ',' end ) as [Address],TSPL_CUSTOMER_MASTER.Contact_Person_Name ,TSPL_Customer_Invoice_Head.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc   ,tspl_customer_invoice_head.Document_Date , yyy.[Document Id], case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.Against_Sale_No end as 'DOC ID', TSPL_Customer_Invoice_Head.Document_Total, yyy.Pending,TSPL_RECEIPT_CHALLAN.grno,TSPL_RECEIPT_CHALLAN.grdate from  (  select [Document Id]  , sum([Due Amount]) as [Pending]  from (  SELECT TSPL_Customer_Invoice_Head.Document_No  as [Document Id], (case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end)-(Select ISNULL(SUM(Item_Net_Amt),0) from TSPL_SD_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Status=1 AND Invoice_Code=TSPL_Customer_Invoice_Head.Against_Sale_No) as [Due Amount] FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'  AND TSPL_CUSTOMER_MASTER.Status='N'   UNION All  select TSPL_RECEIPT_DETAIL.Document_No, Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No  where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0  AND TSPL_CUSTOMER_MASTER.Status='N'     ) xxx group by xxx.[Document Id]    having SUM(xxx.[Due Amount])>0     ) yyy     left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=yyy.[Document Id]    left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_Customer_Invoice_Head.Customer_Code   left outer join tspl_location_master on tspl_location_master.location_code=TSPL_Customer_Invoice_Head.Loc_Code   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Customer_Invoice_Head .Comp_Code  " ',TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2
            qry += " left outer join TSPL_RECEIPT_CHALLAN on TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO=TSPL_Customer_Invoice_Head.Against_Sale_No"

            If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
            End If
            If MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLoc = ""
            End If

            strDateRange = " where  CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer

            '-------------------------N-Level Category-------04/08/2014-----BM00000003354----------------------------------
            Dim whrcate As String = ""
            Dim Strr As String = Nothing
            Dim PivotHeader As String = ""

            Strr = "select distinct (select distinct ',['+ITEM_CATEGORY_CODE+']' from TSPL_ITEM_CATEGORY_LEVEL where category_level='1' for xml path('')) as xvalue"
            PivotHeader = clsDBFuncationality.getSingleValue(Strr)
            If clsCommon.myLen(PivotHeader) > 0 Then
                Try
                    If PivotHeader.Substring(0, 1) = "," Then
                        PivotHeader = PivotHeader.Substring(1, PivotHeader.Length - 1)
                    End If
                Catch exx As Exception

                End Try
            End If

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True

                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            whrcate += " or "
                        End If

                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                Strr += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                Strr = "select distinct (select distinct ',['+ITEM_CATEGORY_CODE+']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('')) as xvalue"
                PivotHeader = clsDBFuncationality.getSingleValue(Strr)
                If clsCommon.myLen(PivotHeader) > 0 Then
                    Try
                        If PivotHeader.Substring(0, 1) = "," Then
                            PivotHeader = PivotHeader.Substring(1, PivotHeader.Length - 1)
                        End If
                    Catch exx As Exception

                    End Try
                End If

                Strr = ""
                Dim query As String = ""
                query = qry
                qry = ""

                whrcate = "and (" + whrcate + ")"

                Strr = "select distinct axs.Against_Sale_No,TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_ITEM_MASTER_CATEGORY.item_category_code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_RECEIPT_CHALLAN.grno,TSPL_RECEIPT_CHALLAN.grdate "
                Strr += "from (select distinct TSPL_Customer_Invoice_Head.Against_Sale_No from TSPL_Customer_Invoice_Head " + strDateRange + " " + strLoc + " " + strCustomer + ")axs left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=axs.Against_Sale_No "
                Strr += "left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_sd_sale_invoice_detail.item_code "
                Strr += "left outer join TSPL_RECEIPT_CHALLAN on TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO=axs.Against_Sale_No "
                Strr += "where isnull(axs.Against_Sale_No,'')<>'' " + whrcate + ""

                'qry = "select ada.*"
                'qry += " from (" + query + ")ada where ada.[doc id] in (select distinct Against_Sale_No from (" + Strr + ")a)"
                qry = "select * from (select aad.Comp_Code ,aad.Comp_Name,aad.Add1,aad.Add2,aad.Add3,aad.Pincode ,aad.Phone1,aad.Phone2,aad.Parent_Customer_No,max(Parent_Master.Customer_Name) as ParentName ,aad.Customer_Code , aad.Customer_Name,aad.[Address],aad.Contact_Person_Name ,aad.Loc_Code,aad.Location_Desc   ,aad.Document_Date , aad.[Document Id],aad.[DOC ID], aad.Document_Total, aad.Pending,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,sum(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt) as [Total Taxable Amt],aad.grno,aad.grdate from (" + query + ")aad left outer join TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aad.Parent_Customer_No  left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=aad.[doc id] left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where 1=1 " + whrcate + " group by aad.Comp_Code ,aad.Comp_Name,aad.Add1,aad.Add2,aad.Add3,aad.Pincode ,aad.Phone1,aad.Phone2, aad.Parent_Customer_No ,aad.Customer_Code , aad.Customer_Name,aad.[Address],aad.Contact_Person_Name ,aad.Loc_Code,aad.Location_Desc   ,aad.Document_Date , aad.[Document Id],aad.[DOC ID], aad.Document_Total, aad.Pending,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,aad.grno,aad.grdate) as s pivot(max(Item_Cagetory_Values) for Item_Category_Code in (" + PivotHeader + "))t "
            Else
                qry = "select * from (select aad.Comp_Code ,aad.Comp_Name,aad.Add1,aad.Add2,aad.Add3,aad.Pincode ,aad.Phone1,aad.Phone2,aad.Parent_Customer_No,max(Parent_Master.Customer_Name) as ParentName ,  aad.Customer_Code , aad.Customer_Name,aad.[Address],aad.Contact_Person_Name ,aad.Loc_Code,aad.Location_Desc   ,aad.Document_Date , aad.[Document Id],aad.[DOC ID], aad.Document_Total, aad.Pending,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,sum(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt) as [Total Taxable Amt],aad.grno,aad.grdate from (" + qry + ")aad left outer join TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aad.Parent_Customer_No left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=aad.[doc id] left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code in (select distinct Item_Category_Code from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL='1') group by aad.Comp_Code ,aad.Comp_Name,aad.Add1,aad.Add2,aad.Add3,aad.Pincode ,aad.Phone1,aad.Phone2,  aad.Parent_Customer_No,aad.Customer_Code , aad.Customer_Name,aad.[Address],aad.Contact_Person_Name ,aad.Loc_Code,aad.Location_Desc   ,aad.Document_Date , aad.[Document Id],aad.[DOC ID], aad.Document_Total, aad.Pending,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,aad.grno,aad.grdate) as s pivot(max(Item_Cagetory_Values) for Item_Category_Code in (" + PivotHeader + "))t"
            End If
            '---------------------------------------------------------------------------------------------

            qry = "select final.*, isnull(TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,'')  as Po_no from ( " + qry + ") as final left outer join TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE = final.[doc id] "

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormatOFGV1()
                gv1.MasterTemplate.AllowAddNewRow = False
                RadPageView1.SelectedPage = RadPageViewPage2
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub RadPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPanel1.Paint

    End Sub

    Private Sub chkChapterSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterSelect.ToggleStateChanged
        If chkChapterSelect.IsChecked() Then
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        Else
            cbgCustomer.UnCheckedAll()
            cbgCustomer.Enabled = False
        End If

    End Sub

    Private Sub MyRadioButton3_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton3.ToggleStateChanged
        If MyRadioButton3.IsChecked() Then
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = True
        Else
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = False
        End If
    End Sub

    Private Sub chkChapterAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterAll.ToggleStateChanged
        If chkChapterAll.IsChecked() Then
            cbgCustomer.CheckedAll()
            cbgCustomer.Enabled = False
        Else
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        End If

    End Sub

    Private Sub MyRadioButton4_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton4.ToggleStateChanged
        If MyRadioButton4.IsChecked() Then
            cbgLocation.CheckedAll()
            cbgLocation.Enabled = False
        Else
            cbgLocation.UnCheckedAll()

            cbgLocation.Enabled = True
        End If
    End Sub
    Sub RefreshData()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_Customer_Invoice_Head.Customer_Code , TSPL_CUSTOMER_MASTER.Customer_Name,(case when isnull(TSPL_CUSTOMER_MASTER.Add1,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add1 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add2,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add2 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.Add3,'')='' then ''  else TSPL_CUSTOMER_MASTER.Add3 + ',' end + case when isnull(TSPL_CUSTOMER_MASTER.PIN_Code ,'')='' then ''  else TSPL_CUSTOMER_MASTER.PIN_Code  + ',' end ) as [Address],TSPL_CUSTOMER_MASTER.Contact_Person_Name ,TSPL_Customer_Invoice_Head.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc   ,tspl_customer_invoice_head.Document_Date , yyy.[Document Id], case when isnull(TSPL_Customer_Invoice_Head.Against_Sale_No,'')='' then TSPL_Customer_Invoice_Head.Document_No else TSPL_Customer_Invoice_Head.Against_Sale_No end as 'DOC ID', TSPL_Customer_Invoice_Head.Document_Total, yyy.Pending,TSPL_RECEIPT_CHALLAN.grno,TSPL_RECEIPT_CHALLAN.grdate   from  (  select [Document Id]  , sum([Due Amount]) as [Pending]  from (  SELECT TSPL_Customer_Invoice_Head.Document_No  as [Document Id], (case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end)-(Select ISNULL(SUM(Item_Net_Amt),0) from TSPL_SD_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Status=1 AND Invoice_Code=TSPL_Customer_Invoice_Head.Against_Sale_No) as [Due Amount] FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'  AND TSPL_CUSTOMER_MASTER.Status='N'   UNION All  select TSPL_RECEIPT_DETAIL.Document_No, Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No  where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0  AND TSPL_CUSTOMER_MASTER.Status='N'     ) xxx group by xxx.[Document Id]    having SUM(xxx.[Due Amount])>0     ) yyy     left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=yyy.[Document Id]    left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_Customer_Invoice_Head.Customer_Code   left outer join tspl_location_master on tspl_location_master.location_code=TSPL_Customer_Invoice_Head.Loc_Code   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Customer_Invoice_Head .Comp_Code  "
            qry += " left outer join TSPL_RECEIPT_CHALLAN on TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO=TSPL_Customer_Invoice_Head.Against_Sale_No"

            If chkChapterSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_Customer_Invoice_Head.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
            End If
            If MyRadioButton3.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_Customer_Invoice_Head.Loc_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLoc = ""
            End If

            strDateRange = " where  CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_Customer_Invoice_Head.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strLoc
            qry += strCustomer

            '-------------------------N-Level Category-------04/08/2014-----BM00000003354----------------------------------
            Dim whrcate As String = ""
            Dim Strr As String = Nothing

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True

                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            whrcate += " or "
                        End If

                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                Strr += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                Strr = ""
                Dim query As String = ""
                query = qry
                qry = ""

                whrcate = "and (" + whrcate + ")"

                Strr = "select distinct axs.Against_Sale_No,TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_ITEM_MASTER_CATEGORY.item_category_code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_RECEIPT_CHALLAN.grno,TSPL_RECEIPT_CHALLAN.grdate "
                Strr += "from (select distinct TSPL_Customer_Invoice_Head.Against_Sale_No from TSPL_Customer_Invoice_Head " + strDateRange + " " + strLoc + " " + strCustomer + ")axs left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=axs.Against_Sale_No "
                Strr += "left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_sd_sale_invoice_detail.item_code "
                Strr += "left outer join TSPL_RECEIPT_CHALLAN on TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO=axs.Against_Sale_No "
                Strr += "where isnull(axs.Against_Sale_No,'')<>'' " + whrcate + ""

                qry = "select ada.*,(select distinct ','+a.Item_Category_Code,','+a.Item_Cagetory_Values from (" + Strr + ")a where a.Against_Sale_No=ada.[Doc Id]  for XML path ('')) as Category"
                qry += " from (" + query + ")ada where ada.[doc id] in (select distinct Against_Sale_No from (" + Strr + ")a)"
            End If

            '---------------------------------------------------------------------------------------------

            qry = "select final.*, isnull(TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,'')  as Po_no from ( " + qry + ") as final left outer join TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE = final.[doc id] "
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormatOFGV1()
                gv1.MasterTemplate.AllowAddNewRow = False
                RadPageView1.SelectedPage = RadPageViewPage2

            End If

        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormatOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
            If chkcatewise.Checked AndAlso ii > 18 Then
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            End If
        Next
        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").Width = 200
        gv1.Columns("Document_Date").HeaderText = " Date"
        gv1.Columns("Document_Date").FormatString = "{0:d}"

        gv1.Columns("DOC ID").IsVisible = True
        gv1.Columns("DOC ID").Width = 200
        gv1.Columns("DOC ID").HeaderText = "Invoice No."

        gv1.Columns("Document_Total").IsVisible = True
        gv1.Columns("Document_Total").Width = 100
        gv1.Columns("Document_Total").HeaderText = "Invoice Amount"

        gv1.Columns("Pending").IsVisible = True
        gv1.Columns("Pending").Width = 100
        gv1.Columns("Pending").HeaderText = "Pending"

        gv1.Columns("grno").IsVisible = True
        gv1.Columns("grno").Width = 100
        gv1.Columns("grno").HeaderText = "GR No."

        gv1.Columns("grdate").IsVisible = True
        gv1.Columns("grdate").Width = 100
        gv1.Columns("grdate").HeaderText = "GR Date"

        gv1.Columns("Location_Desc").IsVisible = False
        gv1.Columns("Location_Desc").Width = 200
        gv1.Columns("Location_Desc").HeaderText = " Location Desc"

        gv1.Columns("Parent_Customer_No").IsVisible = False
        gv1.Columns("Parent_Customer_No").Width = 200
        gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer code"

        gv1.Columns("Customer_Code").IsVisible = False
        gv1.Columns("Customer_Code").Width = 200
        gv1.Columns("Customer_Code").HeaderText = " Customer code"

        gv1.Columns("Po_no").IsVisible = True
        gv1.Columns("Po_no").Width = 200
        gv1.Columns("Po_no").HeaderText = "PO No"

        Try
            gv1.Columns("Category").IsVisible = True
            gv1.Columns("Category").Width = 200
            gv1.Columns("Category").HeaderText = "Category"
        Catch exx As Exception
        End Try

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Document_Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Pending", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Parent_Customer_No as Item format ""{0}: {1}"" Group By Parent_Customer_No"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Customer_Code as Item format ""{0}: {1}"" Group By Customer_Code"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))





        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ReStoreGridLayout()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkcatewise.IsChecked = True Then
            VarID += "_SU"
        End If
        gv1.VarID = VarID
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetReportGridID()
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "Start Date Can Not Be Greater Then End Date", Me.Text)
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select At least One Customer", Me.Text)
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If

        If chkcatewise.Checked Then
            CategorywisePrint()
        Else
            RefreshData()
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If clsCommon.myLen(gv1.CurrentRow.Cells("DOC ID").Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, gv1.CurrentRow.Cells("DOC ID").Value)

        End If

    End Sub

    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub btnsavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
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
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btndeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim arr As New List(Of String)()
        arr.Add("Customer Bill Dues Summary")
        arr.Add(" From :  " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + "  To : " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
        If gv1.Rows.Count > 0 Then
            clsCommon.MyExportToExcelGrid("Customer Bill Dues Summary", gv1, arr, "Summary")
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Dim arr As New List(Of String)()
        arr.Add("Customer Bill Dues Summary")
        arr.Add(" From :  " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + "  To : " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
        If gv1.Rows.Count > 0 Then
            clsCommon.MyExportToPDF("Customer Bill Dues Summary", gv1, arr, "Summary")
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If
    End Sub
End Class
