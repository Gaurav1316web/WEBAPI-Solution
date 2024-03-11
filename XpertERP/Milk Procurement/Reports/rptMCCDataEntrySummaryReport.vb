Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class rptMCCDataEntrySummaryReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try

            Dim qry As String = "select ROW_NUMBER () over (order by TSPL_LOCATION_MASTER.Location_Code ) As SNo,max(TSPL_LOCATION_MASTER.Location_Desc) as [Plant]" &
                             " , COUNT(distinct MCC_Active.MCC_Code) As [Active MCC], SUM(isnull(xx.[Collection Unit], 0)) As [Collection Unit]" &
                             " ,COUNT(distinct MCC_Active.MCC_Code)-SUM(isnull(xx.[Collection Unit],0)) as [Shortage MCC],sum(ISNULL(XX.VLC_Counts,0)) as [No of Collection Center]" &
                             " ,SUM(ISNULL(DC.Dispatch_Count,0)) as [IUT to MPF],SUM(ISNULL(DCO.Dispatch_Count,0)) as [IUT to Other]" &
                             " FROM " &
                             " (select   TSPL_MCC_MASTER.MCC_Code,DWC.DWC_Counts AS [Collection Unit],VLC_Counts from TSPL_MCC_MASTER " &
                            " LEFT OUTER JOIN (select (case when COUNT(Document_No)>0 then 1 else 0 end) as DWC_Counts,MCC_Code from TSPL_MILK_SHIFT_UPLOADER_HEAD where 1=1 " &
                                       "  And  CONVERT(date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) >= convert(date,'" + txtfromDate.Value + "',103) AND " &
                            " Convert(Date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) <= convert(Date,'" + txtToDate.Value + "',103) " &
                             " Group by MCC_Code)AS DWC ON DWC.MCC_Code=TSPL_MCC_MASTER.MCC_Code  " &
                            " LEFT OUTER JOIN (select  COUNT(distinct VLC_Code) as VLC_Counts,TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code from " &
                            " TSPL_MILK_SHIFT_UPLOADER_DETAIL Left join " &
                            " TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No=TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No " &
                            " where 1 = 1 And Convert(Date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) >= Convert(Date,'" + txtfromDate.Value + "',103) AND  " &
                            " Convert(Date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) <= convert(Date,'" + txtToDate.Value + "',103)  group by MCC_Code)AS VLC ON VLC.MCC_Code=TSPL_MCC_MASTER.MCC_Code   " &
                            " where  isnull(TSPL_MCC_MASTER.Plant_Code,'') <>'' " &
                            " )xx " &
                            " LEFT OUTER JOIN(select count(1) as Dispatch_Count, MCC_CODE from TSPL_MCC_Dispatch_Challan " &
                            " WHERE(case when TSPL_MCC_Dispatch_Challan.isIntermittent=1 then FinalLoc else Mcc_Or_Plant_Code end) ='1150'  " &
                            " And Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >= Convert(Date,'" + txtfromDate.Value + "',103) AND  " &
                            " Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <= convert(Date,'" + txtToDate.Value + "',103)  " &
                            "  Group by MCC_CODE)DC ON DC.MCC_Code=XX.MCC_CODE " &
                            " LEFT OUTER JOIN (select count(1) as Dispatch_Count, MCC_CODE " &
                            " From TSPL_MCC_Dispatch_Challan " &
                            " WHERE(case when TSPL_MCC_Dispatch_Challan.isIntermittent=1 then FinalLoc else Mcc_Or_Plant_Code end) <>'1150'  " &
                            " And Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >= Convert(Date,'" + txtfromDate.Value + "',103) AND  " &
                            " Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <= convert(Date,'" + txtToDate.Value + "',103)  " &
                            " Group by MCC_CODE)DCO ON DCO.MCC_Code=XX.MCC_CODE " &
                            " LEFT OUTER JOIN TSPL_MCC_MASTER ON XX.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE  " &
                            " Left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Plant_Code " &
                            " LEFT OUTER JOIN  " &
                            " (SELECT MCC_CODE from TSPL_MCC_MASTER WHERE TSPL_MCC_MASTER.In_active=0)MCC_Active ON MCC_Active.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE " &
                            "  group by TSPL_LOCATION_MASTER.Location_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True

                gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()

            gv1.BestFitColumns()

            ReStoreGridLayout()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub Reset()
        txtfromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()

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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : MCC Data Entry Summary Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : MCC Data Entry Summary Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("MCC Data Entry Summary Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



End Class
