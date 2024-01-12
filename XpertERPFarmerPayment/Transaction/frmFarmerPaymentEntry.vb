
Imports common
Imports System.Data.SqlClient
Imports System

Public Class frmFarmerPaymentEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isSettlementBankOnly As Boolean = False
    Public strPaymentNo As String = Nothing
    Private isCellValueChangedTaxOpen As Boolean = False
    '-----------Used in Bank Reco---------------
    Dim isFlag As Boolean = False
    Public ChequeNo As String = Nothing
    Public ChequeDate As Date? = Nothing
    Public Amount As Decimal = 0
    Public EntryDesc As String = Nothing
    '-------------------------------------------
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private IsInsideLoadData As Boolean = False
    Private isNewEntry As Boolean = False
    Private objRemittance As clsRemittance
    Dim Qry As String = ""
    Dim dt As DataTable
    Dim btnToolTip As ToolTip = New ToolTip()
    Dim IsPaymentTypeChanged As Boolean = False
    Dim GSTStatus As Boolean = False
    Public StrdocNo As String = ""

    '---------------------------------------------
    Dim isApplyBranchAccounting As Boolean
    Dim isApplyCostCenter As Boolean
    Dim deadLockCounter As Integer
    Dim Arr As ArrayList
    Public PDCSetting As Boolean = False


#End Region


    Private Sub frmFarmerPaymentEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetTollTip()
        Reset()
    End Sub


    Private Sub SetTollTip()
        btnToolTip.SetToolTip(btnclose, "Press Esc Close the Window")
    End Sub

    '==========Reset All Controls==========================
    Private Sub Reset()

        isNewEntry = True
        txtPaymentNo.Value = ""
        dtpPayment.Value = clsCommon.GETSERVERDATE()
        dtpPayment.Focus()
        txtPayment_Amount.Text = 0
        txtBankCode.Text = ""
        txtPaymentCode.Text = ""
        txtdescription.Text = ""
        dtp_chequedate.Value = clsCommon.GETSERVERDATE()
        txtchequeno.Text = ""
        txtMccCode.Text = ""
        lblMccName.Text = ""
        txtVSPCode.Text = ""
        lblVSPDesc.Text = ""
        txtVLCCode.Text = ""
        lblVLCDesc.Text = ""
        txtFarmerCode.Text = ""
        lblFarmerName.Text = ""
        txtfarmerinvoice.Text = ""
        dtpfarmerinvoice.Value = clsCommon.GETSERVERDATE()

    End Sub



    Private Sub txtPaymentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPaymentNo._MYValidating
        '' finder query change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
        Dim qry As String = " select TSPL_MP_PAY_HEAD.Payment_No as [Code] ,convert(varchar,TSPL_MP_PAY_HEAD.Payment_Date,103) as [Date]," & _
                            " TSPL_MP_PAY_HEAD.Farmer_Code as [Farmer Code] ,TSPL_MP_PAY_HEAD.Farmer_Name as [Farmer Name], " & _
                            " TSPL_MP_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_MP_PAY_HEAD.Payment_Code as [Payment Code], " & _
                            " TSPL_MP_PAY_HEAD.Cheque_No as [Cheque No],TSPL_MP_PAY_HEAD.Cheque_Date as [Cheque Date],TSPL_MP_PAY_HEAD.Payment_Amount as [Payment Amount], " & _
                            " TSPL_MP_PAY_HEAD.Payment_Process_Code as [Payment Procss Code],TSPL_MP_PAY_HEAD.Entry_Desc as Description " & _
                            " From TSPL_MP_PAY_HEAD left join TSPL_MP_MASTER on TSPL_MP_PAY_HEAD.Farmer_Code=TSPL_MP_MASTER.MP_Code " & _
                            " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code " & _
                            " left join TSPL_VENDOR_MASTER on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code"

        Dim strWhrclas As String = "1=1"
      
        LoadData(clsCommon.ShowSelectForm("PMNTFNDPayment", qry, "Code", strWhrclas, txtPaymentNo.Value, "Code", isButtonClicked))
    End Sub

    Private Sub txtPaymentNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles txtPaymentNo._MYNavigator
        Try
            Dim qst As String = "select TSPL_MP_PAY_HEAD.Payment_no from TSPL_MP_PAY_HEAD where 1=1 "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            'If count = 0 Then
            '    txtPaymentNo.MyReadOnly = False
            'Else
            '    txtPaymentNo.MyReadOnly = True
            'End If

            Select Case NavigatorType
                Case NavigatorType.Current
                    '  qst += "and assign_to='" + txtassign.Value + "' "
                    ' qst += "and job_code in ('" + txtcode1.Value + "')"
                Case NavigatorType.Next
                    qst += "and Payment_no in (select min(Payment_no) from TSPL_MP_PAY_HEAD where Payment_no > '" + txtPaymentNo.Value + "') "

                    'qst += "and job_code in (select min(job_code) from job_master where job_code>'" + txtcode1.Value + "' and assign_to='" + txtassign.Value + "') "
                Case NavigatorType.First
                    qst += "and Payment_no in (select MIN(Payment_no) from TSPL_MP_PAY_HEAD )"

                Case NavigatorType.Last
                    qst += "and Payment_no in (select Max(Payment_no) from TSPL_MP_PAY_HEAD  )"
                Case NavigatorType.Previous
                    qst += "and Payment_no in (select max(Payment_no) from TSPL_MP_PAY_HEAD where Payment_no < '" + txtPaymentNo.Value + "'  )"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Reset()
                txtPaymentNo.Value = clsCommon.myCstr(dt.Rows(0)("Payment_no"))
            End If

            LoadData(txtPaymentNo.Value)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String)
        Try

            Qry = "select TSPL_MP_PAY_HEAD.Payment_No,TSPL_MP_PAY_HEAD.Payment_Date,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No,convert(varchar,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_Date,103) AS Farmer_Invoice_Date " & _
            " ,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code,TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Name " & _
            " ,TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE,TSPL_MP_PAY_PROCESS_DETAIL.VSP_Name " & _
            " ,TSPL_MP_MASTER.VLC_Code,TSPL_MP_PAY_PROCESS_DETAIL.VLC_Name " & _
            " ,TSPL_MCC_MASTER.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,isnull(TSPL_MP_PAY_HEAD.Payment_Amount,0) as Payment_Amount" & _
            " ,isnull(TSPL_MP_PAY_HEAD.Bank_Code,'') as Bank_Code,isnull(TSPL_MP_PAY_HEAD.Payment_Code,'') as Payment_Code" & _
            " ,isnull(TSPL_MP_PAY_HEAD.Cheque_No,'') as Cheque_No,isnull(CONVERT(varchar,TSPL_MP_PAY_HEAD.Cheque_Date,103),'') as Cheque_Date,isnull(TSPL_MP_PAY_HEAD.Entry_Desc,'') as Entry_Desc " & _
           " from TSPL_MP_PAY_PROCESS_DETAIL " & _
            " left join TSPL_MP_PAY_DETAIL on TSPL_MP_PAY_DETAIL.Document_No=TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No " & _
            " left join  TSPL_MP_PAY_HEAD on TSPL_MP_PAY_HEAD.Payment_Process_Code=TSPL_MP_PAY_PROCESS_DETAIL.Doc_No " & _
            " and TSPL_MP_PAY_HEAD.Payment_No=TSPL_MP_PAY_DETAIL.Payment_No " & _
            " left join TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_CODE=TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code " & _
            " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.vlc_code " & _
            " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.mcc " & _
           " where TSPL_MP_PAY_HEAD.Payment_No = '" + strDocumentNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                txtPaymentNo.Value = clsCommon.myCstr(dt.Rows(0).Item("Payment_No"))
                dtpPayment.Value = clsCommon.myCDate(dt.Rows(0).Item("Payment_Date"), "dd/MMM/yyyy")

                txtPayment_Amount.Text = clsCommon.myCdbl(dt.Rows(0).Item("Payment_Amount"))
                txtBankCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Bank_Code"))
                txtPaymentCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Payment_Code"))
                txtdescription.Text = clsCommon.myCstr(dt.Rows(0).Item("Entry_Desc"))
                txtchequeno.Text = clsCommon.myCstr(dt.Rows(0).Item("Cheque_No"))

                If clsCommon.myLen(txtchequeno.Text) > 0 Then
                    dtp_chequedate.Value = (clsCommon.myCDate(dt.Rows(0).Item("Cheque_Date"), "dd/MMM/yyyy"))
                End If


                txtMccCode.Text = clsCommon.myCstr(dt.Rows(0).Item("MCC_CODE"))
                lblMccName.Text = clsCommon.myCstr(dt.Rows(0).Item("MCC_NAME"))
                txtVSPCode.Text = clsCommon.myCstr(dt.Rows(0).Item("VSP_CODE"))
                lblVSPDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("VSP_Name"))
                txtVLCCode.Text = clsCommon.myCstr(dt.Rows(0).Item("VLC_Code"))
                lblVLCDesc.Text = clsCommon.myCstr(dt.Rows(0).Item("VLC_Name"))
                txtFarmerCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Farmer_Code"))
                lblFarmerName.Text = clsCommon.myCstr(dt.Rows(0).Item("Farmer_Name"))
                txtfarmerinvoice.Text = clsCommon.myCstr(dt.Rows(0).Item("Farmer_Invoice_No"))
                dtpfarmerinvoice.Value = (clsCommon.myCDate(dt.Rows(0).Item("Farmer_Invoice_Date"), "dd/MMM/yyyy"))


            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            'gv1.DataSource = dt

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub frmFarmerPaymentEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        ' PrintData()
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub
End Class
