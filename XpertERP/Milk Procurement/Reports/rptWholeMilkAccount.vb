Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Public Class rptWholeMilkAccount
#Region "Variables"
    Dim dt As DataTable = Nothing
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExcel.Visible = MyBase.isExport
    End Sub

    Private Sub RptMassBalanceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset(True)
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        rbtnPaymentCycleWise.IsChecked = True
        SetToDateSplit()

    End Sub


    Sub Reset(ByVal FormLoad As Boolean)
        If FormLoad Then
            Dim dt As DateTime = clsCommon.GETSERVERDATE()
            txtFromDate.Value = New Date(dt.Year, dt.Month, 1)
            txtToDate.Value = txtFromDate.Value
        End If
        txtToDate.ReadOnly = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableAllcontrol(True)
        dt = Nothing
    End Sub
    Sub EnableDisableAllcontrol(ByVal val As Boolean)
        txtFromDate.Enabled = val
        SplitContainer2.Enabled = val
        txtShed.Enabled = val
        lblShed.Enabled = val
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = GetReportID()
            Load_Data()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function getAllMCC(ByVal strshed As String) As ArrayList
        Dim arrReturn As ArrayList = Nothing
        Dim qry As String = "select MCC_Code  from TSPL_MCC_MASTER where plant_Code='" + strshed + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arrReturn = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arrReturn.Add(clsCommon.myCstr(dr("MCC_Code")))
            Next
        End If
        Return arrReturn
    End Function
    Public Sub Load_Data()
        Try
            If rbtnMonthWise.IsChecked Then
                If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDateMonthWise.Value) Then
                    txtFromDate.Focus()
                    Throw New Exception("From-Date cannot be Greater than To-Date")
                End If
            End If
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From-Date cannot be Greater than To-Date")
            End If

            Dim strTotal As String = ""

            Dim qry As String
            Dim ReportNo As Integer = 0
            Dim arrMCC As New ArrayList
            If rbtnPlantMilkAccount.IsChecked Then
                ReportNo = 1
                arrMCC = getAllMCC(txtShed.Value)
            ElseIf rbtnPlantMilkAccountAbstract.IsChecked Then
                ReportNo = 2
                arrMCC = getAllMCC(txtShed.Value)
            ElseIf rbtnMCCMilkAccount.IsChecked Then
                ReportNo = 3
                If clsCommon.myLen(txtMCC.Value) <= 0 Then
                    Throw New Exception("Please select MCC")
                End If
                arrMCC.Add(txtMCC.Value)
            ElseIf rbtnMCCMilkAccountAbstract.IsChecked Then
                ReportNo = 4
                If clsCommon.myLen(txtMCC.Value) <= 0 Then
                    Throw New Exception("Please select MCC")
                End If
                arrMCC.Add(txtMCC.Value)
            End If

            qry = clsDB.GetWholeMilkAccount(ReportNo, txtShed.Value, arrMCC, txtFromDate.Value, txtToDate.Value, rbtnMonthWise.IsChecked, txtToDateMonthWise.Value)
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                If rbtnPlantMilkAccount.IsChecked OrElse rbtnPlantMilkAccountAbstract.IsChecked Then
                    Dim dclTotalStock As Decimal = 0

                    Dim dclCLBal As Decimal = 0
                    Dim drTotal As DataRow = dt.NewRow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii <> 0 Then
                            dt.Rows(ii)("PrevOPBal") = dclCLBal
                        End If
                        dclTotalStock = clsCommon.myCdbl(dt.Rows(ii)("OPBal")) + clsCommon.myCdbl(dt.Rows(ii)("PrevOPBal")) + clsCommon.myCdbl(dt.Rows(ii)("Total")) + clsCommon.myCdbl(dt.Rows(ii)("Sour")) + clsCommon.myCdbl(dt.Rows(ii)("Curd"))
                        dclCLBal = dclTotalStock - clsCommon.myCdbl(dt.Rows(ii)("TotalDisposal"))

                        dt.Rows(ii)("TotalStock") = dclTotalStock
                        dt.Rows(ii)("CLBal") = dclCLBal

                        For jj As Integer = 0 To dt.Columns.Count - 1
                            If Not (clsCommon.CompairString(dt.Columns(jj).ColumnName, "GrpDate") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(jj).ColumnName, "PrevOPBal") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(jj).ColumnName, "CLBal") = CompairStringResult.Equal) Then
                                If drTotal(jj) Is DBNull.Value Then
                                    drTotal(jj) = 0
                                End If
                                drTotal(jj) += clsCommon.myCdbl(dt.Rows(ii)(jj))
                            End If
                        Next
                    Next
                    dt.Rows.Add(drTotal)
                    Dim dclHandling As Decimal = 0
                    Dim dclReceipt As Decimal = 0
                    Dim dclDisposal As Decimal = 0

                    Dim dtAbstact As New DataTable()
                    dtAbstact.Columns.Add("Particular1", GetType(String))
                    dtAbstact.Columns.Add("Amount1", GetType(Decimal))
                    dtAbstact.AcceptChanges()

                    Dim drAbs As DataRow
                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " RECEIPTS"
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " OPBal"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("OPBal"))
                    dclReceipt += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = "Whole Milk Receipts (List enclosed)"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("MCC")) + clsCommon.myCdbl(drTotal("BMCU"))
                    dclReceipt += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = "RECEIVED OTHER MILK SHED"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("OtherMilkShed"))
                    dclReceipt += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Curdled Milk"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("Sour")) + clsCommon.myCdbl(drTotal("Curd"))
                    dclReceipt += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    Dim indxReceipt As Integer = dtAbstact.Rows.Count



                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " DISPOSALS"
                    dtAbstact.Rows.Add(drAbs)

                    Dim dtOPEntry As DataTable = clsDB.GetOPType(True)
                    For jj As Integer = 0 To dtOPEntry.Rows.Count - 1
                        drAbs = dtAbstact.NewRow()
                        drAbs("Particular1") = clsCommon.myCstr(dtOPEntry.Rows(jj)(0))
                        drAbs("Amount1") = clsCommon.myCdbl(drTotal(clsCommon.myCstr(dtOPEntry.Rows(jj)(0))))
                        dclDisposal += clsCommon.myCdbl(drAbs("Amount1"))
                        dtAbstact.Rows.Add(drAbs)
                    Next

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Transit Tankers"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("TransitTankerCurrent"))
                    dclDisposal += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Sent to MPFHYD"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("MPF"))
                    dclDisposal += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Closing Balance / TRANSIT"
                    drAbs("Amount1") = dclCLBal
                    dclDisposal += clsCommon.myCdbl(drAbs("Amount1"))
                    dtAbstact.Rows.Add(drAbs)

                    dclHandling = dclDisposal - dclReceipt

                    If dclHandling > 0 Then
                        drAbs = dtAbstact.NewRow()
                        drAbs("Particular1") = " Handling Gain"
                        drAbs("Amount1") = dclHandling
                        dclReceipt += clsCommon.myCdbl(drAbs("Amount1"))
                        dtAbstact.Rows.InsertAt(drAbs, indxReceipt)
                        indxReceipt += 1
                    Else
                        drAbs = dtAbstact.NewRow()
                        drAbs("Particular1") = " Handling Loss"
                        drAbs("Amount1") = Math.Abs(dclHandling)
                        dclDisposal += clsCommon.myCdbl(drAbs("Amount1"))
                        dtAbstact.Rows.Add(drAbs)
                    End If


                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Total RECEIPTS"
                    drAbs("Amount1") = dclReceipt
                    dtAbstact.Rows.InsertAt(drAbs, indxReceipt)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Total DISPOSALS"
                    drAbs("Amount1") = dclDisposal
                    dtAbstact.Rows.Add(drAbs)



                    If rbtnPlantMilkAccountAbstract.IsChecked Then
                        dt = New DataTable
                        dt = dtAbstact.Copy()
                    Else
                        If dclHandling > 0 Then
                            dt.Rows(dt.Rows.Count - 1)("HandlingGain") = dclHandling
                            dt.Rows(dt.Rows.Count - 2)("HandlingGain") = dclHandling

                            dt.Rows(dt.Rows.Count - 1)("TotalStock") = dclHandling
                            dt.Rows(dt.Rows.Count - 2)("TotalStock") = dclHandling
                        Else
                            dt.Rows(dt.Rows.Count - 1)("HandlingLoss") = Math.Abs(dclHandling)
                            dt.Rows(dt.Rows.Count - 2)("HandlingLoss") = Math.Abs(dclHandling)
                        End If
                        dt.Rows(dt.Rows.Count - 1)("CLBal") = dclHandling
                        dt.Rows(dt.Rows.Count - 2)("CLBal") = dclHandling

                    End If
                ElseIf rbtnMCCMilkAccount.IsChecked OrElse rbtnMCCMilkAccountAbstract.IsChecked Then
                    Dim dclTotal As Decimal = 0
                    Dim dclGrandTotal As Decimal = 0
                    Dim dclCLBal As Decimal = 0
                    Dim drTotal As DataRow = dt.NewRow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii <> 0 Then
                            dt.Rows(ii)("OPBal") = dclCLBal
                        End If
                        dclTotal = clsCommon.myCdbl(dt.Rows(ii)("OPBal")) + clsCommon.myCdbl(dt.Rows(ii)("TankerTrasitPrevious")) + clsCommon.myCdbl(dt.Rows(ii)("GMTotal")) + clsCommon.myCdbl(dt.Rows(ii)("PTCBM")) + clsCommon.myCdbl(dt.Rows(ii)("PTCCM"))
                        dclGrandTotal = dclTotal + clsCommon.myCdbl(dt.Rows(ii)("MCCWNP"))
                        dclCLBal = dclGrandTotal - clsCommon.myCdbl(dt.Rows(ii)("Sales")) - clsCommon.myCdbl(dt.Rows(ii)("PTCCurld")) - clsCommon.myCdbl(dt.Rows(ii)("Curld")) - clsCommon.myCdbl(dt.Rows(ii)("MilkSentOther")) - clsCommon.myCdbl(dt.Rows(ii)("TransitTankerCurrent")) - clsCommon.myCdbl(dt.Rows(ii)("MilkSentOther")) - clsCommon.myCdbl(dt.Rows(ii)("MPFDispatch"))
                        dt.Rows(ii)("Total") = dclTotal
                        dt.Rows(ii)("GrandTotal") = dclGrandTotal
                        dt.Rows(ii)("CLBal") = dclCLBal

                        For jj As Integer = 0 To dt.Columns.Count - 1
                            If Not (clsCommon.CompairString(dt.Columns(jj).ColumnName, "GrpDate") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(jj).ColumnName, "OPBal") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(jj).ColumnName, "CLBal") = CompairStringResult.Equal) Then
                                If drTotal(jj) Is DBNull.Value Then
                                    drTotal(jj) = 0
                                End If
                                drTotal(jj) += clsCommon.myCdbl(dt.Rows(ii)(jj))
                            End If
                        Next
                    Next
                    dt.Rows.Add(drTotal)
                    Dim dclHandling As Decimal = 0

                    Dim dclLeft As Decimal = 0
                    Dim dclRight As Decimal = 0

                    Dim dtAbstact As New DataTable()
                    dtAbstact.Columns.Add("Particular1", GetType(String))
                    dtAbstact.Columns.Add("Amount1", GetType(Decimal))
                    dtAbstact.Columns.Add("Particular2", GetType(String))
                    dtAbstact.Columns.Add("Amount2", GetType(Decimal))
                    dtAbstact.AcceptChanges()

                    Dim drAbs As DataRow
                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Transit Op.Balance"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("OPBal")) + clsCommon.myCdbl(drTotal("TankerTrasitPrevious"))
                    drAbs("Particular2") = "Sales"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("Sales"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " B.M."
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("GMBM"))
                    drAbs("Particular2") = "MPF Despatch"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("MPFDispatch"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " C.M."
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("GMCM"))
                    drAbs("Particular2") = "Cruld"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("Curld"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " MCC"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("MCCWNP"))
                    drAbs("Particular2") = "Milk Sent Other"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("MilkSentOther"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    'drAbs("Particular1") = " "
                    'drAbs("Amount1") = 0
                    drAbs("Particular2") = "TRANSIT"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("TransitTankerCurrent"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " PTC CURLD"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("PTCBM")) + clsCommon.myCdbl(drTotal("PTCCM"))
                    drAbs("Particular2") = "PTC"
                    drAbs("Amount2") = clsCommon.myCdbl(drTotal("PTCCurld"))
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Curld"
                    drAbs("Amount1") = clsCommon.myCdbl(drTotal("Curld"))
                    drAbs("Particular2") = "Cl. Balance"
                    drAbs("Amount2") = dclCLBal
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    dclHandling = dclRight - dclLeft
                    drAbs = dtAbstact.NewRow()
                    If dclHandling > 0 Then
                        drAbs("Particular1") = " Handling gain"
                        drAbs("Amount1") = dclHandling
                        drAbs("Particular2") = "Handling loss"
                        drAbs("Amount2") = 0
                    Else
                        drAbs("Particular1") = " Handling gain"
                        drAbs("Amount1") = 0
                        drAbs("Particular2") = "Handling loss"
                        drAbs("Amount2") = Math.Abs(dclHandling)
                    End If
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.InsertAt(drAbs, 3)

                    drAbs = dtAbstact.NewRow()
                    drAbs("Particular1") = " Total"
                    drAbs("Amount1") = dclLeft
                    drAbs("Particular2") = "Total"
                    drAbs("Amount2") = dclRight
                    dclLeft += clsCommon.myCdbl(drAbs("Amount1"))
                    dclRight += clsCommon.myCdbl(drAbs("Amount2"))
                    dtAbstact.Rows.Add(drAbs)

                    If rbtnMCCMilkAccountAbstract.IsChecked Then
                        dt = New DataTable
                        dt = dtAbstact.Copy()
                    Else
                        If dclHandling > 0 Then
                            dt.Rows(dt.Rows.Count - 1)("HandlingGain") = dclHandling
                            dt.Rows(dt.Rows.Count - 2)("HandlingGain") = dclHandling

                            dt.Rows(dt.Rows.Count - 1)("GrandTotal") = dclHandling
                            dt.Rows(dt.Rows.Count - 2)("GrandTotal") = dclHandling
                        Else
                            dt.Rows(dt.Rows.Count - 1)("HandlingLoss") = Math.Abs(dclHandling)
                            dt.Rows(dt.Rows.Count - 2)("HandlingLoss") = Math.Abs(dclHandling)
                        End If
                        dt.Rows(dt.Rows.Count - 1)("CLBal") = dclHandling
                        dt.Rows(dt.Rows.Count - 2)("CLBal") += dclHandling
                    End If
                End If
            End If
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.ShowGroupPanel = False
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.EnableFiltering = True
                Gv1.ShowFilteringRow = True
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGrifFormat()
                EnableDisableAllcontrol(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
            Gv1.BestFitColumns()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub




    Sub SetGrifFormat()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        If rbtnPlantMilkAccount.IsChecked Then
            Gv1.Columns("GrpDate").HeaderText = "Date"
            Gv1.Columns("OPBal").HeaderText = "Opening Balance"
            Gv1.Columns("PrevOPBal").HeaderText = "Previous Closing Balance"
            Gv1.Columns("PrevOPBal").IsVisible = False
            Gv1.Columns("MCC").HeaderText = "MCC"
            Gv1.Columns("BMCU").HeaderText = "BMCU"
            Gv1.Columns("OtherMilkShed").HeaderText = "Other Milk Shed"
            Gv1.Columns("Total").HeaderText = "Total"
            Gv1.Columns("Sour").HeaderText = "Sour"
            Gv1.Columns("Curd").HeaderText = "Curd"
            Gv1.Columns("HandlingGain").HeaderText = "Handling Gain"
            Gv1.Columns("TotalStock").HeaderText = "Total Stock"
            Gv1.Columns("MPF").HeaderText = "MPF"
            Gv1.Columns("Curd").HeaderText = "Curd"
            Gv1.Columns("HandlingLoss").HeaderText = "Handling Loss"
            Gv1.Columns("TransitTankerCurrent").HeaderText = "Transit Tanker"
            Gv1.Columns("TotalDisposal").HeaderText = "Total Disposal"
            Gv1.Columns("CLBal").HeaderText = "Closing Balance"
        ElseIf rbtnPlantMilkAccountAbstract.IsChecked Then
            Gv1.Columns("Particular1").HeaderText = "Particular"
            Gv1.Columns("Amount1").HeaderText = "Qty"
        ElseIf rbtnMCCMilkAccount.IsChecked Then
            Gv1.Columns("GrpDate").HeaderText = "Date"
            Gv1.Columns("OPBal").HeaderText = "Opening Balance"
            Gv1.Columns("TankerTrasitPrevious").HeaderText = "Transit Tanker"
            Gv1.Columns("GMBM").HeaderText = "Good Milk BM"
            Gv1.Columns("GMCM").HeaderText = "Good Milk CM"
            Gv1.Columns("GMTotal").HeaderText = "Total"
            Gv1.Columns("PTCBM").HeaderText = "PTC BM"
            Gv1.Columns("PTCCM").HeaderText = "PTC CM"
            Gv1.Columns("ProducerBM").HeaderText = "Producer BM"
            Gv1.Columns("ProducerCM").HeaderText = "Producer CM"
            Gv1.Columns("Total").HeaderText = "TOTAL"
            Gv1.Columns("MCCWNP").HeaderText = "Milk Transfer"
            Gv1.Columns("HandlingGain").HeaderText = "HANDLING GAIN"
            Gv1.Columns("GrandTotal").HeaderText = "Grand Total"
            Gv1.Columns("Sales").HeaderText = "Sales"
            Gv1.Columns("PTCCurld").HeaderText = "PTC CURLD"
            Gv1.Columns("Curld").HeaderText = "Curld"
            Gv1.Columns("MilkSentOther").HeaderText = "Sent to Other"
            Gv1.Columns("HandlingLoss").HeaderText = "HANDLING LOSS"
            Gv1.Columns("TransitTankerCurrent").HeaderText = "Transit tanker"
            Gv1.Columns("MPFDispatch").HeaderText = "MPF Despatch"
            Gv1.Columns("CLBal").HeaderText = "Closing Balance"
        ElseIf rbtnMCCMilkAccountAbstract.IsChecked Then
            Gv1.Columns("Particular1").HeaderText = "RECEIPTS"
            Gv1.Columns("Amount1").HeaderText = "Qty"
            Gv1.Columns("Particular2").HeaderText = "DISPOSALS"
            Gv1.Columns("Amount2").HeaderText = "Qty"
        End If

        Gv1.BestFitColumns()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset(False)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ' stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
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
    Function GetReportID() As String
        Dim str As String = MyBase.Form_ID
        If rbtnPlantMilkAccount.IsChecked Then
            str += "A"
        ElseIf rbtnPlantMilkAccountAbstract.IsChecked Then
            str += "B"
        ElseIf rbtnMCCMilkAccount.IsChecked Then
            str += "C"
        ElseIf rbtnMCCMilkAccountAbstract.IsChecked Then
            str += "D"
        End If
        Return str
    End Function
    Private Sub ExpExcel_Click(sender As Object, e As EventArgs) Handles ExpExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Gv1_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles Gv1.ViewCellFormatting
        Try
            'If clsCommon.myCdbl(Gv1.Rows(e.RowIndex).Cells("IsBOLD").Value) = 1 Then
            '    e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtShed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtShed._MYValidating
        Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"
        txtShed.Value = clsLocation.getFinder(whrCls, txtShed.Value, isButtonClicked)
        lblShed.Text = clsLocation.GetName(txtShed.Value, Nothing)
        If clsCommon.myLen(txtShed.Value) > 0 Then
            SetToDate()
        End If
        txtMCC.Value = ""
        lblMCC.Text = ""
    End Sub
    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub
    Sub SetToDate()
        If rbtnMonthWise.IsChecked Then
            txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
            txtToDateMonthWise.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        End If
        'If Not isLoad Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0
        ' If Not isLoad Then
        If clsCommon.myLen(txtShed.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
            Exit Sub
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.Plant_Code ='" & txtShed.Value & "'")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                txtToDate.Value = txtFromDate.Value
                Exit Sub
            End If
            txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

            If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If txtFromDate.Value.Month <> dtNxtPay.Month Then
                txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = txtFromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            txtFromDate.Value = today.AddDays(-dayDiff)
            txtToDate.Value = txtFromDate.Value.AddDays(6)
        End If
    End Sub



    Private Sub MyRadioButton6_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnMonthWise.ToggleStateChanged, rbtnPaymentCycleWise.ToggleStateChanged
        SetToDateSplit()
    End Sub

    Sub SetToDateSplit()
        If rbtnMonthWise.IsChecked Then
            SplitContainer2.Panel1Collapsed = True
            SplitContainer2.Panel2Collapsed = False
        ElseIf rbtnPaymentCycleWise.IsChecked Then
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.Panel2Collapsed = True
        End If
    End Sub

    Private Sub rbtnPlantMilkAccount_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnPlantMilkAccount.ToggleStateChanged, rbtnPlantMilkAccountAbstract.ToggleStateChanged, rbtnMCCMilkAccount.ToggleStateChanged, rbtnMCCMilkAccountAbstract.ToggleStateChanged
        RadGroupBox3.Visible = rbtnMCCMilkAccount.IsChecked OrElse rbtnMCCMilkAccountAbstract.IsChecked
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        If clsCommon.myLen(txtShed.Value) <= 0 Then
            txtShed.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please select shed", Me.Text)
            Exit Sub
        End If

        Dim qry As String = " select MCC_Code as Code,MCC_NAME as Name,Mcc_Code_VLC_Uploader as [Uploader Code] from TSPL_MCC_MASTER"
        Dim whrCls As String = " plant_Code='" + txtShed.Value + "' "
        txtMCC.Value = clsCommon.ShowSelectForm("MCC@WMAcc", qry, "Code", whrCls, txtMCC.Value, "Code", isButtonClicked)
        lblMCC.Text = clsMccMaster.GetName(txtMCC.Value, Nothing)
    End Sub
End Class

