Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptLiabilityReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Dim StrPermission As String
    Private Sub rptLiabilityReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()

    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try



            Dim dt1 As New DataTable
            Dim qry As String = ""
            Dim arrMCC As ArrayList = Nothing

            If clsCommon.myLen(txtmccode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select MCC First.", Me.Text)
                txtmccode.Focus()
                Exit Sub

            Else

                arrMCC = New ArrayList
                arrMCC.Add(txtmccode.Value)
            End If
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycle.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle First.", Me.Text)
                txtPaymentCycle.Focus()
                Exit Sub
            End If

            Dim SetCowFatPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
            qry = clsMilkRejectHead.GetMCCRegisterRejectionQuery(fromDate.Value, ToDate.Value, "M", "E", StrPermission, arrMCC, Nothing, Nothing, "", SetCowFatPer)
            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFiscalYear.Enabled = val
        txtmccode.Enabled = val
        txtPaymentCycle.Enabled = val
    End Sub
    Sub SetGridFormat()
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        If Gv1.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 3 To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsCommon.myCstr("Liability Report"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            'End If
            arrHeader.Add("MCC : " + txtmccode.Value)
            arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
            arrHeader.Add("Payment Cycle : " + txtPaymentCycle.Value)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Liability Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Liability Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    'Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
    '    Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
    '    txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@MCC1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    'End Sub

    Private Sub Txtmccode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtmccode._MYValidating
        Dim whrcls As String = ""

        txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
        If clsCommon.myLen(txtmccode.Value) > 0 Then
            lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
        Else
            txtmccode.Value = ""
            lblmccname.Text = ""
        End If
    End Sub

    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycle__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycle._MYValidating
        Try
            If clsCommon.myLen(txtmccode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select MCC First.", Me.Text)
                txtmccode.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' and MCC_Code='" + txtmccode.Value + "' "
            Dim qry As String = "SELECT Name as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycle.Value = clsCommon.ShowSelectForm("LRPC", qry, "Code", whrcls, txtPaymentCycle.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtPaymentCycle.Value) > 0 Then
                Patment_Cycle_changed()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and MCC_Code='" + txtmccode.Value + "' and Name='" + txtPaymentCycle.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on Selected MCC/Fiscal Year", Me.Text)
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            ToDate.Value = clsCommon.myCDate(dt.Rows(0)("To_Date"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

End Class
