Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class DashboardMilkProcurement
    Inherits FrmMainTranScreen
    Dim Slot1FD As DateTime = Nothing
    Private Sub DashboardMilkProcurement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkRJSBNS.Visible = False
        chkRJSBNS.Checked = True
        rbdAllTrans.Checked = True
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        If rdbPosted.Checked Then
            UninonWise()
            RouteWise()
            DCSWise()
            DaysData()
        ElseIf rdbUnposted.Checked Then
            UninonWiseUnp()
            RouteWiseUNP()
            DCSWiseUNP()
            DaysDataUNP()
        ElseIf rbdAllTrans.Checked Then
            UnionWiseall()
            RouteWiseAll()
            DCSWiseAll()
            DaysDataAll()
        End If

    End Sub

    Sub DaysDataAll()
        Try
            Dim query As String = ""
            Dim DateQry As String = ""
            Dim DateTable As DataTable = Nothing
            Dim DateCode As String = Nothing
            Dim DateName As String = Nothing
            Dim DateUnion As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""

            DateQry = " SELECT DISTINCT '[' + CONVERT(VARCHAR(10), CAST(DOC_DATE AS DATE), 120) + ']' AS DOC_DATE FROM
                        ( SELECT Document_Date as DOC_DATE FROM TSPL_MILK_PROCUREMENT_UPLOADER_HEAD
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Shift_Date as DOC_DATE  FROM TSPL_MILK_SHIFT_UPLOADER_HEAD
                            WHERE CAST(Shift_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Document_Date as DOC_DATE FROM TSPL_MILK_COLLECTION_DCS
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Document_Date as DOC_DATE FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT DOC_DATE FROM TSPL_MILK_SRN_HEAD
                            WHERE CAST(DOC_DATE AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                        ) AS CombinedDates ORDER BY DOC_DATE ASC"

            DateTable = clsDBFuncationality.GetDataTable(DateQry)

            If DateTable.Rows.Count > 0 Then
                For i As Integer = 0 To DateTable.Rows.Count - 1
                    If clsCommon.myLen(DateCode) > 0 Then
                        DateCode += "," + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName += "," + "Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        'DateUnion += "," " 0 as  [" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))+"] "
                        DateUnion += ", 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "

                    Else
                        DateCode = clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName = " Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        DateUnion = " 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Previous Days Data", Me.Text)
                Exit Sub
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += "   select max(SNo)SNo,max([Union Name])[Union Name],max(Fromdate)Fromdate,max(Todate)Todate,max(username)username,
                                 " + DateName + ",max(Union_Contact_Person)[Nodal Officer],max(Union_Contact_PhoneNo)[Mobile No.]
                                from(SELECT  " + clsCommon.myCstr(ii + 1) + " AS SNo,
                              '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                '" + objCommonVar.CurrentUser + "' as username,
                                " + DateCode + "
                                from  (select  SUM(Milk_Weight) AS Milk_Weight,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                                  
                       FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            AND MUH.STATUS=0 
							GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,CONVERT(VARCHAR(10), Shift_Date, 120) AS Dates
                            
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                         AND  msh.Status= 0  GROUP BY CONVERT(VARCHAR(10), Shift_Date, 120)

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                           
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 
                            
                             UNION ALL
                            SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                           
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 
                            

                            Union All
                            SELECT SUM(Qty) AS Milk_Weight,CONVERT(VARCHAR(10), DOC_DATE, 120) AS Dates
                                FROM 
                                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                                    LEFT JOIN 
                                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                                    WHERE 
                                        msh.DOC_DATE BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                    GROUP BY 
                                        CONVERT(VARCHAR(10), DOC_DATE, 120) 
 
                            
                            ) AS Procurement
                            PIVOT (
                                    SUM(Milk_Weight) 
                                    FOR Dates IN (" + DateCode + ")) AS Tab2
                                    union all
								select " + clsCommon.myCstr(ii + 1) + " AS SNo,
                                '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                 '" + objCommonVar.CurrentUser + "' as username, " + DateUnion + " 
								) xx left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 group by SNo 
"
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv4.DataSource = Nothing
                gv4.Rows.Clear()
                gv4.Columns.Clear()
                gv4.GroupDescriptors.Clear()
                gv4.MasterTemplate.SummaryRowsBottom.Clear()
                gv4.MasterView.Refresh()
                gv4.DataSource = dt2
                For ii As Integer = 0 To gv4.Columns.Count - 1
                    gv4.Columns(ii).ReadOnly = True
                Next
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv4.EnableFiltering = True
                SetGridFormat4()
                gv4.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DCSWiseAll()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    'Dim status4 As String
                    'Dim status5 As String
                    'If rdbUnposted.Checked Then
                    '    status4 = " and muh.Status= 0 "
                    '    status5 = "and msh.Status= 0 "
                    'Else
                    '    status4 = " "
                    '    status5 = " "
                    'End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg

                FROM 
                    (SELECT 
                        COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END) as RegCount,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
					    ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
						(COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
						( ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
						SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
						COUNT(CASE WHEN dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
							mud.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                       	 left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mud.VLC_Code
	                     left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                         WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and muh.Status= 0 group by mud.VLC_Code

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
							msd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = msd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 group by  msd.VLC_Code

                        UNION ALL
                        select
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
							mcd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mcd.VLC_Code
	                    left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  GROUP BY mcd.VLC_Code
                       
                            union all

					   SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(Qty * mdd.FAT / 100) AS FATKG,
                            SUM(Qty * SNF / 100) AS SNFKG,
							mdd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
							left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mdd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  Group By mdd.VLC_Code
                            

							union all

							select sum(Qty)Milk_Weight,sum(FAT_KG) AS FATKG,sum(SNF_KG) AS SNFKG,
                        msh.VSP_Code,max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount

                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                            left join 
                             [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VENDOR_MASTER vm on vm.Vendor_Code = msh.VSP_Code
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'   group by msh.VSP_Code
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()
                gv3.DataSource = dt2
                For ii As Integer = 0 To gv3.Columns.Count - 1
                    gv3.Columns(ii).ReadOnly = True
                Next
                ' RadPageView1.SelectedPage = RadPageViewPage2
                gv3.EnableFiltering = True
                SetGridFormat3()
                gv3.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub RouteWiseAll()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    'Dim status4 As String
                    'Dim status5 As String
                    'If rdbUnposted.Checked Then
                    '    status4 = " and muh.Status= 0 "
                    '    status5 = "and msh.Status= 0 "
                    'Else
                    '    status4 = " "
                    '    status5 = " "
                    'End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.routecount), 0) AS RouteCount,
					ISNULL(SUM(Dis_Procurement.mcccount), 0) AS MCCCount,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc

                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
                        	count(distinct Bulk_Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and muh.Status= 0 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
                            count(distinct Bulk_Route_No) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            0 as Route,
							0 as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            count(distinct Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            UNION ALL

                            SELECT 
                                     SUM(Qty) AS Milk_Weight,
                                    SUM(FAT_KG) AS FATKg,
                                    SUM(SNF_KG) AS SNFKG,
                        	        count(distinct ROUTE_CODE ) as Route,
							        count(distinct msd.MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN  '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                        
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.MasterView.Refresh()
                gv2.DataSource = dt2
                For ii As Integer = 0 To gv2.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                Next
                ' RadPageView1.SelectedPage = RadPageViewPage2
                gv2.EnableFiltering = True
                SetGridFormat2()
                gv2.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub UnionWiseall()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    'Dim status4 As String
                    'Dim status5 As String
                    'If rdbUnposted.Checked Then
                    '    status4 = " and muh.Status= 0 "
                    '    status5 = "and msh.Status= 0 "
                    'Else
                    '    status4 = " "
                    '    status5 = " "
                    'End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                           and muh.Status= 0 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                       
                             union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            

                              UNION ALL

                            SELECT 
                                    SUM(Qty) AS Milk_Weight,
                                    SUM(FAT_KG) AS FATKg,
                                    SUM(SNF_KG) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2   "
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DaysDataUNP()
        Try
            Dim query As String = ""
            Dim DateQry As String = ""
            Dim DateTable As DataTable = Nothing
            Dim DateCode As String = Nothing
            Dim DateName As String = Nothing
            Dim DateUnion As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""

            DateQry = " SELECT DISTINCT '[' + CONVERT(VARCHAR(10), CAST(DOC_DATE AS DATE), 120) + ']' AS DOC_DATE FROM
                        ( SELECT Document_Date as DOC_DATE FROM TSPL_MILK_PROCUREMENT_UPLOADER_HEAD
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Shift_Date as DOC_DATE  FROM TSPL_MILK_SHIFT_UPLOADER_HEAD
                            WHERE CAST(Shift_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Document_Date as DOC_DATE FROM TSPL_MILK_COLLECTION_DCS
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            UNION
                            SELECT Document_Date as DOC_DATE FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS
                            WHERE CAST(Document_Date AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                        ) AS CombinedDates ORDER BY DOC_DATE ASC"

            DateTable = clsDBFuncationality.GetDataTable(DateQry)

            If DateTable.Rows.Count > 0 Then
                For i As Integer = 0 To DateTable.Rows.Count - 1
                    If clsCommon.myLen(DateCode) > 0 Then
                        DateCode += "," + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName += "," + "Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        'DateUnion += "," " 0 as  [" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))+"] "
                        DateUnion += ", 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "

                    Else
                        DateCode = clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName = " Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        DateUnion = " 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Previous Days Data", Me.Text)
                Exit Sub
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += "   select max(SNo)SNo,max([Union Name])[Union Name],max(Fromdate)Fromdate,max(Todate)Todate,max(username)username,
                                 " + DateName + ",max(Union_Contact_Person)[Nodal Officer],max(Union_Contact_PhoneNo)[Mobile No.]
                                from(SELECT  " + clsCommon.myCstr(ii + 1) + " AS SNo,
                              '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                '" + objCommonVar.CurrentUser + "' as username,
                                " + DateCode + "
                                from  (select  SUM(Milk_Weight) AS Milk_Weight,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                                  
                       FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            AND MUH.STATUS=0 
							GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,CONVERT(VARCHAR(10), Shift_Date, 120) AS Dates
                            
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                         AND  msh.Status= 0  GROUP BY CONVERT(VARCHAR(10), Shift_Date, 120)

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                           
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 
                            
                             UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,CONVERT(VARCHAR(10), Document_Date, 120) AS Dates
                           
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            GROUP BY CONVERT(VARCHAR(10), Document_Date, 120) 
                            
                            ) AS Procurement
                            PIVOT (
                                    SUM(Milk_Weight) 
                                    FOR Dates IN (" + DateCode + ")) AS Tab2
                                    union all
								select " + clsCommon.myCstr(ii + 1) + " AS SNo,
                                '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                 '" + objCommonVar.CurrentUser + "' as username, " + DateUnion + " 
								) xx left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 group by SNo 
"
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv4.DataSource = Nothing
                gv4.Rows.Clear()
                gv4.Columns.Clear()
                gv4.GroupDescriptors.Clear()
                gv4.MasterTemplate.SummaryRowsBottom.Clear()
                gv4.MasterView.Refresh()
                gv4.DataSource = dt2
                For ii As Integer = 0 To gv4.Columns.Count - 1
                    gv4.Columns(ii).ReadOnly = True
                Next
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv4.EnableFiltering = True
                SetGridFormat4()
                gv4.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DCSWiseUNP()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    Dim status4 As String
                    Dim status5 As String
                    If rdbUnposted.Checked Then
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                    Else
                        status4 = " "
                        status5 = " "
                    End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg

                FROM 
                    (SELECT 
                        COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END) as RegCount,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
					    ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
						(COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
						( ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
						SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
						COUNT(CASE WHEN dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
							mud.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                       	 left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mud.VLC_Code
	                     left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                         WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + " group by mud.VLC_Code

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
							msd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = msd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + " group by  msd.VLC_Code

                        UNION ALL
                        select
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
							mcd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mcd.VLC_Code
	                    left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  GROUP BY mcd.VLC_Code
                        
                            union all

					   SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(Qty * mdd.FAT / 100) AS FATKG,
                            SUM(Qty * SNF / 100) AS SNFKG,
							mdd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
							left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mdd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  Group By mdd.VLC_Code
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()
                gv3.DataSource = dt2
                For ii As Integer = 0 To gv3.Columns.Count - 1
                    gv3.Columns(ii).ReadOnly = True
                Next
                ' RadPageView1.SelectedPage = RadPageViewPage2
                gv3.EnableFiltering = True
                SetGridFormat3()
                gv3.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub RouteWiseUNP()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    Dim status4 As String
                    Dim status5 As String
                    If rdbUnposted.Checked Then
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                    Else
                        status4 = " "
                        status5 = " "
                    End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.routecount), 0) AS RouteCount,
					ISNULL(SUM(Dis_Procurement.mcccount), 0) AS MCCCount,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc

                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
                        	count(distinct Bulk_Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
                            count(distinct Bulk_Route_No) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            0 as Route,
							0 as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                       
                            union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            count(distinct Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.MasterView.Refresh()
                gv2.DataSource = dt2
                For ii As Integer = 0 To gv2.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                Next
                ' RadPageView1.SelectedPage = RadPageViewPage2
                gv2.EnableFiltering = True
                SetGridFormat2()
                gv2.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub UninonWiseUnp()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                qry += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            qry = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qry += " UNION ALL "
                    End If
                    Dim status4 As String
                    Dim status5 As String
                    If rdbUnposted.Checked Then
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                    Else
                        status4 = " "
                        status5 = " "
                    End If
                    qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        
                             union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2   "
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub DaysData()
        Try
            Dim query As String = ""
            Dim DateQry As String = ""
            Dim DateTable As DataTable = Nothing
            Dim DateCode As String = Nothing
            Dim DateName As String = Nothing
            Dim DateUnion As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""

            DateQry = "SELECT DISTINCT '[' + CONVERT(VARCHAR(10), CAST(DOC_DATE AS DATE), 120) + ']' AS DOC_DATE 
                       FROM TSPL_MILK_SRN_HEAD 
                       WHERE CAST(DOC_DATE AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                       ORDER BY DOC_DATE ASC"


            ' DateQry = "SELECT DISTINCT CAST(DOC_DATE AS DATE) AS DOC_DATE FROM TSPL_MILK_SRN_HEAD 
            'WHERE CAST(DOC_DATE AS DATE) BETWEEN DATEADD(day, -6, '" + clsCommon.GetPrintDate(txtToDate.Value) + "') AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' order by DOC_DATE asc"
            DateTable = clsDBFuncationality.GetDataTable(DateQry)

            If DateTable.Rows.Count > 0 Then
                For i As Integer = 0 To DateTable.Rows.Count - 1
                    If clsCommon.myLen(DateCode) > 0 Then
                        DateCode += "," + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName += "," + "Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        'DateUnion += "," " 0 as  [" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))+"] "
                        DateUnion += ", 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "

                    Else
                        DateCode = clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE"))
                        DateName = " Sum(" + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ") As " + clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) + ""
                        DateUnion = " 0 as " & clsCommon.myCstr(DateTable.Rows(i)("DOC_DATE")) & " "
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Previous Days Data", Me.Text)
                Exit Sub
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += "  select max(SNo)SNo,max([Union Name])[Union Name],max(Fromdate)Fromdate,max(Todate)Todate,max(username)username,
                                " + DateName + ",max(Union_Contact_Person)[Nodal Officer],max(Union_Contact_PhoneNo)[Mobile No.]
                            from (

SELECT  " + clsCommon.myCstr(ii + 1) + " AS SNo,
                              '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                '" + objCommonVar.CurrentUser + "' as username,
                                " + DateCode + "
                                FROM (SELECT SUM(Qty) AS Milk_Weight,CONVERT(VARCHAR(10), DOC_DATE, 120) AS Dates,max(Comp_Code)Comp_Code
                                FROM 
                                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                                    LEFT JOIN 
                                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                                    WHERE 
                                        msh.DOC_DATE BETWEEN '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd-MMM-yyyy").AddDays(-6)) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                    GROUP BY 
                                        CONVERT(VARCHAR(10), DOC_DATE, 120) 
                                ) AS Procurement
                                PIVOT (
                                    SUM(Milk_Weight) 
                                    FOR Dates IN (" + DateCode + ")) AS Tab2
                                Left Outer Join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Tab2.Comp_Code    

                                 union all 
								select " + clsCommon.myCstr(ii + 1) + " AS SNo,
                                '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as Todate,
                                 '" + objCommonVar.CurrentUser + "' as username," + DateUnion + "
								) xx left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 group by SNo
"
                Next
            End If



            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv4.DataSource = Nothing
                gv4.Rows.Clear()
                gv4.Columns.Clear()
                gv4.GroupDescriptors.Clear()
                gv4.MasterTemplate.SummaryRowsBottom.Clear()
                gv4.MasterView.Refresh()
                gv4.DataSource = dt2
                For ii As Integer = 0 To gv4.Columns.Count - 1
                    gv4.Columns(ii).ReadOnly = True
                Next
                'RadPageView1.SelectedPage = RadPageViewPage2
                gv4.EnableFiltering = True
                SetGridFormat4()
                gv4.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat4()
        gv4.AutoExpandGroups = True
        gv4.ShowGroupPanel = True
        gv4.ShowRowHeaderColumn = False
        gv4.AllowAddNewRow = False
        gv4.AllowDeleteRow = False
        gv4.EnableFiltering = True
        gv4.ShowFilteringRow = True


        For ii As Integer = 0 To gv4.Columns.Count - 1
            gv4.Columns(ii).ReadOnly = True
            gv4.Columns(ii).BestFit()
            gv4.Columns(ii).IsVisible = True
            gv4.Columns(ii).Width = 200
        Next

        'gv4.Columns("Comp_Code").HeaderText = "Comp_Code"
        'gv4.Columns("Comp_Code").Width = 100
        'gv4.Columns("Comp_Code").IsVisible = False

        gv4.Columns("Fromdate").HeaderText = "From Date"
        gv4.Columns("Fromdate").Width = 100
        gv4.Columns("Fromdate").IsVisible = False

        gv4.Columns("Todate").HeaderText = "To Date"
        gv4.Columns("Todate").Width = 100
        gv4.Columns("Todate").IsVisible = False

    End Sub
    Sub UninonWise()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                                 FROM 
(SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(FAT_KG) AS FATKg,
                            SUM(SNF_KG) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat1()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
            'gv1.Columns(ii).Width = 200
        Next
        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = True '

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 200
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 100
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 100
        gv1.Columns("Todate").IsVisible = False

        gv1.Columns("Milk_WeightProc").HeaderText = "QTY"
        gv1.Columns("Milk_WeightProc").IsVisible = True
        gv1.Columns("Milk_WeightProc").FormatString = "{0:n3}"

        gv1.Columns("FATKGProc").HeaderText = "FATKG"
        gv1.Columns("FATKGProc").IsVisible = True
        gv1.Columns("FATKGProc").FormatString = "{0:n3}"

        gv1.Columns("SNFKGProc").HeaderText = "SNFKG"
        gv1.Columns("SNFKGProc").IsVisible = True
        gv1.Columns("SNFKGProc").FormatString = "{0:n3}"

        gv1.Columns("FATPerProc").HeaderText = "FAT%"
        gv1.Columns("FATPerProc").IsVisible = True
        gv1.Columns("FATPerProc").FormatString = "{0:n2}"

        gv1.Columns("SNFPerProc").HeaderText = "SNF%"
        gv1.Columns("SNFPerProc").IsVisible = True
        gv1.Columns("SNFPerProc").FormatString = "{0:n2}"

        gv1.Columns("Union_Contact_Person").HeaderText = "Nodal Officer"
        gv1.Columns("Union_Contact_Person").Width = 200
        gv1.Columns("Union_Contact_Person").IsVisible = True

        gv1.Columns("Union_Contact_PhoneNo").HeaderText = "Mobile No."
        gv1.Columns("Union_Contact_PhoneNo").Width = 200
        gv1.Columns("Union_Contact_PhoneNo").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Milk_WeightProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("FATKGProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("SNFKGProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub RouteWise()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += " select final.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(sum(Dis_Procurement.RouteCount),0) AS RouteCount,ISNULL(sum(Dis_Procurement.MCCCount),0) AS MCCCount,ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc
                                 FROM 
(SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                             SUM(Qty) AS Milk_Weight,
                            SUM(FAT_KG) AS FATKg,
                            SUM(SNF_KG) AS SNFKG,
                        	count(distinct ROUTE_CODE ) as Route,
							count(distinct msd.MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                          ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.MasterView.Refresh()
                gv2.DataSource = dt2
                For ii As Integer = 0 To gv2.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                Next
                gv2.EnableFiltering = True
                SetGridFormat2()
                gv2.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat2()
        gv2.AutoExpandGroups = True
        gv2.ShowGroupPanel = True
        gv2.ShowRowHeaderColumn = False
        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = False
        gv2.EnableFiltering = True
        gv2.ShowFilteringRow = True


        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).BestFit()
            'gv2.Columns(ii).Width = 500
        Next
        gv2.Columns("SNo").Name = "SNo"
        gv2.Columns("SNo").IsVisible = True '

        gv2.Columns("Union Name").HeaderText = "Union Name"
        gv2.Columns("Union Name").Width = 200
        gv2.Columns("Union Name").IsVisible = True

        gv2.Columns("Fromdate").HeaderText = "From Date"
        gv2.Columns("Fromdate").Width = 100
        gv2.Columns("Fromdate").IsVisible = False

        gv2.Columns("Todate").HeaderText = "To Date"
        gv2.Columns("Todate").Width = 100
        gv2.Columns("Todate").IsVisible = False

        gv2.Columns("RouteCount").HeaderText = "No. Of Route"
        gv2.Columns("RouteCount").IsVisible = True
        ' gv1.Columns("RouteCount").FormatString = "{0:n3}"

        gv2.Columns("MCCCount").HeaderText = "No. Of Chiller"
        gv2.Columns("MCCCount").IsVisible = True
        'gv1.Columns("MCCCount").FormatString = "{0:n3}"

        gv2.Columns("Milk_WeightProc").HeaderText = "QTY"
        gv2.Columns("Milk_WeightProc").IsVisible = True
        gv2.Columns("Milk_WeightProc").FormatString = "{0:n3}"

        gv2.Columns("FATKGProc").HeaderText = "FATKG"
        gv2.Columns("FATKGProc").IsVisible = True
        gv2.Columns("FATKGProc").FormatString = "{0:n3}"

        gv2.Columns("SNFKGProc").HeaderText = "SNFKG"
        gv2.Columns("SNFKGProc").IsVisible = True
        gv2.Columns("SNFKGProc").FormatString = "{0:n3}"

        gv2.Columns("Union_Contact_Person").HeaderText = "Nodal Officer"
        gv2.Columns("Union_Contact_Person").IsVisible = True

        gv2.Columns("Union_Contact_PhoneNo").HeaderText = "Mobile No."
        gv2.Columns("Union_Contact_PhoneNo").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Milk_WeightProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("FATKGProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("SNFKGProc", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)


        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub DCSWise()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            union all
            SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += " select final.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg
                                 FROM 
(SELECT 
                         COUNT(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN 1 ELSE NULL END) as RegCount,
   ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
	COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
	(COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
	( ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
	(ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
	(ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
		sum(xxx.Milk_Weight)Milk_Weight,sum(xxx.FATKG)FATKG,sum(xxx.SNFKG)SNFKG,count(xxx.dcs)dcs,
	COUNT(CASE WHEN xxx.dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
	COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
		
                    FROM (
                     select sum(Qty)Milk_Weight,sum(FAT_PER)FAT,sum(SNF_PER)SNF,sum(FAT_KG) AS FATKG,sum(SNF_KG) AS SNFKG,
                        msh.VSP_Code,max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount

                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                            left join 
                             [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VENDOR_MASTER vm on vm.Vendor_Code = msh.VSP_Code
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' group by msh.VSP_Code
                         ) AS xxx
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.SummaryRowsBottom.Clear()
                gv3.MasterView.Refresh()
                gv3.DataSource = dt2
                For ii As Integer = 0 To gv3.Columns.Count - 1
                    gv3.Columns(ii).ReadOnly = True
                Next
                gv3.EnableFiltering = True
                SetGridFormat3()
                gv3.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormat3()
        gv3.AutoExpandGroups = True
        gv3.ShowGroupPanel = True
        gv3.ShowRowHeaderColumn = False
        gv3.AllowAddNewRow = False
        gv3.AllowDeleteRow = False
        gv3.EnableFiltering = True
        gv3.ShowFilteringRow = True

        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).BestFit()
            'gv3.Columns(ii).Width = 500
        Next
        gv3.Columns("SNo").Name = "SNo"
        gv3.Columns("SNo").IsVisible = True '

        gv3.Columns("Union Name").HeaderText = "Union Name"
        gv3.Columns("Union Name").Width = 200
        gv3.Columns("Union Name").IsVisible = True

        gv3.Columns("Fromdate").HeaderText = "From Date"
        gv3.Columns("Fromdate").Width = 100
        gv3.Columns("Fromdate").IsVisible = False

        gv3.Columns("Todate").HeaderText = "To Date"
        gv3.Columns("Todate").Width = 100
        gv3.Columns("Todate").IsVisible = False

        gv3.Columns("RegisteredDCS").HeaderText = "RegisteredDCS"
        gv3.Columns("RegisteredDCS").IsVisible = True

        gv3.Columns("DCSQTY1").HeaderText = "Qty"
        gv3.Columns("DCSQTY1").IsVisible = True
        gv3.Columns("DCSQTY1").FormatString = "{0:n3}"

        gv3.Columns("FATKG1").HeaderText = "FATKG"
        gv3.Columns("FATKG1").IsVisible = True
        gv3.Columns("FATKG1").FormatString = "{0:n3}"

        gv3.Columns("SNFKG1").HeaderText = "SNFKG"
        gv3.Columns("SNFKG1").IsVisible = True
        gv3.Columns("SNFKG1").FormatString = "{0:n3}"

        gv3.Columns("PDCS").HeaderText = "PDCS"
        gv3.Columns("PDCS").IsVisible = True

        gv3.Columns("DCSQTY2").HeaderText = "QTY"
        gv3.Columns("DCSQTY2").IsVisible = True
        gv3.Columns("DCSQTY2").FormatString = "{0:n3}"

        gv3.Columns("FATKG2").HeaderText = "FATKG"
        gv3.Columns("FATKG2").IsVisible = True
        gv3.Columns("FATKG2").FormatString = "{0:n3}"

        gv3.Columns("SNFKG2").HeaderText = "SNFKG"
        gv3.Columns("SNFKG2").IsVisible = True
        gv3.Columns("SNFKG2").FormatString = "{0:n3}"

        gv3.Columns("TotalDCS").HeaderText = "Total DCS"
        gv3.Columns("TotalDCS").IsVisible = True

        gv3.Columns("TotalQty").HeaderText = "Total Qty"
        gv3.Columns("TotalQty").IsVisible = True
        gv3.Columns("TotalQty").FormatString = "{0:n3}"

        gv3.Columns("TotalFatkg").HeaderText = "Total FATKG"
        gv3.Columns("TotalFatkg").IsVisible = True
        gv3.Columns("TotalFatkg").FormatString = "{0:n3}"

        gv3.Columns("Totalsnfkg").HeaderText = "Total SNFKG"
        gv3.Columns("Totalsnfkg").IsVisible = True
        gv3.Columns("Totalsnfkg").FormatString = "{0:n3}"

        gv3.Columns("Union_Contact_Person").HeaderText = "Nodal Officer"
        gv3.Columns("Union_Contact_Person").Width = 200
        gv3.Columns("Union_Contact_Person").IsVisible = True

        gv3.Columns("Union_Contact_PhoneNo").HeaderText = "Mobile No."
        gv3.Columns("Union_Contact_PhoneNo").Width = 200
        gv3.Columns("Union_Contact_PhoneNo").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Totalsnfkg", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("TotalFatkg", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("TotalQty", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("SNFKG2", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("FATKG2", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("DCSQTY1", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("SNFKG1", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("FATKG1", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("DCSQTY2", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv3.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub ExportGridgv1(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridgv2(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv2, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridgv3(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv3, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridgv4(ByVal exporter As EnumExportTo)
        Try
            If gv4.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv4, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv4, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
            ExportGridgv1(EnumExportTo.Excel)
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
            ExportGridgv2(EnumExportTo.Excel)
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage4.Name) = CompairStringResult.Equal Then
            ExportGridgv3(EnumExportTo.Excel)
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage5.Name) = CompairStringResult.Equal Then
            ExportGridgv4(EnumExportTo.Excel)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
            ExportGrid()
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
            ExportGrids()
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage4.Name) = CompairStringResult.Equal Then
            ExportGridss()
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage5.Name) = CompairStringResult.Equal Then
            ExportGridss4()
        End If
    End Sub

    Sub ExportGridss4()
        Try
            If gv4.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv4, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv4, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub ExportGridss()
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub ExportGrids()
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv2, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub ExportGrid()
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                ' arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        gv3.DataSource = Nothing
        gv4.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
            If rdbPosted.Checked Then
                printUnion()
            ElseIf rdbUnposted.Checked Then
                printUnionUNP()
            ElseIf rbdAllTrans.Checked Then
                printUnionAll()
            End If

        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
            If rdbPosted.Checked Then
                PrintRoute()
            ElseIf rdbUnposted.Checked Then
                PrintRouteUNP()
            ElseIf rbdAllTrans.Checked Then
                PrintRouteAll()
            End If

        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage4.Name) = CompairStringResult.Equal Then
            If rdbPosted.Checked Then
                PrintDCS()
            ElseIf rdbUnposted.Checked Then
                PrintDCSUNP()
            ElseIf rbdAllTrans.Checked Then
                PrintDCSAll()
            End If

        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage5.Name) = CompairStringResult.Equal Then
            ' Print7Days()
        End If
    End Sub

    Sub PrintDCSAll()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                'Dim status4 As String
                'Dim status5 As String
                'If rdbUnposted.Checked Then
                '    status4 = " and muh.Status= 0 "
                '    status5 = "and msh.Status= 0 "
                'Else
                '    status4 = " "
                '    status5 = " "
                'End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg

                FROM 
                    (SELECT 
                        COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END) as RegCount,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
					    ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
						(COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
						( ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
						SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
						COUNT(CASE WHEN dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
							mud.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                       	 left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mud.VLC_Code
	                     left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                         WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and muh.Status= 0 group by mud.VLC_Code

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
							msd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = msd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 group by  msd.VLC_Code

                        UNION ALL
                        select
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
							mcd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mcd.VLC_Code
	                    left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  GROUP BY mcd.VLC_Code
                       
                            union all

					   SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(Qty * mdd.FAT / 100) AS FATKG,
                            SUM(Qty * SNF / 100) AS SNFKG,
							mdd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
							left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mdd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  Group By mdd.VLC_Code
                            

							union all

							select sum(Qty)Milk_Weight,sum(FAT_KG) AS FATKG,sum(SNF_KG) AS SNFKG,
                        msh.VSP_Code,max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount

                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                            left join 
                             [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VENDOR_MASTER vm on vm.Vendor_Code = msh.VSP_Code
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'   group by msh.VSP_Code
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementDCSWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Sub PrintDCSUNP()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                Dim status4 As String
                Dim status5 As String
                If rdbUnposted.Checked Then
                    status4 = " and muh.Status= 0 "
                    status5 = "and msh.Status= 0 "
                Else
                    status4 = " "
                    status5 = " "
                End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg

                FROM 
                    (SELECT 
                        COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END) as RegCount,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
					    ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
						ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
						(COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
						( ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
						(ISNULL(SUM(CASE WHEN dcs = 'REGISTERED' OR dcs IS NULL OR dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
						SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
						COUNT(CASE WHEN dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
						COUNT(CASE WHEN dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
							mud.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                       	 left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mud.VLC_Code
	                     left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                         WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + " group by mud.VLC_Code

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
							msd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = msd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + " group by  msd.VLC_Code

                        UNION ALL
                        select
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
							mcd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mcd.VLC_Code
	                    left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                            WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  GROUP BY mcd.VLC_Code
                        
                            union all

					   SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(Qty * mdd.FAT / 100) AS FATKG,
                            SUM(Qty * SNF / 100) AS SNFKG,
							mdd.VLC_Code,
							max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
							left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD vmh on vmh.VLC_Code = mdd.VLC_Code
	                        left join 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_VENDOR_MASTER vm on vm.Vendor_Code = vmh.VSP_Code
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  Group By mdd.VLC_Code
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementDCSWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Sub PrintRouteAll()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                'Dim status4 As String
                'Dim status5 As String
                'If rdbUnposted.Checked Then
                '    status4 = " and muh.Status= 0 "
                '    status5 = "and msh.Status= 0 "
                'Else
                '    status4 = " "
                '    status5 = " "
                'End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.routecount), 0) AS RouteCount,
					ISNULL(SUM(Dis_Procurement.mcccount), 0) AS MCCCount,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc

                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
                        	count(distinct Bulk_Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and muh.Status= 0 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
                            count(distinct Bulk_Route_No) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            0 as Route,
							0 as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            count(distinct Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            UNION ALL

                            SELECT 
                                     SUM(Qty) AS Milk_Weight,
                                    SUM(FAT_KG) AS FATKg,
                                    SUM(SNF_KG) AS SNFKG,
                        	        count(distinct ROUTE_CODE ) as Route,
							        count(distinct msd.MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN  '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                        
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementRouteWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub
    Sub PrintRouteUNP()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                Dim status4 As String
                Dim status5 As String
                If rdbUnposted.Checked Then
                    status4 = " and muh.Status= 0 "
                    status5 = "and msh.Status= 0 "
                Else
                    status4 = " "
                    status5 = " "
                End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.routecount), 0) AS RouteCount,
					ISNULL(SUM(Dis_Procurement.mcccount), 0) AS MCCCount,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc

                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG,
                        	count(distinct Bulk_Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG,
                            count(distinct Bulk_Route_No) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            0 as Route,
							0 as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                       
                            union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG,
                            count(distinct Route_Code) as Route,
							count(distinct MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2  "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementRouteWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub
    Sub printUnionAll()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                'Dim status4 As String
                'Dim status5 As String
                'If rdbUnposted.Checked Then
                '    status4 = " and muh.Status= 0 "
                '    status5 = "and msh.Status= 0 "
                'Else
                '    status4 = " "
                '    status5 = " "
                'End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                           and muh.Status= 0 

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            and msh.Status= 0 

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                       
                             union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            

                              UNION ALL

                            SELECT 
                                    SUM(Qty) AS Milk_Weight,
                                    SUM(FAT_KG) AS FATKg,
                                    SUM(SNF_KG) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2   "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementUnionWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub
    Sub printUnionUNP()
        Dim qry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If
        Dim docNo As String = ""
        qry = " 
            SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        '          If chkRJSBNS.Checked Then
        '              query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(qry)
        qry = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    qry += " UNION ALL "
                End If
                Dim status4 As String
                Dim status5 As String
                If rdbUnposted.Checked Then
                    status4 = " and muh.Status= 0 "
                    status5 = "and msh.Status= 0 "
                Else
                    status4 = " "
                    status5 = " "
                End If
                qry += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                FROM 
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        
                             union all

					   SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL mdd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS md ON md.Document_No = mdd.Document_No
                        WHERE 
                           md.Status = 0
                           AND
                            CONVERT(DATE, md.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2   "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementUnionWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Sub PrintDCS()
        Dim query As String = ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If

        Dim docNo As String = ""
        query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        If chkRJSBNS.Checked Then
            query += "union all
        SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        union all
        SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        ORDER BY Location_Name"
        End If
        dt = clsDBFuncationality.GetDataTable(query)
        query = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If

                query += " select final.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.RegCount), 0) AS RegisteredDCS ,   ISNULL(SUM(Dis_Procurement.DCS_1_QTY), 0) AS DCSQTY1, 
                    ISNULL(SUM(Dis_Procurement.DCS_1_FATKG), 0) AS FATKG1,ISNULL(SUM(Dis_Procurement.DCS_1_SNFKG), 0) AS SNFKG1,
					ISNULL(SUM(Dis_Procurement.PDCSCount), 0) AS PDCS,ISNULL(SUM(Dis_Procurement.DCS_2_QTY), 0) AS DCSQTY2,
					ISNULL(SUM(Dis_Procurement.DCS_2_FATKG), 0) AS FATKG2,ISNULL(SUM(Dis_Procurement.DCS_2_SNFKG), 0) AS SNFKG2,
					ISNULL(SUM(Dis_Procurement.DCSCount), 0) AS TotalDCS,ISNULL(SUM(Dis_Procurement.QTY1), 0) AS TotalQty,
					ISNULL(SUM(Dis_Procurement.TotalFatkg), 0) AS TotalFatkg,ISNULL(SUM(Dis_Procurement.Totalsnfkg), 0) AS Totalsnfkg
                                 FROM 
(SELECT  
                        COUNT(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN 1 ELSE NULL END) as RegCount,
   ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN Milk_Weight ELSE 0 END), 0) AS DCS_1_QTY,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN FATKG ELSE 0 END), 0) AS DCS_1_FATKG,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN SNFKG ELSE 0 END), 0) AS DCS_1_SNFKG,
	COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCount,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0) AS DCS_2_QTY,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN FATKG ELSE 0 END), 0) AS DCS_2_FATKG,
    ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0) AS DCS_2_SNFKG,
	(COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END)+COUNT(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN 1 ELSE NULL END)) AS DCSCount,
	( ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN Milk_Weight ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN Milk_Weight ELSE 0 END), 0)) as QTY1,
	(ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN FATKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs='PDCS' THEN FATKG ELSE 0 END), 0)) as TotalFATKG,
	(ISNULL(SUM(CASE WHEN xxx.dcs = 'REGISTERED' OR xxx.dcs IS NULL OR xxx.dcs = '' THEN  SNFKG ELSE 0 END), 0)+ISNULL(SUM(CASE WHEN xxx.dcs = 'PDCS' THEN SNFKG ELSE 0 END), 0)) as TotalSNFKG,
		sum(xxx.Milk_Weight)Milk_Weight,sum(xxx.FATKG)FATKG,sum(xxx.SNFKG)SNFKG,count(xxx.dcs)dcs,
	COUNT(CASE WHEN xxx.dcs = 'REGISTERED' THEN 1 ELSE NULL END ) as Regcounts,
	COUNT(CASE WHEN xxx.dcs = 'PDCS' THEN 1 ELSE NULL END) as PDCSCsount
		
                    FROM (
                     select sum(Qty)Milk_Weight,sum(FAT_PER)FAT,sum(SNF_PER)SNF,sum(FAT_KG) AS FATKG,sum(SNF_KG) AS SNFKG,
                        msh.VSP_Code,max(vm.Registered_PDCS_CLUSTER)dcs,max(VM.Registered_PDCS_CLUSTER)dcscount

                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                            left join 
                             [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VENDOR_MASTER vm on vm.Vendor_Code = msh.VSP_Code
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' group by msh.VSP_Code
                        ) AS xxx
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementDCSWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub
    Sub PrintRoute()
        Dim query As String = ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If

        Dim docNo As String = ""
        query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        If chkRJSBNS.Checked Then
            query += "union all
        SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        union all
        SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        ORDER BY Location_Name"
        End If
        dt = clsDBFuncationality.GetDataTable(query)
        query = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If

                query += " select final.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(sum(Dis_Procurement.RouteCount),0) AS RouteCount,ISNULL(sum(Dis_Procurement.MCCCount),0) AS MCCCount,ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc
                                 FROM 
(SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        sum(DISTINCT CASE WHEN Route IS NOT NULL AND Route <> 0 THEN Route END) AS RouteCount,
						sum(DISTINCT CASE WHEN MCC IS NOT NULL AND MCC <> 0 THEN MCC END) AS MCCCount
                    FROM (
                        SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(FAT_KG) AS FATKg,
                            SUM(SNF_KG) AS SNFKG,
                        	count(distinct ROUTE_CODE ) as Route,
							count(distinct msd.MCC_CODE) as MCC
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementRouteWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Sub printUnion()
        Dim query As String = ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If

        Dim docNo As String = ""
        query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        If chkRJSBNS.Checked Then
            query += "union all
        SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        union all
        SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        ORDER BY Location_Name"
        End If
        dt = clsDBFuncationality.GetDataTable(query)
        query = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If

                query += " select FINAL.*,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(AVG(Dis_Procurement.FATPerProc), 0) AS FATPerProc,
					ISNULL(AVG(Dis_Procurement.SNFPerProc), 0) AS SNFPerProc
                                 FROM 
(SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(FATKg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc,
                        AVG(CASE WHEN Milk_Weight <> 0 THEN (FATKg) * 100 / (Milk_Weight)
            ELSE 0 END) AS FATPerProc,
			AVG(CASE WHEN Milk_Weight <> 0 THEN (SNFKG) * 100 / (Milk_Weight)
            ELSE 0 END) AS SNFPerProc
                    FROM (
                        SELECT 
                            SUM(Qty) AS Milk_Weight,
                            SUM(FAT_KG) AS FATKg,
                            SUM(SNF_KG) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD msh ON msh.DOC_CODE = msd.DOC_CODE
                        WHERE 
                            CONVERT(DATE, msh.DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement)final left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2=2 "
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptMilkProcurementUnionWise", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

End Class