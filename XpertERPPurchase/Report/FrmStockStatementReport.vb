Imports common

Public Class FrmStockStatementReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.StockStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub FrmStockStatementReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        reset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "STOCK-ST"
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


    Public Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        loadItem()
        LoadLocation()
        LoadCategory()
        LoadSubCategory()
        chkLocAll.IsChecked = True
        chkIAll.IsChecked = True
        chkcatall.IsChecked = True
        chkSubAll.IsChecked = True
    End Sub

    Public Sub loadItem()
        Dim qry As String = " select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER where Item_Type<>'F'"
        cgvItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvItem.ValueMember = "Code"
        cgvItem.DisplayMember = "Description"
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Sub LoadCategory()
        Dim qry As String = " select Category_Code as Code,Category_Name as Description from TSPL_Item_Category "
        cgvCat.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvCat.ValueMember = "Code"
        cgvCat.DisplayMember = "Description"
    End Sub

    Sub LoadSubCategory()
        Dim qry As String = " select Sub_Category_Code as Code, Description   from TSPL_ITEM_SUB_CATEGORY "
        cgvSubCat.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvSubCat.ValueMember = "Code"
        cgvSubCat.DisplayMember = "Description"
    End Sub


    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub chkIAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIAll.ToggleStateChanged
        cgvItem.Enabled = Not chkIAll.IsChecked
    End Sub

    Private Sub chkcatall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcatall.ToggleStateChanged
        cgvCat.Enabled = Not chkcatall.IsChecked
    End Sub

    Private Sub chkSubAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubAll.ToggleStateChanged
        cgvSubCat.Enabled = Not chkSubAll.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Try
            Dim Item As String
            Dim location As String
            Dim SubCategory As String
            Dim Category As String

            Dim StrItem As String = ""
            Dim Strlocation As String = ""
            Dim StrSubCategory As String = ""
            Dim StrCategory As String = ""

            Dim nubee As Integer = 12
            Dim qry As String
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                Strlocation = location.Replace("'", "")
            End If

            If chkIselect.IsChecked Then
                Item = "'" + clsCommon.GetMulcallString(cgvItem.CheckedValue) + "'"
                StrItem = Item.Replace("'", "")
            End If

            If chkcatselect.IsChecked Then
                Category = "'" + clsCommon.GetMulcallString(cgvCat.CheckedValue) + "'"
                StrCategory = Category.Replace("'", "")
            End If

            If chkSubSelect.IsChecked Then
                SubCategory = "'" + clsCommon.GetMulcallString(cgvSubCat.CheckedValue) + "'"
                StrSubCategory = SubCategory.Replace("'", "")
            End If


            ''qry = " select max('" + txtFromDate.Value + "') as CDate ,Category_Code,MAX( Category_Name) as Category_Name, Sub_Category_Code, max(Description) as SubDesc, Item_Code, max(Item_Desc) as Item_Desc, UOM,SUM(Qty) as Qty,max(Comp_Name) as Comp_Name,max(Add1) as Address ,Location_Code,max(Location_Desc) as Location_Desc  from ( " & _
            ''      " select TSPL_Item_Category.Category_Code,TSPL_Item_Category.Category_Name,TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code,TSPL_ITEM_SUB_CATEGORY.Description,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Qty * (case when InOut='I' then 1 else -1 end ) as Qty,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1, TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc " & _
            ''      " from  TSPL_INVENTORY_MOVEMENT " & _
            ''      " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code " & _
            ''      " left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code=TSPL_ITEM_MASTER.Sub_item_category " & _
            ''      " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_SUB_CATEGORY.Category_Code " & _
            ''      " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_INVENTORY_MOVEMENT.Comp_Code " & _
            ''      " left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code  where(2 = 2)   and TSPL_INVENTORY_MOVEMENT.Entry_Date<='" + txtFromDate.Value + "' and  TSPL_ITEM_MASTER.Item_Type<>'F'  "
            Dim Address As String
            Dim dtCompany As DataTable
            Dim dtLoc As DataTable
            Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            dtCompany = clsDBFuncationality.GetDataTable(CompanyQry)
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
                Dim LocAdd = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name ))As LocAdd from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code =" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                dtLoc = clsDBFuncationality.GetDataTable(LocAdd)
                Address = clsCommon.myCstr(dtLoc.Rows(0)("LocAdd"))
            Else
                Address = clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress"))
            End If

            qry = " select max('" + txtFromDate.Value + "') as CDate, '" + StrSubCategory + "' as StrSubCategory,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,'" + StrCategory + "' as StrCategory ,Category_Code,MAX( Category_Name) as Category_Name, Sub_Category_Code, max(Description) as SubDesc, Item_Code, max(Item_Desc) as Item_Desc, UOM,SUM(Qty) as Qty,max(Comp_Name) as Comp_Name,max('" + Address + "') as Address ,Location_Code,max(Location_Desc) as Location_Desc  from ( " & _
                " select TSPL_Item_Category.Category_Code,TSPL_Item_Category.Category_Name,TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code,TSPL_ITEM_SUB_CATEGORY.Description,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Qty * (case when InOut='I' then 1 else -1 end ) as Qty,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1, TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc " & _
                " from  TSPL_INVENTORY_MOVEMENT " & _
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code " & _
                " left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code=TSPL_ITEM_MASTER.Sub_item_category " & _
                " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_SUB_CATEGORY.Category_Code " & _
                " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_INVENTORY_MOVEMENT.Comp_Code " & _
                " left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code  where(2 = 2)   and    convert (date,TSPL_INVENTORY_MOVEMENT.Entry_Date,103) <='" + clsCommon.GetPrintDate((txtFromDate.Value), "yyyy-MM-dd") + "' and  TSPL_ITEM_MASTER.Item_Type<>'F'  "





            If chkLocSelect.IsChecked Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Location_Code in  (	" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"


            End If

            If chkIselect.IsChecked Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in  (" + clsCommon.GetMulcallString(cgvItem.CheckedValue) + ")  "
            End If


            If chkcatselect.IsChecked Then
                qry += " and TSPL_Item_Category.Category_Code in  (" + clsCommon.GetMulcallString(cgvCat.CheckedValue) + ")  "
            End If


            If chkSubSelect.IsChecked Then
                qry += " and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code in  (" + clsCommon.GetMulcallString(cgvSubCat.CheckedValue) + ")  "
            End If



            qry += " ) xxx group by Category_Code,Sub_Category_Code,Item_Code,UOM ,Location_Code"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptStockStatement", "Stock Statement Summary")
                frmCRV = Nothing

            End If









        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmStockStatementReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If

    End Sub
End Class
