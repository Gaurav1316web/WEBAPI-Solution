Imports System.Data.SqlClient
Imports common

Public Class frmTenderShortPenalty
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedTaxOpen As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        RadButton1.Visible = False
        RadButton3.Visible = False
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AddNew()
        SetLength()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
    End Sub
    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtItem.Value = ""
        lblItem.Text = ""
        txtTenderNo.Value = ""
        txtBillToLocation.Enabled = True
        txtItem.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False

        EnableDisableControls(True)

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
    End Sub

    Sub EnableDisableControls(ByVal val As Boolean)
        txtTenderNo.Enabled = val
        txtVendorNo.Enabled = val
        txtItem.Enabled = val
        txtBillToLocation.Enabled = val
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllControls()
        txtDate.Focus()
        'RadButton1.Visible = False
        RadButton2.Enabled = True
        'RadButton3.Enabled = True
    End Sub
    Function AllowToSave() As Boolean
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("UserStatus").Value) Then
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells("FinalStatus").Value) = 0 Then
                        If clsCommon.myCDecimal(gv1.Rows(ii).Cells("NIRQCStatus").Value) = 0 Then
                            Throw New Exception("QC of GRN [" + clsCommon.myCstr(gv1.Rows(ii).Cells("GRN_No").Value) + "] is Pending/Not Generated But NIR QC Genrated")
                        Else
                            Throw New Exception("Invalid GRN [" + clsCommon.myCstr(gv1.Rows(ii).Cells("GRN_No").Value) + "] Because SRN should be Posted")
                        End If
                    End If
                    If ii > 0 Then
                        If Not clsCommon.myCBool(gv1.Rows(ii - 1).Cells("UserStatus").Value) Then
                            Throw New Exception("Please First Check GRN [" + clsCommon.myCstr(gv1.Rows(ii - 1).Cells("GRN_No").Value) + "]")
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub SaveData(ByVal ChekBtnPost As Boolean, Optional ByVal isamendment As Boolean = False)
        Dim obj As New clsTenderPenalty()
        Try
            btnSave.Focus()
            If (AllowToSave()) Then
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Tender_No = txtTenderNo.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Item_Code = txtItem.Value
                obj.Location_Code = txtBillToLocation.Value
                obj.Remarks = txtRemarks.Text
                obj.Arr = New ArrayList()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells("UserStatus").Value) Then
                        If Not obj.Arr.Contains(clsCommon.myCstr(grow.Cells("SRN_No").Value)) Then
                            obj.Arr.Add(clsCommon.myCstr(grow.Cells("SRN_No").Value))
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    If ChekBtnPost = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsTenderPenalty()
        Try
            btnSave.Enabled = True
            btnPost.Enabled = False
            btnDelete.Enabled = False
            isInsideLoadData = False
            isNewEntry = True
            btnSave.Text = "Save"
            BlankAllControls()
            obj = clsTenderPenalty.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                RadButton2.Enabled = True
                RadButton1.Enabled = False
                RadButton3.Enabled = False
                btnSave.Text = "Update"
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    RadButton2.Enabled = False
                    RadButton1.Enabled = True
                    RadButton3.Enabled = True
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtTenderNo.Value = obj.Tender_No
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.VendorName
                txtItem.Value = obj.Item_Code
                lblItem.Text = obj.ItemName
                txtBillToLocation.Value = obj.Location_Code
                lblBillToLocation.Text = obj.LocationName
                txtRemarks.Text = obj.Remarks


                EnableDisableControls(False)

                Dim qry As String = " and  TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(obj.Arr) + ")"
                qry = GetBaseQery("1", qry)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


                SetGridFormation(dt)
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                'SaveData(True)
                If (clsTenderPenalty.PostData(txtDocNo.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    ''If (clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    ''    Print()
                    ''End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsTenderPenalty.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_TENDER_PENALTY where Document_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT TSPL_TENDER_PENALTY.Document_No,TSPL_TENDER_PENALTY.Document_Date,TSPL_TENDER_PENALTY.Tender_No, TSPL_TENDER_PENALTY.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_TENDER_PENALTY.Vendor_Code as Vendor,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_PENALTY.Item_Code as Item, TSPL_ITEM_MASTER.Item_Desc as ItemName,TSPL_TENDER_PENALTY.Remarks,case when TSPL_TENDER_PENALTY.Status='0' then 'Pending' else 'Approved' end as [Status]
FROM TSPL_TENDER_PENALTY 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TENDER_PENALTY.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_PENALTY.Vendor_Code 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_PENALTY.Item_Code"
        Dim whrClas As String = " 2=2   "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_TENDER_PENALTY.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("TSP@Fnd", qry, "Document_No", whrClas, txtDocNo.Value, "TSPL_TENDER_PENALTY.Document_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    RadButton1.Visible = True
                    RadButton3.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        e.Cancel = True
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsTenderPenalty.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
            lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then

                Throw New Exception("Please select Location")
            End If
            Dim qry As String = "select xx.DocumentCode,max(xx.DocumentDate) as DocumentDate,xx.Location,max(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,xx.Vendor_Code,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,xx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc from (
select DocumentCode,max(DocumentDate) as DocumentDate,Location,Vendor_Code,Item_Code,1 as RI,1 as Chk from (
select TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_HEADER.DocumentDate,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_TENDER_DETAIL.Item_Code from TSPL_TENDER_DETAIL
left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode
where TSPL_TENDER_HEADER.Posted=1  and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "'
)x Group by DocumentCode,Location,Vendor_Code,Item_Code
union all
select TSPL_TENDER_PENALTY.Tender_No as DocumentCode,null as  DocumentDate, Location_Code as Location,Vendor_Code,Item_Code,-1 as RI,0 as Chk from TSPL_TENDER_PENALTY where TSPL_TENDER_PENALTY.Document_No not in ('" + txtDocNo.Value + "')
)xx 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code
Group by xx.DocumentCode,xx.Location,xx.Vendor_Code,xx.Item_Code having sum(xx.RI)>0 and sum(xx.Chk)>0 order by DocumentDate"

            Dim whr As String = "TSPL_TENDER_HEADER.Posted=1 and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "')"
            txtTenderNo.Value = clsTenderHead.getFinder(whr, txtTenderNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtTenderNo.Value = ""
        End Try
    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
            Dim whr As String = " TSPL_VENDOR_MASTER.Status='N' and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code) "
            txtVendorNo.Value = clsVendorMaster.getFinder(whr, txtVendorNo.Value, isButtonClicked)
            lblVendorName.Text = clsVendorMaster.GetName(txtVendorNo.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtVendorNo.Value = ""
        End Try
    End Sub
    Private Sub txtItem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtItem._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Vendor")
            End If
            Dim whr As String = "  exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_TENDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) "
            txtItem.Value = clsItemMaster.getFinder(whr, txtItem.Value, isButtonClicked)
            lblItem.Text = clsItemMaster.GetItemName(txtItem.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtItem.Value = ""
        End Try

    End Sub

    Function GetBaseQery(ByVal UserStaus As String, ByVal WhrCls As String) As String
        Dim qry As String = "select  cast(" + UserStaus + " as bit) as UserStatus, TSPL_GRN_HEAD.GRN_No,convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as GRN_Date,TSPL_GRN_HEAD.VehicleNo,isnull(TSPL_GRN_HEAD.Status,0) as GRNStatus,TSPL_SRN_HEAD.SRN_No,convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as  SRN_Date,isnull(TSPL_SRN_HEAD.Status,0) as SRNStatus, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight,TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,isnull(TSPL_PO_WEIGHTMENT_HEAD.Status,0) as WeightmentStatus,TSPL_SRN_DETAIL.SRN_Qty
,TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt as SecurityDeductionAmt,TSPL_SRN_DEDUCTION.Ded_Per as QualityDeductionPer,TSPL_SRN_DEDUCTION.Ded_Amt as QualityDeductionAmt,case when isnull(TSPL_SRN_TENDER_CALC.Penalty,0)=0 then null else TSPL_SRN_TENDER_CALC.Qty end as LatePenaltyQty,case when isnull(TSPL_SRN_TENDER_CALC.Penalty,0)=0 then null else TSPL_TENDER_SCHEDULE_PENALTY.Penalty end as LatePenaltyPer,TSPL_SRN_TENDER_CALC.Penalty as LatePenaltyAmt
,(case when isnull(TSPL_MRN_HEAD.NIR_QC,0)=0 then 1 else (case when (isnull(TSPL_NIR_QC.QC_Status,0)=1 and isnull(TSPL_NIR_QC.Status,0)=1 and TSPL_QC_CHECK_HEAD.Posted=1) then 1 else 0 end) end) as NIRQCStatus
," + UserStaus + " as FinalStatus
from TSPL_GRN_DETAIL
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No
left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No
left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id
left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No
left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code= TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code and  TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION_SECURITY.Item_Code=TSPL_SRN_DETAIL.Item_Code
left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION.Item_Code=TSPL_SRN_DETAIL.Item_Code
left outer join TSPL_SRN_TENDER_CALC on TSPL_SRN_TENDER_CALC.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_TENDER_CALC.Item_Code=TSPL_SRN_DETAIL.Item_Code and isnull(TSPL_SRN_TENDER_CALC.Penalty,0)>0
left outer join TSPL_TENDER_SCHEDULE_PENALTY on  TSPL_TENDER_SCHEDULE_PENALTY.PK_Id=TSPL_SRN_TENDER_CALC.Against_Tender_Schedule_Penalty_PK_Id
left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
where TSPL_PURCHASE_ORDER_HEAD.Against_Tender='Y' and TSPL_PURCHASE_ORDER_HEAD.RefTendorNo='" + txtTenderNo.Value + "' and  isnull(TSPL_QC_CHECK_HEAD.QC_Status,'')<>'Rejected'  and TSPL_GRN_DETAIL.Item_Code='" + txtItem.Value + "' and TSPL_GRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_GRN_HEAD.Bill_To_Location='" + txtBillToLocation.Value + "' and ISNULL( TSPL_GRN_HEAD.IsCancel,0)=0  
and 2= (case when isnull(TSPL_MRN_HEAD.NIR_QC,0)=1 then (case when isnull(TSPL_NIR_QC.QC_Status,0)=1 then 2 else 3 end) else 2 end) " + WhrCls
        qry += " Order by CONVERT(date, TSPL_GRN_HEAD.GRN_Date,103),isnull(TSPL_SRN_HEAD.Status,0) desc"
        Return qry
    End Function

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Calculate()
    End Sub

    Sub SetGridFormation(ByVal dt As DataTable)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("UserStatus").IsVisible = True
        gv1.Columns("UserStatus").Width = 30
        gv1.Columns("UserStatus").HeaderText = " "
        gv1.Columns("UserStatus").ReadOnly = False

        gv1.Columns("GRN_No").IsVisible = True
        gv1.Columns("GRN_No").Width = 120
        gv1.Columns("GRN_No").HeaderText = "GRN"

        gv1.Columns("GRN_Date").IsVisible = True
        gv1.Columns("GRN_Date").Width = 100
        gv1.Columns("GRN_Date").HeaderText = "GRN Date"

        gv1.Columns("VehicleNo").IsVisible = True
        gv1.Columns("VehicleNo").Width = 100
        gv1.Columns("VehicleNo").HeaderText = "Vehicle No"

        gv1.Columns("GRNStatus").IsVisible = False

        gv1.Columns("SRN_No").IsVisible = True
        gv1.Columns("SRN_No").Width = 120
        gv1.Columns("SRN_No").HeaderText = "SRN"

        gv1.Columns("SRN_Date").IsVisible = True
        gv1.Columns("SRN_Date").Width = 100
        gv1.Columns("SRN_Date").HeaderText = "SRN Date"

        gv1.Columns("SRNStatus").IsVisible = False

        gv1.Columns("Weighment_Code").IsVisible = True
        gv1.Columns("Weighment_Code").Width = 120
        gv1.Columns("Weighment_Code").HeaderText = "Weighemnt No"

        gv1.Columns("Weighment_Date").IsVisible = True
        gv1.Columns("Weighment_Date").Width = 100
        gv1.Columns("Weighment_Date").HeaderText = "Weighemnt Date"

        gv1.Columns("Gross_Weight").IsVisible = True
        gv1.Columns("Gross_Weight").Width = 100
        gv1.Columns("Gross_Weight").HeaderText = "Gross Weight"
        gv1.Columns("Gross_Weight").FormatString = "{0:n3}"

        gv1.Columns("Tare_Weight").IsVisible = True
        gv1.Columns("Tare_Weight").Width = 100
        gv1.Columns("Tare_Weight").HeaderText = "Tare Weight"
        gv1.Columns("Tare_Weight").FormatString = "{0:n3}"

        gv1.Columns("Extra_Weight").IsVisible = True
        gv1.Columns("Extra_Weight").Width = 100
        gv1.Columns("Extra_Weight").HeaderText = "Extra Weight"
        gv1.Columns("Extra_Weight").FormatString = "{0:n3}"

        gv1.Columns("UOM").IsVisible = True
        gv1.Columns("UOM").Width = 100
        gv1.Columns("UOM").HeaderText = "UOM"

        gv1.Columns("Net_Weight").IsVisible = True
        gv1.Columns("Net_Weight").Width = 100
        gv1.Columns("Net_Weight").HeaderText = "Net Weight"
        gv1.Columns("Net_Weight").FormatString = "{0:n3}"

        gv1.Columns("WeightmentStatus").IsVisible = False


        gv1.Columns("SRN_Qty").IsVisible = True
        gv1.Columns("SRN_Qty").Width = 100
        gv1.Columns("SRN_Qty").HeaderText = "SRN Accepted Qty"
        gv1.Columns("SRN_Qty").FormatString = "{0:n2}"

        gv1.Columns("SecurityDeductionAmt").IsVisible = True
        gv1.Columns("SecurityDeductionAmt").Width = 100
        gv1.Columns("SecurityDeductionAmt").HeaderText = "Security Deduction"
        gv1.Columns("SecurityDeductionAmt").FormatString = "{0:n2}"

        gv1.Columns("QualityDeductionPer").IsVisible = True
        gv1.Columns("QualityDeductionPer").Width = 100
        gv1.Columns("QualityDeductionPer").HeaderText = "Quality Deduction %"
        gv1.Columns("QualityDeductionPer").FormatString = "{0:n2}"

        gv1.Columns("QualityDeductionAmt").IsVisible = True
        gv1.Columns("QualityDeductionAmt").Width = 100
        gv1.Columns("QualityDeductionAmt").HeaderText = "Quality Deduction Amount"
        gv1.Columns("QualityDeductionAmt").FormatString = "{0:n2}"

        gv1.Columns("LatePenaltyQty").IsVisible = True
        gv1.Columns("LatePenaltyQty").Width = 100
        gv1.Columns("LatePenaltyQty").HeaderText = "Late Penalty Qty"
        gv1.Columns("LatePenaltyQty").FormatString = "{0:n2}"

        gv1.Columns("LatePenaltyPer").IsVisible = True
        gv1.Columns("LatePenaltyPer").Width = 100
        gv1.Columns("LatePenaltyPer").HeaderText = "Late Penalty %"
        gv1.Columns("LatePenaltyPer").FormatString = "{0:n2}"

        gv1.Columns("LatePenaltyAmt").IsVisible = True
        gv1.Columns("LatePenaltyAmt").Width = 100
        gv1.Columns("LatePenaltyAmt").HeaderText = "Late Penalty Amount"
        gv1.Columns("LatePenaltyAmt").FormatString = "{0:n2}"

        gv1.Columns("FinalStatus").IsVisible = False

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub Calculate()
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                txtBillToLocation.Focus()
                Throw New Exception("Please select " + txtBillToLocation.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                txtTenderNo.Focus()
                Throw New Exception("Please select " + txtTenderNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select " + txtVendorNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtItem.Value) <= 0 Then
                txtItem.Focus()
                Throw New Exception("Please select " + txtItem.MyLinkLable1.Text)
            End If
            Dim qry As String = "and not exists(select 1 from TSPL_PI_DETAIL where TSPL_PI_DETAIL.SRN_Id=TSPL_SRN_HEAD.SRN_No)
and not exists(select 1 from TSPL_TENDER_PENALTY_DETAIL where TSPL_TENDER_PENALTY_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_TENDER_PENALTY_DETAIL.Document_No not in ('" + txtDocNo.Value + "') ) "
            qry = GetBaseQery("0", qry)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrSRN As New ArrayList
            For ii As Integer = 0 To dt.Rows.Count - 1
                If Not arrSRN.Contains(clsCommon.myCstr(dt.Rows(ii)("SRN_No"))) Then
                    arrSRN.Add(clsCommon.myCstr(dt.Rows(ii)("SRN_No")))
                End If
            Next

            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsTenderPenalty.DeleteSRNDeduction(arrSRN, txtItem.Value, True, True, True, tran)
                arrSRN = New ArrayList
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myCDecimal(dt.Rows(ii)("SRNStatus")) = 1 AndAlso clsCommon.myCDecimal(dt.Rows(ii)("NIRQCStatus")) = 1 Then
                        If Not arrSRN.Contains(clsCommon.myCstr(dt.Rows(ii)("SRN_No"))) Then
                            clsSRNHead.GenerateSRNDeduction(clsCommon.myCstr(dt.Rows(ii)("SRN_No")), txtItem.Value, True, True, True, tran)
                            arrSRN.Add(clsCommon.myCstr(dt.Rows(ii)("SRN_No")))
                        End If
                    Else
                        Exit For
                    End If
                Next
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            End Try

            dt = clsDBFuncationality.GetDataTable(qry)
            For ii As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myCDecimal(dt.Rows(ii)("SRNStatus")) = 1 AndAlso clsCommon.myCDecimal(dt.Rows(ii)("NIRQCStatus")) = 1 Then
                    dt.Rows(ii)("FinalStatus") = 1
                Else
                    Exit For
                End If
            Next
            SetGridFormation(dt)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReCalculate()
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                txtBillToLocation.Focus()
                Throw New Exception("Please select " + txtBillToLocation.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                txtTenderNo.Focus()
                Throw New Exception("Please select " + txtTenderNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select " + txtVendorNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtItem.Value) <= 0 Then
                txtItem.Focus()
                Throw New Exception("Please select " + txtItem.MyLinkLable1.Text)
            End If
            Dim ServerDate As DateTime = clsCommon.GETSERVERDATE()
            Dim qry As String = "select Document_No from TSPL_TENDER_PENALTY 
where Location_Code='" + txtBillToLocation.Value + "' and  Tender_No='" + txtTenderNo.Value + "' and Vendor_Code='" + txtVendorNo.Value + "' 
and Item_Code ='" + txtItem.Value + "' and Status=1 order by Document_Date"
            Dim dtDoc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtDoc IsNot Nothing AndAlso dtDoc.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "There are [" + clsCommon.myCstr(dtDoc.Rows.Count) + "] Documents to recalculate " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarPercentShow()
                    Try
                        Dim dt As DataTable = Nothing
                        For idxDoc As Integer = 0 To dtDoc.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(idxDoc + 1, dtDoc.Rows.Count, "Recalculate [" + clsCommon.myCstr(dtDoc.Rows(idxDoc)("Document_No")) + "]")

                            qry = "select TSPL_PI_detail.PI_No from TSPL_PI_detail where  SRN_Id in ( select SRN_No from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + clsCommon.myCstr(dtDoc.Rows(idxDoc)("Document_No")) + "') group by TSPL_PI_detail.PI_No"
                            Dim dtPI As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                            If dtPI IsNot Nothing AndAlso dtPI.Rows.Count > 0 Then
                                For Each drPI As DataRow In dtPI.Rows
                                    qry = "select Document_No From TSPL_VENDOR_INVOICE_HEAD where RefDocType in('SCH-PNT') and RefDocNo='" + clsCommon.myCstr(drPI("PI_No")) + "' and not exists(select 1 from TSPL_VENDOR_INVOICE_HEAD as innerTab where innerTab.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code and innerTab.RefDocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No and innerTab.RefDocType='REV-SPT' and innerTab.Document_Type='C')"
                                    dt = clsDBFuncationality.GetDataTable(qry, tran)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        For Each dr As DataRow In dt.Rows
                                            qry = clsCommon.myCstr(dr("Document_No"))
                                            Dim objVendorInvHead As clsVedorInvoiceHead = clsVedorInvoiceHead.GetData(qry, "", tran)
                                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy")
                                            objVendorInvHead.Description = "Reverse of AP Debit Note Against Schedule Penalty [" + objVendorInvHead.Document_No + "]"
                                            objVendorInvHead.isDeduction = 0
                                            objVendorInvHead.Document_Type = "C"
                                            objVendorInvHead.RefDocType = "REV-SPT"
                                            objVendorInvHead.RefDocNo = objVendorInvHead.Document_No
                                            objVendorInvHead.Document_No = ""
                                            objVendorInvHead.SaveData(objVendorInvHead, True, tran)
                                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", tran)

                                            'clsVedorInvoiceHead.ReverseAndUnpost(qry, tran)
                                            'clsVedorInvoiceHead.DeleteData(qry, tran)
                                        Next
                                    End If
                                Next
                            End If
                            Dim arrSRN As New ArrayList
                            If idxDoc = 0 Then
                                arrSRN = New ArrayList
                                For Each dr As DataRow In dtDoc.Rows
                                    arrSRN.Add(dr("Document_No")) ''Add all purchase invoice
                                Next

                                qry = "select SRN_No from TSPL_TENDER_PENALTY_DETAIL where Document_No in (" + clsCommon.GetMulcallString(arrSRN) + ")"
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                arrSRN = New ArrayList
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt.Rows
                                        arrSRN.Add(dr("SRN_No"))
                                    Next
                                End If
                                clsTenderPenalty.DeleteSRNDeduction(arrSRN, txtItem.Value, False, False, True, tran)
                            End If


                            qry = " and TSPL_SRN_HEAD.SRN_No in ( select SRN_No from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + clsCommon.myCstr(dtDoc.Rows(idxDoc)("Document_No")) + "')"
                            qry = GetBaseQery("0", qry)
                            dt = clsDBFuncationality.GetDataTable(qry, tran)
                            arrSRN = New ArrayList
                            For ii As Integer = 0 To dt.Rows.Count - 1
                                If Not arrSRN.Contains(clsCommon.myCstr(dt.Rows(ii)("SRN_No"))) Then
                                    arrSRN.Add(clsCommon.myCstr(dt.Rows(ii)("SRN_No")))
                                End If
                            Next


                            arrSRN = New ArrayList
                            For ii As Integer = 0 To dt.Rows.Count - 1
                                If clsCommon.myCDecimal(dt.Rows(ii)("SRNStatus")) = 1 AndAlso clsCommon.myCDecimal(dt.Rows(ii)("NIRQCStatus")) = 1 Then
                                    If Not arrSRN.Contains(clsCommon.myCstr(dt.Rows(ii)("SRN_No"))) Then
                                        clsSRNHead.GenerateSRNDeduction(clsCommon.myCstr(dt.Rows(ii)("SRN_No")), txtItem.Value, False, False, True, tran)
                                        arrSRN.Add(clsCommon.myCstr(dt.Rows(ii)("SRN_No")))
                                    End If
                                Else
                                    Exit For
                                End If
                            Next

                            If dtPI IsNot Nothing AndAlso dtPI.Rows.Count > 0 Then
                                For Each drPI As DataRow In dtPI.Rows
                                    Dim objPI As clsPurchaseInvoiceHead = clsPurchaseInvoiceHead.GetData(clsCommon.myCstr(drPI("PI_No")), NavigatorType.Current, tran, "")
                                    objPI.PI_Date = ServerDate
                                    clsPurchaseInvoiceHead.RCDF1QCDed2SecDed3RALPenalty(objPI, False, False, True, True, tran)
                                Next
                            End If
                        Next
                        clsCommon.ProgressBarPercentHide()
                        tran.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Data recalculated successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    Catch ex As Exception
                        clsCommon.ProgressBarPercentHide()
                        tran.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            Else
                Throw New Exception("No document found to recalculate")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        ReCalculate()
    End Sub
End Class
