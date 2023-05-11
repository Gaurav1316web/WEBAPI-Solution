Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common


Public Class FrmIndentReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmIndentReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub




    Private Sub FrmIndentReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadLocation()
        LoadItem()
        chkdocAll.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        rdbtnBasedOnPO.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PEN-IND-RPT"
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


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id as Code,TSPL_REQUISITION_HEAD.description as [Description] from TSPL_REQUISITION_HEAD left outer join TSPL_REQUISITION_DETAIL on  TSPL_REQUISITION_HEAD.Requisition_Id= TSPL_REQUISITION_Detail.Requisition_Id where TSPL_REQUISITION_HEAD.Status =1 and LEN(TSPL_REQUISITION_DETAIL .Vendor_Code )=0 "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"

    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Sub LoadItem()
        Dim qry As String = "select Item_Code as[Item Code],item_desc as [Description]  from TSPL_ITEM_MASTER "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub

    Public Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isitemSelect As Boolean, ByVal ArrItem As ArrayList, ByVal islocation As Boolean, ByVal ArrLoc As ArrayList)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If rdbtnBasedOnPO.IsChecked = True Then
                Dim Address As String
                Dim dtCompany As DataTable
                Dim dtLoc As DataTable
                Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                dtCompany = clsDBFuncationality.GetDataTable(CompanyQry)
                If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
                    Dim LocAdd = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name ))As LocAdd from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code =" + clsCommon.GetMulcallString(ArrLoc) + ")"
                    dtLoc = clsDBFuncationality.GetDataTable(LocAdd)
                    Address = clsCommon.myCstr(dtLoc.Rows(0)("LocAdd"))
                Else
                    Address = clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress"))
                End If




                If isDocSelect AndAlso ArrDoc.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one Document")
                    Return
                ElseIf isitemSelect AndAlso ArrItem.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one Item For Print")
                    Return
                ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                    Return
                End If
                Dim qry As String = "select code,ICode,max(IName) as IName" + Environment.NewLine & _
                                     " ,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,SUM(Qty* case when RI=1 then 1 else 0 end) as IndentQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as  ReciptQty ,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PendingQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,MAX(DelDate)as DelDate,max(final.Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + Address + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2  " + Environment.NewLine & _
                                     "  from ( select TSPL_REQUISITION_DETAIL.Requisition_Id as Code,TSPL_REQUISITION_DETAIL.Vendor_Code as Vendor,TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Item_Desc as IName,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved,TSPL_REQUISITION_DETAIL.Unit_Code as Unit,TSPL_REQUISITION_DETAIL.Location as Location,1 as RI,TSPL_REQUISITION_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_REQUISITION_HEAD.Requisition_Date as TransDate,TSPL_REQUISITION_DETAIL.Status as ReqStatus,TSPL_REQUISITION_HEAD .Require_Date  as DelDate from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id where TSPL_REQUISITION_HEAD.Status=1 and LEN(TSPL_REQUISITION_DETAIL .Vendor_Code )=0 "

                If isDocSelect Then
                    qry += " and TSPL_REQUISITION_HEAD.Requisition_Id in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                End If
                If isitemSelect Then
                    qry += " and TSPL_REQUISITION_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(ArrItem) + ") "
                End If
                If islocation Then
                    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
                    qry += " and TSPL_REQUISITION_HEAD .Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                qry += " and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + ToDate + "',103)" + Environment.NewLine & _
                                     " union all" + Environment.NewLine & _
                                    " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus,null as DelDate from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=1 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0" + Environment.NewLine & _
                                    " --union all " + Environment.NewLine & _
                                    " -- select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus  from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=0 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 " + Environment.NewLine & _
                                    " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor" + Environment.NewLine & _
                                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine & _
                                    " group by Code,ICode having SUM(Chk)>0 and 2=2  order by Code,ICode"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "IndentReport", "Pending Indent Report")
            ElseIf RdbtnBasedOnSrn.IsChecked = True Then
                Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)
                Dim qryBasedSrn As String = "select TSPL_REQUISITION_HEAD .Requisition_Id as Indent_No,TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Item_Desc as IName,TSPL_REQUISITION_DETAIL.Requisition_Qty as IndentQty,TSPL_REQUISITION_DETAIL.Unit_Code as Unit,TSPL_REQUISITION_DETAIL.Location as Location,TSPL_REQUISITION_HEAD.Requisition_Date as TransDate,TSPL_REQUISITION_HEAD .Require_Date  as DelDate,TSPL_REQUISITION_HEAD .Dept as DeptCode,TSPL_REQUISITION_HEAD .Dept_Desc as DeptDesc,TSPL_REQUISITION_HEAD .Request_By as ReqBy ," + Environment.NewLine & _
                                            " '" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2 ," + Environment.NewLine & _
                                            "  Isnull((select SUM(srn_Qty)from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_SRN_HEAD on TSPL_PURCHASE_ORDER_DETAIL .PurchaseOrder_No =TSPL_SRN_HEAD .Against_PO left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL .SRN_No =tspl_srn_head.SRN_No where TSPL_SRN_DETAIL .Item_Code =TSPL_PURCHASE_ORDER_DETAIL .Item_Code and TSPL_PURCHASE_ORDER_DETAIL .Status=1 and LEN(Isnull(TSPL_PURCHASE_ORDER_DETAIL .PurchaseOrder_No,''))>0 ),0)as ReceivedQty " + Environment.NewLine & _
                                            " from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_detail .Requisition_Id = TSPL_REQUISITION_HEAD .Requisition_Id where TSPL_REQUISITION_HEAD.Status=1 and LEN(TSPL_REQUISITION_DETAIL .Vendor_Code )=0 "
                If isDocSelect Then
                    qryBasedSrn += " and TSPL_REQUISITION_HEAD.Requisition_Id in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                End If
                If isitemSelect Then
                    qryBasedSrn += " and TSPL_REQUISITION_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(ArrItem) + ") "
                End If
                If islocation Then
                    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
                    qryBasedSrn += " and TSPL_REQUISITION_HEAD .Location in (" + clsCommon.GetMulcallString(ArrLoc) + ")"
                End If

                qryBasedSrn += " and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + ToDate + "',103)"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryBasedSrn)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PendingBasedOnSrnIndentReport", "PendingBased On Srn IndentReport")

            End If
            frmCRV = Nothing
        Catch er As Exception
            myMessages.myExceptions(er)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadLocation()
        LoadItem()
        chkdocAll.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        rdbtnBasedOnPO.IsChecked = True
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData()
    End Sub
    Sub PrintData()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        If itemselect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        PrintData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, itemselect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged
        cbgDocument.Enabled = Not chkdocAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub itemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles itemall.ToggleStateChanged
        cbgItem.Enabled = Not itemall.IsChecked
    End Sub

    Private Sub FrmIndentReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If


    End Sub
End Class
