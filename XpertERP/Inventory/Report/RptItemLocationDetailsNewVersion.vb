'-------------------created by usha--------
'----------6/7/2012--------
'------added checkbox of item,location,batch,mrp-----------
'---Preeti Gupta--ticket no-[BM00000003140]
Imports common
Imports System.Data.SqlClient



Public Class RptItemLocationDetailsNewVersion
    Inherits FrmMainTranScreen

    Private Sub RptItemLocationDetailsNewVersion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P AndAlso btnprint.Enabled Then
            printdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub


    Private Sub RptItemLocationDetailsNewVersion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadItem()
        LoadLocation()
        LoadBatch()
        LoadMrp()

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemLocationDetailsReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnprint.Visible = MyBase.isPrin
        'btnclose.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadItem()
        Dim qry As String = " select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER  "
        cbgitem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgitem.ValueMember = "Code"
        cbgitem.DisplayMember = "Description"
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER where Location_Type ='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Sub LoadBatch()
        Dim qry As String = " select Batch_No as BatchNo ,Item_Code as Code from TSPL_ITEM_LOCATION_DETAILS  where 2=2 "

        If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(cbgitem.CheckedValue) + ")"
        End If
        If chklocationselect.IsChecked = True AndAlso cbglocation.CheckedValue.Count > 0 Then
            qry += " and Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")"
        End If
        cbgbatch.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgbatch.ValueMember = "BatchNo"
        cbgbatch.DisplayMember = "Code"

    End Sub
    Sub LoadMrp()
        Dim qry As String = " select  CONVERT(varchar(20),MRP) as MRP ,Item_Code as Code from TSPL_ITEM_LOCATION_DETAILS where 2=2"
        If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(cbgitem.CheckedValue) + ")"
        End If
        If chklocationselect.IsChecked = True AndAlso cbglocation.CheckedValue.Count > 0 Then
            qry += " and Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")"
        End If
        cbgmrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgmrp.ValueMember = "MRP"
        cbgmrp.DisplayMember = "Code"
    End Sub
    Private Sub chkitemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemAll.ToggleStateChanged
        cbgitem.Enabled = False
    End Sub

    Private Sub chkitemSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemSelect.ToggleStateChanged
        cbgitem.Enabled = True
    End Sub

    Private Sub chklocationall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocationall.ToggleStateChanged
        cbglocation.Enabled = False
    End Sub

    Private Sub chklocationselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocationselect.ToggleStateChanged
        cbglocation.Enabled = True
    End Sub

    Private Sub chkbatchall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkbatchall.ToggleStateChanged
        cbgbatch.Enabled = False
    End Sub

    Private Sub chkbatchselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkbatchselect.ToggleStateChanged
        cbgbatch.Enabled = True
    End Sub

    Private Sub chkmrpall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmrpall.ToggleStateChanged
        cbgmrp.Enabled = False
    End Sub

    Private Sub chkmrpselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmrpselect.ToggleStateChanged
        cbgmrp.Enabled = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printdata()

    End Sub
    Sub printdata()
        Try

            Dim qry As String = ""


            If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item or select ALL", Me.Text)
                Exit Sub
            End If

            If chklocationselect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location or select ALL", Me.Text)
                Exit Sub
            End If
            If chkbatchselect.IsChecked = True AndAlso cbgbatch.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Batch or select ALL", Me.Text)
                Exit Sub
            End If
            If chkmrpselect.IsChecked = True AndAlso cbgmrp.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one MRP or select ALL", Me.Text)
                Exit Sub
            End If
            qry = " select Item_Code, Item_Desc,Location_Code, Location_Desc,MRP,Batch_No, Item_Qty,case when Item_Qty=0 then 0 else CONVERT(decimal(18,2), ROUND( Amount/Item_Qty,2)) end as Rate, Amount,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  from("
            qry += "select Item_Code,max(Item_Desc) as Item_Desc,TSPL_ITEM_LOCATION_DETAILS.Location_Code,max(TSPL_ITEM_LOCATION_DETAILS.Location_Desc) as Location_Desc,MRP,Batch_No,sum(Item_Qty) as Item_Qty,sum(Amount) as Amount "
            qry += "from TSPL_ITEM_LOCATION_DETAILS "
            qry += "left outer join TSPL_LOCATION_MASTER on TSPL_ITEM_LOCATION_DETAILS .Location_Code =TSPL_LOCATION_MASTER.Location_Code "
            qry += " where 2 = 2 and TSPL_LOCATION_MASTER.Location_Type ='physical'"
            If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count > 0 Then
                qry += " and Item_Code in (" + clsCommon.GetMulcallString(cbgitem.CheckedValue) + ")"
            End If
            If chklocationselect.IsChecked = True AndAlso cbglocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")"
            End If
            If chkbatchselect.IsChecked = True AndAlso cbgbatch.CheckedValue.Count > 0 Then
                qry += " and Batch_No in (" + clsCommon.GetMulcallString(cbgbatch.CheckedValue) + ")"
            End If
            If chkmrpselect.IsChecked = True AndAlso cbgmrp.CheckedValue.Count > 0 Then
                qry += " and MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ")"
            End If
            qry += "group by Item_Code,TSPL_ITEM_LOCATION_DETAILS.Location_Code,MRP,Batch_No"
            qry += ") xxx"
            qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                '' dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "CrptItemLocationDetailsNew", "Report of New Item Location Details")
                frmCRV = Nothing
            End If
           
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cbgitem_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cbgitem.Validating
        LoadBatch()
        LoadMrp()
    End Sub

    Private Sub cbglocation_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cbglocation.Validating
        LoadBatch()
        LoadMrp()
    End Sub
End Class
