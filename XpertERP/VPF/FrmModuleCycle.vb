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
Imports XpertERPSalesAndDistribution
Imports XpertERPProjectManagement


Public Class FrmModuleCycle
    Inherits FrmMainTranScreen
    Dim CreateFreshInvoiceOnDispatchSave As Integer = 0
    Dim userCode, companyCode As String
    Dim str As String
    Dim ScreenCode As String
    Dim Program_Code_ProgramMaster As String = String.Empty
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim VertAxis As Integer = 0
    Dim HorAxis As Integer = 0

    Public Sub New(ByVal FormID As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ScreenCode = FormID
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(ModuleCode)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
    End Sub
    Private Sub DBBindRpt(ByVal StrRpt As String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0
        Dim ShapeContainerNew As Microsoft.VisualBasic.PowerPacks.ShapeContainer = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & StrRpt & "' And VPF_Active_Report=1 "
        ' qry += " UNION ALL Select '' As Code ,'' As Name  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                i = i + 25
                Dim lbl As New Label
                lbl.Text = clsCommon.myCstr(dr("Name"))
                lbl.Location = New Point(63, (-14) + i)
                lbl.BackColor = Color.Transparent
                Dim myfont As New Font("Arial", 9.75, FontStyle.Bold)
                lbl.Font = myfont
                lbl.AutoSize = True

                'Dim img As Image
                'img = Image.FromFile("C:\Users\tecxpert51\Desktop\Icon2.jpg")
                'LVDRpt.Items.Add(img)

                'Dim item As New ListViewDataItem()
                'item.Tag = clsCommon.myCstr(dr("Code"))
                'Me.LVDRpt.Items.Add(item)
                'item("Name") = clsCommon.myCstr(dr("Name"))

                '' Oval
                Dim oval As Microsoft.VisualBasic.PowerPacks.OvalShape = New Microsoft.VisualBasic.PowerPacks.OvalShape()
                oval.Name = "CustomOvel" + clsCommon.myCstr(i)
                ' Add the oval shape to the form.
                Me.PanelDRpt.Controls.Add(ShapeContainerNew)
                Me.PanelDRpt.Controls.Add(lbl)
                Me.PanelDRpt.AutoScroll = True
                ShapeContainerNew.AutoScroll = False
                '  Me.LVDRpt.Items.Add(oval)
                ' If clsCommon.myLen(clsCommon.myCstr(dr("Code"))) > 0 Then
                ShapeContainerNew.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {oval})
                oval.Size = New Size(21, 17)
                oval.FillStyle = FillStyle.LightHorizontal
                OvalY = 363 + i
                oval.BorderColor = Color.Black
                oval.BorderWidth = 2
                oval.Location = New Point(17, (-15) + i)
                oval.Tag = clsCommon.myCstr(dr("Code"))
                'item.Tag = clsCommon.myCstr(dr("Code"))

                '' Me.LVDRpt.Items.Add(item)
                'item("Name") = clsCommon.myCstr(dr("Name"))

                ' End If
                oval.BringToFront()
                lbl.BringToFront()

                AddHandler oval.Click, AddressOf oval_Click
                AddHandler oval.MouseEnter, AddressOf oval_MouseEnter
                AddHandler oval.MouseLeave, AddressOf oval_MouseLeave
                AddHandler oval.MouseUp, AddressOf oval_MouseUp
            Next
        End If

        RectangleShape1.Visible = False
        RectangleShape1.BringToFront()
        PanelDRpt.BringToFront()

    End Sub
    Private Sub DBBindMasters(ByVal StrMaster As String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0
        Dim ShapeContainerNew As Microsoft.VisualBasic.PowerPacks.ShapeContainer = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & StrMaster & "' And VPF_Active_Report=1 "
        '  qry += " UNION ALL Select '' As Code ,'' As Name  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                i = i + 25
                Dim lbl As New Label
                lbl.Text = clsCommon.myCstr(dr("Name"))
                lbl.Location = New Point(63, (-14) + i)
                lbl.BackColor = Color.Transparent
                Dim myfont As New Font("Arial", 9.75, FontStyle.Bold)
                lbl.Font = myfont
                lbl.AutoSize = True

                '' Oval

                Dim ovalM As Microsoft.VisualBasic.PowerPacks.OvalShape = New Microsoft.VisualBasic.PowerPacks.OvalShape()

                ovalM.Name = "MasOvl" + clsCommon.myCstr(i)

                ' Add the oval shape to the form.
                Me.PnlMas.Controls.Add(ShapeContainerNew)
                Me.PnlMas.Controls.Add(lbl)
                Me.PnlMas.AutoScroll = True
                ShapeContainerNew.AutoScroll = False
                ' If clsCommon.myLen(clsCommon.myCstr(dr("Code"))) > 0 Then
                ShapeContainerNew.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {ovalM})
                ovalM.Size = New Size(20, 17)
                ' OvalY = 363 + i
                ovalM.BorderColor = Color.FromArgb(0, 64, 133)
                ovalM.BackColor = Color.White
                ovalM.BackStyle = BackStyle.Opaque
                ovalM.BorderWidth = 2
                ovalM.Location = New Point(17, (-15) + i)
                ' End If
                ovalM.Tag = clsCommon.myCstr(dr("Code"))
                'End If
                ovalM.BringToFront()
                lbl.BringToFront()

                AddHandler ovalM.Click, AddressOf ovalM_Click
                AddHandler ovalM.MouseEnter, AddressOf ovalM_MouseEnter
                AddHandler ovalM.MouseLeave, AddressOf ovalM_MouseLeave
                AddHandler ovalM.MouseUp, AddressOf ovalM_MouseUp
            Next
        End If
        RectangleShape1.Visible = False
        RectangleShape1.BringToFront()
        PanelDRpt.BringToFront()
        ' Panel4.Location = New Point(295, 256)

    End Sub
    Private Sub ovalM_Click(sender As Object, e As EventArgs)
        Dim s As String = clsCommon.myCstr(DirectCast(sender, OvalShape).Tag)
        MDI.ShowForm(s, "", True)
    End Sub
    Private Sub ovalM_MouseEnter(sender As Object, e As EventArgs)
        VertAxis = PanelDRpt.VerticalScroll.Value
        HorAxis = PanelDRpt.HorizontalScroll.Value
        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BorderColor = Color.Black
        ovalM.BackColor = Color.LightGray
        ovalM.BackStyle = BackStyle.Opaque
    End Sub
    Private Sub ovalM_MouseLeave(sender As Object, e As EventArgs)
        PanelDRpt.VerticalScroll.Value = VertAxis
        PanelDRpt.HorizontalScroll.Value = HorAxis
        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BackColor = Color.White
        ovalM.BackStyle = BackStyle.Opaque
        ovalM.BorderColor = Color.FromArgb(0, 64, 133)
    End Sub
    Private Sub ovalM_MouseUp(sender As Object, e As EventArgs)
        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BackColor = Color.White
        ovalM.BackStyle = BackStyle.Opaque
        ovalM.BorderColor = Color.FromArgb(0, 64, 133)
    End Sub
#Region "DReportsOval"

    Private Sub oval_MouseEnter(sender As Object, e As EventArgs)
        VertAxis = PanelDRpt.VerticalScroll.Value
        HorAxis = PanelDRpt.HorizontalScroll.Value
        'Dim oval As OvalShape = DirectCast(sender, OvalShape)
        'oval.BackColor = Color.SkyBlue
        'oval.BorderColor = Color.Black
        'oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseLeave(sender As Object, e As EventArgs)
        PanelDRpt.VerticalScroll.Value = VertAxis
        PanelDRpt.HorizontalScroll.Value = HorAxis
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
        'Dim point As New Point(intx, inty)
        'PanelDRpt.AutoScrollPosition = point
    End Sub
#End Region
#Region "TClicks"
    'Private Sub OvlTankerMas_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblTankerMas.Tag, "", False)
    'End Sub
    'Private Sub OvlParMas_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblParMas.Tag, "", False)
    'End Sub
    'Private Sub OvlPriceCMB_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblPriceCMB.Tag, "", False)
    'End Sub
    'Private Sub OvlVenPCM_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblVenPCM.Tag, "", False)
    'End Sub
#End Region
#Region "MClicks"
    'Private Sub OvlGateEntry_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblGateEntry.Tag, "", False)
    'End Sub
    'Private Sub OvlWeighment_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblWeighment.Tag, "", False)
    'End Sub
    'Private Sub OvlQCheck_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblQC.Tag, "", False)
    'End Sub
    'Private Sub OvlUnloading_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblUnloading.Tag, "", False)
    'End Sub
    'Private Sub OvlBulkSRN_Click(sender As Object, e As EventArgs)
    '    MDI.ShowForm(LblBulkSRN.Tag, "", False)
    'End Sub
#End Region
#Region "MouseEvents"
    Private Sub OvlGateEntry_MouseEnter(sender As Object, e As EventArgs)
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(255, 231, 161)
    End Sub
    Private Sub OvlGateEntry_MouseLeave(sender As Object, e As EventArgs)
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
    Private Sub OvlGateEntry_MouseUp(sender As Object, e As MouseEventArgs)
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
#End Region
#Region "ReportsClick"
    Private Sub OvlRptBulkSRN_Click(sender As Object, e As EventArgs) Handles OvlRptBulkSRN.Click
        OvlRptBulkSRN.Tag = clsBulkMilkSRN.getFinder("", OvlRptBulkSRN.Tag, True)
        If clsCommon.myLen(OvlRptBulkSRN.Tag) > 0 Then
            Dim FrmBulkMilkSRN1 As New FrmBulkMilkSRN
            FrmBulkMilkSRN1.printData(OvlRptBulkSRN.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    Private Sub OvlRptQC_Click(sender As Object, e As EventArgs) Handles OvlRptQC.Click
        OvlRptQC.Tag = clsQualityCheck.getFinder("", OvlRptQC.Tag, True)
        If clsCommon.myLen(OvlRptQC.Tag) > 0 Then
            Dim FrmQualityCheck1 As New FrmQualityCheck
            FrmQualityCheck1.funPrint(OvlRptQC.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    Private Sub OvlRptWeigh_Click(sender As Object, e As EventArgs) Handles OvlRptWeigh.Click
        OvlRptWeigh.Tag = clsWeighment.getFinder("", OvlRptWeigh.Tag, True)
        If clsCommon.myLen(OvlRptWeigh.Tag) > 0 Then
            Dim FrmWeighment1 As New FrmWeighment
            FrmWeighment1.funPrint(OvlRptWeigh.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    '' ----------------------- Purchase Section -----------------------------------
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
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
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
    '' ----------------------------------------------------------------------------
    '' ------------------------ MCC -----------------------------------------------
    Private Sub OvlRptTankerDis_Click(sender As Object, e As EventArgs) Handles OvlRptTankerDis.Click
        Dim Qry As String = String.Empty
        Dim MCC_Code As String = String.Empty
        Dim Mcc_Or_Plant_Code As String = String.Empty

        OvlRptTankerDis.Tag = clsMccDispatch.getFinder("", OvlRptTankerDis.Tag, True)
        MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Code From TSPL_MCC_DISPATCH_CHALLAN Where Chalan_NO ='" & OvlRptTankerDis.Tag & "'"))
        Mcc_Or_Plant_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Mcc_Or_Plant_Code From TSPL_MCC_DISPATCH_CHALLAN Where Chalan_NO ='" & OvlRptTankerDis.Tag & "'"))
        If clsCommon.myLen(OvlRptTankerDis.Tag) > 0 Then
            FrmMccDispatch.printData(OvlRptTankerDis.Tag, MCC_Code, Mcc_Or_Plant_Code)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' --------------------------------------------------------------------------------
    '' ---------------------------- AP Invoice ----------------------------------------
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
    Private Sub OvlRptPayE_Click(sender As Object, e As EventArgs) Handles OvlRptPayE.Click
        Dim arr As New ArrayList()
        Dim qry As String = "select TSPL_PAYMENT_HEADER.Payment_No as [Code] ,convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103) as [Date] ,TSPL_PAYMENT_HEADER.Entry_Desc as [Description],TSPL_PAYMENT_HEADER.Vendor_Code as [Vendor Code] ,TSPL_PAYMENT_HEADER.Vendor_Name as [Vendor Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] ,TSPL_PAYMENT_HEADER.Payment_Post_Date as [Payment Post Date] ,TSPL_PAYMENT_HEADER.Bank_Code as [Bank Code] ,TSPL_PAYMENT_HEADER.Payment_Type as [Payment Type] ,TSPL_PAYMENT_HEADER.Remit_To as [Remit To]  ,TSPL_PAYMENT_HEADER.Reference as [Reference] ,TSPL_PAYMENT_HEADER.Narration as [Narration] ,TSPL_PAYMENT_HEADER.Payment_Code as [Payment Code] ,TSPL_PAYMENT_HEADER.Cheque_No as [Cheque No] ,TSPL_PAYMENT_HEADER.Cheque_Date as [Cheque Date] ,TSPL_PAYMENT_HEADER.Payment_Amount as [Payment Amount] ,TSPL_PAYMENT_HEADER.Vendor_Account_Set as [Vendor Account Set] ,TSPL_PAYMENT_HEADER.TDS_Amount as [Tds Amount] ,TSPL_PAYMENT_HEADER.Total_Prepayment as [Total Prepayment] ,TSPL_PAYMENT_HEADER.Apply_By as [Apply By] ,TSPL_PAYMENT_HEADER.Apply_To as [Apply To] ,TSPL_PAYMENT_HEADER.Posted as [Posted] ,TSPL_PAYMENT_HEADER.Created_By as [Created By] ,TSPL_PAYMENT_HEADER.Created_Date as [Created Date] ,TSPL_PAYMENT_HEADER.Modify_By as [Modify By] ,TSPL_PAYMENT_HEADER.Modify_Date as [Modify Date] ,TSPL_PAYMENT_HEADER.Level1_User_code as [Level1 User Code] ,TSPL_PAYMENT_HEADER.Level2_User_code as [Level2 User Code] ,TSPL_PAYMENT_HEADER.Level3_User_code as [Level3 User Code] ,TSPL_PAYMENT_HEADER.Level4_User_code as [Level4 User Code] ,TSPL_PAYMENT_HEADER.Level5_User_code as [Level5 User Code] ,TSPL_PAYMENT_HEADER.Comp_Code as [Comp Code] ,TSPL_PAYMENT_HEADER.Debit_Account as [Debit Account] ,TSPL_PAYMENT_HEADER.Credit_Account as [Credit Account] ,TSPL_PAYMENT_HEADER.Balance_Amt as [Balance Amt] ,TSPL_PAYMENT_HEADER.Total_Applied_Amount as [Total Applied Amount] ,TSPL_PAYMENT_HEADER.Transport_Id as [Transport Id] ,TSPL_PAYMENT_HEADER.FIFO_Balance as [Fifo Balance] ,TSPL_PAYMENT_HEADER.QuickEntryNo as [Quickentryno] ,TSPL_PAYMENT_HEADER.LoadOutNo as [Loadoutno] ,TSPL_PAYMENT_HEADER.Salesman_Code as [Salesman Code] ,TSPL_PAYMENT_HEADER.Salesman_Name as [Salesman Name] ,TSPL_PAYMENT_HEADER.Route_NO as [Route No] ,TSPL_PAYMENT_HEADER.Route_Description as [Route Description] ,TSPL_PAYMENT_HEADER.Location_Code as [Location Code] ,TSPL_PAYMENT_HEADER.Location_Description as [Location Description] ,TSPL_PAYMENT_HEADER.IsRecoCleared as [Isrecocleared] ,TSPL_PAYMENT_HEADER.IsChkReverse as [Ischkreverse] ,TSPL_PAYMENT_HEADER.Loadout_No as [Loadout No] ,TSPL_PAYMENT_HEADER.Bank_Charges_Ac as [Bank Charges Ac] ,TSPL_PAYMENT_HEADER.Bank_Charges as [Bank Charges] ,TSPL_PAYMENT_HEADER.CURRENCY_CODE as [Currency Code] ,TSPL_PAYMENT_HEADER.ConvRate as [Convrate] ,TSPL_PAYMENT_HEADER.ApplicableFrom as [Applicablefrom] ,TSPL_PAYMENT_HEADER.BASE_CURRENCY_CODE as [Base Currency Code] ,TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT_BASE_CURRENCY as [Payment Amount Base Currency] ,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as [Exchange Loss Amt] ,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as [Exchange Gain Amt] ,TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account] ,TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] ,TSPL_PAYMENT_HEADER.ConvRateOld as [Convrateold] ,TSPL_PAYMENT_HEADER.CFormRecd as [Cformrecd] ,TSPL_PAYMENT_HEADER.CForm_InvoiceNo as [Cform Invoiceno] ,TSPL_PAYMENT_HEADER.EMP_CODE as [Emp Code] ,TSPL_PAYMENT_HEADER.PROJECT_CODE as [Project Code] ,TSPL_PAYMENT_HEADER.PDC_Cheque as [Pdc Cheque] ,TSPL_PAYMENT_HEADER.Document_No as [Document No] ,TSPL_PAYMENT_HEADER.CHECK_PRINT as [Check Print] ,TSPL_PAYMENT_HEADER.CHECK_CODE as [Check Code] ,TSPL_PAYMENT_HEADER.memorandum_amt as [Memorandum Amt] ,TSPL_PAYMENT_HEADER.Applied_Payment as [Applied Payment]  From TSPL_PAYMENT_HEADER " & _
          " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code " & _
          " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PAYMENT_HEADER.Vendor_Code "
        OvlRptPayE.Tag = clsCommon.ShowSelectForm("VPFAPay", qry, "Code", "", OvlRptPayE.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPayE.Tag) > 0 Then
            arr.Add(OvlRptPayE.Tag)
            FrmPaymentEntry.funReport("", "", arr, Nothing, Nothing)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' -----------------------------------------------------------------------------------------
    '' ---------------------------------- AR Invoice -------------------------------------------
    Private Sub OvlRptReceiptE_Click(sender As Object, e As EventArgs) Handles OvlRptReceiptE.Click
        Dim arr As New ArrayList()
        Dim qry As String = String.Empty
        qry = "select TSPL_BANK_MASTER.DESCRIPTION AS BANK, TSPL_RECEIPT_HEADER.Receipt_No as [ReceiptNo] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Date,103) as [Receipt Date] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Post_Date,103) as [Receipt Post Date] ,TSPL_RECEIPT_HEADER.Entry_Desc as [Entry Desc] ,TSPL_RECEIPT_HEADER.Bank_Code as [Bank Code] ,TSPL_RECEIPT_HEADER.Receipt_Type as [Receipt Type] ,TSPL_RECEIPT_HEADER.Cust_Code as [Cust Code] ,TSPL_RECEIPT_HEADER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name] ,TSPL_RECEIPT_HEADER.Reference as [Reference] ,TSPL_RECEIPT_HEADER.Narration as [Narration] ,TSPL_RECEIPT_HEADER.Payment_Code as [Payment Code] ,TSPL_RECEIPT_HEADER.Cheque_No as [Cheque No] ,TSPL_RECEIPT_HEADER.Cheque_Date as [Cheque Date] ,TSPL_RECEIPT_HEADER.Receipt_Amount as [Receipt Amount] ,TSPL_RECEIPT_HEADER.Cust_Account as [Cust Account] ,TSPL_RECEIPT_HEADER.Apply_By as [Apply By] ,TSPL_RECEIPT_HEADER.Apply_To as [Apply To] ,TSPL_RECEIPT_HEADER.Posted as [Posted] ,TSPL_RECEIPT_HEADER.Document_No as [Document No] ,TSPL_RECEIPT_HEADER.Payer as [Payer] ,TSPL_RECEIPT_HEADER.QuickEntryNo as [Quickentryno] ,TSPL_RECEIPT_HEADER.SecurityDeposit as [Securitydeposit] ,TSPL_RECEIPT_HEADER.Salesman_Code as [Salesman Code] ,TSPL_RECEIPT_HEADER.Salesman_Name as [Salesman Name] ,TSPL_RECEIPT_HEADER.Loadout_No as [Loadout No] ,TSPL_RECEIPT_HEADER.Cheque_From as [Cheque From] ,TSPL_RECEIPT_HEADER.CURRENCY_CODE as [Currency Code] ,TSPL_RECEIPT_HEADER.ConvRate as [Convrate] ,TSPL_RECEIPT_HEADER.ApplicableFrom as [Applicablefrom] ,TSPL_RECEIPT_HEADER.CFormRecd as [Cformrecd] ,TSPL_RECEIPT_HEADER.CForm_InvoiceNo as [Cform Invoiceno] ,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE as [Base Currency Code] ,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY as [Received Amount Base Currency] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as [Exchange Loss Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as [Exchange Gain Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] ,TSPL_RECEIPT_HEADER.ConvRateOld as [Convrateold] ,TSPL_RECEIPT_HEADER.PROJECT_ID as [Project Id] ,TSPL_RECEIPT_HEADER.IsParentCust as [Isparentcust] ,TSPL_RECEIPT_HEADER.CHECK_PRINT as [Check Print] ,TSPL_RECEIPT_HEADER.CHECK_CODE as [Check Code] ,TSPL_RECEIPT_HEADER.AUTO_GEN_BT_ENTRY as [Auto Gen Bt Entry] ,TSPL_RECEIPT_HEADER.TO_BANK_CODE as [To Bank Code] ,TSPL_RECEIPT_HEADER.Transfer_No as [Transfer No] ,TSPL_RECEIPT_HEADER.From_Branch as [From Branch] ,TSPL_RECEIPT_HEADER.memorandum_amt as [Memorandum Amt] ,TSPL_RECEIPT_HEADER.Applied_Receipt as [Applied Receipt],TSPL_RECEIPT_HEADER.SaleOrderNo  From TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code " & _
      " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code  "
        OvlRptReceiptE.Tag = clsCommon.ShowSelectForm("VPFRcpE", qry, "ReceiptNo", "", OvlRptReceiptE.Tag, " Convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)", True)
        If clsCommon.myLen(OvlRptReceiptE.Tag) > 0 Then
            arr.Add(OvlRptReceiptE.Tag)
            Frmreceiptvoucher2.PrintData(Nothing, Nothing, Nothing, arr, Nothing, Nothing, Nothing, Nothing)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptReceiptAdjE_Click(sender As Object, e As EventArgs) Handles OvlRptReceiptAdjE.Click
        Dim qry As String = String.Empty
        qry = "select adjustment_no as [AdjustmentNo],Adjustment_date as [Date],Customer_No as [Customer No],tspl_receipt_adjustment_header.CUSTOMER_Name as [Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],(select case when is_post='N' OR Is_Post IS null then 'UnPosted' when is_post='Y' then 'Posted' end ) as [Status] from tspl_receipt_adjustment_header  " & _
      " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = tspl_receipt_adjustment_header.Customer_No  "
        OvlRptReceiptE.Tag = clsCommon.ShowSelectForm("VPFARAdj", qry, "AdjustmentNo", "", OvlRptReceiptE.Tag, " AdjustmentNo", True)
        If clsCommon.myLen(OvlRptReceiptE.Tag) > 0 Then
            frmAdj.PrintData(OvlRptReceiptE.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptARInv_Click(sender As Object, e As EventArgs) Handles OvlRptARInv.Click
        Dim qry As String = String.Empty
        qry = " select TSPL_Customer_Invoice_Head.Document_No as DocumentNo,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_Customer_Invoice_Head.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],case when TSPL_Customer_Invoice_Head.[status]=1 then 'Posted' else 'Pending'end as [Status],Account_Set as AccountSet,Against_Sale_No as [Against Sale Invoice],AgainstScrap as [Against Scrap No],Against_Sale_Return_No as [Against Sale Return],ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') AS [Against VCGL] from TSPL_Customer_Invoice_Head left outer join TSPL_Customer_Invoice_Detail on TSPL_Customer_Invoice_Detail.Document_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_Customer_Invoice_Detail.SNo=1  " & _
      " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Customer_Invoice_Head.Customer_Code  "
        OvlRptARInv.Tag = clsCommon.ShowSelectForm("VPFARInv", qry, "DocumentNo", "", OvlRptARInv.Tag, " DocumentNo", True)
        If clsCommon.myLen(OvlRptARInv.Tag) > 0 Then
            FrmARInvoiceEntry.funPrint(OvlRptARInv.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptQuickRE_Click(sender As Object, e As EventArgs) Handles OvlRptQuickRE.Click
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

    '' ------------------------------------------------------------------------------------------
    '' --------------------------------- General Ledger -----------------------------------------
    Private Sub OvlRptJE_Click(sender As Object, e As EventArgs) Handles OvlRptJE.Click
        Dim Qry As String = String.Empty
        Dim WhrCls As String = String.Empty

        Qry = " SELECT  distinct   TSPL_JOURNAL_MASTER.Voucher_No AS VoucherNo, TSPL_JOURNAL_MASTER.Voucher_Desc AS Description, " & _
                             " TSPL_JOURNAL_MASTER.Source_Code AS [Source Type], convert(varchar(11),TSPL_JOURNAL_MASTER.Voucher_Date,103) AS [Voucher Date],  " & _
                             " TSPL_JOURNAL_MASTER.Source_Doc_No AS [Document No], convert(varchar(11),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) AS [Document Date], " & _
                             " case when Source_Code= 'AR-IN' then (select Against_Sale_No from TSPL_Customer_Invoice_Head where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
                             " when Source_Code= 'AR-CR' then (select Against_Sale_Return_No from TSPL_Customer_Invoice_Head where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
                             " when Source_Code= 'AP-IN' then (select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
                             " when Source_Code= 'AP-CN' then (select Against_PurchaseReturn_No from TSPL_VENDOR_INVOICE_HEAD where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No)  else '' end as RefDocNo, " & _
                             " (SELECT     CASE WHEN Authorized = 'A' THEN 'Posted' ELSE 'Open' END AS Expr1) AS Status,TSPL_JOURNAL_MASTER.Remarks " & _
                             "  FROM TSPL_JOURNAL_MASTER LEFT OUTER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No"
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = ""
        Else

            WhrCls = " TSPL_JOURNAL_DETAILS.Account_code in (" + objCommonVar.strCurrUserGLAccount + ")"
        End If
        OvlRptJE.Tag = clsCommon.ShowSelectForm("VPFRJE", Qry, "VoucherNo", WhrCls, OvlRptJE.Tag, "VoucherNo", True)
        If clsCommon.myLen(OvlRptJE.Tag) > 0 Then
            Dim frm As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.PrintData(OvlRptJE.Tag, "VPFJE")
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptVCGL_Click(sender As Object, e As EventArgs) Handles OvlRptVCGL.Click
        Dim Qry As String = String.Empty
        Dim WhrCls As String = String.Empty
        Dim LocSeg As String = String.Empty

        Qry = "select Document_No,Document_Date as Date,VC_Code as [Vendor/Customer],VC_Name as [Vendor/Customer Name],CASE WHEN ISNULL(Document_Type,'')='C' THEN ISNULL(TSPL_CUSTOMER_MASTER.alies_name,'') WHEN ISNULL(Document_Type,'')='V' THEN ISNULL(TSPL_VENDOR_MASTER.alies_name,'') END [Alies Name],Remarks,case when TSPL_VCGL_Head.status=1 then 'Posted' else 'Pending' end Status from TSPL_VCGL_Head "
        Qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VCGL_HEAD.VC_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_HEAD.VC_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
        If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            WhrCls += " Location_Segment in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        OvlRptVCGL.Tag = clsCommon.ShowSelectForm("VPFVCGL", Qry, "Document_No", WhrCls, OvlRptVCGL.Tag, "", True)
        If clsCommon.myLen(OvlRptVCGL.Tag) > 0 Then
            LocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Segment  From tspl_vcgl_head Where Document_No ='" & OvlRptVCGL.Tag & "'"))
            Dim frmVCGLEntry1 As New frmVCGLEntry
            frmVCGLEntry1.printvoucher(OvlRptVCGL.Tag, LocSeg)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' -----------------------------------------------------------------------------------------
    '' -------------------------- Common Services ----------------------------------------------
    Private Sub OvlRptBRE_Click(sender As Object, e As EventArgs) Handles OvlRptBRE.Click
        Dim Qry As String = String.Empty
        Dim ARCheckNo As String = String.Empty
        Dim APCheckNo As String = String.Empty
        Dim ARDocNo As String = String.Empty
        Dim DocNo As String = String.Empty

        Qry = "select TSPL_BANK_REVERSE.Reverse_Code as [Code], CONVERT(date,reversal_date,105) as [Date], Case When TSPL_BANK_REVERSE.Source_Type='AR' Then 'Account Receivable' else 'Account Payable' END as [Type], TSPL_BANK_REVERSE.Bank_Code as [Bank Code], Case When TSPL_BANK_REVERSE.Post='P' Then 'Posted' Else 'Unposted' End as [Status] from TSPL_BANK_REVERSE " & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code "

        OvlRptBRE.Tag = clsCommon.ShowSelectForm("VPFVCGL", Qry, "Code", "", OvlRptBRE.Tag, "Code", True)
        If clsCommon.myLen(OvlRptBRE.Tag) > 0 Then
            DocNo = clsDBFuncationality.getSingleValue("Select ISNULL(Document_No,'') AS Document_No From tspl_bank_reverse Where Reverse_Code ='" & OvlRptBRE.Tag & "'")
            Dim frm As New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.printData(OvlRptBRE.Tag, DocNo, DocNo, DocNo, DocNo, "P")
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptContra_Click(sender As Object, e As EventArgs) Handles OvlRptContra.Click
        Dim Qry As String = String.Empty

        Qry = "select [Transfer_No] as [Code],Transfer_Date AS [Transfer Date], TSPL_BANK_TRANSFER.Description ,Payment_Mode AS [Payment Mode], Transaction_Type AS [TransactionType],ISNULL(Against_Withdrawal_No,'') AS Against_Withdrawal_No, From_Bank_Code  AS [From Bank Code],From_Bank_Name AS [From Bank Name] ,From_Bank_Acc_No AS [From Bank Acc No],Transfer_Amount AS [Transfer Amount],To_Bank_Code AS [To Bank Code],To_Bank_Name AS [To Bank Name],To_Bank_Acc_No AS  [To Bank Acc No],Deposit_Amount AS [Deposit Amount],Post,TSPL_BANK_TRANSFER.CHECK_PRINT as [Check Print] ,TSPL_BANK_TRANSFER.CHECK_CODE as [Check Code],ISNULL(Remitt_To,'') AS [Remitt To] from TSPL_BANK_TRANSFER" & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code"

        OvlRptContra.Tag = clsCommon.ShowSelectForm("VPFBankT", Qry, "Code", "", OvlRptContra.Tag, "Code", True)
        If clsCommon.myLen(OvlRptContra.Tag) > 0 Then
            Dim frm As New FrmBankTransfer(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.FunPrintData(OvlRptContra.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' -----------------------------------------------------------------------------------------
    '' ------------------------------------ Bulk Sale ------------------------------------------
    Private Sub OvlRptBSDis_Click(sender As Object, e As EventArgs) Handles OvlRptBSDis.Click
        Dim Qry As String = String.Empty
        Dim obj As New clsMCCCodes()
        Dim arrLoc As String = Nothing
        Dim Alocation As String = Nothing

        Qry = " Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
        Try

            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try

        OvlRptBSDis.Tag = clsCommon.ShowSelectForm("VPFRptBS", Qry, "Code", " TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") ", OvlRptBSDis.Tag, "Code", True)
        If clsCommon.myLen(OvlRptBSDis.Tag) > 0 Then
            Dim FrmDispatchBulkSale1 As New FrmDispatchBulkSale
            FrmDispatchBulkSale1.funPrint(OvlRptBSDis.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptBSInv_Click(sender As Object, e As EventArgs) Handles OvlRptBSInv.Click
        Dim Qry As String = String.Empty
        Dim AgainstInv As String = String.Empty

        Qry = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Invoice Date],TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,isnull(TSPL_Dispatch_BulkSale.QC_Code,'') as [FAT/SNF check No] ,Isnull(TSPL_Quality_Check_BulkSale.LoadingTanker_No,'') as [Loading No] ,Isnull(TSPL_Quality_Check_BulkSale.Weighment_No,'') as [Weighment No] ,Isnull(TSPL_Quality_Check_BulkSale.GateEntry_Document_No,'') as [Gate Entry No] ,TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],case when TSPL_INVOICE_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status,TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst as [Invoice Against] from TSPL_INVOICE_MASTER_BULKSALE left outer Join TSPL_CUSTOMER_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code" & _
        " left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " & _
        " Left Outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code " & _
        " Left outer join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No =TSPL_Dispatch_BulkSale.QC_Code "

        OvlRptBSInv.Tag = clsCommon.ShowSelectForm("VPFBSInv", Qry, "Code", "", OvlRptBSInv.Tag, "Code", True)
        If clsCommon.myLen(OvlRptBSInv.Tag) > 0 Then

            AgainstInv = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select InvoiceAgainst From TSPL_INVOICE_MASTER_BULKSAlE Where Document_No ='" & OvlRptBSInv.Tag & "'"))
            Dim FrmInvoiceBulkSale1 As New FrmInvoiceBulkSale
            FrmInvoiceBulkSale1.funPrint(OvlRptBSInv.Tag, AgainstInv)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptBSRet_Click(sender As Object, e As EventArgs) Handles OvlRptBSRet.Click
        Dim Qry As String = String.Empty
        Dim AgainstText As String = String.Empty

        Qry = " Select TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) as [Sale Return Date],TSPL_SALE_RETURN_MASTER_BULKSALE.GateEntryNo as [Gate Entry No],TSPL_SALE_RETURN_MASTER_BULKSALE.Tanker_No as [Tanker No],TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo as [Invoice No],TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No as [Silo No],SubLocation.Location_Desc as [Silo Name],case when TSPL_SALE_RETURN_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SALE_RETURN_MASTER_BULKSALE left outer Join TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocation on TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No =SubLocation.Location_Code "

        OvlRptBSRet.Tag = clsCommon.ShowSelectForm("VPFBSRet", Qry, "Code", "", OvlRptBSRet.Tag, "Code", True)
        If clsCommon.myLen(OvlRptBSRet.Tag) > 0 Then
            AgainstText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Against From TSPL_SALE_RETURN_MASTER_BULKSALE WHERE Document_No ='" & OvlRptBSRet.Tag & "'"))
            Dim AgainstInv As Boolean = IIf(AgainstText = "Bulk Invoice", True, False)
            Dim FrmBulkSaleReturn1 As New FrmBulkSaleReturn
            FrmBulkSaleReturn1.PrintData(OvlRptBSRet.Tag, AgainstInv)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' ----------------------------------------------------------------------------------------
    '' ---------------------------------------- Fresh Sale  -----------------------------------
    Private Sub OvlRptFSDis_Click(sender As Object, e As EventArgs) Handles OvlRptFSDis.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty

        Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SHIPMENT_HEAD.Vehicle_Code as Vehicle,TSPL_SD_SHIPMENT_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS'"
        Else
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' "
        End If

        OvlRptFSDis.Tag = clsCommon.ShowSelectForm("VPFFSDis", Qry, "Code", whrClas, OvlRptFSDis.Tag, "Code", True)
        If clsCommon.myLen(OvlRptFSDis.Tag) > 0 Then
            frmDispatchNoteFreshSale.funPrint(OvlRptFSDis.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptFSDisM_Click(sender As Object, e As EventArgs) Handles OvlRptFSDisM.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty
        If CreateFreshInvoiceOnDispatchSave = 0 Then
            Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,TSPL_SD_SHIPMENT_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "
        Else
            Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  "
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS'"
        Else
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Trans_Type='FS' "
        End If

        OvlRptFSDisM.Tag = clsCommon.ShowSelectForm("VPFFSDis", Qry, "Code", whrClas, OvlRptFSDisM.Tag, "Code", True)
        If clsCommon.myLen(OvlRptFSDisM.Tag) > 0 Then
            Dim frmPrintFreshInvoice As New FrmPrintFreshInvoice
            Dim Qry1 As String = frmPrintFreshInvoice.LoadPrintQuery(OvlRptFSDisM.Tag)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry1)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Fresh Invoice Statement", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                frmCRV = Nothing
            End If
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptFSInv_Click(sender As Object, e As EventArgs) Handles OvlRptFSInv.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty
        Dim strwherecls As String = String.Empty
        strwherecls = Xtra.CustomerPermission()

        Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code as Vehicle,TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Against_Shipment_No as [Shipment No] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R','N') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R','N') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS'"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") and Invoice_Type in ('T','R','N') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS'"
        Else
            whrClas = "  Invoice_Type in ('T','R','N') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS'"
        End If

        OvlRptFSInv.Tag = clsCommon.ShowSelectForm("VPFFSInv", Qry, "Code", whrClas, OvlRptFSInv.Tag, "Code", True)
        If clsCommon.myLen(OvlRptFSInv.Tag) > 0 Then
            frmInvoiceFreshSale.funPrint(OvlRptFSInv.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptFSRet_Click(sender As Object, e As EventArgs) Handles OvlRptFSRet.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty
        Dim strwherecls As String = String.Empty
        strwherecls = Xtra.CustomerPermission()

        Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Total_Amt as Amount,case when TSPL_SD_SALE_RETURN_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and  and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + strwherecls + ")  and TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS'"
        Else
            whrClas = " TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS'"
        End If

        OvlRptFSRet.Tag = clsCommon.ShowSelectForm("VPFFSRet", Qry, "Code", whrClas, OvlRptFSRet.Tag, "Code", True)
        If clsCommon.myLen(OvlRptFSRet.Tag) > 0 Then
            frmSaleReturnFreshSale.funPrint(OvlRptFSRet.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptFSGate_Click(sender As Object, e As EventArgs) Handles OvlRptFSGate.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty

        Qry = " SELECT  GPCode,convert(varchar(10),GPDate,103)  as GPDate,Vehicle_Id,Vehicle_Number FROM  TSPL_GATEPASS_MASTER_FRESHSALE"

        OvlRptFSGate.Tag = clsCommon.ShowSelectForm("VPFFGP", Qry, "GPCode", "", OvlRptFSGate.Tag, "GPCode", True)
        If clsCommon.myLen(OvlRptFSGate.Tag) > 0 Then
            FrmGatePassFS.funPrint(OvlRptFSGate.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' ----------------------------------------------------------------------------------------
    '' ------------------------------------- Product Sale -------------------------------------
    Private Sub OvlRptPSDel_Click(sender As Object, e As EventArgs) Handles OvlRptPSDel.Click
        Dim strwherecls As String = String.Empty
        Dim qry As String = String.Empty
        Dim whrClas As String = String.Empty

        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------

        qry = "select TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as Code,convert(varchar(12),TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103)  as Date, " & _
           "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Against_Sales_Order as [Sale Order], " & _
           "TSPL_SD_SALES_ORDER_HEAD.Against_Booking_No as [Booking No],TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Amt as Amount, " & _
           "case when TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.status='0' then 'Pending' else 'Approved' end as [Status], " & _
           "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location as [Location],(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location ) as [Location Name] " & _
           "from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code " & _
           "left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Against_Sales_Order=TSPL_SD_SALES_ORDER_HEAD.Document_Code"


        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")  "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code in (" + strwherecls + ")  "
        Else
            'whrClas = " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Trans_Type='PS'"
        End If

        OvlRptPSDel.Tag = clsCommon.ShowSelectForm("VPFPSDel", qry, "Code", whrClas, OvlRptPSDel.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPSDel.Tag) > 0 Then
            frmDeliveryOrderProductSale.funPrint(OvlRptPSDel.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptPSDis_Click(sender As Object, e As EventArgs) Handles OvlRptPSDis.Click
        Dim Qry As String = String.Empty
        Dim strwherecls As String = String.Empty
        Dim whrClas As String = String.Empty

        strwherecls = Xtra.CustomerPermission()

        Qry = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, " & _
            "CONVERT(varchar(10), TSPL_SD_SHIPMENT_HEAD.Document_Date,103)+' '+ CONVERT(varchar(5), TSPL_SD_SHIPMENT_HEAD.Document_Date,114) as Date, " & _
            "TSPL_SD_SHIPMENT_HEAD.Customer_Code as [Customer Code], Customer_Name as Customer,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as [Location Code], " & _
            "Location_Desc as [Location Name],TSPL_SD_SHIPMENT_HEAD.Comments,TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount, " & _
            "case when TSPL_SD_SHIPMENT_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Direct_Dispatch as [Direct Dispatch] from " & _
            "TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join  " & _
            "TSPL_LOCATION_MASTER on TSPL_SD_SHIPMENT_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No"

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and  TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and  TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS'"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") and  TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' "
        Else
            whrClas = " TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS'"
        End If

        OvlRptPSDis.Tag = clsCommon.ShowSelectForm("VPFPSDis", Qry, "Code", whrClas, OvlRptPSDis.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPSDis.Tag) > 0 Then
            frmShipmentProductSale.funPrint(OvlRptPSDis.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptPSInv_Click(sender As Object, e As EventArgs) Handles OvlRptPSInv.Click
        Dim Qry As String = String.Empty
        Dim strwherecls As String = String.Empty
        Dim whrClas As String = String.Empty

        strwherecls = Xtra.CustomerPermission()

        Qry =  "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Amt as Amount,case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Against_Shipment_No as [Shipment No] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R','E') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and Invoice_Type in ('T','R','E') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") and Invoice_Type in ('T','R','E') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' "
        Else
            whrClas = "  Invoice_Type in ('T','R','E') and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'"
        End If

        OvlRptPSInv.Tag = clsCommon.ShowSelectForm("VPFPSInv", Qry, "Code", whrClas, OvlRptPSInv.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPSInv.Tag) > 0 Then
            Dim frmSaleInvoiceProductSale As New frmSaleInvoiceProductSale
            frmSaleInvoiceProductSale.funPrint(OvlRptPSInv.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptPSRet_Click(sender As Object, e As EventArgs) Handles OvlRptPSRet.Click
        Dim Qry As String = String.Empty
        Dim strwherecls As String = String.Empty
        Dim whrClas As String = String.Empty

        strwherecls = Xtra.CustomerPermission()

        Qry = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Total_Amt as Amount,case when TSPL_SD_SALE_RETURN_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status] from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + strwherecls + ") and TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' "
        Else
            whrClas = " TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS'"
        End If

        OvlRptPSRet.Tag = clsCommon.ShowSelectForm("VPFPSInv", Qry, "Code", whrClas, OvlRptPSRet.Tag, "Code", True)
        If clsCommon.myLen(OvlRptPSRet.Tag) > 0 Then
            frmSaleReturnProductSale.funPrint(OvlRptPSRet.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' ----------------------------------------------------------------------------------------
    '' --------------------------------- CSA Sale ---------------------------------------------
    Private Sub OvlRptCSADO_Click(sender As Object, e As EventArgs) Handles OvlRptCSADO.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty

        Qry = "select count(*) from TSPL_CSA_DO_HEAD "

        OvlRptCSADO.Tag = clsCSADeliveryOrder.GetFinder(" ", OvlRptCSADO.Tag, True)
        If clsCommon.myLen(OvlRptCSADO.Tag) > 0 Then
            Dim dt As DataTable = New DataTable()
            dt = FrmCSADeliveryOrder.funPrint(OvlRptCSADO.Tag)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptCSADeliverySale", "CSA Delivery Sale")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found.")
            End If
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptCSATran_Click(sender As Object, e As EventArgs) Handles OvlRptCSATran.Click
        Dim Qry As String = String.Empty
        Dim strwherecls As String = String.Empty
        Dim whrClas As String = String.Empty

        strwherecls = Xtra.CustomerPermission()

        Qry = "select DOC_CODE as Code,convert(varchar(10),Transfer_Date,103)  as Date,TSPL_CSA_TRANSFER_HEAD.Cust_Code as [Customer Code], " & _
        " Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CSA_TRANSFER_HEAD.Document_Amount as Amount,case when TSPL_CSA_TRANSFER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status], " & _
        " TSPL_CSA_TRANSFER_HEAD.From_Location_Code as [From Location Code],TSPL_CSA_TRANSFER_HEAD.To_Location_Code as [To Location Code], " & _
        " Loc1.Location_Desc as [From Location Name],Loc2.Location_Desc as [To Location Name] from TSPL_CSA_TRANSFER_HEAD  " & _
        " left join TSPL_LOCATION_MASTER as Loc1 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc1.Location_Code" & _
        " left join TSPL_LOCATION_MASTER as Loc2 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc2.Location_Code " & _
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code "

        If clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ")"
        End If

        OvlRptCSATran.Tag = clsCommon.ShowSelectForm("VPFCSATran", Qry, "Code", whrClas, OvlRptCSATran.Tag, "Code", True)
        If clsCommon.myLen(OvlRptCSATran.Tag) > 0 Then
            frmCSATransfer.funPrint(OvlRptCSATran.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptSaleInv_Click(sender As Object, e As EventArgs) Handles OvlRptSaleInv.Click
        Dim Qry As String = String.Empty
        Dim strwherecls As String = String.Empty
        Dim whrClas As String = String.Empty

        Qry = "select count(*) from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA'"

        OvlRptSaleInv.Tag = clsCSASaleInvoice.GetFinder("", OvlRptSaleInv.Tag, True, Nothing)
        If clsCommon.myLen(OvlRptSaleInv.Tag) > 0 Then
            Dim dt As DataTable = New DataTable()
            dt = FrmCSASaleInvoice.funPrint(OvlRptSaleInv.Tag)
       
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptSaleInvoiceCSA", "Sale Invoice CSA")
                frmCRV = Nothing
            End If
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' ----------------------------------------------------------------------------------------
    '' ------------------------------- MM -----------------------------------------------------
    Private Sub OvlRptMMTran_Click(sender As Object, e As EventArgs) Handles OvlRptMMTran.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = String.Empty

        Qry = "select Document_No as [TransferNO],convert (varchar(10), Document_Date,103) as Date,DOC_Total_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status], From_Location+' - '+FromLocation.Location_Desc as [FromLocation], To_Location+' - '+ToLocation.Location_Desc as [ToLocation], Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then 'LOAD OUT' When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'LOAD IN' Else 'REJECTED' End as [Transfer Type], TSPL_TRANSFER_ORDER_HEAD.TransferOutNo from TSPL_TRANSFER_ORDER_HEAD LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLocation ON FromLocation.Location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLocation on ToLocation.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location"


        OvlRptMMTran.Tag = clsCommon.ShowSelectForm("VPFMMTran", Qry, "TransferNO", whrClas, OvlRptMMTran.Tag, "TransferNO", True)
        If clsCommon.myLen(OvlRptMMTran.Tag) > 0 Then
            FrmTransferKDIL.PrintData(OvlRptMMTran.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If

    End Sub

    Private Sub OvlRptMMPE_Click(sender As Object, e As EventArgs) Handles OvlRptMMPE.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = " 1=1"

        Qry = "SELECT Adjustment_No AS [AdjustmentNumber], Adjustment_Date as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location] FROM  TSPL_ADJUSTMENT_HEADER  "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        whrClas += " AND ItemType IN ('FM', 'FT')"

        OvlRptMMPE.Tag = clsCommon.ShowSelectForm("VPFMMPE", Qry, "AdjustmentNumber", whrClas, OvlRptMMPE.Tag, "AdjustmentNumber", True)
        If clsCommon.myLen(OvlRptMMPE.Tag) > 0 Then
            frmAdjustmentProduction.PrintData(OvlRptMMPE.Tag, False, False)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptMMStA_Click(sender As Object, e As EventArgs) Handles OvlRptMMStA.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = " 1=1"

        Qry = "SELECT Adjustment_No AS [AdjustmentNumber], Adjustment_Date as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location] FROM  TSPL_ADJUSTMENT_HEADER  "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        OvlRptMMStA.Tag = clsCommon.ShowSelectForm("VPFMMPE", Qry, "AdjustmentNumber", whrClas, OvlRptMMStA.Tag, "AdjustmentNumber", True)
        If clsCommon.myLen(OvlRptMMStA.Tag) > 0 Then
            frmAdjustmentStore.PrintData(OvlRptMMStA.Tag, False, False)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptMMWB_Click(sender As Object, e As EventArgs) Handles OvlRptMMWB.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = " 1=1"

        Qry = "SELECT Document_No AS [Document], Document_Date as [Date], Loc_Code as [Location], Case When Is_Post=1 Then 'Posted' Else 'Pending' End as [Status] FROM  TSPL_WH_BREAKAGE_HEAD  "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        OvlRptMMWB.Tag = clsCommon.ShowSelectForm("VPFMMWB", Qry, "Document", whrClas, OvlRptMMWB.Tag, "Document", True)
        If clsCommon.myLen(OvlRptMMWB.Tag) > 0 Then
            FrmWarehouseBreakage.PrintData(OvlRptMMWB.Tag, False, False)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptMMExIE_Click(sender As Object, e As EventArgs) Handles OvlRptMMExIE.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = " 1=1"

        Qry = " SELECT Document_No AS [DocumentNo], Document_Date as [Date], case when Posted='Y' then 'Yes' else 'No' end as Posted,Customer_NAME as [Customer],Loc_Code as [Location] FROM  TSPL_EXPIRY_HEADER  "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        OvlRptMMExIE.Tag = clsCommon.ShowSelectForm("VPFMMExIE", Qry, "DocumentNo", whrClas, OvlRptMMExIE.Tag, "DocumentNo", True)
        If clsCommon.myLen(OvlRptMMExIE.Tag) > 0 Then
            Dim FrmExpiryDateEntry As New FrmExpiryDateEntry()
            FrmExpiryDateEntry.PrintData(OvlRptMMExIE.Tag, False)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub OvlRptMMAsDs_Click(sender As Object, e As EventArgs) Handles OvlRptMMAsDs.Click
        Dim Qry As String = String.Empty
        Dim whrClas As String = " 1=1"

        Qry = " select CODE as Code,Description as Name,ASSEMBLY_DATE AS [Date],TRANSACTION_TYPE as [Type],Main_Item_Code as [Main Item Code],BOM_CODE as [BOM Code],LOCATION_CODE as [Location Code] from  TSPL_PJC_ASSEMBLIES  "

        OvlRptMMAsDs.Tag = clsCommon.ShowSelectForm("VPFMMAsDs", Qry, "Code", whrClas, OvlRptMMAsDs.Tag, "Code", True)
        If clsCommon.myLen(OvlRptMMAsDs.Tag) > 0 Then
            Dim frmAssemblies1 As New frmAssemblies
            frmAssemblies1.funPrint(OvlRptMMAsDs.Tag)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
    '' ----------------------------------------------------------------------------------------
#End Region
    Sub ReportLabelNaming()
        LblRptMMTran.Text = clsUserMgtCode.Transfer
        LblRptMMStA.Text = clsUserMgtCode.mbtnStoreAdjustment
        LblRptMMPE.Text = clsUserMgtCode.mbtnProductionEntry
        LblRptMMExIE.Text = clsUserMgtCode.FrmExpiryDateEntry
        LblRptMMWB.Text = clsUserMgtCode.frmWarehouseBreakage
        LblRptMMAsDs.Text = clsUserMgtCode.frmAssemblies
        LblRptCSADO.Text = clsUserMgtCode.frmCSADeliveryOrder
        LblRptCSATran.Text = clsUserMgtCode.frmCSATransfer
        LblRptCSASaleInv.Text = clsUserMgtCode.frmCSASaleInvoice
        LblRptTankerDispatch.Text = clsUserMgtCode.frmMCCDispatch
        LblRptPSDel.Text = clsUserMgtCode.frmDeliveryPrderProductSale
        LblRptPSDis.Text = clsUserMgtCode.frmShipmentProductSale
        LblRptPSInv.Text = clsUserMgtCode.frmSaleInvoiceProductSale
        LblRptPSRet.Text = clsUserMgtCode.frmSaleReturnProductSale
    End Sub
    Private Sub OvlRptWeigh_MouseEnter(sender As Object, e As EventArgs) Handles OvlRptWeigh.MouseEnter, OvlRptQC.MouseEnter, OvlRptBulkSRN.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.SkyBlue
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub OvlRptWeigh_MouseLeave(sender As Object, e As EventArgs) Handles OvlRptWeigh.MouseLeave, OvlRptQC.MouseLeave, OvlRptBulkSRN.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub OvlRptWeigh_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlRptWeigh.MouseUp, OvlRptQC.MouseUp, OvlRptBulkSRN.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub FrmBulkProcurementCycle_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreateFreshInvoiceOnDispatchSave = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, Nothing))
        Dim RptName As String = String.Empty
        Dim MasterName As String = String.Empty
        Dim TransName As String = String.Empty

        PnlRptAP.Size = New Size(337, 191)
        PnlRptMCC.Size = New Size(337, 191)
        PnlRptBulkP.Size = New Size(337, 191)
        PnlRptPurchase.Size = New Size(337, 191)
        PnlRptAR.Size = New Size(337, 191)
        PnlRptCom.Size = New Size(337, 191)
        PnlRptGL.Size = New Size(337, 191)
        PnlRptBS.Size = New Size(337, 191)
        PnlRptFS.Size = New Size(337, 191)
        PnlRptPS.Size = New Size(337, 191)
        PnlRptCSA.Size = New Size(337, 191)
        PnlRptMM.Size = New Size(337, 191)

        PnlRptAP.Location = New Point(15, 409)
        PnlRptMCC.Location = New Point(15, 409)
        PnlRptBulkP.Location = New Point(15, 409)
        PnlRptPurchase.Location = New Point(15, 409)
        PnlRptAR.Location = New Point(15, 409)
        PnlRptCom.Location = New Point(15, 409)
        PnlRptGL.Location = New Point(15, 409)
        PnlRptBS.Location = New Point(15, 409)
        PnlRptFS.Location = New Point(15, 409)
        PnlRptPS.Location = New Point(15, 409)
        PnlRptCSA.Location = New Point(15, 409)
        PnlRptMM.Location = New Point(15, 409)

        SetUserMgmtNew()
        If clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmGLCycle) = CompairStringResult.Equal Then
            MasterName = "SMGLSetup"
            TransName = "SMGLTrans"
            RptName = "SMGLReport"
            LblModuleName.Text = "General Ledger"
            'Program_Code_ProgramMaster = "MGenLedger"
            PnlRptCom.Visible = False
            PnlRptGL.Visible = True
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptAR.Visible = False
            PnlRptBS.Visible = False
            PnlRptFS.Visible = False
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmCommonCycle) = CompairStringResult.Equal Then
            MasterName = "SMCSSetup"
            TransName = "SMCSTrans"
            RptName = "SMCSReport"
            LblModuleName.Text = "Common Services"
            'Program_Code_ProgramMaster = "MCommSer"
            PnlRptCom.Visible = True
            PnlRptGL.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptAR.Visible = False
            PnlRptBS.Visible = False
            PnlRptFS.Visible = False
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmAPCycle) = CompairStringResult.Equal Then
            MasterName = "SMPaySetup"
            TransName = "SMPayTrans"
            RptName = "SMPayReport"
            LblModuleName.Text = "Vendor Management"
            PnlRptAP.Visible = True
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptAR.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptBS.Visible = False
            PnlRptFS.Visible = False
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
            'Program_Code_ProgramMaster = "MPayable"
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmARCycle) = CompairStringResult.Equal Then
            MasterName = "SMRecSetup"
            TransName = "SMRecTrans"
            RptName = "SMRecReport"
            LblModuleName.Text = "Customer Management"
            'Program_Code_ProgramMaster = "MReceivable"
            PnlRptAR.Visible = True
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptBS.Visible = False
            PnlRptFS.Visible = False
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmBulkSaleCycle) = CompairStringResult.Equal Then
            MasterName = "SMBulkSale"
            TransName = "SMBULKSTRAN"
            RptName = "SMBULKSRPT"
            LblModuleName.Text = "Bulk Sale"
            'Program_Code_ProgramMaster = "MBulkSale"
            PnlRptBS.Visible = True
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmFreshSaleCycle) = CompairStringResult.Equal Then
            MasterName = "SMFreshSale"
            TransName = "SMFRESHSTRAN"
            RptName = "SMFRESHSRPT"
            LblModuleName.Text = "Fresh Sale"
            'Program_Code_ProgramMaster = "MFreshSale"
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = True
            PnlRptPS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmProductSaleCycle) = CompairStringResult.Equal Then
            MasterName = "SMProductSle"
            TransName = "SMPRODSTRAN"
            RptName = "SMPRODSRPT"
            LblModuleName.Text = "Product Sale"
            PnlRptPS.Visible = True
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
            PnlRptCSA.Visible = False
            PnlRptMM.Visible = False
            ' Program_Code_ProgramMaster = "MProductSale"
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmCSASaleCycle) = CompairStringResult.Equal Then
            MasterName = "SMCSASale"
            TransName = "SMCSATRAN"
            RptName = "SMCSARPT"
            LblModuleName.Text = "CSA Sale"
            PnlRptCSA.Visible = True
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptMM.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
            'Program_Code_ProgramMaster = "MCSASale"
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmMMCycle) = CompairStringResult.Equal Then
            MasterName = "SMMatSetup"
            TransName = "SMMatTrans"
            RptName = "SMMatReport"
            LblModuleName.Text = "Material Management"
            PnlRptMM.Visible = True
            PnlRptCSA.Visible = False
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
            'Program_Code_ProgramMaster = "MMaterial"
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmMCCProcurementCycle) = CompairStringResult.Equal Then
            MasterName = "SMMPROCSetup"
            TransName = "SMCPROCTRAN"
            RptName = "SMMPROCRPT"
            LblModuleName.Text = "MCC Procurement"
            'Program_Code_ProgramMaster = "MMMProc"
            PnlRptMCC.Visible = True
            PnlRptMM.Visible = False
            PnlRptCSA.Visible = False
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmBulkProcurementCycle) = CompairStringResult.Equal Then
            MasterName = "SMBPROCSetup"
            TransName = "SMMPROCTRANS"
            RptName = "SMBPROCRPT"
            LblModuleName.Text = "Bulk Procurement"
            'Program_Code_ProgramMaster = "MMBProc"
            PnlRptBulkP.Visible = True
            PnlRptMCC.Visible = False
            PnlRptMM.Visible = False
            PnlRptCSA.Visible = False
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmPurchaseCycle) = CompairStringResult.Equal Then
            MasterName = "SMPurSetup"
            TransName = "SMPurTrans"
            RptName = "SMPurReport"
            LblModuleName.Text = "Purchase Order"
            ' Program_Code_ProgramMaster = "MPurchase"
            PnlRptPurchase.Visible = True
            PnlRptBulkP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptMM.Visible = False
            PnlRptCSA.Visible = False
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
        ElseIf clsCommon.CompairString(ScreenCode, clsUserMgtCode.FrmDProductionCycle) = CompairStringResult.Equal Then
            MasterName = "SMPDSetupD"
            TransName = "SMPDTransD"
            RptName = "SMPDReportD"
            LblModuleName.Text = "Production"

            Label1.Visible = False
            OvlRpt.Visible = False
            LineShape6.Visible = False
            LineShape25.Visible = False
            PnlRptPurchase.Visible = False
            PnlRptBulkP.Visible = False
            PnlRptMCC.Visible = False
            PnlRptMM.Visible = False
            PnlRptCSA.Visible = False
            PnlRptPS.Visible = False
            PnlRptBS.Visible = False
            PnlRptAR.Visible = False
            PnlRptAP.Visible = False
            PnlRptCom.Visible = False
            PnlRptGL.Visible = False
            PnlRptFS.Visible = False
            '  Program_Code_ProgramMaster = "MProdDairy"
        End If
        DBBindRpt(RptName)
        DBBindMasters(MasterName)
        DynamicTransOvals(TransName)
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub
    Private Sub DynamicTransOvals(ByVal StrModuleCode As String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0
        Dim ShapeContainerNew As Microsoft.VisualBasic.PowerPacks.ShapeContainer = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & StrModuleCode & "' And VPF_Active_Report=1 order by SNo "
        Dim theLine As New LineShape
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows

                Dim ovalt As Microsoft.VisualBasic.PowerPacks.OvalShape = New Microsoft.VisualBasic.PowerPacks.OvalShape()
                ovalt.Name = "CustomOvel" + clsCommon.myCstr(i)
                i = i + 180
                m = m + 25

                Dim lbl1 As New Label
                lbl1.Text = clsCommon.myCstr(dr("Name"))
                lbl1.BackColor = Color.Transparent
                Dim myfont As New Font("Arial", 9.75, FontStyle.Bold)
                lbl1.Font = myfont
                lbl1.AutoSize = False
                lbl1.Size = New Size(102, 60)

                ' Add the oval shape to the form.
                Me.PnlTrans.Controls.Add(ShapeContainerNew)
                Me.PnlTrans.AutoScroll = True
                ShapeContainerNew.AutoScroll = False
                Me.PnlTrans.HorizontalScroll.Enabled = True

                ShapeContainerNew.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {ovalt})
                ovalt.BorderColor = Color.FromArgb(0, 162, 222)
                ' RectangleShape1.Parent.Shapes.Add(ovalt)
                lbl1.Location = New Point(i - 165, 8)
                ovalt.Size = New Size(40, 38)
                OvalY = 363 + i
                ovalt.BackColor = Color.SkyBlue
                ovalt.BackStyle = BackStyle.Opaque
                ovalt.BorderWidth = 2
                ' ovalt.Location = New Point((-375) + i, 160)
                ovalt.Location = New Point((-165) + i, 70)
                Me.PnlTrans.Controls.Add(lbl1)
                ovalt.Tag = clsCommon.myCstr(dr("Code"))
                theLine.StartPoint = New System.Drawing.Point(47, 90)
                ' theLine.EndPoint = New System.Drawing.Point(650 + 200, 180)
                theLine.EndPoint = New System.Drawing.Point(i - 150, 90)

                ovalt.BringToFront()
                lbl1.BringToFront()

                AddHandler ovalt.Click, AddressOf ovalt_Click
                AddHandler ovalt.MouseEnter, AddressOf ovalt_MouseEnter
                AddHandler ovalt.MouseLeave, AddressOf ovalt_MouseLeave
                AddHandler ovalt.MouseUp, AddressOf ovalt_MouseUp

            Next

        End If

        ShapeContainerNew.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {theLine})

        'theLine.StartPoint = New System.Drawing.Point((-150) + i, 180)
        'theLine.StartPoint = New System.Drawing.Point(50, 180)
        'theLine.EndPoint = New System.Drawing.Point(650 + 200, 180)
    End Sub
#Region "TransEvents"
    Private Sub ovalt_MouseEnter(sender As Object, e As EventArgs)
        Dim OvlColor As Integer = 0

        VertAxis = PnlTrans.VerticalScroll.Value
        HorAxis = PnlTrans.HorizontalScroll.Value
        'Dim ovalt As OvalShape = DirectCast(sender, OvalShape)

        'Dim DT_Color As DataTable

        'DT_Color = New DataTable
        'DT_Color = ClsVPFSettings.GetColor()

        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BorderColor = Color.Black
        ovalM.BackColor = Color.FromArgb(255, 231, 161)
        '  ovalM.BackStyle = Color.FromArgb(ovalM)

        'If (DT_Color IsNot Nothing AndAlso DT_Color.Rows.Count > 0) Then
        '    OvlColor = clsCommon.myCdbl(DT_Color.Rows(0)("Oval_Color"))
        '    If clsCommon.CompairString(clsCommon.myCdbl(DT_Color.Rows(0)("Is_ColorAppliedForAll")), 1) = CompairStringResult.Equal Then
        '        'ovalt.BackColor = Color.FromArgb(255, 231, 161)
        '        ovalt.BackColor = Color.FromArgb(OvlColor)
        '    Else
        '        If clsCommon.CompairString(clsCommon.myCstr(DT_Color.Rows(0)("VPFScreenCode")), ScreenCode) = CompairStringResult.Equal Then
        '            ovalt.BackColor = Color.FromArgb(OvlColor)
        '        End If
        '    End If
        'End If
        'ovalt.BackColor = Color.FromArgb(255, 231, 161)
        'ovalt.BorderColor = Color.Black
    End Sub
    Private Sub ovalt_MouseLeave(sender As Object, e As EventArgs)
        Dim OvlBColor As Integer = 0

        PnlTrans.VerticalScroll.Value = VertAxis
        PnlTrans.HorizontalScroll.Value = HorAxis
        'Dim ovalt As OvalShape = DirectCast(sender, OvalShape)
        'Dim DT_Color As DataTable

        'DT_Color = New DataTable
        'DT_Color = ClsVPFSettings.GetColor()

        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BorderColor = Color.SkyBlue
        ovalM.BackColor = Color.FromArgb(0, 162, 222)

        'If (DT_Color IsNot Nothing AndAlso DT_Color.Rows.Count > 0) Then
        '    OvlBColor = clsCommon.myCdbl(DT_Color.Rows(0)("Oval_Color"))
        '    If clsCommon.CompairString(clsCommon.myCdbl(DT_Color.Rows(0)("Is_ColorAppliedForAll")), 1) = CompairStringResult.Equal Then
        '        ovalt.BackColor = Color.FromArgb(OvlBColor)
        '        ' ovalt.BorderColor = Color.FromArgb(0, 162, 222)
        '    Else
        '        If clsCommon.CompairString(clsCommon.myCstr(DT_Color.Rows(0)("VPFScreenCode")), ScreenCode) = CompairStringResult.Equal Then
        '            ovalt.BackColor = Color.FromArgb(OvlBColor)
        '        End If
        '    End If
        'End If

        'ovalt.BackColor = Color.SkyBlue
        'ovalt.BorderColor = Color.FromArgb(0, 162, 222)
    End Sub
    Private Sub ovalt_MouseUp(sender As Object, e As EventArgs)
        Dim OvlBColor As Integer = 0

        'Dim ovalt As OvalShape = DirectCast(sender, OvalShape)
        'ovalt.BackColor = Color.SkyBlue
        'ovalt.BorderColor = Color.FromArgb(0, 162, 222)

        'Dim DT_Color As DataTable

        'DT_Color = New DataTable
        'DT_Color = ClsVPFSettings.GetColor()

        Dim ovalM As OvalShape = DirectCast(sender, OvalShape)
        ovalM.BorderColor = Color.FromArgb(0, 162, 222)
        ovalM.BackColor = Color.SkyBlue

        'If (DT_Color IsNot Nothing AndAlso DT_Color.Rows.Count > 0) Then
        '    OvlBColor = clsCommon.myCdbl(DT_Color.Rows(0)("Oval_Color"))
        '    If clsCommon.CompairString(clsCommon.myCdbl(DT_Color.Rows(0)("Is_ColorAppliedForAll")), 1) = CompairStringResult.Equal Then
        '        ovalt.BackColor = Color.FromArgb(OvlBColor)
        '    Else
        '        If clsCommon.CompairString(clsCommon.myCstr(DT_Color.Rows(0)("VPFScreenCode")), ScreenCode) = CompairStringResult.Equal Then
        '            ovalt.BackColor = Color.FromArgb(OvlBColor)
        '        End If
        '    End If
        'End If
    End Sub
    Private Sub ovalt_Click(sender As Object, e As EventArgs)

        Dim s As String = clsCommon.myCstr(DirectCast(sender, OvalShape).Tag)
        MDI.ShowForm(s, "", True)
        'Dim point As New Point(intx, inty)
        'PanelDRpt.AutoScrollPosition = point
    End Sub
#End Region



  
End Class