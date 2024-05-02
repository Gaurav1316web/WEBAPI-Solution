'--29/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports XpertERPEngine
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataSet

Public Class rptClaimMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "rptClaimMaster"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Public DT_BASE As DataTable
    Dim DT_Details As DataTable
    Dim DT_CUST As DataTable
    Dim DT_TEMP As DataTable

#End Region
    Sub LoadData_sum()
        Try
            Dim Qry As String = ""
            Qry += " select T1.Claim_Code,CONVERT(VARCHAR,T1.Claim_Date,103) AS [Claim_Date],  T1.Cust_Code, T2.Customer_Name, T1.Target_Code, T1.Claim_Amount, T1.Approved_Amount, T1.Status, CONVERT(VARCHAR,T1.Approved_Date,103) as 'Approved_Date' ,0 as 'Released_Amount' "
            Qry += " from  TSPL_CLAIM_DETAILS T1 "
            Qry += " left outer join TSPL_CUSTOMER_MASTER T2 on t1.Cust_Code = T2.Cust_Code WHERE 2=2"
            If cbgCustomer.CheckedValue.Count > 0 Then
                Qry += " AND T1.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            Qry += " ORDER BY T1.Cust_Code, T1.Claim_Date "
            DT_BASE = clsDBFuncationality.GetDataTable(Qry)
            Qry = ""
            Qry += " select DISTINCT Cust_Code from  TSPL_CLAIM_DETAILS where Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            DT_CUST = clsDBFuncationality.GetDataTable(Qry)
            Dim BAL_AMOUNT As Double = 0
            Dim MIN_DATE As Date = Nothing
            For Each DR As DataRow In DT_CUST.Rows
                Qry = ""
                Qry += " SELECT MAX(Sale_Invoice_Date) AS [Dis_Date] ,Cust_Code,SUM(Inv_Discount_Amt) AS [Dis_Amount]  FROM TSPL_SALE_INVOICE_HEAD"
                Qry += " WHERE Discount_On =1 AND Cust_Code = '" + DR("Cust_Code") + "' "
                Qry += " GROUP BY Cust_Code "
                DT_TEMP = clsDBFuncationality.GetDataTable(Qry)
                If DT_TEMP.Rows.Count > 0 Then
                    BAL_AMOUNT = clsCommon.myCdbl(DT_TEMP.Rows(0)("Dis_Amount"))
                    MIN_DATE = clsCommon.myCDate(DT_TEMP.Rows(0)("Dis_Date"))
                    If BAL_AMOUNT > 0 Then
                        For Each DR_BASE As DataRow In DT_BASE.Rows
                            If clsCommon.CompairString(DR_BASE("Cust_Code"), DR("Cust_Code")) = CompairStringResult.Equal AndAlso BAL_AMOUNT > 0 AndAlso clsCommon.myCDate(DR_BASE("Claim_Date")) <= MIN_DATE Then
                                If clsCommon.myCdbl(DR_BASE("Approved_Amount")) < BAL_AMOUNT Then
                                    DR_BASE("Released_Amount") = DR_BASE("Approved_Amount")
                                    BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(DR_BASE("Approved_Amount"))
                                Else
                                    DR_BASE("Released_Amount") = BAL_AMOUNT
                                    BAL_AMOUNT = 0
                                End If
                            End If
                        Next
                        DT_BASE.AcceptChanges()
                    End If
                End If
            Next
            Me.gv1.DataSource = Nothing
            Me.gv1.Rows.Clear()
            Me.gv1.Columns.Clear()
            Dim DT_TAMP As DataTable = DT_BASE.Clone()
            Dim DR_T As DataRow
            If clsCommon.myLen(dtpMonthnYear.Value) > 0 Then
                For Each DR_BASE As DataRow In DT_BASE.Rows
                    If clsCommon.myCDate(DR_BASE("Claim_Date")).Year = dtpMonthnYear.Value.Year AndAlso clsCommon.myCDate(DR_BASE("Claim_Date")).Month = dtpMonthnYear.Value.Month Then
                        DR_T = DT_TAMP.NewRow()
                        DR_T = DR_BASE
                        DT_TAMP.ImportRow(DR_BASE)
                    End If
                Next
            End If
            DT_TAMP.AcceptChanges()

            If DT_TAMP.Rows.Count > 0 Then
                Me.gv1.DataSource = DT_TAMP
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = Page_Report
                Me.gv1.MasterTemplate.Columns("Claim_Code").HeaderText = "Claim Code"
                Me.gv1.MasterTemplate.Columns("Claim_Date").HeaderText = "Claim Date"
                Me.gv1.MasterTemplate.Columns("Cust_Code").HeaderText = "Customer Code"
                Me.gv1.MasterTemplate.Columns("Customer_Name").HeaderText = "Customer Name"
                Me.gv1.MasterTemplate.Columns("Target_Code").HeaderText = "Target Code"
                Me.gv1.MasterTemplate.Columns("Claim_Amount").HeaderText = "Claim Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Amount").HeaderText = "Approved Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Date").HeaderText = "Approved Date"
                Me.gv1.MasterTemplate.Columns("Released_Amount").HeaderText = "Released Amount"
            Else
                clsCommon.MyMessageBoxShow("No Data To Display.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData_Detailed()
        Try
            Dim Qry As String = ""
            Qry += " select T1.Claim_Code,CONVERT(VARCHAR,T1.Claim_Date,103) AS [Claim_Date], '' as Sale_Invoice_No ,'' as  Sale_Invoice_Date,  T1.Cust_Code, T2.Customer_Name, T1.Target_Code, T1.Claim_Amount, T1.Approved_Amount, T1.Status, CONVERT(VARCHAR,T1.Approved_Date,103) as 'Approved_Date' ,0 as 'Released_Amount', 0 as 'Released_Amount_Tax', 0 as 'Tot_Rel_Amount' "
            Qry += " from  TSPL_CLAIM_DETAILS T1 "
            Qry += " left outer join TSPL_CUSTOMER_MASTER T2 on t1.Cust_Code = T2.Cust_Code WHERE 2=2"
            Qry += " AND T1.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Qry += " ORDER BY T1.Cust_Code, T1.Claim_Date "
            DT_BASE = clsDBFuncationality.GetDataTable(Qry)
            Qry = ""
            Qry += " select DISTINCT Cust_Code from  TSPL_CLAIM_DETAILS where Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            DT_CUST = clsDBFuncationality.GetDataTable(Qry)
            Dim BAL_AMOUNT As Double = 0
            Dim MIN_DATE As Date = Nothing
            Dim TOT_REL As Double = 0
            Dim REL_AMT_TAX As Double = 0
            For Each DR As DataRow In DT_CUST.Rows
                TOT_REL = 0
                Qry = ""
                'Qry += " SELECT Sale_Invoice_No,CONVERT(VARCHAR,Sale_Invoice_Date,103) AS Sale_Invoice_Date, Cust_Code, Inv_Discount_Amt,TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate FROM TSPL_SALE_INVOICE_HEAD "
                Qry += " SELECT Sale_Invoice_No,CONVERT(VARCHAR,Sale_Invoice_Date,103) AS Sale_Invoice_Date, Cust_Code, Inv_Discount_Amt,"
                Qry += " (CASE WHEN TAX1='VAT' THEN  TAX1_Rate  WHEN TAX2='VAT' THEN  TAX2_Rate WHEN TAX3='VAT' THEN  TAX3_Rate WHEN TAX4='VAT' THEN  TAX4_Rate WHEN TAX5='VAT' THEN  TAX5_Rate WHEN TAX6='VAT' THEN  TAX6_Rate WHEN TAX7='VAT' THEN  TAX7_Rate WHEN TAX8='VAT' THEN  TAX8_Rate WHEN TAX9='VAT' THEN  TAX9_Rate WHEN TAX10='VAT' THEN  TAX10_Rate END) AS VAT_TEX_RATE "
                Qry += " FROM TSPL_SALE_INVOICE_HEAD "
                Qry += " WHERE Discount_On =1 AND Cust_Code = '" + DR("Cust_Code") + "' "
                DT_TEMP = clsDBFuncationality.GetDataTable(Qry)
                If DT_TEMP.Rows.Count > 0 Then
                    Dim Row_Cnt As Int16 = 0
                    For Each DR_temp As DataRow In DT_TEMP.Rows
                        BAL_AMOUNT = clsCommon.myCdbl(DR_temp("Inv_Discount_Amt"))
                        MIN_DATE = clsCommon.myCDate(DR_temp("Sale_Invoice_Date"))
                        If BAL_AMOUNT > 0 Then

                            For Cnt As Int16 = Row_Cnt To DT_BASE.Rows.Count - 1
                                Row_Cnt += 1
                                If clsCommon.CompairString(DT_BASE.Rows(Cnt).Item("Cust_Code"), DR_temp("Cust_Code")) = CompairStringResult.Equal AndAlso BAL_AMOUNT > 0 AndAlso clsCommon.myCDate(DR_temp("Sale_Invoice_Date")) <= MIN_DATE Then
                                    If clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount")) < BAL_AMOUNT Then
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount") = DT_BASE.Rows(Cnt).Item("Approved_Amount")
                                        'TOT_REL += clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'DT_BASE.Rows(Cnt).Item("Sale_Invoice_No") = DR_temp("Sale_Invoice_No")
                                        'DT_BASE.Rows(Cnt).Item("Sale_Invoice_Date") = DR_temp("Sale_Invoice_Date")
                                        'BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL
                                        REL_AMT_TAX = 0
                                        REL_AMT_TAX = clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        REL_AMT_TAX = REL_AMT_TAX + (REL_AMT_TAX * clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")) * 0.01)
                                        DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = REL_AMT_TAX
                                        DT_BASE.Rows(Cnt).Item("Released_Amount") = DT_BASE.Rows(Cnt).Item("Approved_Amount")
                                        TOT_REL += REL_AMT_TAX
                                        DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL
                                        BAL_AMOUNT = BAL_AMOUNT - REL_AMT_TAX
                                        DT_BASE.Rows(Cnt).Item("Sale_Invoice_No") = DR_temp("Sale_Invoice_No")
                                        DT_BASE.Rows(Cnt).Item("Sale_Invoice_Date") = DR_temp("Sale_Invoice_Date")
                                    Else
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount") = BAL_AMOUNT
                                        'TOT_REL += BAL_AMOUNT
                                        'BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL
                                        'DT_BASE.Rows(Cnt).Item("Sale_Invoice_No") = DR_temp("Sale_Invoice_No")
                                        'DT_BASE.Rows(Cnt).Item("Sale_Invoice_Date") = DR_temp("Sale_Invoice_Date")

                                        DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = BAL_AMOUNT
                                        DT_BASE.Rows(Cnt).Item("Released_Amount") = clsCommon.myCdbl(((BAL_AMOUNT * 100) / (100 + clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")))))
                                        TOT_REL += BAL_AMOUNT
                                        DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL
                                        BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(((BAL_AMOUNT * 100) / (100 + clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")))))
                                        DT_BASE.Rows(Cnt).Item("Sale_Invoice_No") = DR_temp("Sale_Invoice_No")
                                        DT_BASE.Rows(Cnt).Item("Sale_Invoice_Date") = DR_temp("Sale_Invoice_Date")
                                    End If

                                    If BAL_AMOUNT < 1 Then
                                        Exit For
                                    End If
                                End If
                            Next
                            DT_BASE.AcceptChanges()
                        End If
                    Next
                End If
            Next
            Me.gv1.DataSource = Nothing
            Me.gv1.Rows.Clear()
            Me.gv1.Columns.Clear()
            Dim DT_TAMP As DataTable = DT_BASE.Clone()
            Dim DR_T As DataRow
            If clsCommon.myLen(dtpMonthnYear.Value) > 0 Then
                For Each DR_BASE As DataRow In DT_BASE.Rows
                    If clsCommon.myCDate(DR_BASE("Claim_Date")).Year = dtpMonthnYear.Value.Year AndAlso clsCommon.myCDate(DR_BASE("Claim_Date")).Month = dtpMonthnYear.Value.Month Then
                        DR_T = DT_TAMP.NewRow()
                        DR_T = DR_BASE
                        DT_TAMP.ImportRow(DR_BASE)
                    End If
                Next
            End If
            DT_TAMP.AcceptChanges()


            If DT_TAMP.Rows.Count > 0 Then
                Me.gv1.DataSource = DT_TAMP
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = Page_Report
                Me.gv1.MasterTemplate.Columns("Sale_Invoice_No").HeaderText = "Sale Inv. No"
                Me.gv1.MasterTemplate.Columns("Sale_Invoice_Date").HeaderText = "Sale Inv. Date"
                Me.gv1.MasterTemplate.Columns("Tot_Rel_Amount").HeaderText = "Total Released Amount"
                Me.gv1.MasterTemplate.Columns("Claim_Code").HeaderText = "Claim Code"
                Me.gv1.MasterTemplate.Columns("Claim_Date").HeaderText = "Claim Date"
                Me.gv1.MasterTemplate.Columns("Cust_Code").HeaderText = "Customer Code"
                Me.gv1.MasterTemplate.Columns("Customer_Name").HeaderText = "Customer Name"
                Me.gv1.MasterTemplate.Columns("Target_Code").HeaderText = "Target Code"
                Me.gv1.MasterTemplate.Columns("Claim_Amount").HeaderText = "Claim Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Amount").HeaderText = "Approved Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Date").HeaderText = "Approved Date"
                Me.gv1.MasterTemplate.Columns("Released_Amount").HeaderText = "Released Amount"
            Else
                clsCommon.MyMessageBoxShow("No Data To Display.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData_Sum_New()
        Try
            Dim Qry As String = ""
            Qry += " select T1.Claim_Code,CONVERT(VARCHAR,T1.Claim_Date,103) AS [Claim_Date], T1.Cust_Code, T2.Customer_Name, T1.Target_Code, T1.Claim_Amount, T1.Approved_Amount, T1.Status, CONVERT(VARCHAR,T1.Approved_Date,103) as 'Approved_Date' ,0 as 'Released_Amount', 0 as 'Released_Amount_Tax',0 as 'Tot_Rel_Amount' "
            Qry += " from  TSPL_CLAIM_DETAILS T1 "
            Qry += " left outer join TSPL_CUSTOMER_MASTER T2 on t1.Cust_Code = T2.Cust_Code WHERE 2=2"
            Qry += " AND T1.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Qry += " ORDER BY T1.Cust_Code, T1.Claim_Date "
            DT_BASE = clsDBFuncationality.GetDataTable(Qry)
            Qry = ""
            Qry += " select DISTINCT Cust_Code from  TSPL_CLAIM_DETAILS where Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            DT_CUST = clsDBFuncationality.GetDataTable(Qry)
            Dim BAL_AMOUNT As Double = 0
            Dim MIN_DATE As Date = Nothing
            Dim REL_AMT_TAX As Double = 0
            Dim TOT_REL As Double = 0
            For Each DR As DataRow In DT_CUST.Rows
                TOT_REL = 0
                Qry = ""
                Qry += " SELECT Sale_Invoice_No,CONVERT(VARCHAR,Sale_Invoice_Date,103) AS Sale_Invoice_Date, Cust_Code, Inv_Discount_Amt,"
                Qry += " (CASE WHEN TAX1='VAT' THEN  TAX1_Rate  WHEN TAX2='VAT' THEN  TAX2_Rate WHEN TAX3='VAT' THEN  TAX3_Rate WHEN TAX4='VAT' THEN  TAX4_Rate WHEN TAX5='VAT' THEN  TAX5_Rate WHEN TAX6='VAT' THEN  TAX6_Rate WHEN TAX7='VAT' THEN  TAX7_Rate WHEN TAX8='VAT' THEN  TAX8_Rate WHEN TAX9='VAT' THEN  TAX9_Rate WHEN TAX10='VAT' THEN  TAX10_Rate END) AS VAT_TEX_RATE "
                Qry += " FROM TSPL_SALE_INVOICE_HEAD "
                Qry += " WHERE Discount_On =1 AND Cust_Code = '" + DR("Cust_Code") + "' "
                DT_TEMP = clsDBFuncationality.GetDataTable(Qry)
                If DT_TEMP.Rows.Count > 0 Then
                    Dim Row_Cnt As Int16 = 0
                    For Each DR_temp As DataRow In DT_TEMP.Rows
                        BAL_AMOUNT = clsCommon.myCdbl(DR_temp("Inv_Discount_Amt"))
                        MIN_DATE = clsCommon.myCDate(DR_temp("Sale_Invoice_Date"))
                        If BAL_AMOUNT > 0 Then

                            For Cnt As Int16 = Row_Cnt To DT_BASE.Rows.Count - 1
                                Row_Cnt += 1
                                If clsCommon.CompairString(DT_BASE.Rows(Cnt).Item("Cust_Code"), DR_temp("Cust_Code")) = CompairStringResult.Equal AndAlso BAL_AMOUNT > 0 AndAlso clsCommon.myCDate(DR_temp("Sale_Invoice_Date")) <= MIN_DATE Then
                                    If clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount")) < BAL_AMOUNT Then
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount") = DT_BASE.Rows(Cnt).Item("Approved_Amount")
                                        'BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'REL_AMT_TAX = 0
                                        'REL_AMT_TAX = clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = REL_AMT_TAX + (REL_AMT_TAX * clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")) * 0.01)

                                        REL_AMT_TAX = 0
                                        REL_AMT_TAX = clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        REL_AMT_TAX = REL_AMT_TAX + (REL_AMT_TAX * clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")) * 0.01)
                                        DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = REL_AMT_TAX
                                        DT_BASE.Rows(Cnt).Item("Released_Amount") = DT_BASE.Rows(Cnt).Item("Approved_Amount")
                                        TOT_REL += REL_AMT_TAX
                                        DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL
                                        BAL_AMOUNT = BAL_AMOUNT - REL_AMT_TAX
                                    Else
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount") = BAL_AMOUNT
                                        'REL_AMT_TAX = 0
                                        'REL_AMT_TAX = BAL_AMOUNT
                                        'BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(DT_BASE.Rows(Cnt).Item("Approved_Amount"))
                                        'DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = REL_AMT_TAX + (REL_AMT_TAX * clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")) * 0.01)

                                        DT_BASE.Rows(Cnt).Item("Released_Amount_Tax") = BAL_AMOUNT
                                        DT_BASE.Rows(Cnt).Item("Released_Amount") = clsCommon.myCdbl(((BAL_AMOUNT * 100) / (100 + clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")))))
                                        TOT_REL += BAL_AMOUNT
                                        DT_BASE.Rows(Cnt).Item("Tot_Rel_Amount") = TOT_REL

                                        BAL_AMOUNT = BAL_AMOUNT - clsCommon.myCdbl(((BAL_AMOUNT * 100) / (100 + clsCommon.myCdbl(DR_temp("VAT_TEX_RATE")))))
                                    End If

                                    If BAL_AMOUNT < 1 Then
                                        Exit For
                                    End If
                                End If
                            Next
                            DT_BASE.AcceptChanges()
                        End If
                    Next
                End If
            Next
            Me.gv1.DataSource = Nothing
            Me.gv1.Rows.Clear()
            Me.gv1.Columns.Clear()
            Dim DT_TAMP As DataTable = DT_BASE.Clone()
            Dim DR_T As DataRow
            If clsCommon.myLen(dtpMonthnYear.Value) > 0 Then
                For Each DR_BASE As DataRow In DT_BASE.Rows
                    If clsCommon.myCDate(DR_BASE("Claim_Date")).Year = dtpMonthnYear.Value.Year AndAlso clsCommon.myCDate(DR_BASE("Claim_Date")).Month = dtpMonthnYear.Value.Month Then
                        DR_T = DT_TAMP.NewRow()
                        DR_T = DR_BASE
                        DT_TAMP.ImportRow(DR_BASE)
                    End If
                Next
            End If
            DT_TAMP.AcceptChanges()

            If DT_TAMP.Rows.Count > 0 Then
                Me.gv1.DataSource = DT_TAMP
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = Page_Report
                Me.gv1.MasterTemplate.Columns("Tot_Rel_Amount").HeaderText = "Total Released Amount"
                Me.gv1.MasterTemplate.Columns("Released_Amount_Tax").HeaderText = "Released Amount(+Tax)"
                Me.gv1.MasterTemplate.Columns("Claim_Code").HeaderText = "Claim Code"
                Me.gv1.MasterTemplate.Columns("Claim_Date").HeaderText = "Claim Date"
                Me.gv1.MasterTemplate.Columns("Cust_Code").HeaderText = "Customer Code"
                Me.gv1.MasterTemplate.Columns("Customer_Name").HeaderText = "Customer Name"
                Me.gv1.MasterTemplate.Columns("Target_Code").HeaderText = "Target Code"
                Me.gv1.MasterTemplate.Columns("Claim_Amount").HeaderText = "Claim Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Amount").HeaderText = "Approved Amount"
                Me.gv1.MasterTemplate.Columns("Approved_Date").HeaderText = "Approved Date"
                Me.gv1.MasterTemplate.Columns("Released_Amount").HeaderText = "Released Amount"
            Else
                clsCommon.MyMessageBoxShow("No Data To Display.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub rptClaimMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptClaimMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub rptClaimMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If cbgCustomer.CheckedValue.Count < 1 Then
            clsCommon.MyMessageBoxShow("Please select One Or All Customer First.")
            Exit Sub
        End If

        If chkSum.IsChecked Then
            LoadData_Sum_New()
        Else
            LoadData_Detailed()
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Claim Report")
        clsCommon.MyExportToPDF("Claim Report", gv1, arr, "Claim Report", False)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Claim Report")
        clsCommon.MyExportToExcelGrid("Claim Report", gv1, arr, "Claim Report", False)
    End Sub

    Private Sub Reset()
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(" select DISTINCT TSPL_CLAIM_DETAILS.Cust_Code, TSPL_CUSTOMER_MASTER .Customer_Name   from  TSPL_CLAIM_DETAILS LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CLAIM_DETAILS.Cust_Code  ")
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Cust_Code"
        RadPageView1.SelectedPage = RadPageViewPage1
        dtpMonthnYear.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        chkSum.IsChecked = True
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
End Class
