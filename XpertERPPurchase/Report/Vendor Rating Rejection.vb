Imports common
Public Class Vendor_Rating_Rejection
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Vendor_Rating_Rejection)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Private Sub Vendor_Rating_Rejection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadVendor()
        LoadItem()
        LoadLocation()
        chkvendor_All1.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")

    End Sub
    Sub LoadVendor()
        qry = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE  Status='N' order by Vendor_Code  "
        cbgVendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor1.ValueMember = "Vendor_Code"
        cbgVendor1.DisplayMember = "Vendor_Name"
    End Sub

    Sub LoadItem()
        qry = "select Item_Code as[Item Code],item_desc as [Description]  from TSPL_ITEM_MASTER "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        funReset()
    End Sub
    Sub funReset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        LoadVendor()
        LoadItem()
        LoadLocation()
        chkvendor_All1.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
    End Sub
    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click

        PrintData()
    End Sub
    Sub PrintData()

        If chk_vendor_select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
            Return
        End If
        If itemselect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
        funPrint(fromdate, todate, chk_vendor_select.IsChecked, cbgVendor1.CheckedValue, itemselect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub

    Sub funPrint(ByVal FromDate As String, ByVal ToDate As String, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemselect As Boolean, ByVal ArrItem As ArrayList, ByVal isLocation As Boolean, ByVal ArrLoc As ArrayList)
        Dim location As String = ""
        Dim Item As String = ""
        Dim Vendor As String = ""

        Dim Strlocation As String = ""
        Dim StrItem As String = ""
        Dim StrVendor As String = ""

        If isVendorSelect Then
            Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If
        If isitemselect Then
            Item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            StrItem = Item.Replace("'", "")
        End If
        If isLocation AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
        End If
        Dim CompanyQry As String = "select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        ''qry = "select  '" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "')as image1,(select Logo_Img2 from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "')as image2,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,'" + FromDate + "' as FromDate,'" + ToDate + "' as ToDate,max(ICode) as ItemCode ,max(IName) as ItemDesc, (final.Vendor) as VendorCode, max(final.VendorName) as VendorName,max(Unit)as UOM,sum(final.ReceiptQty) as ReceivedQty, sum(final.RejectedQty )as RejectedQty,0 as RejectedPercentage" & _
        ''       " from ( select TSPL_SRN_HEAD.SRN_Date as srndate,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_HEAD.Vendor_Name as VendorName,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Status as ReqStatus,TSPL_SRN_HEAD .Bill_To_Location as bill_to_address,Rejected_Qty as RejectedQty ,isnull( ( select  mrn_qty  from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .MRN_No =TSPL_SRN_DETAIL .MRN_Id and TSPL_SRN_DETAIL .Item_Code =TSPL_MRN_DETAIL .Item_Code ),0 ) as ReceiptQty  from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1" & _
        ''"union all " & _
        ''       "select '' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty  from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0" & _
        ''"union all " & _
        ''       " select '' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('') ) final where 2=2"
        Dim Address As String
        If isLocation AndAlso cbgLocation.CheckedValue.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =max(final.bill_to_address))"
        Else
            Address = "'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "'"
        End If

        qry = "select '" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem, '" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "')as image1,(select Logo_Img2 from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "')as image2," + Address + " as CompanyAddress,'" + FromDate + "' as FromDate,'" + ToDate + "' as ToDate,max(ICode) as ItemCode ,max(IName) as ItemDesc, (final.Vendor) as VendorCode, max(final.VendorName) as VendorName,max(Unit)as UOM,sum(final.ReceiptQty) as ReceivedQty, sum(final.RejectedQty )as RejectedQty,0 as RejectedPercentage ,SUM(SRN_Qty) as  SRN_Qty ,max(final.bill_to_address ) as bill_to_address " & _
               " from ( select TSPL_SRN_HEAD.SRN_Date as srndate,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_HEAD.Vendor_Name as VendorName,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Status as ReqStatus,TSPL_SRN_HEAD .Bill_To_Location as bill_to_address,Rejected_Qty as RejectedQty ,isnull(TSPL_SRN_DETAIL .SRN_Qty,0 ) as ReceiptQty,TSPL_SRN_DETAIL.SRN_Qty as SRN_Qty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1" & _
        "union all " & _
               "select '' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty  ,0 as  SRN_Qty  from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0" & _
        "union all " & _
               " select '' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty ,  0 as  SRN_Qty from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('') ) final where 2=2"


        If isVendorSelect Then
            qry += " and Vendor in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        End If
        If isitemselect Then
            qry += " and ICode in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        End If
        If isLocation AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qry += " and Bill_to_Address in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        qry += " and convert(date,srndate ,103)>= convert(date,'" + FromDate + "',103) and convert(date,srndate,103)<= convert(date,'" + ToDate + "',103)"
        qry += "group by ICode,Vendor having sum(ReceiptQty) <>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "VendorRatingRejection", "VendorRating Rej")
        frmCRV = Nothing
    End Sub
    'Sub funPrint(ByVal FromDate As String, ByVal ToDate As String, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemselect As Boolean, ByVal ArrItem As ArrayList)

    '    Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '    Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

    '    ' qry = "select code,ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, SUM(Qty* case when RI=1 then 1 else 0 end) as SRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as InvoiceQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, max(Final.MRP)as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = max(bill_to_address))as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2,max(final.RejectedQty)  as RejectedQty,sum(final.ReceiptQty) as ReceiptQty" & _
    '    qry = "select '" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,'" + FromDate + "' as fromdate,'" + ToDate + "' as todate,max(ICode) as ItemCode ,max(IName) as ItemDesc, (final.Vendor) as VendorCode, max(final.VendorName) as VendorName,max(Unit)as UOM,'04/12/2011' as FilterFromDate,'06/02/2012' as FilterToDate,sum(final.ReceiptQty) as ReceivedQty, sum(final.RejectedQty )as RejectedQty,0 as RejectedPercentage" & _
    '                                " from ( select TSPL_SRN_DETAIL.SRN_No as Code,TSPL_SRN_HEAD.SRN_Date as srndate,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_HEAD.Vendor_Name as VendorName,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Status as ReqStatus,TSPL_SRN_HEAD .Bill_To_Location as bill_to_address,Rejected_Qty as RejectedQty ,isnull( ( select  mrn_qty  from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .MRN_No =TSPL_SRN_DETAIL .MRN_Id and TSPL_SRN_DETAIL .Item_Code =TSPL_MRN_DETAIL .Item_Code ),0 ) as ReceiptQty  from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1  "

    '    qry += "union all select TSPL_PI_DETAIL.SRN_ID as Code,'' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty" & _
    '             ",0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty " & _
    '             " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 " & _
    '             " union all select TSPL_PI_DETAIL.SRN_ID as Code,'' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_HEAD.Vendor_Name as VendorName,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved" & _
    '             ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty" & _
    '             " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('')  )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code group by Code,Vendor,srndate having 2=2 "

    '    If isVendorSelect Then
    '        qry += " and Vendor in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
    '    End If
    '    If isitemselect Then
    '        qry += " and ICode in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
    '    End If
    '    qry += " and convert(date,srndate ,103)>= convert(date,'" + FromDate + "',103) and convert(date,srndate,103)<= convert(date,'" + ToDate + "',103)"

    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    PurchaseOrderViewer.funreport(dt, "VendorRatingRejection", "VendorRatingRej")
    'End Sub

    Private Sub chkvendor_All1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkvendor_All1.ToggleStateChanged, chk_vendor_select.ToggleStateChanged
        cbgVendor1.Enabled = Not chkvendor_All1.IsChecked = True
    End Sub

    Private Sub itemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles itemall.ToggleStateChanged, itemselect.ToggleStateChanged
        cbgItem.Enabled = Not itemall.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        funClose()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VEN-RET-REJ"
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


    Private Sub Vendor_Rating_Rejection_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            funReset()
        End If

    End Sub
End Class
