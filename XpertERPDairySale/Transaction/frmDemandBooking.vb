' '' '' ''Created By richa
Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports XpertERPEngine

Public Class frmDemandBooking
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim gvFullMode As Boolean = False
    Public Shared LockUnlock As Integer = 0
    Dim EnableLocation As Boolean = False
    Dim EnableResetDemand As Boolean = False
    Dim ConvertPouchtoCrate As Boolean = False
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
            EnableLocation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableLocation, clsFixedParameterCode.EnableLocation, Nothing)) = 1, True, False)
            EnableResetDemand = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableResetDemand, clsFixedParameterCode.EnableResetDemand, Nothing)) = 1, True, False)
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
            SeparateDemandMilkandProduct = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeparateDemandMilkandProduct, clsFixedParameterCode.SeparateDemandMilkandProduct, Nothing)) = 1, True, False)
            ConvertPouchtoCrate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConvertPouchtoCrate, clsFixedParameterCode.ConvertPouchtoCrate, Nothing)) = 1, True, False)
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
            SaveData(0, False)
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
            If MyBase.isReverse Then

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
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
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
        Dim repoTripNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTripNo = New GridViewDecimalColumn()
        repoTripNo.FormatString = ""
        repoTripNo.HeaderText = "Trip No"
        repoTripNo.Name = colTripNo
        repoTripNo.Width = 50
        repoTripNo.ReadOnly = False
        repoTripNo.IsPinned = True
        repoTripNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTripNo)
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
        If gv1.CurrentColumn Is gv1.Columns(gv1.Columns.Count - 7) Then
        End If
    End Sub
    Private Sub setGridFocusHome()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            'gv1.CurrentColumn = gv1.Columns(7)
            gv1.Rows(intCurrRow).Cells(9).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(9).IsCurrent = True
        End If
    End Sub
    Private Sub setGridFocusEnd()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            gv1.Rows(intCurrRow).Cells(gv1.Columns.Count - 7).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(gv1.Columns.Count - 7).IsCurrent = True
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
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colbtncol).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colTripNo).Name)
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
                For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
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
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colMAmt).Name)
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
            txtDate.Enabled = True
            If EnableLocation Then
                txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Route_Master where Route_No='" + txtRouteNo.Value + "' "))
            Else
                txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
            If Not SettSeprateDemandForMorningEveningShift Then
                rbtnMorningEveningBoth.IsChecked = True
            End If
            RadGroupBox3.Enabled = True
            'rdbnFreshAmbientBoth.IsChecked = True
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
                    If e.Column.Index >= 9 And e.Column.Name <> colCrate And e.Column.Name <> colAmt And e.Column.Name <> colLitre And e.Column.Name <> colMAmt And e.Column.Name <> colPCount And e.Column.Name <> colPCount Then
                        'If isLoadData = False AndAlso (clsCommon.myLen(clsCommon.myCstr(txtDocNo.Value)) > 0) Then
                        If isLoadData = False Then
                            ''UpdateItemQtyAfterSave(gv1.CurrentRow.Index, gv1.CurrentColumn.Index)
                            UpdateAllTotals(False)
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
            UpdateAllTotals(False)
            isInsideLoadData = False
            Dim dblQuantityCount As Double = 0
            Dim dblQuantityMORNINGCount As Double = 0
            Dim dblQuantityEveningCount As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strItemValueExist As String = ""
                If clsCommon.myLen(gv1.Rows(ii).Cells(colItemExist)) > 0 Then
                    strItemValueExist = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemExist).Value)
                End If
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
            SaveData(0, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function SaveData(ByVal IsRepeatOrder As Integer, ByVal isQuickDemand As Boolean) As Boolean
        Try
            Dim qry As String = ""
            blnSaveTotalQTy = True
            'BookingStatus = 0
            Dim strPriceCode As String = String.Empty
            Dim LineNo As Integer = 1
            If (AllowToSave(Nothing)) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    ResetDemandOnSave(txtDocNo.Value)
                End If
                Dim obj As New clsDemandBookingSale()
                If SeparateDemandMilkandProduct Then
                    If IsRepeatOrder = 1 Then
                        obj.Document_Date = clsCommon.myCDate(txtDate.Value).AddDays(1)
                        Dim strQry1 As String = "select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103 )='" + clsCommon.GetPrintDate(obj.Document_Date) + "') and location_code='" + clsCommon.myCstr(txtLocation.Value) + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "'"
                        If rbtn_Fresh.IsChecked Then
                            strQry1 += " and ItemType='Fresh' "
                        ElseIf rbtn_Ambient.IsChecked Then
                            strQry1 += " and ItemType='Ambient' "
                        Else
                            strQry1 += " and ItemType='Both' "
                        End If
                        strQry1 += " And  IsIndividualCustomer=0 "
                        obj.Document_No = clsDBFuncationality.getSingleValue(strQry1)


                        If clsCommon.myLen(obj.Document_No) > 0 Then
                            isNewEntry = False
                        Else
                            If Not chkMorningPosted.Checked AndAlso Not chkEveningPosted.Checked Then
                                obj.Document_No = ""
                                isNewEntry = True
                            End If
                        End If
                    Else
                        obj.Document_No = txtDocNo.Value
                        obj.Document_Date = txtDate.Value
                    End If
                Else
                    If IsRepeatOrder = 1 Then
                        obj.Document_Date = clsCommon.myCDate(txtDate.Value).AddDays(1)
                        obj.Document_No = clsDBFuncationality.getSingleValue("select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103 )='" + clsCommon.GetPrintDate(obj.Document_Date) + "') and location_code='" + clsCommon.myCstr(txtLocation.Value) + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "' and IsIndividualCustomer=0 ")
                        If clsCommon.myLen(obj.Document_No) > 0 Then
                            isNewEntry = False
                        Else
                            If Not chkMorningPosted.Checked AndAlso Not chkEveningPosted.Checked Then
                                obj.Document_No = ""
                                isNewEntry = True
                            End If
                        End If
                    Else
                        obj.Document_No = txtDocNo.Value
                        obj.Document_Date = txtDate.Value
                    End If
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
                        Dim isCustRouteNotChanged As Boolean = True
                        For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            k = k + 1
                            If obj1 IsNot Nothing Then
                                If IsRepeatOrder = 1 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where Route_No='" + txtRouteNo.Value + "' and Cust_Code='" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) + "'")), clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) = CompairStringResult.Equal Then
                                        isCustRouteNotChanged = True
                                    Else
                                        isCustRouteNotChanged = False
                                    End If

                                End If
                                If (clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0) AndAlso isCustRouteNotChanged Then
                                    Dim objTr As New clsDemandBookingSaleDetail()
                                    objTr.Line_No = LineNo
                                    objTr.Trip_No = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colTripNo).Value)
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
                                                If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and IsReorder=1 and Status='N'")) = CompairStringResult.Equal Then
                                                    objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                Else
                                                    objTr.Qty = 0
                                                End If
                                            Else
                                                objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                            End If
                                        Else
                                            If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and Status='N'")) = CompairStringResult.Equal Then
                                                objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                            Else
                                                objTr.Qty = 0
                                            End If

                                        End If
                                        objTr.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                        objTr.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                        objTr.Rate = clsCommon.myCstr(obj1.ItemRate)
                                        objTr.ItemNetAmount = clsCommon.myCdbl(objTr.Rate * objTr.Qty)
                                        objTr.Vehicle_Code = clsCommon.myCstr(txtVehicleNo.Value)
                                        If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                                If IsRepeatOrder = 1 Then
                                                    If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                                                        If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and IsReorder=1")) = CompairStringResult.Equal Then
                                                            objTr.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                        End If
                                                    Else
                                                        objTr.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                    End If
                                                Else
                                                    objTr.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                End If
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
                                                If IsRepeatOrder = 1 Then
                                                    If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                                                        If clsCommon.CompairString(objTr.Cust_Code, clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where cust_code='" + clsCommon.myCstr(objTr.Cust_Code) + "' and IsReorder=1")) = CompairStringResult.Equal Then
                                                            objTr.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                                        End If
                                                    Else
                                                        objTr.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                                    End If
                                                Else
                                                    objTr.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                                End If
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

                        'isSave = obj.SaveData(obj, False)
                        'isSave = clsDemandBookingSaleDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, Nothing, obj.Location_Code, obj.ShiftType, isNewEntry)
                        If (obj.SaveData(obj, False)) = True Then
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
                        If Not isQuickDemand Then
                            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                            LoadData(obj.Document_No, NavigatorType.Current)
                        End If
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
                txtDate.Enabled = False
                isNewEntry = False
                btn_TSCancel.Enabled = True
                btn_Gatepass.Enabled = True
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
                    If SeparateDemandMilkandProduct Then
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()
                    End If
                ElseIf clsCommon.CompairString(obj.ItemType, "Ambient") = CompairStringResult.Equal Then
                    rbtn_Ambient.IsChecked = True
                    If SeparateDemandMilkandProduct Then
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()
                    End If
                Else
                    rdbnFreshAmbientBoth.IsChecked = True
                    If SeparateDemandMilkandProduct Then
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()
                    End If
                End If
                If Not SeparateDemandMilkandProduct Then
                    LoadBlankGrid()

                End If

                setCustomerDetail(TxtCity.Value, txtRouteNo.Value, True)
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
                                For columns = 9 To gv1.Columns.Count - 7
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.ShiftType, "Morning") = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                        gv1.Rows(dblrows).Cells(colTripNo).Value = objTr.Trip_No

                                        dblMorningCount = 1
                                    ElseIf clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.ShiftType, "Evening") = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                        gv1.Rows(dblrows).Cells(colTripNo).Value = objTr.Trip_No

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
                UpdateAllTotals(True)
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
            Dim qry As String = "select TSPL_DEMAND_BOOKING_MASTER.Document_No as DocumentNo,convert(varchar(12),TSPL_DEMAND_BOOKING_MASTER.Document_date,103) as DocumentDate,TSPL_DEMAND_BOOKING_MASTER.ShiftType,TSPL_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_DEMAND_BOOKING_MASTER.Location_Code as [Location Code],TSPL_DEMAND_BOOKING_MASTER.City_Code as [City Code],TripNo AS [Trip No],TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer as [Individual Cust],case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_DEMAND_BOOKING_MASTER "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentNo", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DEMAND_BOOKING_MASTER.Document_date "), NavigatorType.Current)
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
            Dim NextDayDocNo As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103 )='" + clsCommon.GetPrintDate(txtDate.Value.AddDays(1)) + "') and location_code='" + clsCommon.myCstr(txtLocation.Value) + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "' and IsIndividualCustomer=0 ")
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
                    qry = "select Posted from TSPL_Demand_BOOKING_MAstER where Document_No='" + NextDayDocNo + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                        Throw New Exception("Please Reverse/Unpost Document No: [ " + NextDayDocNo + " ]")
                    End If
                    Dim dt As DataTable = Nothing
                    '' to check gatepass or truck sheet generated
                    Dim strDocNoForGatePass As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & NextDayDocNo & "' and IsGatePassGenerated='Y' "))
                    Dim strDocNoForTrucksheet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Document_No  from TSPL_DEMAND_BOOKING_DETAIL where document_No='" & NextDayDocNo & "' and  IsTruckSheetGenerated ='Y'  "))
                    If clsCommon.myLen(clsCommon.myCstr(strDocNoForGatePass)) > 0 Then
                        Throw New Exception("Demand cannot be reverse because Next Day Demand Gate Pass has generated.")
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(strDocNoForTrucksheet)) > 0 Then
                        Throw New Exception("Demand cannot be reverse because Next Day Demand Gate Pass has generated.")
                    End If
                End If
                If clsDemandBookingSale.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    If clsCommon.myLen(NextDayDocNo) > 0 Then
                        qry = "delete from TSPL_BOOKING_DETAIL where document_No in (select document_No from tspl_booking_matser where Against_DemandBooking_No='" + NextDayDocNo + "') "
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "delete from TSPL_BOOKING_MATSER where Against_DemandBooking_No='" + NextDayDocNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        qry = "delete from TSPL_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_DEMAND_BOOKING_DETAIL where Document_No='" + NextDayDocNo + "'  ) "
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
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
                If rbtn_Ambient.IsChecked Then
                    HideUnhideRowsAndColumnsOFGrid()
                End If
                'HideUnhideRowsAndColumnsOFGrid()
            End If
            btnPrint.Enabled = True
            lblTotalLitre.Text = ""
            lblTotalCrate.Text = ""
            lblDocumentAmt.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim isClicked As Boolean = isButtonClicked
            RouteData(isClicked, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RouteData(ByVal isClicked As Boolean, ByVal isQuickDemand As Boolean)
        Try
            Dim qry As String = String.Empty
            Dim ItemType As String = ""
            If SeparateDemandMilkandProduct Then
                qry = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            Else
                qry = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            End If
            If Not isQuickDemand Then
                txtRouteNo.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isClicked)
            End If
            If SeparateDemandMilkandProduct Then
                If rbtn_Fresh.IsChecked Then
                    ItemType = "Milk"
                ElseIf rbtn_Ambient.IsChecked Then
                    ItemType = "Product"
                ElseIf rdbnFreshAmbientBoth.IsChecked Then
                    ItemType = "Both"
                End If
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
                    If clsCommon.CompairString(DRTItem, "Milk") = CompairStringResult.Equal Then
                        rbtn_Fresh.IsChecked = True
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()
                    ElseIf clsCommon.CompairString(DRTItem, "Product") = CompairStringResult.Equal Then
                        rbtn_Ambient.IsChecked = True
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()
                    ElseIf clsCommon.CompairString(DRTItem, "Both") = CompairStringResult.Equal Then
                        rdbnFreshAmbientBoth.IsChecked = True
                        RadGroupBox1.Enabled = False
                        LoadBlankGrid()
                        HideUnhideRowsAndColumnsOFGrid()

                    End If
                Else

                    Throw New Exception("Distributor Route Not Tagged!")
                End If
            End If
                lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRouteNo.Value, Nothing))
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
            Dim shiftType As String = ""
            If rbtnMorning.IsChecked Then
                shiftType = "Morning"
            Else
                shiftType = "Evening"
            End If
            qry = "Select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No = '" & txtRouteNo.Value & "' and  CONVERT(varchar, CAST(Document_Date AS datetime), 103) ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "' and ShiftType = '" & shiftType & "' and IsIndividualCustomer=0"
            If SeparateDemandMilkandProduct Then
                If rbtn_Fresh.IsChecked Then
                    qry += " and ItemType='Fresh' "
                ElseIf rbtn_Ambient.IsChecked Then
                    qry += " and ItemType='Ambient' "
                ElseIf rdbnFreshAmbientBoth.IsChecked Then
                    qry += " and ItemType='Both' "
                End If
            End If
            Dim DocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(DocNo) > 0 Then
                LoadData(DocNo, NavigatorType.Current)
            Else
                txtDate.Enabled = False
            End If
            SetRouteColumns()
            RefreshFormName()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetRouteColumns()
        Try
            If rbtn_Fresh.IsChecked OrElse rdbnFreshAmbientBoth.IsChecked Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select isnull(Entry_UOM,0) as Entry_UOM from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
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
            setCustomerDetail(TxtCity.Value, txtRouteNo.Value, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub setCustomerDetail(ByVal strCityCode As String, ByVal strtRouteCode As String, ByVal isLoadData As Boolean)
        Try
            Dim MainQry As String = ""
            Dim qry As String = ""

            qry = "select cust_code,Customer_name,display_seq from TSPL_CUSTOMER_MASTER where route_no='" + strtRouteCode + "' and City_code='" + strCityCode + "' and  TSPL_CUSTOMER_MASTER.Status='N' "
            If chkIndividualCustomer.Checked = True Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code ='" & txtCustomerNo.Value & "'"
            End If
            ' qry += " order by isnull(TSPL_CUSTOMER_MASTER.display_seq,0)  "
            If isLoadData Then
                qry += "union 
select TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name,TSPL_CUSTOMER_MASTER.display_seq from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
where TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "'
group by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.display_seq"
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
                    Dim flagE As Boolean = True
                    Dim flagM As Boolean = True
                    If SettSeprateDemandForMorningEveningShift Then
                        flagE = rbtnEvening.IsChecked
                        flagM = rbtnMorning.IsChecked
                        RadGroupBox3.Enabled = False
                    End If
                    If flagE Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbtncol).Value = "Reset "
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTripNo).Value = 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("cust_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).Value = "Evening"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShiftName).ReadOnly = True
                        i = i + 1
                        gv1.Rows.AddNew()
                    End If
                    If flagM Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbtncol).Value = "Reset "
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTripNo).Value = 1
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
                                For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                                    If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code).ToUpper, "POUCH") = CompairStringResult.Equal Then
                                        gv1.Rows(n).Cells(dblcolumns).Style.DrawFill = True
                                        gv1.Rows(n).Cells(dblcolumns).Style.CustomizeFill = True
                                        gv1.Rows(n).Cells(dblcolumns).Style.BackColor = Color.LightGreen
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
            Dim k As Integer = 1
            For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
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
                        ElseIf rdbnFreshAmbientBoth.IsChecked Then

                            gv1.Columns(dblcolumns).IsVisible = True
                            gv1.Columns(colPAmt).IsVisible = True
                            gv1.Columns(colPCount).IsVisible = True
                            gv1.Columns(colMAmt).IsVisible = True
                            gv1.Columns(colCrate).IsVisible = True
                            gv1.Columns(colLitre).IsVisible = True
                        End If
                    End If
                End If
                k = k + 1
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
    Private Sub UpdateAllTotals(ByVal isLoad As Boolean)
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
                For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
                    Dim obj1 As ItemValueClass = Nothing
                    Try
                        obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    Catch ex As Exception
                    End Try
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                    TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    obj1.FreshItem_QtyInCrates = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    'gv1.Rows(gv1.Rows.Count - 1).Cells(dblcolumns).Value = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                Else
                                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                    If ItemCrateType = 1 Then
                                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                            Dim DispatchQty As Double = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                                            If ConvertPouchtoCrate Then
                                                If DispatchQty > (CrateConvFactor / 2) Then
                                                    dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                                                Else
                                                    dblTotalCrateRowWise = 1
                                                End If
                                            Else
                                                If DispatchQty > (CrateConvFactor / 2) Then
                                                    dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                                                Else
                                                    dblTotalCrateRowWise = 0
                                                End If
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
                            If Not isLoad Then
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
                            Else
                                qry = "select item_Rate as Item_Basic_Price from TSPL_DEMAND_BOOKING_DETAIL where  Document_No='" + txtDocNo.Value + "' and Item_Code='" + obj1.itemCode + "' and Unit_code='" + obj1.Unit_code + "'"

                            End If
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
                gv1.Rows(dblrows).Cells(colMAmt).Value = clsCommon.myCdbl(dblTotalMAmt)
                gv1.Rows(dblrows).Cells(colPCount).Value = clsCommon.myCdbl(dblTotalPCount)
                gv1.Rows(dblrows).Cells(colPAmt).Value = clsCommon.myCdbl(dblTotalPAmt)
                gv1.Rows(dblrows).Cells(colAmt).Value = clsCommon.myCdbl(dblTotalDocAmtRowWise)
                If clsCommon.myLen(gv1.Rows(dblrows).Cells(colItemExist)) > 0 Then
                    gv1.Rows(dblrows).Cells(colItemExist).Value = strItemValueExist
                End If
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
            UpdateColumnTotal()
            gv1.Rows(gv1.Rows.Count - 1).IsPinned = True
            gv1.Rows(gv1.Rows.Count - 1).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub UpdateColumnTotal()
        Try
            Dim TotalQty As Double = 0
            'For dbrows1 As Integer = 0 To gv1.Rows.Count - 1
            For dblcolumns As Integer = 9 To gv1.Columns.Count - 1
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
    Private Sub btnUpdateCrateAndAmt_Click(sender As Object, e As EventArgs) Handles btnUpdateCrateAndAmt.Click
        Try
            UpdateAllTotals(False)
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
                If clsCommon.myLen(e.RowElement.RowInfo.Cells(colBookingCreatedFor3Days)) > 0 Then
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
        End If
        If e.RowElement.RowInfo.IsCurrent Then
            e.RowElement.DrawFill = True
            e.RowElement.BackColor = Color.LightGreen
        Else
            e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
            e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
        End If
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
            SaveData(0, True)
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
            'Dim custCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where route_no='" + txtRouteNo.Value + "' and IsDistributor='Y'"))
            Dim StrQry As String = "select  top 1 x.Cust_Code 
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
                    If rbtnMorningEveningBoth.IsChecked Then
                        Throw New Exception("Please select morning/evening")
                    End If
                    If (clsDemandBookingSale.PostData(MyBase.Form_ID, txtDocNo.Value, IIf(rbtnMorning.IsChecked, 1, 2))) Then
                        msg = "Successfully posted"
                        common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                        If chkIndividualCustomer.Checked = False Then
                            If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandAll, clsFixedParameterCode.ApplyDemandAll, Nothing)) = 1 Or clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandCustomerWise, clsFixedParameterCode.ApplyDemandCustomerWise, Nothing)) = 1 Then
                                SaveData(1, True)
                            End If
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
                If rbtn_Fresh.IsChecked Then
                    HideUnhideRowsAndColumnsOFGrid()
                End If

            End If
            btnPrint.Enabled = True
            txtPCount.Text = ""
            txtPAmt.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rdbnBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbnFreshAmbientBoth.ToggleStateChanged
        Try
            If isInsideLoadData = False Then
                If rdbnFreshAmbientBoth.IsChecked Then
                    HideUnhideRowsAndColumnsOFGrid()
                End If
                'HideUnhideRowsAndColumnsOFGrid()
            End If
            btnPrint.Enabled = False
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
    Private Sub TruckSheetExcel(ByVal TripNo As String)
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
    WHERE TSPL_DEMAND_BOOKING_MASTER.Document_No='" + txtDocNo.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'" + IIf(clsCommon.CompairString(TripNo, "ALL") = CompairStringResult.Equal, "", "and TSPL_DEMAND_BOOKING_DETAIL.Trip_No='" + TripNo + "'") + " ) XXXFirst Group By
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
            arrHeader.Add("Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy")) & "   Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "   Trip No : " & clsCommon.myCstr(TripNo))
            arrHeader.Add("Route : " & lblRouteDesc.Text & "    City : " & lblCityName.Text & "   Distributor : " & lblTransporterName.Text)
            'arrHeader.Add("Doc Date : " & clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy")))
            'arrHeader.Add("Route : " & lblRouteDesc.Text)
            'arrHeader.Add("City : " & lblCityName.Text)
            'arrHeader.Add("Shift : " & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
            'arrHeader.Add("Distributor : " & lblTransporterName.Text)
            'arrHeader.Add("Trip : " & clsCommon.myCstr(txtTripNo.Text))
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
            'btn_Gatepass.Enabled = False
            'btn_GPCancel.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Sub PrintGatePass(ByVal StrFormType As String, ByVal StrDocCode As String, ByVal StrShift As String)
        'clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_DETAIL set IsGatePassGenerated='Y' where " + IIf(StrFormType = "DB", "TSPL_DEMAND_BOOKING_DETAIL.Document_No", "TSPL_DEMAND_BOOKING_DETAIL.GPCode") + "'='" & StrDocCode & "' and ShiftType='" & StrShift & "'")
        Dim Qry As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin,TSPL_COMPANY_MASTER.Access_Officer as FSSAI_LIC_NO
                  ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc,'" + objCommonVar.CurrentUser + "' as Currentuser
                  ,Main_Final.Distributor,'" & StrShift & "' shiftType,Main_Final.City_Name,Main_Final.Demand_No,Main_Final.Demand_Date,Main_Final.Route_No,Main_Final.Route_Desc ,Main_Final.Vehicle_Desc
                  ,Main_Final.Item_alies_name,Main_Final.UOM,Main_Final.unit_code_result,Main_Final.Crate_Qty,Main_Final.Pouch_Qty,Main_Final.Loose_Qty,TotalLtr_ItemWise,ItemNetAmount
                  ,Main_Final.Production_Remarks,Main_Final.Trip
                  from (select max(isnull(LEFT((el.files) ,LEN((el.files ))-1),'NoFile')) as Trip, max(TSPL_VENDOR_MASTER.vendor_name) as Distributor,
                  max(TSPL_DEMAND_BOOKING_MASTER.shiftType) as shiftType,
                  max(TSPL_city_MASTER.City_Name) as City_Name,
                  max(TSPL_DEMAND_BOOKING_MASTER.Comp_Code) as Comp_Code,
                  max(TSPL_DEMAND_BOOKING_MASTER.location_code) as location_code
                  ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,max(convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Demand_Date ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,max(isnull(TSPL_ROUTE_MASTER.Route_Desc,'')) as Route_Desc,
                  max(isnull(TSPL_VEHICLE_MASTER.Description,'')) as Vehicle_Desc ,max(TSPL_ITEM_MASTER.alies_name) as Item_alies_name,
                  max(TSPL_ITEM_MASTER.Unit_Code) as UOM,CASE WHEN max(TSPL_ITEM_MASTER.Unit_Code) = 'crate' OR max(TSPL_ITEM_MASTER.Unit_Code) = 'pouch' THEN 'Crate/Pouch' ELSE max(TSPL_ITEM_MASTER.Unit_Code) END AS unit_code_result,
                  sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Crate_Qty
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
                  cross apply(select distinct  convert(varchar,TSPL_DEMAND_BOOKING_DETAIL.Trip_No) + ',' as [text()] from TSPL_DEMAND_BOOKING_DETAIL where Document_No= '" + StrDocCode + "' 
                  FOR XML PATH(''))el(files)

                  WHERE " + IIf(StrFormType = "DB", "TSPL_DEMAND_BOOKING_DETAIL.Document_No", "TSPL_DEMAND_BOOKING_DETAIL.GPCode") + " = '" + StrDocCode + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & StrShift & "'
                   group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Route_No
                  ) as Main_Final
                  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code
                   LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Main_Final.location_code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePassItemWiseUDP", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            Else
                frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePassItemWise", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
            End If
            'frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePassItemWise", "Gate Pass", clsCommon.myCDate(dt.Rows(0)("Demand_Date")), "rptCompanyAddress.rpt")
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            If e.Column.Name = colCustCode Then
                e.CellElement.Font = New Font("Arial", 10, FontStyle.Bold)
            End If
            If e.Column.Index >= 9 And e.Column.Name <> colCrate And e.Column.Name <> colAmt And e.Column.Name <> colLitre And e.Column.Name <> colMAmt And e.Column.Name <> colPCount And e.Column.Name <> colPCount Then
                ' If isLoadData = False Then
                If (chkEveningGatepassTruckSheetGenerated.Checked OrElse chkEveningPosted.Checked) And clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colShiftName).Value), "Evening ") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(e.ColumnIndex).ReadOnly = True
                End If
                If (chkMorningGatepassTruckSheetGenerated.Checked OrElse chkMorningPosted.Checked) And clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colShiftName).Value), "Morning ") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(e.ColumnIndex).ReadOnly = True
                End If
                e.CellElement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
            If e.Column.Index >= 9 AndAlso gv1.Rows.Count > 0 Then
                If e.Column.IsCurrent Then
                    e.CellElement.DrawFill = True
                    e.CellElement.BackColor = Color.LightGreen
                Else
                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                End If
                If e.CellElement.RowInfo.IsCurrent Then
                    e.CellElement.RowElement.BackColor = Color.LightGreen
                    e.CellElement.RowElement.DrawFill = True
                Else
                    e.CellElement.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                End If
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
                    setCellBorders(secondCell, Color.FromArgb(209, 225, 245)) '117, 230, 218
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

        Dim TripNO As String = ""
        Dim qry As String = "select distinct CAST(Trip_No AS VARCHAR(10)) as Code from TSPL_DEMAND_BOOKING_DETAIL where Document_No='" + txtDocNo.Value + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dr = dt.NewRow()
        dr("Code") = "ALL"
        'dr("Name") = "ALL"
        dt.Rows.Add(dr)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            TripNO = clsCommon.myCstr(dt.Rows(0)("Code"))
            If dt.Rows.Count > 1 Then
                Dim frmFC As New FrmFreeComboBox
                frmFC.ComboSource = dt
                frmFC.ComboValueMember = "Code"
                frmFC.ComboDisplayMember = "Code"
                frmFC.LabelCaption = "Trip No"
                frmFC.ShowDialog()
                TripNO = frmFC.strRetValue
            End If
            If clsCommon.myLen(TripNO) > 0 Then
                'If clsCommon.myLen(txtMCC.Value) > 0 Then
                '    RefreshMCCCollectionDetail(txtMCC.Value, strMilkType, Nothing)
                'End If
                'LoadTransactionData(strMilkType)
            End If
        End If

        TruckSheet(EnumExportTo.Excel, TripNO)
    End Sub
    Private Sub rmi_TS_PDF_Click(sender As Object, e As EventArgs) Handles rmi_TS_PDF.Click
        TruckSheet(EnumExportTo.PDF, "ALL")
    End Sub
    Private Sub TruckSheet(ByVal exporter As EnumExportTo, ByVal TripNo As String)
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
                TruckSheetExcel(TripNo)
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
            myMessages.blankValue(Me, "Booking not found to Print", Me.Text)
        End If
        If btnSave.Enabled = True Then
            SaveData(0, True)

        End If
        Dim qry As String = Nothing
        Dim SubRptQry As String = Nothing
        Dim ShiftType As String = ""
        Dim shiftAMPMType As String = ""
        Dim PreshiftAMPMType As String = ""
        Dim Previous_Shift As String = ""
        Dim Previous_Date As String
        Dim ItemCount As Double = 0
        If rbtnEvening.IsChecked = True Then
            ShiftType = "Evening"
            shiftAMPMType = "PM"
            PreshiftAMPMType = "AM"
            Previous_Shift = "Morning"
            Previous_Date = clsCommon.myCDate(txtDate.Value).AddDays(1)
        Else
            ShiftType = "Morning"
            Previous_Shift = "Evening"
            shiftAMPMType = "AM"
            PreshiftAMPMType = "PM"
            ' Previous_Date = clsDBFuncationality.getSingleValue("select CONVERT(varchar, DATEADD(DAY, -1, convert(Nvarchar, '" & txtDate.Value & "' ,112)),21) as Previous_Date")
            Previous_Date = clsCommon.myCDate(txtDate.Value).AddDays(-1)
        End If

        Dim Comp_Name As String = clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select Booking No")
            End If
            Dim Posted As String = String.Empty
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim strItemQry As String = "select count(distinct TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as items from TSPL_DEMAND_BOOKING_DETAIL
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_DETAIL.Document_No='" + txtDocNo.Value + "' "
                If rbtn_Fresh.IsChecked Then
                    strItemQry += " and TSPL_ITEM_MASTER.Is_FreshItem=1 "
                ElseIf rbtn_Ambient.IsChecked Then
                    strItemQry += " and  TSPL_ITEM_MASTER.Is_Ambient=1 "
                End If

                ItemCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strItemQry))
                Posted = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when posted=0 then 'Pending' else 'Approved' end from TSPL_DEMAND_BOOKING_MASTER where Document_No='" + clsCommon.myCstr(txtDocNo.Value) + "'"))
            Else
                Throw New Exception("Demand Not Found!")
            End If
            If rbtn_Fresh.IsChecked Then
                qry = "  select xx.*
 ,case when xx.SNO=1 then ( case when xx.ShiftType='Morning' then (isnull((select sum(ItemNetAmount) as netamt  from TSPL_DEMAND_BOOKING_MASTER left join  TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No where  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ShiftType + "'  and ( CONVERT( date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtDate.Value) + "')  and TSPL_DEMAND_BOOKING_MASTER.Route_No = '" + clsCommon.myCstr(txtRouteNo.Value) + "'  and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=xx.Cust_Code ),0 ) + isnull((xx.PrevItemNetAmount),0) ) else 0 end) else 0 end as AmountBE,
 case when xx.SNO=1 then xx.Crate_Collect else 0 end as TotalCollectCrate,case when xx.SNO=1 then (case when xx.ShiftType='Morning' then (isnull(tcs.TCSAmount,0)+isnull(prevtcs.pTCSAmount,0)) else 0 end) else 0 end as TotalTCSAmt,'" + Posted + "' as DocStatus
  from ( select XXFinal.Cust_Code as Cust_Code, max(XXFinal.ShiftType) as ShiftType, XXFinal.Sku_Seq as Sku_Seq ,max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Short_Description) as Short_Description, sum(XXFinal.Qty) as Qty, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.Crate) as Crate, max(XXFinal.Pouch) as Pouch, sum(XXFinal.ItemNetAmount) as ItemNetAmount,max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc,max(XXFinal.PrevCrate) as Crate_Collect, max(XXFinal.CompanyName) as CompanyName, max(XXFinal.TranspoterName) as TranspoterName, max(XXFinal.DriverName) as DriverName,max(XXFinal.Vehicle_No) as Vehicle_No, max(XXFinal.Item_Rate) as Item_Rate, max(XXFinal.CFForLTR) as CFForLTR, max(XXFinal.Conversion_Factor) as Conversion_Factor, sum(XXFinal.QTYLtr) as QTYLtr,max(XXFinal.PrevItemNetAmount) as PrevItemNetAmount,ROW_NUMBER() over (Partition by Cust_Code order by Cust_Code) as SNO
from
(
select 
  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, 
  TSPL_DEMAND_BOOKING_DETAIL.ShiftType, 
  TSPL_ITEM_MASTER.Sku_Seq, 
  TSPL_DEMAND_BOOKING_MASTER.Document_Date, 
  TSPL_ITEM_MASTER.Short_Description, 
  TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty, 
  0 as PrevQty,
  TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise Else 0 End As Crate, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As PrevCrate, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End As Pouch, 
    Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As PrevPouch,
  TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount, 
    0 as PrevItemNetAmount,
  TSPL_DEMAND_BOOKING_MASTER.Route_No, 
  TSPL_ROUTE_MASTER.Route_Desc, 
  Isnull(
    TSPL_COMPANY_MASTER.Comp_Name, 'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.'
  ) as CompanyName,
  TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, 
  TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
  TSPL_DEMAND_BOOKING_DETAIL.Item_Rate, 
  ITEMDETAIL.CFForLTR, 
  TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 
  Convert(
    decimal(18, 2), 
    (
      TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor
    )/ ITEMDETAIL.CFForLTR
  ) As QTYLtr
from 
  TSPL_DEMAND_BOOKING_DETAIL 
  Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
  Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
  Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
  And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
  Left Join (
    select 
      Conversion_factor AS CFForLTR, 
      TSPL_ITEM_UOM_DETAIL.Item_code 
    from 
      TSPL_ITEM_UOM_DETAIL 
    where 
      UOM_code = 'LTR'
  ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
  Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
  Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
  Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
  Left Join TSPL_COMPANY_MASTER on TSPL_DEMAND_BOOKING_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code
where 
  TSPL_DEMAND_BOOKING_DETAIL.ShiftType = '" + ShiftType + "' 
  and (
    CONVERT(
      date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 
      103
    )= '" + clsCommon.GetPrintDate(txtDate.Value) + "'
  ) 
  and TSPL_DEMAND_BOOKING_MASTER.Route_No = '" + clsCommon.myCstr(txtRouteNo.Value) + "' "
                If chkIndividualCustomer.Checked Then
                    If clsCommon.myLen(txtCustomerNo.Value) > 0 Then
                        qry += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" + txtCustomerNo.Value + "'"
                    End If
                Else
                    qry += " and TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0"
                End If
                qry += "  union all
  select 
  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, 
  '" + ShiftType + "'  as ShiftType, 
  TSPL_ITEM_MASTER.Sku_Seq, 
  '" + clsCommon.GetPrintDate(txtDate.Value) + "' as Document_Date, 
  TSPL_ITEM_MASTER.Short_Description, 
  0 as Qty, 
  TabCustWiseCrate.Qty as PrevQty,
  TSPL_DEMAND_BOOKING_DETAIL.Unit_code, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then 0 Else 0 End As Crate, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' Then TabCustWiseCrate.TotalCrates_ItemWise Else 0 End As PrevCrate, 
  Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then 0 Else 0 End As Pouch, 
    Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' Then TabCustWiseCrate.qty Else 0 End As PrevPouch,
  0 as ItemNetAmount, 
    NetAmount as PrevItemNetAmount,
  TSPL_DEMAND_BOOKING_MASTER.Route_No, 
  TSPL_ROUTE_MASTER.Route_Desc, 
  Isnull(
    TSPL_COMPANY_MASTER.Comp_Name, 'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.'
  ) as CompanyName,
  TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, 
  TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
  TSPL_DEMAND_BOOKING_DETAIL.Item_Rate, 
  ITEMDETAIL.CFForLTR, 
  TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 
  0 As QTYLtr
from 
  (
    select 
      ROW_NUMBER() over (
        PARTITION BY xx.Cust_Code 
        order by 
          xx.Cust_Code, 
          xx.ORDCol desc
      ) as SNO, 
      xx.Cust_Code, 
      xx.ORDCol, 
      sum(xx.TotalCrates_ItemWise) as TotalCrates_ItemWise, 
      sum(xx.TotalLtr_ItemWise) as TotalLtr, 
      sum(xx.ItemNetAmount) as NetAmount, 
	  sum(xx.qty) as Qty
    from 
      (
        select 
          innBD.Cust_Code, 
          convert(
            varchar, InnBM.Document_Date, 102
          )+ case when innBD.ShiftType = 'Evening' then 'B' else 'A' end as ORDCol, 
          innBD.TotalCrates_ItemWise, 
          innBD.TotalLtr_ItemWise, 
          innBD.ItemNetAmount,innBD.qty
        from 
          TSPL_DEMAND_BOOKING_MASTER as InnBM 
          left outer join TSPL_DEMAND_BOOKING_DETAIL innBD on innBD.Document_No = InnBM.Document_No 
        where 
          2 = 2  "
                If rbtnMorning.IsChecked Then
                    qry += " and innBD.ShiftType='" + Previous_Shift + "' and ( CONVERT(date, InnBM.Document_Date, 103)= '" + clsCommon.GetPrintDate(txtDate.Value.AddDays(-1)) + "') "
                ElseIf rbtnEvening.IsChecked Then
                    qry += " and innBD.ShiftType='" + Previous_Shift + "' and CONVERT(date, InnBM.Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value) + "'" ' or CONVERT(date, InnBM.Document_Date,103)<'" + clsCommon.GetPrintDate(txtDate.Value) + "') "
                End If
                qry += " and innBD.Cust_Code is not null ) xx  
    group by 
      xx.Cust_Code, 
      xx.ORDCol
  )  TabCustWiseCrate 
    left join TSPL_Demand_Booking_Detail on TabCustWiseCrate.cust_Code=TSPL_Demand_Booking_Detail.cust_Code and TabCustWiseCrate.SNO=1
  Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
  Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
  Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
  And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
  Left Join (
    select 
      Conversion_factor AS CFForLTR, 
      TSPL_ITEM_UOM_DETAIL.Item_code 
    from 
      TSPL_ITEM_UOM_DETAIL 
    where 
      UOM_code = 'LTR'
  ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
  Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
  Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
  Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
  Left Join TSPL_COMPANY_MASTER on TSPL_DEMAND_BOOKING_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code 
  where TSPL_ROUTE_MASTER.Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "'"
                If chkIndividualCustomer.Checked Then
                    If clsCommon.myLen(txtCustomerNo.Value) > 0 Then
                        qry += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" + txtCustomerNo.Value + "'"
                    End If
                End If
                qry += " )XXFinal
  where XXFinal.Cust_Code in (select distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No where TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + " ' and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code is not null )
            Group by XXFinal.Cust_Code,XXFinal.Sku_Seq )xx 
   left join (
 select 
 sum(XYZ.TCSAmount) as TCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No
	  from(
 select TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER
left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
where TSPL_BOOKING_MATSER.GatePass_Type='" + shiftAMPMType + "' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtDate.Value) + "') 
group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No) XYZ
group by XYZ.Cust_Code
  ) as tcs on xx.Cust_Code=tcs.Cust_Code
left join (
  select 
 sum(XYZ.pTCSAmount) as pTCSAmount,XYZ.Cust_Code,max(XYZ.Against_DemandBooking_No) as Against_DemandBooking_No
	  from(select (TSPL_BOOKING_MATSER.TCSAmount) as pTCSAmount ,(TSPL_BOOKING_MATSER.Against_DemandBooking_No) Against_DemandBooking_No,TSPL_BOOKING_DETAIL.Cust_Code
		  from TSPL_BOOKING_MATSER
left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
where TSPL_BOOKING_MATSER.GatePass_Type='" + PreshiftAMPMType + "' and  (CONVERT(date, TSPL_BOOKING_MATSER.Document_Date,103)= '" + IIf(rbtnMorning.IsChecked, clsCommon.GetPrintDate(txtDate.Value.AddDays(-1)), clsCommon.GetPrintDate(txtDate.Value)) + "') 
group by TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.TCSAmount,TSPL_BOOKING_MATSER.Against_DemandBooking_No
) XYZ
group by XYZ.Cust_Code
) as prevtcs on xx.Cust_Code=prevtcs.Cust_Code  
"
            ElseIf rbtn_Ambient.IsChecked Then
                qry = "  select XXFinal.Cust_Code as Cust_Code, max(XXFinal.ShiftType) as ShiftType, XXFinal.Sku_Seq as Sku_Seq ,max(XXFinal.Document_Date) as Document_Date, max(XXFinal.Short_Description)+' '+ max(XXFinal.Unit_code) as Short_Description, sum(XXFinal.Qty) as Qty, max(XXFinal.Unit_code) as Unit_code, sum(XXFinal.ItemNetAmount) as ItemNetAmount,max(XXFinal.Route_No) as Route_No, max(XXFinal.Route_Desc) as Route_Desc, max(XXFinal.CompanyName) as CompanyName, max(XXFinal.TranspoterName) as TranspoterName, max(XXFinal.DriverName) as DriverName,max(XXFinal.Vehicle_No) as Vehicle_No, max(XXFinal.Item_Rate) as Item_Rate, max(XXFinal.CFForKG) as CFForKG, max(XXFinal.Conversion_Factor) as Conversion_Factor,'" + Posted + "' as DocStatus
from
( select 
  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,  TSPL_DEMAND_BOOKING_DETAIL.ShiftType,  TSPL_ITEM_MASTER.Sku_Seq,TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty, 
TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,Isnull(TSPL_COMPANY_MASTER.Comp_Name, 'Jaipur Zila Dugdh Utpadak Sahakari Sangh Ltd.') as CompanyName,TSPL_TRANSPORT_MASTER.Transporter_Name as TranspoterName, TSPL_VEHICLE_MASTER.DriverName,TSPL_VEHICLE_MASTER.Number as Vehicle_No, 
  TSPL_DEMAND_BOOKING_DETAIL.Item_Rate,ITEMDETAIL.CFForKG,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,Convert(decimal(18, 2),(TSPL_DEMAND_BOOKING_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ ITEMDETAIL.CFForKG) As QTYKg
from 
  TSPL_DEMAND_BOOKING_DETAIL 
  Left join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
  Left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
  Left Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
  And TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
  Left Join (
    select 
      Conversion_factor AS CFForKG, 
      TSPL_ITEM_UOM_DETAIL.Item_code 
    from 
      TSPL_ITEM_UOM_DETAIL 
    where 
      UOM_code = 'KG'
  ) as ITEMDETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = ITEMDETAIL.Item_code 
  Left Join TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id 
  Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
  Left Join TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id = TSPL_TRANSPORT_MASTER.Transport_Id 
  Left Join TSPL_COMPANY_MASTER on TSPL_DEMAND_BOOKING_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code
where 2=2 "
                If rbtnMorning.IsChecked Then
                    qry +=" and TSPL_DEMAND_BOOKING_DETAIL.ShiftType = 'Morning' "
                    ElseIf rbtnEvening.IsChecked then
                     qry +=" and TSPL_DEMAND_BOOKING_DETAIL.ShiftType = 'Evening' "
                End If

                qry += " and (CONVERT(      date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= '" + clsCommon.GetPrintDate(txtDate.Value) + "') 
  and TSPL_DEMAND_BOOKING_MASTER.Route_No = '" + txtRouteNo.Value + "' and TSPL_ITEM_MASTER.Is_Ambient=1  and TSPL_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0 )XXFinal
  Group by XXFinal.Cust_Code,XXFinal.Sku_Seq ,XXFinal.Short_Description  "

            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
                If rbtn_Fresh.IsChecked Then
                    If ItemCount > 0 AndAlso ItemCount <= 9 Then
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingFUDP", "Demand Booking")
                    Else
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingFUDP9", "Demand Booking")
                    End If
                ElseIf rbtn_Ambient.IsChecked Then
                    If ItemCount > 0 AndAlso ItemCount <= 13 Then
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingAUDP", "Demand Booking")
                    Else
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingAUDP14", "Demand Booking")
                    End If
                End If
            Else
                If rbtn_Fresh.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBooking", "Demand Booking")
                ElseIf rbtn_Ambient.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBookingAUDP", "Demand Booking")
                End If

            End If
            'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptDemandBooking", "Demand Booking")
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
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String = ""
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            'End If
            'If Not filePath.Equals(String.Empty) Then
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.exportdata(gv1, "", Me.Text, , Nothing, False, False, True)
            End If
            ' End If
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
            If clsDemandBookingImport.importExcel(gvImport) Then
                LoadBlankGrid()
                RouteData(False, False)
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    If gv1.Columns(ii).IsVisible Then
                        arrVisbleColumns.Add(ii)
                    End If
                Next
                If gvImport.Rows.Count > 0 Then
                    If clsCommon.CompairString(gv1.Rows.Count - 1, gvImport.Rows.Count - 2) = CompairStringResult.Equal Then
                        Try
                            Dim arrCustCodeExist As New List(Of String)
                            For i As Integer = 0 To gv1.Rows.Count - 1
                                arrCustCodeExist.Add(gv1.Rows(i).Cells(1).Value)
                            Next
                            isInsideLoadData = True
                            clsCommon.ProgressBarPercentShow()
                            For ii As Integer = 1 To gvImport.Rows.Count - 2
                                clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / gvImport.RowCount, clsCommon.myCstr((ii + 1)) + "/" + clsCommon.myCstr(gvImport.RowCount))
                                For jj As Integer = 0 To gv1.Rows.Count - 1
                                    Dim code As String = clsCommon.myCstr(gvImport.Rows(ii).Cells(1).Value)
                                    If arrCustCodeExist.Contains(code) Then
                                        If clsCommon.CompairString(clsCommon.myCstr(gvImport.Rows(ii).Cells(2).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colCustCode).Value)) = CompairStringResult.Equal Then
                                            For kk As Integer = 5 To arrVisbleColumns.Count - 7
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
                        UpdateAllTotals(False)
                        clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Else
                        clsCommon.MyMessageBoxShow("You cannot import quantity because both Import and Export Data is different", Me.Text)
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
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnFullMode.Click
        Try
            If gvFullMode Then
                rgbDemandHead.Visible = True
                RadGroupBox2.Location = New System.Drawing.Point(6, 190)
                RadGroupBox2.Size = New System.Drawing.Size(1114, 400)
                gvFullMode = False
            Else
                gvFullMode = True
                rgbDemandHead.Visible = False
                RadGroupBox2.Location = New System.Drawing.Point(6, 6)
                RadGroupBox2.Size = New System.Drawing.Size(1114, 600)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnQuickDemand_Click(sender As Object, e As EventArgs) Handles btnQuickDemand.Click
        Try
            Dim qry As String = "select Route_No,max(demand_date) as DemandDate,max(shiftType) as ShiftType from TSPL_DEMAND_SHEET where convert(date,demand_date,103)='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "'  group by Route_No"  ''and Created_By='" + objCommonVar.CurrentUserCode + "'
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                clsCommon.ProgressBarShow()
                For Each dr As DataRow In dt.Rows
                    AddNew()
                    txtDocNo.Value = ""
                    txtDate.Value = clsCommon.GetPrintDate(dr.Item("DemandDate"))
                    If clsCommon.CompairString(clsCommon.myCstr(dr.Item("ShiftType")), "Morning") = CompairStringResult.Equal Then
                        rbtnMorning.IsChecked = True
                    Else
                        rbtnEvening.IsChecked = True
                    End If
                    QuickDemamd(clsCommon.GetPrintDate(dr.Item("DemandDate")), objCommonVar.CurrentUserCode, clsCommon.myCstr(dr.Item("ShiftType")), clsCommon.myCstr(dr.Item("Route_No")))
                Next
                AddNew()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Saved  ", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "Demand Sheet Found ", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub QuickDemamd(ByVal DocDate As Date, ByVal CurrUser As String, ByVal ShiftType As String, ByVal RouteNo As String)
        Try
            Dim qry As String = " select Document_No from TSPL_DEMAND_BOOKING_MASTER where convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "' and ShiftType='" + ShiftType + "' and Route_No='" + RouteNo + "'"
            Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(DocumentNo) > 0 Then
                qry = "select Posted from TSPL_DEMAND_BOOKING_MASTER where Document_No='" + DocumentNo + "' "
                Dim isPosted As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1, True, False)
                If Not isPosted Then
                    LoadData(DocumentNo, NavigatorType.Current)
                    FillQuickDemandData(DocDate, ShiftType)
                    SaveData(0, True)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Document Posted for Route No [ " + RouteNo + " ]", Me.Text)
                End If
            Else
                Dim isClicked As Boolean = False
                txtRouteNo.Value = RouteNo
                RouteData(isClicked, True)
                FillQuickDemandData(DocDate, ShiftType)
                SaveData(0, True)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub FillQuickDemandData(ByVal docDate As Date, ByVal ShiftType As String)
        Try
            Dim Qty As Integer = 0
            Dim qry As String = "select Cust_Code,Item_Code,Qty from TSPL_DEMAND_SHEET where convert(date,demand_date,103)='" + clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") + "' and ShiftType='" + ShiftType + "' and Route_No='" + clsCommon.myCstr(txtRouteNo.Value) + "' "  ' and Created_By='" + objCommonVar.CurrentUserCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For dblrow As Integer = 0 To gv1.Rows.Count - 2
                        If clsCommon.CompairString(gv1.Rows(dblrow).Cells(colCustCode).Value, clsCommon.myCstr(dr.Item("Cust_Code"))) = CompairStringResult.Equal Then
                            Dim k As Integer = 1
                            For dblcolumns As Integer = 9 To gv1.Columns.Count - 7
                                Dim obj1 As ItemValueClass = Nothing
                                Try
                                    obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                Catch ex As Exception
                                End Try
                                If obj1 IsNot Nothing Then
                                    If clsCommon.CompairString(clsCommon.myCstr(obj1.itemCode), clsCommon.myCstr(dr.Item("Item_Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString("Crate", clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal Then
                                        Qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Qty from TSPL_DEMAND_SHEET where convert(date,demand_date,103)='" + clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") + "' and ShiftType='" + ShiftType + "'  and Item_Code='" + clsCommon.myCstr(dr.Item("Item_Code")) + "' and Cust_Code='" + clsCommon.myCstr(dr.Item("Cust_Code")) + "'  order by Modify_Date desc"))
                                        If Qty > 0 Then
                                            gv1.Rows(dblrow).Cells(dblcolumns).Value = Qty
                                        Else
                                            gv1.Rows(dblrow).Cells(dblcolumns).Value = ""
                                        End If
                                    End If
                                End If
                                k = k + 1
                            Next
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gv1_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellClick
        If e.Column Is gv1.Columns(colbtncol) Then
            Try
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    If common.clsCommon.MyMessageBoxShow(Me, "Do You Want to Reset Demand for Booth  " + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        DeleteBoothDemand(txtDocNo.Value, gv1.CurrentRow.Cells(colCustCode).Value, IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
                    End If
                Else
                    Throw New Exception("Document not Found!")
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Public Sub DeleteBoothDemand(ByVal DocNo As String, ByVal cust_code As String, ByVal ShiftType As String)
        Try
            If clsDemandBookingSale.DeleteBoothDemand(DocNo, cust_code, ShiftType) Then
                clsCommon.MyMessageBoxShow(Me, "Demand Reset for Booth No :" + clsCommon.myCstr(cust_code), Me.Text)
                LoadData(DocNo, NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub ResetDemandOnSave(ByVal DocNo As String)
        Try
            For dblrows As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colItemExist).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value)) > 0 Then
                    Dim StrQry As String = "select count(TSPL_BOOKING_MATSER.Document_Date) from TSPL_BOOKING_MATSER left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No where TSPL_BOOKING_MATSER.Against_DemandBooking_No='" + DocNo + "' and TSPL_BOOKING_DETAIL.Cust_Code='" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value) + "'"
                    Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQry))
                    If count > 0 Then
                        Dim status As Boolean = clsDemandBookingSale.DeleteBoothDemand(DocNo, gv1.Rows(dblrows).Cells(colCustCode).Value, IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
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
