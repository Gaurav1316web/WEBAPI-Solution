''22/09/2013---Created by --[Pradeep Sharma]-- Ticket no : BM00000000485
'----------------BM00000003534

Imports common
Imports Microsoft.Office.Interop

Public Class frmProductionReturn
    Inherits FrmMainTranScreen
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colIs_Serialized_Item As String = "SerializedItem"
    Const colIs_Auto_PickSerialized As String = "Auto_Serial_No"
    Const colserialno As String = "SerialNo"
    Const colLineNo As String = "LNO"
    Const colIssueCode As String = "IssueCode"
    Const colPLCode As String = "PLCode"
    Const colBOMCode As String = "BOMCode"
    Const colICode As String = "ICODE"
    Const colIDesc As String = "IDesc"
    Const colBatchQty As String = "BatchQty"
    Const colReqQty As String = "ReqQty"
    Const colIssueQty As String = "IssueQty"
    Const colUnit As String = "Unit"
    Const colReturnQty As String = "ReturnQty"
    Const colWastageQty As String = "WastageQty"
    Const colConsumedQty As String = "ConsumedQty"
    Const colRemarks As String = "Remarks"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn

    Public strDocumentNo As String = ""
    Dim MOActive As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmProductionReturnSTD Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionReturnSTD)
        ElseIf formtype = clsUserMgtCode.frmProductionReturnPep Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionReturnPep)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        SetLength()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtReqNo.MyMaxLength = 30
        txtDesc.MaxLength = 100
        txtComment.MaxLength = 500
    End Sub

    Sub BlankAllControls()
        txtReqNo.Value = Nothing
        txtDesc.Text = ""
        txtComment.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtFromLocation.Value = ""
        lblFromLocation.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtExpireDate.Value = txtDate.Value
        txtReturnedBy.Value = ""
        lblReturnedBy.Text = ""
        txtReturnedTo.Value = ""
        lblReturnedTo.Text = ""
    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoPLCode As New GridViewTextBoxColumn()
        repoPLCode.FormatString = ""
        repoPLCode.HeaderText = "Production Line Code"
        repoPLCode.Name = colPLCode
        repoPLCode.Width = 100
        repoPLCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPLCode)

        Dim repoBOMCode As New GridViewTextBoxColumn()
        repoBOMCode.FormatString = ""
        repoBOMCode.HeaderText = "BOM Code"
        repoBOMCode.Name = colBOMCode
        repoBOMCode.Width = 100
        repoBOMCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBOMCode)

        Dim repoIssueCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueCode.FormatString = ""
        repoIssueCode.HeaderText = "Issue Code"
        repoIssueCode.Width = 100
        repoIssueCode.Name = colIssueCode
        repoIssueCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIssueCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc."
        repoIName.Name = colIDesc
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        '-----------------------serialized no--------------------------------
        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIs_Serialized_Item
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIs_Auto_PickSerialized
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo) '140

        Dim repoSerNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSerNo.HeaderText = "Serial No."
        repoSerNo.Name = colserialno
        repoSerNo.ReadOnly = True
        repoSerNo.IsVisible = False
        repoSerNo.Width = 80
        repoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSerNo) '140
        '==============================================================================

        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Batch Quantity"
        repoBalQty.Name = colBatchQty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Req. Quantity"
        repoQty.Name = colReqQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoIssueQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIssueQty = New GridViewDecimalColumn()
        repoIssueQty.FormatString = ""
        repoIssueQty.HeaderText = "Issue Quantity"
        repoIssueQty.Name = colIssueQty
        repoIssueQty.Width = 80
        repoIssueQty.Minimum = 0
        repoIssueQty.ReadOnly = True
        repoIssueQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoIssueQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = True
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Return Qty."
        repoRate.Name = colReturnQty
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Wastage Qty."
        repoAmt.Name = colWastageQty
        repoAmt.Width = 80
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoVendorCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorCode = New GridViewTextBoxColumn()
        repoVendorCode.FormatString = ""
        repoVendorCode.HeaderText = "Consumed Qty"
        repoVendorCode.Name = colConsumedQty
        repoVendorCode.Width = 100
        repoVendorCode.ReadOnly = False
        repoVendorCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoVendorCode)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadBatchNo()
        Dim qry1 As String = " SELECT ISSUE_CODE AS [Code],DESCRIPTION AS [Description],convert(varchar(12),ISSUE_DATE,103) AS [Issue Date],ISSUED_BY AS [Issued By],LOCATION_CODE AS [Location Code],COMMENTS AS [Comments],POSTED AS [Posted],convert(varchar(12),POSTING_DATE,103) AS [Posting Date],TR_TYPE AS Type FROM TSPL_MF_ISSUE WHERE POSTED=1 "
        If MOActive = True Then
            qry1 += " AND TR_TYPE='MO'"
        Else
            qry1 += " AND TR_TYPE='BO'"
        End If
        cbgissue.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgissue.ValueMember = "Code"
        cbgissue.DisplayMember = "Description"
        cbgissue.MyShowHeadrText = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colReturnQty) OrElse e.Column Is gv1.Columns(colWastageQty) OrElse e.Column Is gv1.Columns(colConsumedQty) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        End If
                        If e.Column Is gv1.Columns(colReturnQty) OrElse e.Column Is gv1.Columns(colWastageQty) OrElse e.Column Is gv1.Columns(colConsumedQty) Then
                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colReturnQty).Value) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssueQty).Value) Then
                                clsCommon.MyMessageBoxShow("Returned Quantity can not be grater then Issue Quantity.")
                            ElseIf clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssueQty).Value) < (clsCommon.myCdbl(gv1.CurrentRow.Cells(colReturnQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colWastageQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colConsumedQty).Value)) Then
                                clsCommon.MyMessageBoxShow("Sum of Returned,Wastage And Consumed Quantity can not be grater then Issue Quantity. ")
                            End If
                        End If

                        '-------------------serialized item--------------------------
                        If e.Column Is gv1.Columns(colReturnQty) AndAlso Not clsCommon.myCBool(e.Column Is gv1.Columns(colIs_Auto_PickSerialized)) Then
                            OpenSerialItem()
                        End If
                        '=========================================================
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIs_Serialized_Item).Value) Then
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIDesc).Value)
            frm.strLocationCode = txtFromLocation.Value
            frm.strCurrDocNo = txtReqNo.Value
            frm.strCurrDocType = "PROD_RN"
            frm.strAgaintsDocNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colIssueCode).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colReturnQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()

            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
                gv1.CurrentRow.Cells(colserialno).Value = clsCommon.myCstr(frm.arr(0).Auto_Sr_No)
            End If
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "cboItemType.SelectedValue", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIDesc).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            gv1.CurrentRow.Cells(colIs_Serialized_Item).Value = obj.Is_Serial_Item
            gv1.CurrentRow.Cells(colIs_Auto_PickSerialized).Value = obj.Is_Pick_Auto_SrNo
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIDesc).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colIs_Serialized_Item).Value = Nothing
            gv1.CurrentRow.Cells(colIs_Auto_PickSerialized).Value = Nothing
            gv1.CurrentRow.Cells(colserialno).Value = ""

        End If
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colReqQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colReqQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colReturnQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colReturnQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colConsumedQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub UpdateCurrentRow()
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colReqQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colReturnQty).Value)
            Dim dblAmt As Double = (dblQty * dblRate)
            'gv1.CurrentRow.Cells(colWastageQty).Value = dblAmt
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        BlankAllControls()
        LoadBlankGrid()
        gv1.Rows.AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Focus()
        txtLocation.Enabled = True
        LoadBatchNo()
    End Sub

    Function AllowToSave() As Boolean
        Try
            '===================Added by preeti Gupta==============
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            '===========================================================
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select POSTED from TSPL_MF_RETURN where RETURN_CODE ='" + txtReqNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow("Transection already posted")
                    Return False
                End If
            End If
            If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select From Location.")
                txtFromLocation.Focus()
                Return False
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select To Location.")
                txtLocation.Focus()
                Return False
            End If
            If clsCommon.CompairString(txtLocation.Value, txtFromLocation.Value) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("From Location and To Location can not be Same.")
                txtLocation.Focus()
                Return False
            End If

            If clsCommon.myLen(txtReturnedBy.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Returned By.")
                txtReturnedBy.Focus()
                Return False
            End If
            If clsCommon.myLen(txtReturnedTo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Returned To.")
                txtReturnedTo.Focus()
                Return False
            End If
            Dim arrICode As New List(Of String)()

            Dim ii As Integer = 0
            For Each dr As GridViewRowInfo In gv1.Rows
                Dim strICode As String = clsCommon.myCstr(dr.Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(dr.Cells(colIDesc).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    'For jj As Integer = 0 To gv1.Rows.Count - 1
                    '    If (ii = jj) Then
                    '        Continue For
                    '    End If
                    '    If (clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) Then
                    '        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                    '        Return False
                    '    End If
                    'Next
                    If clsCommon.myCdbl(dr.Cells(colReturnQty).Value) > clsCommon.myCdbl(dr.Cells(colIssueQty).Value) Then
                        clsCommon.MyMessageBoxShow("Returned Quantity can not be grater then Issue Quantity in Row No " + (dr.Index + 1).ToString())
                        Return False
                    End If
                    If clsCommon.myCdbl(dr.Cells(colIssueQty).Value) < (clsCommon.myCdbl(dr.Cells(colReturnQty).Value) + clsCommon.myCdbl(dr.Cells(colWastageQty).Value) + clsCommon.myCdbl(dr.Cells(colConsumedQty).Value)) Then
                        clsCommon.MyMessageBoxShow("Sum of Returned,Wastage And Consumed Quantity can not be grater then Issue Quantity in Row No " + (dr.Index + 1).ToString())
                        Return False
                    End If
                    Dim availQty As Double = 0
                    Dim reqQty As Double = 0

                    availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Cells(colICode).Value, Me.txtFromLocation.Value, Me.txtReqNo.Value, txtDate.Value, Nothing, dr.Cells(colUnit).Value)
                    reqQty = dr.Cells(colIssueQty).Value ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
                    If availQty < reqQty Then
                        Throw New Exception("Item Code: " & dr.Cells(colICode).Value & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                    End If

                    Dim is_serialized As String = Nothing
                    Dim serialno As String = Nothing
                    Dim dblQty As Decimal = Nothing

                    is_serialized = clsCommon.myCstr(IIf(dr.Cells(colIs_Serialized_Item).Value = True, "1", "0"))
                    serialno = clsCommon.myCstr(dr.Cells(colserialno).Value)
                    dblQty = clsCommon.myCdbl(dr.Cells(colReturnQty).Value)

                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIs_Serialized_Item).Value) Then
                        Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                        If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                            common.clsCommon.MyMessageBoxShow("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                    End If


                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                End If

                ii += 1
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProductionReturn()
                If MOActive = True Then
                    obj.TR_TYPE = "MO"
                Else
                    obj.TR_TYPE = "BO"
                End If

                obj.RETURN_CODE = txtReqNo.Value
                obj.RETURN_DATE = txtDate.Value
                If txtExpireDate.Checked Then
                    obj.EXP_DATE = txtExpireDate.Value
                End If
                obj.DESCRIPTION = txtDesc.Text
                obj.LOCATION_CODE = txtLocation.Value
                obj.LOCATION_CODE_FROM = txtFromLocation.Value
                obj.COMMENTS = txtComment.Text
                obj.RETURNED_BY = txtReturnedBy.Value
                obj.RETURNED_TO = txtReturnedTo.Value
                obj.ObjList = New List(Of clsProductionReturnDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New clsProductionReturnDetail()
                        objTr.ISSUE_CODE = clsCommon.myCstr(grow.Cells(colIssueCode).Value)
                        objTr.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colPLCode).Value)
                        objTr.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        objTr.ITEM_CODE = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colIDesc).Value)
                        objTr.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                        objTr.REQ_QTY = clsCommon.myCdbl(grow.Cells(colReqQty).Value)
                        objTr.ISSUE_QTY = clsCommon.myCdbl(grow.Cells(colIssueQty).Value)
                        objTr.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.RETURN_QTY = clsCommon.myCdbl(grow.Cells(colReturnQty).Value)
                        objTr.WASTAGE_QTY = clsCommon.myCdbl(grow.Cells(colWastageQty).Value)
                        objTr.CONSUMED_QTY = clsCommon.myCdbl(grow.Cells(colConsumedQty).Value)
                        objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.TR_TYPE = obj.TR_TYPE
                        objTr.Doc_Date = txtDate.Value
                        objTr.To_Location = txtLocation.Value
                        objTr.From_Location = txtFromLocation.Value
                        objTr.IsPosted = ChekBtnPost
                        
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        obj.ObjList.Add(objTr)
                    End If
                Next

                If (obj.ObjList Is Nothing OrElse obj.ObjList.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, Nothing)) Then
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.RETURN_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            Dim obj As New clsProductionReturn()
            obj = obj.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RETURN_CODE) > 0) Then
                BlankAllControls()
                LoadBlankGrid()
                If obj.POSTED Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                If obj.TR_TYPE = "MO" Then
                    MOActive = True
                Else
                    MOActive = False
                End If

                CheckedIssueNo(obj.RETURN_CODE)
                txtReqNo.Value = obj.RETURN_CODE
                txtDate.Value = obj.RETURN_DATE
                UsLock1.Status = obj.POSTED
                txtExpireDate.Checked = IIf(obj.EXP_DATE.Year < 2000, False, True)
                If txtExpireDate.Checked Then
                    txtExpireDate.Value = obj.EXP_DATE
                End If
                txtDesc.Text = obj.DESCRIPTION
                txtLocation.Value = obj.LOCATION_CODE
                lblLocation.Text = obj.LOCATION_NAME
                txtFromLocation.Value = obj.LOCATION_CODE_FROM
                lblFromLocation.Text = obj.LOCATION_FROM_NAME
                txtComment.Text = obj.COMMENTS
                txtReturnedBy.Value = obj.RETURNED_BY
                lblReturnedBy.Text = obj.RETURNED_BY_NAME
                txtReturnedTo.Value = obj.RETURNED_TO
                lblReturnedTo.Text = obj.RETURNED_TO_NAME
                For Each objTr As clsProductionReturnDetail In obj.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIssueCode).Value = objTr.ISSUE_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPLCode).Value = objTr.PRODUCTION_LINE_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBOMCode).Value = objTr.BOM_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ITEM_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIDesc).Value = objTr.ITEM_DESCRIPTION
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchQty).Value = objTr.BATCH_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = objTr.REQ_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIssueQty).Value = objTr.ISSUE_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReturnQty).Value = objTr.RETURN_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWastageQty).Value = objTr.WASTAGE_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colConsumedQty).Value = objTr.CONSUMED_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.REMARKS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Auto_PickSerialized).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.ITEM_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Serialized_Item).Value = clsItemMaster.IsSerializeItem(objTr.ITEM_CODE)

                Next
                If obj.POSTED = ERPTransactionStatus.Pending Then
                    gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    'Sub CheckedIssueNo(ByVal strCode As String)
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select distinct ISSUE_CODE from TSPL_MF_RETURN_DETAIL where RETURN_CODE ='" + strCode + "' ")
    '    Dim lst As New ArrayList
    '    For Each dr As DataRow In dt.Rows
    '        lst.Add(clsCommon.myCstr(dr("ISSUE_CODE")))
    '    Next
    '    cbgissue.CheckedValue = lst
    'End Sub
    Sub CheckedIssueNo(ByVal strCode As String)
        Dim qry As String = ""

        qry += " SELECT ISSUE_CODE AS [Code],DESCRIPTION AS [Description],convert(varchar(12),ISSUE_DATE,103) AS [Issue Date],ISSUED_BY AS [Issued By],LOCATION_CODE AS [Location Code],COMMENTS AS [Comments],POSTED AS [Posted],convert(varchar(12),POSTING_DATE,103) AS [Posting Date],TR_TYPE AS Type FROM TSPL_MF_ISSUE "
        qry += " WHERE  ISSUE_CODE IN ( Select distinct ISSUE_CODE from TSPL_MF_RETURN_DETAIL where RETURN_CODE ='" + strCode + "'  )"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim lst As New ArrayList
        For Each dr As DataRow In dt.Rows
            lst.Add(clsCommon.myCstr(dr("Code")))
        Next
        cbgissue.DataSource = dt
        cbgissue.ValueMember = "Code"
        cbgissue.DisplayMember = "Description"
        cbgissue.MyShowHeadrText = True
        cbgissue.CheckedValue = lst
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
                If (clsProductionReturn.PostData(txtReqNo.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtReqNo.Value, NavigatorType.Current)
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
                If (clsProductionReturn.DeleteData(txtReqNo.Value)) Then
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtReqNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtReqNo._MYNavigator
        Try
            Dim qst As String = ""
            If ClsMFSeetings.Get_MO_BO_Setting() = True Then
                qst = "select count(*) from TSPL_MF_RETURN where TR_TYPE='MO' "
            Else
                qst = "select count(*) from TSPL_MF_RETURN where TR_TYPE='BO'"
            End If
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtReqNo.MyReadOnly = False
            Else
                txtReqNo.MyReadOnly = True
            End If
            LoadData(txtReqNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        Dim qry As String = " select RETURN_CODE as [Code],RETURN_DATE as [Date],EXP_DATE AS [Exp. Date], DESCRIPTION as [Description], (case when POSTED = '0' then 'Pending' else 'Approved' end) as [Posted],RETURNED_BY as [Returned By],COMMENTS AS [Comments],LOCATION_CODE AS [Location Code]  from TSPL_MF_RETURN "
        Dim WHRCLS As String
        If ClsMFSeetings.Get_MO_BO_Setting() Then
            WHRCLS = " TR_TYPE='MO'"
        Else
            WHRCLS = " TR_TYPE='BO'"
        End If
        LoadData(clsCommon.ShowSelectForm("TSPL_MF_RETURN", qry, "Code", WHRCLS, txtReqNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmProductionReturn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIDesc)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtReqNo.Value = "" Then
            myMessages.blankValue("Requisition Number")
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()
        Try
            Dim qry As String = " select TSPL_MF_RETURN.RETURN_CODE as [RETURN_CODE], convert(varchar,TSPL_MF_RETURN.RETURN_DATE,103) as [RETURN_DATE], (case when year(TSPL_MF_RETURN.EXP_DATE)>1 then convert(varchar,TSPL_MF_RETURN.EXP_DATE,103) else '' end) as [EXP_DATE],TSPL_MF_RETURN.DESCRIPTION as [Description], TSPL_MF_RETURN.COMMENTS as [COMMENTS], "
            'qry += " TSPL_MF_RETURN.REQUESTED_BY,TSPL_MF_RETURN.LOCATION_CODE,"
            qry += " TSPL_MF_RETURN_DETAIL.ITEM_CODE as [ITEM_CODE], TSPL_MF_RETURN_DETAIL.ITEM_DESCRIPTION as [ITEM_DESCRIPTION],TSPL_MF_RETURN_DETAIL.BATCH_QTY as [BATCH_QTY], TSPL_MF_RETURN_DETAIL.REMARKS as [Remarks], TSPL_MF_RETURN_DETAIL.UNIT_CODE as [UNIT_CODE], TSPL_MF_RETURN_DETAIL.REQ_QTY as [REQ_QTY], TSPL_MF_RETURN_DETAIL.ISSUE_QTY as [ISSUE_QTY],TSPL_MF_RETURN_DETAIL.ISSUE_CODE as [ISSUE_CODE],TSPL_MF_RETURN_DETAIL.RETURN_QTY as [RETURN_QTY],TSPL_MF_RETURN_DETAIL.WASTAGE_QTY as [WASTAGE_QTY],TSPL_MF_RETURN_DETAIL.CONSUMED_QTY as [CONSUMED_QTY],"
            qry += " TSPL_EMPLOYEE_MASTER.Emp_Name as [REQUESTED_USER],TSPL_LOCATION_MASTER.Location_Desc as [Location_Desc], '' as [AuthorizeBy] "
            qry += " from TSPL_MF_RETURN left outer join TSPL_MF_RETURN_DETAIL on TSPL_MF_RETURN.RETURN_CODE =TSPL_MF_RETURN_DETAIL.RETURN_CODE "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MF_RETURN.RETURNED_BY "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MF_RETURN.LOCATION_CODE"
            qry += " where 2=2 "
            If txtReqNo.Value <> "" Then
                qry += " and  TSPL_MF_RETURN.RETURN_CODE = '" + txtReqNo.Value + "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "ProductionReturn", "Production Return")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub txtReturnedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReturnedBy._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtReturnedBy.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtReturnedBy.Value = OBJEMP.EMP_CODE
                Me.lblReturnedBy.Text = OBJEMP.Emp_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

    'Public Sub SendMail()
    '    Try
    '        If Not objCommonVar.IsMailSend Then
    '            Exit Sub
    '        End If

    '        Dim ArrReceivers As New List(Of String)
    '        Dim ArrUsers As New List(Of String)
    '        Dim no As Integer = 0
    '        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,TSPL_REQUISITION_HEAD.Requisition_Date ,TSPL_REQUISITION_HEAD.Expire_Date ,TSPL_REQUISITION_HEAD.Require_Date ,TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By ,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty,(select SUM(Item_Qty)from TSPL_ITEM_LOCATION_DETAILS where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code)as AvaiQty  ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,TSPL_REQUISITION_HEAD.Approvel_Level_Required"
    '        qry += " , case when TSPL_REQUISITION_HEAD.Status =1 then  user2.User_Name else '' end  as AuthorizeBy ,"
    '        qry += " TSPL_REQUISITION_HEAD.Request_By,TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location ,TSPL_COMPANY_MASTER.Add1  from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"
    '        qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        Dim ReqNo As String = dt.Rows(0)("Requisition_Id").ToString
    '        Dim Reqdate As String = dt.Rows(0)("Requisition_Date").ToString
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            If dt.Rows(i)("vendor_name").ToString() <> "" Then
    '                no = no + 1
    '            End If
    '        Next
    '        If dt.Rows(0)("Approvel_Level_Required").ToString <> "" Then
    '            Dim level As String = dt.Rows(0)("Approvel_Level_Required").ToString
    '            Dim Query As String = "select E_mail,user_code from TSPL_USER_MASTER  where level <=" + level + " "
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Query)
    '            For Each dr As DataRow In dt1.Rows
    '                If clsCommon.myLen(dr("E_mail")) > 0 Then
    '                    ArrReceivers.Add(clsCommon.myCstr(dr("E_mail")))
    '                    ArrUsers.Add(clsCommon.myCstr(dr("user_code")))
    '                End If
    '            Next
    '        End If
    '        Dim strreportPath As String = ""
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        strreportPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition")
    '        frmCRV = Nothing
    '        If ArrReceivers.Count > 0 Then
    '            sendEMailThroughOUTLOOK(strreportPath, ArrReceivers, ArrUsers, ReqNo, Reqdate)

    '            clsCommon.MyMessageBoxShow("Mail has been sent succcessfully.")
    '        Else
    '            Throw New Exception("No Recipients found to mail.")
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Public Sub sendEMailThroughOUTLOOK(ByVal strPath As String, ByVal arrReceivers As List(Of String), ByVal arrUsers As List(Of String), ByVal ReqNo As String, ByVal ReqDate As String)
    '    Try
    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        Dim sDisplayName As [String] = "MyAttachment"
    '        oMsg.Subject = "Approval required for Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + ""

    '        oMsg.Body = "Please find the attached Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + " for your kind approval." & vbCrLf & "" & vbCrLf & "Best Regards" & vbCrLf & "" + objCommonVar.CurrentUser + ""
    '        Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '        Dim iAttachType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '        If clsCommon.myLen(strPath) > 0 Then
    '            Dim oAttach As Outlook.Attachment = oMsg.Attachments.Add(strPath, iAttachType, iPosition, sDisplayName)
    '        End If
    '        For ii As Integer = 0 To arrReceivers.Count - 1
    '            oMsg.Recipients.Add(arrReceivers(ii))
    '        Next
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnShoeItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShoeItems.Click
        Dim qry As String = " SELECT T1.ISSUE_CODE,T1.ITEM_CODE,T1.UNIT_CODE,T1.BATCH_QTY,T1.REQ_QTY,T1.ISSUE_QTY,T1.ITEM_DESCRIPTION,T1.REMARKS,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE FROM TSPL_MF_ISSUE_DETAIL T1 "
        qry += " WHERE T1.ISSUE_CODE IN (" + clsCommon.GetMulcallString(cbgissue.CheckedValue) + ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv1.DataSource = Nothing
        LoadBlankGrid()
        Dim Count As Int16 = 0
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                gv1.Rows.AddNew()
                Count = Count + 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(Count)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIssueCode).Value = clsCommon.myCstr(DR("ISSUE_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPLCode).Value = clsCommon.myCstr(DR("PRODUCTION_LINE_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(DR("BOM_CODE"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(DR("ITEM_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIDesc).Value = clsCommon.myCstr(DR("ITEM_DESCRIPTION"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCdbl(DR("BATCH_QTY"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCdbl(DR("REQ_QTY"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIssueQty).Value = clsCommon.myCdbl(DR("ISSUE_QTY"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(DR("UNIT_CODE"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colReturnQty).Value = clsCommon.myCdbl(DR("REQ_CODE"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colWastageQty).Value = clsCommon.myCdbl(DR("REQ_CODE"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colConsumedQty).Value = clsCommon.myCdbl(DR("REQ_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(DR("REMARKS"))
            Next
        Else
            clsCommon.MyMessageBoxShow("No Data Found to display.")
        End If
        ' gv1.Rows.AddNew()
    End Sub

    Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtFromLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
            lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtReturnedTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReturnedTo._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtReturnedTo.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtReturnedTo.Value = OBJEMP.EMP_CODE
                Me.lblReturnedTo.Text = OBJEMP.Emp_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub
End Class
