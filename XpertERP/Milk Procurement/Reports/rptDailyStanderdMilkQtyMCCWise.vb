Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptDailyStanderdMilkQtyMCCWise
    Inherits FrmMainTranScreen
    Dim qry As String
    'Dim btnReset As Boolean = False
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptDailyStanderdMilkQtyMCCWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Private Sub RptDailyStanderdMilkQtyMCCWise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            Me.Text = "Daily Standard-Milk's Qty MCC-Wise"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        LoadData()
    End Sub
    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select MCC_Code as Code , MCC_NAME as Name from TSPL_MCC_MASTER "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("RPTMCC", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Sub LoadData()

        Try
            If clsCommon.GetDateWithStartTime(dtpFromdate1.Value) > clsCommon.GetDateWithEndTime(dtpToDate.Value) Then
                dtpFromdate1.Focus()
                Throw New Exception("From-Date Cannot be Greater than To-Date")
            End If
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            qry = ""
            Dim whrLoc As String = ""
            Dim Loc As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim strlocation As String = ""
            Dim strMccName As String = ""
            Dim strMccNameWithOuter_ddd As String = ""
            Dim strMccNameWithFinal As String = ""
            Dim strMccNameWith_tttt As String = ""
            Dim strMccNameWith_Max As String = ""
            Dim strRunningCalculation As String = ""
            Dim strVLCMCCTotal As String = ""
            Dim strRunVLCMCCTotal As String = ""
            Dim strSummeryColumn As String = ""


            whrLoc = " and 2=2 "
            Loc = " and 2=2 "
            fromdate = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strlocation = clsCommon.GetMulcallString(txtLocation.arrValueMember)
            End If
            If clsCommon.myLen(strlocation) > 0 Then
                whrLoc = " and TSPL_MCC_MASTER.MCC_Code  in (" + strlocation + " )"
                Loc = " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" + strlocation + " ) "
            End If
            strMccName = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+'['+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') ")
            strMccNameWithOuter_ddd = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ', '+'isnull(ddd.['+ +aa.MCC_NAME +'],0) as [' +aa.MCC_NAME +'] '  from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') ")
            strMccNameWithFinal = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+'final.['+ +aa.MCC_NAME +']' + ' as '+'['+ +aa.MCC_NAME +' - Today]' +','+'final.[Run'+ +aa.MCC_NAME +']' + ' as '+'['+ +aa.MCC_NAME +' - To Date]' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null  " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') ")
            strSummeryColumn = clsDBFuncationality.getSingleValue("Select  STUFF((SELECT ','+ + +aa.MCC_NAME +' - Today' +','++ +aa.MCC_NAME +' - To Date' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + " )aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '')")
            strMccNameWith_tttt = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+'tttt.['+ +aa.MCC_NAME +']' +','+'tttt.[Run'+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') ")
            strMccNameWith_Max = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+'max(a.['+ +aa.MCC_NAME +']' +') as ' + '['+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') ")
            strRunningCalculation = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+'(select sum( b.['+ +aa.MCC_NAME +']' +') as ' + '['+ +aa.MCC_NAME +']' + 'from  RunningTotals b where convert (date, b.Source_Doc_Date,103) <= convert(date,a.Source_Doc_Date,103)) as ' + '['+ 'Run'+ aa.MCC_NAME + ']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '')")
            strVLCMCCTotal = clsDBFuncationality.getSingleValue("Select  STUFF((SELECT '+'+'tttt.['+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where  TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '')")
            strRunVLCMCCTotal = clsDBFuncationality.getSingleValue("Select  STUFF((SELECT '+'+'tttt.[Run'+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where  TSPL_MCC_MASTER.MCC_NAME is not null " + whrLoc + ")aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '')")
            If clsCommon.myLen(strRunningCalculation) > 0 Then
                strRunningCalculation = clsDBFuncationality.getSingleValue("select REPLACE ( '" + strRunningCalculation + "' ,'&lt;','<' )")
            End If
            If clsCommon.myLen(strMccNameWith_tttt) > 0 Then
                strMccNameWith_tttt = strMccNameWith_tttt + " ,( " + strVLCMCCTotal + " ) as VLC_MCC_Total, ( " + strRunVLCMCCTotal + " ) as Run_VLC_MCC_Total, tttt.AClassQty , tttt.RunAClassQty "
            End If
            If clsCommon.myLen(strMccNameWithFinal) > 0 Then
                strMccNameWithFinal = strMccNameWithFinal + " ,final.VLC_MCC_Total as [VLC+MCC - Today],final.Run_VLC_MCC_Total as [VLC+MCC - To Date] , final.AClassQty as [A Class - Today],final.RunAClassQty as [A Class - To Date], ( final.VLC_MCC_Total + final.AClassQty ) as [G. Total - Today] , (final.Run_VLC_MCC_Total + final.RunAClassQty) as [G. Total - To Date] "
                strSummeryColumn = strSummeryColumn + ",VLC+MCC - Today,VLC+MCC - To Date,A Class - Today,A Class - To Date,G. Total - Today,G. Total - To Date"
            End If
            If clsCommon.myLen(strMccNameWithOuter_ddd) > 0 Then
                strMccNameWithOuter_ddd = strMccNameWithOuter_ddd + ",ddd.AClassQty"
            End If

            Dim InnerQueryForMccQty As String = "select DISTINCT convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_Date,103) as Source_Doc_Date , TSPL_MCC_MASTER.MCC_NAME, SUM(convert(decimal(18,2),(fat_PER * (1.028))* 60 / 6.5 ) + convert(decimal(18,2) ,(SNF_PER * (1.028))* 40 /8.5 )) AS Qty, 0 as  AClassQty   from TSPL_INVENTORY_MOVEMENT_NEW  " & _
                                                " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " & _
                                                " where  TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type = 'MCC-MSRN'  and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_Date,103) >=convert(date,'" + fromdate + "',103) and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_Date,103) <=Convert(Date,'" + Todate + "',103) and TSPL_MCC_MASTER.MCC_NAME is not null " + Loc + " group by convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_Date,103) ,TSPL_MCC_MASTER.MCC_NAME"
            'UDL CLIENT - TICKET WAS TO CORRECT A QUAILITY MILK VALUE
            Dim InnerQryForAClass As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                InnerQryForAClass = "   SELECT CONVERT(date, Tspl_Gate_Entry_Details.Date_And_Time, 103) AS Source_Doc_Date, MAX('') AS MCC_NAME, SUM(0) AS Qty, SUM(TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight) AS AClassQty " & _
                                    "   FROM Tspl_Gate_Entry_Details LEFT OUTER JOIN TSPL_GATE_ENTRY_CHEMBER_DETAILS ON Tspl_Gate_Entry_Details.Gate_Entry_No = TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code AND Chamber_Qty > 0 LEFT OUTER JOIN TSPL_Weighment_Detail ON TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No LEFT OUTER JOIN TSPL_WEIGHMENT_CHEMBER_DETAILS ON TSPL_Weighment_Detail.Weighment_No = TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No AND TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc = TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " & _
                                    "   WHERE 1 = 1 " & _
                                    "   AND CASE WHEN ISNULL(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN 'Bulk In' ELSE 'MCC In' END IN ('Bulk In')  AND TSPL_VENDOR_MASTER.Vendor_Code <> 'M0000288' " & _
                                    "   AND CONVERT(date, Tspl_Gate_Entry_Details.Date_And_Time, 103) >= convert(date,'" + fromdate + "',103) AND CONVERT(date, Tspl_Gate_Entry_Details.Date_And_Time, 103) <= Convert(Date,'" + Todate + "',103) " & _
                                    "   GROUP BY CONVERT(date, Tspl_Gate_Entry_Details.Date_And_Time, 103) "

                'InnerQryForAClass = " SELECT CONVERT(date, Source_Doc_Date, 103) AS Source_Doc_Date, '' AS MCC_NAME, 0 AS Qty, (ISNULL(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0)) AS AClassQty FROM Tspl_Gate_Entry_Details LEFT JOIN tspl_bulk_milk_srn ON Tspl_Gate_Entry_Details.Gate_Entry_No = tspl_bulk_milk_srn.Gate_Entry_No LEFT JOIN TSPL_INVENTORY_MOVEMENT_NEW ON TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = tspl_bulk_milk_srn.SRN_NO AND TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type = 'BulkSRN' AND Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' AND Tspl_Gate_Entry_Details.Gate_Entry_Type = 'P' WHERE 1 = 1 "
                'InnerQryForAClass += " and convert (date ,Source_Doc_Date,103) >=convert(date,'" + fromdate + "',103) and convert (date ,Source_Doc_Date,103) <=Convert(Date,'" + Todate + "',103)   "
            Else
                InnerQryForAClass = " select convert (date ,Source_Doc_Date,103) as Source_Doc_Date , '' as MCC_NAME , 0 as Qty, sum (isnull(std_Qty,0) ) as  AClassQty   from TSPL_INVENTORY_MOVEMENT_NEW left outer join " & _
                "tspl_bulk_milk_srn on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=tspl_bulk_milk_srn.SRN_NO left outer join Tspl_Gate_Entry_Details on " & _
                "tspl_bulk_milk_srn.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No where Trans_Type = 'BulkSRN' and Doc_Type='BulkProc' and Gate_Entry_Type='P'   " & _
                "and convert (date ,Source_Doc_Date,103) >=convert(date,'" + fromdate + "',103) and convert (date ,Source_Doc_Date,103) <=Convert(Date,'" + Todate + "',103) group by convert (date ,Source_Doc_Date,103)  "
            End If

            Dim AllDateBetweenToDateQuery As String = " select convert (date, aa.thedate,103) as thedate , '' as MCC_NAME, 0 as std_Qty ,  0 as  AClassQty  from (select * from  dbo.ExplodeDates( convert (date,'" + fromdate + "', 103),convert (date, '" + Todate + "', 103)) ) aa  "

            qry = " ;with RunningTotals as ( select  ddd.Source_Doc_Date, " + strMccNameWithOuter_ddd + " from (  select *  from (  " + InnerQueryForMccQty + "  " & _
                  " Union All " & _
                  " " + InnerQryForAClass + " " & _
                  " union All " & _
                  " " + AllDateBetweenToDateQuery + " " & _
                  " ) xxxx " & _
                  " pivot (   sum(Qty)   for MCC_NAME in (" + strMccName + ") ) piv " & _
                  " ) ddd  ) " & _
                  " select convert(varchar,final.Source_Doc_Date, 103 ) as Date, " + strMccNameWithFinal + "  from (" & _
                  " select tttt.Source_Doc_Date,  " + strMccNameWith_tttt + " from ( " & _
                  " select convert (date,a.Source_Doc_Date,103) as Source_Doc_Date, " + strMccNameWith_Max + " ," & _
                  " " + strRunningCalculation + " " & _
                  " ,max(a.AClassQty) as AClassQty , " & _
                  " (select sum( b.AClassQty) as [RunAClassQty]   from  RunningTotals b where convert (date, b.Source_Doc_Date,103) <= convert(date,a.Source_Doc_Date,103)) as [RunAClassQty] " & _
                  " from RunningTotals a group by convert (date, a.Source_Doc_Date,103)  ) tttt " & _
                  " ) final  order by convert (date, final.Source_Doc_Date,103) "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                FooterSummery(strSummeryColumn)
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = "Company : " & objCommonVar.CurrentCompanyName ' clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" & clsUserMgtCode.RptDailyStanderdMilkQtyMCCWise & "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptDailyStanderdMilkQtyMCCWise & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Public Sub FooterSummery(ByVal strColumnName As String)
        Dim words As String() = strColumnName.Split(New Char() {","c})
        If gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim word As String
            For Each word In words
                Dim item1 As New GridViewSummaryItem(word, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
