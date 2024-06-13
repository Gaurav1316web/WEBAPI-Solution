'-23/10/2012-Updation By--Pankaj Kumar--Added New Column (MRP) in Report That Exports To Excel-------Fwd By--Ranjana Mam
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'============Rohit,BM00000007769================
'' work done agasist ticket no. UDL/31/05/18-000178 on 31/05/2018
'' work done agaist ticket no. UDL/31/05/18-000178
'Sanjay Ticket No- UDL/15/10/18-000230 ,Consider only Invoice type document for getting Vendor Invoice number.
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports common

' Update BY abhishek as on 29 oct 2012 5:05 pm For Excel
' by vipin for pdf work on 31/01/2013
Public Class FrmPendingSrn_Qty
    Inherits FrmMainTranScreen
    Dim ds As DataSet
    Dim dtCategory As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingSrn_Qty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        btnprint1.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub FrmPendingSrn_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        dtptodate1.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadCategory()
        rbtnCategoryAll.IsChecked = True
        LoadLocation()
        chk_All.IsChecked = True
        chkvendor_All1.IsChecked = True
        chkLocationAll.IsChecked = True
        chkFnshdGoods.IsChecked = True
        chkLocationAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")
        RadPageView1.SelectedPage = RadPageViewPage1

        If objCommonVar.IsDemoERP Then
            grpItemType.Visible = False
            chkOthers.IsChecked = True
        End If
    End Sub
   


    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        reset()
    End Sub
    Sub reset()
        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        dtptodate1.Value = clsCommon.GETSERVERDATE()
        ' LoadDocuemntNo()
        ''LoadVendor()
        ''LoadLocation()
        'ChkRgptype.IsChecked = False
        chk_All.IsChecked = True
        chkvendor_All1.IsChecked = True
        chkLocationAll.IsChecked = True
        chkPaymentStatus.Checked = False
        chkSRNReturn.Checked = False
        'gv.Rows.Clear()
        gv.Columns.Clear()
        'chkFnshdGoods.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    ''This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RQ-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'PrintForExcel()

    End Sub
    Sub PrintData()

        '    If chkDoc_select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
        '        Return
        '    End If
        '    If chkVendor_select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
        '        Return
        '    End If
        '    If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
        '        Return
        '    End If

        '    Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        '    Dim todate1 As String = clsCommon.GetPrintDate(dtptodate1.Value, "dd/MM/yyyy")
        '    'PrintData(fromdate, todate1, chk_doc_select.IsChecked, cbgDoc.CheckedValue, chkVendor_select.IsChecked, cbgVendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

        '    ''---------- Added by abhishek kumar as on 30 june 2012 for Excel Report-----------
        '    PrintForExcel(fromdate, todate1, chk_doc_select.IsChecked, cbgDoc.CheckedValue, chkVendor_select.IsChecked, cbgVendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
        'End Sub

        'Public Sub printdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isLocation As Boolean, ByVal ArrLoc As ArrayList)
        '    Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        '    Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        '    If isDocSelect AndAlso ArrDoc.Count <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select at least one Document")
        '        Return
        '    ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
        '        Return
        '    End If
        '    If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
        '        Return
        '    End If
        '    Dim qry As String = "select code,ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as SRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as InvoiceQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, max(Final.MRP)as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = max(bill_to_address))as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2" & _
        '                        " from ( select TSPL_SRN_DETAIL.SRN_No as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Status as ReqStatus,TSPL_SRN_HEAD .Bill_To_Location as bill_to_address from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1  "
        '    If isDocSelect Then
        '        qry += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        '    End If
        '    If isVendorSelect Then
        '        qry += " and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        '    End If
        '    If isLocation Then
        '        '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        '        qry += " and Tspl_Srn_HEAD.Bill_To_Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

        '    End If
        '    qry += " and convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + ToDate + "',103)"


        '    qry += "union all select TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty" & _
        '             ",0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address " & _
        '             " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 " & _
        '             " union all select TSPL_PI_DETAIL.SRN_ID as Code,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved" & _
        '             ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address" & _
        '             " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('')  )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code group by Code,ICode having SUM(Chk)>0"
        '    If Not rdobtnAll.IsChecked Then


        '        If rdoInvoicenever.IsChecked = True Then
        '            qry += "and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
        '        End If
        '        If rdoinvoicePartial.IsChecked = True Then
        '            'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 )"
        '            qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
        '        End If
        '        If rdobtnCompleted.IsChecked = True Then
        '            qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
        '        End If
        '    End If
        '    qry += "order by Code,ICode"

        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    PurchaseOrderViewer.funreport(dt, "Pending SRN", "Pending SRN Qty")



    End Sub
    ''------- Added By abhishek kumar as on 30 june 2012 for Excel Report
    Public Sub PrintForExcel(ByVal exporter As EnumExportTo, Optional ByVal OnlyGrid As Boolean = False)

        Dim fromdate As String = clsCommon.myCDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtptodate1.Value, "dd/MM/yyyy")
        Dim DocNo As ArrayList = cbgDoc.CheckedValue
        Dim locationArr As ArrayList = cbgLocation.CheckedValue
        Dim VendorArr As ArrayList = cbgVendor1.CheckedValue
        Dim location As String
        Dim Doc As String
        Dim Vendor As String
        Dim status As String = ""
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""

        Dim strCodeColumn As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""

        Dim strCodeColumnSelect As String = ""
        Dim strCodeDescColumnSelect As String = ""

        Dim strCategoryTable As String = ""
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","

                    strCodeColumnSelect += ","
                    strCodeDescColumnSelect += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 " + Environment.NewLine & _
            " )xx" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
            " ) Pivt" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " (" + Environment.NewLine & _
            " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
            " ) Pivt1 " + Environment.NewLine & _
            " ) xxx group by Item_Code "
            ''End of Category Table start now.
        End If
        If chk_doc_select.IsChecked = True AndAlso cbgDoc.CheckedValue.Count > 0 Then
            Doc = "'" + clsCommon.GetMulcallString(DocNo) + "'"
            StrDocNo = Doc.Replace("'", "")
        End If
        If chk_vendor_select.IsChecked = True AndAlso cbgVendor1.CheckedValue.Count > 0 Then
            Vendor = ("'" + clsCommon.GetMulcallString(VendorArr) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If

        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
            Strlocation = location.Replace("'", "")
        End If
        If chkFnshdGoods.IsChecked Then
            status = "Finshied Goods"
        ElseIf chkOthers.IsChecked Then
            status = "Others"
        End If
        Dim qry As String = ""
        ' If (chkSummary.Checked) And rb_srnreturn.IsChecked Then
        If (chkSummary.Checked) And chkSRNReturn.Checked Then
            qry += "select [SRN No],max([SRN Date]) as [SRN Date],max([GRN No]) as [GRN No],max([GRN Date]) as [GRN Date],max(Party) as Party,max([Party Name]) as [Party Name],SUM([Total Amount]) as [Total Amount] ,MAX(store) as store,MAX(status) as status "
            '' Anubhooti 28-Jan-2015 (Added New Column [Pending For Billing] Which Will Show Status Of Billing)
            qry += " ,MAX([Pending For Billing]) As [Pending For Billing],max([SRN Return]) as [SRN Return] from ( "
        ElseIf (chkSummary.Checked) Then
            qry += "select [SRN No],max([SRN Date]) as [SRN Date],max([GRN No]) as [GRN No],max([GRN Date]) as [GRN Date],max(Party) as Party,max([Party Name]) as [Party Name],SUM([Total Amount]) as [Total Amount] ,MAX(store) as store,MAX(status) as status "
            '' Anubhooti 28-Jan-2015 (Added New Column [Pending For Billing] Which Will Show Status Of Billing)
            qry += " ,MAX([Pending For Billing]) As [Pending For Billing],max([Payment Terms]) as [Payment Terms],max([PO Payment Terms]) as [PO Payment Terms],max([Payment Due Date]) as [Payment Due Date],max([Actual Payment Date]) as [Actual Payment Date],max([Over Due Payment(In Days)]) as [Over Due Payment(In Days)] from ( "
        End If
        '==============changes by shivani against ticket no.[BM00000008943]
        '= KUNAL > TICKET : BM00000009576 === [ ADDED [Vendor Invoice No & Vendor Invoice Date ] FROM SRN IF IT IS NOT IN INVOICE TABLE =================
        qry += " select distinct TSPL_SRN_DETAIL.SRN_No as [SRN No],convert(varchar,TSPL_SRN_HEAD .SRN_Date,103) AS [SRN Date],tspl_mrn_head.MRN_No as [MRN No],convert(varchar,tspl_mrn_head.MRN_Date,103) as [MRN Date],TSPL_SRN_HEAD.Against_GRN as [GRN No],convert(varchar,TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date],TSPL_SRN_HEAD.Vendor_Code as Party," & _
                " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No],convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date],TSPL_PI_HEAD.PI_No as [Invoice No], " & _
                " convert(varchar,tspl_pi_head.PI_date,103) as [Invoice Date],TSPL_REQUISITION_HEAD.Requisition_Id as [Indent No],TSPL_REQUISITION_HEAD.Requisition_Date as [Indent Date], case when TSPL_SRN_HEAD.Inv_No is not null then TSPL_SRN_HEAD.Inv_No else TSPL_PI_HEAD.Vendor_Invoice_No end as [Vendor Invoice No] , case when TSPL_SRN_HEAD.Inv_Date is not null then convert(varchar,TSPL_SRN_HEAD.Inv_Date,103) else convert(varchar,TSPL_PI_HEAD.InvoiceDate,103) end as [Vendor Invoice Date] ,(select Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code =TSPL_SRN_HEAD .Vendor_Code  )as [Party Name]," & _
                            " tspl_pi_head.Vendor_Invoice_No as [Bill No],convert(varchar,tspl_pi_head.InvoiceDate ,103)as [Bill Date],TSPL_SRN_DETAIL .Item_Code as Item,TSPL_SRN_DETAIL.Item_Desc as [Name Of Item],TSPL_SRN_DETAIL.Unit_Code as UOM, TSPL_SRN_DETAIL.MRP as MRP, tspl_srn_detail.GRN_Qty  as [Challan Qty],TSPL_SRN_DETAIL .MRN_Qty  as [Recieved Qty]," & _
                            " TSPL_SRN_DETAIL .SRN_Qty + (Isnull(TSPL_SRN_DETAIL.Free_Qty ,0)) as [SRN Qty],TSPL_SRN_DETAIL .Rejected_Qty as [Rejected Qty],TSPL_SRN_DETAIL.Short_Qty as [Short Qty],TSPL_SRN_DETAIL.Item_Cost as Rate,TSPL_SRN_DETAIL.Amount as [MRN Value]," & _
                             " (Case when tax1 .Type ='E'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='E'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='E'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='E'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='E'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='E'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='E'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='E'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='E'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='E'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as Excise," & _
                            "    (Case when tax1 .Type ='V'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='V'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='V'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='V'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='V'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='V'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='V'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='V'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='V'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='V'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as VAT," & _
                            "  (Case when tax1 .Type ='C'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='C'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='C'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='C'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='C'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='C'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='C'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='C'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='C'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='C'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as CST, " & _
                            "  (Case when tax1 .Type ='IGST'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='IGST'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='IGST'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='IGST'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='IGST'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='IGST'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='IGST'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='IGST'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='IGST'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='IGST'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as IGST, " & _
                            "  (Case when tax1 .Type ='CGST'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='CGST'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='CGST'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='CGST'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='CGST'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='CGST'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='CGST'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='CGST'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='CGST'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='CGST'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as CGST, " & _
                            "  (Case when tax1 .Type ='SGST'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='SGST'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='SGST'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='SGST'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='SGST'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='SGST'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='SGST'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='SGST'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='SGST'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='SGST'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as SGST, " & _
                            "  (Case when tax1 .Type ='UGST'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='UGST'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='UGST'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='UGST'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='UGST'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='UGST'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='UGST'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='UGST'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='UGST'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='UGST'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as UGST, " & _
                            "  (Case when tax1 .Type ='M'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='M'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='M'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='M'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='M'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='M'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='M'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='M'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='M'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='M'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as Mandi, " & _
                            " (Case when tax1 .Type ='A'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='A'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='A'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='A'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='A'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='A'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='A'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='A'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='A'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='A'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as ADDTAX," & _
                            "   (Case when tax1 .Type ='O'then TSPL_SRN_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='O'then TSPL_SRN_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='O'then TSPL_SRN_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='O'then TSPL_SRN_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='O'then TSPL_SRN_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='O'then TSPL_SRN_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='O'then TSPL_SRN_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='O'then TSPL_SRN_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='O'then TSPL_SRN_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='O'then TSPL_SRN_DETAIL.TAX10_Amt  else 0 end)as OTHER," & _
                            "  0.0 as OtherExp,TSPL_SRN_DETAIL .Item_Net_Amt as [Total Amount],TSPL_ITEM_category.Category_Name as [Name Of Group],tspl_srn_head.Bill_To_Location as Store  ,case when tspl_srn_head.Status='1' then 'Posted' else'Pending ' end as Status "
        '' Anubhooti 28-Jan-2015 (Added New Column [Pending For Billing] Which Will Show Status Of Billing)
        'qry += " ,CASE WHEN TSPL_SRN_HEAD.SRN_No = (select top 1  TSPL_PI_HEAD.Against_SRN from TSPL_PI_HEAD where TSPL_PI_HEAD.Against_SRN= TSPL_SRN_HEAD.SRN_No) THEN 'No' ELSE 'Yes' END AS 'Pending For Billing' "
        '==shivani(pending bill and srn return)
        qry += " , CASE  when isnull(coalesce(TSPL_SRN_HEAD.Against_RGP,tspl_grn_head.Det_RGP_NO),'')<>'' then '' WHEN TSPL_SRN_DETAIL.SRN_No = (select  top 1 TSPL_PI_DETAIL .SRN_Id  from TSPL_PI_DETAIL left join tspl_pi_head on tspl_pi_head.pi_no=TSPL_PI_DETAIL.pi_no where TSPL_PI_DETAIL.SRN_Id= TSPL_SRN_DETAIL.SRN_No and tspl_pi_head.Status = 1) THEN 'No' ELSE 'Yes' END AS 'Pending For Billing',TSPL_VENDOR_INVOICE_HEAD.Document_No as [Vendor Invoice Doc No]"
        ' If chkSummary.Checked Or rb_paymentstatus.IsChecked = True Then chkPaymentStatus
        If chkSummary.Checked Or chkPaymentStatus.Checked = True Then
            qry += ", isnull(TSPL_PURCHASE_ORDER_HEAD.Terms_Code,'') as [Payment Terms], isnull(TSPL_PURCHASE_ORDER_HEAD.Payment_Terms,'') as [PO Payment Terms], isnull(convert(varchar,DATEADD(DAY,isnull((select no_days from tspl_terms_master where Terms_Code=TSPL_PURCHASE_ORDER_HEAD.Terms_Code),0),TSPL_GRN_HEAD.GRN_Date),103),'') as [Payment Due Date]," & _
            " isnull(convert(varchar,Payment_Date,103),'') as [Actual Payment Date], case when isnull(convert(varchar,Payment_Date,103),'')='' then '' else (-1)*isnull(DATEDIFF(DAY,isnull(convert(date,Payment_Date,103),''),isnull(convert(date,DATEADD(DAY,isnull((select no_days from tspl_terms_master where Terms_Code=TSPL_PURCHASE_ORDER_HEAD.Terms_Code),0),TSPL_GRN_HEAD.GRN_Date),103),'') ),'') end as [Over Due Payment(In Days)] "
        End If
        ' If chkSummary.Checked Or rb_srnreturn.IsChecked = True Then chkSRNReturn.Checked
        If chkSummary.Checked Or chkSRNReturn.Checked = True Then
            qry += "  ,isnull(TSPL_SRN_RETURN.Document_No,'') as [SRN Return]"
        End If
        'left join tspl_pi_head on tspl_pi_head.Against_SRN  = TSPL_SRN_HEAD.SRN_No
        qry += " from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left join TSPL_PI_DETAIL on TSPL_SRN_head.SRN_No=TSPL_PI_DETAIL.SRN_ID and TSPL_PI_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code left join tspl_pi_head on tspl_pi_head.pi_no  = TSPL_PI_DETAIL.pi_no  left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  = tspl_pi_head.PI_No and TSPL_VENDOR_INVOICE_HEAD.document_type='I' left join (SELECT TSPL_PAYMENT_DETAIL.Document_No,MAX(convert(date,Payment_Date,103)) as Payment_Date from  TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No group by TSPL_PAYMENT_DETAIL.Document_No )TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No  = TSPL_VENDOR_INVOICE_HEAD.Document_No  left outer join   TSPL_ITEM_MASTER on TSPL_SRN_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code  left JOIN TSPL_Item_Category  ON TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER.item_category LEFT outer JOIN TSPL_ITEM_SUB_CATEGORY ON TSPL_ITEM_MASTER.Sub_item_category = TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code" &
                                "  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SRN_DETAIL .tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SRN_DETAIL .tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SRN_DETAIL  .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SRN_DETAIL  .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SRN_DETAIL  .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SRN_DETAIL  .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SRN_DETAIL  .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SRN_DETAIL  .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SRN_DETAIL  .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SRN_DETAIL  .TAX10 " &
            " left outer join  (select TSPL_GRN_HEAD.*,TSPL_GRN_detail.Against_RGP_No as Det_RGP_NO from TSPL_GRN_HEAD left outer join (select grn_no,max(Against_RGP_No) as Against_RGP_No from TSPL_GRN_DETAIL group by grn_no" &
   " )TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No)TSPL_GRN_HEAD " &
"  on TSPL_SRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_NO " &
            " left join TSPL_SRN_RETURN on TSPL_SRN_RETURN .SRN_No =TSPL_SRN_HEAD.SRN_No" &
  " left outer join (select TSPL_MRN_HEAD.*,TSPL_MRN_HEAD.Against_RGP_No as Det_RGP_NO " &
        " from TSPL_MRN_HEAD " &
   " left outer join (select MRN_No from TSPL_MRN_detail group by MRN_No ) " &
  " TSPL_MRN_detail on isnull(TSPL_MRN_detail.MRN_No,'')=isnull(TSPL_MRN_HEAD.MRN_No,'')) " &
  " TSPL_MRN_HEAD on isnull(TSPL_SRN_HEAD.Against_MRN,'')=isnull(TSPL_MRN_HEAD.MRN_No,'') " &
  " left outer join (select TSPL_PURCHASE_ORDER_HEAD.*,TSPL_PURCHASE_ORDER_HEAD.Against_RGP_NO as Det_RGP_NO " &
    "    from TSPL_PURCHASE_ORDER_HEAD " &
  " left outer join (select PurchaseOrder_No from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No ) " &
  " TSPL_PURCHASE_ORDER_DETAIL on isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'')) " &
  " TSPL_PURCHASE_ORDER_HEAD on isnull(TSPL_MRN_HEAD.Against_PO,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') " &
    " left outer join (select TSPL_REQUISITION_HEAD.* " &
  "      from TSPL_REQUISITION_HEAD " &
 " left outer join (select Requisition_Id from TSPL_REQUISITION_detail group by Requisition_Id ) " &
 " TSPL_REQUISITION_detail on isnull(TSPL_REQUISITION_detail.Requisition_Id,'')=isnull(TSPL_REQUISITION_HEAD.Requisition_Id,'')) " &
"  TSPL_REQUISITION_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition,'')=isnull(TSPL_REQUISITION_HEAD.Requisition_Id,'') "

        '' Anubhooti 28-Jan-2015 (Comment Pending SRN Filter--Come all srn acc. to their status)
        If clsCommon.myLen(strCategoryTable) > 0 Then
            qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SRN_DETAIL.Item_Code"
        End If
        qry += " WHERE 2=2 "

        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += "and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and Tspl_Srn_HEAD.Bill_To_Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and Tspl_Srn_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        Dim strWhrCatg As String = String.Empty
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr_ As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr_ IsNot Nothing AndAlso arr_.Count > 0 Then
                        strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr_.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            qry += " and (" + strWhrCatg + ")"
        End If
        '============

        qry += "  and convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + Todate + "',103)"
        'If ChkRgptype.IsChecked = False Then
        ' qry += " and isnull(Against_RGP,'')=''"
        'End If
        'If chkFnshdGoods.IsChecked Then
        '    qry += "and  TSPL_ITEM_MASTER.Item_Type='F'"
        'ElseIf chkOthers.IsChecked Then
        '    qry += "   and  TSPL_ITEM_MASTER.Item_Type<>'F'"
        'End If
        If (chkSummary.Checked) Then
            qry += " )xxx group by [SRN No] "
        End If

        ' If rb_srnreturn.IsChecked = False Then
        If chkSRNReturn.Checked = False Then
            If chkSummary.Checked Then
                qry += " Having coalesce(max([SRN Return]),'')=''"
            Else
                qry += " and coalesce(TSPL_SRN_RETURN.Document_No,'')=''"
            End If
        End If
        If chkSummary.Checked = False Then
            qry += "order by  [PO No] "
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)


        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.MasterTemplate.BestFitColumns()
            gv.ReadOnly = True
            gv.SummaryRowsBottom.Clear()
        End If

        If dtgv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
        gv.BestFitColumns()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Smitem As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)

        Dim Smitem1 As New GridViewSummaryItem("Challan Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem1)

        Dim Smitem2 As New GridViewSummaryItem("Recieved Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem2)

        Dim Smitem3 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem3)

        Dim Smitem4 As New GridViewSummaryItem("MRN Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem4)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        If OnlyGrid Then
            Exit Sub
        End If
        'Dim str As String = "Pending SRN Report"
        Dim arr As New List(Of String)()
        'arr.Add("Pending SRN Report")
        arr.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingSrn_Qty & "'"))
        arr.Add("From Date:  " + fromdate + "  To Date: " + Todate + "   Type:  " + status + "")
        arr.Add("Company : " + objCommonVar.CurrentCompanyName)
        'If Strlocation <> "" Then
        '    arr.Add(" Location:   " + Strlocation + "")
        'End If
        If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        End If
        If StrDocNo <> "" Then
            arr.Add(" DocumentNo:  " + StrDocNo + "")
        End If
        If StrVendor <> "" Then
            arr.Add(" Vendor:  " + StrVendor + "")
        End If
        'clsCommon.MyExportToExcel(str, gv, arr, "Pending SRN Report")
        If exporter = EnumExportTo.Excel Then
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
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arr)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arr)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Else
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Pending SRN Report", gv, arr, "Pending SRN Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
        'ExporttoMyExcel(qry, Me)



    End Sub
    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)

        Dim bs As New BindingSource()
        ds = connectSql.RunSQLReturnDS(sql)
        bs.DataSource = ds.Tables(0)
        gv.DataSource = bs
        ReStoreGridLayout()
    End Sub
    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If


        Dim IsExists As Boolean = System.IO.Directory.Exists(path)
        If IsExists = False Then
            System.IO.Directory.CreateDirectory(path)
        End If

        Fullpath = path + "\" + "Pending SRN"

        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "gTax"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("There is no data for Show Excel Report.", Me.Text)
                    Return False
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.

                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                'common.clsCommon.MyMessageBoxShow("No Report Created.", "Export Error", MessageBoxButtons.OK)
                Return False
            End Try
        End If
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = True
            e.ExcelStyleElement.FontStyle.Size = 12
        End If

        'e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub

    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub

    Private Sub chk_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgDoc.Enabled = Not chk_All.IsChecked
        If chk_doc_select.IsChecked Then
            chkvendor_All1.IsChecked = chk_doc_select.IsChecked
            cbgVendor1.CheckedAll()
        Else
            cbgDoc.CheckedAll()
        End If
    End Sub

    Private Sub chkvendor_All1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVendor1.Enabled = Not chkvendor_All1.IsChecked
        If chk_vendor_select.IsChecked Then
            chk_All.IsChecked = chk_vendor_select.IsChecked
            cbgDoc.CheckedAll()
        Else
            cbgVendor1.CheckedAll()
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub
    Private Sub FrmPendingSrn_Qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Alt And e.KeyCode = Keys.P Then
            PrintForExcel(EnumExportTo.Excel)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If

    End Sub

    'Private Sub dtpfromdate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
    '    Dim todate1 As String = clsCommon.GetPrintDate(dtptodate1.Value, "dd/MM/yyyy")
    '    Dim qry As String = "select SRN_No as 'Code',SRN_Date as [SRN Date]  from TSPL_SRN_HEAD  where  convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + todate1 + "',103)"
    '    cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgDoc.ValueMember = "Code"
    '    cbgDoc.DisplayMember = "Invoice_Entry_Date"
    'End Sub


    'Private Sub dtptodate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
    '    Dim todate1 As String = clsCommon.GetPrintDate(dtptodate1.Value, "dd/MM/yyyy")
    '    Dim qry As String = "select SRN_No as 'Code',SRN_Date as [SRN Date]  from TSPL_SRN_HEAD  where  convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + todate1 + "',103)"
    '    cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgDoc.ValueMember = "Code"
    '    cbgDoc.DisplayMember = "Invoice_Entry_Date"
    'End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)Handles 
    '    PrintForExcel(EnumExportTo.Excel)
    'End Sub

    'Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        End Sub

    '' =====Ticket No- BM00000009572 Indent Date but these field not include in request
    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strDoc
            If gv.CurrentColumn Is gv.Columns("MRN No") Then
                strDoc = gv.CurrentRow.Cells("MRN No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnMRN, "", True, strDoc)
            ElseIf gv.CurrentColumn Is gv.Columns("SRN No") Then
                strDoc = gv.CurrentRow.Cells("SRN No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnSRN, "", True, strDoc)
            ElseIf gv.CurrentColumn Is gv.Columns("GRN No") Then
                strDoc = gv.CurrentRow.Cells("GRN No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnGRN, "", True, strDoc)
            ElseIf gv.CurrentColumn Is gv.Columns("PO No") Then
                strDoc = gv.CurrentRow.Cells("PO No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseOrder, "", True, strDoc)
            ElseIf gv.CurrentColumn Is gv.Columns("Invoice No") Then
                strDoc = gv.CurrentRow.Cells("Invoice No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseInvoice, "", True, strDoc)
            ElseIf gv.CurrentColumn Is gv.Columns("Indent No") Then
                strDoc = gv.CurrentRow.Cells("Indent No").Value
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseRequistion, "", True, strDoc)
            End If
            
        
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = ReportId()
        TemplateGridview = gv
        PrintForExcel(EnumExportTo.Excel, True)
    End Sub

    ''Private Sub chk_vendor_select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_vendor_select.ToggleStateChanged
    ''    If chkDoc_select.IsChecked = True Then
    ''        cbgDoc.CheckedAll()
    ''        chkDoc_select.IsChecked = False
    ''        chk_All.IsChecked = True
    ''    End If
    ''End Sub

    ''Private Sub chk_doc_select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_doc_select.ToggleStateChanged
    ''    If chkVendor_select.IsChecked = True Then
    ''        cbgVendor.CheckedAll()
    ''        chkVendor_select.IsChecked = False
    ''        chkvendor_All1.IsChecked = True
    ''    End If
    ''End Sub
   
   
    Private Sub chk_All_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_All.ToggleStateChanged
        cbgDoc.Enabled = Not chk_All.IsChecked
    End Sub

    Private Sub chkvendor_All1_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkvendor_All1.ToggleStateChanged
        cbgVendor1.Enabled = Not chkvendor_All1.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        PrintForExcel(EnumExportTo.Excel)

    End Sub

    Private Sub BtnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPDF.Click
        PrintForExcel(EnumExportTo.PDF)

    End Sub
    Sub LoadDocuemntNo()
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(dtptodate1.Value, "dd/MM/yyyy")
        Dim qry As String = "select SRN_No as 'Code',SRN_Date as [SRN Date] from TSPL_SRN_HEAD  where  convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + todate1 + "',103)"
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "SRN Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER   WHERE  Status='N'  order by Vendor_Code"
        cbgvendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgvendor1.ValueMember = "Vendor_Code"
        cbgvendor1.DisplayMember = "Vendor_Name"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(dtptodate1.Value, "dd/MM/yyyy")
        Dim qry As String = "select SRN_No as Code,SRN_Date as  Date from TSPL_SRN_HEAD  where  convert(date,TSPL_SRN_HEAD.SRN_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103)<= convert(date,'" + todate1 + "',103)"
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeDoc", qry, "Code", "Date", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
    End Sub


    'done by stuti on 17/10/2016 against ticket no -BM00000010064

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btn_savelayout_Click(sender As Object, e As EventArgs) Handles btn_savelayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub btn_deletelayout_Click(sender As Object, e As EventArgs) Handles btn_deletelayout.Click
        If clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub
    
    'Private Sub gv_DoubleClick(sender As Object, e As EventArgs) Handles gv.DoubleClick
    '    If gv.CurrentRow IsNot Nothing Then
    '        Dim strSRNNo As String = clsCommon.myCstr(gv.CurrentRow.Cells("SRN No").Value)
    '        If clsCommon.myLen(strSRNNo) > 0 Then
    '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strSRNNo)
    '        End If
    '    End If
    'End Sub

    'Private Sub gv_CellDoubleClick1(sender As Object, e As GridViewCellEventArgs)

    'End Sub
    Private Function ReportId()
        Dim Report_id As String = ""
        If chkSummary.Checked = True Then
            Report_id = Me.Form_ID + "S" + IIf(chkSRNReturn.Checked = True, "R", "") + IIf(chkPaymentStatus.Checked = True, "P", "")
        Else
            Report_id = Me.Form_ID + "D" + IIf(chkSRNReturn.Checked = True, "R", "") + IIf(chkPaymentStatus.Checked = True, "P", "")
        End If
        Return Report_id
    End Function

End Class
