Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmEmployeeOTEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Const colDate As String = "colDate"
    Const colDocumentNo As String = "colDocumentNo"
    Const ColCategory As String = "ColCategory"
    Const ColTrip As String = "ColTrip"
    Const ColKM As String = "ColKM"
    Const ColQuantity As String = "ColQuantity"
    Const ColAmount As String = "ColAmount"
    Const ColDiesel As String = "ColDiesel"
    Const ColStation As String = "ColStation"
    Const ColStation2 As String = "ColStation2"
    Const ColStation3 As String = "ColStation3"
    Const ColStation4 As String = "ColStation4"
    Const ColIceBox As String = "ColIceBox"
    Const ColGPSKM As String = "ColGPSKM"
    Dim TotalAmount As Decimal = 0
    Dim TotalDiesel As Decimal = 0
    Dim TotalQuantity As Decimal = 0
    Dim TotalBMCQuantity As Decimal = 0
    Dim Total_Toll_Tax As Decimal = 0
    Dim Total_Ice_Charge As Decimal = 0
    Dim Total_BMC_TOTAL As Decimal = 0
    Dim Total_fat_snf_shortage As Decimal = 0
    Dim Total_Amount As Decimal = 0
    Public EnableOnPrivateChkbox As Boolean = False
    Public tripValue As String = ""

#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmEmployeeOTEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayPeriod._MYValidating

    End Sub
End Class