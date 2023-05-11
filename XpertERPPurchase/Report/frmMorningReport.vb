Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmMorningReport
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()






    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMorningReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub FrmMorningReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        ItemLoad()
        LoadLocation()
        CategoryLoad()
        SubCategoryLoad()
        drpItemType.SelectedIndex = 0
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Public Sub CategoryLoad()
        qry = "select Category_Code as Code,Category_Name  as Name from TSPL_Item_Category  "
        cbgCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCategory.ValueMember = "Code"
    End Sub
    Public Sub SubCategoryLoad()
        qry = "select sub_Category_Code as Code,Description as Name  from TSPL_ITEM_SUB_CATEGORY  "
        cbgSubCategroy.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubCategroy.ValueMember = "Code"
    End Sub
    Public Sub printdata()
        Try
            If Validatefun() Then
                Dim qry As String
                Dim typeofitem As String = ""
                Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
                Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
                Dim ItemArr As ArrayList = cbgItem.CheckedValue
                Dim CategoryArr As ArrayList = cbgCategory.CheckedValue
                Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
                Dim locationArr As ArrayList = cbgLocation.CheckedValue
                Dim Item As String = ""
                Dim location As String = ""
                Dim Catagory As String = ""
                Dim SubCatagory As String = ""
                Dim StrItem As String = ""
                Dim Strlocation As String = ""
                Dim StrCatagory As String = ""
                Dim StrSubCatagory As String = ""
                Dim type As String = ""
                If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                    location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
                    Strlocation = location.Replace("'", "")
                End If
                If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
                    Item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
                    StrItem = Item.Replace("'", "")
                End If

                If chkCategorySelect.IsChecked And cbgCategory.CheckedValue.Count > 0 Then
                    Catagory = "'" + clsCommon.GetMulcallString(CategoryArr) + "'"
                    StrCatagory = Catagory.Replace("'", "")
                End If

                If chkSubCategroySelect.IsChecked And cbgSubCategroy.CheckedValue.Count > 0 Then
                    SubCatagory = "'" + clsCommon.GetMulcallString(SubCategoryArr) + "'"
                    StrSubCatagory = SubCatagory.Replace("'", "")
                End If
                If drpItemType.Text = "Finished Goods" Then
                    type = "Finished Goods"
                ElseIf drpItemType.Text = "Other" Then
                    type = "Other"
                End If

                If drpItemType.Text = "Finished Goods" Then
                    typeofitem = "=" + "'F'"
                ElseIf drpItemType.Text = "Other" Then
                    typeofitem = "<>" + "'F'"
                End If
                Dim Address As String
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                    Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code = Final .LocationCode  )"
                Else
                    Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "


                End If




                qry = "select '" + fromdate + "' as FromDate ,'" + Todate + "' as ToDate,'" + StrCatagory + "' as StrCatagory ,'" + StrSubCatagory + "' as StrSubCatagory,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem, '" + type + "' as type, Item_Code ,Item_Desc ,UOM, LocationCode, ISNULL("

                qry += " pending_po ,0) as pending_po,subcategory,category_name "
                qry += "  ,'' as locdesc,OPBal ,ReceiveQty,IssueQty ,ClosingBalance ,CompCode,LocationCode,TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2," + Address + "as address "
                qry += "   from (select Item_Code,MAX(Item_Desc) as Item_Desc,UOM,SUM(OpBal) as OpBal,SUM(ReceiveQty) as ReceiveQty,SUM(IssueQty) as IssueQty,SUM(OpBal+ReceiveQty-IssueQty) as ClosingBalance  ,MAX(CompCode)as CompCode,MAX(LocationCode )as LocationCode,SUM(Pending_po) as Pending_po,max(subcategory) as subcategory,max(category_name) as  category_name  "
                qry += "  from(select TSPL_INVENTORY_MOVEMENT .Item_Code,TSPL_INVENTORY_MOVEMENT .Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,0 as ReceiveQty,0 as IssueQty,(Qty*case when InOut='I' then 1 else  case when InOut='O' then -1 else 0 end end) as OpBal,TSPL_INVENTORY_MOVEMENT .Comp_Code as CompCode,TSPL_INVENTORY_MOVEMENT .Location_Code as LocationCode"

                qry += " , ("

                qry += "  select ISNULL( sum(Balance_Qty) ,0)  from TSPL_PURCHASE_ORDER_DETAIL "

                qry += " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "

                qry += "  where TSPL_PURCHASE_ORDER_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code And TSPL_ITEM_MASTER.Morning = 1 And TSPL_ITEM_MASTER.Item_Type"
                qry += "  " + typeofitem + "  and  Convert(Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(Date,'" + dtpToDate.Value + "',103)"
                If cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(locationArr) + ")"
                End If

                If cbgCategory.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Item_Category in (" + clsCommon.GetMulcallString(CategoryArr) + ") "

                End If
                If cbgSubCategroy.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Sub_item_category  in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

                End If


                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                    Return
                ElseIf cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
                End If
                qry += "  ) "
                qry += "  as Pending_po "

                qry += "    ,TSPL_ITEM_SUB_CATEGORY.description as subcategory,TSPL_Item_Category.category_name"

                qry += " from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT .Item_Code =TSPL_ITEM_MASTER .Item_Code and TSPL_INVENTORY_MOVEMENT .UOM =TSPL_ITEM_MASTER .Unit_Code left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Category_Code =TSPL_ITEM_MASTER .item_category and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code =tspl_item_master.Sub_item_category left outer join TSPL_Item_Category  on TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER .item_category  where TSPL_ITEM_MASTER .Morning =1  and TSPL_ITEM_MASTER .Item_Type " + typeofitem + "  and  Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)<Convert(Date,'" + dtpFromdate1.Value + "',103)"

                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
                    qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If

                If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                    Return
                ElseIf cbgCategory.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Item_Category  in (" + clsCommon.GetMulcallString(CategoryArr) + ") "
                End If
                If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Sub Category")
                    Return
                ElseIf cbgSubCategroy.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Sub_item_category in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

                End If


                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                    Return
                ElseIf cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code  in (" + clsCommon.GetMulcallString(ItemArr) + ")"
                End If

                qry += " union all "

                qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,case when InOut='I' then Qty else 0 end as ReceiveQty,case when InOut='O' then Qty else 0 end as IssueQty,0 as OpBal,TSPL_INVENTORY_MOVEMENT .Comp_Code as CompCode,TSPL_INVENTORY_MOVEMENT .Location_Code as LocationCode "
                qry += " , ("

                qry += "  select ISNULL( sum(Balance_Qty) ,0)  from TSPL_PURCHASE_ORDER_DETAIL "

                qry += " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "

                qry += "  where TSPL_PURCHASE_ORDER_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code And TSPL_ITEM_MASTER.Morning = 1 And TSPL_ITEM_MASTER.Item_Type "
                qry += "  " + typeofitem + "  and  Convert(Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(Date,'" + dtpToDate.Value + "',103)"
                If cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(locationArr) + ")"
                End If

                If cbgCategory.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Item_Category in (" + clsCommon.GetMulcallString(CategoryArr) + ") "

                End If
                If cbgSubCategroy.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Sub_item_category  in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

                End If


                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                    Return
                ElseIf cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
                End If
                qry += "  ) "
                qry += "  as Pending_po "
                qry += "    ,TSPL_ITEM_SUB_CATEGORY.description as subcategory,TSPL_Item_Category.category_name"

                qry += " from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT .Item_Code =TSPL_ITEM_MASTER .Item_Code and TSPL_INVENTORY_MOVEMENT .UOM =TSPL_ITEM_MASTER .Unit_Code left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Category_Code =TSPL_ITEM_MASTER .item_category and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code =tspl_item_master.Sub_item_category left outer join TSPL_Item_Category  on TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER .item_category  where TSPL_ITEM_MASTER .Morning =1 and TSPL_ITEM_MASTER .Item_Type  " + typeofitem + " and  Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)<=convert(Date,'" + dtpToDate.Value + "',103)"

                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(locationArr) + ")"
                End If

                If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                    Return
                ElseIf cbgCategory.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Item_Category in (" + clsCommon.GetMulcallString(CategoryArr) + ") "

                End If
                If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Sub Category")
                    Return
                ElseIf cbgSubCategroy.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER .Sub_item_category  in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

                End If


                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                    Return
                ElseIf cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
                End If
                qry += " )xxx group by  Item_Code,UOM) as Final left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =final.CompCode  "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    dt = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "MorningReport", "Morning Report")
                    frmCRV = Nothing

                End If
            Else
                common.clsCommon.MyMessageBoxShow("Please Select Item Type")
            End If


        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printdata()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()

        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        drpItemType.SelectedIndex = 0
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True


    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "MOR-RPT"
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
    Private Function Validatefun() As Boolean
        Dim typeofitem As String = drpItemType.Text
        If typeofitem = "Select" Then
            Return False
        Else
            Return True
        End If


    End Function

    Private Sub chkCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategoryAll.ToggleStateChanged
        cbgCategory.Enabled = Not chkCategoryAll.IsChecked
    End Sub

    Private Sub chkSubCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCategoryAll.ToggleStateChanged
        cbgSubCategroy.Enabled = Not chkSubCategoryAll.IsChecked
    End Sub

    Private Sub FrmMorningReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            printdata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()

        End If


    End Sub
End Class
