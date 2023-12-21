' ----------------- Created By Anubhooti On 21-Jan-2015 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO

Public Class FrmApprovedSuppliers
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False

    Const colApproved As String = "Approved"
    Const colRegistration As String = "Registration No."
    Const colSNo As String = "S No."
    Const colName As String = "Name of Supplier"
    Const colAdd As String = "Address of Supplier"
    Const colCategory As String = "Category"
    Const colProduct As String = "Product"
    Const colMailingAdd As String = "Mailing Address"
    Const colContactNo As String = "Contact No."
    Const colQMSStatusCerti As String = "QMS,Status/Certificate valdity"
    Const colRemarks As String = "Remarks"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmApprovedSuppliers)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnpost.Visible = MyBase.isPostFlag
    End Sub

    Private Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtrequisitioncode.Value) < 1 Then
        '    clsCommon.MyMessageBoxShow("Please select a requisition code ")
        '    Return False
        'End If
        Dim IsRow As Boolean = False
        For i As Integer = 0 To gv1.Rows.Count - 1
            If CBool(gv1.Rows(i).Cells(colApproved).Value) = True Then
                IsRow = True
            End If
        Next
        If IsRow = False Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast one registration no.", Me.Text)
            Return False
        End If
        Return True
    End Function
    Public Sub Reset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub
    Sub LoadData()
        Dim strquery As String = ""
        strquery = " SELECT CAST(ISNULL(Approved,0) as Bit) AS [Approved], Registration_No AS [Registration No],Supplier_Name AS [Name of Supplier],Supplier_Address + ' ' +Supplier_Address2 AS [Address of Supplier],Category,Product,Email AS [Mailing Address],Phone_No_Work + CHAR(13)+CHAR(10) + Fax_No_Work AS [Contact No],CASE WHEN ISNULL(System_Certification,'')='ANY OTHER' THEN Other_Certification ELSE System_Certification END AS [QMS,Status/Certificate validity],Comments AS Remarks FROM TSPL_SUPPLIER_REGISTRATION WHERE Posted =1 AND Approved=0 "

        gv1.DataSource = clsDBFuncationality.GetDataTable(strquery)

        FormatGrid()

        'btnpost.Enabled = True
        btnsave.Enabled = True
        btnsave.Text = "Approve"
    End Sub
    Public Sub savedata()
        Try
            Dim currentdate As Date = Date.Today
            Dim IsApproved As Integer
            Dim qry1 As String
            Dim Approved_Date As String
            Dim Approved_By As String
            Dim ifposted As Integer = 0

            If AllowToSave() Then

                For i As Integer = 0 To gv1.Rows.Count - 1

                    If CBool(gv1.Rows(i).Cells(colApproved).Value) = True Then
                        IsApproved = 1
                        Approved_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                        Approved_By = "'" + objCommonVar.CurrentUserCode + "'"

                        qry1 = "UPDATE TSPL_SUPPLIER_REGISTRATION SET Approved =" + clsCommon.myCstr(IsApproved) + ", Approved_Date = " + clsCommon.myCstr(Approved_Date) + " ,Approved_By = " + clsCommon.myCstr(Approved_By) + " WHERE Registration_No ='" + clsCommon.myCstr(gv1.Rows(i).Cells("Registration No").Value) + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry1)
                        clsCommon.MyMessageBoxShow("Record approved successfully", Me.Text)
                        ifposted = ifposted + 1

                    End If

                Next
                ''For Post Working 20-Aug
                LoadData()
                'If ifposted = 0 Then
                '    clsCommon.MyMessageBoxShow("You can not save!either all entries are already posted or no shortlisted applicant found.")
                'End If
            End If

            btnsave.Text = "Approved"
            'btnpost.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
   
    Sub FormatGrid()
        If gv1.Rows.Count > 0 Then
            gv1.Columns("Approved").Width = 75
            gv1.Columns("Approved").ReadOnly = False
            gv1.Columns("Registration No").Width = 100
            gv1.Columns("Registration No").ReadOnly = True
            gv1.Columns("Name of Supplier").Width = 200
            gv1.Columns("Name of Supplier").ReadOnly = True
            gv1.Columns("Address of Supplier").Width = 200
            gv1.Columns("Address of Supplier").ReadOnly = True
            gv1.Columns("Category").Width = 75
            gv1.Columns("Category").ReadOnly = True
            gv1.Columns("Mailing Address").Width = 100
            gv1.Columns("Mailing Address").ReadOnly = True
            gv1.Columns("Contact No").Width = 100
            gv1.Columns("Contact No").ReadOnly = True
            gv1.Columns("QMS,Status/Certificate validity").Width = 150
            gv1.Columns("QMS,Status/Certificate validity").ReadOnly = True
            gv1.Columns("Remarks").Width = 75
            gv1.Columns("Remarks").ReadOnly = True
        End If
    End Sub
  

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtrequisitioncode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

 

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        'gv1.MasterTemplate
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        savedata()
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub

    Private Sub FrmApprovedSuppliers_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_SUPPLIER_REGISTRATION")
        End If
    End Sub

    Private Sub FrmApprovedSuppliers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save Transaction")
        'ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        LoadData()
    End Sub
End Class
