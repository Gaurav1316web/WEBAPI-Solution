Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Public Class frmDBTStatusAndLastDPTStatus
    Inherits FrmMainTranScreen

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub rdbDBTStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDBTStatus.CheckedChanged

    End Sub


    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try

            fillGridReport(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.Visible = True
                gv1.DataSource = dt2
                gv1.ReadOnly = True
                'SetGridFormat(gv1)
                'ReStoreGridLayout()
                'If objCommonVar.RCDFCFP = False Then
                '    If rdbMilkProcurement.Checked = False AndAlso rdbDBTStatus.Checked = False AndAlso rdbLastDBTStatus.Checked = False Then
                '        GridFormate()
                '    End If
                'End If
                'If isPrint Then
                '    Dim frmCRV As New frmCrystalReportViewer()
                '    If objCommonVar.RCDFCFP Then
                '        frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "rptERPStatusTrackingReport", Label1.Text)
                '    Else
                '        If rdbERPStatusMilkUnion.Checked Then
                '            frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "rptERPStatusTrackingReportUnion", Label1.Text)
                '        End If
                '    End If
                '    frmCRV = Nothing
                'End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class