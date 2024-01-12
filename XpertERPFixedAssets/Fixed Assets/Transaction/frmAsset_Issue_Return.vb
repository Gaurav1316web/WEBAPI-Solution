'--Created By --[Pankaj Kumar Chaudhary]--Aaagainst Ticket No-[BM00000001564]
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmAsset_Issue_Return
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ArrDb As New List(Of String)
    Const colLineNo As String = "LineNo"
    Const colSelect As String = "Select"
    Const colAsset_Id As String = "AssetId"
    Const colAsset_Name As String = "AssetName"
    Const colSpecification As String = "Specification"
    Const colFrom As String = "From"
    Const colFromDesc As String = "FromDesc"
    Const colTransDate As String = "Trans_Date"
    Const colTempTranDate As String = "TempTrans_Date"
    Const ColCostCenter As String = "CostCenter"
    Const ColCostCenterName As String = "CostCenterName"
    Dim IsInsideLoadData As Boolean = True

    Private Sub FrmAsset_Issue_Return_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmAsset_Issue_Return_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkIssue.IsChecked = True
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ArrDb.Add(objCommonVar.CurrDatabase)

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset Trasnaction")


    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAsset_Issue_Return)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        dgvVisi.Rows.Clear()
        dgvVisi.Columns.Clear()

        Dim LineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        LineNo = New GridViewDecimalColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "No"
        LineNo.Name = colLineNo
        LineNo.Width = 40
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvVisi.MasterTemplate.Columns.Add(LineNo)

        Dim CollSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        CollSelect.FormatString = ""
        CollSelect.HeaderText = "Select"
        CollSelect.Name = colSelect
        CollSelect.Width = 61
        CollSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        dgvVisi.MasterTemplate.Columns.Add(CollSelect)

        Dim AssetId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetId.FormatString = ""
        AssetId.HeaderText = "Asset Id"
        AssetId.Name = colAsset_Id
        AssetId.Width = 100
        AssetId.ReadOnly = True
        AssetId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetId)

        Dim AssetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetName.FormatString = ""
        AssetName.HeaderText = "Asset Name"
        AssetName.Name = colAsset_Name
        AssetName.Width = 200
        AssetName.ReadOnly = True
        AssetName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetName)

        Dim Spec As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Spec.FormatString = ""
        Spec.HeaderText = "Specification"
        Spec.Name = colSpecification
        Spec.Width = 300
        Spec.ReadOnly = True
        Spec.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(Spec)

        Dim frmEntity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        frmEntity.FormatString = ""
        frmEntity.Name = colFrom
        frmEntity.Width = 100
        frmEntity.MaxLength = 50
        If chkIssue.IsChecked Then
            frmEntity.HeaderText = "Issue To"
            frmEntity.ReadOnly = False
        Else
            frmEntity.HeaderText = "Return From"
            frmEntity.ReadOnly = True
        End If
        ' frmEntity.HeaderImage = Global.ERP.My.Resources.Resources.search4
        frmEntity.TextImageRelation = TextImageRelation.TextBeforeImage
        frmEntity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(frmEntity)

        Dim Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Desc.FormatString = ""
        Desc.HeaderText = "Description"
        Desc.Name = colFromDesc
        Desc.Width = 200
        Desc.ReadOnly = True
        Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(Desc)

        Dim temptransDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        temptransDate.FormatString = ""
        temptransDate.Format = DateTimePickerFormat.Custom
        temptransDate.CustomFormat = "dd/MMM/yyyy hh:mm tt"
        temptransDate.HeaderText = "Issue Date"
        temptransDate.Name = colTempTranDate
        If chkReturn.IsChecked Then
            temptransDate.IsVisible = True
        Else
            temptransDate.IsVisible = False
        End If
        temptransDate.Width = 140
        temptransDate.ReadOnly = True
        temptransDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(temptransDate)

        Dim transDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        transDate.FormatString = ""
        transDate.Format = DateTimePickerFormat.Custom
        transDate.CustomFormat = "dd/MMM/yyyy hh:mm tt"
        If chkIssue.IsChecked Then
            transDate.HeaderText = "Issue Date"
        Else
            transDate.HeaderText = "Retrun Date"
        End If
        transDate.Name = colTransDate
        transDate.Width = 140
        transDate.ReadOnly = False
        transDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(transDate)

        '=========added by shivani
        Dim Cost As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cost.FormatString = ""
        Cost.Name = ColCostCenter
        Cost.Width = 100
        Cost.MaxLength = 50
        Cost.HeaderText = "Cost Center"
        Cost.ReadOnly = False
        ' Cost.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Cost.TextImageRelation = TextImageRelation.TextBeforeImage
        Cost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(Cost)

        Dim CostDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostDesc.FormatString = ""
        CostDesc.HeaderText = "Cost Center Name"
        CostDesc.Name = ColCostCenterName
        CostDesc.Width = 200
        CostDesc.ReadOnly = True
        CostDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(CostDesc)
        '=====================================
        dgvVisi.EnableFiltering = True
        dgvVisi.AllowDeleteRow = False
        dgvVisi.ShowGroupPanel = False
        dgvVisi.AllowColumnReorder = False
        dgvVisi.AllowRowReorder = False
        dgvVisi.EnableSorting = False
        dgvVisi.AllowAddNewRow = False
        dgvVisi.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvVisi.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub Reset()
        txtFromEntity.Value = ""
        lblFromEntity.Text = ""
        LoadBlankGrid()
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If SaveData() Then
                RadMessageBox.Show("Data Saved Successfully")
                If chkIssue.IsChecked Then
                    LoadDetails(txtFromEntity.Value, "Issue")
                Else
                    LoadDetails(txtFromEntity.Value, "Return")
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsAssetIssueReturn)
                For Each grow As GridViewRowInfo In dgvVisi.Rows
                    Dim objTr As New clsAssetIssueReturn()
                    If grow.Cells(colSelect).Value = True Then
                        objTr.Asset_Id = clsCommon.myCstr(grow.Cells(colAsset_Id).Value)
                        objTr.Trans_Date = clsCommon.myCstr(grow.Cells(colTransDate).Value)
                        If chkIssue.IsChecked Then
                            objTr.From_Entity = clsCommon.myCstr(txtFromEntity.Value)
                            objTr.To_Entity = clsCommon.myCstr(grow.Cells(colFrom).Value)
                            objTr.Trans_Type = "Issue"
                        Else
                            objTr.From_Entity = clsCommon.myCstr(grow.Cells(colFrom).Value)
                            objTr.To_Entity = clsCommon.myCstr(txtFromEntity.Value)
                            objTr.Trans_Type = "Return"
                        End If
                        objTr.CostCenter_Code = clsCommon.myCstr(grow.Cells(ColCostCenter).Value)
                        Arr.Add(objTr)
                    End If
                Next

                If (clsAssetIssueReturn.SaveData(Arr, ArrDb)) Then
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtFromEntity.Value) > 0 Then
            Dim Counter As Integer = 0
            For i As Integer = 0 To dgvVisi.Rows.Count - 1
                If dgvVisi.Rows(i).Cells(colSelect).Value = True Then
                    Counter += 1
                    If clsCommon.myLen(dgvVisi.Rows(i).Cells(colTransDate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please select Date At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    End If
                    If chkReturn.IsChecked Then
                        Dim IssueDate As Date = dgvVisi.Rows(i).Cells(colTempTranDate).Value
                        Dim PulloutDate As Date = dgvVisi.Rows(i).Cells(colTransDate).Value
                        Dim ts As TimeSpan = PulloutDate - IssueDate
                        If CInt(ts.TotalDays) < 0 Then
                            clsCommon.MyMessageBoxShow(Me, "You cann't Return Asset - " + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAsset_Id).Value) + " Before it's Issue Date - " + IssueDate + " at Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                            Return False
                        End If
                    End If
                End If
            Next
            If Counter > 0 Then
                Return True
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Row", Me.Text)
                Return False
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select Location", Me.Text)
            txtFromEntity.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub LoadDetails(ByVal strCustCode As String, ByVal StrTransType As String)
        Try
            IsInsideLoadData = True
            LoadBlankGrid()
            Qry = "Select TSPL_ACQUISITION_DETAIL.Asset_Code, TSPL_ACQUISITION_DETAIL.Asset_Name, TSPL_ACQUISITION_DETAIL.Asset_Specification, "
            Qry += " TSPL_ACQUISITION_DETAIL.Issue_Return_Date as IssueDate, Case When Is_Issued='Y' Then TSPL_ACQUISITION_DETAIL.To_Entity Else '' End as FromEntity, "
            Qry += " Case When Is_Issued='Y' Then TSPL_EMPLOYEE_MASTER.Emp_Name Else '' End as FromEntityDesc,TSPL_ACQUISITION_DETAIL.CostCenter_Code,CostCenter_Name from TSPL_ACQUISITION_DETAIL "
            Qry += " LEFT OUTER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
            Qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_ACQUISITION_HEAD.Loc_Code"
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code = TSPL_ACQUISITION_DETAIL.To_Entity left join TSPL_FA_COST_CENTER_MASTER on TSPL_FA_COST_CENTER_MASTER.CostCenter_Code=TSPL_ACQUISITION_DETAIL.CostCenter_Code"
            Qry += " WHERE TSPL_ACQUISITION_HEAD.Status=1 AND TSPL_ACQUISITION_HEAD.Loc_Code='" + txtFromEntity.Value + "' and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 AND  not exists (select 1 from TSPL_ASSET_SCRAP_DETAIL where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code)"
            If clsCommon.CompairString(StrTransType, "Issue") = CompairStringResult.Equal Then
                Qry += " AND TSPL_ACQUISITION_DETAIL.Is_Issued='N'"
            Else
                Qry += " AND TSPL_ACQUISITION_DETAIL.Is_Issued='Y'"
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            Dim ii As Integer = 0
            For Each dr As DataRow In dt.Rows
                dgvVisi.Rows.AddNew()
                ii += 1
                dgvVisi.CurrentRow.Cells(colLineNo).Value = ii
                dgvVisi.CurrentRow.Cells(colSelect).Value = False
                dgvVisi.CurrentRow.Cells(colAsset_Id).Value = clsCommon.myCstr(dr("Asset_Code"))
                dgvVisi.CurrentRow.Cells(colAsset_Name).Value = clsCommon.myCstr(dr("Asset_Name"))
                dgvVisi.CurrentRow.Cells(colSpecification).Value = clsCommon.myCstr(dr("Asset_Specification"))
                If chkReturn.IsChecked Then
                    dgvVisi.CurrentRow.Cells(colTempTranDate).Value = clsCommon.GetPrintDate(clsCommon.myCstr(dr("IssueDate")), "dd/MMM/yyyy hh:mm tt")
                End If
                dgvVisi.CurrentRow.Cells(colTransDate).Value = Nothing
                dgvVisi.CurrentRow.Cells(colFrom).Value = clsCommon.myCstr(dr("FromEntity"))
                dgvVisi.CurrentRow.Cells(colFromDesc).Value = clsCommon.myCstr(dr("FromEntityDesc"))
                dgvVisi.CurrentRow.Cells(ColCostCenter).Value = clsCommon.myCstr(dr("CostCenter_Code"))
                dgvVisi.CurrentRow.Cells(ColCostCenterName).Value = clsCommon.myCstr(dr("CostCenter_Name"))
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub dgvVisi_CellValueChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvVisi.CellValueChanged
        If IsInsideLoadData = False Then
            If e.Column Is dgvVisi.Columns(colFrom) Then
                Qry = "select Emp_Code as Code,Emp_Name from TSPL_EMPLOYEE_MASTER"
                dgvVisi.CurrentRow.Cells(colFrom).Value = clsCommon.ShowSelectForm("fndFrmEntity", Qry, "Code", "", clsCommon.myCstr(dgvVisi.CurrentRow.Cells(colFrom).Value), "Code", "False")
                dgvVisi.CurrentRow.Cells(colFromDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE Emp_Code='" + dgvVisi.CurrentRow.Cells(colFrom).Value + "'"))
            End If
            If e.Column Is dgvVisi.Columns(ColCostCenter) Then
                Qry = "select CostCenter_Code as Code,CostCenter_Name from TSPL_FA_COST_CENTER_MASTER"
                dgvVisi.CurrentRow.Cells(ColCostCenter).Value = clsCommon.ShowSelectForm("fndFrmEntity", Qry, "Code", "", clsCommon.myCstr(dgvVisi.CurrentRow.Cells(ColCostCenter).Value), "Code", "False")
                dgvVisi.CurrentRow.Cells(ColCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CostCenter_Name from TSPL_FA_COST_CENTER_MASTER WHERE CostCenter_Code='" + dgvVisi.CurrentRow.Cells(ColCostCenter).Value + "'"))
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub txtFromEntity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromEntity._MYValidating
        Try
            Dim WhrCls As String = ""
            'If chkIssue.IsChecked = True Then
            Qry = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            WhrCls = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtFromEntity.Value = clsCommon.ShowSelectForm("finderAssetTrans", Qry, "Code", WhrCls, txtFromEntity.Value, "Code", isButtonClicked)
            lblFromEntity.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromEntity.Value + "'"))
            'ElseIf chkReturn.IsChecked = True Then
            'Qry = "Select Cost_Code as Code, Cost_name as Name from TSPL_CostCenter_MASTER"
            'txtFromEntity.Value = clsCommon.ShowSelectForm("finderAssetTrans", Qry, "Code", WhrCls, txtFromEntity.Value, "Code", isButtonClicked)
            'lblFromEntity.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cost_name from TSPL_CostCenter_MASTER where Cost_Code='" + txtFromEntity.Value + "'"))
            'End If
            If chkIssue.IsChecked Then
                LoadDetails(txtFromEntity.Value, "Issue")
            Else
                LoadDetails(txtFromEntity.Value, "Return")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIssue_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIssue.ToggleStateChanged
        Try
            If chkIssue.IsChecked Then
                RadLabel1.Text = "From"
                LoadDetails(txtFromEntity.Value, "Issue")
            Else
                RadLabel1.Text = "To"
                LoadDetails(txtFromEntity.Value, "Return")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
