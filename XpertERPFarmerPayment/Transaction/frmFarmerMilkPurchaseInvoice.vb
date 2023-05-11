Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.IO
Imports common


Public Class frmFarmerMilkPurchaseInvoice
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Private isInsideLoadData As Boolean = False
    
    Public Shared strDocumentNo As String = ""
    'Dim Payment_Cycle_value As Integer = 0
#End Region


#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
       
    End Sub

  

    Sub AddNew()
        txtCode.Value = ""
        dtpDocDate.Value = clsCommon.GETSERVERDATE()
        txtpaymentno.Text = ""
        txtMccCode.Text = ""
        lblMccName.Text = ""
        txtVSPCode.Text = ""
        lblVSPDesc.Text = ""
        txtVLCCode.Text = ""
        lblVLCDesc.Text = ""
        fndRouteCOde.Text = ""
        lblRouteDesc.Text = ""
        txtFarmerCode.Text = ""
        lblFarmerName.Text = ""
        TxtAPInvoiceNo.Text = ""
        AP_Invoice_Date.Value = clsCommon.GETSERVERDATE()
        TxtAPAdjustmentNo.Text = ""
        AP_Adjustment_Date.Value = clsCommon.GETSERVERDATE()
        txtMilkQuantity.Text = 0
        txtMilkAmount.Text = 0
        TxtMCCSaleAmount.Text = 0
        TxtMCCSaleReturnAmt.Text = 0
        TxtPayableAmt.Text = 0
        txtNextCycleDebitNote.Text = 0
    End Sub


#End Region

#Region "Events"

 

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMilkPurchaseInvoiceMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
      
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        AddNew()
        txtCode.MyMaxLength = 100
    End Sub


    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
      
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()

        End If
    End Sub
#End Region

    Public Sub LoadData(ByVal strDoc As String)
        Try

            Dim Qry As String = ""

            Qry = "Select TSPL_VLC_MASTER_HEAD.VLC_Code, " & _
            "Farmer_Invoice_No,Farmer_Invoice_Date,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Name, " & _
            "Doc_No, AP_Invoice_No, AP_Invoice_Date, AP_Adjustment_No, AP_Adjustment_Date, TSPL_MP_PAY_PROCESS_DETAIL.VLC_Name, TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE, VSP_Name, " & _
            "TSPL_VLC_MASTER_HEAD.MCC, TSPL_MCC_MASTER.MCC_NAME " & _
            ",TSPL_MCC_ROUTE_MASTER.Route_code,TSPL_MCC_ROUTE_MASTER.Route_name " & _
            ",isnull(Milk_Qty,0) as Milk_Qty,isnull(Milk_Amount,0) as Milk_Amount,isnull(MCC_Sale_Amount,0) as MCC_Sale_Amount " & _
            ",isnull(MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount,isnull(Payable_Amount,0) as Payable_Amount, " & _
            "isnull(NextCycleDebitNoteMP,0) as NextCycleDebitNoteMP,TSPL_MP_PAY_PROCESS_DETAIL.Incentive_Amount,TSPL_MP_PAY_PROCESS_DETAIL.Deduction_Amount " & _
           " from TSPL_MP_PAY_PROCESS_DETAIL " & _
            " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE " & _
            " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.mcc " & _
            " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_code= TSPL_VLC_MASTER_HEAD.Route_code " & _
             " where TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No = '" + strDoc + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                dtpDocDate.Value = clsCommon.myCDate(dt.Rows(0).Item("Farmer_Invoice_Date"), "dd/MMM/yyyy")
                txtpaymentno.Text = clsCommon.myCstr(dt.Rows(0).Item("Doc_No"))
                txtMccCode.Text = clsCommon.myCstr(dt.Rows(0).Item("MCC"))
                lblMccName.Text = clsCommon.myCstr(dt.Rows(0).Item("MCC_NAME"))
                txtVSPCode.Text = clsCommon.myCstr(dt.Rows(0).Item("VSP_CODE"))
                lblVSPDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("VSP_Name"))
                txtVLCCode.Text = clsCommon.myCstr(dt.Rows(0).Item("VLC_Code"))
                lblVLCDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("VLC_Name"))
                fndRouteCOde.Text = clsCommon.myCstr(dt.Rows(0).Item("Route_code"))
                lblRouteDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("Route_name"))
                txtFarmerCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Farmer_Code"))
                lblFarmerName.Text = clsCommon.myCstr(dt.Rows(0).Item("Farmer_Name"))
                TxtAPInvoiceNo.Text = clsCommon.myCstr(dt.Rows(0).Item("AP_Invoice_No"))
                AP_Invoice_Date.Value = clsCommon.myCDate(dt.Rows(0).Item("AP_Invoice_Date"), "dd/MMM/yyyy")
                TxtAPAdjustmentNo.Text = clsCommon.myCstr(dt.Rows(0).Item("AP_Adjustment_No"))
                AP_Adjustment_Date.Value = clsCommon.myCDate(dt.Rows(0).Item("AP_Adjustment_Date"), "dd/MMM/yyyy")
                txtMilkQuantity.Text = clsCommon.myCdbl(dt.Rows(0).Item("Milk_Qty"))
                txtMilkAmount.Text = clsCommon.myCdbl(dt.Rows(0).Item("Milk_Amount"))
                TxtMCCSaleAmount.Text = clsCommon.myCdbl(dt.Rows(0).Item("MCC_Sale_Amount"))
                TxtMCCSaleReturnAmt.Text = clsCommon.myCdbl(dt.Rows(0).Item("MCC_Sale_Return_Amount"))

                txtIncentiveAmt.Text = clsCommon.myCdbl(dt.Rows(0).Item("Incentive_Amount"))
                txtDeductionAmt.Text = clsCommon.myCdbl(dt.Rows(0).Item("Deduction_Amount"))

                TxtPayableAmt.Text = clsCommon.myCdbl(dt.Rows(0).Item("Payable_Amount"))

                txtNextCycleDebitNote.Text = clsCommon.myCdbl(dt.Rows(0).Item("NextCycleDebitNoteMP"))
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

   
    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Dim qst As String = "select TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No from TSPL_MP_PAY_PROCESS_DETAIL where 1=1 "

        Select Case NavType
            Case NavigatorType.Current
                
            Case NavigatorType.Next
                qst += "and Farmer_Invoice_No in (select min(Farmer_Invoice_No) from TSPL_MP_PAY_PROCESS_DETAIL where Farmer_Invoice_No > '" + txtCode.Value + "') "
            Case NavigatorType.First
                qst += "and Farmer_Invoice_No in (select MIN(Farmer_Invoice_No) from TSPL_MP_PAY_PROCESS_DETAIL )"
            Case NavigatorType.Last
                qst += "and Farmer_Invoice_No in (select Max(Farmer_Invoice_No) from TSPL_MP_PAY_PROCESS_DETAIL  )"
            Case NavigatorType.Previous
                qst += "and Farmer_Invoice_No in (select max(Farmer_Invoice_No) from TSPL_MP_PAY_PROCESS_DETAIL where Farmer_Invoice_No < '" + txtCode.Value + "'  )"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        AddNew()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtCode.Value = clsCommon.myCstr(dt.Rows(0)("Farmer_Invoice_No"))
        End If

        LoadData(txtCode.Value)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            '' finder query change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
            Dim qry As String = " select TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No as Code,convert(date,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_Date,103) as Date,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code as [Farmer Code],TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Name as [Farmer Name]," & _
                                " TSPL_MP_PAY_PROCESS_DETAIL.Doc_No as [Payment Process Code],TSPL_MP_PAY_PROCESS_DETAIL.AP_Adjustment_No as [AP Adjustment No],TSPL_MP_PAY_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC Code]," & _
                                " TSPL_MP_PAY_PROCESS_DETAIL.VLC_Name as [VLC Name],TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE as [VSP Code],TSPL_MP_PAY_PROCESS_DETAIL.VSP_Name as [VSP Name], " & _
                                " TSPL_MP_PAY_PROCESS_DETAIL.Milk_Qty as [Milk Qty],TSPL_MP_PAY_PROCESS_DETAIL.Milk_Amount as [Milk Amount],TSPL_MP_PAY_PROCESS_DETAIL.MCC_Sale_Amount as [MCC Sale Amount]," & _
                                " TSPL_MP_PAY_PROCESS_DETAIL.MCC_Sale_Return_Amount as [MCC Sale Return Amount],TSPL_MP_PAY_PROCESS_DETAIL.MP_Adjust_Amount as [Adjustment Amount],TSPL_MP_PAY_PROCESS_DETAIL.Payable_Amount as [Payable Amount], " & _
                                " TSPL_MP_PAY_PROCESS_DETAIL.NextCycleDebitNoteMP as [Next Cycle Debit Note] from TSPL_MP_PAY_PROCESS_DETAIL"
            txtCode.Value = clsCommon.ShowSelectForm("Farmer_Invoice", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

  

    Private Sub RadLabel27_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblTotRAmt_Click_1(sender As Object, e As EventArgs)

    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
End Class
