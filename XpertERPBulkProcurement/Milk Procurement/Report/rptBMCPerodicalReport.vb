Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Public Class rptBMCPerodicalReport
    Inherits FrmMainTranScreen


    Private Sub rptBMCPerodicalReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        fromDate.Value = previousDayString
        ToDate.Value = previousDayString

    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False)
    End Sub

    Sub SetGridFormat1()

        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        '' Gv1.Columns("MCC Code").HeaderText = "BMC Code"
        Gv1.Columns("Comp_Name").IsVisible = False
        Gv1.Columns("Logo_Img").IsVisible = False
        Gv1.Columns("MCC Code").IsVisible = False
        Gv1.Columns("From_Date").IsVisible = False
        Gv1.Columns("To_Date").IsVisible = False

        Gv1.Columns("Mcc_Uploader_Code").HeaderText = "BMC Code"
        Gv1.Columns("Mcc_Uploader_Code").IsVisible = True

        Gv1.Columns("MCC Name").HeaderText = "BMC Name"
        Gv1.Columns("MCC Name").IsVisible = True

        Gv1.Columns("Vlc Uploader Code").HeaderText = "DCS Code"
        Gv1.Columns("Vlc Uploader Code").IsVisible = True

        Gv1.Columns("VLC Name").HeaderText = "DCS Name"
        Gv1.Columns("VLC Name").IsVisible = True

        Gv1.Columns("Milk Weight(KG)").IsVisible = False

        Gv1.Columns("FAT(%)").IsVisible = False
        Gv1.Columns("SNF(%)").IsVisible = False
        Gv1.Columns("FAT(KG)").IsVisible = False
        Gv1.Columns("SNF(KG)").IsVisible = False
        Gv1.Columns("QBD").IsVisible = False


        Gv1.Columns("SweetQty").HeaderText = "Quantity"
        Gv1.Columns("SweetQty").IsVisible = True
        Gv1.Columns("SweetQty").FormatString = "{0:n3}"

        Gv1.Columns("SweetFatKG").HeaderText = "KGFAT"
        Gv1.Columns("SweetFatKG").IsVisible = True
        Gv1.Columns("SweetFatKG").FormatString = "{0:n3}"

        Gv1.Columns("SweetSNFKG").HeaderText = " KGSNF"
        Gv1.Columns("SweetSNFKG").IsVisible = True
        Gv1.Columns("SweetSNFKG").FormatString = "{0:n3}"

        Gv1.Columns("SourQty").HeaderText = "Quantity"
        Gv1.Columns("SourQty").IsVisible = True
        Gv1.Columns("SourQty").FormatString = "{0:n3}"

        Gv1.Columns("SourFATKG").HeaderText = "KGFAT"
        Gv1.Columns("SourFATKG").IsVisible = True
        Gv1.Columns("SourFATKG").FormatString = "{0:n3}"

        Gv1.Columns("SourSNFKG").HeaderText = " KGSNF"
        Gv1.Columns("SourSNFKG").IsVisible = True
        Gv1.Columns("SourSNFKG").FormatString = "{0:n3}"

        Gv1.Columns("CurdQty").HeaderText = "Quantity"
        Gv1.Columns("CurdQty").IsVisible = True
        Gv1.Columns("CurdQty").FormatString = "{0:n3}"

        Gv1.Columns("Total_Qty").HeaderText = "Quantity"
        Gv1.Columns("Total_Qty").IsVisible = True
        Gv1.Columns("Total_Qty").FormatString = "{0:n3}"

        Gv1.Columns("TotalKGFAT").HeaderText = "KGFAT"
        Gv1.Columns("TotalKGFAT").IsVisible = True
        Gv1.Columns("TotalKGFAT").FormatString = "{0:n3}"

        Gv1.Columns("TotalKGSNF").HeaderText = " KGSNF"
        Gv1.Columns("TotalKGSNF").IsVisible = True
        Gv1.Columns("TotalKGSNF").FormatString = "{0:n3}"

        Gv1.Columns("%FAT").HeaderText = "%FAT"
        Gv1.Columns("%FAT").IsVisible = True
        Gv1.Columns("%FAT").FormatString = "{0:n3}"

        Gv1.Columns("%SNF").HeaderText = " %SNF"
        Gv1.Columns("%SNF").IsVisible = True
        Gv1.Columns("%SNF").FormatString = "{0:n3}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("SweetQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("SweetFatKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SweetSNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("SourQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SourFATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("SourSNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("CurdQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("TotalKGFAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TotalKGSNF", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        'Dim summaryItem1 As New GridViewSummaryItem()
        'summaryItem1.FormatString = "{0:F2}"
        'summaryItem1.Name = "%FAT"
        'summaryItem1.AggregateExpression = "sum(TotalKGFAT)*100/sum(Total_Qty)"
        'summaryRowItem.Add(summaryItem1)

        'Dim summaryItem2 As New GridViewSummaryItem()
        'summaryItem2.FormatString = "{0:F2}"
        'summaryItem2.Name = "%SNF"
        'summaryItem2.AggregateExpression = "sum(TotalKGSNF)*100)/sum(Total_Qty)"
        'summaryRowItem.Add(summaryItem2)
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        View()
    End Sub
    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Mcc_Uploader_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vlc Uploader Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("VLC Name").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Sweet"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SweetQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SweetFatKG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SweetSNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sour"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("SourQty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("SourFATKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("SourSNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Curd"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("CurdQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("Total_Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("TotalKGFAT").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("TotalKGSNF").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("%FAT").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("%SNF").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try

            Dim FinalQuery As String = Nothing
            Dim BaseQuery As String = Nothing
            Dim qry As String = Nothing

            BaseQuery = " Select TSPL_MILK_SRN_HEAD.Comp_Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Buffalo Milk Qty (Ltr)] , TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_QTY As [Milk Weight(KG)], convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR) As [Milk Weight(LTR)],Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR)*5 as [DBT Amount] , TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.FAT_KG  As [FAT(KG)],TSPL_MILK_SRN_DETAIL.SNF_KG  As [SNF(KG)], Convert(decimal(18,3), ROUND(TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [FAT(LTR)], Convert(decimal(18,3),ROUND(TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR / 100,3,1)) As [SNF(LTR)] ,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) + ' ' + CONVERT(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,8) as [Doc Date Time] ,
              case when isnull(TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'') = ''  then (case when isnull(TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type , '') = '' then 'SWEET' when upper (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type)='SOUR' THEN 'SOUR' when upper(TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type)='CURD' THEN 'CURD' else upper (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type) end ) else case when upper (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type)='SOUR' THEN 'SOUR' when upper (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type)='CURD' THEN 'CURD' else upper (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type )end end as QBD   ,TSPL_MILK_SRN_DETAIL.Qty
                 From TSPL_MILK_SRN_DETAIL 
                 Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
                 Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL ON TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
                 Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_NO=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
                 Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                 Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                 where 2 = 2 and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103) >= convert(date,'" & fromDate.Value & "',103)  and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE ,103)  <=  convert(date,'" & ToDate.Value & "'  ,103) "

            FinalQuery = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,From_Date = '" & fromDate.Value & "',To_Date= '" & ToDate.Value & "',aa.[MCC Code] ,aa.Mcc_Uploader_Code,aa.[MCC Name],aa.[DCS Uploader Code],[DCS Name] ,aa.[Milk Weight(KG)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,
QBD,SweetQty,SweetFatKG,SweetSNFKG,SourQty,SourFATKG,SourSNFKG,CurdQty ,	qty as Total_Qty , (SweetFatKG + SourFATKG ) as TotalKGFAT, (SweetSNFKG + SourSNFKG) as TotalKGSNF, ((SweetFatKG + SourFATKG) * 100 ) / qty as [%FAT],((SweetSNFKG + SourSNFKG) * 100 ) / qty as [%SNF]
from  " & Environment.NewLine & ""

            FinalQuery += "( select max(pp.Comp_Code) as Comp_Name,max(pp.Comp_Code) as Comp_Code,pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],sum([Milk Weight(KG)] ) as [Milk Weight(KG)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)] ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)], sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)] ,sum(NET_AMOUNT) as NET_AMOUNT,max(Mcc_Uploader_Code) as Mcc_Uploader_Code,[Vlc Code],max([Vlc Uploader Code])[DCS Uploader Code],max([VLC Name])[DCS Name] ,QBD,SUM(SweetQty) SweetQty,sum(SweetFatKG) SweetFatKG,sum(SweetSNFKG) SweetSNFKG,sum(SourQty) SourQty ,SUM(SourFATKG) SourFATKG, sum(SourSNFKG) SourSNFKG, sum(CurdQty) CurdQty , sum(Qty)qty from (  " & Environment.NewLine & ""

            FinalQuery += "Select  final.Comp_Code,final.MCC as [MCC Code] ,final.[MCC Name],final.Date ,final.[Doc Date] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name],final.UOM_Code as [UOM],final.[Milk Weight(KG)],final.[DBT Amount],final.[FAT(%)]  ,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)],final.[FAT(LTR)],final.[SNF(LTR)] ,NET_AMOUNT,Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code],final.[Doc Date Time] , final.QBD , final.Qty, Case when QBD = 'SWEET' then Qty else 0 end as SweetQty,  Case when  QBD = 'SWEET' then [FAT(KG)] else 0  end  as SweetFatKG,  Case when  QBD = 'SWEET' then [SNF(KG)] else 0  end  as SweetSNFKG, Case when  QBD = 'SOUR' then Qty else 0 end as SourQty, Case when  QBD = 'SOUR' then [FAT(KG)] else 0 end as SourFATKG,Case when  QBD = 'SOUR' then [SNF(KG)] else 0 end as SourSNFKG,Case when QBD = 'CURD' then Qty else 0 end as CurdQty    From ( " & Environment.NewLine & " " & BaseQuery & " ) As final where 2=2 
              ) as  pp group by pp.[MCC Code] , pp.[Vlc Code] , QBD 
              ) as aa 
              left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = aa.Comp_Code
              order by [MCC Code]"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                SetGridFormat1()
                Gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBMCPeriodicalReport", "")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        fromDate.Value = previousDayString
        ToDate.Value = previousDayString
        btnGo.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    'transportSql.QuickExportToExcel(Gv1, "", "", , arrHeader)
                    clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class