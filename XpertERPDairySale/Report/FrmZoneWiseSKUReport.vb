Imports common
Imports System.IO

'Created By Sanjay, Client - SPMMD
Public Class FrmZoneWiseSKUReport
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
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkDetailReport.Checked = True Then
            VarID += "_DR"
        End If
        If rbtn_DateWise.Checked = True Then
            VarID += "_SU"
        ElseIf rbtn_MonthWise.Checked = True Then
            VarID += "_DE"
        ElseIf rbtn_YearWise.Checked = True Then
            VarID += "_DE"
        End If
        gvData.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportGridID()
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
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        Dim qry As String = ""
        Dim whr As String = " and 2= 2 "
        Dim whrForReturn As String = " and 2= 2 "
        Dim dateFormatewise As String = ""
        Dim dateFormatewiseForReturn As String = ""
        Dim strPivotCol As String = ""
        Dim strPivotColSum As String = ""
        Dim strPivotColTotal As String = ""

        If rdbWithSchemeAndSample.Checked = True Then
            whr += " "
            whrForReturn += " "
        ElseIf rdbOnlySchemeAndSample.Checked = True Then
            whr += " and (isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 1 or TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'Y') "
            whrForReturn += "  and (isnull (TSPL_SD_SALE_RETURN_HEAD.IsSampling,0) = 1 or  TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item  = 'Y') "
        Else
            whr += " and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N'"
            whrForReturn += " and isnull (TSPL_SD_SALE_RETURN_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item  = 'N'"
        End If
        If clsCommon.CompairString(ddlDocType.SelectedValue, "Taxable") = CompairStringResult.Equal Then
            whr += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =1  "
            whrForReturn += " and TSPL_SD_SALE_RETURN_HEAD.Is_Taxable =1  "
        ElseIf clsCommon.CompairString(ddlDocType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
            whr += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =0  "
            whrForReturn += " and TSPL_SD_SALE_RETURN_HEAD.Is_Taxable =0  "
        End If
        If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
            whr += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
            whrForReturn += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
        End If

        If chkDetailReport.Checked = True Then
            Dim QryZone As String = ""
            If rdbSales.Checked = True Then
                QryZone = "SELECT distinct TSPL_ZONE_MASTER.zone_code from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   " + whr + " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)  and TSPL_ZONE_MASTER.zone_code is not null and TSPL_ZONE_MASTER.zone_code<>'' order by zone_code"
            ElseIf rdbSalesReturn.Checked = True Then
                QryZone = "SELECT distinct TSPL_ZONE_MASTER.zone_code from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'   " + whrForReturn + " and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)  and TSPL_ZONE_MASTER.zone_code is not null and TSPL_ZONE_MASTER.zone_code<>'' order by zone_code"
            ElseIf rdbSalesAndSalesReturn.Checked = True Then
                QryZone = "SELECT distinct TSPL_ZONE_MASTER.zone_code from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   " + whr + " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)  and TSPL_ZONE_MASTER.zone_code is not null and TSPL_ZONE_MASTER.zone_code<>''  
                        union SELECT distinct TSPL_ZONE_MASTER.zone_code from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'   " + whrForReturn + " and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)  and TSPL_ZONE_MASTER.zone_code is not null and TSPL_ZONE_MASTER.zone_code<>'' order by zone_code"
            End If

            dtZone = clsDBFuncationality.GetDataTable(QryZone)
            If dtZone Is Nothing OrElse dtZone.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Dim strZone As String = ""
            Dim strZoneZero As String = ""
            Dim strZoneSum As String = ""
            Dim strZoneColSum As String = ""
            Dim strZoneAmt As String = ""
            Dim strZoneAmtZero As String = ""
            Dim strZoneAmtSum As String = ""
            Dim strZoneColAmtSum As String = ""
            'Dim strZoneRate As String = ""
            Dim strFinal As String = ""
            Dim strQryQty As String = ""
            Dim strQryAmt As String = ""
            For i As Integer = 0 To dtZone.Rows.Count - 1
                If i = 0 Then
                    strZone += "[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneZero += "0 as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneSum += "sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) "
                    strZoneColSum += "sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneAmt += "[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    strZoneAmtZero += "0 as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    strZoneAmtSum += "sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A])"
                    strZoneColAmtSum += "sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]) as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    'strZoneRate += " case when [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]>0 then Convert(decimal(18,2),[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]/[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) else 0 end as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_R]"
                    strFinal += "[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]" + "," + " case when [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]>0 then Convert(decimal(18,2),[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]/[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) else 0 end as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_R]" + "," + "[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                Else
                    strZone += ",[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneZero += ",0 as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneSum += "+sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "])"
                    strZoneColSum += ",sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]"
                    strZoneAmt += ",[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    strZoneAmtZero += ",0 as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    strZoneAmtSum += "+sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A])"
                    strZoneColAmtSum += ",sum([" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]) as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                    'strZoneRate += " ,case when [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]>0 then Convert(decimal(18,2),[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]/[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) else 0 end as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_R]"
                    strFinal += ",[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]" + "," + " case when [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]>0 then Convert(decimal(18,2),[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]/[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "]) else 0 end as [" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_R]" + "," + "[" + clsCommon.myCstr(dtZone.Rows(i).Item("Zone_code")) + "_A]"
                End If
            Next

            If rdbSales.Checked = True Then
                strQryQty = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZone + "," + strZoneAmtZero + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(isnull(Final.Final_Qty,0)) as Qty
                        from (select TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
                        , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                        , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " + whr + "  and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)    
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Qty) for Zone_Code  in (" + strZone + ") ) as Pivot1 "

                strQryAmt = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZoneZero + "," + strZoneAmt + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amt
                        from (select TSPL_SD_SALE_INVOICE_DETAIL.Qty
                        ,TSPL_SD_SALE_INVOICE_DETAIL.Amount
                        ,TSPL_CUSTOMER_MASTER.Zone_Code+'_A' as Zone_Code
                        , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                         , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'    " + whr + "   and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)    
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Amt) for Zone_Code  in (" + strZoneAmt + ") ) as Pivot1 "

            ElseIf rdbSalesReturn.Checked = True Then
                strQryQty = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZone + "," + strZoneAmtZero + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(isnull(Final.Final_Qty,0)) as Qty
                        from (select (-1)*TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty,(-1)*TSPL_SD_SALE_RETURN_DETAIL.Amount as Amount,TSPL_CUSTOMER_MASTER.Zone_Code
                        , TSPL_SD_SALE_RETURN_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                        ,(-1)* (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_RETURN_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' " + whrForReturn + "  and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)    
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Qty) for Zone_Code  in (" + strZone + ") ) as Pivot1 "

                strQryAmt = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZoneZero + "," + strZoneAmt + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amt
                        from (select (-1)*TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty
                        ,(-1)*TSPL_SD_SALE_RETURN_DETAIL.Amount as Amount
                        ,TSPL_CUSTOMER_MASTER.Zone_Code+'_A' as Zone_Code
                        , TSPL_SD_SALE_RETURN_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                         ,(-1)* (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_RETURN_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'    " + whrForReturn + "   and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)    
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Amt) for Zone_Code  in (" + strZoneAmt + ") ) as Pivot1 "

            ElseIf rdbSalesAndSalesReturn.Checked = True Then
                strQryQty = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZone + "," + strZoneAmtZero + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(isnull(Final.Final_Qty,0)) as Qty
                        from (select TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Zone_Code
                        , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                        , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' " + whr + "  and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)   
                         union all 
                         select (-1)*TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty,(-1)*TSPL_SD_SALE_RETURN_DETAIL.Amount as Amount,TSPL_CUSTOMER_MASTER.Zone_Code
                        , TSPL_SD_SALE_RETURN_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                        ,(-1)* (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_RETURN_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' " + whrForReturn + "  and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103) 
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Qty) for Zone_Code  in (" + strZone + ") ) as Pivot1 "

                strQryAmt = "select item_code as [Item Code],Item_Desc as [Item Name],Structure_Code as [Milk Group],cheapter_heads as [Product Sub Type], " + strZoneZero + "," + strZoneAmt + " from (select final.Zone_Code ,final.item_code, max(Final.Item_Desc) as Item_Desc , max(Final.Structure_Code) as Structure_Code 
                        ,max(Final.cheapter_heads) as cheapter_heads ,sum(case when Final_Qty<>0 then isnull(Final.Amount,0) else 0 end) as Amt
                        from (select TSPL_SD_SALE_INVOICE_DETAIL.Qty
                        ,TSPL_SD_SALE_INVOICE_DETAIL.Amount
                        ,TSPL_CUSTOMER_MASTER.Zone_Code+'_A' as Zone_Code
                        , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                         , convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_INVOICE_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'    " + whr + "   and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)    
                         union all
                         select (-1)*TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty
                        ,(-1)*TSPL_SD_SALE_RETURN_DETAIL.Amount as Amount
                        ,TSPL_CUSTOMER_MASTER.Zone_Code+'_A' as Zone_Code
                        , TSPL_SD_SALE_RETURN_DETAIL.Item_Code , TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.cheapter_heads						   
                         ,(-1)* (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code = TSPL_SD_SALE_RETURN_DETAIL.item_Code
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                         Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'    " + whrForReturn + "   and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  
                         and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)
                        ) final group by final.Zone_Code,final.Item_Code)XFinal 
                        pivot ( sum(XFinal.Amt) for Zone_Code  in (" + strZoneAmt + ") ) as Pivot1 "

            End If

            qry = " Select [Item Code],max([Item Name]) as [Item Name],[Milk Group],[Product Sub Type], " + strZoneColSum + " , " + strZoneColAmtSum + ",(" + strZoneSum + ") as [Total Qty] , (" + strZoneAmtSum + ") as [Total Amt] from (  " &
             "  " + strQryQty + "  " &
             " Union All " &
             "  " + strQryAmt + " " &
             " ) xyzp  group by xyzp.[Item Code],xyzp.[Milk Group],xyzp.[Product Sub Type]  "

            'qry = "select *," + strZoneRate + ",(case when [Total Qty]>0 then Convert(decimal(18,2),[Total Amt]/[Total Qty]) else 0 end) as [Total Rate] from ( " + qry + " )xx order by [Item Code]"
            qry = "select [Item Code],[Item Name],[Milk Group],[Product Sub Type]," + strFinal + ",[Total Qty],(case when [Total Qty]>0 then Convert(decimal(18,2),[Total Amt]/[Total Qty]) else 0 end) as [Total Rate],[Total Amt] from ( " + qry + " )xx order by [Item Code]"
        Else

            If rbtn_DateWise.Checked = True Then
                strPivotCol = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select  STUFF((Select ',' + QUOTENAME(convert(varchar,dates_cte.Date,103) ) as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value + "',103) order by convert(date,dates_cte.Date,103) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                If clsCommon.myLen(strPivotCol) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                strPivotColSum = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX)  with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select  STUFF((SELECT ',' +'Sum(isnull(' + QUOTENAME(convert(varchar,dates_cte.Date,103)) +',0))' +' as ' + QUOTENAME(convert(varchar,dates_cte.Date,103)) as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value + "',103) order by convert(date,dates_cte.Date,103) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                strPivotColTotal = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select  STUFF((SELECT  '+' +'Sum(isnull(' +  QUOTENAME(convert(varchar,dates_cte.Date,103) )  +',0))'  as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value + "',103) order by convert(date,dates_cte.Date,103) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                dateFormatewise = "  convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  "
                dateFormatewiseForReturn = "  convert (varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  "
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)   "
                whrForReturn += " and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '" + txtfDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + txtToDate.Value + "',103)   "
            ElseIf rbtn_MonthWise.Checked = True Then
                strPivotCol = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value.AddMonths(1) + "',103)) select  STUFF((Select  ',' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) + '/' +convert(varchar,Year(dates_cte.Date))) as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value.AddMonths(1) + "',103) order by  Year(dates_cte.Date),MONTH(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                If clsCommon.myLen(strPivotCol) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                strPivotColSum = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value.AddMonths(1) + "',103)) select STUFF((Select  ',' +'Sum(isnull('  + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) + '/' +convert(varchar,Year(dates_cte.Date))) +',0))' +' as ' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) + '/' +convert(varchar,Year(dates_cte.Date))) as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value.AddMonths(1) + "',103) order by  Year(dates_cte.Date),MONTH(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                strPivotColTotal = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value.AddMonths(1) + "',103)) select  STUFF((SELECT  '+' +'Sum(isnull(' +  QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) + '/' +convert(varchar,Year(dates_cte.Date))) +',0))'  as Alies_Name FROM dates_cte where convert(date,date,103)<=convert(date,'" + txtToDate.Value.AddMonths(1) + "',103) order by  Year(dates_cte.Date),MONTH(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                dateFormatewise = " convert (varchar, DATENAME( month ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)) + '/'+ convert (varchar, Year (TSPL_SD_SALE_INVOICE_HEAD.Document_Date))  "
                dateFormatewiseForReturn = " convert (varchar, DATENAME( month ,TSPL_SD_SALE_RETURN_HEAD.Document_Date)) + '/'+ convert (varchar, Year (TSPL_SD_SALE_RETURN_HEAD.Document_Date))  "
                Dim EndDayOfToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select day( EOMONTH('" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")) + "'))"))
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "yyyy")) + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + EndDayOfToDate + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "yyyy")) + "',103)   "
                whrForReturn += " and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtfDate.Value, "yyyy")) + "',103)  and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '" + EndDayOfToDate + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "yyyy")) + "',103)   "
            ElseIf rbtn_YearWise.Checked = True Then
                strPivotCol = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(year,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select  STUFF((Select  ',' + QUOTENAME(convert(varchar,Year(dates_cte.Date))) as Alies_Name FROM dates_cte where year(date)<=year('" + txtToDate.Value + "') order by  Year(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                If clsCommon.myLen(strPivotCol) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                strPivotColSum = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(year,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select STUFF((Select  ',' +'Sum(isnull('  + QUOTENAME(convert(varchar,Year(dates_cte.Date))) +',0))' +' as ' + QUOTENAME(convert(varchar,Year(dates_cte.Date))) as Alies_Name FROM dates_cte where year(date)<=year('" + txtToDate.Value + "') order by  Year(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                strPivotColTotal = clsDBFuncationality.getSingleValue("Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + txtfDate.Value + "',103) union all select dateadd(year,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + txtToDate.Value + "',103)) select  STUFF((SELECT  '+' +'Sum(isnull(' +  QUOTENAME(convert(varchar,Year(dates_cte.Date))) +',0))'  as Alies_Name FROM dates_cte where year(date)<=year('" + txtToDate.Value + "') order by  Year(dates_cte.Date) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
                dateFormatewise = "  convert (varchar, Year (TSPL_SD_SALE_INVOICE_HEAD.Document_Date))  "
                dateFormatewiseForReturn = "  convert (varchar, Year (TSPL_SD_SALE_RETURN_HEAD.Document_Date))  "
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-Jan-" + txtfDate.Text + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '31-Dec-" + txtToDate.Text + "',103)   "
                whrForReturn += " and convert (date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) > =convert (date, '01-Jan-" + txtfDate.Text + "',103)  and  convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  < = convert (date, '31-Dec-" + txtToDate.Text + "',103)   "
            End If

            If rdbSales.Checked = True Then
                qry = "     select Zone_Code as [Zone Code],max(Zone_Name) as [Zone Name]  ,max(UOM) as UOM, " + strPivotColSum + "," + strPivotColTotal + " as [Total]  from (
                        select final .Document_Date , final.Zone_Code , max(final.Zone_Name) as  Zone_Name ,'" + clsCommon.myCstr(txtUOM.Value) + "' as UOM ,  sum(Final_Qty) as Qty  from (
                        select   TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " + dateFormatewise + " as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1  , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code, convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                        Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   " + whr + " 
                        ) final group by final .Document_Date, final.Zone_Code
                        ) XFinal
                        pivot ( sum(XFinal.Qty) for Document_Date  in (" + strPivotCol + ")  ) as Pivo group by Zone_Code "

            ElseIf rdbSalesReturn.Checked = True Then
                qry = "     select Zone_Code as [Zone Code],max(Zone_Name) as [Zone Name]  ,max(UOM) as UOM, " + strPivotColSum + "," + strPivotColTotal + " as [Total]  from (
                        select final .Document_Date , final.Zone_Code , max(final.Zone_Name) as  Zone_Name ,'" + clsCommon.myCstr(txtUOM.Value) + "' as UOM ,  sum(Final_Qty) as Qty  from (
                        select   TSPL_SD_SALE_RETURN_HEAD.Document_Code, " + dateFormatewiseForReturn + " as Document_Date, TSPL_SD_SALE_RETURN_HEAD.Customer_Code ,(-1) * TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1  , TSPL_SD_SALE_RETURN_DETAIL.Item_Code, TSPL_SD_SALE_RETURN_DETAIL.Unit_code,(-1) * (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                        Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'   " + whrForReturn + " 
                        ) final group by final .Document_Date, final.Zone_Code
                        ) XFinal
                        pivot ( sum(XFinal.Qty) for Document_Date  in (" + strPivotCol + ")  ) as Pivo group by Zone_Code "

            ElseIf rdbSalesAndSalesReturn.Checked = True Then
                qry = "     select Zone_Code as [Zone Code],max(Zone_Name) as [Zone Name]  ,max(UOM) as UOM, " + strPivotColSum + "," + strPivotColTotal + " as [Total]  from (
                        select final .Document_Date , final.Zone_Code , max(final.Zone_Name) as  Zone_Name ,'" + clsCommon.myCstr(txtUOM.Value) + "' as UOM ,  sum(Final_Qty) as Qty  from (
                        select   TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " + dateFormatewise + " as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1  , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code, convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                        Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   " + whr + " 
                        union all select   TSPL_SD_SALE_RETURN_HEAD.Document_Code, " + dateFormatewiseForReturn + " as Document_Date, TSPL_SD_SALE_RETURN_HEAD.Customer_Code ,(-1) * TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1  , TSPL_SD_SALE_RETURN_DETAIL.Item_Code, TSPL_SD_SALE_RETURN_DETAIL.Unit_code,(-1) * (convert (decimal(18,2) , (TSPL_SD_SALE_RETURN_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )) as Final_Qty from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_RETURN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                        Where   TSPL_SD_SALE_RETURN_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS'   " + whrForReturn + " 
                        ) final group by final .Document_Date, final.Zone_Code
                        ) XFinal
                        pivot ( sum(XFinal.Qty) for Document_Date  in (" + strPivotCol + ")  ) as Pivo group by Zone_Code "

            End If


        End If


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

    End Sub
    Sub SetGridFormationOFGV1()
        Try
            gvData.TableElement.TableHeaderHeight = 40
            gvData.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
            Next

            gvData.Columns(0).IsPinned = True
            gvData.Columns(1).IsPinned = True
            gvData.Columns(2).IsPinned = True
            If chkDetailReport.Checked = True Then
                gvData.Columns(3).IsPinned = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 4 To gvData.Columns.Count - 1
                    Dim aa = clsCommon.myCstr(gvData.Columns(i).Name)

                    If clsCommon.CompairString(aa, "Total Rate") = CompairStringResult.Equal Then
                        Dim item1 As New GridViewSummaryItem()
                        item1.FormatString = "{0:F2}"
                        item1.Name = aa
                        item1.AggregateExpression = "IIf(sum([Total Qty])>0,sum([Total Amt])/sum([Total Qty]),0)"
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(aa.Substring(aa.Length - 2, 2), "_R") = CompairStringResult.Equal Then
                        'Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Avg)
                        'summaryRowItem.Add(item1)

                        Dim item1 As New GridViewSummaryItem()
                        item1.FormatString = "{0:F2}"
                        item1.Name = aa
                        'item1.AggregateExpression = "sum([" + aa.Substring(0, aa.Length - 2) + "_A" + "])/sum([" + aa.Substring(0, aa.Length - 2) + "])"
                        item1.AggregateExpression = "IIf(sum([" + aa.Substring(0, aa.Length - 2) + "])>0,  sum([" + aa.Substring(0, aa.Length - 2) + "_A" + "])/sum([" + aa.Substring(0, aa.Length - 2) + "]),0)"
                        summaryRowItem.Add(item1)

                    Else
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    End If
                Next
                gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                'gvData.GroupDescriptors.Add(New GridGroupByExpression("[Product Sub Type] as [Product Sub Type] format ""{0}: {1}"" Group By [Product Sub Type]"))

                For Each dr As DataRow In dtZone.Rows
                    gvData.Columns(dr("Zone_code")).HeaderText = "Ltr/Kg"
                    gvData.Columns(dr("Zone_code") + "_R").HeaderText = "Rate"
                    gvData.Columns(dr("Zone_code") + "_A").HeaderText = "Value"
                Next

                gvData.Columns("Total Qty").HeaderText = "Ltr/Kg"
                gvData.Columns("Total Rate").HeaderText = "Rate"
                gvData.Columns("Total Amt").HeaderText = "Value"
                View()
            Else
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 3 To gvData.Columns.Count - 1
                    Dim aa = gvData.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                Next
                gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvData.Columns("Total").PinPosition = PinnedColumnPosition.Right
            End If
            gvData.ShowGroupPanel = False
            gvData.MasterTemplate.AutoExpandGroups = True
            gvData.MasterTemplate.ShowTotals = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Sub View()
        Try
            If gvData.Rows.Count > 0 Then

                Dim view As New ColumnGroupsViewDefinition()

                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns("Item Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns("Item Name").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns("Milk Group").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns("Product Sub Type").Name)
                Dim TempColGroupCount As Integer = 1
                For Each dr As DataRow In dtZone.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Zone_code"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns(dr("Zone_code")).Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns(dr("Zone_code") + "_R").Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns(dr("Zone_code") + "_A").Name)
                    TempColGroupCount += 1
                Next

                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
                view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns("Total Qty").Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns("Total Rate").Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gvData.Columns("Total Amt").Name)

                gvData.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmZoneWiseSKUReport & "'"))

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
                If chkDetailReport.Checked = True Then
                    '    'Dim spreadExporter As GridViewSpreadExport = New GridViewSpreadExport(gvData)
                    '    'Dim exportRenderer As New SpreadExportRenderer()
                    '    'spreadExporter.RunExport("C:\\ERPTempFolder\\" + "exportedFile.xlsx", exportRenderer)

                    transportSql.exportdata(gvData, "", Me.Text, , arrHeader, False, False, True)
                    'clsCommon.MyExportToExcelGrid(Me.Text, gvData, arrHeader, Me.Text, True)
                    '    'clsCommon.MyExportToExcelGrid(Me.Text, gvData, arrHeader, Me.Text)

                    '    '''''''''''''''
                    'Dim excelExporter As New ExportToExcelML(gvData)

                    ''If Me.radTextBoxSheet.Text <> [String].Empty Then

                    'excelExporter.SheetName = Me.Text
                    ''End If
                    'excelExporter.SummariesExportOption = SummariesOption.ExportAll


                    'excelExporter.ExportVisualSettings = True
                    'excelExporter.ExportHierarchy = True

                    ''excelExporter.RadGridViewToExport = gvData.ViewDefinition
                    'excelExporter.ChildViewExportMode = ChildViewExportMode.ExportFirstView
                    'excelExporter.RunExport("C:\\ERPTempFolder\\" + "exportedFile1.xlsx")

                Else
                    clsCommon.MyExportToExcelGrid(Me.Text, gvData, arrHeader, Me.Text)
                End If
            Else
                clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
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
End Class
