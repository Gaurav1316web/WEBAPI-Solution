Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class frmJobworkItemInStatusReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try

            Dim qry As String = "select TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [Document No],convert(varchar, TSPL_JWO_GATE_ENTRY.Date_And_Time,103) as [Document Date] " &
            ", TSPL_JWO_GATE_ENTRY_DETAIL.item_code As [Item Code], TSPL_JWO_GATE_ENTRY_DETAIL.Item_Desc As [Item Description]   " &
              ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.UOM ELSE TSPL_JWO_SRN_DETAIL.UOM END AS UOM " &
              ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_JWO_SRN_DETAIL.Qty end as Qty " &
              ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END AS [Batch No] " &
            ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_JWO_SRN_DETAIL.Qty end as [C/S Qty as per Logbook] " &
            ",convert(varchar,BI.Manufacture_Date,103) as [Date of Packing] " &
            ",'' as [C/S Qty released by QA] " &
            ",convert(varchar,TSPL_JWO_GATE_ENTRY.QC_Post_Date,103) as [Date of Release] " &
            ",case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_JWO_SRN_DETAIL.Qty end as [C/S Qty received by FGS] " &
            ",convert(varchar,TSPL_JWO_SRN_HEAD.Posted_Date,103) as [Date of Receiving] " &
            ",'' as [Balance Qty at Production] " &
            " From TSPL_JWO_GATE_ENTRY_DETAIL " &
              " left join TSPL_JWO_GATE_ENTRY on TSPL_JWO_GATE_ENTRY.Gate_Entry_No=TSPL_JWO_GATE_ENTRY_DETAIL.Gate_Entry_No " &
            " Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code =TSPL_JWO_GATE_ENTRY.Comp_Code " &
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_JWO_GATE_ENTRY.location_code " &
            " Left Join TSPL_JWO_SRN_HEAD ON TSPL_JWO_SRN_HEAD.Against_Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No " &
            " LEFT JOIN TSPL_JWO_SRN_DETAIL ON TSPL_JWO_SRN_DETAIL.Document_No=TSPL_JWO_SRN_HEAD.Document_No " &
            " And TSPL_JWO_SRN_DETAIL.Item_Code=TSPL_JWO_GATE_ENTRY_DETAIL.Item_Code " &
            " left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code " &
             " Left Join TSPL_BATCH_ITEM AS BI ON TSPL_JWO_SRN_DETAIL.Document_No=BI.Document_Code And TSPL_JWO_SRN_DETAIL.SNo=BI.Parent_Line_No And  BI.Document_Type='JWO-SRN' "
            qry += " where TSPL_JWO_GATE_ENTRY.isPosted=1 "
            qry += " and convert(date, TSPL_JWO_GATE_ENTRY.Date_And_Time,103) >=  convert(date,'" + txtfromDate.Value + "',103)  and  convert(date, TSPL_JWO_GATE_ENTRY.Date_And_Time,103) <= convert(date,'" + txtToDate.Value + "',103) "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_JWO_GATE_ENTRY.location_code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
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
        txtLocation.arrValueMember = Nothing
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
    Private Sub frmJobworkItemInStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub frmJobworkItemInStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
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
                arrHeader.Add("Name : Job Work Item In Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Loaction : " + clsCommon.GetMulcallString(txtLocation.arrValueMember))
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
                arrHeader.Add("Name : Job Work Item In Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Loaction : " + clsCommon.GetMulcallString(txtLocation.arrValueMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Job Work Item In Status Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub TxtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = ""
        qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        '" where Location_Code in (select Main_Location_Code from TSPL_LOCATION_MASTER where Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical') "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub


End Class
