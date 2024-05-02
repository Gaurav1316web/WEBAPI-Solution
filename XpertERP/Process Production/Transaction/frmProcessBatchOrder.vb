'Monika----------------BM00000003196-----------BM00000003794----29/07/2014----BM00000003898-----------BM00000004805-BM00000004866
Imports common
Imports System.Data.SqlClient


Public Class FrmProcessBatchOrder
    Inherits FrmMainTranScreen

#Region "Variables"
    Public activateSFGProduction As Boolean = False
    Dim isSavedSuccess As Boolean = True
    Dim isNewEntry As Boolean = Nothing
    Dim SFGCodes As String = ""
    Dim chkclosestatus As Boolean = False
    Dim AllBomCode As String = ""
    Dim Errorcontrol As New clsErrorControl()
    Dim dt As DataTable = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False

    Const collineno As String = "Sno"
    Const colPlanCode As String = "plancode"
    Const colBOM As String = "BOMCOde"
    Const colBOMDesc As String = "BOMCOdeDesc"
    Const colBOMRevisionNo As String = "colBOMRevisionNo"
    Const colitemcode As String = "ItemCode"
    Const colIname As String = "ItemName"
    Const colItype As String = "ItemType"
    Const colUnit As String = "UOM"
    Const colQty As String = "Qty"
    Const colBOMFatPers As String = "BOMFatPers"
    Const colBOMFatkg As String = "BOMFatkg"
    Const colBOMSNFPers As String = "BOMSnfpers"
    Const colBOMSNFkg As String = "BOMsnfkg"
    Const colShiftcode As String = "ShiftCode"
    Const colShiftDesc As String = "ShiftDesc"
    Const colSection As String = "SectionCode"
    Const colSectionName As String = "SectionName"

    '=============================================
    Const colRawsno As String = "Sno"
    Const colrawbomcode As String = "BOMCODE"
    Const colrawprodtitem As String = "ProdItem"
    Const colRawIcode As String = "Icode"
    Const colRawIname As String = "ItemDesc"
    Const colRawItype As String = "IType"
    Const colRawUnit As String = "RawUOM"
    Const colRawProductType As String = "ProductType"
    Const colRawQty As String = "RawQty"
    Const colFat As String = "FAT"
    Const colSNF As String = "SNF"
    Const colFAT_KG As String = "FAT_KG"
    Const colSNF_KG As String = "SNF_KG"
    Const colrem As String = "Remarks"
    Const colBatchstatus As String = "BatchStatus"
    Const colSubBatchcode As String = "SubBatchCode"

    Dim arrLoc As String = Nothing
    Dim DecimalPoint As Integer = 3
    Dim ManualBOMSelect As Integer = 0
    Dim ManualBatchNoMandatoryOnBatchOrderScreen As Boolean = False
    Dim UseProductionPlaningDateForWholeProductionCycle As Boolean = False
#End Region
    ''ERO/15/03/19-000514 by balwinder on 20/06/2019
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtlocationcode.Value = obj.Default_LocCode
                txtlocationname.Text = obj.Default_LocName
                chkJobWorkInward.Checked = clsLocation.IsJobWorkLocation(txtlocationcode.Value, Nothing)
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderDairy)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting

    End Sub

    Sub FunReset()
        txtSectionCode.Text = ""
        txtSectionName.Text = ""
        AllBomCode = ""
        btnExport.Visible = False
        txtManualBatchNo.Text = ""
        RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
        SFGCodes = ""
        isSavedSuccess = True
        chkclosestatus = False
        isInsideLoadData = True
        chkclose.Checked = False
        isInsideLoadData = False
        txtplan_desc.Text = ""
        txtdesc.Text = ""
        txtitemcategory_name.Text = ""
        txticategory_code.Value = ""
        txtCode.Value = ""
        chkclose.Enabled = True
        txtmain_batch.Text = ""
        txtsub_batch.Text = ""
        dtpDate.Text = clsCommon.GETSERVERDATE()
        cboBOMStatus.Text = "Open"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtlocationcode.Value = ""
        chkJobWorkInward.Checked = False
        txtlocationname.Text = ""
        txtplancode.Value = ""
        LoadBlankGrid()
        LoadBlankRawGrid()
        FndLineNo.Value = ""
        TxtCostCenterCode.Value = ""
        lblCostCenterName.Text = ""
        TxtProfitCenterCode.Value = ""
        lblProfitCenterName.Text = ""
        TxtCostCenterCode.Enabled = True
        FndLineNo.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        txticategory_code.Enabled = True
        'txtplancode.Enabled = True
        txtlocationcode.Enabled = True
        btnCancel.Enabled = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        LOCATIONRIGTHS()

        RadPageView1.SelectedPage = RadPageViewPage1
        txtdesc.Focus()
        txtdesc.Select()
        isNewEntry = True
    End Sub

    Private Sub FrmProcessBatchOrder_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BO_TEMP'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            clsDBFuncationality.ExecuteNonQuery("drop table BO_TEMP")
        End If
    End Sub

    Private Sub FrmProcessBatchOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
                btnpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isReverse AndAlso btnunpost.Visible Then
                btnunpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                        "TSPL_PP_BATCH_ORDER_HEAD" + Environment.NewLine + _
                                        "TSPL_PP_BATCH_ORDER_BOM_DETAIL" + Environment.NewLine + _
                                        "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL")
                If btnunpost.Visible Then
                    btnunpost.Visible = False
                Else
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnunpost.Visible = True
                    End If
                End If
                ''commented because not used currently
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colBOM) Then
                isCellValueChanged = True
                OpenBOMCode(True)
                FillRawItemGridFromBOM()
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colitemcode) Then
                isCellValueChanged = True
                OpenBOMICode(True)
                FillRawItemGridFromBOM()
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colUnit) Then
                isCellValueChanged = True
                OpenUOM(True)
                FillRawItemGridFromBOM()
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colShiftcode) Then
                isCellValueChanged = True
                OpenShiftCode(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colQty) Then
                isCellValueChanged = True
                If clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value) > 0 Then
                    gv.CurrentRow.Cells(colBOMFatkg).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value) / 100), DecimalPoint)
                End If
                If clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value) > 0 Then
                    gv.CurrentRow.Cells(colBOMSNFkg).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value) / 100), DecimalPoint)
                End If
                isCellValueChanged = False
            End If
            '>>>>>>

            If e.KeyCode = Keys.F2 AndAlso gvraw.CurrentColumn IsNot Nothing AndAlso gvraw.CurrentColumn Is gvraw.Columns(colRawIcode) Then
                isCellValueChanged = True
                OpenRawIcode(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvraw.CurrentColumn IsNot Nothing AndAlso ((gvraw.CurrentColumn Is gvraw.Columns(colRawQty)) Or (gvraw.CurrentColumn Is gvraw.Columns(colFat)) Or (gvraw.CurrentColumn Is gvraw.Columns(colFAT_KG))) Then
                isCellValueChanged = True
                Cal_FAT()
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvraw.CurrentColumn IsNot Nothing AndAlso ((gvraw.CurrentColumn Is gvraw.Columns(colRawQty)) Or (gvraw.CurrentColumn Is gvraw.Columns(colSNF)) Or (gvraw.CurrentColumn Is gvraw.Columns(colSNF_KG))) Then
                isCellValueChanged = True
                Cal_SNF()
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmProcessBatchOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' done by Panch Raj on 09-07-2019 against ticket no:ERO/12/06/18-000342
        activateSFGProduction = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, Nothing), "1") = CompairStringResult.Equal, True, False)

        DecimalPoint = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPoint <= 0 Then
            DecimalPoint = 3
        End If
        ManualBOMSelect = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ManualySelectBOMForChildBatch, clsFixedParameterCode.ManualySelectBOMForChildBatch, Nothing)))
        ManualBatchNoMandatoryOnBatchOrderScreen = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ManualBatchNoMandatoryOnBatchOrderScreen, clsFixedParameterCode.ManualBatchNoMandatoryOnBatchOrderScreen, Nothing)) = 1, True, False)
        UseProductionPlaningDateForWholeProductionCycle = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseProductionPlaningDateForWholeProductionCycle, clsFixedParameterCode.UseProductionPlaningDateForWholeProductionCycle, Nothing)) = 1, True, False)
        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Alt+N for reset screen")
        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnpost, "Alt+P for approved/post data")
        ButtonToolTip.SetToolTip(btnpost, "Alt+R for reverse/unpost data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for close window")

        btnpdf.Visibility = ElementVisibility.Collapsed

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = collineno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim bomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colPlanCode
        bomcode.Width = 110
        bomcode.HeaderText = "Plan Code"
        bomcode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        bomcode = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colBOM
        bomcode.Width = 110
        bomcode.HeaderText = "BOM Code"
        bomcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        bomcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        Dim bomcode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode1.FormatString = ""
        bomcode1.Name = colBOMDesc
        bomcode1.Width = 160
        bomcode1.HeaderText = "BOM Description"
        bomcode1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(bomcode1)


        bomcode1 = New GridViewTextBoxColumn()
        bomcode1.FormatString = ""
        bomcode1.Name = colBOMRevisionNo
        bomcode1.Width = 100
        bomcode1.HeaderText = "BOM Revision No"
        bomcode1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(bomcode1)
        bomcode1 = Nothing

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colitemcode
        repoicode.Width = 110
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoicode)
        repoicode = Nothing

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colIname
        repoiname.Width = 200
        repoiname.HeaderText = "Description"
        repoiname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoiname)
        repoiname = Nothing

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colItype
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoitype)
        repoitype = Nothing

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colUnit
        repouom.Width = 80
        repouom.HeaderText = "UOM"
        repouom.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repouom)
        repouom = Nothing

        Dim repoqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.Name = colQty
        repoqty.Width = 80
        repoqty.DecimalPlaces = DecimalPoint
        repoqty.HeaderText = "Quantity"
        gv.MasterTemplate.Columns.Add(repoqty)
        repoqty = Nothing

        Dim repoqty1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty1.FormatString = ""
        repoqty1.Name = colBOMFatPers
        repoqty1.Width = 80
        repoqty1.DecimalPlaces = 2
        repoqty1.HeaderText = "FAT%"
        repoqty1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoqty1)
        repoqty1 = Nothing

        Dim repoqty2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty2.FormatString = ""
        repoqty2.Name = colBOMFatkg
        repoqty2.Width = 80
        repoqty2.DecimalPlaces = DecimalPoint
        repoqty2.HeaderText = "FAT KG"
        repoqty2.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoqty2)
        repoqty2 = Nothing

        Dim repoqty3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty3.FormatString = ""
        repoqty3.Name = colBOMSNFPers
        repoqty3.Width = 80
        repoqty3.DecimalPlaces = 2
        repoqty3.HeaderText = "SNF%"
        repoqty3.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoqty3)
        repoqty3 = Nothing

        Dim repoqty4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty4.FormatString = ""
        repoqty4.Name = colBOMSNFkg
        repoqty4.Width = 80
        repoqty4.DecimalPlaces = DecimalPoint
        repoqty4.HeaderText = "SNF KG"
        repoqty4.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoqty4)
        repoqty4 = Nothing

        Dim reposhiftcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposhiftcode.FormatString = ""
        reposhiftcode.Name = colShiftcode
        reposhiftcode.Width = 80
        reposhiftcode.HeaderText = "Shift Code"
        reposhiftcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        reposhiftcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(reposhiftcode)
        reposhiftcode = Nothing

        Dim reposhift As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposhift.FormatString = ""
        reposhift.Name = colShiftDesc
        reposhift.Width = 130
        reposhift.HeaderText = "Shift Description"
        reposhift.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposhift)
        reposhift = Nothing

        Dim reposectiontcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposectiontcode.FormatString = ""
        reposectiontcode.Name = colSection
        reposectiontcode.Width = 80
        reposectiontcode.HeaderText = "Section Code"
        reposectiontcode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposectiontcode)
        reposectiontcode = Nothing

        Dim reposection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposection.FormatString = ""
        reposection.Name = colSectionName
        reposection.Width = 130
        reposection.HeaderText = "Section Description"
        reposection.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposection)
        reposection = Nothing

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.EnableFiltering = False
        gv.Rows.AddNew()
    End Sub

    Sub LoadBlankRawGrid()
        gvraw.Rows.Clear()
        gvraw.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colRawsno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim rawbom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawbom.FormatString = ""
        rawbom.Name = colrawbomcode
        rawbom.Width = 80
        rawbom.HeaderText = "BOM Code"
        rawbom.ReadOnly = True
        rawbom.IsVisible = False
        gvraw.MasterTemplate.Columns.Add(rawbom)
        rawbom = Nothing

        Dim rawproditem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawproditem.FormatString = ""
        rawproditem.Name = colrawprodtitem
        rawproditem.Width = 80
        rawproditem.HeaderText = "Main Item Code"
        rawproditem.ReadOnly = True
        rawproditem.IsVisible = False
        gvraw.MasterTemplate.Columns.Add(rawproditem)
        rawproditem = Nothing

        Dim rawicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawicode.FormatString = ""
        rawicode.Name = colRawIcode
        rawicode.Width = 110
        rawicode.HeaderText = "Item Code"
        rawicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        rawicode.TextImageRelation = TextImageRelation.TextBeforeImage
        rawicode.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawicode)
        rawicode = Nothing

        Dim rawiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawiname.FormatString = ""
        rawiname.Name = colRawIname
        rawiname.Width = 200
        rawiname.HeaderText = "Description"
        rawiname.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawiname)
        rawiname = Nothing

        Dim rawitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawitype.FormatString = ""
        rawitype.Name = colRawItype
        rawitype.Width = 100
        rawitype.HeaderText = "Item Type"
        rawitype.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawitype)
        rawitype = Nothing

        Dim rawiunit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawiunit.FormatString = ""
        rawiunit.Name = colRawUnit
        rawiunit.Width = 100
        rawiunit.HeaderText = "UOM"
        rawiunit.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawiunit)
        rawiunit = Nothing

        Dim rawproduct As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawproduct.FormatString = ""
        rawproduct.Name = colRawProductType
        rawproduct.Width = 120
        rawproduct.HeaderText = "Product Type"
        rawproduct.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawproduct)
        rawproduct = Nothing

        Dim rawqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        rawqty.FormatString = ""
        rawqty.Name = colRawQty
        rawqty.Width = 100
        rawqty.DecimalPlaces = DecimalPoint
        rawqty.HeaderText = "Quantity"
        rawqty.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawqty)
        rawqty = Nothing

        Dim rawfat As GridViewDecimalColumn = New GridViewDecimalColumn()
        rawfat.FormatString = ""
        rawfat.Name = colFat
        rawfat.Width = 100
        rawfat.DecimalPlaces = 2
        rawfat.HeaderText = "FAT%"
        rawfat.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawfat)
        rawfat = Nothing

        Dim rawfatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        rawfatkg.FormatString = ""
        rawfatkg.Name = colFAT_KG
        rawfatkg.Width = 100
        rawfatkg.DecimalPlaces = DecimalPoint
        rawfatkg.HeaderText = "FAT KG"
        rawfatkg.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawfatkg)
        rawfatkg = Nothing

        Dim rawsnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        rawsnf.FormatString = ""
        rawsnf.Name = colSNF
        rawsnf.Width = 100
        rawsnf.DecimalPlaces = 2
        rawsnf.HeaderText = "SNF%"
        rawsnf.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawsnf)
        rawsnf = Nothing

        Dim rawsnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        rawsnfkg.FormatString = ""
        rawsnfkg.Name = colSNF_KG
        rawsnfkg.Width = 100
        rawsnfkg.DecimalPlaces = DecimalPoint
        rawsnfkg.HeaderText = "SNF KG"
        rawsnfkg.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawsnfkg)
        rawsnfkg = Nothing

        Dim rawstatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        rawstatus.FormatString = ""
        rawstatus.Name = colBatchstatus
        rawstatus.Width = 50
        rawstatus.HeaderText = "Allow Sub-Batch"
        rawstatus.DataSource = LoadCombobox()
        rawstatus.DisplayMember = "Name"
        rawstatus.ValueMember = "Code"
        rawstatus.ReadOnly = True
        gvraw.MasterTemplate.Columns.Add(rawstatus)
        rawstatus = Nothing

        Dim rawsubbatchcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawsubbatchcode.FormatString = ""
        rawsubbatchcode.Name = colSubBatchcode
        rawsubbatchcode.Width = 50
        rawsubbatchcode.ReadOnly = True
        rawsubbatchcode.HeaderText = "Batch Code"
        gvraw.MasterTemplate.Columns.Add(rawsubbatchcode)
        rawsubbatchcode = Nothing

        Dim rawremarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        rawremarks.FormatString = ""
        rawremarks.Name = colrem
        rawremarks.Width = 150
        rawremarks.MaxLength = 250
        rawremarks.HeaderText = "Remarks"
        gvraw.MasterTemplate.Columns.Add(rawremarks)
        rawremarks = Nothing

        gvraw.AllowDeleteRow = True
        gvraw.AllowAddNewRow = False
        gvraw.ShowGroupPanel = False
        gvraw.AllowColumnReorder = True
        gvraw.AllowRowReorder = False
        gvraw.EnableSorting = False
        gvraw.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvraw.MasterTemplate.ShowRowHeaderColumn = False
        gvraw.EnableFiltering = False
        gvraw.Rows.AddNew()
    End Sub

    Private Function LoadCombobox() As DataTable
        Dim qry As String = "select a.Code,a.Name from (select 'Yes' as code,'Yes' as Name union all select 'No' as code,'No' as Name)a"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function


    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(cboBOMStatus.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(cboBOMStatus, "Please set status of batch order")
                cboBOMStatus.Select()
                Throw New Exception("Please set status of batch order")
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If

            If clsCommon.myLen(txtlocationcode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtlocationname, "Select location for batch order")
                txtlocationcode.Focus()
                txtlocationcode.Select()
                Throw New Exception("Select location for batch order")
            Else
                Errorcontrol.ResetError(txtlocationname)
            End If

            If clsCommon.myLen(txticategory_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtitemcategory_name, "Select production category for batch order")
                txticategory_code.Focus()
                txticategory_code.Select()
                Throw New Exception("Select production category for batch order")
            Else
                Errorcontrol.ResetError(txtitemcategory_name)
            End If

            '' richa agarwal 4 Sep,2018 BHA/04/09/18-000504
            If ManualBatchNoMandatoryOnBatchOrderScreen = True Then
                If clsCommon.myLen(clsCommon.myCstr(txtManualBatchNo.Text)) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Errorcontrol.SetError(txtManualBatchNo, "Please enter Manual Batch No.")
                    txtManualBatchNo.Focus()
                    txtManualBatchNo.Select()
                    Throw New Exception("Please enter Manual Batch No.")
                Else
                    Errorcontrol.ResetError(txtManualBatchNo)
                End If
            End If

            '-================================validation for gvraw=========================
            Dim olditemcode As String = Nothing
            Dim itemcode As String = Nothing
            Dim itemqty As Decimal = Nothing
            Dim fatv As Decimal = Nothing
            Dim snfv As Decimal = Nothing
            Dim fatv_kg As Decimal = Nothing
            Dim snfv_kg As Decimal = Nothing
            Dim itemprodct As String = Nothing
            Dim fractnal As Decimal = Nothing
            Dim arrIcode As List(Of String) = Nothing

            arrIcode = New List(Of String)

            For ii As Integer = 0 To gvraw.Rows.Count - 1
                gvraw.Focus()
                gvraw.Select()

                itemcode = clsCommon.myCstr(gvraw.Rows(ii).Cells(colRawIcode).Value)
                itemqty = clsCommon.myCdbl(gvraw.Rows(ii).Cells(colRawQty).Value)
                fatv = clsCommon.myCdbl(gvraw.Rows(ii).Cells(colFat).Value)
                snfv = clsCommon.myCdbl(gvraw.Rows(ii).Cells(colSNF).Value)
                fatv_kg = clsCommon.myCdbl(gvraw.Rows(ii).Cells(colFAT_KG).Value)
                snfv_kg = clsCommon.myCdbl(gvraw.Rows(ii).Cells(colSNF_KG).Value)
                itemprodct = clsCommon.myCstr(gvraw.Rows(ii).Cells(colRawProductType).Value)

                If Not arrIcode.Contains(itemcode) AndAlso clsCommon.myLen(itemcode) > 0 Then
                    arrIcode.Add(itemcode)
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso itemqty <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Errorcontrol.SetError(gvraw, "Fill quanity at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    gvraw.CurrentRow = gvraw.Rows(ii)
                    gvraw.CurrentColumn = gvraw.Columns(colRawQty)
                    Throw New Exception("Fill quanity at row no. " + clsCommon.myCstr(ii + 1) + ".")
                Else
                    Errorcontrol.ResetError(gvraw)
                End If

                If clsCommon.myLen(itemcode) > 0 Then
                    gvraw.Rows(ii).Cells(colFat).Value = clsCommon.myCdbl(clsBOM.GetFAT_PERS(itemcode))
                    gvraw.Rows(ii).Cells(colSNF).Value = clsCommon.myCdbl(clsBOM.GetSNF_PERS(itemcode))
                    gvraw.Rows(ii).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gvraw.Rows(ii).Cells(colRawUnit).Value), clsCommon.myCdbl(gvraw.Rows(ii).Cells(colRawQty).Value), clsCommon.myCdbl(gvraw.Rows(ii).Cells(colFat).Value), Nothing)
                    gvraw.Rows(ii).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gvraw.Rows(ii).Cells(colRawUnit).Value), clsCommon.myCdbl(gvraw.Rows(ii).Cells(colRawQty).Value), clsCommon.myCdbl(gvraw.Rows(ii).Cells(colSNF).Value), Nothing)
                End If

                For jj As Integer = ii + 1 To gvraw.Rows.Count - 1
                    olditemcode = clsCommon.myCstr(gvraw.Rows(jj).Cells(colRawIcode).Value)

                    If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.CompairString(itemcode, olditemcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvraw.Rows(ii).Cells(colRawUnit).Value), clsCommon.myCstr(gvraw.Rows(jj).Cells(colRawUnit).Value)) = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage2
                        Errorcontrol.SetError(gvraw, "Duplicate item not allowed at row no. " + clsCommon.myCstr(jj + 1) + ".")
                        gvraw.CurrentRow = gvraw.Rows(jj)
                        gvraw.CurrentColumn = gvraw.Columns(colRawIcode)
                        Throw New Exception("Duplicate item not allowed at row no. " + clsCommon.myCstr(jj + 1) + ".")
                    Else
                        Errorcontrol.ResetError(gvraw)
                    End If
                Next
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage2
                Errorcontrol.SetError(gvraw, "Fill atleast one row in rawmaterial grid.")
                gvraw.Focus()
                gvraw.Select()
                Throw New Exception("Fill atleast one row in rawmaterial grid.")
            Else
                Errorcontrol.ResetError(gvraw)
            End If
            '===========================================================






            itemcode = Nothing
            itemqty = Nothing
            olditemcode = Nothing
            Dim bomcode As String = Nothing
            Dim shiftcode As String = Nothing

            arrIcode = New List(Of String)

            For ii As Integer = 0 To gv.Rows.Count - 1
                gv.Focus()
                gv.Select()

                itemcode = clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)
                bomcode = clsCommon.myCstr(gv.Rows(ii).Cells(colBOM).Value)
                itemqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value)
                shiftcode = clsCommon.myCstr(gv.Rows(ii).Cells(colShiftcode).Value)

                If Not arrIcode.Contains(itemcode) AndAlso clsCommon.myLen(itemcode) > 0 Then
                    arrIcode.Add(itemcode)
                End If

                If clsCommon.myLen(itemcode) > 0 Then
                    gv.Rows(ii).Cells(colBOMFatPers).Value = clsCommon.myCdbl(clsBOM.GetFAT_PERS(itemcode))
                    gv.Rows(ii).Cells(colBOMSNFPers).Value = clsCommon.myCdbl(clsBOM.GetSNF_PERS(itemcode))
                    gv.Rows(ii).Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gv.Rows(ii).Cells(colUnit).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(colBOMFatPers).Value), Nothing)
                    gv.Rows(ii).Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gv.Rows(ii).Cells(colUnit).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(colBOMSNFPers).Value), Nothing)
                End If

                If clsCommon.myLen(itemcode) <= 0 AndAlso clsCommon.myLen(bomcode) > 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Errorcontrol.SetError(gv, "Select item detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colitemcode)
                    Throw New Exception("Select item detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.myLen(bomcode) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Errorcontrol.SetError(gv, "Select bom detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colBOM)
                    Throw New Exception("Select bom detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso itemqty <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Errorcontrol.SetError(gv, "Fill item quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colQty)
                    Throw New Exception("Fill item quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.myLen(shiftcode) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Errorcontrol.SetError(gv, "Select shift detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colShiftcode)
                    Throw New Exception("Select shift detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If

                For jj As Integer = ii + 1 To gv.Rows.Count - 1
                    olditemcode = clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)

                    If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.CompairString(itemcode, olditemcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colUnit).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colUnit).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colBOM).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colBOM).Value)) = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Errorcontrol.SetError(gv, "No duplicate item allowed at row no. " + clsCommon.myCstr(jj + 1) + "")
                        gv.CurrentRow = gv.Rows(jj)
                        gv.CurrentColumn = gv.Columns(colitemcode)
                        Throw New Exception("No duplicate item allowed at row no. " + clsCommon.myCstr(jj + 1) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If
                Next
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(gv, "Fill atleast one row in main item grid.")
                gv.Focus()
                gv.Select()
                Throw New Exception("Fill atleast one row in main item grid.")
            Else
                Errorcontrol.ResetError(gv)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If clsCommon.CompairString(btnsave.Text, "Save") <> CompairStringResult.Equal Then
            Dim main_bo As String = txtCode.Value
            Dim counter As Integer = 0
            While (clsCommon.myLen(main_bo) > 0)
                main_bo = clsProcessBatchOrder.GetMainBO(main_bo, Nothing)
                If clsCommon.myLen(main_bo) > 0 Then
                    counter += 1
                End If
            End While

            If counter > 1 Then
                If Not clsCommon.MyMessageBoxShow("All the associated subchild batch order(s) are recreated,Want to continue?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
        End If
        CheckSaveData(False)
    End Sub

    Private Sub CheckSaveData(ByVal isPost As Boolean)
        Try
            Dim noOfSfg As Integer = 0
            Dim SFGItem As String = ""
            Dim SFGItemQty As String = ""
            Dim SFGItemUOM As String = ""
            Dim ChildBO_Code As String = ""

            For Each grow As GridViewRowInfo In gvraw.Rows 'Finished Good
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colRawItype).Value), "Finished Good") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colRawItype).Value), "Semi Finished Good") = CompairStringResult.Equal Then
                    noOfSfg += 1
                    SFGItem = SFGItem + "," + clsCommon.myCstr(grow.Cells(colRawIcode).Value)
                    SFGItemQty = SFGItemQty + "," + clsCommon.myCstr(grow.Cells(colRawQty).Value)
                    SFGItemUOM = SFGItemUOM + "," + clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                    ChildBO_Code = ChildBO_Code + "," + clsCommon.myCstr(grow.Cells(colSubBatchcode).Value)
                End If
            Next

            If noOfSfg <= 0 OrElse activateSFGProduction = True Then
                If AllowToSave() Then SaveData(isPost, "", "")
            Else
                If AllowToSave() Then
                    '----------------if no row in child BO and no of sfg is greater than 1 than open child bo selection open----------------
                    '============create temp table for updating BO ref. no.------------------
                    clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CHILDBO_REF_NO where Main_BO_Code='" + txtCode.Value + "'")

                    Dim frm As New FrmChildBOScreen()
                    frm.ManualBOMSelect = ManualBOMSelect
                    frm.LoadBlankChildGrid()
                    frm.gv_Child_BO.Rows.Clear()
                    frm.dtpDate = dtpDate.Text
                    frm.BO_Code = clsCommon.myCstr(txtCode.Value)
                    frm.Location_Code = clsCommon.myCstr(txtlocationcode.Value)
                    frm.btn_Child_Close.Text = btnsave.Text
                    frm.ManualBOMSelect = ManualBOMSelect

                    If noOfSfg > 0 Then
                        For Each grow As GridViewRowInfo In gvraw.Rows
                            frm.isInsideLoadData = True
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colRawItype).Value), "Finished Good") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colRawItype).Value), "Semi Finished Good") = CompairStringResult.Equal Then
                                frm.gv_Child_BO.Rows.AddNew()

                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CSNo").Value = frm.gv_Child_BO.Rows.Count
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("IsChild").Value = False 'colCIsChild
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").Value = False 'colCGowithCalulation
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value = Math.Round(clsProcessBatchOrder.GetBalance(clsCommon.myCstr(grow.Cells(colRawIcode).Value), clsCommon.myCstr(grow.Cells(colRawProductType).Value), clsCommon.myCstr(grow.Cells(colRawUnit).Value), txtlocationcode.Value, txtCode.Value, dtpDate.Text, Nothing), DecimalPoint)
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value = Math.Round(clsProcessBatchOrder.GetBOAvailQty(txtCode.Value, clsCommon.myCstr(grow.Cells(colRawIcode).Value), clsCommon.myCstr(grow.Cells(colRawUnit).Value), txtlocationcode.Value), DecimalPoint)

                                If clsCommon.myCdbl(frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value) <= 0 Then
                                    frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value = 0
                                    frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value = 0
                                End If

                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("DiffStock").Value = Math.Round((clsCommon.myCdbl(frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CStockQty").Value) - clsCommon.myCdbl(frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CAvailQty").Value)) - clsCommon.myCdbl(grow.Cells(colRawQty).Value), DecimalPoint)
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CItemcode").Value = clsCommon.myCstr(grow.Cells(colRawIcode).Value) 'colCitemCode
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CItemdesc").Value = clsCommon.myCstr(grow.Cells(colRawIname).Value) 'colCItemdesc
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CItemUOM").Value = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CBOQty").Value = clsCommon.myCdbl(grow.Cells(colRawQty).Value) 'colCBOQty
                                frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("CParentSNO").Value = "0" 'use for taking reference of child,sub-child
                                If ManualBOMSelect = 1 Then
                                    frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("colBomCOde").Value = clsProcessSFGBatchOrder.GetBOM(txtsub_batch.Text, clsCommon.myCstr(grow.Cells(colRawIcode).Value), Nothing)
                                End If


                                If clsCommon.myCdbl(frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("DiffStock").Value) <= 0 Then
                                    frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").ReadOnly = True
                                Else
                                    frm.gv_Child_BO.Rows(frm.gv_Child_BO.Rows.Count - 1).Cells("GoWithCalculation").ReadOnly = False
                                End If
                            End If

                            frm.isInsideLoadData = False
                        Next

                        'frm.UpdateChildBO_Grid(btnsave.Text, SFGItem, SFGItemQty, SFGItemUOM, ChildBO_Code)
                        RadPageView1.SelectedPage = RadPageViewPage1
                        frm.ShowDialog()

                        If clsCommon.CompairString(frm.Error_Status, "E") <> CompairStringResult.Equal Then
                            If AllowToSave() Then SaveData(isPost, frm.SFGCodes, frm.SFGQty, frm.SFGUOM, frm.SFGREFNO)
                        End If
                        'RadGroupBox2.Visible = True
                        Exit Sub
                    End If
                    '-------------------------------------

                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub SaveData(ByVal isPost As Boolean, ByVal SFGCodesW As String, ByVal SFGQtyW As String, Optional ByVal SFGUOMW As String = Nothing, Optional ByVal SFGRefNo As String = Nothing)
        Dim obj As New clsProcessBatchOrder()
        Try
            obj.Batchcode = clsCommon.myCstr(txtCode.Value)
            obj.Batchdate = clsCommon.myCDate(dtpDate.Text)
            obj.batchdesc = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
            obj.status = clsCommon.myCstr(cboBOMStatus.Text)
            obj.IsPost = "0"
            If clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
                'obj.IsPost = "1"
            End If
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            obj.LINE_NO = clsCommon.myCstr(FndLineNo.Value)
            obj.CostCenterCode = clsCommon.myCstr(TxtCostCenterCode.Value)
            obj.ProfitCenterCode = clsCommon.myCstr(TxtProfitCenterCode.Value)
            ''----------------
            obj.locationcode = clsCommon.myCstr(txtlocationcode.Value)
            obj.Is_Job_Work_Inward = chkJobWorkInward.Checked
            obj.itemcatcode = clsCommon.myCstr(txticategory_code.Value)
            obj.Plancode = clsCommon.myCstr(txtplancode.Value)
            obj.Main_batchcode = clsCommon.myCstr(txtmain_batch.Text)
            obj.Sub_batchcode = "" 'clsCommon.myCstr(txtsub_batch.Text)
            obj.Section_Code = clsCommon.myCstr(txtSectionCode.Text)
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            obj.ManualBatchNo = clsCommon.myCstr(txtManualBatchNo.Text)
            obj.closeyn = "0"
            If chkclose.Checked Then
                obj.closeyn = "1"
            End If
            '--------------------------main item grid
            Dim Shift_Code As String = ""

            obj.ArrMainItem = New List(Of clsProcessBatchOrderMainDetail)
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsProcessBatchOrderMainDetail()
                objtr.SNO = CInt(grow.Cells(collineno).Value)
                objtr.bomcode = clsCommon.myCstr(grow.Cells(colBOM).Value)
                objtr.BOM_Revision_No = clsCommon.myCstr(grow.Cells(colBOMRevisionNo).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                objtr.qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objtr.shiftcode = clsCommon.myCstr(grow.Cells(colShiftcode).Value)
                objtr.sectioncode = clsCommon.myCstr(grow.Cells(colSection).Value)
                objtr.bom_fat_kg = clsCommon.myCdbl(grow.Cells(colBOMFatkg).Value)
                objtr.bom_fat_pers = clsCommon.myCdbl(grow.Cells(colBOMFatPers).Value)
                objtr.bom_snf_kg = clsCommon.myCdbl(grow.Cells(colBOMSNFkg).Value)
                objtr.bom_snf_pers = clsCommon.myCdbl(grow.Cells(colBOMSNFPers).Value)
                objtr.Plan_Code = clsCommon.myCstr(grow.Cells(colPlanCode).Value)
                If clsCommon.myLen(objtr.icode) > 0 Then
                    Shift_Code = objtr.shiftcode
                    obj.ArrMainItem.Add(objtr)
                End If
            Next

            SFGCodes = SFGCodesW
            Dim SFGQty As String = SFGQtyW
            '====================get SFG Item from grid=====================
            'For Each grow As GridViewRowInfo In gv_Child_BO.Rows
            '    If clsCommon.myCBool(grow.Cells(colCIsChild).Value) = True AndAlso clsCommon.myCBool(grow.Cells(colCGowithCalulation).Value) = True Then
            '        SFGCodes = SFGCodes + "," + clsCommon.myCstr(grow.Cells(colCitemCode).Value)
            '        SFGQty = SFGQty + "," + clsCommon.myCstr(grow.Cells(colCDiffStock).Value)
            '    ElseIf clsCommon.myCBool(grow.Cells(colCIsChild).Value) = True AndAlso clsCommon.myCBool(grow.Cells(colCGowithCalulation).Value) = False Then
            '        SFGCodes = SFGCodes + "," + clsCommon.myCstr(grow.Cells(colCitemCode).Value)
            '        SFGQty = SFGQty + "," + clsCommon.myCstr(grow.Cells(colCBOQty).Value)
            '    End If
            'Next
            '==========================================================
            '---------------------------raw item
            obj.ArrRawItem = New List(Of clsProcessBatchOrderRawDetail)
            For Each grow As GridViewRowInfo In gvraw.Rows
                Dim objtr As New clsProcessBatchOrderRawDetail()

                objtr.Rawsno = CInt(grow.Cells(colRawsno).Value)
                objtr.Rawomcode = clsCommon.myCstr(grow.Cells(colrawbomcode).Value)
                objtr.Proditem = clsCommon.myCstr(grow.Cells(colrawprodtitem).Value)
                objtr.rawicode = clsCommon.myCstr(grow.Cells(colRawIcode).Value)
                objtr.rawunit = clsCommon.myCstr(grow.Cells(colRawUnit).Value)
                objtr.rawqty = clsCommon.myCdbl(grow.Cells(colRawQty).Value)
                objtr.fat = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objtr.snf = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objtr.fat_kg = clsCommon.myCdbl(grow.Cells(colFAT_KG).Value)
                objtr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNF_KG).Value)
                If clsCommon.myLen(SFGCodes) > 0 Then
                    objtr.rawbatchcode = clsCommon.myCstr(grow.Cells(colSubBatchcode).Value)
                Else
                    objtr.rawbatchcode = ""
                End If

                'If clsCommon.myLen(grow.Cells(colSubBatchcode).Value) > 0 AndAlso clsCommon.CompairString(grow.Cells(colBatchstatus).Value, "No") <> CompairStringResult.Equal Then
                '    objtr.rawbatchcode = clsCommon.myCstr(grow.Cells(colSubBatchcode).Value)

                '    Dim qry As String = "select isnull(is_post,0) as ispost from TSPL_PP_BATCH_ORDER_HEAD where Main_Batch_Code='" + txtCode.Value + "' and batch_code='" + objtr.rawbatchcode + "'"
                '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                '    '==============if posted then no need to add in array for further save process==========
                '    If check <= 0 Then
                '        SFGCodes = SFGCodes + "','" + objtr.rawicode
                '    ElseIf check > 0 Then
                '        obj.SubBatchAfterPost = obj.SubBatchAfterPost + "," + objtr.rawbatchcode
                '    End If
                'Else
                '    objtr.rawbatchcode = clsCommon.myCstr(grow.Cells(colBatchstatus).Value)
                '    If clsCommon.CompairString(objtr.rawbatchcode, "Yes") = CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells(colRawItype).Value, "Semi Finished Good") = CompairStringResult.Equal Then
                '        SFGCodes = SFGCodes + "','" + objtr.rawicode
                '    End If
                'End If

                objtr.remarks = clsCommon.myCstr(grow.Cells(colrem).Value).Replace("'", "`")

                If clsCommon.myLen(objtr.rawicode) > 0 Then
                    obj.ArrRawItem.Add(objtr)
                End If
            Next

            If clsProcessBatchOrder.SaveData(obj, isNewEntry) Then
                If Not isPost Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If

                txtCode.Value = obj.Batchcode
                txtCode.MyReadOnly = True
                btnsave.Enabled = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True

                UcAttachment1.SaveData(txtCode.Value)

                '======================================saving batch order for SFG Goods=============================
                If clsCommon.myLen(SFGCodes) > 0 AndAlso activateSFGProduction = False Then

                    SFGCodes = SFGCodes.Replace(",", "','")
                    Dim LastRefNo As String = ""
                    Dim NewRefNo As String = ""
                    Dim child_counter As Integer = 0

                    '======================save SFG by there category structure---------------------
                    'Dim qry As String = "select Item_Code,Structure_Code from TSPL_ITEM_MASTER where Item_Code in ('" + SFGCodes + "') order by Item_Code"
                    Dim qry As String = "select COUNT(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_CHILDBO_REF_NO'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("update TEMP_CHILDBO_REF_NO set main_bo_code='" + txtCode.Value + "' where isnull(main_bo_code,'')=''")

                        qry = "select * from TEMP_CHILDBO_REF_NO where main_bo_code='" + txtCode.Value + "' and Status=1"
                        dt = New DataTable()
                        dt = clsDBFuncationality.GetDataTable(qry)
                        Dim counter As Integer = 1

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            LastRefNo = clsCommon.myCstr(dt.Rows(0)("ref_sno"))
                            For Each dr As DataRow In dt.Rows
                                Dim objSFG As New clsProcessSFGBatchOrder()

                                Dim bo_no As String = ""
                                bo_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bo_no from TEMP_CHILDBO_REF_NO where main_bo_code='" + txtCode.Value + "' and Status=1 and sno='" + clsCommon.myCstr(dr("sno")) + "'"))
                                NewRefNo = clsCommon.myCstr(dr("ref_sno"))

                                If clsCommon.myLen(bo_no) <= 0 Then
                                    objSFG.Batchcode = txtCode.Value + "." + clsCommon.myCstr(counter)
                                    objSFG.batchdesc = "Child Batch Order,Having Main Batch Order No. " + clsCommon.myCstr(txtCode.Value) + ""
                                    objSFG.Main_batchcode = clsCommon.myCstr(txtCode.Value)
                                Else
                                    If clsCommon.CompairString(LastRefNo, NewRefNo) = CompairStringResult.Equal Then
                                        child_counter += 1
                                    Else
                                        child_counter = 1
                                    End If
                                    objSFG.Batchcode = bo_no + "." + clsCommon.myCstr(child_counter) 'clsCommon.myCstr(dr("level"))
                                    objSFG.Main_batchcode = clsCommon.myCstr(bo_no)
                                    objSFG.batchdesc = "Child Batch Order,Having Main Batch Order No. " + clsCommon.myCstr(bo_no) + ""
                                End If
                                objSFG.Batchcode = NameByLevels(clsCommon.myCstr(dr("SNO"))) + objSFG.Batchcode
                                'End If
                                objSFG.Batchdate = clsCommon.myCDate(dtpDate.Text)
                                objSFG.status = clsCommon.myCstr(cboBOMStatus.Text)
                                objSFG.IsPost = "0"
                                If clsCommon.CompairString(obj.IsPost, "1") = CompairStringResult.Equal Then
                                    objSFG.IsPost = "1"
                                End If
                                objSFG.locationcode = clsCommon.myCstr(txtlocationcode.Value)
                                objSFG.itemcatcode = clsCommon.myCstr(dr("Struct_Code")) 'Structure_Code
                                objSFG.Plancode = "" ' clsCommon.myCstr(txtplancode.Value)
                                objSFG.Sub_batchcode = ""
                                '' DONE BY PANCH RAJ
                                If ManualBOMSelect = 1 Then
                                    objSFG.bomcode = clsCommon.myCstr(dr("BOMCODE"))
                                End If
                                objSFG.closeyn = "0"
                                If chkclose.Checked Then
                                    objSFG.closeyn = "1"
                                End If

                                objSFG.shiftcode = Shift_Code

                                objSFG.ArrMainItem = New List(Of clsProcessSFGBatchOrder)
                                objSFG.ArrRawItem = New List(Of clsProcessSFGBatchOrder)

                                Dim qty As Decimal = clsCommon.myCdbl(dr("Qty")) ' clsProcessSFGBatchOrder.GetSFGQtyAT_SAVE(SFGCodes, SFGQty, clsCommon.myCstr(dr("item_code")))

                                If clsProcessSFGBatchOrder.SaveData(objSFG, qty, clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("UOM")), True) Then
                                    If clsCommon.myLen(bo_no) <= 0 Then
                                        counter += 1
                                    End If

                                    clsDBFuncationality.ExecuteNonQuery("update TEMP_CHILDBO_REF_NO set bo_no='" + txtCode.Value + "' where main_bo_code='" + txtCode.Value + "' and Status=1 and Sno='" + clsCommon.myCstr(dr("sno")) + "'") ',level=row_number() over (order by sno)
                                    clsDBFuncationality.ExecuteNonQuery("update TEMP_CHILDBO_REF_NO set bo_no='" + objSFG.Batchcode + "' where main_bo_code='" + txtCode.Value + "' and Status=1 and ref_Sno='" + clsCommon.myCstr(dr("Sno")) + "'") ',level=row_number() over (order by sno)

                                    LastRefNo = clsCommon.myCstr(dr("ref_sno"))
                                End If
                            Next
                        End If '==end table check cond.
                    End If '==end sfg check cond.

                End If
                '=====================================================================================================
                isSavedSuccess = True

                'gv_Child_BO.Rows.Clear()
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            isSavedSuccess = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Function NameByLevels(ByVal SNO As String) As String
        Dim str As String = ""
        Dim noOfDots As Integer = 1
        For Each c As Char In clsCommon.myCstr(SNO)
            If clsCommon.CompairString(c, ".") = CompairStringResult.Equal Then
                noOfDots += 1
            End If
        Next

        For ii As Integer = 0 To noOfDots
            If ii = 1 Then
                str = "C-"
            ElseIf ii > 1 Then
                str = "S" + str
            End If
        Next

        Return str
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsProcessBatchOrder()
        Try

            btnExport.Visible = False
            RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
            AllBomCode = ""
            isInsideLoadData = True
            isNewEntry = True
            isSavedSuccess = True
            txtlocationcode.Enabled = True

            chkclose.Enabled = True
            Dim SFG_Counter As Integer = 0

            If clsCommon.myLen(arrLoc) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Set location rights.", Me.Text)
                Exit Sub
            End If

            obj = clsProcessBatchOrder.GetData(strCode, arrLoc, NavType)
            

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Batchcode) > 0 Then
                isNewEntry = False
                txtCode.Value = obj.Batchcode
                dtpDate.Text = obj.Batchdate
                txtdesc.Text = obj.batchdesc
                cboBOMStatus.Text = obj.status
                If obj.IsPost = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If
                txtlocationcode.Value = obj.locationcode
                txtlocationname.Text = obj.locationname
                chkJobWorkInward.Checked = obj.Is_Job_Work_Inward
                txticategory_code.Value = obj.itemcatcode
                txtitemcategory_name.Text = obj.itemcatname
                txtplancode.Value = obj.Plancode
                txtplan_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + txtplancode.Value + "'"))
                txtmain_batch.Text = obj.Main_batchcode
                txtSectionCode.Text = obj.Section_Code
                txtSectionName.Text = obj.Section_Name
                txtsub_batch.Text = obj.Sub_batchcode.Replace("'", "")
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                txtManualBatchNo.Text = obj.ManualBatchNo
                If clsCommon.myLen(txtsub_batch.Text) > 0 Then
                    If clsCommon.myCstr(txtsub_batch.Text).Substring(0, 1) = "," Then
                        txtsub_batch.Text = txtsub_batch.Text.Substring(1, txtsub_batch.Text.Length - 1)
                    End If
                End If
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                FndLineNo.Value = obj.LINE_NO
                TxtCostCenterCode.Value = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                TxtProfitCenterCode.Value = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                If clsCommon.myLen(obj.Plancode) > 0 Then
                    FndLineNo.Enabled = False
                    TxtCostCenterCode.Enabled = False
                Else
                    FndLineNo.Enabled = True
                    TxtCostCenterCode.Enabled = True
                End If
                ''----------------
                chkclose.Checked = IIf(obj.closeyn = "1", True, False)

                gv.Rows.Clear()
                gvraw.Rows.Clear()

                If obj.ArrMainItem IsNot Nothing AndAlso obj.ArrMainItem.Count > 0 Then
                    For Each objtr As clsProcessBatchOrderMainDetail In obj.ArrMainItem
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(collineno).Value = objtr.SNO
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOM).Value = objtr.bomcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_pp_bom_head where bom_code='" + objtr.bomcode + "'"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMRevisionNo).Value = objtr.BOM_Revision_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.icode
                        gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = objtr.iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colItype).Value = ItemType(objtr.itype)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = objtr.UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colShiftcode).Value = objtr.shiftcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colShiftDesc).Value = objtr.shiftname
                        gv.Rows(gv.Rows.Count - 1).Cells(colSection).Value = objtr.sectioncode
                        gv.Rows(gv.Rows.Count - 1).Cells(colSectionName).Value = objtr.sectionname
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMFatkg).Value = objtr.bom_fat_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMFatPers).Value = objtr.bom_fat_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMSNFkg).Value = objtr.bom_snf_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMSNFPers).Value = objtr.bom_snf_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colPlanCode).Value = objtr.Plan_Code

                        AllBomCode = AllBomCode + "','" + objtr.bomcode
                    Next
                End If

                If obj.ArrRawItem IsNot Nothing AndAlso obj.ArrRawItem.Count > 0 Then
                    For Each objtr As clsProcessBatchOrderRawDetail In obj.ArrRawItem
                        gvraw.Rows.AddNew()
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawsno).Value = objtr.Rawsno
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colrawbomcode).Value = objtr.Rawomcode
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colrawprodtitem).Value = objtr.Proditem
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIcode).Value = objtr.rawicode
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIname).Value = objtr.rawiname
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawItype).Value = ItemType(objtr.rawitype)
                        If clsCommon.CompairString(objtr.rawitype, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.rawitype, "F") = CompairStringResult.Equal Then
                            SFG_Counter += 1
                        End If
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawUnit).Value = objtr.rawunit
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawProductType).Value = ProductType(objtr.producttype)
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawQty).Value = objtr.rawqty
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).Value = objtr.fat
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).Value = objtr.snf
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).Value = objtr.fat_kg
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).Value = objtr.snf_kg
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colrem).Value = objtr.remarks
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSubBatchcode).Value = objtr.rawbatchcode
                        If clsCommon.CompairString(objtr.rawbatchcode, "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.rawbatchcode, "") <> CompairStringResult.Equal Then
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colBatchstatus).Value = "Yes"
                        Else
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colBatchstatus).Value = "No"
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawProductType).Value), "Milk") = CompairStringResult.Equal Then
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).ReadOnly = False
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).ReadOnly = False
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = False
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = False
                        Else
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).ReadOnly = True
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = True
                            gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = True
                        End If
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

                txtCode.MyReadOnly = True
                btnsave.Enabled = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True
                txticategory_code.Enabled = False
                'txtplancode.Enabled = False
                txtlocationcode.Enabled = False

                If obj.IsPost = "1" Or obj.closeyn = "1" Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                End If

                If clsCommon.myLen(txtmain_batch.Text) > 0 Then
                    'If SFG_Counter > 0 AndAlso obj.IsPost <> "1" AndAlso obj.closeyn <> "1" Then
                    '    btnsave.Enabled = True
                    'Else
                    '    btnsave.Enabled = False
                    'End If
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    chkclose.Enabled = False
                End If


                UcAttachment1.LoadData(txtCode.Value)
            Else
                FunReset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isSavedSuccess = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

            obj = Nothing
        End Try


    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtCode, "Select batch order code for deletion")
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select batch order code for deletion")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not myMessages.deleteConfirm() Then
                Return
            End If

            If clsProcessBatchOrder.DeleteData(txtCode.Value) Then
                myMessages.delete()
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(txtCode, "Select batch order code for posting")
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select batch order code for posting")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If clsCommon.CompairString(cboBOMStatus.Text, "Approved") <> CompairStringResult.Equal Then
                Errorcontrol.SetError(cboBOMStatus, "Select status as approved")
                RadPageView1.SelectedPage = RadPageViewPage1
                cboBOMStatus.Focus()
                cboBOMStatus.Select()
                Throw New Exception("Select status as approved")
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If

            Dim Saved As Boolean = True
            ''richa agarwal 22/06/2015 add yesnoCancel option through which user can cancel the post ticket no. BM00000007125
            'If Not clsCommon.MyMessageBoxShow("Do you want to post this record?without update" + Environment.NewLine + "(On Update all the associated subchild batch order(s) are recreated.)", "Attention", MessageBoxButtons.YesNoCancel, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            '        CheckSaveData(True)
            '        Saved = isSavedSuccess
            'End If

            If activateSFGProduction = False Then '' condition appled by Panch Raj against ticket no:ERO/12/06/18-000342
                Dim result As Integer = clsCommon.MyMessageBoxShow("Do you want to post this record?without update" + Environment.NewLine + "(On Update all the associated subchild batch order(s) are recreated.)", "Attention", MessageBoxButtons.YesNoCancel, RadMessageIcon.Question)
                If result = System.Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                ElseIf result = System.Windows.Forms.DialogResult.No Then
                    CheckSaveData(True)
                    Saved = isSavedSuccess
                End If
            Else
                If clsCommon.MyMessageBoxShow("Do you want to post the current Docuemnt [" + txtCode.Value + "]", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then ''BHA/19/06/19-000907 by balwinder on 20/06/2019 
                    Exit Sub
                End If
            End If
            
            ''-------------------------------
            If Saved Then
                If clsProcessBatchOrder.PostData(txtCode.Value) Then
                    myMessages.post()
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Set location rights", Me.Text)
        End If

        Dim qry As String = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + clsCommon.myCstr(txtCode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsProcessBatchOrder.GetFinder(" TSPL_PP_BATCH_ORDER_HEAD.location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        Else
            FunReset()
        End If
    End Sub

    Private Sub txtlocationcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocationcode._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Set location rights", Me.Text)
        End If
        txtlocationcode.Value = clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y'", txtlocationcode.Value, isButtonClicked)

        If clsCommon.myLen(txtlocationcode.Value) > 0 Then
            txtlocationname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txtlocationcode.Value + "'"))
            chkJobWorkInward.Checked = clsLocation.IsJobWorkLocation(txtlocationcode.Value, Nothing)
        Else
            chkJobWorkInward.Checked = False
            txtlocationname.Text = ""
            txtplancode.Value = ""
            gv.Rows.Clear()
            gv.Rows.AddNew()
            gvraw.Rows.Clear()
            gvraw.Rows.AddNew()
            txtmain_batch.Text = ""
            txtsub_batch.Text = ""
        End If
    End Sub

    Private Sub txtplancode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtplancode._MYValidating
        If clsCommon.myLen(txtlocationcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select location first.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtlocationcode.Focus()
            txtlocationcode.Select()
            Errorcontrol.SetError(txtlocationname, "Select location first.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtlocationname)
        End If

        If clsCommon.myLen(txticategory_code.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select production category first.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txticategory_code.Focus()
            txticategory_code.Select()
            Errorcontrol.SetError(txtitemcategory_name, "Select production category first.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtitemcategory_name)
        End If

        txtplancode.Value = clsProcessProductionPlanning.GetFinder(" TSPL_PP_PRODUCTION_PLAN_HEAD.location_code in ('" + txtlocationcode.Value + "') and TSPL_PP_PRODUCTION_PLAN_HEAD.Structure_Code='" + txticategory_code.Value + "' and TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code in (select a.plan_code from (select aax.plan_code,aax.item_code,sum(isnull(aax.final_qty,0)) as qty from (select finl.plan_code,finl.item_code,(case when len(isnull(finl.bo_unit,''))>0 then (finl.final_qty*tspl_item_uom_detail.conversion_factor/itemuom.conversion_factor) else finl.final_qty end) as final_Qty from (select finl1.Plan_Code,finl1.Item_Code,max(finl1.Unit_Code) as unit_code,max(finl1.bo_unit) as bo_unit,sum(finl1.Final_Qty) as final_qty from (select Plan_Code,Item_Code,unit_code,'' as bo_unit,(case when isnull(final_qty,0)>0 then final_qty else plan_qty end) as final_qty from TSPL_PP_PRODUCTION_PLAN_DETAIL union all select TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,'' as unit_code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.unit_code as bo_unit,sum(0-isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.quantity,0)) as final_qty from TSPL_PP_BATCH_ORDER_BOM_DETAIL where isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,'')<>'' and TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code not in ('" + txtCode.Value + "') group by TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.unit_code )finl1 group by finl1.Plan_Code,finl1.Item_Code   )finl left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=finl.item_code and tspl_item_uom_detail.uom_code=finl.unit_code left outer join tspl_item_uom_detail itemuom on itemuom.item_code=finl.item_code and itemuom.uom_code=finl.bo_unit )aax where aax.item_code in (select item_code from tspl_item_master where Structure_Code='" + txticategory_code.Value + "') group by aax.Plan_Code,aax.Item_Code)a where a.qty>0)", txtplancode.Value, isButtonClicked) 'isnull(TSPL_PP_PRODUCTION_PLAN_HEAD.is_post,'0')='1' and 
        txtplan_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + txtplancode.Value + "'"))
        txtSectionCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select section_code from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + txtplancode.Value + "'"))
        txtSectionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + txtSectionCode.Text + "'"))
        ''richa agarwal againt ticket no BHA/02/07/18-000120
        If clsCommon.myLen(txtplancode.Value) > 0 Then
            If UseProductionPlaningDateForWholeProductionCycle = True Then
                dtpDate.Value = Nothing
                dtpDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Plan_Date from TSPL_PP_PRODUCTION_PLAN_HEAD where plan_code='" + txtplancode.Value + "'"))
            End If
            Dim qry As String = " select TSPL_PP_PRODUCTION_PLAN_HEAD.Line_No as [Line No],TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name]" & _
            " from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_PRODUCTION_PLAN_HEAD.ProfitCenterCode " & _
            " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_PRODUCTION_PLAN_HEAD.CostCenterCode " & _
            " where TSPL_PP_PRODUCTION_PLAN_HEAD.plan_code='" & clsCommon.myCstr(txtplancode.Value) & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndLineNo.Value = clsCommon.myCstr(dt.Rows(0).Item("Line No"))
                TxtCostCenterCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Code"))
                lblCostCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Name"))
                TxtProfitCenterCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Code"))
                lblProfitCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Name"))
                TxtCostCenterCode.Enabled = False
                FndLineNo.Enabled = False
            Else
                FndLineNo.Value = ""
                TxtCostCenterCode.Value = ""
                lblCostCenterName.Text = ""
                TxtProfitCenterCode.Value = ""
                lblProfitCenterName.Text = ""
                TxtCostCenterCode.Enabled = True
                FndLineNo.Enabled = True
            End If
        End If
        ''----------------
        gv.Rows.Clear()
        gv.Rows.AddNew()
        gvraw.Rows.Clear()
        gvraw.Rows.AddNew()
        If clsCommon.myLen(txtplancode.Value) > 0 Then
            FillPlanItemsInGrid()
        End If
    End Sub

#Region "Common Function"
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

    Private Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        values = clsItemMaster.ProductType(Product_type)

        Return values
    End Function
#End Region

    Private Sub FillPlanItemsInGrid()
        isInsideLoadData = True
        Dim qry As String = "select TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code,TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code,plan1.qty as Final_Qty from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join TSPL_PP_PRODUCTION_PLAN_DETAIL on TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code"
        qry += " left outer join (select a.plan_code,a.Item_Code,a.qty from (select aax.plan_code,aax.item_code,sum(isnull(aax.final_qty,0)) as qty from (select finl.plan_code,finl.item_code,(case when len(isnull(finl.bo_unit,''))>0 then (finl.final_qty*tspl_item_uom_detail.conversion_factor/itemuom.conversion_factor) else finl.final_qty end) as final_Qty from (select finl1.Plan_Code,finl1.Item_Code,max(finl1.Unit_Code) as unit_code,max(finl1.bo_unit) as bo_unit,sum(finl1.Final_Qty) as final_qty from (select Plan_Code,Item_Code,unit_code,'' as bo_unit,(case when isnull(final_qty,0)>0 then final_qty else plan_qty end ) as final_qty from TSPL_PP_PRODUCTION_PLAN_DETAIL union all select TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,'' as unit_code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.unit_code as bo_unit,sum(0-isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.quantity,0)) as final_qty from TSPL_PP_BATCH_ORDER_BOM_DETAIL where isnull(TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,'')<>'' and TSPL_PP_BATCH_ORDER_BOM_DETAIL.Batch_Code not in ('" + txtCode.Value + "') group by TSPL_PP_BATCH_ORDER_BOM_DETAIL.Plan_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_BOM_DETAIL.unit_code )finl1 group by finl1.Plan_Code,finl1.Item_Code   )finl left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=finl.item_code and tspl_item_uom_detail.uom_code=finl.unit_code left outer join tspl_item_uom_detail itemuom on itemuom.item_code=finl.item_code and itemuom.uom_code=finl.bo_unit  )aax group by aax.Plan_Code,aax.Item_Code)a where a.qty>0)plan1"
        qry += " on plan1.plan_code=TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code and plan1.item_code=TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code"
        qry += " where TSPL_ITEM_MASTER.Structure_Code='" + txticategory_code.Value + "' and TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code='" + txtplancode.Value + "' " 'isnull(TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post,'0')='1' and 
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.myCdbl(dr("final_qty")) <= 0 Then
                    Continue For
                End If
                gv.Rows(gv.Rows.Count - 1).Cells(collineno).Value = CInt(gv.Rows.Count)
                gv.Rows(gv.Rows.Count - 1).Cells(colPlanCode).Value = txtplancode.Value
                gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("item_code"))
                gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = clsCommon.myCstr(dr("item_desc"))
                gv.Rows(gv.Rows.Count - 1).Cells(colItype).Value = ItemType(clsCommon.myCstr(dr("item_type")))
                gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("unit_code"))
                gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = Math.Round(clsCommon.myCdbl(dr("final_qty")), DecimalPoint)
                gv.Rows(gv.Rows.Count - 1).Cells(colBOMFatPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + clsCommon.myCstr(dr("item_code")) + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
                gv.Rows(gv.Rows.Count - 1).Cells(colBOMSNFPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + clsCommon.myCstr(dr("item_code")) + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
                gv.Rows(gv.Rows.Count - 1).Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colBOMFatPers).Value), Nothing)
                gv.Rows(gv.Rows.Count - 1).Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colBOMSNFPers).Value), Nothing)
                gv.Rows(gv.Rows.Count - 1).Cells(colSection).Value = txtSectionCode.Text
                gv.Rows(gv.Rows.Count - 1).Cells(colSectionName).Value = txtSectionName.Text
                gv.Rows.AddNew()
            Next
        Else
            clsCommon.MyMessageBoxShow(Me, "No item found for selected Plan and Production Structure.", Me.Text)
            txtplancode.Value = ""
        End If
        isInsideLoadData = False

        dt = Nothing
    End Sub

#Region "GV Cell Event"

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If clsCommon.myLen(txtCode.Value) > 0 AndAlso (btnpost.Enabled Or btnsave.Text = "Save") Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                AllBomCode = AllBomCode.Replace(clsCommon.myCstr(gv.CurrentRow.Cells(colBOM).Value), "")
                Dim qry As String = " delete from TSPL_PP_BATCH_ORDER_BOM_DETAIL where batch_code='" + clsCommon.myCstr(txtCode.Value) + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Else
            e.Cancel = False
            'clsCommon.MyMessageBoxShow("No row deleted", Me.Text)
        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv.Columns(colBOM) Then
                        isCellValueChanged = True
                        OpenBOMCode(False)
                        FillRawItemGridFromBOM()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colitemcode) Then
                        isCellValueChanged = True
                        OpenBOMICode(False)
                        FillRawItemGridFromBOM()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colUnit) Then
                        isCellValueChanged = True
                        OpenUOM(False)
                        FillRawItemGridFromBOM()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colShiftcode) Then
                        isCellValueChanged = True
                        OpenShiftCode(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colQty) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value) / 100), DecimalPoint)
                        gv.CurrentRow.Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value) / 100), DecimalPoint)
                        FillRawItemGridFromBOM()
                        isCellValueChanged = False
                    End If

                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        Dim uom As String = clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value)
        Dim icode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        uom = clsCommon.myCstr(clsCommon.ShowSelectForm("PPBUOM", qry, "Code", " tspl_item_uom_detail.item_code='" + icode + "'", uom, "Code", isButtonClicked))
        gv.CurrentRow.Cells(colUnit).Value = uom

        gv.CurrentRow.Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value), Nothing)
        gv.CurrentRow.Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value), Nothing)
    End Sub

    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvraw.CurrentRow.Cells(colRawQty).Value)
            fat = clsCommon.myCdbl(gvraw.CurrentRow.Cells(colFat).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value), clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawUnit).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gvraw.CurrentRow.Cells(colFAT_KG).Value = fat_kg
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Cal_SNF()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvraw.CurrentRow.Cells(colRawQty).Value)
            fat = clsCommon.myCdbl(gvraw.CurrentRow.Cells(colSNF).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value), clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawUnit).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gvraw.CurrentRow.Cells(colSNF_KG).Value = fat_kg
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenBOMCode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""
        icode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
        Dim sectionCondition As String = ""

        If clsCommon.myLen(txtSectionCode.Text) > 0 Then
            sectionCondition = " and tspl_pp_bom_head.section_code='" + txtSectionCode.Text + "' "
        End If

        If clsCommon.myLen(icode) > 0 Then
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + icode + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txticategory_code.Value + "' and tspl_item_master.Structure_Code='" + txticategory_code.Value + "' and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        Else
            whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and tspl_item_master.Structure_Code='" + txticategory_code.Value + "' and TSPL_PP_BOM_HEAD.item_category_code='" + txticategory_code.Value + "' and tspl_item_master.item_type in ('F','S') and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        End If

        Dim oldbomcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colBOM).Value)
        bomcode = clsBOM.GetBOMFinderWithValidityCheck(whrCls, oldbomcode, dtpDate.Value, isButtonClicked)

        If clsCommon.myLen(bomcode) > 0 Then
            gv.CurrentRow.Cells(colBOM).Value = bomcode
            gv.CurrentRow.Cells(colBOMRevisionNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Revision_No from TSPL_PP_BOM_HEAD where BOM_CODE='" + bomcode + "'"))
            gv.CurrentRow.Cells(colBOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            If clsCommon.myLen(txtplancode.Value) > 0 AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colUnit).Value) > 0 Then
            Else
                gv.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            End If

            If clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) <= 0 Then
                gv.CurrentRow.Cells(colQty).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select prod_quantity from tspl_pp_bom_head where bom_code='" + bomcode + "'")), DecimalPoint)
            End If
            If clsCommon.myLen(icode) <= 0 Then '----------when no item fill in grid from planning then item detail fill from bom else item detail filled by planned remains same
                gv.CurrentRow.Cells(colitemcode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
                gv.CurrentRow.Cells(colIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"))
                gv.CurrentRow.Cells(colItype).Value = ItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'")))
                gv.CurrentRow.Cells(colBOMFatPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gv.CurrentRow.Cells(colitemcode).Value + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
                gv.CurrentRow.Cells(colBOMSNFPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gv.CurrentRow.Cells(colitemcode).Value + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
                gv.CurrentRow.Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value) / 100), DecimalPoint)
                gv.CurrentRow.Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value) / 100), DecimalPoint)
            Else
                gv.CurrentRow.Cells(colBOMFatPers).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))
                gv.CurrentRow.Cells(colBOMSNFPers).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))
            End If
            gv.CurrentRow.Cells(colSection).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select section_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            gv.CurrentRow.Cells(colSectionName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colSection).Value) + "'"))

            AllBomCode = AllBomCode + "','" + bomcode
        Else
            gv.CurrentRow.Cells(colBOM).Value = ""
            gv.CurrentRow.Cells(colBOMRevisionNo).Value = ""
            gv.CurrentRow.Cells(colBOMDesc).Value = ""
            If clsCommon.myLen(icode) <= 0 Then
                gv.CurrentRow.Cells(colitemcode).Value = ""
                gv.CurrentRow.Cells(colIname).Value = ""
                gv.CurrentRow.Cells(colItype).Value = ""
                gv.CurrentRow.Cells(colUnit).Value = ""
                gv.CurrentRow.Cells(colQty).Value = 0
            End If
            gv.CurrentRow.Cells(colSection).Value = ""
            gv.CurrentRow.Cells(colSectionName).Value = ""

            AllBomCode = AllBomCode.Replace(oldbomcode, "")
        End If
        '' Done by panch raj on 10-jul-2018 against ticket no -KDI/09/07/18-000393
        txticategory_code.Enabled = False
    End Sub

    Private Sub FillRawItemGridFromBOM()
        Dim bomcode As String = ""
        isInsideLoadData = True
        AllBomCode = ""


        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BO_TEMP'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            qry = "create table BO_TEMP (BOM_Code varchar(30),item_code varchar(50),UOM varchar(12) null,Qty float)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        ElseIf check > 0 Then
            qry = "drop table BO_TEMP"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "create table BO_TEMP (BOM_Code varchar(30),item_code varchar(50),UOM varchar(12) null,Qty float)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If

        For Each grow As GridViewRowInfo In gv.Rows
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "BOM_Code", clsCommon.myCstr(grow.Cells(colBOM).Value))
            clsCommon.AddColumnsForChange(coll, "item_code", clsCommon.myCstr(grow.Cells(colitemcode).Value))
            clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCdbl(grow.Cells(colQty).Value))
            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(grow.Cells(colUnit).Value))

            If clsCommon.myLen(grow.Cells(colBOM).Value) > 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "BO_TEMP", OMInsertOrUpdate.Insert, "", Nothing)
                AllBomCode = AllBomCode + "','" + clsCommon.myCstr(grow.Cells(colBOM).Value)
            End If
           
        Next

        'qry = "select TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type,sum(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY) as QUANTITY,round(sum(TSPL_PP_BOM_ITEM_DETAIL.FAT)/count(TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE),2) as fat,round(sum(TSPL_PP_BOM_ITEM_DETAIL.SNF)/count(TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE),2) as snf,sum(TSPL_PP_BOM_ITEM_DETAIL.fat_kg) as fat_kg,sum(TSPL_PP_BOM_ITEM_DETAIL.snf_kg) as snf_kg from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE left outer join tspl_pp_bom_head on tspl_pp_bom_head.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code where TSPL_PP_BOM_ITEM_DETAIL.bom_code in ('" + AllBomCode + "') group by TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date" 'tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,
        If clsCommon.myLen(AllBomCode) > 0 Then
            qry = "select axx.deactive,axx.effective_date,axx.ITEM_CODE,axx.item_desc,axx.Item_Type,axx.unit_code,axx.Product_Type,sum(axx.Final_Qty) as QUANTITY,round(sum(axx.fat)/count(axx.ITEM_CODE),2) as fat,round(sum(axx.SNF)/count(axx.ITEM_CODE),2) as snf,sum(axx.fat_kg) as fat_kg,sum(axx.snf_kg) as snf_kg from ("
            qry += "select ax.bom_code,ax.prod_item_code,ax.deactive,ax.effective_date,ax.ITEM_CODE,ax.item_desc,ax.Item_Type,ax.unit_code,ax.Product_Type,ax.quantity,ax.fat,ax.snf,ax.fat_kg,ax.snf_kg,ax.prod_qty,(ax.prod_qty * (ax.quantity/ax.build_qty)) as Final_Qty from ("
            qry += "select (BO_TEMP.qty *finalcnvrsn.Conversion_Factor/ tspl_item_uom_detail.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type,(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY,(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf,(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg,(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE left outer join tspl_pp_bom_head on tspl_pp_bom_head.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code "
            qry += "left outer join BO_TEMP on BO_TEMP.bom_code=tspl_pp_bom_head.bom_code and BO_TEMP.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code and BO_TEMP.item_code=tspl_pp_bom_head.prod_item_code "
            qry += "left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_pp_bom_head.prod_item_code and tspl_item_uom_detail.uom_code=tspl_pp_bom_head.prod_item_unit_code "
            qry += "left outer join tspl_item_uom_detail finalcnvrsn on finalcnvrsn.item_code=tspl_pp_bom_head.prod_item_code and finalcnvrsn.uom_code=bo_temp.uom "
            qry += " where TSPL_PP_BOM_ITEM_DETAIL.bom_code in ('" + AllBomCode + "') "
            qry += ")ax)axx group by axx.deactive,axx.effective_date,axx.ITEM_CODE,axx.item_desc,axx.Item_Type,axx.unit_code,axx.Product_Type"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            gvraw.Rows.Clear()

            Dim deactive As Integer = 0
            Dim effectivedate As Date = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    deactive = CInt(dr("deactive"))
                    effectivedate = clsCommon.myCDate(dr("effective_date"))
                    If deactive >= 1 AndAlso clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy") > clsCommon.GetPrintDate(effectivedate, "dd/MMM/yyyy") Then
                        Continue For
                    End If

                    gvraw.Rows.AddNew()
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawsno).Value = CInt(gvraw.Rows.Count)
                    'gvraw.Rows(gvraw.Rows.Count - 1).Cells(colrawbomcode).Value = clsCommon.myCstr(dr("bom_code"))
                    'gvraw.Rows(gvraw.Rows.Count - 1).Cells(colrawprodtitem).Value = clsCommon.myCstr(dr("PROD_ITEM_CODE"))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIcode).Value = clsCommon.myCstr(dr("ITEM_CODE"))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIname).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawItype).Value = ItemType(clsCommon.myCstr(dr("Item_Type")))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawUnit).Value = clsCommon.myCstr(dr("UNIT_CODE"))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawProductType).Value = ProductType(clsCommon.myCstr(dr("Product_Type")))
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawQty).Value = Math.Round(clsCommon.myCdbl(dr("QUANTITY")), DecimalPoint)
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).Value = Math.Round(clsCommon.myCdbl(dr("FAT")), 2)
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).Value = Math.Round(clsCommon.myCdbl(dr("SNF")), 2)
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIcode).Value), clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawUnit).Value), clsCommon.myCdbl(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawQty).Value), clsCommon.myCdbl(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).Value), Nothing) ' Math.Round(IIf(clsCommon.myCdbl(dr("FAT")) > 0, clsCommon.myCdbl(dr("QUANTITY")) * clsCommon.myCdbl(dr("FAT")) / 100, 0), DecimalPoint)
                    gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawIcode).Value), clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawUnit).Value), clsCommon.myCdbl(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawQty).Value), clsCommon.myCdbl(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).Value), Nothing) ' Math.Round(IIf(clsCommon.myCdbl(dr("SNF")) > 0, clsCommon.myCdbl(dr("QUANTITY")) * clsCommon.myCdbl(dr("SNF")) / 100, 0), DecimalPoint)
                    If clsCommon.CompairString(clsCommon.myCstr(gvraw.Rows(gvraw.Rows.Count - 1).Cells(colRawProductType).Value), "Milk") = CompairStringResult.Equal Then
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).ReadOnly = False
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).ReadOnly = False
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = False
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = False
                    Else
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFat).ReadOnly = True
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = True
                        gvraw.Rows(gvraw.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = True
                    End If
                Next
            End If
        End If

        Dim qry1 As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='BO_TEMP'"
        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)

        If check1 > 0 Then
            qry1 = "drop table BO_TEMP"
            clsDBFuncationality.ExecuteNonQuery(qry1)
        End If
        isInsideLoadData = False

        dt = Nothing
    End Sub

    Sub OpenBOMICode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""

        bomcode = clsCommon.myCstr(gv.CurrentRow.Cells(colBOM).Value)

        If clsCommon.myLen(bomcode) > 0 Then
            Dim qry As String = "select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"
            whrCls = " tspl_item_master.item_code='" + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)) + "' and tspl_item_master.Structure_Code='" + txticategory_code.Value + "' and tspl_item_master.Active='1' "
        Else
            whrCls = " tspl_item_master.item_type in ('F','S') and tspl_item_master.Structure_Code='" + txticategory_code.Value + "' and tspl_item_master.Active='1' "
        End If

        icode = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), isButtonClicked)

        If clsCommon.myLen(icode) > 0 Then
            gv.CurrentRow.Cells(colitemcode).Value = icode
            gv.CurrentRow.Cells(colIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
            gv.CurrentRow.Cells(colItype).Value = ItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + icode + "'")))
            If clsCommon.myLen(txtplancode.Value) > 0 AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colUnit).Value) > 0 Then
            Else
                gv.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            End If

            gv.CurrentRow.Cells(colBOMFatPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
            gv.CurrentRow.Cells(colBOMSNFPers).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
            gv.CurrentRow.Cells(colBOMFatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMFatPers).Value) / 100), DecimalPoint)
            gv.CurrentRow.Cells(colBOMSNFkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value), Nothing) 'Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * (clsCommon.myCdbl(gv.CurrentRow.Cells(colBOMSNFPers).Value) / 100), DecimalPoint)
            If clsCommon.myLen(gv.CurrentRow.Cells(colSection).Value) <= 0 Then
                gv.CurrentRow.Cells(colSection).Value = txtSectionCode.Text
                gv.CurrentRow.Cells(colSectionName).Value = txtSectionName.Text
            End If
            
        Else
            gv.CurrentRow.Cells(colitemcode).Value = ""
            gv.CurrentRow.Cells(colIname).Value = ""
            gv.CurrentRow.Cells(colItype).Value = ""
            gv.CurrentRow.Cells(colUnit).Value = ""
            gv.CurrentRow.Cells(colBOMFatPers).Value = Nothing
            gv.CurrentRow.Cells(colBOMFatkg).Value = Nothing
            gv.CurrentRow.Cells(colBOMSNFPers).Value = Nothing
            gv.CurrentRow.Cells(colBOMSNFkg).Value = Nothing
            gv.CurrentRow.Cells(colSection).Value = ""
            gv.CurrentRow.Cells(colSectionName).Value = ""
        End If
    End Sub

    Sub OpenShiftCode(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select shift_code as Code,shift_name as Description,from_time as [From Time],to_time as [To Time],interval_time as [Interval Time],fsthalf_adjust_min as [First Half Adjustment],sechalf_adjust_min as [Second Half Adjustment] from tspl_shift_master"
        Dim shiftcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colShiftcode).Value)
        shiftcode = clsCommon.ShowSelectForm("PPSFTFND", qry, "Code", "", shiftcode, "Code", isButtonClicked)

        If shiftcode IsNot Nothing AndAlso clsCommon.myLen(shiftcode) > 0 Then
            gv.CurrentRow.Cells(colShiftcode).Value = shiftcode
            gv.CurrentRow.Cells(colShiftDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name from tspl_shift_master where shift_code='" + shiftcode + "'"))
        Else
            gv.CurrentRow.Cells(colShiftcode).Value = ""
            gv.CurrentRow.Cells(colShiftDesc).Value = ""
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(collineno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub
#End Region

#Region "GVRAW Grid Event"

    Private Sub gvraw_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvraw.CellFormatting
        If e.Column Is gvraw.Columns(colBatchstatus) Then
            Dim qry As String = "select isnull(is_post,0) as ispost from TSPL_PP_BATCH_ORDER_HEAD where Main_Batch_Code='" + txtCode.Value + "' and batch_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colSubBatchcode).Value) + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                gvraw.CurrentRow.Cells(colBatchstatus).ReadOnly = True
            End If
        End If
    End Sub

    Private Sub gvraw_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvraw.KeyDown
        Dim Xr As Integer = gvraw.CurrentRow.Index
        Try
            If e.KeyData = Keys.Enter Or IsNumeric(e.KeyData) Or (e.KeyData = Keys.Right AndAlso gvraw.CurrentCell.ColumnIndex = gvraw.Columns.Count - 1) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawProductType).Value), "Milk") = CompairStringResult.Equal Then
                    gvraw.Rows(Xr).Cells(colFat).ReadOnly = False
                    gvraw.Rows(Xr).Cells(colSNF).ReadOnly = False
                    gvraw.Rows(Xr).Cells(colFat).Value = 0
                    gvraw.Rows(Xr).Cells(colSNF).Value = 0
                    gvraw.Rows(Xr).Cells(colFAT_KG).ReadOnly = False
                    gvraw.Rows(Xr).Cells(colSNF_KG).ReadOnly = False
                    gvraw.Rows(Xr).Cells(colFAT_KG).Value = 0
                    gvraw.Rows(Xr).Cells(colSNF_KG).Value = 0
                Else
                    gvraw.Rows(Xr).Cells(colFat).ReadOnly = True
                    gvraw.Rows(Xr).Cells(colSNF).ReadOnly = True
                    gvraw.Rows(Xr).Cells(colFat).Value = 0
                    gvraw.Rows(Xr).Cells(colSNF).Value = 0
                    gvraw.Rows(Xr).Cells(colFAT_KG).ReadOnly = True
                    gvraw.Rows(Xr).Cells(colSNF_KG).ReadOnly = True
                    gvraw.Rows(Xr).Cells(colFAT_KG).Value = 0
                    gvraw.Rows(Xr).Cells(colSNF_KG).Value = 0
                End If
            End If
        Catch ex As Exception
            gvraw.Rows(Xr).Cells(colFat).ReadOnly = True
            gvraw.Rows(Xr).Cells(colSNF).ReadOnly = True
            gvraw.Rows(Xr).Cells(colFat).Value = 0
            gvraw.Rows(Xr).Cells(colSNF).Value = 0
            gvraw.Rows(Xr).Cells(colFAT_KG).ReadOnly = True
            gvraw.Rows(Xr).Cells(colSNF_KG).ReadOnly = True
            gvraw.Rows(Xr).Cells(colFAT_KG).Value = 0
            gvraw.Rows(Xr).Cells(colSNF_KG).Value = 0
        End Try
    End Sub
    Private Sub gvraw_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvraw.UserDeletingRow
        If clsCommon.myLen(txtCode.Value) > 0 AndAlso btnpost.Enabled Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = "select isnull(is_post,0) as ispost from TSPL_PP_BATCH_ORDER_HEAD where Main_Batch_Code='" + txtCode.Value + "' and batch_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colSubBatchcode).Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If check <= 0 Then '==============if SFG BO is not posted only then deleted
                    qry = " delete from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL where batch_code='" + clsCommon.myCstr(txtCode.Value) + "' and item_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                Else
                    e.Cancel = True
                    clsCommon.MyMessageBoxShow(Me, "No row deleted,SFG BO is posted", Me.Text)
                    Exit Sub
                End If
            End If
        Else
                e.Cancel = True
            clsCommon.MyMessageBoxShow(Me, "No row deleted,data is posted", Me.Text)
        End If
    End Sub

    Private Sub gvraw_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvraw.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gvraw.Columns(colRawIcode) Then
                        isCellValueChanged = True
                        OpenRawIcode(False)
                        isCellValueChanged = False
                    End If
                    If e.Column Is gvraw.Columns(colrawbomcode) Then
                        Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_QUANTITY as [Build Qty] from TSPL_PP_BOM_HEAD  "
                        Dim WhrClause As String = ""
                        WhrClause = " PROD_ITEM_CODE = '" & gvraw.CurrentRow.Cells(colRawIcode).Value & "' AND Is_POST=1"
                        gvraw.CurrentRow.Cells(colrawbomcode).Value = clsCommon.ShowSelectForm("TSPL_PP_BOM_HEAD", qry, "Code", WhrClause, gvraw.CurrentRow.Cells(colrawbomcode).Value, "BOM_CODE", False)
                    End If
                    
                    If (e.Column Is gvraw.Columns(colBatchstatus) Or e.Column Is gvraw.Columns(colSubBatchcode)) Then
                        isCellValueChanged = True
                        'CheckSubBatchStatus()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gvraw.Columns(colRawQty)) Or (e.Column Is gvraw.Columns(colFat)) Or (e.Column Is gvraw.Columns(colFAT_KG)) Then
                        isCellValueChanged = True
                        Cal_FAT()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gvraw.Columns(colRawQty)) Or (e.Column Is gvraw.Columns(colSNF)) Or (e.Column Is gvraw.Columns(colSNF_KG)) Then
                        isCellValueChanged = True
                        Cal_SNF()
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CheckSubBatchStatus()
        Dim rawstatus As String = ""
        rawstatus = clsCommon.myCstr(gvraw.CurrentRow.Cells(colBatchstatus).Value)

        If clsCommon.CompairString(rawstatus, "No") = CompairStringResult.Equal Then
            Exit Sub
        End If

        '-------------------check the semi finished good as main item in batch order,if record exist and approved not closed then 
        '------------------------------------------decision is on user hand whether to make new batch or not
        If clsCommon.CompairString(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawItype).Value), "Semi Finished Good") = CompairStringResult.Equal Then
            '-----------------------taking stock qty of closed batch qty---------------------------------------
            Dim qry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawProductType).Value), "Milk") <> CompairStringResult.Equal Then
                qry = "select (SUM(qty)*Conversion_Factor/finalcnvrt) as qty from (select TSPL_INVENTORY_MOVEMENT.Item_Code,Location_Code,UOM,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finalcnvrsn.Conversion_Factor as finalcnvrt,(case when InOut='I' then Qty else case when inout='O' then (0-Qty) end end) as qty from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT.UOM left outer join TSPL_ITEM_UOM_DETAIL as finalcnvrsn on finalcnvrsn.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and finalcnvrsn.UOM_Code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawUnit).Value) + "')a where Item_Code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "' and Location_Code='" + clsCommon.myCstr(txtlocationcode.Value) + "' group by Conversion_Factor,finalcnvrt"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawProductType).Value), "Milk") = CompairStringResult.Equal Then
                qry = "select (SUM(qty)*Conversion_Factor/finalcnvrt) as qty from (select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,Location_Code,UOM,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,finalcnvrsn.Conversion_Factor as finalcnvrt,(case when InOut='I' then Qty else case when inout='O' then (0-Qty) end end) as qty from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT_NEW.UOM left outer join TSPL_ITEM_UOM_DETAIL as finalcnvrsn on finalcnvrsn.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and finalcnvrsn.UOM_Code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawUnit).Value) + "')a where Item_Code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "' and Location_Code='" + clsCommon.myCstr(txtlocationcode.Value) + "' group by Conversion_Factor,finalcnvrt"
            End If
            Dim qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            '----------------------------------------------------------------------------------

            qry = "select sum(isnull(quantity,0)) as qty from TSPL_PP_BATCH_ORDER_BOM_DETAIL where item_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "' and batch_code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where isnull(Is_Post,'0')='0' and isnull(Closed_YN,'0')='0')"
            Dim batch_qty As Decimal = 0
            batch_qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            'If existing open batch order qty is less then req. qty then it proceed and check for stock,if stock exist then t allow to create child BO
            If gvraw.CurrentRow.Cells(colRawQty).Value IsNot Nothing AndAlso clsCommon.myCdbl(gvraw.CurrentRow.Cells(colRawQty).Value) > batch_qty Then
                If qty > clsCommon.myCdbl(gvraw.CurrentRow.Cells(colRawQty).Value) Then '------------if stock exist then it prompt to user,to create child bo?
                    If Not clsCommon.MyMessageBoxShow("Avail. Stock Qty. " + clsCommon.myCstr(qty) + ",Do you proceed with new batch order?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        gvraw.CurrentRow.Cells(colBatchstatus).Value = "No"
                        Return
                    End If
                Else
                    If Not clsCommon.MyMessageBoxShow("Do you proceed with new batch order?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        gvraw.CurrentRow.Cells(colBatchstatus).Value = "No"
                        Return
                    End If

                End If
            Else
                gvraw.CurrentRow.Cells(colBatchstatus).Value = "No"
            End If

            qry = "select count(*) from tspl_pp_bom_head where prod_item_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "' and isnull(is_post,'0')='1' and ('" + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy") + "' between valid_from_date and valid_upto_date)"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                clsCommon.MyMessageBoxShow("No BOM exist for selected SFG item," + Environment.NewLine + "Create BOM first.", Me.Text)
                gvraw.CurrentRow.Cells(colBatchstatus).Value = "No"
                Return
            End If

            gvraw.CurrentRow.Cells(colSubBatchcode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Batch_Code from TSPL_PP_BATCH_ORDER_BOM_DETAIL where item_code='" + clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value) + "' and batch_code in (select batch_code from TSPL_PP_BATCH_ORDER_HEAD where isnull(Is_Post,'0')='1' and isnull(Closed_YN,'0')='0')"))
            'End If
        Else
            gvraw.CurrentRow.Cells(colBatchstatus).Value = "No"
        End If
    End Sub

    Private Sub OpenRawIcode(ByVal isButtonCLicked As Boolean)
        Dim icode As String = ""

        If clsCommon.myLen(AllBomCode) > 0 AndAlso clsCommon.myLen(gvraw.Rows(0).Cells(colRawIname).Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Click Fill Raw-Item Detail Button First", Me.Text)
            btngo.Focus()
            btngo.Select()
            Errorcontrol.SetError(btngo, "Click Fill Raw-Item Detail Button First")
            Return
        Else
            Errorcontrol.ResetError(btngo)
        End If

        icode = clsItemMaster.getFinder(" tspl_item_master.Active='1'", clsCommon.myCstr(gvraw.CurrentRow.Cells(colRawIcode).Value), isButtonCLicked) 'tspl_item_master.item_type not in ('F') and 

        If clsCommon.myLen(icode) > 0 Then
            gvraw.CurrentRow.Cells(colRawIcode).Value = icode
            gvraw.CurrentRow.Cells(colRawIname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
            gvraw.CurrentRow.Cells(colRawItype).Value = ItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + icode + "'")))
            gvraw.CurrentRow.Cells(colRawUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + icode + "'"))
            gvraw.CurrentRow.Cells(colRawProductType).Value = ProductType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Product_Type from tspl_item_master where item_code='" + icode + "'")))
            If clsCommon.CompairString(gvraw.CurrentRow.Cells(colRawProductType).Value, "Milk") = CompairStringResult.Equal Then
                gvraw.CurrentRow.Cells(colFat).ReadOnly = False
                gvraw.CurrentRow.Cells(colSNF).ReadOnly = False
                gvraw.CurrentRow.Cells(colFAT_KG).ReadOnly = False
                gvraw.CurrentRow.Cells(colSNF_KG).ReadOnly = False
            ElseIf clsCommon.CompairString(gvraw.CurrentRow.Cells(colRawProductType).Value, "Milk") <> CompairStringResult.Equal Then
                gvraw.CurrentRow.Cells(colFat).ReadOnly = True
                gvraw.CurrentRow.Cells(colSNF).ReadOnly = True
                gvraw.CurrentRow.Cells(colFAT_KG).ReadOnly = True
                gvraw.CurrentRow.Cells(colSNF_KG).ReadOnly = True
            End If
        Else
            gvraw.CurrentRow.Cells(colRawIcode).Value = ""
            gvraw.CurrentRow.Cells(colRawIname).Value = ""
            gvraw.CurrentRow.Cells(colRawItype).Value = ""
            gvraw.CurrentRow.Cells(colRawUnit).Value = ""
            gvraw.CurrentRow.Cells(colRawProductType).Value = ""
            gvraw.CurrentRow.Cells(colFat).ReadOnly = True
            gvraw.CurrentRow.Cells(colSNF).ReadOnly = True
            gvraw.CurrentRow.Cells(colFat).Value = 0
            gvraw.CurrentRow.Cells(colSNF).Value = 0
            gvraw.CurrentRow.Cells(colFAT_KG).ReadOnly = True
            gvraw.CurrentRow.Cells(colSNF_KG).ReadOnly = True
            gvraw.CurrentRow.Cells(colFAT_KG).Value = 0
            gvraw.CurrentRow.Cells(colSNF_KG).Value = 0
            gvraw.CurrentRow.Cells(colRawQty).Value = 0
            gvraw.CurrentRow.Cells(colrem).Value = ""
        End If
    End Sub

    Private Sub gvraw_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvraw.CurrentColumnChanged
        If gvraw.RowCount > 0 Then
            Dim intCurrRow As Integer = gvraw.CurrentRow.Index
            gvraw.CurrentRow.Cells(collineno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvraw.Rows.Count - 1 Then
                gvraw.Rows.AddNew()
                gvraw.CurrentRow = gvraw.Rows(intCurrRow)
            End If
        End If
    End Sub
#End Region

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txticategory_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txticategory_code._MYValidating
        Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as Length from TSPL_STRUCTURE_MASTER"
        txticategory_code.Value = clsCommon.ShowSelectForm("CATFND", qry, "Code", " ", txticategory_code.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txticategory_code.Value) > 0 Then
            txtitemcategory_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txticategory_code.Value + "'"))
        Else
            txticategory_code.Value = ""
            txtitemcategory_name.Text = ""
            txtplancode.Value = ""
            gv.Rows.Clear()
            gv.Rows.AddNew()
            gvraw.Rows.Clear()
            gvraw.Rows.AddNew()
            txtmain_batch.Text = ""
            txtsub_batch.Text = ""
        End If
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        FillRawItemGridFromBOM()
    End Sub

    Private Sub chkclose_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkclose.CheckedChanged
        If isInsideLoadData = True Then
            Exit Sub
        End If

        If clsCommon.myLen(txtCode) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select batch order for closing", Me.Text)
            txtCode.Focus()
            txtCode.Select()
            Errorcontrol.SetError(txtCode, "Select batch order for closing")
            Return
        Else
            Errorcontrol.ResetError(txtCode)
        End If

        Dim qry As String = "select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            Return
        End If

        '------------------check that if batch order is used in issue entry andalso post then,Batch Order is closed permanently
        'qry = "select count(*) from TSPL_PP_ISSUE_HEAD where batch_code='" + txtCode.Value + "' and isnull(is_post,'0')='1'"
        'check = clsDBFuncationality.getSingleValue(qry)
        'If check > 0 Then
        '    clsCommon.MyMessageBoxShow("Batch Order close status cannot be changed from here," + Environment.NewLine + "Because record is used in Issue Entry and posted.", Me.Text)
        '    isInsideLoadData = True
        '    chkclose.Checked = True
        '    isInsideLoadData = False
        '    Return
        'End If
        '----------------------------------------------------------------------------------------

        chkclosestatus = False
        Try
            If chkclose.Checked AndAlso isInsideLoadData = False Then
                If Not clsCommon.MyMessageBoxShow("Are you sure want to close batch order of code " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                chkclosestatus = True

                If clsProcessBatchOrder.CloseData(txtCode.Value, chkclose.Checked) Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    btndelete.Enabled = False
                End If
            ElseIf Not chkclose.Checked AndAlso isInsideLoadData = False Then
                If Not clsCommon.MyMessageBoxShow("Are you sure want to open batch order of code " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                chkclosestatus = True

                If clsProcessBatchOrder.CloseData(txtCode.Value, chkclose.Checked) Then
                    btnsave.Enabled = True
                    btnpost.Enabled = True
                    btndelete.Enabled = True

                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
            chkclosestatus = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboBOMStatus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBOMStatus.TextChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboBOMStatus.Text), "Approved") = CompairStringResult.Equal AndAlso clsCommon.CompairString(btnsave.Text, "Save") <> CompairStringResult.Equal Then
            'btnsave.Enabled = False
        ElseIf (clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal AndAlso btnpost.Enabled) OrElse clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
            btnsave.Enabled = True
        End If

        If clsCommon.CompairString(cboBOMStatus.Text, "Open") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "On Hold") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboBOMStatus.Text, "In-Active") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Cancel
        End If
        If btnsave.Enabled = False AndAlso btnpost.Enabled = False AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
        End If
    End Sub

    Private Sub btnshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshow.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select BO first.")
                Throw New Exception("Select BO first.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If
            Dim MainBO As String = ""

            Dim qry As String = "select Batch_Code1,axa.prod_item_code,item.item_desc as prod_iname,axa.Item_Code,tspl_item_master.item_desc as Item_Name,axa.Unit_Code,Quantity,FAT_KG,SNF_KG from ( "
            qry += " select (case when isnull(TSPL_PP_BATCH_ORDER_HEAD.description,'')<>'' then TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code+' - '+TSPL_PP_BATCH_ORDER_HEAD.description else TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code end) as Batch_Code1,bom.prod_item_code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Unit_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.batch_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code left outer join "
            qry += " (select PROD_ITEM_CODE,ITEM_CODE from (select ROW_NUMBER() over (partition by TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE order by TSPL_PP_BOM_HEAD.bom_code) as sno,TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE from TSPL_PP_BOM_HEAD left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE where TSPL_PP_BOM_HEAD.BOM_CODE in (select BOM_CODE from TSPL_PP_BATCH_ORDER_BOM_DETAIL where Batch_Code in ('" + txtCode.Value + "')) )ch_bom where sno=1 )bom on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code=bom.ITEM_CODE"
            qry += " where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code in ('" + txtCode.Value + "') "

            MainBO = clsProcessBatchOrder.GetMainBO(txtCode.Value, Nothing)
            While (clsCommon.myLen(MainBO) > 0)
                qry += " union all "
                qry += " select (case when isnull(TSPL_PP_BATCH_ORDER_HEAD.description,'')<>'' then TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code+' - '+TSPL_PP_BATCH_ORDER_HEAD.description else TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code end) as Batch_Code1,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.prod_item_code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Unit_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.batch_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code in ('" + MainBO + "') "

                MainBO = clsProcessBatchOrder.GetMainBO(MainBO, Nothing)
            End While

            qry += " )axa left outer join tspl_item_master on axa.item_code=tspl_item_master.item_code left outer join tspl_item_master item on item.item_code=axa.prod_item_code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.AllowColumnReorder = True
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt

                '=====================================

                gv1.ReadOnly = True
                gv1.Columns("Batch_Code1").Width = 100
                gv1.Columns("Batch_Code1").IsVisible = True
                gv1.Columns("Batch_Code1").HeaderText = "Batch Code"
                gv1.Columns("Batch_Code1").FormatString = ""

                gv1.Columns("prod_item_code").Width = 100
                gv1.Columns("prod_item_code").IsVisible = True
                gv1.Columns("prod_item_code").HeaderText = "Main Item Code"
                gv1.Columns("prod_item_code").FormatString = ""

                gv1.Columns("prod_iname").Width = 100
                gv1.Columns("prod_iname").IsVisible = True
                gv1.Columns("prod_iname").HeaderText = "Main Item Name"
                gv1.Columns("prod_iname").FormatString = ""

                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").HeaderText = "Item Code"
                gv1.Columns("Item_Code").FormatString = ""

                gv1.Columns("Item_Name").Width = 220
                gv1.Columns("Item_Name").IsVisible = True
                gv1.Columns("Item_Name").HeaderText = "Description"
                gv1.Columns("Item_Name").FormatString = ""

                gv1.Columns("Unit_Code").Width = 100
                gv1.Columns("Unit_Code").IsVisible = True
                gv1.Columns("Unit_Code").HeaderText = "UOM"
                gv1.Columns("Unit_Code").FormatString = ""

                gv1.Columns("Quantity").Width = 100
                gv1.Columns("Quantity").IsVisible = True
                gv1.Columns("Quantity").HeaderText = "Quantity"
                gv1.Columns("Quantity").FormatString = ""

                gv1.Columns("FAT_KG").Width = 100
                gv1.Columns("FAT_KG").IsVisible = True
                gv1.Columns("FAT_KG").HeaderText = "FAT Kg"
                gv1.Columns("FAT_KG").FormatString = ""

                gv1.Columns("SNF_KG").Width = 100
                gv1.Columns("SNF_KG").IsVisible = True
                gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
                gv1.Columns("SNF_KG").FormatString = ""

                gv1.ShowGroupedColumns = False
                gv1.GroupDescriptors.Add(New GridGroupByExpression("Batch_Code1 as Batch_Code1  format ""{0}: {1}"" Group By Batch_Code1"))

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item22 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item22)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                gv1.AutoExpandGroups = True
            Else
                Throw New Exception("No data found.")
            End If

            btnExport.Visible = True
            RadPageViewPage4.Item.Visibility = ElementVisibility.Visible
            RadPageView1.SelectedPage = RadPageViewPage4

            dt = Nothing
        Catch ex As Exception
            btnExport.Visible = False
            RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If gv1.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Dated: " + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy"))
            clsCommon.MyExportToExcelGrid("BO Raw Item Detail", gv1, arrHeader, Me.Text)
        End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        If gv1.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Dated: " + clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy"))
            clsCommon.MyExportToPDF("BO Raw Item Detail", gv1, arrHeader, Me.Text)
        End If
    End Sub

    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            '======================================================================
            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsProcessBatchOrder.UnpostData(txtCode.Value) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Unpost and Recreated", Me.Text)
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''richa agarwal againt ticket no BHA/02/07/18-000120
    Private Sub FndLineNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLineNo._MYValidating
        Try
            Dim qry As String = "Select LINE_NO AS Code,MACHINE_NAME,MACHINE_RATED,CAPACITY ,TIME_FRAME  from TSPL_LINE_MASTER"
            FndLineNo.Value = clsCommon.ShowSelectForm("PPLineFND", qry, "Code", " ", FndLineNo.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Batch Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "batch_code", "TSPL_PP_BATCH_ORDER_HEAD", "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            clsProcessBatchOrder.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

End Class
