Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmPendingMrn_Qty
    Inherits FrmMainTranScreen

    '=========================added by preeti gupta against ticket no [BM00000009312]======================


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingMrn_Qty)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnExport.Visible = MyBase.isExport
        btnprint1.Visible = False
    End Sub



    Private Sub FrmPendingMrn_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate1.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadLocation()
        chkall.IsChecked = True
        chk_Vendor_All.IsChecked = True
        chkLocationAll.IsChecked = True
        Dim CancelMRN As String = clsDBFuncationality.getSingleValue("select Description  from TSPL_FIXED_PARAMETER where Code='is_Allow_cancel_Transaction'")
        If CancelMRN = "1" Then 'Working'
            chkCancel.Visible = True
        Else
            chkCancel.Visible = False
        End If
    End Sub
  

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        'dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        'dtpToDate1.Value = clsCommon.GETSERVERDATE()
        'LoadDocuemntNo()
        'LoadVendor()
        'LoadLocation()
        'chkall.IsChecked = True
        'chk_Vendor_All.IsChecked = True
        'chkLocationAll.IsChecked = True
        RadGroupBox7.Enabled = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rdobtnAll.Enabled = True
        rdobtnCompleted.Enabled = True
        rdobtnSRNnever.Enabled = True
        rdobtnSRNPartial.Enabled = True
        'txtDocNo.Enabled = True
        'txtLocation.Enabled = True
        'txtVendor.Enabled = True
        chkCancel.Checked = False
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


    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
       
        PrintData()
    End Sub
   
    Public Sub Refreshdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal islocation As Boolean, ByVal Arrloc As ArrayList)
        Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Document")
            Return
        ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        Dim qry As String = Nothing

       
        ''changes [7862]
        '    Dim qry As String = "select * from (select max(TSPL_REQUISITION_HEAD.Requisition_Id) as [Indent No] ,convert(varchar,max(TSPL_REQUISITION_HEAD.Requisition_Date) ,103) as [Indent Date],max(TSPL_GRN_HEAD.Against_PO) as [PO No] ,convert(varchar,max(PurchaseOrder_Date),103) as [PO Date],max(GRN_No) as [GRN No], " & _
        '   " convert(varchar,max(GRN_Date),103) as [GRN Date],code as [MRN No],convert(varchar,(TransDate),103) as [MRN Date] , max(SRNNo) as [SRN No],max(SRNDate) as [SRN Date], MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName, " & _
        '   " max(Final.Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, (ICode) as [Item Code],MAX(IName) as [Item Name], max(Unit)as Unit, " & _
        '   "SUM(Qty* case when RI=1 then 1 else 0 end) as MRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as SRNQty, SUM(Unapproved) as UnapprovedQty, " & _
        '   " SUM((Qty *RI)- Unapproved) as PendingQty ,MAX(Rate) as Rate,max(Final.MRP)as MRP ,sum(Rejected_Qty) as [Rejected Q ty],sum(Short_Qty) as [Short Qty] from( " & _
        '" select TSPL_MRN_DETAIL.MRN_No as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName " & _
        '   ",TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location, " & _
        '  " 1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_DETAIL.MRP,TSPL_MRN_DETAIL.Batch_No,TSPL_MRN_DETAIL.MFG_Date, " & _
        '   "TSPL_MRN_DETAIL.Expiry_Date,TSPL_MRN_DETAIL.Disc_Per,MRN_Date as TransDate,TSPL_MRN_DETAIL.Status as ReqStatus, " & _
        '   "TSPL_MRN_head.Bill_To_Location as bill_to_address ,0 as Rejected_Qty,0 as Short_Qty ,'' as SRNNo, '' as SRNDate from TSPL_MRN_DETAIL left outer join " & _
        '   "TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_HEAD.Status=1 "

        '    '====added by shivani
        '    If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
        '        qry += " and TSPL_MRN_HEAD.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        '    End If
        '    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '        qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        '    End If
        '    If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
        '        qry += "  and TSPL_MRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        '    End If

        '    qry += " and convert(date,TSPL_MRN_HEAD.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103)<= convert(date,'" + ToDate + "',103)"
        '    ''===
        '    'qry += " Union all  select TSPL_MRN_DETAIL_HISTORY.MRN_No as Code,TSPL_MRN_HEAD_HISTORY.Vendor_Code as Vendor,TSPL_MRN_DETAIL_HISTORY.Item_Code as ICode,TSPL_MRN_DETAIL_HISTORY.Item_Desc as IName ,case when (SRN_Qty )>0 then 0 else TSPL_MRN_DETAIL_HISTORY.MRN_Qty end as Qty,0 as Unapproved,TSPL_MRN_DETAIL_HISTORY.Unit_Code as Unit,TSPL_MRN_DETAIL_HISTORY.Location as Location, 1 as RI,TSPL_MRN_DETAIL_HISTORY.Item_Cost as Rate,1 as Chk,TSPL_MRN_DETAIL_HISTORY.MRP,TSPL_MRN_DETAIL_HISTORY.Batch_No,TSPL_MRN_DETAIL_HISTORY.MFG_Date, TSPL_MRN_DETAIL_HISTORY.Expiry_Date,TSPL_MRN_DETAIL_HISTORY.Disc_Per,MRN_Date as TransDate,TSPL_MRN_DETAIL_HISTORY.Status as ReqStatus, TSPL_MRN_HEAD_HISTORY.Bill_To_Location as bill_to_address ,0 as Rejected_Qty,0 as Short_Qty  from TSPL_MRN_DETAIL_HISTORY left outer join  TSPL_MRN_HEAD_HISTORY on TSPL_MRN_HEAD_HISTORY.MRN_No=TSPL_MRN_DETAIL_HISTORY.MRN_No left join TSPL_SRN_DETAIL on TSPL_MRN_DETAIL_HISTORY.MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL_HISTORY.Item_Code=TSPL_SRN_DETAIL.Item_Code  where TSPL_MRN_DETAIL_HISTORY.Status=0 and TSPL_MRN_HEAD_HISTORY.Status=1 "
        '    'qry += " and convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date,103)<= convert(date,'" + ToDate + "',103)"
        '    ''==

        '    qry += "union all" & _
        '       " select TSPL_SRN_DETAIL.MRN_ID as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode " & _
        '          ",TSPL_SRN_DETAIL.Item_Desc as IName,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0)-isnull(xx.Qty,0) as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk, 0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,TSPL_MRN_HEAD.MRN_Date as TransDate,'' as ReqStatus,''as bill_to_address,Rejected_Qty,TSPL_SRN_DETAIL.Short_Qty, TSPL_SRN_HEAD.SRN_No  as SRNNo, convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as SRNDate from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code  and isnull(TSPL_MRN_DETAIL.PO_ID,'')=isnull(TSPL_SRN_DETAIL.PO_ID,'') left outer join  TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No" & _
        '         " left outer join (select TSPL_SRN_DETAIL.srn_no,TSPL_SRN_DETAIL.MRN_ID as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode " & _
        '          ",TSPL_SRN_DETAIL.Item_Desc as IName,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Qty,0 as Unapproved,'' as Unit,'' as Location,1 as RI,0 as Rate,0 as Chk, 0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,TSPL_MRN_HEAD.MRN_Date as TransDate,'' as ReqStatus,''as bill_to_address,TSPL_SRN_DETAIL.Short_Qty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code  left outer join  TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No left join tspl_srn_return on tspl_srn_return.SRN_No =TSPL_SRN_HEAD.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 AND tspl_srn_return.SRN_No <> ''" & _
        '          ")xx on xx.Code=TSPL_SRN_DETAIL.MRN_ID and xx.Vendor=TSPL_SRN_HEAD.Vendor_Code and xx.ICode=TSPL_SRN_DETAIL.Item_Code and xx.SRN_No=TSPL_SRN_DETAIL.SRN_No" & _
        '          " where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " & _
        '       "union all  select TSPL_SRN_DETAIL.MRN_ID as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode" & _
        '    ",TSPL_SRN_DETAIL.Item_Desc as IName,0  as Qty,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Unapproved" & _
        '    ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address , Rejected_Qty, TSPL_SRN_DETAIL.Short_Qty, TSPL_SRN_HEAD.SRN_No  as SRNNo, convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as SRNDate" & _
        '    " from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code " & _
        '    " left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code" & _
        '    " left outer join TSPL_MRN_HEAD on final.code=TSPL_MRN_HEAD.MRN_No " & _
        '    " left outer join TSPL_GRN_HEAD on TSPL_MRN_HEAD.against_grn=TSPL_GRN_HEAD.GRN_No " & _
        '    " left outer join TSPL_PURCHASE_ORDER_HEAD on isnull(TSPL_GRN_HEAD.Against_PO,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'')" & _
        '    " left outer join TSPL_REQUISITION_HEAD  on isnull(TSPL_REQUISITION_HEAD.Requisition_Id ,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition ,'') " & _
        '    " group by Code,TransDate ,Icode  "


        '    If Not rdobtnAll.IsChecked Then
        '        If rdobtnSRNnever.IsChecked = True Then
        '            qry += " having SUM(Chk)>0 and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
        '        End If
        '        If rdobtnSRNPartial.IsChecked = True Then
        '            'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0)"
        '            qry += " having  (SUM(Unapproved) >0 and  (SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) )))"
        '        End If
        '        If rdobtnCompleted.IsChecked = True Then
        '            qry += " having SUM(Chk)>0 and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
        '        End If
        '    End If
        '    qry += " ) as final "
        '    If rdobtnSRNnever.IsChecked = True Then
        '        qry += " where [SRN No] ='' "
        '    End If
        '    If rdobtnAll.IsChecked Then
        '        qry += " having SUM(Chk) > 0"
        '    End If

        '    qry += " order by [mrn date] "
        qry = "select Indent_no as [Indent No.],convert(varchar,Indent_Date,103) as [Indent date],PurchaseOrder_No as [PO Number],convert(varchar,PurchaseOrder_Date,103) as [PO Date],GRN_No as [GRN No.],convert(varchar,GRN_Date,103) as [GRN Date],MRN_No as [MRN No.],convert(varchar,MRN_Date,103) as [MRN Date],SRN_No as [SRN No.],convert(varchar,SRN_Date,103) as [SRN Date],Vendor_Code as [Vendor Code],Vendor_desc as [Vendor Name],location_code as [Location Code],Loc_desc as [Location Name],Item_Code as [Item Code],Item_Desc as [Item Name],UOM ,MRN_Qty as [MRN Qty],SRN_qty as [SRN Qty],rate as [Rate],MRP ,Rejected_Qty as [Rejected Qty],Short_Qty as [Short Qty],Pending as [Pending Qty],Status1 as [Status] from ( " & _
        "select   max(Indent_no) as Indent_no ,max(Indent_Date) as Indent_Date ,max(PurchaseOrder_No) as PurchaseOrder_No ,max(PurchaseOrder_Date) as PurchaseOrder_Date ,max(GRN_No) as GRN_No ,max(GRN_Date)as GRN_Date ,MRN_No ,MRN_Date,max(SRN_No ) as SRN_No ,max(SRN_Date ) as SRN_Date ,max(final.Vendor_Code ) as Vendor_Code,max(TSPL_VENDOR_MASTER .Vendor_Name  ) as Vendor_desc,max(Bill_To_Location ) as location_code,max(TSPL_LOCATION_MASTER .Location_Desc) as Loc_desc,final.Item_Code,max(TSPL_ITEM_MASTER .Item_Desc) as Item_Desc  ,max(final.Unit_code )as UOM,sum(MRN_Qty ) as MRN_Qty,sum(SRN_qty ) as SRN_qty,sum(final.Rate) as rate,sum(MRP ) as MRP,max(Rejected_Qty ) as Rejected_Qty,sum(Short_Qty ) as Short_Qty,max(final.Status ) as status,sum(MRN_Qty )-(sum(SRN_qty )+max(Rejected_Qty )) as Pending ,max(TranStatus) as Status1 from (" & _
        "select 1 as RI ,TSPL_PURCHASE_ORDER_HEAD.Against_Requisition as Indent_no,TSPL_REQUISITION_HEAD.Requisition_Date as Indent_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,TSPL_GRN_HEAD.GRN_No ,TSPL_GRN_HEAD.GRN_Date ,TSPL_MRN_HEAD.MRN_No ,TSPL_MRN_HEAD.MRN_Date,'' as SRN_No ,null as SRN_Date,TSPL_MRN_HEAD.Vendor_Code ,TSPL_MRN_HEAD.Bill_To_Location ,TSPL_MRN_DETAIL.Item_Code ,TSPL_MRN_DETAIL.Unit_code ,isnull(TSPL_MRN_DETAIL.MRN_Qty,0) as MRN_Qty,0 as SRN_qty ,isnull(TSPL_MRN_DETAIL.Item_Cost,0) as Rate,isnull(TSPL_MRN_DETAIL.MRP,0) as MRP ,0 as Rejected_Qty,0 as Short_Qty,0 as Status,case when TSPL_MRN_HEAD.IsCancel=1 then 'Cancel' else '' end as TranStatus  from TSPL_MRN_DETAIL " & _
        " left join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No =TSPL_MRN_DETAIL.MRN_No " & _
        " left join TSPL_GRN_DETAIL on isnull(TSPL_GRN_DETAIL.GRN_No,'') =isnull(TSPL_MRN_DETAIL.GRN_Id,'') and TSPL_GRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code and isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_MRN_DETAIL.PO_ID,'') " & _
        " left join TSPL_GRN_HEAD on isnull(TSPL_GRN_HEAD.GRN_No,'') =isnull(TSPL_GRN_DETAIL.GRN_No,'') " & _
        " left join TSPL_PURCHASE_ORDER_DETAIL on isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')  =isnull(TSPL_GRN_DETAIL.PO_Id,'') and TSPL_GRN_DETAIL.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
        " and isnull(TSPL_MRN_DETAIL.PO_ID,'') =isnull(TSPL_GRN_DETAIL.PO_Id,'') AND isnull(TSPL_GRN_DETAIL.GRN_No ,'')=isnull(TSPL_MRN_DETAIL.GRN_ID ,'') " & _
        " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " & _
        " left outer join TSPL_REQUISITION_HEAD  on isnull(TSPL_REQUISITION_HEAD.Requisition_Id ,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition ,'') " & _
        " where TSPL_MRN_HEAD.Status =1"
        qry += " and convert(date,TSPL_MRN_HEAD.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103)<= convert(date,'" + ToDate + "',103)"
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and TSPL_MRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        qry += " union " & _
            "select -1 as RI, TSPL_PURCHASE_ORDER_HEAD.Against_Requisition as Indent_no,TSPL_REQUISITION_HEAD.Requisition_Date as Indent_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,TSPL_GRN_HEAD.GRN_No ,TSPL_GRN_HEAD.GRN_Date ,TSPL_MRN_HEAD.MRN_No ,TSPL_MRN_HEAD.MRN_Date,TSPL_SRN_HEAD.SRN_No ,TSPL_SRN_HEAD.SRN_Date , TSPL_MRN_HEAD.Vendor_Code ,TSPL_MRN_HEAD.Bill_To_Location ,TSPL_MRN_DETAIL.Item_Code ,TSPL_MRN_DETAIL.Unit_code ,0 as MRN_Qty,isnull(TSPL_SRN_DETAIL.SRN_Qty,0) as SRN_Qty  ,0 as Rate,0 as MRP,isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Rejected_Qty,isnull(TSPL_SRN_DETAIL.Short_Qty,0) as Short_Qty,TSPL_SRN_HEAD.Status,case when TSPL_MRN_HEAD.IsCancel=1 then 'Cancel' else '' end as TranStatus  from TSPL_SRN_DETAIL" & _
" left join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No " & _
"  left join tspl_srn_return on tspl_srn_return.SRN_No =TSPL_SRN_HEAD.SRN_No " & _
" left outer join TSPL_MRN_DETAIL on isnull(TSPL_MRN_DETAIL.MRN_No,'')=isnull(TSPL_SRN_DETAIL.MRN_Id,'') and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code  left outer join TSPL_MRN_HEAD on isnull(TSPL_MRN_HEAD.MRN_No,'')=isnull(TSPL_MRN_DETAIL.MRN_No,'') " & _
" left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code and isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_MRN_DETAIL.PO_ID,'') " & _
" left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No " & _
" left join TSPL_PURCHASE_ORDER_DETAIL on isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')  =isnull(TSPL_GRN_DETAIL.PO_Id,'') and TSPL_GRN_DETAIL.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
" and isnull(TSPL_MRN_DETAIL.PO_ID,'') =isnull(TSPL_GRN_DETAIL.PO_Id,'') AND isnull(TSPL_GRN_DETAIL.GRN_No ,'')=isnull(TSPL_MRN_DETAIL.GRN_ID ,'') " & _
" left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No" & _
" left outer join TSPL_REQUISITION_HEAD  on isnull(TSPL_REQUISITION_HEAD.Requisition_Id ,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition ,'')"

        qry += " where convert(date,TSPL_MRN_HEAD.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103)<= convert(date,'" + ToDate + "',103)"
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
        qry += " and TSPL_MRN_HEAD.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and TSPL_MRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If

        qry += " and not exists (select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_DETAIL.SRN_No) ) as final " & _
            " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =final.Item_Code " & _
            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =final.Bill_To_Location  " & _
            " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =final.Vendor_Code  "
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and final.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and final.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and final.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        qry += "group by final.MRN_No ,final.MRN_Date ,final.Item_Code  ) as xx "
        qry += " where 2=2"

        If Not rdobtnAll.IsChecked Then
            If rdobtnSRNnever.IsChecked = True Then
                qry += " and isnull(xx.SRN_No,'')='' and xx.Status1<>'Cancel'"
            End If
            If rdobtnSRNPartial.IsChecked = True Then
                'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0)"
                qry += " and len(xx.SRN_No)> 0 and status =0"
            End If
            If rdobtnCompleted.IsChecked = True Then
                qry += " and len(xx.SRN_No)> 0 and status =1"
            End If
        End If

        If chkCancel.Checked Then
            qry = " select max(TSPL_GRN_HEAD.Against_PO) as [PO No] ,convert(varchar,max(PurchaseOrder_Date),103) as [PO Date],max(GRN_No) as [GRN No], "
            qry += " convert(varchar,max(GRN_Date),103) as [GRN Date],code as [MRN No],convert(varchar,(TransDate),103) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName, "
            qry += " max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, "
            qry += " SUM(Qty* case when RI=1 then 1 else 0 end) as MRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as SRNQty, SUM(Unapproved) as UnapprovedQty, "
            qry += " SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(Final.MRP)as MRP ,sum(Rejected_Qty) as [Rejected Qty],sum(Short_Qty) as [Short Qty] from("
            qry += " select TSPL_MRN_DETAIL_HISTORY.MRN_No as Code,Vendor_Code as Vendor,TSPL_MRN_DETAIL_HISTORY.Item_Code as ICode,TSPL_MRN_DETAIL_HISTORY.Item_Desc,TSPL_MRN_DETAIL_HISTORY.MRN_Qty as Qty,0 as Unapproved, TSPL_MRN_DETAIL_HISTORY.Unit_code,TSPL_MRN_DETAIL_HISTORY.Location as Location, 1 as RI,TSPL_MRN_DETAIL_HISTORY.Item_Cost as Rate,1 as Chk,TSPL_MRN_DETAIL_HISTORY.MRP,TSPL_MRN_DETAIL_HISTORY.Batch_No,TSPL_MRN_DETAIL_HISTORY.MFG_Date, TSPL_MRN_DETAIL_HISTORY.Expiry_Date,TSPL_MRN_DETAIL_HISTORY.Disc_Per,MRN_Date as TransDate,TSPL_MRN_DETAIL_HISTORY.Status as ReqStatus, TSPL_MRN_HEAD_HISTORY.Bill_To_Location as bill_to_address ,0 as Rejected_Qty,0 as Short_Qty    from TSPL_MRN_DETAIL_HISTORY left join TSPL_MRN_HEAD_HISTORY on TSPL_MRN_HEAD_HISTORY.MRN_No =TSPL_MRN_DETAIL_HISTORY.MRN_No "
            qry += " where TSPL_MRN_DETAIL_HISTORY.Status=0 and TSPL_MRN_HEAD_HISTORY.Status=1"
            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_MRN_HEAD_HISTORY.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_MRN_HEAD_HISTORY.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qry += " and TSPL_MRN_HEAD_HISTORY.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += "  and TSPL_MRN_HEAD_HISTORY.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += " and convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date,103)<= convert(date,'" + ToDate + "',103))final"
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code left outer join TSPL_MRN_HEAD on final.code=TSPL_MRN_HEAD.MRN_No left outer join TSPL_GRN_HEAD on TSPL_MRN_HEAD.against_grn=TSPL_GRN_HEAD.GRN_No left outer join TSPL_PURCHASE_ORDER_HEAD on isnull(TSPL_GRN_HEAD.Against_PO,'')=isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') group by Code,ICode,TransDate having SUM(Chk)>0 "
            qry += " order by TransDate"
        End If



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dt
            Gv1.MasterTemplate.BestFitColumns()
            Gv1.ReadOnly = True
            Gv1.BestFitColumns()

        End If

        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found")
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        RadGroupBox7.Enabled = False
        ReStoreGridLayout()
    End Sub

    Private Sub btnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub


    Private Sub chkall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkall.ToggleStateChanged
        cbgDoc.Enabled = Not chkall.IsChecked
        If chk_Doc_Select.IsChecked Then
            chk_Vendor_All.IsChecked = chk_Doc_Select.IsChecked
        End If
    End Sub

    Private Sub chk_Vendor_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_Vendor_All.ToggleStateChanged
        cbgVendor1.Enabled = Not chk_Vendor_All.IsChecked
        If chk_Vendor_Select.IsChecked Then
            chkall.IsChecked = chk_Vendor_Select.IsChecked
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub




    Private Sub FrmPendingMrn_Qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'printdata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Sub PrintData()

        If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chk_Vendor_Select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location", Me.Text)
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(dtpToDate1.Value, "dd/MM/yyyy")
        PrintData(fromdate, todate1, chk_Doc_Select.IsChecked, cbgDoc.CheckedValue, chk_Vendor_Select.IsChecked, cbgVendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Public Sub printdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal islocation As Boolean, ByVal Arrloc As ArrayList)
        Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Document", Me.Text)
            Return
        ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location", Me.Text)
            Return
        End If
        Dim qry As String = "select code,max(ICode)as ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location" & _
                           ",MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" & _
                            ",SUM(Qty* case when RI=1 then 1 else 0 end) as MRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as SRNQty" & _
                           ", SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(Final.MRP)as MRP" & _
                           ",max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per" & _
                           ",max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = max(bill_to_address))as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2 " & _
                            " from(select TSPL_MRN_DETAIL.MRN_No as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName" & _
                             ",TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_DETAIL.MRP,TSPL_MRN_DETAIL.Batch_No,TSPL_MRN_DETAIL.MFG_Date,TSPL_MRN_DETAIL.Expiry_Date,TSPL_MRN_DETAIL.Disc_Per,MRN_Date as TransDate,TSPL_MRN_DETAIL.Status as ReqStatus,TSPL_MRN_head.Bill_To_Location as bill_to_address   from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_HEAD.Status=1 "
        'If isDocSelect Then
        '    qry += " and TSPL_MRN_HEAD.MRN_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and TSPL_MRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If islocation Then
        '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        '    qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'End If
        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.MRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_MRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and TSPL_MRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        qry += " and convert(date,TSPL_MRN_HEAD.MRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103)<= convert(date,'" + ToDate + "',103)"


        qry += "union all" & _
                 " select TSPL_SRN_DETAIL.MRN_ID as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode" & _
                 ",TSPL_SRN_DETAIL.Item_Desc as IName,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk, 0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " & _
                 "union all  select TSPL_SRN_DETAIL.MRN_ID as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode" & _
                 ",TSPL_SRN_DETAIL.Item_Desc as IName,0  as Qty,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Unapproved" & _
                 ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address" & _
                 " from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code " & _
                 " group by Code,ICode having SUM(Chk)>0"
        If Not rdobtnAll.IsChecked Then
            If rdobtnSRNnever.IsChecked = True Then
                qry += "and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
            End If
            If rdobtnSRNPartial.IsChecked = True Then
                'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0)"
                qry += "and(SUM(Unapproved) >0 and  (SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) )))"
            End If
            If rdobtnCompleted.IsChecked = True Then
                qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
            End If
        End If
        qry += "order by Code"
       
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If chkCancel.Checked = True Then
            clsCommon.MyMessageBoxShow("No Print For Cancel Document", Me.Text)
        Else
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "Pending MRN", "Pending MRN Qty")
            frmCRV = Nothing
        End If
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chk_Vendor_Select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location", Me.Text)
            Return
        End If
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Dim fromdate As String = clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(dtpToDate1.Value, "dd/MM/yyyy")
        Refreshdata(fromdate, todate1, chk_Doc_Select.IsChecked, cbgDoc.CheckedValue, chk_Vendor_Select.IsChecked, cbgVendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingMrn_Qty & "'"))
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate1.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Pending MRN Report", Gv1, arrHeader, "Pending MRN Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select MRN_No as Code,Description as [Description] from TSPL_MRN_HEAD "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE Status='N'  order by Vendor_Code"
        cbgvendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgvendor1.ValueMember = "Vendor_Code"
        cbgvendor1.DisplayMember = "Vendor_Name"
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String
        If chkCancel.Checked Then
            qry = "select MRN_No as Code,Description as Name from TSPL_MRN_HEAD_HISTORY where convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date ,103)>= convert(date,'" + dtpFromdate1.Value + "',103) and convert(date,TSPL_MRN_HEAD_HISTORY.MRN_Date,103)<= convert(date,'" + dtpToDate1.Value + "',103)"
        Else
            qry = "select MRN_No as Code,Description as Name from TSPL_MRN_HEAD where convert(date,TSPL_MRN_HEAD.MRN_Date ,103)>= convert(date,'" + dtpFromdate1.Value + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103)<= convert(date,'" + dtpToDate1.Value + "',103)"
        End If
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += "  and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        'If Gv1.Rows.Count > 0 Then
        '    Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("MRN No").Value)
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, strTransType)
        'End If
    End Sub


    Private Sub chkCancel_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCancel.ToggleStateChanged

    End Sub

    Private Sub chkCancel_CheckStateChanged(sender As Object, e As EventArgs) Handles chkCancel.CheckStateChanged
        If chkCancel.Checked = True Then
            rdobtnAll.Enabled = False
            rdobtnCompleted.Enabled = False
            rdobtnSRNnever.Enabled = False
            rdobtnSRNPartial.Enabled = False
        Else
            rdobtnAll.Enabled = True
            rdobtnCompleted.Enabled = True
            rdobtnSRNnever.Enabled = True
            rdobtnSRNPartial.Enabled = True
            'txtDocNo.Enabled = False
            'txtLocation.Enabled = False
            'txtVendor.Enabled = False
        End If
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub
End Class
