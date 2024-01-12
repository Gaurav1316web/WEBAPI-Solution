Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
' Ticket No : ERO/12/04/19-000556 By Prabhakar For - Create New Report 
Public Class rptJWTankerStatusReport
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim qry As String = Nothing

            qry = "select [Proc Type],[Vendor Code],[Vendor Name],[To Location Code],[To Location Desc],[Tanker No],[Gate Entry No],[Entry Date],[Weightment No],[Weighment Date],[Weighment Posting Status],[Gate Out No],[Gate Out Date],[Jobwork Estimate No],[Jobwork Estimate Date] from ("
            qry += " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As [Proc Type], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Tspl_Gate_Entry_Details.Vendor_Code End As [Vendor Code], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then TSPL_VENDOR_MASTER.Vendor_Name End As [Vendor Name], " & _
                   " IsNull(Tspl_Gate_Entry_Details.location_Code, '') As [To Location Code], IsNull(toLocation.Location_Desc, '') As [To Location Desc], IsNull(Tspl_Gate_Entry_Details.Tanker_No, '') As [Tanker No], IsNull(Tspl_Gate_Entry_Details.Gate_Entry_No, '') As [Gate Entry No], Convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], " & _
                   " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Entry Date],  IsNull(TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No, '') As [Weightment No], IsNull(Convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_date,103), '') As [Weighment Date], " & _
                   " Case When IsNull(TSPL_GENERAL_WEIGHMENT_DETAIL.Posted, 0) = 0 Then 'Not Done' Else 'Done' End As [Weighment Posting Status], " & _
                   " IsNull(TSPL_Gate_Out.Doc_No, '') As [Gate Out No], IsNull(Convert(varchar,TSPL_Gate_Out.Doc_Date,103), '') As [Gate Out Date],  " & _
                   " TSPL_JWI_ESTIMATION_HEAD.Document_No as [Jobwork Estimate No], convert(varchar, TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) as [Jobwork Estimate Date] " & _
                   " From Tspl_Gate_Entry_Details " & _
                   " Left Outer Join TSPL_GENERAL_WEIGHMENT_DETAIL  On TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " & _
                   " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " & _
                   " Left Outer Join TSPL_LOCATION_MASTER As toLocation On toLocation.Location_Code = Tspl_Gate_Entry_Details.location_Code " & _
                   " Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " & _
                   " left outer join TSPL_JWI_ESTIMATION_WEIGHMENT on  TSPL_JWI_ESTIMATION_WEIGHMENT.WEIGHMENT_Code = TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No " & _
                   " left outer join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_no = TSPL_JWI_ESTIMATION_WEIGHMENT.Document_No "
            qry += " where Tspl_Gate_Entry_Details.GATE_ENTRY_TYPE = 'J' "



            qry += ")x  where 2=2 "



            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and [Vendor Code] in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
            End If





            If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                qry += " and [To Location Code] in (" + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember) + ")  "
            End If

            qry += " and [Gate Entry Date]>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and [Gate Entry Date] <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' "
            qry += " order by  [Gate Entry Date]"


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
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtVendor.arrValueMember = Nothing
        TxtMultiToLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
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


    Private Sub TxtMultiToLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiToLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiToLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiTo", qry, "Code", "Name", TxtMultiToLocation.arrValueMember, TxtMultiToLocation.arrDispalyMember)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Jobwork Tanker Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                End If

               
                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If
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
                arrHeader.Add("Name :Jobwork Tanker Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                End If

                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Tanker Status Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code as [Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Name] from TSPL_VENDOR_MASTER "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiVendor", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub
End Class
