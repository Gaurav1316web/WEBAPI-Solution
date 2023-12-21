' Puran Singh Negi(14/Aug/2008) - TicketNo- BM00000003419

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmPo_action
    Inherits FrmMainTranScreen

    Dim is_Load_MRN As Boolean = False
    Dim dtCategory As DataTable
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPo_action)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub



    Private Sub frmPo_action_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            SetUserMgmtNew()
            is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
            dtpfromdate.Value = clsCommon.GETSERVERDATE()
            dtpTodate.Value = clsCommon.GETSERVERDATE()
            LoadDocuemntNo()
            LoadVendor()
            ItemLoad()
            LoadLocation()
            LoadCategory()
            chkdocAll.IsChecked = True
            rbtnCategoryAll.IsChecked = True
            chkVendor_all.IsChecked = True
            chkItemAll.IsChecked = True
            chkLocationAll.IsChecked = True
            'If objCommonVar.CurrentUserCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
            RadPageView1.SelectedPage = RadPageViewPage1
            gv1.EnableGrouping = True
            'KUNAL > KDIL > FILTERS WAS NOT ENABLED ON USER WISE
            Me.gv.MasterTemplate.EnableFiltering = True
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadCategory()
        'Dim qry As String = "select Code,Name,Parent from ("
        'qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        'qry += " union all"
        'qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        'qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        'qry += " Union all"
        'qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        'qry += " )xxx order by Sno"

        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'tvCategory.DataSource = Nothing
        'tvCategory.TreeViewElement.AutoSizeItems = True
        'tvCategory.ShowLines = True
        'tvCategory.ShowRootLines = True
        'tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        'tvCategory.ShowExpandCollapse = True
        'tvCategory.TreeIndent = 15
        'tvCategory.FullRowSelect = False
        'tvCategory.ShowLines = True
        'tvCategory.LineStyle = TreeLineStyle.Dot
        'tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        'tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        'tvCategory.AllowEdit = False
        'tvCategory.ShowRootLines = False
        'tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        'tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        'tvCategory.TreeViewElement.DrawBorder = True
        'tvCategory.ValueMember = "Code"
        'tvCategory.DisplayMember = "Name"
        'tvCategory.ChildMember = "Code"
        'tvCategory.ParentMember = "Parent"
        'tvCategory.DataSource = dt
        'tvCategory.CheckBoxes = True

        'tvCategory.ExpandAll()

        '------Ravi------------
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gvCategory.DataSource = dt
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Name"

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

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-RPT"
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
        PrintData1()
    End Sub
    Sub PrintData1()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return

        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please selete one Item", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
            Return

        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")

        PrintData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkItemSelect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
    End Sub
    Public Sub PrintData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemSelect As Boolean, ByVal arrItem As ArrayList, ByVal isLocation As Boolean, ByVal arrLocation As ArrayList)
        Dim Address As String = ""
        Dim Item As String = ""
        Dim location As String = ""
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""

        If cbgVendor.CheckedValue.Count > 0 Then
            Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If
        If cbgDocument.CheckedValue.Count > 0 Then
            DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
            StrDocNo = DocNo.Replace("'", "")
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
        End If
        If cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
            StrItem = Item.Replace("'", "")
        End If
        Dim status As String = "ALL"
        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                status = "Never"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = "Partial "
            End If
            If rdoCompleted.IsChecked = True Then
                status = "Completed"
            End If
        End If
        Dim Balance As String = ""
        If is_Load_MRN = True Then
            Balance = "(po_qty)-GRNQty"
        Else
            Balance = "(po_qty)-SRN_Qty"
        End If
        If clsCommon.myLen(Balance) <= 0 Then
            Balance = "0"
        End If
        If isLocation AndAlso arrLocation.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )"
        Else
            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
        End If

        Dim strQuery As String = "select TSPL_PURCHASE_ORDER_HEAD.comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'" + StrDocNo + "' as StrDocNo ,'" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,'" + status + "' as status,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No as purchase_no , convert (date, TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date,convert (varchar, TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date_var ,TSPL_PURCHASE_ORDER_HEAD .Vendor_Code  as vendor_no ,TSPL_PURCHASE_ORDER_HEAD .Vendor_Name  as Vendor_name , TSPL_COMPANY_MASTER.Comp_Name as compname," + Address + " as address1,TSPL_PURCHASE_ORDER_HEAD .delivery_date as Delivaery_date,TSPL_PURCHASE_ORDER_DETAIL .Item_Cost as po_rate ,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code,TSPL_PURCHASE_ORDER_DETAIL.item_desc as itemdesc,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as po_qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom ,tspl_purchase_order_head.Bill_To_Location as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )as Location_Desc, stuff((select ',' + isnull(GRN_NO,'') from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code for xml path ('')),1,1,'' )as GRN_NO ,(select isnull(SUM(isnull(GRN_Qty,0)),0.0) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) as GRNQty,( select isnull(SUM(ISNULL(TSPL_MRN_DETAIL.MRN_Qty,0)),0.0) from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and  TSPL_MRN_DETAIL .GRN_Id in (select GRN_No from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) ) as MRNQty,(select isnull(SUM(ISNULL(TSPL_SRN_DETAIL.SRN_Qty,0)),0.0) from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_ID  )as SRN_Qty,(select isnull(SUM(ISNULL(TSPL_SRN_DETAIL.Item_Cost ,0)),0.0) from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code  and MRN_Id in(select TSPL_MRN_DETAIL.MRN_No from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code  and  TSPL_MRN_DETAIL .GRN_Id in (select GRN_No from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  and  TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code )))as SRN_Cost,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [MainGroup],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [GroupName] from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code left outer join TSPL_ITEM_MASTER on TSPL_PURCHASE_ORDER_DETAIL.item_code=tspl_item_master.item_code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values  where 2=2 "
        'If isDocSelect AndAlso ArrDoc.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Document")
        '    Return
        'ElseIf cbgDocument.CheckedValue.Count > 0 Then
        '    strQuery += " and TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No in (" + clsCommon.GetMulcallString(ArrDoc) + ")"
        '    DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
        '    StrDocNo = DocNo.Replace("'", "")
        'End If
        'If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
        '    Return
        'ElseIf cbgVendor.CheckedValue.Count > 0 Then
        '    strQuery += " and TSPL_PURCHASE_ORDER_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")"
        '    Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
        '    StrVendor = Vendor.Replace("'", "")
        'End If
        'If isLocation AndAlso arrLocation.Count <= 0 Then
        'common.clsCommon.MyMessageBoxShow("Please select at least one Location For Print")
        'Return
        'ElseIf cbgLocation.CheckedValue.Count > 0 Then
        ' '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        'strQuery += " and tspl_purchase_order_head.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
        'Strlocation = location.Replace("'", "")
        'End If
        'If isitemSelect AndAlso arrItem.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Item For Print")
        '    Return
        'ElseIf cbgItem.CheckedValue.Count > 0 Then
        '    strQuery += " and  TSPL_PURCHASE_ORDER_DETAIL.item_code in (" + clsCommon.GetMulcallString(arrItem) + ")"
        '    Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
        '    StrItem = Item.Replace("'", "")
        'End If

        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            strQuery += " and TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strQuery += " and tspl_purchase_order_head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strQuery += " and tspl_purchase_order_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            strQuery += "  and TSPL_PURCHASE_ORDER_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strQuery += "  and  TSPL_PURCHASE_ORDER_DETAIL.item_code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        '==============

        'If rbtnCategorySelect.IsChecked Then
        '    Dim isFirstTime As Boolean = True
        '    strQuery += " and exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine
        '    For Each Ctr As RadTreeNode In tvCategory1.CheckedNodes
        '        If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
        '            If Not isFirstTime Then
        '                strQuery += " or "
        '            End If
        '            strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
        '            isFirstTime = False
        '        End If
        '    Next
        '    strQuery += " ))"
        '    If isFirstTime Then
        '        Throw New Exception("Please select at least one Category")
        '    End If
        'End If
        '------------Ravi--------------
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        Dim isFirstTime As Boolean = True

        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            strQuery += " and TSPL_PURCHASE_ORDER_DETAIL.item_code in  (select Item_code  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine

            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then

                    If Not isFirstTime Then
                        strQuery += " or "
                    End If

                    strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("Code").Value) + "' " + Environment.NewLine
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For Each strInn As String In arr.Keys
                            strQuery += " and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(strInn) + "' "
                        Next
                    End If ''tag arr cond.
                    strQuery += " ) "
                    isFirstTime = False
                End If
            Next
            strQuery += " ))"




            'If Not IsApplicable Then
            '    Throw New Exception("Please select at least one category")
            'End If
            ' strQuery += " and (" + strWhrCatg + ")"
        End If
        '---------------------------'
        strQuery += " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + ToDate + "',103) and PurchaseOrder_Type <>  'J' "

        Dim qry12 As String = strQuery
        strQuery = "select distinct comp_code,Fromdate,Todate,strdocno,strvendor,strlocation,stritem,purchase_no,po_date,po_date_var,vendor_no as vendor_no,vendor_name,compname,address1,delivaery_date,po_rate,item_code,itemdesc,po_qty,uom,location,location_desc,grnqty,mrnqty,srn_qty,srn_cost," + Balance + " as 'Balance Qty' ,GRN_NO,status from (" + qry12 + ")ass1"

        qry12 = strQuery

        strQuery = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,* from (" + qry12 + ")ax left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=ax.comp_code where ax.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                strQuery += " and len(isnull(GRN_NO,''))<=0 "
            End If
            If rdoPOPartial.IsChecked = True Then

                strQuery += " and [Balance Qty]<>0 and len(isnull(GRN_NO,''))>0 "
            End If
            If rdoCompleted.IsChecked = True Then
                strQuery += " and [Balance Qty] =0 "
            End If
        End If
        Dim strOrederCls As String = " order by  Po_Date " ' purchase_no replace by Po_Date 
        Dim qry As String = strQuery + strOrederCls

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "frmPo_Action", "Purchase Order Action")
        frmCRV = Nothing
    End Sub
    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged
        'cbgDocument.Enabled = Not chkdocAll.IsChecked
        'If chkDoc_select.IsChecked Then
        '    chkVendor_all.IsChecked = chkDoc_select.IsChecked
        'End If
        cbgDocument.Enabled = Not chkdocAll.IsChecked
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all.ToggleStateChanged, chkVendor_select.ToggleStateChanged
        'cbgVendor.Enabled = Not chkVendor_all.IsChecked
        'If chkVendor_select.IsChecked Then
        '    chkdocAll.IsChecked = chkVendor_select.IsChecked
        'End If
        cbgVendor.Enabled = Not chkVendor_all.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        'dtpfromdate.Value = clsCommon.GETSERVERDATE()
        'dtpTodate.Value = clsCommon.GETSERVERDATE()
        'chkdocAll.IsChecked = True
        'chkItemAll.IsChecked = True
        'chkVendor_all.IsChecked = True
        'chkLocationAll.IsChecked = True
        'rbtnCategoryAll.IsChecked = True
        'RadPageView1.SelectedPage = RadPageViewPage1
        'gv.Columns.Clear()
        'LoadCategory()
        RadGroupBox7.Enabled = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDetails.IsChecked = True

    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    Private Sub frmPo_action_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        'tvCategory.Enabled = rbtnCategorySelect.IsChecked
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub
    Public Sub ReferehData(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemSelect As Boolean, ByVal arrItem As ArrayList, ByVal isLocation As Boolean, ByVal arrLocation As ArrayList)
        Dim Address As String = ""
        Dim Item As String = ""
        Dim location As String = ""
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""
        Dim status As String = "ALL"
        If cbgVendor.CheckedValue.Count > 0 Then
            Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If
        If cbgDocument.CheckedValue.Count > 0 Then
            DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
            StrDocNo = DocNo.Replace("'", "")
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
        End If
        If cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
            StrItem = Item.Replace("'", "")
        End If

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                status = "Never"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = "Partial "
            End If
            If rdoCompleted.IsChecked = True Then
                status = "Completed"
            End If
        End If
        Dim Balance As String = ""
        If is_Load_MRN = True Then
            Balance = "((po_qty+Tolerence_Qty)-GRNQty +Short_Qty+Leak_Qty+Burst_Qty+Rejected_Qty)"
        Else
            Balance = "((po_qty)-SRN_Qty +Short_Qty+Leak_Qty+Burst_Qty+Rejected_Qty)"
        End If
        If clsCommon.myLen(Balance) <= 0 Then
            Balance = "0"
        End If
        If isLocation AndAlso arrLocation.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )"
        Else
            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
        End If


        Dim strQuery As String = " SELECT PO.comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'' AS StrDocNo, " &
        " '' AS StrVendor,'' AS Strlocation,'' AS StrItem,'ALL' AS status,PO.PurchaseOrder_No AS purchase_no,PO.PurchaseOrder_Date AS po_date, " &
          " CASE WHEN (PO.close_yn) = 'Y' THEN 'Close' WHEN (PO.status) = 1 THEN 'Close'" &
        " when QC.Document_Code is not null and TSPL_SRN_DETAIL.SRN_No is null  and ISNULL( QC.Ok_Qty,0)=0 and isnull(QC.Reject_Qty,0)>0 then 'Open' " + Environment.NewLine +
        " WHEN ((ISNULL(PO.PurchaseOrder_Qty, 0) + ISNULL(Tolerence_Qty, 0)) - ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0)) > 0 THEN 'Open' " &
           " ELSE 'Complete' END AS POStatus, " &
          " (PO.Closed_By) AS ClosedBy, " &
          " (PO.Closed_Date) AS ClosedDate, " &
          " TSPL_SRN_DETAIL.SRN_NO AS SRN_NO," &
          " CONVERT(varchar, TSPL_SRN_HEAD.SRN_Date, 103) AS SRN_Date,PO.Vendor_Code AS vendor_no,PO.Vendor_Name AS Vendor_name,TSPL_COMPANY_MASTER.Comp_Name AS compname, " &
          " (TSPL_COMPANY_MASTER.Add1 + CASE WHEN TSPL_COMPANY_MASTER.Add2 = '' THEN ''" &
           " ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add2, 103) " &
          " END + CASE WHEN TSPL_COMPANY_MASTER.Add3 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add3, 103) END + CASE " &
           " WHEN TSPL_COMPANY_MASTER.City_Code = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code, 103) END + CASE " &
           " WHEN TSPL_COMPANY_MASTER.State = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.State) END + CASE " &
           " WHEN TSPL_COMPANY_MASTER.Pincode = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Pincode, 103) END) AS address1, " &
          " PO.delivery_date AS Delivaery_date, " &
          " PO.Item_Cost AS po_rate, " &
          " PO.item_code AS item_code, " &
          " PO.item_desc AS itemdesc,PO.purchaseorder_qty AS po_qty,PO.unit_code AS uom,PO.Bill_To_Location AS Location," &
          " ( SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code = PO.Bill_To_Location) AS Location_Desc, " &
          " ISNULL(TSPL_GRN_DETAIL.GRN_NO, '') AS GRN_NO,CONVERT(varchar, TSPL_GRN_HEAD.GRN_Date, 103) AS GRN_Date,PO.Against_Requisition," &
         " TSPL_REQUISITION_HEAD.Requisition_Date,ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) AS GRNQty,ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty, 0) AS Tolerence_Qty,ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0) AS MRNQty," &
         " ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0) AS SRN_Qty,ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) AS Short_Qty,ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) AS Leak_Qty,ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) AS Burst_Qty," &
         " ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0) AS Rejected_Qty,ISNULL(TSPL_SRN_DETAIL.Item_Cost, 0) AS SRN_Cost,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AS [MainGroupCode" &
         " TSPL_ITEM_CATEGORY_LEVEL.description AS [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values AS [GroupCode],TSPL_ITEM_CATEGORY_LEVEL_VALUES.description AS [Group Name]," &
         " CASE WHEN COALESCE((ISNULL(ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0), 0.0)), 0) <= 0 THEN COALESCE((SELECT (ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0))), (ISNULL((ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0)), 0.0)))" &
         " END AS QCQty,TSPL_SRN_DETAIL.Balance_Qty,PO.[Payment Terms],PO.[Delivery Term],PO.Freight FROM(select ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, '') AS PurchaseOrder_No" &
        " ,tspl_purchase_order_detail.Item_Code,MAX(TSPL_PURCHASE_ORDER_HEAD.comp_code) AS comp_code,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date) AS PurchaseOrder_Date" &
        " ,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) AS PurchaseOrder_Type,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_By) AS Closed_By" &
        " ,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_Date) AS Closed_Date,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Code) AS Vendor_Code" &
        " ,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Name) AS Vendor_Name,SUM(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) AS PurchaseOrder_Qty" &
        " ,MAX(TSPL_PURCHASE_ORDER_HEAD.close_yn) AS close_yn,MAX(TSPL_PURCHASE_ORDER_DETAIL.status) AS status" &
        " ,MAX(TSPL_PURCHASE_ORDER_HEAD.delivery_date) AS delivery_date,MAX(TSPL_PURCHASE_ORDER_DETAIL.unit_code) AS unit_code" &
        " ,MAX(tspl_purchase_order_head.Bill_To_Location) AS Bill_To_Location,MAX(tspl_purchase_order_head.Against_Requisition) AS Against_Requisition" &
        " ,MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Cost) AS Item_Cost ,MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Desc) AS Item_Desc,max(TSPL_PURCHASE_ORDER_HEAD.Freight) as Freight " &
        " ,max(TSPL_PURCHASE_ORDER_HEAD.Payment_Terms) as [Payment Terms],max(isnull(TSPL_DELIVERY_TERMS_MASTER.Description,'')) as [Delivery Term]" &
        " from tspl_purchase_order_detail" &
        " LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_DELIVERY_TERMS_MASTER on TSPL_DELIVERY_TERMS_MASTER.code=TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code GROUP BY ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, ''),tspl_purchase_order_detail.Item_Code)PO " &
        " LEFT JOIN TSPL_GRN_DETAIL ON ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code" &
        " LEFT JOIN TSPL_GRN_HEAD ON ISNULL(TSPL_GRN_HEAD.grn_no, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '')" &
        " LEFT JOIN TSPL_MRN_DETAIL ON TSPL_MRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(TSPL_MRN_DETAIL.GRN_Id, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '') AND ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code" &
        " left outer join (select TSPL_QC_CHECK_DETAIL.*  from TSPL_QC_CHECK_DETAIL left outer join  TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_DETAIL.Document_Code where TSPL_QC_CHECK_HEAD.Posted=1) as  QC on isnull( QC.MRN_No,'')=ISNULL( TSPL_MRN_DETAIL.MRN_No,'') and QC.Item_Code = TSPL_MRN_DETAIL.Item_Code  and ISNULL(QC.PO_No, '')=ISNULL(TSPL_MRN_DETAIL.PO_ID, '') " + Environment.NewLine +
        " LEFT JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(PO.PurchaseOrder_No, '') = ISNULL(TSPL_SRN_DETAIL.PO_ID, '') AND ISNULL(TSPL_MRN_DETAIL.mrn_no, '') = ISNULL(TSPL_SRN_DETAIL.mrn_id, '')" &
        " AND ISNULL(TSPL_GRN_DETAIL.grn_no, '') = ISNULL(TSPL_MRN_DETAIL.grn_id, '')" &
        " LEFT JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_no" &
        " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON tspl_company_Master.Comp_Code = PO.comp_code" &
        " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = PO.Item_Code" &
        " LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code = TSPL_ITEM_MASTER.item_code" &
        " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code" &
        " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.code = TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values" &
        " LEFT OUTER JOIN TSPL_REQUISITION_HEAD ON ISNULL(PO.Against_Requisition, '') = ISNULL(TSPL_REQUISITION_HEAD.Requisition_Id, '')" &
        " WHERE 2 = 2"

        ''        Dim strQuery As String = " select TSPL_PURCHASE_ORDER_HEAD.comp_code,'" + FromDate + "' as  FromDate,'" + ToDate + "' as ToDate,'' as StrDocNo ,'' as StrVendor," & _
        ''" '' as Strlocation,'' as StrItem,'ALL' as status,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No as purchase_no," & _
        ''" TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date as po_date,case when (TSPL_PURCHASE_ORDER_HEAD.close_yn)='Y' then 'Close' when (TSPL_PURCHASE_ORDER_DETAIL.status)=1 then 'Close' when ((ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)+ISNULL(Tolerence_Qty,0))-ISNULL(TSPL_GRN_DETAIL.GRN_Qty,0) +ISNULL(TSPL_SRN_DETAIL.Short_Qty,0)+ISNULL(TSPL_SRN_DETAIL.Leak_Qty,0)+ISNULL(TSPL_SRN_DETAIL.Burst_Qty,0)+ISNULL(TSPL_SRN_DETAIL.Rejected_Qty,0))>0 then 'Open' else 'Complete' end as POStatus,(TSPL_PURCHASE_ORDER_HEAD.Closed_By) as ClosedBy,(TSPL_PURCHASE_ORDER_HEAD.Closed_Date) as ClosedDate ,TSPL_SRN_DETAIL.SRN_NO as SRN_NO, CONVERT(VARCHAR , TSPL_SRN_HEAD.SRN_Date , 103)  AS SRN_Date, " & _
        ''" TSPL_PURCHASE_ORDER_HEAD .Vendor_Code  as vendor_no ,TSPL_PURCHASE_ORDER_HEAD .Vendor_Name  as Vendor_name , TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
        ''" (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + " & _
        ''" Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + " & _
        ''" case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ " & _
        ''" Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  " & _
        ''" Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end)  as address1, " & _
        ''" TSPL_PURCHASE_ORDER_HEAD .delivery_date as Delivaery_date,TSPL_PURCHASE_ORDER_DETAIL .Item_Cost as po_rate ," & _
        ''" TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code,TSPL_PURCHASE_ORDER_DETAIL.item_desc as itemdesc,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as po_qty," & _
        ''" TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom ,tspl_purchase_order_head.Bill_To_Location as Location,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code " & _
        '' " =TSPL_PURCHASE_ORDER_HEAD .Bill_To_Location )as Location_Desc , " & _
        ''" isnull(TSPL_GRN_DETAIL.GRN_NO,'') as GRN_NO ,CONVERT(VARCHAR , TSPL_GRN_HEAD.GRN_Date , 103)   AS " & _
        ''" GRN_Date ,TSPL_PURCHASE_ORDER_HEAD.Against_Requisition,TSPL_REQUISITION_HEAD.Requisition_Date,isnull(TSPL_GRN_DETAIL.GRN_Qty,0) as GRNQty,isnull(TSPL_GRN_DETAIL.Tolerence_Qty,0) AS Tolerence_Qty,ISNULL(TSPL_MRN_DETAIL.MRN_Qty,0)as " & _
        ''" MRNQty," & _
        '' " ISNULL(TSPL_SRN_DETAIL.SRN_Qty,0)as SRN_Qty,ISNULL(TSPL_SRN_DETAIL.Short_Qty,0)as Short_Qty,ISNULL(TSPL_SRN_DETAIL.Leak_Qty,0)as Leak_Qty,ISNULL(TSPL_SRN_DETAIL.Burst_Qty,0)as Burst_Qty,ISNULL(TSPL_SRN_DETAIL.Rejected_Qty,0)as Rejected_Qty,ISNULL(TSPL_SRN_DETAIL.Item_Cost ,0)as SRN_Cost,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  " & _
        '' " TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name]  " & _
        '' " ,case when coalesce(( isnull(ISNULL(TSPL_SRN_DETAIL.SRN_Qty,0),0.0)),0)<=0 then  coalesce(( select (ISNULL(TSPL_MRN_DETAIL.MRN_Qty,0))  ) ," & _
        '' " ( isnull((isnull(TSPL_GRN_DETAIL.GRN_Qty,0)),0.0) )) end as QCQty,TSPL_SRN_DETAIL.Balance_Qty " & _
        ''" from tspl_purchase_order_detail " & _
        ''" left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " & _
        ''" left join TSPL_GRN_DETAIL on isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and TSPL_GRN_DETAIL.Item_Code " & _
        ''" =tspl_purchase_order_detail.Item_Code  " & _
        ''" left join TSPL_GRN_HEAD on isnull(TSPL_GRN_HEAD.grn_no,'')=isnull(TSPL_GRN_DETAIL.grn_no,'') " & _
        ''" left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
        ''" and  isnull(TSPL_MRN_DETAIL .GRN_Id,'') =isnull(TSPL_GRN_DETAIL.grn_no,'') and isnull(TSPL_GRN_DETAIL.PO_Id,'')=isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'') and  " & _
        ''" TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
        ''" left join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL .Item_Code and isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')= " & _
        ''" isnull(TSPL_SRN_DETAIL.PO_ID ,'') " & _
        ''" and isnull(TSPL_MRN_DETAIL.mrn_no,'')=isnull(TSPL_SRN_DETAIL.mrn_id,'') and isnull(TSPL_GRN_DETAIL.grn_no,'')=isnull(TSPL_MRN_DETAIL.grn_id,'') " & _
        ''" left join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_no " & _
        ''" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code " & _
        ''" left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =tspl_purchase_order_detail.Item_Code " & _
        ''"  LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code " & _
        ''"  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
        '' " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " & _
        '' " TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values " & _
        '' " left outer join TSPL_REQUISITION_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.Against_Requisition,'')=isnull(TSPL_REQUISITION_HEAD.Requisition_Id,'') " & _
        ''"  where 2=2 "



        '====added by shivani
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            strQuery += " and PO.PurchaseOrder_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            'strQuery += " and TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strQuery += " and PO.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            'strQuery += " and tspl_purchase_order_head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strQuery += " and PO.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            strQuery += "  and PO .Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            'strQuery += "  and TSPL_PURCHASE_ORDER_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strQuery += "  and  PO.item_code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            'strQuery += "  and  TSPL_PURCHASE_ORDER_DETAIL.item_code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        '==============
        '------Ravi--------------
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        Dim isFirstTime As Boolean = True

        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            strQuery += " and PO.item_code in  (select Item_code  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine
            'strQuery += " and TSPL_PURCHASE_ORDER_DETAIL.item_code in  (select Item_code  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then

                    If Not isFirstTime Then
                        strQuery += " or "
                    End If

                    strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("Code").Value) + "' " + Environment.NewLine
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For Each strInn As String In arr.Keys
                            strQuery += " and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(strInn) + "' "
                        Next
                    End If ''tag arr cond.
                    strQuery += " ) "
                    isFirstTime = False
                End If
            Next
            strQuery += " ))"




            'If Not IsApplicable Then
            '    Throw New Exception("Please select at least one category")
            'End If
            ' strQuery += " and (" + strWhrCatg + ")"
        End If

        '------------------------------'

        'If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
        '    Return
        'ElseIf cbgVendor.CheckedValue.Count > 0 Then
        '    strQuery += " and TSPL_PURCHASE_ORDER_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")"
        '    Vendor = ("'" + clsCommon.GetMulcallString(ArrVendor) + "'")
        '    StrVendor = Vendor.Replace("'", "")
        'End If
        'If isLocation AndAlso arrLocation.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Location For Print")
        '    Return
        'ElseIf cbgLocation.CheckedValue.Count > 0 Then
        '    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
        '    strQuery += " and tspl_purchase_order_head.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        '    location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
        '    Strlocation = location.Replace("'", "")
        'End If
        'If isitemSelect AndAlso arrItem.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one Item For Print")
        '    Return
        'ElseIf cbgItem.CheckedValue.Count > 0 Then
        '    strQuery += " and  TSPL_PURCHASE_ORDER_DETAIL.item_code in (" + clsCommon.GetMulcallString(arrItem) + ")"
        '    Item = "'" + clsCommon.GetMulcallString(arrItem) + "'"
        '    StrItem = Item.Replace("'", "")
        'End If


        'If rbtnCategorySelect.IsChecked Then
        '    Dim isFirstTime As Boolean = True
        '    strQuery += " and exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PURCHASE_ORDER_DETAIL) and ( " + Environment.NewLine
        '    For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
        '        If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
        '            If Not isFirstTime Then
        '                strQuery += " or "
        '            End If
        '            strQuery += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
        '            isFirstTime = False
        '        End If
        '    Next
        '    strQuery += " ))"
        '    If isFirstTime Then
        '        Throw New Exception("Please select at least one Category")
        '    End If
        'End If

        strQuery += " and convert(date,PO.PurchaseOrder_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,PO.PurchaseOrder_Date,103)<= convert(date,'" + ToDate + "',103) and PO.PurchaseOrder_Type <>  'J' "
        'strQuery += " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>= convert(date,'" + FromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + ToDate + "',103) and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type <>  'J' "

        Dim qry12 As String = strQuery
        'Change by Prabhakar :'
        strQuery = "select distinct comp_code,Fromdate,Todate,strdocno,strvendor,strlocation,stritem,purchase_no,convert(varchar,po_date,103) as po_date,POStatus,ClosedBy,FORMAT(ClosedDate,'dd/MM/yyyy') as ClosedDate,SRN_NO , SRN_Date ,GRN_NO, GRN_Date, Against_Requisition,convert(varchar ,Requisition_Date,103) as Requisition_Date,vendor_no as vendor_no,vendor_name,compname,address1,[Payment Terms],[Delivery Term],Freight,delivaery_date,po_rate,item_code,itemdesc,po_qty,uom,location,location_desc,grnqty,mrnqty,srn_qty ,QCQty,Short_Qty,Rejected_Qty ," + Balance + " as 'Balance Qty',Balance_Qty,srn_cost from (" + qry12 + ")ass1"

        qry12 = strQuery
        'abs(po_qty-sum(SRN_Qty) over (partition by item_code,purchase_no order by item_code,RowNumber))
        strQuery = " select final.*,(case when (po_qty-sum(SRN_Qty) over (partition by item_code,purchase_no order by item_code,RowNumber))  <0 then 0 else round((po_qty-sum(SRN_Qty) over (partition by item_code,purchase_no order by item_code,RowNumber)),2) end) as [Qty1] from (select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ROW_NUMBER() over (partition by item_code order by item_code,purchase_no) as RowNumber,ax.* from (" + qry12 + ")ax left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=ax.comp_code where ax.comp_code='" + objCommonVar.CurrentCompanyCode + "' )final where 1=1 "

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                strQuery += " and len(isnull(GRN_NO,''))<=0 "
            End If
            If rdoPOPartial.IsChecked = True Then


                strQuery += " and [Balance Qty]<>0 and len(isnull(GRN_NO,''))>0 "
            End If
            If rdoCompleted.IsChecked = True Then
                strQuery += " and final.POStatus ='Complete' "
            End If
        End If

        Dim strOrederCls As String = " order by CONVERT(DATE,Po_Date,103)"
        Dim qry As String = strQuery + strOrederCls

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dt
            SetGridFormatOFGV()
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox7.Enabled = False
        End If
    End Sub

    Public Sub SummaryData()
        Dim Address As String = ""
        Dim Item As String = ""
        Dim location As String = ""
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrDocNo As String = ""
        Dim StrVendor As String = ""
        Dim status As String = "ALL"
        If cbgVendor.CheckedValue.Count > 0 Then
            Vendor = ("'" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "'")
            StrVendor = Vendor.Replace("'", "")
        End If
        If cbgDocument.CheckedValue.Count > 0 Then
            DocNo = "'" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + "'"
            StrDocNo = DocNo.Replace("'", "")
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            Location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = Location.Replace("'", "")
        End If
        If cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(txtItem.arrValueMember) + "'"
            StrItem = Item.Replace("'", "")
        End If

        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                status = "Never"
            End If
            If rdoPOPartial.IsChecked = True Then
                status = "Partial "
            End If
            If rdoCompleted.IsChecked = True Then
                status = "Completed"
            End If
        End If
        Dim qry As String = " select * from  (select distinct purchase_no,convert(varchar,po_date,103) as po_date,POStatus,SRN_NO , SRN_Date ,vendor_no as vendor_no,vendor_name,[Payment Terms],[Delivery Term],Freight,convert(decimal(18,2),po_qty) as po_qty,GRN_NO,convert(decimal(18,2),grnqty) as grnqty,convert(decimal(18,2),mrnqty) as mrnqty,convert(decimal(18,2),srn_qty) as srn_qty ,convert(decimal(18,2),QCQty) as QCQty,convert(decimal(18,2),Short_Qty) as Short_Qty,convert(decimal(18,2),Rejected_Qty) as Rejected_Qty ,convert(decimal(18,2),((po_qty+Tolerence_Qty)-GRNQty +Short_Qty+Leak_Qty+Burst_Qty+Rejected_Qty)) as 'Balance Qty',Balance_Qty,srn_cost  "
        qry += " from ( SELECT PO.comp_code,'27/11/2017' as  FromDate,'20/12/2017' as ToDate,'' AS StrDocNo,  '' AS StrVendor,'' AS Strlocation,'' AS StrItem,'ALL' AS status,PO.PurchaseOrder_No AS purchase_no,PO.PurchaseOrder_Date AS po_date"
        qry += " ,  CASE WHEN (PO.close_yn) = 'Y' THEN 'Close' WHEN (PO.status) = 1 THEN 'Close' when QC.Document_Code is not null and TSPL_SRN_DETAIL.SRN_No is null  and ISNULL( QC.Ok_Qty,0)=0 and isnull(QC.Reject_Qty,0)>0 then 'Open' "
        qry += " WHEN ((ISNULL(PO.PurchaseOrder_Qty, 0) + ISNULL(Tolerence_Qty, 0)) - ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0)) > 0 THEN 'Open'  ELSE 'Complete' END AS POStatus"

        qry += " ,  (PO.Closed_By) AS ClosedBy,  (PO.Closed_Date) AS ClosedDate,  TSPL_SRN_DETAIL.SRN_NO AS SRN_NO, CONVERT(varchar, TSPL_SRN_HEAD.SRN_Date, 103) AS SRN_Date,PO.Vendor_Code AS vendor_no,PO.Vendor_Name AS Vendor_name,TSPL_COMPANY_MASTER.Comp_Name AS compname,  (TSPL_COMPANY_MASTER.Add1 + CASE WHEN TSPL_COMPANY_MASTER.Add2 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add2, 103)  END + CASE WHEN TSPL_COMPANY_MASTER.Add3 = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Add3, 103) END + CASE  WHEN TSPL_COMPANY_MASTER.City_Code = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code, 103) END + CASE  WHEN TSPL_COMPANY_MASTER.State = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.State) END + CASE  WHEN TSPL_COMPANY_MASTER.Pincode = '' THEN '' ELSE ', ' + CONVERT(varchar, TSPL_COMPANY_MASTER.Pincode, 103) END) AS address1,  PO.delivery_date AS Delivaery_date,  PO.Item_Cost AS po_rate,  PO.item_code AS item_code,  PO.item_desc AS itemdesc,PO.purchaseorder_qty AS po_qty,PO.unit_code AS uom,PO.Bill_To_Location AS Location, ( SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code = PO.Bill_To_Location) AS Location_Desc,  ISNULL(TSPL_GRN_DETAIL.GRN_NO, '') AS GRN_NO,CONVERT(varchar, TSPL_GRN_HEAD.GRN_Date, 103) AS GRN_Date,PO.Against_Requisition, TSPL_REQUISITION_HEAD.Requisition_Date,ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) AS GRNQty,ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty, 0) AS Tolerence_Qty,ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0) AS MRNQty, ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0) AS SRN_Qty,ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) AS Short_Qty,ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) AS Leak_Qty,ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) AS Burst_Qty, ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0) AS Rejected_Qty,ISNULL(TSPL_SRN_DETAIL.Item_Cost, 0) AS SRN_Cost,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AS [MainGroupCode TSPL_ITEM_CATEGORY_LEVEL.description AS [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values AS [GroupCode],TSPL_ITEM_CATEGORY_LEVEL_VALUES.description AS [Group Name], CASE WHEN COALESCE((ISNULL(ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0), 0.0)), 0) <= 0 THEN COALESCE((SELECT (ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0))), (ISNULL((ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0)), 0.0))) END AS QCQty,TSPL_SRN_DETAIL.Balance_Qty,PO.[Payment Terms],PO.[Delivery Term],PO.Freight FROM(select ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, '') AS PurchaseOrder_No ,tspl_purchase_order_detail.Item_Code,MAX(TSPL_PURCHASE_ORDER_HEAD.comp_code) AS comp_code,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date) AS PurchaseOrder_Date ,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) AS PurchaseOrder_Type,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_By) AS Closed_By ,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_Date) AS Closed_Date,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Code) AS Vendor_Code ,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Name) AS Vendor_Name,SUM(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) AS PurchaseOrder_Qty ,MAX(TSPL_PURCHASE_ORDER_HEAD.close_yn) AS close_yn,MAX(TSPL_PURCHASE_ORDER_DETAIL.status) AS status ,MAX(TSPL_PURCHASE_ORDER_HEAD.delivery_date) AS delivery_date,MAX(TSPL_PURCHASE_ORDER_DETAIL.unit_code) AS unit_code ,MAX(tspl_purchase_order_head.Bill_To_Location) AS Bill_To_Location,MAX(tspl_purchase_order_head.Against_Requisition) AS Against_Requisition ,MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Cost) AS Item_Cost ,MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Desc) AS Item_Desc "
        qry += ",max(TSPL_PURCHASE_ORDER_HEAD.Payment_Terms) as [Payment Terms],max(isnull(TSPL_DELIVERY_TERMS_MASTER.Description,'')) as [Delivery Term],max(TSPL_PURCHASE_ORDER_HEAD.Freight) as Freight"
        qry += " from tspl_purchase_order_detail"
        qry += " LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join TSPL_DELIVERY_TERMS_MASTER on TSPL_DELIVERY_TERMS_MASTER.code=TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code GROUP BY ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, ''),tspl_purchase_order_detail.Item_Code)PO  LEFT JOIN TSPL_GRN_DETAIL ON ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code LEFT JOIN TSPL_GRN_HEAD ON ISNULL(TSPL_GRN_HEAD.grn_no, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '') LEFT JOIN TSPL_MRN_DETAIL ON TSPL_MRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(TSPL_MRN_DETAIL.GRN_Id, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '') AND ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code left outer join (select TSPL_QC_CHECK_DETAIL.*  from TSPL_QC_CHECK_DETAIL left outer join  TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_DETAIL.Document_Code where TSPL_QC_CHECK_HEAD.Posted=1) as  QC on isnull( QC.MRN_No,'')=ISNULL( TSPL_MRN_DETAIL.MRN_No,'') and QC.Item_Code = TSPL_MRN_DETAIL.Item_Code  and ISNULL(QC.PO_No, '')=ISNULL(TSPL_MRN_DETAIL.PO_ID, '') "
        qry += " LEFT JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(PO.PurchaseOrder_No, '') = ISNULL(TSPL_SRN_DETAIL.PO_ID, '') AND ISNULL(TSPL_MRN_DETAIL.mrn_no, '') = ISNULL(TSPL_SRN_DETAIL.mrn_id, '') AND ISNULL(TSPL_GRN_DETAIL.grn_no, '') = ISNULL(TSPL_MRN_DETAIL.grn_id, '') LEFT JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_no LEFT OUTER JOIN TSPL_COMPANY_MASTER ON tspl_company_Master.Comp_Code = PO.comp_code LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = PO.Item_Code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code = TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.code = TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values LEFT OUTER JOIN TSPL_REQUISITION_HEAD ON ISNULL(PO.Against_Requisition, '') = ISNULL(TSPL_REQUISITION_HEAD.Requisition_Id, '') "
        qry += " WHERE 2 = 2 and convert(date,PO.PurchaseOrder_Date,103)>= convert(date,'" & dtpfromdate.Value & "',103) and convert(date,PO.PurchaseOrder_Date,103)<= convert(date,'" & dtpTodate.Value & "',103) and PO.PurchaseOrder_Type <>  'J' )ass1 "
        qry += " where 2=2 "

     
        If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
            qry += " and purchase_no in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine

        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            qry += "  and vendor_no in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If

        qry += " ) final where 2=2 "
        If Not rdobtnall.IsChecked Then
            If rdoPonever.IsChecked = True Then
                qry += " and len(isnull(GRN_NO,''))<=0 "
            End If
            If rdoPOPartial.IsChecked = True Then
                qry += " and [Balance Qty]<>0 and len(isnull(GRN_NO,''))>0 "
            End If
            If rdoCompleted.IsChecked = True Then
                qry += " and POStatus ='Complete' "
            End If
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dt
            SetGridFormatOFGV()
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox7.Enabled = False
        End If

    End Sub
    Sub refereshdata1()

        If chkDoc_select.IsChecked AndAlso cbgDocument.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number", Me.Text)
            Return

        End If
        If chkVendor_select.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor", Me.Text)
            Return
        End If
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please selete one Item", Me.Text)
            Return
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
            Return

        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")

        If clsCommon.CompairString(rbtnDetails.IsChecked, True) = CompairStringResult.Equal Then
            ReferehData(fromdate, todate, chkDoc_select.IsChecked, cbgDocument.CheckedValue, chkVendor_select.IsChecked, cbgVendor.CheckedValue, chkItemSelect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)
        Else
            SummaryData()
        End If

    End Sub
    Sub SetGridFormatOFGV()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If gv.Columns.Contains("Payment Terms") Then
            gv.Columns("Payment Terms").IsVisible = True
            gv.Columns("Payment Terms").Width = 100
        End If
        If gv.Columns.Contains("Delivery Term") Then
            gv.Columns("Delivery Term").IsVisible = True
            gv.Columns("Delivery Term").Width = 100
        End If
        If gv.Columns.Contains("Freight") Then
            gv.Columns("Freight").IsVisible = True
            gv.Columns("Freight").Width = 100
        End If

        If clsCommon.CompairString(rbtnDetails.IsChecked, True) = CompairStringResult.Equal Then

            gv.Columns("item_code").IsVisible = True
            gv.Columns("item_code").Width = 120
            gv.Columns("item_code").HeaderText = "Item"

            gv.Columns("itemdesc").IsVisible = True
            gv.Columns("itemdesc").Width = 120
            gv.Columns("itemdesc").HeaderText = "Item Description"

            gv.Columns("delivaery_date").IsVisible = True
            gv.Columns("delivaery_date").Width = 120
            gv.Columns("delivaery_date").HeaderText = "Delivery Date"

            gv.Columns("uom").IsVisible = True
            gv.Columns("uom").Width = 120
            gv.Columns("uom").HeaderText = "Unit"


            gv.Columns("po_rate").IsVisible = True
            gv.Columns("po_rate").Width = 120
            gv.Columns("po_rate").HeaderText = "Rate"

            gv.Columns("po_qty").IsVisible = True
            gv.Columns("po_qty").Width = 120
            gv.Columns("po_qty").HeaderText = "PO Qty"

            gv.Columns("srn_qty").IsVisible = True
            gv.Columns("srn_qty").Width = 120
            gv.Columns("srn_qty").HeaderText = "SRN QTY"

            'gv.Columns("Balance_Qty").IsVisible = True
            'gv.Columns("Balance_Qty").Width = 120
            'gv.Columns("Balance_Qty").HeaderText = "Balance Qty"

            gv.Columns("Qty1").IsVisible = True
            gv.Columns("Qty1").Width = 120
            gv.Columns("Qty1").HeaderText = "Balance Qty"

            gv.Columns("POstatus").IsVisible = True
            gv.Columns("POstatus").Width = 120
            gv.Columns("POstatus").HeaderText = "PO Status"

            gv.Columns("ClosedBy").IsVisible = True
            gv.Columns("ClosedBy").Width = 120
            gv.Columns("ClosedBy").HeaderText = "Closed By"

            gv.Columns("ClosedDate").IsVisible = True
            gv.Columns("ClosedDate").Width = 120
            gv.Columns("ClosedDate").HeaderText = "ClosedDate"

            gv.Columns("QCQty").IsVisible = True
            gv.Columns("QCQty").Width = 120
            gv.Columns("QCQty").HeaderText = "QC Qty"


            gv.Columns("SRN_No").IsVisible = True
            gv.Columns("SRN_No").Width = 120
            gv.Columns("SRN_No").HeaderText = "SRN No"

            gv.Columns("SRN_Date").IsVisible = True
            gv.Columns("SRN_Date").Width = 120
            gv.Columns("SRN_Date").HeaderText = "SRN_Date"



            gv.Columns("Logo_Img").IsVisible = False
            gv.Columns("Logo_Img").Width = 200
            gv.Columns("Logo_Img").HeaderText = "Logo_Img"


            gv.Columns("Logo_Img2").IsVisible = False
            gv.Columns("Logo_Img2").Width = 200
            gv.Columns("Logo_Img2").HeaderText = "Logo_Img2"

            gv.Columns("comp_code").IsVisible = False
            gv.Columns("comp_code").Width = 70
            gv.Columns("comp_code").HeaderText = "comp_code"

            gv.Columns("Fromdate").IsVisible = False
            gv.Columns("Fromdate").Width = 100
            gv.Columns("Fromdate").HeaderText = "Fromdate"

            gv.Columns("Todate").IsVisible = False
            gv.Columns("Todate").Width = 100
            gv.Columns("Todate").HeaderText = "Todate"

            gv.Columns("strdocno").IsVisible = False
            gv.Columns("strdocno").Width = 100
            gv.Columns("strdocno").HeaderText = "strdocno"

            gv.Columns("strvendor").IsVisible = False
            gv.Columns("strvendor").Width = 120
            gv.Columns("strvendor").HeaderText = "strvendor"

            gv.Columns("strlocation").IsVisible = False
            gv.Columns("strlocation").Width = 120
            gv.Columns("strlocation").HeaderText = "strlocation"

            gv.Columns("stritem").IsVisible = False
            gv.Columns("stritem").Width = 120
            gv.Columns("stritem").HeaderText = "stritem"

            gv.Columns("purchase_no").IsVisible = True
            gv.Columns("purchase_no").Width = 120
            gv.Columns("purchase_no").HeaderText = "PO No"

            gv.Columns("po_date").IsVisible = True
            gv.Columns("po_date").Width = 120
            gv.Columns("po_date").HeaderText = "PO Date"

            gv.Columns("vendor_no").IsVisible = True
            gv.Columns("vendor_no").Width = 120
            gv.Columns("vendor_no").HeaderText = "Vendor No"

            gv.Columns("vendor_name").IsVisible = True
            gv.Columns("vendor_name").Width = 120
            gv.Columns("vendor_name").HeaderText = "Vendor Name"

            gv.Columns("compname").IsVisible = False
            gv.Columns("compname").Width = 120
            gv.Columns("compname").HeaderText = "compname"

            gv.Columns("address1").IsVisible = False
            gv.Columns("address1").Width = 120
            gv.Columns("address1").HeaderText = "address1"

            gv.Columns("location").IsVisible = False
            gv.Columns("location").Width = 120
            gv.Columns("location").HeaderText = "location"

            gv.Columns("location_desc").IsVisible = False
            gv.Columns("location_desc").Width = 120
            gv.Columns("location_desc").HeaderText = "location_desc"

            gv.Columns("grnqty").IsVisible = False
            gv.Columns("grnqty").Width = 120
            gv.Columns("grnqty").HeaderText = "grnqty"

            gv.Columns("mrnqty").IsVisible = False
            gv.Columns("mrnqty").Width = 120
            gv.Columns("mrnqty").HeaderText = "mrnqty"



            gv.Columns("srn_cost").IsVisible = False
            gv.Columns("srn_cost").Width = 120
            gv.Columns("srn_cost").HeaderText = "srn_cost"

            'gv.Columns("City_Code").IsVisible = False
            'gv.Columns("City_Code").Width = 120
            'gv.Columns("City_Code").HeaderText = "City_Code"

            'gv.Columns("Pincode").IsVisible = False
            'gv.Columns("Pincode").Width = 120
            'gv.Columns("Pincode").HeaderText = "Pincode"

            'gv.Columns("state").IsVisible = False
            'gv.Columns("state").Width = 120
            'gv.Columns("state").HeaderText = "state"

            gv.Columns("GRN_NO").IsVisible = True
            gv.Columns("GRN_NO").Width = 120
            gv.Columns("GRN_NO").HeaderText = "GRN_NO"

            gv.Columns("GRN_Date").IsVisible = True
            gv.Columns("GRN_Date").Width = 120
            gv.Columns("GRN_Date").HeaderText = "GRN_Date"

            gv.Columns("Against_Requisition").IsVisible = True
            gv.Columns("Against_Requisition").Width = 120
            gv.Columns("Against_Requisition").HeaderText = "Requisition_No"

            gv.Columns("Requisition_Date").IsVisible = True
            gv.Columns("Requisition_Date").Width = 120
            gv.Columns("Requisition_Date").HeaderText = "Requisition_Date"

            gv.Columns("Short_Qty").IsVisible = True
            gv.Columns("Short_Qty").Width = 120
            gv.Columns("Short_Qty").HeaderText = "Short Qty"

            gv.Columns("Rejected_Qty").IsVisible = True
            gv.Columns("Rejected_Qty").Width = 120
            gv.Columns("Rejected_Qty").HeaderText = "Rejected Qty"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            'Dim item1 As New GridViewSummaryItem("po_qty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("srn_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item5 As New GridViewSummaryItem("grnqty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("mrnqty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)

            Dim item11 As New GridViewSummaryItem("Short_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)

            Dim item12 As New GridViewSummaryItem("Rejected_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            'gv.GroupDescriptors.Add(New GridGroupByExpression("po_date as Item format ""{0}: {1}"" Group By po_date"))
            'gv.GroupDescriptors.Add(New GridGroupByExpression("vendor_no as Item format ""{0}: {1}"" Group By vendor_no"))
            'gv.GroupDescriptors.Add(New GridGroupByExpression("purchase_no as Item format ""{0}: {1}"" Group By purchase_no"))


            gv.ShowGroupPanel = True
            'gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Else
            gv.Columns("purchase_no").IsVisible = True
            gv.Columns("purchase_no").Width = 120
            gv.Columns("purchase_no").HeaderText = "Purchase No"

            gv.Columns("po_date").IsVisible = True
            gv.Columns("po_date").Width = 120
            gv.Columns("po_date").HeaderText = "Purchase Date"

            gv.Columns("POStatus").IsVisible = True
            gv.Columns("POStatus").Width = 120
            gv.Columns("POStatus").HeaderText = "PO Status"

            gv.Columns("SRN_NO").IsVisible = True
            gv.Columns("SRN_NO").Width = 120
            gv.Columns("SRN_NO").HeaderText = "SRN No"

            gv.Columns("SRN_Date").IsVisible = True
            gv.Columns("SRN_Date").Width = 120
            gv.Columns("SRN_Date").HeaderText = "SRN Date"

            gv.Columns("vendor_no").IsVisible = True
            gv.Columns("vendor_no").Width = 120
            gv.Columns("vendor_no").HeaderText = "Vendor Code"

            gv.Columns("vendor_name").IsVisible = True
            gv.Columns("vendor_name").Width = 120
            gv.Columns("vendor_name").HeaderText = "Vendor Name"

            gv.Columns("po_qty").IsVisible = True
            gv.Columns("po_qty").Width = 120
            gv.Columns("po_qty").HeaderText = "PO Qty"

            gv.Columns("grnqty").IsVisible = False
            gv.Columns("grnqty").Width = 120
            gv.Columns("grnqty").HeaderText = "GRN Qty"

            gv.Columns("mrnqty").IsVisible = False
            gv.Columns("mrnqty").Width = 120
            gv.Columns("mrnqty").HeaderText = "MRN Qty"

            gv.Columns("srn_qty").IsVisible = True
            gv.Columns("srn_qty").Width = 120
            gv.Columns("srn_qty").HeaderText = "SRN Qty"

            gv.Columns("QCQty").IsVisible = False
            gv.Columns("QCQty").Width = 120
            gv.Columns("QCQty").HeaderText = "QC Qty"

            gv.Columns("Balance Qty").IsVisible = True
            gv.Columns("Balance Qty").Width = 120
            gv.Columns("Balance Qty").HeaderText = "Balance Qty"

            gv.Columns("Balance_Qty").IsVisible = False
            gv.Columns("Balance_Qty").Width = 120
            gv.Columns("Balance_Qty").HeaderText = "Balance_Qty"

            gv.Columns("srn_cost").IsVisible = False
            gv.Columns("srn_cost").Width = 120
            gv.Columns("srn_cost").HeaderText = "SRN Cost"

            gv.Columns("Short_Qty").IsVisible = True
            gv.Columns("Short_Qty").Width = 120
            gv.Columns("Short_Qty").HeaderText = "Short Qty"

            gv.Columns("Rejected_Qty").IsVisible = True
            gv.Columns("Rejected_Qty").Width = 120
            gv.Columns("Rejected_Qty").HeaderText = "Rejected Qty"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item2 As New GridViewSummaryItem("srn_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item5 As New GridViewSummaryItem("po_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

            Dim item11 As New GridViewSummaryItem("Short_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)

            Dim item12 As New GridViewSummaryItem("Rejected_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            gv.ShowGroupPanel = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        End If
        ReStoreGridLayout()
    End Sub
    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnDetails.IsChecked = True, "D", "S")
        TemplateGridview = gv
        refereshdata1()
        
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column.Name = "purchase_no" Then
                If clsCommon.myLen(gv.CurrentRow.Cells("purchase_no").Value) >= 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, gv.CurrentRow.Cells("purchase_no").Value)
                End If
            End If

            If e.Column.Name = "SRN_NO" Then
                If clsCommon.myLen(gv.CurrentRow.Cells("SRN_No").Value) >= 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, gv.CurrentRow.Cells("SRN_No").Value)
                End If
            End If

            If e.Column.Name = "GRN_NO" Then
                If clsCommon.myLen(gv.CurrentRow.Cells("GRN_NO").Value) >= 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, gv.CurrentRow.Cells("GRN_NO").Value)
                End If
            End If
            ' Against_Requisition

            If e.Column.Name = "Against_Requisition" Then
                If clsCommon.myLen(gv.CurrentRow.Cells("Against_Requisition").Value) >= 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, gv.CurrentRow.Cells("Against_Requisition").Value)
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmPo_action & "'"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")

            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            'If chkLocationSelect.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next
            '    Dim strLocationCode As String = ""
            '    For Each StrCode As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strLocationCode) > 0 Then
            '            strLocationCode += ", "
            '        End If
            '        strLocationCode += StrCode
            '    Next
            '    arrHeader.Add((" Location Name: " + strLocationName + " "))
            'End If
            'If chkItemSelect.IsChecked Then
            '    Dim strItemName As String = ""
            '    For Each StrName As String In cbgItem.CheckedDisplayMember
            '        If clsCommon.myLen(strItemName) > 0 Then
            '            strItemName += ", "
            '        End If
            '        strItemName += StrName
            '    Next
            '    Dim strItemCode As String = ""
            '    For Each StrCode As String In cbgItem.CheckedValue
            '        If clsCommon.myLen(strItemCode) > 0 Then
            '            strItemCode += ", "
            '        End If
            '        strItemCode += StrCode
            '    Next
            '    arrHeader.Add((" Item Name: " + strItemName + " "))
            'End If
            'If chkDoc_select.IsChecked Then
            '    Dim strDocName As String = ""
            '    For Each StrName As String In cbgDocument.CheckedDisplayMember
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
            '    arrHeader.Add((" Document Name: " + strDocName + " "))
            'End If

            'If chkVendor_select.IsChecked Then
            '    Dim strVendorName As String = ""
            '    For Each StrName As String In cbgVendor.CheckedDisplayMember
            '        If clsCommon.myLen(strVendorName) > 0 Then
            '            strVendorName += ", "
            '        End If
            '        strVendorName += StrName
            '    Next
            '    Dim strVendorcode As String = ""
            '    For Each StrCode As String In cbgVendor.CheckedValue
            '        If clsCommon.myLen(strVendorcode) > 0 Then
            '            strVendorcode += ", "
            '        End If
            '        strVendorcode += StrCode
            '    Next
            '    arrHeader.Add(("Vendor Name: " + strVendorName + " "))
            'End If

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
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Purchase Tracking Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Sub LoadDocuemntNo()
        Dim qry As String = "select purchaseorder_no as Code,description as [Description] from TSPL_PURCHASE_ORDER_HEAD"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"
        cbgDocument.DisplayMember = "Invoice_Entry_Date"


    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE Status='N'  order by Vendor_Code"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub
    Public Sub ItemLoad()
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String = "select purchaseorder_no as Code,description as Name from TSPL_PURCHASE_ORDER_HEAD where   convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>= convert(date,'" + dtpfromdate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<= convert(date,'" + dtpTodate.Value + "',103) and PurchaseOrder_Type <>  'J' "
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
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

    Private Sub MyRadioButton2_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
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
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
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
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnclosee_Click(sender As Object, e As EventArgs) Handles btnclosee.Click
        '====Added by preeti gupta Agsainst ticket no[BHA/25/02/19-000821]=
        Me.Close()
    End Sub
End Class
