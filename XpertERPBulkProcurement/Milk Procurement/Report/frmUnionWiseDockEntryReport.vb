Imports common
Public Class frmUnionWiseDockEntryReport
    Private Sub frmUnionWiseDockEntryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ReturnBaseQry(False))
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
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormat()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnBaseQry(ByVal isPrint As Boolean) As String
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            Throw New Exception("Database[TSPL_MASTER] not found")
        End If
        Dim Qry As String = ""
        Qry = " SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE  Union_Report=1 ORDER BY Location_Name "
        dt = Nothing
        dt = clsDBFuncationality.GetDataTable(Qry)
        Qry = ""
        Dim BaseQry As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii <> 0 Then
                    BaseQry &= Environment.NewLine & " UNION All " & Environment.NewLine
                End If

                BaseQry &= " select '" & clsCommon.myCstr(dt.Rows(ii)("DataBase_Name")) & "' As [UnionCode],'" & clsCommon.myCstr(dt.Rows(ii)("Location_Name")) & "' As [Union],
TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift_Date,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.No_Of_Cans,
TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT,
(TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight * TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.FAT) / 100 As FAT_KG,
TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF,
(TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight * TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNF) / 100 As SNF_KG,
TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Weight,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample,
Case When Manual_Weight=1 Then 'Manual' Else 'Auto' End As WeightManualAuto, 
Case When Manual_Weight=1 Then 'Manual' Else 'Auto' End As SampleManualAuto
from [" & clsCommon.myCstr(dt.Rows(ii)("DataBase_Name")) & "].dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
Left Outer Join [" & clsCommon.myCstr(dt.Rows(ii)("DataBase_Name")) & "].dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD On TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
Left Outer Join [" & clsCommon.myCstr(dt.Rows(ii)("DataBase_Name")) & "].dbo.TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code
Where CONVERT(date,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift_Date,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And CONVERT(date,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift_Date,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' And TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Source_API=1
"
            Next

            Qry = " Select [Union],MCC_Code,MCC_NAME,WeightManualCount,WeightManualWeight,WeightAutoCount,WeightAutoWeight,
SampleManualCount,
Case When SampleManualMilk_Weight>0 Then ((SampleManualFATKG*100)/SampleManualMilk_Weight) Else 0 End As [SampleManualFAT],
Case When SampleManualMilk_Weight>0 Then ((SampleManualSNFKG*100)/SampleManualMilk_Weight) Else 0 End As [SampleManualSNF],
SampleAutoCount,
Case When SampleAutoMilk_Weight>0 Then ((SampleAutoFATKG*100)/SampleAutoMilk_Weight) Else 0 End As [SampleAutoFAT],
Case When SampleAutoMilk_Weight>0 Then ((SampleAutoSNFKG*100)/SampleAutoMilk_Weight) Else 0 End As [SampleAutoSNF]"
            If isPrint Then
                Qry &= " ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "' +'  to  '+ '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "' As [DateRange],'" & objCommonVar.CurrentUser & "' As [PrintBy] "
            End If
            Qry &= "from (
Select Max([Union]) As [Union],MCC_Code,Max(MCC_NAME) As MCC_NAME, 
SUM(WeightManualCount) As WeightManualCount,SUM(WeightManualWeight) As WeightManualWeight,
SUM(WeightAutoCount) As WeightAutoCount,SUM(WeightAutoWeight) As WeightAutoWeight,
SUM(SampleManualCount) As SampleManualCount,SUM(SampleManualMilk_Weight) As SampleManualMilk_Weight,SUM(SampleManualFATKG) As SampleManualFATKG,SUM(SampleManualSNFKG) As SampleManualSNFKG,
Sum(SampleAutoCount) As SampleAutoCount,SUM(SampleAutoMilk_Weight) As SampleAutoMilk_Weight,Sum(SampleAutoFATKG) As SampleAutoFATKG,Sum(SampleAutoSNFKG) As SampleAutoSNFKG
from (Select [UnionCode],[Union],MCC_Code,MCC_NAME,Shift_Date,
Case When WeightManualAuto='Manual' Then 1 Else 0 End As WeightManualCount,
Case When WeightManualAuto='Manual' Then Milk_Weight Else 0 End As WeightManualWeight,
Case When WeightManualAuto='Auto' Then 1 Else 0 End As WeightAutoCount,
Case When WeightManualAuto='Auto' Then Milk_Weight Else 0 End As WeightAutoWeight,
Case When SampleManualAuto='Manual' Then 1 Else 0 End As SampleManualCount,
Case When SampleManualAuto='Manual' Then Milk_Weight Else 0 End As SampleManualMilk_Weight,
Case When SampleManualAuto='Manual' Then FAT_KG Else 0 End As SampleManualFATKG,
Case When SampleManualAuto='Manual' Then SNF_KG Else 0 End As SampleManualSNFKG,
Case When SampleManualAuto='Auto' Then 1 Else 0 End As SampleAutoCount,
Case When SampleManualAuto='Auto' Then Milk_Weight Else 0 End As SampleAutoMilk_Weight,
Case When SampleManualAuto='Auto' Then FAT_KG Else 0 End As SampleAutoFATKG,
Case When SampleManualAuto='Auto' Then SNF_KG Else 0 End As SampleAutoSNFKG
from (" & BaseQry & ") As BaseQry ) As xxxFinal Group By UnionCode,MCC_Code ) As FinalQry "
            If isPrint Then
                Qry &= " Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' "
            End If
            Qry &= "Order By [Union],MCC_NAME"
        End If
        Return Qry
    End Function


    Sub SetGridFormat()
        Try
            gv1.AutoExpandGroups = True
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).BestFit()
            Next

            gv1.Columns("Union").HeaderText = "Union"
            'gv1.Columns("MCC_Code").Name = "MCC Code"
            'gv1.Columns("MCC_Code").IsVisible = False
            gv1.Columns("MCC_NAME").HeaderText = "MCC Name"
            gv1.Columns("WeightManualCount").HeaderText = "Weight Manual Count"
            gv1.Columns("WeightManualWeight").HeaderText = "Weight Manual Weight"
            gv1.Columns("WeightAutoCount").HeaderText = "Weight Auto Count"
            gv1.Columns("WeightAutoWeight").HeaderText = "Weight Auto Weight"
            gv1.Columns("SampleManualCount").HeaderText = "QC Manual Count"
            gv1.Columns("SampleManualFAT").HeaderText = "QC Manual FAT"
            gv1.Columns("SampleManualSNF").HeaderText = "QC Manual SNF"
            gv1.Columns("SampleAutoCount").HeaderText = "QC Auto Count"
            gv1.Columns("SampleAutoFAT").HeaderText = "QC Auto FAT"
            gv1.Columns("SampleAutoSNF").HeaderText = "QC Auto SNF"
            gv1.ShowGroupPanel = True
            gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For ii As Integer = 3 To gv1.Columns.Count - 1
                If Not gv1.Columns(ii).Name.Contains("FAT") AndAlso Not gv1.Columns(ii).Name.Contains("SNF") Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:N2}", GridAggregateFunction.Sum))
                End If
            Next

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            View()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()

                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("MCC_Name").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Weighment"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("WeightManualCount").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("WeightManualWeight").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("WeightAutoCount").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("WeightAutoWeight").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Quality Control"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleManualCount").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleManualFAT").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleManualSNF").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleAutoCount").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleAutoFAT").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SampleAutoSNF").Name)
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim Qry As String = ReturnBaseQry(True)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt, "crptUnionWiseDockEntryReport", "") ''report for both (RCDF And RCDFCF)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class