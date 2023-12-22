'' work done agaist ticket no . UDL/26/11/18-000242
'sanjay remove UDL Company code condition
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmPendingRequisitionQty
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim strCurrCode As String = Nothing
    Dim sQuery As String = String.Empty
    Dim fromDate As String = Nothing
    Dim toDate As String = Nothing
    Dim toDateYear As Integer = 0
    Dim byMonth As String = Nothing
    Dim inYear As String = Nothing
    Dim dt As New DataTable
    Dim AllowOnlyOneIssueAgainstStoreRequisition As Boolean = False
    Dim ShowStatusItemWiseInPendingRequisitionRpt As Boolean = False
#End Region
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingRequisitionQty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnExp.Visible = MyBase.isExport
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
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Private Sub FrmPendingRequisitionQty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            AllowOnlyOneIssueAgainstStoreRequisition = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOnlyOneIssueAgainstStoreRequisition, clsFixedParameterCode.AllowOnlyOneIssueAgainstStoreRequisition, Nothing)) = 1, True, False)
            ShowStatusItemWiseInPendingRequisitionRpt = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowStatusItemWiseInPendingRequisitionRpt, clsFixedParameterCode.ShowStatusItemWiseInPendingRequisitionRpt, Nothing)) = 1, True, False)
            If ShowStatusItemWiseInPendingRequisitionRpt = True Then
                rdoShortClose.Visible = True
            Else
                rdoShortClose.Visible = False
            End If
            dtpfromdate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            dtpTodate.Value = clsCommon.GETSERVERDATE()
            LoadDocuemntNo()
            LoadVendor()
            LoadLocation()
            chkdocAll.IsChecked = True
            chkVendor_all.IsChecked = True
            chkLocationAll.IsChecked = True
            RadPageView1.SelectedPage = Me.RadPageView1.Pages("RadPageViewPage1")
            cbxSummary.Checked = False
            cboFiscalYear.Enabled = False
            lblFiscalYear.Enabled = False
            FillFiscalYear()
            byMonth = Nothing
            btnBack.Visible = False
            IndentTypePurchase.IsChecked = True
            'rdobtnall.IsChecked = True
            'IndentTypePurchase.IsChecked = True
            'IndentTypeStore.IsChecked = False

            ' rdoMIndentSummary.Enabled = False
            ' rdoMIndentDetail.Visible = False
            ' If objCommonVar.CurrentUserCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Try
            dtpfromdate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            dtpTodate.Value = clsCommon.GETSERVERDATE()
            RadGroupBox2.Enabled = True
            gv.DataSource = Nothing
            RadPageView1.SelectedPage = RadPageViewPage1
            rdobtnall.IsChecked = True
            IndentTypePurchase.IsChecked = True
            IndentTypeStore.IsChecked = False
            cbxSummary.Checked = False
            byMonth = Nothing
            btnBack.Visible = False
            txtDocNo.arrValueMember = Nothing
            txtLocation.arrValueMember = Nothing
            txtVendor.arrValueMember = Nothing
            txtItem.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        'PrintData()
        print(EnumExportTo.Excel)
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

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        printdata(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

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
        'Dim Address As String
        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
        '    Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code = MAX(Location )  )"

        'Else
        '    Address = "'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "'"
        'End If

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

        'If Not rdobtnall.IsChecked Then
        '    If rdoPonever.IsChecked = True Then
        '        status = "Po Never Created"
        '    End If
        '    If rdoPOPartial.IsChecked = True Then
        '        status = "PO Partial Created"
        '    End If
        '    If rdoCompleted.IsChecked = True Then
        '        status = "Completed"
        '    End If
        'End If
        'Dim qry As String = "select '" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + status + "' as status, code,ICode,max(IName) as IName" + Environment.NewLine & _
        '         ",max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" + Environment.NewLine & _
        '         ",SUM(Qty* case when RI=1 then 1 else 0 end) as RequitionQty" + Environment.NewLine & _
        '         ",SUM(Qty* case when RI=-1 then 1 else 0 end) as POQty" + Environment.NewLine & _
        '         ",SUM(Unapproved) as UnapprovedQty" + Environment.NewLine & _
        '         ",SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,max(final.Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus,max(Indent_Type) as IndentType,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName," + Address + " as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2  " + Environment.NewLine & _
        '         " from (" + Environment.NewLine & _
        '         " select TSPL_REQUISITION_DETAIL.Requisition_Id as Code,TSPL_REQUISITION_DETAIL.Vendor_Code as Vendor,TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Item_Desc as IName,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved" & _
        '          ",TSPL_REQUISITION_DETAIL.Unit_Code as Unit,TSPL_REQUISITION_DETAIL.Location as Location,1 as RI,TSPL_REQUISITION_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_REQUISITION_HEAD.Requisition_Date as TransDate,TSPL_REQUISITION_DETAIL.Status as ReqStatus,case TSPL_REQUISITION_HEAD.Is_Internal when 'Y' then 'Store Requisition' when 'N' then 'Purchase Indent' end as Indent_Type" & _
        '         " from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id where TSPL_REQUISITION_HEAD.Status=1 "
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
        'If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
        '    qry += " and TSPL_REQUISITION_HEAD.Requisition_Id in  (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        'End If
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    qry += " and TSPL_REQUISITION_HEAD .Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        'End If
        'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
        '    qry += " and TSPL_REQUISITION_DETAIL.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        'End If

        'qry += " and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + ToDate + "',103)"

        'qry += " union all" + Environment.NewLine & _
        '        " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus,'' as Indent_Type   " + Environment.NewLine & _
        '        " from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=1 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0  " + Environment.NewLine & _
        '        " union all " + Environment.NewLine & _
        '        " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as ReqStatus,'' as Indent_Type  from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.Status=0 and len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 " + Environment.NewLine & _
        '        " )Final" + Environment.NewLine & _
        '        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor" + Environment.NewLine & _
        '        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine & _
        '        " group by Code,ICode" + Environment.NewLine & _
        '        " having SUM(Chk)>0 and 2=2 "

        'If Not rdobtnall.IsChecked Then
        '    If rdoPonever.IsChecked = True Then
        '        qry += " and ( SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0) "
        '    End If
        '    If rdoPOPartial.IsChecked = True Then
        '        'qry += "and (SUM(Unapproved) >0 and SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 )  "
        '        qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
        '    End If

        '    If rdoCompleted.IsChecked = True Then
        '        qry += " and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)='Y')"
        '    End If
        'End If

        'qry += " order by Code,ICode"

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "RequisitionPendingQty", "Pending Requisition Qty")
            frmCRV = Nothing
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found ", Me.Text)
            gv.DataSource = Nothing
        End If
        'dt = clsDBFuncationality.GetDataTable(qry)


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
    ' OVERLOADED EXISTING FUNCTION SO THAT IT WILL NOT AFFECT OTHER CLIENT WORK
    Private Sub Load_Report(ByVal companyCode As String, ByVal strMonth As String, strYear As String, ByVal strDocumentType As String)
        Try
            toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")

            dtpfromdate.Value = fromDate
            dtpTodate.Value = toDate

            Try
                If IndentTypePurchase.IsChecked Then
                    Load_PendingIndent_Report()
                ElseIf IndentTypeStore.IsChecked Then
                    Load_StoreRequistion_Report()
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' UDL > DOING AS PER PRASHANT SIR AND SHRUTI MADAM TICKET BY EMAIL [27-03-2017]
    Private Sub Load_StoreRequistion_Report()
        Dim qry As String = Nothing
        Try
            If dtpfromdate.Value > dtpTodate.Value Then
                common.clsCommon.MyMessageBoxShow("From Date < " + dtpfromdate.Value + " > cannot be greater than To Date < " + dtpTodate.Value + " > ", Me.Text)
                dtpfromdate.Focus()
                Exit Sub
            End If

            qry = " SELECT lstQry.Req_IssueNo, lstQry.RequisitionNo, format(lstQry.[Issue Posted Date], 'dd/MM/yyyy') AS [Issue Posted Date], COALESCE(DATEPART(mm, [Issue Posted Date]), '') AS [by Month], COALESCE(DATENAME(mm, [Issue Posted Date]), '') AS [In Month], COALESCE((DATENAME(YEAR, [Issue Posted Date])), '') AS [In Year], lstQry.Item_Code, lstQry.Item_Desc,lstQry .Unit_Code , lstQry.Req_Qty, lstQry.Issued_Qty,lstQry.[Bal_Qty],lstQry.[Cost Center Code],lstQry.[Cost Center Name],lstQry.[Unit Cost],lstQry.[Extended Cost] "
            If AllowOnlyOneIssueAgainstStoreRequisition = True Then
                qry += ",  (CASE WHEN len(isnull(lstQry.Req_IssueNo,''))>0 and lstQry.[Qty Status]<>'Completed' THEN 'Closed' ELSE lstQry.[Qty Status] END) AS [Qty Status]"
            Else
                qry += ", lstQry.[Qty Status]"
            End If

            qry += ", lstQry.Dept, lstQry.Dept_Desc, lstQry.Vendor_Code, lstQry.Vendor_Name, lstQry.Location,lstQry.Emergency,lstQry.Category, lstqry.Location_desc, lstQry.created_by "
            qry += " FROM (SELECT MAX(Req_IssueNo) Req_IssueNo, RequisitionNo, [Issue Posted Date], Item_Code, MAX(Item_Desc) Item_Desc,max(unit_code) as Unit_Code ,SUM(Required_Qty) Req_Qty, SUM(Issued_Qty) Issued_Qty, (SUM(Required_Qty) - SUM(Issued_Qty)) AS [Bal_Qty], (CASE WHEN (SUM(Required_Qty) - SUM(Issued_Qty)) > 0 THEN 'Balance' ELSE 'Completed' END) AS [Qty Status], MAX(Dept) AS Dept, MAX(Dept_Desc) AS Dept_Desc, MAX(Vendor_Code) AS Vendor_Code, MAX(Vendor_Name) Vendor_Name, MAX(Location) AS Location, MAX(Location_desc) AS Location_desc,max(Emergency) AS [Emergency] ,max(Catagory) as Category, MAX(created_by) AS created_by, MAX(Modify_By) AS Modify_By , max([Cost Center Code]) as [Cost Center Code],max([Cost Center Name]) as [Cost Center Name], max([Unit Cost]) as [Unit Cost], max ([Extended Cost]) as [Extended Cost] FROM (SELECT MAX(ih.Doc_No) Req_IssueNo, ih.RequisitionNo, COALESCE(CONVERT(date, IH.Posting_Date), '') AS [Issue Posted Date], id.Item_Code, MAX(id.Item_Desc) Item_Desc, max(id.Unit_code) as Unit_Code ,max(id.Required_Qty) Required_Qty, SUM(id.Issued_Qty) Issued_Qty, MAX(IH.Dept) AS Dept, MAX(IH.Dept_Desc) AS Dept_Desc, MAX(COALESCE(RH.Vendor_Code, '')) AS Vendor_Code, MAX(COALESCE(VM.Vendor_Name, '')) Vendor_Name, MAX(RH.Location) AS Location, MAX(lm.location_desc) AS Location_desc,max(case when CONVERT(decimal(18, 2),COALESCE(RH.Emergency, 0)) = 0 THEN 'No' else 'Yes' END) AS [Emergency] ,max(RH.Category) as Catagory, MAX(rh.created_by) AS created_by, MAX(rh.Modify_By) AS Modify_By, max(id.Cost_Code) as [Cost Center Code],max(TSPL_CostCenter_MASTER.Cost_name) as [Cost Center Name] , max(id.Unit_Cost) as [Unit Cost],max(id.Amount) as [Extended Cost] FROM TSPL_IssueReturn_HEAD ih LEFT OUTER JOIN TSPL_IssueReturn_DETAIL id ON ih.Doc_No = id.Doc_No LEFT OUTER JOIN TSPL_REQUISITION_HEAD RH ON IH.RequisitionNo = RH.Requisition_Id LEFT OUTER JOIN TSPL_VENDOR_MASTER VM ON RH.Vendor_Code = VM.Vendor_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER LM ON LM.Location_Code = RH.Location  Left Outer Join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code = id.Cost_Code WHERE 1 = 1 AND Doc_Type = 'issue' AND ih.status = 1 GROUP BY RequisitionNo, id.Item_Code, COALESCE(CONVERT(date, IH.Posting_Date), '') UNION SELECT MAX(ih.Req_IssueNo) Req_IssueNo, ih.RequisitionNo, COALESCE(CONVERT(date, IH.Posting_Date), '') AS [Issue Posted Date], id.Item_Code, MAX(Item_Desc) Item_Desc,max (id.Unit_code) as Unit_Code, SUM(0) AS Required_Qty, -1 * SUM(id.Issued_Qty) Issued_Qty, MAX(IH.Dept) AS Dept, MAX(IH.Dept_Desc) AS Dept_Desc, MAX(COALESCE(RH.Vendor_Code, '')) AS Vendor_Code, MAX(COALESCE(VM.Vendor_Name, '')) Vendor_Name, MAX(RH.Location) AS Location, MAX(lm.location_desc) AS Location_desc,max(case when CONVERT(decimal(18, 2),COALESCE(RH.Emergency, 0)) = 0 THEN 'No' else 'Yes' END) AS [Emergency] ,max(RH.Category) as Catagory, MAX(rh.created_by) AS created_by, MAX(rh.Modify_By) AS Modify_By, max(id.Cost_Code) as [Cost Center Code],max(TSPL_CostCenter_MASTER.Cost_name) as [Cost Center Name] , max(id.Unit_Cost) as [Unit Cost],max(id.Amount) as [Extended Cost] FROM TSPL_IssueReturn_HEAD ih LEFT OUTER JOIN TSPL_IssueReturn_DETAIL id ON ih.Doc_No = id.Doc_No LEFT OUTER JOIN TSPL_REQUISITION_HEAD RH ON IH.RequisitionNo = RH.Requisition_Id LEFT OUTER JOIN TSPL_VENDOR_MASTER VM ON RH.Vendor_Code = VM.Vendor_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER LM ON LM.Location_Code = RH.Location Left Outer Join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code = id.Cost_Code  WHERE 1 = 1 AND Doc_Type = 'return' AND ih.status = 1 AND rh.Is_Internal = 'y' GROUP BY RequisitionNo, Item_Code, COALESCE(CONVERT(date, IH.Posting_Date), '') union"
            qry += " SELECT MAX(ih.Doc_No) Req_IssueNo, RH.Requisition_Id as RequisitionNo, COALESCE(CONVERT(date, Rh.Posting_Date), '') AS [Issue Posted Date], TSPL_REQUISITION_DETAIL.Item_Code, MAX(TSPL_REQUISITION_DETAIL.Item_Desc) Item_Desc,MAX(TSPL_REQUISITION_DETAIL.Unit_Code) Unit_Code ,SUM(Requisition_Qty) AS Requisition_Qty, -1 * SUM(0) Issued_Qty, MAX(RH.Dept) AS Dept, MAX(RH.Dept_Desc) AS Dept_Desc, MAX(COALESCE(RH.Vendor_Code, '')) AS Vendor_Code, MAX(COALESCE(VM.Vendor_Name, '')) Vendor_Name, MAX(RH.Location) AS Location, MAX(lm.location_desc) AS Location_desc,max(case when CONVERT(decimal(18, 2),COALESCE(RH.Emergency, 0)) = 0 THEN 'No' else 'Yes' END) AS [Emergency] ,max(RH.Category) as Catagory, MAX(rh.created_by) AS created_by, MAX(rh.Modify_By) AS Modify_By, max(id.Cost_Code) as [Cost Center Code],max(TSPL_CostCenter_MASTER.Cost_name) as [Cost Center Name] , max(id.Unit_Cost) as [Unit Cost],max(id.Amount) as [Extended Cost] "
            qry += " FROM  TSPL_REQUISITION_HEAD RH"
            qry += " left outer join TSPL_REQUISITION_DETAIL on RH.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id"
            qry += " LEFT OUTER JOIN  TSPL_IssueReturn_HEAD ih  ON IH.RequisitionNo = RH.Requisition_Id "
            qry += " LEFT OUTER JOIN TSPL_IssueReturn_DETAIL id ON ih.Doc_No = id.Doc_No "
            qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER VM ON RH.Vendor_Code = VM.Vendor_Code "
            qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER LM ON LM.Location_Code = RH.Location Left Outer Join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code = id.Cost_Code  WHERE 1 = 1 "
            qry += " AND  Rh.status = 1 AND rh.Is_Internal = 'y'  and isnull(id.Req_IssueNo,'')='' "
            qry += " GROUP BY RH.Requisition_Id, TSPL_REQUISITION_DETAIL.Item_Code, COALESCE(CONVERT(date, RH.Posting_Date), '')) AS fn GROUP BY fn.RequisitionNo, fn.Item_Code, fn.[Issue Posted Date]) AS lstQry "
            qry += " WHERE 1 = 1 AND convert(date,lstQry.[Issue Posted Date],103)>=convert(date,'" + dtpfromdate.Value + "',103) AND convert(date,lstQry.[Issue Posted Date],103)<=convert(date,'" + dtpTodate.Value + "',103) "

            If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
                    qry += " AND LEFT(COALESCE(DATENAME(mm, [Issue Posted Date]), ''),3) IN  ('" + byMonth + "') "
                End If
            End If

            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += " AND  lstQry.RequisitionNo  in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and  lstQry.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and lstQry.Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and lstQry.Item_Code  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If

            qry += " ORDER BY lstQry.[Issue Posted Date], [In Year], [by Month]  "

            'Dim dt As New DataTable
            dt.Rows.Clear()
            dt.Columns.Clear()
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                For Each col As GridViewColumn In gv.Columns
                    col.Width = 150
                    col.ReadOnly = True
                    If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If col.Name = "by Month" Or col.Name = "Req_IssueNo" Then
                            col.IsVisible = False
                        End If
                    Else
                        If col.Name = "by Month" Or col.Name = "Req_IssueNo" Or col.Name = "In Month" Or col.Name = "In Year" Then
                            col.IsVisible = False
                        End If

                    End If
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item4 As New GridViewSummaryItem("Req_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)

                Dim item6 As New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                Dim item5 As New GridViewSummaryItem("Bal_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox2.Enabled = False
            Else
                clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Load_PendingIndent_Report()
        Dim status As String = ""
        Dim qry As String = ""
        Try
            If dtpfromdate.Value > dtpTodate.Value Then
                common.clsCommon.MyMessageBoxShow("From Date < " + dtpfromdate.Value + " > cannot be greater than To Date < " + dtpTodate.Value + " > ", Me.Text)
                dtpfromdate.Focus()
                Exit Sub
            End If
            ' FILTER REPORT INDENTS STATUS ------------------

            If rdoPonever.IsChecked = True Then
                status = " and len(isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,''))<=0 and TSPL_REQUISITION_HEAD.close_yn='N'  "
                'status = " and [PurchaseOrder Qty]=0 and TSPL_REQUISITION_HEAD.close_yn='N'"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = " and len(isnull(TSPL_REQUISITION_HEAD.Requisition_Id,''))>0 and (TSPL_REQUISITION_DETAIL.Requisition_Qty-PurchaseOrder_Qty)<>0 and TSPL_REQUISITION_HEAD.close_yn='N'  "
                'status = " and [Balance Qty]<>0 and len(isnull([PO Doc],''))>0 and TSPL_REQUISITION_HEAD.close_yn='N'"
            End If
            If rdoCompleted.IsChecked = True Then
                status = " and (len(isnull(TSPL_REQUISITION_HEAD.Requisition_Id,''))>0 and (TSPL_REQUISITION_DETAIL.Requisition_Qty-PurchaseOrder_Qty)=0   or " &
                        " ( CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 and TSPL_REQUISITION_HEAD.close_yn='Y')) "
                'status = " and ([Balance Qty] =0 or TSPL_REQUISITION_HEAD.close_yn='Y') "
            End If

            ' status += " and TSPL_REQUISITION_HEAD.close_yn='N' "

            If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                qry += " SELECT COALESCE(TSPL_REQUISITION_HEAD.Dept, '') AS [Dept], COALESCE(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, '') AS [Dept Desc], COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '') AS [Pending Indent Doc], CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103) AS [Pending Indent Date], COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '') AS [PI Created Date],  COALESCE(LEFT(DATENAME(MONTH, TSPL_REQUISITION_HEAD.Posting_Date), 3),'') AS [In Month], (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '') ELSE '' END) AS [PI Posted Date], (CASE WHEN TSPL_REQUISITION_HEAD.Is_Internal = 'N' THEN 'Purchase Indent' END) AS [Indent Type], COALESCE(TSPL_REQUISITION_DETAIL.Item_Code, '') AS [Item Code] , COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc],COALESCE(TSPL_REQUISITION_DETAIL.Unit_code, '') AS [Unit Code], COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '') AS [PO Doc], COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '') AS [PO Date], (CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes' ELSE 'No' END) AS [PI tagged with PO], COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '') AS [Vendor], COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME, '') AS [Vendor Desc], CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) AS [Requisition Qty], CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) AS [PurchaseOrder Qty], CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0))) AS [Balance Qty]," &
                    " (CASE WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'PO Never Created' " &
                    " WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 or " &
                    " ( CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 and TSPL_REQUISITION_HEAD.close_yn='Y') THEN 'Completed' " &
                    " WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'Partial' " &
                    "  WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 and TSPL_REQUISITION_HEAD.close_yn='Y' THEN 'Closed' END) AS [Indents Status],max(case when CONVERT(decimal(18, 2),COALESCE(TSPL_REQUISITION_HEAD.Emergency, 0)) = 0 THEN 'No' else 'Yes' END) AS [Emergency] ,max(TSPL_REQUISITION_HEAD.Category) as Catagory, " &
                    " COALESCE(TSPL_REQUISITION_HEAD.Location, '') AS Location, COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '') AS [Location Desc], COALESCE(TSPL_REQUISITION_HEAD.Created_By, '') AS [Created By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Modify_By ELSE '' END, '') AS [Approved By] , COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Posting_Date ELSE '' END, '') AS [Approved Date] , "
                '", COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN 'Approved' ELSE 'Unapproved' END, '') AS [Approved Status] "
                'qry += " (case when TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status='' or TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status IS NULL THEN 'Unapproved' ELSE TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status END) AS [Approved Status]"
                qry += " ( case when TSPL_REQUISITION_HEAD.status=1 then 'Approved' else 'Unapproved' end ) AS [Approved Status] "
            Else 'ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                'sanjay 20-Nov-2017
                'qry += " SELECT COALESCE(TSPL_REQUISITION_HEAD.Dept, '') AS [Dept], COALESCE(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, '') AS [Dept Desc], COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '') AS [Pending Indent Doc], CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103) AS [Pending Indent Date], COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '') AS [PI Created Date], (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '') ELSE '' END) AS [PI Posted Date], (CASE WHEN TSPL_REQUISITION_HEAD.Is_Internal = 'N' THEN 'Purchase Indent' END) AS [Indent Type], COALESCE(TSPL_REQUISITION_DETAIL.Item_Code, '') AS [Item Code], COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc], COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '') AS [PO Doc], COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '') AS [PO Date], (CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes' ELSE 'No' END) AS [PI tagged with PO], COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '') AS [Vendor], COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME, '') AS [Vendor Desc], CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) AS [Requisition Qty], CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) AS [PurchaseOrder Qty], CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0))) AS [Balance Qty], " & _
                '    " (CASE WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'PO Never Created' " & _
                '    " WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 or " & _
                '    " ( CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 and TSPL_REQUISITION_HEAD.close_yn='Y') THEN 'Completed' " & _
                '    " WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0))  and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'Partial' " & _
                '    "  WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 and TSPL_REQUISITION_HEAD.close_yn='Y' THEN 'Closed' END) AS [Indents Status]," & _
                '    " COALESCE(TSPL_REQUISITION_HEAD.Location, '') AS Location, COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '') AS [Location Desc], COALESCE(TSPL_REQUISITION_HEAD.Created_By, '') AS [Created By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Modify_By ELSE '' END, '') AS [Approved By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Posting_Date ELSE '' END, '') AS [Approved Date],"
                ''qry += " (case when TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status='' or TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status IS NULL THEN 'Unapproved' ELSE TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status END) AS [Approved Status] "
                'qry += " ( case when TSPL_REQUISITION_HEAD.status=1 then 'Approved' else 'Unapproved' end ) AS [Approved Status] "

                qry += "SELECT MAX(COALESCE(TSPL_REQUISITION_HEAD.close_yn,'N')) close_yn,MAX(COALESCE(TSPL_REQUISITION_HEAD.Dept, '')) AS [Dept]  " &
                    ", MAX(COALESCE(TSPL_REQUISITION_HEAD.Dept_Desc, '')) AS [Dept Desc]  " &
                    ", COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '') AS [Pending Indent Doc]  " &
                    ", MAX(CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103)) AS [Pending Indent Date],  " &
                    "MAX(COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '')) AS [PI Created Date],  " &
                    "MAX((CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '') ELSE '' END)) AS [PI Posted Date], " &
                     "MAX((CASE WHEN TSPL_REQUISITION_HEAD.Is_Internal = 'N' THEN 'Purchase Indent' END)) AS [Indent Type] " &
                     ", COALESCE(TSPL_REQUISITION_DETAIL.Item_Code, '') AS [Item Code] " &
                     ", MAX(COALESCE(tspl_item_master.Item_Desc, '')) AS [Item Desc], COALESCE(TSPL_REQUISITION_DETAIL.Unit_code, '') AS [Unit Code] " &
                     ", MAX(COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '')) AS [PO Doc] " &
                     ", MAX(COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '')) AS [PO Date] " &
                     ", MAX((CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes' ELSE 'No' END)) AS [PI tagged with PO] " &
                     ", MAX(COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '')) AS [Vendor], " &
                      "MAX(COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME, '')) AS [Vendor Desc] " &
                    ", CONVERT(decimal(18, 2), max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0))) AS [Requisition Qty] " &
                    ", CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0))) AS [PurchaseOrder Qty] " &
                    ", CONVERT(decimal(18, 2), max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0))) " &
                    "AS [Balance Qty],  "
                If ShowStatusItemWiseInPendingRequisitionRpt = True Then
                    qry += "MAX( (Case When CONVERT(Decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0  " &
                   "And CONVERT(Decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 And TSPL_REQUISITION_HEAD.close_yn='N' and TSPL_REQUISITION_DETAIL.status='N' THEN 'PO Never Created' " &
                   "WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 or  ( CONVERT(decimal(18, 2), COALESCE " &
                   "(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 " &
                   "and (TSPL_REQUISITION_HEAD.close_yn='Y' or TSPL_REQUISITION_DETAIL.status='Y')) THEN 'Completed' " &
                   "WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND " &
                   "CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 " &
                   "AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0))  and " &
                   "TSPL_REQUISITION_HEAD.close_yn='N' and TSPL_REQUISITION_DETAIL.status='N' THEN 'Partial' " &
                   "WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 " &
                    "and (TSPL_REQUISITION_HEAD.close_yn='Y' or TSPL_REQUISITION_DETAIL.status='Y') THEN 'Short Closed' END) )"
                Else
                    qry += "MAX( (Case When CONVERT(Decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0  " &
                    "And CONVERT(Decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 And TSPL_REQUISITION_HEAD.close_yn='N' THEN 'PO Never Created' " &
                    "WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 or  ( CONVERT(decimal(18, 2), COALESCE " &
                    "(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 " &
                    "and TSPL_REQUISITION_HEAD.close_yn='Y') THEN 'Completed' " &
                    "WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND " &
                    "CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 " &
                    "AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0))  and " &
                    "TSPL_REQUISITION_HEAD.close_yn='N' THEN 'Partial' " &
                    "WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 " &
                     "and TSPL_REQUISITION_HEAD.close_yn='Y' THEN 'Closed' END) )"
                End If

                qry += " AS [Indents Status],max(case when CONVERT(decimal(18, 2),COALESCE(TSPL_REQUISITION_HEAD.Emergency, 0)) = 0 THEN 'No' else 'Yes' END) AS [Emergency] ,max(TSPL_REQUISITION_HEAD.Category) as Catagory " &
                    ", MAX(COALESCE(TSPL_REQUISITION_HEAD.Location, '')) AS Location " &
                    ", MAX(COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '')) AS [Location Desc] " &
                    ", MAX(COALESCE(TSPL_REQUISITION_HEAD.Created_By, '')) AS [Created By] " &
                    ", MAX(COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Modify_By ELSE '' END, '')) AS [Approved By] " &
                    ", MAX(COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Posting_Date ELSE '' END, '')) AS [Approved Date] " &
                    ", MAX(( case when TSPL_REQUISITION_HEAD.status=1 then 'Approved' else 'Unapproved' end )) AS [Approved Status] "

                'sanjay 20-Nov-2017
                'Else
                '    qry += " SELECT COALESCE(TSPL_REQUISITION_HEAD.Dept, '') AS [Dept], COALESCE(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, '') AS [Dept Desc], COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '') AS [Pending Indent Doc], CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103) AS [Pending Indent Date], COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '') AS [PI Created Date], (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '') ELSE '' END) AS [PI Posted Date], (CASE WHEN TSPL_REQUISITION_HEAD.Is_Internal = 'N' THEN 'Purchase Indent' END) AS [Indent Type], COALESCE(TSPL_REQUISITION_DETAIL.Item_Code, '') AS [Item Code], COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc], COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '') AS [PO Doc], COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '') AS [PO Date], (CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes' ELSE 'No' END) AS [PI tagged with PO], COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '') AS [Vendor], COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME, '') AS [Vendor Desc], CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) AS [Requisition Qty], CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) AS [PurchaseOrder Qty], CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0))) AS [Balance Qty]," & _
                '        " (CASE WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0  and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'PO Never Created' " & _
                '        " WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 or " & _
                '        " ( CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 and TSPL_REQUISITION_HEAD.close_yn='Y') THEN 'Completed' " & _
                '        " WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0 AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) and TSPL_REQUISITION_HEAD.close_yn='N' THEN 'Partial' " & _
                '        "  WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0 AND CONVERT(decimal(18, 2),COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 and TSPL_REQUISITION_HEAD.close_yn='Y' THEN 'Closed' END) AS [Indents Status], " & _
                '        " COALESCE(TSPL_REQUISITION_HEAD.Location, '') AS Location, COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '') AS [Location Desc], COALESCE(TSPL_REQUISITION_HEAD.Created_By, '') AS [Created By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Modify_By ELSE '' END, '') AS [Approved By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Posting_Date ELSE '' END, '') AS [Approved Date], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN 'Approved' ELSE 'Unapproved' END, '') AS [Approved Status] "
            End If


            qry += " from TSPL_REQUISITION_DETAIL left join TSPL_REQUISITION_HEAD  on TSPL_REQUISITION_HEAD .Requisition_Id =TSPL_REQUISITION_DETAIL .Requisition_Id "

            ' JOINS BETWEEN TABLES ===================
            qry += " left join TSPL_PURCHASE_ORDER_DETAIL  on TSPL_PURCHASE_ORDER_DETAIL .Requisition_Id  =TSPL_REQUISITION_HEAD.Requisition_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code =TSPL_REQUISITION_DETAIL.Item_Code "
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_REQUISITION_DETAIL.Vendor_Code"
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_REQUISITION_HEAD.Location"
            qry += " left join tspl_item_master on tspl_item_master.Item_Code = TSPL_REQUISITION_DETAIL.Item_Code LEFT JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_REQUISITION_HEAD.Dept "

            'qry += " left outer join TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL on TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code=TSPL_REQUISITION_HEAD.Requisition_Id "

            qry += " where 1=1 and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date ,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) "


            'If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '    qry += status
            'End If

            ' FILTERS FOR WHERE CONDITONS ===================
            If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
                    qry += " and COALESCE(LEFT(DATENAME(MONTH, TSPL_REQUISITION_HEAD.Posting_Date), 3),'') in ('" + byMonth + "') "
                End If
            End If

            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_REQUISITION_HEAD.Requisition_Id  in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and  TSPL_REQUISITION_HEAD.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and TSPL_REQUISITION_DETAIL.Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_REQUISITION_DETAIL.Item_Code  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If IndentTypePurchase.IsChecked Then
                qry += " and TSPL_REQUISITION_HEAD.Is_Internal='N'"
                If chkItemWiseSummary.IsChecked = True OrElse chkLocationWiseItemSummary.IsChecked = True Then
                    qry += " and TSPL_REQUISITION_HEAD.Is_Tender='Y' "
                End If

            End If
            If IndentTypeStore.IsChecked Then
                qry += " and TSPL_REQUISITION_HEAD.Is_Internal='Y'"
            End If

            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '    qry += " group by TSPL_REQUISITION_HEAD.Requisition_Id,TSPL_REQUISITION_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Unit_code,COALESCE(TSPL_REQUISITION_HEAD.Dept, '')"
            'End If

            If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                qry += " order by convert(date,TSPL_REQUISITION_HEAD.Posting_Date,103) , LEFT(DATENAME(MONTH, TSPL_REQUISITION_HEAD.Posting_Date), 3)   "
            Else 'ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then

                If ShowStatusItemWiseInPendingRequisitionRpt = True Then
                    qry += " group by TSPL_REQUISITION_HEAD.Requisition_Id,TSPL_REQUISITION_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Unit_code,TSPL_REQUISITION_HEAD.Dept,TSPL_REQUISITION_HEAD.close_yn,TSPL_REQUISITION_DETAIL.status "
                    If rdoPonever.IsChecked = True Then
                        qry += " having CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)))=0 and TSPL_REQUISITION_HEAD.close_yn='N' and TSPL_REQUISITION_DETAIL.status='N'"
                    ElseIf rdoPOPartial.IsChecked = True Then
                        qry += " HAVING (CONVERT(decimal(18, 2) " &
                          ", max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)))<>0 and len(MAX(COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '')))>0 and TSPL_REQUISITION_HEAD.close_yn='N' and TSPL_REQUISITION_DETAIL.status='N')"
                    ElseIf rdoCompleted.IsChecked = True Then
                        qry += " HAVING ((CONVERT(decimal(18, 2) " &
                          ", max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)))=0))"
                    ElseIf rdoShortClose.IsChecked = True Then
                        qry += " HAVING CONVERT(decimal(18, 2), max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0))) > 0 AND CONVERT(decimal(18, 2),sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0))) = 0 and (TSPL_REQUISITION_HEAD.close_yn='Y' or TSPL_REQUISITION_DETAIL.status='Y')"
                    End If
                Else
                    qry += " group by TSPL_REQUISITION_HEAD.Requisition_Id,TSPL_REQUISITION_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Unit_code,TSPL_REQUISITION_HEAD.Dept,TSPL_REQUISITION_HEAD.close_yn "
                    If rdoPonever.IsChecked = True Then
                        'status = " and [PurchaseOrder Qty]=0 and TSPL_REQUISITION_HEAD.close_yn='N'"
                        qry += " having CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)))=0 and TSPL_REQUISITION_HEAD.close_yn='N'"
                    ElseIf rdoPOPartial.IsChecked = True Then
                        'qry += "  and [Balance Qty]<>0 and len(isnull([PO Doc],''))>0 and TSPL_REQUISITION_HEAD.close_yn='N'"
                        qry += " HAVING (CONVERT(decimal(18, 2) " &
                          ", max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)))<>0 and len(MAX(COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '')))>0 and TSPL_REQUISITION_HEAD.close_yn='N')"
                    ElseIf rdoCompleted.IsChecked = True Then
                        qry += " HAVING ((CONVERT(decimal(18, 2) " &
                          ", max(COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)))- CONVERT(decimal(18, 2), sum(COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)))=0 or TSPL_REQUISITION_HEAD.close_yn='Y'))"
                    End If
                End If
                If chkItemWiseSummary.IsChecked = False AndAlso chkLocationWiseItemSummary.IsChecked = False Then
                    qry += " order by [Pending Indent Date]"
                End If

                'Else
                '    qry += " order by convert(date,Requisition_Date,103)"
            End If
            '            If chkItemWiseSummary.IsChecked = True Then
            '                qry = "  select MainQuery.[Item Code] , MainQuery.[Item Desc] , MainQuery.[Unit Code] , MainQuery.[Requisition Qty] , isnull (TBL_Balance.Balance_Qty,0) as [Stock Qty]  from (
            '                        select [Item Code] , max([Item Desc]) as [Item Desc]
            '                        , Target_UOM.UOM_Code as [Unit Code]
            '                        , cast ( ( sum(Source_UOM.Conversion_Factor * [Requisition Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as  [Requisition Qty]
            '                        , cast ( ( sum(Source_UOM.Conversion_Factor * [PurchaseOrder Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as  [PurchaseOrder Qty]
            '                        , cast ( ( sum(Source_UOM.Conversion_Factor * [Requisition Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) - cast ( ( sum(Source_UOM.Conversion_Factor * [PurchaseOrder Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as [Balance Qty]
            '                        from ( " + qry + " ) XXXFinal
            '                        left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = XXXFinal.[Item Code] and Source_UOM.UOM_Code = XXXFinal.[Unit Code]
            '                        left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = XXXFinal.[Item Code] and Target_UOM.Default_UOM=1
            '                        group by XXXFinal.[Item Code] ,Target_UOM.UOM_Code 
            '                        ) as MainQuery left outer join 


            '( select XXXFinal.Item_Code, XXXFinal.Stock_UOM, XXXFinal.Balance_Qty as StockUOM_Balance_Qty ,cast ( ( XXXFinal.Balance_Qty * Source_UOM.Conversion_Factor / Target_UOM.Conversion_Factor) as decimal(18,2)) as Balance_Qty , Target_UOM.UOM_Code from (
            'select Item_Code,Item_Desc,Stock_Qty,Stock_UOM, convert(decimal(18,2),[Balance_Qty]) as [Balance_Qty]
            'from ( select Item_Code,MAX(Item_Desc) as Item_Desc,  SUM(Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty, 
            ' (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, 
            ' SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Balance_Qty] 
            ' from ( select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
            ',VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
            ' union all 
            ' select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
            ') InventroyMovement 
            ' left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
            ' left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
            ' left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
            ' left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
            ' left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC]  from (
            ' select * from ( 
            ' select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
            ' ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
            ' ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
            ' ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
            ' from  TSPL_ITEM_MASTER  
            ' left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
            ' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
            ' where 2=2 
            ' )xx
            ' Pivot 
            ' ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST])
            ' ) Pivt
            ' Pivot 
            ' (
            ' max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC])
            ' ) Pivt1 
            ' ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2 )xxx Group by Item_Type,Item_Group,Item_Code )xxxx
            ' ) XXXFinal 
            ' left outer join TSPL_ITEM_UOM_DETAIL as  Source_UOM on Source_UOM.Item_Code = XXXFinal.Item_Code and XXXFinal.Stock_UOM  = Source_UOM.UOM_Code
            ' left outer join TSPL_ITEM_UOM_DETAIL as  Target_UOM on Target_UOM.Item_Code = XXXFinal.Item_Code and Target_UOM.Default_UOM =1
            ' )  as TBL_Balance  on TBL_Balance.Item_Code = MainQuery.[Item Code] and TBL_Balance.UOM_Code = MainQuery.[Unit Code]  order by MainQuery.[Item Code]
            '                         "
            '            End If

            If chkLocationWiseItemSummary.IsChecked = True OrElse chkItemWiseSummary.IsChecked = True Then
                qry = " select Location, [Location Desc],[Item Code],[Item Desc],[Unit Code],[Requisition Qty], isnull (TBL_Balance.Balance_Qty,0) as [Stock Qty]  from (
                        select Location , max([Location Desc]) as [Location Desc], [Item Code] , max([Item Desc]) as [Item Desc]
                        , Target_UOM.UOM_Code as [Unit Code]
                        , cast ( ( sum(Source_UOM.Conversion_Factor * [Requisition Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as  [Requisition Qty]
                        , cast ( ( sum(Source_UOM.Conversion_Factor * [PurchaseOrder Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as  [PurchaseOrder Qty]
                        , cast ( ( sum(Source_UOM.Conversion_Factor * [Requisition Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) - cast ( ( sum(Source_UOM.Conversion_Factor * [PurchaseOrder Qty] / Target_UOM.Conversion_Factor) ) as decimal(18,2)) as [Balance Qty]
                        from ( " + qry + " ) XXXFinal
                        left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = XXXFinal.[Item Code] and Source_UOM.UOM_Code = XXXFinal.[Unit Code]
                        left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = XXXFinal.[Item Code] and Target_UOM.Default_UOM=1
                        group by Location, XXXFinal.[Item Code] ,Target_UOM.UOM_Code  
                        ) as MainQuery left outer join

( select XXXFinal.Item_Code, XXXFinal.Stock_UOM, XXXFinal.Balance_Qty as StockUOM_Balance_Qty ,cast ( ( XXXFinal.Balance_Qty * Source_UOM.Conversion_Factor / Target_UOM.Conversion_Factor) as decimal(18,2)) as Balance_Qty , Target_UOM.UOM_Code, Location_Code from (
select Item_Code,Item_Desc,Stock_Qty,Stock_UOM, convert(decimal(18,2),[Balance_Qty]) as [Balance_Qty], Location_Code
from ( select  Item_Code,MAX(Item_Desc) as Item_Desc,  SUM(Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty, 
 (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, 
 SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Balance_Qty],Location_Code 
 from ( select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
 union all 
 select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
) InventroyMovement 
 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
 left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
 left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC]  from (
 select * from ( 
 select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
 ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
 ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
 ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
 from  TSPL_ITEM_MASTER  
 left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
 where 2=2 
 )xx
 Pivot 
 ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST])
 ) Pivt
 Pivot 
 (
 max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC])
 ) Pivt1 
 ) xxx group by  Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2 )xxx Group by Location_Code,Item_Code )xxxx
 ) XXXFinal 
 left outer join TSPL_ITEM_UOM_DETAIL as  Source_UOM on Source_UOM.Item_Code = XXXFinal.Item_Code and XXXFinal.Stock_UOM  = Source_UOM.UOM_Code
 left outer join TSPL_ITEM_UOM_DETAIL as  Target_UOM on Target_UOM.Item_Code = XXXFinal.Item_Code and Target_UOM.Default_UOM =1
 )  as TBL_Balance  on TBL_Balance.Item_Code = MainQuery.[Item Code] and TBL_Balance.UOM_Code = MainQuery.[Unit Code]  and  TBL_Balance.Location_Code = MainQuery.Location                         

                        "
                If chkLocationWiseItemSummary.IsChecked = True Then
                    qry += " order by MainQuery.Location,  MainQuery.[Item Code] "
                End If
                If chkItemWiseSummary.IsChecked = True Then
                    qry = " select TTT.[Item Code] , max(TTT.[Item Desc]) as [Item Desc] ,TTT.[Unit Code], sum(TTT.[Requisition Qty])  as [Requisition Qty] , sum(TTT.[Stock Qty]) as  [Stock Qty] from ( " + qry + "  ) TTT group by TTT.[Item Code] , TTT.[Unit Code]  order by TTT.[Item Code] "
                End If
            End If
            'order by Location, XXXFinal.[Item Code]
            'Dim dt As New DataTable
            dt.Rows.Clear()
            dt.Columns.Clear()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If dt.Columns.Contains("close_yn") Then
                    dt.Columns.Remove("close_yn")
                End If
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                For Each col As GridViewColumn In gv.Columns
                    col.Width = 150
                    col.ReadOnly = True
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                If dt.Columns.Contains("Requisition Qty") Then
                    Dim item5 As New GridViewSummaryItem("Requisition Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                End If

                If dt.Columns.Contains("PurchaseOrder Qty") Then
                    Dim item4 As New GridViewSummaryItem("PurchaseOrder Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                End If

                If dt.Columns.Contains("Balance Qty") Then
                    Dim item6 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item6)
                End If


                If dt.Columns.Contains("Stock Qty") Then
                    Dim item7 As New GridViewSummaryItem("Stock Qty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item7)
                End If

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox2.Enabled = False
            Else
                clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Sub Load_Report()
        If dtpfromdate.Value > dtpTodate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater than to Date", Me.Text)
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

        'sQuery = " select coalesce(TSPL_REQUISITION_HEAD.Dept,'') as Dept, coalesce(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,'') as [Department Desc] ,  coalesce(convert(varchar,TSPL_REQUISITION_HEAD.created_date,103),'') AS [Indent Received Date], (case when TSPL_REQUISITION_HEAD.Status = 1 then coalesce(convert(varchar,TSPL_REQUISITION_HEAD.Posting_Date,103),'')  else '' end )  as [Indent Completion Date] , (Case when TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id then 'Yes' else 'No' end ) as [Status (PI tagged with PO)] , case TSPL_REQUISITION_HEAD.Is_Internal when 'Y' then 'Store requition' when 'N' then 'Purchase indent' end as IndentType,TSPL_REQUISITION_HEAD.Requisition_Id as ReqNo,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as  Requisition_Date,TSPL_REQUISITION_DETAIL.Item_Code,tspl_item_master.Item_Desc ,TSPL_REQUISITION_DETAIL.Requisition_Qty as ReqQty,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty ,TSPL_PURCHASE_ORDER_DETAIL.Item_Code ,TSPL_SRN_head.SRN_No ,TSPL_SRN_head.SRN_Date ,TSPL_SRN_DETAIL.SRN_Qty, TSPL_SRN_DETAIL.Item_Code,TSPL_REQUISITION_DETAIL.Vendor_Code , (SRN_Qty-PurchaseOrder_Qty)as BalanceQty,TSPL_REQUISITION_HEAD.Location ,case   TSPL_REQUISITION_HEAD.close_yn when 'Y' then 'Closed' end as Status, TSPL_REQUISITION_HEAD.Created_By , case when TSPL_REQUISITION_HEAD.Status =1 then TSPL_REQUISITION_HEAD.Modify_By else '' end as [Approved By],  case when  TSPL_REQUISITION_HEAD.Status =1 then 'Approved' else 'Unapproved' end  as [Approved Status]   from TSPL_REQUISITION_DETAIL "

        sQuery += " SELECT COALESCE(TSPL_REQUISITION_HEAD.Dept, '') AS [Department], COALESCE(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, '') AS [Department Desc], COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '') AS [Requisition Doc], CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103) AS [Requisition Date], COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '') AS [Indent Received Date], (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '') ELSE '' END) AS [Indent Completion Date], (CASE TSPL_REQUISITION_HEAD.Is_Internal WHEN 'Y' THEN 'Store Requisition' WHEN 'N' THEN 'Purchase Indent' END) AS [Indent Type], COALESCE(TSPL_REQUISITION_DETAIL.Item_Code, '') AS [Item (PI)], COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc (PI)], COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '') AS [PO Doc], COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '') AS [PO Date], COALESCE(TSPL_PURCHASE_ORDER_DETAIL.Item_Code, '') AS [Item (PO)], COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc (PO)], (CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes' ELSE 'No' END) AS [Status (PI tagged with PO)], /* COALESCE(TSPL_SRN_head.SRN_No, '') AS [SRN No], COALESCE(TSPL_SRN_head.SRN_Date, '') AS [SRN Date], COALESCE(TSPL_SRN_DETAIL.Item_Code, '') AS [Item (SRN)], COALESCE(tspl_item_master.Item_Desc, '') AS [Item Desc (SRN)],*/ COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '') AS [Vendor Code], COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME, '') AS [Vendor Name], CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) AS [Req Qty], CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) AS [PO Qty], CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0))) AS [Balance Qty], ( CASE WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0  AND  CONVERT(decimal(18, 2), COALESCE (TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0  THEN 'PO Never Created'  WHEN (CONVERT(decimal(18, 2), (COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)))) = 0 THEN 'Completed'   WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) = 0 THEN 'PO Never Created' WHEN CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) > 0  AND  CONVERT(decimal(18, 2), COALESCE (TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) > 0  AND CONVERT(decimal(18, 2), COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, 0)) >   CONVERT(decimal(18, 2), COALESCE (TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0)) THEN 'Partial'  END ) AS [Status] , /* CONVERT(decimal(18, 2),  COALESCE(TSPL_SRN_DETAIL.SRN_Qty, 0)) AS [SRN Qty],*/ COALESCE(TSPL_REQUISITION_HEAD.Location, '') AS Location, COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '') AS [Location Desc], COALESCE(CASE TSPL_REQUISITION_HEAD.close_yn WHEN 'Y' THEN 'Closed' END, '') AS [Status], COALESCE(TSPL_REQUISITION_HEAD.Created_By, '') AS [Created By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Modify_By ELSE '' END, '') AS [Approved By], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN TSPL_REQUISITION_HEAD.Posting_Date ELSE '' END, '') AS [Approved Date], COALESCE(CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN 'Approved' ELSE 'Unapproved' END, '') AS [Approved Status] "
        sQuery += " from TSPL_REQUISITION_DETAIL left join TSPL_REQUISITION_HEAD  on TSPL_REQUISITION_HEAD .Requisition_Id =TSPL_REQUISITION_DETAIL .Requisition_Id "
        sQuery += " left join TSPL_PURCHASE_ORDER_DETAIL  on TSPL_PURCHASE_ORDER_DETAIL .Requisition_Id  =TSPL_REQUISITION_HEAD.Requisition_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code =TSPL_REQUISITION_DETAIL.Item_Code "
        sQuery += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "

        ' SRN's Linking was get it remove by prashant Sir on 27-03-2017
        'sQuery += " left join TSPL_SRN_detail on TSPL_SRN_detail.PO_ID = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  and TSPL_SRN_detail.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code "
        'sQuery += " left join TSPL_SRN_head on TSPL_SRN_HEAD .SRN_No =TSPL_SRN_detail.SRN_No "

        sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_REQUISITION_DETAIL.Vendor_Code"
        sQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_REQUISITION_HEAD.Location"
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code = TSPL_REQUISITION_DETAIL.Item_Code LEFT JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_REQUISITION_HEAD.Dept "
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
        'If IndentTypeBoth.IsChecked Then
        '    ' sQuery += " and TSPL_REQUISITION_HEAD.Is_Internal='Y' and TSPL_REQUISITION_HEAD.Is_Internal='N'"
        'End If
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
            clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
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

        Try
            gv.TableElement.TableHeaderHeight = 40
            gv.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = False
            Next

            gv.Columns("Department").IsVisible = True
            gv.Columns("Department").Width = 90
            gv.Columns("Department").HeaderText = "Department"

            gv.Columns("Department Desc").IsVisible = True
            gv.Columns("Department Desc").Width = 90
            gv.Columns("Department Desc").HeaderText = "Department Desc"


            gv.Columns("Indent Received Date").IsVisible = True
            gv.Columns("Indent Received Date").Width = 90
            gv.Columns("Indent Received Date").HeaderText = "Indent Received Date"


            gv.Columns("Indent Completion Date").IsVisible = True
            gv.Columns("Indent Completion Date").Width = 90
            gv.Columns("Indent Completion Date").HeaderText = "Indent Completion Date"


            gv.Columns("Status (PI tagged with PO)").IsVisible = True
            gv.Columns("Status (PI tagged with PO)").Width = 90
            gv.Columns("Status (PI tagged with PO)").HeaderText = "Status (PI tagged with PO)"

            gv.Columns("Indent Type").IsVisible = True
            gv.Columns("Indent Type").Width = 90
            gv.Columns("Indent Type").HeaderText = "Indent Type"

            gv.Columns("Item (PI)").IsVisible = True
            gv.Columns("Item (PI)").Width = 150
            gv.Columns("Item (PI)").HeaderText = "Item (PI)"

            gv.Columns("Item Desc (PI)").IsVisible = True
            gv.Columns("Item Desc (PI)").Width = 150
            gv.Columns("Item Desc (PI)").HeaderText = "Item Desc (PI)"

            gv.Columns("Requisition Doc").IsVisible = True
            gv.Columns("Requisition Doc").Width = 150
            gv.Columns("Requisition Doc").HeaderText = "Indent No."

            gv.Columns("Requisition Date").IsVisible = True
            gv.Columns("Requisition Date").Width = 150
            gv.Columns("Requisition Date").HeaderText = "Indent Date"
            gv.Columns("Requisition Date").FormatString = "{0:d}"

            gv.Columns("Req Qty").IsVisible = True
            gv.Columns("Req Qty").Width = 90
            gv.Columns("Req Qty").HeaderText = "Indent Qty"

            gv.Columns("PO Doc").IsVisible = True
            gv.Columns("PO Doc").Width = 150
            gv.Columns("PO Doc").HeaderText = " PO No."

            gv.Columns("PO Date").IsVisible = True
            gv.Columns("PO Date").Width = 150
            gv.Columns("PO Date").HeaderText = "PO Date"
            gv.Columns("PO Date").FormatString = "{0:d}"

            gv.Columns("PO Qty").IsVisible = True
            gv.Columns("PO Qty").Width = 90
            gv.Columns("PO Qty").HeaderText = "PO Qty"

            'gv.Columns("SRN No").IsVisible = True
            'gv.Columns("SRN No").Width = 150
            'gv.Columns("SRN No").HeaderText = "SRN No."

            'gv.Columns("SRN Qty").IsVisible = True
            'gv.Columns("SRN Qty").Width = 90
            'gv.Columns("SRN Qty").HeaderText = "Received Qty"


            gv.Columns("Balance Qty").IsVisible = True
            gv.Columns("Balance Qty").Width = 90
            gv.Columns("Balance Qty").HeaderText = "Balance Qty"

            gv.Columns("Created By").IsVisible = True
            gv.Columns("Created By").Width = 150
            gv.Columns("Created By").HeaderText = "Created By"

            gv.Columns("Approved By").IsVisible = True
            gv.Columns("Approved By").Width = 150
            gv.Columns("Approved By").HeaderText = "Approved By"

            gv.Columns("Approved Date").IsVisible = True
            gv.Columns("Approved Date").Width = 150
            gv.Columns("Approved Date").HeaderText = "Approved Date"

            gv.Columns("Approved Status").IsVisible = True
            gv.Columns("Approved Status").Width = 150
            gv.Columns("Approved Status").HeaderText = "Approved Status"

            If rdobtnall.IsChecked = True Then
                gv.Columns("Status").IsVisible = True
                gv.Columns("Status").Width = 90
                gv.Columns("Status").HeaderText = "Status"
            End If

            If IndentTypeStore.IsChecked = True Then
                gv.Columns("Requisition Doc").IsVisible = True
                gv.Columns("Requisition Doc").Width = 150
                gv.Columns("Requisition Doc").HeaderText = "Store Requisition"
            End If
            For Each column As GridViewColumn In gv.Columns
                column.Width = 150
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item4 As New GridViewSummaryItem("Req Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("PO Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            'Dim item6 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ''Dim PIDocCount As New GridViewSummaryItem("Requisition Doc", "{0:F2}", GridAggregateFunction.Count)
            ' gv.MasterTemplate.SummaryRowsBottom.Add(PIDocCount)


            RadPageView1.SelectedPage = RadPageViewPage2
            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            If cbxSummary.Checked = True Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                Load_Monthly_Summaries(objCommonVar.CurrentCompanyCode)
            ElseIf IndentTypePurchase.IsChecked Then
                Load_PendingIndent_Report()
            ElseIf IndentTypeStore.IsChecked Then
                Load_StoreRequistion_Report()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingRequisitionQty & "'"))
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

            If (gv.Rows.Count > 0) Then

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
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Pending Indent Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                gv.DataSource = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub LoadDocuemntNo()
        Dim qry As String = "select Requisition_Id as Code,description as [Description] from TSPL_REQUISITION_HEAD "
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
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
        Dim qry As String = " select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)

    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    'kunal
    'Public Sub Load_Report(ByVal companyCode As String, ByVal month As String, ByVal year As String, ByVal docType As String)
    '    Dim Status As String = ""
    '    Dim joinsQry As String = Nothing
    '    Dim outterQry As String = Nothing
    '    Dim innerQry As String = Nothing
    '    Dim groupCols As String = Nothing
    '    Dim whereQry As String = Nothing
    '    Dim groupByQry As String = Nothing
    '    Dim orderByQry As String = Nothing
    '    Try
    '        If dtpfromdate.Value > dtpTodate.Value Then
    '            common.clsCommon.MyMessageBoxShow("From date cannot be greater than To date", Me.Text)
    '            dtpfromdate.Focus()
    '            Exit Sub
    '        End If

    '        toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
    '        fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
    '        toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")


    '        If Not rdobtnall.IsChecked Then
    '            If rdoPonever.IsChecked = True Then
    '                Status = " and len(isnull(fnlQry.PurchaseOrder_No,'')) <=0 and fnlQry.Status='N'   "
    '            End If
    '            If rdoPOPartial.IsChecked = True Then
    '                Status = " and len(isnull(fnlQry.ReqNo,''))>0 and (fnlQry.ReqQty-fnlQry.PurchaseOrder_Qty)<>0  and fnlQry.[Status]='N' "
    '            End If
    '            If rdoCompleted.IsChecked = True Then
    '                Status = " and len(isnull(fnlQry.ReqNo,''))>0 and (fnlQry.ReqQty-fnlQry.PurchaseOrder_Qty)=0 and fnlQry.[STATUS]='N'  "
    '            End If
    '        End If
    '        ' MAIN QUERY ========================================================================================================================================================================================================================================================================

    '        outterQry = "SELECT " & _
    '                      " (fnlQry.Dept) AS [Dept] ,  " & _
    '                      " MAX(fnlQry.[Department Desc]) AS [Department Desc] , " & _
    '                      " MAX(fnlQry.Vendor_Code) AS [Vendor_Code] , " & _
    '                      " MAX(FNLQRY.Vendor_DESC) AS [Vendor_Name], " & _
    '                      " MAX(fnlQry.Location) AS [Location] , " & _
    '                      " MAX(fnlQry.Location_Desc) AS [Location_Desc] , "

    '        If clsCommon.CompairString(docType, "Indents Received") = CompairStringResult.Equal Then
    '            outterQry += " (fnlQry.[TSPL_REQUISITION_DETAIL.Item_Code]) AS [TSPL_REQUISITION_DETAIL.Item_Code] , "
    '        Else
    '            outterQry += " MAX(fnlQry.[TSPL_REQUISITION_DETAIL.Item_Code]) AS [TSPL_REQUISITION_DETAIL.Item_Code] , "
    '        End If

    '        outterQry += " MAX(fnlQry.Item_Desc) AS [Item_Desc] ,  " & _
    '                      " MAX(fnlQry.[IndentType]) AS [IndentType] ,  " & _
    '                      " (fnlQry.ReqNo) AS [Req No] ,  " & _
    '                      " MAX(fnlQry.Requisition_Date) AS [Requisition_Date] ,  " & _
    '                      " MAX(fnlQry.[Indent Received Date]) AS [Indent Received Date] ,  " & _
    '                      " MAX(fnlQry.[Indent Completion Date]) AS [Indent Completion Date] ,  " & _
    '                      " MAX(fnlQry.[Status (PI tagged with PO)]) AS [Status (PI tagged with PO)] ,  "

    '        If clsCommon.CompairString(docType, "Indents Processed") = CompairStringResult.Equal Then
    '            outterQry += " (fnlQry.[TSPL_PURCHASE_ORDER_DETAIL.Item_Code]) AS [TSPL_PURCHASE_ORDER_DETAIL.Item_Code] ,  "
    '        Else
    '            outterQry += " MAX(fnlQry.[TSPL_PURCHASE_ORDER_DETAIL.Item_Code]) AS [TSPL_PURCHASE_ORDER_DETAIL.Item_Code] ,  "
    '        End If

    '        outterQry += " MAX(fnlQry.PurchaseOrder_No) AS [PurchaseOrder_No] ,  " & _
    '                      " MAX(fnlQry.PurchaseOrder_Date) AS [PurchaseOrder_Date] ,  " & _
    '                      " MAX(fnlQry.[SPL_SRN_DETAIL.Item_Code]) AS [SPL_SRN_DETAIL.Item_Code] ,  " & _
    '                      " MAX(fnlQry.SRN_No) AS [SRN_No] ,  " & _
    '                      " MAX(fnlQry.SRN_Date) AS [SRN_Date] ,  " & _
    '                      " SUM(CONVERT(DECIMAL(18,2),fnlQry.ReqQty)) AS [Req_Qty] ,  " & _
    '                      " SUM(CONVERT(DECIMAL(18,2),fnlQry.SRN_Qty)) AS [SRN_Qty] ,  " & _
    '                      " SUM(CONVERT(DECIMAL(18,2),fnlQry.PurchaseOrder_Qty)) AS [PO_Qty] ,  " & _
    '                      " SUM(CONVERT(DECIMAL(18,2),fnlQry.BalanceQty)) AS [Bal_Qty] ,  " & _
    '                      " SUM(CONVERT(DECIMAL(18,2),fnlQry.BalanceQty2)) AS [Bal_Qty2] ,  " & _
    '                      " MAX(fnlQry.[Status]) AS [Status] ,  " & _
    '                      " MAX(fnlQry.Created_By) AS [Created_By],  " & _
    '                      " MAX(fnlQry.[Approved By]) AS [Approved By] ,  " & _
    '                      " MAX(fnlQry.[Approved Status]) AS [Approved Status] ,  "

    '        ' IF GV DOUBLE CLICK ON PI DOCS THEN THIS
    '        If clsCommon.CompairString(docType, "Indents Received") = CompairStringResult.Equal Then
    '            groupCols = " (fnlQry.[Req_Month]) AS [Req Month] , " & _
    '                        " (fnlQry.[Req_Year]) AS [Req Year] ," & _
    '                        " MAX(fnlQry.[PO Month]) AS [PO Month] ," & _
    '                        " MAX(fnlQry.[PO Year]) AS [PO Year] "

    '            ' IF GV DOUBLE CLICK ON PO DOCS THEN THIS
    '        ElseIf clsCommon.CompairString(docType, "Indents Processed") = CompairStringResult.Equal Then
    '            groupCols = " MAX(fnlQry.[Req_Month]) AS [Req Month] , " & _
    '                        " MAX(fnlQry.[Req_Year]) AS [Req Year] ," & _
    '                        " (fnlQry.[PO Month]) AS [PO Month] ," & _
    '                        " (fnlQry.[PO Year]) AS [PO Year] "
    '        End If

    '        outterQry += groupCols

    '        ' INNER QUERY FOR OUTTER QUERY  =============================================================================================================================================================
    '        innerQry = "  FROM ( SELECT" & _
    '                      " COALESCE(TSPL_REQUISITION_HEAD.Dept, '') AS Dept , " & _
    '                      " COALESCE(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, '') AS [Department Desc] , " & _
    '                      " COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.created_date, 103), '') AS [Indent Received Date] , " & _
    '                      " (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(CONVERT(varchar, TSPL_REQUISITION_HEAD.Posting_Date, 103), '')  ELSE ''  END) AS [Indent Completion Date] , " & _
    '                      " (CASE WHEN TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id THEN 'Yes'ELSE 'No'END) AS [Status (PI tagged with PO)] , " & _
    '                      " (CASE TSPL_REQUISITION_HEAD.Is_Internal WHEN 'Y' THEN 'Store Requisition'WHEN 'N' THEN 'Purchase indent' END) AS [IndentType] , " & _
    '                      " TSPL_REQUISITION_HEAD.Requisition_Id AS ReqNo , " & _
    '                      " CONVERT(varchar, TSPL_REQUISITION_HEAD.Requisition_Date, 103) AS Requisition_Date , " & _
    '                      " TSPL_REQUISITION_DETAIL.Item_Code AS [TSPL_REQUISITION_DETAIL.Item_Code] , " & _
    '                      " tspl_item_master.Item_Desc AS Item_Desc , " & _
    '                      " COALESCE(TSPL_REQUISITION_DETAIL.Requisition_Qty, '') AS ReqQty , " & _
    '                      " COALESCE(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, '') AS PurchaseOrder_No , " & _
    '                      " COALESCE(CONVERT(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, 103), '') AS PurchaseOrder_Date , " & _
    '                      " COALESCE(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty, 0) AS PurchaseOrder_Qty , " & _
    '                      " COALESCE(TSPL_PURCHASE_ORDER_DETAIL.Item_Code, '') AS [TSPL_PURCHASE_ORDER_DETAIL.Item_Code] , " & _
    '                      " COALESCE(TSPL_SRN_head.SRN_No, '') AS SRN_No , " & _
    '                      " COALESCE(CONVERT(varchar, TSPL_SRN_head.SRN_Date,103), '') AS SRN_Date , " & _
    '                      " COALESCE(TSPL_SRN_DETAIL.SRN_Qty, 0) AS SRN_Qty , " & _
    '                      " COALESCE(TSPL_SRN_DETAIL.Item_Code, '') AS [SPL_SRN_DETAIL.Item_Code] , " & _
    '                      " COALESCE(TSPL_REQUISITION_DETAIL.Vendor_Code, '') AS Vendor_Code , " & _
    '                      " COALESCE(TSPL_VENDOR_MASTER.VENDOR_NAME , '') AS Vendor_DESC , " & _
    '                      " COALESCE(SRN_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)     AS BalanceQty , " & _
    '                      " COALESCE(Requisition_Qty, 0) - COALESCE(PurchaseOrder_Qty, 0)   AS BalanceQty2 , " & _
    '                      " COALESCE(TSPL_REQUISITION_HEAD.Location, '') AS Location , " & _
    '                      " COALESCE(TSPL_lOCATION_MASTER.LOCATION_DESC, '') AS Location_Desc,  " & _
    '                      " COALESCE(CASE (TSPL_REQUISITION_HEAD.close_yn) WHEN 'Y' THEN 'Closed' END, '') AS [Status] , " & _
    '                      " COALESCE(TSPL_REQUISITION_HEAD.Created_By, '') AS Created_By , " & _
    '                      " (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN COALESCE(TSPL_REQUISITION_HEAD.Modify_By, '')  ELSE '' END) AS [Approved By] , " & _
    '                      " (CASE WHEN TSPL_REQUISITION_HEAD.Status = 1 THEN 'Approved' ELSE 'Unapproved' END) AS [Approved Status] , " & _
    '                      " COALESCE(LEFT(DATENAME(mm, TSPL_REQUISITION_HEAD.Posting_Date), 3), '') AS [Req_Month] , " & _
    '                      " COALESCE(LEFT(DATENAME(YEAR, TSPL_REQUISITION_HEAD.Posting_Date), 103), '') AS [Req_Year] , " & _
    '                      " COALESCE(CONVERT(varchar, DATENAME(mm, TSPL_PURCHASE_ORDER_HEAD.Posting_Date), 3), '') AS [PO Month] , " & _
    '                      " COALESCE(CONVERT(varchar, DATENAME(YEAR, TSPL_PURCHASE_ORDER_HEAD.Posting_Date), 103), '') AS [PO Year]"

    '        ' JOINS OF TABLES FOR INNER QUERY FOR OUTTER QUERY  =============================================================================================================================================================           
    '        joinsQry = " FROM TSPL_REQUISITION_DETAIL " & _
    '                   " LEFT JOIN TSPL_REQUISITION_HEAD" & _
    '                   "   ON TSPL_REQUISITION_HEAD.Requisition_Id = TSPL_REQUISITION_DETAIL.Requisition_Id" & _
    '                   " LEFT JOIN TSPL_PURCHASE_ORDER_DETAIL" & _
    '                   "   ON TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id" & _
    '                   "   AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code = TSPL_REQUISITION_DETAIL.Item_Code" & _
    '                   " LEFT JOIN TSPL_PURCHASE_ORDER_HEAD" & _
    '                   "   ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No" & _
    '                   " LEFT JOIN TSPL_SRN_detail" & _
    '                   "   ON TSPL_SRN_detail.PO_ID = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No" & _
    '                   "   AND TSPL_SRN_detail.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code" & _
    '                   " LEFT JOIN TSPL_SRN_head" & _
    '                   "   ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_detail.SRN_No" & _
    '                   " LEFT OUTER JOIN TSPL_VENDOR_MASTER" & _
    '                   "   ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_REQUISITION_DETAIL.Vendor_Code" & _
    '                   " LEFT OUTER JOIN TSPL_LOCATION_MASTER" & _
    '                   "   ON TSPL_LOCATION_MASTER.Location_Code = TSPL_REQUISITION_HEAD.Location" & _
    '                   " LEFT JOIN tspl_item_master" & _
    '                   "   ON tspl_item_master.Item_Code = TSPL_REQUISITION_DETAIL.Item_Code" & _
    '                   " LEFT JOIN TSPL_DEPARTMENT_MASTER" & _
    '                   "   ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_REQUISITION_HEAD.Dept"

    '        ' FILTERS FOR OUTTER QUERY IN WHERE CONDITIONS =======================================================================================================================================================
    '        whereQry = " ) AS fnlQry " & _
    '                      " WHERE 1=1 and fnlQry.Requisition_Date BETWEEN '" + clsCommon.myCDate(fromDate, "dd/MM/yyyy") + "' AND '" + clsCommon.myCDate(toDate, "dd/MM/yyyy") + "'" + Status



    '        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
    '            whereQry += "AND fnlQry.ReqNo IN ( " + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '            whereQry += " and  fnlQry.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
    '            whereQry += " and fnlQry.Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
    '            whereQry += " and fnlQry.[TSPL_REQUISITION_DETAIL.Item_Code]  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        If IndentTypePurchase.IsChecked Then
    '            whereQry += " and fnlQry.[IndentType]='N'"
    '        End If
    '        If IndentTypeStore.IsChecked Then
    '            whereQry += " and fnlQry.[IndentType]='Y'"
    '        End If

    '        ' GROUPING ON OUTTER QUERY RESULTS WITH OUTER QUERY COLUMNS =============================================================================================================================================================           


    '        If clsCommon.CompairString(docType, "Indents Received") = CompairStringResult.Equal Then
    '            groupByQry = " GROUP BY fnlQry.[TSPL_REQUISITION_DETAIL.Item_Code], " & _
    '                                    " fnlQry.Dept, " & _
    '                                    " fnlQry.ReqNo," & _
    '                                    " fnlQry.Req_Month," & _
    '                                    " fnlQry.Req_Year"

    '        ElseIf clsCommon.CompairString(docType, "Indents Processed") = CompairStringResult.Equal Then
    '            groupByQry = "GROUP BY fnlQry.[TSPL_PURCHASE_ORDER_DETAIL.Item_Code], " & _
    '                                    " fnlQry.[PurchaseOrder_Date] ," & _
    '                                    " fnlQry.Dept ," & _
    '                                    " fnlQry.ReqNo ," & _
    '                                    " fnlQry.[PO Month] ," & _
    '                                    " fnlQry.[PO Year]"
    '        End If

    '        ' SORTING OF OUTTER QUERY RESULT BY OUTTER QUERY COLOUMNS =============================================================================================================================================================           

    '        If clsCommon.CompairString(docType, "Indents Received") = CompairStringResult.Equal Then
    '            orderByQry = " ORDER BY " & _
    '                               " Requisition_Date, Dept , Req_Month, Req_Year "
    '        ElseIf clsCommon.CompairString(docType, "Indents Processed") = CompairStringResult.Equal Then
    '            orderByQry = " ORDER BY " & _
    '                                    " fnlQry.PurchaseOrder_Date, fnlQry.Dept , fnlQry.[PO Month], fnlQry.[PO Year] "
    '        End If


    '        ' MERGING ALL QUERIES TO MAKE SINGLE QUERY FOR DATA-TABLE =============================================================================================================================================================           
    '        sQuery = outterQry + innerQry + joinsQry + whereQry + groupByQry + orderByQry

    '        ' GET RESULT IN DATATABLE AND DISPLAY ON GRID-VIEW =============================================================================================================================================================           
    '        Dim dtgv As New DataTable
    '        dtgv = clsDBFuncationality.GetDataTable(sQuery)
    '        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
    '            gv.DataSource = Nothing
    '            gv.Rows.Clear()
    '            gv.Columns.Clear()
    '            gv.DataSource = dtgv
    '            gv.GroupDescriptors.Clear()
    '            gv.MasterTemplate.SummaryRowsBottom.Clear()
    '            'FormatGrid()
    '            For Each col As GridViewColumn In gv.Columns
    '                col.Width = 150
    '                col.ReadOnly = True
    '            Next

    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            RadGroupBox2.Enabled = False
    '        Else
    '            clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
    '        End If
    '        ReStoreGridLayout()
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub
    'kunal
    Private Sub Load_Monthly_Summaries(ByVal companyCode As String)

        Dim qry As String = Nothing
        'Dim dt As DataTable = Nothing
        Try
            toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")


            If cbxSummary.Checked AndAlso IndentTypePurchase.Checked Then

                qry = " SELECT FF.* FROM ( SELECT  Req_Year AS [Year], Req_Month AS [Month], COUNT([Indents Received]) AS [Indents Received]," &
                      " COUNT([Po Processed]) AS [Indents Processed], " &
                      " ((COUNT([Indents Received]) - COUNT([Po Processed])) * 100 / (COUNT([Indents Received])))  AS [Pending Indents %] ,  max(DATEPART(mm, DDD)) as MonthNum  " &
                      " FROM (SELECT " &
                      " TSPL_REQUISITION_HEAD.Requisition_Date AS DDD, TSPL_REQUISITION_HEAD.Requisition_Id AS Req_Document, " &
                      " (TSPL_REQUISITION_HEAD.Requisition_Id) AS [Indents Received], " &
                      " DATENAME(YEAR, TSPL_REQUISITION_HEAD.Requisition_Date) AS [Req_Year], " &
                      " LEFT(DATENAME(mm, TSPL_REQUISITION_HEAD.Requisition_Date), 3) AS [Req_Month], " &
                      " COALESCE(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id, '') AS Document, " &
                      " (TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id) AS [Po Processed], " &
                      " CONVERT(varchar, COALESCE(DATENAME(YEAR, TSPL_PURCHASE_ORDER_HEAD.Posting_Date), ''), 103) AS [PO Year], " &
                      " LEFT(DATENAME(mm, TSPL_PURCHASE_ORDER_HEAD.Posting_Date), 3) AS [Po Month] " &
                                "FROM TSPL_REQUISITION_HEAD" &
                      " LEFT JOIN TSPL_PURCHASE_ORDER_DETAIL" &
                      " ON COALESCE(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id, '') = COALESCE(TSPL_REQUISITION_HEAD.Requisition_Id, '')" &
                      " LEFT JOIN TSPL_PURCHASE_ORDER_HEAD" &
                      " ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No" &
                                " WHERE 1 = 1  " &
                      " AND TSPL_REQUISITION_HEAD.Requisition_Date " &
                      " between '" + clsCommon.GetPrintDate(fromDate, "MM/dd/yyyy") + "' AND '" + clsCommon.GetPrintDate(toDate, "MM/dd/yyyy") + "'" &
                      " AND TSPL_REQUISITION_HEAD.Status = 1)  AS pp  " &
                      " where 1=1 " &
                     "  GROUP BY Req_Year , " &
                             " Req_Month ) AS ff ORDER BY  ff.Year , MonthNum   "
                ' "  ORDER BY  Req_Year ,  Req_Month desc "
                ''" [PO Year], " & _
                ''" [Po Month] "

            ElseIf IndentTypeStore.IsChecked Then
                qry = "SELECT fn.[In Year] AS [Year], fn.[In Month] AS [Month], fn.[Issue Doc_No] AS [Total Issues], fn.[Return Doc No] AS [Total Returns], (((fn.[Issue Doc_No] - fn.[Return Doc No]) * 100) / fn.[Issue Doc_No]) AS [Balance %] FROM (SELECT SUM(ff.[Issue Doc_No]) AS [Issue Doc_No], SUM(ff.[Return Doc No]) AS [Return Doc No], ([In Year]) AS [In Year], ([In Month]) AS [In Month], MAX(dd) AS MonthInNum FROM (SELECT COUNT(IH.Doc_No) AS [Issue Doc_No], MAX('') AS [Return Doc No], COALESCE(DATENAME(YEAR, IH.posting_date), '') AS [In Year], MAX(DATEPART(mm, ih.Posting_Date)) AS dd, COALESCE(LEFT(DATENAME(mm, ih.Posting_Date), 3), '') AS [In Month], MAX(COALESCE(ih.Doc_Type, '')) AS Doc_Type FROM TSPL_REQUISITION_HEAD RH LEFT JOIN TSPL_REQUISITION_DETAIL RD ON RH.Requisition_Id = RD.Requisition_Id LEFT JOIN TSPL_IssueReturn_HEAD IH ON IH.Req_IssueNo = RD.Requisition_Id AND IH.Doc_Type = 'Issue' WHERE 1 = 1 AND CONVERT(date, IH.posting_date, 103) BETWEEN '" + clsCommon.GetPrintDate(fromDate, "MM/dd/yyyy") + "' AND '" + clsCommon.GetPrintDate(toDate, "MM/dd/yyyy") + "' GROUP BY Rh.Requisition_Id, COALESCE(DATENAME(YEAR, IH.posting_date), ''), COALESCE(LEFT(DATENAME(mm, ih.Posting_Date), 3), '') UNION SELECT MAX('') AS [Issue Doc_No], COUNT(IH.Doc_No) AS [Return Doc No], COALESCE(DATENAME(YEAR, IH.posting_date), '') AS [In Year], MAX(DATEPART(mm, ih.Posting_Date)) AS dd, COALESCE(LEFT(DATENAME(mm, ih.Posting_Date), 3), '') AS [In Month], MAX(COALESCE(ih.Doc_Type, '')) AS Doc_Type FROM TSPL_REQUISITION_HEAD RH LEFT JOIN TSPL_REQUISITION_DETAIL RD ON RH.Requisition_Id = RD.Requisition_Id LEFT JOIN TSPL_IssueReturn_HEAD IH ON IH.RequisitionNo = RD.Requisition_Id AND IH.Doc_Type = 'Return' WHERE 1 = 1 AND CONVERT(date, IH.posting_date, 103) BETWEEN '" + clsCommon.GetPrintDate(fromDate, "MM/dd/yyyy") + "' AND '" + clsCommon.GetPrintDate(toDate, "MM/dd/yyyy") + "' GROUP BY RH.Requisition_Id, COALESCE(DATENAME(YEAR, IH.posting_date), ''), COALESCE(LEFT(DATENAME(mm, ih.Posting_Date), 3), '')) AS ff GROUP BY FF.[In Year], ff.[In Month], ff.dd) AS Fn" &
                        " ORDER BY FN.[In Year], fn.MonthInNum"

            End If

            dt.Rows.Clear()
            dt.Columns.Clear()

            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
                    btnBack.Visible = True
                End If

                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormatOfGrid()
                RadPageView1.SelectedPage = Me.RadPageView1.Pages("RadPageViewPage2")
            Else
                btnBack.Visible = False
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub SetGridFormatOfGrid()
        Try
            If cbxSummary.Checked Then
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If
            '--------------------------------------------------
            gv.TableElement.TableHeaderHeight = 45
            gv.MasterTemplate.ShowRowHeaderColumn = True
            '--------------------------------------------------
            For Each col As GridViewColumn In gv.Columns
                col.Width = 120
                col.ReadOnly = True
                If col.Name = "MonthNum" Then
                    col.IsVisible = False
                End If
            Next
            '--------------------------------------------------

            Dim summaryRowItem As New GridViewSummaryRowItem()
            If IndentTypeStore.Checked Then
                Dim intCount As Integer = 0
                Dim item1 As New GridViewSummaryItem("Total Issues", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Total Returns", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Balance %", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
            Else
                Dim intCount As Integer = 0
                Dim item1 As New GridViewSummaryItem("Indents Received", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Indents Processed", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Pending Indents %", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)

            End If

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ReStoreGridLayout()

            If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
            Else
                For Each col As GridViewColumn In gv.Columns
                    If col.Name = "PI Doc" Then
                        col.IsVisible = False
                    End If
                    If col.Name = "PO DOCs" Then
                        col.IsVisible = False
                    End If
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub FillFiscalYear()
        Try
            'Dim qry As String = " select min(datepart(year,TSPL_REQUISITION_HEAD.Requisition_Date)) -1 as FiscalYear from TSPL_REQUISITION_HEAD Union all  select distinct datepart(year,TSPL_REQUISITION_HEAD.Requisition_Date) as FiscalYear from TSPL_REQUISITION_HEAD "
            Dim qry As String = Nothing
            qry = " SELECT " &
                  "  CONVERT(varchar, MIN(DATEPART(YEAR, Requisition_Date)) - 1) + ' - ' + CONVERT(varchar, MIN(DATEPART(YEAR, Requisition_Date)) - 1 + 1) " &
                  "  AS FiscalYear, " &
                  "  CONVERT(varchar, MIN(DATEPART(YEAR, Requisition_Date)) - 1) AS Year " &
                            " FROM TSPL_REQUISITION_HEAD " &
                            " UNION ALL " &
                " Select DISTINCT " &
                  " CONVERT(varchar, DATEPART(YEAR, Requisition_Date)) + ' - ' + CONVERT(varchar, DATEPART(YEAR, Requisition_Date) + 1) " &
                  " AS FiscalYear, " &
                  " CONVERT(varchar, DATEPART(YEAR, Requisition_Date)) AS Year " &
                " FROM TSPL_REQUISITION_HEAD "

            qry = "  select FISCAL_CODE as [FiscalYear] , SUBSTRING(FISCAL_CODE,1,4) AS [Year] from TSPL_Fiscal_Year_Master "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                cboFiscalYear.DataSource = Nothing
                cboFiscalYear.DataSource = dt
                cboFiscalYear.ValueMember = "Year"
                cboFiscalYear.DisplayMember = "FiscalYear"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub cbxSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles cbxSummary.ToggleStateChanged
        Try
            If args.ToggleState = ToggleState.On Then

                dtpfromdate.Enabled = False
                dtpTodate.Enabled = False
                'rdobtnall.Enabled = False
                'rdoPonever.Enabled = False
                'rdoPOPartial.Enabled = False
                'rdoCompleted.Enabled = False
                'IndentTypeBoth.Enabled = False
                'IndentTypePurchase.Enabled = False
                'IndentTypeStore.Enabled = False
                'txtDocNo.Enabled = False
                'txtLocation.Enabled = False
                'txtVendor.Enabled = False
                'txtItem.Enabled = False
                '---------------------------------
                cboFiscalYear.Enabled = True
                lblFiscalYear.Enabled = True
                FillFiscalYear()
                btnBack.Visible = True
                btnBack.Visible = False
            Else

                dtpfromdate.Enabled = True
                dtpTodate.Enabled = True
                rdobtnall.Enabled = True
                rdoPonever.Enabled = True
                rdoPOPartial.Enabled = True
                rdoCompleted.Enabled = True
                rdoShortClose.Enabled = True
                IndentTypePurchase.Enabled = True
                IndentTypeStore.Enabled = True
                txtDocNo.Enabled = True
                txtLocation.Enabled = True
                txtVendor.Enabled = True
                txtItem.Enabled = True
                '---------------------------------
                cboFiscalYear.Enabled = False
                lblFiscalYear.Enabled = False
                FillFiscalYear()
                btnBack.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        byMonth = Nothing
        Try
            If cbxSummary.Checked Then 'AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then

                If e.Column.Name = "Pending Indent Doc" Then
                    Dim obj As frmPurchaseRequistion = New frmPurchaseRequistion()
                    obj.SetUserMgmt(clsUserMgtCode.mbtnPurchaseRequistion)
                    obj.Show()
                    obj.LoadData(e.Row.Cells(e.Column.Name).Value.ToString(), NavigatorType.Current)
                End If

                If e.Column.Name = "RequisitionNo" Then
                    Dim obj As frmStoreRequistion = New frmStoreRequistion()
                    obj.SetUserMgmt(clsUserMgtCode.frmStoreRequistion)
                    obj.Show()
                    obj.LoadData(e.Row.Cells(e.Column.Name).Value.ToString(), NavigatorType.Current)
                End If

                If e.Column.Name = "PO Doc" Then
                    Dim obj1 As frmPurchaseOrder = New frmPurchaseOrder("PO-ODR")
                    obj1.SetUserMgmt(clsUserMgtCode.FrmPurchaseOrderMT)
                    obj1.Show()
                    obj1.LoadData(e.Row.Cells(e.Column.Name).Value.ToString(), NavigatorType.Current)
                End If

                If e.Column.Name = "Indents Received" Or e.Column.Name = "Total Issues" Then
                    If e.Row.Cells("Month").Value IsNot Nothing AndAlso clsCommon.myLen(e.Row.Cells("Month").Value) > 0 Then
                        byMonth = e.Row.Cells("Month").Value
                    End If
                    Load_Report(objCommonVar.CurrentCompanyCode, e.Row.Cells("Month").Value, e.Row.Cells("Year").Value, "Indents Received")
                End If

                If e.Column.Name = "Indents Processed" Or e.Column.Name = "Total Returns" Then
                    If e.Row.Cells("Month").Value IsNot Nothing AndAlso clsCommon.myLen(e.Row.Cells("Month").Value) > 0 Then
                        byMonth = e.Row.Cells("Month").Value

                    End If

                    Load_Report(objCommonVar.CurrentCompanyCode, e.Row.Cells("Month").Value, e.Row.Cells("Year").Value, "Indents Processed")
                End If

                If e.Column.Name = "Month" Or e.Column.Name = "In Month" Then
                    If e.Row.Cells("Month").Value IsNot Nothing AndAlso clsCommon.myLen(e.Row.Cells("Month").Value) > 0 Then
                        byMonth = e.Row.Cells("Month").Value
                    End If
                    Load_Report(objCommonVar.CurrentCompanyCode, byMonth, e.Row.Cells("Year").Value, "Indents Processed")
                End If
                btnBack.Visible = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If cbxSummary.Checked = True Then
                byMonth = Nothing
                gv.DataSource = Nothing
                Load_Monthly_Summaries(objCommonVar.CurrentCompanyCode)
                If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
                    btnBack.Visible = True
                Else
                    btnBack.Visible = False

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
        If RadPageView1.SelectedPage.Equals(RadPageViewPage2) Then
            If byMonth IsNot Nothing AndAlso clsCommon.myLen(byMonth) > 0 Then
                btnBack.Visible = True
            Else
                btnBack.Visible = False
            End If
        Else
            btnBack.Visible = False
        End If
    End Sub
    Private Sub IndentTypeStore_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles IndentTypeStore.ToggleStateChanged
        If args.ToggleState = ToggleState.On Then
            IndentTypePurchase.ToggleState = ToggleState.Off
            rdobtnall.Enabled = False
            rdoPonever.Enabled = False
            rdoPOPartial.Enabled = False
            rdoCompleted.Enabled = False
            'cbxSummary.Enabled = False
            'cboFiscalYear.Enabled = False
            rdoShortClose.Enabled = False
        Else
            IndentTypePurchase.ToggleState = ToggleState.On
            rdobtnall.Enabled = True
            rdoPonever.Enabled = True
            rdoPOPartial.Enabled = True
            rdoCompleted.Enabled = True

            cbxSummary.Enabled = True
            cboFiscalYear.Enabled = True
            rdoShortClose.Enabled = True
        End If

    End Sub
    Private Sub IndentTypePurchase_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles IndentTypePurchase.ToggleStateChanged
        If args.ToggleState = ToggleState.On Then
            IndentTypeStore.ToggleState = ToggleState.Off
        Else
            IndentTypeStore.ToggleState = ToggleState.On
        End If
        If IndentTypePurchase.Checked = True Then
            RadGroupBox8.Enabled = True
            chkItemWiseSummary.IsChecked = True
        Else
            RadGroupBox8.Enabled = False
            chkNone.IsChecked = True
        End If

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub


End Class
