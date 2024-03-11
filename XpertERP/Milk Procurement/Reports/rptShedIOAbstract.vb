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
Public Class rptShedIOAbstract
#Region "Variables"
    Dim FORMTYPE As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim screenCode As String = Nothing
    Dim documentCode As String = Nothing
    Dim listOfUsers As New ArrayList()
    Dim dt As DataTable = Nothing
    Dim GroupFooterCounterLine As Integer = 1


    Dim TotQtyKgReq As Decimal = 0
    Dim TotQtyLtrReq As Decimal = 0
    Dim TotFATKgReq As Decimal = 0
    Dim TotSNFKgReq As Decimal = 0
    Dim TotFATAmtReq As Decimal = 0
    Dim TotSnfAmtReq As Decimal = 0
    Dim TotAvgCostReq As Decimal = 0

    Dim TotQtyKgAck As Decimal = 0
    Dim TotQtyLtrAck As Decimal = 0
    Dim TotFATKgAck As Decimal = 0
    Dim TotSNFKgAck As Decimal = 0
    Dim TotFATAmtAck As Decimal = 0
    Dim TotSNFAmtAck As Decimal = 0
    Dim TotAvgCostAck As Decimal = 0
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
        txtToDate.Enabled = val
        txtShed.Enabled = val
        lblShed.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = GetReportID()
            Load_Data()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Load_Data()
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From-Date cannot be Greater than To-Date")
            End If
            Dim strTotal As String = ""
            Dim strTotalProcurement As String = ""
            Dim strOutPutDetail As String = ""
            Dim ArrShortageGross As Dictionary(Of String, clsTempFATSNFAmt) = Nothing
            Dim qry As String
            Dim ReportNo As Integer = 0
            If rbtnShed.IsChecked Then
                ReportNo = 1
            ElseIf rbtnShortageExcel.IsChecked Then
                ReportNo = 2
            ElseIf rbtnShortageRecovery.IsChecked Then
                ReportNo = 3
            ElseIf rbtnUnitWise.IsChecked Then
                ReportNo = 4
            ElseIf rbtnShedWiseMilkCost.IsChecked Then
                ReportNo = 5
                If txtBMSNFDedRate.Value <= 0 Then
                    txtBMSNFDedRate.Focus()
                    Throw New Exception("Please Enter FAT deduction Rate")
                End If
                If txtCMSNFDedRate.Value <= 0 Then
                    txtCMSNFDedRate.Focus()
                    Throw New Exception("Please Enter SNF deduction Rate")
                End If
            End If
            If rbtnMillBill.IsChecked Then
                dt = clsDB.GetMilkBill(txtMCC.arrValueMember, txtFromDate.Value, txtToDate.Value, GroupFooterCounterLine)
            Else
                Dim TotDeductionAmt As Decimal = 0
                qry = clsDB.GetShedIOAbstract(ReportNo, txtShed.Value, txtFromDate.Value, txtToDate.Value, strTotal, strTotalProcurement, ArrShortageGross, TotDeductionAmt, strOutPutDetail)
                dt = clsDBFuncationality.GetDataTable(qry)
                dt.Columns.Add("DashLine", GetType(String))
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    If rbtnShed.IsChecked Then
                        Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(strTotal)
                        Dim dtProcuremnt As DataTable = clsDBFuncationality.GetDataTable(strTotalProcurement)
                        Dim dtOutPutDetail As DataTable = clsDBFuncationality.GetDataTable(strOutPutDetail)
                        If dtTotal IsNot Nothing AndAlso dtTotal.Rows.Count > 0 Then
                            Dim indx As Integer = -1
                            Dim indxA2 As Integer = -1
                            For ii As Integer = 0 To dt.Rows.Count - 1
                                If indx = -1 Then
                                    If clsCommon.CompairString("O", clsCommon.myCstr(dt.Rows(ii)("IOType"))) = CompairStringResult.Equal Then
                                        indx = ii
                                    End If
                                End If

                                If clsCommon.CompairString("A2", clsCommon.myCstr(dt.Rows(ii)("Alpha"))) = CompairStringResult.Equal Then
                                    indxA2 = ii
                                End If
                            Next

                            If indx >= 0 Then
                                dt.Rows.RemoveAt(indx)
                            End If

                            Dim dr1 As DataRow = dt.NewRow()
                            dr1("SummaryParticularCode") = "MPF RECEIPTS :"
                            dt.Rows.Add(dr1)

                            If dtOutPutDetail IsNot Nothing And dtOutPutDetail.Rows.Count > 0 Then
                                dt.Merge(dtOutPutDetail, True, MissingSchemaAction.Ignore)
                            End If

                            Dim dr As DataRow = dt.NewRow()
                            dr("DashLine") = "1"
                            dr("Alpha") = "X"
                            dr("SummaryParticularName") = "Total Input"
                            dr("Stock_Qty") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("Stock_Qty")), 2, MidpointRounding.ToEven)
                            dr("QtyKg") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("QtyKg")), 2, MidpointRounding.ToEven)
                            dr("QtyLtr") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("QtyLtr")), 2, MidpointRounding.ToEven)
                            dr("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
                            dr("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
                            dr("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)
                            If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                dr("Fat_Per") = Math.Round(clsCommon.myCdbl(dr("Fat_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            End If
                            If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                dr("SNF_Per") = Math.Round(clsCommon.myCdbl(dr("SNF_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            End If
                            Dim dblTotalInputFATPer As Decimal = dr("Fat_Per")
                            Dim dblTotalInputSNFPer As Decimal = dr("SNF_Per")
                            Dim drtemp As DataRow = dt.NewRow
                            dt.Rows.InsertAt(drtemp, indx)
                            dt.Rows.InsertAt(dr, indx)
                            drtemp = dt.NewRow
                            dt.Rows.InsertAt(drtemp, indx)

                            dr = dt.NewRow()
                            dr("DashLine") = "1"
                            dr("Alpha") = "Y"
                            dr("SummaryParticularName") = "Total Output"
                            dr("Stock_Qty") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Stock_Qty")), 2, MidpointRounding.ToEven)
                            dr("QtyKg") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("QtyKg")), 2, MidpointRounding.ToEven)
                            dr("QtyLtr") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("QtyLtr")), 2, MidpointRounding.ToEven)
                            dr("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")), 3, MidpointRounding.ToEven)
                            dr("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")), 3, MidpointRounding.ToEven)
                            dr("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")), 2, MidpointRounding.ToEven)
                            If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                dr("Fat_Per") = Math.Round(clsCommon.myCdbl(dr("Fat_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            End If
                            If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                dr("SNF_Per") = Math.Round(clsCommon.myCdbl(dr("SNF_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            End If
                            Dim dblTotalOutputFATPer As Decimal = dr("Fat_Per")
                            Dim dblTotalOutputSNFPer As Decimal = dr("SNF_Per")
                            drtemp = dt.NewRow
                            dt.Rows.Add(drtemp)
                            dt.Rows.Add(dr)
                            drtemp = dt.NewRow
                            dt.Rows.Add(drtemp)

                            dr = dt.NewRow()
                            dr("DashLine") = "1"
                            dr("Alpha") = "Z"
                            dr("SummaryParticularName") = "Gain/Loss"
                            dr("Stock_Qty") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Stock_Qty")) - clsCommon.myCdbl(dtTotal.Rows(0)("Stock_Qty")), 2, MidpointRounding.ToEven)
                            dr("QtyKg") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("QtyKg")) - clsCommon.myCdbl(dtTotal.Rows(0)("QtyKg")), 2, MidpointRounding.ToEven)
                            dr("QtyLtr") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("QtyLtr")) - clsCommon.myCdbl(dtTotal.Rows(0)("QtyLtr")), 2, MidpointRounding.ToEven)
                            dr("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 3, MidpointRounding.ToEven)
                            dr("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 3, MidpointRounding.ToEven)
                            dr("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)
                            'If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                            '    dr("Fat_Per") = Math.Round(clsCommon.myCdbl(dr("Fat_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            'End If
                            'If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                            '    dr("SNF_Per") = Math.Round(clsCommon.myCdbl(dr("SNF_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                            'End If
                            dr("Fat_Per") = dblTotalOutputFATPer - dblTotalInputFATPer
                            dr("SNF_Per") = dblTotalOutputSNFPer - dblTotalInputSNFPer
                            dt.Rows.Add(dr)

                            If dtProcuremnt IsNot Nothing AndAlso dtProcuremnt.Rows.Count > 0 Then
                                dr = dt.NewRow()
                                dr("DashLine") = "1"
                                dr("Alpha") = "W"
                                dr("SummaryParticularName") = "Total Procurement"
                                dr("Stock_Qty") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("Stock_Qty")), 2, MidpointRounding.ToEven)
                                dr("QtyKg") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("QtyKg")), 2, MidpointRounding.ToEven)
                                dr("QtyLtr") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("QtyLtr")), 2, MidpointRounding.ToEven)
                                dr("Fat_KG") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("Fat_KG")), 3, MidpointRounding.ToEven)
                                dr("SNF_KG") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("SNF_KG")), 3, MidpointRounding.ToEven)
                                dr("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtProcuremnt.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)
                                If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                    dr("Fat_Per") = Math.Round(clsCommon.myCdbl(dr("Fat_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                                End If
                                If clsCommon.myCdbl(dr("QtyKg")) <> 0 Then
                                    dr("SNF_Per") = Math.Round(clsCommon.myCdbl(dr("SNF_KG")) * 100 / clsCommon.myCdbl(dr("QtyKg")), 2, MidpointRounding.ToEven)
                                End If
                                dt.Rows.InsertAt(dr, indxA2 + 1)
                            End If
                        End If
                    ElseIf rbtnShortageExcel.IsChecked OrElse rbtnShortageRecovery.IsChecked OrElse rbtnUnitWise.IsChecked Then
                        Dim dtOLD As DataTable = dt.Copy()
                        dt.Rows.Clear()
                        Dim IndexReq As Integer = 0
                        Dim IndexAck As Integer = 0
                        Dim strMainTransLoc As String = ""
                        For ii As Integer = 0 To dtOLD.Rows.Count - 1
                            If clsCommon.myLen(dtOLD.Rows(ii)("ReqType")) > 0 Or clsCommon.myLen(dtOLD.Rows(ii)("AckType")) > 0 Then
                                If Not clsCommon.CompairString(strMainTransLoc, clsCommon.myCstr(dtOLD.Rows(ii)("MainTranLoc"))) = CompairStringResult.Equal Then
                                    dt.Rows.Add()
                                    dt.Rows(dt.Rows.Count - 1)("MainTrans") = dtOLD.Rows(ii)("MainTrans")
                                    dt.Rows(dt.Rows.Count - 1)("MainTranLoc") = dtOLD.Rows(ii)("MainTranLoc")
                                    dt.Rows(dt.Rows.Count - 1)("MainTranName") = dtOLD.Rows(ii)("MainTranName")

                                    IndexReq = dt.Rows.Count - 1
                                    IndexAck = dt.Rows.Count - 1
                                    strMainTransLoc = clsCommon.myCstr(dtOLD.Rows(ii)("MainTranLoc"))
                                End If
                                If clsCommon.myLen(dtOLD.Rows(ii)("ReqType")) > 0 Then
                                    If IndexReq > IndexAck OrElse IndexReq > dt.Rows.Count - 1 Then
                                        dt.Rows.Add()
                                        dt.Rows(dt.Rows.Count - 1)("MainTrans") = dtOLD.Rows(ii)("MainTrans")
                                        dt.Rows(dt.Rows.Count - 1)("MainTranLoc") = dtOLD.Rows(ii)("MainTranLoc")
                                        dt.Rows(dt.Rows.Count - 1)("MainTranName") = dtOLD.Rows(ii)("MainTranName")
                                    End If
                                    dt.Rows(IndexReq)("ReqAlpha") = dtOLD.Rows(ii)("ReqAlpha")
                                    dt.Rows(IndexReq)("ReqType") = dtOLD.Rows(ii)("ReqType")
                                    dt.Rows(IndexReq)("ReqQtyKg") = dtOLD.Rows(ii)("ReqQtyKg")
                                    dt.Rows(IndexReq)("ReqQtyLtr") = dtOLD.Rows(ii)("ReqQtyLtr")
                                    dt.Rows(IndexReq)("ReqFat_KG") = dtOLD.Rows(ii)("ReqFat_KG")
                                    dt.Rows(IndexReq)("ReqSNF_KG") = dtOLD.Rows(ii)("ReqSNF_KG")
                                    dt.Rows(IndexReq)("ReqFat_Per") = dtOLD.Rows(ii)("ReqFat_Per")
                                    dt.Rows(IndexReq)("ReqSNF_Per") = dtOLD.Rows(ii)("ReqSNF_Per")
                                    dt.Rows(IndexReq)("ReqFat_Amt") = dtOLD.Rows(ii)("ReqFat_Amt")
                                    dt.Rows(IndexReq)("ReqSNF_Amt") = dtOLD.Rows(ii)("ReqSNF_Amt")
                                    dt.Rows(IndexReq)("ReqAvg_Cost") = dtOLD.Rows(ii)("ReqAvg_Cost")
                                    IndexReq += 1
                                End If
                                If clsCommon.myLen(dtOLD.Rows(ii)("AckType")) > 0 Then
                                    If IndexAck > IndexReq OrElse IndexAck > dt.Rows.Count - 1 Then
                                        dt.Rows.Add()
                                        dt.Rows(dt.Rows.Count - 1)("MainTrans") = dtOLD.Rows(ii)("MainTrans")
                                        dt.Rows(dt.Rows.Count - 1)("MainTranLoc") = dtOLD.Rows(ii)("MainTranLoc")
                                        dt.Rows(dt.Rows.Count - 1)("MainTranName") = dtOLD.Rows(ii)("MainTranName")
                                    End If
                                    dt.Rows(IndexAck)("AckAlpha") = dtOLD.Rows(ii)("AckAlpha")
                                    dt.Rows(IndexAck)("AckType") = dtOLD.Rows(ii)("AckType")
                                    dt.Rows(IndexAck)("AckQtyKg") = dtOLD.Rows(ii)("AckQtyKg")
                                    dt.Rows(IndexAck)("AckQtyLtr") = dtOLD.Rows(ii)("AckQtyLtr")
                                    dt.Rows(IndexAck)("AckFat_KG") = dtOLD.Rows(ii)("AckFat_KG")
                                    dt.Rows(IndexAck)("AckSNF_KG") = dtOLD.Rows(ii)("AckSNF_KG")
                                    dt.Rows(IndexAck)("AckFat_Per") = dtOLD.Rows(ii)("AckFat_Per")
                                    dt.Rows(IndexAck)("AckSNF_Per") = dtOLD.Rows(ii)("AckSNF_Per")
                                    dt.Rows(IndexAck)("AckFat_Amt") = dtOLD.Rows(ii)("AckFat_Amt")
                                    dt.Rows(IndexAck)("AckSNF_Amt") = dtOLD.Rows(ii)("AckSNF_Amt")
                                    dt.Rows(IndexAck)("AckAvg_Cost") = dtOLD.Rows(ii)("AckAvg_Cost")
                                    IndexAck += 1
                                End If
                            End If
                        Next
                        AddTotalRows(dt, "")

                        If rbtnShortageExcel.IsChecked Then
                            TotQtyKgReq = 0
                            TotQtyLtrReq = 0
                            TotFATKgReq = 0
                            TotSNFKgReq = 0
                            TotFATAmtReq = 0
                            TotSnfAmtReq = 0
                            TotAvgCostReq = 0

                            TotQtyKgAck = 0
                            TotQtyLtrAck = 0
                            TotFATKgAck = 0
                            TotSNFKgAck = 0
                            TotFATAmtAck = 0
                            TotSNFAmtAck = 0
                            TotAvgCostAck = 0

                            qry = clsDB.GetShedIOAbstract(1, txtShed.Value, txtFromDate.Value, txtToDate.Value, strTotal, strTotalProcurement, ArrShortageGross, TotDeductionAmt, strOutPutDetail)
                            dtOLD = clsDBFuncationality.GetDataTable(qry)
                            If dtOLD IsNot Nothing AndAlso dtOLD.Rows.Count > 0 Then
                                Dim drBlank As DataRow = dt.NewRow
                                dt.Rows.Add(drBlank)
                                IndexReq = dt.Rows.Count - 1
                                IndexAck = dt.Rows.Count - 1
                                Dim indexA4 As Integer = 0
                                For ii As Integer = 0 To dtOLD.Rows.Count - 1
                                    If clsCommon.myLen(dtOLD.Rows(ii)("IOType")) > 0 Then
                                        If clsCommon.CompairString("I", clsCommon.myCstr(dtOLD.Rows(ii)("IOType"))) = CompairStringResult.Equal Then
                                            If IndexReq > IndexAck OrElse IndexReq > dt.Rows.Count - 1 Then
                                                Dim drTemp As DataRow = dt.NewRow
                                                dt.Rows.InsertAt(drTemp, IndexReq)
                                            End If
                                            dt.Rows(IndexReq)("ReqAlpha") = dtOLD.Rows(ii)("Alpha")
                                            dt.Rows(IndexReq)("ReqType") = dtOLD.Rows(ii)("SummaryParticularName")
                                            dt.Rows(IndexReq)("ReqQtyKg") = dtOLD.Rows(ii)("QtyKg")
                                            dt.Rows(IndexReq)("ReqQtyLtr") = dtOLD.Rows(ii)("QtyLtr")
                                            dt.Rows(IndexReq)("ReqFat_KG") = dtOLD.Rows(ii)("Fat_KG")
                                            dt.Rows(IndexReq)("ReqSNF_KG") = dtOLD.Rows(ii)("SNF_KG")
                                            dt.Rows(IndexReq)("ReqFat_Per") = dtOLD.Rows(ii)("Fat_Per")
                                            dt.Rows(IndexReq)("ReqSNF_Per") = dtOLD.Rows(ii)("SNF_Per")
                                            dt.Rows(IndexReq)("ReqFat_Amt") = dtOLD.Rows(ii)("Fat_Amt")
                                            dt.Rows(IndexReq)("ReqSNF_Amt") = dtOLD.Rows(ii)("SNF_Amt")
                                            dt.Rows(IndexReq)("ReqAvg_Cost") = dtOLD.Rows(ii)("Avg_Cost")
                                            IndexReq += 1

                                            If clsCommon.CompairString(dtOLD.Rows(ii)("Alpha"), "A2") = CompairStringResult.Equal Then
                                                indexA4 = IndexReq
                                            End If

                                            TotQtyKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyKg"))
                                            TotQtyLtrReq += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyLtr"))
                                            TotFATKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_KG"))
                                            TotSNFKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_KG"))
                                            TotFATAmtReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_Amt"))
                                            TotSnfAmtReq += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_Amt"))
                                            TotAvgCostReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Avg_Cost"))
                                        End If
                                        If clsCommon.CompairString("O", clsCommon.myCstr(dtOLD.Rows(ii)("IOType"))) = CompairStringResult.Equal Then
                                            If IndexAck > IndexReq OrElse IndexAck > dt.Rows.Count - 1 Then
                                                Dim drTemp As DataRow = dt.NewRow
                                                dt.Rows.InsertAt(drTemp, IndexAck)
                                            End If
                                            dt.Rows(IndexAck)("AckAlpha") = dtOLD.Rows(ii)("Alpha")
                                            dt.Rows(IndexAck)("AckType") = dtOLD.Rows(ii)("SummaryParticularName")
                                            dt.Rows(IndexAck)("AckQtyKg") = dtOLD.Rows(ii)("QtyKg")
                                            dt.Rows(IndexAck)("AckQtyLtr") = dtOLD.Rows(ii)("QtyLtr")
                                            dt.Rows(IndexAck)("AckFat_KG") = dtOLD.Rows(ii)("Fat_KG")
                                            dt.Rows(IndexAck)("AckSNF_KG") = dtOLD.Rows(ii)("SNF_KG")
                                            dt.Rows(IndexAck)("AckFat_Per") = dtOLD.Rows(ii)("Fat_Per")
                                            dt.Rows(IndexAck)("AckSNF_Per") = dtOLD.Rows(ii)("SNF_Per")
                                            dt.Rows(IndexAck)("AckFat_Amt") = dtOLD.Rows(ii)("Fat_Amt")
                                            dt.Rows(IndexAck)("AckSNF_Amt") = dtOLD.Rows(ii)("SNF_Amt")
                                            dt.Rows(IndexAck)("AckAvg_Cost") = dtOLD.Rows(ii)("Avg_Cost")
                                            IndexAck += 1

                                            TotQtyKgAck += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyKg"))
                                            TotQtyLtrAck += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyLtr"))
                                            TotFATKgAck += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_KG"))
                                            TotSNFKgAck += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_KG"))
                                            TotFATAmtAck += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_Amt"))
                                            TotSNFAmtAck += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_Amt"))
                                            TotAvgCostAck += clsCommon.myCdbl(dtOLD.Rows(ii)("Avg_Cost"))
                                        End If
                                    End If
                                Next

                                Dim drTS As DataRow = dt.NewRow()
                                drTS("ReqType") = "Grand Input"
                                drTS("ReqQtyKg") = TotQtyKgReq
                                drTS("ReqQtyLtr") = TotQtyLtrReq
                                drTS("ReqFat_KG") = TotFATKgReq
                                drTS("ReqSNF_KG") = TotSNFKgReq
                                drTS("ReqFat_Amt") = TotFATAmtReq
                                drTS("ReqSNF_Amt") = TotSnfAmtReq
                                drTS("ReqAvg_Cost") = TotAvgCostReq
                                drTS("ReqFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgReq * 100, TotQtyKgReq), 2)
                                drTS("ReqSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgReq * 100, TotQtyKgReq), 2)
                                drTS("AckType") = "Grand Output"
                                drTS("AckQtyKg") = TotQtyKgAck
                                drTS("AckQtyLtr") = TotQtyLtrAck
                                drTS("AckFat_KG") = TotFATKgAck
                                drTS("AckSNF_KG") = TotSNFKgAck
                                drTS("AckFat_Amt") = TotFATAmtAck
                                drTS("AckSNF_Amt") = TotSNFAmtAck
                                drTS("AckAvg_Cost") = TotAvgCostAck
                                drTS("AckFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgAck * 100, TotQtyKgAck), 2)
                                drTS("AckSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgAck * 100, TotQtyKgAck), 2)
                                drTS("ShortageFATkg") = TotFATKgAck - TotFATKgReq
                                drTS("ShortageSNFkg") = TotSNFKgAck - TotSNFKgReq
                                drTS("ShortageFATAmt") = TotFATAmtAck - TotFATAmtReq
                                drTS("ShortageSNFAmt") = TotSNFAmtAck - TotSnfAmtReq
                                drTS("ShortageAmt") = TotAvgCostAck - TotAvgCostReq
                                dt.Rows.Add(drTS)


                                TotQtyKgReq = 0
                                TotQtyLtrReq = 0
                                TotFATKgReq = 0
                                TotSNFKgReq = 0
                                TotFATAmtReq = 0
                                TotSnfAmtReq = 0
                                TotAvgCostReq = 0

                                For ii As Integer = 0 To dtOLD.Rows.Count - 1
                                    If clsCommon.myLen(dtOLD.Rows(ii)("IOType")) > 0 Then
                                        If clsCommon.CompairString("I", clsCommon.myCstr(dtOLD.Rows(ii)("IOType"))) = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(dtOLD.Rows(ii)("Alpha"), "A2") = CompairStringResult.Equal Then
                                                TotQtyKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyKg"))
                                                TotQtyLtrReq += clsCommon.myCdbl(dtOLD.Rows(ii)("QtyLtr"))
                                                TotFATKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_KG"))
                                                TotSNFKgReq += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_KG"))
                                                TotFATAmtReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Fat_Amt"))
                                                TotSnfAmtReq += clsCommon.myCdbl(dtOLD.Rows(ii)("SNF_Amt"))
                                                TotAvgCostReq += clsCommon.myCdbl(dtOLD.Rows(ii)("Avg_Cost"))
                                            End If
                                        End If
                                    End If
                                Next

                                Dim drTP As DataRow = dt.NewRow()
                                drTP("ReqType") = "Total Procurement"
                                drTP("ReqQtyKg") = TotQtyKgReq
                                drTP("ReqQtyLtr") = TotQtyLtrReq
                                drTP("ReqFat_KG") = TotFATKgReq
                                drTP("ReqSNF_KG") = TotSNFKgReq
                                drTP("ReqFat_Amt") = TotFATAmtReq
                                drTP("ReqSNF_Amt") = TotSnfAmtReq
                                drTP("ReqAvg_Cost") = TotAvgCostReq
                                drTP("ReqFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgReq * 100, TotQtyKgReq), 2)
                                drTP("ReqSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgReq * 100, TotQtyKgReq), 2)
                                dt.Rows.InsertAt(drTP, indexA4)
                            End If
                        End If
                        If rbtnShortageRecovery.IsChecked OrElse rbtnUnitWise.IsChecked Then
                            Dim Tot_TIP_Amount As Decimal = 0
                            If ArrShortageGross IsNot Nothing AndAlso ArrShortageGross.Count > 0 Then
                                For ii As Integer = 0 To dt.Rows.Count - 1
                                    If ArrShortageGross.ContainsKey(clsCommon.myCstr(dt.Rows(ii)("MainTranLoc"))) Then
                                        Dim obj As clsTempFATSNFAmt = ArrShortageGross(clsCommon.myCstr(dt.Rows(ii)("MainTranLoc")))
                                        Dim dclAmt As Decimal = obj.Amt
                                        If rbtnUnitWise.IsChecked Then
                                            dclAmt = obj.Amt1
                                        End If
                                        dt.Rows(ii)("ShortageFATAmt") = Math.Round(((clsCommon.myCstr(dt.Rows(ii)("ShortageFATkg"))) * (clsCommon.myCDivide((dclAmt * 55), (obj.FAT * 100)))), 2)
                                        dt.Rows(ii)("ShortageSNFAmt") = Math.Round(((clsCommon.myCstr(dt.Rows(ii)("ShortageSNFkg"))) * (clsCommon.myCDivide((dclAmt * 45), (obj.SNF * 100)))), 2)
                                        dt.Rows(ii)("ShortageAmt") = clsCommon.myCdbl(dt.Rows(ii)("ShortageFATAmt")) + clsCommon.myCdbl(dt.Rows(ii)("ShortageSNFAmt"))
                                        dt.Rows(ii)("ReqAvg_Cost") = obj.Amt
                                        dt.Rows(ii)("NetPayableAmt") = Math.Round(obj.Amt + clsCommon.myCdbl(dt.Rows(ii)("ShortageAmt")), 2)
                                        Tot_TIP_Amount += obj.TIP_Amt
                                    Else
                                        dt.Rows(ii)("ReqAvg_Cost") = 0
                                        dt.Rows(ii)("ShortageFATAmt") = 0
                                        dt.Rows(ii)("ShortageSNFAmt") = 0
                                        dt.Rows(ii)("ShortageAmt") = 0
                                        dt.Rows(ii)("NetPayableAmt") = 0
                                    End If
                                Next
                            End If
                            If rbtnUnitWise.IsChecked Then
                                If (TotDeductionAmt + Tot_TIP_Amount) > 0 Then
                                    Dim drTemp As DataRow = dt.NewRow
                                    drTemp("MainTranName") = "DD Office Account"
                                    drTemp("ReqAvg_Cost") = (TotDeductionAmt + Tot_TIP_Amount)
                                    drTemp("NetPayableAmt") = (TotDeductionAmt + Tot_TIP_Amount)
                                    dt.Rows.Add(drTemp)
                                End If
                            End If
                        End If
                    ElseIf rbtnShedWiseMilkCost.IsChecked Then
                        Dim dtOLD As DataTable = dt.Copy()
                        dt = New DataTable()
                        dt.Columns.Add("Particular", GetType(String))
                        dt.Columns.Add("BM", GetType(Decimal))
                        dt.Columns.Add("CM", GetType(Decimal))
                        dt.Columns.Add("Total", GetType(Decimal))

                        dt.Rows.Add("Qty In KG", clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg")), clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg")), clsCommon.myCdbl(dtOLD.Rows(0)("QtyKg")))
                        dt.Rows.Add("QTY KG FAT", clsCommon.myCdbl(dtOLD.Rows(0)("B_Fat_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("C_Fat_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("Fat_KG")))
                        dt.Rows.Add("QTY KG SNF", clsCommon.myCdbl(dtOLD.Rows(0)("B_SNF_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("C_SNF_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("SNF_KG")))
                        dt.Rows.Add("Qty KG SOLIDS", clsCommon.myCdbl(dtOLD.Rows(0)("B_TS_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("C_TS_KG")), clsCommon.myCdbl(dtOLD.Rows(0)("TS_KG")))
                        dt.Rows.Add("Bulk FAT %", clsCommon.myCdbl(dtOLD.Rows(0)("B_Fat_Per")), clsCommon.myCdbl(dtOLD.Rows(0)("C_Fat_Per")), clsCommon.myCdbl(dtOLD.Rows(0)("Fat_Per")))
                        dt.Rows.Add("Bulk SNF %", clsCommon.myCdbl(dtOLD.Rows(0)("B_SNF_Per")), clsCommon.myCdbl(dtOLD.Rows(0)("C_SNF_Per")), clsCommon.myCdbl(dtOLD.Rows(0)("SNF_Per")))
                        dt.Rows.Add("Rate", clsCommon.myCdbl(dtOLD.Rows(0)("B_Rate")), clsCommon.myCdbl(dtOLD.Rows(0)("C_Rate")), 0)
                        dt.Rows.Add("Milk Value", clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")), clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value")), (clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")) + clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value"))))
                        dt.Rows.Add("SNF DED Rate", -1 * txtBMSNFDedRate.Value, -1 * txtCMSNFDedRate.Value, 0)
                        dt.Rows.Add("SNF DED Value", (-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg"))), (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg"))), ((-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg"))) + (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg")))))
                        dt.Rows.Add("Net Milk Value", clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")) + (-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg"))), clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value")) + (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg"))), ((clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")) + (-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg")))) + (clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value")) + (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg"))))))
                        dt.Rows.Add("As Per Bill", clsCommon.myCdbl(dtOLD.Rows(0)("B_Avg_Cost")), clsCommon.myCdbl(dtOLD.Rows(0)("C_Avg_Cost")), clsCommon.myCdbl(dtOLD.Rows(0)("Avg_Cost")))
                        dt.Rows.Add("Difference Bill", clsCommon.myCdbl(dtOLD.Rows(0)("B_Avg_Cost")) - (clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")) + (-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg")))), clsCommon.myCdbl(dtOLD.Rows(0)("C_Avg_Cost")) - (clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value")) + (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg")))), ((clsCommon.myCdbl(dtOLD.Rows(0)("Avg_Cost"))) - ((clsCommon.myCdbl(dtOLD.Rows(0)("B_Milk_Value")) + (-1 * txtBMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("B_QtyKg")))) + (clsCommon.myCdbl(dtOLD.Rows(0)("C_Milk_Value")) + (-1 * txtCMSNFDedRate.Value * clsCommon.myCdbl(dtOLD.Rows(0)("C_QtyKg")))))))
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
                clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            End If
            Gv1.BestFitColumns()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddTotalRows(ByRef dt As DataTable, ByVal strColPraticularName As String)
        Dim arr As New Dictionary(Of Integer, DataRow)
        TotQtyKgReq = 0
        TotQtyLtrReq = 0
        TotFATKgReq = 0
        TotSNFKgReq = 0
        TotFATAmtReq = 0
        TotSnfAmtReq = 0
        TotAvgCostReq = 0

        TotQtyKgAck = 0
        TotQtyLtrAck = 0
        TotFATKgAck = 0
        TotSNFKgAck = 0
        TotFATAmtAck = 0
        TotSNFAmtAck = 0
        TotAvgCostAck = 0

        Dim NetPayableAmt As Decimal = 0
        Dim strPreviousMainTran As String = clsCommon.myCstr(dt.Rows(0)("MainTrans"))
        Dim strPreviousMainTranLoc As String = clsCommon.myCstr(dt.Rows(0)("MainTranLoc"))
        Dim strPreviousMainTranLocName As String = clsCommon.myCstr(dt.Rows(0)("MainTranName"))

        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim flag As Boolean = False
            If clsCommon.CompairString(strPreviousMainTranLoc, clsCommon.myCstr(dt.Rows(ii)("MainTranLoc"))) = CompairStringResult.Equal Then
                TotQtyKgReq += clsCommon.myCdbl(dt.Rows(ii)("ReqQtyKg"))
                TotQtyLtrReq += clsCommon.myCdbl(dt.Rows(ii)("ReqQtyLtr"))
                TotFATKgReq += clsCommon.myCdbl(dt.Rows(ii)("ReqFat_KG"))
                TotSNFKgReq += clsCommon.myCdbl(dt.Rows(ii)("ReqSNF_KG"))
                TotFATAmtReq += clsCommon.myCdbl(dt.Rows(ii)("ReqFat_Amt"))
                TotSnfAmtReq += clsCommon.myCdbl(dt.Rows(ii)("ReqSNF_Amt"))
                TotAvgCostReq += clsCommon.myCdbl(dt.Rows(ii)("ReqAvg_Cost"))

                TotQtyKgAck += clsCommon.myCdbl(dt.Rows(ii)("AckQtyKg"))
                TotQtyLtrAck += clsCommon.myCdbl(dt.Rows(ii)("AckQtyLtr"))
                TotFATKgAck += clsCommon.myCdbl(dt.Rows(ii)("AckFat_KG"))
                TotSNFKgAck += clsCommon.myCdbl(dt.Rows(ii)("AckSNF_KG"))
                TotFATAmtAck += clsCommon.myCdbl(dt.Rows(ii)("AckFat_Amt"))
                TotSNFAmtAck += clsCommon.myCdbl(dt.Rows(ii)("AckSNF_Amt"))
                TotAvgCostAck += clsCommon.myCdbl(dt.Rows(ii)("AckAvg_Cost"))
            Else
                Dim drTS As DataRow = dt.NewRow()
                If rbtnShortageRecovery.IsChecked Or rbtnUnitWise.IsChecked Then
                    drTS("MainTrans") = strPreviousMainTran
                    drTS("MainTranLoc") = strPreviousMainTranLoc
                    drTS("MainTranName") = strPreviousMainTranLocName
                End If
                drTS("ReqType") = "Total Input"
                drTS("ReqQtyKg") = TotQtyKgReq
                drTS("ReqQtyLtr") = TotQtyLtrReq
                drTS("ReqFat_KG") = TotFATKgReq
                drTS("ReqSNF_KG") = TotSNFKgReq
                drTS("ReqFat_Amt") = TotFATAmtReq
                drTS("ReqSNF_Amt") = TotSnfAmtReq
                drTS("ReqAvg_Cost") = TotAvgCostReq
                drTS("ReqFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgReq * 100, TotQtyKgReq), 2)
                drTS("ReqSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgReq * 100, TotQtyKgReq), 2)
                drTS("AckType") = "Total Output"
                drTS("AckQtyKg") = TotQtyKgAck
                drTS("AckQtyLtr") = TotQtyLtrAck
                drTS("AckFat_KG") = TotFATKgAck
                drTS("AckSNF_KG") = TotSNFKgAck
                drTS("AckFat_Amt") = TotFATAmtAck
                drTS("AckSNF_Amt") = TotSNFAmtAck
                drTS("AckAvg_Cost") = TotAvgCostAck
                drTS("AckFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgAck * 100, TotQtyKgAck), 2)
                drTS("AckSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgAck * 100, TotQtyKgAck), 2)
                drTS("ShortageFATkg") = TotFATKgAck - TotFATKgReq
                drTS("ShortageSNFkg") = TotSNFKgAck - TotSNFKgReq

                drTS("ShortageFATAmt") = TotFATAmtAck - TotFATAmtReq
                drTS("ShortageSNFAmt") = TotSNFAmtAck - TotSnfAmtReq
                drTS("ShortageAmt") = TotAvgCostAck - TotAvgCostReq



                TotQtyKgReq = clsCommon.myCdbl(dt.Rows(ii)("ReqQtyKg"))
                TotQtyLtrReq = clsCommon.myCdbl(dt.Rows(ii)("ReqQtyLtr"))
                TotFATKgReq = clsCommon.myCdbl(dt.Rows(ii)("ReqFat_KG"))
                TotSNFKgReq = clsCommon.myCdbl(dt.Rows(ii)("ReqSNF_KG"))
                TotFATAmtReq = clsCommon.myCdbl(dt.Rows(ii)("ReqFat_Amt"))
                TotSnfAmtReq = clsCommon.myCdbl(dt.Rows(ii)("ReqSNF_Amt"))
                TotAvgCostReq = clsCommon.myCdbl(dt.Rows(ii)("ReqAvg_Cost"))

                TotQtyKgAck = clsCommon.myCdbl(dt.Rows(ii)("AckQtyKg"))
                TotQtyLtrAck = clsCommon.myCdbl(dt.Rows(ii)("AckQtyLtr"))
                TotFATKgAck = clsCommon.myCdbl(dt.Rows(ii)("AckFat_KG"))
                TotSNFKgAck = clsCommon.myCdbl(dt.Rows(ii)("AckSNF_KG"))
                TotFATAmtAck = clsCommon.myCdbl(dt.Rows(ii)("AckFat_Amt"))
                TotSNFAmtAck = clsCommon.myCdbl(dt.Rows(ii)("AckSNF_Amt"))
                TotAvgCostAck = clsCommon.myCdbl(dt.Rows(ii)("AckAvg_Cost"))
                strPreviousMainTran = clsCommon.myCstr(dt.Rows(ii)("MainTrans"))
                strPreviousMainTranLoc = clsCommon.myCstr(dt.Rows(ii)("MainTranLoc"))
                strPreviousMainTranLocName = clsCommon.myCstr(dt.Rows(ii)("MainTranName"))
                arr.Add(ii, drTS)

            End If

            If dt.Rows.Count - 1 = ii Then
                Dim drTS As DataRow = dt.NewRow()
                If rbtnShortageRecovery.IsChecked Or rbtnUnitWise.IsChecked Then
                    drTS("MainTrans") = strPreviousMainTran
                    drTS("MainTranLoc") = strPreviousMainTranLoc
                    drTS("MainTranName") = strPreviousMainTranLocName
                End If
                drTS("ReqType") = "Total Input"
                drTS("ReqQtyKg") = TotQtyKgReq
                drTS("ReqQtyLtr") = TotQtyLtrReq
                drTS("ReqFat_KG") = TotFATKgReq
                drTS("ReqSNF_KG") = TotSNFKgReq
                drTS("ReqFat_Amt") = TotFATAmtReq
                drTS("ReqSNF_Amt") = TotSnfAmtReq
                drTS("ReqAvg_Cost") = TotAvgCostReq
                drTS("ReqFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgReq * 100, TotQtyKgReq), 2)
                drTS("ReqSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgReq * 100, TotQtyKgReq), 2)
                drTS("AckType") = "Total Output"
                drTS("AckQtyKg") = TotQtyKgAck
                drTS("AckQtyLtr") = TotQtyLtrAck
                drTS("AckFat_KG") = TotFATKgAck
                drTS("AckSNF_KG") = TotSNFKgAck
                drTS("AckFat_Amt") = TotFATAmtAck
                drTS("AckSNF_Amt") = TotSNFAmtAck
                drTS("AckAvg_Cost") = TotAvgCostAck
                drTS("AckFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgAck * 100, TotQtyKgAck), 2)
                drTS("AckSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgAck * 100, TotQtyKgAck), 2)
                drTS("ShortageFATkg") = TotFATKgAck - TotFATKgReq
                drTS("ShortageSNFkg") = TotSNFKgAck - TotSNFKgReq
                drTS("ShortageFATAmt") = TotFATAmtAck - TotFATAmtReq
                drTS("ShortageSNFAmt") = TotSNFAmtAck - TotSnfAmtReq
                drTS("ShortageAmt") = TotAvgCostAck - TotAvgCostReq

                arr.Add(ii + 1, drTS)
            End If
        Next

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            If rbtnShortageRecovery.IsChecked Or rbtnUnitWise.IsChecked Then
                dt.Rows.Clear()
                For ii As Integer = 0 To arr.Count - 1
                    Dim Key As Integer = arr.Keys(ii)
                    If clsCommon.CompairString(clsCommon.myCstr(arr(Key)("MainTrans")), "MCC") = CompairStringResult.Equal Then
                        dt.Rows.Add(arr(Key))
                    End If
                Next

                Dim TotShrtFATKg As Decimal = 0
                Dim TotShrtSNFKg As Decimal = 0
                For ii As Integer = 0 To dt.Rows.Count - 1
                    TotShrtFATKg += clsCommon.myCdbl(dt.Rows(ii)("ShortageFATkg"))
                    TotShrtSNFKg += clsCommon.myCdbl(dt.Rows(ii)("ShortageSNFkg"))
                Next

                For ii As Integer = 0 To dt.Rows.Count - 1
                    If TotShrtFATKg > 0 OrElse clsCommon.myCdbl(dt.Rows(ii)("ShortageFATkg")) > -1 Then
                        dt.Rows(ii)("ShortageFATkg") = 0
                        dt.Rows(ii)("ShortageFATAmt") = 0
                    End If
                    If TotShrtSNFKg > 0 OrElse clsCommon.myCdbl(dt.Rows(ii)("ShortageSNFkg")) > -1 Then
                        dt.Rows(ii)("ShortageSNFkg") = 0
                        dt.Rows(ii)("ShortageSNFAmt") = 0
                    End If
                    dt.Rows(ii)("ShortageAmt") = clsCommon.myCdbl(dt.Rows(ii)("ShortageFATAmt")) + clsCommon.myCdbl(dt.Rows(ii)("ShortageSNFAmt"))
                Next
            Else
                For ii As Integer = arr.Count - 1 To 0 Step -1
                    Dim Key As Integer = arr.Keys(ii)
                    Dim dr As DataRow = dt.NewRow()
                    dt.Rows.InsertAt(dr, Key)
                    dt.Rows.InsertAt(arr(Key), Key)
                Next
                If Not rbtnShortageExcel.IsChecked Then
                    TotQtyKgReq = 0
                    TotQtyLtrReq = 0
                    TotFATKgReq = 0
                    TotSNFKgReq = 0
                    TotFATAmtReq = 0
                    TotSnfAmtReq = 0
                    TotAvgCostReq = 0

                    TotQtyKgAck = 0
                    TotQtyLtrAck = 0
                    TotFATKgAck = 0
                    TotSNFKgAck = 0
                    TotFATAmtAck = 0
                    TotSNFAmtAck = 0
                    TotAvgCostAck = 0

                    For ii As Integer = 0 To arr.Count - 1
                        Dim Key As Integer = arr.Keys(ii)

                        TotQtyKgReq += clsCommon.myCdbl(arr(Key)("ReqQtyKg"))
                        TotQtyLtrReq += clsCommon.myCdbl(arr(Key)("ReqQtyLtr"))
                        TotFATKgReq += clsCommon.myCdbl(arr(Key)("ReqFat_KG"))
                        TotSNFKgReq += clsCommon.myCdbl(arr(Key)("ReqSNF_KG"))
                        TotFATAmtReq += clsCommon.myCdbl(arr(Key)("ReqFat_Amt"))
                        TotSnfAmtReq += clsCommon.myCdbl(arr(Key)("ReqSNF_Amt"))
                        TotAvgCostReq += clsCommon.myCdbl(arr(Key)("ReqAvg_Cost"))

                        TotQtyKgAck += clsCommon.myCdbl(arr(Key)("AckQtyKg"))
                        TotQtyLtrAck += clsCommon.myCdbl(arr(Key)("AckQtyLtr"))
                        TotFATKgAck += clsCommon.myCdbl(arr(Key)("AckFat_KG"))
                        TotSNFKgAck += clsCommon.myCdbl(arr(Key)("AckSNF_KG"))
                        TotFATAmtAck += clsCommon.myCdbl(arr(Key)("AckFat_Amt"))
                        TotSNFAmtAck += clsCommon.myCdbl(arr(Key)("AckSNF_Amt"))
                        TotAvgCostAck += clsCommon.myCdbl(arr(Key)("AckAvg_Cost"))
                    Next

                    Dim drTS As DataRow = dt.NewRow()
                    drTS("ReqType") = "Grand Input"
                    drTS("ReqQtyKg") = TotQtyKgReq
                    drTS("ReqQtyLtr") = TotQtyLtrReq
                    drTS("ReqFat_KG") = TotFATKgReq
                    drTS("ReqSNF_KG") = TotSNFKgReq
                    drTS("ReqFat_Amt") = TotFATAmtReq
                    drTS("ReqSNF_Amt") = TotSnfAmtReq
                    drTS("ReqAvg_Cost") = TotAvgCostReq
                    drTS("ReqFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgReq * 100, TotQtyKgReq), 2)
                    drTS("ReqSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgReq * 100, TotQtyKgReq), 2)
                    drTS("AckType") = "Grand Output"
                    drTS("AckQtyKg") = TotQtyKgAck
                    drTS("AckQtyLtr") = TotQtyLtrAck
                    drTS("AckFat_KG") = TotFATKgAck
                    drTS("AckSNF_KG") = TotSNFKgAck
                    drTS("AckFat_Amt") = TotFATAmtAck
                    drTS("AckSNF_Amt") = TotSNFAmtAck
                    drTS("AckAvg_Cost") = TotAvgCostAck
                    drTS("AckFat_Per") = Math.Round(clsCommon.myCDivide(TotFATKgAck * 100, TotQtyKgAck), 2)
                    drTS("AckSNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKgAck * 100, TotQtyKgAck), 2)
                    drTS("ShortageFATkg") = TotFATKgAck - TotFATKgReq
                    drTS("ShortageSNFkg") = TotSNFKgAck - TotSNFKgReq
                    drTS("ShortageFATAmt") = TotFATAmtAck - TotFATAmtReq
                    drTS("ShortageSNFAmt") = TotSNFAmtAck - TotSnfAmtReq
                    drTS("ShortageAmt") = TotAvgCostAck - TotAvgCostReq
                    dt.Rows.Add(drTS)
                End If

            End If
        End If
    End Sub


    Sub SetGrifFormat()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        If rbtnShed.IsChecked Then
            Gv1.Columns("Alpha").HeaderText = "Alpha"
            Gv1.Columns("SummaryParticularCode").HeaderText = "Particular Code"

            Gv1.Columns("SummaryParticularName").HeaderText = "Particular"
            Gv1.Columns("SummaryParticularName").IsVisible = True
            Gv1.Columns("QtyKg").HeaderText = "Qty KG"
            Gv1.Columns("QtyKg").IsVisible = True
            Gv1.Columns("QtyLtr").HeaderText = "Qty Ltr"
            Gv1.Columns("QtyLtr").IsVisible = True
            Gv1.Columns("Fat_Per").HeaderText = "FAT %"
            Gv1.Columns("Fat_Per").IsVisible = True
            Gv1.Columns("SNF_Per").HeaderText = "SNF %"
            Gv1.Columns("SNF_Per").IsVisible = True
            Gv1.Columns("Fat_KG").HeaderText = "FAT Kg"
            Gv1.Columns("Fat_KG").IsVisible = True
            Gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
            Gv1.Columns("SNF_KG").IsVisible = True
            Gv1.Columns("Avg_Cost").HeaderText = "Amount"
            Gv1.Columns("Avg_Cost").IsVisible = True
        ElseIf rbtnShortageExcel.IsChecked Then
            Gv1.Columns("MainTranLoc").HeaderText = "MCC"
            Gv1.Columns("MainTranLoc").IsVisible = True

            Gv1.Columns("MainTranName").HeaderText = "MCC Name"
            Gv1.Columns("MainTranName").IsVisible = True

            Gv1.Columns("ReqType").HeaderText = "Proc Type"
            Gv1.Columns("ReqType").IsVisible = True

            Gv1.Columns("ReqQtyKg").HeaderText = "Req Qty KG"
            Gv1.Columns("ReqQtyKg").IsVisible = True

            Gv1.Columns("ReqQtyLtr").HeaderText = "Req Qty Ltr"
            Gv1.Columns("ReqQtyLtr").IsVisible = True

            Gv1.Columns("ReqFat_KG").HeaderText = "Req FAT KG"
            Gv1.Columns("ReqFat_KG").IsVisible = True

            Gv1.Columns("ReqSNF_KG").HeaderText = "Req SNF KG"
            Gv1.Columns("ReqSNF_KG").IsVisible = True

            Gv1.Columns("ReqFat_Per").HeaderText = "Req FAT%"
            Gv1.Columns("ReqFat_Per").IsVisible = True

            Gv1.Columns("ReqSNF_Per").HeaderText = "Req SNF%"
            Gv1.Columns("ReqSNF_Per").IsVisible = True

            Gv1.Columns("AckType").HeaderText = "Ack Type"
            Gv1.Columns("AckType").IsVisible = True

            Gv1.Columns("AckQtyKg").HeaderText = "Ack Qty KG"
            Gv1.Columns("AckQtyKg").IsVisible = True

            Gv1.Columns("AckQtyLtr").HeaderText = "Ack Qty Ltr"
            Gv1.Columns("AckQtyLtr").IsVisible = True

            Gv1.Columns("AckFat_KG").HeaderText = "Ack FAT KG"
            Gv1.Columns("AckFat_KG").IsVisible = True

            Gv1.Columns("AckSNF_KG").HeaderText = "Ack SNF KG"
            Gv1.Columns("AckSNF_KG").IsVisible = True

            Gv1.Columns("AckFat_Per").HeaderText = "Ack FAT%"
            Gv1.Columns("AckFat_Per").IsVisible = True

            Gv1.Columns("AckSNF_Per").HeaderText = "Ack SNF%"
            Gv1.Columns("AckSNF_Per").IsVisible = True

            Gv1.Columns("ShortageFATkg").HeaderText = "Shortage FAT KG"
            Gv1.Columns("ShortageFATkg").IsVisible = True

            Gv1.Columns("ShortageSNFkg").HeaderText = "Shortage SNF Kg"
            Gv1.Columns("ShortageSNFkg").IsVisible = True


        ElseIf rbtnShortageRecovery.IsChecked Then
            Gv1.Columns("MainTranLoc").HeaderText = "MCC"
            Gv1.Columns("MainTranLoc").IsVisible = True

            Gv1.Columns("MainTranName").HeaderText = "MCC Name"
            Gv1.Columns("MainTranName").IsVisible = True

            Gv1.Columns("ShortageFATkg").HeaderText = "Shortage FAT KG"
            Gv1.Columns("ShortageFATkg").IsVisible = True

            Gv1.Columns("ShortageSNFkg").HeaderText = "Shortage SNF KG"
            Gv1.Columns("ShortageSNFkg").IsVisible = True

            Gv1.Columns("ShortageFATAmt").HeaderText = "Shortage FAT Amt"
            Gv1.Columns("ShortageFATAmt").IsVisible = True

            Gv1.Columns("ShortageSNFAmt").HeaderText = "Shortage SNF Amt"
            Gv1.Columns("ShortageSNFAmt").IsVisible = True

            Gv1.Columns("ShortageAmt").HeaderText = "Shortage Amt"
            Gv1.Columns("ShortageAmt").IsVisible = True

        ElseIf rbtnUnitWise.IsChecked Then
            Gv1.Columns("MainTranLoc").HeaderText = "MCC"
            Gv1.Columns("MainTranLoc").IsVisible = True

            Gv1.Columns("MainTranName").HeaderText = "MCC Name"
            Gv1.Columns("MainTranName").IsVisible = True

            Gv1.Columns("ReqAvg_Cost").HeaderText = "Milk Amount"
            Gv1.Columns("ReqAvg_Cost").IsVisible = True

            Gv1.Columns("ShortageFATkg").HeaderText = "Shortage FAT KG"
            Gv1.Columns("ShortageFATkg").IsVisible = True

            Gv1.Columns("ShortageSNFkg").HeaderText = "Shortage SNF KG"
            Gv1.Columns("ShortageSNFkg").IsVisible = True

            Gv1.Columns("ShortageFATAmt").HeaderText = "Shortage FAT Amt"
            Gv1.Columns("ShortageFATAmt").IsVisible = True

            Gv1.Columns("ShortageSNFAmt").HeaderText = "Shortage SNF Amt"
            Gv1.Columns("ShortageSNFAmt").IsVisible = True

            Gv1.Columns("ShortageAmt").HeaderText = "Total Amt Recovery"
            Gv1.Columns("ShortageAmt").IsVisible = True

            Gv1.Columns("NetPayableAmt").HeaderText = "Net Amount Payable"
            Gv1.Columns("NetPayableAmt").IsVisible = True
        ElseIf rbtnShedWiseMilkCost.IsChecked Then

            Gv1.Columns("Particular").HeaderText = "Particular"
            Gv1.Columns("Particular").IsVisible = True

            Gv1.Columns("BM").HeaderText = "BM"
            Gv1.Columns("BM").IsVisible = True
            Gv1.Columns("BM").FormatString = "{0:F2}"


            Gv1.Columns("CM").HeaderText = "CM"
            Gv1.Columns("CM").IsVisible = True
            Gv1.Columns("CM").FormatString = "{0:F2}"

            Gv1.Columns("Total").HeaderText = "Total"
            Gv1.Columns("Total").IsVisible = True
            Gv1.Columns("Total").FormatString = "{0:F2}"
        ElseIf rbtnMillBill.IsChecked Then
            Gv1.Columns("Plant Name").HeaderText = "Plant"
            Gv1.Columns("Plant Name").IsVisible = True

            Gv1.Columns("MCC Name").HeaderText = "MCC"
            Gv1.Columns("MCC Name").IsVisible = True


            Gv1.Columns("Doc Date").HeaderText = "Date"
            Gv1.Columns("Doc Date").IsVisible = True


            Gv1.Columns("Shift").HeaderText = "Shift"
            Gv1.Columns("Shift").IsVisible = True

            Gv1.Columns("Route Name").HeaderText = "Route"
            Gv1.Columns("Route Name").IsVisible = True


            Gv1.Columns("Vlc Uploader Code").HeaderText = "VLC Uploader No"
            Gv1.Columns("Vlc Uploader Code").IsVisible = True


            Gv1.Columns("VLC Name").HeaderText = "VLC"
            Gv1.Columns("VLC Name").IsVisible = True


            Gv1.Columns("VSP Name").HeaderText = "VSP"
            Gv1.Columns("VSP Name").IsVisible = True

            Gv1.Columns("TYPE").HeaderText = "TYPE"
            Gv1.Columns("TYPE").IsVisible = True


            Gv1.Columns("Milk Weight(LTR)").HeaderText = "Qty-LTR"
            Gv1.Columns("Milk Weight(LTR)").IsVisible = True
            Gv1.Columns("Milk Weight(LTR)").FormatString = "{0:F2}"


            Gv1.Columns("Milk Weight(KG)").HeaderText = "Qty-KG"
            Gv1.Columns("Milk Weight(KG)").IsVisible = True
            Gv1.Columns("Milk Weight(KG)").FormatString = "{0:F2}"

            Gv1.Columns("FAT(%)").HeaderText = "FAT(%)"
            Gv1.Columns("FAT(%)").IsVisible = True
            Gv1.Columns("FAT(%)").FormatString = "{0:F2}"

            Gv1.Columns("SNF(%)").HeaderText = "SNF(%)"
            Gv1.Columns("SNF(%)").IsVisible = True
            Gv1.Columns("SNF(%)").FormatString = "{0:F2}"

            Gv1.Columns("TSDDCS_Rate").HeaderText = "Rate"
            Gv1.Columns("TSDDCS_Rate").IsVisible = True
            Gv1.Columns("TSDDCS_Rate").FormatString = "{0:F2}"

            Gv1.Columns("NET_AMOUNT").HeaderText = "VALUE"
            Gv1.Columns("NET_AMOUNT").IsVisible = True
            Gv1.Columns("NET_AMOUNT").FormatString = "{0:F2}"

            Gv1.Columns("TIP_Amount").HeaderText = "TIP"
            Gv1.Columns("TIP_Amount").IsVisible = True
            Gv1.Columns("TIP_Amount").FormatString = "{0:F2}"

            Gv1.Columns("SRN Amount").HeaderText = "Amount"
            Gv1.Columns("SRN Amount").IsVisible = True
            Gv1.Columns("SRN Amount").FormatString = "{0:F2}"

            Gv1.Columns("SM Milk Weight(LTR)").HeaderText = "SOUR MILK Qty-LTR"
            Gv1.Columns("SM Milk Weight(LTR)").IsVisible = True
            Gv1.Columns("SM Milk Weight(LTR)").FormatString = "{0:F2}"

            Gv1.Columns("SM Milk Weight(KG)").HeaderText = "SOUR MILK Qty-KG"
            Gv1.Columns("SM Milk Weight(KG)").IsVisible = True
            Gv1.Columns("SM Milk Weight(KG)").FormatString = "{0:F2}"

            Gv1.Columns("SM FAT(%)").HeaderText = "SOUR MILK FAT%"
            Gv1.Columns("SM FAT(%)").IsVisible = True
            Gv1.Columns("SM FAT(%)").FormatString = "{0:F2}"

            Gv1.Columns("SMValue").HeaderText = "SOUR MILK Amount"
            Gv1.Columns("SMValue").IsVisible = True
            Gv1.Columns("SMValue").FormatString = "{0:F2}"

            Gv1.Columns("CU Milk Weight(LTR)").HeaderText = "CURDLED Qty-LTR"
            Gv1.Columns("CU Milk Weight(LTR)").IsVisible = True
            Gv1.Columns("CU Milk Weight(LTR)").FormatString = "{0:F2}"

            Gv1.Columns("CU Milk Weight(KG)").HeaderText = "CURDLED Qty-KG"
            Gv1.Columns("CU Milk Weight(KG)").IsVisible = True
            Gv1.Columns("CU Milk Weight(KG)").FormatString = "{0:F2}"

            Gv1.Columns("Tot Milk Weight(LTR)").HeaderText = "Total Qty-LTR"
            Gv1.Columns("Tot Milk Weight(LTR)").IsVisible = True
            Gv1.Columns("Tot Milk Weight(LTR)").FormatString = "{0:F2}"

            Gv1.Columns("Tot Milk Weight(KG)").HeaderText = "Total Qty-KG"
            Gv1.Columns("Tot Milk Weight(KG)").IsVisible = True
            Gv1.Columns("Tot Milk Weight(KG)").FormatString = "{0:F2}"

            Gv1.Columns("TotAmount").HeaderText = "Total Amount"
            Gv1.Columns("TotAmount").IsVisible = True
            Gv1.Columns("TotAmount").FormatString = "{0:F2}"


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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
        If rbtnShortageExcel.IsChecked Then
            str += "M"
        ElseIf rbtnShortageRecovery.IsChecked Then
            str += "S"
        ElseIf rbtnUnitWise.IsChecked Then
            str += "U"
        ElseIf rbtnShedWiseMilkCost.IsChecked Then
            str += "H"
        ElseIf rbtnMillBill.IsChecked Then
            str += "B"
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
                clsCommon.MyMessageBoxShow("No data found to export", Me.Text)
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
    End Sub
    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub
    Sub SetToDate()
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
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to print")
            End If
            If rbtnShed.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + lblShed.Text
                obj.ReportName2 = "PERIOD FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                obj.ReportName3 = "STATEMENT SHOWING THE INPUT/OUTPUT KG-FAT AND KG-SNF ACCOUNT"
                obj.ShowPageNo = False

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SummaryParticularCode", "Unit Code", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SummaryParticularName", "Unit Name", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("QtyLtr", "Qty Ltr", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("QtyKg", "Qty Kg", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_KG", "Fat KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF_KG", "SNF KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_Per", "Fat %", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF_Per", "SNF %", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Landscap, "DashLine", "1")
            ElseIf rbtnShortageExcel.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + lblShed.Text
                obj.ReportName2 = "PERIOD FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                obj.ReportName3 = "Shortage Excel"
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 240

                obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)
                Dim objMergeColumn As New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "AS PER MILK REQUISITION"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("ReqType")
                objMergeColumn.arrColumn.Add("ReqQtyLtr")
                objMergeColumn.arrColumn.Add("ReqQtyKg")
                objMergeColumn.arrColumn.Add("ReqFat_KG")
                objMergeColumn.arrColumn.Add("ReqSNF_KG")
                objMergeColumn.arrColumn.Add("ReqFat_Per")
                objMergeColumn.arrColumn.Add("ReqSNF_Per")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "ACKNOWLEDGENENT"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("AckType")
                objMergeColumn.arrColumn.Add("AckQtyLtr")
                objMergeColumn.arrColumn.Add("AckQtyKg")
                objMergeColumn.arrColumn.Add("AckFat_KG")
                objMergeColumn.arrColumn.Add("AckSNF_KG")
                objMergeColumn.arrColumn.Add("AckFat_Per")
                objMergeColumn.arrColumn.Add("AckSNF_Per")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "SHORTAGE"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("ShortageFATkg")
                objMergeColumn.arrColumn.Add("ShortageSNFkg")
                obj.arrMergeColumn.Add(objMergeColumn)

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranLoc", "MCC", False, DosPrintAlignment.Left, 5, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranName", "MCC Name", False, DosPrintAlignment.Left, 12, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqType", "PROC-TYPE", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqQtyLtr", "Qty Ltr", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqQtyKg", "Qty KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqFat_KG", "FAT KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqSNF_KG", "SNF KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqFat_Per", "FAT%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqSNF_Per", "SNF%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckType", "ACK-TYPE", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckQtyLtr", "Qty Ltr", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckQtyKg", "Qty KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckFat_KG", "FAT KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckSNF_KG", "SNF KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckFat_Per", "FAT%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AckSNF_Per", "SNF%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageFATkg", "FAT KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageSNFkg", "SNF KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Three))

                obj.Print(obj, dt, PageSetup.Landscap)
            ElseIf rbtnShortageRecovery.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + lblShed.Text
                obj.ReportName2 = "PERIOD FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                obj.ReportName3 = "STATEMENT SHOWING THE RECOVERABLE AMOUNT OF SHORTAGE KG-FAT, KG-SNF"
                obj.ShowPageNo = False

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranLoc", "MCC", True, DosPrintAlignment.Left, 4, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranName", "MCC Name", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageFATkg", "Shortage FAT KG", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageSNFkg", "Shortage SNF KG", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageFATAmt", "Shortage FAT Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageSNFAmt", "Shortage SNF Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageAmt", "Total Recoverable Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))

                obj.Print(obj, dt, PageSetup.Landscap)
            ElseIf rbtnUnitWise.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + lblShed.Text
                obj.ReportName2 = "PERIOD FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                obj.ReportName3 = "STATEMENT SHOWING THE RECOVERABLE AMOUNT OF SHORTAGE KG-FAT, KG-SNF"
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 150
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranLoc", "MCC", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MainTranName", "MCC Name", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ReqAvg_Cost", "Milk Amount", False, DosPrintAlignment.Right, 8, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageFATkg", "Shrt FAT KG", False, DosPrintAlignment.Right, 8, True, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageSNFkg", "Shrt SNF KG", False, DosPrintAlignment.Right, 8, True, DecimalPlaces.Three))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageFATAmt", "Shrt FAT Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageSNFAmt", "Shrt SNF Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ShortageAmt", "    Total Amount Recovery", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("NetPayableAmt", "Payable Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))

#Region "Deduction Foolter"
                Dim StrDeductionHead As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(" &
               " ISNULL(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,'')) as Alies_Name" &
               " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
               " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
               " left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode " &
               "  where TSPL_DEDUCTION_MASTER.OTHERS_TYPE=1 and  document_type='D' and isDeduction=1 and Posting_Date is not null " &
               " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtToDate.Value & "',103)" &
               " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

                Dim StrDeductionHeadSum As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' " &
             "  +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' +' as ' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Alies_Name" &
             " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
             " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
             " left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" &
             "  where TSPL_DEDUCTION_MASTER.OTHERS_TYPE=1 and document_type='D' and isDeduction=1 and Posting_Date is not null " &
             " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtToDate.Value & "',103)" &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

                Dim StrDeductionHeadSumTotal As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT " &
             "  isnull(STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' as Alies_Name" &
             " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
             " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
             " left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode" &
             "  where TSPL_DEDUCTION_MASTER.OTHERS_TYPE=1 and  document_type='D' and isDeduction=1 and Posting_Date is not null " &
             " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtToDate.Value & "',103)" &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

                Dim qry As String = ""
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    qry += "select [MCC Code],	max([MCC Name]) as [MCC Name] " &
                       ",sum([BM Amount]) as [BM Amount], sum([BM Qty]) as [BM Qty],sum([CM Amount]) as [CM Amount],sum ([CM Qty])  as [CM Qty] " &
                       ",sum([COMSN Amount]) as [COMSN Amount],sum([OP-COST Amount]) as [OP-COST Amount],sum([CART Amount]) as [CART Amount],sum([INC Amount]) as [INC Amount] " &
                       ",sum([ADDN Amount]) as [ADDN Amount],sum([Gross Amount]) as [Gross Amount] " &
                       " ," & StrDeductionHeadSum & ",(" & StrDeductionHeadSumTotal & ") as [Total Deduction Amount],(sum([Gross Amount])-(" & StrDeductionHeadSumTotal & ")) as [Net Amount]" &
                       " ,sum([T.I.P Amount]) as [T.I.P Amount],sum([KG Fat(RS.30)]) as [KG Fat(RS.30)] from ( " &
                       "select s.[MCC Code], s.[MCC Name] ,Deduction_Desc " &
                       ",isnull(deduction.DeductionAmt,0) as DeductionAmt,[BM Amount],[CM Amount],[BM Qty],[CM Qty] " &
                       ",[COMSN Amount],[OP-COST Amount],[CART Amount],[INC Amount] " &
                       ",[ADDN Amount],[Gross Amount] " &
                       " ,[T.I.P Amount],[KG Fat(RS.30)]" &
                       " from ( "

                End If

                qry += " select pp.MCC as [MCC Code],pp.[MCC Name] " &
                       ",sum(pp.[BM Amount]) as [BM Amount],sum (pp.[BM Qty]) as [BM Qty],sum(pp.[CM Amount]) as [CM Amount] , sum (pp.[CM Qty]) as [CM Qty]  " &
                       ",0 as [COMSN Amount],0 as [OP-COST Amount],0 as [CART Amount],0 as [INC Amount] " &
                       ",isnull (sum(Incentive_Amount),0) as [ADDN Amount] " &
                       ",(isnull(sum(pp.[BM Amount]),0)+isnull(sum(pp.[CM Amount]),0) + isnull(sum(Incentive_Amount),0)) as [Gross Amount] " &
                       " ,sum(TIP_Amount) as [T.I.P Amount], 0 as [KG Fat(RS.30)]" &
                       " from ( " &
            " Select (case when TSPL_MILK_SAMPLE_DETAIL.TYPE='B' then  Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [BM Amount], (case when TSPL_MILK_SAMPLE_DETAIL.TYPE='B' then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR   else 0 end) as [BM Qty] ," &  'convert (decimal(18,2) , (TSPL_MILK_SRN_DETAIL.qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )
            "(case when TSPL_MILK_SAMPLE_DETAIL.TYPE='C' then Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [CM Amount],(case when TSPL_MILK_SAMPLE_DETAIL.TYPE='C' then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR  else 0 end) as [CM Qty],TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type] " &
            ", TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ) as Incentive_Amount " &  ' TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount
            " ,isnull(TSPL_MILK_SRN_DETAIL.TIP_Amount,0) as TIP_Amount From TSPL_MILK_RECEIPT_DETAIL  " &
            " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " &
            " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " &
            " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO  " &
            " And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE  " &
            " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " &
            " And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO" &
            " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_DOC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  " &
            " and   TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_Item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code  " &
            " Left Outer Join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code " &
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " &
            " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code   " &
            " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_MILK_SRN_DETAIL.Item_Code
              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_MILK_SRN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_MILK_SRN_DETAIL.UOM_Code = Stocking_Conversion_Factor.UOM_Code " &
            " where 2=2 " &
             " and convert(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <= convert(date,'" + txtToDate.Value + "',103) " &
             " and tspl_location_master.location_Code ='" + txtShed.Value + "'" &
            " )pp group by pp.MCC,pp.[MCC Name]  "

                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    qry += " ) as s  " &
            " left join    " &
            "(select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt " &
            " from TSPL_VENDOR_INVOICE_DETAIL  " &
            " left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            " left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode " &
            " where TSPL_DEDUCTION_MASTER.OTHERS_TYPE=1 and document_type='D' and isDeduction=1 and Posting_Date is not null " &
            " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtToDate.Value & "',103)" &
            " group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc " &
            ") deduction on deduction.MCC_Code=s.[MCC Code] " &
            ")tt " &
            " pivot (  sum(DeductionAmt) for Deduction_Desc  in (" & StrDeductionHead &
            ") ) as zpivot group by zpivot.[MCC Code]  "
                End If

                Dim strDeductionColumn As String = ""
                Dim strMinusDedAmtInNetAmt As String = ""
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    strDeductionColumn = " ," & StrDeductionHeadSum & ",(" & StrDeductionHeadSumTotal & ") as [Total Deduction Amount] "
                    strMinusDedAmtInNetAmt = " -(" & StrDeductionHeadSumTotal & ") "
                End If


                qry = " select TSPL_MCC_MASTER.Plant_Code as PlantCode, max(TSPL_LOCATION_MASTER.Location_Desc) as PlantName,sum (XXXFinal.[BM Amount]) as [BM Amount], cast (  sum ([BM Qty]) as Decimal(18,2)) as [BM Qty(Ltr)] , cast (  ( sum ([BM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + txtFromDate.Value + "',103), convert (date, '" + txtToDate.Value + "',103))+1 ,0) ) as Decimal(18,2)) as [BM Avg(Ltr)], cast( (sum (XXXFinal.[BM Amount]) /sum ([BM Qty])) as decimal(18,2)) as  [BM Avg Rate], sum ([CM Amount]) as [CM Amount] ,cast ( sum ([CM Qty]) as  Decimal(18,2)) as [CM Qty(Ltr)], cast (  (sum ([CM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + txtFromDate.Value + "',103), convert (date, '" + txtToDate.Value + "',103))+1 ,0)) as decimal(18,2)) [CM Avg(Ltr)] ,cast( (sum (XXXFinal.[CM Amount]) /sum ([CM Qty])) as decimal(18,2)) as  [CM Avg Rate], cast (  (sum ([BM Qty])  +  sum ([CM Qty])) as decimal(18,2)) as [MM Qty(Ltr)], cast (  ( sum ([BM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + txtFromDate.Value + "',103), convert (date, '" + txtToDate.Value + "',103))+1 ,0) ) as Decimal(18,2)) + cast (  (sum ([CM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + txtFromDate.Value + "',103), convert (date, '" + txtToDate.Value + "',103))+1 ,0)) as decimal(18,2))  as [MM Avg(Ltr)], cast (( (sum (XXXFinal.[BM Amount]) + sum (XXXFinal.[CM Amount])) / (sum ([BM Qty]) + sum ([CM Qty]))) as decimal(18,2)) as [MM Avg Rate],  max(TBL_Buffalo.TSDDCS_Rate) as [BM TSDDCS Rate] , max(TBL_COW.TSDDCS_Rate) as [CM TSDDCS Rate] , sum ([COMSN Amount]) as [COMSN Amount],sum ([OP-COST Amount]) as [OP-COST Amount], sum ([CART Amount]) as [CART Amount], sum ([INC Amount]) as [INC Amount], sum ([ADDN Amount]) as [ADDN Amount], sum ([Gross Amount]) as [Gross Amount]  " + strDeductionColumn + " ,(sum([Gross Amount])  " + strMinusDedAmtInNetAmt + "  ) as [Net Amount] ,sum([T.I.P Amount]) as [T.I.P Amount],sum([KG Fat(RS.30)]) as [KG Fat(RS.30)]  from ( " + qry + " ) as XXXFinal 
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = XXXFinal.[MCC Code]
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code
                        left outer join (select top 1 TSPL_MCC_MASTER.Plant_Code,  TSPL_PRICE_CHART_PLANNING.TSDDCS_Rate  from TSPL_PRICE_CHART_PLANNING_MCC  left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code = TSPL_PRICE_CHART_PLANNING_MCC.Planning_Code
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PRICE_CHART_PLANNING_MCC.MCC_Code
                        where Dock_Collection_Milk_Type = 'B' and Status =1 and TSPL_MCC_MASTER.Plant_Code = '" + txtShed.Value + "' 
                        order by  Planning_Date desc) as TBL_Buffalo on TBL_Buffalo.Plant_Code = TSPL_MCC_MASTER.Plant_Code
                        left outer join (select top 1 TSPL_MCC_MASTER.Plant_Code,  TSPL_PRICE_CHART_PLANNING.TSDDCS_Rate  from TSPL_PRICE_CHART_PLANNING_MCC  left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code = TSPL_PRICE_CHART_PLANNING_MCC.Planning_Code
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PRICE_CHART_PLANNING_MCC.MCC_Code
                        where Dock_Collection_Milk_Type = 'C' and Status =1 and TSPL_MCC_MASTER.Plant_Code = '" + txtShed.Value + "' 
                        order by  Planning_Date desc) as TBL_COW on TBL_COW.Plant_Code = TSPL_MCC_MASTER.Plant_Code
                        group by TSPL_MCC_MASTER.Plant_Code
                        order by TSPL_MCC_MASTER.Plant_Code"
                Dim dtFooter As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtFooter IsNot Nothing AndAlso dtFooter.Rows.Count > 0 Then
                    obj.arrReportFooter = New List(Of clsDosPrintReportFooter)

                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("DD Office Account Details", " ", "", "------------", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("                    ", "     ", "", "--------", ""))
                    If clsCommon.myLen(StrDeductionHead) > 0 Then
                        Dim result As String() = StrDeductionHead.Split(New String() {","}, StringSplitOptions.None)
                        Dim finalchar As String = ""
                        For Each s As String In result
                            finalchar = s.Replace("[", "")
                            finalchar = finalchar.Replace("]", "")
                            obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(finalchar, "     ", ":", clsCommon.myCstr(dtFooter.Rows(0)("" + finalchar + "")), ""))
                        Next
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL TIP AMOUNT    ", "     ", ":", clsCommon.myCstr(dtFooter.Rows(0)("T.I.P Amount")), ""))
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("                    ", "     ", "", "--------", ""))
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-------------------", "------------------------------------------------", "", "", ""))
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("DD Office Amount    ", "     ", ":", clsCommon.myCstr(clsCommon.myCdbl(dtFooter.Rows(0)("Total Deduction Amount")) + clsCommon.myCdbl(dtFooter.Rows(0)("T.I.P Amount"))), ""))
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-------------------", "------------------------------------------------", "", "", ""))
                    End If
                End If
#End Region

                obj.Print(obj, dt, PageSetup.Landscap)

            ElseIf rbtnShedWiseMilkCost.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "SHED NAME : " + lblShed.Text
                obj.ReportName2 = "PERIOD FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                obj.ShowPageNo = False
                'obj.LandscapPageSetupColumnsChar = 240
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Particular", "Particular", False, DosPrintAlignment.Left, 12, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("BM", "BM", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CM", "CM", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Total", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Potrate)
            ElseIf rbtnMillBill.IsChecked Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName + " Milk Bill FROM: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " TO " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")

                obj.ShowPageNo = False

                obj.arrGroup = New List(Of clsDosPrintGroup)
                obj.arrGroup.Add(clsDosPrintGroup.GetObject("GrpColumn", "Unit", "", "GrpFooter", True, GroupFooterCounterLine))


                obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)


                Dim objMergeColumn As New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "GOOD MILK"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("Milk Weight(KG)")
                objMergeColumn.arrColumn.Add("FAT(%)")
                objMergeColumn.arrColumn.Add("SNF(%)")
                objMergeColumn.arrColumn.Add("TSDDCS_Rate")
                objMergeColumn.arrColumn.Add("NET_AMOUNT")
                objMergeColumn.arrColumn.Add("TIP_Amount")
                objMergeColumn.arrColumn.Add("TIP_Amount")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "SOUR MILK"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("SM Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("SM Milk Weight(KG)")
                objMergeColumn.arrColumn.Add("SM FAT(%)")
                objMergeColumn.arrColumn.Add("SMValue")
                objMergeColumn.arrColumn.Add("SRN Amount")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "CURDLED"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("CU Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("CU Milk Weight(KG)")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn
                objMergeColumn.MergeText = "TOTAL"
                objMergeColumn.arrColumn = New List(Of String)
                objMergeColumn.arrColumn.Add("Tot Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("Tot Milk Weight(KG)")
                objMergeColumn.arrColumn.Add("TotAmount")
                obj.arrMergeColumn.Add(objMergeColumn)


                'obj.LandscapPageSetupColumnsChar = 20
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Doc Date", "DTD", True, DosPrintAlignment.Left, 2, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Shift", "Shift", True, DosPrintAlignment.Left, 7, False, DecimalPlaces.NA))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Weight(LTR)", "Qty-LTR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Weight(KG)", "Qty-KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("FAT(%)", "FAT%", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF(%)", "SNF%", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TSDDCS_Rate", "Rate", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("NET_AMOUNT", "VALUE", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TIP_Amount", "TIP", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SRN Amount", "Amount", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))


                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SM Milk Weight(LTR)", "Qty-LTR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SM Milk Weight(KG)", "Qty-KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SM FAT(%)", "FAT%", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SMValue", "Amount", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CU Milk Weight(LTR)", "Qty-LTR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CU Milk Weight(KG)", "Qty-KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Tot Milk Weight(LTR)", "Qty-LTR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Tot Milk Weight(KG)", "Qty-KG", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotAmount", "Amount", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.Print(obj, dt, PageSetup.Landscap, "DashLine", "1")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rbtnMillBill_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnMillBill.ToggleStateChanged
        txtMCC.Visible = rbtnMillBill.IsChecked
        MyLabel3.Visible = rbtnMillBill.IsChecked
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            If clsCommon.myLen(txtShed.Value) <= 0 Then
                txtShed.Focus()
                Throw New Exception("Please select Shed")
            End If
            Dim qry As String = "  select MCC_Code as Code,MCC_NAME as Name from TSPL_MCC_MASTER where Plant_Code='" + txtShed.Value + "'"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@ShedIOAbs", qry, "Code", "", txtMCC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnShedWiseMilkCost_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnShedWiseMilkCost.ToggleStateChanged
        lbBMSNFDedRate.Visible = rbtnShedWiseMilkCost.IsChecked
        txtBMSNFDedRate.Visible = rbtnShedWiseMilkCost.IsChecked
        lblCMSNFDedRate.Visible = rbtnShedWiseMilkCost.IsChecked
        txtCMSNFDedRate.Visible = rbtnShedWiseMilkCost.IsChecked
    End Sub
End Class

