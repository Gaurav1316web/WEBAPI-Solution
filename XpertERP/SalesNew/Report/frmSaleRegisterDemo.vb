'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'----preeti gupta---ticket no.-BM00000003031
'----Priti --ticket no.-BM00000003331
'Anand --Ticket No:BM00000003625
''BM00000008324
'''' BM00000003620
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

'---------------------------------------BM00000000767------------------------------------------------------------ Done by Shipra'
Public Class FrmSaleRegisterDemo
    Inherits FrmMainTranScreen
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleRegisterDemo)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub
#End Region

    Dim atchqry As String = ""

    Private Sub FrmSaleRegisterDemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadLocation()
        LoadItem()
        LoadCategory()

        rbtnCategoryAll.IsChecked = True
        rbtnCustomerAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        rbtnItemAll.IsChecked = True
        rdbSummary.IsChecked = True
        ddlSaleType.SelectedIndex = 0
        btnPosted.IsChecked = True
        btnsetting.Visible = False
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
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 and Parent_Customer_YN='N' "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print(Exporter.Refresh)
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            If rbtnItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one item")
            End If
            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one location")
            End If
            If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one customer")
            End If
            If clsCommon.myLen(ddlSaleType.SelectedIndex) < 0 Then
                Throw New Exception("Please select at least one Sales Type")
            End If

            If chkSerializeInv.Checked AndAlso rdbDetail.IsChecked = False Then
                Throw New Exception("Please Select Detail Type Report")
            End If

            Dim str As String = ""
            Dim strSaleInvoice As String = ""
            Dim strSaleReturn As String = ""
            Dim strInvoicePosted As String = ""
            Dim strReturnPosted As String = ""
            If btnAll.IsChecked Then
                strInvoicePosted = ""
                strReturnPosted = ""
            ElseIf btnPosted.IsChecked Then
                strInvoicePosted = " TSPL_SD_SALE_INVOICE_HEAD.status=1 and "
                strReturnPosted = " TSPL_SD_SALE_RETURN_HEAD.status=1 and "
            ElseIf btnUnposted.IsChecked Then
                strInvoicePosted = " TSPL_SD_SALE_INVOICE_HEAD.status=0 and "
                strReturnPosted = " TSPL_SD_SALE_RETURN_HEAD.status=0 and "
            End If

            strSaleInvoice = "SELECT TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor as OldConvF,OrgRate,'" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Material Sale' else 'Sale Invoice' end as Type, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " & _
                        "TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, " & _
                        "TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Parent_Customer_No,case when isnull (TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,'')='' then (TSPL_CUSTOMER_MASTER.add1+' '+TSPL_CUSTOMER_MASTER.add2+' '+TSPL_CUSTOMER_MASTER.add3)  else TSPL_SHIP_TO_LOCATION.Add1 +' ' + TSPL_SHIP_TO_LOCATION.Add2 +' ' + TSPL_SHIP_TO_LOCATION.Add3  end as Address,tspl_city_master.city_name,tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code, " & _
                        "TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, " & _
                        "TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code, TSPL_SD_SALE_INVOICE_DETAIL.Qty as InvoiceQty, 0 as ShippedQty, " & _
                        "TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,Amount,Disc_Per,Disc_Amt, " & _
                        " ((case when TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item=0 then 1 else 0 end)*(case when TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt=0 then 0 else 1 end) * TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount) as Amt_Less_Discount,((case when TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item=0 then 1 else 0 end) *TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt) as Total_Tax_Amt, " & _
                        "TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt,TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE," & _
                        "(case when TSPL_SD_SALE_INVOICE_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_INVOICE_HEAD.ConvRate end) as ConvRate, " & _
                        "(case when TSPL_SD_SALE_INVOICE_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_INVOICE_HEAD.ConvRate end)*TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as Total_BaseCurr FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                        "TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                        "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                        "TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON " & _
                        "TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
                        " left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location " & _
                        " where " & strInvoicePosted & " TSPL_SD_SALE_INVOICE_HEAD.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and " & _
                        " TSPL_SD_SALE_INVOICE_HEAD.Document_Date  <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'  "

            If rbtnItemSelect.IsChecked Then
                strSaleInvoice += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If rbtnLocationSelect.IsChecked Then
                strSaleInvoice += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                strSaleInvoice += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            Dim strInvType As String = ""
            If chkTax.IsChecked Then
                strInvType = " and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='T'"
            ElseIf chkRetail.IsChecked Then
                strInvType = " and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type='R'"
            Else
                strInvType = " "
            End If
            strSaleInvoice += strInvType

            strSaleReturn = "SELECT    TSPL_SD_SALE_RETURN_DETAIL.Conv_Factor as OldConvF,OrgRate,'" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, 'Sale Return' as Type, TSPL_SD_SALE_RETURN_HEAD.Document_Code, " & _
                            "TSPL_SD_SALE_RETURN_HEAD.Document_Date, TSPL_SD_SALE_RETURN_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Parent_Customer_No,case when isnull (TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location ,'')='' then (TSPL_CUSTOMER_MASTER.add1+' '+TSPL_CUSTOMER_MASTER.add2+' '+TSPL_CUSTOMER_MASTER.add3)  else TSPL_SHIP_TO_LOCATION.Add1 +' ' + TSPL_SHIP_TO_LOCATION.Add2 +' ' + TSPL_SHIP_TO_LOCATION.Add3  end as Address,tspl_city_master.city_name,tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, " & _
                            "TSPL_SD_SALE_RETURN_HEAD.Salesman_Code, TSPL_SD_SALE_RETURN_HEAD.Salesman_Name,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_RETURN_DETAIL.Item_Code, " & _
                            "TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code, - TSPL_SD_SALE_RETURN_DETAIL.Qty as InvoiceQty, 0 as ShippedQty, TSPL_SD_SALE_RETURN_DETAIL.Unit_code, " & _
                            "Item_Cost as rate,(-1 * Amount) as Amount, (-1 * Disc_Per) as Disc_Per,Disc_Amt, (-1* TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(case when TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt=0 then 0 else 1 end)) as Amt_Less_Discount,  " & _
                            "(-1 * TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt) as Total_Tax_Amt, - TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt as TotalAmt,TSPL_SD_SALE_RETURN_HEAD.CURRENCY_CODE,(case when TSPL_SD_SALE_RETURN_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_RETURN_HEAD.ConvRate end) as ConvRate," & _
                            " (case when TSPL_SD_SALE_RETURN_HEAD.ConvRate=0 then 1 else TSPL_SD_SALE_RETURN_HEAD.ConvRate end)*TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt  * -1 as Total_BaseCurr  FROM  " & _
                            "TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON " & _
                            "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                            "TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_SD_SALE_RETURN_DETAIL ON " & _
                            "TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code ON " & _
                            "TSPL_SD_SALE_RETURN_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Document_Code  " & _
                            " left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  = TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_Return_HEAD.Ship_To_Location " & _
                            " where " & strReturnPosted & "  TSPL_SD_SALE_RETURN_HEAD.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
                            " TSPL_SD_SALE_RETURN_HEAD.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"
            If rbtnItemSelect.IsChecked Then
                strSaleReturn += "  and  TSPL_SD_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If rbtnLocationSelect.IsChecked Then
                strSaleReturn += " and  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                strSaleReturn += "  and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If



            If ddlSaleType.SelectedIndex = 0 Then
                str = strSaleInvoice + " union  all " + strSaleReturn
            ElseIf ddlSaleType.SelectedIndex = 1 Then
                str = strSaleInvoice
            Else
                str = strSaleReturn
            End If

            If clsCommon.myLen(txtUOM.Value) > 0 Then
                'str = "select Fdate, Tdate,Type, Document_Code, Document_Date, Customer_Code, Customer_Name,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxxx.Item_Code, Item_Desc,CONVERT(decimal(18,2), InvoiceQty*DivideConverstionFactor) as InvoiceQty, ShippedQty, '" + txtUOM.Value + "' as Unit_code, rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr from ( select  Fdate, Tdate,Type, Document_Code, Document_Date, Customer_Code, Customer_Name,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxx.Item_Code, Item_Desc, (InvoiceQty*(1/TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) as InvoiceQty, ShippedQty,Unit_code, rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr,TSPL_ITEM_UOM_DETAILForDivide.Conversion_Factor as DivideConverstionFactor  from(" + str + " ) xxx  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.Unit_code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + txtUOM.Value + "'  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAILForDivide on TSPL_ITEM_UOM_DETAILForDivide.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAILForDivide.UOM_Code='" + txtUOM.Value + "'  )xxxx where xxxx.unit_code='" + txtUOM.Value + "' and DivideConverstionFactor is not null"
                '==============Updated By Rohit on June 04,2014 to show data according to conversion Unit.==================
                str = "select Fdate, Tdate,Type,Parent_Customer_No, Document_Code, Document_Date, Customer_Code, Customer_Name,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxxx.Item_Code, Item_Desc,itf_code,CONVERT(decimal(18,2), InvoiceQty) as InvoiceQty, ShippedQty, '" + txtUOM.Value + "' as Unit_code, rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr from ( select  Fdate, Tdate,Type,Parent_Customer_No, Document_Code, Document_Date, Customer_Code, Customer_Name,address,city_name,state_name,telephone, Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc ,xxx.Item_Code, Item_Desc,itf_code, (InvoiceQty*OldConvF/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as InvoiceQty, ShippedQty,TSPL_ITEM_UOM_DETAIL.UOM_code as Unit_code,InvoiceQty*OrgRate as rate,Amount,Disc_Per,Disc_Amt, Amt_Less_Discount,Total_Tax_Amt,TotalAmt,currency_code,convrate,total_basecurr,TSPL_ITEM_UOM_DETAILForDivide.Conversion_Factor as DivideConverstionFactor  from(" + str + " ) xxx  Inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + txtUOM.Value + "'  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAILForDivide on TSPL_ITEM_UOM_DETAILForDivide.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAILForDivide.UOM_Code='" + txtUOM.Value + "'  )xxxx where xxxx.unit_code='" + txtUOM.Value + "' and DivideConverstionFactor is not null"
                '====================================================================================
            End If

            '***********************************************************
            Dim qry As String
            qry = "select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name],axa.* from (" + str + ")axa left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=axa.item_code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values"
            'qry = "select ass.* from (" + qry + ")ass"

            Dim whrcate As String = ""

            'If rbtnCategorySelect.IsChecked Then
            '    Dim isFirstTime As Boolean = True
            '    qry += " where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + str + ")a1) and ( " + Environment.NewLine
            '    For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
            '        If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
            '            If Not isFirstTime Then
            '                qry += " or "
            '                whrcate += " or "
            '            End If
            '            qry += " ( maingroupcode='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and groupcode='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
            '            whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
            '            isFirstTime = False
            '        End If
            '    Next
            '    qry += " ))"
            '    If isFirstTime Then
            '        Throw New Exception("Please select at least one Category")
            '        Return
            '    End If
            'End If

            '' new code for category (Panch Raj)
            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True
                qry += " where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + str + ")a1)) and ( " + Environment.NewLine
                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            qry += " or "
                            whrcate += " or "
                        End If
                        qry += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                qry += " )"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                    Return
                End If
            End If


            If clsCommon.myLen(whrcate) > 0 Then
                whrcate = " and " + whrcate
            End If
            '***********************************************************
            str = qry
            qry = "select distinct fdate,tdate,type,document_code,document_date,Parent_Customer_No,customer_code,customer_name,address,city_name,state_name,telephone,salesman_code,salesman_name,bill_to_location,location_desc,item_code,item_desc,itf_code,invoiceqty,shippedqty,unit_code,rate,amount,disc_per,disc_amt,amt_less_discount,total_tax_amt,totalamt,currency_code,convrate,total_basecurr from (" + str + ") as ada"

            Dim categorydesc As String = "select ada1.*,(select distinct (select distinct ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=ada1.Item_Code " + whrcate + "  for xml path(''))) as Category from (" + qry + ")ada1"

            If rdbSummary.IsChecked Then
                qry = "select distinct Type, Document_Code,Document_Date,MAX(aa.Parent_Customer_No) as Parent_Customer_No, MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,aa.Customer_Name,address,city_name,state_name,telephone,aa.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc,sum(Amount) as Amount, " & _
                "sum(Disc_Amt) as Disc_Amt,sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as Total_Tax_Amt,sum(TotalAmt) as TotalAmt,SUM(Total_BaseCurr) as Total_BaseCurr  " & _
                "from ( " & qry & ") aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No group by Type, Document_Code,Document_Date,Customer_Code,aa.Customer_Name,address,city_name,state_name,telephone,aa.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc  "
            ElseIf rdbDetail.IsChecked = True Then
                qry = "select distinct Type, Fdate,Tdate,Document_Code,Document_Date,MAX(aa.Parent_Customer_No) as Parent_Customer_No, MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,aa.Customer_Name,address,city_name,state_name,telephone,aa.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc,Item_Code,Item_Desc,cast('ITF code-'+ max(itf_code) as varchar)as Itf_code,sum(InvoiceQty) as InvoiceQty,sum(ShippedQty) as ShippedQty,Unit_Code,Rate,sum(Amount) as Amount, " & _
                "Disc_Per,sum(Disc_Amt) as Disc_Amt,sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as Total_Tax_Amt,sum(TotalAmt) as TotalAmt,aa.Currency_Code,ConvRate,sum(Total_BaseCurr) as Total_BaseCurr,Category  " & _
                "from ( " & categorydesc & ") aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No group by category,Type, Fdate,Tdate,Document_Code,Document_Date,Customer_Code,aa.Customer_Name,address,city_name,state_name,telephone,aa.Salesman_Code,Salesman_Name,Bill_To_Location ,Location_Desc,Item_Code,Item_Desc,unit_code,rate,disc_per,aa.Currency_Code,ConvRate"
            End If

            If chkSerializeInv.Checked Then
                qry = "select final.Type, final.Fdate, final.Tdate, final.Document_Code, final.Document_Date,final.Customer_Code,final.Customer_Name,final.Address, final.city_name, final.STATE_NAME,final.telephone, final.Salesman_Code, final.Salesman_Name,final.Bill_To_Location,final.Location_Desc,TSPL_SERIAL_ITEM.Auto_Sr_No [Item Serial No],final.Item_Code, final.Item_Desc, final.Unit_code,final.TotalAmt from ( " + qry + ") as final left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = final.Document_Code left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code= final.Item_Code and TSPL_SERIAL_ITEM.Document_Code= TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No "

            End If
            qry += " order by Document_Code "

            atchqry = qry '---------------for mail atachmnt


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            If chkSerializeInv.Checked Then
                Dim lstItems As New List(Of String)
                For Each dr As DataRow In dt.Rows
                    If lstItems.Contains(dr("Item_Code").ToString()) Then
                        dr("TotalAmt") = 0.0
                    Else
                        lstItems.Add(dr("Item_Code").ToString())
                    End If
                Next
            End If

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("UOM : " + txtUOM.Value)


            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        rbtnItemAll.IsChecked = True
        rbtnCustomerAll.IsChecked = True
        txtUOM.Value = ""
        rbtnLocationAll.IsChecked = True
        rdbSummary.IsChecked = True
        chkRetail.IsChecked = False
        chkTax.IsChecked = False
        'LoadCategory()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnPosted.IsChecked = True
    End Sub

    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked = True Then

            Gv1.Columns("Type").IsVisible = True
            Gv1.Columns("Type").Width = 150
            Gv1.Columns("Type").HeaderText = "Type"

            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 150
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 150
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("Parent_Customer_No").IsVisible = True
            Gv1.Columns("Parent_Customer_No").Width = 150
            Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 200
            Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 200
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("address").IsVisible = True
            Gv1.Columns("address").Width = 100
            Gv1.Columns("address").HeaderText = "Address"

            Gv1.Columns("city_name").IsVisible = True
            Gv1.Columns("city_name").Width = 80
            Gv1.Columns("city_name").HeaderText = "City"

            Gv1.Columns("state_name").IsVisible = True
            Gv1.Columns("state_name").Width = 80
            Gv1.Columns("state_name").HeaderText = "State"

            Gv1.Columns("telephone").IsVisible = True
            Gv1.Columns("telephone").Width = 80
            Gv1.Columns("telephone").HeaderText = "Telephone Number"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Name").IsVisible = True
            Gv1.Columns("Salesman_Name").Width = 120
            Gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            Gv1.Columns("Bill_To_Location").IsVisible = True
            Gv1.Columns("Bill_To_Location").Width = 100
            Gv1.Columns("Bill_To_Location").HeaderText = "Location Code"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 120
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Item_Code").IsVisible = True
            Gv1.Columns("Item_Code").Width = 80
            Gv1.Columns("Item_Code").HeaderText = "Item Code"

            Gv1.Columns("Item_Desc").IsVisible = True
            Gv1.Columns("Item_Desc").Width = 80
            Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            Gv1.Columns("Unit_code").IsVisible = True
            Gv1.Columns("Unit_code").Width = 80
            Gv1.Columns("Unit_code").HeaderText = "Unit_code"

            Gv1.Columns("TotalAmt").IsVisible = True
            Gv1.Columns("TotalAmt").Width = 80
            Gv1.Columns("TotalAmt").HeaderText = "Total Amt"

            If chkSerializeInv.Checked Then
                Gv1.Columns("Item Serial No").IsVisible = True
                Gv1.Columns("Item Serial No").Width = 80
            Else
                Gv1.Columns("itf_code").IsVisible = True
                Gv1.Columns("itf_code").Width = 80
                Gv1.Columns("itf_code").HeaderText = "ITF Code"

                Gv1.Columns("InvoiceQty").IsVisible = True
                Gv1.Columns("InvoiceQty").Width = 80
                Gv1.Columns("InvoiceQty").HeaderText = "Qty"

                If clsCommon.myLen(txtUOM.Value) > 0 Then
                    Gv1.Columns("rate").IsVisible = False
                Else
                    Gv1.Columns("rate").IsVisible = True
                End If
                Gv1.Columns("rate").Width = 80
                Gv1.Columns("rate").HeaderText = "Item Rate"

                Gv1.Columns("Amount").IsVisible = True
                Gv1.Columns("Amount").Width = 80
                Gv1.Columns("Amount").HeaderText = "Amount"

                Gv1.Columns("Disc_Per").IsVisible = True
                Gv1.Columns("Disc_Per").Width = 50
                Gv1.Columns("Disc_Per").HeaderText = "Disc %"

                Gv1.Columns("Disc_Amt").IsVisible = True
                Gv1.Columns("Disc_Amt").Width = 80
                Gv1.Columns("Disc_Amt").HeaderText = "Disc Amt"

                Gv1.Columns("Amt_Less_Discount").IsVisible = True
                Gv1.Columns("Amt_Less_Discount").Width = 80
                Gv1.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"

                Gv1.Columns("Total_Tax_Amt").IsVisible = True
                Gv1.Columns("Total_Tax_Amt").Width = 80
                Gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"


                '' currency details
                If objCommonVar.IsDemoERP Then
                    Gv1.Columns("Currency_Code").IsVisible = True
                    Gv1.Columns("Currency_Code").Width = 80
                    Gv1.Columns("Currency_Code").HeaderText = "Currency Code"

                    Gv1.Columns("ConvRate").IsVisible = True
                    Gv1.Columns("ConvRate").Width = 80
                    Gv1.Columns("ConvRate").HeaderText = "Conversion Rate"

                    Gv1.Columns("Total_BaseCurr").IsVisible = True
                    Gv1.Columns("Total_BaseCurr").Width = 100
                    Gv1.Columns("Total_BaseCurr").HeaderText = "Total In Base Currency"

                End If

                Try
                    Gv1.Columns("Category").IsVisible = True
                    Gv1.Columns("Category").Width = 220
                    Gv1.Columns("Category").HeaderText = "Item Category"
                Catch ex As Exception
                End Try
            End If
        ElseIf rdbSummary.IsChecked Then

            Gv1.Columns("Type").IsVisible = True
            Gv1.Columns("Type").Width = 150
            Gv1.Columns("Type").HeaderText = "Type"

            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 150
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 150
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("Parent_Customer_No").IsVisible = True
            Gv1.Columns("Parent_Customer_No").Width = 150
            Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 200
            Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"

            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 200
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("address").IsVisible = True
            Gv1.Columns("address").Width = 100
            Gv1.Columns("address").HeaderText = "Address"

            Gv1.Columns("city_name").IsVisible = True
            Gv1.Columns("city_name").Width = 80
            Gv1.Columns("city_name").HeaderText = "City"

            Gv1.Columns("state_name").IsVisible = True
            Gv1.Columns("state_name").Width = 80
            Gv1.Columns("state_name").HeaderText = "State"

            Gv1.Columns("telephone").IsVisible = True
            Gv1.Columns("telephone").Width = 80
            Gv1.Columns("telephone").HeaderText = "Telephone Number"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Name").IsVisible = True
            Gv1.Columns("Salesman_Name").Width = 120
            Gv1.Columns("Salesman_Name").HeaderText = "Salesman Name"

            Gv1.Columns("Bill_To_Location").IsVisible = True
            Gv1.Columns("Bill_To_Location").Width = 100
            Gv1.Columns("Bill_To_Location").HeaderText = "Location Code"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 120
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Amount").IsVisible = True
            Gv1.Columns("Amount").Width = 80
            Gv1.Columns("Amount").HeaderText = "Amount"

            'Gv1.Columns("Disc_Per").IsVisible = True
            'Gv1.Columns("Disc_Per").Width = 50
            'Gv1.Columns("Disc_Per").HeaderText = "Disc %"

            Gv1.Columns("Disc_Amt").IsVisible = True
            Gv1.Columns("Disc_Amt").Width = 80
            Gv1.Columns("Disc_Amt").HeaderText = "Disc Amt"

            Gv1.Columns("Amt_Less_Discount").IsVisible = True
            Gv1.Columns("Amt_Less_Discount").Width = 80
            Gv1.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"

            Gv1.Columns("Total_Tax_Amt").IsVisible = True
            Gv1.Columns("Total_Tax_Amt").Width = 80
            Gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"

            Gv1.Columns("TotalAmt").IsVisible = True
            Gv1.Columns("TotalAmt").Width = 80
            Gv1.Columns("TotalAmt").HeaderText = "Total Amt"
            If objCommonVar.IsDemoERP Then
                Gv1.Columns("Total_BaseCurr").IsVisible = True
                Gv1.Columns("Total_BaseCurr").Width = 100
                Gv1.Columns("Total_BaseCurr").HeaderText = "Total In Base Currency"
            End If

            Try
                Gv1.Columns("Category").IsVisible = True
                Gv1.Columns("Category").Width = 220
                Gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        If Not chkSerializeInv.Checked Then
            Dim item5 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Amt_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)


            If Not rdbSummary.IsChecked AndAlso clsCommon.myLen(txtUOM.Value) > 0 Then
                Dim item8 As New GridViewSummaryItem("InvoiceQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
            End If



            Dim item9 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)

            Dim item10 As New GridViewSummaryItem("Total_BaseCurr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)

            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Code as Item format ""{0}: {1}"" Group By Item_Code"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Desc as Item format ""{0}: {1}"" Group By Item_Desc"))
            'gv1.ShowGroupPanel = False
            'gv1.MasterTemplate.AutoExpandGroups = True
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
    End Sub

    Private Sub rdbDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbDetail.Click

    End Sub

    Private Sub rdbSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSummary.Click

    End Sub


    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged, rbtnCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged, rbtnItemSelect.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelect.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub


    Private Sub Gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gv1.CellDoubleClick
        If rdbSummary.IsChecked Then
            If Gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Type").Value)
                Dim strDoc = Gv1.CurrentRow.Cells("Document_Code").Value
                If strTransType = "Sale Invoice" Then
                    strTransType = "SD-IN"
                ElseIf clsCommon.CompairString(strTransType, "MCC Material Sale") = CompairStringResult.Equal Then
                    strTransType = "M-Material"
                Else
                    strTransType = "Sale Return"
                End If
                Select Case strTransType
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                    Case "M-Material"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strDoc)
                    Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
                End Select
            End If
        End If
    End Sub

    Private Sub btnmailform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmailform.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmSaleRegisterDemo
        frm.ShowDialog()
    End Sub

    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        'Try
        '    Dim repotype As String = ""
        '    Dim invtype As String = ""
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
        '        Return
        '    End If

        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmSaleRegisterDemo)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Try

        '        Dim strEmail As String = ""


        '        If Process.GetProcessesByName("OutLook").Length < 1 Then
        '            'restarts the Process
        '            Process.Start("OutLook.exe")
        '        End If
        '        Dim oApp As New Outlook.Application()
        '        Dim oMsg As Outlook.MailItem

        '        If chkAll.IsChecked Then
        '            invtype = ""
        '        ElseIf chkTax.IsChecked Then
        '            invtype = "Tax Invoice"
        '        ElseIf chkRetail.IsChecked Then
        '            invtype = "Retail Invoice"
        '        End If

        '        If rdbDetail.IsChecked Then
        '            repotype = "Detail Report"
        '        Else
        '            repotype = "Summary Report"
        '        End If

        '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

        '        Try
        '            If strEmail.Substring(0, 1) = "," Then
        '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        If clsCommon.myLen(strEmail) <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
        '            Return
        '        End If

        '        oMsg.Body = obj.mailbody

        '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        oMsg.Subject = obj.mailsubjct

        '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If

        '        '------------------------code for attchament-------------------------------------
        '        If obj.atchmnt = "Y" Then
        '            Dim sDisplayname As [String] = "MyAttachment"
        '            If oMsg.Body Is Nothing Then
        '                oMsg.Body = " "
        '            End If
        '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
        '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

        '            Dim strRptPath As String = ""

        '            Dim oAttachment As Outlook.Attachment = Nothing
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '            If dt.Rows.Count > 0 Then
        '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

        '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

        '                If (IsExists = False) Then

        '                    System.IO.Directory.CreateDirectory(subPath)
        '                End If
        '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
        '                transportSql.exportdata(Gv1, strRptPath, "Sheet1", False, Nothing, False, False, False, True)
        '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
        '            End If
        '        End If
        '        '---------------------------------------------------------------------------


        '        oMsg.Recipients.Add(strEmail)
        '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
        '        oMsg.Send()
        '        oMsg = Nothing
        '        oApp = Nothing

        '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try

        '    Try
        '        Dim client As New System.Net.WebClient()

        '        If clsCommon.myLen(obj.smsbody) <= 0 Then
        '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
        '        End If

        '        Dim strMes As String = ""

        '        strMes = obj.smsbody
        '        strMes = strMes.Replace("'", " ").Replace("`", "/")

        '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

        '        Try
        '            If strphone.Substring(0, 1) = "," Then
        '                strphone = strphone.Substring(1, strphone.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '        Dim data As Stream = client.OpenRead(baseurl)
        '        Dim reader As StreamReader = New StreamReader(data)
        '        Dim s As String = reader.ReadToEnd()
        '        data.Close()
        '        reader.Close()

        '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub
End Class
