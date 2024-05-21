'---Richa Agarwal---Ticket No.-ERO/01/04/19-000537--08/04/2019,ERO/16/04/19-000560
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmSiloMilkTransfer_JobWork
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isFlag As Boolean = False

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Public CheckStockServerDate As Boolean = True
    Dim repoicodestatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colSiloCode As String = "colSiloCode"
    Const colSiloName As String = "colSiloName"
    Const colMainLocCode As String = "colMainLocCode"
    Const colMainLocName As String = "colMainLocName"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colRate As String = "colRate"
    Const colAmt As String = "colAmt"
    Const colFATPers As String = "FAT Pers"
    Const colFATKG As String = "FAT KG"
    Const colSNFPers As String = "SNFPers"
    Const colSNFKG As String = "SNF Kg"
    Const colFatAmt As String = "colFatAmt"
    Const colSNFAmt As String = "colSNFAmt"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String = ""
    Public Qry As String = String.Empty
    Dim AllowSiloMilkTransfertoMainLocation As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()

        LoadBlankGrid()
        AddNew()
        AllowSiloMilkTransfertoMainLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSiloMilkTransfertoMainLocation, clsFixedParameterCode.AllowSiloMilkTransfertoMainLocation, Nothing)) = 1, True, False))

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub SetLength()
        txtAdjustmentNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoMainLocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainLocCode.FormatString = ""
        repoMainLocCode.HeaderText = "Main Location"
        repoMainLocCode.Name = colMainLocCode
        repoMainLocCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoMainLocCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMainLocCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMainLocCode)

        Dim repoMainLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainLocName.FormatString = ""
        repoMainLocName.HeaderText = "Main Location Name"
        repoMainLocName.Name = colMainLocName
        repoMainLocName.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoMainLocName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMainLocName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoMainLocName)

        Dim repoSiloCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSiloCode.FormatString = ""
        repoSiloCode.HeaderText = "Silo"
        repoSiloCode.Name = colSiloCode
        repoSiloCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoSiloCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSiloCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSiloCode)

        Dim repoSiloName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSiloName.FormatString = ""
        repoSiloName.HeaderText = "Silo Name"
        repoSiloName.Name = colSiloName
        repoSiloName.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoSiloName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSiloName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoSiloName)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N2}"
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = "{0:N2}"
        repoRate.HeaderText = "TS Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.ShowUpDownButtons = False
        repoRate.Step = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)


        Dim repofatpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatpers.FormatString = "{0:N6}"
        repofatpers.HeaderText = "FAT%"
        repofatpers.Width = 70
        repofatpers.Minimum = 0
        repofatpers.DecimalPlaces = 2
        repofatpers.Name = colFATPers
        gv1.MasterTemplate.Columns.Add(repofatpers)

        Dim repofatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatkg.FormatString = ""
        repofatkg.HeaderText = "FAT KG"
        repofatkg.Width = 70
        repofatkg.DecimalPlaces = 2
        repofatkg.Name = colFATKG
        repofatkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatkg)

        Dim reposnfpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfpers.HeaderText = "SNF%"
        reposnfpers.Width = 70
        reposnfpers.DecimalPlaces = 2
        reposnfpers.Minimum = 0
        reposnfpers.Name = colSNFPers
        reposnfpers.FormatString = "{0:N6}"
        gv1.MasterTemplate.Columns.Add(reposnfpers)

        Dim reposnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfkg.FormatString = ""
        reposnfkg.HeaderText = "SNF KG"
        reposnfkg.Width = 70
        reposnfkg.DecimalPlaces = 2
        reposnfkg.Name = colSNFKG
        reposnfkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfkg)

        Dim repoFatAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatAmt.HeaderText = "Fat Amt"
        repoFatAmt.Width = 70
        repoFatAmt.DecimalPlaces = 2
        repoFatAmt.Minimum = 0
        repoFatAmt.Name = colFatAmt
        repoFatAmt.FormatString = "{0:N6}"
        gv1.MasterTemplate.Columns.Add(repoFatAmt)

        Dim repoSNFAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFAmt.FormatString = ""
        repoSNFAmt.HeaderText = "SNF Amt"
        repoSNFAmt.Width = 70
        repoSNFAmt.DecimalPlaces = 2
        repoSNFAmt.Name = colSNFAmt
        repoSNFAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSNFAmt)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = "{0:N2}"
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
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
        ' gv1.BestFitColumns = True

    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
    End Sub

    Sub BlankAllControls()
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
            CheckStockServerDate = True
        Else
            CheckStockServerDate = False
        End If
        txtSilo.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtAdjustmentNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDesc.Text = ""
        FndItemCode.Value = ""
        LblItemName.Text = ""
        FndMainLocation.Value = ""
        LblMainLocation.Text = ""
        fnditemuom.Value = ""
        chkJobWorkLocation.Checked = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colSiloCode) OrElse e.Column Is gv1.Columns(colMainLocCode) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSNFPers) OrElse e.Column Is gv1.Columns(colFATPers) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            gv1.CurrentRow.Cells(colQty).Value = LoadQty_Fat_Snf(False)
                            UpdateRateFromTankerDispatchPriceMaster(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colMainLocCode) Then
                            OpenMainLocList(False)
                        ElseIf e.Column Is gv1.Columns(colSiloCode) Then
                            OpenSiloList(False)
                            gv1.CurrentRow.Cells(colQty).Value = LoadQty_Fat_Snf(False)
                            'ElseIf e.Column Is gv1.Columns(colRate) Then
                            '    UpdateRateFromTankerDispatchPriceMaster(False)
                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colFATPers) OrElse e.Column Is gv1.Columns(colSNFPers) Then
                            UpdateFatSnfKG(False)
                        End If
                    End If
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    UpdateFatSnfPer(False)
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenMainLocList(ByVal isButtonClick As Boolean)
        Try
            Dim strwhrcls As String = String.Empty
            If chkJobWorkLocation.Checked = True Then
                strwhrcls += "  (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' ) and Location_code<>'" & clsCommon.myCstr(FndMainLocation.Value) & "' "
            End If
            gv1.CurrentRow.Cells(colMainLocCode).Value = clsLocation.getFinder(strwhrcls, gv1.CurrentRow.Cells(colMainLocCode).Value, isButtonClick)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colMainLocCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colMainLocName).Value = clsLocation.GetName(gv1.CurrentRow.Cells(colMainLocCode).Value, Nothing)
                If clsLocation.IsJobWorkLocation(gv1.CurrentRow.Cells(colMainLocCode).Value, Nothing) Then
                    gv1.CurrentRow.Cells(colSiloCode).Value = gv1.CurrentRow.Cells(colMainLocCode).Value
                    gv1.CurrentRow.Cells(colSiloName).Value = clsLocation.GetName(gv1.CurrentRow.Cells(colSiloCode).Value, Nothing)
                    gv1.CurrentRow.Cells(colSiloCode).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colSiloCode).ReadOnly = False
                End If
                txtSilo.Value = FndMainLocation.Value
            Else
                gv1.CurrentRow.Cells(colMainLocName).Value = ""
                gv1.CurrentRow.Cells(colSiloName).Value = ""
                gv1.CurrentRow.Cells(colSiloCode).ReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub OpenSiloList(ByVal isButtonClick As Boolean)
        Try
            ''GKD/03/10/18-000169 richa 
            If AllowSiloMilkTransfertoMainLocation = True Then
                If clsLocation.IsJobWorkLocation(gv1.CurrentRow.Cells(colMainLocCode).Value, Nothing) = False Then
                    Dim qryforsilo As String = "select * from (select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code  where  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + gv1.CurrentRow.Cells(colMainLocCode).Value + "' " & Environment.NewLine & _
                    "                union all " & Environment.NewLine & _
                    " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   where  (Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) and Location_Code ='" + gv1.CurrentRow.Cells(colMainLocCode).Value + "' )z"
                    gv1.CurrentRow.Cells(colSiloCode).Value = clsCommon.ShowSelectForm("LOCMSTFNDSilogrid", qryforsilo, "Code", "", gv1.CurrentRow.Cells(colSiloCode).Value, "Code", isButtonClick)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colSiloCode).Value) > 0 Then
                        gv1.CurrentRow.Cells(colSiloName).Value = clsLocation.GetName(gv1.CurrentRow.Cells(colSiloCode).Value, Nothing)
                    Else
                        gv1.CurrentRow.Cells(colSiloName).Value = ""
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub UpdateRateFromTankerDispatchPriceMaster(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells(colMainLocCode).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                Dim obj As clsTankerDispatchPriceMaster = Nothing
                obj = clsTankerDispatchPriceMaster.GetLastestPriceChart(gv1.CurrentRow.Cells(colMainLocCode).Value, gv1.CurrentRow.Cells(colICode).Value, txtDate.Value, Nothing)
                If obj IsNot Nothing Then
                    If obj.TOTAL_SOLID_RATE > 0 Then
                        gv1.CurrentRow.Cells(colRate).Value = obj.TOTAL_SOLID_RATE
                        gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * obj.TOTAL_SOLID_RATE
                    Else
                        gv1.CurrentRow.Cells(colRate).Value = 0
                        gv1.CurrentRow.Cells(colAmt).Value = 0
                        Throw New Exception(" Please create Price Chart First")
                    End If
                Else
                    gv1.CurrentRow.Cells(colRate).Value = 0
                    gv1.CurrentRow.Cells(colAmt).Value = 0
                    Throw New Exception(" Please create Price Chart First")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub UpdateFatSnfKG(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(FndMainLocation.Value) > 0 Then
                Dim dblQty As Double = clsItemMaster.GetKGConvQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value))
                If gv1.CurrentRow.Cells(colFATPers).Value > 0 Then
                    gv1.CurrentRow.Cells(colFATKG).Value = Math.Round(dblQty * gv1.CurrentRow.Cells(colFATPers).Value / 100, 2)
                End If
                If (gv1.CurrentRow.Cells(colSNFPers).Value) > 0 Then
                    gv1.CurrentRow.Cells(colSNFKG).Value = Math.Round(dblQty * gv1.CurrentRow.Cells(colSNFPers).Value / 100, 2)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub UpdateFatSnfPer(ByVal isButtonClick As Boolean)
        Try
            Dim dt As DataTable = Nothing
            Dim dblBalQty As Double = LoadQty_Fat_Snf(False)
            If clsCommon.myLen(FndMainLocation.Value) > 0 And ((clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) = dblBalQty) OrElse (gv1.CurrentRow.Cells(colFATPers).Value = 0 AndAlso gv1.CurrentRow.Cells(colSNFPers).Value = 0)) Then
              
                If AllowSiloMilkTransfertoMainLocation = True Then
                    Dim strSubLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select isnull(is_sub_location,'N') from TSPL_LOCATION_MASTER where Location_Code ='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colSiloCode).Value) & "'"))
                    If clsCommon.CompairString(strSubLocation, "N") = CompairStringResult.Equal Then        ''------------means location is main location
                        dt = clsInventoryMovementNew.getBalance_FatAndSnfKG(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colSiloCode).Value, "", txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value)
                    Else
                        dt = clsInventoryMovementNew.getBalance_FatAndSnfKG(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colMainLocCode).Value, gv1.CurrentRow.Cells(colSiloCode).Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value)
                    End If
                Else
                    dt = clsInventoryMovementNew.getBalance_FatAndSnfKG(gv1.CurrentRow.Cells(colICode).Value, FndMainLocation.Value, gv1.CurrentRow.Cells(colSiloCode).Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value)
                End If

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colFATKG).Value = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
                    gv1.CurrentRow.Cells(colSNFKG).Value = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                    Dim dblQty As Double = clsItemMaster.GetKGConvQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value))
                    If dblQty > 0 Then
                        gv1.CurrentRow.Cells(colFATPers).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("FAT_KG")) * 100 / dblQty, 6)
                        gv1.CurrentRow.Cells(colSNFPers).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("SNF_KG")) * 100 / dblQty, 6)


                        'Dim TSRate As Double = 0
                        'Dim FatKG As Double = 0
                        'Dim SNFKG As Double = 0
                        'Dim FatAmount As Double = 0
                        'Dim SNFAmount As Double = 0
                        'Dim Amount As Double = 0

                        'FatKG = clsCommon.myCdbl(gv1.CurrentRow.Cells(colFATKG).Value)
                        'SNFKG = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNFKG).Value)
                        'TSRate = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNFKG).Value)

                        'FatAmount = Math.Round(TSRate * FatKG, 2)
                        'SNFAmount = Math.Round(TSRate * SNFKG, 2)

                        'Amount = FatAmount + SNFAmount

                        'gv1.CurrentRow.Cells(colFatAmt).Value = clsCommon.myCdbl(FatAmount)
                        'gv1.CurrentRow.Cells(colSNFAmt).Value = clsCommon.myCdbl(SNFAmount)
                        'gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(Amount)
                    Else
                        gv1.CurrentRow.Cells(colFATPers).Value = 0
                        gv1.CurrentRow.Cells(colSNFPers).Value = 0
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try

            Dim TSRate As Double = 0
            Dim FatKG As Double = 0
            Dim SNFKG As Double = 0
            Dim FatAmount As Double = 0
            Dim SNFAmount As Double = 0
            Dim Amount As Double = 0

            FatKG = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFATKG).Value)
            SNFKG = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFKG).Value)
            TSRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)

            FatAmount = Math.Round(TSRate * FatKG, 2)
            SNFAmount = Math.Round(TSRate * SNFKG, 2)

            Amount = FatAmount + SNFAmount

            gv1.Rows(IntRowNo).Cells(colFatAmt).Value = clsCommon.myCdbl(FatAmount)
            gv1.Rows(IntRowNo).Cells(colSNFAmt).Value = clsCommon.myCdbl(SNFAmount)
            gv1.Rows(IntRowNo).Cells(colAmt).Value = clsCommon.myCdbl(Amount)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        ' done by priti BHA/27/07/18-000202 to show item mapped with location in location item mapping screen
        Dim obj As clsItemMaster
        Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
        Dim strItemLoc As String = ""
        If ShowLocationItemLocationwise = 1 Then
            strItemLoc = " and Item_code in ( select Item_code  from TSPL_LOCATION_ITEMMAPPING where location_code ='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colSiloCode).Value) & "')"
        End If
        obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, " Product_Type ='MI' and Active=1" & strItemLoc)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 isnull(UOM_Code,'') from TSPL_ITEM_UOM_DETAIL where Item_Code ='" & obj.Item_Code & "' and Default_UOM =1", Nothing))
        Else
            SetBlankOfItemColumns()
        End If
    End Sub
    Private Function LoadQty_Fat_Snf(ByVal isButtonClick As Boolean) As Double
        Try
            Dim BalQty As Double = 0
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colSiloCode).Value)) > 0 Then
                ''GKD/03/10/18-000169 richa 
                If AllowSiloMilkTransfertoMainLocation = True Then
                    Dim strSubLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select isnull(is_sub_location,'N') from TSPL_LOCATION_MASTER where Location_Code ='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colSiloCode).Value) & "'"))
                    If clsCommon.CompairString(strSubLocation, "N") = CompairStringResult.Equal Then        ''------------means location is main location
                        BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colSiloCode).Value, "", txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value))
                    Else
                        BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colMainLocCode).Value, gv1.CurrentRow.Cells(colSiloCode).Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value))
                    End If
                Else
                    BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(gv1.CurrentRow.Cells(colICode).Value, FndMainLocation.Value, gv1.CurrentRow.Cells(colSiloCode).Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, gv1.CurrentRow.Cells(colUnit).Value))
                End If
                'gv1.CurrentRow.Cells(colQty).Value = BalQty
            End If
            Return BalQty
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Function

    Function LoadQty_Fat_Snf(ByVal _Silo_Loc_Code As String, ByVal _Icode As String, ByVal _IUnit As String, ByVal _Main_Loc_Code As String, ByVal _IProductType As String, ByVal _IsSub_Location As Integer, ByVal arrLoc As String, ByVal DocDate As Date) As DataTable
        Dim dt As DataTable = Nothing
        Try
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
                CheckStockServerDate = True
            Else
                CheckStockServerDate = False
            End If
            If CheckStockServerDate Then
                DocDate = clsCommon.GETSERVERDATE()
            End If
            LoadBlankGrid()
            Dim qry As String = ""
            Dim str As String = ""
            Dim whrcls As String = ""
            If _IsSub_Location = 0 Then 'for section
                whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + _Main_Loc_Code + "' and isnull(Is_Section,'N')='Y')"

            ElseIf _IsSub_Location = 1 Then 'for sub-location
                whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + _Main_Loc_Code + "' and isnull(Is_Sub_Location,'N')='Y')"

            ElseIf _IsSub_Location = 2 Then 'for main plant
                whrcls = " and location ='" + _Main_Loc_Code + "' " ' in (select location_code from tspl_location_master where location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y')
            End If


            '======================now merge milk and normal qty
            qry = "select Location,ICode,SUM(qty*RI) as Qty,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg from ("
            qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ("
            qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
            qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew "
            qry += ",0 as fat_kg,0 as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
            qry += " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _Icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
            qry += " and (case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + _Main_Loc_Code + "' "

            Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, Nothing))
            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " union all "

            qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew "
            qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
            qry += " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + _Icode + "' "
            qry += " and (case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + _Main_Loc_Code + "' "

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _IUnit + "')axx group by axx.Location,axx.ICode"
            '==============================================end here==========================================

            str = "select * from (" + qry + ")final where 1=1 and Location='" & _Silo_Loc_Code & "'"
            str += whrcls
            dt = clsDBFuncationality.GetDataTable(str)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt
    End Function
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "

        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SiloUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        'If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
        '    qry = "select top 1 Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER where Item_Code ='" + strICode + "' and UOM='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
        '    gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        'End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = clsCommon.myCstr(i + 1)
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSilo._MYValidating
        Try
            If AllowSiloMilkTransfertoMainLocation = True Then
                Dim qryforsilo As String = "select * from (select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code  where  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + FndMainLocation.Value + "' " & Environment.NewLine & _
    "                union  " & Environment.NewLine & _
    " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   where "
                ''richa agarwal 01/apr/2019 show job location also on finder
                If chkJobWorkLocation.Checked = True Then
                    qryforsilo += " (Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N') or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' ) "
                Else
                    qryforsilo += " (Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) "
                End If


                qryforsilo += " and Location_Code ='" + FndMainLocation.Value + "')z"
                txtSilo.Value = clsCommon.ShowSelectForm("LOCMSTFNDSilo", qryforsilo, "Code", "", txtSilo.Value, "Code", isButtonClicked)
                If clsCommon.myLen(txtSilo.Value) > 0 Then
                    lblLocation.Text = clsLocation.GetName(txtSilo.Value, Nothing)
                Else
                    lblLocation.Text = ""
                End If
            Else
                txtSilo.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + FndMainLocation.Value + "'", txtSilo.Value, isButtonClicked)
                If clsCommon.myLen(txtSilo.Value) > 0 Then
                    lblLocation.Text = clsLocation.GetName(txtSilo.Value, Nothing)
                Else
                    lblLocation.Text = ""
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Function AllowToSave(ByVal blnPost As Boolean) As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        If clsCommon.myLen(FndMainLocation.Value) <= 0 Then
            FndMainLocation.Focus()
            Throw New Exception("Please select Main Location")
        End If

        If clsCommon.myLen(txtSilo.Value) <= 0 Then
            txtSilo.Focus()
            Throw New Exception("Please select Location")
        End If
        If clsCommon.myLen(FndItemCode.Value) <= 0 Then
            FndItemCode.Focus()
            Throw New Exception("Please select Item Code")
        End If
        If clsCommon.myLen(fnditemuom.Value) <= 0 Then
            fnditemuom.Focus()
            Throw New Exception("Please select Item UOM")
        End If
        Dim chkMianLoc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) AS chkMainLoc from TSPL_LOCATION_MASTER where  Location_Code='" + FndMainLocation.Value + "' AND  isnull(is_sub_location,'N')='Y'"))
        Dim chkSubLoc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(1) AS chkMainLoc from TSPL_LOCATION_MASTER where  Location_Code='" + txtSilo.Value + "' AND  isnull(is_sub_location,'N')='Y'"))
        If AllowSiloMilkTransfertoMainLocation = False Then
            If chkMianLoc > 0 AndAlso chkSubLoc > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(txtSilo.Value), clsCommon.myCstr(FndMainLocation.Value)) = CompairStringResult.Equal Then
                    txtSilo.Focus()
                    Throw New Exception("Main Location and Sub Location can not be same")
                End If
            End If
        End If

        Dim arrItemCode As List(Of String) = New List(Of String)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim strSiloCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSiloCode).Value)
            Dim strMainLocCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMainLocCode).Value)
            Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim dblTSRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
            If clsCommon.myLen(strSiloCode) > 0 Then
                If arrItemCode.Contains(strSiloCode) Then
                    Throw New Exception("Duplicate Silo " & strSiloCode & " Found at Row No " & (ii + 1))
                Else
                    arrItemCode.Add(strSiloCode)
                End If
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If

                If clsCommon.myCdbl(dblEnteredQty) <= 0 Then
                    Throw New Exception("Please enter Qty of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If

                If clsCommon.myCdbl(dblTSRate) <= 0 Then
                    Throw New Exception("Please enter Rate of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If blnPost = True Then
                    Dim objPer As MIlkComponentType = clsItemMaster.GetItemFatSNF(gv1.Rows(ii).Cells(colICode).Value, Nothing)
                    Dim dblFatPer As Double = objPer.FAT_Per
                    Dim dblSNFPer As Double = objPer.SNF_Per
                    Dim dblCurrentFatPer As Double = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPers).Value) * 1.5, 2)
                    Dim dblCurrentSNFPer As Double = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPers).Value) * 1.5, 2)
                    If dblCurrentFatPer > dblFatPer Then
                        gv1.Rows(ii).Cells(colFATPers).Value = dblFatPer
                    Else
                        gv1.Rows(ii).Cells(colFATPers).Value = Math.Round(gv1.Rows(ii).Cells(colFATPers).Value, 2)
                    End If
                    If dblCurrentSNFPer > dblSNFPer Then
                        gv1.Rows(ii).Cells(colSNFPers).Value = dblSNFPer
                        gv1.Rows(ii).Cells(colSNFPers).Value = Math.Round(gv1.Rows(ii).Cells(colSNFPers).Value, 2)
                    End If
                End If
                Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)

                Dim BalQty As Double = 0.0
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                    If clsLocation.IsJobWorkLocation(strMainLocCode, Nothing) = True Then
                        BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strICode, strMainLocCode, "", txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM))
                    Else
                        BalQty = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strICode, strMainLocCode, strSiloCode, txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM))
                    End If

                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                        If BalQty > 0 Then
                            BalQty = ClsLoadingTanker.GetTolerane(BalQty, dblEnteredQty)
                        End If
                    End If
                    If Math.Round(BalQty, 2) < Math.Round(dblEnteredQty, 2) Then
                        Throw New Exception("Item : " + strICode + " Available Qty is :     " & Math.Round(BalQty, 2) & Environment.NewLine & "Required Qty :     " & clsCommon.myCstr(dblEnteredQty) & " ")
                    End If

                    ''TO CHECK FAT AND SNF
                    Dim CurBal_SNF As Double = 0
                    Dim CurBal_FAT As Double = 0
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
                        Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & strICode & "','" & strSiloCode & "','" & clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy") & "')", Nothing)
                        If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                            CurBal_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                            CurBal_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                        End If

                        If CurBal_SNF < clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFKG).Value) Then
                            Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFKG).Value) & " ")
                        End If
                        If CurBal_FAT < clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATKG).Value) Then
                            Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATKG).Value) & " ")
                        End If
                    End If


                End If

            End If
        Next
        Return True
    End Function

    Private Function SaveData(ByVal blnPost As Boolean) As Boolean
        Try
            If (AllowToSave(blnPost)) Then
                Dim obj As New ClsSiloMilkTransfer()
                obj.Document_Code = txtAdjustmentNo.Value
                obj.Document_Date = txtDate.Value
                obj.Silo_Code = txtSilo.Value
                obj.Description = txtDesc.Text
                obj.MainLocation_Code = FndMainLocation.Value
                obj.Item_Code = FndItemCode.Value
                obj.Item_UOM = fnditemuom.Value
                If chkJobWorkLocation.Checked Then
                    obj.IsJobWork = 1
                End If
                obj.Arr = New List(Of ClsSiloMilkTransferDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New ClsSiloMilkTransferDetails()

                        objTr.Line_No = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                        objTr.MainLoc_Code = clsCommon.myCstr(grow.Cells(colMainLocCode).Value)
                        objTr.Silo_Code = clsCommon.myCstr(grow.Cells(colSiloCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        objTr.FatAmount = clsCommon.myCdbl(grow.Cells(colFatAmt).Value)
                        objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(colSNFAmt).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.fat_pers = clsCommon.myCdbl(grow.Cells(colFATPers).Value)
                        objTr.fat_kg = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.snf_pers = clsCommon.myCdbl(grow.Cells(colSNFPers).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)

                If Not isFlag Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New ClsSiloMilkTransfer()
            obj = obj.GetData(strCode, NavTyep, Nothing, True)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If


                txtAdjustmentNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtDesc.Text = obj.Description
                txtSilo.Value = obj.Silo_Code
                lblLocation.Text = obj.Silo_Code_Desc
                FndMainLocation.Value = obj.MainLocation_Code
                LblMainLocation.Text = obj.MainLocation_Desc
                FndItemCode.Value = obj.Item_Code
                LblItemName.Text = obj.Item_Desc
                fnditemuom.Value = obj.Item_UOM
                If obj.IsJobWork = 1 Then
                    chkJobWorkLocation.Checked = True
                Else
                    chkJobWorkLocation.Checked = False
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsSiloMilkTransferDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objTr.Line_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSiloCode).Value = objTr.Silo_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSiloName).Value = objTr.Silo_Code_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainLocCode).Value = objTr.MainLoc_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainLocName).Value = objTr.MainLoc_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatAmt).Value = objTr.FatAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFAmt).Value = objTr.SNFAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).Value = objTr.fat_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).Value = objTr.fat_pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).Value = objTr.snf_pers
                    Next

                    If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Doc_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtAdjustmentNo.Value) > 0 Then
                Doc_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Document_Code from TSPL_SILO_MILK_TRANSFER_HEAD  where TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code='" + txtAdjustmentNo.Value + "'"))
                If Doc_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        If SaveData(True) = False Then
                            Exit Sub
                        End If

                        If (ClsSiloMilkTransfer.PostData_JobWork(txtAdjustmentNo.Value)) Then

                            clsCommon.MyMessageBoxShow(Me, "Data posted successfully.", Me.Text)
                            LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering Document code")
                End If

            Else
                Throw New Exception("Document code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
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
                If (ClsSiloMilkTransfer.DeleteData(txtAdjustmentNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtAdjustmentNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAdjustmentNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SILO_MILK_TRANSFER_HEAD where Document_Code='" + txtAdjustmentNo.Value + "' and IsJobWork=1 "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAdjustmentNo.MyReadOnly = False
            Else
                txtAdjustmentNo.MyReadOnly = True
            End If
            LoadData(txtAdjustmentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAdjustmentNo._MYValidating
        Dim qry As String = "SELECT TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code as Code,convert(varchar,TSPL_SILO_MILK_TRANSFER_HEAD.Document_Date,103) as [Document Date],TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code as [Main Location],tspl_Location_master.Location_Desc as MainLocation_Name,TSPL_SILO_MILK_TRANSFER_HEAD.Silo_Code  as [Silo Location],Silo.Location_Desc as Silo_Name,TSPL_SILO_MILK_TRANSFER_HEAD.Item_Code as [Item Code],tspl_Item_master.Item_desc as [Item Name],case when TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SILO_MILK_TRANSFER_HEAD left outer join tspl_Location_master on tspl_Location_master.Location_Code=TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code left outer join tspl_Location_master Silo on Silo.Location_Code=TSPL_SILO_MILK_TRANSFER_HEAD.Silo_Code left outer join tspl_Item_master on tspl_Item_master.Item_Code=TSPL_SILO_MILK_TRANSFER_HEAD.Item_Code  "
        Dim whrClas As String = " TSPL_SILO_MILK_TRANSFER_HEAD.IsJobWork=1 "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        txtAdjustmentNo.Value = clsCommon.ShowSelectForm("SiloMilkTransfer", qry, "Code", whrClas, txtAdjustmentNo.Value, "Code", isButtonClicked, "TSPL_SILO_MILK_TRANSFER_HEAD.Document_Date")
        LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                OpenUOMList(True)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_SILO_MILK_TRANSFER_HEAD " + Environment.NewLine + _
                                              "TSPL_SILO_MILK_TRANSFER_DETAIL " + Environment.NewLine + _
                                              "Post Trasnaction " + Environment.NewLine + _
                                              "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine + _
                                              "TSPL_SERIAL_ITEM " + Environment.NewLine + _
                                              "TSPL_BATCH_ITEM " + Environment.NewLine + _
                                              "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine + _
                                              "TSPL_JOURNAL_MASTER ")
            If btnPost.Enabled = False AndAlso btnSave.Enabled = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnunpost.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            'funPrint()
        End If
    End Sub


    Private Sub FndMainLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMainLocation._MYValidating
        ''richa agarwal 01/apr/2019 show job location also on finder ERO/01/04/19-000537
        Dim strwhrcls As String = String.Empty
        If chkJobWorkLocation.Checked = True Then
            strwhrcls += "  (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' )  "
        Else
            strwhrcls += "  (Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0))"
        End If
        FndMainLocation.Value = clsLocation.getFinder(strwhrcls, FndMainLocation.Value, isButtonClicked)
        If clsCommon.myLen(FndMainLocation.Value) > 0 Then
            LblMainLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndMainLocation.Value & "'"))
            If clsLocation.IsJobWorkLocation(FndMainLocation.Value, Nothing) = True Then
                txtSilo.Value = FndMainLocation.Value
                If clsCommon.myLen(txtSilo.Value) > 0 Then
                    lblLocation.Text = clsLocation.GetName(txtSilo.Value, Nothing)
                Else
                    lblLocation.Text = ""
                End If
                txtSilo.Enabled = False
            Else
                txtSilo.Enabled = True
            End If
        Else
            LblMainLocation.Text = ""
            txtSilo.Value = ""
        End If
    End Sub

    Private Sub FndItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndItemCode._MYValidating
        Try
            'Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            'FndItemCode.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Product_Type ='MI' and Active=1", FndItemCode.Value, "", isButtonClicked)
            'LblItemName.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCode.Value + "' ")
            Dim obj As clsItemMaster
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(FndItemCode.Value)), "", isButtonClicked, " Product_Type ='MI' and Active=1")
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                FndItemCode.Value = obj.Item_Code
                LblItemName.Text = obj.Item_Desc
                fnditemuom.Value = obj.Unit_Code
            Else
                FndItemCode.Value = ""
                LblItemName.Text = ""
                fnditemuom.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fnditemuom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fnditemuom._MYValidating
        Try
            Dim strICode As String = clsCommon.myCstr(FndItemCode.Value)
            If clsCommon.myLen(strICode) <= 0 Then
                Throw New Exception("Please select Item Code")
            End If

            Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "

            Dim WhrCls As String = "Item_Code ='" + strICode + "'"
            fnditemuom.Value = clsCommon.ShowSelectForm("SiloUOM", qry, "Code", WhrCls, clsCommon.myCstr(fnditemuom.Value), "Code", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                isCellValueChangedOpen = False
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    ''richa BHA/17/08/18-000454
    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_SILO_MILK_TRANSFER_HEAD where Posted='0' and Document_Code='" + txtAdjustmentNo.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If ClsSiloMilkTransfer.UnpostData(txtAdjustmentNo.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtAdjustmentNo.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Unpost and Recreated", Me.Text)
                        btnunpost.Visible = False
                        LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtAdjustmentNo.Value)
    End Sub
End Class
