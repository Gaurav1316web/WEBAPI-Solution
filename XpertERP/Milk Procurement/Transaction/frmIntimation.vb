' Created By Priti sharma
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class frmIntimation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colSlNo As String = "SLNO"
    Public Const colSealNo As String = "SealNo"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public strDocCode As String = ""
    Public isCellValueChangedOpen = False
    Public strLoggedInTo As String = String.Empty ' It Will Store Either MCC or Plant as Login Location
    Public strLoginMccOrPlantCode As String = String.Empty 'It Will store the Location Code of Currently Logged In Mcc or Pant
    Public strLoginMccOrPlantDesc As String = String.Empty 'It Will store the Location Desc of Currently Logged In Mcc or Pant
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public obj As clsIntimation = Nothing


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub fndLocationBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocationBulk._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndLocationBulk.Value = clsLocation.getFinder("(type='PLANT' or Location_category='MCC')" & strLocations, fndLocationBulk.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationBulk.Value) > 0 Then
            lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        Else
            lblLocationDecBulk.Text = ""
        End If
        strLocations = Nothing
    End Sub
    Sub UpdateCurrentLoginStatus()
        Dim strQry As String = "select case when isnull(Type,'')='PLANT' then type when ISNULL(Location_Category ,'')='MCC' then  Location_Category else 'NA'end    from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
        strLoggedInTo = clsDBFuncationality.getSingleValue(strQry)
        If clsCommon.CompairString(strLoggedInTo, "MCC") = CompairStringResult.Equal Or clsCommon.CompairString(strLoggedInTo, "PLANT") = CompairStringResult.Equal Then
            Dim qry As String = "select location_Code  from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
            strLoginMccOrPlantCode = clsDBFuncationality.getSingleValue(qry)
            strLoginMccOrPlantDesc = clsLocation.GetName(strLoginMccOrPlantCode, Nothing)
        Else
            strLoginMccOrPlantCode = "NA"
            strLoginMccOrPlantDesc = "NA"
        End If
        'lblLoggedMccOrPlantName.Text = strLoginMccOrPlantDesc & "(Location: " & strLoggedInTo & ")"
        strQry = Nothing
    End Sub
    Private Sub fndVendorBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendorBulk._MYValidating
        fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type in ('A','B') ", fndVendorBulk.Value, isButtonClicked)
        If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
            lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)
        Else
            lblVendorNameBulk.Text = ""
        End If
    End Sub
    Private Sub txtTankerNoBulk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete OrElse Asc(e.KeyChar) = 22 OrElse Asc(e.KeyChar) = 3 Then
        Else
            e.Handled = True
        End If
    End Sub
    Sub reset()
        txtContractorCode.Value = ""
        fndTankerNo.Enabled = True
        lblTankerNo.Text = ""
        txtMilktypeCode.Value = ""
        lblMilkTypeCode.Text = ""
        lblMilkType.Text = ""
        cmbStatus.SelectedValue = ""
        txtDispatchCentreCode.Text = ""
        fndLocationBulk.Enabled = True
        fndVendorBulk.Enabled = True
        fndTankerNo.Value = ""
        fndGateEntryNO.Value = ""
        btnReverse.Visible = False
        fndGateEntryNO.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
        txtSupplierChallanNo.Text = ""
        txtSupplierCode.Value = ""
        lblSupplierName.Text = ""
        txtSupplierChallanNo.ReadOnly = False
        dtpChallanDateBulk.Value = dt
        fndLocationBulk.Value = ""
        lblLocationDecBulk.Text = ""
        fndVendorBulk.Value = ""
        lblVendorNameBulk.Text = ""

        loadBlankGv()
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnPost.Enabled = False
        btnDelete.Enabled = False
        dtpDateAndTimeBulk.Value = dt
        dtpDateAndTimeBulk.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        UpdateCurrentLoginStatus()

        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy"
        End If
        '==========================================================
        dt = Nothing
        loadBlankGv()
        fndLocationBulk.Value = clsIntimation.getUsersDefaultLocation()
        lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        RadPageView1.SelectedPage = RadPageViewPage1
        FindAndRestoreGridLayout(Me)
        'FindAndSetTabStopFalse(Me)
    End Sub
    Sub LoadStatus()
        cmbStatus.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Diverted"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Return"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Accept"
        dt.Rows.Add(dr)

        cmbStatus.DataSource = dt
        cmbStatus.DisplayMember = "Name"
        cmbStatus.ValueMember = "Code"
    End Sub
    Sub loadBlankGv()
        loadBlankGvItemBulk()
    End Sub
    Sub loadBlankGvItemBulk()
        gvItemBulk.Tag = "Bulk"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing
        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        'gvItemBulk.Columns(colItemCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gvItemBulk.Columns(colItemCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colItemCode).ReadOnly = True

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 150
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 200
        gvItemBulk.Columns(colChamberDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 50
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Challan Qty")
        gvItemBulk.Columns(colQty).Width = 80
        gvItemBulk.Columns(colQty).ReadOnly = False

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 100
        gvItemBulk.Columns(colFat).ReadOnly = False
        gvItemBulk.Columns(colFat).IsVisible = True

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 100
        gvItemBulk.Columns(colSNF).ReadOnly = False
        gvItemBulk.Columns(colSNF).IsVisible = True

        gvItemBulk.Columns.Add(colFatKG, "FAT (KG)")
        gvItemBulk.Columns(colFatKG).Width = 100
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF (KG)")
        gvItemBulk.Columns(colSNFKG).Width = 100
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False


        'gvItemBulk.Rows.AddNew()
        'gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        RadGroupBox1.Width = Me.Width - 200
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub


    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub FrmGateEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gvItemBulk.CurrentCell IsNot Nothing Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub FrmGateEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetUserMgmtNew()
        LoadStatus()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S to Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C to Close The Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P To Post Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

    End Sub
    Public Sub SetUserMgmtNew()
        Try
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmIntimation)
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")

            End If
            btnSave.Visible = MyBase.isModifyFlag
            btnDelete.Visible = MyBase.isDeleteFlag
            btnPost.Visible = MyBase.isPostFlag
            btnReverse.Visible = False

            'If MyBase.isReverse Then
            '    btnReverse.Enabled = True
            'Else
            '    btnReverse.Enabled = False
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsIntimation.ReverseAndUnpost(fndGateEntryNO.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndGateEntryNO.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvItemBulk.Columns(colQty) Then
                If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colQty).Value) > 0 Then
                    UpdateTotal()
                End If
            ElseIf e.Column Is gvItemBulk.Columns(colFat) Or e.Column Is gvItemBulk.Columns(colQty) Then
                gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
            ElseIf e.Column Is gvItemBulk.Columns(colSNF) Or e.Column Is gvItemBulk.Columns(colQty) Then
                gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
            End If
        End If
        isCellValueChangedOpen = False
    End Sub
    Private Sub fndChallanNoMcc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim arr As List(Of String) = New List(Of String)
        If clsCommon.myLen(fndGateEntryNO.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter Intimation No To delete ", Me.Text)
        Else
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
            If isUsed > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Gate Entry No is in use", Me.Text)
                Exit Sub
            End If
            If myMessages.deleteConfirm() Then
                arr.Add(fndGateEntryNO.Value)
                If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmGateEntry, Nothing) Then
                    If clsIntimation.deleteData(fndGateEntryNO.Value, Nothing) Then
                        reset()
                        myMessages.delete()
                    End If
                End If
            End If
        End If
    End Sub
    Function allowToSave() As Boolean
        Try

            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                errorControl.SetError(fndLocationBulk, "Please select the Tanker.It is mandatory ")
                fndTankerNo.Focus()
                Throw New Exception("Please select the Tanker.It is mandatory")
            Else
                errorControl.SetError(fndTankerNo, "")
            End If
            If clsCommon.myLen(txtSupplierCode.Value) <= 0 Then
                errorControl.SetError(txtSupplierCode, "Please select the Supplier.It is mandatory ")
                txtSupplierCode.Focus()
                Throw New Exception("Please select the Supplier.It is mandatory")
            Else
                errorControl.SetError(txtSupplierCode, "")
            End If
            If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
                errorControl.SetError(fndLocationBulk, "Please select the location.It is mandatory ")
                fndLocationBulk.Focus()
                Throw New Exception("Please select the location.It is mandatory")
            Else
                errorControl.SetError(fndLocationBulk, "")
            End If
            If clsCommon.myLen(fndVendorBulk.Value) <= 0 Then
                errorControl.SetError(fndVendorBulk, "Please select the vendor.It is mandatory ")
                fndVendorBulk.Focus()
                Throw New Exception("Please select the vendor.It is manadatory")
            Else
                errorControl.SetError(fndVendorBulk, "")
            End If
            If clsCommon.myLen(cmbStatus.SelectedValue) <= 0 Then
                errorControl.SetError(cmbStatus, "Please select the Status.It is mandatory ")
                cmbStatus.Focus()
                Throw New Exception("Please select the Status.It is manadatory")
            Else
                errorControl.SetError(cmbStatus, "")
            End If

            If clsCommon.CompairString(cmbStatus.SelectedValue, "D") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtContractorCode.Value) <= 0 Then
                    errorControl.SetError(txtContractorCode, "Please select the Contractor.It is mandatory ")
                    txtContractorCode.Focus()
                    Throw New Exception("Please select the Contractor.It is mandatory")
                Else
                    errorControl.SetError(txtContractorCode, "")
                End If
            End If

            If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                errorControl.SetError(txtMilktypeCode, "Please select the MilkType.It is mandatory ")
                txtMilktypeCode.Focus()
                Throw New Exception("Please select the MilkType.It is mandatory")
            Else
                errorControl.SetError(txtMilktypeCode, "")
            End If


            If clsCommon.myLen(txtSupplierChallanNo.Text) <= 0 Then
                errorControl.SetError(txtSupplierChallanNo, "Please enter the challan no or 'ND' as challan no if there is 'No Document' ")
                txtSupplierChallanNo.Focus()
                Throw New Exception("Please enter the challan no or 'ND' as challan no if there is 'No Document'")
            Else
                errorControl.SetError(txtSupplierChallanNo, "")
            End If

            If clsCommon.myLen(txtDispatchCentreCode.Text) <= 0 Then
                errorControl.SetError(txtDispatchCentreCode, "Please enter Dispatch Centre No ")
                txtDispatchCentreCode.Focus()
                Throw New Exception("Please enter Dispatch Centre No ")
            Else
                errorControl.SetError(txtDispatchCentreCode, "")
            End If


            If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colItemCode).Value) <= 0 Then
                Throw New Exception("Please Enter Item Code At Row No 1 in Item Grid")
            End If

            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code='" & gvItemBulk.Rows(0).Cells(colItemCode).Value & "'")) = 0 Then
                Throw New Exception("Please Enter Valid Item Code At Row No 1 in Item Grid")
            End If

            If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                If (Not clsCommon.CompairString(txtSupplierChallanNo.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtSupplierChallanNo.Text) > 0 Then
                    Throw New Exception("Please Enter Item Qty At Row No 1 in Item Grid")
                End If
            End If
            If IsNumeric(gvItemBulk.Rows(0).Cells(colQty).Value) = False Then
                If (Not clsCommon.CompairString(txtSupplierChallanNo.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtSupplierChallanNo.Text) > 0 Then
                    Throw New Exception(" Item Qty Must be a Number At Row No 1 in Item Grid")
                End If
            End If

            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                If (Not clsCommon.CompairString(txtSupplierChallanNo.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtSupplierChallanNo.Text) > 0 Then
                    Throw New Exception(" Item Qty Must be a Number and Not Zero or Negative At Row No 1 in Item Grid")
                End If
            End If

            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" Gate Entry Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" Gate Entry Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsIntimation()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                obj.Intimation_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.IntimationScreen, "", fndLocationBulk.Value)
                If clsCommon.myLen(obj.Intimation_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error in Gate Intimation  No genertion", Me.Text)
                    Exit Sub
                End If
            Else
                obj.Intimation_No = clsCommon.myCstr(fndGateEntryNO.Value)
            End If
            fndGateEntryNO.Value = obj.Intimation_No
            obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
            obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
            obj.Vendor_Code = clsCommon.myCstr(fndVendorBulk.Value)
            obj.Supplier_Code = clsCommon.myCstr(txtSupplierCode.Value)
            obj.Dispatch_Centre_Code = clsCommon.myCstr(txtDispatchCentreCode.Text)
            obj.MIKL_TYPE_CODE = clsCommon.myCstr(txtMilktypeCode.Value)
            obj.Challan_No = clsCommon.myCstr(txtSupplierChallanNo.Text)
            obj.Status = clsCommon.myCstr(cmbStatus.SelectedValue)
            obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy")
            obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
            obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
            obj.TotalQty_In_Kg = clsCommon.myCdbl(lblTotalQTy.Text)
            obj.CONTRACTOR_CODE = clsCommon.myCstr(txtContractorCode.Value)
            obj.Arr = New List(Of clsIntimationChamberDetails)
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                Dim objTr As New clsIntimationChamberDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            If Not isPost Then
                obj.isPosted = 0
            End If
            If clsIntimation.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                btnSave.Text = "Update"
                fndGateEntryNO.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                obj = Nothing
                Exit Sub
                LoadData(obj.Intimation_No, NavigatorType.Current)

            End If
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            fndGateEntryNO.MyReadOnly = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub postData()
        Try
            Dim strDocType As String = String.Empty
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsIntimation.postData(fndGateEntryNO.Value, strDocType, Me.Form_ID)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
            LoadData(fndGateEntryNO.Value, NavigatorType.Current)
            End If
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadGrid(ByVal strTankerCode As String)
        If clsCommon.myLen(strTankerCode) > 0 Then
            loadBlankGv()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select LINE_NO,CHAMBER_DESC,(select Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem' and Code='MilkSetting') as ItemCode from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" & fndTankerNo.Value & "'")
            If dt.Rows.Count > 0 Then
                Dim intLineNo As Integer = 0
                For Each dr As DataRow In dt.Rows
                    gvItemBulk.Rows.AddNew()
                    intLineNo += 1
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(clsCommon.myCstr(dr("ItemCode")), Nothing)
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(dr("ItemCode")) & "' and Default_UOM='1' "))
                    If clsCommon.myLen(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value) <= 0 Then
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                    End If
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("CHAMBER_DESC"))
                Next
            End If
        End If
    End Sub
    Sub UpdateTotal()
        Dim dblTotalQty As Double = 0
        For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
            dblTotalQty = dblTotalQty + clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colQty).Value)
        Next
        lblTotalQTy.Text = clsCommon.myFormat(dblTotalQty)
    End Sub
    Sub LoadData(ByVal strGateEntryNo As String, ByVal nav As NavigatorType)
        Dim obj As clsIntimation = Nothing
        obj = clsIntimation.getData(strGateEntryNo, "BulkProc", nav)
        If obj IsNot Nothing Then
            fndLocationBulk.Enabled = False
            fndTankerNo.Enabled = False
            fndGateEntryNO.Value = obj.Intimation_No
            dtpDateAndTimeBulk.Value = obj.Date_And_Time
            fndLocationBulk.Value = obj.location_Code
            fndVendorBulk.Value = obj.Vendor_Code        
            txtSupplierChallanNo.Text = obj.Challan_No
            dtpChallanDateBulk.Value = obj.Challan_Date
            txtSupplierCode.Value = obj.Supplier_Code
            txtMilktypeCode.Value = obj.MIKL_TYPE_CODE
            cmbStatus.SelectedValue = obj.Status
            txtDispatchCentreCode.Text = obj.Dispatch_Centre_Code
            lblTotalQTy.Text = obj.TotalQty_In_Kg
            fndTankerNo.Value = obj.Tanker_No
            txtContractorCode.Value = obj.CONTRACTOR_CODE
            lblSupplierName.Text = clsSupplierMaster.getSupplierName(txtSupplierCode.Value, Nothing)
            lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
            lblTankerNo.Text = clsContractTankerHead.getTankerNo(fndTankerNo.Value, Nothing)
            loadBlankGvItemBulk()
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objTr As clsIntimationChamberDetails In obj.Arr
                    gvItemBulk.Rows.AddNew()
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                Next
            End If
          
            If obj.isPosted = 1 Then
                lblPending.Status = ERPTransactionStatus.Approved
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
            Else
                lblPending.Status = ERPTransactionStatus.Pending
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
            End If
            btnSave.Text = "Update"
        Else
            reset()
        End If

        If obj.isPosted = 1 Then
            lblPending.Status = ERPTransactionStatus.Approved
            btnSave.Enabled = False
            btnDelete.Enabled = False
            btnPost.Enabled = False
        Else
            lblPending.Status = ERPTransactionStatus.Pending
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
        End If
        btnSave.Text = "Update"

        obj = Nothing
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub fndGateEntryNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNO._MYNavigator

        LoadData(fndGateEntryNO.Value, NavType)
    End Sub

    Private Sub fndGateEntryNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNO._MYValidating
        Dim strDocType As String = String.Empty

        Dim whrcls As String = String.Empty
        If (Not clsMccMaster.isCurrentUserHO()) AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " Location_Code in (" & objCommonVar.strCurrUserLocations & ") and doc_type='" & strDocType & "'"
        End If
        fndGateEntryNO.Value = clsIntimation.getFinder(whrcls, fndGateEntryNO.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
            LoadData(fndGateEntryNO.Value, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub grpBulkProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub gvItemBulk_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvItemBulk.KeyDown

    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItemBulk.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItemBulk.Columns.Count - 1 Step ii + 1
                        gvItemBulk.Columns(ii).IsVisible = False
                        gvItemBulk.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItemBulk.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            RadPageViewPage1.Text = "Gate" & Environment.NewLine & "Entry"
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItemBulk.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItemBulk.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItemBulk.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub


    Private Sub txtChallanNoBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSupplierChallanNo.Validating
        If clsCommon.myLen(txtSupplierChallanNo.Text) <= 0 Then
            txtSupplierChallanNo.Text = "ND"
        End If
    End Sub

   

    Private Sub SplitContainer4_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer4.Panel1.Paint

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        Dim whr As String = ""
        fndTankerNo.Value = clsContractTankerHead.getFinder("", fndTankerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndTankerNo.Value) > 0 Then
            lblTankerNo.Text = clsContractTankerHead.getTankerNo(fndTankerNo.Value, Nothing)
            LoadGrid(fndTankerNo.Value)
        End If
    End Sub

    Private Sub dtpDateAndTimeBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDateAndTimeBulk.Validating
        Try
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                'dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" Gate Entry Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" Gate Entry Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        End Try
    End Sub

    Private Sub txtSupplierCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSupplierCode._MYValidating
        If clsCommon.myLen(fndVendorBulk.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
            fndVendorBulk.Focus()
            Exit Sub
        End If
        Dim whr As String = " Vendor_Code='" & fndVendorBulk.Value & "'"
        txtSupplierCode.Value = clsSupplierMaster.getFinder(whr, txtSupplierCode.Value, isButtonClicked)
        If clsCommon.myLen(txtSupplierCode.Value) > 0 Then
            lblSupplierName.Text = clsSupplierMaster.getSupplierName(txtSupplierCode.Value, Nothing)
        End If
    End Sub

    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
        If clsCommon.myLen(txtMilktypeCode.Value) > 0 Then
            lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
        End If
    End Sub

    Private Sub txtContractorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtContractorCode._MYValidating
        Dim whr As String = ""
        txtContractorCode.Value = clsDivertedContractor.getFinder(whr, txtContractorCode.Value, isButtonClicked)
    End Sub
End Class
