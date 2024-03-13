Imports System.IO
Imports common
Public Class FrmERPStatusTrackingReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmERPStatusTrackingReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReport.Enabled Then
            fillGridReport(False)
            chkDBT.Visible = False
            chkDBT.Checked = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            'reset()
            'chkDBT.Visible = False
            'chkDBT.Checked = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
            chkDBT.Visible = False
            chkDBT.Checked = False
        ElseIf e.Control AndAlso e.Shift AndAlso e.Alt AndAlso e.KeyCode = Keys.F12 Then
            If chkDBT.Visible = True Then
                chkDBT.Visible = False
                chkDBT.Checked = False
            Else
                chkDBT.Visible = True
            End If
        End If
    End Sub
    Private Sub FrmERPStatusTrackingReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If objCommonVar.RCDFCFP = True Then
            Label1.Text = "ERP Status At Cattle Feed Plants"
            SplitContainer3.Panel1Collapsed = True
        Else
            Label1.Text = "ERP Status At Milk Unions"
            rdbERPStatusMilkUnion.Visible = True
            rdbMilkProcurement.Visible = True
            rdbDBTStatus.Visible = True
            rdbLastDBTStatus.Visible = True
            txtFinYr.Visible = False
            MyLabel1.Visible = False
            txtDate.Visible = False
            RadLabel3.Visible = False
            txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            rdbDBTStatus.Location = New System.Drawing.Point(292, 4)
            rdbLastDBTStatus.Location = New System.Drawing.Point(414, 5)
            MyLabel1.Location = New System.Drawing.Point(414, 5)
            txtFinYr.Location = New System.Drawing.Point(489, 5)

        End If
        chkDBT.Checked = False
        chkDBT.Visible = False
    End Sub
    Public Sub fillGridReport(ByVal isPrint As Boolean)
        Try
            If objCommonVar.RCDFCFP = True Then
                PageSetupReport_ID = Me.Form_ID + "CFP"
            Else
                PageSetupReport_ID = Me.Form_ID + "D"
            End If
            TemplateGridview = gv1

            Dim query As String
            gv1.DataSource = Nothing
            If objCommonVar.RCDFCFP = True Then
                query = "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
                ,TSPL_LOCATION_MASTER.location_code as [Location Code] ,TSPL_LOCATION_MASTER.Location_Desc AS [Location Name]
                ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location]
                ,convert(varchar, GRN.GRN_Date,103) AS [Last Gate In Date]
                ,convert(varchar, WEIGHTMENT.Weighment_Date,103) as [Last Weighment Date]
                ,convert(varchar,QC.Document_Date,103) as [Last QC Date]
                ,convert(varchar,SRN.SRN_Date,103) as [Last SRN Date]
                ,convert(varchar, PInvoice.PI_Date,103) as [Last Purchase Invoice Date]
                ,convert(varchar, RECEIPT.Receipt_Date,103) as [Last Advance Receipt Date]
                ,convert(varchar, SHIPMENT.Document_Date,103) as [Last Dispatch Date]
                ,convert(varchar, SInvoice.Document_Date,103) as [Last Sale Bill Date]
                ,convert(varchar, Production.PROD_DATE,103) as [Last Production Entry Date]
                FROM TSPL_LOCATION_MASTER LEFT OUTER JOIN
                (SELECT  max(TSPL_GRN_HEAD.GRN_Date) AS GRN_Date,TSPL_GRN_HEAD.Bill_To_Location FROM TSPL_GRN_HEAD
                GROUP BY TSPL_GRN_HEAD.Bill_To_Location) GRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =GRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date) AS Weighment_Date,TSPL_PO_WEIGHTMENT_HEAD.Location_code FROM TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Type='IN'
                GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_code) WEIGHTMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =WEIGHTMENT.Location_code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_QC_CHECK_HEAD.Document_Date) AS Document_Date,TSPL_QC_CHECK_HEAD.Bill_To_Location FROM TSPL_QC_CHECK_HEAD
                GROUP BY TSPL_QC_CHECK_HEAD.Bill_To_Location) QC
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =QC.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SRN_HEAD.SRN_Date) AS SRN_Date,TSPL_SRN_HEAD.Bill_To_Location FROM TSPL_SRN_HEAD
                GROUP BY TSPL_SRN_HEAD.Bill_To_Location) SRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PI_HEAD.PI_Date) AS PI_Date,TSPL_PI_HEAD.Bill_To_Location FROM TSPL_PI_HEAD
                GROUP BY TSPL_PI_HEAD.Bill_To_Location) PInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =PInvoice.Bill_To_Location
                 LEFT OUTER JOIN
                (SELECT  max(TSPL_RECEIPT_HEADER.Receipt_Date) AS Receipt_Date,TSPL_RECEIPT_HEADER.Location_GL_Code FROM TSPL_RECEIPT_HEADER
                GROUP BY TSPL_RECEIPT_HEADER.Location_GL_Code) RECEIPT
                ON TSPL_LOCATION_MASTER.Loc_Segment_Code =RECEIPT.Location_GL_Code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SD_SHIPMENT_HEAD.Document_Date) AS Document_Date,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM TSPL_SD_SHIPMENT_HEAD
                GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SHIPMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SHIPMENT.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(tspl_sd_sale_invoice_head.Document_Date) AS Document_Date,tspl_sd_sale_invoice_head.Bill_To_Location FROM tspl_sd_sale_invoice_head
                GROUP BY tspl_sd_sale_invoice_head.Bill_To_Location) SInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SInvoice.Bill_To_Location
                LEFT OUTER JOIN
                (select max(TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE) as PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY
                group by TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE)Production
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =Production.LOCATION_CODE
                where TSPL_LOCATION_MASTER.IsMainPlant='0'"
            Else
                If rdbMilkProcurement.Checked Then
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                    If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                        gv1.DataSource = Nothing
                        Exit Sub
                    End If

                    Dim frmDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")

                    Dim dtr As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_APP_LOCATION].Location_Name")
                    query = ""
                    For ii As Integer = 0 To dtr.Rows.Count - 1
                        If ii > 0 Then
                            query += " UNION ALL "
                        End If
                        query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                        query += ", ( (select ISNULL(SUM(Qty),0) from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
where [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Status = 1 and convert(date, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS.Document_Date, 103) = convert(date ,'" + frmDate + "' , 103))  + (select ISNULL(SUM(Milk_Weight),0) from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL
left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
where [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Status = 1 and convert(date, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date, 103) = convert(date ,'" + frmDate + "' , 103) ) + (select ISNULL(SUM(Milk_Weight),0) from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
where [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Status = 1 and convert(date, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date, 103) = convert(date ,'" + frmDate + "' , 103))  
) as [Milk Collection in Ltr]"
                        query += ",(select isnull(SUM(TotalLtr_ItemWise),0)  from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL
 left outer join [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER on [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No
 where [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Posted = 1 and convert(date,  [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date , 103) = convert(date ,'" + frmDate + "' , 103)) as [Demand in Ltr]"
                        query += ",(select COUNT(PurchaseOrder_No) from [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1 and convert(date, PurchaseOrder_Date , 103)=convert(date ,'" + frmDate + "' , 103)) as [Purchase Order]"

                    Next
                ElseIf rdbDBTStatus.Checked Then
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                    If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                        gv1.DataSource = Nothing
                        Exit Sub
                    End If

                    Dim frmDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,Start_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))
                    Dim toDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,End_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))

                    Dim dtr As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_APP_LOCATION].Location_Name")
                    query = ""
                    Dim status As String = ""
                    If rbtnTransactionPosted.Checked Then

                        status = "AND Status = 1"
                    Else
                        status = ""
                    End If
                    For ii As Integer = 0 To dtr.Rows.Count - 1
                        If ii > 0 Then
                            query += " UNION ALL "
                        End If
                        query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 4 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As April"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 5 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As May"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 6 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As June"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 7 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As July"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 8 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As August"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 9 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As September"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 10 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As October"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 11 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As November"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 12 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As December"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 1 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As January"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 2 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As February"
                        query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 3 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As March"
                    Next
                ElseIf rdbLastDBTStatus.Checked Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                    If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                        gv1.DataSource = Nothing
                        Exit Sub
                    End If

                    Dim docNo As String = ""
                    query = ""
                    dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            If ii > 0 Then
                                query += " UNION ALL "
                            End If

                            Dim qry As String = "SELECT Top 1 Document_Code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where 2=2 "
                            If rbtnTransactionPosted.Checked Then
                                qry += " AND Status = 1"
                            End If
                            qry += " ORDER BY Document_date DESC"
                            docNo = clsDBFuncationality.getSingleValue(qry)

                            query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],COUNT([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code) AS [No Of Farmer],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Jan_Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Jan Aadhar Verified No],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Addhar Verified],
    CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, 103) + ' - ' + CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date, 103) AS [Last DBT Cycle]
    from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code= '" & docNo & "' group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date"

                        Next
                    End If
                Else
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                    If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                        gv1.DataSource = Nothing
                        Exit Sub
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_ERP_STATUS].Location_Name,[TSPL_ERP_STATUS].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_ERP_STATUS] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_ERP_STATUS].Location_Name")
                    query = ""

                    Dim Status1 As String = "  "
                    Dim PostedY As String = "   "
                    Dim POSTED1 As String = "  "
                    If rbtnTransactionPosted.Checked Then
                        Status1 = " and Status = 1 "
                        PostedY = " and Posted ='Y' "
                        POSTED1 = " and POSTED = 1 "
                    End If

                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii > 0 Then
                            query += " UNION ALL "
                        End If
                        query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                        query += ",(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date]"
                        query += ",(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SRN_HEAD where 2=2  " + Status1 + " ) as [Last Stock Received (SRN) Date]"
                        query += ",(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' " + Status1 + " ) as [Last Stock Issue Date]"
                        query += ",(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ADJUSTMENT_HEADER where 2=2 " + PostedY + " ) as [Last Stock Adjustment Date]"
                        query += ",(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY where 2=2 " + POSTED1 + ") as [Last Production Entry Date]"
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER where 2=2 " + POSTED1 + "  ) as [Last Demand Date]"
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "RJS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BNS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BRN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "JHL") = CompairStringResult.Equal Then
                            query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale where 2=2 " + POSTED1 + "  ) as [Last Dispatch Date]"
                        Else
                            query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD where 2=2 " + Status1 + "  ) as [Last Dispatch Date]"
                        End If
                        query += ",(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PI_head where 2=2 " + Status1 + ") as [Last Stock Voucher Date]"
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD where 2=2 " + Status1 + " ) as [Last Sales Voucher Date]"
                        query += ",(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_RECEIPT_HEADER where 2=2 " + PostedY + " ) as [Last Receipt Date]"
                        If chkDBT.Checked Then
                            query += ",(select FORMAT(max(Document_Date),'MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD) as [Last DBT Entry]
                    ,(select FORMAT(max(Document_Date),'MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT) as [Last DBT Advice]
                    ,(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_MCC) as [Last BMC Truck Sheet]
                    ,(SELECT FORMAT(max(Document_Date),'dd/MMM/yyyy') FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS) as [Last DCS Truck Sheet Date]
                    ,(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD) as [Last Milk Bill Generation Date]"
                        End If
                    Next
                End If
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.Visible = True
                gv1.DataSource = dt2
                gv1.ReadOnly = True
                SetGridFormat(gv1)
                ReStoreGridLayout()
                If objCommonVar.RCDFCFP = False Then
                    If rdbMilkProcurement.Checked = False AndAlso rdbDBTStatus.Checked = False AndAlso rdbLastDBTStatus.Checked = False Then
                        GridFormate()
                    End If
                End If
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If objCommonVar.RCDFCFP Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "rptERPStatusTrackingReport", Label1.Text)
                    Else
                        If rdbERPStatusMilkUnion.Checked Then
                            frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "rptERPStatusTrackingReportUnion", Label1.Text)
                        End If
                    End If
                    frmCRV = Nothing
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub GridFormate()
        If objCommonVar.RCDFCFP = False Then
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(" "))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Purchase & Store"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Indent Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Purchase order Date").Name)
                'view.ColumnGroups.Add(New GridViewColumnGroup("Store"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Received (SRN) Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Issue Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Adjustment Date").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Last Production Entry Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Sales & Marketing"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Last Demand Date").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Last Dispatch Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Accounts"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Voucher Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Sales Voucher Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Receipt Date").Name)
                If chkDBT.Checked Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("DBT Status"))
                    view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DBT Entry").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DBT Advice").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last BMC Truck Sheet").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DCS Truck Sheet Date").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last Milk Bill Generation Date").Name)
                End If
                gv1.ViewDefinition = view
            End If
        End If
    End Sub
    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Milk Collection in Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Demand in Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Purchase Order", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        'gv1.Visible = False
        'txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")

    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try

            fillGridReport(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
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
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1
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
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmERPStatusTrackingReport & "'"))
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
            arrHeader.Add("User : " + objCommonVar.CurrentUser)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Label1.Text, , arrHeader)
            Else
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF(Label1.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Dim doc As New RadPrintDocument()
                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 100
                doc.Landscape = True
                doc.LeftFooter = "Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")
                doc.RightFooter = "User : " + objCommonVar.CurrentUser
                doc.AssociatedObject = gv1
                Dim strHeader As String = Label1.Text 'Me.Text.Replace("/", "")
                doc.MiddleHeader = strHeader
                doc.HeaderFont = New Font("Verdana", 12, FontStyle.Bold)
                'doc.Print()
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub chkDBT_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDBT.CheckStateChanged
        If chkDBT.Checked Then
            Label1.Text = "Milk Procurement & DBT Status at Milk Unions"
        Else
            If objCommonVar.RCDFCFP = True Then
                Label1.Text = "ERP Status At Cattle Feed Plants"
            Else
                Label1.Text = "ERP Status At Milk Unions"
            End If
        End If
    End Sub
    Private Sub rdbMilkProcurement_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMilkProcurement.CheckedChanged
        If rdbMilkProcurement.Checked Then
            txtDate.Visible = True
            RadLabel3.Visible = True
            Label1.Text = "Milk Procurement"
            RadLabel3.Location = New System.Drawing.Point(292, 4)
            rdbDBTStatus.Location = New System.Drawing.Point(425, 5)
            rdbLastDBTStatus.Location = New Point(525, 5)
            MyLabel1.Location = New System.Drawing.Point(500, 5)
            txtFinYr.Location = New System.Drawing.Point(574, 3)
        Else
            rdbDBTStatus.Location = New System.Drawing.Point(292, 4)
            rdbLastDBTStatus.Location = New Point(414, 5)
            MyLabel1.Location = New System.Drawing.Point(392, 5)
            txtFinYr.Location = New System.Drawing.Point(478, 5)
            txtDate.Visible = False
            RadLabel3.Visible = False
            Label1.Text = "ERP Status At Milk Unions"
        End If
    End Sub

    Private Sub RMIALL_Click(sender As Object, e As EventArgs) Handles RMIALL.Click
        'Try
        '    GetAllRecord(False)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            fillGridReport(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '    Sub Print()
    '        Try
    '            Dim StrQuery As String = " select 1 AS SNo,'ALWAR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [ALW].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 2 AS SNo,'BANSWARA' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_Dispatch_BulkSale where Posted=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [BNS].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 3 AS SNo,'BARAN' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [BRN].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 4 AS SNo,'BARMER' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [BAR].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 5 AS SNo,'BHARATPUR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [BHR].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " select 6 AS SNo,'BIKANER' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [BKN].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 7 AS SNo,'CHITTORGARH' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [CHT].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date] 
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 8 AS SNo,'CHURU' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [CHU].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "   select 9 AS SNo,'GANGANAGAR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [GNG].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all  " & Environment.NewLine & "  select 10 AS SNo,'JAIPUR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [JPR].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date] 
    '" & Environment.NewLine & " union all  " & Environment.NewLine & " select 11 AS SNo,'JAISALMER' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [JSL].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "   select 12 AS SNo,'JALORE' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [JAL].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 13 AS SNo,'JHALAWAR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [JHL].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 14 AS SNo,'JODHPUR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [JDH].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 15 AS SNo,'KOTA' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [KTA].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 16 AS SNo,'NAGORE' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [NAG].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date] 
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 17 AS SNo,'PALI' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [PLI].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date] 
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 18 AS SNo,'RAJSAMAND' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_Dispatch_BulkSale where Posted=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [RJS].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 19 AS SNo,'SAWAI MADHOPUR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [SWM].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 20 AS SNo,'SIKAR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [SKR].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "  select 21 AS SNo,'TONK' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [TNK].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]
    '" & Environment.NewLine & " union all " & Environment.NewLine & "   select 22 AS SNo,'UDAIPUR' AS [Union Name],(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date],(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date],(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date],
    '(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date],(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date],
    '(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date],(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date],(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [UDP].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date] "
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptERPStatusTrackingReportUnionrpt", "ERP Status Tracking Report")
    '            frmCRV = Nothing
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            Dim arrItem As New List(Of String)
            Dim item As String = Nothing
            Dim ERPStatusreport As String = "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
                ,TSPL_LOCATION_MASTER.location_code as [Location Code] ,TSPL_LOCATION_MASTER.Location_Desc AS [Location Name]
                ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location]
                ,convert(varchar, GRN.GRN_Date,103) AS [Last Gate In Date]
                ,convert(varchar, WEIGHTMENT.Weighment_Date,103) as [Last Weighment Date]
                ,convert(varchar,QC.Document_Date,103) as [Last QC Date]
                ,convert(varchar,SRN.SRN_Date,103) as [Last SRN Date]
                ,convert(varchar, PInvoice.PI_Date,103) as [Last Purchase Invoice Date]
                ,convert(varchar, RECEIPT.Receipt_Date,103) as [Last Advance Receipt Date]
                ,convert(varchar, SHIPMENT.Document_Date,103) as [Last Dispatch Date]
                ,convert(varchar, SInvoice.Document_Date,103) as [Last Sale Bill Date]
                ,convert(varchar, Production.PROD_DATE,103) as [Last Production Entry Date]
                FROM TSPL_LOCATION_MASTER LEFT OUTER JOIN
                (SELECT  max(TSPL_GRN_HEAD.GRN_Date) AS GRN_Date,TSPL_GRN_HEAD.Bill_To_Location FROM TSPL_GRN_HEAD
                GROUP BY TSPL_GRN_HEAD.Bill_To_Location) GRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =GRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date) AS Weighment_Date,TSPL_PO_WEIGHTMENT_HEAD.Location_code FROM TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Type='IN'
                GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_code) WEIGHTMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =WEIGHTMENT.Location_code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_QC_CHECK_HEAD.Document_Date) AS Document_Date,TSPL_QC_CHECK_HEAD.Bill_To_Location FROM TSPL_QC_CHECK_HEAD
                GROUP BY TSPL_QC_CHECK_HEAD.Bill_To_Location) QC
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =QC.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SRN_HEAD.SRN_Date) AS SRN_Date,TSPL_SRN_HEAD.Bill_To_Location FROM TSPL_SRN_HEAD
                GROUP BY TSPL_SRN_HEAD.Bill_To_Location) SRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PI_HEAD.PI_Date) AS PI_Date,TSPL_PI_HEAD.Bill_To_Location FROM TSPL_PI_HEAD
                GROUP BY TSPL_PI_HEAD.Bill_To_Location) PInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =PInvoice.Bill_To_Location
                 LEFT OUTER JOIN
                (SELECT  max(TSPL_RECEIPT_HEADER.Receipt_Date) AS Receipt_Date,TSPL_RECEIPT_HEADER.Location_GL_Code FROM TSPL_RECEIPT_HEADER
                GROUP BY TSPL_RECEIPT_HEADER.Location_GL_Code) RECEIPT
                ON TSPL_LOCATION_MASTER.Loc_Segment_Code =RECEIPT.Location_GL_Code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SD_SHIPMENT_HEAD.Document_Date) AS Document_Date,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM TSPL_SD_SHIPMENT_HEAD
                GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SHIPMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SHIPMENT.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(tspl_sd_sale_invoice_head.Document_Date) AS Document_Date,tspl_sd_sale_invoice_head.Bill_To_Location FROM tspl_sd_sale_invoice_head
                GROUP BY tspl_sd_sale_invoice_head.Bill_To_Location) SInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SInvoice.Bill_To_Location
                LEFT OUTER JOIN
                (select max(TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE) as PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY
                group by TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE)Production
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =Production.LOCATION_CODE
                where TSPL_LOCATION_MASTER.IsMainPlant='0'"


            Dim dtERPStatusreport As DataTable = clsDBFuncationality.GetDataTable(ERPStatusreport)

            If dtERPStatusreport IsNot Nothing And dtERPStatusreport.Rows.Count > 0 Then
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funreport(CrystalReportFolder.PRODUCTION, dtSaleConsignee, "rptRMUnloadingReport", "")
                'frmCRV = Nothing
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dtERPStatusreport, "rptERPStatusTrackingReport", "")
                'PDFPath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.MilkProcurement, dt, dtAdditionFinance, "crptMilkPurchaseBillPaymentProcessNewJPR", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeductionFinance, "subReduceDeduction.rpt", dtReduceDeduction, "subSaving.rpt", dtSaving, "SubAdditionOther.rpt", dtAdditionOther, "SubDeductionOther.rpt", dtDeductionOther)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rdbDBTStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDBTStatus.CheckedChanged
        If rdbDBTStatus.Checked Then
            txtFinYr.Visible = True
            MyLabel1.Visible = True
            Label1.Text = "DBT Status Report At Milk Unions"
            rdbLastDBTStatus.Location = New Point(645, 5)
            txtFinYr.Value = clsDBFuncationality.getSingleValue("select Fiscal_Code as Code from TSPL_Fiscal_Year_Master WHERE Is_Current_Year = 1")
        Else
            txtFinYr.Visible = False
            MyLabel1.Visible = False
            Label1.Text = "ERP Status At Milk Unions"
            rdbLastDBTStatus.Location = New Point(414, 5)
        End If
    End Sub
    Private Sub txtFinYr__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYr._MYValidating
        Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
        txtFinYr.Value = clsCommon.ShowSelectForm("fndFinancialYearMaster", qry, "Code", "", txtFinYr.Value, "Code", isButtonClicked)
    End Sub

    Private Sub rdbLastDBTStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdbLastDBTStatus.CheckedChanged
        If rdbLastDBTStatus.Checked Then
            Label1.Text = "Last DBT Status Report"
        Else
            Label1.Text = "ERP Status At Milk Unions"
        End If
    End Sub
End Class
