Imports common
Imports System.Threading
Imports System.IO

Public Class FrmRptPurchaseReturnBook
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ReportID As String = "PurchaseReturnBook"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.nrptSales)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        ''clsLocation()
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmRptPurchaseReturnBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnExportToExcel, "Press Alt+P for Export to Excel")
        SetUserMgmtNew()

        chkCatgAll.IsChecked = True
        chkSubCatgAll.IsChecked = True
        chkItemall.IsChecked = True
        chkVendorAll.IsChecked = True
        chkPRAll.IsChecked = True
        chkLocAll.IsChecked = True

        ItemLoad()
        CategoryLoad()
        SubCategoryLoad()
        LoadLocation()
        LoadPRInvoice()
        LoadVendor()

        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
       
        ' btnExportToExcel.Visible = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Public Sub ItemLoad()
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Name"
    End Sub
    Public Sub CategoryLoad()
        Dim qry As String = "select Category_Code as Code,Category_Name  as Name from TSPL_Item_Category  "
        cbgCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCategory.ValueMember = "Code"
        cbgCategory.DisplayMember = "Category_Name"

    End Sub
    Public Sub SubCategoryLoad()
        Dim qry As String = "select sub_Category_Code as Code,Description as Name  from TSPL_ITEM_SUB_CATEGORY  "
        cbgSubcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubcategory.ValueMember = "Code"
        cbgSubcategory.DisplayMember = "Description"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Public Sub LoadPRInvoice()
        Dim Qry As String = " select PR_No,CONVERT(varchar(11), PR_Date,103) as PR_Date from TSPL_PR_HEAD "
        cbgPR.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPR.ValueMember = "PR_No"
        cbgPR.DisplayMember = "PR_Date"
    End Sub

    Public Sub LoadVendor()
        Dim Qry As String = "select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Description"
    End Sub
    Private Sub LoadData(ByVal isExportToExcel As Boolean, ByVal exporter As EnumExportTo)
        Try
            gvReport.EnableFiltering = True
            reset()
            Dim location As String
            Dim Item As String
            Dim Vendor As String
            Dim Category As String
            Dim SubCategory As String
            Dim PoReturn As String

            Dim Strlocation As String = ""
            Dim StrItem As String = ""
            Dim StrVendor As String = ""
            Dim StrCategory As String = ""
            Dim StrSubCategory As String = ""
            Dim StrPoReturn As String = ""
            Dim type As String = ""
            gvReport.DataSource = Nothing
            If chkCatgSelect.IsChecked AndAlso cbgCategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Category", Me.Text)
                Exit Sub
            End If

            If chkSubCatgSelect.IsChecked AndAlso cbgSubcategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Sub Category", Me.Text)
                Exit Sub
            End If

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location", Me.Text)
                Exit Sub
            End If

            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Vendor", Me.Text)
                Exit Sub
            End If

            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item", Me.Text)
                Exit Sub
            End If

            If chkPRSelect.IsChecked AndAlso cbgPR.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Purchase Return", Me.Text)
                Exit Sub
            End If
            If rbtnFG.IsChecked Then
                type = "Finished Goods"
            ElseIf rbtnRMOther.IsChecked Then
                type = "others"
            End If

            If chkCatgSelect.IsChecked Then
                Category = "'" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + "'"
                StrCategory = Category.Replace("'", "")
            End If
            If chkSubCatgSelect.IsChecked Then
                SubCategory = "'" + clsCommon.GetMulcallString(cbgSubcategory.CheckedValue) + "'"
                StrSubCategory = SubCategory.Replace("'", "")
            End If
            If chkLocSelect.IsChecked Then
                location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                Strlocation = location.Replace("'", "")
            End If
            If chkVendorSelect.IsChecked Then
                Vendor = "'" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + "'"
                StrVendor = Vendor.Replace("'", "")
            End If
            If chkItemSelect.IsChecked Then
                Item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
                StrItem = Item.Replace("'", "")
            End If
            If chkPRSelect.IsChecked Then
                PoReturn = "'" + clsCommon.GetMulcallString(cbgPR.CheckedValue) + "'"
                StrPoReturn = PoReturn.Replace("'", "")
            End If
            Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt")

            Dim baseQuery As String = "select TSPL_Item_Category.Category_Code as [MainGroupCode],  TSPL_Item_Category.Category_Name as [Main Group],TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code as [GroupCode], TSPL_ITEM_SUB_CATEGORY.Description as [Group Name],TSPL_PR_DETAIL.PR_No,TSPL_PR_HEAD.PR_Date, TSPL_PR_HEAD.Description, TSPL_PR_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc as Location,TSPL_PR_HEAD.Vendor_Code,TSPL_PR_HEAD.Vendor_Name,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Item_Desc,TSPL_PR_DETAIL.PR_Qty , TSPL_PR_DETAIL.Amount,"
            baseQuery += " (case when  TaxM1.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX1_Base_Amt,0)else 0 end  ) as ExciseBaseAmt,"
            baseQuery += " (case when  TaxM1.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX1_Amt,0)else 0 end ) as ExciseAmt,"
            baseQuery += " (case when  TaxM2.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX2_Base_Amt,0)else 0 end  ) as ECessBaseAmt,"
            baseQuery += " (case when  TaxM2.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX2_Amt,0)else 0 end ) as ECessAmt,"
            baseQuery += " (case when  TaxM3.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX3_Base_Amt,0)else 0 end  ) as HCessBaseAmt, "
            baseQuery += " (case when  TaxM3.Type='E' then ISNULL(TSPL_PR_DETAIL.TAX3_Amt,0)else 0 end  ) as HCessAmt,"
            baseQuery += " (case when  TaxM1.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX1_Base_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM2.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX2_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM3.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX3_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM4.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX4_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX5_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM6.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX6_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM7.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX7_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM8.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX8_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM9.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX9_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM10.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX10_Base_Amt,0)else 0 end "
            baseQuery += "  ) as AddTaxBaseAmt,           "
            baseQuery += " (case when  TaxM1.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX1_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM2.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX2_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM3.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX3_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM4.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX4_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX5_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM6.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX6_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM7.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX7_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM8.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX8_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM9.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX9_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM10.Type='A' then ISNULL(TSPL_PR_DETAIL.TAX10_Amt,0)else 0 end "
            baseQuery += " ) as AddTaxAmt,"
            baseQuery += " (case when  TaxM1.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX1_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM2.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX2_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM3.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX3_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM4.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX4_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX5_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM6.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX6_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM7.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX7_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM8.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX8_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM9.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX9_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM10.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX10_Base_Amt,0)else 0 end "
            baseQuery += " ) as VatTaxBaseAmt,           "
            baseQuery += "  (case when  TaxM1.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX1_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM2.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX2_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM3.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX3_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM4.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX4_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX5_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM6.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX6_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM7.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX7_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM8.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX8_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM9.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX9_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM10.Type='V' then ISNULL(TSPL_PR_DETAIL.TAX10_Amt,0)else 0 end "
            baseQuery += "  ) as VatTaxAmt,"
            baseQuery += " (case when  TaxM1.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX1_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM2.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX2_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM3.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX3_Base_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM4.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX4_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX5_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM6.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX6_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM7.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX7_Base_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM8.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX8_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM9.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX9_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM10.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX10_Base_Amt,0)else 0 end "
            baseQuery += "   ) as CSTTaxBaseAmt,"
            baseQuery += "    (case when  TaxM1.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX1_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM2.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX2_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM3.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX3_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM4.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX4_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX5_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM6.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX6_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM7.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX7_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM8.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX8_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM9.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX9_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM10.Type='C' then ISNULL(TSPL_PR_DETAIL.TAX10_Amt,0)else 0 end "
            baseQuery += "  ) as CSTTaxAmt,"
            baseQuery += " (case when  TaxM1.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX1_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM2.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX2_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM3.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX3_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM4.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX4_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM5.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX5_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM6.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX6_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM7.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX7_Base_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM8.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX8_Base_Amt,0)else 0 end "
            baseQuery += "   +case when  TaxM9.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX9_Base_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM10.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX10_Base_Amt,0)else 0 end "
            baseQuery += "  ) as OtherTaxBaseAmt,           "
            baseQuery += "  (case when  TaxM1.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX1_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM2.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX2_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM3.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX3_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM4.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX4_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM5.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX5_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM6.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX6_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM7.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX7_Amt,0)else 0 end "
            baseQuery += " +case when  TaxM8.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX8_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM9.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX9_Amt,0)else 0 end "
            baseQuery += "  +case when  TaxM10.Type='O' then ISNULL(TSPL_PR_DETAIL.TAX10_Amt,0)else 0 end "
            baseQuery += "  ) as OtherTaxAmt"
            baseQuery += "  from TSPL_PR_DETAIL "
            baseQuery += " left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No"
            baseQuery += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PR_DETAIL.Item_Code"
            baseQuery += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER.item_category"
            baseQuery += " left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code=TSPL_ITEM_MASTER.Sub_item_category"
            baseQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=TSPL_PR_DETAIL.TAX1"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=TSPL_PR_DETAIL.TAX2"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=TSPL_PR_DETAIL.TAX3"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=TSPL_PR_DETAIL.TAX4"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=TSPL_PR_DETAIL.TAX5"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=TSPL_PR_DETAIL.TAX6"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=TSPL_PR_DETAIL.TAX7"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=TSPL_PR_DETAIL.TAX8"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=TSPL_PR_DETAIL.TAX9"
            baseQuery += " left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=TSPL_PR_DETAIL.TAX10"
            baseQuery += " where 2=2  and TSPL_PR_HEAD.PR_Date>='" + strFromDate + "' and TSPL_PR_HEAD.PR_Date<='" + strToDate + "'   "

            If rbtnFG.IsChecked Then
                baseQuery += " and TSPL_PR_HEAD.Item_Type='F'"
            ElseIf rbtnRMOther.IsChecked Then
                baseQuery += " and TSPL_PR_HEAD.Item_Type='O'"
            End If

            If chkCatgSelect.IsChecked Then
                baseQuery += " and TSPL_ITEM_MASTER.item_category in (" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + ") "
            End If
            If chkSubCatgSelect.IsChecked Then
                baseQuery += " and TSPL_ITEM_MASTER.Sub_item_category in (" + clsCommon.GetMulcallString(cbgSubcategory.CheckedValue) + ") "
            End If
            If chkLocSelect.IsChecked Then
                baseQuery += " and TSPL_PR_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkVendorSelect.IsChecked Then
                baseQuery += " and TSPL_PR_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
            End If
            If chkItemSelect.IsChecked Then
                baseQuery += " and TSPL_PR_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            End If
            If chkPRSelect.IsChecked Then
                baseQuery += " and TSPL_PR_HEAD.PR_No in (" + clsCommon.GetMulcallString(cbgPR.CheckedValue) + ") "
            End If

            Dim qry As String = "select  MainGroupCode,[Main Group],GroupCode,[Group Name],PR_No,convert(varchar(11), PR_Date,103) as PR_Date, Description, Bill_To_Location,Location,Vendor_Code,Vendor_Name,Item_Code,Item_Desc,PR_Qty,Amount"
            qry += " ,convert(decimal(18,2), (case when ExciseBaseAmt=0 then 0 else (ExciseAmt*100)/ExciseBaseAmt  end)) as ExciseRate,ExciseAmt"
            qry += " ,convert(decimal(18,2),(case when ECessBaseAmt=0 then 0 else (ECessAmt*100)/ECessBaseAmt end)) as ECessRate,ECessAmt"
            qry += " ,convert(decimal(18,2),(case when HCessBaseAmt=0 then 0 else (HCessAmt*100)/HCessBaseAmt end)) as HCessRate,HCessAmt"
            qry += " ,convert(decimal(18,2),(case when AddTaxBaseAmt=0 then 0 else (AddTaxAmt*100)/AddTaxBaseAmt end)) as AddTaxRate,AddTaxAmt"
            qry += " ,convert(decimal(18,2),(case when VatTaxBaseAmt=0 then 0 else (VatTaxAmt*100)/VatTaxBaseAmt end)) as VatTaxRate,VatTaxAmt"
            qry += " ,convert(decimal(18,2),(case when CSTTaxBaseAmt=0 then 0 else (CSTTaxAmt*100)/CSTTaxBaseAmt end)) as CSTTaxRate,CSTTaxAmt"
            qry += " ,convert(decimal(18,2),(case when OtherTaxBaseAmt=0 then 0 else (OtherTaxAmt*100)/OtherTaxBaseAmt end)) as OtherRate,OtherTaxAmt"
            qry += "  ,(Amount+ExciseAmt+ECessAmt+HCessAmt+ AddTaxAmt+VatTaxAmt+CSTTaxAmt+OtherTaxAmt) as Total"
            qry += " from (" + baseQuery + ")xxx order by PR_No,PR_Date"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
                EnableDisableControls(False)
            End If
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
            If isExportToExcel Then
                If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                    Throw New Exception("No Data found to Export")
                End If
                Dim str As String = "Purchase Return Book Report"
                Dim arr As New List(Of String)()
                arr.Add("Purchase return Book Report")
                arr.Add("  From Date:  " + strFromDate + "  To Date: " + strToDate + "   Type:  " + type + "")
                If Strlocation <> "" Then
                    arr.Add(" Location:   " + Strlocation + "")
                End If
                If StrItem <> "" Then
                    arr.Add(" Item:  " + StrItem + "")
                End If
                If StrVendor <> "" Then
                    arr.Add(" Vendor:  " + StrVendor + "")
                End If
                If StrPoReturn <> "" Then
                    arr.Add(" PoReturn:   " + StrPoReturn + "")
                End If
                If StrCategory <> "" Then
                    arr.Add(" Category:  " + StrCategory + "")
                End If
                If StrSubCategory <> "" Then
                    arr.Add(" SubCategory:  " + StrSubCategory + "")
                End If

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcel(str, gvReport, arr, "Purchase Return  Book Report")
                Else
                    clsCommon.MyExportToPDF(str, gvReport, arr, "Purchase Return  Book Report", True)
                End If
                ' clsCommon.MyExportToExcel(str, gvReport, arr, "Purchase Return  Book Report")
                'ExportToExcelGV()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub reset()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()

        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        EnableDisableControls(True)

    End Sub
    Private Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
             
            If chkCatgSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgCategory.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Category : " + strTemp)
            End If
            If chkSubCatgSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgSubcategory.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Sub Category : " + strTemp)
            End If
            If chkVendorSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Vendor : " + strTemp)
            End If
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location : " + strTemp)
            End If

            If chkItemSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Item : " + strTemp)
            End If

            If chkPRSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgPR.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Purchase Return No : " + strTemp)
            End If
            
            clsCommon.MyExportToExcel("Purchase Return Book", gvReport, arrHeader, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem

        gvReport.Columns("MainGroupCode").IsVisible = False
        gvReport.Columns("MainGroupCode").HeaderText = "Category Code"

        gvReport.Columns("Main Group").IsVisible = True
        gvReport.Columns("Main Group").Width = 100
        gvReport.Columns("Main Group").HeaderText = "Category"

        gvReport.Columns("GroupCode").IsVisible = False
        gvReport.Columns("GroupCode").HeaderText = "Sub Category Code"

        gvReport.Columns("Group Name").Width = 100
        gvReport.Columns("Group Name").HeaderText = "Sub Category"
        gvReport.Columns("Group Name").IsVisible = True

        gvReport.Columns("PR_No").Width = 100
        gvReport.Columns("PR_No").HeaderText = "Document No"
        gvReport.Columns("PR_No").IsVisible = True

        gvReport.Columns("PR_Date").Width = 70
        gvReport.Columns("PR_Date").HeaderText = "Document Date"
        gvReport.Columns("PR_Date").IsVisible = True

        gvReport.Columns("Description").Width = 100
        gvReport.Columns("Description").IsVisible = True

        gvReport.Columns("Bill_To_Location").Width = 71
        gvReport.Columns("Bill_To_Location").HeaderText = "Locatioin code"
        gvReport.Columns("Bill_To_Location").IsVisible = True

        gvReport.Columns("Location").IsVisible = True
        gvReport.Columns("Location").Width = 100
        gvReport.Columns("Location").HeaderText = "Location"

        gvReport.Columns("Vendor_Code").IsVisible = True
        gvReport.Columns("Vendor_Code").Width = 70
        gvReport.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gvReport.Columns("Vendor_Name").IsVisible = True
        gvReport.Columns("Vendor_Name").Width = 150
        gvReport.Columns("Vendor_Name").HeaderText = "Vendor"

        gvReport.Columns("Item_Code").IsVisible = True
        gvReport.Columns("Item_Code").Width = 71
        gvReport.Columns("Item_Code").HeaderText = "Item Code"

        gvReport.Columns("Item_Desc").IsVisible = True
        gvReport.Columns("Item_Desc").Width = 150
        gvReport.Columns("Item_Desc").HeaderText = "Item"

        gvReport.Columns("PR_Qty").IsVisible = True
        gvReport.Columns("PR_Qty").Width = 71
        gvReport.Columns("PR_Qty").HeaderText = "Quantity"
        gvReport.Columns("PR_Qty").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("PR_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Amount").IsVisible = True
        gvReport.Columns("Amount").Width = 71
        gvReport.Columns("Amount").HeaderText = "Base Amount"
        gvReport.Columns("Amount").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("ExciseRate").IsVisible = True
        gvReport.Columns("ExciseRate").Width = 71
        gvReport.Columns("ExciseRate").HeaderText = "Excise Rate"
        gvReport.Columns("ExciseRate").FormatString = "{0:F2}"
        
        gvReport.Columns("ExciseAmt").IsVisible = True
        gvReport.Columns("ExciseAmt").Width = 71
        gvReport.Columns("ExciseAmt").HeaderText = "Excise Amount"
        gvReport.Columns("ExciseAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("ExciseAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("ECessRate").IsVisible = True
        gvReport.Columns("ECessRate").Width = 71
        gvReport.Columns("ECessRate").HeaderText = "ECess Rate"
        gvReport.Columns("ECessRate").FormatString = "{0:F2}"

        gvReport.Columns("ECessAmt").IsVisible = True
        gvReport.Columns("ECessAmt").Width = 100
        gvReport.Columns("ECessAmt").HeaderText = "ECess Amount"
        gvReport.Columns("ECessAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("ECessAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("HCessRate").IsVisible = True
        gvReport.Columns("HCessRate").Width = 100
        gvReport.Columns("HCessRate").HeaderText = "HCess Rate"
        gvReport.Columns("HCessRate").FormatString = "{0:F2}"

        gvReport.Columns("HCessAmt").IsVisible = True
        gvReport.Columns("HCessAmt").Width = 100
        gvReport.Columns("HCessAmt").HeaderText = "HCess Amount"
        gvReport.Columns("HCessAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("HCessAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("AddTaxRate").IsVisible = True
        gvReport.Columns("AddTaxRate").Width = 100
        gvReport.Columns("AddTaxRate").HeaderText = "Additional Rate"
        gvReport.Columns("AddTaxRate").FormatString = "{0:F2}"
        
        gvReport.Columns("AddTaxAmt").IsVisible = True
        gvReport.Columns("AddTaxAmt").Width = 100
        gvReport.Columns("AddTaxAmt").HeaderText = "Additional Amount"
        gvReport.Columns("AddTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("AddTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("VatTaxRate").IsVisible = True
        gvReport.Columns("VatTaxRate").Width = 100
        gvReport.Columns("VatTaxRate").HeaderText = "Vat Rate"
        gvReport.Columns("VatTaxRate").FormatString = "{0:F2}"

        gvReport.Columns("VatTaxAmt").IsVisible = True
        gvReport.Columns("VatTaxAmt").Width = 100
        gvReport.Columns("VatTaxAmt").HeaderText = "Vat Amount"
        gvReport.Columns("VatTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("VatTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("CSTTaxRate").IsVisible = True
        gvReport.Columns("CSTTaxRate").Width = 100
        gvReport.Columns("CSTTaxRate").HeaderText = "CST Rate"
        gvReport.Columns("CSTTaxRate").FormatString = "{0:F2}"

        gvReport.Columns("CSTTaxAmt").IsVisible = True
        gvReport.Columns("CSTTaxAmt").Width = 100
        gvReport.Columns("CSTTaxAmt").HeaderText = "CST Amount"
        gvReport.Columns("CSTTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("CSTTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("OtherRate").IsVisible = True
        gvReport.Columns("OtherRate").Width = 100
        gvReport.Columns("OtherRate").HeaderText = "Other Rate"
        gvReport.Columns("OtherRate").FormatString = "{0:F2}"

        gvReport.Columns("OtherTaxAmt").IsVisible = True
        gvReport.Columns("OtherTaxAmt").Width = 100
        gvReport.Columns("OtherTaxAmt").HeaderText = "Other Amount"
        gvReport.Columns("OtherTaxAmt").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("OtherTaxAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Total").IsVisible = True
        gvReport.Columns("Total").Width = 100
        gvReport.Columns("Total").HeaderText = "Total Amount"
        gvReport.Columns("Total").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(False, 1)
        
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ' LoadData(True)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox2.Enabled = val
        grpCategory.Enabled = val
        grpCustomerType.Enabled = val
        grpLocation.Enabled = val
        RadGroupBox3.Enabled = val
        grpTemplate.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub chkCatgAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCatgAll.ToggleStateChanged, chkCatgSelect.ToggleStateChanged
        cbgCategory.Enabled = chkCatgSelect.IsChecked
    End Sub

    Private Sub chkSubCatgSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCatgSelect.ToggleStateChanged, chkSubCatgAll.ToggleStateChanged
        cbgSubcategory.Enabled = chkSubCatgSelect.IsChecked
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged, chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = chkLocSelect.IsChecked
    End Sub

    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorSelect.ToggleStateChanged, chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = chkVendorSelect.IsChecked
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSelect.ToggleStateChanged, chkItemall.ToggleStateChanged
        cbgItem.Enabled = chkItemSelect.IsChecked
    End Sub

    Private Sub chkPRSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPRSelect.ToggleStateChanged, chkPRAll.ToggleStateChanged
        cbgPR.Enabled = chkPRSelect.IsChecked
    End Sub

    Private Sub gvReport_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvReport.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        LoadData(True, EnumExportTo.Excel)

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        LoadData(True, EnumExportTo.PDF)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
