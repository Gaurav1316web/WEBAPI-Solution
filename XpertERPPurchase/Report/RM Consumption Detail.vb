Imports common
Public Class RM_Consumption_Detail
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RM_Consumption_Detail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Private Sub RM_Consumption_Detail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        LoadItem()
        SubCategoryLoad()
        LoadLocation()
        itemall.IsChecked = True
        chkLocAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RM-CONS-RPT"
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

    Sub LoadItem()
        qry = "select Item_Code as[Item Code],item_desc as [Description] from TSPL_ITEM_MASTER  where Item_Type ='R'"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Public Sub SubCategoryLoad()
        qry = "select sub_Category_Code as Code,Description as Name  from TSPL_ITEM_SUB_CATEGORY  "
        cbgSubCategroy.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubCategroy.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()
        LoadItem()
        SubCategoryLoad()
        LoadLocation()
        itemall.IsChecked = True
        chkLocAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        PrintData()
    End Sub
    Sub PrintData()

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
        Dim ItemArr As ArrayList = cbgItem.CheckedValue
        Dim LocArr As ArrayList = cbgLocation.CheckedValue
        Dim SubCatArr As ArrayList = cbgSubCategroy.CheckedValue
        Dim Item As String = ""
        Dim location As String = ""
        Dim SubCategory As String = ""

        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrSubCategory As String = ""

        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(LocArr) + "'"
            Strlocation = location.Replace("'", "")

        End If
      
        If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count > 0 Then
            SubCategory = "'" + clsCommon.GetMulcallString(SubCatArr) + "'"
            StrSubCategory = SubCategory.Replace("'", "")
        End If
        
        If itemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
            StrItem = Item.Replace("'", "")
        End If

        Dim Address As String
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code = xxx1.LocationCode)"
        Else
            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "


        End If

        Try
            qry = "select '" + fromdate + "' as FromDate,'" + todate + "'as ToDate,'" + StrSubCategory + "' as StrSubCategory,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,xxx1.Sub_Category_Code ,xxx1.Description ,xxx1.Item_Code,  xxx1 .item_desc,xxx1.issued_qty ,xxx1.Net_qty, xxx1 .unit_code,TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img as Image1,TSPL_COMPANY_MASTER.Logo_Img2 as Image2," + Address + " as address,xxx1 .LocationCode" & _
             " from (select xxx.Sub_Category_Code, max(xxx.Description)as Description,xxx.item_code,max(xxx.item_desc)as item_desc, sum(xxx.issued_qty) as issued_qty  ,sum(xxx.issued_qty * case when XXX.Doc_Type ='Issue' then 1 else -1 end)as Net_qty,max(xxx.unit_code)as unit_code,max(xxx.Doc_type)as DocType,max(xxx.comp_code )as Comp_code , max(xxx.Doc_Date)as DocumentDate,MAX(xxx.dept)as DeptCode ,Max(xxx.LocationCode )as LocationCode  from " & _
             " (select TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code, TSPL_ITEM_SUB_CATEGORY.Description, issuertn.Item_Code, issuertn.Item_Desc, issuertn.Issued_Qty, issuertn.Unit_code,  issuehd.Doc_Type, issuehd.Comp_code, issuehd.Doc_Date, issuehd.dept,issuehd .From_Location as LocationCode from TSPL_IssueReturn_DETAIL as issuertn   left outer join TSPL_IssueReturn_HEAD as issuehd on issuertn .Doc_No = issuehd .Doc_No  left outer  join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code = issuertn .Item_Code  left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category  LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category  where TSPL_ITEM_MASTER.Item_Type ='R'  and Doc_Type in ('Issue') and Convert(Date,issuehd .Doc_Date,103) >=Convert(Date,'" + dtpfromdate.Value + "',103) and Convert(Date,issuehd .Doc_Date,103) <=Convert(Date,'" + dtptodate.Value + "',103) "
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Return
            ElseIf chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and issuehd. From_Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"


            End If
            If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one SubCategory")
                Return
            ElseIf chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count > 0 Then
                qry += " and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code in (" + clsCommon.GetMulcallString(SubCatArr) + ")"
            End If
            If itemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one ItemCode")
                Return
            ElseIf itemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                qry += " and issuertn.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            End If


            qry += "  )xxx group by xxx.Sub_Category_Code  ,xxx .Item_Code,xxx.Unit_code )xxx1 inner join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = xxx1  .Comp_code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "RMConsumptionBreakup", "RM Consumption Breakup")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub itemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles itemall.ToggleStateChanged, itemselect.ToggleStateChanged
        cbgItem.Enabled = Not itemall.IsChecked = True
    End Sub
    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        funClose()
    End Sub
    Sub funClose()
        Me.Close()
    End Sub
    Private Sub chkSubCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCategoryAll.ToggleStateChanged
        cbgSubCategroy.Enabled = Not chkSubCategoryAll.IsChecked = True
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked = True
    End Sub

    Private Sub RM_Consumption_Detail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub
End Class
