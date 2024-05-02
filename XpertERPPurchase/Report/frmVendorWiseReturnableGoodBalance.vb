Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmVendorWiseReturnableGoodBalance
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorWiseReturnableGoodBalance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub FrmVendorWiseReturnableGoodBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkvendor_All1.IsChecked = True
        rdbtnVendor.IsChecked = True
        chkLocAll.IsChecked = True
        ItemLoad()
        VendorLoad()
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VWBRGB"
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


    Public Sub VendorLoad()
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        cbgVendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor1.ValueMember = "Code"
        cbgVendor1.DisplayMember = "Name"
    End Sub
    Public Sub Printdata()
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim VendorArr As ArrayList = cbgVendor1.CheckedValue
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim Item As String = ""
            Dim location As String = ""
            Dim Vendor As String = ""

            Dim StrItem As String = ""
            Dim Strlocation As String = ""
            Dim StrVendor As String = ""

            Dim type As String = ""
            Dim Address As String = ""
            If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
                Strlocation = location.Replace("'", "")
            End If
            If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
                Item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
                StrItem = Item.Replace("'", "")
            End If

            If rdbtnVendor.IsChecked And cbgVendor1.CheckedValue.Count > 0 Then
                Vendor = "'" + clsCommon.GetMulcallString(VendorArr) + "'"
                StrVendor = Vendor.Replace("'", "")
            End If
            If rdbtnVendor.IsChecked Then
                type = "Vendor"
            ElseIf rdbtnPerson.IsChecked Then
                type = "Person"
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                Address = " (select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code =xxx.Location )"
            Else
                Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "


            End If
            qry = "select final.* from (select RGP_No as RgpSlipNo,convert(date,RGPDate,103) as RGPDate,ItemCode,ItemDesc,VendorCode + ' - ' + VendorName as VendorCode,VendorName,Person + ' - ' + PersonName as Person,PersonName,SendingQty,ReceivedQty,0 as AdjustedQty,Balance as BalanceQty,'" + fromdate + "'as FromDate,'" + Todate + "'as TODate,'" + StrVendor + "' as StrVendor,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem, '" + type + "' as type,CompCode as COMPCODE ,Location,TSPL_COMPANY_MASTER.Comp_Name as compname,  TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2," + Address + " as address " & _
                  "  from(select RGP_No,ItemCode,MAX(ItemDesc) as ItemDesc,VendorCode,MAX(VendorName) as VendorName,MAX(RGPDate) as RGPDate,SUM(Qty * case when RI=1 then 1 else 0 end) as SendingQty,SUM(Qty * case when RI=-1 then 1 else 0 end) as ReceivedQty,SUM(Qty * RI ) as Balance,MAX(COMPCODE )as CompCode,MAX(Location)as Location,MAX(Person) as Person,max(PersonName) as PersonName" & _
                  " from (Select TSPL_RGP_HEAD .RGP_No, (TSPL_RGP_DETAIL .Item_Code)as ItemCode ,(TSPL_RGP_DETAIL .Item_Desc)as ItemDesc, (TSPL_RGP_HEAD .Vendor_Code)as VendorCode,(TSPL_RGP_HEAD .Vendor_Name)as VendorName , (TSPL_RGP_HEAD .RGP_Date) as RGPDate , (isnull(TSPL_RGP_DETAIL .RGP_Qty,0)) as  Qty,(tspl_Rgp_head.Delivered_By ) AS Person,(TSPL_EMPLOYEE_MASTER.Emp_Name  ) AS  PersonName,TSPL_RGP_HEAD.comp_code  as COMPCODE,(TSPL_RGP_HEAD .Location )Location,1 as RI,1 as Chk FROM TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD ON TSPL_RGP_HEAD.RGP_NO=TSPL_RGP_DETAIL.RGP_NO left outer join TSPL_EMPLOYEE_MASTER on TSPL_RGP_HEAD .Delivered_By =TSPL_EMPLOYEE_MASTER .EMP_CODE   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_RGP_HEAD.Location  where TSPL_RGP_HEAD .Status =1 " & _
                    " and TSPL_RGP_HEAD.Doc_Type='RGP' and  Convert(Date,tspl_rgp_head.RGP_Date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103)and Convert(Date,tspl_rgp_head.RGP_Date,103)<=Convert(Date,'" + dtpToDate.Value + "',103) "

            If rdbtnVendor.IsChecked Then
              
                If chk_vendor_select.IsChecked = True AndAlso cbgVendor1.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                    Return
                ElseIf cbgVendor1.CheckedValue.Count > 0 Then
                    qry += " and  TSPL_RGP_HEAD .Vendor_Code  in (" + clsCommon.GetMulcallString(VendorArr) + ")"
                End If
            ElseIf rdbtnPerson.IsChecked Then
                If chk_vendor_select.IsChecked = True AndAlso cbgVendor1.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Person")
                    Return
                ElseIf cbgVendor1.CheckedValue.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD .Delivered_By  in (" + clsCommon.GetMulcallString(VendorArr) + ")"
                End If
            End If
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                Return
            ElseIf cbgItem.CheckedValue.Count > 0 Then
                qry += " and TSPL_RGP_DETAIL .Item_Code in  (" + clsCommon.GetMulcallString(ItemArr) + ")"
            End If

            qry += " UNION ALL"

            qry += " SELECT TSPL_SRN_HEAD.Against_RGP as RGP_No, TSPL_SRN_DETAIL .Item_Code AS ItemCode,'' as ItemDesc,(TSPL_SRN_HEAD .Vendor_Code)as VendorCode,'' as VendorName ,null as RGPDate , (isnull(TSPL_SRN_DETAIL .SRN_Qty,0))AS  Qty,'' AS Person,'' AS  PersonName,'' AS COMPCODE,'' as Location,-1 as RI,0 as Chk" & _
                 " FROM TSPL_SRN_DETAIL LEFT OUTER JOIN TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code = TSPL_SRN_HEAD.Bill_To_Location where LEN( ISNULL( TSPL_SRN_HEAD.Against_RGP,''))>0 "
            If rdbtnVendor.IsChecked Then
                If chk_vendor_select.IsChecked = True AndAlso cbgVendor1.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                    Return
                ElseIf cbgVendor1.CheckedValue.Count > 0 Then
                    qry += " and  TSPL_SRN_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(VendorArr) + ")"
                End If
            End If
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                Return
            ElseIf cbgItem.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_DETAIL .Item_Code in  (" + clsCommon.GetMulcallString(ItemArr) + ")"
            End If
            qry += " )AS XX  GROUP bY RGP_No,ItemCode,VendorCode having SUM(chk)>0 )as xxx  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxx.COMPCODE )final where BalanceQty <> 0 order by final.RGPDate   "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If rdbtnVendor.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "VendorWiseGoodsReturnableBalance", "Vendor Wise Retrunable Goods Balance")
                    ElseIf rdbtnPerson.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PersonWiseGoodsReturnableBalance", "Person Wise Retrunable Goods Balance")
                    End If
                    frmCRV = Nothing
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try



    End Sub
    Public Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkvendor_All1.IsChecked = True
        rdbtnVendor.IsChecked = True
        chkLocAll.IsChecked = True
        ItemLoad()
        VendorLoad()
    End Sub

    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER where Item_Type <>'F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Printdata()
    End Sub

    Private Sub chkvendor_All1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkvendor_All1.ToggleStateChanged
        cbgVendor1.Enabled = Not chkvendor_All1.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub rdbtnPerson_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnPerson.ToggleStateChanged
        If rdbtnPerson.IsChecked Then
            cbgVendor1.DataSource.Clear()
            qry = "select EMP_CODE as Code ,Emp_Name as Name from TSPL_EMPLOYEE_MASTER order by EMP_CODE  "
            cbgVendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgVendor1.ValueMember = "Code"
            cbgVendor1.DisplayMember = "Name"
            RadGroupBox2.Text = "Person"

        ElseIf rdbtnVendor.IsChecked Then
            qry = "select Vendor_Code as Code ,Vendor_Name as Name  from TSPL_VENDOR_MASTER order by Vendor_Code"
            cbgVendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgVendor1.ValueMember = "Code"
            cbgVendor1.DisplayMember = "Name"
            RadGroupBox2.Text = "Vendor"
        End If
    End Sub


    Private Sub FrmVendorWiseReturnableGoodBalance_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            Printdata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(rdbtnVendor.IsChecked = True, "V", "P")
        TemplateGridview = gv
        Printdata()
    End Sub
    Sub FormatGrid()
        
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
           
        Next

        gv.Columns("ItemCode").IsVisible = True
        gv.Columns("ItemCode").Width = 100
        gv.Columns("ItemCode").HeaderText = "Item Code"

        gv.Columns("ItemDesc").IsVisible = True
        gv.Columns("ItemDesc").Width = 100
        gv.Columns("ItemDesc").HeaderText = "Item Name"

        gv.Columns("RGPDate").IsVisible = True
        gv.Columns("RGPDate").Width = 100
        gv.Columns("RGPDate").HeaderText = "Date"
        gv.Columns("RGPDate").FormatString = "{0:d}"

        If rdbtnVendor.IsChecked = True Then
            gv.Columns("VendorCode").IsVisible = True
            gv.Columns("VendorCode").Width = 100
            gv.Columns("VendorCode").HeaderText = "Vendor Code"

            gv.Columns("VendorName").IsVisible = True
            gv.Columns("VendorName").Width = 100
            gv.Columns("VendorName").HeaderText = "Vendor Name"
        ElseIf rdbtnPerson.IsChecked = True Then
            gv.Columns("Person").IsVisible = True
            gv.Columns("Person").Width = 100
            gv.Columns("Person").HeaderText = "Person Code"

            gv.Columns("PersonName").IsVisible = True
            gv.Columns("PersonName").Width = 100
            gv.Columns("PersonName").HeaderText = "Person Name"
        End If

        gv.Columns("RgpSlipNo").IsVisible = True
        gv.Columns("RgpSlipNo").Width = 80
        gv.Columns("RgpSlipNo").HeaderText = "RGP Slip No"

        gv.Columns("SendingQty").IsVisible = True
        gv.Columns("SendingQty").Width = 80
        gv.Columns("SendingQty").HeaderText = "Sending Qty"

        gv.Columns("ReceivedQty").IsVisible = True
        gv.Columns("ReceivedQty").Width = 80
        gv.Columns("ReceivedQty").HeaderText = "Received Qty "

        gv.Columns("BalanceQty").IsVisible = True
        gv.Columns("BalanceQty").Width = 50
        gv.Columns("BalanceQty").HeaderText = "Balance Qty"

        gv.Columns("AdjustedQty").IsVisible = True
        gv.Columns("AdjustedQty").Width = 100
        gv.Columns("AdjustedQty").HeaderText = "Adjustment Qty"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("SendingQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item4 As New GridViewSummaryItem("ReceivedQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("BalanceQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("AdjustedQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        If rdbtnVendor.IsChecked = True Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("VendorCode as Item format ""{0}: {1}"" Group By VendorCode"))
        ElseIf rdbtnPerson.IsChecked = True Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("Person as Item format ""{0}: {1}"" Group By Person"))
        End If

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub print(ByVal exporter As EnumExportTo)

        Dim arr As New List(Of String)()
        arr.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmVendorWiseReturnableGoodBalance & "'"))
        arr.Add("From Date:  " + clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy") + "  To Date: " + clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy"))
        arr.Add("Company : " + objCommonVar.CurrentCompanyName)
   
        Dim VendorArr As ArrayList = cbgVendor1.CheckedDisplayMember
        Dim ItemArr As ArrayList = cbgItem.CheckedDisplayMember
        Dim locationArr As ArrayList = cbgLocation.CheckedDisplayMember
        Dim Item As String = ""
        Dim location As String = ""
        Dim Vendor As String = ""

        Dim StrItem As String = ""
        Dim Strlocation As String = ""
        Dim StrVendor As String = ""

        Dim type As String = ""
        Dim Address As String = ""
        If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
            Strlocation = location.Replace("'", "")
            arr.Add("Location : " + Strlocation)
        End If
        If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
            StrItem = Item.Replace("'", "")
            arr.Add("Item : " + StrItem)
        End If

        If rdbtnVendor.IsChecked And cbgVendor1.CheckedValue.Count > 0 Then
            Vendor = "'" + clsCommon.GetMulcallString(VendorArr) + "'"
            StrVendor = Vendor.Replace("'", "")
            If rdbtnVendor.IsChecked Then
                arr.Add("Vendor : " + StrVendor)
            ElseIf rdbtnPerson.IsChecked Then
                arr.Add("Person : " + StrVendor)
            End If
        End If

        If Exporter = EnumExportTo.Excel Then
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
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arr)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            clsCommon.MyExportToExcelGrid(Me.Text, gv, arr, Me.Text)
        Else
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv, arr, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If

    End Sub
End Class
