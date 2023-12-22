''--22/06/2012--Updation By--[Pankaj Kumar]--Added a check box(Export To Excell Sheet), And created Qry for Exporting data in 
''--30/10/2012--Updation By--[Pankaj Kumar]--Opng rate was not Correct while Exporting to excel--Qry based Changes------Fwd By--Ranjana Mam---- 
' Update BY abhishek as on 29 oct 2012 4:35 pm For Excel
''''' This check Done BY Abhishek on 2 Nov 2012 10.48 pm For Show data In Excel with Or WithOut Data Added RadioBtn Summary and Detail


Imports common
Imports System.Data.SqlClient

Public Class FrmStoresLedger
    Inherits FrmMainTranScreen
    Dim DtWithValueForExcel As DataTable
    Dim DtWithOutValueForExcel As DataTable
    Dim dtSummaryWithoutValue As DataTable
    Private Sub FrmStoresLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadLocation()
        LoadItem()
        LoadCategory()
        LoadSubCategory()
        Reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnStoresLedger)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Sub LoadItem()
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc  from TSPL_ITEM_MASTER Where Item_Type  <> 'F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgLocation.DisplayMember = "Item_Desc"
    End Sub

    Sub LoadCategory()
        Dim qry As String = " SELECT DISTINCT Category_Code AS [Code], Category_Name AS [Description] FROM  TSPL_Item_Category "
        cbgItmCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItmCategory.ValueMember = "Code"
        cbgItmCategory.DisplayMember = "Description"
    End Sub
    Sub LoadSubCategory()
        Dim qry As String = " SELECT DISTINCT Sub_Category_Code AS [Code], Description FROM  TSPL_ITEM_SUB_CATEGORY "
        cbgItemSubCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItemSubCategory.ValueMember = "Code"
        cbgItemSubCategory.DisplayMember = "Description"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub ItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = True
    End Sub

    Private Sub chkItmCatAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItmCatAll.ToggleStateChanged
        cbgItmCategory.Enabled = False
    End Sub

    Private Sub chkItemCatSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemCatSelect.ToggleStateChanged
        cbgItmCategory.Enabled = True
    End Sub

    Private Sub chkItemSubCatAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSubCatAll.ToggleStateChanged
        cbgItemSubCategory.Enabled = False
    End Sub

    Private Sub chkItemSubCatSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSubCatSelect.ToggleStateChanged
        cbgItemSubCategory.Enabled = True
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkItemAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkItmCatAll.IsChecked = True
        chkItemSubCatAll.IsChecked = True
        rdbDetail.IsChecked = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Dim item As String = ""
        Dim location As String = ""
        Dim itemcategory As String = ""
        Dim itemsubcategory As String = ""
        Dim type As String = ""
        Dim Stritem As String = ""
        Dim Strlocation As String = ""
        Dim Stritemcategory As String = ""
        Dim Stritemsubcategory As String = ""
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            Strlocation = location.Replace("'", "")
            
        End If
        If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
            item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            Stritem = item.Replace("'", "")
        End If
        If chkItemCatSelect.IsChecked = True AndAlso cbgItmCategory.CheckedValue.Count > 0 Then
            itemcategory = "'" + clsCommon.GetMulcallString(cbgItmCategory.CheckedValue) + "'"
            Stritemcategory = itemcategory.Replace("'", "")
        End If
        If chkItemSubCatSelect.IsChecked = True AndAlso cbgItemSubCategory.CheckedValue.Count > 0 Then
            itemsubcategory = "'" + clsCommon.GetMulcallString(cbgItemSubCategory.CheckedValue) + "'"
            Stritemsubcategory = itemsubcategory.Replace("'", "")
        End If
        If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Item Or Select All", Me.Text)
            Return
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Location Or Select All", Me.Text)
            Return
        End If
        If chkItemCatSelect.IsChecked = True AndAlso cbgItmCategory.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Item Category Or Select All", Me.Text)
            Return
        End If
        If chkItemSubCatSelect.IsChecked = True AndAlso cbgItemSubCategory.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Item Sub Category Or Select All", Me.Text)
            Return
        End If

        Try
            Dim strtdate As String = clsCommon.GetPrintDate(dtpFrmDate.Value, "dd-MMM-yyy")
            Dim EndDate As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy")
            Dim PrintDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
            Dim Qry As String
            ''-------------------------Added By--[panakj Kumar]-------For Export Data in Excell Sheet--------Req by-Amit Sir------------
            If chkExport2Exl.Checked = True AndAlso rdbtnSummary.IsChecked = True Then
                Qry = "SELECT TSPL_Item_Category.Category_Name AS [Main Group], TSPL_ITEM_SUB_CATEGORY.Description AS [Name Of Group], [Item Code], [Item Name], UOM,  "
                Qry += " [Opening Qty], [Opening Val], [Opening Rate], [Receipt Qty], [Receipt Val], [Receipt Rate], [Issue Qty], [Issue Val], [Issue Rate], QTY_P, VAL_P, ISS_Q, VAL_I, QTY_IR, VAL_IR, QTY_PR, VAL_PR, [Closing Qty],  "
                Qry += " [Closing Val]  FROM (  SELECT Item_Code AS [Item Code], MAX(Item_Desc) AS [Item Name], UOM, SUM(BalQty) AS [Opening Qty], SUM(BalValue) AS [Opening Val],  "
                Qry += " Case When SUM(BalQty)=0 Then 0 else Convert(Decimal(18,2),SUM(BalValue)/SUM(BalQty), 103) end AS [Opening Rate], SUM(RcptQty) AS [Receipt Qty], SUM(RcptValue) AS [Receipt Val], "
                Qry += " ISNULL((Case When SUM(RcptQty) =0 Then 0 else (SUM(RcptValue)/SUM(RcptQty)) End), 0) AS [Receipt Rate],  "
                Qry += " SUM(IssueQty) AS [Issue Qty], SUM(IssueValue) AS [Issue Val], "
                Qry += " ISNULL((Case When SUM(IssueQty) =0 Then 0 else (SUM(IssueValue)/SUM(IssueQty)) End), 0) AS [Issue Rate],  "
                Qry += " SUM(QTY_P) as [QTY_P], SUM(VAL_P) as VAL_P, SUM(ISS_Q) as ISS_Q, SUM(VAL_I) as VAL_I, SUM(QTY_IR) as QTY_IR, SUM(VAL_IR) as VAL_IR, SUM(QTY_PR) as QTY_PR, SUM(VAL_PR) as VAL_PR, (SUM(BalQty)+SUM(RcptQty)-SUM(IssueQty)) AS [Closing Qty],  (SUM(BalValue)+SUM(RcptValue)-SUM(IssueValue)) AS [Closing Val]  FROM ( "
                Qry += " Select xxxx.Item_Code, xxxx.Item_Desc, UOM, Source_Doc_Date, CONVERT(date,Source_Doc_Date,103) as OrderDate, DocNo ,  "
                Qry += " TSPL_LOCATION_MASTER.Location_Desc , RIType, RI,  (Case When InOut='I' Then 'R'  Else CAse When InOut='O' Then 'I' End end ) as Type , "
                Qry += " ISNULL((Case When inOut='I' Then Qty  End), 0) as RcptQty,  "
                Qry += " ISNULL((Case When inOut='I' Then Net_Cost   End), 0) as RcptValue,  "
                Qry += " ISNULL((Case When inOut='O' Then Qty  End), 0) as IssueQty,  "
                Qry += " ISNULL((Case When inOut='O' Then Net_Cost   End), 0) as IssueValue,  "
                Qry += " ISNULL((Case When inOut='' Then Qty  End), 0) as BalQty,  "
                'Qry += " ISNULL((Case When inOut='' AND Qty>0 Then Net_Cost/Qty else 0 End), 0) as BalRate,  "
                Qry += " ISNULL((Case When inOut='' Then Net_Cost   End), 0) as BalValue,  RowNo, Party, Dept_Desc, Case When RIType='SRN' Then Qty Else 0 End as [QTY_P], Case When RIType='SRN' Then Net_Cost Else 0 End as VAL_P, "
                Qry += " Case When RIType='ISSUE' AND Type='I' Then Qty Else 0 End as ISS_Q, Case When RIType='ISSUE' AND Type='I' Then Net_Cost Else  0 End as VAL_I, 	"
                Qry += " Case When RIType='ISSUE' AND Type='R' Then Qty Else 0 End as QTY_IR, Case When RIType='ISSUE' AND Type='R' Then Net_Cost Else 0 End as VAL_IR, 	"
                Qry += " Case When RIType='Purchase Return' Then Qty Else 0 End as QTY_PR, 	Case When RIType='Purchase Return' Then Net_Cost Else 0 End as VAL_PR  from ( "
                Qry += " Select Item_Code, MAX(Item_Desc) as Item_Desc, UOM,  '' as Source_Doc_Date, Max(Location_Code) as Location, '' as RIType, '' as RI, '' as InOut, "
                Qry += " 'Opening Balance' as [DocNo], SUM(Qty * case when InOut='I' then 1 else -1 end) as Qty,  SUM(Net_Cost * case when InOut='I' then 1 else -1 end) as Net_Cost, "
                Qry += " 0 AS RowNo,  '' as Party, '' as Dept_Desc from   ("
                Qry += " select TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc, TSPL_INVENTORY_MOVEMENT.UOM, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, TSPL_INVENTORY_MOVEMENT.Location_Code,  '' as RIType, '' as RI, TSPL_INVENTORY_MOVEMENT.inOut, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , "
                Qry += " TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost  from TSPL_INVENTORY_MOVEMENT   "
                Qry += " Where CONVERT(Date, Source_Doc_Date, 103)<CONVERT(Date, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103)) xxx Group By Item_Code, xxx.UOM, xxx.Location_Code     "
                Qry += " Union All"
                Qry += " select TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc, TSPL_INVENTORY_MOVEMENT.UOM, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,TSPL_INVENTORY_MOVEMENT.Location_Code as Location, (Case When TSPL_INVENTORY_MOVEMENT.Trans_Type='ISSTRAN' Then TSPL_IssueReturn_HEAD.Doc_Type  else TSPL_INVENTORY_MOVEMENT.Trans_Type end ) as RIType, '' as RI,  TSPL_INVENTORY_MOVEMENT.InOut  as Type, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost, 1 AS RowNo,  TSPL_SRN_HEAD.Vendor_Name as Party, "
                Qry += " ( Case When ISNULL(TSPL_IssueReturn_HEAD.Dept_Desc, '')='' Then'' else TSPL_IssueReturn_HEAD.Dept_Desc End  ) + ( Case When ISNULL(TSPL_SRN_HEAD.Dept_Desc, '')='' Then'' else TSPL_SRN_HEAD.Dept_Desc End  ) as Dept_Desc  from TSPL_INVENTORY_MOVEMENT  "
                Qry += " Left Outer  Join TSPL_SRN_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SRN_HEAD.SRN_No  "
                Qry += " LEFT OUTER JOIN  TSPL_IssueReturn_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No= TSPL_IssueReturn_HEAD.Doc_No "
                Qry += " Where CONVERT(Date, Source_Doc_Date, 103)>= CONVERT(Date, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) And "
                Qry += " CONVERT(Date, Source_Doc_Date, 103)<= CONVERT(Date, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "
                Qry += " )xxxx  Left Outer Join TSPL_LOCATION_MASTER on xxxx.Location=TSPL_LOCATION_MASTER.Location_Code  "
                Qry += " Left Outer Join TSPL_ITEM_MASTER on xxxx.Item_Code=TSPL_ITEM_MASTER.Item_Code  Where 1=1   And Item_Type<>'F' And Location_Type='Physical' "

                If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    Qry += " and xxxx.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                    Qry += " And xxxx.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If
                If chkItemCatSelect.IsChecked = True AndAlso cbgItmCategory.CheckedValue.Count > 0 Then
                    Qry += " AND TSPL_ITEM_MASTER.item_category in (" + clsCommon.GetMulcallString(cbgItmCategory.CheckedValue) + ")"
                End If
                If chkItemSubCatSelect.IsChecked = True AndAlso cbgItemSubCategory.CheckedValue.Count > 0 Then
                    Qry += " AND TSPL_ITEM_MASTER.Sub_item_category in (" + clsCommon.GetMulcallString(cbgItemSubCategory.CheckedValue) + ")"
                End If

                Qry += ") FInal GROUP BY Item_Code, FInal.UOM "
                Qry += ") EEE "
                Qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON [Item Code]=TSPL_ITEM_MASTER.Item_Code"
                Qry += " LEFT OUTER JOIN TSPL_Item_Category ON TSPL_ITEM_MASTER.item_category=TSPL_Item_Category.Category_Code"
                Qry += " LEFT OUTER JOIN TSPL_ITEM_SUB_CATEGORY ON TSPL_ITEM_MASTER.Sub_item_category=TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code WHERE 1=1 "

                Qry += " ORDER BY [Item Code]"
                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(Qry)
                If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dtgv
                End If
                Dim str As String = "StoresLedger Report"
                type = "Summary"
                Dim arr As New List(Of String)()
                arr.Add("StoresLedger Report")
                arr.Add("  From Date:  " + strtdate + "  To Date: " + EndDate + "   Type:   " + type + "")
                If Strlocation <> "" Then
                    arr.Add(" Location:   " + Strlocation + "")
                End If
                If Stritem <> "" Then
                    arr.Add(" Item:  " + Stritem + "")
                End If

                If Stritemcategory <> "" Then
                    arr.Add(" Category:  " + Stritemcategory + "")
                End If
                If Stritemsubcategory <> "" Then
                    arr.Add(" SubCategory:   " + Stritemsubcategory + "")
                End If
                If cbWthoutValue.Checked = True And Not chkNewDetail.Checked Then
                    dtSummaryWithoutValue = clsDBFuncationality.GetDataTable(Qry)
                    dtSummaryWithoutValue.Columns.Remove("Opening Val")
                    dtSummaryWithoutValue.Columns.Remove("Opening Rate")
                    dtSummaryWithoutValue.Columns.Remove("Receipt Val")
                    dtSummaryWithoutValue.Columns.Remove("Receipt Rate")
                    dtSummaryWithoutValue.Columns.Remove("Issue Rate")
                    dtSummaryWithoutValue.Columns.Remove("Issue Val")
                    dtSummaryWithoutValue.Columns.Remove("Closing Val")
                    DtWithOutValueForExcel = dtSummaryWithoutValue
                    clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                ElseIf Not chkNewDetail.Checked Then
                    clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                Else
                    '-[Opening Qty], [Opening Val], [Opening Rate], [Receipt Qty], [Receipt Val], [Receipt Rate], 
                    '[Issue Qty], [Issue Val], [Issue Rate], QTY_P, VAL_P, ISS_Q, VAL_I, QTY_IR, VAL_IR, QTY_PR, VAL_PR, [Closing Qty]
                    dtgv.Columns.Remove("Main Group")
                    dtgv.Columns.Remove("Name Of Group")
                    dtgv.Columns.Remove("Opening Rate")
                    'dtgv.Columns.Remove("Receipt Qty")
                    'dtgv.Columns.Remove("Receipt Val")
                    'dtgv.Columns.Remove("Receipt Rate")
                    'dtgv.Columns.Remove("Issue Qty")
                    'dtgv.Columns.Remove("Issue Val")
                    'dtgv.Columns.Remove("Issue Rate")
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dtgv
                    clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                End If
                ''---------------------------------------------Code Ends Here-------------------------------------------------------------
            Else
                Dim Address As String
                If cbgLocation.CheckedValue.Count = 1 Then
                    Address = "(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) as Address "
                Else
                    Address = "Address"
                End If
                Qry = "Select '" + strtdate + "' as StartDate, '" + EndDate + "' as EndDate, '" + PrintDate + "' as Printdate, '" + Stritem + "' as Stritem, '" + Strlocation + "' as Strlocation, '" + Stritemcategory + "' as Stritemcategory,'" + Stritemsubcategory + "' as Stritemsubcategory,   xxxx.Item_Code, xxxx.Item_Desc, Source_Doc_Date, CONVERT(date,Source_Doc_Date,103) as OrderDate, xxxx.Location_Code, DocNo , TSPL_LOCATION_MASTER.Location_Desc , RIType, RI, (Case When InOut='I' Then 'R'  Else CAse When InOut='O' Then 'I' End end ) as Type ,(Case When inOut='I' Then Qty  End) as RcptQty, (Case When inOut='I' AND Qty>0 Then Net_Cost/Qty else 0 End) as RcptRate, (Case When inOut='I' Then Net_Cost   End) as RcptValue, (Case When inOut='O' Then Qty  End) as IssueQty, (Case When inOut='O' AND Qty>0 Then Net_Cost/Qty else 0 End) as IssueRate, (Case When inOut='O' Then Net_Cost   End) as IssueValue, ISNULL((Case When inOut='' Then Qty  End), 0) as BalQty, ISNULL((Case When inOut='' AND Qty>0 Then Net_Cost/Qty else 0 End), 0) as BalRate, ISNULL((Case When inOut='' Then Net_Cost   End), 0) as BalValue, " + Address + ", RowNo, compName, Party, Dept_Desc, "
                Qry += " Case When RIType='SRN' Then Qty Else 0 End as [QTY_P], Case When RIType='SRN' Then Net_Cost Else 0 End as VAL_P, 	"
                Qry += " Case When RIType='ISSUE' AND Type='I' Then Qty Else 0 End as ISS_Q, 	Case When RIType='ISSUE' AND Type='I' Then Net_Cost End as VAL_I, 	"
                Qry += " Case When RIType='ISSUE' AND Type='R' Then Qty Else 0 End as QTY_IR, 	Case When RIType='ISSUE' AND Type='R' Then Net_Cost Else 0 End as VAL_IR, 	"
                Qry += " Case When RIType='Purchase Return' Then Qty Else 0 End as QTY_PR, 	Case When RIType='Purchase Return' Then Net_Cost Else 0 End as VAL_PR  from " & _
                    " (Select Item_Code, MAX(Item_Desc) as Item_Desc,  '' as Source_Doc_Date, Max(Location_Code) as Location, Max(Location_Desc) as Location_Code, '' as RIType, '' as RI, '' as InOut, 'Opening Balance' as [DocNo], SUM(Qty * case when InOut='I' then 1 else -1 end) as Qty, SUM(Net_Cost * case when InOut='I' then 1 else -1 end) as Net_Cost, MAX(Address) as Address,0 as RowNo, Max(Comp_Name) as  COmpName, '' as Party, '' as Dept_Desc from " & _
                    " (select TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, '' as RIType, '' as RI, TSPL_INVENTORY_MOVEMENT.inOut, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost,(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) as Address, TSPL_COMPANY_MASTER.Comp_Name  from TSPL_INVENTORY_MOVEMENT Left Outer Join TSPL_COMPANY_MASTER on TSPL_INVENTORY_MOVEMENT.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER on TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code Where CONVERT(Date, Source_Doc_Date, 103)<CONVERT(Date, '" + dtpFrmDate.Value.Date + "', 103)) xxx Group By Item_Code, Location_Code  " & _
                    " Union All " & _
                    " select TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc, TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,TSPL_LOCATION_MASTER.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as  Location_Code, (Case When TSPL_INVENTORY_MOVEMENT.Trans_Type='ISSTRAN' Then TSPL_IssueReturn_HEAD.Doc_Type  else TSPL_INVENTORY_MOVEMENT.Trans_Type end ) as RIType, '' as RI,  TSPL_INVENTORY_MOVEMENT.InOut  as Type, TSPL_INVENTORY_MOVEMENT.Source_Doc_No , TSPL_INVENTORY_MOVEMENT.qty, TSPL_INVENTORY_MOVEMENT.Net_Cost,(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) as Address,1 as RowNo, TSPL_COMPANY_MASTER.Comp_Name as COmpName,  TSPL_SRN_HEAD.Vendor_Name as Party, ( Case When ISNULL(TSPL_IssueReturn_HEAD.Dept_Desc, '')='' Then'' else TSPL_IssueReturn_HEAD.Dept_Desc End  ) + ( Case When ISNULL(TSPL_SRN_HEAD.Dept_Desc, '')='' Then'' else TSPL_SRN_HEAD.Dept_Desc End  ) as Dept_Desc  from TSPL_INVENTORY_MOVEMENT Left Outer Join TSPL_COMPANY_MASTER on TSPL_INVENTORY_MOVEMENT.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code Left outer Join TSPL_LOCATION_MASTER on TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer  Join TSPL_SRN_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SRN_HEAD.SRN_No LEFT OUTER JOIN  TSPL_IssueReturn_HEAD on TSPL_INVENTORY_MOVEMENT.Source_Doc_No= TSPL_IssueReturn_HEAD.Doc_No Where CONVERT(Date, Source_Doc_Date, 103)>= CONVERT(Date, '" + dtpFrmDate.Value.Date + "', 103) And CONVERT(Date, Source_Doc_Date, 103)<= CONVERT(Date, '" + dtpToDate.Value.Date + "', 103) " & _
                    " )xxxx Left Outer Join TSPL_LOCATION_MASTER on xxxx.Location_Code=TSPL_LOCATION_MASTER.Location_Desc Left Outer Join TSPL_ITEM_MASTER on xxxx.Item_Code=TSPL_ITEM_MASTER.Item_Code  Where 1=1  "
                If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    Qry += " And xxxx.Location  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                    Qry += " And xxxx.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If
                If chkItemCatSelect.IsChecked = True AndAlso cbgItmCategory.CheckedValue.Count > 0 Then
                    Qry += " AND TSPL_ITEM_MASTER.item_category in (" + clsCommon.GetMulcallString(cbgItmCategory.CheckedValue) + ")"
                End If
                If chkItemSubCatSelect.IsChecked = True AndAlso cbgItemSubCategory.CheckedValue.Count > 0 Then
                    Qry += " AND TSPL_ITEM_MASTER.Sub_item_category in (" + clsCommon.GetMulcallString(cbgItemSubCategory.CheckedValue) + ")"
                End If
                Qry += " And Item_Type<>'F' And Location_Type='Physical' Order by Item_Code,RowNo,OrderDate "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found")
                Else
                    Dim dtFinal As DataTable = New DataTable
                    'Dim location_Code As String = dt.Rows(0)("Location_Code")
                    Dim Item_Code As String = dt.Rows(0)("Item_Code")
                    Dim dblBalQty As Double = 0
                    Dim dblBalValue As Double = 0
                    dtFinal.Columns.Add("Stritem", GetType(String))
                    dtFinal.Columns.Add("Strlocation", GetType(String))
                    dtFinal.Columns.Add("StrItemCategory", GetType(String))
                    dtFinal.Columns.Add("StrItemSubcategory", GetType(String))
                    dtFinal.Columns.Add("startDate", GetType(String))
                    dtFinal.Columns.Add("EndDate", GetType(String))
                    dtFinal.Columns.Add("PrintDate", GetType(String))
                    dtFinal.Columns.Add("Item_Code", GetType(String))
                    dtFinal.Columns.Add("Item_Desc", GetType(String))
                    dtFinal.Columns.Add("Source_Doc_Date", GetType(String))
                    dtFinal.Columns.Add("Location_Code", GetType(String))
                    dtFinal.Columns.Add("DocNo", GetType(String))
                    dtFinal.Columns.Add("Location_Desc", GetType(String))
                    dtFinal.Columns.Add("RIType", GetType(String))
                    dtFinal.Columns.Add("RI", GetType(String))
                    dtFinal.Columns.Add("Type", GetType(String))
                    dtFinal.Columns.Add("RcptQty", GetType(Double))
                    dtFinal.Columns.Add("RcptRate", GetType(Double))
                    dtFinal.Columns.Add("RcptValue", GetType(Double))
                    dtFinal.Columns.Add("IssueQty", GetType(Double))
                    dtFinal.Columns.Add("IssueRate", GetType(Double))
                    dtFinal.Columns.Add("IssueValue", GetType(Double))
                    dtFinal.Columns.Add("BalQty", GetType(Double))
                    dtFinal.Columns.Add("BalRate", GetType(Double))
                    dtFinal.Columns.Add("BalValue", GetType(Double))
                    '--------------------------------------------
                    dtFinal.Columns.Add("QTY_P", GetType(Double))
                    dtFinal.Columns.Add("Rate_P", GetType(Double))
                    dtFinal.Columns.Add("VAL_P", GetType(Double))
                    dtFinal.Columns.Add("ISS_Q", GetType(Double))
                    dtFinal.Columns.Add("VAL_I", GetType(Double))
                    dtFinal.Columns.Add("QTY_IR", GetType(Double))
                    dtFinal.Columns.Add("VAL_IR", GetType(Double))
                    dtFinal.Columns.Add("QTY_PR", GetType(Double))
                    dtFinal.Columns.Add("VAL_PR", GetType(Double))
                    '--------------------------------------------
                    dtFinal.Columns.Add("Address", GetType(String))
                    dtFinal.Columns.Add("RowNo", GetType(Integer))
                    dtFinal.Columns.Add("CompName", GetType(String))
                    dtFinal.Columns.Add("Party", GetType(String))
                    dtFinal.Columns.Add("Dept_Desc", GetType(String))
                    Dim DrFinal As DataRow = dtFinal.NewRow()

                    For Each dr As DataRow In dt.Rows
                        If ((clsCommon.CompairString(dr("Item_Code"), Item_Code))) Then
                            Item_Code = clsCommon.myCstr(dr("Item_Code"))
                            'location_Code = clsCommon.myCstr(dr("location_Code"))
                            dblBalQty = 0
                            dblBalValue = 0
                        End If
                        DrFinal = dtFinal.NewRow()
                        DrFinal.Item("Stritem") = clsCommon.myCstr(dr("Stritem"))
                        DrFinal.Item("Strlocation") = clsCommon.myCstr(dr("Strlocation"))
                        DrFinal.Item("StrItemCategory") = clsCommon.myCstr(dr("StrItemCategory"))
                        DrFinal.Item("StrItemSubcategory") = clsCommon.myCstr(dr("StrItemSubcategory"))
                        DrFinal.Item("startDate") = clsCommon.myCstr(dr("startDate"))
                        DrFinal.Item("EndDate") = clsCommon.myCstr(dr("EndDate"))
                        DrFinal.Item("PrintDate") = clsCommon.myCstr(dr("PrintDate"))
                        DrFinal.Item("Item_Code") = clsCommon.myCstr(dr("Item_Code"))
                        DrFinal.Item("Item_Desc") = clsCommon.myCstr(dr("Item_Desc"))
                        DrFinal.Item("Source_Doc_Date") = clsCommon.myCstr(dr("Source_Doc_Date"))
                        DrFinal.Item("Location_Code") = clsCommon.myCstr(dr("Location_Code"))
                        DrFinal.Item("DocNo") = clsCommon.myCstr(dr("DocNo"))
                        DrFinal.Item("Location_Desc") = clsCommon.myCstr(dr("Location_Desc"))
                        DrFinal.Item("RIType") = clsCommon.myCstr(dr("RIType"))
                        DrFinal.Item("RI") = clsCommon.myCstr(dr("RI"))
                        DrFinal.Item("Type") = clsCommon.myCstr(dr("Type"))
                        '-----------------------------------------------------------
                        DrFinal.Item("QTY_P") = clsCommon.myCdbl(dr("QTY_P"))
                        DrFinal.Item("Rate_P") = clsCommon.myCdbl(dr("VAL_P")) / clsCommon.myCdbl(dr("QTY_P"))
                        DrFinal.Item("VAL_P") = clsCommon.myCdbl(dr("VAL_P"))
                        DrFinal.Item("ISS_Q") = clsCommon.myCdbl(dr("ISS_Q"))
                        DrFinal.Item("VAL_I") = clsCommon.myCdbl(dr("VAL_I"))
                        DrFinal.Item("QTY_IR") = clsCommon.myCdbl(dr("QTY_IR"))
                        DrFinal.Item("VAL_IR") = clsCommon.myCdbl(dr("VAL_IR"))
                        DrFinal.Item("QTY_PR") = clsCommon.myCdbl(dr("QTY_PR"))
                        DrFinal.Item("VAL_PR") = clsCommon.myCdbl(dr("VAL_IR"))
                        '-----------------------------------------------------------
                        DrFinal.Item("RcptQty") = clsCommon.myCdbl(dr("RcptQty"))
                        DrFinal.Item("RcptRate") = clsCommon.myCdbl(dr("RcptRate"))
                        DrFinal.Item("RcptValue") = clsCommon.myCdbl(dr("RcptValue"))
                        DrFinal.Item("IssueQty") = clsCommon.myCdbl(dr("IssueQty"))
                        DrFinal.Item("IssueRate") = clsCommon.myCdbl(dr("IssueRate"))
                        DrFinal.Item("issueValue") = clsCommon.myCdbl(dr("issueValue"))
                        If clsCommon.CompairString(clsCommon.myCstr(DrFinal.Item("Type")), "I") = CompairStringResult.Equal Then
                            dblBalQty = dblBalQty - clsCommon.myCdbl(dr.Item("IssueQty")) + clsCommon.myCdbl(dr.Item("BalQty"))
                            If dblBalQty = 0 Then
                                dblBalValue = 0
                            Else
                                dblBalValue = dblBalValue - clsCommon.myCdbl(dr.Item("issueValue")) + clsCommon.myCdbl(dr.Item("BalValue"))
                                If dblBalValue < 0 Then
                                    dblBalValue = dblBalValue * -1
                                End If
                            End If

                        Else
                            dblBalQty = dblBalQty + clsCommon.myCdbl(dr.Item("RcptQty")) + clsCommon.myCdbl(dr.Item("BalQty"))
                            If dblBalQty = 0 Then
                                dblBalValue = 0
                            Else
                                dblBalValue = dblBalValue + clsCommon.myCdbl(dr.Item("RcptValue")) + clsCommon.myCdbl(dr.Item("BalValue"))
                                If dblBalValue < 0 Then
                                    dblBalValue = dblBalValue * -1
                                End If
                            End If


                        End If
                        DrFinal.Item("Address") = clsCommon.myCstr(dr("Address"))
                        DrFinal.Item("CompName") = clsCommon.myCstr(dr("CompName"))


                        DrFinal.Item("BalQty") = dblBalQty
                        DrFinal.Item("BalValue") = dblBalValue
                        If dblBalQty = 0 Then
                            DrFinal.Item("BalRate") = 0
                        Else
                            DrFinal.Item("BalRate") = dblBalValue / dblBalQty
                        End If
                        DrFinal.Item("Party") = clsCommon.myCstr(dr("Party"))
                        DrFinal.Item("Dept_Desc") = clsCommon.myCstr(dr("Dept_Desc"))
                        dtFinal.Rows.Add(DrFinal)
                    Next



                    If dtFinal IsNot Nothing And dtFinal.Rows.Count > 0 Then

                        gv.DataSource = Nothing
                        gv.Rows.Clear()
                        gv.Columns.Clear()
                        gv.DataSource = dtFinal
                        gridformat()
                    End If

                    Dim str As String = "StoresLedger Report"
                    type = "Detail"
                    Dim arr As New List(Of String)()
                    arr.Add("StoresLedger Report")
                    arr.Add("  From Date:  " + strtdate + "  To Date: " + EndDate + "   Type:   " + type + "")
                    If Strlocation <> "" Then
                        arr.Add(" Location:   " + Strlocation + "")
                    End If
                    If Stritem <> "" Then
                        arr.Add(" Item:  " + Stritem + "")
                    End If

                    If Stritemcategory <> "" Then
                        arr.Add(" Category:  " + Stritemcategory + "")
                    End If
                    If Stritemsubcategory <> "" Then
                        arr.Add(" SubCategory:   " + Stritemsubcategory + "")
                    End If
                    ''''' This check Done BY Abhishek on 1 Nov 2012 7.15 pm For Show data In Excel with Or WithOut Data 
                    Dim frmCRV As New frmCrystalReportViewer()
                    If cbWthoutValue.Checked = True AndAlso chkExport2Exl.Checked = False AndAlso rdbDetail.IsChecked = True And Not chkNewDetail.Checked Then
                        frmCRV.funreport(CrystalReportFolder.Purchase, dtFinal, "crptStoresLedgerWithoutValue", "Stores Ledger Report (Without Value)")
                    ElseIf cbWthoutValue.Checked = True AndAlso chkExport2Exl.Checked = True AndAlso rdbDetail.IsChecked = True Then
                        dtFinal.Columns.Remove("RcptRate")
                        dtFinal.Columns.Remove("RcptValue")
                        dtFinal.Columns.Remove("IssueRate")
                        dtFinal.Columns.Remove("IssueValue")
                        dtFinal.Columns.Remove("BalRate")
                        dtFinal.Columns.Remove("BalValue")
                        dtFinal.Columns.Remove("Stritem")
                        dtFinal.Columns.Remove("Strlocation")
                        dtFinal.Columns.Remove("StrItemCategory")
                        dtFinal.Columns.Remove("StrItemSubcategory")

                        DtWithOutValueForExcel = dtFinal
                        clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                        'ExporttoMyExcel(Nothing, Me, DtWithOutValueForExcel)

                    ElseIf cbWthoutValue.Checked = False AndAlso chkExport2Exl.Checked = True AndAlso rdbDetail.IsChecked = True And Not chkNewDetail.Checked Then
                        dtFinal.Columns.Remove("Stritem")
                        dtFinal.Columns.Remove("Strlocation")
                        dtFinal.Columns.Remove("StrItemCategory")
                        dtFinal.Columns.Remove("StrItemSubcategory")
                        DtWithValueForExcel = dtFinal
                        clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                        'ExporttoMyExcel(Nothing, Me, DtWithValueForExcel)
                    ElseIf cbWthoutValue.Checked = False AndAlso chkExport2Exl.Checked = False AndAlso rdbDetail.IsChecked = True And Not chkNewDetail.Checked Then
                        frmCRV.funreport(CrystalReportFolder.Purchase, dtFinal, "crptStoresLedger", "Stores Ledger Report")
                    ElseIf chkNewDetail.Checked Then
                        dtFinal.Columns.Remove("RcptRate")
                        dtFinal.Columns.Remove("RcptValue")
                        dtFinal.Columns.Remove("IssueRate")
                        dtFinal.Columns.Remove("IssueValue")
                        dtFinal.Columns.Remove("BalRate")
                        dtFinal.Columns.Remove("BalValue")
                        dtFinal.Columns.Remove("Stritem")
                        dtFinal.Columns.Remove("Strlocation")
                        dtFinal.Columns.Remove("StrItemCategory")
                        dtFinal.Columns.Remove("StrItemSubcategory")
                        dtFinal.Columns.Remove("startDate")
                        dtFinal.Columns.Remove("EndDate")
                        dtFinal.Columns.Remove("PrintDate")
                        dtFinal.Columns.Remove("Source_Doc_Date")
                        dtFinal.Columns.Remove("Location_Code")
                        dtFinal.Columns.Remove("DocNo")
                        dtFinal.Columns.Remove("RIType")
                        dtFinal.Columns.Remove("RI")
                        dtFinal.Columns.Remove("Type")
                        dtFinal.Columns.Remove("Address")
                        dtFinal.Columns.Remove("RowNo")
                        dtFinal.Columns.Remove("CompName")
                        dtFinal.Columns.Remove("Party")
                        dtFinal.Columns.Remove("Dept_Desc")
                        clsCommon.MyExportToExcel(str, gv, arr, "StoresLedger Report")
                    End If
                    '''''' Check End Here '''''''''''''''''
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub gridformat()
        Try

         

            gv.AllowAddNewRow = False

            gv.Columns("Stritem").IsVisible = False
            gv.Columns("Strlocation").IsVisible = False
            gv.Columns("StrItemCategory").IsVisible = False
            gv.Columns("StrItemSubcategory").IsVisible = False

            If chkNewDetail.Checked Then

            Else

            End If
            
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm, ByVal Dt As DataTable) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If

        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "StoreLedger"
                frm.Controls.Add(gv)
                If (Dt IsNot Nothing AndAlso Dt.Rows.Count > 0) Then
                    FillGridViewWithDt(Dt, gv)
                Else
                    FillGridView(sql, gv)
                End If

                If gv.Rows.Count = 0 Then
                    Throw New Exception("There is no data for Show Excel Report.")
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.
                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                Throw New Exception(ex.Message)
                Return False
            End Try
        End If
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub

    Private Sub FrmStoresLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    '' Code Added By abhishek as On 1 Nov 2012 7.15 Pm -----
    Private Sub rdbtnSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnSummary.ToggleStateChanged
        chkExport2Exl.Checked = True
        chkExport2Exl.Enabled = False
    End Sub

    Private Sub rdbDetail_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbDetail.ToggleStateChanged
        chkExport2Exl.Checked = False
        chkExport2Exl.Enabled = True
        cbWthoutValue.Enabled = True
        cbWthoutValue.Checked = False
    End Sub

    ''-- Code Added By Abhishek As On 1 Nov 2012 7.15 Pm ----
End Class
