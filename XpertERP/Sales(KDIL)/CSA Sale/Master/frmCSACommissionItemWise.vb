''created by Monika 24/09/2016
Imports common
Imports Telerik
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.IO
Imports System.Data.SqlClient

Public Class FrmCSACommissionItemWise
    Inherits FrmMainTranScreen
#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isCellValuseChanged As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim ErrCntrl As New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Const ColLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colCommsnUom As String = "CmsnUOM"
    Const colCommsnType As String = "CmmsnType"
    Const colCommsnRate As String = "CmmsnRate"
    Const colCommsnAC As String = "CmmsnAc"
    Const colCmmsnACDesc As String = "CmmsnACDesc"
    Const colFrghtUom As String = "FrghtUOM"
    Const colFrghtType As String = "FrghtType"
    Const colFrghtRate As String = "FrghtRate"
    Const colFrghtAC As String = "FrghtAc"
    Const colFrghtACDesc As String = "FrghtACDesc"
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSACommissionItemWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCSACommissionItemWise_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                btnClose.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                btnNew.PerformClick()
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colItemCode)) Then
                isCellValuseChanged = True
                OpenItemCode(True)
                isCellValuseChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colCommsnUom)) Then
                isCellValuseChanged = True
                OpenCMSNUOM(True)
                isCellValuseChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colCommsnAC)) Then
                isCellValuseChanged = True
                OpenCMSN_AC(True)
                isCellValuseChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colFrghtUom)) Then
                isCellValuseChanged = True
                OpenFRGHTUOM(True)
                isCellValuseChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso (gv.CurrentColumn Is gv.Columns(colFrghtAC)) Then
                isCellValuseChanged = True
                OpenFRGHT_AC(True)
                isCellValuseChanged = False
            End If
        Catch ex As Exception
            isCellValuseChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "Finder Methods"
    Private Sub OpenItemCode(ByVal isButtonClick As Boolean)
        gv.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(clsItemMaster.getFinder(" tspl_item_master.Active=1 and isnull(tspl_item_master.Is_FreshItem,0)<>1 and tspl_item_master.item_type in ('F','T') ", clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), isButtonClick))
        gv.CurrentRow.Cells(colItemName).Value = clsCommon.myCstr(clsItemMaster.GetItemName(clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), Nothing))
    End Sub

    Private Sub OpenCMSNUOM(ByVal isButtonClick As Boolean)
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as [Description],TSPL_ITEM_UOM_DETAIL.conversion_factor as [CF] from TSPL_ITEM_UOM_DETAIL "
        gv.CurrentRow.Cells(colCommsnUom).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CMSNUOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colCommsnUom).Value), "Code", isButtonClick))
    End Sub

    Private Sub OpenFRGHTUOM(ByVal isButtonClick As Boolean)
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as [Description],TSPL_ITEM_UOM_DETAIL.conversion_factor as [CF] from TSPL_ITEM_UOM_DETAIL "
        gv.CurrentRow.Cells(colFrghtUom).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("FRGHTUOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colFrghtUom).Value), "Code", isButtonClick))
    End Sub

    Private Sub OpenCMSN_AC(ByVal isButtonClick As Boolean)
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        gv.CurrentRow.Cells(colCommsnAC).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CMSNACfnd", qry, "AccountCode", "", clsCommon.myCstr(gv.CurrentRow.Cells(colCommsnAC).Value), "", isButtonClick))
        gv.CurrentRow.Cells(colCmmsnACDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCommsnAC).Value) + "' "))
        'txtcommsn_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Commission_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code in (select Vendor_Account from TSPL_VENDOR_MASTER where vendor_code='" + txtvndrcode.Value + "')"))
    End Sub

    Private Sub OpenFRGHT_AC(ByVal isButtonClick As Boolean)
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        gv.CurrentRow.Cells(colFrghtAC).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("FRGHTACfnd", qry, "AccountCode", "", clsCommon.myCstr(gv.CurrentRow.Cells(colFrghtAC).Value), "", isButtonClick))
        gv.CurrentRow.Cells(colFrghtACDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colFrghtAC).Value) + "' "))
    End Sub
#End Region

    Private Sub FrmCSACommissionItemWise_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for reset window.")
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoTextBox As New GridViewTextBoxColumn()
        Dim repoCombobox As New GridViewComboBoxColumn()
        Dim repoDecimal As New GridViewDecimalColumn()

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Line No."
        repoTextBox.Name = ColLineNo
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 60
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = colItemCode
        repoTextBox.Width = 110
        repoTextBox.WrapText = True
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Description"
        repoTextBox.Name = colItemName
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 220
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Commission UOM"
        repoTextBox.Name = colCommsnUom
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoCombobox = New GridViewComboBoxColumn()
        repoCombobox.FormatString = ""
        repoCombobox.HeaderText = "Commission Type"
        repoCombobox.Name = colCommsnType
        repoCombobox.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'R' as Code,'Rs.' as Name union all select 'P' as Code,'%(Pers)' as Name")
        repoCombobox.ValueMember = "Code"
        repoCombobox.DisplayMember = "Name"
        repoCombobox.Width = 100
        repoCombobox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoCombobox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Commission Rate"
        repoDecimal.Name = colCommsnRate
        repoDecimal.Width = 100
        repoDecimal.WrapText = True
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Commission A/c"
        repoTextBox.Name = colCommsnAC
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Commission A/c Name"
        repoTextBox.Name = colCmmsnACDesc
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 200
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Freight UOM"
        repoTextBox.Name = colFrghtUom
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoCombobox = New GridViewComboBoxColumn()
        repoCombobox.FormatString = ""
        repoCombobox.HeaderText = "Freight Type"
        repoCombobox.Name = colFrghtType
        repoCombobox.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'R' as Code,'Rs.' as Name union all select 'P' as Code,'%(Pers)' as Name")
        repoCombobox.ValueMember = "Code"
        repoCombobox.DisplayMember = "Name"
        repoCombobox.Width = 100
        repoCombobox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoCombobox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Freight Rate"
        repoDecimal.Name = colFrghtRate
        repoDecimal.Width = 100
        repoDecimal.WrapText = True
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Freight A/c"
        repoTextBox.Name = colFrghtAC
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Freight A/c Name"
        repoTextBox.Name = colFrghtACDesc
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 200
        repoTextBox.WrapText = True
        gv.MasterTemplate.Columns.Add(repoTextBox)


        repoTextBox = Nothing
        repoCombobox = Nothing
        repoDecimal = Nothing

        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.ShowFilteringRow = True
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.AllowDeleteRow = True

        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtCSACode.Value) <= 0 Then
                txtCSACode.Focus()
                txtCSACode.Select()
                ErrCntrl.SetError(txtCSACode, "Select CSA detail.")
                Throw New Exception("Please select CSA detail.")
            Else
                ErrCntrl.ResetError(txtCSACode)
            End If

            If clsCommon.myLen(txtVendorCode.Value) <= 0 Then
                txtVendorCode.Focus()
                txtVendorCode.Select()
                ErrCntrl.SetError(txtVendorCode, "Select vendor detail.")
                Throw New Exception("Please select vendor detail.")
            Else
                ErrCntrl.ResetError(txtVendorCode)
            End If


            Dim Icode As String = ""
            Dim cmns_type As String = ""
            Dim cmsnuom As String = ""
            Dim cmsn_ac As String = ""
            Dim frght_type As String = ""
            Dim frghtuom As String = ""
            Dim frght_ac As String = ""
            Dim ArrIcode As New ArrayList()

            For Each grow As GridViewRowInfo In gv.Rows
                Icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                cmns_type = clsCommon.myCstr(grow.Cells(colCommsnType).Value)
                cmsnuom = clsCommon.myCstr(grow.Cells(colCommsnUom).Value)
                cmsn_ac = clsCommon.myCstr(grow.Cells(colCommsnAC).Value)
                frght_type = clsCommon.myCstr(grow.Cells(colFrghtType).Value)
                frghtuom = clsCommon.myCstr(grow.Cells(colFrghtUom).Value)
                frght_ac = clsCommon.myCstr(grow.Cells(colFrghtAC).Value)

                If clsCommon.myLen(Icode) > 0 Then
                    If Not ArrIcode.Contains(Icode) Then
                        ArrIcode.Add(Icode)
                    End If

                    ''if both blank,then select atleast one
                    If clsCommon.myLen(cmsnuom) <= 0 AndAlso clsCommon.myLen(frghtuom) <= 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colCommsnUom)
                        Throw New Exception("Select either commission uom or freight uom at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.myLen(cmns_type) <= 0 AndAlso clsCommon.myLen(cmsnuom) > 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colCommsnType)
                        Throw New Exception("Select commission type at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.myLen(cmsn_ac) <= 0 AndAlso clsCommon.myLen(cmsnuom) > 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colCommsnAC)
                        Throw New Exception("Select commission account at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    ''if both blank,then select atleast one
                    If clsCommon.myLen(cmsnuom) <= 0 AndAlso clsCommon.myLen(frghtuom) <= 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colFrghtUom)
                        Throw New Exception("Select freight uom at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.myLen(frght_type) <= 0 AndAlso clsCommon.myLen(frghtuom) > 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colFrghtType)
                        Throw New Exception("Select freight type at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.myLen(frght_ac) <= 0 AndAlso clsCommon.myLen(frghtuom) > 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colFrghtAC)
                        Throw New Exception("Select freight account at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                End If ''end icode cond
            Next

            If ArrIcode Is Nothing OrElse ArrIcode.Count <= 0 Then
                Throw New Exception("No item detail found to save.")
            End If

            Dim qry As String = "select count(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where doc_no<>'" + clsCommon.myCstr(txtCode.Value) + "' " & _
                " and cust_code='" + clsCommon.myCstr(txtCSACode.Value) + "' and vendor_code='" + clsCommon.myCstr(txtVendorCode.Value) + "'"
            Dim check As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            If check > 0 Then
                Throw New Exception("Duplicate CSA and Vendor mapping not allowed,do modification in old record.")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData()
        Dim obj As New clsCSACommissionFreightMappingHead()
        Dim objtr As New clsCSACommissionFreightMappingDetail()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCSACommissionItemWise, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            obj.Doc_No = clsCommon.myCstr(txtCode.Value)
            obj.Doc_Date = clsCommon.myCDate(dtpdate.Text)
            obj.Cust_Code = clsCommon.myCstr(txtCSACode.Value)
            obj.Vendor_Code = clsCommon.myCstr(txtVendorCode.Value)
            obj.isNewEntry = isNewEntry

            obj.Arr = New List(Of clsCSACommissionFreightMappingDetail)

            For Each grow As GridViewRowInfo In gv.Rows
                objtr = New clsCSACommissionFreightMappingDetail()

                objtr.Commission_AC_Code = clsCommon.myCstr(grow.Cells(colCommsnAC).Value)
                objtr.Commission_Rate = clsCommon.myCdbl(grow.Cells(colCommsnRate).Value)
                objtr.Commission_Type = clsCommon.myCstr(grow.Cells(colCommsnType).Value)
                objtr.Commission_UOM = clsCommon.myCstr(grow.Cells(colCommsnUom).Value)
                objtr.Freight_AC_Code = clsCommon.myCstr(grow.Cells(colFrghtAC).Value)
                objtr.Freight_Rate = clsCommon.myCdbl(grow.Cells(colFrghtRate).Value)
                objtr.Freight_Type = clsCommon.myCstr(grow.Cells(colFrghtType).Value)
                objtr.Freight_UOM = clsCommon.myCstr(grow.Cells(colFrghtUom).Value)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.S_No = clsCommon.myCdbl(grow.Cells(ColLineNo).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            If obj.Arr Is Nothing OrElse obj.Arr.Count <= 0 Then
                Throw New Exception("No record found to save.")
            End If

            If clsCSACommissionFreightMappingHead.SaveData(obj) Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)

                LoadData(obj.Doc_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrCntrl.SetError(txtCode, "Select document code for deletion.")
                Throw New Exception("Select document code for deletion.")
            Else
                ErrCntrl.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsCSACommissionFreightMappingHead.DeleteData(clsCommon.myCstr(txtCode.Value)) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FunReset()
        txtCode.Value = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtCSACode.Value = ""
        txtCSAName.Text = ""
        txtVendorCode.Value = ""
        txtVendorName.Text = ""
        isNewEntry = True

        gv.Rows.Clear()
        gv.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        txtCode.MyReadOnly = True
        txtCSACode.Enabled = True

        RadPageView1.SelectedPage = RadPageViewPage1
        dtpdate.Focus()
        dtpdate.Select()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtVendorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorCode._MYValidating
        txtVendorCode.Value = clsCommon.myCstr(clsVendorMaster.getFinder(" isnull(csa_type,'N')='Y' ", clsCommon.myCstr(txtVendorCode.Value), isButtonClicked))
        txtVendorName.Text = clsCommon.myCstr(clsVendorMaster.GetName(clsCommon.myCstr(txtVendorCode.Value), Nothing))
        ResetGridAccounts()
    End Sub

    Private Sub ResetGridAccounts()
        For Each grow As GridViewRowInfo In gv.Rows
            grow.Cells(colCommsnAC).Value = ""
            grow.Cells(colCmmsnACDesc).Value = ""
            grow.Cells(colFrghtAC).Value = ""
            grow.Cells(colFrghtACDesc).Value = ""
        Next
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select 1 from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where doc_no='" + clsCommon.myCstr(txtCode.Value) + "'"
        txtCode.MyReadOnly = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCSACommissionFreightMappingHead.GetFinder(" TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "' ", clsCommon.myCstr(txtCode.Value), isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCSACommissionFreightMappingHead()
        Try
            isInsideLoadData = True
            FunReset()

            obj = clsCSACommissionFreightMappingHead.GetData(strCode, NavType, Nothing)

            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                txtCode.Value = obj.Doc_No
                dtpdate.Text = obj.Doc_Date
                txtCSACode.Value = obj.Cust_Code
                txtCSAName.Text = obj.Cust_Name
                txtVendorCode.Value = obj.Vendor_Code
                txtVendorName.Text = obj.Vendor_Name

                gv.Rows.Clear()
                gv.Rows.AddNew()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsCSACommissionFreightMappingDetail In obj.Arr
                        gv.Rows(gv.Rows.Count - 1).Cells(ColLineNo).Value = objtr.S_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Name
                        gv.Rows(gv.Rows.Count - 1).Cells(colCmmsnACDesc).Value = objtr.Commission_AC_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommsnAC).Value = objtr.Commission_AC_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommsnRate).Value = objtr.Commission_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommsnType).Value = objtr.Commission_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommsnUom).Value = objtr.Commission_UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtAC).Value = objtr.Freight_AC_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtACDesc).Value = objtr.Freight_AC_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtRate).Value = objtr.Freight_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtType).Value = objtr.Freight_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtUom).Value = objtr.Freight_UOM

                        gv.Rows.AddNew()
                    Next
                End If

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                txtCode.MyReadOnly = True
                txtCSACode.Enabled = False
                isNewEntry = False
            Else
                FunReset()
            End If ''end if cond
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
        End If
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValuseChanged Then
                    If e.Column Is gv.Columns(colItemCode) Then
                        isCellValuseChanged = True
                        OpenItemCode(False)
                        isCellValuseChanged = False
                    End If

                    If e.Column Is gv.Columns(colCommsnAC) Then
                        isCellValuseChanged = True
                        OpenCMSN_AC(False)
                        isCellValuseChanged = False
                    End If

                    If e.Column Is gv.Columns(colCommsnUom) Then
                        isCellValuseChanged = True
                        OpenCMSNUOM(False)
                        isCellValuseChanged = False
                    End If

                    If e.Column Is gv.Columns(colFrghtAC) Then
                        isCellValuseChanged = True
                        OpenFRGHT_AC(False)
                        isCellValuseChanged = False
                    End If

                    If e.Column Is gv.Columns(colFrghtUom) Then
                        isCellValuseChanged = True
                        OpenFRGHTUOM(False)
                        isCellValuseChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValuseChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(ColLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtCSACode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCSACode._MYValidating
        txtCSACode.Value = clsCommon.myCstr(clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' ", clsCommon.myCstr(txtCSACode.Value), isButtonClicked))
        txtCSAName.Text = clsCommon.myCstr(clsCustomerMaster.GetName(clsCommon.myCstr(txtCSACode.Value), Nothing))
        txtVendorCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_code from tspl_location_master where cust_code='" + txtCSACode.Value + "'"))
        txtVendorName.Text = clsCommon.myCstr(clsVendorMaster.GetName(clsCommon.myCstr(txtVendorCode.Value), Nothing))
        'vendor
    End Sub


    


    Private Sub btnExportHead_Click(sender As Object, e As EventArgs) Handles btnExportHead.Click
        Try
            Dim whrcls As String = Nothing
            Dim qry As String = "select convert(varchar,Doc_Date,103) as Doc_Date,Cust_Code,Vendor_Code from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD"
            transportSql.ExporttoExcel(qry, whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExportDetail_Click(sender As Object, e As EventArgs) Handles btnExportDetail.Click
        Try
            Dim whrcls As String = Nothing
            Dim qry As String = "select Head.Doc_No,Detail.S_No,Detail.Item_Code,Detail.Commission_Rate," & _
                                " Detail.Commission_UOM,Detail.Commission_Type,Detail.Commission_AC_Code,Detail.Freight_Rate,Detail.Freight_UOM,Detail.Freight_Type," & _
                                " Detail.Freight_AC_Code from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD as Head" & _
                                " inner join TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL as Detail on Head.Doc_No=Detail.Doc_No"
            transportSql.ExporttoExcel(qry, whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnImportHead_Click(sender As Object, e As EventArgs) Handles btnImportHead.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gv1 As New RadGridView()
        Dim coll As New Hashtable()
        Try

            Me.Controls.Add(gv1)
            Dim DocDateImp As DateTime? = Nothing
            Dim CustCodeImp As String = ""
            Dim VendorCodeImp As String = ""
            Dim DoCNoImp As String = ""
            Dim qry As String = ""
            Dim check As Integer = 0



            If transportSql.importExcel(gv1, "Doc_Date", "Cust_Code", "Vendor_Code") Then

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows

                    If grow.Cells("Doc_Date").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Doc_Date").Value) > 0 AndAlso IsDate(grow.Cells("Doc_Date").Value) Then
                        DocDateImp = clsCommon.myCDate(grow.Cells("Doc_Date").Value)
                    Else
                        Throw New Exception("Enter Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                   

                    CustCodeImp = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                    If clsCommon.myLen(CustCodeImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter CSA Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    ''check in customer
                    If clsCommon.myLen(CustCodeImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + CustCodeImp + "' "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Customer Code " + CustCodeImp + "is not valid. First create Customer,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If

                    VendorCodeImp = clsCommon.myCstr(grow.Cells("Vendor_Code").Value)
                    If clsCommon.myLen(VendorCodeImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Vendor Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(VendorCodeImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_VENDOR_MASTER where Vendor_Code='" + VendorCodeImp + "' "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Vendor Code " + VendorCodeImp + "is not valid. First create Vendor,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If

                    If clsCommon.myLen(VendorCodeImp) > 0 AndAlso clsCommon.myLen(CustCodeImp) > 0 Then
                        qry = Nothing
                        qry = "select count(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where  " & _
                        " cust_code='" + clsCommon.myCstr(CustCodeImp) + "' and vendor_code='" + clsCommon.myCstr(VendorCodeImp) + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check > 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Duplicate CSA and Vendor mapping not allowed,do modification in old record.")
                        End If
                    End If


                    qry = Nothing
                    qry = "select max(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD"
                    DoCNoImp = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    If clsCommon.myLen(DoCNoImp) > 0 Then
                        DoCNoImp = clsCommon.incval(DoCNoImp)
                    Else
                        DoCNoImp = "CSA-CF-0000000001"
                    End If

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DoCNoImp)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(DocDateImp, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", CustCodeImp, True)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", VendorCodeImp, True)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub btnImportDetail_Click(sender As Object, e As EventArgs) Handles btnImportDetail.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gv1 As New RadGridView()
        Dim coll As New Hashtable()
        Me.Controls.Add(gv1)
        Dim DocNoImp As String = ""
        Dim SNoImp As Integer = 0
        Dim IcodeImp As String = ""
        Dim cmns_typeImp As String = ""
        Dim cmsnuomImp As String = ""
        Dim cmsn_acImp As String = ""
        Dim frght_typeImp As String = ""
        Dim frghtuomImp As String = ""
        Dim frght_acImp As String = ""
        Dim cmns_rateImp As String = ""
        Dim Freight_RateImp As String = ""
        Dim qry As String = ""
        Dim check As Integer = 0
        Dim lineno As Integer = 1
        Dim arrstrCode As New ArrayList

        Try
           

            If transportSql.importExcel(gv1, "Doc_No", "S_No", "Item_Code", "Commission_Rate", "Commission_UOM", "Commission_Type", "Commission_AC_Code", "Freight_Rate", "Freight_UOM",
                                        "Freight_Type", "Freight_AC_Code") Then

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows

                    DocNoImp = clsCommon.myCstr(grow.Cells("Doc_No").Value)
                    If clsCommon.myLen(DocNoImp) <= 0 Then
                        Throw New Exception("Enter Doc No at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(DocNoImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_ITEM_COMMISSION_FREIGHT_AC_Head where Doc_No='" + DocNoImp + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Doc No " + DocNoImp + "is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")

                        End If
                    End If

                    SNoImp = clsCommon.myCdbl(grow.Cells("S_No").Value)
                    

                    IcodeImp = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    If clsCommon.myLen(IcodeImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Item Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(IcodeImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_ITEM_MASTER where Item_Code='" + IcodeImp + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Item Code " + IcodeImp + "is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myLen(cmsnuomImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL where Item_Code='" + IcodeImp + "' and Doc_No='" + DocNoImp + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check > 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Item already mapped with doc No " + DocNoImp + ",see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    cmsnuomImp = clsCommon.myCstr(grow.Cells("Commission_UOM").Value)
                    frghtuomImp = clsCommon.myCstr(grow.Cells("Freight_UOM").Value)
                    If clsCommon.myLen(frghtuomImp) <= 0 AndAlso clsCommon.myLen(cmsnuomImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Fill either Commission or freight uom at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If


                    ''check unit from TSPL_ITEM_UOM_DETAIL,with item_code
                    If clsCommon.myLen(cmsnuomImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code='" + IcodeImp + "' and UOM_Code='" + cmsnuomImp + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled Commission UOM " + cmsnuomImp + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    cmns_typeImp = clsCommon.myCstr(grow.Cells("Commission_Type").Value)
                    If clsCommon.myLen(cmsnuomImp) > 0 AndAlso clsCommon.myLen(cmns_typeImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter commission type at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    ''check type should be P or R
                    If clsCommon.myLen(cmsnuomImp) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cmns_typeImp), "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cmns_typeImp), "R") <> CompairStringResult.Equal Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter commission type P(%) or R(Rs.) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    


                    cmsn_acImp = clsCommon.myCstr(grow.Cells("Commission_AC_Code").Value)
                    If clsCommon.myLen(cmsnuomImp) > 0 AndAlso clsCommon.myLen(cmsn_acImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter commission account  at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    ''check ac from tspl_gl_accounts
                    If clsCommon.myLen(cmsn_acImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from tspl_gl_accounts where Account_Code='" + cmsn_acImp + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled commission account code " + cmsn_acImp + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    cmns_rateImp = clsCommon.myCstr(grow.Cells("Commission_Rate").Value)
                    If clsCommon.myLen(cmsnuomImp) > 0 AndAlso clsCommon.myCdbl(cmns_rateImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Commisssion rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If


                    Freight_RateImp = clsCommon.myCstr(grow.Cells("Freight_Rate").Value)
                    If clsCommon.myLen(frghtuomImp) > 0 AndAlso clsCommon.myCdbl(Freight_RateImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter Freight rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If


                    frght_typeImp = clsCommon.myCstr(grow.Cells("Freight_Type").Value)
                    If clsCommon.myLen(frghtuomImp) > 0 AndAlso clsCommon.myLen(frght_typeImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter freight type at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    ''check type should be P or R
                    If clsCommon.myLen(frghtuomImp) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(frght_typeImp), "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(frght_typeImp), "R") <> CompairStringResult.Equal Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter freight type P(%) or R(Rs) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    frght_acImp = clsCommon.myCstr(grow.Cells("Freight_AC_Code").Value)
                    If clsCommon.myLen(frghtuomImp) > 0 AndAlso clsCommon.myLen(frght_acImp) <= 0 Then
                        clsCommon.ProgressBarHide()
                        Throw New Exception("Enter freight account at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(frghtuomImp) > 0 AndAlso clsCommon.myLen(frght_acImp) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from tspl_gl_accounts where Account_Code='" + frght_acImp + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            clsCommon.ProgressBarHide()
                            Throw New Exception("Filled freight account code " + frght_acImp + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    If clsCommon.myLen(DocNoImp) > 0 Then
                        If Not arrstrCode.Contains(DocNoImp) Then
                            lineno = 1
                            arrstrCode.Add(DocNoImp)
                        End If
                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Doc_No", DocNoImp)
                        clsCommon.AddColumnsForChange(coll, "S_No", clsCommon.myCstr(lineno))
                        clsCommon.AddColumnsForChange(coll, "Item_Code", IcodeImp, True)
                        clsCommon.AddColumnsForChange(coll, "Commission_Rate", cmns_rateImp)
                        clsCommon.AddColumnsForChange(coll, "Commission_UOM", cmsnuomImp, True)
                        clsCommon.AddColumnsForChange(coll, "Commission_Type", cmns_typeImp)
                        clsCommon.AddColumnsForChange(coll, "Commission_AC_Code", cmsn_acImp, True)
                        clsCommon.AddColumnsForChange(coll, "Freight_Rate", Freight_RateImp)
                        clsCommon.AddColumnsForChange(coll, "Freight_UOM", frghtuomImp, True)
                        clsCommon.AddColumnsForChange(coll, "Freight_Type", frght_typeImp)
                        clsCommon.AddColumnsForChange(coll, "Freight_AC_Code", frght_acImp, True)

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL", OMInsertOrUpdate.Insert, "", trans)


                        lineno += 1
                    End If
                    
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            End If
            
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub btnCombinedExport_Click(sender As Object, e As EventArgs) Handles btnCombinedExport.Click
        Dim qry As String = "select convert(varchar,TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_Date,103) as Doc_Date,TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Type,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_UOM,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_AC_Code,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Type,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_UOM,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_AC_Code from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL on TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No "
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnImportCombined_Click(sender As Object, e As EventArgs) Handles btnImportCombined.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim Line_No As Integer = 0
        Dim Doc_No As String = Nothing
        Dim Doc_date As DateTime? = Nothing
        Dim Cust_code As String = Nothing
        Dim Item_code As String = Nothing
        Dim Vendor_code As String = Nothing
        Dim commission_type As String = Nothing
        Dim commission_rate As String = Nothing
        Dim commission_uom As String = Nothing
        Dim commission_ac_code As String = Nothing
        Dim freight_type As String = Nothing
        Dim freight_rate As String = Nothing
        Dim freight_uom As String = Nothing
        Dim freight_ac_code As String = Nothing

        Dim qry As String = ""
        Dim check As Integer = 0

        If transportSql.importExcel(gv, "Doc_Date", "Cust_Code", "Item_Code", "Commission_Type", "Commission_Rate", "Commission_UOM", "Commission_AC_Code", "Freight_Type", "Freight_Rate", "Freight_UOM", "Freight_AC_Code") Then
            Try
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows


                    If grow.Cells("Doc_Date").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Doc_Date").Value) > 0 AndAlso IsDate(grow.Cells("Doc_Date").Value) Then
                        Doc_date = clsCommon.myCDate(grow.Cells("Doc_Date").Value)
                    Else
                        Throw New Exception("Enter Doc Date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    Cust_code = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                    If clsCommon.myLen(Cust_code) <= 0 Then
                        Throw New Exception("Enter Customer Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(Cust_code) > 0 Then
                        qry = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Cust_code + "' "
                        check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Filled Customer Code " + Cust_code + "is not valid. First create Customer,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        Else
                            Vendor_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_code from tspl_location_master where cust_code='" + clsCommon.myCstr(Cust_code) + "'", trans))
                        End If
                    End If

                    Item_code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                    If clsCommon.myLen(Item_code) <= 0 Then
                        Throw New Exception("Enter Item Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(Item_code) > 0 Then
                        qry = "select count(*) from TSPL_ITEM_MASTER where Item_Code='" + Item_code + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Item Code " + Item_code + "is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    commission_uom = clsCommon.myCstr(grow.Cells("Commission_UOM").Value)
                    freight_uom = clsCommon.myCstr(grow.Cells("Freight_UOM").Value)
                    If clsCommon.myLen(freight_uom) <= 0 AndAlso clsCommon.myLen(commission_uom) <= 0 Then
                        Throw New Exception("Fill either Commission or freight uom at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(commission_uom) > 0 Then
                        qry = "select count(*) from TSPL_ITEM_UOM_DETAIL where Item_Code='" + Item_code + "' and UOM_Code='" + commission_uom + "' "
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Commission UOM " + commission_uom + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    commission_type = clsCommon.myCstr(grow.Cells("Commission_Type").Value)
                    If clsCommon.myLen(commission_uom) > 0 AndAlso clsCommon.myLen(commission_type) <= 0 Then
                        Throw New Exception("Enter commission type at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(commission_uom) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(commission_type), "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(commission_type), "R") <> CompairStringResult.Equal Then
                        Throw New Exception("Enter commission type P(%) or R(Rs.) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    commission_ac_code = clsCommon.myCstr(grow.Cells("Commission_AC_Code").Value)
                    If clsCommon.myLen(commission_uom) > 0 AndAlso clsCommon.myLen(commission_ac_code) <= 0 Then
                        Throw New Exception("Enter commission account  at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(commission_ac_code) > 0 Then
                        qry = Nothing
                        qry = "select count(*) from tspl_gl_accounts where Account_Code='" + commission_ac_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled commission account code " + commission_ac_code + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    commission_rate = clsCommon.myCstr(grow.Cells("Commission_Rate").Value)
                    If clsCommon.myLen(commission_uom) > 0 AndAlso Not IsNumeric(commission_rate) Then
                        Throw New Exception("Enter Commisssion rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    freight_rate = clsCommon.myCstr(grow.Cells("Freight_Rate").Value)
                    If clsCommon.myLen(freight_uom) > 0 AndAlso Not IsNumeric(freight_rate) Then
                        Throw New Exception("Enter Freight rate at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    freight_type = clsCommon.myCstr(grow.Cells("Freight_Type").Value)
                    If clsCommon.myLen(freight_uom) > 0 AndAlso clsCommon.myLen(freight_type) <= 0 Then
                        Throw New Exception("Enter freight type at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(freight_uom) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(freight_type), "P") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(freight_type), "R") <> CompairStringResult.Equal Then
                        Throw New Exception("Enter freight type P(%) or R(Rs) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    Freight_ac_code = clsCommon.myCstr(grow.Cells("Freight_AC_Code").Value)
                    If clsCommon.myLen(freight_uom) > 0 AndAlso clsCommon.myLen(Freight_ac_code) <= 0 Then
                        Throw New Exception("Enter freight account at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(freight_uom) > 0 AndAlso clsCommon.myLen(Freight_ac_code) > 0 Then
                        qry = "select count(*) from tspl_gl_accounts where Account_Code='" + Freight_ac_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled freight account code " + Freight_ac_code + " is not valid,see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    'for inserting in head table
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(Doc_date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", Cust_code, True)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", Vendor_code, True)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    qry = "select count(*) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where cust_code='" + clsCommon.myCstr(Cust_code) + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If check <= 0 Then

                        qry = "select max(doc_no) from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD"
                        Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(Doc_No) > 0 Then
                            Doc_No = clsCommon.incval(Doc_No)
                        Else
                            Doc_No = "CSA-CF-0000000001"
                        End If
                        clsCommon.AddColumnsForChange(coll, "Doc_No", clsCommon.myCstr(Doc_No))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD", OMInsertOrUpdate.Update, " cust_code='" + clsCommon.myCstr(Cust_code) + "' ", trans)
                    End If


                    'for inserting in detail table'
                    qry = "select doc_no from TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD where cust_code='" + clsCommon.myCstr(Cust_code) + "'"
                    Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", Item_code, True)
                    clsCommon.AddColumnsForChange(coll, "Commission_Rate", commission_rate)
                    clsCommon.AddColumnsForChange(coll, "Commission_UOM", commission_uom, True)
                    clsCommon.AddColumnsForChange(coll, "Commission_Type", commission_type)
                    clsCommon.AddColumnsForChange(coll, "Commission_AC_Code", commission_ac_code, True)
                    clsCommon.AddColumnsForChange(coll, "Freight_Rate", freight_rate)
                    clsCommon.AddColumnsForChange(coll, "Freight_UOM", freight_uom, True)
                    clsCommon.AddColumnsForChange(coll, "Freight_Type", freight_type)
                    clsCommon.AddColumnsForChange(coll, "Freight_AC_Code", freight_ac_code, True)

                    qry = "select count(*) from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL where doc_no='" + clsCommon.myCstr(Doc_No) + "' and item_code='" + clsCommon.myCstr(Item_code) + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If check <= 0 Then

                        qry = "select count(*) from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL where doc_no='" + clsCommon.myCstr(Doc_No) + "'"
                        Line_No = CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) + 1)

                        clsCommon.AddColumnsForChange(coll, "S_No", Line_No)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL", OMInsertOrUpdate.Update, " doc_no='" + clsCommon.myCstr(Doc_No) + "' and item_code='" + clsCommon.myCstr(Item_code) + "' ", trans)
                    End If

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
End Class
