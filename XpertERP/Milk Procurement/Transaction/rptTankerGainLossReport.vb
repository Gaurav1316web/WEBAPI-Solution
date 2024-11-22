Imports common
Imports System.IO
Public Class rptTankerGainLossReport
    Inherits FrmMainTranScreen

    Private Sub rptTankerGainLossReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")

    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False, False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean, ByVal print2 As Boolean)
        Try

            Dim FinalQuery As String = Nothing
            Dim BaseQuery As String = Nothing
            Dim qry As String = Nothing

            If clsCommon.myLen(txtTankerNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Tanker no", Me.Text)
                txtTankerNo.Focus()
                Exit Sub
            End If
            BaseQuery = " SELECT *,(XX.Milk_Qty-XX.Entered_Qty)AS flushing, (xx.fat_per-XX.Entered_FATKg) as FlushingFat,(xx.snf_Per-xx.Entered_SNFKg) AS FlushingSnf FROM (SELECT 
    Document_Date,
    Tanker_No,
    MAX(ROUTE_CODE) AS ROUTE_CODE,
    SUM(Entered_Qty) AS Entered_Qty,
    SUM(CAST(Milk_Qty AS FLOAT)) AS Milk_Qty,
    SUM(Gross_Weight) AS Gross_Weight,
    SUM(Tare_Weight) AS Tare_Weight,
    SUM(CAST(fat_per AS FLOAT)) AS fat_per,
    SUM(CAST(snf_Per AS FLOAT)) AS snf_Per,
    SUM(Entered_SNFKg) AS Entered_SNFKg,
    SUM(Entered_FATKg) AS Entered_FATKg
    --SUM(Flushing) AS Flushing
FROM (
    -- Include your three UNION queries here (use the same query you provided)
    SELECT CONVERT(DATE, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date, 103) AS Document_Date,
           TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.ROUTE_CODE as ROUTE_CODE,
           TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No as Tanker_No,
           TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_Qty as Entered_Qty,
           '' AS Milk_Qty,
           0 AS Gross_Weight,
           0 AS Tare_Weight,
           '' AS fat_per,
           '' AS snf_Per,
           TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_SNFKg as Entered_SNFKg,
           TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_FATKg as Entered_FATKg,
           '' AS Flushing
    FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS 
    WHERE 
convert(date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
          AND TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No = '" + txtTankerNo.Value + " '
          
    UNION ALL
    
    SELECT CONVERT(DATE, Tspl_Gate_Entry_Details.Challan_Date, 103) AS Document_Date,
           Tspl_Gate_Entry_Details.ROUTE_NO AS ROUTE_CODE,
           Tspl_Gate_Entry_Details.Tanker_No,
           0 AS Entered_Qty,
           (CASE 
                WHEN IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' 
                THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
                ELSE CASE 
                        WHEN IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 
                        THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
                        ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) 
                    END 
            END) AS Milk_Qty,
           0 AS Gross_Weight,
           0 AS Tare_Weight,
           (Tspl_Gate_Entry_Details.fat_per * (Tspl_Gate_Entry_Details.Qty_In_Kg * 1)) / 100 AS fat_per,
           (Tspl_Gate_Entry_Details.snf_Per * (Tspl_Gate_Entry_Details.Qty_In_Kg * 1)) / 100 AS snf_Per,
           0 AS Entered_SNFKg,
           0 AS Entered_FATKg,
           0 AS Flushing
    FROM Tspl_Gate_Entry_Details
    WHERE
         convert(date,Tspl_Gate_Entry_Details.Challan_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Challan_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) 
          AND Tspl_Gate_Entry_Details.Tanker_No =  '" + txtTankerNo.Value + " '
    
    UNION ALL
    
    SELECT CONVERT(DATE, TSPL_Weighment_Detail.Weighment_date, 103) AS Document_Date,
           '' AS ROUTE_CODE,
           TSPL_Weighment_Detail.Tanker_No as Tanker_No,
           0 AS Entered_Qty,
           0 AS Milk_Qty,
           TSPL_Weighment_Detail.Gross_Weight as Gross_Weight,
           TSPL_Weighment_Detail.Tare_Weight as Tare_Weight,
           0 AS fat_per,
           0 AS snf_Per,
           0 AS Entered_SNFKg,
           0 AS Entered_FATKg,
           0 AS Flushing
    FROM TSPL_Weighment_Detail
    WHERE 
                   convert(date,TSPL_Weighment_Detail.Weighment_date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Weighment_Detail.Weighment_date,103) <=convert(date,'" + txtToDate.Value + "' ,103) 

          AND TSPL_Weighment_Detail.Tanker_No =  '" + txtTankerNo.Value + " '
) AS CombinedData
GROUP BY Document_Date, Tanker_No
 )XX left outer join tspl_company_master on tspl_company_master.Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'
"
            '            BaseQuery = " select * from (SELECT CONVERT(varchar, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date, 103) AS Document_Date,
            '    MAX(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.ROUTE_CODE) AS ROUTE_CODE,
            '    MAX(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No) AS Tanker_No,
            '    MAX(CASE 
            '            WHEN IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' 
            '            THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
            '            ELSE CASE 
            '                    WHEN IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 
            '                    THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
            '                    ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) 
            '                END 
            '        END) AS Milk_Qty,SUM(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_Qty) AS Entered_Qty,  
            '    MAX(Tspl_Gate_Entry_Details.Doc_Type) AS Doc_Type,
            '    (TSPL_Weighment_Detail.Gross_Weight) AS Gross_Weight,
            '    (TSPL_Weighment_Detail.Tare_Weight) AS Tare_Weight,
            '	MAX (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*1))/100 as fat_per,
            '	MAX(Tspl_Gate_Entry_Details.snf_Per*  (Tspl_Gate_Entry_Details.Qty_In_Kg*1))/100 as snf_Per
            '	 ,sum(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_SNFKg) as Entered_SNFKg,sum(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_FATKg)AS Entered_FATKg
            ',MAX(CASE 
            '            WHEN IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' 
            '            THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
            '            ELSE CASE 
            '                    WHEN IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 
            '                    THEN Tspl_Gate_Entry_Details.Qty_In_Kg 
            '                    ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) 
            '                END 

            '        END) -
            'SUM(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Entered_Qty) 
            'AS [Flushing]
            'FROM 
            '    TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS
            'LEFT JOIN TSPL_BULK_ROUTE_MASTER 
            '    ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
            'LEFT JOIN TSPL_MCC_MASTER 
            '    ON TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code
            'LEFT JOIN Tspl_Gate_Entry_Details 
            '    ON CONVERT(DATE, Tspl_Gate_Entry_Details.Challan_Date, 103) = CONVERT(DATE, '" + txtDate.Value + "', 103)
            'LEFT JOIN TSPL_Weighment_Detail 
            '    ON CONVERT(DATE, TSPL_Weighment_Detail.Weighment_date, 103) = CONVERT(DATE, '" + txtDate.Value + "', 103)
            'WHERE 
            '    CONVERT(DATE, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date, 103) = CONVERT(DATE, '" + txtDate.Value + "' , 103)
            '    AND TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No = '" + txtTankerNo.Value + " '
            'GROUP BY 
            '    TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No,Gross_Weight,Tare_Weight,Document_Date ) XX
            '	left outer join tspl_company_master on tspl_company_master.Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                'SetGridFormat1()
                SetGridFormationOFGV1Collection()
                View()
                gv1.BestFitColumns()
                'If print = True Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "rptTankerGainLossReport", "Tanker Gain Loss Report")
                'frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtsub, "rptTankerGainLossReport", "ProfitLoss", "SubTankerProfitLoss.rpt")

                ' frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, "", "rptTankerProfitLoss", "ProfitLoss", "SubTankerProfitLoss.rpt")
                'End If
                'If print2 = True Then
                '    Dim frmCRV As New frmCrystalReportViewer()
                '    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtsub, "rptTankerProfitLossPrint2", "ProfitLoss", "SubTankerProfitLoss.rpt")
                'End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub View()
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            'If rdbDetails.Checked = True Then
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Document_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("ROUTE_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Gross_Weight").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tare_Weight").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)
            'End If

            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
            'If rdbSummary.Checked = True Then
            view.ColumnGroups.Add(New GridViewColumnGroup("Tanker Data"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk_Qty").Name)

            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("fat_per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("snf_Per").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("BMC Data"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_SNFKg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_FATKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Flushing").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("FlushingFat").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("FlushingSnf").Name)

            'End If
            gv1.ViewDefinition = view
        End If
    End Sub
    Sub SetGridFormationOFGV1Collection()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").HeaderText = "Document Date"
            gv1.Columns("ROUTE_CODE").IsVisible = True
            gv1.Columns("ROUTE_CODE").HeaderText = "ROUTE CODE"
            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").HeaderText = "Tanker No"

            gv1.Columns("Gross_Weight").IsVisible = True
            gv1.Columns("Gross_Weight").HeaderText = "Gross Weight."

            gv1.Columns("Tare_Weight").IsVisible = True
            gv1.Columns("Tare_Weight").HeaderText = "Tare Weight"
            'gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
            'gv1.Columns("Tanker_No").HeaderText = "Tanker No."
            gv1.Columns("Entered_FATKg").IsVisible = True
            gv1.Columns("Entered_FATKg").HeaderText = "KG FAT"

            gv1.Columns("Entered_SNFKg").IsVisible = True
            gv1.Columns("Entered_SNFKg").HeaderText = "KG SNF"

            gv1.Columns("fat_per").IsVisible = True
            gv1.Columns("fat_per").HeaderText = "Tanker FAT KG"

            gv1.Columns("snf_Per").IsVisible = True
            gv1.Columns("snf_Per").HeaderText = "Tanker SNF KG"

            gv1.Columns("Entered_Qty").IsVisible = True

            gv1.Columns("Entered_Qty").HeaderText = "Net Qty"
            gv1.Columns("Milk_Qty").IsVisible = True

            gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
            gv1.Columns("Flushing").IsVisible = True

            gv1.Columns("Flushing").HeaderText = "Flushing"
        Next
    End Sub

    Private Sub txtToShift_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        ' cboDocumentType.SelectedIndex = 0
        'txtRouteCode.Value = ""
        'lblRouteCode.Text = ""
        txtTankerNo.Value = ""
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
End Class