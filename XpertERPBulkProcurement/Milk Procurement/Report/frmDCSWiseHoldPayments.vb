Imports common
Public Class frmDCSWiseHoldPayments

    Private Sub frmDCSWiseHoldPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CurrentFinacialYearDate()
            ResetGrid()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CurrentFinacialYearDate()
        txtFromDate.Value = objCommonVar.CurrFiscalStartDate
        txtToDate.Value = objCommonVar.CurrFiscalEndDate
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strMonth As String = Nothing
            Dim pivotMonth As String = Nothing
            Dim strTotalMonth As String = Nothing
            Dim strTotal As String = Nothing
            Dim BaseQry As String = "SELECT   DateName(MONTH,TSPL_PAYMENT_PROCESS_HEAD.From_Date)[Month],
CONCAT(DateName(MONTH,TSPL_PAYMENT_PROCESS_HEAD.From_Date),' ',Convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103),' to ',Convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)) As [Month With Date],"

            If rbtnTDS.IsChecked Then
                BaseQry += " '' As [A/C No.],"
            ElseIf rbtnSaving.IsChecked Then
                BaseQry += " TSPL_PAYMENT_PROCESS_DETAIL.Bank_Account_No_Saving As [Sav A/C No.],"
            Else
                BaseQry += " TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No As [Curr A/C No.],"
            End If

            BaseQry += " TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader As [DCS Code],TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE As [VSP Code],
TSPL_PAYMENT_PROCESS_DETAIL.VSP_NAME As [DCS Name]"

            If rbtnTDS.IsChecked Then
                BaseQry += ",TSPL_PAYMENT_PROCESS_DETAIL.TDS_Amount As [Amount]"
            ElseIf rbtnSaving.IsChecked Then
                BaseQry += ",TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount As [Amount]"
            Else
                BaseQry += ",TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount As [Amount]"
            End If

            BaseQry += " FROM TSPL_PAYMENT_PROCESS_DETAIL 
Left Outer Join  TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No "

            If rbtnSaving.IsChecked Then
                BaseQry += " Left Outer Join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code_Saving "
            End If
            If rbtnCredit.IsChecked Then
                BaseQry += " Left Outer Join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code "
            End If

            BaseQry += " Where Convert(Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=Convert(Date,'" + txtFromDate.Value + "',103) 
  And Convert(Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)<=Convert(Date,'" + txtToDate.Value + "',103) "

            If rbtnTDS.IsChecked Then
                BaseQry += " And TSPL_PAYMENT_PROCESS_DETAIL.TDS_Amount is not null "
            ElseIf rbtnSaving.IsChecked Then
                BaseQry += " And TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount is not null And TSPL_BANK_MASTER.UnPaid<>1 "
            Else
                BaseQry += " And TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount is not null And TSPL_BANK_MASTER.UnPaid=1 "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry + " Order By TSPL_PAYMENT_PROCESS_HEAD.From_Date")

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myLen(pivotMonth) > 0 Then
                        If Not strMonth.Contains(clsCommon.myCstr(dt.Rows(i)("Month With Date"))) Then
                            strMonth += ",IsNull([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "],0)[" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                            pivotMonth += ",[" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                            strTotalMonth += " ,Sum([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "])[" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                            strTotal += "+ IsNull([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "],0)"
                        End If
                    Else
                        strMonth = " IsNull([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "],0)[" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                        pivotMonth = " [" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                        strTotalMonth = " Sum([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "])[" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "]"
                        strTotal = " IsNull([" + clsCommon.myCstr(dt.Rows(i)("Month With Date")) + "],0)"
                    End If
                Next
            Else
                Throw New Exception("Data Not Found !")
            End If
            Dim Qry As String = "SELECT "
            If rbtnTDS.IsChecked Then
                Qry += " [A/C No.],"
            ElseIf rbtnSaving.IsChecked Then
                Qry += " [Sav A/C No.],"
            Else
                Qry += " [Curr A/C No.],"
            End If
            Qry += " [DCS Code],[DCS Name], " + strMonth + ",(" + strTotal + ") As [Grand Total] FROM (" + BaseQry + ") AS SourceTable PIVOT ( SUM([Amount]) FOR [Month With Date] IN (" + pivotMonth + ") ) AS PivotTable "

            Dim finalQry As String = "Select "
            If rbtnTDS.IsChecked Then
                finalQry += " Max([A/C No.])[A/C No.],"
            ElseIf rbtnSaving.IsChecked Then
                finalQry += " Max([Sav A/C No.])[Sav A/C No.],"
            Else
                finalQry += " Max([Curr A/C No.])[Curr A/C No.],"
            End If
            finalQry += " [DCS Code],Max([DCS Name])[DCS Name]," + strTotalMonth + " ,Max([Grand Total])[Grand Total] from (" + Qry + ") As final Group By [DCS Code]"
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(finalQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ResetGrid()
                gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat()
                gv1.BestFitColumns()
                SummaryRow()
                ReStoreGridLayout()
            Else
                Throw New Exception("Data Not Found !")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ResetGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
    End Sub

    Sub SummaryRow()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For i As Integer = 0 To gv1.Columns.Count - 1
            If i > 2 Then
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub SetGridFormat()
        'Gv.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
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
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                'Dim arr As List(Of String) = New List(Of String)
                'arr.Add("Bank Advise Report(OHC) from " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + " for 'Udaipur' Unit")
                'arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
                clsCommon.MyExportToExcelGrid("" + objCommonVar.CurrentCompanyName + "," + objCommonVar.CurrLocationName + "" + Environment.NewLine + "Bank Advise Report(OHC) from " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + " for '" + objCommonVar.CurrLocationName + "' Unit", gv1, Nothing, "Bank Advise(OHC)")
            Else
                Throw New Exception("No Data Found to export")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Try
            ResetGrid()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class