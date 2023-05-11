'--------Created By Richa 16/01/2015 Against Ticket No 
Imports common
Imports System.Data.SqlClient

Public Class FrmMTReportContextFormat
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmMTReportContextFormat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmMTReportContextFormat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadData()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RDSGSWaiver
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmMTReportContextFormat)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Sub SaveData()
        Try
            Dim obj As New ClsReportContextFormatMT()
           
            obj.SGS_Waiver_Context = clsCommon.myCstr(txtSGSWaiverContext.Text)
            obj.Merchant_Dec_Context = clsCommon.myCstr(TxtMerchantDecContext.Text)
            obj.Merchant_Dec_Context_Format2 = clsCommon.myCstr(TxtMerDecFormat2.Text)
            obj.LC_Issuing_Application_Context = clsCommon.myCstr(txtLCIssueApp.Text)
            obj.Trust_Receipt_Context = clsCommon.myCstr(TxtTrustReceipt.Text)
            obj.Acceptance_Letter_Context = clsCommon.myCstr(TxtAcceptanceLetterContext.Text)
            Dim drcount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(SGS_Waiver_Context) from TSPL_REPORT_FORMAT_DECLARATION_MT where ISNull(Comp_Code,'')<>''"))
            If drcount = 0 Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If
            If (ClsReportContextFormatMT.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData()
        Dim obj As ClsReportContextFormatMT = ClsReportContextFormatMT.GetData()
        If obj IsNot Nothing Then
            isNewEntry = False
            txtSGSWaiverContext.Text = obj.SGS_Waiver_Context
            TxtMerchantDecContext.Text = obj.Merchant_Dec_Context
            txtLCIssueApp.Text = obj.LC_Issuing_Application_Context
            TxtMerDecFormat2.Text = obj.Merchant_Dec_Context_Format2
            TxtTrustReceipt.Text = obj.Trust_Receipt_Context
            TxtAcceptanceLetterContext.Text = obj.Acceptance_Letter_Context
        End If
    End Sub

    
    
End Class
