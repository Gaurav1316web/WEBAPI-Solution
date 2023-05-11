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


Public Class FrmPurchaseCycle
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseCycle)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub ProgramName()
        LblPurInd.Tag = clsUserMgtCode.mbtnPurchaseRequistion
        LblPurInd.Text = ProgramCodeNew.GetProgramName(LblPurInd.Tag)
        LblPO.Tag = clsUserMgtCode.mbtnPurchaseOrder
        LblPO.Text = ProgramCodeNew.GetProgramName(LblPO.Tag)
        LblGRN.Tag = clsUserMgtCode.mbtnGRN
        LblGRN.Text = ProgramCodeNew.GetProgramName(LblGRN.Tag)
        LblMRN.Tag = clsUserMgtCode.mbtnMRN
        LblMRN.Text = ProgramCodeNew.GetProgramName(LblMRN.Tag)
        LblSRN.Tag = clsUserMgtCode.mbtnSRN
        LblSRN.Text = ProgramCodeNew.GetProgramName(LblSRN.Tag)
        LblPI.Tag = clsUserMgtCode.mbtnPurchaseInvoice
        LblPI.Text = ProgramCodeNew.GetProgramName(LblPI.Tag)
        LblPR.Tag = clsUserMgtCode.mbtnPurchaseReturn
        LblPR.Text = ProgramCodeNew.GetProgramName(LblPR.Tag)

        LblPurIndSet.Tag = clsUserMgtCode.mbtnPurchaseRequistion
        LblPurIndSet.Text = ProgramCodeNew.GetProgramName(LblPurIndSet.Tag)
        LblPOSet.Tag = clsUserMgtCode.mbtnPurchaseOrder
        LblPOSet.Text = ProgramCodeNew.GetProgramName(LblPOSet.Tag)
        LblSRNSet.Tag = clsUserMgtCode.mbtnSRN
        LblSRNSet.Text = ProgramCodeNew.GetProgramName(LblSRNSet.Tag)
        LbPISet.Tag = clsUserMgtCode.mbtnPurchaseInvoice
        LbPISet.Text = ProgramCodeNew.GetProgramName(LbPISet.Tag)
        LblPRSet.Tag = clsUserMgtCode.mbtnPurchaseReturn
        LblPRSet.Text = ProgramCodeNew.GetProgramName(LblPRSet.Tag)

        LblRptPurInd.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnPurchaseRequistion)
        LblRptPO.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnPurchaseOrder)
        LblRptGRN.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnGRN)
        LblRptMRN.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnMRN)
        LblRptSRN.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnSRN)
        LblRptPI.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnPurchaseInvoice)
        LblRptPR.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.mbtnPurchaseReturn)
    End Sub
    Private Sub OvlTag()
        OvlPurInd.Tag = clsUserMgtCode.mbtnPurchaseRequistion
        OvlPO.Tag = clsUserMgtCode.mbtnPurchaseOrder
        OvlGRN.Tag = clsUserMgtCode.mbtnGRN
        OvlMRN.Tag = clsUserMgtCode.mbtnMRN
        OvlSRN.Tag = clsUserMgtCode.mbtnSRN
        OvlPI.Tag = clsUserMgtCode.mbtnPurchaseInvoice
        OvlPR.Tag = clsUserMgtCode.mbtnPurchaseReturn

        OvlPurIndSet.Tag = clsUserMgtCode.mbtnPurchaseRequistion
        OvlPOSet.Tag = clsUserMgtCode.mbtnPurchaseOrder
        OvlSRNSet.Tag = clsUserMgtCode.mbtnSRN
        OvlPISet.Tag = clsUserMgtCode.mbtnPurchaseInvoice
        OvlPRSet.Tag = clsUserMgtCode.mbtnPurchaseReturn

        OvlVen.Tag = clsUserMgtCode.vendormaster
        OvlItem.Tag = clsUserMgtCode.itemMaster
    End Sub
    Private Sub DBBind()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='SMPurReport' And VPF_Active_Report=1 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                i = i + 25
                Dim lbl As New Label
                lbl.Text = clsCommon.myCstr(dr("Name"))
                Me.Controls.Add(lbl)
                '  lbl.Location = New Point(437, 372 + i)
                lbl.Location = New Point(464, 350 + i)
                LocY = 375 + i
                If clsCommon.myCdbl(LocY) > 639 Then
                    j = j + 25
                    lbl.Location = New Point(729, 351 + j)
                End If
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
                oval.Location = New Point(400, 347 + i)
                OvalY = 347 + i
                If clsCommon.myCdbl(LocY) > 639 Then
                    m = m + 25
                    oval.Location = New Point(685, 351 + m)
                End If
                oval.Tag = clsCommon.myCstr(dr("Code"))
                AddHandler oval.Click, AddressOf oval_Click
                AddHandler oval.MouseEnter, AddressOf oval_MouseEnter
                AddHandler oval.MouseLeave, AddressOf oval_MouseLeave
                AddHandler oval.MouseUp, AddressOf oval_MouseUp
            Next
        End If
        'Dim Pan As Panel = New Panel()
        'Me.Controls.Add(Pan)
        'Pan.Location = New Point(368, 390)
        'Pan.Size = New Point(274, 192)
        'Pan.BorderStyle = BorderStyle.FixedSingle
        'Pan.BackColor = Color.Gainsboro
        RectangleShape1.Visible = False
    End Sub

    Private Sub oval_MouseEnter(sender As Object, e As EventArgs)
        '    clsCommon.MyMessageBoxShow("Done")
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.SkyBlue
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseLeave(sender As Object, e As EventArgs)
        '    clsCommon.MyMessageBoxShow("Done")
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseUp(sender As Object, e As EventArgs)
        '    clsCommon.MyMessageBoxShow("Done")
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)

        'Dim baseDice As New RectangleShape
        'Dim shapeContainer As New ShapeContainer

        'shapeContainer.Parent = Me
        'baseDice.Parent = shapeContainer
        'baseDice.CornerRadius = 5
        'baseDice.Height = 50
        'baseDice.Width = 50
        'baseDice.BackColor = Color.Blue
        'baseDice.Left = 50
        'baseDice.Top = 50
        'baseDice.Location = New Point(367, 390)

        ' Declare a new oval shape to add to the form. 
        Dim oval As OvalShape = New OvalShape()
        ' Add the oval shape to the form.
        'shapeContainer.Shapes.Add(oval)
        RectangleShape1.Parent.Shapes.Add(oval)
        oval.Location = New Point(392, 424)
        oval.Size = New Size(21, 17)
        oval.FillStyle = FillStyle.LightHorizontal
        oval.BorderColor = Color.Black
        oval.BorderWidth = 2
        oval.Location = New Point(380, 402)
        AddHandler oval.Click, AddressOf oval_Click

        'Dim lb As New Label

        'lb.Name = "LblDRpt"
        'lb.Text = "PO Report"
        'lb.Location = New Point(415, 428) ' change this if you want
        'Me.Controls.Add(lb)

        Dim FILE_NAME As String = "D:\1.txt"
        Dim i As Integer = 1
        For Each line As String In System.IO.File.ReadAllLines(FILE_NAME)
            Dim NextLabel As New Label

            ' Dim Nextbtn As New Button
            ' Nextbtn.Tag = NextLabel
            '    NextLabel.Text = line
            'Nextbtn.Text = "Copy"
            '  NextLabel.Height = 20
            ' Nextbtn.Width = 55
            ' Nextbtn.Height = 400
            ' NextLabel.BackColor = Color.Yellow
            ' Me.Controls.Add(NextLabel)
            ' Me.Controls.Add(Nextbtn)
            'NextLabel.Location = New Point(346, 400 * i + ((i - 1) * NextLabel.Height))
            ' Nextbtn.Location = New Point(415, 10 * i + ((i - 1) * Nextbtn.Height))
            '  AddHandler oval.Click, AddressOf oval_click Handle
            '  AddHandler oval.Click, AddressOf oval_Click
            i += 1
        Next
    End Sub

    Private Sub oval_Click(sender As Object, e As EventArgs)
        Dim s As String = clsCommon.myCstr(DirectCast(sender, OvalShape).Tag)
        'clsCommon.MyMessageBoxShow("Done")
        MDI.ShowForm(s, "", True)
    End Sub
    Private Sub FrmPurchaseCycle_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ShowGRN As Integer = 0
        Dim ShowMRN As Integer = 0

        SetUserMgmtNew()
        ProgramName()
        OvlTag()
        DBBind()
        ShowGRN = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_fixed_parameter  where type='ShowGRN'"))
        ShowMRN = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_fixed_parameter  where type='ShowMRN'"))

        If clsCommon.CompairString(ShowGRN, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ShowMRN, "1") = CompairStringResult.Equal Then
            PnlSetOff.Visible = True
            PnlSetOff.Location = New Point(3, 141)
            PnlSetON.Visible = False
            PnlSetON.Location = New Point(3, 222)
        Else
            PnlSetON.Visible = True
            PnlSetON.Location = New Point(3, 141)
            PnlSetOff.Visible = False
            PnlSetOff.Location = New Point(3, 222)
        End If
        Panel1.BackColor = Color.FromArgb(30, 20, 30, 30)
        Panel1.BorderStyle = BorderStyle.None
    End Sub

    Private Sub OvlPurInd_Click(sender As Object, e As EventArgs) Handles OvlPurInd.Click
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, "")
    End Sub

    Private Sub OvalPO_Click(sender As Object, e As EventArgs) Handles OvlPO.Click
        MDI.ShowForm(LblPO.Tag, "", False)
    End Sub

    Private Sub OvlGRN_Click(sender As Object, e As EventArgs) Handles OvlGRN.Click
        MDI.ShowForm(LblGRN.Tag, "", False)
    End Sub

    Private Sub OvlMRN_Click(sender As Object, e As EventArgs) Handles OvlMRN.Click
        MDI.ShowForm(LblMRN.Tag, "", False)
    End Sub

    Private Sub OvlSRN_Click(sender As Object, e As EventArgs) Handles OvlSRN.Click
        MDI.ShowForm(LblSRN.Tag, "", False)
    End Sub

    Private Sub OvlPI_Click(sender As Object, e As EventArgs) Handles OvlPI.Click
        MDI.ShowForm(LblPI.Tag, "", False)
    End Sub

    Private Sub OvlPR_Click(sender As Object, e As EventArgs) Handles OvlPR.Click
        MDI.ShowForm(LblPR.Tag, "", False)
    End Sub

    Private Sub OvlVen_Click(sender As Object, e As EventArgs) Handles OvlVen.Click
        MDI.ShowForm(OvlVen.Tag, "", False)
    End Sub

    Private Sub OvlItem_Click(sender As Object, e As EventArgs) Handles OvlItem.Click
        MDI.ShowForm(OvlItem.Tag, "", False)
    End Sub

    Private Sub OvlRptPurInd_Click(sender As Object, e As EventArgs) Handles OvlRptPurInd.Click
        Dim Qry As String = String.Empty
        Qry = "select Requisition_Id as Code,Requisition_Date as Date,Description, case when Status='0' then 'Pending' else 'Approved' end as [Status],case when  Is_Internal='Y' then 'Internal' else 'External' end as Internal from TSPL_REQUISITION_HEAD"
        OvlRptPurInd.Tag = clsCommon.ShowSelectForm("VPFPurInd", Qry, "Code", "", OvlRptPurInd.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPurInd.Tag) > 0 Then
            frmPurchaseRequistion.funPrint(OvlRptPurInd.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If

    End Sub

    Private Sub OvlRptPO_Click(sender As Object, e As EventArgs) Handles OvlRptPO.Click
        Dim Qry As String = String.Empty
        Dim IsMTBool As Boolean
        Dim ISPOBool As Boolean
        Dim IsMT As Integer = 0
        Dim ISPO As Integer = 0

        Qry = "select PurchaseOrder_No as PONO,convert (varchar(10), PurchaseOrder_Date,103) as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],PO_Total_Amt as Amount,case when TSPL_PURCHASE_ORDER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],Bill_To_Location as [Location],(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =Bill_To_Location ) as [Location Name] from TSPL_PURCHASE_ORDER_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code  "
        OvlRptPO.Tag = clsCommon.ShowSelectForm("VPFPO", Qry, "PONO", "", OvlRptPO.Tag, "PONO", True)
        If clsCommon.myLen(OvlRptPO.Tag) > 0 Then
            IsMT = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT MT_Is_Merchant_Trade FROM tspl_purchase_order_head WHERE PurchaseOrder_No ='" & clsCommon.myCstr(OvlRptPO.Tag) & "'"))
            IsMTBool = IIf(clsCommon.myCdbl(IsMT) = 1, True, False)
            ISPO = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT IsPO FROM tspl_purchase_order_head WHERE PurchaseOrder_No ='" & clsCommon.myCstr(OvlRptPO.Tag) & "'"))
            ISPOBool = IIf(clsCommon.myCdbl(ISPO) = 1, True, False)
            Dim obj As New clsPurchaseOrderHead
            obj.PrintData(OvlRptPO.Tag)
            obj = Nothing
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptGRN_Click(sender As Object, e As EventArgs) Handles OvlRptGRN.Click
        Dim Qry As String = String.Empty
        Qry = "select GRN_No as Code,GRN_Date as Date,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code], TSPL_GRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],GRN_Total_Amt as Amount,case when TSPL_GRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],Against_PO as [Against PO Code] from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code"
        OvlRptGRN.Tag = clsCommon.ShowSelectForm("VPFGRN", Qry, "Code", "", OvlRptGRN.Tag, "Code", True)
        If clsCommon.myLen(OvlRptGRN.Tag) > 0 Then
            frmGRN.print(OvlRptGRN.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptMRN_Click(sender As Object, e As EventArgs) Handles OvlRptMRN.Click
        Dim Qry As String = String.Empty
        Qry = "select MRN_No as Code,MRN_Date as Date,TSPL_MRN_HEAD.Vendor_Code as [Vendor Code], TSPL_MRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],MRN_Total_Amt as Amount,case when TSPL_MRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],Against_GRN as [Against GRN Code]  from TSPL_MRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MRN_HEAD.Vendor_Code"
        OvlRptMRN.Tag = clsCommon.ShowSelectForm("VPFMRN", Qry, "Code", "", OvlRptMRN.Tag, "Code", True)
        If clsCommon.myLen(OvlRptMRN.Tag) > 0 Then
            frmMRN.PrintData(OvlRptMRN.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptSRN_Click(sender As Object, e As EventArgs) Handles OvlRptSRN.Click
        Dim Qry As String = String.Empty
        Qry = "select SRN_No as Code,CONVERT(varchar(10), SRN_Date,103)+' '+ CONVERT(varchar(5), SRN_Date,114) as Date,TSPL_SRN_HEAD.Vendor_Code as [Vendor Code], TSPL_SRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],SRN_Total_Amt as Amount,case when TSPL_SRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],Against_QC_Code as [Against QC Code],Against_QC_Date as [Against QC Date] from TSPL_SRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code "
        OvlRptSRN.Tag = clsCommon.ShowSelectForm("VPFSRN", Qry, "Code", "", OvlRptSRN.Tag, "Code", True)
        If clsCommon.myLen(OvlRptSRN.Tag) > 0 Then
            frmSRN.PrintDataNew(OvlRptSRN.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptPI_Click(sender As Object, e As EventArgs) Handles OvlRptPI.Click
        Dim Qry As String = String.Empty
        Qry = "select PI_No as Code,convert(varchar,PI_Date,103) as Date,TSPL_PI_HEAD.Vendor_Code as [Vendor Code], TSPL_PI_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_Invoice_No as [Vendor Invoice No],Convert(Varchar(12),InvoiceDate,103) as [Invoice Date],PI_Total_Amt as Amount,case when TSPL_PI_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] ,(select top 1 PJV_No from TSPL_PJV_HEAD where Invoice_No=PI_No) as [PJV No],Against_SRN as [SRN No] from TSPL_PI_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PI_HEAD.Vendor_Code "
        OvlRptPI.Tag = clsCommon.ShowSelectForm("VPFPI", Qry, "Code", "", OvlRptPI.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPI.Tag) > 0 Then
            frmPurchaseInvoice.print(OvlRptPI.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlPO_MouseEnter(sender As Object, e As EventArgs) Handles OvlPO.MouseEnter, OvlPI.MouseEnter, OvlGRN.MouseEnter, OvlMRN.MouseEnter, OvlSRN.MouseEnter, OvlPurInd.MouseEnter, OvlPR.MouseEnter, OvlPOSet.MouseEnter, OvlPISet.MouseEnter, OvlSRNSet.MouseEnter, OvlPurIndSet.MouseEnter, OvlPRSet.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(255, 231, 161)
    End Sub

    Private Sub OvlPO_MouseLeave(sender As Object, e As EventArgs) Handles OvlPO.MouseLeave, OvlPI.MouseLeave, OvlGRN.MouseLeave, OvlMRN.MouseLeave, OvlSRN.MouseLeave, OvlPurInd.MouseLeave, OvlPR.MouseLeave, OvlPOSet.MouseLeave, OvlPISet.MouseLeave, OvlSRNSet.MouseLeave, OvlPurIndSet.MouseLeave, OvlPRSet.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub

    Private Sub OvlPO_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlPO.MouseUp, OvlPI.MouseUp, OvlGRN.MouseUp, OvlMRN.MouseUp, OvlSRN.MouseUp, OvlPurInd.MouseUp, OvlPR.MouseUp, OvlPOSet.MouseUp, OvlPISet.MouseUp, OvlSRNSet.MouseUp, OvlPurIndSet.MouseUp, OvlPRSet.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub

    Private Sub OvlVen_MouseEnter(sender As Object, e As EventArgs) Handles OvlVen.MouseEnter, OvlItem.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(255, 231, 161)
    End Sub

    Private Sub OvlVen_MouseLeave(sender As Object, e As EventArgs) Handles OvlVen.MouseLeave, OvlItem.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.FromArgb(43, 153, 204)
    End Sub

    Private Sub OvlVen_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlVen.MouseUp, OvlItem.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.FromArgb(43, 153, 204)
    End Sub

    Private Sub OvlPurIndSet_Click(sender As Object, e As EventArgs) Handles OvlPurIndSet.Click
        MDI.ShowForm(LblPurIndSet.Tag, "", False)
    End Sub

    Private Sub OvlPRSet_Click(sender As Object, e As EventArgs) Handles OvlPRSet.Click
        MDI.ShowForm(LblPRSet.Tag, "", False)
    End Sub

    Private Sub OvlSRNSet_Click(sender As Object, e As EventArgs) Handles OvlSRNSet.Click
        MDI.ShowForm(LblSRNSet.Tag, "", False)
    End Sub

    Private Sub OvlRptPR_Click(sender As Object, e As EventArgs) Handles OvlRptPR.Click
        Dim Qry As String = String.Empty
        Qry = "select PR_No as Code,PR_Date as Date,TSPL_PR_HEAD.Vendor_Code as [Vendor Code], TSPL_PR_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_invoice_No as [Vendor Invoice Number],Against_SRN as [SRN Num],PR_Total_Amt as Amount,case when TSPL_PR_HEAD.Status='0' then 'Pending' else 'Approved' end as Status from TSPL_PR_HEAD  LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PR_HEAD.Vendor_Code "
        OvlRptPR.Tag = clsCommon.ShowSelectForm("VPFPR", Qry, "Code", "", OvlRptPR.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPR.Tag) > 0 Then
            frmPurchaseReturn.printReport1(OvlRptPR.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptPurInd_MouseEnter(sender As Object, e As EventArgs) Handles OvlRptPurInd.MouseEnter, OvlRptGRN.MouseEnter, OvlRptMRN.MouseEnter, OvlRptSRN.MouseEnter, OvlRptPI.MouseEnter, OvlRptPO.MouseEnter, OvlRptPR.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.SkyBlue
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub

    Private Sub OvlRptPurInd_MouseLeave(sender As Object, e As EventArgs) Handles OvlRptPurInd.MouseLeave, OvlRptGRN.MouseLeave, OvlRptMRN.MouseLeave, OvlRptSRN.MouseLeave, OvlRptPI.MouseLeave, OvlRptPO.MouseLeave, OvlRptPR.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub

    Private Sub OvlRptPurInd_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlRptPurInd.MouseUp, OvlRptGRN.MouseUp, OvlRptMRN.MouseUp, OvlRptSRN.MouseUp, OvlRptPI.MouseUp, OvlRptPO.MouseUp, OvlRptPR.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub


End Class