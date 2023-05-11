
''----22/06/2012--created by usha-----

Imports common
Imports Telerik.WinControls
Imports System.IO
Public Class RptTransfer_IncompleteReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptTransfer_IncompleteReport)
        If Not (MyBase.isReadFlag) Then
            RadMessageBox.Show("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    
    Private Sub RptTransfer_IncompleteReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'Load()'---- Done By Abhishek as on 6 July 2012 due To Error Comes while We open The Form First Time.
        LoadLocation()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")



    End Sub

    Private Sub RptTransfer_IncompleteReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funprint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub
    Sub Loadsub()
        Dim qry As String
        qry = "select transfer_no as [Tansfer No],Transfer_Date  as [Tansfer Date] from TSPL_TRANSFER_HEAD where  convert(Date,Transfer_Date,103)>=convert(date,'" + dtpFromDate.Value + "',103) and convert(Date,Transfer_Date,103)<=convert(date,'" + dtpToDate.Value + "',103)"
        cbgtans.ValueMember = Nothing
        cbgtans.DataSource = Nothing
        cbgtans.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgtans.ValueMember = "Tansfer No"
        cbgtans.DisplayMember = "Tansfer Date"




    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgloc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgloc.ValueMember = "Code"
        cbgloc.DisplayMember = "Description"


    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rbtall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtall.ToggleStateChanged
        cbgtans.Enabled = rbtnselect.IsChecked
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funprint()
    End Sub
    Sub funprint()
        Dim Fromdate As String = clsCommon.myCDate(dtpFromDate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
        Dim FilterLocation As String = ""
        Dim FilterTransfer As String = ""
        Try
            If chklocselect.IsChecked = True AndAlso cbgloc.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            If chklocselect.IsChecked = True AndAlso cbgloc.CheckedValue.Count > 0 Then
                FilterLocation = "'" + clsCommon.GetMulcallString(cbgloc.CheckedValue) + "'"
                FilterLocation = FilterLocation.Replace("'", "")
            End If
            If rbtnselect.IsChecked = True AndAlso cbgtans.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Transfer No")
            End If
            If rbtnselect.IsChecked = True AndAlso cbgtans.CheckedValue.Count > 0 Then
                FilterTransfer = "'" + clsCommon.GetMulcallString(cbgtans.CheckedValue) + "'"
                FilterTransfer = FilterLocation.Replace("'", "")
            End If

            Dim qry As String = "Select '" + FilterLocation + "' as FLocation, '" + FilterTransfer + "' as FTransfer, '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd-MMM-yyyy") + "' as StratDate, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") + "' as EndDate, * from ( select transfer_no as TransferNo,'Quick Settlement' as Type,quicksettlementremarks as Remarks,TSPL_TRANSFER_HEAD.From_Location as Location,TSPL_TRANSFER_HEAD.transfer_date from tspl_transferincompleteremarks left outer join "
            qry += " TSPL_TRANSFER_HEAD on tspl_transferincompleteremarks.transferno=TSPL_TRANSFER_HEAD.Transfer_No "
            qry += " where TSPL_TRANSFER_HEAD.quick_settlement='N' union all"
            qry += " select transfer_no as TransferNo,'Sale Invoice' as Type,InvoiceRemarks as Remarks,TSPL_TRANSFER_HEAD.From_Location as Location,TSPL_TRANSFER_HEAD.transfer_date from "
            qry += " tspl_transferincompleteremarks left outer join "
            qry += " TSPL_TRANSFER_HEAD on tspl_transferincompleteremarks.transferno=TSPL_TRANSFER_HEAD.Transfer_No "
            qry += " where TSPL_TRANSFER_HEAD.sale_invoice_completed='0') XXX Where 1=1 "

            If chklocselect.IsChecked = True AndAlso cbgloc.CheckedValue.Count > 0 Then
                qry += " And  Location in (" + clsCommon.GetMulcallString(cbgloc.CheckedValue) + ")"
            End If
            If rbtnselect.IsChecked = True AndAlso cbgtans.CheckedValue.Count > 0 Then
                qry += " And  TransferNo in (" + clsCommon.GetMulcallString(cbgtans.CheckedValue) + ")"
            End If
            qry += "and  convert(Date,Transfer_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(Date,Transfer_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103)"

            qry += " Order by TransferNo,Type"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptTransferIncomplete", "Report For Transfer Incomplete")
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    
    Private Sub chklocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocall.ToggleStateChanged
        cbgloc.Enabled = False
    End Sub

    Private Sub chklocselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocselect.ToggleStateChanged
        cbgloc.Enabled = True
    End Sub

    Private Sub dtpFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
        Loadsub()
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        Loadsub()
    End Sub
End Class
