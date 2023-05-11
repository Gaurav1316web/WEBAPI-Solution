''BM00000008148
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmApprovalScreen
    Inherits FrmMainTranScreen

#Region "variables"
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
    Dim SettingCapex As Boolean = False
    Const colLevelNo As String = "LevelNo"
    Const colLevel_UserCode As String = "LevelUserCode"
    Const colLevel_UserName As String = "LevelUserName"
    Const colLevel_Department As String = "LevelDepartment"
    Const colLevel_DepartmentName As String = "LevelDepartmentName"
    Const colAmount_Qty As String = "Amount_Qty"
    Const colAmount_Qty_Type As String = "Amt_Qty_Type" 'min,max

#End Region
    ''UDL/10/05/18-000160 by balwinder on 21/05/2017.
    Private Sub frmApprovalScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, Nothing) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Throw New Exception("Unauthorized access of Work flow of Approval.")
            Me.Close()
        End If
        SettingCapex = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = 1)
        If SettingCapex Then
            MyLabel39.Visible = True
            ddl_category.Visible = True
        Else
            MyLabel39.Visible = False
            ddl_category.Visible = False
        End If

        SetUserMgmtNew()
        LoadBlankGrid()
        LoadApprovalType()

        LoadModuleType()
        LoadTrnsListOfSelectedModeule()
        Reset()
        LoadCategory()

        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
        End If

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset window.")
        ButtonToolTip.SetToolTip(RadButton1, "Increase Level")

    End Sub

    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoTextbox As New GridViewTextBoxColumn
        Dim repoCombobox As New GridViewComboBoxColumn
        Dim repoDecimal As New GridViewDecimalColumn

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 60
        repoTextbox.HeaderText = "Level No."
        repoTextbox.Name = colLevelNo
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 130
        repoTextbox.HeaderText = "User Code"
        repoTextbox.Name = colLevel_UserCode
        repoTextbox.HeaderImage = Global.XpertERPAdminServices.My.Resources.Resources.search4
        repoTextbox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 200
        repoTextbox.HeaderText = "User Name"
        repoTextbox.Name = colLevel_UserName
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 130
        repoTextbox.HeaderText = "Department"
        repoTextbox.Name = colLevel_Department
        repoTextbox.HeaderImage = Global.XpertERPAdminServices.My.Resources.Resources.search4
        repoTextbox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextbox.IsVisible = False
        repoTextbox.VisibleInColumnChooser = False
        repoTextbox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 200
        repoTextbox.HeaderText = "Department Name"
        repoTextbox.Name = colLevel_DepartmentName
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = False
        repoTextbox.VisibleInColumnChooser = False
        repoTextbox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTextbox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.Name = colAmount_Qty
        repoDecimal.HeaderText = "Amount"
        repoDecimal.Width = 120
        repoDecimal.DecimalPlaces = 2
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.ThousandsSeparator = False
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoCombobox = New GridViewComboBoxColumn()
        repoCombobox.FormatString = ""
        repoCombobox.HeaderText = "Amount Type"
        repoCombobox.Name = colAmount_Qty_Type
        repoCombobox.Width = 100
        repoCombobox.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'Less' as Code,'<' as Name union all select 'Greater' as Code,'>' as Name union all select 'LessEqual' as Code,'<=' as Name union all select 'GreaterEqual' as Code,'>=' as Name union all select 'Equal' as Code,'=' as Name")
        repoCombobox.DisplayMember = "Name"
        repoCombobox.ValueMember = "Code"
        repoCombobox.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoCombobox)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.ShowFilteringRow = True
        gv1.EnableFiltering = True
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        repoTextbox = Nothing
        repoDecimal = Nothing
        repoCombobox = Nothing
    End Sub

    Private Sub LoadApprovalType()
        isInsideLoadData = True

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "RECEIPT-E") <> CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "AR-INVOICE") <> CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "AP-INVOICE") <> CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "PYMT-NEW") <> CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Amount"
            dr("Name") = "Amount Based"
            dt.Rows.Add(dr)
        End If
      

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "BOOK-DS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "CrLmt"
            dr("Name") = "Credit Limit"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "CSA-DO-TRN") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "CrLmt"
            dr("Name") = "Credit Limit"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "CSA-INV-TRN") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Rate"
            dr("Name") = "Rate"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "DEL-NOTE-FS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "CrLmt"
            dr("Name") = "Credit Limit"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "DEL-ORD-PS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "CrLmt"
            dr("Name") = "Credit Limit"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "DISPATCH-BS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "CrLmt"
            dr("Name") = "Credit Limit"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "LC-CREATION") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "IncAmt"
            dr("Name") = "Increase Amount"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-Material") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Rate"
            dr("Name") = "Rate"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-QC") = CompairStringResult.Equal Then
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-RECEIPT") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Other"
            dr("Name") = "Other"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-SRN-B") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Rate"
            dr("Name") = "Rate"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "SHIPMENT-PS") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "ARcpt"
            dr("Name") = "Advance Receipt"
            dt.Rows.Add(dr)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "VSP-Item") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "Rate"
            dr("Name") = "Rate"
            dt.Rows.Add(dr)
        End If

        cboApprovalType.DataSource = dt
        cboApprovalType.DisplayMember = "Name"
        cboApprovalType.ValueMember = "Code"
        isInsideLoadData = False
    End Sub

    Sub LoadCategory()

        ddl_category.DataSource = Xtra.GetCapexCombo()
        ddl_category.ValueMember = "Code"
        ddl_category.DisplayMember = "Name"
        ddl_category.SelectedValue = ""
    End Sub

    Private Sub SetUserMgmtNew()
        '--------richa Ticket no. BM00000003014 on 15/07/2014
        'MyBase.SetUserMgmt(clsUserMgtCode.frmApprovalLevelScreen)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function

        End If
        '--------richa Ticket no. BM00000003014
        btnSave.Visible = MyBase.isModifyFlag
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub LoadModuleType()
        isInsideLoadData = True
        Dim qry As String = "select Program_Code as Code,case when len(isnull(Re_Name,''))>0 then Re_Name else Program_Name end  as Name from TSPL_PROGRAM_MASTER " + Environment.NewLine +
            " inner join TSPL_MODULE_PERMISSION on TSPL_MODULE_PERMISSION.Module_Name=TSPL_PROGRAM_MASTER.Program_Code " + Environment.NewLine +
            " where Type='M' and Program_Code in ('" + clsUserMgtCode.ModuleSalesNew + "','" + clsUserMgtCode.ModuleProductSale + "','" + clsUserMgtCode.ModuleSaleDairy +
            "','" + clsUserMgtCode.ModuleCSASale + "','" + clsUserMgtCode.ModuleFreshSale + "','" + clsUserMgtCode.ModuleBulkSale + "','" + clsUserMgtCode.ModuleMerchantTradeSale +
            "','" + clsUserMgtCode.ModuleMCCMilkProcurement + "','" + clsUserMgtCode.ModuleBulkMilkProcurement + "','" + clsUserMgtCode.ModuleJobWorkOutWard + "','" + clsUserMgtCode.ModulePurchase + "','" +
            clsUserMgtCode.ModuleReceivable + "','" + clsUserMgtCode.ModulePayable + "','" + clsUserMgtCode.ModuleMaterial +
           "') order by SNo"

        cboModule.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboModule.ValueMember = "Code"
        cboModule.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        If Not isInsideLoadData Then
            LoadTrnsListOfSelectedModeule()
        End If
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        Dim dt As New DataTable
        Try
            Dim qry As String
            Dim whrCls As String = " and Program_Code in ('PO-ODR','PO-REQ','PO-INV','SALN-SO','SALE-ORDER','SALN-SP','DEL-NOTE-FS','DEL-ORD-PS','DISPATCH-BS','M-Material','VSP-Item','SHIPMENT-PS','CSA-INV-TRN','M-SRN-B','M-RECEIPT','M-QC','LC-CREATION','BOOK-DS','CSA-DO-TRN','SALE-RET-PS','RETRUN-DS','STORE-REQ','AP-INVOICE','PYMT-NEW','AR-INVOICE','RECEIPT-E','" + clsUserMgtCode.Transfer + "','" + clsUserMgtCode.frmMilkJobWorkTransferOther + "')"
            If Not clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleSaleDairy) = CompairStringResult.Equal Then ''std. sales
                If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleBulkMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Bulk Transaction')) " + whrCls + " "

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleMCCMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('MCC Transaction')) " + whrCls + " "
                Else
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                            "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Transaction')) " + whrCls + " "
                End If
            Else
                qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                    "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Fresh Sale')) " + whrCls + "  " &
                "union all " &
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Bulk Sale')) " + whrCls + " " &
                "union all " &
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Product Sale','Product postdaturn')) " + whrCls + " " &
                "union all " &
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Export Sale')) " + whrCls + " " &
                "union all " &
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('CSA Sale')) " + whrCls + " " &
                "union all " &
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " &
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(cboModule.SelectedValue) + "' and Program_Name in ('Transaction')) " + whrCls + " "
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            cboTransaction.DataSource = dt
            cboTransaction.DisplayMember = "Name"
            cboTransaction.ValueMember = "Code"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub

    Sub closeForm()
        Me.Close()
    End Sub

    Private Sub frmApprovalScreen_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt And e.KeyCode = Keys.C Then
                closeForm()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
                btnSave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
                btnDelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                btnReset.PerformClick()
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colLevel_UserCode) Then
                isCellValueChanged = True
                OpenUserCode(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colLevel_Department) Then
                isCellValueChanged = True
                OpenDepartment(True)
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub Reset()
        cboModule.SelectedValue = ""
        cboTransaction.SelectedValue = ""
        txtLevelNo.Text = 0
        cboApprovalType.SelectedValue = ""
        cboApprovalType.Enabled = True
        txtLocation.Value = ""
        ddl_category.SelectedValue = ""
        ChkAutoPost.Checked = False
        ChkAutoPost.IsThreeState = False

        chk_isdepartmentwise.Checked = False
        chk_isdepartmentwise.IsThreeState = False
        chkAllLevelApprovalRequired.Checked = False
        gv1.Rows.Clear()
        'gv1.Rows.AddNew()

        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnSave.Enabled = True
        cboModule.Enabled = True
        cboTransaction.Enabled = True
        txtLocation.Enabled = True
        ddl_category.Enabled = True
        cboModule.Focus()
        cboModule.Select()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub

    Sub LoadData(ByVal ModuleName As String, ByVal ScreenName As String, ByVal LevelNo As Decimal)
        Dim obj As New clsApprovalScreen()
        Try
            isInsideLoadData = True
            obj = clsApprovalScreen.GetData(ModuleName, ScreenName, txtLocation.Value, clsCommon.myCstr(ddl_category.SelectedValue))
            gv1.Rows.Clear()
            'gv1.Rows.AddNew()
            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                cboModule.SelectedValue = obj.Arr(0).Module_Code
                cboTransaction.SelectedValue = obj.Arr(0).Trans_Code
                txtLevelNo.Text = obj.Arr(0).Level
                cboApprovalType.SelectedValue = obj.Arr(0).Approval_Type
                ChkAutoPost.Checked = IIf(obj.Arr(0).Auto_Post = True, True, False)
                chk_isdepartmentwise.Checked = IIf(obj.Arr(0).Is_DepartmentWise = True, True, False)
                chkAllLevelApprovalRequired.Checked = obj.Arr(0).All_Level_Approval_Required
                txtLocation.Value = obj.Arr(0).Loc_Code
                ddl_category.SelectedValue = obj.Arr(0).Capex_Category
                For Each obj1 As clsApprovalScreen In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelNo).Value = obj1.No_Of_Level
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserCode).Value = obj1.User_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserName).Value = obj1.User_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_Department).Value = obj1.Department_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_DepartmentName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_Code ='" + obj1.Department_Code + "'", Nothing)) ''UDL/22/05/19-000298 by balwinder on 22/05/2019
                    If clsCommon.CompairString(cboApprovalType.SelectedValue, "Qty") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty).Value = obj1.Qty_Limit
                    ElseIf clsCommon.CompairString(cboApprovalType.SelectedValue, "Amount") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty).Value = obj1.Amount_Limit
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty_Type).Value = obj1.Amount_Qty_Type
                Next
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnSave.Enabled = True
                cboModule.Enabled = False
                cboTransaction.Enabled = False
                txtLocation.Enabled = False
                ddl_category.Enabled = False
            Else
                If clsCommon.myCdbl(txtLevelNo.Text) > 0 Then
                    If clsCommon.myLen(cboApprovalType.SelectedValue) > 0 AndAlso clsCommon.CompairString(cboApprovalType.SelectedValue, "Amount") = CompairStringResult.Equal Then
                        gv1.Columns(colAmount_Qty_Type).HeaderText = cboApprovalType.SelectedValue + " Type"
                        gv1.Columns(colAmount_Qty).HeaderText = cboApprovalType.SelectedValue + " Limit"
                    End If

                    gv1.Rows.Clear()
                    'gv1.Rows.AddNew()
                    For ii As Integer = 1 To clsCommon.myCdbl(txtLevelNo.Text)
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelNo).Value = ii
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserCode).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserName).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty_Type).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_Department).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_DepartmentName).Value = Nothing

                    Next ''loop

                    gv1.Focus()
                    gv1.Select()
                    gv1.CurrentRow = gv1.Rows(0)
                    gv1.CurrentColumn = gv1.Columns(colLevelNo)
                End If ''no of level
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(cboModule.SelectedValue) <= 0 Then
                cboModule.Focus()
                cboModule.Select()
                Errorcontrol.SetError(cboModule, "Select module.")
                Throw New Exception("Module code can not left blank.")
            Else
                Errorcontrol.ResetError(cboModule)
            End If

            If clsCommon.myLen(cboTransaction.SelectedValue) <= 0 Then
                cboTransaction.Focus()
                cboTransaction.Select()
                Errorcontrol.SetError(cboTransaction, "Select transaction.")
                Throw New Exception("Transaction code can not left blank.")
            Else
                Errorcontrol.ResetError(cboTransaction)
            End If

            If clsCommon.myCdbl(txtLevelNo.Text) <= 0 Then
                txtLevelNo.Focus()
                txtLevelNo.Select()
                Errorcontrol.SetError(txtLevelNo, "Fill no of level.")
                Throw New Exception("Fill no of levels.")
            Else
                Errorcontrol.ResetError(txtLevelNo)
            End If
            
            CheckPOAndIndent()
            Dim arr As New List(Of String)
            Dim user_Code As String = Nothing
            Dim no_of_level As Decimal = Nothing
            Dim app_type As String = Nothing
            Dim Department As String = Nothing
            Dim amount As Decimal = Nothing
            Dim max_no_of_level As Decimal = Nothing

            gv1.Focus()
            gv1.Select()
            For Each grow As GridViewRowInfo In gv1.Rows
                user_Code = clsCommon.myCstr(grow.Cells(colLevel_UserCode).Value)
                app_type = clsCommon.myCstr(grow.Cells(colAmount_Qty_Type).Value)
                amount = clsCommon.myCdbl(grow.Cells(colAmount_Qty).Value)
                no_of_level = clsCommon.myCdbl(grow.Cells(colLevelNo).Value)
                Department = clsCommon.myCstr(grow.Cells(colLevel_Department).Value)

                If no_of_level > max_no_of_level Then
                    max_no_of_level = no_of_level
                End If

                If no_of_level > 0 Then
                    If chk_isdepartmentwise.Checked Then
                        user_Code = user_Code + "-" + Department
                    End If

                    If Not arr.Contains(user_Code) Then
                        arr.Add(user_Code)
                    Else
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colLevel_UserCode)
                        Throw New Exception("Duplicate user " + IIf(chk_isdepartmentwise.Checked, "-Department", "") + " mapped at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If chk_isdepartmentwise.Checked Then
                        If clsCommon.myLen(Department) <= 0 Then
                            gv1.CurrentRow = gv1.Rows(grow.Index)
                            gv1.CurrentColumn = gv1.Columns(colLevel_Department)
                            Throw New Exception("Fill Department at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) <= 0 AndAlso amount < 0 Then
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty)
                        Throw New Exception("Fill " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " limit at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) > 0 AndAlso amount < 0 Then ''UDL/29/04/19-000293 by balwinder on 09/05/2019
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty)
                        Throw New Exception("Fill " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " limit at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) <= 0 AndAlso amount > 0 Then
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty_Type)
                        Throw New Exception("Select " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " type at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                End If
            Next

            If arr Is Nothing OrElse arr.Count <= 0 Then
                Throw New Exception("No record found.")
            End If

            If clsCommon.myCdbl(txtLevelNo.Text) <> max_no_of_level Then
                Throw New Exception("Set approval level are not match with grid level.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub CheckPOAndIndent()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                txtLocation.Select()
                Errorcontrol.SetError(txtLocation, "Select Location.")
                Throw New Exception("Select Location.")
            Else
                Errorcontrol.ResetError(txtLocation)
            End If
            If SettingCapex Then
                If clsCommon.myLen(ddl_category.SelectedValue) <= 0 Then
                    ddl_category.Focus()
                    ddl_category.Select()
                    Errorcontrol.SetError(ddl_category, "Select capex category.")
                    Throw New Exception("Select capex category.")
                Else
                    Errorcontrol.ResetError(ddl_category)
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim obj As New clsApprovalScreen()
        Dim obj1 As New clsApprovalScreen()
        Try
            obj = New clsApprovalScreen()
            obj.Arr = New List(Of clsApprovalScreen)

            If Not AllowToSave() Then
                Exit Sub
            End If

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmApprovalLevelScreen, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            For Each grow As GridViewRowInfo In gv1.Rows
                obj1 = New clsApprovalScreen()

                obj1.Module_Code = clsCommon.myCstr(cboModule.SelectedValue)
                obj1.Trans_Code = clsCommon.myCstr(cboTransaction.SelectedValue)
                obj1.Level = clsCommon.myCstr(txtLevelNo.Text)
                obj1.Approval_Type = clsCommon.myCstr(cboApprovalType.SelectedValue)
                obj1.Auto_Post = clsCommon.myCBool(ChkAutoPost.Checked)
                obj1.Is_DepartmentWise = clsCommon.myCBool(chk_isdepartmentwise.Checked)
                obj1.Department_Code = clsCommon.myCstr(grow.Cells(colLevel_Department).Value)
                obj1.Loc_Code = txtLocation.Value
                obj1.Capex_Category = clsCommon.myCstr(ddl_category.SelectedValue)
                obj1.User_Code = clsCommon.myCstr(grow.Cells(colLevel_UserCode).Value)
                obj1.No_Of_Level = clsCommon.myCdbl(grow.Cells(colLevelNo).Value)
                obj1.All_Level_Approval_Required = chkAllLevelApprovalRequired.Checked
                If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Qty") = CompairStringResult.Equal Then
                    obj1.Qty_Limit = clsCommon.myCdbl(grow.Cells(colAmount_Qty).Value)
                    obj1.Amount_Limit = 0
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal Then
                    obj1.Amount_Limit = clsCommon.myCdbl(grow.Cells(colAmount_Qty).Value)
                    obj1.Qty_Limit = 0
                End If

                obj1.Amount_Qty_Type = clsCommon.myCstr(grow.Cells(colAmount_Qty_Type).Value)

                If obj1.No_Of_Level > 0 AndAlso clsCommon.myLen(obj1.User_Code) > 0 Then
                    obj.Arr.Add(obj1)
                End If
            Next

            If obj.Arr Is Nothing OrElse obj.Arr.Count <= 0 Then
                Throw New Exception("No record found to save.")
            End If

            If clsApprovalScreen.SaveData(obj) Then
                clsCommon.MyMessageBoxShow("Data saved successfully.")

                LoadData(cboModule.SelectedValue, cboTransaction.SelectedValue, clsCommon.myCdbl(txtLevelNo.Text))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            obj1 = Nothing
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(cboModule.SelectedValue) <= 0 Then
                cboModule.Focus()
                cboModule.Select()
                Errorcontrol.SetError(cboModule, "Select module.")
                Throw New Exception("Module code can not left blank.")
            Else
                Errorcontrol.ResetError(cboModule)
            End If

            If clsCommon.myLen(cboTransaction.SelectedValue) <= 0 Then
                cboTransaction.Focus()
                cboTransaction.Select()
                Errorcontrol.SetError(cboTransaction, "Select transaction.")
                Throw New Exception("Transaction code can not left blank.")
            Else
                Errorcontrol.ResetError(cboTransaction)
            End If
            CheckPOAndIndent()
            If Not clsCommon.MyMessageBoxShow("Are you sure,want to delete setting.", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            If clsApprovalScreen.DeleteData(clsCommon.myCstr(cboModule.SelectedValue), clsCommon.myCstr(cboTransaction.SelectedValue), txtLocation.Value, clsCommon.myCstr(ddl_category.SelectedValue)) Then
                myMessages.delete()
                Reset()
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click ''save layout
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click '' delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(cboModule.SelectedValue) <= 0 Then
                cboModule.Focus()
                cboModule.Select()
                Errorcontrol.SetError(cboModule, "Select module.")
                Throw New Exception("Module code can not left blank.")
            Else
                Errorcontrol.ResetError(cboModule)
            End If

            If clsCommon.myLen(cboTransaction.SelectedValue) <= 0 Then
                cboTransaction.Focus()
                cboTransaction.Select()
                Errorcontrol.SetError(cboTransaction, "Select transaction.")
                Throw New Exception("Transaction code can not left blank.")
            Else
                Errorcontrol.ResetError(cboTransaction)
            End If
            CheckPOAndIndent()
            LoadData(clsCommon.myCstr(cboModule.SelectedValue), clsCommon.myCstr(cboTransaction.SelectedValue), clsCommon.myCdbl(txtLevelNo.Text))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If Not isInsideLoadData Then
                If e.RowIndex >= 0 Then
                    If (e.Column Is gv1.Columns(colAmount_Qty) OrElse e.Column Is gv1.Columns(colAmount_Qty_Type)) AndAlso clsCommon.CompairString(cboApprovalType.SelectedValue, "Amount") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colAmount_Qty).ReadOnly = False
                        gv1.CurrentRow.Cells(colAmount_Qty_Type).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colAmount_Qty).ReadOnly = True
                        gv1.CurrentRow.Cells(colAmount_Qty_Type).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv1.Columns(colLevel_UserCode) Then
                        If e.RowIndex >= 0 Then  'Ticket No- UDL/31/05/18-000179
                            isCellValueChanged = True
                            OpenUserCode(False)
                            isCellValueChanged = False
                        End If
                    ElseIf e.Column Is gv1.Columns(colLevel_Department) Then
                        If e.RowIndex >= 0 Then  'Ticket No- UDL/31/05/18-000179
                            isCellValueChanged = True
                            OpenDepartment(False)
                            isCellValueChanged = False
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub OpenUserCode(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select user_code as Code,user_name as Name from tspl_user_master"
        Dim arr As New ArrayList()
        Dim arr1 As New ArrayList()
        Dim intRow As Integer = gv1.CurrentRow.Index

        Dim xUser() As String = Nothing
        If clsCommon.myLen(gv1.CurrentRow.Cells(colLevel_UserCode).Value) > 0 Then
            xUser = clsCommon.myCstr(gv1.CurrentRow.Cells(colLevel_UserCode).Value).Replace("'", "").Split(",")

            For Each Str As String In xUser
                If clsCommon.myLen(Str) > 0 AndAlso Not arr.Contains(Str) Then
                    arr.Add(Str)
                End If
            Next
        End If

        arr1 = clsCommon.ShowMultipleSelectForm("USERMULAPPFND", qry, "Code", "Name", arr, Nothing)

        If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
            For ii As Integer = 0 To arr1.Count - 1
                If ii = 0 Then
                    gv1.Rows(intRow).Cells(colLevel_UserCode).Value = clsCommon.myCstr(arr1(ii))
                    gv1.Rows(intRow).Cells(colLevel_UserName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select user_name from tspl_user_master where user_code ='" + clsCommon.myCstr(arr1(ii)) + "'"))
                Else
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelNo).Value = clsCommon.myCdbl(gv1.Rows(intRow).Cells(colLevelNo).Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserCode).Value = clsCommon.myCstr(arr1(ii))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select user_name from tspl_user_master where user_code ='" + clsCommon.myCstr(arr1(ii)) + "'"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty_Type).Value = ""

                    gv1.Rows.Move(gv1.Rows.Count - 1, intRow)
                End If ''ii cond
            Next

            gv1.CurrentRow = gv1.Rows(intRow)
            gv1.CurrentColumn = gv1.Columns(colLevel_UserCode)
        Else
            gv1.CurrentRow.Cells(colLevel_UserCode).Value = ""
            gv1.CurrentRow.Cells(colLevel_UserName).Value = ""
        End If
        
    End Sub

    Private Sub OpenDepartment(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE"
        gv1.CurrentRow.Cells(colLevel_Department).Value = clsCommon.ShowSelectForm("NLVLDPTFND", qry, "Code", " Seg_No=3", clsCommon.myCstr(gv1.CurrentRow.Cells(colLevel_Department).Value), "Code", isButtonClicked)
        gv1.CurrentRow.Cells(colLevel_DepartmentName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLevel_Department).Value) + "'", Nothing))
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        Try
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                'If gv1.CurrentRow.Index > 0 Then
                '    gv1.CurrentRow.Cells(colLevelNo).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.Rows(intCurrRow - 1).Cells(colLevelNo).Value) + 1)
                'Else
                '    gv1.CurrentRow.Cells(colLevelNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                'End If

                'If intCurrRow = gv1.Rows.Count - 1 Then
                '    gv1.Rows.AddNew()
                '    gv1.CurrentRow = gv1.Rows(intCurrRow)
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chk_isdepartmentwise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_isdepartmentwise.ToggleStateChanged
        gridcolvisible()
    End Sub

    Sub gridcolvisible()
        If chk_isdepartmentwise.Checked Then
            gv1.Columns(colLevel_Department).IsVisible = True
            gv1.Columns(colLevel_Department).VisibleInColumnChooser = True
            gv1.Columns(colLevel_DepartmentName).IsVisible = True
            gv1.Columns(colLevel_DepartmentName).VisibleInColumnChooser = True
        Else
            gv1.Columns(colLevel_Department).IsVisible = False
            gv1.Columns(colLevel_Department).VisibleInColumnChooser = False
            gv1.Columns(colLevel_DepartmentName).IsVisible = False
            gv1.Columns(colLevel_DepartmentName).VisibleInColumnChooser = False
        End If
    End Sub

    Private Sub cboTransaction_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTransaction.SelectedIndexChanged
        If Not isInsideLoadData Then
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "SALN-SP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-Material") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "PO-INV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "DEL-ORD-PS") = CompairStringResult.Equal Then
                chk_isdepartmentwise.Visible = True
                chk_isdepartmentwise.Checked = False
            Else
                chk_isdepartmentwise.Visible = False
                chk_isdepartmentwise.Checked = False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                Panel1.Visible = True
            Else
                Panel1.Visible = False
            End If
            gridcolvisible()
            LoadApprovalType()
        End If
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.Value = clsCommon.ShowSelectForm("BILLTOxxCPO", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim query As String = "select TSPL_APPROVAL_LEVEL_SCREEN.Module_Code as Module,TSPL_APPROVAL_LEVEL_SCREEN.TRANS_Code as [Transaction],TSPL_APPROVAL_LEVEL_SCREEN.Loc_Code as Location"
        If SettingCapex Then
            query += ",TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category as [Category]"
        End If
        query += ",TSPL_APPROVAL_LEVEL_SCREEN.LEVEL as [NoOfLevel],case when Auto_Post=1 then 'Y' else 'N' end as [AutoPost],case when All_Level_Approval_Required =1 then 'Y' else 'N' end as [AllLevelApprovalRequired],TSPL_APPROVAL_LEVEL_SCREEN.No_Of_Level as [UserLevel],TSPL_APPROVAL_LEVEL_SCREEN.User_Code as [User],case when TSPL_APPROVAL_LEVEL_SCREEN.Is_DepartmentWise=1 then 'Y' else 'N' end as [DepartmentWise],TSPL_APPROVAL_LEVEL_SCREEN.Department_Code as [Department],TSPL_APPROVAL_LEVEL_SCREEN.Approval_Type as [ApprovalType],TSPL_APPROVAL_LEVEL_SCREEN.Amount_Limit as [Amount]," + Environment.NewLine + _
        "case when Amount_Qty_Type='Less' then '<' when Amount_Qty_Type='Greater' then '>' when Amount_Qty_Type='LessEqual' then '<=' when Amount_Qty_Type='GreaterEqual' then '>=' when Amount_Qty_Type='Equal' then '=' else '' end  [AmountType]" + Environment.NewLine + _
        " from TSPL_APPROVAL_LEVEL_SCREEN"
        transportSql.ExporttoExcel(query, Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            isInsideLoadData = True
            Dim Flag As Boolean = False
            If SettingCapex Then
                Flag = transportSql.importExcel(dgv, "Module", "Transaction", "Location", "Category", "NoOfLevel", "AutoPost", "AllLevelApprovalRequired", "UserLevel", "User", "DepartmentWise", "Department", "ApprovalType", "Amount", "AmountType")
            Else
                Flag = transportSql.importExcel(dgv, "Module", "Transaction", "Location", "NoOfLevel", "AutoPost", "AllLevelApprovalRequired", "UserLevel", "User", "DepartmentWise", "Department", "ApprovalType", "Amount", "AmountType")
            End If
            If Flag Then
                Dim arr As New List(Of clsTempMTLC)
                Dim arrTemp As New List(Of String)
                Dim lineNo As Integer
                Try
                    For ii As Integer = 0 To dgv.RowCount - 1
                        lineNo = ii
                        If clsCommon.myLen(dgv.Rows(ii).Cells("Module").Value) <= 0 OrElse clsCommon.myLen(dgv.Rows(ii).Cells("Transaction").Value) <= 0 Then
                            Continue For
                        End If
                        Dim obj As New clsTempMTLC
                        obj.Module_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("Module").Value)
                        obj.Transaction_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("Transaction").Value)
                        cboModule.SelectedIndex = -1
                        cboModule.SelectedValue = obj.Module_Code
                        If cboModule.SelectedIndex < 0 Then
                            Throw New Exception("Invalid Module code " + obj.Module_Code)
                        End If
                        LoadTrnsListOfSelectedModeule()
                        cboTransaction.SelectedIndex = -1
                        cboTransaction.SelectedValue = obj.Transaction_Code
                        If cboTransaction.SelectedIndex < 0 Then
                            Throw New Exception("Invalid Transaction code " + obj.Transaction_Code)
                        End If
                        obj.Location_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("Location").Value)
                        If clsCommon.CompairString(obj.Transaction_Code, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                                Throw New Exception("Please Provide Location Code")
                            End If
                            obj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "'"))
                            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                                Throw New Exception("Invalid Location Code " + clsCommon.myCstr(dgv.Rows(ii).Cells("Location").Value))
                            End If
                            If SettingCapex Then
                                obj.Category_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("Category").Value)
                                If clsCommon.myLen(obj.Category_Code) <= 0 Then
                                    Throw New Exception("Please Provide Category Code")
                                End If
                                ddl_category.SelectedIndex = -1
                                ddl_category.SelectedValue = obj.Category_Code
                                If ddl_category.SelectedIndex < 0 Then
                                    Throw New Exception("Invalid category " + obj.Category_Code)
                                End If
                            End If
                        Else
                            obj.Location_Code = ""
                        End If
                        Dim strMerge As String = clsCommon.myCstr(obj.Module_Code + obj.Transaction_Code + obj.Location_Code + obj.Category_Code).ToUpper()
                        If Not arrTemp.Contains(strMerge) Then
                            arrTemp.Add(strMerge)
                            arr.Add(obj)
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception("Error at Line No " + clsCommon.myCstr(lineNo + 2) + Environment.NewLine + ex.Message)
                End Try
                If arr Is Nothing OrElse arr.Count <= 0 Then
                    Throw New Exception("No valid row found to import")
                End If
                lineNo = 0
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For Each obj As clsTempMTLC In arr
                        Dim objAprrovalMain As New clsApprovalScreen()
                        objAprrovalMain.Arr = New List(Of clsApprovalScreen)
                        For ii As Integer = 0 To dgv.RowCount - 1
                            lineNo = ii
                            Flag = False
                            If clsCommon.CompairString(obj.Module_Code, clsCommon.myCstr(dgv.Rows(ii).Cells("Module").Value)) = CompairStringResult.Equal Then
                                If clsCommon.CompairString(obj.Transaction_Code, clsCommon.myCstr(dgv.Rows(ii).Cells("Transaction").Value)) = CompairStringResult.Equal Then
                                    Flag = True
                                    If Not (clsCommon.myLen(obj.Location_Code) > 0 AndAlso clsCommon.CompairString(obj.Location_Code, clsCommon.myCstr(dgv.Rows(ii).Cells("Location").Value)) = CompairStringResult.Equal) Then
                                        Flag = False
                                    End If
                                    If SettingCapex Then
                                        If Not (clsCommon.myLen(obj.Category_Code) > 0 AndAlso clsCommon.CompairString(obj.Category_Code, clsCommon.myCstr(dgv.Rows(ii).Cells("Category").Value)) = CompairStringResult.Equal) Then
                                            Flag = False
                                        End If
                                    End If
                                End If
                            End If
                            If Flag Then
                                Dim objAP As New clsApprovalScreen()
                                objAP = New clsApprovalScreen()
                                objAP.Module_Code = obj.Module_Code
                                objAP.Trans_Code = obj.Transaction_Code
                                objAP.Loc_Code = obj.Location_Code
                                objAP.Capex_Category = obj.Category_Code

                                objAP.Level = clsCommon.myCdbl(dgv.Rows(ii).Cells("NoOfLevel").Value)
                                If objAP.Level <= 0 Then
                                    Throw New Exception("NoOfLevel should be greater than 0")
                                End If

                                objAP.Auto_Post = (clsCommon.CompairString(clsCommon.myCstr(dgv.Rows(ii).Cells("AutoPost").Value), "Y") = CompairStringResult.Equal)
                                objAP.All_Level_Approval_Required = (clsCommon.CompairString(clsCommon.myCstr(dgv.Rows(ii).Cells("AllLevelApprovalRequired").Value), "Y") = CompairStringResult.Equal)
                                objAP.No_Of_Level = clsCommon.myCdbl(dgv.Rows(ii).Cells("UserLevel").Value)
                                If objAP.No_Of_Level <= 0 Then
                                    Throw New Exception("UserLevel should be greater than 0")
                                End If

                                objAP.User_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("User").Value)
                                If clsCommon.myLen(objAP.User_Code) <= 0 Then
                                    Throw New Exception("Please provide user code")
                                End If
                                objAP.User_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select User_Code from tspl_user_master where User_Code='" + objAP.User_Code + "'", trans))
                                If clsCommon.myLen(objAP.User_Code) <= 0 Then
                                    Throw New Exception("Invalid user code " + clsCommon.myCstr(dgv.Rows(ii).Cells("User").Value))
                                End If
                                If clsCommon.CompairString(objAP.Trans_Code, "SALN-SP") = CompairStringResult.Equal OrElse clsCommon.CompairString(objAP.Trans_Code, "M-Material") = CompairStringResult.Equal OrElse clsCommon.CompairString(objAP.Trans_Code, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(objAP.Trans_Code, clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal OrElse clsCommon.CompairString(objAP.Trans_Code, clsUserMgtCode.mbtnPurchaseRequistion) OrElse clsCommon.CompairString(objAP.Trans_Code, "PO-INV") = CompairStringResult.Equal OrElse clsCommon.CompairString(objAP.Trans_Code, "DEL-ORD-PS") = CompairStringResult.Equal Then
                                    objAP.Is_DepartmentWise = (clsCommon.CompairString(clsCommon.myCstr(dgv.Rows(ii).Cells("DepartmentWise").Value), "Y") = CompairStringResult.Equal)
                                    If objAP.Is_DepartmentWise Then
                                        objAP.Department_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("Department").Value)
                                        If clsCommon.myLen(objAP.Department_Code) <= 0 Then
                                            Throw New Exception("Please provide Department code")
                                        End If
                                        objAP.Department_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_CODE from tspl_department_master where DEPARTMENT_CODE='" + objAP.Department_Code + "'", trans))
                                        If clsCommon.myLen(objAP.Department_Code) <= 0 Then
                                            Throw New Exception("Invalid Department " + clsCommon.myCstr(dgv.Rows(ii).Cells("Department").Value))
                                        End If
                                    End If
                                Else
                                    objAP.Is_DepartmentWise = False
                                End If
                                objAP.Approval_Type = clsCommon.myCstr(dgv.Rows(ii).Cells("ApprovalType").Value)
                                If clsCommon.myLen(objAP.Approval_Type) > 0 Then
                                    If Not clsCommon.CompairString(objAP.Approval_Type, "Amount") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(objAP.Approval_Type, "CrLmt") = CompairStringResult.Equal Then
                                            If Not (clsCommon.CompairString(obj.Transaction_Code, "BOOK-DS") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "CSA-DO-TRN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "DEL-NOTE-FS") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "DEL-ORD-PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "DISPATCH-BS") = CompairStringResult.Equal) Then
                                                Throw New Exception("Invalid ApprovalType" + objAP.Approval_Type)
                                            End If
                                        ElseIf clsCommon.CompairString(objAP.Approval_Type, "Rate") = CompairStringResult.Equal Then
                                            If Not (clsCommon.CompairString(obj.Transaction_Code, "CSA-INV-TRN") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "M-Material") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "M-SRN-B") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Transaction_Code, "VSP-Item") = CompairStringResult.Equal) Then
                                                Throw New Exception("Invalid ApprovalType" + objAP.Approval_Type)
                                            End If
                                        ElseIf clsCommon.CompairString(objAP.Approval_Type, "IncAmt") = CompairStringResult.Equal Then
                                            If Not (clsCommon.CompairString(obj.Transaction_Code, "LC-CREATION") = CompairStringResult.Equal) Then
                                                Throw New Exception("Invalid ApprovalType" + objAP.Approval_Type)
                                            End If
                                        ElseIf clsCommon.CompairString(objAP.Approval_Type, "Other") = CompairStringResult.Equal Then
                                            If Not (clsCommon.CompairString(obj.Transaction_Code, "M-RECEIPT") = CompairStringResult.Equal) Then
                                                Throw New Exception("Invalid ApprovalType" + objAP.Approval_Type)
                                            End If
                                        ElseIf clsCommon.CompairString(objAP.Approval_Type, "ARcpt") = CompairStringResult.Equal Then
                                            If Not (clsCommon.CompairString(obj.Transaction_Code, "SHIPMENT-PS") = CompairStringResult.Equal) Then
                                                Throw New Exception("Invalid ApprovalType" + objAP.Approval_Type)
                                            End If
                                        End If
                                    End If
                                    objAP.Amount_Qty_Type = clsCommon.myCstr(dgv.Rows(ii).Cells("AmountType").Value)
                                    If clsCommon.myLen(objAP.Amount_Qty_Type) > 0 Then
                                        If clsCommon.CompairString(objAP.Amount_Qty_Type, "<") = CompairStringResult.Equal Then
                                            objAP.Amount_Qty_Type = "Less"
                                        ElseIf clsCommon.CompairString(objAP.Amount_Qty_Type, ">") = CompairStringResult.Equal Then
                                            objAP.Amount_Qty_Type = "Greater"
                                        ElseIf clsCommon.CompairString(objAP.Amount_Qty_Type, "<=") = CompairStringResult.Equal Then
                                            objAP.Amount_Qty_Type = "LessEqual"
                                        ElseIf clsCommon.CompairString(objAP.Amount_Qty_Type, ">=") = CompairStringResult.Equal Then
                                            objAP.Amount_Qty_Type = "GreaterEqual"
                                        ElseIf clsCommon.CompairString(objAP.Amount_Qty_Type, "=") = CompairStringResult.Equal Then
                                            objAP.Amount_Qty_Type = "Equal"
                                        Else
                                            Throw New Exception("Wrong symbol " + objAP.Amount_Qty_Type + ".[<,>,<=,>=,=]")
                                        End If
                                    End If
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(objAP.Approval_Type), "Qty") = CompairStringResult.Equal Then
                                    objAP.Qty_Limit = clsCommon.myCdbl(dgv.Rows(ii).Cells("Amount").Value)
                                    objAP.Amount_Limit = 0
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(objAP.Approval_Type), "Amount") = CompairStringResult.Equal Then
                                    objAP.Amount_Limit = clsCommon.myCdbl(dgv.Rows(ii).Cells("Amount").Value)
                                    objAP.Qty_Limit = 0
                                End If
                                objAprrovalMain.Arr.Add(objAP)
                            End If
                        Next
                        clsApprovalScreen.SaveData(objAprrovalMain, trans)

                        Dim strTemp As String = "Module-" + obj.Module_Code + ",Transaction-" + obj.Transaction_Code
                        If clsCommon.myLen(obj.Location_Code) > 0 Then
                            strTemp += ",Location- " + obj.Location_Code
                        End If
                        If clsCommon.myLen(obj.Category_Code) > 0 Then
                            strTemp += " ,Category- " + obj.Category_Code
                        End If

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQry("LEVEL", obj), trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                            Throw New Exception("NoOfLevel Should be same for " + strTemp)
                        End If
                        dt = clsDBFuncationality.GetDataTable(GetQry("Approval_Type", obj), trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                            Throw New Exception("ApprovalType Should be same for " + strTemp)
                        End If
                        dt = clsDBFuncationality.GetDataTable(GetQry("Auto_Post", obj), trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                            Throw New Exception("AutoPost Should be same for " + strTemp)
                        End If
                        dt = clsDBFuncationality.GetDataTable(GetQry("Is_DepartmentWise", obj), trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                            Throw New Exception("DepartmentWise Should be same for " + strTemp)
                        End If
                        dt = clsDBFuncationality.GetDataTable(GetQry("All_Level_Approval_Required", obj), trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                            Throw New Exception("AllLevelApprovalRequired Should be same for " + strTemp)
                        End If
                    Next
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data imported successfully.")
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception("Error at Line No " + clsCommon.myCstr(lineNo + 2) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Function GetQry(ByVal strColumn As String, ByVal obj As clsTempMTLC) As String
        Dim qry As String = "select " + strColumn + " from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code='" + obj.Module_Code + "' and TRANS_Code='" + obj.Transaction_Code + "'  "
        If clsCommon.myLen(obj.Location_Code) > 0 Then
            qry += " and Loc_Code='" + obj.Location_Code + "'"
        End If
        If clsCommon.myLen(obj.Category_Code) > 0 Then
            qry += " and Capex_Category='" + obj.Category_Code + "'"
        End If
        qry += "group by " + strColumn + ""
        Return qry
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        ''UDL/31/10/18-000237 by balwinder on 15/11/2018
        txtLevelNo.Value += 1
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelNo).Value = txtLevelNo.Value
        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserCode).Value = Nothing
        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserName).Value = Nothing
        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty).Value = Nothing
        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount_Qty_Type).Value = ""
        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_Department).Value = Nothing
        gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_DepartmentName).Value = Nothing
    End Sub
End Class

Public Class clsTempMTLC
    Public Module_Code As String
    Public Transaction_Code As String
    Public Location_Code As String
    Public Category_Code As String
    Public arr As List(Of clsApprovalScreen)
End Class




