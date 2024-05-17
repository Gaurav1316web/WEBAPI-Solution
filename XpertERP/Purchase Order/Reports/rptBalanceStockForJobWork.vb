Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==============Created By Preeti Gupta==========

Public Class RptBalanceStockForJobWork
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim btnReferesh As Boolean = False
    Public arrItem As ArrayList
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
        rbtnSummary.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptBalanceStockForJobWork)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
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
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBalanceStockForJobWork & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")

           
            If txtLocationMul.arrDispalyMember IsNot Nothing AndAlso txtLocationMul.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            End If

            If txtVendorMult.arrDispalyMember IsNot Nothing AndAlso txtVendorMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor_Code : " + clsCommon.GetMulcallStringWithComma(txtVendorMult.arrDispalyMember))
            End If
            If TxtMultiItem.arrDispalyMember IsNot Nothing AndAlso TxtMultiItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrDispalyMember))
            End If
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
                clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
        'Dim sQuery As String = "select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,"
        'sQuery += " TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,  final.Type ,final.Cons_Item,TSPL_ITEM_MASTER .Item_Desc as Cons_Item_Desc ,convert(decimal(18,3),Final.RGP_Qty) as RGP_Qty  ,convert(decimal(18,3),final.cnsm_qty) as cnsm_qty,(Final.RGP_Qty-convert(decimal(18,3),final.cnsm_qty)) as Balance ,Final.Consu_Unit   from ("

        'sQuery += "select preeti.Type,preeti.Cons_Item,preeti.Consu_Unit,sum(preeti.RGP_Qty) as rgp_qty,sum(preeti.cnsm_qty) as cnsm_qty from "
        'sQuery += " (select yy.RGP_Date , yy.type,yy.Cons_Item,yy.Consu_Unit, yy.RGP_Qty,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty from"
        'sQuery += " ( select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from (select TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_HEAD.RGP_Date,TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit,TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item from TSPL_SRN_HEAD left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code  left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit  where 2=2 "
        'If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
        '    sQuery += " and subq.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        'End If

        'If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
        '    sQuery += " and subq.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        'End If
        'If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
        '    sQuery += " and subq.SRN_Item in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        'End If
        'sQuery += " and  convert(date,subq.RGP_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,subq.RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  )as YY "
        'sQuery += " )preeti group by preeti.Type,preeti.Cons_Item,preeti.Consu_Unit"
        'sQuery += " union all "
        'sQuery += " select 'Against Bom' as type,child.Item_Code,child.Unit_Code,sum(child.sendqty) as sendqty,sum(child.recqty) as recqty from ("
        'sQuery += " select Item_Code,Unit_Code,SUM(qty) as sendqty,0 as recqty from TSPL_RGP_BOM_DETAIL"
        'sQuery += " inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_BOM_DETAIL.RGP_No "
        'sQuery += " inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code "
        'sQuery += " where InOut='o' "
        'If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
        '    sQuery += " and TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        'End If
        'If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
        '    sQuery += " and TSPL_RGP_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        'End If
        'If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
        '    sQuery += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        'End If
        'sQuery += " and convert(date,RGP_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  group by Item_Code,Unit_Code"
        'sQuery += " union all"
        'sQuery += " select Item_Code,Unit_Code,0 as sendqty,SUM(qty) as recqty from TSPL_RGP_BOM_DETAIL "
        'sQuery += " inner join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_RGP_BOM_DETAIL.GRN_No "
        ''=============added by shivani for temporary use to for match the Send and Recv report
        'sQuery += " inner join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN  =TSPL_GRN_HEAD.GRN_No "
        ''======================================================================================
        'sQuery += " inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_GRN_HEAD.Vendor_Code "
        'sQuery += " where InOut='i' "
        'If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
        '    sQuery += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        'End If
        'If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
        '    sQuery += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        'End If
        'If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
        '    sQuery += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        'End If
        'sQuery += " and convert(date,SRN_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,SRN_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) group by Item_Code,Unit_Code)child"
        'sQuery += " group by child.Item_Code,child.Unit_Code"

        'sQuery += " ) Final left join tspl_item_master on final.Cons_Item=tspl_item_master.Item_Code "
        'sQuery += "  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code "
        'sQuery += "  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        'sQuery += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
        'sQuery += "   where 2 = 2 "
        'If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
        '    sQuery += " and isnull(final.Type,'')<>''"
        'End If
        'If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
        '    sQuery += " and final.Type ='" + ddlRGPType.Text + "'"
        'End If
        'If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
        '    sQuery += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        'End If

        Dim INNERQry As String = "select 'R' as Cond,preeti.Type,preeti.Cons_Item,preeti.Consu_Unit,sum(preeti.RGP_Qty) as rgp_qty,sum(preeti.cnsm_qty) as cnsm_qty from  ( select yy.RGP_No ,yy.RGP_Date , yy.type,yy.Cons_Item,yy.Consu_Unit, yy.RGP_Qty,SRN_No ,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty from ( select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from " & _
                                " (select TSPL_RGP_HEAD.RGP_No ,TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_HEAD.RGP_Date,TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit,TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item from TSPL_SRN_HEAD " & _
                                " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') " & _
                                " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code  left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_HEAD.RGP_Type=TSPL_GRN_HEAD.RGP_Type and TSPL_SRN_HEAD.PurchaseOrder_Type=TSPL_GRN_HEAD.PurchaseOrder_Type left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No" & _
                                " and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code " & _
                                " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No " & _
                                " and (case when (TSPL_RGP_HEAD .Against_As_It_Is = 1 and TSPL_RGP_HEAD .Against_JobWork = 1) then 'AI' when (TSPL_RGP_HEAD .Against_As_It_Is = 0 and TSPL_RGP_HEAD .Against_JobWork = 1 and TSPL_RGP_HEAD .Against_BOM = 0) then 'AR' end)=TSPL_GRN_HEAD.RGP_Type " & _
                                " left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code " & _
                                " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit  where 2=2 "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and subq.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and subq.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and subq.SRN_Item in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " AND convert(date,subq.RGP_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,subq.RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  )as YY  )preeti group by preeti.Type,preeti.Cons_Item,preeti.Consu_Unit " & _
                                 " union all select 'R' as Cond,'Against Bom' as type,child.Item_Code,child.Unit_Code,sum(child.sendqty) as sendqty,sum(child.recqty) as recqty from ( " & _
                                " select Item_Code,Unit_Code,SUM(qty) as sendqty,0 as recqty from TSPL_RGP_BOM_DETAIL inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_BOM_DETAIL.RGP_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  where InOut='o' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_RGP_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " and convert(date,RGP_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  group by Item_Code,Unit_Code union all select Item_Code,Unit_Code,0 as sendqty,SUM(qty) as recqty from TSPL_RGP_BOM_DETAIL  inner join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_RGP_BOM_DETAIL.GRN_No  inner join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN  =TSPL_GRN_HEAD.GRN_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_GRN_HEAD.Vendor_Code  where InOut='i' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " and convert(date,SRN_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,SRN_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) group by Item_Code,Unit_Code " & _
                                " )child group by child.Item_Code,child.Unit_Code "

        Dim OUTERQRY As String = "  select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,  Type ,Cons_Item,TSPL_ITEM_MASTER .Item_Desc as Cons_Item_Desc ,isnull(Op_qty,0) as Op_qty,convert(decimal(18,3),RGP_Qty) as RGP_Qty  ,convert(decimal(18,3),cnsm_qty) as cnsm_qty ,Consu_Unit  ,ISNULL(CLQty,0) AS CLQty,isnull(bal_qty,0) as bal_qty  from " & _
" (SELECT TYPE,Cons_Item ,Consu_Unit,sum(op_qty) as Op_qty,SUM(rgp_qty) AS rgp_qty ,SUM(cnsm_qty) AS cnsm_qty,sum(rgp_qty) - sum(cnsm_qty) as bal_qty,SUM(Op_qty)+sum(rgp_qty) - sum(cnsm_qty) as CLQty   FROM " & _
" ( select Type,Cons_Item,Consu_Unit,sum(case when cond='O' then 0 else rgp_qty end) as rgp_qty,sum(case when cond='O' then 0 else cnsm_qty end) as cnsm_qty,sum(case when cond='R' then rgp_qty - cnsm_qty else 0 end) as Stock_Qty,sum(case when cond='O' then (rgp_qty) - (cnsm_qty) else 0 end) as Op_qty from " & _
" ( " + INNERQry + " union all SELECT 'O' as Cond,TYPE,Cons_Item ,Consu_Unit ,sum(rgp_qty) AS rgp_qty ,sum(cnsm_qty) AS cnsm_qty  FROM " & _
 " (select preeti.Type,preeti.Cons_Item,preeti.Consu_Unit,sum(preeti.RGP_Qty) as rgp_qty,sum(preeti.cnsm_qty) as cnsm_qty from " & _
  " (select yy.RGP_No ,yy.RGP_Date , yy.type,yy.Cons_Item,yy.Consu_Unit, yy.RGP_Qty,SRN_No ,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty from ( select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from ( " & _
" select TSPL_RGP_HEAD.RGP_No ,TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_HEAD.RGP_Date,TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit,TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item from TSPL_SRN_HEAD left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI')" & _
 " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code  left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_HEAD.RGP_Type=TSPL_GRN_HEAD.RGP_Type and  TSPL_SRN_HEAD.PurchaseOrder_Type=TSPL_GRN_HEAD.PurchaseOrder_Type  left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No " & _
" and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code " & _
 "  left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code  where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit  where 2=2 "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            OUTERQRY += " and subq.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            OUTERQRY += " and subq.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            OUTERQRY += " and subq.SRN_Item in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        OUTERQRY += " AND convert(date,subq.RGP_Date,103)<(convert(date,'" + fromDate.Value + "',103)) )as YY  )preeti group by preeti.Type,preeti.Cons_Item,preeti.Consu_Unit " & _
   " union all select 'Against Bom' as type,child.Item_Code,child.Unit_Code,sum(child.sendqty) as sendqty,sum(child.recqty) as recqty from ( select Item_Code,Unit_Code,SUM(qty) as sendqty,0 as recqty from TSPL_RGP_BOM_DETAIL inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_BOM_DETAIL.RGP_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  where InOut='o' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            OUTERQRY += " and TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            OUTERQRY += " and TSPL_RGP_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            OUTERQRY += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        OUTERQRY += " and convert(date,RGP_Date ,103)<(convert(date,'" + fromDate.Value + "',103))  group by Item_Code,Unit_Code union all select Item_Code,Unit_Code,0 as sendqty,SUM(qty) as recqty from TSPL_RGP_BOM_DETAIL  inner join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_RGP_BOM_DETAIL.GRN_No  inner join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN  =TSPL_GRN_HEAD.GRN_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_GRN_HEAD.Vendor_Code  where InOut='i' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        OUTERQRY += " and  convert(date,SRN_Date ,103)<(convert(date,'" + fromDate.Value + "',103)) group by Item_Code,Unit_Code)child group by child.Item_Code,child.Unit_Code )AS LL group by ll.Type,ll.Cons_Item,ll.Consu_Unit " & _
 " ) Final group by Type,Cons_Item,Consu_Unit)as dd GROUP BY TYPE,Cons_Item ,Consu_Unit) SS " & _
" left join tspl_item_master on Cons_Item=tspl_item_master.Item_Code   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code   LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State    where 2 = 2   "
        If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
            OUTERQRY += " and isnull(Type,'')<>''"
        End If
        If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
            OUTERQRY += " and Type ='" + ddlRGPType.Text + "'"
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            OUTERQRY += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(OUTERQRY)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()

            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            If btnReferesh = False Then
                'PurchaseViewer.funsub(dtgv, "crptMaterialSendForJobWork", "Material Send For Job Work")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dtgv, "rptBalanceStockForJobWork", "Material Received After Job Work")
                frmCRV = Nothing
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Type").IsVisible = True
        gv.Columns("Type").Width = 100
        gv.Columns("Type").HeaderText = "Type"



        gv.Columns("Cons_Item").IsVisible = True
        gv.Columns("Cons_Item").Width = 100
        gv.Columns("Cons_Item").HeaderText = "Item Code"

        gv.Columns("Cons_Item_Desc").IsVisible = True
        gv.Columns("Cons_Item_Desc").Width = 300
        gv.Columns("Cons_Item_Desc").HeaderText = "Item Description"

        gv.Columns("RGP_Qty").IsVisible = True
        gv.Columns("RGP_Qty").Width = 100
        gv.Columns("RGP_Qty").HeaderText = "Send Qty"

        gv.Columns("cnsm_qty").IsVisible = True
        gv.Columns("cnsm_qty").Width = 80
        gv.Columns("cnsm_qty").HeaderText = "Received Qty"

        'gv.Columns("bal_qty").IsVisible = True
        'gv.Columns("bal_qty").Width = 80
        'gv.Columns("bal_qty").HeaderText = "Balance"

        gv.Columns("CLQty").IsVisible = True
        gv.Columns("CLQty").Width = 80
        gv.Columns("CLQty").HeaderText = "Closing"

        gv.Columns("Op_qty").IsVisible = True
        gv.Columns("Op_qty").Width = 80
        gv.Columns("Op_qty").HeaderText = "Opening"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("RGP_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'Dim item7 As New GridViewSummaryItem("NewAmount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Item as Item format ""{0}: {1}"" Group By Item"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC as Item format ""{0}: {1}"" Group By VLC"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub


    Private Sub RptBalanceStockForJobWork_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P Adding New")
        rbtnSummary.IsChecked = True
        Reset()
    End Sub

    Private Sub RptBalanceStockForJobWork_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
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
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
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
                            " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code   WHERE TSPL_VENDOR_MASTER.Status='N'  "
        txtVendorMult.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMSelESI", qry, "Code", "Name", txtVendorMult.arrValueMember, txtVendorMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as Code ,Item_Desc as Name from tspl_item_master"
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub
    Sub Qry()
        Dim INNERQry As String = "select RGP_No ,convert(varchar,RGP_Date,103) as RGP_Date,SRN_No ,convert(varchar,SRN_Date,103) as SRN_Date,'R' as Cond,preeti.Type,preeti.Cons_Item,preeti.Consu_Unit,sum(preeti.RGP_Qty) as rgp_qty,sum(preeti.cnsm_qty) as cnsm_qty from  ( select yy.RGP_No ,yy.RGP_Date , yy.type,yy.Cons_Item,yy.Consu_Unit, yy.RGP_Qty,SRN_No,yy.SRN_Date ,(case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty from ( select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from " & _
                                " (select TSPL_RGP_HEAD.RGP_No ,TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_HEAD.RGP_Date,TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit,TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item from TSPL_SRN_HEAD " & _
                                " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') " & _
                                " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code  left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.RGP_Id=TSPL_GRN_DETAIL.Against_RGP_No left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_HEAD.RGP_Type=TSPL_GRN_HEAD.RGP_Type and TSPL_SRN_HEAD.PurchaseOrder_Type=TSPL_GRN_HEAD.PurchaseOrder_Type" & _
                                " and TSPL_RGP_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code " & _
                                " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No " & _
                                " left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code " & _
                                " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit  where 2=2 "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and subq.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and subq.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and subq.SRN_Item in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " AND convert(date,subq.RGP_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,subq.RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  )as YY  )preeti group by preeti.Type,preeti.Cons_Item,preeti.Consu_Unit,RGP_No ,SRN_No,RGP_Date ,SRN_Date   " & _
                                 " union all select RGP_No ,case when isnull(RGP_No,'')= null then '' else   convert(varchar, RGP_Date,103) end as RGP_Date ,SRN_No ,case when isnull(RGP_No,'')= null then '' else  convert(varchar,SRN_Date,103) end as SRN_Date,'R' as Cond,'Against Bom' as type,child.Item_Code,child.Unit_Code,sum(child.sendqty) as sendqty,sum(child.recqty) as recqty from ( " & _
                                " select TSPL_RGP_HEAD.RGP_No,null as SRN_No ,RGP_Date ,null as SRN_Date, Item_Code,Unit_Code,SUM(qty) as sendqty,0 as recqty from TSPL_RGP_BOM_DETAIL inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_BOM_DETAIL.RGP_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  where InOut='o' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_RGP_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " and convert(date,RGP_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)  group by Item_Code,Unit_Code,TSPL_RGP_HEAD.RGP_No,RGP_Date union all select null as RGP_No,TSPL_SRN_HEAD.srn_no,null as RGP_Date,SRN_Date as SRN_Date,Item_Code,Unit_Code,0 as sendqty,SUM(qty) as recqty from TSPL_RGP_BOM_DETAIL  inner join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No =TSPL_RGP_BOM_DETAIL.GRN_No  inner join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN  =TSPL_GRN_HEAD.GRN_No  inner join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_GRN_HEAD.Vendor_Code  where InOut='i' "
        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If
        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            INNERQry += " and TSPL_GRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            INNERQry += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        INNERQry += " and convert(date,SRN_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,SRN_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) group by Item_Code,Unit_Code ,TSPL_SRN_HEAD.srn_no,SRN_Date " & _
                                " )child group by child.Item_Code,child.Unit_Code,RGP_No ,SRN_No ,RGP_Date ,SRN_Date "
        Dim Outer As String = " select RGP_No as [RGP No],RGP_Date as [RGP Date],Type,Cons_Item as [Item Code],Item_Desc as [Item Name] ,Consu_Unit as [Unit],rgp_qty as [RGP Qty],srn_no as [SRN No.],SRN_Date  as [SRN Date],cnsm_qty as [SRN Qty] from  (" + INNERQry + " ) Final   left join tspl_item_master on Cons_Item=tspl_item_master.Item_Code   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code   LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State where 2=2 "

        If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
            Outer += " and isnull(Type,'')<>''"
        End If
        If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
            Outer += " and Type ='" + ddlRGPType.Text + "'"
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            Outer += " and Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Outer)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.ReadOnly = True
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        'ReStoreGridLayout()
    End Sub

    Sub DrillDown()
        Try
            If rbtnSummary.IsChecked Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                rbtnDetail.IsChecked = True
                arrItem = New ArrayList()
                arrItem = TxtMultiItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Cons_Item").Value))
                TxtMultiItem.arrValueMember = tmp
                'Print(Exporter.Refresh)
                Qry()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If rbtnSummary.IsChecked = True Then
            DrillDown()
        ElseIf rbtnDetail.IsChecked = True Then
            If TxtMultiItem.arrValueMember.Count > 0 Then
                Dim strDoc As String = String.Empty
                If e.Column Is gv.Columns("SRN No.") AndAlso clsCommon.myLen(gv.Columns("SRN No.")) > 0 Then
                    strDoc = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("SRN No.").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strDoc)
                ElseIf e.Column Is gv.Columns("RGP No") AndAlso clsCommon.myLen(gv.Columns("RGP No")) > 0 Then
                    strDoc = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("RGP No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDoc)
                End If
            End If
        End If
        

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnDetail.IsChecked Then
                arrBack.Remove("Summary")
                TxtMultiItem.arrValueMember = arrItem
                rbtnSummary.IsChecked = True
                'print(Exporter.Refresh)
                Load_Report()
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
