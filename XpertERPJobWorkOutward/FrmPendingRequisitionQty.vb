Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmPendingRequisitionQty
    Inherits FrmMainTranScreen
    Dim strCurrCode As String = Nothing
    Dim sQuery As String = String.Empty
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingRequisitionQty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExcel.Visible = MyBase.isExport
        BtnQuickExport.Visible = MyBase.isExport
    End Sub

    Public Sub SetDiplayMember(ByVal Fnd As common.UserControls.txtMultiSelectFinder, ByVal Col_Name As String, ByVal tb_name As String, ByVal val_col_Name As String)
        Try
            sQuery = "select  " & Col_Name & " from " & tb_name & " where " & val_col_Name & " in (" & clsCommon.GetMulcallString(Fnd.arrValueMember) & ")"
            'sQuery = " select stuff((select ', ['+" & Col_Name & "+']' from " & tb_name & " where " & val_col_Name & " in (" & clsCommon.GetMulcallString(Fnd.arrValueMember) & ") for xml path ('')),1,1,'')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            Dim arrList As New ArrayList
            For Each row As DataRow In dt.Rows
                arrList.Add(row(0))
            Next
            Fnd.arrDispalyMember = arrList
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub FrmPendingRequisitionQty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadLocation()
        chkdocAll.IsChecked = True
        chkVendor_all.IsChecked = True
        chkLocationAll.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        'dtpfromdate.Value = clsCommon.GETSERVERDATE()
        'dtpTodate.Value = clsCommon.GETSERVERDATE()
        'chkdocAll.IsChecked = True
        'chkVendor_all.IsChecked = True
        'chkLocationAll.IsChecked = True
        RadGroupBox2.Enabled = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        IndentTypeBoth.IsChecked = True
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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData()
    End Sub
    Sub PrintData()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Documnet Number", Me.Text)
            Return
        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Location", Me.Text)
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        PrintData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

    End Sub

    Public Sub printdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal islocation As Boolean, ByVal ArrLoc As ArrayList)
        Dim location As String = ""
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim status As String = "ALL"
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""
        Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        'If isDocSelect AndAlso ArrDoc.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Document")
        '    Return
        'ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
        '    Return
        'ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
        '    Return
        'End If
        Dim Address As String
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code = MAX(Location )  )"

        Else
            Address = "'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "'"
        End If

        'If isDocSelect Then
        '    DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
        '    StrDocNo = DocNo.Replace("'", "")
        'End If
        'If isVendorSelect Then
        '    Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
        '    StrVendor = Vendor.Replace("'", "")
        'End If
        'If islocation Then
        '    location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
        '    Strlocation = location.Replace("'", "")
        'End If

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                status = "Po Never Created"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = "PO Partial Created"
            End If
            If rdoCompleted.IsChecked = True Then
                status = "Completed"
            End If
        End If
        Dim qry As String = "select '" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + status + "' as status, code,ICode,max(IName) as IName" + Environment.NewLine & _
                 ",max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" + Environment.NewLine & _
                 ",SUM(Qty* case when RI=1 then 1 else 0 end) as RequitionQty" + Environment.NewLine & _
                 ",SUM(Qty* case when RI=-1 then 1 else 0 end) as POQty" + Environment.NewLine & _
                 ",SUM(Unapproved) as UnapprovedQty" + Environment.NewLine & _
                 ",SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,max(final.Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus,max(Indent_Type) as IndentType,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName," + Address + " as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2  " + Environment.NewLine & _
                 " from (" + Environment.NewLine & _
                 " select TSPL_REQUISITION_DETAIL.Requisition_Id as Code,TSPL_REQUISITION_DETAIL.Vendor_Code as Vendor,TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Item_Desc as IName,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved" & _
                  ",TSPL_REQUISITION_DETAIL.Unit_Code as Unit,TSPL_REQUISITION_DETAIL.Location as Location,1 as RI,TSPL_REQUISITION_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_REQUISITION_HEAD.Requisition_Date as TransDate,TSPL_REQUISITION_DETAIL.Status as ReqStatus,case TSPL_REQUISITION_HEAD.Is_Internal when 'Y' then 'Store Requisition' when 'N' then 'Purchase Indent' end as Indent_Type" & _
                 " from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id where TSPL_REQUISITION_HEAD.Status=1 "
        'If ArrVendor.Count > 0 Then
        '    qry += " and (TSPL_REQUISITION_DETAIL.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + "))" + Environment.NewLine
        'End If
        'If isDocSelect Then
        '    qry += " and TSPL_REQUISITION_HEAD.Requisition_Id in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and TSPL_REQUISITION_DETAIL.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If islocation Then
        '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        '    qry += " and TSPL_REQUISITION_HEAD .Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'End If
        '==added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and TSPL_REQUISITION_HEAD.Requisition_Id in  (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_REQUISITION_HEAD .Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += " and TSPL_REQUISITION_DETAIL.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If

        qry += " and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + ToDate + "',103)"

        qry += " union all" + Environment.NewLine & _
                " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus,'' as Indent_Type   " + Environment.NewLine & _
                " from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=1 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0  " + Environment.NewLine & _
                " union all " + Environment.NewLine & _
                " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus,'' as Indent_Type  from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=0 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 " + Environment.NewLine & _
                " )Final" + Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor" + Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine & _
                " group by Code,ICode" + Environment.NewLine & _
                " having SUM(Chk)>0 and 2=2 "

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                qry += " and ( SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0) "
            End If
            If rdoPOPartial.IsChecked = True Then
                'qry += "and (SUM(Unapproved) >0 and SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 )  "
                qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
            End If

            If rdoCompleted.IsChecked = True Then
                qry += " and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)='Y')"
            End If
        End If

        qry += " order by Code,ICode"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.PurchaseOrder, dt, "RequisitionPendingQty", "Pending Requisition Qty")
    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged
        cbgDocument.Enabled = Not chkdocAll.IsChecked
        If chkDoc_select.IsChecked Then
            chkVendor_all.IsChecked = chkDoc_select.IsChecked
        End If
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all.ToggleStateChanged, chkVendor_select.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendor_all.IsChecked
        If chkVendor_select.IsChecked Then
            chkdocAll.IsChecked = chkVendor_select.IsChecked
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub FrmPendingRequisitionQty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    '======shivani
    Public Sub Load_Report()
        If dtpfromdate.Value > dtpTodate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            dtpfromdate.Focus()
            Exit Sub
        End If
        'If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Document or select all.")
        '    Exit Sub
        'End If
        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
        '    Exit Sub
        'End If
        'If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Vendor or select all.")
        '    Exit Sub
        'End If
        Dim Status As String = ""
        Dim sQuery As String = ""
        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                Status = " and len(isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,''))<=0 and TSPL_REQUISITION_HEAD.close_yn='N'  "
            End If
            If rdoPOPartial.IsChecked = True Then
                Status = " and len(isnull(TSPL_REQUISITION_HEAD.Requisition_Id,''))>0 and (TSPL_REQUISITION_DETAIL.Requisition_Qty-PurchaseOrder_Qty)<>0  and TSPL_REQUISITION_HEAD.close_yn='N' "
            End If
            If rdoCompleted.IsChecked = True Then
                Status = " and len(isnull(TSPL_REQUISITION_HEAD.Requisition_Id,''))>0 and (TSPL_REQUISITION_DETAIL.Requisition_Qty-PurchaseOrder_Qty)=0 and TSPL_REQUISITION_HEAD.close_yn='N'  "
            End If
        End If
        '' =====Ticket No- BM00000009578 Indent Type(Purchase or store)
        'sQuery = " select  ReqNo,ReqQty,PurchaseOrder_No,PurchaseOrder_Qty,SRN_No,SRN_Qty,(SRN_Qty-PurchaseOrder_Qty)as BalanceQty ,dd.Vendor_Code,Location  from (select  TSPL_REQUISITION_DETAIL.Requisition_Id as ReqNo,Requisition_Date,TSPL_REQUISITION_DETAIL.Requisition_Qty as ReqQty ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_SRN_Detail.SRN_No,PurchaseOrder_Qty,SRN_Qty,TSPL_REQUISITION_HEAD.Status as ReqStatus ,TSPL_PURCHASE_ORDER_HEAD.Status as poStatus,TSPL_REQUISITION_DETAIL.Vendor_Code,TSPL_REQUISITION_DETAIL.Location from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id   left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_DETAIL.Requisition_Id left join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No and TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id and TSPL_PURCHASE_ORDER_DETAIL.item_code=TSPL_REQUISITION_DETAIL.item_code left join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_PO = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No and TSPL_SRN_HEAD.against_requisition=TSPL_PURCHASE_ORDER_HEAD.against_requisition left join TSPL_SRN_Detail on TSPL_SRN_Detail.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_Detail.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code)dd "
        'sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=dd.Vendor_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=dd.Location "
        'sQuery += " where convert(date,dd.Requisition_Date ,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,dd.Requisition_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) " & Status & ""
        sQuery = " select case TSPL_REQUISITION_HEAD.Is_Internal when 'Y' then 'Store requition' when 'N' then 'Purchase indent' end as IndentType,TSPL_REQUISITION_HEAD.Requisition_Id as ReqNo,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as  Requisition_Date,TSPL_REQUISITION_DETAIL.Item_Code,tspl_item_master.Item_Desc ,TSPL_REQUISITION_DETAIL.Requisition_Qty as ReqQty,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty ,TSPL_PURCHASE_ORDER_DETAIL.Item_Code ,TSPL_SRN_head.SRN_No ,TSPL_SRN_head.SRN_Date ,TSPL_SRN_DETAIL.SRN_Qty, TSPL_SRN_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Vendor_Code , (SRN_Qty-PurchaseOrder_Qty)as BalanceQty,TSPL_REQUISITION_HEAD.Location ,case   TSPL_REQUISITION_HEAD.close_yn when 'Y' then 'Closed' end as Status, TSPL_REQUISITION_HEAD.Created_By , case when TSPL_REQUISITION_HEAD.Status =1 then TSPL_REQUISITION_HEAD.Modify_By else '' end as [Approved By],  case when  TSPL_REQUISITION_HEAD.Status =1 then 'Approved' else 'Unapproved' end  as [Approved Status]   from TSPL_REQUISITION_DETAIL "
        sQuery += " left join TSPL_REQUISITION_HEAD  on TSPL_REQUISITION_HEAD .Requisition_Id =TSPL_REQUISITION_DETAIL .Requisition_Id "
        sQuery += " left join TSPL_PURCHASE_ORDER_DETAIL  on TSPL_PURCHASE_ORDER_DETAIL .Requisition_Id  =TSPL_REQUISITION_HEAD.Requisition_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code =TSPL_REQUISITION_DETAIL.Item_Code "
        sQuery += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "
        sQuery += " left join TSPL_SRN_detail on TSPL_SRN_detail.PO_ID = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  and TSPL_SRN_detail.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code "
        sQuery += " left join TSPL_SRN_head on TSPL_SRN_HEAD .SRN_No =TSPL_SRN_detail.SRN_No "
        sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_REQUISITION_DETAIL.Vendor_Code"
        sQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_REQUISITION_HEAD.Location"
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code = TSPL_REQUISITION_DETAIL.Item_Code"
        sQuery += " where convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) " & Status & ""
        '==added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_REQUISITION_HEAD.Requisition_Id  in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            sQuery += " and  TSPL_REQUISITION_HEAD.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_REQUISITION_DETAIL.Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_REQUISITION_DETAIL.Item_Code  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        If IndentTypePurchase.IsChecked Then
            sQuery += " and TSPL_REQUISITION_HEAD.Is_Internal='N'"
        End If
        If IndentTypeStore.IsChecked Then
            sQuery += " and TSPL_REQUISITION_HEAD.Is_Internal='Y'"
        End If
        If IndentTypeBoth.IsChecked Then
            ' sQuery += " and TSPL_REQUISITION_HEAD.Is_Internal='Y' and TSPL_REQUISITION_HEAD.Is_Internal='N'"
        End If
        sQuery += " order by convert(date,Requisition_Date,103)"
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
            RadGroupBox2.Enabled = False
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


        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("IndentType").IsVisible = True
        gv.Columns("IndentType").Width = 90
        gv.Columns("IndentType").HeaderText = "Indent Type"

        gv.Columns("Item_Code").IsVisible = True
        gv.Columns("Item_Code").Width = 150
        gv.Columns("Item_Code").HeaderText = "Item Code"

        gv.Columns("Item_Desc").IsVisible = True
        gv.Columns("Item_Desc").Width = 150
        gv.Columns("Item_Desc").HeaderText = "Item Desc"

        gv.Columns("ReqNo").IsVisible = True
        gv.Columns("ReqNo").Width = 150
        gv.Columns("ReqNo").HeaderText = "Intended No."

        gv.Columns("Requisition_Date").IsVisible = True
        gv.Columns("Requisition_Date").Width = 150
        gv.Columns("Requisition_Date").HeaderText = "Intended Date"
        gv.Columns("Requisition_Date").FormatString = "{0:d}"

        gv.Columns("ReqQty").IsVisible = True
        gv.Columns("ReqQty").Width = 90
        gv.Columns("ReqQty").HeaderText = "Intended Qty"

        gv.Columns("PurchaseOrder_No").IsVisible = True
        gv.Columns("PurchaseOrder_No").Width = 150
        gv.Columns("PurchaseOrder_No").HeaderText = " PO No."

        gv.Columns("PurchaseOrder_Date").IsVisible = True
        gv.Columns("PurchaseOrder_Date").Width = 150
        gv.Columns("PurchaseOrder_Date").HeaderText = "PO Date"
        gv.Columns("PurchaseOrder_Date").FormatString = "{0:d}"

        gv.Columns("PurchaseOrder_Qty").IsVisible = True
        gv.Columns("PurchaseOrder_Qty").Width = 90
        gv.Columns("PurchaseOrder_Qty").HeaderText = "PO Qty"

        gv.Columns("SRN_No").IsVisible = True
        gv.Columns("SRN_No").Width = 150
        gv.Columns("SRN_No").HeaderText = "SRN No."

        gv.Columns("SRN_Qty").IsVisible = True
        gv.Columns("SRN_Qty").Width = 90
        gv.Columns("SRN_Qty").HeaderText = "Received Qty"


        gv.Columns("BalanceQty").IsVisible = True
        gv.Columns("BalanceQty").Width = 90
        gv.Columns("BalanceQty").HeaderText = "Balance Qty"

        gv.Columns("Created_By").IsVisible = True
        gv.Columns("Created_By").Width = 150
        gv.Columns("Created_By").HeaderText = "Created By"

        gv.Columns("Approved By").IsVisible = True
        gv.Columns("Approved By").Width = 150
        gv.Columns("Approved By").HeaderText = "Approved By"

        gv.Columns("Approved Status").IsVisible = True
        gv.Columns("Approved Status").Width = 150
        gv.Columns("Approved Status").HeaderText = "Approved Status"

        If rdobtnall.IsChecked = True Then
            gv.Columns("Status").IsVisible = True
            gv.Columns("Status").Width = 90
            gv.Columns("Status").HeaderText = "Status"
        End If




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("ReqQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("PurchaseOrder_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("SRN_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("BalanceQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Load_Report()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If


            'If chkDoc_select.IsChecked Then
            '    Dim strDocName As String = ""
            '    For Each StrName As String In cbgDocument.CheckedDisplayMember()
            '        If clsCommon.myLen(strDocName) > 0 Then
            '            strDocName += ", "
            '        End If
            '        strDocName += StrName
            '    Next
            '    Dim strDocCode As String = ""
            '    For Each StrCode As String In cbgDocument.CheckedValue
            '        If clsCommon.myLen(strDocCode) > 0 Then
            '            strDocCode += ", "
            '        End If
            '        strDocCode += StrCode
            '    Next
            '    arrHeader.Add((" Doc Name: " + strDocName + " "))
            'End If


            'If chkVendor_select.IsChecked Then
            '    Dim stVendorName As String = ""
            '    For Each StrName As String In cbgVendor.CheckedDisplayMember()
            '        If clsCommon.myLen(stVendorName) > 0 Then
            '            stVendorName += ", "
            '        End If
            '        stVendorName += StrName
            '    Next
            '    Dim strVendorCode As String = ""
            '    For Each StrCode As String In cbgVendor.CheckedValue
            '        If clsCommon.myLen(strVendorCode) > 0 Then
            '            stVendorName += ", "
            '        End If
            '        strVendorCode += StrCode
            '    Next
            '    arrHeader.Add(("Vendor Name: " + strVendorCode + " "))
            'End If

            'If chkLocationAll.IsChecked Then
            '    Dim stLocationName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember()
            '        If clsCommon.myLen(stLocationName) > 0 Then
            '            stLocationName += ", "
            '        End If
            '        stLocationName += StrName
            '    Next
            '    Dim strLocationCode As String = ""
            '    For Each StrCode As String In cbgVendor.CheckedValue
            '        If clsCommon.myLen(strLocationCode) > 0 Then
            '            strLocationCode += ", "
            '        End If
            '        strLocationCode += StrCode
            '    Next
            '    arrHeader.Add(("Vendor Name: " + strLocationCode + " "))
            'End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Pending Indent Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Pending Indent Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select Requisition_Id as Code,description as [Description] from TSPL_REQUISITION_HEAD "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String = "select Requisition_Id as Code,description as Name from TSPL_REQUISITION_HEAD where convert(date,Requisition_Date ,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,Requisition_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) "
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)

    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)

    End Sub

    Private Sub BtnQuickExport_Click(sender As Object, e As EventArgs) Handles BtnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingRequisitionQty & "'"))
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub


End Class
