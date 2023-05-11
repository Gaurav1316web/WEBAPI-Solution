Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmDispatch
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsformLoad As Boolean = True
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colLastRGPNRGP As String = "RGPNRGP"
    Const colDate As String = "Date"
    Const colSp As String = "COLSPECIFICATION"

    Public DocumentNo As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        LoadBlankGrid()
        gv1.Rows.AddNew()
        IsformLoad = True
        LoadBilling()
        LoadDocType()
        IsformLoad = False
        'LoadModeOfTrasport()
        AddNew()
        SetLength()
        RadPageView1.SelectedPage = RadPageViewPage1

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment

        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If

    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtReason.MaxLength = 200
        txtVehicleNo.MaxLength = 50

    End Sub

    'Sub LoadModeOfTrasport()
    '    cboModeOfTransport.Items.Add("By Road")
    '    cboModeOfTransport.Items.Add("By Air")
    '    cboModeOfTransport.Items.Add("By Sea")
    'End Sub

    Public Sub LoadDocType()
        Dim dtDocType As New DataTable
        dtDocType.Columns.Add("Code", GetType(String))
        dtDocType.Columns.Add("Desc", GetType(String))
        dtDocType.Rows.Add("RGP", "Returnable Gate Pass")
        dtDocType.Rows.Add("NRGP", "Non Returnable Gate Pass")
        cboDocType.DataSource = dtDocType
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Desc"




        'Dim dt As DataTable = New DataTable()
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))

        'Dim dr As DataRow = dt.NewRow()
        'dr("Code") = "RGP"
        'dr("Name") = "Returnable Gate Pass"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "NRGP"
        'dr("Name") = "Non Returnable Gate Pass"
        'dt.Rows.Add(dr)

        'cboDocType.DataSource = dt
        'cboDocType.ValueMember = "Code"
        'cboDocType.DisplayMember = "Name"
    End Sub

    Sub LoadBilling()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "No"
        dt.Rows.Add(dr)

        ddlBilling.DataSource = dt
        ddlBilling.ValueMember = "Code"
        ddlBilling.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtReason.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        txtDepartment.Value = ""
        lblDepartment.Text = ""
        chkAgainst_Sale.Checked = False
        lblVendorName.Text = ""
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtReason.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtVehicleNo.Text = ""
        txtGPNo.Text = ""
        txtGPDate.Checked = False
        txtGPDate.Value = txtDate.Value
        cboDocType.Enabled = True
        txtLocation.Enabled = True
        txtLocation.Value = ""
        lblLocation.Text = ""
        lblDocumentAmt.Text = ""
        txtDeliveredBy.Value = ""
        lblDeliveredBy.Text = ""
        chkNonInventoryItem.Checked = False
        fndCostCentre.Value = ""
        txtCostCentre.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 120
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 220
        repoIName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 100
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 100
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim LastRGPNRGP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LastRGPNRGP.FormatString = ""
        LastRGPNRGP.HeaderText = "Last RGP/NRGP"
        LastRGPNRGP.Name = colLastRGPNRGP
        LastRGPNRGP.Width = 100
        LastRGPNRGP.ReadOnly = True
        LastRGPNRGP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LastRGPNRGP)

        Dim GRPDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GRPDate.FormatString = ""
        GRPDate.HeaderText = "RGP/NRGP Date"
        GRPDate.Name = colDate
        GRPDate.Width = 100
        GRPDate.ReadOnly = True
        GRPDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(GRPDate)

        Dim repoSP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP = New GridViewTextBoxColumn()
        repoSP.FormatString = ""
        repoSP.HeaderText = "Specification"
        repoSP.Name = colSp
        repoSP.Width = 100
        'repoSP.Minimum = 0
        repoSP.ReadOnly = False
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSP)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If (e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) Then

                        If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then
                            UpdateCurrentRow()
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colUnit) And chkNonInventoryItem.Checked = False Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colICode) And chkNonInventoryItem.Checked = False Then
                            OpenICodeList(False)
                            If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
                                Dim Qry As String = "Select RGP_No, CONVERT(VARCHAR,RGP_Date,103) as RGP_Date, Specification from ("
                                Qry += " Select TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_DETAIL.Specification,ROW_NUMBER() OVER (Order By RGP_Date DESC) as Row from TSPL_RGP_HEAD "
                                Qry += " LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No  "
                                Qry += " Where TSPL_RGP_HEAD.Doc_Type='RGP' AND TSPL_RGP_DETAIL.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "') XXX WHERE XXX.Row=1"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                If dt.Rows.Count > 0 Then
                                    gv1.CurrentRow.Cells(colLastRGPNRGP).Value = clsCommon.myCstr(dt.Rows(0)("RGP_No"))
                                    gv1.CurrentRow.Cells(colDate).Value = clsCommon.myCstr(dt.Rows(0)("RGP_Date"))
                                    gv1.CurrentRow.Cells(colSp).Value = clsCommon.myCstr(dt.Rows(0)("Specification"))
                                Else
                                    gv1.CurrentRow.Cells(colLastRGPNRGP).Value = ""
                                    gv1.CurrentRow.Cells(colDate).Value = ""
                                    gv1.CurrentRow.Cells(colSp).Value = ""
                                End If
                            End If
                        End If
                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
        setBalance()
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colRate)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
        End If
        setBalance()
    End Sub

    Private Sub UpdateCurrentRow()
        Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        gv1.CurrentRow.Cells(colAmt).Value = Math.Round(dblAmt, 2)
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        lblDocumentAmt.Text = clsCommon.myFormat(dblTotAmt)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        LoadBlankGrid()
        gv1.Rows.AddNew()
        RadLabel2.Text = "Vendor No"
        HideUnhideColumn()
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""
        chlCust.Checked = False

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub


    Function AllowToSave() As Boolean
        Try
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_RGP_HEAD where RGP_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                    Return False
                End If
            End If



            UpdateAllTotals()
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                If chlCust.Checked Then
                    Throw New Exception("Please Enter Customer.")
                Else
                    Throw New Exception("Please Enter Vendor.")
                End If
                Return False
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                txtDocNo.Focus()
                Throw New Exception("SRN No Not found to save")
                Return False
            End If
            If clsCommon.myLen(cboDocType.SelectedValue) <= 0 Then
                cboDocType.Focus()
                Throw New Exception("Please select Document Type")
                Return False
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please Enter Location")
                Return False
            End If
            If clsCommon.myLen(txtDeliveredBy.Value) <= 0 Then
                txtDeliveredBy.Focus()
                Throw New Exception("Please Enter Deliverd By")
                Return False
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            dblEnteredQty += dblQtyInner
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If dblEnteredQty > dblBalQty And chkNonInventoryItem.Checked = False Then
                        Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        Return False
                    End If
                End If
            Next

            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsRGPHead()
                obj.RGP_No = txtDocNo.Value
                obj.RGP_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                If (cboDocType.SelectedValue = "NRGP") Then
                    obj.Against_Sale = clsCommon.myCdbl(chkAgainst_Sale.Checked)
                End If
                obj.Mode_Of_Transport = txtModeOfTransport.Text
                obj.Cash_Memo_Detail = txtCashMemoDetail.Text
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GPNo = txtGPNo.Text
                obj.GPDate = txtGPDate.Value
                obj.Remarks = txtRemarks.Text
                obj.Reason = txtReason.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Document_Amount = clsCommon.myCdbl(lblDocumentAmt.Text)
                obj.Location = txtLocation.Value
                obj.Delivered_By = txtDeliveredBy.Value
                obj.Department = txtDepartment.Value
                obj.Billing = clsCommon.myCstr(ddlBilling.SelectedValue)
                obj.CostCentre = fndCostCentre.Value
                obj.CostCentreDesc = txtCostCentre.Text
                obj.Against_Customer = clsCommon.myCdbl(chlCust.Checked)
                If chkNonInventoryItem.Checked Then
                    obj.Is_Non_Inventory = 1
                End If
                obj.Arr = New List(Of clsRGPDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsRGPDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Last_RGP_No = clsCommon.myCstr(grow.Cells(colLastRGPNRGP).Value)
                    objTr.Last_RGP_Date = clsCommon.myCstr(grow.Cells(colDate).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSp).Value)
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
                End If
                ''End of For Custom Fields


                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.RGP_No)
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If

                    LoadData(obj.RGP_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If Not ChekBtnPost Then
                common.clsCommon.MyMessageBoxShow(ex.Message)
            Else
                Throw New Exception(ex.Message)
            End If


        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsRGPHead()
            obj = clsRGPHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                txtModeOfTransport.Text = obj.Mode_Of_Transport
                txtCashMemoDetail.Text = obj.Cash_Memo_Detail
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.RGP_No
                txtDate.Value = obj.RGP_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                chkOnHold.Checked = obj.On_Hold
                txtReason.Text = obj.Reason
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                txtLocation.Enabled = False
                cboDocType.SelectedValue = obj.Doc_Type
                chkAgainst_Sale.Checked = obj.Against_Sale
                ddlBilling.SelectedValue = obj.Billing
                lblDocumentAmt.Text = clsCommon.myFormat(obj.Document_Amount)
                txtVehicleNo.Text = obj.VehicleNo
                txtGPNo.Text = obj.GPNo
                txtGPDate.Value = obj.GPDate
                txtLocation.Value = obj.Location
                lblLocation.Text = obj.LocationName
                txtDeliveredBy.Value = obj.Delivered_By
                lblDeliveredBy.Text = obj.Delivered_ByName
                txtDepartment.Value = obj.Department
                fndCostCentre.Value = obj.CostCentre
                txtCostCentre.Text = obj.CostCentreDesc
                chlCust.Checked = obj.Against_Customer
                If obj.Is_Non_Inventory = 1 Then
                    chkNonInventoryItem.Checked = True
                Else
                    chkNonInventoryItem.Checked = False
                End If
                lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsRGPDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.RGP_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLastRGPNRGP).Value = objTr.Last_RGP_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = objTr.Last_RGP_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objTr.Specification
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                    End If
                End If

                If (chlCust.Checked) Or chkAgainst_Sale.Checked Then
                    RadLabel2.Text = "Customer No"
                Else
                    RadLabel2.Text = "Vendor No"
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.RGP_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.RGP_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.RGP_No)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
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
                SaveData(True)
                If (clsRGPHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        print()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsRGPHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-GP"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_RGP_HEAD where RGP_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select RGP_No as Code,RGP_Date as Date,Vendor_Code as [Vendor Code], Vendor_Name as Vendor,Document_Amount as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_RGP_HEAD"

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("RGPFNDR", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        If chkAgainst_Sale.Checked = True Or chlCust.Checked = True Then
            Dim qry As String = "select Cust_Code as [Code],Customer_Name  as [Name] from TSPL_CUSTOMER_MASTER "
            txtVendorNo.Value = clsCommon.ShowSelectForm("RGPCustFNDer", qry, "Code", "status='N'", txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtVendorNo.Value + "'"))

        Else

            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER"
            txtVendorNo.Value = clsCommon.ShowSelectForm("RGPVeFNDer", qry, "Code", "", txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"))
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colUnit) Then
                    If chkNonInventoryItem.Checked Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    End If
                End If
                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtLocation.Value = obj.Code
        '    lblLocation.Text = obj.Name
        'Else
        '    txtLocation.Value = ""
        '    lblLocation.Text = ""
        'End If

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtLocation.Value = clsCommon.ShowSelectForm("Location_MasterFD", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))



    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub

    Public Sub print()
        Try

            Dim type As String = cboDocType.Text
            Dim strDep As String = txtDepartment.Value
            If clsCommon.myLen(txtDepartment.Value) > 0 Then
                strDep = " Seg_No  ='3' and "
            Else
                strDep = ""
            End If
            Dim strqry As String = "SELECT TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address, ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')+ Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End as Customer_Phone,TSPL_RGP_HEAD.Created_By,TSPL_RGP_HEAD.Modify_By, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.VehicleNo, convert(date,TSPL_RGP_HEAD.RGP_Date) as RGP_Date , TSPL_RGP_HEAD.Doc_Type, TSPL_RGP_HEAD.Vendor_Code, " & _
                     " TSPL_RGP_HEAD.Vendor_Name, TSPL_RGP_HEAD.VehicleNo, TSPL_RGP_HEAD.GPNo, TSPL_RGP_HEAD.GPDate, TSPL_RGP_HEAD.Reason, " & _
                     " TSPL_RGP_HEAD.Remarks, TSPL_RGP_HEAD.Posting_Date, TSPL_RGP_HEAD.comp_code, TSPL_RGP_HEAD.Location, TSPL_RGP_HEAD.Mode_Of_Transport, TSPL_RGP_HEAD.Cash_Memo_Detail, " & _
                     " TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as Add1" & _
                     " , TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " & _
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_RGP_DETAIL.Line_No, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Item_Desc, " & _
                     " TSPL_RGP_DETAIL.RGP_Qty, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.Item_Cost, TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Specification, "
            If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
                strqry += " TSPL_VENDOR_MASTER.Add1 AS venadd1, TSPL_VENDOR_MASTER.Add2 AS venadd2, TSPL_VENDOR_MASTER.Add3 AS venadd3, " & _
                     " TSPL_VENDOR_MASTER.City_Code_Desc as vencity,TSPL_VENDOR_MASTER.Lst_No,TSPL_VENDOR_MASTER.CST"
            Else
                strqry += "  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end   as Customer_address,TSPL_CUSTOMER_MASTER.Tin_No as customer_Tin_No,TSPL_CUSTOMER_MASTER.Remarks1  +case when len(TSPL_CUSTOMER_MASTER.Remarks2 )>0 then ', '+TSPL_CUSTOMER_MASTER.Remarks2 else ''  end   as Customer_Remarks,TSPL_CUSTOMER_MASTER.Add1 AS venadd1, TSPL_CUSTOMER_MASTER.Add2 AS venadd2, TSPL_CUSTOMER_MASTER.Add3 AS venadd3,  TSPL_CITY_MASTER.City_Name as vencity,TSPL_CUSTOMER_MASTER.Lst_No "
            End If

            strqry += " ,'" + type + "' as RGPType ,TSPL_GL_SEGMENT_CODE.Description as Department,case when  TSPL_RGP_HEAD.billing='Y' then 'Yes' when  TSPL_RGP_HEAD.billing='N' then 'No' else '' end as Billing "
            strqry += " FROM TSPL_RGP_HEAD INNER JOIN "
            strqry += " TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No = TSPL_RGP_DETAIL.RGP_No  "
            If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
                strqry += " INNER JOIN TSPL_VENDOR_MASTER ON TSPL_RGP_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  "

            Else
                strqry += " INNER JOIN tspl_customer_master ON TSPL_RGP_HEAD.Vendor_Code = tspl_customer_master.cust_code  "
                strqry += " left outer JOIN tspl_city_master ON tspl_customer_master.city_code = tspl_city_master.city_code  "

            End If
            strqry += "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code LEFT OUTER JOIN  TSPL_GL_SEGMENT_CODE on TSPL_RGP_HEAD.Department = TSPL_GL_SEGMENT_CODE.Segment_code  where   " & strDep & " TSPL_RGP_HEAD.RGP_No='" + txtDocNo.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            Dim frmCRV As New frmCrystalReportViewer()
            If (type = "Returnable Gate Pass") Then
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptRGPNew", "RGP Report")
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptNRGP", "NRGP Report")
            End If
            frmCRV = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtDeliveredBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDeliveredBy._MYValidating

        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployeeInGL_Segment_Code(txtDeliveredBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtDeliveredBy.Value = obj.EMP_CODE
            lblDeliveredBy.Text = obj.Emp_Name
        Else
            txtDeliveredBy.Value = ""
            lblDeliveredBy.Text = ""
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged, ddlBilling.SelectedIndexChanged
        If Not IsformLoad Then
            HideUnhideColumn()
            If (cboDocType.SelectedValue = "NRGP") Then
                'ddlBilling.Visible = True
                'lblBilling.Visible = True
                chkAgainst_Sale.Enabled = True

            Else
                'ddlBilling.Visible = False
                'lblBilling.Visible = False
                chkAgainst_Sale.Enabled = False
                chkAgainst_Sale.Checked = False
                RadLabel2.Text = "Vendor No"
            End If
        End If


    End Sub

    Private Sub HideUnhideColumn()
        If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
            gv1.Columns(colLastRGPNRGP).IsVisible = True
            gv1.Columns(colDate).IsVisible = True
            chkAgainst_Sale.Enabled = False
            chkAgainst_Sale.Checked = False

        Else
            gv1.Columns(colLastRGPNRGP).IsVisible = False
            gv1.Columns(colDate).IsVisible = False
            chkAgainst_Sale.Enabled = True

        End If
    End Sub

    Private Sub txtDepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepartment._MYValidating
        Dim qry As String = "select Segment_code as [Code], Description as [Name] from TSPL_GL_SEGMENT_CODE "
        Dim WhrCls As String = "Seg_No  ='3'"
        txtDepartment.Value = clsCommon.ShowSelectForm("EmployeeFinder", qry, "Code", WhrCls, txtDepartment.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtDepartment.Value) > 0 Then
            qry += "where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblDepartment.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If
        Else
            txtDepartment.Value = ""
            lblDepartment.Text = ""
        End If
    End Sub

    Private Sub chkAgainst_Sale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAgainst_Sale.Click

        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        If (chkAgainst_Sale.Checked) Then
            RadLabel2.Text = "Vendor No"
        Else
            RadLabel2.Text = "Customer No"
        End If
        'Dim qry As String = "select Cust_Code as [Code],Customer_Name  as [Name] from TSPL_CUSTOMER_MASTER "
        'txtVendorNo.Value = clsCommon.ShowSelectForm("RGPCustFNDer", qry, "Code", "status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        'lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtVendorNo.Value + "'"))

    End Sub

    Private Sub chkNonInventoryItem_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkNonInventoryItem.ToggleStateChanged
        ControlNonInventory(True)
    End Sub

    Private Sub ControlNonInventory(ByVal Ischecked As Boolean)
        If Not isInsideLoadData Then
            If Ischecked Then
                LoadBlankGrid()
                gv1.Rows.AddNew()
            End If
        End If
    End Sub

    Private Sub fndCostCentre__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCostCentre._MYValidating
        Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        Dim WhrCls As String = " seg_no <>'7'  "
        fndCostCentre.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, fndCostCentre.Value, "", isButtonClicked)
        txtCostCentre.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + fndCostCentre.Value + "'")
    End Sub

    Private Sub chlCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chlCust.Click
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        If (chlCust.Checked) Then
            RadLabel2.Text = "Vendor No"
        Else
            RadLabel2.Text = "Customer No"
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsRGPHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        'UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtLocation.Value
        UcItemBalance1.LocationName = lblLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = False
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub
End Class
