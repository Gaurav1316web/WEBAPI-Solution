Imports common
Public Class FrmEmptyInwardRpt
    Inherits FrmMainTranScreen




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("JRN-ENTRY")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmEmptyInwardRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



    Private Sub FrmEmptyInwardRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        Loadlocation()
        chkAll.IsChecked = True

    End Sub


    Sub Loadlocation()
        '  Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Description"
    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        cbglocation.Enabled = Not chkAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        chkAll.IsChecked = True
        Loadlocation()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try


            Dim qry As String = " SELECT     TSPL_ADJUSTMENT_DETAIL.Item_Code AS Item, TSPL_ADJUSTMENT_DETAIL.Item_Quantity AS qty, Adjustment_Date,'" + dtpstart.Value + "' as Fdate,'" + dtpend.Value + "' as Tdate,TSPL_ADJUSTMENT_DETAIL.Location_Code as Loc   FROM         TSPL_ADJUSTMENT_DETAIL INNER JOIN   TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_DETAIL.Adjustment_No = TSPL_ADJUSTMENT_HEADER.Adjustment_No where TSPL_ADJUSTMENT_HEADER.ItemType='E' and  CONVERT(date,adjustment_date ,103) >=CONVERT(date,'" + dtpstart.Value + "',103)  and CONVERT(date,adjustment_date ,103)  <=CONVERT(date,'" + dtpend.Value + "',103)"


            Dim qry1 As String = "union all "


            Dim qry2 As String = "   SELECT     TSPL_TRANSFER_DETAIL.Item_Code AS Item, TSPL_TRANSFER_DETAIL.Item_Qty  as qty, convert(varchar(10),TSPL_TRANSFER_HEAD.Transfer_Date,103) as Adjustment_Date,'" + dtpstart.Value + "' as Fdate,'" + dtpend.Value + "' as Tdate,TSPL_TRANSFER_HEAD.To_Location as Loc  FROM TSPL_TRANSFER_HEAD INNER JOIN       TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No where Transfer_Type='LI' and Item_Type='Empty' and CONVERT(date,transfer_date ,103) >=CONVERT(date,'" + dtpstart.Value + "',103)  and CONVERT(date,transfer_date ,103)  <=CONVERT(date,'" + dtpend.Value + "',103) "


            If chkAll.IsChecked = True Then


            Else

                qry += "  and TSPL_ADJUSTMENT_DETAIL.Location_Code  in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + "))"
                qry2 += "  and TSPL_TRANSFER_HEAD.To_Location  in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + "))"

            End If

            Dim qryfinal = qry & qry1 & qry2

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptEmptyInwardRegister", "Empty Inward Register Report")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
