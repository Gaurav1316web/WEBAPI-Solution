Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient

Public Class rptBMCTankerTesting
#Region "Variables"
    Dim dt As DataTable

#End Region

    Private Sub rptBMCTankerTesting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        LoadReportType()
    End Sub

    Private Sub LoadReportType()

        Dim dtType As DataTable = New DataTable()
        dtType.Columns.Add("Code", GetType(String))
        dtType.Columns.Add("Type", GetType(String))
        Dim dr As DataRow = dtType.NewRow()
        dr("Code") = "BMC"
        dr("Type") = "BMC"
        dtType.Rows.Add(dr)

        dr = dtType.NewRow()
        dr("Code") = "Tanker"
        dr("Type") = "Tanker"
        dtType.Rows.Add(dr)

        txtReportType.DataSource = dtType
        txtReportType.ValueMember = "Code"
        txtReportType.DisplayMember = "Type"
        txtReportType.SelectedIndex = 0

    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select ROUTE_NO as [ROUTE NO] ,ROUTE_NAME as [ROUTE NAME] from TSPL_BULK_ROUTE_MASTER where 2=2 "

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("BMCRoute", qry, "ROUTE NO", "ROUTE NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Dim qry As String = "select distinct MCC_Code as Code,MCC_NAME as Name from TSPL_MCC_MASTER"
        txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(Me.Form_ID, qry, "Code", "Name", txtBMC.arrValueMember, Nothing)
    End Sub

    'Sub RefreshRoute()
    '    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
    '        Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        txtRoute.arrValueMember = Nothing
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Dim arr As New ArrayList
    '            For Each dr As DataRow In dt.Rows
    '                arr.Add(clsCommon.myCstr(dr("Route_Code")))
    '            Next
    '            txtRoute.arrValueMember = arr
    '        End If
    '    End If
    'End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtBMC.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtReportType.SelectedIndex = 0
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            If clsCommon.CompairString(txtReportType.SelectedItem.Value, "BMC") = CompairStringResult.Equal Then
                qry = "SELECT convert(varchar,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) as Document_Date,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo ,TSPL_MILK_COLLECTION_MCC.Tanker_No , TSPL_MILK_COLLECTION_MCC.Route_Code,
                TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No , TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty as Qty
                ,Case When TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty >0 Then cast(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_FATKg * 100/TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty as decimal(18,2)) Else 0 End as FAT
                ,Case When TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_SNFKg*100/TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty as decimal(18,2))  Else 0 End as SNF,'' as CLR,
                TSPL_MILK_COLLECTION_MCC_DETAIL.Temp , TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_FAT , TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_SNF  ,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_CLR,TSPL_MILK_COLLECTION_MCC.Route_Code as Route ,TSPL_MILK_COLLECTION_MCC_DETAIL.Correction_Qty as Corr_Qty,  TSPL_MILK_COLLECTION_MCC_DETAIL.Correction_FAT , TSPL_MILK_COLLECTION_MCC_DETAIL.Correction_SNF , '' as Corr_CLR
                FROM TSPL_MILK_COLLECTION_MCC_DETAIL 
                left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No =TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No  
                left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code  
                where  convert( date ,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) >= CONVERT(date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "', 103)
                and convert( date ,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "', 103)"
                If clsCommon.myLen(txtRoute.arrValueMember) > 0 Then
                    qry += "and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
                If clsCommon.myLen(txtBMC.arrValueMember) > 0 Then
                    qry += "And TSPL_MILK_COLLECTION_MCC_DETAIL. in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"

                End If
                qry += "ORDER BY TSPL_MILK_COLLECTION_MCC_DETAIL.SNO"
            Else
                qry = "select convert(varchar,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) as Document_Date, ROW_NUMBER() OVER(PARTITION BY 1 ORDER BY TSPL_MILK_COLLECTION_MCC.Document_No) AS SNo, TSPL_MILK_COLLECTION_MCC.Tanker_No , TSPL_MILK_COLLECTION_MCC.Route_Code, TSPL_MILK_COLLECTION_MCC.Trip_No,TSPL_MILK_COLLECTION_MCC.Original_Qty AS Qty
                       ,Case When TSPL_MILK_COLLECTION_MCC.Original_Qty >0 Then cast(TSPL_MILK_COLLECTION_MCC.Original_FATKg * 100/TSPL_MILK_COLLECTION_MCC.Original_Qty as decimal(18,2)) Else 0 End as FAT
                       ,Case When TSPL_MILK_COLLECTION_MCC.Original_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC.Original_SNFKg*100/TSPL_MILK_COLLECTION_MCC.Original_Qty as decimal(18,2))  Else 0 End as SNF
                       ,'' as CLR,TSPL_MILK_COLLECTION_MCC.Temp , TSPL_MILK_COLLECTION_MCC.Retesting_FAT, TSPL_MILK_COLLECTION_MCC.Retesting_SNF , TSPL_MILK_COLLECTION_MCC.Retesting_CLR ,TSPL_MILK_COLLECTION_MCC.Route_Code as Route,TSPL_MILK_COLLECTION_MCC.Correction_Qty as Corr_Qty, TSPL_MILK_COLLECTION_MCC.Correction_FAT , TSPL_MILK_COLLECTION_MCC.Correction_SNF, '' as Corr_CLR
                        from  TSPL_MILK_COLLECTION_MCC
					   where convert( date ,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) >= CONVERT(date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "', 103)
                       and convert( date ,TSPL_MILK_COLLECTION_MCC.Document_Date , 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "', 103)"

                If clsCommon.myLen(txtRoute.arrValueMember) > 0 Then
                    qry += "TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
                'If clsCommon.myLen(txtBMC.arrValueMember) > 0 Then
                '    qry += "And TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"

                'End If
                qry += "ORDER BY SNO"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next

        gv1.Columns("Document_Date").HeaderText = "Date"
        gv1.Columns("SNo").HeaderText = "SL.No"
        gv1.Columns("Tanker_No").HeaderText = "Tanker No"
        gv1.Columns("Route_Code").HeaderText = "Route No"
        If clsCommon.CompairString(txtReportType.SelectedItem.Value, "BMC") = CompairStringResult.Equal Then
            gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC"
            gv1.Columns("Sample_No").HeaderText = "Sample No"
        Else
            gv1.Columns("Trip_No").HeaderText = "Trip No"
        End If

        gv1.Columns("Qty").HeaderText = "Qty"
        gv1.Columns("FAT").HeaderText = "Fat"
        gv1.Columns("SNF").HeaderText = "Snf"
        gv1.Columns("Temp").HeaderText = "Temp"
        gv1.Columns("Retesting_FAT").HeaderText = "Fat"
        gv1.Columns("Retesting_SNF").HeaderText = "Snf"
        gv1.Columns("Retesting_CLR").HeaderText = "CLR"
        gv1.Columns("Corr_Qty").HeaderText = "Qty"
        gv1.Columns("Correction_FAT").HeaderText = "Fat"
        gv1.Columns("Correction_SNF").HeaderText = "Snf"
        gv1.Columns("Corr_CLR").HeaderText = "CLR"



        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.BestFitColumns()
        EnableDisableControls(False)
        ReStoreGridLayout()
        Dim Trans As SqlTransaction = Nothing
        Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Trans)
        Dim CLR As Decimal
        Dim Corr_CLR As Decimal
        For ii As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(ii)("FAT") IsNot DBNull.Value OrElse dt.Rows(ii)("SNF") IsNot DBNull.Value Then
                CLR = clsEkoPro.getClrOnCalculation(dt.Rows(ii)("FAT"), dt.Rows(ii)("SNF"), corrFactor)
                gv1.Rows(ii).Cells("CLR").Value = CLR
            End If
            If dt.Rows(ii)("Correction_FAT") IsNot DBNull.Value OrElse dt.Rows(ii)("Correction_SNF") IsNot DBNull.Value Then
                Corr_CLR = clsEkoPro.getClrOnCalculation(dt.Rows(ii)("Correction_FAT"), dt.Rows(ii)("Correction_SNF"), corrFactor)
                gv1.Rows(ii).Cells("Corr_CLR").Value = Corr_CLR
            End If
        Next
        gv1.Columns("Corr_CLR").FormatString = "{0:n2}"
        gv1.Columns("CLR").FormatString = "{0:n2}"
        View()
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = MyBase.Form_ID
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message)
        End Try
    End Sub

    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("Original"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Document_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_Code").Name)
            If clsCommon.CompairString(txtReportType.SelectedItem.Value, "BMC") = CompairStringResult.Equal Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Mcc_Code_VLC_Uploader").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Sample_No").Name)

            Else
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Trip_No").Name)
            End If
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Fat").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Snf").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("CLR").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Temp").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Retesting"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Retesting_FAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Retesting_SNF").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Retesting_CLR").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Correction"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Route").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Corr_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Correction_FAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Correction_SNF").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Corr_CLR").Name)
            gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub txtReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles txtReportType.SelectedIndexChanged
        If txtReportType.SelectedIndex = 0 Then
            lblBMC.Visible = True
            txtBMC.Visible = True
        Else
            lblBMC.Visible = False
            txtBMC.Visible = False
        End If
    End Sub

End Class