Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Parteek-==============
'============= Ticket No: BM00000010016 by Parteek'
'Ticket No-No  ERO/09/07/19-000677 ,Sanjay Add TSKG,TS%
Public Class RptWreckageReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDairyProductionWreckageReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)

        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        If txtBatchNoMult.arrDispalyMember IsNot Nothing AndAlso txtBatchNoMult.arrDispalyMember.Count > 0 Then

            arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
        End If
        If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
        End If
        Dim qry As String = Nothing
        Dim qtywreckage As String = Nothing
        Dim qtybooking As String = Nothing
        ' Ticket No : KDI/15/05/18-000314 By Prabhakar 
        If chk_stockingunit.Checked Then
            qtywreckage = " stockunitconv.Uom_Code as Unit_Code ,TSPL_UNIT_MASTER.Unit_Desc,  ((isnull(unitconv.Conversion_Factor,1)*BACK_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as BACK_QTY,((isnull(unitconv.Conversion_Factor,1)*WRECKAGE_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as WRECKAGE_QTY"
            qtybooking = " stockunitconv.Uom_Code as Unit_Code ,TSPL_UNIT_MASTER.Unit_Desc,((isnull(unitconv.Conversion_Factor,1)*BACK_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as BACK_QTY, ((isnull(unitconv.Conversion_Factor,1)* (case  when Category = 'Scrap'  then ScrapQty else WRECKAGE_QTY end))/isnull(stockunitconv.Conversion_Factor,1)) as WRECKAGE_QTY "
        Else
            qtywreckage = " TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code ,TSPL_UNIT_MASTER.Unit_Desc,BACK_QTY ,WRECKAGE_QTY"
            qtybooking = " TSPL_WRECKAGE_BOOKING.Unit_Code ,TSPL_UNIT_MASTER.Unit_Desc,BACK_QTY , case  when Category = 'Scrap'  then ScrapQty else WRECKAGE_QTY end as WRECKAGE_QTY "
        End If

        'qry = " select case '' when 'Purchase' then 'Purchase' else 'Purchase' end as Category,case Type when 'PLANT' then 'Purchase'end as TYPE,TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE,convert(varchar,PROD_DATE,103) as  PROD_DATE ,case TSPL_PP_PRODUCTION_ENTRY.Batch_Code when TSPL_PP_PRODUCTION_ENTRY.Batch_Code then TSPL_PP_PRODUCTION_ENTRY.Batch_Code else 'Emplty' end as BATCH_CODE ,convert(varchar,BATCH_DATE,103) as BATCH_DATE ,TSPL_PP_PRODUCTION_ENTRY.Location_Code ,Location_Desc,CONSM_LOCATION_CODE,CONSM_SECTION_CODE,Section_Stage_Map_Code,TSPL_PP_PE_WRECKAGE_FLASHING.Item_Code ,Item_Desc," + qtywreckage + " ,TSPL_PP_PE_WRECKAGE_FLASHING.Location_Code as Wreckage_Location,Avail_FAT_Per ,Avail_FAT_KG ,Avail_SNF_Per ,Avail_SNF_KG ,Remarks  from TSPL_PP_PE_WRECKAGE_FLASHING  left join TSPL_ITEM_MASTER on TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
        qry = " select case '' when 'Purchase' then 'Purchase' else 'Purchase' end as Category,case Type when 'PLANT' then 'Purchase'end as TYPE,TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE,convert(varchar,PROD_DATE,103) as  PROD_DATE ,case TSPL_PP_PRODUCTION_ENTRY.Batch_Code when TSPL_PP_PRODUCTION_ENTRY.Batch_Code then TSPL_PP_PRODUCTION_ENTRY.Batch_Code else 'Emplty' end as BATCH_CODE ,convert(varchar,BATCH_DATE,103) as BATCH_DATE ,TSPL_PP_PRODUCTION_ENTRY.Location_Code ,Location_Desc,CONSM_LOCATION_CODE,CONSM_SECTION_CODE,Section_Stage_Map_Code,TSPL_PP_PE_WRECKAGE_FLASHING.Item_Code ,Item_Desc," + qtywreckage + " ,TSPL_PP_PE_WRECKAGE_FLASHING.Location_Code as Wreckage_Location,Avail_FAT_Per ,Avail_FAT_KG ,Avail_SNF_Per ,Avail_SNF_KG,Avail_FAT_Per+Avail_SNF_Per AS TS_Per,Avail_FAT_KG+Avail_SNF_KG AS TS_KG ,Remarks  from TSPL_PP_PE_WRECKAGE_FLASHING  left join TSPL_ITEM_MASTER on TSPL_PP_PE_WRECKAGE_FLASHING.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  " & _
            " left join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = TSPL_PP_PE_WRECKAGE_FLASHING.PROD_ENTRY_CODE " & _
            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE " &
            " left JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_PP_PRODUCTION_ENTRY.RECEIVED_BY = TSPL_EMPLOYEE_MASTER.EMP_CODE "
        If chk_stockingunit.Checked Then
            qry += "  left join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_PP_PE_WRECKAGE_FLASHING.Item_Code " & _
                    " left join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_PP_PE_WRECKAGE_FLASHING.Item_Code and unitconv.UOM_Code=TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code" & _
                    " left join TSPL_UNIT_MASTER on TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code=TSPL_UNIT_MASTER.Unit_Code"
        Else
            qry += " left join TSPL_UNIT_MASTER on TSPL_PP_PE_WRECKAGE_FLASHING.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "
        End If
        qry += " where  convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

        If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.Batch_Code  in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
        End If
        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_PP_PE_WRECKAGE_FLASHING.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If

        If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
        End If
        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If
        If chkScrap.IsChecked = True Then
            qry += " and Type='Purchase'"
        End If
        If chkWreckage.IsChecked = True Then
            qry += " and Type='Purchase'"
        End If
        qry += "Union All " & _
            " select case TSPL_WRECKAGE_ENTRY.Category when TSPL_WRECKAGE_ENTRY.Category then TSPL_WRECKAGE_ENTRY.Category when '' then 'Purchase' end as Category,case TSPL_LOCATION_MASTER.Type when 'PLANT' then 'Wreckage'end as TYPE,TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE,convert(varchar,PROD_DATE,103) as  PROD_DATE ,TSPL_WRECKAGE_ENTRY.Batch_Code as BATCH_CODE ,convert(varchar,Batch_Code,103) as BATCH_DATE ,TSPL_WRECKAGE_ENTRY.Location_Code ,Location_Desc,CONSM_LOCATION_CODE,CONSM_SECTION_CODE,Section_Stage_Map_Code,TSPL_WRECKAGE_BOOKING.Item_Code ,Item_Desc," + qtybooking + " ,TSPL_WRECKAGE_BOOKING.Location_Code as Wreckage_Location,Avail_FAT_Per ,Avail_FAT_KG ,Avail_SNF_Per ,Avail_SNF_KG,Avail_FAT_Per+Avail_SNF_Per AS TS_Per,Avail_FAT_KG+Avail_SNF_KG AS TS_KG ,Remarks from TSPL_WRECKAGE_BOOKING " & _
             " inner join TSPL_WRECKAGE_ENTRY on TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE=TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE " & _
             " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_WRECKAGE_ENTRY.LOCATION_CODE " & _
             " left join TSPL_ITEM_MASTER on TSPL_WRECKAGE_BOOKING.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE "
        If chk_stockingunit.Checked Then
            qry += "  left join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_WRECKAGE_BOOKING.Item_Code " & _
                    " left join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_WRECKAGE_BOOKING.Item_Code and unitconv.UOM_Code=TSPL_WRECKAGE_BOOKING.Unit_Code" & _
                    " left join TSPL_UNIT_MASTER on TSPL_WRECKAGE_BOOKING.Unit_Code=TSPL_UNIT_MASTER.Unit_Code"
        Else
            qry += " left join TSPL_UNIT_MASTER on TSPL_WRECKAGE_BOOKING.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "
        End If
        qry += " where  convert(date,TSPL_WRECKAGE_ENTRY.PROD_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_WRECKAGE_ENTRY.PROD_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_WRECKAGE_BOOKING.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If
        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If
        If chkScrap.IsChecked = True Then
            qry += " and Category='Scrap'"
        End If
        If chkWreckage.IsChecked = True Then
            qry += " and Category='Wreckage'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            gv1.EnableFiltering = True

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()
        gv1.BestFitColumns()
        chk_stockingunit.Enabled = False
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Category").Width = 50
        gv1.Columns("Category").IsVisible = True
        gv1.Columns("Category").HeaderText = "Category"

        gv1.Columns("PROD_ENTRY_CODE").Width = 50
        gv1.Columns("PROD_ENTRY_CODE").IsVisible = True
        gv1.Columns("PROD_ENTRY_CODE").HeaderText = "Document Code"

        gv1.Columns("PROD_DATE").Width = 50
        gv1.Columns("PROD_DATE").IsVisible = True
        gv1.Columns("PROD_DATE").HeaderText = "Document Date"

        gv1.Columns("Type").Width = 50
        gv1.Columns("Type").IsVisible = False
        gv1.Columns("Type").HeaderText = "Document Type"

        gv1.Columns("Batch_Code").Width = 100
        gv1.Columns("Batch_Code").IsVisible = True
        gv1.Columns("Batch_Code").HeaderText = "Batch Code"

        gv1.Columns("BATCH_DATE").Width = 100
        gv1.Columns("BATCH_DATE").IsVisible = True
        gv1.Columns("BATCH_DATE").HeaderText = "Batch Date"

        gv1.Columns("Location_Code").Width = 100
        gv1.Columns("Location_Code").IsVisible = True
        gv1.Columns("Location_Code").HeaderText = "Location Code"

        gv1.Columns("Location_Desc").Width = 100
        gv1.Columns("Location_Desc").IsVisible = True
        gv1.Columns("Location_Desc").HeaderText = "Location Name"


        gv1.Columns("CONSM_LOCATION_CODE").Width = 100
        gv1.Columns("CONSM_LOCATION_CODE").IsVisible = True
        gv1.Columns("CONSM_LOCATION_CODE").HeaderText = "Consm Location"

        gv1.Columns("CONSM_SECTION_CODE").Width = 100
        gv1.Columns("CONSM_SECTION_CODE").IsVisible = True
        gv1.Columns("CONSM_SECTION_CODE").HeaderText = "Consm Section"

        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("Item_Desc").Width = 100
        gv1.Columns("Item_Desc").IsVisible = True
        gv1.Columns("Item_Desc").HeaderText = "Item Name"

        gv1.Columns("Unit_Desc").Width = 100
        gv1.Columns("Unit_Desc").IsVisible = True
        gv1.Columns("Unit_Desc").HeaderText = "UOM"


        gv1.Columns("BACK_QTY").Width = 100
        gv1.Columns("BACK_QTY").IsVisible = True
        gv1.Columns("BACK_QTY").HeaderText = "Back Qty"

        gv1.Columns("WRECKAGE_QTY").Width = 100
        gv1.Columns("WRECKAGE_QTY").IsVisible = True
        gv1.Columns("WRECKAGE_QTY").HeaderText = "Qty"


        gv1.Columns("Wreckage_Location").Width = 100
        gv1.Columns("Wreckage_Location").IsVisible = True
        gv1.Columns("Wreckage_Location").HeaderText = "Wreckage Location"

        gv1.Columns("Avail_FAT_Per").Width = 100
        gv1.Columns("Avail_FAT_Per").IsVisible = True
        gv1.Columns("Avail_FAT_Per").HeaderText = "[Fat %]"

        gv1.Columns("Avail_FAT_KG").Width = 100
        gv1.Columns("Avail_FAT_KG").IsVisible = True
        gv1.Columns("Avail_FAT_KG").HeaderText = "[Fat KG]"

        gv1.Columns("Avail_SNF_Per").Width = 100
        gv1.Columns("Avail_SNF_Per").IsVisible = True
        gv1.Columns("Avail_SNF_Per").HeaderText = "[SNF %]"

        gv1.Columns("Avail_SNF_KG").Width = 100
        gv1.Columns("Avail_SNF_KG").IsVisible = True
        gv1.Columns("Avail_SNF_KG").HeaderText = "[SNF KG]"

        gv1.Columns("TS_Per").Width = 100
        gv1.Columns("TS_Per").IsVisible = True
        gv1.Columns("TS_Per").HeaderText = "[TS %]"

        gv1.Columns("TS_KG").Width = 100
        gv1.Columns("TS_KG").IsVisible = True
        gv1.Columns("TS_KG").HeaderText = "[TS KG]"

        gv1.Columns("Remarks").Width = 100
        gv1.Columns("Remarks").IsVisible = True
        gv1.Columns("Remarks").HeaderText = "Remarks"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("BACK_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("WRECKAGE_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        chk_stockingunit.Checked = False
        chk_stockingunit.Enabled = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        chkBoth.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub

    Private Sub txtSectionMult__My_Click(sender As Object, e As EventArgs) Handles txtSectionMult._My_Click
        Dim qry As String = " select distinct Section_Code as Code , Section_Code as Name from TSPL_PP_BATCH_ORDER_HEAD where Section_Code is not null "
        txtSectionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtSectionMult", qry, "Code", "Name", txtSectionMult.arrValueMember, txtSectionMult.arrDispalyMember)
    End Sub

    Private Sub txtBatchNoMult__My_Click(sender As Object, e As EventArgs) Handles txtBatchNoMult._My_Click
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Name  from TSPL_PP_BATCH_ORDER_HEAD "
        txtBatchNoMult.arrValueMember = clsCommon.ShowMultipleSelectForm("multBatchNo", qry, "Code", "Name", txtBatchNoMult.arrValueMember, txtBatchNoMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub RptWreckageReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

        Reset()
    End Sub

    Private Sub RptWreckageReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDairyProductionWreckageReport & "'"))
            arrHeader.Add("Report Type : " & IIf(chkBoth.IsChecked = True, "All", IIf(chkScrap.IsChecked = True, "Scrap", "Wreckage")))

            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
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
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDairyProductionWreckageReport & "'"))
            arrHeader.Add("Report Type : " & IIf(chkBoth.IsChecked = True, "All", IIf(chkScrap.IsChecked = True, "Scrap", "Wreckage")))

            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
