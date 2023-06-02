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


Public Class FrmMCCProcurementCycle
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmMCCProcurementCycle)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub ProgramName()
        LblOpenMCC.Tag = clsUserMgtCode.frmOpenMCCShift
        LblOpenMCC.Text = ProgramCodeNew.GetProgramName(LblOpenMCC.Tag)
        LblMilkRcp.Tag = clsUserMgtCode.frmMilkReceipt
        LblMilkRcp.Text = ProgramCodeNew.GetProgramName(LblMilkRcp.Tag)
        LblMilkSample.Tag = clsUserMgtCode.frmMilkSample
        LblMilkSample.Text = ProgramCodeNew.GetProgramName(LblMilkSample.Tag)
        'LblMilTrSh.Tag = clsUserMgtCode.MilkTruckSheet
        LblMilTrSh.Text = ProgramCodeNew.GetProgramName(LblMilTrSh.Tag)
        LblVLCDataUpl.Tag = clsUserMgtCode.frmVlcdataUploadar
        LblVLCDataUpl.Text = ProgramCodeNew.GetProgramName(LblVLCDataUpl.Tag)
        LblMilkShiftEnd.Tag = clsUserMgtCode.frmMilkShiftEndMCC
        LblMilkShiftEnd.Text = ProgramCodeNew.GetProgramName(LblMilkShiftEnd.Tag)
        LblTankerDispatch.Tag = clsUserMgtCode.frmMCCDispatch
        LblTankerDispatch.Text = ProgramCodeNew.GetProgramName(LblTankerDispatch.Tag)
        LblVSPBillInc.Tag = clsUserMgtCode.MilkVSPPayment
        LblVSPBillInc.Text = ProgramCodeNew.GetProgramName(LblVSPBillInc.Tag)
        LblPayPro.Tag = clsUserMgtCode.frmPaymentProcess
        LblPayPro.Text = ProgramCodeNew.GetProgramName(LblPayPro.Tag)

        LblRptTakerDis.Text = ProgramCodeNew.GetProgramName(clsUserMgtCode.frmMCCDispatch)
    End Sub
    Private Sub OvlTag()
        OvlOpenMCC.Tag = clsUserMgtCode.frmOpenMCCShift
        OvlMilkRcp.Tag = clsUserMgtCode.frmMilkReceipt
        OvlMilkRcp.Tag = clsUserMgtCode.frmMilkSample
        'OvlMilkTrSh.Tag = clsUserMgtCode.MilkTruckSheet
        OvlVLCDataUpl.Tag = clsUserMgtCode.frmVlcdataUploadar
        OvlMilkShiftEnd.Tag = clsUserMgtCode.frmMilkShiftEndMCC
        OvlTankerDispatch.Tag = clsUserMgtCode.frmMCCDispatch
        OvlVSPBillInc.Tag = clsUserMgtCode.MilkVSPPayment
        OvlPayPro.Tag = clsUserMgtCode.frmPaymentProcess

        OvlVSPMas.Tag = clsUserMgtCode.frmVSPMaster
        LblVSPMas.Text = ProgramCodeNew.GetProgramName(OvlVSPMas.Tag)
        OvlMCCMas.Tag = clsUserMgtCode.frmMCCMaster
        LblMCCMas.Text = ProgramCodeNew.GetProgramName(OvlMCCMas.Tag)
        OvlVLCMas.Tag = clsUserMgtCode.frmVLCMaster
        LblVLCMas.Text = ProgramCodeNew.GetProgramName(OvlVLCMas.Tag)
        OvlPTMas.Tag = clsUserMgtCode.frmPrimaryTransporterVehicalMaster
        LblPTVMas.Text = ProgramCodeNew.GetProgramName(OvlPTMas.Tag)
        OvlPriceCU.Tag = clsUserMgtCode.FrmPriceChartUploader
        LblPriceCU.Text = ProgramCodeNew.GetProgramName(OvlPriceCU.Tag)
        OvlIncMas.Tag = clsUserMgtCode.frmIncentiveMaster
        LblIncMas.Text = ProgramCodeNew.GetProgramName(OvlIncMas.Tag)
        OvlVLCTarg.Tag = clsUserMgtCode.frmVLCMasterTarget
        LblVLCTarMas.Text = ProgramCodeNew.GetProgramName(OvlVLCTarg.Tag)
    End Sub
    Private Sub DBBind()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As Integer = 0
        Dim LocY As Integer = 0
        Dim OvalY As Integer = 0

        Dim qry As String = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='SMMPROCRPT' And VPF_Active_Report=1 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                i = i + 25
                Dim lbl As New Label
                lbl.Text = clsCommon.myCstr(dr("Name"))
                Me.Controls.Add(lbl)
                lbl.Location = New Point(495, 267 + i)
                LocY = 292 + i
                If clsCommon.myCdbl(LocY) > 593 Then
                    j = j + 25
                    lbl.Location = New Point(748, 267 + j)
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
                oval.Location = New Point(443, 267 + i)
                OvalY = 292 + i
                If clsCommon.myCdbl(LocY) > 590 Then
                    m = m + 25
                    oval.Location = New Point(702, 267 + m)
                End If
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
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.SkyBlue
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseLeave(sender As Object, e As EventArgs)
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_MouseUp(sender As Object, e As EventArgs)

        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.White
        oval.BorderColor = Color.Black
        oval.FillStyle = FillStyle.LightHorizontal
    End Sub
    Private Sub oval_Click(sender As Object, e As EventArgs)
        Dim s As String = clsCommon.myCstr(DirectCast(sender, OvalShape).Tag)
        MDI.ShowForm(s, "", True)
    End Sub 
    Private Sub FrmMCCProcurementCycle_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ProgramName()
        OvlTag()
        DBBind()
    End Sub

#Region "MouseEvents"
    Private Sub OvlOpenMCC_MouseEnter(sender As Object, e As EventArgs) Handles OvlOpenMCC.MouseEnter, OvlMilkRcp.MouseEnter, OvlMilkSample.MouseEnter, OvlMilkTrSh.MouseEnter, OvlVLCDataUpl.MouseEnter, OvlMilkShiftEnd.MouseEnter, OvlTankerDispatch.MouseEnter, OvlVSPBillInc.MouseEnter, OvlPayPro.MouseEnter
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(255, 231, 161)
    End Sub
    Private Sub OvlOpenMCC_MouseLeave(sender As Object, e As EventArgs) Handles OvlOpenMCC.MouseLeave, OvlMilkRcp.MouseLeave, OvlMilkSample.MouseLeave, OvlMilkTrSh.MouseLeave, OvlVLCDataUpl.MouseLeave, OvlMilkShiftEnd.MouseLeave, OvlTankerDispatch.MouseLeave, OvlVSPBillInc.MouseLeave, OvlPayPro.MouseLeave
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
    Private Sub OvlOpenMCC_MouseUp(sender As Object, e As MouseEventArgs) Handles OvlOpenMCC.MouseUp, OvlMilkRcp.MouseUp, OvlMilkSample.MouseUp, OvlMilkTrSh.MouseUp, OvlVLCDataUpl.MouseUp, OvlMilkShiftEnd.MouseUp, OvlTankerDispatch.MouseUp, OvlVSPBillInc.MouseUp, OvlPayPro.MouseUp
        Dim oval As OvalShape = DirectCast(sender, OvalShape)
        oval.BackColor = Color.FromArgb(43, 153, 204)
    End Sub
#End Region
#Region "Clicks"
    Private Sub OvlOpenMCC_Click(sender As Object, e As EventArgs) Handles OvlOpenMCC.Click
        MDI.ShowForm(LblOpenMCC.Tag, "", False)
    End Sub
    Private Sub OvlMilkRcp_Click(sender As Object, e As EventArgs) Handles OvlMilkRcp.Click
        MDI.ShowForm(LblMilkRcp.Tag, "", False)
    End Sub
    Private Sub OvlMilkSample_Click(sender As Object, e As EventArgs) Handles OvlMilkSample.Click
        MDI.ShowForm(LblMilkSample.Tag, "", False)
    End Sub
    Private Sub OvlMilkTrSh_Click(sender As Object, e As EventArgs) Handles OvlMilkTrSh.Click
        MDI.ShowForm(LblMilTrSh.Tag, "", False)
    End Sub
    Private Sub OvlVLCDataUpl_Click(sender As Object, e As EventArgs) Handles OvlVLCDataUpl.Click
        MDI.ShowForm(LblVLCDataUpl.Tag, "", False)
    End Sub
    Private Sub OvlMilkShiftEnd_Click(sender As Object, e As EventArgs) Handles OvlMilkShiftEnd.Click
        MDI.ShowForm(LblMilkShiftEnd.Tag, "", False)
    End Sub
    Private Sub OvlTankerDispatch_Click(sender As Object, e As EventArgs) Handles OvlTankerDispatch.Click
        MDI.ShowForm(LblTankerDispatch.Tag, "", False)
    End Sub
    Private Sub OvlVSPBillInc_Click(sender As Object, e As EventArgs) Handles OvlVSPBillInc.Click
        MDI.ShowForm(LblVSPBillInc.Tag, "", False)
    End Sub
    Private Sub OvlPayPro_Click(sender As Object, e As EventArgs) Handles OvlPayPro.Click
        MDI.ShowForm(LblPayPro.Tag, "", False)
    End Sub
#End Region
#Region "ReportFormatClick"
    Private Sub OvlRptTankerDis_Click(sender As Object, e As EventArgs) Handles OvlRptTankerDis.Click
        Dim Qry As String = String.Empty
        Dim MCC_Code As String = String.Empty
        Dim Mcc_Or_Plant_Code As String = String.Empty

        OvlRptTankerDis.Tag = clsMccDispatch.getFinder("", OvlRptTankerDis.Tag, True)
        'OvlRptTankerDis.Tag = clsCommon.ShowSelectForm("VPFPurInd", Qry, "Code", "", OvlRptTankerDis.Tag, "Code", True)
        MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Code From TSPL_MCC_DISPATCH_CHALLAN Where Chalan_NO ='" & OvlRptTankerDis.Tag & "'"))
        Mcc_Or_Plant_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Mcc_Or_Plant_Code From TSPL_MCC_DISPATCH_CHALLAN Where Chalan_NO ='" & OvlRptTankerDis.Tag & "'"))
        If clsCommon.myLen(OvlRptTankerDis.Tag) > 0 Then
            FrmMccDispatch.printData(OvlRptTankerDis.Tag, MCC_Code, Mcc_Or_Plant_Code)
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub
#End Region
#Region "Masters__Click"
    Private Sub OvlVSPMas_Click(sender As Object, e As EventArgs) Handles OvlVSPMas.Click
        MDI.ShowForm(OvlVSPMas.Tag, "", False)
    End Sub
    Private Sub OvlMCCMas_Click(sender As Object, e As EventArgs) Handles OvlMCCMas.Click
        MDI.ShowForm(OvlMCCMas.Tag, "", False)
    End Sub
    Private Sub OvlVLCMas_Click(sender As Object, e As EventArgs) Handles OvlVLCMas.Click
        MDI.ShowForm(OvlVLCMas.Tag, "", False)
    End Sub
    Private Sub OvlPTMas_Click(sender As Object, e As EventArgs) Handles OvlPTMas.Click
        MDI.ShowForm(OvlPTMas.Tag, "", False)
    End Sub
    Private Sub OvlPriceCU_Click(sender As Object, e As EventArgs) Handles OvlPriceCU.Click
        MDI.ShowForm(OvlPriceCU.Tag, "", False)
    End Sub
    Private Sub OvlVLCTarg_Click(sender As Object, e As EventArgs) Handles OvlVLCTarg.Click
        MDI.ShowForm(OvlVLCTarg.Tag, "", False)
    End Sub
    Private Sub OvlIncMas_Click(sender As Object, e As EventArgs) Handles OvlIncMas.Click
        MDI.ShowForm(OvlIncMas.Tag, "", False)
    End Sub
#End Region


End Class