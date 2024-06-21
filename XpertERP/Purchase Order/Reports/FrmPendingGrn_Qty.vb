Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmPendingGrn_Qty
    Inherits FrmMainTranScreen

   
    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        'dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        'todate.Value = clsCommon.GETSERVERDATE()
        RadGroupBox10.Enabled = True
        Gv1.DataSource = Nothing
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
    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        PrintData()
    End Sub
    Sub PrintData()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(todate.Value, "dd/MM/yyyy")
        PrintData(fromdate, todate1, chk_doc_select.IsChecked, cbgdoc.CheckedValue, chkVendor_select.IsChecked, cbgvendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

    End Sub
    Public Sub Refreshdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal islocation As Boolean, ByVal Arrloc As ArrayList)
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
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        '=======================update by preeti gupta Against ticket no [BM00000009053]
        Dim GRNqry As String = "select TSPL_GRN_DETAIL.Row_Type as RowType,TSPL_GRN_DETAIL.GRN_No as Code,TSPL_GRN_HEAD.Vendor_Code as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName" & _
          ",TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_Code as Unit,TSPL_GRN_DETAIL.Location as Location,1 as RI,TSPL_GRN_DETAIL.Item_Cost as Rate" & _
          ",1 as Chk,TSPL_GRN_DETAIL.MRP,TSPL_GRN_DETAIL.Batch_No,TSPL_GRN_DETAIL.MFG_Date,TSPL_GRN_DETAIL.Expiry_Date,TSPL_GRN_DETAIL.Disc_Per,TSPL_GRN_HEAD.Carrier,TSPL_GRN_HEAD.VehicleNo" & _
          ",TSPL_GRN_HEAD.GRNo,TSPL_GRN_HEAD.GENo,TSPL_GRN_HEAD.GEDate,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_HEAD.Description,TSPL_GRN_HEAD.Remarks,TSPL_GRN_HEAD.Bill_To_Location as billto_location ,TSPL_GRN_HEAD.Ship_To_Location" & _
          ",TSPL_GRN_HEAD.GRN_Date as TransDate,TSPL_GRN_DETAIL.Status as ReqStatus,case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancel' else '' end as TranStatus from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.Status=0 and TSPL_GRN_HEAD.Status=1  "
        'If isDocSelect Then
        '    qry += " and TSPL_GRN_HEAD.GRN_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If islocation Then
        '    qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'End If
        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            GRNqry += " and TSPL_GRN_HEAD.GRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            GRNqry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                GRNqry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            GRNqry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        '=================================

        GRNqry += " and convert(date,TSPL_GRN_HEAD.GRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103) "


        Dim MRNQry = "select TSPL_MRN_DETAIL.Row_Type as RowType,TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor" & _
                 ",TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName" & _
                ",(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Qty" & _
                ",0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No" & _
                ",null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo" & _
                ",null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description" & _
                ",null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,'' as ReqStatus" & _
                " ,case when TSPL_MRN_HEAD.IsCancel=1 then 'Cancel' else '' end as TranStatus from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No" & _
                 " where TSPL_MRN_HEAD.Status=1 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 " & _
                 "union all" & _
                  " select TSPL_MRN_DETAIL.Row_Type as RowType,TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode " & _
                 ",TSPL_MRN_DETAIL.Item_Desc as IName,0  as Qty,(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Unapproved" & _
                 ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo" & _
                 ",null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description,null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,'' as ReqStatus" & _
                 " ,case when TSPL_MRN_HEAD.IsCancel=1 then 'Cancel' else '' end as TranStatus from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No  where TSPL_MRN_HEAD.Status=0 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 "

        Dim qry As String = "select max(TSPL_GRN_HEAD.Against_PO) as [PO No] ,convert(varchar,max(PurchaseOrder_Date),103) as [PO Date],Code as [GRN No], " & _
        " convert(varchar,max(TransDate),103) as [GRN Date],MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as [Vendor Name],Max(ReqStatus) as ReqStatus, " & _
        " (ICode) as [Item Code],max(IName) as [Item Name],max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName, " & _
        " SUM(Qty* case when RI=1 then 1 else 0 end) as GRNQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as MRNQty, " & _
        "-SUM((Qty *RI)- Unapproved) as [Balance Qty] ,MAX(Rate) as Rate,max(TranStatus) as Status from  ( "
        If Not rdobtnall.IsChecked Then
            If rdoMRNnever.IsChecked = True Then
                qry += GRNqry
            End If
            If rdobtnMrnpartial.IsChecked = True Then
                qry += GRNqry & " Union All  " & MRNQry
            End If
            If rdobtnCompleted.IsChecked = True Then
                qry += GRNqry & " Union All  " & MRNQry
            End If
        Else
            qry += GRNqry & " Union All  " & MRNQry
        End If
        qry += ") Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.billto_location = loc_master.Location_Code" & _
         " left outer join TSPL_GRN_HEAD on Final.Code=TSPL_GRN_HEAD.GRN_No left outer join " & _
         "TSPL_PURCHASE_ORDER_HEAD on TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
         " group by Code,ICode,TranStatus,RowType having SUM(Chk)>0"
        If Not rdobtnall.IsChecked Then
            If rdoMRNnever.IsChecked = True Then
                qry += "and final.code not in (select   GRN_Id from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_HEAD.IsCancel=0) "
            End If
            If rdobtnMrnpartial.IsChecked = True Then
                qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
            End If
            If rdobtnCompleted.IsChecked = True Then
                qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
            End If
      
        End If
        qry += "order by max(TransDate)"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dt
            ' Gv1.MasterTemplate.BestFitColumns()
            Gv1.BestFitColumns()
            Gv1.ReadOnly = True

        End If

        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        RadGroupBox10.Enabled = False
        ReStoreGridLayout()
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
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        Dim qry As String = "select Code,ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" & _
        ", SUM(Qty* case when RI=1 then 1 else 0 end) as GRNQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as MRNQty, SUM(Unapproved) as UnapprovedQty" & _
        ", SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(GRNo) as GRNo,max(Description) as Description,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = max(billto_location))as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2 " & _
         "from (select TSPL_GRN_DETAIL.GRN_No as Code,TSPL_GRN_HEAD.Vendor_Code as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName" & _
          ",TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_Code as Unit,TSPL_GRN_DETAIL.Location as Location,1 as RI,TSPL_GRN_DETAIL.Item_Cost as Rate" & _
          ",1 as Chk,TSPL_GRN_DETAIL.MRP,TSPL_GRN_DETAIL.Batch_No,TSPL_GRN_DETAIL.MFG_Date,TSPL_GRN_DETAIL.Expiry_Date,TSPL_GRN_DETAIL.Disc_Per,TSPL_GRN_HEAD.Carrier,TSPL_GRN_HEAD.VehicleNo" & _
          ",TSPL_GRN_HEAD.GRNo,TSPL_GRN_HEAD.GENo,TSPL_GRN_HEAD.GEDate,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_HEAD.Description,TSPL_GRN_HEAD.Remarks,TSPL_GRN_HEAD.Bill_To_Location as billto_location ,TSPL_GRN_HEAD.Ship_To_Location" & _
          ",TSPL_GRN_HEAD.GRN_Date as TransDate,TSPL_GRN_DETAIL.Status as ReqStatus from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.Status=0 and TSPL_GRN_HEAD.Status=1  "
        'If isDocSelect Then
        '    qry += " and TSPL_GRN_HEAD.GRN_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If islocation Then
        '    qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'End If
        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and TSPL_GRN_HEAD.GRN_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        '========================
        qry += " and convert(date,TSPL_GRN_HEAD.GRN_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= convert(date,'" + ToDate + "',103)"

        qry += "union all " & _
                "select TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor" & _
                ",TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName" & _
               ",(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Qty" & _
               ",0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No" & _
               ",null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo" & _
               ",null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description" & _
               ",null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,'' as ReqStatus" & _
               " from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No" & _
                " where TSPL_MRN_HEAD.Status=1 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0" & _
                "union all" & _
                 " select TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode" & _
                ",TSPL_MRN_DETAIL.Item_Desc as IName,0  as Qty,(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Unapproved" & _
                ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo" & _
                ",null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description,null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,'' as ReqStatus" & _
                " from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No  where TSPL_MRN_HEAD.Status=0 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 )" & _
                "Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.billto_location = loc_master.Location_Code" & _
                " group by Code,ICode having SUM(Chk)>0"
        If Not rdobtnall.IsChecked Then
            If rdoMRNnever.IsChecked = True Then
                qry += "and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
            End If
            If rdobtnMrnpartial.IsChecked = True Then
                'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0)"
                qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
            End If
            If rdobtnCompleted.IsChecked = True Then
                qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
            End If
        End If
        qry += "order by Code,ICode"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PendingGRNQty", "Pending GRN Qty")
        frmCRV = Nothing
    End Sub
    'Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    If Gv1.Rows.Count > 0 Then
    '        ExportToExcel(EnumExportTo.Excel)
    '    Else
    '        RadMessageBox.Show("No Data Found to Display", Me.Text)
    '    End If
    'End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingGrn_Qty & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")


            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
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
                clsCommon.MyExportToPDF("Pending GRN Report", Gv1, arrHeader, "Pending GRN Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub btnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingGrn_Qty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        btnprint1.Visible = False
    End Sub
    Private Sub FrmPendingGrn_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        todate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadLocation()
        chk_doc_All.IsChecked = True
        chkVendor_All1.IsChecked = True
        chkLocationAll.IsChecked = True
        '=done by shivani against ticket [BM00000009242]
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub chk_doc_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_doc_All.ToggleStateChanged
        cbgdoc.Enabled = Not chk_doc_All.IsChecked
        If chk_doc_select.IsChecked Then
            chkVendor_All1.IsChecked = chk_doc_select.IsChecked
        End If
    End Sub
    Private Sub chkVendor_All1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_All1.ToggleStateChanged
        cbgvendor1.Enabled = Not chkVendor_All1.IsChecked
        If chkVendor_select.IsChecked Then
            chk_doc_All.IsChecked = chkVendor_select.IsChecked
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    Private Sub FrmPendingGrn_Qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim todate1 As String = clsCommon.GetPrintDate(todate.Value, "dd/MM/yyyy")
        Refreshdata(fromdate, todate1, chk_doc_select.IsChecked, cbgdoc.CheckedValue, chkVendor_select.IsChecked, cbgvendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select GRN_No as 'Code',Description as [Description] from TSPL_GRN_HEAD "
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgdoc.ValueMember = "Code"
        cbgdoc.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        cbgvendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgvendor1.ValueMember = "Vendor_Code"
        cbgvendor1.DisplayMember = "Vendor_Name"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String = "select GRN_No as Code,Description as Name from TSPL_GRN_HEAD where convert(date,TSPL_GRN_HEAD.GRN_Date ,103)>= convert(date,'" + dtpfromdate1.Value + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= convert(date,'" + todate.Value + "',103)"
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "

        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If Gv1.Rows.Count > 0 Then
            Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("GRN No").Value)
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, strTransType)
        End If

    End Sub

    'done by stuti on 17/10/2016 against ticket no -BM00000010063

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

    Private Sub btn_savelayout_Click(sender As Object, e As EventArgs) Handles btn_savelayout.Click
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

    Private Sub btn_deletelayout_Click(sender As Object, e As EventArgs) Handles btn_deletelayout.Click
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class
