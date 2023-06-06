''Created By monika
''UD=Under Deviation
''Green= OK, Red= NOT OK, Yellow= UD(Under Deviation)
Imports common
Imports System.Data.SqlClient
Imports System.Text



Public Class FrmQualityCheckForSRN
    Inherits FrmMainTranScreen

#Region "variables"
    Dim AllowDeductionPers As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim isNewEntry As Boolean = True
    Dim ButtonToolTip As New ToolTip()
    Dim Errorcontrol As New clsErrorControl()

    Const colMRNSelect As String = "MRNSelect"
    Const colMRNLineNo As String = "MLineNo"
    Const colMRNRowType As String = "MRowType"
    Const colMRNItemCode As String = "MItemCode"
    Const colMRNItemName As String = "MItemName"
    Const colMRNDrawingNo As String = "MDrawingNo"
    Const colMRNPartNo As String = "MPartNo"
    Const colMRNItemUnit As String = "MItemUnit"
    Const colMRNQty As String = "MQty"
    Const colMRNDocNo As String = "MDocNo"
    Const colMRNDate As String = "MDate"
    Const colMRNVehicleNo As String = "MVehicleNo"
    Const colMRNDesc As String = "MDesc"
    Const colMRNDocType As String = "DocType"
    Const colGRNDocNo As String = "GDocNo"
    Const colGRNDate As String = "GRNDate"
    '===========================
    Const colLineNo As String = "LineNo"
    Const colMRNNo As String = "MRNNO"
    Const colPONo As String = "PONO"
    'Const colREQNo As String = "REQNO"
    Const colRowType As String = "RowType"
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colDrawingNo As String = "DrawingNo"
    Const colPartNo As String = "PartNo"
    Const colUnit As String = "Unit"
    Const colQty As String = "Qty"
    Const colOkQty As String = "OkQty"
    Const colRejQty As String = "RejQty"
    Const colRemarks As String = "Remraks"
    Const colSpecification As String = "Specification"
    Const colQualityStatus As String = "QualityStatus"
    Const colAdditionalRemarks As String = "colAdditionalRemarks"

    Public Array_Template As List(Of clsQualityCheckForSRNDetail) = Nothing
    Dim arrLoc As String = Nothing
    Public QC_Type As String = Nothing
    Public FORMTYPE As String = Nothing
    Public strDocumentCode As String = Nothing
    Dim Template_Remarks As String = Nothing
    Dim Template_Status As String = Nothing
    Dim SettItemWiseQualityCheckInGeneralPurchase As Boolean = False
#End Region

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String, ByVal QCType As String)
        InitializeComponent()
        FORMTYPE = formid
        QC_Type = QCType
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtBillToLocation.Value = obj.Default_LocCode
                lblBillToLocation.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
        btnTemplates.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub FrmQualityCheckForSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                btnNew.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.T AndAlso MyBase.isModifyFlag AndAlso btnTemplates.Enabled Then
                btnTemplates.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                clsERPFuncationality.closeForm(Me)
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FunReset()
        txtDocNo.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        TxtVendor_desc.Text = ""
        fndVendor_code.Value = ""
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtGENo.Text = ""
        txtGEDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtGEDate.Checked = False
        cboSRNType.SelectedValue = ""
        cboItemType.SelectedValue = ""
        txtRALNo.Text = ""
        gv_MRN.Rows.Clear()
        gv_MRN.Rows.AddNew()
        gv_MRN.MasterTemplate.FilterDescriptors.Clear()

        gv.Rows.Clear()
        gv.Rows.AddNew()

        txtDocNo.MyReadOnly = False
        fndVendor_code.Enabled = True
        txtBillToLocation.Enabled = True
        cboSRNType.Enabled = False
        cboItemType.Enabled = False

        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
        isNewEntry = True
        btnTemplates.Enabled = True

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        UsLock1.Status = ERPTransactionStatus.Pending
        btnSendEmail.Enabled = False
        txtAccept.Visible = False

        RadPageView1.SelectedPage = RadPageViewPage1
        txtDesc.Focus()
        txtDesc.Select()
        Array_Template = Nothing
    End Sub

    Private Sub FrmQualityCheckForSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        AllowDeductionPers = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDeductionPercentOnIncoming, clsFixedParameterCode.AllowDeductionPercentOnIncoming, Nothing)) = "1", True, False)
        SettItemWiseQualityCheckInGeneralPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1)

        LoadBlankItemGrid()
        LoadBlankMRNGrid()
        LoadSRNType()
        LoadItemType()
        FunReset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")

        txtBillToLocation.Enabled = False
        txtGENo.Enabled = False
        txtGEDate.Enabled = False

        If clsCommon.CompairString(QC_Type, "Incoming") = CompairStringResult.Equal Then
            MyLabel4.Text = "INCOMING QUALITY CHECK"
            RadGroupBox2.Text = "MRN Details"
        ElseIf clsCommon.CompairString(QC_Type, "Outgoing") = CompairStringResult.Equal Then
            MyLabel4.Text = "OUTGOING QUALITY CHECK"
        ElseIf clsCommon.CompairString(QC_Type, "InProcess") = CompairStringResult.Equal Then
            MyLabel4.Text = "IN-PROCESS QUALITY CHECK"
        End If

        If clsCommon.myLen(strDocumentCode) > 0 Then
            LoadData(strDocumentCode, NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadSRNType()
        cboSRNType.DataSource = Nothing
        cboSRNType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cboSRNType.DisplayMember = "Name"
        cboSRNType.ValueMember = "Code"
    End Sub

    Private Sub LoadItemType()
        cboItemType.DataSource = Nothing
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.DisplayMember = "Name"
        cboItemType.ValueMember = "Code"
    End Sub

    Private Sub LoadBlankMRNGrid()
        gv_MRN.Rows.Clear()
        gv_MRN.Columns.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()

        repoChk = New GridViewCheckBoxColumn()
        repoChk.FormatString = ""
        repoChk.HeaderText = "Select"
        repoChk.Name = colMRNSelect
        repoChk.Width = 60
        gv_MRN.MasterTemplate.Columns.Add(repoChk)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colMRNLineNo
        repoStr.Width = 70
        repoStr.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "MRN No."
        repoStr.Name = colMRNDocNo
        repoStr.Width = 130
        repoStr.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoDate = New GridViewDateTimeColumn()
        repoDate.FormatString = "{0:d}"
        repoDate.HeaderText = "MRN Date"
        repoDate.Name = colMRNDate
        repoDate.Width = 130
        repoDate.CustomFormat = True
        repoDate.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoDate)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Vehicle No."
        repoStr.Name = colMRNVehicleNo
        repoStr.Width = 130
        repoStr.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "MRN Description"
        repoStr.Name = colMRNDesc
        repoStr.Width = 250
        repoStr.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Type"
        repoStr.Name = colMRNRowType
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "MRN Type"
        repoStr.Name = colMRNDocType
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colMRNItemCode
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Description"
        repoStr.Name = colMRNItemName
        repoStr.Width = 230
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Drawing No"
        repoStr.Name = colMRNDrawingNo
        repoStr.Width = 230
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Part No"
        repoStr.Name = colMRNPartNo
        repoStr.Width = 230
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "UOM"
        repoStr.Name = colMRNItemUnit
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colMRNQty
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        repoInt.IsVisible = False
        gv_MRN.MasterTemplate.Columns.Add(repoInt)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "GRN No."
        repoStr.Name = colGRNDocNo
        repoStr.Width = 130
        repoStr.ReadOnly = True
        repoStr.IsVisible = objCommonVar.RCDFCFP
        gv_MRN.MasterTemplate.Columns.Add(repoStr)

        repoDate = New GridViewDateTimeColumn()
        repoDate.FormatString = "{0:d}"
        repoDate.HeaderText = "GRN Date"
        repoDate.Name = colGRNDate
        repoDate.Width = 130
        repoDate.CustomFormat = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = objCommonVar.RCDFCFP
        gv_MRN.MasterTemplate.Columns.Add(repoDate)

        gv_MRN.AllowAddNewRow = False
        gv_MRN.AllowDeleteRow = False
        gv_MRN.ShowGroupPanel = False
        gv_MRN.AllowColumnReorder = True
        gv_MRN.AllowRowReorder = False
        gv_MRN.EnableSorting = False
        gv_MRN.EnableFiltering = True
        gv_MRN.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_MRN.MasterTemplate.ShowRowHeaderColumn = False
        gv_MRN.TableElement.TableHeaderHeight = 40

        repoStr = Nothing
        repoInt = Nothing
        repoChk = Nothing
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv_MRN.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv_MRN.Columns.Count - 1 Step ii + 1
                        gv_MRN.Columns(ii).IsVisible = False
                        gv_MRN.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv_MRN.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub LoadBlankItemGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoCombo As New GridViewComboBoxColumn()

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colLineNo
        repoStr.Width = 70
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "PO Id"
        repoStr.Name = colPONo
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        'repoStr = New GridViewTextBoxColumn()
        'repoStr.FormatString = ""
        'repoStr.HeaderText = "Requisition Id"
        'repoStr.Name = colREQNo
        'repoStr.Width = 100
        'repoStr.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "MRN No."
        repoStr.Name = colMRNNo
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Type"
        repoStr.Name = colRowType
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colItemCode
        repoStr.Width = 100
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Description"
        repoStr.Name = colItemName
        repoStr.Width = 230
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "UOM"
        repoStr.Name = colUnit
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Drawing No"
        repoStr.Name = colDrawingNo
        repoStr.Width = 80
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Part No"
        repoStr.Name = colPartNo
        repoStr.Width = 80
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colQty
        repoInt.DecimalPlaces = 2
        repoInt.ReadOnly = True
        repoInt.Width = 80
        gv.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Ok Quantity"
        repoInt.Name = colOkQty
        repoInt.DecimalPlaces = 4
        repoInt.Width = 80
        repoInt.ReadOnly = AllowDeductionPers
        gv.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Reject Quantity"
        repoInt.Name = colRejQty
        repoInt.DecimalPlaces = 4
        repoInt.Width = 80
        repoInt.ReadOnly = AllowDeductionPers
        gv.MasterTemplate.Columns.Add(repoInt)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Reject Remarks"
        repoStr.Name = colSpecification
        repoStr.Width = 100
        repoStr.MaxLength = 200
        repoStr.TextAlignment = ContentAlignment.TopLeft
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Remarks"
        repoStr.Name = colRemarks
        repoStr.Width = 100
        repoStr.MaxLength = 200
        repoStr.TextAlignment = ContentAlignment.TopLeft
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Status"
        repoStr.Name = colQualityStatus
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.MaxLength = 300
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Additional Remarks"
        repoStr.Name = colAdditionalRemarks
        repoStr.Width = 500
        repoStr.ReadOnly = False
        repoStr.Multiline = True
        repoStr.MaxLength = 500
        repoStr.AcceptsReturn = True
        gv.MasterTemplate.Columns.Add(repoStr)


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40

        repoStr = Nothing
        repoInt = Nothing
        repoCombo = Nothing
    End Sub

    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select document no. for deletion.")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Select document no. for deletion.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If

            If myMessages.deleteConfirm() Then
                If clsQualityCheckForSRNHead.DeleteData(txtDocNo.Value, QC_Type) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select document no. for post.")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Select document no. for post.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If
            'If AllowToSave(True) Then
            If myMessages.postConfirm() Then
                If clsQualityCheckForSRNHead.PostData(txtDocNo.Value, QC_Type) Then
                    myMessages.post()
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsQualityCheckForSRNHead()
        Try
            FunReset()
            Dim whrcls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls += "  and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            obj = clsQualityCheckForSRNHead.GetData(strCode, QC_Type, whrcls, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtRALNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ref_No from TSPL_GRN_HEAD where GRN_No='" + obj.Gate_Entry_No + "'"))
                txtDocNo.Value = obj.Document_Code
                dtpDate.Text = obj.Document_Date
                txtDesc.Text = obj.Description
                fndVendor_code.Value = obj.Vendor_Code
                TxtVendor_desc.Text = obj.Vendor_Name
                txtBillToLocation.Value = obj.Bill_To_location
                lblBillToLocation.Text = obj.Location_Desc
                txtGENo.Text = obj.Gate_Entry_No
                If clsCommon.myLen(obj.Gate_Entry_No) > 0 Then
                    txtGEDate.Text = obj.Gate_Entry_Date
                    txtGEDate.Checked = True
                End If
                cboItemType.SelectedValue = obj.Item_Type
                cboSRNType.SelectedValue = obj.SRN_Type

                gv.Rows.Clear()
                gv_MRN.Rows.Clear()

                If obj.Arr_MRN IsNot Nothing AndAlso obj.Arr_MRN.Count > 0 Then
                    For Each objtr As clsQualityCheckForSRN_MRNDetail In obj.Arr_MRN
                        gv_MRN.Rows.AddNew()

                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNSelect).Value = IIf(objtr.Status = 1, True, False)
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNLineNo).Value = clsCommon.myCstr(objtr.Line_No)
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocNo).Value = objtr.MRN_No
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDate).Value = objtr.MRN_Date
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNVehicleNo).Value = objtr.VehicleNo
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDesc).Value = objtr.MRN_Desc
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(objtr.Row_Type)
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNItemCode).Value = objtr.Item_Code
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNItemName).Value = objtr.Item_Desc
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDrawingNo).Value = objtr.Drawing_No
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNPartNo).Value = objtr.Part_No
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNItemUnit).Value = objtr.Unit_Code
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNQty).Value = objtr.Qty
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocType).Value = clsQualityCheckForSRNHead.FullNameOfPurchaseOrderType(objtr.MRN_Type)
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colGRNDocNo).Value = clsCommon.myCstr(obj.Gate_Entry_No)
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colGRNDate).Value = clsCommon.myCstr(obj.Gate_Entry_Date)
                    Next
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsQualityCheckDetail In obj.Arr
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objtr.Line_No)
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRNNo).Value = objtr.MRN_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colPONo).Value = objtr.PO_No
                        'gv.Rows(gv.Rows.Count - 1).Cells(colREQNo).Value = objtr.REQ_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(objtr.Row_Type)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colDrawingNo).Value = objtr.Drawing_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colPartNo).Value = objtr.Part_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.MRN_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colOkQty).Value = objtr.Ok_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRejQty).Value = objtr.Reject_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colSpecification).Value = objtr.Reject_Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colQualityStatus).Value = objtr.QC_Status
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdditionalRemarks).Value = objtr.Additional_Remarks
                    Next
                End If


                UcAttachment1.LoadData(txtDocNo.Value)
                txtDocNo.MyReadOnly = True
                fndVendor_code.Enabled = False
                txtBillToLocation.Enabled = False
                cboSRNType.Enabled = False
                cboItemType.Enabled = False

                UsLock1.Status = ERPTransactionStatus.Pending
                btnSendEmail.Enabled = False
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnpost.Enabled = True
                txtAccept.Text = obj.QC_Status
                txtAccept.Visible = True

                If obj.Posted = 1 Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    'btnTemplates.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnSendEmail.Enabled = True
                End If

                txtDesc.Focus()
                txtDesc.Select()

                If clsCommon.CompairString(obj.QC_Status, "Accepted") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Green
                ElseIf clsCommon.CompairString(obj.QC_Status, "Under Deviation") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Yellow
                ElseIf clsCommon.CompairString(obj.QC_Status, "Rejected") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Red
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData(False)
    End Sub
    Private Function AllowToSave() As Boolean 'ByVal isPost As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                dtpDate.Focus()
                Return False
            End If
            If clsCommon.myLen(fndVendor_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                fndVendor_code.Focus()
                fndVendor_code.Select()
                clsCommon.MyMessageBoxShow("Select Vendor")
                Errorcontrol.SetError(TxtVendor_desc, "Select vendor detail")
                Return False
            Else
                Errorcontrol.ResetError(TxtVendor_desc)
            End If

            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtBillToLocation.Focus()
                txtBillToLocation.Select()
                clsCommon.MyMessageBoxShow("Select from location")
                Errorcontrol.SetError(lblBillToLocation, "Select from location")
                Return False
            Else
                Errorcontrol.ResetError(lblBillToLocation)
            End If

            If clsCommon.myLen(txtGENo.Text) > 0 AndAlso txtGEDate.Checked = False Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtGEDate.Focus()
                txtGEDate.Select()
                clsCommon.MyMessageBoxShow("Select gate entry date.")
                Errorcontrol.SetError(txtGEDate, "Select gate entry date.")
                Return False
            Else
                Errorcontrol.ResetError(txtGEDate)
            End If

            Dim arrMRN As New List(Of String)
            Dim arr As New List(Of String)

            Dim icode As String = ""
            Dim status As Integer = 0
            For ii As Integer = 0 To gv_MRN.Rows.Count - 1
                icode = clsCommon.myCstr(gv_MRN.Rows(ii).Cells(colMRNDocNo).Value)
                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myCBool(gv_MRN.Rows(ii).Cells(colMRNSelect).Value) = True Then
                    status += 1
                End If
                If clsCommon.myLen(icode) > 0 AndAlso Not arrMRN.Contains(icode) Then
                    arrMRN.Add(icode)
                End If
            Next

            If arrMRN.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow("No MRN details found for QC process.")
                Return False
            End If

            Dim qty As Decimal = Nothing
            Dim netqty As Decimal = Nothing
            Dim countr1 As Integer = 0
            Dim countr2 As Integer = 0
            Dim qc_status As String = Nothing
            For Each grow As GridViewRowInfo In gv.Rows
                icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                qc_status = clsCommon.myCstr(grow.Cells(colQualityStatus).Value)
                netqty = clsCommon.myCdbl(grow.Cells(colOkQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If Not arr.Contains(icode) Then
                        arr.Add(icode)
                    End If

                    If clsCommon.myLen(qc_status) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(grow.Index)
                        Throw New Exception("Fill Quality Templates at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If SettItemWiseQualityCheckInGeneralPurchase = True Then 'AndAlso isPost = False
                    Else
                        If qty <> netqty Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            Throw New Exception("sum of Ok and Reject quantity should be equal to " + clsCommon.myCstr(netqty) + " at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myCdbl(grow.Cells(colRejQty).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colSpecification).Value) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(grow.Index)
                        Throw New Exception("Fill rejection remarks at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If 'rej rem.

                End If
            Next

            If arr.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow("No Item details found for QC process.")
                Return False
            End If
            If Not SettItemWiseQualityCheckInGeneralPurchase Then
                '' added funcionality 13/10/2017
                If clsCommon.CompairString(txtAccept.Text, "Under Deviation") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Not able to save, Item is Under Deviation")
                    Return False
                End If
                '' End functionality
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsQualityCheckForSRNHead()
        Dim obj_MRN As New clsQualityCheckForSRN_MRNDetail()
        Dim objtr As New clsQualityCheckDetail()
        Try

            If AllowToSave() Then 'False
                obj.Document_Code = clsCommon.myCstr(txtDocNo.Value)
                obj.QC_Type = QC_Type
                obj.Document_Date = clsCommon.myCDate(dtpDate.Text)
                obj.Description = clsCommon.myCstr(txtDesc.Text)
                obj.Vendor_Code = clsCommon.myCstr(fndVendor_code.Value)
                obj.Bill_To_location = clsCommon.myCstr(txtBillToLocation.Value)
                obj.Gate_Entry_No = clsCommon.myCstr(txtGENo.Text)
                obj.Gate_Entry_Date = Nothing
                If clsCommon.myLen(obj.Gate_Entry_No) > 0 Then
                    obj.Gate_Entry_Date = clsCommon.myCDate(txtGEDate.Text)
                End If
                obj.SRN_Type = clsCommon.myCstr(cboSRNType.SelectedValue)
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.QC_Status = clsCommon.myCstr(txtAccept.Text)
                obj.Template_Status = Template_Status
                obj.Template_Remarks = Template_Remarks

                obj.Arr_MRN = New List(Of clsQualityCheckForSRN_MRNDetail)
                obj.Arr_item = New List(Of clsQualityCheckForSRNDetail)
                obj.Arr = New List(Of clsQualityCheckDetail)

                For Each grow As GridViewRowInfo In gv_MRN.Rows
                    obj_MRN = New clsQualityCheckForSRN_MRNDetail()

                    obj_MRN.Status = IIf(clsCommon.myCBool(grow.Cells(colMRNSelect).Value) = True, 1, 0)
                    obj_MRN.Line_No = CInt(clsCommon.myCstr(grow.Cells(colMRNLineNo).Value))
                    obj_MRN.MRN_No = clsCommon.myCstr(grow.Cells(colMRNDocNo).Value)
                    obj_MRN.Row_Type = clsQualityCheckForSRNHead.CodeOfItemType(clsCommon.myCstr(grow.Cells(colMRNRowType).Value))
                    obj_MRN.Item_Code = clsCommon.myCstr(grow.Cells(colMRNItemCode).Value)
                    obj_MRN.Unit_Code = clsCommon.myCstr(grow.Cells(colMRNItemUnit).Value)
                    obj_MRN.Qty = clsCommon.myCdbl(grow.Cells(colMRNQty).Value)
                    obj_MRN.MRN_Type = clsQualityCheckForSRNHead.CodeOfPurchaseOrderType(clsCommon.myCstr(grow.Cells(colMRNDocType).Value))

                    If clsCommon.myLen(obj_MRN.MRN_No) > 0 AndAlso obj_MRN.Status = 1 Then
                        obj.Arr_MRN.Add(obj_MRN)
                    End If
                Next

                For Each grow As GridViewRowInfo In gv.Rows
                    objtr = New clsQualityCheckDetail()

                    objtr.Line_No = CInt(clsCommon.myCstr(grow.Cells(colLineNo).Value))
                    objtr.MRN_No = clsCommon.myCstr(grow.Cells(colMRNNo).Value)
                    objtr.PO_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    'objtr.REQ_No = clsCommon.myCstr(grow.Cells(colREQNo).Value)
                    objtr.Row_Type = clsQualityCheckForSRNHead.CodeOfItemType(clsCommon.myCstr(grow.Cells(colRowType).Value))
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objtr.MRN_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objtr.Ok_Qty = clsCommon.myCdbl(grow.Cells(colOkQty).Value)
                    objtr.Reject_Qty = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                    objtr.Reject_Remarks = clsCommon.myCstr(grow.Cells(colSpecification).Value).Replace("'", "`")
                    objtr.QC_Status = clsCommon.myCstr(grow.Cells(colQualityStatus).Value).Replace("'", "`")
                    objtr.Additional_Remarks = clsCommon.myCstr(grow.Cells(colAdditionalRemarks).Value).Replace("'", "`")

                    If clsCommon.myLen(objtr.Item_Code) > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next

                obj.Arr_item = Array_Template ''come from template screen

                If SettItemWiseQualityCheckInGeneralPurchase = True AndAlso isNewEntry = False AndAlso (obj.Arr_item Is Nothing OrElse obj.Arr_item.Count <= 0) Then
                    Dim objTemp As New clsQualityCheckForSRNHead()
                    objTemp = clsQualityCheckForSRNHead.GetData(txtDocNo.Value, QC_Type, NavigatorType.Current)
                    If objTemp IsNot Nothing AndAlso clsCommon.myLen(objTemp.Document_Code) > 0 Then
                        If objTemp.Arr_item IsNot Nothing AndAlso objTemp.Arr_item.Count > 0 Then
                            obj.Arr_item = objTemp.Arr_item
                        End If
                    End If
                    objTemp = Nothing
                End If

                If obj.Arr_item Is Nothing OrElse obj.Arr_item.Count <= 0 Then
                    Throw New Exception("No record found for save.")
                End If

                If SettItemWiseQualityCheckInGeneralPurchase = True Then
                    For i As Integer = 0 To obj.Arr_item.Count - 1
                        obj.Arr_item(i).Ok_Qty = clsCommon.myCdbl(gv.Rows(0).Cells(colOkQty).Value)
                        obj.Arr_item(i).Reject_Qty = clsCommon.myCdbl(gv.Rows(0).Cells(colRejQty).Value)
                    Next
                End If

                If clsQualityCheckForSRNHead.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")
                    End If

                    txtDocNo.Value = obj.Document_Code

                    UcAttachment1.SaveData(txtDocNo.Value)

                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj_MRN = Nothing
            objtr = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select count(*) from TSPL_QC_CHECK_HEAD where document_code='" + txtDocNo.Value + "' and qc_type='" + QC_Type + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim whrcls As String = " TSPL_QC_CHECK_HEAD.qc_type='" + QC_Type + "' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += "  and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtDocNo.MyReadOnly = False
        If check > 0 Then
            txtDocNo.MyReadOnly = True
        End If

        If txtDocNo.MyReadOnly OrElse isButtonClicked Then
            txtDocNo.Value = clsQualityCheckForSRNHead.Getfinder(whrcls, txtDocNo.Value, isButtonClicked)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub fndVendor_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor_code._MYValidating
        fndVendor_code.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N'", fndVendor_code.Value, isButtonClicked)
        TxtVendor_desc.Text = clsVendorMaster.GetName(fndVendor_code.Value, Nothing)
        FillMRNGrid("")
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim whrcls As String = ""
        'If clsCommon.myLen(arrLoc) > 0 Then
        '    whrcls = " location_code in (" + arrLoc + ")"
        'End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtBillToLocation.Value = clsLocation.getFinder(whrcls, txtBillToLocation.Value, isButtonClicked)
        lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)
        FillMRNGrid("")
    End Sub

    Private Sub FillMRNGrid(ByVal strMRN As String)
        Dim dt As New DataTable()
        Dim Cmpr_SRN_Type As String = ""
        Dim Cmpr_Item_Type As String = ""
        Try
            gv.Rows.Clear()
            gv_MRN.Rows.Clear()

            Dim qry As String = "select distinct MRN_No as MRN_No,MAX(GRN_No) as GRN_No,max(MRN_Date) as MRN_Date,Max(Grn_date) as GRN_date from ( select Final1.* from (" + Environment.NewLine &
                      " select pod.Bin_No,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1 "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
                qry += "  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) and TSPL_MRN_HEAD.IsCancel=0 "
            End If

            If clsCommon.myLen(fndVendor_code.Value) > 0 Then
                qry += " and TSPL_MRN_Head.Vendor_Code='" + fndVendor_code.Value + "'" + Environment.NewLine
            End If
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += " and TSPL_MRN_Head.bill_to_location='" + txtBillToLocation.Value + "' " + Environment.NewLine
            End If

            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_MRN_Head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            If clsCommon.myLen(strMRN) > 0 Then
                qry += " and TSPL_MRN_HEAD.MRN_No='" + strMRN + "'"
            End If
            qry += " union all" + Environment.NewLine &
            " select null as Bin_No,vendor_code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0  " + Environment.NewLine &
            " union all  " + Environment.NewLine &
            " select null as Bin_No,vendor_code as Vendor,TSPL_QC_CHECK_DETAIL.Item_Code as ICode,'' as IType,0 as Qty,(isnull(TSPL_QC_CHECK_DETAIL.OK_Qty,0)+isnull(TSPL_QC_CHECK_DETAIL.Reject_Qty,0)) as Unapproved,TSPL_QC_CHECK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,TSPL_QC_CHECK_DETAIL.MRN_No as MRN_No,'',null,null from TSPL_QC_CHECK_DETAIL left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_DETAIL.document_code where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and len(isnull(TSPL_QC_CHECK_DETAIL.MRN_No,''))>0 and TSPL_QC_CHECK_HEAD.IsCancel=0  " + Environment.NewLine &
            " union all  " + Environment.NewLine &
            " select null as Bin_No,vendor_code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " + Environment.NewLine &
            " )Final1 where 2=2 "
            If SettItemWiseQualityCheckInGeneralPurchase Then
                qry += " and final1.icode in (select distinct TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER) "
            Else
                qry += " and final1.icode in (select distinct TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Item_Code from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.Document_Code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Document_Code where TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code=Final1.vendor) "
            End If
            qry += " )Final group by MRN_No,ICode,Unit,MRP having SUM(Chk)>0  "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                qry = qry & " and MRN_No not in (select distinct MRN_No from TSPL_QC_CHECK_DETAIL ) "
                qry = qry & " and MRN_No not in (select distinct MRN_Id from TSPL_SRN_DETAIL where isnull(MRN_Id,'')<>'')  "
            Else
                qry += " and SUM((Qty *RI)-Unapproved-DamageQty) <>0 "
            End If
            qry = qry & " order by MRN_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isInsideLoadData = True
                For Each dr As DataRow In dt.Rows
                    Cmpr_SRN_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PurchaseOrder_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))
                    Cmpr_Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))

                    ''cond. add so that same srn type and same item type mrn detail fill at same time
                    '' UDL Company change concept
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        gv_MRN.Rows.AddNew()

                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNSelect).Value = False
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNLineNo).Value = gv_MRN.Rows.Count
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'")))
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocNo).Value = clsCommon.myCstr(dr("MRN_No"))
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDate).Value = clsCommon.myCstr(dr("MRN_Date"))
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNVehicleNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicleno from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))
                        gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocType).Value = clsQualityCheckForSRNHead.FullNameOfPurchaseOrderType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PurchaseOrder_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'")))

                        If clsCommon.myLen(txtGENo.Text) <= 0 Then
                            txtGENo.Text = clsCommon.myCstr(dr("GRN_No"))
                            txtGEDate.Text = clsCommon.myCDate(dr("GRN_date"))
                        End If
                        If clsCommon.myLen(txtGENo.Text) > 0 Then
                            txtGEDate.Checked = True
                        End If
                        If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                            txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bill_to_location from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("mrn_no")) + "'"))
                            lblBillToLocation.Text = clsCommon.myCstr(clsLocation.GetName(txtBillToLocation.Value, Nothing))
                        End If
                        If clsCommon.CompairString(cboSRNType.SelectedValue, "") = CompairStringResult.Equal Then
                            cboSRNType.SelectedValue = Cmpr_SRN_Type
                        End If
                        If clsCommon.CompairString(cboItemType.SelectedValue, "") = CompairStringResult.Equal Then
                            cboItemType.SelectedValue = Cmpr_Item_Type
                        End If
                    Else
                        If (clsCommon.CompairString(cboSRNType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboSRNType.SelectedValue, Cmpr_SRN_Type) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(cboItemType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, Cmpr_Item_Type) = CompairStringResult.Equal) Then
                            gv_MRN.Rows.AddNew()

                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNSelect).Value = False
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNLineNo).Value = gv_MRN.Rows.Count
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'")))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocNo).Value = clsCommon.myCstr(dr("MRN_No"))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDate).Value = clsCommon.myCstr(dr("MRN_Date"))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNVehicleNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicleno from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'"))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colMRNDocType).Value = clsQualityCheckForSRNHead.FullNameOfPurchaseOrderType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PurchaseOrder_Type from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("MRN_No")) + "'")))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colGRNDocNo).Value = clsCommon.myCstr(dr("GRN_No"))
                            gv_MRN.Rows(gv_MRN.Rows.Count - 1).Cells(colGRNDate).Value = clsCommon.myCstr(dr("GRN_date"))
                            If clsCommon.myLen(txtGENo.Text) <= 0 Then
                                txtGENo.Text = clsCommon.myCstr(dr("GRN_No"))
                                txtGEDate.Text = clsCommon.myCDate(dr("GRN_date"))
                            End If
                            If clsCommon.myLen(txtGENo.Text) > 0 Then
                                txtGEDate.Checked = True
                            End If
                            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                                txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bill_to_location from tspl_mrn_head where mrn_no='" + clsCommon.myCstr(dr("mrn_no")) + "'"))
                                lblBillToLocation.Text = clsCommon.myCstr(clsLocation.GetName(txtBillToLocation.Value, Nothing))
                            End If
                            If clsCommon.CompairString(cboSRNType.SelectedValue, "") = CompairStringResult.Equal Then
                                cboSRNType.SelectedValue = Cmpr_SRN_Type
                            End If
                            If clsCommon.CompairString(cboItemType.SelectedValue, "") = CompairStringResult.Equal Then
                                cboItemType.SelectedValue = Cmpr_Item_Type
                            End If
                        End If ''compr cond
                    End If



                Next

            End If ''dt cond.
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            dt = Nothing
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    Dim countr1 As Integer = 0
                    Dim countr2 As Integer = 0
                    Dim decimalvalue As Decimal = 0

                    If e.Column Is gv.Columns(colOkQty) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colOkQty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) Then
                            gv.CurrentRow.Cells(colOkQty).Value = 0
                            gv.CurrentRow.Cells(colRejQty).Value = 0
                            Throw New Exception("Ok quantity cannot be greater than MRN quantity i.e. (" + clsCommon.myCstr(gv.CurrentRow.Cells(colQty).Value) + ")")
                        End If
                        gv.CurrentRow.Cells(colRejQty).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colOkQty).Value)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colRejQty) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colRejQty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) Then
                            gv.CurrentRow.Cells(colOkQty).Value = 0
                            gv.CurrentRow.Cells(colRejQty).Value = 0
                            Throw New Exception("Reject quantity cannot be greater than MRN quantity i.e. (" + clsCommon.myCstr(gv.CurrentRow.Cells(colQty).Value) + ")")
                        End If
                        gv.CurrentRow.Cells(colOkQty).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colRejQty).Value)
                        isCellValueChanged = False
                    End If

                End If ''cellchanged
            End If ''loaddata
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_MRN_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv_MRN.ValueChanging
        Dim dt As New DataTable()
        Try
            If isInsideLoadData Then
                Exit Sub
            End If
            If gv_MRN.CurrentRow.Index = -1 Then
                Exit Sub
            End If

            If e.NewValue Then

                If SettItemWiseQualityCheckInGeneralPurchase = True Then
                    Dim Tempselect As Boolean = False
                    For Each grow As GridViewRowInfo In gv_MRN.Rows
                        If (clsCommon.myCBool(grow.Cells(colMRNSelect).Value) = True) Then
                            Tempselect = True
                        End If
                    Next
                    If Tempselect = True Then
                        e.Cancel = True
                        Throw New Exception("Select one document at a time.")
                    End If
                End If

                Dim icode As String = ""
                Dim arrIcode As New List(Of String)
                arrIcode = GetPendingMRNItems(clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNDocNo).Value))

                Dim qry As String = ""
                If SettItemWiseQualityCheckInGeneralPurchase Then
                    qry = "select distinct TSPL_MRN_DETAIL.Po_Id, TSPL_MRN_DETAIL.item_code,tspl_item_master.item_desc,tspl_item_master.drawing_no,tspl_item_master.part_no,tspl_item_master.item_type " + Environment.NewLine +
                    "from TSPL_MRN_DETAIL" + Environment.NewLine +
                    "left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MRN_DETAIL.Item_Code " + Environment.NewLine +
                    "inner join (select distinct TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code in (" + clsCommon.GetMulcallString(arrIcode) + ")) as QCTable on  QCTable.item_code=TSPL_MRN_DETAIL.Item_Code" + Environment.NewLine +
                    "where TSPL_MRN_DETAIL.MRN_No='" + clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNDocNo).Value) + "'"
                Else
                    qry = "select distinct TSPL_MRN_DETAIL.Po_Id, TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code,tspl_item_master.item_desc,tspl_item_master.drawing_no,tspl_item_master.part_no,tspl_item_master.item_type from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.document_code"
                    qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code  where TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code in (" + clsCommon.GetMulcallString(arrIcode) + ") and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code='" + fndVendor_code.Value + "' and TSPL_MRN_DETAIL.MRN_No='" + clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNDocNo).Value) + "'"

                End If
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows 'if item find for testing in vendor-item mapping screen then fill qc here
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = gv.Rows.Count
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRNNo).Value = clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNDocNo).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("item_code"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PO_Id"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PO_Id"))
                        icode = clsCommon.myCstr(dr("item_code"))

                        gv.Rows(gv.Rows.Count - 1).Cells(colRowType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(clsCommon.myCstr(dr("item_type")))
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(dr("item_desc"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colDrawingNo).Value = clsCommon.myCstr(dr("drawing_no"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colPartNo).Value = clsCommon.myCstr(dr("part_no"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(GetPendingMRNUnit(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colMRNNo).Value), icode, clsCommon.myCstr(dr("PO_Id"))))
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(GetPendingMRNQty(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colMRNNo).Value), icode, clsCommon.myCstr(dr("PO_Id"))))
                    Next

                End If 'dt cond.
            Else
                For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMRNNo).Value), clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNDocNo).Value)) = CompairStringResult.Equal Then 'AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value), clsCommon.myCstr(gv_MRN.CurrentRow.Cells(colMRNItemCode).Value)) = CompairStringResult.Equal
                        gv.Rows.RemoveAt(ii)
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            dt = Nothing
            RefreshLineNo()
        End Try
    End Sub

    Private Function GetPendingMRNUnit(ByVal MRN_No As String, ByVal Item_Code As String, ByVal PO_Id As String) As String
        Dim qry As String = "select (unit) as unit,mrn_no from ( " + Environment.NewLine &
                      " select pod.Bin_No,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) and coalesce(TSPL_MRN_DETAIL.PO_ID,'')='" + PO_Id + "'"
        If clsCommon.myLen(fndVendor_code.Value) > 0 Then
            qry += " and TSPL_MRN_Head.Vendor_Code='" + fndVendor_code.Value + "'" + Environment.NewLine
        End If
        qry += " union all" + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_QC_CHECK_DETAIL.Item_Code as ICode,'' as IType,0 as Qty,(isnull(TSPL_QC_CHECK_DETAIL.OK_Qty,0)+isnull(TSPL_QC_CHECK_DETAIL.Reject_Qty,0)) as Unapproved,TSPL_QC_CHECK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,TSPL_QC_CHECK_DETAIL.MRN_No as MRN_No,'',null,null from TSPL_QC_CHECK_DETAIL left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_DETAIL.document_code where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and len(isnull(TSPL_QC_CHECK_DETAIL.MRN_No,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " + Environment.NewLine &
        " )Final GROUP BY MRN_NO,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 and icode='" + Item_Code + "' and mrn_no='" + MRN_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim unit As String = ""

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            unit = clsCommon.myCstr(dt.Rows(0)("unit"))
        End If
        If clsCommon.myLen(unit) <= 0 Then
            qry = "select max(Unit_code) as Unit_code  from TSPL_MRN_DETAIL where MRN_No='" & MRN_No & "' and Item_Code='" & Item_Code & "' and PO_ID='" & PO_Id & "'"
            unit = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        End If

        Return unit
    End Function

    Private Function GetPendingMRNQty(ByVal MRN_No As String, ByVal Item_Code As String, ByVal PO_Id As String) As Double
        Dim qry As String = "select SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty,mrn_no from ( " + Environment.NewLine &
                      " select distinct pod.Bin_No,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) and coalesce(TSPL_MRN_DETAIL.PO_ID,'')='" + PO_Id + "'"
        If clsCommon.myLen(fndVendor_code.Value) > 0 Then
            qry += " and TSPL_MRN_Head.Vendor_Code='" + fndVendor_code.Value + "'" + Environment.NewLine
        End If
        qry += " union all" + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_QC_CHECK_DETAIL.Item_Code as ICode,'' as IType,0 as Qty,(isnull(TSPL_QC_CHECK_DETAIL.OK_Qty,0)+isnull(TSPL_QC_CHECK_DETAIL.Reject_Qty,0)) as Unapproved,TSPL_QC_CHECK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,TSPL_QC_CHECK_DETAIL.MRN_No as MRN_No,'',null,null from TSPL_QC_CHECK_DETAIL left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_DETAIL.document_code where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and len(isnull(TSPL_QC_CHECK_DETAIL.MRN_No,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " + Environment.NewLine &
        " )Final GROUP BY MRN_NO,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 and icode='" + Item_Code + "' and mrn_no='" + MRN_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim qty As Decimal = 0

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qty = clsCommon.myCdbl(dt.Rows(0)("PedningQty"))
        End If

        Return qty
    End Function

    Private Function GetPendingMRNItems(ByVal MRN_No As String) As List(Of String)
        Dim str As New List(Of String)
        Dim qry As String = "select (icode) as icode,mrn_no from ( " + Environment.NewLine &
                      " select pod.Bin_No,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1 "
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
            qry += "  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) "
        End If

        If clsCommon.myLen(fndVendor_code.Value) > 0 Then
            qry += " and TSPL_MRN_Head.Vendor_Code='" + fndVendor_code.Value + "'" + Environment.NewLine
        End If
        qry += " union all" + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_QC_CHECK_DETAIL.Item_Code as ICode,'' as IType,0 as Qty,(isnull(TSPL_QC_CHECK_DETAIL.OK_Qty,0)+isnull(TSPL_QC_CHECK_DETAIL.Reject_Qty,0)) as Unapproved,TSPL_QC_CHECK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,TSPL_QC_CHECK_DETAIL.MRN_No as MRN_No,'',null,null from TSPL_QC_CHECK_DETAIL left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_DETAIL.document_code where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and len(isnull(TSPL_QC_CHECK_DETAIL.MRN_No,''))>0  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select null as Bin_No,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " + Environment.NewLine &
        " )Final GROUP BY MRN_NO,ICode,Unit,MRP having SUM(Chk)>0 "
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
            qry += " and SUM((Qty *RI)-Unapproved-DamageQty) <>0 "
        End If
        qry += " and mrn_no in ('" + MRN_No + "') "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                str.Add(dr("icode"))
            Next
        End If

        Return str
    End Function

    Private Sub RefreshLineNo()
        For Each grow As GridViewRowInfo In gv.Rows
            grow.Cells(colLineNo).Value = grow.Index + 1
        Next
    End Sub

    Private Function GetParameterQry(ByVal Icode As String) As DataTable
        Dim qry As String = ""
        If SettItemWiseQualityCheckInGeneralPurchase Then
            qry = "select TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code as parameter_code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code,tspl_item_master.item_desc  as item_desc, tspl_item_master.drawing_no ,tspl_item_master.part_no , tspl_item_master.item_type , TSPL_QC_LOG_SHEET_MASTER.description as [description],'' as lower_range,'' as upper_range
            , (convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3)) as status
            ,'' as value1
            ,(convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2) +','
            +convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3) + '-'+convert(varchar,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3)) as qc_status,0 as Deduction_Per,case when TSPL_PARAMETER_MAPPING_QC.QC_Param_Code is null then 1 else 0 end as Mandatory " + Environment.NewLine +
            "from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER" + Environment.NewLine +
            "left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code" + Environment.NewLine +
            "left outer join tspl_item_master on tspl_item_master.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code 
              left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code
              and tspl_item_master.item_code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code
              left outer join TSPL_PARAMETER_MAPPING_QC on TSPL_PARAMETER_MAPPING_QC.Mapped_QC_Param_Code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code
              where TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code in ('" + Icode + "')    
             order by TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO"
            'and (isnull(TSPL_PARAMETER_RANGE_MASTER_QC.lower_range,0)<>0 or isnull(TSPL_PARAMETER_RANGE_MASTER_QC.upper_range,0)<>0)
        Else
            qry = "select TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code,TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code,max(tspl_item_master.item_desc) as item_desc,max(tspl_item_master.drawing_no) as drawing_no,max(tspl_item_master.part_no) as part_no,max(tspl_item_master.item_type) as item_type,max(TSPL_QC_LOG_SHEET_MASTER.description) as [description],max(TSPL_PARAMETER_RANGE_MASTER_QC.lower_range) as lower_range,max(TSPL_PARAMETER_RANGE_MASTER_QC.upper_range) as upper_range,max(TSPL_PARAMETER_RANGE_MASTER_QC.status) as status,max(TSPL_PARAMETER_RANGE_MASTER_QC.value1) as value1,max(TSPL_PARAMETER_RANGE_MASTER_QC.qc_status) as qc_status,max(TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Per) as Deduction_Per,1 as Mandatory from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code and TSPL_PARAMETER_RANGE_MASTER_QC.trans_id='standard' left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.document_code"
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code where TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code in ('" + Icode + "') and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code='" + fndVendor_code.Value + "' group by TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code,TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code"
        End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function

    Private Sub btnTemplates_Click(sender As Object, e As EventArgs) Handles btnTemplates.Click
        Try
            Dim arrIcode As New List(Of String)
            arrIcode = New List(Of String)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim Icode1 As String = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                If clsCommon.myLen(Icode1) > 0 Then
                    If Not arrIcode.Contains(Icode1) Then
                        arrIcode.Add(Icode1)
                    End If

                    If SettItemWiseQualityCheckInGeneralPurchase = True Then
                    Else
                        ''================when deduction setting is ON then rej. and ok qty fill from template screen.
                        If Not AllowDeductionPers AndAlso clsCommon.myCdbl(grow.Cells(colOkQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value) <> clsCommon.myCdbl(grow.Cells(colQty).Value) Then
                            Throw New Exception("Fill ok and reject quantity at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myCdbl(grow.Cells(colRejQty).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colSpecification).Value) <= 0 Then
                        Throw New Exception("Fill reject remarks at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                End If

            Next
            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                Throw New Exception("No record found.")
            End If


            Dim frm As New frmQCTemplateEntry()
            frm.SettItemWiseQualityCheckInGeneralPurchase = SettItemWiseQualityCheckInGeneralPurchase
            frm.strVendorCode = fndVendor_code.Value
            frm.FORMTYPE = FORMTYPE
            frm.QC_Type = QC_Type
            frm.AllowDeductionPers = AllowDeductionPers
            frm.LoadEvent()
            frm.LoadData(txtDocNo.Value, NavigatorType.Current)
            frm.txtDocNo.Value = txtDocNo.Value
            frm.dtpDate.Text = dtpDate.Text

            frm.txtAccept.Text = txtAccept.Text
            Dim CHIcode As String = ""
            Dim CHMRN_NO As String = ""
            Dim CHPO_Id As String = ""
            Dim Icode As String = ""
            Dim MRN_NO As String = ""
            Dim PO_Id As String = ""
            Dim Match_Counter As Integer = 0

            frm.isInsideLoadData = True
            For Each grow As GridViewRowInfo In gv.Rows ''current screen grid
                Icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                MRN_NO = clsCommon.myCstr(grow.Cells(colMRNNo).Value)
                PO_Id = clsCommon.myCstr(grow.Cells(colPONo).Value)

                Match_Counter = 0
                For grow_ch As Integer = 0 To frm.gv.Rows.Count - 1 ''child screen grid
                    CHIcode = clsCommon.myCstr(frm.gv.Rows(grow_ch).Cells("ItemCode").Value)
                    CHMRN_NO = clsCommon.myCstr(frm.gv.Rows(grow_ch).Cells("MRNNo").Value)
                    CHPO_Id = clsCommon.myCstr(frm.gv.Rows(grow_ch).Cells("PONO").Value)

                    If clsCommon.CompairString(Icode, CHIcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(MRN_NO, CHMRN_NO) = CompairStringResult.Equal AndAlso clsCommon.CompairString(PO_Id, CHPO_Id) = CompairStringResult.Equal Then
                        Match_Counter += 1
                    End If
                Next ''child grid loop

                If Match_Counter <= 0 Then
                    Dim dt As DataTable = GetParameterQry(Icode)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows 'if item find for testing in vendor-item mapping screen then fill qc here
                            frm.gv.Rows.AddNew()

                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("LineNo").Value = frm.gv.Rows.Count
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("MRNNo").Value = MRN_NO
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("PONo").Value = PO_Id
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("ItemCode").Value = clsCommon.myCstr(dr("item_code"))

                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("RowType").Value = clsQualityCheckForSRNHead.FullNameOfItemType(clsCommon.myCstr(dr("item_type")))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("ItemName").Value = clsCommon.myCstr(dr("item_desc"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("DrawingNo").Value = clsCommon.myCstr(dr("drawing_no"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("PartNo").Value = clsCommon.myCstr(dr("part_no"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Unit").Value = clsCommon.myCstr(grow.Cells(colUnit).Value)
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Qty").Value = clsCommon.myCdbl(grow.Cells(colQty).Value)

                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("ParamCode").Value = clsCommon.myCstr(dr("parameter_code"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("ParamDesc").Value = clsCommon.myCstr(dr("description"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Lrange").Value = clsCommon.myCdbl(dr("lower_range"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("URange").Value = clsCommon.myCdbl(dr("upper_Range"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Value").Value = clsCommon.myCstr(dr("value1"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Status").Value = clsCommon.myCstr(dr("status"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("QCStatus").Value = clsCommon.myCstr(dr("qc_status"))
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("OkQty").Value = clsCommon.myCdbl(grow.Cells(colOkQty).Value)
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("RejQty").Value = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("colDeductionPers").Value = clsCommon.myCdbl(dr("Deduction_Per"))

                            If AllowDeductionPers Then
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("OkQty").Value = clsCommon.myCdbl(grow.Cells(colQty).Value) - System.Math.Round(clsCommon.myCdbl(grow.Cells(colQty).Value) * clsCommon.myCdbl(dr("Deduction_Per")) / 100, 2)
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("RejQty").Value = System.Math.Round(clsCommon.myCdbl(grow.Cells(colQty).Value) * clsCommon.myCdbl(dr("Deduction_Per")) / 100, 2)
                            End If

                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Remraks").Value = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                            frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Specification").Value = clsCommon.myCstr(grow.Cells(colSpecification).Value)

                            If dr("Mandatory") = 1 Then
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("MandatorySelect").Value = True
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("FRange1").Value = ""
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Netresult").Value = ""
                            Else
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("MandatorySelect").Value = False
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("FRange1").Value = "NA"
                                frm.gv.Rows(frm.gv.Rows.Count - 1).Cells("Netresult").Value = "NA"
                            End If

                        Next

                    End If 'dt cond.

                End If ''comp cond.

            Next ''parent gris loop

            If SettItemWiseQualityCheckInGeneralPurchase = True Then
                For Each grow As GridViewRowInfo In gv_MRN.Rows
                    If (clsCommon.myCBool(grow.Cells(colMRNSelect).Value) = True) Then
                        Dim strSql As String = "Select TSPL_MRN_Head.Vendor_Name As [Vendor Name],tspl_item_master.Item_Desc As [Item Name],TSPL_MRN_Head.VehicleNo
                                            ,TSPL_GRN_HEAD.GRN_No as [GRN No],convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date]
                                            ,TSPL_GRN_HEAD.Ref_No as [RAL No] ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No],convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date]
                                            From TSPL_MRN_Head
                                            Left Join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_Head.Against_GRN
                                            Left Join TSPL_GRN_HEAD on  TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                                            Left Join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                                            Left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                                            where TSPL_GRN_HEAD.GRN_No ='" + clsCommon.myCstr(grow.Cells(colGRNDocNo).Value) + "'"
                        Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(strSql)
                        If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then
                            frm.lblVendor.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("Vendor Name"))
                            frm.lblVehicleNo.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("VehicleNo"))
                            frm.lblItem.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("Item Name"))
                            frm.lblRALNo.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("RAL No"))
                            frm.lblGRNNo.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("GRN No"))
                            frm.lblGRNDate.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("GRN Date"))
                            frm.lblWeighmentNo.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("Weighment No"))
                            frm.lblWeighmentDate.Text = clsCommon.myCstr(dtDetail.Rows(0).Item("Weighment Date"))
                        End If
                        Exit For
                    End If
                Next
            End If

            frm.isInsideLoadData = False
            frm.WindowState = FormWindowState.Maximized
            If btnpost.Enabled = False AndAlso btnsave.Enabled = False AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                frm.btnsave.Enabled = False
            End If
            frm.ShowDialog()

            If Not (btnpost.Enabled = False AndAlso btnsave.Enabled = False AndAlso UsLock1.Status = ERPTransactionStatus.Approved) Then ''if posted then no need to refresh the value
                Array_Template = frm.Arr_Item
                txtAccept.Text = frm.Acceptedstatus
                txtAccept.Visible = frm.txtAccept.Visible
                If clsCommon.CompairString(txtAccept.Text, "Accepted") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Green
                ElseIf clsCommon.CompairString(txtAccept.Text, "Rejected") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Red
                ElseIf clsCommon.CompairString(txtAccept.Text, "Under Deviation") = CompairStringResult.Equal Then
                    txtAccept.BackColor = Color.Yellow
                End If

                Dim okqty As Double = 0
                Dim rejqty As Double = 0
                Dim count As Double = 1
                Dim DeductionPer As Double = 0
                Dim Org_Qty As Double = 0


                For Each grow_PR As GridViewRowInfo In gv.Rows
                    'DeductionPer = 0
                    For Each grow As GridViewRowInfo In frm.gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colMRNNo).Value), clsCommon.myCstr(grow_PR.Cells(colMRNNo).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colPONo).Value), clsCommon.myCstr(grow_PR.Cells(colPONo).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow_PR.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            If grow.Cells("Netresult").Style.BackColor = Color.Green OrElse (SettItemWiseQualityCheckInGeneralPurchase = True AndAlso clsCommon.CompairString(frm.Acceptedstatus, "Accepted") = CompairStringResult.Equal) Then
                                grow_PR.Cells(colQualityStatus).Value = "Accepted"
                            ElseIf grow.Cells("Netresult").Style.BackColor = Color.Red OrElse (SettItemWiseQualityCheckInGeneralPurchase = True AndAlso clsCommon.CompairString(frm.Acceptedstatus, "Rejected") = CompairStringResult.Equal) Then
                                grow_PR.Cells(colQualityStatus).Value = "Rejected"

                            ElseIf grow.Cells("Netresult").Style.BackColor = Color.Yellow OrElse (SettItemWiseQualityCheckInGeneralPurchase = True AndAlso clsCommon.CompairString(frm.Acceptedstatus, "Under Deviation") = CompairStringResult.Equal) Then
                                grow_PR.Cells(colQualityStatus).Value = "Under Deviation"
                            End If
                            If clsCommon.myCdbl(grow.Cells("RejQty").Value) > 0 Then
                                grow_PR.Cells(colSpecification).Value = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                            End If
                            If AllowDeductionPers Then
                                Org_Qty = clsCommon.myCdbl(grow_PR.Cells(colQty).Value)
                                okqty += clsCommon.myCdbl(grow.Cells("OkQty").Value)
                                rejqty += clsCommon.myCdbl(grow.Cells("RejQty").Value)
                                DeductionPer += clsCommon.myCdbl(grow.Cells("colDeductionPers").Value)
                                If DeductionPer > 100 Then
                                    DeductionPer = 100
                                End If
                                '======
                                'grow_PR.Cells(colOkQty).Value = System.Math.Round(okqty / count, 2)
                                'grow_PR.Cells(colRejQty).Value = System.Math.Round(rejqty / count, 2)
                                grow_PR.Cells(colOkQty).Value = System.Math.Round(Org_Qty - clsCommon.myCdbl(System.Math.Round(Org_Qty * DeductionPer / 100, 4)), 4)
                                grow_PR.Cells(colRejQty).Value = System.Math.Round(Org_Qty * DeductionPer / 100, 4)
                                count += 1
                            End If
                        Else
                            DeductionPer = 0
                            okqty = 0
                            rejqty = 0
                            count = 1
                        End If

                        'grow_PR.Cells(colRejQty).Value = System.Math.Round(clsCommon.myCdbl(grow_PR.Cells(colQty).Value) - clsCommon.myCdbl(grow_PR.Cells(colOkQty).Value), 2)
                    Next
                Next
                Template_Remarks = frm.Template_Remarks
                Template_Status = frm.Template_Status
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsQualityCheckForSRNHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click
        Try

            Dim qry As String = "select isnull(email,'') as email from TSPL_vendor_MASTER where vendor_Code in ('" + fndVendor_code.Value + "') "
            Dim emailId As String = clsDBFuncationality.getSingleValue(qry)
            If clsCommon.myLen(emailId) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter vendor email id.", Me.Text)
                Exit Sub
            End If


            Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text,Email_subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmQualityCheckForSRN & "'")
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim strEmailContent As String = ""
            Dim strEmailsubject As String = ""

            If (dtSMSEmail.Rows.Count > 0) Then
                strEmailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text").ToString())
                strEmailsubject = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("Email_subject").ToString())


                If (clsCommon.myLen(strEmailContent) > 0) Then

                    Dim obj As New clsQualityCheckForSRNHead()
                    obj = clsQualityCheckForSRNHead.GetData(txtDocNo.Value, QC_Type, NavigatorType.Current)

                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                        Dim strDetailsData As String = " <table border=" + "1" + " cellpadding=" + "1" + " cellspacing=" + "0" + " width = " + "1200" + "><tr bgcolor=''#4da6ff''><td width=100><b> SNO</b></td><td width=100><b> MRN No</b></td> <td> <b> PO NO</b> </td>  <td width=100> <b> Item Type</b> </td> <td> <b> Item Code</b> </td> <td width=100> <b> Item Description</b> </td><td> <b>UOM</b> </td> <td> <b> Drawing No</b> </td> <td> <b> Part No</b> </td> <td> <b> Quantity</b> </td> <td> <b> Specification</b> </td> <td> <b> Ok Quantity</b> </td> <td> <b> Reject Quantity</b> </td> <td> <b> Observed Value</b> </td> <td> <b> Auto Observation</b> </td> <td> <b> Reject Remarks</b> </td> <td> <b> Remarks</b> </td></tr> "
                        If obj.Arr_item IsNot Nothing AndAlso obj.Arr_item.Count > 0 Then

                            For Each objtr As clsQualityCheckForSRNDetail In obj.Arr_item
                                strDetailsData += "<tr><td>" + clsCommon.myCstr(objtr.Line_no) + "</td><td> " + clsCommon.myCstr(objtr.MRN_No) + "</td> <td> " + clsCommon.myCstr(objtr.PO_No) + "</td> <td> " + clsCommon.myCstr(clsQualityCheckForSRNHead.FullNameOfItemType(objtr.Row_Type)) + "</td> <td> " + clsCommon.myCstr(objtr.Item_Code) + "</td>  <td> " + clsCommon.myCstr(objtr.Item_Desc) + "</td><td> " + clsCommon.myCstr(objtr.Unit_Code) + "</td>  <td> " + clsCommon.myCstr(objtr.Drawing_No) + "</td> <td> " + clsCommon.myCstr(objtr.Part_No) + "</td> <td> " + clsCommon.myCstr(objtr.MRN_Qty) + "</td> <td> " + clsCommon.myCstr(objtr.Param_Desc) + "</td> <td> " + clsCommon.myCstr(objtr.Ok_Qty) + "</td> <td> " + clsCommon.myCstr(objtr.Reject_Qty) + "</td> <td> " + clsCommon.myCstr(objtr.Net_Measurement) + "</td> <td> " + clsCommon.myCstr(objtr.Auto_Measured) + "</td> <td> " + clsCommon.myCstr(objtr.Remarks) + "</td> <td> " + clsCommon.myCstr(objtr.Reject_Remarks) + "</td> </tr>"
                            Next

                        End If
                        strDetailsData += "</table>"

                        objEmailH.IsBodyHtml = 1

                        objEmailH.Email_Subject = clsCommon.myCstr(strEmailsubject)
                        objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, obj.Document_Code)
                        objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))

                        objEmailH.Email_Text = clsCommon.myCstr(strEmailContent)
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.Document_Code)
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Detail_Data, strDetailsData)

                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                        objEmailH.SaveData(clsUserMgtCode.frmQualityCheckForSRN, objEmailH, Nothing)
                        objEmailH = Nothing
                        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
                Return
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo) <= 0 Then
                Throw New Exception("Document number not found")
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " select TSPL_COMPANY_MASTER.Comp_Name, TSPL_QC_CHECK_SRN_DETAIL.Document_Code as QC_NO,convert (varchar,TSPL_QC_CHECK_HEAD.Document_Date,103) as QC_Date, TSPL_QC_CHECK_HEAD.QC_STATUS,TSPL_QC_CHECK_SRN_DETAIL.MRN_No, Convert (varchar, TSPL_MRN_HEAD.MRN_DATE,103) as MRN_DATE,TSPL_MRN_HEAD.Against_PO,TSPL_MRN_HEAD.Against_GRN,Convert (varchar,TSPL_GRN_HEAD.GRN_Date,103) as GRN_Date,TSPL_MRN_HEAD.Invoice_No as [Vendor_Invoice_No], case when len( isnull(TSPL_MRN_HEAD.Invoice_No,'')) > 0 then  convert (varchar, TSPL_MRN_HEAD.Invoice_Date,103) else '' end [Vendor_Invoice_Date],TSPL_QC_CHECK_HEAD.Vendor_Code,tspl_vendor_master.Vendor_Name,TSPL_QC_CHECK_HEAD.Bill_To_location, TSPL_QC_CHECK_SRN_DETAIL.Item_code,TSPL_ITEM_MASTER.Item_Desc,TSPL_QC_CHECK_SRN_DETAIL.Unit_Code,TSPL_QC_CHECK_SRN_DETAIL.MRN_Qty,TSPL_QC_CHECK_SRN_DETAIL.OK_Qty,TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code ,TSPL_QC_LOG_SHEET_MASTER.Description as [PARAMETER],TSPL_QC_CHECK_SRN_DETAIL.Param_QC_Status as [SPECIFICATIONS],TSPL_QC_CHECK_SRN_DETAIL.Reject_Remarks as [OBSERVATIONS],TSPL_QC_CHECK_SRN_DETAIL.Remarks as [COMMENTS] ,TSPL_QC_CHECK_DETAIL.Additional_Remarks from TSPL_QC_CHECK_SRN_DETAIL " &
                                                " left outer Join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_SRN_DETAIL.Document_Code = TSPL_QC_CHECK_HEAD.Document_Code " &
                                                "left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code and TSPL_QC_CHECK_SRN_DETAIL.Item_Code=TSPL_QC_CHECK_DETAIL.Item_Code" +
                                " left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code = TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code " &
                                " left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_code " &
                                " left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code " &
                                " left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_NO = TSPL_QC_CHECK_SRN_DETAIL.MRN_No " &
                                " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_NO = TSPL_MRN_HEAD.Against_GRN " &
                                " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_QC_CHECK_HEAD.Comp_Code " &
                                " where TSPL_QC_CHECK_SRN_DETAIL.Document_Code = '" + txtDocNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCEntry", "Quality Control Report", clsCommon.myCDate(dtpDate.Value))
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellEditorInitialized(sender As Object, e As GridViewCellEventArgs) Handles gv.CellEditorInitialized
        Try
            If gv.CurrentColumn Is gv.Columns(colAdditionalRemarks) Then
                Dim tbEditor As RadTextBoxEditor = e.ActiveEditor
                If (tbEditor IsNot Nothing) Then
                    Dim element As New RadTextBoxEditorElement
                    element.TextBoxItem.Multiline = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub fnd_PendingMRN__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fnd_PendingMRN._MYValidating
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                FunReset()
            End If

            Dim qry As String = "select distinct max(vendor) as [Vendor Code],max(tspl_vendor_master.vendor_name) as [Vendor Name],MRN_No as [MRN No],Final.GRN_No as [GRN No],max(MRN_Date) as [MRN Date],convert(varchar,Final.Grn_date,103) as [GRN date],max(Final.VehicleNo) as VehicleNo,max(TSPL_ITEM_MASTER.Item_Desc) as [Item Name]
                    ,max(TSPL_GRN_HEAD.Ref_No) as [RAL No] 
                    ,max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code) as [Weighment No]
                    ,convert(varchar,max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date),103) as [Weighment Date]
                    from ( select Final1.* from (" + Environment.NewLine &
                    " select TSPL_MRN_Head.VehicleNo, pod.Bin_No,TSPL_MRN_Head.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Row_Type as IType,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,TSPL_MRN_DETAIL.Location as Location,1 as RI,TSPL_MRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_MRN_Head.Tax_Group,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.TAX1_Amt,TSPL_MRN_DETAIL.TAX2_Amt,TSPL_MRN_DETAIL.TAX3_Amt,TSPL_MRN_DETAIL.TAX4_Amt,TSPL_MRN_DETAIL.TAX5_Amt,TSPL_MRN_DETAIL.TAX6_Amt,TSPL_MRN_DETAIL.TAX7_Amt,TSPL_MRN_DETAIL.TAX8_Amt,TSPL_MRN_DETAIL.TAX9_Amt,TSPL_MRN_DETAIL.TAX10_Amt ,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.QC_Check=1 and TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1 AND gh.IsSkipPurchaseQC=0 "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal AndAlso objCommonVar.RCDFCFP = False Then
                qry += "  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) and TSPL_MRN_HEAD.IsCancel=0 "
            End If

            If clsCommon.myLen(fndVendor_code.Value) > 0 Then
                qry += " and TSPL_MRN_Head.Vendor_Code='" + fndVendor_code.Value + "'" + Environment.NewLine
            End If
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += " and TSPL_MRN_Head.bill_to_location='" + txtBillToLocation.Value + "' " + Environment.NewLine
            End If
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += "  and TSPL_MRN_Head.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            qry += " union all" + Environment.NewLine &
            " select '' as VehicleNo,null as Bin_No,vendor_code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0  " + Environment.NewLine &
            " union all  " + Environment.NewLine &
            " select '' as VehicleNo,null as Bin_No,vendor_code as Vendor,TSPL_QC_CHECK_DETAIL.Item_Code as ICode,'' as IType,0 as Qty,(isnull(TSPL_QC_CHECK_DETAIL.OK_Qty,0)+isnull(TSPL_QC_CHECK_DETAIL.Reject_Qty,0)) as Unapproved,TSPL_QC_CHECK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate,TSPL_QC_CHECK_DETAIL.MRN_No as MRN_No,'',null,null from TSPL_QC_CHECK_DETAIL left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.document_code=TSPL_QC_CHECK_DETAIL.document_code where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and len(isnull(TSPL_QC_CHECK_DETAIL.MRN_No,''))>0 and TSPL_QC_CHECK_HEAD.IsCancel=0  " + Environment.NewLine &
            " union all  " + Environment.NewLine &
            " select '' as VehicleNo,null as Bin_No,vendor_code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,'' as IType,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))>0 " + Environment.NewLine &
            " )Final1 where 2=2 "
            If SettItemWiseQualityCheckInGeneralPurchase Then
                qry += " and final1.icode in (select distinct TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER) "
            Else
                qry += " and final1.icode in (select distinct TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Item_Code from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.Document_Code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.Document_Code where TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code=Final1.vendor) "
            End If
            qry += " )Final left join tspl_vendor_master on tspl_vendor_master.vendor_code=Final.Vendor left outer join tspl_item_master on tspl_item_master.Item_Code=Final.ICode
                 left join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=Final.GRN_No
                 left join TSPL_GRN_HEAD on  TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                 group by MRN_No,Final.GRN_No,Final.Grn_date,ICode,Unit,MRP having SUM(Chk)>0  "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal OrElse objCommonVar.RCDFCFP = True Then
                qry = qry & " and MRN_No not in (select distinct MRN_No from TSPL_QC_CHECK_DETAIL ) "
                qry = qry & " and MRN_No not in (select distinct MRN_Id from TSPL_SRN_DETAIL where isnull(MRN_Id,'')<>'')  "
            Else
                qry += " and SUM((Qty *RI)-Unapproved-DamageQty) <>0 "
            End If
            qry = qry & " order by convert(varchar,Final.Grn_date,103),Final.GRN_No"

            Dim rows As DataRow = clsCommon.ShowSelectFormForRow("PendingMRN", qry)
            If Not rows Is Nothing Then
                fndVendor_code.Value = clsCommon.myCstr(rows("Vendor Code"))
                txtRALNo.Text = clsCommon.myCstr(rows("RAL No"))
                Dim strMRN As String = clsCommon.myCstr(rows("MRN No"))
                If clsCommon.myLen(fndVendor_code.Value) > 0 Then
                    TxtVendor_desc.Text = clsVendorMaster.GetName(fndVendor_code.Value, Nothing)
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_MRN_Head.Bill_To_Location from TSPL_MRN_Head where TSPL_MRN_Head.mrn_no='" + clsCommon.myCstr(rows("MRN No")) + "'", Nothing))
                    End If
                    FillMRNGrid(strMRN)

                    If SettItemWiseQualityCheckInGeneralPurchase = True Then
                        Dim iMRN As String = ""
                        For ii As Integer = 0 To gv_MRN.Rows.Count - 1
                            iMRN = clsCommon.myCstr(gv_MRN.Rows(ii).Cells(colMRNDocNo).Value)
                            If clsCommon.myLen(iMRN) > 0 AndAlso clsCommon.CompairString(iMRN, clsCommon.myCstr(rows("MRN No"))) = CompairStringResult.Equal Then
                                gv_MRN.Rows(ii).IsCurrent = True
                                gv_MRN.Rows(ii).Cells(colMRNSelect).Value = True

                                txtGENo.Text = clsCommon.myCstr(rows("GRN No"))
                                txtGEDate.Text = clsCommon.myCDate(rows("GRN date"))
                                txtGEDate.Checked = True

                                Exit For
                            End If
                        Next
                    End If
                End If
                If SettItemWiseQualityCheckInGeneralPurchase = True Then
                    btnTemplates.PerformClick()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAnalysisPrint_Click(sender As Object, e As EventArgs) Handles btnAnalysisPrint.Click
        AnalysisPrint(False)
    End Sub

    Private Sub TxtFinderVendorPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderVendorPrint._MYValidating
        Try
            TxtFinderVendorPrint.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N'", TxtFinderVendorPrint.Value, isButtonClicked)
            lblVendorPrint.Text = clsVendorMaster.GetName(TxtFinderVendorPrint.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub TxtFinderItemPrint_Click(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderItemPrint.Click

    'End Sub

    Private Sub TxtFinderItemPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderItemPrint._MYValidating
        Try
            Dim obj As clsItemMaster = clsItemMaster.FinderForItem(TxtFinderItemPrint.Value, "R", isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                TxtFinderItemPrint.Value = obj.Item_Code
                lblItemPrint.Text = obj.Item_Desc
            Else
                TxtFinderItemPrint.Value = ""
                lblItemPrint.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAnalysisPrintVertical_Click(sender As Object, e As EventArgs) Handles btnAnalysisPrintVertical.Click
        Try
            AnalysisPrint(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub AnalysisPrint(ByVal IsPrintVertical As Boolean)
        Try
            Dim StrWhere As String = ""
            If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Document number not found")
                    txtDocNo.Focus()
                    Exit Sub
                End If
                StrWhere += " AND TSPL_QC_CHECK_SRN_DETAIL.Document_Code = '" + txtDocNo.Value + "'"
            ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
                If fromDate.Value > ToDate.Value Then
                    common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date")
                    fromDate.Focus()
                    Exit Sub
                End If

                If clsCommon.myLen(TxtFinderVendorPrint.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select vendor first")
                    TxtFinderVendorPrint.Focus()
                    Exit Sub
                End If
                If clsCommon.myLen(TxtFinderItemPrint.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select item first")
                    TxtFinderItemPrint.Focus()
                    Exit Sub
                End If
                StrWhere += " and TSPL_QC_CHECK_HEAD.Vendor_Code = '" + TxtFinderVendorPrint.Value + "' 
                              and  TSPL_QC_CHECK_SRN_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'
                              and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) >= convert(date,('" & fromDate.Value & "'),103) and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103) "
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    StrWhere += " and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If

            Dim frmCRV As New frmCrystalReportViewer()
            'ROW_NUMBER() OVER(PARTITION BY TSPL_QC_CHECK_HEAD.Document_Code ORDER BY TSPL_QC_CHECK_HEAD.Document_Code ASC)
            Dim qry As String = " select TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO as RowNum,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_MRN_Head.VehicleNo,TSPL_COMPANY_MASTER.Comp_Code   , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2 , TSPL_COMPANY_MASTER.Add3 as Comp_Add3 , TSPL_COMPANY_MASTER.City_Code as Comp_City_Code, TSPL_COMPANY_MASTER.Email as Comp_Email , TSPL_COMPANY_MASTER.Pincode as Comp_Pincode , TSPL_COMPANY_MASTER.State as Comp_State , TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No , TSPL_COMPANY_MASTER.Logo_Img as Comp_Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 as Comp_Logo_Img2, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTReg_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNo, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1 , TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_LOCATION_MASTER.Location_Code  , TSPL_LOCATION_MASTER.Location_Desc , TSPL_LOCATION_MASTER.Add1 as Location_Add1, TSPL_LOCATION_MASTER.Add2 as Location_Add2 , TSPL_LOCATION_MASTER.Add3 as Location_Add3 , TSPL_LOCATION_MASTER.Add4 as Location_Add4 , TSPL_LOCATION_MASTER.City_Code as Location_City_Code , TSPL_LOCATION_MASTER.State as Location_State , TSPL_LOCATION_MASTER.Pin_Code as Location_Pin_Code , TSPL_LOCATION_MASTER.Country as Location_Country , TSPL_LOCATION_MASTER.Telphone as Location_Telphone, TSPL_LOCATION_MASTER.Email as Location_Email, TSPL_LOCATION_MASTER.Loc_Short_Name as Loc_Short_Name , TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant,  TSPL_QC_CHECK_HEAD.Document_Code, convert (varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as Document_Date ,TSPL_QC_CHECK_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_QC_CHECK_SRN_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc , TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code , convert (varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date , TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code, (case when len(tspl_qc_log_sheet_master.AliasName)>0 then tspl_qc_log_sheet_master.AliasName else tspl_qc_log_sheet_master.description end) as param_desc,
                                  TSPL_QC_CHECK_SRN_DETAIL.InputData , TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer
                                  ,TSPL_QC_CHECK_HEAD.Gate_Entry_No,TSPL_QC_CHECK_HEAD.Gate_Entry_Date,TSPL_QC_CHECK_HEAD.QC_Status,TSPL_GRN_HEAD.Ref_No as RAL_NO
                                  from TSPL_QC_CHECK_SRN_DETAIL 
                                  inner join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code
                                  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code
                                  left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = TSPL_QC_CHECK_SRN_DETAIL.MRN_No and TSPL_MRN_DETAIL.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No = TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code
                                  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No
                                  left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_DETAIL.GRN_No
                                  left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard'
                                  left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code
                                  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location
                                  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_MASTER.Comp_code
                                  left outer join TSPL_MRN_Head on TSPL_MRN_DETAIL.MRN_No = TSPL_MRN_Head.MRN_No
                                  where 1=1 " + StrWhere + " order by TSPL_QC_CHECK_HEAD.Document_Code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                If IsPrintVertical = True Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReportVertical", "Analysis Report", clsCommon.myCDate(dtpDate.Value))
                ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReport", "Analysis Report", clsCommon.myCDate(dtpDate.Value))
                ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReportMultiple", "Analysis Report", clsCommon.myCDate(ToDate.Value))
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiEnglish_Click(sender As Object, e As EventArgs) Handles rmiEnglish.Click
        PrintRejected(rmiEnglish.Text)
    End Sub

    Private Sub rmiHindi_Click(sender As Object, e As EventArgs) Handles rmiHindi.Click
        PrintRejected(rmiHindi.Text)
    End Sub

    Private Sub PrintRejected(strBtnText As String)
        Try
            Dim StrWhere As String = ""
            If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Document number not found")
                    txtDocNo.Focus()
                    Exit Sub
                End If
                StrWhere += " AND TSPL_QC_CHECK_SRN_DETAIL.Document_Code = '" + txtDocNo.Value + "'"
            ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
                If fromDate.Value > ToDate.Value Then
                    common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date")
                    fromDate.Focus()
                    Exit Sub
                End If

                If clsCommon.myLen(TxtFinderVendorPrint.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select vendor first")
                    TxtFinderVendorPrint.Focus()
                    Exit Sub
                End If
                If clsCommon.myLen(TxtFinderItemPrint.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select item first")
                    TxtFinderItemPrint.Focus()
                    Exit Sub
                End If
                StrWhere += " and TSPL_QC_CHECK_HEAD.Vendor_Code = '" + TxtFinderVendorPrint.Value + "' 
                              and  TSPL_QC_CHECK_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'
                              and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) >= convert(date,('" & fromDate.Value & "'),103) and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103) "
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    StrWhere += " and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If

            Dim frmCRV As New frmCrystalReportViewer()
            'ROW_NUMBER() OVER(PARTITION BY TSPL_QC_CHECK_HEAD.Document_Code ORDER BY TSPL_QC_CHECK_HEAD.Document_Code ASC)
            Dim qry As String = " select  TSPL_QC_CHECK_HEAD.Document_Code,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_MRN_Head.VehicleNo,TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2 , TSPL_COMPANY_MASTER.Add3 as Comp_Add3 , TSPL_COMPANY_MASTER.City_Code as Comp_City_Code, TSPL_COMPANY_MASTER.Email as Comp_Email , TSPL_COMPANY_MASTER.Pincode as Comp_Pincode , TSPL_COMPANY_MASTER.State as Comp_State , TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No , TSPL_COMPANY_MASTER.Logo_Img as Comp_Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 as Comp_Logo_Img2, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTReg_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNo, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1 , TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_LOCATION_MASTER.Location_Code  , TSPL_LOCATION_MASTER.Location_Desc , TSPL_LOCATION_MASTER.Add1 as Location_Add1, TSPL_LOCATION_MASTER.Add2 as Location_Add2 , TSPL_LOCATION_MASTER.Add3 as Location_Add3 , TSPL_LOCATION_MASTER.Add4 as Location_Add4 , TSPL_LOCATION_MASTER.City_Code as Location_City_Code , TSPL_LOCATION_MASTER.State as Location_State , TSPL_LOCATION_MASTER.Pin_Code as Location_Pin_Code , TSPL_LOCATION_MASTER.Country as Location_Country , TSPL_LOCATION_MASTER.Telphone as Location_Telphone, TSPL_LOCATION_MASTER.Email as Location_Email, TSPL_LOCATION_MASTER.Loc_Short_Name as Loc_Short_Name , TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant,  TSPL_QC_CHECK_HEAD.Document_Code, convert (varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as Document_Date ,TSPL_QC_CHECK_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,(TSPL_VENDOR_MASTER.Add1+''+TSPL_VENDOR_MASTER.Add2+''+TSPL_VENDOR_MASTER.Add3) As VendorAddress,(TSPL_VENDOR_MASTER.Phone1+' '+TSPL_VENDOR_MASTER.Phone2) As VendorPhoneNo,TSPL_VENDOR_MASTER.Email As VendorEmail,
                                  Isnull(TSPL_LOCATION_MASTER.Range_Name,'') As RangeName,
                                  (Case When TSPL_LOCATION_MASTER.Range_Address='' Then 'Manager'  Else IsNull(TSPL_LOCATION_MASTER.Range_Address,'Manager') End) As DesignationName, 
                                 TSPL_QC_CHECK_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc , TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code , convert (varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date ,
                                  TSPL_QC_CHECK_HEAD.Gate_Entry_No,TSPL_QC_CHECK_HEAD.Gate_Entry_Date,TSPL_QC_CHECK_HEAD.QC_Status,TSPL_GRN_HEAD.Ref_No as RAL_NO,TSPL_QC_CHECK_HEAD.Template_Remarks
                                  from TSPL_QC_CHECK_DETAIL 
                 				inner join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_DETAIL.Document_Code
                                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code
                                left outer Join TSPL_QC_CHECK_SRN_DETAIL On TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
									left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_QC_CHECK_DETAIL.Item_Code
                                  left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = TSPL_QC_CHECK_DETAIL.MRN_No and TSPL_MRN_DETAIL.Item_Code = TSPL_QC_CHECK_DETAIL.Item_Code
                                  left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No = TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code
                                  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No
                                  left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_DETAIL.GRN_No
                                          left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location
                                  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_MASTER.Comp_code
                                  left outer join TSPL_MRN_Head on TSPL_MRN_DETAIL.MRN_No = TSPL_MRN_Head.MRN_No
                                  where 1=1 And TSPL_QC_CHECK_HEAD.QC_Status='Rejected' " + StrWhere
            qry += "order by TSPL_QC_CHECK_HEAD.Document_Code"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
                Exit Sub
            Else
                If clsCommon.CompairString(strBtnText, "English") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReportRejectionEnglish", "Rejected Analysis Report")
                ElseIf clsCommon.CompairString(strBtnText, "Hindi") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReportRejectionHindi", "Rejected Analysis Report")
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtFinderRalPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderRalPrint._MYValidating
        Dim qry As String = "select  tspl_tender_header.DocumentCode as RALNO ,tspl_tender_header.DocumentDate from tspl_tender_header
                            left outer join TSPL_TENDER_DETAIL on TSPL_TENDER_DETAIL.DocumentCode=tspl_tender_header.DocumentCode "

        Dim whrcls As String = "  TSPL_TENDER_DETAIL.Vendor_Code = '" + TxtFinderVendorPrint.Value + "'
                                 and  TSPL_TENDER_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'
                                 and convert(date,tspl_tender_header.DocumentDate,103) >= convert(date,('" & fromDate.Value & "'),103) and convert(date,tspl_tender_header.DocumentDate,103) <= convert(date,('" & ToDate.Value & "'),103) "
        TxtFinderRalPrint.Value = clsCommon.ShowSelectForm("RPTRal", qry, "RALNO", whrcls, TxtFinderRalPrint.Value, "RALNO", isButtonClicked)
        lblRalPrint.Text = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select DocumentDate from tspl_tender_header WHERE DocumentCode ='" + TxtFinderRalPrint.Value + "'"))
    End Sub


    Private Sub btnRALWiseAnaysisPrint_Click(sender As Object, e As EventArgs) Handles btnRALWiseAnaysisPrint.Click
        Try
            Dim StrWhere As String = ""
            If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Document number not found")
                    txtDocNo.Focus()
                    Exit Sub
                End If
                StrWhere += " AND TSPL_QC_CHECK_SRN_DETAIL.Document_Code = '" + txtDocNo.Value + "'"
            ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
                If fromDate.Value > ToDate.Value Then
                    common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date")
                    fromDate.Focus()
                    Exit Sub
                End If

                If clsCommon.myLen(TxtFinderVendorPrint.Value) > 0 Then
                    StrWhere += " and TSPL_QC_CHECK_HEAD.Vendor_Code = '" + TxtFinderVendorPrint.Value + "'"

                ElseIf clsCommon.myLen(TxtFinderItemPrint.Value) > 0 Then
                    StrWhere += " and TSPL_QC_CHECK_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'"

                ElseIf clsCommon.myLen(TxtFinderRalPrint.Value) > 0 Then
                    StrWhere += " and TSPL_GRN_HEAD.Ref_No = '" + TxtFinderRalPrint.Value + "'"

                End If
                StrWhere += " and TSPL_QC_CHECK_HEAD.Vendor_Code = '" + TxtFinderVendorPrint.Value + "' 
                              and  TSPL_QC_CHECK_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'
                              and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) >= convert(date,('" & fromDate.Value & "'),103) and convert(date,TSPL_QC_CHECK_HEAD.Document_Date,103) <= convert(date,('" & ToDate.Value & "'),103) "
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    StrWhere += " and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If

            Dim frmCRV As New frmCrystalReportViewer()

            Dim qry As String = " select TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO as RowNum,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_MRN_Head.VehicleNo,TSPL_COMPANY_MASTER.Comp_Code   , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2 , TSPL_COMPANY_MASTER.Add3 as Comp_Add3 , TSPL_COMPANY_MASTER.City_Code as Comp_City_Code, TSPL_COMPANY_MASTER.Email as Comp_Email , TSPL_COMPANY_MASTER.Pincode as Comp_Pincode , TSPL_COMPANY_MASTER.State as Comp_State , TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No , TSPL_COMPANY_MASTER.Logo_Img as Comp_Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 as Comp_Logo_Img2, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTReg_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNo, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1 , TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_LOCATION_MASTER.Location_Code  , TSPL_LOCATION_MASTER.Location_Desc , TSPL_LOCATION_MASTER.Add1 as Location_Add1, TSPL_LOCATION_MASTER.Add2 as Location_Add2 , TSPL_LOCATION_MASTER.Add3 as Location_Add3 , TSPL_LOCATION_MASTER.Add4 as Location_Add4 , TSPL_LOCATION_MASTER.City_Code as Location_City_Code , TSPL_LOCATION_MASTER.State as Location_State , TSPL_LOCATION_MASTER.Pin_Code as Location_Pin_Code , TSPL_LOCATION_MASTER.Country as Location_Country , TSPL_LOCATION_MASTER.Telphone as Location_Telphone, TSPL_LOCATION_MASTER.Email as Location_Email, TSPL_LOCATION_MASTER.Loc_Short_Name as Loc_Short_Name , TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant,  TSPL_QC_CHECK_HEAD.Document_Code, convert (varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as Document_Date ,TSPL_QC_CHECK_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_QC_CHECK_SRN_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc , TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code , convert (varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date , TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code, (case when len(tspl_qc_log_sheet_master.AliasName)>0 then tspl_qc_log_sheet_master.AliasName else tspl_qc_log_sheet_master.description end) as param_desc,
                                  TSPL_QC_CHECK_SRN_DETAIL.InputData , TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer
                                  ,TSPL_QC_CHECK_HEAD.Gate_Entry_No,TSPL_QC_CHECK_HEAD.Gate_Entry_Date,TSPL_QC_CHECK_HEAD.QC_Status,TSPL_GRN_HEAD.Ref_No as RAL_NO,TSPL_QC_CHECK_HEAD.Template_Remarks as [Template Remark],TSPL_GRN_HEAD.LR_No as [Bilty No],TSPL_GRN_HEAD.GRN_Date
                                  from TSPL_QC_CHECK_SRN_DETAIL 
                                  inner join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code
                                   left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
                                  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code
                                  left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = TSPL_QC_CHECK_SRN_DETAIL.MRN_No and TSPL_MRN_DETAIL.Item_Code = TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No = TSPL_MRN_DETAIL.GRN_Id and TSPL_GRN_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code
                                  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No
                                  left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No = TSPL_GRN_DETAIL.GRN_No
                                  left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard'
                                  left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
                                  and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.QC_Code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code
                                  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location
                                  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_MASTER.Comp_code
                                  left outer join TSPL_MRN_Head on TSPL_MRN_DETAIL.MRN_No = TSPL_MRN_Head.MRN_No
                                  where 1=1 " + StrWhere + " order by TSPL_QC_CHECK_HEAD.Document_Code,TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
                Exit Sub
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCRALWiseRMReportMultiple", "RL Wise Report")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


End Class
