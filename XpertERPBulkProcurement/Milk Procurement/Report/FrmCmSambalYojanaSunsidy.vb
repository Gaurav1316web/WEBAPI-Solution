Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions
Public Class FrmCmSambalYojanaSunsidy
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dtDistrict As DataTable = New DataTable
    'Private Sub SetUserMgmtNew()
    '    ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
    '    If Not (MyBase.isReadFlag) Then
    '        Throw New Exception("Permission Denied")
    '    End If
    '    'rmExportToExcel.Visible = MyBase.isExport
    '    btnSplitExport.Visible = MyBase.isExport
    'End Sub
#End Region
    Private Sub FrmCmSambalYojanaSunsidy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub

    Public Sub LoadData()
        Try
            Dim ConversionFactor As String = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            Dim dt As New DataTable
            Dim strQry As String = ""
            Dim strQryy As String = ""


            Dim whrcls As String = ""
            whrcls = " and TSPL_DBT_NEFT.From_Date >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and TSPL_DBT_NEFT.To_Date<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'  "

            If TxtDistrict.arrValueMember IsNot Nothing AndAlso TxtDistrict.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_DISTRICT_MASTER.Code in (" + clsCommon.GetMulcallString(TxtDistrict.arrValueMember) + ")"
            End If

            If TxtVidhanSabha.arrValueMember IsNot Nothing AndAlso TxtVidhanSabha.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE in (" + clsCommon.GetMulcallString(TxtVidhanSabha.arrValueMember) + ")"
            End If
            Dim BaseQry As String = ""
            BaseQry = " WITH CTE AS  ( SELECT FORMAT(From_Date,'MMM/yyyy') AS [Month],TSPL_DISTRICT_MASTER.Name AS District,TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_NAME AS VidhanSabha,Farmers,Amount
                    FROM(SELECT From_Date,DISTRICT_Code,VIDHAN_SABHA_CODE,COUNT(DISTINCT MP_Code) AS Farmers,SUM(Amount) AS Amount  FROM(SELECT TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,TSPL_DBT_NEFT_DETAIL.Amount,TSPL_VENDOR_MASTER.DISTRICT_Code, TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE,From_Date 
                      FROM(SELECT Document_Code,Against_MP_Incentive_TR,Amount FROM TSPL_DBT_NEFT_DETAIL 
                    UNION ALL SELECT Document_Code,Against_MP_Incentive_TR,Amount FROM TSPL_DBT_NEFT_DETAIL_invalid 
                    UNION ALL 
                    SELECT Document_Code,Against_MP_Incentive_TR,Amount 
                    FROM TSPL_DBT_NEFT_DETAIL_hold) TSPL_DBT_NEFT_DETAIL
                    INNER JOIN (SELECT * FROM (SELECT ROW_NUMBER() OVER(Partition BY from_date ORDER BY UKID) AS Rep, Document_Code,RCDF_Status,From_Date,To_Date FROM TSPL_DBT_NEFT)x WHERE Rep=1)TSPL_DBT_NEFT 
                                     ON TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
                                LEFT JOIN TSPL_MP_INCENTIVE_ENTRY_DETAIL 
                                     ON TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id= TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
                                LEFT JOIN TSPL_MP_MASTER 
                                     ON TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
                                LEFT JOIN TSPL_VLC_MASTER_HEAD 
                                     ON TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
                                LEFT JOIN TSPL_VENDOR_MASTER 
                                     ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                                WHERE 2=2 " & whrcls & ") xx 
                               GROUP BY DISTRICT_Code,VIDHAN_SABHA_CODE,From_Date ) xxx
                        LEFT JOIN TSPL_DISTRICT_MASTER ON TSPL_DISTRICT_MASTER.Code=xxx.DISTRICT_Code
                        LEFT JOIN TSPL_VIDHAN_SABHA_MASTER ON TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE=xxx.VIDHAN_SABHA_CODE ),
                    UNPVT AS 
                    ( SELECT  VidhanSabha,1 as SNo, District + ' Farmers' AS Col, Farmers AS Val FROM CTE
                    UNION ALL
                    SELECT  VidhanSabha,2 as SNo, District + ' Amount' AS Col, Amount AS Val FROM CTE
                    ) "


            dtDistrict = clsDBFuncationality.GetDataTable(BaseQry + " select col from UNPVT  where Col is not null group by col,SNo order by SNo ")
            Dim DistrictName As String = ""
            Dim AllDistrictName As String = ""
            Dim TotalFarmers As String = ""
            Dim TotalAmt As String = ""
            If dtDistrict.Rows.Count > 0 Then
                For i As Integer = 0 To dtDistrict.Rows.Count - 1
                    If i = 0 Then
                        DistrictName += "[" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "] "
                        AllDistrictName += " sum(IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0)) As [" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "]"
                        If clsCommon.myCstr(dtDistrict.Rows(i)("Col")).Contains("Far") Then
                            TotalFarmers += " IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0) "
                        End If
                    Else
                        DistrictName += ", [" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "] "
                        AllDistrictName += " ,sum(IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0)) As [" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "]"
                        If clsCommon.myCstr(dtDistrict.Rows(i)("Col")).Contains("Far") Then
                            TotalFarmers += " + " + " IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0) "
                        End If
                        If clsCommon.myCstr(dtDistrict.Rows(i)("Col")).Contains("Amount") Then
                            If clsCommon.myLen(TotalAmt) > 0 Then
                                TotalAmt += " + " + " IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0) "
                            Else
                                TotalAmt += " IsNull([" + clsCommon.myCstr(dtDistrict.Rows(i)("Col")) + "],0) "
                            End If
                        End If
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
                Exit Sub
            End If


            If VidhanSabhaWise.IsChecked Then
                strQry += " " & BaseQry & " SELECT  VidhanSabha," & AllDistrictName & ", sum(" & TotalFarmers & ") as TotalFarmers,sum(" & TotalAmt & ") as TotalAmt
                    FROM UNPVT
                    PIVOT (SUM(Val) FOR Col IN (" & DistrictName & "))PVT where VidhanSabha is not null group by VidhanSabha
                    ORDER By VidhanSabha "
            Else

                strQry = " select format( From_Date,'MMM/yyyy') as [Month],TSPL_DISTRICT_MASTER.Name as Distict,TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_NAME as VidhanSabha,Farmers,Amount from (
                            select From_Date,DISTRICT_Code,VIDHAN_SABHA_CODE,count(Distinct MP_Code) as Farmers,sum(Amount) as Amount from 
                            (
                            select  TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code,TSPL_DBT_NEFT_DETAIL.Amount,TSPL_VENDOR_MASTER.DISTRICT_Code, TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE,From_Date 
                            from (select Document_Code,Against_MP_Incentive_TR,Amount from TSPL_DBT_NEFT_DETAIL 
                            union all select  Document_Code,Against_MP_Incentive_TR,Amount from TSPL_DBT_NEFT_DETAIL_invalid 
                            union all select Document_Code,Against_MP_Incentive_TR,Amount from TSPL_DBT_NEFT_DETAIL_hold 
                            ) TSPL_DBT_NEFT_DETAIL
                            inner join (select * from ( select ROW_NUMBER() over(Partition by from_date order by UKID) as Rep,Document_Code,RCDF_Status,From_Date,To_Date from TSPL_DBT_NEFT)x where rep=1 
                            )TSPL_DBT_NEFT on TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
                            left outer join TSPL_MP_INCENTIVE_ENTRY_DETAIL on TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id= TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
                            left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
                            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
                            left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                            WHERE 2=2  " & whrcls & ") xx group by DISTRICT_Code,VIDHAN_SABHA_CODE,From_Date)xxx
                            left outer join TSPL_DISTRICT_MASTER on TSPL_DISTRICT_MASTER.Code=xxx.DISTRICT_Code
                            left outer join TSPL_VIDHAN_SABHA_MASTER on TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE=xxx.VIDHAN_SABHA_CODE
                            order by From_Date,Distict,VidhanSabha "
            End If
            dt = clsDBFuncationality.GetDataTable(strQry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                gv.EnableFiltering = True
                'FormatGrid()
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            SetGridFormationOFGV()
            gv.BestFitColumns()
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To Gv1.Columns.Count - 1
        '    Gv1.Columns(ii).ReadOnly = True

        'Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        For i As Integer = 1 To gv.Columns.Count - 1
            Dim aa = gv.Columns(i).HeaderText()
            Dim item8 As New GridViewSummaryItem(gv.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

        Next
        'Dim aa = gvData.Columns(i).HeaderText()
        'Dim item81 As New GridViewSummaryItem("Distict", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item81)

        'Dim item82 As New GridViewSummaryItem("VidhanSabha", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item82)

        'Dim item83 As New GridViewSummaryItem("Farmers", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item83)

        'Dim item84 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item84)
        gv.ShowGroupPanel = True
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv.MasterTemplate.ShowTotals = True
        ReStoreGridLayout()

    End Sub



    'Sub View()
    '    If gv.Rows.Count > 0 Then
    '        Dim view As New ColumnGroupsViewDefinition()

    '        view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
    '        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNo").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Union Name").Name)

    '        view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month1, "MMM-yyyy")))
    '        view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
    '    End If
    'End Sub





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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        TxtVidhanSabha.arrValueMember = Nothing
        TxtDistrict.arrValueMember = Nothing
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Close()
    End Sub

    Private Sub TxtDistrict__My_Click(sender As Object, e As EventArgs) Handles TxtDistrict._My_Click
        Try
            Dim qry As String = "select Code,Name from TSPL_DISTRICT_MASTER where 2=2 "
            TxtDistrict.arrValueMember = clsCommon.ShowMultipleSelectForm("DISTRICT", qry, "Code", "Name", TxtDistrict.arrValueMember, TxtDistrict.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVidhanSabha__My_Click(sender As Object, e As EventArgs) Handles TxtVidhanSabha._My_Click
        Try
            Dim qry As String = " select VIDHAN_SABHA_CODE,VIDHAN_SABHA_NAME from TSPL_VIDHAN_SABHA_MASTER where 2=2 "
            TxtVidhanSabha.arrValueMember = clsCommon.ShowMultipleSelectForm("VIDHANSABHA", qry, "VIDHAN_SABHA_CODE", "VIDHAN_SABHA_NAME", TxtVidhanSabha.arrValueMember, TxtVidhanSabha.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("CM Sambal Yojana Subsidy", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("CM Sambal Yojana Subsidy", gv, arrHeader, "CM Sambal Yojana Subsidy", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class