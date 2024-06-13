Imports common
Imports System.Data.SqlClient
Imports System

Public Class frmNIRQC
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.capexmaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnPost.Visible = MyBase.isPostFlag
        CancelBtn.Visible = MyBase.isCancel_Flag_After_Posting
        'If MyBase.isReverse Then
        '    RadButton1.Enabled = True
        'Else
        '    RadButton1.Enabled = False
        'End If
        RadButton1.Visible = False
    End Sub
    Private Sub FrmCapexMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CancelBtn.Visible = False
        SetUserMgmtNew()
        LoadQCStatus()
        AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for Post ")
        ButtonToolTip.SetToolTip(CancelBtn, "Press Alt+L for Cancel ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub LoadQCStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Not OK"
        dt.Rows.Add(dr)


        cboVisualQCStatus.DataSource = dt
        cboVisualQCStatus.ValueMember = "Code"
        cboVisualQCStatus.DisplayMember = "Name"
    End Sub
    Sub AddNew()

        RadButton1.Visible = False
        isNewEntry = True
        txtCode.Value = Nothing
        txtCode.Focus()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPrint.Enabled = True
        btnPrint.Visible = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        txtDate.Text = clsCommon.GETSERVERDATE()
        BlankAllControls()
    End Sub
    Sub BlankAllControls()
        CancelBtn.Visible = False
        txtCode.Value = ""
        txtRemarks.Text = ""
        cboVisualQCStatus.SelectedValue = ""
        txtMRNNo.Value = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        BlankMRNFields()
    End Sub
    Sub BlankMRNFields()
        lblGRNNo.Text = ""
        lblGRNDate.Text = ""
        lblWeightmentNo.Text = ""
        lblWeightmentDate.Text = ""
        lblRAL.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblBillToLocationCode.Text = ""
        lblBillToLocationName.Text = ""
        lblItem.Text = ""
        lblItemName.Text = ""
        lblVehicleNo.Text = ""
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As New clsNIRQC()
        obj = clsNIRQC.GetData(strCode, NavTyep, Nothing)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            isNewEntry = False
            txtCode.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            txtMRNNo.Value = obj.MRN_No
            txtRemarks.Text = obj.QC_Remarks
            cboVisualQCStatus.SelectedValue = clsCommon.myCstr(obj.QC_Status)
            UsLock1.Status = obj.Status
            If obj.Status = ERPTransactionStatus.Approved Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                CancelBtn.Enabled = True
                CancelBtn.Visible = True
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
            End If
            LoadMRNData()
        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsNIRQC()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.MRN_No = txtMRNNo.Value
                obj.QC_Remarks = txtRemarks.Text
                obj.QC_Status = clsCommon.myCDecimal(cboVisualQCStatus.SelectedValue)
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(cboVisualQCStatus.SelectedValue) <= 0 Then
            cboVisualQCStatus.Focus()
            Throw New Exception("Please select " + cboVisualQCStatus.MyLinkLable1.Text)
        End If
        If clsCommon.myLen(txtMRNNo.Value) <= 0 Then
            txtMRNNo.Focus()
            Throw New Exception("Please select " + txtMRNNo.MyLinkLable1.Text)
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsNIRQC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub FrmCapexMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift And e.KeyCode = Keys.F12 Then
            CancelNIRQCData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    RadButton1.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating

        txtCode.Value = clsNIRQC.getFinder("", txtCode.Value, isButtonClicked)
        If txtCode.Value <> "" Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub txtMRNNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMRNNo._MYValidating
        AddNew()
        Dim qry As String = "select 
TSPL_MRN_DETAIL.MRN_No,TSPL_MRN_HEAD.MRN_Date,
TSPL_MRN_HEAD.Against_GRN,TSPL_GRN_HEAD.GRN_Date,TSPL_GRN_HEAD.VehicleNo ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo,TSPL_MRN_HEAD.Vendor_Code,TSPL_MRN_HEAD.Vendor_Name,TSPL_MRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , case when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end as [Visual QC Status], case when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else ' ' end as [Visual QC Second Status]
from TSPL_MRN_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code 
left outer join TSPL_MRN_HEAD  on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location"
        Dim whrcls As String = "TSPL_MRN_HEAD.Status=1 and TSPL_GRN_HEAD.IsSkipPurchaseQC=0 and TSPL_MRN_HEAD.NIR_QC=1 and TSPL_ITEM_MASTER.NIR_QC=1
and not exists(select 1 from TSPL_NIR_QC where TSPL_NIR_QC.MRN_No=TSPL_MRN_DETAIL.MRN_No and TSPL_NIR_QC.Document_No not in ('" + txtCode.Value + "'))  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and TSPL_MRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtMRNNo.Value = clsCommon.ShowSelectForm("NICQCMRNFnd", qry, "MRN_No", whrcls, txtMRNNo.Value, "", isButtonClicked)
        LoadMRNData()
    End Sub
    Private Sub LoadMRNData()
        BlankMRNFields()
        Dim qry As String = "select TSPL_GRN_HEAd.VisualQCStatusSecond,TSPL_GRN_HEAd.VisualQCStatus,TSPL_MRN_HEAD.Against_GRN,TSPL_GRN_HEAD.GRN_Date ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo,TSPL_MRN_HEAD.Vendor_Code,TSPL_MRN_HEAD.Vendor_Name,TSPL_MRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MRN_HEAD.VehicleNo
from TSPL_MRN_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code 
left outer join TSPL_MRN_HEAD  on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location
where TSPL_MRN_DETAIL.MRN_No='" + txtMRNNo.Value + "' and TSPL_MRN_HEAD.Status=1 and TSPL_MRN_HEAD.NIR_QC=1 and TSPL_ITEM_MASTER.NIR_QC=1"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim QC1 As Integer = clsCommon.myCdbl(dt.Rows(0)("VisualQCStatus"))
            Dim QC2 As Integer = clsCommon.myCdbl(dt.Rows(0)("VisualQCStatusSecond"))
            If QC1 = 2 OrElse QC2 = 2 Then
                clsCommon.MyMessageBoxShow(Me, "Visual QC NotOk so NIR QC not allowed ", Me.Text)
                Exit Sub
            End If
            lblGRNNo.Text = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
            If dt.Rows(0)("GRN_Date") IsNot DBNull.Value Then
                lblGRNDate.Text = clsCommon.myCstr(dt.Rows(0)("GRN_Date"))
            End If
            lblWeightmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            If dt.Rows(0)("Weighment_Date") IsNot DBNull.Value Then
                lblWeightmentDate.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_Date"))
            End If
            lblRAL.Text = clsCommon.myCstr(dt.Rows(0)("RefTendorNo"))
            lblVendorCode.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            lblBillToLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            lblBillToLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            lblItem.Text = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            lblItemName.Text = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
        End If
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsNIRQC.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsNIRQC.ReverseAndUnpost(txtCode.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim sqlqry As String = "Select case when TSPL_MRN_HEAD.Status=1 then 'OK' else 'Not OK' end as QC_Status,
TSPL_MRN_HEAD.Against_GRN,TSPL_GRN_HEAD.GRN_Date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo,TSPL_MRN_HEAD.Vendor_Code,TSPL_MRN_HEAD.Vendor_Name,TSPL_MRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MRN_HEAD.VehicleNo,TSPL_NIR_QC.Document_No,TSPL_NIR_QC.QC_Remarks,TSPL_NIR_QC.MRN_No,TSPL_NIR_QC.Document_Date
From TSPL_MRN_DETAIL
Left join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_MRN_DETAIL.MRN_No
Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code 
Left outer join TSPL_MRN_HEAD  on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No
Left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
Left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_HEAD.Against_GRN
Left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
Left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location
where TSPL_MRN_DETAIL.MRN_No ='" + txtMRNNo.Value + "' and TSPL_MRN_HEAD.Status=1 and TSPL_MRN_HEAD.NIR_QC=1 and TSPL_ITEM_MASTER.NIR_QC=1"

            Dim dtItem As DataTable
            dtItem = clsDBFuncationality.GetDataTable(sqlqry)
            If dtItem.Rows.Count > 0 Then
                Dim crysFrm As New frmCrystalReportViewer()
                crysFrm.funreport(CrystalReportFolder.PurchaseOrder, dtItem, "NIRQc", "NIRQc Details")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelNIRQCData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
                '    If clsPurchaseOrderHead.CheckPOUsedInSRNorGRN(clsCommon.myCstr(txtDocNo.Value), Nothing) Then
                '        Throw New Exception("PO can not be cancelled because it is used in SRN/GRN.")

                '    End If

                '    If (myMessages.CancelConfirms(Me)) Then
                '                Dim Qry As String = "select distinct TSPL_SRN_DETAIL.SRN_No,TSPL_SRN_HEAD.Status from TSPL_SRN_DETAIL 
                'left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.MRN_Id ='" + txtMRNNo.Value + "'"
                Dim frm1 As New FrmPWD(Nothing)
                frm1.strType = "PO Cancel"
                frm1.strCode = "PO Cancel"
                frm1.ShowDialog()
                If frm1.isPasswordCorrect Then
                    Dim iscancel As Boolean = False
                    If clsNIRQC.CheckNIRQCUsedInSRN(clsCommon.myCstr(txtMRNNo.Value), Nothing) Then
                        Throw New Exception("NIRQC can not be cancelled because it is used in SRN.")
                        'Else
                        '    clsPurchaseOrderHead.ReverseAndUnpost(txtDocNo.Value, MyBase.Form_ID)
                    End If
                    If common.clsCommon.MyMessageBoxShow("Do you want to cancel the NIRQC?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        Dim Reason As String = ""
                        If (myMessages.CancelConfirms(Me)) Then
                            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtCode.Value))
                            If clsCancelLog.CheckForReasonOnDelete() Then
                                '' REASON FOR DELETE 
                                Dim frm As New FrmFreeTxtBox1
                                frm.Text = "Remarks for Cancel"
                                frm.ShowDialog()
                                If clsCommon.myLen(frm.strRmks) <= 0 Then
                                    Exit Sub
                                Else
                                    Reason = frm.strRmks
                                End If
                            End If
                            If clsNIRQC.CancelData(clsCommon.myCstr(txtCode.Value)) Then

                                'If clsNIRQC.CancelData(Me.Form_ID, clsCommon.myCstr(txtCode.Value)) Then
                                ' saveCancelLog(Reason, "Cancel", Nothing)
                                clsCommon.MyMessageBoxShow(Me, "Data Cancel Successfully ", Me.Text)
                                AddNew()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        CancelNIRQCData()
    End Sub
End Class
