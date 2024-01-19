Imports common
Imports System.IO

'Created By Sanjay, Client - SPMMD
Public Class FrmDairySaleSchemeReport
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim ReportID As String = ""
    Dim dtZone As DataTable
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If txtfDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
            txtfDate.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtUOM.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select UOM first", Me.Text)
            Exit Sub
        End If
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        Dim qry As String = ""
        Dim whrDate As String = " and 2= 2 "
        Dim whr As String = " and 2= 2 "
        'Dim whrDatePrevThreeDays As String = " and 2= 2 "

        If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
            whr += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
        End If


        If rbtn_DateWise.Checked = True Then
            whrDate += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)   "
            'whrDatePrevThreeDays += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value.AddDays(-3) + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  <  convert (date, '" + txtfDate.Value + "',103)   "
        ElseIf rbtn_MonthWise.Checked = True Then
            Dim EndDayOfToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select day( EOMONTH('" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")) + "'))"))
            whrDate += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "yyyy")) + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + EndDayOfToDate + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "yyyy")) + "',103)   "
            'whrDatePrevThreeDays += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =DATEADD(dd,-3,convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "yyyy")) + "',103))  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  <  convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "yyyy")) + "',103)   "
        ElseIf rbtn_YearWise.Checked = True Then
            whrDate += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-Jan-" + txtfDate.Text + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '31-Dec-" + txtToDate.Text + "',103)   "
            'whrDatePrevThreeDays += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =DATEADD(dd,-3,convert (date, '01-Jan-" + txtfDate.Text + "',103))  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  <  convert (date, '01-Jan-" + txtfDate.Text + "',103)   "
        End If

        ' qry = "select TSPL_CUSTOMER_MASTER.cust_code,TSPL_CUSTOMER_MASTER.customer_name
        ' ,TSPL_SCHEME_MASTER_new.MaxlimitStart_Date as start_date,TSPL_SCHEME_MASTER_new.MaxlimitEnd_Date as end_date
        ' ,TSPL_SCHEME_MASTER_NEW.scheme_code,TSPL_SCHEME_MASTER_NEW.scheme_desc,scheme.item_code
        ' ,scheme.zone_code,convert (decimal(18,2) ,lastthreeday.qty/3) as lastthreedayqty,scheme.qty,scheme.Amount,isnull(sale.qty,0) as saleQty,(isnull(sale.qty,0)-
        ' isnull(convert (decimal(18,2) ,lastthreeday.qty/3),0)) as [INC/DEC] from
        ' (select final.Zone_Code ,final.item_code,final.Customer_Code,final.Scheme_code,sum(isnull(Final.Final_Qty,0)) as Qty
        ' ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amount from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
        ' ,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        ' , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        ' , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        ' ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_code
        ' from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        ' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        ' left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        ' left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        ' left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        ' left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '  Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        '  and 2= 2  and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'Y' and TSPL_SD_SALE_INVOICE_DETAIL.scheme_type in ('Fixed','Quantitive'" + IIf(chkIncVolScheme.Checked = True, ",'VolumeSlab'", "") + ")
        '" + whr + " " + whrDate + " 
        ' ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code,final.Scheme_code) as scheme
        ' left outer join
        ' (select final.Zone_Code ,final.item_code,final.Customer_Code
        ' ,sum(isnull(Final.Final_Qty,0)) as Qty,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amount
        '  from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        ' , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        ' , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        ' from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        ' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        ' left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        ' left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        ' left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        ' left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '  Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        '  and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N'
        ' " + whr + " " + whrDate + "     
        ' ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code
        ' )  as sale on sale.customer_code=scheme.customer_code and  sale.item_code=scheme.item_code and sale.zone_code=scheme.zone_code
        ' left outer join
        ' (select final.Zone_Code ,final.item_code,final.Customer_Code,final.Scheme_code
        ' ,sum(isnull(Final.Final_Qty,0)) as Qty,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amount
        ' from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
        ' ,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        ' , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        ' , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        ' ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_code
        '  from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        ' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        ' left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        ' left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        ' left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        ' left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '  Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        ' and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N' and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0
        ' " + whr + " " + whrDatePrevThreeDays + "        
        ' ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code,final.Scheme_code
        ' ) as lastthreeday  on lastthreeday.customer_code=scheme.customer_code
        ' and  lastthreeday.item_code=scheme.item_code and lastthreeday.zone_code=scheme.zone_code
        ' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = scheme.Customer_Code 
        ' left outer join TSPL_SCHEME_MASTER_new on TSPL_SCHEME_MASTER_NEW.Scheme_Code=scheme.Scheme_Code
        ' "

        ' qry = "select max(final.customer_name) as [Customer],final.zone_code as [Zone],final.Scheme_Code as [Scheme Code],max(final.Scheme_desc) as [Scheme Detail]
        '       ,convert(varchar,max(final.start_date),113) as [Start Date],convert(varchar,max(final.end_date),113) as [End Date],sum(lastthreedayqty) as [Before Avg]
        '       ,sum(qty) as [Scheme Qty],sum(Amount) as [Scheme Value],sum(saleQty) as [Sale Qty],sum([INC/DEC]) as [Inc/Dec] from ( " + qry + " ) final where 1=1 group by final.Cust_Code,final.zone_code,final.Scheme_Code"

        'Before Avg (3 days before Scheme start date)'''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'qry = "select TSPL_CUSTOMER_MASTER.cust_code,TSPL_CUSTOMER_MASTER.customer_name
        '         ,TSPL_SCHEME_MASTER_new.MaxlimitStart_Date as start_date,TSPL_SCHEME_MASTER_new.MaxlimitEnd_Date as end_date
        '         ,TSPL_SCHEME_MASTER_NEW.scheme_code,TSPL_SCHEME_MASTER_NEW.scheme_desc,scheme.item_code
        '         ,scheme.zone_code,scheme.qty,scheme.Amount,isnull(sale.qty,0) as saleQty from
        '         (select final.Zone_Code ,final.item_code,final.Customer_Code,final.Scheme_code,sum(isnull(Final.Final_Qty,0)) as Qty
        '         ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amount from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
        '         ,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        '         , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        '         , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        '         ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_code
        '         from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        '         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        '         left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        '         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        '         left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        '         left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '          Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        '          and 2= 2  and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'Y' and TSPL_SD_SALE_INVOICE_DETAIL.scheme_type in ('Fixed','Quantitive'" + IIf(chkIncVolScheme.Checked = True, ",'VolumeSlab'", "") + ")
        '        " + whr + " " + whrDate + " 
        '         ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code,final.Scheme_code) as scheme
        '         left outer join
        '         (select final.Zone_Code ,final.item_code,final.Customer_Code
        '         ,sum(isnull(Final.Final_Qty,0)) as Qty,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amount
        '          from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        '         , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        '         , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        '         from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        '         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        '         left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        '         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        '         left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        '         left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '          Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        '          and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N'
        '         " + whr + " " + whrDate + "     
        '         ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code
        '         )  as sale on sale.customer_code=scheme.customer_code and  sale.item_code=scheme.item_code and sale.zone_code=scheme.zone_code
        '         left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = scheme.Customer_Code 
        '         left outer join TSPL_SCHEME_MASTER_new on TSPL_SCHEME_MASTER_NEW.Scheme_Code=scheme.Scheme_Code
        '         "

        'qry = "select xx.Customer,xx.[Zone],xx.[Scheme Code],xx.[Scheme Detail],xx.[Start Date],xx.[End Date]
        ',convert (decimal(18,2) ,xx.lastthreedayqty/3)  as [Before Avg]
        ',xx.[Scheme Qty],xx.[Scheme Value],xx.[Sale Qty]
        ',xx.[Sale Qty]-convert (decimal(18,2) ,xx.lastthreedayqty/3)  as [INC/DEC]
        'from (select max(final.customer_name) as [Customer],final.zone_code as [Zone],final.Scheme_Code as [Scheme Code],max(final.Scheme_desc) as [Scheme Detail]
        '               ,convert(varchar,max(final.start_date),113) as [Start Date],convert(varchar,max(final.end_date),113) as [End Date]
        '               ,sum(qty) as [Scheme Qty],sum(Amount) as [Scheme Value],sum(saleQty) as [Sale Qty] 

        ' ,(select sum(isnull(Final.Final_Qty,0)) as Qty
        '        from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
        '        ,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
        '        , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
        '        , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
        '        ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_code
        '         from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
        '        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
        '        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
        '        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
        '        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        '        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
        '         Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
        '        and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N' and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0
        '         " + whr + "  
        '		  and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, dateadd(day,-3,max(final.start_date)),103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  <  convert (date, max(final.start_date),103) 
        ' and     TSPL_CUSTOMER_MASTER.Zone_Code=final.zone_code
        '		 and      TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=final.cust_code 
        '        ) final group by final.Zone_Code,final.Customer_Code,final.Scheme_code
        '        ) as lastthreedayQty


        'from ( " + qry + " ) final where 1=1 group by final.Cust_Code,final.zone_code,final.Scheme_Code) xx"

        'Final Query
        qry = "select yy.Customer,yy.[Zone],yy.[Scheme Code],yy.[Scheme Detail],yy.[Start Date],yy.[End Date]
                ,convert (decimal(18,2) ,yy.lastthreedayqty/3)  as [Before Avg]
                ,yy.[Scheme Qty],yy.[Scheme Value],yy.[Sale Qty]
                ,yy.[Sale Qty]-convert (decimal(18,2) ,yy.lastthreedayqty/3)  as [INC/DEC]
                from (
                select cust_code,max(xx.customer_name) as [Customer],max(xx.zone_code) as [Zone],xx.Scheme_Code as [Scheme Code],max(xx.Scheme_desc) as [Scheme Detail]
                ,convert(varchar,max(xx.start_date),113) as [Start Date],convert(varchar,max(xx.end_date),113) as [End Date]
                ,sum(isnull(SchemeQty,0)) as [Scheme Qty],sum(isnull(SchemeAmount,0)) as [Scheme Value],sum(isnull(saleQty,0)) as [Sale Qty] 
                ,(select sum(isnull(Final.Final_Qty,0)) as Qty
                from (select convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
                left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
                and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N' and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 
                " + whr + "
                 and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, dateadd(day,-3,max(xx.start_date)),103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  <  convert (date, max(xx.start_date),103) 
                 and  TSPL_CUSTOMER_MASTER.Zone_Code=xx.zone_code and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=xx.cust_code 
                ) final  ) 	as lastthreedayQty
                from (
                select final.Zone_code,TSPL_CUSTOMER_MASTER.cust_code,TSPL_CUSTOMER_MASTER.customer_name
                ,TSPL_SCHEME_MASTER_new.MaxlimitStart_Date as start_date,TSPL_SCHEME_MASTER_new.MaxlimitEnd_Date as end_date,TSPL_SCHEME_MASTER_NEW.scheme_code,TSPL_SCHEME_MASTER_NEW.scheme_desc
                ,SchemeQty,SchemeAmount,saleQty
                from
                ( select scheme.customer_code,scheme.scheme_code as scheme_code,scheme.zone_code,sum(scheme.SchemeQty) as SchemeQty,sum(scheme.SchemeAmount) as SchemeAmount
                ,max(isnull(sale.saleQty,0)) as saleQty
                 from(select final.Zone_Code ,final.Customer_Code,final.Scheme_code,sum(isnull(Final.Final_Qty,0)) as SchemeQty
                 ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as SchemeAmount ,0 as SaleQty
                 from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                ,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					
                ,convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
                ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_code
                from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
                left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
                 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'Y' and TSPL_SD_SALE_INVOICE_DETAIL.scheme_type in ('Fixed','Quantitive'" + IIf(chkIncVolScheme.Checked = True, ",'VolumeSlab'", "") + ")
                 " + whr + " " + whrDate + "   
                ) final group by final.Zone_Code,final.Item_Code,final.Customer_Code,final.Scheme_code)scheme
                left outer join
                (select final.Zone_Code ,final.Customer_Code,'' as Scheme_code,0 as SchemeQty ,0 as SchemeAmount,sum(isnull(Final.Final_Qty,0)) as SaleQty
                from (select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
                , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc					   
                 , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty
                 from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
                 left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                 left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code =  '" + txtUOM.Value + "'  ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                 left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                 Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  
                 and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N'   
                 " + whr + " " + whrDate + "    
                 ) final group by final.Zone_Code ,final.Customer_Code )sale on sale.customer_code=scheme.customer_code and sale.zone_code=scheme.zone_code group by 
                 scheme.customer_code,scheme.scheme_code,scheme.zone_code
                  ) final 
                   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = final.Customer_Code 
                left outer join TSPL_SCHEME_MASTER_new on TSPL_SCHEME_MASTER_NEW.Scheme_Code=final.Scheme_Code
                 where 1=1 
                 ) xx group by zone_code,Scheme_Code,cust_code ) yy   order by customer"


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

            SetGridFormationOFGV1()
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = False
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
        End If
        ReStoreGridLayout()
    End Sub
    Sub SetGridFormationOFGV1()
        Try
            gvData.TableElement.TableHeaderHeight = 40
            gvData.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 6 To gvData.Columns.Count - 1
                Dim aa = gvData.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvData.ShowGroupPanel = False
            gvData.MasterTemplate.AutoExpandGroups = True
            gvData.MasterTemplate.ShowTotals = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtUOM.Value = ""
        txtZone.arrValueMember = Nothing
        txtZone.arrDispalyMember = Nothing
        LoadDocumentType()
        gvData.ViewDefinition = New TableViewDefinition
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub FrmPendingBookingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmPendingBookingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ReportID = MyBase.Form_ID
    End Sub
    Private Sub rmexcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(Exporter.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDairySaleSchemeReport & "'"))

            If rbtn_DateWise.Checked = True Then
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & IIf(chkDetailReport.Checked = True, "Detail Report", "Day wise"))
            ElseIf rbtn_MonthWise.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(txtfDate.Value, "MMM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "MMM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Monthly")
            ElseIf rbtn_YearWise.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(txtfDate.Value, "yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Yearly")
            End If

            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                'If chkDetailReport.Checked = True Then
                '    transportSql.exportdata(gvData, "", Me.Text, , arrHeader, False, False, True)
                'Else
                transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(Me.Text, gvData, arrHeader, Me.Text)
                'End If
            Else
                transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub LoadDocumentType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Both")
        dt.Rows.Add("Taxable")
        dt.Rows.Add("Non Taxable")

        ddlDocType.DataSource = dt
        ddlDocType.ValueMember = "Code"
        ddlDocType.DisplayMember = "Code"
    End Sub

    Private Sub TxtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMSKU", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Private Sub rbtn_YearWise_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_YearWise.CheckedChanged
        Try
            If rbtn_YearWise.Checked = True Then
                txtfDate.Format = DateTimePickerFormat.Custom
                txtfDate.CustomFormat = "yyyy"
                txtfDate.ShowUpDown = True
                txtToDate.Format = DateTimePickerFormat.Custom
                txtToDate.CustomFormat = "yyyy"
                txtToDate.ShowUpDown = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rbtn_MonthWise_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_MonthWise.CheckedChanged
        Try
            If rbtn_MonthWise.Checked = True Then
                txtfDate.Format = DateTimePickerFormat.Custom
                txtfDate.CustomFormat = "MMM/yyyy"
                txtfDate.ShowUpDown = True
                txtToDate.Format = DateTimePickerFormat.Custom
                txtToDate.CustomFormat = "MMM/yyyy"
                txtToDate.ShowUpDown = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rbtn_DateWise_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_DateWise.CheckedChanged
        Try
            If rbtn_DateWise.Checked = True Then
                txtfDate.Format = DateTimePickerFormat.Custom
                txtfDate.CustomFormat = "dd/MM/yyyy"
                txtToDate.Format = DateTimePickerFormat.Custom
                txtToDate.CustomFormat = "dd/MM/yyyy"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select Zone_Code  as Code, Description as Name from TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneCode@sku", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub ChkDetailReport_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkDetailReport.ToggleStateChanged
        Try
            If chkDetailReport.Checked = True Then
                RadGroupBox2.Enabled = False
                gvData.ViewDefinition = New TableViewDefinition
                gvData.DataSource = Nothing
                rbtn_DateWise.Checked = True
            Else
                RadGroupBox2.Enabled = True
                gvData.ViewDefinition = New TableViewDefinition
                gvData.DataSource = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvData.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvData.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

End Class
