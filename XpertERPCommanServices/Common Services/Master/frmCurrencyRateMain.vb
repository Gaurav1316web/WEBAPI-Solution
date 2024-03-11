Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports System.Data.SqlClient
Public Class frmCurrencyRateMain
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "CurrencyRate"

    Private Const colCode As String = "colCode"
    Private Const colFromCurr As String = "colFromCurr"
    Private Const colToCurr As String = "colToCurr"
    Private Const colFromDate As String = "colFromDate"
    Private Const colToDate As String = "colToDate"
    Private Const colRate As String = "colRate"
    Private Const colDesc As String = "colDesc"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False

    Private Sub FrmDealerMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData(False)
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A for Add New Record")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R for Refresh ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCurrencyConversion)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If 
    End Sub

    Sub LoadData(ByVal isShowMsg As Boolean)
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            isInsideLoadData = True
            Dim qry As String = "select Code, FROM_CURRENCY as [From Currency], TO_CURRENCY as [To Currency], convert(varchar,FROM_DATE,103) as [From Date], convert(varchar,TO_DATE,103) as [To Date], Rate, DESCRIPTION as [Description] from TSPL_CURRENCY_CONVERSION_RATE "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                If isShowMsg Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If
            Else
                'For Each dr As DataRow In dt.Rows
                '    gv1.Rows.AddNew()
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("Client_id"))
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colName).Value = clsCommon.myCstr(dr("Client_Name"))
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDesc).Value = clsCommon.myCstr(dr("type"))
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colFromDate).Value = clsCommon.myCstr(dr("Address"))
                'Next
                gv1.DataSource = dt
                gv1.CurrentRow = gv1.Rows(0)
                gv1.BestFitColumns()
            End If
            ReStoreGridLayout()
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        Dim frm As New frmCurrencyConversion()
        frm.SetUserMgmt("")
        frm.ShowDialog()
        LoadData(False)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim frm As New frmCurrencyConversion()
        frm.SetUserMgmt("")
        frm.StrCode = clsCommon.myCstr(gv1.CurrentRow.Cells("Code").Value)
        frm.ShowDialog()
        LoadData(False)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(False)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim str As String
        str = " select Code, FROM_CURRENCY as [From Currency], TO_CURRENCY as [To Currency], FROM_DATE as [From Date], TO_DATE as [To Date], DESCRIPTION as [Description] from TSPL_CURRENCY_CONVERSION_RATE "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "From Currency", "To Currency", "From Date", "To Date", "Description") Then
            'Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCurrencyConversion()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("From Currency can not be blank or incorrect.")
                    End If
                    obj.FROM_CURRENCY = strName

                    strName = clsCommon.myCstr(grow.Cells(2).Value)
                    If strName.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("To Currency can not be blank or incorrect.")
                    End If
                    obj.TO_CURRENCY = strName

                    Dim fDate As Date = clsCommon.myCDate(grow.Cells(3).Value)
                    If fDate.Year < 1900 Then
                        Throw New Exception("From Date can not be blank or incorrect.")
                    End If
                    obj.FROM_DATE = fDate

                    Dim TDate As Date = clsCommon.myCDate(grow.Cells(4).Value)
                    If TDate.Year < 1900 Then
                        Throw New Exception("To Date can not be blank or incorrect.")
                    End If
                    obj.TO_DATE = TDate

                    Dim strDes As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strDes.Length > 200 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = strDes

                    obj.SaveData(obj, True)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub mbtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub mbtnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnDeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub frmCurrencyRateMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.A AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnRefresh.Enabled Then
            LoadData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
End Class
