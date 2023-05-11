Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports Microsoft.VisualBasic.PowerPacks
Imports System.Text.RegularExpressions
Imports common

Public Class FrmAPCycle
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmAPCycle)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub ProgramName()
        LblAPInv.Tag = clsUserMgtCode.mbtnAPInvoiceEntry
        LblAPInv.Text = ProgramCodeNew.GetProgramName(LblAPInv.Tag)
        LblPaymentE.Tag = clsUserMgtCode.PaymentEntryNew
        LblPaymentE.Text = ProgramCodeNew.GetProgramName(LblPaymentE.Tag)
        LblQuickE.Tag = clsUserMgtCode.frmQuickBook
        LblQuickE.Text = ProgramCodeNew.GetProgramName(LblQuickE.Tag)
        LblVenAccSet.Tag = clsUserMgtCode.vendoraccountset
        LblVenAccSet.Text = ProgramCodeNew.GetProgramName(LblVenAccSet.Tag)
        LblVenGrp.Tag = clsUserMgtCode.vendorgroup
        LblVenGrp.Text = ProgramCodeNew.GetProgramName(LblVenGrp.Tag)
        LblPayT.Tag = clsUserMgtCode.paymentTerms
        LblPayT.Text = ProgramCodeNew.GetProgramName(LblPayT.Tag)
        LblVenMas.Tag = clsUserMgtCode.vendormaster
        LblVenMas.Text = ProgramCodeNew.GetProgramName(LblVenMas.Tag)
    End Sub
    Private Sub OvlTag()
        OvlAPInv.Tag = clsUserMgtCode.mbtnAPInvoiceEntry
        OvlPayE.Tag = clsUserMgtCode.PaymentEntryNew
        OvlQuickE.Tag = clsUserMgtCode.frmQuickBook
        OvlPayT.Tag = clsUserMgtCode.paymentTerms
        OvlVenAccSet.Tag = clsUserMgtCode.vendoraccountset
        OvlVenGrp.Tag = clsUserMgtCode.vendorgroup
        OvlPayT.Tag = clsUserMgtCode.paymentTerms
        OvlVenMas.Tag = clsUserMgtCode.vendormaster
    End Sub
  Private Sub DBBind()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='SMPayReport' And VPF_Active_Report=1 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                i = i + 25
                Dim lbl As New Label
                lbl.Text = clsCommon.myCstr(dr("Name"))
                Me.Controls.Add(lbl)
                lbl.Location = New Point(429, 341 + i)
                LocY = 366 + i
                'If clsCommon.myCdbl(LocY) > 484 Then
                '    j = j + 25
                '    lbl.Location = New Point(530, 237 + j)
                'End If
                lbl.BackColor = Color.Transparent
                Dim myfont As New Font("Arial", 9.75, FontStyle.Bold)
                lbl.Font = myfont
                lbl.AutoSize = True

                '' Oval
                Dim oval As Microsoft.VisualBasic.PowerPacks.OvalShape = New Microsoft.VisualBasic.PowerPacks.OvalShape()
                oval.Name = "CustomOvel" + clsCommon.myCstr(i)
                ' Add the oval shape to the form.
                RectangleShape1.Parent.Shapes.Add(oval)
                oval.Size = New Size(21, 17)
                oval.FillStyle = FillStyle.LightHorizontal
                oval.BorderColor = Color.Black
                oval.BorderWidth = 2
                oval.Location = New Point(373, 340 + i)
                OvalY = 365 + i
                oval.BackColor = Color.White
                oval.FillGradientColor = Color.LightGray
                oval.BackStyle = BackStyle.Opaque
                'If clsCommon.myCdbl(LocY) > 484 Then
                '    m = m + 25
                '    oval.Location = New Point(489, 234 + m)
                'End If
                oval.Tag = clsCommon.myCstr(dr("Code"))
                AddHandler oval.Click, AddressOf oval_Click
                AddHandler oval.MouseEnter, AddressOf oval_MouseEnter
                AddHandler oval.MouseLeave, AddressOf oval_MouseLeave
                AddHandler oval.MouseUp, AddressOf oval_MouseUp
            Next
        End If
        RectangleShape1.Visible = False
    End Sub
    Private Sub oval_MouseEnter(sender As Object, e As EventArgs)
        'Dim oval As OvalShape = DirectCast(sender, OvalShape)
        'oval.BackColor = Color.SkyBlue
        'oval.BorderColor = Color.Black
        'oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseLeave(sender As Object, e As EventArgs)
        'Dim oval As OvalShape = DirectCast(sender, OvalShape)
        'oval.BackColor = Color.White
        'oval.BorderColor = Color.Black
        'oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseUp(sender As Object, e As EventArgs)
        'Dim oval As OvalShape = DirectCast(sender, OvalShape)
        'oval.BackColor = Color.White
        'oval.BorderColor = Color.Black
        'oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_Click(sender As Object, e As EventArgs)
        Dim s As String = clsCommon.myCstr(DirectCast(sender, OvalShape).Tag)
        MDI.ShowForm(s, "", True)
    End Sub
 

#Region "TClicks"
    Private Sub OvlAPInv_Click(sender As Object, e As EventArgs) Handles OvlAPInv.Click
        MDI.ShowForm(LblAPInv.Tag, "", False)
    End Sub
    Private Sub OvlPayE_Click(sender As Object, e As EventArgs) Handles OvlPayE.Click
        MDI.ShowForm(LblPaymentE.Tag, "", False)
    End Sub
    Private Sub OvlQuickE_Click(sender As Object, e As EventArgs) Handles OvlQuickE.Click
        MDI.ShowForm(LblQuickE.Tag, "", False)
    End Sub
#End Region
#Region "MClicks"
    Private Sub OvlVenAccSet_Click(sender As Object, e As EventArgs) Handles OvlVenAccSet.Click
        MDI.ShowForm(LblVenAccSet.Tag, "", False)
    End Sub
    Private Sub OvlVenGrp_Click(sender As Object, e As EventArgs) Handles OvlVenGrp.Click
        MDI.ShowForm(LblVenGrp.Tag, "", False)
    End Sub
    Private Sub OvlPayT_Click(sender As Object, e As EventArgs) Handles OvlPayT.Click
        MDI.ShowForm(LblPayT.Tag, "", False)
    End Sub
    Private Sub OvlVenMas_Click(sender As Object, e As EventArgs) Handles OvlVenMas.Click
        MDI.ShowForm(LblVenMas.Tag, "", False)
    End Sub
#End Region

#Region "ReportsClick"
    Private Sub OvlRptPayE_Click(sender As Object, e As EventArgs) Handles OvlRptPayE.Click
        Dim arr As New ArrayList()
        Dim qry As String = "select TSPL_PAYMENT_HEADER.Payment_No as [Code] ,convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103) as [Date] ,TSPL_PAYMENT_HEADER.Entry_Desc as [Description],TSPL_PAYMENT_HEADER.Vendor_Code as [Vendor Code] ,TSPL_PAYMENT_HEADER.Vendor_Name as [Vendor Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] ,TSPL_PAYMENT_HEADER.Payment_Post_Date as [Payment Post Date] ,TSPL_PAYMENT_HEADER.Bank_Code as [Bank Code] ,TSPL_PAYMENT_HEADER.Payment_Type as [Payment Type] ,TSPL_PAYMENT_HEADER.Remit_To as [Remit To]  ,TSPL_PAYMENT_HEADER.Reference as [Reference] ,TSPL_PAYMENT_HEADER.Narration as [Narration] ,TSPL_PAYMENT_HEADER.Payment_Code as [Payment Code] ,TSPL_PAYMENT_HEADER.Cheque_No as [Cheque No] ,TSPL_PAYMENT_HEADER.Cheque_Date as [Cheque Date] ,TSPL_PAYMENT_HEADER.Payment_Amount as [Payment Amount] ,TSPL_PAYMENT_HEADER.Vendor_Account_Set as [Vendor Account Set] ,TSPL_PAYMENT_HEADER.TDS_Amount as [Tds Amount] ,TSPL_PAYMENT_HEADER.Total_Prepayment as [Total Prepayment] ,TSPL_PAYMENT_HEADER.Apply_By as [Apply By] ,TSPL_PAYMENT_HEADER.Apply_To as [Apply To] ,TSPL_PAYMENT_HEADER.Posted as [Posted] ,TSPL_PAYMENT_HEADER.Created_By as [Created By] ,TSPL_PAYMENT_HEADER.Created_Date as [Created Date] ,TSPL_PAYMENT_HEADER.Modify_By as [Modify By] ,TSPL_PAYMENT_HEADER.Modify_Date as [Modify Date] ,TSPL_PAYMENT_HEADER.Level1_User_code as [Level1 User Code] ,TSPL_PAYMENT_HEADER.Level2_User_code as [Level2 User Code] ,TSPL_PAYMENT_HEADER.Level3_User_code as [Level3 User Code] ,TSPL_PAYMENT_HEADER.Level4_User_code as [Level4 User Code] ,TSPL_PAYMENT_HEADER.Level5_User_code as [Level5 User Code] ,TSPL_PAYMENT_HEADER.Comp_Code as [Comp Code] ,TSPL_PAYMENT_HEADER.Debit_Account as [Debit Account] ,TSPL_PAYMENT_HEADER.Credit_Account as [Credit Account] ,TSPL_PAYMENT_HEADER.Balance_Amt as [Balance Amt] ,TSPL_PAYMENT_HEADER.Total_Applied_Amount as [Total Applied Amount] ,TSPL_PAYMENT_HEADER.Transport_Id as [Transport Id] ,TSPL_PAYMENT_HEADER.FIFO_Balance as [Fifo Balance] ,TSPL_PAYMENT_HEADER.QuickEntryNo as [Quickentryno] ,TSPL_PAYMENT_HEADER.LoadOutNo as [Loadoutno] ,TSPL_PAYMENT_HEADER.Salesman_Code as [Salesman Code] ,TSPL_PAYMENT_HEADER.Salesman_Name as [Salesman Name] ,TSPL_PAYMENT_HEADER.Route_NO as [Route No] ,TSPL_PAYMENT_HEADER.Route_Description as [Route Description] ,TSPL_PAYMENT_HEADER.Location_Code as [Location Code] ,TSPL_PAYMENT_HEADER.Location_Description as [Location Description] ,TSPL_PAYMENT_HEADER.IsRecoCleared as [Isrecocleared] ,TSPL_PAYMENT_HEADER.IsChkReverse as [Ischkreverse] ,TSPL_PAYMENT_HEADER.Loadout_No as [Loadout No] ,TSPL_PAYMENT_HEADER.Bank_Charges_Ac as [Bank Charges Ac] ,TSPL_PAYMENT_HEADER.Bank_Charges as [Bank Charges] ,TSPL_PAYMENT_HEADER.CURRENCY_CODE as [Currency Code] ,TSPL_PAYMENT_HEADER.ConvRate as [Convrate] ,TSPL_PAYMENT_HEADER.ApplicableFrom as [Applicablefrom] ,TSPL_PAYMENT_HEADER.BASE_CURRENCY_CODE as [Base Currency Code] ,TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT_BASE_CURRENCY as [Payment Amount Base Currency] ,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as [Exchange Loss Amt] ,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as [Exchange Gain Amt] ,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account] ,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] ,TSPL_PAYMENT_HEADER.ConvRateOld as [Convrateold] ,TSPL_PAYMENT_HEADER.CFormRecd as [Cformrecd] ,TSPL_PAYMENT_HEADER.CForm_InvoiceNo as [Cform Invoiceno] ,TSPL_PAYMENT_HEADER.EMP_CODE as [Emp Code] ,TSPL_PAYMENT_HEADER.PROJECT_CODE as [Project Code] ,TSPL_PAYMENT_HEADER.PDC_Cheque as [Pdc Cheque] ,TSPL_PAYMENT_HEADER.Document_No as [Document No] ,TSPL_PAYMENT_HEADER.CHECK_PRINT as [Check Print] ,TSPL_PAYMENT_HEADER.CHECK_CODE as [Check Code] ,TSPL_PAYMENT_HEADER.memorandum_amt as [Memorandum Amt] ,TSPL_PAYMENT_HEADER.Applied_Payment as [Applied Payment]  From TSPL_PAYMENT_HEADER " & _
          " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code " & _
          " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PAYMENT_HEADER.Vendor_Code "
        clsCommon.ShowSelectForm("VPFAPay", qry, "Code", "", OvlRptPayE.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPayE.Tag) > 0 Then
            arr.Add(OvlRptPayE.Tag)
            FrmPaymentEntry.funReport("", "", arr, Nothing, Nothing)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    Private Sub OvlRptQuickE_Click(sender As Object, e As EventArgs) Handles OvlRptQuickE.Click
        Dim Qry As String = String.Empty
        Qry = "Select QuickEntryNo, Type, Receipt_Date, TSPL_BANK_MASTER. BANK_CODE, DESCRIPTION, ADD1, ADD2, ADD3, CITY, STATE, POSTAL, COUNTRY, CONTACT, PHONE, FAX, INACTIVE, BANKACCNUMBER, BANKACC, WRITEOFFACC, Bank_type from (select Distinct QuickEntryNo ,Case When Receipt_Type='O' Then 'Receipt' When Receipt_Type='M' Then 'Misc Receipt' End as [Type],Receipt_Date, Bank_Code  from TSPL_RECEIPT_HEADER Union select Distinct QuickEntryNo ,'Payment' as [Type], CONVERT(VARCHAR,Payment_Date,102) as Payment_Date, Bank_Code  from TSPL_PAYMENT_HEADER ) AAA Left Outer Join TSPL_BANK_MASTER on AAA.Bank_Code=TSPL_BANK_MASTER.BANK_CODE"
        Dim WhrCls As String = " ISNULL(QuickEntryNo,'')<>'' "
        OvlRptQuickE.Tag = clsCommon.ShowSelectForm("VPFAPay", Qry, "Code", WhrCls, OvlRptQuickE.Tag, "Code", True)
        If clsCommon.myLen(OvlRptQuickE.Tag) > 0 Then
            Dim frm As New FrmQuickEntry1(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.funprint(OvlRptQuickE.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
#End Region

    Private Sub FrmAPCycle_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ProgramName()
        OvlTag()
        DBBind()
    End Sub
#Region "MouseEvents"
    Private Sub OvlAPInv_MouseEnter(sender As Object, e As EventArgs) Handles OvlPayE.MouseEnter, OvlAPInv.MouseEnter, OvlQuickE.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(255, 231, 161)
    End Sub
    Private Sub OvlAPInv_MouseLeave(sender As Object, e As EventArgs) Handles OvlPayE.MouseLeave, OvlAPInv.MouseLeave, OvlQuickE.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
    Private Sub OvlAPInv_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlAPInv.MouseUp, OvlQuickE.MouseUp, OvlPayE.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
#End Region

    Private Sub OvlRptPayE_MouseEnter(sender As Object, e As EventArgs) Handles OvlRptPayE.MouseEnter, OvlRptQuickE.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.SkyBlue
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub

    Private Sub OvlRptPayE_MouseLeave(sender As Object, e As EventArgs) Handles OvlRptPayE.MouseLeave, OvlRptQuickE.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub

    Private Sub OvlRptPayE_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlRptPayE.MouseUp, OvlRptQuickE.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class