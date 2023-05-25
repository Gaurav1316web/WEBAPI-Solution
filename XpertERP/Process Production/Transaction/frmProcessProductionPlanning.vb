'------------------------BM00000003192-----28/07/2014-BM00000003794---BM00000004866
Imports common
Imports System.Data.SqlClient

Public Class FrmProcessProductionPlanning
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim DecimalPoint As Integer = 3
    Dim isSavedSuccess As Boolean = True
    Dim arrLoc As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As New clsErrorControl()

    Const colLineno As String = "SNO"
    Const colIcode As String = "Icode"
    Const colIname As String = "Iname"
    Const colItype As String = "ItemType"
    Const colUOM As String = "UOM"
    Const colDeliveryqty As String = "DeliveryQty"
    Const colyestsale As String = "YesterdaySale"
    Const colPlanqty As String = "PlanQty"
    Const colFinalqty As String = "FinalQty"
    Const colAvgQty As String = "AvgQty"
    Const colStockQty As String = "StockQty"
    Const colRem As String = "Remarks"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim isNewEntry As Boolean = Nothing
#End Region

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtlocationcode.Value = obj.Default_LocCode
                txtlocationname.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionPlanningDairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting

    End Sub

    Sub FunReset()
        fndItemCategory.Value = ""
        TxtCategory.Text = ""
        TxtSection.Text = ""
        fndSection.Value = ""
        txtDispatch_Days.Text = Nothing
        isSavedSuccess = True
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE()
        cboBOMStatus.Text = "Open"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtlocationcode.Value = ""
        txtlocationname.Text = ""
        LoadBlankGrid()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        txtdesc.Text = ""
        TxtCostCenterCode.Value = ""
        lblCostCenterName.Text = ""
        TxtProfitCenterCode.Value = ""
        lblProfitCenterName.Text = ""
        FndLineNo.Value = ""
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnCopy.Enabled = True
        btnCancel.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        txtlocationcode.Enabled = True
        fndItemCategory.Enabled = True
        fndSection.Enabled = True
        btnunpost.Visible = False

        LOCATIONRIGTHS()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtdesc.Focus()
        txtdesc.Select()
        isNewEntry = True
        isInsideLoadData = False
        gvSectionStock.DataSource = Nothing
        gvSectionStockHistory.DataSource = Nothing
    End Sub
    ' KUNAL > DATE :23-DEC-2016 > KDIL > TICKET : UNKNOWN > ALLOCATE BY : RANJANA MADAM > DISCUSSED BY : NISHA > TASKS > ADD FILTERS OVER GRIDS ON SECTION AND SECTION HISTORY TABS GRIDS
    Private Sub FrmProcessProductionPlanning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                btnPost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isReverse AndAlso btnunpost.Visible Then
                btnunpost.PerformClick()               
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "TSPL_PP_PRODUCTION_PLAN_HEAD" + Environment.NewLine + _
                                         "TSPL_PP_PRODUCTION_PLAN_DETAIL")
                If btnPost.Enabled = False AndAlso btnsave.Enabled = False Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnunpost.Visible = True
                    End If
                End If
            ElseIf e.KeyData = (Keys.Control + Keys.H) Then
                RadPageView1.SelectedPage = pageSectionStockHistory
            End If


            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colIcode) Then
                isCellValueChanged = True
                OpenIcode(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colUOM) Then
                isCellValueChanged = True
                OpenUOM(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F4 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colStockQty) Then
                isCellValueChanged = True
                Dim frm As New FrmStockReco
                frm.SetUserMgmt(clsUserMgtCode.MISStockReco)
                frm.isDataLoad = True
                frm.dtFrom = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select top 1 convert(date,Source_Doc_Date,103) from TSPL_INVENTORY_MOVEMENT order by trans_id"))
                frm.dtTo = clsCommon.myCDate(dtpDate.Text)
                frm.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where unit_desc='" + clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value) + "'"))
                frm.arrItem = New ArrayList
                frm.arrItem.Add(clsCommon.myCstr(gv.CurrentRow.Cells(colIcode).Value))
                frm.arrLoc = New Dictionary(Of String, Object)
                frm.arrLoc.Add(txtlocationcode.Value, Nothing)
                frm.strType = "Item Wise Summary"
                frm.WindowState = FormWindowState.Maximized
                frm.chkIncludeGIT.Checked = True
                frm.InOutType = "All"
                frm.SkipCheckFatAndSNF = True
                frm.Show()
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmProcessProductionPlanning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' TEMP TABLE CREATE
        
        SetUserMgmtNew()

        DecimalPoint = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPoint <= 0 Then
            DecimalPoint = 3
        End If

        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Alt+N for reset screen")
        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnPost, "Alt+P for post data")
        ButtonToolTip.SetToolTip(btnunpost, "Alt+R for reverse/unpost data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for close window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        gvSectionStock.AutoGenerateColumns = True
        gvSectionStockHistory.AutoGenerateColumns = True
        gvSectionStock.ReadOnly = True
        gvSectionStockHistory.ReadOnly = True
        gvSectionStock.AutoGenerateColumns = True

    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.FormatString = ""
        reposno.Name = colLineno
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        reposno.Width = 60
        gv.MasterTemplate.Columns.Add(reposno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colIcode
        repoicode.HeaderText = "Item Code"
        repoicode.Width = 130
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colIname
        repoiname.HeaderText = "Description"
        repoiname.ReadOnly = True
        repoiname.Width = 250
        gv.MasterTemplate.Columns.Add(repoiname)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colItype
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        repoitype.Width = 110
        gv.MasterTemplate.Columns.Add(repoitype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colUOM
        repouom.HeaderText = "UOM"
        repouom.Width = 70
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repouom)

        Dim repodlvryqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repodlvryqty.FormatString = ""
        repodlvryqty.Name = colDeliveryqty
        repodlvryqty.HeaderText = "Delivery Qty"
        repodlvryqty.Width = 70
        repodlvryqty.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repodlvryqty)

        Dim reposale As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposale.FormatString = ""
        reposale.Name = colyestsale
        reposale.HeaderText = "Yesterday Sale Qty"
        reposale.ReadOnly = True
        reposale.Width = 70
        reposale.DecimalPlaces = DecimalPoint
        reposale.IsVisible = False
        gv.MasterTemplate.Columns.Add(reposale)

        Dim reposaleAvg As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposaleAvg.FormatString = ""
        reposaleAvg.Name = colAvgQty
        reposaleAvg.HeaderText = "Average Sale Qty"
        reposaleAvg.ReadOnly = True
        reposaleAvg.Width = 70
        reposaleAvg.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(reposaleAvg)

        Dim repoplanqtyStk As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoplanqtyStk.FormatString = ""
        repoplanqtyStk.Name = colStockQty
        repoplanqtyStk.HeaderText = "Stock In Hand"
        repoplanqtyStk.Width = 70
        repoplanqtyStk.ReadOnly = True
        repoplanqtyStk.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoplanqtyStk)

        Dim repoplanqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoplanqty.FormatString = ""
        repoplanqty.Name = colPlanqty
        repoplanqty.HeaderText = "Planned Qty"
        repoplanqty.Width = 70
        repoplanqty.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoplanqty)

        Dim repofinalqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofinalqty.FormatString = ""
        repofinalqty.Name = colFinalqty
        repofinalqty.HeaderText = "Final Qty"
        repofinalqty.Width = 70
        repofinalqty.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repofinalqty)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colRem
        reporem.HeaderText = "Remarks"
        reporem.Width = 250
        reporem.MaxLength = 250
        gv.MasterTemplate.Columns.Add(reporem)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(TxtCategory, "Select production category detail")
                fndItemCategory.Focus()
                fndItemCategory.Select()
                Throw New Exception("Select production category detail")
            Else
                Errorcontrol.ResetError(TxtCategory)
            End If

            If clsCommon.myLen(fndSection.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(TxtSection, "Select section detail")
                fndSection.Focus()
                fndSection.Select()
                Throw New Exception("Select section detail")
            Else
                Errorcontrol.ResetError(TxtSection)
            End If

            If clsCommon.myLen(txtlocationcode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtlocationname, "Please select location detail")
                txtlocationcode.Focus()
                txtlocationcode.Select()
                Throw New Exception("Please select location detail")
            Else
                Errorcontrol.ResetError(txtlocationname)
            End If

            If clsCommon.myLen(cboBOMStatus.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(cboBOMStatus, "Select status for planning")
                cboBOMStatus.Select()
                Throw New Exception("Select status for planning")
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If

            Dim icode As String = ""
            Dim dlvry As Decimal = Nothing
            Dim planqty As Decimal = Nothing
            Dim finalqty As Decimal = Nothing
            Dim saleqty As Decimal = Nothing
            Dim oldicode As String = ""
            Dim uom As String = ""
            Dim arrIcode As List(Of String) = Nothing

            arrIcode = New List(Of String)
            For ii As Integer = 0 To gv.Rows.Count - 1
                gv.Focus()
                gv.Select()

                icode = clsCommon.myCstr(gv.Rows(ii).Cells(colIcode).Value)
                uom = clsCommon.myCstr(gv.Rows(ii).Cells(colUOM).Value)

                If Not arrIcode.Contains(icode) AndAlso clsCommon.myLen(icode) > 0 Then
                    arrIcode.Add(icode)
                End If

                dlvry = clsCommon.myCdbl(gv.Rows(ii).Cells(colDeliveryqty).Value)
                planqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colPlanqty).Value)
                finalqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colFinalqty).Value)

                If finalqty <= 0 AndAlso planqty > 0 Then
                    'gv.Rows(ii).Cells(colFinalqty).Value = planqty
                End If

                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(uom) <= 0 Then
                    Errorcontrol.SetError(gv, "Select UOM detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colUOM)
                    Throw New Exception("Select UOM detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                'If clsCommon.myLen(icode) > 0 AndAlso dlvry <= 0 Then
                '    Errorcontrol.SetError(gv, "Fill delivery quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                '    gv.Focus()
                '    gv.Select()
                '    Throw New Exception("Fill delivery quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                'Else
                '    Errorcontrol.ResetError(gv)
                'End If

                If clsCommon.myLen(icode) > 0 AndAlso planqty <= 0 Then
                    Errorcontrol.SetError(gv, "Fill planned quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colPlanqty)
                    Throw New Exception("Fill planned quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                For jj As Integer = ii + 1 To gv.Rows.Count - 1
                    oldicode = clsCommon.myCstr(gv.Rows(jj).Cells(colIcode).Value)

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal Then
                        Errorcontrol.SetError(gv, "Duplicate item not allowed at row no. " + clsCommon.myCstr(jj + 1) + "")
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(jj)
                        gv.CurrentColumn = gv.Columns(colIcode)
                        Throw New Exception("Duplicate item not allowed at row no. " + clsCommon.myCstr(jj + 1) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If
                Next
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                Errorcontrol.SetError(gv, "Fill atleast one row in grid")
                RadPageView1.SelectedPage = RadPageViewPage1
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(0)
                gv.CurrentColumn = gv.Columns(colIcode)
                Throw New Exception("Fill atleast one row in grid")
            Else
                Errorcontrol.ResetError(gv)
            End If

            '===================check last document is posted ,if not then not allow to save----------------
            Dim qry As String = "select Plan_Code,plan_date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code in (select max(plan_code) from TSPL_PP_PRODUCTION_PLAN_HEAD where Is_Post='0' and plan_code <> '" + txtCode.Value + "' and section_code = '" + fndSection.Value + "' and structure_code = '" + fndItemCategory.Value + "' and location_code='" + txtlocationcode.Value + "') and plan_date<'" + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy") + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso clsCommon.myLen(dt.Rows(0)("Plan_Code")) > 0 Then
                Throw New Exception("Yesterday production final quantity is not entered, please post yesterday planning sheet to make new." + Environment.NewLine + "[Plan Code: " + clsCommon.myCstr(dt.Rows(0)("Plan_Code")) + " Dated: " + clsCommon.GetPrintDate(clsCommon.myCstr(dt.Rows(0)("Plan_Date")), "dd/MM/yyyy") + "]")
            End If
            '===============================================================================================

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsProcessProductionPlanning()
        Dim objtr As New clsProcessProductionPlanningDetail()
        Try
            If AllowToSave() Then
                obj.plancode = clsCommon.myCstr(txtCode.Value)
                obj.plandate = clsCommon.myCDate(dtpDate.Text)
                obj.plandesc = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
                obj.status = clsCommon.myCstr(cboBOMStatus.Text)
                obj.Dispatch_Days = CInt(clsCommon.myCdbl(txtDispatch_Days.Text))
                obj.Section_Code = clsCommon.myCstr(fndSection.Value)
                obj.Structure_Code = clsCommon.myCstr(fndItemCategory.Value)
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                obj.LINE_NO = clsCommon.myCstr(FndLineNo.Value)
                obj.CostCenterCode = clsCommon.myCstr(TxtCostCenterCode.Value)
                obj.ProfitCenterCode = clsCommon.myCstr(TxtProfitCenterCode.Value)
                ''----------------
                obj.Is_Post = "0"
                If clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
                    'obj.Is_Post = "1"
                End If
                obj.locationcode = clsCommon.myCstr(txtlocationcode.Value)

                obj.Arr = New List(Of clsProcessProductionPlanningDetail)

                For Each grow As GridViewRowInfo In gv.Rows
                    objtr = New clsProcessProductionPlanningDetail()

                    objtr.sno = CInt(grow.Cells(colLineno).Value)
                    objtr.icode = clsCommon.myCstr(grow.Cells(colIcode).Value)
                    objtr.uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objtr.dlvryqty = clsCommon.myCdbl(grow.Cells(colDeliveryqty).Value)
                    objtr.saleqty = clsCommon.myCdbl(grow.Cells(colyestsale).Value)
                    objtr.planqty = clsCommon.myCdbl(grow.Cells(colPlanqty).Value)
                    objtr.finalqty = clsCommon.myCdbl(grow.Cells(colFinalqty).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colRem).Value).Replace("'", "`")
                    objtr.Avg_Sale_Qty = clsCommon.myCdbl(grow.Cells(colAvgQty).Value)

                    If clsCommon.myLen(objtr.icode) > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next


                If clsProcessProductionPlanning.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    End If
                    txtCode.Value = obj.plancode

                    UcAttachment1.SaveData(txtCode.Value)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If

            isSavedSuccess = True
        Catch ex As Exception
            isSavedSuccess = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Errorcontrol.SetError(txtCode, "Please select plan code for deletion")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Please select plan code for deletion")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsProcessProductionPlanning.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully", Me.Text)
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Errorcontrol.SetError(txtCode, "Please select plan code for posting")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Please select plan code for posting")
            Else
                Errorcontrol.ResetError(txtCode)
            End If


            If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
                Errorcontrol.SetError(lblConsmSectionLocCode, "Create Consumption Location for " & txtlocationcode.Value & " ")
                RadPageView1.SelectedPage = RadPageViewPage1

                Throw New Exception("Create Consumption Location for " & txtlocationcode.Value & " ")
            Else
                Errorcontrol.ResetError(lblConsmSectionLocCode)
            End If


            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(colIcode).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colFinalqty).Value) <= 0 Then
                    Errorcontrol.SetError(gv, "Fill final quantity at row no. " + clsCommon.myCstr(grow.Index) + "")
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv.CurrentRow = gv.Rows(grow.Index)
                    gv.CurrentColumn = gv.Columns(colFinalqty)
                    Throw New Exception("Fill final quantity at row no. " + clsCommon.myCstr(grow.Index) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If
            Next

            If clsCommon.CompairString(cboBOMStatus.Text, "Approved") <> CompairStringResult.Equal Then
                Errorcontrol.SetError(cboBOMStatus, "Set the status Approved,before posting")
                RadPageView1.SelectedPage = RadPageViewPage1
                cboBOMStatus.Select()
                Throw New Exception("Set the status Approved,before posting")
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If

            If Not myMessages.postConfirm() Then
                Exit Sub
            End If

            If AllowToSave() Then
                SaveData(True)

                If clsProcessProductionPlanning.PostData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow("Data posted successfully", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If

            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub OpenIcode(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select production category first.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            fndItemCategory.Select()
            fndItemCategory.Focus()
            Errorcontrol.SetError(TxtCategory, "Select production category first.")
            Exit Sub
        Else
            Errorcontrol.ResetError(TxtCategory)
        End If

        If clsCommon.myLen(fndSection.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select section first.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            fndSection.Select()
            fndSection.Focus()
            Errorcontrol.SetError(TxtSection, "Select section first.")
            Exit Sub
        Else
            Errorcontrol.ResetError(TxtSection)
        End If

        If clsCommon.myLen(txtlocationcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select location first.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtlocationcode.Select()
            txtlocationcode.Focus()
            Errorcontrol.SetError(txtlocationname, "Select location first.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtlocationname)
        End If

        Dim icode As String = ""
        icode = clsItemMaster.getFinder(" tspl_item_master.Structure_Code='" + fndItemCategory.Value + "' and tspl_item_master.item_type in ('F','S') and tspl_item_master.Active='1' ", clsCommon.myCstr(gv.CurrentRow.Cells(colIcode).Value), isButtonClicked)

        If clsCommon.myLen(icode) > 0 Then
            gv.CurrentRow.Cells(colIcode).Value = icode
            gv.CurrentRow.Cells(colIname).Value = clsItemMaster.GetItemName(icode, Nothing)
            gv.CurrentRow.Cells(colItype).Value = clsItemMaster.GetItemType(icode, Nothing)
            gv.CurrentRow.Cells(colItype).Value = ItemType(clsCommon.myCstr(gv.CurrentRow.Cells(colItype).Value))
            gv.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetItemDefaultUnit(icode, Nothing)
            gv.CurrentRow.Cells(colyestsale).Value = clsCommon.myCdbl(YesterdaySaleQty(icode, clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), CInt(clsCommon.myCdbl(txtDispatch_Days.Text))))
            If CInt(clsCommon.myCdbl(txtDispatch_Days.Text)) > 0 Then
                gv.CurrentRow.Cells(colAvgQty).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colyestsale).Value) / CInt(clsCommon.myCdbl(txtDispatch_Days.Text)), DecimalPoint)
            Else
                gv.CurrentRow.Cells(colAvgQty).Value = 0
            End If
            gv.CurrentRow.Cells(colStockQty).Value = Math.Round(clsProcessProductionPlanning.GetMilkAndALLItemStockBalance(icode, txtlocationcode.Value, txtlocationcode.Value, txtCode.Value, clsCommon.GETSERVERDATE(Nothing), Nothing, clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value)), DecimalPoint)
        Else
            gv.CurrentRow.Cells(colIcode).Value = ""
            gv.CurrentRow.Cells(colIname).Value = ""
            gv.CurrentRow.Cells(colItype).Value = ""
            gv.CurrentRow.Cells(colUOM).Value = ""
            gv.CurrentRow.Cells(colDeliveryqty).Value = Nothing
            gv.CurrentRow.Cells(colyestsale).Value = Nothing
            gv.CurrentRow.Cells(colAvgQty).Value = Nothing
            gv.CurrentRow.Cells(colPlanqty).Value = Nothing
            gv.CurrentRow.Cells(colStockQty).Value = Nothing
            gv.CurrentRow.Cells(colFinalqty).Value = Nothing
        End If
    End Sub

    Private Function YesterdaySaleQty(ByVal ItemCode As String, ByVal Unit_Code As String, ByVal Days As Integer) As Decimal
        Try
            Dim str As Decimal = 0
            Days = 0 - Days

            Dim qry As String = "select sum(aa.qty) as qty from (select (case when isnull(finl.Conversion_Factor,0)>0 then (a.qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finl.Conversion_Factor else 0 end) as qty from (" & _
                " select SUM(isnull(qty,0)) as qty,unit_code from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where convert(date,Document_Date,103) >= convert(date,DATEADD(DAY," + clsCommon.myCstr(Days) + ",'" + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy") + "'),103) and isnull(status,0)=1 and Bill_To_Location='" + clsCommon.myCstr(txtlocationcode.Value) + "') and Item_Code='" + ItemCode + "' group by unit_code " & _
            " union all select SUM(isnull(invoiceqty,0)) as qty,unit_code from TSPL_INVOICE_DETAIL_BULKSALE where DOCUMENT_no in (select Document_no from TSPL_INVOICE_MASTER_BULKSALE where convert(date,Document_Date,103) >= convert(date,DATEADD(DAY," + clsCommon.myCstr(Days) + ",'" + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy") + "'),103) and isnull(posted,0)=1 and location_code='" + clsCommon.myCstr(txtlocationcode.Value) + "') and Item_Code='" + ItemCode + "' group by unit_code " & _
            " )a left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code='" + ItemCode + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code=a.Unit_code left outer join TSPL_ITEM_UOM_DETAIL finl on finl.Item_Code='" + ItemCode + "' and finl.UOM_Code='" + Unit_Code + "')aa "
            str = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            Return Math.Round(str, DecimalPoint)
        Catch ex As Exception
        End Try
    End Function

    Private Function ItemType(ByVal itype As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(itype, "R") = CompairStringResult.Equal Then
            values = "Raw Material"
        ElseIf clsCommon.CompairString(itype, "F") = CompairStringResult.Equal Then
            values = "Finished Good"
        ElseIf clsCommon.CompairString(itype, "S") = CompairStringResult.Equal Then
            values = "Semi Finished Good"
        ElseIf clsCommon.CompairString(itype, "A") = CompairStringResult.Equal Then
            values = "Asset"
        ElseIf clsCommon.CompairString(itype, "H") = CompairStringResult.Equal Then
            values = "Fresh"
        ElseIf clsCommon.CompairString(itype, "O") = CompairStringResult.Equal Then
            values = "Other"
        End If

        Return values
    End Function

    Sub OpenUOM(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim UOM As String = ""
        icode = clsCommon.myCstr(gv.CurrentRow.Cells(colIcode).Value)
        If clsCommon.myLen(icode) <= 0 Then
            clsCommon.MyMessageBoxShow("Select item first", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            gv.CurrentRow = gv.Rows(gv.CurrentRow.Index)
            gv.CurrentColumn = gv.Columns(colIcode)
            Errorcontrol.SetError(gv, "Select item first")
            Return
        Else
            Errorcontrol.ResetError(gv)
        End If

        Dim qry As String = "select UOM_Code as Code,UOM_Description as Description,Conversion_Factor as [Conversion Factor],Stocking_Unit as [Stocking Unit],Weight from TSPL_ITEM_UOM_DETAIL "
        UOM = clsCommon.myCstr(clsCommon.ShowSelectForm("PPUOMFND", qry, "Code", " item_code='" + icode + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), "Code", isButtonClicked))

        If clsCommon.myLen(UOM) > 0 Then
            gv.CurrentRow.Cells(colUOM).Value = UOM
            gv.CurrentRow.Cells(colyestsale).Value = clsCommon.myCdbl(YesterdaySaleQty(clsCommon.myCstr(gv.CurrentRow.Cells(colIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), CInt(clsCommon.myCdbl(txtDispatch_Days.Text))))
            If CInt(clsCommon.myCdbl(txtDispatch_Days.Text)) > 0 Then
                gv.CurrentRow.Cells(colAvgQty).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colyestsale).Value) / CInt(clsCommon.myCdbl(txtDispatch_Days.Text)), DecimalPoint)
            Else
                gv.CurrentRow.Cells(colAvgQty).Value = 0
            End If

            gv.CurrentRow.Cells(colStockQty).Value = Math.Round(clsProcessProductionPlanning.GetMilkAndALLItemStockBalance(icode, txtlocationcode.Value, txtlocationcode.Value, txtCode.Value, clsCommon.GETSERVERDATE(Nothing), Nothing, clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value)), 2)
        Else
            gv.CurrentRow.Cells(colUOM).Value = ""
            gv.CurrentRow.Cells(colStockQty).Value = 0
        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv.Columns(colIcode) Then
                        isCellValueChanged = True
                        OpenIcode(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colUOM) Then
                        isCellValueChanged = True
                        OpenUOM(False)
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtlocationcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocationcode._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow("No record found.")
            txtlocationcode.Value = ""
            txtlocationname.Text = ""
            Exit Sub
        End If
        txtlocationcode.Value = clsCommon.myCstr(clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y'", txtlocationcode.Value, isButtonClicked))

        txtlocationname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txtlocationcode.Value + "'"))
        For Each grow As GridViewRowInfo In gv.Rows
            grow.Cells(colyestsale).Value = clsCommon.myCdbl(YesterdaySaleQty(clsCommon.myCstr(grow.Cells(colIcode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), CInt(clsCommon.myCdbl(txtDispatch_Days.Text))))
            If CInt(clsCommon.myCdbl(txtDispatch_Days.Text)) > 0 Then
                grow.Cells(colAvgQty).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colyestsale).Value) / CInt(clsCommon.myCdbl(txtDispatch_Days.Text)), DecimalPoint)
            Else
                grow.Cells(colAvgQty).Value = 0
            End If

            grow.Cells(colStockQty).Value = Math.Round(clsProcessProductionPlanning.GetMilkAndALLItemStockBalance(clsCommon.myCstr(grow.Cells(colIcode).Value), txtlocationcode.Value, txtlocationcode.Value, txtCode.Value, clsCommon.GETSERVERDATE(Nothing), Nothing, clsCommon.myCstr(grow.Cells(colUOM).Value)), 2)
        Next
        lblConsmSectionLocCode.Text = clsProductionEntry.GetSectionConsumptionSection(txtlocationcode.Value, fndSection.Value, Nothing)
        FillSection()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsProcessProductionPlanning()
        Try
            If clsCommon.myLen(arrLoc) <= 0 Then
                clsCommon.MyMessageBoxShow("User have no location rights.")
                Exit Sub
            End If

            FunReset()

            obj = clsProcessProductionPlanning.GetData(strCode, arrLoc, NavType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.plancode) > 0 Then
                isInsideLoadData = True
                isNewEntry = False

                txtCode.Value = obj.plancode
                dtpDate.Text = obj.plandate
                cboBOMStatus.Text = obj.status
                txtdesc.Text = obj.plandesc
                txtlocationcode.Value = obj.locationcode
                txtlocationname.Text = obj.locationname
                txtDispatch_Days.Text = obj.Dispatch_Days
                fndItemCategory.Value = obj.Structure_Code
                TxtCategory.Text = obj.Structure_Name
                fndSection.Value = obj.Section_Code
                TxtSection.Text = obj.Section_Name
                lblConsmSectionLocCode.Text = clsProductionEntry.GetSectionConsumptionSection(txtlocationcode.Value, fndSection.Value, Nothing)
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                FndLineNo.Value = obj.LINE_NO
                TxtCostCenterCode.Value = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                TxtProfitCenterCode.Value = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                ''----------------
                isNewEntry = False

                gv.Rows.Clear()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsProcessProductionPlanningDetail In obj.Arr
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = objtr.sno
                        gv.Rows(gv.Rows.Count - 1).Cells(colIcode).Value = objtr.icode
                        gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = objtr.iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colItype).Value = ItemType(objtr.itype)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colDeliveryqty).Value = objtr.dlvryqty
                        gv.Rows(gv.Rows.Count - 1).Cells(colyestsale).Value = objtr.saleqty
                        gv.Rows(gv.Rows.Count - 1).Cells(colPlanqty).Value = objtr.planqty
                        gv.Rows(gv.Rows.Count - 1).Cells(colFinalqty).Value = objtr.finalqty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRem).Value = objtr.Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colAvgQty).Value = objtr.Avg_Sale_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colStockQty).Value = Math.Round(clsProcessProductionPlanning.GetMilkAndALLItemStockBalance(objtr.icode, txtlocationcode.Value, txtlocationcode.Value, obj.plancode, clsCommon.GETSERVERDATE(Nothing), Nothing, objtr.uom), DecimalPoint)
                    Next
                End If

                If clsCommon.CompairString(obj.status, "Open") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.status, "On Hold") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.status, "Approved") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                ElseIf clsCommon.CompairString(obj.status, "In-Active") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Cancel
                End If

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnCopy.Enabled = False

                txtCode.MyReadOnly = True
                txtlocationcode.Enabled = False
                fndItemCategory.Enabled = False
                fndSection.Enabled = False

                UcAttachment1.LoadData(txtCode.Value)

                If obj.Is_Post = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If
            Else
                FunReset()
            End If
            FillSection()
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            If clsCommon.myLen(arrLoc) <= 0 Then
                clsCommon.MyMessageBoxShow("User have no location rights.")
                Exit Sub
            End If

            txtCode.Value = clsProcessProductionPlanning.GetFinder(" TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub cboBOMStatus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBOMStatus.TextChanged
        If clsCommon.CompairString(cboBOMStatus.Text, "Open") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "On Hold") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "In-Active") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Cancel
        End If
        If btnsave.Enabled = False AndAlso btnPost.Enabled = False AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal OrElse btnsave.Enabled OrElse btnPost.Enabled Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = " delete from TSPL_PP_PRODUCTION_PLAN_DETAIL where plan_code='" + clsCommon.myCstr(txtCode.Value) + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colIcode).Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Else
            e.Cancel = True
            clsCommon.MyMessageBoxShow("No row deleted,data is posted", Me.Text)
        End If
    End Sub

    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_PP_PRODUCTION_PLAN_HEAD where Is_Post='0' and plan_code='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProdcutionDoNotCheckForwardDocuments, clsFixedParameterCode.ProdcutionDoNotCheckForwardDocuments, Nothing)) <= 0 Then
                qry = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where plan_code='" + txtCode.Value + "'"
                check = clsDBFuncationality.getSingleValue(qry)
                If check > 0 Then
                    Throw New Exception("Cannot unpost document,is used in Batch Order.")
                End If
            End If
            If common.clsCommon.MyMessageBoxShow("Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Unpost"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsProcessProductionPlanning.UnpostData(txtCode.Value) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Amended and Recreated", Me.Text)
                        btnunpost.Visible = False
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndItemCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndItemCategory._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER"
            fndItemCategory.Value = clsCommon.ShowSelectForm("PPPlanSTRFND", qry, "Code", " Structure_Code in (select distinct Structure_Code from item_master)", fndItemCategory.Value, "Code", isButtonClicked)
            TxtCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + fndItemCategory.Value + "'"))

            gv.Rows.Clear()
            gv.Rows.AddNew()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndSection__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSection._MYValidating
        Try
            Dim qry As String = "select Section_Code as Code,Description as Name from TSPL_SECTION_MASTER"
            fndSection.Value = clsCommon.ShowSelectForm("PPlanSECFND", qry, "Code", " ", fndSection.Value, "", isButtonClicked)
            TxtSection.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SECTION_MASTER where Section_Code='" + fndSection.Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            If clsCommon.myLen(arrLoc) <= 0 Then
                clsCommon.MyMessageBoxShow("User have no location rights.")
                Exit Sub
            End If
            Dim whrCls As String = " TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in (" + arrLoc + ") "
            If clsCommon.myLen(fndItemCategory.Value) > 0 Then
                whrCls += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code='" + fndItemCategory.Value + "' "
            End If
            If clsCommon.myLen(fndSection.Value) > 0 Then
                whrCls += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Section_Code='" + fndSection.Value + "' "
            End If
            If clsCommon.myLen(txtlocationcode.Value) > 0 Then
                whrCls += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Location_Code='" + txtlocationcode.Value + "' "
            End If

            txtCode.Value = clsProcessProductionPlanning.GetFinder(whrCls, txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)

                btnsave.Text = "Save"
                btnsave.Enabled = True
                btndelete.Enabled = False
                btnPost.Enabled = False
                btnCopy.Enabled = True

                txtCode.MyReadOnly = False
                txtCode.Value = ""
                txtlocationcode.Enabled = True
                fndItemCategory.Enabled = True
                fndSection.Enabled = True

                cboBOMStatus.SelectedValue = "Open"
                UsLock1.Status = ERPTransactionStatus.Open

                isNewEntry = True
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDispatch_Days_TextChanged(sender As Object, e As EventArgs) Handles txtDispatch_Days.TextChanged
        If clsCommon.myCdbl(txtDispatch_Days.Text) > 0 Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colyestsale).Value = clsCommon.myCdbl(YesterdaySaleQty(clsCommon.myCstr(grow.Cells(colIcode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), CInt(clsCommon.myCdbl(txtDispatch_Days.Text))))
                If CInt(clsCommon.myCdbl(txtDispatch_Days.Text)) > 0 Then
                    grow.Cells(colAvgQty).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colyestsale).Value) / CInt(clsCommon.myCdbl(txtDispatch_Days.Text)), DecimalPoint)
                Else
                    grow.Cells(colAvgQty).Value = 0
                End If
            Next
        End If
    End Sub
    Sub FillSection()
        gvSectionStock.DataSource = clsProcessProductionPlanning.GetSectionStock(lblConsmSectionLocCode.Text)
        gvSectionStock.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub FillSectionHistory(ByVal Item_Code As String)
        gvSectionStockHistory.DataSource = clsProcessProductionPlanning.GetSectionStockHistory(lblConsmSectionLocCode.Text, Item_Code)
        gvSectionStockHistory.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        RadPageView1.SelectedPage = pageSectionStockHistory
    End Sub

    Private Sub gvSectionStock_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSectionStock.DoubleClick
        If gvSectionStock.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        FillSectionHistory(gvSectionStock.SelectedRows(0).Cells(0).Value)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub
    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            clsProcessProductionPlanning.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    ''richa agarwal againt ticket no BHA/02/07/18-000120
    Private Sub FndLineNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLineNo._MYValidating
        Try
            Dim qry As String = "Select LINE_NO AS Code,MACHINE_NAME,MACHINE_RATED,CAPACITY ,TIME_FRAME  from TSPL_LINE_MASTER"
            FndLineNo.Value = clsCommon.ShowSelectForm("PPLineFND", qry, "Code", " ", FndLineNo.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub TxtCostCenterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCostCenterCode._MYValidating
        Try
            Dim qry As String = "select TSPL_PROFIT_CENTER_MAPPING.CostCenter_Code as Code, TSPL_CostCenter_MASTER.Cost_name as [Name], TSPL_PROFIT_CENTER_MAPPING.ProfitCenter_Code as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name]  from TSPL_PROFIT_CENTER_MAPPING" & _
            " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PROFIT_CENTER_MAPPING.ProfitCenter_Code  " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PROFIT_CENTER_MAPPING.CostCenter_Code"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("PPCostCenterFND", qry)
            If dr IsNot Nothing Then
                TxtCostCenterCode.Value = clsCommon.myCstr(dr("Code"))
                lblCostCenterName.Text = clsCommon.myCstr(dr("Name"))
                TxtProfitCenterCode.Value = clsCommon.myCstr(dr("Profit Center Code"))
                lblProfitCenterName.Text = clsCommon.myCstr(dr("Profit Center Name"))
            Else
                TxtCostCenterCode.Value = ""
                lblCostCenterName.Text = ""
                TxtProfitCenterCode.Value = ""
                lblProfitCenterName.Text = ""
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Planning Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "Plan_Code", "TSPL_PP_PRODUCTION_PLAN_HEAD", "TSPL_PP_PRODUCTION_PLAN_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
