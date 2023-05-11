Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmStockAnalysis1
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmStockAnalysis)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub

    Private Sub FrmStockAnalysis1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkItemAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        chkLocAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        ItemLoad()
        CategoryLoad()
        SubCategoryLoad()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsLocation.GetLocationSegments() commented by Abhishek kumar as on 24 sep 2012
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
    End Sub

    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER where Item_Type <>'F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
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
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategoryAll.ToggleStateChanged
        cbgCategory.Enabled = Not chkCategoryAll.IsChecked
    End Sub
    Private Sub chkSubCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCategoryAll.ToggleStateChanged
        cbgSubCategroy.Enabled = Not chkSubCategoryAll.IsChecked
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ItemLoad()
        CategoryLoad()
        SubCategoryLoad()
        chkItemAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        '' txtLocation.Value = ""
        chkLocAll.IsChecked = True
    End Sub
    ''Public Function ValidateLocation() As Boolean
    ''    If txtLocation.Value = "" Then
    ''        Return False
    ''    Else
    ''        Return True
    ''    End If

    ''End Function
    Public Sub Printdata()
        Try
            'If (ValidateLocation()) Then
            Dim qry As String
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
            'qry = "Select distinct finalcode.openingbalance,'" + fromdate + "'as FromDate,'" + Todate + "'as ToDate, FinalCode .item_category as CategoryCode,FinalCode .Category_Name as Categoryname,FinalCode .Sub_item_category as SubCategoryCode," & _
            '       " finalcode.ItemCode as ItemCode,FinalCode .Item_Desc as ItemDesc,FinalCode .Unit_Code as Uom," & _
            '       " FinalCode .Reciept_Qty as ReciepQty,finalcode.issueQty as IssuedQty,FinalCode .retrunQty as ReturnQty,FinalCode .Cost as itemCost " & _
            '       " from (select xx.Item_Type,  xx .item_category,xx.Sub_item_category  ,TSPL_Item_Category.Category_Name," & _
            '      " TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code,TSPL_ITEM_SUB_CATEGORY.Description," & _
            '       " ItemCode, xx.Item_Desc, xx.Unit_Code,openingbalance, Reciept_Qty, issueQty, retrunQty," & _
            '      " (select (case when sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty,0))<>0 then  isnull(sum(TSPL_ITEM_LOCATION_DETAILS.Amount),0)/(sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty ,0))* (xx.Conversion_Factor))else 0 end) from TSPL_ITEM_LOCATION_DETAILS where TSPL_ITEM_LOCATION_DETAILS.Item_Code =xx.ItemCode and TSPL_ITEM_LOCATION_DETAILS.Location_Code ='" + txtLocation.Value + "') as Cost" & _
            '      " from (select TSPL_ITEM_MASTER.item_category,TSPL_ITEM_MASTER.Sub_item_category,tspl_item_master.Item_Type, tspl_item_master.Item_Code as ItemCode  ,item_desc," & _
            '      " Unit_Code,isnull((select SUM(srn_qty)" & _
            '      " from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No where Bill_To_Location ='" + txtLocation.Value + "' and convert(date,tspl_srn_head.srn_date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,tspl_srn_head.srn_date,103)< =Convert(Date,'" + dtpToDate.Value + "',103)  and Item_Code =TSPL_ITEM_MASTER .Item_Code and UOM_Code =TSPL_ITEM_MASTER .Unit_Code  ),0 )as Reciept_Qty," & _
            '          "isnull((select SUM(srn_qty) from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No where Bill_To_Location ='" + txtLocation.Value + "' and convert(date,tspl_srn_head.srn_date,103)< Convert(Date,'" + dtpFromdate1.Value + "',103)  and Item_Code =TSPL_ITEM_MASTER .Item_Code and UOM_Code =TSPL_ITEM_MASTER .Unit_Code  ),0 )as openingbalance," & _
            '      " isnull((select SUM(Issued_Qty )from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No  where doc_type='Issue' and TSPL_IssueReturn_HEAD .From_Location ='" + txtLocation.Value + "'and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)<=Convert(Date,'" + dtpToDate.Value + "',103)  and TSPL_IssueReturn_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code  ),0 )as issueQty," & _
            '      " isnull((select SUM(Issued_Qty )from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No  where doc_type='Return' and TSPL_IssueReturn_HEAD .From_Location ='" + txtLocation.Value + "'and  convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)<=Convert(Date,'" + dtpToDate.Value + "',103)  and TSPL_IssueReturn_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code ),0 )as retrunQty," & _
            '      " TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_ITEM_MASTER, TSPL_ITEM_UOM_DETAIL " & _
            '      "   where(TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code)" & _
            '       " )as xx " & _
            '      " left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY .Category_Code = xx .item_category " & _
            '      " LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_SUB_CATEGORY .Category_Code) as FinalCode  where 2=2 and FinalCode.Item_Type ='R' and FinalCode.Reciept_Qty >0 "




            'qry = "Select distinct finalcode.openingbalance,'" + fromdate + "' as FromDate,'" + Todate + "'as ToDate,FinalCode .item_category as CategoryCode,FinalCode .Category_Name as Categoryname,FinalCode .Sub_item_category as SubCategoryCode, finalcode.ItemCode as ItemCode,FinalCode .Item_Desc as ItemDesc,FinalCode .Unit_Code as Uom, FinalCode .Reciept_Qty as ReciepQty,finalcode.issueQty as IssuedQty,FinalCode .retrunQty as ReturnQty,FinalCode .Cost as itemCost  from (select xx.Item_Type,  xx .item_category,xx.Sub_item_category  ,TSPL_Item_Category.Category_Name, TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code,TSPL_ITEM_SUB_CATEGORY.Description, ItemCode, xx.Item_Desc, xx.Unit_Code,openingbalance, Reciept_Qty, issueQty, retrunQty, (select (case when sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty,0))<>0 then  isnull(sum(TSPL_ITEM_LOCATION_DETAILS.Amount),0)/(sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty ,0))* (xx.Conversion_Factor))else 0 end) from TSPL_ITEM_LOCATION_DETAILS " & _
            '     "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_ITEM_LOCATION_DETAILS.Location_Code  " & _
            ' " where TSPL_ITEM_LOCATION_DETAILS.Item_Code = xx.ItemCode "
            'If chkLocSelect.IsChecked Then
            '    If cbgLocation.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please select one location ")
            '        Return
            '    End If
            '    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            'End If

            'qry += " ) as Cost  from (" & _
            '           "select TSPL_ITEM_MASTER.item_category,TSPL_ITEM_MASTER.Sub_item_category,tspl_item_master.Item_Type, tspl_item_master.Item_Code as ItemCode  ,item_desc, Unit_Code " & _
            '           ",isnull((select SUM(srn_qty) from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No " & _
            '           "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_SRN_HEAD.Bill_To_Location " & _
            '           "  where convert(date,tspl_srn_head.srn_date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,tspl_srn_head.srn_date,103)< =Convert(Date,'" + dtpToDate.Value + "',103)  and Item_Code =TSPL_ITEM_MASTER .Item_Code and UOM_Code =TSPL_ITEM_MASTER .Unit_Code  ),0 )as Reciept_Qty, " & _
            '           "isnull((select SUM(srn_qty) from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No " & _
            '           " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_SRN_HEAD .Bill_To_Location WHERE 2=2 "
            'If chkLocSelect.IsChecked Then
            '    If cbgLocation.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please select one location ")
            '        Return
            '    End If
            '    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            'End If
            'qry += "and convert(date,tspl_srn_head.srn_date,103)< Convert(Date,'" + dtpFromdate1.Value + "',103)  and Item_Code =TSPL_ITEM_MASTER .Item_Code and UOM_Code =TSPL_ITEM_MASTER .Unit_Code  ),0 )as openingbalance, " & _
            '               "isnull((select SUM(Issued_Qty )from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No  " & _
            '               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_IssueReturn_HEAD.From_Location  " & _
            '               " where doc_type='Issue '"
            'If chkLocSelect.IsChecked Then
            '    If cbgLocation.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please select one location ")
            '        Return
            '    End If
            '    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            'End If
            '' "  and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN ('lwr') " & _
            'qry += " and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) " & _
            '               "  and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)<=Convert(Date,'" + dtpToDate.Value + "',103)  and TSPL_IssueReturn_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code  ),0 )as issueQty " & _
            '              ", isnull((select SUM(Issued_Qty )from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No  " & _
            '                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_IssueReturn_HEAD.From_Location  " & _
            '               "where doc_type='Return' "
            'If chkLocSelect.IsChecked Then
            '    If cbgLocation.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please select one location ")
            '        Return
            '    End If
            '    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            'End If
            ''    "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN ('lwr') " & _
            'qry += "and  convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) " & _
            '               "and convert(date,TSPL_IssueReturn_HEAD.doc_date ,103)<=Convert(Date,'" + dtpToDate.Value + "',103)  " & _
            '               "and TSPL_IssueReturn_DETAIL .Item_Code =TSPL_ITEM_MASTER .Item_Code ),0 )as retrunQty, " & _
            '                 "        TSPL_ITEM_UOM_DETAIL.Conversion_Factor  from  TSPL_ITEM_MASTER, TSPL_ITEM_UOM_DETAIL  where TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  " & _
            '               ")as xx  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY .Category_Code = xx .item_category  LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_SUB_CATEGORY .Category_Code WHERE ISNULL(xx.openingbalance, 0)>0 OR  ISNULL(xx.Reciept_Qty, 0) >0 OR ISNULL(xx.issueQty, 0) >0 OR ISNULL(xx.retrunQty, 0) >0) as FinalCode  " & _
            '               " where 2=2  "




            'If chkItemSelect.IsChecked Then
            '    If cbgItem.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Code.")
            '        Return
            '    End If

            '    qry += " and FinalCode.ItemCode in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            'End If

            'If chkCategorySelect.IsChecked Then
            '    If cbgCategory.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Category Code.")
            '        Return
            '    End If
            '    qry += " and FinalCode.item_category in  (" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + ")  "
            'End If

            'If chkSubCategroySelect.IsChecked Then
            '    If cbgSubCategroy.CheckedValue.Count <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please select atleast one SubCategory Code")
            '    End If
            '    qry += " and finalcode .Sub_item_category in (" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + ")"
            'End If
            '''''''' Added By Abhishek kumar Updated query as on 26/09/2012 '''''''''''''''''''''''''
            If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(locationArr) + "'"
                Strlocation = location.Replace("'", "")
            End If
            If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
                Item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
                StrItem = Item.Replace("'", "")
            End If

            If chkCategorySelect.IsChecked And cbgCategory.CheckedValue.Count > 0 Then
                Catagory = "'" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + "'"
                StrCatagory = Catagory.Replace("'", "")
            End If

            If chkSubCategroySelect.IsChecked And cbgSubCategroy.CheckedValue.Count > 0 Then
                SubCatagory = "'" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + "'"
                StrSubCatagory = SubCatagory.Replace("'", "")
            End If

            qry = "select  finalquery .OpeningBalnc as openingbalance ,'" + fromdate + "'as FromDate,'" + Todate + "'as ToDate,'" + StrCatagory + "' as StrCatagory ,'" + StrSubCatagory + "' as StrSubCatagory,'" + Strlocation + "' as Strlocation,'" + StrItem + "' as StrItem, TSPL_ITEM_MASTER.item_category as CategoryCode,TSPL_Item_Category .Category_Name as Categoryname ," & _
                  " TSPL_ITEM_MASTER.Sub_item_category as SubCategoryCode,finalquery .Item_Code as ItemCode ,finalquery .Item_Desc as ItemDesc ,finalquery .UnitCode as Uom " & _
                  "  ,finalquery .ReceiptQty as ReciepQty ,finalquery .issueQty as IssuedQty ,finalquery .ReturnQty as ReturnQty ," & _
                  "  (select (case when sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty,0))<>0 then  isnull(sum(TSPL_ITEM_LOCATION_DETAILS.Amount),0)/(sum(isnull(TSPL_ITEM_LOCATION_DETAILS.Item_Qty ,0))* (TSPL_ITEM_UOM_DETAIL .Conversion_Factor))else 0 end) from TSPL_ITEM_LOCATION_DETAILS left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Loc_Segment_Code= TSPL_ITEM_LOCATION_DETAILS.Location_Code   where TSPL_ITEM_LOCATION_DETAILS.Item_Code = finalquery .Item_Code "

            qry += "  ) as itemCost," & _
                   " finalquery .location  from   (select sum(xxx.openingbalnc)as OpeningBalnc, xxx.Item_Code ,max(xxx.Item_Desc)as Item_Desc ,SUM(xxx.Qty* case when xxx.Datatype =1 then 1 else 0 end )as ReceiptQty,SUM(xxx.Qty * case when xxx.Datatype =-1 then 1 else 0 end)as issueQty,SUM(xxx.returnQty)as ReturnQty,xxx.UnitCode,max(xxx.location) as location from"

            qry += "( select TSPL_SRN_DETAIL .Item_Code ,TSPL_SRN_DETAIL .Item_Desc, srn_qty as Qty,1 as Datatype, 0 as IssueQty,0 as returnQty," & _
                   "  0 as openingbalnc,TSPL_SRN_DETAIL.Unit_code as UnitCode,TSPL_SRN_HEAD .Bill_To_Location as location" & _
                "	from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_SRN_HEAD.Bill_To_Location" & _
                "   where convert(date,tspl_srn_head.srn_date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,tspl_srn_head.srn_date,103)< =Convert(Date,'" + dtpToDate.Value + "',103)   "
            If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select one location ")
                Return
            End If
            qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            qry += " union all "
            qry += "   select TSPL_ADJUSTMENT_DETAIL.Item_Code ,TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc ,(TSPL_ADJUSTMENT_DETAIL.Item_Quantity) as Qty,(case when TSPL_ADJUSTMENT_HEADER .Trans_Type ='In' then 1 else -1 end) as Datatype, 0 as IssueQty ,0 as returnQty, 0 as openingbalnc," & _
                   " TSPL_ADJUSTMENT_DETAIL.Unit_Code as UnitCode,TSPL_ADJUSTMENT_HEADER .Loc_Code as location   from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER  on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_header.Adjustment_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_ADJUSTMENT_HEADER .Loc_Code" & _
                   "   where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)< =Convert(Date,'" + dtpToDate.Value + "',103)    "
            If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select one location ")
                Return
            End If
            qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            qry += "  union all "

            qry += "   select  TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL .Item_Desc ,(case when   TSPL_IssueReturn_HEAD.Doc_Type ='Issue' then TSPL_IssueReturn_DETAIL.Issued_Qty else 0 end) as Qty," & _
                   " (case when TSPL_IssueReturn_HEAD .Doc_Type ='Issue' then -1 else 1 end) as Datatype ,0 as IssueQty,(case when  TSPL_IssueReturn_HEAD.Doc_Type ='Return' then TSPL_IssueReturn_DETAIL.Issued_Qty else 0 end ) as returnQty," & _
                   " 0 as openingbalnc,TSPL_IssueReturn_DETAIL.Unit_code as UnitCode,TSPL_IssueReturn_HEAD .From_Location as location from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No " & _
                   "   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_IssueReturn_HEAD.From_Location   where  convert(date,TSPL_IssueReturn_HEAD.doc_date,103)>=Convert(Date,'" + dtpFromdate1.Value + "',103)   and convert(date,TSPL_IssueReturn_HEAD.doc_date,103)<=Convert(Date,'" + dtpToDate.Value + "',103)  "
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            qry += "   union all "

            qry += "select TSPL_SRN_DETAIL .Item_Code ,TSPL_SRN_DETAIL .Item_Desc, 0 as Qty,0 as Datatype,0 as IssueQty,0 as returnQty,srn_qty as openingbalnc,TSPL_SRN_DETAIL.Unit_code as UnitCode,TSPL_SRN_HEAD .Bill_To_Location  as location	" & _
                    " 	from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_SRN_HEAD.Bill_To_Location " & _
                   "  where convert(date,tspl_srn_head.srn_date,103) <  Convert(Date,'" + dtpFromdate1.Value + "',103)"
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            qry += "  union all"

            qry += "  select TSPL_ADJUSTMENT_DETAIL.Item_Code , TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc ,0 as Qty,0 as Datatype,0 as IssueQty,0 as returnQty," & _
                   " (TSPL_ADJUSTMENT_DETAIL.Item_Quantity * case when TSPL_ADJUSTMENT_HEADER .Trans_Type ='In' then 1 else -1 end)  as openingbalnc,TSPL_ADJUSTMENT_DETAIL.Unit_Code as UnitCode,TSPL_ADJUSTMENT_HEADER .Loc_Code   as location" & _
                   "   from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER  on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_header.Adjustment_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_ADJUSTMENT_HEADER .Loc_Code" & _
                   "   where  convert(date,TSPL_ADJUSTMENT_HEADER .Adjustment_Date,103) <  Convert(Date,'" + dtpFromdate1.Value + "',103) "
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If
            qry += "  union all"

            qry += "  select TSPL_ADJUSTMENT_DETAIL.Item_Code , TSPL_ADJUSTMENT_DETAIL.Item_Description as Item_Desc ,0 as Qty,0 as Datatype,0 as IssueQty,0 as returnQty," & _
                   " (TSPL_ADJUSTMENT_DETAIL.Item_Quantity * case when TSPL_ADJUSTMENT_HEADER .Trans_Type ='In' then 1 else -1 end)  as openingbalnc,TSPL_ADJUSTMENT_DETAIL.Unit_Code as UnitCode,TSPL_ADJUSTMENT_HEADER .Loc_Code   as location" & _
                   "   from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER  on TSPL_ADJUSTMENT_DETAIL.Adjustment_No =TSPL_ADJUSTMENT_header.Adjustment_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_ADJUSTMENT_HEADER .Loc_Code" & _
                   "   where  convert(date,TSPL_ADJUSTMENT_HEADER .Adjustment_Date,103) <  Convert(Date,'" + dtpFromdate1.Value + "',103) "
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If


            qry += "  union all"

            qry += "  select  TSPL_PR_DETAIL.Item_Code ,TSPL_PR_DETAIL .Item_Desc ,(case when   TSPL_PR_HEAD.Status =0 then TSPL_PR_DETAIL.PR_Qty else 0 end) as Qty, (case when TSPL_PR_HEAD .Status  =0 then -1 else 1 end) as Datatype ,0 as IssueQty,( TSPL_PR_DETAIL .PR_Qty ) as returnQty," & _
                   " 0 as openingbalnc,TSPL_PR_DETAIL.Unit_code as UnitCode,TSPL_PR_HEAD.Bill_To_Location  as location from TSPL_PR_DETAIL join TSPL_PR_HEAD on TSPL_PR_DETAIL .PR_No =TSPL_PR_HEAD .PR_No    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code " & _
                   "    = TSPL_PR_HEAD.Bill_To_Location   where  convert(date,TSPL_PR_HEAD.PR_Date,103)>= Convert(Date,'" + dtpFromdate1.Value + "',103)" & _
                   "     and convert(date,TSPL_PR_HEAD.PR_Date,103)<=  Convert(Date,'" + dtpToDate.Value + "',103) "
            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select one location ")
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Location_code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If

            qry += " union all"

            qry += " select  TSPL_IssueReturn_DETAIL.Item_Code , TSPL_IssueReturn_DETAIL .Item_Desc  ,0 as Qty,0 as Datatype,0 as IssueQty,0 as returnQty," & _
                    " ( TSPL_IssueReturn_DETAIL.Issued_Qty * case when TSPL_IssueReturn_HEAD.Doc_Type='Issue' then 1 else -1 end) as openingbalnc,TSPL_IssueReturn_DETAIL.Unit_code  as UnitCode,TSPL_IssueReturn_HEAD .From_Location   as location " & _
                   " from TSPL_IssueReturn_DETAIL join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL .Doc_No =TSPL_IssueReturn_HEAD .Doc_No   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_code  = TSPL_IssueReturn_HEAD.From_Location where convert(date, TSPL_IssueReturn_HEAD.doc_date,103)< Convert(Date,'" + dtpFromdate1.Value + "',103)" & _
                  "  ) as xxx group by xxx.Item_Code ,xxx.UnitCode)as finalquery left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =finalquery .Item_Code  left outer join  TSPL_ITEM_SUB_CATEGORY   on TSPL_ITEM_SUB_CATEGORY .Category_Code = TSPL_ITEM_MASTER .item_category and tspl_item_sub_category.Sub_Category_Code = TSPL_ITEM_MASTER .sub_item_category" & _
                  "   LEFT outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER .item_category left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code =finalquery .Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code =finalquery .UnitCode  left outer join TSPL_LOCATION_MASTER on finalquery .location =TSPL_LOCATION_MASTER .Location_Code where 2=2  "
            If chkItemSelect.IsChecked Then
                If cbgItem.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Code.")
                    Return
                End If

                qry += " and finalquery.Item_Code in  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            End If

            If chkCategorySelect.IsChecked Then
                If cbgCategory.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Item Category Code.")
                    Return
                End If
                qry += " and TSPL_ITEM_MASTER.item_category in  (" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + ")  "
            End If

            If chkSubCategroySelect.IsChecked Then
                If cbgSubCategroy.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one SubCategory Code")
                End If
                qry += " and TSPL_ITEM_MASTER.Sub_item_category in (" + clsCommon.GetMulcallString(cbgSubCategroy.CheckedValue) + ")"
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "StockAnalysis", "Stock Analysis")
                frmCRV = Nothing
            End If
            '   Else
            ' common.clsCommon.MyMessageBoxShow("Please select Location")
            '     End If

        Catch ex As Exception
            myMessages.myExceptions(ex)

        End Try


    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Printdata()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim query As String = "select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER "
    '    txtLocation.Value = clsCommon.ShowSelectForm("Location", query, "Code", "Location_Type ='Physical'", txtLocation.Value, "Code", isButtonClicked)

    'End Sub


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "STK-AYS-RPT"
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


    Private Sub FrmStockAnalysis1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown


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

  
End Class
