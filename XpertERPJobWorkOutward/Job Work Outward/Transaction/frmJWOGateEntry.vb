Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Public Class frmJWOGateEntry
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colQCStatus As String = "colQCStatus"
    Public Const colSlNo As String = "SLNO"
    Public Const colSealNo As String = "SealNo"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colHSNCode As String = "HsnCode"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public strDocCode As String = ""
    'Dim trans As SqlTransaction = Nothing
    Public isCellValueChangedOpen = False
    Public strLoggedInTo As String = String.Empty ' It Will Store Either MCC or Plant as Login Location
    Public strLoginMccOrPlantCode As String = String.Empty 'It Will store the Location Code of Currently Logged In Mcc or Pant
    Public strLoginMccOrPlantDesc As String = String.Empty 'It Will store the Location Desc of Currently Logged In Mcc or Pant
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public obj As clsMilkGateEntry_JOW = Nothing
    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTankerReceipt.ToggleStateChanged
        If rbtnTankerReceipt.IsChecked Then
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            txtChallanNoBulk.Text = "ND"
            fndVendorBulk.Enabled = True
            lblVendorBulk.Text = "Vendor"
            dtpChallanDateBulk.Enabled = True
            txtTankerNoBulk.Visible = True
        ElseIf rbtnSKU.IsChecked Then
            lblVendorBulk.Text = "Vendor"
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            txtChallanNoBulk.Text = ""
            fndVendorBulk.Enabled = False
            dtpChallanDateBulk.Enabled = True
            fndLocationBulk.Enabled = False
            txtTankerNoBulk.Visible = True
            txtTankerNoBulk.ReadOnly = False
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
        End If
        If rbtnTankerReceipt.IsChecked OrElse rbtnSKU.IsChecked Then
            reset()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.Close()
        'Me.Dispose()
        'GC.Collect()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub fndLocationBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocationBulk._MYValidating
        Dim strLocations = String.Empty
        strLocations = " isnull(GIT_Type ,'') in ('N','') and ISNULL(Location_Type ,'')<>'Virtual' and Is_Jobwork =0"
        fndLocationBulk.Value = clsLocation.getFinder(strLocations, fndLocationBulk.Value, isButtonClicked)
        fndVendorBulk.Value = Nothing
        lblVendorNameBulk.Text = Nothing
        Dim count As Integer = clsDBFuncationality.getSingleValue("select coaLESCE(is_jobwork,'0') from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationBulk.Value & "'")
        If count <> 0 Then
            clsCommon.MyMessageBoxShow("Selected Location is Job-Work Location, Gate Entry not allowed for this. Please create SRN directly")
            reset()

        End If

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
        fndVendorBulk.Value = clsVendorMaster.getFinder(" Vendor_Code in (select Jobwork_Vendor  from TSPL_Location_MASTER  where Is_Jobwork =1 and isnull(Is_Sub_Location,'')='Y' )", fndVendorBulk.Value, isButtonClicked)
        lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)

        TxtJobworlLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Location_Code from TSPL_Location_MASTER  where ISNULL (Jobwork_Vendor,'') ='" & clsCommon.myCstr(fndVendorBulk.Value) & "'"))
        lblJobworkLocation.Text = clsLocation.GetName(TxtJobworlLocation.Value, Nothing)
    End Sub


    Private Sub txtTankerNoBulk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTankerNoBulk.KeyPress
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete OrElse Asc(e.KeyChar) = 22 OrElse Asc(e.KeyChar) = 3 Then
        Else
            e.Handled = True
        End If
    End Sub
    Sub reset()
        fndLocationBulk.Enabled = True
        fndVendorBulk.Enabled = True
        fndTankerNo.Visible = False
        txtTankerNoBulk.Text = ""
        fndGateEntryNO.Value = ""
        btnReverse.Visible = False
        fndGateEntryNO.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
        fndChallanNoMcc.Visible = False
        txtChallanNoBulk.Text = ""
        txtChallanNoBulk.ReadOnly = False
        dtpChallanDateBulk.Value = dt
        fndLocationBulk.Value = ""
        lblLocationDecBulk.Text = ""
        fndVendorBulk.Value = ""
        lblVendorNameBulk.Text = ""
        txtTankerNoBulk.Text = ""
        TxtJobworlLocation.Value = ""
        lblJobworkLocation.Text = ""
        loadBlankGv()
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnPost.Enabled = False
        btnDelete.Enabled = False
        dtpDateAndTimeBulk.Value = dt
        dtpDateAndTimeBulk.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        UpdateCurrentLoginStatus()
        fndChallanNoMcc.Enabled = False
        UcAttachment1.BlankAllControls()
        txtTransferNo.arrValueMember = Nothing
        'chkBulkMilkProc.IsChecked = True
        If rbtnTankerReceipt.IsChecked Then
            dtpChallanDateBulk.ReadOnly = False
            txtTankerNoBulk.ReadOnly = False
            fndTankerNo.Visible = False
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        Else
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            'loadBlankGridManualSeal()
            'loadBlankGridPaperSeal()
            txtTankerNoBulk.ReadOnly = False
            fndTankerNo.Visible = False
            txtTankerNoBulk.Visible = True
            dtpChallanDateBulk.ReadOnly = False
            'txtTankerNoBulk.ReadOnly = True
        End If
        dt = Nothing
        fndLocationBulk.Value = ""
        lblLocationDecBulk.Text = ""
        'fndLocationBulk.Value = clsMilkGateEntry_JOW.getUsersDefaultLocation()
        'lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        RadPageView1.SelectedPage = RadPageViewPage1
        FindAndRestoreGridLayout(Me)
        'FindAndSetTabStopFalse(Me)
    End Sub
    Sub loadBlankGv()
        If rbtnTankerReceipt.IsChecked Then
            loadBlankGvItemBulk()
        Else
            loadBlankGvItemMCC()
        End If
        'ReStoreGridLayout()
    End Sub
    Sub loadBlankGvItemBulk()
        gvItemBulk.Tag = "Bulk"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing


        Dim repoQCStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoQCStatus.HeaderText = "QC Status"
        repoQCStatus.Name = colQCStatus
        repoQCStatus.ReadOnly = True
        repoQCStatus.IsVisible = False
        repoQCStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemBulk.MasterTemplate.Columns.Add(repoQCStatus)


        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        ' gvItemBulk.Columns(colItemCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gvItemBulk.Columns(colItemCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colItemCode).ReadOnly = False

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colHSNCode, "HSN Code")
        gvItemBulk.Columns(colHSNCode).Width = 100
        gvItemBulk.Columns(colHSNCode).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = False

        gvItemBulk.Columns.Add(colQty, "Challan Qty")
        gvItemBulk.Columns(colQty).Width = 150
        gvItemBulk.Columns(colQty).ReadOnly = False

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 100
        gvItemBulk.Columns(colFat).ReadOnly = False
        gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 100
        gvItemBulk.Columns(colSNF).ReadOnly = False
        gvItemBulk.Columns(colSNF).IsVisible = False

        gvItemBulk.Columns.Add(colFatKG, "FAT (KG)")
        gvItemBulk.Columns(colFatKG).Width = 100
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF (KG)")
        gvItemBulk.Columns(colSNFKG).Width = 100
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False

        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            For ii As Integer = 0 To gvItemBulk.Columns.Count - 1
                If clsCommon.CompairString(gvItemBulk.Columns(ii).Name, colQCStatus) = CompairStringResult.Equal Then
                    gvItemBulk.Columns(ii).ReadOnly = False
                    gvItemBulk.Columns(ii).IsVisible = True
                Else
                    gvItemBulk.Columns(ii).ReadOnly = True
                End If
            Next
        End If
        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
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
    Sub loadBlankGvItemMCC()
        gvItemBulk.Tag = "MCC"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing


        Dim repoQCStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoQCStatus.HeaderText = "QC Status"
        repoQCStatus.Name = colQCStatus
        repoQCStatus.ReadOnly = True
        repoQCStatus.IsVisible = False
        repoQCStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemBulk.MasterTemplate.Columns.Add(repoQCStatus)

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        '  gvItemBulk.Columns(colItemCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gvItemBulk.Columns(colItemCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colItemCode).ReadOnly = False

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colHSNCode, "HSN Code")
        gvItemBulk.Columns(colHSNCode).Width = 100
        gvItemBulk.Columns(colHSNCode).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = False

        gvItemBulk.Columns.Add(colQty, "Challan Qty")
        gvItemBulk.Columns(colQty).Width = 150
        gvItemBulk.Columns(colQty).ReadOnly = False

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 100
        gvItemBulk.Columns(colFat).ReadOnly = False
        gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 100
        gvItemBulk.Columns(colSNF).ReadOnly = False
        gvItemBulk.Columns(colSNF).IsVisible = False

        gvItemBulk.Columns.Add(colFatKG, "FAT (KG)")
        gvItemBulk.Columns(colFatKG).Width = 100
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF (KG)")
        gvItemBulk.Columns(colSNFKG).Width = 100
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False

        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            For ii As Integer = 0 To gvItemBulk.Columns.Count - 1
                If clsCommon.CompairString(gvItemBulk.Columns(ii).Name, colQCStatus) = CompairStringResult.Equal Then
                    gvItemBulk.Columns(ii).ReadOnly = False
                    gvItemBulk.Columns(ii).IsVisible = True
                Else
                    gvItemBulk.Columns(ii).ReadOnly = True
                End If
            Next
        End If
        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        gvItemBulk.AllowAddNewRow = False
        gvItemBulk.AllowDeleteRow = True
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
        gvItemBulk.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom

    End Sub



    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub FrmMilkGateEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gvItemBulk.CurrentCell IsNot Nothing Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If gvItemBulk.CurrentColumn Is gvItemBulk.Columns(colItemCode) AndAlso rbtnTankerReceipt.IsChecked Then
                    gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi'  or Product_Type ='MP'", gvItemBulk.CurrentRow.Cells(colItemCode).Value, True)
                    gvItemBulk.CurrentRow.Cells(colItemCode).EndEdit()
                    If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
                        gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
                        gvItemBulk.CurrentRow.Cells(colHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where item_code='" + clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value) + "'"))
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    Else
                        gvItemBulk.CurrentRow.Cells(colItemDesc).Value = ""
                        gvItemBulk.CurrentRow.Cells(colHSNCode).Value = ""
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = ""
                    End If
                End If
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
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub FrmMilkGateEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S to Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C to Close The Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P To Post Transaction")
        UcAttachment1.Form_ID = MyBase.Form_ID
        If clsCommon.myLen(strDocCode) > 0 Then
            Dim DocType As String = clsDBFuncationality.getSingleValue("select doc_type from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no='" & strDocCode & "'")
            If clsCommon.CompairString(DocType, "Tanker") = CompairStringResult.Equal Then
                rbtnTankerReceipt.IsChecked = True
            Else
                rbtnSKU.IsChecked = True
            End If
            LoadData(strDocCode, DocType, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            Dim DocType As String = clsDBFuncationality.getSingleValue("select doc_type from TSPL_MILK_GATE_ENTRY_DETAILS where gate_entry_no='" & clsCommon.myCstr(Me.Tag) & "'")
            If clsCommon.CompairString(DocType, "Tanker") = CompairStringResult.Equal Then
                rbtnTankerReceipt.IsChecked = True
            Else
                rbtnSKU.IsChecked = True
            End If
            LoadData(clsCommon.myCstr(Me.Tag), DocType, NavigatorType.Current)
        End If
        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            btnReset.Visible = False
            btnDelete.Visible = False
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        End If
    End Sub
    Public Sub SetUserMgmtNew()
        Try
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")

            End If
            btnSave.Visible = MyBase.isModifyFlag
            btnDelete.Visible = MyBase.isDeleteFlag
            If MyBase.isReverse Then
                btnReverse.Enabled = True
            Else
                btnReverse.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkGateEntry_JOW.ReverseAndUnpost(fndGateEntryNO.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndGateEntryNO.Value, IIf(rbtnSKU.IsChecked, "Sku_Receipt", "Tanker"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            'If e.Column Is gvItemBulk.Columns(colItemCode) AndAlso chkBulkMilkProc.IsChecked Then
            Dim intCurrRow As Integer = gvItemBulk.CurrentRow.Index
            gvItemBulk.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvItemBulk.Rows.Count - 1 Then
                'gvItemBulk.Rows.AddNew()
                gvItemBulk.CurrentRow = gvItemBulk.Rows(intCurrRow)
            End If

            If e.Column Is gvItemBulk.Columns(colItemCode) Then
                gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi' or Product_Type ='MP'", gvItemBulk.CurrentRow.Cells(colItemCode).Value, False)
                If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItemBulk.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
                    gvItemBulk.CurrentRow.Cells(colHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where item_code='" + clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value) + "'"))
                    If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colUOM).Value) <= 0 Then

                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    End If
                Else
                    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = ""
                    gvItemBulk.CurrentRow.Cells(colHSNCode).Value = ""
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = ""
                    gvItemBulk.CurrentRow.Cells(colQty).Value = ""
                    gvItemBulk.CurrentRow.Cells(colFat).Value = ""
                    gvItemBulk.CurrentRow.Cells(colFatKG).Value = ""
                    gvItemBulk.CurrentRow.Cells(colSNF).Value = ""
                    gvItemBulk.CurrentRow.Cells(colSNFKG).Value = ""
                End If
            ElseIf e.Column Is gvItemBulk.Columns(colUOM) Then
                OpenUOMList(False)
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
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)

        End If
    End Sub
    Private Sub fndChallanNoMcc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndChallanNoMcc._MYValidating
        'If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Location First.")
        '    fndLocationBulk.Focus()
        '    Exit Sub
        'End If
        'Dim whr As String = ""
        'If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
        '    whr = " and TSPL_MCC_Dispatch_Challan.mcc_code='" & fndVendorBulk.Value & "'"
        'End If
        'fndChallanNoMcc.Value = clsMccDispatch.getFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from TSPL_MILK_GATE_ENTRY_DETAILS where TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_No<>'" & fndGateEntryNO.Value & "') ", fndChallanNoMcc.Value, isButtonClicked)
        'If clsCommon.myLen(fndChallanNoMcc.Value) > 0 Then
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_Dispatch_Challan.mcc_code,mcc_name,TSPL_MCC_Dispatch_Challan.dispatch_date,TSPL_MCC_Dispatch_Challan.tanker_no,TSPL_MCC_Dispatch_Challan.item_code,TSPL_MCC_Dispatch_Challan.item_desc,TSPL_MCC_Dispatch_Challan.net_qty from TSPL_MCC_Dispatch_Challan   where TSPL_MCC_Dispatch_Challan.chalan_no='" & fndChallanNoMcc.Value & "'")
        '    fndVendorBulk.Value = dt.Rows(0)("mcc_code")
        '    fndVendorBulk.Enabled = False
        '    lblVendorNameBulk.Text = dt.Rows(0)("mcc_name")
        '    dtpChallanDateBulk.Value = dt.Rows(0)("dispatch_date")
        '    txtTankerNoBulk.Text = dt.Rows(0)("tanker_no")
        '    gvItemBulk.Rows(0).Cells(colItemCode).Value = dt.Rows(0)("item_code")
        '    gvItemBulk.Rows(0).Cells(colItemDesc).Value = dt.Rows(0)("item_desc")
        '    gvItemBulk.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(dt.Rows(0)("item_code"), Nothing)
        '    gvItemBulk.Rows(0).Cells(colQty).Value = dt.Rows(0)("net_qty")
        '    gvItemBulk.Rows(0).Cells(colFat).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='FAT'")
        '    gvItemBulk.Rows(0).Cells(colSNF).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='SNF'")
        '    gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
        '    gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
        '    Dim objDis As clsMccDispatch = clsMccDispatch.getData(fndChallanNoMcc.Value, NavigatorType.Current)
        '    gvManualSeal.Rows(0).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No1)
        '    gvManualSeal.Rows(1).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No2)
        '    gvManualSeal.Rows(2).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No3)
        '    gvManualSeal.Rows(3).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No4)
        '    gvManualSeal.Rows(4).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No5)
        '    gvManualSeal.Rows(5).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No6)
        '    gvManualSeal.Rows(6).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No7)
        '    gvManualSeal.Rows(7).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No8)
        '    gvManualSeal.Rows(8).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No9)
        '    gvManualSeal.Rows(9).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No10)
        '    If objDis.arrPaperSeal IsNot Nothing AndAlso objDis.arrPaperSeal.Count > 0 Then
        '        For i As Integer = 0 To objDis.arrPaperSeal.Count - 1
        '            gvPaperSeal.Rows(i).Cells(colSealNo).Value = objDis.arrPaperSeal(i).Seal_No
        '        Next
        '    End If
        '    dt = Nothing
        'Else
        '    fndVendorBulk.Value = ""
        '    lblVendorNameBulk.Text = ""
        '    'dtpChallanDateBulk.Value = ""
        '    txtTankerNoBulk.Text = ""
        '    gvItemBulk.Rows(0).Cells(colItemCode).Value = ""
        '    gvItemBulk.Rows(0).Cells(colItemDesc).Value = ""
        '    gvItemBulk.Rows(0).Cells(colUOM).Value = ""
        '    gvItemBulk.Rows(0).Cells(colQty).Value = ""
        '    gvItemBulk.Rows(0).Cells(colFat).Value = ""
        '    gvItemBulk.Rows(0).Cells(colSNF).Value = ""
        '    gvItemBulk.Rows(0).Cells(colFatKG).Value = ""
        '    gvItemBulk.Rows(0).Cells(colSNFKG).Value = ""
        'End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim arr As List(Of String) = New List(Of String)
        If clsCommon.myLen(fndGateEntryNO.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter  Gate Entry No To delete ")
        Else
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_JWO_Weighment where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from TSPL_JWO_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
            If isUsed > 0 Then
                clsCommon.MyMessageBoxShow("Gate Entry No is in use")
                Exit Sub
            End If
            If myMessages.deleteConfirm() Then
                arr.Add(fndGateEntryNO.Value)
                If clsMilkGateEntry_JOW.deleteData(fndGateEntryNO.Value, Nothing) Then
                    UcAttachment1.funDelete(fndGateEntryNO.Value)
                    reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub
    Function allowToSave() As Boolean
        Try
            If rbtnTankerReceipt.IsChecked = False AndAlso rbtnSKU.IsChecked = False Then
                errorControl.SetError(rbtnTankerReceipt, "Please Select Gate Entry Type Either Tanker Receipt or Sku Receipt ")
                Throw New Exception("Please Select Gate Entry Type Either Tanker Receipt or Sku Receipt ")
            Else
                errorControl.SetError(rbtnTankerReceipt, "")
            End If

            If rbtnTankerReceipt.IsChecked Then
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

                If clsCommon.myLen(txtTankerNoBulk.Text) <= 0 And Not rbtnSKU.IsChecked Then
                    errorControl.SetError(txtTankerNoBulk, "Please enter the tanker no. It is mandatory ")
                    txtTankerNoBulk.Focus()
                    Throw New Exception("Please enter the tanker no. It is mandatory ")
                Else
                    errorControl.SetError(txtTankerNoBulk, "")
                End If




                If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
                    errorControl.SetError(txtChallanNoBulk, "Please enter the challan no or 'ND' as challan no if there is 'No Document' ")
                    txtChallanNoBulk.Focus()
                    Throw New Exception("Please enter the challan no or 'ND' as challan no if there is 'No Document'")
                Else
                    errorControl.SetError(txtChallanNoBulk, "")
                End If



                If clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal Then

                Else
                    If dtpChallanDateBulk.Value > dtpDateAndTimeBulk.Value Then
                        Throw New Exception("Challan date can't be greater than gate entry date")
                    End If
                End If

                If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colItemCode).Value) <= 0 Then
                    Throw New Exception("Please Enter Item Code At Row No 1 in Item Grid")
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code='" & gvItemBulk.Rows(0).Cells(colItemCode).Value & "'")) = 0 Then
                    Throw New Exception("Please Enter Valid Item Code At Row No 1 in Item Grid")
                End If

                If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colHSNCode).Value) <= 0 Then
                    Throw New Exception("Please Set HSN Code For Row No 1 in Item Master")
                End If

                If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception("Please Enter Item Qty At Row No 1 in Item Grid")
                    End If
                End If
                If IsNumeric(gvItemBulk.Rows(0).Cells(colQty).Value) = False Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception(" Item Qty Must be a Number At Row No 1 in Item Grid")
                    End If
                End If

                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception(" Item Qty Must be a Number and Not Zero or Negative At Row No 1 in Item Grid")
                    End If
                End If




            ElseIf rbtnSKU.IsChecked Then

                If clsCommon.myLen(txtTankerNoBulk.Text) <= 0 Then
                    errorControl.SetError(txtTankerNoBulk, "Please enter the tanker no. It is mandatory ")
                    txtTankerNoBulk.Focus()
                    Throw New Exception("Please enter the tanker no. It is mandatory ")
                Else
                    errorControl.SetError(txtTankerNoBulk, "")
                End If

                If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
                    errorControl.SetError(txtChallanNoBulk, "Please enter the challan no. It is mandatory ")
                    txtChallanNoBulk.Focus()
                    Throw New Exception("Please enter the challan no. It is mandatory ")
                Else
                    errorControl.SetError(fndChallanNoMcc, "")
                End If
                If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
                    errorControl.SetError(fndLocationBulk, "Please select the location.It is mandatory ")
                    fndLocationBulk.Focus()
                    Throw New Exception("Please select the location.It is mandatory")
                Else
                    errorControl.SetError(fndLocationBulk, "")
                End If


                For i As Integer = 0 To gvItemBulk.Rows.Count - 1


                    Dim strICode As String = clsCommon.myCstr(gvItemBulk.Rows(i).Cells(colItemCode).Value)
                    Dim strUOM As String = clsCommon.myCstr(gvItemBulk.Rows(i).Cells(colUOM).Value)
                    For jj As Integer = 0 To gvItemBulk.Rows.Count - 1
                        If jj = i Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colItemCode).Value)
                        Dim strInnerUOM As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colUOM).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(i + 1) + " And " + clsCommon.myCstr(jj + 1)

                            Msg = Msg + Environment.NewLine + "Item: " + strICode + ""
                            Msg = Msg + Environment.NewLine + "UOM: " + strUOM

                            Throw New Exception(Msg)
                            Return False
                        End If


                    Next

                    If clsCommon.myLen(gvItemBulk.Rows(i).Cells(colItemCode).Value) > 0 OrElse clsCommon.myCdbl(gvItemBulk.Rows(i).Cells(colQty).Value) > 0 OrElse clsCommon.myLen(gvItemBulk.Rows(i).Cells(colUOM).Value) > 0 Then

                        If clsCommon.myLen(gvItemBulk.Rows(i).Cells(colItemCode).Value) <= 0 Then
                            Throw New Exception("Please Enter Item Code At Row No " & i + 1 & " in Item Grid")
                        End If

                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code='" & gvItemBulk.Rows(i).Cells(colItemCode).Value & "'")) = 0 Then
                            Throw New Exception("Please Enter Valid Item Code At Row No " & i + 1 & " in Item Grid")
                        End If

                        If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colHSNCode).Value) <= 0 Then
                            Throw New Exception("Please Set HSN Code For Row No " & i + 1 & " in Item Master")
                        End If

                        If clsCommon.myLen(gvItemBulk.Rows(i).Cells(colUOM).Value) <= 0 Then
                            Throw New Exception("Please Enter UOM At Row No " & i + 1 & " in Item Grid")
                        End If

                        If clsCommon.myLen(gvItemBulk.Rows(i).Cells(colQty).Value) <= 0 Then
                            If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                                Throw New Exception("Please Enter Item Qty At Row No " & i + 1 & " in Item Grid")
                            End If
                        End If
                        If IsNumeric(gvItemBulk.Rows(i).Cells(colQty).Value) = False Then
                            If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                                Throw New Exception(" Item Qty Must be a Number At Row No " & i + 1 & " in Item Grid")
                            End If
                        End If

                        If clsCommon.myCdbl(gvItemBulk.Rows(i).Cells(colQty).Value) <= 0 Then
                            If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                                Throw New Exception(" Please entry Qty At Row No " & i + 1 & " in Item Grid")
                            End If
                        End If
                    End If

                Next


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
            If clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal Then

            Else
                If dtpChallanDateBulk.Value > dtpDateAndTimeBulk.Value Then
                    Throw New Exception("Challan date can't be greater than gate entry date")
                End If
            End If

            If btnSave.Enabled AndAlso clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                Dim isTankerGateOut As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(xxx.tanker_No) from (select Tanker_No  from TSPL_MILK_GATE_ENTRY_DETAILS where Gate_Entry_No not in (select Gate_Entry_No from TSPL_Gate_Out) and Doc_Type='Tanker')xxx where Tanker_No='" & txtTankerNoBulk.Text & "'"))
                If isTankerGateOut >= 1 Then
                    txtTankerNoBulk.Text = ""
                    Throw New Exception("Please enter another Tanker No.It is in use at some other plant ")
                End If
            End If
            UcAttachment1.AllowToSave()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Sub SaveDataQC()
        Try

            obj = New clsMilkGateEntry_JOW()
            Dim objTr As New clsMilkGateEntryDetail_JOW()
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNO.Value)
            obj.arrJOWGateEntryDetail = New List(Of clsMilkGateEntryDetail_JOW)
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                    objTr = New clsMilkGateEntryDetail_JOW()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.QC_Status = clsCommon.myCBool(grow.Cells(colQCStatus).Value)
                    obj.arrJOWGateEntryDetail.Add(objTr)
                End If
            Next
            If clsMilkGateEntry_JOW.SaveDataQC(obj) Then
                UcAttachment1.SaveData(obj.Gate_Entry_No)
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    myMessages.insert()
                Else
                    myMessages.update()
                End If
                btnSave.Text = "Update"
                fndGateEntryNO.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                LoadData(obj.Gate_Entry_No, obj.Doc_Type, NavigatorType.Current)
                obj = Nothing
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            fndGateEntryNO.MyReadOnly = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SaveData(ByVal isPost As Boolean)
        Try

            obj = New clsMilkGateEntry_JOW()
            Dim objTr As New clsMilkGateEntryDetail_JOW()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNO.Value)
            If rbtnTankerReceipt.IsChecked Then
                obj.Doc_Type = "Tanker"
                obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDecBulk.Text)
                obj.JobWorkLocation = clsCommon.myCstr(TxtJobworlLocation.Value)
                obj.Vendor_Code = clsCommon.myCstr(fndVendorBulk.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)

                obj.Tanker_No = clsCommon.myCstr(txtTankerNoBulk.Text)
                obj.Challan_No = clsCommon.myCstr(txtChallanNoBulk.Text)
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy")
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
            ElseIf rbtnSKU.IsChecked Then
                obj.Doc_Type = "Sku_Receipt"
                obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.Tanker_No = clsCommon.myCstr(txtTankerNoBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(fndVendorBulk.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
                obj.Challan_No = clsCommon.myCstr(txtChallanNoBulk.Text)
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy")
                'obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                'obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                'obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                'obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                'obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                'obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDecBulk.Text)
                obj.JobWorkLocation = clsCommon.myCstr(TxtJobworlLocation.Value)

                obj.arrJOWGateEntryDetail = New List(Of clsMilkGateEntryDetail_JOW)
                For Each grow As GridViewRowInfo In gvItemBulk.Rows
                    objTr = New clsMilkGateEntryDetail_JOW()
                    objTr.Gate_Entry_No = clsCommon.myCstr(obj.Gate_Entry_No)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objTr.Qty_In_Kg = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                    objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        obj.arrJOWGateEntryDetail.Add(objTr)
                    End If

                Next

            End If

            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If


            obj.ArrTransferIssueNo = txtTransferNo.arrValueMember


            If clsMilkGateEntry_JOW.SaveData(obj, obj.isNewEntry) Then
                UcAttachment1.SaveData(obj.Gate_Entry_No)
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
                LoadData(obj.Gate_Entry_No, obj.Doc_Type, NavigatorType.Current)
                obj = Nothing
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            fndGateEntryNO.MyReadOnly = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub postDataQC()
        Try
            Dim strDocType As String = String.Empty
            If rbtnTankerReceipt.IsChecked Then
                strDocType = "Tanker"
            ElseIf rbtnSKU.IsChecked Then
                strDocType = "Sku_Receipt"
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If (clsMilkGateEntry_JOW.PostDataQC(fndGateEntryNO.Value, strDocType, Me.Form_ID)) Then
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
                LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
            End If
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub postData()
        Try
            Dim strDocType As String = String.Empty
            If rbtnTankerReceipt.IsChecked Then
                strDocType = "Tanker"
            ElseIf rbtnSKU.IsChecked Then
                strDocType = "Sku_Receipt"
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsMilkGateEntry_JOW.postData(fndGateEntryNO.Value, strDocType, Me.Form_ID)) Then
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
                LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
            End If
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal nav As NavigatorType)
        Dim obj As clsMilkGateEntry_JOW = Nothing
        If rbtnTankerReceipt.IsChecked Then
            obj = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Tanker", nav)
            If obj IsNot Nothing Then
                txtTransferNo.arrValueMember = obj.ArrTransferIssueNo
                fndLocationBulk.Enabled = False
                fndVendorBulk.Enabled = False
                fndGateEntryNO.Value = obj.Gate_Entry_No
                dtpDateAndTimeBulk.Value = obj.Date_And_Time
                fndLocationBulk.Value = obj.location_Code
                lblLocationDecBulk.Text = obj.Location_Desc
                fndVendorBulk.Value = obj.Vendor_Code
                lblVendorNameBulk.Text = obj.Vendor_Desc
                txtTankerNoBulk.Text = obj.Tanker_No
                txtChallanNoBulk.Text = obj.Challan_No
                dtpChallanDateBulk.Value = obj.Challan_Date
                TxtJobworlLocation.Value = obj.JobWorkLocation
                lblJobworkLocation.Text = clsLocation.GetName(TxtJobworlLocation.Value, Nothing)
                loadBlankGvItemBulk()
                gvItemBulk.Rows(0).Cells(colQCStatus).Value = obj.QC_Status
                gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gvItemBulk.Rows(0).Cells(colHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where item_code='" + obj.Item_Code + "'"))
                gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.fat_per * obj.Qty_In_Kg / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.snf_Per * obj.Qty_In_Kg / 100
                If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
                    If obj.QC_Status = 1 Then
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
                Else
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
                End If
                UcAttachment1.LoadData(obj.Gate_Entry_No)
                btnSave.Text = "Update"
            Else
                reset()
            End If
        ElseIf rbtnSKU.IsChecked Then
            obj = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Sku_Receipt", nav)
            If obj IsNot Nothing Then
                txtTransferNo.arrValueMember = obj.ArrTransferIssueNo
                fndGateEntryNO.Value = obj.Gate_Entry_No
                dtpDateAndTimeBulk.Value = obj.Date_And_Time
                fndLocationBulk.Value = obj.location_Code
                lblLocationDecBulk.Text = obj.Location_Desc
                fndVendorBulk.Value = obj.Vendor_Code
                lblVendorNameBulk.Text = obj.Vendor_Desc
                TxtJobworlLocation.Value = obj.JobWorkLocation
                lblJobworkLocation.Text = clsLocation.GetName(TxtJobworlLocation.Value, Nothing)
                fndLocationBulk.Enabled = False
                fndVendorBulk.Enabled = False
                lblVendorNameBulk.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVendorBulk.Value & "'"))
                txtTankerNoBulk.Text = obj.Tanker_No
                fndTankerNo.Value = obj.Tanker_No
                fndChallanNoMcc.Value = obj.Challan_No
                txtChallanNoBulk.Text = obj.Challan_No
                dtpChallanDateBulk.Value = obj.Challan_Date
                loadBlankGvItemMCC()
                If obj.arrJOWGateEntryDetail IsNot Nothing AndAlso obj.arrJOWGateEntryDetail.Count > 0 Then
                    For Each objTr As clsMilkGateEntryDetail_JOW In obj.arrJOWGateEntryDetail
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQCStatus).Value = objTr.QC_Status
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = gvItemBulk.Rows.Count
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"))
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Qty_In_Kg
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = obj.fat_per * obj.Qty_In_Kg / 100
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = obj.snf_Per * obj.Qty_In_Kg / 100
                        gvItemBulk.Rows.AddNew()
                    Next
                    gvItemBulk.Rows.RemoveAt(gvItemBulk.Rows.Count - 1)
                Else
                    gvItemBulk.DataSource = Nothing
                End If


                ''------

                If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
                    If obj.QC_Status = 1 Then
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
                Else
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
                End If


                UcAttachment1.LoadData(obj.Gate_Entry_No)
                btnSave.Text = "Update"
            Else
                reset()
            End If
        Else
            clsCommon.MyMessageBoxShow("Please Select Gate Entry Type Either Tanker Receipt or Sku Receipt ")
            Exit Sub
        End If

        obj = Nothing
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            SaveDataQC()
        Else
            If allowToSave() Then
                SaveData(False)
            End If
        End If

    End Sub

    Private Sub fndGateEntryNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNO._MYNavigator
        Dim strDocType As String = String.Empty
        If rbtnTankerReceipt.IsChecked Then
            strDocType = "Tanker"
        ElseIf rbtnSKU.IsChecked Then
            strDocType = "Sku_Receipt"
        Else
            clsCommon.MyMessageBoxShow("Please select Gate Entry Type as Tanker Receipt or Sku Receipt")
            Exit Sub
        End If
        LoadData(fndGateEntryNO.Value, strDocType, NavType)
    End Sub

    Private Sub fndGateEntryNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNO._MYValidating
        Dim strDocType As String = String.Empty
        If rbtnTankerReceipt.IsChecked Then
            strDocType = "Tanker"
        ElseIf rbtnSKU.IsChecked Then
            strDocType = "Sku_Receipt"
        Else
            clsCommon.MyMessageBoxShow("Please select Gate Entry Type as Tanker Receipt or Sku Receipt")
            Exit Sub
        End If
        Dim whrcls As String = String.Empty
        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            whrcls = "  TSPL_JWO_GATE_ENTRY.isPosted=1 "
        Else
            whrcls = "  2=2 "
        End If
        If (Not clsMccMaster.isCurrentUserHO()) AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in (" & objCommonVar.strCurrUserLocations & ") and doc_type='" & strDocType & "'"
        Else
            whrcls += " and doc_type='" & strDocType & "'"
        End If

        fndGateEntryNO.Value = clsMilkGateEntry_JOW.getFinder(whrcls, fndGateEntryNO.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
            LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click

        If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.JWO_Item_Acceptance) = CompairStringResult.Equal Then
            postDataQC()
        Else
            postData()
        End If

    End Sub

    Private Sub grpBulkProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSKU.ToggleStateChanged
        If rbtnTankerReceipt.IsChecked Then
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            fndVendorBulk.Enabled = True
            lblVendorBulk.Text = "Vendor"
            dtpChallanDateBulk.Enabled = True
        ElseIf rbtnSKU.IsChecked Then
            lblVendorBulk.Text = "Vendor"
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            fndVendorBulk.Enabled = False
            fndLocationBulk.Enabled = False
            dtpChallanDateBulk.Enabled = True
            txtTankerNoBulk.Visible = True
            txtTankerNoBulk.ReadOnly = False
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
        End If
        If rbtnTankerReceipt.IsChecked OrElse rbtnSKU.IsChecked Then
            reset()
        End If
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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

    Sub loadBlankGridManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvManualSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = True
        gvManualSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvManualSeal.Rows.AddNew()
            gvManualSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvManualSeal.Rows(i - 1).Cells(colSealNo).Value = ""
        Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.AllowColumnReorder = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvManualSeal.MasterTemplate.ShowColumnHeaders = True
        gvManualSeal.EnableAlternatingRowColor = True
        gvManualSeal.EnableFiltering = False
        gvManualSeal.ShowFilteringRow = False
        gvManualSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Sub loadBlankGridPaperSeal()
        gvPaperSeal.Rows.Clear()
        gvPaperSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvPaperSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        'repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = True
        gvPaperSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvPaperSeal.Rows.AddNew()
            gvPaperSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvPaperSeal.Rows(i - 1).Cells(colSealNo).Value = ""
        Next
        gvPaperSeal.AllowAddNewRow = False
        gvPaperSeal.AllowDeleteRow = False
        gvPaperSeal.ShowGroupPanel = False
        gvPaperSeal.AllowColumnReorder = False
        gvPaperSeal.AllowRowReorder = False
        gvPaperSeal.EnableSorting = False
        gvPaperSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvPaperSeal.MasterTemplate.ShowColumnHeaders = True
        gvPaperSeal.EnableAlternatingRowColor = True
        gvPaperSeal.EnableFiltering = False
        gvPaperSeal.ShowFilteringRow = False
        gvPaperSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub txtChallanNoBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChallanNoBulk.Validating
        If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
            txtChallanNoBulk.Text = "ND"
        End If
    End Sub

    Private Sub txtTankerNoBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTankerNoBulk.Validating
        'Try
        '    If btnSave.Enabled Then
        '        Dim isTankerGateOut As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(xxx.tanker_No) from (select Tanker_No  from TSPL_MILK_GATE_ENTRY_DETAILS where Gate_Entry_No not in (select Gate_Entry_No from TSPL_Gate_Out) and Doc_Type='Tanker')xxx where Tanker_No='" & txtTankerNoBulk.Text & "'"))
        '        If isTankerGateOut >= 1 Then
        '            'errorControl.SetError(txtTankerNoBulk, "Please enter another Tanker No.It is in use at some other plant ")
        '            'txtTankerNoBulk.Focus()
        '            txtTankerNoBulk.Text = ""
        '            Throw New Exception("Please enter another RGP No.It is in use at some other plant ")
        '        End If
        '        If clsCommon.myLen(txtTankerNoBulk.Text) > 0 Then
        '            For i As Integer = 1 To txtTankerNoBulk.Text.Length
        '                If (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 48 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 57) OrElse (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 65 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 90) OrElse (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 97 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 122) Then
        '                Else
        '                    Throw New Exception("Tanker no must only contain Alphabates and numbers, Not any blank space and symbol ")

        '                End If
        '            Next
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        '    txtTankerNoBulk.Text = ""
        '    txtTankerNoBulk.Focus()
        'End Try

    End Sub

    Private Sub SplitContainer4_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer4.Panel1.Paint

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating

        'If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Location First.")
        '    fndLocationBulk.Focus()
        '    Exit Sub
        'End If
        ''Dim whr As String = ""

        ''If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
        ''    whr = " and TSPL_MCC_Dispatch_Challan.mcc_code='" & fndVendorBulk.Value & "'"
        ''End If
        ' ''fndChallanNoMcc.Value = clsMccDispatch.getTankerFinder("", fndTankerNo.Value)
        ''fndChallanNoMcc.Value = clsMccDispatch.getTankerFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from TSPL_MILK_GATE_ENTRY_DETAILS where TSPL_MILK_GATE_ENTRY_DETAILS.gate_entry_No<>'" & fndGateEntryNO.Value & "') and Chalan_NO not in (select distinct Challan_No from TSPL_MCC_DISPATCH_TRANSFER where isPosted=0) ", fndTankerNo.Value)
        ''If clsCommon.myLen(fndChallanNoMcc.Value) > 0 Then
        ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_Dispatch_Challan.mcc_code,mcc_name,TSPL_MCC_Dispatch_Challan.dispatch_date,TSPL_MCC_Dispatch_Challan.uom_Code,TSPL_MCC_Dispatch_Challan.tanker_no,TSPL_MCC_Dispatch_Challan.item_code,TSPL_MCC_Dispatch_Challan.item_desc,TSPL_MCC_Dispatch_Challan.net_qty from TSPL_MCC_Dispatch_Challan   where TSPL_MCC_Dispatch_Challan.chalan_no='" & fndChallanNoMcc.Value & "'")
        ''    fndVendorBulk.Value = dt.Rows(0)("mcc_code")
        ''    fndVendorBulk.Enabled = False
        ''    lblVendorNameBulk.Text = dt.Rows(0)("mcc_name")
        ''    dtpChallanDateBulk.Value = dt.Rows(0)("dispatch_date")
        ''    txtTankerNoBulk.Text = dt.Rows(0)("tanker_no")
        ''    fndTankerNo.Value = dt.Rows(0)("tanker_no")
        ''    gvItemBulk.Rows(0).Cells(colItemCode).Value = dt.Rows(0)("item_code")
        ''    gvItemBulk.Rows(0).Cells(colItemDesc).Value = dt.Rows(0)("item_desc")
        ''    gvItemBulk.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("uom_Code"))
        ''    gvItemBulk.Rows(0).Cells(colQty).Value = dt.Rows(0)("net_qty")
        ''    gvItemBulk.Rows(0).Cells(colFat).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='FAT'")
        ''    gvItemBulk.Rows(0).Cells(colSNF).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='SNF'")
        ''    gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
        ''    gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
        ''    Dim objDis As clsMccDispatch = clsMccDispatch.getData(fndChallanNoMcc.Value, NavigatorType.Current)
        ''    gvManualSeal.Rows(0).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No1)
        ''    gvManualSeal.Rows(1).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No2)
        ''    gvManualSeal.Rows(2).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No3)
        ''    gvManualSeal.Rows(3).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No4)
        ''    gvManualSeal.Rows(4).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No5)
        ''    gvManualSeal.Rows(5).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No6)
        ''    gvManualSeal.Rows(6).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No7)
        ''    gvManualSeal.Rows(7).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No8)
        ''    gvManualSeal.Rows(8).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No9)
        ''    gvManualSeal.Rows(9).Cells(colSealNo).Value = clsCommon.myCstr(objDis.Seal_No10)
        ''    If objDis.arrPaperSeal IsNot Nothing AndAlso objDis.arrPaperSeal.Count > 0 Then
        ''        For i As Integer = 0 To objDis.arrPaperSeal.Count - 1
        ''            gvPaperSeal.Rows(i).Cells(colSealNo).Value = objDis.arrPaperSeal(i).Seal_No
        ''        Next
        ''    End If
        ''    dt = Nothing
        ''Else
        ''    fndVendorBulk.Value = ""
        ''    lblVendorNameBulk.Text = ""
        ''    'dtpChallanDateBulk.Value = ""
        ''    txtTankerNoBulk.Text = ""
        ''    fndTankerNo.Value = ""
        ''    gvItemBulk.Rows(0).Cells(colItemCode).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colItemDesc).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colUOM).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colQty).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colFat).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colSNF).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colFatKG).Value = ""
        ''    gvItemBulk.Rows(0).Cells(colSNFKG).Value = ""
        ''End If
        ''=================================
        'Dim qry As String = "select RGP_NO ,*  from TSPL_MILK_RGP_HEAD"
        'fndTankerNo.Value = clsCommon.ShowSelectForm("RGP", qry, "RGP_NO", "", fndTankerNo.Value, "", isButtonClicked)
        'If clsCommon.myLen(clsCommon.myCstr(fndTankerNo.Value)) > 0 Then
        '    fndVendorBulk.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code  from TSPL_MILK_RGP_HEAD where RGP_No='" & fndTankerNo.Value & "'"))
        '    lblVendorNameBulk.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVendorBulk.Value & "'"))
        '    fndLocationBulk.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location from TSPL_MILK_RGP_HEAD where RGP_No='" & fndTankerNo.Value & "'"))
        '    lblLocationDecBulk.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationBulk.Value & "'"))
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_RGP_DETAIL.item_code,TSPL_ITEM_MASTER.item_desc,TSPL_MILK_RGP_DETAIL.Unit_Code from TSPL_MILK_RGP_HEAD left join TSPL_MILK_RGP_DETAIL on TSPL_MILK_RGP_DETAIL.RGP_NO=TSPL_MILK_RGP_HEAD.RGP_NO left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_RGP_DETAIL.item_code where TSPL_MILK_RGP_HEAD.RGP_No='" & fndTankerNo.Value & "'")
        '    '    fndVendorBulk.Value = dt.Rows(0)("mcc_code")
        '    '    fndVendorBulk.Enabled = False
        '    '    lblVendorNameBulk.Text = dt.Rows(0)("mcc_name")
        '    '    dtpChallanDateBulk.Value = dt.Rows(0)("dispatch_date")
        '    '    txtTankerNoBulk.Text = dt.Rows(0)("tanker_no")
        '    '    fndTankerNo.Value = dt.Rows(0)("tanker_no")
        '    gvItemBulk.Rows(0).Cells(colItemCode).Value = dt.Rows(0)("item_code")
        '    gvItemBulk.Rows(0).Cells(colItemDesc).Value = dt.Rows(0)("item_desc")
        '    gvItemBulk.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
        '    gvItemBulk.Columns(colQty).ReadOnly = False
        'End If

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
            clsCommon.MyMessageBoxShow(ex.Message)
            dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        End Try
    End Sub


    Private Sub TxtJobworlLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtJobworlLocation._MYValidating
        Dim strLocations = String.Empty

        strLocations = " Is_Jobwork =1 and isnull(Is_Sub_Location,'')='Y' "
        TxtJobworlLocation.Value = clsLocation.getFinder(strLocations, TxtJobworlLocation.Value, isButtonClicked)
        fndVendorBulk.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Jobwork_Vendor  from TSPL_Location_MASTER  where Location_Code ='" & clsCommon.myCstr(TxtJobworlLocation.Value) & "'"))
        lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)

        If clsCommon.myLen(TxtJobworlLocation.Value) > 0 Then
            lblJobworkLocation.Text = clsLocation.GetName(TxtJobworlLocation.Value, Nothing)
        Else
            lblJobworkLocation.Text = ""
        End If
        strLocations = Nothing
    End Sub

    Private Sub gvItemBulk_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvItemBulk.CurrentColumnChanged
        If gvItemBulk.RowCount > 0 And rbtnSKU.IsChecked = True Then
            Dim intCurrRow As Integer = gvItemBulk.CurrentRow.Index
            gvItemBulk.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvItemBulk.Rows.Count - 1 Then
                'If e.CurrentColumn.HeaderText Is gvItemBulk.Columns(colQty).HeaderText.ToString Then
                gvItemBulk.Rows.AddNew()
                gvItemBulk.CurrentRow = gvItemBulk.Rows(intCurrRow)
                'End If
            End If
        End If
    End Sub

    Private Sub txtTransferNo__My_Click(sender As Object, e As EventArgs) Handles txtTransferNo._My_Click
        Dim qry As String = " select TRANSFER_NO,TRANSFER_DATE,From_Locaction,To_Locaction,Vendor_Code,Total_Net_Amount from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD where Status=1"
        txtTransferNo.arrValueMember = clsCommon.ShowMultipleSelectForm("MGEMulTran", qry, "TRANSFER_NO", "", txtTransferNo.arrValueMember, Nothing)
    End Sub

    Private Sub BtnPrintNew_Click(sender As Object, e As EventArgs) Handles btnPrintNew.Click
        Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        Try
            If clsCommon.myLen(fndGateEntryNO.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(" Transfer No Not Available for Print .", Me.Text)
                Return
            End If
            Dim qry As String = "select TSPL_COMPANY_MASTER.logo_Img,tspl_company_master.Comp_Name " &
              ", TSPL_JWO_GATE_ENTRY.Gate_Entry_No As Transfer_No,TSPL_JWO_GATE_ENTRY.Date_And_Time " &
                ",TSPL_LOCATION_MASTER.Location_Desc " &
              ", TSPL_JWO_GATE_ENTRY_DETAIL.item_code, TSPL_JWO_GATE_ENTRY_DETAIL.Item_Desc as itemdesc   " &
               ", TSPL_JWO_GATE_ENTRY_DETAIL.Qty_In_Kg as qty ,'' AS Item_BatchNo  , TSPL_JWO_GATE_ENTRY_DETAIL.UOM as uom " &
               ",Created.User_Name as CreatedBy,case when TSPL_JWO_GATE_ENTRY.isPosted=1 then Post.User_Name else '' end as PostedBy " &
              " From TSPL_JWO_GATE_ENTRY_DETAIL " &
               " left join TSPL_JWO_GATE_ENTRY on TSPL_JWO_GATE_ENTRY.Gate_Entry_No=TSPL_JWO_GATE_ENTRY_DETAIL.Gate_Entry_No " &
              " Left Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_JWO_GATE_ENTRY.Comp_Code " &
              " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_JWO_GATE_ENTRY.location_code " &
              " Left join TSPL_USER_MASTER  AS Created ON Created.USER_CODE=TSPL_JWO_GATE_ENTRY.Created_By " &
              " left join TSPL_USER_MASTER  AS Post ON Post.USER_CODE=TSPL_JWO_GATE_ENTRY.Modify_By " &
              " where TSPL_JWO_GATE_ENTRY.Gate_Entry_No ='" + fndGateEntryNO.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "RptJWOItemAccepatance", "JOB WORK ITEM ACCEPATANCE", dtDocdate)
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class