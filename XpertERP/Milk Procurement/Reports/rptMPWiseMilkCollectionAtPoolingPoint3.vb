Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptMPWiseMilkCollectionAtPoolingPoint3

    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim DisplayAverageFatSNFMPWise As Boolean = False
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
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
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMPWiseMilkCollection)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub



    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub

    Sub LoadEntrySource()
        Dim qry As String = Nothing
        qry = "select xx.ttype as Code from (select 'All' as ttype union all select 'Manual' as ttype union all select Distinct Entry_Source as ttype from TSPL_VLC_DATA_UPLOADER where Entry_Source is not null)xx"
        cmbEntrySource.DataSource = clsDBFuncationality.GetDataTable(qry)
        cmbEntrySource.ValueMember = "Code"
        cmbEntrySource.DisplayMember = "Code"
    End Sub

    Sub LoadReport()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "Detail"
        dr("Name") = "Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Summary"
        dr("Name") = "Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "MP Polling"
        dr("Name") = "MP Polling"
        dt.Rows.Add(dr)


        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"
    End Sub



    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
        Next

        gv.Columns("Account NO").IsVisible = False
        gv.Columns("Bank Branch").IsVisible = False
        gv.Columns("Bank Name").IsVisible = False
        gv.Columns("fromDate").IsVisible = False
        gv.Columns("Todate").IsVisible = False
        gv.Columns("companyADD").IsVisible = False
        gv.Columns("CompName").IsVisible = False
        gv.Columns("CompCode").IsVisible = False
        gv.Columns("HShift").IsVisible = False
        gv.Columns("ShiftMor").IsVisible = False
        gv.Columns("ShiftEve").IsVisible = False
        gv.Columns("Unit").IsVisible = False
        gv.Columns("Doc_No").IsVisible = False

        ''richa ERO/14/12/18-000443 18 Dec,2018
        gv.Columns("Buffalo_Fat_avg_per_tol").IsVisible = False
        gv.Columns("Buffalo_SNF_avg_per_tol").IsVisible = False
        gv.Columns("Cow_Fat_avg_per_tol").IsVisible = False
        gv.Columns("Cow_SNF_avg_per_tol").IsVisible = False
        gv.Columns("ChangeColorYesNo").IsVisible = False
        gv.Columns("Buff_fat%_avg").IsVisible = False
        gv.Columns("Buff_SNF%_avg").IsVisible = False
        gv.Columns("Cow_fat%_avg").IsVisible = False
        gv.Columns("Cow_SNF%_avg").IsVisible = False
        gv.Columns("TOLERANCE").IsVisible = False

        gv.Columns("Buffalo_Fat_avg_per_tol").FormatString = "{0:F2}"
        gv.Columns("Buffalo_SNF_avg_per_tol").FormatString = "{0:F2}"
        gv.Columns("Cow_Fat_avg_per_tol").FormatString = "{0:F2}"
        gv.Columns("Cow_SNF_avg_per_tol").FormatString = "{0:F2}"
        gv.Columns("Buff_fat%_avg").FormatString = "{0:F2}"
        gv.Columns("Buff_SNF%_avg").FormatString = "{0:F2}"
        gv.Columns("Cow_fat%_avg").FormatString = "{0:F2}"
        gv.Columns("Cow_SNF%_avg").FormatString = "{0:F2}"

        gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
        gv.Columns("Buffalo Milk Qty (KG)").Width = 100
        gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty"
        gv.Columns("Buffalo Milk Qty (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo FAT(%)").IsVisible = True
        gv.Columns("Buffalo FAT(%)").Width = 100
        gv.Columns("Buffalo FAT(%)").HeaderText = "Buffalo FAT(%)"
        gv.Columns("Buffalo FAT(%)").FormatString = "{0:F2}"

        gv.Columns("Buffalo SNF(%)").IsVisible = True
        gv.Columns("Buffalo SNF(%)").Width = 100
        gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"
        gv.Columns("Buffalo SNF(%)").FormatString = "{0:F2}"

        gv.Columns("Buffalo FAT (KG)").IsVisible = True
        gv.Columns("Buffalo FAT (KG)").Width = 100
        gv.Columns("Buffalo FAT (KG)").HeaderText = "Total Buffalo FAT"
        gv.Columns("Buffalo FAT (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo SNF (KG)").IsVisible = True
        gv.Columns("Buffalo SNF (KG)").Width = 100
        gv.Columns("Buffalo SNF (KG)").HeaderText = "Total Buffalo SNF"
        gv.Columns("Buffalo SNF (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo Amount").IsVisible = True
        gv.Columns("Buffalo Amount").Width = 100
        gv.Columns("Buffalo Amount").HeaderText = "Buffalo Amount"
        gv.Columns("Buffalo Amount").FormatString = "{0:F3}"

        gv.Columns("Cow Milk Qty (KG)").IsVisible = True
        gv.Columns("Cow Milk Qty (KG)").Width = 100
        gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty"
        gv.Columns("Cow Milk Qty (KG)").FormatString = "{0:F3}"

        gv.Columns("Cow FAT(%)").IsVisible = True
        gv.Columns("Cow FAT(%)").Width = 100
        gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"
        gv.Columns("Cow FAT(%)").FormatString = "{0:F2}"

        gv.Columns("Cow SNF(%)").IsVisible = True
        gv.Columns("Cow SNF(%)").Width = 100
        gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"
        gv.Columns("Cow SNF(%)").FormatString = "{0:F2}"

        gv.Columns("Cow FAT (KG)").IsVisible = True
        gv.Columns("Cow FAT (KG)").Width = 100
        gv.Columns("Cow FAT (KG)").HeaderText = "Total Cow FAT"
        gv.Columns("Cow FAT (KG)").FormatString = "{0:F3}"

        gv.Columns("Cow SNF (KG)").IsVisible = True
        gv.Columns("Cow SNF (KG)").Width = 100
        gv.Columns("Cow SNF (KG)").HeaderText = "Total Cow SNF"
        gv.Columns("Cow SNF (KG)").FormatString = "{0:F3}"

        gv.Columns("Cow Amount").IsVisible = True
        gv.Columns("Cow Amount").Width = 100
        gv.Columns("Cow Amount").HeaderText = "Cow Amount"
        gv.Columns("Cow Amount").FormatString = "{0:F3}"

        gv.Columns("Total").IsVisible = True
        gv.Columns("Total").Width = 100
        gv.Columns("Total").HeaderText = "Total"
        gv.Columns("Total").FormatString = "{0:F3}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            gv.Columns("Total Milk Qty (KG)").IsVisible = True
            gv.Columns("Total Milk Qty (KG)").Width = 100
            gv.Columns("Total Milk Qty (KG)").HeaderText = "Total Milk Qty (KG)"
            gv.Columns("Total Milk Qty (KG)").FormatString = "{0:F3}"

            gv.Columns("Total FAT(%)").IsVisible = True
            gv.Columns("Total FAT(%)").Width = 100
            gv.Columns("Total FAT(%)").HeaderText = "Total FAT(%)"
            gv.Columns("Total FAT(%)").FormatString = "{0:F2}"

            gv.Columns("Total SNF(%)").IsVisible = True
            gv.Columns("Total SNF(%)").Width = 100
            gv.Columns("Total SNF(%)").HeaderText = "Total SNF(%)"
            gv.Columns("Total SNF(%)").FormatString = "{0:F2}"

            gv.Columns("Total FAT (KG)").IsVisible = True
            gv.Columns("Total FAT (KG)").Width = 100
            gv.Columns("Total FAT (KG)").HeaderText = "Total FAT (KG)"
            gv.Columns("Total FAT (KG)").FormatString = "{0:F3}"

            gv.Columns("Total SNF (KG)").IsVisible = True
            gv.Columns("Total SNF (KG)").Width = 100
            gv.Columns("Total SNF (KG)").HeaderText = "Total SNF (KG)"
            gv.Columns("Total SNF (KG)").FormatString = "{0:F3}"

            gv.Columns("Total Amount").IsVisible = True
            gv.Columns("Total Amount").Width = 100
            gv.Columns("Total Amount").HeaderText = "Total Amount"
            gv.Columns("Total Amount").FormatString = "{0:F3}"

            Dim item111 As New GridViewSummaryItem("Total Milk Qty (KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item111)

            Dim item112 As New GridViewSummaryItem()
            item112.FormatString = "{0:F2}"
            item112.Name = "Total FAT(%)"
            item112.AggregateExpression = "sum([Total FAT (KG)])*100/sum(Total Milk Qty (KG))"
            summaryRowItem.Add(item112)

            Dim Item113 As New GridViewSummaryItem()
            Item113.FormatString = "{0:F2}"
            Item113.Name = "Total SNF(%)"
            Item113.AggregateExpression = "sum([Total SNF (KG)])*100/sum(Total Milk Qty (KG))"
            summaryRowItem.Add(Item113)

            Dim item114 As New GridViewSummaryItem("Total FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item114)

            Dim item115 As New GridViewSummaryItem("Total SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item115)

            Dim item116 As New GridViewSummaryItem("Total Amount", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item116)

        End If



        'Dim intCount As Integer = 0

        Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)

        summaryRowItem.Add(item8)
        Dim summaryItem3 As New GridViewSummaryItem()
        summaryItem3.FormatString = "{0:F2}"
        summaryItem3.Name = "Cow SNF(%)"
        summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem3)

        Dim summaryItem4 As New GridViewSummaryItem()
        summaryItem4.FormatString = "{0:F2}"
        summaryItem4.Name = "Cow FAT(%)"
        summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem4)

        Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim summaryItem5 As New GridViewSummaryItem()
        summaryItem5.FormatString = "{0:F2}"
        summaryItem5.Name = "Buffalo FAT(%)"
        summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem5)

        Dim summaryItem6 As New GridViewSummaryItem()
        summaryItem6.FormatString = "{0:F2}"
        summaryItem6.Name = "Buffalo SNF(%)"
        summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem6)

        Dim item12 As New GridViewSummaryItem("Cow Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Buffalo Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)

        Dim item14 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        cboUnit.Text = "Kg"
        txtReportType.Text = ""
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtFromShift.Enabled = val
        txtToDate.Enabled = val
        txtToShift.Enabled = val
        cboReportType.Enabled = val
        cmbEntrySource.Enabled = val
        cboUnit.Enabled = val
        txtMCC.Enabled = val
        txtVLC.Enabled = val
        txtReportType.Enabled = val

        txtMinDays.Enabled = val
    End Sub

    Private Sub DateWise()
        Try

            Dim qry As String
            qry = GetBaseQuery()

            If chkSupervisor.Checked = True Then
                Dim strSource As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+Code+']' from (select xx.ttype as Code from (select 'Manual' as ttype union all select Distinct Entry_Source as ttype from TSPL_VLC_DATA_UPLOADER where Entry_Source is not null)xx) XXX For XML Path('')),1,1,'') "))

                Dim SourceName As String = ""
                If clsCommon.myLen(strSource) > 0 Then
                    SourceName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', sum(isnull (['+Code+'],0)) as ' +'['+ Code+']' from (select xx.ttype as Code from (select 'Manual' as ttype union all select Distinct Entry_Source as ttype from TSPL_VLC_DATA_UPLOADER where Entry_Source is not null)xx) XXX For XML Path('')),1,1,'')   "))
                    SourceName = " ," + SourceName
                End If

                qry += "  select [VLC Uploader],[VLC Code],max([VLC Name]) as [VLC Name],[MCC Code],max([MCC Name]) as [MCC Name]
                         ,[Route Code],max([Route Name]) as [Route Name],max([Supervisor Name]) as [Supervisor Name]
                         ,convert(nvarchar, [Date],103) as [Date]
                         " + SourceName + " from 
                         (select CTEMP.[VLC Uploader],CTEMP.[VLC Code],max(CTEMP.[VLC Name]) as [VLC Name],CTEMP.[MCC Code],max(CTEMP.[MCC Name]) as [MCC Name]
                         ,CTEMP.[Route Code],max(CTEMP.[Route Name]) as [Route Name],max(isnull(tspl_employee_master.Emp_Name,'')) as [Supervisor Name]
                         ,convert(date, Doc_Date,103) as [Date],tttype,count(1) as [cc] 
                         from CTEMP
                         left join TSPL_VLC_Supervisor_Tagging on TSPL_VLC_Supervisor_Tagging.vlc_code=CTEMP.[VLC Code]
                         and  TSPL_VLC_Supervisor_Tagging.mcc_code=CTEMP.[MCC Code]
                         left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VLC_Supervisor_Tagging.Supervisor_code
                          WHERE 1=1 
                          group by tttype,
                         convert(date, Doc_Date,103) ,CTEMP.[VLC Uploader] ,CTEMP.[VLC Code],CTEMP.[MCC Code],TSPL_VLC_Supervisor_Tagging.Supervisor_code,CTEMP.[Route Code] 
                          )tt pivot (  sum(cc) for tttype in (" + strSource + ") ) as zpivot
                          group by [Date],[VLC Uploader],[VLC Code],[MCC Code],[Route Code]
                          order by [Date] ,[VLC Uploader] "
            Else
                qry += " select doc_date as [Date],count(1) as [Count] from CTEMP WHERE 1=1 group by doc_date order by Doc_Date  "
            End If

            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            txtReportType.Text = "DateWise"

            gv.TableElement.TableHeaderHeight = 25
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = True
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()
            If chkSupervisor.Checked = True Then
                Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable("select xx.ttype as Code from (select 'Manual' as ttype union all select Distinct Entry_Source as ttype from TSPL_VLC_DATA_UPLOADER where Entry_Source is not null)xx")

                If dtDeduction IsNot Nothing AndAlso dtDeduction.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDeduction.Rows.Count - 1
                        Dim aa = clsCommon.myCstr(dtDeduction.Rows(i).Item(0))
                        Dim item111 As New GridViewSummaryItem(aa, "{0:F0}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item111)
                        gv.Columns(i).FormatString = "{0:F0}"
                    Next
                End If
            Else
                Dim item14 As New GridViewSummaryItem("Count", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item14)
            End If




            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData()
        If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
            txtFromDate.Focus()
            clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal Then
            DateWise()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MP Polling") = CompairStringResult.Equal Then
            MPPollingFarmerCount()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Detail()
        End If
    End Sub



    Private Function GetBaseQuery()
        Dim qry As String = ""
        Dim companyADD, CompName, CompCode As String
        Try
            qry = " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            companyADD = dt1.Rows(0).Item("comp_address")

            qry = " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            CompName = dt2.Rows(0).Item("Comp_Name")


            qry = " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
            CompCode = dt5.Rows(0).Item("Comp_Code")
            Dim Shh As String = String.Empty
            Dim fromDate As Date = clsCommon.GetPrintDate(txtFromDate.Value)

            Dim Todate As Date = clsCommon.GetPrintDate(txtToDate.Value)
            Dim DiffDate As Integer = clsCommon.myCDate(Todate).Day - clsCommon.myCDate(fromDate).Day
            If DiffDate > 0 Then
                Shh = "Both"
            ElseIf DiffDate = 0 And txtFromShift.SelectedValue <> txtToShift.SelectedValue Then
                Shh = "Both"
            Else
                Shh = IIf(txtFromShift.SelectedValue = "M", "Morning", "Evening")
            End If
            ''richa ERO/14/12/18-000443 18 Dec,2018
            Dim QryUploader As String = " select  '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code, TSPL_VLC_DATA_UPLOADER.Doc_No , TSPL_Mp_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_uploader_CODE,TSPL_Mp_MASTER.MCC_NAME ,TSPL_MP_MASTER.VLC_Code ,TSPL_MP_MASTER.VLC_Name,TSPL_Mp_MASTER.MP_Code ,TSPL_VLC_DATA_UPLOADER.MP_CODE   as MP_Code_uploader ,TSPL_MP_MASTER.MP_Name ,TSPL_MP_MASTER.AccountNO ,TSPL_MP_MASTER.BankBranch ,TSPL_MP_MASTER. BankName,TSPL_VLC_DATA_UPLOADER.Doc_Date ,TSPL_VLC_DATA_UPLOADER.shift,TSPL_VLC_DATA_UPLOADER.qty as qty,TSPL_VLC_DATA_UPLOADER.fat, TSPL_VLC_DATA_UPLOADER.snf ,TSPL_VLC_DATA_UPLOADER.Amount ,TSPL_Mp_Master.UOM_Code,TSPL_MP_MASTER.Route_Code,RT.Route_Name,TSPL_MP_MASTER.TOLERANCE,TSPL_VLC_DATA_UPLOADER.Entry_Source as tttype   from  (select TSPL_MP_MASTER.TOLERANCE,TSPL_Mp_MASTER.MP_Code ," & Environment.NewLine &
            " TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,VLC_Code_VLC_Uploader,MP_Code_VLC_Uploader,Mp_Name,TSPL_MP_MASTER.AccountNO,TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,TSPL_VLC_MASTER_HEAD.MCC, MCC_NAME,UOM_Code,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code  " & Environment.NewLine &
            " from TSPL_VLC_MASTER_HEAD   left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC " & Environment.NewLine &
            " Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE =TSPL_MCC_MASTER.MCC_Code and Stocking_Unit ='Y' Left join TSPL_MP_MASTER on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code  left join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_BRANCH_MASTER.BRANCH_CODE =TSPL_MP_MASTER.BankBranch  left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_MP_MASTER.BankName) TSPL_MP_MASTER left join TSPL_VLC_DATA_UPLOADER " & Environment.NewLine &
            " on TSPL_MP_MASTER.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_MP_MASTER.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_MP_MASTER.MCC =TSPL_VLC_DATA_UPLOADER.MCC_Code  " & Environment.NewLine &
            " left join TSPL_MCC_ROUTE_MASTER RT on TSPL_MP_MASTER.Route_Code=RT.Route_Code where 2=2"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='E' then 3 else 2 end  )"
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                QryUploader += " and TSPL_VLC_DATA_UPLOADER.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                QryUploader += " and TSPL_MP_MASTER.VLC_CODE  IN (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
            End If
            If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                QryUploader += " and TSPL_MP_MASTER.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            QryUploader += "  and convert(date,File_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,File_Date,103) <=convert(date,('" + txtToDate.Value + "'),103) "

            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal Then
                If clsCommon.CompairString(cmbEntrySource.Text, "All") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbEntrySource.Text, "Manual") <> CompairStringResult.Equal Then
                    QryUploader += " and TSPL_VLC_DATA_UPLOADER.Entry_Source='" + clsCommon.myCstr(cmbEntrySource.Text) + "'"
                End If
            End If

            '' query change by Panch Raj against ticket No:KDI/21/05/18-000323-> Route code must be picked from vlc master
            Dim QryManual As String = "select  '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,VDUM.document_code as Doc_No,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, VLCM.MCC as MCC_Code,VLCM.vlc_code_vlc_uploader AS VLC_uploader_CODE," & Environment.NewLine &
            " tspl_mcc_master.mcc_name,VDUM.VLC_Code,VLCM.vlc_Name as VLC_Name,TSPL_MP_MASTER.mp_code,TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_Code_uploader,TSPL_MP_MASTER.mp_name,TSPL_MP_MASTER.AccountNO," & Environment.NewLine &
            " TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,VDUM.Document_Date as Doc_Date,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift," & Environment.NewLine &
            " VDUD.Qty,VDUD.fatper as fat,VDUD.snfper as snf,VDUD.Amount as Amount,VDUD.Unit_Code as UOM_Code,VLCM.Route_Code, " & Environment.NewLine &
            "  tspl_mcc_route_master.route_name ,TSPL_MP_MASTER.TOLERANCE,'Manual' as tttype from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD  inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code  " & Environment.NewLine &
            " left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code  left join tspl_mcc_master on tspl_mcc_master.mcc_code=VLCM.MCC " & Environment.NewLine &
            " left join tspl_mp_master on tspl_mp_master.mp_code=VDUD.farmer_code left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=VLCM.Route_Code " & Environment.NewLine &
            " where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='MORNING' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='EVENING' then 3 else 2 end  )"
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                QryManual += " and VLCM.MCC  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                QryManual += " and VLCM.VLC_Code IN (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
            End If
            If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                QryManual += " and VLCM.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If


            ' Change By Prabhakar (Live issue in EROD) : replace "<" to "<="  for [Cow FAT(%)] and [Cow SNF(%)]    
            qry = "with CTEMP AS  (" & Environment.NewLine &
            "select ROW_NUMBER() OVER(ORDER BY Doc_Date) as SNo,'" + txtFromShift.Text + "' as ShiftMor,'" + txtToShift.Text + "' as ShiftEve,'" + cboUnit.Text + "' as Unit,'" & Shh & "'  as HShift ,'" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," & Environment.NewLine &
            " (Mcc_Code_VLC_Uploader) as [MCC Uploader], MCC_Code as [MCC Code] ,(MCC_NAME) as [MCC Name],(VLC_uploader_CODE) as [VLC Uploader],VLC_CODE as [VLC Code],(Route_Code) as [Route Code],(Route_Name) as [Route Name],(VLC_Name )as [VLC Name],MP_Code_uploader as [MP Uploader],MP_Code as [MP Code] ,(MP_Name) as [MP Name]," & Environment.NewLine &
            " (AccountNO) as [Account NO],(BankBranch) as [Bank Branch],(BankName) as [Bank Name] , shift,TOLERANCE,tttype, " & Environment.NewLine &
            " isnull([Buffalo Milk Qty (KG)],0) as [Buffalo Milk Qty (KG)] ," & Environment.NewLine &
            " isnull( [Buffalo FAT(%)],0) as [Buffalo FAT(%)],  " & Environment.NewLine &
            "  isnull([Buffalo SNF(%)],0) as [Buffalo SNF(%)] , " & Environment.NewLine &
            " case when isnull(TOLERANCE,0 )<>0 then  cast((((((sum([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end))*TOLERANCE )/100)+(((sum([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Buffalo FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end)) as decimal(18,2)) else 0 end as [Buff_fat%_avg], " & Environment.NewLine &
            " case when isnull(TOLERANCE,0 )<>0 then  cast((((((sum([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end))*TOLERANCE )/100)+(((sum([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Buffalo SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end)) as decimal(18,2)) else 0 end as [Buff_SNF%_avg], " & Environment.NewLine &
            "  isnull([Buffalo FAT (KG)],0) as [Buffalo FAT (KG)]," & Environment.NewLine &
            " isnull ([Buffalo SNF (KG)],0) as [Buffalo SNF (KG)] ," & Environment.NewLine &
            "  isnull([Buffalo Amount],0 ) as [Buffalo Amount], " & Environment.NewLine &
            " isnull([Cow Milk Qty (KG)],0) as [Cow Milk Qty (KG)], " & Environment.NewLine &
            " isnull(  [Cow FAT(%)],0) as [Cow FAT(%)], " & Environment.NewLine &
            " isnull( [Cow SNF(%)],0) as [Cow SNF(%)] , " & Environment.NewLine &
            " case when isnull(TOLERANCE,0 )<>0 then  cast((((((sum([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end))*TOLERANCE )/100)+(((sum([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Cow FAT(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end)) as decimal(18,2)) else 0 end as [Cow_fat%_avg], " & Environment.NewLine &
            "  case when isnull(TOLERANCE,0 )<>0 then  cast((((((sum([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end))*TOLERANCE )/100)+(((sum([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )) -(last_value([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding )))/(case when (count([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))=1 then 1 else (count([Cow SNF(%)]) over (partition by mp_code order by Doc_Date asc rows  3 preceding ))-1 end)) as decimal(18,2)) else 0 end as [Cow_SNF%_avg], " & Environment.NewLine &
            "  isnull([cow FAT (KG)],0) as [Cow FAT (KG)]," & Environment.NewLine &
            " isnull([cow SNF (KG)],0) as [Cow SNF (KG)] , " & Environment.NewLine &
            " isnull([Cow Amount],0) as [Cow Amount],Doc_No,Doc_date,isnull([Cow Amount],0)+isnull([Buffalo Amount],0 ) as Total " & Environment.NewLine &
            "from (" & Environment.NewLine &
            "select qq.*,Case When qq.FAT <= 5 Then  qq.qty    Else 0 End [Cow Milk Qty (KG)] ,Case When qq.FAT <= 5 Then qq.FAT Else 0 End [Cow FAT(%)], " & Environment.NewLine &
            " Case When qq.FAT <= 5 Then qq.SNF Else 0 End [Cow SNF(%)],Case When qq.FAT  <= 5 Then qq.NewFAT_KG  Else 0 End [Cow FAT (KG)]," & Environment.NewLine &
            "   Case When qq.FAT <= 5 Then qq.NewSNF_KG Else 0 End [Cow SNF (KG)], Case When qq.FAT <= 5 Then qq.Amount Else 0 End [Cow Amount]," & Environment.NewLine &
            "  Case When qq.FAT > 5 Then qq.qty  Else 0 End [Buffalo Milk Qty (KG)],Case When qq.FAT > 5 Then qq.FAT Else 0 End [Buffalo FAT(%)],  " & Environment.NewLine &
            " Case When qq.FAT > 5 Then qq.SNF Else 0 End [Buffalo SNF(%)], Case When qq.FAT > 5 Then qq.NewFAT_KG Else 0 End [Buffalo FAT (KG)],  " & Environment.NewLine &
            " Case When qq.FAT > 5 Then qq.NewSNF_KG Else 0 End [Buffalo SNF (KG)],Case When qq.FAT > 5 Then qq.Amount  Else 0 End [Buffalo Amount] from ( " & Environment.NewLine &
            " select pp.tttype,pp.TOLERANCE,pp.doc_no, pp.Mcc_Code_VLC_Uploader ,pp.VLC_uploader_CODE ,pp.MCC_Code,pp.VLC_CODE,pp.VLC_Name,pp.MCC_NAME, pp.MP_CODE   as MP_Code,pp.MP_Code_uploader  ," & Environment.NewLine &
            " pp.MP_Name ,pp.AccountNO ,pp.BankBranch ,pp. BankName,pp.Doc_Date ,pp.shift,pp.NewQty  as qty,pp.fat, pp.snf,pp.NewFAT_KG ,pp.NewSNF_KG ,pp.NewAmount as Amount ,pp.UOM_Code,Route_Code,Route_Name  " & Environment.NewLine &
            "  from ( select xx.*,xx.qty*CF as NewQty,xx.Amount as NewAmount,fat_KG *Cf as NewFAT_KG,snf_KG *cf as NewSNF_KG  from (" & Environment.NewLine &
            " select *,(qty *fat) /100 as fat_KG,(qty *snf) /100 as snf_KG from ("
            '& Environment.NewLine &
            '" " & QryUploader & " "

            If Not (clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal) OrElse ((clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(cmbEntrySource.Text, "All") = CompairStringResult.Equal) Then
                qry += QryUploader & Environment.NewLine & " union all " & Environment.NewLine &
                " " & QryManual & " "
            ElseIf ((clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(cmbEntrySource.Text, "Manual") = CompairStringResult.Equal) Then
                qry += QryManual & Environment.NewLine & ""
            Else
                qry += QryUploader & Environment.NewLine & ""
            End If

            qry += " ) as pp )xx" & Environment.NewLine &
              " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "
            ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
            If ItemStructureMandatoryOnWeightConversion = True Then
                qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
            Else
                qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
            End If

            qry += " as pp  ) as qq   ) as final) "

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return qry
    End Function

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
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMPWiseMilkCollectionATPoolingPoint & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        'If Not (clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal) Then
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        'End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub



    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal Then
            PageSetupReport_ID = PageSetupReport_ID + "S"
            If chkSupervisor.Checked = True Then
                PageSetupReport_ID = PageSetupReport_ID + "WSUP"
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MP Polling") = CompairStringResult.Equal Then
            PageSetupReport_ID = PageSetupReport_ID + "P"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            PageSetupReport_ID = PageSetupReport_ID + "D"
        End If
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnReferesh = False
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptMPWiseMilkCollectionAtPoolingPoint3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        DisplayAverageFatSNFMPWise = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DisplayAverageFatSNFMPWise, clsFixedParameterCode.DisplayAverageFatSNFMPWise, Nothing)) = "1", True, False))
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadReport()
        LoadShiftFrom()
        LoadShiftTo()
        LoadEntrySource()
        Reset()
    End Sub

    Private Sub RptMPWiseMilkCollectionAtPoolingPoint3_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub


    Private Sub gv_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv.RowFormatting
        Try
            If Not (clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal) Then
                If DisplayAverageFatSNFMPWise = True Then
                    If clsCommon.CompairString(e.RowElement.RowInfo.Cells("ChangeColorYesNo").Value, "Y") = CompairStringResult.Equal Then
                        e.RowElement.DrawFill = True
                        e.RowElement.GradientStyle = GradientStyles.Solid
                        e.RowElement.ForeColor = Color.Black
                        e.RowElement.BackColor = Color.LightGreen
                    Else
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                        e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub chkSummary_CheckedChanged(sender As Object, e As EventArgs)
        Reset()
    End Sub



    Private Sub VLCWise()
        Try

            Dim qry As String
            qry = GetBaseQuery()

            qry += " select doc_date as [Date],[VLC Uploader],[VLC Name],[MCC Code],[MCC Name],count(1) as [Count] from CTEMP where  convert(date, doc_date,103)=convert(date, '" + clsCommon.myCstr(gv.CurrentRow.Cells("Date").Value) + "',103) group by doc_date,[VLC Uploader],[VLC Name],[MCC Code],[MCC Name] order by doc_date,[MCC Code],[VLC Uploader]  "
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            txtReportType.Text = "VLCWise"
            gv.TableElement.TableHeaderHeight = 25
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = True
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item14 As New GridViewSummaryItem("Count", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub ShiftWise()
        Try

            Dim qry As String
            qry = GetBaseQuery()

            qry += " select ROW_NUMBER() OVER(ORDER BY [VLC Uploader]) as SoNo,CTEMP.*,0.00 as Buffalo_Fat_avg_per_tol,0.00 as Buffalo_SNF_avg_per_tol,0.00 AS Cow_Fat_avg_per_tol,0.00 AS Cow_SNF_avg_per_tol, '' as ChangeColorYesNo from CTEMP where convert(date, doc_date,103)=convert(date, '" + clsCommon.myCstr(gv.CurrentRow.Cells("Date").Value) + "',103) and [VLC Uploader]='" + clsCommon.myCstr(gv.CurrentRow.Cells("VLC Uploader").Value) + "' and [MCC Code]='" + clsCommon.myCstr(gv.CurrentRow.Cells("MCC Code").Value) + "' order by CTEMP.doc_date,CTEMP.[MCC Code],CTEMP.[VLC Uploader]  "
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            txtReportType.Text = "ShiftWise"
            FormatGrid()
            gv.Columns("SNo").IsVisible = False
            gv.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal Then
                If gv.CurrentRow.Index >= 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(txtReportType.Text), "DateWise") = CompairStringResult.Equal Then
                        VLCWise()
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(txtReportType.Text), "VLCWise") = CompairStringResult.Equal Then
                        ShiftWise()
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MP Polling") = CompairStringResult.Equal Then
                If gv.CurrentRow.Index >= 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(txtReportType.Text), "MP Polling Count") = CompairStringResult.Equal Then
                        MPDetail()
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(txtReportType.Text), "MP Detail") = CompairStringResult.Equal Then
                        Detail()
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("Pooling@M", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        txtVLC.arrValueMember = Nothing

    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " SELECT  DISTINCT RT.Route_Code as Code,RT.Route_Name as Name FROM TSPL_MCC_ROUTE_MASTER RT LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON RT.Route_Code=VLC.Route_Code where 2=2 "
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            qry += " and VLC.VLC_Code  IN (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
        End If
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("Pooling@R", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [DCS Code],VLC.VLC_Name as [DCS Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name " &
                  " from TSPL_VLC_MASTER_HEAD    VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code where 2=2 "
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            qry += " and VLC.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
        End If
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("Pooling@V", qry, "DCS Code", "DCS Name", txtVLC.arrValueMember, txtVLC.arrDispalyMember)

    End Sub

    Private Sub cboReportType_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboReportType.Validating
        lblConvRate.Visible = False
        txtMinDays.Visible = False
        chkSupervisor.Visible = False
        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MP Polling") = CompairStringResult.Equal Then
            lblConvRate.Visible = True
            txtMinDays.Visible = True
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Summary") = CompairStringResult.Equal Then
            chkSupervisor.Visible = True
        End If
    End Sub



    Private Sub MPPollingFarmerCount()
        Try
            Dim ts As TimeSpan = txtToDate.Value.Subtract(txtFromDate.Value)
            If ts.Days < txtMinDays.Value Then
                Throw New Exception("Min Days Should be less then Date Range Days")
            End If
            Dim qry As String
            qry = GetBaseQuery()

            qry += " select [MCC Code],max([MCC Name]) as [MCC Name],[VLC Code],max([VLC Uploader]) as [VLC Uploader],max([VLC Name]) as [VLC Name],count([MP Code]) as NoOFMP from (
select [MCC Code],max([MCC Name]) as [MCC Name],[VLC Code],max([VLC Uploader]) as [VLC Uploader],max([VLC Name]) as [VLC Name],[MP Code],max([MP Name]) as [MP Name],COUNT(Doc_Date) as NoOFDays from(
select [MCC Code],max([MCC Name]) as [MCC Name],[VLC Code],max([VLC Uploader]) as [VLC Uploader],max([VLC Name]) as [VLC Name],[MP Code],max([MP Name]) as [MP Name],Doc_Date from (
 select [MCC Code],[MCC Name],[VLC Code],[VLC Uploader],[VLC Name],[MP Code],[MP Name],[MP Uploader],convert(date,Doc_Date,103) as Doc_Date ,shift,Doc_No,Total from CTEMP
)xx group by [MCC Code],[VLC Code],[MP Code],Doc_Date
)xxx group by [MCC Code],[VLC Code],[MP Code] having count(Doc_Date)>" + clsCommon.myCstr(txtMinDays.Value) + "
)xxxx group by [MCC Code],[VLC Code]  "
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            txtReportType.Text = "MP Polling Count"

            gv.TableElement.TableHeaderHeight = 25
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = True
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item14 As New GridViewSummaryItem("NoOFMP", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MPDetail()
        Try
            Dim ts As TimeSpan = txtToDate.Value.Subtract(txtFromDate.Value)
            If ts.Days < txtMinDays.Value Then
                Throw New Exception("Min Days Should be less then Date Range Days")
            End If
            Dim qry As String
            qry = GetBaseQuery()

            qry += "select [MCC Code],max([MCC Name]) as [MCC Name],[VLC Code],max([VLC Uploader]) as [VLC Uploader],max([VLC Name]) as [VLC Name],max([MP Uploader]) as [MP Uploader],[MP Code],max([MP Name]) as [MP Name],COUNT(Doc_Date) as NoOFDays from(
select [MCC Code],max([MCC Name]) as [MCC Name],[VLC Code],max([VLC Uploader]) as [VLC Uploader],max([VLC Name]) as [VLC Name],[MP Code],max([MP Uploader]) as [MP Uploader],max([MP Name]) as [MP Name],Doc_Date from (
 select [MCC Code],[MCC Name],[VLC Code],[VLC Uploader],[VLC Name],[MP Code],[MP Name],[MP Uploader],convert(date,Doc_Date,103) as Doc_Date ,shift,Doc_No,Total from CTEMP
where [VLC Code]='" + clsCommon.myCstr(gv.CurrentRow.Cells("VLC Code").Value) + "'
)xx group by [MCC Code],[VLC Code],[MP Code],Doc_Date
)xxx group by [MCC Code],[VLC Code],[MP Code] having count(Doc_Date)>" + clsCommon.myCstr(txtMinDays.Value) + ""
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            txtReportType.Text = "MP Detail"

            gv.TableElement.TableHeaderHeight = 25
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = True
            Next
            gv.Columns("MP Code").IsVisible = False
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item14 As New GridViewSummaryItem("NoOFDays", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Detail()
        Try
            Dim qry As String
            qry = GetBaseQuery()

            qry += " select CTEMP.*
                     ,TSPL_VLC_Supervisor_Tagging.supervisor_code as [Supervisor Code]
                     ,tspl_employee_master.Emp_Name as [Supervisor Name]
                     ,0.00 as Buffalo_Fat_avg_per_tol,0.00 as Buffalo_SNF_avg_per_tol,0.00 AS Cow_Fat_avg_per_tol,0.00 AS Cow_SNF_avg_per_tol, '' as ChangeColorYesNo
                     ,CTEMP.[Cow Milk Qty (KG)] +CTEMP.[Buffalo Milk Qty (KG)] as [Total Milk Qty (KG)]
                      ,CASE WHEN (CTEMP.[Cow Milk Qty (KG)] +CTEMP.[Buffalo Milk Qty (KG)])>0 THEN 
                      ((CTEMP.[Cow FAT (KG)]+CTEMP.[Buffalo FAT (KG)])*100)/(CTEMP.[Cow Milk Qty (KG)] +CTEMP.[Buffalo Milk Qty (KG)])
                       ELSE 0 END as [Total FAT(%)]
                    ,CASE WHEN (CTEMP.[Cow Milk Qty (KG)] +CTEMP.[Buffalo Milk Qty (KG)])>0 THEN 
                      ((CTEMP.[Cow SNF (KG)]+CTEMP.[Buffalo SNF (KG)])*100)/(CTEMP.[Cow Milk Qty (KG)] +CTEMP.[Buffalo Milk Qty (KG)])
                      ELSE 0 END as [Total SNF(%)]
                      ,CTEMP.[Cow FAT (KG)]+CTEMP.[Buffalo FAT (KG)] as [Total FAT (KG)]
                      ,CTEMP.[Cow SNF (KG)]+CTEMP.[Buffalo SNF (KG)] as [Total SNF (KG)]
                      ,CTEMP.[Cow Amount]+CTEMP.[Buffalo Amount] as [Total Amount]
                      from CTEMP 					  
                      left join TSPL_VLC_Supervisor_Tagging on TSPL_VLC_Supervisor_Tagging.vlc_code=CTEMP.[VLC Code]
					  and  TSPL_VLC_Supervisor_Tagging.mcc_code=CTEMP.[MCC Code]
					  left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VLC_Supervisor_Tagging.Supervisor_code "
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MP Polling") = CompairStringResult.Equal Then
                qry += " where [MP Code]='" + clsCommon.myCstr(gv.CurrentRow.Cells("MP Code").Value) + "'"
            End If
            qry += "order by CTEMP .Doc_Date,CTEMP.SNo  "

            dt = clsDBFuncationality.GetDataTable(qry)
            If DisplayAverageFatSNFMPWise = True Then
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    dt.Rows(0)("Buffalo_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(0)("Buff_fat%_avg"))
                    dt.Rows(0)("Buffalo_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(0)("Buff_SNF%_avg"))
                    dt.Rows(0)("Cow_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(0)("Cow_fat%_avg"))
                    dt.Rows(0)("Cow_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(0)("Cow_SNF%_avg"))

                    For i As Integer = 1 To dt.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("MP Code")), clsCommon.myCstr(dt.Rows(i - 1)("MP Code"))) = CompairStringResult.Equal Then
                            dt.Rows(i)("Buffalo_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i - 1)("Buff_fat%_avg"))
                            dt.Rows(i)("Buffalo_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i - 1)("Buff_SNF%_avg"))
                            dt.Rows(i)("Cow_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i - 1)("Cow_fat%_avg"))
                            dt.Rows(i)("Cow_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i - 1)("Cow_SNF%_avg"))
                        Else
                            dt.Rows(i)("Buffalo_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i)("Buff_fat%_avg"))
                            dt.Rows(i)("Buffalo_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i)("Buff_SNF%_avg"))
                            dt.Rows(i)("Cow_Fat_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i)("Cow_fat%_avg"))
                            dt.Rows(i)("Cow_SNF_avg_per_tol") = clsCommon.myCdbl(dt.Rows(i)("Cow_SNF%_avg"))
                        End If
                        If clsCommon.myCdbl(dt.Rows(i)("TOLERANCE")) > 0 AndAlso ((clsCommon.myCdbl(dt.Rows(i)("Buffalo FAT(%)")) > clsCommon.myCdbl(dt.Rows(i)("Buffalo_Fat_avg_per_tol"))) Or (clsCommon.myCdbl(dt.Rows(i)("Buffalo SNF(%)")) > clsCommon.myCdbl(dt.Rows(i)("Buffalo_SNF_avg_per_tol"))) Or (clsCommon.myCdbl(dt.Rows(i)("Cow FAT(%)")) > clsCommon.myCdbl(dt.Rows(i)("Cow_Fat_avg_per_tol"))) Or (clsCommon.myCdbl(dt.Rows(i)("Cow SNF(%)")) > clsCommon.myCdbl(dt.Rows(i)("Cow_SNF_avg_per_tol")))) Then
                            dt.Rows(i)("ChangeColorYesNo") = "Y"
                        Else
                            dt.Rows(i)("ChangeColorYesNo") = "N"
                        End If
                    Next
                End If
            End If


            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            If btnReferesh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMPWiseMilkCollectionAtPoolingPoint", "MP Wise Milk Collection Pooling Point", "")
                frmCRV = Nothing
            End If
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkSupervisor_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSupervisor.ToggleStateChanged
        If chkSupervisor.Checked = True Then
            cmbEntrySource.Text = "All"
            cmbEntrySource.Enabled = False
        Else
            cmbEntrySource.Enabled = True
        End If
    End Sub
End Class
