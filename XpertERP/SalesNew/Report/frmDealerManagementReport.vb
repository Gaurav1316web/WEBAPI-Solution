'---------------Created By Monika(21/08/2014)----------BM00000003569-------
Imports common
Imports System.Data.SqlClient

Public Class FrmDealerManagementReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDealerManagementReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub FunReset()

        gv.Columns.Clear()
        gv.DataSource = Nothing
        txtfrm_date.Text = clsCommon.GETSERVERDATE(Nothing)
        txtto_date.Text = clsCommon.GETSERVERDATE(Nothing)
        chkCompAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkCustAll.IsChecked = True
        ChkAllDelear.IsChecked = True
        chkAll.IsChecked = True
        chksummary.IsChecked = True

        cbgCompany.CheckedAll()
        cbgItem.CheckedAll()
        cbgCustomer.CheckedAll()
        RadPageView1.SelectedPage = RadPageViewPage1

        btnpdf.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub FrmDealerManagementReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()

        LoadCompany()
        LoadCustomer()
        LoadItem()
        LoadDealer()
        RadGroupBox2.Visible = False

        Dim qry As String = "select count(*) from tspl_company_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and isnull(is_main_company,'0')='1'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow("This is not a main company.", Me.Text)
            btnrefresh.Enabled = False
            btnreset.Enabled = False
            btnexport.Enabled = False
        End If
    End Sub

    Private Sub LoadCompany()
        Dim qry As String = "select comp_code as Code,comp_name as Description from tspl_company_master order by comp_name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgCompany.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgCompany.DataSource = dt
            cbgCompany.DisplayMember = "Description"
            cbgCompany.ValueMember = "Code"
        End If
    End Sub

    Private Sub LoadItem()
        Dim qry As String = "select item_code as Code,item_desc as Description from tspl_item_master order by item_desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgItem.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgItem.DataSource = dt
            cbgItem.DisplayMember = "Description"
            cbgItem.ValueMember = "Code"
        End If
    End Sub
    Private Sub LoadDealer()
        Dim qry As String = "select EMP_CODE as Code ,Emp_Name as Description from TSPL_EMPLOYEE_MASTER where Emp_Type ='Service Dealer'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgServiceDealer.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgServiceDealer.DataSource = dt
            cbgServiceDealer.DisplayMember = "Description"
            cbgServiceDealer.ValueMember = "Code"
        End If
    End Sub

    Private Sub LoadCustomer()
        Dim qry As String = "select cust_code as Code,customer_name as Description from tspl_customer_master order by customer_name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgCustomer.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgCustomer.DataSource = dt
            cbgCustomer.DisplayMember = "Description"
            cbgCustomer.ValueMember = "Code"
        End If
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Try
            If chksummary.IsChecked AndAlso gv.Rows.Count <= 0 Then
                Print()
            ElseIf chkdetail.IsChecked AndAlso gv.Rows.Count <= 0 Then
                Print_Detail()
            End If

            Dim arr As New List(Of String)()
            If chksummary.IsChecked Then
                arr.Add("Dealer Sales Summary Report")
                arr.Add(" From :  " + clsCommon.GetPrintDate(txtfrm_date.Text, "dd/MMM/yyyy") + "  To : " + clsCommon.GetPrintDate(txtto_date.Text, "dd/MMM/yyyy"))
                clsCommon.MyExportToExcelGrid("Summary Report", gv, arr, "Dealer Summary Report")
            Else
                arr.Add("Dealer Sales Detail Report")
                arr.Add(" From :  " + clsCommon.GetPrintDate(txtfrm_date.Text, "dd/MMM/yyyy") + "  To : " + clsCommon.GetPrintDate(txtto_date.Text, "dd/MMM/yyyy"))
                clsCommon.MyExportToExcelGrid("Detail Report", gv, arr, "Dealer Detail Report")
            End If
            

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkPost.IsChecked = True Then
            VarID += "_P"
        End If
        If chksummary.IsChecked = True Then
            VarID += "_S"
        Else
            chkdetail.IsChecked = True
            VarID += "_D"
        End If
        gv.VarID = VarID
    End Sub
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        'If chksummary.IsChecked Then
        '    Print()
        'ElseIf chkdetail.IsChecked Then
        '    Print_Detail()
        'End If
        GetReportGridID()
        Referesh()
    End Sub

    Private Sub Print_Detail()
        Try
            Dim xpost As String = ""
            Dim xot_post As String = ""
            Dim frmdate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtfrm_date.Text), "dd/MMM/yyyy"))
            Dim todate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtto_date.Text), "dd/MMM/yyyy"))
            Dim itemcode As String = ""
            Dim customercode As String = ""
            Dim ot_itemcode As String = ""
            Dim ot_customercode As String = ""
            Dim inv_itemcode As String = ""


            If chkCompSelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 1 Then
                Throw New Exception("Select atleast two company")
            End If

            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one item")
            End If

            If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one customer")
            End If

            If chkItemSelect.IsChecked Then
                itemcode = " and TSPL_SD_SALE_INVOICE_DETAIL.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                ot_itemcode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                inv_itemcode = " and TSPL_INVENTORY_MOVEMENT.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If

            'If chkCustSelect.IsChecked Then
            If cbgCompany.CheckedValue.Count > 0 Then
                customercode = " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master where comp_code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "))" '" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + "
                ot_customercode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master where comp_code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "))"
            Else
                customercode = " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master)" '" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + "
                ot_customercode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master)"
            End If

            'End If

            If chkPost.IsChecked Then
                xpost = " and TSPL_SD_SALE_INVOICE_HEAD.status=1"
                xot_post = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.status=1"
            ElseIf chkUnpost.IsChecked Then
                xpost = " and TSPL_SD_SALE_INVOICE_HEAD.status<>1"
                xot_post = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.status<>1"
            End If

            Dim qry As String = ""
            Dim arrDataBase As New List(Of String)()
            arrDataBase = GetDataBaseName()
            Dim query As String = ""

            qry = "select xxb.Document_Code,xxb.Document_Date,xxb.Customer_Code,(xxb.Customer_Code+' $ '+xxb.Customer_Name) as Customer_Name,xxb.Item_Code,(xxb.Item_Code+' $ '+isnull(xxb.Item_Desc,'')) as Item_Desc,xxb.Unit_Code,isnull(xxb.op_qty,0) as op_qty,isnull(xxb.Other_DB_Qty,0) as Other_DB_Qty,isnull(xxa.Pending,0) as Pending from "
            qry += "(select aa.Document_Code,aa.Document_Date,aa.Customer_Code,aa.Customer_Name,aa.Item_Code,aa.Item_Desc,aa.Unit_Code,b.op_qty,aa.Other_DB_Qty from "
            qry += "(select a.Document_Code,a.Document_Date,a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code,SUM(a.qty) as qty,sum(a.Other_DB_Qty) as Other_DB_Qty,(sum(a.qty)-sum(a.Other_DB_Qty)) as balance from ("
            '------------------GETTING QTY OF MAIN COMPAN SALE------------------
            qry += "select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as qty,0 as Other_DB_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date between '" + frmdate + "' and '" + todate + "' " + customercode + " " + xpost + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code "
            '------------------GETTING QTY OF OTHER THAN MAIN COMPAN SALE------------------
            If arrDataBase IsNot Nothing AndAlso arrDataBase.Count > 0 Then
                query = "select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.document_code," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.document_date," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code,0 as qty,sum(" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Other_DB_Qty from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + frmdate + "' and '" + todate + "' " + ot_customercode + " " + xot_post + ") " + ot_itemcode + " group by " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.document_code," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.document_date"
                qry += clsCommon.GetQueryWithAllSelectedDataBase(query, arrDataBase, True)
            End If
            qry += " )a group by a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code,a.Document_Code,a.Document_Date)aa"
            qry += " left outer join "
            '--------------opening from main company -----(purchase-sale)-------------------------
            qry += "(select xx.Item_Code,(sum(xx.op_qty)-sum(xx.sale_op_qty)) as op_qty from (select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,0 as op_qty,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as sale_op_qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date < '" + frmdate + "' " + customercode + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            qry += "union all "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,sum(TSPL_INVENTORY_MOVEMENT.Qty) as op_qty,0 as sale_op_qty from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code where TSPL_INVENTORY_MOVEMENT.Source_Doc_Date < '" + frmdate + "' " + inv_itemcode + " group by TSPL_INVENTORY_MOVEMENT.Item_Code)xx group by xx.Item_Code "
            qry += ")b on aa.Item_Code=b.Item_Code) xxb"

            qry += " left outer join "

            '==================================pending qty======================================
            qry += "(select aa.Customer_Code,aa.Item_Code,((b.op_qty-aa.qty)+aa.balance) as Pending from "
            qry += "(select a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code,SUM(a.qty) as qty,sum(a.Other_DB_Qty) as Other_DB_Qty,(sum(a.qty)-sum(a.Other_DB_Qty)) as balance from ("
            '------------------GETTING QTY OF MAIN COMPAN SALE------------------
            qry += "select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as qty,0 as Other_DB_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date between '" + frmdate + "' and '" + todate + "' " + customercode + " " + xpost + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code "
            'qry += "union all "
            '------------------GETTING QTY OF OTHER THAN MAIN COMPAN SALE------------------
            If arrDataBase IsNot Nothing AndAlso arrDataBase.Count > 0 Then
                query = "select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code,0 as qty,sum(" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Other_DB_Qty from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + frmdate + "' and '" + todate + "' " + ot_customercode + " " + xot_post + ") " + ot_itemcode + " group by " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code"
                qry += clsCommon.GetQueryWithAllSelectedDataBase(query, arrDataBase, True)
            End If
            qry += " )a group by a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code)aa"
            qry += " left outer join "
            '--------------opening from main company -----(purchase-sale)-------------------------
            qry += "(select xx.Item_Code,(sum(xx.op_qty)-sum(xx.sale_op_qty)) as op_qty from (select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,0 as op_qty,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as sale_op_qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date < '" + frmdate + "' " + customercode + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            qry += "union all "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,sum(TSPL_INVENTORY_MOVEMENT.Qty) as op_qty,0 as sale_op_qty from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code where TSPL_INVENTORY_MOVEMENT.Source_Doc_Date < '" + frmdate + "' " + inv_itemcode + " group by TSPL_INVENTORY_MOVEMENT.Item_Code)xx group by xx.Item_Code "
            qry += ")b on aa.Item_Code=b.Item_Code ) xxa"
            qry += " on xxa.item_code=xxb.item_code and xxa.customer_code=xxb.customer_code"
            '========================================================================================

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt

                gv.SummaryRowsBottom.Clear()

                FormatGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No Data Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Print()
        Try
            Dim xpost As String = ""
            Dim xot_post As String = ""
            Dim frmdate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtfrm_date.Text), "dd/MMM/yyyy"))
            Dim todate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtto_date.Text), "dd/MMM/yyyy"))
            Dim itemcode As String = ""
            Dim customercode As String = ""
            Dim ot_itemcode As String = ""
            Dim ot_customercode As String = ""
            Dim inv_itemcode As String = ""


            If chkCompSelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 1 Then
                Throw New Exception("Select atleast two company")
            End If

            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one item")
            End If

            If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one customer")
            End If
            
            If chkItemSelect.IsChecked Then
                itemcode = " and TSPL_SD_SALE_INVOICE_DETAIL.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                ot_itemcode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                inv_itemcode = " and TSPL_INVENTORY_MOVEMENT.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If

            If cbgCompany.CheckedValue.Count > 0 Then
                customercode = " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master where comp_code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "))"
                ot_customercode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master where comp_code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + "))"
            Else
                customercode = " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master)"
                ot_customercode = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (select cust_code from tspl_company_master)"
            End If

            If chkPost.IsChecked Then
                xpost = " and TSPL_SD_SALE_INVOICE_HEAD.status=1"
                xot_post = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.status=1"
            ElseIf chkUnpost.IsChecked Then
                xpost = " and TSPL_SD_SALE_INVOICE_HEAD.status<>1"
                xot_post = " and " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.status<>1"
            End If

            Dim qry As String = ""
            Dim arrDataBase As New List(Of String)()
            arrDataBase = GetDataBaseName()
            Dim query As String = ""

            qry = "select aa.Customer_Code,aa.Customer_Name,aa.Item_Code,(aa.Item_Code+' $ '+isnull(aa.Item_Desc,'')) as Item_Desc,aa.Unit_Code,isnull(b.op_qty,0) as op_qty,isnull(aa.qty,0) as qty,isnull(aa.Other_DB_Qty,0) as Other_DB_Qty,isnull(aa.balance,0) as balance from "
            qry += "(select a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code,SUM(a.qty) as qty,sum(a.Other_DB_Qty) as Other_DB_Qty,(sum(a.qty)-sum(a.Other_DB_Qty)) as balance from ("
            '------------------GETTING QTY OF MAIN COMPAN SALE------------------
            qry += "select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as qty,0 as Other_DB_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date between '" + frmdate + "' and '" + todate + "' " + customercode + " " + xpost + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code "
            'qry += "union all "
            '------------------GETTING QTY OF OTHER THAN MAIN COMPAN SALE------------------
            If arrDataBase IsNot Nothing AndAlso arrDataBase.Count > 0 Then
                query = "select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code,0 as qty,sum(" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Other_DB_Qty from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code left outer join " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE from " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD where " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" + frmdate + "' and '" + todate + "' " + ot_customercode + " " + xot_post + ") " + ot_itemcode + " group by " + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name," + clsCommon.ReplicateDBString + "TSPL_SD_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code"
                qry += clsCommon.GetQueryWithAllSelectedDataBase(query, arrDataBase, True)
            End If
            qry += " )a group by a.Customer_Code,a.Customer_Name,a.Item_Code,a.Item_Desc,a.Unit_Code)aa"
            qry += " left outer join "
            '--------------opening from main company -----(purchase-sale)-------------------------
            qry += "(select xx.Item_Code,(sum(xx.op_qty)-sum(xx.sale_op_qty)) as op_qty from (select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,0 as op_qty,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as sale_op_qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where Document_Date < '" + frmdate + "' " + customercode + ") " + itemcode + " group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            qry += "union all "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,sum(TSPL_INVENTORY_MOVEMENT.Qty) as op_qty,0 as sale_op_qty from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code where TSPL_INVENTORY_MOVEMENT.Source_Doc_Date < '" + frmdate + "' " + inv_itemcode + " group by TSPL_INVENTORY_MOVEMENT.Item_Code)xx group by xx.Item_Code "
            qry += ")b on aa.Item_Code=b.Item_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.SummaryRowsBottom.Clear()
                FormatGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGridLayout()
        ' Dim strItemCode, head2 As String


        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.ShowFilteringRow = True

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If chkdetail.IsChecked Then
            gv.Columns("document_code").IsVisible = True
            gv.Columns("document_code").Width = 100
            gv.Columns("document_code").HeaderText = "Invoice No."
            gv.Columns("document_code").FormatString = ""

            gv.Columns("document_date").IsVisible = True
            gv.Columns("document_date").Width = 130
            gv.Columns("document_date").HeaderText = "Invoice Date"
            gv.Columns("document_date").FormatString = "{0:d}"
        End If

        gv.Columns("customer_code").IsVisible = True
        gv.Columns("customer_code").Width = 130
        gv.Columns("customer_code").HeaderText = "Customer Code"
        gv.Columns("customer_code").FormatString = ""


        gv.Columns("customer_name").IsVisible = True
        gv.Columns("customer_name").Width = 250
        gv.Columns("customer_name").HeaderText = "Customer Name"
        gv.Columns("customer_name").FormatString = ""


        gv.Columns("item_code").IsVisible = False
        gv.Columns("item_code").Width = 130
        gv.Columns("item_code").HeaderText = "Item Code"
        gv.Columns("item_code").FormatString = ""


        gv.Columns("item_desc").IsVisible = True
        gv.Columns("item_desc").Width = 250
        gv.Columns("item_desc").HeaderText = "Description"
        gv.Columns("item_desc").FormatString = ""


        gv.Columns("unit_code").Width = 100
        gv.Columns("unit_code").HeaderText = "UOM"
        gv.Columns("unit_code").IsVisible = True
        gv.Columns("unit_code").FormatString = ""

        gv.Columns("op_qty").IsVisible = True
        gv.Columns("op_qty").Width = 120
        gv.Columns("op_qty").HeaderText = "Opening Qty."
        gv.Columns("op_qty").FormatString = "{0:F2}"
        

        If chksummary.IsChecked Then
            gv.Columns("qty").IsVisible = True
            gv.Columns("qty").Width = 120
            gv.Columns("qty").HeaderText = "Sale Qty."
            gv.Columns("qty").FormatString = "{0:F2}"
        End If

        gv.Columns("other_db_qty").IsVisible = True
        gv.Columns("other_db_qty").Width = 120
        gv.Columns("other_db_qty").HeaderText = "Qty. Sold To Other"
        gv.Columns("other_db_qty").FormatString = "{0:F2}"

        If chksummary.IsChecked Then
            gv.Columns("balance").IsVisible = True
            gv.Columns("balance").Width = 120
            gv.Columns("balance").HeaderText = "Balance Qty."
            gv.Columns("balance").FormatString = "{0:F2}"
        End If
        If chkdetail.IsChecked Then
            gv.Columns("pending").IsVisible = True
            gv.Columns("pending").Width = 120
            gv.Columns("pending").HeaderText = "Pending Qty."
            gv.Columns("pending").FormatString = "{0:F2}"
        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item4 As New GridViewSummaryItem("other_db_qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        If chksummary.IsChecked Then
            Dim item3 As New GridViewSummaryItem("balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
        End If

        gv.GroupDescriptors.Add(New GridGroupByExpression("item_desc as Item format ""{0}: {1}"" Group By item_desc"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("op_qty as Item format ""{0}: {1}"" Group By op_qty"))
        If chkdetail.IsChecked Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("pending as item format""{0}: {1}"" Group By Pending"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("customer_Name as Item format""{0}: {1}""Group By Customer_Name"))
        End If

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub Referesh()
        Try
           
            Dim frmdate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtfrm_date.Text), "dd/MMM/yyyy"))
            Dim todate As String = clsCommon.myCstr(clsCommon.GetPrintDate((txtto_date.Text), "dd/MMM/yyyy"))
       


            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one item")
            End If

            If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one customer")
            End If

           
            Dim qry As String = ""
            Dim finalqry As String = ""
            qry = "select TSPL_SD_SHIPMENT_DETAIL.Document_Code ,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_SD_SHIPMENT_DETAIL.Qty ,TSPL_SD_SHIPMENT_DETAIL.Item_Cost ,TSPL_SD_SHIPMENT_DETAIL.Amount ,TSPL_SD_SALESMAN_TARGET_DETAIL.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name    from TSPL_SD_SHIPMENT_HEAD "
            qry += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code "

            qry += "left join (select TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code,TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code from TSPL_SD_SALESMAN_TARGET_DETAIL left outer join TSPL_SD_SALESMAN_TARGET_HEADER on TSPL_SD_SALESMAN_TARGET_HEADER.Code=TSPL_SD_SALESMAN_TARGET_DETAIL.Code where (month(monthyear) between '" + clsCommon.GetPrintDate(clsCommon.myCDate(frmdate), "MM") + "'  and '" + clsCommon.GetPrintDate(clsCommon.myCDate(todate), "MM") + "' ) and (year(monthyear) between '" + clsCommon.GetPrintDate(clsCommon.myCDate(frmdate), "yyyy") + "' and '" + clsCommon.GetPrintDate(clsCommon.myCDate(todate), "yyyy") + "'))TSPL_SD_SALESMAN_TARGET_DETAIL on TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code "

            qry += " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_SD_SALESMAN_TARGET_DETAIL.Salesman_Code "
            qry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code "
            qry += " where 2=2 and convert(date,Document_Date,103)>=convert(date,'" + frmdate + "',103) and convert(date,Document_Date,103) <=convert(date,'" + todate + "' ,103)"
            If chkItemSelect.IsChecked Then
                qry += " and  TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            End If
            If ChkSelectDelear.IsChecked Then
                qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgServiceDealer.CheckedValue) + ") "
            End If
           
            If chksummary.IsChecked Then
                finalqry = "select (DOCUMENT_CODE) as DOCUMENT_CODE,max(Document_Date) as Document_Date,sum(Qty )/2 as Qty,sum(Item_Cost )/2 as Item_Cost,sum(Amount )/2 as Amount,max(Salesman_Code ) as Salesman_Code,max(Emp_Name ) as Emp_Name from ("
                finalqry += "" & qry & ""
                finalqry += ")as xx group by DOCUMENT_CODE "
            ElseIf chkdetail.IsChecked Then
                finalqry = "" & qry & ""
            End If
           
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalqry)

            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.SummaryRowsBottom.Clear()
                FormatNewGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatNewGridLayout()
        ' Dim strItemCode, head2 As String


        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.ShowFilteringRow = True

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If chksummary.IsChecked Then
            gv.Columns("document_code").IsVisible = True
            gv.Columns("document_code").Width = 100
            gv.Columns("document_code").HeaderText = "Invoice No."
            gv.Columns("document_code").FormatString = ""

            gv.Columns("document_date").IsVisible = True
            gv.Columns("document_date").Width = 130
            gv.Columns("document_date").HeaderText = "Invoice Date"
            gv.Columns("document_date").FormatString = "{0:d}"
        End If

        'gv.Columns("customer_code").IsVisible = True
        'gv.Columns("customer_code").Width = 130
        'gv.Columns("customer_code").HeaderText = "Customer Code"
        'gv.Columns("customer_code").FormatString = ""


        'gv.Columns("customer_name").IsVisible = True
        'gv.Columns("customer_name").Width = 250
        'gv.Columns("customer_name").HeaderText = "Customer Name"
        'gv.Columns("customer_name").FormatString = ""
        If chkdetail.IsChecked Then
            gv.Columns("item_code").IsVisible = True
            gv.Columns("item_code").Width = 130
            gv.Columns("item_code").HeaderText = "Item Code"
            gv.Columns("item_code").FormatString = ""


            gv.Columns("item_desc").IsVisible = True
            gv.Columns("item_desc").Width = 250
            gv.Columns("item_desc").HeaderText = "Description"
            gv.Columns("item_desc").FormatString = ""

        End If

       

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 120
        gv.Columns("Qty").HeaderText = "Qty"
        gv.Columns("Qty").FormatString = "{0:F2}"


        gv.Columns("Item_Cost").IsVisible = True
        gv.Columns("Item_Cost").Width = 120
        gv.Columns("Item_Cost").HeaderText = "Item Cost"
        gv.Columns("Item_Cost").FormatString = "{0:F2}"

        gv.Columns("Amount").IsVisible = True
        gv.Columns("Amount").Width = 120
        gv.Columns("Amount").HeaderText = "Amount"
        gv.Columns("Amount").FormatString = "{0:F2}"

        gv.Columns("Salesman_Code").IsVisible = True
        gv.Columns("Salesman_Code").Width = 250
        gv.Columns("Salesman_Code").HeaderText = "Salesman Code"
        gv.Columns("Salesman_Code").FormatString = ""

        gv.Columns("Emp_Name").IsVisible = True
        gv.Columns("Emp_Name").Width = 250
        gv.Columns("Emp_Name").HeaderText = "Salesman Name"
        gv.Columns("Emp_Name").FormatString = ""
    


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        

        'gv.GroupDescriptors.Add(New GridGroupByExpression("item_desc as Item format ""{0}: {1}"" Group By item_desc"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("op_qty as Item format ""{0}: {1}"" Group By op_qty"))
        'If chkdetail.IsChecked Then
        '    gv.GroupDescriptors.Add(New GridGroupByExpression("pending as item format""{0}: {1}"" Group By Pending"))
        '    gv.GroupDescriptors.Add(New GridGroupByExpression("customer_Name as Item format""{0}: {1}""Group By Customer_Name"))
        'End If

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Function GetDataBaseName() As List(Of String)
        Dim arrDB As New List(Of String)()
        Dim qry As String = ""

        If chkCompSelect.IsChecked Then
            qry = "select distinct DataBase_Name from tspl_company_master where comp_code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + ") and isnull(Is_Main_Company,'0')<>'1'"
        Else
            qry = "select distinct DataBase_Name from tspl_company_master where isnull(Is_Main_Company,'0')<>'1'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                arrDB.Add(clsCommon.myCstr(dr("DataBase_Name")))
            Next
        End If

        Return arrDB
    End Function

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Try
            If chksummary.IsChecked AndAlso gv.Rows.Count <= 0 Then
                Print()
            ElseIf chkdetail.IsChecked AndAlso gv.Rows.Count <= 0 Then
                Print_Detail()
            End If

            Dim arr As New List(Of String)()
            If chksummary.IsChecked Then
                arr.Add("Dealer Sales Summary Report")
                arr.Add(" From :  " + clsCommon.GetPrintDate(txtfrm_date.Text, "dd/MMM/yyyy") + "  To : " + clsCommon.GetPrintDate(txtto_date.Text, "dd/MMM/yyyy"))
                clsCommon.MyExportToPDF("Summary Report", gv, arr, "Dealer Summary Report")
            Else
                arr.Add("Dealer Sales Detail Report")
                arr.Add(" From :  " + clsCommon.GetPrintDate(txtfrm_date.Text, "dd/MMM/yyyy") + "  To : " + clsCommon.GetPrintDate(txtto_date.Text, "dd/MMM/yyyy"))
                clsCommon.MyExportToPDF("Detail Report", gv, arr, "Dealer Detail Report")
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkCompAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCompAll.ToggleStateChanged
        cbgCompany.Enabled = chkCompSelect.IsChecked
        If chkCompSelect.IsChecked Then
            cbgCompany.UnCheckedAll()
        End If
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = chkItemSelect.IsChecked
        If chkItemSelect.IsChecked Then
            cbgItem.UnCheckedAll()
        End If
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = chkCustSelect.IsChecked
        If chkCustSelect.IsChecked Then
            cbgCustomer.UnCheckedAll()
        End If
    End Sub

    Private Sub ChkAllDelear_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkAllDelear.ToggleStateChanged
        cbgServiceDealer.Enabled = ChkSelectDelear.IsChecked
        If ChkSelectDelear.IsChecked Then
            cbgServiceDealer.UnCheckedAll()
        End If
    End Sub
End Class
