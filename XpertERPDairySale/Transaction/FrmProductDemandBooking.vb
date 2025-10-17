Imports common
Public Class FrmProductDemandBooking
#Region "Variables"
    Dim gvFullMode As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Public Shared LockUnlock As Integer = 0
    Dim EnableLocation As Boolean = False
    Dim DisableRouteandVehicle As Boolean = False
    Dim EnableResetDemand As Boolean = False
    Dim ConvertPouchtoCrate As Boolean = False
    Dim DontCreateForPouch As Boolean = False
    Dim SeparateDemandMilkandProduct As Boolean = False
    Dim LockedByUserName As String = ""
    Dim LockedByUserCode As String = ""
    Dim dr As DataRow
    Dim SingleUserParticularDairyBookingEdit As Boolean = False
    Dim UseCutOffTimeonRouteForERP As Boolean = False
    Dim checkCreditLimit As Boolean = False
    Dim FlagPaste As Boolean = False
    Dim ChangeVehicleOnDairySaleBooking As Boolean = False
    Dim FlagChangeVehical As Boolean = False
    Dim ChangedRouteNo As String = ""
    Dim ChangedVehicalNo As String = ""
    'Dim ChangedVehicleCustCode As String = ""
    Dim blnPageLoad As Boolean = False
    Dim intChangeColumn As Integer = 0
    Public StrDocNo As String = ""
    Public arrBookingItem As List(Of clsBookingTemp) = Nothing
    Dim blnSaveTotalQTy As Boolean = False
    Dim DOmsg As String = ""
    Private isNewEntry As Boolean = False
    Private DOCreated As Boolean = False
    Dim AllowWo_Outstanding As Boolean
    Dim CheckOutstandingOnbooking As Integer = 0
    Dim DairyTaxableOrNonTaxable As Integer = 0
    Dim ShowItemLocationWiseonBooking As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colItemExist As String = "colItemExist"
    Const colIsItemUpdate As String = "colIsItemUpdate"
    Const colBookingCreatedFor3Days As String = "colBookingCreatedFor3Days"
    Const colLineNo As String = "COLLNO"
    Const colTripNo As String = "colTripNo"
    Const colbtncol As String = "colbtncol"
    Const colCustCode As String = "colCustCode"
    Const colCreated_By As String = "colCreated_By"
    Const colCustName As String = "colCustName"
    Const colShiftName As String = "colShiftName"
    Const colItemCode As String = "colItemCode"
    Const colCrate As String = "colCrate"
    Const colLitre As String = "colLitre"
    Const colPCount As String = "colPCount"
    Const colPAmt As String = "colPAmt"
    Const colMAmt As String = "colMAmt"
    Const colAmt As String = "COLAMT"
    'Const colTaxGroup As String = "colTaxGroup"
    ''Const colIsKKF As String = "colIsKKF"
    ''Const colIsMNDTax As String = "colIsMNDTax"
    'Const colTax As String = "colTax"
    'Const colTax_Base_Amt As String = "colTax_Base_Amt"
    'Const colTax_Rate As String = "colTax_Rate"
    'Const colTax_Amt As String = "colTax_Amt"
    Const colLocCode As String = "colLocCode"
    Const colLocName As String = "colLocName"
    Const ReportID As String = "DairyBookingGrid"
    Const colQty As String = "colQty"
    Const colICode As String = "colICode"
    Const colIDesc As String = "colIDesc"
    Const colIHSN As String = "colIHSN"
    Const colSDesc As String = "colSDesc"
    Const colUnit As String = "colUnit"
    Const colTotalQty As String = "colTotalQty"
    Const colISeqNo As String = "colISeqNo"
    Const colIGroup As String = "colIGroup"
    Dim strSql As String
    Dim dblCustOutstandingAmt As Double = 0
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Boolean = False
    Dim DoNotConsiderCustomerCreditLimit As Boolean = False
    Dim ImportProcess As Boolean = False
    Dim SettSeprateDemandForMorningEveningShift As Boolean = False
    Dim UpdateDemandBeforePost As Boolean = False
    Dim lstCustItem As List(Of clsDemandCustItem)
#End Region
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'RadMenu1.Visible = MyBase.isExport
        If MyBase.isExport = True Then
            btnExport.Enabled = True
            btnImport.Enabled = True
        Else
            btnExport.Enabled = False
            btnImport.Enabled = False
        End If
        btnreverse.Visible = False
        'If MyBase.isReverse Then
        '    btnreverse.Enabled = True
        'Else
        '    btnreverse.Enabled = False
        'End If
    End Sub
    Private Sub FrmProductDemandBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
            DisableRouteandVehicle = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisableRouteandVehicle, clsFixedParameterCode.DisableRouteandVehicle, Nothing)) = 1, True, False)
            AddNew()
            CreateTable()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FrmProductDemandBooking_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            'setGridFocus()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            '    If clsCommon.myLen(txtDocNo.Value) > 0 Then
            '        Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "'"))
            '        If clsCommon.CompairString(txtRouteNo.Value, RouteNo) = CompairStringResult.Equal Then
            '            SaveData(0, False)
            '        Else
            '            clsCommon.MyMessageBoxShow(Me, "You can't change route", Me.Text)
            '            txtRouteNo.Value = RouteNo
            '        End If
            '    Else
            '        SaveData(0, False)
            '    End If
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            '    DeleteData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            '    CloseForm()
        ElseIf e.KeyCode = Keys.Enter Then
            setGridFocus()
            'ElseIf e.KeyCode = Keys.PageDown Then
            '    setPagedown()
        ElseIf e.KeyCode = Keys.Home Then
            setGridFocusHome()
        ElseIf e.KeyCode = Keys.End Then
            setGridFocusEnd()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIR
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If
                'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                '  "TSPL_DEMAND_BOOKING_MASTER " + Environment.NewLine +
                '                  "TSPL_DEMAND_BOOKING_DETAIL " + Environment.NewLine +
                '"TSPL_BOOKING_MATSER " + Environment.NewLine +
                '                  "TSPL_BOOKING_DETAIL " + Environment.NewLine +
                '                  "TSPL_GATEPASS_MASTER_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                '                  "TSPL_GATEPASS_DETAIL_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                '                  "Press Alt+F for Create DO/Post DO Trasnaction" + Environment.NewLine +
                '                  "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " + Environment.NewLine +
                '                  "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " + Environment.NewLine +
                '                  "TSPL_TRANSACTION_APPROVAL (For Approving Pending Document) ")
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub AddNew()
        Try
            'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
            '    btnPrintChallan.Visible = True
            'Else
            '    btnPrintChallan.Visible = False
            'End If
            lstCustItem = New List(Of clsDemandCustItem)
            blnSaveTotalQTy = False
            isNewEntry = True
            btnSave.Text = "Save"
            txtDocNo.Value = ""
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            RadGroupBox3.Enabled = True
            txtcustomersearch.Text = ""
            txtDate.Value = clsCommon.GETSERVERDATE()
            UsLock1.Status = ERPTransactionStatus.Pending
            chkIndividualCustomer.Checked = False
            chkIndividualCustomer.Enabled = True
            TxtCity.Value = ""
            lblTransporterName.Text = ""
            lblCityName.Text = ""
            txtRouteNo.Value = ""
            txtRouteNo.Enabled = True
            lblRouteDesc.Text = ""
            txtVehicleNo.Value = ""
            txtVehicleNo.Enabled = True
            lblVehicleNo.Text = ""
            txtCustomerNo.Value = ""
            lblCustomerName.Text = ""
            txtLocation.Value = Nothing
            txtCustomerNo.Enabled = False
            lblLocation.Text = ""
            txtPCount.Text = "0"
            txtPAmt.Text = "0"
            'RadGroupBox1.Enabled = True
            txtDate.Enabled = True
            If EnableLocation Then
                txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Route_Master where Route_No='" + txtRouteNo.Value + "' "))
            Else
                txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
            LoadBlankGrid()
            HideUnhideRowsAndColumnsOFGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkIndividualCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIndividualCustomer.ToggleStateChanged
        If chkIndividualCustomer.Checked = True Then
            txtCustomerNo.Enabled = True
        Else
            txtCustomerNo.Enabled = False
        End If
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        'rdbnFreshAmbientBoth.IsChecked = True
        gv1.DataSource = Nothing
        'gv1.ViewDefinition = New TableViewDefinition
        gv1.Rows.AddNew()
        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn
        Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
        RepobtnCol.HeaderText = "Action "
        RepobtnCol.Name = colbtncol
        RepobtnCol.ReadOnly = False
        RepobtnCol.Width = 150
        RepobtnCol.DefaultText = "Reset ..."
        If EnableResetDemand Then
            RepobtnCol.IsVisible = True
        Else
            RepobtnCol.IsVisible = False
        End If
        RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(RepobtnCol)
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Code"
        repoCustCode.Name = colCustCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 50
        repoCustCode.IsVisible = True
        repoCustCode.IsPinned = True
        repoCustCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Name"
        repoCName.Name = colCustName
        repoCName.Width = 150
        repoCName.ReadOnly = True
        repoCName.IsVisible = True
        repoCName.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoCName)
        Dim repoItemValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemValue.FormatString = ""
        repoItemValue.HeaderText = "Is Item Value Exist"
        repoItemValue.Name = colItemExist
        repoItemValue.Width = 150
        repoItemValue.ReadOnly = True
        repoItemValue.IsVisible = False
        repoItemValue.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoItemValue)
        Dim repoItemUpdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemUpdate.FormatString = ""
        repoItemUpdate.HeaderText = "Is Item Update After Save"
        repoItemUpdate.Name = colIsItemUpdate
        repoItemUpdate.Width = 150
        repoItemUpdate.ReadOnly = True
        repoItemUpdate.IsVisible = False
        repoItemUpdate.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoItemUpdate)
        Dim repoBookingCreatedFor3Days As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBookingCreatedFor3Days.FormatString = ""
        repoBookingCreatedFor3Days.HeaderText = "Booking Created for last 3 days"
        repoBookingCreatedFor3Days.Name = colBookingCreatedFor3Days
        repoBookingCreatedFor3Days.Width = 150
        repoBookingCreatedFor3Days.ReadOnly = True
        repoBookingCreatedFor3Days.IsVisible = False
        repoBookingCreatedFor3Days.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoBookingCreatedFor3Days)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (
	select 'Product' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  TypeOfItm ='P' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1
	union
    select 'IceCream' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where   TypeOfItm ='I' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1
    )z order by RowNo,Sku_Seq,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("UOM_Code"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                obj.Conversion_Factor = clsCommon.myCdbl(dr("Conversion_Factor"))
                obj.Stocking_Unit = clsCommon.myCstr(dr("Stocking_Unit"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 150
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        Dim repoPCount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPCount = New GridViewDecimalColumn()
        repoPCount.FormatString = ""
        repoPCount.HeaderText = "PCount"
        repoPCount.Name = colPCount
        repoPCount.Width = 80
        repoPCount.Minimum = 0
        repoPCount.ReadOnly = True
        repoPCount.IsVisible = True
        repoPCount.IsPinned = True
        repoPCount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPCount)
        Dim repoPAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPAmt = New GridViewDecimalColumn()
        repoPAmt.FormatString = ""
        repoPAmt.HeaderText = "PAmt"
        repoPAmt.Name = colPAmt
        repoPAmt.Width = 80
        repoPAmt.Minimum = 0
        repoPAmt.ReadOnly = True
        repoPAmt.IsVisible = True
        repoPAmt.IsPinned = True
        repoPAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPAmt)
        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Total Amt"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = False
        repoAmt.IsPinned = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)
        Dim repoCreatedby As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCreatedby.FormatString = ""
        repoCreatedby.HeaderText = "Created By"
        repoCreatedby.Name = colCreated_By
        repoCreatedby.Width = 150
        repoCreatedby.ReadOnly = True
        repoCreatedby.IsVisible = False
        repoCreatedby.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoCreatedby)
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        View()
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("Booth"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colbtncol).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustCode).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustName).Name)
                'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colShiftName).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colItemExist).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIsItemUpdate).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colBookingCreatedFor3Days).Name)
                view.ColumnGroups(0).IsPinned = True
                Dim TempColGroupCount As Integer = 1
                Dim obj As ItemValueClass = New ItemValueClass()
                Dim i As Integer = 1
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 5
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        If clsCommon.CompairString(obj1.IsFreshAmbient, "Fresh") = CompairStringResult.Equal Then
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            dblcolumns = dblcolumns + 1
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            dblcolumns = dblcolumns + 1
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                        Else
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                        End If
                    End If
                Next
                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
                view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colPCount).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colPAmt).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colAmt).Name)
                view.ColumnGroups(TempColGroupCount).IsPinned = True
                view.ColumnGroups(TempColGroupCount).PinPosition = PinnedColumnPosition.Right
                'MergeHorizontally(gv1, 0, gv1.Rows.Count - 1)
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'  "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_Product_DEMAND_BOOKING_MASTER.Document_No as DocumentNo,convert(varchar(12),TSPL_Product_DEMAND_BOOKING_MASTER.Document_date,103) as DocumentDate,TSPL_Product_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_Product_DEMAND_BOOKING_MASTER.Location_Code as [Location Code],TSPL_Product_DEMAND_BOOKING_MASTER.City_Code as [City Code],case when TSPL_PRODUCT_DEMAND_BOOKING_MASTER.ItemType='Product' then 'Product' else 'IceCream' end as ItemType,TSPL_Product_DEMAND_BOOKING_MASTER.IsIndividualCustomer as [Individual Cust],case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_Product_DEMAND_BOOKING_MASTER "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentNo", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_Product_DEMAND_BOOKING_MASTER.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim isClicked As Boolean = isButtonClicked
            RouteData(isClicked, False)
            HideUnhideRowsAndColumnsOFGrid()
            txtRouteNo.Enabled = False
            RadGroupBox3.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RouteData(ByVal isClicked As Boolean, ByVal isQuickDemand As Boolean)
        Try
            Dim qry As String = String.Empty
            Dim ItemType As String = ""
            Dim shiftType As String = ""
            If SeparateDemandMilkandProduct Then
                qry = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            Else
                qry = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            End If
            If Not isQuickDemand Then
                txtRouteNo.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isClicked)
                lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRouteNo.Value, Nothing))
            End If
            qry = "Select Document_No from TSPL_Product_DEMAND_BOOKING_MASTER where Route_No = '" & txtRouteNo.Value & "' and Posted=0  and IsIndividualCustomer=0"
            If rbtn_Product.IsChecked Then
                qry += " and ItemType='Product' "
            ElseIf rbtn_IceCream.IsChecked Then
                qry += " and ItemType='IceCream' "
            End If
            Dim DocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(DocNo) > 0 Then
                LoadData(DocNo, NavigatorType.Current)
            Else
                txtDate.Enabled = False
                If SeparateDemandMilkandProduct Then
                    qry = " select  top 1 x.ItemType 
                from(
                select TSPL_DISTRIBUTOR_ROUTE.Code as Code,TSPL_DISTRIBUTOR_ROUTE.Start_Date,TSPL_DISTRIBUTOR_ROUTE.Remarks,max(TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code) as cust_code,TSPL_DISTRIBUTOR_ROUTE.ItemType
                from TSPL_DISTRIBUTOR_ROUTE
                left join TSPL_DISTRIBUTOR_ROUTE_CUSTOMER on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
                where  TSPL_DISTRIBUTOR_ROUTE.Status=1 and IS_Transpoter=0 and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No='" + txtRouteNo.Value + "' and TSPL_DISTRIBUTOR_ROUTE.ItemType='" + ItemType + "'
                 Group by TSPL_DISTRIBUTOR_ROUTE.Code,TSPL_DISTRIBUTOR_ROUTE.Start_Date,TSPL_DISTRIBUTOR_ROUTE.Remarks,TSPL_DISTRIBUTOR_ROUTE.ItemType
                ) X "
                    Dim DRTItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(DRTItem) > 0 Then
                        If clsCommon.CompairString(DRTItem, "P") = CompairStringResult.Equal Then
                            rbtn_Product.IsChecked = True
                            'RadGroupBox1.Enabled = False
                            LoadBlankGrid()
                            'HideUnhideRowsAndColumnsOFGrid()
                        ElseIf clsCommon.CompairString(DRTItem, "I") = CompairStringResult.Equal Then
                            rbtn_IceCream.IsChecked = True
                            'RadGroupBox1.Enabled = False
                            LoadBlankGrid()
                            'HideUnhideRowsAndColumnsOFGrid()
                        End If
                    Else
                        Throw New Exception("Distributor Route Not Tagged!")
                    End If
                End If
                If EnableLocation Then
                    txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Route_Master where Route_No='" + txtRouteNo.Value + "' "))
                    txtLocation.Enabled = False
                Else
                    txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
                End If
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                End If
                If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 Then
                    setRouteVehicleCityDetail()
                End If
            End If
            SetRouteColumns()
            RefreshFormName()
            If DisableRouteandVehicle Then
                txtRouteNo.Enabled = False
                txtVehicleNo.Enabled = False
            Else
                txtRouteNo.Enabled = True
                txtVehicleNo.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetRouteColumns()
    End Sub
    Private Sub TxtCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCity._MYValidating
    End Sub
    Private Sub txtVehicleNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleNo._MYValidating
        Try
            Dim whrcls As String = ""
            Dim qry As String = "Select vehicle_id,Description ,route_no as 'Route No',route_desc as 'Route Name'  from TSPL_VEHICLE_MASTER left join tspl_route_master on tspl_route_master.vehicle_code=TSPL_VEHICLE_MASTER.vehicle_id "
            If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 Then
                whrcls = " tspl_route_master.route_no ='" & txtRouteNo.Value & "' "
            End If
            txtVehicleNo.Value = clsCommon.ShowSelectForm("DBookingVehicle", qry, "vehicle_id", whrcls, txtVehicleNo.Value, "vehicle_id", isButtonClicked)
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleNo.Value) + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub txtCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomerNo._MYValidating
        Try
            Dim qry As String = "select Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No"
            qry += " ,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Name],isnull(TSPL_CUSTOMER_MASTER.Customer_Category,'') as [Customer Category],tspl_customer_master.Cust_Group_Code as [Customer Group Code] "
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
            qry += " left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim WhrCls As String = ""
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' and TSPL_CUSTOMER_MASTER.Route_No='" & txtRouteNo.Value & "' "
            Else
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' "
            End If
            '-------richa 17/12/2019 show customer according to custoer permission Ticket No. ---------
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
            End If
            txtCustomerNo.Value = clsCommon.ShowSelectForm("CustomerFnder1", qry, "Code", WhrCls, txtCustomerNo.Value, "Code", isButtonClicked)
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomerNo.Value + "'"))
            If clsCommon.myLen(txtCustomerNo.Value) > 0 Then
                setRouteVehicleCityDetail()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        '' Setting UpdateDemandBeforePost for Save Demand before post 
        If UpdateDemandBeforePost Then
            SaveData()
        End If
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Dim desc As String = Nothing
        Dim IsPost As Boolean = False
        Try
            Dim StrQry As String = ""
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                StrQry = "select IsPosting,IsUpdating,Posted from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrQry)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dt1.Rows(0)("IsPosting")) = 1 OrElse clsCommon.myCdbl(dt1.Rows(0)("IsUpdating")) = 1 OrElse clsCommon.myCdbl(dt1.Rows(0)("Posted")) = 1 Then
                        Throw New Exception("Document in use by another user.")
                    End If
                End If
                StrQry = "Update TSPL_Product_DEMAND_BOOKING_MASTER set IsPosting=1 where Document_No='" + txtDocNo.Value + "' "
                clsDBFuncationality.ExecuteNonQuery(StrQry)
            End If
            'Dim custCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where route_no='" + txtRouteNo.Value + "' and IsDistributor='Y'"))
            StrQry = "select  top 1 x.Cust_Code 
from(
select TSPL_DISTRIBUTOR_ROUTE.Code as Code,TSPL_DISTRIBUTOR_ROUTE.Start_Date,TSPL_DISTRIBUTOR_ROUTE.Remarks,max(TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code) as cust_code
from TSPL_DISTRIBUTOR_ROUTE
left join TSPL_DISTRIBUTOR_ROUTE_CUSTOMER on TSPL_DISTRIBUTOR_ROUTE.Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code
where  TSPL_DISTRIBUTOR_ROUTE.Status=1 and IS_Transpoter=0 and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No='" + txtRouteNo.Value + "'
 Group by TSPL_DISTRIBUTOR_ROUTE.Code,TSPL_DISTRIBUTOR_ROUTE.Start_Date,TSPL_DISTRIBUTOR_ROUTE.Remarks
) X"
            Dim custCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrQry))
            If (myMessages.postConfirm()) Then
                If checkCreditLimit Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("select Credit_Customer from TSPL_CUSTOMER_MASTER where Cust_Code='" + custCode + "'"), "N") = CompairStringResult.Equal Then
                        Dim OsBal As Decimal = GetOutStandingBal(custCode, txtDate.Value)
                        If OsBal > clsCommon.myCdbl(txtPAmt.Text) Then
                            IsPost = True
                        Else
                            IsPost = False
                            If clsCommon.myLen(custCode) > 0 Then
                                Dim count As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where Program_Code='" + clsUserMgtCode.frmDemandBooking + "' and Document_No='" + txtDocNo.Value + "' and Cust_Code='" + custCode + "'")
                                If count = 0 Then
                                    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code,Cust_Code,Loc_Code) " &
                                      "values ('Product Demand Booking','" & clsUserMgtCode.FrmProductDemandBooking & "','" & txtDocNo.Value & "', " &
                                      "'" & clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "', " &
                                      "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
                                      "'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
                                      "'" & objCommonVar.CurrentCompanyCode & "','" & custCode & "','" & txtLocation.Value & "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                    common.clsCommon.MyMessageBoxShow(Me, "Insufficient O/S Balance send for Approval", Me.Text)
                                Else
                                    common.clsCommon.MyMessageBoxShow(Me, "Demand already send for Approval", Me.Text)
                                End If
                            Else
                                If clsCommon.MyMessageBoxShow(Me, "Distributor Not Found! Do You Want to Continue?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                                    IsPost = True
                                Else
                                    IsPost = False
                                End If
                            End If
                        End If
                    Else
                        IsPost = True
                    End If
                Else
                    IsPost = True
                End If
                If IsPost Then
                    If (clsProductDemandBookingSale.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                        msg = "Successfully posted"
                        common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                        'If Not chkIndividualCustomer.Checked Then
                        '    If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandAll, clsFixedParameterCode.ApplyDemandAll, Nothing)) = 1 Or clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                        '        SaveData(1, True)
                        '    End If
                        'End If
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = "Update TSPL_Product_DEMAND_BOOKING_MASTER set IsPosting=0 where Document_No='" + txtDocNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(qry)
            msg = Nothing
            qry = Nothing
            dt = Nothing
            desc = Nothing
        End Try
    End Sub
    Public Function GetOutStandingBal(ByVal VendorNo As String, ByVal dtDoc As DateTime) As Decimal
        Dim OSBal As Decimal = 0
        Try
            Dim strcustomerfilter As String = String.Empty
            strcustomerfilter = "'" + VendorNo + "'"
            'Dim qry = ""
            Dim qry As String = "Select  case when (( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt)) )>=0 then -abs(( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))) else abs(( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))) end  as BalAmt From ( " &
                    "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
                    "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
                    "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
                    "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(dtDoc.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, Nothing, False) & "   " &
                    " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
                    "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(dtDoc, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & VendorNo & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
                    ") XXX GROUP BY ACode ORDER BY ACode"
            Dim dblBal As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing))
            OSBal = dblBal
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return OSBal
    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            DeleteData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                If (clsProductDemandBookingSale.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    Sub setRouteVehicleCityDetail()
        Try
            Dim qry As String = ""
            qry = "select TSPL_ROUTE_MASTER.City_Code,Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,TSPL_CUSTOMER_MASTER.Zone_Code,"
            If rbtn_Product.IsChecked Then
                qry += " TSPL_CUSTOMER_MASTER.P_Route_No as Route_No, "
            ElseIf rbtn_IceCream.IsChecked Then
                qry += "TSPL_CUSTOMER_MASTER.I_Route_No as Route_No,"
            End If
            qry += "Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon,tspl_transport_master.Transporter_Name from TSPL_CUSTOMER_MASTER left outer join " &
                "TSPL_ROUTE_MASTER on "
            If rbtn_Product.IsChecked Then
                qry += "TSPL_CUSTOMER_MASTER.P_Route_No=TSPL_ROUTE_MASTER.Route_No"
            ElseIf rbtn_IceCream.IsChecked Then
                qry += "TSPL_CUSTOMER_MASTER.I_Route_No=TSPL_ROUTE_MASTER.Route_No"
            End If
            qry += " left outer join TSPL_VEHICLE_MASTER on " &
                "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join tspl_transport_master on tspl_transport_master.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id "
            If chkIndividualCustomer.Checked = True Then
                qry += " where TSPL_CUSTOMER_MASTER.Cust_Code ='" & txtCustomerNo.Value & "'"
            Else
                qry += " where TSPL_ROUTE_MASTER.Route_No ='" & txtRouteNo.Value & "'"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                txtVehicleNo.Value = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                lblVehicleNo.Text = clsCommon.myCstr(dt1.Rows(0)("Number"))
                txtRouteNo.Value = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                lblRouteDesc.Text = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
                lblTransporterName.Text = clsCommon.myCstr(dt1.Rows(0)("Transporter_Name"))
                TxtCity.Value = clsCommon.myCstr(dt1.Rows(0)("City_Code"))
                lblCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select City_name from tspl_city_master where city_code=  '" & TxtCity.Value & "' "))
                If chkIndividualCustomer.Checked = True Then
                    If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) <= 0 Then
                        Throw New Exception("Please Map Route with Customer " & lblCustomerName.Text & "")
                    End If
                End If
                If clsCommon.myLen(clsCommon.myCstr(TxtCity.Value)) <= 0 Then
                    Throw New Exception("Please Map city with Route " & lblRouteDesc.Text & "")
                End If
                If clsCommon.myLen(clsCommon.myCstr(txtVehicleNo.Value)) <= 0 Then
                    Throw New Exception("Please Map Vehicle with Route " & lblRouteDesc.Text & "")
                End If
                If EnableLocation Then
                    txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Route_Master where Route_No='" + txtRouteNo.Value + "' "))
                    txtLocation.Enabled = False
                Else
                    txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
                End If
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                End If
                If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(TxtCity.Value)) > 0 Then
                    setCustomerDetail(TxtCity.Value, txtRouteNo.Value, False)
                End If
            End If
            RefreshFormName()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub setCustomerDetail(ByVal strCityCode As String, ByVal strtRouteCode As String, ByVal isLoadData As Boolean)
        Try
            Dim MainQry As String = ""
            Dim qry As String = ""
            qry = "select cust_code,Customer_name,display_seq from TSPL_CUSTOMER_MASTER where 2=2 and  TSPL_CUSTOMER_MASTER.Status='N'  "
            If rbtn_Product.IsChecked Then
                qry += " and P_route_no ='" + strtRouteCode + "' "
            ElseIf rbtn_IceCream.IsChecked Then
                qry += " and I_route_no ='" + strtRouteCode + "' "
            Else
                Throw New Exception("No Customers found for Selected Route and City")
            End If
            If chkIndividualCustomer.Checked = True Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code ='" & txtCustomerNo.Value & "'"
            End If
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                qry += "  and IsDistributor='N'"
            End If
            ' qry += " order by isnull(TSPL_CUSTOMER_MASTER.display_seq,0)  "
            If isLoadData Then
                qry += "union 
select TSPL_Product_DEMAND_BOOKING_DETAIL.Cust_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name,TSPL_CUSTOMER_MASTER.display_seq from TSPL_Product_DEMAND_BOOKING_MASTER 
left join TSPL_Product_DEMAND_BOOKING_DETAIL on TSPL_Product_DEMAND_BOOKING_MASTER.Document_No=TSPL_Product_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_Product_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
where TSPL_Product_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "'
group by TSPL_Product_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.display_seq"
                MainQry += "select X.* from (" + qry + " )X order by isnull(X.display_seq,0)"
            Else
                MainQry = qry + " order by isnull(TSPL_CUSTOMER_MASTER.display_seq,0) "
            End If
            If Not SeparateDemandMilkandProduct Then
                LoadBlankGrid()
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(MainQry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                Dim i As Integer = 1
                For Each dr As DataRow In dt1.Rows
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colbtncol).Value = "Reset "
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("cust_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_name"))
                    i = i + 1
                    gv1.Rows.AddNew()
                Next
            Else
                Throw New Exception("No Customers found for Selected Route and City")
            End If
            gv1.Columns(colLineNo).IsPinned = True
            gv1.Columns(colCustCode).IsPinned = True
            gv1.Columns(colCustName).IsPinned = True
            'gv1.Columns(colShiftName).IsPinned = True
            gv1.Columns(colItemExist).IsPinned = True
            gv1.Columns(colIsItemUpdate).IsPinned = True
            gv1.Columns(colBookingCreatedFor3Days).IsPinned = True
            'gv1.Columns(colAmt).IsPinned = True
            'gv1.Columns(colCrate).IsPinned = True
            'gv1.Columns(colLitre).IsPinned = True
            'gv1.Columns(colMAmt).IsPinned = True
            gv1.Columns(colPCount).IsPinned = True
            gv1.Columns(colPAmt).IsPinned = True
            gv1.Columns(colLineNo).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colCustCode).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colCustName).PinPosition = PinnedColumnPosition.Left
            'gv1.Columns(colShiftName).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colItemExist).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colIsItemUpdate).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colBookingCreatedFor3Days).PinPosition = PinnedColumnPosition.Left
            'gv1.Columns(colAmt).PinPosition = PinnedColumnPosition.Right
            'gv1.Columns(colCrate).PinPosition = PinnedColumnPosition.Right
            'gv1.Columns(colLitre).PinPosition = PinnedColumnPosition.Right
            'gv1.Columns(colMAmt).PinPosition = PinnedColumnPosition.Right
            gv1.Columns(colPCount).PinPosition = PinnedColumnPosition.Right
            gv1.Columns(colPAmt).PinPosition = PinnedColumnPosition.Right
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            gv1.BestFitColumns()
            'MergeHorizontally(gv1, 1, gv1.Rows.Count - 1)
            If gv1.Rows.Count > 0 Then
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(7)
            End If
            'MergeVertically(gv1, New Integer() {1, 2})
            'View()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub RefreshFormName()
        Me.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when len(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as FormName from tspl_Program_master where program_code='" + clsUserMgtCode.frmDemandBooking + "'"))
        If clsCommon.myLen(txtRouteNo.Value) > 0 Then
            Me.Text += " - " + txtRouteNo.Value + ""
        End If
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub CreateTable()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("Document_No", "varchar(30) NOT NULL Primary key")
            coll.Add("Document_Date", "datetime not NULL")
            coll.Add("Route_No", "varchar(12) NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
            coll.Add("Location_Code", "Varchar(12) NULL  References TSPL_LOCATION_MASTER(Location_Code) ")
            coll.Add("City_Code", "Varchar(50) NULL REFERENCES TSPL_CITY_MASTER(CITY_CODE)")
            'coll.Add("ShiftType", "Varchar(20) null")
            coll.Add("ItemType", "Varchar(20) null")
            'coll.Add("TripNo", "Varchar(50) null")
            coll.Add("Posted", "integer not null default 0")
            coll.Add("Comp_Code", "varchar(8) NULL")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")
            coll.Add("Modified_By", "varchar(12) NOT NULL")
            coll.Add("Modified_Date", "Datetime NOT NULL")
            coll.Add("IsIndividualCustomer", "int not null default 0")
            'coll.Add("TotalQtyInCrates", "decimal(18,2) null")
            'coll.Add("TotalQtyInLtr", "decimal(18,2) null")
            coll.Add("DocumentAmount", "decimal(18,2) null")
            coll.Add("Posting_Date", "Datetime NULL")
            'coll.Add("Posted_Morning", "integer null")
            'coll.Add("Posted_Morning_By", "varchar(12) NULL")
            'coll.Add("Posted_Morning_Date", "Datetime NULL")
            'coll.Add("Posted_Evening", "integer null")
            'coll.Add("Posted_Evening_By", "varchar(12) NULL")
            'coll.Add("Posted_Evening_Date", "Datetime NULL")
            'coll.Add("UploderDocNo", "Varchar(30) null references TSPL_DEMAND_UPLOADER(Document_No)")
            coll.Add("IsUpdating", "integer null")
            coll.Add("IsPosting", "integer null")
            coll.Add("Send_By", "varchar(12)  NULL")
            coll.Add("Send_Date", "datetime  NULL")
            coll.Add("FILE_INFO", "bigint NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", coll, "", True, False, "", "Document_No", "Document_Date", True)
            coll = New Dictionary(Of String, String)()
            coll.Add("TR_Code", "varchar(30) NOT NULL primary Key")
            coll.Add("Document_No", "varchar(30) NOT NULL REFERENCES TSPL_PRODUCT_DEMAND_BOOKING_MASTER(Document_No)")
            coll.Add("Line_No", "integer not null default 0")
            coll.Add("Cust_Code", "Varchar(12) null references TSPL_CUSTOMER_MASTER(Cust_Code)")
            coll.Add("Item_Code", "Varchar(50) null references TSPL_Item_MASTER(Item_Code)")
            coll.Add("Qty", "decimal(18,2) null")
            coll.Add("Unit_code", "Varchar(12) null")
            coll.Add("Vehicle_Code", "Varchar(12) null")
            coll.Add("Item_Rate", "decimal(18,2) not null default 0")
            coll.Add("Price_code", "varchar(12) NULL")
            'coll.Add("ShiftType", "varchar(20) NULL")
            coll.Add("IsItemUpdate", "int not null default 0")
            coll.Add("TotalCrates_ItemWise", "decimal(18,2) null")
            coll.Add("TotalLtr_ItemWise", "decimal(18,2) null")
            coll.Add("ItemNetAmount", "decimal(18,2) null")
            'coll.Add("IsGatePassGenerated", "char(1) not null default 'N'")
            'coll.Add("IsTruckSheetGenerated", "char(1) not null default 'N'")
            'coll.Add("Production_Remarks", "varchar(200) NULL")
            'coll.Add("GPCode", "varchar(30) NULL")
            'coll.Add("Is_Posted", "char(1) not null default 'N'")
            'coll.Add("Trip_No", "integer null")
            coll.Add("TAX_Group", "varchar(12) NULL")
            coll.Add("TAX1", "varchar(12) NULL")
            coll.Add("TAX1_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX1_Rate", "decimal(18, 4) NULL")
            coll.Add("TAX1_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX2", "varchar(12) NULL")
            coll.Add("TAX2_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX2_Rate", "decimal(18, 4) NULL")
            coll.Add("TAX2_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX3", "varchar(12) NULL")
            coll.Add("TAX3_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX3_Rate", "decimal(18, 4) NULL")
            coll.Add("TAX3_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX4", "varchar(12) NULL")
            coll.Add("TAX4_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX4_Rate", "decimal(18, 4) NULL")
            coll.Add("TAX4_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX5", "varchar(12) NULL")
            coll.Add("TAX5_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX5_Rate", "decimal(18, 4) NULL")
            coll.Add("TAX5_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX6", "varchar(12) NULL")
            coll.Add("TAX6_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX6_Rate", "decimal(18, 2) NULL")
            coll.Add("TAX6_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX7", "varchar(12) NULL")
            coll.Add("TAX7_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX7_Rate", "decimal(18, 2) NULL")
            coll.Add("TAX7_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX8", "varchar(12) NULL")
            coll.Add("TAX8_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX8_Rate", "decimal(18, 2) NULL")
            coll.Add("TAX8_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX9", "varchar(12) NULL")
            coll.Add("TAX9_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX9_Rate", "decimal(18, 2) NULL")
            coll.Add("TAX9_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX10", "varchar(12) NULL")
            coll.Add("TAX10_Base_Amt", "decimal(18, 2) NULL")
            coll.Add("TAX10_Rate", "decimal(18, 2) NULL")
            coll.Add("TAX10_Amt", "decimal(18, 2) NULL")
            coll.Add("Created_By", "varchar(30) NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PRODUCT_DEMAND_BOOKING_DETAIL", coll, "", True, False, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "Document_No", "", True)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            If intCurrRow = gv1.Rows.Count - 1 Then
                intCurrRow = 0
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            Else
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
            End If
            If gv1.CurrentColumn Is gv1.Columns(gv1.Columns.Count - 5) Then
            End If
        End If
    End Sub
    Private Sub setGridFocusHome()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            'gv1.CurrentColumn = gv1.Columns(7)
            gv1.Rows(intCurrRow).Cells(7).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(7).IsCurrent = True
        End If
    End Sub
    Private Sub setGridFocusEnd()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            gv1.Rows(intCurrRow).Cells(gv1.Columns.Count - 5).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(gv1.Columns.Count - 5).IsCurrent = True
        End If
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Index >= 7 And e.Column.Name <> colPCount And e.Column.Name <> colPCount Then
                        'If isLoadData = False AndAlso (clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0) Then
                        If Not isLoadData Then
                            ''UpdateItemQtyAfterSave(gv1.CurrentRow.Index, gv1.CurrentColumn.Index)
                            'UpdateAllTotals(False)
                            UpdateRowTotal()
                            'HideUnhideRowsAndColumnsOFGrid()
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            'Finally
            '    isInsideLoadData = False
            '    isCellValueChangedOpen = False
        End Try
    End Sub
    Private Sub UpdateAllTotals(ByVal isLoad As Boolean)
        Try
            isInsideLoadData = True
            Dim TotalCrate As Double = 0
            Dim TotalLitre As Double = 0
            Dim TotalPCount As Double = 0
            Dim TotalPAmt As Double = 0
            Dim TotalMAmt As Double = 0
            Dim dblTotalDocAmtRowWise As Decimal = 0
            Dim dblTotalCrateRowWise As Double = 0
            Dim dblTotalLitreRowWise As Double = 0
            Dim strPriceCode As String = String.Empty
            Dim strItemValueExist As String = String.Empty
            Dim strItemUpdateAfterSave As String = String.Empty
            Dim dblDocTotalCrate As Double = 0
            Dim dblDocTotalLitre As Double = 0
            Dim dblDocTotalAmt As Double = 0
            Dim dblTotalPCount As Double = 0
            Dim dblToalPouchCount As Double = 0
            Dim dblTotalPAmt As Double = 0
            Dim dblTotalMAmt As Double = 0
            Dim colTotalQty As Double = 0
            For dblrows As Integer = 0 To gv1.Rows.Count - 2
                Dim k As Integer = 1
                TotalCrate = 0
                dblTotalCrateRowWise = 0
                TotalLitre = 0
                dblTotalLitreRowWise = 0
                dblTotalDocAmtRowWise = 0
                dblTotalPCount = 0
                dblTotalPAmt = 0
                dblTotalMAmt = 0
                strItemValueExist = "No"
                strItemUpdateAfterSave = "No"
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 5
                    Dim obj1 As ItemValueClass = Nothing
                    Try
                        obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    Catch ex As Exception
                    End Try
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                            'If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                            '    If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                            '        TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '        obj1.FreshItem_QtyInCrates = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '        'gv1.Rows(gv1.Rows.Count - 1).Cells(dblcolumns).Value = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '    Else
                            '        Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                            '        If ItemCrateType = 1 Then
                            '            Dim IsStockingUnit As String = obj1.Stocking_Unit 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                            '            Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                            '            Dim ItemConvFactor As Double = obj1.Conversion_Factor 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                            '            If Not clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                            '                If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                            '                    Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                            '                    If ConvertPouchtoCrate Then
                            '                        If DispatchQty > (CrateConvFactor / 2) Then
                            '                            dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                            '                        Else
                            '                            dblTotalCrateRowWise = 1
                            '                        End If
                            '                    Else
                            '                        If DispatchQty > (CrateConvFactor / 2) Then
                            '                            dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                            '                        Else
                            '                            dblTotalCrateRowWise = 0
                            '                        End If
                            '                    End If
                            '                End If
                            '            End If
                            '            TotalCrate = TotalCrate + dblTotalCrateRowWise
                            '            obj1.FreshItem_QtyInCrates = dblTotalCrateRowWise
                            '        End If
                            '    End If
                            '    ''to convert into litre
                            '    Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                            '    Dim ItemConvFactor_Ltr As Double = obj1.Conversion_Factor 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                            '    If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                            '        Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor_Ltr
                            '        dblTotalLitreRowWise = (DispatchQty / CrateConvFactor_Ltr)
                            '    End If
                            '    TotalLitre = TotalLitre + dblTotalLitreRowWise
                            '    obj1.FreshItem_QtyInLitres = dblTotalLitreRowWise
                            '    ''---------end of litre conversion
                            'Else
                            dblTotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            'End If
                            strItemValueExist = "Yes"
                            Dim dt As New DataTable()
                            Dim dblRate As Double = 0
                            Dim qry As String = "select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) & "'"
                            strPriceCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.myLen(strPriceCode) <= 0 Then
                                Throw New Exception("price_CodeNon not found for Customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & "")
                            End If
                            If Not isLoad Then
                                qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, XXXE.Tax_group,XXXE.TAX1_Rate, " &
                        " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                        "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                        " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                        " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                        " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                        " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt, XXXE.TAX5_Amt,XXXE.TAX6_Amt,XXXE.TAX7_Amt,XXXE.TAX8_Amt,XXXE.TAX9_Amt,XXXE.TAX10_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                        "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                        "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                        "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                        " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,   TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                        "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                        "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & obj1.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & obj1.itemCode & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                        ") XXXE WHERE RowNo=1  "
                            Else
                                qry = "select item_Rate as Item_Basic_Price,tax_group,TAX1,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7,TAX8,TAX9,TAX10,TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate, TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate,TAX1_Amt,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt from TSPL_Product_DEMAND_BOOKING_DETAIL where  Document_No='" + txtDocNo.Value + "' and Item_Code='" + obj1.itemCode + "' and Unit_code='" + obj1.Unit_code + "' and Cust_Code='" & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) & "'"
                            End If
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                Dim objCustItem As clsDemandCustItem = New clsDemandCustItem()
                                dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                If dblRate = 0 Then
                                    Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(obj1.ShortDesc) & Environment.NewLine)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                    'obj1.ItemRate = dblRate
                                    dblTotalMAmt = dblTotalMAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                Else
                                    'obj1.ItemRate = dblRate
                                    dblTotalPAmt = dblTotalPAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                End If
                                objCustItem.ItemRate = dblRate
                                'obj1.ItemTotAmt = Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                objCustItem.Cust_Code = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)
                                objCustItem.FreshItem_QtyInLitres = obj1.FreshItem_QtyInLitres
                                objCustItem.FreshItem_QtyInCrate = obj1.FreshItem_QtyInCrates
                                objCustItem.ItemCode = obj1.itemCode
                                objCustItem.UnitCode = obj1.Unit_code
                                objCustItem.ItemTotAmt = Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                objCustItem.TAX_Group = clsCommon.myCstr(dt.Rows(0).Item("TAX_Group"))
                                dblTotalDocAmtRowWise = dblTotalDocAmtRowWise + objCustItem.ItemTotAmt
                                objCustItem.TAX1 = clsCommon.myCstr(dt.Rows(0).Item("TAX1"))
                                If clsCommon.CompairString(objCustItem.TAX1, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX1_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Rate"))
                                End If
                                objCustItem.TAX1_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX1_Rate / 100), 2)
                                objCustItem.TAX1_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX2 = clsCommon.myCstr(dt.Rows(0).Item("TAX2"))
                                If clsCommon.CompairString(objCustItem.TAX2, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX2_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Rate"))
                                End If
                                objCustItem.TAX2_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX2_Rate / 100), 2)
                                objCustItem.TAX2_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX3 = clsCommon.myCstr(dt.Rows(0).Item("TAX3"))
                                If clsCommon.CompairString(objCustItem.TAX3, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX3_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Rate"))
                                End If
                                objCustItem.TAX3_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX3_Rate / 100), 2)
                                objCustItem.TAX3_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX4 = clsCommon.myCstr(dt.Rows(0).Item("TAX4"))
                                If clsCommon.CompairString(objCustItem.TAX4, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX4_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Rate"))
                                End If
                                objCustItem.TAX4_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX4_Rate / 100), 2)
                                objCustItem.TAX4_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX5 = clsCommon.myCstr(dt.Rows(0).Item("TAX5"))
                                If clsCommon.CompairString(objCustItem.TAX5, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX5_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX5_Rate"))
                                End If
                                objCustItem.TAX5_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX5_Rate / 100), 2)
                                objCustItem.TAX5_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX6 = clsCommon.myCstr(dt.Rows(0).Item("TAX6"))
                                If clsCommon.CompairString(objCustItem.TAX6, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX6_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX6_Rate"))
                                End If
                                objCustItem.TAX6_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX6_Rate / 100), 2)
                                objCustItem.TAX6_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX7 = clsCommon.myCstr(dt.Rows(0).Item("TAX7"))
                                If clsCommon.CompairString(objCustItem.TAX7, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX7_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX7_Rate"))
                                End If
                                objCustItem.TAX7_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX7_Rate / 100), 2)
                                objCustItem.TAX7_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX8 = clsCommon.myCstr(dt.Rows(0).Item("TAX8"))
                                If clsCommon.CompairString(objCustItem.TAX8, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX8_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX8_Rate"))
                                End If
                                objCustItem.TAX8_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX8_Rate / 100), 2)
                                objCustItem.TAX8_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX9 = clsCommon.myCstr(dt.Rows(0).Item("TAX9"))
                                If clsCommon.CompairString(objCustItem.TAX9, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX9_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX9_Rate"))
                                End If
                                objCustItem.TAX9_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX9_Rate / 100), 2)
                                objCustItem.TAX9_Base_Amt = objCustItem.ItemTotAmt
                                objCustItem.TAX10 = clsCommon.myCstr(dt.Rows(0).Item("TAX10"))
                                If clsCommon.CompairString(objCustItem.TAX10, "TCS") = CompairStringResult.Equal Then
                                    objCustItem.TAX10_Rate = CalculateTCS(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value))
                                Else
                                    objCustItem.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX10_Rate"))
                                End If
                                objCustItem.TAX10_Amt = Math.Round(objCustItem.ItemTotAmt * (objCustItem.TAX10_Rate / 100), 2)
                                objCustItem.TAX10_Base_Amt = objCustItem.ItemTotAmt
                                lstCustItem.Add(objCustItem)
                            Else
                                gv1.Rows(dblrows).Cells(dblcolumns).Value = 0
                                Throw New Exception("Please create Price for Item " & obj1.ShortDesc & " and customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & " ")
                            End If
                        End If
                    End If
                Next
                'gv1.Rows(dblrows).Cells(colCrate).Value = Math.Round(clsCommon.myCdbl(TotalCrate), 2)
                'gv1.Rows(dblrows).Cells(colLitre).Value = Math.Round(clsCommon.myCdbl(TotalLitre), 2)
                'gv1.Rows(dblrows).Cells(colMAmt).Value = clsCommon.myCdbl(dblTotalMAmt)
                gv1.Rows(dblrows).Cells(colPCount).Value = clsCommon.myCdbl(dblTotalPCount)
                gv1.Rows(dblrows).Cells(colPAmt).Value = clsCommon.myCdbl(dblTotalPAmt)
                'gv1.Rows(dblrows).Cells(colAmt).Value = clsCommon.myCdbl(dblTotalDocAmtRowWise)
                If clsCommon.myLen(gv1.Rows(dblrows).Cells(colItemExist)) > 0 Then
                    gv1.Rows(dblrows).Cells(colItemExist).Value = strItemValueExist
                End If
                'dblDocTotalAmt = dblDocTotalAmt + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colAmt).Value)
                'dblDocTotalLitre = dblDocTotalLitre + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                'dblDocTotalCrate = dblDocTotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                TotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                TotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                'TotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
            Next
            'lblTotalCrate.Text = clsCommon.myCdbl(dblDocTotalCrate)
            'lblTotalLitre.Text = clsCommon.myCdbl(dblDocTotalLitre)
            'txtDocAmt.Text = clsCommon.myCdbl(dblDocTotalAmt)
            'lblDocumentAmt.Text = clsCommon.myCdbl(TotalMAmt)
            txtPCount.Text = clsCommon.myCdbl(TotalPCount)
            txtPAmt.Text = clsCommon.myCdbl(TotalPAmt)
            UpdateColumnTotal()
            gv1.Rows(gv1.Rows.Count - 1).IsPinned = True
            gv1.Rows(gv1.Rows.Count - 1).PinPosition = PinnedRowPosition.Bottom
            isInsideLoadData = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function CalculateTCS(ByVal CustCode As String) As Double
        Dim TCSBaseAmount As Double = 0
        Dim TCSTaxRate As Double = 0
        Dim balanceAmt As Double = 0
        Dim OPInvoice_Sale_Amt As Double = 0
        Dim CurrFinYR As String = String.Empty
        Dim FinancialYear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN DatePart(Month, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "') >= 4 THEN DatePart(Year, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "') + 1 ELSE DatePart(Year, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "') END AS Fiscal_Year"))
        Dim strStartDate As Date = "01/Apr/" + clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1))
        Dim strEndDate As Date = "31/Mar/" + FinancialYear
        CurrFinYR = clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1)) + "-" + FinancialYear
        TCSBaseAmount = 0
        Dim strqry As String = "select sum(ItemNetAmount) from TSPL_Product_DEMAND_BOOKING_MASTER left join TSPL_Product_DEMAND_BOOKING_DETAIL on TSPL_Product_DEMAND_BOOKING_MASTER.Document_No=TSPL_Product_DEMAND_BOOKING_DETAIL.Document_No where TSPL_Product_DEMAND_BOOKING_DETAIL.Cust_Code='" + clsCommon.myCstr(CustCode) + "' and convert(date, TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(strStartDate) + "' and convert(date, TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(strEndDate) + "' "
        balanceAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
        OPInvoice_Sale_Amt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Sale_amt from TSPL_OP_INVOICE_FOR_TCS where Customer_Code='" + clsCommon.myCstr(CustCode) + "' and Financial_Year_Code='" + clsCommon.myCstr(CurrFinYR) + "'"))
        TCSBaseAmount = OPInvoice_Sale_Amt + balanceAmt
        If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
            If TCSBaseAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing)) = 1 Then
                    Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + clsCommon.myCstr(CustCode) + "'")) = 1, True, False)
                    If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                    Else
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                    End If
                Else
                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" + clsCommon.myCstr(CustCode) + "'"))
                    If clsCommon.myLen(panno) > 0 Then
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                    Else
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                    End If
                End If
            Else
                TCSTaxRate = 0
            End If
        Else
            TCSTaxRate = 0
        End If
        Return TCSTaxRate
    End Function
    Private Sub UpdateRowTotal()
        Try
            Dim TotalCrate As Double = 0
            Dim TotalLitre As Double = 0
            Dim TotalPCount As Double = 0
            Dim TotalPAmt As Double = 0
            Dim TotalMAmt As Double = 0
            Dim dblTotalDocAmtRowWise As Decimal = 0
            Dim dblTotalCrateRowWise As Double = 0
            Dim dblTotalLitreRowWise As Double = 0
            Dim strPriceCode As String = String.Empty
            Dim strItemValueExist As String = String.Empty
            Dim strItemUpdateAfterSave As String = String.Empty
            Dim dblDocTotalCrate As Double = 0
            Dim dblDocTotalLitre As Double = 0
            Dim dblDocTotalAmt As Double = 0
            Dim dblTotalPCount As Double = 0
            Dim dblToalPouchCount As Double = 0
            Dim dblTotalPAmt As Double = 0
            Dim dblTotalMAmt As Double = 0
            Dim colTotalQty As Double = 0
            For dblrows As Integer = 0 To gv1.Rows.Count - 2
                Dim k As Integer = 1
                TotalCrate = 0
                dblTotalCrateRowWise = 0
                TotalLitre = 0
                dblTotalLitreRowWise = 0
                dblTotalDocAmtRowWise = 0
                dblTotalPCount = 0
                dblTotalPAmt = 0
                dblTotalMAmt = 0
                strItemValueExist = "No"
                strItemUpdateAfterSave = "No"
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 5
                    Dim obj1 As ItemValueClass = Nothing
                    Try
                        obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    Catch ex As Exception
                    End Try
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                            'If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                            '    If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                            '        Dim cellValue As String = clsCommon.myCstr(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                            '            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(AllowEntryInDecimal) from TSPL_ITEM_MASTER where Item_Code='" + obj1.itemCode + "' and  AllowEntryInDecimal=1")) = 0 Then
                            '                If cellValue.Contains(".") OrElse cellValue.Contains(",") Then
                            '                    gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                            '                    Throw New Exception("Decimal values are not allowed.")
                            '                End If
                            '            Else
                            '                If clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) Mod 0.5 <> 0 Then
                            '                    gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                            '                    Throw New Exception("Should be in multiple of 0.5")
                            '                End If
                            '            End If
                            '        End If
                            '        TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '        obj1.FreshItem_QtyInCrates = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '        'gv1.Rows(gv1.Rows.Count - 1).Cells(dblcolumns).Value = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            '    Else
                            '        Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                            '        If ItemCrateType = 1 Then
                            '            Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                            '            Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                            '            Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                            '            If Not clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                            '                If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                            '                    Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                            '                    If ConvertPouchtoCrate Then
                            '                        If DispatchQty > (CrateConvFactor / 2) Then
                            '                            dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                            '                        Else
                            '                            dblTotalCrateRowWise = 1
                            '                        End If
                            '                    ElseIf DontCreateForPouch Then
                            '                        dblTotalCrateRowWise = Math.Floor(DispatchQty / CrateConvFactor)
                            '                    Else
                            '                        If DispatchQty > (CrateConvFactor / 2) Then
                            '                            dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                            '                        Else
                            '                            dblTotalCrateRowWise = 0
                            '                        End If
                            '                    End If
                            '                End If
                            '            End If
                            '            TotalCrate = TotalCrate + dblTotalCrateRowWise
                            '            obj1.FreshItem_QtyInCrates = dblTotalCrateRowWise
                            '        End If
                            '    End If
                            '    ''to convert into litre
                            '    Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                            '    Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                            '    If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                            '        Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor_Ltr
                            '        dblTotalLitreRowWise = (DispatchQty / CrateConvFactor_Ltr)
                            '    End If
                            '    TotalLitre = TotalLitre + dblTotalLitreRowWise
                            '    obj1.FreshItem_QtyInLitres = dblTotalLitreRowWise
                            '    ''---------end of litre conversion
                            'Else
                            dblTotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            'End If
                        End If
                    End If
                Next
                'gv1.Rows(dblrows).Cells(colCrate).Value = Math.Round(clsCommon.myCdbl(TotalCrate), 2)
                'gv1.Rows(dblrows).Cells(colLitre).Value = Math.Round(clsCommon.myCdbl(TotalLitre), 2)
                gv1.Rows(dblrows).Cells(colPCount).Value = clsCommon.myCdbl(dblTotalPCount)
                If clsCommon.myLen(gv1.Rows(dblrows).Cells(colItemExist)) > 0 Then
                    gv1.Rows(dblrows).Cells(colItemExist).Value = strItemValueExist
                End If
                'dblDocTotalAmt = dblDocTotalAmt + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colAmt).Value)
                'dblDocTotalLitre = dblDocTotalLitre + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                'dblDocTotalCrate = dblDocTotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                TotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                TotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                'TotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
            Next
            UpdateColumnTotal()
            gv1.Rows(gv1.Rows.Count - 1).IsPinned = True
            gv1.Rows(gv1.Rows.Count - 1).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateColumnTotal()
        Try
            Dim TotalQty As Double = 0
            'For dbrows1 As Integer = 0 To gv1.Rows.Count - 1
            For dblcolumns As Integer = 7 To gv1.Columns.Count - 1
                TotalQty = 0
                For dbrows As Integer = 0 To gv1.Rows.Count - 2
                    TotalQty += clsCommon.myCdbl(gv1.Rows(dbrows).Cells(dblcolumns).Value)
                Next
                gv1.Rows(gv1.Rows.Count - 1).Cells(dblcolumns).Value = TotalQty
            Next
            'Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub HideUnhideRowsAndColumnsOFGrid()
        Try
            isLoadData = True
            Dim k As Integer = 1
            For dblcolumns As Integer = 7 To gv1.Columns.Count - 5
                Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                If obj1 IsNot Nothing Then
                    If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then
                        If rbtn_Product.IsChecked Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Product") = CompairStringResult.Equal Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                'gv1.Columns(colMAmt).IsVisible = True
                                'gv1.Columns(colCrate).IsVisible = True
                                'gv1.Columns(colLitre).IsVisible = True
                            Else
                                'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                gv1.Columns(dblcolumns).IsVisible = False
                                '    gv1.Columns(colPAmt).IsVisible = False
                                '    gv1.Columns(colPCount).IsVisible = False
                            End If
                        ElseIf rbtn_IceCream.IsChecked Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "IceCream") = CompairStringResult.Equal Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(colPAmt).IsVisible = True
                                gv1.Columns(colPCount).IsVisible = True
                            Else
                                'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                gv1.Columns(dblcolumns).IsVisible = False
                                '    gv1.Columns(colMAmt).IsVisible = False
                                '    gv1.Columns(colCrate).IsVisible = False
                                '    gv1.Columns(colLitre).IsVisible = False
                            End If
                        End If
                    End If
                End If
                k = k + 1
            Next
            ' MergeVertically(gv1, New Integer() {1, 2})
            isLoadData = False
            SetRouteColumns()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            isLoadData = False
        End Try
    End Sub
    Private Sub rbtn_Product_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtn_Product.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                'If rbtn_Product.IsChecked Then
                HideUnhideRowsAndColumnsOFGrid()
                'End If
            End If
            'btnPrint.Enabled = True
            txtPCount.Text = ""
            txtPAmt.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                Throw New Exception("Please select Route")
            End If
            If clsCommon.myLen(TxtCity.Value) <= 0 Then
                Throw New Exception("Please select City")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtVehicleNo.Value) <= 0 Then
                Throw New Exception("Please select Vehicle")
            End If
            isInsideLoadData = True
            UpdateAllTotals(False)
            isInsideLoadData = False
            Dim dblQuantityCount As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strItemValueExist As String = ""
                If clsCommon.myLen(gv1.Rows(ii).Cells(colItemExist)) > 0 Then
                    strItemValueExist = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemExist).Value)
                End If
                If clsCommon.CompairString(strItemValueExist, "Yes") = CompairStringResult.Equal Then
                    dblQuantityCount = dblQuantityCount + 1
                End If
            Next
            If dblQuantityCount <= 0 Then
                Throw New Exception("Please enter quantity for atleast one customer")
            End If
            ''If  Then
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "'"))
                If clsCommon.CompairString(txtRouteNo.Value, RouteNo) = CompairStringResult.Equal Then
                    SaveData()
                Else
                    Throw New Exception("You can't change route")
                    txtRouteNo.Value = RouteNo
                End If
            Else
                SaveData()
            End If
            ' SaveData(0, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function SaveData() As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                qry = "select IsPosting,IsUpdating,Posted from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dt1.Rows(0)("IsPosting")) = 1 OrElse clsCommon.myCdbl(dt1.Rows(0)("IsUpdating")) = 1 OrElse clsCommon.myCdbl(dt1.Rows(0)("Posted")) = 1 Then
                        Throw New Exception("Document in used by another user.")
                    End If
                End If
                qry = "Update TSPL_Product_DEMAND_BOOKING_MASTER set IsUpdating=1 where Document_No='" + txtDocNo.Value + "' "
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
            UpdateAllTotals(False)
            blnSaveTotalQTy = True
            'BookingStatus = 0
            Dim strPriceCode As String = String.Empty
            Dim LineNo As Integer = 1
            If (AllowToSave()) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    ResetDemandOnSave(txtDocNo.Value)
                End If
                Dim obj As New clsProductDemandBookingSale()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location_Code = txtLocation.Value
                obj.Route_No = txtRouteNo.Value
                obj.City_Code = TxtCity.Value
                If rbtn_Product.IsChecked = True Then
                    obj.ItemType = "Product"
                ElseIf rbtn_IceCream.IsChecked = True Then
                    obj.ItemType = "IceCream"
                End If
                If chkIndividualCustomer.Checked = True Then
                    obj.IsIndividualCustomer = 1
                Else
                    obj.IsIndividualCustomer = 0
                End If
                obj.DocumentAmount = clsCommon.myCdbl(txtPAmt.Text)
                obj.Arr = New List(Of clsProductDemandBookingSaleDetail)
                ''richa 4 Aug,2021 optimization related
                Dim intLine As Integer = 0
                For dblrows As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colItemExist).Value), "Yes") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) > 0 Then
                        Dim k As Integer = 1
                        Dim isCustRouteNotChanged As Boolean = True
                        For dblcolumns As Integer = 7 To gv1.Columns.Count - 5
                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            k = k + 1
                            If obj1 IsNot Nothing Then
                                If (clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0) AndAlso isCustRouteNotChanged Then
                                    Dim objTr As New clsProductDemandBookingSaleDetail()
                                    objTr.Line_No = LineNo
                                    'objTr.Trip_No = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colTripNo).Value)
                                    objTr.Cust_Code = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)
                                    objTr.Created_By = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCreated_By).Value)
                                    'objTr.ShiftType = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value)
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colIsItemUpdate).Value), "Yes") = CompairStringResult.Equal Then
                                        objTr.IsItemUpdate = 1
                                    Else
                                        objTr.IsItemUpdate = 0
                                    End If
                                    If clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                        If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and Status='N'")) = CompairStringResult.Equal Then
                                            objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                        Else
                                            objTr.Qty = 0
                                        End If
                                        'End If
                                        For Each obj2 As clsDemandCustItem In lstCustItem
                                            If clsCommon.CompairString(objTr.Cust_Code, obj2.Cust_Code) = CompairStringResult.Equal AndAlso (clsCommon.CompairString(obj1.itemCode, obj2.ItemCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj1.Unit_code, obj2.UnitCode) = CompairStringResult.Equal) Then
                                                objTr.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                                objTr.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                                objTr.Rate = clsCommon.myCdbl(obj2.ItemRate)
                                                objTr.ItemNetAmount = clsCommon.myCdbl(obj2.ItemTotAmt)
                                                objTr.TAX_Group = clsCommon.myCstr(obj2.TAX_Group)
                                                objTr.TAX1 = clsCommon.myCstr(obj2.TAX1)
                                                objTr.TAX1_Rate = obj2.TAX1_Rate
                                                objTr.TAX1_Amt = obj2.TAX1_Amt
                                                objTr.TAX1_Base_Amt = clsCommon.myCdbl(obj2.TAX1_Base_Amt)
                                                objTr.TAX2 = clsCommon.myCstr(obj2.TAX2)
                                                objTr.TAX2_Rate = clsCommon.myCdbl(obj2.TAX2_Rate)
                                                objTr.TAX2_Amt = clsCommon.myCdbl(obj2.TAX2_Amt)
                                                objTr.TAX2_Base_Amt = clsCommon.myCdbl(obj2.TAX2_Base_Amt)
                                                objTr.TAX3 = clsCommon.myCstr(obj2.TAX3)
                                                objTr.TAX3_Rate = clsCommon.myCdbl(obj2.TAX3_Rate)
                                                objTr.TAX3_Amt = clsCommon.myCdbl(obj2.TAX3_Amt)
                                                objTr.TAX3_Base_Amt = clsCommon.myCdbl(obj2.TAX3_Base_Amt)
                                                objTr.TAX4 = clsCommon.myCstr(obj2.TAX4)
                                                objTr.TAX4_Rate = clsCommon.myCdbl(obj2.TAX4_Rate)
                                                objTr.TAX4_Amt = clsCommon.myCdbl(obj2.TAX4_Amt)
                                                objTr.TAX4_Base_Amt = clsCommon.myCdbl(obj2.TAX4_Base_Amt)
                                                objTr.TAX5 = clsCommon.myCstr(obj2.TAX5)
                                                objTr.TAX5_Rate = clsCommon.myCdbl(obj2.TAX5_Rate)
                                                objTr.TAX5_Amt = clsCommon.myCdbl(obj2.TAX5_Amt)
                                                objTr.TAX5_Base_Amt = clsCommon.myCdbl(obj2.TAX5_Base_Amt)
                                                objTr.TAX6 = clsCommon.myCstr(obj2.TAX6)
                                                objTr.TAX6_Rate = clsCommon.myCdbl(obj2.TAX6_Rate)
                                                objTr.TAX6_Amt = clsCommon.myCdbl(obj2.TAX6_Amt)
                                                objTr.TAX6_Base_Amt = clsCommon.myCdbl(obj2.TAX6_Base_Amt)
                                                objTr.TAX7 = clsCommon.myCstr(obj2.TAX7)
                                                objTr.TAX7_Rate = clsCommon.myCdbl(obj2.TAX7_Rate)
                                                objTr.TAX7_Amt = clsCommon.myCdbl(obj2.TAX7_Amt)
                                                objTr.TAX7_Base_Amt = clsCommon.myCdbl(obj2.TAX7_Base_Amt)
                                                objTr.TAX8 = clsCommon.myCstr(obj2.TAX8)
                                                objTr.TAX8_Rate = clsCommon.myCdbl(obj2.TAX8_Rate)
                                                objTr.TAX8_Amt = clsCommon.myCdbl(obj2.TAX8_Amt)
                                                objTr.TAX8_Base_Amt = clsCommon.myCdbl(obj2.TAX8_Base_Amt)
                                                objTr.TAX9 = clsCommon.myCstr(obj2.TAX9)
                                                objTr.TAX9_Rate = clsCommon.myCdbl(obj2.TAX9_Rate)
                                                objTr.TAX9_Amt = clsCommon.myCdbl(obj2.TAX9_Amt)
                                                objTr.TAX9_Base_Amt = clsCommon.myCdbl(obj2.TAX9_Base_Amt)
                                                objTr.TAX10 = clsCommon.myCstr(obj2.TAX10)
                                                objTr.TAX10_Rate = clsCommon.myCdbl(obj2.TAX10_Rate)
                                                objTr.TAX10_Amt = clsCommon.myCdbl(obj2.TAX10_Amt)
                                                objTr.TAX10_Base_Amt = clsCommon.myCdbl(obj2.TAX10_Base_Amt)
                                                'If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                                '    If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                                '        objTr.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                '    Else
                                                '        Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                                '        If ItemCrateType = 1 Then
                                                '            objTr.TotalCrates_ItemWise = clsCommon.myCdbl(obj2.FreshItem_QtyInCrate)
                                                '        End If
                                                '    End If
                                                '    objTr.TotalLtr_ItemWise = clsCommon.myCdbl(obj2.FreshItem_QtyInLitres)
                                                'End If
                                            End If
                                        Next
                                        objTr.Vehicle_Code = clsCommon.myCstr(txtVehicleNo.Value)
                                        qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,TSPL_ROUTE_MASTER.Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"
                                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                                        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                            objTr.Price_Code = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
                                            If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Route_No"))) <= 0 Then
                                                Throw New Exception("Plz tag Route for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                            End If
                                            If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))) <= 0 Then
                                                Throw New Exception("Plz tag Vehicle for this route " & clsCommon.myCstr(dt1.Rows(0)("Route_No")) & "")
                                            End If
                                            If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))) <= 0 Then
                                                Throw New Exception("Plz tag price_CodeNon for this Customer " & clsCommon.myCstr(objTr.Cust_Code) & "")
                                            End If
                                        End If
                                        If (clsCommon.myLen(objTr.Cust_Code) > 0) AndAlso (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                            obj.Arr.Add(objTr)
                                        End If
                                        LineNo = LineNo + 1
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) = True Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Dim qry As String = "Update TSPL_Product_DEMAND_BOOKING_MASTER set IsUpdating=0 where Document_No='" + txtDocNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(qry)
        End Try
        Return False
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim isLoadData As Boolean = True
            Dim dblTotalDocAmt As Decimal = 0
            Dim qry As String = ""
            Dim obj As New clsProductDemandBookingSale
            'Dim intRow As Integer
            obj = clsProductDemandBookingSale.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                AddNew()
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                txtDate.Enabled = False
                isNewEntry = False
                RadGroupBox3.Enabled = False
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtLocation.Value = obj.Location_Code
                TxtCity.Value = obj.City_Code
                txtRouteNo.Value = obj.Route_No
                If obj.IsIndividualCustomer = 1 Then
                    chkIndividualCustomer.Checked = True
                    txtCustomerNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT top 1 cust_code  FROM TSPL_Product_DEMAND_BOOKING_DETAIL where document_no='" & txtDocNo.Value & "' "))
                    lblCustomerName.Text = clsCommon.myCstr(clsCustomerMaster.GetName(txtCustomerNo.Value, Nothing))
                Else
                    chkIndividualCustomer.Checked = False
                End If
                chkIndividualCustomer.Enabled = False
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                ElseIf obj.Posted = 2 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Cancel
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                lblRouteDesc.Text = clsRouteMaster.GetName(txtRouteNo.Value, Nothing)
                lblCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select City_name from tspl_city_master where city_code=  '" & TxtCity.Value & "' "))
                'lblTotalCrate.Text = obj.TotalQtyInCrates
                'lblTotalLitre.Text = obj.TotalQtyInLtr
                txtPAmt.Text = obj.DocumentAmount
                If SettSeprateDemandForMorningEveningShift Then
                    RadGroupBox3.Enabled = False
                End If
                If clsCommon.CompairString(obj.ItemType, "Product") = CompairStringResult.Equal Then
                    rbtn_Product.IsChecked = True
                    HideUnhideRowsAndColumnsOFGrid()
                ElseIf clsCommon.CompairString(obj.ItemType, "IceCream") = CompairStringResult.Equal Then
                    rbtn_IceCream.IsChecked = True
                    HideUnhideRowsAndColumnsOFGrid()
                End If
                If Not SeparateDemandMilkandProduct Then
                    'LoadBlankGrid()
                End If
                setCustomerDetail(TxtCity.Value, txtRouteNo.Value, True)
                isLoadData = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsProductDemandBookingSaleDetail In obj.Arr
                        For dblrows As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value), objTr.Cust_Code) = CompairStringResult.Equal Then
                                gv1.Rows(dblrows).Cells(colCreated_By).Value = objTr.Created_By
                                Dim k As Integer = 1
                                For columns = 7 To gv1.Columns.Count - 5
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                        'dblMorningCount = 1
                                    End If
                                Next
                            End If
                        Next
                        'strCustCode = objTr.Cust_Code
                        If isLoadData Then
                            txtVehicleNo.Value = objTr.Vehicle_Code
                            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" & Convert.ToString(txtVehicleNo.Value) & "'"))
                            isLoadData = False
                        End If
                    Next
                End If
                lblTransporterName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select transporter_name from tspl_transport_master where Transport_Id =(select Transport_Id from tspl_vehicle_master where vehicle_id= '" + Convert.ToString(txtVehicleNo.Value) + "')"))
                'HideUnhideRowsOFGrid()
                isLoadData = False
                UpdateAllTotals(True)
                If Not SettSeprateDemandForMorningEveningShift Then
                    HideUnhideRowsAndColumnsOFGrid()
                End If
            End If
            SetRouteColumns()
            RefreshFormName()
            If DisableRouteandVehicle Then
                txtRouteNo.Enabled = False
                txtVehicleNo.Enabled = False
            Else
                txtRouteNo.Enabled = True
                txtVehicleNo.Enabled = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            isLoadData = False
        End Try
    End Sub
    Private Sub ResetDemandOnSave(ByVal DocNo As String)
        Try
            For dblrows As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colItemExist).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) > 0 Then
                    Dim status As Boolean = clsProductDemandBookingSale.DeleteBoothDemand(DocNo, gv1.Rows(dblrows).Cells(colCustCode).Value, False)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim ItemType As String = ""
        If rbtn_Product.IsChecked Then
            ItemType = "Product"
        ElseIf rbtn_IceCream.IsChecked Then
            ItemType = "IceCream"
        End If
        Dim ArrRoute As ArrayList = New ArrayList
        ArrRoute.Add(txtRouteNo.Value)
        clsProductDemandBookingSale.PrintDemandProductData(MyBase.Form_ID, ArrRoute, ItemType, txtDate.Value, UsLock1.Status, False, False, False)
    End Sub

    Private Sub btnAssessment_Click(sender As Object, e As EventArgs) Handles btnAssessment.Click
        Dim frm As New frmAssessmentGrid
        frm.IDate = txtDate.Value
        frm.ShowDialog()
    End Sub
    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim NextDayDocNo As String = ""
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim isDispatch As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_SD_SHIPMENT_BOOKING_DETAIL where Booking_TR_Code in(select TR_Code from TSPL_DEMAND_BOOKING_DETAIL where Document_No='" + txtDocNo.Value + "')"))
                If isDispatch >= 1 Then
                    Throw New Exception("Dispatch already Created!")
                End If
            Else
                Throw New Exception("Please Select Document")
            End If
            If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandAll, clsFixedParameterCode.ApplyDemandAll, Nothing)) = 1 Or clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                NextDayDocNo = clsDBFuncationality.getSingleValue("select Document_No from TSPL_Product_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' and ( CONVERT( date, TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date, 103 )='" + clsCommon.GetPrintDate(txtDate.Value.AddDays(1)) + "') and location_code='" + clsCommon.myCstr(txtLocation.Value) + "'  and IsIndividualCustomer=0 ")
            End If
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document " + IIf(clsCommon.myLen(NextDayDocNo) > 0, "and Delete Next Day Document [" + NextDayDocNo + "]", "") + " " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim qry As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsCommon.myLen(clsCommon.myCstr(NextDayDocNo)) > 0 Then
                    qry = "select Posted from TSPL_Product_Demand_BOOKING_MAstER where Document_No='" + NextDayDocNo + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                        Throw New Exception("Please Reverse/Unpost Document No: [ " + NextDayDocNo + " ]")
                    End If
                    Dim dt As DataTable = Nothing
                    '' to check gatepass or truck sheet generated
                End If
                If clsCommon.myLen(NextDayDocNo) > 0 Then
                    If clsProductDemandBookingSale.DeleteData(NextDayDocNo) Then
                        If clsProductDemandBookingSale.ReverseAndUnpost(txtDocNo.Value) Then
                            saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    If clsProductDemandBookingSale.ReverseAndUnpost(txtDocNo.Value) Then
                        saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            For jj As Integer = 0 To gv1.Rows.Count - 1
                Dim qry As String = ""
                Dim strCustomerDesc As String = ""
                Dim strCustomerCode As String = ""
                ' Dim strCustomer = clsCommon.myCstr(gv1.Rows(jj).Cells(colCustCode).Value)
                Dim strCustomer = clsCommon.myCstr(gv1.Rows(jj).Cells(colCustName).Value).ToLower()
                Dim strCustCode = clsCommon.myCstr(gv1.Rows(jj).Cells(colCustCode).Value).ToLower()
                gv1.ClearSelection()
                If strCustomer.Contains(txtcustomersearch.Text.ToLower()) OrElse strCustCode.Contains(txtcustomersearch.Text.ToLower()) Then
                    gv1.Rows(jj).Cells(colCustCode).IsSelected = True
                    gv1.Rows(jj).IsCurrent = True
                    gv1.Columns(colCustCode).IsCurrent = True
                    gv1.PerformLayout()
                    gv1.Focus()
                    gv1.VerticalScroll.Visible = True
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "TSPL_Product_DEMAND_BOOKING_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintLoadinSlip_Click(sender As Object, e As EventArgs) Handles btnPrintLoadinSlip.Click
        Dim ItemType As String = ""
        If rbtn_Product.IsChecked Then
            ItemType = "Product"
        ElseIf rbtn_IceCream.IsChecked Then
            ItemType = "IceCream"
        End If
        Dim ArrRoute As ArrayList = New ArrayList
        ArrRoute.Add(txtRouteNo.Value)
        clsProductDemandBookingSale.PrintLoadInSlipData(MyBase.Form_ID, ArrRoute, ItemType, txtDate.Value, UsLock1.Status, False)
    End Sub
End Class
