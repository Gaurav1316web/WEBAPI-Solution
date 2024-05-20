Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==========ADDING THREE LEVEL DRILL DOWN BY SHIVANI AGAINST TICKET [BM00000008905]
'==============Created By Preeti Gupta==========
Public Class RptMaterialSendForJobWork
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
        ToDate1.Value = clsCommon.GETSERVERDATE()
        fromDate1.Value = ToDate1.Value.AddMonths(-1)
        LoadRGPType()
        txtLocationMul.arrValueMember = Nothing
        txtVendorMult.arrValueMember = Nothing
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        cboType.SelectedIndex = 1
        rbtnSummary.IsChecked = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptMaterialSendforJobWork)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMaterialSendforJobWork & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate1.Value, "dd/MM/yyyy")) + " ")

            If txtLocationMul.arrDispalyMember IsNot Nothing AndAlso txtLocationMul.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            End If

            If txtVendorMult.arrDispalyMember IsNot Nothing AndAlso txtVendorMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendorMult.arrDispalyMember))
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
                clsCommon.MyExportToPDF("Material Send For Job Work", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub




    Private Sub RptMaterialSendForJobWork_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P Adding New")
        Reset()
    End Sub

    Private Sub RptMaterialSendForJobWork_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
    Public Sub Load_Report()
        ''Item Filter added by shivani[BM00000008607]
        If fromDate1.Value > ToDate1.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            fromDate1.Focus()
            Exit Sub
        End If
        Dim sQuery As String = ""

        If clsCommon.CompairString(cboType.SelectedIndex, 1) = CompairStringResult.Equal Then
            sQuery = "select * from (select TSPL_RGP_HEAD.RGP_No as [RGP No],convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as [RGP Date],TSPL_RGP_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name] ,convert(decimal(18,3),TSPL_RGP_DETAIL.RGP_Qty) as RGP_Qty ,TSPL_RGP_DETAIL.Unit_code ," & _
             "case when Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =0 then 'Against Job Work' else case when  Against_JobWork =1 and Against_BOM  =1 and Against_As_It_Is =0 then 'Against BOM' else case when  Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =1 then 'Against as is it' end end end as 'RGP Type' ,TSPL_RGP_HEAD.Vendor_Code ,TSPL_RGP_HEAD.Location, " & _
             " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" & _
             "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" & _
             "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" & _
             "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" & _
             "  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" & _
             "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " & _
             "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " & _
             "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " & _
             "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " & _
             " as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name " & _
             " from TSPL_RGP_HEAD" & _
            " left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_RGP_HEAD.RGP_No " & _
            " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_RGP_DETAIL.Item_Code " & _
            " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code " & _
            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_RGP_HEAD.Location " & _
             " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_RGP_HEAD.comp_code" & _
            " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
          " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
            " ) as final" & _
            "   where 2 = 2 and convert(date,[RGP Date],103)>=convert(date,'" + fromDate1.Value + "',103) and convert(date,[RGP Date],103) <=convert(date,'" + ToDate1.Value + "' ,103)"

            If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
                sQuery += " and isnull([rgp type],'')<>''"
            End If

            If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
                sQuery += " and final.[RGP Type] ='" + ddlRGPType.Text + "'"
            End If

            If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                sQuery += " and final.Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
            End If

            If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
                sQuery += " and final.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                sQuery += " and final.[Item Code] in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
            End If
            sQuery += " order by convert(date,[RGP Date],103)"

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
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dtgv, "crptMaterialSendForJobWork", "Material Send For Job Work")
                    frmCRV = Nothing
                End If

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If

        ElseIf (clsCommon.CompairString(cboType.SelectedIndex, 0) = CompairStringResult.Equal) Then
            sQuery = ""
            sQuery += " select Location as [Location Code],max(Location_Desc) as [Location Name] ,[RGP Type] ,[Item Code],max([Item Name]) as [Item Name]  ,sum(RGP_Qty)as [RGP Qty] ,Unit_code as [Unit Code],Vendor_Code as [Vendor Code],max(Vendor_Name) as [Vendor Name]   from (select final.* from (select TSPL_RGP_HEAD.RGP_No as [RGP No],convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as [RGP Date],TSPL_RGP_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name] ,convert(decimal(18,3),TSPL_RGP_DETAIL.RGP_Qty) as RGP_Qty ,TSPL_RGP_DETAIL.Unit_code ,case when Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =0 then 'Against Job Work' else case when  Against_JobWork =1 and Against_BOM  =1 and Against_As_It_Is =0 then 'Against BOM' else case when  Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =1 then 'Against as is it' end end end as 'RGP Type' ,TSPL_RGP_HEAD.Vendor_Code ,TSPL_RGP_HEAD.Location,  TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name,Location_Desc  from TSPL_RGP_HEAD left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_RGP_HEAD.RGP_No  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_RGP_DETAIL.Item_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_RGP_HEAD.Location  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_RGP_HEAD.comp_code LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  ) as final  " & _
            " where 2 = 2 and convert(date,[RGP Date],103)>=convert(date,'" + fromDate1.Value + "',103) and convert(date,[RGP Date],103) <=convert(date,'" + ToDate1.Value + "' ,103)  "
            If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
                sQuery += " and isnull([rgp type],'')<>''"
            End If

            If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
                sQuery += " and [RGP Type] ='" + ddlRGPType.Text + "'"
            End If

            If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                sQuery += " and Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
            End If

            If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
                sQuery += " and Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                sQuery += " and [Item Code] in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
            End If
            sQuery += " )as dd group by [Item Code],Vendor_Code ,Location ,[RGP Type]  ,Unit_code "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.ReadOnly = True
                gv.BestFitColumns()
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
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

        gv.Columns("RGP No").IsVisible = True
        gv.Columns("RGP No").Width = 100
        gv.Columns("RGP No").HeaderText = "RGP No"



        gv.Columns("RGP Date").IsVisible = True
        gv.Columns("RGP Date").Width = 100
        gv.Columns("RGP Date").HeaderText = "RGP Date"

        gv.Columns("Item Code").IsVisible = True
        gv.Columns("Item Code").Width = 100
        gv.Columns("Item Code").HeaderText = "Item Code"


        gv.Columns("Item Name").IsVisible = True
        gv.Columns("Item Name").Width = 100
        gv.Columns("Item Name").HeaderText = "Item Name"

        gv.Columns("RGP_Qty").IsVisible = True
        gv.Columns("RGP_Qty").Width = 100
        gv.Columns("RGP_Qty").HeaderText = "RGP Qty"

        gv.Columns("Unit_code").IsVisible = True
        gv.Columns("Unit_code").Width = 80
        gv.Columns("Unit_code").HeaderText = "UOM"

        gv.Columns("RGP Type").IsVisible = True
        gv.Columns("RGP Type").Width = 80
        gv.Columns("RGP Type").HeaderText = "RGP Type"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("RGP_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        'Dim item7 As New GridViewSummaryItem("NewAmount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC as Item format ""{0}: {1}"" Group By VLC"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtLocationMul__My_Click(sender As Object, e As EventArgs) Handles txtLocationMul._My_Click
        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Code in (" + arrLoc + ") "
        txtLocationMul.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMSelESI", qry, "Code", "Name", txtLocationMul.arrValueMember, txtLocationMul.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocationMul, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendorMult__My_Click(sender As Object, e As EventArgs) Handles txtVendorMult._My_Click
        Dim qry As String = "select distinct TSPL_RGP_HEAD.Vendor_Code as [Code],TSPL_VENDOR_MASTER.Vendor_Name as [name] from TSPL_RGP_HEAD" & _
                            " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  WHERE  TSPL_VENDOR_MASTER.Status='N'   "
        txtVendorMult.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMSelESI", qry, "Code", "Name", txtVendorMult.arrValueMember, txtVendorMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as Code ,Item_Desc as Name from tspl_item_master"
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If cboType.SelectedIndex = 0 And rbtnSummary.IsChecked = True Then
            DrillDown()
        ElseIf (cboType.SelectedIndex = 1) And rbtnSummary.IsChecked = True Then
            If gv.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("RGP No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransType)
            End If
        ElseIf cboType.SelectedIndex = 0 And rbtnDetail.IsChecked = True Then
            If gv.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("RGP No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransType)
            End If
        End If

      
    End Sub
    Sub qry()
        Dim qry As String = "select final.[RGP No],[RGP Date] ,[Item Code] ,[Item Name] ,RGP_Qty  as [RGP Qty],Unit_code as [Unit Code],[RGP Type] from (select TSPL_RGP_HEAD.RGP_No as [RGP No],convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as [RGP Date],TSPL_RGP_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name] ,convert(decimal(18,3),TSPL_RGP_DETAIL.RGP_Qty) as RGP_Qty ,TSPL_RGP_DETAIL.Unit_code ,case when Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =0 then 'Against Job Work' else case when  Against_JobWork =1 and Against_BOM  =1 and Against_As_It_Is =0 then 'Against BOM' else case when  Against_JobWork =1 and Against_BOM  =0 and Against_As_It_Is =1 then 'Against as is it' end end end as 'RGP Type' ,TSPL_RGP_HEAD.Vendor_Code ,TSPL_RGP_HEAD.Location,  TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name  from TSPL_RGP_HEAD left join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No =TSPL_RGP_HEAD.RGP_No  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_RGP_DETAIL.Item_Code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_RGP_HEAD.Vendor_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_RGP_HEAD.Location  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_RGP_HEAD.comp_code LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  ) as final where 2 = 2 and convert(date,[RGP Date],103)>=convert(date,'" + fromDate1.Value + "',103) and convert(date,[RGP Date],103) <=convert(date,'" + ToDate1.Value + "' ,103) "

        If clsCommon.CompairString(ddlRGPType.Text, "All") = CompairStringResult.Equal Then
            qry += " and isnull([rgp type],'')<>''"
        End If

        If clsCommon.CompairString(ddlRGPType.Text, "All") <> CompairStringResult.Equal Then
            qry += " and [RGP Type] ='" + ddlRGPType.Text + "'"
        End If

        If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
            qry += " and Location in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") "
        End If

        If txtVendorMult.arrValueMember IsNot Nothing AndAlso txtVendorMult.arrValueMember.Count > 0 Then
            qry += " and Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorMult.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            qry += " and [Item Code] in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        qry += " order by convert(date,[RGP Date],103) "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
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
                tmp2.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Item Code").Value))
                TxtMultiItem.arrValueMember = tmp2
                qry()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

   
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        
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
