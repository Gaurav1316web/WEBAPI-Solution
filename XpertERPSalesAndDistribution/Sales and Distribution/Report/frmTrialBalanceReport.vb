Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports XpertERPEngine

Public Class frmTrialBalanceReport
    Inherits FrmMainTranScreen
    Dim strCustAccount As String
    Dim sql As String
    Dim dr As DataTable
    Dim ds As New DataSet
    Dim trans As SqlTransaction
    Dim tableName As String = "TSPL_SALE_INVOICE_HEAD"
    Dim tableCode As String = "Sale_Invoice_No"
    Dim codePrefix As String = "INVNO"
    Dim noOfZero As Integer = 3
    Dim userCode, companyCode As String
    Private abatement As String = 0
    Private ttlItemInvQtyForCheck As Decimal = 0
    Private preInvQty As Decimal = 0
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("TRI-BAL-RPT")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmTrialBalanceReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub RadForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bindgvLoadOut1Columns()
        SetUserMgmtNew()
    End Sub

    Private Sub bindgvLoadOut1Columns()
        Dim viewInfo As New GridViewInfo(grdTrial.MasterTemplate)
        Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
        grdTrial.AllowAddNewRow = True
        'dataRowInfo.Cells("Code").Value = "0"
        'dataRowInfo.Cells("name").Value = "0"
        'dataRowInfo.Cells("Description").Value = "0"
        Dim colSegType As GridViewComboBoxColumn = TryCast(grdTrial.Columns("Name"), GridViewComboBoxColumn)
        Dim colSegCode As GridViewTextBoxColumn = TryCast(grdTrial.Columns("Code"), GridViewTextBoxColumn)
        sql = "select Seg_No as [Segment No],Seg_Name as [Segment Name] from TSPL_GL_SEGMENT"
        ds = connectSql.RunSQLReturnDS(sql)
        colSegType.DisplayMember = "Segment Name"
        colSegType.ValueMember = "Segment No"
        colSegType.DataSource = ds.Tables(0)

    End Sub




    Private Sub grdTrial_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdTrial.CellClick
        'Dim CountRows = grdTrial.Rows.Count
        'If CountRows = 0 Then
        '    If e.ColumnIndex = 0 Then
        '        Dim viewInfo As New GridViewInfo(grdTrial.MasterTemplate)
        '        Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
        '        'grdTrial.AllowAddNewRow = True
        '        dataRowInfo.Cells("Code").Value = "0"
        '        dataRowInfo.Cells("name").Value = "0"
        '        dataRowInfo.Cells("desc").Value = "0"
        '        Dim colUnitCode As GridViewComboBoxColumn = TryCast(grdTrial.Columns("Name"), GridViewComboBoxColumn)
        '        Dim colLocation As GridViewComboBoxColumn = TryCast(grdTrial.Columns("Code"), GridViewComboBoxColumn)
        '        sql = "SELECT Unit_Code, Unit_Desc, Conv_Factor FROM TSPL_UNIT_MASTER ORDER BY Unit_Code"
        '        ds = connectSql.RunSQLReturnDS(sql)
        '        colUnitCode.DisplayMember = "Unit_Desc"
        '        colUnitCode.ValueMember = "Unit_Code"
        '        colUnitCode.DataSource = ds.Tables(0)

        '    End If
        'End If


        'If CountRows = 1 Then
        '    If e.ColumnIndex = 0 Then
        '        Dim viewInfo As New GridViewInfo(grdTrial.MasterTemplate)
        '        Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
        '        'grdTrial.AllowAddNewRow = True
        '        dataRowInfo.Cells("Code").Value = "0"
        '        dataRowInfo.Cells("name").Value = "0"
        '        dataRowInfo.Cells("desc").Value = "0"
        '        Dim colUnitCode As GridViewComboBoxColumn = TryCast(grdTrial.Columns("Name"), GridViewComboBoxColumn)
        '        Dim colLocation As GridViewComboBoxColumn = TryCast(grdTrial.Columns("Code"), GridViewComboBoxColumn)
        '        Dim strLoc1 As String
        '        strLoc1 = grdTrial.Rows(0).Cells(0).Value
        '        sql = "SELECT Unit_Code, Unit_Desc, Conv_Factor FROM TSPL_UNIT_MASTER where Unit_Code <> '" & strLoc1 & "'  ORDER BY Unit_Code"
        '        ds = connectSql.RunSQLReturnDS(sql)
        '        colUnitCode.DisplayMember = "Unit_Desc"
        '        colUnitCode.ValueMember = "Unit_Code"
        '        colUnitCode.DataSource = ds.Tables(0)

        '    End If
        'End If
        'If e.RowIndex = 2 Then

        'End If
        'If e.RowIndex = 3 Then

        'End If
        'If e.RowIndex = 4 Then

        'End If
        'If e.RowIndex = 5 Then

        'End If
        'If e.RowIndex = 6 Then

        'End If
        'If e.RowIndex = 7 Then

        'End If
        'If e.RowIndex = 8 Then

        'End If
        'If e.RowIndex = 9 Then

        'End If
        'If e.RowIndex = 10 Then

        'End If
    End Sub

    Private Sub grdTrial_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTrial.DoubleClick
        Try
            If clsCommon.CompairString(grdTrial.CurrentCell.ColumnInfo.Name, colCode) = CompairStringResult.Equal Then
                Dim qry As String = "select Segment_code as [SegmnentCode],Description as [Description] from TSPL_GL_SEGMENT_CODE "
                Dim dblNewCode As String = clsCommon.myCstr(clsCommon.ShowSelectForm("TrialBalance", qry, "SegmnentCode", "Seg_No='" + clsCommon.myCstr(grdTrial.CurrentRow.Cells(0).Value) + "'", ""))
                grdTrial.CurrentRow.Cells(1).Value = dblNewCode
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
       
        printdata()
    End Sub
    Sub printdata()
        Try
            FrmProvionalSalesReport.proShowReport("Trial balance", dtpFdate.Value, DtpTodate.Value)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
