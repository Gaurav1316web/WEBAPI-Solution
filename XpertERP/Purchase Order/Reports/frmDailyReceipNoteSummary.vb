Imports common
' by vipin for ecess value on 5/04/2013 on report
Public Class FrmDailyReceipNoteSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnDailyRcptNoteSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub



    Private Sub FrmDailyReceipNoteSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetUserMgmtNew()
        Reset()
        If objCommonVar.CurrentUserCode <> "ADMIN" Then
            If funSetUserAccess() = False Then Exit Sub
        End If

        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")


    End Sub

    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "DRN-SUM-RPT"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access

            End If
            If strTemp(2) = "0" Then 'Grant modify access

            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
        End Try
    End Function

    Sub Reset()
        rbtnItemWise.Visible = False
        rbtnVendorWise.Visible = False
        dtpfromdate.Value = clsCommon.GETSERVERDATE
        dtptodate.Value = clsCommon.GETSERVERDATE
        rbtnItemWise.IsChecked = True
        chkVendorall.IsChecked = True
        chkLocAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkSubCatAll.IsChecked = True
        LoadLocation()
        LoadVendor()
        LoadItem()
        LoadSubCategory()
    End Sub

#Region "Filters"
    Sub LoadLocation()
        'Commment remove by abhishek kumar as on 19/06/2012
        Dim qry As String = "select Location_Code as CODE,Location_Desc as [DESC] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "CODE"
        cbgLocation.DisplayMember = "DESC"
    End Sub

    Sub LoadVendor()
        Dim qry As String = "Select Vendor_Code, Vendor_Name   from TSPL_VENDOR_MASTER "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub

    Sub LoadItem()
        Dim qry As String = "Select Item_Code, Item_Desc  from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Item_Desc"
    End Sub

    Sub LoadSubCategory()
        Dim qry As String = "Select Sub_Category_Code, Description  from TSPL_ITEM_SUB_CATEGORY"
        cbgSubCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubCategory.ValueMember = "Sub_Category_Code"
        cbgSubCategory.DisplayMember = "Description"
    End Sub

    Private Sub chkVendorall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorall.ToggleStateChanged
        cbgVendor.Enabled = False
    End Sub

    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = True
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = False
    End Sub

    Private Sub chkItemselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemselect.ToggleStateChanged
        cbgItem.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkSubCatAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCatAll.ToggleStateChanged
        cbgSubCategory.Enabled = False
    End Sub

    Private Sub chkSubCatSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCatSelect.ToggleStateChanged
        cbgSubCategory.Enabled = True
    End Sub
#End Region

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub

    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        PrintDailyRcptSummary()
    End Sub

    Sub PrintDailyRcptSummary()
        Dim Item As String = String.Empty
        Dim location As String = String.Empty
        Dim SubCategory As String = String.Empty
        Dim Vendor As String = String.Empty

        Dim StrItem As String = String.Empty
        Dim Strlocation As String = String.Empty
        Dim StrSubCategory As String = String.Empty
        Dim StrVendor As String = String.Empty

        If chkItemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Item Or Select All")
            Exit Sub
        End If
        If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Vendor Or Select All")
            Exit Sub
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Location Or Select All")
            Exit Sub
        End If
        If chkSubCatSelect.IsChecked = True AndAlso cbgSubCategory.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Sub Category Or Select All")
            Exit Sub
        End If

        If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
            Vendor = "'" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + "'"
            StrVendor = Vendor.Replace("'", "")
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")

        End If
        If chkItemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
            Item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            StrItem = Item.Replace("'", "")
        End If
        If chkSubCatSelect.IsChecked = True AndAlso cbgSubCategory.CheckedValue.Count > 0 Then
            SubCategory = "'" + clsCommon.GetMulcallString(cbgSubCategory.CheckedValue) + "'"
            StrSubCategory = SubCategory.Replace("'", "")
        End If
        Dim Address As String
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code where Location_Code = MAX(Location))"
        Else
            Address = "MAX(TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2  end +Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add3  end+Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.City_Code  end+Case When ISNULL(TSPL_COMPANY_MASTER.State ,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State  end+Case When ISNULL(TSPL_COMPANY_MASTER.Pincode ,'')='' Then '' else ' - '+TSPL_COMPANY_MASTER.Pincode  end)"
        End If

        Dim Qry As String = "Select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as RunDate, '" + clsCommon.GetPrintDate(dtpfromdate.Value, "dd-MMM-yyyy") + "' as StartDate, '" + clsCommon.GetPrintDate(dtptodate.Value, "dd-MMM-yyyy") + "' as EndDate,'" + StrSubCategory + "' as StrSubCategory,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem,'" + StrVendor + "' as StrVendor,"
        'If rbtnVendorWise.IsChecked = True Then
        Qry += " Vendor_Code, MAX(Vendor_Name) as VendorName ,"
        'End If
        Qry += " Item_Code, MAX(Item_Desc ) as Itemdesc, MAX(SubCatCode) as SubcatCode, MAX(SubCatDesc) as SubCatDesc, MAX(Itemcat) as Itemcategory ,  SUM(SRN_Qty) as SRNQty, Sum(Amount) as Value, SUM(EXCISEAmt ) as ExciseAmt, SUM(ECESSAmt) as ECESSAmt, SUM(HCESSAmt) as HCESSAmt, SUM(CSTVATAmt) as CSTVATAmt, SUM(AdditionalAmt ) as AdditionalAmt, (SUM(Amount)+SUM(EXCISEAmt )+SUM(ECESSAmt)+SUM(HCESSAmt)+SUM(CSTVATAmt)+SUM(AdditionalAmt )) as TotalValue, MAX(Location) as Location " & _
        ", " + Address + " as Add1, Max(TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name " & _
        " from ( Select TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc,  TSPL_ITEM_MASTER.Sub_item_category as [SubCatCode], TSPL_ITEM_SUB_CATEGORY.Description as [SubCatDesc], TSPL_ITEM_MASTER.item_category as Itemcat  , TSPL_SRN_HEAD.Vendor_Code, TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.Bill_To_Location as Location, " & _
        " (TSPL_SRN_DETAIL.SRN_Qty / isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SRN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SRN_DETAIL.Unit_code ),1)) as SRN_Qty,  TSPL_SRN_DETAIL.Amount, " & _
        " (Case When TM1.Excisable   ='Y' Then TSPL_SRN_DETAIL.TAX1_Amt Else 0 end) as EXCISEAmt, " & _
        " (Case When TM2.Excisable   ='Y' Then TSPL_SRN_DETAIL.TAX2_Amt Else 0 end) as ECESSAmt," & _
        " (Case When TM3.Excisable   ='Y' Then TSPL_SRN_DETAIL.TAX3_Amt Else 0 end) as HCESSAmt, " & _
        " (Case When TM1.Excisable   ='N' Then TSPL_SRN_DETAIL.TAX1_Amt else TSPL_SRN_DETAIL.TAX4_Amt End) as CSTVATAmt, " & _
        "   TSPL_SRN_DETAIL.Total_AddtionalCost_PerUnit * TSPL_SRN_DETAIL.SRN_Qty as AdditionalAmt, TSPL_SRN_HEAD.Comp_Code " & _
        " from TSPL_SRN_DETAIL " & _
        " Left Outer Join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No " & _
        " Left Outer join TSPL_ITEM_MASTER on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        " Left Outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_MASTER.Sub_item_category=TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code " & _
        " Left Outer Join TSPL_TAX_MASTER as TM1 On TSPL_SRN_DETAIL.TAX1=TM1.Tax_Code " & _
        " Left Outer Join TSPL_TAX_MASTER as TM2 on TSPL_SRN_DETAIL.TAX2=TM2.Tax_Code " & _
        " Left Outer Join TSPL_TAX_MASTER as TM3 On TSPL_SRN_DETAIL.TAX1=TM3.Tax_Code " & _
        " Left Outer Join TSPL_TAX_MASTER as TM4 on TSPL_SRN_DETAIL.TAX2=TM4.Tax_Code " & _
        " Left Outer Join TSPL_TAX_MASTER as TM5 On TSPL_SRN_DETAIL.TAX1=TM5.Tax_Code " & _
        "  Where Convert(Date,SRN_Date, 103)>=Convert(Date, '" + dtpfromdate.Value + "', 103) AND Convert(Date,SRN_Date, 103)<=Convert(Date, '" + dtptodate.Value + "', 103)"

        If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
            Qry += " AND TSPL_SRN_HEAD.Vendor_Code IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
            Qry += " And TSPL_SRN_HEAD.Bill_To_Location in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        If chkItemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
            Qry += " AND TSPL_SRN_DETAIL.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        End If
        If chkSubCatSelect.IsChecked = True AndAlso cbgSubCategory.CheckedValue.Count > 0 Then
            Qry += " AND Sub_Category_Code IN (" + clsCommon.GetMulcallString(cbgSubCategory.CheckedValue) + ")"
        End If
        Qry += " )xxx Left Outer Join TSPL_COMPANY_MASTER on xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code"

        'If rbtnVendorWise.IsChecked = True Then
        Qry += " Group by Vendor_Code, Item_Code "
        'Else
        'Qry += " Group by Item_Code "
        'End If

        Try

            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If Dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                'If rbtnItemWise.IsChecked = True Then
                'PurchaseOrderViewer.funreport(Dt, "crptDailyRcptSummaryItemWise", "Daily Receipt Summary")
                'Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, Dt, "crptDailyRcptSummary", "Daily Receipt Summary")
                frmCRV = Nothing
            End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Private Sub FrmDailyReceipNoteSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintDailyRcptSummary()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()

        End If

    End Sub
End Class
