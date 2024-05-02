Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO


Public Class QualitySummaryReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'TxtMultiLocation.arrValueMember = Nothing
        txttBillToLocation.Value = Nothing
        lablBillToLocation.Text = ""
        TxttRAL.arrValueMember = Nothing
        Gvv1.DataSource = Nothing
    End Sub

    Private Sub btnnclose_Click(sender As Object, e As EventArgs) Handles btnnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnnReset_Click(sender As Object, e As EventArgs) Handles BtnnReset.Click
        Reset()
    End Sub

    Private Sub btnnGo_Click(sender As Object, e As EventArgs) Handles btnnGo.Click
        Load_Quality_Summary_Report()
    End Sub

    Private Sub Load_Quality_Summary_Report()

        Dim qry As String = ""
        Dim dt As New DataTable()
        Try

            Dim whr As String = ""
            If TxttRAL.arrValueMember IsNot Nothing AndAlso TxttRAL.arrValueMember.Count > 0 Then
                whr += "  and TSPL_GRN_HEAD.Ref_No  In  (" + clsCommon.GetMulcallString(TxttRAL.arrValueMember) + ")  "
            End If

            qry = " select ROW_NUMBER() OVER (ORDER BY TSPL_GRN_HEAD.Bill_To_Location) AS 'S.NO','RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' AS HeadName,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add4,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',
                count((CASE WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 THEN 'FULL_REJECT'  end)) AS 'FULL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'PARTIAL_REJECT',
                count((case when TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 OR TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'TOTALL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=1 AND (TSPL_GRN_HEAD.VisualQCStatusSecond<>3 OR TSPL_GRN_HEAD.VisualQCStatusSecond<>2 or TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected')then 'Ok'  end)) AS 'TOTAL_ACCEPTED',
				count(case when deduction>0 and TSPL_QC_CHECK_HEAD.QC_Status='Under Deviation' then 'Accepted With Deduction' end) as 'Accepted With Deduction',
				count(case when deduction=0 and TSPL_QC_CHECK_HEAD.QC_Status='Accepted' then 'Accepted Without Deduction' end) as 'Accepted Without Deduction',
				count(case when deduction>=0 and TSPL_QC_CHECK_HEAD.QC_Status IN ('Accepted','Under Deviation') then 'Total Wet QC' end) as 'Total Wet QC',
				count(case when  TSPL_QC_CHECK_HEAD.QC_Status='Rejected'then 'Rejected Wet QC' end) as 'Rejected Wet QC'
                from  TSPL_GRN_HEAD
                left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left OUTER join TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location
				left outer join (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_sRN_DETAIL 
				group by Document_Code) as TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
                where 2=2  
				AND TSPL_ITEM_MASTER.structure_Code IN ('RM','PM')
                and CONVERT(DATE,TSPL_GRN_HEAD.GRN_Date,103)>='01/APR/2023'  
				And TSPL_GRN_HEAD.Bill_To_Location = ('" + clsCommon.myCstr(txttBillToLocation.Value) + "') " + whr + " 
				GROUP BY TSPL_LOCATION_MASTER.Add4,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description
				order by TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No desc"


            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gvv1.DataSource = Nothing
                Gvv1.GroupDescriptors.Clear()
                Gvv1.SummaryRowsBottom.Clear()
                Gvv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageVieww1.SelectedPage = RadPageViewPage2
                Gvv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "rptQualitySummaryReport")
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub FormatGrid()

        Gvv1.TableElement.TableHeaderHeight = 40
        Gvv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gvv1.Columns.Count - 1
            Gvv1.Columns(ii).ReadOnly = True
            Gvv1.Columns(ii).IsVisible = False
        Next

        Gvv1.Columns("S.NO").Width = 50
        Gvv1.Columns("S.NO").IsVisible = True
        Gvv1.Columns("S.NO").HeaderText = "S No"

        Gvv1.Columns("Ref_No").Width = 150
        Gvv1.Columns("Ref_No").IsVisible = True
        Gvv1.Columns("Ref_No").HeaderText = "RAL NO"

        Gvv1.Columns("Item_Code").Width = 100
        Gvv1.Columns("Item_Code").IsVisible = True
        Gvv1.Columns("Item_Code").HeaderText = "Item Code"

        Gvv1.Columns("item_desc").Width = 100
        Gvv1.Columns("item_desc").IsVisible = True
        Gvv1.Columns("item_desc").HeaderText = "Item Name"

        Gvv1.Columns("full_reject").Width = 100
        Gvv1.Columns("full_reject").IsVisible = True
        Gvv1.Columns("full_reject").HeaderText = "Full Rejected"

        Gvv1.Columns("partial_reject").Width = 100
        Gvv1.Columns("partial_reject").IsVisible = True
        Gvv1.Columns("partial_reject").HeaderText = "Partial Rejected"

        Gvv1.Columns("totall_reject").Width = 100
        Gvv1.Columns("totall_reject").IsVisible = True
        Gvv1.Columns("totall_reject").HeaderText = "Total Rejected"

        Gvv1.Columns("TOTAL_ACCEPTED").Width = 100
        Gvv1.Columns("TOTAL_ACCEPTED").IsVisible = True
        Gvv1.Columns("TOTAL_ACCEPTED").HeaderText = "Total Accepted"

        Gvv1.Columns("Accepted With Deduction").Width = 100
        Gvv1.Columns("Accepted With Deduction").IsVisible = True
        Gvv1.Columns("Accepted With Deduction").HeaderText = "Accepted With Deduction"

        Gvv1.Columns("Accepted Without Deduction").Width = 100
        Gvv1.Columns("Accepted Without Deduction").IsVisible = True
        Gvv1.Columns("Accepted Without Deduction").HeaderText = "Accepted Without Deduction"

        Gvv1.Columns("Total Wet QC").Width = 100
        Gvv1.Columns("Total Wet QC").IsVisible = True
        Gvv1.Columns("Total Wet QC").HeaderText = "Total Wet In QC"

        Gvv1.Columns("Rejected Wet QC").Width = 100
        Gvv1.Columns("Rejected Wet QC").IsVisible = True
        Gvv1.Columns("Rejected Wet QC").HeaderText = "Rejected Wet In QC"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("FULL_REJECT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("PARTIAL_REJECT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("TOTALL_REJECT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("TOTAL_ACCEPTED", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Accepted With Deduction", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("Accepted Without Deduction", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("Total Wet QC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("Rejected Wet QC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Gvv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxttRAL._My_Click

        Dim qry As String = " SELECT DocumentCode,DocumentDate from TSPL_TENDER_HEADER order by DocumentCode "
        TxttRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "DocumentCode", "DocumentDate", TxttRAL.arrValueMember, TxttRAL.arrDispalyMember)


    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txttBillToLocation._MYValidating

        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
                WhrCls = " Location_Type='Virtual' "
            Else
                WhrCls = " (Location_Type='Physical' or Location_Type='WorkOrder')  "
            End If

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txttBillToLocation.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txttBillToLocation.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txttBillToLocation.Value) > 0 Then
                lablBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txttBillToLocation.Value + "'"))
            Else
                lablBillToLocation.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rptPerformanceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim WhrCls As String = String.Empty
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            WhrCls = " and Location_Type='Virtual' "
        Else
            WhrCls = " and Location_Type='Physical' or Location_Type='WorkOrder'  "
        End If
        '  If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
        ' txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        txttBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' " & WhrCls & " "))
        If clsCommon.myLen(txttBillToLocation.Value) > 0 Then
            lablBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txttBillToLocation.Value + "' "))
        End If

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click

        Try
            If Gvv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                arrHeader.Add("Location:" + clsCommon.myCstr(txttBillToLocation.Value))

                transportSql.applyExportTemplate(Gvv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Quality Summary Report", Gvv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click

        Try

            If Gvv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                arrHeader.Add("Location:" + clsCommon.myCstr(txttBillToLocation.Value))

                transportSql.applyExportTemplate(Gvv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Quality Summary Report", Gvv1, arrHeader, "Quality Summary Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnnPrint.Click


        Try

            Dim whr As String = ""
            If TxttRAL.arrValueMember IsNot Nothing AndAlso TxttRAL.arrValueMember.Count > 0 Then
                whr += "  and TSPL_GRN_HEAD.Ref_No  In  (" + clsCommon.GetMulcallString(TxttRAL.arrValueMember) + ")  "
            End If

            Dim qry As String = " select ROW_NUMBER() OVER (ORDER BY TSPL_GRN_HEAD.Bill_To_Location) AS 'S.NO','RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' AS HeadName,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add4,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',
                count((CASE WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 THEN 'FULL_REJECT'  end)) AS 'FULL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'PARTIAL_REJECT',
                count((case when TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 OR TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'TOTALL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=1 AND (TSPL_GRN_HEAD.VisualQCStatusSecond<>3 OR TSPL_GRN_HEAD.VisualQCStatusSecond<>2 or TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected')then 'Ok'  end)) AS 'TOTAL_ACCEPTED',
				count(case when deduction>0 and TSPL_QC_CHECK_HEAD.QC_Status='Under Deviation' then 'Accepted With Deduction' end) as 'Accepted With Deduction',
				count(case when deduction=0 and TSPL_QC_CHECK_HEAD.QC_Status='Accepted' then 'Accepted Without Deduction' end) as 'Accepted Without Deduction',
				count(case when deduction>=0 and TSPL_QC_CHECK_HEAD.QC_Status IN ('Accepted','Under Deviation') then 'Total Wet QC' end) as 'Total Wet QC',
				count(case when  TSPL_QC_CHECK_HEAD.QC_Status='Rejected'then 'Rejected Wet QC' end) as 'Rejected Wet QC'
                from  TSPL_GRN_HEAD
                left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left OUTER join TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location
				left outer join (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_sRN_DETAIL 
				group by Document_Code) as TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
                where 2=2  
				AND TSPL_ITEM_MASTER.structure_Code IN ('RM','PM')
                and CONVERT(DATE,TSPL_GRN_HEAD.GRN_Date,103)>='01/APR/2023'  
				And TSPL_GRN_HEAD.Bill_To_Location = ('" + clsCommon.myCstr(txttBillToLocation.Value) + "') " + whr + " 
				GROUP BY TSPL_LOCATION_MASTER.Add4,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description
				order by TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No desc"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptQualitySummaryReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception

        End Try


    End Sub

End Class