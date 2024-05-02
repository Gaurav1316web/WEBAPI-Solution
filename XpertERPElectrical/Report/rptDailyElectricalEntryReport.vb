'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


' Ticket No : BHA/31/08/18-000501 by prabhakar - Create new report 
Public Class rptDailyElectricalEntryReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub



    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        'txtItem.arrValueMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtTransaction.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strSlotCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_SLOT_MASTER "))
            If strSlotCount <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " Slot is not Available in [Slot Master]", Me.Text)
                Return
            End If
            Dim strDGcount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_DG_MASTER "))
            If strDGcount <= 0 Then
                clsCommon.MyMessageBoxShow(Me, " DG is not Available in [DG Master]", Me.Text)
                Return
            End If


            Dim strSlotName As String = clsDBFuncationality.getSingleValue("  DECLARE @colsSlot AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( TSPL_SLOT_MASTER.Description) as Description FROM TSPL_SLOT_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strUnitForDGName As String = clsDBFuncationality.getSingleValue("  DECLARE @colsDGName AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME('Units For('+ TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
            Dim strConsumptionForDGName As String = clsDBFuncationality.getSingleValue("  DECLARE @colsDGName AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME('Consumption For('+ TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
            Dim strRuningHoursForDGName As String = clsDBFuncationality.getSingleValue("  DECLARE @colsDGName AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME('Runing Hours For('+ TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")


            Dim strUnitForDGSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( +'Units For('+ TSPL_DG_MASTER.Description+')')+ ' ,0) ) as ' + QUOTENAME( +'Units For('+ TSPL_DG_MASTER.Description+')')   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strConsumptionForDGSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( +'Consumption For('+ TSPL_DG_MASTER.Description+')')+ ' ,0) ) as ' + QUOTENAME( +'Consumption For('+ TSPL_DG_MASTER.Description+')')   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strRuningHoursForDGSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( +'Runing Hours For('+ TSPL_DG_MASTER.Description+')')+ ' ,0) ) as ' + QUOTENAME( +'Runing Hours For('+ TSPL_DG_MASTER.Description+')')   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")



            Dim strSlotNameWithSum As String = clsDBFuncationality.getSingleValue("  DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_SLOT_MASTER.Description)+ ' ,0) ) as ' + QUOTENAME( TSPL_SLOT_MASTER.Description)   FROM TSPL_SLOT_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strDGNameWithSum As String = clsDBFuncationality.getSingleValue("  DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_DG_MASTER.Description)+ ' ,0) ) as ' + QUOTENAME( TSPL_DG_MASTER.Description)   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strSlotNameWithZero As String = clsDBFuncationality.getSingleValue(" DECLARE @colsslotNameWithZero AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as '+ QUOTENAME( TSPL_SLOT_MASTER.Description) as Description FROM TSPL_SLOT_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

            Dim strUnitsForDGNameWithZero As String = clsDBFuncationality.getSingleValue("DECLARE @colssUnitsForDGNameWithZero AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as '+ QUOTENAME( 'Units For(' +TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
            Dim strConsumptionForDGNameWithZero As String = clsDBFuncationality.getSingleValue("DECLARE @colssUnitsForDGNameWithZero AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as '+ QUOTENAME( 'Consumption For(' +TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")
            Dim strRuningHoursForDGNameWithZero As String = clsDBFuncationality.getSingleValue("DECLARE @colssUnitsForDGNameWithZero AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as '+ QUOTENAME( 'Runing Hours For(' +TSPL_DG_MASTER.Description+')') as Description FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            Dim strTotalSlot As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_SLOT_MASTER.Description)+ ' ,0) ) '  FROM TSPL_SLOT_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strTotalDGUnit As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( +'Units For('+ TSPL_DG_MASTER.Description+')')+',0))'   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strTotalDGConsumption As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( +'Consumption For('+ TSPL_DG_MASTER.Description+')')+',0))'   FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strTotalDGRuningHours As String = clsDBFuncationality.getSingleValue(" DECLARE @colsDGNameWithSum AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( +'Runing Hours For('+ TSPL_DG_MASTER.Description+')') +',0))'  FROM TSPL_DG_MASTER   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")


            Dim qry As String = ""
            Dim dt As New DataTable

            qry = "  select convert (varchar,finalXXX.Consumption_Date,103) as [Consumption Date], " + strSlotNameWithSum + " , " + strUnitForDGSum + " , " + strConsumptionForDGSum + " , " + strRuningHoursForDGSum + " ,( " + strTotalDGUnit + " + " + strTotalDGConsumption + "+" + strTotalSlot + " + " + strTotalDGRuningHours + " ) as Total from ( " & _
                  "  select Consumption_Date ," + strSlotNameWithSum + " ,  " + strUnitForDGSum + " , " + strConsumptionForDGSum + " , " + strRuningHoursForDGSum + "   from ( " & _
                  "  select Document_No,Consumption_Date , " + strSlotNameWithZero + " , " + strUnitForDGName + "  , " + strConsumptionForDGName + ", " + strRuningHoursForDGName + " from ( " & _
                  "  select TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No, TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date , 'Units For('+TSPL_DG_MASTER.Description+')' as DG_CODE_Units , 'Consumption For(' +TSPL_DG_MASTER.Description + ')' as  DG_CODE_Cons, 'Runing Hours For(' +TSPL_DG_MASTER.Description + ')' as  DG_CODE_RunningHours, " & _
                  "  TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.DG_Unit, TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.DG_Consumption , TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.DG_Runing_Hours  from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD  " & _
                  "  Left Outer join TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS on TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.Document_No = TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No " & _
                  "  left outer join TSPL_DG_MASTER on TSPL_DG_MASTER.Code = TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.DG_CODE " & _
                  "  where convert(date,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date,103)<=convert(date,'" + ToDate.Value + "',103)" & _
                  "  ) XXX " & _
                  " Pivot  " & _
                  " ( " & _
                  " sum (DG_UNIT) For DG_CODE_Units in (" + strUnitForDGName + ") " & _
                  "  ) as Piv " & _
                  " PIVOT " & _
                  " ( " & _
                  " SUM(DG_Consumption) FOR DG_CODE_Cons IN (" + strConsumptionForDGName + ") " & _
                  " ) AS pv2 " & _
                  " Pivot " & _
                  " ( " & _
                  " SUM(DG_Runing_Hours) FOR DG_CODE_RunningHours IN (" + strRuningHoursForDGName + ") " & _
                  " ) AS pv3 " & _
                  " ) PPP group by Consumption_Date " & _
                  "  Union All " & _
                  "  select Final.Consumption_Date , " + strSlotNameWithSum + ", " + strUnitForDGSum + ", " + strConsumptionForDGSum + ", " + strRuningHoursForDGSum + "" & _
                  " from (Select Consumption_Date ," + strSlotName + "," + strUnitsForDGNameWithZero + " ," + strConsumptionForDGNameWithZero + ", " + strRuningHoursForDGNameWithZero + "" & _
                  " from (select TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No,  " & _
                  " TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date ,TSPL_SLOT_MASTER.Description,TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS.SLOT_UNIT   from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD " & _
                  " Left Outer join TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS on TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS.Document_No = TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No  " & _
                  " left outer join TSPL_SLOT_MASTER on TSPL_SLOT_MASTER.Code = TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS.SLOT_CODE " & _
                  " where convert(date,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date,103)<=convert(date,'" + ToDate.Value + "',103) " & _
                  " ) XXX " & _
                  " Pivot  " & _
                  " ( " & _
                  " SUM(SLOT_UNIT) FOR Description IN (" + strSlotName + ") " & _
                  " ) as Piv " & _
                  " ) Final group by Final.Consumption_Date " & _
                  " )finalXXX group by finalXXX.Consumption_Date " & _
                  "   "
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Gv1.Columns("Consumption Date").IsPinned = True
                Gv1.Columns("Total").IsPinned = True
                Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
                ' For summary ==========================================================
                Dim item As Integer = 0
                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = item To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        If clsCommon.CompairString(aa, "Consumption Date") <> CompairStringResult.Equal Then
                            Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(item1)
                        End If

                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
                '=======================================================================
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    'Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
    '    Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

    '    txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    'End Sub
    'Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
    '    Dim qry As String
    '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
    '    txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    'End Sub
    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            clsCommon.MyExportToExcelGrid("Daily Electrical Entry Report", Gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
