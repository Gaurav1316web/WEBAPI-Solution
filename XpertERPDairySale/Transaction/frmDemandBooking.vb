' '' '' ''Created By richa
Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmDemandBooking
    Inherits FrmMainTranScreen
#Region "Variables"
    Public Shared LockUnlock As Integer = 0
    Dim LockedByUserName As String = ""
    Dim LockedByUserCode As String = ""
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
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colShiftName As String = "colShiftName"
    Const colItemCode As String = "colItemCode"
    Const colCrate As String = "colCrate"
    Const colLitre As String = "colLitre"
    Const colPCount As String = "colPCount"
    Const colPAmt As String = "colPAmt"
    Const colMAmt As String = "colMAmt"
    Const colAmt As String = "COLAMT"
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
#End Region
    Private Sub FrmBookingEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gv1.EnterKeyMode = RadGridViewEnterKeyMode.EnterMovesToNextRow
            blnPageLoad = True
            SettSeprateDemandForMorningEveningShift = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeprateDemandForMorningEveningShift, clsFixedParameterCode.SeprateDemandForMorningEveningShift, Nothing)) = 1)
            ChangeVehicleOnDairySaleBooking = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ChangeVehicleOnDairySaleBooking, clsFixedParameterCode.ChangeVehicleOnDairySaleBooking, Nothing)) = 0, False, True)
            ShowItemLocationWiseonBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemLocationWiseonDairyBooking, clsFixedParameterCode.ShowItemLocationWiseonDairyBooking, Nothing))
            CheckOutstandingOnbooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckOutstandingCreditLimitOnBooking, clsFixedParameterCode.CheckOutstandingCreditLimitOnBooking, Nothing))
            AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
            CalculateTaxRatefromItemwsieTaxOnSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing)) = 1, True, False)
            SingleUserParticularDairyBookingEdit = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SingleUserParticularDairyBookingEdit, clsFixedParameterCode.SingleUserParticularDairyBookingEdit, Nothing)) = 1, True, False)
            DoNotConsiderCustomerCreditLimit = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderCustomerCreditLimit, clsFixedParameterCode.DoNotConsiderCustomerCreditLimit, Nothing)) = 1, True, False)
            UseCutOffTimeonRouteForERP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseCutOffTimeonRouteForERP, clsFixedParameterCode.UseCutOffTimeonRouteForERP, Nothing)) = 1, True, False)
            checkCreditLimit = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckCreditLimit, clsFixedParameterCode.CheckCreditLimit, Nothing)) = 1, True, False)
            AddNew()
            SetUserMgmtNew()
            If clsCommon.myLen(StrDocNo) > 0 Then
                LoadData(StrDocNo, NavigatorType.Current)
            End If
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            End If
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
            blnPageLoad = False
            txtLocation.Enabled = False
            LoadBlankGrid()
            If SettSeprateDemandForMorningEveningShift Then
                rbtnMorningEveningBoth.Enabled = False
                rbtnMorningEveningBoth.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmBookingDairyMultipleCustomer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            'sanjay GKD/20/06/18-000150
            If SingleUserParticularDairyBookingEdit = True AndAlso clsCommon.myLen(txtDocNo.Value) > 0 Then
                If LockUnlock = 1 And LockedByUserCode = objCommonVar.CurrentUserCode Then
                    Dim qry As String = ""
                    qry = "update tspl_booking_matser set User_Lock_For_Edit=0,lockedby_usercode=''  where LockedBy_UserCode='" & objCommonVar.CurrentUserCode & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FrmBookingEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            'setGridFocus()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(0)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.KeyCode = Keys.Enter Then
            setGridFocus()
        ElseIf e.KeyCode = Keys.PageDown Then
            setPagedown()
        ElseIf e.KeyCode = Keys.Home Then
            setGridFocusHome()
        ElseIf e.KeyCode = Keys.End Then
            setGridFocusEnd()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnreverse.Visible = True
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
              "TSPL_DEMAND_BOOKING_MASTER " + Environment.NewLine +
                              "TSPL_DEMAND_BOOKING_DETAIL " + Environment.NewLine +
            "TSPL_BOOKING_MATSER " + Environment.NewLine +
                              "TSPL_BOOKING_DETAIL " + Environment.NewLine +
                              "TSPL_GATEPASS_MASTER_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                              "TSPL_GATEPASS_DETAIL_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                              "Press Alt+F for Create DO/Post DO Trasnaction" + Environment.NewLine +
                              "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " + Environment.NewLine +
                              "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " + Environment.NewLine +
                              "TSPL_TRANSACTION_APPROVAL (For Approving Pending Document) ")
        End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub

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
        If MyBase.isReverse Then
            btnreverse.Enabled = True
        Else
            btnreverse.Enabled = False
        End If
    End Sub
    Function FillMorningEvening() As DataTable
        Dim qry As String = String.Empty
        If rbtnMorning.IsChecked Then
            qry = "select 'Morning' as value union all select 'Evening' as value "
        ElseIf rbtnEvening.IsChecked Then
            qry = "select 'Evening' as value "
        Else
            qry = "select 'Morning' as value union all select 'Evening' as value "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        If Not SettSeprateDemandForMorningEveningShift Then
            rbtnMorningEveningBoth.IsChecked = True
        End If

        rdbnFreshAmbientBoth.IsChecked = True
        gv1.DataSource = Nothing
        'gv1.ViewDefinition = New TableViewDefinition
        gv1.Rows.AddNew()
        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn
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
        Dim repoShiftName As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoShiftName.FormatString = ""
        repoShiftName.HeaderText = "Shift"
        repoShiftName.Name = colShiftName
        repoShiftName.Width = 100
        repoShiftName.IsVisible = True
        repoShiftName.DataSource = FillMorningEvening()
        repoShiftName.ReadOnly = True
        repoShiftName.DisplayMember = "Value"
        repoShiftName.ValueMember = "Value"
        repoShiftName.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoShiftName)
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
        qry = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch','LTR')
    union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch','LTR')
    union all
    select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient=1   and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
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
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 150
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        Dim repoCrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCrate = New GridViewDecimalColumn()
        repoCrate.FormatString = ""
        repoCrate.HeaderText = "Crate"
        repoCrate.Name = colCrate
        repoCrate.Width = 80
        repoCrate.Minimum = 0
        repoCrate.ReadOnly = True
        repoCrate.IsVisible = True
        repoCrate.IsPinned = True
        repoCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCrate)
        Dim repoLtr As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLtr = New GridViewDecimalColumn()
        repoLtr.FormatString = ""
        repoLtr.HeaderText = "Litre"
        repoLtr.Name = colLitre
        repoLtr.Width = 80
        repoLtr.Minimum = 0
        repoLtr.ReadOnly = True
        repoLtr.IsVisible = True
        repoLtr.IsPinned = True
        repoCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLtr)
        Dim repoMAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMAmt = New GridViewDecimalColumn()
        repoMAmt.FormatString = ""
        repoMAmt.HeaderText = "MAmt"
        repoMAmt.Name = colMAmt
        repoMAmt.Width = 80
        repoMAmt.Minimum = 0
        repoMAmt.ReadOnly = True
        repoMAmt.IsVisible = True
        repoMAmt.IsPinned = True
        repoCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMAmt)
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
        repoCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
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
        repoCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPAmt)
        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Total Amt"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = True
        repoAmt.IsPinned = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)
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
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If intCurrRow = gv1.Rows.Count - 1 Then
            intCurrRow = 0
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If gv1.CurrentColumn Is gv1.Columns(gv1.Columns.Count - 8) Then
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
            gv1.Rows(intCurrRow).Cells(gv1.Columns.Count - 8).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(gv1.Columns.Count - 8).IsCurrent = True
        End If
    End Sub
    Private Sub setPagedown()
        Dim scrollDelta As Integer = gv1.TableElement.ViewElement.ScrollableRows.Size.Height + CInt(gv1.TableElement.ViewElement.ScrollableRows.ScrollOffset.Height)
        Dim newVScrollValue As Integer = gv1.TableElement.VScrollBar.Value + scrollDelta
        If newVScrollValue < gv1.TableElement.VScrollBar.Maximum - gv1.TableElement.VScrollBar.LargeChange Then
            gv1.TableElement.VScrollBar.Value = newVScrollValue
        Else
            gv1.TableElement.VScrollBar.Value = gv1.TableElement.VScrollBar.Maximum - gv1.TableElement.VScrollBar.LargeChange
        End If
        Dim navigator As IGridNavigator = gv1.GridViewElement.Navigator
        navigator.BeginSelection(New GridNavigationContext(GridNavigationInputType.Keyboard, MouseButtons.None, Keys.None))
        navigator.SelectRow(Me.GetLastScrollableRow(gv1.TableElement))
        navigator.EndSelection()
    End Sub
    Private Function GetLastScrollableRow(ByVal tableElement As GridTableElement) As GridViewRowInfo
        Dim rows As ScrollableRowsContainerElement = tableElement.ViewElement.ScrollableRows
        Dim traverser As GridTraverser = CType((CType(tableElement.RowScroller, IEnumerable)).GetEnumerator(), GridTraverser)
        For i As Integer = 0 To rows.Children.Count - 1
            If rows.Children(i).BoundingRectangle.Bottom > rows.Size.Height Then
                Exit For
            End If
            If Not traverser.MoveNext() Then
                Exit For
            End If
        Next
        Return traverser.Current
    End Function
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("Booth"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustCode).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustName).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colShiftName).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colItemExist).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIsItemUpdate).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colBookingCreatedFor3Days).Name)
                view.ColumnGroups(0).IsPinned = True
                Dim TempColGroupCount As Integer = 1
                Dim obj As ItemValueClass = New ItemValueClass()
                Dim i As Integer = 1
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
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
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colCrate).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colLitre).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colAmt).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colMAmt).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colPCount).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colPAmt).Name)
                view.ColumnGroups(TempColGroupCount).IsPinned = True
                view.ColumnGroups(TempColGroupCount).PinPosition = PinnedColumnPosition.Right
                'MergeHorizontally(gv1, 0, gv1.Rows.Count - 1)
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'detect up arrow key
        Try
            If keyData = Keys.Up AndAlso gv1.CurrentRow.IsSelected Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index - 1)
            ElseIf keyData = Keys.Down Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub AddNew()
        Try
            blnSaveTotalQTy = False
            isNewEntry = True
            btnSave.Text = "Save"
            txtDocNo.Value = ""
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            btn_TruckSheet.Enabled = False
            SplitButtonTruckSheet.Enabled = False
            btn_TSCancel.Enabled = True
            btn_Gatepass.Enabled = False
            btnPrint.Enabled = False
            btn_TSCancel.Enabled = False
            btn_GPCancel.Enabled = False
            txtcustomersearch.Text = ""
            txtDate.Value = clsCommon.GETSERVERDATE()
            UsLock1.Status = ERPTransactionStatus.Pending
            chkIndividualCustomer.Checked = False
            chkIndividualCustomer.Enabled = True
            lblTotalCrate.Text = "0"
            lblTotalLitre.Text = "0"
            lblDocumentAmt.Text = "0"
            TxtCity.Value = ""
            lblTransporterName.Text = ""
            lblCityName.Text = ""
            txtRouteNo.Value = ""
            lblRouteDesc.Text = ""
            txtVehicleNo.Value = ""
            lblVehicleNo.Text = ""
            txtTripNo.Text = ""
            txtCustomerNo.Value = ""
            lblCustomerName.Text = ""
            txtLocation.Value = Nothing
            txtCustomerNo.Enabled = False
            lblLocation.Text = ""
            txtPCount.Text = "0"
            txtPAmt.Text = "0"
            txtDocAmt.Text = "0"
            chkMorningPosted.Checked = False
            chkEveningPosted.Checked = False
            RadGroupBox1.Enabled = True
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
            If Not SettSeprateDemandForMorningEveningShift Then
                rbtnMorningEveningBoth.IsChecked = True
            End If
            RadGroupBox3.Enabled = True

            rdbnFreshAmbientBoth.IsChecked = True
            chkMorningGatepassTruckSheetGenerated.Checked = False
            chkEveningGatepassTruckSheetGenerated.Checked = False
            FlagChangeVehical = False
            LoadBlankGrid()
            FlagPaste = False
            RefreshFormName()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Index >= 7 And e.Column.Name <> colCrate And e.Column.Name <> colAmt And e.Column.Name <> colLitre And e.Column.Name <> colMAmt And e.Column.Name <> colPCount And e.Column.Name <> colPCount Then
                        'If isLoadData = False AndAlso (clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0) Then
                        If isLoadData = False Then
                            ''UpdateItemQtyAfterSave(gv1.CurrentRow.Index, gv1.CurrentColumn.Index)
                            UpdateAllTotals()
                            HideUnhideRowsAndColumnsOFGrid()
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
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        AddNew()
        gv1.DataSource = Nothing
    End Sub
    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
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
            If SettSeprateDemandForMorningEveningShift Then
                If Not (rbtnMorning.IsChecked Or rbtnEvening.IsChecked) Then
                    Throw New Exception("Shift should be Morning/Evening")
                End If
            End If
            isInsideLoadData = True
            UpdateAllTotals()
            isInsideLoadData = False
            Dim dblQuantityCount As Double = 0
            Dim dblQuantityMORNINGCount As Double = 0
            Dim dblQuantityEveningCount As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strItemValueExist As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemExist).Value)
                If clsCommon.CompairString(strItemValueExist, "Yes") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colShiftName).Value), "Morning") = CompairStringResult.Equal Then
                        dblQuantityMORNINGCount = dblQuantityMORNINGCount + 1
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colShiftName).Value), "Evening") = CompairStringResult.Equal Then
                        dblQuantityEveningCount = dblQuantityEveningCount + 1
                    End If
                    dblQuantityCount = dblQuantityCount + 1
                End If
            Next
            If dblQuantityCount <= 0 Then
                Throw New Exception("Please enter quantity for atleast one customer")
            End If
            ''If  Then
            Dim Morningvalue As Double = 0
            Dim eveningvalue As Double = 0
            If chkIndividualCustomer.Checked = False AndAlso clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) <= 0 AndAlso UseCutOffTimeonRouteForERP = True Then
                Dim strMorningShiftTime As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  MorningCutOff_Time from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                Dim strEveningShiftTime As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  EveningCutOff_Time from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                If dblQuantityMORNINGCount > 0 Then
                    If clsCommon.myLen(strMorningShiftTime) <= 0 Then
                        Throw New Exception("Please enter Morning Cut Off time for Route " & lblRouteDesc.Text & "")
                    End If
                    If clsCommon.myLen(strMorningShiftTime) > 0 Then
                        Morningvalue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select datediff(minute,cast(MorningCutOff_Time as time),cast('" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as time)) as aa from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                        If Morningvalue > 0 Then
                            Throw New Exception("You cannot create Demand Booking due to Morning cut off is over")
                        End If
                    End If
                End If
                If dblQuantityEveningCount > 0 Then
                    If clsCommon.myLen(strEveningShiftTime) <= 0 Then
                        Throw New Exception("Please enter Evening Cut Off time for Route " & lblRouteDesc.Text & "")
                    End If
                    If clsCommon.myLen(strEveningShiftTime) > 0 Then
                        eveningvalue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select datediff(minute,cast(EveningCutOff_Time as time),cast('" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as time)) as aa from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                        If eveningvalue > 0 Then
                            Throw New Exception("You cannot create Demand Booking due to Evening cut off is over")
                        End If
                    End If
                End If
            End If
            '' to check cut off time on update 
            If chkIndividualCustomer.Checked = False AndAlso clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0 AndAlso UseCutOffTimeonRouteForERP = True Then
                Morningvalue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select datediff(minute,cast(MorningCutOff_Time as time),cast('" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as time)) as aa from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                eveningvalue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select datediff(minute,cast(EveningCutOff_Time as time),cast('" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm tt") + "' as time)) as aa from TSPL_ROUTE_MASTER where route_no='" & txtRouteNo.Value & "'"))
                If Morningvalue > 0 OrElse eveningvalue > 0 Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        Dim strItemValueExist As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemExist).Value)
                        If clsCommon.CompairString(strItemValueExist, "Yes") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colShiftName).Value), "Morning") = CompairStringResult.Equal Then
                                If Morningvalue > 0 Then
                                    Dim strDemandBooKingNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from tspl_demand_booking_detail where document_no='" & clsCommon.myCstr(txtDocNo.Value) & "' and ShiftType='Morning' and Cust_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value) & "'"))
                                    If clsCommon.myLen(clsCommon.myCstr(strDemandBooKingNo)) <= 0 Then
                                        Throw New Exception("You cannot create Demand Booking for customer " & clsCommon.myCstr(gv1.Rows(ii).Cells(colCustName).Value) & " due to Morning cut off is over")
                                    End If
                                End If
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colShiftName).Value), "Evening") = CompairStringResult.Equal Then
                                If eveningvalue > 0 Then
                                    Dim strDemandBooKingNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from tspl_demand_booking_detail where document_no='" & clsCommon.myCstr(txtDocNo.Value) & "' and ShiftType='Evening' and Cust_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colCustCode).Value) & "'"))
                                    If clsCommon.myLen(clsCommon.myCstr(strDemandBooKingNo)) <= 0 Then
                                        Throw New Exception("You cannot create Demand Booking for customer " & clsCommon.myCstr(gv1.Rows(ii).Cells(colCustName).Value) & " due to Evening cut off is over")
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            '' to check gatepass or truck sheet generated
            If clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0 Then
                Dim strDocNoForGatePassOrTrucksheetGeneratedMorning As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & txtDocNo.Value & "' and (IsGatePassGenerated='Y' or IsTruckSheetGenerated ='Y') and ShiftType='Evening' "))
                Dim strDocNoForGatePassOrTrucksheetGeneratedEvening As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & txtDocNo.Value & "' and (IsGatePassGenerated='Y' or IsTruckSheetGenerated ='Y') and ShiftType='Morning' "))
                If clsCommon.myLen(clsCommon.myCstr(strDocNoForGatePassOrTrucksheetGeneratedMorning)) > 0 And clsCommon.myLen(clsCommon.myCstr(strDocNoForGatePassOrTrucksheetGeneratedEvening)) > 0 Then
                    Throw New Exception("Demand cannot be updated because its Gate Pass/Trucksheet has generated for both shifts.")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData(0)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function SaveData(ByVal IsRepeatOrder As Integer) As Boolean
        Try
            Dim qry As String = ""
            blnSaveTotalQTy = True
            'BookingStatus = 0
            Dim strPriceCode As String = String.Empty
            Dim LineNo As Integer = 1
            If (AllowToSave(Nothing)) Then
                Dim obj As New clsDemandBookingSale()
                If IsRepeatOrder = 1 Then
                    obj.Document_Date = clsCommon.myCDate(txtDate.Value).AddDays(1)
                    If Not chkMorningPosted.Checked AndAlso Not chkEveningPosted.Checked Then
                        obj.Document_No = ""
                        isNewEntry = True
                    Else
                        isNewEntry = False
                        obj.Document_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No=" + clsCommon.myCstr(txtRouteNo.Value) + " and Document_Date ='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "' and location_code='" + clsCommon.myCstr(txtLocation.Value) + "' ")
                    End If
                Else
                    obj.Document_No = txtDocNo.Value
                    obj.Document_Date = txtDate.Value
                End If
                obj.Location_Code = txtLocation.Value
                obj.Route_No = txtRouteNo.Value
                obj.City_Code = TxtCity.Value
                obj.TripNo = txtTripNo.Text
                If rbtnEvening.IsChecked = True Then
                    obj.ShiftType = "Evening"
                ElseIf rbtnMorning.IsChecked = True Then
                    obj.ShiftType = "Morning"
                Else
                    obj.ShiftType = "Both"
                End If
                If rbtn_Fresh.IsChecked = True Then
                    obj.ItemType = "Fresh"
                ElseIf rbtn_Ambient.IsChecked = True Then
                    obj.ItemType = "Ambient"
                Else
                    obj.ItemType = "Both"
                End If
                If chkIndividualCustomer.Checked = True Then
                    obj.IsIndividualCustomer = 1
                Else
                    obj.IsIndividualCustomer = 0
                End If
                obj.TotalQtyInCrates = clsCommon.myCdbl(lblTotalCrate.Text)
                obj.TotalQtyInLtr = clsCommon.myCdbl(lblTotalLitre.Text)
                obj.DocumentAmount = clsCommon.myCdbl(lblDocumentAmt.Text)
                obj.Arr = New List(Of clsDemandBookingSaleDetail)
                ''richa 4 Aug,2021 optimization related
                Dim intLine As Integer = 0
                For dblrows As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colItemExist).Value), "Yes") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Morning") = CompairStringResult.Equal AndAlso chkMorningPosted.Checked Then
                            Continue For
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Evening") = CompairStringResult.Equal AndAlso chkEveningPosted.Checked Then
                            Continue For
                        End If
                        If IsRepeatOrder = 1 Then
                            If Not chkMorningPosted.Checked AndAlso Not chkEveningPosted.Checked Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Evening") = CompairStringResult.Equal AndAlso rbtnMorning.IsChecked Then
                                    Continue For
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Morning") = CompairStringResult.Equal AndAlso rbtnEvening.IsChecked Then
                                    Continue For
                                End If
                            End If
                        End If
                        Dim k As Integer = 1
                        For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            k = k + 1
                            If obj1 IsNot Nothing Then
                                If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                    Dim objTr As New clsDemandBookingSaleDetail()
                                    objTr.Line_No = LineNo
                                    objTr.Cust_Code = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)
                                    objTr.ShiftType = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value)
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colIsItemUpdate).Value), "Yes") = CompairStringResult.Equal Then
                                        objTr.IsItemUpdate = 1
                                    Else
                                        objTr.IsItemUpdate = 0
                                    End If
                                    If clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                        If IsRepeatOrder = 1 Then
                                            If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                                                If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and IsReorder=1")) = CompairStringResult.Equal Then
                                                    objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                End If
                                            Else
                                                objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                            End If
                                        Else
                                            objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                        End If
                                        objTr.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                        objTr.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                        objTr.Rate = clsCommon.myCstr(obj1.ItemRate)
                                        objTr.ItemNetAmount = clsCommon.myCdbl(objTr.Rate * objTr.Qty)
                                        objTr.Vehicle_Code = clsCommon.myCstr(txtVehicleNo.Value)
                                        If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                                objTr.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                            Else
                                                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                                If ItemCrateType = 1 Then
                                                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                                    Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                                    Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                                    If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                                        Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                                                        If DispatchQty > (CrateConvFactor / 2) Then
                                                            objTr.TotalCrates_ItemWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                                                        Else
                                                            objTr.TotalCrates_ItemWise = 0
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            ''to convert into litre
                                            Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                            Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                            If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                                                Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor_Ltr
                                                objTr.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                            End If
                                        End If
                                        qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                           "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                           "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(objTr.Cust_Code) & "'"
                                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                                        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                            'objTr.Route_No = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                            'objTr.Vehicle_Code = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
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
                If IsRepeatOrder = 1 Then
                    Dim isSave As Boolean = False
                    If clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0 Then
                        isSave = clsDemandBookingSaleDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, Nothing, obj.Location_Code, obj.ShiftType, isNewEntry)
                        If isSave Then
                            clsCommon.MyMessageBoxShow(Me, "" + clsCommon.myCstr(obj.ShiftType) + " Demand Data Saved Successfully", Me.Text)
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Somthing Went Worng", Me.Text)
                        End If
                    Else
                        If (obj.SaveData(obj, isNewEntry)) = True Then
                            clsCommon.MyMessageBoxShow(Me, "" + clsCommon.myCstr(obj.ShiftType) + " Demand Data Saved Successfully", Me.Text)

                        End If
                    End If
                Else
                    If (obj.SaveData(obj, isNewEntry)) = True Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                        Return True
                    End If
                End If
                'FlagCopy = False
            Else
                Return False
            End If
            blnSaveTotalQTy = True
        Catch ex As Exception
            blnSaveTotalQTy = True
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function
    Sub GatePass_TruckSheet_Button()
        Try
            If rbtnMorningEveningBoth.IsChecked = True Then
                btn_TruckSheet.Enabled = False
                SplitButtonTruckSheet.Enabled = False
                btn_Gatepass.Enabled = False
                btn_TSCancel.Enabled = False
                btn_GPCancel.Enabled = False
                btnPrint.Enabled = False
            Else
                Dim strDocNoForGatePass As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & txtDocNo.Value & "' and ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' and IsGatePassGenerated='Y'"))
                Dim strDocNoForTrucksheet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & txtDocNo.Value & "' and ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' and  IsTruckSheetGenerated ='Y'"))
                If clsCommon.myLen(strDocNoForGatePass) > 0 Then
                    btn_Gatepass.Enabled = False
                    btn_GPCancel.Enabled = True
                Else
                    btn_Gatepass.Enabled = True
                    btn_GPCancel.Enabled = False
                End If
                If clsCommon.myLen(strDocNoForTrucksheet) > 0 Then
                    btn_TruckSheet.Enabled = False
                    SplitButtonTruckSheet.Enabled = False
                    btn_TSCancel.Enabled = True
                Else
                    btn_TruckSheet.Enabled = True
                    SplitButtonTruckSheet.Enabled = True
                    btn_TSCancel.Enabled = False
                End If
                btnPrint.Enabled = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim dblTotalDocAmt As Decimal = 0
            Dim qry As String = ""
            Dim obj As New clsDemandBookingSale
            'Dim intRow As Integer
            obj = clsDemandBookingSale.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                AddNew()
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtLocation.Value = obj.Location_Code
                TxtCity.Value = obj.City_Code
                txtRouteNo.Value = obj.Route_No
                txtTripNo.Text = obj.TripNo
                If obj.IsIndividualCustomer = 1 Then
                    chkIndividualCustomer.Checked = True
                    txtCustomerNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT top 1 cust_code  FROM TSPL_DEMAND_BOOKING_DETAIL where document_no='" & txtDocNo.Value & "' "))
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
                lblTotalCrate.Text = obj.TotalQtyInCrates
                lblTotalLitre.Text = obj.TotalQtyInLtr
                lblDocumentAmt.Text = obj.DocumentAmount

                If clsCommon.CompairString(obj.ShiftType, "Morning") = CompairStringResult.Equal Then
                    rbtnMorning.IsChecked = True
                ElseIf clsCommon.CompairString(obj.ShiftType, "Evening") = CompairStringResult.Equal Then
                    rbtnEvening.IsChecked = True
                Else
                    rbtnMorningEveningBoth.IsChecked = True
                End If
                If SettSeprateDemandForMorningEveningShift Then
                    RadGroupBox3.Enabled = False
                End If
                If clsCommon.CompairString(obj.ItemType, "Fresh") = CompairStringResult.Equal Then
                    rbtn_Fresh.IsChecked = True
                ElseIf clsCommon.CompairString(obj.ItemType, "Ambient") = CompairStringResult.Equal Then
                    rbtn_Ambient.IsChecked = True
                Else
                    rdbnFreshAmbientBoth.IsChecked = True
                End If
                LoadBlankGrid()
                setCustomerDetail(TxtCity.Value, txtRouteNo.Value)
                chkMorningPosted.Checked = (obj.Posted_Morning = 1)
                chkEveningPosted.Checked = (obj.Posted_Evening = 1)
                Dim dblMorningCount As Integer = 0
                Dim dblEveningCount As Integer = 0
                isLoadData = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDemandBookingSaleDetail In obj.Arr
                        For dblrows As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value), objTr.Cust_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), objTr.ShiftType) = CompairStringResult.Equal Then
                                Dim k As Integer = 1
                                For columns = 7 To gv1.Columns.Count - 8
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.ShiftType, "Morning") = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                        dblMorningCount = 1
                                    ElseIf clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.ShiftType, "Evening") = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                        dblEveningCount = 1
                                    End If
                                Next
                            End If
                        Next
                        'strCustCode = objTr.Cust_Code
                        txtVehicleNo.Value = objTr.Vehicle_Code
                        lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleNo.Value) + "'"))
                        If clsCommon.CompairString(objTr.ShiftType, "Evening") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(objTr.IsGatePassGenerated, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.IsTruckSheetGenerated, "Y") = CompairStringResult.Equal Then
                                chkEveningGatepassTruckSheetGenerated.Checked = True
                            End If
                        End If
                        If clsCommon.CompairString(objTr.ShiftType, "Morning") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(objTr.IsGatePassGenerated, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.IsTruckSheetGenerated, "Y") = CompairStringResult.Equal Then
                                chkMorningGatepassTruckSheetGenerated.Checked = True
                            End If
                        End If
                    Next
                End If
                lblTransporterName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select transporter_name from tspl_transport_master where Transport_Id =(select Transport_Id from tspl_vehicle_master where vehicle_id= '" + Convert.ToString(txtVehicleNo.Value) + "')"))
                'HideUnhideRowsOFGrid()
                isLoadData = False
                UpdateAllTotals()
                If Not SettSeprateDemandForMorningEveningShift Then
                    HideUnhideRowsAndColumnsOFGrid()
                End If
            End If
            SetRouteColumns()
            RefreshFormName()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            isLoadData = False
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'  "
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
            Dim qry As String = "select TSPL_DEMAND_BOOKING_MASTER.Document_No as DocumentNo,convert(varchar(12),TSPL_DEMAND_BOOKING_MASTER.Document_date,103) as Document_date,TSPL_DEMAND_BOOKING_MASTER.ShiftType,TSPL_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_DEMAND_BOOKING_MASTER.Location_Code as [Location Code],TSPL_DEMAND_BOOKING_MASTER.City_Code as [City Code],TripNo AS [Trip No],case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_DEMAND_BOOKING_MASTER"
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentNo", "", txtDocNo.Value, "DocumentNo", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseData()
    End Sub
    Sub CloseData()
        clsERPFuncationality.closeForm(Me)
    End Sub
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
                If (clsDemandBookingSale.DeleteData(txtDocNo.Value)) Then
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
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Export()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel()
        Try
            Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Booking No : " + txtDocNo.Value
            arrHeader.Add(strtemp)
            strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            strtemp = "Created By : " + strCreatedBy
            arrHeader.Add(strtemp)
            strtemp = "Transaction Type : " + IIf(rbtn_Fresh.IsChecked = True, "Fresh", "Ambient")
            arrHeader.Add(strtemp)
            clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Shared Function CreateNotificationContentEMP(ByVal Booking_Id As String, ByVal Booking_Date As DateTime, ByVal Ex_Factory_Date As DateTime, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        'Dim strNotification_Condition_Date As DateTime = Nothing
        'Dim strdate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Condition_Date from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        'If clsCommon.myLen(strdate) > 0 Then
        '    strNotification_Condition_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("SELECT Condition_Date from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'", trans))
        'End If
        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_ConditionDate = clsCommon.myCDate(Ex_Factory_Date)
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(Booking_Id))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(Booking_Date))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Ex_Factory_Date, clsCommon.myCstr(clsCommon.myCDate(Ex_Factory_Date)))
            objNotification.SaveData(clsUserMgtCode.frmbookingdairy, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function
    Private Sub btnLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub btnSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub gv1_Pasting(sender As Object, e As GridViewClipboardEventArgs) Handles gv1.Pasting
        FlagPaste = True
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub
    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Dim Qry As String = GetQuery()
            Qry += " where TSPL_BOOKING_MATSER.Document_No ='" + strDocNo + "'"
            'Qry = "Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDSBoking", "Performa Invoice", "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetQuery() As String
        Dim Qry As String = " select TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,TSPL_COMPANY_MASTER.Comp_Name AS CompName ," &
                            " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP," &
                            " TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER.STATE_NAME   )>0 then ', '+ TSPL_STATE_MASTER.STATE_NAME  else ' ' end    as Location_Address_GP, " &
                            " TSPL_BOOKING_DETAIL.item_code,tspl_item_master.item_desc,TSPL_BOOKING_DETAIL.booking_Qty,TSPL_BOOKING_DETAIL.unit_Code,TSPL_BOOKING_DETAIL.item_Rate,TSPL_BOOKING_DETAIL.documentAmount,TSPL_BOOKING_DETAIL.vehicle_code,tspl_vehicle_master.Number as Vechicle_Name,TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.customer_name " &
                            " from TSPL_BOOKING_DETAIL  left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " &
                            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_BOOKING_MATSER.comp_code " &
                            " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
                            " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
                            " left join tspl_location_master on TSPL_LOCATION_MASTER .Location_Code =TSPL_BOOKING_MATSER.location_code " &
                            " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER .STATE_CODE =tspl_location_master.State " &
                            " left join tspl_item_master on tspl_item_master.item_code=TSPL_BOOKING_DETAIL.item_code " &
                            " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code" &
                            " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .cust_code=TSPL_BOOKING_DETAIL.cust_code "
        Return Qry
    End Function
    Private Sub ddlTaxType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
        If isInsideLoadData = False Then
            'AddNew()
            'ddlTaxType.Text = "Select"
            txtLocation.Value = Nothing
            lblLocation.Text = ""
            gv1.DataSource = Nothing
        End If
    End Sub
    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsDemandBookingSale.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtn_Ambient_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtn_Ambient.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
            lblTotalLitre.Text = ""
            lblTotalCrate.Text = ""
            lblDocumentAmt.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            txtRouteNo.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRouteNo.Value, Nothing))
            If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 Then
                setRouteVehicleCityDetail()
            End If
            SetRouteColumns()
            RefreshFormName()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetRouteColumns()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select isnull(Entry_UOM,0) as Entry_UOM from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        If clsCommon.CompairString(obj1.IsFreshAmbient, "Fresh") = CompairStringResult.Equal Then
                            If clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 0 Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = True
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 1 Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(dblcolumns).Width = 100
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 2 Then
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(dblcolumns).Width = 100
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub RefreshFormName()
        Me.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when len(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as FormName from tspl_Program_master where program_code='" + clsUserMgtCode.frmDemandBooking + "'"))
        If clsCommon.myLen(txtRouteNo.Value) > 0 Then
            Me.Text += " - " + txtRouteNo.Value + ""
        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) <= 0 Then
                Throw New Exception("Please select Route First")
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtCity.Value)) <= 0 Then
                Throw New Exception("Please select City First")
            End If
            setCustomerDetail(TxtCity.Value, txtRouteNo.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub setCustomerDetail(ByVal strCityCode As String, ByVal strtRouteCode As String)
        Try
            Dim qry As String = ""
            qry = "select cust_code,Customer_name from TSPL_CUSTOMER_MASTER where route_no='" + strtRouteCode + "' and City_code='" + strCityCode + "' and  TSPL_CUSTOMER_MASTER.Status='N' "
            If chkIndividualCustomer.Checked = True Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code ='" & txtCustomerNo.Value & "'"
            End If
            qry += " order by isnull(TSPL_CUSTOMER_MASTER.display_seq,0)  "
            LoadBlankGrid()
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                Dim i As Integer = 1
                For Each dr As DataRow In dt1.Rows
                    Dim flagE As Boolean = True
                    Dim flagM As Boolean = True

                    If SettSeprateDemandForMorningEveningShift Then
                        flagE = rbtnEvening.IsChecked
                        flagM = rbtnMorning.IsChecked
                        RadGroupBox3.Enabled = False
                    End If

                    If flagE Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("cust_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).Value = "Evening"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).ReadOnly = True
                        i = i + 1
                        gv1.Rows.AddNew()
                    End If
                    If flagM Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("cust_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).Value = "Morning"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).ReadOnly = True
                        i = i + 1
                        gv1.Rows.AddNew()
                    End If
                Next
                For n As Integer = 0 To gv1.Rows.Count - 1
                    Try
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(n).Cells(colCustCode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(n).Cells(colShiftName).Value)) > 0 Then
                            Dim strqry As String = "Select count(ShiftType) from (select ShiftType,convert(date,Document_Date ,103) as Document_Date from (select tspl_demand_booking_master.Document_Date , tspl_demand_booking_detail.ShiftType  from tspl_demand_booking_detail
left outer join tspl_demand_booking_master on tspl_demand_booking_detail.document_no=tspl_demand_booking_master.document_no
where convert(date,tspl_demand_booking_master.document_date,103) in ('" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-1)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-3)) & "')
and tspl_demand_booking_detail.Cust_Code='" & clsCommon.myCstr(gv1.Rows(n).Cells(colCustCode).Value) & "' and TSPL_DEMAND_BOOKING_DETAIL .ShiftType ='" & clsCommon.myCstr(gv1.Rows(n).Cells(colShiftName).Value) & "')Final 
group by ShiftType ,convert(date,Document_Date ,103))FinalQry"
                            gv1.Rows(n).Cells(colBookingCreatedFor3Days).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strqry))
                            Try
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(n).Cells(colBookingCreatedFor3Days).Value), "3") = CompairStringResult.Equal Then
                                    gv1.Rows(n).Cells(colLineNo).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colLineNo).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colLineNo).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colCustCode).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colCustCode).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colCustCode).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colCustName).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colCustName).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colCustName).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colShiftName).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colShiftName).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colShiftName).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colCrate).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colCrate).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colCrate).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colAmt).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colAmt).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colAmt).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colLitre).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colLitre).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colLitre).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colMAmt).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colMAmt).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colMAmt).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colPCount).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colPCount).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colPCount).Style.BackColor = Color.LightGreen
                                    gv1.Rows(n).Cells(colPAmt).Style.DrawFill = True
                                    gv1.Rows(n).Cells(colPAmt).Style.CustomizeFill = True
                                    gv1.Rows(n).Cells(colPAmt).Style.BackColor = Color.LightGreen
                                End If
                            Catch ex As Exception
                                Throw New Exception(ex.Message)
                            End Try
                            Try
                                For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                                    If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code).ToUpper, "POUCH") = CompairStringResult.Equal Then
                                        gv1.Rows(n).Cells(dblcolumns).Style.DrawFill = True
                                        gv1.Rows(n).Cells(dblcolumns).Style.CustomizeFill = True
                                        gv1.Rows(n).Cells(dblcolumns).Style.BackColor = Color.DarkOrange
                                    End If
                                Next
                            Catch ex As Exception
                                Throw New Exception(ex.Message)
                            End Try
                        End If
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                Next
            Else
                Throw New Exception("No Customers found for Selected Route and City")
            End If
            gv1.Columns(colLineNo).IsPinned = True
            gv1.Columns(colCustCode).IsPinned = True
            gv1.Columns(colCustName).IsPinned = True
            gv1.Columns(colShiftName).IsPinned = True
            gv1.Columns(colItemExist).IsPinned = True
            gv1.Columns(colIsItemUpdate).IsPinned = True
            gv1.Columns(colBookingCreatedFor3Days).IsPinned = True
            gv1.Columns(colAmt).IsPinned = True
            gv1.Columns(colCrate).IsPinned = True
            gv1.Columns(colLitre).IsPinned = True
            gv1.Columns(colMAmt).IsPinned = True
            gv1.Columns(colPCount).IsPinned = True
            gv1.Columns(colPAmt).IsPinned = True
            gv1.Columns(colLineNo).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colCustCode).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colCustName).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colShiftName).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colItemExist).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colIsItemUpdate).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colBookingCreatedFor3Days).PinPosition = PinnedColumnPosition.Left
            gv1.Columns(colAmt).PinPosition = PinnedColumnPosition.Right
            gv1.Columns(colCrate).PinPosition = PinnedColumnPosition.Right
            gv1.Columns(colLitre).PinPosition = PinnedColumnPosition.Right
            gv1.Columns(colMAmt).PinPosition = PinnedColumnPosition.Right
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
            MergeVertically(gv1, New Integer() {1, 2})
            'View()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub rbtnMorning_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnMorning.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub HideUnhideRowsAndColumnsOFGrid()
        Try
            isLoadData = True
            Dim dblTotalCount As Decimal = 0
            Dim dblTotalPAmt As Decimal = 0
            Dim dblTotalCeart As Decimal = 0
            Dim dblTotalMAmt As Decimal = 0
            Dim dblTotalLiter As Decimal = 0
            For dblrows As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) > 0 Then
                    If rbtnMorning.IsChecked Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Evening") = CompairStringResult.Equal Then
                            gv1.Rows(dblrows).IsVisible = False
                        Else
                            If rbtn_Fresh.IsChecked Then
                                dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                                dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                                dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                            ElseIf rbtn_Ambient.IsChecked Then
                                dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                                dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                            Else
                                dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                                dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                                dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                                dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                                dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                            End If
                            gv1.Rows(dblrows).IsVisible = True
                        End If
                    ElseIf rbtnEvening.IsChecked Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colShiftName).Value), "Morning") = CompairStringResult.Equal Then
                            gv1.Rows(dblrows).IsVisible = False
                        Else
                            If rbtn_Fresh.IsChecked Then
                                dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                                dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                                dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                            ElseIf rbtn_Ambient.IsChecked Then
                                dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                                dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                            Else
                                dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                                dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                                dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                                dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                                dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                            End If
                            gv1.Rows(dblrows).IsVisible = True
                            'gv1.Rows(dblrows).IsVisible = True
                        End If
                    Else
                        If rbtn_Fresh.IsChecked Then
                            dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                            dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                            dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                        ElseIf rbtn_Ambient.IsChecked Then
                            dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                            dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                        Else
                            dblTotalCeart += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                            dblTotalLiter += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                            dblTotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
                            dblTotalCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                            dblTotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                        End If
                        gv1.Rows(dblrows).IsVisible = True
                    End If
                End If
            Next
            For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
                Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                If obj1 IsNot Nothing Then
                    If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then
                        If rbtn_Fresh.IsChecked Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(colMAmt).IsVisible = True
                                gv1.Columns(colCrate).IsVisible = True
                                gv1.Columns(colLitre).IsVisible = True
                            Else
                                'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                gv1.Columns(dblcolumns).IsVisible = False
                                gv1.Columns(colPAmt).IsVisible = False
                                gv1.Columns(colPCount).IsVisible = False
                            End If
                        ElseIf rbtn_Ambient.IsChecked Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Ambient") = CompairStringResult.Equal Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(colPAmt).IsVisible = True
                                gv1.Columns(colPCount).IsVisible = True
                            Else
                                'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                gv1.Columns(dblcolumns).IsVisible = False
                                gv1.Columns(colMAmt).IsVisible = False
                                gv1.Columns(colCrate).IsVisible = False
                                gv1.Columns(colLitre).IsVisible = False
                            End If
                        Else
                            gv1.Columns(dblcolumns).IsVisible = True
                            gv1.Columns(colPAmt).IsVisible = True
                            gv1.Columns(colPCount).IsVisible = True
                            gv1.Columns(colMAmt).IsVisible = True
                            gv1.Columns(colCrate).IsVisible = True
                            gv1.Columns(colLitre).IsVisible = True
                        End If
                    End If
                End If
            Next
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                GatePass_TruckSheet_Button()
            End If
            MergeVertically(gv1, New Integer() {1, 2})
            isLoadData = False
            'UpdateAllTotals()
            lblTotalCrate.Text = dblTotalCeart
            lblTotalLitre.Text = dblTotalLiter
            lblDocumentAmt.Text = dblTotalMAmt
            txtPCount.Text = dblTotalCount
            txtPAmt.Text = dblTotalPAmt
            txtDocAmt.Text = dblTotalMAmt + dblTotalPAmt
            SetRouteColumns()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            isLoadData = False
        End Try
    End Sub
    Private Sub UpdateItemQtyAfterSave(ByVal dblrows As Integer, ByVal dblcolumns As Integer)
        Try
            Dim strItemUpdateAfterSave As String = String.Empty
            'For dblrows As Integer = 0 To gv1.Rows.Count - 1
            Dim k As Integer = 1
            ' For dblrows As Integer = 0 To gv1.Rows.Count - 1
            Dim TotalCrate As Double = 0
            Dim dblTotalDocAmt As Decimal = 0
            Dim dblTotalCrateRowWise As Double = 0
            Dim strPriceCode As String = String.Empty
            Dim strItemValueExist As String = String.Empty
            Dim TotalLitre As Decimal = 0
            Dim dblTotalLitreRowWise As Double = 0
            k = 1
            strItemValueExist = "No"
            strItemUpdateAfterSave = "No"
            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
            k = k + 1
            If obj1 IsNot Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                            'TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colItemCode + clsCommon.myCstr(dblcolumns)).Value)
                            TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            obj1.FreshItem_QtyInCrates = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                        Else
                            Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                            If ItemCrateType = 1 Then
                                ' Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                    Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                                    If DispatchQty >= CrateConvFactor Then
                                        dblTotalCrateRowWise = Math.Floor(DispatchQty / CrateConvFactor)
                                    Else
                                        dblTotalCrateRowWise = 0
                                    End If
                                Else
                                    ' clsCommon.MyMessageBoxShow("Please fill conversion factor for this unit at line no." & i + 1 & "")
                                End If
                                obj1.FreshItem_QtyInCrates = dblTotalCrateRowWise
                                TotalCrate = TotalCrate + dblTotalCrateRowWise
                            End If
                        End If
                        ''to convert into litre
                        'Dim IsStockingUnit_Ltr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                        Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                        Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                        If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                            Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor_Ltr
                            If DispatchQty >= CrateConvFactor_Ltr Then
                                dblTotalLitreRowWise = Math.Floor(DispatchQty / CrateConvFactor_Ltr)
                            Else
                                dblTotalLitreRowWise = 0
                            End If
                        Else
                            ' clsCommon.MyMessageBoxShow("Please fill conversion factor for this unit at line no." & i + 1 & "")
                        End If
                        obj1.FreshItem_QtyInLitres = dblTotalLitreRowWise
                        TotalLitre = TotalLitre + dblTotalLitreRowWise
                        ''---------end of litre conversion
                    End If
                    strItemValueExist = "Yes"
                    Dim dt As New DataTable()
                    Dim dblRate As Double = 0
                    Dim qry As String = "select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) & "'"
                    strPriceCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(strPriceCode) <= 0 Then
                        Throw New Exception("price_CodeNon not found for Customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & "")
                    End If
                    qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                    " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                    "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                    " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                    " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                    " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                    " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                    "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                    "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                    "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                    "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                    " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                    " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                    "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                    "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                    "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & obj1.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & obj1.itemCode & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                    ") XXXE WHERE RowNo=1  "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 Then
                        dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                        If dblRate = 0 Then
                            Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(obj1.ShortDesc) & Environment.NewLine)
                        End If
                        dblTotalDocAmt = dblTotalDocAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                    Else
                        Throw New Exception("Please create Price for Item " & obj1.ShortDesc & " and customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & " ")
                    End If
                End If
            End If
            ' Next
            gv1.Rows(dblrows).Cells(colCrate).Value = Math.Round(clsCommon.myCdbl(TotalCrate), 2)
            gv1.Rows(dblrows).Cells(colLitre).Value = Math.Round(clsCommon.myCdbl(TotalLitre), 2)
            gv1.Rows(dblrows).Cells(colAmt).Value = clsCommon.myCdbl(dblTotalDocAmt)
            'gv1.Rows(dblrows).Cells(colMAmt).Value = clsCommon.myCdbl(dblTotalDocAmt)
            'gv1.Rows(dblrows).Cells(colPCount).Value = clsCommon.myCdbl(dblTotalDocAmt)
            'gv1.Rows(dblrows).Cells(colPAmt).Value = clsCommon.myCdbl(dblTotalDocAmt)
            gv1.Rows(dblrows).Cells(colItemExist).Value = strItemValueExist
            ' Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
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
            Dim dblTotalPAmt As Double = 0
            Dim dblTotalMAmt As Double = 0
            For dblrows As Integer = 0 To gv1.Rows.Count - 1
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
                For dblcolumns As Integer = 7 To gv1.Columns.Count - 8
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                    TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    obj1.FreshItem_QtyInCrates = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                Else
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                                            If DispatchQty > (CrateConvFactor / 2) Then
                                                dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                                            Else
                                                dblTotalCrateRowWise = 0
                                            End If
                                        End If
                                        TotalCrate = TotalCrate + dblTotalCrateRowWise
                                        obj1.FreshItem_QtyInCrates = dblTotalCrateRowWise
                                    End If
                                End If
                                ''to convert into litre
                                Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                                    Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor_Ltr
                                    dblTotalLitreRowWise = (DispatchQty / CrateConvFactor_Ltr)
                                End If
                                TotalLitre = TotalLitre + dblTotalLitreRowWise
                                obj1.FreshItem_QtyInLitres = dblTotalLitreRowWise
                                ''---------end of litre conversion
                            Else
                                dblTotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                            End If
                            strItemValueExist = "Yes"
                            Dim dt As New DataTable()
                            Dim dblRate As Double = 0
                            Dim qry As String = "select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) & "'"
                            strPriceCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.myLen(strPriceCode) <= 0 Then
                                Throw New Exception("price_CodeNon not found for Customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & "")
                            End If
                            qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & obj1.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & obj1.itemCode & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                ") XXXE WHERE RowNo=1  "
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                If dblRate = 0 Then
                                    Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(obj1.ShortDesc) & Environment.NewLine)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                    obj1.ItemRate = dblRate
                                    dblTotalMAmt = dblTotalMAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                Else
                                    obj1.ItemRate = dblRate
                                    dblTotalPAmt = dblTotalPAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                                End If
                                obj1.ItemRate = dblRate
                                dblTotalDocAmtRowWise = dblTotalDocAmtRowWise + Math.Round(clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * clsCommon.myCdbl(dblRate), 2)
                            Else
                                gv1.Rows(dblrows).Cells(dblcolumns).Value = 0
                                Throw New Exception("Please create Price for Item " & obj1.ShortDesc & " and customer " & clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustName).Value) & " ")
                            End If
                        End If
                    End If
                Next
                gv1.Rows(dblrows).Cells(colCrate).Value = Math.Round(clsCommon.myCdbl(TotalCrate), 2)
                gv1.Rows(dblrows).Cells(colLitre).Value = Math.Round(clsCommon.myCdbl(TotalLitre), 2)
                gv1.Rows(dblrows).Cells(colAmt).Value = clsCommon.myCdbl(dblTotalDocAmtRowWise)
                gv1.Rows(dblrows).Cells(colMAmt).Value = clsCommon.myCdbl(dblTotalMAmt)
                gv1.Rows(dblrows).Cells(colPCount).Value = clsCommon.myCdbl(dblTotalPCount)
                gv1.Rows(dblrows).Cells(colPAmt).Value = clsCommon.myCdbl(dblTotalPAmt)
                gv1.Rows(dblrows).Cells(colItemExist).Value = strItemValueExist
                dblDocTotalAmt = dblDocTotalAmt + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colAmt).Value)
                dblDocTotalLitre = dblDocTotalLitre + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colLitre).Value)
                dblDocTotalCrate = dblDocTotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCrate).Value)
                TotalPAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPAmt).Value)
                TotalPCount += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colPCount).Value)
                TotalMAmt += clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colMAmt).Value)
            Next
            lblTotalCrate.Text = clsCommon.myCdbl(dblDocTotalCrate)
            lblTotalLitre.Text = clsCommon.myCdbl(dblDocTotalLitre)
            txtDocAmt.Text = clsCommon.myCdbl(dblDocTotalAmt)
            lblDocumentAmt.Text = clsCommon.myCdbl(TotalMAmt)
            txtPCount.Text = clsCommon.myCdbl(TotalPCount)
            txtPAmt.Text = clsCommon.myCdbl(TotalPAmt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnUpdateCrateAndAmt_Click(sender As Object, e As EventArgs) Handles btnUpdateCrateAndAmt.Click
        Try
            UpdateAllTotals()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnAssessment_Click(sender As Object, e As EventArgs) Handles btnAssessment.Click
        Dim frm As New frmAssessmentGrid
        frm.IDate = txtDate.Value
        frm.ShowDialog()
    End Sub
    Private Sub txtVehicleNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleNo._MYValidating
        Dim whrcls As String = ""
        Dim qry As String = "Select vehicle_id,Description ,route_no as 'Route No',route_desc as 'Route Name'  from TSPL_VEHICLE_MASTER left join tspl_route_master on tspl_route_master.vehicle_code=TSPL_VEHICLE_MASTER.vehicle_id "
        If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 Then
            whrcls = " tspl_route_master.route_no ='" & txtRouteNo.Value & "' "
        End If
        txtVehicleNo.Value = clsCommon.ShowSelectForm("DBookingVehicle", qry, "vehicle_id", whrcls, txtVehicleNo.Value, "vehicle_id", isButtonClicked)
        lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleNo.Value) + "'"))
    End Sub
    Private Sub chkIndividualCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIndividualCustomer.ToggleStateChanged
        If chkIndividualCustomer.Checked = True Then
            txtCustomerNo.Enabled = True
        Else
            txtCustomerNo.Enabled = False
        End If
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
    Sub setRouteVehicleCityDetail()
        Try
            Dim qry As String = ""
            qry = "select TSPL_ROUTE_MASTER.City_Code,Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon,tspl_transport_master.Transporter_Name from TSPL_CUSTOMER_MASTER left outer join " &
                "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
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
                If clsCommon.myLen(clsCommon.myCstr(txtRouteNo.Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(TxtCity.Value)) > 0 Then
                    setCustomerDetail(TxtCity.Value, txtRouteNo.Value)
                End If
            End If
            RefreshFormName()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gv1_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv1.ViewRowFormatting
        If TypeOf e.RowElement Is GridTableHeaderRowElement Then
            If gv1.Rows.Count > 0 Then
                If clsCommon.myLen(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colCustCode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colShiftName).Value)) > 0 Then
                    Dim strqry As String = "Select count(ShiftType) from (select ShiftType,convert(date,Document_Date ,103) as Document_Date from (select tspl_demand_booking_master.Document_Date , tspl_demand_booking_detail.ShiftType  from tspl_demand_booking_detail
left outer join tspl_demand_booking_master on tspl_demand_booking_detail.document_no=tspl_demand_booking_master.document_no
where convert(date,tspl_demand_booking_master.document_date,103) in ('" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-1)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-3)) & "')
and tspl_demand_booking_detail.Cust_Code='" & clsCommon.myCstr(e.RowElement.RowInfo.Cells(colCustCode).Value) & "' and TSPL_DEMAND_BOOKING_DETAIL .ShiftType ='" & clsCommon.myCstr(e.RowElement.RowInfo.Cells(colShiftName).Value) & "')Final 
group by ShiftType ,convert(date,Document_Date ,103))FinalQry"
                    Dim bookingcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
                    If bookingcount = 3 Then
                        e.RowElement.DrawFill = True
                        e.RowElement.GradientStyle = GradientStyles.Solid
                        e.RowElement.BackColor = Color.LightGreen
                    Else
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                        'e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                    End If
                End If
            End If
        Else
            e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
            e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local)
            e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
        End If
    End Sub
    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        'Dim font_Renamed As Font
        'font_Renamed = New Font(e.RowElement.Font, FontStyle.Bold)
        If gv1.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colCustCode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colShiftName).Value)) > 0 Then
                '                Dim strqry As String = "Select count(ShiftType) from (select ShiftType,convert(date,Document_Date ,103) as Document_Date from (select tspl_demand_booking_master.Document_Date , tspl_demand_booking_detail.ShiftType  from tspl_demand_booking_detail
                'left outer join tspl_demand_booking_master on tspl_demand_booking_detail.document_no=tspl_demand_booking_master.document_no
                'where convert(date,tspl_demand_booking_master.document_date,103) in ('" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-1)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2)) & "','" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-3)) & "')
                'and tspl_demand_booking_detail.Cust_Code='" & clsCommon.myCstr(e.RowElement.RowInfo.Cells(colCustCode).Value) & "' and TSPL_DEMAND_BOOKING_DETAIL .ShiftType ='" & clsCommon.myCstr(e.RowElement.RowInfo.Cells(colShiftName).Value) & "')Final 
                'group by ShiftType ,convert(date,Document_Date ,103))FinalQry"
                '                Dim bookingcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
                If clsCommon.CompairString(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colBookingCreatedFor3Days).Value), "3") = CompairStringResult.Equal Then
                    'End If
                    'If bookingcount = 3 Then
                    e.RowElement.DrawFill = True
                    e.RowElement.GradientStyle = GradientStyles.Solid
                    e.RowElement.BackColor = Color.LightGreen
                    'e.RowElement.Font = font_Renamed
                Else
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                    'e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                    ' e.RowElement.ResetValue(LightVisualElement.FontProperty, font_Renamed)
                    'e.RowElement.BackColor = Color.Black
                End If
            End If
        End If
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Dim desc As String = Nothing
        Dim IsPost As Boolean = False
        Try
            Dim custCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where route_no='" + txtRouteNo.Value + "' and IsDistributor='Y'"))
            If (myMessages.postConfirm()) Then
                If checkCreditLimit Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("select Credit_Customer from TSPL_CUSTOMER_MASTER where Cust_Code='" + custCode + "'"), "N") = CompairStringResult.Equal Then
                        Dim OsBal As Decimal = GetOutStandingBal(custCode, txtDate.Value)
                        If OsBal > clsCommon.myCdbl(txtDocAmt.Text) Then
                            IsPost = True
                        Else
                            IsPost = False
                            If clsCommon.myLen(custCode) > 0 Then
                                Dim count As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where Program_Code='" + clsUserMgtCode.frmDemandBooking + "' and Document_No='" + txtDocNo.Value + "' and Cust_Code='" + custCode + "'")
                                If count = 0 Then
                                    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code,Cust_Code,Loc_Code) " &
                                      "values ('Demand Booking','" & clsUserMgtCode.frmDemandBooking & "','" & txtDocNo.Value & "', " &
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
                                common.clsCommon.MyMessageBoxShow(Me, "Distributor  Not Found!", Me.Text)
                            End If
                        End If
                    Else
                        IsPost = True
                    End If
                Else
                    IsPost = True
                End If
                If IsPost Then
                    If rbtnMorningEveningBoth.IsChecked Then
                        Throw New Exception("Please select morning/evening")
                    End If
                    If (clsDemandBookingSale.PostData(MyBase.Form_ID, txtDocNo.Value, IIf(rbtnMorning.IsChecked, 1, 2))) Then
                        msg = "Successfully posted"
                        common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                        If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandAll, clsFixedParameterCode.ApplyDemandAll, Nothing)) = 1 Or clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                            SaveData(1)
                        End If
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            msg = Nothing
            qry = Nothing
            dt = Nothing
            desc = Nothing
        End Try
    End Sub
    Private Sub rbtnEvening_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnEvening.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtnMorningEveningBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnMorningEveningBoth.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtn_Fresh_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtn_Fresh.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
            txtPCount.Text = ""
            txtPAmt.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rdbnBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbnFreshAmbientBoth.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                HideUnhideRowsAndColumnsOFGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Btn_TruckSheet_Click(sender As Object, e As EventArgs) Handles btn_TruckSheet.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select document", Me.Text)
                txtDocNo.Focus()
                Exit Sub
            End If
            If rbtnMorningEveningBoth.IsChecked = True Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Morning/Evening Shift", Me.Text)
                rbtnMorning.Focus()
                Exit Sub
            End If
            clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_DETAIL set IsTruckSheetGenerated='Y' where document_no='" & txtDocNo.Value & "' and ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'")
            '    Dim Qry As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin
            ',TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc
            ',Main_Final.shiftType,Main_Final.City_Name,Main_Final.Demand_No,Main_Final.Demand_Date,Main_Final.Route_No,Main_Final.Route_Desc
            ',Main_Final.cust_code,Main_Final.Customer_Name_Hindi,Main_Final.Crate_Qty,Main_Final.Milk_Amt,Main_Final.Product_Amt,Main_Final.Total_Amt
            'from (select 
            'max(TSPL_DEMAND_BOOKING_MASTER.shiftType) as shiftType,
            'max(TSPL_city_MASTER.City_Name) as City_Name,
            'max(TSPL_DEMAND_BOOKING_MASTER.Comp_Code) as Comp_Code,
            'max(TSPL_DEMAND_BOOKING_MASTER.location_code) as location_code
            ',TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,max(convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Demand_Date ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,max(isnull(TSPL_ROUTE_MASTER.Route_Desc,'')) as Route_Desc 
            ' ,TSPL_DEMAND_BOOKING_DETAIL.cust_code,max(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi) as Customer_Name_Hindi
            ',sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) AS Crate_Qty
            ',sum(case when TSPL_ITEM_MASTER.Is_Milk_Pouch=1 then TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount else 0 end) as Milk_Amt
            ',sum(case when TSPL_ITEM_MASTER.Is_Milk_Pouch=0 then TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount else 0 end) as Product_Amt
            ',sum(TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount) AS Total_Amt 
            'from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
            'on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
            ' left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
            'left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
            ' left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
            'left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            'left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
            'WHERE TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
            ' group by TSPL_DEMAND_BOOKING_DETAIL.cust_code,TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Route_No
            ') as Main_Final
            'LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code
            ' LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Main_Final.location_code"
            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            '    If dt.Rows.Count > 0 Then
            '        Dim frmCRV As New frmCrystalReportViewer()
            '        frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptTruckSheetPrint", "Truck Sheet", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            '        frmCRV = Nothing
            '    End If
            'Dim Qry As String = "select TSPL_DEMAND_BOOKING_MASTER.TripNo,
            '    '" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' shiftType,
            '    TSPL_city_MASTER.City_Name,
            '    TSPL_DEMAND_BOOKING_MASTER.Comp_Code,
            '    TSPL_DEMAND_BOOKING_MASTER.location_code
            '    ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Demand_Date 
            '    ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc 
            '    ,TSPL_VENDOR_MASTER.vendor_name as Distributor 
            '    ,TSPL_DEMAND_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.Customer_Name_Hindi
            '    ,TSPL_DEMAND_BOOKING_DETAIL.Qty
            '    ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise
            '    ,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
            '    ,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
            '    ,TSPL_DEMAND_BOOKING_DETAIL.unit_code
            '    ,TSPL_ITEM_MASTER.Alies_Name_Hindi as Item_Alies_Name_Hindi 
            '    ,case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end as CrateQty
            '    ,case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end as PouchQty
            '    ,N'क्रेट' as Crate,N'क्रेट    थैली' as Pouch
            '    from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
            '    on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
            '     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
            '    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
            '     left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
            '    left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            '    left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
            '    left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
            '    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_DEMAND_BOOKING_MASTER.location_code
            '    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VEHICLE_MASTER.Transport_Id
            '    WHERE TSPL_ITEM_MASTER.Is_Milk_Pouch=1
            '    and TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count > 0 Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptTruckSheetPrintNew", "Truck Sheet", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            '    frmCRV = Nothing
            'End If
            'TruckSheetExcel()
            'TruckPDF()
            btn_TruckSheet.Enabled = False
            btn_TSCancel.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub TruckSheetExcel()
        Dim GVTruckSheet As New RadGridView()
        Me.Controls.Add(GVTruckSheet)
        Try
            Dim ItemInUse As String = " TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
                On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                where TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
                and TSPL_ITEM_MASTER.Is_Milk_Pouch=1 order by sku_seq"
            Dim ItemInUseProduct As String = " TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
                On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                where TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
                and TSPL_ITEM_MASTER.Is_Milk_Pouch=0 order by sku_seq"
            Dim dtDataExist As DataTable = clsDBFuncationality.GetDataTable("select distinct isnull(TSPL_ITEM_MASTER.Alies_Name_Hindi,'')  Alies_Name,sku_seq,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS Size from " + ItemInUse)
            Dim dtDataExistProduct As DataTable = clsDBFuncationality.GetDataTable("select distinct isnull(TSPL_ITEM_MASTER.Alies_Name_Hindi,'')  Alies_Name,sku_seq,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS Size from " + ItemInUseProduct)
            If (dtDataExist Is Nothing OrElse dtDataExist.Rows.Count = 0) AndAlso (dtDataExistProduct Is Nothing OrElse dtDataExistProduct.Rows.Count = 0) Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            Dim strItemC As String = ""
            Dim strItemP As String = ""
            Dim strItemL As String = ""
            Dim strItemA As String = ""
            Dim strProdQ As String = ""
            Dim strItemSUM As String = ""
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    If i = 0 Then
                        strItemC += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemC += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                Next
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    If clsCommon.CompairString(strItemSUM, "") = CompairStringResult.Equal Then
                        strItemA += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemA += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                    If clsCommon.CompairString(strProdQ, "") = CompairStringResult.Equal Then
                        strProdQ += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    Else
                        strProdQ += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    End If
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    If i = 0 Then
                        strItemC += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemC += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    If clsCommon.CompairString(strItemSUM, "") = CompairStringResult.Equal Then
                        strItemA += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemA += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                    If clsCommon.CompairString(strProdQ, "") = CompairStringResult.Equal Then
                        strProdQ += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    Else
                        strProdQ += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    End If
                Next
            End If
            Dim Qry As String = "select max(customer_Name) as Agents
        , " + strItemSUM + "
                ,sum(isnull(TotalLtr_CustWise,0)) as [Milk In Ltr] 
        ,sum(isnull(TotalCrates_ItemWise,0)) as [Crates]
        ,sum(isnull(MAmt,0)) as [Milk Amount]
        ,sum(isnull(PQty,0)) as [Product Quantity]
        ,sum(isnull(PAmt,0)) as [Product Amount]
        from   
        (Select  max(Display_Seq) as Display_Seq,Cust_Code,max(Customer_Name) as Customer_Name,Item_Code,max(Alies_Name)+'#C' as Alies_Name#C
        ,max(Alies_Name)+'#P' AS Alies_Name#P
		,max(Alies_Name)+'#L' AS Alies_Name#L,max(Alies_Name)+'#A' AS Alies_Name#A
        ,max(Alies_Name)+'#ProdQ' AS Alies_Name#ProdQ
		,max(Unit_Desc) as Unit_Desc,max(Code) as Code
        ,sum(Qty_Crate) as Qty_Crate, sum (Qty_Pouch) as Qty_Pouch
        ,sum(TotalLtr_ItemWise) as TotalLtr_CustWise 
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise
		,sum(ItemNetAmount) as TotalAmt_ItemWise
        ,SUM(TotalCrates_ItemWise) AS TotalCrates_ItemWise
        ,sum(ProdQ) as ProdQ
        ,SUM(MAmt) AS MAmt
        ,SUM(PQty) AS PQty
        ,SUM(PAmt) AS PAmt
        from (   Select '1' as  Code,TSPL_CUSTOMER_MASTER.Display_Seq,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name 
	, TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name_Hindi as Alies_Name
	,TSPL_UNIT_MASTER.Unit_Desc
	,(CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Crate' THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as Qty_Crate
	,(CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as Qty_Pouch
	,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
    ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise
    ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
	,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
    ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty
	,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	 from TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
     On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
     Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
	left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
	left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
    WHERE TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'  ) XXXFirst Group By
    XXXFirst.Cust_Code,	XXXFirst.Item_Code  ) 
    as s "
            If rdbnFreshAmbientBoth.IsChecked = True Then
                Qry += "  pivot (  sum(Qty_Crate) for Alies_Name#C in (" + strItemC + " ) ) as zpivot 
    pivot (  sum(Qty_Pouch) for Alies_Name#P in (" + strItemP + " )  ) as zpivot1
    pivot (  sum(TotalLtr_ItemWise) for Alies_Name#L in (" + strItemL + ") ) as zpivotLtr_ItemWise
    pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise "
                If clsCommon.myLen(strProdQ) > 0 Then
                    Qry += " pivot (  sum(ProdQ) for Alies_Name#ProdQ in (" + strProdQ + ") ) as zpivotProdQ "
                End If
            ElseIf rbtn_Fresh.IsChecked = True Then
                Qry += "  pivot (  sum(Qty_Crate) for Alies_Name#C in (" + strItemC + " ) ) as zpivot 
    pivot (  sum(Qty_Pouch) for Alies_Name#P in (" + strItemP + " )  ) as zpivot1
    pivot (  sum(TotalLtr_ItemWise) for Alies_Name#L in (" + strItemL + ") ) as zpivotLtr_ItemWise
    pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise "
            ElseIf rbtn_Ambient.IsChecked = True Then
                Qry += " pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise
    pivot (  sum(ProdQ) for Alies_Name#ProdQ in (" + strProdQ + ") ) as zpivotProdQ "
            End If
            Qry += "  group by Cust_Code,Display_Seq order by Display_Seq"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'Total Row
            Dim newTotalRow As DataRow = dt.NewRow
            newTotalRow("Agents") = "Net Total"
            Dim newTotalLtrRow As DataRow = dt.NewRow
            If rdbnFreshAmbientBoth.IsChecked = True OrElse rbtn_Fresh.IsChecked = True Then
                newTotalLtrRow("Agents") = "Total Qty(Ltr)"
            End If
            Dim newTotalAmtRow As DataRow = dt.NewRow
            newTotalAmtRow("Agents") = "Total Amt"
            For i As Integer = 1 To dt.Columns.Count - 1
                Dim ColName As String = dt.Columns(i).ColumnName
                newTotalRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColName + "])", ""))
                If ColName.Contains("#C") Then
                    Dim ColNameLtr As String = ColName.Substring(0, ColName.Length - 2) + "#L"
                    newTotalLtrRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameLtr + "])", ""))
                    Dim ColNameAmt As String = ColName.Substring(0, ColName.Length - 2) + "#A"
                    newTotalAmtRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameAmt + "])", ""))
                ElseIf ColName.Contains("#ProdQ") Then
                    Dim ColNameAmt As String = ColName.Substring(0, ColName.Length - 6) + "#A"
                    newTotalAmtRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameAmt + "])", ""))
                End If
            Next
            dt.Rows.Add(newTotalRow)
            If rdbnFreshAmbientBoth.IsChecked = True OrElse rbtn_Fresh.IsChecked = True Then
                dt.Rows.Add(newTotalLtrRow)
            End If
            dt.Rows.Add(newTotalAmtRow)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                GVTruckSheet.DataSource = dt
            End If
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "क्रेट"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "थैली"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L").IsVisible = False
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderText = clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Size")) + Environment.NewLine + " "
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "क्रेट"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "थैली"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L").IsVisible = False
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderText = clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Size")) + Environment.NewLine + " "
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderTextAlignment = ContentAlignment.TopRight
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            End If
            GVTruckSheet.Columns("Milk In Ltr").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Crates").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Milk Amount").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Product Quantity").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Product Amount").FormatString = "{0:n2}"
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Agents").Name)
            Dim TempColGroupCount As Integer = 1
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For Each dr As DataRow In dtDataExist.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#C").Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#P").Name)
                    TempColGroupCount += 1
                Next
                For Each dr As DataRow In dtDataExistProduct.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#ProdQ").Name)
                    TempColGroupCount += 1
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For Each dr As DataRow In dtDataExist.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#C").Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#P").Name)
                    TempColGroupCount += 1
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For Each dr As DataRow In dtDataExistProduct.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(dr("Alies_Name") + "#ProdQ").Name)
                    TempColGroupCount += 1
                Next
            End If
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Milk In Ltr").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Crates").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Milk Amount").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Product Quantity").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Product Amount").Name)
            GVTruckSheet.ViewDefinition = view
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy")))
            arrHeader.Add("Route : " & lblRouteDesc.Text)
            arrHeader.Add("City : " & lblCityName.Text)
            arrHeader.Add("Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
            arrHeader.Add("Distributor : " & lblTransporterName.Text)
            arrHeader.Add("Trip : " & clsCommon.myCstr(txtTripNo.Text))
            transportSql.exportdata(GVTruckSheet, "", "Truck Sheet", , arrHeader, False, False, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(GVTruckSheet)
        End Try
    End Sub
    Private Sub TruckSheetPDF()
        Dim GVTruckSheet As New RadGridView()
        Me.Controls.Add(GVTruckSheet)
        Dim doc As New clsMyPrintDocument()
        Try
            Dim ItemInUse As String = " TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
                On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                where TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
                and TSPL_ITEM_MASTER.Is_Milk_Pouch=1 order by sku_seq"
            Dim ItemInUseProduct As String = " TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
                On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                where TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
                and TSPL_ITEM_MASTER.Is_Milk_Pouch=0 order by sku_seq"
            Dim dtDataExist As DataTable = clsDBFuncationality.GetDataTable("select distinct isnull(TSPL_ITEM_MASTER.Alies_Name_Hindi,'')  Alies_Name,sku_seq,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS Size from " + ItemInUse)
            Dim dtDataExistProduct As DataTable = clsDBFuncationality.GetDataTable("select distinct isnull(TSPL_ITEM_MASTER.Alies_Name_Hindi,'')  Alies_Name,sku_seq,SUBSTRING(TSPL_ITEM_MASTER.Alies_Name, LEN(TSPL_ITEM_MASTER.Alies_Name) -  CHARINDEX(' ', REVERSE(TSPL_ITEM_MASTER.Alies_Name))+2,LEN(TSPL_ITEM_MASTER.Alies_Name)) AS Size from " + ItemInUseProduct)
            If (dtDataExist Is Nothing OrElse dtDataExist.Rows.Count = 0) AndAlso (dtDataExistProduct Is Nothing OrElse dtDataExistProduct.Rows.Count = 0) Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            Dim strItemC As String = ""
            Dim strItemP As String = ""
            Dim strItemL As String = ""
            Dim strItemA As String = ""
            Dim strProdQ As String = ""
            Dim strItemSUM As String = ""
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    If i = 0 Then
                        strItemC += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemC += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                Next
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    If clsCommon.CompairString(strItemSUM, "") = CompairStringResult.Equal Then
                        strItemA += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemA += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                    If clsCommon.CompairString(strProdQ, "") = CompairStringResult.Equal Then
                        strProdQ += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    Else
                        strProdQ += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    End If
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    If i = 0 Then
                        strItemC += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += "[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemC += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]"
                        strItemP += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]"
                        strItemL += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]"
                        strItemA += ",[" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L]
                    ,sum([" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    If clsCommon.CompairString(strItemSUM, "") = CompairStringResult.Equal Then
                        strItemA += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += "sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    Else
                        strItemA += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                        strItemSUM += ",sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]
                    ,sum([" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]) as [" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A]"
                    End If
                    If clsCommon.CompairString(strProdQ, "") = CompairStringResult.Equal Then
                        strProdQ += "[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    Else
                        strProdQ += ",[" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ]"
                    End If
                Next
            End If
            Dim Qry As String = "select Cust_Code as AgentCode,max(customer_Name) as Agents
        , " + strItemSUM + "
                ,sum(isnull(TotalLtr_CustWise,0)) as [Milk In Ltr] 
        ,floor(sum(isnull(TotalCrates_ItemWise,0))) as [Crates]
        ,sum(isnull(MAmt,0)) as [Milk Amount]
        ,floor(sum(isnull(PQty,0))) as [Product Quantity]
        ,sum(isnull(PAmt,0)) as [Product Amount]
        from   
        (Select  max(Display_Seq) as Display_Seq,Cust_Code,max(Customer_Name) as Customer_Name,Item_Code,max(Alies_Name)+'#C' as Alies_Name#C
        ,max(Alies_Name)+'#P' AS Alies_Name#P
		,max(Alies_Name)+'#L' AS Alies_Name#L,max(Alies_Name)+'#A' AS Alies_Name#A
        ,max(Alies_Name)+'#ProdQ' AS Alies_Name#ProdQ
		,max(Unit_Desc) as Unit_Desc,max(Code) as Code
        ,floor(sum(Qty_Crate)) as Qty_Crate, floor(sum (Qty_Pouch)) as Qty_Pouch
        ,sum(TotalLtr_ItemWise) as TotalLtr_CustWise 
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise
		,sum(ItemNetAmount) as TotalAmt_ItemWise
        ,SUM(TotalCrates_ItemWise) AS TotalCrates_ItemWise
        ,floor(sum(ProdQ)) as ProdQ
        ,SUM(MAmt) AS MAmt
        ,SUM(PQty) AS PQty
        ,SUM(PAmt) AS PAmt
        from (   Select '1' as  Code,TSPL_CUSTOMER_MASTER.Display_Seq,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name 
	, TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name_Hindi as Alies_Name
	,TSPL_UNIT_MASTER.Unit_Desc
	,(CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Crate' THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE NULL END) as Qty_Crate
	,(CASE WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE NULL END) as Qty_Pouch
	,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
    ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise
    ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
	,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
    ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty
	,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	 from TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL
     On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
     Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
	left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
	left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
    WHERE TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'  ) XXXFirst Group By
    XXXFirst.Cust_Code,	XXXFirst.Item_Code  ) 
    as s "
            If rdbnFreshAmbientBoth.IsChecked = True Then
                Qry += "  pivot (  sum(Qty_Crate) for Alies_Name#C in (" + strItemC + " ) ) as zpivot 
    pivot (  sum(Qty_Pouch) for Alies_Name#P in (" + strItemP + " )  ) as zpivot1
    pivot (  sum(TotalLtr_ItemWise) for Alies_Name#L in (" + strItemL + ") ) as zpivotLtr_ItemWise
    pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise "
                If clsCommon.myLen(strProdQ) > 0 Then
                    Qry += " pivot (  sum(ProdQ) for Alies_Name#ProdQ in (" + strProdQ + ") ) as zpivotProdQ "
                End If
            ElseIf rbtn_Fresh.IsChecked = True Then
                Qry += "  pivot (  sum(Qty_Crate) for Alies_Name#C in (" + strItemC + " ) ) as zpivot 
    pivot (  sum(Qty_Pouch) for Alies_Name#P in (" + strItemP + " )  ) as zpivot1
    pivot (  sum(TotalLtr_ItemWise) for Alies_Name#L in (" + strItemL + ") ) as zpivotLtr_ItemWise
    pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise "
            ElseIf rbtn_Ambient.IsChecked = True Then
                Qry += " pivot (  sum(TotalAmt_ItemWise) for Alies_Name#A in (" + strItemA + ") ) as zpivotAmt_ItemWise
    pivot (  sum(ProdQ) for Alies_Name#ProdQ in (" + strProdQ + ") ) as zpivotProdQ "
            End If
            Qry += "  group by Cust_Code,Display_Seq order by Display_Seq"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'Total Row
            Dim newTotalRow As DataRow = dt.NewRow
            newTotalRow("Agents") = "Net Total"
            Dim newTotalLtrRow As DataRow = dt.NewRow
            If rdbnFreshAmbientBoth.IsChecked = True OrElse rbtn_Fresh.IsChecked = True Then
                newTotalLtrRow("Agents") = "Total Qty(Ltr)"
            End If
            Dim newTotalAmtRow As DataRow = dt.NewRow
            newTotalAmtRow("Agents") = "Total Amt"
            For i As Integer = 2 To dt.Columns.Count - 1
                Dim ColName As String = dt.Columns(i).ColumnName
                newTotalRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColName + "])", ""))
                If ColName.Contains("#C") Then
                    Dim ColNameLtr As String = ColName.Substring(0, ColName.Length - 2) + "#L"
                    newTotalLtrRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameLtr + "])", ""))
                    Dim ColNameAmt As String = ColName.Substring(0, ColName.Length - 2) + "#A"
                    newTotalAmtRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameAmt + "])", ""))
                ElseIf ColName.Contains("#ProdQ") Then
                    Dim ColNameAmt As String = ColName.Substring(0, ColName.Length - 6) + "#A"
                    newTotalAmtRow(ColName) = clsCommon.myCdbl(dt.Compute("sum([" + ColNameAmt + "])", ""))
                End If
            Next
            dt.Rows.Add(newTotalRow)
            If rdbnFreshAmbientBoth.IsChecked = True OrElse rbtn_Fresh.IsChecked = True Then
                dt.Rows.Add(newTotalLtrRow)
            End If
            dt.Rows.Add(newTotalAmtRow)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                GVTruckSheet.DataSource = dt
            End If
            GVTruckSheet.Columns("AgentCode").HeaderText = "Code"
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").FormatString = "{0:n2}"
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "क्रेट"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "थैली"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L").IsVisible = False
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderText = clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Size")) + Environment.NewLine + " "
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For i As Integer = 0 To dtDataExist.Rows.Count - 1
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").FormatString = "{0:n2}"
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "क्रेट"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#C").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderText = clsCommon.myCstr(dtDataExist.Rows(i).Item("Size")) + Environment.NewLine + "थैली"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#P").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#L").IsVisible = False
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExist.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For i As Integer = 0 To dtDataExistProduct.Rows.Count - 1
                    'GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").FormatString = "{0:n2}"
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderText = clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Size")) + Environment.NewLine + " "
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#ProdQ").HeaderTextAlignment = ContentAlignment.TopLeft
                    GVTruckSheet.Columns("" + clsCommon.myCstr(dtDataExistProduct.Rows(i).Item("Alies_Name")) + "#A").IsVisible = False
                Next
            End If
            GVTruckSheet.Columns("Milk In Ltr").FormatString = "{0:n2}"
            'GVTruckSheet.Columns("Crates").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Milk Amount").FormatString = "{0:n2}"
            'GVTruckSheet.Columns("Product Quantity").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Product Amount").FormatString = "{0:n2}"
            GVTruckSheet.Columns("Milk Amount").HeaderText = "Milk Amt"
            GVTruckSheet.Columns("Product Quantity").HeaderText = "Prod Qty"
            GVTruckSheet.Columns("Product Amount").HeaderText = "Prod Amt"
            'GVTruckSheet.Columns("Milk In Ltr").Width = 50
            'GVTruckSheet.Columns("Crates").Width = 25
            'GVTruckSheet.Columns("Milk Amount").Width = 50
            'GVTruckSheet.Columns("Product Quantity").Width = 50
            'GVTruckSheet.Columns("Product Amount").Width = 50
            'GVTruckSheet.MasterTemplate.BestFitColumns()
            If rbtn_Fresh.IsChecked = True Then
                GVTruckSheet.Columns("Milk Amount").IsVisible = False
                GVTruckSheet.Columns("Product Quantity").IsVisible = False
                GVTruckSheet.Columns("Product Amount").IsVisible = False
            ElseIf rbtn_Ambient.IsChecked = True Then
                GVTruckSheet.Columns("Milk Amount").IsVisible = False
                GVTruckSheet.Columns("Milk In Ltr").IsVisible = False
                GVTruckSheet.Columns("Crates").IsVisible = False
                GVTruckSheet.Columns("Product Amount").IsVisible = False
            End If
            For I As Int16 = 0 To GVTruckSheet.Columns.Count - 1
                GVTruckSheet.Columns(I).WrapText = True
                'GVTruckSheet.Columns(I).BestFit()
            Next
            GVTruckSheet.Columns("Agents").Width = 100
            GVTruckSheet.Columns("Agents").HeaderTextAlignment = ContentAlignment.TopLeft
            GVTruckSheet.Columns("AgentCode").HeaderTextAlignment = ContentAlignment.TopLeft
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("AgentCode").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Agents").Name)
            Dim TempColGroupCount As Integer = 1
            If rdbnFreshAmbientBoth.IsChecked = True Then
                For Each dr As DataRow In dtDataExist.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    Dim groupRow = New GridViewColumnGroupRow()
                    groupRow.MinHeight = 30
                    'view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows.Add(groupRow)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#C").Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#P").Name)
                    TempColGroupCount += 1
                Next
                For Each dr As DataRow In dtDataExistProduct.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    Dim groupRow = New GridViewColumnGroupRow()
                    groupRow.MinHeight = 30
                    'view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(TempColGroupCount).Rows.Add(groupRow)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#ProdQ").Name)
                    TempColGroupCount += 1
                Next
            ElseIf rbtn_Fresh.IsChecked = True Then
                For Each dr As DataRow In dtDataExist.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    'view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    Dim groupRow = New GridViewColumnGroupRow()
                    groupRow.MinHeight = 30
                    view.ColumnGroups(TempColGroupCount).Rows.Add(groupRow)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#C").Name)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#P").Name)
                    TempColGroupCount += 1
                Next
            ElseIf rbtn_Ambient.IsChecked = True Then
                For Each dr As DataRow In dtDataExistProduct.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(dr("Alies_Name"))))
                    view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                    'Dim groupRow = New GridViewColumnGroupRow()
                    'groupRow.MinHeight = 30
                    'view.ColumnGroups(TempColGroupCount).Rows.Add(groupRow)
                    view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns(clsCommon.myCstr(dr("Alies_Name")) + "#ProdQ").Name)
                    TempColGroupCount += 1
                Next
            End If
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Milk In Ltr").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Crates").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Milk Amount").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Product Quantity").Name)
            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(GVTruckSheet.Columns("Product Amount").Name)
            GVTruckSheet.ViewDefinition = view
            'doc.HeaderHeight = 60
            'doc.Landscape = True
            'doc.AssociatedObject = GVTruckSheet
            'doc.HeaderFont = New Font("Arial", 8)
            'doc.LeftUpperText = "Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy"))
            'doc.LeftUpperFont = New Font("Arial", 8)
            'doc.MiddleHeader = "City : " & lblCityName.Text
            'doc.RightHeader = "Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening")
            'doc.LeftMiddleText = "Route : " & lblRouteDesc.Text
            'doc.LeftMiddleFont = New Font("Arial", 8)
            'doc.LeftLowerText = "Distributor : " & lblTransporterName.Text & " || " & "Trip : " & clsCommon.myCstr(txtTripNo.Text)
            'doc.LeftLowerFont = New Font("Arial", 8)
            doc.Margins.Top = 50
            doc.Margins.Bottom = 50
            doc.Margins.Left = 50
            doc.Margins.Right = 50
            doc.HeaderHeight = 90
            doc.Landscape = True
            doc.AssociatedObject = GVTruckSheet
            'Dim strHeader As String = "Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy"))
            'strHeader += "  Route : " & lblRouteDesc.Text
            'strHeader += "  City : " & lblCityName.Text
            'strHeader += "  Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening")
            'strHeader += "  Distributor : " & lblTransporterName.Text
            'strHeader += "  Trip : " & clsCommon.myCstr(txtTripNo.Text)
            Dim strHeader As String = "Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy"))
            Dim strHeader2 As String = "Route : " & lblRouteDesc.Text
            strHeader2 += " City : " & lblCityName.Text
            strHeader2 += " Distributor : " & lblTransporterName.Text
            strHeader2 += " Trip : " & clsCommon.myCstr(txtTripNo.Text)
            strHeader2 += " Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening")
            doc.LeftUpperText = strHeader
            doc.LeftHeader = strHeader2
            doc.LeftUpperFont = New Font("Arial", 16, FontStyle.Bold)
            doc.HeaderFont = New Font("Arial", 16, FontStyle.Bold)
            doc.AssociatedObject = GVTruckSheet
            doc.Print()
            doc = Nothing
        Catch ex As Exception
            doc = Nothing
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(GVTruckSheet)
        End Try
    End Sub
    Private Sub Btn_Gatepass_Click(sender As Object, e As EventArgs) Handles btn_Gatepass.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select document", Me.Text)
                txtDocNo.Focus()
                Exit Sub
            End If
            If rbtnMorningEveningBoth.IsChecked = True Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Morning/Evening Shift", Me.Text)
                rbtnMorning.Focus()
                Exit Sub
            End If
            clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_DETAIL set IsGatePassGenerated='Y' where document_no='" & txtDocNo.Value & "' and ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'")
            'Dim Qry As String = "select TSPL_DEMAND_BOOKING_MASTER.TripNo,
            '    TSPL_DEMAND_BOOKING_MASTER.shiftType,
            '    TSPL_city_MASTER.City_Name,
            '    TSPL_DEMAND_BOOKING_MASTER.Comp_Code,
            '    TSPL_DEMAND_BOOKING_MASTER.location_code
            '    ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Demand_Date 
            '    ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc 
            '     ,TSPL_DEMAND_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.Customer_Name_Hindi
            '    ,TSPL_DEMAND_BOOKING_DETAIL.Qty
            '    ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise
            '    ,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
            '    ,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
            '    ,TSPL_DEMAND_BOOKING_DETAIL.unit_code
            '    ,TSPL_ITEM_MASTER.Alies_Name_Hindi as Item_Alies_Name_Hindi 
            '    ,case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end as CrateQty
            '    ,case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end as PouchQty
            '    ,N'क्रेट' as Crate,N'क्रेट    थैली' as Pouch
            '    from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
            '    on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
            '     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
            '    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
            '     left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
            '    left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            '    left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
            '    left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
            '    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_DEMAND_BOOKING_MASTER.location_code
            '    WHERE TSPL_ITEM_MASTER.Is_Milk_Pouch=1
            '    and TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count > 0 Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            '    frmCRV = Nothing
            'End If
            'Dim Qry As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin
            '      ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc
            '      ,Main_Final.Distributor,'" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' shiftType,Main_Final.City_Name,Main_Final.Demand_No,Main_Final.Demand_Date,Main_Final.Route_No,Main_Final.Route_Desc
            '      ,Main_Final.Item_alies_name,Main_Final.Crate_Qty,Main_Final.Pouch_Qty,Main_Final.Loose_Qty,TotalLtr_ItemWise
            '      from (select max(TSPL_VENDOR_MASTER.vendor_name) as Distributor,
            '      max(TSPL_DEMAND_BOOKING_MASTER.shiftType) as shiftType,
            '      max(TSPL_city_MASTER.City_Name) as City_Name,
            '      max(TSPL_DEMAND_BOOKING_MASTER.Comp_Code) as Comp_Code,
            '      max(TSPL_DEMAND_BOOKING_MASTER.location_code) as location_code
            '      ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,max(convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Demand_Date ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,max(isnull(TSPL_ROUTE_MASTER.Route_Desc,'')) as Route_Desc 
            '      ,max(TSPL_ITEM_MASTER.alies_name) as Item_alies_name
            '      ,sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Crate_Qty
            ',sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Pouch_Qty
            ',sum(case when (TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Crate' and TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Pouch') then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Loose_Qty
            ',sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) AS TotalLtr_ItemWise
            '      from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
            '      on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
            '       left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
            '      left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
            '       left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
            '      left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            '      left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
            '      left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VEHICLE_MASTER.Transport_Id
            '      WHERE TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
            '       group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Route_No
            '      ) as Main_Final
            '      LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code
            '       LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Main_Final.location_code"
            PrintGatePass("DB", txtDocNo.Value, IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count > 0 Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePassItemWise", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            '    frmCRV = Nothing
            'End If
            btn_Gatepass.Enabled = False
            btn_GPCancel.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Sub PrintGatePass(ByVal StrFormType As String, ByVal StrDocCode As String, ByVal StrShift As String)
        'clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_DETAIL set IsGatePassGenerated='Y' where " + IIf(StrFormType = "DB", "TSPL_DEMAND_BOOKING_DETAIL.Document_No", "TSPL_DEMAND_BOOKING_DETAIL.GPCode") + "'='" & StrDocCode & "' and ShiftType='" & StrShift & "'")
        Dim Qry As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin
                  ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc
                  ,Main_Final.Distributor,'" & StrShift & "' shiftType,Main_Final.City_Name,Main_Final.Demand_No,Main_Final.Demand_Date,Main_Final.Route_No,Main_Final.Route_Desc ,Main_Final.Vehicle_Desc
                  ,Main_Final.Item_alies_name,Main_Final.Crate_Qty,Main_Final.Pouch_Qty,Main_Final.Loose_Qty,TotalLtr_ItemWise,ItemNetAmount
                  ,Main_Final.Production_Remarks
                  from (select max(TSPL_VENDOR_MASTER.vendor_name) as Distributor,
                  max(TSPL_DEMAND_BOOKING_MASTER.shiftType) as shiftType,
                  max(TSPL_city_MASTER.City_Name) as City_Name,
                  max(TSPL_DEMAND_BOOKING_MASTER.Comp_Code) as Comp_Code,
                  max(TSPL_DEMAND_BOOKING_MASTER.location_code) as location_code
                  ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,max(convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Demand_Date ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,max(isnull(TSPL_ROUTE_MASTER.Route_Desc,'')) as Route_Desc,
                  max(isnull(TSPL_VEHICLE_MASTER.Description,'')) as Vehicle_Desc ,max(TSPL_ITEM_MASTER.alies_name) as Item_alies_name
                  ,sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Crate_Qty
            ,sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Pouch_Qty
            ,sum(case when (TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Crate' and TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Pouch') then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Loose_Qty
            ,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) AS TotalLtr_ItemWise
                   ,sum(TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount) AS ItemNetAmount
                  ,max(TSPL_DEMAND_BOOKING_DETAIL.Production_Remarks) as Production_Remarks
                  from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
                  on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                   left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
                  left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                  left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
                  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VEHICLE_MASTER.Transport_Id
                  WHERE " + IIf(StrFormType = "DB", "TSPL_DEMAND_BOOKING_DETAIL.Document_No", "TSPL_DEMAND_BOOKING_DETAIL.GPCode") + " = '" + StrDocCode + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & StrShift & "'
                   group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Route_No
                  ) as Main_Final
                  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code
                   LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Main_Final.location_code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePassItemWise", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            frmCRV = Nothing
        End If
    End Sub
    Private Sub Btn_GPCancel_Click(sender As Object, e As EventArgs) Handles btn_GPCancel.Click
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0 Then
                If rbtnMorningEveningBoth.IsChecked = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Morning/Evening Shift", Me.Text)
                    rbtnMorning.Focus()
                    Exit Sub
                End If
                Dim qry As String = ""
                Dim StrGatePassCode As String = ""
                StrGatePassCode = clsDBFuncationality.getSingleValue("select max(GPCode) as GPCode from TSPL_DEMAND_BOOKING_DETAIL where Document_no='" & txtDocNo.Value & "'")
                If clsCommon.myLen(StrGatePassCode) > 0 Then
                    qry = "delete from TSPL_DAIRYSALE_GATEPASS_DETAIL where GPCode='" + StrGatePassCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    qry = "delete from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode='" + StrGatePassCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                Dim StrQry As String = "update TSPL_DEMAND_BOOKING_DETAIL set IsGatePassGenerated='N',GPCode='' where document_no='" & txtDocNo.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'"
                If clsDBFuncationality.ExecuteNonQuery(StrQry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Gate Pass Cancel Successfully", Me.Text)
                    btn_Gatepass.Enabled = True
                    btn_GPCancel.Enabled = False
                    'LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub Btn_TSCancel_Click(sender As Object, e As EventArgs) Handles btn_TSCancel.Click
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0 Then
                If rbtnMorningEveningBoth.IsChecked = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Morning/Evening Shift", Me.Text)
                    rbtnMorning.Focus()
                    Exit Sub
                End If
                Dim StrQry As String = "update TSPL_DEMAND_BOOKING_DETAIL set IsTruckSheetGenerated='N' where document_no='" & txtDocNo.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'"
                If clsDBFuncationality.ExecuteNonQuery(StrQry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Truck Sheet Cancel Successfully", Me.Text)
                    btn_TruckSheet.Enabled = True
                    SplitButtonTruckSheet.Enabled = True
                    btn_TSCancel.Enabled = False
                    'LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 7 And e.Column.Name <> colCrate And e.Column.Name <> colAmt And e.Column.Name <> colLitre And e.Column.Name <> colMAmt And e.Column.Name <> colPCount And e.Column.Name <> colPCount Then
                ' If isLoadData = False Then
                If (chkEveningGatepassTruckSheetGenerated.Checked OrElse chkEveningPosted.Checked) And clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colShiftName).Value), "Evening ") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(e.ColumnIndex).ReadOnly = True
                End If
                If (chkMorningGatepassTruckSheetGenerated.Checked OrElse chkMorningPosted.Checked) And clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colShiftName).Value), "Morning ") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(e.ColumnIndex).ReadOnly = True
                End If
                e.CellElement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MergeHorizontally(radGridView As RadGridView, startColumnIndex As Integer, endColumnIndex As Integer)
        For Each item As GridViewRowInfo In radGridView.Rows
            For i As Integer = startColumnIndex To endColumnIndex - 1
                Dim firstCell As GridViewCellInfo = item.Cells(i)
                Dim secondCell As GridViewCellInfo = item.Cells(i + 1)
                Dim firstCellText As String = (If(firstCell IsNot Nothing AndAlso firstCell.Value IsNot Nothing, firstCell.Value.ToString(), String.Empty))
                Dim secondCellText As String = (If(secondCell IsNot Nothing AndAlso secondCell.Value IsNot Nothing, secondCell.Value.ToString(), String.Empty))
                setCellBorders(firstCell, Color.FromArgb(209, 225, 245))
                setCellBorders(secondCell, Color.FromArgb(209, 225, 245))
                If firstCellText = secondCellText Then
                    firstCell.Style.BorderRightColor = Color.Transparent
                    secondCell.Style.BorderLeftColor = Color.Transparent
                    secondCell.Style.ForeColor = Color.Transparent
                Else
                    secondCell.Style.ForeColor = Color.Black
                End If
            Next
        Next
    End Sub
    Private Sub MergeVertically(radGridView As RadGridView, columnIndexes As Integer())
        Dim Prev As GridViewRowInfo = Nothing
        For Each item As GridViewRowInfo In radGridView.Rows
            If Prev IsNot Nothing Then
                Dim firstCellText As String = String.Empty
                Dim secondCellText As String = String.Empty
                For Each i As Integer In columnIndexes
                    Dim firstCell As GridViewCellInfo = Prev.Cells(i)
                    Dim secondCell As GridViewCellInfo = item.Cells(i)
                    firstCellText = (If(firstCell IsNot Nothing AndAlso firstCell.Value IsNot Nothing, firstCell.Value.ToString(), String.Empty))
                    secondCellText = (If(secondCell IsNot Nothing AndAlso secondCell.Value IsNot Nothing, secondCell.Value.ToString(), String.Empty))
                    setCellBorders(firstCell, Color.FromArgb(209, 225, 245))
                    setCellBorders(secondCell, Color.FromArgb(209, 225, 245))
                    If rbtnMorningEveningBoth.IsChecked = True Then
                        If firstCellText = secondCellText Then
                            firstCell.Style.BorderBottomColor = Color.Transparent
                            secondCell.Style.BorderTopColor = Color.Transparent
                            secondCell.Style.ForeColor = Color.Transparent
                        Else
                            secondCell.Style.ForeColor = Color.Black
                            Prev = item
                            Exit For
                        End If
                    Else
                        If firstCellText = secondCellText Then
                            'firstCell.Style.BorderBottomColor = Color.Black
                            'secondCell.Style.BorderTopColor = Color.Black
                            secondCell.Style.ForeColor = Color.Black
                        Else
                            secondCell.Style.ForeColor = Color.Black
                            Prev = item
                            Exit For
                        End If
                    End If
                Next
            Else
                Prev = item
            End If
        Next
    End Sub
    Private Sub setCellBorders(cell As GridViewCellInfo, color As Color)
        cell.Style.CustomizeBorder = True
        cell.Style.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders
        cell.Style.BorderLeftColor = color
        cell.Style.BorderRightColor = color
        cell.Style.BorderBottomColor = color
        If cell.Style.BorderTopColor <> Color.Transparent Then
            cell.Style.BorderTopColor = color
        End If
    End Sub
    Private Sub gv1_SortChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.SortChanged
        MergeVertically(gv1, New Integer() {1, 2})
    End Sub
    Private Sub gv1_FilterChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.FilterChanged
        MergeVertically(gv1, New Integer() {1, 2})
        ''MergeHorizontally(gv1, 2, 3)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            For jj As Integer = 0 To gv1.Rows.Count - 1
                Dim qry As String = ""
                Dim strCustomerDesc As String = ""
                Dim strCustomerCode As String = ""
                ' Dim strCustomer = clsCommon.myCstr(gv1.Rows(jj).Cells(colCustCode).Value)
                Dim strCustomer = clsCommon.myCstr(gv1.Rows(jj).Cells(colCustName).Value).ToLower()
                gv1.ClearSelection()
                If strCustomer.Contains(txtcustomersearch.Text.ToLower()) Then
                    gv1.Rows(jj).Cells(colCustCode).IsSelected = True
                    gv1.Rows(jj).IsCurrent = True
                    gv1.Columns(colCustCode).IsCurrent = True
                    gv1.PerformLayout()
                    gv1.Focus()
                    gv1.VerticalScroll.Visible = True
                    Exit Sub
                End If
                'If clsCommon.CompairString(strCustomer, txtcustomersearch.Text) = CompairStringResult.Equal Then
                '    gv1.Rows(jj).Cells(colCustCode).IsSelected = True
                '    gv1.Rows(jj).IsCurrent = True
                '    gv1.Columns(colCustCode).IsCurrent = True
                '    gv1.PerformLayout()
                '    gv1.Focus()
                '    gv1.VerticalScroll.Visible = True
                '    Exit Sub
                'End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub rmi_TS_Excel_Click(sender As Object, e As EventArgs) Handles rmi_TS_Excel.Click
        TruckSheet(EnumExportTo.Excel)
    End Sub
    Private Sub rmi_TS_PDF_Click(sender As Object, e As EventArgs) Handles rmi_TS_PDF.Click
        TruckSheet(EnumExportTo.PDF)
    End Sub
    Private Sub TruckSheet(ByVal exporter As EnumExportTo)
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select document", Me.Text)
                txtDocNo.Focus()
                Exit Sub
            End If
            If rbtnMorningEveningBoth.IsChecked = True Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Morning/Evening Shift", Me.Text)
                rbtnMorning.Focus()
                Exit Sub
            End If
            clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_DETAIL set IsTruckSheetGenerated='Y' where document_no='" & txtDocNo.Value & "' and ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'")
            If exporter = EnumExportTo.Excel Then
                TruckSheetExcel()
            Else
                TruckSheetPDF()
            End If
            SplitButtonTruckSheet.Enabled = False
            btn_TSCancel.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint1_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue("Booking not found to Print")
        End If
        Dim qry As String = Nothing
        Dim SubRptQry As String = Nothing
        Dim ShiftType As String = ""
        Dim Previous_Shift As String = ""
        Dim Previous_Date As String
        If rbtnEvening.IsChecked = True Then
            ShiftType = "Evening"
            Previous_Shift = "Morning"
            Previous_Date = clsCommon.myCDate(txtDate.Value).AddDays(1)
        Else
            ShiftType = "Morning"
            Previous_Shift = "Evening"
            ' Previous_Date = clsDBFuncationality.getSingleValue("select CONVERT(varchar, DATEADD(DAY, -1, convert(Nvarchar, '" & txtDate.Value & "' ,112)),21) as Previous_Date")
            Previous_Date = clsCommon.myCDate(txtDate.Value).AddDays(-1)
        End If
        Dim Comp_Name As String = clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select Booking No")
            End If
            '   Dim qry As String = "	Select * from(Select tmp.Cust_Code, Sum([TM500ML])[TM500ML],Sum([DTM 500 Ml])[DTM 500 ML], Sum([SM 500 ML])[SM 500 ML] ,Sum([GOLD 500 Ml])[GOLD 500 ML],
            'SUM([DTM 200 Ml])[DTM 200 ML] , Sum([TM 1Ltr])[TM 1Ltr],SUM ([PCHCH 500])[PCHCH 500],SUM ([TM (CS 1 Ltr)])[TM (CS 1 Ltr)] , Sum([TM 6Ltr])[TM 6Ltr],
            'SUM([DTM 6L])[DTM 6L] , Sum([GOLD 6Ltr])[GOLD 6Ltr],SUM ([SKIM 6Ltr])[SKIM 6Ltr] , Sum([SM 6LTR])[SM 6LTR],
            'ShiftType , sum(ItemNetAmount) as Total_Amt , Sum(tmp.TotalCrates_ItemWise)TotalCrates_ItemWise	,CONVERT(VARCHAR,GETDATE(),105) As Date, '" & Comp_Name & "' as Comp_Name, '" & txtRouteNo.Value & "' as Route_No
            ', '" & lblTransporterName.Text & "' as transporter_name , '" & lblVehicleNo.Text & "' as Description from
            '(SELECT Cust_Code  , IsNull([TM500ML],0)[TM500ML], isnull([DTM 500 Ml],0)[DTM 500 ML], ISNULL([SM 500 ML],0)[SM 500 ML] ,IsNull([GOLD 500 Ml],0)[GOLD 500 ML] ,
            'IsNull([DTM 200 Ml],0)[DTM 200 ML], IsNull([TM 1Ltr],0)[TM 1Ltr], IsNull([PCHCH 500],0)[PCHCH 500], IsNull([TM (CS 1 Ltr)],0)[TM (CS 1 Ltr)] , IsNull([TM 6Ltr],0)[TM 6Ltr],IsNull([DTM 6L],0)[DTM 6L],
            'IsNull([GOLD 6Ltr],0)[GOLD 6Ltr], IsNull([SKIM 6Ltr],0)[SKIM 6Ltr] , IsNull([SM 6LTR],0)[SM 6LTR] , ShiftType , ItemNetAmount , TotalCrates_ItemWise FROM
            '(SELECT Cust_Code,Qty, ShiftType,ItemNetAmount , TotalCrates_ItemWise , short_description FROM TSPL_DEMAND_BOOKING_DETAIL
            'left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code  
            'where ShiftType = '" & ShiftType & "' and Document_No = '" & txtDocNo.Value & "' 
            ')tab1
            'PIVOT(SUM(qty) FOR short_description IN ([TM500ML],[DTM 500 ML],[SM 500 ML],[GOLD 500 ML] ,[DTM 200 ML],[TM 1Ltr],[PCHCH 500],[TM (CS 1 Ltr)],[TM 6Ltr],[DTM 6L],
            '[GOLD 6Ltr],[SKIM 6Ltr],[SM 6LTR])) AS Tab2 )tmp
            'group by tmp.Cust_Code,tmp.ShiftType)As tmp1
            'left outer join 
            '(select Cust_Code as Code,Sum(Isnull(TotalCrates_ItemWise,0))Previous_Shift_Crate from TSPL_DEMAND_BOOKING_DETAIL
            '   left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
            '   where TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" & Previous_Shift & "'  and Document_Date = '" & Previous_Date & "' and TSPL_DEMAND_BOOKING_MASTER.Document_No = '" & txtDocNo.Value & "'
            'Group By Cust_Code)as Previous ON Previous.Code=tmp1.Cust_Code order by Cust_Code"

            '    qry = "select TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType,
            ''TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty,
            ''TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_DEMAND_BOOKING_MASTER.Route_No,'" & Comp_Name & "' as CompanyName,'" & lblTransporterName.Text & "' as TranspoterName,TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Rate
            ''from TSPL_DEMAND_BOOKING_MASTER
            ''left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
            ''left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
            ''where TSPL_DEMAND_BOOKING_DETAIL.Document_No='" & txtDocNo.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & ShiftType & "'"

            qry = "select '' As [FromDate],'' As [ToDate],TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType,
                    TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,
                    Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code='Crate' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Crate,
                    Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code='Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Pouch,
                    TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Document_Date,
                    TSPL_ROUTE_MASTER.Route_Desc,
                    TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
                    isnull (PreviousDemand.Crate, 0)as Crate_Collect,
                    Isnull(TSPL_COMPANY_MASTER.Comp_Name,'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.') as CompanyName,
                    TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName,
                    TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,
					ITEMDETAIL.CFForLTR,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
					Convert(decimal(18,2),(TSPL_DEMAND_BOOKING_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ITEMDETAIL.CFForLTR) As QTYLtr
                    from TSPL_DEMAND_BOOKING_MASTER
                    Left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
                    Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
					Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
					Left Join (select Conversion_factor AS CFForLTR,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_ITEM_UOM_DETAIL.Item_Code
                    Left Join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code
                    Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No
                    Left Join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id
                    Left Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_DEMAND_BOOKING_MASTER.Comp_Code
Left join ( select TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, sum(TSPL_DEMAND_BOOKING_DETAIL.Qty )as Qty, Case When max(TSPL_DEMAND_BOOKING_DETAIL.Unit_Code) = 'Crate' Then sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) Else 0 End As Crate, Case When max(TSPL_DEMAND_BOOKING_DETAIL.Unit_Code) = 'Pouch' Then sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) Else 0 End As Pouch, max(TSPL_DEMAND_BOOKING_MASTER.Route_No) as Route_No from TSPL_DEMAND_BOOKING_MASTER Left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code where TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" & Previous_Shift & "' "
            If rbtnEvening.IsChecked Then
                qry += " and TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(txtDate.Value) + " ' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(Previous_Date) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "'  group by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ) "
            Else
                qry += "  and TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(Previous_Date) + " ' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' group by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ) "

            End If
            qry += " as PreviousDemand on PreviousDemand.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  "

            qry += "  where TSPL_DEMAND_BOOKING_DETAIL.Document_No='" & txtDocNo.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & ShiftType & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBooking", "Demand Booking")
            'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptDemandBooking", "Demand Booking", "rptSubDemandBooking")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String = ""
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else

            End If

            If Not filePath.Equals(String.Empty) Then
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.exportdata(gv1, "", Me.Text, , Nothing, False, False, True)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvImport As New RadGridView()
        Dim arrVisbleColumns As New List(Of Integer)
        Try
            If clsCommon.myLen(gv1.Rows(0).Cells(colCustCode).Value) = 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Import", Me.Text)
                Exit Sub
            End If
            Me.Controls.Add(gvImport)
            For ii As Integer = 0 To gv1.Columns.Count - 1
                If gv1.Columns(ii).IsVisible Then
                    arrVisbleColumns.Add(ii)
                End If
            Next


            If clsDemandBookingImport.importExcel(gvImport) Then
                If gvImport.Rows.Count > 0 Then
                    If clsCommon.CompairString(gv1.Rows.Count - 1, gvImport.Rows.Count - 1) = CompairStringResult.Equal Then
                        Try
                            Dim arrCustCodeExist As New List(Of String)
                            For i As Integer = 0 To gv1.Rows.Count - 1
                                arrCustCodeExist.Add(gv1.Rows(i).Cells(1).Value)
                            Next
                            isInsideLoadData = True
                            clsCommon.ProgressBarPercentShow()
                            For ii As Integer = 1 To gvImport.Rows.Count - 1
                                clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / gvImport.RowCount, clsCommon.myCstr((ii + 1)) + "/" + clsCommon.myCstr(gvImport.RowCount))
                                For jj As Integer = 0 To gv1.Rows.Count - 1
                                    Dim code As String = clsCommon.myCstr(gvImport.Rows(ii).Cells(1).Value)
                                    If arrCustCodeExist.Contains(code) Then
                                        If clsCommon.CompairString(clsCommon.myCstr(gvImport.Rows(ii).Cells(1).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colCustCode).Value)) = CompairStringResult.Equal Then
                                            For kk As Integer = 4 To arrVisbleColumns.Count - 8
                                                If clsCommon.myCDecimal(gv1.Rows(jj).Cells(arrVisbleColumns(kk)).Value) <> clsCommon.myCDecimal(gvImport.Rows(ii).Cells(kk).Value) Then
                                                    gv1.Rows(jj).Cells(arrVisbleColumns(kk)).Value = gvImport.Rows(ii).Cells(kk).Value

                                                End If

                                                'clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / gvImport.RowCount, clsCommon.myCstr((ii + 1)) + "/" + clsCommon.myCstr(gvImport.RowCount) + "  " + clsCommon.myCstr(gvImport.Rows(ii).Cells(kk).Value))
                                            Next
                                        End If
                                    Else
                                        clsCommon.MyMessageBoxShow("Default Customer '" + clsCommon.myCstr(gvImport.Rows(ii).Cells(1).Value) + "' Does Not Exist", Me.Text)
                                        Exit Sub

                                    End If
                                Next
                            Next
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        Finally
                            clsCommon.ProgressBarPercentHide()
                        End Try
                        isInsideLoadData = False
                        UpdateAllTotals()
                    Else
                        clsCommon.MyMessageBoxShow("You cannot import qunatity because both Import and Export Data is different", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvImport)
        End Try
    End Sub

    Public Function GetOutStandingBal(ByVal VendorNo As String, ByVal dtDoc As DateTime) As Decimal
        Dim OSBal As Decimal = 0
        Try
            Dim fromdate As DateTime = dtDoc.AddDays(-30)
            Dim todate As DateTime = dtDoc
            Dim StrPermission As String = clsERPFuncationality.UserWiseAvailableLocationSegment()
            'Dim qry = ""
            Dim qry As String = "With CTETemp as (Select ROW_NUMBER() OVER (PARTITION BY Vendor_Code ORDER BY Vendor_Code, DocDate) as RowNo, '13/Aug/2015 12:50 PM'  as RunDate,'28/07/2023' as fromdate,'28/08/2023' as todate,'Udaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.' as Comp_Name ,'Ahmedabad Road Gordhan Vilas, Sector 14 Hiran Magri' as CompanyAdd , CustomerVendorFinal.* from ( SELECT MAX(FINALCUSTOMERVENDOR.ACode) AS ACode,MAX(FINALCUSTOMERVENDOR.AName)  AS AName,'' AS DocNo,MAX(FINALCUSTOMERVENDOR.DocDate) AS DocDate,'OP' AS DocType  ,'' AS DocNarr,max(FINALCUSTOMERVENDOR.ChequeDetails)as ChequeDetails, MAX(FINALCUSTOMERVENDOR.Currency_Code) as Currency_Code, sUM(FINALCUSTOMERVENDOR.DrAmt) AS DrAmt, SUM(FINALCUSTOMERVENDOR.CrAmt) AS CrAmt, MAX(FINALCUSTOMERVENDOR.Location) AS Location,'' AS SourceCode,'' AS CUSTOMER,FINALCUSTOMERVENDOR.Vendor_Code,max(FINALCUSTOMERVENDOR.AgainstInvoiceNo) as AgainstInvoiceNo FROM ( (SELECT FINALCUSTOMER.ACode AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate, convert(decimal(18,2),InnQuery.DrAmt) as DrAmt,convert(decimal(18,2),InnQuery.CrAmt) as CrAmt,convert(decimal(18,2),InnQuery.Sales) as Sales,case when InnQuery.DocType='IM' then case when InnQuery.CrAmt>0 then  convert(decimal(18,2),InnQuery.CrAmt) else convert(decimal(18,2),InnQuery.DrAmt) * -1 end  else convert(decimal(18,2),InnQuery.CollectionRefund) end  as CollectionRefund , 
 InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,InnQuery .EXCHANGE_GAIN_AMT ,InnQuery .EXCHANGE_LOSS_AMT   from (Select InnQuery.ACode AS ACode,InnQuery.Child  , InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ,
case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then 
 case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt*  InnQuery. ConvRate) else 
 case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt*  InnQuery. ConvRate) when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate) WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate)  else 0 end end else 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else (CrAmt*  InnQuery. ConvRate)  end end else 
 case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt*  InnQuery. ConvRate) +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt*  InnQuery. ConvRate) -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else  (CrAmt*  InnQuery. ConvRate)  end end end as CrAmt, 
 InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (SELECT 
 ACode AS ACode,max(Child) as Child, 
MAX(AName) AS AName,MAX(DocNo) AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType  ,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt) AS DrAmt, SUM(CrAmt) AS CrAmt, SUM(SecurityDrAmt) as SecurityDrAmt, SUM(SecurityCrAmt) as SecurityCrAmt, SUM(Sales) as Sales,case when MAX(DocType)='IM' then case when SUM(CrAmt)>0 then  SUM(CrAmt) else  0 end  else  Sum(CollectionRefund) end  as CollectionRefund , SUM(DrNote) as DrNote,case when MAX(DocType)='IM' then 0 else  Sum(CrNote) end  as CrNote, Location AS Location, 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code, MAX(Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='S' THEN 'Security' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='C' THEN 'Crate Security' ELSE  CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='R' THEN 'Refrigerator' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='O' THEN 'Others' END END END END Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,  Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as DrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (CASE WHEN   TSPL_RECEIPT_HEADER.Receipt_Type='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' THEN TSPL_RECEIPT_DETAIL.Applied_Amount  WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS SecurityCrAmt, 0 as [Sales], case when TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') Then Receipt_Amount*-1 When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then TSPL_RECEIPT_DETAIL.Applied_Amount WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount Else Receipt_Amount end as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_Customer_Invoice_Head.Loc_Code  WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN TSPL_RECEIPT_HEADER.Location_GL_Code ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END  ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END as [Location], (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Case When TSPL_RECEIPT_HEADER.Receipt_Type='R' Then TSPL_Customer_Invoice_Head.Customer_Code Else TSPL_RECEIPT_HEADER.Cust_Code End LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y'   and  TSPL_RECEIPT_HEADER.Receipt_Type not in ('M','A','U','K') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'   and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL 
  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_RECEIPT_HEADER.Receipt_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
 where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('R','A') 
 AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL 
 Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, ''  as AgainstInvoiceNo, Reversal_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_BANK_REVERSE.Source_Type As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_BANK_REVERSE  
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
 where  TSPL_BANK_REVERSE.Reverse_Document='Receipts' AND TSPL_BANK_REVERSE.Post ='P'
 and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL 
 Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when RD.Receipt_Type='C' then 0 else RD.Applied_Amount  end as DrAmt,case when RD.Receipt_Type='C' then RD.Applied_Amount  else 0  end  AS CrAmt, Receipt_Amount as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],     CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE  CASE WHEN (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt )<>''  THEN  (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt) ELSE  substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END END AS [Location],  
 (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON TSPL_RECEIPT_HEADER.Receipt_No =RD.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND CIH.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL 
 SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 )INV  GROUP BY  DocNo,Location 
 UNION ALL 
 SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM'  WHEN TSPL_RECEIPT_HEADER.Receipt_Type='K' THEN 'KN' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount *-1  end  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then TSPL_RECEIPT_DETAIL.Applied_Amount else 0 end  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' when TSPL_RECEIPT_HEADER.Receipt_Type='K' then 'AR-KN' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y'  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'K'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 )INV  GROUP BY  DocNo,Location 
 UNION ALL  Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child,TSPL_RECEIPT_HEADER.Customer_Name as AName, (Select r1.Receipt_No from TSPL_RECEIPT_HEADER r1 where r1 .UnApplied_No  =TSPL_RECEIPT_HEADER.Receipt_No)  as DocNo,'' as AgainstInvoiceNo,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate,'RC' as DocType,'' as DocNarr,(rtrim(TSPL_RECEIPT_HEADER.Entry_Desc) + (case when len(RTRIM(TSPL_RECEIPT_HEADER.Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_RECEIPT_HEADER.Cheque_No +  ' - '+TSPL_RECEIPT_HEADER.Cheque_Date else '' end)) as ChequeDetails, RH.Currency_Code, RH.ConvRate, 0 as DrAmt,  TSPL_RECEIPT_HEADER.Receipt_Amount  AS CrAmt, 0 as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 End  AS SecurityCrAmt, 0 as [Sales], TSPL_RECEIPT_HEADER.Receipt_Amount as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], Right(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location],  'AR-RC' as SourceCode, '' as Item_Code, '' as Item_Desc, TSPL_RECEIPT_HEADER.Receipt_Type As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_HEADER RH ON TSPL_RECEIPT_HEADER.Receipt_No =RH.UnApplied_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON RH.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('U') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 )XX GROUP BY XX.ACode, XX.Location, XX.DocNo,XX.DocType 
  UNION ALL
 Select ACode,Child, AName, DocNo, AgainstInvoiceNo, DocDate, DocType, Narration, ChequeDetails, CURRENCY_CODE, ConvRate, DrAmt, CrAmt, SecurityDrAmt, SecurityCrAmt, Sales, Collectionrefund, DrNote, CrNote, Location, SourceCode, Item_Code, Item_Desc, Receipt_Type, Bank_Code, Cust_Type_Code, Cust_Type_Desc, Cust_Category_Code, CUST_CATEGORY_DESC from (
 Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child, CIH.Customer_Code, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, '' as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then RD.Applied_Amount Else 0 end as DrAmt, 0 as CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 end as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales, 0 as Collectionrefund, TSPL_RECEIPT_HEADER.Receipt_Amount as DrNote, 0 as CrNote, 
  CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC, ROW_NUMBER() OVER (Partition By TSPL_RECEIPT_HEADER.Receipt_No ORDER BY TSPL_RECEIPT_HEADER.Receipt_No) as RowNo  from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A'  AND ISNULL(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 ) XX 
 UNION ALL
 Select max(ACode) as ACode ,max(Child) as Child , max(AName) as AName, max(DocNo) as DocNo, max(AgainstInvoiceNo) as AgainstInvoiceNo,max( DocDate) as DocDate, max(DocType) as DocType, max(Narration) as Narration, max(ChequeDetails) as Narration, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate, Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt, sum(SecurityDrAmt) as SecurityDrAmt, sum(SecurityCrAmt) as SecurityCrAmt, sum(Sales) as Sales, Sum(Collectionrefund) as Collectionrefund, Sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(Location) as Location, max(SourceCode) as SourceCode, max(Item_Code) as Item_Code,max( Item_Desc) as Item_Desc, max(Receipt_Type) as Receipt_Type , max(Bank_Code) as Bank_Code, max(Cust_Type_Code) as Cust_Type_Code, max(Cust_Type_Desc) as Cust_Type_Desc, max(Cust_Category_Code) as Cust_Category_Code,max( CUST_CATEGORY_DESC) as  CUST_CATEGORY_DESC from (
 Select  isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code )  as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='A' then RD.Child_Cust_Code else CIH.Customer_Code end as Child, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, CIH.Document_No as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, 0 as DrAmt, RD.Applied_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales,RD.Applied_Amount as CollectionRefund,0 as DrNote, 0 as CrNote,
 CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC   from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A' AND isnull(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 )z group by DocNo ,Location,ACode
 UNION ALL
 SELECT max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as ACode,max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as Child,max( TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,'' as AgainstInvoiceNo,  CONVERT(DATE,MAX(Final.Adjustment_Date),103) AS DocDate, 'AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost)*-1 else SUM(Final.Cost) end as Collectionrefund, 0 as [drNote], 0 as [CrNote], max(Final.Location) as [Location], Max(SourceCode)as SourceCode, Item_Code, MAX(Item_Description) as Item_Desc  ,MAX(Receipt_Type) As Receipt_Type, '' as Bank_Code,MAX(Cust_Type_Code) as Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No,  TSPL_ADJUSTMENT_HEADER.Adjustment_Date,  TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN  TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost,  TSPL_ADJUSTMENT_HEADER.Document_No , TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type ,'IC-AD' as SourceCode, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description  ,'' As Receipt_Type , TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    FROM TSPL_ADJUSTMENT_HEADER  LEFT OUTER JOIN  TSPL_ADJUSTMENT_DETAIL ON  TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left OUTER JOIN  TSPL_LOCATION_MASTER on  TSPL_ADJUSTMENT_DETAIL.Location_Code= TSPL_LOCATION_MASTER.Location_Code inner join  tspl_customer_master on  tspl_adjustment_header.customer_code= tspl_customer_master.cust_code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   WHERE ( ( TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND ( TSPL_ADJUSTMENT_HEADER.Document_No= '' and  TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') or TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and  TSPL_ADJUSTMENT_HEADER.Posted='Y'   and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 ) AS Final INNER JOIN  TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No =  TSPL_ADJUSTMENT_HEADER.Adjustment_No  GROUP BY Final.Adjustment_No, Final.Item_Code
 UNION ALL
select TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.Customer_Code as Child,TSPL_Customer_Invoice_Head.Customer_Name , TSPL_Customer_Invoice_Head.Document_No  AS DocNo, CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN  CASE WHEN LEN(ISNULL( (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No),''))>0 THEN (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) ELSE (Select Top 1 ISNULL(TSPL_INVOICE_MASTER_BULKSALE.Document_No,'')  FROM TSPL_INVOICE_MASTER_BULKSALE  where TSPL_INVOICE_MASTER_BULKSALE.Document_No  =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) END   WHEN TSPL_Customer_Invoice_Head.Document_Type ='I' AND len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 THEN (Select Top 1 TSPL_SCRAPINVOICE_HEAD.invoice_No  FROM TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.invoice_No  =TSPL_CUSTOMER_Invoice_Head.AgainstScrap ) END AS AgainstInvoiceNo,  CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'', TSPL_Customer_Invoice_Head.Currency_Code, TSPL_Customer_Invoice_Head.ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], 0 as SecurityDrAmt, 0 as SecurityCrAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='I' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [Sales], 0 as [CollectionRefund], case when TSPL_Customer_Invoice_Head.Document_Type ='D' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [DrNote], Case when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_Customer_Invoice_Head.Status=1    and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL
 select TSPL_Receipt_Adjustment_Header.Customer_No  as ACode,TSPL_Receipt_Adjustment_Header.Customer_No as Child, TSPL_Receipt_Adjustment_Header.Customer_Name  as AName, TSPL_Receipt_Adjustment_Header.Adjustment_No  as DocNo,  TSPL_Receipt_Adjustment_Header.ARInvoiceNo as AgainstInvoiceNo, CONVERT(DATE, TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) as DocDate,'Adjustment' as docType,  TSPL_Receipt_Adjustment_Header.Remarks as DocNarr,  '' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as DrAmt ,case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund],  case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end *-1 as [DrNote],  case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end  * -1 as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code as Location, '' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,  TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Receipt_Adjustment_Header.Customer_No = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   where isnull(TSPL_Receipt_Adjustment_Header.Is_Post,'')='Y'   and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL
 select (coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as ACode,(coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as Child,(coalesce( TSPL_Customer_Invoice_Head.Customer_Name,TSPL_BANK_REVERSE.Cust_Name))  as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, 
 TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END End as DrAmt,  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN TSPL_BANK_REVERSE.Amount When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END ELSE 0 END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
 Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else TSPL_BANK_REVERSE.Amount End*-1 as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
 CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location], 
 'RV-TA'as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'  
  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 union all
select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'UA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  TSPL_RECEIPT_HEADER.UnApplied_Balance as DrAmt,  0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) AS [Location], 
 'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER as UnappliedReceipt ON UnappliedReceipt.Receipt_No = TSPL_RECEIPT_HEADER .UnApplied_No  
 where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and UnappliedReceipt.Receipt_Type ='U' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>'' 
  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL
 select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when  TSPL_VCGL_Head.Amount_Type='Cr' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when  TSPL_VCGL_Head.Amount_Type='Dr' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount Else 0 End as [DrNote], case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount*-1 Else 0 End as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_VCGL_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 UNION ALL
 select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Detail.Document_No       where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 ) InnQuery 
 LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo where 1=1   and InnQuery.DocType<>'IM'  and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 Union All
 Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
  ) )    )InnQuery    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode) FINALCUSTOMER inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type in ('VSP'))  Union All (Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (  Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,   CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,CONVERT(DECIMAL(18,2),InnQuery.Purchase) AS Purchase,CONVERT(DECIMAL(18,2),InnQuery.Payments) AS Payments ,  InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end)  as  CrAmt ,
 case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then 
case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*    InnQuery.ConvRate) +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else 
case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   InnQuery.ConvRate)  else case when (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else 
case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt*  InnQuery.ConvRate)  else case when (DocType)<>'EXC' then  (DrAmt*  InnQuery.ConvRate)  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt*  InnQuery.ConvRate)  else  (DrAmt*  InnQuery.ConvRate)  end end end as DrAmt, 
 CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end + ( case when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END) as CrAmt,   case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code 
 from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0   
  and  convert(date,Invoice_Entry_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 and TSPL_VENDOR_INVOICE_HEAD.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 Select * from ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,'WCT' as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, 0 as CrAmt,case when isnull(TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END  DrAmt,  Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  ) FinalWCt where (FinalWCt.CrAmt <>0 or FinalWCt.DrAmt <>0) 
 and  convert(date,FinalWCt.DocDate ,103) <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'   and FinalWCt.VCode in ('" + clsCommon.myCstr(VendorNo) + "')  UNION ALL
 Select  Reference_Doc_No,Emp_type,Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt,  CrAmt-DrAmt as Purchase, 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (
 select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN , TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end ) as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1
 and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and TSPL_PI_HEAD.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')) XXX UNION ALL
 select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then  Actual_Total_TDS  else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN ('I','D') AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_REMITTANCE.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND 1=2
  and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_REMITTANCE.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'   and TSPL_BANK_REVERSE.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select      (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  
 and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and VC_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''
 and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and VC_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
  select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END as CrAmt, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Header.Adjustment_Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Header  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='P'  
 and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103) <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'   and TSPL_Payment_Adjustment_Header.Vendor_No in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
  select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as CrAmt,  CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='R'  
 and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_Payment_Adjustment_Header.Vendor_No in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 Select max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
 select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
  and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'   and tspl_payment_header.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') ) XX Group By XX.account, XX.DocNo UNION ALL
 select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName,TSPL_PAYMENT_HEADER.Payment_No as DocNo,'EXC' as [DocType],convert(date,Payment_Date, 103) as DocDate, case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(TSPL_PAYMENT_HEADER.cheque_No+'-'+ CONVERT(VARCHAR,Cheque_Date , 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, 0 as Balance_Amount,isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_PAYMENT_HEADER.Comp_Code from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_Vendor_Master ON TSPL_Vendor_Master.Vendor_Code =TSPL_PAYMENT_HEADER.Vendor_Code 
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_PAYMENT_HEADER.Payment_No 
 LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
 LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No  
 where TSPL_PAYMENT_HEADER.Posted=1 and  TSPL_PAYMENT_HEADER.Payment_Type  in ('PY','AD') 
 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0)  
  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_VENDOR_MASTER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')
 UNION ALL
 Select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN,TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
 'EXC'  as DocType  ,convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate,case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security ,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_PAYMENT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+ convert(varchar ,TSPL_PAYMENT_HEADER . Cheque_Date ,103)else '' end)) as ChequeDetails, TSPL_PAYMENT_HEADER.Currency_Code, 1 as ConvRate,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as DrAmt,  0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,
 'RV',0, isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType,TSPL_PAYMENT_HEADER.Comp_Code 
 from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_VENDOR_MASTER  ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code   
 LEFT OUTER JOIN TSPL_Vendor_TYPE_MASTER  ON TSPL_Vendor_TYPE_MASTER.Ven_Type_Code  = TSPL_VENDOR_MASTER.Ven_Type_Code   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
 LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.PAYMENT_NO =TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
 LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No 
 where  TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P'
 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_VENDOR_MASTER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') 
 UNION ALL
 Select  max(FinalQryForAdvance.Reference_Doc_No) as Reference_Doc_No,max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account 
 , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code 
 from (select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code 
 left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code
 left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code
 left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary 
 where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   
  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103) <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance
 group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code UNION ALL
Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 ) XX Group By XX.account, XX.DocNo
 UNION ALL
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1
 UNION ALL
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   ) InnQuery 
  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code   where InnQuery.DocNo not in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 and TSPL_PAYMENT_HEADER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')
 Union All
 Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  
 and TSPL_PAYMENT_HEADER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') ) )    ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo  WHERE TSPL_VENDOR_MASTER.Form_Type in ('VSP')) FinalVENDOR left outer jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in ('VSP'))  )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate <'" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' GROUP BY FINALCUSTOMERVENDOR.Vendor_Code   Union All  (SELECT FINALCUSTOMERVENDOR.ACode, FINALCUSTOMERVENDOR.AName, FINALCUSTOMERVENDOR.DocNo, FINALCUSTOMERVENDOR.DocDate, FINALCUSTOMERVENDOR.DocType, FINALCUSTOMERVENDOR.DocNarr,FINALCUSTOMERVENDOR.ChequeDetails, FINALCUSTOMERVENDOR.Currency_Code, FINALCUSTOMERVENDOR.DrAmt, FINALCUSTOMERVENDOR.CrAmt, FINALCUSTOMERVENDOR.Location, FINALCUSTOMERVENDOR.SourceCode, '' AS CUSTOMER, FINALCUSTOMERVENDOR.Vendor_Code,FINALCUSTOMERVENDOR.AgainstInvoiceNo FROM ( (SELECT FINALCUSTOMER.ACode AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate, convert(decimal(18,2),InnQuery.DrAmt) as DrAmt,convert(decimal(18,2),InnQuery.CrAmt) as CrAmt,convert(decimal(18,2),InnQuery.Sales) as Sales,case when InnQuery.DocType='IM' then case when InnQuery.CrAmt>0 then  convert(decimal(18,2),InnQuery.CrAmt) else convert(decimal(18,2),InnQuery.DrAmt) * -1 end  else convert(decimal(18,2),InnQuery.CollectionRefund) end  as CollectionRefund , 
 InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,InnQuery .EXCHANGE_GAIN_AMT ,InnQuery .EXCHANGE_LOSS_AMT   from (Select InnQuery.ACode AS ACode,InnQuery.Child  , InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ,
case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then 
 case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt*  InnQuery. ConvRate) else 
 case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt*  InnQuery. ConvRate) when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate) WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate)  else 0 end end else 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else (CrAmt*  InnQuery. ConvRate)  end end else 
 case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt*  InnQuery. ConvRate) +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt*  InnQuery. ConvRate) -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else 
 case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else  (CrAmt*  InnQuery. ConvRate)  end end end as CrAmt, 
 InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (SELECT 
 ACode AS ACode,max(Child) as Child, 
MAX(AName) AS AName,MAX(DocNo) AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType  ,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt) AS DrAmt, SUM(CrAmt) AS CrAmt, SUM(SecurityDrAmt) as SecurityDrAmt, SUM(SecurityCrAmt) as SecurityCrAmt, SUM(Sales) as Sales,case when MAX(DocType)='IM' then case when SUM(CrAmt)>0 then  SUM(CrAmt) else  0 end  else  Sum(CollectionRefund) end  as CollectionRefund , SUM(DrNote) as DrNote,case when MAX(DocType)='IM' then 0 else  Sum(CrNote) end  as CrNote, Location AS Location, 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code, MAX(Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='S' THEN 'Security' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='C' THEN 'Crate Security' ELSE  CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='R' THEN 'Refrigerator' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='O' THEN 'Others' END END END END Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,  Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as DrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (CASE WHEN   TSPL_RECEIPT_HEADER.Receipt_Type='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' THEN TSPL_RECEIPT_DETAIL.Applied_Amount  WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS SecurityCrAmt, 0 as [Sales], case when TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') Then Receipt_Amount*-1 When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then TSPL_RECEIPT_DETAIL.Applied_Amount WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount Else Receipt_Amount end as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_Customer_Invoice_Head.Loc_Code  WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN TSPL_RECEIPT_HEADER.Location_GL_Code ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END  ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END as [Location], (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Case When TSPL_RECEIPT_HEADER.Receipt_Type='R' Then TSPL_Customer_Invoice_Head.Customer_Code Else TSPL_RECEIPT_HEADER.Cust_Code End LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y'   and  TSPL_RECEIPT_HEADER.Receipt_Type not in ('M','A','U','K') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'   and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL 
  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_RECEIPT_HEADER.Receipt_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
 where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('R','A') 
 AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL 
 Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, ''  as AgainstInvoiceNo, Reversal_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_BANK_REVERSE.Source_Type As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_BANK_REVERSE  
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
 where  TSPL_BANK_REVERSE.Reverse_Document='Receipts' AND TSPL_BANK_REVERSE.Post ='P'
 and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL 
 Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when RD.Receipt_Type='C' then 0 else RD.Applied_Amount  end as DrAmt,case when RD.Receipt_Type='C' then RD.Applied_Amount  else 0  end  AS CrAmt, Receipt_Amount as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],     CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE  CASE WHEN (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt )<>''  THEN  (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt) ELSE  substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END END AS [Location],  
 (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON TSPL_RECEIPT_HEADER.Receipt_No =RD.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND CIH.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL 
 SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 )INV  GROUP BY  DocNo,Location 
 UNION ALL 
 SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
 MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM'  WHEN TSPL_RECEIPT_HEADER.Receipt_Type='K' THEN 'KN' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount *-1  end  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then TSPL_RECEIPT_DETAIL.Applied_Amount else 0 end  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' when TSPL_RECEIPT_HEADER.Receipt_Type='K' then 'AR-KN' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y'  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'K'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 )INV  GROUP BY  DocNo,Location 
 UNION ALL  Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child,TSPL_RECEIPT_HEADER.Customer_Name as AName, (Select r1.Receipt_No from TSPL_RECEIPT_HEADER r1 where r1 .UnApplied_No  =TSPL_RECEIPT_HEADER.Receipt_No)  as DocNo,'' as AgainstInvoiceNo,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate,'RC' as DocType,'' as DocNarr,(rtrim(TSPL_RECEIPT_HEADER.Entry_Desc) + (case when len(RTRIM(TSPL_RECEIPT_HEADER.Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_RECEIPT_HEADER.Cheque_No +  ' - '+TSPL_RECEIPT_HEADER.Cheque_Date else '' end)) as ChequeDetails, RH.Currency_Code, RH.ConvRate, 0 as DrAmt,  TSPL_RECEIPT_HEADER.Receipt_Amount  AS CrAmt, 0 as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 End  AS SecurityCrAmt, 0 as [Sales], TSPL_RECEIPT_HEADER.Receipt_Amount as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], Right(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location],  'AR-RC' as SourceCode, '' as Item_Code, '' as Item_Desc, TSPL_RECEIPT_HEADER.Receipt_Type As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_HEADER RH ON TSPL_RECEIPT_HEADER.Receipt_No =RH.UnApplied_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON RH.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('U') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 )XX GROUP BY XX.ACode, XX.Location, XX.DocNo,XX.DocType 
  UNION ALL
 Select ACode,Child, AName, DocNo, AgainstInvoiceNo, DocDate, DocType, Narration, ChequeDetails, CURRENCY_CODE, ConvRate, DrAmt, CrAmt, SecurityDrAmt, SecurityCrAmt, Sales, Collectionrefund, DrNote, CrNote, Location, SourceCode, Item_Code, Item_Desc, Receipt_Type, Bank_Code, Cust_Type_Code, Cust_Type_Desc, Cust_Category_Code, CUST_CATEGORY_DESC from (
 Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child, CIH.Customer_Code, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, '' as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then RD.Applied_Amount Else 0 end as DrAmt, 0 as CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 end as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales, 0 as Collectionrefund, TSPL_RECEIPT_HEADER.Receipt_Amount as DrNote, 0 as CrNote, 
  CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC, ROW_NUMBER() OVER (Partition By TSPL_RECEIPT_HEADER.Receipt_No ORDER BY TSPL_RECEIPT_HEADER.Receipt_No) as RowNo  from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A'  AND ISNULL(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'
  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 ) XX 
 UNION ALL
 Select max(ACode) as ACode ,max(Child) as Child , max(AName) as AName, max(DocNo) as DocNo, max(AgainstInvoiceNo) as AgainstInvoiceNo,max( DocDate) as DocDate, max(DocType) as DocType, max(Narration) as Narration, max(ChequeDetails) as Narration, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate, Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt, sum(SecurityDrAmt) as SecurityDrAmt, sum(SecurityCrAmt) as SecurityCrAmt, sum(Sales) as Sales, Sum(Collectionrefund) as Collectionrefund, Sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(Location) as Location, max(SourceCode) as SourceCode, max(Item_Code) as Item_Code,max( Item_Desc) as Item_Desc, max(Receipt_Type) as Receipt_Type , max(Bank_Code) as Bank_Code, max(Cust_Type_Code) as Cust_Type_Code, max(Cust_Type_Desc) as Cust_Type_Desc, max(Cust_Category_Code) as Cust_Category_Code,max( CUST_CATEGORY_DESC) as  CUST_CATEGORY_DESC from (
 Select  isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code )  as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='A' then RD.Child_Cust_Code else CIH.Customer_Code end as Child, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, CIH.Document_No as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, 0 as DrAmt, RD.Applied_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales,RD.Applied_Amount as CollectionRefund,0 as DrNote, 0 as CrNote,
 CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC   from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
 LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
 WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A' AND isnull(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 )z group by DocNo ,Location,ACode
 UNION ALL
 SELECT max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as ACode,max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as Child,max( TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,'' as AgainstInvoiceNo,  CONVERT(DATE,MAX(Final.Adjustment_Date),103) AS DocDate, 'AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost)*-1 else SUM(Final.Cost) end as Collectionrefund, 0 as [drNote], 0 as [CrNote], max(Final.Location) as [Location], Max(SourceCode)as SourceCode, Item_Code, MAX(Item_Description) as Item_Desc  ,MAX(Receipt_Type) As Receipt_Type, '' as Bank_Code,MAX(Cust_Type_Code) as Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No,  TSPL_ADJUSTMENT_HEADER.Adjustment_Date,  TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN  TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost,  TSPL_ADJUSTMENT_HEADER.Document_No , TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type ,'IC-AD' as SourceCode, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description  ,'' As Receipt_Type , TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    FROM TSPL_ADJUSTMENT_HEADER  LEFT OUTER JOIN  TSPL_ADJUSTMENT_DETAIL ON  TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left OUTER JOIN  TSPL_LOCATION_MASTER on  TSPL_ADJUSTMENT_DETAIL.Location_Code= TSPL_LOCATION_MASTER.Location_Code inner join  tspl_customer_master on  tspl_adjustment_header.customer_code= tspl_customer_master.cust_code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   WHERE ( ( TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND ( TSPL_ADJUSTMENT_HEADER.Document_No= '' and  TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') or TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and  TSPL_ADJUSTMENT_HEADER.Posted='Y'   and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 ) AS Final INNER JOIN  TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No =  TSPL_ADJUSTMENT_HEADER.Adjustment_No  GROUP BY Final.Adjustment_No, Final.Item_Code
 UNION ALL
select TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.Customer_Code as Child,TSPL_Customer_Invoice_Head.Customer_Name , TSPL_Customer_Invoice_Head.Document_No  AS DocNo, CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN  CASE WHEN LEN(ISNULL( (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No),''))>0 THEN (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) ELSE (Select Top 1 ISNULL(TSPL_INVOICE_MASTER_BULKSALE.Document_No,'')  FROM TSPL_INVOICE_MASTER_BULKSALE  where TSPL_INVOICE_MASTER_BULKSALE.Document_No  =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) END   WHEN TSPL_Customer_Invoice_Head.Document_Type ='I' AND len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 THEN (Select Top 1 TSPL_SCRAPINVOICE_HEAD.invoice_No  FROM TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.invoice_No  =TSPL_CUSTOMER_Invoice_Head.AgainstScrap ) END AS AgainstInvoiceNo,  CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'', TSPL_Customer_Invoice_Head.Currency_Code, TSPL_Customer_Invoice_Head.ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], 0 as SecurityDrAmt, 0 as SecurityCrAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='I' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [Sales], 0 as [CollectionRefund], case when TSPL_Customer_Invoice_Head.Document_Type ='D' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [DrNote], Case when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_Customer_Invoice_Head.Status=1    and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL
 select TSPL_Receipt_Adjustment_Header.Customer_No  as ACode,TSPL_Receipt_Adjustment_Header.Customer_No as Child, TSPL_Receipt_Adjustment_Header.Customer_Name  as AName, TSPL_Receipt_Adjustment_Header.Adjustment_No  as DocNo,  TSPL_Receipt_Adjustment_Header.ARInvoiceNo as AgainstInvoiceNo, CONVERT(DATE, TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) as DocDate,'Adjustment' as docType,  TSPL_Receipt_Adjustment_Header.Remarks as DocNarr,  '' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as DrAmt ,case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund],  case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end *-1 as [DrNote],  case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end  * -1 as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code as Location, '' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,  TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Receipt_Adjustment_Header.Customer_No = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   where isnull(TSPL_Receipt_Adjustment_Header.Is_Post,'')='Y'   and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL
 select (coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as ACode,(coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as Child,(coalesce( TSPL_Customer_Invoice_Head.Customer_Name,TSPL_BANK_REVERSE.Cust_Name))  as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, 
 TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END End as DrAmt,  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN TSPL_BANK_REVERSE.Amount When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END ELSE 0 END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
 Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else TSPL_BANK_REVERSE.Amount End*-1 as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
 CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location], 
 'RV-TA'as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'  
  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 union all
select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'UA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  TSPL_RECEIPT_HEADER.UnApplied_Balance as DrAmt,  0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) AS [Location], 
 'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
 LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_RECEIPT_HEADER as UnappliedReceipt ON UnappliedReceipt.Receipt_No = TSPL_RECEIPT_HEADER .UnApplied_No  
 where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and UnappliedReceipt.Receipt_Type ='U' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>'' 
  and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL
 select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when  TSPL_VCGL_Head.Amount_Type='Cr' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when  TSPL_VCGL_Head.Amount_Type='Dr' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount Else 0 End as [DrNote], case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount*-1 Else 0 End as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_VCGL_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 UNION ALL
 select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Detail.Document_No       where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 ) InnQuery 
 LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo where 1=1   and InnQuery.DocType<>'IM'  and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 Union All
 Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
  ) )    )InnQuery    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode) FINALCUSTOMER inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type in ('VSP'))  Union All (Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end AS ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (  Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,   CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,CONVERT(DECIMAL(18,2),InnQuery.Purchase) AS Purchase,CONVERT(DECIMAL(18,2),InnQuery.Payments) AS Payments ,  InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end)  as  CrAmt ,
 case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then 
case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*    InnQuery.ConvRate) +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else 
case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   InnQuery.ConvRate)  else case when (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else 
case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt*  InnQuery.ConvRate)  else case when (DocType)<>'EXC' then  (DrAmt*  InnQuery.ConvRate)  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else 
case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt*  InnQuery.ConvRate)  else  (DrAmt*  InnQuery.ConvRate)  end end end as DrAmt, 
 CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end + ( case when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END) as CrAmt,   case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code 
 from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0   
  and  convert(date,Invoice_Entry_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,Invoice_Entry_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 and TSPL_VENDOR_INVOICE_HEAD.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 Select * from ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,'WCT' as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, 0 as CrAmt,case when isnull(TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END  DrAmt,  Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  ) FinalWCt where (FinalWCt.CrAmt <>0 or FinalWCt.DrAmt <>0) 
 and  convert(date,FinalWCt.DocDate ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,FinalWCt.DocDate ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and FinalWCt.VCode in ('" + clsCommon.myCstr(VendorNo) + "')  UNION ALL
 Select  Reference_Doc_No,Emp_type,Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt,  CrAmt-DrAmt as Purchase, 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (
 select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN , TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end ) as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1
 and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and TSPL_PI_HEAD.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')) XXX UNION ALL
 select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then  Actual_Total_TDS  else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN ('I','D') AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and TSPL_REMITTANCE.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND 1=2
  and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and TSPL_REMITTANCE.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date  ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and TSPL_BANK_REVERSE.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select      (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  
 and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and VC_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''
 and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and VC_Code in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
  select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END as CrAmt, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Header.Adjustment_Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Header  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='P'  
 and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and TSPL_Payment_Adjustment_Header.Vendor_No in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
  select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as CrAmt,  CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='R'  
 and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and TSPL_Payment_Adjustment_Header.Vendor_No in ('" + clsCommon.myCstr(VendorNo) + "') UNION ALL
 Select max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
 select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
  and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PAYMENT_HEADER.payment_date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and tspl_payment_header.vendor_code in ('" + clsCommon.myCstr(VendorNo) + "') ) XX Group By XX.account, XX.DocNo UNION ALL
 select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName,TSPL_PAYMENT_HEADER.Payment_No as DocNo,'EXC' as [DocType],convert(date,Payment_Date, 103) as DocDate, case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(TSPL_PAYMENT_HEADER.cheque_No+'-'+ CONVERT(VARCHAR,Cheque_Date , 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, 0 as Balance_Amount,isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_PAYMENT_HEADER.Comp_Code from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_Vendor_Master ON TSPL_Vendor_Master.Vendor_Code =TSPL_PAYMENT_HEADER.Vendor_Code 
 left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_PAYMENT_HEADER.Payment_No 
 LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
 LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No  
 where TSPL_PAYMENT_HEADER.Posted=1 and  TSPL_PAYMENT_HEADER.Payment_Type  in ('PY','AD') 
 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0)  
  and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and TSPL_VENDOR_MASTER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')
 UNION ALL
 Select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN,TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
 'EXC'  as DocType  ,convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate,case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security ,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_PAYMENT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+ convert(varchar ,TSPL_PAYMENT_HEADER . Cheque_Date ,103)else '' end)) as ChequeDetails, TSPL_PAYMENT_HEADER.Currency_Code, 1 as ConvRate,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as DrAmt,  0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,
 'RV',0, isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType,TSPL_PAYMENT_HEADER.Comp_Code 
 from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_VENDOR_MASTER  ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code   
 LEFT OUTER JOIN TSPL_Vendor_TYPE_MASTER  ON TSPL_Vendor_TYPE_MASTER.Ven_Type_Code  = TSPL_VENDOR_MASTER.Ven_Type_Code   
 left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
 LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.PAYMENT_NO =TSPL_BANK_REVERSE.Document_No 
 LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
 LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No 
 where  TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P'
 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' and TSPL_VENDOR_MASTER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') 
 UNION ALL
 Select  max(FinalQryForAdvance.Reference_Doc_No) as Reference_Doc_No,max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account 
 , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code 
 from (select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code 
 left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code
 left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code
 left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary 
 where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   
  and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance
 group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code UNION ALL
Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
 ) XX Group By XX.account, XX.DocNo
 UNION ALL
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1
 UNION ALL
 select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   ) InnQuery 
  left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code   where InnQuery.DocNo not in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 and TSPL_PAYMENT_HEADER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')
 Union All
 Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "' 
 and TSPL_PAYMENT_HEADER.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "') ) )    ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo  WHERE TSPL_VENDOR_MASTER.Form_Type in ('VSP')) FinalVENDOR left outer jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in ('VSP'))  )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate >='" + clsCommon.GetPrintDate(fromdate, "dd/MMM/yyyy") + "'  and FINALCUSTOMERVENDOR.DocDate <='" + clsCommon.GetPrintDate(todate, "dd/MMM/yyyy") + "'  ) ) CustomerVendorFinal where 1=1   and CustomerVendorFinal.Vendor_Code in ('" + clsCommon.myCstr(VendorNo) + "')   and CustomerVendorFinal.Location in (" + StrPermission + ")  ) select 
  sum(XXXX.Closing) as OutStandingAmt 
from 
  (Select CTETemp.RowNo, CTETemp.RunDate ,CTETemp.fromdate,CTETemp.todate,CTETemp.Comp_Name,CTETemp.CompanyAdd, CTETemp.ACode , CTETemp.AName , CTETemp.DocNo,CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate,CASE WHEN CTETemp.DocNarr LIKE '%Opening Bal%' then 'OP' else CTETemp.DocType end as DocType,  CTETemp.DocNarr,CTETemp.ChequeDetails, CTETemp.Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt,SUM(DrAmt -CrAmt) Over (Partition by CTETemp.Vendor_Code ORDER BY RowNo) as [Closing], CTETemp.Location , CTETemp.SourceCode, CTETemp.Vendor_Code,CTETemp.AgainstInvoiceNo from CTETemp left outer join TSPL_CUSTOMER_MASTER on CTETemp.ACode =TSPL_CUSTOMER_MASTER.Cust_Code  left outer join TSPL_CUSTOMER_GROUP_MASTER  on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  =TSPL_CUSTOMER_MASTER.Cust_Group_Code left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code= CTETemp.Vendor_Code where TSPL_VENDOR_MASTER.Form_Type in ('VSP'))XXXX"
            Dim dblBal As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing))
            OSBal = dblBal
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return OSBal
    End Function

End Class
Public Class ItemValueClass
    Public itemCode As String = String.Empty
    Public itemDesc As String = String.Empty
    Public Unit_code As String = String.Empty
    Public IsFreshAmbient As String = String.Empty
    Public ShortDesc As String = String.Empty
    Public ItemRate As Double = 0
    Public FreshItem_QtyInCrates As Double = 0
    Public FreshItem_QtyInLitres As Double = 0
End Class
