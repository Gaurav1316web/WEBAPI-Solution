'--03/10/2013--form Add By- panch raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmDatewiseProduction
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "DatewiseProductionReport"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
#End Region
    Sub LoadData()
        
        If isInsideLoadData Then
            clsCommon.MyMessageBoxShow("Work in Progress Please Wait...")
            Exit Sub
        End If

        btnGenrate.Enabled = True
        isInsideLoadData = True
        btnGenrate.Enabled = False
        gv1.DataSource = Nothing
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        DT = clsProductionReceipt.GetProductionReportData(Me.dtpFrom.Value, Me.dtpTo.Value)
        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            gv1.DataSource = DT

            Dim summaryRowItem As New GridViewSummaryRowItem()

            For Each dr As DataColumn In DT.Columns
                If clsCommon.CompairString(dr.ColumnName, "RECEIPT_DATE") = CompairStringResult.Equal Then
                    gv1.Columns("RECEIPT_DATE").IsVisible = True
                    gv1.Columns("RECEIPT_DATE").Width = 150
                    gv1.Columns("RECEIPT_DATE").HeaderText = "Production Date"
                    gv1.Columns("RECEIPT_DATE").FormatString = "{0:  dd/MMM/yyyy}"
               
                Else
                    gv1.Columns(dr.ColumnName).IsVisible = True
                    gv1.Columns(dr.ColumnName).Width = 100
                    Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                End If
            Next
            ReStoreGridLayout()
            'For Each dr As DataColumn In DT.Columns

            'Next

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            clsCommon.MyMessageBoxShow("No Data to Show in Selected Dates.")
        End If
        isInsideLoadData = False
        btnGenrate.Enabled = True
    End Sub

    Private Sub frmDatewiseProduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        dtpFrom.Value = clsCommon.GETSERVERDATE
        dtpTo.Value = clsCommon.GETSERVERDATE

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmDatewiseProduction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnGenrate.Visible = MyBase.isModifyFlag

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub


    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmDatewiseProduction_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()


    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
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

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoExl.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Datewise Production ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Sheet")
        clsCommon.MyExportToExcelGrid("Datewise Production", gv1, arr, "Datewise Production", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Datewise Production")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Datewise Production", gv1, arr, "Datewise Production", True)

    End Sub

    'Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
    '    'If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
    '    '    e.CellElement.TextAlignment = ContentAlignment.MiddleRight
    '    '    e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)

    '    'End If
    'End Sub

End Class
