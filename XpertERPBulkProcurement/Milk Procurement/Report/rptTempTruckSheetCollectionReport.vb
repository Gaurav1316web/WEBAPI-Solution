Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class rptTempTruckSheetCollectionReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim whre As String = ""
            qry = " DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((  SELECT  ','  +  QUOTENAME( convert (varchar, thedate,103) )  as thedate FROM  ExplodeDates( convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) ,convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) order by convert(date,thedate,103)  asc  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
            Dim strAllColumnWithoutMax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            qry = " DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((  SELECT  ',' +'isnull ( ' +  QUOTENAME( convert (varchar, thedate,103)) + ',0) as ' + QUOTENAME( convert (varchar, thedate,103))  as thedate FROM  ExplodeDates( convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) ,convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) order by convert (date,thedate,103) asc  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
            Dim strAllColumnWithMax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            Dim strAllcolumnWithTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((  SELECT  '+' +'isnull ( ' +  QUOTENAME( convert (varchar, thedate,103)) + ',0)'   as thedate FROM  ExplodeDates( convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103) ,convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103)) order by convert (date,thedate,103) asc  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))

            qry = " select ROUTE_CODE,ROUTE_NAME, " + strAllColumnWithMax + " , CURD_Qty as CURD ," + strAllcolumnWithTotal + " as Total  from (
Select thedate, isnull (ROUTE_CODE,'') as ROUTE_CODE, isnull ( ROUTE_NAME,'') as ROUTE_NAME, isnull (Qty,0) as Qty , isnull (CURD_Qty,0) as CURD_Qty  from (  select convert (varchar, thedate,103) as thedate from ExplodeDates( convert (date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "',103), convert (date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "',103) ) ) as AllDate
left outer join (
select XXX.ROUTE_CODE, XXX.DOC_DATE , max(ROUTE_NAME) as  ROUTE_NAME, Sum (Qty) as Qty , Sum (CURD_Qty) as CURD_Qty from  (
select convert (varchar,TSPL_MILK_COLLECTION_DCS.Document_Date,103) as DOC_DATE , Tab.ROUTE_CODE,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, case when TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type <> 'CURD' then TSPL_MILK_COLLECTION_DCS_DETAIL.Qty else 0 end  as Qty, case when TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type = 'CURD' then TSPL_MILK_COLLECTION_DCS_DETAIL.Qty else 0 end as CURD_Qty  
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join (select Document_No,max(MCC_Code) as MCC_Code,max(Route_Code) as Route_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MILK_COLLECTION_MCC.Route_Code 
from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail 
inner join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
)xx group by Document_No )Tab on Tab.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No   
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=Tab.Route_Code
where  TSPL_MILK_COLLECTION_DCS.Document_Date > = '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_COLLECTION_DCS.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
) XXX group by XXX.ROUTE_CODE, XXX.DOC_DATE
) as XXXData on XXXData.DOC_DATE = AllDate.thedate
)XXXFinal
PIVOT  
(  
SUM(Qty) FOR thedate IN (" + strAllColumnWithoutMax + ")
) AS Tab2 
order by ROUTE_CODE asc "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True

                'Dim summaryRowItem As New GridViewSummaryRowItem()

                'Dim MCCQty As New GridViewSummaryItem("MCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(MCCQty)
                'Dim MCCFatKg As New GridViewSummaryItem("MCC_FATKG", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(MCCFatKg)
                'Dim MCCSnfKg As New GridViewSummaryItem("MCC_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(MCCSnfKg)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 2 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
                'Pinned column 
                Gv1.Columns(0).IsPinned = True
                Gv1.Columns(1).IsPinned = True
                Gv1.Columns(Gv1.Columns.Count - 1).IsPinned = True
                Gv1.Columns(Gv1.Columns.Count - 1).PinPosition = PinnedColumnPosition.Right
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
            'View()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("ROUTE_CODE").HeaderText = "Route Code"
            Gv1.Columns("ROUTE_NAME").HeaderText = "Route Name"
            Gv1.Columns(ii).BestFit()
        Next

    End Sub

    'Sub View()

    '    If Gv1.Rows.Count > 0 Then
    '        Dim view As New ColumnGroupsViewDefinition()

    '        view.ColumnGroups.Add(New GridViewColumnGroup(" "))
    '        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_No").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_Date").Name)
    '        If rdbDetails.Checked = True Then
    '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
    '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
    '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("UploaderNo").Name)
    '        End If
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("ROUTE_NAME").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vehicle_No").Name)

    '        '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
    '        ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
    '        If rdbSummary.Checked = True Then
    '            view.ColumnGroups.Add(New GridViewColumnGroup("Entered Data"))
    '            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FAT").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNF").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
    '            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
    '            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_Qty").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FATKG").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNFKG").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (BMC-DCS)"))
    '            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
    '            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
    '            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
    '            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
    '            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data(Entered - BMC)"))
    '            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_Qty").Name)
    '            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FAT").Name)
    '            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNF").Name)
    '            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_FATKG").Name)
    '            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DiffEnteredVsMCC_SNFKG").Name)

    '        Else
    '            view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
    '            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Qty").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FAT").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNF").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_FATKG").Name)
    '            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_SNFKG").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("DCS Data"))
    '            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_Qty").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FAT").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNF").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_FATKG").Name)
    '            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("DCS_SNFKG").Name)

    '            view.ColumnGroups.Add(New GridViewColumnGroup("Difference Data (BMC-DCS)"))
    '            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
    '            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)
    '        End If
    '        Gv1.ViewDefinition = view
    '    End If
    'End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTempTruckSheetCollectionReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(fromDate.Text) + "  To " + clsCommon.myCstr(dtpToDate.Text))


                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTempTruckSheetCollectionReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(fromDate.Text) + "  To " + clsCommon.myCstr(dtpToDate.Text))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub rptMccCollectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = dtpToDate.Value.AddMonths(-1)
        Reset()
    End Sub
End Class
