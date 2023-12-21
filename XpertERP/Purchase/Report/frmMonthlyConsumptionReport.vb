Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class frmMonthlyConsumptionReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim fromDate As String = Nothing
    Dim toDate As String = Nothing
    Dim toDateYear As Integer = 0
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMonthlyConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
    End Sub
    Private Sub Reset()
        Try
            gv.DataSource = Nothing
            FillFiscalYear()
            fnd_DocNo.arrValueMember = Nothing
            fnd_Dept.arrValueMember = Nothing
            fnd_Category.arrValueMember = Nothing
            fnd_ItemCode.arrValueMember = Nothing
            fnd_Months.arrValueMember = Nothing
            txtHighValue.Text = Nothing
            chkValueWise.Checked = True
            RadPageView1.SelectedPage = Me.RadPageView1.Pages("RadPageViewPage1")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub frmMonthlyConsumptionReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            Print(EnumExportTo.Excel)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Private Sub frmMonthlyConsumptionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            FillFiscalYear()
            chkValueWise.Checked = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub FillFiscalYear()
        Try
            Dim qry As String = " SELECT " & _
                                  " CONVERT(varchar, MIN(DATEPART(YEAR, Doc_Date)) - 1) + ' - ' + CONVERT(varchar, MIN(DATEPART(YEAR, Doc_Date)) - 1 + 1) " & _
                                  " AS FiscalYear, " & _
                                  " CONVERT(varchar, MIN(DATEPART(YEAR, Doc_Date)) - 1) AS Year " & _
                                            " FROM TSPL_IssueReturn_HEAD " & _
                                            " UNION ALL " & _
                               " Select DISTINCT " & _
                                  " CONVERT(varchar, DATEPART(YEAR, Doc_Date)) + ' - ' + CONVERT(varchar, DATEPART(YEAR, Doc_Date) + 1) " & _
                                  " AS FiscalYear, " & _
                                  " CONVERT(varchar, DATEPART(YEAR, Doc_Date)) AS Year " & _
                               " FROM TSPL_IssueReturn_HEAD "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                cboFiscalYear.DataSource = Nothing
                cboFiscalYear.DataSource = dt
                cboFiscalYear.ValueMember = "Year"
                cboFiscalYear.DisplayMember = "FiscalYear"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Load_Report()
        Dim outterQry As String = Nothing
      
        Dim issueQry As String = Nothing
        Dim joinsQry As String = Nothing
        Dim finalQry As String = Nothing
        Dim groupByQry As String = Nothing
        Dim orderByQry As String = Nothing
        Dim pivotQry As String = Nothing
       
        Dim pivotCols As String = Nothing
        Dim innerQry As String = Nothing
        Dim whereQry As String = Nothing
        Dim dt As DataTable = Nothing

        Try
            toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")

            ' OUTER SELECT COLUMNS ====================================================================================
            outterQry = " select " & _
                         " (Doc_No) as Doc_No , CONVERT(VARCHAR,Doc_Date) Doc_Date ," & _
                         " MAX(Dept) Dept , MAX(Dept_Desc) Dept_Desc , " & _
                         " (Item_Code) as Item_Code ," & _
                         "  max(Item_Desc) as Item_Desc ," & _
                         "  MAX([UOM]) as [UOM] , " & _
                         " (Item_Category_Code) as Item_Category_Code ,  " & _
                         " (Item_Cagetory_Values) as Item_Cagetory_Values ," & _
                         " [In Year] ,"
            If ChkQtyWise.Checked = True Then
                outterQry += " SUM(COALESCE(apr_Qty, 0)) AS [Apr Qty] , " & _
                   " SUM(COALESCE(may_Qty, 0)) AS [May Qty] , " & _
                   " SUM(COALESCE(jun_Qty, 0)) AS [Jun Qty] , " & _
                   " SUM(COALESCE(jul_Qty, 0)) AS [Jul Qty] , " & _
                   " SUM(COALESCE(aug_Qty, 0)) AS [Aug Qty] , " & _
                   " SUM(COALESCE(sep_Qty, 0)) AS [Sep Qty] , " & _
                   " SUM(COALESCE(oct_Qty, 0)) AS [Oct Qty] , " & _
                   " SUM(COALESCE(nov_Qty, 0)) AS [Nov Qty] , " & _
                   " SUM(COALESCE(dec_Qty, 0)) AS [Dec Qty] , " & _
                   " SUM(COALESCE(jan_Qty, 0)) AS [Jan Qty] , " & _
                   " SUM(COALESCE(Feb_Qty, 0)) AS [Feb Qty] , " & _
                   " SUM(COALESCE(mar_Qty, 0)) AS [Mar Qty] , "

                ' PIVOT COLUMNS ====================================================================================
                pivotCols = " SUM(ISNULL(jan_Qty, 0) + ISNULL(feb_Qty, 0) + ISNULL(mar_Qty, 0) + ISNULL(apr_Qty, 0) + ISNULL(may_Qty, 0) + ISNULL(jun_Qty, 0) + ISNULL(jul_Qty, 0) + ISNULL(aug_Qty, 0) + ISNULL(sep_Qty, 0) +     ISNULL(oct_Qty, 0) + ISNULL(nov_Qty, 0) + ISNULL(dec_Qty, 0)) AS TotalQTY "
            ElseIf chkValueWise.Checked = True Then
                outterQry += " SUM(COALESCE(apr_Amt, 0)) AS [Apr Amt] , " & _
                  " SUM(COALESCE(may_Amt, 0)) AS [May Amt] , " & _
                  " SUM(COALESCE(jun_Amt, 0)) AS [Jun Amt] , " & _
                  " SUM(COALESCE(jul_Amt, 0)) AS [Jul Amt] , " & _
                  " SUM(COALESCE(aug_Amt, 0)) AS [Aug Amt] , " & _
                  " SUM(COALESCE(sep_Amt, 0)) AS [Sep Amt] , " & _
                  " SUM(COALESCE(oct_Amt, 0)) AS [Oct Amt] , " & _
                  " SUM(COALESCE(nov_Amt, 0)) AS [Nov Amt] , " & _
                  " SUM(COALESCE(dec_Amt, 0)) AS [Dec Amt] , " & _
                  " SUM(COALESCE(jan_Amt, 0)) AS [Jan Amt] , " & _
                  " SUM(COALESCE(Feb_Amt, 0)) AS [Feb Amt] , " & _
                  " SUM(COALESCE(mar_Amt, 0)) AS [Mar Amt] , "
                pivotCols = " SUM(ISNULL(jan_Amt, 0) + ISNULL(feb_Amt, 0) + ISNULL(mar_Amt, 0) + ISNULL(apr_Amt, 0) + ISNULL(may_Amt, 0) + ISNULL(jun_Amt, 0) + ISNULL(jul_Amt, 0) + ISNULL(aug_Amt, 0) + ISNULL(sep_Amt, 0) +     ISNULL(oct_Amt, 0) + ISNULL(nov_Amt, 0) + ISNULL(dec_Amt, 0)) AS TotalAmt "
            ElseIf chkBoth.Checked = True Then
                outterQry += " SUM(COALESCE(apr_Qty, 0)) AS [Apr Qty] , " & _
                    " SUM(COALESCE(apr_Amt, 0)) AS [Apr Amt] , " & _
                    " SUM(COALESCE(may_Qty, 0)) AS [May Qty] , " & _
                      " SUM(COALESCE(may_Amt, 0)) AS [May Amt] , " & _
                       " SUM(COALESCE(jun_Qty, 0)) AS [Jun Qty] , " & _
                         " SUM(COALESCE(jun_Amt, 0)) AS [Jun Amt] , " & _
                          " SUM(COALESCE(jul_Qty, 0)) AS [Jul Qty] , " & _
                             " SUM(COALESCE(jul_Amt, 0)) AS [Jul Amt] , " & _
                                " SUM(COALESCE(aug_Qty, 0)) AS [Aug Qty] , " & _
                                 " SUM(COALESCE(aug_Amt, 0)) AS [Aug Amt] , " & _
                                  " SUM(COALESCE(sep_Qty, 0)) AS [Sep Qty] , " & _
                                  " SUM(COALESCE(sep_Amt, 0)) AS [Sep Amt] , " & _
                                    " SUM(COALESCE(oct_Qty, 0)) AS [Oct Qty] , " & _
                                        " SUM(COALESCE(oct_Amt, 0)) AS [Oct Amt] , " & _
                                          " SUM(COALESCE(nov_Qty, 0)) AS [Nov Qty] , " & _
                                            " SUM(COALESCE(nov_Amt, 0)) AS [Nov Amt] , " & _
                                              " SUM(COALESCE(dec_Qty, 0)) AS [Dec Qty] , " & _
                                                " SUM(COALESCE(dec_Amt, 0)) AS [Dec Amt] , " & _
                                                  " SUM(COALESCE(jan_Qty, 0)) AS [Jan Qty] , " & _
                                                     " SUM(COALESCE(jan_Amt, 0)) AS [Jan Amt] , " & _
                                                      " SUM(COALESCE(Feb_Qty, 0)) AS [Feb Qty] , " & _
                                                         " SUM(COALESCE(Feb_Amt, 0)) AS [Feb Amt] , " & _
                                                             " SUM(COALESCE(mar_Qty, 0)) AS [Mar Qty] , " & _
                                                                " SUM(COALESCE(mar_Amt, 0)) AS [Mar Amt] , "
                pivotCols = " SUM(ISNULL(jan_Qty, 0) + ISNULL(feb_Qty, 0) + ISNULL(mar_Qty, 0) + ISNULL(apr_Qty, 0) + ISNULL(may_Qty, 0) + ISNULL(jun_Qty, 0) + ISNULL(jul_Qty, 0) + ISNULL(aug_Qty, 0) + ISNULL(sep_Qty, 0) +     ISNULL(oct_Qty, 0) + ISNULL(nov_Qty, 0) + ISNULL(dec_Qty, 0)) AS TotalQTY ,"
                pivotCols += " SUM(ISNULL(jan_Amt, 0) + ISNULL(feb_Amt, 0) + ISNULL(mar_Amt, 0) + ISNULL(apr_Amt, 0) + ISNULL(may_Amt, 0) + ISNULL(jun_Amt, 0) + ISNULL(jul_Amt, 0) + ISNULL(aug_Amt, 0) + ISNULL(sep_Amt, 0) +     ISNULL(oct_Amt, 0) + ISNULL(nov_Amt, 0) + ISNULL(dec_Amt, 0)) AS TotalAmt "
            End If




            innerQry = " FROM (" & _
                                "SELECT " & _
                                  " Doc_No , Doc_Date , " & _
                                  " Item_Code ," & _
                                  " Item_Desc ," & _
                                  "  [UOM] , " & _
                                  " Issued_Qty ," & _
                                  " Issued_Amount ," & _
                                  " Item_Category_Code ," & _
                                  " Item_Cagetory_Values ," & _
                                  " [Month Name]+'_Qty' as [Month Name Qty] ," & _
                                   " [Month Name]+'_Amt' as [Month Name Amt] ," & _
                                  " [In Year], " & _
                                  " Dept , Dept_Desc " & _
                        "FROM ( " & _
                             " SELECT " & _
                                  " COALESCE(fnl.Doc_No, '') AS Doc_No , COALESCE(fnl.Doc_Date,'') as Doc_Date ," & _
                                  " COALESCE(fnl.Item_Code, '') AS Item_Code ," & _
                                  " MAX(fnl.[UOM]) as [UOM], " & _
                                  " MAX(COALESCE(fnl.Item_Desc, '')) AS Item_Desc ," & _
                                  " SUM(COALESCE(fnl.Issued_Qty, 0)) AS Issued_Qty ," & _
                                 "  SUM(COALESCE(fnl.Issued_Amount, 0)) as Issued_Amount ," & _
                                  " COALESCE(IMC.Item_Category_Code, '') AS Item_Category_Code ," & _
                                  " COALESCE(IMC.Item_Cagetory_Values, '') AS Item_Cagetory_Values ," & _
                                  " COALESCE(fnl.Month, '') AS [Month Name] ," & _
                                  " COALESCE(fnl.[Year], '') AS [In Year], " & _
                                  " MAX(COALESCE(Dept,'')) Dept , MAX(COALESCE(Dept_Desc,'')) Dept_Desc " & _
                            " FROM ( "

            ' ISSUE ITEM QUERY ===========================================================================================================
            issueQry = "SELECT" & _
                                      " xx.Doc_No ,  cast(IH.Doc_Date as date) as Doc_Date, " & _
                                      " '' AS Req_IssueNo ," & _
                                      " (xx.Item_Code) Item_Code ," & _
                                      " MAX(xx.Item_Desc) AS Item_Desc ," & _
                                      " SUM(xx.Issued_Qty) AS Issued_Qty ," & _
                                      " SUM(xx.Amount ) AS Issued_Amount, " & _
                                      " MAX(XX.Unit_code) AS [UOM] ," & _
                                      " DATENAME(YEAR, IH.Doc_Date) AS [Year] ," & _
                                      " LEFT(DATENAME(MONTH, IH.Doc_Date), 3) AS [Month], " & _
                                      " MAX(IH.Dept) AS Dept , MAX(IH.Dept_Desc) AS Dept_Desc " & _
                                    " FROM TSPL_IssueReturn_HEAD IH " & _
                                    " LEFT JOIN TSPL_IssueReturn_DETAIL xx " & _
                                    "  ON IH.Doc_No = XX.Doc_No " & _
                                    " GROUP BY xx.Doc_No ," & _
                                    "  XX.Item_Code ," & _
                                    "  IH.Doc_Date " & _
                                "UNION " & _
                                    "SELECT " & _
                                    " (COALESCE(yy.Req_IssueNo, '')) AS doc_no ,   cast(IH.Doc_Date as date) as Doc_Date ," & _
                                    " MAX(yy.Doc_No) AS Req_IssueNo ," & _
                                    " yy.Item_Code , " & _
                                    " MAX(yy.Item_Desc) Item_Desc ," & _
                                    " SUM(-1 * yy.Issued_Qty) AS Issued_Qty ," & _
                                    " SUM(-1*yy.Amount ) AS Issued_Amount ," & _
                                    " MAX(XX.Unit_code) AS [UOM] ," & _
                                    " DATENAME(YEAR, IH.Doc_Date) AS [Year] ," & _
                                    " LEFT(DATENAME(MONTH, IH.Doc_Date), 3) AS [Month] , " & _
                                    " MAX(IH.Dept) AS Dept , MAX(IH.Dept_Desc) AS Dept_Desc " & _
                                    " FROM TSPL_IssueReturn_HEAD IH " & _
                                    " LEFT JOIN TSPL_IssueReturn_DETAIL AS xx " & _
                                    "  ON IH.Doc_No = XX.Doc_No " & _
                                    " LEFT JOIN TSPL_IssueReturn_DETAIL AS yy " & _
                                    "  ON COALESCE(xx.Doc_No, '') = COALESCE(yy.Req_IssueNo, '') " & _
                                    "  AND xx.Item_Code = yy.Item_Code " & _
                                    " GROUP BY yy.Req_IssueNo ," & _
                                    "  yy.Item_Code , " & _
                                    "  IH.Doc_Date " & _
                                " ) AS fnl " & _
                                   " LEFT JOIN TSPL_ITEM_MASTER_CATEGORY IMC " & _
                         " ON fnl.Item_Code = IMC.Item_code" & _
                    " GROUP BY fnl.Doc_No , fnl.Doc_Date ," & _
                                " fnl.Item_Code," & _
                                " Item_Category_Code," & _
                                " Item_Cagetory_Values," & _
                                " fnl.Month," & _
                                " fnl.Year " & _
                                " ) AS finalQry " & _
                              " ) AS qryForPivot "
            ' PIVOT ON QRY ===============================================================================================================================
            'If ChkQtyWise.Checked = True Then
            '    pivotQryQty = " PIVOT (SUM(Issued_Qty) FOR [Month Name Qty] IN (jan_Qty, feb_Qty, mar_Qty, apr_Qty, may_Qty, jun_Qty, jul_Qty, aug_Qty, sep_Qty, oct_Qty, nov_Qty, dec_Qty)) AS pvtResultQt "
            'ElseIf chkValueWise.Checked = True Then
            '    pivotQryAmt = " PIVOT (SUM(Issued_Amount) FOR [Month Name Amt] IN (jan_Amt, feb_Amt, mar_Amt, apr_Amt, may_Amt, jun_Amt, jul_Amt, aug_Amt, sep_Amt, oct_Amt, nov_Amt, dec_Amt)) AS pvtResultAmt "
            'Else
            pivotQry = " PIVOT (SUM(Issued_Qty) FOR [Month Name Qty] IN (jan_Qty, feb_Qty, mar_Qty, apr_Qty, may_Qty, jun_Qty, jul_Qty, aug_Qty, sep_Qty, oct_Qty, nov_Qty, dec_Qty)) AS pvtResultQt "
            pivotQry += " PIVOT (SUM(Issued_Amount) FOR [Month Name Amt] IN (jan_Amt, feb_Amt, mar_Amt, apr_Amt, may_Amt, jun_Amt, jul_Amt, aug_Amt, sep_Amt, oct_Amt, nov_Amt, dec_Amt)) AS pvtResultAmt "



            ' FILTERS USING WHERE QRY ====================================================================================================================
            whereQry = " WHERE 1 = 1 AND COALESCE(Doc_No,'') <> '' AND Doc_Date BETWEEN '" + clsCommon.GetPrintDate(fromDate, "MM/dd/yyyy") + "' AND '" + clsCommon.GetPrintDate(toDate, "MM/dd/yyyy") + "' "

            If fnd_DocNo.arrValueMember IsNot Nothing AndAlso fnd_DocNo.arrValueMember.Count > 0 Then
                whereQry += " AND Doc_No IN  (" + clsCommon.GetMulcallString(fnd_DocNo.arrValueMember) + ") " + Environment.NewLine
            End If

            If fnd_Dept.arrValueMember IsNot Nothing AndAlso fnd_Dept.arrValueMember.Count > 0 Then
                whereQry += " AND Dept IN  (" + clsCommon.GetMulcallString(fnd_Dept.arrValueMember) + ") " + Environment.NewLine
            End If
            If fnd_ItemCode.arrValueMember IsNot Nothing AndAlso fnd_ItemCode.arrValueMember.Count > 0 Then
                whereQry += " AND Item_Code IN  (" + clsCommon.GetMulcallString(fnd_ItemCode.arrValueMember) + ") " + Environment.NewLine
            End If
            If fnd_Category.arrValueMember IsNot Nothing AndAlso fnd_Category.arrValueMember.Count > 0 Then
                whereQry += " AND Item_Cagetory_Values IN  (" + clsCommon.GetMulcallString(fnd_Category.arrValueMember) + ") " + Environment.NewLine
            End If

            ' FIND HIGH VALUE CONSUMPTION MONTHLY ===========================================================================================
            If txtHighValue.Text IsNot Nothing AndAlso clsCommon.myLen(txtHighValue.Text) > 0 Then
                If fnd_Category.arrValueMember IsNot Nothing AndAlso fnd_Category.arrValueMember.Count > 0 Then
                    whereQry += " and ("
                    If fnd_Months.arrValueMember IsNot Nothing AndAlso fnd_Months.arrValueMember.Count > 0 Then
                        For Each monthName As String In fnd_Months.arrValueMember
                            whereQry += monthName + "_Qty" + " > " + txtHighValue.Text + " or "
                        Next
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please Select <Months> to check Monthly High Consumption ", Me.Text)
                        fnd_Months.Select()
                        Exit Sub

                    End If

                    whereQry += " ) "
                    If whereQry.Contains("or  )") Then
                        whereQry = whereQry.Replace("or  )", "  )")
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please Select <Category> and <Months> to check High Consumption", Me.Text)
                    Exit Sub
                End If
            End If

            If txtHighValue.Text IsNot Nothing AndAlso clsCommon.myLen(txtHighValue.Text) > 0 Then
                If fnd_Category.arrValueMember IsNot Nothing AndAlso fnd_Category.arrValueMember.Count > 0 Then
                    whereQry += " and ("
                    If fnd_Months.arrValueMember IsNot Nothing AndAlso fnd_Months.arrValueMember.Count > 0 Then
                        For Each monthName As String In fnd_Months.arrValueMember
                            whereQry += monthName + "_Amt" + " > " + txtHighValue.Text + " or "
                        Next
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please Select <Months> to check Monthly High Consumption ", Me.Text)
                        fnd_Months.Select()
                        Exit Sub

                    End If

                    whereQry += " ) "
                    If whereQry.Contains("or  )") Then
                        whereQry = whereQry.Replace("or  )", "  )")
                    End If
                Else
                    clsCommon.MyMessageBoxShow("Please Select <Category> and <Months> to check High Consumption", Me.Text)
                    Exit Sub
                End If
            End If

            ' JOINS OF INNER QUERY ===========================================================================================================
            groupByQry = " GROUP BY Doc_No, Doc_Date, Item_Code, Item_Category_Code, Item_Cagetory_Values, [In Year] ORDER BY Item_Code "

            finalQry = outterQry + pivotCols + innerQry + issueQry + pivotQry + whereQry + groupByQry

            dt = clsDBFuncationality.GetDataTable(finalQry, Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormatOfGrid()
                RadPageView1.SelectedPage = Me.RadPageView1.Pages("RadPageViewPage2")
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Load_Data()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing

        Try
            toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")

            If fnd_DocNo.arrValueMember IsNot Nothing AndAlso fnd_DocNo.arrValueMember.Count > 0 Then
                qry = " SELECT (Document) AS Document, MAX(Category_Value_Desc) AS [Category_Value_Desc], [Item_Category_Code1], MAX([Item_Cagetory_Values]) AS [Item_Cagetory_Values], MAX(fnlQry.[Department]) AS [Department], MAX(fnlQry.[Dept. Desc]) AS [Dept Desc], (fnlQry.[Item Code]) AS [Item Code], MAX(fnlQry.[Item Desc]) AS [Item Desc], MAX(fnlQry.[UOM]) AS [UOM], CONVERT(varchar, (CAST([Date] AS date))) AS [Date], MAX(fnlQry.[Year]) AS [Year], SUM(COALESCE(apr, 0)) AS [Apr], SUM(COALESCE(may, 0)) AS [May], SUM(COALESCE(jun, 0)) AS [Jun], SUM(COALESCE(jul, 0)) AS [Jul], SUM(COALESCE(aug, 0)) AS [Aug], SUM(COALESCE(sep, 0)) AS [Sep], SUM(COALESCE(oct, 0)) AS [Oct], SUM(COALESCE(nov, 0)) AS [Nov], SUM(COALESCE(dec, 0)) AS [Dec], SUM(COALESCE(jan, 0)) AS [Jan], SUM(COALESCE(Feb, 0)) AS [Feb], SUM(COALESCE(mar, 0)) AS [Mar], SUM(ISNULL(jan, 0) + ISNULL(feb, 0) + ISNULL(mar, 0) + ISNULL(apr, 0) + ISNULL(may, 0) + ISNULL(jun, 0) + ISNULL(jul, 0) + ISNULL(aug, 0) + ISNULL(sep, 0) + ISNULL(oct, 0) + ISNULL(nov, 0) + ISNULL(dec, 0)) AS Total FROM (SELECT * FROM (SELECT (TSPL_IssueReturn_HEAD.Doc_No) AS [Document], CONVERT(date, TSPL_IssueReturn_HEAD.Doc_Date, 103) AS [Date], COALESCE(TSPL_ITEM_MASTER_CATEGORYQry.Category_Value_Desc, '') AS [Category_Value_Desc], COALESCE(Item_Category_Code1, '') AS [Item_Category_Code1], COALESCE([Item_Cagetory_Values], '') AS [Item_Cagetory_Values], COALESCE(TSPL_IssueReturn_HEAD.Dept, '') AS [Department], COALESCE(TSPL_IssueReturn_HEAD.Dept_Desc, '') AS [Dept. Desc], COALESCE(TSPL_IssueReturn_DETAIL.Item_Code, '') AS [Item Code], COALESCE(TSPL_IssueReturn_DETAIL.Item_Desc, '') AS [Item Desc], (TSPL_IssueReturn_DETAIL.Unit_code) AS [UOM], DATENAME(YEAR, TSPL_IssueReturn_HEAD.Doc_Date) AS [Year], LEFT(DATENAME(MONTH, TSPL_IssueReturn_HEAD.Doc_Date), 3) AS [Month], issQtyQry.Issued_Qty FROM TSPL_IssueReturn_HEAD LEFT JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.DOC_NO = TSPL_IssueReturn_DETAIL.Doc_No LEFT JOIN (SELECT COALESCE([Item Code], '') AS [Item Code], COALESCE([Item_Category_Code1], '') AS [Item_Category_Code1], COALESCE([Item_Cagetory_Values], '') AS [Item_Cagetory_Values], (COALESCE([Category_Value_Desc], '')) AS [Category_Value_Desc], (COALESCE([CATEGORY RM], '')) AS [CATEGORY RM], (COALESCE([BRAND], '')) AS [BRAND], (COALESCE([SUB BRAND], '')) AS [SUB BRAND], (COALESCE([DESCRP], '')) AS [DESCRP], (COALESCE([PACK], '')) AS [PACK], (COALESCE([PACK SIZE], '')) AS [PACK SIZE], (COALESCE([CATEGORY OT], '')) AS [CATEGORY OT], (COALESCE([CATEGORY FA], '')) AS [CATEGORY FA], (COALESCE([P TYPE], '')) AS [P TYPE], (COALESCE([L TYPE], '')) AS [L TYPE], (COALESCE([JW], '')) AS [JW], (COALESCE([SCRAP], '')) AS [SCRAP] FROM (SELECT COALESCE(TSPL_ITEM_MASTER.Item_Code, '') AS [Item Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code1], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values, '') AS [Item_Cagetory_Values], COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION, '') AS [Category_Value_Desc] FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values WHERE 2 = 2) xx PIVOT (MAX(Item_Category_Code) FOR Item_Category_Code IN ([CATEGORY RM], [BRAND], [SUB BRAND], [DESCRP], [PACK], [PACK SIZE], [CATEGORY OT], [CATEGORY FA], [P TYPE], [L TYPE], [JW], [SCRAP])) Pivt) AS TSPL_ITEM_MASTER_CATEGORYQry ON TSPL_IssueReturn_DETAIL.Item_Code = TSPL_ITEM_MASTER_CATEGORYQry.[Item Code] LEFT JOIN (SELECT SUM(fnl.Issued_Qty) Issued_Qty, Doc_No, item_Code FROM (SELECT SUM(Issued_Qty) Issued_Qty, item_Code, (Doc_No) Doc_No FROM TSPL_IssueReturn_DETAIL WHERE 1 = 1 GROUP BY Doc_No, item_Code UNION SELECT SUM(-1 * Issued_Qty) Issued_Qty, item_Code, (req_issueNo) Doc_No FROM TSPL_IssueReturn_DETAIL WHERE 1 = 1 AND req_issueNo IN (SELECT Doc_No FROM TSPL_IssueReturn_DETAIL) GROUP BY req_issueNo, item_Code) AS fnl WHERE 1 = 1 GROUP BY Doc_No, item_Code) AS issQtyQry ON TSPL_IssueReturn_DETAIL.Doc_No = issQtyQry.doc_no AND TSPL_IssueReturn_DETAIL.item_Code = issQtyQry.item_Code "
            Else
                qry = " SELECT  MAX(Category_Value_Desc) AS [Category_Value_Desc], [Item_Category_Code1], MAX([Item_Cagetory_Values]) AS [Item_Cagetory_Values], MAX(fnlQry.[Department]) AS [Department], MAX(fnlQry.[Dept. Desc]) AS [Dept Desc], (fnlQry.[Item Code]) AS [Item Code], MAX(fnlQry.[Item Desc]) AS [Item Desc], MAX(fnlQry.[UOM]) AS [UOM], CONVERT(varchar, (CAST([Date] AS date))) AS [Date], MAX(fnlQry.[Year]) AS [Year], SUM(COALESCE(apr, 0)) AS [Apr], SUM(COALESCE(may, 0)) AS [May], SUM(COALESCE(jun, 0)) AS [Jun], SUM(COALESCE(jul, 0)) AS [Jul], SUM(COALESCE(aug, 0)) AS [Aug], SUM(COALESCE(sep, 0)) AS [Sep], SUM(COALESCE(oct, 0)) AS [Oct], SUM(COALESCE(nov, 0)) AS [Nov], SUM(COALESCE(dec, 0)) AS [Dec], SUM(COALESCE(jan, 0)) AS [Jan], SUM(COALESCE(Feb, 0)) AS [Feb], SUM(COALESCE(mar, 0)) AS [Mar], SUM(ISNULL(jan, 0) + ISNULL(feb, 0) + ISNULL(mar, 0) + ISNULL(apr, 0) + ISNULL(may, 0) + ISNULL(jun, 0) + ISNULL(jul, 0) + ISNULL(aug, 0) + ISNULL(sep, 0) + ISNULL(oct, 0) + ISNULL(nov, 0) + ISNULL(dec, 0)) AS Total FROM (SELECT * FROM (SELECT (TSPL_IssueReturn_HEAD.Doc_No) AS [Document], CONVERT(date, TSPL_IssueReturn_HEAD.Doc_Date, 103) AS [Date], COALESCE(TSPL_ITEM_MASTER_CATEGORYQry.Category_Value_Desc, '') AS [Category_Value_Desc], COALESCE(Item_Category_Code1, '') AS [Item_Category_Code1], COALESCE([Item_Cagetory_Values], '') AS [Item_Cagetory_Values], COALESCE(TSPL_IssueReturn_HEAD.Dept, '') AS [Department], COALESCE(TSPL_IssueReturn_HEAD.Dept_Desc, '') AS [Dept. Desc], COALESCE(TSPL_IssueReturn_DETAIL.Item_Code, '') AS [Item Code], COALESCE(TSPL_IssueReturn_DETAIL.Item_Desc, '') AS [Item Desc], (TSPL_IssueReturn_DETAIL.Unit_code) AS [UOM], DATENAME(YEAR, TSPL_IssueReturn_HEAD.Doc_Date) AS [Year], LEFT(DATENAME(MONTH, TSPL_IssueReturn_HEAD.Doc_Date), 3) AS [Month], issQtyQry.Issued_Qty FROM TSPL_IssueReturn_HEAD LEFT JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.DOC_NO = TSPL_IssueReturn_DETAIL.Doc_No LEFT JOIN (SELECT COALESCE([Item Code], '') AS [Item Code], COALESCE([Item_Category_Code1], '') AS [Item_Category_Code1], COALESCE([Item_Cagetory_Values], '') AS [Item_Cagetory_Values], (COALESCE([Category_Value_Desc], '')) AS [Category_Value_Desc], (COALESCE([CATEGORY RM], '')) AS [CATEGORY RM], (COALESCE([BRAND], '')) AS [BRAND], (COALESCE([SUB BRAND], '')) AS [SUB BRAND], (COALESCE([DESCRP], '')) AS [DESCRP], (COALESCE([PACK], '')) AS [PACK], (COALESCE([PACK SIZE], '')) AS [PACK SIZE], (COALESCE([CATEGORY OT], '')) AS [CATEGORY OT], (COALESCE([CATEGORY FA], '')) AS [CATEGORY FA], (COALESCE([P TYPE], '')) AS [P TYPE], (COALESCE([L TYPE], '')) AS [L TYPE], (COALESCE([JW], '')) AS [JW], (COALESCE([SCRAP], '')) AS [SCRAP] FROM (SELECT COALESCE(TSPL_ITEM_MASTER.Item_Code, '') AS [Item Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code1], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values, '') AS [Item_Cagetory_Values], COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION, '') AS [Category_Value_Desc] FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values WHERE 2 = 2) xx PIVOT (MAX(Item_Category_Code) FOR Item_Category_Code IN ([CATEGORY RM], [BRAND], [SUB BRAND], [DESCRP], [PACK], [PACK SIZE], [CATEGORY OT], [CATEGORY FA], [P TYPE], [L TYPE], [JW], [SCRAP])) Pivt) AS TSPL_ITEM_MASTER_CATEGORYQry ON TSPL_IssueReturn_DETAIL.Item_Code = TSPL_ITEM_MASTER_CATEGORYQry.[Item Code] LEFT JOIN (SELECT SUM(fnl.Issued_Qty) Issued_Qty, Doc_No, item_Code FROM (SELECT SUM(Issued_Qty) Issued_Qty, item_Code, (Doc_No) Doc_No FROM TSPL_IssueReturn_DETAIL WHERE 1 = 1 GROUP BY Doc_No, item_Code UNION SELECT SUM(-1 * Issued_Qty) Issued_Qty, item_Code, (req_issueNo) Doc_No FROM TSPL_IssueReturn_DETAIL WHERE 1 = 1 AND req_issueNo IN (SELECT Doc_No FROM TSPL_IssueReturn_DETAIL) GROUP BY req_issueNo, item_Code) AS fnl WHERE 1 = 1 GROUP BY Doc_No, item_Code) AS issQtyQry ON TSPL_IssueReturn_DETAIL.Doc_No = issQtyQry.doc_no AND TSPL_IssueReturn_DETAIL.item_Code = issQtyQry.item_Code "
            End If

            qry += " WHERE 1 = 1 AND TSPL_IssueReturn_HEAD.Doc_Date BETWEEN '" + clsCommon.GetPrintDate(fromDate, "MM/dd/yyyy") + "' AND '" + clsCommon.GetPrintDate(toDate, "MM/dd/yyyy") + "' "

            If fnd_Months.arrValueMember IsNot Nothing AndAlso fnd_Months.arrValueMember.Count > 0 Then
                qry += " AND LEFT(DATENAME(MONTH, TSPL_IssueReturn_HEAD.Doc_Date),3) IN  (" + clsCommon.GetMulcallString(fnd_Months.arrValueMember) + ") " + Environment.NewLine
            End If

            qry += " ) AS SourceQryResult PIVOT (SUM(Issued_Qty) FOR [Month] IN (jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec)) AS pvtResult) fnlQry " & _
                  " WHERE 1 = 1  "

            If fnd_DocNo.arrValueMember IsNot Nothing AndAlso fnd_DocNo.arrValueMember.Count > 0 Then
                qry += " AND fnlQry.Document IN  (" + clsCommon.GetMulcallString(fnd_DocNo.arrValueMember) + ") " + Environment.NewLine
            End If

            If fnd_Dept.arrValueMember IsNot Nothing AndAlso fnd_Dept.arrValueMember.Count > 0 Then
                qry += " AND fnlQry.Department IN  (" + clsCommon.GetMulcallString(fnd_Dept.arrValueMember) + ") " + Environment.NewLine
            End If
            If fnd_ItemCode.arrValueMember IsNot Nothing AndAlso fnd_ItemCode.arrValueMember.Count > 0 Then
                qry += " AND fnlQry.[Item Code] IN  (" + clsCommon.GetMulcallString(fnd_ItemCode.arrValueMember) + ") " + Environment.NewLine
            End If
            If fnd_Category.arrValueMember IsNot Nothing AndAlso fnd_Category.arrValueMember.Count > 0 Then
                qry += " AND fnlQry.Item_Cagetory_Values IN  (" + clsCommon.GetMulcallString(fnd_Category.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtHighValue.Text IsNot Nothing AndAlso clsCommon.myLen(txtHighValue.Text) > 0 Then
                If fnd_Category.arrValueMember IsNot Nothing AndAlso fnd_Category.arrValueMember.Count > 0 Then
                    qry += " and ("
                    If fnd_Months.arrValueMember IsNot Nothing AndAlso fnd_Months.arrValueMember.Count > 0 Then
                        For Each monthName As String In fnd_Months.arrValueMember
                            qry += "  fnlQry." + monthName + " > " + txtHighValue.Text + " or "
                        Next
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please Select <Months> to check Monthly High Consumption ", Me.Text)
                        fnd_Months.Select()
                        Exit Sub

                    End If

                    qry += " ) "
                    If qry.Contains("or  )") Then
                        qry = qry.Replace("or  )", "  )")
                    End If
                Else
                    clsCommon.MyMessageBoxShow("Please Select <Category> and <Months> to check High Consumption", Me.Text)
                    Exit Sub
                End If
            End If

            If fnd_DocNo.arrValueMember IsNot Nothing AndAlso fnd_DocNo.arrValueMember.Count > 0 Then
                qry += " GROUP BY  fnlQry.Document, fnlQry.[Item_Category_Code1], fnlQry.[Date], fnlQry.[Item Code] "
            Else
                qry += " GROUP BY " & _
                  " fnlQry.[Item_Category_Code1], fnlQry.[Date], fnlQry.[Item Code] " & _
                  " ORDER BY " & _
                  " fnlQry.[Date]    "
            End If

            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormatOfGrid()
                RadPageView1.SelectedPage = Me.RadPageView1.Pages("RadPageViewPage2")
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetGridFormatOfGrid()
        Try

            gv.TableElement.TableHeaderHeight = 45
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For Each col As GridViewColumn In gv.Columns
                col.Width = 120
                col.ReadOnly = True

                If col.Name = "Document" Then
                    col.Width = 180
                End If

                If col.Name = "Category_Value_Desc" Then
                    col.Width = 180
                    col.HeaderText = "Category Value Desc"
                End If

                If col.Name = "Item Desc" Then
                    col.Width = 180
                End If

                If col.Name = "Dept. Desc" Then
                    col.Width = 180
                End If
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("Jan Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item14 As New GridViewSummaryItem("Jan Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)
            Dim item2 As New GridViewSummaryItem("Feb Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Mar Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Apr Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("May Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Jun Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Jul Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Aug Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("Sep Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Oct Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Nov Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item12 As New GridViewSummaryItem("Dec Qty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("TotalQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
   
            Dim item15 As New GridViewSummaryItem("Feb Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)
            Dim item16 As New GridViewSummaryItem("Mar Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
            Dim item17 As New GridViewSummaryItem("Apr Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item17)
            Dim item18 As New GridViewSummaryItem("May Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item18)
            Dim item19 As New GridViewSummaryItem("Jun Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
            Dim item20 As New GridViewSummaryItem("Jul Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item21 As New GridViewSummaryItem("Aug Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21)
            Dim item22 As New GridViewSummaryItem("Sep Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item22)
            Dim item23 As New GridViewSummaryItem("Oct Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item23)
            Dim item24 As New GridViewSummaryItem("Nov Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item24)
            Dim item25 As New GridViewSummaryItem("Dec Amt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item25)
            Dim item26 As New GridViewSummaryItem("TotalAmt", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item26)

            'gv.ShowGroupPanel = False
            'gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ReStoreGridLayout()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            'Load_Data()
            PageSetupReport_ID = MyBase.Form_ID + IIf(ChkQtyWise.Checked = True, "Q", "") + IIf(chkValueWise.Checked = True, "V", "") + IIf(chkBoth.Checked = True, "B", "")
            TemplateGridview = gv
            Load_Report()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub QuickExcelExport()
        Try
            toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(toDate, "dd/MMM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmMonthlyConsumptionReport & "'"))

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
            'common.clsCommon.MyMessageBoxShow("Exported Successfully", Me.Text)
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
 
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub Print(ByVal exporter As EnumExportTo)
        Dim arrHeader As List(Of String) = Nothing
        Try
            If clsCommon.myLen(fromDate) < 0 AndAlso clsCommon.myLen(toDate) < 0 Then
                toDateYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
                fromDate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
                toDate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(toDateYear) + "", "dd/MM/yyyy")
            End If

            arrHeader = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmMonthlyConsumptionReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(fromDate, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(toDate, "dd/MMM/yyyy")) + " ")

            If exporter = EnumExportTo.Excel Then
                If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully", Me.Text)
                    'Process.Start(filePath)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found in Grid to Export In Excel Sheet", Me.Text)
                End If
            Else
                If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Monthly Consumption Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Else
                    clsCommon.MyMessageBoxShow("No Data is found in Grid, Load Data in grid and then Export to PDF", Me.Text)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub menuSaveLayout_Click(sender As Object, e As EventArgs) Handles menuSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                gv.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout Saved Successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub menuDeleteLayout_Click(sender As Object, e As EventArgs) Handles menuDeleteLayout.Click
        Try
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            clsGridLayout.DeleteData(obj.ReportID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
            'ReStoreGridLayout()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Print(EnumExportTo.Excel)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub fnd_Category__My_Click(sender As Object, e As EventArgs) Handles fnd_Category._My_Click
        Try
            'Dim qry = " SELECT  COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,'') AS Item_Cagetory_Values , MAX(COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION,'')) AS Category_Value_Desc  FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_code ,'') = COALESCE(TSPL_ITEM_MASTER.Item_code,'') LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE,'') = COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,'') AND COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE,'') = COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,'') WHERE 2 = 2 GROUP BY Item_Cagetory_Values ORDER BY Item_Cagetory_Values DESC "
            Dim qry As String = " SELECT [Item_Category_Code1], [Item_Cagetory_Values], [Category_Value_Desc] FROM (SELECT MAX([Item Code]) AS [Item Code], [Item_Category_Code1], [Item_Cagetory_Values], MAX([Category_Value_Desc]) AS [Category_Value_Desc], MAX(COALESCE([CATEGORY RM], '')) AS [CATEGORY RM], MAX(COALESCE([BRAND], '')) AS [BRAND], MAX(COALESCE([SUB BRAND], '')) AS [SUB BRAND], MAX(COALESCE([DESCRP], '')) AS [DESCRP], MAX(COALESCE([PACK], '')) AS [PACK], MAX(COALESCE([PACK SIZE], '')) AS [PACK SIZE], MAX(COALESCE([CATEGORY OT], '')) AS [CATEGORY OT], MAX(COALESCE([CATEGORY FA], '')) AS [CATEGORY FA], MAX(COALESCE([P TYPE], '')) AS [P TYPE], MAX(COALESCE([L TYPE], '')) AS [L TYPE], MAX(COALESCE([JW], '')) AS [JW], MAX(COALESCE([SCRAP], '')) AS [SCRAP] FROM (SELECT COALESCE(TSPL_ITEM_MASTER.Item_Code, '') AS [Item Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code, '') AS [Item_Category_Code1], COALESCE(TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values, '') AS [Item_Cagetory_Values], COALESCE(TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION, '') AS [Category_Value_Desc] FROM TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values WHERE 2 = 2) xx PIVOT (MAX(Item_Category_Code) FOR Item_Category_Code IN ([CATEGORY RM], [BRAND], [SUB BRAND], [DESCRP], [PACK], [PACK SIZE], [CATEGORY OT], [CATEGORY FA], [P TYPE], [L TYPE], [JW], [SCRAP])) Pivt GROUP BY [Item_Category_Code1], [Item_Cagetory_Values]) AS subQryCat order by  [Item_Cagetory_Values] desc"
            fnd_Category.arrValueMember = clsCommon.ShowMultipleSelectForm("MCR_Category", qry, "Item_Cagetory_Values", "Item_Cagetory_Values", fnd_Category.arrValueMember, fnd_Category.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fnd_ItemCode__My_Click(sender As Object, e As EventArgs) Handles fnd_ItemCode._My_Click
        Try
            Dim qry As String = "SELECT item_code , Item_Desc from TSPL_ITEM_MASTER"
            fnd_ItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("MCR_ItemCode", qry, "item_code", "Item_Desc", fnd_ItemCode.arrValueMember, fnd_ItemCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fnd_Dept__My_Click(sender As Object, e As EventArgs) Handles fnd_Dept._My_Click
        Try
            Dim qry As String = "select distinct TSPL_IssueReturn_HEAD.Dept  , TSPL_IssueReturn_HEAD.Dept_Desc from  TSPL_IssueReturn_HEAD "
            fnd_Dept.arrValueMember = clsCommon.ShowMultipleSelectForm("MCR_Dept", qry, "Dept", "Dept_Desc", fnd_Dept.arrValueMember, fnd_Dept.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fnd_DocNo__My_Click(sender As Object, e As EventArgs) Handles fnd_DocNo._My_Click
        Try
            Dim qry = "SELECT distinct Doc_No , CAST(Doc_Date AS date) AS Doc_Date , Doc_Type  FROM TSPL_IssueReturn_HEAD "
            fnd_DocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("MCR_fnd_DocNo", qry, "Doc_No", "Doc_No", fnd_DocNo.arrValueMember, fnd_DocNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column.Name = "Document" Then

                Dim selectedDocument As String = e.Row.Cells("Document").Value
                If selectedDocument IsNot Nothing AndAlso clsCommon.myLen(selectedDocument) > 0 Then
                    Dim obj As frmIssueReturn = New frmIssueReturn()
                    obj.SetUserMgmt(clsUserMgtCode.mbtnIssueReturn)
                    obj.Show()
                    obj.LoadData(selectedDocument, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fnd_Months__My_Click(sender As Object, e As EventArgs) Handles fnd_Months._My_Click
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Try
            qry = " select MM , [FullForm] from ( select 'Jan' as MM , 'January' as [FullForm] union all select 'feb' as MM , 'Feburary' as [Month] union all select 'Mar' as MM , 'March' as [Month] union all select 'Apr' as MM , 'April' as [Month] union all select 'May' as MM , 'May' as [Month] union all select 'Jun' as MM , 'June' as [Month] union all select 'Jul' as MM , 'July' as [Month] union all select 'Aug' as MM , 'August' as [Month] union all select 'Sep' as MM , 'September' as [Month] union all select 'Oct' as MM , 'October' as [Month] union all select 'Nov' as MM , 'November' as [Month] union all select 'Dec' as MM , 'December' as [Month] ) as MonthsTable "
            fnd_Months.arrValueMember = clsCommon.ShowMultipleSelectForm("MCR_Month", qry, "MM", "FullForm", fnd_Months.arrValueMember, fnd_Months.arrDispalyMember)
        Catch ex As Exception

        End Try
    End Sub

  
    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Print(EnumExportTo.PDF)
    End Sub
End Class



