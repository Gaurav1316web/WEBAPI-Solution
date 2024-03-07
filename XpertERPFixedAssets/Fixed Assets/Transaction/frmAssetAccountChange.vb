Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Imports System.Data.SqlClient
'Create By Sanjay

Public Class frmAssetAccountChange
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim i As Integer

    Private IsFormLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colcheck As String = "colcheck"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colAssetID As String = "colAssetID"
    Const colAssetSpecificaion As String = "colAssetSpecificaion"
    Const colAssetName As String = "colAssetName"
    Const colAccountSetCode As String = "colAccountSetCode"
    Const colAccountSetName As String = "colAccountSetName"
    Const colNetAmt As String = "colNetAmt"
    Const colAccountCode As String = "colAccountCode"
    Const colAccountName As String = "colAccountName"
    Const colChangedAccountCode As String = "colChangedAccountCode"
    Const colChangedAccountName As String = "colChangedAccountName"
    Const colRemarks As String = "colRemarks"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ApplyFinancialCostCenter As Boolean = False
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colHierarchyName As String = "colHierarchyName"
    Const colHierarchyLevelNumber As String = "colHierarchyLevelNumber"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colCostCenterName As String = "colCostCenterName"
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False


#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FAAcquisitionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnReverse.Visible = False

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)

        RadPageView1.SelectedPage = RadPageViewPage1
        ApplyFinancialCostCenter = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False))
        LoadBlankGrid()

        IsFormLoad = True
        AddNew()
        IsFormLoad = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub


    Sub BlankAllControls()

        txtDocNo.Value = ""
        fndAssetGroup.Value = ""
        lblAssetGroupDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLocation.Value = ""
        lblLocation.Text = ""
        fndFAAccount.Value = ""
        txtAcquision.Value = ""
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repocheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocheck = New GridViewCheckBoxColumn()
        repocheck.FormatString = ""
        repocheck.HeaderText = "Save"
        repocheck.Name = colcheck
        repocheck.Width = 40
        repocheck.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repocheck)

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetID
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        repoAssetCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        Dim repoAssetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetName.FormatString = ""
        repoAssetName.HeaderText = "Asset Description"
        repoAssetName.Name = colAssetName
        repoAssetName.Width = 150
        repoAssetName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAssetName)

        Dim repoAssetSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetSpecification.FormatString = ""
        repoAssetSpecification.HeaderText = "Asset Specification"
        repoAssetSpecification.Name = colAssetSpecificaion
        repoAssetSpecification.Width = 200
        repoAssetSpecification.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAssetSpecification)

        '==================================================================================



        Dim repoHierarchyLevelCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelCode.FormatString = ""
        repoHierarchyLevelCode.HeaderText = "Hierarchy Level Code"
        repoHierarchyLevelCode.Name = colHierarchyCode
        repoHierarchyLevelCode.Width = 100
        repoHierarchyLevelCode.ReadOnly = False
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelCode.IsVisible = True
        Else
            repoHierarchyLevelCode.IsVisible = False
        End If

        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelCode)


        Dim repoHierarchyLevelName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelName.FormatString = ""
        repoHierarchyLevelName.HeaderText = "Hierarchy Level Name"
        repoHierarchyLevelName.Name = colHierarchyName
        repoHierarchyLevelName.Width = 150
        repoHierarchyLevelName.ReadOnly = True
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelName.IsVisible = True
        Else
            repoHierarchyLevelName.IsVisible = False
        End If

        repoHierarchyLevelName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelName)

        Dim repoHierarchyLevelNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelNumber.FormatString = ""
        repoHierarchyLevelNumber.HeaderText = "Hierarchy Level Number"
        repoHierarchyLevelNumber.Name = colHierarchyLevelNumber
        repoHierarchyLevelNumber.Width = 150
        repoHierarchyLevelNumber.ReadOnly = True
        repoHierarchyLevelNumber.IsVisible = False
        repoHierarchyLevelNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelNumber)

        Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterCode.FormatString = ""
        repoCostCenterCode.HeaderText = "Cost Center Code"
        repoCostCenterCode.Name = colCostCenterCode
        repoCostCenterCode.Width = 100
        repoCostCenterCode.ReadOnly = False
        repoCostCenterCode.IsVisible = ApplyFinancialCostCenter
        gv1.MasterTemplate.Columns.Add(repoCostCenterCode)

        Dim repoCostCenterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterName.FormatString = ""
        repoCostCenterName.HeaderText = "Cost Center"
        repoCostCenterName.Name = colCostCenterName
        repoCostCenterName.Width = 150
        repoCostCenterName.ReadOnly = True
        repoCostCenterName.IsVisible = ApplyFinancialCostCenter
        repoCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCostCenterName)


        '==================================================================================


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = Global.XpertERPFixedAssets.My.Resources.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = True
        repoICode.Width = 100
        repoICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoAccountSetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetCode.FormatString = ""
        repoAccountSetCode.HeaderText = "Account Set Code"
        repoAccountSetCode.Name = colAccountSetCode
        repoAccountSetCode.Width = 100
        repoAccountSetCode.ReadOnly = True
        repoAccountSetCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAccountSetCode)

        Dim repoAccountSetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetName.FormatString = ""
        repoAccountSetName.HeaderText = "Account Set"
        repoAccountSetName.Name = colAccountSetName
        repoAccountSetName.Width = 150
        repoAccountSetName.ReadOnly = True
        repoAccountSetName.IsVisible = True
        repoAccountSetName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoAccountSetName)

        'Account
        Dim repoAcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAcCode.FormatString = ""
        repoAcCode.HeaderText = "Account Code"
        repoAcCode.Name = colAccountCode
        repoAcCode.HeaderImage = Global.XpertERPFixedAssets.My.Resources.Resources.search4
        repoAcCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAcCode.Width = 150
        'repoAcCode.ReadOnly = false
        repoAcCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAcCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Account Description"
        repoACName.Name = colAccountName
        repoACName.Width = 200
        repoACName.ReadOnly = True
        repoACName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoACName)


        Dim repoChangedAccountSetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChangedAccountSetCode.FormatString = ""
        repoChangedAccountSetCode.HeaderText = "Changed Account Code"
        repoChangedAccountSetCode.Name = colChangedAccountCode
        repoChangedAccountSetCode.HeaderImage = Global.XpertERPFixedAssets.My.Resources.Resources.search4
        repoChangedAccountSetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoChangedAccountSetCode.Width = 150
        'repochangedAccountSetCode.ReadOnly = True
        repoChangedAccountSetCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoChangedAccountSetCode)

        Dim repoChangedAccountSetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChangedAccountSetName.FormatString = ""
        repoChangedAccountSetName.HeaderText = "Changed Account Description"
        repoChangedAccountSetName.Name = colChangedAccountName
        repoChangedAccountSetName.Width = 200
        repoChangedAccountSetName.ReadOnly = True
        repoChangedAccountSetName.IsVisible = True
        repoChangedAccountSetName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoChangedAccountSetName)

        Dim repoNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetAmt = New GridViewDecimalColumn()
        repoNetAmt.FormatString = ""
        repoNetAmt.HeaderText = "Net Amount"
        repoNetAmt.Name = colNetAmt
        repoNetAmt.WrapText = True
        repoNetAmt.Width = 80
        repoNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNetAmt)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        'repoChangedAccountSetName.ReadOnly = True
        repoRemarks.IsVisible = True
        repoRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoRemarks)



        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If (clsCommon.CompairString(e.Column.Name, colChangedAccountCode) = CompairStringResult.Equal) Then
                        OpenGLAccountChanged(False)

                    ElseIf (clsCommon.CompairString(e.Column.Name, colAccountCode) = CompairStringResult.Equal) Then
                        OpenGLAccount(False)
                    ElseIf e.Column Is gv1.Columns(colHierarchyCode) Then
                        OpenHierarchyCode(False)
                    ElseIf e.Column Is gv1.Columns(colCostCenterCode) Then

                        OpenCostCenterList(False)

                    End If

                    isCellValueChangedOpen = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
        gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Description,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
        gv1.CurrentRow.Cells(colCostCenterName).Value = ""
    End Sub

    Private Sub OpenCostCenterList(ByVal isButtonClick As Boolean)

        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
            If ApplyFinancialCostCenter = True Then
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("TSPL_COST_CENTRE_FINANCIAL@AEFinder", qry, "Code", " Hirerachy_Level = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "", isButtonClick)
                gv1.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cost_Center_Fin_Name  from TSPL_COST_CENTRE_FINANCIAL  where  Cost_Center_Fin_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'")) ' ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCostCenter).Value)
            Else
                gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                gv1.CurrentRow.Cells(colCostCenterName).Value = ""
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value), "A", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
        End If
    End Sub



    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
                gv1.Rows(i).Cells(colcheck).Value = True
            End If
        Next
    End Sub


    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()

        BlankAllControls()
        LoadBlankGrid()
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        isNewEntry = True
        btnSave.Text = "Save"

        btnSave.Enabled = True
        btnPost.Enabled = True

        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()


        fndAssetGroup.Value = Nothing
        Me.lblAssetGroupDesc.Text = ""

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti==================================
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If

        RefreshSNo()


        Dim arrAssetCode As New List(Of String)
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then
                    If arrAssetCode.Contains(clsCommon.myCstr(grow.Cells(colAssetID).Value).ToUpper()) Then
                        clsCommon.MyMessageBoxShow(Me, "Repeared Asset Code [" + (clsCommon.myCstr(grow.Cells(colAssetID).Value) + "]  at line no " & (grow.Index + 1) & "."))
                        Return False
                    Else
                        arrAssetCode.Add(clsCommon.myCstr(grow.Cells(colAssetID).Value).ToUpper())
                    End If
                End If
            End If
        Next
        arrAssetCode = Nothing
        '===added by shivani[BM00000007837]
        Dim Dt_Temp_Id As New DataTable
        Dt_Temp_Id.Columns.Add("Item_Code")
        Dt_Temp_Id.Columns.Add("Temp_Code")
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                If (clsCommon.myLen(grow.Cells(colAssetName).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Asset Description  cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
                If (clsCommon.myLen(grow.Cells(colAssetSpecificaion).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Asset Specification  cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
            End If

            If (clsCommon.myLen(grow.Cells(colAssetName).Value)) > 0 Then
                If (clsCommon.myCdbl(grow.Cells(colNetAmt).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Net Amount is  zero at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
            End If

        Next

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False, ChekBtnPost)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean, ByVal ChekBtnPost As Boolean) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsAssetAccountChangeHead()

                obj.Doc_Code = txtDocNo.Value
                obj.Doc_Date = txtDate.Value

                obj.Acquisition_Code = txtAcquision.Value
                obj.Loc_Code = txtLocation.Value

                obj.Arr = New List(Of clsAssetAccountChangeDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                        Dim objTr As New clsAssetAccountChangeDetail()
                        objTr.Doc_Code = obj.Doc_Code
                        objTr.SNo = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        objTr.Asset_Name = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                        objTr.Asset_Specification = clsCommon.myCstr(grow.Cells(colAssetSpecificaion).Value)
                        objTr.AcSet_Code = clsCommon.myCstr(grow.Cells(colAccountSetCode).Value)
                        objTr.AcSet_Code_Name = clsCommon.myCstr(grow.Cells(colAccountSetName).Value)
                        objTr.Ac_Code = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
                        objTr.Ac_Name = clsCommon.myCstr(grow.Cells(colAccountName).Value)
                        objTr.ChangedAc_Code = clsCommon.myCstr(grow.Cells(colChangedAccountCode).Value)
                        objTr.ChangedAc_Name = clsCommon.myCstr(grow.Cells(colChangedAccountName).Value)

                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Name = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colNetAmt).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        'If clsCommon.myLen(objTr.Asset_Name) > 0 Or clsCommon.myLen(objTr.Asset_Code) > 0 Or clsCommon.myLen(obj.SRN_No) > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)) > 0 Then
                            objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)) > 0 Then
                            objTr.CostCenter_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                        End If



                        obj.Arr.Add(objTr)
                            'End If

                        End If
                Next


                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill/Select at list one Item", Me.Text)
                    Return False
                End If
                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)

                LoadData(obj.Doc_Code, NavigatorType.Current)
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True

            BlankAllControls()
            LoadBlankGrid()

            'objRemittance = Nothing
            Dim obj As New clsAssetAccountChangeHead()
            obj = clsAssetAccountChangeHead.GetData(strCode, NavTyep)
            'Dim TempAcCode As String = ""
            'Dim TempAcName As String = ""

            'Dim dtAsset As DataTable = clsDBFuncationality.GetDataTable("Select account_code,account_desc from TSPL_JOURNAL_DETAILS where voucher_no in  (Select voucher_no from TSPL_JOURNAL_MASTER where Source_Code='AQ-AS' and Source_Doc_No='" & strCode & "') AND AMOUNT>0")
            'If dtAsset IsNot Nothing AndAlso dtAsset.Rows.Count > 0 Then
            '    For Each dr As DataRow In dtAsset.Rows
            '        TempAcCode = clsCommon.myCstr(dr("account_code"))
            '        TempAcName = clsCommon.myCstr(dr("account_desc"))
            '    Next
            'End If

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    If CostCenterAndHirerachyCodeUpdateAfterPost = True Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If

                End If
                isNewEntry = False
                btnSave.Text = "Update"
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                txtAcquision.Value = obj.Acquisition_Code
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = clsLocation.GetName(obj.Loc_Code, Nothing)

                'Dim ArrAssets As New ArrayList
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAssetAccountChangeDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcheck).Value = True

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).Value = objTr.Asset_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = objTr.Asset_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetCode).Value = objTr.AcSet_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetName).Value = objTr.AcSet_Code_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetSpecificaion).Value = objTr.Asset_Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                        If clsCommon.myLen(objTr.Hirerachy_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Level from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                        End If
                        If ApplyFinancialCostCenter = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.CostCenter_Code
                            If clsCommon.myLen(objTr.CostCenter_Code) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.CostCenter_Code + "'"))  ' objTr.CostCenter_Name
                            End If
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetAmt).Value = objTr.Item_Net_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = objTr.Ac_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountName).Value = objTr.Ac_Name

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChangedAccountCode).Value = objTr.ChangedAc_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChangedAccountName).Value = objTr.ChangedAc_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks

                    Next

                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Sub LoadDataAcquision(ByVal strCode As String)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True

            'BlankAllControls()
            LoadBlankGrid()

            'objRemittance = Nothing
            Dim obj As New clsAcquisitionHead()
            obj = clsAcquisitionHead.GetData(strCode, NavigatorType.Current)
            Dim TempAcCode As String = ""
            Dim TempAcName As String = ""

            Dim dtAsset As DataTable = clsDBFuncationality.GetDataTable("Select account_code,account_desc from TSPL_JOURNAL_DETAILS where voucher_no in  (Select voucher_no from TSPL_JOURNAL_MASTER where Source_Code='AQ-AS' and Source_Doc_No='" & strCode & "') AND AMOUNT>0")
            If dtAsset IsNot Nothing AndAlso dtAsset.Rows.Count > 0 Then
                For Each dr As DataRow In dtAsset.Rows
                    TempAcCode = clsCommon.myCstr(dr("account_code"))
                    TempAcName = clsCommon.myCstr(dr("account_desc"))
                Next
            End If

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Acquisition_Code) > 0) Then

                Dim ArrAssets As New ArrayList
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAcquisitionDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcheck).Value = True

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).Value = objTr.Asset_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = objTr.Asset_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetCode).Value = objTr.AcSet_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetName).Value = objTr.AcSet_Code_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetSpecificaion).Value = objTr.Asset_Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetAmt).Value = objTr.Book_Source_Original_value

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountCode).Value = TempAcCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountName).Value = TempAcName

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChangedAccountCode).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChangedAccountName).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                        If clsCommon.myLen(objTr.Hirerachy_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Level from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                        End If
                        If ApplyFinancialCostCenter = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.CostCenter_Code
                            If clsCommon.myLen(objTr.CostCenter_Code) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.CostCenter_Code + "'"))
                            End If
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = ""
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = ""
                        End If

                    Next

                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        '        Dim qry As String
        '        Dim whrcls As String
        '        Dim arr As New ArrayList()
        '        If txtLocation.Value = "" Then
        '            common.clsCommon.MyMessageBoxShow("Please first select Location")
        '            Return
        '        End If
        '        arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        '        qry = arr.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        '        whrcls = arr.Item(1)

        '        If whrcls = "" Then

        '        Else
        '            whrcls = "(" + whrcls + ")"
        '        End If
        '        If whrcls Is Nothing OrElse whrcls = "" Then
        '            whrcls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        Else
        '            whrcls = whrcls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        End If
        '        whrcls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "'  and TSPL_GL_ACCOUNTS.ControlAccount='N'  "

        '        Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        '        Dim JEWithOPening As Boolean = False
        '        If clsCommon.myLen(strERPStartDate) > 0 Then
        '            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(strERPStartDate).AddDays(-1)
        '            If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
        '                JEWithOPening = True
        '            End If
        '        End If

        '        Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrcls & Environment.NewLine &
        '          " UNION All " & Environment.NewLine &
        '          " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine &
        '" left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine &
        '" and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine &
        '  " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine &
        '  " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine &
        '  " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1) and  TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "' "


        '        strqry += " ) Final "

        '        gv1.CurrentRow.Cells(colAccountCode).Value = clsCommon.ShowSelectForm("TaxRateFND5", strqry, "Account_Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colAccountCode).Value), "Account_Code", isButtonClick)
        '        gv1.CurrentRow.Cells(colAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colAccountCode).Value) + "'"))
        'txtLocation.Enabled = False

        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"

        gv1.CurrentRow.Cells(colAccountCode).Value = clsCommon.ShowSelectForm("TaxRateFND6", qry, "Account_Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colAccountCode).Value), "Account_Code", isButtonClick)
        gv1.CurrentRow.Cells(colAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colAccountCode).Value) + "'"))
    End Sub


    Private Sub OpenGLAccountChanged(ByVal isButtonClick As Boolean)
        '        Dim qry As String
        '        Dim whrcls As String
        '        Dim arr As New ArrayList()
        '        If txtLocation.Value = "" Then
        '            common.clsCommon.MyMessageBoxShow("Please first select Location")
        '            Return
        '        End If
        '        arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        '        qry = arr.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        '        whrcls = arr.Item(1)

        '        If whrcls = "" Then

        '        Else
        '            whrcls = "(" + whrcls + ")"
        '        End If
        '        If whrcls Is Nothing OrElse whrcls = "" Then
        '            whrcls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        Else
        '            whrcls = whrcls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        End If
        '        whrcls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "'  and TSPL_GL_ACCOUNTS.ControlAccount='N'  "

        '        Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        '        Dim JEWithOPening As Boolean = False
        '        If clsCommon.myLen(strERPStartDate) > 0 Then
        '            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(strERPStartDate).AddDays(-1)
        '            If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
        '                JEWithOPening = True
        '            End If
        '        End If


        '        Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrcls & Environment.NewLine &
        '          " UNION All " & Environment.NewLine &
        '          " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine &
        '" left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine &
        '" and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine &
        '  " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine &
        '  " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine &
        '  " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1) and  TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "' "


        '        strqry += " ) Final "
        '        gv1.CurrentRow.Cells(colChangedAccountCode).Value = clsCommon.ShowSelectForm("TaxRateFND5", strqry, "Account_Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colChangedAccountCode).Value), "Account_Code", isButtonClick)
        '        gv1.CurrentRow.Cells(colChangedAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colChangedAccountCode).Value) + "'"))

        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"

        gv1.CurrentRow.Cells(colChangedAccountCode).Value = clsCommon.ShowSelectForm("TaxRateFND5", qry, "Account_Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colChangedAccountCode).Value), "Account_Code", isButtonClick)
        gv1.CurrentRow.Cells(colChangedAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colChangedAccountCode).Value) + "'"))
    End Sub


    Sub LoadAssembleDetail()
        ReStoreGridLayout()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsAssetAccountChangeHead.PostData(Form_ID, txtDocNo.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsAssetAccountChangeHead.DeleteData(txtDocNo.Value)) Then
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ASSET_ACCOUNT_CHANGE_HEAD where Doc_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_Code as Code,convert (varchar(10), Doc_Date,103) as Date,case when Status='0' then 'Pending' else 'Approved' end as [Status]  from TSPL_ASSET_ACCOUNT_CHANGE_HEAD"
        LoadData(clsCommon.ShowSelectForm("AcquiFndd1", qry, "Code", "", txtDocNo.Value, "TSPL_ASSET_ACCOUNT_CHANGE_HEAD.Doc_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
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

    Sub SaveLayout1()
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Sub DeleteLayout()
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub





    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick

    End Sub

    Sub RefreshSNo()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub


    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow

        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshSNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub


    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        'Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER"
        txtLocation.Value = clsLocation.getFinder("Location_Type='Physical'", txtLocation.Value, isButtonClicked) ' clsCommon.ShowSelectForm("POVendorFndr", qry, "Location_Code", "Location_Type='Physical'", txtLocation.Value, "Location_Code", isButtonClicked)
        'qry = "select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + txtLocation.Value + "'"
        lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing) 'clsDBFuncationality.getSingleValue(qry)
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Ticket No-ERO/02/08/19-000980
        Try
            Dim frm As New frmCrystalReportViewer()
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select a Acquisition First.", Me.Text)
                Return
            End If
            Dim dtCompAddress As DataTable = Nothing
            Dim Qry As String = ""

            Qry = " select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end " &
                    " +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  " &
                    " + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  " &
                    " + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  " &
                    " + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
                    " + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else  " &
                    "',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  " &
                    " '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ' " &
                    ",Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address  " &
                    " , case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ' CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  " &
                    " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  as CIN_PAN" &
                    " from tspl_company_master "
            dtCompAddress = clsDBFuncationality.GetDataTable(Qry)
            Qry = " SELECT TSPL_ACQUISITION_HEAD.Vendor_Invoice_No,(select cast(TSPL_COMPANY_MASTER.logo_img as image) from TSPL_COMPANY_MASTER) as [logo_img],'" + clsCommon.myCstr(dtCompAddress.Rows(0)("Comp_Address")) + "' as [Company Address],'" + clsCommon.myCstr(dtCompAddress.Rows(0)("CIN_PAN")) + "' as CIN_PAN,'" + objCommonVar.CurrentCompanyName + "' as [Company], TSPL_ACQUISITION_HEAD.Acquisition_Code, TSPL_ACQUISITION_HEAD.Acquisition_Date,"
            Qry += " TSPL_ACQUISITION_HEAD.PO_No,  TSPL_ACQUISITION_HEAD.Description, TSPL_ACQUISITION_HEAD.Remarks,TSPL_LOCATION_MASTER.Location_Desc ,"
            Qry += " TSPL_VENDOR_MASTER.Vendor_Name,  "
            Qry += " TSPL_ACQUISITION_HEAD.tax1,TSPL_ACQUISITION_HEAD.tax1_amt,TSPL_ACQUISITION_HEAD.tax2,TSPL_ACQUISITION_HEAD.tax2_amt,TSPL_ACQUISITION_HEAD.tax3,TSPL_ACQUISITION_HEAD.tax3_amt,TSPL_ACQUISITION_HEAD.tax4,TSPL_ACQUISITION_HEAD.tax4_amt,TSPL_ACQUISITION_HEAD.tax5,TSPL_ACQUISITION_HEAD.tax5_amt,TSPL_ACQUISITION_HEAD.tax6,TSPL_ACQUISITION_HEAD.tax6_amt,TSPL_ACQUISITION_HEAD.tax7,TSPL_ACQUISITION_HEAD.tax7_amt,TSPL_ACQUISITION_HEAD.tax8,TSPL_ACQUISITION_HEAD.tax8_amt,TSPL_ACQUISITION_HEAD.tax9,TSPL_ACQUISITION_HEAD.tax9_amt,TSPL_ACQUISITION_HEAD.tax10,TSPL_ACQUISITION_HEAD.tax10_amt,"
            Qry += " TSPL_ACQUISITION_HEAD.total_amt, TSPL_ACQUISITION_HEAD.total_tax_amt, TSPL_ACQUISITION_HEAD.Net_Amt,"
            Qry += " TSPL_ACQUISITION_DETAIL.SNo, TSPL_ACQUISITION_DETAIL.Asset_Code, TSPL_ACQUISITION_DETAIL.Asset_Name, TSPL_ACQUISITION_DETAIL.Asset_Specification, TSPL_ACQUISITION_DETAIL.Dep_Rate, TSPL_ACQUISITION_DETAIL.Book_Source_value"
            'Qry += " ,TSPL_ACQUISITION_HEAD.Capex_Code as CapexName,TSPL_ACQUISITION_HEAD.CapexSub_Code as SubCapexName"
            Qry += " ,TSPL_ACQUISITION_DETAIL.Capex_Code as CapexName,TSPL_ACQUISITION_DETAIL.Capex_SubCode as SubCapexName"
            Qry += " ,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as SubCapexNameDesc,TSPL_CAPEX_MASTER.DESCRIPTION as CapexDesc"
            Qry += " ,case when TSPL_ACQUISITION_HEAD.Status=1 then TSPL_ACQUISITION_HEAD.ASSEMBLE_CODE else '' end as ASSEMBLE_CODE,"
            Qry += " case when TSPL_ACQUISITION_HEAD.Status=1 then convert(varchar,TSPL_ACQUISITION_HEAD.Post_Date,103) else '' end as Post_Date"
            Qry += " FROM TSPL_ACQUISITION_HEAD "
            Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_ACQUISITION_HEAD.Loc_Code"
            Qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code"
            'Show Capex,SubCapex Description  Ticket No-UDL/07/05/18-000153
            'Qry += "    left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE=TSPL_ACQUISITION_HEAD.Capex_Code"
            'Qry += " left outer join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE=TSPL_ACQUISITION_HEAD.CapexSub_Code"
            Qry += "    left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE=TSPL_ACQUISITION_DETAIL.Capex_Code"
            Qry += " left outer join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE=TSPL_ACQUISITION_DETAIL.Capex_SubCode"
            ''''''''''''
            Qry += " where TSPL_ACQUISITION_HEAD.Acquisition_Code = '" + txtDocNo.Value + " ' order by TSPL_ACQUISITION_DETAIL.SNo"

            Dim dt_final As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt_final.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                frm.funreport(CrystalReportFolder.FixedAssets, dt_final, "frmAcquisionEntryReport", "Acquision Entry Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If clsCommon.myLen(txtAcquision.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Asset Group first", Me.Text)
            Exit Sub
        End If
        LoadDataAcquision(txtAcquision.Value)
    End Sub


    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        '' export header
        ExportHeader()
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs)
        '' export detail
        ExportDetail()
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs)
        '' import header
        ImportHeader()
    End Sub

    Private Sub RadMenuItem7_Click(sender As Object, e As EventArgs)
        '' import detail
        ImportDetail()
    End Sub

    Private Sub ExportHeader()
        Try
            Dim Qry As String
            Qry = " select Acquisition_Code as [Acqisition Code],Acquisition_Date as [Acquisition Date],Vendor_Code as [Vendor Code],Description, " &
                  " Remarks,Loc_Code as [Location Code],SRN_No as [SRN No],Templete_Code as [Template Code],Status_New_Old as [New/Old] from TSPL_ACQUISITION_HEAD"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = " select '' as [Acqisition Code],'' as [Acquisition Date],'' as [Vendor Code],'' as Description, " &
                 " '' as Remarks,'' as [Location Code],'' as [SRN No],'' as [Template Code],'' as [New/Old] from TSPL_ACQUISITION_HEAD"
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Sub ExportDetail()
        Try
            Dim Qry As String
            Qry = "select Acquisition_Code as [Acqisition Code],SNo,Asset_Code as [Asset Code],Asset_Name as [Asset Name],Asset_Specification as [Asset Specification],Item_Code as [Item Code],Templete_Code as [Template Code]," &
                  " Category_code as [Category Code],Group_Code as [Group Code],AcSet_Code as [Account Set Code],CostCenter_Code as [Cost Center Code], " &
                  " Acqusition_Date as [Acquisition Date],Dep_Method_Code as [Depreciation Method Code],Dep_Period_Code as [Depreciation Period Code], " &
                  " Start_Date as [Start Date],Dep_Rate as [Depreciation Rate],Book_Estimated_Life as [Book Estimated Life],Book_Source_value as [Book Source Value], " &
                  " Book_Source_Original_value as [Book Source Original Value], " &
                  " Dep_Tax_Rate as [Dep Tax Rate],Book_Salvage_Value as [Book Solvage Value] from TSPL_ACQUISITION_DETAIL "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "select '' as [Acqisition Code],'' as SNo,'' as [Asset Code],'' as [Asset Name],'' as [Asset Specification],'' as [Item Code],'' as [Template Code]," &
                  " '' as [Category Code],'' as [Group Code],'' as [Account Set Code],'' as [Cost Center Code], " &
                  " '' as [Acquisition Date],'' as [Depreciation Method Code],'' as [Depreciation Period Code], " &
                  " '' as [Start Date],'' as [Depreciation Rate],'' as [Book Estimated Life],'' as [Book Source Value], " &
                  " '' as [Book Source Original Value], " &
                  " '' as [Dep Tax Rate],'' as [Book Solvage Value] "
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Private Sub ImportHeader()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim total As Integer = 0
        If transportSql.importExcel(gv, "Acqisition Code", "Acquisition Date", "Vendor Code", "Description", "Remarks", "Location Code", "SRN No", "Template Code", "New/Old") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsCategories)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAcquisitionHead

                    '-------Acquisition Code---------

                    obj.Acquisition_Code = clsCommon.myCstr(grow.Cells("Acqisition Code").Value)
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        isNewEntry = Not clsAcquisitionHead.CheckCode(obj.Acquisition_Code)
                    Else
                        isNewEntry = True
                    End If
                    obj.Acquisition_Date = clsCommon.myCstr(grow.Cells("Acquisition Date").Value)
                    obj.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                    'obj.Acquisition_Date = clsCommon.myCstr(grow.Cells("Acqisition Date").Value)
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj.Loc_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    obj.SRN_No = clsCommon.myCstr(grow.Cells("SRN No").Value)
                    obj.Templete_Code = clsCommon.myCstr(grow.Cells("Template Code").Value)
                    obj.statusnewold = clsCommon.myCstr(grow.Cells("New/Old").Value)

                    If clsCommon.myLen(obj.Acquisition_Date) <= 0 Then
                        Throw New Exception("Please enter Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                        obj.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                        If clsCommon.myLen(obj.Vendor_Name) <= 0 Then
                            Throw New Exception("Vendor Code does not exist on Line No '" + LineNo + "'")
                            Exit Sub
                        End If
                    End If

                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        Throw New Exception("Enter Location Code on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(clsLocation.GetName(obj.Loc_Code, Nothing)) <= 0 Then
                        Throw New Exception("Location Code does not exist on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.SRN_No) > 30 Then
                        Throw New Exception("The Maximum Length of SRN No on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.Templete_Code) > 30 Then
                        Throw New Exception("The Maximum Length of Template Code on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    ElseIf clsCommon.myCdbl(obj.statusnewold) > 1 Then
                        Throw New Exception("Satus must be 0 or 1 on Line No '" + LineNo + "'")
                        Exit Sub
                        'ElseIf clsCommon.myCdbl(obj.statusnewold) > 1 Then
                        '    Throw New Exception("Satus must be 0 or 1 on Line No '" + LineNo + "'")
                        '    Exit Sub
                    End If
                    If (obj.SaveData(obj, isNewEntry, False, Nothing)) Then
                        total = total + 1
                    End If

                    'Arr.Add(obj)
                Next

                common.clsCommon.MyMessageBoxShow("" & total & " Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                clsCommon.ProgressBarHide()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub ImportDetail()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim total As Integer = 0

        If transportSql.importExcel(gv, "Acqisition Code", "SNo", "Asset Code", "Asset Name", "Asset Specification", "Item Code", "Template Code", "Category Code", "Group Code", "Account Set Code", "Cost Center Code", "Acquisition Date", "Depreciation Method Code", "Depreciation Period Code", "Start Date", "Depreciation Rate", "Book Estimated Life", "Book Source Value", "Book Source Original Value", "Dep Tax Rate", "Book Solvage Value") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsAcquisitionDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAcquisitionDetail

                    '-------Acquisition Code---------

                    obj.Acquisition_Code = clsCommon.myCstr(grow.Cells("Acqisition Code").Value)
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        isNewEntry = Not clsAcquisitionHead.CheckCode(obj.Acquisition_Code)
                    Else
                        isNewEntry = True
                    End If
                    obj.Acqusition_Date = clsCommon.myCstr(grow.Cells("Acquisition Date").Value)
                    obj.SNo = clsCommon.myCdbl(grow.Cells("SNo").Value)
                    obj.Asset_Code = clsCommon.myCstr(grow.Cells("Asset Code").Value)
                    obj.Asset_Name = clsCommon.myCstr(grow.Cells("Asset Name").Value)
                    obj.Asset_Specification = clsCommon.myCstr(grow.Cells("Asset Specification").Value)
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    obj.Templete_Code = clsCommon.myCstr(grow.Cells("Template Code").Value)
                    obj.Category_code = clsCommon.myCstr(grow.Cells("Category Code").Value)
                    obj.Group_Code = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    obj.AcSet_Code = clsCommon.myCstr(grow.Cells("Account Set Code").Value)
                    obj.CostCenter_Code = clsCommon.myCstr(grow.Cells("Cost Center Code").Value)
                    obj.Dep_Method_Code = clsCommon.myCstr(grow.Cells("Depreciation Method Code").Value)
                    obj.Dep_Period_Code = clsCommon.myCstr(grow.Cells("Depreciation Period Code").Value)
                    obj.Start_Date = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    obj.Dep_Rate = clsCommon.myCdbl(grow.Cells("Depreciation Rate").Value)
                    obj.Book_Estimated_Life = clsCommon.myCdbl(grow.Cells("Book Estimated Life").Value)
                    obj.Book_Source_value = clsCommon.myCdbl(grow.Cells("Book Source Value").Value)
                    obj.Book_Source_Original_value = clsCommon.myCdbl(grow.Cells("Book Source Original Value").Value)
                    obj.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells("Dep Tax Rate").Value)
                    obj.Book_Salvage_Value = clsCommon.myCdbl(grow.Cells("Book Solvage Value").Value)


                    If clsCommon.myLen(obj.Acqusition_Date) <= 0 Then
                        Throw New Exception("Please enter Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    'If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    '    Throw New Exception("Please enter Asset Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If
                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
                        Throw New Exception("Please enter Item Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Templete_Code) <= 0 Then
                        Throw New Exception("Please enter Template Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Category_code) <= 0 Then
                        Throw New Exception("Please enter Category Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Group_Code) <= 0 Then
                        Throw New Exception("Please enter Group Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.AcSet_Code) <= 0 Then
                        Throw New Exception("Please enter Account Set Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.CostCenter_Code) <= 0 Then
                        Throw New Exception("Please enter Cost Center Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Dep_Method_Code) <= 0 Then
                        Throw New Exception("Please enter Depreciation Method Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Dep_Period_Code) <= 0 Then
                        Throw New Exception("Please enter Depreciation Period Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If obj.Start_Date < obj.Acqusition_Date Then
                        Throw New Exception("Please start date must be less than Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        Arr.Add(obj)
                    End If

                Next
                clsAcquisitionDetail.SaveData("", Arr, Nothing)

                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                clsCommon.ProgressBarHide()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub




    Sub Export_Asset()
        Try
            Dim str As String = ""
            If gv1.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If ii <> 0 Then
                        str += " Union all "
                    End If
                    'If chkMilk.Checked Then
                    '    str += "select '" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' as Item,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value).Replace("'", "''") + "' as ItemName,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccountInvControl).Value).Replace("'", "''") + "' as InventoryAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccount).Value) + "' as GLAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrQty).Value) + "' as CurrentQty,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATPers).Value) + "' as CurrentFATPer,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATKG).Value) + "' as CurrentFATKg,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATAmount).Value) + "' as CurrentFATAmt,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFPers).Value) + "' as CurrentSNFPer,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFKG).Value) + "' as CurrentSNFKg,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFAmt).Value) + "' as CurrentSNFAmt,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrAmt).Value) + "' as CurrentAmt ,'" + IIf(clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value), "Y", "N") + "' as [NillBalance(Y/N)],0 as Qty,0 as FAT,0 as FATAmount,0 as SNF,0 as SNFAmount"
                    'Else
                    '    str += "select '" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' as Item,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value).Replace("'", "''") + "' as ItemName,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccountInvControl).Value).Replace("'", "''") + "' as InventoryAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccount).Value) + "' as GLAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrQty).Value) + "' as CurrentQty,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrAmt).Value) + "' as CurrentAmt,'" + IIf(clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value), "Y", "N") + "' as [NillBalance(Y/N)],0 as Qty,0 as Amount "
                    'End If
                    str += "select '" + IIf(clsCommon.myCBool(gv1.Rows(ii).Cells(colcheck).Value), "Y", "N") + "' as [Save(Y/N)],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colLineNo).Value) + "' as [S No],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetID).Value).Replace("'", "''") + "' as [Asset Code],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetName).Value).Replace("'", "''") + "' as [Asset Description],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetSpecificaion).Value).Replace("'", "''") + "' as [Asset Specification],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value).Replace("'", "''") + "' as [Item Code],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value).Replace("'", "''") + "' as [Item Description],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAccountSetCode).Value).Replace("'", "''") + "' as [Account Set Code],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAccountSetName).Value).Replace("'", "''") + "' as [Account Set],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAccountCode).Value).Replace("'", "''") + "' as [Account Code],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colAccountName).Value).Replace("'", "''") + "' as [Account Description],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colChangedAccountCode).Value).Replace("'", "''") + "' as [Changed Account Code],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colChangedAccountName).Value).Replace("'", "''") + "' as [Changed Account Description],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colNetAmt).Value) + "' as [Net Amount],'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colRemarks).Value).Replace("'", "''") + "' as [Remarks]"
                Next
            Else
                'If chkMilk.Checked Then
                '    str = "select '' as Item,'' as ItemName,'' as InventoryAccount,'' as GLAccount,0 as CurrentQty,0 as CurrentFATPer,0 as as CurrentFATKg,0 as CurrentFATAmt,0 as CurrentSNFPer,0 as CurrentSNFKg,0 as CurrentSNFAmt,0 as CurrentAmt ,'N' as [NillBalance(Y/N)],0 as Qty,0 as FAT,0 as FATAmount,0 as SNF,0 as SNFAmount"
                'Else
                '    str = "select '' as Item,'' as ItemName,'' as InventoryAccount,'' as GLAccount,0 as CurrentQty,0 as CurrentAmt,'N' as [NillBalance(Y/N)],0 as Qty,0 as Amount "
                'End If
                str = "select 'N' as [Save(Y/N)],0 as [S No],'' as [Asset Code],'' as [Asset Description],'' as [Asset Specification],'' as [Item Code],'' as [Item Description],'' as [Account Set Code],'' as [Account Set],'' as [Account Code],'' as [Account Description],'' as [Changed Account Code],'' as [Changed Account Description],0 as [Net Amount],'' as [Remarks]"
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export_Asset()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim gvImport As New RadGridView()
        Me.Controls.Add(gvImport)

        Try

            transportSql.importExcel(gvImport, "Save(Y/N)", "S No", "Asset Code", "Asset Description", "Asset Specification", "Item Code", "Item Description", "Account Set Code", "Account Set", "Account Code", "Account Description", "Changed Account Code", "Changed Account Description", "Net Amount", "Remarks")

            For ii As Integer = 0 To gvImport.RowCount - 1
                Dim strICode As String = clsCommon.myCstr(gvImport.Rows(ii).Cells("Asset Code").Value)
                For jj As Integer = 0 To gv1.RowCount - 1
                    Dim strICodeGrid As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colAssetID).Value)
                    If clsCommon.CompairString(strICode, strICodeGrid) = CompairStringResult.Equal Then
                        gv1.CurrentRow = gv1.Rows(jj)
                        gv1.Rows(jj).Cells(colcheck).Value = (clsCommon.CompairString(clsCommon.myCstr(gvImport.Rows(ii).Cells("Save(Y/N)").Value), "Y") = CompairStringResult.Equal)
                        gv1.Rows(jj).Cells(colLineNo).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("S No").Value)

                        gv1.Rows(jj).Cells(colAssetID).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Asset Code").Value)
                        gv1.Rows(jj).Cells(colAssetName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Asset Description").Value)
                        gv1.Rows(jj).Cells(colAssetSpecificaion).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Asset Specification").Value)

                        gv1.Rows(jj).Cells(colICode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Item Code").Value)
                        gv1.Rows(jj).Cells(colIName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Item Description").Value)

                        gv1.Rows(jj).Cells(colAccountSetCode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Account Set Code").Value)
                        gv1.Rows(jj).Cells(colAccountSetName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Account Set").Value)

                        gv1.Rows(jj).Cells(colAccountCode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Account Code").Value)
                        gv1.Rows(jj).Cells(colAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gvImport.Rows(ii).Cells("Account Code").Value) + "'"))
                        'clsCommon.myCstr(gvImport.Rows(ii).Cells("Account Description").Value)

                        gv1.Rows(jj).Cells(colChangedAccountCode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Changed Account Code").Value)
                        gv1.Rows(jj).Cells(colChangedAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gvImport.Rows(ii).Cells("Changed Account Code").Value) + "'"))
                        'clsCommon.myCstr(gvImport.Rows(ii).Cells("Changed Account Description").Value)

                        gv1.Rows(jj).Cells(colNetAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Net Amount").Value)
                        gv1.Rows(jj).Cells(colRemarks).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Remarks").Value)

                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvImport)
        End Try

    End Sub
    ''shivani ==========> against ticket[BM00000008015]
  
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        SaveLayout1()
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        DeleteLayout()
    End Sub



    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
    '======================Added  by preeti gupta=====================================

    ''UDL/16/10/18-000231 richa 
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If clsAssetAccountChangeHead.ReverseAndUnpost(txtDocNo.Value, trans) Then
                    trans.Commit()
                    saveCancelLog(Reason, "Reverse And Recreate")
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub Btn_apply_Click(sender As Object, e As EventArgs) Handles Btn_apply.Click
        Try
            If clsCommon.myLen(fndFAAccount.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select changed GL Account", Me.Text)
                fndFAAccount.Focus()
                Exit Sub
            End If
            Dim accname As String = clsDBFuncationality.getSingleValue("select Description  from TSPL_GL_ACCOUNTS where account_code='" & fndFAAccount.Value & "'")
            isInsideLoadData = True
            For ii As Integer = 1 To gv1.Rows.Count
                gv1.Rows(ii - 1).Cells(colChangedAccountCode).Value = fndFAAccount.Value
                gv1.Rows(ii - 1).Cells(colChangedAccountName).Value = clsCommon.myCstr(accname)
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub FndFAAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFAAccount._MYValidating
        'Dim qry As String
        'Dim whrcls As String
        '        Dim arr As New ArrayList()
        '        If txtLocation.Value = "" Then
        '            common.clsCommon.MyMessageBoxShow("Please first select Location")
        '            Return
        '        End If
        '        arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        '        qry = arr.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        '        whrcls = arr.Item(1)

        '        If whrcls = "" Then

        '        Else
        '            whrcls = "(" + whrcls + ")"
        '        End If
        '        If whrcls Is Nothing OrElse whrcls = "" Then
        '            whrcls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        Else
        '            whrcls = whrcls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        '        End If
        '        whrcls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "'  and TSPL_GL_ACCOUNTS.ControlAccount='N'  "

        '        Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        '        Dim JEWithOPening As Boolean = False
        '        If clsCommon.myLen(strERPStartDate) > 0 Then
        '            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(strERPStartDate).AddDays(-1)
        '            If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
        '                JEWithOPening = True
        '            End If
        '        End If


        '        Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrcls & Environment.NewLine &
        '          " UNION All " & Environment.NewLine &
        '          " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine &
        '" left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine &
        '" and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine &
        '  " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine &
        '  " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine &
        '  " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1) and  TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtLocation.Value + "' "


        '        strqry += " ) Final "
        '        fndFAAccount.Value = clsCommon.ShowSelectForm("FndFAccount1", strqry, "Account_Code", "", fndFAAccount.Value, "Account_Code", isButtonClicked)
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndFAAccount.Value = clsCommon.ShowSelectForm("FndFAccount1", qry, "Account_Code", "", fndFAAccount.Value, "Account_Code", isButtonClicked)
    End Sub

    Private Sub FndAssetGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndAssetGroup._MYValidating
        'Dim qry As String = " select group_code as Code,Description,category_code from TSPL_ASSET_GROUP "
        'fndAssetGroup.Value = clsCommon.ShowSelectForm("AssetGroup", qry, "Code", "", fndAssetGroup.Value, "", isButtonClicked)
        ''If clsCommon.myLen(fndPINo.Value) <= 0 AndAlso clsCommon.CompairString(ddlAcqType.Text, "Asset") <> CompairStringResult.Equal Then
        'If Not fndAssetGroup.Value Is Nothing Then
        '    If clsCommon.myLen(clsCommon.myCstr(fndAssetGroup.Value)) > 0 Then

        '        gv1.Rows.Clear()
        '        gv1.Rows.AddNew()
        '        Dim obj As ClsTemplateMaster = clsAssetAccountChangeDetail.GetAsset(fndTemplateCode.Value, NavigatorType.Current)
        '        Me.lblAssetGroupDesc.Text = clsCommon.myCstr(obj.template_Name)


        '        Dim grow As GridViewRowInfo = gv1.CurrentRow

        '        ''If UsLock1.Status = ERPTransactionStatus.Posted OrElse UsLock1.Status = ERPTransactionStatus.Approved Then
        '        ''    grow.Cells(colAssetID).Value = obj.Acset_code
        '        ''Else
        '        ''    grow.Cells(colAssetID).Value = obj.Acset_code
        '        ''    grow.Cells(colOldAssetID).Value = obj.Acset_code
        '        ''End If

        '        'Dim arr As New ArrayList
        '        'arr.Add(frm.obj.Asset_Code)
        '        'Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr)
        '        'qry = "select count(*) from (" & qry & ") Final "
        '        'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
        '        '    grow.Cells(colAssetID).ReadOnly = True
        '        'Else
        '        '    If clsCommon.myLen(obj.Acset_code) <= 0 Then
        '        '        grow.Cells(colAssetID).ReadOnly = False
        '        '    End If
        '        'End If
        '        'grow.Cells(colAssetID).Value = obj.Acset_code
        '        'grow.Cells(colAssetName).Value = obj.Acset_Description
        '        grow.Cells(colTemplete).Value = obj.template_code
        '        grow.Cells(colTempleteName).Value = obj.template_Name
        '        grow.Cells(colCategoryCode).Value = obj.category_code
        '        grow.Cells(colCategoryName).Value = obj.category_Description
        '        'grow.Cells(colCostCenterCode).Value = obj.CostCenter_Code
        '        'grow.Cells(colCostCenterName).Value = obj.CostCenter_Description
        '        grow.Cells(colGroupCode).Value = obj.group_code
        '        grow.Cells(colGroupName).Value = obj.group_Description
        '        grow.Cells(colAccountSetCode).Value = obj.Acset_code
        '        grow.Cells(colAccountSetName).Value = obj.Acset_Description
        '        grow.Cells(colAcquisitionDate).Value = obj.Created_Date
        '        grow.Cells(colDepMethod).Value = obj.Dep_Method_Code
        '        grow.Cells(colDepMethodName).Value = obj.Dep_Method_Name
        '        grow.Cells(colDepMethodTax).Value = obj.Dep_Method_Tax_Code
        '        grow.Cells(colDepMethodNameTax).Value = obj.Dep_Method_Tax_Name
        '        grow.Cells(colDepPeriodCode).Value = obj.Dep_Period_Code
        '        grow.Cells(colDepPeriodName).Value = obj.Dep_Period_Name

        '        ''grow.Cells(colPutToUse).Value = If(obj.Put_To_Use = True, "1", "0")
        '        grow.Cells(colStartDate).Value = obj.Start_Date
        '        grow.Cells(colDepRate).Value = obj.Dep_Rate
        '        grow.Cells(ColBookEstimatedLife).Value = obj.Book_Estimated_Life
        '        grow.Cells(ColBookSourceValue).Value = obj.Book_Source_value

        '        grow.Cells(ColBookSourceOriginalValue).Value = obj.Book_Source_Original_value
        '        grow.Cells(ColBookSalvageRate).Value = obj.Book_Salvage_Rate
        '        grow.Cells(ColBookSalvageValue).Value = obj.Book_Salvage_Value
        '        grow.Cells(colAssetSpecificaion).Value = obj.Asset_Specification
        '        grow.Cells(colICode).Value = obj.Item_Code
        '        grow.Cells(colIName).Value = obj.Item_Name
        '        grow.Cells(colDepTaxRate).Value = obj.Dep_Tax_Rate
        '        grow.Cells(colTotTaxAmt).Value = obj.Total_Tax_Amt
        '        grow.Cells(colNetAmt).Value = obj.Item_Net_Amt

        '        grow.Cells(colIsCategory).Value = IIf(obj.CapexSub_Code = "", False, True)  'obj.IsCapex
        '        grow.Cells(colCategoryType).Value = IIf(obj.CapexSub_Code = "", "None", "Capex")

        '        grow.Cells(colCapexCode).Value = obj.Capex_Code

        '        grow.Cells(colCapexSubCode).Value = obj.CapexSub_Code

        '        RefreshSNo()

        '    End If
        'End If
        'End If


    End Sub


    Private Sub TxtAcquision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtAcquision._MYValidating
        If txtLocation.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
            Return
        End If
        Dim qry As String = "select Acquisition_Code as Code,convert (varchar(10), Acquisition_Date,103) as Date, Vendor_Code as Vendor,Net_Amt as Amount,Acquisition_Type as [Acquisition Type]  from TSPL_ACQUISITION_HEAD"
        txtAcquision.Value = clsCommon.ShowSelectForm("AcqFnd", qry, "Code", " Acquisition_Type<>'Merge' and Status=1 and loc_code='" + txtLocation.Value + "'", txtAcquision.Value, "TSPL_ACQUISITION_HEAD.Acquisition_Date desc", isButtonClicked)
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + txtDocNo.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim coll As New Hashtable()
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAssetID).Value)) > 0 Then
                    Dim strAssetCode As String = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(grow.Cells(colHierarchyCode).Value), True)
                    clsCommon.AddColumnsForChange(coll, "CostCenter_Code", clsCommon.myCstr(grow.Cells(colCostCenterCode).Value), True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_ACCOUNT_CHANGE_DETAIL", OMInsertOrUpdate.Update, "Doc_code='" + txtDocNo.Value + "' and Asset_Code = '" + strAssetCode + "'", trans)
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + txtDocNo.Value + "' ", trans))
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        If clsCommon.myLen(strVoucherNo) > 0 Then
                            Dim strAc_Control As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Cells(colChangedAccountCode).Value), txtLocation.Value, trans)
                            Dim strWIP_AC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Cells(colAccountCode).Value), txtLocation.Value, trans)
                            If clsCommon.myLen(strAc_Control) > 0 Then
                                Dim qry As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' WHERE Voucher_No='" + strVoucherNo + "'  and Account_code = '" + strAc_Control + "'  "
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                            If clsCommon.myLen(strWIP_AC) > 0 Then
                                Dim qry As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' WHERE Voucher_No='" + strVoucherNo + "'  and Account_code = '" + strWIP_AC + "'  "
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                        End If
                    End If
                End If
            Next
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try








    End Sub
End Class
