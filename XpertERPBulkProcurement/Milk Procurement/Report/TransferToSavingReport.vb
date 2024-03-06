Imports System.IO
Imports common

'Create by new report Shaurya
Public Class TransferToSavingReport
    Inherits FrmMainTranScreen
    #Region "Variables"
    Dim MultipleFinderFillAuto As Boolean = False
#End Region
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        'ControlEnableDisable(True)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TransferToSavingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = dtpToDate.Value.AddMonths(-1)
        Reset()
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            qry = "Select ROW_NUMBER() over(order by TSPL_TRANSFER_TO_SAVING_DETAIL.PK_ID) as SNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as Dcs_Uploader_Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Dcs_Name,TSPL_TRANSFER_TO_SAVING_DETAIL.Amount 
from TSPL_TRANSFER_TO_SAVING_DETAIL
left outer join TSPL_TRANSFER_TO_SAVING on TSPL_TRANSFER_TO_SAVING.Document_No = TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code
where
            TSPL_TRANSFER_TO_SAVING.Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_TRANSFER_TO_SAVING.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and Loc_Seg_Code='ALW'"
            dt = clsDBFuncationality.GetDataTable(qry)

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                'SetGridFormationOFGV1()
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                'Gv1.EnableFiltering = True

                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 2 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        'Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        'summaryRowItem.Add(item1)
                    Next
                    'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Sub SetdtpToDate()
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0


        Dim strMCCcode = ""
        'If clsCommon.myLen(txtMCC.Value) Then
        '    strMCCcode = " location_Code in ( '" + clsCommon.myCstr(txtMCC.Value) + "')  and "
        'End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + "  Location_Category='MCC' and Rejected_Type='N') ")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))


        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                dtpToDate.Value = fromDate.Value
                Exit Sub
            End If
            dtpToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

            If fromDate.Value.Month <> dtpToDate.Value.Month Then
                dtpToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If fromDate.Value.Month <> dtNxtPay.Month Then
                dtpToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = fromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            fromDate.Value = today.AddDays(-dayDiff)
            dtpToDate.Value = fromDate.Value.AddDays(6)
        End If
    End Sub
    Private Sub fromDate_Leave(sender As Object, e As EventArgs) Handles fromDate.Leave
        If MultipleFinderFillAuto Then
            SetdtpToDate()
        Else
            SetdtpToDate()
        End If
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles fromDate.Validating
        If MultipleFinderFillAuto Then
            SetdtpToDate()
        Else
            SetdtpToDate()
        End If
    End Sub

    'Sub SetGridFormationOFGV1()
    '    Gv1.TableElement.TableHeaderHeight = 40
    '    Gv1.MasterTemplate.ShowRowHeaderColumn = False
    '    For ii As Integer = 0 To Gv1.Columns.Count - 1
    '        Gv1.Columns(ii).ReadOnly = True
    '        Gv1.Columns(ii).IsVisible = True
    '        Gv1.Columns("ROUTE_CODE").HeaderText = "Route Code"
    '        Gv1.Columns("ROUTE_NAME").HeaderText = "Route Name"
    '        Gv1.Columns(ii).BestFit()
    '    Next

    'End Sub
End Class
