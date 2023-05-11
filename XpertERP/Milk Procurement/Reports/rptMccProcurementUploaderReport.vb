Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class rptMccProcurementUploaderReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
           
            Dim qry As String = "select tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name],TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code as [MCC Code]  ,tspl_mcc_master.MCC_NAME as [Mcc Name],TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No as [Document No],convert (varchar,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103) as [Document Date]" & _
             ", (select  convert(varchar,min(shift_date),103) + '  To  ' + convert(varchar,max(shift_date),103) from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No) AS [From Date - To Date] " & _
             ",(case when TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status=1 then 'Posted' else 'Pending' end) as Status" & _
             ",TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.DOCK_CODE as [Dock Code]" & _
             ",TSPL_DOCK_MASTER.Description as [Dock Name],TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Description" & _
             " from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD" & _
             " left join  tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.mcc_code" & _
             " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" & _
             " left join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.dock_code"
            qry += " where 2=2 "
            qry += " and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_date,103) >=  convert(date,'" + txtfromDate.Value + "',103)  and  convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_date,103) <= convert(date,'" + txtToDate.Value + "',103) "

            If clsCommon.CompairString(ddlStatus.SelectedValue, "Posted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status,0)=1 "
            ElseIf clsCommon.CompairString(ddlStatus.SelectedValue, "Unposted") = CompairStringResult.Equal Then
                qry += " and isnull(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status,0)=0 "
            End If



            If txtPlant.arrValueMember IsNot Nothing AndAlso txtPlant.arrValueMember.Count > 0 Then
                qry += " and tspl_mcc_master.Plant_Code in (" + clsCommon.GetMulcallString(txtPlant.arrValueMember) + ")  "
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  "
            End If

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
                clsCommon.MyMessageBoxShow("No Data Found")
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
        txtPlant.arrValueMember = Nothing
        txtMCC.arrValueMember = Nothing
        txtfromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        ddlStatus.SelectedValue = "All"
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
        LoadStatusType()
    End Sub

    Sub LoadStatusType()
        Dim dt1 As DataTable
        dt1 = New DataTable
        dt1.Columns.Add("Code", GetType(String))
        dt1.Rows.Add("All")
        dt1.Rows.Add("Posted")
        dt1.Rows.Add("Unposted")

        ddlStatus.DataSource = dt1
        ddlStatus.ValueMember = "Code"
        ddlStatus.DisplayMember = "Code"
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
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : MCC Procurement Uploader Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrValueMember))
                End If

                If txtPlant.arrValueMember IsNot Nothing AndAlso txtPlant.arrValueMember.Count > 0 Then
                    arrHeader.Add("Plant : " + clsCommon.GetMulcallString(txtPlant.arrValueMember))
                End If
                If clsCommon.myLen(ddlStatus.Text) > 0 Then
                    arrHeader.Add("Status : " + ddlStatus.Text)
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : MCC Procurement Uploader Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrValueMember))
                End If

                If txtPlant.arrValueMember IsNot Nothing AndAlso txtPlant.arrValueMember.Count > 0 Then
                    arrHeader.Add("Plant : " + clsCommon.GetMulcallString(txtPlant.arrValueMember))
                End If

                If clsCommon.myLen(ddlStatus.Text) > 0 Then
                    arrHeader.Add("Status : " + ddlStatus.Text)
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("MCC Procurement Uploader Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select tspl_mcc_master.MCC_Code as [Code],tspl_mcc_master.MCC_NAME as [Name] from tspl_mcc_master"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@MCC", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtPlant__My_Click(sender As Object, e As EventArgs) Handles txtPlant._My_Click
        Try
            Dim qry As String = "Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc as Name from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Type = 'PLANT'"
            txtPlant.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@PL", qry, "Code", "Name", txtPlant.arrValueMember, txtPlant.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    
End Class
