Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmPendingPO_Qty
    Inherits FrmMainTranScreen
    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select PurchaseOrder_No as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD "
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgdoc.ValueMember = "Code"
        cbgdoc.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
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






    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("PEN-PO-QTY")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub

    Private Sub FrmPendingPO_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetUserMgmtNew()


        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        dtpTodate1.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadLocation()
        chkdocAll1.IsChecked = True
        chkVendor_all1.IsChecked = True
        chkLocationAll.IsChecked = True


    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        dtpTodate1.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadVendor()
        LoadLocation()
        chkdocAll1.IsChecked = True
        chkVendor_all1.IsChecked = True
        chkLocationAll.IsChecked = True
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

        If chkDoc_select1.IsChecked AndAlso cbgdoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
            Return
        End If
        If chkVendor_select1.IsChecked AndAlso cbgvendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate1.Value, "dd/MM/yyyy")
        PrintData(fromdate, todate, chkDoc_select1.IsChecked, cbgdoc.CheckedValue, chkVendor_select1.IsChecked, cbgvendor1.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

    End Sub
    Public Sub printdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal islocation As Boolean, ByVal Arrloc As ArrayList)
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
        Dim qry As String = "select code,ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
        " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
        " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
        " SUM(Unapproved) as UnapprovedQty," & _
        " SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," & _
        " max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,Max(ReqStatus) as ReqStatus,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = max(billto_location))as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2 " + Environment.NewLine & _
        " from (" + Environment.NewLine & _
        " select TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,TSPL_PURCHASE_ORDER_DETAIL.Status as ReqStatus,TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location as billto_location from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 "
        If isDocSelect Then
            qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        End If
        If isVendorSelect Then
            qry += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        End If
        If islocation Then
            '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
            qry += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"


        End If
        qry += " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + ToDate + "',103)"


        qry += " union all" + Environment.NewLine & _
        " select TSPL_GRN_DETAIL.PO_Id as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus ,'' as billto_location from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_Id,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_GRN_DETAIL.PO_Id as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus ,'' as billto_location from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No  where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.PO_Id,''))>0 " + Environment.NewLine & _
        " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.billto_location = loc_master.Location_Code " & _
        " group by Code,ICode" & _
        " having SUM(Chk)>0 "
        If Not rdobtnall.IsChecked Then
            If rdobtnGRnNever.IsChecked = True Then
                qry += "and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
            End If
            If rdobtnGrnPartial.IsChecked = True Then
                'qry += "and (SUM(Unapproved) >0 and SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0)"
                qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
            End If
            If rdobtncompletd.IsChecked = True Then
                qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
            End If
        End If
        qry += "order by Code,ICode"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PendingPOQty", "Pending PO Quantity")
        frmCRV = Nothing
    End Sub
    Private Sub chkdocAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll1.ToggleStateChanged
        cbgdoc.Enabled = Not chkdocAll1.IsChecked
        If chkDoc_select1.IsChecked Then
            chkVendor_all1.IsChecked = chkDoc_select1.IsChecked
        End If
    End Sub
    Private Sub chkVendor_all1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all1.ToggleStateChanged
        cbgvendor1.Enabled = Not chkVendor_all1.IsChecked
        If chkVendor_select1.IsChecked Then
            chkdocAll1.IsChecked = chkVendor_select1.IsChecked
        End If
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub FrmPendingPO_Qty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown


        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If




    End Sub
End Class
