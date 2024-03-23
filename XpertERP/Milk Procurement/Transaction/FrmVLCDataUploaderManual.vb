'--------Created By Richa 05/09/2014 Against Ticket No BM00000003791
''updation by richa agarwal on 15/10/2014 (add uom in grid) against ticket no BM00000004318

Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports XpertERPCommanServices
Public Class FrmVLCDataUploaderManual
    Inherits FrmMainTranScreen
    Public Const colSlNo As String = "SLNO"
    Public Const colFarmerCode As String = "colFarmerCode"
    Public Const colFarmerCode_VlcUploader As String = "colFarmerCode_VlcUploader"
    Public Const colFarmerName As String = "colFarmerName"
    Public Const colUOM As String = "colUOM"
    Public Const colQty As String = "Qty"
    Public Const colFatPer As String = "colFatPer"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colRate As String = "colRate"
    Public Const colAmount As String = "colAmount"
    Const colRejectRejectType As String = "colRejectRejectType"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoadData As Boolean = False
    Dim Qry As String
    Dim isFlag As Boolean = False
    Dim Mcc_Uom As String = String.Empty
    Dim arrLoc As String = Nothing
    Dim UseVLCUploaderCodeMPUploaderCodeInMCCProcurement As Boolean = False
    Dim settBennyImportPickRateFromPrice As Boolean = False
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmVLCDataUploaderManual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                      "========Table Name=========" + Environment.NewLine + _
                      "TSPL_VLC_DATA_UPLOADER_MASTER" + Environment.NewLine + _
                      "TSPL_VLC_DATA_UPLOADER_DETAIL" + Environment.NewLine + _
                      "=========Setting Name======" + Environment.NewLine + _
                      "isFarmerPaymentCycle")
            If RadButton238.Visible Then
                multipleDelteVisible(False)
            Else
                Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" + clsFixedParameterCode.VillageDataReverse + "' and TYPE='" + clsFixedParameterType.VillageDataReverse + "'")
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.VillageDataReverse
                pwd.strType = clsFixedParameterType.VillageDataReverse
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    multipleDelteVisible(True)
                End If
            End If
        End If
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Sub multipleDelteVisible(ByVal val As Boolean)
        MyLabel34.Visible = val
        TxtMultiSelectFinder3.Visible = val
        RadButton238.Visible = val
    End Sub

    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, clsFixedParameterCode.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, Nothing)) = 1, True, False)
        settBennyImportPickRateFromPrice = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BennyImportPickRateFromPrice, clsFixedParameterCode.BennyImportPickRateFromPrice, Nothing)) = 1)
        Reset()
        multipleDelteVisible(False)
        MCCLOCATIONFINDER()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        'Try
        '    If clsCommon.myLen(fndMCC_Code.Value) > 0 Then
        '        SetDocKCollectionMilkType(fndMCC_Code.Value)
        '    End If            
        'Catch ex As Exception
        'End Try
        '' MilkDocType
        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"
        SetDocKCollectionMilkType(fndMCC_Code.Value)
        'cboType.Text = "Mix"
        'cboMilkType.Text = "Good"
        'txtMilkWeight.Enabled = False

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Me.Focus()
        txtdate.Focus()
        
    End Sub
    Sub loadBlankItemGrid()

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(lineNo)


        Dim farmercode As New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "Farmer Code Main"
        farmercode.Name = colFarmerCode
        farmercode.Width = 0
        farmercode.ReadOnly = False
        farmercode.IsVisible = False
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode)

        Dim farmercode_VlcUploader As New GridViewTextBoxColumn()
        farmercode_VlcUploader.FormatString = ""
        farmercode_VlcUploader.HeaderText = "Farmer Code"
        farmercode_VlcUploader.Name = colFarmerCode_VlcUploader
        farmercode_VlcUploader.Width = 100
        farmercode_VlcUploader.ReadOnly = False
        farmercode_VlcUploader.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmercode_VlcUploader)


        Dim farmername As New GridViewTextBoxColumn()
        farmername.FormatString = ""
        farmername.HeaderText = "Farmer Name"
        farmername.Name = colFarmerName
        farmername.Width = 320
        farmername.ReadOnly = True
        farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(farmername)

        Dim UOM As New GridViewTextBoxColumn()
        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(UOM)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = ""
        Qty.HeaderText = "Qty"
        Qty.Name = colQty
        Qty.Width = 120
        Qty.ReadOnly = False
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Qty.ShowUpDownButtons = False
        gvItem.Columns.Add(Qty)

        Dim fat As New GridViewDecimalColumn
        fat.FormatString = ""
        fat.HeaderText = "FAT %"
        fat.Name = colFatPer
        fat.Width = 75
        fat.FormatString = "{0:n2}"
        fat.ReadOnly = False
        fat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        fat.ShowUpDownButtons = False
        gvItem.Columns.Add(fat)


        Dim snf As New GridViewDecimalColumn
        snf.FormatString = ""
        snf.HeaderText = "SNF %"
        snf.Name = colSNFPer
        snf.Width = 75
        snf.FormatString = "{0:n2}"
        snf.ReadOnly = False
        snf.ShowUpDownButtons = False
        snf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(snf)

        Dim Rate As New GridViewDecimalColumn
        Rate.FormatString = ""
        Rate.HeaderText = "Rate"
        Rate.Name = colRate
        Rate.Width = 75
        Rate.FormatString = "{0:n2}"
        Rate.ReadOnly = True
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Rate)


        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = ""
        Amount.HeaderText = "Amount"
        Amount.Name = colAmount
        Amount.Width = 75
        Amount.FormatString = "{0:n2}"
        Amount.ReadOnly = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(Amount)

        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Reject Type"
        repoComboBox.Name = colRejectRejectType
        repoComboBox.Width = 80
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = ""
            dr("Name") = "Good"
            dt.Rows.InsertAt(dr, 0)
        End If
        repoComboBox.DataSource = dt
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Name"
        repoComboBox.IsVisible = True
        gvItem.MasterTemplate.Columns.Add(repoComboBox)



        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = True
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = True
        gvItem.ShowFilteringRow = True
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        ReStoreGridLayout()
        'gvItem.GridBehavior = New MyBehavior()
        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSlNo).Value = "1"
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmVLCDataUploaderManual)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag

    End Sub
    Sub Reset()
        fndDocumentNo.Value = ""
        FNDRouteCode.Value = ""
        fndVLCCode.Value = ""
        fndVLCCode.Tag = ""
        LblRouteName.Text = ""
        LblVLCName.Text = ""
        ddlShift.Text = ""
        fndMCC_Code.Value = ""
        lblMCCDesc.Text = ""
        txtdate.Text = clsCommon.GETSERVERDATE()
        fndDocumentNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        loadBlankItemGrid()
        ReStoreGridLayout()
        txtdate.Focus()
        gvPDCollectHist.DataSource = Nothing
        gvManualCollectHist.DataSource = Nothing
        'fndMCC_Code.Value = objCommonVar.CurrLocationCode
        'lblMCCDesc.Text = objCommonVar.CurrLocationName
        fndMCC_Code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        FNDRouteCode.Tag = fndMCC_Code.Value
        If clsCommon.myLen(fndMCC_Code.Value) > 0 Then
            lblMCCDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" & fndMCC_Code.Value & "' "))
        End If
        Mcc_Uom = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE=(select mcc_Code from tspl_mcc_route_master where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "')")
        FNDRouteCode.Enabled = False
        EnableInputDataField()
        isNewEntry = True
        LockControlAfterSelectAllFinders(True)
        IsinsideLoadData = False
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colFarmerCode_VlcUploader) Then
                        OpenFarmerCodeList(False)
                    End If
                    If e.Column Is gvItem.Columns(colFatPer) Or e.Column Is gvItem.Columns(colSNFPer) Or e.Column Is gvItem.Columns(colQty) Or e.Column Is gvItem.Columns(colRejectRejectType) Then
                        gvItem.CurrentRow.Cells(colRate).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(gvItem.CurrentRow.Cells(colFatPer).Value), clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNFPer).Value), clsCommon.myCstr(FNDRouteCode.Tag), clsCommon.myCstr(fndVLCCode.Tag), IIf(clsCommon.myCstr(ddlShift.Text).ToString.Contains("M"), "M", "E"), clsCommon.myCDate(txtdate.Value), Nothing, cboDockCollectionMilkType.SelectedValue, settBennyImportPickRateFromPrice)
                        If clsCommon.myLen(gvItem.CurrentRow.Cells(colRejectRejectType).Value) > 0 Then
                            Dim objMRT As clsMilkRejectType = clsMilkRejectType.GetData(clsCommon.myCstr(gvItem.CurrentRow.Cells(colRejectRejectType).Value), NavigatorType.Current, Nothing)
                            If objMRT Is Nothing Then
                                Throw New Exception("Invalid rejection type [" + clsCommon.myCstr(gvItem.CurrentRow.Cells(colRejectRejectType).Value) + "]")
                            End If
                            gvItem.CurrentRow.Cells(colRate).Value = Math.Round(clsCommon.myCdbl(gvItem.CurrentRow.Cells(colRate).Value) * objMRT.Applicable_Per / 100, 2, MidpointRounding.ToEven)
                        End If

                        gvItem.CurrentRow.Cells(colAmount).Value = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colRate).Value)
                        CalculateFatSNFQty()
                    End If
                End If
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub
    Private Sub CalculateFatSNFQty()
        Try

            Dim TotalFatKG As Double = 0
            Dim TotalSNFKG As Double = 0
            Dim TotalQty As Double = 0
            Dim FatWeightage As Double = 0
            Dim SNFWeightage As Double = 0
            Dim FatPer As Double = 0
            Dim SNFPer As Double = 0
            Dim FatKG As Double = 0
            Dim SNFKG As Double = 0
            Dim Qty As Double = 0

            For i As Integer = 0 To gvItem.Rows.Count - 1
                FatPer = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatPer).Value)
                SNFPer = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFPer).Value)
                Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value)
                FatKG = Math.Round(((Qty * FatPer) / 100), 3)
                SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)

                TotalFatKG = TotalFatKG + FatKG
                TotalSNFKG = TotalSNFKG + SNFKG
                TotalQty = TotalQty + Qty
            Next



            lblTotQty.Text = Format(clsCommon.myCdbl(TotalQty), "###0.00")
            lblAvgFat.Text = Format(clsCommon.myCdbl((TotalFatKG * 100) / TotalQty), "###0.00")
            lblAvgSNF.Text = Format(clsCommon.myCdbl((TotalSNFKG * 100) / TotalQty), "###0.00")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenFarmerCodeList(ByVal isButtonClick As Boolean)

        Dim strFCode As String = clsCommon.myCstr(gvItem.CurrentRow.Cells(colFarmerCode_VlcUploader).Value)
        Dim qry As String = "Select Mp_Code_vlc_Uploader as Code,MP_Code  as [MP Code] ,MP_Name as [Farmer Name] from TSPL_MP_MASTER inner join tspl_vlc_master_Head " _
              & " on tspl_vlc_master_Head.vlc_Code=TSPL_MP_MASTER.vlc_Code "
        gvItem.CurrentRow.Cells(colFarmerCode_VlcUploader).Value = clsCommon.ShowSelectForm("FarmerCode", qry, "Code", "  MCC=(select mcc_Code from tspl_mcc_route_master where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "') and vlc_code_vlc_uploader='" & clsCommon.myCstr(fndVLCCode.Value) & "'  ", strFCode, "Code", isButtonClick)
        gvItem.CurrentRow.Cells(colFarmerCode).Value = clsDBFuncationality.getSingleValue("Select MP_Code as [Farmer Name] from TSPL_MP_MASTER inner join tspl_vlc_master_Head " _
              & " on tspl_vlc_master_Head.vlc_Code=TSPL_MP_MASTER.vlc_Code  where MP_Code_vlc_Uploader='" + clsCommon.myCstr(gvItem.CurrentRow.Cells(colFarmerCode_VlcUploader).Value) + "'  and  MCC=(select mcc_Code from tspl_mcc_route_master where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "') and vlc_code_vlc_uploader='" & clsCommon.myCstr(fndVLCCode.Value) & "' ")
        gvItem.CurrentRow.Cells(colFarmerName).Value = clsDBFuncationality.getSingleValue("Select MP_Name as [Farmer Name] from TSPL_MP_MASTER where MP_Code='" + clsCommon.myCstr(gvItem.CurrentRow.Cells(colFarmerCode).Value) + "' ")
        gvItem.CurrentRow.Cells(colUOM).Value = Mcc_Uom
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("VLCUOMFinder", qry, "Code", "", clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
    End Sub

    Private Function AllowToSave() As Boolean

        '====================Added by preeti gupta==================

        clsLockMPPaymentCycle.LockMPTransaction(fndMCC_Code.Value, txtdate.Value)

        '===========================================================
        '= KUNAL > TICKET : BM00000009575 =====
        If AllowFutureDateTransaction(txtdate.Value, Nothing) = False Then
            txtdate.Focus()
            Return False
        End If

        If clsCommon.myLen(fndVLCCode.Value) <= 0 Then
            fndVLCCode.Focus()
            Throw New Exception("Please Select VLC Code")
        End If

        If clsCommon.myLen(FNDRouteCode.Value) <= 0 Then
            FNDRouteCode.Focus()
            Throw New Exception("Please Select Route Code")
        End If

        If clsCommon.myLen(ddlShift.Text) <= 0 Then
            ddlShift.Focus()
            Throw New Exception("Please Select Shift")
        End If
        If gvItem.Rows.Count = 1 Then
            If clsCommon.myLen(gvItem.Rows(0).Cells(colFarmerCode).Value) <= 0 Then
                Throw New Exception("Please enter alteast one entry in grid")
            End If
        End If
        Dim arrMP As New List(Of String)
        For i As Integer = 0 To gvItem.Rows.Count - 1
            'If clsCommon.myLen(gvItem.Rows(i).Cells(colFarmerCode).Value) <= 0 Then
            '    Throw New Exception("Please Select Farmer Code")
            'End If
            If clsCommon.myLen(gvItem.Rows(i).Cells(colFarmerCode).Value) > 0 Then
                Dim Mcc_Code_Uploader As String = clsDBFuncationality.getSingleValue("select Mcc_Code_Vlc_Uploader from tspl_mcc_master where mcc_code='" & fndMCC_Code.Value & "'")
                '' check for duplicate MP collection
                Dim dt As DataTable = ClsVLCDataUploaderManual.GetQueryDT("PDU", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"), ddlShift.Text, fndMCC_Code.Value, "", fndVLCCode.Value, FNDRouteCode.Value, clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode_VlcUploader).Value), Mcc_Code_Uploader)
                If dt.Rows.Count > 0 Then
                    Throw New Exception("Data is Entered for MP-" & clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode).Value) & " Uploader Code-" & clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode_VlcUploader).Value) & " MP Name-" & clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerName).Value) & " through Uploader.")
                End If
                If clsCommon.myLen(gvItem.Rows(i).Cells(colUOM).Value) <= 0 Then
                    Throw New Exception("Please Select UOM")
                End If
                'If clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value) <= 0 Then
                '    Throw New Exception("Please Enter Qty")
                'End If
                If clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value) > 0 Then
                    If clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatPer).Value) <= 0 Then
                        Throw New Exception("Please Enter Fat %")
                    End If
                    If clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFPer).Value) <= 0 Then
                        Throw New Exception("Please Enter SNF %")
                    End If
                End If

                'If clsCommon.myCdbl(gvItem.Rows(i).Cells(colRate).Value) <= 0 Then
                '    Throw New Exception("Please Enter Rate")
                'End If
                If arrMP.Contains(clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode).Value)) Then
                    Throw New Exception("Farmer Code [" + (clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode_VlcUploader).Value)) + "].Farmer Main code [" + (clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode).Value)) + "] is repeated at row no [" + clsCommon.myCstr(gvItem.Rows(i).Cells(colSlNo).Value) + "]")
                Else
                    arrMP.Add(clsCommon.myCstr(gvItem.Rows(i).Cells(colFarmerCode).Value))
                End If
            End If
        Next
        If arrMP IsNot Nothing AndAlso arrMP.Count > 0 Then
            Qry = "Select TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.Mp_Code_vlc_Uploader as Uploader_Code,TSPL_MP_MASTER.MP_Name,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader
from TSPL_MP_MASTER 
left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.vlc_Code=TSPL_MP_MASTER.VLC_Code  
Where 2=2   and TSPL_MP_MASTER.VLC_Code<>'" + clsCommon.myCstr(fndVLCCode.Tag) + "' and MP_Code in(" + clsCommon.GetMulcallString(arrMP) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim str As String = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    str += "MP [" + clsCommon.myCstr(dt.Rows(ii)("Uploader_Code")) + "][ " + clsCommon.myCstr(dt.Rows(ii)("MP_Code")) + "][" + clsCommon.myCstr(dt.Rows(ii)("MP_Name")) + "] is not related To VLC [" + fndVLCCode.Tag + "]"
                Next
                Throw New Exception(str)
            End If
        End If
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsVLCDataUploaderManual()
                obj.Document_Code = fndDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.Route_Code = FNDRouteCode.Value
                obj.VLC_Code = fndVLCCode.Tag 'fndVLCCode.Value
                obj.Shift = ddlShift.Text
                obj.Dock_Collection_Milk_Type = cboDockCollectionMilkType.SelectedValue
                Dim i As Integer = 0
                Dim objTr As New ClsVLCDataUploaderManualdetail
                obj.arrVLCDetail = New List(Of ClsVLCDataUploaderManualdetail)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    objTr = New ClsVLCDataUploaderManualdetail()
                    If clsCommon.myLen(grow.Cells(colFarmerCode).Value) > 0 Then
                        objTr.Document_Code = clsCommon.myCstr(obj.Document_Code)
                        objTr.Farmer_Code = clsCommon.myCstr(grow.Cells(colFarmerCode).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                        objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                        objTr.Reject_Type = clsCommon.myCstr(grow.Cells(colRejectRejectType).Value)
                        If objTr.Qty > 0 Then
                            obj.arrVLCDetail.Add(objTr)
                        End If
                    End If
                Next
                If (ClsVLCDataUploaderManual.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If clsCommon.myLen(ex.Message) > 200 Then
                clsERPFuncationality.OpenNotepadFile(ex.Message, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            clsLockMPPaymentCycle.LockMPTransaction(fndMCC_Code.Value, txtdate.Value)
            If (deleteConfirm()) Then
                If (ClsVLCDataUploaderManual.DeleteData(fndDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        IsinsideLoadData = True
        Dim obj As ClsVLCDataUploaderManual = ClsVLCDataUploaderManual.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndDocumentNo.Value = obj.Document_Code
            txtdate.Value = obj.Document_Date
            FNDRouteCode.Value = obj.Route_Code
            LblRouteName.Text = obj.Route_Name
            fndVLCCode.Tag = obj.VLC_Code
            cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type

            fndVLCCode.Value = clsDBFuncationality.getSingleValue("Select VLC_code_vlc_Uploader from TSPL_VLC_MASTER_HEAD where VLC_Code ='" + fndVLCCode.Tag + "' and " _
                                   & " mcc=(select mcc_Code from TSPL_MCC_ROUTE_MASTER where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "')")
            LblVLCName.Text = obj.VLC_NAME
            ddlShift.Text = obj.Shift
            fndMCC_Code.Value = obj.MCC_Code
            lblMCCDesc.Text = obj.MCC_Name
            FNDRouteCode.Tag = obj.MCC_Code
            Mcc_Uom = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE=(select mcc_Code from tspl_mcc_route_master where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "')")
            If obj.arrVLCDetail IsNot Nothing AndAlso obj.arrVLCDetail.Count > 0 Then
                LockControlAfterSelectAllFinders(False)
                loadBlankItemGrid()
                For Each objTr As ClsVLCDataUploaderManualdetail In obj.arrVLCDetail
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode).Value = objTr.Farmer_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode_VlcUploader).Value = objTr.Farmer_Code_vlc_Uploader
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerName).Value = objTr.Farmer_Name
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.Unit_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                    gvItem.Rows.AddNew()
                Next
                gvItem.Rows.RemoveAt(gvItem.Rows.Count - 1)
            Else
                gvItem.DataSource = Nothing
            End If


            fndDocumentNo.MyReadOnly = True
            btnsave.Text = "Update"
            btnsave.Enabled = True
            btndelete.Enabled = True

        Else
            Reset()

        End If
        LoadHistryData()
        CalculateFatSNFQty()
        IsinsideLoadData = False
    End Sub

    Private Sub RDSavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDSavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItem.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gvItem"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItem.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItem.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gvItem.MasterTemplate.FilterDescriptors.Clear()
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RdDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub fndDocumentNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_VLC_DATA_UPLOADER_MASTER where Document_Code='" + fndDocumentNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndDocumentNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndDocumentNo.MyReadOnly = False
            End If

            LoadData(fndDocumentNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocumentNo._MYValidating
        fndDocumentNo.Value = ClsVLCDataUploaderManual.getFinder("", fndDocumentNo.Value, isButtonClicked)
        LoadData(fndDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FNDRouteCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FNDRouteCode._MYValidating
        FNDRouteCode.Value = clsfrmMilkRouteMaster.getFinder("tspl_mcc_route_master.mcc_code in (" + arrLoc + ")", FNDRouteCode.Value, isButtonClicked)
        LblRouteName.Text = clsDBFuncationality.getSingleValue("Select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code ='" + FNDRouteCode.Value + "' ")
        FNDRouteCode.Tag = clsDBFuncationality.getSingleValue("Select Mcc_Code from TSPL_MCC_ROUTE_MASTER where Route_Code ='" + FNDRouteCode.Value + "' ")
        Mcc_Uom = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE=(select mcc_Code from tspl_mcc_route_master where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "')")
    End Sub
    Dim intAutoFillMPOrder As Integer = 0
    Private Sub fndVLCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVLCCode._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(fndMCC_Code.Value)) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select MCC First.", Me.Text)
                fndMCC_Code.Focus()
                Exit Sub
            End If

            If clsCommon.myLen(clsCommon.myCstr(ddlShift.Text)) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Shift.", Me.Text)
                ddlShift.Focus()
                Exit Sub
            End If

            Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [VLC Code],VLC.VLC_Name as [VLC Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name,VLC.Auto_Fill_MP_Order " &
                      " from TSPL_VLC_MASTER_HEAD VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code "
            Dim Whrcls As String
            If clsCommon.myLen(fndMCC_Code.Value) > 0 Then
                Whrcls = " VLC.MCC='" & clsCommon.myCstr(fndMCC_Code.Value) & "'"
            Else
                Whrcls = ""
            End If
            fndVLCCode.Value = clsCommon.ShowSelectForm("VLCCode", qry, "Code", Whrcls, fndVLCCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndVLCCode.Value) > 0 Then
                Mcc_Uom = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & fndMCC_Code.Value & "'")
                If clsCommon.myLen(Whrcls) > 0 Then
                    Whrcls = Whrcls & " AND VLC.VLC_Code_vlc_Uploader='" & fndVLCCode.Value & "'"
                Else
                    Whrcls = "  VLC.VLC_Code_vlc_Uploader='" & fndVLCCode.Value & "'"
                End If
                If clsCommon.myLen(Whrcls) > 0 Then
                    qry = qry & " where 2=2 and " & Whrcls
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    fndVLCCode.Tag = clsCommon.myCstr(dt.Rows(0).Item("VLC Code"))
                    LblVLCName.Text = clsCommon.myCstr(dt.Rows(0).Item("VLC Name"))
                    FNDRouteCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Route Code"))
                    LblRouteName.Text = clsCommon.myCstr(dt.Rows(0).Item("Route_Name"))
                    If clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order")) > 0 Then
                        intAutoFillMPOrder = clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order"))
                        qry = "select MP_Code,MP_Code_VLC_Uploader,MP_Name  from TSPL_MP_MASTER where vlc_Code='" + clsCommon.myCstr(dt.Rows(0).Item("VLC Code")) + "' order by "
                        If clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order")) = 1 Then
                            qry += " MP_Code_VLC_Uploader "
                        ElseIf clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order")) = 2 Then
                            qry += " MP_Code_VLC_Uploader desc "
                        ElseIf clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order")) = 3 Then
                            qry += " MP_Name"
                        ElseIf clsCommon.myCdbl(dt.Rows(0).Item("Auto_Fill_MP_Order")) = 4 Then
                            qry += " MP_Name desc "
                        Else
                            Throw New Exception("Wrong Auto Fill MP Order")
                        End If
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            loadBlankItemGrid()
                            IsinsideLoadData = True
                            For Each dr As DataRow In dt.Rows
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode).Value = clsCommon.myCstr(dr("MP_Code"))
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode_VlcUploader).Value = clsCommon.myCstr(dr("MP_Code_VLC_Uploader"))
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerName).Value = clsCommon.myCstr(dr("MP_Name"))
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = Mcc_Uom
                                gvItem.Rows.AddNew()
                            Next
                            IsinsideLoadData = False
                            gvItem.CurrentRow = gvItem.Rows(0)
                            gvItem.CurrentColumn = gvItem.Columns(colQty)
                            gvItem.Focus()
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadHistryData()
        LoadVLCDataManual()
        LoadVLCDataPDU()
        Dim dt As DataTable = ClsVLCDataUploaderManual.GetQueryDT1("", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"), ddlShift.Text, fndMCC_Code.Value, "", fndVLCCode.Value, FNDRouteCode.Value, "")
        If dt.Rows.Count > 0 Then
            lblTotQty.Text = Format(clsCommon.myCdbl(dt.Rows(0).Item("VLC_Qty")), "###0.00")
            lblAvgFat.Text = Format(clsCommon.myCdbl(dt.Rows(0).Item("Fat_Per")), "###0.00")
            lblAvgSNF.Text = Format(clsCommon.myCdbl(dt.Rows(0).Item("Snf_Per")), "###0.00")
        End If
        '' disable fields
        DisableInputDataField()
    End Sub

    Sub DisableInputDataField()
        txtdate.Enabled = False
        ddlShift.Enabled = False
        fndMCC_Code.Enabled = False
        fndVLCCode.Enabled = False
    End Sub
    Sub EnableInputDataField()
        txtdate.Enabled = True
        ddlShift.Enabled = True
        fndMCC_Code.Enabled = True
        fndVLCCode.Enabled = True
    End Sub
    Sub LoadVLCDataManual()
        Dim dt As DataTable = ClsVLCDataUploaderManual.GetQueryDT("Manual", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"), ddlShift.Text, fndMCC_Code.Value, "", fndVLCCode.Value, FNDRouteCode.Value, "", "")
        gvManualCollectHist.DataSource = Nothing
        gvManualCollectHist.DataSource = dt
        gvManualCollectHist.ReadOnly = True
        gvManualCollectHist.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("VLC_Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("VLC_Fat", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("VLC_SNF", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("fat_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("VLC_Water", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Amount", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gvManualCollectHist.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        For col As Integer = 0 To gvManualCollectHist.Columns.Count - 1
            gvManualCollectHist.Columns(col).Width = 100
        Next
        gvManualCollectHist.EnableFiltering = True
        gvManualCollectHist.ShowFilteringRow = True
        'gvManualCollectHist.BestFitColumns()
    End Sub
    Sub LoadVLCDataPDU()
        Dim dt As DataTable = ClsVLCDataUploaderManual.GetQueryDT("PDU", clsCommon.GetPrintDate(txtdate.Value, "dd/MMM/yyyy"), ddlShift.Text, fndMCC_Code.Value, "", fndVLCCode.Value, FNDRouteCode.Value, "", "")
        gvPDCollectHist.DataSource = Nothing
        gvPDCollectHist.DataSource = dt
        gvPDCollectHist.ReadOnly = True
        gvPDCollectHist.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("VLC_Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("VLC_Fat", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("VLC_SNF", "{0:F3}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("fat_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("VLC_Water", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Amount", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gvPDCollectHist.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gvPDCollectHist.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

        For col As Integer = 0 To gvPDCollectHist.Columns.Count - 1
            gvPDCollectHist.Columns(col).Width = 100
        Next

        gvPDCollectHist.EnableFiltering = True
        gvPDCollectHist.ShowFilteringRow = True
        'gvPDCollectHist.BestFitColumns()
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click

        DeleteData()


    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub gvItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
        If Not IsinsideLoadData Then
            If gvItem.RowCount > 0 Then
                Dim intCurrRow As Integer = gvItem.CurrentRow.Index
                If intCurrRow = gvItem.Rows.Count - 1 Then
                    gvItem.Rows.AddNew()
                    gvItem.CurrentRow = gvItem.Rows(intCurrRow)
                    Dim k As Integer = 1
                    For i As Integer = 0 To gvItem.Rows.Count - 1
                        gvItem.Rows(i).Cells(colSlNo).Value = k
                        k = k + 1
                    Next
                End If
            End If
        End If
    End Sub
    ''============================================Preeti Gupta======================
    Public Sub funPrint(ByVal strDocNo As String)

        Try

            Dim Qry As String = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1  as Comp_ADD1,TSPL_COMPANY_MASTER.Add2 as Comp_ADD2 ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,"

            Qry += " TSPL_VLC_DATA_UPLOADER_MASTER.Shift ,TSPL_COMPANY_MASTER.Add3 as Comp_ADD3 ,TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME  else '' end +   "
            Qry += "  case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  "
            Qry += "  as Comp_address,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code ,convert(varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103)  as Document_Date, TSPL_VLC_DATA_UPLOADER_MASTER	.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name  ,TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code ,TSPL_MCC_ROUTE_MASTER.Route_Name ,"


            Qry += " TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code ,TSPL_MP_MASTER.MP_Name ,TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer ,TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code  from TSPL_VLC_DATA_UPLOADER_DETAIL"
            Qry += " left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code =TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code "
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code "
            Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_COMPANY_MASTER.City_Code "
            Qry += " left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State "
            Qry += " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code= TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code "
            Qry += " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code "
            Qry += " LEFT OUTER JOIN TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_Code =TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  "
            Qry += "   where  TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code ='" + strDocNo + "'"



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptVillageData", "Village Data")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndDocumentNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Invoice not found to Print", Me.Text)
        Else
            funPrint(fndDocumentNo.Value)
        End If
    End Sub
    Sub GridFocus(Optional ByVal gvRow As GridViewRowInfo = Nothing)
        'If gvItem.Rows.Count > 0 Then
        '    gvItem.Focus()
        '    gvItem.CurrentColumn = gvItem.Columns(colFarmerCode_VlcUploader)
        '    If Not gvRow Is Nothing Then
        '        gvItem.CurrentRow = gvItem.Rows(gvRow.Index + 1)
        '    Else
        '        If gvItem.CurrentRow Is Nothing Then
        '            gvItem.CurrentRow = gvItem.Rows(gvItem.Rows.Count - 1)
        '        End If
        '    End If


        '    gvItem.PerformLayout()
        '    gvItem.BeginEdit()
        '    'gvItem.CurrentCell.Value = ""
        '    gvItem.EndEdit()
        'Else
        '    gvItem.Rows.AddNew()
        '    gvItem.CurrentColumn = gvItem.Columns(colFarmerCode_VlcUploader)
        '    gvItem.CurrentRow = gvItem.Rows(gvItem.Rows.Count - 1)
        '    gvItem.PerformLayout()
        '    gvItem.BeginEdit()
        'End If
    End Sub

    'Private Sub fndVLCCode_KeyDown(sender As Object, e As KeyEventArgs) Handles fndVLCCode.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        GridFocus()
    '    End If
    'End Sub

    Private Sub fndVLCCode_Leave(sender As Object, e As EventArgs) Handles fndVLCCode.Leave
        GridFocus()
        If clsCommon.myLen(fndVLCCode.Value) > 0 Then
            LockControlAfterSelectAllFinders(False)
        End If
    End Sub

    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gvItem.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If gvItem.CurrentCell Is Nothing Then
        '        Exit Sub
        '    End If
        '    If (gvItem.CurrentCell.ColumnInfo.Name = colSNFPer Or gvItem.CurrentCell.ColumnInfo.Name = colRate Or gvItem.CurrentCell.ColumnInfo.Name = colAmount) Then
        '        GridFocus(gvItem.CurrentRow)
        '    ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colFarmerCode_VlcUploader) Then
        '        gvItem.EndEdit()
        '        gvItem.CurrentColumn = gvItem.Columns(colQty)
        '        gvItem.BeginEdit()
        '    ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colFarmerCode_VlcUploader Or gvItem.CurrentCell.ColumnInfo.Name = colFarmerName Or gvItem.CurrentCell.ColumnInfo.Name = colUOM Or gvItem.CurrentCell.ColumnInfo.Name = colQty Or gvItem.CurrentCell.ColumnInfo.Name = colFatPer) Then
        '        gvItem.EndEdit()
        '        gvItem.CurrentColumn = gvItem.Columns(gvItem.CurrentCell.ColumnInfo.Index + 1)
        '        gvItem.BeginEdit()
        '    End If
        'End If
    End Sub

    'Private Sub txtdate_LostFocus(sender As Object, e As EventArgs) Handles txtdate.LostFocus
    '    LoadHistryData()
    'End Sub

    'Private Sub ddlShift_LostFocus(sender As Object, e As EventArgs) Handles ddlShift.LostFocus
    '    LoadHistryData()
    'End Sub

    Private Sub btnHistrory_Click(sender As Object, e As EventArgs) Handles btnHistrory.Click
        LoadHistryData()
    End Sub

    Private Sub fndMCC_Code__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCC_Code._MYValidating
        fndMCC_Code.Value = clsMccMaster.getFinder("TSPL_MCC_MASTER.MCC_CODE in (" + arrLoc + ")", fndMCC_Code.Value, isButtonClicked)
        Try
            SetDocKCollectionMilkType(fndMCC_Code.Value)
        Catch ex As Exception
        End Try
        lblMCCDesc.Text = clsDBFuncationality.getSingleValue("Select MCC_NAME from TSPL_MCC_MASTER where MCC_CODE ='" + fndMCC_Code.Value + "' ")
        FNDRouteCode.Tag = fndMCC_Code.Value
        Mcc_Uom = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & fndMCC_Code.Value & "'")
        'fndVLCCode.Select()
        'fndVLCCode.Focus()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim gvItem As New RadGridView()
        Me.Controls.Add(gvItem)
        Try
            If transportSql.importExcel(gvItem, "ID", "vlc_code", "milk_qty", "fatper", "snfper", "collection_date", "shift", "farmer_id", "TFS", "fatkg", "snfkg", "route_code", "mcc_code", "Milk_Type", "Flag") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim line As Integer = 1
                Try
                    Dim qry As String
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gvItem.Rows
                        Dim obj As New ClsVLCDataUploaderManual
                        obj.Document_Date = clsCommon.myCDate(grow.Cells("collection_date").Value)

                        obj.VLC_Code = clsCommon.myCstr(grow.Cells("vlc_code").Value)
                        qry = "select VLC_Code,Route_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code='" + obj.VLC_Code + "' "
                        Dim dtResult As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtResult Is Nothing OrElse dtResult.Rows.Count <= 0 Then
                            Throw New Exception("VLC Master not exists for VLC Uploader no" + obj.VLC_Code)
                        End If
                        obj.VLC_Code = clsCommon.myCstr(dtResult.Rows(0)("VLC_Code"))
                        If clsCommon.myLen(obj.VLC_Code) <= 0 Then
                            Throw New Exception("VLC Code not exists for VLC Uploader no" + obj.VLC_Code)
                        End If

                        obj.Route_Code = clsCommon.myCstr(dtResult.Rows(0)("Route_Code"))
                        If clsCommon.myLen(obj.Route_Code) <= 0 Then
                            Throw New Exception("VLC's Route not exists for VLC Uploader no" + obj.VLC_Code)
                        End If
                        obj.Shift = clsCommon.myCstr(grow.Cells("shift").Value)

                        Dim objTR As New ClsVLCDataUploaderManualdetail
                        objTR.Farmer_Code = clsCommon.myCstr(grow.Cells("farmer_id").Value)
                        qry = "select MP_Code from TSPL_MP_MASTER where MP_Code ='" + objTR.Farmer_Code + "'"
                        'MP_Code_VLC_Uploader='" + objTR.Farmer_Code + "' and VLC_Code='" + obj.VLC_Code + "'"
                        objTR.Farmer_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(objTR.Farmer_Code) <= 0 Then
                            Throw New Exception("Producer Master not exists for MP Uploader no" + objTR.Farmer_Code)
                        End If

                        objTR.Qty = clsCommon.myCdbl(grow.Cells("milk_qty").Value)
                        objTR.FatPer = clsCommon.myCdbl(grow.Cells("fatper").Value)
                        objTR.SNFPer = clsCommon.myCdbl(grow.Cells("snfper").Value)
                        qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where MCC_CODE='" + clsCommon.myCstr(dtResult.Rows(0)("MCC")) + "' and Stocking_Unit='Y'"
                        objTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        objTR.Rate = clsEkoPro.getRateFromUploaderShiftWise(objTR.FatPer, objTR.SNFPer, clsCommon.myCstr(dtResult.Rows(0)("MCC")), obj.VLC_Code, IIf(clsCommon.myCstr(obj.Shift).ToString.Contains("M"), "M", "E"), clsCommon.myCDate(obj.Document_Date), trans, clsCommon.myCstr(grow.Cells("Milk_Type").Value), settBennyImportPickRateFromPrice)
                        objTR.Amount = Math.Round(objTR.Qty * objTR.Rate, 2, MidpointRounding.ToEven)

                        SaveImportData(obj, objTR, trans)
                        line += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Line No " + clsCommon.myCstr(line) + "" + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvItem)
        End Try
    End Sub

    Public Shared Function SaveImportData(ByVal obj As ClsVLCDataUploaderManual, ByVal objTR As ClsVLCDataUploaderManualdetail, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            qry = "Select TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.Mp_Code_vlc_Uploader as Uploader_Code,TSPL_MP_MASTER.MP_Name,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader
from TSPL_MP_MASTER 
left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.vlc_Code=TSPL_MP_MASTER.VLC_Code  
Where 2=2 and TSPL_MP_MASTER.VLC_Code<>'" + obj.VLC_Code + "' and MP_Code in('" + objTR.Farmer_Code + "')  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim str As String = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    str += "MP [" + clsCommon.myCstr(dt.Rows(ii)("Uploader_Code")) + "][ " + clsCommon.myCstr(dt.Rows(ii)("MP_Code")) + "][" + clsCommon.myCstr(dt.Rows(ii)("MP_Name")) + "] is not related To VLC [" + obj.VLC_Code + "]"
                Next
                Throw New Exception(str)
            End If

            qry = "select Document_Code from TSPL_VLC_DATA_UPLOADER_MASTER where convert(date, Document_Date,103)='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' and VLC_Code='" + obj.VLC_Code + "' and Route_Code='" + obj.Route_Code + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Shift ='" + obj.Shift + "' "
            obj.Document_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            Dim coll As New Hashtable()
            If clsCommon.myLen(obj.Document_Code) <= 0 Then
                Dim mcc_Code As String = clsDBFuncationality.getSingleValue("select location_Code from tspl_mcc_route_master inner join tspl_location_master on location_Code=mcc_Code where route_Code='" & obj.Route_Code & "'", trans)
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.VLCDataUploaderManual, "", mcc_Code)

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_DATA_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            End If

            qry = "delete from TSPL_VLC_DATA_UPLOADER_DETAIL where Document_Code='" + obj.Document_Code + "' and Farmer_Code='" + objTR.Farmer_Code + "' and Qty='" + clsCommon.myCstr(objTR.Qty) + "' and FatPer='" + clsCommon.myCstr(objTR.FatPer) + "' and SNFPer='" + clsCommon.myCstr(objTR.SNFPer) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", objTR.Farmer_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", objTR.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
            clsCommon.AddColumnsForChange(coll, "FatPer", objTR.FatPer)
            clsCommon.AddColumnsForChange(coll, "SNFPer", objTR.SNFPer)
            clsCommon.AddColumnsForChange(coll, "Reject_Type", objTR.Reject_Type, True)
            clsCommon.AddColumnsForChange(coll, "Rate", objTR.Rate)
            clsCommon.AddColumnsForChange(coll, "Amount", objTR.Amount)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_DATA_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Private Sub rmimport_Click(sender As Object, e As EventArgs) Handles rmimport.Click

    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click

    End Sub
    Public Sub funExportGrid()
        Try
            Dim strCmd As String = ""
            If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                strCmd = " select TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Code],TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as [Route Code],TSPL_VLC_DATA_UPLOADER_MASTER.Shift,TSPL_MP_MASTER.MP_Code_VLC_Uploader as [Farmer Code],Qty,FatPer as [Fat %],SNFPer as [SNF %],Unit_Code as Unit,Rate,TSPL_VLC_DATA_UPLOADER_DETAIL.Reject_Type as [Reject Type] from TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code Left Outer Join TSPL_MP_MASTER On TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code=TSPL_MP_MASTER .MP_Code"
            Else
                strCmd = " select TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code as [VLC Code],TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as [Route Code],TSPL_VLC_DATA_UPLOADER_MASTER.Shift,farmer_code as [Farmer Code],Qty,FatPer as [Fat %],SNFPer as [SNF %],Unit_Code as Unit,Rate,TSPL_VLC_DATA_UPLOADER_DETAIL.Reject_Type as [Reject Type] from TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code "
            End If

            Dim whr As String = ""
            If clsCommon.myLen(fndDocumentNo.Value) > 0 Then
                whr += " and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code='" + fndDocumentNo.Value + "' "
            End If


            transportSql.ExporttoExcel(strCmd, whr, "", Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Village Data")
        End Try
    End Sub
    Public Sub funExport(ByVal ManualRate As Boolean)
        Try
            Dim strCmd As String = ""
            If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                strCmd = " select convert(varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as [Document Date],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Code],TSPL_VLC_DATA_UPLOADER_MASTER.Shift,TSPL_MP_MASTER.MP_Code_VLC_Uploader as [Farmer Code],Qty as [Milk Qty],FatPer as [Fat %],SNFPer as [SNF %],TSPL_VLC_DATA_UPLOADER_DETAIL.Reject_Type as [Reject Type] " & If(ManualRate, ",Rate", "") & " from TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code left outer Join TSPL_MP_MASTER On TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code=TSPL_MP_MASTER .MP_Code"
            Else
                strCmd = " select convert(varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as [Document Date],TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code as [VLC Code],TSPL_VLC_DATA_UPLOADER_MASTER.Shift,farmer_code as [Farmer Code],Qty as [Milk Qty],FatPer as [Fat %],SNFPer as [SNF %],TSPL_VLC_DATA_UPLOADER_DETAIL.Reject_Type as [Reject Type] " & If(ManualRate, ",Rate", "") & " from TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code "
            End If
            transportSql.ExporttoExcel(strCmd, "", "", Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Village Data")
        End Try
    End Sub


    Sub LockControlAfterSelectAllFinders(ByVal chk As Boolean)
        txtdate.Enabled = chk
        ddlShift.Enabled = chk
        fndMCC_Code.Enabled = chk
        fndVLCCode.Enabled = chk
    End Sub
    Sub SetDocKCollectionMilkType(ByVal strMCCCode As String)
        If clsCommon.myLen(strMCCCode) > 0 Then
            Dim qry As String = "select Is_Seprate_Dock_Cow_Buffalo from TSPL_MCC_MASTER where MCC_Code='" + strMCCCode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                Dim frm As New FrmFreeComboBox()
                frm.ComboSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False)
                frm.ComboValueMember = "Code"
                frm.ComboDisplayMember = "Name"
                frm.isAcceptOKOnlyMandatory = True
                frm.ShowDialog()
                cboDockCollectionMilkType.SelectedValue = frm.strRetValue
            Else
                cboDockCollectionMilkType.SelectedValue = "M"
            End If
            cboDockCollectionMilkType.Enabled = False
        End If
    End Sub



    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        ImportData(True)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "MCC Code", "VLC Code", "Shift", "Farmer Code", "Qty", "Fat %", "SNF %", "Unit", "Rate", "Reject Type") Then
            Try
                clsCommon.ProgressBarShow()
                Dim line As Integer = 0
                Dim counter As Integer = 0
                Dim firstMCC As String = Nothing
                Dim firstVLC As String = Nothing
                Dim firstShift As String = Nothing
                Dim obj As New ClsVLCDataUploaderManual()
                Dim objTr1 As New ClsVLCDataUploaderManualdetail
                obj.arrVLCDetail = New List(Of ClsVLCDataUploaderManualdetail)

                For Each grow As GridViewRowInfo In gv.Rows
                    objTr1 = New ClsVLCDataUploaderManualdetail()

                    If counter = 0 Then
                        firstMCC = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        firstVLC = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                        'firstShift = clsCommon.myCstr(grow.Cells("Shift").Value)
                        firstShift = IIf(clsCommon.myCstr(grow.Cells("Shift").Value).ToUpper().Contains("M"), "MORNING", "EVENING")
                    End If


                    obj.MCC_Code = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                        Throw New Exception("Fill MCC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    obj.VLC_Code = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                    If clsCommon.myLen(obj.VLC_Code) <= 0 Then
                        Throw New Exception("Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    obj.Shift = clsCommon.myCstr(grow.Cells("Shift").Value)
                    If clsCommon.myLen(obj.Shift) <= 0 Then
                        'ddlShift.Focus()
                        Throw New Exception("Fill Shift At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    obj.Shift = IIf(clsCommon.myCstr(obj.Shift).ToUpper().Contains("M"), "MORNING", "EVENING")

                    ''--------------- Validation Check for MCC, VLC and Shift

                    If counter >= 1 Then
                        If clsCommon.CompairString(obj.MCC_Code, firstMCC) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Fill MCC Code Unique.")
                        End If
                        If clsCommon.CompairString(obj.VLC_Code, firstVLC) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Fill VLC Code Unique.")
                        End If
                        If clsCommon.CompairString(obj.Shift, firstShift) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Fill Shift Unique.")
                        End If
                    End If

                    ''--------------------End
                    Dim VLC_Code_vlc_Uploader As String = ""
                    If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                        VLC_Code_vlc_Uploader = obj.VLC_Code
                        If clsCommon.myLen(obj.MCC_Code) > 0 Then
                            Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [VLC Code],VLC.VLC_Name as [VLC Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name  from TSPL_VLC_MASTER_HEAD VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code  where 2=2 and  VLC.MCC='" & obj.MCC_Code & "' AND VLC.VLC_Code_vlc_Uploader='" & obj.VLC_Code & "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                obj.VLC_Code = clsCommon.myCstr(dt.Rows(0).Item("VLC Code"))
                                obj.VLC_NAME = clsCommon.myCstr(dt.Rows(0).Item("VLC Name"))
                                obj.Route_Code = clsCommon.myCstr(dt.Rows(0).Item("Route Code"))
                                LblRouteName.Text = clsCommon.myCstr(dt.Rows(0).Item("Route_Name"))
                            Else
                                Throw New Exception("Correct Mapping MCC Code and VLC code ")
                            End If
                        Else
                            fndMCC_Code.Focus()
                            Throw New Exception("Fill MCC Code ")
                        End If
                    Else
                        VLC_Code_vlc_Uploader = clsDBFuncationality.getSingleValue("select VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD where VLC_Code='" & obj.VLC_Code & "'")

                        If clsCommon.myLen(obj.MCC_Code) > 0 Then
                            Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [VLC Code],VLC.VLC_Name as [VLC Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name  from TSPL_VLC_MASTER_HEAD VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code  where 2=2 and  VLC.MCC='" & obj.MCC_Code & "' AND VLC.VLC_Code_vlc_Uploader='" & VLC_Code_vlc_Uploader & "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                obj.VLC_Code = clsCommon.myCstr(dt.Rows(0).Item("VLC Code"))
                                obj.VLC_NAME = clsCommon.myCstr(dt.Rows(0).Item("VLC Name"))
                                obj.Route_Code = clsCommon.myCstr(dt.Rows(0).Item("Route Code"))
                                LblRouteName.Text = clsCommon.myCstr(dt.Rows(0).Item("Route_Name"))
                            Else
                                Throw New Exception("Correct Mapping MCC Code and VLC code ")
                            End If
                        Else
                            fndMCC_Code.Focus()
                            Throw New Exception("Fill MCC Code ")
                        End If
                    End If

                    objTr1.Farmer_Code = clsCommon.myCstr(grow.Cells("Farmer Code").Value)
                    If clsCommon.myLen(objTr1.Farmer_Code) <= 0 Then
                        Throw New Exception("Fill Farmer Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim MP_Code_vlc_Uploader As String = ""
                    If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                        MP_Code_vlc_Uploader = objTr1.Farmer_Code
                        If clsCommon.myLen(objTr1.Farmer_Code) <= 0 Then
                            Throw New Exception("Fill Farmer Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        objTr1.Farmer_Code = objTr1.Farmer_Code

                        objTr1.Farmer_Name = clsDBFuncationality.getSingleValue("Select MP_Name as [Farmer Name] from TSPL_MP_MASTER inner join tspl_vlc_master_Head on tspl_vlc_master_Head.vlc_Code=TSPL_MP_MASTER.vlc_Code  where MP_Code_vlc_Uploader='" & MP_Code_vlc_Uploader & "'  and  MCC=(select mcc_Code from tspl_mcc_route_master where route_Code='" & obj.Route_Code & "') and vlc_code_vlc_uploader='" & VLC_Code_vlc_Uploader & "'")
                    Else

                        MP_Code_vlc_Uploader = clsDBFuncationality.getSingleValue("select MP_Code_VLC_Uploader from TSPL_MP_MASTER where MP_Code='" & objTr1.Farmer_Code & "'")
                        'obj.Route_Code = clsCommon.myCstr(grow.Cells("Route Code").Value)
                        If clsCommon.myLen(objTr1.Farmer_Code) <= 0 Then
                            Throw New Exception("Fill Farmer Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        objTr1.Farmer_Code = clsDBFuncationality.getSingleValue("Select MP_Code_VLC_Uploader as [Farmer Name] from TSPL_MP_MASTER inner join tspl_vlc_master_Head on tspl_vlc_master_Head.vlc_Code=TSPL_MP_MASTER.vlc_Code  where MP_Code_vlc_Uploader='" & MP_Code_vlc_Uploader & "'  and  MCC=(select mcc_Code from tspl_mcc_route_master where route_Code='" & obj.Route_Code & "') and vlc_code_vlc_uploader='" & VLC_Code_vlc_Uploader & "'")

                        objTr1.Farmer_Name = clsDBFuncationality.getSingleValue("Select MP_Name as [Farmer Name] from TSPL_MP_MASTER inner join tspl_vlc_master_Head on tspl_vlc_master_Head.vlc_Code=TSPL_MP_MASTER.vlc_Code  where MP_Code_vlc_Uploader='" & MP_Code_vlc_Uploader & "'  and  MCC=(select mcc_Code from tspl_mcc_route_master where route_Code='" & obj.Route_Code & "') and vlc_code_vlc_uploader='" & VLC_Code_vlc_Uploader & "'")
                    End If


                    objTr1.Reject_Type = clsCommon.myCstr(grow.Cells("Reject Type").Value)
                    If clsCommon.myLen(objTr1.Reject_Type) > 0 Then
                        Dim qry As String = "select code from TSPL_MILK_REJECT_TYPE  where code='" + objTr1.Reject_Type + "'"
                        objTr1.Reject_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(objTr1.Reject_Type) <= 0 Then
                            Throw New Exception("Invalid Rejection Type [" + clsCommon.myCstr(grow.Cells("Reject Type").Value) + "]")
                        End If
                    End If


                    objTr1.Qty = clsCommon.myCdbl(grow.Cells("Qty").Value)

                    objTr1.Unit_Code = clsCommon.myCstr(grow.Cells("Unit").Value)
                    If clsCommon.myLen(objTr1.Unit_Code) <= 0 Then
                        Throw New Exception("Fill Unit Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    objTr1.FatPer = clsCommon.myCdbl(grow.Cells("Fat %").Value)
                    objTr1.SNFPer = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                    objTr1.Rate = clsCommon.myCdbl(grow.Cells("Rate").Value)

                    obj.arrVLCDetail.Add(objTr1)
                    counter += 1
                    clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                Next

                ''--- Show in Grid
                If obj.arrVLCDetail IsNot Nothing AndAlso obj.arrVLCDetail.Count > 0 Then
                    fndMCC_Code.Value = obj.MCC_Code
                    fndVLCCode.Tag = obj.VLC_Code
                    LblVLCName.Text = obj.VLC_NAME
                    FNDRouteCode.Value = obj.Route_Code
                    ddlShift.Text = obj.Shift
                    fndVLCCode.Value = clsDBFuncationality.getSingleValue("Select VLC_code_vlc_Uploader from TSPL_VLC_MASTER_HEAD where VLC_Code ='" + fndVLCCode.Tag + "' and " _
                     & " mcc=(select mcc_Code from TSPL_MCC_ROUTE_MASTER where route_Code='" & clsCommon.myCstr(FNDRouteCode.Value) & "')")

                    For Each objTr As ClsVLCDataUploaderManualdetail In obj.arrVLCDetail
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode).Value = objTr.Farmer_Code
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerCode_VlcUploader).Value = objTr.Farmer_Code
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFarmerName).Value = objTr.Farmer_Name
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.Unit_Code
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
                        gvItem.Rows.AddNew()
                    Next
                    ' gvItem.Rows.RemoveAt(gvItem.Rows.Count - 1)
                Else
                    gvItem.DataSource = Nothing
                End If
                ''------------
                clsCommon.ProgressBarHide()

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        ImportData(False)
    End Sub
    Function ImportData(ByVal ManualRate As Boolean) As Boolean
        '' done by Panch Raj against ticket no: ERO/08/06/18-000339 on 11-06-2018
        Dim gvItem As New RadGridView()
        Me.Controls.Add(gvItem)
        Try
            Dim importcol As String = ""
            If If(ManualRate, transportSql.importExcel(gvItem, "Document Date", "VLC Code", "Shift", "Farmer Code", "Milk Qty", "Fat %", "SNF %", "Reject Type", "Rate"), transportSql.importExcel(gvItem, "Document Date", "VLC Code", "Shift", "Farmer Code", "Milk Qty", "Fat %", "SNF %", "Reject Type")) Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim line As Integer = 1
                Try
                    Dim qry As String
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gvItem.Rows
                        Dim obj As New ClsVLCDataUploaderManual
                        obj.Document_Date = clsCommon.myCDate(grow.Cells("Document Date").Value)
                        Dim dtResult As DataTable = Nothing
                        If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                            obj.VLC_Code = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                            qry = "select VLC_Code,Route_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + obj.VLC_Code + "' "
                            dtResult = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtResult Is Nothing OrElse dtResult.Rows.Count <= 0 Then
                                Throw New Exception("VLC Master not exists for VLC Uploader no" + obj.VLC_Code)
                            End If
                            obj.VLC_Code = clsCommon.myCstr(dtResult.Rows(0)("VLC_Code"))
                            If clsCommon.myLen(obj.VLC_Code) <= 0 Then
                                Throw New Exception("VLC Code not exists for VLC Uploader no" + obj.VLC_Code)
                            End If
                        Else
                            obj.VLC_Code = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                            qry = "select VLC_Code,Route_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code='" + obj.VLC_Code + "' "
                            dtResult = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtResult Is Nothing OrElse dtResult.Rows.Count <= 0 Then
                                Throw New Exception("VLC Master not exists for VLC Uploader no" + obj.VLC_Code)
                            End If
                            obj.VLC_Code = clsCommon.myCstr(dtResult.Rows(0)("VLC_Code"))
                            If clsCommon.myLen(obj.VLC_Code) <= 0 Then
                                Throw New Exception("VLC Code not exists for VLC Uploader no" + obj.VLC_Code)
                            End If
                        End If

                        obj.Route_Code = clsCommon.myCstr(dtResult.Rows(0)("Route_Code"))
                        If clsCommon.myLen(obj.Route_Code) <= 0 Then
                            Throw New Exception("VLC's Route not exists for VLC Uploader no" + obj.VLC_Code)
                        End If
                        obj.Shift = clsCommon.myCstr(grow.Cells("Shift").Value)
                        If clsCommon.myLen(obj.Shift) <= 0 Then
                            Throw New Exception("Shift not exists for VLC Uploader no" + obj.VLC_Code)
                        End If
                        obj.Shift = IIf(clsCommon.myCstr(obj.Shift).ToUpper().Contains("M"), "MORNING", "EVENING")

                        obj.Dock_Collection_Milk_Type = cboDockCollectionMilkType.SelectedValue ''clsCommon.myCstr(grow.Cells("Milk Type").Value)

                        Dim objTR As New ClsVLCDataUploaderManualdetail
                        objTR.Farmer_Code = clsCommon.myCstr(grow.Cells("Farmer Code").Value)
                        If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                            qry = "select MP_Code from TSPL_MP_MASTER where MP_Code_VLC_Uploader ='" + objTR.Farmer_Code + "' and VLC_Code='" + obj.VLC_Code + "'"
                        Else
                            qry = "select MP_Code from TSPL_MP_MASTER where MP_Code ='" + objTR.Farmer_Code + "'"
                        End If

                        objTR.Farmer_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(objTR.Farmer_Code) <= 0 Then
                            Throw New Exception("Producer Master not exists for MP Uploader no" + objTR.Farmer_Code)
                        End If

                        objTR.Reject_Type = clsCommon.myCstr(grow.Cells("Reject Type").Value)
                        If clsCommon.myLen(objTR.Reject_Type) > 0 Then
                            qry = "select code from TSPL_MILK_REJECT_TYPE  where code='" + objTR.Reject_Type + "'"
                            objTR.Reject_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(objTR.Reject_Type) <= 0 Then
                                Throw New Exception("Invalid Rejection Type [" + clsCommon.myCstr(grow.Cells("Reject Type").Value) + "]")
                            End If
                        End If

                        objTR.Qty = clsCommon.myCdbl(grow.Cells("Milk Qty").Value)
                        objTR.FatPer = clsCommon.myCdbl(grow.Cells("Fat %").Value)
                        objTR.SNFPer = clsCommon.myCdbl(grow.Cells("SNF %").Value)


                        qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where MCC_CODE='" + clsCommon.myCstr(dtResult.Rows(0)("MCC")) + "' and Stocking_Unit='Y'"
                        objTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If ManualRate Then
                            objTR.Rate = clsCommon.myCdbl(grow.Cells("Rate").Value)
                        Else
                            objTR.Rate = clsEkoPro.getRateFromUploaderShiftWise(objTR.FatPer, objTR.SNFPer, clsCommon.myCstr(dtResult.Rows(0)("MCC")), obj.VLC_Code, IIf(clsCommon.myCstr(obj.Shift).ToString.Contains("M"), "M", "E"), clsCommon.myCDate(obj.Document_Date), trans, obj.Dock_Collection_Milk_Type, settBennyImportPickRateFromPrice)
                            If clsCommon.myLen(objTR.Reject_Type) > 0 Then
                                Dim objMRT As clsMilkRejectType = clsMilkRejectType.GetData(objTR.Reject_Type, NavigatorType.Current, trans)
                                If objMRT Is Nothing Then
                                    Throw New Exception("Invalid rejection type [" + objTR.Reject_Type + "]")
                                End If
                                objTR.Rate = Math.Round(objTR.Rate * objMRT.Applicable_Per / 100, 2, MidpointRounding.ToEven)
                            End If
                        End If

                        objTR.Amount = Math.Round(objTR.Qty * objTR.Rate, 2, MidpointRounding.ToEven)

                        SaveImportData(obj, objTR, trans)
                        line += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Line No " + clsCommon.myCstr(line) + "" + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvItem)
        End Try
    End Function
    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        funExportGrid()
    End Sub

    Private Sub RadMenuItem7_Click(sender As Object, e As EventArgs) Handles RadMenuItem7.Click
        funExport(False)
    End Sub

    Private Sub RadMenuItem8_Click(sender As Object, e As EventArgs) Handles RadMenuItem8.Click
        funExport(True)
    End Sub

    Private Sub TxtMultiSelectFinder3__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder3._My_Click
        Dim qry As String = "Select TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code as Code,Convert(varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Date, " + Environment.NewLine +
             "TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC NAME],TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as [Route Code], " + Environment.NewLine +
             "TSPL_MCC_ROUTE_MASTER.Route_Name as [Route Name],TSPL_VLC_DATA_UPLOADER_MASTER.Shift,TSPL_VLC_MASTER_HEAD.MCC AS [MCC CODE],TSPL_MCC_MASTER.MCC_NAME AS [MCC NAME] from TSPL_VLC_DATA_UPLOADER_MASTER " + Environment.NewLine +
             "Left Outer Join TSPL_MCC_ROUTE_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code= TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine +
             "Left Outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code " + Environment.NewLine +
             "Left Outer Join TSPL_MCC_MASTER on TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_CODE " + Environment.NewLine +
             "where not exists(select 1  from TSPL_LOCK_MP_PC where  Convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103)  between Cast(TSPL_LOCK_MP_PC.FROM_DATE as Date)   and Cast(TSPL_LOCK_MP_PC.TO_DATE as Date) and TSPL_LOCK_MP_PC.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC And TSPL_LOCK_MP_PC.POSTED = 'Y')"
        TxtMultiSelectFinder3.arrValueMember = clsCommon.ShowMultipleSelectForm("MVLDU@Mul", qry, "Code", "Code", TxtMultiSelectFinder3.arrValueMember, TxtMultiSelectFinder3.arrDispalyMember)
    End Sub

    Private Sub RadButton238_Click(sender As Object, e As EventArgs) Handles RadButton238.Click
        Try
            If TxtMultiSelectFinder3.arrValueMember Is Nothing OrElse TxtMultiSelectFinder3.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one document to delete")
            End If

            If (clsCommon.MyMessageBoxShow(Me, "Delete " + clsCommon.myCstr(TxtMultiSelectFinder3.arrValueMember.Count) + "Documents." + Environment.NewLine + "Are You sure?", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question)) = System.Windows.Forms.DialogResult.Yes Then
                If (ClsVLCDataUploaderManual.DeleteData(TxtMultiSelectFinder3.arrValueMember)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    TxtMultiSelectFinder3.arrValueMember = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub gvItem_CellValidating(sender As Object, e As CellValidatingEventArgs) Handles gvItem.CellValidating

    End Sub

    Private Sub gvItem_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvItem.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocus()
        If gvItem.CurrentCell IsNot Nothing Then
            If gvItem.CurrentCell.ColumnInfo.Name = colFarmerCode_VlcUploader Then
                gvItem.CurrentColumn = gvItem.Columns(colQty)
            ElseIf gvItem.CurrentCell.ColumnInfo.Name = colQty Then
                gvItem.CurrentColumn = gvItem.Columns(colFatPer)
            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colFatPer) Then
                gvItem.CurrentColumn = gvItem.Columns(colSNFPer)
            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colSNFPer) Then
                If gvItem.Rows.Count >= gvItem.CurrentRow.Index + 1 Then
                    gvItem.CurrentRow = gvItem.Rows(gvItem.CurrentRow.Index + 1)
                End If
                If clsCommon.myLen(gvItem.CurrentRow.Cells(colFarmerCode_VlcUploader).Value) > 0 Then
                    gvItem.CurrentColumn = gvItem.Columns(colQty)
                Else
                    gvItem.CurrentColumn = gvItem.Columns(colFarmerCode_VlcUploader)
                End If

            End If
        End If
    End Sub
End Class

'Class MyBehavior
'    Inherits BaseGridBehavior
'    Public Overrides Function ProcessKeyDown(keys__1 As KeyEventArgs) As Boolean
'        If keys__1.KeyData = Keys.Enter AndAlso Me.GridControl.IsInEditMode Then
'            Me.GridControl.GridNavigator.SelectNextColumn()
'        ElseIf keys__1.KeyData = Keys.Up AndAlso Me.GridControl.IsInEditMode Then
'            Me.GridControl.GridNavigator.SelectPreviousRow(1)
'        ElseIf keys__1.KeyData = Keys.Down AndAlso Me.GridControl.IsInEditMode Then
'            Me.GridControl.GridNavigator.SelectNextRow(1)
'        ElseIf keys__1.KeyData = Keys.Right AndAlso Me.GridControl.IsInEditMode Then
'            Me.GridControl.GridNavigator.SelectNextColumn()
'        ElseIf keys__1.KeyData = Keys.Left AndAlso Me.GridControl.IsInEditMode Then
'            Me.GridControl.GridNavigator.SelectPreviousColumn()
'        End If

'        Return True
'    End Function

'End Class