Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Imports Microsoft.Office.Interop
'======================created by Shivani Tyagi against ticket no[BM00000008713]
Public Class RptJobWorkStatus
    Dim arrBack As New List(Of String)
    Public arrVendor As ArrayList
    Dim StrFromDate As Date?
    Dim StrToDate As Date?
    Dim IsInsideLoaddata As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptJobWorkStatus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where ((Is_Section='N' and Is_Sub_Location='N' and (Location_Type='Physical' or Location_Type='Logical') ) or (CSA_Type='Y') ) "
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Private Sub TxtMultiVendor__My_Click(sender As Object, e As EventArgs) Handles TxtMultiVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER WHERE Status='N' order by Vendor_Code"
        TxtMultiVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", TxtMultiVendor.arrValueMember, TxtMultiVendor.arrDispalyMember)
    End Sub

    Private Sub TxtMultiRGP__My_Click(sender As Object, e As EventArgs) Handles TxtMultiRGP._My_Click
        Dim qry As String = "select RGP_No as Code,convert(varchar,RGP_Date,103) as Name  from TSPL_MILK_RGP_HEAD where convert(date,RGP_Date ,103)>= convert(date,'" + txtFromDate.Value + "',103) and convert(date,RGP_Date,103)<= convert(date,'" + txtToDate.Value + "',103) order by convert(date,RGP_Date,103)"
        TxtMultiRGP.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeRGP", qry, "Code", "Name", TxtMultiRGP.arrValueMember, TxtMultiRGP.arrDispalyMember)
    End Sub

    Private Sub TxtMultiSRN__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSRN._My_Click
        Dim qry As String = "select SRN_NO as Code ,convert(varchar,SRN_Date,103) as Name   from TSPL_JOB_MILK_SRN where convert(date,SRN_Date ,103)>= convert(date,'" + txtFromDate.Value + "',103) and convert(date,SRN_Date,103)<= convert(date,'" + txtToDate.Value + "',103) order by convert(date,SRN_Date,103)"
        TxtMultiSRN.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeSRN", qry, "Code", "Name", TxtMultiSRN.arrValueMember, TxtMultiSRN.arrDispalyMember)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            If IsInsideLoaddata = False Then
                StrFromDate = clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")
                StrToDate = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")
            End If
           
            PageSetupReport_ID = MyBase.Form_ID
            loaddata()
        Catch ex As Exception
        Finally
            IsInsideLoaddata = True
        End Try
        
    End Sub
    Sub loaddata()
        
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater then to Date")
        End If
        Dim innerqry As String = " (select TSPL_MILK_RGP_HEAD.RGP_No as Doc_No,TSPL_MILK_RGP_HEAD.Location ,Item_Code,TSPL_MILK_RGP_DETAIL.To_Location_Code ,Vendor_Code  ,FAT_KG ,SNF_KG  " & _
                            " ,convert(varchar,rgp_date,103) as SRNDate " & _
                            " ,RGP_Qty as Qty,'IN' as Type,Unit_code as UOM " & _
                            " from TSPL_MILK_RGP_DETAIL left join TSPL_MILK_RGP_HEAD on TSPL_MILK_RGP_HEAD.RGP_No =TSPL_MILK_RGP_DETAIL.RGP_No "
        innerqry += " where convert(date,rgp_date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,rgp_date,103)<=convert(date,'" + txtToDate.Value + "',103) "
        If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
            innerqry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
        End If
        If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
            innerqry += "  and Location in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
        End If
        innerqry += " union all " & _
                            " select TSPL_JOB_MILK_SRN.SRN_NO as Doc_No ," & _
                            " TSPL_JOB_MILK_SRN.Loc_Code as Location ,Item_Code ,'' as To_Location_Code,Vendor_Code ,fat_KG ,SNF_KG ,convert(varchar,SRN_Date,103) as SRNDate ,Qty ,'OUT' as Type,UOM " & _
                            " from TSPL_JOB_MILK_SRN where convert(date,SRN_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,SRN_Date,103)<=convert(date,'" + txtToDate.Value + "',103) "
        If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
            innerqry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
        End If
        If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
            innerqry += " and Loc_Code in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
        End If
        innerqry += " ) as InnerQry left join tspl_item_master on tspl_item_master.Item_Code =InnerQry.Item_Code " & _
                            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =InnerQry.Location " & _
                            " left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = InnerQry.Vendor_Code " & _
                            " left join TSPL_LOCATION_MASTER as Tolocation on InnerQry.To_Location_Code =Tolocation.Location_Code " & _
                            " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) " & _
                            " ) union all " & _
                            " ( select Doc_No,Location,TSPL_LOCATION_MASTER.Location_Desc,InnerQry.Item_Code,Item_Desc,To_Location_Code,Tolocation.Location_Desc as sub_Location ,InnerQry.Vendor_Code,vendor_name,FAT_KG ,SNF_KG,SRNDate,Qty,InnerQry.Type,UOM  from (  " & _
                            " select TSPL_MILK_RGP_HEAD.RGP_No as Doc_No,TSPL_MILK_RGP_HEAD.Location ,Item_Code,TSPL_MILK_RGP_DETAIL.To_Location_Code ,Vendor_Code  ,FAT_KG ,SNF_KG ,convert(varchar,RGP_Date,103) as SRNDate ,RGP_Qty as Qty,'IN' as Type,Unit_code as UOM " & _
                            " from TSPL_MILK_RGP_DETAIL left join TSPL_MILK_RGP_HEAD on TSPL_MILK_RGP_HEAD.RGP_No =TSPL_MILK_RGP_DETAIL.RGP_No " & _
                            " where convert(date,RGP_Date,103) < convert(date,'" + txtFromDate.Value + "',103) "
        If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
            innerqry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
        End If
        If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
            innerqry += "  and Location in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
        End If
        innerqry += " union all select TSPL_JOB_MILK_SRN.SRN_NO as Doc_No ,TSPL_JOB_MILK_SRN.Loc_Code as Location ,Item_Code ,'' as To_Location_Code,Vendor_Code ,fat_KG ,SNF_KG ,convert(varchar,SRN_Date,103) as SRNDate ,Qty ,'OUT' as Type,UOM " & _
                            " from TSPL_JOB_MILK_SRN where convert(date,SRN_Date,103) < convert(date,'" + txtFromDate.Value + "',103) "
        If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
            innerqry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
        End If
        If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
            innerqry += " and Loc_Code in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
        End If
        innerqry += " ) as InnerQry left join tspl_item_master on tspl_item_master.Item_Code =InnerQry.Item_Code " & _
                            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =InnerQry.Location " & _
                            " left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = InnerQry.Vendor_Code " & _
                            " left join TSPL_LOCATION_MASTER as Tolocation on InnerQry.To_Location_Code =Tolocation.Location_Code " & _
                            " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)" & _
                            " ))"


        Dim Qry As String = " select  SRNdate   ,Vendor_Code ,vendor_name  , convert(decimal(18,2), OPQty) as  OPQty, convert(decimal(18,3) , OPFAT) as OPFAT, convert(decimal(18,3),OPSNF) as OPSNF, convert(decimal(18,3),RecQty) as RecQty, convert(decimal(18,3),RecFat) as RecFat, convert(decimal(18,3),RecSNF,103) as RecSNF,convert(decimal(18,2), IssQty) as IssQty ,convert(decimal(18,2), IssFat) as IssFat, convert(decimal(18,3),IssSNF) as IssSNF ,convert(decimal(18,2), CLQty) as CLQty, convert(decimal(18,3), CLFAT) as CLFAT, convert(decimal(18,3),CLSNF) as CLSNF,case when " + txtRecFat.Text + "= null then 0  when " + txtRecFat.Text + " = 0 then 0 else convert(decimal(18,3),(IssFat * " + txtRecFat.Text + ")/100)  end as Recovery_fAT,case when " + txtRecSnf.Text + "=  null then 0  when " + txtRecSnf.Text + " = 0 then 0 else convert(decimal(18,3),(IssSNF * " + txtRecSnf.Text + ")/100) end AS Recovery_SNF  from (select " & _
                                   " convert(varchar, SRNdate,103) as SRNdate   ,Vendor_Code ,m.vendor_name , (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty , (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT,(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as OPSNF, isnull(RecQty,0) as RecQty,isnull(RecFAT,0) as RecFat,isnull(RecSNF,0) as RecSNF,isnull(IssQty,0) as IssQty,isnull(IssFAT,0) as IssFat,isnull(IssSNF,0) as IssSNF ,CLQty  as CLQty, CLBalance_FAT as CLFAT, CLBalance_SNF as CLSNF  from " & _
                                   " (select SUM(sum(Stock_Qty)) OVER (Partition BY vendor_code ORDER BY vendor_code,convert(date,SRNDATE,103)) as CLQty ,Vendor_Code ,max(vendor_name ) as vendor_name  ,SRNDate ,sum(Balance_FAT ) as Balance_FAT,sum(Balance_SNF )as Balance_SNF,sum(case when Type='IN' then Stock_Qty else 0 end) as RecQty,sum(case when type='OUT' then -1 *Stock_Qty else 0 end) as IssQty " & _
                                   " ,SUM(sum(Balance_FAT)) OVER (Partition BY vendor_Code ORDER BY Vendor_Code, convert(date,SRNDate,103)) as CLBalance_FAT " & _
                                   " ,SUM(sum(Balance_SNF)) OVER (Partition BY vendor_Code ORDER BY vendor_Code,convert(date,SRNdate,103)) as CLBalance_SNF " & _
                                   " ,sum(case when type='IN' then Balance_FAT else 0 end) as RecFAT " & _
                                   " ,sum(case when type='OUT' then -1*Balance_FAT else 0 end) as IssFAT " & _
                                   " ,sum(case when type='IN' then Balance_SNF else 0 end) as RecSNF " & _
                                   " ,sum(case when type='OUT' then -1*Balance_SNF else 0 end) as IssSNF " & _
                                   " from (select sum( Qty * case when Type='IN' then 1 else -1 end) as Stock_Qty,sum(qty) as qty,Vendor_Code,max(vendor_name) as vendor_name,sum(FAT_KG) as FAT_KG ,sum(SNF_KG) as SNF_KG,SRNDate,Type as Type,max(UOM) as UOM,sum(FAT_KG * case when Type='IN' then 1 else -1 end) as Balance_FAT,sum(SNF_KG * case when Type='IN' then 1 else -1 end) as Balance_SNF from ( " & _
                                   " ( select Doc_No,Location,TSPL_LOCATION_MASTER.Location_Desc,InnerQry.Item_Code,Item_Desc,To_Location_Code,Tolocation.Location_Desc as sub_Location ,InnerQry.Vendor_Code,vendor_name,FAT_KG ,SNF_KG,SRNDate,Qty,InnerQry.Type,UOM  from  " & _
                                  " " + innerqry + " as DD group by Vendor_Code ,SRNDate,type) as XX group by Vendor_Code ,SRNDate ) as m  where (convert(date,SRNdate,103) between convert(date,'" + txtFromDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))  " & _
                                   " )as Final Order by  convert(date,SRNdate,103),Vendor_Code"
        Dim dtgv As New DataTable
        If clsCommon.myCstr(clsCommon.CompairString(cboType.SelectedIndex, "0") = CompairStringResult.Equal) Then
            dtgv = clsDBFuncationality.GetDataTable(Qry)

        End If

        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            gv.BestFitColumns()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

    End Sub
    Sub FormatGrid()

        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
       
        If clsCommon.CompairString(cboType.SelectedIndex, "0") = CompairStringResult.Equal Then
            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("vendor_name").IsVisible = True
            gv.Columns("vendor_name").Width = 100
            gv.Columns("vendor_name").HeaderText = "Vendor Name"

            gv.Columns("OPQty").IsVisible = True
            gv.Columns("OPQty").Width = 100
            gv.Columns("OPQty").HeaderText = "Opening Qty"

            gv.Columns("OPFAT").IsVisible = True
            gv.Columns("OPFAT").Width = 100
            gv.Columns("OPFAT").HeaderText = "Opening FAT"

            gv.Columns("OPSNF").IsVisible = True
            gv.Columns("OPSNF").Width = 100
            gv.Columns("OPSNF").HeaderText = "Opening SNF"

            gv.Columns("RecQty").IsVisible = True
            gv.Columns("RecQty").Width = 100
            gv.Columns("RecQty").HeaderText = "Issue Qty"

            gv.Columns("RecFat").IsVisible = True
            gv.Columns("RecFat").Width = 100
            gv.Columns("RecFat").HeaderText = "Issue FAT"

            gv.Columns("RecSNF").IsVisible = True
            gv.Columns("RecSNF").Width = 100
            gv.Columns("RecSNF").HeaderText = "Issue SNF"

            gv.Columns("IssQty").IsVisible = True
            gv.Columns("IssQty").Width = 100
            gv.Columns("IssQty").HeaderText = "Receive Qty"

            gv.Columns("IssFat").IsVisible = True
            gv.Columns("IssFat").Width = 100
            gv.Columns("IssFat").HeaderText = "Receive FAT"

            gv.Columns("IssSNF").IsVisible = True
            gv.Columns("IssSNF").Width = 100
            gv.Columns("IssSNF").HeaderText = "Receive SNF"

            gv.Columns("CLQty").IsVisible = True
            gv.Columns("CLQty").Width = 100
            gv.Columns("CLQty").HeaderText = "Closing Qty"

            gv.Columns("CLFAT").IsVisible = True
            gv.Columns("CLFAT").Width = 100
            gv.Columns("CLFAT").HeaderText = "Closing FAT"

            gv.Columns("CLSNF").IsVisible = True
            gv.Columns("CLSNF").Width = 100
            gv.Columns("CLSNF").HeaderText = "Closing SNF"

            gv.Columns("Recovery_fAT").IsVisible = True
            gv.Columns("Recovery_fAT").Width = 100
            gv.Columns("Recovery_fAT").HeaderText = "Recovery FAT"

            gv.Columns("Recovery_SNF").IsVisible = True
            gv.Columns("Recovery_SNF").Width = 100
            gv.Columns("Recovery_SNF").HeaderText = "Recovery SNF"

            gv.Columns("SRNDate").IsVisible = True
            gv.Columns("SRNDate").Width = 100
            gv.Columns("SRNDate").HeaderText = "Date"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("OPQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("RecQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("RecFat", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("RecSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("IssQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("IssFat", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("IssSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("CLQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("CLFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item12 As New GridViewSummaryItem("CLSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("Recovery_fAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item14 As New GridViewSummaryItem("Recovery_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            gv.GroupDescriptors.Add(New GridGroupByExpression("Vendor_Code""{0}: {1}"" Group By Vendor_Code"))
            'gv.GroupDescriptors.Add(New GridGroupByExpression("Item_Code""{0}: {1}"" Group By Item_Code"))

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            View()
        Else
            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location Code"

            gv.Columns("Location_Desc").IsVisible = True
            gv.Columns("Location_Desc").Width = 100
            gv.Columns("Location_Desc").HeaderText = "Location Name"

            gv.Columns("Doc_No").IsVisible = True
            gv.Columns("Doc_No").Width = 100
            gv.Columns("Doc_No").HeaderText = "Document No."

            gv.Columns("SRNdate").IsVisible = True
            gv.Columns("SRNdate").Width = 100
            gv.Columns("SRNdate").HeaderText = "Date"
            gv.Columns("SRNdate").FormatString = "{0:d}"

            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 100
            gv.Columns("Item_Code").HeaderText = "Item Code"


            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 100
            gv.Columns("Item_Desc").HeaderText = "Item Name"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("vendor_name").IsVisible = True
            gv.Columns("vendor_name").Width = 100
            gv.Columns("vendor_name").HeaderText = "Vendor Name"

            gv.Columns("FAT_KG").IsVisible = True
            gv.Columns("FAT_KG").Width = 100
            gv.Columns("FAT_KG").HeaderText = "FAT"

            gv.Columns("SNF_KG").IsVisible = True
            gv.Columns("SNF_KG").Width = 100
            gv.Columns("SNF_KG").HeaderText = "SNF"

            gv.Columns("Qty").IsVisible = True
            gv.Columns("Qty").Width = 100
            gv.Columns("Qty").HeaderText = "Qty"



            gv.Columns("UOM").IsVisible = True
            gv.Columns("UOM").Width = 100
            gv.Columns("UOM").HeaderText = "UOM"

            'gv.Columns("Transtype").IsVisible = True
            'gv.Columns("Transtype").Width = 100
            'gv.Columns("Transtype").HeaderText = "Type"

        End If

    End Sub
    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Vendor_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("vendor_name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SRNdate").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Opening Stock"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("OPQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("OPFAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("OPSNF").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sent Stock"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RecQty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RecFat").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RecSNF").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Receipt"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("IssQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("IssFat").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("IssSNF").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Recovery"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Recovery_fAT").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Recovery_SNF").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Closing"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("CLQty").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("CLFAT").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("CLSNF").Name)
            gv.ViewDefinition = view
        End If

    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiVendor.arrValueMember)
                arrHeader.Add((" Vendor : " + strLocationName + " "))
           
            End If
           

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Job Work Status", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Job Work Status", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        TxtMultiSelectLocation.Visible = False
        TxtMultiRGP.Visible = False
        lblRGP.Visible = False
        lblSRN.Visible = False
        TxtMultiSRN.Visible = False
        lblLocation.Visible = False
        cboType.SelectedIndex = 0
        Dim view As New TableViewDefinition()
        gv.ViewDefinition = view
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(cboType.SelectedIndex, 1) = CompairStringResult.Equal Then
                arrBack.Remove("Summary")

                TxtMultiVendor.arrValueMember = arrVendor
                TxtMultiRGP.arrValueMember = Nothing
                cboType.SelectedIndex = 0
                txtFromDate.Value = clsCommon.GetPrintDate(StrFromDate, "dd-MMM-yyyy")
                txtToDate.Value = clsCommon.GetPrintDate(StrToDate, "dd-MMM-yyyy")
                loaddata()

            Else
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            IsInsideLoaddata = False
        End Try
    End Sub

    'Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
    '    print(EnumExportTo.Excel)
    'End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptJobWorkStatus & "'"))

            If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiVendor.arrValueMember)
                arrHeader.Add(("Vendor : " + strLocationName + " "))

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
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                clsCommon.MyExportToExcelGrid("Job Work Status", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Job Work Status", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub RptJobWorkStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LoadLocation()
        reset()
    End Sub
    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub TxtMultiSelectLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type ='Physical'"
        TxtMultiSelectLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", TxtMultiSelectLocation.arrValueMember, TxtMultiSelectLocation.arrDispalyMember)
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        'If clsCommon.myCstr(clsCommon.CompairString(cboType.SelectedIndex, "1") = CompairStringResult.Equal) Then
        '    TxtMultiSelectLocation.Visible = True
        '    lblLocation.Visible = True
        'Else
        '    TxtMultiSelectLocation.Visible = False
        '    lblLocation.Visible = False
        'End If
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
       
    End Sub
    Sub DrillDown()
        Try
            If clsCommon.CompairString(cboType.SelectedIndex, 0) = CompairStringResult.Equal Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                cboType.SelectedIndex = 1
                arrVendor = New ArrayList()
                arrVendor = TxtMultiVendor.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Vendor_Code").Value))
                TxtMultiVendor.arrValueMember = tmp
                Dim tmp1 As Date
                tmp1 = (clsCommon.myCDate(gv.CurrentRow.Cells("SRNdate").Value))
                txtFromDate.Value = tmp1
                txtToDate.Value = tmp1
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    
    
    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If clsCommon.CompairString(cboType.SelectedIndex, 0) = CompairStringResult.Equal Then
            DrillDown()
            If TxtMultiVendor.arrValueMember.Count > 0 Then
                Dim strDoc As String = String.Empty
                Dim qry As String = Nothing
                If e.Column Is gv.Columns("IssQty") AndAlso clsCommon.myLen(gv.Columns("IssQty")) > 0 Then
                    qry = "select Doc_No as [SRN No],SRNDate as [SRN Date],InnerQry.Item_Code as [Item Code],Item_Desc as [Item Name],Location ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],UOM as [UOM],Qty as [SRN Qty],Net_Weight as [Net Weight],fat_per as [FAT%],snf_Per as [SNF%]  ,FAT_KG as [FAT KG],SNF_KG as [SNF KG] from  "
                    qry += " (select TSPL_JOB_MILK_SRN.SRN_NO as Doc_No , TSPL_JOB_MILK_SRN.Loc_Code as Location ,Item_Code ,'' as To_Location_Code,Vendor_Code ,fat_KG ,SNF_KG "
                    qry += " ,convert(varchar,SRN_Date,103) as SRNDate ,Qty ,'OUT' as Type,UOM,Net_Weight,fat_per ,snf_Per   from TSPL_JOB_MILK_SRN "
                    qry += " where convert(date,SRN_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,SRN_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"
                    If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                        qry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                    End If
                    'If TxtMultiSRN.arrValueMember IsNot Nothing AndAlso TxtMultiSRN.arrValueMember.Count > 0 Then
                    '    qry += " and TSPL_JOB_MILK_SRN.SRN_NO in  (" + clsCommon.GetMulcallString(TxtMultiSRN.arrValueMember) + ") "
                    'End If
                    'If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
                    '    qry += " and TSPL_JOB_MILK_SRN.Loc_Code in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
                    'End If
                    qry += " ) as InnerQry"
                    qry += "  left join tspl_item_master on tspl_item_master.Item_Code =InnerQry.Item_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =InnerQry.Location "
                    qry += "  left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = InnerQry.Vendor_Code  left join TSPL_LOCATION_MASTER as Tolocation on InnerQry.To_Location_Code =Tolocation.Location_Code "
                    Dim dtgv1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtgv1 IsNot Nothing And dtgv1.Rows.Count > 0 Then
                        gv.DataSource = Nothing
                        gv.Rows.Clear()
                        gv.ReadOnly = True
                        Dim view As New TableViewDefinition()
                        gv.ViewDefinition = view
                        gv.Columns.Clear()
                        gv.DataSource = dtgv1
                        gv.GroupDescriptors.Clear()
                        'FormatGrid()
                        gv.MasterTemplate.SummaryRowsBottom.Clear()
                        gv.BestFitColumns()
                        RadPageView1.SelectedPage = RadPageViewPage2
                    Else
                        clsCommon.MyMessageBoxShow("No Data Found")
                    End If

                ElseIf e.Column Is gv.Columns("RecQty") AndAlso clsCommon.myLen(gv.Columns("RecQty")) > 0 Then
                    qry = "select Doc_No as [RGP No],SRNDate as [RGP Date],InnerQry.Item_Code as [Item Code],Item_Desc as [Item Name] ,Location ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],InnerQry.Vendor_Code as [Vendor Code],vendor_name as [Vendor Name],Tanker_No as [Tanker No],UOM as [UOM],Qty as Qty,FAT_Pers as [FAT%] ,SNF_Pers as [SNF%] ,FAT_KG as [FAT KG] ,SNF_KG as [SNF KG]  from    (select TSPL_MILK_RGP_HEAD.RGP_No as Doc_No,TSPL_MILK_RGP_HEAD.Location ,Item_Code,TSPL_MILK_RGP_DETAIL.To_Location_Code ,Vendor_Code  ,FAT_KG ,SNF_KG   ,convert(varchar,rgp_date,103) as SRNDate  ,RGP_Qty as Qty,'IN' as Type,Unit_code as UOM ,TSPL_MILK_RGP_DETAIL.Tanker_No,FAT_Pers ,SNF_Pers from TSPL_MILK_RGP_DETAIL left join TSPL_MILK_RGP_HEAD on TSPL_MILK_RGP_HEAD.RGP_No =TSPL_MILK_RGP_DETAIL.RGP_No "
                    qry += " where convert(date,rgp_date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,rgp_date,103)<=convert(date,'" + txtToDate.Value + "',103)"
                    If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                        qry += " and Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                    End If
                    'If TxtMultiRGP.arrValueMember IsNot Nothing AndAlso TxtMultiRGP.arrValueMember.Count > 0 Then
                    '    qry += " and TSPL_MILK_RGP_HEAD.RGP_No in  (" + clsCommon.GetMulcallString(TxtMultiRGP.arrValueMember) + ") "
                    'End If
                    'If TxtMultiSelectLocation.arrValueMember IsNot Nothing AndAlso TxtMultiSelectLocation.arrValueMember.Count > 0 Then
                    '    qry += " and TSPL_MILK_RGP_HEAD.Location in  (" + clsCommon.GetMulcallString(TxtMultiSelectLocation.arrValueMember) + ") "
                    'End If
                    qry += " ) as InnerQry"
                    qry += "  left join tspl_item_master on tspl_item_master.Item_Code =InnerQry.Item_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =InnerQry.Location  left join tspl_vendor_master on  tspl_vendor_master.Vendor_Code = InnerQry.Vendor_Code  left join TSPL_LOCATION_MASTER as Tolocation on InnerQry.To_Location_Code =Tolocation.Location_Code  "
                    Dim dtgv1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtgv1 IsNot Nothing And dtgv1.Rows.Count > 0 Then
                        gv.DataSource = Nothing
                        gv.Rows.Clear()
                        gv.ReadOnly = True
                        Dim view As New TableViewDefinition()
                        gv.ViewDefinition = view
                        gv.Columns.Clear()
                        gv.DataSource = dtgv1
                        gv.GroupDescriptors.Clear()
                        'FormatGrid()
                        gv.MasterTemplate.SummaryRowsBottom.Clear()
                        gv.BestFitColumns()
                        RadPageView1.SelectedPage = RadPageViewPage2
                    Else
                        clsCommon.MyMessageBoxShow("No Data Found")
                    End If
                ElseIf e.Column IsNot gv.Columns("IssQty") OrElse e.Column IsNot gv.Columns("RecQty") Then
                    cboType.SelectedIndex = 0
                    clsCommon.MyMessageBoxShow("No Data Found")
                End If
            

            End If



        ElseIf clsCommon.CompairString(cboType.SelectedIndex, 1) = CompairStringResult.Equal Then
            If TxtMultiVendor.arrValueMember.Count > 0 Then
                Dim strDoc As String = String.Empty
                If e.Column Is gv.Columns("SRN No") AndAlso clsCommon.myLen(gv.Columns("SRN No")) > 0 Then
                    strDoc = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("SRN No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, strDoc)
                ElseIf e.Column Is gv.Columns("RGP No") AndAlso clsCommon.myLen(gv.Columns("RGP No")) > 0 Then
                    strDoc = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("RGP No").Value)
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkJobWork, strDoc)
                End If
            End If
        End If

        'ReStoreGridLayout()
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

    Private Sub txtRecFat_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRecFat.KeyDown

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class
