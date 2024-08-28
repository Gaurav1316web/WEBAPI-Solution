Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptDailyWiseMilkCost
    Inherits FrmMainTranScreen

#Region "Variables"
    Public insideloaddata As Boolean = False
#End Region

    Private Sub RptDailyWiseMilkCost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadMonth()
        loadYear()
        loadType()
        loadReportType()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDailyWiseMilkCost)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        radbtnBulkExp.Visible = MyBase.isExport

    End Sub

    Private Sub loadYear()
        Dim i As Integer = 0
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        For i = DateTime.Now.Year - 20 To DateTime.Now.Year
            dr = dt.NewRow()
            dr("Code") = i.ToString()
            dr("Name") = i.ToString()
            dt.Rows.Add(dr)
        Next

        txtYear.DataSource = dt
        txtYear.ValueMember = "Code"
        txtYear.DisplayMember = "Name"
        txtYear.SelectedValue = ""

    End Sub

    Private Sub loadMonth()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "01"
        dr("Name") = "January"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "02"
        dr("Name") = "February"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "03"
        dr("Name") = "March"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "04"
        dr("Name") = "April"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "05"
        dr("Name") = "May"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "06"
        dr("Name") = "June"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "07"
        dr("Name") = "July"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "08"
        dr("Name") = "August"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "09"
        dr("Name") = "September"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "10"
        dr("Name") = "October"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "11"
        dr("Name") = "November"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "12"
        dr("Name") = "December"
        dt.Rows.Add(dr)

        txtmonth.DataSource = dt
        txtmonth.ValueMember = "Code"
        txtmonth.DisplayMember = "Name"
        txtmonth.SelectedValue = ""

    End Sub

    Private Sub loadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "Select"
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job-Work"
        dt.Rows.Add(dr)

        txttype.DataSource = dt
        txttype.ValueMember = "Code"
        txttype.DisplayMember = "Name"
        txttype.SelectedValue = "Select"

    End Sub

    Private Sub loadReportType()
        insideloaddata = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Monthly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yearly"
        dt.Rows.Add(dr)

        txtReportType.DataSource = dt
        txtReportType.ValueMember = "Code"
        txtReportType.DisplayMember = "Name"
        txtReportType.SelectedValue = "M"
        insideloaddata = False
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtn_summary.IsChecked Then
            VarID += "_S"
        ElseIf rbnt_detail.IsChecked Then
            VarID += "_D"
        End If
        gv.VarID = VarID
    End Sub

    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try
            If clsCommon.myLen(txtmonth.SelectedValue) <= 0 AndAlso clsCommon.CompairString(txtReportType.SelectedValue, "M") = CompairStringResult.Equal Then
                txtmonth.Focus()
                Throw New Exception("Please select month.")
            End If

            If clsCommon.myLen(txtYear.SelectedValue) <= 0 Then
                txtYear.Focus()
                Throw New Exception("Please select year.")
            End If

            If clsCommon.CompairString(clsCommon.myCstr(txttype.SelectedValue), "Select") = CompairStringResult.Equal Then
                txttype.Focus()
                Throw New Exception("Please select type.")
            End If

            Dim FinalQuery As String = Nothing
            Dim Pivot1Col As String = Nothing
            Dim Pivot1ColName As String = Nothing
            Dim Pivot11ColName As String = Nothing
            Dim PivotShowColName1 As String = Nothing
            Dim PivotShowColName2 As String = Nothing
            Dim PivotSummaryShowColName1 As String = Nothing
            Dim PivotSummaryShowColName2 As String = Nothing
            Dim Pivot2Col As String = Nothing
            Dim Pivot2ColName As String = Nothing
            Dim Pivot22ColName As String = Nothing
            Dim Pivot3Col As String = Nothing
            Dim Pivot3ColName As String = Nothing
            Dim Pivot33ColName As String = Nothing
            Dim Pivot4Col As String = Nothing
            Dim Pivot4ColName As String = Nothing
            Dim Pivot44ColName As String = Nothing
            Dim TotalQty1 As String = Nothing
            Dim AvgCost1 As String = Nothing
            Dim TotalQty2 As String = Nothing
            Dim AvgCost2 As String = Nothing
            Dim TotalQty As String = Nothing
            Dim AvgCost As String = Nothing

            Pivot1Col = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + TSPL_MCC_MASTER.MCC_Code+'Qty]'  from TSPL_MCC_MASTER for xml path('')  ),1,1,'')"))
            Pivot1ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + TSPL_MCC_MASTER.MCC_Code+'Qty]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))
            Pivot11ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',0 as [' + TSPL_MCC_MASTER.MCC_Code+'Qty]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))

            Pivot2Col = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + TSPL_MCC_MASTER.MCC_Code+'Cost]'  from TSPL_MCC_MASTER for xml path('')  ),1,1,'')"))
            Pivot2ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + TSPL_MCC_MASTER.MCC_Code+'Cost]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))
            Pivot22ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',0 as [' + TSPL_MCC_MASTER.MCC_Code+'Cost]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))

            Pivot3Col = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,1,'')"))
            Pivot3ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))
            Pivot33ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',0 as [' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))

            Pivot4Col = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Cost]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,1,'')"))
            Pivot4ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',[' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Cost]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))
            Pivot44ColName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',0 as [' + RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Cost]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))

            PivotShowColName1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',sum(isnull(['+ TSPL_MCC_MASTER.MCC_Code+'Qty],0)) as ['+ TSPL_MCC_MASTER.MCC_NAME+' Qty],case when sum(isnull(['+TSPL_MCC_MASTER.MCC_Code+'Qty],0))=0 then 0 else sum(isnull(['+ TSPL_MCC_MASTER.MCC_Code+'Cost],0))/sum(isnull(['+TSPL_MCC_MASTER.MCC_Code+'Qty],0)) end as ['+ TSPL_MCC_MASTER.MCC_Name+' Cost]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))
            PivotShowColName2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty],0)) as ['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Qty],case when sum(isnull(['+RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty],0))=0 then 0 else sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Cost],0))/sum(isnull(['+RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty],0)) end as ['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Cost]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))

            PivotSummaryShowColName1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',sum(isnull(['+ TSPL_MCC_MASTER.MCC_NAME+' Qty],0)) as ['+ TSPL_MCC_MASTER.MCC_NAME+' Qty],sum(isnull(['+ TSPL_MCC_MASTER.MCC_NAME+' Cost],0)) as ['+ TSPL_MCC_MASTER.MCC_Name+' Cost]'  from TSPL_MCC_MASTER for xml path('')  ),1,0,'')"))
            PivotSummaryShowColName2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT ',sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Qty],0)) as ['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Qty],sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Cost],0)) as ['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+' Cost]'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))

            TotalQty1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT '+sum(isnull(['+ TSPL_MCC_MASTER.MCC_Code+'Qty],0))'  from TSPL_MCC_MASTER for xml path('')  ),1,1,'')"))
            AvgCost1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT '+sum(isnull(['+ TSPL_MCC_MASTER.MCC_Code+'Cost],0))'  from TSPL_MCC_MASTER for xml path('')  ),1,1,'')"))

            TotalQty2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT '+sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Qty],0))'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))
            AvgCost2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stuff((select DISTINCT '+sum(isnull(['+ RTRIM(TSPL_MILK_GRADE_MASTER.GRADE_TYPE)+'Cost],0))'  from TSPL_MILK_GRADE_MASTER for xml path('')  ),1,0,'')"))

            TotalQty = clsCommon.myCstr(TotalQty1 + TotalQty2)
            AvgCost = clsCommon.myCstr(AvgCost1 + AvgCost2)

            'Sanjay Ticket no- BHA/21/11/18-000690
            If clsCommon.myLen(Pivot3Col) = 0 OrElse clsCommon.myLen(Pivot4Col) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            'Sanjay Ticket no- BHA/21/11/18-000690

            Dim qry As String = Nothing
            Dim qry1 As String = Nothing
            Dim whrcls As String = Nothing
            Dim whrcls1 As String = Nothing

            If clsCommon.CompairString(txtReportType.SelectedValue, "Y") = CompairStringResult.Equal Then
                whrcls = " DATEPART(year,Punching_Date)=" + txtYear.SelectedValue + ""
                whrcls1 = " where 2=2 and DATEPART(year,convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103))=" + txtYear.SelectedValue + " and Tspl_Gate_Entry_Details.Doc_Type='BulkProc' "
            Else
                whrcls = " DATEPART(year,Punching_Date)=" + txtYear.SelectedValue + " and DATEPART(month,Punching_Date)=" + txtmonth.SelectedValue + " "
                whrcls1 = " where 2=2 and DATEPART(year,convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103))=" + txtYear.SelectedValue + " and DATEPART(month,convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103))=" + txtmonth.SelectedValue + "  and Tspl_Gate_Entry_Details.Doc_Type='BulkProc' "
            End If
            
            If clsCommon.CompairString(txttype.SelectedValue, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(txttype.SelectedValue, "J") = CompairStringResult.Equal Then
                whrcls1 += " and isnull(Tspl_Gate_Entry_Details.Gate_Entry_Type,'')='" + clsCommon.myCstr(txttype.SelectedValue) + "'"
            Else
                whrcls1 += " and isnull(Tspl_Gate_Entry_Details.Gate_Entry_Type,'') not in ('P','J')"
            End If
            qry = clsInventoryMovement.GetBaseQuery(whrcls)
            qry1 = RptBulkMilkRegister.GetBaseQuery(whrcls1)
            
            FinalQuery = "select DATENAME(MONTH,Punching_Date) as [Month],convert(varchar,DATEPART(YEAR,Punching_Date)) as [Year],Punching_Date" + PivotShowColName1 + PivotShowColName2 + ",(" + TotalQty + ") as TotalQty,case when (" + TotalQty + ")=0 then 0 else ((" + AvgCost + ")/(" + TotalQty + ")) end as [Average Cost/Kg] from " & _
                         " (select * from" & _
                         "  (SELECT Punching_Date" + Pivot1ColName + Pivot2ColName + Pivot33ColName + Pivot44ColName + " FROM " & _
                         " (select stuti.Punching_Date,stuti.LocQty,stuti.LocCost,avg(stuti.Basic_Cost) as basic_cost,sum(stuti.Stock_Qty) as stock_qty,sum(stuti.stockamt) as stockamt from (select Punching_Date,LocQty,LocCost,Basic_Cost,case when InOut='I' then Stock_Qty*(1) else Stock_Qty*(-1) end as Stock_Qty,(Stock_Qty*Basic_Cost) as StockAmt from (" + qry + ")ab where ab.Location_Code in (select mcc_code from TSPL_MCC_MASTER) "
            If chk_fatsnf.Checked Then
                FinalQuery += "  and (coalesce(ab.Fat_Per,0)<>0 or coalesce(ab.SNF_Per,0)<>0)"
            Else
                FinalQuery += "  and (coalesce(ab.Fat_Per,0)=0 or coalesce(ab.SNF_Per,0)=0)"
            End If

            FinalQuery += " ) stuti group by stuti.Punching_Date,stuti.LocQty,stuti.LocCost) at PIVOT ( sum(stock_qty) FOR LocQty IN (" + Pivot1Col + ")) AS PT1  PIVOT ( sum(StockAmt) FOR LocCost IN (" + Pivot2Col + ")) AS PT2)tp" & _
                         " UNION ALL" & _
                         " SELECT Punching_Date" + Pivot11ColName + Pivot22ColName + Pivot3ColName + Pivot4ColName + " FROM " & _
                         " (select convert(date,ab1.[Gate Entry Date],103) as Punching_Date,'' as LocQty,'' as LocCost,0 as Basic_Cost,0 as Stock_Qty,0 as StockAmt,(rtrim(ab1.[Grade Type])+'Qty') as GradeQty,(rtrim(ab1.[Grade Type])+'Cost') as GradeCost, ab1.[Net Weight] as NetWeight,ab1.[Basic Rate] as BasicRate,(ab1.[Net Weight]*ab1.[Basic Rate]) as BasicAmt from (" + qry1 + ")ab1" & _
                         " )at PIVOT ( sum(NetWeight) FOR GradeQty IN (" + Pivot3Col + ")) AS PT3 PIVOT ( sum(BasicAmt) FOR GradeCost IN (" + Pivot4Col + ")) AS PT4" & _
                         " )abc  group by Punching_Date "



            If rbnt_detail.IsChecked Then
                FinalQuery += " order by Punching_Date"
            Else
                FinalQuery = "select convert(varchar,[Month]) as [Month],convert(varchar,[Year]) as [Year] " + PivotSummaryShowColName1 + PivotSummaryShowColName2 + ",sum(TotalQty) as TotalQty,sum([Average Cost/Kg]) as [Average Cost/Kg] from (" + FinalQuery + ")at group by [Month],[Year] ORDER BY [Month],[Year]"
            End If
            '' bulk export
            If BulkExport = 1 Then
                transportSql.BulkExport("Daily_Wise_Milk_Cost", FinalQuery, "", "csv")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Daily_Wise_Milk_Cost", FinalQuery, "", "xls")
                Exit Sub
            End If

            Dim dt As New DataTable()
            dt = clsDBFuncationality.GetDataTable(FinalQuery)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.EnableFiltering = True
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).Width = 100
                If TypeOf gv.Columns(ii) Is GridViewDateTimeColumn Then
                    gv.Columns(ii).FormatString = "{0:d}"
                ElseIf TypeOf gv.Columns(ii) Is GridViewDecimalColumn Then
                    gv.Columns(ii).FormatString = "{0:n4}"
                End If
            Next

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim ReportID As String = clsUserMgtCode.rptDailyWiseMilkCost
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = clsUserMgtCode.rptDailyWiseMilkCost
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)  'CSV
        LoadData(1)
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs)  'excel
        LoadData(2)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rbtn_summary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtn_summary.ToggleStateChanged, rbnt_detail.ToggleStateChanged
        If rbtn_summary.IsChecked Then
            'txtmonth.Enabled = False
        Else
            'txtmonth.Enabled = True
        End If
    End Sub

    Private Sub txtReportType_SelectedValueChanged(sender As Object, e As EventArgs) Handles txtReportType.SelectedValueChanged
        If Not insideloaddata Then
            If clsCommon.CompairString(txtReportType.SelectedValue, "Y") = CompairStringResult.Equal Then
                txtmonth.Enabled = False
            Else
                txtmonth.Enabled = True
            End If
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Month : " + txtmonth.SelectedItem.ToString() + " Year : " + txtYear.SelectedItem.ToString()))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDailyWiseMilkCost & "'"))

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
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Daily wise Milk Cost Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
