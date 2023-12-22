Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==========ADDING THREE LEVEL DRILL DOWN BY SHIVANI AGAINST TICKET [BM00000008905 ]
'==============Created By Preeti Gupta==========
Public Class RptMaterialReceivedAfterJobWork
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim btnReferesh As Boolean = False
    Public strItem As ArrayList
    Public strLocation As ArrayList
    Public strVendor As ArrayList
    Dim arrBack As New List(Of String)
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadRGPType()
        txtLocationMul.arrValueMember = Nothing
        txtVendorMult.arrValueMember = Nothing
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnSummary.IsChecked = True
        cboType.SelectedIndex = 1
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptMaterialReceivedAfterJobWork)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
    End Sub
    Sub LoadRGPType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("code")
        dt.Columns.Add("Name")




        Dim dr As DataRow = dt.NewRow
        dr = dt.NewRow
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Against Job Work"
        dr("Name") = "Against Job Work"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Against BOM"
        dr("Name") = "Against BOM"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Against as is it"
        dr("Name") = "Against as is it"
        dt.Rows.Add(dr)



        ddlRGPType.DataSource = dt
        ddlRGPType.ValueMember = "Code"
        'ddlRGPType.DisplayMember = "All"
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
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMaterialReceivedAfterJobWork & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'If rbtnMCCRouteVLCCSelect.IsChecked Then

            If txtLocationMul.arrDispalyMember IsNot Nothing AndAlso txtLocationMul.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            End If

            If txtVendorMult.arrDispalyMember IsNot Nothing AndAlso txtVendorMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendorMult.arrDispalyMember))
            End If
            If TxtMultiItem.arrDispalyMember IsNot Nothing AndAlso TxtMultiItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Material Received After Job Work", gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Material Received After Job Work", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Public Sub Load_Report()
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            fromDate.Focus()
            Exit Sub
        End If
        Dim sQuery As String = "select 0 as srnqtyforinternaluse,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name, final.Vendor_Code ,final.Bill_To_Location , final.Type ,final.Challan_No ,convert(varchar,final.Challan_Date ,103) as Challan_Date ,"
        sQuery += "final.SRN_No ,convert(varchar,final.SRN_Date,103) as SRN_Date,final.SRN_Item ,final.Item_Desc as SRN_desc,"
        sQuery += " final.SRN_Item +'  Received Item : '+  final.Item_Desc +' Qty : '+  convert(varchar,final.SRN_Qty  )+ ' '+final.SRN_Unit  as Item1, final.SRN_Item +'  Received Item : '+  final.Item_Desc   as Item,"
        sQuery += " final.SRN_Unit ,convert(decimal(18,2),final.SRN_Qty) as SRN_Qty ,final.Cons_Item,TSPL_ITEM_MASTER .Item_Desc as Cons_Item_Desc ,convert(decimal(18,3),final.cnsm_qty) as cnsm_qty,Final.Consu_Unit ,Location_Desc,vendor_name     from ("

        sQuery += "select yy.Vendor_Code ,yy.Bill_To_Location ,yy.type,yy.Challan_No ,yy.Challan_Date ,yy.SRN_No ,yy.SRN_Date ,yy.SRN_Item,yy.Item_Desc,yy.SRN_Qty,yy.SRN_Unit,yy.Cons_Item ,yy.Consu_Unit,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty"
        sQuery += " from("
        sQuery += " select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from "
        sQuery += "(select TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit"
        sQuery += ",TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit"
        sQuery += ",TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item"

        sQuery += " from TSPL_SRN_HEAD"
        sQuery += " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') "
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code "
        sQuery += " left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No "
        sQuery += " left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_HEAD.RGP_Type=TSPL_GRN_HEAD.RGP_Type and TSPL_SRN_HEAD.PurchaseOrder_Type=TSPL_GRN_HEAD.PurchaseOrder_Type"
        sQuery += " left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code  "
        sQuery += " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No  and (case when (TSPL_RGP_HEAD .Against_As_It_Is = 1 and TSPL_RGP_HEAD .Against_JobWork = 1) then 'AI' when (TSPL_RGP_HEAD .Against_As_It_Is = 0 and TSPL_RGP_HEAD .Against_JobWork = 1 and TSPL_RGP_HEAD .Against_BOM = 0) then 'AR' end)=TSPL_GRN_HEAD.RGP_Type"
        sQuery += " left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ"
        sQuery += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit "
        sQuery += " )as YY"
        sQuery += " union all "
        sQuery += " select TSPL_SRN_HEAD.Vendor_Code ,"
        sQuery += " TSPL_SRN_HEAD.Bill_To_Location , 'Against BOM' as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No ,TSPL_SRN_HEAD.SRN_Date ,"
        sQuery += " TSPL_SRN_DETAIL.Item_Code ,tspl_item_master.Item_Desc, TSPL_SRN_DETAIL.SRN_Qty   ,TSPL_SRN_DETAIL.Unit_code,TSPL_RGP_BOM_DETAIL.Item_Code as cnsum_item,"
        sQuery += " TSPL_RGP_BOM_DETAIL.Unit_Code as cnsm_unit,TSPL_RGP_BOM_DETAIL.Qty as cnsm_qty  from TSPL_SRN_HEAD  "
        sQuery += " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No "
        sQuery += "  left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code      "
        sQuery += " left outer join TSPL_RGP_BOM_DETAIL on (TSPL_RGP_BOM_DETAIL.SRN_No=TSPL_SRN_DETAIL.SRN_No or TSPL_RGP_BOM_DETAIL.GRN_No=TSPL_SRN_DETAIL.GRN_ID)"
        sQuery += "  left join (select TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code,max(tspl_item_master.Item_Desc) as Item_Desc  ,TSPL_RGP_BOM_DETAIL.Unit_Code as Unit_Code  ,sum(TSPL_RGP_BOM_DETAIL.qty ) as RGp_Qty from TSPL_RGP_BOM_DETAIL  left join tspl_item_master on tspl_item_master.Item_Code = TSPL_RGP_BOM_DETAIL.Item_Code where InOut ='O' group by TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code ,TSPL_RGP_BOM_DETAIL.Unit_Code) as RGP on TSPL_SRN_HEAD.Vendor_Code =rgp .Vendor_Code and TSPL_RGP_BOM_DETAIL.Item_Code =rgp.Item_Code and TSPL_RGP_BOM_DETAIL.Unit_Code =rgp.Unit_Code  "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type='AB' "
        sQuery += " ) Final left join tspl_item_master on final.Cons_Item=tspl_item_master.Item_Code "
        sQuery += "  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code "
        sQuery += "  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        sQuery += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State left join tspl_location_master on tspl_location_master.Location_Code = final.Bill_To_Location left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = final.Vendor_Code "
        sQuery += "   where 2 = 2 and convert(date,final.SRN_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,final.SRN_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)"

        If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
            sQuery += " and isnull(final.Type,'')<>''"
        End If

        If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
            sQuery += " and final.Type ='" + ddlRGPType.Text + "'"
        End If

        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            sQuery += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            sQuery += " and final.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            sQuery += " and final.SRN_Item  in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        'sQuery += " order by convert(date,final.SRN_Date,103)"
        If clsCommon.CompairString(cboType.SelectedIndex, 1) = CompairStringResult.Equal Then
            Dim qry As String = "" + sQuery + ""
            qry += " order by convert(date,final.SRN_Date,103)"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                ToShowSrnQtyingridonlyonetime()
                FormatGrid()
                If btnReferesh = False Then
                    'PurchaseViewer.funsub(dtgv, "crptMaterialSendForJobWork", "Material Send For Job Work")
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dtgv, "crptmaterialReceivedAfterJobWork", "Material Received After Job Work")
                    frmCRV = Nothing
                End If

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        ElseIf (clsCommon.CompairString(cboType.SelectedIndex, 0) = CompairStringResult.Equal) Then
            Dim qry1 As String = "select  [Location Code], max([location Name]) as [location Name] , [Vendor Code], max([Vendor Name]) as [Vendor Name] ,  sum([SRN Qty]) as [SRN Qty], [SRN Unit] , [SRN Item], max([SRN Item Name] ) as [SRN Item Name] ,Type from (select Bill_To_Location as [Location Code],max(location_desc) as [location Name] ,Vendor_Code as [Vendor Code],max(vendor_name) as [Vendor Name] ,max(SRN_Qty) as  [SRN Qty],SRN_Unit as [SRN Unit] ,SRN_Item as [SRN Item],max(SRN_desc) as [SRN Item Name]  ,Type from (" + sQuery + ")  as ff group by SRN_Item  , Vendor_Code ,Bill_To_Location , Type ,SRN_Unit,SRN_No)  as mm group by [SRN Item] , [Vendor Code]  ,[Location Code]  , Type ,[SRN Unit]  "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry1)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.BestFitColumns()
                gv.ReadOnly = True
                gv.MasterTemplate.SummaryRowsBottom.Clear()
               RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
        End If
        ReStoreGridLayout()
    End Sub
    Sub ToShowSrnQtyingridonlyonetime()

        If gv.Rows.Count > 0 Then
            Dim strouterSrnNo As String = String.Empty
            Dim strinnerSrnNo As String = String.Empty
            strouterSrnNo = clsCommon.myCstr(gv.Rows(0).Cells("SRN_No").Value)
            For i As Integer = 0 To gv.Rows.Count - 1
                strinnerSrnNo = clsCommon.myCstr(gv.Rows(i).Cells("SRN_No").Value)

                If i = 0 Then
                    gv.Rows(i).Cells("srnqtyforinternaluse").Value = clsCommon.myCdbl(gv.Rows(i).Cells("SRN_Qty").Value)
                Else
                    If clsCommon.CompairString(strinnerSrnNo, strouterSrnNo) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(i).Cells("Type").Value), "Against BOM") = CompairStringResult.Equal Then
                        gv.Rows(i).Cells("srnqtyforinternaluse").Value = 0
                    Else
                        gv.Rows(i).Cells("srnqtyforinternaluse").Value = clsCommon.myCdbl(gv.Rows(i).Cells("SRN_Qty").Value)
                    End If
                End If
                strouterSrnNo = clsCommon.myCstr(gv.Rows(i).Cells("SRN_No").Value)



            Next
        End If
    End Sub
    Sub FormatGrid()
        'Dim strItemCode, head2 As String
        gv.SummaryRowsTop.Clear()
        gv.SummaryRowsBottom.Clear()
        gv.GroupDescriptors.Clear()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Type").IsVisible = True
        gv.Columns("Type").Width = 100
        gv.Columns("Type").HeaderText = "Type"

        gv.Columns("SRN_desc").IsVisible = True
        gv.Columns("SRN_desc").Width = 100
        gv.Columns("SRN_desc").HeaderText = "SRN Item Name"

        gv.Columns("SRN_Unit").IsVisible = True
        gv.Columns("SRN_Unit").Width = 100
        gv.Columns("SRN_Unit").HeaderText = "SRN Unit"

        gv.Columns("Challan_No").IsVisible = True
        gv.Columns("Challan_No").Width = 100
        gv.Columns("Challan_No").HeaderText = "Challan No"

        gv.Columns("Challan_Date").IsVisible = True
        gv.Columns("Challan_Date").Width = 100
        gv.Columns("Challan_Date").HeaderText = "Challan Date"

        gv.Columns("SRN_No").IsVisible = True
        gv.Columns("SRN_No").Width = 100
        gv.Columns("SRN_No").HeaderText = "SRN No"

        gv.Columns("SRN_Item").IsVisible = True
        gv.Columns("SRN_Item").Width = 100
        gv.Columns("SRN_Item").HeaderText = "SRN Item"

        gv.Columns("SRN_Qty").IsVisible = True
        gv.Columns("SRN_Qty").Width = 100
        gv.Columns("SRN_Qty").HeaderText = "SRN Qty"


        gv.Columns("Item").IsVisible = True
        gv.Columns("Item").Width = 80
        gv.Columns("Item").HeaderText = "Item"

        gv.Columns("Cons_Item_Desc").IsVisible = True
        gv.Columns("Cons_Item_Desc").Width = 300
        gv.Columns("Cons_Item_Desc").HeaderText = "Consumed Item"

        gv.Columns("cnsm_qty").IsVisible = True
        gv.Columns("cnsm_qty").Width = 80
        gv.Columns("cnsm_qty").HeaderText = "Consumed Qty"

        gv.Columns("Consu_Unit").IsVisible = True
        gv.Columns("Consu_Unit").Width = 80
        gv.Columns("Consu_Unit").HeaderText = "Consumed Unit"

        gv.Columns("srnqtyforinternaluse").IsVisible = False
        gv.Columns("srnqtyforinternaluse").Width = 80
        gv.Columns("srnqtyforinternaluse").HeaderText = "SRN Qty1"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("cnsm_qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("srnqtyforinternaluse", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        'Dim item2 As New GridViewSummaryItem("SRN_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)

        ' gv.GroupDescriptors.Add(New GridGroupByExpression("Item as Item format ""{0}: {1}"" Group By Item"))

        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC as Item format ""{0}: {1}"" Group By VLC"))

        Dim descriptor As New GroupDescriptor
        descriptor.GroupNames.Add("Item", System.ComponentModel.ListSortDirection.Ascending)
        descriptor.Aggregates.Add("Sum(srnqtyforinternaluse)")
        descriptor.Aggregates.Add("max(SRN_Unit)")
        descriptor.Format = "{0}: {1}: {2} {3}"
        gv.GroupDescriptors.Add(descriptor)


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsTop.Add(summaryRowItem)


    End Sub
    Private Sub RptMaterialReceivedAfterJobWork_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P Adding New")
        Reset()
    End Sub

    Private Sub RptMaterialReceivedAfterJobWork_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt And e.KeyCode = Keys.P Then
            Load_Report()
        End If
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

    Private Sub rmDeletelayout_Click(sender As Object, e As EventArgs) Handles rmDeletelayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMaterialReceivedAfterJobWork & "'"))

            If txtLocationMul.arrDispalyMember IsNot Nothing AndAlso txtLocationMul.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            End If

            If txtVendorMult.arrDispalyMember IsNot Nothing AndAlso txtVendorMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendorMult.arrDispalyMember))
            End If
            If TxtMultiItem.arrDispalyMember IsNot Nothing AndAlso TxtMultiItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrDispalyMember))
            End If
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
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtLocationMul__My_Click(sender As Object, e As EventArgs) Handles txtLocationMul._My_Click
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Code in (" + arrLoc + ") "
        txtLocationMul.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMSelESI", qry, "Code", "Name", txtLocationMul.arrValueMember, txtLocationMul.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocationMul, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendorMult__My_Click(sender As Object, e As EventArgs) Handles txtVendorMult._My_Click
        Dim qry As String = "select distinct TSPL_RGP_HEAD.Vendor_Code as [Code],TSPL_VENDOR_MASTER.Vendor_Name as [name] from TSPL_RGP_HEAD" & _
                            " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  WHERE TSPL_VENDOR_MASTER.Status='N' "
        txtVendorMult.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMSelESI", qry, "Code", "Name", txtVendorMult.arrValueMember, txtVendorMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as Code ,Item_Desc as Name from tspl_item_master"
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub
    Sub QryFinal()
        Dim sQuery As String = "select 0 as srnqtyforinternaluse,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name, final.Vendor_Code ,final.Bill_To_Location , final.Type ,final.Challan_No ,convert(varchar,final.Challan_Date ,103) as Challan_Date ,"
        sQuery += "final.SRN_No ,convert(varchar,final.SRN_Date,103) as SRN_Date,final.SRN_Item ,final.Item_Desc as SRN_desc,"
        sQuery += " final.SRN_Item +'  Received Item : '+  final.Item_Desc +' Qty : '+  convert(varchar,final.SRN_Qty  )+ ' '+final.SRN_Unit  as Item1, final.SRN_Item +'  Received Item : '+  final.Item_Desc   as Item,"
        sQuery += " final.SRN_Unit ,convert(decimal(18,2),final.SRN_Qty) as SRN_Qty ,final.Cons_Item,TSPL_ITEM_MASTER .Item_Desc as Cons_Item_Desc ,convert(decimal(18,3),final.cnsm_qty) as cnsm_qty,Final.Consu_Unit,Location_Desc,vendor_name      from ("

        sQuery += "select yy.Vendor_Code ,yy.Bill_To_Location ,yy.type,yy.Challan_No ,yy.Challan_Date ,yy.SRN_No ,yy.SRN_Date ,yy.SRN_Item,yy.Item_Desc,yy.SRN_Qty,yy.SRN_Unit,yy.Cons_Item ,yy.Consu_Unit,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty"
        sQuery += " from("
        sQuery += " select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from "
        sQuery += "(select TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit"
        sQuery += ",TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit"
        sQuery += ",TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item"

        sQuery += " from TSPL_SRN_HEAD"
        sQuery += " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') "
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code "
        sQuery += " left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No "
        sQuery += " left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_HEAD.RGP_Type=TSPL_GRN_HEAD.RGP_Type and TSPL_SRN_HEAD.PurchaseOrder_Type=TSPL_GRN_HEAD.PurchaseOrder_Type"
        sQuery += " left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code  "
        sQuery += " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No  and (case when (TSPL_RGP_HEAD .Against_As_It_Is = 1 and TSPL_RGP_HEAD .Against_JobWork = 1) then 'AI' when (TSPL_RGP_HEAD .Against_As_It_Is = 0 and TSPL_RGP_HEAD .Against_JobWork = 1 and TSPL_RGP_HEAD .Against_BOM = 0) then 'AR' end)=TSPL_GRN_HEAD.RGP_Type"
        sQuery += " left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ"
        sQuery += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit "
        sQuery += " )as YY"
        sQuery += " union all "
        sQuery += " select TSPL_SRN_HEAD.Vendor_Code ,"
        sQuery += " TSPL_SRN_HEAD.Bill_To_Location , 'Against BOM' as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No ,TSPL_SRN_HEAD.SRN_Date ,"
        sQuery += " TSPL_SRN_DETAIL.Item_Code ,tspl_item_master.Item_Desc, TSPL_SRN_DETAIL.SRN_Qty   ,TSPL_SRN_DETAIL.Unit_code,TSPL_RGP_BOM_DETAIL.Item_Code as cnsum_item,"
        sQuery += " TSPL_RGP_BOM_DETAIL.Unit_Code as cnsm_unit,TSPL_RGP_BOM_DETAIL.Qty as cnsm_qty  from TSPL_SRN_HEAD  "
        sQuery += " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No "
        sQuery += "  left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code      "
        sQuery += " left outer join TSPL_RGP_BOM_DETAIL on (TSPL_RGP_BOM_DETAIL.SRN_No=TSPL_SRN_DETAIL.SRN_No or TSPL_RGP_BOM_DETAIL.GRN_No=TSPL_SRN_DETAIL.GRN_ID)"
        sQuery += "  left join (select TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code,max(tspl_item_master.Item_Desc) as Item_Desc  ,TSPL_RGP_BOM_DETAIL.Unit_Code as Unit_Code  ,sum(TSPL_RGP_BOM_DETAIL.qty ) as RGp_Qty from TSPL_RGP_BOM_DETAIL  left join tspl_item_master on tspl_item_master.Item_Code = TSPL_RGP_BOM_DETAIL.Item_Code where InOut ='O' group by TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code ,TSPL_RGP_BOM_DETAIL.Unit_Code) as RGP on TSPL_SRN_HEAD.Vendor_Code =rgp .Vendor_Code and TSPL_RGP_BOM_DETAIL.Item_Code =rgp.Item_Code and TSPL_RGP_BOM_DETAIL.Unit_Code =rgp.Unit_Code  "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type='AB' "
        sQuery += " ) Final left join tspl_item_master on final.Cons_Item=tspl_item_master.Item_Code "
        sQuery += "  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code "
        sQuery += "  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        sQuery += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State left join tspl_location_master on tspl_location_master.Location_Code = final.Bill_To_Location left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = final.Vendor_Code "
        sQuery += "   where 2 = 2 and convert(date,final.SRN_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,final.SRN_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)"

        If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
            sQuery += " and isnull(final.Type,'')<>''"
        End If

        If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
            sQuery += " and final.Type ='" + ddlRGPType.Text + "'"
        End If

        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            sQuery += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            sQuery += " and final.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            sQuery += " and final.SRN_Item  in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        sQuery += " order by convert(date,final.SRN_Date,103)"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            ToShowSrnQtyingridonlyonetime()
            FormatGrid()
           RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Sub DrillDown()
        Try
            If clsCommon.CompairString(cboType.SelectedIndex, 0) = CompairStringResult.Equal Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                rbtnDetail.IsChecked = True
                strLocation = New ArrayList()
                strLocation = txtLocationMul.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Location Code").Value))
                txtLocationMul.arrValueMember = tmp
                strVendor = New ArrayList()
                strVendor = txtVendorMult.arrValueMember
                Dim tmp1 As New ArrayList()
                tmp1.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Vendor Code").Value))
                txtVendorMult.arrValueMember = tmp1
                strItem = New ArrayList()
                strItem = TxtMultiItem.arrValueMember
                Dim tmp2 As New ArrayList()
                tmp2.Add(clsCommon.myCstr(gv.CurrentRow.Cells("SRN Item").Value))
                TxtMultiItem.arrValueMember = tmp2
                QryFinal()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If cboType.SelectedIndex = 0 And rbtnSummary.IsChecked = True Then
            DrillDown()
        ElseIf (cboType.SelectedIndex = 1 And rbtnSummary.IsChecked = True) Then
            If gv.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("SRN_No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransType)
            End If
        ElseIf cboType.SelectedIndex = 0 And rbtnDetail.IsChecked = True Then
            If gv.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("SRN_No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransType)
            End If
        End If

       
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If cboType.SelectedIndex = 0 Then
                arrBack.Remove("Summary")
                TxtMultiItem.arrValueMember = strItem
                txtLocationMul.arrValueMember = strLocation
                txtVendorMult.arrValueMember = strVendor
                cboType.SelectedIndex = 0
                rbtnSummary.IsChecked = True
                Load_Report()
                PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
            Else
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
