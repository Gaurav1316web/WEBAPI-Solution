Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine

'' created by richa Agarwal on 27 Nov,2021 UCD/18/11/21-000019
Public Class rptMultipleDeductionReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim AreaWiseBilling As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub rptMultipleDeductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Visible = AreaWiseBilling
        lblArea.Visible = AreaWiseBilling
        Reset()
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtLocation.arrValueMember = Nothing
        txtMultiVSP.arrValueMember = Nothing
        TxtDeductionCode.arrValueMember = Nothing
        TxtItem.arrDispalyMember = Nothing
        chkItemWise.Checked = False
        fndArea.Value = Nothing
        LoadTypes()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        'Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        Gv1.Columns("Vendor Code").IsVisible = True
        Gv1.Columns("Vendor Code").Width = 100
        Gv1.Columns("Vendor Code").HeaderText = "Vendor Code"

        Gv1.Columns("VLC Uploader Code").IsVisible = True
        Gv1.Columns("VLC Uploader Code").Width = 100
        Gv1.Columns("VLC Uploader Code").HeaderText = "VLC Uploader Code"

        Gv1.Columns("Document No").IsVisible = True
        Gv1.Columns("Document No").Width = 100
        Gv1.Columns("Document No").HeaderText = "Document No"

        Gv1.Columns("Vendor Name").IsVisible = True
        Gv1.Columns("Vendor Name").Width = 100
        Gv1.Columns("Vendor Name").HeaderText = "Vendor Name"

        Gv1.Columns("Type").IsVisible = True
        Gv1.Columns("Type").Width = 100
        Gv1.Columns("Type").HeaderText = "Type"

        Gv1.Columns("Document Date").IsVisible = True
        Gv1.Columns("Document Date").Width = 100
        Gv1.Columns("Document Date").HeaderText = "Document Date"

        Gv1.Columns("Addition").IsVisible = True
        Gv1.Columns("Addition").Width = 100
        Gv1.Columns("Addition").HeaderText = "Addition"

        Gv1.Columns("Deduction").IsVisible = True
        Gv1.Columns("Deduction").Width = 100
        Gv1.Columns("Deduction").HeaderText = "Deduction"

        Gv1.Columns("Deduction Code").IsVisible = True
        Gv1.Columns("Deduction Code").Width = 100
        Gv1.Columns("Deduction Code").HeaderText = "Addition/Deduction Code"
        Gv1.MasterTemplate.Columns("Deduction Code").TextAlignment = ContentAlignment.MiddleCenter

        Gv1.Columns("Deduction Desc").IsVisible = True
        Gv1.Columns("Deduction Desc").Width = 100
        Gv1.Columns("Deduction Desc").HeaderText = "Deduction Desc"
        Gv1.MasterTemplate.Columns("Deduction Desc").TextAlignment = ContentAlignment.MiddleCenter

        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Deduction", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Addition", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            TemplateGridview = Gv1
            Dim strBaseqry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            If chkItemWise.Checked = True Then
                Dim strAliasCol As String = "( tspl_item_master.Item_Desc )"
                Dim ItemInUse As String = " tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Document_Code=tspl_sd_shipment_head.Document_code left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_code where tspl_sd_shipment_head.Trans_Type='MCC' and tspl_sd_shipment_head.status=1 "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    ItemInUse += " and tspl_sd_shipment_head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                    ItemInUse += " and tspl_sd_shipment_head.Customer_Code in (" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
                End If



                Dim strItem As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                Dim strItemTotal2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                Dim strItemwhrcls As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + ") as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                qry = "Select Route_No as [Route Code]," & strItem & "," & strItemTotal2 & " as Total
from (Select Final.Route_No,Final.Item_code,Final.Item_Desc,isnull(Sum(final.QtyInStockingUnit),0) as Qty From (select TSPL_VLC_MASTER_HEAD.Route_Code as Route_No,tspl_item_Uom_detail.UOM_Code,TSPL_SD_SHIPMENT_DETAIL.Item_code,tspl_item_master.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,--ConvertedUnit.Conversion_Factor,isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1),
isnull((TSPL_SD_SHIPMENT_DETAIL.Qty*isnull(ConvertedUnit.Conversion_Factor,1)/isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))  ,0) as QtyInStockingUnit
from tspl_sd_shipment_head
left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Document_Code=tspl_sd_shipment_head.Document_code
left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_code
LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_CODE= tspl_sd_shipment_head.Customer_Code
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_code   AND TSPL_ITEM_UOM_DETAIL.Stocking_unit='Y' left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_code  and ConvertedUnit.uom_code=TSPL_SD_SHIPMENT_DETAIL.unit_code
where tspl_sd_shipment_head.Trans_Type='MCC' 
and tspl_sd_shipment_head.status=1 "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and tspl_sd_shipment_head.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                    qry += " and tspl_sd_shipment_head.Customer_Code in (" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
                End If

                qry += " )Final
group by Final.Route_No,Final.Item_code,Final.Item_Desc
) t
PIVOT(
sum(Qty) 
FOR Item_Desc IN (
" & strItemwhrcls & "
)
) AS pivot_table
group by Route_no
"
            Else

                strBaseqry = "select TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.amount  end as Deduction,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code]  from TSPL_MULTIPLE_DEDUCTION_HEAD 
LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
left outer join TSPL_MCC_MASTER ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_Code
where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103) "


                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    strBaseqry += " and TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                    strBaseqry += " and TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code in(" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
                End If

                If TxtDeductionCode.arrValueMember IsNot Nothing AndAlso TxtDeductionCode.arrValueMember.Count > 0 Then
                    strBaseqry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode in (" + clsCommon.GetMulcallString(TxtDeductionCode.arrValueMember) + ")"
                End If

                If clsCommon.myLen(fndArea.Value) > 0 Then
                    strBaseqry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                End If



                If clsCommon.CompairString(ddlReportType.SelectedValue, "Document Wise") = CompairStringResult.Equal Then
                    qry = " Select final.Vendor_Code as [Vendor Code] ,max(final.Vendor_Name) as [Vendor Name],max(final.[VLC Uploader Code]) as [VLC Uploader Code],max(final.Type) as Type,final.Document_No as [Document No],final.Document_Date as [Document Date],sum(final.Addition) as Addition,sum(final.Deduction) as Deduction,final.DeductionCode as [Deduction Code] ,max(final.Deduction_Desc) as [Deduction Desc] from ( " & strBaseqry & " )Final group by final.Document_No,final.Document_Date , final.Vendor_Code ,final.DeductionCode  "
                Else
                    qry = strBaseqry

                End If
            End If
            ''qry += " group by final.Cust_Code,final.route_no  ,final.Item_Code  ,final.Payment_Mode ,final.Document_Date ,final.Loc_Code  order by convert(date,final.Document_Date,103)  "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            'FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    'Gv1.Rows.Add()
                Next


                'Gv1.Rows.Add()
                'dt.Rows.Add(Gv1)

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                'Gv1.Columns("Amount").FormatString = "{0:n2}"
                'Gv1.Columns("ToParty").IsVisible = False
                'Gv1.Columns("Cheque/DD No").IsVisible = False
                'Gv1.Columns("Cheque Date").IsVisible = False
                'Gv1.Columns("Bank/Branch").IsVisible = False
                'Gv1.Columns("Receipt No").IsVisible = False
                'Gv1.Columns("Receipt Type").IsVisible = False


                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(Amount)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'Gv1.Rows.Add()
                FormatGrid()
                Gv1.BestFitColumns()

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(clsUserMgtCode.rptMultipleDeductionReport) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.rptMultipleDeductionReport, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where  Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        'Dim qry As String = "select MCC_Code as Code ,MCC_NAME  as Name from TSPL_MCC_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = clsUserMgtCode.rptMultipleDeductionReport
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(clsUserMgtCode.rptMultipleDeductionReport, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Addition/Deduction Report")
            arrHeader.Add(("Date Range:  " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtMultiVSP.arrDispalyMember IsNot Nothing AndAlso txtMultiVSP.arrDispalyMember.Count > 0 Then
                arrHeader.Add("VSP Code : " + clsCommon.GetMulcallStringWithComma(txtMultiVSP.arrDispalyMember))
            End If


            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, clsUserMgtCode.rptMultipleDeductionReport)
                clsCommon.MyExportToExcelGrid("Multiple Deduction Report", Gv1, arrHeader, Me.Text)
            Else

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                Dim style As New GridPrintStyle()
                style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                style.PrintSummaries = True
                Gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1

                doc.DocumentName = objCommonVar.CurrentCompanyName
                doc.LeftHeader = "Name : Addition/Deduction Report" + Environment.NewLine + "Date Range:  " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " " +
Environment.NewLine + "Company : " & objCommonVar.CurrentCompanyName
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Regular)

                doc.AssociatedObject = Gv1

                doc.RightFooter = "Page [Page #] Of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show()

                doc.Print()
                'clsCommon.MyExportToPDF("Addition/Deduction Report", Gv1, arrHeader, Me.Text, clsUserMgtCode.rptMultipleDeductionReport, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtMultiVSP__My_Click(sender As Object, e As EventArgs) Handles txtMultiVSP._My_Click
        Dim qry As String = " select M.Vendor_Code AS [Code], m.Vendor_Name as [Name],ISNULL(m.alies_name,'') As [Alies Name],TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [Plant Name],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code " &
                               " left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = M.Vendor_Code " &
                               " Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC " &
                               " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code where m.Status='N' "
        txtMultiVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "Name", txtMultiVSP.arrValueMember, txtMultiVSP.arrDispalyMember)
    End Sub



    Private Sub TxtDeductionCode__My_Click(sender As Object, e As EventArgs) Handles TxtDeductionCode._My_Click
        Dim qry As String = " Select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Ded_Grp_Code As [Deduction Group Code],TSPL_DEDUCTION_GROUP.Ded_Description As [Deduction Group Description] ,TSPL_DEDUCTION_MASTER.GL_Account_Code As [GL Account],TSPL_GL_ACCOUNTS.Description As [GL Account Desc],Security  from TSPL_DEDUCTION_MASTER  left outer join TSPL_DEDUCTION_GROUP On TSPL_DEDUCTION_GROUP.Ded_Code=TSPL_DEDUCTION_MASTER.Ded_Grp_Code  left outer join TSPL_GL_ACCOUNTS On TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code    where Security ='0' "
        TxtDeductionCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DEDMulSel", qry, "Code", "Description", TxtDeductionCode.arrValueMember, TxtDeductionCode.arrDispalyMember)
    End Sub

    Sub LoadTypes()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail Wise")

        ''dt.Rows.Add("Distributor Wise")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Private Sub chkItemWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkItemWise.ToggleStateChanged
        If chkItemWise.Checked Then
            TxtDeductionCode.Enabled = False
            TxtItem.Enabled = True
        Else
            TxtDeductionCode.Enabled = True
            TxtItem.Enabled = False
        End If
    End Sub

    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click
        Dim qry As String = " Select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Ded_Grp_Code As [Deduction Group Code],TSPL_DEDUCTION_GROUP.Ded_Description As [Deduction Group Description] ,TSPL_DEDUCTION_MASTER.GL_Account_Code As [GL Account],TSPL_GL_ACCOUNTS.Description As [GL Account Desc],Security  from TSPL_DEDUCTION_MASTER  left outer join TSPL_DEDUCTION_GROUP On TSPL_DEDUCTION_GROUP.Ded_Code=TSPL_DEDUCTION_MASTER.Ded_Grp_Code  left outer join TSPL_GL_ACCOUNTS On TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code    where Security ='0' "
        TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemDMulSel", qry, "Code", "Description", TxtItem.arrValueMember, TxtItem.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim strQry As String = Nothing
            '' Dim subqry As String = Nothing
            Dim strQry1 As String = Nothing

            Dim strQry2 As String = Nothing
            Dim strQry3 As String = Nothing
            Dim strQry4 As String = Nothing
            Dim strQry5 As String = Nothing
            ' Dim chktranstype As String = Nothing

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strQry1 += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtMultiVSP.arrValueMember IsNot Nothing AndAlso txtMultiVSP.arrValueMember.Count > 0 Then
                strQry2 += " and TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code in(" + clsCommon.GetMulcallString(txtMultiVSP.arrValueMember) + ")"
            End If

            If TxtDeductionCode.arrValueMember IsNot Nothing AndAlso TxtDeductionCode.arrValueMember.Count > 0 Then
                strQry3 += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode in (" + clsCommon.GetMulcallString(TxtDeductionCode.arrValueMember) + ") "
                strQry4 += "and (TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code in (" + clsCommon.GetMulcallString(TxtDeductionCode.arrValueMember) + ")"
            End If
            If clsCommon.myLen(fndArea.Value) > 0 Then
                strQry5 += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
            End If



            strQry = "  select xx.SNo,xx.company_name,xx.[Vendor Code],xx.[Vendor Name],xx.[VLC Uploader Code],xx.MCC_Name,xx.[Document Date],xx.[Document No],
                        xx.Type,xx.Addition,xx.Deduction,xx.[Deduction Code],xx.Regn_No,xx.[Deduction Desc],xx.[SRN Qty],xx.Phone,xx.Remarks,xx.SRN_AMOUNT,xx.FromDate,xx.ToDate,xx.Reduce_Deduc_Amt,xx.ReduceAmt,xx.User_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 from
                        (select round(row_number() over(order by(select 1)),0) as SNo,FINAL3.company_name,FINAL3.[Vendor Code],FINAL3.[Vendor Name],FINAL3.[VLC Uploader Code],FINAL3.MCC_Name,FINAL3.[Document Date],FINAL3.[Document No],FINAL3.Type,FINAL3.Addition,FINAL3.Deduction,FINAL3.[Deduction Code],FINAL3.Regn_No,FINAL3.[Deduction Desc],final3.[SRN Qty],FINAL3.Phone,final3.Remarks,final3.SRN_AMOUNT,final3.FromDate, final3.ToDate,sum(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt) as Reduce_Deduc_Amt,
                        CASE
                          WHEN final3.Type = 'd' THEN (FINAL3.Deduction - SUM(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt))
                          WHEN final3.Type = 'a' THEN final3.Addition
                        END AS 'ReduceAmt','" & objCommonVar.CurrentUser & "' as User_Name,final3.Comp_Code
                         from (
                          SELECT  FINAL2.company_name,FINAL2.[Vendor Code],FINAL2.[Vendor Name],FINAL2.[VLC Uploader Code],FINAL2.MCC_Name,FINAL2.[Document Date],
                        FINAL2.[Document No],FINAL2.Type,FINAL2.Addition,FINAL2.Deduction,FINAL2.[Deduction Code],FINAL2.Regn_No,FINAL2.[Deduction Desc],
                        FINAL2.ACC_WEIGHT,FINAL2.Phone,final2.Remarks,SUM(TSPL_MILK_SRN_DETAIL.AMOUNT) AS SRN_AMOUNT,sum(TSPL_MILK_SRN_DETAIL.Qty) as [SRN Qty],
                        final2.FromDate, final2.ToDate,final2.Comp_Code  FROM (
                         select FINAL1.company_name,FINAL1.[Vendor Code],FINAL1.[Vendor Name],FINAL1.[VLC Uploader Code],FINAL1.Regn_No,FINAL1.MCC_Name,
                        FINAL1.[Document Date],FINAL1.[Document No],FINAL1.Type,FINAL1.Addition,FINAL1.Deduction,FINAL1.[Deduction Code],FINAL1.Phone,final1.Remarks,
                        FINAL1.[Deduction Desc],FINAL1.FromDate, final1.ToDate, SUM(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT) AS ACC_WEIGHT,FINAL1.Comp_Code FROM (
                         Select  max(Final.company_name) as company_name,final.Vendor_Code as [Vendor Code] ,max(final.Vendor_Name) as [Vendor Name],
                        max(final.[VLC Uploader Code]) as [VLC Uploader Code],max(final.MCC_Name) as MCC_Name,max(final.Regn_No) as Regn_No,max(final.Phone1) as Phone,
                        max(final.Remarks) as Remarks, max(final.Type) as Type,final.Document_No as [Document No],final.Document_Date as [Document Date],
                        sum(final.Addition) as Addition,sum(final.Deduction) as Deduction,final.DeductionCode as [Deduction Code] ,
                        max(final.Deduction_Desc) as [Deduction Desc],'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As FromDate
                        ,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' As ToDate,'" + objCommonVar.CurrentUser + "' as User_Name,max(Comp_Code)Comp_Code From 
                        ( select TSPL_COMPANY_MASTER.Comp_Name as company_name,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name,
                        case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,
                        convert(varchar,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date  ,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then TSPL_MULTIPLE_DEDUCTION_detail.amount else 0 end as Addition,
                        case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 0 else TSPL_MULTIPLE_DEDUCTION_detail.Amount end  as Deduction, 
                        TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc ,cast(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as integer) as [VLC Uploader Code],
                        TSPL_MCC_MASTER.MCC_Name,TSPL_COMPANY_MASTER.Regn_No,TSPL_COMPANY_MASTER.Phone1,TSPL_MULTIPLE_DEDUCTION_DETAIL.Remarks,TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code from TSPL_MULTIPLE_DEDUCTION_HEAD 
                         left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MULTIPLE_DEDUCTION_HEAD.Comp_Code
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                         left outer  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) >= convert(date,('" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) <= convert(date,('" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'),103) " + strQry3 + strQry1 + strQry5 + "  )Final 
                        group by final.company_name,final.Document_No,final.Document_Date , final.Vendor_Code ,final.DeductionCode  
                        ) FINAL1
                        LEFT OUTER JOIN TSPL_MILK_RECEIPT_DETAIL ON TSPL_MILK_RECEIPT_DETAIL.VSP_CODE=FINAL1.[Vendor Code]
                        WHERE convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103) >= convert(date,('" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103) <= convert(date,('" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'),103)
                        GROUP BY 
                        final1.company_name,FINAL1.[Vendor Code],FINAL1.[Vendor Name],FINAL1.[VLC Uploader Code],final1.MCC_Name
                        ,FINAL1.[Document Date],FINAL1.[Document No],FINAL1.Type,FINAL1.Addition,FINAL1.Deduction,FINAL1.[Deduction Code],FINAL1.Regn_No,
                        FINAL1.[Deduction Desc],FINAL1.Phone,final1.remarks,FINAL1.FromDate,FINAL1.ToDate,final1.Comp_Code
                        ) FINAL2 
                        left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.VSP_CODE=FINAL2.[Vendor Code]
                        left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
                        WHERE convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >= convert(date,('" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,('" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'),103)
                        GROUP BY 
                        FINAL2.company_name,FINAL2.[Vendor Code],FINAL2.[Vendor Name],FINAL2.[VLC Uploader Code],FINAL2.MCC_Name,FINAL2.[Document Date],
                        FINAL2.[Document No],FINAL2.Type,FINAL2.Addition,FINAL2.Deduction,FINAL2.[Deduction Code],FINAL2.Regn_No,FINAL2.[Deduction Desc],
                        FINAL2.ACC_WEIGHT,FINAL2.Phone,final2.remarks,FINAL2.FromDate,FINAL2.ToDate,FINAL2.Comp_Code)final3

                        left join TSPL_PAYMENT_PROCESS_DEDUCTION on TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE=final3.[Vendor Code]
                        left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        WHERE convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) >=convert(date,('" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) <= convert(date,('" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'),103) and final3.Type = 'D'" + strQry4 + " )
	                        or final3.Type='a'
                        group by FINAL3.company_name,FINAL3.[Vendor Code],FINAL3.[Vendor Name],FINAL3.[VLC Uploader Code],FINAL3.MCC_Name,FINAL3.[Document Date],FINAL3.[Document No],
                        FINAL3.Type,FINAL3.Addition,FINAL3.Deduction,FINAL3.[Deduction Code],FINAL3.Regn_No,FINAL3.[Deduction Desc],final3.[SRN Qty],FINAL3.Phone,final3.remarks,
                        final3.SRN_AMOUNT,final3.FromDate, final3.ToDate,final3.Comp_Code)xx 
                        left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=xx.Comp_Code
                        order by  [VLC Uploader Code]"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt1.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt1, "crptMultpleDeductionNewJPR", "MD Print")
                Else
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt1, "crptMultpleDeductionNewGNG", "MD Print")
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub
End Class
