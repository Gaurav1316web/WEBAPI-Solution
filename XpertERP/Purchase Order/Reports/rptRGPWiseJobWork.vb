Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==============Created By Preeti Gupta==========

Public Class RptRGPWiseJobWork
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim btnReferesh As Boolean = False

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
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptBalanceStockForJobWork)
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
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptRGPWiseJobWork & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")


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
                    clsCommon.MyExportToPDF("RGP Wise Job Work", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
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
        Dim sQuery As String = ";with WoleData as ("
        sQuery += "select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,"
        sQuery += " TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name, final.Vendor_Code ,final.Bill_To_Location , "
        sQuery += "  final.Type,final.RGP_No ,convert(varchar,final.RGP_Date,103) as RGP_Date ,final.RGP_Item_Code ,final.RGP_Item_Desc, Final.RGP_Qty ,final.SRN_No ,final.Challan_No ,"
        sQuery += "  convert(varchar,final.Challan_Date ,103) as Challan_Date  ,convert(varchar,final.SRN_Date,103) as SRN_Date,final.SRN_Item ,final.Item_Desc as SRN_desc, "
        sQuery += " final.SRN_Item +'  Received Item : '+  final.Item_Desc +' Qty : '+  convert(varchar,final.SRN_Qty  )+ ' '+final.SRN_Unit  as Item , final.SRN_Unit ,final.SRN_Qty ,"
        sQuery += " final.Cons_Item,TSPL_ITEM_MASTER .Item_Desc as Cons_Item_Desc  ,final.cnsm_qty,(Final.RGP_Qty-final.cnsm_qty) as Balance ,Final.Consu_Unit    from "
        sQuery += "("

        sQuery += "select yy.RGP_No ,yy.RGP_Item_Code ,yy.RGP_Item_Desc , yy.RGP_Qty,yy.RGP_Date, yy.Vendor_Code ,yy.Bill_To_Location ,yy.type,yy.Challan_No ,"
        sQuery += " yy.Challan_Date ,yy.SRN_No ,yy.SRN_Date ,yy.SRN_Item,yy.Item_Desc,yy.SRN_Qty,yy.SRN_Unit,yy.Cons_Item ,yy.Consu_Unit,"
        sQuery += " (case when isnull(Stad_Qty,0)>0 then (case when isnull(CF,0)>0 then ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor)/CF else ((Cons_Qty/Stad_Qty)*SRN_Qty*Conversion_Factor) end) else 0 end) as cnsm_qty from( select subq.*,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finaluom.Conversion_Factor as CF from"
        sQuery += " (select TSPL_RGP_HEAD.RGP_No,TSPL_RGP_DETAIL.Item_Code as RGP_Item_Code,Item_RGP.Item_Desc as RGP_Item_Desc, TSPL_RGP_DETAIL.RGP_Qty,TSPL_RGP_HEAD.RGP_Date,TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location ,case when TSPL_SRN_HEAD.RGP_Type='AR' then 'Against Job Work ' else case when TSPL_SRN_HEAD.RGP_Type='AI' then 'Against as is it' end end as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code as SRN_Item,tspl_item_master.Item_Desc  ,TSPL_SRN_DETAIL .SRN_Qty ,TSPL_SRN_DETAIL .Unit_code as SRN_Unit,TSPL_RGP_DETAIL.Item_Code as Cons_Item,TSPL_RGP_DETAIL.RGP_Qty as Cons_Qty,TSPL_RGP_DETAIL.Unit_code as Consu_Unit,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Stad_Qty,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Stan_Unit,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as Stand_Item"

        sQuery += " from TSPL_SRN_HEAD"
        sQuery += " left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No "
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code "
        sQuery += " left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No =TSPL_SRN_DETAIL.GRN_ID and TSPL_GRN_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code "
        sQuery += " left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  "
        sQuery += " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No =TSPL_RGP_DETAIL.RGP_No"
        sQuery += " left join tspl_item_master as Item_RGP on Item_RGP.Item_Code =TSPL_RGP_DETAIL.Item_Code "
        sQuery += " left join TSPL_RGP_JOB_WORK_DETAIL  on TSPL_RGP_JOB_WORK_DETAIL.RGP_No = TSPL_RGP_DETAIL.RGP_No and TSPL_RGP_JOB_WORK_DETAIL.RGP_No =TSPL_GRN_DETAIL.Against_RGP_No  and TSPL_RGP_JOB_WORK_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type in ('AR','AI') and ISNULL(TSPL_GRN_DETAIL.Against_RGP_No,'')<>'')subQ"
        sQuery += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=subQ.SRN_Item and TSPL_ITEM_UOM_DETAIL.UOM_Code=subQ.SRN_Unit left outer join TSPL_ITEM_UOM_DETAIL finaluom on finaluom.Item_Code=subQ.SRN_Item and finaluom.UOM_Code=subQ.Stan_Unit "
        sQuery += " )as YY"
        sQuery += " union all "
        sQuery += " select '' as RGP_No,rgp.Item_Code  as RGP_Item_Code,rgp.Item_Desc  as RGP_Item_Desc ,rgp.RGp_Qty  as RGP_Qty,NULL as RGP_Date, TSPL_SRN_HEAD.Vendor_Code ,TSPL_SRN_HEAD.Bill_To_Location , 'Against BOM' as Type,TSPL_SRN_HEAD.Challan_No ,TSPL_SRN_HEAD.Challan_Date ,TSPL_SRN_HEAD.SRN_No ,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL.Item_Code ,tspl_item_master.Item_Desc, TSPL_SRN_DETAIL.SRN_Qty   ,TSPL_SRN_DETAIL.Unit_code,TSPL_RGP_BOM_DETAIL.Item_Code as cnsum_item,TSPL_RGP_BOM_DETAIL.Unit_Code as cnsm_unit,TSPL_RGP_BOM_DETAIL.Qty as cnsm_qty  from TSPL_SRN_HEAD  left  join TSPL_SRN_DETAIL  on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_No  left join tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code      left outer join TSPL_RGP_BOM_DETAIL on (TSPL_RGP_BOM_DETAIL.SRN_No=TSPL_SRN_DETAIL.SRN_No or TSPL_RGP_BOM_DETAIL.GRN_No=TSPL_SRN_DETAIL.GRN_ID)"
        sQuery += " left join (select TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code,max(tspl_item_master.Item_Desc) as Item_Desc  ,TSPL_RGP_BOM_DETAIL.Unit_Code as Unit_Code  ,sum(TSPL_RGP_BOM_DETAIL.qty ) as RGp_Qty from TSPL_RGP_BOM_DETAIL "
        sQuery += " left join tspl_item_master on tspl_item_master.Item_Code = TSPL_RGP_BOM_DETAIL.Item_Code where InOut ='O'"
        sQuery += " group by TSPL_RGP_BOM_DETAIL.Vendor_Code ,TSPL_RGP_BOM_DETAIL.Item_Code ,TSPL_RGP_BOM_DETAIL.Unit_Code) as RGP on TSPL_SRN_HEAD.Vendor_Code =rgp .Vendor_Code and TSPL_RGP_BOM_DETAIL.Item_Code =rgp.Item_Code and TSPL_RGP_BOM_DETAIL.Unit_Code =rgp.Unit_Code "
        sQuery += " where TSPL_SRN_HEAD.PurchaseOrder_Type='J' and TSPL_SRN_HEAD.RGP_Type='AB' "
        sQuery += " ) Final left join tspl_item_master on final.Cons_Item=tspl_item_master.Item_Code "
        sQuery += "  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_item_master.Comp_Code "
        sQuery += "  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
        sQuery += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
        sQuery += "   where 2 = 2 and ((convert(date,final.RGP_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,final.RGP_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)) or isnull(final.RGP_Date,'')='')"

        
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
            sQuery += " and final.RGP_Item_Code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If

        sQuery += " ),finalwholedata"
        sQuery += " as (select ROW_NUMBER () over (partition by SRN_No order by srn_date) as SLNO,* from WoleData) select company_address,logo_img,logo_img2,comp_code,comp_name,vendor_code,bill_To_Location,Type,RGP_No ,RGP_Date ,RGP_Item_Code ,RGP_Item_Desc ,convert(decimal(18,3),RGP_Qty) as RGP_Qty  ,case when slno=1 then SRN_No   else '' end as SRN_No ,case when slno=1 then Challan_No    else '' end as Challan_No,case when slno=1 then Challan_Date     else '' end as Challan_Date,case when slno=1 then SRN_Date      else '' end as SRN_Date, case when slno=1 then SRN_Item  else '' end as SRN_Item, case when slno=1 then SRN_desc  else '' end as SRN_desc,case when slno=1 then SRN_Unit   else '' end as SRN_Unit,convert(decimal(18,3),case when slno=1 then SRN_Qty    else 0 end) as SRN_Qty,Cons_Item,Cons_Item_Desc,convert(decimal(18,3),cnsm_qty) as cnsm_qty,convert(decimal(18,3),Balance) as Balance ,Consu_Unit  from FinalWholeData"
        sQuery += " order by type,SRN_No  "

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
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
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dtgv, "rptRGPWiseJobWork", "RGP Wise Job Work")
                frmCRV = Nothing
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        gv.BestFitColumns()
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

        gv.Columns("RGP_No").IsVisible = True
        gv.Columns("RGP_No").Width = 100
        gv.Columns("RGP_No").HeaderText = "RGP No"

        gv.Columns("RGP_Date").IsVisible = True
        gv.Columns("RGP_Date").Width = 100
        gv.Columns("RGP_Date").HeaderText = "RGP Date"

        gv.Columns("RGP_Item_Desc").IsVisible = True
        gv.Columns("RGP_Item_Desc").Width = 200
        gv.Columns("RGP_Item_Desc").HeaderText = "Sending Item"

        gv.Columns("RGP_Qty").IsVisible = True
        gv.Columns("RGP_Qty").Width = 100
        gv.Columns("RGP_Qty").HeaderText = "Send Qty"

        gv.Columns("SRN_No").IsVisible = True
        gv.Columns("SRN_No").Width = 100
        gv.Columns("SRN_No").HeaderText = "SRN No"

        gv.Columns("Challan_No").IsVisible = True
        gv.Columns("Challan_No").Width = 100
        gv.Columns("Challan_No").HeaderText = "Vendor Challan No"

        'gv.Columns("Challan_Date").IsVisible = True
        'gv.Columns("Challan_Date").Width = 100
        'gv.Columns("Challan_Date").HeaderText = "Vendor Challan Date"

        gv.Columns("Challan_Date").IsVisible = True
        gv.Columns("Challan_Date").Width = 100
        gv.Columns("Challan_Date").HeaderText = "Vendor Challan Date"

        gv.Columns("SRN_desc").IsVisible = True
        gv.Columns("SRN_desc").Width = 200
        gv.Columns("SRN_desc").HeaderText = "Received Item"

        gv.Columns("SRN_Qty").IsVisible = True
        gv.Columns("SRN_Qty").Width = 100
        gv.Columns("SRN_Qty").HeaderText = "Received Qty"

        gv.Columns("Cons_Item").IsVisible = False
        gv.Columns("Cons_Item").Width = 100
        gv.Columns("Cons_Item").HeaderText = "Item Code"

        gv.Columns("Cons_Item_Desc").IsVisible = True
        gv.Columns("Cons_Item_Desc").Width = 300
        gv.Columns("Cons_Item_Desc").HeaderText = "Balance Item"
      

        gv.Columns("cnsm_qty").IsVisible = True
        gv.Columns("cnsm_qty").Width = 80
        gv.Columns("cnsm_qty").HeaderText = "balance Qty"

        gv.Columns("Balance").IsVisible = True
        gv.Columns("Balance").Width = 80
        gv.Columns("Balance").HeaderText = " Total Balance"


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

    Private Sub RptRGPWiseJobWork_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P Adding New")
        Reset()
    End Sub

    Private Sub RptRGPWiseJobWork_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
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


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        GetReportID()
        Load_Report()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If clsCommon.CompairString(ddlRGPType.SelectedItem.Value, "All") = CompairStringResult.Equal Then
            VarID += "_A"
        ElseIf clsCommon.CompairString(ddlRGPType.SelectedItem.Value, "Against Job Work") = CompairStringResult.Equal Then
            VarID += "_AJ"
        ElseIf clsCommon.CompairString(ddlRGPType.SelectedItem.Value, "Against BOM") = CompairStringResult.Equal Then
            VarID += "_AB"
        ElseIf clsCommon.CompairString(ddlRGPType.SelectedItem.Value, "Against as is it") = CompairStringResult.Equal Then
            VarID += "_AI"
        End If

        gv.VarID = VarID

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        Reset()
    End Sub

    Private Sub rmexcel_Click(sender As Object, e As EventArgs) Handles rmexcel.Click
        print(EnumExportTo.Excel)
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click

        Dim qry As String = "select Item_Code as Code ,Item_Desc as Name from tspl_item_master"
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
